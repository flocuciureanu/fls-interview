using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.MongoDataModel;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel;
using FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Services;
using Moq;
using Xunit;

namespace FirstLineSoftwareInterviewTask.Testing.UnitTests
{
    public class PricingServiceFixture
    {
        private readonly IPricingService _sut;
        private readonly Mock<IItemService> _itemServiceMock;

        public PricingServiceFixture()
        {
            _itemServiceMock = new Mock<IItemService>();
            
            _sut = new PricingService(_itemServiceMock.Object);
        }
        
        [Fact]
        public async Task GetItemPriceAsync_WhenItemIsNull_ReturnsZero()
        {
            //Arrange
            const string itemId = "notAValidItemId";
            ItemCollection nullItem = null;
            _itemServiceMock.Setup(x => x.GetByIdAsync(itemId)).ReturnsAsync(nullItem);

            //Act
            var actual = await _sut.GetItemPriceAsync(itemId, It.IsAny<int>());
            
            //Assert
            Assert.Equal(0, actual);
        }

        [Fact]
        public async Task GetItemPriceAsync_WhenCalled_CallsGetByIdAsyncAtLeastOnce()
        {
            //Act
            await _sut.GetItemPriceAsync(It.IsAny<string>(), It.IsAny<int>());

            //Assert
            _itemServiceMock.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(220)]
        public async Task GetItemPriceAsync_ItemHasNoDiscount_ReturnsPriceTimesCount(int count)
        {
            //Arrange
            const double itemPrice = 10.99;

            var item = new ItemCollection()
            {
                Pricing = new PricingDocument()
                {
                    PriceAmount = itemPrice,
                    AvailableDiscount = new DiscountDocument()
                    {
                        DiscountType = DiscountType.None
                    }
                }
            };
            
            _itemServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(item);

            //Act
            var actual = await _sut.GetItemPriceAsync(It.IsAny<string>(), count);

            //Assert
            Assert.Equal(Math.Round(item.Pricing.PriceAmount * count, 2), actual);
        }
        
        [Fact]
        public async Task GetItemPriceAsync_ItemHasBOGSHPDiscountAndCountIsLessThanTwo_ReturnsPriceTimesCount()
        {
            //Arrange
            const double itemPrice = 5.22;
            const int count = 1;
            
            var item = new ItemCollection()
            {
                Pricing = new PricingDocument()
                {
                    PriceAmount = itemPrice,
                    AvailableDiscount = new DiscountDocument()
                    {
                        DiscountType = DiscountType.MultiBuy_BOGSHP
                    }
                }
            };
            
            _itemServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(item);

            //Act
            var actual = await _sut.GetItemPriceAsync(It.IsAny<string>(), count);

            //Assert
            Assert.Equal(Math.Round(item.Pricing.PriceAmount * count, 2), actual);
        }
        
        [Theory]
        [InlineData(4)]
        [InlineData(8)]
        [InlineData(220)]
        [InlineData(1522)]
        public async Task GetItemPriceAsync_ItemHasBOGSHPDiscountAndCountIsEvenAndGreaterThanTwo_ReturnsBOGSHPFormula(int count)
        {
            //Arrange
            const double itemPrice = 2.81;
            
            var item = new ItemCollection()
            {
                Pricing = new PricingDocument()
                {
                    PriceAmount = itemPrice,
                    AvailableDiscount = new DiscountDocument()
                    {
                        DiscountType = DiscountType.MultiBuy_BOGSHP
                    }
                }
            };
            
            _itemServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(item);

            //Act
            var actual = await _sut.GetItemPriceAsync(It.IsAny<string>(), count);

            //Assert
            Assert.Equal(Math.Round(item.Pricing.PriceAmount * count * 0.75, 2), actual);
        }
            
