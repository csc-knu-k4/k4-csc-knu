using Microsoft.EntityFrameworkCore;
using Osvita.Tests.Integration.Helpers;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaBLL.Services;
using OsvitaDAL.Data;
using OsvitaDAL.Interfaces;

namespace Osvita.Tests.Integration.ServicesTests
{
	public class TopicServiceTests
	{
        private OsvitaDbContext context;
        private IUnitOfWork unitOfWork;
        private ITopicService topicService;

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
            topicService = new TopicService(unitOfWork, UnitTestHelper.GetAutoMapperProfile());
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
        public async Task GetAllAsync_TopicsExist_ShouldReturnAllTopicsModels()
        {
            //Arrange
            var expected = new List<TopicModel>()
            {
                new TopicModel { Id = 1, Title = "Topic1", ChapterId = 1, MaterialsIds = new List<int> { 1 }, OrderPosition = 1 },
                new TopicModel { Id = 2, Title = "Topic2", ChapterId = 2, MaterialsIds = new List<int> { 2, 3 }, OrderPosition = 2 },
                new TopicModel { Id = 3, Title = "Topic3", ChapterId = 2, MaterialsIds = new List<int>(), OrderPosition = 3 }
            };

            //Act
            var actual = await topicService.GetAllAsync();

            //Assert
            Assert.That(actual, Is.EquivalentTo(expected).Using(new TopicModelEqualityComparer()));
        }

        [Test]
        public async Task GetByIdAsync_TopicExist_ShouldReturnTopicModel()
        {
            //Arrange
            var expected = new TopicModel { Id = 1, Title = "Topic1", ChapterId = 1, MaterialsIds = new List<int> { 1 }, OrderPosition = 1 };

            //Act
            var actual = await topicService.GetByIdAsync(1);

            //Asset
            Assert.That(actual, Is.EqualTo(expected).Using(new TopicModelEqualityComparer()));
        }

        [Test]
        public async Task DeleteByIdAsync_TopicIsExist_ShouldDeleteTopic()
        {
            //Arrange
            var topicModel = await topicService.GetByIdAsync(1);

            //Act
            await topicService.DeleteByIdAsync(1);

            //Assert
            var actual = await topicService.GetAllAsync();
            Assert.That(actual, Has.No.Member(topicModel).Using(new TopicModelEqualityComparer()));
        }

        [Test]
        public async Task AddAsync_TopicIsNotExist_ShouldAddTopic()
        {
            //Arrange
            var newTopicModel = new TopicModel { Id = 4, Title = "Topic4", ChapterId = 1, MaterialsIds = new List<int>(), OrderPosition = 1 };

            //Act
            await topicService.AddAsync(newTopicModel);

            //Assert
            var actual = await topicService.GetAllAsync();
            Assert.That(actual, Has.Member(newTopicModel).Using(new TopicModelEqualityComparer()));
        }
    }
}

