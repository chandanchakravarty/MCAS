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
using System.IO;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlProcess;
using Cms.BusinessLayer.Blapplication;

namespace Cms.Application.Aspx
{
    public partial class PolicyDecPage : Cms.Application.appbase
    {
        DataTable objProcessTable;
        DataTable objPolicyStatusTable;
        string strAppStatus = "";
        string strPolicyStatus = "";
        //string stateCode = "";
        string gstrLOBCODE = "";
        //string strCheck = "CHK";
        string CHECK_ID;
        string strCustomerId, strAppId = "", strAppVersionId = "", strVehicleId = "";
        string strCalledFrom = "";
        string strCalledForPDF = "DECPAGE";//"ACORD"/"DECPAGE"
        string strCalledForPrtChk = "";
        //string strPolicyProcessID = "";
        string getRequeststring, getRequestPrintChk;
        string FilePath = "";
        String Path = System.Configuration.ConfigurationSettings.AppSettings["UploadURL"].ToString();
        //String fileName = "";
        String ShowFile = "";
        
        
        string strProcessStatus = "";
      //  int intProcessID = 0;
      //  string strRevrtVer = "";

        protected void Page_Load(object sender, EventArgs e) 
          { 
            String AgencyCode = "";
            //String Generatedxml = "";   
            string fileName="";
            GetQueryStringValues();
            String Customerid = GetCustomerID();
            String PolicyID = GetPolicyID();
            String PolicyVersionID = GetPolicyVersionID();
            String strCarrierSystemID = CarrierSystemID;
            //Check xml is generated or not
             string PdfxmlString = "";
             //For policy
             if (strAppId == "")
                 strAppId = GetPolicyID();
             if (strAppVersionId == "")
                 strAppVersionId = GetPolicyVersionID();
             ClsProductPdfXml objClsProductPdfXml = new ClsProductPdfXml();
             PdfxmlString = objClsProductPdfXml.GetXmlString(int.Parse(Customerid), int.Parse(PolicyID), int.Parse(PolicyVersionID));
               Cms.BusinessLayer.BlCommon.ClsCommon objCommon=new Cms.BusinessLayer.BlCommon.ClsCommon();
               objPolicyStatusTable = objCommon.GetPolicyStatusInfo(int.Parse(strCustomerId), int.Parse(strAppId), int.Parse(strAppVersionId));
               if (objPolicyStatusTable != null && objPolicyStatusTable.Rows.Count > 0)
                 {
                     strPolicyStatus = objPolicyStatusTable.Rows[0]["POLICY_STATUS"].ToString();
                     strAppStatus    = objPolicyStatusTable.Rows[0]["APP_STATUS"].ToString();
                 }
               objProcessTable = objCommon.GetPolicyProcessInfo(int.Parse(strCustomerId), int.Parse(strAppId), int.Parse(strAppVersionId));
               if (objProcessTable != null && objProcessTable.Rows.Count > 0)
               {
                   //intProcessID = int.Parse(objProcessTable.Rows[0]["PROCESS_ID"].ToString());
                   strProcessStatus = objProcessTable.Rows[0]["PROCESS_STATUS"].ToString();
                  // strRevrtVer = objProcessTable.Rows[0]["LAST_REVERT_BACK"].ToString();
               }
               //Case 1 
               //if xml is already generated then Get Generated XML and show it on pdf      
               if (PdfxmlString != "")
               {
                   AgencyCode = ClsCommon.FetchValueFromXML("AGENCY_CODE", objClsProductPdfXml.GetXmlString(int.Parse(Customerid), int.Parse(PolicyID), int.Parse(PolicyVersionID)));
                   FilePath = System.Web.HttpContext.Current.Server.MapPath(Path) + "\\" + "OutPutPdfs" + "\\"+ strCarrierSystemID +"\\" + AgencyCode + "\\" + Customerid + "\\" + "POLICY" + "\\" + "final" + "\\";
                   string strnameBegin = "Policy_Doc_" + Customerid.ToString() + "_" + PolicyID.ToString() + "_"
                          + PolicyVersionID.ToString() + "_";
                   ShowFile = GetExistingPdf(FilePath, strnameBegin);
                   
                   if (strProcessStatus != "")
                   {
                       if (ShowFile != "")//pdf is already generated in final folder then Show Generated pdf
                       {
                           ShowFile = FilePath + ShowFile;
                           ShowPdf(ShowFile);
                           return;
                       }
                       else
                       { 
                        objClsProductPdfXml.Saveinfinal = "final";
                       fileName = objClsProductPdfXml.generateDocuments(int.Parse(Customerid), int.Parse(PolicyID), int.Parse(PolicyVersionID), "POLICY", int.Parse(GetUserId()));
                       AgencyCode = ClsCommon.FetchValueFromXML("AGENCY_CODE", objClsProductPdfXml.GetXmlString(int.Parse(Customerid), int.Parse(PolicyID), int.Parse(PolicyVersionID)));
                       fileName = objClsProductPdfXml.GeneratePolicyDocumentpdfFromXML(int.Parse(Customerid), int.Parse(PolicyID), int.Parse(PolicyVersionID), AgencyCode);
                       FilePath = System.Web.HttpContext.Current.Server.MapPath(Path) + "\\" + "OutPutPdfs" + "\\" + strCarrierSystemID + "\\" + AgencyCode + "\\" + Customerid + "\\" + "POLICY" + "\\" + "final" + "\\";
                       ShowFile = FilePath + fileName;
                       
                       }
                      
                   }
                   //Policy status is blank and application status is application/Complete
                   if ((strAppStatus.ToUpper().ToString() != ""))
                   {
                       objClsProductPdfXml.Saveinfinal = "";
                       fileName = objClsProductPdfXml.generateDocuments(int.Parse(Customerid), int.Parse(PolicyID), int.Parse(PolicyVersionID), "POLICY", int.Parse(GetUserId()));
                       AgencyCode = ClsCommon.FetchValueFromXML("AGENCY_CODE", objClsProductPdfXml.GetXmlString(int.Parse(Customerid), int.Parse(PolicyID), int.Parse(PolicyVersionID)));
                       fileName = objClsProductPdfXml.GeneratePolicyDocumentpdfFromXML(int.Parse(Customerid), int.Parse(PolicyID), int.Parse(PolicyVersionID), AgencyCode);
                       FilePath = System.Web.HttpContext.Current.Server.MapPath(Path) + "\\" + "OutPutPdfs" + "\\" + strCarrierSystemID+"\\" +  AgencyCode + "\\" + Customerid + "\\" + "POLICY" + "\\" + "final" + "\\";
                       ShowFile = System.Web.HttpContext.Current.Server.MapPath(Path) + "\\" + "OutPutPdfs" + "\\" + strCarrierSystemID + "\\" + AgencyCode + "\\" + Customerid + "\\" + "POLICY" + "\\" + "final" + "\\" + "temp" + "\\" + fileName;
                   }
                   ////Policy status is blank and application status is application
                   //  if ((strPolicyStatus == "") && (strAppStatus.ToUpper().ToString() == "APPLICATION"))
                   //  {
                   //      objClsProductPdfXml.Saveinfinal = "";
                   //      fileName = objClsProductPdfXml.generateDocuments(int.Parse(Customerid), int.Parse(PolicyID), int.Parse(PolicyVersionID), "POLICY", int.Parse(GetUserId()));
                   //      AgencyCode = ClsCommon.FetchValueFromXML("AGENCY_CODE", objClsProductPdfXml.GetXmlString(int.Parse(Customerid), int.Parse(PolicyID), int.Parse(PolicyVersionID)));
                   //      fileName = objClsProductPdfXml.GeneratePolicyDocumentpdfFromXML(int.Parse(Customerid), int.Parse(PolicyID), int.Parse(PolicyVersionID), AgencyCode);
                   //      FilePath = System.Web.HttpContext.Current.Server.MapPath(Path) + "\\" + "OutPutPdfs" + "\\" + AgencyCode + "\\" + Customerid + "\\" + "POLICY" + "\\" + "final" + "\\";
                   //      ShowFile = System.Web.HttpContext.Current.Server.MapPath(Path) + "\\" + "OutPutPdfs" + "\\" + AgencyCode + "\\" + Customerid + "\\" + "POLICY" + "\\" + "final" + "\\" + "temp" + "\\" + fileName;
                   //  }
                     
                   ////if pdf is not generated then generate it and save it in final folder
                   //  else if ((strPolicyStatus != "") && (strAppStatus.ToUpper().ToString() == "COMPLETE"))
                   //  {
                   //      objClsProductPdfXml.Saveinfinal = "";
                   //      AgencyCode = ClsCommon.FetchValueFromXML("AGENCY_CODE", objClsProductPdfXml.GetXmlString(int.Parse(Customerid), int.Parse(PolicyID), int.Parse(PolicyVersionID)));
                   //      fileName = objClsProductPdfXml.GeneratePolicyDocumentpdfFromXML(int.Parse(Customerid), int.Parse(PolicyID), int.Parse(PolicyVersionID), AgencyCode);
                   //      FilePath = System.Web.HttpContext.Current.Server.MapPath(Path) + "\\" + "OutPutPdfs" + "\\" + AgencyCode + "\\" + Customerid + "\\" + "POLICY" + "\\" + "final" + "\\";
                   //      ShowFile = System.Web.HttpContext.Current.Server.MapPath(Path) + "\\" + "OutPutPdfs" + "\\" + AgencyCode + "\\" + Customerid + "\\" + "POLICY" + "\\" + "final" + "\\" + "temp" + "\\" + fileName;
                   //      //ShowFile = System.Web.HttpContext.Current.Server.MapPath(Path) + "\\" + "OutPutPdfs" + "\\" + AgencyCode + "\\" + Customerid + "\\" + "POLICY" + "\\" + "final" + "\\" + fileName;
                   //  } 
               }
                //Case 2 
                //if xml is not already generated then Generate XML and pdf, save pdf in temp folder and show 
                 else if((PdfxmlString == "") && ((strPolicyStatus != "")||(strAppStatus.ToUpper().ToString()=="APPLICATION")))
                 {
                     objClsProductPdfXml.Saveinfinal = "";
                     fileName = objClsProductPdfXml.generateDocuments(int.Parse(Customerid), int.Parse(PolicyID), int.Parse(PolicyVersionID), "POLICY", int.Parse(GetUserId()));
                     AgencyCode = ClsCommon.FetchValueFromXML("AGENCY_CODE", objClsProductPdfXml.GetXmlString(int.Parse(Customerid), int.Parse(PolicyID), int.Parse(PolicyVersionID)));
                     fileName = objClsProductPdfXml.GeneratePolicyDocumentpdfFromXML(int.Parse(Customerid), int.Parse(PolicyID), int.Parse(PolicyVersionID), AgencyCode);
                     FilePath = System.Web.HttpContext.Current.Server.MapPath(Path) + "\\" + "OutPutPdfs" + "\\" + strCarrierSystemID + "\\" + AgencyCode + "\\" + Customerid + "\\" + "POLICY" + "\\" + "final" + "\\";
                      ShowFile = System.Web.HttpContext.Current.Server.MapPath(Path) + "\\" + "OutPutPdfs" + "\\" + strCarrierSystemID + "\\" + AgencyCode + "\\" + Customerid + "\\" + "POLICY" + "\\" + "final" + "\\" + "temp" + "\\" + fileName;
                 } 

            ShowPdf(ShowFile);


        }


