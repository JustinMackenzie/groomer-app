// <copyright file="Repository.cs" company="Groomer App">
// Copyright (c) Groomer App. All rights reserved.
// </copyright>

namespace GroomerApp.Infrastructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GroomerApp.Core.Entities;
    using GroomerApp.Core.Interfaces;
    using GroomerApp.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Represents the implementation of a generic repository that stores entities.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <seealso cref="GroomerApp.Core.Interfaces.IRepository{T}" />
    public class Repository<T> : IRepository<T>
        where T : Entity
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly GroomerContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(GroomerContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The added item.
        /// </returns>
        public async Task<T> Add(T item)
        {
            this.context.Set<T>().Add(item);
            await this.context.SaveChangesAsync();
            return item;
        }

        /// <summary>
        /// Gets all the items.
        /// </summary>
        /// <returns>
        /// All the items.
        /// </returns>
        public async Task<IEnumerable<T>> GetAll()
        {
            List<T> items = await this.context.Set<T>().ToListAsync();
            return items;
        }

        /// <summary>
        /// Gets the item by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The item with the given idenitifer.
        /// </returns>
        public Task<T> GetById(Guid id)
        {
            return Task.FromResult(this.context.Set<T>().Find(id));
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The removed item.
        /// </returns>
        public async Task<T> Remove(T item)
        {
            this.context.Set<T>().Remove(item);
            await this.context.SaveChangesAsync();
            return item;
        }

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The updated item.
        /// </returns>
        public async Task<T> Update(T item)
        {
            this.context.Set<T>().Update(item);
            await this.context.SaveChangesAsync();
            return item;
        }
    }
}
