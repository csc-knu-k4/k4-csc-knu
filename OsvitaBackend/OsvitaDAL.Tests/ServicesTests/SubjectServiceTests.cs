using Microsoft.EntityFrameworkCore;
using Osvita.Tests.Integration.Helpers;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaBLL.Services;
using OsvitaDAL.Data;
using OsvitaDAL.Interfaces;

namespace Osvita.Tests.Integration.ServicesTests
{
	public class SubjectServiceTests
	{
        private OsvitaDbContext context;
        private IUnitOfWork unitOfWork;
        private ISubjectService subjectService;

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
            subjectService = new SubjectService(unitOfWork, UnitTestHelper.GetAutoMapperProfile());
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
        public async Task GetAllAsync_SubjectsExist_ShouldReturnAllSubjectsModels()
        {
            //Arrange
            var expected = new List<SubjectModel>()
            {
                new SubjectModel { Id = 1, Title = "Subject1", ChaptersIds = new List<int> { 1 } },
                new SubjectModel { Id = 2, Title = "Subject2", ChaptersIds = new List<int> { 2, 3 } },
                new SubjectModel { Id = 3, Title = "Subject3", ChaptersIds = new List<int>() }
            };

            //Act
            var actual = await subjectService.GetAllAsync();

            //Assert
            Assert.That(actual, Is.EquivalentTo(expected).Using(new SubjectModelEqualityComparer()));
        }

        [Test]
        public async Task GetByIdAsync_SubjectExist_ShouldReturnSubjectModel()
        {
            //Arrange
            var expected = new SubjectModel { Id = 1, Title = "Subject1", ChaptersIds = new List<int> { 1 } };

            //Act
            var actual = await subjectService.GetByIdAsync(1);

            //Asset
            Assert.That(actual, Is.EqualTo(expected).Using(new SubjectModelEqualityComparer()));
        }

        [Test]
        public async Task DeleteByIdAsync_SubjectIsExist_ShouldDeleteSubject()
        {
            //Arrange
            var subjectModel = await subjectService.GetByIdAsync(1);

            //Act
            await subjectService.DeleteByIdAsync(1);

            //Assert
            var actual = await subjectService.GetAllAsync();
            Assert.That(actual, Has.No.Member(subjectModel).Using(new SubjectModelEqualityComparer()));
        }

        [Test]
        public async Task AddAsync_SubjectIsNotExist_ShouldAddSubject()
        {
            //Arrange
            var newSubjectModel = new SubjectModel { Id = 4, Title = "Subject4", ChaptersIds = new List<int>() };

            //Act
            await subjectService.AddAsync(newSubjectModel);

            //Assert
            var actual = await subjectService.GetAllAsync();
            Assert.That(actual, Has.Member(newSubjectModel).Using(new SubjectModelEqualityComparer()));
        }
    }
}

