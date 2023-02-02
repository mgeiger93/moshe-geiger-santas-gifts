namespace GiftManagement.API.RequestModels
{
    public class GiftRequest
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public IEnumerable<GiftItemRequest> GiftsWanted { get; set; }
    }
}
