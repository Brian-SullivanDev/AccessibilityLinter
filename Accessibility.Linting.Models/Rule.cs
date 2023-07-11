using System;
using System.Collections.Generic;

namespace AccessibilityLinting.Models
{

    public class Rule
    {

        /// <summary>
        /// This is the Rule ID, reference https://dequeuniversity.com/rules/axe/4.7 - Rule ID column in the "WCAG 2.0 Level A & AA Rules" table
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Short name for the rule
        /// </summary>
        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Rule for the Linter to use to check if the syntax is valid
        /// </summary>
        public LinterRule LinterRule { get; set; }

        public List<string> Tags { get; set; } = null;

        public Impact? Impact { get; set; } = null;

        public ACTRule ACTRule { get; set; } = null;

    }

}
