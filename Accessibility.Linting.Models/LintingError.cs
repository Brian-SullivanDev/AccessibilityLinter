using System;
using System.Collections.Generic;
using System.Text;

namespace AccessibilityLinting.Models
{

    public class LintingError
    {

        /// <summary>
        /// ID from the Rule from which the Linting Error was caught
        /// </summary>
        public string RuleID { get; set; }

        /// <summary>
        /// On which Line does the error begin
        /// </summary>
        public string LineNumber { get; set; }

        /// <summary>
        /// With which character does the error begin
        /// </summary>
        public short StartingCharacter { get; set; }

        /// <summary>
        /// From the StartingCharacter, how many characters are included in scope of this particular error
        /// </summary>
        public short CharacterLength { get; set; }

    }

}
