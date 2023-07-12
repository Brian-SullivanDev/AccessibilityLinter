using NUnit.Framework;
using AccessibilityLinting;
using static AccessibilityLinting.AspxLintingHelper;
using AccessibilityLinting.Models;
using System.Collections.Generic;

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

        [TestCase("<html>", HtmlEntity.HTML)]
        [TestCase("<head>", HtmlEntity.HEAD)]
        [TestCase("<title>", HtmlEntity.TITLE)]
        [TestCase("<body>", HtmlEntity.BODY)]
        [Parallelizable(ParallelScope.All)]
        public void Test_GetHtmlEntity_ReturnsExpectedEntityPerHtmlProvided(string testHtml, HtmlEntity expectedEntity)
        {

            var derivedEntity = GetHtmlEntity(testHtml);

            Assert.AreEqual(expectedEntity, derivedEntity);

        }

        [Test]
        public void Test_GetTag_ReturnsDerivedHtmlTagObjectWithPropertiesAsExpected()
        {

            string mockHtml = $@"<div id='{testID}' class=""{testClass}"" {testDataAttributeName}='{testDataAttributeValue}'
            {testSecondDataAttributeName}='{testSecondDataAttributeValue}'>
            innerHTML goes here
        </div>";

            var expectedHtmlTag = new HtmlTag()
            {
                Attributes = new List<HtmlAttribute>()
                {
                    new HtmlAttribute()
                    {
                        Name = "id",
                        Value = testID
                    },
                    new HtmlAttribute()
                    {
                        Name = "class",
                        Value = testClass
                    },
                    new HtmlAttribute()
                    {
                        Name = testDataAttributeName,
                        Value = testDataAttributeValue
                    },
                    new HtmlAttribute()
                    {
                        Name = testSecondDataAttributeName,
                        Value = testSecondDataAttributeValue
                    }
                },
                Identifier = testID,
                Entity = HtmlEntity.DIV
            };

            var derivedHtmlTag = GetTag(mockHtml);

            CollectionAssert.AreEquivalent(expectedHtmlTag.Attributes, derivedHtmlTag.Attributes);
            Assert.AreEqual(expectedHtmlTag.Entity, derivedHtmlTag.Entity);
            Assert.AreEqual(expectedHtmlTag.Identifier, derivedHtmlTag.Identifier);

        }

        [Test]
        public void Test_GetTags_ReturnsDerivedHtmlTagObjectCollectionWithPropertiesAsExpected()
        {



            var secondDivID = "testID2";

            string mockHTML = @$"
        <div id='{testID}' class=""{testClass}"" {testDataAttributeName}='{testDataAttributeValue}'
            {testSecondDataAttributeName}='{testSecondDataAttributeValue}'>
            innerHTML goes here
        </div>

        <span id='{secondDivID}' />
";

            var testContext = new LintingContext()
            {
                Content = mockHTML,
                Identifier = "First Unit Test"
            };

            var derivedTags = GetTags(testContext.Content);

            Assert.AreEqual(2, derivedTags.Count);

            var firstDerviedTag = derivedTags[0];

            Assert.AreEqual(4, firstDerviedTag.Attributes.Count);
            Assert.AreEqual(HtmlEntity.DIV, firstDerviedTag.Entity);
            Assert.AreEqual(testID, firstDerviedTag.Identifier);

            var secondDerivedTag = derivedTags[1];

            Assert.AreEqual(1, secondDerivedTag.Attributes.Count);
            Assert.AreEqual(HtmlEntity.SPAN, secondDerivedTag.Entity);
            Assert.AreEqual(secondDivID, secondDerivedTag.Identifier);

        }

        [Test]
        public void Test_GetTags_ReturnsDerivedHtmlTagObjectCollectionWithPropertiesAsExpectedWithFullHtml()
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

        <span id='{secondDivID}' />

    </body>
</html>
";

            var testContext = new LintingContext()
            {
                Content = mockHTML,
                Identifier = "First Unit Test"
            };

            var derivedTags = GetTags(testContext.Content);

            Assert.AreEqual(6, derivedTags.Count);

            var firstDerivedTag = derivedTags[0];

            Assert.AreEqual(0, firstDerivedTag.Attributes.Count);
            Assert.AreEqual(HtmlEntity.HTML, firstDerivedTag.Entity);
            Assert.IsNull(firstDerivedTag.Identifier);

            var secondDerivedTag = derivedTags[1];

            Assert.AreEqual(0, secondDerivedTag.Attributes.Count);
            Assert.AreEqual(HtmlEntity.HEAD, secondDerivedTag.Entity);
            Assert.IsNull(secondDerivedTag.Identifier);

            var thirdDerivedTag = derivedTags[2];

            Assert.AreEqual(0, thirdDerivedTag.Attributes.Count);
            Assert.AreEqual(HtmlEntity.TITLE, thirdDerivedTag.Entity);
            Assert.IsNull(thirdDerivedTag.Identifier);

            var fourthDerivedTag = derivedTags[3];

            Assert.AreEqual(0, fourthDerivedTag.Attributes.Count);
            Assert.AreEqual(HtmlEntity.BODY, fourthDerivedTag.Entity);
            Assert.IsNull(fourthDerivedTag.Identifier);

            var fifthDerivedTag = derivedTags[4];

            Assert.AreEqual(4, fifthDerivedTag.Attributes.Count);
            Assert.AreEqual(HtmlEntity.DIV, fifthDerivedTag.Entity);
            Assert.AreEqual(testID, fifthDerivedTag.Identifier);

            var sixthDerivedTag = derivedTags[5];

            Assert.AreEqual(1, sixthDerivedTag.Attributes.Count);
            Assert.AreEqual(HtmlEntity.SPAN, sixthDerivedTag.Entity);
            Assert.AreEqual(secondDivID, sixthDerivedTag.Identifier);

        }
    
    }

}