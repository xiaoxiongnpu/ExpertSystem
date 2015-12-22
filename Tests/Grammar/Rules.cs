﻿using System.Linq;
using NUnit.Framework;
using Sprache;

namespace Tests.Grammar
{
    [TestFixture]
    public class Rules
    {

        [Test]
        public void ParsingRule()
        {
            const string input = "Friends(X, Y) : Likes(X, Y) AND NOT Enemies(X, Y);";

            var rule = Logikek.Grammar.Rule.Parse(input);
            var firstCondition = rule.FirstCondition;
            var otherCondition = rule.Conditions.First();

            Assert.AreEqual("Friends", rule.Name);
            Assert.AreEqual("X", rule.Arguments.ToArray()[0].Name);
            Assert.AreEqual("Y", rule.Arguments.ToArray()[1].Name);
            Assert.AreEqual("Likes", firstCondition.Name);
            Assert.AreEqual(false, firstCondition.IsNegated);
            Assert.AreEqual("Enemies", otherCondition.Condition.Name);
            Assert.AreEqual(true, otherCondition.Condition.IsNegated);

        }

        [Test]
        public void ThrowsOnRuleMissingSemicolon()
        {
            const string input = "Friends(X, Y) : Likes(X, Y) AND NOT Enemies(X, Y)";

            Assert.Throws<ParseException>(() => { Logikek.Grammar.Rule.Parse(input); });
        }
    }
}