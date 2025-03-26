using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaBLL.Services
{
	public class ExcelService : IExcelService
	{
        private readonly IUnitOfWork unitOfWork;
        private readonly IAssignmentService assignmentService;
        public ExcelService(IUnitOfWork unitOfWork, IAssignmentService assignmentService)
        {
            this.unitOfWork = unitOfWork;
            this.assignmentService = assignmentService;
        }

        public async Task ImportMaterialsAsync(IFormFile fileExcel)
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
                            Title = material.Title,
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

        public async Task ImportAssignmentsAsync(IFormFile fileExcel)
        {
            if (fileExcel != null)
            {
                using (var stream = fileExcel.OpenReadStream())
                {
                    using (XLWorkbook workBook = new XLWorkbook(stream))
                    {
                        var oneAnswerAssignmentsWorksheet = workBook.Worksheets.ToArray()[0];
                        await AddOneAnswerAssignments(oneAnswerAssignmentsWorksheet);
                        var openAnswerAssignmentsWorksheet = workBook.Worksheets.ToArray()[1];
                        await AddOpenAnswerAssignments(openAnswerAssignmentsWorksheet);
                        var matchComplianceAssignmentsWorksheet = workBook.Worksheets.ToArray()[2];
                        await AddMatchComplianceAssignments(matchComplianceAssignmentsWorksheet);
                    }
                }
            }
        }

        private async Task AddOneAnswerAssignments(IXLWorksheet oneAnswerAssignmentsWorksheet)
        {
            var index = 2;
            var isAssignmentExist = !string.IsNullOrEmpty(oneAnswerAssignmentsWorksheet.Row(index).Cell(1).Value.ToString());
            while (isAssignmentExist)
            {
                var materialId = (int)oneAnswerAssignmentsWorksheet.Row(index).Cell(1).Value.GetNumber();
                var description = oneAnswerAssignmentsWorksheet.Row(index).Cell(2).Value.ToString();
                var descriptionImage = oneAnswerAssignmentsWorksheet.Row(index).Cell(3).Value.ToString().Trim();
                var answersCount = (int)oneAnswerAssignmentsWorksheet.Row(index).Cell(4).Value.GetNumber();
                var explanation = oneAnswerAssignmentsWorksheet.Row(index).Cell(8).Value.ToString();
                var assignmentModel = new AssignmentModel
                {
                    ObjectId = materialId,
                    ProblemDescription = description,
                    ProblemDescriptionImage = descriptionImage,
                    Explanation = explanation,
                    AssignmentModelType = AssignmentModelType.OneAnswerAsssignment,
                    Answers = new List<AnswerModel>()
                };
                for (int i = 0; i < answersCount; i++)
                {
                    var answerIndex = index + i + 1;
                    var answerValue = oneAnswerAssignmentsWorksheet.Row(answerIndex).Cell(5).Value.ToString();
                    var answerValueImage = oneAnswerAssignmentsWorksheet.Row(answerIndex).Cell(6).Value.ToString().Trim();
                    var answerPoints = (int)oneAnswerAssignmentsWorksheet.Row(answerIndex).Cell(7).Value.GetNumber();
                    var answerIsCorrect = answerPoints > 0;
                    var answerModel = new AnswerModel
                    {
                        Value = answerValue,
                        ValueImage = answerValueImage,
                        IsCorrect = answerIsCorrect,
                        Points = answerPoints
                    };
                    assignmentModel.Answers.Add(answerModel);
                }
                await assignmentService.AddAssignmentAsync(assignmentModel);
                index = index + answersCount + 1;
                isAssignmentExist = !string.IsNullOrEmpty(oneAnswerAssignmentsWorksheet.Row(index).Cell(1).Value.ToString());
            }
        }

        private async Task AddOpenAnswerAssignments(IXLWorksheet openAnswerAssignmentsWorksheet)
        {
            foreach (var row in openAnswerAssignmentsWorksheet.Rows().Skip(1))
            {
                var materialId = (int)row.Cell(1).Value.GetNumber();
                var description = row.Cell(2).Value.ToString();
                var descriptionImage = row.Cell(3).Value.ToString();
                var answerValue = row.Cell(4).Value.ToString();
                var answerValueImage = string.IsNullOrWhiteSpace(row.Cell(5).Value.ToString()) ? null : row.Cell(5).Value.ToString();
                var answerPoints = (int)row.Cell(6).Value.GetNumber();
                var answerIsCorrect = answerPoints > 0;
                var explanation = row.Cell(7).Value.ToString();
                var answerModel = new AnswerModel
                {
                    Value = answerValue,
                    ValueImage = string.IsNullOrWhiteSpace(answerValueImage) ? null : answerValueImage,
                    IsCorrect = answerIsCorrect,
                    Points = answerPoints
                };
                var assignmentModel = new AssignmentModel
                {
                    ObjectId = materialId,
                    ProblemDescription = description,
                    ProblemDescriptionImage = string.IsNullOrWhiteSpace(descriptionImage) ? null : descriptionImage,
                    Explanation = explanation,
                    AssignmentModelType = AssignmentModelType.OpenAnswerAssignment,
                    Answers = new List<AnswerModel>() { answerModel }
                };
                await assignmentService.AddAssignmentAsync(assignmentModel);
            }
        }

        private async Task AddMatchComplianceAssignments(IXLWorksheet matchComplianceAssignmentsWorksheet)
        {
            var index = 2;
            var isAssignmentExist = !string.IsNullOrEmpty(matchComplianceAssignmentsWorksheet.Row(index).Cell(1).Value.ToString());
            while (isAssignmentExist)
            {
                var materialId = (int)matchComplianceAssignmentsWorksheet.Row(index).Cell(1).Value.GetNumber();
                var description = matchComplianceAssignmentsWorksheet.Row(index).Cell(2).Value.ToString();
                var descriptionImage = matchComplianceAssignmentsWorksheet.Row(index).Cell(3).Value.ToString().Trim();
                var childAssignmentsCount = (int)matchComplianceAssignmentsWorksheet.Row(index).Cell(4).Value.GetNumber();
                var explanation = matchComplianceAssignmentsWorksheet.Row(index).Cell(9).Value.ToString();
                var assignmentModel = new AssignmentModel
                {
                    ObjectId = materialId,
                    ProblemDescription = description,
                    ProblemDescriptionImage = string.IsNullOrWhiteSpace(descriptionImage) ? null : descriptionImage,
                    Explanation = explanation,
                    AssignmentModelType = AssignmentModelType.MatchComplianceAssignment,
                    Answers = new List<AnswerModel>(),
                    ChildAssignments = new List<AssignmentModel>()
                };
                var childIndex = index + 1;
                for (int i = 0; i < childAssignmentsCount; i++)
                {
                    var childDescription = matchComplianceAssignmentsWorksheet.Row(childIndex).Cell(2).Value.ToString();
                    var childDescriptionImage = matchComplianceAssignmentsWorksheet.Row(childIndex).Cell(3).Value.ToString().Trim();
                    var answersCount = (int)matchComplianceAssignmentsWorksheet.Row(childIndex).Cell(5).Value.GetNumber();
                    var childExplanation = matchComplianceAssignmentsWorksheet.Row(childIndex).Cell(9).Value.ToString();
                    var childAssignmentModel = new AssignmentModel
                    {
                        ObjectId = materialId,
                        ProblemDescription = childDescription,
                        ProblemDescriptionImage = string.IsNullOrWhiteSpace(childDescriptionImage) ? null : childDescriptionImage,
                        Explanation = childExplanation,
                        AssignmentModelType = AssignmentModelType.ChildAssignment,
                        Answers = new List<AnswerModel>()
                    };
                    for (int j = 0; j < answersCount; j++)
                    {
                        var answerIndex = childIndex + j + 1;
                        var answerValue = matchComplianceAssignmentsWorksheet.Row(answerIndex).Cell(6).Value.ToString();
                        var answerValueImage = matchComplianceAssignmentsWorksheet.Row(answerIndex).Cell(7).Value.ToString().Trim();
                        var answerPoints = (int)matchComplianceAssignmentsWorksheet.Row(answerIndex).Cell(8).Value.GetNumber();
                        var answerIsCorrect = answerPoints > 0;
                        var answerModel = new AnswerModel
                        {
                            Value = answerValue,
                            ValueImage = string.IsNullOrWhiteSpace(answerValueImage) ? null : answerValueImage,
                            IsCorrect = answerIsCorrect,
                            Points = answerPoints
                        };
                        childAssignmentModel.Answers.Add(answerModel);
                    }
                    childIndex = childIndex + answersCount + 1;
                    index = childIndex;
                    assignmentModel.ChildAssignments.Add(childAssignmentModel);
                }
                await assignmentService.AddAssignmentAsync(assignmentModel);
                isAssignmentExist = !string.IsNullOrEmpty(matchComplianceAssignmentsWorksheet.Row(index).Cell(1).Value.ToString());
            }
        }
    }
}

