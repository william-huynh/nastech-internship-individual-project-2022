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
    public class TestCategoriesController
    {
        private IMapper _mapper;
        private readonly ITestOutputHelper _output;

        public TestCategoriesController(ITestOutputHelper output)
        {
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new CategoriesProfiles()));
            _mapper = mockMapper.CreateMapper();
            _output = output;
        }

        //----- GET CATEGORIES (ALL) -----//

        [Fact]
        public async Task GetCategories_WithoutDeleted_WhenSuccess_ReturnOk()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category {
                    Id = 1,
                    Name = "Category 01",
                    Description = "Category 01 Description",
                    IsDeleted = false,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false },
                        new Clothes { ID = 2, Name = "Clothes 02", Description = "Clothes 02 Description", Stock = 40, Price = 60000, CategoryId = 1, IsDeleted = false },
                        new Clothes { ID = 3, Name = "Clothes 03", Description = "Clothes 03 Description", Stock = 60, Price = 90000, CategoryId = 1, IsDeleted = false },
                    }
                },
                new Category {
                    Id = 2,
                    Name = "Category 02",
                    Description = "Category 02 Description",
                    IsDeleted = false,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 4, Name = "Clothes 04", Description = "Clothes 04 Description", Stock = 80, Price = 120000, CategoryId = 2, IsDeleted = false },
                        new Clothes { ID = 5, Name = "Clothes 05", Description = "Clothes 05 Description", Stock = 100, Price = 150000, CategoryId = 2, IsDeleted = false },
                        new Clothes { ID = 6, Name = "Clothes 06", Description = "Clothes 06 Description", Stock = 120, Price = 180000, CategoryId = 2, IsDeleted = false },
                    }
                },
                new Category {
                    Id = 3,
                    Name = "Category 03",
                    Description = "Category 03 Description",
                    IsDeleted = false,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 7, Name = "Clothes 07", Description = "Clothes 07 Description", Stock = 140, Price = 210000, CategoryId = 3, IsDeleted = false },
                        new Clothes { ID = 8, Name = "Clothes 08", Description = "Clothes 08 Description", Stock = 160, Price = 240000, CategoryId = 3, IsDeleted = false },
                        new Clothes { ID = 9, Name = "Clothes 09", Description = "Clothes 09 Description", Stock = 180, Price = 270000, CategoryId = 3, IsDeleted = false },
                    }
                },
            };

            var returnCategories = new List<CategoryDto>
            {
                new CategoryDto { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                new CategoryDto { Id = 2, Name = "Category 02", Description = "Category 02 Description", IsDeleted = false },
                new CategoryDto { Id = 3, Name = "Category 03", Description = "Category 03 Description", IsDeleted = false },
            };

            var categoriesRepositoryMock = new Mock<ICategoryRepository>();
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.GetAsync()).Returns(Task.FromResult(categories));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<CategoryDto>>(categories)).Returns(returnCategories);

            var categoriesController = new CategoriesController(mapperMock.Object, categoriesRepositoryMock.Object);

            // Act
            var result = await categoriesController.GetCategories();

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as IEnumerable<CategoryDto>;
            Assert.NotNull(data);
            Assert.Equal(returnCategories, data);
        }

        [Fact]
        public async Task GetCategories_WithDeleted_WhenSuccess_ReturnOk()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category {
                    Id = 1,
                    Name = "Category 01",
                    Description = "Category 01 Description",
                    IsDeleted = false,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false },
                        new Clothes { ID = 2, Name = "Clothes 02", Description = "Clothes 02 Description", Stock = 40, Price = 60000, CategoryId = 1, IsDeleted = false },
                        new Clothes { ID = 3, Name = "Clothes 03", Description = "Clothes 03 Description", Stock = 60, Price = 90000, CategoryId = 1, IsDeleted = false },
                    }
                },
                new Category {
                    Id = 2,
                    Name = "Category 02",
                    Description = "Category 02 Description",
                    IsDeleted = true,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 4, Name = "Clothes 04", Description = "Clothes 04 Description", Stock = 80, Price = 120000, CategoryId = 2, IsDeleted = false },
                        new Clothes { ID = 5, Name = "Clothes 05", Description = "Clothes 05 Description", Stock = 100, Price = 150000, CategoryId = 2, IsDeleted = false },
                        new Clothes { ID = 6, Name = "Clothes 06", Description = "Clothes 06 Description", Stock = 120, Price = 180000, CategoryId = 2, IsDeleted = false },
                    }
                },
                new Category {
                    Id = 3,
                    Name = "Category 03",
                    Description = "Category 03 Description",
                    IsDeleted = false,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 7, Name = "Clothes 07", Description = "Clothes 07 Description", Stock = 140, Price = 210000, CategoryId = 3, IsDeleted = false },
                        new Clothes { ID = 8, Name = "Clothes 08", Description = "Clothes 08 Description", Stock = 160, Price = 240000, CategoryId = 3, IsDeleted = false },
                        new Clothes { ID = 9, Name = "Clothes 09", Description = "Clothes 09 Description", Stock = 180, Price = 270000, CategoryId = 3, IsDeleted = false },
                    }
                },
            };

            var returnCategories = new List<CategoryDto>
            {
                new CategoryDto { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                new CategoryDto { Id = 3, Name = "Category 03", Description = "Category 03 Description", IsDeleted = false },
            };

            var categoriesRepositoryMock = new Mock<ICategoryRepository>();
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.GetAsync()).Returns(Task.FromResult(categories));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<CategoryDto>>(categories)).Returns(returnCategories);

            var categoriesController = new CategoriesController(mapperMock.Object, categoriesRepositoryMock.Object);

            // Act
            var result = await categoriesController.GetCategories();

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as IEnumerable<CategoryDto>;
            Assert.NotNull(data);
            Assert.Equal(returnCategories, data);
        }

        [Fact]
        public async Task GetCategories_WhenNoCategories_ReturnNotFound()
        {
            // Arrange
            var categories = new List<Category>();

            var returnCategories = new List<CategoryDto>();

            var categoriesRepositoryMock = new Mock<ICategoryRepository>();
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.GetAsync()).Returns(Task.FromResult(new List<Category>()));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<CategoryDto>>(categories)).Returns(returnCategories);

            var categoriesController = new CategoriesController(mapperMock.Object, categoriesRepositoryMock.Object);

            // Act
            var result = await categoriesController.GetCategories();

            // Assert 
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Categories empty!", notFoundResult.Value);
        }

        [Fact]
        public async Task GetCategories_WhenException_ReturnBadRequest()
        {
            // Arrange
            var categories = new List<Category>();

            var returnCategories = new List<CategoryDto>();

            var categoriesRepositoryMock = new Mock<ICategoryRepository>();
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.GetAsync()).Throws(new Exception());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<CategoryDto>>(categories)).Returns(returnCategories);

            var categoriesController = new CategoriesController(mapperMock.Object, categoriesRepositoryMock.Object);

            // Act
            var result = await categoriesController.GetCategories();

            // Assert 
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong! Error: Exception of type 'System.Exception' was thrown.", badRequestResult.Value);
        }

        //----- GET CATEGORIES (5) -----//

        [Fact]
        public async Task Get5Categories_WithoutDeleted_WhenSuccess_ReturnOk()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category {
                    Id = 1,
                    Name = "Category 01",
                    Description = "Category 01 Description",
                    IsDeleted = false,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false },
                        new Clothes { ID = 2, Name = "Clothes 02", Description = "Clothes 02 Description", Stock = 40, Price = 60000, CategoryId = 1, IsDeleted = false },
                        new Clothes { ID = 3, Name = "Clothes 03", Description = "Clothes 03 Description", Stock = 60, Price = 90000, CategoryId = 1, IsDeleted = false },
                    }
                },
                new Category {
                    Id = 2,
                    Name = "Category 02",
                    Description = "Category 02 Description",
                    IsDeleted = false,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 4, Name = "Clothes 04", Description = "Clothes 04 Description", Stock = 80, Price = 120000, CategoryId = 2, IsDeleted = false },
                        new Clothes { ID = 5, Name = "Clothes 05", Description = "Clothes 05 Description", Stock = 100, Price = 150000, CategoryId = 2, IsDeleted = false },
                        new Clothes { ID = 6, Name = "Clothes 06", Description = "Clothes 06 Description", Stock = 120, Price = 180000, CategoryId = 2, IsDeleted = false },
                    }
                },
                new Category {
                    Id = 3,
                    Name = "Category 03",
                    Description = "Category 03 Description",
                    IsDeleted = false,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 7, Name = "Clothes 07", Description = "Clothes 07 Description", Stock = 140, Price = 210000, CategoryId = 3, IsDeleted = false },
                        new Clothes { ID = 8, Name = "Clothes 08", Description = "Clothes 08 Description", Stock = 160, Price = 240000, CategoryId = 3, IsDeleted = false },
                        new Clothes { ID = 9, Name = "Clothes 09", Description = "Clothes 09 Description", Stock = 180, Price = 270000, CategoryId = 3, IsDeleted = false },
                    }
                },
                new Category {
                    Id = 4,
                    Name = "Category 04",
                    Description = "Category 04 Description",
                    IsDeleted = false,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 10, Name = "Clothes 10", Description = "Clothes 10 Description", Stock = 140, Price = 210000, CategoryId = 4, IsDeleted = false },
                        new Clothes { ID = 11, Name = "Clothes 11", Description = "Clothes 11 Description", Stock = 160, Price = 240000, CategoryId = 4, IsDeleted = false },
                        new Clothes { ID = 12, Name = "Clothes 12", Description = "Clothes 12 Description", Stock = 180, Price = 270000, CategoryId = 4, IsDeleted = false },
                    }
                },
                new Category {
                    Id = 5,
                    Name = "Category 05",
                    Description = "Category 05 Description",
                    IsDeleted = false,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 13, Name = "Clothes 13", Description = "Clothes 13 Description", Stock = 140, Price = 210000, CategoryId = 5, IsDeleted = false },
                        new Clothes { ID = 14, Name = "Clothes 14", Description = "Clothes 14 Description", Stock = 160, Price = 240000, CategoryId = 5, IsDeleted = false },
                        new Clothes { ID = 15, Name = "Clothes 15", Description = "Clothes 15 Description", Stock = 180, Price = 270000, CategoryId = 5, IsDeleted = false },
                    }
                },
                new Category {
                    Id = 6,
                    Name = "Category 06",
                    Description = "Category 06 Description",
                    IsDeleted = false,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 16, Name = "Clothes 16", Description = "Clothes 16 Description", Stock = 140, Price = 210000, CategoryId = 6, IsDeleted = false },
                        new Clothes { ID = 17, Name = "Clothes 17", Description = "Clothes 17 Description", Stock = 160, Price = 240000, CategoryId = 6, IsDeleted = false },
                        new Clothes { ID = 18, Name = "Clothes 18", Description = "Clothes 18 Description", Stock = 180, Price = 270000, CategoryId = 6, IsDeleted = false },
                    }
                },
            };

            var returnCategories = new List<CategoryDto>
            {
                new CategoryDto { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                new CategoryDto { Id = 2, Name = "Category 02", Description = "Category 02 Description", IsDeleted = false },
                new CategoryDto { Id = 3, Name = "Category 03", Description = "Category 03 Description", IsDeleted = false },
                new CategoryDto { Id = 4, Name = "Category 04", Description = "Category 04 Description", IsDeleted = false },
                new CategoryDto { Id = 5, Name = "Category 05", Description = "Category 05 Description", IsDeleted = false },
            };

            var categoriesRepositoryMock = new Mock<ICategoryRepository>();
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.Get5Async()).Returns(Task.FromResult(categories));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<CategoryDto>>(categories)).Returns(returnCategories);

            var categoriesController = new CategoriesController(mapperMock.Object, categoriesRepositoryMock.Object);

            // Act
            var result = await categoriesController.Get5Categories();

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as IEnumerable<CategoryDto>;
            Assert.NotNull(data);
            Assert.Equal(returnCategories, data);
        }

        [Fact]
        public async Task Get5Categories_WithDeleted_WhenSuccess_ReturnOk_5Categories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category {
                    Id = 1,
                    Name = "Category 01",
                    Description = "Category 01 Description",
                    IsDeleted = false,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false },
                        new Clothes { ID = 2, Name = "Clothes 02", Description = "Clothes 02 Description", Stock = 40, Price = 60000, CategoryId = 1, IsDeleted = false },
                        new Clothes { ID = 3, Name = "Clothes 03", Description = "Clothes 03 Description", Stock = 60, Price = 90000, CategoryId = 1, IsDeleted = false },
                    }
                },
                new Category {
                    Id = 2,
                    Name = "Category 02",
                    Description = "Category 02 Description",
                    IsDeleted = true,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 4, Name = "Clothes 04", Description = "Clothes 04 Description", Stock = 80, Price = 120000, CategoryId = 2, IsDeleted = false },
                        new Clothes { ID = 5, Name = "Clothes 05", Description = "Clothes 05 Description", Stock = 100, Price = 150000, CategoryId = 2, IsDeleted = false },
                        new Clothes { ID = 6, Name = "Clothes 06", Description = "Clothes 06 Description", Stock = 120, Price = 180000, CategoryId = 2, IsDeleted = false },
                    }
                },
                new Category {
                    Id = 3,
                    Name = "Category 03",
                    Description = "Category 03 Description",
                    IsDeleted = false,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 7, Name = "Clothes 07", Description = "Clothes 07 Description", Stock = 140, Price = 210000, CategoryId = 3, IsDeleted = false },
                        new Clothes { ID = 8, Name = "Clothes 08", Description = "Clothes 08 Description", Stock = 160, Price = 240000, CategoryId = 3, IsDeleted = false },
                        new Clothes { ID = 9, Name = "Clothes 09", Description = "Clothes 09 Description", Stock = 180, Price = 270000, CategoryId = 3, IsDeleted = false },
                    }
                },
                new Category {
                    Id = 4,
                    Name = "Category 04",
                    Description = "Category 04 Description",
                    IsDeleted = false,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 10, Name = "Clothes 10", Description = "Clothes 10 Description", Stock = 140, Price = 210000, CategoryId = 4, IsDeleted = false },
                        new Clothes { ID = 11, Name = "Clothes 11", Description = "Clothes 11 Description", Stock = 160, Price = 240000, CategoryId = 4, IsDeleted = false },
                        new Clothes { ID = 12, Name = "Clothes 12", Description = "Clothes 12 Description", Stock = 180, Price = 270000, CategoryId = 4, IsDeleted = false },
                    }
                },
                new Category {
                    Id = 5,
                    Name = "Category 05",
                    Description = "Category 05 Description",
                    IsDeleted = false,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 13, Name = "Clothes 13", Description = "Clothes 13 Description", Stock = 140, Price = 210000, CategoryId = 5, IsDeleted = false },
                        new Clothes { ID = 14, Name = "Clothes 14", Description = "Clothes 14 Description", Stock = 160, Price = 240000, CategoryId = 5, IsDeleted = false },
                        new Clothes { ID = 15, Name = "Clothes 15", Description = "Clothes 15 Description", Stock = 180, Price = 270000, CategoryId = 5, IsDeleted = false },
                    }
                },
                new Category {
                    Id = 6,
                    Name = "Category 06",
                    Description = "Category 06 Description",
                    IsDeleted = false,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 16, Name = "Clothes 16", Description = "Clothes 16 Description", Stock = 140, Price = 210000, CategoryId = 6, IsDeleted = false },
                        new Clothes { ID = 17, Name = "Clothes 17", Description = "Clothes 17 Description", Stock = 160, Price = 240000, CategoryId = 6, IsDeleted = false },
                        new Clothes { ID = 18, Name = "Clothes 18", Description = "Clothes 18 Description", Stock = 180, Price = 270000, CategoryId = 6, IsDeleted = false },
                    }
                },
            };

            var returnCategories = new List<CategoryDto>
            {
                new CategoryDto { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                new CategoryDto { Id = 3, Name = "Category 03", Description = "Category 03 Description", IsDeleted = false },
                new CategoryDto { Id = 4, Name = "Category 04", Description = "Category 04 Description", IsDeleted = false },
                new CategoryDto { Id = 5, Name = "Category 05", Description = "Category 05 Description", IsDeleted = false },
                new CategoryDto { Id = 6, Name = "Category 06", Description = "Category 06 Description", IsDeleted = false },
            };

            var categoriesRepositoryMock = new Mock<ICategoryRepository>();
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.Get5Async()).Returns(Task.FromResult(categories));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<CategoryDto>>(categories)).Returns(returnCategories);

            var categoriesController = new CategoriesController(mapperMock.Object, categoriesRepositoryMock.Object);

            // Act
            var result = await categoriesController.Get5Categories();

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as IEnumerable<CategoryDto>;
            Assert.NotNull(data);
            Assert.Equal(returnCategories, data);
        }

        [Fact]
        public async Task Get5Categories_WithDeleted_WhenSuccess_ReturnOk_3Categories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category {
                    Id = 1,
                    Name = "Category 01",
                    Description = "Category 01 Description",
                    IsDeleted = false,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false },
                        new Clothes { ID = 2, Name = "Clothes 02", Description = "Clothes 02 Description", Stock = 40, Price = 60000, CategoryId = 1, IsDeleted = false },
                        new Clothes { ID = 3, Name = "Clothes 03", Description = "Clothes 03 Description", Stock = 60, Price = 90000, CategoryId = 1, IsDeleted = false },
                    }
                },
                new Category {
                    Id = 2,
                    Name = "Category 02",
                    Description = "Category 02 Description",
                    IsDeleted = true,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 4, Name = "Clothes 04", Description = "Clothes 04 Description", Stock = 80, Price = 120000, CategoryId = 2, IsDeleted = false },
                        new Clothes { ID = 5, Name = "Clothes 05", Description = "Clothes 05 Description", Stock = 100, Price = 150000, CategoryId = 2, IsDeleted = false },
                        new Clothes { ID = 6, Name = "Clothes 06", Description = "Clothes 06 Description", Stock = 120, Price = 180000, CategoryId = 2, IsDeleted = false },
                    }
                },
                new Category {
                    Id = 3,
                    Name = "Category 03",
                    Description = "Category 03 Description",
                    IsDeleted = false,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 7, Name = "Clothes 07", Description = "Clothes 07 Description", Stock = 140, Price = 210000, CategoryId = 3, IsDeleted = false },
                        new Clothes { ID = 8, Name = "Clothes 08", Description = "Clothes 08 Description", Stock = 160, Price = 240000, CategoryId = 3, IsDeleted = false },
                        new Clothes { ID = 9, Name = "Clothes 09", Description = "Clothes 09 Description", Stock = 180, Price = 270000, CategoryId = 3, IsDeleted = false },
                    }
                },
                new Category {
                    Id = 4,
                    Name = "Category 04",
                    Description = "Category 04 Description",
                    IsDeleted = true,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 10, Name = "Clothes 10", Description = "Clothes 10 Description", Stock = 140, Price = 210000, CategoryId = 4, IsDeleted = false },
                        new Clothes { ID = 11, Name = "Clothes 11", Description = "Clothes 11 Description", Stock = 160, Price = 240000, CategoryId = 4, IsDeleted = false },
                        new Clothes { ID = 12, Name = "Clothes 12", Description = "Clothes 12 Description", Stock = 180, Price = 270000, CategoryId = 4, IsDeleted = false },
                    }
                },
                new Category {
                    Id = 5,
                    Name = "Category 05",
                    Description = "Category 05 Description",
                    IsDeleted = false,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 13, Name = "Clothes 13", Description = "Clothes 13 Description", Stock = 140, Price = 210000, CategoryId = 5, IsDeleted = false },
                        new Clothes { ID = 14, Name = "Clothes 14", Description = "Clothes 14 Description", Stock = 160, Price = 240000, CategoryId = 5, IsDeleted = false },
                        new Clothes { ID = 15, Name = "Clothes 15", Description = "Clothes 15 Description", Stock = 180, Price = 270000, CategoryId = 5, IsDeleted = false },
                    }
                },
                new Category {
                    Id = 6,
                    Name = "Category 06",
                    Description = "Category 06 Description",
                    IsDeleted = true,
                    Clothes = new List<Clothes>
                    {
                        new Clothes { ID = 16, Name = "Clothes 16", Description = "Clothes 16 Description", Stock = 140, Price = 210000, CategoryId = 6, IsDeleted = false },
                        new Clothes { ID = 17, Name = "Clothes 17", Description = "Clothes 17 Description", Stock = 160, Price = 240000, CategoryId = 6, IsDeleted = false },
                        new Clothes { ID = 18, Name = "Clothes 18", Description = "Clothes 18 Description", Stock = 180, Price = 270000, CategoryId = 6, IsDeleted = false },
                    }
                },
            };

            var returnCategories = new List<CategoryDto>
            {
                new CategoryDto { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false },
                new CategoryDto { Id = 3, Name = "Category 03", Description = "Category 03 Description", IsDeleted = false },
                new CategoryDto { Id = 5, Name = "Category 05", Description = "Category 05 Description", IsDeleted = false },
            };

            var categoriesRepositoryMock = new Mock<ICategoryRepository>();
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.Get5Async()).Returns(Task.FromResult(categories));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<CategoryDto>>(categories)).Returns(returnCategories);

            var categoriesController = new CategoriesController(mapperMock.Object, categoriesRepositoryMock.Object);

            // Act
            var result = await categoriesController.Get5Categories();

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as IEnumerable<CategoryDto>;
            Assert.NotNull(data);
            Assert.Equal(returnCategories, data);
        }

        [Fact]
        public async Task Get5Categories_WhenNoCategories_ReturnNotFound()
        {
            // Arrange
            var categories = new List<Category>();

            var returnCategories = new List<CategoryDto>();

            var categoriesRepositoryMock = new Mock<ICategoryRepository>();
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.Get5Async()).Returns(Task.FromResult(new List<Category>()));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<CategoryDto>>(categories)).Returns(returnCategories);

            var categoriesController = new CategoriesController(mapperMock.Object, categoriesRepositoryMock.Object);

            // Act
            var result = await categoriesController.Get5Categories();

            // Assert 
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Categories empty!", notFoundResult.Value);
        }

        [Fact]
        public async Task Get5Categories_WhenException_ReturnBadRequest()
        {
            // Arrange
            var categories = new List<Category>();

            var returnCategories = new List<CategoryDto>();

            var categoriesRepositoryMock = new Mock<ICategoryRepository>();
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.Get5Async()).Throws(new Exception());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<CategoryDto>>(categories)).Returns(returnCategories);

            var categoriesController = new CategoriesController(mapperMock.Object, categoriesRepositoryMock.Object);

            // Act
            var result = await categoriesController.Get5Categories();

            // Assert 
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong! Error: Exception of type 'System.Exception' was thrown.", badRequestResult.Value);
        }

        //----- GET CATEGORY -----//

        [Fact]
        public async Task GetCategory_WhenSuccess_ReturnOk()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Name = "Category 01",
                Description = "Category 01 Description",
                IsDeleted = false,
                Clothes = new List<Clothes>
                {
                    new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 2, Name = "Clothes 02", Description = "Clothes 02 Description", Stock = 40, Price = 60000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 3, Name = "Clothes 03", Description = "Clothes 03 Description", Stock = 60, Price = 90000, CategoryId = 1, IsDeleted = false },
                }
            };

            var returnCategory = new CategoryDto { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false };

            int id = 1;

            var categoriesRepositoryMock = new Mock<ICategoryRepository>();
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.GetByIdAsync(id)).Returns(Task.FromResult(category));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<CategoryDto>(category)).Returns(returnCategory);

            var categoriesController = new CategoriesController(mapperMock.Object, categoriesRepositoryMock.Object);

            // Act
            var result = await categoriesController.GetCategory(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as CategoryDto;
            Assert.NotNull(data);
            Assert.Equal(returnCategory, data);
        }

        [Fact]
        public async Task GetCategory_WhenNoCategory_ReturnNotFound()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Name = "Category 01",
                Description = "Category 01 Description",
                IsDeleted = false,
                Clothes = new List<Clothes>
                {
                    new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 2, Name = "Clothes 02", Description = "Clothes 02 Description", Stock = 40, Price = 60000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 3, Name = "Clothes 03", Description = "Clothes 03 Description", Stock = 60, Price = 90000, CategoryId = 1, IsDeleted = false },
                }
            };

            var returnCategory = new CategoryDto { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false };

            int id = 5;

            var categoriesRepositoryMock = new Mock<ICategoryRepository>();
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.GetByIdAsync(id));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<CategoryDto>(category)).Returns(returnCategory);

            var categoriesController = new CategoriesController(mapperMock.Object, categoriesRepositoryMock.Object);

            // Act
            var result = await categoriesController.GetCategory(id);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Category not found!", notFoundResult.Value);
        }

        [Fact]
        public async Task GetCategory_WhenException_ReturnBadRequest()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Name = "Category 01",
                Description = "Category 01 Description",
                IsDeleted = false,
                Clothes = new List<Clothes>
                {
                    new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 2, Name = "Clothes 02", Description = "Clothes 02 Description", Stock = 40, Price = 60000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 3, Name = "Clothes 03", Description = "Clothes 03 Description", Stock = 60, Price = 90000, CategoryId = 1, IsDeleted = false },
                }
            };

            var returnCategory = new CategoryDto { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false };

            int id = 1;

            var categoriesRepositoryMock = new Mock<ICategoryRepository>();
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.GetByIdAsync(id)).Throws(new Exception());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<CategoryDto>(category)).Returns(returnCategory);

            var categoriesController = new CategoriesController(mapperMock.Object, categoriesRepositoryMock.Object);

            // Act
            var result = await categoriesController.GetCategory(id);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong! Error: Exception of type 'System.Exception' was thrown.", badRequestResult.Value);
        }

        //----- POST CATEGORY -----//

        [Fact]
        public async Task PostCategory_WhenSuccess_ReturnOk()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Name = "Category 01",
                Description = "Category 01 Description",
                IsDeleted = false,
                Clothes = new List<Clothes>
                {
                    new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 2, Name = "Clothes 02", Description = "Clothes 02 Description", Stock = 40, Price = 60000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 3, Name = "Clothes 03", Description = "Clothes 03 Description", Stock = 60, Price = 90000, CategoryId = 1, IsDeleted = false },
                }
            };

            var returnCategory = new CategoryDto { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false };

            var categoriesRepositoryMock = new Mock<ICategoryRepository>();
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.PostAsync(category)).Returns(Task.FromResult(category));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Category>(returnCategory)).Returns(category);
            mapperMock.Setup(mapper => mapper.Map<CategoryDto>(category)).Returns(returnCategory);

            var categoriesController = new CategoriesController(mapperMock.Object, categoriesRepositoryMock.Object);

            // Act
            var result = await categoriesController.PostCategory(returnCategory);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as CategoryDto;
            Assert.NotNull(data);
            Assert.Equal(returnCategory, data);
        }

        [Fact]
        public async Task PostCategory_WhenException_ReturnBadRequest()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Name = "Category 01",
                Description = "Category 01 Description",
                IsDeleted = false,
                Clothes = new List<Clothes>
                {
                    new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 2, Name = "Clothes 02", Description = "Clothes 02 Description", Stock = 40, Price = 60000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 3, Name = "Clothes 03", Description = "Clothes 03 Description", Stock = 60, Price = 90000, CategoryId = 1, IsDeleted = false },
                }
            };

            var returnCategory = new CategoryDto { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false };

            var categoriesRepositoryMock = new Mock<ICategoryRepository>();
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.PostAsync(category)).Throws(new Exception());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Category>(returnCategory)).Returns(category);
            mapperMock.Setup(mapper => mapper.Map<CategoryDto>(category)).Returns(returnCategory);

            var categoriesController = new CategoriesController(mapperMock.Object, categoriesRepositoryMock.Object);

            // Act
            var result = await categoriesController.PostCategory(returnCategory);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong! Error: Exception of type 'System.Exception' was thrown.", badRequestResult.Value);
        }

        //----- PUT CATEGORY -----//

        [Fact]
        public async Task PutCategory_WhenSuccess_ReturnOk()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Name = "Category 01",
                Description = "Category 01 Description",
                IsDeleted = false,
                Clothes = new List<Clothes>
                {
                    new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 2, Name = "Clothes 02", Description = "Clothes 02 Description", Stock = 40, Price = 60000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 3, Name = "Clothes 03", Description = "Clothes 03 Description", Stock = 60, Price = 90000, CategoryId = 1, IsDeleted = false },
                }
            };

            var returnCategory = new CategoryDto { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false };

            var categoriesRepositoryMock = new Mock<ICategoryRepository>();
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.GetByIdAsync(category.Id)).Returns(Task.FromResult(category));
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.PutAsync(category)).Returns(Task.FromResult(category));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Category>(returnCategory)).Returns(category);
            mapperMock.Setup(mapper => mapper.Map<CategoryDto>(category)).Returns(returnCategory);

            var categoriesController = new CategoriesController(mapperMock.Object, categoriesRepositoryMock.Object);

            // Act
            var result = await categoriesController.PutCategory(returnCategory);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value as CategoryDto;
            Assert.NotNull(data);
            Assert.Equal(returnCategory, data);
        }

        [Fact]
        public async Task PutCategory_WhenNoCategory_ReturnNotFound()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Name = "Category 01",
                Description = "Category 01 Description",
                IsDeleted = false,
                Clothes = new List<Clothes>
                {
                    new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 2, Name = "Clothes 02", Description = "Clothes 02 Description", Stock = 40, Price = 60000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 3, Name = "Clothes 03", Description = "Clothes 03 Description", Stock = 60, Price = 90000, CategoryId = 1, IsDeleted = false },
                }
            };

            var returnCategory = new CategoryDto { Id = 3, Name = "Category 03", Description = "Category 03 Description", IsDeleted = false };

            var categoriesRepositoryMock = new Mock<ICategoryRepository>();
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.GetByIdAsync(category.Id));
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.PutAsync(category));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Category>(returnCategory)).Returns(category);
            mapperMock.Setup(mapper => mapper.Map<CategoryDto>(category)).Returns(returnCategory);

            var categoriesController = new CategoriesController(mapperMock.Object, categoriesRepositoryMock.Object);

            // Act
            var result = await categoriesController.PutCategory(returnCategory);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Category not found!", notFoundResult.Value);
        }

        [Fact]
        public async Task PutCategory_WhenException_ReturnBadRequest()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Name = "Category 01",
                Description = "Category 01 Description",
                IsDeleted = false,
                Clothes = new List<Clothes>
                {
                    new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 2, Name = "Clothes 02", Description = "Clothes 02 Description", Stock = 40, Price = 60000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 3, Name = "Clothes 03", Description = "Clothes 03 Description", Stock = 60, Price = 90000, CategoryId = 1, IsDeleted = false },
                }
            };

            var returnCategory = new CategoryDto { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false };

            var categoriesRepositoryMock = new Mock<ICategoryRepository>();
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.GetByIdAsync(category.Id)).Returns(Task.FromResult(category));
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.PutAsync(category)).Throws(new Exception());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Category>(returnCategory)).Returns(category);
            mapperMock.Setup(mapper => mapper.Map<CategoryDto>(category)).Returns(returnCategory);

            var categoriesController = new CategoriesController(mapperMock.Object, categoriesRepositoryMock.Object);

            // Act
            var result = await categoriesController.PutCategory(returnCategory);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong! Error: Exception of type 'System.Exception' was thrown.", badRequestResult.Value);
        }

        //----- DELETE CATEGORY -----//

        public async Task DeleteCategory_WhenSuccess_ReturnOk()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Name = "Category 01",
                Description = "Category 01 Description",
                IsDeleted = false,
                Clothes = new List<Clothes>
                {
                    new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 2, Name = "Clothes 02", Description = "Clothes 02 Description", Stock = 40, Price = 60000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 3, Name = "Clothes 03", Description = "Clothes 03 Description", Stock = 60, Price = 90000, CategoryId = 1, IsDeleted = false },
                }
            };

            var returnCategory = new CategoryDto { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false };

            int id = 1;

            var categoriesRepositoryMock = new Mock<ICategoryRepository>();
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.GetByIdAsync(id)).Returns(Task.FromResult(category));
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.DeleteAsync(id));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<CategoryDto>(category)).Returns(returnCategory);

            var categoriesController = new CategoriesController(mapperMock.Object, categoriesRepositoryMock.Object);

            // Act
            var result = await categoriesController.DeleteCategory(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Category deleted!", okResult.Value);
        }

        public async Task DeleteCategory_WhenNoCategory_ReturnNotFound()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Name = "Category 01",
                Description = "Category 01 Description",
                IsDeleted = false,
                Clothes = new List<Clothes>
                {
                    new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 2, Name = "Clothes 02", Description = "Clothes 02 Description", Stock = 40, Price = 60000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 3, Name = "Clothes 03", Description = "Clothes 03 Description", Stock = 60, Price = 90000, CategoryId = 1, IsDeleted = false },
                }
            };

            var returnCategory = new CategoryDto { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false };

            int id = 3;

            var categoriesRepositoryMock = new Mock<ICategoryRepository>();
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.GetByIdAsync(id));
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.DeleteAsync(id));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<CategoryDto>(category)).Returns(returnCategory);

            var categoriesController = new CategoriesController(mapperMock.Object, categoriesRepositoryMock.Object);

            // Act
            var result = await categoriesController.DeleteCategory(id);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Category not found!", notFoundResult.Value);
        }

        public async Task DeleteCategory_WhenException_ReturnBadRequest()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Name = "Category 01",
                Description = "Category 01 Description",
                IsDeleted = false,
                Clothes = new List<Clothes>
                {
                    new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 2, Name = "Clothes 02", Description = "Clothes 02 Description", Stock = 40, Price = 60000, CategoryId = 1, IsDeleted = false },
                    new Clothes { ID = 3, Name = "Clothes 03", Description = "Clothes 03 Description", Stock = 60, Price = 90000, CategoryId = 1, IsDeleted = false },
                }
            };

            var returnCategory = new CategoryDto { Id = 1, Name = "Category 01", Description = "Category 01 Description", IsDeleted = false };

            int id = 1;

            var categoriesRepositoryMock = new Mock<ICategoryRepository>();
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.GetByIdAsync(id)).Returns(Task.FromResult(category));
            categoriesRepositoryMock.Setup(categoriesRepository => categoriesRepository.DeleteAsync(id));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<CategoryDto>(category)).Returns(returnCategory);

            var categoriesController = new CategoriesController(mapperMock.Object, categoriesRepositoryMock.Object);

            // Act
            var result = await categoriesController.DeleteCategory(id);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong! Error: Exception of type 'System.Exception' was thrown.", badRequestResult.Value);
        }
    }
}