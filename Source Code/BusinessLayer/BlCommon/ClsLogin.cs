using System;
using System.Data;
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data.SqlClient;
using System.Configuration;

/******************************************************************************************
	<Author					: Gaurav Tyagi- >
	<Start Date				: March 14, 2005-	>
	<End Date				: - >
	<Description			: - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - > 17/03/2005
	<Modified By			: - > Anurag Verma
	<Purpose				: - > Changing inline query to Stored Procedure
    
	<Modified Date			: - > 08/04/2005
	<Modified By			: - > Anurag Verma
	<Purpose				: - > Removing SqlParameter from the class and using Object array to get dataset
	
	
*******************************************************************************************/

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsLogin.
	/// </summary>
	public class ClsLogin:ClsCommon
	{
		private bool boolTransactionLog; // using to store whether transaction is required or not

		/// <summary>
		/// Default Constructor
		/// </summary>
        public ClsLogin()
		{
			boolTransactionLog	= TransactionLogRequired;	
		}

		#region Login

		/// <summary>
		/// This function is used to reterive User Information and allow them to login .
		///  if there entered information matched with database.
		/// </summary>
		/// <param name="sid">system id</param>
		/// <param name="uid">user id</param>
		/// <param name="pwd">password</param>
		/// <returns>dataset containing user information</returns>
		public DataSet GetUserInformation(string sid,string uid,string pwd)
		{
			try
			{
                Object [] sparam=new Object[3]; //declaring object array to be passed to executeDataset method of datawrapper

                sparam[0]=sid;
                sparam[1]=uid;
                sparam[2]=pwd;

                
                return DataWrapper.ExecuteDataset(ConnStr,"Proc_CheckUser",sparam); 
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				
			}
		}
		#endregion
        
            #region Carrier Information

		/// <summary>
        /// This function is used to reterive Carrier Information  
		/// </summary>
		/// <param name="sid">system id</param>
        /// <returns>dataset containing Carrier information</returns>
        public DataSet GetCarrierInformation(string sid)
		{
			try
			{
                Object [] sparam=new Object[1]; //declaring object array to be passed to executeDataset method of datawrapper
                sparam[0]=sid;
                return DataWrapper.ExecuteDataset(CommonDBConnStr, "Proc_GetCarrierinfo", sparam); 
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				
			}
		}
		#endregion

		#region Update Logged Status
		public void UpdateLoggedStatus(int userID,string logOut)
		{
			string		strStoredProc	=	"Proc_UpdateLoggedStatus";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@USER_ID",userID);				
				objDataWrapper.AddParameter("@LOG_OUT",logOut);		
				int returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				
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
		public void UpdateLoggedStatus(int userID,string logOut,string sessionID)
		{
			string		strStoredProc	=	"Proc_UpdateLoggedStatus";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@USER_ID",userID);				
				objDataWrapper.AddParameter("@LOG_OUT",logOut);		
				objDataWrapper.AddParameter("@SESSION_ID",sessionID);	
				int returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				
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
		#endregion

		#region Logout

		
		#endregion
	}
}