        public void GetQueryStringValues()
        {
            string strLOB_ID = "";
            strCustomerId = GetCustomerID();
            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"] != "")
            {
                strCalledFrom = Request.QueryString["CalledFrom"].ToString().ToUpper();

                if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"] != "")
                    strCustomerId = Request.QueryString["CUSTOMER_ID"].ToString();

                if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"] != "")
                    strAppId = Request.QueryString["POLICY_ID"].ToString();

                if (Request.QueryString["POLICY_VER_ID"] != null && Request.QueryString["POLICY_VER_ID"] != "")
                    strAppVersionId = Request.QueryString["POLICY_VER_ID"].ToString();

                if (Request.QueryString["CHECK_ID"] != null && Request.QueryString["CHECK_ID"] != "")
                    CHECK_ID = Request.QueryString["CHECK_ID"].ToString();
                if (Request.QueryString["VEHICLE_ID"] != null && Request.QueryString["VEHICLE_ID"] != "")
                    strVehicleId = Request.QueryString["VEHICLE_ID"].ToString();
            }
            if (Request.QueryString["CALLEDFOR"] != null && Request.QueryString["CALLEDFOR"] != "")
            {
                strCalledForPDF = Request.QueryString["CALLEDFOR"].ToUpper();
            }

            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"].ToString() != "")
            {
                getRequeststring = Request.QueryString["CalledFrom"];
                getRequestPrintChk = Request.QueryString["CalledFrom"];
            }
            else
            {
                getRequeststring = "";
                getRequestPrintChk = "";

            }

            if (Request.QueryString["CALLEDFORPRINT"] != null && Request.QueryString["CALLEDFORPRINT"] != "")
            {
                strCalledForPrtChk = Request.QueryString["CALLEDFORPRINT"].ToUpper();
            }
            else
            {
                strCalledForPrtChk = "";
                if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != "")
                {
                    strLOB_ID = Request.QueryString["LOB_ID"].ToString();
                }
                if (strLOB_ID == "" || strLOB_ID == "0")
                {
                    gstrLOBCODE = GetLOBString();
                }
                else
                {
                    if (strLOB_ID == ((int)enumLOB.AUTOP).ToString())
                        gstrLOBCODE = "PPA";
                    else if (strLOB_ID == ((int)enumLOB.HOME).ToString())
                        gstrLOBCODE = "HOME";
                    else if (strLOB_ID == ((int)enumLOB.BOAT).ToString())
                        gstrLOBCODE = "WAT";
                    else if (strLOB_ID == ((int)enumLOB.CYCL).ToString())
                        gstrLOBCODE = "MOT";
                    else if (strLOB_ID == ((int)enumLOB.REDW).ToString())
                        gstrLOBCODE = "RENT";
                    else if (strLOB_ID == ((int)enumLOB.UMB).ToString())
                        gstrLOBCODE = "UMB";
                }

            }
        }

