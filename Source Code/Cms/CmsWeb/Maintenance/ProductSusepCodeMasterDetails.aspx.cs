
/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	19-August-2011
<End Date			: -	 
<Description		: - To display And Add the Product Susep code details information
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
    public partial class ProductSusepCodeMasterDetails : Cms.CmsWeb.cmsbase
    {
        ResourceManager objresource;
        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnDelete;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
        public static String strRowId = String.Empty;
        ClsProducts objProduct = new ClsProducts();
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "502_2";

            btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnReset.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnDelete.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnDelete.PermissionString = gstrSecurityXML;
          

            objresource = new System.Resources.ResourceManager("Cms.Cmsweb.Maintenance.ProductSusepCodeMasterDetails", System.Reflection.Assembly.GetExecutingAssembly());
            btnReset.Attributes.Add("onclick", "javascript: return ResetTheForm();");
            hlkEFFECTIVE_FROM.Attributes.Add("OnClick", "fPopCalendar(document.getElementById('txtEFFECTIVE_FROM'), document.getElementById('txtEFFECTIVE_FROM'))");
            hlkEFFECTIVE_TO.Attributes.Add("OnClick", "fPopCalendar(document.getElementById('txtEFFECTIVE_TO'), document.getElementById('txtEFFECTIVE_TO'))");
            
            
            if (!IsPostBack)
            {
                this.SetCaptions();
                this.SetErrorMessages();

                if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != ""
                    && Request.QueryString["LOB_SUSEPCODE_ID"] != null && Request.QueryString["LOB_SUSEPCODE_ID"].ToString() != "")
                {
                    hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();
                    hidLOB_SUSEPCODE_ID.Value = Request.QueryString["LOB_SUSEPCODE_ID"].ToString();
                    this.GetOldDataObject(Convert.ToInt32(hidLOB_ID.Value),Convert.ToInt32(hidLOB_SUSEPCODE_ID.Value));
                    strRowId = hidLOB_ID.Value;
                    
                }
                else
                {
                    if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != "")
                        hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();

                    hidLOB_SUSEPCODE_ID.Value = "NEW";                
                    btnDelete.Attributes.Add("style", "display:none");//changed by praveer TFS# 736
                    
                    
                }
            }
            
        }
        private void GetOldDataObject(Int32 LOB_ID, Int32 LOB_SUSEPCODE_ID)
        {
            ClsProductMasterInfo objProductMasterInfo = new ClsProductMasterInfo();
            ClsProducts objProduct = new ClsProducts();
            objProductMasterInfo.LOB_ID.CurrentValue = LOB_ID;
            objProductMasterInfo.LOB_SUSEPCODE_ID.CurrentValue = LOB_SUSEPCODE_ID;
            DataSet ds = new DataSet();
            if (objProduct.FetchProductSUSEPCODEMasterInfo(objProductMasterInfo, ref ds))
            {
                objProductMasterInfo.LOB_ID.CurrentValue = Convert.ToInt32(ds.Tables[0].Rows[0]["LOB_ID"]);
                objProductMasterInfo.LOB_SUSEPCODE_ID.CurrentValue = Convert.ToInt32(ds.Tables[0].Rows[0]["LOB_SUSEPCODE_ID"]);
                objProductMasterInfo.EFFECTIVE_FROM.CurrentValue = Convert.ToDateTime((ds.Tables[0].Rows[0]["EFFECTIVE_FROM"]));
                objProductMasterInfo.EFFECTIVE_TO.CurrentValue = Convert.ToDateTime((ds.Tables[0].Rows[0]["EFFECTIVE_TO"]));
                objProductMasterInfo.SUSEP_LOB_CODE.CurrentValue =ds.Tables[0].Rows[0]["SUSEP_LOB_CODE"].ToString();
                
                hidLOB_ID.Value=objProductMasterInfo.LOB_ID.CurrentValue.ToString();
                hidLOB_SUSEPCODE_ID.Value=objProductMasterInfo.LOB_SUSEPCODE_ID.CurrentValue.ToString();
                txtEFFECTIVE_FROM.Text=objProductMasterInfo.EFFECTIVE_FROM.CurrentValue.ToShortDateString();
                txtEFFECTIVE_TO.Text=objProductMasterInfo.EFFECTIVE_TO.CurrentValue.ToShortDateString();
                txtSUSEP_LOB_CODE.Text = objProductMasterInfo.SUSEP_LOB_CODE.CurrentValue.ToString();

                base.SetPageModelObject(objProductMasterInfo);

            }//if (objProductMasterInfo.FetchProductMasterInfo(ref objProductMasterInfo))

        }
        private void SetErrorMessages()
        {
            rfvSUSEP_LOB_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1"); 
            rfvEFFECTIVE_FROM.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2"); 
            rfvEFFECTIVE_TO.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3"); 

            revSUSEP_LOB_CODE.ValidationExpression = aRegExpTextArea100;
            revSUSEP_LOB_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");

            revEFFECTIVE_FROM.ValidationExpression = aRegExpDate;
            revEFFECTIVE_FROM.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");

            revEFFECTIVE_TO.ValidationExpression = aRegExpDate;
            revEFFECTIVE_TO.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
           
        }
       
        
        private void SetCaptions()
        {
            lblManHeader.Text = ClsMessages.FetchGeneralMessage("1168");
            capSUSEP_LOB_CODE.Text = objresource.GetString("txtSUSEP_LOB_CODE");
            capEFFECTIVE_FROM.Text = objresource.GetString("txtEFFECTIVE_FROM");
            capEFFECTIVE_TO.Text = objresource.GetString("txtEFFECTIVE_TO");
        }//private void SetCaptions()

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
                    
                    case "EbixString":
                        if (KeyAndValue.Value != null || KeyAndValue.Value != "")
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

                if (ProductMasterInfo["IS_ACTIVE"].ToUpper().Equals("N"))
                {
                    objProductMasterInfo = new ClsProductMasterInfo();
                    objProductMasterInfo.CREATED_BY.CurrentValue = int.Parse(HttpContext.Current.Session["userId"].ToString());
                    objProductMasterInfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    
                    PopulateModel(objProductMasterInfo, ProductMasterInfo);

                    if (objProduct.InsertProductSUSEPCODEInfo(objProductMasterInfo))
                    {
                        retValue = "1-" + objProductMasterInfo.LOB_ID.CurrentValue.ToString() + "-" + objProductMasterInfo.LOB_SUSEPCODE_ID.CurrentValue.ToString() + "-";
                        retValue += ClsMessages.FetchGeneralMessage("29");
                    }
                    else
                    {
                        retValue = "2-" + objProductMasterInfo.LOB_ID.CurrentValue.ToString() + "-" + objProductMasterInfo.LOB_SUSEPCODE_ID.CurrentValue.ToString() + "-";
                        retValue += ClsMessages.FetchGeneralMessage("20");
                    }
                  
                } // if (strRowId.ToUpper().Equals("NEW"))
                else
                {
                    //-------------
                    objProductMasterInfo = (ClsProductMasterInfo)GetEbixPageModelObject();
                    objProductMasterInfo.IS_ACTIVE.CurrentValue = "Y";
                    objProductMasterInfo.MODIFIED_BY.CurrentValue = int.Parse(HttpContext.Current.Session["userId"].ToString());
                    objProductMasterInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);


                    PopulateModel(objProductMasterInfo, ProductMasterInfo);

                    if (objProduct.UpdateProductSUSEPCODEInfo(objProductMasterInfo))
                    {
                        retValue = "1-" + objProductMasterInfo.LOB_ID.CurrentValue.ToString() + "-" + objProductMasterInfo.LOB_SUSEPCODE_ID.CurrentValue.ToString() + "-";
                        retValue += ClsMessages.FetchGeneralMessage("31");
                    }
                    else
                    {
                        retValue = "2-" + objProductMasterInfo.LOB_ID.CurrentValue.ToString() + "-" + objProductMasterInfo.LOB_SUSEPCODE_ID.CurrentValue.ToString() + "-";
                        retValue += ClsMessages.FetchGeneralMessage("20");
                    }
                     
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while Update" + ex.Message);
            }
            return retValue;
        }
        [System.Web.Services.WebMethod]
        public static String DeleteData(Dictionary<string, string> ProductMasterInfo)
        {
            String retValue = String.Empty;
            ClsProducts objProduct = new ClsProducts();
            ClsProductMasterInfo objProductMasterInfo;
            try
            {
                objProductMasterInfo = (ClsProductMasterInfo)GetEbixPageModelObject();
                objProductMasterInfo.LOB_ID.CurrentValue = Convert.ToInt32(ProductMasterInfo["LOB_ID"]);
                objProductMasterInfo.LOB_SUSEPCODE_ID.CurrentValue = Convert.ToInt32(ProductMasterInfo["LOB_SUSEPCODE_ID"]);
                objProductMasterInfo.MODIFIED_BY.CurrentValue = int.Parse(HttpContext.Current.Session["userId"].ToString());
                objProductMasterInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);


                if (objProduct.DeleteProductSUSEPCODEInfo(objProductMasterInfo))
                {
                    retValue = "Delete-1-" + objProductMasterInfo.LOB_ID.CurrentValue.ToString() + "-" + "0" + "-";
                    retValue += Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error while Delete" + ex.Message);

            }
            return retValue;
        }
    }
}