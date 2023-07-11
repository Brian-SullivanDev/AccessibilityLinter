using AccessibilityLinting.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessibilityLinting.Abstract
{

    public interface ILintingHelpSource
    {

        public Help GetHelpByRuleID(string ruleID);

    }

}
