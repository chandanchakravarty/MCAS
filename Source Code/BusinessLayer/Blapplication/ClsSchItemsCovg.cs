/******************************************************************************************
<Author				: - Vijay Joshi
<Start Date			: -	5/18/2005 5:29:50 PM
<End Date			: -	
<Description		: - Busines logic class for Add schedule Coverage screen.
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date			: - Anshuman
<Modified By			: - June 07, 2005
<Purpose				: - transaction description modified
*******************************************************************************************/ 
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using Cms.Model.Application.HomeOwners;
using Cms.Model.Application;

using Cms.Model.Policy;
using Cms.Model.Policy.Homeowners;


namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Business layer clas for schedule items coverage.
	/// </summary>
	public class ClsSchItemsCovg : ClsCoverages
	{
		private const string APP_HOME_OWNER_SCH_ITEMS_CVGS = "APP_HOME_OWNER_SCH_ITEMS_CVGS";
		private const string GET_LOCATION_INFO_PROC = "Proc_GetAPP_HOME_OWNER_SCH_ITEMS_CVGS_XML";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		private const string ACTIVATE_DEACTIVATE_PROC	= "";
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
		public ClsSchItemsCovg()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objSchItemsCovgInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsSchItemsCovgInfo objSchItemsCovgInfo)
		{
			string		strStoredProc	=	"Proc_InsertAPP_HOME_OWNER_SCH_ITEMS_CVGS";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objSchItemsCovgInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objSchItemsCovgInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objSchItemsCovgInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@CATEGORY",objSchItemsCovgInfo.CATEGORY);
				objDataWrapper.AddParameter("@DETAILED_DESC",objSchItemsCovgInfo.DETAILED_DESC);
				objDataWrapper.AddParameter("@SN_DETAILS",objSchItemsCovgInfo.SN_DETAILS);
				objDataWrapper.AddParameter("@AMOUNT_OF_INSURANCE",objSchItemsCovgInfo.AMOUNT_OF_INSURANCE);
				objDataWrapper.AddParameter("@APPRAISAL_DESC",objSchItemsCovgInfo.APPRAISAL_DESC);
				
				objDataWrapper.AddParameter("@BREAKAGE_DESC",objSchItemsCovgInfo.BREAKAGE_DESC);


				if(objSchItemsCovgInfo.RATE==0)
				{
					objDataWrapper.AddParameter("@RATE",null);
				}
				else
					objDataWrapper.AddParameter("@RATE",objSchItemsCovgInfo.RATE);

				if(objSchItemsCovgInfo.PREMIUM==0)
				{
					objDataWrapper.AddParameter("@PREMIUM",null);
				}
				else
					objDataWrapper.AddParameter("@PREMIUM",objSchItemsCovgInfo.PREMIUM);

				objDataWrapper.AddParameter("@APPRAISAL",objSchItemsCovgInfo.APPRAISAL);

				if (objSchItemsCovgInfo.PURCHASE_APPRAISAL_DATE.Ticks > 0 )
					objDataWrapper.AddParameter("@PURCHASE_APPRAISAL_DATE", objSchItemsCovgInfo.PURCHASE_APPRAISAL_DATE);
				else
					objDataWrapper.AddParameter("@PURCHASE_APPRAISAL_DATE", null);

				objDataWrapper.AddParameter("@BREAKAGE_COVERAGE",objSchItemsCovgInfo.BREAKAGE_COVERAGE);
				objDataWrapper.AddParameter("@CREATED_BY",objSchItemsCovgInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objSchItemsCovgInfo.CREATED_DATETIME);
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ITEM_ID",objSchItemsCovgInfo.ITEM_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objSchItemsCovgInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/homeowners/addschitemscovg.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objSchItemsCovgInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objSchItemsCovgInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objSchItemsCovgInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objSchItemsCovgInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objSchItemsCovgInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New scheduled item/coverages is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				int ITEM_ID = int.Parse(objSqlParameter.Value.ToString());
				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
				if (ITEM_ID == -1)
				{
					return -1;
				}
				else
				{
					objSchItemsCovgInfo.ITEM_ID = ITEM_ID;
					return returnResult;
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
		#endregion

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldSchItemsCovgInfo">Model object having old information</param>
		/// <param name="objSchItemsCovgInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsSchItemsCovgInfo objOldSchItemsCovgInfo,ClsSchItemsCovgInfo objSchItemsCovgInfo)
		{
			string		strStoredProc	=	"Proc_UpdateAPP_HOME_OWNER_SCH_ITEMS_CVGS";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@ITEM_ID",objSchItemsCovgInfo.ITEM_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objSchItemsCovgInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objSchItemsCovgInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objSchItemsCovgInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@CATEGORY",objSchItemsCovgInfo.CATEGORY);
				objDataWrapper.AddParameter("@DETAILED_DESC",objSchItemsCovgInfo.DETAILED_DESC);
				objDataWrapper.AddParameter("@SN_DETAILS",objSchItemsCovgInfo.SN_DETAILS);
				objDataWrapper.AddParameter("@AMOUNT_OF_INSURANCE",objSchItemsCovgInfo.AMOUNT_OF_INSURANCE);
				objDataWrapper.AddParameter("@APPRAISAL_DESC",objSchItemsCovgInfo.APPRAISAL_DESC);
				objDataWrapper.AddParameter("@BREAKAGE_DESC",objSchItemsCovgInfo.BREAKAGE_DESC);
				
				if(objSchItemsCovgInfo.RATE==0)
				{
					objDataWrapper.AddParameter("@RATE",null);
				}

				else
					objDataWrapper.AddParameter("@RATE",objSchItemsCovgInfo.RATE);


				if(objSchItemsCovgInfo.PREMIUM==0)
				{
					objDataWrapper.AddParameter("@PREMIUM",null);
				}

				else
					objDataWrapper.AddParameter("@PREMIUM",objSchItemsCovgInfo.PREMIUM);


				objDataWrapper.AddParameter("@APPRAISAL",objSchItemsCovgInfo.APPRAISAL);
				
				if (objSchItemsCovgInfo.PURCHASE_APPRAISAL_DATE.Ticks > 0 )
					objDataWrapper.AddParameter("@PURCHASE_APPRAISAL_DATE", objSchItemsCovgInfo.PURCHASE_APPRAISAL_DATE);
				else
					objDataWrapper.AddParameter("@PURCHASE_APPRAISAL_DATE", null);

				objDataWrapper.AddParameter("@BREAKAGE_COVERAGE",objSchItemsCovgInfo.BREAKAGE_COVERAGE);
				objDataWrapper.AddParameter("@MODIFIED_BY",objSchItemsCovgInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objSchItemsCovgInfo.LAST_UPDATED_DATETIME);
				
				if(TransactionLogRequired) 
				{
					objSchItemsCovgInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/homeowners/addschitemscovg.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldSchItemsCovgInfo,objSchItemsCovgInfo);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.APP_ID = objSchItemsCovgInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objSchItemsCovgInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objSchItemsCovgInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objSchItemsCovgInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Scheduled item/coverages is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

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

		#region GetLocationInfo

		public static string GetSchItemCovgInfo(int intCustomerId,int intAppid, int intAppVersionId, int intItemId )
		{

			DataSet dsLocationInfo = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
				objDataWrapper.AddParameter("@APP_ID",intAppid);
				objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId);
				objDataWrapper.AddParameter("@ITEM_ID",intItemId);

				dsLocationInfo = objDataWrapper.ExecuteDataSet(GET_LOCATION_INFO_PROC);
				
				if (dsLocationInfo.Tables[0].Rows.Count != 0)
				{
					return dsLocationInfo.GetXml();
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
		

		/// <summary>
		/// Saves Inland marine coverages in the database
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <param name="strOldXML"></param>
		/// <param name="CreatedBy"></param>
		/// <returns></returns>
		public int SaveInlandMarine(ArrayList alNewCoverages,string strOldXML,string CreatedBy)
		{
			
			string	strStoredProc =	"Proc_InsertAPP_HOME_OWNER_SCH_ITEMS_CVGS";

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			SqlCommand cmdCoverage = new SqlCommand();
			cmdCoverage.CommandText = strStoredProc;
			cmdCoverage.CommandType = CommandType.StoredProcedure;
			
			SqlParameter[] param = new SqlParameter[16];
			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			

			if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}
			
		
			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Application.ClsCoveragesInfo objNew = (ClsCoveragesInfo)alNewCoverages[i];
					
					objDataWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objDataWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objDataWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
					objDataWrapper.AddParameter("@CATEGORY",objNew.COVERAGE_CODE_ID);
					objDataWrapper.AddParameter("@ITEM_ID",objNew.COVERAGE_ID);
					objDataWrapper.AddParameter("@DETAILED_DESC",objNew.DETAILED_DESC);
					objDataWrapper.AddParameter("@SN_DETAILS",null);

					objDataWrapper.AddParameter("@AMOUNT_OF_INSURANCE",Default.GetDoubleNull(objNew.LIMIT_1));
					objDataWrapper.AddParameter("@APPRAISAL_DESC",null);
				
					objDataWrapper.AddParameter("@BREAKAGE_DESC",null);
					objDataWrapper.AddParameter("@RATE",null);
					
					objDataWrapper.AddParameter("@PREMIUM",null);
					
					

					objDataWrapper.AddParameter("@APPRAISAL",null);

					objDataWrapper.AddParameter("@PURCHASE_APPRAISAL_DATE", null);

					objDataWrapper.AddParameter("@BREAKAGE_COVERAGE",null);
					objDataWrapper.AddParameter("@DEDUCTIBLE",objNew.DEDUCTIBLE_1);

					objDataWrapper.AddParameter("@CREATED_BY",CreatedBy);
					objDataWrapper.AddParameter("@CREATED_DATETIME",System.DateTime.Now);
				
					//SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ITEM_ID",objSchItemsCovgInfo.ITEM_ID,SqlDbType.Int,ParameterDirection.Output);

				
					string strTranXML = "";
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					if ( objNew.COVERAGE_ID == -1 )
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/homeowners/AddInlandMarine.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Inland marine added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

					}
					else
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/homeowners/AddInlandMarine.aspx.resx");
				
						strTranXML = this.GetTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);
					}
				
					if ( strTranXML.Trim() == "" )
					{
						//SqlHelper.ExecuteNonQuery(tran,"Proc_SAVE_VEHICLE_COVERAGES",param);
						//SqlHelper.ExecuteNonQuery(tran,"Proc_SAVE_VEHICLE_COVERAGES",param);
				
					}
					else
					{
						
					
						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Inland marine added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						
						//SqlHelper.ExecuteNonQuery(tran,CommandType.StoredProcedure,"Proc_SAVE_VEHICLE_COVERAGES",param);
						//int retVal = cmdCoverage.ExecuteNonQuery();
						//ClsCommon.AddTransactionLog(objTransactionInfo,tran);
						
						
					}
					
					objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					objDataWrapper.ClearParameteres();

				}
			}
			catch(Exception ex)
			{
				//tran.Rollback();
				//conn.Close();
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				if ( ex.InnerException != null)
				{
					string message = ex.InnerException.Message.ToLower();
				

					if ( message.StartsWith("violation of primary key"))
					{
						return -2;
					}

				}

				throw(ex);
			} 
			
			//tran.Commit();
			//conn.Close();
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;
		}


		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objSchItemsCovgInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		
		public int Save(ClsSchItemsCovgInfo objSchItemsCovgInfo, DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_SaveHOME_OWNER_SCH_ITEMS_CVGS_ACORD";
			DateTime	RecordDate		=	DateTime.Now;
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			
			objDataWrapper.AddParameter("@CUSTOMER_ID",objSchItemsCovgInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",objSchItemsCovgInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objSchItemsCovgInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@CATEGORY",objSchItemsCovgInfo.CATEGORY);
			objDataWrapper.AddParameter("@CATEGORY_CODE",objSchItemsCovgInfo.CATEGORY_CODE);
			objDataWrapper.AddParameter("@DETAILED_DESC",objSchItemsCovgInfo.DETAILED_DESC);
			objDataWrapper.AddParameter("@SN_DETAILS",objSchItemsCovgInfo.SN_DETAILS);
			objDataWrapper.AddParameter("@AMOUNT_OF_INSURANCE",objSchItemsCovgInfo.AMOUNT_OF_INSURANCE);
			objDataWrapper.AddParameter("@DEDUCTIBLE",objSchItemsCovgInfo.DEDUCTIBLE);
			objDataWrapper.AddParameter("@APPRAISAL_DESC",objSchItemsCovgInfo.APPRAISAL_DESC);
				
			objDataWrapper.AddParameter("@BREAKAGE_DESC",objSchItemsCovgInfo.BREAKAGE_DESC);


			if(objSchItemsCovgInfo.RATE==0)
			{
				objDataWrapper.AddParameter("@RATE",null);
			}
			else
				objDataWrapper.AddParameter("@RATE",objSchItemsCovgInfo.RATE);

			if(objSchItemsCovgInfo.PREMIUM==0)
			{
				objDataWrapper.AddParameter("@PREMIUM",null);
			}
			else
				objDataWrapper.AddParameter("@PREMIUM",objSchItemsCovgInfo.PREMIUM);

			objDataWrapper.AddParameter("@APPRAISAL",objSchItemsCovgInfo.APPRAISAL);

			if (objSchItemsCovgInfo.PURCHASE_APPRAISAL_DATE != DateTime.MinValue )
				objDataWrapper.AddParameter("@PURCHASE_APPRAISAL_DATE", objSchItemsCovgInfo.PURCHASE_APPRAISAL_DATE);
			else
				objDataWrapper.AddParameter("@PURCHASE_APPRAISAL_DATE", null);

			objDataWrapper.AddParameter("@BREAKAGE_COVERAGE",objSchItemsCovgInfo.BREAKAGE_COVERAGE);
			objDataWrapper.AddParameter("@CREATED_BY",objSchItemsCovgInfo.CREATED_BY);
			objDataWrapper.AddParameter("@CREATED_DATETIME",RecordDate);
				
			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ITEM_ID",objSchItemsCovgInfo.ITEM_ID,SqlDbType.Int,ParameterDirection.Output);

			int returnResult = 0;

			/*
				if(TransactionLogRequired)
				{
					objSchItemsCovgInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/homeowners/addschitemscovg.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objSchItemsCovgInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objSchItemsCovgInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objSchItemsCovgInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objSchItemsCovgInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objSchItemsCovgInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New scheduled item/coverages is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{*/
			returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
			//}

			int ITEM_ID = int.Parse(objSqlParameter.Value.ToString());
				
			//objDataWrapper.ClearParameteres();
			//objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
			if (ITEM_ID == -1)
			{
				return -1;
			}
			else
			{
				objSchItemsCovgInfo.ITEM_ID = ITEM_ID;
				return returnResult;
			}
			
		}

		public  DataSet GetInLandCoverages(int customerID, int appID, 
			int appVersionID, int dwellingID, string appType,string COVERAGETYPE)
		{
			string	strStoredProc =	"Proc_GetAPP_HOME_OWNER_SCH_ITEMS_CVGS";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@DWELLING_ID",dwellingID);
			objWrapper.AddParameter("@APP_TYPE",appType);
			objWrapper.AddParameter("@COVERAGE_TYPE",COVERAGETYPE);
			//objWrapper.AddParameter("@CURRENT_PAGE_INDEX",currentPageIndex);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		
		}


		//Added by Mohit Agarwal 7-May-2007 to add Amount field in Coverages grid
		public string GetInLandCoveragesAmount(int customerID, int appID, int appVersionID, string CalledFrom, string strCOV_ID)
		{
			string	strStoredProc =	"PROC_GETPDF_HOME_SCHEDULE_ARTICLES_INFO";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMERID",customerID);
			objWrapper.AddParameter("@POLID",appID);
			objWrapper.AddParameter("@VERSIONID",appVersionID);
			objWrapper.AddParameter("@CALLEDFROM",CalledFrom);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				foreach(DataRow schrow in ds.Tables[0].Rows)
				{
					if(schrow["ITEM_ID"].ToString() == strCOV_ID)
						return schrow["AMOUNT"].ToString();
				}
			}

			return "0";//Changed from 0.00 by Charles on 6-Oct-09 for Itrack 6488
		
		}

		public  DataSet GetInLandCoveragesFilled(int customerID, int appID, int appVersionID)
		{
			string	strStoredProc =	"PROC_GETAPP_IM_COVG";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		
		}

		public  DataSet GetInLandCoveragesFilledPolicy(int customerID, int PolID, int PolVersionID)
		{
			string	strStoredProc =	"PROC_GETPOL_IM_COVG";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POL_ID",PolID);
			objWrapper.AddParameter("@POL_VERSION_ID",PolVersionID);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>dataset after filteration of records (coverages) that are no longer required </returns>
		public DataSet GetRentInlandCoverages(int customerID, int appID, int appVersionID, int dwellingID, string appType,string COVERAGETYPE)
		{
			//fetching dataset with all coverages
			DataSet dsCoverages=null;
			dsCoverages = this.GetInLandCoverages(customerID,
				appID,
				appVersionID,
				dwellingID,appType,COVERAGETYPE
				);	

			
			return dsCoverages;             
		}


		/// <summary>
		/// Gets inland marine for Application
		/// </summary>
		/// <returns>dataset after filteration of records (coverages) that are no longer required </returns>
		public DataSet GetInlandCoverages(int customerID, int appID, int appVersionID, int dwellingID, string appType,string COVERAGETYPE)
		{
			//fetching dataset with all coverages
			DataSet dsCoverages=null;
			dsCoverages = this.GetInLandCoverages(customerID,
				appID,
				appVersionID,
				dwellingID,appType,COVERAGETYPE
				);	

			

			return dsCoverages;             
		}
		
		/// <summary>
		/// Saves scheduled Items/Coverages for Home Policy
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <param name="strOldXML"></param>
		/// <param name="CreatedBy"></param>
		/// <returns></returns>
		public int SavePolicyInlandMarine(ArrayList alNewCoverages,string strOldXML,string CreatedBy)
		{
			
			string	strStoredProc =	"Proc_Save_POL_HOME_OWNER_SCH_ITEMS_CVGS";

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
		
			SqlParameter[] param = new SqlParameter[16];
			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			

			if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}
			
		
			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Application.HomeOwners.ClsSchItemsCovgInfo objNew = (Cms.Model.Application.HomeOwners.ClsSchItemsCovgInfo)alNewCoverages[i];
					
					objDataWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objDataWrapper.AddParameter("@POLICY_ID",objNew.APP_ID);
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",objNew.APP_VERSION_ID);
					objDataWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_CODE_ID);
					objDataWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
					objDataWrapper.AddParameter("@DETAILED_DESC",objNew.DETAILED_DESC);
					objDataWrapper.AddParameter("@LIMIT_1",objNew.AMOUNT_OF_INSURANCE);
					objDataWrapper.AddParameter("@DEDUCTIBLE_1",objNew.DEDUCTIBLE);
					objDataWrapper.AddParameter("@CREATED_BY",objNew.CREATED_BY);
					objDataWrapper.AddParameter("@CREATED_DATE_TIME",System.DateTime.Now);
				
					//SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ITEM_ID",objSchItemsCovgInfo.ITEM_ID,SqlDbType.Int,ParameterDirection.Output);

				
					string strTranXML = "";
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					if ( objNew.COVERAGE_ID == -1 )
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/homeowners/AddInlandMarine.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Inland marine added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

					}
					else
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/homeowner/PolSchItemsCoverages.aspx.resx");
				
						strTranXML = this.GetTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);
						objTransactionInfo.TRANS_DESC		=	"Inland marine updated.";
					}
				
					if ( strTranXML.Trim() == "" )
					{
						//SqlHelper.ExecuteNonQuery(tran,"Proc_SAVE_VEHICLE_COVERAGES",param);
						//SqlHelper.ExecuteNonQuery(tran,"Proc_SAVE_VEHICLE_COVERAGES",param);
				
					}
					else
					{
						
					
						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						
						//SqlHelper.ExecuteNonQuery(tran,CommandType.StoredProcedure,"Proc_SAVE_VEHICLE_COVERAGES",param);
						//int retVal = cmdCoverage.ExecuteNonQuery();
						//ClsCommon.AddTransactionLog(objTransactionInfo,tran);
						
						
					}
					
					objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					objDataWrapper.ClearParameteres();

				}
			}
			catch(Exception ex)
			{
				//tran.Rollback();
				//conn.Close();
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				if ( ex.InnerException != null)
				{
					string message = ex.InnerException.Message.ToLower();
				

					if ( message.StartsWith("violation of primary key"))
					{
						return -2;
					}

				}

				throw(ex);
			} 
			
			//tran.Commit();
			//conn.Close();
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;
		}

		
		public int DeletePolicyInlandMarineCoveragesSingle(string APP_ID, string APP_VER_ID, string CUST_ID, string COVG_ID)
		{				
			string	strStoredProc =	"Proc_DeleteAPP_HOME_OWNER_SCH_ITEMS_CVGS";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	

			try
			{
				objWrapper.AddParameter("@CUSTOMER_ID",CUST_ID);
				objWrapper.AddParameter("@APP_ID",APP_ID);
				objWrapper.AddParameter("@APP_VERSION_ID",APP_VER_ID);
				objWrapper.AddParameter("@ITEM_ID",COVG_ID);


				objWrapper.ExecuteNonQuery(strStoredProc);

				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return 1;
			}
			catch (Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
		}

		public int DeletePolicyInlandMarineCoverages(ArrayList alNewCoverages)
		{
			
			string	strStoredProc =	"Proc_Delete_POL_HOME_OWNER_SCH_ITEMS_CVGS";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			SqlParameter sCustomerID = (SqlParameter)objWrapper.AddParameter("@CUSTOMER_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sPolID = (SqlParameter)objWrapper.AddParameter("@POLICY_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sPolVersionID = (SqlParameter)objWrapper.AddParameter("@POLICY_VERSION_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sCoverageID = (SqlParameter)objWrapper.AddParameter("@COVERAGE_ID",SqlDbType.Int,ParameterDirection.Input);

			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					sPolID.Value = ((ClsSchItemsCovgInfo)alNewCoverages[i]).APP_ID;
					sPolVersionID.Value = ((ClsSchItemsCovgInfo)alNewCoverages[i]).APP_VERSION_ID;
					sCustomerID.Value = ((ClsSchItemsCovgInfo)alNewCoverages[i]).CUSTOMER_ID;
					sCoverageID.Value = ((ClsSchItemsCovgInfo)alNewCoverages[i]).COVERAGE_ID;

					objWrapper.ExecuteNonQuery(strStoredProc);				
				}
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;

		}

		/// <summary>
		/// Gets the Scheduled Item Coverages for Home LOB Policy
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="policyID"></param>
		/// <param name="policyVersionID"></param>
		/// <param name="polType"></param>
		/// <returns></returns>
		public  DataSet GetPolInlandCoverages(int customerID, int policyID, 
			int policyVersionID, string polType)
		{
			string	strStoredProc =	"Proc_GetPOL_HOME_OWNER_SCH_ITEMS_CVGS";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",policyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);	
			objWrapper.AddParameter("@POL_TYPE",polType);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		
		}

		/// <summary>
		/// Gets inland marine coverages for Policy
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="policyID"></param>
		/// <param name="policyVersionID"></param>
		/// <param name="polType"></param>
		/// <returns></returns>
		public DataSet GetPolicyInlandCoverages(int customerID, int policyID, 
			int policyVersionID, 
			string polType)
		{
			//fetching dataset with all coverages
			DataSet dsCoverages=null;
			dsCoverages = this.GetPolInlandCoverages(customerID,
				policyID,
				policyVersionID,
				polType
				);	
			
			/*
			//fetching XML string with all coverages to remove
			ClsHomeGeneralInformation objHome=new  ClsHomeGeneralInformation();
			string covXML=objHome.GetHomeCoveragesToRemove(customerID,
				appID,
				appVersionID,
				dwellingID,dsCoverages
				);	
			*/
			/*Reading from the XML file and saving the text in string until the function start giving the XML string */
			
			/*TextReader tr=new StreamReader(System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath  + "/working/coveragedummyxml.xml"));
			string covXML=tr.ReadToEnd(); 
			tr.Close();*/
			
			/*
			//if XML string is not blank		
			if(covXML!="" )
			{
				Cms.BusinessLayer.BlApplication.clsapplication 	objCovInformation = new Cms.BusinessLayer.BlApplication.clsapplication();
				//function call to delete coverage
				dsCoverages=this.DeleteCoverage(dsCoverages,covXML);			

				//function call to delete coverage limits
				dsCoverages=this.DeleteCoverageOptions(dsCoverages,covXML);			

				//function call to update mandatory field
				dsCoverages=this.UpdateCoverageMandatory(dsCoverages,covXML);			

				//function call to update default field
				dsCoverages=this.OverwriteCoverageDefaultValue(dsCoverages,covXML);			
			}*/

			return dsCoverages;             
		}
		
		
		/// <summary>
		/// Deletes inland marine coverages from the database
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <returns></returns>
		public int DeleteInlandMarineCoverages(ArrayList alNewCoverages)
		{
			
			string	strStoredProc =	"Proc_DeleteAPP_HOME_OWNER_SCH_ITEMS_CVGS";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			SqlParameter sCustomerID = (SqlParameter)objWrapper.AddParameter("@CUSTOMER_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppID = (SqlParameter)objWrapper.AddParameter("@APP_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppVersionID = (SqlParameter)objWrapper.AddParameter("@APP_VERSION_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sCoverageID = (SqlParameter)objWrapper.AddParameter("@ITEM_ID",SqlDbType.Int,ParameterDirection.Input);

			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					sAppID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).APP_ID;
					sAppVersionID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).APP_VERSION_ID;
					sCustomerID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).CUSTOMER_ID;
					sCoverageID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).COVERAGE_ID;

					objWrapper.ExecuteNonQuery(strStoredProc);				
				}
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;

		}


		public int SaveInlandMarineCovgNewPolicy(ArrayList objArlist)
		{
			//DELETE Old Data
			
			string	strDelStoredProc =	"PROC_DELETE_POL_IM_SCH_ITEMS_CVGS_ALL";
			string  strInStoredProc =	"PROC_INSERTPOL_IM_SCH_ITEMS_CVGS";
			int customerID=0;
			int polID=0;
			int polVersionID=0;
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	

			try
			{
				for(int i = 0; i < objArlist.Count; i++ )
				{
					Cms.Model.Policy.HomeOwners.ClsSchItemsCovgInfo   objSchItem = (Cms.Model.Policy.HomeOwners.ClsSchItemsCovgInfo)objArlist[i];
					customerID = objSchItem.CUSTOMER_ID;
					polID = objSchItem.POLICY_ID;
					polVersionID = objSchItem.POLICY_VERSION_ID;
					objWrapper.AddParameter("@CUSTOMER_ID",objSchItem.CUSTOMER_ID);
					objWrapper.AddParameter("@POL_ID",objSchItem.POLICY_ID);
					objWrapper.AddParameter("@POL_VERSION_ID",objSchItem.POLICY_VERSION_ID);
					objWrapper.AddParameter("@ITEM_ID",objSchItem.COVERAGE_CODE_ID);
					if(objSchItem.Action == "I")
					{
						objWrapper.AddParameter("@CREATED_BY",objSchItem.CREATED_BY);
						objWrapper.AddParameter("@CREATED_DATETIME",objSchItem.CREATED_DATETIME);
						objWrapper.AddParameter("@DEDUCTIBLE",objSchItem.DEDUCTIBLE);
						objWrapper.ExecuteNonQuery(strInStoredProc);

					}
					else if(objSchItem.Action=="D")
					{

						objWrapper.ExecuteNonQuery(strDelStoredProc);

					}
					objWrapper.ClearParameteres();
						
				}


				//Update Coverage On Marine Dependency
				ClsHomeCoverages objCoverage=new ClsHomeCoverages();
				objCoverage.UpdateCoveragesByRulePolicy(objWrapper,customerID,polID ,
					polVersionID,RuleType.OtherAppDependent);
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return 1;
			}
				
			

			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}


		}

		public int SaveInlandMarineCovgNew(ArrayList objArlist)
		{
			//DELETE Old Data
			string	strDelStoredProc =	"PROC_DELETE_APP_IM_SCH_ITEMS_CVGS_ALL";
			string  strInStoredProc =	"PROC_INSERTAPP_IM_SCH_ITEMS_CVGS";
			int customerID=0;
			int appID=0;
			int appVersionID=0;
			//int dwellingID=0;
			//int retVal=1;
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	

			try
			{
				for(int i = 0; i < objArlist.Count; i++ )
				{
					Cms.Model.Application.HomeOwners.ClsSchItemsCovgInfo  objSchItem = (ClsSchItemsCovgInfo)objArlist[i];
					customerID = objSchItem.CUSTOMER_ID;
					appID = objSchItem.APP_ID;
					appVersionID = objSchItem.APP_VERSION_ID;
					objWrapper.AddParameter("@CUSTOMER_ID",objSchItem.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objSchItem.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objSchItem.APP_VERSION_ID);					
					objWrapper.AddParameter("@ITEM_ID",objSchItem.COVERAGE_CODE_ID);
					if(objSchItem.Action=="I")
					{

						objWrapper.AddParameter("@CREATED_BY",objSchItem.CREATED_BY);
						objWrapper.AddParameter("@CREATED_DATETIME",objSchItem.CREATED_DATETIME);
						objWrapper.AddParameter("@DEDUCTIBLE",objSchItem.DEDUCTIBLE);
						objWrapper.ExecuteNonQuery(strInStoredProc);
					}
					else if(objSchItem.Action=="D")
					{

						objWrapper.ExecuteNonQuery(strDelStoredProc);

					}
					objWrapper.ClearParameteres();
				}
				

				//Update Coverage On Marine Dependency
				ClsHomeCoverages objCoverage=new ClsHomeCoverages();
				objCoverage.UpdateCoveragesByRuleApp(objWrapper,customerID,appID ,
					appVersionID,RuleType.OtherAppDependent);

				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return 1;
			}

			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}

		}
	}
}
