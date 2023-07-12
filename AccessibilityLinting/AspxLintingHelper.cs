using AccessibilityLinting.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AccessibilityLinting
{

    public static class AspxLintingHelper
    {

        public const string _htmlTagMatchRegex = "(<([^>]+)>)";
        public const string _htmlTagAttributeMatchRegex = @"([a-zA-Z.-]+)=[""']?((?:.(?![""']?\s+(?:\S+)=|\s*\/?[>""']))+.)[""']?";
        public const string _htmlIdAttributeName = "id";
        public const char _htmlAttributeNameValueDelimiter = '=';

        public const char _htmlEntityLeadingCharacter = '<';
        public const char _spaceCharacter = ' ';
        public const char _htmlTagCloseCharacter = '>';
        public const char _htmlTagTerminatorShorthandCharacter = '/';

        public readonly static List<char> _htmlTagEntitySearchTerminators = new List<char>() { _spaceCharacter, _htmlTagCloseCharacter, _htmlTagTerminatorShorthandCharacter };

        public const string _htmlEntityNotFoundExceptionMessage = "HTML entity provided does is not properly formed to have a defined HTML entity.";

        public static Dictionary<string, HtmlEntity> _htmlEntityMapping = new Dictionary<string, HtmlEntity>()
        {
            { "div", HtmlEntity.DIV },
            { "span", HtmlEntity.SPAN },
            { "img", HtmlEntity.IMAGE },
            { "select", HtmlEntity.SELECT },
            { "option", HtmlEntity.OPTION },
            { "ul", HtmlEntity.LIST },
            { "ol", HtmlEntity.LIST },
            { "li", HtmlEntity.LIST_ITEM },
            { "p", HtmlEntity.PARAGRAPH },
            { "h1", HtmlEntity.HEADER },
            { "h2", HtmlEntity.HEADER },
            { "h3", HtmlEntity.HEADER },
            { "h4", HtmlEntity.HEADER },
            { "h5", HtmlEntity.HEADER },
            { "h6", HtmlEntity.HEADER },
            { "a", HtmlEntity.ANCHOR },
            { "table", HtmlEntity.TABLE },
            { "tr", HtmlEntity.TABLE_ROW },
            { "td", HtmlEntity.TABLE_DATA },
            { "button", HtmlEntity.BUTTON },
            { "input", HtmlEntity.INPUT },
            { "label", HtmlEntity.LABEL },

            { "html", HtmlEntity.HTML },
            { "head", HtmlEntity.HEAD },
            { "title", HtmlEntity.TITLE },
            { "body", HtmlEntity.BODY },
        };

        public static List<HtmlTag> GetTags(string content)
        {

            List<HtmlTag> tags = new List<HtmlTag>();

            var regex = new Regex(_htmlTagMatchRegex);

            var allTags = regex.Matches(content);

            foreach (Match tag in allTags)
            {

                var resolvedTag = GetTag(tag.Value);

                if (resolvedTag != null)
                {
                    tags.Add(resolvedTag);
                }

            }

            return tags;

        }

        public static HtmlTag GetTag(string tagContent)
        {

            var attributes = new List<HtmlAttribute>();

            if (tagContent.IndexOf("</") == 0)
            {
                // This is a closing tag, and these tags have no attributes, ignore
                return null;
            }

            var regex = new Regex(_htmlTagAttributeMatchRegex);

            var htmlAttributeBlocks = regex.Matches(tagContent);

            string identifier = null;

            foreach (Match attributeBlock in htmlAttributeBlocks)
            {

                var html = attributeBlock.Value;

                var attribute = GetAttribute(html);

                attributes.Add(attribute);

                if (attribute.Name.ToLower() == _htmlIdAttributeName.ToLower())
                {
                    identifier = attribute.Value;
                }

            }

            var entity = GetHtmlEntity(tagContent);

            return new HtmlTag()
            {
                Attributes = attributes,
                Entity = entity,
                Identifier = identifier
            };

        }

        public static HtmlAttribute GetAttribute(string html)
        {

            var attribute = new HtmlAttribute();

            var len = html.Length;

            var attributeBuilder = new StringBuilder(len);

            short index = 0;

            short delimiterIndex = -1;

            while (index < len && delimiterIndex == -1)
            {

                char nextChar = html[index];

                if (nextChar == _htmlAttributeNameValueDelimiter)
                {
                    if (index == 0)
                    {
                        // Error case - should not be possible, return empty attribute if we ever run into this
                        return attribute;
                    }
                    else
                    {
                        delimiterIndex = index;
                    }
                }
                else
                {
                    attributeBuilder.Append(nextChar);
                }

                ++index;

            }

            string attributeName = attributeBuilder.ToString();

            // We want to skip over the equal and the opening quote delimiter, either " or '
            index += 1;

            string attributeValue = html.Substring(index, len - (index + 1));

            attribute.Name = attributeName;
            attribute.Value = attributeValue;

            return attribute;

        }

        public static HtmlEntity GetHtmlEntity(string html)
        {

            var derivedEntity = GetFirstHtmlEntityValue(html).ToLower();

            if (_htmlEntityMapping.ContainsKey(derivedEntity))
            {
                return _htmlEntityMapping[derivedEntity];
            }

            return HtmlEntity.CUSTOM;

        }

        private static string GetFirstHtmlEntityValue(string html)
        {

            var len = html.Length;

            var entityBuilder = new StringBuilder(len);

            short index = 0;

            while (index < len)
            {

                char nextChar = html[index];

                if (nextChar != _htmlEntityLeadingCharacter)
                {

                    if (_htmlTagEntitySearchTerminators.Contains(nextChar))
                    {
                        return entityBuilder.ToString();
                    }
                    else
                    {
                        entityBuilder.Append(nextChar);
                    }

                }

                ++index;

            }

            throw new Exception(_htmlEntityNotFoundExceptionMessage);

        }

    }

}
