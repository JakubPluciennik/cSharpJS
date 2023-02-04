using ConsoleApp1.Models;
using ConsoleApp1.Requests;
using ConsoleApp1.Responses;
using FastEndpoints;
using LiteDB;

namespace ConsoleApp1.Endpoints
{
    public class PersonEndpoint : Endpoint<PersonRequest, PersonResponse>
    {
        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("person");
            AllowAnonymous();
        }

        public override async Task HandleAsync(PersonRequest req, CancellationToken ct)
        {
            using (var db = new LiteDatabase(@"Database\database.db"))
            {
                var collection = db.GetCollection<Person>("people");

                Person p = collection.FindById(req.Id);

                PersonResponse response = new PersonResponse { Name = p.Name, Surname = p.Surname, Age = p.Age };

                await SendAsync(new()
                {
                    Age = response.Age,
                    Name = response.Name,
                    Surname = response.Surname,
                }
            , cancellation: ct);
            }
        }
    }
}
