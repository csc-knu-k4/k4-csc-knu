using OsvitaBLL.Models;
using OsvitaBLL.Models.ReportModels;
using OsvitaDAL.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ScottPlot;
using ScottPlot.MultiplotLayouts;

namespace OsvitaBLL.Services
{
	public class EducationClassAssignmentsReportDocument : IDocument
    {
        public EducationClassAssignmentsReportDocument(List<EducationClassAssignmetSetReportModel> models)
        {
            Models = models;
            Model = new EducationClassAssignmetSetReportModel();
        }

        public List<EducationClassAssignmetSetReportModel> Models { get; }
        public EducationClassAssignmetSetReportModel Model { get; set; }
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            foreach (var model in Models)
            {
                Model = model;
                container
                    .Page(page =>
                    {
                        page.Size(PageSizes.A4.Landscape());

                        page.Margin(50);

                        page.Header().Element(ComposeHeader);
                        page.Content().Element(ComposeContent);

                        page.Footer().AlignCenter().Text(x =>
                        {
                            x.CurrentPageNumber();
                            x.Span(" / ");
                            x.TotalPages();
                        });
                    });
            }
        }

        private void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item()
                        .Text($"Тема: {Model.AssignmetSetReportModels.First().ObjectName}")
                        .FontSize(20).SemiBold();
                });
            });
        }

        private void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(5);

                column.Item().Element(ComposeTable);
            });
        }

        private void ComposeTable(IContainer container)
        {
            var columnCount = Model.AssignmetSetReportModels.FirstOrDefault(x => x.Assignments.Count != 0).Assignments.Count;
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    for (int i = 0; i < columnCount; i++)
                    {
                        columns.ConstantColumn(25);
                    }
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("Учень");
                    foreach (var assignment in Model.AssignmetSetReportModels.FirstOrDefault(x => x.Assignments.Count != 0).Assignments)
                    {
                        header.Cell().Element(CellStyle).PaddingHorizontal(2).AlignRight().Text(assignment.AssignmentNumber);
                    }
                    header.Cell().Element(CellStyle).AlignRight().Text("Результат");
                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).BorderBottom(1).BorderColor(QuestPDF.Helpers.Colors.Black);
                    }
                });

                foreach (var assignmetSetReportModel in Model.AssignmetSetReportModels)
                {
                    table.Cell().Element(CellStyle).Text($"{assignmetSetReportModel.UserEmail}");
                    if (assignmetSetReportModel.Assignments.Count != 0)
                    {
                        foreach (var assignment in assignmetSetReportModel.Assignments)
                        {
                            var backgroundColor = GetBackgroundColor(assignment);
                            table.Cell().Background(backgroundColor).Element(CellStyle).PaddingHorizontal(2).AlignRight().Text($"{assignment.Points}/{assignment.MaxPoints}");
                        }
                        table.Cell().BorderBottom(1).PaddingVertical(5).AlignRight().Text($"{assignmetSetReportModel.Score}/{assignmetSetReportModel.MaxScore}");
                    }
                    else
                    {
                        for (int i = 0; i < columnCount; i++)
                        {
                            table.Cell().Background(QuestPDF.Helpers.Colors.Grey.Medium.Hex).Element(CellStyle).PaddingHorizontal(2).AlignRight().Text($"NA");
                        }
                        table.Cell().BorderBottom(1).PaddingVertical(5).AlignRight().Text($"NA");
                    }

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderRight(1).PaddingVertical(5);
                    }
                }
            });
        }

        private QuestPDF.Infrastructure.Color GetBackgroundColor(AssignmentReportModel assignmentReportModel)
        {
            if (assignmentReportModel.IsCorrect)
            {
                return QuestPDF.Helpers.Colors.Green.Medium.Hex;
            }
            else if (assignmentReportModel.Points > 0)
            {
                return QuestPDF.Helpers.Colors.Yellow.Medium.Hex;
            }
            else
            {
                return QuestPDF.Helpers.Colors.Red.Medium.Hex;
            }
        }
    }
}

