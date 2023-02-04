using ConsoleApp1.Models;
using ConsoleApp1.Requests;
using ConsoleApp1.Responses;
using FastEndpoints;
using LiteDB;

namespace ConsoleApp1.Endpoints
{
    public class People2Endpoint : Endpoint<Person, PersonResponse>
    {
        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("people");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Person req, CancellationToken ct)
        {
            Person person = new Person { Age= req.Age , Name = req.Name, Surname = req.Surname};
            using (var db = new LiteDatabase(@"Database\database.db"))
            {
                var collection = db.GetCollection<Person>("people");
                collection.Insert(collection.Count(), person);
            }

            await SendAsync(new()
            {
                Age = person.Age,
                Name = person.Name,
                Surname = person.Surname,
            }
            , cancellation: ct);
        }
    }
}
