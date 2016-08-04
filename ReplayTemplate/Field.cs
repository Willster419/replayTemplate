using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplayTemplate
{
    class Field
    {
        public string name { get; set; }
        public int type { get; set; }
        public string value { get; set; }
        //1=standard, 2=date, 3=victoryDefeat, 4=youtube
        public bool duplicate { get; set; }
        public Field(string fieldName, int fieldType = 1)
        {
            name = fieldName;
            type = fieldType;
        }
        //have conditions for if it's date victoryDefeat of youtube
    }
}
