// <copyright file="GroomerContext.cs" company="Groomer App">
// Copyright (c) Groomer App. All rights reserved.
// </copyright>

namespace GroomerApp.Infrastructure.Data
{
    using GroomerApp.Core.Entities;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The database context for the groomer application.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class GroomerContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroomerContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public GroomerContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the clients.
        /// </summary>
        /// <value>
        /// The clients.
        /// </value>
        public DbSet<Client> Clients { get; set; }

        /// <summary>
        /// Gets or sets the pets.
        /// </summary>
        /// <value>
        /// The pets.
        /// </value>
        public DbSet<Pet> Pets { get; set; }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Client configuration.
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Client>().Property(c => c.Email).IsRequired();
            modelBuilder.Entity<Client>().Property(c => c.FirstName).IsRequired();
            modelBuilder.Entity<Client>().Property(c => c.LastName).IsRequired();
            modelBuilder.Entity<Client>().Property(c => c.Phone).IsRequired();

            // Pet configuration.
            modelBuilder.Entity<Pet>().ToTable("Pet");
            modelBuilder.Entity<Pet>().Property(c => c.Name).IsRequired();
            modelBuilder.Entity<Pet>().Property(c => c.DateOfBirth).IsRequired();
            modelBuilder.Entity<Pet>().Property(c => c.Breed).IsRequired();
            modelBuilder.Entity<Pet>().Property(c => c.OwnerId).IsRequired();
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Owner)
                .WithMany(c => c.Pets);
        }
    }
}
