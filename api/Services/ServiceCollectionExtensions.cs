using Autofac;
using BBWM.AutofacExtensions;
using BBWT.Services.Classes;
using BBWT.Services.Interfaces;
using Services.Interfaces.Interfaces;

namespace BBWT.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterProjectServices(this ContainerBuilder builder)
        {
            // Register you project services, placed into BBWT.Services root, here.
            // For example: builder.RegisterService<ISampleBusinessService, SampleBusinessService>();

            builder.RegisterService<IUserService, ProjectUserService>();

          

        }
    }
}