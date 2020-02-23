// <copyright file="PetController.cs" company="Groomer App">
// Copyright (c) Groomer App. All rights reserved.
// </copyright>

namespace GroomerApp.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using GroomerApp.API.Requests;
    using GroomerApp.Core.Entities;
    using GroomerApp.Core.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Handles the requests made to the pet-related endpoints.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        /// <summary>
        /// The pet repository.
        /// </summary>
        private readonly IRepository<Pet> petRepository;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The mapper.
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PetController"/> class.
        /// </summary>
        /// <param name="petRepository">The pet repository.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        public PetController(
            IRepository<Pet> petRepository,
            ILogger<PetController> logger,
            IMapper mapper)
        {
            this.petRepository = petRepository ?? throw new ArgumentNullException(nameof(petRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets all pets.
        /// </summary>
        /// <returns>A collection of the pets.</returns>
        /// <response code="200">Returns the pets.</response>
        /// <response code="500">If there is was an error finding the pets.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Pet>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<Pet> pets = await this.petRepository.GetAll();
                return this.Ok(pets);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to get all pets.");
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets the pet by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The pet.
        /// </returns>
        /// <response code="200">Returns the pet with the given identifier.</response>
        /// <response code="404">If there is no pet with the given identifier.</response>
        /// <response code="500">If there is was an error finding the pet.</response>
        [HttpGet("{id}")]
        [ActionName("GetPet")]
        [ProducesResponseType(typeof(Pet), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                Pet pet = await this.petRepository.GetById(id);

                if (pet == null)
                {
                    return this.NotFound();
                }

                return this.Ok(pet);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to get the pet.");
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Updates the pet.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns>The pet that was updated.</returns>
        /// <response code="200">Returns the pet with that was updated.</response>
        /// <response code="404">If there is no pet with the given identifier.</response>
        /// <response code="500">If there is was an error finding the pet.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Pet), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePet(Guid id, [FromBody] CreateOrUpdatePetRequest request)
        {
            try
            {
                Pet existingPet = await this.petRepository.GetById(id);

                if (existingPet == null)
                {
                    this.logger.LogWarning($"Could not find the pet with id '{id}' to update.");
                    return this.NotFound();
                }

                existingPet.Name = request.Name;
                existingPet.Breed = request.Breed;
                existingPet.DateOfBirth = request.DateOfBirth;
                existingPet.Comments = request.Comments;

                Pet pet = await this.petRepository.Update(existingPet);
                return this.Ok(pet);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to update the pet.");
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Deletes the pet.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The pet that was deleted.</returns>
        /// <response code="200">Returns the pet that was deleted.</response>
        /// <response code="404">If there is no pet with the given identifier.</response>
        /// <response code="500">If there is was an error deleting the pet.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Pet), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePet(Guid id)
        {
            try
            {
                Pet existingPet = await this.petRepository.GetById(id);

                if (existingPet == null)
                {
                    this.logger.LogWarning($"Could not find the pet with id '{id}' to delete.");
                    return this.NotFound();
                }

                Pet pet = await this.petRepository.Remove(existingPet);
                return this.Ok(pet);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to delete the pet.");
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}