// <copyright file="ClientController.cs" company="Groomer App">
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
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Handles the requests made to the client-related endpoints.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        /// <summary>
        /// The client repository.
        /// </summary>
        private readonly IRepository<Client> clientRepository;

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
        /// Initializes a new instance of the <see cref="ClientController" /> class.
        /// </summary>
        /// <param name="clientRepository">The client repository.</param>
        /// <param name="petRepository">The pet repository.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        public ClientController(
            IRepository<Client> clientRepository,
            IRepository<Pet> petRepository,
            ILogger<ClientController> logger,
            IMapper mapper)
        {
            this.clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            this.petRepository = petRepository ?? throw new ArgumentNullException(nameof(petRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets all clients.
        /// </summary>
        /// <returns>A collection of the clients.</returns>
        /// <response code="200">Returns the clients.</response>
        /// <response code="500">If there is was an error finding the clients.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Client>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<Client> clients = await this.clientRepository.GetAll();
                return this.Ok(clients);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to get all clients.");
                return this.StatusCode(500);
            }
        }

        /// <summary>
        /// Gets the client by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The client.
        /// </returns>
        /// <response code="200">Returns the client with the given identifier.</response>
        /// <response code="404">If there is no client with the given identifier.</response>
        /// <response code="500">If there is was an error finding the client.</response>
        [HttpGet("{id}")]
        [ActionName("GetClient")]
        [ProducesResponseType(typeof(Client), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                Client client = await this.clientRepository.GetById(id);

                if (client == null)
                {
                    return this.NotFound();
                }

                return this.Ok(client);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to get the client.");
                return this.StatusCode(500);
            }
        }

        /// <summary>
        /// Creates the client.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The client that was created.</returns>
        /// <response code="201">Returns the client that was created.</response>
        /// <response code="500">If there is was an error creating the client.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Client), 201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateClient([FromBody] CreateOrUpdateClientRequest request)
        {
            try
            {
                Client client = await this.clientRepository.Add(this.mapper.Map<Client>(request));
                return this.CreatedAtAction("GetClient", new { id = client.Id }, client);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to create the client.");
                return this.StatusCode(500);
            }
        }

        /// <summary>
        /// Updates the client.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns>The client that was updated.</returns>
        /// <response code="200">Returns the client with that was updated.</response>
        /// <response code="404">If there is no client with the given identifier.</response>
        /// <response code="500">If there is was an error finding the client.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Client), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateClient(Guid id, [FromBody] CreateOrUpdateClientRequest request)
        {
            try
            {
                Client existingClient = await this.clientRepository.GetById(id);

                if (existingClient == null)
                {
                    this.logger.LogWarning($"Could not find the client with id '{id}' to update.");
                    return this.NotFound();
                }

                existingClient.Email = request.Email;
                existingClient.FirstName = request.FirstName;
                existingClient.LastName = request.LastName;
                existingClient.Phone = request.Phone;

                Client client = await this.clientRepository.Update(existingClient);
                return this.Ok(client);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to update the client.");
                return this.StatusCode(500);
            }
        }

        /// <summary>
        /// Deletes the client.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The client that was deleted.</returns>
        /// <response code="200">Returns the client that was deleted.</response>
        /// <response code="404">If there is no client with the given identifier.</response>
        /// <response code="500">If there is was an error deleting the client.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Client), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            try
            {
                Client existingClient = await this.clientRepository.GetById(id);

                if (existingClient == null)
                {
                    this.logger.LogWarning($"Could not find the client with id '{id}' to delete.");
                    return this.NotFound();
                }

                Client client = await this.clientRepository.Remove(existingClient);
                return this.Ok(client);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to delete the client.");
                return this.StatusCode(500);
            }
        }

        /// <summary>
        /// Creates the pet for a given owner.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns>The pet that was created.</returns>
        [HttpPost("{id}/pet")]
        [ProducesResponseType(typeof(Client), 201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreatePetForOwner(Guid id, [FromBody] CreateOrUpdatePetRequest request)
        {
            try
            {
                Pet newPet = this.mapper.Map<Pet>(request);
                newPet.OwnerId = id;

                Pet pet = await this.petRepository.Add(newPet);
                return this.CreatedAtAction("GetPet", "Pet", new { id = pet.Id }, pet);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to create the pet.");
                return this.StatusCode(500);
            }
        }
    }
}