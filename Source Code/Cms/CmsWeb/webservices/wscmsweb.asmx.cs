using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Text; 
using System.Xml; 
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlClient;
using Cms.BusinessLayer.BLClaims;
using Cms.DataLayer;
using System.Data.SqlClient;
using System.Web.Services.Protocols;


namespace Cms.CmsWeb.webservices
{
	//
	//CLASS
	//Provides various WebMethods for Manipulating XML and Generating SQL Queries
	// 
	
	public class wscmsweb: System.Web.Services.WebService 
	{
		public AuthenticationToken AuthenticationTokenHeader;
		private string authenticationKey = "";
	
		string SystemID ; 

		public wscmsweb()
		{			
			InitializeComponent();
            cmsbase obj = new cmsbase();
            SystemID = obj.getCarrierSystemID();//.CarrierSystemID;//System.Configuration.ConfigurationManager.AppSettings.Get("CarrierSystemID"); 
		}

		//Used to Generate Query from the Input XML(Sent from Concurrency Page for Overwriting Data when 
		//Concurrency is found and User wants to overwrite changes
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string OverWriteData(string conXML)
		{
			string strXML;
			conXML				=		Server.UrlDecode(conXML);
			try
			{
				if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
				{
					strXML			=		ClsCommon.OverWriteData(conXML);
				}
				else
					return(ClsCommon.ServiceAuthenticationMsg);

			}
			catch(Exception ex)
			{
				strXML			=		"";
			}
			return strXML;
		}
		
