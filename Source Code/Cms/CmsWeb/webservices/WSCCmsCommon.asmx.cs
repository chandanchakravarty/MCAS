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
//using Cms.BusinessLayer.BLClaims;
using Cms.DataLayer;
using System.Data.SqlClient;
using System.Configuration;
using Cms.Model.Application;
using Cms.Model.Quote;
using System.IO;

namespace Cms.CmsWeb.webservices
{
	/// <summary>
	/// Summary description for WSCCmsCommon.
	/// </summary>
	public class WSCCmsCommon : System.Web.Services.WebService
	{
		public WSCCmsCommon()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
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

		// WEB SERVICE EXAMPLE
		// The HelloWorld() example service returns the string Hello World
		// To build, uncomment the following lines then save and build the project
		// To test this web service, press F5

//		[WebMethod]
//		public string HelloWorld()
//		{
//			return "Hello World";
//		}

//		[WebMethod]
//		public string GetSymbolForAppPolicy(string VehicleType,int Amount,int Year)
//		{
//			return GetSymbolForVehicle(VehicleType,Amount,Year);
//		}
//
//		[WebMethod(MessageName="GetSymbolForVehicle1", Description="GetSymbolForVehicle with 3 arguments")]
//		public string GetSymbolForVehicle(string VehicleType,int Amount,int Year)
//		{
//			int Symbol=0;
//			try
//			{
//				switch(VehicleType)
//				{
//					case "11334": //Get Symbol for Private Pesanger
//					case "11337": //Get Symbol for Trailer
//					case "11336": ////Get Symbol for Motor
//					case "11870": //Get Symbol for Campers
//					case "11338": //Get Symbol for Local Haul - Intermittent
//					case "11339": //Get Symbol for Local Haul
//					case "11340": //Get Symbol for Trailer  - Intermittent
//					case "11341": //Get Symbol for Trailer
//					case "11871": //Get Symbol for Long Haul
//					case "11868": //Get Symbol for Classic Car
//					case "11869": //Get Symbol for Antique Car
//						Symbol = GetSymbol(VehicleType,Amount);		
//						break;
//					case "11335": ////Get Symbol for CustomizedVan
//						Symbol = GetSymbolForCustomized(VehicleType,Amount,Year);
//						break;
//					default:
//						Symbol = 0;
//						break;
//				}			
//				return Symbol.ToString();
//			}
//			catch(Exception ex)
//			{
//				return "";
//			}
//			finally{}
//		}

//		public int GetSymbol(string VehicleType, int Amount)
//		{
//			int Symbol=0;			
//			
//			XmlDocument xDoc=new XmlDocument();
//			xDoc.Load(Server.MapPath("~/cmsweb/xsl/symbol/VehicleSymbols.xml")); 			
//			
//
//			//XmlNodeList xNodeList=xDoc.SelectNodes("GeneralRules/VehicleType[@ID='11337']/Amount[@Amount1<=" + Amount.ToString() + " and @Amount2>=" + Amount.ToString() + "]"); 
//			XmlNodeList xNodeList=xDoc.SelectNodes("GeneralRules/VehicleType[@ID='" + VehicleType + "']/Amount[@Amount1<=" + Amount.ToString() + " and @Amount2>=" + Amount.ToString() + "]"); 
//			
//			if ( xNodeList.Count > 0 )
//			{
//				XmlNode node = xNodeList[0];
//				
//				XmlNode symbolNode = node.SelectSingleNode("Symbol");
//
//				string strSymbol = symbolNode.InnerText.Trim();
//				Symbol = Convert.ToInt32(strSymbol);
//
//			}			
//
//			return Symbol;
//		}

//		public int GetSymbolForCustomized(string VehicleType, int Amount, int Year)
//		{
//			int Symbol=0;			
//			
//			XmlDocument xDoc=new XmlDocument();
//			xDoc.Load(Server.MapPath("~/cmsweb/xsl/symbol/VehicleSymbols.xml")); 			
//
//			if(Year>=1990)
//				Year=1990;
//			else
//				Year=1989;
//			
//
//			//XmlNodeList xNodeList=xDoc.SelectNodes("GeneralRules/VehicleType[@ID='11337']/Amount[@Amount1<=" + Amount.ToString() + " and @Amount2>=" + Amount.ToString() + "]"); 
//			XmlNodeList xNodeList=xDoc.SelectNodes("GeneralRules/VehicleType[@ID='" + VehicleType + "']/Year[@ID='" + Year.ToString() + "']/Amount[@Amount1<=" + Amount.ToString() + " and @Amount2>=" + Amount.ToString() + "]"); 
//			
//			if ( xNodeList.Count > 0 )
//			{
//				XmlNode node = xNodeList[0];
//				
//				XmlNode symbolNode = node.SelectSingleNode("Symbol");
//
//				string strSymbol = symbolNode.InnerText.Trim();
//				Symbol = Convert.ToInt32(strSymbol);
//
//			}			
//
//			return Symbol;
//		}

