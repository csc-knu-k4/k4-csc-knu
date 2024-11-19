using System;
using Microsoft.EntityFrameworkCore;
using Osvita.Tests.Integration.Helpers;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;
using OsvitaDAL.Repositories;

namespace Osvita.Tests.Integration.RepositoriesTests
{
    [TestFixture]
	public class TopicRepositoryTests
	{
        private OsvitaDbContext context;
        private TopicRepository topicRepository;

        [SetUp]
        public void Setup()
        {
            context = new OsvitaDbContext(UnitTestHelper.GetUnitTestDbContextOptions());

            if (context.Database.IsInMemory())
            {
                context.Database.EnsureDeleted();
                UnitTestHelper.SeedData(context);
            }

            topicRepository = new TopicRepository(context);
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
        public async Task GetAllAsync_TopicsAreExist_ShouldReturnAllTopics()
        {
            //Arrange
            var expected = new List<Topic>
            {
                new Topic { Id = 1, Title = "Topic1", ChapterId = 1, OrderPosition = 1 },
                new Topic { Id = 2, Title = "Topic2", ChapterId = 2, OrderPosition = 2 },
                new Topic { Id = 3, Title = "Topic3", ChapterId = 2, OrderPosition = 3 }
            };

            //Act
            var actual = await topicRepository.GetAllAsync();

            //Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new TopicEqualityComparer()));
        }

        [Test]
        public async Task GetByIdAsync_TopicIsExist_ShouldReturnTopic()
        {
            //Arrange
            var expected = new Topic { Id = 2, Title = "Topic2", ChapterId = 2, OrderPosition = 2 };

            //Act
            var actual = await topicRepository.GetByIdAsync(2);

            //Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new TopicEqualityComparer()));
        }

        [Test]
        public async Task AddAsync_TopicIsNotExist_ShouldAddTopic()
        {
            //Arrange
            var newTopic = new Topic { Id = 4, Title = "Topic4", ChapterId = 3, OrderPosition = 4 };

            //Act
            await topicRepository.AddAsync(newTopic);
            await context.SaveChangesAsync();

            //Assert
            var actual = await topicRepository.GetAllAsync();
            Assert.That(actual, Has.Member(newTopic).Using(new TopicEqualityComparer()));
        }

        [Test]
        public async Task DeleteByIdAsync_TopicIsExist_ShouldDeleteTopic()
        {
            //Arrange
            var topic = await topicRepository.GetByIdAsync(1);

            //Act
            await topicRepository.DeleteByIdAsync(topic.Id);
            await context.SaveChangesAsync();

            //Assert
            var actual = await topicRepository.GetAllAsync();
            Assert.That(actual, Has.No.Member(topic).Using(new TopicEqualityComparer()));
        }

        [Test]
        public async Task UpdateAsync_TopicIsExist_ShouldUpdateTopic()
        {
            //Arrange
            var topic = await topicRepository.GetByIdAsync(1);
            topic.Title = "NewTitle1";

            //Act
            await topicRepository.UpdateAsync(topic);
            await context.SaveChangesAsync();

            //Assert
            var actual = await topicRepository.GetAllAsync();
            var updatedTopic = new Topic { Id = 1, Title = "NewTitle1", ChapterId = 1, OrderPosition = 1 };
            Assert.That(actual, Has.Member(updatedTopic).Using(new TopicEqualityComparer()));
        }
    }
}

