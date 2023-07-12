using NUnit.Framework;
using AccessibilityLinting;
using static AccessibilityLinting.HtmlLintingHelper;
using AccessibilityLinting.Models;
using System.Collections.Generic;
using AccessibilityLinting.LinterRules;
using AccessibilityLinting.Abstract;
using AccessibilityLinting.Tests;
using AccessibilityLinting.Mock;

namespace AccessibilityLinting.Tests
{

    public class LinterTests
    {

        const string testID = "testID1";
        const string testClass = "testClass1";
        const string testDataAttributeName = "data-test";
        const string testDataAttributeValue = "testValue";
        const string testSecondDataAttributeName = "data-another-test";
        const string testSecondDataAttributeValue = "anotherTestValue";

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test_Parse_ReturnsExpectedLintingErrorsInSimpleHTMLCase()
        {

            string secondDivID = "testID2";

            string labelID = "labelID";
            string inputID = "inputID";

            string secondLabelID = "secondLabelID";
            string secondInputID = "secondInputID";

            string mockHTML = @$"
<html>
    <head>
        <title>Test Title</title>
    </head>

    <body>

        <div id='{testID}' class=""{testClass}"" {testDataAttributeName}='{testDataAttributeValue}'
            {testSecondDataAttributeName}='{testSecondDataAttributeValue}'>
            innerHTML goes here
        </div>

        <label id='{labelID}'>label for the input</label>
        <input id='{inputID}'>

        <label id='{secondLabelID}'>label for the input</label>
        <input id='{secondInputID}'>

    </body>
</html>
";

            var testContext = new LintingContext()
            {
                Content = mockHTML,
                Identifier = "Second Unit Test",
                FileType = FileType.HTML
            };

            var mockDataSource = new MockHtmlLintingDataSource();

            var testLinter = new Linter(mockDataSource, null);

            var linterErrors = testLinter.Parse(testContext);

            Assert.AreEqual(2, linterErrors.Count);

        }

    }

}