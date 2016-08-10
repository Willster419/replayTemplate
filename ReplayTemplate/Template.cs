using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplayTemplate
{
    class Template
    {
        public string threadURL { get; set; }
        public string clanName { get; set; }
        public string youtubeEmbedStartURL { get; set; }
        public string youtubeEmbedEndURL { get; set; }
        public int numFields { get; set; }
        public int templateType { get; set; }
        public string delimiter { get; set; }
        //1=single, 2=landing, 3=stronghold
        public List<Field> fieldList;
        public Template()
        {
            fieldList = new List<Field>();
        }
        public override string ToString()
        {
            if(templateType == 1) return "[" + clanName + "] - " + "single";
            else if(templateType == 2) return "[" + clanName + "] - " + "series";
            else if (templateType == 3) return "[" + clanName + "] - " + "stronghold";
            else return null;
        }
    }
}
