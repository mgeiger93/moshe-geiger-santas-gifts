using GiftManagement.API.Controllers;
using GiftManagement.API.RequestModels;
using GiftManagement.BusinessLogic.Services.RequestGift;
using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace GiftManagement.Tests.API_tests
{
    public class GiftRequestControllerTests
    {
        private readonly GiftRequestController _target;
        private readonly Mock<IRequestGiftService> _serviceMock;

        public GiftRequestControllerTests()
        {
            Mock<ILogger<GiftRequestController>> mockLogger = new();
            _serviceMock = new();
            _target = new GiftRequestController(mockLogger.Object, _serviceMock.Object);
        }

        [Fact]
        public void CreateGiftsRequest_ValidInput_CallsService()
        {
            _target.CreateGiftsRequest(new GiftRequest
            {
                Name = "name",
                Age = 30,
                Address = "address",
                GiftsWanted = new GiftItemRequest[]
                {
                    CreateGiftItem(1),
                    CreateGiftItem(2)
                }
            });

            _serviceMock.Verify(service => service.CreateOrUpdateGiftRequest(It.Is<CreateOrUpdateGiftRequestModel>(model =>
                model.RequestorName == "name" && model.RequestorAge == 30 && model.RequestedAddress == "address" && model.Gifts.Count() == 2)));
        }

        private static GiftItemRequest CreateGiftItem(int index)
        {
            return new GiftItemRequest { Name = $"Gift {index}", Color = $"Color {index}" };
        }

        [Theory]
        [InlineData(null, 1, null, 2)]
        [InlineData("name", 2, null, 1)]
        [InlineData("name", 3, "address", 0)]
        [InlineData("name", -1, "address", 1)]
        public void CreateGiftsRequest_InvalidInput_ThrowsArgumentException(string name, int age, string address, int numOfGifts)
        {
            Assert.ThrowsAny<ArgumentException>(() =>
            {
                _target.CreateGiftsRequest(new GiftRequest
                {
                    Name = name,
                    Age = age,
                    Address = address,
                    GiftsWanted = CreateNGifts(numOfGifts)
                });
            });
        }

        private IEnumerable<GiftItemRequest> CreateNGifts(int numOfGifts)
        {
            int i = 0;
            while (i < numOfGifts)
            {
                yield return CreateGiftItem(i++);
            }
        }
    }
}
