/* ***************************************************************************************
   Author		: Harmanjeet Singh 
   Creation Date: April 18, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: Class file for Special Acceptance Amount 
   Comments		: 
   ------------------------------------------------------------------------------------- 
   History	Date	     Modified By		Description
   
   ------------------------------------------------------------------------------------- 
   *****************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Configuration;
using Cms.DataLayer;
using System.Web.UI.WebControls;
using Cms.Model.Maintenance.Reinsurance;

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsReinsuranceContact.
	/// </summary>
	public class ClsReinsuranceContact:Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		# region D E C L A R A T I O N 

		private const string MNT_REIN_CONTACT="MNT_REIN_CONTACT";
		private	bool boolTransactionLog;

		public ClsReinsuranceContact()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= "Proc_MNT_REIN_DEACTIVATE_ACTIVATE_CONTACT";
		}

		# endregion 

		#region A D D  (I N S E R T)
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objReinsuranceInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsReinsuranceContactInfo objReinsuranceContactInfo)
		{
			string		strStoredProc	=	"MNT_REIN_INSERT_CONTACT";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@REIN_CONTACT_NAME",objReinsuranceContactInfo.REIN_CONTACT_NAME);
			
				objDataWrapper.AddParameter("@REIN_CONTACT_CODE",objReinsuranceContactInfo.REIN_CONTACT_CODE);
				objDataWrapper.AddParameter("@REIN_CONTACT_POSITION",objReinsuranceContactInfo.REIN_CONTACT_POSITION);
				objDataWrapper.AddParameter("@REIN_CONTACT_SALUTATION",objReinsuranceContactInfo.REIN_CONTACT_SALUTATION);
				objDataWrapper.AddParameter("@REIN_CONTACT_ADDRESS_1",objReinsuranceContactInfo.REIN_CONTACT_ADDRESS_1);
				objDataWrapper.AddParameter("@REIN_CONTACT_ADDRESS_2",objReinsuranceContactInfo.REIN_CONTACT_ADDRESS_2);
				objDataWrapper.AddParameter("@REIN_CONTACT_CITY",objReinsuranceContactInfo.REIN_CONTACT_CITY);
				objDataWrapper.AddParameter("@REIN_CONTACT_COUNTRY",objReinsuranceContactInfo.REIN_CONTACT_COUNTRY);
				objDataWrapper.AddParameter("@REIN_CONTACT_STATE",objReinsuranceContactInfo.REIN_CONTACT_STATE);
				objDataWrapper.AddParameter("@REIN_CONTACT_ZIP",objReinsuranceContactInfo.REIN_CONTACT_ZIP);
				objDataWrapper.AddParameter("@REIN_CONTACT_PHONE_1",objReinsuranceContactInfo.REIN_CONTACT_PHONE_1);
				objDataWrapper.AddParameter("@REIN_CONTACT_PHONE_2",objReinsuranceContactInfo.REIN_CONTACT_PHONE_2);
				objDataWrapper.AddParameter("@REIN_CONTACT_EXT_1",objReinsuranceContactInfo.REIN_CONTACT_EXT_1);
				objDataWrapper.AddParameter("@REIN_CONTACT_EXT_2",objReinsuranceContactInfo.REIN_CONTACT_EXT_2);
				objDataWrapper.AddParameter("@M_REIN_CONTACT_ADDRESS_1",objReinsuranceContactInfo.M_REIN_CONTACT_ADDRESS_1);
				objDataWrapper.AddParameter("@M_REIN_CONTACT_ADDRESS_2",objReinsuranceContactInfo.M_REIN_CONTACT_ADDRESS_2);
				objDataWrapper.AddParameter("@M_REIN_CONTACT_CITY",objReinsuranceContactInfo.M_REIN_CONTACT_CITY);
				objDataWrapper.AddParameter("@M_REIN_CONTACT_COUNTRY",objReinsuranceContactInfo.M_REIN_CONTACT_COUNTRY);
				objDataWrapper.AddParameter("@M_REIN_CONTACT_STATE",objReinsuranceContactInfo.M_REIN_CONTACT_STATE);
				objDataWrapper.AddParameter("@M_REIN_CONTACT_ZIP",objReinsuranceContactInfo.M_REIN_CONTACT_ZIP);
				objDataWrapper.AddParameter("@M_REIN_CONTACT_PHONE_1",objReinsuranceContactInfo.M_REIN_CONTACT_PHONE_1);
				objDataWrapper.AddParameter("@M_REIN_CONTACT_PHONE_2",objReinsuranceContactInfo.M_REIN_CONTACT_PHONE_2);
				objDataWrapper.AddParameter("@M_REIN_CONTACT_EXT_1",objReinsuranceContactInfo.M_REIN_CONTACT_EXT_1);
				objDataWrapper.AddParameter("@M_REIN_CONTACT_EXT_2",objReinsuranceContactInfo.M_REIN_CONTACT_EXT_2);
				objDataWrapper.AddParameter("@REIN_CONTACT_MOBILE",objReinsuranceContactInfo.REIN_CONTACT_MOBILE);
				objDataWrapper.AddParameter("@REIN_CONTACT_FAX",objReinsuranceContactInfo.REIN_CONTACT_FAX);
				objDataWrapper.AddParameter("@REIN_CONTACT_SPEED_DIAL",objReinsuranceContactInfo.REIN_CONTACT_SPEED_DIAL);
				objDataWrapper.AddParameter("@REIN_CONTACT_EMAIL_ADDRESS",objReinsuranceContactInfo.REIN_CONTACT_EMAIL_ADDRESS);
				objDataWrapper.AddParameter("@REIN_CONTACT_CONTRACT_DESC",objReinsuranceContactInfo.REIN_CONTACT_CONTRACT_DESC);
				objDataWrapper.AddParameter("@REIN_CONTACT_COMMENTS",objReinsuranceContactInfo.REIN_CONTACT_COMMENTS);
				objDataWrapper.AddParameter("@REIN_COMAPANY_ID",objReinsuranceContactInfo.REIN_COMAPANY_ID);
				objDataWrapper.AddParameter("@CREATED_BY",objReinsuranceContactInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objReinsuranceContactInfo.CREATED_DATETIME);				
			
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@REIN_CONTACT_ID",objReinsuranceContactInfo.REIN_CONTACT_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objReinsuranceContactInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/maintenance/Reinsurance/ReinsuranceContact.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objReinsuranceContactInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objReinsuranceContactInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Contact Information has been added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int  intRein_Contact_ID= int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (intRein_Contact_ID == -1)
				{
					return -1;
				}
				else
				{
					objReinsuranceContactInfo.REIN_CONTACT_ID= intRein_Contact_ID;
					return intRein_Contact_ID;
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
			
		# endregion 

		#region U P D A T E   
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldReinsuranceInfo">Model object having old information</param>
		/// <param name="objReinsuranceInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsReinsuranceContactInfo objOldReinsuranceContactInfo, ClsReinsuranceContactInfo objReinsuranceContactInfo)
		{
			string	strStoredProc	=	"MNT_REIN_UPDATE_CONTACT";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@REIN_CONTACT_ID",objReinsuranceContactInfo.REIN_CONTACT_ID);
				objDataWrapper.AddParameter("@REIN_CONTACT_NAME",objReinsuranceContactInfo.REIN_CONTACT_NAME);
				objDataWrapper.AddParameter("@REIN_CONTACT_CODE",objReinsuranceContactInfo.REIN_CONTACT_CODE);
				objDataWrapper.AddParameter("@REIN_CONTACT_POSITION",objReinsuranceContactInfo.REIN_CONTACT_POSITION);
				objDataWrapper.AddParameter("@REIN_CONTACT_SALUTATION",objReinsuranceContactInfo.REIN_CONTACT_SALUTATION);
				objDataWrapper.AddParameter("@REIN_CONTACT_ADDRESS_1",objReinsuranceContactInfo.REIN_CONTACT_ADDRESS_1);
				objDataWrapper.AddParameter("@REIN_CONTACT_ADDRESS_2",objReinsuranceContactInfo.REIN_CONTACT_ADDRESS_2);
				objDataWrapper.AddParameter("@REIN_CONTACT_CITY",objReinsuranceContactInfo.REIN_CONTACT_CITY);
				objDataWrapper.AddParameter("@REIN_CONTACT_COUNTRY",objReinsuranceContactInfo.REIN_CONTACT_COUNTRY);
				objDataWrapper.AddParameter("@REIN_CONTACT_STATE",objReinsuranceContactInfo.REIN_CONTACT_STATE);
				objDataWrapper.AddParameter("@REIN_CONTACT_ZIP",objReinsuranceContactInfo.REIN_CONTACT_ZIP);
				objDataWrapper.AddParameter("@REIN_CONTACT_PHONE_1",objReinsuranceContactInfo.REIN_CONTACT_PHONE_1);
				objDataWrapper.AddParameter("@REIN_CONTACT_PHONE_2",objReinsuranceContactInfo.REIN_CONTACT_PHONE_2);
				objDataWrapper.AddParameter("@REIN_CONTACT_EXT_1",objReinsuranceContactInfo.REIN_CONTACT_EXT_1);
				objDataWrapper.AddParameter("@REIN_CONTACT_EXT_2",objReinsuranceContactInfo.REIN_CONTACT_EXT_2);
				objDataWrapper.AddParameter("@M_REIN_CONTACT_ADDRESS_1",objReinsuranceContactInfo.M_REIN_CONTACT_ADDRESS_1);
				objDataWrapper.AddParameter("@M_REIN_CONTACT_ADDRESS_2",objReinsuranceContactInfo.M_REIN_CONTACT_ADDRESS_2);
				objDataWrapper.AddParameter("@M_REIN_CONTACT_CITY",objReinsuranceContactInfo.M_REIN_CONTACT_CITY);
				objDataWrapper.AddParameter("@M_REIN_CONTACT_COUNTRY",objReinsuranceContactInfo.M_REIN_CONTACT_COUNTRY);
				objDataWrapper.AddParameter("@M_REIN_CONTACT_STATE",objReinsuranceContactInfo.M_REIN_CONTACT_STATE);
				objDataWrapper.AddParameter("@M_REIN_CONTACT_ZIP",objReinsuranceContactInfo.M_REIN_CONTACT_ZIP);
				objDataWrapper.AddParameter("@M_REIN_CONTACT_PHONE_1",objReinsuranceContactInfo.M_REIN_CONTACT_PHONE_1);
				objDataWrapper.AddParameter("@M_REIN_CONTACT_PHONE_2",objReinsuranceContactInfo.M_REIN_CONTACT_PHONE_2);
				objDataWrapper.AddParameter("@M_REIN_CONTACT_EXT_1",objReinsuranceContactInfo.M_REIN_CONTACT_EXT_1);
				objDataWrapper.AddParameter("@M_REIN_CONTACT_EXT_2",objReinsuranceContactInfo.M_REIN_CONTACT_EXT_2);
				objDataWrapper.AddParameter("@REIN_CONTACT_MOBILE",objReinsuranceContactInfo.REIN_CONTACT_MOBILE);
				objDataWrapper.AddParameter("@REIN_CONTACT_FAX",objReinsuranceContactInfo.REIN_CONTACT_FAX);
				objDataWrapper.AddParameter("@REIN_CONTACT_SPEED_DIAL",objReinsuranceContactInfo.REIN_CONTACT_SPEED_DIAL);
				objDataWrapper.AddParameter("@REIN_CONTACT_EMAIL_ADDRESS",objReinsuranceContactInfo.REIN_CONTACT_EMAIL_ADDRESS);
				objDataWrapper.AddParameter("@REIN_CONTACT_CONTRACT_DESC",objReinsuranceContactInfo.REIN_CONTACT_CONTRACT_DESC);
				objDataWrapper.AddParameter("@REIN_CONTACT_COMMENTS",objReinsuranceContactInfo.REIN_CONTACT_COMMENTS);				
				objDataWrapper.AddParameter("@REIN_COMAPANY_ID",objReinsuranceContactInfo.REIN_COMAPANY_ID);
				objDataWrapper.AddParameter("@MODIFIED_BY",objReinsuranceContactInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objReinsuranceContactInfo.LAST_UPDATED_DATETIME);
	
				if(base.TransactionLogRequired) 
				{
					objReinsuranceContactInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/Reinsurance/ReinsuranceContact.aspx.resx");
					objBuilder.GetUpdateSQL(objOldReinsuranceContactInfo, objReinsuranceContactInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objReinsuranceContactInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Contact Information has been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
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

		# endregion 

		#region G E T  D A T A   F O R   E D I T   M O D E

		public DataSet GetDataForPageControls(string REIN_CONTACT_ID)
		{
			string strSql = "MNT_REIN_GETXML_CONTACT";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@REIN_CONTACT_ID",REIN_CONTACT_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}

		#endregion

		#region FETCHING DATA
		public DataSet FetchData(int REIN_COMPANY_ID)
		{
			string		strStoredProc	=	"Proc_FetchReisurernfo";
			DataSet dsCount=null;
           			
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@REIN_COMPANY_ID",REIN_COMPANY_ID,SqlDbType.Int);                                

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
		
		#region DEACTIVATE OR ACTIVATE CONTRACT TYPE

		public int GetDeactivateActivate(string REIN_CONTACT_ID,string Status_Check)
		{
			string strSql = "Proc_MNT_REIN_DEACTIVATE_ACTIVATE_CONTACT";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@REIN_CONTACT_ID",REIN_CONTACT_ID);
			objDataWrapper.AddParameter("@STATUS_CHECK",Status_Check,System.Data.SqlDbType.NVarChar);

			//DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			int returnResult	= objDataWrapper.ExecuteNonQuery(strSql);
			return returnResult;
		}

		#endregion


		# region Reinsurence Premium Reports
			# region Fill Transaction type

			public static DataSet FillTransactiontype()
			{
				string		strStoredProc	=	"Proc_FillTransactionType";
				DataSet dsCount=null;
	           			
				try
				{
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
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
			# region Fill Contract Dates
			public DataSet FillContractDates(int CONTRACT_ID)
			{
				string		strStoredProc	=	"Proc_FillContractDates";
				DataSet dsCount=null;
		           			
				try
				{
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objDataWrapper.AddParameter("@CONTRACT_ID",CONTRACT_ID,SqlDbType.Int);                                
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
			# region Fill Contract Number
			public static DataSet FillContractNumber()
			{
				string		strStoredProc	=	"Proc_FillContractNumber";
				DataSet dsCount=null;
			           			
				try
				{
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
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
			# region FetchReinsurancePremReport
			public DataSet FetchReinsurancePremReport(string contract_number,string contract_dates,string type_report,string report,string month_ending,string year,string major_part,string major_desc,string sp_accep,string tot_value_from,string tot_value_to,string tran_type,string user_id,string insu_value)
			{
				DataSet ds = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				try
				{
					if(contract_number == "")
						objDataWrapper.AddParameter("@CONTRACT_NUMBER", null, SqlDbType.VarChar);
					else
						objDataWrapper.AddParameter("@CONTRACT_NUMBER", contract_number, SqlDbType.VarChar);

					if(contract_dates == "")
						objDataWrapper.AddParameter("@CONTRACT_DATES", null, SqlDbType.VarChar);
					else
						objDataWrapper.AddParameter("@CONTRACT_DATES", contract_dates, SqlDbType.VarChar);
					
					if(type_report == "")
						objDataWrapper.AddParameter("@TYPE_REPORT", null, SqlDbType.VarChar);
					else
						objDataWrapper.AddParameter("@TYPE_REPORT", type_report, SqlDbType.VarChar);

					if(report == "")
						objDataWrapper.AddParameter("@REPORT", null, SqlDbType.VarChar);
					else
						objDataWrapper.AddParameter("@REPORT", report, SqlDbType.VarChar);
					if(month_ending == "")
						objDataWrapper.AddParameter("@MONTH_ENDING", null, SqlDbType.VarChar);
					else
						objDataWrapper.AddParameter("@MONTH_ENDING", month_ending, SqlDbType.VarChar);

					if(year == "")
						objDataWrapper.AddParameter("@YEAR", null, SqlDbType.VarChar);
					else
						objDataWrapper.AddParameter("@YEAR", year, SqlDbType.VarChar);
					if(major_part == "")
						objDataWrapper.AddParameter("@MAJOR_PART", null, SqlDbType.VarChar);
					else
						objDataWrapper.AddParameter("@MAJOR_PART", major_part, SqlDbType.VarChar);

					if(major_desc == "")
						objDataWrapper.AddParameter("@MAJOR_DESC", null, SqlDbType.VarChar);
					else
						objDataWrapper.AddParameter("@MAJOR_DESC", major_desc, SqlDbType.VarChar);
					if(sp_accep == "")
						objDataWrapper.AddParameter("@SP_ACCEP", null, SqlDbType.VarChar);
					else
						objDataWrapper.AddParameter("@SP_ACCEP", sp_accep, SqlDbType.VarChar);

					if(tot_value_from == "")
						objDataWrapper.AddParameter("@TOT_VALUE_FROM", null, SqlDbType.VarChar);
					else
						objDataWrapper.AddParameter("@TOT_VALUE_FROM", tot_value_from, SqlDbType.VarChar);
					if(tot_value_to == "")
						objDataWrapper.AddParameter("@TOT_VALUE_TO", null, SqlDbType.VarChar);
					else
						objDataWrapper.AddParameter("@TOT_VALUE_TO", tot_value_to, SqlDbType.VarChar);

					if(tran_type == "")
						objDataWrapper.AddParameter("@TRAN_TYPE", null, SqlDbType.VarChar);
					else
						objDataWrapper.AddParameter("@TRAN_TYPE", tran_type, SqlDbType.VarChar);

					if(user_id == "")
						objDataWrapper.AddParameter("@USER_ID", null, SqlDbType.VarChar);
					else
						objDataWrapper.AddParameter("@USER_ID", user_id, SqlDbType.VarChar);
					
					if(insu_value == "")
						objDataWrapper.AddParameter("@INSU_VALUE", null, SqlDbType.VarChar);
					else
						objDataWrapper.AddParameter("@INSU_VALUE", insu_value, SqlDbType.VarChar);

					ds = objDataWrapper.ExecuteDataSet("Proc_RPTReinsurancePremReport");
					
					return ds;				
					
				}
				catch(Exception objEx)
				{
					throw(objEx);				
				}
				finally
				{
					if(objDataWrapper!=null)
						objDataWrapper.Dispose(); 			
				}

			}
			#endregion

			public static DataSet FetchReinsuranceUmbReport(string contract_number,string tran_type)
			{
				DataSet ds = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				try
				{
					if(contract_number == "")
						objDataWrapper.AddParameter("@CONTRACT_NUMBER", null, SqlDbType.VarChar);
					else
						objDataWrapper.AddParameter("@CONTRACT_NUMBER", contract_number, SqlDbType.VarChar);
					
					if(tran_type == "")
						objDataWrapper.AddParameter("@TRAN_TYPE", null, SqlDbType.VarChar);
					else
						objDataWrapper.AddParameter("@TRAN_TYPE", tran_type, SqlDbType.VarChar);

					ds = objDataWrapper.ExecuteDataSet("Proc_RPTReinsuranceUmbrellaReport");
					
					return ds;				
					
				}
				catch(Exception objEx)
				{
					throw(objEx);				
				}
				finally
				{
					if(objDataWrapper!=null)
						objDataWrapper.Dispose(); 			
				}

			}
		#endregion
		
	}
}

