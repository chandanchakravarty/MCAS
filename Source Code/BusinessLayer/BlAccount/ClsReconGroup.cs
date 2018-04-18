/******************************************************************************************
<Author					: -   Vijay Joshi
<Start Date				: -	6/29/2005 12:27:40 PM
<End Date				: -	
<Description			: - 	Business layer class for recongroup`
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using Cms.Model.Account;

namespace Cms.BusinessLayer.BlAccount
{

	public enum ReconEntityType
	{
		CUST=1,  //Customer
		AGN=2,   //Agency
		VEN=3	//Vendor
	}

	/// <summary>
	/// Business layer class for reconciliation group.
	/// </summary>
	public class ClsReconGroup : Cms.BusinessLayer.BlAccount.ClsAccount
	{
		private const	string		ACT_RECONCILIATION_GROUPS			=	"ACT_RECONCILIATION_GROUPS";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateACT_RECONCILIATION_GROUPS";
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
		
		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsReconGroup()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// created by : Ajit dated 19/7/2005
		/// overload of save which cerates a datatwrapper object
		/// </summary>
		/// <param name="objReconGroupInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsReconGroupInfo objReconGroupInfo)
		{
			DataWrapper objDataWrapper=null;
			int retVal=1;
			try
			{
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				retVal=Add(objReconGroupInfo,objDataWrapper);
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
			return retVal;
		}

		/// <summary>
		/// Get Deposit  Id based on CD_LINE_ITEM
		/// </summary>
		/// <param name="intCD_LINE_ITEM"></param>
		/// <returns></returns>
		public string GetDepositId(int intCD_LINE_ITEM)
		{
			string	strStoredProc =	"Proc_FetchDepositId";

			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CD_LINE_ITEM_ID",intCD_LINE_ITEM);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			try
			{
				return ds.Tables[0].Rows[0]["DEPOSIT_NUMBER"].ToString();
			}
			catch
			{
				return "";
			}
		}

		/// <summary>
		/// overload of save, Saves the information passed in model object to database.
		/// and takes part in transactions
		/// </summary>
		/// <param name="objReconGroupInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		/// modified: 18/7/2005
		/// By: Ajit
		public int Add(ClsReconGroupInfo objReconGroupInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_InsertACT_RECONCILIATION_GROUPS";
			DateTime	RecordDate		=	DateTime.Now;

			try
			{
				objDataWrapper.AddParameter("@RECON_ENTITY_ID",objReconGroupInfo.RECON_ENTITY_ID);
				objDataWrapper.AddParameter("@RECON_ENTITY_TYPE",objReconGroupInfo.RECON_ENTITY_TYPE);
				objDataWrapper.AddParameter("@IS_COMMITTED",objReconGroupInfo.IS_COMMITTED);
				objDataWrapper.AddParameter("@CD_LINE_ITEM_ID",objReconGroupInfo.CD_LINE_ITEM_ID);

				if (objReconGroupInfo.DATE_COMMITTED.Ticks != 0)
					objDataWrapper.AddParameter("@DATE_COMMITTED",objReconGroupInfo.DATE_COMMITTED);
				else
					objDataWrapper.AddParameter("@DATE_COMMITTED",null);

				objDataWrapper.AddParameter("@COMMITTED_BY",objReconGroupInfo.COMMITTED_BY);
				objDataWrapper.AddParameter("@CREATED_BY",objReconGroupInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objReconGroupInfo.CREATED_DATETIME);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@GROUP_ID",objReconGroupInfo.GROUP_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objReconGroupInfo.TransactLabel = ClsCommon.MapTransactionLabel("/account/aspx/AddReconGroup.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objReconGroupInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objReconGroupInfo.CREATED_BY;
					//objTransactionInfo.TRANS_DESC		=	"Agency Reconciliation Record Has Been Added";
					//ADDED BY PRAVEEN KUMAR(09-03-2009):ITRACK 4946
					if(objReconGroupInfo.RECON_ENTITY_TYPE == ReconEntityType.CUST.ToString())
						objTransactionInfo.TRANS_DESC		=	"Customer Reconciliation record has been added.";
					else if(objReconGroupInfo.RECON_ENTITY_TYPE == ReconEntityType.AGN .ToString())
						objTransactionInfo.TRANS_DESC		=	"Agency Reconciliation record has been added.";
					else if(objReconGroupInfo.RECON_ENTITY_TYPE == ReconEntityType.VEN.ToString())
						objTransactionInfo.TRANS_DESC		=	"Vendor Reconciliation record has been added.";
					//END PRAVEEN KUMAR
					//objTransactionInfo.CUSTOM_INFO		=   "Deposit Number :" + GetDepositId(objReconGroupInfo.CD_LINE_ITEM_ID)+ ";Deposit Type :" + objReconGroupInfo.RECON_ENTITY_TYPE ;
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				int GROUP_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				
				if (GROUP_ID == -1)
				{
					return -1;
				}
				else
				{
					objReconGroupInfo.GROUP_ID = GROUP_ID;
					return returnResult;
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			
		}
		#endregion

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldReconGroupInfo">Model object having old information</param>
		/// <param name="objReconGroupInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsReconGroupInfo objOldReconGroupInfo,ClsReconGroupInfo objReconGroupInfo,string EntityName)
		{
			XmlDocument tranXml = new XmlDocument();
			DataSet userName = new DataSet();
			string strTranXML;
			string strStoredProc = "Proc_UpdateACT_RECONCILIATION_GROUPS";
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			DataWrapper dataWrapperXml = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			try 
			{
				objDataWrapper.AddParameter("@RECON_ENTITY_ID",objReconGroupInfo.RECON_ENTITY_ID);
				objDataWrapper.AddParameter("@RECON_ENTITY_TYPE",objReconGroupInfo.RECON_ENTITY_TYPE);
				objDataWrapper.AddParameter("@IS_COMMITTED",objReconGroupInfo.IS_COMMITTED);
				
				if (objReconGroupInfo.DATE_COMMITTED.Ticks != 0)
					objDataWrapper.AddParameter("@DATE_COMMITTED",objReconGroupInfo.DATE_COMMITTED);
				else
					objDataWrapper.AddParameter("@DATE_COMMITTED",null);

				objDataWrapper.AddParameter("@COMMITTED_BY",objReconGroupInfo.COMMITTED_BY);
				objDataWrapper.AddParameter("@MODIFIED_BY",objReconGroupInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objReconGroupInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@GROUP_ID",objReconGroupInfo.GROUP_ID);

				if(TransactionLogRequired) 
				{
					objReconGroupInfo.TransactLabel = ClsCommon.MapTransactionLabel("/account/aspx/addrecongroup.aspx.resx");
					string strUpdate = objBuilder.GetUpdateSQL(objOldReconGroupInfo,objReconGroupInfo,out strTranXML);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objReconGroupInfo.MODIFIED_BY;
					//objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
					//ADDED BY PRAVEEN KUMAR(09-03-2009):ITRACK 4946
					if(objReconGroupInfo.RECON_ENTITY_TYPE == ReconEntityType.CUST.ToString())
					{
						objTransactionInfo.TRANS_DESC		=	"Customer Reconciliation record has been updated.";
						dataWrapperXml.AddParameter("@CUSTOMERID",objReconGroupInfo.RECON_ENTITY_ID.ToString());
						userName = dataWrapperXml.ExecuteDataSet("Proc_GetCustomerInfo");
						//objTransactionInfo.CUSTOM_INFO		=	";Customer Name = " + userName.Tables[0].Rows[0][0].ToString() + " " + userName.Tables[0].Rows[0][1].ToString() + " " +  userName.Tables[0].Rows[0][2].ToString();
						//Added FOR iTrack Issue #4946.
						if(userName.Tables[0].Rows[0][1].ToString() != null && userName.Tables[0].Rows[0][1].ToString() !="")
						{
							objTransactionInfo.CUSTOM_INFO		=	"Customer Name = " + userName.Tables[0].Rows[0][0].ToString() + " " + userName.Tables[0].Rows[0][1].ToString() + " " +  userName.Tables[0].Rows[0][2].ToString();
						}
						else
						{
						  objTransactionInfo.CUSTOM_INFO		=	"Customer Name = " + userName.Tables[0].Rows[0][0].ToString() + " " +   userName.Tables[0].Rows[0][2].ToString();
						}
					
					}
					else if(objReconGroupInfo.RECON_ENTITY_TYPE == ReconEntityType.AGN .ToString())
					{
						objTransactionInfo.TRANS_DESC		=	"Agency Reconciliation record has been updated.";
						dataWrapperXml.AddParameter("@AGENCY_ID",objReconGroupInfo.RECON_ENTITY_ID.ToString());
						userName = dataWrapperXml.ExecuteDataSet("Proc_FetchAgencyInfo");
						objTransactionInfo.CUSTOM_INFO		=	";Agency Name = " + userName.Tables[0].Rows[0][3].ToString();
					}
					else if(objReconGroupInfo.RECON_ENTITY_TYPE == ReconEntityType.VEN.ToString())
					{
						objTransactionInfo.TRANS_DESC		=	"Vendor Reconciliation record has been updated.";
						objTransactionInfo.CUSTOM_INFO		=	";Vendor Name = " + EntityName;
					}
					//END PRAVEEN KUMAR
					tranXml.LoadXml(strTranXML);
					XmlNode root = tranXml.FirstChild;
					XmlNodeList entityNameNode = root.SelectNodes("Map");
					foreach(XmlNode node in entityNameNode)
					{
						XmlAttributeCollection attrlist;
						attrlist = node.Attributes;
						foreach(XmlAttribute attr in attrlist)
						{
							if(attr.Name == "label")
							{
								if(attr.InnerText == "Entity Name")
								{
									node.Attributes.GetNamedItem("OldValue").InnerText = EntityName;
									//Added FOr iTrack Issue #4946.
									if(objReconGroupInfo.RECON_ENTITY_TYPE == ReconEntityType.CUST.ToString())
									{
										if(userName.Tables[0].Rows[0][1].ToString() != null && userName.Tables[0].Rows[0][1].ToString() != "")
											node.Attributes.GetNamedItem("NewValue").InnerText = userName.Tables[0].Rows[0][0].ToString() + " " +  userName.Tables[0].Rows[0][1].ToString() + " " +  userName.Tables[0].Rows[0][2].ToString();
										else
											node.Attributes.GetNamedItem("NewValue").InnerText = userName.Tables[0].Rows[0][0].ToString() + " "  + userName.Tables[0].Rows[0][2].ToString();
									}
									else if(objReconGroupInfo.RECON_ENTITY_TYPE == ReconEntityType.AGN .ToString())
										node.Attributes.GetNamedItem("NewValue").InnerText = userName.Tables[0].Rows[0][3].ToString();
								}
							}
						}
					}
					strTranXML = tranXml.InnerXml;
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					
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

		#region Commit Customer Items
		public int CommitReconGroup(int entity_ID,int group_ID,string entity_Type,int CommittedBy,string EntityName)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			string strStoredProc = "";
			if(entity_Type == ReconEntityType.CUST.ToString())
				strStoredProc= "Proc_CommitACT_CUSTOMER_OPEN_ITEMS";	
			else if(entity_Type == ReconEntityType.AGN .ToString())
				strStoredProc = "Proc_CommitACT_AGENCY_OPEN_ITEMS";	
			else if(entity_Type == ReconEntityType.VEN.ToString())
				strStoredProc = "Proc_CommitACT_Vendor_OPEN_ITEMS";

			int result =0;
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@ENTITY_ID", entity_ID);
				objWrapper.AddParameter("@GROUP_ID", group_ID);
				objWrapper.AddParameter("@COMMITTED_BY", CommittedBy);
				SqlParameter objParam = (SqlParameter)objWrapper.AddParameter("@RetVal", SqlDbType.Int, ParameterDirection.ReturnValue);
				
				//ADDED BY PRAVEEN KUMAR(09-03-2009):ITRACK 4946
				if(TransactionLogRequired) 
				{
					//objReconGroupInfo.TransactLabel = ClsCommon.MapTransactionLabel("/account/aspx/addrecongroup.aspx.resx");
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.RECORDED_BY = CommittedBy;
					if(entity_Type == ReconEntityType.CUST.ToString())
					{
						objTransactionInfo.TRANS_DESC		=	"Customer Reconciliation record has been committed.";
						objTransactionInfo.CUSTOM_INFO		=	";Customer Name = " + EntityName;
					}
					else if(entity_Type == ReconEntityType.AGN .ToString())
					{
						objTransactionInfo.TRANS_DESC		=	"Agency Reconciliation record has been committed.";
						objTransactionInfo.CUSTOM_INFO		=	";Agency Name = " + EntityName;
					}
					else if(entity_Type == ReconEntityType.VEN.ToString())
					{
						objTransactionInfo.TRANS_DESC		=	"Vendor Reconciliation record has been committed.";
						objTransactionInfo.CUSTOM_INFO		=	";Vendor Name = " + EntityName;
					}
					
					
					result =  objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					result =  objWrapper.ExecuteNonQuery(strStoredProc);
				}

				
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				//END PRAVEEN KUMAR
				result =  int.Parse(objParam.Value.ToString());
				return result;

				//result =  objWrapper.ExecuteNonQuery(strStoredProc);
				//result =  int.Parse(objParam.Value.ToString());
				//return result;

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in CommitCustomerOpenItems\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}
		}

        #endregion

		#region GetReconGroupInfo
		/// <summary>
		/// Returns the data in the form of XML of specified deposit id
		/// </summary>
		/// <param name="intDepositId">Deposit id whose data will be returned</param>
		/// <returns>XML of data</returns>
		public static string GetReconGroupInfo(int GroupId)
		{
			String strStoredProc = "Proc_GetACT_RECONCILIATION_GROUPS";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@GROUP_ID",GroupId);
				
				ds = objDataWrapper.ExecuteDataSet(strStoredProc);
				
				if (ds.Tables[0].Rows.Count != 0)
				{
					return ds.GetXml();
				}
				else
				{
					return "";
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

		#region Delete method
		/// <summary>
		/// Delete method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldReconGroupInfo">Model object having old information</param>
		/// <param name="objReconGroupInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int DeleteReconGroup(int GroupId, string EntityType, int UserId,string EntityName)
		{
			try
			{
				/*Calling the stored procedure to get the maximum Deposit entry no*/
				String strStoredProc = "Proc_DeleteACT_RECONCILIATION_GROUPS";
				int Value;

				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			
				objDataWrapper.AddParameter("@GROUP_ID", GroupId);
				objDataWrapper.AddParameter("@RECON_ENTITY_TYPE", EntityType);
				if(TransactionLogRequired) 
				{
					//objReconGroupInfo.TransactLabel = ClsCommon.MapTransactionLabel("/account/aspx/addrecongroup.aspx.resx");
					//ADDED BY PRAVEEN KUMAR(09-03-2009):ITRACK 4946
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.RECORDED_BY = UserId;
					if(EntityType == ReconEntityType.CUST.ToString())
					{
						objTransactionInfo.TRANS_DESC		=	"Customer Reconciliation record has been deleted.";
						objTransactionInfo.CUSTOM_INFO		=	";Customer Name = " + EntityName;
					}
					else if(EntityType == ReconEntityType.AGN .ToString())
					{
						objTransactionInfo.TRANS_DESC		=	"Agency Reconciliation record has been deleted.";
						objTransactionInfo.CUSTOM_INFO		=	";Agency Name = " + EntityName;
					}
					else if(EntityType == ReconEntityType.VEN.ToString())
					{
						objTransactionInfo.TRANS_DESC		=	"Vendor Reconciliation record has been deleted.";
						objTransactionInfo.CUSTOM_INFO		=	";Vendor Name = " + EntityName;
					}
					
					//END PRAVEEN KUMAR
					Value = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					Value = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

//				objDataWrapper.ClearParameteres();
//				objDataWrapper.Dispose();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return Value;
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		}
		#endregion

	}
}
