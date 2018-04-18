/******************************************************************************************
<Author				: -   Sumit Chhabra
<Start Date				: -	9/29/2005 10:06:52 AM
<End Date				: -	
<Description				: - 	BL Class to modify MNT_VIOLATIONS
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;				
using Cms.Model.Application;
namespace Cms.Model.Application
{
	/// <summary>
	/// BL Class to modify MNT_VIOLATIONS
	/// </summary>
	public class ClsMntViolations : Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
	{
		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsMntViolations()
		{
			
		}
		#endregion

		
		#region "GetxmlMethods for complete table"
		
		public static DataTable  GetViolationTypes(string strCustomerID, string strAppID, string strAppVersionID)
		{
			string strSql = "Proc_GetMNT_VIOLATIONS_TYPE";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);			
			objDataWrapper.AddParameter("@CUSTOMER_ID",strCustomerID);
			objDataWrapper.AddParameter("@APP_ID",strAppID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",strAppVersionID);
			//objDataWrapper.AddParameter("@LOB_ID",LOB_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			if(objDataSet!=null && objDataSet.Tables[0].Rows.Count>0)
				return objDataSet.Tables[0];
			else
				return null;
		}
		public static DataTable  GetViolationTypesForPolicy(string strCustomerID, string strPolID, string strPolVersionID)
		{
			string strSql = "Proc_GetMNT_VIOLATIONS_TYPE_POL";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);			
			objDataWrapper.AddParameter("@CUSTOMER_ID",strCustomerID);
			objDataWrapper.AddParameter("@POLICY_ID",strPolID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",strPolVersionID);
			//objDataWrapper.AddParameter("@LOB_ID",LOB_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			if(objDataSet!=null && objDataSet.Tables[0].Rows.Count>0)
				return objDataSet.Tables[0];
			else
				return null;
		}
			
		#endregion
	}
}
