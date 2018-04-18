using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Web.Objects.MastersHelper;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.Data.Objects.SqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using MCAS.Web.Objects.Resources.Common;

namespace MCAS.Web.Objects.ClaimObjectHelper
{
    public class ClaimReserveModel : BaseModel
    {
        #region properties
        #region PrivateProperties
        private DateTime? _lOGDate = null;
        private DateTime? _modifieddate = null;
        private DateTime? _createddate1 = null;
        private List<ClaimReserveModelCollection> _claimReserveModel;
        private static IList<string> GetPropertyNames(Type sourceType)
        {
            List<string> result = new List<string>();
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(sourceType);
            foreach (PropertyDescriptor item in props)
                if (item.IsBrowsable)
                    result.Add(item.Name);
            return result;
        }
        #endregion
        #region PublicProperties
        public List<ClaimReserveModelCollection> ClaimReserveModelCollection
        {
            get { return _claimReserveModel; }
        }
        public ClaimReserveModel()
        {
            this._claimReserveModel = FetchReservelist(0);
        }
        public ClaimReserveModel(int AccidentId)
        {
            this._claimReserveModel = FetchReservelist(AccidentId);
        }

        #region InitialreserveColumn
        public decimal? Total_C { get; set; }
        public string Noofdays_C { get; set; }
        public string Rateperday_C { get; set; }

