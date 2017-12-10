using System.Collections.Generic;

namespace ChatCode.Models
{
    public class Documents
    {
        public List<string> keyPhrases { get; set; }
        public string id { get; set; }
    }

    public class AnalysisModel
    {
        public List<Documents> documents { get; set; }
        public List<object> errors { get; set; }
    }
}