using System;

class ClientRecord
    {
        public string CompanyCode { get; set; }
        public string Company { get; set; }
        public string PolicyNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string CoverType { get; set; }
        public decimal AmountInsured { get; set; }
        public decimal PremiumAmount { get; set; }
        public string Frequency { get; set; }
        public decimal AnnualPremium { get; set; }
        public string PolicyStatus { get; set; }
        public bool CoverActive { get; set; }
        public CoverActiveType CoverActiveType { get; set; }
        public DateTime? CommencementDate { get; set; }
        public DateTime? RenewalDate { get; set; }
        public DateTime? CancellationDate { get; set; }
        public string PremiumType { get; set; }
        public string PolicyType { get; set; }
        public string BenefitPeriod { get; set;}
        public string WaitPeriod { get; set; }
    }