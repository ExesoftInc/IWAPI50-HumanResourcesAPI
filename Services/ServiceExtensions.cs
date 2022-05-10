using Microsoft.Extensions.DependencyInjection;

namespace HumanResourcesAPI.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddScoped<IDepartmentBuilder, DepartmentBuilder>();
            services.AddScoped<IEmployeeBuilder, EmployeeBuilder>();
            services.AddScoped<IEmployeeDepartmentHistoryBuilder, EmployeeDepartmentHistoryBuilder>();
            services.AddScoped<IEmployeePayHistoryBuilder, EmployeePayHistoryBuilder>();
            services.AddScoped<IJobCandidateBuilder, JobCandidateBuilder>();
            services.AddScoped<IShiftBuilder, ShiftBuilder>();
        }
    }
}
