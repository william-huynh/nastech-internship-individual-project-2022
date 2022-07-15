using AutoMapper;
using ClothesShop.API.Controllers;
using ClothesShop.API.Interfaces;
using ClothesShop.API.Models;
using ClothesShop.API.Profiles;
using ClothesShop.SharedVMs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit.Abstractions;

namespace ClothesShop.Test
{
    public class TestImagesController
    {
        private IMapper _mapper;
        private readonly ITestOutputHelper _output;

        public TestImagesController(ITestOutputHelper output)
        {
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new ImagesProfiles()));
            _mapper = mockMapper.CreateMapper();
            _output = output;
        }

        //----- GET IMAGES -----//

        [Fact]
        public async Task GetImages_WhenSuccess_ReturnOk()
        {
            // Arrange
            var images = new List<Image>
            {
                new Image {
                    Id = 1,
                    URL = "dummy-image.jpg",
                    IsDeleted = false,
                    ClothesId = 1,
                    Clothes = new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false }
                },
                new Image {
                    Id = 2,
                    URL = "dummy-image.jpg",
                    IsDeleted = false,
                    ClothesId = 2,
                    Clothes = new Clothes { ID = 2, Name = "Clothes 02", Description = "Clothes 02 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false }
                },
            };

            var returnImages = new List<ImageDto>
            {
                new ImageDto { Id = 1, URL = "dummy-image.jpg", File = null, IsDeleted = false, ClothesId = 1 },
                new ImageDto { Id = 2, URL = "dummy-image.jpg", File = null, IsDeleted = false, ClothesId = 2 },
            };

            var imagesRepositoryMock = new Mock<IImageRepository>();
            imagesRepositoryMock.Setup(imagesRepository => imagesRepository.GetAsync()).Returns(Task.FromResult(images));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<ImageDto>>(images)).Returns(returnImages);

            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            //webHostEnvironmentMock.Setup(webHostEnvironment => webHostEnvironment.WebRootPath).Returns()

            var imagesController = new ImagesController(mapperMock.Object, imagesRepositoryMock.Object, webHostEnvironmentMock.Object);

            // Act
            var result = await imagesController.GetImages();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value;
            Assert.NotNull(data);
            Assert.Equal(returnImages, data);
        }

        [Fact]
        public async Task GetImages_WhenNoImages_ReturnNotFound()
        {
            // Arrange
            var images = new List<Image>
            {
                new Image {
                    Id = 1,
                    URL = "dummy-image.jpg",
                    IsDeleted = false,
                    ClothesId = 1,
                    Clothes = new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false }
                },
                new Image {
                    Id = 2,
                    URL = "dummy-image.jpg",
                    IsDeleted = false,
                    ClothesId = 2,
                    Clothes = new Clothes { ID = 2, Name = "Clothes 02", Description = "Clothes 02 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false }
                },
            };

            var returnImages = new List<ImageDto>
            {
                new ImageDto { Id = 1, URL = "dummy-image.jpg", File = null, IsDeleted = false, ClothesId = 1 },
                new ImageDto { Id = 2, URL = "dummy-image.jpg", File = null, IsDeleted = false, ClothesId = 2 },
            };

            var imagesRepositoryMock = new Mock<IImageRepository>();
            imagesRepositoryMock.Setup(imagesRepository => imagesRepository.GetAsync()).Returns(Task.FromResult(new List<Image>()));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<ImageDto>>(images)).Returns(returnImages);

            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            //webHostEnvironmentMock.Setup(webHostEnvironment => webHostEnvironment.WebRootPath).Returns()

            var imagesController = new ImagesController(mapperMock.Object, imagesRepositoryMock.Object, webHostEnvironmentMock.Object);

            // Act
            var result = await imagesController.GetImages();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Images empty!", notFoundResult.Value);
        }

        [Fact]
        public async Task GetImages_WhenException_ReturnBadRequest()
        {
            // Arrange
            var images = new List<Image>
            {
                new Image {
                    Id = 1,
                    URL = "dummy-image.jpg",
                    IsDeleted = false,
                    ClothesId = 1,
                    Clothes = new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false }
                },
                new Image {
                    Id = 2,
                    URL = "dummy-image.jpg",
                    IsDeleted = false,
                    ClothesId = 2,
                    Clothes = new Clothes { ID = 2, Name = "Clothes 02", Description = "Clothes 02 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false }
                },
            };

            var returnImages = new List<ImageDto>
            {
                new ImageDto { Id = 1, URL = "dummy-image.jpg", File = null, IsDeleted = false, ClothesId = 1 },
                new ImageDto { Id = 2, URL = "dummy-image.jpg", File = null, IsDeleted = false, ClothesId = 2 },
            };

            var imagesRepositoryMock = new Mock<IImageRepository>();
            imagesRepositoryMock.Setup(imagesRepository => imagesRepository.GetAsync()).Throws(new Exception());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<ImageDto>>(images)).Returns(returnImages);

            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            //webHostEnvironmentMock.Setup(webHostEnvironment => webHostEnvironment.WebRootPath).Returns()

            var imagesController = new ImagesController(mapperMock.Object, imagesRepositoryMock.Object, webHostEnvironmentMock.Object);

            // Act
            var result = await imagesController.GetImages();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong! Error: Exception of type 'System.Exception' was thrown.", badRequestResult.Value);
        }

        //----- GET IMAGE -----//

        [Fact]
        public async Task GetImage_WhenSuccess_ReturnOk()
        {
            // Arrange
            var image = new Image
            {
                Id = 1,
                URL = "dummy-image.jpg",
                IsDeleted = false,
                ClothesId = 1,
                Clothes = new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false }
            };

            var returnImage = new ImageDto { Id = 1, URL = "dummy-image.jpg", File = null, IsDeleted = false, ClothesId = 1 };

            int id = 1;

            var imagesRepositoryMock = new Mock<IImageRepository>();
            imagesRepositoryMock.Setup(imagesRepository => imagesRepository.GetByIdAsync(id)).Returns(Task.FromResult(image));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<ImageDto>(image)).Returns(returnImage);

            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            //webHostEnvironmentMock.Setup(webHostEnvironment => webHostEnvironment.WebRootPath).Returns()

            var imagesController = new ImagesController(mapperMock.Object, imagesRepositoryMock.Object, webHostEnvironmentMock.Object);

            // Act
            var result = await imagesController.GetImage(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value;
            Assert.NotNull(data);
            Assert.Equal(returnImage, data);
        }

        [Fact]
        public async Task GetImage_WhenNoImage_ReturnNotFound()
        {
            // Arrange
            var image = new Image
            {
                Id = 1,
                URL = "dummy-image.jpg",
                IsDeleted = false,
                ClothesId = 1,
                Clothes = new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false }
            };

            var returnImage = new ImageDto { Id = 1, URL = "dummy-image.jpg", File = null, IsDeleted = false, ClothesId = 1 };

            int id = 3;

            var imagesRepositoryMock = new Mock<IImageRepository>();
            imagesRepositoryMock.Setup(imagesRepository => imagesRepository.GetByIdAsync(id));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<ImageDto>(image)).Returns(returnImage);

            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            //webHostEnvironmentMock.Setup(webHostEnvironment => webHostEnvironment.WebRootPath).Returns()

            var imagesController = new ImagesController(mapperMock.Object, imagesRepositoryMock.Object, webHostEnvironmentMock.Object);

            // Act
            var result = await imagesController.GetImage(id);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Image not found!", notFoundResult.Value);
        }

        [Fact]
        public async Task GetImage_WhenException_ReturnBadRequest()
        {
            // Arrange
            var image = new Image
            {
                Id = 1,
                URL = "dummy-image.jpg",
                IsDeleted = false,
                ClothesId = 1,
                Clothes = new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false }
            };

            var returnImage = new ImageDto { Id = 1, URL = "dummy-image.jpg", File = null, IsDeleted = false, ClothesId = 1 };

            int id = 1;

            var imagesRepositoryMock = new Mock<IImageRepository>();
            imagesRepositoryMock.Setup(imagesRepository => imagesRepository.GetByIdAsync(id)).Throws(new Exception());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<ImageDto>(image)).Returns(returnImage);

            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            //webHostEnvironmentMock.Setup(webHostEnvironment => webHostEnvironment.WebRootPath).Returns()

            var imagesController = new ImagesController(mapperMock.Object, imagesRepositoryMock.Object, webHostEnvironmentMock.Object);

            // Act
            var result = await imagesController.GetImage(id);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong! Error: Exception of type 'System.Exception' was thrown.", badRequestResult.Value);
        }

        //----- POST IMAGE -----//

        [Fact]
        public async Task PostImage_WhenSuccess_ReturnOk()
        {
            // Arrange
            var image = new Image
            {
                Id = 1,
                URL = "dummy-image.jpg",
                IsDeleted = false,
                ClothesId = 1,
                Clothes = new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false }
            };

            var returnImage = new ImageDto { Id = 1, URL = "dummy-image.jpg", File = null, IsDeleted = false, ClothesId = 1 };

            var imagesRepositoryMock = new Mock<IImageRepository>();
            imagesRepositoryMock.Setup(imagesRepository => imagesRepository.PostAsync(image)).Returns(Task.FromResult(image));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<ImageDto>(image)).Returns(returnImage);
            mapperMock.Setup(mapper => mapper.Map<Image>(returnImage)).Returns(image);

            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            //webHostEnvironmentMock.Setup(webHostEnvironment => webHostEnvironment.WebRootPath).Returns()

            var imagesController = new ImagesController(mapperMock.Object, imagesRepositoryMock.Object, webHostEnvironmentMock.Object);

            // Act
            var result = await imagesController.PostImage(returnImage);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value;
            Assert.NotNull(data);
            Assert.Equal(returnImage, data);
        }

        [Fact]
        public async Task PostImage_WhenException_ReturnBadRequest()
        {
            // Arrange
            var image = new Image
            {
                Id = 1,
                URL = "dummy-image.jpg",
                IsDeleted = false,
                ClothesId = 1,
                Clothes = new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false }
            };

            var returnImage = new ImageDto { Id = 1, URL = "dummy-image.jpg", File = null, IsDeleted = false, ClothesId = 1 };

            var imagesRepositoryMock = new Mock<IImageRepository>();
            imagesRepositoryMock.Setup(imagesRepository => imagesRepository.PostAsync(image)).Throws(new Exception());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<ImageDto>(image)).Returns(returnImage);
            mapperMock.Setup(mapper => mapper.Map<Image>(returnImage)).Returns(image);

            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            //webHostEnvironmentMock.Setup(webHostEnvironment => webHostEnvironment.WebRootPath).Returns()

            var imagesController = new ImagesController(mapperMock.Object, imagesRepositoryMock.Object, webHostEnvironmentMock.Object);

            // Act
            var result = await imagesController.PostImage(returnImage);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong! Error: Object reference not set to an instance of an object.", badRequestResult.Value);
        }

        //----- PUT IMAGE -----//

        [Fact]
        public async Task PutImage_WhenSuccess_ReturnOk()
        {
            // Arrange
            var image = new Image
            {
                Id = 1,
                URL = "dummy-image.jpg",
                IsDeleted = false,
                ClothesId = 1,
                Clothes = new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false }
            };

            var returnImage = new ImageDto { Id = 1, URL = "dummy-image.jpg", File = null, IsDeleted = false, ClothesId = 1 };

            int id = 1;

            var imagesRepositoryMock = new Mock<IImageRepository>();
            imagesRepositoryMock.Setup(imagesRepository => imagesRepository.PutAsync(id, image)).Returns(Task.FromResult(image));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<ImageDto>(image)).Returns(returnImage);
            mapperMock.Setup(mapper => mapper.Map<Image>(returnImage)).Returns(image);

            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            //webHostEnvironmentMock.Setup(webHostEnvironment => webHostEnvironment.WebRootPath).Returns()

            var imagesController = new ImagesController(mapperMock.Object, imagesRepositoryMock.Object, webHostEnvironmentMock.Object);

            // Act
            var result = await imagesController.PutImage(id, returnImage);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = okResult.Value;
            Assert.NotNull(data);
            Assert.Equal(returnImage, data);
        }

        [Fact]
        public async Task PutImage_WhenException_ReturnBadRequest()
        {
            // Arrange
            var image = new Image
            {
                Id = 1,
                URL = "dummy-image.jpg",
                IsDeleted = false,
                ClothesId = 1,
                Clothes = new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false }
            };

            var returnImage = new ImageDto { Id = 1, URL = "dummy-image.jpg", File = null, IsDeleted = false, ClothesId = 1 };

            int id = 1;

            var imagesRepositoryMock = new Mock<IImageRepository>();
            imagesRepositoryMock.Setup(imagesRepository => imagesRepository.PutAsync(id, image)).Throws(new Exception());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<ImageDto>(image)).Returns(returnImage);
            mapperMock.Setup(mapper => mapper.Map<Image>(returnImage)).Returns(image);

            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            //webHostEnvironmentMock.Setup(webHostEnvironment => webHostEnvironment.WebRootPath).Returns()

            var imagesController = new ImagesController(mapperMock.Object, imagesRepositoryMock.Object, webHostEnvironmentMock.Object);

            // Act
            var result = await imagesController.PutImage(id, returnImage);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong! Error: Object reference not set to an instance of an object.", badRequestResult.Value);
        }

        //----- DELETE IMAGE -----//

        [Fact]
        public async Task DeleteImage_WhenSuccess_ReturnOk()
        {
            // Arrange
            var image = new Image
            {
                Id = 1,
                URL = "dummy-image.jpg",
                IsDeleted = false,
                ClothesId = 1,
                Clothes = new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false }
            };

            var returnImage = new ImageDto { Id = 1, URL = "dummy-image.jpg", File = null, IsDeleted = false, ClothesId = 1 };

            int id = 1;

            var imagesRepositoryMock = new Mock<IImageRepository>();
            imagesRepositoryMock.Setup(imagesRepository => imagesRepository.DeleteAsync(id)).Returns(Task.FromResult(image));

            var mapperMock = new Mock<IMapper>();

            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            //webHostEnvironmentMock.Setup(webHostEnvironment => webHostEnvironment.WebRootPath).Returns()

            var imagesController = new ImagesController(mapperMock.Object, imagesRepositoryMock.Object, webHostEnvironmentMock.Object);

            // Act
            var result = await imagesController.DeleteImage(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Image deleted!", okResult.Value);
        }

        [Fact]
        public async Task DeleteImage_WhenNoImage_ReturnNotFound()
        {
            // Arrange
            var image = new Image
            {
                Id = 1,
                URL = "dummy-image.jpg",
                IsDeleted = false,
                ClothesId = 1,
                Clothes = new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false }
            };

            var returnImage = new ImageDto { Id = 1, URL = "dummy-image.jpg", File = null, IsDeleted = false, ClothesId = 1 };

            int id = 3;

            var imagesRepositoryMock = new Mock<IImageRepository>();
            imagesRepositoryMock.Setup(imagesRepository => imagesRepository.DeleteAsync(id));

            var mapperMock = new Mock<IMapper>();

            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            //webHostEnvironmentMock.Setup(webHostEnvironment => webHostEnvironment.WebRootPath).Returns()

            var imagesController = new ImagesController(mapperMock.Object, imagesRepositoryMock.Object, webHostEnvironmentMock.Object);

            // Act
            var result = await imagesController.DeleteImage(id);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Image not found!", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteImage_WhenException_ReturnBadRequest()
        {
            // Arrange
            var image = new Image
            {
                Id = 1,
                URL = "dummy-image.jpg",
                IsDeleted = false,
                ClothesId = 1,
                Clothes = new Clothes { ID = 1, Name = "Clothes 01", Description = "Clothes 01 Description", Stock = 20, Price = 30000, CategoryId = 1, IsDeleted = false }
            };

            var returnImage = new ImageDto { Id = 1, URL = "dummy-image.jpg", File = null, IsDeleted = false, ClothesId = 1 };

            int id = 1;

            var imagesRepositoryMock = new Mock<IImageRepository>();
            imagesRepositoryMock.Setup(imagesRepository => imagesRepository.DeleteAsync(id)).Throws(new Exception());

            var mapperMock = new Mock<IMapper>();

            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            //webHostEnvironmentMock.Setup(webHostEnvironment => webHostEnvironment.WebRootPath).Returns()

            var imagesController = new ImagesController(mapperMock.Object, imagesRepositoryMock.Object, webHostEnvironmentMock.Object);

            // Act
            var result = await imagesController.DeleteImage(id);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Image not found!", badRequestResult.Value);
        }
    }
}
