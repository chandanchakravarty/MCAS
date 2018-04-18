using System;
using Cms.BusinessLayer;
using Cms.DataLayer;
using System.Xml;
using System.Data;
using System.Collections;

namespace Cms.BusinessLayer.BlProcess
{
	//Proc_GetPolQuoteDetails
	//Proc_GetQuoteDetails
	//Proc_InsertPremiumSplitDetails
	//Proc_InsertPremiumSplit
	/*public class PremiumDetails
	{
		private string strPremiumXML;
		private string strQuoteType;
		public string PremiumXML
		{
			get
			{
				return strPremiumXML;
			}
			set
			{
				strPremiumXML= value;
			}
		}
		public string QuoteType
		{
			get
			{
				return strQuoteType;
			}
			set
			{
				strQuoteType= value;
			}
		}

	}*/
	//Commented 12-14-2006 As Code Moved to BlQuote/ClsSplitPremium

	public class clsprocess: Cms.BusinessLayer.BlCommon.ClsCommon
	{

		/*public System.Collections.ArrayList  SplitPremiumsApp(int CustomerId,int AppId,int AppVersionId,string ProcessType,DataWrapper objWrapper)
		{
			return SplitPremiums(CustomerId,AppId,AppVersionId,0,0,ProcessType,objWrapper);
		}

		public System.Collections.ArrayList  SplitPremiumsPol(int CustomerId,int PolicyId,int PolicyVersionId,string ProcessType,DataWrapper objWrapper)
		{
			return SplitPremiums(CustomerId,0,0,PolicyId,PolicyVersionId,ProcessType,objWrapper);
		}

		public ArrayList GetPremiumXMLFromQuote(int CustomerId,int PolicyId,int PolicyVersionId,DataWrapper objWrapper)
		{
			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@CUSTOMER_ID",(object)CustomerId.ToString(),SqlDbType.Int);
			objWrapper.AddParameter("@POLICY_ID",(object)PolicyId.ToString(),SqlDbType.Int);
			objWrapper.AddParameter("@POLICY_VERSION_ID",(object)PolicyVersionId.ToString(),SqlDbType.Int);
			DataSet DSTemp = objWrapper.ExecuteDataSet("Proc_GetPolQuoteDetails");
			System.Collections.ArrayList  arrPremiumXml = new System.Collections.ArrayList();
			for(int i =0 ;i < DSTemp.Tables[0].Rows.Count ; i++)
			{
				string strPremiumXml = DSTemp.Tables[0].Rows[i]["QUOTE_XML"].ToString();
				string strQuoteType = DSTemp.Tables[0].Rows[i]["QUOTE_TYPE"].ToString();
				PremiumDetails objPremiumdetails = new PremiumDetails();
				
				objPremiumdetails.PremiumXML=strPremiumXml;
				objPremiumdetails.QuoteType=strQuoteType;
				arrPremiumXml.Add(objPremiumdetails); 
			}
			objWrapper.ClearParameteres();
			return arrPremiumXml;
		}

		private System.Collections.ArrayList  SplitPremiums(int CustomerId,int AppId,int AppVersionId,int PolicyId,int PolicyVersionId,string ProcessType,DataWrapper objWrapper)
		{
			bool retVal = false;
			System.Collections.ArrayList  arrPremiumXml = new System.Collections.ArrayList();
			DataSet DSTemp = new DataSet();
			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@CUSTOMER_ID",(object)CustomerId.ToString(),SqlDbType.Int);
			if (AppId == 0 && AppVersionId == 0)
			{
				objWrapper.AddParameter("@POLICY_ID",(object)PolicyId.ToString(),SqlDbType.Int);
				objWrapper.AddParameter("@POLICY_VERSION_ID",(object)PolicyVersionId.ToString(),SqlDbType.Int);
				DSTemp = objWrapper.ExecuteDataSet("Proc_GetPolQuoteDetails");
			}
			else
			{
				objWrapper.AddParameter("@APP_ID",(object)AppId.ToString(),SqlDbType.Int);
				objWrapper.AddParameter("@APP_VERSION_ID",(object)AppVersionId.ToString(),SqlDbType.Int);
				DSTemp = objWrapper.ExecuteDataSet("Proc_GetQuoteDetails");
			}
			
			for(int i =0 ;i < DSTemp.Tables[0].Rows.Count ; i++)
			{
				string strPremiumXml = DSTemp.Tables[0].Rows[i]["QUOTE_XML"].ToString();
				string strQuoteType = DSTemp.Tables[0].Rows[i]["QUOTE_TYPE"].ToString();
				PremiumDetails objPremiumdetails = new PremiumDetails();
				
				objPremiumdetails.PremiumXML=strPremiumXml;
				objPremiumdetails.QuoteType=strQuoteType;
				arrPremiumXml.Add(objPremiumdetails); 

				retVal = SplitPremiums(CustomerId,AppId,AppVersionId,PolicyId,PolicyVersionId,strPremiumXml,ProcessType,objWrapper);
			}
			objWrapper.ClearParameteres();
			return arrPremiumXml;
		}

		
		private bool SplitPremiums(int CustomerId,int AppId,int AppVersionId,int PolicyId,int PolicyVersionId,string PremiumXML,string ProcessType,DataWrapper objWrapper)
		{
			DataSet DsTemp = new DataSet();
			string SplitUniqeId = "0";
			PremiumXML = PremiumXML.Replace("<?xml-stylesheet type='text/xsl' href='FinalXML.xsl'?><!DOCTYPE people [<!ATTLIST homepage xlink:type CDATA #FIXED 'simple' xmlns:xlink CDATA #FIXED 'http://www.w3.org/1999/xlink'> <!ATTLIST person id ID #IMPLIED>]>","");
			XmlDocument objPremium = new XmlDocument();
			objPremium.LoadXml(PremiumXML);
			string strXPATH = @"PRIMIUM/RISK";
			foreach(XmlNode Node in objPremium.SelectNodes(strXPATH))
			{
				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@CUSTOMER_ID",CustomerId,SqlDbType.Int);
				objWrapper.AddParameter("@APP_ID",AppId,SqlDbType.Int);
				objWrapper.AddParameter("@APP_VERSION_ID",AppVersionId,SqlDbType.Int);
				objWrapper.AddParameter("@POLICY_ID",PolicyId ,SqlDbType.Int);
				objWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionId,SqlDbType.Int);
				objWrapper.AddParameter("@RISK_ID",(object)GetAttributeValue(Node,"ID").ToString().Trim(),SqlDbType.Int);
				objWrapper.AddParameter("@RISK_TYPE",(object)GetAttributeValue(Node,"TYPE").ToString().Trim(),SqlDbType.NVarChar);
				objWrapper.AddParameter("@PROCESS_TYPE",ProcessType,SqlDbType.NVarChar);

				DsTemp = objWrapper.ExecuteDataSet("Proc_InsertPremiumSplit");

				SplitUniqeId = DsTemp.Tables[0].Rows[0]["UNIQUE_ID"].ToString();

				foreach(XmlNode StepNode in Node.SelectNodes("STEP[@COMPONENT_TYPE!='' and @STEPPREMIUM!='' and @STEPPREMIUM!='0']"))
				{
					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@SPLIT_UNIQUE_ID",(object)SplitUniqeId.ToString(),SqlDbType.Int);
					objWrapper.AddParameter("@COMPONENT_TYPE",(object)GetAttributeValue(StepNode,"COMPONENT_TYPE").ToString().Trim(),SqlDbType.NVarChar);
					objWrapper.AddParameter("@COMPONENT_CODE",(object)GetAttributeValue(StepNode,"COMPONENT_CODE").ToString().Trim(),SqlDbType.NVarChar);
					objWrapper.AddParameter("@LIMIT",(object)GetAttributeValue(StepNode,"LIMITVALUE").ToString().Trim(),SqlDbType.NVarChar);
					objWrapper.AddParameter("@DEDUCTIBLE",(object)GetAttributeValue(StepNode,"DEDUCTIBLEVALUE").ToString().Trim(),SqlDbType.NVarChar);
					objWrapper.AddParameter("@PREMIUM",(object)GetAttributeValue(StepNode,"STEPPREMIUM").ToString().Trim(),SqlDbType.NVarChar);
					objWrapper.AddParameter("@REMARKS",(object)GetAttributeValue(StepNode,"COMPONENT_REMARKS").ToString().Trim(),SqlDbType.NVarChar);
					objWrapper.AddParameter("@EXCEPTION_XML",(object)StepNode.OuterXml.ToString().Trim(),SqlDbType.NVarChar);
					
					objWrapper.ExecuteDataSet("Proc_InsertPremiumSplitDetails");
				}
			}
			return(true);
		}

		private string GetAttributeValue(XmlNode Node, string AttributeName)
		{
			for (int AttributeCounter = 0; AttributeCounter < Node.Attributes.Count;AttributeCounter++)
			{
				XmlAttribute XmlAttrib= Node.Attributes[AttributeCounter];
				if (XmlAttrib.Name.ToUpper() == AttributeName.ToUpper())
				{	
					return XmlAttrib.Value;
				}
			}
			return "";
		}*/

