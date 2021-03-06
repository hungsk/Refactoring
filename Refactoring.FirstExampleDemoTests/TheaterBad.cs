using System;
using System.Collections.Generic;
using System.Globalization;

namespace Refactoring.FirstExampleDemoTests
{
    public class TheaterBad
    {
        public string Statement(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = $"Statement for {invoice.Customer}\r\n";
            var format = new CultureInfo("en-US", false).NumberFormat;
            format.CurrencyDecimalDigits = 2;
            format.CurrencySymbol = "$";
            format.CurrencyDecimalSeparator = ",";
            format.CurrencyGroupSeparator = ".";

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var thisAmount = 0;

                switch (play.Type)
                {
                    case "tragedy":
                        thisAmount = 40000;
                        if (perf.Audience > 30)
                        {
                            thisAmount += 1000 * (perf.Audience - 30);
                        }
                        break;
                    case "comedy":
                        thisAmount = 30000;
                        if (perf.Audience > 20)
                        {
                            thisAmount += 10000 + 500 * (perf.Audience - 20);
                        }

                        thisAmount += 300 * perf.Audience;
                        break;
                    default:
                        throw new Exception($"unknown type: {play.Type}");
                }

                // add volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((double)perf.Audience / 5);

                // print line for this order
                result += $" {play.Name}: {(thisAmount / 100).ToString("C", new CultureInfo("en-US"))} ({perf.Audience} seats)\r\n";
                totalAmount += thisAmount;
            }
            result += $"Amount owed is {(totalAmount / 100).ToString("C", new CultureInfo("en-US"))}\r\n";
            result += $"You earned {volumeCredits} credits";
            return result;
        }
    }
}