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


class ProjectUser(Model):
    """ProjectUser.

    :param id:
    :type id: str
    :param role: Possible values include: 'None', 'Member', 'Owner'
    :type role: str or ~teamcloud.models.enum
    :param properties:
    :type properties: dict[str, str]
    """

    _attribute_map = {
        'id': {'key': 'id', 'type': 'str'},
        'role': {'key': 'role', 'type': 'str'},
        'properties': {'key': 'properties', 'type': '{str}'},
    }

    def __init__(self, *, id: str=None, role=None, properties=None, **kwargs) -> None:
        super(ProjectUser, self).__init__(**kwargs)
        self.id = id
        self.role = role
        self.properties = properties
