using Microsoft.EntityFrameworkCore;
using Osvita.Tests.Integration.Helpers;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaBLL.Services;
using OsvitaDAL.Data;
using OsvitaDAL.Interfaces;

namespace Osvita.Tests.Integration.ServicesTests
{
	public class ContentBlockServiceTests
	{
        private OsvitaDbContext context;
        private IUnitOfWork unitOfWork;
        private IContentBlockService contentBlockService;

        [SetUp]
        public void Setup()
        {
            context = new OsvitaDbContext(UnitTestHelper.GetUnitTestDbContextOptions());

            if (context.Database.IsInMemory())
            {
                context.Database.EnsureDeleted();
                UnitTestHelper.SeedData(context);
            }
            unitOfWork = new UnitOfWork(context);
            contentBlockService = new ContentBlockService(unitOfWork, UnitTestHelper.GetAutoMapperProfile());
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            if (context.Database.IsInMemory())
            {
                context.Database.EnsureDeleted();
            }
        }

        [Test]
        public async Task GetAllAsync_ContentBlocksExist_ShouldReturnAllContentBlocksModels()
        {
            //Arrange
            var expected = new List<ContentBlockModel>()
            {
                new ContentBlockModel { Id = 1, Title = "ContentBlock1", MaterialId = 1, OrderPosition = 1, ContentBlockModelType = ContentBlockModelType.TextBlock, Value = "Text1" },
                new ContentBlockModel { Id = 2, Title = "ContentBlock2", MaterialId = 2, OrderPosition = 2, ContentBlockModelType = ContentBlockModelType.TextBlock, Value = "Text2" },
                new ContentBlockModel { Id = 3, Title = "ContentBlock3", MaterialId = 2, OrderPosition = 3, ContentBlockModelType = ContentBlockModelType.ImageBlock, Value = "Image2" }
            };

            //Act
            var actual = await contentBlockService.GetAllAsync();

            //Assert
            Assert.That(actual, Is.EquivalentTo(expected).Using(new ContentBlockModelEqualityComparer()));
        }

        [Test]
        public async Task GetByIdAsync_ContentBlockExist_ShouldReturnContentBlockModel()
        {
            //Arrange
            var expected = new ContentBlockModel { Id = 1, Title = "ContentBlock1", MaterialId = 1, OrderPosition = 1, ContentBlockModelType = ContentBlockModelType.TextBlock, Value = "Text1" };

            //Act
            var actual = await contentBlockService.GetByIdAsync(1);

            //Asset
            Assert.That(actual, Is.EqualTo(expected).Using(new ContentBlockModelEqualityComparer()));
        }

        [Test]
        public async Task DeleteByIdAsync_ContentBlockIsExist_ShouldDeleteContentBlock()
        {
            //Arrange
            var contentBlockModel = await contentBlockService.GetByIdAsync(1);

            //Act
            await contentBlockService.DeleteByIdAsync(1);

            //Assert
            var actual = await contentBlockService.GetAllAsync();
            Assert.That(actual, Has.No.Member(contentBlockModel).Using(new ContentBlockModelEqualityComparer()));
        }

        [Test]
        public async Task AddAsync_ContentBlockIsNotExist_ShouldAddContentBlock()
        {
            //Arrange
            var newContentBlockModel = new ContentBlockModel { Id = 4, Title = "ContentBlock4", MaterialId = 1, OrderPosition = 1, ContentBlockModelType = ContentBlockModelType.ImageBlock, Value = "Image4" };

            //Act
            await contentBlockService.AddAsync(newContentBlockModel);

            //Assert
            var actual = await contentBlockService.GetAllAsync();
            Assert.That(actual, Has.Member(newContentBlockModel).Using(new ContentBlockModelEqualityComparer()));
        }
    }
}

