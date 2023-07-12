using System;
using System.Collections.Generic;
using System.Text;

namespace AccessibilityLinting.Models
{

    public class HtmlAttribute
    {

        public string Name { get; set; }

        public string Value { get; set; }

        public int Index { get; set; }

        public override string ToString()
        {
            return $@"{Name}=`{Value}`";
        }

        public override bool Equals(object obj)
        {
            var other = obj as HtmlAttribute;
            return Name.Equals(other.Name) && Value.Equals(other.Value);
        }

    }

}
