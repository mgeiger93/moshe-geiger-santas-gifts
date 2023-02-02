namespace GiftManagement.BusinessLogic.Services.RequestGift
{
    public record class GiftsToAddressModel(string Address, IEnumerable<GiftModel> Gifts)
    {
    }
}