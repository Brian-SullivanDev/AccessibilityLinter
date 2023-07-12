using AccessibilityLinting.Abstract;
using AccessibilityLinting.Models;
using System;
using System.Collections.Generic;

namespace AccessibilityLinting.Mock
{

    public class MockHtmlLintingDataSource : ILintingDataSource
    {
        public List<Rule> FetchRules()
        {
            return MockedDataSources.GetTestHTMLDataSourceRules();
        }

    }

    public static class MockedDataSources
    {

        public static List<Rule> GetTestHTMLDataSourceRules()
        {

            var rules = new List<Rule>();

            var firstRuleID = "TEST123";
            var requiredAttributeName = "for";
            var matchedEntityType = HtmlEntity.LABEL;
            var cannotBeEmpty = true;

            var forAttributeRequiredLinterRule = new LinterRule()
            {
                RuleCategory = LinterRuleCategory.HTML,
                RuleType = LinterRuleType.HTML_REQUIRED_ATTRIBUTE,
                RuleMetaData = new List<object>()
                {
                    firstRuleID,
                    requiredAttributeName,
                    matchedEntityType,
                    cannotBeEmpty
                }
            };

            var requiredForAttributeInLabelTagsRule = new Rule()
            {
                ID = firstRuleID,
                Name = "Mock Rule 1",
                Description = "This is just our first Mock Rule",
                LinterRule = forAttributeRequiredLinterRule
            };

            rules.Add(requiredForAttributeInLabelTagsRule);

            return rules;

        }

    }

}
