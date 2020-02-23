// <copyright file="CreateOrUpdatePetRequest.cs" company="Groomer App">
// Copyright (c) Groomer App. All rights reserved.
// </copyright>

namespace GroomerApp.API.Requests
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents the data for a request to create a new pet or update an existing one.
    /// </summary>
    public class CreateOrUpdatePetRequest
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        [Required]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the breed.
        /// </summary>
        /// <value>
        /// The breed.
        /// </value>
        [Required]
        public string Breed { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public string Comments { get; set; }
    }
}
