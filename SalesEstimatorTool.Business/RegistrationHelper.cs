using Autofac;
using SalesEstimatorTool.Data.Access.Services;
using SalesEstimatorTool.Data.Contracts;

namespace SalesEstimatorTool.Business
{
    internal static class RegistrationHelper
    {
        public static IContainer CreateDependenciesContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ParsingService>().As<IParsingService>();
            builder.RegisterType<ExcelService>().As<IExcelService>();
            builder.RegisterType<SettingsService>().As<ISettingsService>();
            builder.RegisterType<EstimatorService>().As<IEstimatorService>();
            builder.RegisterType<EmailExchangeService>().As<IEmailExchangeService>();
            builder.RegisterType<FileService>().As<IFileService>();
            builder.RegisterType<WordService>().As<IWordService>();
            builder.RegisterType<PowerPointService>().As<IPowerPointService>();
            builder.RegisterType<ImageService>().As<IImageService>();
            builder.RegisterType<ZipService>().As<IZipService>();

            var container = builder.Build();

            return container;
        }
    }
}
