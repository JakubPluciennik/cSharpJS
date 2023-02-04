using ConsoleApp1.Models;
using FastEndpoints;

namespace ConsoleApp1
{
    internal static class Program
    {
        static List<Person> GeneratePeople(int n)
        {
            string[] names = { "Ryszard", "Aleks", "Kamil", "Grzegorz", "Michał", "Izabela", "Aleksandra", "Maria", "Joanna", "Daria" };
            string[] surnames = { "Walczak", "Bąk", "Krupa", "Szewczyk", "Wójcik", "Baran", "Pawlak" };

            Random r = Random.Shared;
            List<Person> people = new List<Person>();
            for (int i = 0; i < n; i++)
            {
                string name = names[r.Next(names.Length)];
                string surname = surnames[r.Next(surnames.Length)];
                int age = r.Next(18, 65);

                people.Add(new Person {Name = name, Surname = surname, Age = age });
            }

            return people;
        }
        public static List<Person> people = GeneratePeople(100);

        static void Main(string[] args)
        {
             

            var builder = WebApplication.CreateBuilder();
            builder.Services.AddFastEndpoints();

            builder.Services.AddCors(o =>
            o.AddDefaultPolicy(b =>
            b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            var app = builder.Build();

            app.UseCors();
            app.UseAuthorization();
            app.UseFastEndpoints();

            app.Run();
        }
    }
}