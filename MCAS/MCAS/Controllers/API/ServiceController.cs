using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections;
using MCAS.Web.Objects.ClaimObjectHelper;
using MCAS.Web.Objects.CommonHelper;
using System.Web;
using MCAS.Entity;
using MCAS.Models;
using MCAS.Globalisation;

namespace MCAS.Controllers.API
{
    public class ServiceController : ApiController
    {
        MCASEntities obj = new MCASEntities();
        public Hashtable GetLookUpsForServiceProvider(int accidentId)
        {
            Hashtable ht = new Hashtable();
            ht["PartyTypeList"] = ServiceProviderModel.FetchLookUpListWithOptions("PartyTypeList", false);
            ht["ServiceProviderOptionList"] = ServiceProviderModel.ServiceProviderList();
            ht["ClaimantNameList"] = ServiceProviderModel.SelectOnlyListNoSelect();
            ht["CompanyNameList"] = ServiceProviderModel.SelectOnlyListNoSelect();
            ht["UserCountryList"] = UserCountryListItems.Fetch(false);
            ht["StatusList"] = LookUpListItems.Fetch("StatusList", false, false);
            ht["ClaimTypeList"] = ServiceProviderModel.FetchSelectTypeLookUpList("ClaimType");
            ht["TPVehicleNoList"] = ServiceProviderModel.FetchTPVehicleList(accidentId);
            return ht;
        }

        [HttpPost]
        public List<ClaimantStatus> GetCompanyNameList(string InsurerType, string PartyTypeId,string Status)
        {
            var list = obj.Proc_GetMNT_Cedant_CompanyName(InsurerType, PartyTypeId, Status).ToList();
            var item = new List<ClaimantStatus>();
            item = (from data in list
                    select new ClaimantStatus()
                    {
                        Id = data.CedantId,
                        Text = data.CedantName
                    }
                        ).ToList();
            return item;
        }

        [HttpPost]
        public HttpResponseMessage CreateServiceProvider(ServiceProviderModel model)
        {
            try
            {

                ModelState["model.Reference"].Errors.Clear();
                ModelState.Remove("model.Reference");
                if (!ModelState.IsValid)
                {
                    var modelStateErrors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                    List<string> errors = new List<string>();
                    foreach (var item in modelStateErrors)
                    {
                        bool chk = false;
                        if (item.Exception != null)
                        {
                            chk = item.Exception.Message.ToLower().Contains("required");
                        }
                        if (item.ErrorMessage != "")
                        {
                            errors.Add(item.ErrorMessage);
                        }
                        else if (chk)
                        {
                            errors.Add(item.Exception.Message.Split(',')[0]);
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
                }
                model.AccidentId = Convert.ToInt32(model.AccidentClaimId);
                model.ClaimantNameId = Convert.ToInt32(model.TPVehNo);
                model.CreatedBy = HttpContext.Current.Session["LoggedInUserName"].ToString();
                ModelState.Clear();
                model.Update();
                var actionInt = 0;
                if (model.ResultMessage.IndexOf("saved") >= 0)
                {
                    actionInt = 1;
                }
                else if (model.ResultMessage.IndexOf("updated") >= 0)
                {
                    actionInt = 2;
                }
                string directLink = "?policyId=" + model.PolicyId + "&AccidentClaimId=" + model.AccidentClaimId + "&ServiceProviderId=" + model.ServiceProviderId + "&ActDone=" + actionInt;
                object routes = new { policyId = model.PolicyId, AccidentClaimId = model.AccidentClaimId, ServiceProviderId = model.ServiceProviderId, ActDone = actionInt };
                string res = RouteEncryptDecrypt.getRouteString(routes);
                res = RouteEncryptDecrypt.Encrypt(res);
                return Request.CreateResponse(HttpStatusCode.OK, directLink);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage FetchServiceProvider(int ServiceProviderId)
        {
            try
            {
                MCASEntities obj = new MCASEntities();
                ServiceProviderModel model = new ServiceProviderModel();
                var list = obj.Proc_GetCLM_ServicePID(Convert.ToString(ServiceProviderId)).ToList().FirstOrDefault();
                model = model.ServiceProvider(Convert.ToString(ServiceProviderId), model);
                model.CreatedBy = list.Createdby;
                model.CreatedOn = Convert.ToDateTime(list.Createddate);
                if (list.Modifieddate != null || list.Modifieddate.ToString() != "")
                {
                    model.ModifiedOn = Convert.ToDateTime(list.Modifieddate);
                    model.ModifiedBy = list.Modifiedby;
                }
                return Request.CreateResponse(HttpStatusCode.OK, model);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        /////////// ----------- BaseConrtoller Functions ------------- //////////////////
        #region BaseController Functions

        #endregion
    }
}
