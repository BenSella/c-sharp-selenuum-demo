using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagentoSelenuimAutomationDemo.Utils.Caching
{
    public static class CachingHelper
    {
        private const string NODATA = "no data found in appsettings.json";
        public static void UpdateCachingFile(string jsonFileLocation, string key, string newValue, string jsonFileName)
        {
            // Load the entire Caching.json file into a dynamic object
            string jsonFilePath = Path.Combine(jsonFileLocation, "cachingData.json");
            var json = File.ReadAllText(jsonFilePath);

            dynamic jsonObj = JsonConvert.DeserializeObject(json);

            // Update the key with the new value
            jsonObj["Caching"][key] = newValue;

            // Write the updated object back to Caching.json
            string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText(jsonFilePath, output);
        }
    }
}
