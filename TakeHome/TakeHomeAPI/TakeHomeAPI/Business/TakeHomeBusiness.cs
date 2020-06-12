using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TakeHomeAPI.Models;
using TakeHomeDataAccess;

namespace TakeHomeAPI.Business
{
    public static class TakeHomeBusiness
    {
        public static void CreateContractPlans(ContractModel contract)
        {
            var age = CalculateAge(contract.DOB);

            using (var plans = new TakeHomeEntities())
            {
                var coveragePlan = plans.CoveragePlans.ToList();
                var rateChart = plans.RateCharts.ToList();

                var countryPlans = coveragePlan.Where(x =>
                string.Equals(x.EligibilityCountry, contract.Country, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                var netPrice = rateChart.Where(y => string.Equals(y.CoveragePlan, countryPlans.CoveragePlan1, StringComparison.OrdinalIgnoreCase)
                && string.Equals(y.Gender, contract.Gender, StringComparison.OrdinalIgnoreCase));

                var price = GetNetPrice(netPrice.ToList());

                var addContract = new Contract()
                {
                    Address = contract.Address,
                    Country = contract.Country,
                    CoveragePlan = countryPlans.CoveragePlan1,
                    CustomerName = contract.CustomerName,
                    DOB = contract.DOB,
                    Gender = contract.Gender,
                    NetPrice = price,
                    SaleDate = contract.SaleDate
                };

                plans.Contracts.Add(addContract);
                plans.SaveChanges();
            }
        }

        private static int? GetNetPrice(List<RateChart> rate)
        {
            int? price = 0;
            foreach (var item in rate)
            {
                var strAge = item.Age;
                string opStr = strAge.Substring(0, 1);
                switch (opStr)
                {
                    case "<":
                        price = item.NetPrice;
                        break;
                    case ">":
                        price = item.NetPrice;
                        break;
                }
            }
            return price;
        }

        private static int CalculateAge(DateTime dOB)
        {
            DateTime currentDate = DateTime.Now;
            TimeSpan difference = currentDate.Subtract(dOB);
            DateTime age = DateTime.MinValue + difference;
            int ageInYears = age.Year - 1;
            return ageInYears;
        }
    }
}