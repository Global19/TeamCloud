/**
 *  Copyright (c) Microsoft Corporation.
 *  Licensed under the MIT License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TeamCloud.API.Data.Results;
using TeamCloud.API.Services;
using TeamCloud.Data;
using TeamCloud.Model.Data;
using TeamCloud.Model.Validation.Data;

namespace TeamCloud.API.Controllers
{
    [ApiController]
    [Route("api/projectTypes")]
    [Produces("application/json")]
    public class ProjectTypesController : ControllerBase
    {
        readonly Orchestrator orchestrator;
        readonly IProvidersRepository providersRepository;
        readonly IProjectTypesRepository projectTypesRepository;

        public ProjectTypesController(Orchestrator orchestrator, IProvidersRepository providersRepository, IProjectTypesRepository projectTypesRepository)
        {
            this.orchestrator = orchestrator ?? throw new ArgumentNullException(nameof(orchestrator));
            this.providersRepository = providersRepository ?? throw new ArgumentNullException(nameof(providersRepository));
            this.projectTypesRepository = projectTypesRepository ?? throw new ArgumentNullException(nameof(projectTypesRepository));
        }


        [HttpGet]
        [SwaggerOperation(OperationId = "GetProjectTypes", Summary = "Gets all Project Types.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns all ProjectTypes.", typeof(DataResult<List<ProjectType>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "A validation error occured.", typeof(ErrorResult))]
        public async Task<IActionResult> Get()
        {
            var projectTypes = await projectTypesRepository
                .ListAsync()
                .ToListAsync()
                .ConfigureAwait(false);

            return DataResult<List<ProjectType>>
                .Ok(projectTypes)
                .ActionResult();
        }


        [HttpGet("{projectTypeId}")]
        [Authorize(Policy = "admin")]
        [SwaggerOperation(OperationId = "GetProjectTypeById", Summary = "Gets a Project Type by ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns a ProjectType.", typeof(DataResult<ProjectType>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "A validation error occured.", typeof(ErrorResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A ProjectType with the projectTypeId provided was not found.", typeof(ErrorResult))]
        public async Task<IActionResult> Get(string projectTypeId)
        {
            var projectType = await projectTypesRepository
                .GetAsync(projectTypeId)
                .ConfigureAwait(false);

            if (projectType is null)
                return ErrorResult
                    .NotFound($"A ProjectType with the ID '{projectTypeId}' could not be found in this TeamCloud Instance")
                    .ActionResult();

            return DataResult<ProjectType>
                .Ok(projectType)
                .ActionResult();
        }


        [HttpPost]
        [Authorize(Policy = "admin")]
        [Consumes("application/json")]
        [SwaggerOperation(OperationId = "CreateProjectType", Summary = "Creates a new Project Type.")]
        [SwaggerResponse(StatusCodes.Status201Created, "The new ProjectType was created.", typeof(DataResult<ProjectType>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "A validation error occured.", typeof(ErrorResult))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "A ProjectType already exists with the ID provided in the request body.", typeof(ErrorResult))]
        public async Task<IActionResult> Post([FromBody] ProjectType projectType)
        {
            if (projectType is null)
                throw new ArgumentNullException(nameof(projectType));

            var validation = new ProjectTypeValidator().Validate(projectType);

            if (!validation.IsValid)
                return ErrorResult
                    .BadRequest(validation)
                    .ActionResult();

            var existingProjectType = await projectTypesRepository
                .GetAsync(projectType.Id)
                .ConfigureAwait(false);

            if (existingProjectType != null)
                return ErrorResult
                    .Conflict($"A ProjectType with id '{projectType.Id}' already exists.  Please try your request again with a unique id or call PUT to update the existing ProjectType.")
                    .ActionResult();

            var providers = await providersRepository
                .ListAsync()
                .ToListAsync()
                .ConfigureAwait(false);

            var validProviders = projectType.Providers
                .All(p => providers.Any(provider => provider.Id == p.Id));

            if (!validProviders)
            {
                var validProviderIds = string.Join(", ", providers.Select(p => p.Id));
                return ErrorResult
                    .BadRequest(new ValidationError { Field = "projectType", Message = $"All provider ids on a ProjectType must match the id of a registered Provider on the TeamCloud instance. Valid provider ids are: {validProviderIds}" })
                    .ActionResult();
            }

            var addResult = await orchestrator
                .AddAsync(projectType)
                .ConfigureAwait(false);

            var baseUrl = HttpContext.GetApplicationBaseUrl();
            var location = new Uri(baseUrl, $"api/projectTypes/{addResult.Id}").ToString();

            return DataResult<ProjectType>
                .Created(addResult, location)
                .ActionResult();
        }


        [HttpPut]
        [Authorize(Policy = "admin")]
        [Consumes("application/json")]
        [SwaggerOperation(OperationId = "UpdateProjectType", Summary = "Updates an existing Project Type.")]
        [SwaggerResponse(StatusCodes.Status200OK, "The ProjectType was updated.", typeof(DataResult<ProjectType>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "A validation error occured.", typeof(ErrorResult))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "A Project Type already exists with the ID provided in the reques body.", typeof(ErrorResult))]
        public async Task<IActionResult> Put([FromBody] ProjectType projectType)
        {
            if (projectType is null)
                throw new ArgumentNullException(nameof(projectType));

            var validation = new ProjectTypeValidator().Validate(projectType);

            if (!validation.IsValid)
                return ErrorResult
                    .BadRequest(validation)
                    .ActionResult();

            var existingProjectType = await projectTypesRepository
                .GetAsync(projectType.Id)
                .ConfigureAwait(false);

            if (existingProjectType is null)
                return ErrorResult
                    .NotFound($"A ProjectType with the ID '{projectType.Id}' could not be found in this TeamCloud Instance")
                    .ActionResult();

            var providers = await providersRepository
                .ListAsync()
                .ToListAsync()
                .ConfigureAwait(false);

            var validProviders = projectType.Providers
                .All(p => providers.Any(provider => provider.Id == p.Id));

            if (!validProviders)
            {
                var validProviderIds = string.Join(", ", providers.Select(p => p.Id));
                return ErrorResult
                    .BadRequest(new ValidationError { Field = "projectType", Message = $"All provider ids on a ProjectType must match the id of a registered Provider on the TeamCloud instance. Valid provider ids are: {validProviderIds}" })
                    .ActionResult();
            }

            var updateResult = await orchestrator
                .UpdateAsync(projectType)
                .ConfigureAwait(false);

            return DataResult<ProjectType>
                .Ok(updateResult)
                .ActionResult();
        }


        [HttpDelete("{projectTypeId}")]
        [Authorize(Policy = "admin")]
        [SwaggerOperation(OperationId = "DeleteProjectType", Summary = "Deletes a Project Type.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The ProjectType was deleted.", typeof(DataResult<ProjectType>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "A validation error occured.", typeof(ErrorResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A ProjectType with the projectTypeId provided was not found.", typeof(ErrorResult))]
        public async Task<IActionResult> Delete(string projectTypeId)
        {
            var existingProjectType = await projectTypesRepository
                .GetAsync(projectTypeId)
                .ConfigureAwait(false);

            if (existingProjectType is null)
                return ErrorResult
                    .NotFound($"A ProjectType with the ID '{projectTypeId}' could not be found in this TeamCloud Instance")
                    .ActionResult();

            _ = await orchestrator
                .DeleteAsync(projectTypeId)
                .ConfigureAwait(false);

            return DataResult<ProjectType>
                .NoContent()
                .ActionResult();
        }
    }
}
