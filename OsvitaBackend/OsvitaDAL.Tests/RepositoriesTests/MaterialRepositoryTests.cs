using Microsoft.EntityFrameworkCore;
using Osvita.Tests.Integration.Helpers;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Repositories;

namespace Osvita.Tests.Integration.RepositoriesTests
{
    [TestFixture]
	public class MaterialRepositoryTests
	{
        private OsvitaDbContext context;
        private MaterialRepository materialRepository;

        [SetUp]
        public void Setup()
        {
            context = new OsvitaDbContext(UnitTestHelper.GetUnitTestDbContextOptions());

            if (context.Database.IsInMemory())
            {
                context.Database.EnsureDeleted();
                UnitTestHelper.SeedData(context);
            }

            materialRepository = new MaterialRepository(context);
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
        public async Task GetAllAsync_MaterialsAreExist_ShouldReturnAllMaterials()
        {
            //Arrange
            var expected = new List<Material>
            {
                new Material { Id = 1, Title = "Material1", TopicId = 1, OrderPosition = 1 },
                new Material { Id = 2, Title = "Material2", TopicId = 2, OrderPosition = 2 },
                new Material { Id = 3, Title = "Material3", TopicId = 2, OrderPosition = 3 }
            };

            //Act
            var actual = await materialRepository.GetAllAsync();

            //Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new MaterialEqualityComparer()));
        }

        [Test]
        public async Task GetByIdAsync_MaterialIsExist_ShouldReturnMaterial()
        {
            //Arrange
            var expected = new Material { Id = 2, Title = "Material2", TopicId = 2, OrderPosition = 2 };

            //Act
            var actual = await materialRepository.GetByIdAsync(2);

            //Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new MaterialEqualityComparer()));
        }

        [Test]
        public async Task AddAsync_MaterialIsNotExist_ShouldAddMaterial()
        {
            //Arrange
            var newMaterial = new Material { Id = 4, Title = "Material4", TopicId = 3, OrderPosition = 1 };

            //Act
            await materialRepository.AddAsync(newMaterial);
            await context.SaveChangesAsync();

            //Assert
            var actual = await materialRepository.GetAllAsync();
            Assert.That(actual, Has.Member(newMaterial).Using(new MaterialEqualityComparer()));
        }

        [Test]
        public async Task DeleteByIdAsync_TopicIsExist_ShouldDeleteTopic()
        {
            //Arrange
            var material = await materialRepository.GetByIdAsync(1);

            //Act
            await materialRepository.DeleteByIdAsync(material.Id);
            await context.SaveChangesAsync();

            //Assert
            var actual = await materialRepository.GetAllAsync();
            Assert.That(actual, Has.No.Member(material).Using(new MaterialEqualityComparer()));
        }

        [Test]
        public async Task UpdateAsync_TopicIsExist_ShouldUpdateTopic()
        {
            //Arrange
            var material = await materialRepository.GetByIdAsync(1);
            material.Title = "NewTitle1";

            //Act
            await materialRepository.UpdateAsync(material);
            await context.SaveChangesAsync();

            //Assert
            var actual = await materialRepository.GetAllAsync();
            var updatedMaterial = new Material { Id = 1, Title = "NewTitle1", TopicId = 1, OrderPosition = 1 };
            Assert.That(actual, Has.Member(updatedMaterial).Using(new MaterialEqualityComparer()));
        }
    }
}

