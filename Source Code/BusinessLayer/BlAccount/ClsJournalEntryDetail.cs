/******************************************************************************************
<Author				: -   Vijay Joshi
<Start Date				: -	6/9/2005 12:32:03 PM
<End Date				: -	
<Description				: - 	Business logic for add journal entry detail screen.
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
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using Cms.Model.Account;

namespace Cms.BusinessLayer.BlAccount
{
	/// <summary>
	/// Business logic for add journal entry detail screen.
	/// </summary>
	public class ClsJournalEntryDetail : Cms.BusinessLayer.BlAccount.ClsAccount,IDisposable
	{
		private const	string		ACT_JOURNAL_LINE_ITEMS			=	"ACT_JOURNAL_LINE_ITEMS";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
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
		public ClsJournalEntryDetail()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= "";
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objJournalEntryDetail">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsJournalEntryDetailInfo objJournalEntryDetail)
		{
			string		strStoredProc	=	"Proc_InsertACT_JOURNAL_LINE_ITEMS";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@JOURNAL_ID",objJournalEntryDetail.JOURNAL_ID);
				objDataWrapper.AddParameter("@DIV_ID",objJournalEntryDetail.DIV_ID);
				objDataWrapper.AddParameter("@DEPT_ID",objJournalEntryDetail.DEPT_ID);
				objDataWrapper.AddParameter("@PC_ID",objJournalEntryDetail.PC_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objJournalEntryDetail.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objJournalEntryDetail.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objJournalEntryDetail.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@AMOUNT",objJournalEntryDetail.AMOUNT);
				objDataWrapper.AddParameter("@TYPE",objJournalEntryDetail.TYPE);
				objDataWrapper.AddParameter("@REGARDING",objJournalEntryDetail.REGARDING);

				if (objJournalEntryDetail.REF_CUSTOMER != 0)
					objDataWrapper.AddParameter("@REF_CUSTOMER",objJournalEntryDetail.REF_CUSTOMER);
				else
					objDataWrapper.AddParameter("@REF_CUSTOMER",null);

				objDataWrapper.AddParameter("@ACCOUNT_ID",objJournalEntryDetail.ACCOUNT_ID);
				objDataWrapper.AddParameter("@BILL_TYPE",objJournalEntryDetail.BILL_TYPE);
				objDataWrapper.AddParameter("@NOTE",objJournalEntryDetail.NOTE);
				objDataWrapper.AddParameter("@POLICY_NUMBER",objJournalEntryDetail.POLICY_NUMBER);
				objDataWrapper.AddParameter("@CREATED_BY",objJournalEntryDetail.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objJournalEntryDetail.CREATED_DATETIME);
				objDataWrapper.AddParameter("@TRAN_CODE",objJournalEntryDetail.TRAN_CODE);
				//Added to Checj Bill Type
				SqlParameter objRetVal = (SqlParameter) objDataWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@JE_LINE_ITEM_ID",objJournalEntryDetail.JE_LINE_ITEM_ID,SqlDbType.Int,ParameterDirection.Output);
             
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objJournalEntryDetail.TransactLabel = ClsCommon.MapTransactionLabel("/Account/Aspx/AddJournalEntryDetail.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objJournalEntryDetail);
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='POLICY_ID']");
					if(strTranXML!="")
                        strTranXML = GetReferenceCustomerName(strTranXML,objJournalEntryDetail.TYPE);  //Modified:
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					// Commented : The entry didn't appear in Tran Log as the condition failed in case of
					// 'calledfrom = MNT' in Tran Log Index page. The tran log entry will have 0 in all IDS's
					// although, entries of policy id will be sent in MNT_TRANSACTION_XML.
					//objTransactionInfo.POLICY_ID		=	objJournalEntryDetail.POLICY_ID;
					//objTransactionInfo.POLICY_VER_TRACKING_ID = objJournalEntryDetail.POLICY_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objJournalEntryDetail.CREATED_BY;
                    objTransactionInfo.TRANS_DESC       = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1753", "");// "Journal Entry Detail has been added.";
					objTransactionInfo.CUSTOM_INFO      =   GetJournalNoType(objJournalEntryDetail.JOURNAL_ID);
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				objJournalEntryDetail.JE_LINE_ITEM_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				if (int.Parse(objRetVal.Value.ToString()) == -5)//ADDED BILL TYPE CHECK
					return -5;
				else
					return returnResult;
				
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
		#region
		/// <summary>
		/// 
		/// </summary>
		/// <param name="transXML"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public string GetReferenceCustomerName(string transXML,string type)
		{
			System.Xml.XmlDocument transDoc = new System.Xml.XmlDocument();
			string regardingName = "";
			int regardingID = 0;

			string insuredName = "";
			int insuredID = 0;
			string[] strArray = new string[4];
            
			try
			{
				transDoc.LoadXml(transXML);
				if(transDoc!=null)
				{
					if(type!=null)
					{
						if(type.Equals("AGN"))
						{
							//Agency name :REGARDING
							XmlNode nodeREGARDING = transDoc.SelectSingleNode("//Map[@field='REGARDING']");
							if(nodeREGARDING!=null)
							{
								XmlAttribute attrREGARDING = nodeREGARDING.Attributes["label"];
								attrREGARDING.InnerText = "Agency Name";
							}
							//Insured Name : REF_CUSTOMER
							XmlNode nodeREF_CUSTOMER = transDoc.SelectSingleNode("//Map[@field='REF_CUSTOMER']");
							if(nodeREF_CUSTOMER!=null)
							{
								XmlAttribute attrREF_CUSTOMER = nodeREF_CUSTOMER.Attributes["label"];
								attrREF_CUSTOMER.InnerText = "Insured Name";
							}
							//ADDED TO SHOW AGENCY DETAILS IN TRANS LOG
							#region Regarding ID
                            if(nodeREGARDING!=null)
							{
								XmlAttribute attrREGARDING_OLD = nodeREGARDING.Attributes["OldValue"];
								XmlAttribute attrREGARDING_NEW = nodeREGARDING.Attributes["NewValue"];
								//Save Case
								if(attrREGARDING_NEW!=null)
								{
									regardingID = int.Parse(attrREGARDING_NEW.InnerText.ToString());
									insuredName = FetchJournalEntryInfo(type,regardingID,insuredID);
									strArray = insuredName.Split('^');
									if(strArray[0]!="")
										attrREGARDING_NEW.InnerText = strArray[0];

								}
								//Update Case
								if(attrREGARDING_OLD!=null)
								{
									if(attrREGARDING_OLD.InnerText!="")
									{
										regardingID = int.Parse(attrREGARDING_OLD.InnerText.ToString());
										insuredName = FetchJournalEntryInfo(type,regardingID,insuredID);
										strArray = insuredName.Split('^');
										if(strArray[0]!="")
											attrREGARDING_OLD.InnerText = strArray[0];
									}

								}

																
							}
							#endregion END Regarding
							#region Insured ID
                             
							if(nodeREF_CUSTOMER!=null)
							{
								XmlAttribute attrREF_CUSTOMER_OLD = nodeREF_CUSTOMER.Attributes["OldValue"];
								XmlAttribute attrREF_CUSTOMER_NEW = nodeREF_CUSTOMER.Attributes["NewValue"];
								//Save Case
								if(attrREF_CUSTOMER_NEW!=null)
								{
									insuredID = int.Parse(attrREF_CUSTOMER_NEW.InnerText.ToString());
									insuredName = FetchJournalEntryInfo(type,regardingID,insuredID);
									strArray = insuredName.Split('^');
									if(strArray[1]!="")
										attrREF_CUSTOMER_NEW.InnerText = strArray[1];

								}
								//Update Case
								if(attrREF_CUSTOMER_OLD!=null)
								{
									if(attrREF_CUSTOMER_OLD.InnerText!="")
									{
										//Null check Added By Raghav For Itrack #5076 
										if(attrREF_CUSTOMER_OLD.InnerText!="null")
										{ 
											insuredID = int.Parse(attrREF_CUSTOMER_OLD.InnerText.ToString());
											insuredName = FetchJournalEntryInfo(type,regardingID,insuredID);
											strArray = insuredName.Split('^');
											if(strArray[1]!="")
												attrREF_CUSTOMER_OLD.InnerText =  strArray[1];
										}
									}

								}
							}
							#endregion END Insured
						}
						if(type.Equals("CUS"))
						{
							//Insured Name : regarding
							XmlNode nodeREGARDING = transDoc.SelectSingleNode("//Map[@field='REGARDING']");
							if(nodeREGARDING!=null)
							{
								XmlAttribute attrREGARDING = nodeREGARDING.Attributes["label"];
								attrREGARDING.InnerText = "Insured Name";
							}

							//Remove REF_CUSTOMER
							XmlNode nodeREF_CUSTOMER = transDoc.SelectSingleNode("//Map[@field='REF_CUSTOMER']");
							if(nodeREF_CUSTOMER!=null)
                                transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(nodeREF_CUSTOMER);	
							//ADDED TO GET TRANS LOG ON 21 JAN 08
							if(nodeREGARDING!=null)
							{
								XmlAttribute attrREGARDING_OLD = nodeREGARDING.Attributes["OldValue"];
								XmlAttribute attrREGARDING_NEW = nodeREGARDING.Attributes["NewValue"];
								//Save Case
								if(attrREGARDING_NEW!=null)
								{
									regardingID = int.Parse(attrREGARDING_NEW.InnerText.ToString());
									regardingName = FetchJournalEntryInfo(type,regardingID,0);
									if(regardingName!="")
										attrREGARDING_NEW.InnerText = regardingName;

								}
								//Update Case
								if(attrREGARDING_OLD!=null)
								{
									if(attrREGARDING_OLD.InnerText!="")
									{
										regardingID = int.Parse(attrREGARDING_OLD.InnerText.ToString());
										regardingName = FetchJournalEntryInfo(type,regardingID,0);
										if(regardingName!="")
											attrREGARDING_OLD.InnerText = regardingName;
									}

								}
							}
						}
						if(type.Equals("VEN"))
						{
							//Insured Name : REF_CUSTOMER
							XmlNode nodeREF_CUSTOMER = transDoc.SelectSingleNode("//Map[@field='REGARDING']");
							if(nodeREF_CUSTOMER!=null)
							{
								XmlAttribute attrREF_CUSTOMER = nodeREF_CUSTOMER.Attributes["label"];
								attrREF_CUSTOMER.InnerText = "Vendor Name";
							}

							//Remove regarding
							XmlNode nodeREF_CUSTOMERVEN = transDoc.SelectSingleNode("//Map[@field='REF_CUSTOMER']");
							if(nodeREF_CUSTOMERVEN!=null)
                                transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(nodeREF_CUSTOMERVEN);	

							//GET VENDOR ID
							//Fetch Vendot ID for Old in case of update
							//Fetch Vendot ID for new in case of update
							//Insured Name : REF_CUSTOMER
							if(nodeREF_CUSTOMER!=null)
							{
								XmlAttribute attrREF_CUSTOMER_OLD = nodeREF_CUSTOMER.Attributes["OldValue"];
								XmlAttribute attrREF_CUSTOMER_NEW = nodeREF_CUSTOMER.Attributes["NewValue"];
								//Save Case
								if(attrREF_CUSTOMER_NEW!=null)
								{
									regardingID = int.Parse(attrREF_CUSTOMER_NEW.InnerText.ToString());
									regardingName = FetchJournalEntryInfo(type,regardingID,0);
									if(regardingName!="")
										attrREF_CUSTOMER_NEW.InnerText = regardingName;

								}
								//Update Case
								if(attrREF_CUSTOMER_OLD!=null)
								{
									if(attrREF_CUSTOMER_OLD.InnerText!="")
									{
										regardingID = int.Parse(attrREF_CUSTOMER_OLD.InnerText.ToString());
										regardingName = FetchJournalEntryInfo(type,regardingID,0);
										if(regardingName!="")
											attrREF_CUSTOMER_OLD.InnerText = regardingName;
									}
								}
							}
						}
					}
				}

			
				return transDoc.OuterXml;

                
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{}			

		}
		#endregion

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldJournalEntryDetail">Model object having old information</param>
		/// <param name="objJournalEntryDetail">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsJournalEntryDetailInfo objOldJournalEntryDetail,ClsJournalEntryDetailInfo objJournalEntryDetail)
		{
			string		strStoredProc	=	"Proc_UpdateACT_JOURNAL_LINE_ITEMS";
			string strTranXML;
		//	int strjournalno =Convert.ToInt32(ClsJournalEntryMaster.GetMaxEntryNo().ToString()) - 1;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@JOURNAL_ID",objJournalEntryDetail.JOURNAL_ID);
				objDataWrapper.AddParameter("@DIV_ID",objJournalEntryDetail.DIV_ID);
				objDataWrapper.AddParameter("@DEPT_ID",objJournalEntryDetail.DEPT_ID);
				objDataWrapper.AddParameter("@PC_ID",objJournalEntryDetail.PC_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objJournalEntryDetail.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objJournalEntryDetail.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objJournalEntryDetail.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@AMOUNT",objJournalEntryDetail.AMOUNT);
				objDataWrapper.AddParameter("@TYPE",objJournalEntryDetail.TYPE);
				objDataWrapper.AddParameter("@REGARDING",objJournalEntryDetail.REGARDING);
				
				if (objJournalEntryDetail.REF_CUSTOMER != 0)
					objDataWrapper.AddParameter("@REF_CUSTOMER",objJournalEntryDetail.REF_CUSTOMER);
				else
					objDataWrapper.AddParameter("@REF_CUSTOMER",null);

				objDataWrapper.AddParameter("@ACCOUNT_ID",objJournalEntryDetail.ACCOUNT_ID);
				objDataWrapper.AddParameter("@BILL_TYPE",objJournalEntryDetail.BILL_TYPE);
				objDataWrapper.AddParameter("@NOTE",objJournalEntryDetail.NOTE);
				objDataWrapper.AddParameter("@POLICY_NUMBER",objJournalEntryDetail.POLICY_NUMBER);
				objDataWrapper.AddParameter("@MODIFIED_BY",objJournalEntryDetail.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objJournalEntryDetail.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@JE_LINE_ITEM_ID",objJournalEntryDetail.JE_LINE_ITEM_ID);
				objDataWrapper.AddParameter("@TRAN_CODE",objJournalEntryDetail.TRAN_CODE);

				SqlParameter objRetVal = (SqlParameter) objDataWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);

				if(TransactionLogRequired) 
				{
					objJournalEntryDetail.TransactLabel = ClsCommon.MapTransactionLabel("/Account/Aspx/AddJournalEntryDetail.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldJournalEntryDetail,objJournalEntryDetail);
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='POLICY_ID']");
					if(strTranXML!="")
                        strTranXML = GetReferenceCustomerName(strTranXML,objJournalEntryDetail.TYPE);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					// Commented : The entry didn't appear in Tran Log as the condition failed in case of
					// 'calledfrom = MNT' in Tran Log Index page.
					//objTransactionInfo.POLICY_ID		=	objJournalEntryDetail.POLICY_ID;
					//objTransactionInfo.POLICY_VER_TRACKING_ID	=	objJournalEntryDetail.POLICY_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objJournalEntryDetail.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1754", "");// "Journal Entry Detail has been modified";
					objTransactionInfo.CUSTOM_INFO      =   GetJournalNoType(objJournalEntryDetail.JOURNAL_ID);
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
				if (int.Parse(objRetVal.Value.ToString()) == -8)//ADDED FOR POLICY AND AGENCY CHECK
				{
					return -8;
				}
				if (int.Parse(objRetVal.Value.ToString()) == -5)//ADDED BILL TYPE CHECK
				{
					return -5;
				}
				if (int.Parse(objRetVal.Value.ToString()) == -7)//ADDED FOR POLICY AND CUSTOMER CHECK
				{
					return -7;
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

		#region GetJournalEntryDetailInfo

		public static string GetJournalEntryDetailInfo(int intJournalLineItemId )
		{
			String strStoredProc = "Proc_GetACT_JOURNAL_LINE_ITEMS";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@JE_LINE_ITEM_ID", intJournalLineItemId);
				
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
		#region Get Journal Details for TL
		public static string FetchJournalEntryInfo (string  type,int regardingId,int insuredId)
		{
			String strStoredProc = "Proc_FetchJournalEntryInfo";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@TYPE", type);
				objDataWrapper.AddParameter("@REGARDING_ID", regardingId);
				objDataWrapper.AddParameter("@INSURED_ID", insuredId);
				
				ds = objDataWrapper.ExecuteDataSet(strStoredProc);
				
				if (ds.Tables[0].Rows.Count != 0)
				{
					return ds.Tables[0].Rows[0]["REGARDING_NAME"].ToString();
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

		public string GetJournalNoType(int journalID)
		{
			string	strStoredProc =	"Proc_FetchJOurnalEntryNoType";

			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@JOURNAL_ID",journalID);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			try
			{
				return ds.Tables[0].Rows[0]["CUST_INFO"].ToString();
			}
			catch
			{
				return "";
			}

		}

		#region Delete
		/// <summary>
		/// Delete the whole record of spefied id 
		/// </summary>
		/// <returns>Nos of rows deleted</returns>
		public int Delete(int intJournalId, int intLineItemId,ClsJournalEntryDetailInfo objJournalEntryDetailInfo)
		{
			try
			{
				/*Calling the stored procedure to get the maximum Journal entry no*/
				String strStoredProc = "Proc_DeleteACT_JOURNAL_LINE_ITEMS";
				int Value;
               // int strjournalno =Convert.ToInt32(ClsJournalEntryMaster.GetMaxEntryNo().ToString()) - 1;
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
				objDataWrapper.AddParameter("@JOURNAL_ID", intJournalId);
				objDataWrapper.AddParameter("@JE_LINE_ITEM_ID", intLineItemId);
				SqlParameter objRetVal = (SqlParameter) objDataWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);
				if(TransactionLogRequired) 
				{
					objJournalEntryDetailInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/Aspx/AddJournalEntryDetail.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objJournalEntryDetailInfo);
					//Added to get Trans log details when deleted line item
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='POLICY_ID']");
					if(strTranXML!="")
						strTranXML = GetReferenceCustomerName(strTranXML,objJournalEntryDetailInfo.TYPE);
					//
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
                    objTransactionInfo.TRANS_DESC       = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1755", "");// "Journal Entry Detail has been deleted.";
					objTransactionInfo.RECORDED_BY		=	objJournalEntryDetailInfo.CREATED_BY;
					objTransactionInfo.CHANGE_XML		= strTranXML;
					objTransactionInfo.CUSTOM_INFO      =   GetJournalNoType(intJournalId);
					Value = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
				}
				else
					Value = objDataWrapper.ExecuteNonQuery(strStoredProc);

				Value = int.Parse(objRetVal.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();

				return Value ;
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		}
		#endregion


	}
}
