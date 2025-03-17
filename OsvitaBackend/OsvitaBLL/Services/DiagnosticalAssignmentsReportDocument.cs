using System.Globalization;
using OsvitaBLL.Models;
using OsvitaBLL.Models.ReportModels;
using OsvitaDAL.Entities;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using ScottPlot;

namespace OsvitaBLL.Services
{
	public class DiagnosticalAssignmentsReportDocument : IDocument
	{
		public DiagnosticalAssignmentsReportDocument(List<DiagnosticalAssignmentSetReportModel> models)
		{
            Models = models;
            Model = new DiagnosticalAssignmentSetReportModel();
        }

        public List<DiagnosticalAssignmentSetReportModel> Models { get; }
        public DiagnosticalAssignmentSetReportModel Model { get; set; }
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
                        .Text($"{ObjectTypeToString(Model.AssignmentSetReportModel.ObjectType)}: {Model.AssignmentSetReportModel.ObjectName}")
                        .FontSize(20).SemiBold();

                    column.Item().Text(text =>
                    {
                        text.Span("Дата проходження: ").SemiBold();
                        text.Span($"{Model.AssignmentSetReportModel.CompletedDate.ToString(new CultureInfo("uk-UA"))}");
                    });

                    column.Item().Text(text =>
                    {
                        text.Span("Результат: ").SemiBold();
                        text.Span($"{Model.AssignmentSetReportModel.Score}/{Model.AssignmentSetReportModel.MaxScore}");
                    });
                });

                row.RelativeItem().AlignRight().Column(column =>
                {
                    column.Item()
                        .Width(100)
                        .AlignRight()
                        .AspectRatio(1)
                        .Svg(size =>
                        {
                            ScottPlot.Plot plot = new();

                            var slices = new PieSlice[]
                            {
                                new() { Value = Model.AssignmentSetReportModel.Score, FillColor = new ScottPlot.Color(QuestPDF.Helpers.Colors.Green.Medium.Hex) },
                                new() { Value = Model.AssignmentSetReportModel.MaxScore - Model.AssignmentSetReportModel.Score, FillColor = new ScottPlot.Color(QuestPDF.Helpers.Colors.Red.Medium.Hex) },
                            };

                            var pie = plot.Add.Pie(slices);
                            pie.DonutFraction = 0.5;
                            pie.SliceLabelDistance = 1;
                            pie.LineColor = ScottPlot.Colors.White;
                            plot.Axes.Frameless();
                            plot.HideGrid();

                            return plot.GetSvgXml((int)size.Width, (int)size.Height);
                        });
                });
            });
        }

        private void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(5);

                column.Item().Element(ComposeTable);

                if (!string.IsNullOrWhiteSpace(Model.RecomendationText))
                {
                    column.Item().PaddingTop(25).Element(ComposeRecomendations);
                }
            });
        }

        private void ComposeTable(IContainer container)
        {
            var topics = Model.AssignmentSetReportModel.Assignments.GroupBy(x => x.TopicName);
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(100);
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).AlignRight().Text("Тема");
                    header.Cell().Element(CellStyle).AlignRight().Text("Результат");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(QuestPDF.Helpers.Colors.Black);
                    }
                });

                foreach (var topic in topics)
                {
                    table.Cell().Element(CellStyle).AlignRight().Text(topic.Key);
                    table.Cell().Element(CellStyle).AlignRight().Text($"{topic.Sum(x => x.Points)}/{topic.Sum(x => x.MaxPoints)}");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(QuestPDF.Helpers.Colors.Grey.Lighten2).PaddingVertical(5).PaddingHorizontal(3);
                    }
                }
            });
        }

        void ComposeRecomendations(IContainer container)
        {
            container.Background(QuestPDF.Helpers.Colors.Grey.Lighten3).Padding(10).Column(column =>
            {
                column.Spacing(5);
                column.Item().Text("Аналіз результатів").FontSize(14);
                column.Item().Text(Model.RecomendationText);
            });
        }

        private string AssignmentTypeToString(AssignmentModelType assignmentModelType)
        {
            switch (assignmentModelType)
            {
                case AssignmentModelType.OneAnswerAsssignment:
                    return "Вибір однієї відповіді";
                case AssignmentModelType.OpenAnswerAssignment:
                    return "Відкритої форми";
                case AssignmentModelType.MatchComplianceAssignment:
                    return "Встановлення відповідності";
                default:
                    return "Невідомий тип завдання";
            }
        }

        private string ObjectTypeToString(ObjectModelType objectModelType)
        {
            switch (objectModelType)
            {
                case ObjectModelType.TopicModel:
                    return "Тема";
                case ObjectModelType.SubjectModel:
                    return "Предмет";
                case ObjectModelType.DiagnosticalModel:
                    return "Діагностичний тест";
                default:
                    return "";
            }
        }

        private string GetColumnNameByReportType(ObjectModelType objectModelType)
        {
            switch (objectModelType)
            {
                case ObjectModelType.TopicModel:
                    return "Тип завдання";
                case ObjectModelType.SubjectModel:
                    return "Тема";
                case ObjectModelType.DiagnosticalModel:
                    return "Тема";
                default:
                    return "";
            }
        }

        private string GetColumnValueByReportType(ObjectModelType objectModelType, AssignmentReportModel assignmentReportModel)
        {
            switch (objectModelType)
            {
                case ObjectModelType.TopicModel:
                    return AssignmentTypeToString(assignmentReportModel.AssignmentType);
                case ObjectModelType.SubjectModel:
                    return assignmentReportModel.TopicName;
                case ObjectModelType.DiagnosticalModel:
                    return assignmentReportModel.TopicName;
                default:
                    return "";
            }
        }
    }
}