        private void ShowPdf(string strS)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + strS); 
           Response.WriteFile(strS); 
            //Response.TransmitFile(strS);
            Response.End();
           
            Response.Flush();
            Response.Clear();

        }

        public string GetExistingPdf(string strpath, string strnameBegin)
        {
            try
            {
                Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment = new Cms.BusinessLayer.BlCommon.ClsAttachment();
                string strImpersonationUserId = "", strImpersonationPassword = "", strImpersonationDomain = "";
                //AcordPDF.ImpersonateWrapper objEbixImpersonatePDF = new AcordPDF.ImpersonateWrapper();
                strImpersonationUserId = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName").ToString().Trim();
                strImpersonationPassword = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd").ToString().Trim();
                strImpersonationDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain").ToString().Trim();
                objAttachment.ImpersonateUser(strImpersonationUserId, strImpersonationPassword, strImpersonationDomain);

                string FilePath = strpath;

                FileInfo finfo = new FileInfo(FilePath);

                DirectoryInfo dinfo = finfo.Directory;

                FileSystemInfo[] fsinfo = dinfo.GetFiles();
                          
                    foreach (FileSystemInfo info in fsinfo)
                    {
                        if (info.Name.StartsWith(strnameBegin))
                        {
                            objAttachment.endImpersonation();
                            return info.Name;
                        }
                    }
                objAttachment.endImpersonation();
                return "";
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return "";
            }
        }

    }
}