		public enum enumPROC_PRINT_OPTIONS
		{
			COMMIT_PRINT_SPOOL = 11980,
			NOT_REQUIRED = 11981,
			DOWNLOAD = 11984,
			MICHIGAN_MAILERS=11985 
		}
		public enum enumPROC_PRINT_OPTIONS_CANCEL
		{
			COMMIT_PRINT_SPOOL = 13001,
			NOT_REQUIRED	= 13002,
			DOWNLOAD		=	13003,
			ON_DEMAND		=	13004,
			MICHIGAN_MAILERS =	13035
		}
		public enum enumPROC_CANCEL_TYPE
		{
			AGENCY_TERMINATE_NOTIFICATION		=13026, //13027;//13026  Agency Terminated/Notification // 
			AGENCY_TERMINATE_NO_NOTIFICATION	=13027 ,// 13027 'Agency Terminated/No Notification'
			NON_RENEWAL							=11991 //"11991">Non Renewal
		}

		public enum enumDIARY_RULES_VERIFIED
		{
			RULES_VERIFICATION_IMAGE = 1, //Process comitted with rules being violated
			RENEWAL_OTHER = 2 //Renewal case
		}
		public enum enumCANCELLATION_OPTIONS
		{
			PRO_RATA = 11994,
			FLAT = 11995,
			EQUITY = 11996,
			NOT_APPLICABLE = 13028
		}
		protected string mSystemID; 

