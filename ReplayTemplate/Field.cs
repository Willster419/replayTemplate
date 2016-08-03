using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplayTemplate
{
    class Field
    {
        public static int DELIMITER = 3;
        public static int PANEL_WIDTH = 330;
        public static int PANEL_HEIGHT = 45;
        public static int TEXTBOX_LOCATION_Y = 20;
        public static int LABEL_WIDTH = 50;
        public static int LABEL_HEIGHT = 13;
        public static int TEXTBOX_SIZE_WIDTH = 300;
        public static int TEXTBOX_SIZE_HEIGHT = 20;
        public string name { get; set; }
        public int type { get; set; }
        public string value { get; set; }
        //1=standard, 2=date, 3=victoryDefeat, 4=youtube
        public Field(string fieldName, int fieldType = 1)
        {
            name = fieldName;
            type = fieldType;
        }
        //have conditions for if it's date victoryDefeat of youtube
    }
}
