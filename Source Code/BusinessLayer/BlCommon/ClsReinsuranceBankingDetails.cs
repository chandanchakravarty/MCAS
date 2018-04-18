/* ***************************************************************************************
   Author		: Harmanjeet Singh 
   Creation Date: April 24, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: Class file for BANKING DETAILS 
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
	/// Summary description for ClsReinsuranceBankingDetails.
	/// </summary>
	public class ClsReinsuranceBankingDetails:Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		
		# region D E C L A R A T I O N 

		private const string MNT_REIN_BANKING_DETAIL="MNT_REIN_BANKING_DETAIL";
		private	bool boolTransactionLog;

		public ClsReinsuranceBankingDetails()
		{
			//
			// TODO: Add constructor logic here
			//
			boolTransactionLog	= base.TransactionLogRequired;
		}

		# endregion 

		#region A D D  (I N S E R T)
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objReinsuranceInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsReinsuranceBankingDetailsInfo objReinsuranceBankingDetailsInfo)
		{
			string		strStoredProc	=	"MNT_REIN_INSERT_BANKINGDETAIL";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@REIN_COMAPANY_ID",objReinsuranceBankingDetailsInfo.REIN_COMAPANY_ID);
				objDataWrapper.AddParameter("@REIN_BANK_DETAIL_ADDRESS_1",objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_ADDRESS_1);
				objDataWrapper.AddParameter("@REIN_BANK_DETAIL_ADDRESS_2",objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_ADDRESS_2);
				objDataWrapper.AddParameter("@REIN_BANK_DETAIL_CITY",objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_CITY);
				objDataWrapper.AddParameter("@REIN_BANK_DETAIL_COUNTRY",objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_COUNTRY);
				objDataWrapper.AddParameter("@REIN_BANK_DETAIL_STATE",objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_STATE);
				objDataWrapper.AddParameter("@REIN_BANK_DETAIL_ZIP",objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_ZIP);
				
				objDataWrapper.AddParameter("@M_REIN_BANK_DETAIL_ADDRESS_1",objReinsuranceBankingDetailsInfo.M_REIN_BANK_DETAIL_ADDRESS_1);
				objDataWrapper.AddParameter("@M_REIN_BANK_DETAIL_ADDRESS_2",objReinsuranceBankingDetailsInfo.M_REIN_BANK_DETAIL_ADDRESS_2);
				objDataWrapper.AddParameter("@M_REIN_BANK_DETAIL_CITY",objReinsuranceBankingDetailsInfo.M_REIN_BANK_DETAIL_CITY);
				objDataWrapper.AddParameter("@M_REIN_BANK_DETAIL_COUNTRY",objReinsuranceBankingDetailsInfo.M_REIN_BANK_DETAIL_COUNTRY);
				objDataWrapper.AddParameter("@M_REIN_BANK_DETAIL_STATE",objReinsuranceBankingDetailsInfo.M_REIN_BANK_DETAIL_STATE);
				objDataWrapper.AddParameter("@M_REIN_BANK_DETAIL_ZIP",objReinsuranceBankingDetailsInfo.M_REIN_BANK_DETAIL_ZIP);
				
				
				objDataWrapper.AddParameter("@REIN_PAYMENT_BASIS",objReinsuranceBankingDetailsInfo.REIN_PAYMENT_BASIS);
				objDataWrapper.AddParameter("@REIN_BANK_NAME",objReinsuranceBankingDetailsInfo.REIN_BANK_NAME);
				objDataWrapper.AddParameter("@REIN_TRANSIT_ROUTING",objReinsuranceBankingDetailsInfo.REIN_TRANSIT_ROUTING);
				objDataWrapper.AddParameter("@REIN_BANK_ACCOUNT",objReinsuranceBankingDetailsInfo.REIN_BANK_ACCOUNT);
				//objDataWrapper.AddParameter("@REIN_CONTACT_COMMENTS",objReinsuranceContactInfo.REIN_CONTACT_COMMENTS);
				objDataWrapper.AddParameter("@CREATED_BY",objReinsuranceBankingDetailsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objReinsuranceBankingDetailsInfo.CREATED_DATETIME);				
			
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@REIN_BANK_DETAIL_ID",objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objReinsuranceBankingDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/maintenance/Reinsurance/ReinsuranceBankingDetails.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objReinsuranceBankingDetailsInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	239;
					objTransactionInfo.RECORDED_BY		=	objReinsuranceBankingDetailsInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
                    objTransactionInfo.CUSTOM_INFO = objReinsuranceBankingDetailsInfo.CUSTOM_INFO;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int  intREIN_BANK_DETAIL_ID= int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (intREIN_BANK_DETAIL_ID == -1)
				{
					return -1;
				}
				else
				{
					objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_ID= intREIN_BANK_DETAIL_ID;
					return intREIN_BANK_DETAIL_ID;
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
		public int Update(ClsReinsuranceBankingDetailsInfo objOldReinsuranceBankingDetailsInfo, ClsReinsuranceBankingDetailsInfo objReinsuranceBankingDetailsInfo)
		{
			string	strStoredProc	=	"MNT_REIN_UPDATE_BANKINGDETAIL";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@REIN_BANK_DETAIL_ID",objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_ID);
				objDataWrapper.AddParameter("@REIN_COMAPANY_ID",objReinsuranceBankingDetailsInfo.REIN_COMAPANY_ID);
				objDataWrapper.AddParameter("@REIN_BANK_DETAIL_ADDRESS_1",objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_ADDRESS_1);
				objDataWrapper.AddParameter("@REIN_BANK_DETAIL_ADDRESS_2",objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_ADDRESS_2);
				objDataWrapper.AddParameter("@REIN_BANK_DETAIL_CITY",objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_CITY);
				objDataWrapper.AddParameter("@REIN_BANK_DETAIL_COUNTRY",objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_COUNTRY);
				objDataWrapper.AddParameter("@REIN_BANK_DETAIL_STATE",objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_STATE);
				objDataWrapper.AddParameter("@REIN_BANK_DETAIL_ZIP",objReinsuranceBankingDetailsInfo.REIN_BANK_DETAIL_ZIP);
				
				objDataWrapper.AddParameter("@M_REIN_BANK_DETAIL_ADDRESS_1",objReinsuranceBankingDetailsInfo.M_REIN_BANK_DETAIL_ADDRESS_1);
				objDataWrapper.AddParameter("@M_REIN_BANK_DETAIL_ADDRESS_2",objReinsuranceBankingDetailsInfo.M_REIN_BANK_DETAIL_ADDRESS_2);
				objDataWrapper.AddParameter("@M_REIN_BANK_DETAIL_CITY",objReinsuranceBankingDetailsInfo.M_REIN_BANK_DETAIL_CITY);
				objDataWrapper.AddParameter("@M_REIN_BANK_DETAIL_COUNTRY",objReinsuranceBankingDetailsInfo.M_REIN_BANK_DETAIL_COUNTRY);
				objDataWrapper.AddParameter("@M_REIN_BANK_DETAIL_STATE",objReinsuranceBankingDetailsInfo.M_REIN_BANK_DETAIL_STATE);
				objDataWrapper.AddParameter("@M_REIN_BANK_DETAIL_ZIP",objReinsuranceBankingDetailsInfo.M_REIN_BANK_DETAIL_ZIP);
				
				objDataWrapper.AddParameter("@REIN_PAYMENT_BASIS",objReinsuranceBankingDetailsInfo.REIN_PAYMENT_BASIS);
				objDataWrapper.AddParameter("@REIN_BANK_NAME",objReinsuranceBankingDetailsInfo.REIN_BANK_NAME);
				objDataWrapper.AddParameter("@REIN_TRANSIT_ROUTING",objReinsuranceBankingDetailsInfo.REIN_TRANSIT_ROUTING);
				objDataWrapper.AddParameter("@REIN_BANK_ACCOUNT",objReinsuranceBankingDetailsInfo.REIN_BANK_ACCOUNT);
			
				objDataWrapper.AddParameter("@MODIFIED_BY",objReinsuranceBankingDetailsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objReinsuranceBankingDetailsInfo.LAST_UPDATED_DATETIME);				
			
			
				if(base.TransactionLogRequired) 
				{
					objReinsuranceBankingDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/Reinsurance/ReinsuranceBankingDetails.aspx.resx");
					objBuilder.GetUpdateSQL(objOldReinsuranceBankingDetailsInfo, objReinsuranceBankingDetailsInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	240;
					objTransactionInfo.RECORDED_BY		=	objReinsuranceBankingDetailsInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
                    objTransactionInfo.CUSTOM_INFO = objReinsuranceBankingDetailsInfo.CUSTOM_INFO;
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

		public DataSet GetDataForPageControls(string REIN_COMAPANY_ID)
		{
			string strSql = "MNT_REIN_GETXML_BANKINGDETAIL";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@REIN_COMAPANY_ID",REIN_COMAPANY_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}

		#endregion

		
	}
}
