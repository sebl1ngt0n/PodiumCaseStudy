using System.Threading.Tasks;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using PodiumCaseStudy.Tests.Context;

namespace PodiumCaseStudy.Tests.Tests
{
    [FeatureDescription("Testing the Mortgage controller")]
    public class MortgageProductsTests : FeatureFixture
    {
        [Scenario(DisplayName = "Getting all mortage products with a newly registered user")]
        public async Task Get_mortgage_products_with_of_age_user()
        {
            await Runner.WithContext<MoqMortgageContext>()
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
                    _ => _.Then_guid_returns_in_response(),
                    _ => _.When_mortgage_searched(Helper.StandardMortgageProductQueryModel)
                )
                .AddSteps(
                    _ => _.Then_status_code_is_returned(System.Net.HttpStatusCode.OK)
                )
                .AddAsyncSteps(
                    _ => _.Then_total_products_returned(3)
                )
                .RunAsync();
        }

        [Scenario(DisplayName = "Getting 0 mortage products with a newly registered underage user")]
        public async Task Get_mortgage_products_with_under_age_user()
        {
            await Runner.WithContext<MoqMortgageContext>()
                .AddSteps(
                    _ => _.Given_http_client(),
                    _ => _.Given_register_model(17)
                )
                .AddAsyncSteps(
                    _ => _.When_user_registers()
                )
                .AddSteps(
                    _ => _.Then_status_code_is_returned(System.Net.HttpStatusCode.OK)
                )
                .AddAsyncSteps(
                    _ => _.Then_guid_returns_in_response(),
                    _ => _.When_mortgage_searched(Helper.StandardMortgageProductQueryModel)
                )
                .AddSteps(
                    _ => _.Then_status_code_is_returned(System.Net.HttpStatusCode.OK)
                )
                .AddAsyncSteps(
                    _ => _.Then_total_products_returned(0)
                )
                .RunAsync();
        }

        [Scenario(DisplayName = "Getting 1 mortage products with a newly registered user and high LTV")]
        public async Task Get_mortgage_products_with_of_age_user_and_high_loan_to_value()
        {
            await Runner.WithContext<MoqMortgageContext>()
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
                    _ => _.Then_guid_returns_in_response(),
                    _ => _.When_mortgage_searched(Helper.HighLtvMortgageProductQueryModel)
                )
                .AddSteps(
                    _ => _.Then_status_code_is_returned(System.Net.HttpStatusCode.OK)
                )
                .AddAsyncSteps(
                    _ => _.Then_total_products_returned(1)
                )
                .RunAsync();
        }
    }
}
