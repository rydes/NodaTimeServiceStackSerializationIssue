# NodaTimeServiceStackSerializationIssue
NodaTime and ServiceStack serialization isssue

Basicaly problem is that the JsConfig.Reset() doesn't really resets the configuration of ServiceStack.Text library to it's orginal state.
In reset method we replace all registered serializers with some default 
SerivceStack AbstractSerializers insted of removing configuration or give type.
It's caused by design of the library - a lot of static stuff.

So in my case after JsConfig.Reset() method being called after first test fixture two things happen:
1. NodaTime serializers are set to AbstractSerializer - it will always give null 
2. TestPerson type serializer is reset - now it uses the AbstractSerializer for date field

Then next OneTimeSetUp method is called for next test fixture. We register providers for NodaTime. But
when the second test is fire Json reader use cached config with AbstractSerializer for noda type. In the 
end we get null date value.

