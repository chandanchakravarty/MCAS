/*=============================================================================================
<Author					: -   Ashwani
<Start Date				: -	  13 Dec. 2006
<End Date				: -	  Base class for ACORD Xml parser.
<Description			: -   
<Review Date			: -   
<Reviewed By			: - 	
 Modification History
<Modified Date			: - kranti	
<Modified By			: - 03 jan 2007
<Purpose				: - 
=============================================================================================*/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using Cms.Model.Client;
using Cms.Model;
using Cms.Model.Application;
using Cms.Model.Application.HomeOwners;
//using Cms.Model.Application.Watercrafts;
using Cms.Model.Application.PrivatePassenger;
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlClient;
using Cms.BusinessLayer.BlApplication.HomeOwners;
using Cms.DataLayer;
using Cms.ExceptionPublisher.ExceptionManagement;


namespace Cms.BusinessLayer.BlQuote.AcordXMLParser
{
	/// <summary>
	/// Summary description for ClsBaseAcordXmlParser.
	/// </summary>
	public abstract class ClsBaseAcordXmlParser:Cms.BusinessLayer.BlCommon.ClsCommon
	{
		#region Variables Declaration
		//Xml Document
		protected XmlDocument myXmlDocument;
		protected XmlDocument qqXmlDocument;
		protected XmlNode dataNode;
		protected XmlNode currentNode;
		protected XmlNodeList nodList;
		protected XmlNode covNode;
		protected XmlNode nodHealthCare;
		//Model Object Of Customer
		public ClsCustomerInfo objClsCustomerInfo;		
		//Model Object Of App		
		public ClsGeneralInfo objClsGeneralInfo;
		//Model object of geninfo
		public ClsPPGeneralInformationInfo  objPPGeneralInformation;
		//Model Object of CoApplicant Info
		public ClsApplicantInsuedInfo ObjApplicantInsued;
		public const string ApplicantInfo = "PersPolicy/PersApplicationInfo";
		public const string PriorOrOtherPolicy = "PersPolicy/OtherOrPriorPolicy";
		public const string InsuredOrPrincipal = "InsuredOrPrincipal";
		public const string PersPolicy = "PersPolicy";

		public string strAgencyIds="";
		protected DataWrapper objDataWrapper;	
		private const int SUB_LOB_ID_FOR_TRAILBLAZER =1;
	
		#endregion
		

		protected string mSystemID ; 

		public ClsBaseAcordXmlParser()
		{
			objDataWrapper =new DataWrapper(ClsCommon.ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
		}	


		#region 
		public string MakeApplication(string GUID, string agencyID)
		{	
			try
			{
				if(AgencyExists(agencyID))
				{
					string ApplicationNumber = LoadAcordXML(GUID,agencyID);
					if(ApplicationNumber != "")
					{
						return "Application already created for this quote, Application Number : " + ApplicationNumber ; 
					}
					else
					{
						Parse();
						SaveRiskInfo(); //Virtual implement in Derived class
						UpdateAcordDetails(GUID);//update Acord XML table 
						return this.objClsGeneralInfo.APP_NUMBER.ToString() ;

						//Commented by Ravindra(04-24-2009)
						//						+ '/'
						//						+ this.objClsGeneralInfo.APP_ID + '/'
						//						+ this.objClsGeneralInfo.APP_VERSION_ID + '/'
						//						+ this.objClsGeneralInfo.CUSTOMER_ID + '/';
					}
				}
				else
				{
					return "noAgency";
				}
			}
			catch(Exception objExc)
			{
				System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
				addInfo.Add("Err Descriptor ","Error while Making Application.");
				addInfo.Add("GUID" ,GUID);
				addInfo.Add("Agency Code",agencyID);
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExc,addInfo);
			}
			return "";
		}
		#region CHECK AGENCY
		public bool AgencyExists(string agencyID)
		{
			try
			{
				if(agencyID!="")
				{
					ClsAcord ObjAcord = new ClsAcord(mSystemID);
					string appStatus  = ObjAcord.CheckAgencyExists(agencyID);
					if(appStatus == "2")
						return(true);
					else
						return(false);

				}
				

			}
			catch(Exception ex)
			{
				throw ex;
			}
			return(true);

		}
		#endregion

