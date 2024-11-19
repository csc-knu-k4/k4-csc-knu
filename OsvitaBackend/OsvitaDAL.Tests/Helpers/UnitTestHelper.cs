using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OsvitaBLL.Configurations;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;

namespace Osvita.Tests.Integration.Helpers
{
	public class UnitTestHelper
	{
        public static DbContextOptions<OsvitaDbContext> GetUnitTestDbContextOptions()
        {
            var options = new DbContextOptionsBuilder<OsvitaDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            return options;

        }

        public static void SeedData(OsvitaDbContext context)
        {
            context.Subjects.AddRange(
                new Subject { Id = 1, Title = "Subject1" },
                new Subject { Id = 2, Title = "Subject2" },
                new Subject { Id = 3, Title = "Subject3" }
            );
            context.Chapters.AddRange(
                new Chapter { Id = 1, Title = "Chapter1", SubjectId = 1, OrderPosition = 1 },
                new Chapter { Id = 2, Title = "Chapter2", SubjectId = 2, OrderPosition = 2 },
                new Chapter { Id = 3, Title = "Chapter3", SubjectId = 2, OrderPosition = 3 }
            );
            context.Topics.AddRange(
                new Topic { Id = 1, Title = "Topic1", ChapterId = 1, OrderPosition = 1 },
                new Topic { Id = 2, Title = "Topic2", ChapterId = 2, OrderPosition = 2 },
                new Topic { Id = 3, Title = "Topic3", ChapterId = 2, OrderPosition = 3 }
            );
            context.Materials.AddRange(
                new Material { Id = 1, Title = "Material1", TopicId = 1, OrderPosition = 1 },
                new Material { Id = 2, Title = "Material2", TopicId = 2, OrderPosition = 2 },
                new Material { Id = 3, Title = "Material3", TopicId = 2, OrderPosition = 3 }
            );
            context.ContentBlocks.AddRange(
                new ContentBlock { Id = 1, Title = "ContentBlock1", MaterialId = 1, OrderPosition = 1, ContentType = ContentType.TextBlock, Value = "Text1" },
                new ContentBlock { Id = 2, Title = "ContentBlock2", MaterialId = 2, OrderPosition = 2, ContentType = ContentType.TextBlock, Value = "Text2" },
                new ContentBlock { Id = 3, Title = "ContentBlock3", MaterialId = 2, OrderPosition = 3, ContentType = ContentType.ImageBlock, Value = "Image2" }
            );

            context.SaveChanges();
        }

        public static IMapper GetAutoMapperProfile()
        {
            var profile = new OsvitaAutomapperProfile();
            var configuration = new MapperConfiguration(c => c.AddProfile(profile));
            return new Mapper(configuration);
        }
    }
}

