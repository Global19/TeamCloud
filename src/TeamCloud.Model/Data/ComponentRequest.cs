/**
 *  Copyright (c) Microsoft Corporation.
 *  Licensed under the MIT License.
 */

using Newtonsoft.Json;
using TeamCloud.Model.Common;
using TeamCloud.Serialization;

namespace TeamCloud.Model.Data
{
    [JsonObject(NamingStrategyType = typeof(TeamCloudNamingStrategy))]
    public sealed class ComponentRequest : IValidatable
    {
        [JsonProperty(Required = Required.Always)]
        public string TemplateId { get; set; }

        public string InputJson { get; set; }
    }
}
