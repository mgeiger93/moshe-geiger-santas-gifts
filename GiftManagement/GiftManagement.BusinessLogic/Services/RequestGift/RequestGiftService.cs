using GiftManagement.DAL.DataModels;
using GiftManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GiftManagement.BusinessLogic.Services.RequestGift
{
    public class RequestGiftService : IRequestGiftService
    {
        private readonly IGiftRequestorsRepository _repository;

        public RequestGiftService(IGiftRequestorsRepository repository)
        {
            _repository = repository;
        }

        public void CreateOrUpdateGiftRequest(CreateOrUpdateGiftRequestModel model)
        {
            ValidateModel(model);
            Requestor? requestor = _repository.Requestors.FirstOrDefault(x => x.Name == model.RequestorName && x.Age == model.RequestorAge);

            if (requestor == null)
            {
                requestor = new Requestor
                {
                    Name = model.RequestorName,
                    Age = model.RequestorAge,
                };
                _repository.Requestors.Add(requestor);
            }

            requestor.Address = model.RequestedAddress;

            requestor.RequestedProducts = model.Gifts.ToDictionary(gift => gift.Type, gift => gift.Color);
        }

        public IEnumerable<GiftsToAddressModel> GetGiftsToDeliver()
        {
            Dictionary<string, List<GiftModel>> giftsPerAddress = new();
            foreach (Requestor requestor in _repository.Requestors)
            {
                List<GiftModel> gifts = new();
                float budget = 50;
                foreach (var gift in
                    requestor.RequestedProducts
                        .OrderByDescending(g => _repository.Catalog[g.Key])
                        .ThenByDescending(g => g.Key)
                        .ThenByDescending(g => g.Value))
                {
                    if ((budget -= _repository.Catalog[gift.Key]) < 0)
                        break;

                    gifts.Add(new GiftModel(gift.Key, gift.Value));
                }

                if (giftsPerAddress.ContainsKey(requestor.Address))
                {
                    giftsPerAddress[requestor.Address].AddRange(gifts);
                }
                else
                {
                    giftsPerAddress[requestor.Address] = gifts;
                }
            }
            return giftsPerAddress.Select(x => new GiftsToAddressModel(x.Key, x.Value));
        }

        private void ValidateModel(CreateOrUpdateGiftRequestModel model)
        {
            if (model.Gifts.Any(x => !_repository.Catalog.ContainsKey(x.Type))) throw new ArgumentException("Not all gifts types exist. Please configure all gifts with appropriate prices.");
            if (model.Gifts.GroupBy(x => x.Type).Any(group => group.Count() > 1)) throw new ArgumentException("Cannot request the same gift twice.");
        }
    }
}
