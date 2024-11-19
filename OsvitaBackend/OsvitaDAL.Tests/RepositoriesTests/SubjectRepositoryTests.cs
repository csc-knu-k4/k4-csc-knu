using Microsoft.EntityFrameworkCore;
using Osvita.Tests.Integration.Helpers;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Repositories;

namespace Osvita.Tests.Integration.RepositoriesTests
{
    [TestFixture]
	public class SubjectRepositoryTests
	{
		private OsvitaDbContext context;
		private SubjectRepository subjectRepository;

        [SetUp]
        public void Setup()
        {
            context = new OsvitaDbContext(UnitTestHelper.GetUnitTestDbContextOptions());

            if (context.Database.IsInMemory())
            {
                context.Database.EnsureDeleted();
                UnitTestHelper.SeedData(context);
            }

            subjectRepository = new SubjectRepository(context);
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
        public async Task GetAllAsync_SubjectsExist_ShouldReturnAllSubjects()
        {
            //Arrange
            var expected = new List<Subject>
            {
                new Subject { Id = 1, Title = "Subject1"},
                new Subject { Id = 2, Title = "Subject2" },
                new Subject { Id = 3, Title = "Subject3" }
            };

            //Act
            var actual = await subjectRepository.GetAllAsync();

            //Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new SubjectEqualityComparer()));
        }

        [Test]
        public async Task GetByIdAsync_SubjectsExist_ShouldReturnSubject()
        {
            //Arrange
            var expected = new Subject { Id = 2, Title = "Subject2" };

            //Act
            var actual = await subjectRepository.GetByIdAsync(2);

            //Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new SubjectEqualityComparer()));
        }

        [Test]
        public async Task AddAsync_SubjectIsNotExist_ShouldAddSubject()
        {
            //Arrange
            var newSubject = new Subject { Id = 4, Title = "Subject4" };

            //Act
            await subjectRepository.AddAsync(newSubject);
            await context.SaveChangesAsync();

            //Assert
            var actual = await subjectRepository.GetAllAsync();
            Assert.That(actual, Has.Member(newSubject).Using(new SubjectEqualityComparer()));
        }

        [Test]
        public async Task DeleteByIdAsync_SubjectIsExist_ShouldDeleteSubject()
        {
            //Arrange
            var subject = await subjectRepository.GetByIdAsync(1);

            //Act
            await subjectRepository.DeleteByIdAsync(subject.Id);
            await context.SaveChangesAsync();

            //Assert
            var actual = await subjectRepository.GetAllAsync();
            Assert.That(actual, Has.No.Member(subject).Using(new SubjectEqualityComparer()));
        }

        [Test]
        public async Task UpdateAsync_SubjectIsExist_ShouldUpdateSubject()
        {
            //Arrange
            var subject = await subjectRepository.GetByIdAsync(1);
            subject.Title = "NewTitle1";

            //Act
            await subjectRepository.UpdateAsync(subject);
            await context.SaveChangesAsync();

            //Assert
            var actual = await subjectRepository.GetAllAsync();
            var updatedSubject = new Subject { Id = 1, Title = "NewTitle1" };
            Assert.That(actual, Has.Member(updatedSubject).Using(new SubjectEqualityComparer()));
        }
    }
}

