/******************************************************************************************
<Author					: -  Pradeep Kushwaha
<Start Date				: -	 01 - Nov - 2010
<End Date				: -	
<Description			: -  Boleto re-print info
<Review Date			: - 
<Reviewed By			: - 	
 
Modification History
<Modified Date			: 
<Modified By			: 
<Purpose				: 
						
****************************************************************************************** */
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Configuration;
using System.Collections.Generic;
using Cms.BusinessLayer.BlApplication;
using Cms.Model.Application;
using Cms.Model.Quote;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlClient;
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Policy;
using System.Resources;
using Cms.BusinessLayer.Blapplication;
using System.Linq;
using Cms.Model.Account;
using Cms.BusinessLayer.BlBoleto;
using Cms.BusinessLayer.BlProcess;//itrack 1383

namespace Cms.Policies.Aspx
{
    public partial class BoletoRePrint :  Cms.Policies.policiesbase
    {
        protected List<ClsPolicyBillingInfo> lstBillingInfo = new List<ClsPolicyBillingInfo>();
        Cms.BusinessLayer.Blapplication.ClsPolicyInstallments objclsBLInstallments = new Cms.BusinessLayer.Blapplication.ClsPolicyInstallments();
        ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.BoletoRePrint", System.Reflection.Assembly.GetExecutingAssembly()); //Resource manager For Multilingual Support
         
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "224_38";
            numberFormatInfo.NumberDecimalDigits = 2;
            if (!IsPostBack)
            {
                this.SetErrorMessages();
                hidCUSTOMER_ID.Value = GetCustomerID();
                hidPOLICY_ID.Value = GetPolicyID();
                hidPOLICY_VERSION_ID.Value = GetPolicyVersionID();
                hidPOLICY_STATUS.Value = GetPolicyStatus();
                hidCALLED_FROM.Value = Request.QueryString["CALLEDFROM"].ToString();
                if(Request.QueryString["CALLED_FOR"]!=null && Request.QueryString["CALLED_FOR"].ToString().Trim()!="")
                 hidCALLED_FOR.Value = Request.QueryString["CALLED_FOR"].ToString();
             
                BindGrid();
            }// if (!IsPostBack)
            SetCaptions();
            btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            //btnSave.PermissionString = gstrSecurityXML;
            btnSave.PermissionString ="<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
            
        }//protected void Page_Load(object sender, EventArgs e)
        private void SetErrorMessages()
        {
            hidUnSavedMsg.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
            hidFirstInstallMsg.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7");
            hidSecandInstallMsg.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");
            hidInsatallExipreMsg.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8");
            HidInstalDueDateFromCancelMsg.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "10");
        }
        /// <summary>
        /// Use to bind the grid if the installment have been generated. else bind the grid with blank
        /// </summary>
        private void BindGrid()
        {
            DataSet ds = new DataSet();
            lstBillingInfo = ClsPolicyInstallments.GetBoletoReprintInstallmentDetails(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));
            if (lstBillingInfo.Count > 0)
            {
                numberFormatInfo.NumberDecimalDigits = 2;
                grdBOLETO_REPRINT.DataSource = lstBillingInfo;
                grdBOLETO_REPRINT.DataBind();
              
            }
            else
            {
                this.BindBlankGrid();
            }
            base.SetPageModelObjects(lstBillingInfo);
            hidGRID_ROW_COUNT.Value = grdBOLETO_REPRINT.Rows.Count.ToString();
        }// private void BindGrid()
        /// <summary>
        /// Use to set the caption of controls 
        /// </summary>
        private void SetCaptions()
        {
            lblHeader.Text = objResourceMgr.GetString("lblHeader");
            btnBack.Text = ClsMessages.GetButtonsText(ScreenId,"btnBack");
        }// private void SetCaptions()

        /// <summary>
        /// Use to bind the grid with blank records
        /// </summary>
        private void BindBlankGrid()
        {
           
            lstBillingInfo = new List<ClsPolicyBillingInfo>();

            DataTable dt = new DataTable();
            dt.Columns.Add("ROW_ID", typeof(string));
            dt.Columns.Add("INSTALLMENT_NO", typeof(string));
            dt.Columns.Add("BOLETO_NO", typeof(string));
            dt.Columns.Add("INSTALLMENT_EFFECTIVE_DATE", typeof(string));
            dt.Columns.Add("INSTALLMENT_AMOUNT", typeof(string));
            dt.Columns.Add("INTEREST_AMOUNT", typeof(string));
            dt.Columns.Add("FEE", typeof(string));
            dt.Columns.Add("TAXES", typeof(string));
            dt.Columns.Add("TOTAL", typeof(string));
            dt.Columns.Add("INSTALLMENT_EXPIRE_DATE", typeof(string));
            DataRow dr = dt.NewRow();
            dr["ROW_ID"] = "0";
            dr["INSTALLMENT_NO"] = "0";
            dr["BOLETO_NO"] = "";
            dr["INSTALLMENT_EFFECTIVE_DATE"] = "";
            dr["INSTALLMENT_AMOUNT"] = "";
            dr["INTEREST_AMOUNT"] = "";    
            dr["FEE"] = "";        
            dr["TAXES"] = "";             
            dr["TOTAL"] = "";
            dr["INSTALLMENT_EXPIRE_DATE"] = "";
            dt.Rows.Add(dr);
            foreach (DataRow dro in dt.Rows)
            {
                ClsPolicyBillingInfo objmodelInfo = new ClsPolicyBillingInfo();
                ClsCommon.PopulateEbixPageModel(dro, objmodelInfo);
                lstBillingInfo.Add(objmodelInfo);
            }//foreach (DataRow dro in dt.Rows)
            grdBOLETO_REPRINT.DataSource = lstBillingInfo;
            grdBOLETO_REPRINT.DataBind();
            if (grdBOLETO_REPRINT.Rows.Count > 0)
            {
                for (int i = 0; i < grdBOLETO_REPRINT.Rows[0].Cells.Count; i++)
                    grdBOLETO_REPRINT.Rows[0].Cells[i].Controls.Clear();
            }//if (grdBOLETO_REPRINT.Rows.Count > 0)

        }//private void BindBlankGrid()

        /// <summary>
        /// Get the Installment details data modifing installment amount and date according for Boleto reprint
        /// </summary>
        /// <returns></returns>
        private ArrayList GetUpdatedInstallmentDetails()
        {
            ArrayList objBillingInfoList = new ArrayList();

            lstBillingInfo = (List<ClsPolicyBillingInfo>)GetPageModelObjects();
            int flag = 0;
            int totalUpdatedRows = 0;
            if (hidUPDATEDROWS.Value != "")
            {
                totalUpdatedRows = Convert.ToInt32(hidUPDATEDROWS.Value);
            }// if (hidUPDATEDROWS.Value != "")
          
            foreach (GridViewRow rw in grdBOLETO_REPRINT.Rows)
            {
                HtmlInputHidden hidUPDATEFlag = (HtmlInputHidden)rw.FindControl("hidUPDATEFlag");
                Label lblINSTALLMENT_NO = (Label)rw.FindControl("lblINSTALLMENT_NO");
                Label lblPOLICY_VERSION_ID = (Label)rw.FindControl("lblPOLICY_VERSION_ID");
                Label lblROW_ID = (Label)rw.FindControl("lblROW_ID"); 
                TextBox txtINSTALLMENT_DATE = (TextBox)rw.FindControl("txtINSTALLMENT_DATE");
                HtmlInputHidden hidINSTALLMENT_DATE = (HtmlInputHidden)rw.FindControl("hidINSTALLMENT_DATE");
                HtmlInputHidden hidINSTALLMENT_EXPIRE_DATE = (HtmlInputHidden)rw.FindControl("hidINSTALLMENT_EXPIRE_DATE");
                HtmlInputHidden hidINSTALLMENT_EXPIRE_DATE1 = (HtmlInputHidden)rw.FindControl("hidINSTALLMENT_EXPIRE_DATE1");

                
                
                TextBox txtINSTALLMENT_EXPIRE_DATE = (TextBox)rw.FindControl("txtINSTALLMENT_EXPIRE_DATE");
                TextBox txtINTEREST_AMOUNT = (TextBox)rw.FindControl("txtINTEREST_AMOUNT");
                TextBox txtTOTAL = (TextBox)rw.FindControl("txtTOTAL");
                HtmlInputHidden hidTOTAL = (HtmlInputHidden)rw.FindControl("hidTOTAL");

                if (hidUPDATEFlag.Value.Equals("1"))
                {

                    //ClsPolicyBillingInfo objBillingInfo = lstBillingInfo.Select((ob, id) => new { reinsurer = ob, REINID = id }).Where(ob => ob.reinsurer.REINSURANCE_ID.CurrentValue == int.Parse(lb.Text.Trim()) && ob.reinsurer.COMPANY_ID.CurrentValue == CompanyId).Select(ob => ob.reinsurer).First();
                    //Find model object from list using lamda expression
                    
                        ClsPolicyBillingInfo objBillingInfo = lstBillingInfo.Select((ob, id) => new { BillingInfo = ob, instl = id }).Where(ob => ob.BillingInfo.INSTALLMENT_NO.CurrentValue ==int.Parse(lblINSTALLMENT_NO.Text.Trim()) && ob.BillingInfo.POLICY_VERSION_ID.CurrentValue == int.Parse(lblPOLICY_VERSION_ID.Text )&& ob.BillingInfo.ROW_ID.CurrentValue == int.Parse(lblROW_ID.Text)).Select(ob => ob.BillingInfo).First();
                        
                        objBillingInfo.ACTION = enumAction.Update;

                        if (lblINSTALLMENT_NO.Text != "")
                            //if (int.Parse(lblPOLICY_VERSION_ID.Text) == int.Parse(hidPOLICY_VERSION_ID.Value))
                            //{
                                objBillingInfo.INSTALLMENT_NO.CurrentValue = int.Parse(lblINSTALLMENT_NO.Text);
                                objBillingInfo.INSTALLMENT_NO.IsChanged = true;

                                #region Comment on 19-Jan-2011 (Do not update interest amount and total)
                                //if (txtINTEREST_AMOUNT.Text != "")
                                //{
                                //    objBillingInfo.INTEREST_AMOUNT.CurrentValue = double.Parse(txtINTEREST_AMOUNT.Text, numberFormatInfo);
                                //    objBillingInfo.TOTAL_INTEREST_AMOUNT.CurrentValue = this.TotalchangeInterest(lstBillingInfo, int.Parse(lblPOLICY_VERSION_ID.Text)); //double.Parse(txtINTEREST_AMOUNT.Text, numberFormatInfo);
                                //}//if (txtINTEREST_AMOUNT.Text != "")
                                //else
                                //    objBillingInfo.INTEREST_AMOUNT.CurrentValue = 0;

                                //if (hidTOTAL.Value != "")
                                //{
                                //    objBillingInfo.TOTAL.CurrentValue = double.Parse(hidTOTAL.Value, numberFormatInfo);
                                //    //objBillingInfo.TOTAL_AMOUNT.CurrentValue = double.Parse(hidTOTAL.Value, numberFormatInfo);
                                //    objBillingInfo.TOTAL_AMOUNT.CurrentValue = TotalchangeAmount(lstBillingInfo, int.Parse(lblPOLICY_VERSION_ID.Text));
                                //}
                                #endregion


                                if (hidINSTALLMENT_DATE.Value != "")
                                {
                                    objBillingInfo.INSTALLMENT_EFFECTIVE_DATE.CurrentValue = ConvertToDate(hidINSTALLMENT_DATE.Value);
                                }
                                if (hidINSTALLMENT_EXPIRE_DATE.Value != "")
                                {
                                    objBillingInfo.INSTALLMENT_EXPIRE_DATE.CurrentValue = ConvertToDate(hidINSTALLMENT_EXPIRE_DATE.Value);
                                }
                                objBillingInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                                objBillingInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                                objBillingInfo.LAST_UPDATED_DATETIME.CurrentValue = DateTime.Now;

                                flag = flag + 1;

                                //Update Policy Total Amounts In ACT_POLICY_INSTALL_PALN_DATA Table                   
                                //Update Total amount if gridview row has updated.
                                //create Model Object set Policy Total amount values ie:Total Policy Premium, Total Policy Taxes etc.
                                //set value in last updated installment object 
                                //Start

                                #region Comment On 07-Dec-2010 
                                //if (flag == totalUpdatedRows)  //Check for object add in array list at last when all updated row will added in arraylist   
                                //{
                                //    objBillingInfo.TOTAL_INTEREST_AMOUNT.CurrentValue = this.TotalInterest(lstBillingInfo, int.Parse(lblPOLICY_VERSION_ID.Text));
                                //    objBillingInfo.TOTAL_AMOUNT.CurrentValue = this.TotalAmount(lstBillingInfo,int.Parse(lblPOLICY_VERSION_ID.Text ));
                                //    objBillingInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                                //    objBillingInfo.LAST_UPDATED_DATETIME.CurrentValue = DateTime.Now;

                                //    objBillingInfoList.Add(objBillingInfo);  //Add  model Object for total value field in array list
                                //}//if (flag == totalUpdatedRows)
                                //else
                                //{
                                //    objBillingInfoList.Add(objBillingInfo);
                                //}
                                #endregion

                                objBillingInfoList.Add(objBillingInfo);
                            //}//if (int.Parse(lblPOLICY_VERSION_ID.Text) == int.Parse(hidPOLICY_VERSION_ID.Value))
                     
                }//if (hidUPDATEFlag.Value.Equals("1"))

            }//foreach (GridViewRow rw in grdBOLETO_REPRINT.Rows)
            return objBillingInfoList;
        } //update billing installments data

        private Decimal TotalchangeInterest(List<ClsPolicyBillingInfo> lstBillingInfo, int Polic_version_id) 
        {
            Decimal PreviousInterestTotal = 0;
            Decimal CurrentInterestTotal = 0;
            Decimal ChangeInAmount = 0;
            if (lstBillingInfo.Count > 0)
            {
                PreviousInterestTotal = lstBillingInfo.Where(x => x.POLICY_VERSION_ID.CurrentValue == Polic_version_id).Select(o => o.TOTAL_INTEREST_AMOUNT.CurrentValue).First();
                CurrentInterestTotal = lstBillingInfo.Where(x => x.POLICY_VERSION_ID.CurrentValue == Polic_version_id).Sum(o => o.INTEREST_AMOUNT.CurrentValue);
                ChangeInAmount = CurrentInterestTotal - PreviousInterestTotal;
            }//if (lstBillingInfo.Count > 0)
            return Math.Round(ChangeInAmount, 2);
        }
        private Decimal TotalchangeAmount(List<ClsPolicyBillingInfo> lstBillingInfo, int Polic_version_id)
        {
            Decimal PreviousTotalAmount = 0;
            Decimal CurrentTotal = 0;
            Decimal ChangeInAmount = 0;
            if (lstBillingInfo.Count > 0)
            {
                PreviousTotalAmount = lstBillingInfo.Where(x => x.POLICY_VERSION_ID.CurrentValue == Polic_version_id).Select(o => o.TOTAL_AMOUNT.CurrentValue).First();
                CurrentTotal = lstBillingInfo.Where(x => x.POLICY_VERSION_ID.CurrentValue == Polic_version_id).Sum(o => o.TOTAL.CurrentValue);
                ChangeInAmount = CurrentTotal - PreviousTotalAmount;
            }//if (lstBillingInfo.Count > 0)
            return Math.Round(ChangeInAmount, 2);
        }
        /// <summary>
        /// Get the Sum of the Total Amount 
        /// </summary>
        /// <param name="lstBillingInfo"></param>
        /// <returns></returns>
        private Decimal TotalAmount(List<ClsPolicyBillingInfo> lstBillingInfo, int Polic_version_id)
        {
            Decimal Total = 0;
            if (lstBillingInfo.Count > 0)
            {
                Total = lstBillingInfo.Where(x => x.POLICY_VERSION_ID.CurrentValue == Polic_version_id).Sum(x => x.TOTAL.CurrentValue);

            }//if (lstBillingInfo.Count > 0)
            return Math.Round(Total, 2);
        }// private Double TotalAmount(List<ClsPolicyBillingInfo> lstBillingInfo )
        
        /// <summary>
        /// Get the Sum of the Interest Amount 
        /// </summary>
        /// <param name="lstBillingInfo"></param>
        /// <returns></returns>
        private Decimal TotalInterest(List<ClsPolicyBillingInfo> lstBillingInfo, int Policy_version_id)
        {
            Decimal TotalInterest = 0;
            if (lstBillingInfo.Count > 0)
            {
                 TotalInterest = lstBillingInfo.Where(x => x.POLICY_VERSION_ID.CurrentValue == Policy_version_id).Sum(x => x.INTEREST_AMOUNT.CurrentValue);

            }//if (lstBillingInfo.Count > 0)
            return Math.Round(TotalInterest, 2);
        }// private Double TotalInterest(List<ClsPolicyBillingInfo> lstBillingInfo)
       
        /// <summary>
        /// Save Installment data on button save click event
        /// </summary>
        /// 
        private void SaveBoletoRePrint()
        {
            if (grdBOLETO_REPRINT.Rows.Count > 0)
            {
                ArrayList objBillingInfoList = new ArrayList();
                int retval = 0;
                try
                {
                    objBillingInfoList = GetUpdatedInstallmentDetails();
                    if (objBillingInfoList.Count > 0)
                    {
                        retval = objclsBLInstallments.SaveBoletoReprintInstallmentsDetailsHistory(objBillingInfoList);
                        if (retval > 0)
                        {
                            this.GenerateBoletoForPrPrint(objBillingInfoList);
                            lblMessage.Text = lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                            BindGrid();
                            hidUPDATEDROWS.Value = "";
                            hidCHANGEDROW.Value = "";
                            foreach (GridViewRow rw in grdBOLETO_REPRINT.Rows)
                            { HtmlInputHidden hidUPDATEFlag = (HtmlInputHidden)rw.FindControl("hidUPDATEFlag"); hidUPDATEFlag.Value = ""; }
                            #region Commented by Pradeep Kushwaha Itrack - 835
                            //if (hidCALLED_FOR.Value == "MST_POL_BILL")
                            //    ClientScript.RegisterStartupScript(this.GetType(), "BoletoReprint", "<script language=javascript> self.location ='/cms/Policies/Aspx/MasterPolicyBillingInfo.aspx?CALLEDFROM=" + hidCALLED_FROM.Value + "&';</script>");
                            //else
                            //    ClientScript.RegisterStartupScript(this.GetType(), "BoletoReprint", "<script language=javascript> self.location ='/cms/Policies/Aspx/BillingInfo.aspx?CALLEDFROM=" + hidCALLED_FROM.Value + "&'; </script>");
                            #endregion Till Here

                        }//if (retval > 0)
                        else if (retval == -1)
                        {
                            lblMessage.Text = lblMessage.Text = ClsMessages.GetMessage(ScreenId, "4");
                        }//else if (retval == -1)

                    }// if (objBillingInfoList.Count > 0)
                    else
                    {
                        BindGrid();
                        hidUPDATEDROWS.Value = "";
                        hidCHANGEDROW.Value = "";
                        #region Commented by Pradeep Kushwaha Itrack - 835
                        //if (hidCALLED_FOR.Value == "MST_POL_BILL")
                        //    ClientScript.RegisterStartupScript(this.GetType(), "BoletoReprint", "<script language=javascript> self.location ='/cms/Policies/Aspx/MasterPolicyBillingInfo.aspx?CALLEDFROM=" + hidCALLED_FROM.Value + "&'; </script>");
                        //else
                        //    ClientScript.RegisterStartupScript(this.GetType(), "BoletoReprint", "<script language=javascript> self.location ='/cms/Policies/Aspx/BillingInfo.aspx?CALLEDFROM=" + hidCALLED_FROM.Value + "&';</script>");
                        #endregion Till Here
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = lblMessage.Text = ClsMessages.GetMessage(ScreenId, "2") + ": " + ex.Message;
                }// catch (Exception ex)
            }//if (grdBOLETO_REPRINT.Rows.Count > 0)
        }//private void SaveBoletoRePrint()

        private void GenerateBoletoForPrPrint(ArrayList objClsPolicyBillingInfoList)
        {   ClsProductPdfXml objClsProductPdfXml = new ClsProductPdfXml();//itrack 1383
            if (objClsPolicyBillingInfoList.Count > 0)
            {
                for (int CountList = 0; CountList < objClsPolicyBillingInfoList.Count; CountList++)
                {
                    ClsPolicyBillingInfo objBillingInfo = new ClsPolicyBillingInfo();
                    objBillingInfo = (ClsPolicyBillingInfo)objClsPolicyBillingInfoList[CountList];
                    ClsBoleto objBoleto = new ClsBoleto();
                   objBoleto.GenereteBoletoForRePrint(objBillingInfo.CUSTOMER_ID.CurrentValue,
                        objBillingInfo.POLICY_ID.CurrentValue, objBillingInfo.POLICY_VERSION_ID.CurrentValue,
                       objBillingInfo.ROW_ID.CurrentValue, objBillingInfo.INSTALLMENT_NO.CurrentValue, int.Parse(GetUserId()), objBillingInfo.CO_APPLICANT_ID.CurrentValue);

                   //itrack 1383
                   objClsProductPdfXml.InsertPrintJobsEntryOfBoletoRePrint(objBillingInfo.CUSTOMER_ID.CurrentValue,
                       objBillingInfo.POLICY_ID.CurrentValue, objBillingInfo.POLICY_VERSION_ID.CurrentValue,
                        objBillingInfo.CO_APPLICANT_ID.CurrentValue, int.Parse(GetUserId()));
                    //till here 
                }
            }
        }
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveBoletoRePrint();
        }////protected void btnSave_Click(object sender, EventArgs e)

        protected void grdBOLETO_REPRINT_RowDataBound(object sender, GridViewRowEventArgs e)
        {
             
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Label capINSLAMENT_NO = (Label)e.Row.FindControl("capINSLAMENT_NO");
                capINSLAMENT_NO.Text = objResourceMgr.GetString("lblPOLICY_VERSION_ID");
          
                Label capINSTALLMENT_DATE = (Label)e.Row.FindControl("capINSTALLMENT_DATE");
                capINSTALLMENT_DATE.Text = objResourceMgr.GetString("txtINSTALLMENT_DATE");

                Label capPREMIUM = (Label)e.Row.FindControl("capPREMIUM");
                capPREMIUM.Text = objResourceMgr.GetString("txtPREMIUM");

                Label capINTEREST_AMOUNT = (Label)e.Row.FindControl("capINTEREST_AMOUNT");
                capINTEREST_AMOUNT.Text = objResourceMgr.GetString("txtINTEREST_AMOUNT");

                Label capFEE = (Label)e.Row.FindControl("capFEE");
                capFEE.Text = objResourceMgr.GetString("txtFEE");

                Label capTAXES = (Label)e.Row.FindControl("capTAXES");
                capTAXES.Text = objResourceMgr.GetString("txtTAXES");

                Label capTOTAL = (Label)e.Row.FindControl("capTOTAL");
                capTOTAL.Text = objResourceMgr.GetString("txtTOTAL");

                Label capBOLETO = (Label)e.Row.FindControl("capBOLETO");
                capBOLETO.Text = objResourceMgr.GetString("capBOLETO");

                Label capINSTALLMENT_EXPIRE_DATE = (Label)e.Row.FindControl("capINSTALLMENT_EXPIRE_DATE");
                capINSTALLMENT_EXPIRE_DATE.Text = objResourceMgr.GetString("capINSTALLMENT_EXPIRE_DATE");

                Label capEDIT = (Label)e.Row.FindControl("capEDIT");
                capEDIT.Text = objResourceMgr.GetString("capEDIT");

                Label capBOLETO_NO = (Label)e.Row.FindControl("capBOLETO_NO");
                capBOLETO_NO.Text = objResourceMgr.GetString("capBOLETO_NO");


            }//if (e.Row.RowType == DataControlRowType.Header)
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblINSTALLMENT_NO = (Label)e.Row.FindControl("lblINSTALLMENT_NO");
                Label lblROW_ID = (Label)e.Row.FindControl("lblROW_ID");
                Label lblBOLETO = (Label)e.Row.FindControl("lblBOLETO");
                Button btnEDIT = (Button)e.Row.FindControl("btnEDIT");
                lblBOLETO.Text = objResourceMgr.GetString("lblBOLETO");
                btnEDIT.Text = objResourceMgr.GetString("capEDIT");

                Label lblPOLICY_VERSION_ID = (Label)e.Row.FindControl("lblPOLICY_VERSION_ID");
                TextBox txtINSTALLMENT_DATE = (TextBox)e.Row.FindControl("txtINSTALLMENT_DATE");
                TextBox txtPREMIUM = (TextBox)e.Row.FindControl("txtPREMIUM");
                TextBox txtINTEREST_AMOUNT = (TextBox)e.Row.FindControl("txtINTEREST_AMOUNT");
                TextBox txtFEE = (TextBox)e.Row.FindControl("txtFEE");
                TextBox txtTAXES = (TextBox)e.Row.FindControl("txtTAXES");
                TextBox txtTOTAL = (TextBox)e.Row.FindControl("txtTOTAL");
                TextBox txtINSTALLMENT_EXPIRE_DATE = (TextBox)e.Row.FindControl("txtINSTALLMENT_EXPIRE_DATE");
                System.Web.UI.HtmlControls.HtmlInputHidden hidINSTALLMENT_EXPIRE_DATE = (System.Web.UI.HtmlControls.HtmlInputHidden)e.Row.FindControl("hidINSTALLMENT_EXPIRE_DATE");
                System.Web.UI.HtmlControls.HtmlInputHidden hidINSTALLMENT_EXPIRE_DATE1 = (System.Web.UI.HtmlControls.HtmlInputHidden)e.Row.FindControl("hidINSTALLMENT_EXPIRE_DATE1");
                System.Web.UI.HtmlControls.HtmlInputHidden hidINSTALLMENT_DATE = (System.Web.UI.HtmlControls.HtmlInputHidden)e.Row.FindControl("hidINSTALLMENT_DATE");
                
                System.Web.UI.HtmlControls.HtmlInputHidden hidEND_EFFECTIVE_DATE = (System.Web.UI.HtmlControls.HtmlInputHidden)e.Row.FindControl("hidEND_EFFECTIVE_DATE");
                System.Web.UI.HtmlControls.HtmlInputHidden hidEND_EXPIRY_DATE = (System.Web.UI.HtmlControls.HtmlInputHidden)e.Row.FindControl("hidEND_EXPIRY_DATE");
                System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_EFFECTIVE_DATE = (System.Web.UI.HtmlControls.HtmlInputHidden)e.Row.FindControl("hidPOLICY_EFFECTIVE_DATE");
                System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_EXPIRATION_DATE = (System.Web.UI.HtmlControls.HtmlInputHidden)e.Row.FindControl("hidPOLICY_EXPIRATION_DATE");

                
                RegularExpressionValidator revTAXES = (RegularExpressionValidator)e.Row.FindControl("revTAXES");
                RegularExpressionValidator revFEE = (RegularExpressionValidator)e.Row.FindControl("revFEE");
                RegularExpressionValidator revINTEREST_AMOUNT = (RegularExpressionValidator)e.Row.FindControl("revINTEREST_AMOUNT");
                RegularExpressionValidator revPREMIUM = (RegularExpressionValidator)e.Row.FindControl("revPREMIUM");
                RequiredFieldValidator rfvINSTALLMENT_DATE = (RequiredFieldValidator)e.Row.FindControl("rfvINSTALLMENT_DATE");
                RequiredFieldValidator rfvINTEREST_AMOUNT = (RequiredFieldValidator)e.Row.FindControl("rfvINTEREST_AMOUNT");
                RequiredFieldValidator rfvINSTALLMENT_EXPIRE_DATE = (RequiredFieldValidator)e.Row.FindControl("rfvINSTALLMENT_EXPIRE_DATE");
                
                rfvINTEREST_AMOUNT.ErrorMessage = ClsMessages.GetMessage(ScreenId, "1");
                rfvINSTALLMENT_DATE.ErrorMessage = ClsMessages.GetMessage(ScreenId, "2");
                rfvINSTALLMENT_EXPIRE_DATE.ErrorMessage = ClsMessages.GetMessage(ScreenId, "5");

                RegularExpressionValidator revINSTALLMENT_DATE = (RegularExpressionValidator)e.Row.FindControl("revINSTALLMENT_DATE");
                RegularExpressionValidator revINSTALLMENT_EXPIRE_DATE = (RegularExpressionValidator)e.Row.FindControl("revINSTALLMENT_EXPIRE_DATE");


                revTAXES.ValidationExpression = aRegExpCurrencyformat;
                revTAXES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

                revFEE.ValidationExpression = aRegExpCurrencyformat;
                revFEE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

                revINTEREST_AMOUNT.ValidationExpression = aRegExpCurrencyformat;
                revINTEREST_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

                revPREMIUM.ValidationExpression = aRegExpCurrencyformat;
                revPREMIUM.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");


                revINSTALLMENT_DATE.ValidationExpression = aRegExpDate;
                revINSTALLMENT_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");


                DateTime dt = Convert.ToDateTime(txtINSTALLMENT_DATE.Text);
                txtINSTALLMENT_DATE.Text = ConvertToDateCulture(dt); //ConvertDBDateToCulture(txtINSTALLMENT_DATE.Text);
                hidINSTALLMENT_DATE.Value = ConvertToDateCulture(dt); 

                DateTime dtEND_EFFECTIVE_DATE = Convert.ToDateTime(hidEND_EFFECTIVE_DATE.Value);
                hidEND_EFFECTIVE_DATE.Value = ConvertToDateCulture(dtEND_EFFECTIVE_DATE);

                DateTime dtEND_EXPIRY_DATE = Convert.ToDateTime(hidEND_EXPIRY_DATE.Value);
                hidEND_EXPIRY_DATE.Value = ConvertToDateCulture(dtEND_EXPIRY_DATE);

                DateTime dtPOLICY_EFFECTIVE_DATE = Convert.ToDateTime(hidPOLICY_EFFECTIVE_DATE.Value);
                hidPOLICY_EFFECTIVE_DATE.Value = ConvertToDateCulture(dtPOLICY_EFFECTIVE_DATE);

                DateTime dtPOLICY_EXPIRATION_DATE = Convert.ToDateTime(hidPOLICY_EXPIRATION_DATE.Value);
                hidPOLICY_EXPIRATION_DATE.Value = ConvertToDateCulture(dtPOLICY_EXPIRATION_DATE);

                txtINTEREST_AMOUNT.Attributes.Add("onBlur", "InstallmentTotal(this);this.value=formatAmount(this.value)");

                revINSTALLMENT_EXPIRE_DATE.ValidationExpression = aRegExpDate;
                revINSTALLMENT_EXPIRE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
                
                DateTime dtExpireDate = Convert.ToDateTime(txtINSTALLMENT_EXPIRE_DATE.Text);
                txtINSTALLMENT_EXPIRE_DATE.Text = ConvertToDateCulture(dtExpireDate); //ConvertDBDateToCulture(txtINSTALLMENT_DATE.Text);
                hidINSTALLMENT_EXPIRE_DATE.Value= ConvertToDateCulture(dtExpireDate); //ConvertDBDateToCulture(txtINSTALLMENT_DATE.Text);
                hidINSTALLMENT_EXPIRE_DATE1.Value = ConvertToDateCulture(dtExpireDate);

                //if (lstBillingInfo[e.Row.RowIndex].POLICY_VERSION_ID.CurrentValue != int.Parse(hidPOLICY_VERSION_ID.Value))
                //{
                //    txtINTEREST_AMOUNT.ReadOnly = true;
                //    txtINSTALLMENT_DATE.ReadOnly = true;
                     
                //}

            }//  if (e.Row.RowType == DataControlRowType.DataRow)

        }//protected void grdBOLETO_REPRINT_RowDataBound(object sender, GridViewRowEventArgs e) 
       
        [System.Web.Services.WebMethod]
        public static String GetPolicyExpireDate(String CUSTOMER_ID, String POLICY_ID, String POLICY_VERSION_ID, String INSTALLMENT_NO, String INSTALLMENT_DATE)
        {
            String retVal = String.Empty;
            ClsBoleto objObleto = new ClsBoleto();
            CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();  //Declare the object ob common webservice
            String strExpireDate= objObleto.GetPolicyCancellationDate(int.Parse(CUSTOMER_ID), int.Parse(POLICY_ID), int.Parse(POLICY_VERSION_ID), int.Parse(INSTALLMENT_NO));
            DateTime dtExpireDate = Convert.ToDateTime(strExpireDate);
            
            //Itrack-835 Added By Pradeep Kushwaha on 29-March-2011
            DateTime dtINSTALLMENT_DATE = Convert.ToDateTime(INSTALLMENT_DATE);
            if (dtExpireDate < dtINSTALLMENT_DATE)
                dtExpireDate = dtINSTALLMENT_DATE;
            //Added till here 

            String format=String.Empty;
            if (ClsCommon.BL_LANG_ID == 2)
            {
                format = "dd/MM/yyyy";
                retVal = dtExpireDate.ToString(format);
            }
            else
                retVal = ConvertToDateCulture(dtExpireDate);
            
             
            return retVal;
        }
    }
}
