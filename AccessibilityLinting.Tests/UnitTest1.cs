using NUnit.Framework;
using AccessibilityLinting;
using static AccessibilityLinting.AspxLinter;
using AccessibilityLinting.Models;

namespace AccessibilityLinting.Tests
{

    public class Tests
    {

        [SetUp]
        public void Setup()
        {
    
        }

        [Test]
        public void Test1()
        {

            var testID = "testID1";
            var testClass = "testClass1";
            var testDataAttributeName = "data-test";
            var testDataAttributeValue = "testValue";
            var testSecondDataAttributeName = "data-another-test";
            var testSecondDataAttributeValue = "anotherTestValue";

            var secondDivID = "testID2";

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

        <div id='{secondDivID}' />

    </body>
</html>
";

            var testContext = new LintingContext()
            {
                Content = mockHTML,
                Identifier = "First Unit Test"
            };

            var errors = ParseAspx(testContext.Content);

        }
    
    }

}