using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace COPOL.BLL.Models
{
    public class ParametersRepository
    {
        public static void SaveParameters(Parameters parameters, string fileName)
        {
            Serializer(parameters, fileName);
        }
        
        public static Parameters GetParameters(string fileName)
        {
            return Deserializer(fileName);
        }
        
        private static void Serializer(Parameters parameters, string fileName)
        {
            try
            {
                var serializer = new JsonSerializer();
                using var sw = new StreamWriter(fileName);
                using JsonWriter writer = new JsonTextWriter(sw);
                serializer.Serialize(writer, new List<Parameters>{ parameters });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private static Parameters Deserializer(string fileName)
        {
            try
            {
                var deserializer = new JsonSerializer();
                using var sr = new StreamReader(fileName);
                using var reader = new JsonTextReader(sr);
                {  
                    var listParameters = deserializer.Deserialize<List<Parameters>>(reader);
                    return listParameters.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}