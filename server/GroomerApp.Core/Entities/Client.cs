// <copyright file="Client.cs" company="Groomer App">
// Copyright (c) Groomer App. All rights reserved.
// </copyright>

namespace GroomerApp.Core.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a grooming client that owns pets that require grooming.
    /// </summary>
    public class Client : Entity
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the pets.
        /// </summary>
        /// <value>
        /// The pets.
        /// </value>
        public ICollection<Pet> Pets { get; set; }
    }
}
