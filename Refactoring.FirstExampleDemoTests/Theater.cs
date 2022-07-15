﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace Refactoring.FirstExampleDemoTests
{
    public class Theater
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
                var thisAmount = AmountFor(perf, play);

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

        private static int AmountFor(Performance aPerformance, Play play)
        {
            int result;
            switch (play.Type)
            {
                case "tragedy":
                    result = 40000;
                    if (aPerformance.Audience > 30)
                    {
                        result += 1000 * (aPerformance.Audience - 30);
                    }
                    break;
                case "comedy":
                    result = 30000;
                    if (aPerformance.Audience > 20)
                    {
                        result += 10000 + 500 * (aPerformance.Audience - 20);
                    }

                    result += 300 * aPerformance.Audience;
                    break;
                default:
                    throw new Exception($"unknown type: {play.Type}");
            }

            return result;
        }
    }
}
