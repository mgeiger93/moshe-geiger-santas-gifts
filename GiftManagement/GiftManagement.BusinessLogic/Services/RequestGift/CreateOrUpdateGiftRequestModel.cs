namespace GiftManagement.BusinessLogic.Services.RequestGift
{
    public record class CreateOrUpdateGiftRequestModel(string RequestorName,
                                                       int RequestorAge,
                                                       string RequestedAddress,
                                                       IEnumerable<GiftModel> Gifts);
}
