using GiftManagement.BusinessLogic.Services.RequestGift;
using GiftManagement.DAL.Repositories;

namespace GiftManagement.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
        {
            return services.AddScoped<IRequestGiftService, RequestGiftService>()
                           .AddSingleton<IGiftRequestorsRepository, GiftRequestorsRepository>();
        }
    }
}