		//Used to Generate Queries from the Input XML for checking Concurrency. If found then selecting the current
		//data from the Database table else updating the Database table with the data sent through XML
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string CompareData(string compareXML)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				string compareData="";
				ClsWebServiceCommon dataCompare = new ClsWebServiceCommon();
				compareData=dataCompare.CompareData(compareXML);
				return compareData;
			}
			else				
				return(ClsCommon.ServiceAuthenticationMsg);

//			string strXML;
//			compareXML			=		Server.UrlDecode(compareXML);
//			try
//			{
//				strXML			=		ClsCommon.CompareData(compareXML);
//			}
//			catch(Exception ex)
//			{
//				strXML			=		"";
//			}
//			return strXML;
		}

		/// <summary>
		/// update or insert data into database. Implementation needs to be coded.
		/// </summary>
		/// <param name="SaveDataXML">XML string conating data to be saved</param>
		/// <returns>"true" or "false" depending save succeed or failed.</returns>
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string SaveData(string SaveDataXML)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				string saveData="";
				ClsWebServiceCommon dataSave = new ClsWebServiceCommon();
				saveData=dataSave.SaveData(SaveDataXML);
				return saveData;
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
//			string saveXML		=		Server.UrlDecode(SaveDataXML);
//			return "false";
		}


		/// <summary>
		/// The implimentation of this method needs to be coded.
		/// Method will accept screenId and deleteclause.
		/// screenId will be used to determine which businesslayer's method will be called
		/// </summary>
		/// <param name="screenId"></param>
		/// <param name="deleteClause"></param>
		/// <returns></returns>
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
			//public bool deleteRecord(string screenId, object[] deleteClause)
		public object deleteRecord(string className, string methodName, object[] arguments)
		{
			try
			{
				if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
				{
					object obj = System.Reflection.Assembly.Load("blcommon").CreateInstance(className);
					System.Type	typeClass				=	obj.GetType();
					System.Reflection.MethodInfo mi		=	typeClass.GetMethod(methodName);
					if(mi != null)
					{
						return mi.Invoke(obj,arguments);// to be set
					}
					else
					{
						return false;
					}
				}
				else
					return false;
			}
			catch(Exception obj)
			{
				return false;
			}
			/*
			ClsCommon objBL = new ClsCommon();		//Business layer object to apply business rule

			if(screenId == "0") // This is for TODOLIST
			{
				//return objBL.TempMethodDelete(deleteClause);
				ClsDiary objDiary	=	new ClsDiary();
				int[] dirayPKField	=	new int[deleteClause.Length];
				for(int i=0; i<deleteClause.Length; i++)
				{
					dirayPKField[i]	=	Convert.ToInt32(deleteClause[i]);
				}
				if(objDiary.DeleteDiaryEntry(dirayPKField) > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			} */
		}
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
			//public bool deleteRecord(string screenId, object[] deleteClause)
		public int deleteRecord1()
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				return 1;
			}
			else
				return 0;

			
		}
		/// <summary>
		/// This general method will be called from different place.
		/// According to the screen ID, and button Id supplied, appropriate 
		/// businesslayer's function will be called. Returns whether execution performed successfully or not
		/// </summary>
		/// <param name="screenId"></param>
		/// <param name="buttonId"></param>
		/// <returns></returns>
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public object generalClickHandler(string className, string methodName, object[] arguments)
		{
			System.Type	typeClass	=	System.Type.GetType("Cms.BusinessLayer.BlCommon.ClsDiary");
			System.Reflection.MethodInfo	mi	=	typeClass.GetMethod("SetCompleteTask");
			object oFetcher = Activator.CreateInstance(typeClass); 
			if(mi != null)
			{
				if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
				{
					return mi.Invoke(oFetcher,arguments);
				}
				else
					return null;
			}
			else
			{
				return false;
			}
			return false;
		}

		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public DataSet getGridResult(string selectClause,string fromClause,string whereclause,string searchClause,string orderbyClause,int iCacheSize)
		{
			try
			{
				if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
				{
					ClsCommon  loBl = new ClsCommon();
					int iStartRec=0;
					int iEndRec =iCacheSize;
				
					string psQuery = createQuery(selectClause,fromClause,whereclause,searchClause,orderbyClause,iCacheSize);
					return loBl.ExecuteInlineQuery(psQuery,iStartRec,iEndRec);
				}
				else
					return null;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private string createQuery(string selectClause,string fromClause,string whereClause,string searchClause,string orderbyClause,int intCacheSize)
		{
			bool bWhereAdded					=	false;					//If whereClause is not null or not blank
			StringBuilder sbSqlQuery			=	new	StringBuilder();	//Query to fetch data depending on maxrecord to fetch
			try
			{
				if (selectClause != null && selectClause.Trim() != "" && fromClause != null && fromClause != "")
				{
					sbSqlQuery.Append("SELECT ");
					if(intCacheSize > 0) sbSqlQuery.Append(" TOP " + intCacheSize.ToString()); 
					sbSqlQuery.Append(" " + selectClause + " FROM " + fromClause);
				}
				else
				{
					throw new Exception("From clause and select clause are necessary.");
				}
				if (whereClause != null && whereClause.Trim() != "")
				{
					sbSqlQuery.Append(" WHERE " + whereClause.Trim());
					bWhereAdded		=	true;
				}
				if (searchClause != null && searchClause.Trim() != "")
				{
					if (!bWhereAdded) 
					{
						sbSqlQuery.Append(" WHERE ");
					}
					else 
					{
						sbSqlQuery.Append(" AND ");
					}
					
					sbSqlQuery.Append(searchClause);
				}
				if (orderbyClause != null && orderbyClause.Trim() != "")
				{
					sbSqlQuery.Append(" ORDER BY " + orderbyClause.Trim());
				}
			}
			catch(Exception oEx)
			{
				throw oEx;
			}
			return sbSqlQuery.ToString();
		}
		/// <summary>
		/// This function is used to Delete the division
		/// </summary>
		/// <param name="DivisionId">Id of division to be deleted</param>
		/// <returns>True if successfull</returns>
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public bool DeleteDepartment(int intDeptId)
		{
			Cms.BusinessLayer.BlCommon.ClsDepartment objDept;
			objDept = new Cms.BusinessLayer.BlCommon.ClsDepartment();
				
			try
			{
				//Here we will create the object of clsDivsion class
				//and call its Delete method
				if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
				{
					return objDept.Delete(intDeptId);
				}
				else
					return false;

			}
			catch (Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
				return false;
			}
			finally
			{
				objDept.Dispose();
			}
		}

		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]
		public string GetQuoteXML(int Customer_ID,int App_ID,int App_Version_ID,int Dwelling_ID)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				string finalPremiumXML="";
				string inputXML	= "",premiumXML="";
				ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
				inputXML	=objGeneralInformation.GetInputXML(Customer_ID, App_ID, App_Version_ID);
		
				if(inputXML	!= "<NewDataSet />" && inputXML	!="")			
				{
					/* strReturnXML is of the form <INPUTXML><DWELLLINGDETAILS> ... </DWELLLINGDETAILS></INPUTXML>
					 * 1.  Run a loop to calculate the premium for each dwelling. 
					 * 2.  Save the output xml in another xmlstring. 
					 * 3.  add the <DWELLLINGDETAILS> tags for each premium.
					 * 4.  add the <PREMIUMXML> as the root node.
					 */ 
				
					string combinedPremiumComponent ="";

					XmlDocument xmlTempDoc = new XmlDocument();
				
					inputXML = "<INPUTXML>"+inputXML+"</INPUTXML>";
					xmlTempDoc.LoadXml(inputXML);
					XmlElement xmlTempElement = xmlTempDoc.DocumentElement;

					finalPremiumXML="<PREMIUMXML>";
					//foreach (XmlNode nodChildNode in xmlTempElement.ChildNodes)
					//{	
					foreach (XmlNode nodInput in xmlTempElement.ChildNodes)
					{						
						string premiumComponent="<" ;
						premiumComponent=premiumComponent + nodInput.Name.Trim() + " " + nodInput.Attributes[0].Name.Trim() + " = '"  + nodInput.Attributes[0].Value.Trim() +"' " + nodInput.Attributes[1].Name.Trim() + " = '"  + nodInput.Attributes[1].Value.Trim() + "' >" ;
						
						string premiumInput = premiumComponent + nodInput.InnerXml + "</"+nodInput.Name+">";
						QEngineDll.QuoteEngine strGetPath = new QEngineDll.QuoteEngine( premiumInput,Server.MapPath(@"\cms\cmsweb\XSL\Quote\ho\ho-3\Premium.xsl"),Server.MapPath(@"\cms\cmsweb\XSL\Quote\ho\ho-3\Input.xsl"));
						string shortestPath			  =	strGetPath.GetPath();
						premiumXML						  =	strGetPath.CalculatePremium(shortestPath.Trim());
						XmlDocument xmlTempPremiumXML = new XmlDocument();
						xmlTempPremiumXML.LoadXml(premiumXML);
						XmlElement xmlTempPremiumElement = xmlTempPremiumXML.DocumentElement;
						premiumComponent= premiumComponent +xmlTempPremiumElement.InnerXml + "</" + nodInput.Name + ">"; 
						combinedPremiumComponent = combinedPremiumComponent +  premiumComponent;
					}
					finalPremiumXML=finalPremiumXML + combinedPremiumComponent + "</PREMIUMXML>";
					//}
					//--------------------------------------------------------------------------------------------
				}
				return finalPremiumXML;
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
			

		}
		//Following WebMethod will fetch the territory based on ZIP code and LOB_ID and CUSTOMER_ID and APP_ID and APP_VERSION_ID
		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]
		public string FetchTerritoryForZipStateLob(string zipId,int lobId, int stateId,int intCustomerId,int intAppId,int intAppVersionId,string calledFrom,int intvehicleuse)
		{	
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				string territoryForZipStateLob="";
				ClsWebServiceCommon territoryZipStateLob = new ClsWebServiceCommon();
				territoryForZipStateLob=territoryZipStateLob.FetchTerritoryForZipStateLob(zipId,lobId,stateId,intCustomerId,intAppId,intAppVersionId,calledFrom,intvehicleuse);
				return territoryForZipStateLob;
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
//			try
//			{
//				DataSet dsTemp = new DataSet();
//				string result="";
//				DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
//				objDataWrapper.AddParameter("@ZIP_ID",zipId);
//				objDataWrapper.AddParameter("@LOB_ID",lobId);				
//				objDataWrapper.AddParameter("@STATE_ID",stateId);	
//				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
//				objDataWrapper.AddParameter("@APP_ID",intAppId);
//				objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId);
//				objDataWrapper.AddParameter("@CALLED_FROM" ,calledFrom);
//				objDataWrapper.AddParameter("@VEHICLE_USE" ,intvehicleuse);
//				dsTemp=objDataWrapper.ExecuteDataSet("Proc_FetchTerritoryForZipStateLob");
//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//				if (dsTemp.Tables[0].Rows.Count > 0)
//				{
//					result=(dsTemp.Tables[0].Rows[0][0]).ToString().Trim();
//				}
//				else
//				{	
//					result="";				
//				}
//				return result;				 
//				//string result=Cms.BusinessLayer.BlApplication.ClsVehicleInformation.FetchTerritoryForZip("1111",1).ToString();
//				//return result;
//			}
//			catch(Exception exc)
//			{return "";}
//			finally
//			{}	
		}

		//Following WebMethod will fetch the territory based on ZIP code and LOB_ID
		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]
		public string FetchTerritoryForZip(string zipId,int lobId)
		{		
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				string territoryForZip="";
				ClsWebServiceCommon territoryZip = new ClsWebServiceCommon();
				territoryForZip=territoryZip.FetchTerritoryForZip(zipId,lobId);
				return territoryForZip;
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
//			try
//			{
//				DataSet dsTemp = new DataSet();
//				string result="";
//				DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
//				objDataWrapper.AddParameter("@ZIP_ID",zipId);
//				objDataWrapper.AddParameter("@LOBID",lobId);				
//				dsTemp=objDataWrapper.ExecuteDataSet("Proc_FetchTerritoryForZip");
//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//				if (dsTemp.Tables[0].Rows.Count > 0)
//				{
//					result=(dsTemp.Tables[0].Rows[0][0]).ToString();
//				}
//				else
//				{	
//					result="";				
//				}
//				return result;				 
//				//string result=Cms.BusinessLayer.BlApplication.ClsVehicleInformation.FetchTerritoryForZip("1111",1).ToString();
//				//return result;
//			}
//			catch(Exception exc)
//			{return "";}
//			finally
//			{}	
		}
		//Following WebMethod will fetch the territory based on ZIP code and LOB_ID
		[WebMethod]
        [SoapHeader("AuthenticationTokenHeader")]
        public string FetchAgencyCSRProducer(int AgencyId, int LobID, string sSystemId)//LobID,sSystemId added by Charles on 28-May-2010
		{		
			string CSR="";
			try
			{
				if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
				{
					ClsUser objUser = new ClsUser();
                    DataTable dtCSRProducers = objUser.GetCSRProducers(AgencyId, LobID, sSystemId);
					CSR= dtCSRProducers.DataSet.GetXml(); 
					return CSR;
				}
				else
					return(ClsCommon.ServiceAuthenticationMsg);
			}
			catch
			{return "";}
			finally
			{}	
		}

		//Following WebMethod will fetch the County belonging to the specified state
		[WebMethod]
        [SoapHeader("AuthenticationTokenHeader")]
		public string FetchZipForState(int stateID, string ZipID)
		{		
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				string zipforState="";
				ClsWebServiceCommon ObjZip = new ClsWebServiceCommon();
				zipforState=ObjZip.FetchZipForState(stateID,ZipID);
				return zipforState;
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
//			try
//			{
//				DataSet dsTemp = new DataSet();
//				DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
//				objDataWrapper.AddParameter("@STATE_ID",stateID);	
//				objDataWrapper.AddParameter("@ZIP_ID",ZipID);	
//				dsTemp=objDataWrapper.ExecuteDataSet("Proc_FetchZipForState");
//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//				if(dsTemp.Tables[0].Rows[0][0].ToString()!="")
//				{
//					//return true;//result=(dsTemp.Tables[0].Rows[0][0]).ToString();
//					return dsTemp.Tables[0].Rows[0][0].ToString();
//				}
//				else
//				{	
//					return "";			
//				}
//			}
//			catch(Exception exc)
//			{return "";}
//			finally
//			{}	
		}
        //Following WebMethod will fetch the zip belonging to the specified state
		[WebMethod] [SoapHeader("AuthenticationTokenHeader")]
		public string FetchZipState(string ZipID,int stateID)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{		
				string zip="";
				ClsWebServiceCommon zipstate = new ClsWebServiceCommon();
				zip=zipstate.FetchZipState(ZipID,stateID);
				return zip;
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
//			try
//			{
//				DataSet dsTemp = new DataSet();
//				DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
//				objDataWrapper.AddParameter("@STATE_ID",stateID);	
//				objDataWrapper.AddParameter("@ZIP_ID",ZipID);	
//				dsTemp=objDataWrapper.ExecuteDataSet("Proc_FetchZipState");
//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//				if(dsTemp.Tables[0].Rows[0][0].ToString().Trim()!="")
//				{
//					//return true;//result=(dsTemp.Tables[0].Rows[0][0]).ToString();
//					return dsTemp.Tables[0].Rows[0][0].ToString().Trim();
//				}
//				else
//				{	
//					return "";			
//				}
//			}
//			catch(Exception exc)
//			{return "";}
//			finally
//			{}	
		}
		//Following method will fill the state for the county 
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string FillState(string CountyID)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{	
				string fillState="";
				ClsWebServiceCommon state = new ClsWebServiceCommon();
				fillState=state.FillState(CountyID);
				return fillState;
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
//			try
//			{
//				DataSet dsTemp = new DataSet();
//				string strSql = "Proc_GetStateListForCountry";
//				string result="";
//				DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
//				objDataWrapper.AddParameter("@COUNTRY_ID",CountyID);
//				DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//				if(objDataSet!=null && objDataSet.Tables.Count>0)
//				{ 
//					return objDataSet.GetXml();				 
//				}
//				else
//					return "";
//			}
//			catch(Exception exc)
//			{return "";}
//			finally
//			{}	
			
		}
		//Following method will fetch the county based on zip code
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string GetCountyForZip(string zip)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{	
				string countyZip="";
				ClsWebServiceCommon countyforZip = new ClsWebServiceCommon();
				countyZip=countyforZip.GetCountyForZip(zip);
				return countyZip;
			}
			else               				
				return(ClsCommon.ServiceAuthenticationMsg);


				//			try
				//			{
				//				DataSet dsTemp = new DataSet();
				//				string result="";
				//				DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				//				objDataWrapper.AddParameter("@ZIP_ID",zip);				
				//				dsTemp=objDataWrapper.ExecuteDataSet("Proc_GetCountyForZip");
				//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				//				if (dsTemp.Tables[0].Rows.Count > 0)
				//				{
				//					result=(dsTemp.Tables[0].Rows[0][0]).ToString();
				//				}
				//				else
				//				{	
				//					result="";				
				//				}
				//				return result;				 
				//				//string result=Cms.BusinessLayer.BlApplication.ClsVehicleInformation.FetchTerritoryForZip("1111",1).ToString();
				//				//return result;
				//			}
				//			catch(Exception exc)
				//			{return "";}
				//			finally
				//			{}	
			
			}
		//Following method will fetch the county based on zip code
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string GetViolations(int CUSTOMER_ID, int APP_ID, int APP_VERSION_ID,int VIOLATION_ID,string CALLED_FROM)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{	
				string violations="";
				ClsWebServiceCommon violation = new ClsWebServiceCommon();
				violations=violation.GetViolations(CUSTOMER_ID,APP_ID,APP_VERSION_ID,VIOLATION_ID,CALLED_FROM);
				return violations;
			}
			else               				
				return(ClsCommon.ServiceAuthenticationMsg);
