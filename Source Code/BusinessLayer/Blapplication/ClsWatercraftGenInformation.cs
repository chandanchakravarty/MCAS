/******************************************************************************************
<Author				: -   Anurag Verma
<Start Date				: -	5/16/2005 5:39:24 PM
<End Date				: -	
<Description				: - 	Business login implemented for watercraft general information screen
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Anshuman
<Modified By			: - June 07, 2005
<Purpose				: - transaction description modified

<Modified Date			: - Vijay Arora
<Modified By			: - 01-12-2005
<Purpose				: - Added the Policy Functions Region
*******************************************************************************************/ 

using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication;
//using Cms.Model.Application.Watercrafts;

using Cms.DataLayer; 


namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsWatercraftGenInformation.
	/// </summary>
	public class ClsWatercraftGenInformation : Cms.BusinessLayer.BlApplication.clsapplication   
	{
        private const	string		APP_WATERCRAFT_GEN_INFO			=	"APP_WATERCRAFT_GEN_INFO";

        #region Private Instance Variables
        private			bool		boolTransactionLog;
        // private int _APP_ID;
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

        #region FETCHING DATA
        public DataSet FetchData(int appId,int customerId,int appVersionId)
        {
            string		strStoredProc	=	"Proc_FetchWatercraftGenInfo";
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

		public string FetchAppNumber(int appId,int customerId,int appVersionId)
		{
			string		strStoredProc	=	"Proc_GetApplicationNumber";
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",appId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionId,SqlDbType.Int);           

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			if(dsCount.Tables[0].Rows.Count>0)
				return dsCount.Tables[0].Rows[0]["APP_NUMBER"].ToString();
			}
			catch
			{
				return "";
			}
			finally
			{
				
			}
			return "";
		}

		// Fetch data Policy
		public DataSet FetchData_Pol(int polId,int customerId,int polVersionId)
		{
			string		strStoredProc	=	"Proc_FetchWatercraftGenInfo_Pol";
			DataSet dsCount=null;          		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",polId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",polVersionId,SqlDbType.Int);           
				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{}
			return dsCount;
		}

        public DataSet FetchHullData(int appId,int customerId,int appVersionId)
        {
            string		strStoredProc	=	"Proc_FetchWatercraftHullInfo";
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
        #endregion
        #region Constructors
        /// <summary>
        /// deafault constructor
        /// </summary>
        public ClsWatercraftGenInformation()
        {
            boolTransactionLog	= base.TransactionLogRequired;
        }
        #endregion

        #region Add(Insert) functions
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="objWatercraftGenInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
       /* public int Add(ClsWatercraftGenInfo objWatercraftGenInfo)
        {
            string		strStoredProc	=	"Proc_InsertAPP_WATERCRAFT_GEN_INFO";
            DateTime	RecordDate		=	DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftGenInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftGenInfo.APP_VERSION_ID);
                objDataWrapper.AddParameter("@APP_ID",objWatercraftGenInfo.APP_ID);    
                //objDataWrapper.AddParameter("@HAS_CURR_ADD_THREE_YEARS",objWatercraftGenInfo.HAS_CURR_ADD_THREE_YEARS);
                objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED",objWatercraftGenInfo.PHY_MENTL_CHALLENGED);
                objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED",objWatercraftGenInfo.DRIVER_SUS_REVOKED);
                objDataWrapper.AddParameter("@IS_CONVICTED_ACCIDENT",objWatercraftGenInfo.IS_CONVICTED_ACCIDENT);
                objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP",objWatercraftGenInfo.ANY_OTH_INSU_COMP);
                objDataWrapper.AddParameter("@OTHER_POLICY_NUMBER_LIST",objWatercraftGenInfo.OTHER_POLICY_NUMBER_LIST);
                objDataWrapper.AddParameter("@ANY_LOSS_THREE_YEARS",objWatercraftGenInfo.ANY_LOSS_THREE_YEARS);
                objDataWrapper.AddParameter("@COVERAGE_DECLINED",objWatercraftGenInfo.COVERAGE_DECLINED);
				//Nov12,2005:Sumit Chhabra:Following field is being removed
                //objDataWrapper.AddParameter("@DEGREE_CONVICTION",objWatercraftGenInfo.DEGREE_CONVICTION);
                //objDataWrapper.AddParameter("@IS_CREDIT",objWatercraftGenInfo.IS_CREDIT);
               // objDataWrapper.AddParameter("@CREDIT_DETAILS",objWatercraftGenInfo.CREDIT_DETAILS);
                objDataWrapper.AddParameter("@IS_RENTED_OTHERS",objWatercraftGenInfo.IS_RENTED_OTHERS);
                objDataWrapper.AddParameter("@IS_REGISTERED_OTHERS",objWatercraftGenInfo.IS_REGISTERED_OTHERS);
                objDataWrapper.AddParameter("@IS_ACTIVE",objWatercraftGenInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY",objWatercraftGenInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME",objWatercraftGenInfo.CREATED_DATETIME); 
               
			   // objDataWrapper.AddParameter("@HAS_CURR_ADD_THREE_YEARS_DESC",objWatercraftGenInfo.HAS_CURR_ADD_THREE_YEARS_DESC);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED_DESC",objWatercraftGenInfo.PHY_MENTL_CHALLENGED_DESC);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED_DESC",objWatercraftGenInfo.DRIVER_SUS_REVOKED_DESC);
				objDataWrapper.AddParameter("@IS_CONVICTED_ACCIDENT_DESC",objWatercraftGenInfo.IS_CONVICTED_ACCIDENT_DESC);
		//		objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP_DESC",objWatercraftGenInfo.ANY_OTH_INSU_COMP_DESC);
				objDataWrapper.AddParameter("@ANY_LOSS_THREE_YEARS_DESC",objWatercraftGenInfo.ANY_LOSS_THREE_YEARS_DESC);
				objDataWrapper.AddParameter("@COVERAGE_DECLINED_DESC",objWatercraftGenInfo.COVERAGE_DECLINED_DESC);
				//Nov12,2005:Sumit Chhabra:Following field is being removed
				//objDataWrapper.AddParameter("@DEGREE_CONVICTION_DESC",objWatercraftGenInfo.DEGREE_CONVICTION_DESC);
				objDataWrapper.AddParameter("@IS_RENTED_OTHERS_DESC",objWatercraftGenInfo.IS_RENTED_OTHERS_DESC);
				objDataWrapper.AddParameter("@IS_REGISTERED_OTHERS_DESC",objWatercraftGenInfo.IS_REGISTERED_OTHERS_DESC);
				objDataWrapper.AddParameter("@MINOR_VIOLATION",objWatercraftGenInfo.MINOR_VIOLATION);
				objDataWrapper.AddParameter("@DRINK_DRUG_VOILATION",objWatercraftGenInfo.DRINK_DRUG_VOILATION);
				objDataWrapper.AddParameter("@PARTICIPATE_RACE",objWatercraftGenInfo.PARTICIPATE_RACE);
				objDataWrapper.AddParameter("@CARRY_PASSENGER_FOR_CHARGE",objWatercraftGenInfo.CARRY_PASSENGER_FOR_CHARGE);
				objDataWrapper.AddParameter("@PARTICIPATE_RACE_DESC",objWatercraftGenInfo.PARTICIPATE_RACE_DESC);
				objDataWrapper.AddParameter("@CARRY_PASSENGER_FOR_CHARGE_DESC",objWatercraftGenInfo.CARRY_PASSENGER_FOR_CHARGE_DESC);

				//ADDEd by SHAFI
				objDataWrapper.AddParameter("@IS_PRIOR_INSURANCE_CARRIER",objWatercraftGenInfo.IS_PRIOR_INSURANCE_CARRIER);
                objDataWrapper.AddParameter("@PRIOR_INSURANCE_CARRIER_DESC",objWatercraftGenInfo.PRIOR_INSURANCE_CARRIER_DESC);

				objDataWrapper.AddParameter("@IS_BOAT_COOWNED",objWatercraftGenInfo.IS_BOAT_COOWNED);
				objDataWrapper.AddParameter("@IS_BOAT_COOWNED_DESC",objWatercraftGenInfo.IS_BOAT_COOWNED_DESC);

				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED",objWatercraftGenInfo.MULTI_POLICY_DISC_APPLIED );
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED_PP_DESC",objWatercraftGenInfo.MULTI_POLICY_DISC_APPLIED_PP_DESC );
				
				objDataWrapper.AddParameter("@ANY_BOAT_AMPHIBIOUS",objWatercraftGenInfo.ANY_BOAT_AMPHIBIOUS );
				objDataWrapper.AddParameter("@ANY_BOAT_AMPHIBIOUS_DESC",objWatercraftGenInfo.ANY_BOAT_AMPHIBIOUS_DESC );
				objDataWrapper.AddParameter("@ANY_BOAT_RESIDENCE",objWatercraftGenInfo.ANY_BOAT_RESIDENCE );
				objDataWrapper.AddParameter("@ANY_BOAT_RESIDENCE_DESC",objWatercraftGenInfo.ANY_BOAT_RESIDENCE_DESC );

				objDataWrapper.AddParameter("@IS_BOAT_USED_IN_ANY_WATER",objWatercraftGenInfo.IS_BOAT_USED_IN_ANY_WATER );
				objDataWrapper.AddParameter("@IS_BOAT_USED_IN_ANY_WATER_DESC",objWatercraftGenInfo.IS_BOAT_USED_IN_ANY_WATER_DESC );



             


                int returnResult = 0;
                if(TransactionLogRequired)
                {
                    objWatercraftGenInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/Watercrafts/AddWatercraftGenInformation.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objWatercraftGenInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = objWatercraftGenInfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID = objWatercraftGenInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objWatercraftGenInfo.APP_VERSION_ID;
                    objTransactionInfo.RECORDED_BY		=	objWatercraftGenInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC		=	"Underwriting question is added";
                    objTransactionInfo.CHANGE_XML		=	strTranXML;
                    //Executing the query
                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                }
                else
                {
                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (returnResult == -1)
                {
                    return -1;
                }
                else
                {
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
        }*/
        #endregion
		public static int CheckExistancePolicyHome(int customer_id)
		{
			string		strStoredProc	=	"Proc_CheckExistPolicyHome";
			int retValue=0;
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMERID",customer_id);
				DataSet da  =    objDataWrapper.ExecuteDataSet(strStoredProc);
				if(da.Tables.Count > 0 )
				{
					retValue=int.Parse(da.Tables[0].Rows[0][0].ToString());

				}
				return retValue;
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


        #region Add(Insert) functions
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="objWatercraftHullInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
//        public int AddHull(ClsWatercraftHullInfo objWatercraftHullInfo)
//        {
//            string		strStoredProc	=	"Proc_InsertAPP_WATERCRAFT_HULL_INFO";
//            DateTime	RecordDate		=	DateTime.Now;
//            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
//
//            try
//            {
//                objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftHullInfo.CUSTOMER_ID);
//                objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftHullInfo.APP_VERSION_ID);
//                objDataWrapper.AddParameter("@APP_ID",objWatercraftHullInfo.APP_ID);
//                objDataWrapper.AddParameter("@BOAT_CHARTERED_OTHERS",objWatercraftHullInfo.BOAT_CHARTERED_OTHERS);
//                objDataWrapper.AddParameter("@BOAT_BUSINESS",objWatercraftHullInfo.BOAT_BUSINESS);
//                objDataWrapper.AddParameter("@BOAT_RACING",objWatercraftHullInfo.BOAT_RACING);
//                
//                objDataWrapper.AddParameter("@EMP_CREW",objWatercraftHullInfo.EMP_CREW);
//				//<Gaurav> 2 June 2005 ; START: Following fields not required now; BUG NO:<511>
//				/*
//                objDataWrapper.AddParameter("@BOAT_WAT_SKII",objWatercraftHullInfo.BOAT_WAT_SKII);
//				objDataWrapper.AddParameter("@SLEEP_FACLTY",objWatercraftHullInfo.SLEEP_FACLTY);
//                objDataWrapper.AddParameter("@NO_OF_BEDS",objWatercraftHullInfo.NO_OF_BEDS);
//                */
//				//<Gaurav> 2 June 2005 ; END: Following fields not required now; BUG NO:<511>
//
//				objDataWrapper.AddParameter("@BOAT_DAMAGE",objWatercraftHullInfo.BOAT_DAMAGE);
//                objDataWrapper.AddParameter("@OPR_UNDER21",objWatercraftHullInfo.OPR_UNDER21);
//                objDataWrapper.AddParameter("@OPR_INFO",objWatercraftHullInfo.OPR_INFO);
//                objDataWrapper.AddParameter("@IS_ACTIVE",objWatercraftHullInfo.IS_ACTIVE);
//                objDataWrapper.AddParameter("@CREATED_BY",objWatercraftHullInfo.CREATED_BY);
//                objDataWrapper.AddParameter("@CREATED_DATETIME",objWatercraftHullInfo.CREATED_DATETIME);
//                
//
//                int returnResult = 0;
//                if(TransactionLogRequired)
//                {
//                    objWatercraftHullInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/Watercrafts/AddWatercraftHullInformation.aspx.resx");
//                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
//                    string strTranXML = objBuilder.GetTransactionLogXML(objWatercraftHullInfo);
//                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//                    objTransactionInfo.TRANS_TYPE_ID	=	1;
//					objTransactionInfo.CLIENT_ID = objWatercraftHullInfo.CUSTOMER_ID;
//					objTransactionInfo.APP_ID = objWatercraftHullInfo.APP_ID;
//					objTransactionInfo.APP_VERSION_ID = objWatercraftHullInfo.APP_VERSION_ID;
//                    objTransactionInfo.RECORDED_BY		=	objWatercraftHullInfo.CREATED_BY;
//                    objTransactionInfo.TRANS_DESC		=	"New watercraft's hull information is added";
//                    objTransactionInfo.CHANGE_XML		=	strTranXML;
//                    //Executing the query
//                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
//                }
//                else
//                {
//                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
//                }
//                
//                objDataWrapper.ClearParameteres();
//                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//                if (returnResult == -1)
//                {
//                    return -1;
//                }
//                else
//                {
//                   return returnResult;
//                }
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


        #region Update method
        /// <summary>
        /// Update method that recieves Model object to save.
        /// </summary>
        /// <param name="objOldWatercraftGenInfo">Model object having old information</param>
        /// <param name="objWatercraftGenInfo">Model object having new information(form control's value)</param>
        /// <returns>No. of rows updated (1 or 0)</returns>
       /* public int Update(ClsWatercraftGenInfo objOldWatercraftGenInfo,ClsWatercraftGenInfo objWatercraftGenInfo)
        {
            string strTranXML;
            int returnResult = 0;
            string strStoredProc="Proc_UpdateAPP_WATERCRAFT_GEN_INFO";
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
            try 
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftGenInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftGenInfo.APP_VERSION_ID);
                objDataWrapper.AddParameter("@APP_ID",objWatercraftGenInfo.APP_ID);
                //objDataWrapper.AddParameter("@HAS_CURR_ADD_THREE_YEARS",objWatercraftGenInfo.HAS_CURR_ADD_THREE_YEARS);
                objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED",objWatercraftGenInfo.PHY_MENTL_CHALLENGED);
                objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED",objWatercraftGenInfo.DRIVER_SUS_REVOKED);
                objDataWrapper.AddParameter("@IS_CONVICTED_ACCIDENT",objWatercraftGenInfo.IS_CONVICTED_ACCIDENT);
                objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP",objWatercraftGenInfo.ANY_OTH_INSU_COMP);
                objDataWrapper.AddParameter("@OTHER_POLICY_NUMBER_LIST",objWatercraftGenInfo.OTHER_POLICY_NUMBER_LIST);
                objDataWrapper.AddParameter("@ANY_LOSS_THREE_YEARS",objWatercraftGenInfo.ANY_LOSS_THREE_YEARS);
                objDataWrapper.AddParameter("@COVERAGE_DECLINED",objWatercraftGenInfo.COVERAGE_DECLINED);
				//No14,2005:Sumit Chhabra:Field is being removed
                //objDataWrapper.AddParameter("@DEGREE_CONVICTION",objWatercraftGenInfo.DEGREE_CONVICTION);
               // objDataWrapper.AddParameter("@IS_CREDIT",objWatercraftGenInfo.IS_CREDIT);
               // objDataWrapper.AddParameter("@CREDIT_DETAILS",objWatercraftGenInfo.CREDIT_DETAILS);
                objDataWrapper.AddParameter("@IS_RENTED_OTHERS",objWatercraftGenInfo.IS_RENTED_OTHERS);
                objDataWrapper.AddParameter("@IS_REGISTERED_OTHERS",objWatercraftGenInfo.IS_REGISTERED_OTHERS);
                objDataWrapper.AddParameter("@MODIFIED_BY",objWatercraftGenInfo.MODIFIED_BY);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWatercraftGenInfo.LAST_UPDATED_DATETIME);


				//objDataWrapper.AddParameter("@HAS_CURR_ADD_THREE_YEARS_DESC",objWatercraftGenInfo.HAS_CURR_ADD_THREE_YEARS_DESC);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED_DESC",objWatercraftGenInfo.PHY_MENTL_CHALLENGED_DESC);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED_DESC",objWatercraftGenInfo.DRIVER_SUS_REVOKED_DESC);
				objDataWrapper.AddParameter("@IS_CONVICTED_ACCIDENT_DESC",objWatercraftGenInfo.IS_CONVICTED_ACCIDENT_DESC);
		//		objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP_DESC",objWatercraftGenInfo.ANY_OTH_INSU_COMP_DESC);
				objDataWrapper.AddParameter("@ANY_LOSS_THREE_YEARS_DESC",objWatercraftGenInfo.ANY_LOSS_THREE_YEARS_DESC);
				objDataWrapper.AddParameter("@COVERAGE_DECLINED_DESC",objWatercraftGenInfo.COVERAGE_DECLINED_DESC);
				//objDataWrapper.AddParameter("@DEGREE_CONVICTION_DESC",objWatercraftGenInfo.DEGREE_CONVICTION_DESC);
				objDataWrapper.AddParameter("@IS_RENTED_OTHERS_DESC",objWatercraftGenInfo.IS_RENTED_OTHERS_DESC);
				objDataWrapper.AddParameter("@IS_REGISTERED_OTHERS_DESC",objWatercraftGenInfo.IS_REGISTERED_OTHERS_DESC);
				objDataWrapper.AddParameter("@MINOR_VIOLATION",objWatercraftGenInfo.MINOR_VIOLATION);
				objDataWrapper.AddParameter("@DRINK_DRUG_VOILATION",objWatercraftGenInfo.DRINK_DRUG_VOILATION);
				objDataWrapper.AddParameter("@PARTICIPATE_RACE",objWatercraftGenInfo.PARTICIPATE_RACE);
				objDataWrapper.AddParameter("@CARRY_PASSENGER_FOR_CHARGE",objWatercraftGenInfo.CARRY_PASSENGER_FOR_CHARGE);
				objDataWrapper.AddParameter("@PARTICIPATE_RACE_DESC",objWatercraftGenInfo.PARTICIPATE_RACE_DESC);
				objDataWrapper.AddParameter("@CARRY_PASSENGER_FOR_CHARGE_DESC",objWatercraftGenInfo.CARRY_PASSENGER_FOR_CHARGE_DESC);

				//Added By Shafi

				objDataWrapper.AddParameter("@IS_PRIOR_INSURANCE_CARRIER",objWatercraftGenInfo.IS_PRIOR_INSURANCE_CARRIER);
				objDataWrapper.AddParameter("@PRIOR_INSURANCE_CARRIER_DESC",objWatercraftGenInfo.PRIOR_INSURANCE_CARRIER_DESC);

				objDataWrapper.AddParameter("@IS_BOAT_COOWNED",objWatercraftGenInfo.IS_BOAT_COOWNED);
				objDataWrapper.AddParameter("@IS_BOAT_COOWNED_DESC",objWatercraftGenInfo.IS_BOAT_COOWNED_DESC);

				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED",objWatercraftGenInfo.MULTI_POLICY_DISC_APPLIED );
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED_PP_DESC",objWatercraftGenInfo.MULTI_POLICY_DISC_APPLIED_PP_DESC );
				objDataWrapper.AddParameter("@ANY_BOAT_AMPHIBIOUS",objWatercraftGenInfo.ANY_BOAT_AMPHIBIOUS);
				objDataWrapper.AddParameter("@ANY_BOAT_AMPHIBIOUS_DESC",objWatercraftGenInfo.ANY_BOAT_AMPHIBIOUS_DESC );
				objDataWrapper.AddParameter("@ANY_BOAT_RESIDENCE",objWatercraftGenInfo.ANY_BOAT_RESIDENCE);
				objDataWrapper.AddParameter("@ANY_BOAT_RESIDENCE_DESC",objWatercraftGenInfo.ANY_BOAT_RESIDENCE_DESC );

				objDataWrapper.AddParameter("@IS_BOAT_USED_IN_ANY_WATER",objWatercraftGenInfo.IS_BOAT_USED_IN_ANY_WATER);
				objDataWrapper.AddParameter("@IS_BOAT_USED_IN_ANY_WATER_DESC",objWatercraftGenInfo.IS_BOAT_USED_IN_ANY_WATER_DESC );



				


                if(TransactionLogRequired) 
                {
					objWatercraftGenInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/Watercrafts/AddWatercraftGenInformation.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    strTranXML = objBuilder.GetTransactionLogXML(objOldWatercraftGenInfo,objWatercraftGenInfo);
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{	                    
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.CLIENT_ID = objWatercraftGenInfo.CUSTOMER_ID;
						objTransactionInfo.APP_ID = objWatercraftGenInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objWatercraftGenInfo.APP_VERSION_ID;
						objTransactionInfo.RECORDED_BY		=	objWatercraftGenInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Underwriting question is modified";
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
        */
        #endregion
        #region Update method
        /// <summary>
        /// Update method that recieves Model object to save.
        /// </summary>
        /// <param name="objOldWatercraftHullInfo">Model object having old information</param>
        /// <param name="objWatercraftHullInfo">Model object having new information(form control's value)</param>
        /// <returns>No. of rows updated (1 or 0)</returns>
//        public int UpdateHull(ClsWatercraftHullInfo objOldWatercraftHullInfo,ClsWatercraftHullInfo objWatercraftHullInfo)
//        {
//            string strTranXML;
//            int returnResult = 0;
//            string strStoredProc="Proc_UpdateAPP_WATERCRAFT_HULL_INFO";
//            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
//            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
//            try 
//            {
//                objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftHullInfo.CUSTOMER_ID);
//                objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftHullInfo.APP_VERSION_ID);
//                objDataWrapper.AddParameter("@APP_ID",objWatercraftHullInfo.APP_ID);
//                objDataWrapper.AddParameter("@BOAT_CHARTERED_OTHERS",objWatercraftHullInfo.BOAT_CHARTERED_OTHERS);
//                objDataWrapper.AddParameter("@BOAT_BUSINESS",objWatercraftHullInfo.BOAT_BUSINESS);
//                objDataWrapper.AddParameter("@BOAT_RACING",objWatercraftHullInfo.BOAT_RACING);
//				
//                objDataWrapper.AddParameter("@EMP_CREW",objWatercraftHullInfo.EMP_CREW);
//				//<Gaurav> 2 June 2005 ; START: Following fields not required now; BUG NO:<511>
//				/*
//				objDataWrapper.AddParameter("@BOAT_WAT_SKII",objWatercraftHullInfo.BOAT_WAT_SKII);
//				objDataWrapper.AddParameter("@SLEEP_FACLTY",objWatercraftHullInfo.SLEEP_FACLTY);
//                objDataWrapper.AddParameter("@NO_OF_BEDS",objWatercraftHullInfo.NO_OF_BEDS);
//				*/
//				//<Gaurav> 2 June 2005 ; END: Following fields not required now; BUG NO:<511>
//                
//				objDataWrapper.AddParameter("@BOAT_DAMAGE",objWatercraftHullInfo.BOAT_DAMAGE);
//                objDataWrapper.AddParameter("@OPR_UNDER21",objWatercraftHullInfo.OPR_UNDER21);
//                objDataWrapper.AddParameter("@OPR_INFO",objWatercraftHullInfo.OPR_INFO);                
//                objDataWrapper.AddParameter("@MODIFIED_BY",objWatercraftHullInfo.MODIFIED_BY);
//                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWatercraftHullInfo.LAST_UPDATED_DATETIME);
//                if(TransactionLogRequired) 
//                {
//                    strTranXML = objBuilder.GetTransactionLogXML(objOldWatercraftHullInfo,objWatercraftHullInfo);
//
//                    objWatercraftHullInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/Watercrafts/AddWatercraftHullInformation.aspx.resx");Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//                    objTransactionInfo.TRANS_TYPE_ID	=	3;
//					objTransactionInfo.CLIENT_ID = objWatercraftHullInfo.CUSTOMER_ID;
//					objTransactionInfo.APP_ID = objWatercraftHullInfo.APP_ID;
//					objTransactionInfo.APP_VERSION_ID = objWatercraftHullInfo.APP_VERSION_ID;
//                    objTransactionInfo.RECORDED_BY		=	objWatercraftHullInfo.MODIFIED_BY;
//                    objTransactionInfo.TRANS_DESC		=	"Watercraft's hull information is modified";
//                    objTransactionInfo.CHANGE_XML		=	strTranXML;
//                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
//
//                }
//                else
//                {
//                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
//                }
//                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//                return returnResult;
//            }
//            catch(Exception ex)
//            {
//                throw(ex);
//            }
//            finally
//            {
//                if(objDataWrapper != null) 
//                {
//                    objDataWrapper.Dispose();
//                }
//                if(objBuilder != null) 
//                {
//                    objBuilder = null;
//                }
//            }
//        }
        #endregion

		#region Save Watercrat Gen Info
		/// <summary>
		/// Saves the information passed in model object to database. :  15 feb 2006 praveen k 
		/// </summary>
		/// <param name="objPPGeneralInformation">Model class object.</param>
		/// <returns>No of records effected.</returns>
		/*public int Save(ClsWatercraftGenInfo objWatercraftGenInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_SAVE_APP_WATERCRAFT_GEN_INFO_ACORD";
			DateTime	RecordDate		=	DateTime.Now;
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftGenInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftGenInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@APP_ID",objWatercraftGenInfo.APP_ID);
			objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED",objWatercraftGenInfo.PHY_MENTL_CHALLENGED);
			objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED",objWatercraftGenInfo.DRIVER_SUS_REVOKED);
			objDataWrapper.AddParameter("@IS_CONVICTED_ACCIDENT",objWatercraftGenInfo.IS_CONVICTED_ACCIDENT);
			objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP",objWatercraftGenInfo.ANY_OTH_INSU_COMP);
			objDataWrapper.AddParameter("@OTHER_POLICY_NUMBER_LIST",objWatercraftGenInfo.OTHER_POLICY_NUMBER_LIST);
			objDataWrapper.AddParameter("@ANY_LOSS_THREE_YEARS",objWatercraftGenInfo.ANY_LOSS_THREE_YEARS);
			objDataWrapper.AddParameter("@COVERAGE_DECLINED",objWatercraftGenInfo.COVERAGE_DECLINED);
			objDataWrapper.AddParameter("@IS_RENTED_OTHERS",objWatercraftGenInfo.IS_RENTED_OTHERS);
			objDataWrapper.AddParameter("@IS_REGISTERED_OTHERS",objWatercraftGenInfo.IS_REGISTERED_OTHERS);
			objDataWrapper.AddParameter("@IS_ACTIVE",objWatercraftGenInfo.IS_ACTIVE);
			objDataWrapper.AddParameter("@CREATED_BY",objWatercraftGenInfo.CREATED_BY);
			objDataWrapper.AddParameter("@CREATED_DATETIME",DateTime.Now); 
			objDataWrapper.AddParameter("@MODIFIED_BY",null);
			//objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWatercraftGenInfo.LAST_UPDATED_DATETIME);
			//objDataWrapper.AddParameter("@HAS_CURR_ADD_THREE_YEARS_DESC",objWatercraftGenInfo.HAS_CURR_ADD_THREE_YEARS_DESC);
			objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED_DESC",objWatercraftGenInfo.PHY_MENTL_CHALLENGED_DESC);
			objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED_DESC",objWatercraftGenInfo.DRIVER_SUS_REVOKED_DESC);
			objDataWrapper.AddParameter("@IS_CONVICTED_ACCIDENT_DESC",objWatercraftGenInfo.IS_CONVICTED_ACCIDENT_DESC);
			//		objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP_DESC",objWatercraftGenInfo.ANY_OTH_INSU_COMP_DESC);
			objDataWrapper.AddParameter("@ANY_LOSS_THREE_YEARS_DESC",objWatercraftGenInfo.ANY_LOSS_THREE_YEARS_DESC);
			objDataWrapper.AddParameter("@COVERAGE_DECLINED_DESC",objWatercraftGenInfo.COVERAGE_DECLINED_DESC);
			//objDataWrapper.AddParameter("@DEGREE_CONVICTION_DESC",objWatercraftGenInfo.DEGREE_CONVICTION_DESC);
			objDataWrapper.AddParameter("@IS_RENTED_OTHERS_DESC",objWatercraftGenInfo.IS_RENTED_OTHERS_DESC);
			objDataWrapper.AddParameter("@IS_REGISTERED_OTHERS_DESC",objWatercraftGenInfo.IS_REGISTERED_OTHERS_DESC);
			objDataWrapper.AddParameter("@MINOR_VIOLATION",objWatercraftGenInfo.MINOR_VIOLATION);
			objDataWrapper.AddParameter("@DRINK_DRUG_VOILATION",objWatercraftGenInfo.DRINK_DRUG_VOILATION);
			objDataWrapper.AddParameter("@PARTICIPATE_RACE",objWatercraftGenInfo.PARTICIPATE_RACE);
			objDataWrapper.AddParameter("@CARRY_PASSENGER_FOR_CHARGE",objWatercraftGenInfo.CARRY_PASSENGER_FOR_CHARGE);
			objDataWrapper.AddParameter("@PARTICIPATE_RACE_DESC",objWatercraftGenInfo.PARTICIPATE_RACE_DESC);
			objDataWrapper.AddParameter("@CARRY_PASSENGER_FOR_CHARGE_DESC",objWatercraftGenInfo.CARRY_PASSENGER_FOR_CHARGE_DESC);

			objDataWrapper.AddParameter("@IS_PRIOR_INSURANCE_CARRIER",objWatercraftGenInfo.IS_PRIOR_INSURANCE_CARRIER);
			objDataWrapper.AddParameter("@PRIOR_INSURANCE_CARRIER_DESC",objWatercraftGenInfo.PRIOR_INSURANCE_CARRIER_DESC);

			objDataWrapper.AddParameter("@IS_BOAT_COOWNED",objWatercraftGenInfo.IS_BOAT_COOWNED);
			objDataWrapper.AddParameter("@IS_BOAT_COOWNED_DESC",objWatercraftGenInfo.IS_BOAT_COOWNED_DESC);
			objDataWrapper.AddParameter("@BOAT_HOME_DISCOUNT",objWatercraftGenInfo.BOAT_HOME_DISCOUNT);

			//Added on 11 MAY 2006
			objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED",objWatercraftGenInfo.MULTI_POLICY_DISC_APPLIED);
			objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED_PP_DESC",objWatercraftGenInfo.MULTI_POLICY_DISC_APPLIED_PP_DESC);

			objDataWrapper.AddParameter("@ANY_BOAT_AMPHIBIOUS",objWatercraftGenInfo.ANY_BOAT_AMPHIBIOUS);
			objDataWrapper.AddParameter("@ANY_BOAT_AMPHIBIOUS_DESC",objWatercraftGenInfo.ANY_BOAT_AMPHIBIOUS_DESC);
			
			objDataWrapper.AddParameter("@ANY_BOAT_RESIDENCE",objWatercraftGenInfo.ANY_BOAT_RESIDENCE);
			objDataWrapper.AddParameter("@ANY_BOAT_RESIDENCE_DESC",objWatercraftGenInfo.ANY_BOAT_RESIDENCE_DESC);

			objDataWrapper.AddParameter("@IS_BOAT_USED_IN_ANY_WATER",objWatercraftGenInfo.IS_BOAT_USED_IN_ANY_WATER);
			objDataWrapper.AddParameter("@IS_BOAT_USED_IN_ANY_WATER_DESC",objWatercraftGenInfo.IS_BOAT_USED_IN_ANY_WATER_DESC);
			
			//End 



			int returnResult = 0;
			if(TransactionLogRequired) 
			{
				objWatercraftGenInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/Watercrafts/AddWatercraftGenInformation.aspx.resx");
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objWatercraftGenInfo);
				if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
				else				
				{	                    
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID = objWatercraftGenInfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID = objWatercraftGenInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objWatercraftGenInfo.APP_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftGenInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Watercraft's general information is modified";
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
			//				}
			
		}*/
		#endregion

		public static string getHomeOwnerActivePolicyList(int customer_id)
		{
			string strStoredProcName	= "PROC_GET_HOMEOWNER_ACTIVE_POLICY_LIST";
			string strActivePolicyList	= "";
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMERID",customer_id);
				DataSet da  =    objDataWrapper.ExecuteDataSet(strStoredProcName);
				if(da.Tables.Count > 0 )
				{
					strActivePolicyList = da.Tables[0].Rows[0]["ACTPOLLIST"].ToString();
				}
				return strActivePolicyList;
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



		#region POLICY FUNCTIONS
		
		/// <summary>
		/// Fetch the Policy WaterCraft General Information Details.
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		/// <returns></returns>
		public DataSet FetchPolicyWaterCraftGenInfo(int CustomerID,int PolicyID, int PolicyVersionID)
		{
			string		strStoredProc	=	"Proc_FetchPolicyWatercraftGenInfo";
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


		/// <summary>
		/// Add the Policy WaterCraft General Information Details
		/// </summary>
		/// <param name="objWatercraftGenInfo"></param>
		/// <returns></returns>
		public int AddPolicyWaterCraftGenInfo(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftGenInfo objWatercraftGenInfo)
		{
			string		strStoredProc	=	"Proc_InsertPOL_WATERCRAFT_GEN_INFO";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftGenInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWatercraftGenInfo.POLICY_ID);    
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWatercraftGenInfo.POLICY_VERSION_ID);
				//objDataWrapper.AddParameter("@HAS_CURR_ADD_THREE_YEARS",objWatercraftGenInfo.HAS_CURR_ADD_THREE_YEARS);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED",objWatercraftGenInfo.PHY_MENTL_CHALLENGED);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED",objWatercraftGenInfo.DRIVER_SUS_REVOKED);
				objDataWrapper.AddParameter("@IS_CONVICTED_ACCIDENT",objWatercraftGenInfo.IS_CONVICTED_ACCIDENT);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP",objWatercraftGenInfo.ANY_OTH_INSU_COMP);
				objDataWrapper.AddParameter("@OTHER_POLICY_NUMBER_LIST",objWatercraftGenInfo.OTHER_POLICY_NUMBER_LIST);
				objDataWrapper.AddParameter("@ANY_LOSS_THREE_YEARS",objWatercraftGenInfo.ANY_LOSS_THREE_YEARS);
				objDataWrapper.AddParameter("@COVERAGE_DECLINED",objWatercraftGenInfo.COVERAGE_DECLINED);
				//objDataWrapper.AddParameter("@IS_CREDIT",objWatercraftGenInfo.IS_CREDIT);
				//objDataWrapper.AddParameter("@CREDIT_DETAILS",objWatercraftGenInfo.CREDIT_DETAILS);
				objDataWrapper.AddParameter("@IS_RENTED_OTHERS",objWatercraftGenInfo.IS_RENTED_OTHERS);
				objDataWrapper.AddParameter("@IS_REGISTERED_OTHERS",objWatercraftGenInfo.IS_REGISTERED_OTHERS);
				objDataWrapper.AddParameter("@IS_ACTIVE",objWatercraftGenInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objWatercraftGenInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objWatercraftGenInfo.CREATED_DATETIME); 
				//objDataWrapper.AddParameter("@HAS_CURR_ADD_THREE_YEARS_DESC",objWatercraftGenInfo.HAS_CURR_ADD_THREE_YEARS_DESC);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED_DESC",objWatercraftGenInfo.PHY_MENTL_CHALLENGED_DESC);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED_DESC",objWatercraftGenInfo.DRIVER_SUS_REVOKED_DESC);
				objDataWrapper.AddParameter("@IS_CONVICTED_ACCIDENT_DESC",objWatercraftGenInfo.IS_CONVICTED_ACCIDENT_DESC);
				objDataWrapper.AddParameter("@ANY_LOSS_THREE_YEARS_DESC",objWatercraftGenInfo.ANY_LOSS_THREE_YEARS_DESC);
				objDataWrapper.AddParameter("@COVERAGE_DECLINED_DESC",objWatercraftGenInfo.COVERAGE_DECLINED_DESC);
				objDataWrapper.AddParameter("@IS_RENTED_OTHERS_DESC",objWatercraftGenInfo.IS_RENTED_OTHERS_DESC);
				objDataWrapper.AddParameter("@IS_REGISTERED_OTHERS_DESC",objWatercraftGenInfo.IS_REGISTERED_OTHERS_DESC);
				objDataWrapper.AddParameter("@MINOR_VIOLATION",objWatercraftGenInfo.MINOR_VIOLATION);
				objDataWrapper.AddParameter("@DRINK_DRUG_VOILATION",objWatercraftGenInfo.DRINK_DRUG_VOILATION);
				objDataWrapper.AddParameter("@PARTICIPATE_RACE",objWatercraftGenInfo.PARTICIPATE_RACE);
				objDataWrapper.AddParameter("@CARRY_PASSENGER_FOR_CHARGE",objWatercraftGenInfo.CARRY_PASSENGER_FOR_CHARGE);
				objDataWrapper.AddParameter("@PARTICIPATE_RACE_DESC",objWatercraftGenInfo.PARTICIPATE_RACE_DESC);
				objDataWrapper.AddParameter("@CARRY_PASSENGER_FOR_CHARGE_DESC",objWatercraftGenInfo.CARRY_PASSENGER_FOR_CHARGE_DESC);
				objDataWrapper.AddParameter("@IS_PRIOR_INSURANCE_CARRIER",objWatercraftGenInfo.IS_PRIOR_INSURANCE_CARRIER);
				objDataWrapper.AddParameter("@PRIOR_INSURANCE_CARRIER_DESC",objWatercraftGenInfo.PRIOR_INSURANCE_CARRIER_DESC);
				//objDataWrapper.AddParameter("@IS_PRIOR_INSURANCE_CARRIER",objWatercraftGenInfo.IS_PRIOR_INSURANCE_CARRIER);
				objDataWrapper.AddParameter("@IS_BOAT_COOWNED",objWatercraftGenInfo.IS_BOAT_COOWNED);
				objDataWrapper.AddParameter("@IS_BOAT_COOWNED_DESC",objWatercraftGenInfo.IS_BOAT_COOWNED_DESC);

				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED",objWatercraftGenInfo.MULTI_POLICY_DISC_APPLIED );
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED_PP_DESC",objWatercraftGenInfo.MULTI_POLICY_DISC_APPLIED_PP_DESC );
				objDataWrapper.AddParameter("@ANY_BOAT_AMPHIBIOUS",objWatercraftGenInfo.ANY_BOAT_AMPHIBIOUS);
				objDataWrapper.AddParameter("@ANY_BOAT_AMPHIBIOUS_DESC",objWatercraftGenInfo.ANY_BOAT_AMPHIBIOUS_DESC );
				objDataWrapper.AddParameter("@ANY_BOAT_RESIDENCE",objWatercraftGenInfo.ANY_BOAT_RESIDENCE);
				objDataWrapper.AddParameter("@ANY_BOAT_RESIDENCE_DESC",objWatercraftGenInfo.ANY_BOAT_RESIDENCE_DESC );

				objDataWrapper.AddParameter("@IS_BOAT_USED_IN_ANY_WATER",objWatercraftGenInfo.IS_BOAT_USED_IN_ANY_WATER);
				objDataWrapper.AddParameter("@IS_BOAT_USED_IN_ANY_WATER_DESC",objWatercraftGenInfo.IS_BOAT_USED_IN_ANY_WATER_DESC);




				

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objWatercraftGenInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Policies/Aspx/Watercraft/PolicyAddWatercraftGenInfo.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objWatercraftGenInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = objWatercraftGenInfo.CUSTOMER_ID;
					objTransactionInfo.POLICY_ID = objWatercraftGenInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objWatercraftGenInfo.POLICY_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftGenInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1514", "");// "Underwriting question is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
                
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (returnResult == -1)
				{
					return -1;
				}
				else
				{
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

		
		/// <summary>
		/// Update the Policy WaterCraft General Information Details
		/// </summary>
		/// <param name="objOldWatercraftGenInfo"></param>
		/// <param name="objWatercraftGenInfo"></param>
		/// <returns></returns>
	
		public int UpdatePolicyWaterCraftGenInfo(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftGenInfo objOldWatercraftGenInfo,Cms.Model.Policy.Watercraft.ClsPolicyWatercraftGenInfo objWatercraftGenInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string strStoredProc="Proc_UpdatePOL_WATERCRAFT_GEN_INFO";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftGenInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWatercraftGenInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWatercraftGenInfo.POLICY_VERSION_ID);
				//objDataWrapper.AddParameter("@HAS_CURR_ADD_THREE_YEARS",objWatercraftGenInfo.HAS_CURR_ADD_THREE_YEARS);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED",objWatercraftGenInfo.PHY_MENTL_CHALLENGED);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED",objWatercraftGenInfo.DRIVER_SUS_REVOKED);
				objDataWrapper.AddParameter("@IS_CONVICTED_ACCIDENT",objWatercraftGenInfo.IS_CONVICTED_ACCIDENT);
				objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP",objWatercraftGenInfo.ANY_OTH_INSU_COMP);
				objDataWrapper.AddParameter("@OTHER_POLICY_NUMBER_LIST",objWatercraftGenInfo.OTHER_POLICY_NUMBER_LIST);
				objDataWrapper.AddParameter("@ANY_LOSS_THREE_YEARS",objWatercraftGenInfo.ANY_LOSS_THREE_YEARS);
				objDataWrapper.AddParameter("@COVERAGE_DECLINED",objWatercraftGenInfo.COVERAGE_DECLINED);
				//objDataWrapper.AddParameter("@IS_CREDIT",objWatercraftGenInfo.IS_CREDIT);
				//objDataWrapper.AddParameter("@CREDIT_DETAILS",objWatercraftGenInfo.CREDIT_DETAILS);
				objDataWrapper.AddParameter("@IS_RENTED_OTHERS",objWatercraftGenInfo.IS_RENTED_OTHERS);
				objDataWrapper.AddParameter("@IS_REGISTERED_OTHERS",objWatercraftGenInfo.IS_REGISTERED_OTHERS);
				objDataWrapper.AddParameter("@MODIFIED_BY",objWatercraftGenInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWatercraftGenInfo.LAST_UPDATED_DATETIME);
				//objDataWrapper.AddParameter("@HAS_CURR_ADD_THREE_YEARS_DESC",objWatercraftGenInfo.HAS_CURR_ADD_THREE_YEARS_DESC);
				objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED_DESC",objWatercraftGenInfo.PHY_MENTL_CHALLENGED_DESC);
				objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED_DESC",objWatercraftGenInfo.DRIVER_SUS_REVOKED_DESC);
				objDataWrapper.AddParameter("@IS_CONVICTED_ACCIDENT_DESC",objWatercraftGenInfo.IS_CONVICTED_ACCIDENT_DESC);
				objDataWrapper.AddParameter("@ANY_LOSS_THREE_YEARS_DESC",objWatercraftGenInfo.ANY_LOSS_THREE_YEARS_DESC);
				objDataWrapper.AddParameter("@COVERAGE_DECLINED_DESC",objWatercraftGenInfo.COVERAGE_DECLINED_DESC);
				objDataWrapper.AddParameter("@IS_RENTED_OTHERS_DESC",objWatercraftGenInfo.IS_RENTED_OTHERS_DESC);
				objDataWrapper.AddParameter("@IS_REGISTERED_OTHERS_DESC",objWatercraftGenInfo.IS_REGISTERED_OTHERS_DESC);
				objDataWrapper.AddParameter("@MINOR_VIOLATION",objWatercraftGenInfo.MINOR_VIOLATION);
				objDataWrapper.AddParameter("@DRINK_DRUG_VOILATION",objWatercraftGenInfo.DRINK_DRUG_VOILATION);
				objDataWrapper.AddParameter("@PARTICIPATE_RACE",objWatercraftGenInfo.PARTICIPATE_RACE);
				objDataWrapper.AddParameter("@CARRY_PASSENGER_FOR_CHARGE",objWatercraftGenInfo.CARRY_PASSENGER_FOR_CHARGE);
				objDataWrapper.AddParameter("@PARTICIPATE_RACE_DESC",objWatercraftGenInfo.PARTICIPATE_RACE_DESC);
				objDataWrapper.AddParameter("@CARRY_PASSENGER_FOR_CHARGE_DESC",objWatercraftGenInfo.CARRY_PASSENGER_FOR_CHARGE_DESC);
				objDataWrapper.AddParameter("@IS_PRIOR_INSURANCE_CARRIER",objWatercraftGenInfo.IS_PRIOR_INSURANCE_CARRIER);
				objDataWrapper.AddParameter("@PRIOR_INSURANCE_CARRIER_DESC",objWatercraftGenInfo.PRIOR_INSURANCE_CARRIER_DESC);
				objDataWrapper.AddParameter("@IS_BOAT_COOWNED",objWatercraftGenInfo.IS_BOAT_COOWNED);
				objDataWrapper.AddParameter("@IS_BOAT_COOWNED_DESC",objWatercraftGenInfo.IS_BOAT_COOWNED_DESC);
				
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED",objWatercraftGenInfo.MULTI_POLICY_DISC_APPLIED );
				objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED_PP_DESC",objWatercraftGenInfo.MULTI_POLICY_DISC_APPLIED_PP_DESC );
				objDataWrapper.AddParameter("@ANY_BOAT_AMPHIBIOUS",objWatercraftGenInfo.ANY_BOAT_AMPHIBIOUS);
				objDataWrapper.AddParameter("@ANY_BOAT_AMPHIBIOUS_DESC",objWatercraftGenInfo.ANY_BOAT_AMPHIBIOUS_DESC );
				objDataWrapper.AddParameter("@ANY_BOAT_RESIDENCE",objWatercraftGenInfo.ANY_BOAT_RESIDENCE);
				objDataWrapper.AddParameter("@ANY_BOAT_RESIDENCE_DESC",objWatercraftGenInfo.ANY_BOAT_RESIDENCE_DESC );

				objDataWrapper.AddParameter("@IS_BOAT_USED_IN_ANY_WATER",objWatercraftGenInfo.IS_BOAT_USED_IN_ANY_WATER);
				objDataWrapper.AddParameter("@IS_BOAT_USED_IN_ANY_WATER_DESC",objWatercraftGenInfo.IS_BOAT_USED_IN_ANY_WATER_DESC);




				

				
				if(TransactionLogRequired) 
				{
					objWatercraftGenInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Watercraft/PolicyAddWatercraftGenInfo.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML = objBuilder.GetTransactionLogXML(objOldWatercraftGenInfo,objWatercraftGenInfo);
					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{                    
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.CLIENT_ID = objWatercraftGenInfo.CUSTOMER_ID;
						objTransactionInfo.POLICY_ID  = objWatercraftGenInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID = objWatercraftGenInfo.POLICY_VERSION_ID;
						objTransactionInfo.RECORDED_BY		=	objWatercraftGenInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Underwriting question is modified";
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
    }
}
