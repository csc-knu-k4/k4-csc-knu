using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using OsvitaBLL.Interfaces;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaBLL.Services
{
	public class ExcelService : IExcelService
	{
        private readonly IUnitOfWork unitOfWork;
        public ExcelService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task ImportAsync(IFormFile fileExcel)
        {
            if (fileExcel != null)
            {
                using (var stream = fileExcel.OpenReadStream())
                {
                    using (XLWorkbook workBook = new XLWorkbook(stream))
                    {
                        foreach (IXLWorksheet worksheet in workBook.Worksheets)
                        {
                            await AddSubjectAsync(worksheet, stream);
                        }
                    }
                }
            }
        }

        private async Task AddSubjectAsync(IXLWorksheet worksheet, Stream stream)
        {
            var subject = (await unitOfWork.SubjectRepository.GetAllAsync()).FirstOrDefault(x => x.Title == worksheet.Name);

            if (subject is null)
            {
                subject = new Subject
                {
                    Title = worksheet.Name
                };
                await unitOfWork.SubjectRepository.AddAsync(subject);
                await unitOfWork.SaveChangesAsync();
            }
            var index = 2;
            index = await AddChaptersAsync(worksheet, subject, index, stream);
        }

        private async Task<int> AddChaptersAsync(IXLWorksheet worksheet, Subject? subject, int index, Stream stream)
        {
            var chapterName = worksheet.Row(index).Cell(1).Value.ToString();
            while (!string.IsNullOrEmpty(chapterName))
            {
                var chapter = (await unitOfWork.ChapterRepository.GetAllAsync()).FirstOrDefault(x => x.Title == chapterName && x.SubjectId == subject.Id);
                if (chapter is null)
                {
                    chapter = new Chapter
                    {
                        Title = chapterName,
                        SubjectId = subject.Id,
                        OrderPosition = (int)worksheet.Row(index).Cell(3).Value.GetNumber()
                    };
                    await unitOfWork.ChapterRepository.AddAsync(chapter);
                    await unitOfWork.SaveChangesAsync();
                }
                var topicCount = (int)worksheet.Row(index).Cell(2).Value.GetNumber();
                var topicIndex = index + 1;
                topicIndex = await AddTopicsAsync(worksheet, chapter, topicCount, topicIndex, stream);
                index = topicIndex;
                chapterName = worksheet.Row(index).Cell(1).Value.ToString();
            }
            return index;
        }

        private async Task<int> AddTopicsAsync(IXLWorksheet worksheet, Chapter? chapter, int topicCount, int topicIndex, Stream stream)
        {
            for (int i = 0; i < topicCount; i++)
            {
                var topicName = worksheet.Row(topicIndex).Cell(1).Value.ToString();
                var topic = (await unitOfWork.TopicRepository.GetAllAsync()).FirstOrDefault(x => x.Title == topicName && x.ChapterId == chapter.Id);
                if (topic is null)
                {
                    topic = new OsvitaDAL.Entities.Topic
                    {
                        Title = topicName,
                        ChapterId = chapter.Id,
                        OrderPosition = (int)worksheet.Row(topicIndex).Cell(3).Value.GetNumber()
                    };
                    await unitOfWork.TopicRepository.AddAsync(topic);
                    await unitOfWork.SaveChangesAsync();
                }
                var materialCount = (int)worksheet.Row(topicIndex).Cell(2).Value.GetNumber();
                var materialIndex = topicIndex + 1;
                materialIndex = await AddMaterialsAsync(worksheet, topic, materialCount, materialIndex, stream);
                topicIndex = materialIndex;
            }
            return topicIndex;
        }

        private async Task<int> AddMaterialsAsync(IXLWorksheet worksheet, OsvitaDAL.Entities.Topic? topic, int materialCount, int materialIndex, Stream stream)
        {
            for (int k = 0; k < materialCount; k++)
            {
                var materialName = worksheet.Row(materialIndex).Cell(1).Value.ToString();
                var material = (await unitOfWork.MaterialRepository.GetAllAsync()).FirstOrDefault(x => x.Title == materialName && x.TopicId == topic.Id);
                if (material is null)
                {
                    material = new Material
                    {
                        Title = materialName,
                        TopicId = topic.Id,
                        OrderPosition = (int)worksheet.Row(materialIndex).Cell(3).Value.GetNumber()
                    };
                    await unitOfWork.MaterialRepository.AddAsync(material);
                    await unitOfWork.SaveChangesAsync();
                }
                var contentBlockCount = (int)worksheet.Row(materialIndex).Cell(2).Value.GetNumber();
                var contentBlockIndex = materialIndex + 1;
                contentBlockIndex = await AddContentBlocksAsync(worksheet, material, contentBlockCount, contentBlockIndex, stream);
                materialIndex = contentBlockIndex;
            }
            return materialIndex;
        }

        private async Task<int> AddContentBlocksAsync(IXLWorksheet worksheet, Material? material, int contentBlockCount, int contentBlockIndex, Stream stream)
        {
            for (int j = 0; j < contentBlockCount; j++)
            {
                var contentType = GetContentType((int)worksheet.Row(contentBlockIndex).Cell(4).Value.GetNumber());
                if (contentType == ContentType.TextBlock)
                {
                    var content = worksheet.Row(contentBlockIndex).Cell(1).Value.ToString();
                    var contentBlock = (await unitOfWork.ContentBlockRepository.GetAllAsync()).FirstOrDefault(x => x.Value == content && x.MaterialId == material.Id);
                    if (contentBlock is null)
                    {
                        contentBlock = new ContentBlock
                        {
                            Value = content,
                            Title = content,
                            ContentType = contentType,
                            MaterialId = material.Id,
                            OrderPosition = (int)worksheet.Row(contentBlockIndex).Cell(3).Value.GetNumber()
                        };
                        await unitOfWork.ContentBlockRepository.AddAsync(contentBlock);
                        await unitOfWork.SaveChangesAsync();
                    }
                }
                if (contentType == ContentType.ImageBlock)
                {
                    var fileName = worksheet.Row(contentBlockIndex).Cell(1).Value.ToString();
                    var contentBlock = (await unitOfWork.ContentBlockRepository.GetAllAsync()).FirstOrDefault(x => x.Title == fileName && x.MaterialId == material.Id);
                    if (contentBlock is null)
                    {
                        contentBlock = new ContentBlock
                        {
                            Value = fileName,
                            Title = fileName,
                            ContentType = contentType,
                            MaterialId = material.Id,
                            OrderPosition = (int)worksheet.Row(contentBlockIndex).Cell(3).Value.GetNumber()
                        };
                        await unitOfWork.ContentBlockRepository.AddAsync(contentBlock);
                        await unitOfWork.SaveChangesAsync();
                    }
                }
                contentBlockIndex = contentBlockIndex + 1;
            }
            return contentBlockIndex;
        }

        private ContentType GetContentType(int type)
        {
            return type == 0 ? ContentType.TextBlock : ContentType.ImageBlock;
        }
    }
}

