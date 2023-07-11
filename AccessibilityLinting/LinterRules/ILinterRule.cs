using AccessibilityLinting.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessibilityLinting.LinterRules
{

    public interface ILinterRule
    {

        public bool RuleAppliesToContext(LintingContext context);

        public List<LintingError> RunAgainstContext(LintingContext context);

    }

}
