using System.Threading.Tasks;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using PodiumCaseStudy.Tests.Context;

namespace PodiumCaseStudy.Tests.Tests
{
    [FeatureDescription("Testing the Register controller")]
    public class RegisterTests : FeatureFixture
    {
        [Scenario(DisplayName = "Registering a user through the API receives OK and Guid in Response")]
        public async Task Registering_user_received_ok_and_guid_id()
        {
            await Runner.WithContext<MoqRegisterContext>()
                .AddSteps(
                    _ => _.Given_http_client(),
                    _ => _.Given_register_model(25)
                )
                .AddAsyncSteps(
                    _ => _.When_user_registers()
                )
                .AddSteps(
                    _ => _.Then_status_code_is_returned(System.Net.HttpStatusCode.OK)
                )
                .AddAsyncSteps(
                    _ => _.Then_guid_returns_in_response()
                )
                .RunAsync();
        }
    }
}
