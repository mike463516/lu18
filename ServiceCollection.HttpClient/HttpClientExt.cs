using Microsoft.Extensions.DependencyInjection;
using VideoHub.ServiceCollectionEx.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace VideoHub.ServiceCollectionEx
{
    public static class VideoHubServiceCollectionExtensions
    {
        public static IServiceCollection AddVideoHubHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient("User").AddTypedClient(c => Refit.RestService.For<IUserService>(c));
            services.AddHttpClient("Video").AddTypedClient(c => Refit.RestService.For<IVideoService>(c));
            services.AddHttpClient("Category").AddTypedClient(c => Refit.RestService.For<ICategoryService>(c));
            services.AddHttpClient("Tag").AddTypedClient(c => Refit.RestService.For<ITagService>(c));
            services.AddHttpClient("Comment").AddTypedClient(c => Refit.RestService.For<ICommentService>(c));
            services.AddHttpClient("PreView").AddTypedClient(c => Refit.RestService.For<IPreViewService>(c));
            services.AddHttpClient("Ad").AddTypedClient(c => Refit.RestService.For<IAdService>(c));
            return services;
        }
        public static IServiceCollection AddAccountHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient("User").AddTypedClient(c => Refit.RestService.For<IUserService>(c));
            return services;
        }
        public static IServiceCollection AddVideoHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient("Video").AddTypedClient(c => Refit.RestService.For<IVideoService>(c));
            return services;
        }
        public static IServiceCollection AddCategoryHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient("Category").AddTypedClient(c => Refit.RestService.For<ICategoryService>(c));
            return services;
        }
        public static IServiceCollection AddTagHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient("Tag").AddTypedClient(c => Refit.RestService.For<ITagService>(c));
            return services;
        }
        public static IServiceCollection AddCommentHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient("Comment").AddTypedClient(c => Refit.RestService.For<ICommentService>(c));
            return services;
        }
        public static IServiceCollection AddSystemHttpClient(this IServiceCollection services)
        {
            return services;
        }
        public static IServiceCollection AddPreViewHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient("PreView").AddTypedClient(c => Refit.RestService.For<IPreViewService>(c));
            return services;
        }
        public static IServiceCollection AddAdHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient("Ad").AddTypedClient(c => Refit.RestService.For<IAdService>(c));
            return services;
        }
    }
}
