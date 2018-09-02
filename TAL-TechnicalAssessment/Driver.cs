using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TAL_TechnicalAssessment
{
    class Driver
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                throw new System.ArgumentException("At least one CSV file must be supplied", "args");
            }

            string fn = @args[0];
            TechnicalAssessmentAnswer Answer = new TechnicalAssessmentAnswer(fn);
            IEnumerable<ClientRecord> IEClientRecord = Answer.ReadCSVFile();

            foreach (ClientRecord cr in IEClientRecord)
            {
                ClientRecord s = cr;
                Console.WriteLine(s);
            }
        }

        /**
         * Remove dollar sign prefix from the input.
         */
        private static String RemoveDollarSignPrefix(string input)
        {
            //return input.StartsWith("$") ? input.Substring(1) : input;
            string s = Regex.Replace(input, @"[""$]", string.Empty);
            return s;
        }

    }

    class TechnicalAssessmentAnswer
        {
            public TextReader Reader;
            private bool isFirstLine;

            public TechnicalAssessmentAnswer(String CSVFileName)
            {
                Reader = File.OpenText(CSVFileName);
                isFirstLine = true;
            }

            public IEnumerable<ClientRecord> ReadCSVFile()
            { 
                IEnumerable<ClientRecord> allValues = null; 
                var csvReader = new CsvReader(Reader);
                csvReader.Configuration.RegisterClassMap<ConvertUsingClassMap>();
                csvReader.Configuration.HasHeaderRecord = true;
                allValues = csvReader.GetRecords<ClientRecord>();
                return allValues;
            }
        }

    sealed class ConvertUsingClassMap : ClassMap<ClientRecord>
    {
        public ConvertUsingClassMap()
        {
            Map(m => m.CompanyCode).Name("Company Code");
            Map(m => m.PolicyNo).Name("Policy number");
            Map(m => m.FirstName).Name("Life insured first name");
            Map(m => m.LastName).Name("Life insured last name");
            Map(m => m.DateOfBirth).Name("Date of birth");
            Map(m => m.Gender).Name("Gender");
            //Map(m => m.CoverType);
            Map(m => m.AmountInsured).ConvertUsing(row =>
            {
                var sAmountInsured = row.GetField("Amount insured");
                var sb_trim = Regex.Replace(sAmountInsured, @"[$,]", "");
                sb_trim = Regex.Replace(sb_trim, @"/mth", "");

                return Convert.ToDecimal(sb_trim);
            });
            Map(m => m.PremiumAmount).ConvertUsing(row =>
            {
                var sPremiumAmount = row.GetField("Current premium");
                var sb_trim = Regex.Replace(sPremiumAmount, @"[$,]", "");

                return Convert.ToDecimal(sb_trim);
            });
            Map(m => m.AnnualPremium).ConvertUsing(row =>
            {
                var sPremiumAmount = row.GetField("Current premium");
                var sb_trim = Regex.Replace(sPremiumAmount, @"[$,]", "");

                var premiumAmount = Convert.ToDecimal(sb_trim);
                var freq = row.GetField("Payment frequency");
                freq = freq.ToLower();
                if (freq == "monthly")
                {
                    premiumAmount = 12 * premiumAmount;
                }
                return premiumAmount;
            });
            Map(m => m.Frequency).Name("Payment frequency");
            // Calculate premium amount * frequency.
            //Map(m => m.PolicyStatus) // Calculate
            Map(m => m.CommencementDate).Name("Policy start date");
            Map(m => m.PremiumType).Name("Premium type");
            Map(m => m.PolicyType).Name("Product");
            Map(m => m.CoverType).Name("Cover");
            Map(m => m.BenefitPeriod).Name("Benefit period");
            Map(m => m.WaitPeriod).Name("Waiting period");
            Map(m => m.CancellationDate).Name("Cancellation effective date");
            Map(m => m.RenewalDate).Name("Endorsement effective date");
            //If Cover Status is "in force" or "provisional renewal", set the CoverActive flag to true. Else, the CoverActive flag should be set false.
            Map(m => m.CoverActive).ConvertUsing(row =>
            {
                var coverStatus = row.GetField("Cover status");
                coverStatus = coverStatus.ToLower();
                if (coverStatus == "in force" || coverStatus == "Lapsed")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });

            Map(m => m.CoverActiveType).ConvertUsing(row =>
            {
                var coverStatus = row.GetField("Cover status");
                coverStatus = coverStatus.ToLower();
                if (coverStatus == "in force")
                {
                    return CoverActiveType.Active;
                }
                else if (coverStatus == "Lapsed")
                {
                    return CoverActiveType.ActiveProvisional;
                }
                else
                {
                    return CoverActiveType.Inactive;
                }
            });

            Map(m => m.PolicyStatus).ConvertUsing(row =>
            {
                var policyStatus = row.GetField("Policy status");
                if (policyStatus == null || policyStatus == "")
                {
                    return "";
                }
                policyStatus = policyStatus.ToLower();
                if (policyStatus == "in force" || policyStatus == "Lapsed")
                {
                    return "Active";
                }
                else if (policyStatus == "cancelled")
                {
                    return "Cancelled";
                }
                else
                {
                    return "Initial";
                }
            });
        }
    }
}
