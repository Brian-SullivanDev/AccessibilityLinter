using AccessibilityLinting.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static AccessibilityLinting.HtmlLintingHelper;

namespace AccessibilityLinting.LinterRules
{

    public class HtmlRequiredAttributeLinter : ILinterRule
    {

        public const string REQUIRED_ATTRIBUTE_BAD_META_DATA_EXCEPTION_MESSAGE_GENERIC = 
            "Could not properly instantiate a required attribute linter from the provided metadata";
        public const string REQUIRED_ATTRIBUTE_BAD_META_DATA_EXCEPTION_MESSAGE_BAD_RULE_NAME_PARAM_TYPE =
            "Could not properly instantiate a required attribute linter from the provided rule name metadata as provided object was not a string (expected string for ID of the rule per Deque University)";
        public const string REQUIRED_ATTRIBUTE_BAD_META_DATA_EXCEPTION_MESSAGE_BAD_NAME_PARAM_TYPE =
            "Could not properly instantiate a required attribute linter from the provided attribute name metadata as provided object was not a string (expected string for the name of the expected attribute)";
        public const string REQUIRED_ATTRIBUTE_BAD_META_DATA_EXCEPTION_MESSAGE_BAD_ENTITY_PARAM_TYPE =
            "Could not properly instantiate a required attribute linter from the provided html entity metadata as provided object was not an HTML entity (expected string for the name of the expected attribute)";
        public const string REQUIRED_ATTRIBUTE_BAD_META_DATA_EXCEPTION_MESSAGE_BAD_EMPTY_FLAG_PARAM_TYPE =
            "Could not properly instantiate a required attribute linter from the provided html entity metadata as provided object was not a boolean (expected boolean to tell if the attribute value cannot be empty)";

        private string rule;
        private string requiredAttributeName;
        private HtmlEntity requiredAttributeEntityType;
        private bool attributeCannotBeEmpty = false;

        public string RuleID
        {
            get
            {
                return rule;
            }
        }

        public string RequiredAttributeName
        {
            get
            {
                return requiredAttributeName;
            }
        }

        public HtmlEntity RequiredAttributeEntityType
        {
            get
            {
                return requiredAttributeEntityType;
            }
        }

        public bool AttributeCannotBeEmpty
        {
            get
            {
                return attributeCannotBeEmpty;
            }
        }

        public HtmlRequiredAttributeLinter(string ruleID, string attributeName, HtmlEntity matchHtmlEntity, bool cannotBeEmpty)
        {

            rule = ruleID;
            requiredAttributeName = attributeName;
            requiredAttributeEntityType = matchHtmlEntity;
            attributeCannotBeEmpty = cannotBeEmpty;

        }

        public HtmlRequiredAttributeLinter(List<object> ruleMetaData)
        {

            if (ruleMetaData.Count != 4)
            {
                throw new Exception(REQUIRED_ATTRIBUTE_BAD_META_DATA_EXCEPTION_MESSAGE_GENERIC);
            }

            var ruleIDMetaData = ruleMetaData[0];

            if (!(ruleIDMetaData is string))
            {
                throw new Exception(REQUIRED_ATTRIBUTE_BAD_META_DATA_EXCEPTION_MESSAGE_BAD_RULE_NAME_PARAM_TYPE);
            }

            var attributeNameMetaData = ruleMetaData[1];

            if (!(attributeNameMetaData is string))
            {
                throw new Exception(REQUIRED_ATTRIBUTE_BAD_META_DATA_EXCEPTION_MESSAGE_BAD_NAME_PARAM_TYPE);
            }

            var matchedEntityMetaData = ruleMetaData[2];

            if (!(matchedEntityMetaData is HtmlEntity))
            {
                throw new Exception(REQUIRED_ATTRIBUTE_BAD_META_DATA_EXCEPTION_MESSAGE_BAD_ENTITY_PARAM_TYPE);
            }

            var emptyAttributeMetaData = ruleMetaData[3];

            if (!(emptyAttributeMetaData is bool))
            {
                throw new Exception(REQUIRED_ATTRIBUTE_BAD_META_DATA_EXCEPTION_MESSAGE_BAD_EMPTY_FLAG_PARAM_TYPE);
            }

            rule = ruleIDMetaData as string;
            requiredAttributeName = attributeNameMetaData as string;
            requiredAttributeEntityType = (matchedEntityMetaData as HtmlEntity?).Value;
            attributeCannotBeEmpty = (emptyAttributeMetaData as bool?).Value;

        }

        public bool RuleAppliesToContext(LintingContext context)
        {

            if (context.FileType != FileType.HTML)
            {
                return false;
            }

            return true;

        }

        public List<LintingError> RunAgainstContext(LintingContext context)
        {

            var errors = new List<LintingError>();

            var derivedTags = GetTags(context.Content);

            foreach (var derivedTag in derivedTags)
            {

                var foundAttribute = false;

                if (derivedTag.Entity == requiredAttributeEntityType)
                {

                    foreach (var attribute in derivedTag.Attributes)
                    {

                        if (attribute.Name.ToLower() == requiredAttributeName)
                        {

                            foundAttribute = true;

                            if (attributeCannotBeEmpty && attribute.Value.Equals(string.Empty))
                            {

                                var lineNumber = derivedTag.LineNumberTagStartsOn;
                                var startingCharacter = (short)(attribute.Index);
                                var characterLength = (short)(attribute.Name.Length + 3);

                                errors.Add(InlineLintingError(lineNumber, startingCharacter, characterLength));
                            }

                        }

                        break;

                    }

                    if (!foundAttribute)
                    {

                        errors.Add(InlineLintingError(derivedTag.LineNumberTagStartsOn, 0, (short)(derivedTag.OriginalHTML.Length)));

                    }

                }

            }

            return errors;

        }

        private LintingError InlineLintingError(int lineNumber, short startingCharacter, short characterLength)
        {

            return new LintingError()
            {
                RuleID = rule,
                LineNumber = lineNumber,
                StartingCharacter = startingCharacter,
                CharacterLength = characterLength
            };

        }

    }

}
