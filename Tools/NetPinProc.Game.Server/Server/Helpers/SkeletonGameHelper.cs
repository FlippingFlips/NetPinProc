using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

namespace NetPinProc.Game.Manager.Server.Helpers
{
    public static class SkeletonGameHelper
    {
        public static string ConvertYamlToJson(string ymlContents)
        {            
            var deserializer = new DeserializerBuilder()
                .WithAttemptingUnquotedStringTypeDeserialization()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var yamlObject = deserializer.Deserialize(ymlContents);                
            return ConvertToJson(yamlObject);
        }
      
        public static string ConvertToJson(object yamlObject)
        {
            var serializer = new SerializerBuilder()
                .JsonCompatible()
                .Build();
            return serializer.Serialize(yamlObject);
        }
    }
}
