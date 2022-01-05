using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using TransactionApplication.Api.Mapping;
using TransactionApplication.Api.Middleware;
using TransactionApplication.DataAccess.Contexts;
using TransactionApplication.DataAccess.Entitties;
using TransactionApplication.Domain.Handlers;
using TransactionApplication.Domain.Validators;
using TransactionApplication.Infrastructure.Constants;

namespace TransactionApplication.Api.Extensions
{
    public static class ApplicationExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddMediatR(typeof(MakeWithdrawalHandler));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddAutoMapper(typeof(ContractDomainMappingProfile));
            services.AddDbContext<DbContext, TransactionApplicationDbContext>(
                opt => opt.UseInMemoryDatabase(Constants.DatabaseName));

            services.AddValidatorsFromAssembly(typeof(MakeWithdrawalValidator).Assembly);

            services.AddTransient<ExceptionHandlingMiddleware>();

            services.AddSwaggerGen();
        }

        public static void InitializeData(this IApplicationBuilder builder)
        {
            var scope = builder.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DbContext>();
            context.Set<Player>().Add(new Player { Id = Guid.NewGuid(), Balance = 10 });
            context.SaveChanges();
        }
    }
}
