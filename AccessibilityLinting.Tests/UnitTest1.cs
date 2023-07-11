using NUnit.Framework;
using AccessibilityLinting;
using static AccessibilityLinting.AspxLintingHelper;
using AccessibilityLinting.Models;

namespace AccessibilityLinting.Tests
{

    public class Tests
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
        //Single Quote delimiter cases
        [TestCase("'", "testValue")]
        [TestCase("'", @"test""Value")]
        [TestCase("'", "test&aposValue")]
        //Double Quote delimiter cases
        [TestCase(@"""", "testValue")]
        [TestCase(@"""", "test&quot;Value")]
        [TestCase(@"""", "test'Value")]
        [Parallelizable(ParallelScope.All)]
        public void Test_GetAttribute_ProperlyHandlesSingleQuotesAroundValueWithoutInteriorQuotes(string attributeValueDelimiter, string attributevalue)
        {

            var mockAttributeHtml = $@"{testDataAttributeName}={attributeValueDelimiter}{attributevalue}{attributeValueDelimiter}";

            var derivedAttribute = GetAttribute(mockAttributeHtml);

            Assert.AreEqual(testDataAttributeName, derivedAttribute.Name);
            Assert.AreEqual(attributevalue, derivedAttribute.Value);

        }

        [Test]
        [TestCase("<div id='exampleId'>", HtmlEntity.DIV)]
        [TestCase("<DIV id='exampleId'>", HtmlEntity.DIV)]
        [TestCase("<dIv id='exampleId'>", HtmlEntity.DIV)]
        [TestCase("<span id='exampleId'>", HtmlEntity.SPAN)]
        [TestCase("<img id='exampleId'>", HtmlEntity.IMAGE)]
        [TestCase("<select id='exampleId'>", HtmlEntity.SELECT)]
        [TestCase("<option id='exampleId'>", HtmlEntity.OPTION)]
        [TestCase("<ul id='exampleId'>", HtmlEntity.LIST)]
        [TestCase("<ol id='exampleId'>", HtmlEntity.LIST)]
        [TestCase("<li id='exampleId'>", HtmlEntity.LIST_ITEM)]
        [TestCase("<p id='exampleId'>", HtmlEntity.PARAGRAPH)]
        [TestCase("<h1 id='exampleId'>", HtmlEntity.HEADER)]
        [TestCase("<h2 id='exampleId'>", HtmlEntity.HEADER)]
        [TestCase("<h3 id='exampleId'>", HtmlEntity.HEADER)]
        [TestCase("<h4 id='exampleId'>", HtmlEntity.HEADER)]
        [TestCase("<h5 id='exampleId'>", HtmlEntity.HEADER)]
        [TestCase("<h6 id='exampleId'>", HtmlEntity.HEADER)]
        [TestCase("<a id='exampleId'>", HtmlEntity.ANCHOR)]
        [TestCase("<table id='exampleId'>", HtmlEntity.TABLE)]
        [TestCase("<tr id='exampleId'>", HtmlEntity.TABLE_ROW)]
        [TestCase("<td id='exampleId'>", HtmlEntity.TABLE_DATA)]
        [TestCase("<button id='exampleId'>", HtmlEntity.BUTTON)]
        [TestCase("<input id='exampleId'>", HtmlEntity.INPUT)]
        [TestCase("<label id='exampleId'>", HtmlEntity.LABEL)]
        [TestCase("<ukg-button id='exampleId'>", HtmlEntity.CUSTOM)]
        [TestCase("<asp:label id='exampleId'>", HtmlEntity.CUSTOM)]
        [Parallelizable(ParallelScope.All)]
        public void Test_GetHtmlEntity_ReturnsExpectedEntityPerHtmlProvided(string testHtml, HtmlEntity expectedEntity)
        {

            var derivedEntity = GetHtmlEntity(testHtml);

            Assert.AreEqual(expectedEntity, derivedEntity);

        }

        [Test]
        public void Test1()
        {

            

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

            var tags = GetTags(testContext.Content);

        }
    
    }

}