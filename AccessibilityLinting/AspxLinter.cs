using AccessibilityLinting.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static AccessibilityLinting.AspxLintingHelper;

namespace AccessibilityLinting
{

    public static class AspxLinter
    {

        

        public static List<LintingError> ParseAspx(string content)
        {

            var tags = GetTags(content);

            return null;

        }

        

    }

}
