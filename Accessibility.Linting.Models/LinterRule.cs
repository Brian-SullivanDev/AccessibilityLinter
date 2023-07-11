using System;
using System.Collections.Generic;
using System.Text;

namespace AccessibilityLinting.Models
{

    public class LinterRule
    {

        public LinterRuleCategory RuleCategory { get; set; }

        public LinterRuleType RuleType { get; set; }

        public List<object> RuleMetaData { get; set; }

    }

    public enum LinterRuleCategory
    {

        HTML,
        CSS

    }

    public enum LinterRuleType
    {

        HTML_REQUIRED_ATTRIBUTE,
        HTML_ATTRIBUTE_VALUE_IS_PROPER,
        HTML_ENTITY_NOT_IN_USE,
        HTML_ENTITY_IS_PRESENT

    }

}
