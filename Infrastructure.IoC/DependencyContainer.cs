
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
             services.AddScoped<IGraduationService, Ef_GraduaionService>();
             services.AddScoped<IPrefixService, Ef_PrefixService>();
             services.AddScoped<IIntroductionTypeService, Ef_IntroductionTypeService>();
             services.AddScoped<IPotentialService, Ef_PotentialService>();
             services.AddScoped<ICategoryService, Ef_CategoryService>();
             services.AddScoped<IAddressService, Ef_AddressService>();
             services.AddScoped<IPeopleVirtualService, Ef_PeopleVirtualService>();
             services.AddScoped<IPeoplePropertyService, Ef_PeoplePropertyService>();
             services.AddScoped<IApplicationUserService, Ef_ApplicationUserService>();
             services.AddScoped<IGeneratedNumberService, Ef_GeneralService>();
             services.AddScoped<IMenuService, Ef_MenuService>();
             services.AddScoped<IUserMenuService, Ef_UserMenuService>();
             services.AddScoped<IClerkServiceService, Ef_ClerkServiceService>();
             services.AddScoped<ITBASServiceService, Ef_TBASServicesService>();
             services.AddScoped<IPeopleServiceService, Ef_PeopleServiceService>();
             services.AddScoped<IReservationService, Ef_ReservationService>();
             services.AddScoped<ITBASPayTypeService, Ef_TBASPayTypeService>();
             services.AddScoped<ISalonInfoService, Ef_SalonInfoService>();
             services.AddScoped<ISalonCostsService, Ef_SalonCostsService>();
             services.AddScoped<ITransferCostsService, Ef_TransferCostsService>();
             services.AddScoped<IBillCostsService, Ef_BillCostsService>();
             services.AddScoped<ITBASSalonCostsService, Ef_TBASSalonCostsService>();
             //services.AddTransient<IMyRepository, MyRepository>();

            return services;

        }
    }
}
