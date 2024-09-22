using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int Salary { get; set; }

    public Employee(int id, string name, int age, int salary)
    {
        Id = id;
        Name = name;
        Age = age;
        Salary = salary;
    }
}

class Program
{
    static void Main()
    {
        List<Employee> employees = new List<Employee>();

        // Fájl beolvasása
        string[] lines = File.ReadAllLines("tulajdonsagok_100sor.txt");

        foreach (var line in lines)
        {

            string[] adatok = line.Split(';');

            if (adatok.Length != 4)
            {
                Console.WriteLine($"Hibás formátumú sor: {line}");
                continue;
            }

            try
            {
                employees.Add(new Employee(
                    int.Parse(adatok[0]),   // Azonosító
                    adatok[1],              // Név
                    int.Parse(adatok[2]),   // Kor
                    int.Parse(adatok[3])    // Kereset
                ));
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Nem sikerült feldolgozni a sort: {line}. Hiba: {ex.Message}");
            }
        }
        // 3. Az összes dolgozó nevének megjelenítése
        Console.WriteLine("Összes dolgozó neve:");
        foreach (var emp in employees)
        {
            Console.WriteLine(emp.Name);
        }
        // 4. Legjobban kereső dolgozó azonosítója és neve
        var maxSalary = employees.Max(e => e.Salary);
        var legjobbanKeresok = employees.Where(e => e.Salary == maxSalary);
        Console.WriteLine("\nLegjobban kereső dolgozó(k):");
        foreach (var emp in legjobbanKeresok)
        {
            Console.WriteLine($"Azonosító: {emp.Id}, Név: {emp.Name}, Kereset: {emp.Salary}");
        }
        // 5. Azok neve és kora, akiknek 10 évük van a nyugdíjig
        Console.WriteLine("\nAzok, akiknek 10 évük van a nyugdíjig:");
        var nyugdij10 = employees.Where(e => 65 - e.Age <= 10 && e.Age < 65);
        foreach (var emp in nyugdij10)
        {
            Console.WriteLine($"Név: {emp.Name}, Kor: {emp.Age}");
        }
        Console.ReadKey();
    }
}