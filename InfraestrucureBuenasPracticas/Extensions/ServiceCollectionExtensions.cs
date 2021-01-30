using CoreBuenasPracticas.CustomEntities;
using CoreBuenasPracticas.Interfaces;
using CoreBuenasPracticas.Services;
using InfraestructureBuenasPracticas.Data;
using InfraestructureBuenasPracticas.Interfaces;
using InfraestructureBuenasPracticas.Options;
using InfraestructureBuenasPracticas.Repositories;
using InfraestructureBuenasPracticas.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace InfraestructureBuenasPracticas.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {

            //Conexion con bd entity framework dbcontext
            services.AddDbContext<SocialMediaContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SocialMedia"))
            );
            return services;
        }

        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {

            //Conexion con bd entity framework dbcontext
            services.Configure<PaginationOptions>(options => configuration.GetSection("Pagination").Bind(options)); //leer la seccion 
            //paginacion y mapearlacontra clase, el configure crea un singleton automaticamente para ser inyectado 
            //en la solucion (IOptions)
            services.Configure<PasswordOptions>(options => configuration.GetSection("PasswordOptions").Bind(options));
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddSingleton<IUriService>(provider =>
            {
                //se hace para tomar la url de request desde donde esté publicado el api, con esto se forma la url
                //para la paginacion , ej paginaSiguiente:http://dominio.com/api/post?paginicial=1&PageSize=10
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUri); //tarea: tratar de hacer esto para la clase
                //conexion con ADO.nET
            });

            return services;
        }


        public static IServiceCollection AddSwagger(this IServiceCollection services , string xmlFileName)
        {

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Social Media API", Version = "V1" });
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                options.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
