using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlCommon;
using Cms.Model;
using Cms.Model.Maintenance;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using Cms.CmsWeb.Controls;

namespace Cms.CmsWeb.Maintenance
{
    public partial class AddRetentionLimitDetail : Cms.CmsWeb.cmsbase
    {
        ClsRetentionLimitInfo objRetentionLimitInfo = new ClsRetentionLimitInfo();
        ClsRetentionLimit objRetentionLimit = new ClsRetentionLimit();
        System.Resources.ResourceManager objResourceMgr;
        string strRowId = "";
        
        public static int flag = 0;    
        protected void Page_Load(object sender, EventArgs e)
        {

            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddRetentionLimitDetail", System.Reflection.Assembly.GetExecutingAssembly());

            base.ScreenId = "558";
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;
            btnDelete.CmsButtonClass = CmsButtonType.Write;
            btnDelete.PermissionString = gstrSecurityXML;
            if (!IsPostBack)
            {               
                SetErrorMessages();
                //SetCaptions();
                string strSysID = GetSystemId();
                if (strSysID == "ALBAUAT")
                    strSysID = "ALBA";
                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "cmsweb/support/PageXml/" + strSysID, "RetentionLimitDetail.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "cmsweb/support/PageXml/" + strSysID + "/RetentionLimitDetail.xml");
                PopulateSusepLOBID();
                txtRETENTION_LIMIT.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value,2);"); //Changed by Aditya for tfs bug # 404
                if (Request.QueryString["RETENTION_LIMIT_ID"] != null && Request.QueryString["RETENTION_LIMIT_ID"].ToString() != "")
                {
                   
                     hidRETENTION_LIMIT_ID.Value = Request.QueryString["RETENTION_LIMIT_ID"].ToString();
                    this.GetOldDataObject(Convert.ToInt32(Request.QueryString["RETENTION_LIMIT_ID"].ToString()));
                    btnDelete.Visible = true;
                }
                 else if (Request.QueryString["RETENTION_LIMIT_ID"] == null)
                    {
                        hidRETENTION_LIMIT_ID.Value = "NEW";
                        btnDelete.Visible = false;
                    }

            }
            strRowId = hidRETENTION_LIMIT_ID.Value;


        }

        private void PopulateSusepLOBID() //Changed by Aditya for TFS BUG # 404
        {

            string LANG_ID = GetLanguageID();
            DataTable dt = ClsEndorsmentDetails.GetSUSEPLOBs(LANG_ID).Tables[0];
            cmbREF_SUSEP_LOB_ID.DataSource = dt;
            cmbREF_SUSEP_LOB_ID.DataTextField = "SUSEP_LOB_DESC";
            cmbREF_SUSEP_LOB_ID.DataValueField = "SUSEP_LOB_ID";
            cmbREF_SUSEP_LOB_ID.DataBind();
            cmbREF_SUSEP_LOB_ID.Items.Insert(0, new ListItem("", ""));   
        }        

