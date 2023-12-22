using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Yummi.Application.CQRS.Ingredient.Cmmnd.Hndlr;
using Yummi.Application.CQRS.Ingredient.Qry.Hndlr;
using Yummi.Application.CQRS.Recipe.Cmmnd.hndlr;
using Yummi.Application.CQRS.Recipe.Qry.Hndlr;
using Yummi.Application.Mappers;
using Yummi.Core.Interfaces;
using Yummi.Core.Interfaces.Generic;
using Yummi.Infrastructure.Uow;
using Yummi.Persistance.DataContext;
using Yummi.Persistance.Repositories;
using Yummi.Persistance.Validators;

namespace Yummi.WebAPI.Extensions
{
    public static class ApplicationExtencions
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            #region MediatR Configuration

            // Add MediatR to the collection service.
            service.AddMediatR(config =>
              config.RegisterServicesFromAssemblies(typeof(AddRecipeCmmndHndlr).Assembly));
            // Add MediatR to the collection service.
            service.AddMediatR(config =>
              config.RegisterServicesFromAssemblies(typeof(GetAllRecipeQryHndlr).Assembly));
            // Add MediatR to the collection service.
            service.AddMediatR(config =>
              config.RegisterServicesFromAssemblies(typeof(GetByIdRecipeQryHndlr).Assembly));
            // Add MediatR to the collection service.
            service.AddMediatR(config =>
              config.RegisterServicesFromAssemblies(typeof(AddIngredientCmmndHndlr).Assembly));
            // Add MediatR to the collection service.
            service.AddMediatR(config =>
              config.RegisterServicesFromAssemblies(typeof(GettAllIngredientQryHndlr).Assembly));
            
            #endregion
            // Add Scoped IRepository to the collection service.
            service.AddScoped(typeof(IRepository<>),
                typeof(GenericRepository<>));
            // Add UnitOfWork to the container
            service.AddTransient<IUnitOfWork,
                UnitOfWork>();
            // Add AutoMapper to the container
            //service.AddAutoMapper(config =>
            //    config.AddMaps(Assembly.GetExecutingAssembly()));
            service.AddAutoMapper(typeof(RecipeMap));
            // Add FluentValidationAspNetCore to the colletion service
            service.AddControllers()
            .AddFluentValidation(c =>
            c.RegisterValidatorsFromAssemblyContaining<RecipeVldtr>());

            return service;
        }

        public static void AddDbContext(this IServiceCollection services,
           IConfiguration config)
        {
            // Add DbContext to the collection service.
            services.AddDbContext<YummiDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("Default"))
             );
        }
    }
}
