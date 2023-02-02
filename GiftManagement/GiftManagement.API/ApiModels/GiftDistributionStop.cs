namespace GiftManagement.API.ApiModels
{
    public record class GiftDistributionStop(string Address, IEnumerable<string> Gifts)
    {
    }
}
