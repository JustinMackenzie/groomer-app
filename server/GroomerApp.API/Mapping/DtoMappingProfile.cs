// <copyright file="DtoMappingProfile.cs" company="Groomer App">
// Copyright (c) Groomer App. All rights reserved.
// </copyright>

namespace GroomerApp.API.Mapping
{
    using AutoMapper;
    using GroomerApp.API.Requests;
    using GroomerApp.Core.Entities;

    /// <summary>
    /// Declares the mapping profile for data transfer objects.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class DtoMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DtoMappingProfile"/> class.
        /// </summary>
        public DtoMappingProfile()
        {
            this.CreateMap<CreateOrUpdateClientRequest, Client>();
        }
    }
}
