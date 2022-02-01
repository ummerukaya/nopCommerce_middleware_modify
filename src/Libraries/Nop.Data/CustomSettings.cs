using System.Collections.Generic;
using FluentMigrator.Runner.Initialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nop.Data
{
    public partial class CustomSettings
    {
        [JsonProperty(PropertyName = "CheckingString")]
        public string CheckingString { get; set; }

    }
   
}
