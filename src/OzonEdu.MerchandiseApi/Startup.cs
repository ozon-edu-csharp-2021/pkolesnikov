using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTracing.Contrib.NetCore.Configuration;
using OzonEdu.MerchandiseApi.GrpcServices;
using OzonEdu.MerchandiseApi.HostedServices;
using OzonEdu.MerchandiseApi.Infrastructure.Configuration;
using OzonEdu.MerchandiseApi.Infrastructure.Extensions;
using OzonEdu.MerchandiseApi.Infrastructure.Filters;
using OzonEdu.MerchandiseApi.Infrastructure.Interceptors;

namespace OzonEdu.MerchandiseApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<KafkaConfiguration>(Configuration.GetSection(nameof(KafkaConfiguration)))
                .AddHostedService<StockReplenishedHostedService>()
                .AddMediatR(typeof(Startup), typeof(DatabaseConnectionOptions))
                .AddMediatorHandlers()
                .AddDatabaseComponents(Configuration)
                .AddRepositories()
                .AddDomainServices()
                .AddOpenTracing()
                .AddJaegerTracer()
                .Configure<HttpHandlerDiagnosticOptions>(options =>
                {
                    options.OperationNameResolver =
                        request => $"{request.Method.Method}: {request.RequestUri?.AbsoluteUri}";
                })
                .AddStockApiGrpcClient(Configuration)
                .AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());

            services.AddGrpc(options =>
            {
                options.Interceptors.Add<LoggingInterceptor>();
                options.Interceptors.Add<ExceptionInterceptor>();
            });
        }

        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment env)
        {
            applicationBuilder
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapGrpcService<MerchandiseApiGrpcService>();
                    endpoints.MapControllers();
                });
        }
    }
}