//			try
//			{
//				DataSet dsTemp = new DataSet();
//				string result="";
//				DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
//				objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);				
//				objDataWrapper.AddParameter("@APP_ID",APP_ID);				
//				objDataWrapper.AddParameter("@APP_VERSION_ID",APP_VERSION_ID);				
//				objDataWrapper.AddParameter("@VIOLATION_ID",VIOLATION_ID);				
//				objDataWrapper.AddParameter("@CALLED_FROM",CALLED_FROM);								
//				dsTemp=objDataWrapper.ExecuteDataSet("Proc_GetMNT_VIOLATIONS_NEW");
//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//				if (dsTemp!=null && dsTemp.Tables[0].Rows.Count > 0)
//				{
//					result=Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsTemp.Tables[0]);
//				}
//				else
//				{	
//					result="";				
//				}
//				return result;				 
//				//string result=Cms.BusinessLayer.BlApplication.ClsVehicleInformation.FetchTerritoryForZip("1111",1).ToString();
//				//return result;
//			}
//			catch(Exception exc)
//			{return "";}
//			finally
//			{}	
			
		}
		//Following Method will fetch the Violations In case of QQ : 05 April 2005(Not In use to be removed)
		//		[WebMethod]
		//		public DataSet GetViolationsQQ(int VIOLATION_ID,string strStateName,string strLOB)
		//		{
		//
		//			try
		//			{
		//				DataSet dsTemp = new DataSet();
		//				//string result="";
		//				DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
		//				objDataWrapper.AddParameter("@VIOLATION_ID",VIOLATION_ID);		
		//				objDataWrapper.AddParameter("@STATE_NAME",strStateName);		
		//				objDataWrapper.AddParameter("@APP_LOBCODE",strLOB);		
		//				dsTemp=objDataWrapper.ExecuteDataSet("Proc_GetMNT_VIOLATIONS_QQ");
		//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
		//
		//				return dsTemp;				 
		//				
		//			}
		//			catch(Exception exc)
		//			{
		//			 throw(exc);
		//			}
		//			finally
		//			{}	
		//			
		//		}

		//


		//Following method will fetch the policies based on customer
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string GetCustomerPolicies(int CUSTOMER_ID, int AGENCY_ID)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{	
				string customerPolicies="";
				ClsWebServiceCommon customerPolicy = new ClsWebServiceCommon();
				customerPolicies=customerPolicy.GetCustomerPolicies(CUSTOMER_ID,AGENCY_ID);
				return customerPolicies;
			}
			else               				
				return(ClsCommon.ServiceAuthenticationMsg);

