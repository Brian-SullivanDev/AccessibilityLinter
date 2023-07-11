using AccessibilityLinting.Models;
using System;
using System.Collections.Generic;

namespace AccessibilityLinting.Abstract
{

    public interface ILintingDataSource
    {

        public List<Rule> FetchRules();

    }

}
