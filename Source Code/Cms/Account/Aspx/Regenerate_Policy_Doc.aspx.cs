using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.CmsWeb;
using System.Resources;
using System.Reflection;
using System.Collections;
using System.Drawing;
using System.Xml;
using System.Text;
using BlGeneratePdf;
using Cms.BusinessLayer.BlAccount;
using Cms.BusinessLayer.BlProcess;
using Cms.BusinessLayer.BLClaims;
using Cms.BusinessLayer.BlBoleto;


namespace Cms.Account.Aspx
{
    public partial class Regenerate_Policy_Doc : Cms.Account.AccountBase
    {
        System.Resources.ResourceManager objResourceMgr;
        private const string COINSURANCE_FILLOWER = "14549";
        private const string COINSURANCE_DIRECT = "14547";
        private XmlDocument PDFXMLDoc;
        //ResourceManager Objresources;
        DataSet ds = new DataSet();
        string POLICY_NUMBER = "";
        ClsAccount objAccount = new ClsAccount();
        protected void Page_Load(object sender, EventArgs e)
        {
            SetCultureThread(GetLanguageCode());
            objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.Regenerate_Policy_Doc", System.Reflection.Assembly.GetExecutingAssembly());
            POLICY_NUMBER = txtPOLICY_NUMBER.Text.ToString();
            setErrorMessages();
            lblMsg.Visible = false;
          
            if (!(Page.IsPostBack))
            {
                SetCaptions();
                btnProcess.Enabled = false;

                string Lang =GetLanguageID();

                if (Lang == "1")
                {
                    CmbACTION_TYPE.Items.Add(new ListItem("Policy", "1"));
                    CmbACTION_TYPE.Items.Add(new ListItem("Claim", "2"));
                    CmbACTION_TYPE.Items.Add(new ListItem("Boleto", "3"));//iTrack 1383,1361
                }
                else
                {
                    CmbACTION_TYPE.Items.Add(new ListItem("Apólice", "1"));
                    CmbACTION_TYPE.Items.Add(new ListItem("Sinistro", "2"));
                    CmbACTION_TYPE.Items.Add(new ListItem("Boleto", "3"));//iTrack 1383,1361
                }

            }          
        }

        private void SetCaptions()
        {
            capheaders.Text = objResourceMgr.GetString("capheaders");
            capPOLICY_NUMBER.Text = objResourceMgr.GetString("capPOLICY_NUMBER");
            capDISPLAY_VER_NO.Text = objResourceMgr.GetString("capDISPLAY_VER_NO");
            btnClick.Text = objResourceMgr.GetString("btnClick");
            btnProcess.Text = objResourceMgr.GetString("btnProcess");
            capACTION_TYPE.Text = objResourceMgr.GetString("capACTION_TYPE");
        }


        protected void btnClick_Click(object sender, EventArgs e)
         {
             btnProcess.Enabled = false;
             lblMsg.Visible = false;
             CmbDISPLAY_VERSION_NO.Items.Clear();

             if (CmbACTION_TYPE.SelectedValue == "1" || CmbACTION_TYPE.SelectedValue == "3") // FOR POLICY or Boleto
             {
                 ds = objAccount.GetRecordData(POLICY_NUMBER);
                 if (ViewState["Data"] != null)
                 {
                     ViewState["Data"] = null;
                     ViewState["Data"] = ds;
                 }
                 else
                     ViewState["Data"] = ds;
                 
                 if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count>0)
                 {
                     CmbDISPLAY_VERSION_NO.DataSource = ds;
                     CmbDISPLAY_VERSION_NO.DataValueField = "POLICY_DISP_VERSION";
                     CmbDISPLAY_VERSION_NO.DataTextField = "POLICY_DISP_VERSION_AGG";
                     CmbDISPLAY_VERSION_NO.DataBind();
                     btnProcess.Enabled = true;
                     
                     //CmbDISPLAY_VERSION_NO.Items.Insert(0, "");
                     lblMsg.Text = "";
                 }
                 
                 if (ds.Tables[0].Rows.Count < 1 || ds == null || ds.Tables.Count < 1)
                 {
                     lblMsg.Visible = true;
                     lblMsg.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1996");
                     return;
                 }
                
             }
             else  // FOR CLAIM
             {
                 int lang_id = int.Parse(GetLanguageID());

                 hidCLAIM_NUMBER.Value=txtPOLICY_NUMBER.Text.Trim();
                 ClsActivity objActivity = new ClsActivity();
                 DataSet ds= objActivity.GetClaimActivityList(hidCLAIM_NUMBER.Value, lang_id);

                 if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                 {
                     if (ds.Tables[1].Rows.Count > 0)
                     {
                         hidCLAIM_ID.Value          = ds.Tables[0].Rows[0]["CLAIM_ID"].ToString();
                         hidCUSTOMER_ID.Value       = ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString();
                         hidPOLICY_ID.Value         = ds.Tables[0].Rows[0]["POLICY_ID"].ToString();
                         hidPOLICY_VERSION_ID.Value = ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();

                         CmbDISPLAY_VERSION_NO.DataSource = ds.Tables[1];
                         CmbDISPLAY_VERSION_NO.DataValueField = "ACTIVITY_ID";
                         CmbDISPLAY_VERSION_NO.DataTextField = "ACTIVITY_TEXT";
                         CmbDISPLAY_VERSION_NO.DataBind();
                         btnProcess.Enabled = true;
                     }
                     else
                     {
                         lblMsg.Visible = true;
                         lblMsg.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1992");
                        // lblMsg.Text = "Claim does not have payment/recovery activities.";
                     }

                 }
                 else
                 {
                     lblMsg.Visible = true;
                     lblMsg.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1993");
                     //lblMsg.Text = "Invalid claim number.";
                 }

             }
        }



