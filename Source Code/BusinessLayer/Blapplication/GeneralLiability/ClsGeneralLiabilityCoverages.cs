/******************************************************************************************
<Author					: -  Ravindra Kumar Gupta
<Start Date				: -	 March 28, 2006
<End Date				: -	
<Description			: -  Business Logic for General Liability Coverages
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: -	Ravindra	
<Modified By			: - 04-03-2006
<Purpose				: - To Add Policy Level Functions
************************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
//using Cms.Model.Application.GeneralLiability;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
namespace Cms.BusinessLayer.BlApplication.GeneralLiability
{
	/// <summary>
	/// Summary description for ClsGeneralLiabilityCoverages.
	/// </summary>
	public class ClsGeneralLiabilityCoverages : Cms.BusinessLayer.BlApplication.clsapplication 
	{
		public ClsGeneralLiabilityCoverages()
		{
		
		}

		#region GetCoverageForGL Function
		public static DataTable GetCoverageForGL(int intCustomerId,int intAppId,int intAppVersionId,string strCoverageCode)
		{
			string	strStoredProc =	"Proc_GetCoveragesForGL";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
			objWrapper.AddParameter("@APP_ID",intAppId);
			objWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId);
			objWrapper.AddParameter("@COVERAGE_CODE",strCoverageCode);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			return ds.Tables[0];
		}
		#endregion

		#region GetPolicyCoverageForGL Function
		public static DataTable GetPolicyCoverageForGL(int intCustomerId,int intPolicyId,int intPolicyVersionId,string strCoverageCode)
		{
			string	strStoredProc =	"Proc_GetPolicyCoveragesForGL";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
			objWrapper.AddParameter("@POLICY_ID",intPolicyId );
			objWrapper.AddParameter("@POLICY_VERSION_ID",intPolicyVersionId );
			objWrapper.AddParameter("@COVERAGE_CODE",strCoverageCode);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			return ds.Tables[0];
		}
		#endregion

        //#region Add  Function To Insert Data
        //public int Add(ClsGeneralLiabilityCoveragesInfo  objInfo)
        //{
        //    string	strStoredProc =	"Proc_InsertGeneralLiabilityCoverages";
        //    string strTranXML = "";
        //    DateTime RecordDate = DateTime.Now;
        //    int intResult=0;
			
        //    //Get the tran log XML , if present
        //    if ( this.TransactionLogRequired)
        //    {
        //        objInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/GeneralLiability/GeneralLiabilityCoverages.aspx.resx");
        //        SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
        //        strTranXML = objBuilder.GetTransactionLogXML(objInfo);
        //    }

        //    DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
        //    objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
        //    objWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
        //    objWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);

        //    if(objInfo.COVERAGE_L_AMOUNT == 0)
        //        objWrapper.AddParameter("@COVERAGE_L_AMOUNT",System.DBNull.Value);
        //    else
        //        objWrapper.AddParameter("@COVERAGE_L_AMOUNT",objInfo.COVERAGE_L_AMOUNT );
			
        //    objWrapper.AddParameter("@COVERAGE_L_ID",objInfo.COVERAGE_L_ID); 
			
        //    if(objInfo.COVERAGE_L_AGGREGATE==0 )
        //        objWrapper.AddParameter("@COVERAGE_L_AGGREGATE",System.DBNull.Value);
        //    else
        //        objWrapper.AddParameter("@COVERAGE_L_AGGREGATE",objInfo.COVERAGE_L_AGGREGATE );

        //    if(objInfo.COVERAGE_O_AMOUNT == 0 )
        //        objWrapper.AddParameter("@COVERAGE_O_AMOUNT",System.DBNull.Value  );
        //    else
        //        objWrapper.AddParameter("@COVERAGE_O_AMOUNT",objInfo.COVERAGE_O_AMOUNT );
			
        //    objWrapper.AddParameter("@COVERAGE_O_ID",objInfo.COVERAGE_O_ID );
			
        //    if(objInfo.COVERAGE_O_AGGREGATE==0)
        //        objWrapper.AddParameter("@COVERAGE_O_AGGREGATE",System.DBNull.Value );
        //    else
        //        objWrapper.AddParameter("@COVERAGE_O_AGGREGATE",objInfo.COVERAGE_O_AGGREGATE);
			
        //    if(objInfo.COVERAGE_M_EACH_PERSON_AMOUNT ==0)
        //        objWrapper.AddParameter("@COVERAGE_M_EACH_PERSON_AMOUNT",System.DBNull.Value );
        //    else
        //        objWrapper.AddParameter("@COVERAGE_M_EACH_PERSON_AMOUNT",objInfo.COVERAGE_M_EACH_PERSON_AMOUNT );
			
        //    objWrapper.AddParameter("@COVERAGE_M_EACH_PERSON_ID",objInfo.COVERAGE_M_EACH_PERSON_ID );
			
        //    if(objInfo.COVERAGE_M_EACH_OCC_AMOUNT==0)
        //        objWrapper.AddParameter("@COVERAGE_M_EACH_OCC_AMOUNT",System.DBNull.Value  );
        //    else
        //        objWrapper.AddParameter("@COVERAGE_M_EACH_OCC_AMOUNT",objInfo.COVERAGE_M_EACH_OCC_AMOUNT );
			
        //    objWrapper.AddParameter("@COVERAGE_M_EACH_OCC_ID",objInfo.COVERAGE_M_EACH_OCC_ID );
			
        //    if(objInfo.TOTAL_GENERAL_AGGREGATE == 0)
        //        objWrapper.AddParameter("@TOTAL_GENERAL_AGGREGATE",System.DBNull.Value  );
        //    else
        //        objWrapper.AddParameter("@TOTAL_GENERAL_AGGREGATE",objInfo.TOTAL_GENERAL_AGGREGATE );
			
        //    objWrapper.AddParameter("@CREATED_BY",objInfo.CREATED_BY);
        //    objWrapper.AddParameter("@CREATED_DATETIME",RecordDate);
        //    try
        //    {
        //        if ( strTranXML.Trim() == "" )
        //        {
        //            objWrapper.ExecuteNonQuery(strStoredProc);
				
        //        }
        //        else
        //        {
        //            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
        //            objTransactionInfo.TRANS_TYPE_ID	=	 1;
        //            objTransactionInfo.APP_ID			=	 objInfo.APP_ID;
        //            objTransactionInfo.APP_VERSION_ID	=	 objInfo.APP_VERSION_ID;
        //            objTransactionInfo.CLIENT_ID		= 	 objInfo.CUSTOMER_ID;
        //            objTransactionInfo.RECORDED_BY		=	 objInfo.CREATED_BY ;
        //            objTransactionInfo.TRANS_DESC		=	 "General Liability Coverage is added.";
        //            objTransactionInfo.CHANGE_XML		=	 strTranXML;
        //            intResult= objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
        //        }
										
        //        //If negative value returned because of no insert
        //        if ( intResult <= 0 )
        //        {
        //            objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
        //            return intResult;
        //        }
							
        //        objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
        //    }
		
        //    catch(Exception ex)
        //    {
        //        objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
        //        throw(ex);
        //    }
			
			

        //    return intResult;
        //}
        //#endregion

		#region AddPolicy  Function To Insert Data
		public int AddPolicy(Cms.Model.Policy.GeneralLiability.ClsGeneralLiabilityCoveragesInfo   objInfo)
		{
			string	strStoredProc =	"Proc_InsertPolicyGeneralLiabilityCoverages";
			string strTranXML = "";
			DateTime RecordDate = DateTime.Now;
			int intResult=0;
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/GeneralLiability/PolicyGeneralLiabilityCoverages.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objInfo);
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@POLICY_ID",objInfo.POLICY_ID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",objInfo.POLICY_VERSION_ID);

			if(objInfo.COVERAGE_L_AMOUNT == 0)
				objWrapper.AddParameter("@COVERAGE_L_AMOUNT",System.DBNull.Value);
			else
				objWrapper.AddParameter("@COVERAGE_L_AMOUNT",objInfo.COVERAGE_L_AMOUNT );
			
			objWrapper.AddParameter("@COVERAGE_L_ID",objInfo.COVERAGE_L_ID); 
			
			if(objInfo.COVERAGE_L_AGGREGATE==0 )
				objWrapper.AddParameter("@COVERAGE_L_AGGREGATE",System.DBNull.Value);
			else
				objWrapper.AddParameter("@COVERAGE_L_AGGREGATE",objInfo.COVERAGE_L_AGGREGATE );

			if(objInfo.COVERAGE_O_AMOUNT == 0 )
				objWrapper.AddParameter("@COVERAGE_O_AMOUNT",System.DBNull.Value  );
			else
				objWrapper.AddParameter("@COVERAGE_O_AMOUNT",objInfo.COVERAGE_O_AMOUNT );
			
			objWrapper.AddParameter("@COVERAGE_O_ID",objInfo.COVERAGE_O_ID );
			
			if(objInfo.COVERAGE_O_AGGREGATE==0)
				objWrapper.AddParameter("@COVERAGE_O_AGGREGATE",System.DBNull.Value );
			else
				objWrapper.AddParameter("@COVERAGE_O_AGGREGATE",objInfo.COVERAGE_O_AGGREGATE);
			
			if(objInfo.COVERAGE_M_EACH_PERSON_AMOUNT ==0)
				objWrapper.AddParameter("@COVERAGE_M_EACH_PERSON_AMOUNT",System.DBNull.Value );
			else
				objWrapper.AddParameter("@COVERAGE_M_EACH_PERSON_AMOUNT",objInfo.COVERAGE_M_EACH_PERSON_AMOUNT );
			
			objWrapper.AddParameter("@COVERAGE_M_EACH_PERSON_ID",objInfo.COVERAGE_M_EACH_PERSON_ID );
			
			if(objInfo.COVERAGE_M_EACH_OCC_AMOUNT==0)
				objWrapper.AddParameter("@COVERAGE_M_EACH_OCC_AMOUNT",System.DBNull.Value  );
			else
				objWrapper.AddParameter("@COVERAGE_M_EACH_OCC_AMOUNT",objInfo.COVERAGE_M_EACH_OCC_AMOUNT );
			
			objWrapper.AddParameter("@COVERAGE_M_EACH_OCC_ID",objInfo.COVERAGE_M_EACH_OCC_ID );
			
			if(objInfo.TOTAL_GENERAL_AGGREGATE == 0)
				objWrapper.AddParameter("@TOTAL_GENERAL_AGGREGATE",System.DBNull.Value  );
			else
				objWrapper.AddParameter("@TOTAL_GENERAL_AGGREGATE",objInfo.TOTAL_GENERAL_AGGREGATE );
			
			objWrapper.AddParameter("@CREATED_BY",objInfo.CREATED_BY);
			objWrapper.AddParameter("@CREATED_DATETIME",RecordDate);
			try
			{
				if ( strTranXML.Trim() == "" )
				{
					objWrapper.ExecuteNonQuery(strStoredProc);
				
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	 1;
					objTransactionInfo.POLICY_ID 		=	 objInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID 	=	 objInfo.POLICY_VERSION_ID; 
					objTransactionInfo.CLIENT_ID		= 	 objInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	 objInfo.CREATED_BY ;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1724", "");// "General Liability Coverage is added.";
					objTransactionInfo.CHANGE_XML		=	 strTranXML;
					intResult= objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
										
				
				if ( intResult <= 0 )
				{
					objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
					return intResult;
				}
							
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			

			return intResult;
		}
		#endregion

        //#region Update Function To Update Record
		
        //public int Update(ClsGeneralLiabilityCoveragesInfo  objOldInfo,ClsGeneralLiabilityCoveragesInfo  objInfo)
        //{
        //    string	strStoredProc =	"Proc_UpdateGeneralLiabilityCoverages";
        //    string strTranXML = "";
        //    DateTime RecordDate = DateTime.Now;
        //    int intResult=0;
			
        //    if ( this.TransactionLogRequired)
        //    {
        //        objInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/GeneralLiability/GeneralLiabilityCoverages.aspx.resx");
        //        SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
        //        strTranXML = objBuilder.GetTransactionLogXML(objOldInfo,objInfo);
		
        //    }

        //    DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
        //    objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
        //    objWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
        //    objWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
			
        //    if(objInfo.COVERAGE_L_AMOUNT == 0)
        //        objWrapper.AddParameter("@COVERAGE_L_AMOUNT",System.DBNull.Value);
        //    else
        //        objWrapper.AddParameter("@COVERAGE_L_AMOUNT",objInfo.COVERAGE_L_AMOUNT );
			
        //    objWrapper.AddParameter("@COVERAGE_L_ID",objInfo.COVERAGE_L_ID); 
			
        //    if(objInfo.COVERAGE_L_AGGREGATE==0 )
        //        objWrapper.AddParameter("@COVERAGE_L_AGGREGATE",System.DBNull.Value);
        //    else
        //        objWrapper.AddParameter("@COVERAGE_L_AGGREGATE",objInfo.COVERAGE_L_AGGREGATE );

        //    if(objInfo.COVERAGE_O_AMOUNT == 0 )
        //        objWrapper.AddParameter("@COVERAGE_O_AMOUNT",System.DBNull.Value  );
        //    else
        //        objWrapper.AddParameter("@COVERAGE_O_AMOUNT",objInfo.COVERAGE_O_AMOUNT );
			
        //    objWrapper.AddParameter("@COVERAGE_O_ID",objInfo.COVERAGE_O_ID );
			
        //    if(objInfo.COVERAGE_O_AGGREGATE==0)
        //        objWrapper.AddParameter("@COVERAGE_O_AGGREGATE",System.DBNull.Value );
        //    else
        //        objWrapper.AddParameter("@COVERAGE_O_AGGREGATE",objInfo.COVERAGE_O_AGGREGATE);
			
        //    if(objInfo.COVERAGE_M_EACH_PERSON_AMOUNT ==0)
        //        objWrapper.AddParameter("@COVERAGE_M_EACH_PERSON_AMOUNT",System.DBNull.Value );
        //    else
        //        objWrapper.AddParameter("@COVERAGE_M_EACH_PERSON_AMOUNT",objInfo.COVERAGE_M_EACH_PERSON_AMOUNT );
			
        //    objWrapper.AddParameter("@COVERAGE_M_EACH_PERSON_ID",objInfo.COVERAGE_M_EACH_PERSON_ID );
			
        //    if(objInfo.COVERAGE_M_EACH_OCC_AMOUNT==0)
        //        objWrapper.AddParameter("@COVERAGE_M_EACH_OCC_AMOUNT",System.DBNull.Value  );
        //    else
        //        objWrapper.AddParameter("@COVERAGE_M_EACH_OCC_AMOUNT",objInfo.COVERAGE_M_EACH_OCC_AMOUNT );
			
        //    objWrapper.AddParameter("@COVERAGE_M_EACH_OCC_ID",objInfo.COVERAGE_M_EACH_OCC_ID );
			
        //    if(objInfo.TOTAL_GENERAL_AGGREGATE == 0)
        //        objWrapper.AddParameter("@TOTAL_GENERAL_AGGREGATE",System.DBNull.Value  );
        //    else
        //        objWrapper.AddParameter("@TOTAL_GENERAL_AGGREGATE",objInfo.TOTAL_GENERAL_AGGREGATE );
			
        //    objWrapper.AddParameter("@MODIFIED_BY",objInfo.MODIFIED_BY );
        //    objWrapper.AddParameter("@LAST_UPDATED_DATETIME",RecordDate);

			
        //    try
        //    {	
        //        if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
        //        {
        //            objWrapper.ExecuteNonQuery(strStoredProc);
				
        //        }
        //        else
        //        {
        //            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
        //            objTransactionInfo.TRANS_TYPE_ID	=	2;
        //            objTransactionInfo.APP_ID	 = objInfo.APP_ID;
        //            objTransactionInfo.APP_VERSION_ID = objInfo.APP_VERSION_ID;
        //            objTransactionInfo.CLIENT_ID = objInfo.CUSTOMER_ID;
        //            objTransactionInfo.RECORDED_BY		=	objInfo.MODIFIED_BY;
        //            objTransactionInfo.TRANS_DESC		=	"General Liability Coverage is modified";
        //            objTransactionInfo.CHANGE_XML		=	strTranXML;
        //            intResult= objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
        //        throw(ex);
        //    }
        //    objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
		
        //    return intResult;
        //}
        //#endregion

		#region UpdatePolicy Function To Update Record
		
		public int UpdatePolicy(Cms.Model.Policy.GeneralLiability.ClsGeneralLiabilityCoveragesInfo   objOldInfo,
				Cms.Model.Policy.GeneralLiability.ClsGeneralLiabilityCoveragesInfo   objInfo)
		{
			string	strStoredProc =	"Proc_UpdatePolicyGeneralLiabilityCoverages";
			string strTranXML = "";
			DateTime RecordDate = DateTime.Now;
			int intResult=0;
			
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/GeneralLiability/PolicyGeneralLiabilityCoverages.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objOldInfo,objInfo);
		
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@POLICY_ID",objInfo.POLICY_ID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",objInfo.POLICY_VERSION_ID);
			
			if(objInfo.COVERAGE_L_AMOUNT == 0)
				objWrapper.AddParameter("@COVERAGE_L_AMOUNT",System.DBNull.Value);
			else
				objWrapper.AddParameter("@COVERAGE_L_AMOUNT",objInfo.COVERAGE_L_AMOUNT );
			
			objWrapper.AddParameter("@COVERAGE_L_ID",objInfo.COVERAGE_L_ID); 
			
			if(objInfo.COVERAGE_L_AGGREGATE==0 )
				objWrapper.AddParameter("@COVERAGE_L_AGGREGATE",System.DBNull.Value);
			else
				objWrapper.AddParameter("@COVERAGE_L_AGGREGATE",objInfo.COVERAGE_L_AGGREGATE );

			if(objInfo.COVERAGE_O_AMOUNT == 0 )
				objWrapper.AddParameter("@COVERAGE_O_AMOUNT",System.DBNull.Value  );
			else
				objWrapper.AddParameter("@COVERAGE_O_AMOUNT",objInfo.COVERAGE_O_AMOUNT );
			
			objWrapper.AddParameter("@COVERAGE_O_ID",objInfo.COVERAGE_O_ID );
			
			if(objInfo.COVERAGE_O_AGGREGATE==0)
				objWrapper.AddParameter("@COVERAGE_O_AGGREGATE",System.DBNull.Value );
			else
				objWrapper.AddParameter("@COVERAGE_O_AGGREGATE",objInfo.COVERAGE_O_AGGREGATE);
			
			if(objInfo.COVERAGE_M_EACH_PERSON_AMOUNT ==0)
				objWrapper.AddParameter("@COVERAGE_M_EACH_PERSON_AMOUNT",System.DBNull.Value );
			else
				objWrapper.AddParameter("@COVERAGE_M_EACH_PERSON_AMOUNT",objInfo.COVERAGE_M_EACH_PERSON_AMOUNT );
			
			objWrapper.AddParameter("@COVERAGE_M_EACH_PERSON_ID",objInfo.COVERAGE_M_EACH_PERSON_ID );
			
			if(objInfo.COVERAGE_M_EACH_OCC_AMOUNT==0)
				objWrapper.AddParameter("@COVERAGE_M_EACH_OCC_AMOUNT",System.DBNull.Value  );
			else
				objWrapper.AddParameter("@COVERAGE_M_EACH_OCC_AMOUNT",objInfo.COVERAGE_M_EACH_OCC_AMOUNT );
			
			objWrapper.AddParameter("@COVERAGE_M_EACH_OCC_ID",objInfo.COVERAGE_M_EACH_OCC_ID );
			
			if(objInfo.TOTAL_GENERAL_AGGREGATE == 0)
				objWrapper.AddParameter("@TOTAL_GENERAL_AGGREGATE",System.DBNull.Value  );
			else
				objWrapper.AddParameter("@TOTAL_GENERAL_AGGREGATE",objInfo.TOTAL_GENERAL_AGGREGATE );
			
			objWrapper.AddParameter("@MODIFIED_BY",objInfo.MODIFIED_BY );
			objWrapper.AddParameter("@LAST_UPDATED_DATETIME",RecordDate);

			
			try
			{	
				if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
				{
					objWrapper.ExecuteNonQuery(strStoredProc);
				
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.POLICY_ID	 = objInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID  = objInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1723", "");// "General Liability Coverage is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					intResult= objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
		
			return intResult;
		}
		#endregion
		
		#region GetCoverageDetails Function
		public DataTable GetCoverageDetails(int intCustomerId,int intAppId,int intAppVersionId)
		{
			string	strStoredProc =	"Proc_GetGeneralLiabilityCoverages";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
			objWrapper.AddParameter("@APP_ID",intAppId);
			objWrapper.AddParameter("@APP_VERSION_ID",intAppVersionId);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds.Tables[0];
		}
		#endregion

		#region GetPolicyCoverageDetails Function
		public DataTable GetPolicyCoverageDetails(int intCustomerId,int intPolicyId,int intPolicyVersionId)
		{
			string	strStoredProc =	"Proc_GetPolicyGeneralLiabilityCoverages";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
			objWrapper.AddParameter("@POLICY_ID",intPolicyId );
			objWrapper.AddParameter("@POLICY_VERSION_ID",intPolicyVersionId );
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds.Tables[0];
		}
		#endregion
	}
}
