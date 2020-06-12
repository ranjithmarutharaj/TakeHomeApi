using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TakeHomeDataAccess;
using TakeHomeAPI.Models;
using TakeHomeAPI.Business;

namespace TakeHomeAPI.Controllers
{
    public class TakeHomeController : ApiController
    {
        public HttpResponseMessage GetAllContracts()
        {
            using (var entities = new TakeHomeEntities())
            {
                var result = entities.Contracts.ToList();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        public HttpResponseMessage CreateContract(ContractModel model)
        {
            TakeHomeBusiness.CreateContractPlans(model);

            return Request.CreateResponse(HttpStatusCode.OK);

        }

        public HttpResponseMessage UpdateContract(ContractModel model)
        {
            using (var entities = new TakeHomeEntities())
            {
                var getContract = entities.Contracts.Where(c => c.CustomerID == model.CustomerID).FirstOrDefault();

                if (null == getContract)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

                var contract = new Contract()
                {
                    Address = model.Address,
                    Country = model.Country,
                    CoveragePlan = model.CoveragePlan,
                    CustomerName = model.CustomerName,
                    DOB = model.DOB,
                    Gender = model.Gender,
                    NetPrice = model.NetPrice,
                    SaleDate = model.SaleDate
                };

                entities.Contracts.Add(contract);
                entities.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        public HttpResponseMessage DeleteContract(int contractId)
        {
            using (var entities = new TakeHomeEntities())
            {
                var contract = entities.Contracts.Where(c => c.CustomerID == contractId).FirstOrDefault();

                entities.Entry(contract).State = System.Data.Entity.EntityState.Deleted;
                entities.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}