//			try
//			{
//				DataSet dsTemp = new DataSet();
//				string result="";
//				DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
//				objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);				
//				if(AGENCY_ID != 0)
//				{
//					objDataWrapper.AddParameter("@AGENCY_ID",AGENCY_ID);				
//				}
//				dsTemp=objDataWrapper.ExecuteDataSet("Proc_Get_Policy_For_Customer");
//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//				if (dsTemp!=null && dsTemp.Tables[0].Rows.Count > 0)
//				{
//					result=Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsTemp.Tables[0]);
//				}
//				else
//				{	
//					result="";				
//				}
//				return result;				 
//				//string result=Cms.BusinessLayer.BlApplication.ClsVehicleInformation.FetchTerritoryForZip("1111",1).ToString();
//				//return result;
//			}
//			catch(Exception exc)
//			{return "";}
//			finally
//			{}	
			
		}
		//
		//For Getting the Violation Type : 04 April 2006
		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]
		public string GetViolationType(string strStateName,string strLobcode)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				return(new ClsQuickQuote().GetViolationTypeQQ(strStateName,strLobcode));
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);

		}
		//For Getting the Violation Description 
		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]
		public string GetViolationDesc(string strviolationID,string strStateName,string strLobcode)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				return(new ClsQuickQuote().GetViolationDescQQ(strviolationID,strStateName,strLobcode));
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
		}
		//Calculationg Violation and Accidents points :13 April 06(Not in use)
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string GetMVRPoints(string strMvrXML,string strLOBCode)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				return(new ClsQuickQuote().GetMVRPointsForSurcharge(strMvrXML,strLOBCode));
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);

		}
		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]
		public DataSet GetAccidentViolationConfigDataQQ(int Lob_ID) 
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				return(ClsQuickQuote.GetAccidentViolationConfigData(Lob_ID));
			}
			else
                return null;
		}

		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string DeleteCoverage (string CUST_ID, string APP_ID, string APP_VERSION_ID, string COVG_ID)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				return (new ClsSchItemsCovg().DeletePolicyInlandMarineCoveragesSingle(APP_ID,APP_VERSION_ID,CUST_ID,COVG_ID).ToString());
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
		}

		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string GetCustomerLoss(string CUSTOMER_ID, string LOB, string StartLossDate, string EndLossDate)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				string l_CUSTOMER_ID	= CUSTOMER_ID;
				string l_LOB			= LOB == ""?null:LOB;
				string l_StartLossDate	= StartLossDate == ""?null:StartLossDate;
				string l_EndLossDate	= EndLossDate == ""?null:EndLossDate;
				return (new ClsCustomer().GetCustomerLoss(l_CUSTOMER_ID, l_LOB, l_StartLossDate, l_EndLossDate));
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);

		}

		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string GetDetailsFromVIN(string VIN)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				return ClsVinMaster.GetDetailsFromVIN(VIN).GetXml();			
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);

		}
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string CancelProcReturnPremium(string CustomerId, string PolicyId, string PolicyVersionId, string ChangeEffDate)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				return this.CancelProcReturnPremiumAmount(CustomerId,  PolicyId,  PolicyVersionId, ChangeEffDate,"0","0");
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);

		}
		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string CancelProcReturnPremiumAmount(string CustomerId, string PolicyId, string PolicyVersionId, string ChangeEffDate,string CancellationType,string CancellationOption)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				string cancelProcReturnPremiumAmount="";
				ClsWebServiceCommon objPrem = new ClsWebServiceCommon();
				cancelProcReturnPremiumAmount=objPrem.CancelProcReturnPremiumAmount(CustomerId,PolicyId,PolicyVersionId,ChangeEffDate,CancellationType,CancellationOption);
				return cancelProcReturnPremiumAmount;
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);

