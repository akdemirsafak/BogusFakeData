using Bogus;
using Bogus.DataSets;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;



Lorem lorem= new Lorem();
var value= lorem.Paragraphs(3);
//value=lorem.Sentence(5);
//vs vs...

Console.WriteLine(value);


var addressFaker= new Faker<Address>("tr")//Optional paramter
    .RuleFor(a=>a.Id,f=>f.Random.Guid())
    .RuleFor(a=>a.City,f=>f.Address.City())
    .RuleFor(a=>a.StreetName,f=>f.Address.StreetName())
    .RuleFor(a=>a.ZipCode,f=>f.Address.ZipCode());

var userFaker=new Faker<User>("tr") //parametre girilmeyebilir.
    .RuleFor(u=>u.Id,i=>i.Random.Guid())
    .RuleFor(u=>u.FirstName,f=>f.Person.FirstName)
    .RuleFor(u=>u.LastName,f=>f.Person.LastName)
    .RuleFor(u=>u.UserName,f=> f.Person.UserName)
    .RuleFor(u=>u.BirthDate,f=>f.Person.DateOfBirth)
    .RuleFor(u=>u.Email,f=>f.Person.Email)
    .RuleFor(u=>u.Address,f=>f.PickRandom(addressFaker))
    .RuleFor(u=>u.Gender,f=>f.PickRandom<Gender>());




var generatedObject=userFaker.Generate(5); //Optional Parametre -Kaç tane kayıt oluşturulacağı girilebilir.
var opt=new JsonSerializerOptions()
{
    WriteIndented = true,
    Encoder=JavaScriptEncoder.Create(UnicodeRanges.All) //Türkçe karakterler de desteklenecek.
};
string valueAsJson=JsonSerializer.Serialize(generatedObject,opt);
Console.WriteLine(valueAsJson);



public enum Gender
{
    None, Male, Female
}
public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender Gender { get; set; }
    public Address Address { get; set; }

}

public class Address
{
    public Guid Id { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public string StreetName { get; set; }
}