		//---------------------------



//		[WebMethod]
//		public DataSet GetProtectionClass(string protectionClass,int milesToDwell,string feetToHydrant,string lobCode)
//		{
//			return(new ClsHome().FetchProtectionClass(protectionClass,milesToDwell,feetToHydrant,lobCode));
//		}

		//-----------------------------------------------------------

//		[WebMethod]
//		public string FetchAgencyCSRProducer(int AgencyId)
//		{		
//			string CSR="";
//			try
//			{
//				ClsUser objUser = new ClsUser();
//				DataTable dtCSRProducers = objUser.GetCSRProducers(AgencyId);
//				CSR= dtCSRProducers.DataSet.GetXml(); 
//				return CSR;
//			}
//			catch(Exception exc)
//			{return "";}
//			finally
//			{}	
//		}

		//------------------------------------------------------------------

//		[WebMethod]
//		public string CancelProcReturnPremiumAmount(string CustomerId, string PolicyId, string PolicyVersionId, string ChangeEffDate,string CancellationType,string CancellatoinOption)
//		{
//			string cancelProcReturnPremiumAmount="";
//			ClsWebServiceCommon cancelReturnPremiumAmount = new ClsWebServiceCommon();
//			cancelProcReturnPremiumAmount=cancelReturnPremiumAmount.CancelProcReturnPremiumAmount(CustomerId,PolicyId,PolicyVersionId,ChangeEffDate,CancellationType,CancellatoinOption);
//			return cancelProcReturnPremiumAmount;
//						try
//						{
//							DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
//							objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);				
//							objDataWrapper.AddParameter("@POLICY_ID",PolicyId);				
//							objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionId);
//							objDataWrapper.AddParameter("@CHANGE_EFFECTIVE_DATE",Convert.ToDateTime(ChangeEffDate));					
//							objDataWrapper.AddParameter("@CANCELLTION_TYPE",CancellationType);
//							objDataWrapper.AddParameter("@CANCELLATION_OPTION",CancellatoinOption);
//							SqlParameter objSqlCancelPrem = (SqlParameter)objDataWrapper.AddParameter("@CANCELLATION_PREMIUM",SqlDbType.Int,ParameterDirection.Output);
//							objDataWrapper.ExecuteNonQuery("Proc_CalculateReturnPremium");
//							objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//							if(objSqlCancelPrem!=null && objSqlCancelPrem.Value.ToString()!="")
//								return objSqlCancelPrem.Value.ToString();
//							else
//								return "";
//						}
//						catch(Exception ex)
//						{
//							return "";
//						}
//						finally
//						{
//						}

//		}
		//-------------------------------------