		public void  Parse()
		{
			try
			{
				ParseCustomerInfo(); // Base Class 
				AddCustomer();  //Base Class Concrete
				//FetchandUpdateCustomerInsurancScore();
				ParseCoApplicantInfo();
				AddCoApplicantInfo();
				ParseApplicationInfo();//Base Class Concrete
				SaveApp();//Base Class Concrete	
				ParseUWInfo();
				SaveUW();
				ParseRiskInfo();//Derived Implemented		
			}
			catch(Exception objExc)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExc);
			}
		}


		public abstract void ParseRiskInfo();

		public abstract void SaveRiskInfo();		

		// Parse Customer Info from Acord
		protected void ParseCustomerInfo()
		{
			//No of request in the XML
			XmlNodeList InsuranceSvcRq = GetApplicationNodeList();
			foreach(XmlNode node in InsuranceSvcRq)
			{
				XmlNodeList appNodeList = node.SelectNodes("PersAutoPolicyQuoteInqRq");		
				foreach(XmlNode nodeApp in appNodeList)
				{
					ParseInsuredOrPrincipal(nodeApp);
				}
			}			
		}
		// Parse Co Applicant Info from Acord
		protected void ParseCoApplicantInfo()
		{
			//No of request in the XML
			try
			{
				XmlNodeList InsuranceSvcRq = GetApplicationNodeList();
				foreach(XmlNode node in InsuranceSvcRq)
				{
					XmlNodeList appNodeList = node.SelectNodes("PersAutoPolicyQuoteInqRq");		
					foreach(XmlNode nodeApp in appNodeList)
					{
						ParseInsuredParseCoApplicant(nodeApp);
					}
				}
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(Cms.DataLayer.DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -2 ;
			}
		}
		//Parse Application Info from Acord
		protected void ParseApplicationInfo()
		{
			//No of request in the XML
			try
			{
				XmlNodeList InsuranceSvcRq = GetApplicationNodeList();
				foreach(XmlNode node in InsuranceSvcRq)
				{
					XmlNodeList appNodeList = node.SelectNodes("PersAutoPolicyQuoteInqRq");		
					foreach(XmlNode nodeApp in appNodeList)
					{
						ParsePersPolicy(nodeApp);				
					}
				}	
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(Cms.DataLayer.DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -2 ;
			}
		}	
        	
		//Parse UW Info from Acord
		protected void ParseUWInfo()
		{
			//No of request in the XML
			try
			{
				XmlNodeList InsuranceSvcRq = GetApplicationNodeList();
				foreach(XmlNode node in InsuranceSvcRq)
				{
					XmlNodeList appNodeList = node.SelectNodes("PersAutoPolicyQuoteInqRq");		
					foreach(XmlNode nodeApp in appNodeList)
					{
						ParsePersUW(nodeApp);				
					}
				}	
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(Cms.DataLayer.DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -2 ;
			}
		}	
		#endregion
		#region Insurance Score
		void FetchandUpdateCustomerInsurancScore()
		{
		/*	if(this.objClsCustomerInfo.CustomerInsuranceScore==-1)
			{
				CreditScoreDetails objScore = new CreditScoreDetails();			
				System.Collections.Specialized.NameValueCollection dic = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationSettings.GetConfig("IIXSettings");
				string strUserName = dic["UserName"].ToString();
				string strPassword = dic["Password"].ToString();
				string strAccountNumber = dic["AccountNumber"].ToString();
				string strUrl = dic["URL"].ToString();
				string strCustDetail="",strCustInsudetails="";
				string[] strCustInsuDetails=new string[0];
				string CmsWebURL = "";
				CmsWebURL = ClsCommon.GetKeyValue("CmsWebUrl"); 
				strCustDetail=objClsCustomerInfo.CustomerLastName+"^"+objClsCustomerInfo.CustomerFirstName+"^"+objClsCustomerInfo.CustomerMiddleName+"^"+objClsCustomerInfo.CustomerAddress1+"^"+objClsCustomerInfo.CustomerCity+"^"+objClsCustomerInfo.CustomerZip+"^"+objClsCustomerInfo.CustomerStateCode+"^"+objClsCustomerInfo.CustomerHomePhone+"^"+objClsCustomerInfo.CustomerSuffix+"^"+objClsCustomerInfo.SSN_NO;
				try
				{
					WscCmsWeb.wscmsweb ObjServices = new WscCmsWeb.wscmsweb(CmsWebURL);
					strCustInsudetails = ObjServices.GetCapitalCustomerCreditScore(strUserName,strPassword,strAccountNumber,strUrl,strCustDetail);
					if(strCustInsudetails.IndexOf('^')!=-1)
						strCustInsuDetails = strCustInsudetails.Split('^');
					else if(strCustInsudetails=="-2")
					{
						this.objClsCustomerInfo.CustomerInsuranceScore= -2;
						this.objClsCustomerInfo.CustomerInsuranceReceivedDate = DateTime.Now;
						//AddRequestLog("5291","INSURANCE SCORE",strCustInsudetails,"");
						return;
					}
					else if(strCustInsudetails=="-1")
					{
						this.objClsCustomerInfo.CustomerInsuranceScore= -1;
						this.objClsCustomerInfo.CustomerInsuranceReceivedDate = DateTime.Now;
						//AddRequestLog("5291","INSURANCE SCORE",strCustInsudetails,"");
						return;
					}					
				}
				catch(Exception ex)
				{
					//AddRequestLog("5291","INSURANCE SCORE",ex.Message,"");
					//throw(ex);
					return;
				}
			
				if(strCustInsuDetails[0]!=null)
					this.objClsCustomerInfo.CustomerInsuranceScore=int.Parse(strCustInsuDetails[0].ToString());
				this.objClsCustomerInfo.CustomerInsuranceReceivedDate = DateTime.Now;
				if(strCustInsuDetails[1]!=null)
					this.objClsCustomerInfo.CustomerReasonCode = strCustInsuDetails[1].ToString();
				if(strCustInsuDetails[2]!=null)
					this.objClsCustomerInfo.CustomerReasonCode2 = strCustInsuDetails[2].ToString();
				if(strCustInsuDetails[3]!=null)
					this.objClsCustomerInfo.CustomerReasonCode3 = strCustInsuDetails[3].ToString();
				if(strCustInsuDetails[4]!=null)
					this.objClsCustomerInfo.CustomerReasonCode4 = strCustInsuDetails[4].ToString();
				ClsCustomer objBLCustomer = new ClsCustomer();	
				//int resultVal=objBLCustomer.SetCapitalInsuranceScore(objClsCustomerInfo,objDataWrapper);
				return;		
			}
			*/
		}
		private void AddRequestLog(string RequestID, string RequestDetails, string ResponseMessage, string AdditionalMessage)
		{
			//Cms.DataLayer.DataWrapper objWrapper = new Cms.DataLayer.DataWrapper(ClsCommon.ConnStr ,CommandType.StoredProcedure);
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@INSURANCE_SVC_RQ",RequestID);
			objDataWrapper.AddParameter("@REQUEST_DATETIME",DateTime.Now );
			objDataWrapper.AddParameter("@REQUEST_DETAILS",RequestDetails);
			objDataWrapper.AddParameter("@RETURN_MESSAGE",ResponseMessage);
			objDataWrapper.AddParameter("@ADDITIONAL_INFO",AdditionalMessage);
			objDataWrapper.ExecuteNonQuery("Proc_AddRealTimeLog");
			objDataWrapper.ClearParameteres();
			//objDataWrapper.Dispose();
		}
		#endregion
		#region Save
		// Save Customer Info in database
		protected void AddCustomer()
		{
            objDataWrapper.ClearParameteres();
			ClsCustomer objBLCustomer = new ClsCustomer();			
			objBLCustomer.TransactionLogRequired = false;
			//We have to add agency info later 
			//objClsCustomerInfo.CustomerAgencyId = this.objAgency.AGENCY_ID;
			try
			{
				int intCustomerID = objBLCustomer.CheckCustomerExistence(objClsCustomerInfo,objDataWrapper);			
				objDataWrapper.ClearParameteres();			
				objClsCustomerInfo.CustomerCountry = "1";
				objClsCustomerInfo.LAST_UPDATED_DATETIME = DateTime.Now;
				objClsCustomerInfo.CREATED_DATETIME = DateTime.Now;			

				objClsCustomerInfo.CustomerAddress1=ReplaceEsqapeXmlCharacters(objClsCustomerInfo.CustomerAddress1);
				objClsCustomerInfo.CustomerAddress2=ReplaceEsqapeXmlCharacters(objClsCustomerInfo.CustomerAddress2);
				objClsCustomerInfo.CustomerBusinessPhone=ReplaceEsqapeXmlCharacters(objClsCustomerInfo.CustomerBusinessPhone);
				objClsCustomerInfo.CustomerBusinessType=ReplaceEsqapeXmlCharacters(objClsCustomerInfo.CustomerBusinessType);
				objClsCustomerInfo.CustomerCity=ReplaceEsqapeXmlCharacters(objClsCustomerInfo.CustomerCity);
				objClsCustomerInfo.CustomerCode=ReplaceEsqapeXmlCharacters(objClsCustomerInfo.CustomerCode);
				objClsCustomerInfo.CustomerContactName=ReplaceEsqapeXmlCharacters(objClsCustomerInfo.CustomerContactName);
				objClsCustomerInfo.CustomerCountry=ReplaceEsqapeXmlCharacters(objClsCustomerInfo.CustomerCountry);
				objClsCustomerInfo.CustomerFirstName=ReplaceEsqapeXmlCharacters(objClsCustomerInfo.CustomerFirstName);
				objClsCustomerInfo.CustomerHomePhone=ReplaceEsqapeXmlCharacters(objClsCustomerInfo.CustomerHomePhone);
				objClsCustomerInfo.CustomerLastName=ReplaceEsqapeXmlCharacters(objClsCustomerInfo.CustomerLastName);
				objClsCustomerInfo.CustomerMiddleName=ReplaceEsqapeXmlCharacters(objClsCustomerInfo.CustomerMiddleName);
				objClsCustomerInfo.CustomerState=ReplaceEsqapeXmlCharacters(objClsCustomerInfo.CustomerState);
				objClsCustomerInfo.CustomerStateCode=ReplaceEsqapeXmlCharacters(objClsCustomerInfo.CustomerStateCode);
				objClsCustomerInfo.EMPLOYER_ADD1=ReplaceEsqapeXmlCharacters(objClsCustomerInfo.EMPLOYER_ADD1);
				objClsCustomerInfo.EMPLOYER_ADD2=ReplaceEsqapeXmlCharacters(objClsCustomerInfo.EMPLOYER_ADD2);
				objClsCustomerInfo.EMPLOYER_ADDRESS=ReplaceEsqapeXmlCharacters(objClsCustomerInfo.EMPLOYER_ADDRESS);
				objClsCustomerInfo.EMPLOYER_CITY=ReplaceEsqapeXmlCharacters(objClsCustomerInfo.EMPLOYER_CITY);
				objClsCustomerInfo.EMPLOYER_NAME=ReplaceEsqapeXmlCharacters(objClsCustomerInfo.EMPLOYER_NAME);
				objClsCustomerInfo.EMPLOYER_STATE=ReplaceEsqapeXmlCharacters(objClsCustomerInfo.EMPLOYER_STATE);

				if ( intCustomerID == -1 )// new 
				{
					string firstName = objClsCustomerInfo.CustomerFirstName;					
					string lastName =  objClsCustomerInfo.CustomerLastName;

					//Insert
					if ( objClsCustomerInfo.CustomerType == "11110" )
					{
					
						if ( firstName.Length > 2 && lastName.Length > 2 )
						{
							objClsCustomerInfo.CustomerCode = objClsCustomerInfo.CustomerFirstName.Substring(0,2)  + objClsCustomerInfo.CustomerLastName.Substring(0,2) + "000001";
						}
					}
														
					int custID = objBLCustomer.AddCustomer(this.objClsCustomerInfo,objDataWrapper);
					this.objClsCustomerInfo.CustomerId = custID;

				}
				else
				{
					//Update
					objClsCustomerInfo.CustomerType = "11110";
					this.objClsCustomerInfo.CustomerId = intCustomerID;
					objBLCustomer.UpdateCustomer(objClsCustomerInfo,objDataWrapper);
				}
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(Cms.DataLayer.DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -2 ;
			}
			//return 1;	
		}
			 // Save CoApplicant Info in database
			 protected void AddCoApplicantInfo()
			 {
				 objDataWrapper.ClearParameteres();
				 //ClsCustomer objBLCustomer = new ClsCustomer();	
				 ClsApplicantInsued  ObjAddApplicationInsured = new ClsApplicantInsued();
				 //ClsApplicantInsued.TransactionLogRequired = false;
				 //We have to add agency info later 
				 //objClsCustomerInfo.CustomerAgencyId = this.objAgency.AGENCY_ID;
				 try
				 {
					// int intCustomerID = objBLCustomer.CheckCustomerExistence(objClsCustomerInfo,objDataWrapper);			
					// objDataWrapper.ClearParameteres();			
					 //objClsCustomerInfo.CustomerCountry = "1";
					 ObjApplicantInsued.LAST_UPDATED_DATETIME = DateTime.Now;
					 ObjApplicantInsued.CREATED_DATETIME = DateTime.Now;			

					 //if ( intCustomerID == -1 )// new 
					 //{
						// string firstName = objClsCustomerInfo.CustomerFirstName;					
						// string lastName =  objClsCustomerInfo.CustomerLastName;

						 //Insert
//						 if ( objClsCustomerInfo.CustomerType == "11110" )
//						 {
//					
//							 if ( firstName.Length > 2 && lastName.Length > 2 )
//							 {
//								 objClsCustomerInfo.CustomerCode = objClsCustomerInfo.CustomerFirstName.Substring(0,2)  + objClsCustomerInfo.CustomerLastName.Substring(0,2) + "000001";
//							 }
//						 }

						 //int applID = objBLCustomer.AddCustomer(this.objClsCustomerInfo,objDataWrapper);
						ObjApplicantInsued.ADDRESS1=ReplaceEsqapeXmlCharacters(ObjApplicantInsued.ADDRESS1);
						ObjApplicantInsued.ADDRESS2=ReplaceEsqapeXmlCharacters(ObjApplicantInsued.ADDRESS2);
						ObjApplicantInsued.CITY=ReplaceEsqapeXmlCharacters(ObjApplicantInsued.CITY);
						ObjApplicantInsued.CO_APPLI_EMPL_ADDRESS=ReplaceEsqapeXmlCharacters(ObjApplicantInsued.CO_APPLI_EMPL_ADDRESS);
						ObjApplicantInsued.CO_APPLI_EMPL_ADDRESS1=ReplaceEsqapeXmlCharacters(ObjApplicantInsued.CO_APPLI_EMPL_ADDRESS1);
						ObjApplicantInsued.CO_APPLI_EMPL_CITY=ReplaceEsqapeXmlCharacters(ObjApplicantInsued.CO_APPLI_EMPL_CITY);
						ObjApplicantInsued.CO_APPLI_EMPL_COUNTRY=ReplaceEsqapeXmlCharacters(ObjApplicantInsued.CO_APPLI_EMPL_COUNTRY);
						ObjApplicantInsued.CO_APPLI_EMPL_NAME=ReplaceEsqapeXmlCharacters(ObjApplicantInsued.CO_APPLI_EMPL_NAME);
						ObjApplicantInsued.CO_APPLI_EMPL_STATE=ReplaceEsqapeXmlCharacters(ObjApplicantInsued.CO_APPLI_EMPL_STATE);
						ObjApplicantInsued.COUNTRY=ReplaceEsqapeXmlCharacters(ObjApplicantInsued.COUNTRY);
						ObjApplicantInsued.CUSTOMER_CODE=ReplaceEsqapeXmlCharacters(ObjApplicantInsued.CUSTOMER_CODE);
						ObjApplicantInsued.CUSTOMER_FIRST_NAME=ReplaceEsqapeXmlCharacters(ObjApplicantInsued.CUSTOMER_FIRST_NAME);
						ObjApplicantInsued.CUSTOMER_LAST_NAME=ReplaceEsqapeXmlCharacters(ObjApplicantInsued.CUSTOMER_LAST_NAME);
						ObjApplicantInsued.CUSTOMER_MIDDLE_NAME=ReplaceEsqapeXmlCharacters(ObjApplicantInsued.CUSTOMER_MIDDLE_NAME);
						ObjApplicantInsued.FIRST_NAME=ReplaceEsqapeXmlCharacters(ObjApplicantInsued.FIRST_NAME);
						ObjApplicantInsued.LAST_NAME=ReplaceEsqapeXmlCharacters(ObjApplicantInsued.LAST_NAME);
						ObjApplicantInsued.MIDDLE_NAME=ReplaceEsqapeXmlCharacters(ObjApplicantInsued.MIDDLE_NAME);
						ObjApplicantInsued.STATE=ReplaceEsqapeXmlCharacters(ObjApplicantInsued.STATE);
					    int applID =  ObjAddApplicationInsured.AddCoApplicant(ObjApplicantInsued,objDataWrapper);	
						// this.objClsCustomerInfo.CustomerId = custID;

					// }
//					 else
//					 {
//						 //Update
//						 objClsCustomerInfo.CustomerType = "11110";
//						 this.objClsCustomerInfo.CustomerId = intCustomerID;
//						 objBLCustomer.UpdateCustomer(objClsCustomerInfo,objDataWrapper);
//					 }
				 }
				 catch(Exception ex)
				 {
					 objDataWrapper.RollbackTransaction(Cms.DataLayer.DataWrapper.CloseConnection.YES);
					 throw(ex);
					 //return -2 ;
				 }
				 //return 1;	
			 }
		//Save Application Info in Database
		protected void SaveApp()
		{	
			objDataWrapper.ClearParameteres();
			ClsGeneralInformation objClsGeneralInformation = new ClsGeneralInformation();		
			int appID;
			int appVersionID;
		
			objClsGeneralInfo.COUNTRY_ID = 1;
			objClsGeneralInfo.CUSTOMER_ID = objClsCustomerInfo.CustomerId;
			// we hv to send agency id later
			//objClsGeneralInfo.APP_AGENCY_ID = objClsCustomerInfo.AGENCY_ID;
		
			try
			{
				int retVal = objClsGeneralInformation.CheckApplicationExistence(objClsGeneralInfo,objDataWrapper,out appID,out appVersionID);
				objDataWrapper.ClearParameteres();		
				
				objClsGeneralInfo.APP_STATUS = "Incomplete";
				objClsGeneralInfo.CREATED_DATETIME = DateTime.Now;
				objClsGeneralInfo.LAST_UPDATED_DATETIME = DateTime.Now;				
				
				if ( retVal == -1 )
				{
					objClsGeneralInfo.APP_VERSION_ID = 1;
					int val = objClsGeneralInformation.Add(objClsGeneralInfo,objDataWrapper);	
				}
				else
				{
					//Update 
					this.objClsGeneralInfo.APP_ID= appID;
					this.objClsGeneralInfo.APP_VERSION_ID= appVersionID;
					//objClsGeneralInformation.UpdateApp(objClsGeneralInfo,objDataWrapper);
				}
				
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(Cms.DataLayer.DataWrapper.CloseConnection.YES);
				throw(ex);					
			}			
		}
		protected void SaveUW()
		{	
			objDataWrapper.ClearParameteres();
			ClsGeneralInformation objClsGeneralInformation = new ClsGeneralInformation();		
			int appID;
			int appVersionID;
		
			objClsGeneralInfo.COUNTRY_ID = 1;
			objClsGeneralInfo.CUSTOMER_ID = objClsCustomerInfo.CustomerId;
			// we hv to send agency id later
			//objClsGeneralInfo.APP_AGENCY_ID = objClsCustomerInfo.AGENCY_ID;
		
			try
			{
				int retVal = objClsGeneralInformation.CheckApplicationExistence(objClsGeneralInfo,objDataWrapper,out appID,out appVersionID);
				objDataWrapper.ClearParameteres();		
				
				objClsGeneralInfo.APP_STATUS = "Incomplete";
				objClsGeneralInfo.CREATED_DATETIME = DateTime.Now;
				objClsGeneralInfo.LAST_UPDATED_DATETIME = DateTime.Now;				
				
				if ( retVal == -1 )
				{
					objClsGeneralInfo.APP_VERSION_ID = 1;
					int val = objClsGeneralInformation.AddUWGEN(objClsGeneralInfo,objPPGeneralInformation,objDataWrapper);
				}
				else
				{
					//Update 
					//this.objClsGeneralInfo.APP_ID= appID;
					//this.objClsGeneralInfo.APP_VERSION_ID= appVersionID;
					//objClsGeneralInformation.UpdateApp(objClsGeneralInfo,objDataWrapper);
				}
				
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(Cms.DataLayer.DataWrapper.CloseConnection.YES);
				throw(ex);					
			}			
		}
				 
		/// <summary>
		/// update acord detail with custId,appid,appversion and stateid./
		/// </summary>
		/// <param name="GUID">Insurance Reference ID in Acord</param>
		/// <returns></returns>
		protected void UpdateAcordDetails(string GUID)
		{
			try
			{
				int appID=0,appVersionID=0,customerID=0,stateID=0;
				
				customerID =objClsCustomerInfo.CustomerId;
				appID = objClsGeneralInfo.APP_ID;
				appVersionID=objClsGeneralInfo.APP_VERSION_ID;		

				stateID = GetStateCode(objClsGeneralInfo.STATE_CODE);
				ClsAcord objAcord = new ClsAcord(mSystemID);
				objAcord.UpdateAppDetailsInAcordDetails(customerID,appID,appVersionID,stateID,GUID,objDataWrapper);
				
			}
			catch (Exception exc)
			{
				objDataWrapper.RollbackTransaction(Cms.DataLayer.DataWrapper.CloseConnection.YES);
				System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
				addInfo.Add("Err Descriptor ","Error while Updating Acord Details.");
				addInfo.Add("GUID" ,GUID);
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc,addInfo);
				throw(exc);
			}
			finally
			{}
		}
		#endregion

		#region Parse

		protected XmlNodeList GetApplicationNodeList()
		{
			XmlNodeList InsuranceSvcRq = myXmlDocument.DocumentElement.SelectNodes("InsuranceSvcRq");
			return InsuranceSvcRq;
		}


		/// <summary>
		/// Customer Info
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public void ParseInsuredOrPrincipal(XmlNode node)
		{
			objClsCustomerInfo = new ClsCustomerInfo();							
			XmlNode objNode = node.SelectSingleNode(InsuredOrPrincipal);

			if ( objNode == null )
			{
				throw new Exception("InsuredOrPrincipal node not found in ACORD XML.");
			}
					
			bool isPersonal = false;
			bool isCommercial = false;

			if(strAgencyIds !="")
			{
				objClsCustomerInfo.CustomerAgencyId = int.Parse(strAgencyIds);

			}
			//Getting the Personal name of customer/////////////
			currentNode = objNode.SelectSingleNode("GeneralPartyInfo/NameInfo/PersonName");

			if ( currentNode != null )
			{
				dataNode = currentNode.SelectSingleNode("GivenName");
				if ( dataNode != null )
				{	
					objClsCustomerInfo.CustomerFirstName = dataNode.InnerText;
				}

				dataNode = currentNode.SelectSingleNode("OtherGivenName");
				if ( dataNode != null )
				{	
					objClsCustomerInfo.CustomerMiddleName = dataNode.InnerText;
				}
			
				dataNode = currentNode.SelectSingleNode("Surname");
				if ( dataNode != null )
				{	
					objClsCustomerInfo.CustomerLastName = dataNode.InnerText;
				}
								
			}
			currentNode = objNode.SelectSingleNode("GeneralPartyInfo/NameInfo/TaxIdentity");
			if(currentNode != null)
			{
				dataNode = currentNode.SelectSingleNode("TaxIdTypeCd");
				string strSSN = "";
				if(dataNode != null && dataNode.InnerText.Trim()== "SSN" )
				{	
					dataNode = currentNode.SelectSingleNode("TaxId");
					if(dataNode != null )
					{
						if(dataNode.InnerText.ToString() !="")
						{
							if(dataNode.InnerText.ToString().Length > 11)
							{
								strSSN = dataNode.InnerText.ToString().Substring(0,11);//11 DIgit format
								strSSN = Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(strSSN);
								objClsCustomerInfo.SSN_NO = strSSN.Trim();
							}
							else
							{
								objClsCustomerInfo.SSN_NO = Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(dataNode.InnerText.ToString().Trim());
							}

						}
						else
							objClsCustomerInfo.SSN_NO = null;		
					}
					else
						objClsCustomerInfo.SSN_NO =  null;						
				}
				else
					objClsCustomerInfo.SSN_NO =  null;				
			}

				 
			string commercialName = "";
			if (objClsCustomerInfo.CustomerLastName=="" && objClsCustomerInfo.CustomerFirstName =="")
			{
				//Getting Commercial Name 
				currentNode = objNode.SelectSingleNode("GeneralPartyInfo/NameInfo/CommlName");
				
				if ( currentNode != null )
				{
					dataNode = currentNode.SelectSingleNode("CommercialName");

					if ( dataNode != null)
					{
						commercialName = dataNode.InnerText.Trim();
						//objClsCustomerInfo.CustomerFirstName = commercialName;
					}					
				}
			}				
			if (objClsCustomerInfo.CustomerFirstName != "" && objClsCustomerInfo.CustomerLastName != "" )																				
			{
				isPersonal = true;
				objClsCustomerInfo.CustomerType = "11110";
			}
				
			if ( commercialName != "" )
			{
				objClsCustomerInfo.CustomerFirstName = commercialName;
				isCommercial = true;
				objClsCustomerInfo.CustomerType = "11109";	
			}

			if ( isPersonal && isCommercial )
			{
				throw new Exception("The Customer element has both Personal and Commercial name tags.");
			}
					
			if ( isPersonal )
			{
				if ( objClsCustomerInfo.CustomerFirstName == null || objClsCustomerInfo.CustomerFirstName == "" )
				{
					//System.Web.HttpContext.Current.Response.Write("<br>Customer First Name cannot be empty.");
					throw new Exception("Customer First Name cannot be empty.");
				}
			
				if ( objClsCustomerInfo.CustomerLastName == null || objClsCustomerInfo.CustomerLastName == "" )
				{
					//System.Web.HttpContext.Current.Response.Write("<br>Customer Last Name cannot be empty.");
					throw new Exception("Customer Last Name cannot be empty.");
				}
			}

			if ( isCommercial )
			{
				if ( commercialName == "" )
				{
					throw new Exception("Customer's Commercial name cannot be empty.");
				}
			}
				
			//Get the address details////////
			XmlNodeList nodeList = objNode.SelectNodes("GeneralPartyInfo/Addr");			
			foreach(XmlNode addrNode in nodeList)
			{
//				string addrType  = "";
//				
//				dataNode = addrNode.SelectSingleNode("AddrTypeCd");
//				
//				if ( dataNode != null )
//				{
//					addrType = dataNode.InnerText;
//				}

				//if ( addrType == "StreetAddress")
				//{
					XmlNode addrDataNode = addrNode.SelectSingleNode("Addr1");

					if ( addrDataNode != null )
					{
						objClsCustomerInfo.CustomerAddress1 = addrDataNode.InnerText;
					}
					
					addrDataNode = addrNode.SelectSingleNode("Addr2");
					
					if ( addrDataNode != null )
					{
						objClsCustomerInfo.CustomerAddress2 = addrDataNode.InnerText;
					}
					
					addrDataNode = addrNode.SelectSingleNode("City");
					
					if ( addrDataNode != null )
					{
						objClsCustomerInfo.CustomerCity = addrDataNode.InnerText;
					}

//					addrDataNode = addrNode.SelectSingleNode("StateProvCd");
//					
//					if ( addrDataNode != null )
//					{
//						objClsCustomerInfo.CustomerState = addrDataNode.InnerText;
//					}
					
					//converting state code to state id
					addrDataNode = addrNode.SelectSingleNode("StateProvCd");					
					if ( addrDataNode != null )
					{
						//int StateID = GetStateCode(addrDataNode.InnerText);
						//objClsCustomerInfo.CustomerState =  StateID.ToString();

						objClsCustomerInfo.CustomerState = addrDataNode.InnerText;
					}

					addrDataNode = addrNode.SelectSingleNode("PostalCode");					
					if ( addrDataNode != null )
					{
						objClsCustomerInfo.CustomerZip = addrDataNode.InnerText;
					}

					addrDataNode = addrNode.SelectSingleNode("CountryCd");
					
					if ( addrDataNode != null )
					{
						objClsCustomerInfo.CustomerCountry = addrDataNode.InnerText;
					}
				//}
			}
			//End of address

			//Get the phone details
			nodeList = objNode.SelectNodes("GeneralPartyInfo/Communications/PhoneInfo");
			
			foreach(XmlNode phoneNode in nodeList)
			{
				string phoneType  = "";
				
				dataNode = phoneNode.SelectSingleNode("PhoneTypeCd");
				
				if ( dataNode != null )
				{
					phoneType = dataNode.InnerText;
				}
				
				dataNode = phoneNode.SelectSingleNode("CommunicationsUseCd");
				
				string commType = "";
				if ( dataNode  != null  )
				{
					commType = dataNode.InnerText;
				}
				string strPhoneNo="";
				switch(phoneType)
				{
					case "Phone":
						if ( commType == "Home" )
						{
							dataNode = phoneNode.SelectSingleNode("PhoneNumber");

							if ( dataNode != null )
							{
								strPhoneNo=dataNode.InnerText;
								if(strPhoneNo.Length>=10)
								{
									strPhoneNo="("+strPhoneNo.Substring(0,3)+")"+strPhoneNo.Substring(3,3)+"-"+strPhoneNo.Substring(6,4);
								}
								objClsCustomerInfo.CustomerHomePhone = strPhoneNo;//dataNode.InnerText;
							}
						}
						
						if ( commType == "Business" )
						{
							dataNode = phoneNode.SelectSingleNode("PhoneNumber");

							if ( dataNode != null )
							{
								strPhoneNo=dataNode.InnerText;
								if(strPhoneNo.Length>=10)
								{
									strPhoneNo="("+strPhoneNo.Substring(0,3)+")"+strPhoneNo.Substring(3,3)+"-"+strPhoneNo.Substring(6,4);
								}
								objClsCustomerInfo.CustomerBusinessPhone = strPhoneNo;//dataNode.InnerText;
							}
						}
						break;
					case "Cell":
						if ( commType == "Home" )
						{
							dataNode = phoneNode.SelectSingleNode("PhoneNumber");

							if ( dataNode != null )
							{
								strPhoneNo=dataNode.InnerText;
								if(strPhoneNo.Length>=10)
								{
									strPhoneNo="("+strPhoneNo.Substring(0,3)+")"+strPhoneNo.Substring(3,3)+"-"+strPhoneNo.Substring(6,4);
								}
								objClsCustomerInfo.CustomerMobile = strPhoneNo;//dataNode.InnerText;
							}
						}

						break;
					case "Pager":
						if ( commType == "Home" )
						{
							dataNode = phoneNode.SelectSingleNode("PhoneNumber");

							if ( dataNode != null )
							{
								objClsCustomerInfo.CustomerPagerNo = dataNode.InnerText;
							}
						}
						break;
					case "Fax":
						if ( commType == "Home" )
						{
							dataNode = phoneNode.SelectSingleNode("PhoneNumber");

							if ( dataNode != null )
							{
								objClsCustomerInfo.CustomerFax = dataNode.InnerText;
							}
						}
						break;
				}				
				//Get the Email details
				nodeList = objNode.SelectNodes("GeneralPartyInfo/Communications/EmailInfo");
			
				foreach(XmlNode emailNode in nodeList)
				{
					dataNode = emailNode.SelectSingleNode("EmailAddr");

					if ( dataNode != null )
					{
						objClsCustomerInfo.CustomerEmail = dataNode.InnerText;
					}

				}
				
				//Website details
				nodeList = objNode.SelectNodes("GeneralPartyInfo/Communications/WebsiteInfo");
			
				foreach(XmlNode emailNode in nodeList)
				{
					dataNode = emailNode.SelectSingleNode("WebsiteURL");

					if ( dataNode != null )
					{
						objClsCustomerInfo.CustomerWebsite = dataNode.InnerText;
					}
				}

			}
			//End of communications
				
			//Get Customer ID if it exists//////////////////////////
			currentNode = objNode.SelectSingleNode("InsuredOrPrincipalInfo");

			if ( currentNode != null )
			{
				if ( currentNode.Attributes["id"] != null )
				{
					objClsCustomerInfo.CustomerId = Convert.ToInt32(currentNode.Attributes["id"].Value);
				}
			}
			
			if( currentNode != null )
			{
				dataNode = currentNode.SelectSingleNode("PersonInfo/GenderCd");
				if(dataNode!=null)
				{
					objClsCustomerInfo.GENDER = dataNode.InnerText;
				}
				dataNode = currentNode.SelectSingleNode("PersonInfo/MaritalStatusCd");
				if(dataNode!=null)
				{
					objClsCustomerInfo.MARITAL_STATUS = dataNode.InnerText;
				}
				dataNode = currentNode.SelectSingleNode("PersonInfo/BirthDt");
				if(dataNode!=null)
				{
					objClsCustomerInfo.DATE_OF_BIRTH = DefaultValues.GetDateFromString(dataNode.InnerText);
				}
				dataNode = currentNode.SelectSingleNode("PersonInfo/OccupationClassCd");
				if(dataNode!=null)
				{
					objClsCustomerInfo.APPLICANT_OCCU = dataNode.InnerText;
				}
			}
			//Customer Insurance Score 
			XmlNode objInsuNode = node.SelectSingleNode("PersPolicy/CreditScoreInfo");
			if ( objInsuNode != null )
			{
				dataNode = objInsuNode.SelectSingleNode("CreditScore");

				if ( dataNode != null )
				{
					//					if (objClsCustomerInfo.CustomerInsuranceScore != null )
					//					{
					string strinsuSc=dataNode.InnerText.Trim();
					bool IsNumeric = false;
					try
					{
						int iTest = Int32.Parse(strinsuSc);
						IsNumeric = true;
					}
					catch(Exception ex)
					{
						IsNumeric = false;
                        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
					}
					if(!IsNumeric)
					{
						if(strinsuSc.ToUpper().Trim()!="NO SCORE" && strinsuSc.ToUpper().Trim()!="NO HIT" && strinsuSc.ToUpper().Trim()!="751 OR HIGHER"  && strinsuSc.ToUpper().Trim()!="" )
						{
							strinsuSc=strinsuSc.Substring((strinsuSc.Length - 4),4);
						}
						else if(strinsuSc.ToUpper().Trim()=="751 OR HIGHER")
						{
							strinsuSc="751";
						}
					}
					if(strinsuSc.ToUpper().Trim()!="NO SCORE" && strinsuSc.ToUpper().Trim()!="NO HIT"  && strinsuSc.ToUpper().Trim()!="")
						objClsCustomerInfo.CustomerInsuranceScore = DefaultValues.GetDecimalFromString(strinsuSc.Trim());
					else if(strinsuSc.ToUpper().Trim()=="NO SCORE" || strinsuSc.ToUpper().Trim()=="NO HIT")
						objClsCustomerInfo.CustomerInsuranceScore = -2;
					//else
					//	objClsCustomerInfo.CustomerInsuranceScore = -1;
					//					}
				}

			}
			if(objClsCustomerInfo.CustomerInsuranceScore==-1)
			{
				string QQinsuScore="-1";
				if(qqXmlDocument.SelectSingleNode("QQ_XML/QUICKQUOTE/POLICY/INSURANCESCORE")!=null)
					QQinsuScore=qqXmlDocument.SelectSingleNode("QQ_XML/QUICKQUOTE/POLICY/INSURANCESCORE").InnerText.ToString();
				if(QQinsuScore=="NOHITNOSCORE")
					objClsCustomerInfo.CustomerInsuranceScore = -2;
				else if(QQinsuScore!="-1" && QQinsuScore!="NOHITNOSCORE")
					objClsCustomerInfo.CustomerInsuranceScore = int.Parse(QQinsuScore);
				
				if(qqXmlDocument.SelectSingleNode("QQ_XML/QUICKQUOTE/POLICY/REASONCODE1")!=null)
					objClsCustomerInfo.CustomerReasonCode=qqXmlDocument.SelectSingleNode("QQ_XML/QUICKQUOTE/POLICY/REASONCODE1").InnerText.Trim();
				if(qqXmlDocument.SelectSingleNode("QQ_XML/QUICKQUOTE/POLICY/REASONCODE2")!=null)
					objClsCustomerInfo.CustomerReasonCode2=qqXmlDocument.SelectSingleNode("QQ_XML/QUICKQUOTE/POLICY/REASONCODE2").InnerText.Trim();
				if(qqXmlDocument.SelectSingleNode("QQ_XML/QUICKQUOTE/POLICY/REASONCODE3")!=null)
					objClsCustomerInfo.CustomerReasonCode3=qqXmlDocument.SelectSingleNode("QQ_XML/QUICKQUOTE/POLICY/REASONCODE3").InnerText.Trim();
				if(qqXmlDocument.SelectSingleNode("QQ_XML/QUICKQUOTE/POLICY/REASONCODE4")!=null)
					objClsCustomerInfo.CustomerReasonCode4=qqXmlDocument.SelectSingleNode("QQ_XML/QUICKQUOTE/POLICY/REASONCODE4").InnerText.Trim();

			}
			if(qqXmlDocument.SelectSingleNode("QQ_XML/QUICKQUOTE/POLICY/CUSTOMERINSURANCERECEIVEDDATE")!=null)
			{
				if(qqXmlDocument.SelectSingleNode("QQ_XML/QUICKQUOTE/POLICY/CUSTOMERINSURANCERECEIVEDDATE").InnerText!="")
					objClsCustomerInfo.CustomerInsuranceReceivedDate=System.Convert.ToDateTime(qqXmlDocument.SelectSingleNode("QQ_XML/QUICKQUOTE/POLICY/CUSTOMERINSURANCERECEIVEDDATE").InnerText.Trim());
			}
			
			//return objClsCustomerInfo;
		}

		/// <summary>
		/// CoApplicant Info
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public void ParseInsuredParseCoApplicant(XmlNode node)
		{
			ObjApplicantInsued = new ClsApplicantInsuedInfo();
			XmlNode objNode = node.SelectSingleNode("InsuredOrPrincipal[@id='2']");
			XmlNode objAplntNode = node.SelectSingleNode(InsuredOrPrincipal);
			if( objNode == null) 
				return;	
					
			bool isPersonal = false;
			bool isCommercial = false;

			//Getting the Personal name of customer/////////////
			currentNode = objNode.SelectSingleNode("GeneralPartyInfo/NameInfo/PersonName");

			if ( currentNode != null )
			{
				dataNode = currentNode.SelectSingleNode("GivenName");
				if ( dataNode != null )
				{	
					ObjApplicantInsued.FIRST_NAME = dataNode.InnerText;
				}

				dataNode = currentNode.SelectSingleNode("OtherGivenName");
				if ( dataNode != null )
				{	
					ObjApplicantInsued.MIDDLE_NAME = dataNode.InnerText;
				}
			
				dataNode = currentNode.SelectSingleNode("Surname");
				if ( dataNode != null )
				{	
					ObjApplicantInsued.LAST_NAME = dataNode.InnerText;
				}
				ObjApplicantInsued.IS_ACTIVE = "Y";
								
			}
			currentNode = objNode.SelectSingleNode("GeneralPartyInfo/NameInfo/TaxIdentity");
			if(currentNode != null)
			{
				dataNode = currentNode.SelectSingleNode("TaxIdTypeCd");
				string strSSN = "";
				if(dataNode != null && dataNode.InnerText.Trim()== "SSN" )
				{	
					dataNode = currentNode.SelectSingleNode("TaxId");
					if(dataNode != null )
					{
						if(dataNode.InnerText.ToString() !="")
						{
							if(dataNode.InnerText.ToString().Length > 11)
							{
								strSSN = dataNode.InnerText.ToString().Substring(0,11);//11 DIgit format
								strSSN = Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(strSSN);
								ObjApplicantInsued.CO_APPL_SSN_NO = strSSN.Trim();
							}
							else
							{
								ObjApplicantInsued.CO_APPL_SSN_NO = Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(dataNode.InnerText.ToString().Trim());
							}

						}
						else
							ObjApplicantInsued.CO_APPL_SSN_NO = "0";		
					}
					else
						ObjApplicantInsued.CO_APPL_SSN_NO = "0";						
				}
				else
					ObjApplicantInsued.CO_APPL_SSN_NO = "0";				
			}

			// Get Personal Info 	 
			currentNode = objNode.SelectSingleNode("InsuredOrPrincipalInfo/PersonInfo");
			if(currentNode!=null)
			{
				// Gender
				dataNode = currentNode.SelectSingleNode("GenderCd");
				if(dataNode!=null)
				{
					ObjApplicantInsued.CO_APPL_GENDER=dataNode.InnerText;
				}
				// Marital Status
				dataNode = currentNode.SelectSingleNode("MaritalStatusCd");
				if(dataNode!=null)
				{
					ObjApplicantInsued.CO_APPL_MARITAL_STATUS=dataNode.InnerText;
				}
				// Occupation Class
				dataNode = currentNode.SelectSingleNode("OccupationClassCd");
				if(dataNode!=null)
				{
					ObjApplicantInsued.CO_APPLI_OCCU=dataNode.InnerText;
				}
				// Birth date
				dataNode = currentNode.SelectSingleNode("BirthDt");
				if(dataNode!=null)
				{
					ObjApplicantInsued.CO_APPL_DOB=DefaultValues.GetDateFromString(dataNode.InnerText);
				}
			}
			string commercialName = "";
			if (ObjApplicantInsued.LAST_NAME=="" && ObjApplicantInsued.FIRST_NAME =="")
			{
				//Getting Commercial Name 
				currentNode = objNode.SelectSingleNode("GeneralPartyInfo/NameInfo/CommlName");
				
				if ( currentNode != null )
				{
					dataNode = currentNode.SelectSingleNode("CommercialName");

					if ( dataNode != null)
					{
						commercialName = dataNode.InnerText.Trim();
						//objClsCustomerInfo.CustomerFirstName = commercialName;
					}					
				}
			}				
			if (ObjApplicantInsued.LAST_NAME=="" && ObjApplicantInsued.FIRST_NAME =="" )																				
			{
				isPersonal = true;
				//ObjApplicantInsued.CustomerType = "11110";
			}
				
			if ( commercialName != "" )
			{
				//objClsCustomerInfo.CustomerFirstName = commercialName;
				isCommercial = true;
				//objClsCustomerInfo.CustomerType = "11109";	
			}

			if ( isPersonal )
			{
				if (ObjApplicantInsued.LAST_NAME=="" && ObjApplicantInsued.FIRST_NAME =="")
				{
					//System.Web.HttpContext.Current.Response.Write("<br>Customer First Name cannot be empty.");
					throw new Exception("Co Applicant First Name cannot be empty.");
				}
			
				if (ObjApplicantInsued.LAST_NAME=="" && ObjApplicantInsued.FIRST_NAME =="")
				{
					//System.Web.HttpContext.Current.Response.Write("<br>Customer Last Name cannot be empty.");
					throw new Exception("Co Applicant  Last Name cannot be empty.");
				}
			}

			//Get the address details////////
			XmlNodeList nodeList = objAplntNode.SelectNodes("GeneralPartyInfo/Addr");			
			foreach(XmlNode addrNode in nodeList)
			{
				//				string addrType  = "";
				//				
				//				dataNode = addrNode.SelectSingleNode("AddrTypeCd");
				//				
				//				if ( dataNode != null )
				//				{
				//					addrType = dataNode.InnerText;
				//				}

				//if ( addrType == "StreetAddress")
				//{
				XmlNode addrDataNode = addrNode.SelectSingleNode("Addr1");

				if ( addrDataNode != null )
				{
					ObjApplicantInsued.ADDRESS1 = addrDataNode.InnerText;
				}
					
				addrDataNode = addrNode.SelectSingleNode("Addr2");
					
				if ( addrDataNode != null )
				{
					ObjApplicantInsued.ADDRESS2 = addrDataNode.InnerText;
				}
					
				addrDataNode = addrNode.SelectSingleNode("City");
					
				if ( addrDataNode != null )
				{
					ObjApplicantInsued.CITY = addrDataNode.InnerText;
				}

				//					addrDataNode = addrNode.SelectSingleNode("StateProvCd");
				//					
				//					if ( addrDataNode != null )
				//					{
				//						objClsCustomerInfo.CustomerState = addrDataNode.InnerText;
				//					}
					
				//converting state code to state id
				addrDataNode = addrNode.SelectSingleNode("StateProvCd");					
				if ( addrDataNode != null )
				{
					//int StateID = GetStateCode(addrDataNode.InnerText);
					//objClsCustomerInfo.CustomerState =  StateID.ToString();
					if(addrDataNode.InnerText.ToUpper()=="MI")
						ObjApplicantInsued.STATE = "22";
					else if(addrDataNode.InnerText.ToUpper()=="IN")
						ObjApplicantInsued.STATE = "14";
				}

				addrDataNode = addrNode.SelectSingleNode("PostalCode");					
				if ( addrDataNode != null )
				{
					ObjApplicantInsued.ZIP_CODE = addrDataNode.InnerText;
				}

				addrDataNode = addrNode.SelectSingleNode("CountryCd");
					
				if ( addrDataNode != null )
				{
					ObjApplicantInsued.COUNTRY = addrDataNode.InnerText;
				}
				//}
			}
			//End of address

			//Get the phone details
			nodeList = objNode.SelectNodes("GeneralPartyInfo/Communications/PhoneInfo");
			
			foreach(XmlNode phoneNode in nodeList)
			{
				string phoneType  = "";
				
				dataNode = phoneNode.SelectSingleNode("PhoneTypeCd");
				
				if ( dataNode != null )
				{
					phoneType = dataNode.InnerText;
				}
				
				dataNode = phoneNode.SelectSingleNode("CommunicationsUseCd");
				
				string commType = "";
				if ( dataNode  != null  )
				{
					commType = dataNode.InnerText;
				}

				switch(phoneType)
				{
					case "Phone":
						if ( commType == "Home" )
						{
							dataNode = phoneNode.SelectSingleNode("PhoneNumber");

							if ( dataNode != null )
							{
								objClsCustomerInfo.CustomerHomePhone = dataNode.InnerText;
							}
						}
						
						if ( commType == "Business" )
						{
							dataNode = phoneNode.SelectSingleNode("PhoneNumber");

							if ( dataNode != null )
							{
								objClsCustomerInfo.CustomerBusinessPhone = dataNode.InnerText;
							}
						}
						break;
					case "Cell":
						if ( commType == "Home" )
						{
							dataNode = phoneNode.SelectSingleNode("PhoneNumber");

							if ( dataNode != null )
							{
								objClsCustomerInfo.CustomerMobile = dataNode.InnerText;
							}
						}

						break;
					case "Pager":
						if ( commType == "Home" )
						{
							dataNode = phoneNode.SelectSingleNode("PhoneNumber");

							if ( dataNode != null )
							{
								objClsCustomerInfo.CustomerPagerNo = dataNode.InnerText;
							}
						}
						break;
					case "Fax":
						if ( commType == "Home" )
						{
							dataNode = phoneNode.SelectSingleNode("PhoneNumber");

							if ( dataNode != null )
							{
								objClsCustomerInfo.CustomerFax = dataNode.InnerText;
							}
						}
						break;
				}				
				//Get the Email details
				nodeList = objNode.SelectNodes("GeneralPartyInfo/Communications/EmailInfo");
			
				foreach(XmlNode emailNode in nodeList)
				{
					dataNode = emailNode.SelectSingleNode("EmailAddr");

					if ( dataNode != null )
					{
						objClsCustomerInfo.CustomerEmail = dataNode.InnerText;
					}

				}
				
				//Website details
				nodeList = objNode.SelectNodes("GeneralPartyInfo/Communications/WebsiteInfo");
			
				foreach(XmlNode emailNode in nodeList)
				{
					dataNode = emailNode.SelectSingleNode("WebsiteURL");

					if ( dataNode != null )
					{
						objClsCustomerInfo.CustomerWebsite = dataNode.InnerText;
					}
				}

			}
			//End of communications
				
			//Get Customer ID if it exists//////////////////////////
			currentNode = objNode.SelectSingleNode("InsuredOrPrincipalInfo");

			if ( currentNode != null )
			{
				if ( currentNode.Attributes["id"] != null )
				{
					ObjApplicantInsued.APPLICANT_ID = Convert.ToInt32(currentNode.Attributes["id"].Value);
				}
			}
			if(objClsCustomerInfo.CustomerId.ToString()!="")
				ObjApplicantInsued.CUSTOMER_ID=objClsCustomerInfo.CustomerId;
			//return objClsCustomerInfo;
		}

		/// <summary>
		///Fatch State code	from database on the base of state id 
		/// </summary>
		/// <param name="StateID">state id </param>
		/// <returns></returns>		
		public int  GetStateCode(string StateProvCd )
		{
			string strStoredProc	=	"Proc_GetStateCode";			
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,System.Data.CommandType.StoredProcedure);
			try
			{
				objDataWrapper.AddParameter("@StateProvCd",StateProvCd);	
				DataSet objDataSet = new DataSet();	
				int Result = 0;
					objDataSet = objDataWrapper.ExecuteDataSet(strStoredProc);
				 	Result = System.Convert.ToInt32(objDataSet.Tables[0].Rows[0][0]);
				objDataWrapper.ClearParameteres();	
				return Result;		
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}		
		}	
		//end  GetStateCode


		/// <summary>
		/// parse the application nodes
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public void ParsePersPolicy(XmlNode node)
		{
			objClsGeneralInfo = new ClsGeneralInfo();			
			nodHealthCare = node.SelectSingleNode("PersAutoLineBusiness/com.brics_HealthCareProvider");
			//Get the application details
			XmlNode objNode = node.SelectSingleNode("PersPolicy");
				
			if ( objNode == null )
			{
				throw new Exception("PersPolicy node not found in ACORD XML.");
			}

			dataNode = objNode.SelectSingleNode("PolicyNumber");
			if ( dataNode != null )
			{
				objClsGeneralInfo.APP_NUMBER = dataNode.InnerText.Trim();
			}
			
			dataNode = objNode.SelectSingleNode("PolicyVersion");

			if ( dataNode != null )
			{
				objClsGeneralInfo.APP_VERSION = dataNode.InnerText.Trim();
			}
				
			dataNode = objNode.SelectSingleNode("LOBCd");
			if ( dataNode != null )
			{
				objClsGeneralInfo.APP_LOB = dataNode.InnerText.Trim();
			}

			//sublob ..if qualifies trailblazer
			dataNode = objNode.SelectSingleNode("SubLOBCd");
			if ( dataNode != null )
			{
				string trailBlazer = dataNode.InnerText.Trim();
				if (trailBlazer.ToUpper().Trim() == "TRUE")
				{
					objClsGeneralInfo.APP_SUBLOB = SUB_LOB_ID_FOR_TRAILBLAZER.ToString().Trim();
				}

			}
			if(strAgencyIds != null)
			{
				objClsGeneralInfo.APP_AGENCY_ID=int.Parse(strAgencyIds);
			}
			dataNode = objNode.SelectSingleNode("PolicyStatusCd");

			if ( dataNode != null )
			{
				objClsGeneralInfo.APP_STATUS = dataNode.InnerText.Trim();
			}
			
			dataNode = objNode.SelectSingleNode("ControllingStateProvCd");

			if ( dataNode != null )
			{
				objClsGeneralInfo.STATE_CODE = dataNode.InnerText.Trim();
			}
			
			dataNode = objNode.SelectSingleNode("OriginalInceptionDt");

			if ( dataNode != null )
			{
				if ( dataNode.InnerText != "" )
				{
					//objClsGeneralInfo.APP_INCEPTION_DATE = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
				}
			}

			currentNode = objNode.SelectSingleNode("ContractTerm");
			
			if ( currentNode != null )
			{
				dataNode = currentNode.SelectSingleNode("EffectiveDt");

				if ( dataNode != null )
				{
					if ( dataNode.InnerText != "" )
					{
						objClsGeneralInfo.APP_EFFECTIVE_DATE = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
						objClsGeneralInfo.APP_INCEPTION_DATE = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
					}
				}
					
				// Here we have to calculate exp date
				currentNode = objNode.SelectSingleNode("ContractTerm/DurationPeriod");
				if ( currentNode != null )
				{
					dataNode = currentNode.SelectSingleNode("NumUnits");
					if ( dataNode != null )
					{						
						objClsGeneralInfo.APP_EXPIRATION_DATE = DateTime.Parse(objClsGeneralInfo.APP_EFFECTIVE_DATE.ToString()).AddMonths( int.Parse(dataNode.InnerText.Trim()));//DefaultValues.GetDateFromString(dataNode.InnerText.Trim());	
					}
				}
				//				dataNode = currentNode.SelectSingleNode("ExpirationDt");
				//	
				//				if ( dataNode != null )
				//				{
				//					if ( dataNode.InnerText != "" )
				//					{
				//						objClsGeneralInfo.APP_EXPIRATION_DATE = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
				//					}
				//				}
			}		
			// Here we have to calculate exp date
//			currentNode = objNode.SelectSingleNode("OtherOrPriorPolicy/ContractTerm");
//			if ( currentNode != null )
//			{
//				dataNode = currentNode.SelectSingleNode("ExpirationDt");
//				if ( dataNode != null )
//				{
//					objClsGeneralInfo.APP_EXPIRATION_DATE = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());	
//				}
//			}
			//Insurance score/Credit score
			currentNode = objNode.SelectSingleNode("CreditScoreInfo");

			if ( currentNode != null )
			{
				dataNode = currentNode.SelectSingleNode("CreditScore");

				if ( dataNode != null )
				{
					//					if (objClsCustomerInfo.CustomerInsuranceScore != null )
					//					{
					string strinsuSc=dataNode.InnerText.Trim();
					bool IsNumeric = false;
					try
					{
						int iTest = Int32.Parse(strinsuSc);
						IsNumeric = true;
					}
					catch(Exception ex)
					{
						IsNumeric = false;
                        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
					}
					if(!IsNumeric)
					{
						if(strinsuSc.ToUpper().Trim()!="NO SCORE" && strinsuSc.ToUpper().Trim()!="NO HIT" && strinsuSc.ToUpper().Trim()!="751 OR HIGHER"  && strinsuSc.ToUpper().Trim()!="" )
						{
							strinsuSc=strinsuSc.Substring((strinsuSc.Length - 4),4);
						}
						else if(strinsuSc.ToUpper().Trim()=="751 OR HIGHER")
						{
							strinsuSc="751";
						}
					}
					if(strinsuSc.ToUpper().Trim()!="NO SCORE" && strinsuSc.ToUpper().Trim()!="NO HIT"  && strinsuSc.ToUpper().Trim()!="")
						objClsCustomerInfo.CustomerInsuranceScore = DefaultValues.GetDecimalFromString(strinsuSc.Trim());
					else if(strinsuSc.ToUpper().Trim()=="NO SCORE" || strinsuSc.ToUpper().Trim()=="NO HIT")
						objClsCustomerInfo.CustomerInsuranceScore = -2;
					else
						objClsCustomerInfo.CustomerInsuranceScore = -1;
						//					}
				}

			}
			//Perform validations
			if ( objClsGeneralInfo.APP_LOB == null || objClsGeneralInfo.APP_LOB == "" )
			{
					
				throw new Exception("Application LOB cannot be empty in XML.");
			}
			
			if ( objClsGeneralInfo.APP_EFFECTIVE_DATE == DateTime.MinValue )
			{
					
				throw new Exception("Application Effective date cannot be empty in XML.");
			}

			if ( objClsGeneralInfo.APP_EXPIRATION_DATE == DateTime.MinValue )
			{
					
				throw new Exception("Application Expiration Date cannot be empty in XML.");
			}			
		}


		#endregion 
		# region Fetch UW Acord
		public void ParsePersUW(XmlNode node)
		{
			objPPGeneralInformation = new ClsPPGeneralInformationInfo();
			//Get the application details
			XmlNode objNode = node.SelectSingleNode("PersPolicy");
			// years Continously insured
			dataNode = objNode.SelectSingleNode("OtherOrPriorPolicy/ContractTerm/ContinuousInd");
			if ( dataNode != null )
			{
				string ContnousIns = dataNode.InnerText.Trim();
				if (ContnousIns.Trim() !="")
				{
					ContnousIns= System.Convert.ToString(int.Parse(ContnousIns)/12);
					if(int.Parse(ContnousIns)>0)
						objPPGeneralInformation.YEARS_INSU = int.Parse(ContnousIns);
				}

			}
			// years Continously insured with Wolverine
			dataNode = objNode.SelectSingleNode("OtherOrPriorPolicy/ContractTerm/YearsContinuousIndWolverine");
			if ( dataNode != null )
			{
				string ContnousInsWolv= dataNode.InnerText.Trim();
				if (ContnousInsWolv.Trim() !="")
				{
					//ContnousInsWolv=System.Convert.ToString(int.Parse(ContnousInsWolv)/12);
					if(int.Parse(ContnousInsWolv)>0)
						objPPGeneralInformation.YEARS_INSU_WOL = int.Parse(ContnousInsWolv);
				}
			}
			// Have to Map with acord xml
			objPPGeneralInformation.ANY_NON_OWNED_VEH="0";
			objPPGeneralInformation.ANY_NON_OWNED_VEH="0";
			objPPGeneralInformation.CAR_MODIFIED="0";
			objPPGeneralInformation.EXISTING_DMG="0";
			objPPGeneralInformation.ANY_CAR_AT_SCH="0";
			objPPGeneralInformation.ANY_OTH_AUTO_INSU="0";
			objPPGeneralInformation.ANY_OTH_INSU_COMP="0";
			objPPGeneralInformation.H_MEM_IN_MILITARY="0";
			objPPGeneralInformation.DRIVER_SUS_REVOKED="0";
			objPPGeneralInformation.PHY_MENTL_CHALLENGED="0";
			objPPGeneralInformation.ANY_FINANCIAL_RESPONSIBILITY="0";
			objPPGeneralInformation.INS_AGENCY_TRANSFER="0";
			objPPGeneralInformation.COVERAGE_DECLINED="0";
			objPPGeneralInformation.AGENCY_VEH_INSPECTED="0";
			objPPGeneralInformation.USE_AS_TRANSPORT_FEE="0";
			objPPGeneralInformation.SALVAGE_TITLE="0";
			objPPGeneralInformation.ANY_ANTIQUE_AUTO="0";
			objPPGeneralInformation.IS_ACTIVE="Y";
			//Multi Policy Discount
			dataNode = objNode.SelectSingleNode("OtherOrPriorPolicy/Coverage/OtherOrPriorPolicy/PolicyCd");
			if ( dataNode != null )
			{
				objPPGeneralInformation.MULTI_POLICY_DISC_APPLIED="1";
				if(objNode.SelectSingleNode("OtherOrPriorPolicy/Coverage/OtherOrPriorPolicy/PolicyNumber")!=null)
					objPPGeneralInformation.MULTI_POLICY_DISC_APPLIED_PP_DESC=objNode.SelectSingleNode("OtherOrPriorPolicy/Coverage/OtherOrPriorPolicy/PolicyNumber").InnerText.ToString().Trim();
			}
			else
			{
				objPPGeneralInformation.MULTI_POLICY_DISC_APPLIED="0";
			}
			objPPGeneralInformation.IS_COST_OVER_DEFINED_LIMIT="0";
			objPPGeneralInformation.CURR_RES_TYPE="0";
			//Prior Loss
			nodList = objNode.SelectNodes("AccidentViolation");
			string strPriorLos="";
			//if(dataNode!=null)
			//{
			foreach(XmlNode AccidentViolationNode in objNode.SelectNodes("AccidentViolation"))
			{	
				if(AccidentViolationNode.SelectSingleNode("AccidentViolationCd").InnerText.Trim()=="42100" || AccidentViolationNode.SelectSingleNode("AccidentViolationCd").InnerText.Trim()=="42110" || AccidentViolationNode.SelectSingleNode("AccidentViolationCd").InnerText.Trim()=="42120" || AccidentViolationNode.SelectSingleNode("AccidentViolationCd").InnerText.Trim()=="42130")
				{
					strPriorLos="Y";
				}
			}
			if(strPriorLos=="Y")
			{
				objPPGeneralInformation.ANY_PRIOR_LOSSES="1";
				objPPGeneralInformation.ANY_PRIOR_LOSSES_DESC="0";
			}
			else
			{
				objPPGeneralInformation.ANY_PRIOR_LOSSES="0";
			}
			objPPGeneralInformation.CREATED_DATETIME=DateTime.Now;

		}
		#endregion
		#region Fetch ACORD Xml
		
		private string LoadAcordXML(string GUID, string agencyID)
		{
			string ApplicationNumber ="";
			try
			{
				
				string strAcordXml = FetchAcordXML(GUID,agencyID,out ApplicationNumber);
				
				if(ApplicationNumber == "")
				{
					myXmlDocument = new XmlDocument();
					myXmlDocument.LoadXml(strAcordXml);
					//Fetch the QQ XML..Get the Node containing Vehicle Class per vehicle(Calculated Class).
					//Append the Node <RateClassCd>  per vehicle in Acord XML before Make Application
					string strQQXml = FetchQQXML(GUID);
					qqXmlDocument = new XmlDocument();
					qqXmlDocument.LoadXml(strQQXml);
					string strVehClass = "";
					const int PERSVEHID_ACORD = 7; 

					foreach (XmlNode vehNode in qqXmlDocument.SelectNodes("QQ_XML/QUICKQUOTE/VEHICLES/VEHICLE"))
					{
						int vehID = 0; 
						if(vehNode.Attributes["ID"]!=null)
						{
							vehID = int.Parse(vehNode.Attributes["ID"].Value.ToString());
							foreach(XmlNode acordvehNode in myXmlDocument.SelectNodes("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh"))
							{
								strVehClass = "";
								int vehAcordID = 0;
								if(acordvehNode.Attributes["id"]!=null)
								{
									string strVehID = acordvehNode.Attributes["id"].Value.ToString().Trim();
									string strID = strVehID.Substring(PERSVEHID_ACORD);
									vehAcordID = int.Parse(strID.ToString());
									if(vehID == vehAcordID)
									{
										strVehClass = vehNode.SelectSingleNode("VEHICLECLASS").InnerText.ToString();
										//**Creating New Nodes for Class Nodes (RateClassCd) in ACORD XML**//
										XmlElement classNode= myXmlDocument.CreateElement("RateClassCd");
										XmlText classText = myXmlDocument.CreateTextNode(strVehClass.ToString());
										acordvehNode.AppendChild(classNode);
										classNode.AppendChild(classText);

									}
								}
							}
						}
					}
				}
				
			}
			catch(Exception ex)
			{
				System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
				addInfo.Add("Err Descriptor ","Error while Loading Accord XML.");
				addInfo.Add("GUID" ,GUID);
				addInfo.Add("Agency Code",agencyID);
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo);
			}
			return ApplicationNumber ; 
		}


		/// <summary>
		/// Fetch the ACORD XML against the given GUID
		/// </summary>
		/// <param name="strINSURANCE_SVC_RQ"></param>
		/// <returns></returns>
		public string FetchAcordXML(string GUID, string agencyID, out string ApplicationNumber )
		{
			// This parm we have to send later on the basis of this GUID we will fetch acord xml from database
			//string strINSURANCE_SVC_RQ="7FBDB947-BF27-4F97-BB2B-F97EDB7C3941";
			string strStoredProc = "Proc_GetAcord_XML",strAcordXml="";

			DataSet myDataSet= null;
			ApplicationNumber = "";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,System.Data.CommandType.StoredProcedure);
			try
			{
				
				objDataWrapper.AddParameter("@INSURANCE_SVC_RQ", GUID);		
				SqlParameter objParam = (SqlParameter) objDataWrapper.AddParameter 
					("@APP_NUMBER", SqlDbType.VarChar ,ParameterDirection.Output);		
				objParam.Size = 20 ; 
		
				myDataSet	= objDataWrapper.ExecuteDataSet(strStoredProc);				
				objDataWrapper.ClearParameteres();
				
				if(myDataSet.Tables[0].Rows.Count>0)
				{
					strAcordXml=myDataSet.Tables[0].Rows[0]["ACORD_XML"].ToString();
					strAgencyIds=myDataSet.Tables[0].Rows[0]["AGENCY_ID"].ToString();
					strAcordXml=RemoveJunkXmlCharacters(strAcordXml);
					strAcordXml=Replace(strAcordXml);
					if(objParam != null && objParam.Value != DBNull.Value )
					{
						ApplicationNumber = objParam.Value.ToString();
					}
				}
				return strAcordXml;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}	
		}
		#region Load QQ XML
		/// <summary>
		/// Fetch the QQ XML against the given GUID
		/// </summary>
		/// <param name="strINSURANCE_SVC_RQ"></param>
		/// <returns></returns>
		public string FetchQQXML(string GUID)
		{
			// This parm we have to send later on the basis of this GUID we will fetch acord xml from database
			string strStoredProc = "Proc_GetQQ_XML",strAcordXml="";

			DataSet myDataSet= null;

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,System.Data.CommandType.StoredProcedure);
			try
			{
				
				objDataWrapper.AddParameter("@INSURANCE_SVC_RQ", GUID);								
				myDataSet	= objDataWrapper.ExecuteDataSet(strStoredProc);				
				objDataWrapper.ClearParameteres();
				
				if(myDataSet.Tables[0].Rows.Count>0)
				{
					strAcordXml=myDataSet.GetXml();
					strAcordXml=Replace(strAcordXml);
				}
				return strAcordXml;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}	
		}

		#endregion

		private string Replace(string strAcordXml)
		{
		
			strAcordXml=strAcordXml.Replace("&gt;",">");
			strAcordXml=strAcordXml.Replace("&lt;","<");
			strAcordXml=strAcordXml.Replace("\"","'");
			strAcordXml=strAcordXml.Replace("\r","");
			strAcordXml=strAcordXml.Replace("\n","");
			strAcordXml=strAcordXml.Replace("\t","");							
			strAcordXml=strAcordXml.Replace("<?","<!--?");
			strAcordXml=strAcordXml.Replace("?>","?-->");
			strAcordXml =strAcordXml.Replace("<?xml version='1.0' encoding='UTF-8'?>","");		
			strAcordXml =strAcordXml.Replace("<?xml version='1.0' encoding='utf-8' ?> ","");	
			strAcordXml=strAcordXml.Replace("<Table>","");
			strAcordXml=strAcordXml.Replace("<NewDataSet>","");
			strAcordXml=strAcordXml.Replace("</Table>","");
			strAcordXml=strAcordXml.Replace("</NewDataSet>","");
			strAcordXml=strAcordXml.Replace("<ACORD_XML>","");
			strAcordXml=strAcordXml.Replace("</ACORD_XML>","");
			return strAcordXml;			
		}


		
		#endregion
		public static string RemoveJunkXmlCharacters(string strNodeContent)
		{
			if(strNodeContent !=null && strNodeContent !="")
			{
				strNodeContent = strNodeContent.Replace("&","&amp;");
				//strNodeContent = strNodeContent.Replace("\"","&quot;");
				strNodeContent = strNodeContent.Replace("'","H673GSUYD7G3J73UDH");
			}
			return strNodeContent;
		}
		public static string ReplaceEsqapeXmlCharacters(string strNodeContent)
		{
			if(strNodeContent !=null && strNodeContent !="")
			{
				strNodeContent = strNodeContent.Replace("&amp;","&");
				//strNodeContent = strNodeContent.Replace("\"","&quot;");
				strNodeContent = strNodeContent.Replace("H673GSUYD7G3J73UDH","'");
			}
			return strNodeContent;
		}

	}
	public class CreditScoreDetails
	{
		public CreditScoreDetails()
		{
		}

		public int Score;
		public string FirstFactor = "";
		public string SecondFactor = "";
		public string ThirdFactor = "";
		public string FourthFactor = "";
	}

}
