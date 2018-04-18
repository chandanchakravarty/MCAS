/******************************************************************************************
	<Author					: - > Anurag Verma	
	<Start Date				: -	> March 25,2005
	<End Date				: - >
	<Description			: - > This file contains functionality for adding and updating prior loss information
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - Anshuman
	<Modified By			: - June 07, 2005
	<Purpose				: - transaction description modified 
    
    <Modified Date			: - > 
	<Modified By			: - > 
	<Purpose				: - > 
*******************************************************************************************/

using System;
using System.Data;
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data.SqlClient;
using System.Configuration;
using Cms.Model.Application.PriorLoss;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;  




namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsPriorLoss.
	/// </summary>
	public class ClsPriorLoss : Cms.BusinessLayer.BlApplication.clsapplication 
	{
        private const	string		APP_PRIOR_LOSS_INFO			=	"APP_PRIOR_LOSS_INFO";

        #region Private Instance Variables
        private			bool		boolTransactionLog;
        // private int _LOSS_ID;
        private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateAPP_PRIOR_LOSS_INFO";
        #endregion
      
        #region Private Instance Variables
   
        //    private int _VALUE_ID;
        #endregion

        #region Public Properties
       /*
		public int 	DEFV_ID
        {
            get
            {
                return _VALUE_ID;
            }
        }
		*/

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

        #region Constructors
        /// <summary>
        /// deafault constructor
        /// </summary>
        public ClsPriorLoss()
		{
            boolTransactionLog	= base.TransactionLogRequired;
            //boolTransactionLog	= false;
            base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
        #endregion

        #region Add(Insert) functions
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="objPriorLossInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
        public int Add(ClsPriorLossInfo objPriorLossInfo)
        {
            string		strStoredProc	=	"Proc_InsertAPP_PRIOR_LOSS_INFO";
            DateTime	RecordDate		=	DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID",objPriorLossInfo.CUSTOMER_ID);                
                string oc_Date=objPriorLossInfo.OCCURENCE_DATE.Year.ToString();
                if(oc_Date  != "1900")
                    objDataWrapper.AddParameter("@OCCURENCE_DATE",objPriorLossInfo.OCCURENCE_DATE);
                else
                    objDataWrapper.AddParameter("@OCCURENCE_DATE",System.DBNull.Value);

              /*  if(objPriorLossInfo.CLAIM_DATE.Year != 1900)
                    objDataWrapper.AddParameter("@CLAIM_DATE",objPriorLossInfo.CLAIM_DATE);
                else
                    objDataWrapper.AddParameter("@CLAIM_DATE",System.DBNull.Value);	 */
				objDataWrapper.AddParameter("@CLAIM_DATE",null);

                objDataWrapper.AddParameter("@LOB",objPriorLossInfo.LOB);
                objDataWrapper.AddParameter("@LOSS_TYPE",objPriorLossInfo.LOSS_TYPE);

				if(objPriorLossInfo.AMOUNT_PAID==0)
				{
					objDataWrapper.AddParameter("@AMOUNT_PAID",null);
				}
				else
				{
					objDataWrapper.AddParameter("@AMOUNT_PAID",objPriorLossInfo.AMOUNT_PAID);
				}
			/*	if(objPriorLossInfo.AMOUNT_RESERVED==0)
				{
					objDataWrapper.AddParameter("@AMOUNT_RESERVED",null);
				}
				else
				{
					objDataWrapper.AddParameter("@AMOUNT_RESERVED",objPriorLossInfo.AMOUNT_RESERVED);
				}	*/
				  objDataWrapper.AddParameter("@AMOUNT_RESERVED",null);
				objDataWrapper.AddParameter("@CLAIM_STATUS",objPriorLossInfo.CLAIM_STATUS);
                //objDataWrapper.AddParameter("@LOSS_DESC",ReplaceInvalidCharecter(objPriorLossInfo.LOSS_DESC));
				objDataWrapper.AddParameter("@LOSS_DESC",null);
                objDataWrapper.AddParameter("@REMARKS",ReplaceInvalidCharecter(objPriorLossInfo.REMARKS));
             // objDataWrapper.AddParameter("@MOD",ReplaceInvalidCharecter(objPriorLossInfo.MOD));
				objDataWrapper.AddParameter("@MOD",null);
             //  objDataWrapper.AddParameter("@LOSS_RUN",objPriorLossInfo.LOSS_RUN);
				 objDataWrapper.AddParameter("@LOSS_RUN",null);

             //   objDataWrapper.AddParameter("@CAT_NO",ReplaceInvalidCharecter(objPriorLossInfo.CAT_NO));
				  objDataWrapper.AddParameter("@CAT_NO",null);
                objDataWrapper.AddParameter("@CLAIMID",objPriorLossInfo.CLAIMID);
                objDataWrapper.AddParameter("@IS_ACTIVE",objPriorLossInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY",objPriorLossInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME",objPriorLossInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@MODIFIED_BY",objPriorLossInfo.MODIFIED_BY);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objPriorLossInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@APLUS_REPORT_ORDERED",objPriorLossInfo.APLUS_REPORT_ORDERED);
				objDataWrapper.AddParameter("@NAME_MATCH",objPriorLossInfo.NAME_MATCH);//Done for Itrack Issue 6723 on 27 Nov 09
				objDataWrapper.AddParameter("@DRIVER_ID",objPriorLossInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@DRIVER_NAME",objPriorLossInfo.DRIVER_NAME);
				objDataWrapper.AddParameter("@RELATIONSHIP",objPriorLossInfo.RELATIONSHIP);
				objDataWrapper.AddParameter("@CLAIMS_TYPE",objPriorLossInfo.CLAIMS_TYPE);
				objDataWrapper.AddParameter("@AT_FAULT",objPriorLossInfo.AT_FAULT);
				objDataWrapper.AddParameter("@CHARGEABLE",objPriorLossInfo.CHARGEABLE);
                
				objDataWrapper.AddParameter("@LOSS_LOCATION",ReplaceInvalidCharecter(objPriorLossInfo.LOSS_LOCATION));
				objDataWrapper.AddParameter("@CAUSE_OF_LOSS",ReplaceInvalidCharecter(objPriorLossInfo.CAUSE_OF_LOSS));
				objDataWrapper.AddParameter("@POLICY_NUM",ReplaceInvalidCharecter(objPriorLossInfo.POLICY_NUM));
				objDataWrapper.AddParameter("@LOSS_CARRIER",ReplaceInvalidCharecter(objPriorLossInfo.LOSS_CARRIER));
				objDataWrapper.AddParameter("@OTHER_DESC",ReplaceInvalidCharecter(objPriorLossInfo.OTHER_DESC));
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@LOSS_ID",objPriorLossInfo.LOSS_ID,SqlDbType.Int,ParameterDirection.Output);

                int returnResult = 0;
                if(TransactionLogRequired)
                {
                    objPriorLossInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/PriorLoss/AddPriorLoss.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objPriorLossInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    
					objTransactionInfo.TRANS_TYPE_ID	=	1;

					//objTransactionInfo.APP_ID = objPriorLossInfo.APP_ID;
					//objTransactionInfo.APP_VERSION_ID = objPriorLossInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objPriorLossInfo.CUSTOMER_ID;
                    objTransactionInfo.RECORDED_BY		=	objPriorLossInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC		=	"New Prior Loss is added";
                    objTransactionInfo.CHANGE_XML		=	strTranXML;
                    //Executing the query
                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                }
                else
                {
                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                int LOSS_ID = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();

				//UT Tier
				if(objPriorLossInfo.LOB == "2")
				{
					ClsUnderwritingTier objTier = new ClsUnderwritingTier();
					objTier.UpdateUTierPriorInfo(objPriorLossInfo.CUSTOMER_ID,objDataWrapper);
				}

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (LOSS_ID == -1)
                {
                    return -1;
                }
                else
                {
                    objPriorLossInfo.LOSS_ID = LOSS_ID;
                    return LOSS_ID;
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
		#region Add Home Loss(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objPriorLossInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsPriorLossInfo_Home objPriorLossInfoHome,ClsPriorLossInfo objPriorLossInfo)
		{
			string		strStoredProc	=	"Proc_InsertHome_Loss";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objPriorLossInfoHome.CUSTOMER_ID);                
				//objDataWrapper.AddParameter("@LOSS_ID",objPriorLossInfoHome.LOSS_ID);
				objDataWrapper.AddParameter("@LOCATION_ID",objPriorLossInfoHome.LOCATION_ID);
				objDataWrapper.AddParameter("@LOSS_ADD1",objPriorLossInfoHome.LOSS_ADD1);
				objDataWrapper.AddParameter("@LOSS_ADD2",objPriorLossInfoHome.LOSS_ADD2);
				objDataWrapper.AddParameter("@LOSS_CITY",objPriorLossInfoHome.LOSS_CITY);
				objDataWrapper.AddParameter("@LOSS_STATE",objPriorLossInfoHome.LOSS_STATE);
				objDataWrapper.AddParameter("@LOSS_ZIP",objPriorLossInfoHome.LOSS_ZIP );
				objDataWrapper.AddParameter("@CURRENT_ADD1",objPriorLossInfoHome.CURRENT_ADD1);
				objDataWrapper.AddParameter("@CURRENT_ADD2",objPriorLossInfoHome.CURRENT_ADD2);
				objDataWrapper.AddParameter("@CURRENT_CITY",objPriorLossInfoHome.CURRENT_CITY);
				objDataWrapper.AddParameter("@CURRENT_STATE",objPriorLossInfoHome.CURRENT_STATE);
				objDataWrapper.AddParameter("@CURRENT_ZIP",objPriorLossInfoHome.CURRENT_ZIP);
				objDataWrapper.AddParameter("@POLICY_TYPE",objPriorLossInfoHome.POLICY_TYPE);
				objDataWrapper.AddParameter("@POLICY_NUMBER",objPriorLossInfoHome.POLICY_NUMBER);
				objDataWrapper.AddParameter("@WATERBACKUP_SUMPPUMP_LOSS",objPriorLossInfoHome.WATERBACKUP_SUMPPUMP_LOSS);  //Added by Charles on 30-Nov-09 for Itrack 6647
				objDataWrapper.AddParameter("@WEATHER_RELATED_LOSS",objPriorLossInfoHome.WEATHER_RELATED_LOSS);  //Added for Itrack 6640 on 9 Dec 09

				//SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@PRIOR_LOSS_ID",objPriorLossInfo.LOSS_ID,SqlDbType.Int,ParameterDirection.Output);
				int loss_id =0;
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objPriorLossInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/PriorLoss/AddPriorLoss.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objPriorLossInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = objPriorLossInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objPriorLossInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New Prior Loss Location is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					if (objPriorLossInfo!=null)
					{
						 loss_id=this.Add(objPriorLossInfo); 
						if (loss_id>0)
						{
							objDataWrapper.AddParameter("@LOSS_ID",loss_id);
							returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						}
					}
					else
					{
						objDataWrapper.AddParameter("@LOSS_ID",objPriorLossInfoHome.LOSS_ID);	
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
				}
				else
				{
					if (objPriorLossInfo!=null)
					{
						 loss_id=this.Add(objPriorLossInfo); 
						if (loss_id>0)
						{
							objDataWrapper.AddParameter("@LOSS_ID",loss_id);
							returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
						}
					}
					else
					{
						objDataWrapper.AddParameter("@LOSS_ID",objPriorLossInfoHome.LOSS_ID);
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					}
				}
				//int LOSS_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (objPriorLossInfo!=null)
				{	
					if (loss_id == -1)
					{
						return -1;
					}
					else
					{
						objPriorLossInfo.LOSS_ID = loss_id;
						return loss_id;
					}
				}
				else
				{
					if (returnResult == -1)
					{
						return -1;
					}
					else
					{
						//objPriorLossInfo.LOSS_ID = loss_id;
						return returnResult;
					}
				}

			}
		catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES); 
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}


		//Acord Prior Loss Info
		public int SavePriorLossHome(Cms.Model.Application.PriorLoss.ClsPriorLossInfo_Home objPriorLossInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_InsertHome_Loss_ACORD";
			DateTime	RecordDate		=	DateTime.Now;
					
			objDataWrapper.AddParameter("@CUSTOMER_ID",objPriorLossInfo.CUSTOMER_ID);	
			objDataWrapper.AddParameter("@WATERBACKUP_SUMPPUMP_LOSS",objPriorLossInfo.WATERBACKUP_SUMPPUMP_LOSS);
			//objDataWrapper.AddParameter("@WEATHER_RELATED_LOSS",objPriorLossInfo.WEATHER_RELATED_LOSS);  //Added for Itrack 6647 on 9 Dec 09
			int returnResult = 0;
			returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
			objDataWrapper.ClearParameteres();

			return 1;
			
		}
		#endregion


        #region Update method
        /// <summary>
        /// Update method that recieves Model object to save.
        /// </summary>
        /// <param name="objOldPriorLossInfo">Model object having old information</param>
        /// <param name="objPriorLossInfo">Model object having new information(form control's value)</param>
        /// <returns>No. of rows updated (1 or 0)</returns>
        public int Update(ClsPriorLossInfo objOldPriorLossInfo,ClsPriorLossInfo objPriorLossInfo)
        {
            string strTranXML;
            string		strStoredProc	=	"Proc_UpdateAPP_PRIOR_LOSS_INFO";
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            int returnResult = 0;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure ,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
            try 
            {

                objDataWrapper.AddParameter("@CUSTOMER_ID",objPriorLossInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@LOSS_ID",objPriorLossInfo.LOSS_ID);
                
                if(objPriorLossInfo.OCCURENCE_DATE.Year != 1900)
                    objDataWrapper.AddParameter("@OCCURENCE_DATE",objPriorLossInfo.OCCURENCE_DATE);
                else
                    objDataWrapper.AddParameter("@OCCURENCE_DATE",System.DBNull.Value);

               /* if(objPriorLossInfo.CLAIM_DATE.Year != 1900)
                    objDataWrapper.AddParameter("@CLAIM_DATE",objPriorLossInfo.CLAIM_DATE);
                else
                    objDataWrapper.AddParameter("@CLAIM_DATE",System.DBNull.Value);*/
				  objDataWrapper.AddParameter("@CLAIM_DATE",null);

                objDataWrapper.AddParameter("@LOB",objPriorLossInfo.LOB);
                objDataWrapper.AddParameter("@LOSS_TYPE",objPriorLossInfo.LOSS_TYPE);
				if(objPriorLossInfo.AMOUNT_PAID==0)
				{
					objDataWrapper.AddParameter("@AMOUNT_PAID",null);
				}
				else
				{
					objDataWrapper.AddParameter("@AMOUNT_PAID",objPriorLossInfo.AMOUNT_PAID);
				}
			/*	if(objPriorLossInfo.AMOUNT_RESERVED==0)
				{
					objDataWrapper.AddParameter("@AMOUNT_RESERVED",null);
				}
				else
				{
					objDataWrapper.AddParameter("@AMOUNT_RESERVED",objPriorLossInfo.AMOUNT_RESERVED);
				}	*/
				objDataWrapper.AddParameter("@AMOUNT_RESERVED",null);
                objDataWrapper.AddParameter("@CLAIM_STATUS",objPriorLossInfo.CLAIM_STATUS);
              //  objDataWrapper.AddParameter("@LOSS_DESC",ReplaceInvalidCharecter(objPriorLossInfo.LOSS_DESC));
				objDataWrapper.AddParameter("@LOSS_DESC",null);
                objDataWrapper.AddParameter("@REMARKS",ReplaceInvalidCharecter(objPriorLossInfo.REMARKS));
              //objDataWrapper.AddParameter("@MOD",ReplaceInvalidCharecter(objPriorLossInfo.MOD));
				objDataWrapper.AddParameter("@MOD",null);
               //objDataWrapper.AddParameter("@LOSS_RUN",objPriorLossInfo.LOSS_RUN);
				objDataWrapper.AddParameter("@LOSS_RUN",null);
            //   objDataWrapper.AddParameter("@CAT_NO",ReplaceInvalidCharecter(objPriorLossInfo.CAT_NO));
				 objDataWrapper.AddParameter("@CAT_NO",null);
                objDataWrapper.AddParameter("@MODIFIED_BY",objPriorLossInfo.MODIFIED_BY);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objPriorLossInfo.LAST_UPDATED_DATETIME);

				objDataWrapper.AddParameter("@APLUS_REPORT_ORDERED",objPriorLossInfo.APLUS_REPORT_ORDERED);
				objDataWrapper.AddParameter("@NAME_MATCH",objPriorLossInfo.NAME_MATCH);//Done for Itrack Issue 6723 on 27 Nov 09
				objDataWrapper.AddParameter("@DRIVER_ID",objPriorLossInfo.DRIVER_ID);
				objDataWrapper.AddParameter("@DRIVER_NAME",objPriorLossInfo.DRIVER_NAME);
				objDataWrapper.AddParameter("@RELATIONSHIP",objPriorLossInfo.RELATIONSHIP);
				objDataWrapper.AddParameter("@CLAIMS_TYPE",objPriorLossInfo.CLAIMS_TYPE);
				objDataWrapper.AddParameter("@AT_FAULT",objPriorLossInfo.AT_FAULT);
				objDataWrapper.AddParameter("@CHARGEABLE",objPriorLossInfo.CHARGEABLE);
                                    
				objDataWrapper.AddParameter("@LOSS_LOCATION",ReplaceInvalidCharecter(objPriorLossInfo.LOSS_LOCATION));
				objDataWrapper.AddParameter("@CAUSE_OF_LOSS",ReplaceInvalidCharecter(objPriorLossInfo.CAUSE_OF_LOSS));
				objDataWrapper.AddParameter("@POLICY_NUM",ReplaceInvalidCharecter(objPriorLossInfo.POLICY_NUM));
				objDataWrapper.AddParameter("@LOSS_CARRIER",ReplaceInvalidCharecter(objPriorLossInfo.LOSS_CARRIER));
				objDataWrapper.AddParameter("@OTHER_DESC",ReplaceInvalidCharecter(objPriorLossInfo.OTHER_DESC));		
               
                if(TransactionLogRequired)
                {
                    objPriorLossInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/PriorLoss/AddPriorLoss.aspx.resx");
                    string strUpdate = objBuilder.GetUpdateSQL(objOldPriorLossInfo,objPriorLossInfo,out strTranXML);
                    
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//objTransactionInfo.APP_ID = objPriorLossInfo.APP_ID;
					//objTransactionInfo.APP_VERSION_ID = objPriorLossInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objPriorLossInfo.CUSTOMER_ID;
                    objTransactionInfo.RECORDED_BY		=	objPriorLossInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC		=	"Prior Loss is modified";
                    objTransactionInfo.CHANGE_XML		=	strTranXML;
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                objDataWrapper.ClearParameteres();

				//UT Tier
				if(objPriorLossInfo.LOB == "2")
				{
					ClsUnderwritingTier objTier = new ClsUnderwritingTier();
					objTier.UpdateUTierPriorInfo(objPriorLossInfo.CUSTOMER_ID,objDataWrapper);
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
		//Function added by Charles on 30-Nov-09 for Itrack 6647
		public int Update(ClsPriorLossInfo_Home objOldPriorLossInfo_Home,ClsPriorLossInfo_Home objPriorLossInfo_Home)
		{
			string strTranXML;
			string		strStoredProc	=	"Proc_UpdateHome_Loss";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			int returnResult = 0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure ,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objPriorLossInfo_Home.CUSTOMER_ID);                
				objDataWrapper.AddParameter("@LOSS_ID",objPriorLossInfo_Home.LOSS_ID);
				objDataWrapper.AddParameter("@LOCATION_ID",objPriorLossInfo_Home.LOCATION_ID);
				objDataWrapper.AddParameter("@LOSS_ADD1",objPriorLossInfo_Home.LOSS_ADD1);
				objDataWrapper.AddParameter("@LOSS_ADD2",objPriorLossInfo_Home.LOSS_ADD2);
				objDataWrapper.AddParameter("@LOSS_CITY",objPriorLossInfo_Home.LOSS_CITY);
				objDataWrapper.AddParameter("@LOSS_STATE",objPriorLossInfo_Home.LOSS_STATE);
				objDataWrapper.AddParameter("@LOSS_ZIP",objPriorLossInfo_Home.LOSS_ZIP );
				objDataWrapper.AddParameter("@CURRENT_ADD1",objPriorLossInfo_Home.CURRENT_ADD1);
				objDataWrapper.AddParameter("@CURRENT_ADD2",objPriorLossInfo_Home.CURRENT_ADD2);
				objDataWrapper.AddParameter("@CURRENT_CITY",objPriorLossInfo_Home.CURRENT_CITY);
				objDataWrapper.AddParameter("@CURRENT_STATE",objPriorLossInfo_Home.CURRENT_STATE);
				objDataWrapper.AddParameter("@CURRENT_ZIP",objPriorLossInfo_Home.CURRENT_ZIP);
				objDataWrapper.AddParameter("@POLICY_TYPE",objPriorLossInfo_Home.POLICY_TYPE);
				objDataWrapper.AddParameter("@POLICY_NUMBER",objPriorLossInfo_Home.POLICY_NUMBER);
				objDataWrapper.AddParameter("@WATERBACKUP_SUMPPUMP_LOSS",objPriorLossInfo_Home.WATERBACKUP_SUMPPUMP_LOSS);
                objDataWrapper.AddParameter("@WEATHER_RELATED_LOSS",objPriorLossInfo_Home.WEATHER_RELATED_LOSS);  //Added for Itrack 6647 on 9 Dec 09

				if(TransactionLogRequired)
				{
					objPriorLossInfo_Home.TransactLabel = ClsCommon.MapTransactionLabel("/Application/PriorLoss/AddPriorLoss.aspx.resx");
					string strUpdate = objBuilder.GetUpdateSQL(objOldPriorLossInfo_Home,objPriorLossInfo_Home,out strTranXML);
                    
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID		=	objPriorLossInfo_Home.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objPriorLossInfo_Home.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Home Prior Loss is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
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

		public DataSet GetExistingPriorLoss(string sqlSelect)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);
			DataSet PriorLossds = objDataWrapper.ExecuteDataSet(sqlSelect);
			return PriorLossds;
		}

        #region FETCHING DATA
        public DataSet FetchData(int LossId,int customer_Id)
        {
            string		strStoredProc	=	"Proc_FetchPriorLossInfo";
            DataSet dsCount=null;
           			
            //SqlParameter [] sparam=new SqlParameter[1];
            try
            {
                /*sparam[0]=new SqlParameter("@LOSS_ID",SqlDbType.Int);
                sparam[0].Value=LossId; 
				
                dsCount=DataWrapper.ExecuteDataset(ConnStr,strStoredProc,sparam);           
                 */

                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@LOSS_ID",LossId,SqlDbType.Int);
                
                objDataWrapper.AddParameter("@CUSTOMER_ID",customer_Id,SqlDbType.Int);
                objDataWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID, SqlDbType.Int);

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
		#region Delete

		//Modified by Asfa (29-May-2008) - iTrack #4240
		public int Delete(ClsPriorLossInfo objPriorLossInfo)
		{
			string strStoredProc = "Proc_DeletePriorLoss";
            int result=-1;
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	

			objWrapper.AddParameter("@CUSTOMER_ID",objPriorLossInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@LOSS_ID",objPriorLossInfo.LOSS_ID);
			SqlParameter sqlParamRetVal = (SqlParameter) objWrapper.AddParameter("@RETVAL",SqlDbType.Int,ParameterDirection.ReturnValue);
			try
			{	
				if(TransactionLogRequired)
				{
					objPriorLossInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/PriorLoss/AddPriorLoss.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objPriorLossInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID		= objPriorLossInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objPriorLossInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Prior Loss is deleted";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					result	= objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					result	= objWrapper.ExecuteNonQuery(strStoredProc);
				}
				
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}

			//UT Tier
			if(objPriorLossInfo.LOB == "2")
			{
				ClsUnderwritingTier objTier = new ClsUnderwritingTier();
				objTier.UpdateUTierPriorInfo(objPriorLossInfo.CUSTOMER_ID,objWrapper);
			}
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);	

			return result;
		}
		#endregion
		#region fetching prior loss from IIX and Inserting in prior Loss Table added by Pravesh
		
		public string InsertToPriorLossTable(DataSet objDSDriver, string retAutoLoss, string driver_id, string driver_name,int userID,string calledFrom,int intNameMatch)//Done for Itrack Issue 6723 on 7 Dec 09
		{
			ClsPriorLossInfo objPriorLossInfo;
			objPriorLossInfo                            = new ClsPriorLossInfo();
			string insurername="";
			string policynum="";
			string polTyp="";
			string oc_Date="";
			string clmTyp="";
			string lossTyp="";
			string clmStat="";
			string lossAmt="";
			string driv_rel="",strAtFault="";
			string DriverId="0",lblMessage="";
			int IntCUSTOMER_ID,IntAPP_POL_ID,IntVERSION_ID;
			if(calledFrom=="APP")
			{
				IntCUSTOMER_ID		=int.Parse(objDSDriver.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				IntAPP_POL_ID		=int.Parse(objDSDriver.Tables[0].Rows[0]["APP_ID"].ToString());
				IntVERSION_ID		=int.Parse(objDSDriver.Tables[0].Rows[0]["APP_VERSION_ID"].ToString());
			}
			else
			{
				IntCUSTOMER_ID		=int.Parse(objDSDriver.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				IntAPP_POL_ID		=int.Parse(objDSDriver.Tables[0].Rows[0]["POLICY_ID"].ToString());
				IntVERSION_ID		=int.Parse(objDSDriver.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
			}
			try
			{
				retAutoLoss = retAutoLoss.Replace("&","and");
				retAutoLoss = retAutoLoss.Replace(" xmlns=\"Common\"","");

				XmlDocument lossxml = new XmlDocument();
				lossxml.LoadXml(retAutoLoss);

				XmlNodeList lossnodes = lossxml.SelectNodes("ISO/PassportSvcRs/PassportInqRs/Match/Claim");
				foreach(XmlNode lossnode in lossnodes)
				{

					strAtFault=driv_rel=polTyp=policynum=oc_Date=clmTyp=lossTyp=lossAmt="";
					objPriorLossInfo.REMARKS= "";
					try { insurername = lossnode.SelectSingleNode("Insurer/InsurerName").InnerText; } catch(Exception){}
					objPriorLossInfo.LOSS_CARRIER = insurername;
					XmlNodeList PartiesNodeList=lossnode.SelectNodes("Party");
					foreach(XmlNode PartyNode in PartiesNodeList)
					{
						string MiscPartyRoleCd="";
						string ClaimPartyName="",ClaimPartyBirthDate="",ClaimPartyDLNumber="";

						try { MiscPartyRoleCd = PartyNode.SelectSingleNode("MiscPartyRoleCd").InnerText; }	catch(Exception){}
						try { ClaimPartyName = PartyNode.SelectSingleNode("Individual/Name/GivenName").InnerText; }	catch(Exception){}
						try { ClaimPartyName = ClaimPartyName + " " + PartyNode.SelectSingleNode("Individual/Name/Surname").InnerText; } catch(Exception){}
						try { ClaimPartyBirthDate = PartyNode.SelectSingleNode("Individual/BirthDt").InnerText; } catch(Exception){}
						try { ClaimPartyDLNumber = PartyNode.SelectSingleNode("Individual/DriversLicense/DriversLicenseNumber").InnerText; } catch(Exception){}
						if(MiscPartyRoleCd!=ClsCommon.InsuredDriver) continue;
						objPriorLossInfo.REMARKS= "" + objPriorLossInfo.REMARKS + " Claim Party :" + ClaimPartyName + "-{" + ClaimPartyBirthDate + "-" +  ClaimPartyDLNumber + "-PartyRoleCd=" + MiscPartyRoleCd + "};";
						for (int i=0;i< objDSDriver.Tables[0].Rows.Count;i++)
						{
							string DriverName=objDSDriver.Tables[0].Rows[i]["DRIVER_FNAME"].ToString().Trim();
							if (objDSDriver.Tables[0].Rows[i]["DRIVER_MNAME"].ToString().Trim()!="")
								DriverName = DriverName + " " + objDSDriver.Tables[0].Rows[i]["DRIVER_MNAME"].ToString().Trim();
							if (objDSDriver.Tables[0].Rows[i]["DRIVER_LNAME"].ToString().Trim()!="")
								DriverName = DriverName + " " + objDSDriver.Tables[0].Rows[i]["DRIVER_LNAME"].ToString();
							DriverId = objDSDriver.Tables[0].Rows[i]["DRIVER_ID"].ToString();
							string DriverDOB="";
							if (objDSDriver.Tables[0].Rows[i]["DRIVER_DOB"]!= null)
							{
								DriverDOB=Convert.ToDateTime(objDSDriver.Tables[0].Rows[i]["DRIVER_DOB"].ToString()).ToString("MMddyyyy");   
							}
							string DriverDLNumber=objDSDriver.Tables[0].Rows[i]["DRIVER_DRIV_LIC"].ToString();
							if(DriverDLNumber==ClaimPartyDLNumber 
								&& DriverDOB==Convert.ToDateTime(ClaimPartyBirthDate).ToString("MMddyyyy")
								&& DriverName.ToUpper()==ClaimPartyName.Trim().ToUpper()
								)
							{
								driver_id=DriverId;
								driver_name=DriverName;	
							}
						}
					}
					objPriorLossInfo.LOSS_CARRIER = insurername;

					try { policynum = lossnode.SelectSingleNode("Policy/PolicyNumber").InnerText;}	catch(Exception){}
					objPriorLossInfo.POLICY_NUM = policynum;
					try { polTyp = lossnode.SelectSingleNode("Policy/PolicyTypeCd").InnerText;}	catch(Exception){}

					if(polTyp == "PAPP" || polTyp == "CAPP")
						objPriorLossInfo.LOB = "2";
					else if(polTyp == "CYPP")
						objPriorLossInfo.LOB = "3";
					else if(polTyp == "MPHH")
						objPriorLossInfo.LOB = "-1";
				
					//Code for retreiving the forms valus will go here		
					try { oc_Date    = lossnode.SelectSingleNode("Loss/LossDt").InnerText;}	catch(Exception){}
					if(oc_Date!="")
						objPriorLossInfo.OCCURENCE_DATE             = Convert.ToDateTime(oc_Date);
					else
						objPriorLossInfo.OCCURENCE_DATE             = Convert.ToDateTime("1/1/1900");
					/* Commented by pravesh on 3 sep 09 as create record in prior loss table for each Payment node 
					try { clmTyp    = lossnode.SelectSingleNode("Payment/CoverageCd").InnerText;} catch(Exception){}

					objPriorLossInfo.CLAIMS_TYPE =  Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupUniqueId("CMTYPS", clmTyp);

					try { lossTyp    = lossnode.SelectSingleNode("Payment/CoverageCd").InnerText;}	catch(Exception){}
					objPriorLossInfo.LOSS_TYPE =  Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupUniqueId("CLOSS", lossTyp);

					try { clmStat    = lossnode.SelectSingleNode("Payment/ClaimStatusCd").InnerText;}	catch(Exception){}
					objPriorLossInfo.CLAIM_STATUS =  Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupUniqueId("CLAIM", clmStat).ToString();

					try { lossAmt = lossnode.SelectSingleNode("Payment/LossPaymentAmt").InnerText;}	catch(Exception){}
					objPriorLossInfo.AMOUNT_PAID                = Double.Parse(lossAmt);
					*/
					try { driv_rel = lossnode.SelectSingleNode("DriverRelationshipToOwnerCd").InnerText;}	catch(Exception){}
				
					if(driv_rel == "PH" || driv_rel == "P")
						objPriorLossInfo.RELATIONSHIP = 3468;
					else if(driv_rel == "SP" || driv_rel == "S")
						objPriorLossInfo.RELATIONSHIP = 3472;
					else if(driv_rel == "CH" || driv_rel == "C")
						objPriorLossInfo.RELATIONSHIP = 3467;
					else
						objPriorLossInfo.RELATIONSHIP = 3470;
					try { strAtFault = lossnode.SelectSingleNode("OperatorAtFaultInd").InnerText;}	catch(Exception){}
					if (strAtFault=="Y") // as per Margot Mail (itrack 5756)
					{
						objPriorLossInfo.AT_FAULT=10963;
						objPriorLossInfo.CHARGEABLE=11924;
					}
					else if (strAtFault=="N")
					{
						objPriorLossInfo.AT_FAULT=10964;
						objPriorLossInfo.CHARGEABLE=11923;
					}
					try { objPriorLossInfo.LOSS_DESC     = lossnode.SelectSingleNode("Loss/LossDesc").InnerText;}	catch(Exception){}
					//objPriorLossInfo.REMARKS                    = lossnode.SelectSingleNode("Cause-Of-Loss-Text").InnerText;//txtDESC_OF_LOSS_AND_REMARKS.Text;
					// objPriorLossInfo.MOD                        = "";
					// objPriorLossInfo.LOSS_RUN                   = "";
					objPriorLossInfo.CUSTOMER_ID                = IntCUSTOMER_ID ;
					objPriorLossInfo.APLUS_REPORT_ORDERED = 1;
					objPriorLossInfo.DRIVER_ID = int.Parse(driver_id);
					if (calledFrom=="APP")
						objPriorLossInfo.DRIVER_NAME = IntCUSTOMER_ID.ToString() + "^" + IntAPP_POL_ID.ToString() + "^" + IntVERSION_ID.ToString() + "^" + driver_id + "^" + "APP";
					else
						objPriorLossInfo.DRIVER_NAME = IntCUSTOMER_ID.ToString() + "^" + IntAPP_POL_ID.ToString() + "^" + IntVERSION_ID.ToString() + "^" + driver_id + "^" + "POL";
					objPriorLossInfo.CREATED_BY = userID;
					objPriorLossInfo.CREATED_DATETIME = DateTime.Now;
					objPriorLossInfo.MODIFIED_BY  = userID;
					objPriorLossInfo.LAST_UPDATED_DATETIME  = DateTime.Now;
					objPriorLossInfo.IS_ACTIVE ="Y";
					//Done for Itrack Issue 6723 on 7 Dec 09
					if(intNameMatch > 0)
						objPriorLossInfo.NAME_MATCH = 2;
					else
						objPriorLossInfo.NAME_MATCH = 1;
					// added by pravesh on 3 sep 09 to create record in prior loss table for each Payment node in Response as discussed with Ashish (itrack 6341)
					XmlNodeList PaymentNodeList=lossnode.SelectNodes("Payment");
					foreach(XmlNode PaymentNode in PaymentNodeList)
					{
						objPriorLossInfo.CLAIMS_TYPE=0;objPriorLossInfo.LOSS_TYPE=0;objPriorLossInfo.CLAIM_STATUS="";objPriorLossInfo.AMOUNT_PAID =0;
						clmTyp=lossTyp=clmStat="";lossAmt="0";
						try { clmTyp    = PaymentNode.SelectSingleNode("CoverageCd").InnerText;} catch(Exception){}
						objPriorLossInfo.CLAIMS_TYPE =  Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupUniqueId("CMTYPS", clmTyp);
						try { lossTyp    = PaymentNode.SelectSingleNode("LossTypeCd").InnerText;} catch(Exception){}
						objPriorLossInfo.LOSS_TYPE =  Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupUniqueId("CLOSS", lossTyp);
						try { clmStat    = PaymentNode.SelectSingleNode("ClaimStatusCd").InnerText;} catch(Exception){}
						objPriorLossInfo.CLAIM_STATUS =  Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupUniqueId("CLAIM", clmStat).ToString();
						try { lossAmt = PaymentNode.SelectSingleNode("LossPaymentAmt").InnerText;} catch(Exception){}
						objPriorLossInfo.AMOUNT_PAID                = Double.Parse(lossAmt);

						//Calling the add method of business layer class
						#region Check Update
						//string sqlSelPriorLoss = "SELECT * FROM APP_PRIOR_LOSS_INFO WHERE OCCURENCE_DATE = '" + oc_Date + "' AND LOB = '" + objPriorLossInfo.LOB.ToString() + "' AND CUSTOMER_ID = " + IntCUSTOMER_ID.ToString()  + " AND DRIVER_ID = " + driver_id;
						string sqlSelPriorLoss = "SELECT * FROM APP_PRIOR_LOSS_INFO WHERE LOSS_TYPE=" + objPriorLossInfo.LOSS_TYPE.ToString() + " AND OCCURENCE_DATE = '" + oc_Date + "' AND LOB = '" + objPriorLossInfo.LOB.ToString() + "' AND CUSTOMER_ID = " + IntCUSTOMER_ID.ToString()  + " AND DRIVER_ID = " + driver_id;
						DataSet PriorLossds = GetExistingPriorLoss(sqlSelPriorLoss);
						#endregion
						if(PriorLossds.Tables[0].Rows.Count == 0)
						{	
							int intRetVal = this.Add(objPriorLossInfo);
							//lossadded = 1;
							lblMessage += "Prior Loss Record added for: " + driver_name + "<br>";
						
						}
					}
				}
				return lblMessage;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		#endregion
		//Added by Mohit Agarwal 31-Oct-07
		public void SetLossOrdered(int customerID, int appID, int appVersionID, int LocID, string CalledFrom,string ReportStatus)
		{
			
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objDataWrapper.AddParameter("@APP_ID",appID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
				objDataWrapper.AddParameter("@LOCATION_ID",LocID);
				objDataWrapper.AddParameter("@CALLED_FROM",CalledFrom);
				objDataWrapper.AddParameter("@REPORT_STATUS",ReportStatus);

				objDataWrapper.ExecuteNonQuery("Proc_SetLossReportOrder");
			}
			catch
			{
				return;
			}
			finally
			{}
		}

    }
}


