using System.Globalization;
using OsvitaBLL.Models;
using OsvitaBLL.Models.ReportModels;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using ScottPlot;

namespace OsvitaBLL.Services
{
	public class UserAssignmentsReportDocument : IDocument
	{
		public UserAssignmentsReportDocument(List<AssignmentSetReportModel> models)
		{
            Models = models;
            Model = new AssignmentSetReportModel();
        }

        public List<AssignmentSetReportModel> Models { get; }
        public AssignmentSetReportModel Model { get; set; }
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
                        .Text($"Тема: {Model.ObjectName}")
                        .FontSize(20).SemiBold();

                    column.Item().Text(text =>
                    {
                        text.Span("Дата проходження: ").SemiBold();
                        text.Span($"{Model.CompletedDate.ToString(new CultureInfo("uk-UA"))}");
                    });

                    column.Item().Text(text =>
                    {
                        text.Span("Результат: ").SemiBold();
                        text.Span($"{Model.Score}/{Model.MaxScore}");
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
                                new() { Value = Model.Score, FillColor = new ScottPlot.Color(QuestPDF.Helpers.Colors.Green.Medium.Hex) },
                                new() { Value = Model.MaxScore - Model.Score, FillColor = new ScottPlot.Color(QuestPDF.Helpers.Colors.Red.Medium.Hex) },
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
            });
        }

        private void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(100);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).AlignRight().Text("Номер завдання");
                    header.Cell().Element(CellStyle).AlignRight().Text("Тип завдання");
                    header.Cell().Element(CellStyle).AlignRight().Text("Результат");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(QuestPDF.Helpers.Colors.Black);
                    }
                });

                foreach (var assignment in Model.Assignments)
                {
                    table.Cell().Element(CellStyle).AlignRight().Text(assignment.AssignmentNumber);
                    table.Cell().Element(CellStyle).AlignRight().Text(AssignmentTypetoString(assignment.AssignmentType));
                    table.Cell().Element(CellStyle).AlignRight().Text($"{assignment.Points}/{assignment.MaxPoints}");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(QuestPDF.Helpers.Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }

        private string AssignmentTypetoString(AssignmentModelType assignmentModelType)
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
    }
}

