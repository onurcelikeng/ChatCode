namespace ChatCode.Helpers
{
    public sealed class DomainHelper
    {
        public string ComputerVisionApiKey = "f75820f210054c4eb12976598c623290";

        public string TranslateApiKey = "90f34fd27d374314bf3ab98daa152cd7";

        public string TextAnalysisApiKey = "5f448d5dc5cd43b1b17aaa7c3eb6ec7f";


        public static DomainHelper Instance
        {
            get
            {
                return new DomainHelper();
            }
        }

    }
}