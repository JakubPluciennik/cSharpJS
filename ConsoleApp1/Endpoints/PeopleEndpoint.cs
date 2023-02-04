using ConsoleApp1.Models;
using ConsoleApp1.Requests;
using ConsoleApp1.Responses;
using FastEndpoints;

namespace ConsoleApp1.Endpoints
{
    public class PeopleEndpoint : EndpointWithoutRequest<PeopleResponse>
    {
        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("people");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            List<PersonResponse> PeopleResponse = new List<PersonResponse>();

            foreach (Person p in Program.people)
            {
                PeopleResponse.Add(new PersonResponse { Age = p.Age, Name = p.Name, Surname = p.Surname });
            }

            var response = new PeopleResponse() { People = PeopleResponse };

            await SendAsync(response);
        }
    }
}
