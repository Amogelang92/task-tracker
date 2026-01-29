using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace APITests
{
    [TestClass]
    public sealed class Test1
    {
        private static HttpClient _client = null;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory.CreateClient();
        }

        [TestMethod]
        public async Task GetTasks_ReturnsSeededTasks()
        {
            var response = await _client.GetAsync("/api/tasks");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var tasks = await response.Content.ReadFromJsonAsync<List<Object>>();
            Assert.IsNotNull(tasks);
            Assert.IsTrue(tasks.Count > 0);
        }

        [TestMethod]
        public async Task CreateTask_InvalidPayload_ReturnsBadRequest()
        {
            var task = new
            {
                description = "Missing title",
                status = "Empty",
                priority = "Low"
            };

            var response = await _client.PostAsJsonAsync("/api/tasks", task);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            var issue = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            Assert.IsNotNull(issue);
            Assert.AreEqual(400, issue.Status);
        }
    }
}