		public string SystemID 
		{
			get 
			{
				return mSystemID; 
			}
			set
			{
				mSystemID = value; 
			}
		}

		public string strHTML(int CUSTOMER_ID,int POLICY_ID,int POLICY_VERSION_ID,out bool valid,out string strRulesStatus,string strCalledFrom)
		{
			Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules objRules = new Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules(mSystemID);
			string strCSSNo="",polLobID="",strHTML="";
			
			try
			{
				Cms.CmsWeb.cmsbase objBase = new Cms.CmsWeb.cmsbase();
				
				strCSSNo =objBase.GetColorScheme();;polLobID = objBase.GetLOBID();

                strHTML = objRules.VerifyPolicy(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, polLobID, out valid, strCSSNo, out strRulesStatus, strCalledFrom);			
				return strHTML;
			}
			catch(Exception ex)
			{
                throw new Exception(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1646",""), ex); //"Error at rules verification" //iTrack No 990
			}
			
		}

        public string rein_Install(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID)
        {
            try
			{
            string result = "";   
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            //string strStoredProc = "PROC_GET_POLICY_REIN_INSLT_DETAILS";
            objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
            objDataWrapper.AddParameter("@POLICY_ID", POLICY_ID);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
            DataSet ds = objDataWrapper.ExecuteDataSet("PROC_GET_POLICY_REIN_INSLT_DETAILS");
                  
               result = ds.Tables[0].Rows[0]["RESULT"].ToString();          
            return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
			
        }

        

		// Added by Mohit Agarwal
		// 18-Jan-2007
//		public static string strHTMLMandatory(int CUSTOMER_ID,int POLICY_ID,int POLICY_VERSION_ID,out bool valid,out string strRulesStatus)
//		{
//			Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules objRules = new Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules();
//			string strCSSNo="",polLobID="",strHTML="";
//			
//			try
//			{
//				Cms.CmsWeb.cmsbase objBase = new Cms.CmsWeb.cmsbase();
//				
//				strCSSNo =objBase.GetColorScheme();;polLobID = objBase.GetLOBID();
//						
//				strHTML=objRules.VerifyPolicyMandatory(CUSTOMER_ID,POLICY_ID ,POLICY_VERSION_ID,polLobID,out valid,strCSSNo,out strRulesStatus);			
//				return strHTML;
//			}
//			catch(Exception ex)
//			{
//				throw ex;
//			}
//			
//		}
	}
}
