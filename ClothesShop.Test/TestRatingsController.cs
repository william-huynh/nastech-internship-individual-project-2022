using AutoMapper;
using ClothesShop.API.Controllers;
using ClothesShop.API.Interfaces;
using ClothesShop.API.Models;
using ClothesShop.API.Profiles;
using ClothesShop.SharedVMs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit.Abstractions;

namespace ClothesShop.Test
{
    public class TestRatingsController
    {
        private IMapper _mapper;
        private readonly ITestOutputHelper _output;

        public TestRatingsController(ITestOutputHelper output)
        {
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new RatingsProfiles()));
            _mapper = mockMapper.CreateMapper();
            _output = output;
        }

        //----- POST RATING -----//

        [Fact]
        public async Task PostRating_WhenSuccess_ReturnOk()
        {
            // Arrange
            var rating = new Rating
            {
                Id = 1,
                RatingNumber = 3,
                IsDelete = false,
                Clothes = new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false }
            };

            var returnRating = new RatingDto { Id = 1, RatingNumber = 3, IsDelete = false };

            var ratingsRepositoryMock = new Mock<IRatingRepository>();
            ratingsRepositoryMock.Setup(ratingsRepository => ratingsRepository.PostAsync(rating)).Returns(Task.FromResult(rating));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Rating>(returnRating)).Returns(rating);
            mapperMock.Setup(mapper => mapper.Map<RatingDto>(rating)).Returns(returnRating);

            var ratingsController = new RatingsController(mapperMock.Object, ratingsRepositoryMock.Object);

            // Act
            var result = await ratingsController.PostRating(returnRating);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as RatingDto;
            Assert.NotNull(data);
            Assert.Equal(returnRating, data);
        }

        [Fact]
        public async Task PostRating_WhenException_ReturnBadRequest()
        {
            // Arrange
            var rating = new Rating
            {
                Id = 1,
                RatingNumber = 3,
                IsDelete = false,
                Clothes = new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false }
            };

            var returnRating = new RatingDto { Id = 1, RatingNumber = 3, IsDelete = false };

            var ratingsRepositoryMock = new Mock<IRatingRepository>();
            ratingsRepositoryMock.Setup(ratingsRepository => ratingsRepository.PostAsync(rating)).Throws(new Exception());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Rating>(returnRating)).Returns(rating);
            mapperMock.Setup(mapper => mapper.Map<RatingDto>(rating)).Returns(returnRating);

            var ratingsController = new RatingsController(mapperMock.Object, ratingsRepositoryMock.Object);

            // Act
            var result = await ratingsController.PostRating(returnRating);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong! Error: Exception of type 'System.Exception' was thrown.", badRequestResult.Value);
        }
    }
}
