using ConsoleApp1.Models;
using ConsoleApp1.Requests;
using ConsoleApp1.Responses;
using FastEndpoints;

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
            Program.people.Add(person);

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
