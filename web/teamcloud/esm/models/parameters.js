/*
 * Copyright (c) Microsoft Corporation.
 * Licensed under the MIT License.
 *
 * Code generated by Microsoft (R) AutoRest Code Generator.
 * Changes may cause incorrect behavior and will be lost if the code is regenerated.
 */
import { ProjectDefinition as ProjectDefinitionMapper, ComponentRequest as ComponentRequestMapper, ProjectLink as ProjectLinkMapper, ProviderData as ProviderDataMapper, ProjectType as ProjectTypeMapper, UserDefinition as UserDefinitionMapper, User as UserMapper, ComponentOffer as ComponentOfferMapper, Component as ComponentMapper, Provider as ProviderMapper, TeamCloudInstance as TeamCloudInstanceMapper } from "../models/mappers";
export var accept = {
    parameterPath: "accept",
    mapper: {
        defaultValue: "application/json",
        isConstant: true,
        serializedName: "Accept",
        type: {
            name: "String"
        }
    }
};
export var $host = {
    parameterPath: "$host",
    mapper: {
        serializedName: "$host",
        required: true,
        type: {
            name: "String"
        }
    },
    skipEncoding: true
};
export var contentType = {
    parameterPath: ["options", "contentType"],
    mapper: {
        defaultValue: "application/json",
        isConstant: true,
        serializedName: "Content-Type",
        type: {
            name: "String"
        }
    }
};
export var body = {
    parameterPath: ["options", "body"],
    mapper: ProjectDefinitionMapper
};
export var accept1 = {
    parameterPath: "accept",
    mapper: {
        defaultValue: "application/json",
        isConstant: true,
        serializedName: "Accept",
        type: {
            name: "String"
        }
    }
};
export var projectNameOrId = {
    parameterPath: "projectNameOrId",
    mapper: {
        serializedName: "projectNameOrId",
        required: true,
        type: {
            name: "String"
        }
    }
};
export var projectId = {
    parameterPath: "projectId",
    mapper: {
        serializedName: "projectId",
        required: true,
        type: {
            name: "String"
        }
    }
};
export var body1 = {
    parameterPath: ["options", "body"],
    mapper: ComponentRequestMapper
};
export var componentId = {
    parameterPath: "componentId",
    mapper: {
        serializedName: "componentId",
        required: true,
        type: {
            name: "String"
        }
    }
};
export var body2 = {
    parameterPath: ["options", "body"],
    mapper: ProjectLinkMapper
};
export var linkId = {
    parameterPath: "linkId",
    mapper: {
        serializedName: "linkId",
        required: true,
        type: {
            name: "String"
        }
    }
};
export var offerId = {
    parameterPath: "offerId",
    mapper: {
        serializedName: "offerId",
        required: true,
        type: {
            name: "String"
        }
    }
};
export var includeShared = {
    parameterPath: ["options", "includeShared"],
    mapper: {
        serializedName: "includeShared",
        type: {
            name: "Boolean"
        }
    }
};
export var providerId = {
    parameterPath: "providerId",
    mapper: {
        serializedName: "providerId",
        required: true,
        type: {
            name: "String"
        }
    }
};
export var body3 = {
    parameterPath: ["options", "body"],
    mapper: ProviderDataMapper
};
export var providerDataId = {
    parameterPath: "providerDataId",
    mapper: {
        serializedName: "providerDataId",
        required: true,
        type: {
            name: "String"
        }
    }
};
export var body4 = {
    parameterPath: ["options", "body"],
    mapper: {
        serializedName: "body",
        type: {
            name: "Dictionary",
            value: { type: { name: "String" } }
        }
    }
};
export var tagKey = {
    parameterPath: "tagKey",
    mapper: {
        serializedName: "tagKey",
        required: true,
        type: {
            name: "String"
        }
    }
};
export var body5 = {
    parameterPath: ["options", "body"],
    mapper: ProjectTypeMapper
};
export var projectTypeId = {
    parameterPath: "projectTypeId",
    mapper: {
        serializedName: "projectTypeId",
        required: true,
        type: {
            name: "String"
        }
    }
};
export var body6 = {
    parameterPath: ["options", "body"],
    mapper: UserDefinitionMapper
};
export var userNameOrId = {
    parameterPath: "userNameOrId",
    mapper: {
        serializedName: "userNameOrId",
        required: true,
        type: {
            name: "String"
        }
    }
};
export var body7 = {
    parameterPath: ["options", "body"],
    mapper: UserMapper
};
export var body8 = {
    parameterPath: ["options", "body"],
    mapper: ComponentOfferMapper
};
export var body9 = {
    parameterPath: ["options", "body"],
    mapper: ComponentMapper
};
export var body10 = {
    parameterPath: ["options", "body"],
    mapper: ProviderMapper
};
export var trackingId = {
    parameterPath: "trackingId",
    mapper: {
        serializedName: "trackingId",
        required: true,
        type: {
            name: "Uuid"
        }
    }
};
export var accept2 = {
    parameterPath: "accept",
    mapper: {
        defaultValue: "application/json, text/json",
        isConstant: true,
        serializedName: "Accept",
        type: {
            name: "String"
        }
    }
};
export var accept3 = {
    parameterPath: "accept",
    mapper: {
        defaultValue: "application/json, text/json",
        isConstant: true,
        serializedName: "Accept",
        type: {
            name: "String"
        }
    }
};
export var body11 = {
    parameterPath: ["options", "body"],
    mapper: TeamCloudInstanceMapper
};
export var userId = {
    parameterPath: "userId",
    mapper: {
        serializedName: "userId",
        required: true,
        type: {
            name: "String"
        }
    }
};
//# sourceMappingURL=parameters.js.map