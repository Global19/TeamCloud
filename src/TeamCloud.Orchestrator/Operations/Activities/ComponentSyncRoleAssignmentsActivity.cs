﻿/**
 *  Copyright (c) Microsoft Corporation.
 *  Licensed under the MIT License.
 */

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using TeamCloud.Azure.Resources;
using TeamCloud.Data;
using TeamCloud.Model.Data;
using TeamCloud.Serialization;

namespace TeamCloud.Orchestrator.Operations.Activities
{
    public sealed class ComponentSyncRoleAssignmentsActivity
    {
        private readonly IComponentRepository componentRepository;
        private readonly IUserRepository userRepository;
        private readonly IAzureResourceService azureResourceService;

        public ComponentSyncRoleAssignmentsActivity(IComponentRepository componentRepository, IUserRepository userRepository, IAzureResourceService azureResourceService)
        {
            this.componentRepository = componentRepository ?? throw new ArgumentNullException(nameof(componentRepository));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.azureResourceService = azureResourceService ?? throw new ArgumentNullException(nameof(azureResourceService));
        }

        [FunctionName(nameof(ComponentSyncRoleAssignmentsActivity))]
        public async Task Run(
            [ActivityTrigger] IDurableActivityContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            var component = context.GetInput<Input>().Component;

            try
            {
                var task = component.Type switch
                {
                    ComponentType.Environment => HandleEnvironmentAsync(component),
                    _ => Task.CompletedTask
                };

                await task.ConfigureAwait(false);
            }
            catch (Exception exc)
            {
                throw exc.AsSerializable();
            }
        }

        private async Task HandleEnvironmentAsync(Component component)
        {
            if (AzureResourceIdentifier.TryParse(component.ResourceId, out var componentResourceId))
            {
                var roleDefinitionId = AzureRoleDefinition.Contributor;

                var roleAssignmentMap = await userRepository
                    .ListAsync(component.Organization, component.ProjectId)
                    .ToDictionaryAsync(user => user.Id, user => Enumerable.Repeat(roleDefinitionId, 1))
                    .ConfigureAwait(false);

                if (string.IsNullOrEmpty(componentResourceId.ResourceGroup))
                {
                    var subscription = await azureResourceService
                        .GetSubscriptionAsync(componentResourceId.SubscriptionId, throwIfNotExists: true)
                        .ConfigureAwait(false);

                    await subscription
                        .SetRoleAssignmentsAsync(roleAssignmentMap)
                        .ConfigureAwait(false);
                }
                else
                {

                    var resourceGroup = await azureResourceService
                        .GetResourceGroupAsync(componentResourceId.SubscriptionId, componentResourceId.ResourceGroup, throwIfNotExists: true)
                        .ConfigureAwait(false);

                    await resourceGroup
                        .SetRoleAssignmentsAsync(roleAssignmentMap)
                        .ConfigureAwait(false);
                }
            }
        }

        internal struct Input
        {
            public Component Component { get; set; }
        }
    }
}
