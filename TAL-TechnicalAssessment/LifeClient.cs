using System;

public class LifeClient
    {
        public string CompanyCode { get; set; }
        public string Company { get; set; }
        public string PolicyNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string CoverType { get; set; }
        public string PolicyStatus { get; set; }
        public bool CoverActive { get; set; }
        public CoverActiveType CoverActiveType { get; set; }
        public decimal PremiumAmount { get; set; }
        public string Frequency { get; set; }
        public decimal AnnualPremium { get; set; }
        public decimal Life { get; set; }
        public decimal Tpd { get; set; }
        public decimal Trauma { get; set; }
        public decimal MonthlyBenefit { get; set; }
        public string BenefitPeriod { get; set; }
        public string WaitingPeriod { get; set; }
        public DateTime? CommencementDate { get; set; }
        public DateTime? RenewalDate { get; set; }
        public DateTime? CancellationDate { get; set; }


        public LifeClient(string companyCode, string policyNo, DateTime dateOfBirth, string firstName, string lastName, bool coverActive, CoverActiveType coverActiveType)
        {
            this.CompanyCode = companyCode;
            this.PolicyNo = policyNo;
            this.DateOfBirth = dateOfBirth;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.CoverActive = coverActive;
            this.CoverActiveType = coverActiveType;
        }
    }