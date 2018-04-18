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
using BlGeneratePdf;

namespace Cms.Application.Aspx
{
	/// <summary>
	/// Summary description for DecPage.
	/// </summary>
	public class DecPage : Cms.Application.appbase
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl iframeACORDPDF;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCHECK_PDF;
		string stateCode="";
		string gstrLOBCODE="";
		string strCheck="CHK";
		//int ACCOUNT_ID;
		string CHECK_ID;
		string PDFName="";
		string strCustomerId, strAppId="", strAppVersionId="";//,strVehicleId="";
		string strCalledFrom = "";
		string strCalledForPDF="DECPAGE";//"ACORD"/"DECPAGE"	
		//string strCalledForPrtChk="";
		//string strPolicyProcessID="";
        string getRequeststring, getRequestNotice;
		//string FilePath ="";
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;

        private void Page_Load(object sender, System.EventArgs e) 
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "BoletoReprint", "<script language=javascript> self.location ='/cms/Policies/Aspx/BillingInfo.aspx?CALLEDFROM=" + hidCALLED_FROM.Value + "&';</script>");
          //  ClientScript.RegisterStartupScript(this.GetType(), "BoletoReprint", "<script language=javascript> self.location =' /cms/application/Aspx/DeclarationPage.aspx';</script>");
          //  Response.Redirect("DeclarationPage.aspx");//?CUSTOMER_ID=" + lIntCUSTOMER_ID + "&APP_ID=" + lIntAPP_ID + "&APP_VERSION_ID=" + lIntAPP_VERSION_ID + "&LOBID=" + lStrLobID + "&QUOTE_ID=" + lIntQuoteID + "&SHOW=" + lIntShowQuote);
            GeneratePdf();
        }

        private void GeneratePdf()//object sender, System.EventArgs e
		{
			try
			{

				GetQueryStringValues();

                if (strCalledForPDF != "PREM" && getRequestNotice != "Notice")
                {
                    Response.Redirect("DeclarationPage.aspx");
                    return;
                }
                else
                {
                    if (getRequestNotice.Equals("Notice"))
                    {
                        if (strCalledForPDF == "PREM")
                            base.ScreenId = "390_0";
                    }
                    string CARRIER_code = GetSystemId();
                    //BlGeneratePdf.ControlParse objControlParse = new BlGeneratePdf.ControlParse();
                    //objControlParse.CUSTOMER_Id = int.Parse(strCustomerId);
                    //objControlParse.Policy_Id = int.Parse(strAppId);
                    //objControlParse.Policy_Version = int.Parse(strAppVersionId);
                    //objControlParse.ConnStr = ClsCommon.ConnStr;
                    //objControlParse.CARRIER_CODE = CARRIER_code;
                    //objControlParse.LANG_ID = ClsCommon.BL_LANG_ID;
                    //objControlParse.IDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain").ToString();
                    //objControlParse.IUserName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName").ToString();
                    //objControlParse.IPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd").ToString();
                    //objControlParse.PdfTemplatePath = ClsCommon.GetKeyValueWithIP("PremiumNotice_Template_Path");
                    //objControlParse.MapXmlFilePath = ClsCommon.GetKeyValueWithIP("PDFCONTROLNAMEMAPPING_XML__Path");
                    //objControlParse.PdfOutPutPath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()).ToString() + "\\OUTPUTPDFs\\";
                    //PDFName = objControlParse.GeneratePdf();

                   

                    //ClsNotice Code

                    ClsNotices objNotices = new ClsNotices();
                    PDFName = objNotices.GeneratePremiumNotice(int.Parse(strCustomerId), int.Parse(strAppId), int.Parse(strAppVersionId), CARRIER_code, null, ClsCommon.BL_LANG_ID);

                    PDFName = "\\OUTPUTPDFs\\" + PDFName;

                    Response.Redirect(ClsCommon.CreateContentViewerURL(PDFName, FILE_TYPE_PDF));
                    return;
                }

                //DataTable objProcessTable;
                //DataTable objPolicyStatusTable;
                //DataTable objPolicyMaxVerTable;
                //DataTable objEndoPrePolicyMaxVerTable;
                //DataTable objPolicyFromConversionInfo;
                //string strEndoPrePolicyMaxVer = "";
                //string strPolicyMaxVer = "";
                //string strProcessStatus = "";
                //string strPolicyStatus = "";
                //int intProcessID = 0;
                //string strRevrtVer = "";
                //string PDFFinalPath = "";
                //string PDFNameBegin = "", PDFNameBeginCust = "";
                //string strPolicyFromConversion = "";
               // //int CustomerID = int.Parse(hidCUSTOMER_ID.Value);
               // //int PolicyID = int.Parse(hidPOLICY_ID.Value);
               // //int PolicyVersionID = int.Parse(hidPOLICY_VERSION_ID.Value);
               // //Response.Redirect("/cms/application/Aspx/PolicyDecPage.aspx");
               // //Response.Redirect("/cms/application/Aspx/DeclarationPage.aspx");//?CUSTOMER_ID=" + CustomerID + "&POLICY_ID=" + PolicyID + "&POLICY_VERSION_ID=" + PolicyVersionID+"&");
                
               //// return;
               // //ClsProductPdfXml objgenerateDocuments = new ClsProductPdfXml();
               // //PDFName = objgenerateDocuments.generateDocuments(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), "POLICY", int.Parse(GetUserId()));
               // //string agencycode = objgenerateDocuments.GetPolicyAgencyCode(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), "POLICY");
               // //if (PDFFinalPath == "")
               // //    PDFFinalPath = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString() + "/OUTPUTPDFs/" + agencycode + "/" + strCustomerId + "/" + strCalledFrom + "/" + "final" + "/";
               // //string PdfFilePath = "/OUTPUTPDFs/" + agencycode + "/" + strCustomerId + "/" + strCalledFrom + "/" + "temp" + "/" + PDFName;
               // //if (PDFFinalPath.IndexOf("/cms/Upload") >= 0)
               // //{
               // //    PdfFilePath = PDFFinalPath.Replace("/cms/Upload", "") + "/" + "temp" + "/" + PDFName;
               // //}

               // //Response.Redirect(ClsCommon.CreateContentViewerURL(PdfFilePath, FILE_TYPE_PDF));

               // //objgenerateDocuments.generateDocuments(2126, 179, 2, "POLICY");//Policy with Installment
               // //objgenerateDocuments.generateDocuments(2164, 1, 1, "POLICY");//Policy without Installment
               // //objgenerateDocuments.generateDocuments(2167, 1, 1, "POLICY");//Application without Installment
               // //return;
               // if (strCalledFrom!="")
               // {
               //     if(strCalledFrom.Equals("POLICY"))
               //     {
               //         //For policy
               //         if(strAppId == "")
               //             strAppId = GetPolicyID();
               //         if(strAppVersionId == "")
               //             strAppVersionId = GetPolicyVersionID();
				
               //         //added by pravesh on 29 april 2008 to check if dec page is genrated for the process
               //         Cms.BusinessLayer.BlCommon.ClsCommon objPolicyProcess=new Cms.BusinessLayer.BlCommon.ClsCommon();
               //         objProcessTable = objPolicyProcess.GetPolicyProcessInfo(int.Parse(strCustomerId),int.Parse(strAppId),int.Parse(strAppVersionId));
               //         if (objProcessTable!=null && objProcessTable.Rows.Count>0)
               //         {
               //             intProcessID=int.Parse(objProcessTable.Rows[0]["PROCESS_ID"].ToString());
               //             strProcessStatus=objProcessTable.Rows[0]["PROCESS_STATUS"].ToString();
               //             strRevrtVer=objProcessTable.Rows[0]["LAST_REVERT_BACK"].ToString();
               //         }
               //         objPolicyStatusTable = objPolicyProcess.GetPolicyStatusInfo(int.Parse(strCustomerId),int.Parse(strAppId),int.Parse(strAppVersionId));
               //         if (objPolicyStatusTable!=null && objPolicyStatusTable.Rows.Count>0)
               //         {
               //             strPolicyStatus=objPolicyStatusTable.Rows[0]["POLICY_STATUS"].ToString();
               //         }
               //         objPolicyMaxVerTable = objPolicyProcess.GetPolicyMaxVerInfo(int.Parse(strCustomerId),int.Parse(strAppId),int.Parse(strAppVersionId));
               //         if (objPolicyMaxVerTable!=null && objPolicyMaxVerTable.Rows.Count>0)
               //         {
               //             strPolicyMaxVer=objPolicyMaxVerTable.Rows[0]["NEW_POLICY_VERSION_ID"].ToString();
               //         }
               //         objPolicyFromConversionInfo = objPolicyProcess.GetPolicyFromConversionInfo(int.Parse(strCustomerId),int.Parse(strAppId),int.Parse(strAppVersionId));
               //         if(objPolicyFromConversionInfo!=null && objPolicyFromConversionInfo.Rows.Count>0)
               //         {
               //             strPolicyFromConversion=objPolicyFromConversionInfo.Rows[0]["FROM_AS400"].ToString();
               //         }
						
               //         Cms.BusinessLayer.BlApplication.clsapplication objApp = new Cms.BusinessLayer.BlApplication.clsapplication();
               //         if(strCalledForPDF=="DECPAGE")
               //         {
               //             strPolicyProcessID=intProcessID.ToString();
               //             if(strPolicyProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RESCIND.ToString() || strPolicyProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RESCIND_PROCESS.ToString() || strPolicyProcessID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_CANCELLATION_PROCESS.ToString()|| strPolicyProcessID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_CANCELLATION_PROCESS.ToString())
               //             {
               //                 if(Convert.ToInt32(strAppVersionId) >1)
               //                 {
               //                     strAppVersionId=Convert.ToString(Convert.ToInt32(strAppVersionId)-1);
               //                 }
               //             }
               //             // Getting Generated PDF file details
               //             DataSet prnjobds= objApp.FetchPrnJobDetails(int.Parse(strCustomerId), int.Parse(strAppId), int.Parse(strAppVersionId));
               //             if(prnjobds != null && prnjobds.Tables[0].Rows.Count > 0)
               //             {
               //                 foreach(DataRow prnjobdrcust in prnjobds.Tables[0].Rows)
               //                 {
               //                     if(prnjobdrcust["ENTITY_TYPE"].ToString() == "CUSTOMER")
               //                     {
               //                         PDFFinalPath=prnjobdrcust["URL_PATH"].ToString();
               //                         if(PDFFinalPath.IndexOf("final/")>0)
               //                         {
               //                         }
               //                         else
               //                         {
               //                             PDFFinalPath=PDFFinalPath+"/";
               //                         }
               //                         PDFNameBeginCust=prnjobdrcust["FILE_NAME"].ToString();
               //                     }
               //                 }
               //             }
						
               //         }
               //         else if(strCalledForPDF.ToUpper()=="ACORD")
               //         {
               //             strPolicyProcessID=intProcessID.ToString();
               //             if(strPolicyProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RESCIND.ToString() || strPolicyProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RESCIND_PROCESS.ToString() || strPolicyProcessID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_CANCELLATION_PROCESS.ToString()|| strPolicyProcessID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_CANCELLATION_PROCESS.ToString())
               //             {
               //                 if(Convert.ToInt32(strAppVersionId) >1)
               //                 {
               //                     strAppVersionId=Convert.ToString(Convert.ToInt32(strAppVersionId)-1);
               //                 }
               //             }
               //             DataSet prnjobds= objApp.FetchPdfFileLogDetails(int.Parse(strCustomerId), int.Parse(strAppId), int.Parse(strAppVersionId));
               //             if(prnjobds != null && prnjobds.Tables[0].Rows.Count > 0)
               //             {
               //                 foreach(DataRow prnjobdrcust in prnjobds.Tables[0].Rows)
               //                 {
               //                     if(prnjobdrcust["ENTITY_TYPE"].ToString() == "ACORD")
               //                     {
               //                         PDFFinalPath=prnjobdrcust["URL_PATH"].ToString();
               //                         if(PDFFinalPath.IndexOf("final/")>0)
               //                         {
               //                         }
               //                         else
               //                         {
               //                             PDFFinalPath=PDFFinalPath+"/";
               //                         }
               //                         PDFNameBeginCust=prnjobdrcust["FILE_NAME"].ToString();
               //                     }
               //                 }
               //             }
               //         }
               //         //					Cms.BusinessLayer.BlProcess.ClsCommonPdfXML objCommonnPdfXml = new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML();
               //         //strPolicyProcessID=objCommonnPdfXml.GetPolicyProcess(strCustomerId,strAppId,strAppVersionId,strCalledFrom);
               //         strPolicyProcessID=intProcessID.ToString();
               //         if(strPolicyProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RESCIND.ToString() || strPolicyProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RESCIND_PROCESS.ToString() || strPolicyProcessID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_CANCELLATION_PROCESS.ToString())
               //         {
               //             //if application version id > 1 then decreasing it by 1
							
               //             if(Convert.ToInt32(strAppVersionId) >1)
               //             {
               //                 strAppVersionId=Convert.ToString(Convert.ToInt32(strAppVersionId)-1);
               //             }
               //         }
               //         if(strPolicyProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REVERT_PROCESS.ToString() ||  strPolicyProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REVERT_PROCESS.ToString())
               //         {
               //             string[] arrLstRertBck = new string[0];
               //             arrLstRertBck = strRevrtVer.Split('^');
               //             if(arrLstRertBck.Length >1)
               //             {
               //                 strAppVersionId = arrLstRertBck[5];
               //             }
               //             else
               //             {
               //                 strAppVersionId = "";
               //             }
               //         }
               //         //					stateCode=new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode(strCalledFrom,int.Parse(strAppId),int.Parse(strAppVersionId),int.Parse(strCustomerId) );  
               //     }				
               //     else if(strCalledFrom.Equals("UNDERLAYING_POL"))
               //     {
					
               //         if(Request.QueryString["APP_ID"].Equals("APP"))
               //         {
               //             strCalledFrom = "APPLICATION";
               //         }
               //         else
               //         {
               //             strCalledFrom = "POLICY";

               //         }				
               //         //					stateCode=new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode(strCalledFrom,int.Parse(strAppId),int.Parse(strAppVersionId),int.Parse(strCustomerId) );  
				
               //     }
               //     else if (strCalledFrom.Equals("ACCOUNT"))
               //     {
               //         // by default set screen id same as for policy need to change later 
               //         base.ScreenId="343_0";
               //         Form1099Pdf();
               //         return;
               //     }
               // }
               // else
               // {
               //     //For application
				
               //     strCalledFrom = "APPLICATION";				
               //     strAppId = GetAppID();
               //     strAppVersionId = GetAppVersionID();
               //     //				stateCode=new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode(strCalledFrom,int.Parse(strAppId),int.Parse(strAppVersionId),int.Parse(strCustomerId));  				
               // }

               // //Done by Sibin for Itrack Issue 0n 12 Jan 09
               // if(getRequestNotice.Equals("CHECKPDFPRINT") || getRequestNotice.Equals("CUST_RECEIPT"))
               // {
               //     //Mohit Agarwal(21-Oct-2008)
               //     if(strCalledForPDF =="PREM" ) 
               //         base.ScreenId="390_0";
               //     else if (getRequestNotice.Equals("CUST_RECEIPT"))
               //         base.ScreenId="392_0";
               //     else if (getRequestNotice.Equals("CHECKPDFPRINT"))
               //         base.ScreenId="218_0";
               // }

               // else
               // {
               //     if(strCalledForPDF=="DECPAGE")
               //     {
               //         if (strCalledFrom=="POLICY") 
               //             base.ScreenId="343_0";
               //         else 
               //             base.ScreenId="342_0";
               //     }
               //     else if(strCalledForPDF=="ACORD")
               //     {
               //         if (strCalledFrom=="POLICY") 
               //             base.ScreenId="341_0";
               //         else 
               //             base.ScreenId="340_0";
               //     }
               // }
               // //Done till here
               // //			string strAcordPDFXml = "", strCheckPDFXml="";				

               // //			if(getRequestNotice.Equals("CHECKPDFPRINT"))
               // //			{
               // //				strCheckPDFXml = new Cms.BusinessLayer.BlProcess.ClsCheckPdfXml(CHECK_ID).getCheckPDFXml();
               // //				
               // //			}
               // //			else
               // //			{				
               // /*if(getRequeststring.Equals("UNDERLAYING_POL"))
               //     {
               //         if (Request.QueryString["LOB_ID"].Equals("4"))
               //         {
               //             strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsBoatPdfXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,stateCode ).getBoatAcordPDFXml();
               //             gstrLOBCODE = "WAT";
               //         }
               //         else if (Request.QueryString["LOB_ID"].Equals("1"))
               //         {
               //             strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsHomePdfXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,stateCode).getHomeAcordPDFXml();
               //             gstrLOBCODE = "HOME";
               //         }
               //         else if (Request.QueryString["LOB_ID"].Equals("6"))
               //         {
               //             strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsRedwPdfXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,stateCode).getRentalAcordPDFXml();
               //             gstrLOBCODE = "RENT";
               //         }
               //         else if (Request.QueryString["LOB_ID"].Equals("2"))
               //         {
               //             strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsAutoPDFXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,stateCode).getAutoAcordPDFXml();
               //             gstrLOBCODE = "PPA";
               //         }
               //         else if (Request.QueryString["LOB_ID"].Equals("3"))
               //         {
               //             strAcordPDFXml = new Cms.BusinessLayer.BlProcess.clsMotorCyclePdfXml(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,stateCode).getMotorCycleAcordPDFXml();
               //             gstrLOBCODE = "MOT";
               //         }
               //         else if (Request.QueryString["LOB_ID"].Equals("5"))
               //         {
               //             strAcordPDFXml = new Cms.BusinessLayer.BlProcess.clsUmbrellaPdfXml(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,stateCode).getUmbrellaAcordPDFXml();
               //             gstrLOBCODE = "UMB";
               //         }

               //     }
			
               //     else
               //     {

               //         if (GetLOBString().ToUpper().Trim() == "WAT")
               //             strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsBoatPdfXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,stateCode ).getBoatAcordPDFXml();
               //         else if (GetLOBString().ToUpper().Trim() == "HOME")
               //             strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsHomePdfXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,stateCode).getHomeAcordPDFXml();
               //         else if (GetLOBString().ToUpper().Trim() == "RENT")
               //             strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsRedwPdfXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,stateCode).getRentalAcordPDFXml();
               //         else if (GetLOBString().ToUpper().Trim() == "PPA")
               //             strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsAutoPDFXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,stateCode).getAutoAcordPDFXml();
               //         else if (GetLOBString().ToUpper().Trim() == "MOT")
               //             strAcordPDFXml = new Cms.BusinessLayer.BlProcess.clsMotorCyclePdfXml(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,stateCode).getMotorCycleAcordPDFXml();
               //         else if (GetLOBString().ToUpper().Trim() == "UMB")
               //             strAcordPDFXml = new Cms.BusinessLayer.BlProcess.clsUmbrellaPdfXml(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,stateCode).getUmbrellaAcordPDFXml();
               //     }*/
               // //strAcordPDFXml = new Cms.BusinessLayer.BlProcess.clsUmbrellaPdfXml(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,stateCode).getUmbrellaAcordPDFXml();
               // //			}
			
					
               // //			AcordPDF.AcordXMLParser objEbixAcordPDF = new AcordPDF.AcordXMLParser();
               // //			if(strCustomerId!=null && strCustomerId!="")
               // //			objEbixAcordPDF.ClientId = int.Parse(strCustomerId);
               // //			if(strAppId!=null && strAppId!="")
               // //			objEbixAcordPDF.PolicyId = int.Parse(strAppId);
               // //			if(strAppVersionId!=null && strAppVersionId!="")
               // //			objEbixAcordPDF.PolicyVersion = int.Parse(strAppVersionId);
               // //			if(getRequestNotice.Equals("CHECKPDFPRINT"))
               // //			{
               // //				objEbixAcordPDF.LobCode = strCheck;
               // //
               // //			}
               // //			
               // //			else 
               // //			{
               // //					if(getRequeststring.Equals("UNDERLAYING_POL"))
               // //					{
               // //						 objEbixAcordPDF.LobCode = gstrLOBCODE.ToString();
               // //					}
               // //					else 
               // //					{
               // //						objEbixAcordPDF.LobCode = GetLOBString();
               // //					}
               // //			}
               // //			if(getRequestNotice.Equals("CHECKPDFPRINT"))
               // //			{
               // //				objEbixAcordPDF.InputXml = strCheckPDFXml;
               // //				objEbixAcordPDF.InputPath = Request.PhysicalApplicationPath.ToString() + "CmsWeb\\INPUTPDFs\\" + strCheck.ToString().ToUpper().Trim() + "\\" ;
               // //				objEbixAcordPDF.OutputPath = Server.MapPath(System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL").ToString()).ToString() + "\\OUTPUTPDFs\\" + GetSystemId()+ "\\" + strCheck;
               // //			}
               // //			else
               // //			{
               // //				objEbixAcordPDF.InputXml = strAcordPDFXml;
               // //				if(getRequeststring.Equals("UNDERLAYING_POL"))
               // //				{
               // //					objEbixAcordPDF.InputPath = Request.PhysicalApplicationPath.ToString() + "CmsWeb\\INPUTPDFs\\" + gstrLOBCODE.ToString().ToUpper().Trim() + "\\" + stateCode.ToUpper().Trim()  + "\\" ;
               // //				}
               // //				else
               // //				{
               // //					objEbixAcordPDF.InputPath = Request.PhysicalApplicationPath.ToString() + "CmsWeb\\INPUTPDFs\\" + GetLOBString().ToUpper().Trim() + "\\" + stateCode.ToUpper().Trim()  + "\\" ;
               // //				}
               // //				objEbixAcordPDF.OutputPath = Server.MapPath(System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL").ToString()).ToString() + "\\OUTPUTPDFs\\" + GetSystemId() + "\\" + strCustomerId;
               // //			}
               // //			objEbixAcordPDF.ImpersonationUserId = System.Configuration.ConfigurationSettings.AppSettings.Get("IUserName").ToString().Trim();
               // //			objEbixAcordPDF.ImpersonationPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("IPassWd").ToString().Trim();
               // //			objEbixAcordPDF.ImpersonationDomain = System.Configuration.ConfigurationSettings.AppSettings.Get("IDomain").ToString().Trim();
               // //
               // //				//objEbixAcordPDF.OutputPath = Request.PhysicalApplicationPath.ToString() + "CmsWeb\\OUTPUTPDFs\\" + GetSystemId() ;
               // //					
               // //			
               // //				PDFName = objEbixAcordPDF.GeneratePDF(strCalledFrom);

               // string agency_code=GetSystemId();
               // string agency_name = GetSystemId();
               // //Changed by Charles on 19-May-10 for Itrack 51
               // string strCarrierSystemID = CarrierSystemID;//System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString().ToUpper();
               // // if policy is from conversion and policy version below than new business or renewal 
               // if(strAppVersionId!="" && strPolicyMaxVer!="")
               // {
               //     if(agency_name.ToUpper()!=strCarrierSystemID.Trim().ToUpper() && Convert.ToInt32(strAppVersionId) < Convert.ToInt32(strPolicyMaxVer) && strPolicyFromConversion.ToString().ToUpper().Trim()=="Y")
               //     {
               //         //Message Change For Itrack Issue #5487.
               //         //Response.Write("<script language='javascript'> alert('This Policy Is From Conversion. Declaration Page is Not Available. To Get Dec Page See Succeeded Version.'); </script>");
               //         if(strCalledForPDF == "AUTOCARD")
               //         {
               //             Response.Write("<script language='javascript'> alert('This policy is from Conversion. Auto Id Cards are not available.'); </script>");//Alert message changed by Charles on 18-Sep-09 for Itrack 6422
               //         }
               //         else
               //         {
               //             Response.Write("<script language='javascript'> alert('This Policy Is From Conversion. Declaration Page is Not Available. To Get Dec Page See active Version.'); </script>");
               //         }
					
               //         Response.Write("<script>window.close();</script>");
               //         return;
               //     }
               // }
               // Cms.BusinessLayer.BlProcess.ClsCommonPdf objCommonPdf = new Cms.BusinessLayer.BlProcess.ClsCommonPdf();
               // Cms.BusinessLayer.BlProcess.ClsCommonPdfXML objCommonPdfXml = new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML();
				
               // if(getRequestNotice.Equals("CHECKPDFPRINT") || getRequestNotice.Equals("CUST_RECEIPT"))
               // {
               //     //Ravindra(03-13-2008)
               //     if(strCalledForPDF =="PREM" )
               //     {
               //         BlGeneratePdf.ControlParse objControlParse = new BlGeneratePdf.ControlParse();
               //         objControlParse.CUSTOMER_Id = int.Parse(strCustomerId);
               //         objControlParse.Policy_Id = int.Parse(strAppId);
               //         objControlParse.Policy_Version = int.Parse(strAppVersionId);
               //         objControlParse.ConnStr = ClsCommon.ConnStr;
               //         objControlParse.CARRIER_CODE = "w001";
               //         objControlParse.LANG_ID = ClsCommon.BL_LANG_ID;
               //         objControlParse.PdfTemplatePath = ClsCommon.GetKeyValueWithIP("PremiumNotice_Template_Path");
               //         objControlParse.XmlFilePath = ClsCommon.GetKeyValueWithIP("PDFCONTROLNAMEMAPPING_XML__Path");
               //         objControlParse.PdfOutPutPath = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString() + "/OUTPUTPDFs/";
               //         PDFName = objControlParse.GeneratePdf();
               //         Response.Redirect(ClsCommon.CreateContentViewerURL(PDFName, FILE_TYPE_PDF));
               //         return;
               //         //Insert PDF details in PDF_FILE_LOG Table
               //         //BusinessLayer.BlProcess.ClsNotices objNotice = new Cms.BusinessLayer.BlProcess.ClsNotices();
               //         //PDFName = objNotice.GeneratePremiumNotice(Convert.ToInt32(strCustomerId),Convert.ToInt32(strAppId),Convert.ToInt32(strAppVersionId),strCarrierSystemID,"");
                        
               //     }
               //     else
               //     {
               //         //Condition Added For Itrack Issue #6437
               //         if(getRequestNotice=="CUST_RECEIPT")
               //             PDFName = objCommonPdfXml.GeneratePdfProxy(strCustomerId,strAppId,strAppVersionId,strCalledFrom,strCalledForPDF,gstrLOBCODE,getRequeststring,ref agency_code,getRequestNotice,CHECK_ID,"temp");
               //         else
               //             PDFName = objCommonPdf.PrintChecks(strCalledFrom,ref agency_code,CHECK_ID);
               //     }

               //     if(PDFName == "BLANK_NUM")
               //         Response.Write("<script language='javascript'> alert('Check(s) could not be generated due to Account(s) missing Bank Information'); </script>");
               //     else if(PDFName.IndexOf("BLANK_NUM_SOME") == 0)
               //     {
               //         string []PDFtemp = PDFName.Split('~');
               //         if(PDFtemp.Length > 1)
               //             PDFName = PDFtemp[1];
               //         Response.Write("<script language='javascript'> alert('Some check(s) not generated due to Account(s) missing Bank Information'); </script>");
               //         hidCHECK_PDF.Value = "BLANK_NUM_SOME";
               //         //Response.Redirect(System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL").ToString() + "/OUTPUTPDFs/" + agency_code + "/" + strCheck + "/" + PDFName);				
               //         //Response.Write Commented by Raghav and change in to Response.Redirect. 
               //         //Response.Write("<script language='javascript'> window.open('" +System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL").ToString() + "/OUTPUTPDFs/" + agency_code + "/" + strCheck + "/" + PDFName + "'); </script>");
               //         //Added By Raghav
               //         string FilePath = "/OUTPUTPDFs/" + agency_code + "/" + strCheck + "/" + PDFName ; 
			
               //         Response.Redirect( ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF )) ; 
               //     }
               //     else
               //     {
               //         //Response.Redirect(System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL").ToString() + "/OUTPUTPDFs/" + agency_code + "/" + strCheck + "/" + PDFName);				
               //         //Changes By Raghav  
               //         string FilePath =  "/OUTPUTPDFs/" + agency_code + "/" + strCheck + "/" + PDFName ; 
               //         Response.Redirect( ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF )) ; 
               //     }
               // }
               // else
               // {
               //     Cms.BusinessLayer.BlProcess.ClsPolicyProcess objPolicyProcess = new Cms.BusinessLayer.BlProcess.ClsPolicyProcess();
               //     objPolicyProcess.BeginTransaction();
               //     if(strAppVersionId =="" && strPolicyProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REVERT_PROCESS.ToString())
               //     {
               //         Response.Write("<script language='javascript'> alert('Revert Back Process is in progress. Please select policy version which you want to revert back.'); </script>");
               //         return;
               //     }
               //     agency_code = objPolicyProcess.GetPDFAgencyCode(int.Parse(strCustomerId),int.Parse(strAppId),int.Parse(strAppVersionId),strCalledFrom);
               //     objPolicyProcess.CommitTransaction();
               //     //ITrack 3251 24-Dec-07
               //     if(agency_code.Trim().EndsWith("."))
               //         agency_code = agency_code.Substring(0, agency_code.LastIndexOf("."));
               //     if(PDFFinalPath=="")
               //         PDFFinalPath = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString() + "/OUTPUTPDFs/" + agency_code + "/" + strCustomerId + "/" + strCalledFrom + "/" + "final" + "/";
				
               //     if(strCalledForPDF == "DECPAGE")
               //     {
               //         if(PDFNameBegin=="")
               //             PDFNameBegin = gstrLOBCODE + "_DEC_PAGE" + "_" + strCustomerId + "_" + strAppId  + "_" + strAppVersionId;
               //         if(PDFNameBeginCust=="")
               //             PDFNameBeginCust = gstrLOBCODE + "_DEC_PAGE_C" + "_" + strCustomerId + "_" + strAppId  + "_" + strAppVersionId;
               //     }
               //     else if(strCalledForPDF == "ACORD")
               //         PDFNameBegin = gstrLOBCODE + "_ACORD" + "_" + strCustomerId + "_" + strAppId  + "_" + strAppVersionId;		
               //     else if(strCalledForPDF == "AUTOCARD")
               //         PDFNameBegin = gstrLOBCODE + "_AUTO_ID_CARD" + "_" + strCustomerId + "_" + strAppId  + "_" + strAppVersionId;		
               //     else
               //         PDFNameBegin = gstrLOBCODE + "0" + "_" + strCustomerId + "_" + strAppId  + "_" + strAppVersionId;		

               //     PDFName = "";

               //     if(PDFNameBeginCust != "")
               //         PDFName = GetExistingPdf(PDFFinalPath, PDFNameBeginCust);

               //     if (Request.QueryString["CALLEDAGENCY"]!=null && Request.QueryString["CALLEDAGENCY"].ToString()!="" && Request.QueryString["CALLEDAGENCY"].ToString()=="Y")
               //     {
               //         if(PDFName != "" && strCalledForPDF == "DECPAGE")
               //         {
               //             string LobId="";
               //             if (Request.QueryString["LOB_ID"]!=null && Request.QueryString["LOB_ID"].ToString()!="")
               //             {
               //                 LobId = Request.QueryString["LOB_ID"].ToString();
               //             }
               //             if(LobId=="" || LobId=="0")
               //             {
               //                 LobId = GetLOBID();
               //             }
               //             if(intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_CANCELLATION_PROCESS || intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_CANCELLATION_PROCESS || intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RESCIND 
               //                 || intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RESCIND_PROCESS || strPolicyStatus==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_EXPIRED)
               //             {
               //                 if(agency_name.ToUpper()!=strCarrierSystemID.Trim().ToUpper())
               //                 {
               //                     Response.Write("<script language='javascript'> alert('Declaration Page is Not Available – See Prior Version.'); </script>");
               //                 }
               //                 else
               //                 {
               //                     string querystring = "&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strAppId + "&POLICY_VER_ID=" + strAppVersionId;
               //                     string decpageselurl = "../../cmsweb/aspx/AgencyDecSelect.aspx?CalledFrom=POLICY&CALLEDFOR=DECPAGE&CALLEDAGENCY=" + agency_code + "&LOB_ID=" + LobId + querystring;
               //                     Response.Redirect(decpageselurl);
               //                 }
               //             }
               //             else
               //             {
               //                 string querystring = "&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strAppId + "&POLICY_VER_ID=" + strAppVersionId;
               //                 string decpageselurl = "../../cmsweb/aspx/AgencyDecSelect.aspx?CalledFrom=POLICY&CALLEDFOR=DECPAGE&CALLEDAGENCY=" + agency_code + "&LOB_ID=" + LobId + querystring;
               //                 Response.Redirect(decpageselurl);
               //             }
               //             //Response.Write("<script language='javascript'> window.open('" + decpageselurl + "'); </script>");
               //             return;
               //         }
               //         else
               //         {
               //             //change by pravesh on 29 april 08
               //             if(intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REVERT_PROCESS )
               //                 Response.Write("<script language='javascript'> alert('Declaration Page is not available for this version.'); </script>");
               //             else if(intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RESCIND_PROCESS)
               //             {
               //                 if(agency_name.ToUpper()!=strCarrierSystemID.Trim().ToUpper())
               //                 {
               //                     Response.Write("<script language='javascript'> alert('Declaration Page is Not Available – See Prior Version.'); </script>");
               //                 }
               //                 else
               //                 {
               //                     Response.Write("<script language='javascript'> alert('Declaration Page is not available for this version.'); </script>");
               //                 }
               //             }
               //             else if(intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_CANCELLATION_PROCESS || intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_CANCELLATION_PROCESS 
               //                 || intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RESCIND || strPolicyStatus==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_EXPIRED)
               //             {
               //                 if(agency_name.ToUpper()!=strCarrierSystemID.Trim().ToUpper())
               //                 {
               //                     Response.Write("<script language='javascript'> alert('Declaration Page is Not Available – See Prior Version.'); </script>");
               //                 }
               //                 else
               //                 {
               //                     Response.Write("<script language='javascript'> alert('Declaration Page is not available for this version.'); </script>");
               //                 }
               //             }
               //             else if(strProcessStatus==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.PROCESS_STATUS_PENDING)
               //                 Response.Write("<script language='javascript'> alert('This version of the policy is In Progress and has not been committed. Declaration Page is not available at this time.'); </script>");
               //             else
               //                 Response.Write("<script language='javascript'> alert('Declaration Page is not available at this time.'); </script>");
               //             return;
               //         }


               //     }
				
               //     if(PDFName == "")
               //         PDFName = GetExistingPdf(PDFFinalPath, PDFNameBegin);

               //     // If Called from Agency Login and autoId card is not genrated for 
               //     //that version of policy 
               //     //then so id card from previous version
               //     if(PDFName == "")
               //     {
               //         if(strCalledForPDF == "AUTOCARD" && agency_name.ToUpper()!=strCarrierSystemID.Trim().ToUpper())
               //         {
               //             //if( strPolicyProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString())
               //             //{
               //                 if(Convert.ToInt32(strAppVersionId) >1)
               //                 {
               //                     Cms.BusinessLayer.BlCommon.ClsCommon objCommon=new Cms.BusinessLayer.BlCommon.ClsCommon();
               //                     objEndoPrePolicyMaxVerTable= objCommon.GetEndoPrePolicyMaxVerInfo(int.Parse(strCustomerId),int.Parse(strAppId),int.Parse(strAppVersionId));
               //                     if (objEndoPrePolicyMaxVerTable!=null && objEndoPrePolicyMaxVerTable.Rows.Count>0)
               //                     {
               //                         strEndoPrePolicyMaxVer=objEndoPrePolicyMaxVerTable.Rows[0]["ENDO_PRE_POLICY_VERSION_ID"].ToString();
               //                     }
               //                 }
               //             //}
               //             PDFNameBegin=gstrLOBCODE + "_AUTO_ID_CARD" + "_" + strCustomerId + "_" + strAppId  + "_" + strEndoPrePolicyMaxVer;
               //             PDFName = GetExistingPdf(PDFFinalPath, PDFNameBegin);
               //         }
               //     }
               //     if(PDFName == "")
               //     {
               //         if(agency_name.ToUpper()!=strCarrierSystemID.Trim().ToUpper())
               //         {
               //             if(strCalledForPDF == "DECPAGE")
               //             {
               //                 //change by pravesh on 29 april 08
               //                 if(intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REVERT_PROCESS )
               //                 {
               //                     Response.Write("<script language='javascript'> alert('Declaration Page is not available for this version.'); </script>");
               //                     Response.Write("<script>window.close();</script>");
               //                 }
               //                 else if (intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RESCIND_PROCESS)
               //                 {
               //                     Response.Write("<script language='javascript'> alert('Declaration Page is Not Available – See Prior Version.'); </script>");
               //                     Response.Write("<script>window.close();</script>");
               //                 }
               //                 else if(intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_CANCELLATION_PROCESS || intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_CANCELLATION_PROCESS || intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RESCIND )
               //                 {
               //                     Response.Write("<script language='javascript'> alert('Declaration Page is Not Available – See Prior Version.'); </script>");
               //                     Response.Write("<script>window.close();</script>");
               //                 }
               //                 else if(strProcessStatus==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.PROCESS_STATUS_PENDING)
               //                 {
               //                     Response.Write("<script language='javascript'> alert('This version of the policy is In Progress and has not been committed. Declaration Page is not available at this time.'); </script>");
               //                     Response.Write("<script>window.close();</script>");
               //                 }
						
               //                 else
               //                 {
               //                     Response.Write("<script language='javascript'> alert('Declaration Page is not available at this time.'); </script>");
               //                     Response.Write("<script>window.close();</script>");
               //                 }
               //             }
               //             else if(strCalledForPDF == "AUTOCARD")
               //             {
               //                 Response.Write("<script language='javascript'> alert('This policy is from Conversion. Auto Id Cards are not available.'); </script>");//Alert message changed by Charles on 18-Sep-09 for Itrack 6422
               //                 Response.Write("<script>window.close();</script>");
               //             }
               //             else if(strCalledForPDF == "ACORD" && (intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_CANCELLATION_PROCESS || intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_CANCELLATION_PROCESS || intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RESCIND ||intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RESCIND_PROCESS))
               //             {
               //                 Response.Write("<script language='javascript'> alert('Acord Page is Not Available – See Prior Version.'); </script>");
               //                 Response.Write("<script>window.close();</script>");
               //             }
               //         }
               //         else
               //         {
               //             if(gstrLOBCODE=="PPA")
               //             {
               //                 //stateCode = objCommonPdf.SetStateCode(strCalledFrom,int.Parse(strAppId),int.Parse(strAppVersionId),int.Parse(strCustomerId) );  
               //                 gstrLOBCODE ="2";
               //                 PDFName = objCommonPdf.GenratePdfForMenu(strCustomerId,strAppId,strAppVersionId,strCalledFrom,strCalledForPDF,stateCode,gstrLOBCODE,intProcessID.ToString(),ref agency_code, "temp");
               //             }
               //             else if(gstrLOBCODE=="HOME")
               //             {
               //                 //stateCode = objCommonPdf.SetStateCode(strCalledFrom,int.Parse(strAppId),int.Parse(strAppVersionId),int.Parse(strCustomerId) );  
               //                 gstrLOBCODE ="1";
               //                 PDFName = objCommonPdf.GenratePdfForMenu(strCustomerId,strAppId,strAppVersionId,strCalledFrom,strCalledForPDF,stateCode,gstrLOBCODE,intProcessID.ToString(),ref agency_code, "temp");
               //             }
               //             else
               //                 PDFName = objCommonPdfXml.GeneratePdfProxy(strCustomerId,strAppId,strAppVersionId,strCalledFrom,strCalledForPDF,gstrLOBCODE,getRequeststring,ref agency_code,getRequestNotice,CHECK_ID,"temp");

               //             //Response.Redirect(System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL").ToString() + "/OUTPUTPDFs/" + agency_code + "/" + strCustomerId +  "/" + strCalledFrom +  "/" + "temp" + "/" + PDFName);
               //             //Changes By Raghav
               //             FilePath = "/OUTPUTPDFs/" + agency_code + "/" + strCustomerId +  "/" + strCalledFrom +  "/" + "temp" + "/"+ PDFName;						 
               //             //						if(PDFFinalPath.IndexOf("/cms/Upload")>=0)
               //             //						{
               //             //						  FilePath = PDFFinalPath.Replace("/cms/Upload","")+ PDFName;
               //             //						}
               //             //Done By Chetna On 12 March,10
               //             //Start
               //             //Response.Redirect( ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF )) ; 
               //             Response.Write("<script language='javascript'> alert('PDF generation in process...........'); </script>");//By Chetna
               //             Response.Write("<script>window.close();</script>");
               //             return;
               //             //Stop
               //         }
               //     }
               //     else
               //     {
               //         if(agency_name.ToUpper()!=strCarrierSystemID.Trim().ToUpper())
               //         {
               //             if(intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_CANCELLATION_PROCESS || intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_CANCELLATION_PROCESS 
               //                 || intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RESCIND || intProcessID==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RESCIND_PROCESS || strPolicyStatus==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_EXPIRED)
               //                 Response.Write("<script language='javascript'> alert('Declaration Page is Not Available – See Prior Version.'); </script>");
               //             else
               //             {
               //                 //Response.Redirect(PDFFinalPath + PDFName);
               //                 if(PDFFinalPath.IndexOf("/cms/Upload")>=0)
               //                 {
               //                     FilePath = PDFFinalPath.Replace("/cms/Upload","");
               //                     FilePath = FilePath + PDFName;
               //                 }
               //                 Response.Redirect( ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF )) ; 
               //             }
               //         }
               //         else
               //         {
               //             if(PDFFinalPath.IndexOf("/cms/Upload")>=0)
               //             {
               //                 FilePath = PDFFinalPath.Replace("/cms/Upload","");
               //                 FilePath = FilePath + PDFName;
               //             }
               //            //Response.Redirect( ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF )) ; By Chetna on 12March,10
						    
               //         }
               //     }
               // }
				//TabCtl.TabURLs = TabCtl.TabURLs + "," + "/cms/cmsweb/OUTPUTPDFs/" + PDFName;
			}
			catch(Exception ex)
			{
				if(ex.GetType().Name != "ThreadAbortException")
				{
					System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
					addInfo.Add("Err Descriptor ","Error while generating PDF.");
					addInfo.Add("CustomerID" ,strCustomerId);
					addInfo.Add("PolicyID",strAppId);
					addInfo.Add("PolicyVersionID",strAppVersionId);
					addInfo.Add("PolicyVersionID",strCalledFrom);
					addInfo.Add("PolicyVersionID",strCalledForPDF);
					ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo );
					throw(new Exception("Error while genrating pdf.",ex));
				}
			}
		}

		//Added by Mohit Agarwal 31-Jan-2007
		// Check if pdf already generated in final folder
		public string GetExistingPdf(string strpath, string strnameBegin)
		{
			try
			{
				Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment =  new Cms.BusinessLayer.BlCommon.ClsAttachment();
				string strImpersonationUserId="",strImpersonationPassword="",strImpersonationDomain="";
				//AcordPDF.ImpersonateWrapper objEbixImpersonatePDF = new AcordPDF.ImpersonateWrapper();
                strImpersonationUserId = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName").ToString().Trim();
                strImpersonationPassword = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd").ToString().Trim();
                strImpersonationDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain").ToString().Trim();
				objAttachment.ImpersonateUser(strImpersonationUserId, strImpersonationPassword, strImpersonationDomain);
				
				string FilePath = Server.MapPath(strpath+"temp");

				FileInfo finfo = new FileInfo(FilePath);

				DirectoryInfo dinfo = finfo.Directory;

				FileSystemInfo[] fsinfo = dinfo.GetFiles();

				foreach (FileSystemInfo info in fsinfo)
				{
					if(info.Name.StartsWith(strnameBegin))
					{
						objAttachment.endImpersonation();
						return info.Name;
					}
				}
				objAttachment.endImpersonation();
				return "";
			}
			catch(Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				return "";
			}
		}

		public void GetQueryStringValues()
		{

            hidCUSTOMER_ID.Value = GetCustomerID();
            hidPOLICY_ID.Value = GetPolicyID();
            hidPOLICY_VERSION_ID.Value = GetPolicyVersionID();

            //string strLOB_ID="";
            //strCustomerId = GetCustomerID();
            //if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"] != "")
            //{
            strCalledFrom = Request.QueryString["CalledFrom"].ToString().ToUpper();

            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"] != "")
                strCustomerId = Request.QueryString["CUSTOMER_ID"].ToString();

            if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"] != "")
                strAppId = Request.QueryString["POLICY_ID"].ToString();

            if (Request.QueryString["POLICY_VER_ID"] != null && Request.QueryString["POLICY_VER_ID"] != "")
                strAppVersionId = Request.QueryString["POLICY_VER_ID"].ToString();	

            //    if (Request.QueryString["CHECK_ID"] != null && Request.QueryString["CHECK_ID"] != "")
            //        CHECK_ID = Request.QueryString["CHECK_ID"].ToString();
            //    if(Request.QueryString["VEHICLE_ID"] != null && Request.QueryString["VEHICLE_ID"] != "")
            //        strVehicleId = Request.QueryString["VEHICLE_ID"].ToString();
            //}
            if (Request.QueryString["CALLEDFOR"] != null && Request.QueryString["CALLEDFOR"] != "")
            {
                strCalledForPDF = Request.QueryString["CALLEDFOR"].ToUpper();
            }

            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"].ToString() != "")
            {
                getRequeststring = Request.QueryString["CalledFrom"];
                getRequestNotice = Request.QueryString["CalledFrom"];
            }
            else
            {
                getRequeststring = "";
                getRequestNotice = "";

            }
			
            //if (Request.QueryString["CALLEDFORPRINT"] != null && Request.QueryString["CALLEDFORPRINT"] != "")
            //{
            //    strCalledForPrtChk=Request.QueryString["CALLEDFORPRINT"].ToUpper();
            //}
            //else
            //{
            //    strCalledForPrtChk="";
            //    if (Request.QueryString["LOB_ID"]!=null && Request.QueryString["LOB_ID"].ToString()!="")
            //    {
            //        strLOB_ID = Request.QueryString["LOB_ID"].ToString();
            //    }
            //    if(strLOB_ID=="" || strLOB_ID=="0")
            //    {
            //        gstrLOBCODE = GetLOBString();
            //    }
            //    else
            //    {
            //        if(strLOB_ID == ((int)enumLOB.AUTOP).ToString())
            //            gstrLOBCODE = "PPA";
            //        else if(strLOB_ID == ((int)enumLOB.HOME).ToString())
            //            gstrLOBCODE = "HOME";
            //        else if(strLOB_ID == ((int)enumLOB.BOAT).ToString())
            //            gstrLOBCODE = "WAT";
            //        else if(strLOB_ID == ((int)enumLOB.CYCL).ToString())
            //            gstrLOBCODE = "MOT";
            //        else if(strLOB_ID == ((int)enumLOB.REDW).ToString())
            //            gstrLOBCODE = "RENT";
            //        else if(strLOB_ID == ((int)enumLOB.UMB).ToString())
            //            gstrLOBCODE = "UMB";				
            //    }
				
            //}		
		}
		# region form1099 pdf
		private void Form1099Pdf()
		{
			Cms.BusinessLayer.BlProcess.ClsForm1099PdfXml objClsForm1099PdfXml = new Cms.BusinessLayer.BlProcess.ClsForm1099PdfXml(strCustomerId);
			objClsForm1099PdfXml.GenrateForm1099Pdf();
			string agency_code=objClsForm1099PdfXml.strAgencyCode;
			string PdfName=objClsForm1099PdfXml.PDFName;
            string PDFFinalPath = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString() + "/OUTPUTPDFs/" + agency_code + "/" + "form1099" + "/";
			//	Response.Redirect(System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL").ToString() + "/OUTPUTPDFs/" + agency_code + "/"+"form1099" + "/" +PdfName);
			//Comment by Raghav and change.

			//string FilePath = "/OUTPUTPDFs/" + agency_code + "/" + strCheck + "/" + PDFName ; 
			string FilePath =""; 
			if(PDFFinalPath.IndexOf("/cms/Upload")>=0)
			{
				FilePath = PDFFinalPath.Replace("/cms/Upload","")+ PdfName;
			}
			Response.Redirect( ClsCommon.CreateContentViewerURL(FilePath, FILE_TYPE_PDF )) ; 
		
		}
		
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}


