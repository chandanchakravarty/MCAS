/******************************************************************************************
<Author				: -   Priya
<Start Date				: -	6/24/2005 12:23:43 PM
<End Date				: -	
<Description				: - 	
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
using Cms.DataLayer;
using System.Configuration;
using Cms.Model.Maintenance;

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsPolicyNoSetup : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private const	string		MNT_LOB_MASTER			=	"MNT_LOB_MASTER";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int _LOB_ID;
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
		public ClsPolicyNoSetup()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			
		}
		#endregion

		

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldPolicyNoSetupInfo">Model object having old information</param>
		/// <param name="ObjPolicyNoSetupInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(Cms.Model.Maintenance.ClsPolicyNoSetupInfo objOldPolicyNoSetupInfo,Cms.Model.Maintenance.ClsPolicyNoSetupInfo ObjPolicyNoSetupInfo)
		{												  
			string strTranXML;
			string strStoredProc="Proc_UpdateMasterLOB";
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@LOB_ID",ObjPolicyNoSetupInfo.LOB_ID);
				objDataWrapper.AddParameter("@LOB_PREFIX",ObjPolicyNoSetupInfo.LOB_PREFIX);
				objDataWrapper.AddParameter("@LOB_SUFFIX",ObjPolicyNoSetupInfo.LOB_SUFFIX);
				objDataWrapper.AddParameter("@LOB_SEED",ObjPolicyNoSetupInfo.LOB_SEED);
				SqlParameter objRetVal = (SqlParameter) objDataWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);

				if(TransactionLogRequired) 
				{
					string strUpdate = objBuilder.GetUpdateSQL(objOldPolicyNoSetupInfo,ObjPolicyNoSetupInfo,out strTranXML);

					ObjPolicyNoSetupInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsWeb/Maintenance/PolicyNoSetup.aspx.resx");Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	ObjPolicyNoSetupInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				if (int.Parse(objRetVal.Value.ToString()) == -2)
				{
					return -2;
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

		#region GetLOBMasterXml
		
		public static DataSet GetLOBMasterXml(int intLOB_ID)
		{
			string strStoredProc = "Proc_GetLOBMasterInformation";
			DataSet dsLOBMaster= null;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@LOB_ID",intLOB_ID);
				   dsLOBMaster=new DataSet();

				dsLOBMaster= objDataWrapper.ExecuteDataSet(strStoredProc);
				if (dsLOBMaster.Tables[0].Rows.Count != 0)
				{
					return dsLOBMaster;
				}
				else
				{
					return dsLOBMaster;
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

		#region GetFiscalYearPolicies
		//Added by Mohit Agarwal 16-May 2007
		public static string GetFiscalYearPoliciesFile()
		{
			string strStoredProc = "Proc_GetFiscalYearActiveCancPolicies";
			DataSet dsFiscalPolicies= null;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				string PolNum = "";
				string file_path = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()).ToString() + "\\OUTPUTPDFs\\" + "Fiscal_policies.txt";
				dsFiscalPolicies=new DataSet();

				dsFiscalPolicies= objDataWrapper.ExecuteDataSet(strStoredProc);
				System.IO.StreamWriter fiscal_file = new System.IO.StreamWriter(file_path, true);
				foreach(DataRow drFiscal in dsFiscalPolicies.Tables[0].Rows)
				{
					PolNum = drFiscal["POLICY_NUMBER"].ToString();
					if(drFiscal["POLICY_STATUS"].ToString().ToUpper() == "CANCEL")
						PolNum += "C";
					fiscal_file.WriteLine(PolNum);
				}
				fiscal_file.Close();
				return file_path;
			}
			catch
			{
				return "";
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}
		
		#endregion

		#region GetPremiumNoticePolicyData
		//Added by Mohit Agarwal 16-May 2007
		public static DataSet GetPremiumNoticePolicyData(string polNum)
		{
			string strStoredProc = "Proc_GetPolicyInformationFromPolNumber";
			DataSet dsPolicy= null;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				dsPolicy=new DataSet();

				objDataWrapper.AddParameter("@POLICY_NUMBER",polNum);
				dsPolicy= objDataWrapper.ExecuteDataSet(strStoredProc);
				return dsPolicy;

			}
			catch
			{
				return null;
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}
		
		#endregion

		/*public string GetPolicyXML()
		{
			string returnResult;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			returnResult = objDataWrapper.ExecuteDataSet("select   from  WHERE ").GetXml();
			return	returnResult;
        }	 */
		
	}
}