        #endregion
        public Int64? SNO { get; set; }
        public Int64? SerialNo { get; set; }
        public int? ClaimID { get; set; }
        public string Noofdays_I { get; set; }
        public string Noofdays_R { get; set; }
        public string Noofdays_O { get; set; }
        public string Rateperday_I { get; set; }
        public string Rateperday_R { get; set; }
        public string Rateperday_O { get; set; }
        public string Createdby1 { get; set; }
        public string Modifiedby1 { get; set; }
        public decimal? Total_I { get; set; }
        public decimal? Total_R { get; set; }
        public decimal? Total_O { get; set; }
        public string ClaimRecordNo { get; set; }
        public string Hdivdis { get; set; }
        public string Himgsrc { get; set; }
        public string Hselect { get; set; }
        public string Hshowgrid { get; set; }
        public string HGridImageSign { get; set; }
        public string HLogText { get; set; }
        public string CORemarks { get; set; }
        public int? HopitalNameId { get; set; }
        public int? AssignToId { get; set; }
        public string ResultMessage { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created Date")]
        public DateTime? Createddate1
        {
            get { return _createddate1; }
            set { _createddate1 = value; }
        }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Modified date")]
        public DateTime? Modifieddate
        {
            get { return _modifieddate; }
            set { _modifieddate = value; }
        }

        public int? AccidentId { get; set; }
        public int? PolicyId { get; set; }
        //public string IsActive { get; set; }
        public List<CommonUtilities.CommonType> RateperdayList { get; set; }
        public List<CommonUtilities.CommonType> SelectListClamiantName { get; set; }
        public List<CommonUtilities.CommonType> HopitalNameIdList { get; set; }
        public List<CommonUtilities.CommonType> AssignToIdList { get; set; }
        #endregion
        #region Override Properties
        public override string screenId
        {
            get
            {
                return "137";
            }

        }
        public override string listscreenId
        {

            get
            {
                return "137";
            }

        }
        #endregion
        #endregion
        #region PublicMethods
        //public ClaimReserveModel Save()
        //{
        //    MCASEntities obj = new MCASEntities();
        //    CLM_ReserveSummary claimres = new CLM_ReserveSummary();
        //    if (obj.Connection.State == System.Data.ConnectionState.Closed)
        //        obj.Connection.Open();
        //    using (System.Data.Common.DbTransaction transaction = obj.Connection.BeginTransaction())
        //    {
        //        try
        //        {
        //            var exits = (from l in obj.CLM_ReserveSummary where AccidentClaimId == this.AccidentClaimId && l.ClaimID == this.ClaimID select l).FirstOrDefault();
        //            if (exits == null)
        //            {
        //                claimres.Createddate = DateTime.Now;
        //                claimres.Createdby = this.CreatedBy;
        //                claimres.MovementType = "I";
        //            }
        //            else
        //            {
        //                claimres.Createddate = exits.Createddate;
        //                claimres.Createdby = exits.Createdby;
        //                claimres.Modifieddate = DateTime.Now;
        //                claimres.Modifiedby = this.CreatedBy;
        //                claimres.MovementType = "M";
        //                this.ModifiedOn = DateTime.Now;
        //                this.ModifiedBy = this.CreatedBy;
        //                this.CreatedOn = Convert.ToDateTime(exits.Createddate);
        //                this.CreatedBy = exits.Createdby;
        //            }
        //            claimres.ReserveId = 0;
        //            claimres.AccidentClaimId = this.AccidentClaimId;
        //            claimres.ClaimID = Convert.ToInt32(this.ClaimID);
        //            claimres.ClaimType = Convert.ToInt32(this.ClaimType);
        //            claimres.InitialReserve = (from detail in this.ReserveDetails where detail.CompCode == "TOTAL" select detail.InitialReserve).FirstOrDefault();
        //            claimres.MovementReserve = (from detail in this.ReserveDetails where detail.CompCode == "TOTAL" select detail.MovementReserve).FirstOrDefault();
        //            claimres.PreReserve = (from detail in this.ReserveDetails where detail.CompCode == "TOTAL" select detail.PreviousReserve).FirstOrDefault();
        //            claimres.CurrentReserve = (from detail in this.ReserveDetails where detail.CompCode == "TOTAL" select detail.CurrentReserve).FirstOrDefault();

        //            claimres.IsActive = "Y";
        //            obj.CLM_ReserveSummary.AddObject(claimres);
        //            obj.SaveChanges();
        //            this.ReserveId = claimres.ReserveId;
        //            #region InsertIntoReserveDetails
        //            for (var i = 0; i < this.ReserveDetails.Count; i++)
        //            {
        //                if (this.ReserveDetails[i].CompCode != "SubTotal" && this.ReserveDetails[i].CompCode != "Labl")
        //                {
        //                    CLM_ReserveDetails claimresdetails = new CLM_ReserveDetails();
        //                    if (this.ReserveDetails[i].CompCode == "LOU")
        //                    {
        //                        claimresdetails.InitialNoofdays = this.Noofdays_I == null ? "0" : this.Noofdays_I;
        //                        claimresdetails.CurrentNoofdays = this.Noofdays_R == null ? "0" : this.Noofdays_R;
        //                        claimresdetails.MovementNoofdays = this.Noofdays_O == null ? "0" : this.Noofdays_O;
        //                        claimresdetails.InitialRateperday = this.Rateperday_I;
        //                        claimresdetails.CurrentRateperday = this.Rateperday_R;
        //                        claimresdetails.MovementlRateperday = this.Rateperday_O;
        //                    }
        //                    else
        //                    {

        //                        claimresdetails.InitialNoofdays = "0";
        //                        claimresdetails.CurrentNoofdays = "0";
        //                        claimresdetails.MovementNoofdays = "0";
        //                        claimresdetails.InitialRateperday = "0";
        //                        claimresdetails.CurrentRateperday = "0";
        //                        claimresdetails.MovementlRateperday = "0";
        //                    }
        //                    claimresdetails.ReserveDetailID = 0;
        //                    claimresdetails.InitialReserve = this.ReserveDetails[i].InitialReserve == null ? 0.00m : this.ReserveDetails[i].InitialReserve;
        //                    claimresdetails.MovementReserve = this.ReserveDetails[i].MovementReserve == null ? 0.00m : this.ReserveDetails[i].MovementReserve;
        //                    claimresdetails.PreReserve = this.ReserveDetails[i].CompCode != "TOTAL" ? this.ReserveDetails[i].PreviousReserve == null ? 0.00m : this.ReserveDetails[i].PreviousReserve : (from l in this.ReserveDetails where l.CompCode != "TOTAL" && l.CompCode != "SubTotal" && this.ReserveDetails[i].CompCode != "Labl" select l.PreviousReserve).Sum();

        //                    claimresdetails.CurrentReserve = this.ReserveDetails[i].CurrentReserve == null ? 0.00m : this.ReserveDetails[i].CurrentReserve;
        //                    bool Ifallequal = new[] { claimresdetails.InitialReserve, claimresdetails.MovementReserve, claimresdetails.PreReserve, claimresdetails.CurrentReserve }.Distinct().Count() == 1;
        //                    claimresdetails.MovementType = Ifallequal && (claimresdetails.InitialReserve != 0.00m) ? "I" : (!Ifallequal && claimresdetails.PreReserve != claimresdetails.CurrentReserve) ? "M" : "";
        //                    claimresdetails.CmpCode = this.ReserveDetails[i].CompCode;
        //                    claimresdetails.ReserveId = Convert.ToInt32(this.ReserveId);
        //                    claimresdetails.Createddate = DateTime.Now;
        //                    claimresdetails.Createdby = this.CreatedBy;
        //                    claimresdetails.IsActive = "Y";
        //                    claimresdetails.AccidentClaimId = this.AccidentClaimId;
        //                    claimresdetails.ClaimID = this.ClaimID;
        //                    obj.CLM_ReserveDetails.AddObject(claimresdetails);
        //                    obj.SaveChanges();
        //                }
        //            }
        //            #endregion
        //            transaction.Commit();
        //            this._claimReserveModel = FetchReservelist(Convert.ToInt32(this.AccidentClaimId));
        //            this.ReserveDetails.ForEach(s => s.CompDesc = (from l in obj.MNT_Lookups where l.Category == "TranComponent" && l.Lookupvalue.Trim() == s.CompCode.Trim() select l.Description).FirstOrDefault());
        //            obj.Dispose();
        //            return this;
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            obj.Dispose();
        //            throw (ex);
        //        }
        //    }
        //}

        public ClaimReserveModel Save()
        {
            MCASEntities obj = new MCASEntities();
            if (obj.Connection.State == System.Data.ConnectionState.Closed)
                obj.Connection.Open();
            CLM_ReserveSummary claimres = new CLM_ReserveSummary();
            CommonUtilities objCommonUtilities = new CommonUtilities();
            string ActionSummary = MCASEntities.AuditActions.I.ToString();
            string ActionDetails = MCASEntities.AuditActions.I.ToString();
            int returnValue = 0;

            try
            {
                var exits = (from l in obj.CLM_ReserveSummary where AccidentClaimId == this.AccidentClaimId && l.ClaimID == this.ClaimID select l).FirstOrDefault();
                if (exits == null)
                {
                    
                    claimres.Createddate = DateTime.Now;
                    claimres.Createdby = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
                    claimres.Createdby = this.CreatedBy;
                    claimres.MovementType = "I";
                    this.ResultMessage = "Record saved successfully.";
                   
                }
                else
                {
                    claimres.Createddate = exits.Createddate;
                    claimres.Createdby = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
                    //claimres.Createdby = exits.Createdby;
                    claimres.Modifieddate = DateTime.Now;
                   // claimres.Modifiedby = this.CreatedBy;
                    claimres.Modifiedby = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
                    claimres.MovementType = "M";
                    this.ModifiedOn = DateTime.Now;
                    this.ModifiedBy = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
                    this.CreatedOn = Convert.ToDateTime(exits.Createddate);
                    this.CreatedBy = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUserName"]);
                    this.ResultMessage = "Record updated successfully.";
                }
                //claimres.ReserveId = 0;
                claimres.AccidentClaimId = this.AccidentClaimId;
                claimres.ClaimID = Convert.ToInt32(this.ClaimID);
                claimres.ClaimType = Convert.ToInt32(this.ClaimType);
                claimres.InitialReserve = (from detail in this.ReserveDetails where detail.CompCode == "TOTAL" select detail.InitialReserve).FirstOrDefault();
                claimres.MovementReserve = (from detail in this.ReserveDetails where detail.CompCode == "TOTAL" select detail.MovementReserve).FirstOrDefault();
                claimres.PreReserve = (from detail in this.ReserveDetails where detail.CompCode == "TOTAL" select detail.PreviousReserve).FirstOrDefault();
                claimres.CurrentReserve = (from detail in this.ReserveDetails where detail.CompCode == "TOTAL" select detail.CurrentReserve).FirstOrDefault();
                claimres.IsActive = "Y";

                string[] ignoreListSummary = new string[] { "ReserveId", "PaymentId" };
                DataTable tableReserveSummary = objCommonUtilities.CreateDataTable(claimres, ignoreListSummary);

                string XMLSummary = "<ChangeXml>" + objCommonUtilities.GenerateXMLForChangedColumns(null, claimres, null) + "</ChangeXml>";

                #region InsertIntoReserveDetails

                string XMLDetails = "";
                List<CLM_ReserveDetails> lstReserveDetails = new List<CLM_ReserveDetails>();
                for (var i = 0; i < this.ReserveDetails.Count; i++)
                {
                    if (this.ReserveDetails[i].CompCode != "SubTotal" && this.ReserveDetails[i].CompCode != "Labl")
                    {
                        CLM_ReserveDetails claimresdetails = new CLM_ReserveDetails();
                        if (this.ReserveDetails[i].CompCode == "LOU")
                        {
                            claimresdetails.InitialNoofdays = this.Noofdays_I == null ? "0" : this.Noofdays_I;
                            claimresdetails.CurrentNoofdays = this.Noofdays_R == null ? "0" : this.Noofdays_R;
                            claimresdetails.MovementNoofdays = this.Noofdays_O == null ? "0" : this.Noofdays_O;
                            claimresdetails.InitialRateperday = this.Rateperday_I;
                            claimresdetails.CurrentRateperday = this.Rateperday_R;
                            claimresdetails.MovementlRateperday = this.Rateperday_O;
                        }
                        else
                        {

                            claimresdetails.InitialNoofdays = "0";
                            claimresdetails.CurrentNoofdays = "0";
                            claimresdetails.MovementNoofdays = "0";
                            claimresdetails.InitialRateperday = "0";
                            claimresdetails.CurrentRateperday = "0";
                            claimresdetails.MovementlRateperday = "0";
                        }
                        //claimresdetails.ReserveDetailID = 0;
                        claimresdetails.InitialReserve = this.ReserveDetails[i].InitialReserve == null ? 0.00m : this.ReserveDetails[i].InitialReserve;
                        claimresdetails.MovementReserve = this.ReserveDetails[i].MovementReserve == null ? 0.00m : this.ReserveDetails[i].MovementReserve;
                        claimresdetails.PreReserve = this.ReserveDetails[i].CompCode != "TOTAL" ? this.ReserveDetails[i].PreviousReserve == null ? 0.00m : this.ReserveDetails[i].PreviousReserve : (from l in this.ReserveDetails where l.CompCode != "TOTAL" && l.CompCode != "SubTotal" && this.ReserveDetails[i].CompCode != "Labl" select l.PreviousReserve).Sum();

                        claimresdetails.CurrentReserve = this.ReserveDetails[i].CurrentReserve == null ? 0.00m : this.ReserveDetails[i].CurrentReserve;
                        bool Ifallequal = new[] { claimresdetails.InitialReserve, claimresdetails.MovementReserve, claimresdetails.PreReserve, claimresdetails.CurrentReserve }.Distinct().Count() == 1;
                        claimresdetails.MovementType = Ifallequal && (claimresdetails.InitialReserve != 0.00m) ? "I" : (!Ifallequal && claimresdetails.PreReserve != claimresdetails.CurrentReserve) ? "M" : "";
                        claimresdetails.CmpCode = this.ReserveDetails[i].CompCode;
                        claimresdetails.ReserveId = Convert.ToInt32(this.ReserveId);
                        claimresdetails.Createddate = DateTime.Now;
                        claimresdetails.Createdby = this.CreatedBy;
                        claimresdetails.IsActive = "Y";
                        claimresdetails.AccidentClaimId = this.AccidentClaimId;
                        claimresdetails.ClaimID = this.ClaimID;

                        lstReserveDetails.Add(claimresdetails);

                        //If there is a change in value of these property of checkList, than only row will be created...
                        string[] checkListDtails = { "InitialReserve", "PreReserve", "MovementReserve", "CurrentReserve", "InitialNoofdays", "MovementNoofdays", "CurrentNoofdays", "InitialRateperday", "MovementlRateperday", "CurrentRateperday" };
                        string tempXML = objCommonUtilities.GenerateXMLForChangedColumns(null, claimresdetails, checkListDtails);
                        if (!string.IsNullOrEmpty(tempXML))
                        {
                            XMLDetails += "<Row Id=\"" + claimresdetails.CmpCode.Trim() + "\">" + tempXML;
                            XMLDetails += "</Row>";
                        }
                    }
                }
                XMLDetails = "<ChangeXml>" + XMLDetails + "</ChangeXml>";

                #endregion

                string[] ignoreListDetails = new string[] { "ReserveDetailID", "PaymentId", "Modifiedby", "Modifieddate" };
                DataTable tableReserveDetails = objCommonUtilities.CreateDataTable(lstReserveDetails, ignoreListDetails);

                var parameterSummary = new SqlParameter("@ReserveSummary", SqlDbType.Structured);
                parameterSummary.Value = tableReserveSummary;
                parameterSummary.TypeName = "dbo.TVP_ReserveSummary";

                var parameterDetails = new SqlParameter("@ReserveDetailsList", SqlDbType.Structured);
                parameterDetails.Value = tableReserveDetails;
                parameterDetails.TypeName = "dbo.TVP_ReserveDetails";

                object ret;
                obj.AddParameter(parameterSummary);
                obj.AddParameter(parameterDetails);
                obj.AddParameter("@XMLSummary", XMLSummary);
                obj.AddParameter("@XMLDetails", XMLDetails);
                obj.AddParameter("@ActionSummary", ActionSummary);
                obj.AddParameter("@ActionDetails", ActionDetails);
                obj.ExecuteNonQuery("Proc_SaveReserve", CommandType.StoredProcedure, out ret);
                returnValue = Convert.ToInt32(ret);


                this._claimReserveModel = FetchReservelist(Convert.ToInt32(this.AccidentClaimId));
                this.ReserveDetails.ForEach(s => s.CompDesc = (from l in obj.MNT_Lookups where l.Category == "TranComponent" && l.Lookupvalue.Trim() == s.CompCode.Trim() select l.Description).FirstOrDefault());

                if (returnValue > 0)
                    this.ReserveId = returnValue;
                else
                {
                    throw new Exception("Exception generated in procedure Proc_SaveReserve.");
                }

                //obj.InsertTransactionAuditLog("CLM_ReserveSummary", "ReserveId", this.CreatedBy, this.ClaimID, this.AccidentClaimId, ActionSummary, XMLSummary);
                //obj.InsertTransactionAuditLog("CLM_ReserveDetails", "ReserveDetailID", this.CreatedBy, this.ClaimID, this.AccidentClaimId, ActionDetails, XMLDetails);

                return this;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                obj.Connection.Close();
                obj.Dispose();
            }

        }

        public ClaimReserveModel FetchReserve(string ReserveId, ClaimReserveModel model, string InAdjustShowOutStandingAsInitial)
        {
            MCASEntities db = new MCASEntities();
            var TransactonHistoryList = (from l in db.MNT_Lookups where l.Category == "TranComponent" && l.IsActive == "Y" select l.Description).ToList();
            try
            {
                model.RateperdayList = ClaimReserveModel.FetchRateperdayList();
                model.SelectListClamiantName = SelectOnlyList();
                model.HopitalNameIdList = FetchHopitalNameIdList();
                model.AssignToIdList = FetchAssignToIdListList();
                int rid = Convert.ToInt32(string.IsNullOrEmpty(ReserveId)?"0":ReserveId);
                var ReserveSummary = (from l in db.CLM_ReserveSummary where l.ReserveId == rid select l).FirstOrDefault();
                if (ReserveSummary != null && rid != 0)
                {
                    model.InitialReserve = ReserveSummary.InitialReserve;
                    model.PreviousReserve = ReserveSummary.PreReserve; ;
                    model.CurrentReserve = ReserveSummary.CurrentReserve;
                    model.MovementReserve = ReserveSummary.MovementReserve;
                    model.CreatedBy = ReserveSummary.Createdby;
                    model.CreatedOn = Convert.ToDateTime(ReserveSummary.Createddate);
                    if (ReserveSummary.Modifieddate != null)
                    {
                        model.ModifiedOn = ReserveSummary.Modifieddate;
                        model.ModifiedBy = ReserveSummary.Modifiedby;
                    }
                }
                var ReserveDetailList = (from details in db.CLM_ReserveDetails join lk in db.MNT_Lookups on details.CmpCode equals lk.Lookupvalue where lk.Category == "TranComponent" orderby lk.DisplayOrder ascending where details.ReserveId == rid select details);
                if (ReserveDetailList.Any())
                {
                    model.ReserveDetails = (from detail in ReserveDetailList
                                            select new ClaimReserveDetails()
                                            {
                                                ReserveDetailId = detail.ReserveDetailID,
                                                ReserveId = detail.ReserveId,
                                                AccidentClaimId = detail.AccidentClaimId,
                                                CompCode = detail.CmpCode.Trim(),
                                                CompDesc = (from l in db.MNT_Lookups where l.Category == "TranComponent" && l.Lookupvalue == detail.CmpCode select l.Description).FirstOrDefault(),
                                                InitialReserve = detail.InitialReserve,
                                                PreviousReserve = detail.PreReserve,
                                                MovementReserve = detail.MovementReserve,
                                                CurrentReserve = detail.CurrentReserve,
                                                PaymentId = detail.PaymentId
                                            }
                            ).ToList();
                    var remainlist = (from lk in db.MNT_Lookups where lk.Lookupvalue != "SubTotal" && lk.Lookupvalue != "Labl" && lk.IsActive == "Y" && lk.Category == "TranComponent" select lk).Where(p => !(from details in db.CLM_ReserveDetails where details.ReserveId == rid select details).Any(p2 => p2.CmpCode == p.Lookupvalue)).OrderBy(x => x.DisplayOrder).ToList();

                     if (remainlist.Any())
                     {
                         var list = model.ReserveDetails;
                         foreach(var remain in remainlist)
                         {
                             var Position = (from lk in db.MNT_Lookups where lk.Lookupvalue != "SubTotal" && lk.Lookupvalue != "Labl" && lk.IsActive == "Y" && lk.Category == "TranComponent" orderby lk.DisplayOrder select lk).ToList().FindIndex(c => c.DisplayOrder == remain.DisplayOrder);
                             list.Insert(Convert.ToInt32(Position), new ClaimReserveDetails()
                             {
                                 ReserveDetailId = null,
                                 ReserveId = model.ReserveDetails.FirstOrDefault().ReserveId,
                                 AccidentClaimId = model.ReserveDetails.FirstOrDefault().AccidentClaimId,
                                 CompCode = remain.Lookupvalue,
                                 CompDesc = remain.Description,
                                 InitialReserve = 0.00m,
                                 PreviousReserve = 0.00m,
                                 MovementReserve = 0.00m,
                                 CurrentReserve = 0.00m,
                                 PaymentId = model.ReserveDetails.FirstOrDefault().PaymentId
                             });
                         }
                         model._reserveDetails.Clear();
                         model.GetType().GetProperty("ReserveDetails").SetValue(model, list, null);
                    }
                    model.Noofdays_I = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "LOU" select detail.InitialNoofdays).FirstOrDefault();
                    model.Noofdays_R = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "LOU" select detail.MovementNoofdays).FirstOrDefault();
                    model.Noofdays_O = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "LOU" select detail.CurrentNoofdays).FirstOrDefault();
                    model.Rateperday_I = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "LOU" select detail.InitialRateperday).FirstOrDefault();
                    model.Rateperday_R = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "LOU" select detail.MovementlRateperday).FirstOrDefault();
                    model.Rateperday_O = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "LOU" select detail.CurrentRateperday).FirstOrDefault();
                }
                else
                {
                    var CompCodeList = (from l in db.MNT_Lookups where l.Category == "TranComponent" && l.IsActive == "Y" && l.Lookupvalue != "SubTotal" && l.Lookupvalue != "Labl" orderby l.DisplayOrder ascending select l.Lookupvalue).ToList();
                    var TranList = new List<ClaimReserveDetails>();
                    for (var i = 0; i < CompCodeList.Count; i++)
                    {
                        var desc = Convert.ToString(CompCodeList[i]);
                        TranList.Add(new ClaimReserveDetails()
                        {
                            ReserveDetailId = 0,
                            ReserveId = 0,
                            AccidentClaimId = model.AccidentClaimId,
                            CompCode = CompCodeList[i].Trim(),
                            CompDesc = (from l in db.MNT_Lookups where l.Category == "TranComponent" && l.Lookupvalue == desc select l.Description).FirstOrDefault(),
                            InitialReserve = 0.00m,
                            PreviousReserve = 0.00m,
                            MovementReserve = 0.00m,
                            CurrentReserve = 0.00m,
                            PaymentId = 0
                        });
                    }
                    model.ReserveDetails = TranList.ToList();
                }

                return model;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                db.Dispose();
            }
        }


        public static List<ClaimReserveModelCollection> FetchReservelist(int AccidentClaimId)
        {
            MCASEntities db = new MCASEntities();
            var items = new List<ClaimReserveModelCollection>();
            try
            {
                items = (from data in db.Proc_GetCLM_ReserveList(Convert.ToString(AccidentClaimId)).ToList()
                         select new ClaimReserveModelCollection()
                         {
                             RecordNumber = data.ClaimRecordNo,
                             ClaimID = data.ClaimID,
                             ClaimantName = data.ClaimantName,
                             ClaimType = data.ClaimType,
                             AccidentClaimId = data.AccidentClaimId,
                             PolicyId = data.PolicyId,
                             ReserveId = data.ReserveId,
                             Total_O = data.CurrentReserve,
                             ClaimTypeCode = data.ClaimTypeCode,
                             ClaimTypeDesc = data.ClaimType.ToString() == "1" ? Common._1 : data.ClaimType.ToString() == "2" ? Common._2 : data.ClaimType.ToString() == "3" ? Common._3 : "",
                             HClaimantStatus = data.ClaimantStatus
                         }
                        ).ToList();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                db.Dispose();
            }
            return items;
        }
        public static List<CommonUtilities.CommonType> FetchHopitalNameIdList()
        {
            MCASEntities _db = new MCASEntities();
            var items = new List<CommonUtilities.CommonType>();
            var list = _db.Proc_GetMnt_HopitalList().ToList();
            items = (from n in list
                     select new CommonUtilities.CommonType()
                     {
                         intID = n.Id,
                         Text = n.HospitalName
                     }).ToList();
            items.Insert(0, new CommonUtilities.CommonType() { intID = 0, Text = "[Select...]" });
            _db.Dispose();
            return items;
        }
        public static List<CommonUtilities.CommonType> FetchAssignToIdListList()
        {
            MCASEntities _db = new MCASEntities();
            var items = new List<CommonUtilities.CommonType>();
            var list = _db.Proc_GetSupervisorList().ToList();
            items = (from n in list
                     select new CommonUtilities.CommonType()
                     {
                         intID = n.SNo,
                         Text = n.UserId
                     }).ToList();
            items.Insert(0, new CommonUtilities.CommonType() { intID = 0, Text = "[Select...]" });
            _db.Dispose();
            return items;
        }
        public static List<ClaimReserveModel> Fetchall(string resid, string AccidentClaimId)
        {
            MCASEntities _db = new MCASEntities();
            var items = new List<ClaimReserveModel>();
            var list = _db.Proc_GetCLM_Reserve_ClaimTypeList(resid, AccidentClaimId).ToList();
            items = (from n in list
                     select new ClaimReserveModel()
                     {
                         SerialNo = n.SerialNo,
                         MovementType = n.MovementType,
                         SNO = n.SNO,
                         ClaimantName = n.ClaimantName,
                         Createdby1 = n.Createdby,
                         Modifiedby1 = n.Modifiedby,
                         Createddate1 = n.Createddate,
                         Modifieddate = n.Modifieddate,
                         Total_I = n.Total_I,
                         Total_R = n.Total_R,
                         Total_O = n.Total_O,
                         ClaimRecordNo = n.ClaimRecordNo,
                         ReserveId = n.ReserveId,
                         ClaimType = n.ClaimType
                     }).ToList();
            _db.Dispose();
            return items;
        }
        public static List<CommonUtilities.CommonType> FetchRateperdayList()
        {
            MCASEntities _db = new MCASEntities();
            var item = new List<CommonUtilities.CommonType>();
            var list = (from l in _db.MNT_LOU_MASTER
                        select new LOUModel()
                        {
                            Id = l.Id,
                            LouRate = l.LouRate
                        }
                        ).ToList();
            foreach (var data in list)
            {
                item.Add(new CommonUtilities.CommonType()
                {
                    Id = Convert.ToString(data.Id),
                    Text = Convert.ToString(data.LouRate)
                });
            }
            item.Insert(0, new CommonUtilities.CommonType() { Id = "", Text = "[Select...]" });
            _db.Dispose();
            return item;
        }

        public static List<CommonUtilities.CommonType> SelectOnlyList()
        {
            var item = new List<CommonUtilities.CommonType>();
            item.Add(new CommonUtilities.CommonType() { intID = 0, Text = "[Select...]" });
            return item;
        }
        public static List<ClaimReserveModel> GetClaimantNameReserveList(string Acc)
        {
            MCASEntities _db = new MCASEntities();
            var list = _db.Proc_GetClaminNameReserve(Acc).ToList();
            var item = new List<ClaimReserveModel>();
            item = (from data in list
                    select new ClaimReserveModel()
                    {
                        ReserveId = data.ReserveId,
                        ClaimantName = data.ClaimantName
                    }
                        ).ToList();
            return item;
        }
        public static string GridButtonTextEditOrAssign(string ReserveId)
        {
            MCASEntities db = new MCASEntities();
            int Rid = Convert.ToInt32(ReserveId);
            var GridButtonText = (from Reserve1 in db.CLM_Reserve join Reserve2 in db.CLM_Reserve on Reserve1.ClaimID equals Reserve2.ClaimID where Reserve1.ReserveId == Rid orderby Reserve1.ReserveId ascending select Reserve2.Modifieddate).FirstOrDefault();
            db.Dispose();
            return GridButtonText == null ? "F" : "T";
        }
        public static string GridButtonTextEditOrAssignAccordingToPaymentTab(string ReserveId)
        {
            MCASEntities db = new MCASEntities();
            int Rid = Convert.ToInt32(ReserveId);
            var GridButtonText = (from Reserve1 in db.CLM_Reserve where Reserve1.ReserveId == Rid && Reserve1.MovementType == "P" && (Reserve1.Finalize == "N" || Reserve1.Finalize == null) select Reserve1).FirstOrDefault();
            db.Dispose();
            return GridButtonText == null ? "F" : "T";
        }

        public static string GridButtonTextEditOrAssignAccordingToReserveFinilize(string ReserveId)
        {
            MCASEntities db = new MCASEntities();
            int Rid = Convert.ToInt32(ReserveId);
            var GridButtonText = (from Reserve1 in db.CLM_Reserve where Reserve1.ReserveId == Rid && (Reserve1.Finalize == "N" || Reserve1.Finalize == null) select Reserve1).FirstOrDefault();
            db.Dispose();
            return GridButtonText == null ? "F" : "T";
        }
        public static string CheckInitialAmountFinilize(string ReserveId)
        {
            MCASEntities db = new MCASEntities();
            int Rid = Convert.ToInt32(ReserveId);
            var CheckInitialAmountFinilize = (from Reserve1 in db.CLM_Reserve where Reserve1.ReserveId == Rid && (Reserve1.Finalize == "N" || Reserve1.Finalize == null) && Reserve1.MovementType == "I" select Reserve1).FirstOrDefault();
            db.Dispose();
            return CheckInitialAmountFinilize == null ? "F" : "T";
        }
        public static string UpdateFinilize(string ReserveId)
        {
            MCASEntities _db = new MCASEntities();
            var model = new ClaimReserveModel();
            int Rid = Convert.ToInt32(ReserveId);
            CLM_Reserve clmres;
            var val = _db.CLM_Reserve.Where(x => x.ReserveId == Rid).ToList();
            if (val.Any())
            {
                clmres = _db.CLM_Reserve.Where(x => x.ReserveId == Rid).FirstOrDefault();
                clmres.Finalize = "Y";
                clmres.Modifieddate = DateTime.Now;
                clmres.Modifiedby = Convert.ToString(HttpContext.Current.Session["LoggedInUserId"]);
                _db.SaveChanges();
                _db.Dispose();
                return "T";
            }
            _db.Dispose();
            return "F";
        }
        public static string GetClaimRecordNoForFinilize(string ReserveId)
        {
            MCASEntities _db = new MCASEntities();
            int resid = Convert.ToInt32(ReserveId);
            var result = (from Claims in _db.CLM_Claims join Reserve in _db.CLM_Reserve on Claims.ClaimID equals Reserve.ClaimID where Reserve.ReserveId == resid select Claims.ClaimRecordNo).FirstOrDefault();
            _db.Dispose();
            return result == null ? "" : result;
        }
        public static string GetClaimTypeNo(string ClaimType)
        {
            MCASEntities _db = new MCASEntities();
            var result = (from l in _db.MNT_Lookups where l.Category == "ClaimType" && l.Description.Contains(ClaimType) && l.IsActive == "Y" select l.Lookupvalue).FirstOrDefault();
            _db.Dispose();
            return result == null ? "" : result;
        }
        #endregion

        #region properties for new model

        public Int64? ReserveId { get; set; }
        public int AccidentClaimId { get; set; }
        public int? ClaimType { get; set; }
        public string MovementType { get; set; }
        public int? PaymentId { get; set; }
        public decimal? InitialReserve { get; set; }
        public decimal? PreviousReserve { get; set; }
        public decimal? MovementReserve { get; set; }
        public decimal? CurrentReserve { get; set; }

        public List<ClaimReserveDetails> _reserveDetails = new List<ClaimReserveDetails>();

        public List<ClaimReserveDetails> ReserveDetails
        {
            get { return _reserveDetails.ToList<ClaimReserveDetails>(); }
            set
            {
                _reserveDetails.AddRange(value);
            }
        }

        // to be removed
        public string ClaimantName { get; set; }
        public decimal? LossofUseUn_O { get; set; }
        public int? ClaimantNameId { get; set; }

        #endregion



        public static List<ClaimReserveDetails> Fetchdetailsfromreservedetails(string ClaimType, string ReserveId)
        {
            MCASEntities _db = new MCASEntities();
            var Resid = Convert.ToInt32(ReserveId);
            var result = (from l in _db.CLM_ReserveDetails
                          where l.ReserveId == Resid
                          select new ClaimReserveDetails()
                              {
                                  CompCode = l.CmpCode,
                                  InitialReserve = l.InitialReserve,
                                  PreviousReserve = l.PreReserve,
                                  MovementReserve = l.MovementReserve,
                                  CurrentReserve = l.CurrentReserve,
                                  Createdby1 = l.Createdby,
                                  Createddate = l.Createddate,
                                  MovementType = l.MovementType
                              }).ToList();

            var res = new List<ClaimReserveDetails>();
            foreach (var data in result)
            {
                if (data.InitialReserve != 0.00m && data.MovementReserve != 0.00m)
                {
                    if (ClaimType == "1" && data.CompCode != "LOUUN")
                    {
                        res.Add(new ClaimReserveDetails()
                        {
                            CompCode = (from l in _db.MNT_Lookups where l.Category == "Trancomponent" && l.Lookupvalue == data.CompCode select l.Lookupdesc).FirstOrDefault(),
                            MovementReserve = data.MovementReserve,
                            CurrentReserve = data.CurrentReserve,
                            Createdby1 = data.Createdby1,
                            Crdate = data.Createddate.Value.ToString(),
                            MovementType = data.MovementType
                        });
                    }
                    else if ((ClaimType == "2" || ClaimType == "3") && data.CompCode != "LOU")
                    {
                        res.Add(new ClaimReserveDetails()
                        {
                            CompCode = (from l in _db.MNT_Lookups where l.Category == "Trancomponent" && l.Lookupvalue == data.CompCode select l.Lookupdesc).FirstOrDefault(),
                            MovementReserve = data.MovementReserve,
                            CurrentReserve = data.CurrentReserve,
                            Createdby1 = data.Createdby1,
                            Crdate = data.Createddate.Value.ToString(),
                            MovementType = data.MovementType == null ? "" : data.MovementType
                        });
                    }
                }
            }
            return res;
        }
    }
    public class ClaimReserveModelCollection
    {
        #region "Object Properties"
        public Int64? OrgRecordNumber { get; set; }
        public string RecordNumber { get; set; }
        public int? ClaimID { get; set; }
        public string ClaimantName { get; set; }
        public int? ClaimType { get; set; }
        public string ClaimTypeDesc { get; set; }
        public string ClaimTypeCode { get; set; }
        public Decimal? Total_O { get; set; }
        public int? ReserveId { get; set; }
        public int? AccidentClaimId { get; set; }
        public int? PolicyId { get; set; }
        public string HClaimantStatus { get; set; }
        public List<string> HeaderListCollection { get; set; }
        #endregion
    }

    public class ClaimReserveDetails : BaseModel
    {
        #region "Object Properties"
        public Int64? ReserveDetailId { get; set; }
        public int ReserveId { get; set; }
        public int AccidentClaimId { get; set; }
        public int? ClaimID { get; set; }
        public int? PaymentId { get; set; }
        public string CompCode { get; set; }
        public string CompDesc { get; set; }
        public string Createdby1 { get; set; }
        public string MovementType { get; set; }
        public string Crdate { get; set; }
        public decimal? InitialReserve { get; set; }
        public decimal? PreviousReserve { get; set; }
        public decimal? MovementReserve { get; set; }
        public decimal? CurrentReserve { get; set; }
        public DateTime? Createddate { get; set; }
        #endregion
    }
}
