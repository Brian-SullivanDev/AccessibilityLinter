using System;
using System.Collections.Generic;
using System.Text;

namespace AccessibilityLinting.Models
{

    public class LintingContext
    {

        /// <summary>
        /// For our purposes, this will be filename in most cases
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// For our purposes, this will be file text in most cases
        /// </summary>
        public string Content { get; set; }

    }

}
