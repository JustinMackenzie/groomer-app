// <copyright file="IRepository.cs" company="Groomer App">
// Copyright (c) Groomer App. All rights reserved.
// </copyright>

namespace GroomerApp.Core.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GroomerApp.Core.Entities;

    /// <summary>
    /// Represents a generic repository.
    /// </summary>
    /// <typeparam name="T">The type of the entity being stored.</typeparam>
    public interface IRepository<T>
        where T : Entity
    {
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The added item.</returns>
        Task<T> Add(T item);

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The updated item.</returns>
        Task<T> Update(T item);

        /// <summary>
        /// Gets the item by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The item with the given idenitifer.</returns>
        Task<T> GetById(Guid id);

        /// <summary>
        /// Gets all the items.
        /// </summary>
        /// <returns>All the items.</returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The removed item.</returns>
        Task<T> Remove(T item);
    }
}
