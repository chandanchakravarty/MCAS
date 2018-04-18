
/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	01-Sep-2010
<End Date			: -	 
<Description		: - To display the Product details information ( Line of business details information) 
<Review Date		: - 
<Reviewed By		: - 	

Modification History
<Modified Date		: - 
<Modified By		: -   
<Purpose			: -  
*******************************************************************************************/
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
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Maintenance;
using System.Resources;
using System.Reflection;
using Cms.BusinessLayer.BlApplication;
using System.Collections.Generic;
using System.Globalization;
using System.IO; 

namespace Cms.CmsWeb.Maintenance
{
    public partial class ProductMasterDetails : Cms.CmsWeb.cmsbase
    {
        ResourceManager objresource;
       
        private static String strRowId = String.Empty;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "502_0";
            
            btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnReset.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnAddProcessNo.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnAddProcessNo.PermissionString = gstrSecurityXML;
    
            objresource = new System.Resources.ResourceManager("Cms.Cmsweb.Maintenance.ProductMasterDetails", System.Reflection.Assembly.GetExecutingAssembly());
            btnReset.Attributes.Add("onclick", "javascript: return ResetTheForm();");
            txtADMINISTRATIVE_EXPENSE.Attributes.Add("onBlur", "this.value=formatRateBase(this.value);");
            //txtADMINISTRATIVE_EXPENSE.Attributes.Add("onBlur", "this.value=formatRateBase(this.value),2");
            if (!IsPostBack)
            {
                this.SetCaptions();
                this.SetErrorMessages();
                hidTAB_TITLES.Value = ClsMessages.GetTabTitles("502_1", "TabCtl");// changed by praveer TFS# 736

                if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != "" && Request.QueryString["LOB_ID"].ToString() != "NEW")
                {
                    hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();
                    this.GetOldDataObject(Convert.ToInt32(hidLOB_ID.Value));
                    strRowId = hidLOB_ID.Value;
                   
                }
                else
                {
                    hidLOB_ID.Value = "NEW";
                    strRowId = "NEW";
                }
            }
            
        }
        private void BindAppCommission(ClsProductMasterInfo objProductMasterInfo)
        {

            DataTable dt = ClsGeneralInformation.GetCommissionType("COM",0) ;
            lstbAPPLICABLE_COMMISSION.DataSource = dt;
            lstbAPPLICABLE_COMMISSION.DataValueField = "TRAN_ID";
            lstbAPPLICABLE_COMMISSION.DataTextField = "DISPLAY_DESCRIPTION";
            lstbAPPLICABLE_COMMISSION.DataBind();

          
            String[] strAPPLICABLE_COMMISSION= objProductMasterInfo.APPLICABLE_COMMISSION.CurrentValue.Split(',');

            for (int Counter = 0; Counter < strAPPLICABLE_COMMISSION.Length; Counter++)
            {
                for (int CounterLstItem = 0; CounterLstItem < lstbAPPLICABLE_COMMISSION.Items.Count; CounterLstItem++)
                {
                    if (lstbAPPLICABLE_COMMISSION.Items[CounterLstItem].Value == strAPPLICABLE_COMMISSION[Counter].ToString())
                    {
                        lstbAPPLICABLE_COMMISSION.Items[CounterLstItem].Selected = true;
                        continue;
                    }

                }
            }
          
        }
        private void BindSUSEPProcessNumber(DataTable dt)
        {
            lstbSUSEP_PROCESS_NO.DataSource = dt;
            lstbSUSEP_PROCESS_NO.DataValueField = "LOB_ID";
            lstbSUSEP_PROCESS_NO.DataTextField = "SUSEP_PROCESS_NO";
            lstbSUSEP_PROCESS_NO.DataBind();
 
        }
        private void BindSUSEP_LOB()
        {
            DataTable dt = Cms.CmsWeb.ClsFetcher.Susep_lob;
            StoreSUSEP_LOB_DESC.DataSource = dt;
            StoreSUSEP_LOB_DESC.DataBind();
            CCcmbSUSEP_LOB_DESC.StoreID = "StoreSUSEP_LOB_DESC";
            CCcmbSUSEP_LOB_DESC.ValueField = "SUSEP_LOB_ID";
            CCcmbSUSEP_LOB_DESC.DisplayField = "SUSEP_LOB_DESC";
            
        }
        private void BindCommissionLevel()
        {

            StoreCOMMISSION_LEVEL.DataSource = new object[]
                 {
                    new object[]{((Cms.Model.ClsLookupInfo)( ClsCommon.GetLookup("COVTP")[2])).LookupCode, ((Cms.Model.ClsLookupInfo)(ClsCommon.GetLookup("COVTP")[2])).LookupDesc},
                    new object[] {(( Cms.Model.ClsLookupInfo)(ClsCommon.GetLookup("COVTP")[3])).LookupCode, ((Cms.Model.ClsLookupInfo)(ClsCommon.GetLookup("COVTP")[3])).LookupDesc},
                 };
             StoreCOMMISSION_LEVEL.DataBind();
             CCcmbCOMMISSION_LEVEL.StoreID = "StoreCOMMISSION_LEVEL";
             CCcmbCOMMISSION_LEVEL.ValueField = "LookupCode";
             CCcmbCOMMISSION_LEVEL.DisplayField = "LookupDesc";
        }

        private void GetOldDataObject(int LOB_ID)
        {
            ClsProductMasterInfo objProductMasterInfo = new ClsProductMasterInfo();
            ClsProducts objProduct = new ClsProducts();
            objProductMasterInfo.LOB_ID.CurrentValue = LOB_ID;
            DataTable dt = new DataTable();
            if (objProduct.FetchProductMasterInfo(ref objProductMasterInfo,ref dt))
            {
                BindSUSEPProcessNumber(dt);
                PopulatePageFromEbixModelObject(this.Page, objProductMasterInfo);
                BindSUSEP_LOB();
                BindCommissionLevel();
                CCcmbSUSEP_LOB_DESC.SelectedItem.Value =objProductMasterInfo.SUSEP_LOB_ID.CurrentValue.ToString();
                CCcmbCOMMISSION_LEVEL.SelectedItem.Value = objProductMasterInfo.COMMISSION_LEVEL.CurrentValue.ToString();
                NfiBaseCurrency.NumberDecimalDigits = 2;
                txtADMINISTRATIVE_EXPENSE.Text = objProductMasterInfo.ADMINISTRATIVE_EXPENSE.CurrentValue.ToString("N", NfiBaseCurrency);
                BindAppCommission(objProductMasterInfo);
                base.SetPageModelObject(objProductMasterInfo);
                
            }//if (objProductMasterInfo.FetchProductMasterInfo(ref objProductMasterInfo))

        }
       
        private void SetCaptions()
        {
            lblManHeader.Text = ClsMessages.FetchGeneralMessage("1168");
            capLOB_CODE.Text = objresource.GetString("txtLOB_CODE");
            capLOB_DESC.Text = objresource.GetString("txtLOB_DESC");
            capLOB_CATEGORY.Text = objresource.GetString("txtLOB_CATEGORY");
            capLOB_TYPE.Text = objresource.GetString("txtLOB_TYPE");
            capLOB_ACORD_STD.Text = objresource.GetString("txtLOB_ACORD_STD");
            capLOB_PREFIX.Text = objresource.GetString("txtLOB_PREFIX");
            capLOB_SUFFIX.Text = objresource.GetString("txtLOB_SUFFIX");
            capSUSEP_LOB_DESC.Text = objresource.GetString("txtSUSEP_LOB_DESC");
            capCOMMISSION_LEVEL.Text = objresource.GetString("cmbCOMMISSION_LEVEL");
            capAPPLICABLE_COMMISSION.Text = objresource.GetString("lstbAPPLICABLE_COMMISSION");
            capSUSEP_PROCESS_NO.Text = objresource.GetString("txtSUSEP_PROCESS_NO");
            btnAddProcessNo.Text = objresource.GetString("btnAddProcessNo"); //sneha
            capADMINISTRATIVE_EXPENSE.Text = objresource.GetString("txtADMINISTRATIVE_EXPENSE"); 

        }
        private void SetErrorMessages()
        {
            rfvLOB_DESC.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
            rfvLOB_CATEGORY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
            rfvLOB_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
            rfvLOB_PREFIX.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
            rfvLOB_SUFFIX.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
           
            //rfvCOMMISSION_LEVEL.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8");

            revLOB_DESC.ValidationExpression = aRegExpTextArea100;
            revLOB_DESC.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");
            
            revLOB_CATEGORY.ValidationExpression = aRegExpTextArea100;
            revLOB_CATEGORY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "10");

            revLOB_TYPE.ValidationExpression = aRegExpTextArea100;
            revLOB_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "11");

            revLOB_PREFIX.ValidationExpression = aRegExpTextArea100;
            revLOB_PREFIX.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "12");

            revLOB_SUFFIX.ValidationExpression = aRegExpTextArea100;
            revLOB_SUFFIX.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13");

            revADMINISTRATIVE_EXPENSE.ValidationExpression = aRegExpBaseDouble;
            revADMINISTRATIVE_EXPENSE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "18");
            csvADMINISTRATIVE_EXPENSE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "17");
        }
       
        public static Model.Support.ClsModelBaseClass PopulateModel(Model.Support.ClsModelBaseClass objModel, Dictionary<string, string> PageData)
        {
          
            ICollection ColumnsNameKey = objModel.htPropertyCollection.Keys;

           foreach (KeyValuePair<string, string> KeyAndValue in PageData)
           {
               String ColumnType = String.Empty;
               String ColumnName = KeyAndValue.Key;

               ColumnType = objModel.htPropertyCollection[ColumnName].GetType().Name.ToString();

               switch (ColumnType)
               {
                   case "EbixInt32":
                       if (KeyAndValue.Value != "")
                           ((Cms.EbixDataTypes.EbixInt32)objModel.htPropertyCollection[ColumnName]).CurrentValue = int.Parse(KeyAndValue.Value);
                       else
                           ((Cms.EbixDataTypes.EbixInt32)objModel.htPropertyCollection[ColumnName]).CurrentValue = -1;
                       break;
                   case "EbixDouble":
                       if (KeyAndValue.Value != "") 
                            ((Cms.EbixDataTypes.EbixDouble)objModel.htPropertyCollection[ColumnName]).CurrentValue = FormatDouble(KeyAndValue.Value);

                       else
                           ((Cms.EbixDataTypes.EbixDouble)objModel.htPropertyCollection[ColumnName]).CurrentValue = -1;
                       
                       break;
                   case "EbixString":
                       if (KeyAndValue.Value != null || KeyAndValue.Value!="")
                           ((Cms.EbixDataTypes.EbixString)objModel.htPropertyCollection[ColumnName]).CurrentValue = KeyAndValue.Value;
                       else
                           ((Cms.EbixDataTypes.EbixString)objModel.htPropertyCollection[ColumnName]).CurrentValue = String.Empty;
                       break;
                   case "EbixDateTime":
                       if (KeyAndValue.Value != "")
                           ((Cms.EbixDataTypes.EbixDateTime)objModel.htPropertyCollection[ColumnName]).CurrentValue = Convert.ToDateTime(KeyAndValue.Value);
                       break;
                   case "EbixBoolean":
                       if (KeyAndValue.Value != "")
                           ((Cms.EbixDataTypes.EbixBoolean)objModel.htPropertyCollection[ColumnName]).CurrentValue = Convert.ToBoolean(KeyAndValue.Value);
                       break;
                   default:
                       ((Cms.EbixDataTypes.EbixString)objModel.htPropertyCollection[ColumnName]).CurrentValue = KeyAndValue.Value;
                       break;
               }

           }
           return objModel;
        }
        private static double FormatDouble(string value)
        {
            double retValue = 0;
            NumberFormatInfo NfiBase;
            if (HttpContext.Current.Session["SYSBaseCurrency"].ToString() == enumCurrencyId.BR)
            {

                NfiBase = new CultureInfo(enumCulture.BR, true).NumberFormat;
                NfiBase.NumberDecimalDigits = 2;
                retValue = Convert.ToDouble(value, NfiBase);
            }
            else if (HttpContext.Current.Session["SYSBaseCurrency"].ToString() == enumCurrencyId.US)
            {

                NfiBase = new CultureInfo(enumCulture.US, true).NumberFormat;
                NfiBase.NumberDecimalDigits = 2;
                retValue = Convert.ToDouble(value, NfiBase);
            }

            return retValue;
        }
        //Get the page model data from session 
        private static Model.Support.IEbixModel GetEbixPageModelObject()
        {
            if (HttpContext.Current.Session["PageModelObject"] != null)
            {
                byte[] blob = (byte[])HttpContext.Current.Session["PageModelObject"];
                if (blob != null)
                {
                    MemoryStream Dms = new MemoryStream(blob);
                    Model.Support.IEbixModel objModel = (Model.Support.IEbixModel)DeSerializeBinary(Dms);
                    Dms.Flush(); Dms.Close(); Dms.Dispose();
                    return objModel;
                }

            }

            return null;
        }
        [System.Web.Services.WebMethod]
        public static String SaveData(Dictionary<string, string> ProductMasterInfo)
        {
            String retValue = String.Empty;
            ClsProducts objProduct = new ClsProducts();
            ClsProductMasterInfo objProductMasterInfo;
            try
            {
                objProductMasterInfo = (ClsProductMasterInfo)GetEbixPageModelObject();
                objProductMasterInfo.IS_ACTIVE.CurrentValue = "Y";
                objProductMasterInfo.MODIFIED_BY.CurrentValue = int.Parse(HttpContext.Current.Session["userId"].ToString());
                objProductMasterInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);

               
                PopulateModel(objProductMasterInfo, ProductMasterInfo);

                if (strRowId != "NEW")
                {
                    if (objProduct.UpdateProductInfo(objProductMasterInfo))
                    {
                        retValue = "1-" + objProductMasterInfo.LOB_ID.CurrentValue.ToString()+"-";  
                        retValue += ClsMessages.FetchGeneralMessage("31");
                    }
                    else
                    {
                        retValue = "2-" + objProductMasterInfo.LOB_ID.CurrentValue.ToString() + "-";  
                        retValue += ClsMessages.FetchGeneralMessage("20");
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception ( "Error while Update" + ex.Message );
            }
            return retValue;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (hidLOB_ID.Value == "NEW" || hidLOB_ID.Value!="")
            {
                GetOldDataObject(int.Parse(hidLOB_ID.Value.ToString()));
            }
        }
        
        
        
    }
}