        [Theory]
        [InlineData(15)]
        [InlineData(21)]
        [InlineData(999)]
        [InlineData(3527)]
        public async Task GetItemPriceAsync_ItemHasBOGSHPDiscountAndCountIsOddAndGreaterThanTwo_ReturnsBOGSHPFormula(int count)
        {
            //Arrange
            const double itemPrice = 11.03;
            
            var item = new ItemCollection()
            {
                Pricing = new PricingDocument()
                {
                    PriceAmount = itemPrice,
                    AvailableDiscount = new DiscountDocument()
                    {
                        DiscountType = DiscountType.MultiBuy_BOGSHP
                    }
                }
            };
            
            _itemServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(item);

            //Act
            var actual = await _sut.GetItemPriceAsync(It.IsAny<string>(), count);

            //Assert
            Assert.Equal(Math.Round(item.Pricing.PriceAmount * (count - 1) * 0.75 + itemPrice, 2), actual);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetItemPriceAsync_ItemHasBTGTFDiscountAndCountIsLessThanThree_ReturnsPriceTimesCount(int count)
        {
            //Arrange
            const double itemPrice = 5.91;
            
            var item = new ItemCollection()
            {
                Pricing = new PricingDocument()
                {
                    PriceAmount = itemPrice,
                    AvailableDiscount = new DiscountDocument()
                    {
                        DiscountType = DiscountType.MultiBuy_BTGTF
                    }
                }
            };
            
            _itemServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(item);

            //Act
            var actual = await _sut.GetItemPriceAsync(It.IsAny<string>(), count);

            //Assert
            Assert.Equal(Math.Round(item.Pricing.PriceAmount * count, 2), actual);
        }
        
        [Theory]
        [InlineData(9)]
        [InlineData(81)]
        [InlineData(669)]
        [InlineData(1524)]
        public async Task GetItemPriceAsync_ItemHasBTGTFDiscountAndCountIsMultipleOfThreeAndGreaterThanThree_ReturnsBTGTFFormula(int count)
        {
            //Arrange
            const double itemPrice = 4.20;
            
            var item = new ItemCollection()
            {
                Pricing = new PricingDocument()
                {
                    PriceAmount = itemPrice,
                    AvailableDiscount = new DiscountDocument()
                    {
                        DiscountType = DiscountType.MultiBuy_BTGTF
                    }
                }
            };
            
            _itemServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(item);

            //Act
            var actual = await _sut.GetItemPriceAsync(It.IsAny<string>(), count);

            //Assert
            Assert.Equal(Math.Round(item.Pricing.PriceAmount * (count / 3 * 2), 2), actual);
        }
            
        [Theory]
        [InlineData(15)]
        [InlineData(21)]
        [InlineData(999)]
        [InlineData(3527)]
        public async Task GetItemPriceAsync_ItemHasBTGTFDiscountAndCountIsNotMultipleOfThreeAndGreaterThanThree_ReturnsBTGTFFormula(int count)
        {
            //Arrange
            const double itemPrice = 9.09;
            
            var item = new ItemCollection()
            {
                Pricing = new PricingDocument()
                {
                    PriceAmount = itemPrice,
                    AvailableDiscount = new DiscountDocument()
                    {
                        DiscountType = DiscountType.MultiBuy_BTGTF
                    }
                }
            };
            
            _itemServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(item);

            //Act
            var actual = await _sut.GetItemPriceAsync(It.IsAny<string>(), count);

            //Assert
            Assert.Equal(Math.Round(item.Pricing.PriceAmount * (count / 3 * 2) + itemPrice * (count % 3), 2), actual);
        }
        
        [Fact]
        public async Task GetItemPriceAsync_ItemHasInvalidDiscount_ReturnsZero()
        {
            //Arrange
            const double itemPrice = 5.22;
            
            var item = new ItemCollection()
            {
                Pricing = new PricingDocument()
                {
                    PriceAmount = itemPrice,
                    AvailableDiscount = new DiscountDocument()
                    {
                        DiscountType = (DiscountType)10
                    }
                }
            };
            
            _itemServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(item);

            //Act
            var actual = await _sut.GetItemPriceAsync(It.IsAny<string>(), It.IsAny<int>());

            //Assert
            Assert.Equal(0, actual);
        }

        [Fact]
        public async Task GetCartTotalAsync_WhenCalled_CallsGetByIdAsyncAtLeastOnce()
        {
            //Arrange
            var cartWithItems = new CartDetails()
            {
                CartItems = new List<CartItem>() { new() { ItemId = "1", Count = 1 } }
            };
            
            //Act
            await _sut.GetCartTotalAsync(cartWithItems);

            //Assert
            _itemServiceMock.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task GetCartTotalAsync_CartHasNoItems_ReturnsZero()
        {
            //Arrange
            var cartWithNoItems = new CartDetails()
            {
                CartItems = new List<CartItem>()
            };

            //Act
            var actual = await _sut.GetCartTotalAsync(cartWithNoItems);
            
            //Assert
            Assert.Equal(0, actual);
        }
        
        [Fact]
        public async Task GetCartTotalAsync_CartHasItems_ReturnsCartTotal()
        {
            //Arrange
            var firstItem = new ItemCollection()
            {
                Id = "1",
                Pricing = new PricingDocument()
                {
                    PriceAmount = 10.2,
                    AvailableDiscount = new DiscountDocument() { DiscountType = DiscountType.None }
                }
            };            
            var secondItem = new ItemCollection()
            {
                Id = "2",
                Pricing = new PricingDocument()
                {
                    PriceAmount = 11.5,
                    AvailableDiscount = new DiscountDocument() { DiscountType = DiscountType.None }
                }
            };
            
            var cartWithItems = new CartDetails()
            {
                CartItems = new List<CartItem>()
                {
                    new() { ItemId = "1", Count = 5 },
                    new() { ItemId = "2", Count = 2 }
                }
            };
            
            _itemServiceMock.Setup(x => x.GetByIdAsync("1")).ReturnsAsync(firstItem);
            _itemServiceMock.Setup(x => x.GetByIdAsync("2")).ReturnsAsync(secondItem);
            
            //Act
            var actual = await _sut.GetCartTotalAsync(cartWithItems);
            
            //Assert
            Assert.Equal(74, actual);
        }
    }
}