		//Following method will fill the state for the county 
//		[WebMethod]
//		public string FillState(string CountyID)
//		{
//			string fillState="";
//			ClsWebServiceCommon state = new ClsWebServiceCommon();
//			fillState=state.FillState(CountyID);
//			return fillState;
//						try
//						{
//							DataSet dsTemp = new DataSet();
//							string strSql = "Proc_GetStateListForCountry";
//							string result="";
//							DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
//							objDataWrapper.AddParameter("@COUNTRY_ID",CountyID);
//							DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
//							objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//							if(objDataSet!=null && objDataSet.Tables.Count>0)
//							{ 
//								return objDataSet.GetXml();				 
//							}
//							else
//								return "";
//						}
//						catch(Exception exc)
//						{return "";}
//						finally
//						{}	
//			
//		}

		//----------------------------------------

		//Following WebMethod will fetch the County belonging to the specified state
//		[WebMethod]
//		public string FetchZipForState(int stateID, string ZipID)
//		{		
//			string zipforState="";
//			ClsWebServiceCommon objZip = new ClsWebServiceCommon();
//			zipforState=objZip.FetchZipForState(stateID,ZipID);
//			return zipforState;
////			try
////			{
////				DataSet dsTemp = new DataSet();
////				DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
////				objDataWrapper.AddParameter("@STATE_ID",stateID);	
////				objDataWrapper.AddParameter("@ZIP_ID",ZipID);	
////				dsTemp=objDataWrapper.ExecuteDataSet("Proc_FetchZipForState");
////				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
////				if(dsTemp.Tables[0].Rows[0][0].ToString()!="")
////				{
////					//return true;//result=(dsTemp.Tables[0].Rows[0][0]).ToString();
////					return dsTemp.Tables[0].Rows[0][0].ToString();
////				}
////				else
////				{	
////					return "";			
////				}
////			}
////			catch(Exception exc)
////			{return "";}
////			finally
////			{}	
//		}

		//Following WebMethod will fetch the zip belonging to the specified state
//		[WebMethod]
//		public string FetchZipState(string ZipID,int stateID)
//		{		
//			string zipState="";
//			ClsWebServiceCommon zipstate = new ClsWebServiceCommon();
//			zipState=zipstate.FetchZipForState(stateID,ZipID);
//			return zipState;
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
//		}

		//----------------------------------------------------

//		[WebMethod]
//		public string GetCountyForZip(string zip)
//		{
//			string countyZip="";
//			ClsWebServiceCommon countyforZip = new ClsWebServiceCommon();
//			countyZip=countyforZip.GetCountyForZip(zip);
//			return countyZip;

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
			
//		}

//		[WebMethod]
//		public string FetchTerritoryForZip(string zipId,int lobId)
//		{		
//			string territoryForZip="";
//			ClsWebServiceCommon territoryZip = new ClsWebServiceCommon();
//			territoryForZip=territoryZip.FetchTerritoryForZip(zipId,lobId);
//			return territoryForZip;
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
//		}

			//		[WebMethod]
			//		public string FetchTerritoryForZipStateLob(string zipId,int lobId, int stateId,int intCustomerId,int intAppId,int intAppVersionId,string calledFrom,int intvehicleuse)
			//		{		
			//			string territoryForZipStateLob="";
			//			ClsWebServiceCommon territoryZipStateLob = new ClsWebServiceCommon();
			//			territoryForZipStateLob=territoryZipStateLob.FetchTerritoryForZipStateLob(zipId,lobId,stateId,intCustomerId,intAppId,intAppVersionId,calledFrom,intvehicleuse);
			//			return territoryForZipStateLob;
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
		//}

//		public string DeleteCoverage (string CUST_ID, string APP_ID, string APP_VERSION_ID, string COVG_ID)
//		{
//			return (new ClsSchItemsCovg().DeletePolicyInlandMarineCoveragesSingle(APP_ID,APP_VERSION_ID,CUST_ID,COVG_ID).ToString());
//		}
	
	}
}
