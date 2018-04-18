/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	12/11/2006
<End Date				: -	
<Description			: - Business Layer class for Installment Info screen
<Review Date			: - 
<Reviewed By			: - 	
Modification History
********************************************************************************************/


using System;
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data;
using System.Configuration;
using Cms.Model.Application;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlCommon;
using Cms.Model;
using Cms.Model.Policy;
using System.Collections;
using Cms.Model.Account;

namespace  Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsInstallmentInfo.
	/// </summary>
	public class ClsInstallmentInfo :Cms.BusinessLayer.BlCommon.ClsCommon
	{
		public ClsInstallmentInfo()
		{
			
		}

		private bool boolTransactionRequired			=	true;
		public bool TransactionRequired
		{
			get
			{
				return boolTransactionRequired;
			}set
			{
				boolTransactionRequired=value;
			}
		}

		public DataSet GetPolicyInstallmentInfo(int CustomerID, int PolicyID, int PolicyVersionID)
		{
			try
			{
				string	strStoredProc =	"Proc_GetPolicyInstallmentPlanInfo";			
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
				objWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objWrapper.AddParameter("@POLICY_ID",PolicyID);
				objWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);			
				if(ds!=null)
					return ds;
				else
					return null;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
			}
		}


		public DataSet GetInstallmentDetailBreakup(int CustomerID, int PolicyID,int installmentNo,int currentTerm)
		{
			try
			{
				string	strStoredProc =	"Proc_GetInstallmentDetailBreakup";			
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
				objWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objWrapper.AddParameter("@POLICY_ID",PolicyID);
				objWrapper.AddParameter("@INSTALLMENT_NO",installmentNo);
				objWrapper.AddParameter("@CURRENT_TERM",currentTerm);				
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);			
				if(ds!=null)
					return ds;
				else
					return null;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
			}
		}
		
		//GetAppInstallmentInfo
		public DataSet GetAppInstallmentInfo(int CustomerID, int AppID, int AppVersionID)
		{
			try
			{
				string	strStoredProc =	"Proc_GetAppInstallmentPlanInfo";			
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
				objWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objWrapper.AddParameter("@APP_ID",AppID);
				objWrapper.AddParameter("@APP_VERSION_ID",AppVersionID);
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);			
				if(ds!=null)
					return ds;
				else
					return null;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
			}
		}
		//Modified GetApplicableInstallmentPlans(int intPolicyTerm)
		public static DataSet GetApplicableInstallmentPlans(int intPolicyTerm,int custId,int appId,int appversionId,string strCalledfrom)
		{
			try
			{
				string	strStoredProc =	"ProcGetAppilcableInstallPlans";			
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
				objWrapper.AddParameter("@POLICY_TERM",intPolicyTerm );
				objWrapper.AddParameter("@CUSTOMER_ID",custId  );
				objWrapper.AddParameter("@APP_ID",appId  );
				objWrapper.AddParameter("@APP_VERSION_ID",appversionId  );
				objWrapper.AddParameter("@CALLED_FROM",strCalledfrom  );
                objWrapper.AddParameter("@LANG_ID", BL_LANG_ID);
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);			
				if(ds!=null)
					return ds;
				else
					return null;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
			}
		}
		//Modified GetApplicableInstallmentPlans(),Called fro APP and POL ,with POLICY_TERM as Null
		public static DataSet GetApplicableInstallmentPlans(int custId,int appId,int appversionId,string strCalledfrom)
		{
			try
			{
				string	strStoredProc =	"ProcGetAppilcableInstallPlans";			
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
				objWrapper.AddParameter("@POLICY_TERM",System.DBNull.Value  );
				objWrapper.AddParameter("@CUSTOMER_ID",custId  );
				objWrapper.AddParameter("@APP_ID",appId  );
				objWrapper.AddParameter("@APP_VERSION_ID",appversionId  );
				objWrapper.AddParameter("@CALLED_FROM",strCalledfrom );
                objWrapper.AddParameter("@LANG_ID", BL_LANG_ID);
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);			
				if(ds!=null)
					return ds;
				else
					return null;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
			}
		}
		//Added by Charles on 14-Sep-09 for APP/POL Optimization
		public static DataSet GetApplicableInstallmentPlans(int intPolicyTerm)
		{
			try
			{
				string	strStoredProc =	"ProcGetAppilcableInstallPlans";			
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
				objWrapper.AddParameter("@POLICY_TERM",System.DBNull.Value);
				objWrapper.AddParameter("@CUSTOMER_ID",System.DBNull.Value  );
				objWrapper.AddParameter("@APP_ID",System.DBNull.Value  );
				objWrapper.AddParameter("@APP_VERSION_ID",System.DBNull.Value );
                objWrapper.AddParameter("@LANG_ID", BL_LANG_ID);
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);			
				if(ds!=null)
					return ds;
				else
					return null;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
			}
		}
		//new app
		public static DataSet GetApplicableInstallmentPlans()
		{
			try
			{
				string	strStoredProc =	"ProcGetAppilcableInstallPlans";			
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
				objWrapper.AddParameter("@POLICY_TERM",System.DBNull.Value  );
				objWrapper.AddParameter("@CUSTOMER_ID",System.DBNull.Value  );
				objWrapper.AddParameter("@APP_ID",System.DBNull.Value  );
				objWrapper.AddParameter("@APP_VERSION_ID",System.DBNull.Value );
                objWrapper.AddParameter("@LANG_ID",BL_LANG_ID);
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);			
				if(ds!=null)
					return ds;
				else
					return null;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
			}
		}
        public static DataSet GetapppolDownpayment(int CUSTID,int APP_ID,int APP_VERSIONID,string SELECT_FOR)
		{
			try
			{
				string	strStoredProc =	"ProcGetAppilcableInstallPlans";			
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
				objWrapper.AddParameter("@POLICY_TERM",System.DBNull.Value  );
                objWrapper.AddParameter("@CUSTOMER_ID", CUSTID);
                objWrapper.AddParameter("@APP_ID", APP_ID);
                objWrapper.AddParameter("@APP_VERSION_ID", APP_VERSIONID);
                objWrapper.AddParameter("@SELECT_FOR", SELECT_FOR);
                objWrapper.AddParameter("@LANG_ID", BL_LANG_ID); 
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);			
				if(ds!=null)
					return ds;
				else
					return null;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
			}
		}

        
		
		public int ReadjustInstallmentPlan(ClsEFTInfo objEFTInfo,int intCustomerID, int intPolicyID, int intPolicyVersionID,int intNewPlanID,string UserId,string strPlan,string strOldPlan)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			int returnResult = 0;
			try
			{
				string	strStoredProc =	"Proc_ChangeInstallPlan";			
				objWrapper.AddParameter("@CUSTOMER_ID",intCustomerID  );
				objWrapper.AddParameter("@POLICY_ID",intPolicyID );
				objWrapper.AddParameter("@POLICY_VERSION_ID",intPolicyVersionID );
				objWrapper.AddParameter("@NEW_PLAN_ID ",intNewPlanID );
				SqlParameter objRetParam = (SqlParameter)objWrapper.AddParameter("@RetVal",System.Data.SqlDbType.Int , ParameterDirection.ReturnValue);

				//if(TransactionLog)
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					string strTranXML="";
						
					objEFTInfo.TransactLabel = BlCommon.ClsCommon.MapTransactionLabel("/application/aspx/InstallmentInfo.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					strTranXML = objBuilder.GetTransactionLogXML(objEFTInfo);
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1741", "");//"Select Billing Plan Has Been Changed in Billing Info.";
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objEFTInfo.CREATED_BY;
					objTransactionInfo.CLIENT_ID		=	objEFTInfo.CUSTOMER_ID;
					objTransactionInfo.POLICY_ID			=	objEFTInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objEFTInfo.POLICY_VERSION_ID;
					if(strOldPlan!="")
						objTransactionInfo.CUSTOM_INFO		=	";Billing plan has been changed from " + strOldPlan + " to " + strPlan;
					else
						objTransactionInfo.CUSTOM_INFO		=	";Select billing plan  = " + strPlan;

					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					returnResult = Convert.ToInt32(objRetParam.Value); 

					objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			finally
			{
				if(objWrapper != null) objWrapper.Dispose();
			}
			return returnResult;

		}

		public int ReadjustInstallmentPlanApp(ClsEFTInfo objEFTInfo,int intNewPlanID, string UserId,string strPlan,string strOldPlan)
		{
			int returnResult = 0;
			try
			{
				string	strStoredProc =	"Proc_ChangeInstallPlanAPP";			
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
				objWrapper.AddParameter("@CUSTOMER_ID",objEFTInfo.CUSTOMER_ID  );
				objWrapper.AddParameter("@APP_ID",objEFTInfo.APP_ID );
				objWrapper.AddParameter("@APP_VERSION_ID",objEFTInfo.APP_VERSION_ID );
				objWrapper.AddParameter("@NEW_PLAN_ID ",intNewPlanID );
				SqlParameter objRetParam = (SqlParameter)objWrapper.AddParameter("@RetVal",System.Data.SqlDbType.Int , ParameterDirection.ReturnValue);
				//if(TransactionLog)
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					string strTranXML="";
					
					objEFTInfo.TransactLabel = BlCommon.ClsCommon.MapTransactionLabel("/application/aspx/InstallmentInfo.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					strTranXML = objBuilder.GetTransactionLogXML(objEFTInfo);
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1741", "");// "Select Billing Plan Has Been Changed in Billing Info.";
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objEFTInfo.CREATED_BY;
					objTransactionInfo.CLIENT_ID		=	objEFTInfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID			=	objEFTInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objEFTInfo.APP_VERSION_ID;
					if(strOldPlan!="")
						objTransactionInfo.CUSTOM_INFO		=	";Billing plan has been changed from " + strOldPlan + " to " + strPlan;
					else
						objTransactionInfo.CUSTOM_INFO		=	";Select billing plan  = " + strPlan;
					//objTransactionInfo.CHANGE_XML		=	strTranXML;
					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					returnResult = Convert.ToInt32(objRetParam.Value); 
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			return returnResult;

		}
        public int AddPolBillinInfo(ClsBillingInformationInfo objBillingInformationInfo, string XmlFullFilePath)
        {
            int returnValue = 0;

            if (objBillingInformationInfo.RequiredTransactionLog)
            {
                objBillingInformationInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);
                returnValue = objBillingInformationInfo.AddPolBillinInfo();

            }
            return returnValue;
        }

        public int updatePolBillingInfo(ClsBillingInformationInfo objBillingInformationInfo, string XmlFullFilePath)
        {
            int returnValue = 0;

            if (objBillingInformationInfo.RequiredTransactionLog)
            {
                objBillingInformationInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);

                returnValue = objBillingInformationInfo.updatePolBillingInfo();

            }
            return returnValue;
        }

        public ClsBillingInformationInfo FetchData(Int32 BILLING_ID, string CUSTOMER_ID, string POLICY_ID, string POLICY_VERSION_ID, string LOB_ID)
        {


            DataSet ds = null;
            ClsBillingInformationInfo objBillingInformationInfo = new ClsBillingInformationInfo();

            try
            {
                ds = objBillingInformationInfo.FetchData(BILLING_ID,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,LOB_ID);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(ds, objBillingInformationInfo);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return objBillingInformationInfo;


        }

        public ClsBillingInformationInfo FetchId(string CUSTOMER_ID, string POLICY_ID, string POLICY_VERSION_ID, string LOB_ID)
        {


            DataSet ds = null;
            ClsBillingInformationInfo objBillingInformationInfo = new ClsBillingInformationInfo();

            try
            {
                ds = objBillingInformationInfo.FetchId(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, LOB_ID);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(ds, objBillingInformationInfo);
                }
    
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return objBillingInformationInfo;


        }

        public int DelPolBillinInfo(ClsBillingInformationInfo objBillingInformationInfo, string XmlFullFilePath)
        {
            int returnValue = 0;

            if (objBillingInformationInfo.RequiredTransactionLog)
            {
                objBillingInformationInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);
                returnValue = objBillingInformationInfo.DelPolBillinInfo();

            }
            return returnValue;
        }

        //public int ActivateDeactivatePolBillingInfo(ClsBillingInformationInfo objBillingInformationInfo, string XmlFullFilePath)
        //{
        //    int returnValue = 0;
        //    if (objBillingInformationInfo.RequiredTransactionLog)
        //    {
        //        objBillingInformationInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);

        //        if (objBillingInformationInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
        //            objBillingInformationInfo.TRANS_TYPE_ID = 314;
        //        else
        //            objBillingInformationInfo.TRANS_TYPE_ID = 315;

        //        returnValue = objBillingInformationInfo.ActivateDeactivatePolBillingInfo();
        //    }
        //    return returnValue;
        //}

        public DataTable GET_BILL_PLAN()
        {


            DataSet ds = null;
            ClsBillingInformationInfo objBillingInformationInfo = new ClsBillingInformationInfo();

            try
            {
                ds = objBillingInformationInfo.GET_BILL_PLAN();
                if (ds.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(ds, objBillingInformationInfo);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return ds.Tables[0];


        }
	}	
}


