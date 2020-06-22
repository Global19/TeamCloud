# coding=utf-8
# --------------------------------------------------------------------------
# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License. See License.txt in the project root for
# license information.
#
# Code generated by Microsoft (R) AutoRest Code Generator.
# Changes may cause incorrect behavior and will be lost if the code is
# regenerated.
# --------------------------------------------------------------------------

from msrest.serialization import Model


class ProjectType(Model):
    """ProjectType.

    :param id:
    :type id: str
    :param default:
    :type default: bool
    :param region:
    :type region: str
    :param subscriptions:
    :type subscriptions: list[str]
    :param subscription_capacity:
    :type subscription_capacity: int
    :param resource_group_name_prefix:
    :type resource_group_name_prefix: str
    :param providers:
    :type providers: list[~teamcloud.models.ProviderReference]
    :param tags:
    :type tags: dict[str, str]
    :param properties:
    :type properties: dict[str, str]
    """

    _attribute_map = {
        'id': {'key': 'id', 'type': 'str'},
        'default': {'key': 'default', 'type': 'bool'},
        'region': {'key': 'region', 'type': 'str'},
        'subscriptions': {'key': 'subscriptions', 'type': '[str]'},
        'subscription_capacity': {'key': 'subscriptionCapacity', 'type': 'int'},
        'resource_group_name_prefix': {'key': 'resourceGroupNamePrefix', 'type': 'str'},
        'providers': {'key': 'providers', 'type': '[ProviderReference]'},
        'tags': {'key': 'tags', 'type': '{str}'},
        'properties': {'key': 'properties', 'type': '{str}'},
    }

    def __init__(self, **kwargs):
        super(ProjectType, self).__init__(**kwargs)
        self.id = kwargs.get('id', None)
        self.default = kwargs.get('default', None)
        self.region = kwargs.get('region', None)
        self.subscriptions = kwargs.get('subscriptions', None)
        self.subscription_capacity = kwargs.get('subscription_capacity', None)
        self.resource_group_name_prefix = kwargs.get('resource_group_name_prefix', None)
        self.providers = kwargs.get('providers', None)
        self.tags = kwargs.get('tags', None)
        self.properties = kwargs.get('properties', None)
