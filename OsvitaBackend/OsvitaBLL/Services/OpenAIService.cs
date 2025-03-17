using System;
using System.Text;
using Microsoft.Extensions.Options;
using OpenAI.Chat;
using OsvitaBLL.Configurations;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models.ReportModels;

namespace OsvitaBLL.Services
{
	public class OpenAIService : IAIService
	{
        private readonly OpenAISettings openAISettings;

        public OpenAIService(IOptions<OpenAISettings> openAISettings)
        {
            this.openAISettings = openAISettings.Value;
        }

        public async Task<string> GetRecomendationTextByDiagnosticalAssignmentSetResultAsync(AssignmentSetReportModel assignmentSetReportModel)
		{
            ChatClient client = new(model: openAISettings.Model, apiKey: openAISettings.ApiKey);

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Ти вчитель. Проаналізуй статистику проходження тестів за темами при підготовці до іспиту у форматі НМТ і порадь учню навчальний план підготовки:");
            foreach (var model in assignmentSetReportModel.Assignments.GroupBy(x => x.TopicId))
            {
                stringBuilder.AppendLine($"Тема: \"{model.First().TopicName}\"; Результат: {model.Sum(x => x.Points)}; Максимальний результат: {model.Sum(x => x.MaxPoints)}; ID теми: {model.Key}");
            }
            stringBuilder.AppendLine($"Розмір відповіді повинна бути не більше 10 рядків тексту. Звертайся до учня. Привітання не потрібне. Відповідь у вигляді списку аналізу тем.");
            ChatCompletion completion = client.CompleteChat(stringBuilder.ToString());

            return completion.Content[0].Text;
        }
	}
}

