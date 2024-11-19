using System;
using Microsoft.EntityFrameworkCore;
using Osvita.Tests.Integration.Helpers;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;
using OsvitaDAL.Repositories;

namespace Osvita.Tests.Integration.RepositoriesTests
{
	public class ContentBlockRepositoryTests
	{
        private OsvitaDbContext context;
        private ContentBlockRepository contentBlockRepository;

        [SetUp]
        public void Setup()
        {
            context = new OsvitaDbContext(UnitTestHelper.GetUnitTestDbContextOptions());

            if (context.Database.IsInMemory())
            {
                context.Database.EnsureDeleted();
                UnitTestHelper.SeedData(context);
            }

            contentBlockRepository = new ContentBlockRepository(context);
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
        public async Task GetAllAsync_ContentBlocksAreExist_ShouldReturnAllContentBlocks()
        {
            //Arrange
            var expected = new List<ContentBlock>
            {
                new ContentBlock { Id = 1, Title = "ContentBlock1", MaterialId = 1, OrderPosition = 1, ContentType = ContentType.TextBlock, Value = "Text1" },
                new ContentBlock { Id = 2, Title = "ContentBlock2", MaterialId = 2, OrderPosition = 2, ContentType = ContentType.TextBlock, Value = "Text2" },
                new ContentBlock { Id = 3, Title = "ContentBlock3", MaterialId = 2, OrderPosition = 3, ContentType = ContentType.ImageBlock, Value = "Image2" }
            };

            //Act
            var actual = await contentBlockRepository.GetAllAsync();

            //Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new ContentBlockEqualityComparer()));
        }

        [Test]
        public async Task GetByIdAsync_ContentBlockIsExist_ShouldReturnContentBlock()
        {
            //Arrange
            var expected = new ContentBlock { Id = 2, Title = "ContentBlock2", MaterialId = 2, OrderPosition = 2, ContentType = ContentType.TextBlock, Value = "Text2" };

            //Act
            var actual = await contentBlockRepository.GetByIdAsync(2);

            //Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new ContentBlockEqualityComparer()));
        }

        [Test]
        public async Task AddAsync_ContentBlockIsNotExist_ShouldAddContentBlock()
        {
            //Arrange
            var newContentBlock = new ContentBlock { Id = 4, Title = "ContentBlock4", MaterialId = 2, OrderPosition = 2, ContentType = ContentType.TextBlock, Value = "Text4" };

            //Act
            await contentBlockRepository.AddAsync(newContentBlock);
            await context.SaveChangesAsync();

            //Assert
            var actual = await contentBlockRepository.GetAllAsync();
            Assert.That(actual, Has.Member(newContentBlock).Using(new ContentBlockEqualityComparer()));
        }

        [Test]
        public async Task DeleteByIdAsync_ContentBlockIsExist_ShouldDeleteContentBlock()
        {
            //Arrange
            var contentBlock = await contentBlockRepository.GetByIdAsync(1);

            //Act
            await contentBlockRepository.DeleteByIdAsync(contentBlock.Id);
            await context.SaveChangesAsync();

            //Assert
            var actual = await contentBlockRepository.GetAllAsync();
            Assert.That(actual, Has.No.Member(contentBlock).Using(new ContentBlockEqualityComparer()));
        }

        [Test]
        public async Task UpdateAsync_TopicIsExist_ShouldUpdateTopic()
        {
            //Arrange
            var contentBlock = await contentBlockRepository.GetByIdAsync(1);
            contentBlock.Value = "NewText1";

            //Act
            await contentBlockRepository.UpdateAsync(contentBlock);
            await context.SaveChangesAsync();

            //Assert
            var actual = await contentBlockRepository.GetAllAsync();
            var updatedContentBlock = new ContentBlock { Id = 1, Title = "ContentBlock1", MaterialId = 1, OrderPosition = 1, ContentType = ContentType.TextBlock, Value = "NewText1" };
            Assert.That(actual, Has.Member(updatedContentBlock).Using(new ContentBlockEqualityComparer()));
        }
    }
}

