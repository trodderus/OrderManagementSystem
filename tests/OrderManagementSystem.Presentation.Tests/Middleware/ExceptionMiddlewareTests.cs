using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OrderManagementSystem.Presentation.Middleware;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace OrderManagementSystem.Presentation.Tests.Middleware
{
    public class ExceptionMiddlewareTests: IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ExceptionMiddlewareTests(TestWebApplicationFactory factory)
        {
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.Configure(app =>
                {
                    app.UseRouting();
                    app.UseMiddleware<ExceptionMiddleware>();

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapGet("/throw", context =>
                        {
                            throw new InvalidOperationException("Boom!");
                        });
                    });
                });
            }).CreateClient();
        }

        [Fact]
        public async Task ExceptionMiddleware_Should_Return_ProblemDetails()
        {
            var response = await _client.GetAsync("/throw");

            response.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);

            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();

            problem.ShouldNotBeNull();
            problem!.Title.ShouldBe("An unhandled exception occurred.");
            problem.Detail.ShouldBe("Boom!");
            problem.Status.ShouldBe(500);
        }
    }
}
