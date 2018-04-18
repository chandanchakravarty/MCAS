/******************************************************************************************
<Author				: -   Nidhi 
<Start Date				: -	5/16/2005 5:14:26 PM
<End Date				: -	
<Description				: - 	This screen is used for modification of Watercraft Information
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Anshuman
<Modified By			: - June 07, 2005
<Purpose				: - transaction description modified

<Modified Date			: - 11/11/2005
<Modified By			: - Pawan Papreja
<Purpose				: - Corrected driver/operator changes


<Modified Date			: - 21/11/2005
<Modified By			: - Vijay Arora
<Purpose				: - Added the Policy Functions.
*******************************************************************************************/ 

using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.BusinessLayer.BlApplication;
//using Cms.Model.Application.Watercrafts;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// This class contains all the methods that will be called from Watercraft  Information screen
	/// </summary>
	public class clsWatercraftInformation : clsapplication 
	{
		private const	string		APP_WATERCRAFT_INFO			=	"APP_WATERCRAFT_INFO";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		// private int _BOAT_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateWatercraft";

		//DataSet dsWatercraftInfo;
		#endregion

		#region Public Properties
		public bool TransactionLog
		{
			set
			{
				boolTransactionLog	=	value;
			}
			get
			{
				return boolTransactionLog;
			}
		}
		#endregion

		#region Utility Functions

		public static string FetchWatercraftInfoXML(int CustomerID,int AppID, int AppVersionID, int BoatID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPVERSIONID",AppVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@BOAT_ID",BoatID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetWatercraftInfo");
			
				return dsTemp.GetXml();
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}  

		public static DataSet FetchWatercraftInfo(int CustomerID,int AppID, int AppVersionID, int BoatID)
		{
			DataSet dsTemp = null;
			try
			{
				dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPVERSIONID",AppVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@BOAT_ID",BoatID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetWatercraftInfo");
			
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
			return dsTemp;
		}  

		public static string FetchUmbrellaWatercraftInfoXML(int CustomerID,int AppID, int AppVersionID, int BoatID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPVERSIONID",AppVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@BOAT_ID",BoatID,SqlDbType.Int);
				 

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetUmbrellaWatercraftInfo");
			
				//return dsTemp.GetXml();
				return ClsCommon.GetXMLEncoded(dsTemp.Tables[0]);
															  
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}

		//Fetch boat info as well as rec veh info
		public static DataSet FetchBoatInfo(int appId,int customerId,int appVersionId)
		{
			string		strStoredProc	=	"Proc_FetchBoatInfo";
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",appId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionId,SqlDbType.Int);                           
                

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				
			}
			return dsCount;
		}

		//Asfa Praveen - 13-June-2007
		//Fetch Trailer Deductible values
		public static DataSet FetchTrailerDed(int appId,int customerId,int appVersionId, int var_TRAILER_TYPE)
		{
			string		strStoredProc	=	"Proc_FetchTrailerDed";
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPID",appId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPVERSIONID",appVersionId,SqlDbType.Int);                           
				objDataWrapper.AddParameter("@TRAILER_TYPE",var_TRAILER_TYPE,SqlDbType.Int); 
				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				
			}
			return dsCount;
		}

		//Asfa Praveen - 13-June-2007
		//Fetch Trailer Deductible values
		
		public static DataSet FetchPolicyTrailerDed(int polId,int customerId,int polVersionId, int var_TRAILER_TYPE)
		{
			string		strStoredProc	=	"Proc_FetchPolicyTrailerDed";
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICYID",polId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICYVERSIONID",polVersionId,SqlDbType.Int); 
                objDataWrapper.AddParameter("@TRAILER_TYPE",var_TRAILER_TYPE,SqlDbType.Int);           
				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				
			}
			return dsCount;
		}


		// Mohit Agarwal 22-Nov-2006
		public static DataSet FetchViolationInfo(int appId,int customerId,int appVersionId, DateTime mvrDate,string serious)
		{
			string		strStoredProc	=	"Proc_FetchWaterViolationInfo";
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",appId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionId,SqlDbType.Int);                           
				objDataWrapper.AddParameter("@MVR_DATE",mvrDate,SqlDbType.DateTime);                           
				objDataWrapper.AddParameter("@SERIOUS",serious,SqlDbType.VarChar,ParameterDirection.Input,2);                           

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				
			}
			return dsCount;
		}

		//Ravindra Gupta (03-01-2006)
		public static DataSet FetchUmbrellaBoatInfo(int appId,int customerId,int appVersionId)
		{
			string		strStoredProc	=	"Proc_FetchUmbrellaBoatInfo";
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",appId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionId,SqlDbType.Int);                           
                

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				
			}
			return dsCount;
		}
      //Changes by ravindra ends 
		public static DataSet FetchBoatInfo(int appId,int customerId,int appVersionId,int boatID)
		{
			string		strStoredProc	=	"Proc_FetchBoatInfo";
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",appId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionId,SqlDbType.Int);                           
				objDataWrapper.AddParameter("@boatID",boatID,SqlDbType.Int);                           
                

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				
			}
			return dsCount;
		}
        
		#endregion

		
		/// <summary>
		/// Function for retrieving driver count
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
		public static int GetDriverCount(int intCustomerID,int intAppID,int intAppVersionID)
		{
			DataSet dsTemp = new DataSet();			
			int retResult=0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMERID",intCustomerID);
			objDataWrapper.AddParameter("@APPID",intAppID);
			objDataWrapper.AddParameter("@APPVERSIONID",intAppVersionID);
			objDataWrapper.AddParameter("@APPVERSIONID",intAppVersionID);
			objDataWrapper.AddParameter("@CALLEDFROM",null);
			//added by anurag verma on 23/09/2005
			//type parameter is used to decide which query will run in proc_getdrivercount
			//objDataWrapper.AddParameter("@type","Y");

			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetDriverCount");
			if(dsTemp!=null)
			{
				if(dsTemp.Tables[0].Rows.Count>0 )
					retResult=int.Parse(dsTemp.Tables[0].Rows[0]["DRIVER_ID"].ToString())  ;		
			}			
			return retResult;
		}
		/// <summary>
		/// Function for retrieving driver count
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
		public static int GetDriverCount(int intCustomerID,int intAppID,int intAppVersionID,string strCalledFrom)
		{
			DataSet dsTemp = new DataSet();			
			int retResult=0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMERID",intCustomerID);
			objDataWrapper.AddParameter("@APPID",intAppID);
			objDataWrapper.AddParameter("@APPVERSIONID",intAppVersionID);
			objDataWrapper.AddParameter("@CALLEDFROM",strCalledFrom);
			
			//added by anurag verma on 23/09/2005
			//type parameter is used to decide which query will run in proc_getdrivercount
			// Commented by Mohit On 15/10/2005.
			//objDataWrapper.AddParameter("@type","Y");

			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetDriverCount");
			if(dsTemp!=null)
			{
				if(dsTemp.Tables[0].Rows.Count > 0 )
					retResult=int.Parse(dsTemp.Tables[0].Rows[0]["DRIVER_ID"].ToString())  ;		
			}			
			return retResult;
		}

		public static int GetDriverCount(int intCustomerID,int intAppID,int intAppVersionID,string strCalledFrom,string strCalledFor)
		{
			DataSet dsTemp = new DataSet();			
			int retResult=0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMERID",intCustomerID);
			objDataWrapper.AddParameter("@APPID",intAppID);
			objDataWrapper.AddParameter("@APPVERSIONID",intAppVersionID);
			objDataWrapper.AddParameter("@CALLEDFROM",strCalledFrom);
			objDataWrapper.AddParameter("@CALLEDFOR",strCalledFor);
			//added by anurag verma on 23/09/2005
			//type parameter is used to decide which query will run in proc_getdrivercount
			// Commented by Mohit On 15/10/2005.
			//objDataWrapper.AddParameter("@type","Y");

			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetDriverCount");
			if(dsTemp!=null)
			{
				if(dsTemp.Tables[0].Rows.Count > 0 )
					retResult=int.Parse(dsTemp.Tables[0].Rows[0]["DRIVER_ID"].ToString())  ;		
			}			
			return retResult;
		}

		public static string FetchWatercraftDriverInfoXML(int CustomerID,int AppID, int AppVersionID, int DriverID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@DRIVER_ID",DriverID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetWatercraftDriverInfo");			
				
				if(dsTemp.Tables[0].Rows.Count>0)
					return ClsCommon.GetXMLEncoded(dsTemp.Tables[0]);
				else
					return "";
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}

		public static int GetNewBoatNumber(int CustomerID,int AppID,int AppVersionID,string CalledFrom, DataWrapper objDataWrapper)
		{
			try
			{
				DataSet dsTemp = new DataSet();

				objDataWrapper.ClearParameteres();

				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@APP_ID",AppID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID);
				objDataWrapper.AddParameter("@CALLEDFROM",CalledFrom);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetNewBoatNo");

				objDataWrapper.ClearParameteres();
				return int.Parse(dsTemp.Tables[0].Rows[0][0].ToString());		
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}

		public static int GetNewBoatNumber(int CustomerID,int AppID,int AppVersionID,string CalledFrom)
		{
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);	
				return GetNewBoatNumber(CustomerID, AppID, AppVersionID, CalledFrom, objDataWrapper);
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}
		 
		

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public clsWatercraftInformation()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion
		
		
		//10 feb 2006 : Praveen k
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objWaterCraftInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		/*public int AddWatercraftTrailerAcord(ClsWatercraftTrailerInfo objWaterCraftTrailerInfo, DataWrapper objDataWrapper)
		{
			
			string		strStoredProc	=	"Proc_INSERT_WATERCRAFT_TRAILER_ACORD";
			DateTime	RecordDate		=	DateTime.Now;
			
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			
			objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftTrailerInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",objWaterCraftTrailerInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objWaterCraftTrailerInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@TRAILER_NO",objWaterCraftTrailerInfo.TRAILER_NO);
			objDataWrapper.AddParameter("@YEAR",objWaterCraftTrailerInfo.YEAR);
			objDataWrapper.AddParameter("@MANUFACTURER",objWaterCraftTrailerInfo.MANUFACTURER);
			objDataWrapper.AddParameter("@SERIAL_NO",objWaterCraftTrailerInfo.SERIAL_NO);
			objDataWrapper.AddParameter("@MODEL",objWaterCraftTrailerInfo.MODEL);

			
			//Trailer Deductible
			objDataWrapper.AddParameter("@TRAILER_DED",objWaterCraftTrailerInfo.TRAILER_DED);
			objDataWrapper.AddParameter("@TRAILER_DED_ID",objWaterCraftTrailerInfo.TRAILER_DED_ID);
			objDataWrapper.AddParameter("@TRAILER_DED_AMOUNT_TEXT",objWaterCraftTrailerInfo.TRAILER_DED_AMOUNT_TEXT);


			objDataWrapper.AddParameter("@ASSOCIATED_BOAT",objWaterCraftTrailerInfo.ASSOCIATED_BOAT);		
			objDataWrapper.AddParameter("@CREATED_BY",objWaterCraftTrailerInfo.CREATED_BY);
			objDataWrapper.AddParameter("@CREATED_DATETIME",RecordDate);

			if(objWaterCraftTrailerInfo.INSURED_VALUE==0)
			{
				objDataWrapper.AddParameter("@INSURED_VALUE",null);
			}
			else
				objDataWrapper.AddParameter("@INSURED_VALUE",objWaterCraftTrailerInfo.INSURED_VALUE);

		
			objDataWrapper.AddParameter("@TRAILER_TYPE_CODE",objWaterCraftTrailerInfo.TRAILER_TYPE_CODE);
			
			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@TRAILER_ID",objWaterCraftTrailerInfo.TRAILER_ID,SqlDbType.Int,ParameterDirection.Output);

			int returnResult = 0;

			if(TransactionLogRequired)
			{
				objWaterCraftTrailerInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddTrailerInformation.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objWaterCraftTrailerInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.CLIENT_ID = objWaterCraftTrailerInfo.CUSTOMER_ID;
				objTransactionInfo.APP_ID = objWaterCraftTrailerInfo.APP_ID;
				objTransactionInfo.APP_VERSION_ID = objWaterCraftTrailerInfo.APP_VERSION_ID;
				objTransactionInfo.RECORDED_BY		=	objWaterCraftTrailerInfo.CREATED_BY;
				objTransactionInfo.TRANS_DESC		=	"New Trailer is added from Acord";
				objTransactionInfo.CHANGE_XML		=	strTranXML;
					
				//Executing the query
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					
				objDataWrapper.ClearParameteres();

				objDataWrapper.ExecuteNonQuery(objTransactionInfo);

			}
			else
			{
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
			}
			int TRAILER_ID = int.Parse(objSqlParameter.Value.ToString());
			
			objWaterCraftTrailerInfo.TRAILER_ID = TRAILER_ID;
			
			objDataWrapper.ClearParameteres();
				
		
			//Commented As there are no covergaes related to the Trailer Section :  21 feb 2006
//			if ( TRAILER_ID > 0 )
//			{
//				
//				//Insert/delete relevant coverages*********************	
//				objDataWrapper.AddParameter("@APP_ID",objWaterCraftTrailerInfo.APP_ID);
//				objDataWrapper.AddParameter("@APP_VERSION_ID",objWaterCraftTrailerInfo.APP_VERSION_ID);
//				objDataWrapper.AddParameter("@TRAILER_ID",TRAILER_ID);
//				objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftTrailerInfo.CUSTOMER_ID);
//
//				objDataWrapper.ExecuteNonQuery("Proc_Update_WATERCRAFT_TRAILER_COVERAGES");
//				//*********************************************************
//			}

			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
			
			return TRAILER_ID;
				
			
		}



		//End : 10 feb 2006

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objWaterCraftInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsWatercraftInfo objWaterCraftInfo)
		{
			return Add(objWaterCraftInfo,"");
		}

		public int Add(ClsWatercraftInfo objWaterCraftInfo, string strCustomInfo)
		{
			int RetVal = 0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				RetVal = Add(objWaterCraftInfo, strCustomInfo, objDataWrapper);

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
			catch(Exception objExp)
			{
				throw(objExp);
			}
			finally
			{
				objDataWrapper.Dispose();
			}

			return RetVal;
		}

		public int Add(ClsWatercraftInfo objWaterCraftInfo, string strCustomInfo, DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_InsertWatercraft";
			DateTime	RecordDate		=	DateTime.Now;
			
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWaterCraftInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWaterCraftInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@BOAT_NO",objWaterCraftInfo.BOAT_NO);
				objDataWrapper.AddParameter("@BOAT_NAME",null);
				objDataWrapper.AddParameter("@YEAR",objWaterCraftInfo.YEAR);
				objDataWrapper.AddParameter("@MAKE",objWaterCraftInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objWaterCraftInfo.MODEL);
				objDataWrapper.AddParameter("@HULL_ID_NO",objWaterCraftInfo.HULL_ID_NO);
				objDataWrapper.AddParameter("@STATE_REG",objWaterCraftInfo.STATE_REG);
				objDataWrapper.AddParameter("@HULL_MATERIAL",objWaterCraftInfo.HULL_MATERIAL);
				objDataWrapper.AddParameter("@FUEL_TYPE",objWaterCraftInfo.FUEL_TYPE);

				//Added by RPSINGH - 12 May 2006
				objDataWrapper.AddParameter("@LOCATION_ADDRESS",objWaterCraftInfo.LOCATION_ADDRESS);
				objDataWrapper.AddParameter("@LOCATION_CITY",objWaterCraftInfo.LOCATION_CITY);
				objDataWrapper.AddParameter("@LOCATION_STATE",objWaterCraftInfo.LOCATION_STATE);
				objDataWrapper.AddParameter("@LOCATION_ZIP",objWaterCraftInfo.LOCATION_ZIP);
				objDataWrapper.AddParameter("@LAY_UP_PERIOD_FROM_DAY",objWaterCraftInfo.LAY_UP_PERIOD_FROM_DAY);
				objDataWrapper.AddParameter("@LAY_UP_PERIOD_FROM_MONTH",objWaterCraftInfo.LAY_UP_PERIOD_FROM_MONTH);
				objDataWrapper.AddParameter("@LAY_UP_PERIOD_TO_DAY",objWaterCraftInfo.LAY_UP_PERIOD_TO_DAY);
				objDataWrapper.AddParameter("@LAY_UP_PERIOD_TO_MONTH",objWaterCraftInfo.LAY_UP_PERIOD_TO_MONTH);
				//End of additon by RPSINGH - 12 May 2006

				
				if(objWaterCraftInfo.DATE_PURCHASED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_PURCHASED",objWaterCraftInfo.DATE_PURCHASED);
				}
				objDataWrapper.AddParameter("@LENGTH",objWaterCraftInfo.LENGTH);
				objDataWrapper.AddParameter("@INCHES",objWaterCraftInfo.INCHES);
				//if(objWaterCraftInfo.MAX_SPEED==0.0 || objWaterCraftInfo.MAX_SPEED==0 || objWaterCraftInfo.MAX_SPEED==.00 || objWaterCraftInfo.MAX_SPEED==0.00)
				//objDataWrapper.AddParameter("@MAX_SPEED",System.DBNull.Value);
//				if(objWaterCraftInfo.MAX_SPEED==0.0)
//				{
//					objDataWrapper.AddParameter("@MAX_SPEED",null);
//				}
//
//				else
					objDataWrapper.AddParameter("@MAX_SPEED",objWaterCraftInfo.MAX_SPEED);
				
				if(objWaterCraftInfo.LOSSREPORT_ORDER != 0)
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",objWaterCraftInfo.LOSSREPORT_ORDER );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",System.DBNull.Value );

				if(objWaterCraftInfo.LOSSREPORT_DATETIME != DateTime.MinValue)
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",objWaterCraftInfo.LOSSREPORT_DATETIME );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",System.DBNull.Value );
				
				objDataWrapper.AddParameter("@BERTH_LOC",objWaterCraftInfo.BERTH_LOC);
				objDataWrapper.AddParameter("@WATERS_NAVIGATED",objWaterCraftInfo.WATERS_NAVIGATED);
				objDataWrapper.AddParameter("@TERRITORY",objWaterCraftInfo.TERRITORY);		
				objDataWrapper.AddParameter("@CREATED_BY",objWaterCraftInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objWaterCraftInfo.CREATED_DATETIME);

				if(objWaterCraftInfo.INSURING_VALUE==0.0)
				{
					objDataWrapper.AddParameter("@INSURING_VALUE",DBNull.Value);
				}
				else
					objDataWrapper.AddParameter("@INSURING_VALUE",objWaterCraftInfo.INSURING_VALUE);

//				if(objWaterCraftInfo.DEDUCTIBLE==0)
//				{
//					objDataWrapper.AddParameter("@DEDUCTIBLE",null);
//				}
//				else
//					objDataWrapper.AddParameter("@DEDUCTIBLE",objWaterCraftInfo.DEDUCTIBLE); 

//				if(objWaterCraftInfo.WATERCRAFT_HORSE_POWER==0)
//				{
//					objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",null);
//				}
//				else		 
					objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",objWaterCraftInfo.WATERCRAFT_HORSE_POWER);
				objDataWrapper.AddParameter("@TWIN_SINGLE",objWaterCraftInfo.TWIN_SINGLE);
				objDataWrapper.AddParameter("@TYPE_OF_WATERCRAFT",objWaterCraftInfo.TYPE_OF_WATERCRAFT);

				objDataWrapper.AddParameter("@LORAN_NAV_SYSTEM",objWaterCraftInfo.LORAN_NAV_SYSTEM);
				objDataWrapper.AddParameter("@DIESEL_ENGINE",objWaterCraftInfo.DIESEL_ENGINE);
				objDataWrapper.AddParameter("@SHORE_STATION",objWaterCraftInfo.SHORE_STATION);
				objDataWrapper.AddParameter("@HALON_FIRE_EXT_SYSTEM",objWaterCraftInfo.HALON_FIRE_EXT_SYSTEM);
				objDataWrapper.AddParameter("@DUAL_OWNERSHIP",objWaterCraftInfo.DUAL_OWNERSHIP);
				objDataWrapper.AddParameter("@REMOVE_SAILBOAT",objWaterCraftInfo.REMOVE_SAILBOAT);
				objDataWrapper.AddParameter("@COV_TYPE_BASIS",objWaterCraftInfo.COV_TYPE_BASIS);
				objDataWrapper.AddParameter("@PHOTO_ATTACHED",objWaterCraftInfo.PHOTO_ATTACHED);
				objDataWrapper.AddParameter("@MARINE_SURVEY",objWaterCraftInfo.MARINE_SURVEY);
				
				if(objWaterCraftInfo.DATE_MARINE_SURVEY!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_MARINE_SURVEY",objWaterCraftInfo.DATE_MARINE_SURVEY);
				}
				
				objDataWrapper.AddParameter("@DESC_OTHER_WATERCRAFT",objWaterCraftInfo.DESC_OTHER_WATERCRAFT);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@BOAT_ID",objWaterCraftInfo.BOAT_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objWaterCraftInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objWaterCraftInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = objWaterCraftInfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID = objWaterCraftInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objWaterCraftInfo.APP_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objWaterCraftInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New watercraft's boat is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					if (strCustomInfo != "")
						objTransactionInfo.CUSTOM_INFO	=	strCustomInfo;

					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int BOAT_ID = int.Parse(objSqlParameter.Value.ToString());


				#region Assigned Boats Process watercraft
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWaterCraftInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWaterCraftInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@BOAT_ID",BOAT_ID);
				returnResult	= objDataWrapper.ExecuteNonQuery("Proc_SetAssgndDriverBoatType");
				objDataWrapper.ClearParameteres();
				#endregion
				


				objDataWrapper.ClearParameteres();

				if ( BOAT_ID > 0 )
				{
					
					//Update default coverages and endorsement
					ClsWatercraftCoverages objCoverage = new ClsWatercraftCoverages();
					objCoverage.createdby =objWaterCraftInfo.CREATED_BY;
					objCoverage.modifiedby =objWaterCraftInfo.MODIFIED_BY; 
					objCoverage.SaveDefaultCoveragesApp(objDataWrapper, objWaterCraftInfo.CUSTOMER_ID, objWaterCraftInfo.APP_ID
						, objWaterCraftInfo.APP_VERSION_ID, BOAT_ID);

					
					objDataWrapper.ClearParameteres();
				}
                //Check lob if home Update Coverage of home
			    ClsGeneralInformation obj=new ClsGeneralInformation();
				string lobId="";
				lobId= obj.Fun_GetLObID(objWaterCraftInfo.CUSTOMER_ID, objWaterCraftInfo.APP_ID
						, objWaterCraftInfo.APP_VERSION_ID);
				if(lobId == ((int)enumLOB.HOME).ToString())
				{
					ClsHomeCoverages objCoverage=new ClsHomeCoverages();
					objCoverage.createdby =objWaterCraftInfo.CREATED_BY;
					objDataWrapper.ClearParameteres();
					objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objWaterCraftInfo.CUSTOMER_ID,objWaterCraftInfo.APP_ID  ,
						objWaterCraftInfo.APP_VERSION_ID,RuleType.LobDependent);
				}
				

				
				if (BOAT_ID == -1)
				{
					return -1;
				}
				else if(BOAT_ID == -2)
				{
					return -2;
				}
				else
				{
					objWaterCraftInfo.BOAT_ID = BOAT_ID;
					return BOAT_ID;
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}*/

		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objWaterCraftInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		/*public int AddWatercraftAcord(ClsWatercraftInfo objWaterCraftInfo, DataWrapper objDataWrapper)
		{
			try
			{
				objWaterCraftInfo.CREATED_DATETIME = DateTime.Now;
				objWaterCraftInfo.TYPE_OF_WATERCRAFT = ClsCommon.GetLookupUniqueId("WCTCD", objWaterCraftInfo.TYPE_OF_WATERCRAFT).ToString();
				objWaterCraftInfo.TERRITORY = ClsCommon.GetLookupUniqueId("TERR", objWaterCraftInfo.TERRITORY).ToString();

				//if outboat then This should default to Outbaord w/Motor
				if(objWaterCraftInfo.TYPE_OF_WATERCRAFT == "11369")
				{
					objWaterCraftInfo.TYPE_OF_WATERCRAFT ="11487";

				}
				objWaterCraftInfo.WATERS_NAVIGATED = ClsCommon.GetLookupUniqueId("WNVC", objWaterCraftInfo.WATERS_NAVIGATED).ToString();
				objWaterCraftInfo.HULL_MATERIAL = ClsCommon.GetLookupUniqueId("%HULL", objWaterCraftInfo.HULL_MATERIAL_CODE);
				objWaterCraftInfo.BOAT_NO = GetNewBoatNumber(objWaterCraftInfo.CUSTOMER_ID, objWaterCraftInfo.APP_ID,
											objWaterCraftInfo.APP_VERSION_ID, "WAT",objDataWrapper);

				return Add(objWaterCraftInfo, "", objDataWrapper);
			}
			catch(Exception objexp)
			{
				throw(objexp);
			}
		}
		
		public int AddUmbrellaWatercraft(ClsWatercraftInfo objWaterCraftInfo)
		{
			return AddUmbrellaWatercraft(objWaterCraftInfo,"");
//			string		strStoredProc	=	"Proc_InsertUmbrellaWatercraft";
//			DateTime	RecordDate		=	DateTime.Now;
//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
//
//			try
//			{
//				objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);
//				objDataWrapper.AddParameter("@APP_ID",objWaterCraftInfo.APP_ID);
//				objDataWrapper.AddParameter("@APP_VERSION_ID",objWaterCraftInfo.APP_VERSION_ID);
//				objDataWrapper.AddParameter("@BOAT_NO",objWaterCraftInfo.BOAT_NO);
//				objDataWrapper.AddParameter("@BOAT_NAME",null);
//				objDataWrapper.AddParameter("@YEAR",objWaterCraftInfo.YEAR);
//				objDataWrapper.AddParameter("@MAKE",objWaterCraftInfo.MAKE);
//				objDataWrapper.AddParameter("@MODEL",objWaterCraftInfo.MODEL);
//				objDataWrapper.AddParameter("@HULL_ID_NO",objWaterCraftInfo.HULL_ID_NO);
//				objDataWrapper.AddParameter("@STATE_REG",objWaterCraftInfo.STATE_REG);
//				objDataWrapper.AddParameter("@REG_NO",objWaterCraftInfo.REG_NO);
//				objDataWrapper.AddParameter("@POWER",objWaterCraftInfo.POWER);
//				objDataWrapper.AddParameter("@HULL_TYPE",objWaterCraftInfo.HULL_TYPE);
//				objDataWrapper.AddParameter("@HULL_MATERIAL",objWaterCraftInfo.HULL_MATERIAL);
//				objDataWrapper.AddParameter("@FUEL_TYPE",objWaterCraftInfo.FUEL_TYPE);
//				objDataWrapper.AddParameter("@HULL_DESIGN",objWaterCraftInfo.HULL_DESIGN);
//				objDataWrapper.AddParameter("@LOCATION_ADDRESS",objWaterCraftInfo.LOCATION_ADDRESS);
//				objDataWrapper.AddParameter("@LOCATION_CITY",objWaterCraftInfo.LOCATION_CITY);
//				objDataWrapper.AddParameter("@LOCATION_STATE",objWaterCraftInfo.LOCATION_STATE);
//				objDataWrapper.AddParameter("@LOCATION_ZIP",objWaterCraftInfo.LOCATION_ZIP);
//				objDataWrapper.AddParameter("@COV_TYPE_BASIS",objWaterCraftInfo.COV_TYPE_BASIS);
//			
//				if(objWaterCraftInfo.DATE_PURCHASED!=DateTime.MinValue)
//				{
//					objDataWrapper.AddParameter("@DATE_PURCHASED",objWaterCraftInfo.DATE_PURCHASED);
//				}
//				objDataWrapper.AddParameter("@LENGTH",objWaterCraftInfo.LENGTH);
//				//if(objWaterCraftInfo.MAX_SPEED==0.0 || objWaterCraftInfo.MAX_SPEED==0 || objWaterCraftInfo.MAX_SPEED==.00 || objWaterCraftInfo.MAX_SPEED==0.00)
//				//objDataWrapper.AddParameter("@MAX_SPEED",System.DBNull.Value);
//				if(objWaterCraftInfo.MAX_SPEED==0.0)
//				{
//					objDataWrapper.AddParameter("@MAX_SPEED",null);
//				}
//										
//				else
//					objDataWrapper.AddParameter("@MAX_SPEED",objWaterCraftInfo.MAX_SPEED);
//				objDataWrapper.AddParameter("@COST_NEW",objWaterCraftInfo.COST_NEW);
//				objDataWrapper.AddParameter("@PRESENT_VALUE",objWaterCraftInfo.PRESENT_VALUE);
//				objDataWrapper.AddParameter("@BERTH_LOC",objWaterCraftInfo.BERTH_LOC);
//				objDataWrapper.AddParameter("@WATERS_NAVIGATED",objWaterCraftInfo.WATERS_NAVIGATED);
//				objDataWrapper.AddParameter("@TERRITORY",objWaterCraftInfo.TERRITORY);		
//				objDataWrapper.AddParameter("@CREATED_BY",objWaterCraftInfo.CREATED_BY);
//				objDataWrapper.AddParameter("@CREATED_DATETIME",objWaterCraftInfo.CREATED_DATETIME);
//				if(objWaterCraftInfo.INSURING_VALUE==0)
//				{
//					objDataWrapper.AddParameter("@INSURING_VALUE",null);
//				}
//				else
//					objDataWrapper.AddParameter("@INSURING_VALUE",objWaterCraftInfo.INSURING_VALUE);
//
//				if(objWaterCraftInfo.DEDUCTIBLE==0)
//				{
//					objDataWrapper.AddParameter("@DEDUCTIBLE",null);
//				}
//				else
//					objDataWrapper.AddParameter("@DEDUCTIBLE",objWaterCraftInfo.DEDUCTIBLE);
//
//				//	objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",objWaterCraftInfo.WATERCRAFT_HORSE_POWER);
//				if(objWaterCraftInfo.WATERCRAFT_HORSE_POWER==0)
//				{
//					objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",null);
//				}
//				else		 
//					objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",objWaterCraftInfo.WATERCRAFT_HORSE_POWER);
//				objDataWrapper.AddParameter("@TYPE_OF_WATERCRAFT",objWaterCraftInfo.TYPE_OF_WATERCRAFT);
//				objDataWrapper.AddParameter("@DESC_OTHER_WATERCRAFT",objWaterCraftInfo.DESC_OTHER_WATERCRAFT);
//
//				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@BOAT_ID",objWaterCraftInfo.BOAT_ID,SqlDbType.Int,ParameterDirection.Output);
//
//				int returnResult = 0;
//				if(TransactionLogRequired)
//				{
//					objWaterCraftInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftInformation.aspx.resx");
//					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
//					string strTranXML = objBuilder.GetTransactionLogXML(objWaterCraftInfo);
//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//					objTransactionInfo.TRANS_TYPE_ID	=	1;
//					objTransactionInfo.CLIENT_ID = objWaterCraftInfo.CUSTOMER_ID;
//					objTransactionInfo.APP_ID = objWaterCraftInfo.APP_ID;
//					objTransactionInfo.APP_VERSION_ID = objWaterCraftInfo.APP_VERSION_ID;
//					objTransactionInfo.RECORDED_BY		=	objWaterCraftInfo.CREATED_BY;
//					objTransactionInfo.TRANS_DESC		=	"New umbrella watercraft's boat is added";
//					objTransactionInfo.CHANGE_XML		=	strTranXML;
//					//Executing the query
//					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
//				}
//				else
//				{
//					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
//				}
//				int BOAT_ID = int.Parse(objSqlParameter.Value.ToString());
//				objDataWrapper.ClearParameteres();
//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//				if (BOAT_ID == -1)
//				{
//					return -1;
//				}
//				else
//				{
//					objWaterCraftInfo.BOAT_ID = BOAT_ID;
//					return BOAT_ID;
//				}
//			}
//			catch(Exception ex)
//			{
//				throw(ex);
//			}
//			finally
//			{
//				if(objDataWrapper != null) objDataWrapper.Dispose();
//			}
		}

		public int AddUmbrellaWatercraft(ClsWatercraftInfo objWaterCraftInfo, string strCustomInfo)
		{
			string		strStoredProc	=	"Proc_InsertUmbrellaWatercraft";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWaterCraftInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWaterCraftInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@BOAT_NO",objWaterCraftInfo.BOAT_NO);
				objDataWrapper.AddParameter("@BOAT_NAME",null);
				objDataWrapper.AddParameter("@YEAR",objWaterCraftInfo.YEAR);
				objDataWrapper.AddParameter("@MAKE",objWaterCraftInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objWaterCraftInfo.MODEL);
				objDataWrapper.AddParameter("@HULL_ID_NO",objWaterCraftInfo.HULL_ID_NO);
				objDataWrapper.AddParameter("@STATE_REG",objWaterCraftInfo.STATE_REG);
				objDataWrapper.AddParameter("@REG_NO",objWaterCraftInfo.REG_NO);
				objDataWrapper.AddParameter("@POWER",objWaterCraftInfo.POWER);
				objDataWrapper.AddParameter("@HULL_TYPE",objWaterCraftInfo.HULL_TYPE);
				objDataWrapper.AddParameter("@HULL_MATERIAL",objWaterCraftInfo.HULL_MATERIAL);
				objDataWrapper.AddParameter("@FUEL_TYPE",objWaterCraftInfo.FUEL_TYPE);
				objDataWrapper.AddParameter("@HULL_DESIGN",objWaterCraftInfo.HULL_DESIGN);
				if(objWaterCraftInfo.DATE_PURCHASED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_PURCHASED",objWaterCraftInfo.DATE_PURCHASED);
				}
				objDataWrapper.AddParameter("@LENGTH",objWaterCraftInfo.LENGTH);
				//if(objWaterCraftInfo.MAX_SPEED==0.0 || objWaterCraftInfo.MAX_SPEED==0 || objWaterCraftInfo.MAX_SPEED==.00 || objWaterCraftInfo.MAX_SPEED==0.00)
				//objDataWrapper.AddParameter("@MAX_SPEED",System.DBNull.Value);
//				if(objWaterCraftInfo.MAX_SPEED==0.0)
//				{
//					objDataWrapper.AddParameter("@MAX_SPEED",null);
//				}
//
//				else
					objDataWrapper.AddParameter("@MAX_SPEED",objWaterCraftInfo.MAX_SPEED);
				objDataWrapper.AddParameter("@COST_NEW",objWaterCraftInfo.COST_NEW);
				objDataWrapper.AddParameter("@PRESENT_VALUE",objWaterCraftInfo.PRESENT_VALUE);
				objDataWrapper.AddParameter("@BERTH_LOC",objWaterCraftInfo.BERTH_LOC);
				objDataWrapper.AddParameter("@WATERS_NAVIGATED",objWaterCraftInfo.WATERS_NAVIGATED);
				objDataWrapper.AddParameter("@TERRITORY",objWaterCraftInfo.TERRITORY);		
				objDataWrapper.AddParameter("@CREATED_BY",objWaterCraftInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objWaterCraftInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@LOCATION_ADDRESS",objWaterCraftInfo.LOCATION_ADDRESS);
				objDataWrapper.AddParameter("@LOCATION_CITY",objWaterCraftInfo.LOCATION_CITY);
				objDataWrapper.AddParameter("@LOCATION_STATE",objWaterCraftInfo.LOCATION_STATE);
				objDataWrapper.AddParameter("@LOCATION_ZIP",objWaterCraftInfo.LOCATION_ZIP);
				objDataWrapper.AddParameter("@COV_TYPE_BASIS",objWaterCraftInfo.COV_TYPE_BASIS);
//				if(objWaterCraftInfo.INSURING_VALUE==0)
//				{
//					objDataWrapper.AddParameter("@INSURING_VALUE",null);
//				}
//				else
//					objDataWrapper.AddParameter("@INSURING_VALUE",objWaterCraftInfo.INSURING_VALUE);
//				if(objWaterCraftInfo.DEDUCTIBLE==0)
//				{
//					objDataWrapper.AddParameter("@DEDUCTIBLE",null);
//				}
//				else
//					objDataWrapper.AddParameter("@DEDUCTIBLE",objWaterCraftInfo.DEDUCTIBLE);   

				//	objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",objWaterCraftInfo.WATERCRAFT_HORSE_POWER);
//				if(objWaterCraftInfo.WATERCRAFT_HORSE_POWER==0)
//				{
//					objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",null);
//				}
//				else		 
					objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",objWaterCraftInfo.WATERCRAFT_HORSE_POWER);
				objDataWrapper.AddParameter("@TYPE_OF_WATERCRAFT",objWaterCraftInfo.TYPE_OF_WATERCRAFT);
				objDataWrapper.AddParameter("@DESC_OTHER_WATERCRAFT",objWaterCraftInfo.DESC_OTHER_WATERCRAFT);

				
				objDataWrapper.AddParameter("@LORAN_NAV_SYSTEM",objWaterCraftInfo.LORAN_NAV_SYSTEM);
				objDataWrapper.AddParameter("@DIESEL_ENGINE",objWaterCraftInfo.DIESEL_ENGINE);
				objDataWrapper.AddParameter("@SHORE_STATION",objWaterCraftInfo.SHORE_STATION);
				objDataWrapper.AddParameter("@HALON_FIRE_EXT_SYSTEM",objWaterCraftInfo.HALON_FIRE_EXT_SYSTEM);
				objDataWrapper.AddParameter("@DUAL_OWNERSHIP",objWaterCraftInfo.DUAL_OWNERSHIP);
				objDataWrapper.AddParameter("@REMOVE_SAILBOAT",objWaterCraftInfo.REMOVE_SAILBOAT);
				objDataWrapper.AddParameter("@TWIN_SINGLE",objWaterCraftInfo.TWIN_SINGLE);
				objDataWrapper.AddParameter("@INCHES",objWaterCraftInfo.INCHES);
				objDataWrapper.AddParameter("@USED_PARTICIPATE",objWaterCraftInfo.USED_PARTICIPATE);
				objDataWrapper.AddParameter("@WATERCRAFT_CONTEST",objWaterCraftInfo.WATERCRAFT_CONTEST);
				objDataWrapper.AddParameter("@OTHER_POLICY",objWaterCraftInfo.OTHER_POLICY);
				objDataWrapper.AddParameter("@IS_BOAT_EXCLUDED",objWaterCraftInfo.IS_BOAT_EXCLUDED);

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@BOAT_ID",objWaterCraftInfo.BOAT_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objWaterCraftInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objWaterCraftInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = objWaterCraftInfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID = objWaterCraftInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objWaterCraftInfo.APP_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objWaterCraftInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New umbrella watercraft's boat is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				objDataWrapper.ClearParameteres();
				int BOAT_ID = int.Parse(objSqlParameter.Value.ToString());				
				if (BOAT_ID == -1)
				{
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					return -1;
				}
				else
				{
					if(returnResult >= 1)					
					{
						ClsUmbrellaRecrVeh objUmbRecVeh = new ClsUmbrellaRecrVeh();
						objUmbRecVeh.UmbrellaEndorsementRule(objDataWrapper,objWaterCraftInfo.CUSTOMER_ID,objWaterCraftInfo.APP_ID,objWaterCraftInfo.APP_VERSION_ID,clsapplication.CALLED_FROM_APPLICATION,ClsUmbSchRecords.CALLED_FROM_BOAT);
					}
					objWaterCraftInfo.BOAT_ID = BOAT_ID;
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					return BOAT_ID;
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}*/
		//#endregion

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldWaterCraftInfo">Model object having old information</param>
		/// <param name="objWaterCraftInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		/*public int Update(ClsWatercraftInfo objOldWaterCraftInfo,ClsWatercraftInfo objWaterCraftInfo)
		{
			return Update(objOldWaterCraftInfo, objWaterCraftInfo, "");
		}

		public int Update(ClsWatercraftInfo objOldWaterCraftInfo,ClsWatercraftInfo objWaterCraftInfo, string strCustomInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string		strStoredProc	=	"Proc_UpdateWatercraft";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWaterCraftInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWaterCraftInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@BOAT_ID",objWaterCraftInfo.BOAT_ID);
				objDataWrapper.AddParameter("@BOAT_NO",objWaterCraftInfo.BOAT_NO);
				objDataWrapper.AddParameter("@BOAT_NAME",null);
				objDataWrapper.AddParameter("@YEAR",objWaterCraftInfo.YEAR);
				objDataWrapper.AddParameter("@MAKE",objWaterCraftInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objWaterCraftInfo.MODEL);
				objDataWrapper.AddParameter("@HULL_ID_NO",objWaterCraftInfo.HULL_ID_NO);
				objDataWrapper.AddParameter("@STATE_REG",objWaterCraftInfo.STATE_REG);
				objDataWrapper.AddParameter("@HULL_MATERIAL",objWaterCraftInfo.HULL_MATERIAL);
				objDataWrapper.AddParameter("@FUEL_TYPE",objWaterCraftInfo.FUEL_TYPE);

				//Added by RPSINGH - 12 May 2006
				objDataWrapper.AddParameter("@LOCATION_ADDRESS",objWaterCraftInfo.LOCATION_ADDRESS);
				objDataWrapper.AddParameter("@LOCATION_CITY",objWaterCraftInfo.LOCATION_CITY);
				objDataWrapper.AddParameter("@LOCATION_STATE",objWaterCraftInfo.LOCATION_STATE);
				objDataWrapper.AddParameter("@LOCATION_ZIP",objWaterCraftInfo.LOCATION_ZIP);
				objDataWrapper.AddParameter("@LAY_UP_PERIOD_FROM_DAY",objWaterCraftInfo.LAY_UP_PERIOD_FROM_DAY);
				objDataWrapper.AddParameter("@LAY_UP_PERIOD_FROM_MONTH",objWaterCraftInfo.LAY_UP_PERIOD_FROM_MONTH);
				objDataWrapper.AddParameter("@LAY_UP_PERIOD_TO_DAY",objWaterCraftInfo.LAY_UP_PERIOD_TO_DAY);
				objDataWrapper.AddParameter("@LAY_UP_PERIOD_TO_MONTH",objWaterCraftInfo.LAY_UP_PERIOD_TO_MONTH);
				//End of additon by RPSINGH - 12 May 2006


				objDataWrapper.AddParameter("@TWIN_SINGLE",objWaterCraftInfo.TWIN_SINGLE);
				if(objWaterCraftInfo.DATE_PURCHASED!= DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_PURCHASED",objWaterCraftInfo.DATE_PURCHASED);
				}
				else
				{
					objDataWrapper.AddParameter("@DATE_PURCHASED",null);
				}
				objDataWrapper.AddParameter("@LENGTH",objWaterCraftInfo.LENGTH);
				objDataWrapper.AddParameter("@INCHES",objWaterCraftInfo.INCHES);
				//if(objWaterCraftInfo.MAX_SPEED==0.0 || objWaterCraftInfo.MAX_SPEED==0 || objWaterCraftInfo.MAX_SPEED==.00 || objWaterCraftInfo.MAX_SPEED==0.00)
				//objDataWrapper.AddParameter("@MAX_SPEED",System.DBNull.Value);
//				if(objWaterCraftInfo.MAX_SPEED==0.0)
//				{
//					objDataWrapper.AddParameter("@MAX_SPEED",null);
//				}
//
//				else
					objDataWrapper.AddParameter("@MAX_SPEED",objWaterCraftInfo.MAX_SPEED);
				
				objDataWrapper.AddParameter("@BERTH_LOC",objWaterCraftInfo.BERTH_LOC);
				objDataWrapper.AddParameter("@WATERS_NAVIGATED",objWaterCraftInfo.WATERS_NAVIGATED);
				objDataWrapper.AddParameter("@TERRITORY",objWaterCraftInfo.TERRITORY);						 
				objDataWrapper.AddParameter("@MODIFIED_BY",objWaterCraftInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWaterCraftInfo.LAST_UPDATED_DATETIME);
				if(objWaterCraftInfo.INSURING_VALUE==0.0)
				{
					objDataWrapper.AddParameter("@INSURING_VALUE",DBNull.Value);
				}
				else
					objDataWrapper.AddParameter("@INSURING_VALUE",objWaterCraftInfo.INSURING_VALUE);

//				if(objWaterCraftInfo.DEDUCTIBLE==0)
//				{
//					objDataWrapper.AddParameter("@DEDUCTIBLE",null);
//				}
//				else
//					objDataWrapper.AddParameter("@DEDUCTIBLE",objWaterCraftInfo.DEDUCTIBLE); 

//				if(objWaterCraftInfo.WATERCRAFT_HORSE_POWER==0)
//				{
//					objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",null);
//				}
//				else		 
					objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",objWaterCraftInfo.WATERCRAFT_HORSE_POWER);
		
				objDataWrapper.AddParameter("@TYPE_OF_WATERCRAFT",objWaterCraftInfo.TYPE_OF_WATERCRAFT);
				objDataWrapper.AddParameter("@DESC_OTHER_WATERCRAFT",objWaterCraftInfo.DESC_OTHER_WATERCRAFT);

				
				objDataWrapper.AddParameter("@LORAN_NAV_SYSTEM",objWaterCraftInfo.LORAN_NAV_SYSTEM);
				objDataWrapper.AddParameter("@DIESEL_ENGINE",objWaterCraftInfo.DIESEL_ENGINE);
				objDataWrapper.AddParameter("@SHORE_STATION",objWaterCraftInfo.SHORE_STATION);
				objDataWrapper.AddParameter("@HALON_FIRE_EXT_SYSTEM",objWaterCraftInfo.HALON_FIRE_EXT_SYSTEM);
				objDataWrapper.AddParameter("@DUAL_OWNERSHIP",objWaterCraftInfo.DUAL_OWNERSHIP);
				objDataWrapper.AddParameter("@REMOVE_SAILBOAT",objWaterCraftInfo.REMOVE_SAILBOAT);
				objDataWrapper.AddParameter("@COV_TYPE_BASIS",objWaterCraftInfo.COV_TYPE_BASIS);
				objDataWrapper.AddParameter("@PHOTO_ATTACHED",objWaterCraftInfo.PHOTO_ATTACHED);
				objDataWrapper.AddParameter("@MARINE_SURVEY",objWaterCraftInfo.MARINE_SURVEY);
				if(objWaterCraftInfo.DATE_MARINE_SURVEY!= DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_MARINE_SURVEY",objWaterCraftInfo.DATE_MARINE_SURVEY);
				}
				else
				{
					objDataWrapper.AddParameter("@DATE_MARINE_SURVEY",null);
				}
				
				if(objWaterCraftInfo.LOSSREPORT_ORDER != 0)
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",objWaterCraftInfo.LOSSREPORT_ORDER );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",System.DBNull.Value );

				if(objWaterCraftInfo.LOSSREPORT_DATETIME != DateTime.MinValue)
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",objWaterCraftInfo.LOSSREPORT_DATETIME );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",System.DBNull.Value );

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RET_VAL",null,SqlDbType.Int,ParameterDirection.ReturnValue);
				if(TransactionLog) 
				{
					objWaterCraftInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftInformation.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldWaterCraftInfo,objWaterCraftInfo);
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.CLIENT_ID = objWaterCraftInfo.CUSTOMER_ID;
						objTransactionInfo.APP_ID = objWaterCraftInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objWaterCraftInfo.APP_VERSION_ID;
						objTransactionInfo.RECORDED_BY		=	objWaterCraftInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Watercraft's boat is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

						if (strCustomInfo != "")
							objTransactionInfo.CUSTOM_INFO	=	strCustomInfo;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				if(objSqlParameter!=null && objSqlParameter.Value!="")
					returnResult = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();

				ClsWatercraftCoverages objCoverage = new ClsWatercraftCoverages();
