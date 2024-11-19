using Microsoft.EntityFrameworkCore;
using Osvita.Tests.Helpers;
using Osvita.Tests.Integration.Helpers;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Repositories;

namespace Osvita.Tests.Integration.RepositoriesTests
{
    [TestFixture]
	public class ChapterRepositoryTests
	{
        private OsvitaDbContext context;
        private ChapterRepository chapterRepository;

        [SetUp]
        public void Setup()
        {
            context = new OsvitaDbContext(UnitTestHelper.GetUnitTestDbContextOptions());

            if (context.Database.IsInMemory())
            {
                context.Database.EnsureDeleted();
                UnitTestHelper.SeedData(context);
            }

            chapterRepository = new ChapterRepository(context);
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
        public async Task GetAllAsync_ChaptersAreExist_ShouldReturnAllChapters()
        {
            //Arrange
            var expected = new List<Chapter>
            {
                new Chapter { Id = 1, Title = "Chapter1", SubjectId = 1, OrderPosition = 1 },
                new Chapter { Id = 2, Title = "Chapter2", SubjectId = 2, OrderPosition = 2 },
                new Chapter { Id = 3, Title = "Chapter3", SubjectId = 2, OrderPosition = 3 }
            };

            //Act
            var actual = await chapterRepository.GetAllAsync();

            //Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new ChapterEqualityComparer()));
        }

        [Test]
        public async Task GetByIdAsync_ChapterIsExist_ShouldReturnChapter()
        {
            //Arrange
            var expected = new Chapter { Id = 2, Title = "Chapter2", SubjectId = 2, OrderPosition = 2 };

            //Act
            var actual = await chapterRepository.GetByIdAsync(2);

            //Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new ChapterEqualityComparer()));
        }

        [Test]
        public async Task AddAsync_ChapterIsNotExist_ShouldAddChapter()
        {
            //Arrange
            var newChapter = new Chapter { Id = 4, Title = "Chapter4", SubjectId = 3, OrderPosition = 4 };

            //Act
            await chapterRepository.AddAsync(newChapter);
            await context.SaveChangesAsync();

            //Assert
            var actual = await chapterRepository.GetAllAsync();
            Assert.That(actual, Has.Member(newChapter).Using(new ChapterEqualityComparer()));
        }

        [Test]
        public async Task DeleteByIdAsync_ChapterIsExist_ShouldDeleteChapter()
        {
            //Arrange
            var chapter = await chapterRepository.GetByIdAsync(1);

            //Act
            await chapterRepository.DeleteByIdAsync(chapter.Id);
            await context.SaveChangesAsync();

            //Assert
            var actual = await chapterRepository.GetAllAsync();
            Assert.That(actual, Has.No.Member(chapter).Using(new ChapterEqualityComparer()));
        }

        [Test]
        public async Task UpdateAsync_ChapterIsExist_ShouldUpdateChapter()
        {
            //Arrange
            var chapter = await chapterRepository.GetByIdAsync(1);
            chapter.Title = "NewTitle1";

            //Act
            await chapterRepository.UpdateAsync(chapter);
            await context.SaveChangesAsync();

            //Assert
            var actual = await chapterRepository.GetAllAsync();
            var updatedChapter = new Chapter { Id = 1, Title = "NewTitle1", SubjectId = 1, OrderPosition = 1 };
            Assert.That(actual, Has.Member(updatedChapter).Using(new ChapterEqualityComparer()));
        }
    }
}

