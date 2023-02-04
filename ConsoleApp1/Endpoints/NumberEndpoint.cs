using ConsoleApp1.Requests;
using ConsoleApp1.Responses;
using FastEndpoints;

namespace ConsoleApp1.Endpoints
{
    public class NumberEndpoint : Endpoint<NumberRequest, NumberResponse>
    {
        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("GetData");
            AllowAnonymous();
        }

        public override async Task HandleAsync(NumberRequest req, CancellationToken ct)
        {
            await SendAsync(new()
            {
                Value= req.Value*2,
            }, cancellation:ct);
        }
    }
}
