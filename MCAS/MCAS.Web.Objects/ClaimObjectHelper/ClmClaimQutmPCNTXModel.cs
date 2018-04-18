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
    public class ClmClaimQutmPCNTXModel : BaseModel
    {
        #region PrivateProperties
        private DateTime? _lOGDate = null;
        private DateTime? _modifieddate = null;
        private DateTime? _createddate1 = null;
        private List<ClmClaimQutmPCNTXModelCollection> _claimReserveModel;
        #endregion
        #region PublicProperties
        public List<ClmClaimQutmPCNTXModelCollection> ClaimReserveModelCollection
        {
            get { return _claimReserveModel; }
        }
        public ClmClaimQutmPCNTXModel()
        {
            this._claimReserveModel = FetchReservelist(0);
        }
        public ClmClaimQutmPCNTXModel(int AccidentId)
        {
            this._claimReserveModel = FetchReservelist(AccidentId);
        }

        public string ClaimRef { get; set; }
        public string TPVehicleNo { get; set; }
        public string TPInsurer { get; set; }

        #region InitialreserveColumn
        public string Noofdays_C { get; set; }
        public string Rateperday_C { get; set; }
        #endregion
        public Int64? SNO { get; set; }
        public Int64? SerialNo { get; set; }
        public int? ClaimID { get; set; }
        //For CELOR
        public string Noofdays_I { get; set; }
        public string Noofdays_R { get; set; }
        public string Noofdays_O { get; set; }
        public string Rateperday_I { get; set; }
        public string Rateperday_R { get; set; }
        public string Rateperday_O { get; set; }
        //For CELOI
        public string LOINoofdays_I { get; set; }
        public string LOINoofdays_R { get; set; }
        public string LOINoofdays_O { get; set; }
        public string LOIRateperday_I { get; set; }
        public string LOIRateperday_R { get; set; }
        public string LOIRateperday_O { get; set; }
        //For CELOU
        public string LOUNoofdays_I { get; set; }
        public string LOUNoofdays_R { get; set; }
        public string LOUNoofdays_O { get; set; }
        public string LOURateperday_I { get; set; }
        public string LOURateperday_R { get; set; }
        public string LOURateperday_O { get; set; }
        //For CECarRental
        public string CRNoofdays_I { get; set; }
        public string CRNoofdays_R { get; set; }
        public string CRNoofdays_O { get; set; }
        public string CRRateperday_I { get; set; }
        public string CRRateperday_R { get; set; }
        public string CRRateperday_O { get; set; }

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
        public string Horgtype { get; set; }

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

        public List<ClaimantStatus> TPVehNoList { get; set; }
        public List<ClaimantStatus> TPInsurerList { get; set; }
        public List<CommonUtilities.CommonType> RateperdayList { get; set; }
        public string ResultMessage { get; set; }
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

        #region PublicMethods
        public ClmClaimQutmPCNTXModel Save()
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
                    claimres.Createdby = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                    claimres.MovementType = "I";
                    this.ResultMessage = "Record saved successfully.";
                }
                else
                {
                    claimres.Createddate = exits.Createddate;
                    claimres.Createdby = exits.Createdby;
                    claimres.Modifieddate = DateTime.Now;
                    claimres.Modifiedby = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                    claimres.MovementType = "M";
                    this.ModifiedOn = DateTime.Now;
                    this.ModifiedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                    this.CreatedOn = Convert.ToDateTime(exits.Createddate);
                    this.CreatedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
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
                        if (this.ReserveDetails[i].CompCode == "CELOR" || this.ReserveDetails[i].CompCode == "CELOI" || this.ReserveDetails[i].CompCode == "CELOU" || this.ReserveDetails[i].CompCode == "CR")
                        {
                            if (this.ReserveDetails[i].CompCode == "CELOR")
                            {
                                claimresdetails.InitialNoofdays = this.Noofdays_I == null ? "0" : this.Noofdays_I;
                                claimresdetails.CurrentNoofdays = this.Noofdays_R == null ? "0" : this.Noofdays_R;
                                claimresdetails.MovementNoofdays = this.Noofdays_O == null ? "0" : this.Noofdays_O;
                                claimresdetails.InitialRateperday = this.Rateperday_I;
                                claimresdetails.CurrentRateperday = this.Rateperday_R;
                                claimresdetails.MovementlRateperday = this.Rateperday_O;
                            }
                            else if (this.ReserveDetails[i].CompCode == "CELOI") {
                                claimresdetails.InitialNoofdays = this.LOINoofdays_I == null ? "0" : this.LOINoofdays_I;
                                claimresdetails.CurrentNoofdays = this.LOINoofdays_R == null ? "0" : this.LOINoofdays_R;
                                claimresdetails.MovementNoofdays = this.LOINoofdays_O == null ? "0" : this.LOINoofdays_O;
                                claimresdetails.InitialRateperday = this.LOIRateperday_I;
                                claimresdetails.CurrentRateperday = this.LOIRateperday_R;
                                claimresdetails.MovementlRateperday = this.LOIRateperday_O;
                            }
                            else if (this.ReserveDetails[i].CompCode == "CELOU")
                            {
                                claimresdetails.InitialNoofdays = this.LOUNoofdays_I == null ? "0" : this.LOUNoofdays_I;
                                claimresdetails.CurrentNoofdays = this.LOUNoofdays_R == null ? "0" : this.LOUNoofdays_R;
                                claimresdetails.MovementNoofdays = this.LOUNoofdays_O == null ? "0" : this.LOUNoofdays_O;
                                claimresdetails.InitialRateperday = this.LOURateperday_I;
                                claimresdetails.CurrentRateperday = this.LOURateperday_R;
                                claimresdetails.MovementlRateperday = this.LOURateperday_O;
                            }
                            else if (this.ReserveDetails[i].CompCode == "CR")
                            {
                                claimresdetails.InitialNoofdays = this.CRNoofdays_I == null ? "0" : this.CRNoofdays_I;
                                claimresdetails.CurrentNoofdays = this.CRNoofdays_R == null ? "0" : this.CRNoofdays_R;
                                claimresdetails.MovementNoofdays = this.CRNoofdays_O == null ? "0" : this.CRNoofdays_O;
                                claimresdetails.InitialRateperday = this.CRRateperday_I;
                                claimresdetails.CurrentRateperday = this.CRRateperday_R;
                                claimresdetails.MovementlRateperday = this.CRRateperday_O;
                            }
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
                        claimresdetails.Createdby = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
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


        public ClmClaimQutmPCNTXModel FetchReserve(string ReserveId, ClmClaimQutmPCNTXModel model, string InAdjustShowOutStandingAsInitial)
        {
            MCASEntities db = new MCASEntities();
            var TransactonHistoryList = (from l in db.MNT_Lookups where l.Category == "TranComponent" && l.IsActive == "Y" select l.Description).ToList();
            try
            {
                model.RateperdayList = ClaimReserveModel.FetchRateperdayList();
                //model.SelectListClamiantName = SelectOnlyList();
                //model.HopitalNameIdList = FetchHopitalNameIdList();
                //model.AssignToIdList = FetchAssignToIdListList();
                int rid = Convert.ToInt32(string.IsNullOrEmpty(ReserveId) ? "0" : ReserveId);
                var ReserveSummary = (from l in db.CLM_ReserveSummary where l.ReserveId == rid select l).FirstOrDefault();
                if (ReserveSummary != null && rid != 0)
                {
                    model.InitialReserve = ReserveSummary.InitialReserve;
                    model.PreviousReserve = ReserveSummary.PreReserve; ;
                    model.CurrentReserve = ReserveSummary.CurrentReserve;
                    model.MovementReserve = ReserveSummary.MovementReserve;
                    model.CreatedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
                    model.CreatedOn = Convert.ToDateTime(ReserveSummary.Createddate);
                    if (ReserveSummary.Modifieddate != null)
                    {
                        model.ModifiedOn = ReserveSummary.Modifieddate;
                        model.ModifiedBy = Convert.ToString(HttpContext.Current.Session["LoggedInUserName"]);
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
                        foreach (var remain in remainlist)
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
                    model.Noofdays_I = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CELOR" select detail.InitialNoofdays).FirstOrDefault();
                    model.Noofdays_R = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CELOR" select detail.MovementNoofdays).FirstOrDefault();
                    model.Noofdays_O = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CELOR" select detail.CurrentNoofdays).FirstOrDefault();
                    model.Rateperday_I = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CELOR" select detail.InitialRateperday).FirstOrDefault();
                    model.Rateperday_R = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CELOR" select detail.MovementlRateperday).FirstOrDefault();
                    model.Rateperday_O = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CELOR" select detail.CurrentRateperday).FirstOrDefault();
                    model.LOINoofdays_I = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CELOI" select detail.InitialNoofdays).FirstOrDefault();
                    model.LOINoofdays_R = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CELOI" select detail.MovementNoofdays).FirstOrDefault();
                    model.LOINoofdays_O = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CELOI" select detail.CurrentNoofdays).FirstOrDefault();
                    model.LOIRateperday_I = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CELOI" select detail.InitialRateperday).FirstOrDefault();
                    model.LOIRateperday_R = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CELOI" select detail.MovementlRateperday).FirstOrDefault();
                    model.LOIRateperday_O = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CELOI" select detail.CurrentRateperday).FirstOrDefault();
                    model.LOUNoofdays_I = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CELOU" select detail.InitialNoofdays).FirstOrDefault();
                    model.LOUNoofdays_R = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CELOU" select detail.MovementNoofdays).FirstOrDefault();
                    model.LOUNoofdays_O = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CELOU" select detail.CurrentNoofdays).FirstOrDefault();
                    model.LOURateperday_I = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CELOU" select detail.InitialRateperday).FirstOrDefault();
                    model.LOURateperday_R = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CELOU" select detail.MovementlRateperday).FirstOrDefault();
                    model.LOURateperday_O = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CELOU" select detail.CurrentRateperday).FirstOrDefault();
                    model.CRNoofdays_I = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CR" select detail.InitialNoofdays).FirstOrDefault();
                    model.CRNoofdays_R = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CR" select detail.MovementNoofdays).FirstOrDefault();
                    model.CRNoofdays_O = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CR" select detail.CurrentNoofdays).FirstOrDefault();
                    model.CRRateperday_I = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CR" select detail.InitialRateperday).FirstOrDefault();
                    model.CRRateperday_R = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CR" select detail.MovementlRateperday).FirstOrDefault();
                    model.CRRateperday_O = (from detail in ReserveDetailList where detail.CmpCode.Trim() == "CR" select detail.CurrentRateperday).FirstOrDefault();

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

        public static List<ClmClaimQutmPCNTXModel> Fetchall(string resid, string AccidentClaimId)
        {
            MCASEntities _db = new MCASEntities();
            var items = new List<ClmClaimQutmPCNTXModel>();
            var list = _db.Proc_GetCLM_Reserve_ClaimTypeList(resid, AccidentClaimId).ToList();
            items = (from n in list
                     select new ClmClaimQutmPCNTXModel()
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



        public static List<ClmClaimQutmPCNTXModelCollection> FetchReservelist(int AccidentClaimId)
        {
            MCASEntities db = new MCASEntities();
            var items = new List<ClmClaimQutmPCNTXModelCollection>();
            try
            {
                items = (from data in db.Proc_GetCLM_ReserveList(Convert.ToString(AccidentClaimId)).ToList()
                         select new ClmClaimQutmPCNTXModelCollection()
                         {
                             RecordNumber = data.ClaimRecordNo,
                             ClaimID = data.ClaimID,
                             TPVehicleNo = data.TPVehicleNo,
                             TPInsurer = data.TPInsurer,
                             ClaimType = data.ClaimType,
                             AccidentClaimId = data.AccidentClaimId,
                             PolicyId = data.PolicyId,
                             ReserveId = data.ReserveId,
                             Total_O = data.CurrentReserve,
                             ClaimTypeCode = data.ClaimTypeCode,
                             ClaimTypeDesc = data.ClaimType.ToString() == "4" ? "Recovery Claim" : "",
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

        public static List<ClaimantStatus> GetTPVehNoList(int Acc)
        {
            MCASEntities _db = new MCASEntities();
            var list = (from m in _db.CLM_Claims where m.AccidentClaimId == Acc select new ClaimantStatus()
                    {
                        Id = m.ClaimID,
                        Text = m.TPVehicleNo
                    }).ToList();
            list.Insert(0, new ClaimantStatus() { Id = 0, Text = "[Select...]" });
            _db.Dispose();
            return list;
        }

        public static List<ClaimantStatus> GetTPInsurerList(int Acc)
        {
            MCASEntities _db = new MCASEntities();
            var list = (from m in _db.CLM_Claims
                        where m.AccidentClaimId == Acc
                        select new ClaimantStatus()
                        {
                            Id = m.ClaimID,
                            Text = m.TPInsurer
                        }).ToList();
            list.Insert(0, new ClaimantStatus() { Id = 0, Text = "[Select...]" });
            _db.Dispose();
            return list;
        }

    }

    public class ClmClaimQutmPCNTXModelCollection
  {
      #region "Object Properties"
      public Int64? OrgRecordNumber { get; set; }
      public string RecordNumber { get; set; }
      public int? ClaimID { get; set; }
      public string TPVehicleNo { get; set; }
      public string TPInsurer { get; set; }
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
}
