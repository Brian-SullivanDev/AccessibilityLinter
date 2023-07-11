using AccessibilityLinting.Abstract;
using System;

namespace AccessibilityLinting
{

    public class Linter
    {

        private ILintingDataSource _dataSource;
        private ILintingHelpSource _helpSource;

        public Linter (ILintingDataSource dataSource, ILintingHelpSource helpSource)
        {

            _dataSource = dataSource;
            _helpSource = helpSource;

        }

    }

}
