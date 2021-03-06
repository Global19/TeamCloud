/**
 *  Copyright (c) Microsoft Corporation.
 *  Licensed under the MIT License.
 */

using TeamCloud.Model.Data;

namespace TeamCloud.Model.Commands
{
    public sealed class OrchestratorProjectComponentCreateCommand : OrchestratorCreateCommand<ComponentDocument, OrchestratorProjectComponentCreateCommandResult, ProviderComponentCreateCommand, Component>
    {
        public OrchestratorProjectComponentCreateCommand(UserDocument user, ComponentDocument payload, string projectId) : base(user, payload)
            => ProjectId = projectId;
    }
}
