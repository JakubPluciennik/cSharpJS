using ConsoleApp1.Models;
using ConsoleApp1.Requests;
using ConsoleApp1.Responses;
using FastEndpoints;
using LiteDB;

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


            using (var db = new LiteDatabase(@"Database\database.db"))
            {
                List<PersonResponse> PeopleResponse = new List<PersonResponse>();
                var collection = db.GetCollection<Person>("people");

                for (int i = 0; i < collection.Count(); i++)
                {
                    Person p = collection.FindById(i);
                    PeopleResponse.Add(new PersonResponse { Age = p.Age, Name = p.Name, Surname = p.Surname });
                }
                var response = new PeopleResponse() { People = PeopleResponse };

                await SendAsync(response);
            }

        }
    }
}
