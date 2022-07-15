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
    public class TestClothesController
    {
        private IMapper _mapper;
        private readonly ITestOutputHelper _output;

        public TestClothesController(ITestOutputHelper output)
        {
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new ClothesProfiles()));
            _mapper = mockMapper.CreateMapper();
            _output = output;
        }

        //----- GET CLOTHES (ALL) -----//

        [Fact]
        public async Task GetAllClothes_WhenSuccess_ReturnOk()
        {
            // Arrange
            var clothes = new List<Clothes>
            {
                new Clothes {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new Clothes {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new Image { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            var returnClothes = new List<ClothesDto>
            {
                new ClothesDto {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new ClothesDto {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new ImageDto { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.GetAsync()).Returns(Task.FromResult(clothes));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<ClothesDto>>(clothes)).Returns(returnClothes);

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.GetAllClothes();

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as IEnumerable<ClothesDto>;
            Assert.NotNull(data);
            Assert.Equal(returnClothes, data);
        }

        [Fact]
        public async Task GetAllClothes_WhenNoClothes_ReturnNotFound()
        {
            // Arrange
            var clothes = new List<Clothes>
            {
                new Clothes {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new Clothes {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new Image { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            var returnClothes = new List<ClothesDto>
            {
                new ClothesDto {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new ClothesDto {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new ImageDto { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.GetAsync()).Returns(Task.FromResult(new List<Clothes>()));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<ClothesDto>>(clothes)).Returns(returnClothes);

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.GetAllClothes();

            // Assert 
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Clothes empty!", notFoundResult.Value);
        }

        [Fact]
        public async Task GetAllClothes_WhenException_ReturnBadRequest()
        {
            // Arrange
            var clothes = new List<Clothes>
            {
                new Clothes {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new Clothes {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new Image { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            var returnClothes = new List<ClothesDto>
            {
                new ClothesDto {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new ClothesDto {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new ImageDto { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.GetAsync()).Throws(new Exception());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<ClothesDto>>(clothes)).Returns(returnClothes);

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.GetAllClothes();

            // Assert 
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong! Error: Exception of type 'System.Exception' was thrown.", badRequestResult.Value);
        }

        //----- GET CLOTHES (DELETED) -----//

        [Fact]
        public async Task GetAllClothes_WithDeleted_WhenSuccess_ReturnOk()
        {
            // Arrange
            var clothes = new List<Clothes>
            {
                new Clothes {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new Clothes {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new Image { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            var returnClothes = new List<ClothesDto>
            {
                new ClothesDto {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new ClothesDto {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new ImageDto { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.GetAllAsync()).Returns(Task.FromResult(clothes));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<ClothesDto>>(clothes)).Returns(returnClothes);

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.GetAllClothesDeleted();

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as IEnumerable<ClothesDto>;
            Assert.NotNull(data);
            Assert.Equal(returnClothes, data);
        }

        [Fact]
        public async Task GetAllClothes_WithDeleted_WhenNoClothes_ReturnNotFound()
        {
            // Arrange
            var clothes = new List<Clothes>
            {
                new Clothes {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new Clothes {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new Image { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            var returnClothes = new List<ClothesDto>
            {
                new ClothesDto {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new ClothesDto {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new ImageDto { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.GetAllAsync()).Returns(Task.FromResult(new List<Clothes>()));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<ClothesDto>>(clothes)).Returns(returnClothes);

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.GetAllClothesDeleted();

            // Assert 
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Clothes empty!", notFoundResult.Value);
        }

        [Fact]
        public async Task GetAllClothes_WithDeleted_WhenException_ReturnBadRequest()
        {
            // Arrange
            var clothes = new List<Clothes>
            {
                new Clothes {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new Clothes {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new Image { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            var returnClothes = new List<ClothesDto>
            {
                new ClothesDto {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new ClothesDto {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new ImageDto { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.GetAllAsync()).Throws(new Exception());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<ClothesDto>>(clothes)).Returns(returnClothes);

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.GetAllClothesDeleted();

            // Assert 
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong! Error: Exception of type 'System.Exception' was thrown.", badRequestResult.Value);
        }

        //----- GET CLOTHES (CATEGORY) -----//

        [Fact]
        public async Task GetAllClothes_WithCategory_WhenSuccess_ReturnOk()
        {
            // Arrange
            var clothes = new List<Clothes>
            {
                new Clothes {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new Clothes {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new Image { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            var returnClothes = new List<ClothesDto>
            {
                new ClothesDto {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new ClothesDto {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new ImageDto { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            int id = 1;

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.GetByCategoryId(id)).Returns(Task.FromResult(clothes));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<ClothesDto>>(clothes)).Returns(returnClothes);

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.GetAllClothesCategoryId(id);

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as IEnumerable<ClothesDto>;
            Assert.NotNull(data);
            Assert.Equal(returnClothes, data);
        }

        [Fact]
        public async Task GetAllClothes_WithCategory_WhenNoClothes_ReturnNotFound()
        {
            // Arrange
            var clothes = new List<Clothes>
            {
                new Clothes {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new Clothes {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new Image { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            var returnClothes = new List<ClothesDto>
            {
                new ClothesDto {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new ClothesDto {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new ImageDto { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            int id = 1;

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.GetByCategoryId(id)).Returns(Task.FromResult(new List<Clothes>()));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<ClothesDto>>(clothes)).Returns(returnClothes);

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.GetAllClothesCategoryId(id);

            // Assert 
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Clothes empty!", notFoundResult.Value);
        }

        [Fact]
        public async Task GetAllClothes_WithCategory_WhenException_ReturnBadRequest()
        {
            // Arrange
            var clothes = new List<Clothes>
            {
                new Clothes {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new Clothes {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new Image { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            var returnClothes = new List<ClothesDto>
            {
                new ClothesDto {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new ClothesDto {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new ImageDto { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            int id = 1;

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.GetByCategoryId(id)).Throws(new Exception());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<ClothesDto>>(clothes)).Returns(returnClothes);

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.GetAllClothesCategoryId(id);

            // Assert 
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong! Error: Exception of type 'System.Exception' was thrown.", badRequestResult.Value);
        }

        //----- GET CLOTHES (5) -----//

        [Fact]
        public async Task Get5Clothes_WhenSuccess_ReturnOk()
        {
            // Arrange
            var clothes = new List<Clothes>
            {
                new Clothes {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new Clothes {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new Image { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            var returnClothes = new List<ClothesDto>
            {
                new ClothesDto {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new ClothesDto {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new ImageDto { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.Get5ClothesAsync()).Returns(Task.FromResult(clothes));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<ClothesDto>>(clothes)).Returns(returnClothes);

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.Get5Clothes();

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as IEnumerable<ClothesDto>;
            Assert.NotNull(data);
            Assert.Equal(returnClothes, data);
        }

        [Fact]
        public async Task Get5Clothes_WhenNoClothes_ReturnNotFound()
        {
            // Arrange
            var clothes = new List<Clothes>
            {
                new Clothes {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new Clothes {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new Image { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            var returnClothes = new List<ClothesDto>
            {
                new ClothesDto {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new ClothesDto {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new ImageDto { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.Get5ClothesAsync()).Returns(Task.FromResult(new List<Clothes>()));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<ClothesDto>>(clothes)).Returns(returnClothes);

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.Get5Clothes();

            // Assert 
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Clothes empty!", notFoundResult.Value);
        }

        [Fact]
        public async Task Get5Clothes_WhenException_ReturnBadRequest()
        {
            // Arrange
            var clothes = new List<Clothes>
            {
                new Clothes {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new Clothes {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new Image { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            var returnClothes = new List<ClothesDto>
            {
                new ClothesDto {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
                new ClothesDto {
                    ID = 2,
                    Name = "Clothes 02",
                    Description = "Clothes 02 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 3, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                        new ImageDto { Id = 4, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 2 },
                    }
                },
            };

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.Get5ClothesAsync()).Throws(new Exception());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<ClothesDto>>(clothes)).Returns(returnClothes);

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.Get5Clothes();

            // Assert 
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong! Error: Exception of type 'System.Exception' was thrown.", badRequestResult.Value);
        }

        //----- GET CLOTHES -----//

        [Fact]
        public async Task GetClothes_WhenSuccess_ReturnOk()
        {
            // Arrange
            var clothes = new List<Clothes>
            {
                new Clothes {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                }
            };

            var returnClothes = new List<ClothesDto>
            {
                new ClothesDto {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                }
            };

            int id = 1;

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.GetByIdAsync(id)).Returns(Task.FromResult(clothes));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<ClothesDto>>(clothes)).Returns(returnClothes);

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.GetClothes(id);

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as IEnumerable<ClothesDto>;
            Assert.NotNull(data);
            Assert.Equal(returnClothes, data);
        }

        [Fact]
        public async Task GetClothes_WhenNoClothes_ReturnNotFound()
        {
            // Arrange
            var clothes = new List<Clothes>
            {
                new Clothes {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
            };

            var returnClothes = new List<ClothesDto>
            {
                new ClothesDto {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
            };

            int id = 1;

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.GetByIdAsync(id)).Returns(Task.FromResult(new List<Clothes>()));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<ClothesDto>>(clothes)).Returns(returnClothes);

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.GetClothes(id);

            // Assert 
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Clothes empty!", notFoundResult.Value);
        }

        [Fact]
        public async Task GetClothes_WhenException_ReturnBadRequest()
        {
            // Arrange
            var clothes = new List<Clothes>
            {
                new Clothes {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                    Images = new List<Image> {
                        new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
            };

            var returnClothes = new List<ClothesDto>
            {
                new ClothesDto {
                    ID = 1,
                    Name = "Clothes 01",
                    Description = "Clothes 01 Description",
                    Stock = 12,
                    Price = 20000,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CategoryId = 1,
                    CategoryName = "Category 01",
                    Images = new List<ImageDto> {
                        new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                        new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    }
                },
            };

            int id = 1;

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.GetByIdAsync(id)).Throws(new Exception());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<ClothesDto>>(clothes)).Returns(returnClothes);

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.GetClothes(id);

            // Assert 
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong! Error: Exception of type 'System.Exception' was thrown.", badRequestResult.Value);
        }

        //----- POST CLOTHES -----//

        [Fact]
        public async Task PostClothes_WhenSuccess_ReturnOk()
        {
            // Arrange
            var clothes = new Clothes {
                ID = 1,
                Name = "Clothes 01",
                Description = "Clothes 01 Description",
                Stock = 12,
                Price = 20000,
                AddedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
                CategoryId = 1,
                Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                Images = new List<Image> {
                    new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                }
            };

            var returnClothes = new ClothesDto {
                ID = 1,
                Name = "Clothes 01",
                Description = "Clothes 01 Description",
                Stock = 12,
                Price = 20000,
                AddedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                IsDeleted = false,
                CategoryId = 1,
                CategoryName = "Category 01",
                Images = new List<ImageDto> {
                    new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                }
            };

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.PostAsync(clothes)).Returns(Task.FromResult(clothes));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<ClothesDto>(clothes)).Returns(returnClothes);
            mapperMock.Setup(mapper => mapper.Map<Clothes>(returnClothes)).Returns(clothes);

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.PostClothes(returnClothes);

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as ClothesDto;
            Assert.NotNull(data);
            Assert.Equal(returnClothes, data);
        }

        [Fact]
        public async Task PostClothes_WhenException_ReturnBadRequest()
        {
            // Arrange
            var clothes = new Clothes {
                ID = 1,
                Name = "Clothes 01",
                Description = "Clothes 01 Description",
                Stock = 12,
                Price = 20000,
                AddedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
                CategoryId = 1,
                Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                Images = new List<Image> {
                    new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                }
            };

            var returnClothes = new ClothesDto {
                ID = 1,
                Name = "Clothes 01",
                Description = "Clothes 01 Description",
                Stock = 12,
                Price = 20000,
                AddedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                IsDeleted = false,
                CategoryId = 1,
                CategoryName = "Category 01",
                Images = new List<ImageDto> {
                    new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                }
            };

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.PostAsync(clothes)).Throws(new Exception());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<ClothesDto>(clothes)).Returns(returnClothes);
            mapperMock.Setup(mapper => mapper.Map<Clothes>(returnClothes)).Returns(clothes);

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.PostClothes(returnClothes);

            // Assert 
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong! Error: Exception of type 'System.Exception' was thrown.", badRequestResult.Value);
        }

        //----- PUT CLOTHES -----//

        [Fact]
        public async Task PutClothes_WhenSuccess_ReturnOk()
        {
            // Arrange
            var clothes = new Clothes
            {
                ID = 1,
                Name = "Clothes 01",
                Description = "Clothes 01 Description",
                Stock = 12,
                Price = 20000,
                AddedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
                CategoryId = 1,
                Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                Images = new List<Image> {
                    new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                }
            };

            var returnClothes = new ClothesDto
            {
                ID = 1,
                Name = "Clothes 01",
                Description = "Clothes 01 Description",
                Stock = 12,
                Price = 20000,
                AddedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                IsDeleted = false,
                CategoryId = 1,
                CategoryName = "Category 01",
                Images = new List<ImageDto> {
                    new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                }
            };

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.PutAsync(clothes)).Returns(Task.FromResult(clothes));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<ClothesDto>(clothes)).Returns(returnClothes);
            mapperMock.Setup(mapper => mapper.Map<Clothes>(returnClothes)).Returns(clothes);

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.PutClothes(returnClothes);

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as ClothesDto;
            Assert.NotNull(data);
            Assert.Equal(returnClothes, data);
        }

        [Fact]
        public async Task PutClothes_WhenException_ReturnBadRequest()
        {
            // Arrange
            var clothes = new Clothes
            {
                ID = 1,
                Name = "Clothes 01",
                Description = "Clothes 01 Description",
                Stock = 12,
                Price = 20000,
                AddedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
                CategoryId = 1,
                Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                Images = new List<Image> {
                    new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                }
            };

            var returnClothes = new ClothesDto
            {
                ID = 1,
                Name = "Clothes 01",
                Description = "Clothes 01 Description",
                Stock = 12,
                Price = 20000,
                AddedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                IsDeleted = false,
                CategoryId = 1,
                CategoryName = "Category 01",
                Images = new List<ImageDto> {
                    new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                }
            };

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.PutAsync(clothes)).Throws(new Exception());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<ClothesDto>(clothes)).Returns(returnClothes);
            mapperMock.Setup(mapper => mapper.Map<Clothes>(returnClothes)).Returns(clothes);

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.PutClothes(returnClothes);

            // Assert 
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong! Error: Exception of type 'System.Exception' was thrown.", badRequestResult.Value);
        }

        //----- DELETE CLOTHES -----//

        [Fact]
        public async Task DeleteClothes_WhenSuccess_ReturnOk()
        {
            // Arrange
            var clothes = new Clothes
            {
                ID = 1,
                Name = "Clothes 01",
                Description = "Clothes 01 Description",
                Stock = 12,
                Price = 20000,
                AddedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
                CategoryId = 1,
                Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                Images = new List<Image> {
                    new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                }
            };

            var returnClothes = new ClothesDto
            {
                ID = 1,
                Name = "Clothes 01",
                Description = "Clothes 01 Description",
                Stock = 12,
                Price = 20000,
                AddedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                IsDeleted = false,
                CategoryId = 1,
                CategoryName = "Category 01",
                Images = new List<ImageDto> {
                    new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                }
            };

            int id = 1;

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.DeleteAsync(id)).Returns(Task.FromResult(clothes));

            var mapperMock = new Mock<IMapper>();

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.DeleteClothes(id);

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as ClothesDto;
            Assert.NotNull(data);
            Assert.Equal(returnClothes, data);
        }

        [Fact]
        public async Task DeleteClothes_WhenNoClothes_ReturnNotFound()
        {
            // Arrange
            var clothes = new Clothes
            {
                ID = 1,
                Name = "Clothes 01",
                Description = "Clothes 01 Description",
                Stock = 12,
                Price = 20000,
                AddedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
                CategoryId = 1,
                Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                Images = new List<Image> {
                    new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                }
            };

            var returnClothes = new ClothesDto
            {
                ID = 1,
                Name = "Clothes 01",
                Description = "Clothes 01 Description",
                Stock = 12,
                Price = 20000,
                AddedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                IsDeleted = false,
                CategoryId = 1,
                CategoryName = "Category 01",
                Images = new List<ImageDto> {
                    new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                }
            };

            int id = 3;

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.DeleteAsync(id));

            var mapperMock = new Mock<IMapper>();

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.DeleteClothes(id);

            // Assert 
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Clothes not found!", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteClothes_WhenException_ReturnBadRequest()
        {
            // Arrange
            var clothes = new Clothes
            {
                ID = 1,
                Name = "Clothes 01",
                Description = "Clothes 01 Description",
                Stock = 12,
                Price = 20000,
                AddedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
                CategoryId = 1,
                Category = new Category { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                Images = new List<Image> {
                    new Image { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    new Image { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                }
            };

            var returnClothes = new ClothesDto
            {
                ID = 1,
                Name = "Clothes 01",
                Description = "Clothes 01 Description",
                Stock = 12,
                Price = 20000,
                AddedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                IsDeleted = false,
                CategoryId = 1,
                CategoryName = "Category 01",
                Images = new List<ImageDto> {
                    new ImageDto { Id = 1, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                    new ImageDto { Id = 2, URL = "dummy-image.jpg", IsDeleted = false, ClothesId = 1 },
                }
            };

            int id = 1;

            var clothesRepositoryMock = new Mock<IClothesRepository>();
            clothesRepositoryMock.Setup(clothesRepository => clothesRepository.DeleteAsync(id)).Throws(new Exception());

            var mapperMock = new Mock<IMapper>();

            var clothesController = new ClothesController(mapperMock.Object, clothesRepositoryMock.Object);

            // Act
            var result = await clothesController.DeleteClothes(id);

            // Assert 
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong! Error: Exception of type 'System.Exception' was thrown.", badRequestResult.Value);
        }
    }
}
