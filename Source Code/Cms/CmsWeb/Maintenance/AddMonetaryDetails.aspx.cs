/******************************************************************************************
	<Author					:  Aditya Goel
	<Start Date				:   23/12/2010
	<End Date				: - >
	<Description			: - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >
*******************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.Model.Maintenance;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BLClaims;
using Cms.BusinessLayer.BlCommon;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Cms.CmsWeb.Maintenance
{
    
    public partial class AddMonetaryDetails : Cms.CmsWeb.cmsbase
    {
      
        
        System.Resources.ResourceManager objResourceMgr;
        ClsMonetaryDetails objMonetaryDetails = new ClsMonetaryDetails();
        private String strRowId = String.Empty;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "524_1";
            lblMessage.Visible = false;
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddMonetaryDetails", System.Reflection.Assembly.GetExecutingAssembly());
            txtINFLATION_RATE.Attributes.Add("onBlur", "this.value=formatRateBase(this.value);");
            txtINTEREST_RATE.Attributes.Add("onBlur", "this.value=formatRateBase(this.value);");
                if (!Page.IsPostBack)
                {

                hlkDATE.Attributes.Add("OnClick", "fPopCalendar(txtDATE,txtDATE)");
                SetCaptions();
                SetErrorMessages();
                GetQueryStringValues();
                GetOldDataObject();


            }
        }

     
        private void GetOldDataObject()
        {
            strRowId = hidMonetaryIndexID.Value;
            if (strRowId.ToUpper().Equals("NEW"))
                return;

            ClsMonetaryInfo objMonetaryInfo = new ClsMonetaryInfo();

            // FILL STATE AS PER THE COUNTRY BECAUSE STATE IS FILL BY USING AJAX WHICH IS CLEAR ON THE POSTBACK          

            objMonetaryInfo.ROW_ID.CurrentValue = int.Parse(hidMonetaryIndexID.Value);
            DataTable dt = objMonetaryDetails.FetchData(ref objMonetaryInfo);

            if (dt != null && dt.Rows.Count > 0)
            {
                hidMonetaryIndexID.Value = dt.Rows[0]["ROW_ID"].ToString();

                //NfiBaseCurrency.NumberDecimalDigits = 2;                
                PopulatePageFromEbixModelObject(this.Page, objMonetaryInfo);

               
                    txtINFLATION_RATE.Text = objMonetaryInfo.INFLATION_RATE.CurrentValue.ToString("N",NfiBaseCurrency);

               
                    txtINTEREST_RATE.Text = objMonetaryInfo.INTEREST_RATE.CurrentValue.ToString("N", NfiBaseCurrency);

                base.SetPageModelObject(objMonetaryInfo);
                btnReset.Enabled = true;
               
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int intRetval;


            ClsMonetaryInfo objMonetaryInfo;
            try
            {
                //For new item to add
                strRowId = hidMonetaryIndexID.Value;
                if (strRowId.ToUpper().Equals("NEW"))
                {
                    objMonetaryInfo = new ClsMonetaryInfo();
                    this.getFormValues(objMonetaryInfo);

                    objMonetaryInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objMonetaryInfo.CREATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());
                    objMonetaryInfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;
                    intRetval = objMonetaryDetails.AddMonetaryInformation(objMonetaryInfo);
                    hidMonetaryIndexID.Value = objMonetaryInfo.ROW_ID.CurrentValue.ToString();
                    

                    if (intRetval > 0)
                    {


                        hidMonetaryIndexID.Value = objMonetaryInfo.ROW_ID.CurrentValue.ToString();

                        this.GetOldDataObject();

                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        hidFormSaved.Value = "1";


                    }
                    else if (intRetval == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                        hidFormSaved.Value = "2";
                    }

                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }
                     lblMessage.Visible = true;
                }
                 //For The Update cse
                else
                {

                    objMonetaryInfo = (ClsMonetaryInfo)base.GetPageModelObject();
                    this.getFormValues(objMonetaryInfo);
                 
                    objMonetaryInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objMonetaryInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                    intRetval = objMonetaryDetails.UpdateMonetaryInformation(objMonetaryInfo);



                    if (intRetval > 0)
                    {
                        hidMonetaryIndexID.Value = objMonetaryInfo.ROW_ID.CurrentValue.ToString();
                        this.GetOldDataObject();
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");

                        hidFormSaved.Value = "1";


                    }
                    else if (intRetval == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                        hidFormSaved.Value = "2";
                    }

                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }

                    lblMessage.Visible = true;

                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
            finally
            {
                //if (objNamedPerilsinfo != null)
                //    objNamedPerilsinfo.Dispose();
            }
        }
        

        protected void btnReset_Click(object sender, EventArgs e)
        {
            
        }


        private void GetQueryStringValues()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ROW_ID"]) && Request.QueryString["ROW_ID"] != "NEW")
                hidMonetaryIndexID.Value = Request.QueryString["ROW_ID"].ToString();
            else
                hidMonetaryIndexID.Value = "NEW";

            
        }

        private void SetCaptions()
        {

            capINFLATION_RATE.Text = objResourceMgr.GetString("txtINFLATION_RATE"); 
            capINTEREST_RATE.Text = objResourceMgr.GetString("txtINTEREST_RATE");
            capDATE.Text = objResourceMgr.GetString("txtDATE");
            revDATE.ValidationExpression = aRegExpDate;
            revDATE.ErrorMessage = ClsMessages.FetchGeneralMessage("22");
            

        }

        private void SetErrorMessages()
        {
            revINFLATION_RATE.ValidationExpression = aRegExpBaseDouble;
            revINFLATION_RATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
            csvINFLATION_RATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
            revINTEREST_RATE.ValidationExpression = aRegExpBaseDouble;
            revINTEREST_RATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
            csvINTEREST_RATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
            rfvINFLATION_RATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
            rfvINTEREST_RATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvDATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
            revDATE.ValidationExpression = aRegExpDate;
            csvDATE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2077");
        }

        private void getFormValues(ClsMonetaryInfo objMonetaryInfo)
        {

           
                 if (txtINFLATION_RATE.Text.Trim() != "")
                 { objMonetaryInfo.INFLATION_RATE.CurrentValue = Convert.ToDouble(txtINFLATION_RATE.Text,NfiBaseCurrency); }

                 if (txtINTEREST_RATE.Text.Trim() != "")
                 { objMonetaryInfo.INTEREST_RATE.CurrentValue = Convert.ToDouble(txtINTEREST_RATE.Text,NfiBaseCurrency); }

                if (txtDATE.Text.Trim() != "")
                { objMonetaryInfo.DATE.CurrentValue = ConvertToDate(txtDATE.Text); }


          }
         
         



    }
}
