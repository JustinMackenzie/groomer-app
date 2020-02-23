// <copyright file="Pet.cs" company="Groomer App">
// Copyright (c) Groomer App. All rights reserved.
// </copyright>

namespace GroomerApp.Core.Entities
{
    using System;

    /// <summary>
    /// Represents a pet that requires grooming.
    /// </summary>
    /// <seealso cref="GroomerApp.Core.Entities.Entity" />
    public class Pet : Entity
    {
        /// <summary>
        /// Gets or sets the owner identifier.
        /// </summary>
        /// <value>
        /// The owner identifier.
        /// </value>
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public Client Owner { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the breed.
        /// </summary>
        /// <value>
        /// The breed.
        /// </value>
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
