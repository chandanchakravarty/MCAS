/******************************************************************************************
<Author					: -	Pradeep
<Start Date				: -	26 Apr, 2005
<End Date				: -	
<Description			: - Business layer class for Customer AKA/DBA	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*/

using System;
using System.Data;
using System.Data.SqlClient;
using Cms.Model;
using Cms.Model.Client;
using Cms.DataLayer;

namespace Cms.BusinessLayer.BlClient
{
	/// <summary>
	/// Summary description for ClsAkaDba.
	/// </summary>
	public class ClsAkaDba : Cms.BusinessLayer.BlClient.ClsClient
	{
		public ClsAkaDba()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		/// <summary>
		/// Returns a DataSet comtaining a single record from Customer AKADBA table. 
		/// </summary>
		/// <param name="akaDbaID"></param>
		/// <returns></returns>
		public DataSet GetAkaDbaByID(int akaDbaID)
		{
			string	strStoredProc =	"Proc_GetAkADbaByID";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@AKADBA_ID",akaDbaID);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

			return ds;

		}
		
		/// <summary>
		/// Updates a record in CLT_CUSTOMER_AKADBA table
		/// </summary>
		/// <param name="objDbaInfo"></param>
		/// <returns></returns>
		public int Update(Cms.Model.Client.ClsAkaDbaInfo objDbaInfo)
		{
			string	strStoredProc =	"Proc_UpdateCustomerAkaDba";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@AkaDba_ID",objDbaInfo.AKADBA_ID);
			objWrapper.AddParameter("@AkaDba_Type",objDbaInfo.AKADBA_TYPE);
			objWrapper.AddParameter("@AkaDba_Name",objDbaInfo.AKADBA_NAME);
			objWrapper.AddParameter("@AkaDba_Add",objDbaInfo.AKADBA_ADD);
			objWrapper.AddParameter("@AkaDba_Add2",objDbaInfo.AKADBA_ADD2);
			objWrapper.AddParameter("@AkaDba_City",objDbaInfo.AKADBA_CITY);
			objWrapper.AddParameter("@AkaDba_State",objDbaInfo.AKADBA_STATE);
			objWrapper.AddParameter("@AkaDba_Zip",objDbaInfo.AKADBA_ZIP);
			objWrapper.AddParameter("@AkaDba_Country",objDbaInfo.AKADBA_COUNTRY);
			objWrapper.AddParameter("@AkaDba_Website",objDbaInfo.AKADBA_WEBSITE);
			objWrapper.AddParameter("@AkaDba_Email",objDbaInfo.AKADBA_EMAIL);
			objWrapper.AddParameter("@AkaDba_Legal_Entity_Code",objDbaInfo.AKADBA_LEGAL_ENTITY_CODE);
			objWrapper.AddParameter("@AkaDba_Name_On_Form",objDbaInfo.AKADBA_NAME_ON_FORM);
			objWrapper.AddParameter("@AkaDba_Disp_Order",DefaultValues.GetIntNull(objDbaInfo.AKADBA_DISP_ORDER));
			objWrapper.AddParameter("@AkaDba_Memo",objDbaInfo.AKADBA_MEMO);
			objWrapper.AddParameter("@Modified_By",objDbaInfo.MODIFIED_BY);
			
			try
			{
				objWrapper.ExecuteNonQuery(strStoredProc);
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 0;
			
		}
		
		/// <summary>
		/// Inserts a record into  CLT_CUSTOMER_AKADBA table
		/// </summary>
		/// <param name="objDbaInfo"></param>
		/// <returns></returns>
		public int Add(ClsAkaDbaInfo objDbaInfo)
		{
			string	strStoredProc =	"Proc_InsertCustomerAkaDba";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@Customer_ID",objDbaInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@AkaDba_Type",objDbaInfo.AKADBA_TYPE);
			objWrapper.AddParameter("@AkaDba_Name",objDbaInfo.AKADBA_NAME);
			objWrapper.AddParameter("@AkaDba_Add",objDbaInfo.AKADBA_ADD);
			objWrapper.AddParameter("@AkaDba_Add2",objDbaInfo.AKADBA_ADD2);
			objWrapper.AddParameter("@AkaDba_City",objDbaInfo.AKADBA_CITY);
			objWrapper.AddParameter("@AkaDba_State",objDbaInfo.AKADBA_STATE);
			objWrapper.AddParameter("@AkaDba_Zip",objDbaInfo.AKADBA_ZIP);
			objWrapper.AddParameter("@AkaDba_Country",objDbaInfo.AKADBA_COUNTRY);
			objWrapper.AddParameter("@AkaDba_Website",objDbaInfo.AKADBA_WEBSITE);
			objWrapper.AddParameter("@AkaDba_Email",objDbaInfo.AKADBA_EMAIL);
			objWrapper.AddParameter("@AkaDba_Legal_Entity_Code",objDbaInfo.AKADBA_LEGAL_ENTITY_CODE);
			objWrapper.AddParameter("@AkaDba_Name_On_Form",objDbaInfo.AKADBA_NAME_ON_FORM);
			objWrapper.AddParameter("@AkaDba_Disp_Order",DefaultValues.GetIntNull(objDbaInfo.AKADBA_DISP_ORDER));
			objWrapper.AddParameter("@AkaDba_Memo",objDbaInfo.AKADBA_MEMO);
			objWrapper.AddParameter("@Is_Active",objDbaInfo.IS_ACTIVE);
			objWrapper.AddParameter("@Created_By",objDbaInfo.CREATED_BY);
			objWrapper.AddParameter("@Modified_By",objDbaInfo.MODIFIED_BY);
			
			objWrapper.AddParameter("@AkaDba_ID",SqlDbType.Int,ParameterDirection.Output);
			
			try
			{
				objWrapper.ExecuteNonQuery(strStoredProc);
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			int akaDbaID = Convert.ToInt32(objWrapper.CommandParameters[18].Value);

			return akaDbaID;

		}
		

	}
}
