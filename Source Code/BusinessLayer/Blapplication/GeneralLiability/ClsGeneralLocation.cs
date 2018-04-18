/******************************************************************************************
<Author				: -   Priya
<Start Date				: -	8/23/2005 11:28:12 AM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Nov 09,2005
<Modified By			: - Sumit Chhabra
<Purpose				: - Additional Info in the Transaction log to record LOB name and screen name has been added
<Modified Date			: - 16/12/2005
<Modified By			: - Sumit Chhabra
<Purpose				: - Check has been added at update of data to prevent an entry from going into transaction log when no modication has taken place.
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
//using Cms.Model.Application.GeneralLiability;

namespace Cms.BusinessLayer.BlApplication.GeneralLiability
{
	/// <summary>
	/// Business Logic for Locations
	/// </summary>
	public class ClsLocations : Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
	{
		private const	string		APP_LOCATIONS			=	"APP_LOCATIONS";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
	
		//private const string ACTIVATE_DEACTIVATE_PROC	= "";
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

		#region private Utility Functions
		#endregion

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsLocations()
		{
			boolTransactionLog	= base.TransactionLogRequired;
		}
		#endregion

        #region Add(Insert) functions
//        /// <summary>
//        /// Saves the information passed in model object to database.
//        /// </summary>
//        /// <param name="ObjLocationsInfo">Model class object.</param>
//        /// <returns>No of records effected.</returns>
//        public int Add(ClsLocationsInfo ObjLocationsInfo)
//        {
//            string		strStoredProc	=	"Proc_InsertGeneralLocations";
//            DateTime	RecordDate		=	DateTime.Now;
//            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

//            try
//            {
//                objDataWrapper.AddParameter("@CUSTOMER_ID",ObjLocationsInfo.CUSTOMER_ID);
//                objDataWrapper.AddParameter("@APP_ID",ObjLocationsInfo.APP_ID);
//                objDataWrapper.AddParameter("@APP_VERSION_ID",ObjLocationsInfo.APP_VERSION_ID);
//                objDataWrapper.AddParameter("@LOC_NUM",ObjLocationsInfo.LOC_NUM);
			
//                objDataWrapper.AddParameter("@LOC_ADD1",ObjLocationsInfo.LOC_ADD1);
//                objDataWrapper.AddParameter("@LOC_ADD2",ObjLocationsInfo.LOC_ADD2);
//                objDataWrapper.AddParameter("@LOC_CITY",ObjLocationsInfo.LOC_CITY);
//                objDataWrapper.AddParameter("@LOC_COUNTY",ObjLocationsInfo.LOC_COUNTY);
//                objDataWrapper.AddParameter("@LOC_STATE",ObjLocationsInfo.LOC_STATE);
//                objDataWrapper.AddParameter("@LOC_ZIP",ObjLocationsInfo.LOC_ZIP);
//                //Added by mohit.
//                objDataWrapper.AddParameter("@LOC_TERRITORY",ObjLocationsInfo.LOC_TERRITORY);

//                objDataWrapper.AddParameter("@IS_ACTIVE",ObjLocationsInfo.IS_ACTIVE);
//                objDataWrapper.AddParameter("@CREATED_BY",ObjLocationsInfo.CREATED_BY);
//                objDataWrapper.AddParameter("@CREATED_DATETIME",ObjLocationsInfo.CREATED_DATETIME);
//                objDataWrapper.AddParameter("@LOC_COUNTRY",ObjLocationsInfo.LOC_COUNTRY);
//            //	objDataWrapper.AddParameter("@MODIFIED_BY",ObjLocationsInfo.MODIFIED_BY);
//            //	objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",ObjLocationsInfo.LAST_UPDATED_DATETIME);
				
//                //SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@LOCATION_ID",ObjLocationsInfo.LOCATION_ID,SqlDbType.Int,ParameterDirection.Output);
														
//                int returnResult = 0;
//                if(TransactionLogRequired)
//                {
//                    ObjLocationsInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/Aspx/GeneralLiability/AddGeneralLocation.aspx.resx");
//                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
//                    string strTranXML = objBuilder.GetTransactionLogXML(ObjLocationsInfo);
//                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//                    objTransactionInfo.TRANS_TYPE_ID	=	1;
//                    objTransactionInfo.RECORDED_BY		=	ObjLocationsInfo.CREATED_BY;
//                    objTransactionInfo.CLIENT_ID		=   ObjLocationsInfo.CUSTOMER_ID; 
//                    objTransactionInfo.APP_ID			=	ObjLocationsInfo.APP_ID;
//                    objTransactionInfo.APP_VERSION_ID	=	ObjLocationsInfo.APP_VERSION_ID;
//                    objTransactionInfo.TRANS_DESC		=	"New Location Has Been Added";
//                    //Nov 9,2005:Sumit Chhabra:Following information is being added to transaction log to display the LOB 
//                    //							worked upon as well as Screen Name
//                    objTransactionInfo.CUSTOM_INFO		=	";Screen Name = " +  "Location Details";
//                    objTransactionInfo.CHANGE_XML		=	strTranXML;
//                    //Executing the query
//                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
//                }
//                else
//                {
//                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
//                }
//                objDataWrapper.ClearParameteres();
//                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//                return returnResult;
////				int LOCATION_ID = -1;
////
////				if( returnResult > 0)
////				{
////					LOCATION_ID = int.Parse(objSqlParameter.Value.ToString());
////				}
////				
////				objDataWrapper.ClearParameteres();
////				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
////
////				if (LOCATION_ID == -1)
////				{
////					return -1;
////				}
////				else
////				{
////					ObjLocationsInfo.LOCATION_ID = LOCATION_ID;
////					return returnResult;
////				}
//            }
//            catch(Exception ex)
//            {
//                throw(ex);
//            }
//            finally
//            {
//                if(objDataWrapper != null) objDataWrapper.Dispose();
//            }
//        }
       #endregion

       //#region Update method
       // /// <summary>
       // /// Update method that recieves Model object to save.
       // /// </summary>
       // /// <param name="objOldLocationsInfo">Model object having old information</param>
       // /// <param name="ObjLocationsInfo">Model object having new information(form control's value)</param>
       // /// <returns>No. of rows updated (1 or 0)</returns>
       // public int Update(ClsLocationsInfo objOldLocationsInfo,ClsLocationsInfo ObjLocationsInfo)
       // {
       //     string		strStoredProc	=	"Proc_UpdateGeneralLocations";
       //     string strTranXML;
       //     int returnResult = 0;
       //     SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
       //     DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
       //     try 
       //     {
       //         objDataWrapper.AddParameter("@CUSTOMER_ID",ObjLocationsInfo.CUSTOMER_ID);
       //         objDataWrapper.AddParameter("@APP_ID",ObjLocationsInfo.APP_ID);
       //         objDataWrapper.AddParameter("@APP_VERSION_ID",ObjLocationsInfo.APP_VERSION_ID);
       //         //objDataWrapper.AddParameter("@LOCATION_ID",ObjLocationsInfo.LOCATION_ID);
       //         objDataWrapper.AddParameter("@LOC_NUM",ObjLocationsInfo.LOC_NUM);
       //         objDataWrapper.AddParameter("@LOC_ADD1",ObjLocationsInfo.LOC_ADD1);
       //         objDataWrapper.AddParameter("@LOC_ADD2",ObjLocationsInfo.LOC_ADD2);
       //         objDataWrapper.AddParameter("@LOC_CITY",ObjLocationsInfo.LOC_CITY);
       //         objDataWrapper.AddParameter("@LOC_COUNTY",ObjLocationsInfo.LOC_COUNTY);
       //         objDataWrapper.AddParameter("@LOC_STATE",ObjLocationsInfo.LOC_STATE);
       //         objDataWrapper.AddParameter("@LOC_ZIP",ObjLocationsInfo.LOC_ZIP);
       //         //Added by mohit.
       //         objDataWrapper.AddParameter("@LOC_TERRITORY",ObjLocationsInfo.LOC_TERRITORY);
				
       //         objDataWrapper.AddParameter("@IS_ACTIVE",ObjLocationsInfo.IS_ACTIVE);
       //     //	objDataWrapper.AddParameter("@CREATED_BY",ObjLocationsInfo.CREATED_BY);
       //     //	objDataWrapper.AddParameter("@CREATED_DATETIME",ObjLocationsInfo.CREATED_DATETIME);
       //         objDataWrapper.AddParameter("@MODIFIED_BY",ObjLocationsInfo.MODIFIED_BY);
       //         objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",ObjLocationsInfo.LAST_UPDATED_DATETIME);
       //         objDataWrapper.AddParameter("@LOC_COUNTRY",ObjLocationsInfo.LOC_COUNTRY);
				
       //         if(base.TransactionLogRequired) 
       //         {
       //             ObjLocationsInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/Aspx/GeneralLiability/AddGeneralLocation.aspx.resx");
       //             objBuilder.GetUpdateSQL(objOldLocationsInfo,ObjLocationsInfo,out strTranXML);
       //             Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
       //             if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML=="")
       //                 returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
       //             else
       //             {
       //                 objTransactionInfo.TRANS_TYPE_ID	=	3;
       //                 objTransactionInfo.RECORDED_BY		=	ObjLocationsInfo.MODIFIED_BY;
       //                 objTransactionInfo.CLIENT_ID		=   ObjLocationsInfo.CUSTOMER_ID; 
       //                 objTransactionInfo.TRANS_DESC		=	"Location Has Been Updated";
       //                 objTransactionInfo.APP_ID			=	ObjLocationsInfo.APP_ID;
       //                 objTransactionInfo.APP_VERSION_ID	=	ObjLocationsInfo.APP_VERSION_ID;
       //                 //Nov 9,2005:Sumit Chhabra:Following information is being added to transaction log to display the LOB 
       //                 //							worked upon as well as Screen Name
       //                 //objTransactionInfo.CUSTOM_INFO		=	";LOB = " + "General Liability" + ";Screen Name = " +  "Location Details";
       //                 objTransactionInfo.CUSTOM_INFO		=	";Screen Name = " +  "Location Details";
       //                 objTransactionInfo.CHANGE_XML		=	strTranXML;
       //                 returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
       //             }

       //         }
       //         else
       //         {
       //             returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
       //         }
       //         objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
       //         return returnResult;
       //     }
       //     catch(Exception ex)
       //     {
       //         throw(ex);
       //     }
       //     finally
       //     {
       //         if(objDataWrapper != null) 
       //         {
       //             objDataWrapper.Dispose();
       //         }
       //         if(objBuilder != null) 
       //         {
       //             objBuilder = null;
       //         }
       //     }
       // }
       // #endregion

		#region "GetxmlMethods"
		public static string GetXmlForPageControls(string CUSTOMER_ID)
		{
			string strSql = "Proc_GetXMLAPP_LOCATIONS";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.GetXml();
		}
		#endregion

		#region GetGeneralLocationsXml
		public static string GetGeneralLocationsXml(int intCustoemrId, int intAppId, int intAppVersionId)
		{
			string strStoredProc = "Proc_GetGeneralLocationsXml";
			DataSet dsGeneralLocations= new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustoemrId);
				objDataWrapper.AddParameter("@APP_ID",intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId);
				//objDataWrapper.AddParameter("@Location_Id",Location_Id);
				
				dsGeneralLocations= objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsGeneralLocations.Tables[0].Rows.Count != 0)
				{
					return dsGeneralLocations.GetXml();
				}
				else
				{
					return "";
				}
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}
		#endregion

//		#region GetNewLocationNumber
//		public static int GetNewLocationNumber(string strCustomerID, string strAppID, string strAppVersionID, string strCalledFrom)
//		{
//			string strStoredProc = "Proc_GetNewLocationNumber";
//			DataSet dsGeneralLocations= new DataSet();
//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
//			int NewLocationNum=0,returnResult=0;
//			try
//			{
//				objDataWrapper.AddParameter("@CUSTOMER_ID", strCustomerID);
//				objDataWrapper.AddParameter("@APP_ID", strAppID);
//				objDataWrapper.AddParameter("@APP_VERSION_ID",strAppVersionID);				
//				objDataWrapper.AddParameter("@CALLEDFROM",strCalledFrom);
//				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@CODE",NewLocationNum,SqlDbType.Int,ParameterDirection.Output);
//				returnResult= objDataWrapper.ExecuteNonQuery(strStoredProc);
//				if (returnResult!=0)				
//					NewLocationNum= int.Parse(objSqlParameter.Value.ToString());					
//				else
//					NewLocationNum=1;
//				return NewLocationNum;
//			}
//			catch(Exception exc)
//			{
//				throw (exc);
//			}
//			finally
//			{
//				if(objDataWrapper!=null) objDataWrapper.Dispose();
//			}
//		}
//		#endregion


		#region Policy Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="ObjLocationsInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int AddPolicyGenLocations(Cms.Model.Policy.GeneralLiability.ClsLocationsInfo ObjLocationsInfo)
		{
			string		strStoredProc	=	"Proc_InsertPolicyGeneralLocations";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjLocationsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",ObjLocationsInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",ObjLocationsInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@LOC_NUM",ObjLocationsInfo.LOC_NUM);
			
				objDataWrapper.AddParameter("@LOC_ADD1",ObjLocationsInfo.LOC_ADD1);
				objDataWrapper.AddParameter("@LOC_ADD2",ObjLocationsInfo.LOC_ADD2);
				objDataWrapper.AddParameter("@LOC_CITY",ObjLocationsInfo.LOC_CITY);
				objDataWrapper.AddParameter("@LOC_COUNTY",ObjLocationsInfo.LOC_COUNTY);
				objDataWrapper.AddParameter("@LOC_STATE",ObjLocationsInfo.LOC_STATE);
				objDataWrapper.AddParameter("@LOC_ZIP",ObjLocationsInfo.LOC_ZIP);
				//Added by mohit.
				objDataWrapper.AddParameter("@LOC_TERRITORY",ObjLocationsInfo.LOC_TERRITORY);

				objDataWrapper.AddParameter("@IS_ACTIVE",ObjLocationsInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",ObjLocationsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",ObjLocationsInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@LOC_COUNTRY",ObjLocationsInfo.LOC_COUNTRY);
				//	objDataWrapper.AddParameter("@MODIFIED_BY",ObjLocationsInfo.MODIFIED_BY);
				//	objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",ObjLocationsInfo.LAST_UPDATED_DATETIME);
				
				//SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@LOCATION_ID",ObjLocationsInfo.LOCATION_ID,SqlDbType.Int,ParameterDirection.Output);
														
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					ObjLocationsInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/Aspx/GeneralLiability/AddGeneralLocation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(ObjLocationsInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	ObjLocationsInfo.CREATED_BY;
					objTransactionInfo.CLIENT_ID		=   ObjLocationsInfo.CUSTOMER_ID; 
					objTransactionInfo.POLICY_ID			=	ObjLocationsInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	ObjLocationsInfo.POLICY_VERSION_ID;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1727", "");// "New Location Has Been Added";					
					objTransactionInfo.CUSTOM_INFO		=	";Screen Name = " +  "Location Details";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

//				int LOCATION_ID = -1;
//
//				if( returnResult > 0)
//				{
//					LOCATION_ID = int.Parse(objSqlParameter.Value.ToString());
//				}
				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

//				if (LOCATION_ID == -1)
//				{
//					return -1;
//				}
//				else
//				{
//					ObjLocationsInfo.LOCATION_ID = LOCATION_ID;
//					return returnResult;
//				}
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

		#region Policy Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldLocationsInfo">Model object having old information</param>
		/// <param name="ObjLocationsInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int UpdatePolicyGenLocation(Cms.Model.Policy.GeneralLiability.ClsLocationsInfo objOldLocationsInfo,Cms.Model.Policy.GeneralLiability.ClsLocationsInfo ObjLocationsInfo)
		{
			string		strStoredProc	=	"Proc_UpdatePolicyGeneralLocations";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjLocationsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",ObjLocationsInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",ObjLocationsInfo.POLICY_VERSION_ID);
				//objDataWrapper.AddParameter("@LOCATION_ID",ObjLocationsInfo.LOCATION_ID);
				objDataWrapper.AddParameter("@LOC_NUM",ObjLocationsInfo.LOC_NUM);
				objDataWrapper.AddParameter("@LOC_ADD1",ObjLocationsInfo.LOC_ADD1);
				objDataWrapper.AddParameter("@LOC_ADD2",ObjLocationsInfo.LOC_ADD2);
				objDataWrapper.AddParameter("@LOC_CITY",ObjLocationsInfo.LOC_CITY);
				objDataWrapper.AddParameter("@LOC_COUNTY",ObjLocationsInfo.LOC_COUNTY);
				objDataWrapper.AddParameter("@LOC_STATE",ObjLocationsInfo.LOC_STATE);
				objDataWrapper.AddParameter("@LOC_ZIP",ObjLocationsInfo.LOC_ZIP);
				//Added by mohit.
				objDataWrapper.AddParameter("@LOC_TERRITORY",ObjLocationsInfo.LOC_TERRITORY);
				
				objDataWrapper.AddParameter("@IS_ACTIVE",ObjLocationsInfo.IS_ACTIVE);
				//	objDataWrapper.AddParameter("@CREATED_BY",ObjLocationsInfo.CREATED_BY);
				//	objDataWrapper.AddParameter("@CREATED_DATETIME",ObjLocationsInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",ObjLocationsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",ObjLocationsInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@LOC_COUNTRY",ObjLocationsInfo.LOC_COUNTRY);
				
				if(base.TransactionLogRequired) 
				{
					ObjLocationsInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/Aspx/GeneralLiability/AddGeneralLocation.aspx.resx");
					objBuilder.GetUpdateSQL(objOldLocationsInfo,ObjLocationsInfo,out strTranXML);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML=="")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	ObjLocationsInfo.MODIFIED_BY;
						objTransactionInfo.CLIENT_ID		=   ObjLocationsInfo.CUSTOMER_ID;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1728", "");// "Location Has Been Updated";
						objTransactionInfo.POLICY_ID			=	ObjLocationsInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID	=	ObjLocationsInfo.POLICY_VERSION_ID;						
						objTransactionInfo.CUSTOM_INFO		=	";Screen Name = " +  "Location Details";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
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
		#endregion

		#region GetPolicyGeneralLocationsXml
		public static string GetPolicyGeneralLocationsXml(int intCustoemrId, int intPolicyId, int intPolicyVersionId)//,int Location_Id)
		{
			string strStoredProc = "Proc_GetPolicyGeneralLocationsXml";
			DataSet dsGeneralLocations= new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustoemrId);
				objDataWrapper.AddParameter("@POLICY_ID",intPolicyId);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",intPolicyVersionId);
				//objDataWrapper.AddParameter("@Location_Id",Location_Id);
				
				dsGeneralLocations= objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsGeneralLocations.Tables[0].Rows.Count != 0)
				{
					return dsGeneralLocations.GetXml();
				}
				else
				{
					return "";
				}
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}
		#endregion

		#region GetPolicyNewLocationNumber
		public static int GetPolicyNewLocationNumber(string strCustomerID, string strPolicyID, string strPolicyVersionID)
		{
			string strStoredProc = "Proc_GetPolicyGenLocationNumber";
			DataSet dsGeneralLocations= new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			int NewLocationNum=0,returnResult=0;
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID", strCustomerID);
				objDataWrapper.AddParameter("@POLICY_ID", strPolicyID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",strPolicyVersionID);								
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@CODE",NewLocationNum,SqlDbType.Int,ParameterDirection.Output);
				returnResult= objDataWrapper.ExecuteNonQuery(strStoredProc);
				if (returnResult!=0)				
					NewLocationNum= int.Parse(objSqlParameter.Value.ToString());					
				else
					NewLocationNum=1;
				return NewLocationNum;
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				if(objDataWrapper!=null) objDataWrapper.Dispose();
			}
		}
		#endregion


	}
}
