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
        public List<Field> fieldList;
        public Template()
        {
            fieldList = new List<Field>();
        }
        public override string ToString()
        {
            return clanName;
        }
    }
}
