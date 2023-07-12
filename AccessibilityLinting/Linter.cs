using AccessibilityLinting.Abstract;
using AccessibilityLinting.LinterRules;
using AccessibilityLinting.Models;
using System;
using System.Collections.Generic;
using static AccessibilityLinting.LinterRuleParser;

namespace AccessibilityLinting
{

    public class Linter
    {

        private ILintingDataSource _dataSource;
        private ILintingHelpSource _helpSource;

        /// <summary>
        /// List of Rules to use when Linting a file - pull this from whichever data source is chosen
        /// </summary>
        private List<Rule> _rules = new List<Rule>();

        public Linter (ILintingDataSource dataSource, ILintingHelpSource helpSource)
        {

            _dataSource = dataSource;
            _helpSource = helpSource;
            _rules = _dataSource.FetchRules();

        }

        public Dictionary<string, List<LintingError>> ParseAll(IEnumerable<LintingContext> contexts)
        {

            var errorMappings = new Dictionary<string, List<LintingError>>();

            foreach (var context in contexts)
            {

                var identifier = context.Identifier;

                var errors = Parse(context);

                errorMappings.Add(identifier, errors);

            }

            return errorMappings;

        }

        public List<LintingError> Parse(LintingContext context)
        {

            var errors = new List<LintingError>();

            foreach (var rule in _rules)
            {

                var linter = GetLinterRule(rule);

                if (linter.RuleAppliesToContext(context))
                {
                    errors.AddRange(linter.RunAgainstContext(context));
                }

            }

            return errors;

        }

    }

}
