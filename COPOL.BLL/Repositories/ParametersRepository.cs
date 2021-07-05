using System;
using System.IO;
using COPOL.BLL.Models;
using Newtonsoft.Json;

namespace COPOL.BLL.Repositories
{
    public static class ParametersRepository
    {
        public static void SaveParameters(Parameters parameters, string fileName)
        {
            if (parameters == null)
            {
                throw new ArgumentException("Параметры не были заполнены.");
            }
            
            Serializer(parameters, fileName);
        }
        
        public static Parameters GetParameters(string fileName)
        {
            return Deserializer(fileName);
        }
        
        private static void Serializer(Parameters parameters, string fileName)
        {
            var serializer = new JsonSerializer();
            using var sw = new StreamWriter(fileName);
            using JsonWriter writer = new JsonTextWriter(sw);
            serializer.Serialize(writer, parameters);
        }

        private static Parameters Deserializer(string fileName)
        {
           
            var deserializer = new JsonSerializer();
            using var sr = new StreamReader(fileName);
            using var reader = new JsonTextReader(sr);
            return deserializer.Deserialize<Parameters>(reader);
        }
    }
}