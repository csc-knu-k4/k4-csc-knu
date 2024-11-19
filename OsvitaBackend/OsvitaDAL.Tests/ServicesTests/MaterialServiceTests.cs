using Microsoft.EntityFrameworkCore;
using Osvita.Tests.Integration.Helpers;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaBLL.Services;
using OsvitaDAL.Data;
using OsvitaDAL.Interfaces;

namespace Osvita.Tests.Integration.ServicesTests
{
	public class MaterialServiceTests
	{
        private OsvitaDbContext context;
        private IUnitOfWork unitOfWork;
        private IMaterialService materialService;

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
            materialService = new MaterialService(unitOfWork, UnitTestHelper.GetAutoMapperProfile());
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
        public async Task GetAllAsync_MaterialsExist_ShouldReturnAllMaterialsModels()
        {
            //Arrange
            var expected = new List<MaterialModel>()
            {
                new MaterialModel { Id = 1, Title = "Material1", TopicId = 1, ContentBlocksIds = new List<int> { 1 }, OrderPosition = 1 },
                new MaterialModel { Id = 2, Title = "Material2", TopicId = 2, ContentBlocksIds = new List<int> { 2, 3 }, OrderPosition = 2 },
                new MaterialModel { Id = 3, Title = "Material3", TopicId = 2, ContentBlocksIds = new List<int>(), OrderPosition = 3 }
            };

            //Act
            var actual = await materialService.GetAllAsync();

            //Assert
            Assert.That(actual, Is.EquivalentTo(expected).Using(new MaterialModelEqualityComparer()));
        }

        [Test]
        public async Task GetByIdAsync_MaterialExist_ShouldReturnMaterialModel()
        {
            //Arrange
            var expected = new MaterialModel { Id = 1, Title = "Material1", TopicId = 1, ContentBlocksIds = new List<int> { 1 }, OrderPosition = 1 };

            //Act
            var actual = await materialService.GetByIdAsync(1);

            //Asset
            Assert.That(actual, Is.EqualTo(expected).Using(new MaterialModelEqualityComparer()));
        }

        [Test]
        public async Task DeleteByIdAsync_MaterialIsExist_ShouldDeleteMaterial()
        {
            //Arrange
            var materialModel = await materialService.GetByIdAsync(1);

            //Act
            await materialService.DeleteByIdAsync(1);

            //Assert
            var actual = await materialService.GetAllAsync();
            Assert.That(actual, Has.No.Member(materialModel).Using(new MaterialModelEqualityComparer()));
        }

        [Test]
        public async Task AddAsync_MaterialIsNotExist_ShouldAddMaterial()
        {
            //Arrange
            var newMaterialModel = new MaterialModel { Id = 4, Title = "Material4", TopicId = 1, ContentBlocksIds = new List<int>(), OrderPosition = 1 };

            //Act
            await materialService.AddAsync(newMaterialModel);

            //Assert
            var actual = await materialService.GetAllAsync();
            Assert.That(actual, Has.Member(newMaterialModel).Using(new MaterialModelEqualityComparer()));
        }
    }
}