//				objCoverage.SaveDefaultCoverages(objDataWrapper, objWaterCraftInfo.CUSTOMER_ID
//					, objWaterCraftInfo.APP_ID, objWaterCraftInfo.APP_VERSION_ID
//					, objWaterCraftInfo.BOAT_ID);
				objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objWaterCraftInfo.CUSTOMER_ID
					, objWaterCraftInfo.APP_ID, objWaterCraftInfo.APP_VERSION_ID
					,RuleType.RiskDependent , objWaterCraftInfo.BOAT_ID);
				//Check If Lob Is Home
				ClsGeneralInformation obj=new ClsGeneralInformation();
				string lobId="";
				lobId= obj.Fun_GetLObID(objWaterCraftInfo.CUSTOMER_ID, objWaterCraftInfo.APP_ID
					, objWaterCraftInfo.APP_VERSION_ID);
				if(lobId == ((int)enumLOB.HOME).ToString())
				{
					//added by Pravesh to update Lobdependent Coverages
					objDataWrapper.ClearParameteres();
					objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objWaterCraftInfo.CUSTOMER_ID
						, objWaterCraftInfo.APP_ID, objWaterCraftInfo.APP_VERSION_ID
						,RuleType.LobDependent, objWaterCraftInfo.BOAT_ID);
					ClsHomeCoverages objCov=new ClsHomeCoverages();
					objDataWrapper.ClearParameteres();
					objCov.UpdateCoveragesByRuleApp(objDataWrapper,objWaterCraftInfo.CUSTOMER_ID,objWaterCraftInfo.APP_ID  ,
						objWaterCraftInfo.APP_VERSION_ID,RuleType.LobDependent);
				}

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) 
				{
					objDataWrapper.Dispose();
				}
				if(objBuilder != null) 
				{
					objBuilder = null;
				}
			}
		}*/
		/*public int UpdateUmbrellaWatercraft(ClsWatercraftInfo objOldWaterCraftInfo,ClsWatercraftInfo objWaterCraftInfo)
		{
			return UpdateUmbrellaWatercraft(objOldWaterCraftInfo,objWaterCraftInfo,"");
//			string strTranXML;
//			int returnResult = 0;
//			string		strStoredProc	=	"Proc_UpdateUmbrellaWatercraft";
//			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
//			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
//			try 
//			{
//				objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);
//				objDataWrapper.AddParameter("@APP_ID",objWaterCraftInfo.APP_ID);
//				objDataWrapper.AddParameter("@APP_VERSION_ID",objWaterCraftInfo.APP_VERSION_ID);
//				objDataWrapper.AddParameter("@BOAT_ID",objWaterCraftInfo.BOAT_ID);
//				objDataWrapper.AddParameter("@BOAT_NO",objWaterCraftInfo.BOAT_NO);
//				objDataWrapper.AddParameter("@BOAT_NAME",null);
//				objDataWrapper.AddParameter("@YEAR",objWaterCraftInfo.YEAR);
//				objDataWrapper.AddParameter("@MAKE",objWaterCraftInfo.MAKE);
//				objDataWrapper.AddParameter("@MODEL",objWaterCraftInfo.MODEL);
//				objDataWrapper.AddParameter("@HULL_ID_NO",objWaterCraftInfo.HULL_ID_NO);
//				objDataWrapper.AddParameter("@STATE_REG",objWaterCraftInfo.STATE_REG);
//				objDataWrapper.AddParameter("@REG_NO",objWaterCraftInfo.REG_NO);
//				objDataWrapper.AddParameter("@POWER",objWaterCraftInfo.POWER);
//				objDataWrapper.AddParameter("@HULL_TYPE",objWaterCraftInfo.HULL_TYPE);
//				objDataWrapper.AddParameter("@HULL_MATERIAL",objWaterCraftInfo.HULL_MATERIAL);
//				objDataWrapper.AddParameter("@FUEL_TYPE",objWaterCraftInfo.FUEL_TYPE);
//				objDataWrapper.AddParameter("@HULL_DESIGN",objWaterCraftInfo.HULL_DESIGN);
//				objDataWrapper.AddParameter("@LOCATION_ADDRESS",objWaterCraftInfo.LOCATION_ADDRESS);
//				objDataWrapper.AddParameter("@LOCATION_CITY",objWaterCraftInfo.LOCATION_CITY);
//				objDataWrapper.AddParameter("@LOCATION_STATE",objWaterCraftInfo.LOCATION_STATE);
//				objDataWrapper.AddParameter("@LOCATION_ZIP",objWaterCraftInfo.LOCATION_ZIP);
//				objDataWrapper.AddParameter("@COV_TYPE_BASIS",objWaterCraftInfo.COV_TYPE_BASIS);
//				if(objWaterCraftInfo.DATE_PURCHASED!= DateTime.MinValue)
//				{
//					objDataWrapper.AddParameter("@DATE_PURCHASED",objWaterCraftInfo.DATE_PURCHASED);
//				}
//				else
//				{
//					objDataWrapper.AddParameter("@DATE_PURCHASED",null);
//				}
//				objDataWrapper.AddParameter("@LENGTH",objWaterCraftInfo.LENGTH);
//				//if(objWaterCraftInfo.MAX_SPEED==0.0 || objWaterCraftInfo.MAX_SPEED==0 || objWaterCraftInfo.MAX_SPEED==.00 || objWaterCraftInfo.MAX_SPEED==0.00)
//				//objDataWrapper.AddParameter("@MAX_SPEED",System.DBNull.Value);
//				if(objWaterCraftInfo.MAX_SPEED==0.0)
//				{
//					objDataWrapper.AddParameter("@MAX_SPEED",null);
//				}
//
//				else
//					objDataWrapper.AddParameter("@MAX_SPEED",objWaterCraftInfo.MAX_SPEED);
//				objDataWrapper.AddParameter("@COST_NEW",objWaterCraftInfo.COST_NEW);
//				objDataWrapper.AddParameter("@PRESENT_VALUE",objWaterCraftInfo.PRESENT_VALUE);
//				objDataWrapper.AddParameter("@BERTH_LOC",objWaterCraftInfo.BERTH_LOC);
//				objDataWrapper.AddParameter("@WATERS_NAVIGATED",objWaterCraftInfo.WATERS_NAVIGATED);
//				objDataWrapper.AddParameter("@TERRITORY",objWaterCraftInfo.TERRITORY);						 
//				objDataWrapper.AddParameter("@MODIFIED_BY",objWaterCraftInfo.MODIFIED_BY);
//				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWaterCraftInfo.LAST_UPDATED_DATETIME);
//				if(objWaterCraftInfo.INSURING_VALUE==0)
//				{
//					objDataWrapper.AddParameter("@INSURING_VALUE",null);
//				}
//				else
//					objDataWrapper.AddParameter("@INSURING_VALUE",objWaterCraftInfo.INSURING_VALUE);
//
//				if(objWaterCraftInfo.DEDUCTIBLE==0)
//				{
//					objDataWrapper.AddParameter("@DEDUCTIBLE",null);
//				}
//				else
//					objDataWrapper.AddParameter("@DEDUCTIBLE",objWaterCraftInfo.DEDUCTIBLE); 
//				//	objDataWrapper.AddParameter("@INSURING_VALUE",objWaterCraftInfo.INSURING_VALUE);
//				if(objWaterCraftInfo.WATERCRAFT_HORSE_POWER==0)
//				{
//					objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",null);
//				}
//				else		 
//					objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",objWaterCraftInfo.WATERCRAFT_HORSE_POWER);
//				//objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",objWaterCraftInfo.WATERCRAFT_HORSE_POWER);
//				objDataWrapper.AddParameter("@TYPE_OF_WATERCRAFT",objWaterCraftInfo.TYPE_OF_WATERCRAFT);
//				objDataWrapper.AddParameter("@DESC_OTHER_WATERCRAFT",objWaterCraftInfo.DESC_OTHER_WATERCRAFT);
//				objDataWrapper.AddParameter("@TWIN_SINGLE",objWaterCraftInfo.TWIN_SINGLE);
//				objDataWrapper.AddParameter("@INCHES",objWaterCraftInfo.INCHES);
//				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RET_VAL",null,SqlDbType.Int,ParameterDirection.ReturnValue);
//				if(TransactionLog) 
//				{
//					objWaterCraftInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftInformation.aspx.resx");
//					strTranXML = objBuilder.GetTransactionLogXML(objOldWaterCraftInfo,objWaterCraftInfo);
//
//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//					objTransactionInfo.TRANS_TYPE_ID	=	3;
//					objTransactionInfo.CLIENT_ID = objWaterCraftInfo.CUSTOMER_ID;
//					objTransactionInfo.APP_ID = objWaterCraftInfo.APP_ID;
//					objTransactionInfo.APP_VERSION_ID = objWaterCraftInfo.APP_VERSION_ID;
//					objTransactionInfo.RECORDED_BY		=	objWaterCraftInfo.MODIFIED_BY;
//					objTransactionInfo.TRANS_DESC		=	"Watercraft's boat is modified";
//					objTransactionInfo.CHANGE_XML		=	strTranXML;
//					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
//
//				}
//				else
//				{
//					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
//				}
//				if(objSqlParameter!=null && objSqlParameter.Value!="")
//					returnResult = int.Parse(objSqlParameter.Value.ToString());
//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//				return returnResult;
//			}
//			catch(Exception ex)
//			{
//				throw(ex);
//			}
//			finally
//			{
//				if(objDataWrapper != null) 
//				{
//					objDataWrapper.Dispose();
//				}
//				if(objBuilder != null) 
//				{
//					objBuilder = null;
//				}
//			}
		}

		public int UpdateUmbrellaWatercraft(ClsWatercraftInfo objOldWaterCraftInfo,ClsWatercraftInfo objWaterCraftInfo, string strCustomInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string		strStoredProc	=	"Proc_UpdateUmbrellaWatercraft";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWaterCraftInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWaterCraftInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@BOAT_ID",objWaterCraftInfo.BOAT_ID);
				objDataWrapper.AddParameter("@BOAT_NO",objWaterCraftInfo.BOAT_NO);
				objDataWrapper.AddParameter("@BOAT_NAME",null);
				objDataWrapper.AddParameter("@YEAR",objWaterCraftInfo.YEAR);
				objDataWrapper.AddParameter("@MAKE",objWaterCraftInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objWaterCraftInfo.MODEL);
				objDataWrapper.AddParameter("@HULL_ID_NO",objWaterCraftInfo.HULL_ID_NO);
				objDataWrapper.AddParameter("@STATE_REG",objWaterCraftInfo.STATE_REG);
				objDataWrapper.AddParameter("@REG_NO",objWaterCraftInfo.REG_NO);
				objDataWrapper.AddParameter("@POWER",objWaterCraftInfo.POWER);
				objDataWrapper.AddParameter("@HULL_TYPE",objWaterCraftInfo.HULL_TYPE);
				objDataWrapper.AddParameter("@HULL_MATERIAL",objWaterCraftInfo.HULL_MATERIAL);
				objDataWrapper.AddParameter("@FUEL_TYPE",objWaterCraftInfo.FUEL_TYPE);
				objDataWrapper.AddParameter("@HULL_DESIGN",objWaterCraftInfo.HULL_DESIGN);
				if(objWaterCraftInfo.DATE_PURCHASED!= DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_PURCHASED",objWaterCraftInfo.DATE_PURCHASED);
				}
				else
				{
					objDataWrapper.AddParameter("@DATE_PURCHASED",null);
				}
				objDataWrapper.AddParameter("@LENGTH",objWaterCraftInfo.LENGTH);
				//if(objWaterCraftInfo.MAX_SPEED==0.0 || objWaterCraftInfo.MAX_SPEED==0 || objWaterCraftInfo.MAX_SPEED==.00 || objWaterCraftInfo.MAX_SPEED==0.00)
				//objDataWrapper.AddParameter("@MAX_SPEED",System.DBNull.Value);
//				if(objWaterCraftInfo.MAX_SPEED==0.0)
//				{
//					objDataWrapper.AddParameter("@MAX_SPEED",null);
//				}
//
//				else
					objDataWrapper.AddParameter("@MAX_SPEED",objWaterCraftInfo.MAX_SPEED);
				objDataWrapper.AddParameter("@COST_NEW",objWaterCraftInfo.COST_NEW);
				objDataWrapper.AddParameter("@PRESENT_VALUE",objWaterCraftInfo.PRESENT_VALUE);
				objDataWrapper.AddParameter("@BERTH_LOC",objWaterCraftInfo.BERTH_LOC);
				objDataWrapper.AddParameter("@WATERS_NAVIGATED",objWaterCraftInfo.WATERS_NAVIGATED);
				objDataWrapper.AddParameter("@TERRITORY",objWaterCraftInfo.TERRITORY);						 
				objDataWrapper.AddParameter("@MODIFIED_BY",objWaterCraftInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWaterCraftInfo.LAST_UPDATED_DATETIME);
//				if(objWaterCraftInfo.INSURING_VALUE==0)
//				{
//					objDataWrapper.AddParameter("@INSURING_VALUE",null);
//				}
//				else
					objDataWrapper.AddParameter("@INSURING_VALUE",objWaterCraftInfo.INSURING_VALUE);

				//if(objWaterCraftInfo.DEDUCTIBLE==0)
				//{
				//	objDataWrapper.AddParameter("@DEDUCTIBLE",null);
				//}
				//else
				//	objDataWrapper.AddParameter("@DEDUCTIBLE",objWaterCraftInfo.DEDUCTIBLE);
				//	objDataWrapper.AddParameter("@INSURING_VALUE",objWaterCraftInfo.INSURING_VALUE);
//				if(objWaterCraftInfo.WATERCRAFT_HORSE_POWER==0)
//				{
//					objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",null);
//				}
//				else		 
					objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",objWaterCraftInfo.WATERCRAFT_HORSE_POWER);
				//objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",objWaterCraftInfo.WATERCRAFT_HORSE_POWER);
				objDataWrapper.AddParameter("@TYPE_OF_WATERCRAFT",objWaterCraftInfo.TYPE_OF_WATERCRAFT);
				objDataWrapper.AddParameter("@DESC_OTHER_WATERCRAFT",objWaterCraftInfo.DESC_OTHER_WATERCRAFT);
				
				objDataWrapper.AddParameter("@LORAN_NAV_SYSTEM",objWaterCraftInfo.LORAN_NAV_SYSTEM);
				objDataWrapper.AddParameter("@DIESEL_ENGINE",objWaterCraftInfo.DIESEL_ENGINE);
				objDataWrapper.AddParameter("@SHORE_STATION",objWaterCraftInfo.SHORE_STATION);
				objDataWrapper.AddParameter("@HALON_FIRE_EXT_SYSTEM",objWaterCraftInfo.HALON_FIRE_EXT_SYSTEM);
				objDataWrapper.AddParameter("@DUAL_OWNERSHIP",objWaterCraftInfo.DUAL_OWNERSHIP);
				objDataWrapper.AddParameter("@REMOVE_SAILBOAT",objWaterCraftInfo.REMOVE_SAILBOAT);
				objDataWrapper.AddParameter("@TWIN_SINGLE",objWaterCraftInfo.TWIN_SINGLE);
				objDataWrapper.AddParameter("@INCHES",objWaterCraftInfo.INCHES);
				objDataWrapper.AddParameter("@LOCATION_ADDRESS",objWaterCraftInfo.LOCATION_ADDRESS);
				objDataWrapper.AddParameter("@LOCATION_CITY",objWaterCraftInfo.LOCATION_CITY);
				objDataWrapper.AddParameter("@LOCATION_STATE",objWaterCraftInfo.LOCATION_STATE);
				objDataWrapper.AddParameter("@LOCATION_ZIP",objWaterCraftInfo.LOCATION_ZIP);
				objDataWrapper.AddParameter("@COV_TYPE_BASIS",objWaterCraftInfo.COV_TYPE_BASIS);
				objDataWrapper.AddParameter("@USED_PARTICIPATE",objWaterCraftInfo.USED_PARTICIPATE);
				objDataWrapper.AddParameter("@WATERCRAFT_CONTEST",objWaterCraftInfo.WATERCRAFT_CONTEST);
				objDataWrapper.AddParameter("@OTHER_POLICY",objWaterCraftInfo.OTHER_POLICY);
				objDataWrapper.AddParameter("@IS_BOAT_EXCLUDED",objWaterCraftInfo.IS_BOAT_EXCLUDED);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RET_VAL",null,SqlDbType.Int,ParameterDirection.ReturnValue);

				if(TransactionLog) 
				{
					objWaterCraftInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\Watercrafts\AddWatercraftInformation.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldWaterCraftInfo,objWaterCraftInfo);
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.CLIENT_ID = objWaterCraftInfo.CUSTOMER_ID;
						objTransactionInfo.APP_ID = objWaterCraftInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objWaterCraftInfo.APP_VERSION_ID;
						objTransactionInfo.RECORDED_BY		=	objWaterCraftInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Watercraft's boat is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				if(objSqlParameter!=null && objSqlParameter.Value.ToString()!="")
					returnResult = int.Parse(objSqlParameter.Value.ToString());

				if(returnResult >= 1)					
				{
					ClsUmbrellaRecrVeh objUmbRecVeh = new ClsUmbrellaRecrVeh();
					objUmbRecVeh.UmbrellaEndorsementRule(objDataWrapper,objWaterCraftInfo.CUSTOMER_ID,objWaterCraftInfo.APP_ID,objWaterCraftInfo.APP_VERSION_ID,clsapplication.CALLED_FROM_APPLICATION,ClsUmbSchRecords.CALLED_FROM_BOAT);
				}
				//objWaterCraftInfo.BOAT_ID = BOAT_ID;				

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) 
				{
					objDataWrapper.Dispose();
				}
				if(objBuilder != null) 
				{
					objBuilder = null;
				}
			}
		}*/
		#endregion

		/// <summary>
		/// Function for retrieving Customer name on the basis of customer id as parameter.   
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <returns></returns>
		public static string GetCustomerNameXML(int intCustomerID)
		{
			DataSet dsTemp = new DataSet();			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMERID",intCustomerID);
			//dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetCustomerName");
			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetCustomerInfo");
			//return dsTemp.GetXml();
			string localxml = dsTemp.GetXml();
			#region Make a new node
			if(localxml.IndexOf("NewDataSet") >= 0)
			{
				string lblSSN = "";
				XmlDocument objxml = new XmlDocument();
				objxml.LoadXml(dsTemp.GetXml());
				XmlNode node = objxml.SelectSingleNode("NewDataSet");
				foreach(XmlNode nodes in node.SelectNodes("Table"))
				{

					XmlNode noder1 = nodes.SelectSingleNode("SSN_NO");
					if(noder1!=null && noder1.InnerText!="")
					{
						string strSSN_NO = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);
						if(strSSN_NO.Trim() != "")//Done for Itrack Issue 6063 on 7 July 09
						{
							string strvaln = "xxx-xx-";
							strvaln += strSSN_NO.Substring(strvaln.Length, strSSN_NO.Length - strvaln.Length);
							lblSSN = strvaln;
						}
						else
							lblSSN = "";
					}
					else
						lblSSN = "";
					XmlNode newnode = objxml.CreateElement("DECRYPT_SSN_NO");
					newnode.InnerText = lblSSN;
					nodes.AppendChild(newnode);
				}
				return objxml.OuterXml;
			}
			#endregion
			return dsTemp.GetXml();
		
		}
		public static int GetApplicationLOBID(string sCustomerID,string sAppID, string sAppVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",int.Parse (sCustomerID),SqlDbType.Int );
				objDataWrapper.AddParameter("@APP_ID",int.Parse(sAppID) ,SqlDbType.Int );
				objDataWrapper.AddParameter("@APP_VERSION_ID", int.Parse(sAppVersionID) ,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetApplicationLOB");
				return int.Parse(dsTemp.Tables[0].Rows[0][0].ToString());		
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}

		// following code commented and added by ashwani on 13 Feb 2006 
		//public void ActivateDeactivateWatercraft(ClsWatercraftInfo objWatercraftInfo, string strStatus,string strCustomInfo)
		/*public int ActivateDeactivateWatercraft(ClsWatercraftInfo objWatercraftInfo, string strStatus , string strCustInfo)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateWatercraft";		
			int returnResult = 0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWatercraftInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@BOAT_ID",objWatercraftInfo.BOAT_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);
				//<Added by Ashwani on 13 Feb. 2006 
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RESULT",SqlDbType.Int,ParameterDirection.Output);
				
				if(TransactionLogRequired)
				{																			
					objWatercraftInfo.TransactLabel = ClsCommon.MapTransactionLabel("/application/Aspx/Watercrafts/AddWatercraftInformation.aspx.resx");
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID			=	objWatercraftInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objWatercraftInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftInfo.MODIFIED_BY;				

					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	"Watercraft is Activated";
					else
						objTransactionInfo.TRANS_DESC		=	"Watercraft is Deactivated";
					objTransactionInfo.CUSTOM_INFO		=	strCustInfo;	
					
					//objTransactionInfo.CUSTOM_INFO		=	";Operator Name = " + strDriverName + ";Operator Code = " + strDriverCode;					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}	
				int intResult = int.Parse(objSqlParameter.Value.ToString());
				if(intResult>0)
				{
					ClsUmbrellaRecrVeh objUmbRecVeh = new ClsUmbrellaRecrVeh();
					objUmbRecVeh.UmbrellaEndorsementRule(objDataWrapper,objWatercraftInfo.CUSTOMER_ID,objWatercraftInfo.APP_ID,objWatercraftInfo.APP_VERSION_ID,clsapplication.CALLED_FROM_APPLICATION,ClsUmbSchRecords.CALLED_FROM_BOAT);
				}
				if(intResult==-2)
				{
					returnResult=intResult;
				}				
				objDataWrapper.ClearParameteres();
				//Check lob if home Update Coverage of home
				ClsGeneralInformation obj=new ClsGeneralInformation();
				string lobId="";
				lobId= obj.Fun_GetLObID(objWatercraftInfo.CUSTOMER_ID, objWatercraftInfo.APP_ID
					, objWatercraftInfo.APP_VERSION_ID);
				if(lobId == ((int)enumLOB.HOME).ToString())
				{
					ClsHomeCoverages objCoverage=new ClsHomeCoverages();
					objDataWrapper.ClearParameteres();
					objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objWatercraftInfo.CUSTOMER_ID,objWatercraftInfo.APP_ID  ,
						objWatercraftInfo.APP_VERSION_ID,RuleType.LobDependent);
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);	
				return returnResult;
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

		public int GetOperatorCountForAssignedBoat(ClsWatercraftInfo objVehicleInfo)
		{
			string		strStoredProc	=	"Proc_GetOperatorCountForAssignedBoat";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@BOAT_ID",objVehicleInfo.BOAT_ID);				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN",null,SqlDbType.Int ,ParameterDirection.ReturnValue);				
				objDataWrapper.ExecuteNonQuery(strStoredProc);				
				objDataWrapper.ClearParameteres();
				int returnResult = Convert.ToInt32(objSqlParameter.Value);
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
				return returnResult;
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
		

		public int GetDriverCountForAssignedVehicle(ClsWatercraftInfo objVehicleInfo, string strCalledFrom)
		{
			string		strStoredProc	=	"Proc_GetDriverCountForAssignedVehicle";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.BOAT_ID);
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN",null,SqlDbType.Int ,ParameterDirection.ReturnValue);				
				objDataWrapper.ExecuteNonQuery(strStoredProc);				
				objDataWrapper.ClearParameteres();
				int returnResult = Convert.ToInt32(objSqlParameter.Value);
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
				return returnResult;
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
		
        


		public void ActivateDeactivateUmbrellaWatercraft(ClsWatercraftInfo objWatercraftInfo, string strStatus,string strCustomInfo)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateUmbrellaWatercraft";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWatercraftInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@BOAT_ID",objWatercraftInfo.BOAT_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

				int returnResult = 0;
				if(TransactionLogRequired)
				{																			
					objWatercraftInfo.TransactLabel = ClsCommon.MapTransactionLabel("/application/Aspx/Watercrafts/AddWatercraftInformation.aspx.resx");
																					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID			=	objWatercraftInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objWatercraftInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftInfo.MODIFIED_BY;
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	"Watercraft is Activated";
					else
						objTransactionInfo.TRANS_DESC		=	"Watercraft is Deactivated";
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;					
					//objTransactionInfo.CUSTOM_INFO		=	";Operator Name = " + strDriverName + ";Operator Code = " + strDriverCode;					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
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
		*/
		
			/// <summary>
			/// Gets a  Equipment Ids against an application
			/// </summary>
			/// <param name="customerID"></param>
			/// <param name="appID"></param>
			/// <param name="appVersionID"></param>
			/// <returns>Boat Ids  as a carat separated string . if no boat exist then it returns -1</returns>
			public static string  GetEquipmentID(int customerID, int appID, int appVersionID)
			{
				string	strStoredProc =	"Proc_GetEquipIDs";
			
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
				objWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objWrapper.AddParameter("@APP_ID",appID);
				objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
				string strEquipID="-1";
				if(ds.Tables[0]!=null && ds.Tables[0].Rows !=null)
				{
					int intCount=ds.Tables[0].Rows.Count;				
					for(int i=0;i<intCount;i++)
					{
						if(i==0)
						{
							strEquipID=ds.Tables[0].Rows[i][0].ToString();
						}
						else
						{
							strEquipID = strEquipID + '^'  + ds.Tables[0].Rows[i][0].ToString();
						}				
					}
				}
				return strEquipID;
			}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="policyID"></param>
		/// <param name="policyVersionID"></param>
		/// <returns>Equipment Ids  as a carat separated string . if no one exist then it returns -1</returns>
		public static string  GetEquipmentID_Pol(int customerID, int policyID, int policyVersionID)
		{
			string	strStoredProc =	"Proc_GetPolicyEquipIDs";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",policyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			string strEquipID="-1";
			if(ds.Tables[0]!=null && ds.Tables[0].Rows !=null)
			{
				int intCount=ds.Tables[0].Rows.Count;				
				for(int i=0;i<intCount;i++)
				{
					if(i==0)
					{
						strEquipID=ds.Tables[0].Rows[i][0].ToString();
					}
					else
					{
						strEquipID = strEquipID + '^'  + ds.Tables[0].Rows[i][0].ToString();
					}				
				}
			}
			return strEquipID;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="appID"></param>
        /// <param name="appVersionID"></param>
        /// <returns></returns>
		public static string  GetTrailerBoatIDs(int customerID, int appID, int appVersionID)
		{
			string	strStoredProc =	"Proc_GetWATERCRAFT_TRAILERIDs";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			string strBoatID="-1";
			if(ds.Tables[0]!=null && ds.Tables[0].Rows !=null)
			{
				int intCount=ds.Tables[0].Rows.Count;				
				for(int i=0;i<intCount;i++)
				{
					if(i==0)
					{
						strBoatID=ds.Tables[0].Rows[i][0].ToString();
					}
					else
					{
						strBoatID = strBoatID + '^'  + ds.Tables[0].Rows[i][0].ToString();
					}				
				}
			}
			return strBoatID;
		}

		/// <summary>
		/// Gets a  Boat Ids against an application
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns>Boat Ids  as a carat separated string . if no boat exist then it returns -1</returns>
		public static string  GetBoatIDs(int customerID, int appID, int appVersionID)
		{
			string	strStoredProc =	"Proc_GetRuleBoatIDs";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			string strBoatID="-1";
			if(ds.Tables[0]!=null && ds.Tables[0].Rows !=null)
			{
				int intCount=ds.Tables[0].Rows.Count;				
				for(int i=0;i<intCount;i++)
				{
					if(i==0)
					{
						strBoatID=ds.Tables[0].Rows[i][0].ToString();
					}
					else
					{
						strBoatID = strBoatID + '^'  + ds.Tables[0].Rows[i][0].ToString();
					}				
				}
			}
			return strBoatID;
		}

		public static string GetUmbrellaWatercraftID(int customerId,int appId,int appVersionId, int dataaccessvalue)
		{
			string		strStoredProc	=	"PROC_GETUMBRELLA_WATERCRAFT_ID";
			DataSet dsCount=null;
           		
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
			objDataWrapper.AddParameter("@ID",appId,SqlDbType.Int);
			objDataWrapper.AddParameter("@VERSION_ID",appVersionId,SqlDbType.Int);                           
            objDataWrapper.AddParameter("@DATA_ACCESS_POINT",dataaccessvalue,SqlDbType.Int);      
			string strWatercraftId="-1";
			dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			if(dsCount.Tables.Count > 0)
			{
				int intCount=dsCount.Tables[0].Rows.Count;
			
				
				for(int i=0;i<intCount;i++)
				{
					if(i==0)
					{
						strWatercraftId=dsCount.Tables[0].Rows[i][0].ToString();
						//+ '^' + dsCount.Tables[0].Rows[i][1].ToString();
					}
					else
					{
						strWatercraftId = strWatercraftId + '~'  + dsCount.Tables[0].Rows[i][0].ToString();
						//+ '^' + dsCount.Tables[0].Rows[i][1].ToString();
					}
	
				}
				
			}	
			return strWatercraftId;
		}
		public static string GetUmbrellaWatercraftIDPolicy(int customerId,int polId,int polVersionId, int dataaccessvalue)
		{
			string		strStoredProc	=	"PROC_GETUMBRELLA_WATERCRAFT_ID_POL";
			DataSet dsCount=null;
           		
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
			objDataWrapper.AddParameter("@ID",polId,SqlDbType.Int);
			objDataWrapper.AddParameter("@VERSION_ID",polVersionId,SqlDbType.Int);                           
            objDataWrapper.AddParameter("@DATA_ACCESS_POINT",dataaccessvalue,SqlDbType.Int);     
			string strWatercraftId="-1";
			dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			if(dsCount.Tables.Count > 0)
			{
				int intCount=dsCount.Tables[0].Rows.Count;
			
				
				for(int i=0;i<intCount;i++)
				{
					if(i==0)
					{
						strWatercraftId=dsCount.Tables[0].Rows[i][0].ToString();
						//+ '^' + dsCount.Tables[0].Rows[i][1].ToString();
					}
					else
					{
						strWatercraftId = strWatercraftId + '~'  + dsCount.Tables[0].Rows[i][0].ToString();
						//+ '^' + dsCount.Tables[0].Rows[i][1].ToString();
					}
	
				}
				
			}	
			return strWatercraftId;
		}

	
		 
		public int DeleteUmbrellaWatercraft(string strCustomerId, string strAppId, string strAppVerId,string strBoatID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			string	strStoredProc =	"Proc_DeleteUmbrellaWatercraft";
			objDataWrapper.AddParameter("@CUSTOMER_ID",strCustomerId);
			objDataWrapper.AddParameter("@APP_ID",strAppId);
			objDataWrapper.AddParameter("@APP_VERSION_ID",strAppVerId);
			objDataWrapper.AddParameter("@BOAT_ID",strBoatID);
			int intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);	
			if(intResult>0)
			{
				ClsUmbrellaRecrVeh objUmbRecVeh = new ClsUmbrellaRecrVeh();
				objUmbRecVeh.UmbrellaEndorsementRule(objDataWrapper,int.Parse(strCustomerId),int.Parse(strAppId),int.Parse(strAppVerId),clsapplication.CALLED_FROM_APPLICATION,ClsUmbSchRecords.CALLED_FROM_BOAT);
			}
			return intResult;
		
		}

		/*public int Delete(ClsWatercraftInfo objWatercraftInfo,string strCustomInfo)
		{
			
			string	strStoredProc =	"Proc_DeleteWatercraft";						
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWatercraftInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@BOAT_ID",objWatercraftInfo.BOAT_ID);
				//<001> <start> Added by Ashwani on 13 Feb. 2006 
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RESULT",SqlDbType.Int,ParameterDirection.Output);
				//<001> <end>

				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = objWatercraftInfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID = objWatercraftInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objWatercraftInfo.APP_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Boat is deleted";
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
					
				int intResult = int.Parse(objSqlParameter.Value.ToString());
				if(intResult==-2)
				{
					returnResult=intResult;
				}
                objDataWrapper.ClearParameteres();

				//Updates relevant home coverages and endorsments on deleting a watercraft/////
				ClsWatercraftCoverages objWater = new ClsWatercraftCoverages();
				objWater.UpdateAppCoveragesAndEndorsementsOnDelete(objWatercraftInfo.CUSTOMER_ID,objWatercraftInfo.APP_ID,objWatercraftInfo.APP_VERSION_ID,objDataWrapper);

				//Check For Lob Id
				ClsGeneralInformation obj=new ClsGeneralInformation();
				string lobId="";
				lobId= obj.Fun_GetLObID(objWatercraftInfo.CUSTOMER_ID, objWatercraftInfo.APP_ID
					, objWatercraftInfo.APP_VERSION_ID);
				if(lobId == ((int)enumLOB.HOME).ToString())
				{
					ClsHomeCoverages objCoverage=new ClsHomeCoverages();
					objDataWrapper.ClearParameteres();
					objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objWatercraftInfo.CUSTOMER_ID,objWatercraftInfo.APP_ID  ,
						objWatercraftInfo.APP_VERSION_ID,RuleType.LobDependent);
				}
				////////////////
				

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);		
				return returnResult;
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

		public int DeleteUmbrellaWatercraft(ClsWatercraftInfo objWatercraftInfo,string strCustomInfo)
		{
			string	strStoredProc =	"Proc_DeleteUmbrellaWatercraft";			
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWatercraftInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@BOAT_ID",objWatercraftInfo.BOAT_ID);				

				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = objWatercraftInfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID = objWatercraftInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objWatercraftInfo.APP_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Boat is deleted";
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);		
				return returnResult;
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
         */

		public static DataSet FetchDiscountInfo(int customerId,int appID, int appVersionId,int driverID)
		{
			string		strStoredProc	=	"Proc_GetOperatorDiscounts";
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",appID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionId,SqlDbType.Int);                           
				objDataWrapper.AddParameter("@DRIVER_ID",driverID,SqlDbType.Int);                

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
				if(dsCount!=null)
				{
					return dsCount;
				}
				else
					return null;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				
			}			
		}

		#region APP/POL EFFECTIVE DATE

		public string GetAppEffectiveDate(int customerID, int appID, int appVersionID)
		{
			string		strStoredProc	=	"Proc_GetAppEffectiveDate";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{	
				DataSet dsTemp = new DataSet();
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objDataWrapper.AddParameter("@APP_ID",appID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
				dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);
				return dsTemp.Tables[0].Rows[0][0].ToString();	
				
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
		public string GetPolEffectiveDate(int customerID, int policyID, int policyVersionID)
		{
			string		strStoredProc	=	"Proc_GetPolEffectiveDate";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{	
				DataSet dsTemp = new DataSet();
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objDataWrapper.AddParameter("@POLICY_ID",policyID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
				dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);
				return dsTemp.Tables[0].Rows[0][0].ToString();	
				
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

		#region POLICY FUNCTIONS
		
		public static DataSet FetchDiscountInfoPol(int customerId,int policyID, int policyVersionId,int driverID)

		{

			string                strStoredProc    =          "Proc_GetOperatorDiscountsForPol";

			DataSet dsCount=null;

                        
			try

			{

				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);

				objDataWrapper.AddParameter("@POLICY_ID",policyID,SqlDbType.Int);

				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionId,SqlDbType.Int);                           

				objDataWrapper.AddParameter("@DRIVER_ID",driverID,SqlDbType.Int);                

 

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);

				if(dsCount!=null)

				{

					return dsCount;

				}

				else

					return null;

			}

			catch(Exception ex)

			{

				throw(ex);

			}

			finally

			{

                                                

			}                                   

		}


		/// <summary>
		/// Fetch the Policy WaterCraft Information.
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		/// <param name="BoatID"></param>
		/// <returns></returns>
		public static string FetchPolicyWatercraftInfoXML(int CustomerID,int PolicyID, int PolicyVersionID, int BoatID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@BOAT_ID",BoatID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyWatercraftInfo");
			
				//return dsTemp.GetXml();
				return ClsCommon.GetXMLEncoded(dsTemp.Tables[0]);
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}

		public static string FetchPolicyUmbrellaWatercraftInfoXML(int CustomerID,int PolicyID, int PolicyVersionID, int BoatID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@BOAT_ID",BoatID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyUmbrellaWatercraftInfo");
			
				//return dsTemp.GetXml();
				return ClsCommon.GetXMLEncoded(dsTemp.Tables[0]);
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}

		/// <summary>
		/// Fetch the Policy New Boat Number.
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		/// <param name="CalledFrom"></param>
		/// <returns></returns>
		public static int GetPolicyNewBoatNumber(int CustomerID,int PolicyID,int PolicyVersionID,string CalledFrom)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);	
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);
				objDataWrapper.AddParameter("@CALLEDFROM",CalledFrom);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyNewBoatNo");
				return int.Parse(dsTemp.Tables[0].Rows[0][0].ToString());		
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}


		/// <summary>
		/// Saves the policy watercraft information.
		/// </summary>
		/// <param name="objWaterCraftInfo"></param>
		/// <returns></returns>
		public int AddPolicyWaterCraft(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftInfo objWaterCraftInfo)
		{
			string		strStoredProc	=	"Proc_InsertPolicyWatercraft";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWaterCraftInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWaterCraftInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@BOAT_NO",objWaterCraftInfo.BOAT_NO);
				objDataWrapper.AddParameter("@BOAT_NAME",null);
				objDataWrapper.AddParameter("@YEAR",objWaterCraftInfo.YEAR);
				objDataWrapper.AddParameter("@MAKE",objWaterCraftInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objWaterCraftInfo.MODEL);
				objDataWrapper.AddParameter("@HULL_ID_NO",objWaterCraftInfo.HULL_ID_NO);
				objDataWrapper.AddParameter("@STATE_REG",objWaterCraftInfo.STATE_REG);
				objDataWrapper.AddParameter("@HULL_MATERIAL",objWaterCraftInfo.HULL_MATERIAL);
				objDataWrapper.AddParameter("@FUEL_TYPE",objWaterCraftInfo.FUEL_TYPE);
				
				if(objWaterCraftInfo.DATE_PURCHASED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_PURCHASED",objWaterCraftInfo.DATE_PURCHASED);
				}
				objDataWrapper.AddParameter("@LENGTH",objWaterCraftInfo.LENGTH);
				objDataWrapper.AddParameter("@INCHES",objWaterCraftInfo.INCHES);
//				if(objWaterCraftInfo.MAX_SPEED==0.0)
//				{
//					objDataWrapper.AddParameter("@MAX_SPEED",null);
//				}
//
//				else
					objDataWrapper.AddParameter("@MAX_SPEED",objWaterCraftInfo.MAX_SPEED);
			

				objDataWrapper.AddParameter("@BERTH_LOC",objWaterCraftInfo.BERTH_LOC);
				objDataWrapper.AddParameter("@WATERS_NAVIGATED",objWaterCraftInfo.WATERS_NAVIGATED);
				objDataWrapper.AddParameter("@TERRITORY",objWaterCraftInfo.TERRITORY);		
				objDataWrapper.AddParameter("@CREATED_BY",objWaterCraftInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objWaterCraftInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@TYPE_OF_WATERCRAFT",objWaterCraftInfo.TYPE_OF_WATERCRAFT);

				if(objWaterCraftInfo.INSURING_VALUE==0.0)
				{
					objDataWrapper.AddParameter("@INSURING_VALUE",DBNull.Value);
				}
				else
					objDataWrapper.AddParameter("@INSURING_VALUE",objWaterCraftInfo.INSURING_VALUE);

//				if(objWaterCraftInfo.WATERCRAFT_HORSE_POWER==0)
//				{
//					objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",null);
//				}
//				else		 
					objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",objWaterCraftInfo.WATERCRAFT_HORSE_POWER);

				objDataWrapper.AddParameter("@TWIN_SINGLE",objWaterCraftInfo.TWIN_SINGLE);
				objDataWrapper.AddParameter("@DESC_OTHER_WATERCRAFT",objWaterCraftInfo.DESC_OTHER_WATERCRAFT);
				objDataWrapper.AddParameter("@LORAN_NAV_SYSTEM",objWaterCraftInfo.LORAN_NAV_SYSTEM );
				objDataWrapper.AddParameter("@DIESEL_ENGINE",objWaterCraftInfo.DIESEL_ENGINE );
				objDataWrapper.AddParameter("@SHORE_STATION",objWaterCraftInfo.SHORE_STATION );
				objDataWrapper.AddParameter("@HALON_FIRE_EXT_SYSTEM",objWaterCraftInfo.HALON_FIRE_EXT_SYSTEM );
				objDataWrapper.AddParameter("@DUAL_OWNERSHIP",objWaterCraftInfo.DUAL_OWNERSHIP );
				objDataWrapper.AddParameter("@REMOVE_SAILBOAT",objWaterCraftInfo.REMOVE_SAILBOAT);
				objDataWrapper.AddParameter("@COV_TYPE_BASIS",objWaterCraftInfo.COV_TYPE_BASIS);
				objDataWrapper.AddParameter("@PHOTO_ATTACHED",objWaterCraftInfo.PHOTO_ATTACHED);
				objDataWrapper.AddParameter("@MARINE_SURVEY",objWaterCraftInfo.MARINE_SURVEY);
				if(objWaterCraftInfo.DATE_MARINE_SURVEY!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_MARINE_SURVEY",objWaterCraftInfo.DATE_MARINE_SURVEY);
				}

				objDataWrapper.AddParameter("@LOCATION_ADDRESS",objWaterCraftInfo.LOCATION_ADDRESS);
				objDataWrapper.AddParameter("@LOCATION_CITY",objWaterCraftInfo.LOCATION_CITY);
				objDataWrapper.AddParameter("@LOCATION_STATE",objWaterCraftInfo.LOCATION_STATE);
				objDataWrapper.AddParameter("@LOCATION_ZIP",objWaterCraftInfo.LOCATION_ZIP);
				objDataWrapper.AddParameter("@LAY_UP_PERIOD_FROM_DAY",objWaterCraftInfo.LAY_UP_PERIOD_FROM_DAY);
				objDataWrapper.AddParameter("@LAY_UP_PERIOD_FROM_MONTH",objWaterCraftInfo.LAY_UP_PERIOD_FROM_MONTH);
				objDataWrapper.AddParameter("@LAY_UP_PERIOD_TO_DAY",objWaterCraftInfo.LAY_UP_PERIOD_TO_DAY);
				objDataWrapper.AddParameter("@LAY_UP_PERIOD_TO_MONTH",objWaterCraftInfo.LAY_UP_PERIOD_TO_MONTH);
				
				if(objWaterCraftInfo.LOSSREPORT_ORDER != 0)
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",objWaterCraftInfo.LOSSREPORT_ORDER );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",System.DBNull.Value );

				if(objWaterCraftInfo.LOSSREPORT_DATETIME != DateTime.MinValue)
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",objWaterCraftInfo.LOSSREPORT_DATETIME );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",System.DBNull.Value );
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@BOAT_ID",objWaterCraftInfo.BOAT_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objWaterCraftInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"Policies\aspx\Watercraft\PolicyAddWatercraftInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objWaterCraftInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = objWaterCraftInfo.CUSTOMER_ID;
					objTransactionInfo.POLICY_ID = objWaterCraftInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objWaterCraftInfo.POLICY_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objWaterCraftInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New watercraft's boat is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int BOAT_ID = int.Parse(objSqlParameter.Value.ToString());

				#region Assigned Boats Process watercraft :Mofified 13 Sep 2007
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objWaterCraftInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objWaterCraftInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@BOAT_ID",BOAT_ID);
				returnResult	= objDataWrapper.ExecuteNonQuery("Proc_SetAssgndDriverBoatType_Pol");
				objDataWrapper.ClearParameteres();
				
				#endregion
				
				
				if ( BOAT_ID > 0 )
				{
					/*//Insert/delete relevant coverages*********************	
					objDataWrapper.AddParameter("@POL_ID",objWaterCraftInfo.POLICY_ID );
					objDataWrapper.AddParameter("@POL_VERSION_ID",objWaterCraftInfo.POLICY_VERSION_ID );
					objDataWrapper.AddParameter("@BOAT_ID",BOAT_ID);
					objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);

					objDataWrapper.ExecuteNonQuery("Proc_Update_POL_WATERCRAFT_COVERAGES");
					//*********************************************************

					//Update endorsements//////////////////////////////////////////
					objDataWrapper.ClearParameteres();

					objDataWrapper.AddParameter("@POLICY_ID",objWaterCraftInfo.POLICY_ID);
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWaterCraftInfo.POLICY_VERSION_ID);
					objDataWrapper.AddParameter("@BOAT_ID",BOAT_ID);
					objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);

					objDataWrapper.ExecuteNonQuery("Proc_Update_WATERCRAFT_ENDORSEMENTS_POLICY");
					/////////////////////////////////////////////////////////////
					objDataWrapper.ClearParameteres();

					objDataWrapper.AddParameter("@POLICY_ID",objWaterCraftInfo.POLICY_ID );
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWaterCraftInfo.POLICY_VERSION_ID);
					objDataWrapper.AddParameter("@BOAT_ID",BOAT_ID);
					objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);
					objDataWrapper.ExecuteNonQuery("Proc_UPDATE_WATERCRAFT_HOME_COVERAGES_POLICY");*/

					ClsWatercraftCoverages objWatCov = new ClsWatercraftCoverages();
					objWatCov.SaveDefaultCoveragesPolicy(objDataWrapper,objWaterCraftInfo.CUSTOMER_ID,
														objWaterCraftInfo.POLICY_ID,objWaterCraftInfo.POLICY_VERSION_ID,
														BOAT_ID);

				}
				ClsGeneralInformation obj=new ClsGeneralInformation();
				string lobId="";
				
				lobId= (obj.GetPolicyLOBID(objWaterCraftInfo.CUSTOMER_ID, objWaterCraftInfo.POLICY_ID 
					, objWaterCraftInfo.POLICY_VERSION_ID )).Tables[0].Rows[0][0].ToString();
				if(lobId == ((int)enumLOB.HOME).ToString())
				{
					ClsHomeCoverages objCov=new ClsHomeCoverages();
					objDataWrapper.ClearParameteres();
					objCov.UpdateCoveragesByRulePolicy(objDataWrapper,objWaterCraftInfo.CUSTOMER_ID,objWaterCraftInfo.POLICY_ID 
						, objWaterCraftInfo.POLICY_VERSION_ID,RuleType.LobDependent);
				}


				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
				if (BOAT_ID == -1)
				{
					return -1;
				}
				else if(BOAT_ID == -2)
				{
					return -2;
				}
				else
				{
					objWaterCraftInfo.BOAT_ID = BOAT_ID;
					return BOAT_ID;
				}

				
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


		public int AddPolicyUmbrellaWaterCraft(Cms.Model.Policy.Umbrella.ClsWaterCraftInfo objWaterCraftInfo)
		{
			string		strStoredProc	=	"Proc_InsertPolicyUmbrellaWatercraft";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWaterCraftInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWaterCraftInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@BOAT_NO",objWaterCraftInfo.BOAT_NO);
				objDataWrapper.AddParameter("@BOAT_NAME",null);
				objDataWrapper.AddParameter("@YEAR",objWaterCraftInfo.YEAR);
				objDataWrapper.AddParameter("@MAKE",objWaterCraftInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objWaterCraftInfo.MODEL);
				objDataWrapper.AddParameter("@HULL_ID_NO",objWaterCraftInfo.HULL_ID_NO);
				objDataWrapper.AddParameter("@STATE_REG",objWaterCraftInfo.STATE_REG);
				objDataWrapper.AddParameter("@HULL_MATERIAL",objWaterCraftInfo.HULL_MATERIAL);
				objDataWrapper.AddParameter("@FUEL_TYPE",objWaterCraftInfo.FUEL_TYPE);
				objDataWrapper.AddParameter("@COV_TYPE_BASIS",objWaterCraftInfo.COV_TYPE_BASIS);
				objDataWrapper.AddParameter("@LOCATION_ADDRESS",objWaterCraftInfo.LOCATION_ADDRESS);
				objDataWrapper.AddParameter("@LOCATION_CITY",objWaterCraftInfo.LOCATION_CITY);
				objDataWrapper.AddParameter("@LOCATION_STATE",objWaterCraftInfo.LOCATION_STATE);
				objDataWrapper.AddParameter("@LOCATION_ZIP",objWaterCraftInfo.LOCATION_ZIP);
				
				if(objWaterCraftInfo.DATE_PURCHASED!=DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_PURCHASED",objWaterCraftInfo.DATE_PURCHASED);
				}
				objDataWrapper.AddParameter("@LENGTH",objWaterCraftInfo.LENGTH);
				//objDataWrapper.AddParameter("@INCHES",objWaterCraftInfo.INCHES);
//				if(objWaterCraftInfo.MAX_SPEED==0.0)
//				{
//					objDataWrapper.AddParameter("@MAX_SPEED",null);
//				}
//
//				else
					objDataWrapper.AddParameter("@MAX_SPEED",objWaterCraftInfo.MAX_SPEED);
			

				objDataWrapper.AddParameter("@BERTH_LOC",objWaterCraftInfo.BERTH_LOC);
				objDataWrapper.AddParameter("@WATERS_NAVIGATED",objWaterCraftInfo.WATERS_NAVIGATED);
				objDataWrapper.AddParameter("@TERRITORY",objWaterCraftInfo.TERRITORY);		
				objDataWrapper.AddParameter("@CREATED_BY",objWaterCraftInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objWaterCraftInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@TYPE_OF_WATERCRAFT",objWaterCraftInfo.TYPE_OF_WATERCRAFT);

//				if(objWaterCraftInfo.INSURING_VALUE==0)
//				{
//					objDataWrapper.AddParameter("@INSURING_VALUE",null);
//				}
//				else
					objDataWrapper.AddParameter("@INSURING_VALUE",objWaterCraftInfo.INSURING_VALUE);

//				if(objWaterCraftInfo.WATERCRAFT_HORSE_POWER==0)
//				{
//					objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",null);
//				}
//				else		 
					objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",objWaterCraftInfo.WATERCRAFT_HORSE_POWER);

				//objDataWrapper.AddParameter("@TWIN_SINGLE",objWaterCraftInfo.TWIN_SINGLE);
				objDataWrapper.AddParameter("@DESC_OTHER_WATERCRAFT",objWaterCraftInfo.DESC_OTHER_WATERCRAFT);
				objDataWrapper.AddParameter("@LORAN_NAV_SYSTEM",objWaterCraftInfo.LORAN_NAV_SYSTEM );
				objDataWrapper.AddParameter("@DIESEL_ENGINE",objWaterCraftInfo.DIESEL_ENGINE );
				objDataWrapper.AddParameter("@SHORE_STATION",objWaterCraftInfo.SHORE_STATION );
				objDataWrapper.AddParameter("@HALON_FIRE_EXT_SYSTEM",objWaterCraftInfo.HALON_FIRE_EXT_SYSTEM );
				objDataWrapper.AddParameter("@DUAL_OWNERSHIP",objWaterCraftInfo.DUAL_OWNERSHIP );
				objDataWrapper.AddParameter("@REMOVE_SAILBOAT",objWaterCraftInfo.REMOVE_SAILBOAT);
				objDataWrapper.AddParameter("@TWIN_SINGLE",objWaterCraftInfo.TWIN_SINGLE);
				objDataWrapper.AddParameter("@INCHES",objWaterCraftInfo.INCHES);
				objDataWrapper.AddParameter("@USED_PARTICIPATE",objWaterCraftInfo.USED_PARTICIPATE);
				objDataWrapper.AddParameter("@WATERCRAFT_CONTEST",objWaterCraftInfo.WATERCRAFT_CONTEST);
				objDataWrapper.AddParameter("@OTHER_POLICY",objWaterCraftInfo.OTHER_POLICY);
				objDataWrapper.AddParameter("@IS_BOAT_EXCLUDED",objWaterCraftInfo.IS_BOAT_EXCLUDED);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@BOAT_ID",objWaterCraftInfo.BOAT_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objWaterCraftInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"Policies\aspx\Watercraft\PolicyAddWatercraftInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objWaterCraftInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = objWaterCraftInfo.CUSTOMER_ID;
					objTransactionInfo.POLICY_ID = objWaterCraftInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objWaterCraftInfo.POLICY_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objWaterCraftInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New watercraft's boat is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int BOAT_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				
				if ( BOAT_ID > 0 )
				{
					//Insert/delete relevant coverages*********************	
					/*objDataWrapper.AddParameter("@POL_ID",objWaterCraftInfo.POLICY_ID );
					objDataWrapper.AddParameter("@POL_VERSION_ID",objWaterCraftInfo.POLICY_VERSION_ID );
					objDataWrapper.AddParameter("@BOAT_ID",BOAT_ID);
					objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);

					objDataWrapper.ExecuteNonQuery("Proc_Update_POL_WATERCRAFT_COVERAGES");
					//*********************************************************

					//Update endorsements//////////////////////////////////////////
					objDataWrapper.ClearParameteres();

					objDataWrapper.AddParameter("@POLICY_ID",objWaterCraftInfo.POLICY_ID);
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWaterCraftInfo.POLICY_VERSION_ID);
					objDataWrapper.AddParameter("@BOAT_ID",BOAT_ID);
					objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);

					objDataWrapper.ExecuteNonQuery("Proc_Update_WATERCRAFT_ENDORSEMENTS_POLICY");*/
					/////////////////////////////////////////////////////////////
					///
					ClsUmbrellaRecrVeh objUmbRecVeh = new ClsUmbrellaRecrVeh();
					objUmbRecVeh.UmbrellaEndorsementRule(objDataWrapper,objWaterCraftInfo.CUSTOMER_ID,objWaterCraftInfo.POLICY_ID,objWaterCraftInfo.POLICY_VERSION_ID,clsapplication.CALLED_FROM_POLICY,ClsUmbSchRecords.CALLED_FROM_BOAT);	
				}

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
				if (BOAT_ID == -1)
				{
					return -1;
				}
				else if(BOAT_ID == -2)
				{
					return -2;
				}
				else
				{
					objWaterCraftInfo.BOAT_ID = BOAT_ID;
					return BOAT_ID;
				}

				
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

		/// <summary>
		/// Updates the policy watercraft information.
		/// </summary>
		/// <param name="objOldWaterCraftInfo"></param>
		/// <param name="objWaterCraftInfo"></param>
		/// <returns></returns>
		public int UpdatePolicyWaterCraft(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftInfo objOldWaterCraftInfo,Cms.Model.Policy.Watercraft.ClsPolicyWatercraftInfo objWaterCraftInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string		strStoredProc	=	"Proc_UpdatePolicyWatercraft";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWaterCraftInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWaterCraftInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@BOAT_ID",objWaterCraftInfo.BOAT_ID);
				objDataWrapper.AddParameter("@BOAT_NO",objWaterCraftInfo.BOAT_NO);
				objDataWrapper.AddParameter("@BOAT_NAME",null);
				objDataWrapper.AddParameter("@YEAR",objWaterCraftInfo.YEAR);
				objDataWrapper.AddParameter("@MAKE",objWaterCraftInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objWaterCraftInfo.MODEL);
				objDataWrapper.AddParameter("@HULL_ID_NO",objWaterCraftInfo.HULL_ID_NO);
				objDataWrapper.AddParameter("@STATE_REG",objWaterCraftInfo.STATE_REG);
				objDataWrapper.AddParameter("@HULL_MATERIAL",objWaterCraftInfo.HULL_MATERIAL);
				objDataWrapper.AddParameter("@FUEL_TYPE",objWaterCraftInfo.FUEL_TYPE);

				if(objWaterCraftInfo.DATE_PURCHASED!= DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_PURCHASED",objWaterCraftInfo.DATE_PURCHASED);
				}
				else
				{
					objDataWrapper.AddParameter("@DATE_PURCHASED",null);
				}

				objDataWrapper.AddParameter("@LENGTH",objWaterCraftInfo.LENGTH); 
				objDataWrapper.AddParameter("@INCHES",objWaterCraftInfo.INCHES);
//				if(objWaterCraftInfo.MAX_SPEED==0.0)
//				{
//					objDataWrapper.AddParameter("@MAX_SPEED",null);
//				}
//
//				else
					objDataWrapper.AddParameter("@MAX_SPEED",objWaterCraftInfo.MAX_SPEED);
				
				objDataWrapper.AddParameter("@BERTH_LOC",objWaterCraftInfo.BERTH_LOC);

				objDataWrapper.AddParameter("@WATERS_NAVIGATED",objWaterCraftInfo.WATERS_NAVIGATED);
				objDataWrapper.AddParameter("@TERRITORY",objWaterCraftInfo.TERRITORY);						 
				objDataWrapper.AddParameter("@MODIFIED_BY",objWaterCraftInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWaterCraftInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@TYPE_OF_WATERCRAFT",objWaterCraftInfo.TYPE_OF_WATERCRAFT);
				
				if(objWaterCraftInfo.INSURING_VALUE==0)
				{
					objDataWrapper.AddParameter("@INSURING_VALUE",DBNull.Value);
				}
				else
					objDataWrapper.AddParameter("@INSURING_VALUE",objWaterCraftInfo.INSURING_VALUE);

//				if(objWaterCraftInfo.WATERCRAFT_HORSE_POWER==0)
//				{
//					objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",null);
//				}
//				else		 
					objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",objWaterCraftInfo.WATERCRAFT_HORSE_POWER);
				
				objDataWrapper.AddParameter("@TWIN_SINGLE",objWaterCraftInfo.TWIN_SINGLE);
				objDataWrapper.AddParameter("@DESC_OTHER_WATERCRAFT",objWaterCraftInfo.DESC_OTHER_WATERCRAFT);
				objDataWrapper.AddParameter("@LORAN_NAV_SYSTEM",objWaterCraftInfo.LORAN_NAV_SYSTEM );
				objDataWrapper.AddParameter("@DIESEL_ENGINE",objWaterCraftInfo.DIESEL_ENGINE );
				objDataWrapper.AddParameter("@SHORE_STATION",objWaterCraftInfo.SHORE_STATION );
				objDataWrapper.AddParameter("@HALON_FIRE_EXT_SYSTEM",objWaterCraftInfo.HALON_FIRE_EXT_SYSTEM );
				objDataWrapper.AddParameter("@DUAL_OWNERSHIP",objWaterCraftInfo.DUAL_OWNERSHIP );
				objDataWrapper.AddParameter("@REMOVE_SAILBOAT",objWaterCraftInfo.REMOVE_SAILBOAT);
				objDataWrapper.AddParameter("@COV_TYPE_BASIS",objWaterCraftInfo.COV_TYPE_BASIS);
				objDataWrapper.AddParameter("@PHOTO_ATTACHED",objWaterCraftInfo.PHOTO_ATTACHED);
				objDataWrapper.AddParameter("@MARINE_SURVEY",objWaterCraftInfo.MARINE_SURVEY);
				if(objWaterCraftInfo.DATE_MARINE_SURVEY!= DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_MARINE_SURVEY",objWaterCraftInfo.DATE_MARINE_SURVEY);
				}
				else
				{
					objDataWrapper.AddParameter("@DATE_MARINE_SURVEY",null);
				}
				
				objDataWrapper.AddParameter("@LOCATION_ADDRESS",objWaterCraftInfo.LOCATION_ADDRESS);
				objDataWrapper.AddParameter("@LOCATION_CITY",objWaterCraftInfo.LOCATION_CITY);
				objDataWrapper.AddParameter("@LOCATION_STATE",objWaterCraftInfo.LOCATION_STATE);
				objDataWrapper.AddParameter("@LOCATION_ZIP",objWaterCraftInfo.LOCATION_ZIP);
				objDataWrapper.AddParameter("@LAY_UP_PERIOD_FROM_DAY",objWaterCraftInfo.LAY_UP_PERIOD_FROM_DAY);
				objDataWrapper.AddParameter("@LAY_UP_PERIOD_FROM_MONTH",objWaterCraftInfo.LAY_UP_PERIOD_FROM_MONTH);
				objDataWrapper.AddParameter("@LAY_UP_PERIOD_TO_DAY",objWaterCraftInfo.LAY_UP_PERIOD_TO_DAY);
				objDataWrapper.AddParameter("@LAY_UP_PERIOD_TO_MONTH",objWaterCraftInfo.LAY_UP_PERIOD_TO_MONTH);
				if(objWaterCraftInfo.LOSSREPORT_ORDER != 0)
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",objWaterCraftInfo.LOSSREPORT_ORDER );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_ORDER",System.DBNull.Value );

				if(objWaterCraftInfo.LOSSREPORT_DATETIME != DateTime.MinValue)
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",objWaterCraftInfo.LOSSREPORT_DATETIME );
				else
					objDataWrapper.AddParameter("@LOSSREPORT_DATETIME",System.DBNull.Value );
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RET_VAL",null,SqlDbType.Int,ParameterDirection.ReturnValue);

				if(TransactionLog) 
				{
					objWaterCraftInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\Watercraft\PolicyAddWatercraftInformation.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldWaterCraftInfo,objWaterCraftInfo);
					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.CLIENT_ID = objWaterCraftInfo.CUSTOMER_ID;
						objTransactionInfo.POLICY_ID = objWaterCraftInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID = objWaterCraftInfo.POLICY_VERSION_ID;
						objTransactionInfo.RECORDED_BY		=	objWaterCraftInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Watercraft's boat is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				if(objSqlParameter!=null && objSqlParameter.Value!="")
					returnResult = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
			/*
				//Insert/delete relevant coverages*********************	
				objDataWrapper.AddParameter("@POL_ID",objWaterCraftInfo.POLICY_ID );
				objDataWrapper.AddParameter("@POL_VERSION_ID",objWaterCraftInfo.POLICY_VERSION_ID );
				objDataWrapper.AddParameter("@BOAT_ID",objWaterCraftInfo.BOAT_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);

				objDataWrapper.ExecuteNonQuery("Proc_Update_POL_WATERCRAFT_COVERAGES");
				//*********************************************************

				//Update endorsements//////////////////////////////////////////
				objDataWrapper.ClearParameteres();

				objDataWrapper.AddParameter("@POLICY_ID",objWaterCraftInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWaterCraftInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@BOAT_ID",objWaterCraftInfo.BOAT_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);

				objDataWrapper.ExecuteNonQuery("Proc_Update_WATERCRAFT_ENDORSEMENTS_POLICY");
				/////////////////////////////////////////////////////////////
				objDataWrapper.ClearParameteres();

				objDataWrapper.AddParameter("@POLICY_ID",objWaterCraftInfo.POLICY_ID );
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWaterCraftInfo.POLICY_VERSION_ID );
				objDataWrapper.AddParameter("@BOAT_ID",objWaterCraftInfo.BOAT_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);

				objDataWrapper.ExecuteNonQuery("Proc_UPDATE_WATERCRAFT_HOME_COVERAGES_POLICY");*/

				ClsWatercraftCoverages objWatCov = new ClsWatercraftCoverages();
				objWatCov.UpdateCoveragesByRulePolicy(objDataWrapper,objWaterCraftInfo.CUSTOMER_ID,
														objWaterCraftInfo.POLICY_ID,
														objWaterCraftInfo.POLICY_VERSION_ID,
														RuleType.RiskDependent, 
														objWaterCraftInfo.BOAT_ID);
				ClsGeneralInformation obj=new ClsGeneralInformation();
				string lobId="";
				
				lobId= (obj.GetPolicyLOBID(objWaterCraftInfo.CUSTOMER_ID, objWaterCraftInfo.POLICY_ID 
					, objWaterCraftInfo.POLICY_VERSION_ID )).Tables[0].Rows[0][0].ToString();
				if(lobId == ((int)enumLOB.HOME).ToString())
				{
					//added by Pravesh to update Lobdependent Coverages
					objDataWrapper.ClearParameteres();
					objWatCov.UpdateCoveragesByRulePolicy(objDataWrapper,objWaterCraftInfo.CUSTOMER_ID,
						objWaterCraftInfo.POLICY_ID,
						objWaterCraftInfo.POLICY_VERSION_ID,
						RuleType.LobDependent, 
						objWaterCraftInfo.BOAT_ID);
					//
					ClsHomeCoverages objCov=new ClsHomeCoverages();
					objDataWrapper.ClearParameteres();
					objCov.UpdateCoveragesByRulePolicy(objDataWrapper,objWaterCraftInfo.CUSTOMER_ID,objWaterCraftInfo.POLICY_ID 
					, objWaterCraftInfo.POLICY_VERSION_ID,RuleType.LobDependent);
				}


				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) 
				{
					objDataWrapper.Dispose();
				}
				if(objBuilder != null) 
				{
					objBuilder = null;
				}
			}
		}


		public int UpdatePolicyUmbrellaWaterCraft(Cms.Model.Policy.Umbrella.ClsWaterCraftInfo objOldWaterCraftInfo,Cms.Model.Policy.Umbrella.ClsWaterCraftInfo objWaterCraftInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string		strStoredProc	=	"Proc_UpdatePolicyUmbrellaWatercraft";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWaterCraftInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWaterCraftInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@BOAT_ID",objWaterCraftInfo.BOAT_ID);
				objDataWrapper.AddParameter("@BOAT_NO",objWaterCraftInfo.BOAT_NO);
				objDataWrapper.AddParameter("@BOAT_NAME",null);
				objDataWrapper.AddParameter("@YEAR",objWaterCraftInfo.YEAR);
				objDataWrapper.AddParameter("@MAKE",objWaterCraftInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objWaterCraftInfo.MODEL);
				objDataWrapper.AddParameter("@HULL_ID_NO",objWaterCraftInfo.HULL_ID_NO);
				objDataWrapper.AddParameter("@STATE_REG",objWaterCraftInfo.STATE_REG);
				objDataWrapper.AddParameter("@HULL_MATERIAL",objWaterCraftInfo.HULL_MATERIAL);
				objDataWrapper.AddParameter("@FUEL_TYPE",objWaterCraftInfo.FUEL_TYPE);
				objDataWrapper.AddParameter("@COV_TYPE_BASIS",objWaterCraftInfo.COV_TYPE_BASIS);
				objDataWrapper.AddParameter("@LOCATION_ADDRESS",objWaterCraftInfo.LOCATION_ADDRESS);
				objDataWrapper.AddParameter("@LOCATION_CITY",objWaterCraftInfo.LOCATION_CITY);
				objDataWrapper.AddParameter("@LOCATION_STATE",objWaterCraftInfo.LOCATION_STATE);
				objDataWrapper.AddParameter("@LOCATION_ZIP",objWaterCraftInfo.LOCATION_ZIP);

				if(objWaterCraftInfo.DATE_PURCHASED!= DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@DATE_PURCHASED",objWaterCraftInfo.DATE_PURCHASED);
				}
				else
				{
					objDataWrapper.AddParameter("@DATE_PURCHASED",null);
				}

				objDataWrapper.AddParameter("@LENGTH",objWaterCraftInfo.LENGTH); 
				//objDataWrapper.AddParameter("@INCHES",objWaterCraftInfo.INCHES);
//				if(objWaterCraftInfo.MAX_SPEED==0.0)
//				{
//					objDataWrapper.AddParameter("@MAX_SPEED",null);
//				}
//
//				else
					objDataWrapper.AddParameter("@MAX_SPEED",objWaterCraftInfo.MAX_SPEED);
				
				objDataWrapper.AddParameter("@BERTH_LOC",objWaterCraftInfo.BERTH_LOC);

				objDataWrapper.AddParameter("@WATERS_NAVIGATED",objWaterCraftInfo.WATERS_NAVIGATED);
				objDataWrapper.AddParameter("@TERRITORY",objWaterCraftInfo.TERRITORY);						 
				objDataWrapper.AddParameter("@MODIFIED_BY",objWaterCraftInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWaterCraftInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@TYPE_OF_WATERCRAFT",objWaterCraftInfo.TYPE_OF_WATERCRAFT);
				
//				if(objWaterCraftInfo.INSURING_VALUE==0)
//				{
//					objDataWrapper.AddParameter("@INSURING_VALUE",null);
//				}
//				else
					objDataWrapper.AddParameter("@INSURING_VALUE",objWaterCraftInfo.INSURING_VALUE);

//				if(objWaterCraftInfo.WATERCRAFT_HORSE_POWER==0)
//				{
//					objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",null);
//				}
//				else		 
					objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",objWaterCraftInfo.WATERCRAFT_HORSE_POWER);
				
				//objDataWrapper.AddParameter("@TWIN_SINGLE",objWaterCraftInfo.TWIN_SINGLE);
				objDataWrapper.AddParameter("@DESC_OTHER_WATERCRAFT",objWaterCraftInfo.DESC_OTHER_WATERCRAFT);
				objDataWrapper.AddParameter("@LORAN_NAV_SYSTEM",objWaterCraftInfo.LORAN_NAV_SYSTEM );
				objDataWrapper.AddParameter("@DIESEL_ENGINE",objWaterCraftInfo.DIESEL_ENGINE );
				objDataWrapper.AddParameter("@SHORE_STATION",objWaterCraftInfo.SHORE_STATION );
				objDataWrapper.AddParameter("@HALON_FIRE_EXT_SYSTEM",objWaterCraftInfo.HALON_FIRE_EXT_SYSTEM );
				objDataWrapper.AddParameter("@DUAL_OWNERSHIP",objWaterCraftInfo.DUAL_OWNERSHIP );
				objDataWrapper.AddParameter("@REMOVE_SAILBOAT",objWaterCraftInfo.REMOVE_SAILBOAT);
				objDataWrapper.AddParameter("@TWIN_SINGLE",objWaterCraftInfo.TWIN_SINGLE);
				objDataWrapper.AddParameter("@INCHES",objWaterCraftInfo.INCHES);
				objDataWrapper.AddParameter("@USED_PARTICIPATE",objWaterCraftInfo.USED_PARTICIPATE);
				objDataWrapper.AddParameter("@WATERCRAFT_CONTEST",objWaterCraftInfo.WATERCRAFT_CONTEST);
				objDataWrapper.AddParameter("@OTHER_POLICY",objWaterCraftInfo.OTHER_POLICY);
				objDataWrapper.AddParameter("@IS_BOAT_EXCLUDED",objWaterCraftInfo.IS_BOAT_EXCLUDED);

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RET_VAL",null,SqlDbType.Int,ParameterDirection.ReturnValue);

				if(TransactionLog) 
				{
					objWaterCraftInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\Watercraft\PolicyAddWatercraftInformation.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldWaterCraftInfo,objWaterCraftInfo);
					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.CLIENT_ID = objWaterCraftInfo.CUSTOMER_ID;
						objTransactionInfo.POLICY_ID = objWaterCraftInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID = objWaterCraftInfo.POLICY_VERSION_ID;
						objTransactionInfo.RECORDED_BY		=	objWaterCraftInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Watercraft's boat is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				objDataWrapper.ClearParameteres();
				//Insert/delete relevant coverages*********************	
				/*objDataWrapper.AddParameter("@POL_ID",objWaterCraftInfo.POLICY_ID );
				objDataWrapper.AddParameter("@POL_VERSION_ID",objWaterCraftInfo.POLICY_VERSION_ID );
				objDataWrapper.AddParameter("@BOAT_ID",objWaterCraftInfo.BOAT_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);

				objDataWrapper.ExecuteNonQuery("Proc_Update_POL_WATERCRAFT_COVERAGES");
				//*********************************************************

				//Update endorsements//////////////////////////////////////////
				objDataWrapper.ClearParameteres();

				objDataWrapper.AddParameter("@POLICY_ID",objWaterCraftInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWaterCraftInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@BOAT_ID",objWaterCraftInfo.BOAT_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWaterCraftInfo.CUSTOMER_ID);

				objDataWrapper.ExecuteNonQuery("Proc_Update_WATERCRAFT_ENDORSEMENTS_POLICY");*/
				/////////////////////////////////////////////////////////////

				if(objSqlParameter!=null && objSqlParameter.Value.ToString()!="")
					returnResult = int.Parse(objSqlParameter.Value.ToString());

				if(returnResult>0)
				{
					ClsUmbrellaRecrVeh objUmbRecVeh = new ClsUmbrellaRecrVeh();
					objUmbRecVeh.UmbrellaEndorsementRule(objDataWrapper,objWaterCraftInfo.CUSTOMER_ID,objWaterCraftInfo.POLICY_ID,objWaterCraftInfo.POLICY_VERSION_ID,clsapplication.CALLED_FROM_POLICY,ClsUmbSchRecords.CALLED_FROM_BOAT);	
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) 
				{
					objDataWrapper.Dispose();
				}
				if(objBuilder != null) 
				{
					objBuilder = null;
				}
			}
		}

		/// <summary>
		/// Delete the policy watercraft information.
		/// </summary>
		/// <param name="strCustomerId"></param>
		/// <param name="strPolicyId"></param>
		/// <param name="strPolicyVerId"></param>
		/// <param name="strBoatID"></param>
		/// <returns></returns>
		public int DeletePolicyWaterCraft(string strCustomerId, string strPolicyId, string strPolicyVerId,string strBoatID,string user_id,string strCustInfo)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			string	strStoredProc =	"Proc_DeletePolicyWatercraft";
			objDataWrapper.AddParameter("@CUSTOMER_ID",strCustomerId);
			objDataWrapper.AddParameter("@POLICY_ID",strPolicyId);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",strPolicyVerId);
			objDataWrapper.AddParameter("@BOAT_ID",strBoatID);
			
			//Added by Swastika on 30th Mar'06 to check for assigned operator before deletion
			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RESULT",SqlDbType.Int,ParameterDirection.Output);
			//string strCustInfo = "Policy Number    : " + strPolicyId  + ";Policy_Version_Id : " + strPolicyVerId;
			int intRetResult = 0;
			if(TransactionLogRequired)
			{	
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();	
				Cms.Model.Policy.Watercraft.ClsPolicyWatercraftInfo objWatercraftInfo = new Cms.Model.Policy.Watercraft.ClsPolicyWatercraftInfo();      																	 
				//Added For Itrack Issue #5479
				objTransactionInfo.RECORDED_BY = objWatercraftInfo.CREATED_BY; 
				objTransactionInfo.CLIENT_ID  = int.Parse(strCustomerId.ToString()) ;
				objTransactionInfo.POLICY_ID  = int.Parse(strPolicyId.ToString()); 
				objTransactionInfo.POLICY_VER_TRACKING_ID  = int.Parse(strPolicyVerId.ToString());	
				objTransactionInfo.RECORDED_BY = int.Parse(user_id); 
				objTransactionInfo.TRANS_DESC = "Watercraft  is deleted.";  
				objTransactionInfo.CUSTOM_INFO = strCustInfo; 
				intRetResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);     
			}
			else
			{
				intRetResult = objDataWrapper.ExecuteNonQuery(strStoredProc);    
			}

			//int intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);	
			
			int 	intResult = int.Parse(objSqlParameter.Value.ToString());
			if(intResult==-2)
			{
				intRetResult = intResult;
			}

			objDataWrapper.ClearParameteres();

			//Updates relevant home coverages and endorsments on deleting a watercraft/////
			ClsWatercraftCoverages objWater = new ClsWatercraftCoverages();
			objWater.UpdatePolicyCoveragesAndEndorsementsOnDelete(Convert.ToInt32(strCustomerId),Convert.ToInt32(strPolicyId),Convert.ToInt32(strPolicyVerId),objDataWrapper);
			////////////////
			
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);		
			return intRetResult;
		
		}
		public int DeletePolicyUmbrellaWaterCraft(string strCustomerId, string strPolicyId, string strPolicyVerId,string strBoatID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			string	strStoredProc =	"Proc_DeletePolicyUmbrellaWatercraft";
			objDataWrapper.AddParameter("@CUSTOMER_ID",strCustomerId);
			objDataWrapper.AddParameter("@POLICY_ID",strPolicyId);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",strPolicyVerId);
			objDataWrapper.AddParameter("@BOAT_ID",strBoatID);
			int intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);	
			if(intResult>0)
			{
				ClsUmbrellaRecrVeh objUmbRecVeh = new ClsUmbrellaRecrVeh();
				objUmbRecVeh.UmbrellaEndorsementRule(objDataWrapper,int.Parse(strCustomerId),int.Parse(strPolicyId),int.Parse(strPolicyVerId),clsapplication.CALLED_FROM_POLICY,ClsUmbSchRecords.CALLED_FROM_BOAT);
			}
			return intResult;
		
		}

		/// <summary>
		/// Fetch the policy Boat information.
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="policyID"></param>
		/// <param name="policyVersionID"></param>
		/// <param name="boatID"></param>
		/// <returns></returns>
		public static DataSet FetchPolicyBoatInfo(int customerID,int policyID, int policyVersionID,int boatID)
		{
			string		strStoredProc	=	"Proc_FetchPolicyBoatInfo";
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",policyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID,SqlDbType.Int);                           
				objDataWrapper.AddParameter("@boatID",boatID,SqlDbType.Int);                           
                

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				
			}
			return dsCount;
		}

		public static DataSet FetchPolicyUmbrellaBoatInfo(int customerID,int policyID, int policyVersionID,int boatID)
		{
			string		strStoredProc	=	"Proc_FetchPolicyUmbrellaBoatInfo";
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",policyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID,SqlDbType.Int);                           
				objDataWrapper.AddParameter("@boatID",boatID,SqlDbType.Int);                           
                

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				
			}
			return dsCount;
		}

		
		/// <summary>
		/// Fetch the policy watercraft operation information.
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		/// <param name="DriverID"></param>
		/// <returns></returns>
		public static string FetchPolicyWatercraftOperatorInfoXML(int CustomerID,int PolicyID, int PolicyVersionID, int DriverID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@DRIVER_ID",DriverID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyWatercraftDriverInfo");
			
				//return dsTemp.GetXml();
				return ClsCommon.GetXMLEncoded(dsTemp.Tables[0]);
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}

		
		/// <summary>
		/// Fetch the policy boat information.
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		/// <returns></returns>
		public static DataSet FetchPolicyBoatInfo(int CustomerID, int PolicyID, int PolicyVersionID)
		{
			string		strStoredProc	=	"Proc_FetchPolicyWaterCraftInfo";
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID,SqlDbType.Int);                           
                
				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				
			}
			return dsCount;
		}


		//public void ActivateDeactivateWatercraftPolicy(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftInfo objWatercraftInfo, string strStatus)
		public int ActivateDeactivateWatercraftPolicy(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftInfo objWatercraftInfo, string strStatus , string strCustINFO )
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateBoatWatercraftPolicy";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWatercraftInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWatercraftInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@BOAT_ID",objWatercraftInfo.BOAT_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);
				//Added by Swastika on 30th Mar'06
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RESULT",SqlDbType.Int,ParameterDirection.Output);
				
				int returnResult = 0;
				if(TransactionLogRequired)
				{																			
					objWatercraftInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Watercraft/PolicyAddWatercraftInformation.aspx.resx");
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID		=	objWatercraftInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID=	objWatercraftInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftInfo.MODIFIED_BY;
					
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	"Watercraft is Activated";
					else
						objTransactionInfo.TRANS_DESC		=	"Watercraft is Deactivated";
					    objTransactionInfo.CUSTOM_INFO      =    strCustINFO;
					    
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				int intResult = int.Parse(objSqlParameter.Value.ToString());
				// Swastika  : -2 can't be deactivated as boat has been assigned to trailer/equipment
				if(intResult==-2)
				{
					returnResult=intResult;
				}				
				objDataWrapper.ClearParameteres();
				//Check lob if home Update Coverage of home
				ClsGeneralInformation obj=new ClsGeneralInformation();
				string lobId="";
				lobId= obj.Fun_GetLObID(objWatercraftInfo.CUSTOMER_ID, objWatercraftInfo.POLICY_ID 
					, objWatercraftInfo.POLICY_VERSION_ID, "POLICY" );
				if(lobId == ((int)enumLOB.HOME).ToString())
				{
					ClsHomeCoverages objCoverage=new ClsHomeCoverages();
					objDataWrapper.ClearParameteres();
					objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objWatercraftInfo.CUSTOMER_ID,objWatercraftInfo.POLICY_ID  ,
						objWatercraftInfo.POLICY_VERSION_ID ,RuleType.LobDependent);
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);	
				return returnResult;
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


		public void ActivateDeactivateUmbrellaWatercraftPolicy(Cms.Model.Policy.Umbrella.ClsWaterCraftInfo objWatercraftInfo, string strStatus , string strCustINFO )
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateUmbrellaWatercraftPolicy";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWatercraftInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWatercraftInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@BOAT_ID",objWatercraftInfo.BOAT_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

				int returnResult = 0;
				if(TransactionLogRequired)
				{																			
					objWatercraftInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Watercraft/PolicyAddWatercraftInformation.aspx.resx");
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID		=	objWatercraftInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID=	objWatercraftInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftInfo.MODIFIED_BY;
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	"Watercraft is Activated";
					else
						objTransactionInfo.TRANS_DESC		=	"Watercraft is Deactivated";					    
					   objTransactionInfo.CUSTOM_INFO       =    strCustINFO;
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}			
				if(returnResult>0)
				{
					ClsUmbrellaRecrVeh objUmbRecVeh = new ClsUmbrellaRecrVeh();
					objUmbRecVeh.UmbrellaEndorsementRule(objDataWrapper,objWatercraftInfo.CUSTOMER_ID,objWatercraftInfo.POLICY_ID,objWatercraftInfo.POLICY_VERSION_ID,clsapplication.CALLED_FROM_POLICY,ClsUmbSchRecords.CALLED_FROM_BOAT);
				}
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
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

		// Added by Swastika on 8th Mar'06  for Pol Iss # 59

		public int GetOperatorCountForAssignedWatercraft(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftInfo objWatercraftInfo)
		{
			string		strStoredProc	=	"Proc_GetOperatorCountForAssignedBoatPolicy";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objWatercraftInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objWatercraftInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@BOAT_ID",objWatercraftInfo.BOAT_ID);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN",null,SqlDbType.Int ,ParameterDirection.ReturnValue);				
				objDataWrapper.ExecuteNonQuery(strStoredProc);				
				objDataWrapper.ClearParameteres();
				int returnResult = Convert.ToInt32(objSqlParameter.Value);
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
				return returnResult;
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

		public int GetOperatorCountForAssignedUmbrella(Cms.Model.Policy.Umbrella.ClsWaterCraftInfo objWatercraftInfo)
		{
			string		strStoredProc	=	"Proc_GetOperatorCountForAssignedUmbBoatPolicy";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objWatercraftInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objWatercraftInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@BOAT_ID",objWatercraftInfo.BOAT_ID);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN",null,SqlDbType.Int ,ParameterDirection.ReturnValue);				
				objDataWrapper.ExecuteNonQuery(strStoredProc);				
				objDataWrapper.ClearParameteres();
				int returnResult = Convert.ToInt32(objSqlParameter.Value);
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
				return returnResult;
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

		/// <summary>
		/// Gets a  Boat Ids against an application for Umbrella
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns>Boat Ids  as a carat separated string . if no boat exist then it returns -1</returns>
		public static string  GetUMBoatIDs(int customerID, int appID, int appVersionID)
		{
			string	strStoredProc =	"Proc_GetRuleUMBoatIDs";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			string strBoatID="-1";
			if(ds.Tables[0]!=null && ds.Tables[0].Rows !=null)
			{
				int intCount=ds.Tables[0].Rows.Count;				
				for(int i=0;i<intCount;i++)
				{
					if(i==0)
					{
						strBoatID=ds.Tables[0].Rows[i][0].ToString();
					}
					else
					{
						strBoatID = strBoatID + '^'  + ds.Tables[0].Rows[i][0].ToString();
					}				
				}
			}
			return strBoatID;
		}	

		/// <summary>
		/// Gets a  Boat Ids against an application for Umbrella
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns>Boat Ids  as a carat separated string . if no boat exist then it returns -1</returns>
		public static string  GetUMBoatIDs_Pol(int customerID, int PolicyID, int PolicyVersionID)
		{
			string	strStoredProc =	"Proc_GetRuleUMBoatIDs_Pol";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICYID",PolicyID);
			objWrapper.AddParameter("@POLICYVERSIONID",PolicyVersionID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			string strBoatID="-1";
			if(ds.Tables[0]!=null && ds.Tables[0].Rows !=null)
			{
				int intCount=ds.Tables[0].Rows.Count;				
				for(int i=0;i<intCount;i++)
				{
					if(i==0)
					{
						strBoatID=ds.Tables[0].Rows[i][0].ToString();
					}
					else
					{
						strBoatID = strBoatID + '^'  + ds.Tables[0].Rows[i][0].ToString();
					}				
				}
			}
			return strBoatID;
		}
		
		/// <summary>
		/// get assign boats ID
		/// </summary>
		/// <param name="intCustomrId"></param>
		/// <param name="intAppId"></param>
		/// <param name="intAppVersionId"></param>
		/// <param name="intDriverId"></param>
		/// <returns></returns>
		public static string FillAppBoats(int intCustomrId, int intAppId,int intAppVersionId, int intDriverId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomrId);
				objDataWrapper.AddParameter("@APP_ID", intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID", intAppVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID", intDriverId);
				return objDataWrapper.ExecuteDataSet("PROC_FILLAPPVEHICLEDROPDOWN_BAOT").GetXml();
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
			finally
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
		}
		/// <summary>
		/// get app boats		/// 
		/// </summary>
		/// <param name="intCustomrId"></param>
		/// <param name="intAppId"></param>
		/// <param name="intAppVersionId"></param>
		/// <param name="intDriverId"></param>
		/// <returns></returns>
		public static DataTable GetAppBoat(int intCustomrId, int intAppId,int intAppVersionId, int intDriverId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				DataSet dsTemp = new DataSet();	
				objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomrId);
				objDataWrapper.AddParameter("@APP_ID", intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID", intAppVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID", intDriverId);
				dsTemp = objDataWrapper.ExecuteDataSet("PROC_FILLAPPVEHICLEDROPDOWN_BAOT");
				return dsTemp.Tables[0];
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
			finally
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
		}
		/// <summary>
		/// Get Pol Boat Id : Modified 13 sep 2007
		/// </summary>
		/// <param name="intCustomrId"></param>
		/// <param name="intPolId"></param>
		/// <param name="intPolVersionId"></param>
		/// <param name="intDriverId"></param>
		/// <returns></returns>
		public static DataTable GetPolBoat(int intCustomrId, int intPolId,int intPolVersionId, int intDriverId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				DataSet dsTemp = new DataSet();	
				objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomrId);
				objDataWrapper.AddParameter("@POL_ID", intPolId);
				objDataWrapper.AddParameter("@POL_VERSION_ID", intPolVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID", intDriverId);
				dsTemp = objDataWrapper.ExecuteDataSet("PROC_FILLPOLVEHICLEDROPDOWN_BOAT");
				return dsTemp.Tables[0];
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
			finally
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="intCustomrId"></param>
		/// <param name="intPolId"></param>
		/// <param name="intPolVersionId"></param>
		/// <param name="intDriverId"></param>
		/// <returns>Dsataset</returns>
		public static string FillPolBoats(int intCustomrId, int intPolId,int intPolVersionId, int intDriverId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomrId);
				objDataWrapper.AddParameter("@POL_ID", intPolId);
				objDataWrapper.AddParameter("@POL_VERSION_ID", intPolVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID", intDriverId);
				return objDataWrapper.ExecuteDataSet("PROC_FILLPOLVEHICLEDROPDOWN_BOAT").GetXml();
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
			finally
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
		}

		
		//Done for Itrack Issue 6737 on 17 Nov 09
		public static string FillAppReacreationalVehicles(int intCustomrId, int intAppId,int intAppVersionId, int intDriverId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomrId);
				objDataWrapper.AddParameter("@APP_ID", intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID", intAppVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID", intDriverId);
				return objDataWrapper.ExecuteDataSet("PROC_FILLAPP_REACREATIONALVEHICLE_DROPDOWN").GetXml();
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
			finally
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
		}

		public static string FillPolReacreationalVehicles(int intCustomrId, int intPolId,int intPolVersionId, int intDriverId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomrId);
				objDataWrapper.AddParameter("@POLICY_ID", intPolId);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID", intPolVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID", intDriverId);
				return objDataWrapper.ExecuteDataSet("PROC_FILLPOL_REACREATIONALVEHICLE_DROPDOWN").GetXml();
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
			finally
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
		}

		public static DataTable GetAppReacreationalVehicles(int intCustomrId, int intAppId,int intAppVersionId, int intDriverId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				DataSet dsTemp = new DataSet();	
				objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomrId);
				objDataWrapper.AddParameter("@APP_ID", intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID", intAppVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID", intDriverId);
				dsTemp = objDataWrapper.ExecuteDataSet("PROC_FILLAPP_REACREATIONALVEHICLE_DROPDOWN");
				return dsTemp.Tables[0];
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
			finally
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
		}

		public static DataTable GetPolReacreationalVehicles(int intCustomrId, int intPolId,int intPolVersionId, int intDriverId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				DataSet dsTemp = new DataSet();	
				objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomrId);
				objDataWrapper.AddParameter("@POLICY_ID", intPolId);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID", intPolVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID", intDriverId);
				dsTemp = objDataWrapper.ExecuteDataSet("PROC_FILLPOL_REACREATIONALVEHICLE_DROPDOWN");
				return dsTemp.Tables[0];
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
			finally
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
		}

		//Added till here
		
	}
}


