using System.Collections.Generic;
using System.Threading.Tasks;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.MongoDataModel;
using FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Services;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Persistence;
using Moq;
using Xunit;

namespace FirstLineSoftwareInterviewTask.Testing.UnitTests
{
    public class ItemServiceFixture
    {
        private readonly IItemService _sut;
        private readonly Mock<IDatabaseRepository<ItemCollection>> _itemRepositoryMock;

        public ItemServiceFixture()
        {
            _itemRepositoryMock = new Mock<IDatabaseRepository<ItemCollection>>();
            
            _sut = new ItemService(_itemRepositoryMock.Object);
        }
        
        [Fact]
        public async Task GetAllAsync_WhenCalled_CallsGetAllAsyncAtLeastOnce()
        {
            //Act
            await _sut.GetAllAsync();
            
            //Assert
            _itemRepositoryMock.Verify(x => x.GetAllAsync(), Times.AtLeastOnce);
        }        
        
        [Fact]
        public async Task GetAllAsync_WhenCalled_ReturnsItemList()
        {
            //Arrange
            var itemListToReturn = new List<ItemCollection> { };
            
            _itemRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(itemListToReturn);
            
            //Act
            var actual = await _sut.GetAllAsync();
            
            //Assert
            Assert.Equal(itemListToReturn, actual);
        }
    }
}