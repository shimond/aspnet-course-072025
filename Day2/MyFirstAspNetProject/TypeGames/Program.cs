
// Generate an array with 5 people
Person[] people = new Person[]
{
    new Person { Name = 1, Age = 25, Address = "123 Main St" },
    new Person { Name = 2, Age = 30, Address = "456 Oak Ave" },
    new Person { Name = 3, Age = 22, Address = "789 Pine Rd" },
    new Person { Name = 4, Age = 28, Address = "321 Maple Dr" },
    new Person { Name = 5, Age = 35, Address = "654 Elm St" }
};

foreach (var item in people)
{
    PrintPerson(item);
}
void PrintPerson(Person p)
{
    if(p.Age < 1)
    {
        p.Age = 1;
    }
    Console.WriteLine($"Name: {p.Name}, Age: {p.Age}, Address: {p.Address}");
}
public class Person
{
    public string Name { get;  }
    public int Age { get; } = 0;
    public string? Address { get; }

    public Person(int age)
    {
        Age = age;
    }

}