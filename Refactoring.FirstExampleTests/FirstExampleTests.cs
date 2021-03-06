using System.Collections.Generic;
using FluentAssertions;
using Xunit;

//https://github.com/tedmax100/refactoring-first-example/tree/1e667ef31bcd1dff33f62c724e7b39978405d3d4
namespace Refactoring.FirstExampleTests
{

    public class FirstExampleTests
    {
        [Fact]
        public void TestIfItStillWorksAsBefore()
        {
            // How does Martin Fowler calls this type of test?
            var theater = new Theater();

            var performances = new[]
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
            };
            var invoice = new Invoice("BigCo", performances);
            var plays = new Dictionary<string, Play>
            {
                {"hamlet", new Play("Hamlet", "tragedy")},
                {"as-like", new Play("As You Like It", "comedy")},
                {"othello", new Play("Othello", "tragedy")}
            };

            var statement = theater.Statement(invoice, plays);

            string expected = "Statement for BigCo\r\n";
            expected += " Hamlet: $650.00 (55 seats)\r\n";
            expected += " As You Like It: $580.00 (35 seats)\r\n";
            expected += " Othello: $500.00 (40 seats)\r\n";
            expected += "Amount owed is $1,730.00\r\n";
            expected += "You earned 47 credits";

            statement.Should().Be(expected);
        }
    }
}

