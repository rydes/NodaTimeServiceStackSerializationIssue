using NodaTime;
using NodaTime.Serialization.ServiceStackText;
using NUnit.Framework;
using ServiceStack.Text;

namespace TestSerialization
{
    /// <summary>
    /// Base integration test class which starts service stack self hosted and raven in memory.
    /// </summary>
    public abstract class IntegrationTestBase
    {
        private readonly INodaSerializerSettings _defaultSerializers;

        protected IntegrationTestBase()
        {
            //DateTimeZoneProviders.Tzdb.CreateDefaultSerializersForNodaTime().DateTimeZoneSerializer.
            var timeZoneProvider = DateTimeZoneProviders.Tzdb;
            _defaultSerializers = timeZoneProvider.CreateDefaultSerializersForNodaTime();
        }

        [OneTimeSetUp]
        public void IntegrationTestFixtureSetup()
        {
            JsConfig.Reset();
            CustomExt.ConfigureSerializer(_defaultSerializers.LocalDateSerializer);
            //after registering custom serializers we need to refresh JsonReader
            ServiceStack.Text.Json.JsonReader<TestPerson>.Refresh();
        }

        [OneTimeTearDown]
        public void IntegrationTestFixtureTearDown()
        {
            JsConfig.Reset();
        }

    }

    public static class CustomExt
    {

        public static void ConfigureSerializer(IServiceStackSerializer<LocalDate> serializer)
        {
            JsConfig<LocalDate>.SerializeFn = args =>
            {
                var serialized = serializer.Serialize(args);
                return serialized;
            };
            JsConfig<LocalDate>.DeSerializeFn = args =>
            {
                var deserialized = serializer.Deserialize(args);
                return deserialized;
            };

            JsConfig<LocalDate?>.SerializeFn = arg =>
            {
                var serializedValue = serializer.Serialize(arg.Value);
                return serializedValue;
            };
            JsConfig<LocalDate?>.DeSerializeFn = s =>
            {
                var deserializedValue = serializer.Deserialize(s);
                return deserializedValue;
            };
        }
    }
}
