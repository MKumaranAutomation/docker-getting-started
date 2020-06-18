using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Generator.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Generator.IntegrationTests
{
    public class UsersTests : TestBase
    {
        [Test]
        public async Task GetUsers_Count_ShouldMatch([Values(1, 5, 10)] int count)
        {
            // Act
            var response = await Client.GetAsync($"/api/Users/users?count={count}");
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(await response.Content.ReadAsStringAsync());
            
            // Assert
            users.Count().Should().Be(count);
            users.ToList().ForEach(x =>
            {
                x.Name.Should().NotBeNull();
                x.Name.Should().NotBeEmpty();

                x.Email.Should().NotBeNull();
                x.Email.Should().NotBeEmpty();
                x.Email.Should().MatchRegex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");

                x.Phone.Should().NotBeNull();
                x.Phone.Should().NotBeEmpty();
            });
        }
    }
}