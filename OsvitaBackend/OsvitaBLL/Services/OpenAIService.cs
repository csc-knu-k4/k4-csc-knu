using System;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using OpenAI.Chat;
using OsvitaBLL.Configurations;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
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

        public async Task<string> GetRecomendationTextByAssignmentResultsAsync(List<AssignmentReportModel> assignmentReportModels)
        {
            ChatClient client = new(model: openAISettings.Model, apiKey: openAISettings.ApiKey);
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Ти вчитель. Проаналізуй статистику проходження тестів за темами при підготовці до іспиту у форматі НМТ і надай невелику пораду щодо обрання теми для вивчення:");
            foreach (var model in assignmentReportModels.GroupBy(x => x.TopicId))
            {
                stringBuilder.AppendLine($"Тема: \"{model.First().TopicName}\"; Результат: {model.Sum(x => x.Points)}; Максимальний результат: {model.Sum(x => x.MaxPoints)}; ID теми: {model.Key}");
            }
            stringBuilder.AppendLine($"Розмір відповіді повинна бути не більше 2 рядків тексту. Привітання не потрібне. Відповідь у вигляді невеликого повідомлення. У повідомленні не згадуй про кількості балів.");
            ChatCompletion completion = await client.CompleteChatAsync(stringBuilder.ToString());

            return completion.Content[0].Text;
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
            ChatCompletion completion = await client.CompleteChatAsync(stringBuilder.ToString());

            return completion.Content[0].Text;
        }

        public async Task<RecomendationAIModel> GetRecomendationByDiagnosticalAssignmentSetResultAsync(AssignmentSetReportModel assignmentSetReportModel)
        {
            ChatClient client = new(model: openAISettings.Model, apiKey: openAISettings.ApiKey);
            ChatCompletionOptions options = new()
            {
                ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                    jsonSchemaFormatName: "math_reasoning",
                    jsonSchema: BinaryData.FromBytes("""
                        {
                            "type": "object",
                            "properties": {
                                "TopicIds": {
                                    "type": "array",
                                    "items": { "type": "number" }
                                },
                                "RecomendationText": { "type": "string" }
                            },
                            "required": ["TopicIds", "RecomendationText"],
                            "additionalProperties": false
                        }
                        """u8.ToArray()),
                    jsonSchemaIsStrict: true)
            };
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Ти вчитель. Проаналізуй статистику проходження тестів за темами при підготовці до іспиту у форматі НМТ і порадь учню навчальний план підготовки:");
            foreach (var model in assignmentSetReportModel.Assignments.GroupBy(x => x.TopicId))
            {
                stringBuilder.AppendLine($"Тема: \"{model.First().TopicName}\"; Результат: {model.Sum(x => x.Points)}; Максимальний результат: {model.Sum(x => x.MaxPoints)}; ID теми: {model.Key}");
            }
            stringBuilder.AppendLine($"Розмір відповіді повинна бути не більше 10 рядків тексту. Звертайся до учня. Привітання не потрібне. Відповідь у вигляді списку аналізу тем. В RecomendationText ніколи не згадуй про ID. В TopicIds запиши ID тем, які необхідно вивчити. Не вигадуй власні ID. Бери ID лише з інформації про проходження тем.");
            var messages = new List<ChatMessage>
            {
                new UserChatMessage(stringBuilder.ToString())
            };
            ChatCompletion completion = await client.CompleteChatAsync(messages, options);
            var result = JsonSerializer.Deserialize<RecomendationAIModel>(completion.Content[0].Text);

            return result;
        }

        public async Task<RecommendedTopicsModel> GetRecommendedTopicsByAssignmentsResult(List<AssignmentReportModel> assignmentReportModels)
        {
            ChatClient client = new(model: openAISettings.Model, apiKey: openAISettings.ApiKey);
            ChatCompletionOptions options = new()
            {
                ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                    jsonSchemaFormatName: "math_reasoning",
                    jsonSchema: BinaryData.FromBytes("""
                        {
                            "type": "object",
                            "properties": {
                                "TopicIds": {
                                    "type": "array",
                                    "items": { "type": "number" }
                                }
                            },
                            "required": ["TopicIds"],
                            "additionalProperties": false
                        }
                        """u8.ToArray()),
                    jsonSchemaIsStrict: true)
            };
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Ти вчитель. Проаналізуй статистику проходження тестів за темами при підготовці до іспиту у форматі НМТ і порадь учню навчальний план підготовки:");
            foreach (var model in assignmentReportModels.GroupBy(x => x.TopicId))
            {
                stringBuilder.AppendLine($"Тема: \"{model.First().TopicName}\"; Результат: {model.Sum(x => x.Points)}; Максимальний результат: {model.Sum(x => x.MaxPoints)}; ID теми: {model.Key}");
            }
            stringBuilder.AppendLine($"Відповідь у вигляді списку аналізу тем. В TopicIds запиши ID тем, які необхідно вивчити. Не вигадуй власні ID. Бери ID лише з інформації про проходження тем.");
            var messages = new List<ChatMessage>
            {
                new UserChatMessage(stringBuilder.ToString())
            };
            ChatCompletion completion = await client.CompleteChatAsync(messages, options);
            var result = JsonSerializer.Deserialize<RecommendedTopicsModel>(completion.Content[0].Text);

            return result;
        }

        public async Task<AssistantResponseModel> GetChatAssistantResponseAsync(AssistantRequestModel assistantRequestModel)
        {
            ChatClient client = new(model: openAISettings.Model, apiKey: openAISettings.ApiKey);
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Ти вчитель. Учень готується до іспиту. Учень питає визначення терміну. Поясни визначення терміну. Якщо терміну не існує або питання немає, надсилай відповідь 'На жаль, я не можу Вам допогти.' Питання учня:");
            stringBuilder.AppendLine($"{assistantRequestModel.RequestText}");
            ChatCompletion completion = await client.CompleteChatAsync(stringBuilder.ToString());
            var assistantResponseModel = new AssistantResponseModel
            {
                UserId = assistantRequestModel.UserId,
                CreationDate = DateTime.Now,
                RequestText = assistantRequestModel.RequestText,
                ResponseText = completion.Content[0].Text
            };
            return assistantResponseModel;
        }
    }
}