//			try
//			{
//				DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
//				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);				
//				objDataWrapper.AddParameter("@POLICY_ID",PolicyId);				
//				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionId);
//				objDataWrapper.AddParameter("@CHANGE_EFFECTIVE_DATE",Convert.ToDateTime(ChangeEffDate));					
//				objDataWrapper.AddParameter("@CANCELLTION_TYPE",CancellationType);
//				objDataWrapper.AddParameter("@CANCELLATION_OPTION",CancellatoinOption);
//				SqlParameter objSqlCancelPrem = (SqlParameter)objDataWrapper.AddParameter("@CANCELLATION_PREMIUM",SqlDbType.Int,ParameterDirection.Output);
//				objDataWrapper.ExecuteNonQuery("Proc_CalculateReturnPremium");
//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//				if(objSqlCancelPrem!=null && objSqlCancelPrem.Value.ToString()!="")
//					return objSqlCancelPrem.Value.ToString();
//				else
//					return "";
//			}
//			catch(Exception ex)
//			{
//				return "";
//			}
//			finally
//			{
//			}

		}

		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public int ClaimsCheckOperation(string CheckIDs, string Operation)
		{		
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				return ClsClaims.ClaimsCheckOperation(CheckIDs.Replace("%20","").Replace("%2C",","),Operation);			
			}
			else
				return 0;
			
		}

		[WebMethod(EnableSession=true)]
		[SoapHeader("AuthenticationTokenHeader")]
		public string GeneratePolicyQuote(int CustomerId,int PolicyId,int PolicyVersionId,string LobId ,int UserID )
		{	
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				int QuoteId = 0;
				System.Web.SessionState.HttpSessionState sess;
				sess = HttpContext.Current.Session;
				if (sess["userId"] == null)
				{
					sess["userId"] = UserID.ToString();
				}
				Cms.BusinessLayer.BlQuote.ClsGenerateQuote objQuote = new Cms.BusinessLayer.BlQuote.ClsGenerateQuote(SystemID);
				objQuote.LoggedInUserId = Convert.ToInt32(sess["userId"].ToString());
				objQuote.GeneratePolicyQuote(CustomerId,PolicyId,PolicyVersionId,LobId,out QuoteId,UserID.ToString());
				return QuoteId.ToString();
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
		}

		[WebMethod(EnableSession=true)]
		[SoapHeader("AuthenticationTokenHeader")]
		public string VerifyRules(int CUSTOMER_ID,int POLICY_ID,int POLICY_VERSION_ID,string PolicyLob,
			int UserID,string SystemID,string ColorScheme,out bool valid,out string strRulesStatus)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
			
				System.Web.SessionState.HttpSessionState sess;
				sess = HttpContext.Current.Session;
				if (sess["userId"] == null)
				{
					sess["userId"] = UserID.ToString();
				}
				if(sess["systemId"] == null)
				{
					sess["systemId"] = SystemID;
				}			

				ClsRatingAndUnderwritingRules objRule = new ClsRatingAndUnderwritingRules(SystemID);
				string strHTML = "";
				strHTML = objRule.VerifyPolicy(CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,
					PolicyLob,out valid,ColorScheme,out strRulesStatus,"");
				return strHTML;
			}
			else
			{	
				valid = false;strRulesStatus="";
				return(ClsCommon.ServiceAuthenticationMsg);
			}


		}
		[WebMethod(MessageName="GetCapitalCustomerCreditScore", Description="Get Customer insurance score with customer model as parameter.")]
		[SoapHeader("AuthenticationTokenHeader")]
		public string GetCapitalCustomerCreditScore(string strUserName,string strPassword,string strAccountNumber ,string strUrl,string strCustDetail)
		{

			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				Cms.Model.Client.ClsCustomerInfo objClsCustomerInfo=new Cms.Model.Client.ClsCustomerInfo();
				string  [] strCustDetails = new string[0];
				strCustDetails = strCustDetail.Split('^');
				objClsCustomerInfo.CustomerLastName=strCustDetails[0].ToString();
				objClsCustomerInfo.CustomerFirstName=strCustDetails[1].ToString();
				objClsCustomerInfo.CustomerMiddleName=strCustDetails[2].ToString();
				objClsCustomerInfo.CustomerAddress1=strCustDetails[3].ToString();
				objClsCustomerInfo.CustomerCity=strCustDetails[4].ToString();
				objClsCustomerInfo.CustomerZip=strCustDetails[5].ToString();
				objClsCustomerInfo.CustomerStateCode=strCustDetails[6].ToString();
				objClsCustomerInfo.CustomerHomePhone=strCustDetails[7].ToString();
				objClsCustomerInfo.CustomerSuffix=strCustDetails[8].ToString();
				objClsCustomerInfo.SSN_NO=strCustDetails[9].ToString();
								
				Utils.CreditScoreDetails objScore;
				objScore = (new Utils.Utility(strUserName, strPassword, strAccountNumber , strUrl).GetCustomerCreditScore(objClsCustomerInfo));
				if(objScore.Score.ToString()=="-2")
					return objScore.Score.ToString();
				else if(objScore.Score.ToString()=="-1")
					return objScore.Score.ToString();
				else
					return objScore.Score.ToString()+"^"+objScore.FirstFactor+"^"+objScore.SecondFactor+"^"+objScore.ThirdFactor+"^"+objScore.FourthFactor;
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
		}


		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			authenticationKey = ClsCommon.ServiceAuthenticationToken;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

	}
}
