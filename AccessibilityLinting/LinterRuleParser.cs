using AccessibilityLinting.LinterRules;
using AccessibilityLinting.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessibilityLinting
{

    public static class LinterRuleParser
    {

        public const string BAD_RULE_META_DATA_EXCEPTION_GENERAL =
            "Encountered an exception attempting to parse provided metadata into a Linter rule";

        public static ILinterRule GetLinterRule(Rule rule)
        {

            if (rule.LinterRule.RuleType == LinterRuleType.HTML_REQUIRED_ATTRIBUTE)
            {
                return new HtmlRequiredAttributeLinter(rule.LinterRule.RuleMetaData);
            }

            throw new Exception(BAD_RULE_META_DATA_EXCEPTION_GENERAL);

        }

    }

}
