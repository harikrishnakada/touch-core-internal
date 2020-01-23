using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace touch_core_internal.Configuration
{
    public class InternalConfiguration : IInternalConfiguration
    {
        public JObject GetConfigurationJson()
        {
            var defaultLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var fiilePath = Path.Combine(defaultLocation, @"Configuration\Configuration.json");

            JObject configurationJson = new JObject();

            if (System.IO.File.Exists(fiilePath))
            {
                using (StreamReader r = new StreamReader(fiilePath))
                {
                    var jsonString = r.ReadToEnd();
                    configurationJson = JObject.Parse(jsonString);
                }
            }
            return configurationJson;

        }
    }
}
