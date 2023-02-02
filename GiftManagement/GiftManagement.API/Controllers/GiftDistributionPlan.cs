using GiftManagement.API.ApiModels;
using GiftManagement.BusinessLogic.Services.RequestGift;
using Microsoft.AspNetCore.Mvc;

namespace GiftManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GiftDistributionPlan : ControllerBase
    {

        private readonly ILogger<GiftRequestController> _logger;
        private readonly IRequestGiftService _requestService;

        public GiftDistributionPlan(ILogger<GiftRequestController> logger, IRequestGiftService requestService)
        {
            _logger = logger;
            _requestService = requestService;
        }

        [HttpGet]
        public IEnumerable<GiftDistributionStop> GetGiftDistributionPlan()
        {
            return _requestService.GetGiftsToDeliver().Select(x => new GiftDistributionStop(x.Address, x.Gifts.Select(g => g.Type)));
        }
    }
}
