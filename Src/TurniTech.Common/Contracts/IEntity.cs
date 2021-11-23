using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TurniTech.Common.Contracts
{
    public interface IEntity
    {
        [JsonProperty("id")]
        string Id { get; set; }
    }
}
