using ChatCode.Bot.Services;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Net.Http;

namespace ChatCode.Bot.Models
{
    [Serializable]
    public enum GenderEnum
    {
        Male, Female
    };

    [Serializable]
    public enum PublishEnum
    {
        Yes, No
    };

    public enum StarEnum
    {
        Excellent = 5,
        VeryGood = 4,
        Good = 3,
        Satisfactory = 2,
        Poor = 1
    };

    [Serializable]
    public class MessageModel
    {
        [Prompt("What do you request?")]
        public string AnalysisText;

        [Prompt("Your email address:")]
        public string Email;

        [Prompt("Your age:")]
        public string Age;

        [Prompt("Your gender? {||}")]
        public GenderEnum? Gender;

        [Prompt("Tell me about yourself.")]
        public string Description;

        [Prompt("Upload an image that expresses the website you want to create")]
        public string Image;

        [Prompt("Facebook:")]
        public string Facebook;

        [Prompt("Twitter:")]
        public string Twitter;

        [Prompt("LinkedIn:")]
        public string LinkedIn;

        [Prompt("Do you want to publish your site? {||}")]
        public PublishEnum? IsPublish;

        [Prompt("Do you like it? {||}")]
        public StarEnum? Star;

        public static IForm<MessageModel> BuildForm()
        {
            return new FormBuilder<MessageModel>()
                .Message("Welcome to ChatCode")
                .Field(nameof(AnalysisText))
                .Field(nameof(Email))
                .Field(nameof(Age))
                .Field(nameof(Gender))
                .Field(nameof(Description))
                .Field(nameof(Image))
                .Field(nameof(Facebook))
                .Field(nameof(Twitter))
                .Field(nameof(LinkedIn))
                .Field(nameof(IsPublish))
                .Field(nameof(Star))
                .OnCompletion(async (context, state) =>
                {
                    var client = new DataClient();

                    if (state.IsPublish == PublishEnum.Yes)
                    {
                        var analysis = new CreateAnalysisModel()
                        {
                            Text = state.AnalysisText
                        };

                        var textAnalaysisKeyword = await client.RequestAnalysis(analysis);

                        var model = new CreateAboutMeModel()
                        {
                            Email = state.Email,
                            NameSurname = MessagesController.UserName,
                            Age = "23",
                            Gender = state.Gender.Value.ToString(),
                            Description = state.Description,
                            ImageUrl = "image-url",
                            Background = "#ffffff",
                            Foreground = "#000000",
                            AnalysisText = textAnalaysisKeyword
                        };

                        var response = await client.SendInformation(model);

                        if (response.IsSuccess == true)
                        {
                            await context.PostAsync("https://chatcode.blob.core.windows.net/chatcodecontainer/aboutme.html");

                            var social = new CreateSocialMediaModel()
                            {
                                Email = state.Email,
                                Facebook = state.Facebook,
                                Twitter = state.Twitter,
                                LinkedIn = state.LinkedIn
                            };
                            var socialResponse = client.AddSocialMedia(social);

                            int websiteId = Convert.ToInt32(socialResponse.Result.Message);
                            var star = new CreateStartModel()
                            {
                                WebsiteId = websiteId,
                                Rate = (int)state.Star.Value
                            };

                            await context.PostAsync("https://chatcode.blob.core.windows.net/chatcodecontainer/aboutme.html");
                        }

                        else
                            await context.PostAsync("https://chatcode.blob.core.windows.net/chatcodecontainer/aboutme.html");
                    }

                    else
                        await context.PostAsync("You gave up");
                })
                .Build();
        }
    }
}