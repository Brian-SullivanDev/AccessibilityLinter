using System;
using System.Collections.Generic;
using System.Text;

namespace AccessibilityLinting.Models
{

    public class HtmlAttribute
    {

        public string Name { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return $@"{Name}=`{Value}`";
        }

    }

}
