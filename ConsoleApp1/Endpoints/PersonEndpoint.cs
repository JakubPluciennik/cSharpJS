using ConsoleApp1.Models;
using ConsoleApp1.Requests;
using ConsoleApp1.Responses;
using FastEndpoints;

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
            Person response = Program.people[req.Id];


            await SendAsync(new()
            {
                Age= response.Age,
                Name= response.Name,
                Surname= response.Surname,
            }
            , cancellation: ct);
        }
    }
}
