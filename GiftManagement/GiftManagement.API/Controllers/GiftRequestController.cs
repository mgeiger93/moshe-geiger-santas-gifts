using GiftManagement.API.RequestModels;
using GiftManagement.BusinessLogic.Services.RequestGift;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace GiftManagement.API.Controllers
{
    [ApiController]

    [Route("[controller]")]
    public class GiftRequestController : ControllerBase
    {
        private readonly ILogger<GiftRequestController> _logger;
        private readonly IRequestGiftService _requestService;

        public GiftRequestController(ILogger<GiftRequestController> logger,
                                     IRequestGiftService requestService)
        {
            _logger = logger;
            _requestService = requestService;
        }

        [HttpPost]
        public void CreateGiftsRequest(GiftRequest giftRequests)
        {
            ValidateGifts(giftRequests);
            _requestService.CreateOrUpdateGiftRequest(ConvertGiftRequest(giftRequests));
        }

        private void ValidateGifts(GiftRequest giftRequest)
        {
            if (giftRequest == null) throw new ArgumentNullException(nameof(giftRequest));
            if (string.IsNullOrWhiteSpace(giftRequest.Name)) throw new ArgumentNullException(nameof(giftRequest.Name));
            if (string.IsNullOrWhiteSpace(giftRequest.Address)) throw new ArgumentNullException(nameof(giftRequest.Address));
            if (giftRequest.GiftsWanted == null) throw new ArgumentNullException(nameof(giftRequest.GiftsWanted));
            if (!giftRequest.GiftsWanted.Any()) throw new ArgumentException("Cannot have no gifts requested", nameof(giftRequest.GiftsWanted));
            if (giftRequest.Age < 0) throw new ArgumentException("Age cannot be less than zero");
        }

        private static CreateOrUpdateGiftRequestModel ConvertGiftRequest(GiftRequest giftRequests) =>
            new(giftRequests.Name,
                giftRequests.Age,
                giftRequests.Address,
                giftRequests.GiftsWanted.Select(ConvertGiftItemToGiftRequestItem));

        private static GiftModel ConvertGiftItemToGiftRequestItem(GiftItemRequest giftRequestItem) =>
            new(giftRequestItem.Name, giftRequestItem.Color);
    }
}