
using CRM_Core.Application.Interfaces;
using CRM_Core.Application.Services;
using CRM_Core.DataAccessLayer.Repositories;
using CRM_Core.Entities.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            //CleanArchitecture.Application

            //CleanArchitecture.Domain.Interfaces | CleanArchitecture.Infra.Data.Repositories
            // services.AddScoped<IPeopleRepository, PeopleRepository>();
            // services.AddScoped<IPeopleService, PeopleService>();
             services.AddScoped<IPeopleService, Ef_PeopleService>();
             services.AddScoped<IUserService, Ef_UserService>();
             services.AddScoped<IGraduationService, Ef_GraduaionService>();
             services.AddScoped<IPrefixService, Ef_PrefixService>();
             services.AddScoped<IIntroductionTypeService, Ef_IntroductionTypeService>();
             services.AddScoped<IPotentialService, Ef_PotentialService>();
             services.AddScoped<ICategoryService, Ef_CategoryService>();
             services.AddScoped<IAddressService, Ef_AddressService>();
             services.AddScoped<IPeopleVirtualService, Ef_PeopleVirtualService>();
             services.AddScoped<IPeoplePropertyService, Ef_PeoplePropertyService>();

            return services;

        }
    }
}
