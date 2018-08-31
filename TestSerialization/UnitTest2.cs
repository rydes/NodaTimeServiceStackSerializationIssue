using NUnit.Framework;
using Octopus.Tests;
using ServiceStack.Text;
using Shouldly;

namespace TestSerialization
{
    [TestFixture]
    public class SerializerTest2 : IntegrationTestBase
    {
        [Test]
        public void SecondDeserialization()
        {
            var dd = (TestPerson)ServiceStack.Text.Json.JsonReader<TestPerson>.Parse(
                "{\"dateOfBirth\":\"1992-08-26\"}");
            dd.DateOfBirth.ShouldNotBeNull();
        }
    }
}
