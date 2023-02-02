using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftManagement.BusinessLogic.Services.RequestGift
{
    public interface IRequestGiftService
    {
        void CreateOrUpdateGiftRequest(CreateOrUpdateGiftRequestModel model);
        IEnumerable<GiftsToAddressModel> GetGiftsToDeliver();
    }
}
