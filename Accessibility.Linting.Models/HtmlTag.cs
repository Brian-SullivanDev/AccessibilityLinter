using System;
using System.Collections.Generic;
using System.Text;

namespace AccessibilityLinting.Models
{

    public class HtmlTag
    {

        public HtmlEntity Entity { get; set; }

        /// <summary>
        /// This ought to always be populated, but it is possible to be null if id attribute is not explicitly defined
        /// </summary>
        public string Identifier { get; set; } = null;

        public List<HtmlAttribute> Attributes { get; set; }

        public string OriginalHTML { get; set; }

        public int IndexWithinFile { get; set; }

        public int LineNumberTagStartsOn { get; set; }

    }

}