        public string generateDocuments(int CustomerID, int PolicyID, int PolicyVersionID, string CalledFor, int userID, int ProcessId, int ProcessRowId)
        {
            // fetching Policy details
            try
            {
                string Co_Insurance = "";//, Prem_NoticeFileName = "", AgencyCode = "", CarrierCode = "";

                DataSet dsPolicy = objAccount.fetchPolicyDataforPdfXml(CustomerID, PolicyID, PolicyVersionID);

                //generating XML from Policy Details for PDF
                String strPolicyPdfXml = GenerateDocumentdataXML(dsPolicy);//dsPolicy.GetXml();

                if (Co_Insurance != COINSURANCE_FILLOWER)
                    if (CalledFor == "PROCESS")
                    {
                        // generate Boleto if payer is Insured - 14542 lookup id for Insured
                        PDFXMLDoc = new XmlDocument();
                        PDFXMLDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + strPolicyPdfXml);
                        XmlNode Node = PDFXMLDoc.SelectSingleNode("POLICY_DOCUMENTS/APPLICATION");
                        string Payer = getNodeValue(Node, "PAYOR");

                        PDFXMLDoc = null;
                    }
                string fileName = "";
                //save PDF XML In DB
                if (strPolicyPdfXml != "" && strPolicyPdfXml != "<POLICY_DOCUMENTS></POLICY_DOCUMENTS>")
                {
                    objAccount.SavePolicyDocumentXml(CustomerID, PolicyID, PolicyVersionID, strPolicyPdfXml, CalledFor,0,0);


                }

                return fileName;
            }
            catch (Exception ex)
            {
                System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while generating Policy Documents XML and adding in Print job.");
                addInfo.Add("CustomerID", CustomerID.ToString());
                addInfo.Add("PolicyID", PolicyID.ToString());
                addInfo.Add("PolicyVersionID", PolicyVersionID.ToString());
                addInfo.Add("ProcessRowID", ProcessId.ToString());
                addInfo.Add("DOCUMENT_CODE", "PolicyDoc");
                throw (ex);
            }
        }


        private string GenerateDocumentdataXML(DataSet ds)
        {
            string PdfDataxml = "";
            string tableXML = "";
            StringBuilder strBuilder = new StringBuilder();
            try
            {
                strBuilder.Append("<POLICY_DOCUMENTS>");
                if (ds != null)
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        tableXML = "";
                        foreach (DataRow dr in dt.Rows)
                        {
                            tableXML += dr.ItemArray[0].ToString();

                        }
                        strBuilder.Append(tableXML);
                    }
                }
                strBuilder.Append("</POLICY_DOCUMENTS>");
            }
            catch (Exception ex) { throw new Exception("No data Found to generate pdf XML : " + ex.Message); }
            PdfDataxml = strBuilder.ToString();
            return PdfDataxml;
        }

        private string getNodeValue(System.Xml.XmlNode node, string strNodeName)
        {
            XmlNode xmlnode = node.SelectSingleNode(strNodeName);
            if (xmlnode != null)
                return xmlnode.InnerText;
            else
                return "";
        }
       

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmbACTION_TYPE.SelectedValue == "1" || CmbACTION_TYPE.SelectedValue == "3") // FOR POLICY or Boleto
                {
                    string Policy_display_version = string.Empty;
                    Policy_display_version = CmbDISPLAY_VERSION_NO.SelectedItem.Value;
                    //To check the lenght of selected datatable based on the policy Version -Added by Pradeep on 08-08-2011 - for itrack - 1463
                    if (((DataSet)ViewState["Data"]).Tables[0].Select("POLICY_DISP_VERSION=" + Policy_display_version.Trim()).Length > 0)
                    {
                        DataRow[] row = ((DataSet)ViewState["Data"]).Tables[0].Select("POLICY_DISP_VERSION=" + Policy_display_version.Trim());
                        int CUSTOMER_ID = Convert.ToInt32(row[0].ItemArray[1]);
                        int POLICY_ID = Convert.ToInt32(row[0].ItemArray[2]);
                        int POLICY_VERSION_ID = Convert.ToInt32(row[0].ItemArray[3]);

                        if (CmbACTION_TYPE.SelectedValue == "3")//For Boleto to regenerate iTrack 1383,1361
                        {
                            //Update Print jobs table to set IS_PROCESSED=0, ATTEMPTS=0 for boleto 
                            objAccount.SavePolicyDocumentXml(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, "", "BOLETO", 0, 0);

                            ClsBoleto objBoleto = new ClsBoleto();
                            //Regenerate the boleto html and update the regenerated boleto
                            objBoleto.RegenerateBoleto(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, int.Parse(GetUserId()));

                        }//if (CmbACTION_TYPE.SelectedValue == "3")
                        else//for policy
                        {
                            generateDocuments(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, "PROCESS", 1, 1, 1);
                        }
                        lblMsg.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("3");
                        lblMsg.Visible = true;
                    }//if (((DataSet)ViewState["Data"]).Tables[0].Select("POLICY_DISP_VERSION=" + Policy_display_version.Trim()).Length > 0)
                }
                else
                {

                    if (CmbDISPLAY_VERSION_NO.SelectedValue != "")
                    {

                        ClsProductPdfXml ObjPdfXML = new ClsProductPdfXml();
                        int PolicyID = int.Parse(hidPOLICY_ID.Value);
                        int PolicyVersionID = int.Parse(hidPOLICY_VERSION_ID.Value);
                        int CustomerID = int.Parse(hidCUSTOMER_ID.Value);
                        int UserID = int.Parse(GetUserId());
                        int ClaimID = int.Parse(hidCLAIM_ID.Value);
                        int ActivityID = int.Parse(CmbDISPLAY_VERSION_NO.SelectedValue);
                        ObjPdfXML.generateProductClaimReceipt(ClaimID, ActivityID, CustomerID, PolicyID, PolicyVersionID, UserID, "Y");

                        // UPDATE PRINT JOB 
                        objAccount.SavePolicyDocumentXml(CustomerID, PolicyID, PolicyVersionID, "", "", ClaimID, ActivityID);


                        lblMsg.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("3");
                        lblMsg.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ClsMessages.GetMessage("G", "20") + " - " + ex.Message;
                lblMsg.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                 
            }
        }

        protected void CmbDISPLAY_VERSION_NO_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg.Text = "";
        }
        private void setErrorMessages()
        {
            if (CmbACTION_TYPE.SelectedValue == "1") // FOR POLICY
            {
                rfvProcess.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1990");
            }
            if (CmbACTION_TYPE.SelectedValue == "2") // FOR POLICY
            {
                rfvProcess.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1994");
            }
            //        modified by naveen , itrack 631
            if (CmbACTION_TYPE.SelectedValue == "3") // FOR Boleto
            {
                rfvProcess.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1990");
            }
            csvProcess.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1991");
           
        }

        protected void CmbACTION_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPOLICY_NUMBER.Text = "";
            CmbDISPLAY_VERSION_NO.Items.Clear();
            if (CmbACTION_TYPE.SelectedValue =="1") // FOR POLICY
            {
                capPOLICY_NUMBER.Text = objResourceMgr.GetString("capPOLICY_NUMBER");
                capDISPLAY_VER_NO.Text = objResourceMgr.GetString("capDISPLAY_VER_NO");

            }
            if (CmbACTION_TYPE.SelectedValue == "2") // FOR CLAIM
            {
                capPOLICY_NUMBER.Text = objResourceMgr.GetString("capCLAIM");
                capDISPLAY_VER_NO.Text = objResourceMgr.GetString("capACTIVITY");
            }
            
        }

    }
}
