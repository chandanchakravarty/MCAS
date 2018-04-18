using System;
using Cms.BusinessLayer.BlApplication;
using System.Data; 
using Cms.DataLayer; 
using System.Xml; 
using System.IO; 
namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for clsapplication.
	/// </summary>
	public class clsapplication : Cms.BusinessLayer.BlCommon.ClsCommon
	{
		public static string CALLED_FROM_APPLICATION = "APPLICATION";
		public static string CALLED_FROM_POLICY = "POLICY";
		
		public clsapplication()
		{
			//
			// TODO: Add constructor logic here
			//

			DataWrapper.SetID = new DataWrapper.SetTransId(SetTransactionId);
			DataWrapper.SetEndorsementIDs = new DataWrapper.SetEndorsementTransIds(SetEndorsementTranId);

		}

		private void SetEndorsementTranId(string ID)
		{
			if(!IsEODProcess)
			{
				if ( System.Web.HttpContext.Current.Session != null )
				{
					
					if(System.Web.HttpContext.Current.Session["InValidateSession"] == null ||
						System.Web.HttpContext.Current.Session["InValidateSession"].ToString() == "Y")
					{
						System.Web.HttpContext.Current.Session.Add("EndorsementTranIds", ID.ToString());
						System.Web.HttpContext.Current.Session["InValidateSession"] = "N";
					}
					else
					{
						string strTemp = System.Web.HttpContext.Current.Session["EndorsementTranIds"].ToString();
						strTemp = strTemp + "^" + ID;
						System.Web.HttpContext.Current.Session["EndorsementTranIds"] =strTemp ;
					}
				}
			}
		}
		private void SetTransactionId(int ID)
		{
			if(!IsEODProcess)
			{
				if ( System.Web.HttpContext.Current.Session != null )
				{
					System.Web.HttpContext.Current.Session.Add("TransactionId", ID.ToString());
				}
			}
		}

		
		/// <summary>
		/// Function for creating Exception xml 
		/// </summary>
		/// <param name="ExceptionId"></param>
		/// <returns></returns>
		public DataSet FillExceptionDetails(int ExceptionId)
		{
			
			DataSet dsException = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@EXCEPTIONID",ExceptionId,SqlDbType.Int);
			dsException = objDataWrapper.ExecuteDataSet("Proc_GetExceptionInformation");
			
			
			return dsException;
		}


		/// <summary>
		/// Function for creating Application xml 
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <returns></returns>
		public string FillApplicationDetails(int customerID,int applicationID,int appVersionID)
		{
			
			DataSet dsCustomer = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMERID",customerID,SqlDbType.Int);
			objDataWrapper.AddParameter("@APPID",applicationID,SqlDbType.Int);
			objDataWrapper.AddParameter("@APPVERSIONID",appVersionID,SqlDbType.Int);

			dsCustomer = objDataWrapper.ExecuteDataSet("Proc_GetApplicationInformation");
			
			string strXML = dsCustomer.GetXml();
			return strXML;
		}

		/// <summary>
		/// Function for fetching Add Int Details
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <returns></returns>
		public DataSet FetchAddIntDetails(string customerID,string policyID,string polVersionID, string strLOB, string strCalledFrom)
		{
			
			DataSet dsCustomer = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);

			DataSet DSTempDwellinAdd=null;

			if(strLOB == "1" || strLOB == "6")
				DSTempDwellinAdd = objDataWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest " + customerID + "," + policyID + "," + polVersionID  + ",0,'" + strCalledFrom + "'");
			else if(strLOB == "2" || strLOB == "3")
				DSTempDwellinAdd = objDataWrapper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS " + customerID + "," + policyID + "," + polVersionID  + ",0,'" + strCalledFrom + "'");
			else if(strLOB == "4")
				DSTempDwellinAdd = objDataWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS " + customerID + "," + policyID + "," + polVersionID  + ",0,'" + strCalledFrom + "'");
			
			return DSTempDwellinAdd;
		}

		public DataSet FetchAddIntDetails(string customerID,string policyID,string polVersionID, string strLOB, string strCalledFrom ,
					DataWrapper objDataWrapper)
		{
			
			objDataWrapper.ClearParameteres();
			DataSet DSTempDwellinAdd=null;

			objDataWrapper.AddParameter("@CUSTOMERID",customerID);
			objDataWrapper.AddParameter("@POLID",policyID);
			objDataWrapper.AddParameter("@VERSIONID",polVersionID);
			objDataWrapper.AddParameter("@CALLEDFROM",strCalledFrom);


			if(strLOB == "1" || strLOB == "6")
			{
				objDataWrapper.AddParameter("@DWELLINGID",0);
				DSTempDwellinAdd = objDataWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest");
			}
			else if(strLOB == "2" || strLOB == "3")
			{
				objDataWrapper.AddParameter("@VEHICLEID",0);
				DSTempDwellinAdd = objDataWrapper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS");
			}
			else if(strLOB == "4")
			{
				objDataWrapper.AddParameter("@BOATID",0);
				DSTempDwellinAdd = objDataWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS");
			}
			objDataWrapper.ClearParameteres();
			return DSTempDwellinAdd;
		}

		/// <summary>
		/// Function for fetching Print Job Details
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <returns></returns>
		public DataSet FetchPrnJobDetails(int customerID,int policyID,int polVersionID)
		{
			
			DataSet dsCustomer = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",customerID,SqlDbType.Int);
			objDataWrapper.AddParameter("@POLICY_ID",policyID,SqlDbType.Int);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID,SqlDbType.Int);

			dsCustomer = objDataWrapper.ExecuteDataSet("Proc_GetPrintJobInformation");
			
			return dsCustomer;
		}

		public DataSet FetchPdfFileLogDetails(int customerID,int policyID,int polVersionID)
		{
			
			DataSet dsCustomer = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",customerID,SqlDbType.Int);
			objDataWrapper.AddParameter("@POLICY_ID",policyID,SqlDbType.Int);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID,SqlDbType.Int);

			dsCustomer = objDataWrapper.ExecuteDataSet("Proc_GetPdfFileLogInformation");
			
			return dsCustomer;
		}
		public string getWaterCraftHomeAttachedForQQ(int customerID,int applicationID,int appVersionID)
		{
			
			DataSet dsCustomer = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objDataWrapper.AddParameter("@APP_ID",applicationID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionID);

			dsCustomer = objDataWrapper.ExecuteDataSet("getWaterCraftHomeAttachedForQQ");

			if (dsCustomer.Tables[0].Rows.Count >0)
			{
				return (dsCustomer.Tables[0].Rows[0]["APP_NUMBER"].ToString());				
			}
			else
			{
				return "";
			}
			
		}
		
		

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <returns></returns>
		public DataSet GetPolicyTypeAndStateForPolicy(int customerID, int polID, int polVersionID)
		{
			string	strStoredProc =	"Proc_Get_POLICY_STATE_TYPE";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POL_ID",polID);
			objWrapper.AddParameter("@POL_VERSION_ID",polVersionID);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

			return ds;
		}

		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <returns></returns>
		public DataSet GetReinsurance_Inquiry_Details(int customerID, int polID, int polVersionID)
		{
			string	strStoredProc =	"Proc_GetReinsurance_Inquiry_Details";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",polID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

			return ds;
		}
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <returns></returns>
		public DataSet GetReinsurance_Breakdown_Details(int customerID, int polID, int polVersionID,int processID)
		{
			string	strStoredProc =	"Proc_GetReinsurance_BreakDown_Details";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",polID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);
			objWrapper.AddParameter("@PROCESS_ID",processID);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

			return ds;
		}


		/// <summary>
		/// Gets the policy type and state
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns></returns>
		public DataSet GetPolicyTypeAndState(int customerID, int appID, int appVersionID)
		{
			string	strStoredProc =	"Proc_Get_POLICY_STATE";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

			return ds;
		}
		
		
		/// <summary>
		/// Gets the lob fore the passed in state
		/// </summary>
		/// <param name="stateID"></param>
		/// <returns></returns>
		public static DataSet GetLobByStateId(int stateID)
		{
			try
			{
		
				DataSet dsLOB = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@STATE_ID",stateID,SqlDbType.Int);
                objDataWrapper.AddParameter("@Lang_Id", BlCommon.ClsCommon.BL_LANG_ID);

				dsLOB = objDataWrapper.ExecuteDataSet("Proc_SELECTLOB");
				
				return dsLOB;	
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{}

		}



		public static DataSet GetCommClassDesc()
		{
			try
			{		
				DataSet dsGET_COMM_CLASS_DESC = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				dsGET_COMM_CLASS_DESC = objDataWrapper.ExecuteDataSet("PROC_GET_COMM_CLASS_DESC");
				
				return dsGET_COMM_CLASS_DESC;	
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{}

		}
		// ADDED BY SWARUP ON 12-JULY-2007
		public static DataSet GetLEASEDPUR()
		{
			try
			{		
				DataSet dsGET_LEASED_PUR = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				dsGET_LEASED_PUR = objDataWrapper.ExecuteDataSet("Proc_GetLEASEDPUR");
				
				return dsGET_LEASED_PUR;	
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{}

		}

		public static DataSet GetNEWUSED()
		{
			try
			{		
				DataSet dsGER_NEW_USED = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				dsGER_NEW_USED = objDataWrapper.ExecuteDataSet("Proc_GetNEWUSED");
				
				return dsGER_NEW_USED;	
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{}

		}

		


		public static string GetCommClassBasedOnID(string strUniqueID)
		{
			try
			{		
				DataSet DS = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@UNIQUEID",strUniqueID);
				DS = objDataWrapper.ExecuteDataSet("GetCommClassBasedOnID");
				return DS.Tables[0].Rows[0]["CLASSCODE"].ToString();	
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{}
		}

		public static int SaveUmbrellaDriversSOU(XmlDocument XmlDoc)
		{
			DataWrapper objDataWrapper=null;
			try
			{		
				int returnResult;
				string strCustID = XmlDoc.FirstChild.Attributes.Item(0).Value;
				string strAppID  = XmlDoc.FirstChild.Attributes.Item(1).Value;
				string strVerID  = XmlDoc.FirstChild.Attributes.Item(2).Value;

				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

				for (int i=0; i<XmlDoc.FirstChild.ChildNodes.Count;i++)
				{
					objDataWrapper.ClearParameteres();
					DateTime	RecordDate		=	DateTime.Now;
					objDataWrapper.AddParameter("@CUSTOMER_ID",strCustID);
					objDataWrapper.AddParameter("@APP_ID",strAppID);
					objDataWrapper.AddParameter("@APP_VERSION_ID",strVerID);	
					objDataWrapper.AddParameter("@DRIVER_FNAME",BlCommon.ClsCommon.DecodeXMLCharacters(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_FNAME").InnerXml));
					objDataWrapper.AddParameter("@DRIVER_MNAME",BlCommon.ClsCommon.DecodeXMLCharacters(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_MNAME").InnerXml));
					objDataWrapper.AddParameter("@DRIVER_LNAME",BlCommon.ClsCommon.DecodeXMLCharacters(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_LNAME").InnerXml));
					objDataWrapper.AddParameter("@DRIVER_CODE",BlCommon.ClsCommon.DecodeXMLCharacters(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_CODE").InnerXml));
					objDataWrapper.AddParameter("@DRIVER_DOB",BlCommon.ClsCommon.DecodeXMLCharacters(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_DOB").InnerXml));
					objDataWrapper.AddParameter("@DRIVER_ADD1",BlCommon.ClsCommon.DecodeXMLCharacters(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_ADD1").InnerXml));
					objDataWrapper.AddParameter("@DRIVER_ADD2",BlCommon.ClsCommon.DecodeXMLCharacters(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_ADD2").InnerXml));
					objDataWrapper.AddParameter("@DRIVER_CITY",BlCommon.ClsCommon.DecodeXMLCharacters(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_CITY").InnerXml));
					objDataWrapper.AddParameter("@DRIVER_STATE",BlCommon.ClsCommon.DecodeXMLCharacters(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_STATE").InnerXml));
					objDataWrapper.AddParameter("@DRIVER_ZIP",BlCommon.ClsCommon.DecodeXMLCharacters(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_ZIP").InnerXml));
					objDataWrapper.AddParameter("@DRIVER_COUNTRY",BlCommon.ClsCommon.DecodeXMLCharacters(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_COUNTRY").InnerXml));
					objDataWrapper.AddParameter("@DRIVER_HOME_PHONE",BlCommon.ClsCommon.DecodeXMLCharacters(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_HOME_PHONE").InnerXml));
					objDataWrapper.AddParameter("@DRIVER_BUSINESS_PHONE",BlCommon.ClsCommon.DecodeXMLCharacters(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_BUSINESS_PHONE").InnerXml));
					//objDataWrapper.AddParameter("@DRIVER_EXT",BlCommon.ClsCommon.DecodeXMLCharacters(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_EXT").InnerXml));
					objDataWrapper.AddParameter("@DRIVER_MOBILE",BlCommon.ClsCommon.DecodeXMLCharacters(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_MOBILE").InnerXml));
					objDataWrapper.AddParameter("@DRIVER_SSN",BlCommon.ClsCommon.DecodeXMLCharacters(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_SSN").InnerXml));
					objDataWrapper.AddParameter("@DRIVER_SEX",BlCommon.ClsCommon.DecodeXMLCharacters(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_SEX").InnerXml));
					objDataWrapper.AddParameter("@DRIVER_ID",0,SqlDbType.Int,ParameterDirection.Output);
					objDataWrapper.AddParameter("@INSERTUPDATE","I");
					
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertUmbrellaDriverDetails");
				}

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return 1;
			}
			catch
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
            
				return 0;
			}
			finally
			{}
		}
	}
}
	