        private void GetOldDataObject(Int32 RETENTION_LIMIT_ID)
        {
            try
            {

                ClsRetentionLimitInfo ObjRetentionLimitInfo = new ClsRetentionLimitInfo();
                ClsRetentionLimit ObjRetentionLimit = new ClsRetentionLimit();
                NfiBaseCurrency.NumberDecimalDigits = 2;
               // DataSet DsRetentionLimit
                ObjRetentionLimitInfo = ObjRetentionLimit.GetRetentionLimit(RETENTION_LIMIT_ID);
                ObjRetentionLimitInfo.IS_ACTIVE.CurrentValue = "Y";
                PopulatePageFromEbixModelObject(this.Page, ObjRetentionLimitInfo);
                cmbREF_SUSEP_LOB_ID.SelectedValue = ObjRetentionLimitInfo.REF_SUSEP_LOB_ID.CurrentValue.ToString();
                txtRETENTION_LIMIT.Text = ObjRetentionLimitInfo.RETENTION_LIMIT.CurrentValue.ToString("N", NfiBaseCurrency);
                
                
                //txtRETENTION_LIMIT.Text = ObjRetentionLimitInfo.RETENTION_LIMIT.CurrentValue.ToString("N", NfiBaseCurrency);
               base.SetPageModelObject(ObjRetentionLimitInfo);

              
            }


            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

                return;
            }

        }
        private void GetFormValue(ClsRetentionLimitInfo objRetentionLimitInfo)
        {
            NfiBaseCurrency.NumberDecimalDigits = 2;
            if (cmbREF_SUSEP_LOB_ID.SelectedValue != "" && cmbREF_SUSEP_LOB_ID.SelectedValue != null)
            {
                objRetentionLimitInfo.REF_SUSEP_LOB_ID.CurrentValue = int.Parse(cmbREF_SUSEP_LOB_ID.SelectedValue);
            }

            objRetentionLimitInfo.RETENTION_LIMIT.CurrentValue = Convert.ToDouble(txtRETENTION_LIMIT.Text, NfiBaseCurrency);
        
        }
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {           
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
       

        }
        #endregion


        private void btnSave_Click(object sender, System.EventArgs e)
        {
            
            try
            {
                btnDelete.Visible = true;
                int intRetVal;	//For retreiving the return value of business class save function              

                //Retreiving the form values into model class object              

                if (strRowId.ToUpper().Equals("NEW")) //save case              
                {
                    this.GetFormValue(objRetentionLimitInfo);                   

                    //Calling the add method of business layer class 
                    intRetVal = objRetentionLimit.Add(objRetentionLimitInfo);

                    if (intRetVal > 0)
                    {
                        this.GetOldDataObject(objRetentionLimitInfo.RETENTION_LIMIT_ID.CurrentValue);
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        hidFormSaved.Value = "1";
                        hidRETENTION_LIMIT_ID.Value = objRetentionLimitInfo.RETENTION_LIMIT_ID.CurrentValue.ToString();
                    }                

                   
                    else
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;
                } // end save case
                else //UPDATE CASE
                {


                    objRetentionLimitInfo = (ClsRetentionLimitInfo)base.GetPageModelObject();

                    this.GetFormValue(objRetentionLimitInfo);
                    objRetentionLimitInfo.RETENTION_LIMIT_ID.CurrentValue = int.Parse(hidRETENTION_LIMIT_ID.Value);



                    int intRetval = objRetentionLimit.Update(objRetentionLimitInfo); 

                    if (intRetval > 0)
                    {
                        int Retentionlimit = int.Parse(hidRETENTION_LIMIT_ID.Value);
                        this.GetOldDataObject(Retentionlimit);
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                        hidFormSaved.Value = "1";

                    }

                  
                 
                    else
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
                        hidFormSaved.Value = "2";
                    }

                    lblMessage.Visible = true;
                }


            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.FetchGeneralMessage("21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
            finally
            {
                
            }
          

        }


        private void btnDelete_Click(object sender, EventArgs e)
        {

            try
            {
                

                objRetentionLimitInfo.RETENTION_LIMIT_ID.CurrentValue = int.Parse(hidRETENTION_LIMIT_ID.Value);


                if (cmbREF_SUSEP_LOB_ID.SelectedValue == flag.ToString())
                {

                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "11");
                    lblMessage.Visible = true;
                    return;
                }

                int intRetval = objRetentionLimit.Delete(objRetentionLimitInfo);
                if (intRetval >= 0)
                {
                    lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                    hidFormSaved.Value = "1";
                    trBody.Attributes.Add("style", "display:none");
                }
                else
                {
                    lblDelete.Text = ClsMessages.GetMessage("224_52", "128");
                    hidFormSaved.Value = "2";
                }
                lblDelete.Visible = true;
                lblMessage.Visible = false;


            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.FetchGeneralMessage("21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";
            }
        }

        private void SetErrorMessages()
        {
            try
            {
                csvRETENTION_LIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
                revRETENTION_LIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
                rfvREF_SUSEP_LOB_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
                rfvRETENTION_LIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
              
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }

        }

        private void SetCaptions()
        {
           
            //lblheader_field.Text = objResourceMgr.GetString("lblheader_field");
           
          
        }


    }  

}