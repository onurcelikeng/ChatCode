namespace ChatCode.Bot.Models
{
    public sealed class CreateAboutMeModel
    {
        public string Email { get; set; }

        public string NameSurname { get; set; }

        public string Age { get; set; }

        public string Gender { get; set; }

        public string Description { get; set; }

        public string[] AnalysisText { get; set; }

        public string ImageUrl { get; set; }

        public string Background { get; set; }

        public string Foreground { get; set; }
    }
}