namespace ChatCode.Models
{
    public class AboutMeModel
    {
        public string Email { get; set; } // for UserT. return UserId From UserT.

        public string NameSurname { get; set; }

        public string Image { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public string Description { get; set; }

        public string Background { get; set; }

        public string Foreground { get; set; }

        public SocialModel SocialModel { get; set; }
    }
}