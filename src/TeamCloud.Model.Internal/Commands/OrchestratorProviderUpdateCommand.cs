﻿/**
 *  Copyright (c) Microsoft Corporation.
 *  Licensed under the MIT License.
 */

using TeamCloud.Model.Data;

namespace TeamCloud.Model.Commands
{
    public class OrchestratorProviderUpdateCommand : OrchestratorUpdateCommand<ProviderDocument, OrchestratorProviderUpdateCommandResult>
    {
        public OrchestratorProviderUpdateCommand(UserDocument user, ProviderDocument payload) : base(user, payload) { }
    }
}
