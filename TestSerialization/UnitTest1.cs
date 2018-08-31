using NUnit.Framework;
using Octopus.Tests;
using Shouldly;

namespace TestSerialization
{
    [TestFixture]
    public class SerializerTest1 : IntegrationTestBase
    {
        [Test]
        public void FirstDeserialization()
        {

            var dd = (TestPerson)ServiceStack.Text.Json.JsonReader<TestPerson>.Parse(
                "{\"dateOfBirth\":\"1992-08-26\",\"dateOfDeath\":\"2022-08-26\"}");
            dd.DateOfBirth.ShouldNotBeNull();
        }
    }
}