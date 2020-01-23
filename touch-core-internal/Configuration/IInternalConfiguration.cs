using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace touch_core_internal.Configuration
{
   public interface IInternalConfiguration
    {
        JObject GetConfigurationJson();
    }
}
