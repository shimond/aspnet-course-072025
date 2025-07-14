

Dictionary<Person, string> personDictionary = new Dictionary<Person, string>();
var person1 = new Person { Name = "a", Age = 25, Address = "123 Main St" };
personDictionary.Add(person1, "a");

var person2 = new Person { Name = "a", Age = 25, Address = "123 Main St" };

var v = personDictionary[person2];
Console.WriteLine(v);

if(person1 == person2) // true
{

}

// Generate an array with 5 people
Person[] people = new Person[]
{
    new Person { Name = "a", Age = 25, Address = "123 Main St" },
    new Person { Name = "b", Age = 30, Address = "456 Oak Ave" },
    new Person { Name = "c", Age = 22, Address = "789 Pine Rd" },
    new Person { Name = "d", Age = 28, Address = "321 Maple Dr" },
    new Person { Name = "e", Age = 35, Address = "654 Elm St" }

};
var p1 = people[0];
//var p2 = new Person { Name = "f", Age = p1.Age, Address = p1.Address };
var p2 = p1 with { Name = "f" }; // copy and change only Name


foreach (var item in people)
{
    PrintPerson(item);
}
void PrintPerson(Person p)
{
    if (p.Age < 1)
    {
       // p.Age = 1;
    }
    Console.WriteLine($"Name: {p.Name}, Age: {p.Age}, Address: {p.Address}");
}
//version 1:
//public class Person
//{
//    public string Name { get; }
//    public int Age { get; } = 0;
//    public string? Address { get; }

//    public Person(string name, int age, string? address = null)
//    {
//        Name = name;
//        Age = age;
//        Address = address;
//    }
//}


//version 2:
// record is a reference type that provides built-in functionality for value equality,
// //immutability, and concise syntax for defining data structures.
public record Person
{
    public required string Name { get; init; }
    public int Age { get; init; } = 0;
    public string? Address { get; init; }
}

//
public record Person2(string Name, int Age, string? Address);
public record Worker(string Name, int Age, string? Address, string JobTitle) :
    Person2(Name, Age, Address);

    