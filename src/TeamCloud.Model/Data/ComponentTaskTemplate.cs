/**
 *  Copyright (c) Microsoft Corporation.
 *  Licensed under the MIT License.
 */

using System;
using Newtonsoft.Json;
using TeamCloud.Model.Common;
using TeamCloud.Serialization;

namespace TeamCloud.Model.Data
{
    [JsonObject(NamingStrategyType = typeof(TeamCloudNamingStrategy))]
    public sealed class ComponentTaskTemplate : IValidatable
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string InputJsonSchema { get; set; }

        [JsonProperty(Required = Required.Always)]
        public ComponentTaskType Type { get; set; }

        public string TypeName
            => Type == ComponentTaskType.Custom ? Id : default;

        public bool Equals(ComponentTaskTemplate other)
            => Id.Equals(other?.Id, StringComparison.Ordinal);

        public override bool Equals(object obj)
            => base.Equals(obj) || Equals(obj as ComponentTaskTemplate);

        public override int GetHashCode()
            => Id?.GetHashCode(StringComparison.Ordinal) ?? base.GetHashCode();
    }
}