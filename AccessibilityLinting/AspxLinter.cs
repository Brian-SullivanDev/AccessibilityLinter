using AccessibilityLinting.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AccessibilityLinting
{

    public static class AspxLinter
    {

        private const string _htmlTagMatchRegex = "(<([^>]+)>)";

        private const string _htmlTagAttributeMatchRegex = @"(\w+)=[""']?((?:.(?![""']?\s+(?:\S+)=|\s*\/?[>""']))+.)[""']?";

        private const string _htmlIdAttributeName = "id";
        private const char _htmlAttributeNameValueDelimiter = '=';

        public static List<LintingError> ParseAspx(string content)
        {

            var tags = GetTags(content);

            return null;

        }

        private static List<HtmlTag> GetTags (string content)
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

        private static HtmlTag GetTag (string tagContent)
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

        private static HtmlAttribute GetAttribute (string html)
        {

            var attribute = new HtmlAttribute();

            var len = html.Length;

            var attributeBuilder = new StringBuilder(len);

            short index = 0;

            short delimiterIndex = -1;

            while(index < len && delimiterIndex == -1)
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

        private static HtmlEntity GetHtmlEntity (string html)
        {

            return HtmlEntity.CUSTOM;

        }

    }

}
