using Newtonsoft.Json;

namespace FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Serialization
{
    public interface IJsonSerializer
    {
        T Deserialize<T>(string source);
        T DecodeAndDeserialize<T>(string source);
        string Serialize(object source, Formatting formatting = Formatting.Indented);
        string SerializeAndEncode(object source);
    }
}