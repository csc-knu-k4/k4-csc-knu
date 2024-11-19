using Microsoft.EntityFrameworkCore;
using Osvita.Tests.Integration.Helpers;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaBLL.Services;
using OsvitaDAL.Data;
using OsvitaDAL.Interfaces;

namespace Osvita.Tests.Integration.ServicesTests
{
	public class ChapterServiceTests
	{
        private OsvitaDbContext context;
        private IUnitOfWork unitOfWork;
        private IChapterService chapterService;

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
            chapterService = new ChapterService(unitOfWork, UnitTestHelper.GetAutoMapperProfile());
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
        public async Task GetAllAsync_ChaptersExist_ShouldReturnAllChaptersModels()
        {
            //Arrange
            var expected = new List<ChapterModel>()
            {
                new ChapterModel { Id = 1, Title = "Chapter1", SubjectId = 1, TopicsIds = new List<int> { 1 }, OrderPosition = 1 },
                new ChapterModel { Id = 2, Title = "Chapter2", SubjectId = 2, TopicsIds = new List<int> { 2, 3 }, OrderPosition = 2 },
                new ChapterModel { Id = 3, Title = "Chapter3", SubjectId = 2, TopicsIds = new List<int>(), OrderPosition = 3 }
            };

            //Act
            var actual = await chapterService.GetAllAsync();

            //Assert
            Assert.That(actual, Is.EquivalentTo(expected).Using(new ChapterModelEqualityComparer()));
        }

        [Test]
        public async Task GetByIdAsync_ChapterExist_ShouldReturnChapterModel()
        {
            //Arrange
            var expected = new ChapterModel { Id = 1, Title = "Chapter1", SubjectId = 1, TopicsIds = new List<int> { 1 }, OrderPosition = 1 };

            //Act
            var actual = await chapterService.GetByIdAsync(1);

            //Asset
            Assert.That(actual, Is.EqualTo(expected).Using(new ChapterModelEqualityComparer()));
        }

        [Test]
        public async Task DeleteByIdAsync_ChapterIsExist_ShouldDeleteChapter()
        {
            //Arrange
            var chapterModel = await chapterService.GetByIdAsync(1);

            //Act
            await chapterService.DeleteByIdAsync(1);

            //Assert
            var actual = await chapterService.GetAllAsync();
            Assert.That(actual, Has.No.Member(chapterModel).Using(new ChapterModelEqualityComparer()));
        }

        [Test]
        public async Task AddAsync_ChapterIsNotExist_ShouldAddChapter()
        {
            //Arrange
            var newChapterModel = new ChapterModel { Id = 4, Title = "Chapter4", SubjectId = 1, TopicsIds = new List<int>(), OrderPosition = 1 };

            //Act
            await chapterService.AddAsync(newChapterModel);

            //Assert
            var actual = await chapterService.GetAllAsync();
            Assert.That(actual, Has.Member(newChapterModel).Using(new ChapterModelEqualityComparer()));
        }
    }
}

