/******************************************************************************************
<Author					: - Vijay Joshi
<Start Date				: -	6/22/2005 7:09:49 PM
<End Date				: -	
<Description			: - 	
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
using System.Collections;


namespace Cms.BusinessLayer.BlAccount
{
	/// <summary>
	/// Summary description for ClsReconDetail.
	/// </summary>
	public class ClsReconDetail : Cms.BusinessLayer.BlAccount.ClsAccount
	{
		private const string INSERT_PROC = "Proc_InsertACT_CUSTOMER_RECON_GROUP_DETAILS";
		private const string UPDATE_PROC = "Proc_UpdateACT_CUSTOMER_RECON_GROUP_DETAILS";
		private const string DELETE_PROC = "Proc_DeleteACT_CUSTOMER_RECON_GROUP_DETAILS";
		
		private const string CHECK_INSERT_PROC = "Proc_Insert_CHECK_ACT_RECON_GROUP_DETAILS";
		private const string CHECK_UPDATE_PROC = "Proc_Update_CHECK_ACT_RECON_GROUP_DETAILS";
		private const string CHECK_DELETE_PROC = "Proc_Delete_CHECK_ACT_RECON_GROUP_DETAILS";

		private DataWrapper objWrapper=null;

		//---Praveen kUmar
		public string policyNumber;
		public string customerName;
		public string entityType;
		public int commitedby;
		public string entityID;
		//---

		public ClsReconDetail()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		

		public bool SaveReconDetails(ArrayList al,ArrayList alPolicy,string EntityType,string EntityId,int commitedBy)
		{
			objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
			DataSet ds = new DataSet();  //Added Praveen Kumar Itrack:4946			 
			commitedby = commitedBy;
			entityID = EntityId;


			try
			{
				for(int i = 0; i < al.Count; i++ )
				{
					ClsReconDetailInfo objReconDetails = (ClsReconDetailInfo)al[i];					
				 
					if(objReconDetails.IDEN_ROW_NO == -1)
					{
						objWrapper.ClearParameteres();
						objWrapper.AddParameter("@IDEN_ROW_NO",0);
						objWrapper.AddParameter("@ENTITY_TYPE",EntityType);
						objWrapper.AddParameter("@ENTITY_TYPE_ID",EntityId); 
						//Added Praveen Kumar Itrack:4946
						//objWrapper.AddParameter("@CUSTOMERID",EntityId);
						ds = objWrapper.ExecuteDataSet("PROC_FETCH_RECON_AMOUNT");
						if(ds.Tables[0].Rows.Count > 0)
						{ 							
						      customerName = ds.Tables[0].Rows[0]["ENTITY_NAME"].ToString();
						}
						entityType = EntityType;
						policyNumber = (string)alPolicy[i];
						objWrapper.ClearParameteres();						
						objReconDetails.ITEM_TYPE = EntityType;  
						Add(objReconDetails, objWrapper);	
					}
					else
					{
						objWrapper.ClearParameteres();
						objWrapper.AddParameter("@IDEN_ROW_NO",0);
						objWrapper.AddParameter("@ENTITY_TYPE",EntityType);
						objWrapper.AddParameter("@ENTITY_TYPE_ID",EntityId); 
						//Added Praveen Kumar Itrack:4946
						//objWrapper.AddParameter("@CUSTOMERID",EntityId);
						ds = objWrapper.ExecuteDataSet("PROC_FETCH_RECON_AMOUNT");
						if(ds.Tables[0].Rows.Count > 0)
						{ 							
							customerName = ds.Tables[0].Rows[0]["ENTITY_NAME"].ToString();
						}
						entityType = EntityType;
						policyNumber = (string)alPolicy[i];
						objWrapper.ClearParameteres();						
						objReconDetails.ITEM_TYPE=EntityType;
						Update(objReconDetails,objWrapper);
					}
				}
				//Commiting the transaction
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
			catch(Exception ex)
			{
				throw(ex);				
			}

			return true;
		}

		
		public bool DeleteReconDetails(ArrayList al,ArrayList alPolicy,int commitedBy,string EntityId,string strEntityType)
		{
			objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
			DataSet ds = new DataSet();//Added itrack:4946
			commitedby = commitedBy;

			try
			{
				for(int i = 0; i < al.Count; i++ )
				{
					//ADDED Praveen Kumar:Itrack 4946
					policyNumber = (string)alPolicy[i];
					
					/*objWrapper.AddParameter("@CUSTOMERID",EntityId);
					ds = objWrapper.ExecuteDataSet("Proc_GetCustomerInfo");
					customerName = ds.Tables[0].Rows[0][0].ToString()+ds.Tables[0].Rows[0][1].ToString()+ds.Tables[0].Rows[0][2].ToString();
					objWrapper.ClearParameteres();	*/
					//End Praveen Kumar

					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@IDEN_ROW_NO",0);
					objWrapper.AddParameter("@ENTITY_TYPE",strEntityType);
					objWrapper.AddParameter("@ENTITY_TYPE_ID",EntityId); 
					//Added Praveen Kumar Itrack:4946
					//objWrapper.AddParameter("@CUSTOMERID",EntityId);
					ds = objWrapper.ExecuteDataSet("PROC_FETCH_RECON_AMOUNT");
					if(ds.Tables[0].Rows.Count > 0)
					{ 							
						customerName = ds.Tables[0].Rows[0]["ENTITY_NAME"].ToString();
					}
					ClsReconDetailInfo objReconDetails = (ClsReconDetailInfo)al[i];
					objWrapper.ClearParameteres();
					if(! DeleteReconDetails(objReconDetails,strEntityType,objWrapper))
					{
						objWrapper.RollbackTransaction (DataWrapper.CloseConnection.YES);
						return false;
					}
				}
				//Commiting the transaction
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);				
			}

			return true;
		}
		public bool DeleteReconDetails(ClsReconDetailInfo objReconDetails,string strEntityType ,DataWrapper objWrapper)
		{
			try
			{
				objWrapper.AddParameter("@RECON_IDEN_ID", objReconDetails.IDEN_ROW_NO );
				objWrapper.AddParameter("@ENTITY_TYPE", strEntityType);
				DataSet ds = FetchReconAmount(objReconDetails.IDEN_ROW_NO,strEntityType);
				int RetVal = 0;
				if(TransactionLogRequired) 
				{
					//objReconGroupInfo.TransactLabel = ClsCommon.MapTransactionLabel("/account/aspx/ReconDetail.aspx.resx");
					//ADDED BY PRAVEEN KUMAR(09-03-2009):ITRACK 4946
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.RECORDED_BY = commitedby;
					if(strEntityType == ReconEntityType.CUST.ToString())
					{
						objTransactionInfo.TRANS_DESC		=	"Customer Reconciliation items have been deleted.";
						objTransactionInfo .CUSTOM_INFO =       ";Customer Name = " + customerName+";Policy # = "+ policyNumber +";Amount to apply = " + ds.Tables[0].Rows[0]["RECON_AMOUNT"].ToString();					
					
					}
					else if (strEntityType == ReconEntityType.AGN.ToString())
					{
						objTransactionInfo.TRANS_DESC		=	"Agency Reconciliation items have been deleted.";
						objTransactionInfo .CUSTOM_INFO     =   ";Agency Name = " + customerName+";Policy # = "+ policyNumber +";Amount to apply = " + ds.Tables[0].Rows[0]["RECON_AMOUNT"].ToString();					
					}
					else if (strEntityType == ReconEntityType.VEN.ToString())
					{
						objTransactionInfo.TRANS_DESC		=	"Vendor Reconciliation items have been deleted.";								
                        objTransactionInfo .CUSTOM_INFO =       ";Vendor Name = " + customerName+";Amount to apply = " + ds.Tables[0].Rows[0]["RECON_AMOUNT"].ToString();					
					}										
					//END PRAVEEN KUMAR
					RetVal = objWrapper.ExecuteNonQuery("Proc_DeleteReconDetail",objTransactionInfo);
				}
				else
				{
					RetVal = objWrapper.ExecuteNonQuery("Proc_DeleteReconDetail");
				}
				
				//int RetVal = objWrapper.ExecuteNonQuery("Proc_DeleteReconDetail");
				objWrapper.ClearParameteres();

				if ( RetVal > 0)
					return true;
				else
					return false;

			}
			catch
			{
				return false;
			}
		}


		/// <summary>
		/// Fatch Vendor Name and Invoice Number
		/// </summary>
		/// <param name="venderId"></param>
		/// <returns></returns>
		public DataSet FatchDepositNoType(int CD_LINE_ITEM_ID)
		{
			string	strStoredProc =	"Proc_FetchDepositNoType";
			
			try
			{
				DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
				objWrapper.AddParameter("@CD_LINE_ITEM_ID",CD_LINE_ITEM_ID);
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

				return ds ;
			}
			catch
			{
				return null;
			}	
		}


		public bool Save(ArrayList al)
		{
			return Save(al,0,0);
		}


		public bool Save(ArrayList al,int CheckTypeID ,int CREATED_BY,int intCdLineItem)
		{
			objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
			
			//Add by kranti 
			DataSet DepositNoType = FatchDepositNoType(intCdLineItem);
			string DepositType = "";
			string DepositNo = "" ;

			if(DepositNoType.Tables[0].Rows.Count > 0)
			{
				DepositType	=	DepositNoType.Tables[0].Rows[0]["DEPOSIT_TYPE"].ToString();
				DepositNo	=	DepositNoType.Tables[0].Rows[0]["DEPOSIT_NUMBER"].ToString();				
			}	
			//end 
			

			if ( al.Count > 0)
			{
				//Deleting previous line items for this recon
				objWrapper.AddParameter("@GROUP_ID", ((ClsReconDetailInfo)al[0]).GROUP_ID);
				if(TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=  CREATED_BY ;
					objTransactionInfo.TRANS_DESC		=	"Reconciliation Group has been deleted.";
					objTransactionInfo.CUSTOM_INFO		=	"Deposit Number :" + DepositNo + ";Deposit Type :" + DepositType;
					objWrapper.ExecuteNonQuery("Proc_DeleteReconGroupDetails", objTransactionInfo);
				}
				else
				{
					objWrapper.ExecuteNonQuery("Proc_DeleteReconGroupDetails");
				}
				objWrapper.ClearParameteres();
			}


			bool blnGroupCreated = false;
			int intGroupId = 0;
			bool result=false;
			try
			{
				for(int i = 0; i < al.Count; i++ )
				{
					ClsReconDetailInfo objReconDetails = (ClsReconDetailInfo)al[i];

					
					if (objReconDetails.IDEN_ROW_NO <= 0)
					{
						//intGroupId = objReconDetails.GROUP_ID;
						if (blnGroupCreated == false && objReconDetails.GROUP_ID == 0)
						{
							//Creating the group first
							if (CreateGroup(out intGroupId) == false)
							{
								objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
								return false;
							}
							blnGroupCreated = true;

							objReconDetails.GROUP_ID = intGroupId;
						}

						
						
						if(CheckTypeID==0)
						{
							result = AddAgencyReceipt(objReconDetails, objWrapper,DepositNo,CREATED_BY);
						}
						//Added For Itrack Issue #6362. 
						else
							result = AddAgencyReceipt(objReconDetails, objWrapper,CHECK_INSERT_PROC,CheckTypeID,DepositNo,CREATED_BY);
						//Adding the group details
						if (result == false)
						{
							objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
							return false;
						}
					}
					else
					{
						if (objReconDetails.RECON_AMOUNT != 0)
						{
							if(CheckTypeID==0)
								result = Update(objReconDetails, objWrapper);
							else
								result = Update(objReconDetails, objWrapper,CHECK_UPDATE_PROC,CheckTypeID);
							if (result == false)
							{
								objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
								return false;
							}
						}
						else
						{
							if (Delete(objWrapper, objReconDetails.IDEN_ROW_NO) == false)
							{
								objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
								return false;
							}
						}
					}
				}

				//Commiting the transaction
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return true;
			}
			catch
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				return false;
			}
			finally
			{
				objWrapper.Dispose();
			}
		}
		public bool Save(ArrayList al,int CheckTypeID ,int CREATED_BY)
		{
			objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
			

			if ( al.Count > 0)
			{
				//Deleting previous line items for this recon
				objWrapper.AddParameter("@GROUP_ID", ((ClsReconDetailInfo)al[0]).GROUP_ID);
				if(TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=  CREATED_BY ;
					objTransactionInfo.TRANS_DESC		=	"Reconciliation Group has been deleted.";
					objTransactionInfo.CUSTOM_INFO		=	CheckTypeID.ToString();
					objWrapper.ExecuteNonQuery("Proc_DeleteReconGroupDetails", objTransactionInfo);
				}
				else
				{
					objWrapper.ExecuteNonQuery("Proc_DeleteReconGroupDetails");
				}
				objWrapper.ClearParameteres();
			}


			bool blnGroupCreated = false;
			int intGroupId = 0;
			bool result=false;
			try
			{
				for(int i = 0; i < al.Count; i++ )
				{
					ClsReconDetailInfo objReconDetails = (ClsReconDetailInfo)al[i];

					
					if (objReconDetails.IDEN_ROW_NO <= 0)
					{
						//intGroupId = objReconDetails.GROUP_ID;
						if (blnGroupCreated == false && objReconDetails.GROUP_ID == 0)
						{
							//Creating the group first
							if (CreateGroup(out intGroupId) == false)
							{
								objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
								return false;
							}
							blnGroupCreated = true;

							objReconDetails.GROUP_ID = intGroupId;
						}

						
						
						if(CheckTypeID==0)
						{
							result = Add(objReconDetails, objWrapper);
						}
						else
							result = Add(objReconDetails, objWrapper,CHECK_INSERT_PROC,CheckTypeID);
						//Adding the group details
						if (result == false)
						{
							objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
							return false;
						}
					}
					else
					{
						if (objReconDetails.RECON_AMOUNT != 0)
						{
							if(CheckTypeID==0)
								result = Update(objReconDetails, objWrapper);
							else
								result = Update(objReconDetails, objWrapper,CHECK_UPDATE_PROC,CheckTypeID);
							if (result == false)
							{
								objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
								return false;
							}
						}
						else
						{
							if (Delete(objWrapper, objReconDetails.IDEN_ROW_NO) == false)
							{
								objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
								return false;
							}
						}
					}
				}

				//Commiting the transaction
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return true;
			}
			catch
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				return false;
			}
			finally
			{
				objWrapper.Dispose();
			}
		}

		private bool Add(Cms.Model.Account.ClsReconDetailInfo objReconDetailInfo, DataWrapper objWrapper)
		{
			return Add(objReconDetailInfo, objWrapper,INSERT_PROC,0);
		}
		private bool Add(Cms.Model.Account.ClsReconDetailInfo objReconDetailInfo, DataWrapper objWrapper,string INSERT_PROC_NAME,int CHECK_TYPE_ID)
		{
			try
			{
				SqlParameter objSql = (SqlParameter)objWrapper.AddParameter("@IDEN_ROW_NO",SqlDbType.Int,ParameterDirection.Output);
				objWrapper.AddParameter("@GROUP_ID", objReconDetailInfo.GROUP_ID);
				objWrapper.AddParameter("@ITEM_TYPE", objReconDetailInfo.ITEM_TYPE);
				objWrapper.AddParameter("@ITEM_REFERENCE_ID", objReconDetailInfo.ITEM_REFERENCE_ID);
				objWrapper.AddParameter("@SUB_LEDGER_TYPE", objReconDetailInfo.SUB_LEDGER_TYPE);

				objWrapper.AddParameter("@RECON_AMOUNT", objReconDetailInfo.RECON_AMOUNT);
				objWrapper.AddParameter("@NOTE", objReconDetailInfo.NOTE);
				if(CHECK_TYPE_ID>0)
					objWrapper.AddParameter("@CHECK_TYPE_ID", CHECK_TYPE_ID);
				if (objReconDetailInfo.DIV_ID != 0)
					objWrapper.AddParameter("@DIV_ID", objReconDetailInfo.DIV_ID);
				else
					objWrapper.AddParameter("@DIV_ID", null);

				if (objReconDetailInfo.DEPT_ID != 0)
					objWrapper.AddParameter("@DEPT_ID", objReconDetailInfo.DEPT_ID);
				else
					objWrapper.AddParameter("@DEPT_ID", null);

				if (objReconDetailInfo.PC_ID != 0)
					objWrapper.AddParameter("@PC_ID", objReconDetailInfo.PC_ID);
				else
					objWrapper.AddParameter("@PC_ID", null);

				int RetVal = 0;
				if(TransactionLogRequired) 
				{
					//objReconGroupInfo.TransactLabel = ClsCommon.MapTransactionLabel("/account/aspx/ReconDetail.aspx.resx");
					//ADDED BY PRAVEEN KUMAR(09-03-2009):ITRACK 4946
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					objTransactionInfo.RECORDED_BY = commitedby;

					if(objReconDetailInfo.ITEM_TYPE.ToUpper().ToString() == "CUST")
					{ 
						objTransactionInfo.TRANS_DESC		=	"Customer Reconciliation items have been saved.";
						objTransactionInfo.CLIENT_ID		=	int.Parse(entityID.ToString());
					}
					else if (objReconDetailInfo.ITEM_TYPE.ToUpper().ToString() == "AGN")
					{
						objTransactionInfo.TRANS_DESC		=	"Agency Reconciliation items have been saved.";
					}
					else if(objReconDetailInfo.ITEM_TYPE.ToUpper().ToString() == "VEN")
					{
					  	objTransactionInfo.TRANS_DESC		=	"Vendor Reconciliation items have been saved.";
					}
					else if(objReconDetailInfo.ITEM_TYPE.ToUpper().ToString() == "")
					{
						objTransactionInfo.TRANS_DESC		=	"Agency Receipt Distribution has been modified.";
					}

					//Added For itrack Issue #4946.
					objTransactionInfo.CUSTOM_INFO = GetReconInfo(objReconDetailInfo.ITEM_TYPE,objReconDetailInfo.RECON_AMOUNT,0,"ADD");  
					
					RetVal = objWrapper.ExecuteNonQuery(INSERT_PROC_NAME,objTransactionInfo);
				}
				else
				{
					RetVal = objWrapper.ExecuteNonQuery(INSERT_PROC_NAME);
				}


				//int RetVal = objWrapper.ExecuteNonQuery(INSERT_PROC_NAME);
				objWrapper.ClearParameteres();

				if ( RetVal > 0)
					return true;
				else
					return false;

			}
			catch
			{
				return false;
			}
		} 
        //Added For Itrack Issue #6362  
		 private bool AddAgencyReceipt(Cms.Model.Account.ClsReconDetailInfo objReconDetailInfo, DataWrapper objWrapper,string DepositNo,int CREATED_BY)
		{
			return AddAgencyReceipt(objReconDetailInfo, objWrapper,INSERT_PROC,0,DepositNo,CREATED_BY);
		}
		private bool AddAgencyReceipt(Cms.Model.Account.ClsReconDetailInfo objReconDetailInfo, DataWrapper objWrapper,string INSERT_PROC_NAME,int CHECK_TYPE_ID,string DepositNo,int CREATED_BY)
		{
			try
			{
				SqlParameter objSql = (SqlParameter)objWrapper.AddParameter("@IDEN_ROW_NO",SqlDbType.Int,ParameterDirection.Output);
				objWrapper.AddParameter("@GROUP_ID", objReconDetailInfo.GROUP_ID);
				objWrapper.AddParameter("@ITEM_TYPE", objReconDetailInfo.ITEM_TYPE);
				objWrapper.AddParameter("@ITEM_REFERENCE_ID", objReconDetailInfo.ITEM_REFERENCE_ID);
				objWrapper.AddParameter("@SUB_LEDGER_TYPE", objReconDetailInfo.SUB_LEDGER_TYPE);

				objWrapper.AddParameter("@RECON_AMOUNT", objReconDetailInfo.RECON_AMOUNT);
				objWrapper.AddParameter("@NOTE", objReconDetailInfo.NOTE);
				if(CHECK_TYPE_ID>0)
					objWrapper.AddParameter("@CHECK_TYPE_ID", CHECK_TYPE_ID);
				if (objReconDetailInfo.DIV_ID != 0)
					objWrapper.AddParameter("@DIV_ID", objReconDetailInfo.DIV_ID);
				else
					objWrapper.AddParameter("@DIV_ID", null);

				if (objReconDetailInfo.DEPT_ID != 0)
					objWrapper.AddParameter("@DEPT_ID", objReconDetailInfo.DEPT_ID);
				else
					objWrapper.AddParameter("@DEPT_ID", null);

				if (objReconDetailInfo.PC_ID != 0)
					objWrapper.AddParameter("@PC_ID", objReconDetailInfo.PC_ID);
				else
					objWrapper.AddParameter("@PC_ID", null);

				int RetVal = 0;
				if(TransactionLogRequired) 
				{
					//objReconGroupInfo.TransactLabel = ClsCommon.MapTransactionLabel("/account/aspx/ReconDetail.aspx.resx");
					//ADDED BY PRAVEEN KUMAR(09-03-2009):ITRACK 4946
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					objTransactionInfo.RECORDED_BY =CREATED_BY; 

					if(objReconDetailInfo.ITEM_TYPE.ToUpper().ToString() == "CUST")
					{ 
						objTransactionInfo.TRANS_DESC		=	"Customer Reconciliation items have been saved.";
						objTransactionInfo.CLIENT_ID		=	int.Parse(entityID.ToString());
					}
					else if (objReconDetailInfo.ITEM_TYPE.ToUpper().ToString() == "AGN")
					{
						objTransactionInfo.TRANS_DESC		=	"Agency Reconciliation items have been saved.";
					}
					else if(objReconDetailInfo.ITEM_TYPE.ToUpper().ToString() == "VEN")
					{
					  	objTransactionInfo.TRANS_DESC		=	"Vendor Reconciliation items have been saved.";
					}
					else if(objReconDetailInfo.ITEM_TYPE.ToUpper().ToString() == "")
					{
						objTransactionInfo.TRANS_DESC		=	"Agency Receipt Distribution has been modified.";
					}
					
					//Added For itrack Issue #4946.
					objTransactionInfo.CUSTOM_INFO = GetAgencyRecon(objReconDetailInfo.ITEM_TYPE,DepositNo,objReconDetailInfo.RECON_AMOUNT,0,"ADD");  
					
					RetVal = objWrapper.ExecuteNonQuery(INSERT_PROC_NAME,objTransactionInfo);
				}
				else
				{
					RetVal = objWrapper.ExecuteNonQuery(INSERT_PROC_NAME);
				}


				//int RetVal = objWrapper.ExecuteNonQuery(INSERT_PROC_NAME);
				objWrapper.ClearParameteres();

				if ( RetVal > 0)
					return true;
				else
					return false;

			}
			catch
			{
				return false;
			}
		} 
	 

		private bool Update(Cms.Model.Account.ClsReconDetailInfo objReconDetailInfo, DataWrapper objWrapper )
		{
			return Update(objReconDetailInfo, objWrapper,UPDATE_PROC,0);
		}
		private bool Update(Cms.Model.Account.ClsReconDetailInfo objReconDetailInfo, DataWrapper objWrapper,string UPDATE_PROC_NAME,int CHECK_TYPE_ID )		{
			try
			{
				//objWrapper.ClearParameteres();
				XmlDocument tranXml = new XmlDocument();
				objWrapper.AddParameter("@IDEN_ROW_NO", objReconDetailInfo.IDEN_ROW_NO);
				objWrapper.AddParameter("@GROUP_ID", objReconDetailInfo.GROUP_ID);
				objWrapper.AddParameter("@ITEM_TYPE", objReconDetailInfo.ITEM_TYPE);
				objWrapper.AddParameter("@ITEM_REFERENCE_ID", objReconDetailInfo.ITEM_REFERENCE_ID);
				objWrapper.AddParameter("@SUB_LEDGER_TYPE", objReconDetailInfo.SUB_LEDGER_TYPE);

				objWrapper.AddParameter("@RECON_AMOUNT", objReconDetailInfo.RECON_AMOUNT);
				objWrapper.AddParameter("@NOTE", objReconDetailInfo.NOTE);

			

				if (objReconDetailInfo.DIV_ID != 0)
					objWrapper.AddParameter("@DIV_ID", objReconDetailInfo.DIV_ID);
				else
					objWrapper.AddParameter("@DIV_ID", null);

				if (objReconDetailInfo.DEPT_ID != 0)
					objWrapper.AddParameter("@DEPT_ID", objReconDetailInfo.DEPT_ID);
				else
					objWrapper.AddParameter("@DEPT_ID", null);

				if (objReconDetailInfo.PC_ID != 0)
					objWrapper.AddParameter("@PC_ID", objReconDetailInfo.PC_ID);
				else
					objWrapper.AddParameter("@PC_ID", null);

				if(CHECK_TYPE_ID>0)
					objWrapper.AddParameter("@CHECK_TYPE_ID", CHECK_TYPE_ID);

				int RetVal = 0;
				if(TransactionLogRequired) 
				{
					//objReconGroupInfo.TransactLabel = ClsCommon.MapTransactionLabel("/account/aspx/ReconDetail.aspx.resx");
					//ADDED BY PRAVEEN KUMAR(09-03-2009):ITRACK 4946
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					double reconAmount = double.Parse(objReconDetailInfo.RECON_AMOUNT.ToString());	

					//Amount Values for Transaction Log
				    
					DataSet ds = FetchReconAmount(objReconDetailInfo.IDEN_ROW_NO,objReconDetailInfo.ITEM_TYPE);
					string  recon_amount = ds.Tables[0].Rows[0]["RECON_AMOUNT"].ToString();  
                     //customerName = ds.Tables[0].Rows[0]["ENTITY_NAME"].ToString();   

					objTransactionInfo.RECORDED_BY = commitedby;

					if(objReconDetailInfo.ITEM_TYPE.ToUpper().ToString() == "CUST")
					{ 
						objTransactionInfo.TRANS_DESC		=	"Customer Reconciliation items have been updated.";
						objTransactionInfo.CLIENT_ID		=	int.Parse(entityID.ToString());
					}
					else if (objReconDetailInfo.ITEM_TYPE.ToUpper().ToString() == "AGN")
					{
						objTransactionInfo.TRANS_DESC		=	"Agency Reconciliation items have been updated.";
					}
					else if(objReconDetailInfo.ITEM_TYPE.ToUpper().ToString() == "VEN")
					{
						objTransactionInfo.TRANS_DESC		=	"Vendor Reconciliation items have been updated.";
					}
					//objTransactionInfo .CUSTOM_INFO =       ";Customer Name = " + customerName+";Policy # = "+ policyNumber +";Amount Modified from  " + String.Format("{0:0,0.00}",recon_amount) + " to "  +  String.Format("{0:0,0.00}",objReconDetailInfo.RECON_AMOUNT);
					
                       					
					objTransactionInfo.CUSTOM_INFO = GetReconInfo(objReconDetailInfo.ITEM_TYPE,double.Parse(recon_amount),objReconDetailInfo.RECON_AMOUNT,"UPDATE");  
					
						
					
					//END PRAVEEN KUMAR
					RetVal = objWrapper.ExecuteNonQuery(UPDATE_PROC_NAME,objTransactionInfo);
				}
				else
				{
					RetVal = objWrapper.ExecuteNonQuery(UPDATE_PROC_NAME);
				}


				//int RetVal = objWrapper.ExecuteNonQuery(UPDATE_PROC_NAME);
				objWrapper.ClearParameteres();

				if ( RetVal > 0)
					return true;
				else
					return false;

			}
			catch
			{
				return false;
			}
		}


		/// <summary>
		/// This function returns the customer open items dataset
		/// </summary>
		/// <param name="CustomerId">Id of customer</param>
		/// <param name="currentPageIndex">Current Index of page</param>
		/// <param name="pageSize">Total page size</param>
		/// <returns>Dataset</returns>
		public DataSet GetOpenItems(int EntityID, int GroupID,string entityType)
		{

			string	strStoredProc= "";
			if(entityType == ReconEntityType.CUST.ToString())
				strStoredProc =	"Proc_GetCustomer_Open_Items";
			else if (entityType == ReconEntityType.AGN.ToString())
				strStoredProc =	"Proc_GetAgency_Open_Items";
			else if (entityType == ReconEntityType.VEN.ToString())
				strStoredProc =	"Proc_Get_Vendor_Open_Items";

			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
						objWrapper.AddParameter("@ENTITY_ID", EntityID );
			if (GroupID != 0)
			{
				objWrapper.AddParameter("@RECON_GROUP_ID", GroupID);
			}
			else
			{
				objWrapper.AddParameter("@RECON_GROUP_ID", null);
			}
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			return ds;
		}

		public DataSet FetchReconAmount(int idenRowNo,string entity_type)
		{

			string strProc = "Proc_Fetch_Recon_Amount";			
			DataSet ds = null ; 
			try
			{

				DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
				objWrapper.AddParameter("@IDEN_ROW_NO", idenRowNo );		
			    objWrapper.AddParameter("@ENTITY_TYPE",entity_type);  	
				 ds = objWrapper.ExecuteDataSet(strProc); 					
				
				
			}
			catch
			{
			}
			return ds;
		}
		#region "Check - items to be paid"
		/// <summary>
		/// added by : Ajit on 19/7/2005
		/// This function returns the customer/vendor/tax/agency/claim open items dataset
		/// depending on check type.
		/// </summary>
		/// <param name="CustomerId">Id of Entity</param>
		/// <param name="currentPageIndex">Current Index of page</param>
		/// <param name="pageSize">Total page size</param>
		/// <returns>Dataset</returns>
		public DataSet GetCheckOpenItems(int EntityId, int CD_LINE_ITEM_ID, int currentPageIndex,int pageSize,string CHECK_TYPE_ID)
		{
			string	strStoredProc =	"Proc_GetEntity_Open_Items_For_Check";

			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
			
			
			objWrapper.AddParameter("@EntityId", EntityId);
			objWrapper.AddParameter("@PAGE_SIZE", pageSize);
			objWrapper.AddParameter("@CD_LINE_ITEM_ID", CD_LINE_ITEM_ID);
			objWrapper.AddParameter("@CURRENT_PAGE_INDEX", currentPageIndex);
			objWrapper.AddParameter("@CHECK_TYPE_ID", CHECK_TYPE_ID);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
			#region "deleted"
//			//"2474" /*Return Premium Checks*/
//			//"9936" :/*Return Suspense Checks*/
//			//"9935" /*Return Over Payments/*/
//			if(CheckTypeId =  "2474" ||CheckTypeId = "9936" || CheckTypeId = "9935" )
//			{//Customer Checks
//				return GetCustomerOpenItems(EntityId,CD_LINE_ITEM_ID,currentPageIndex,pageSize);
//			}
//			if(CheckTypeId =  "2472")//Agency Commission Checks
//			{//Agency checks
//				GetAgencyOpenItems(EntityId,CD_LINE_ITEM_ID,currentPageIndex,pageSize);
//			}	
//			if(CheckTypeId =  "	")//Vendor Checks
//			{
//				GetVendorOpenItems(EntityId,CD_LINE_ITEM_ID,currentPageIndex,pageSize);
//			}
//			if(CheckTypeId =  "9939")//Tax Payment Checks
//			{
//				GetTaxOpenItems(EntityId,CD_LINE_ITEM_ID,currentPageIndex,pageSize);
//			}
//			if(CheckTypeId =  "9937")//Claims Checks
//			{//deffered till clarification
//				GetClaimOpenItems(EntityId,CD_LINE_ITEM_ID,currentPageIndex,pageSize);
//			}
		#endregion
		}
		#region "deleted"
		/*
		/// <summary>
		/// added by : Ajit on 19/7/2005
		/// This function returns the customer/vendor/tax/agency open items dataset
		/// depending on check type.
		/// </summary>
		/// <param name="CustomerId">Id of Entity</param>
		/// <param name="currentPageIndex">Current Index of page</param>
		/// <param name="pageSize">Total page size</param>
		/// <returns>Dataset</returns>
		private DataSet GetCustomerOpenItems(int CustomerId, int CD_LINE_ITEM_ID, int currentPageIndex,int pageSize)
		{
			return GetOpenItems(CustomerId,CD_LINE_ITEM_ID,currentPageIndex,pageSize);
		}
		/// <summary>
		/// added by : Ajit on 19/7/2005
		/// This function returns the customer/vendor/tax/agency open items dataset
		/// depending on check type.
		/// </summary>
		/// <param name="CustomerId">Id of Entity</param>
		/// <param name="currentPageIndex">Current Index of page</param>
		/// <param name="pageSize">Total page size</param>
		/// <returns>Dataset</returns>
		private DataSet GetAgencyOpenItems(int AgencyId, int CD_LINE_ITEM_ID, int currentPageIndex,int pageSize)
		{
			try
			{
				string	strStoredProc =	"Proc_GetAgency_Open_Items";

				DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
			
			
				objWrapper.AddParameter("@AgencyId", AgencyId);
				objWrapper.AddParameter("@PAGE_SIZE", pageSize);

				
				objWrapper.AddParameter("@CD_LINE_ITEM_ID", null);
				

				objWrapper.AddParameter("@CURRENT_PAGE_INDEX", currentPageIndex);

				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
				return ds;
			}
			catch(Exception ex)
			{
				throw ex;
			}

		}
		/// <summary>
		/// added by : Ajit on 19/7/2005
		/// This function returns the customer/vendor/tax/agency open items dataset
		/// depending on check type.
		/// </summary>
		/// <param name="CustomerId">Id of Entity</param>
		/// <param name="currentPageIndex">Current Index of page</param>
		/// <param name="pageSize">Total page size</param>
		/// <returns>Dataset</returns>
		private DataSet GetVendorOpenItems(int VendorId, int CD_LINE_ITEM_ID, int currentPageIndex,int pageSize)
		{
			try
			{
				string	strStoredProc =	"Proc_GetVendor_Open_Items";

				DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
			
			
				objWrapper.AddParameter("@VendorId", VendorId);
				objWrapper.AddParameter("@PAGE_SIZE", pageSize);

				
				objWrapper.AddParameter("@CD_LINE_ITEM_ID", null);
				

				objWrapper.AddParameter("@CURRENT_PAGE_INDEX", currentPageIndex);

				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
				return ds;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		/// <summary>
		/// added by : Ajit on 19/7/2005
		/// This function returns the customer/vendor/tax/agency open items dataset
		/// depending on check type.
		/// </summary>
		/// <param name="CustomerId">Id of Entity</param>
		/// <param name="currentPageIndex">Current Index of page</param>
		/// <param name="pageSize">Total page size</param>
		/// <returns>Dataset</returns>
		private DataSet GetTaxOpenItems(int TaxId, int CD_LINE_ITEM_ID, int currentPageIndex,int pageSize)
		{
			try
			{
				string	strStoredProc =	"Proc_GetTax_Open_Items";

				DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
			
			
				objWrapper.AddParameter("@TaxId", TaxId);
				objWrapper.AddParameter("@PAGE_SIZE", pageSize);

				
				objWrapper.AddParameter("@CD_LINE_ITEM_ID", null);
				

				objWrapper.AddParameter("@CURRENT_PAGE_INDEX", currentPageIndex);

				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
				return ds;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		/// <summary>
		/// added by : Ajit on 19/7/2005
		/// This function returns the customer/vendor/tax/agency open items dataset
		/// depending on check type.
		/// </summary>
		/// <param name="CustomerId">Id of Entity</param>
		/// <param name="currentPageIndex">Current Index of page</param>
		/// <param name="pageSize">Total page size</param>
		/// <returns>Dataset</returns>
		private DataSet GetClaimOpenItems(int ClaimId, int CD_LINE_ITEM_ID, int currentPageIndex,int pageSize)
		{//deffered till clarification
			try
			{
				string	strStoredProc =	"Proc_GetTax_Open_Items";

				DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
			
			
				objWrapper.AddParameter("@TaxId", TaxId);
				objWrapper.AddParameter("@PAGE_SIZE", pageSize);

				
				objWrapper.AddParameter("@CD_LINE_ITEM_ID", null);
				

				objWrapper.AddParameter("@CURRENT_PAGE_INDEX", currentPageIndex);

				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
				return ds;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		*/
		#endregion 
		#endregion 
		private bool CreateGroup(out int GroupId )
		{
			GroupId  = 0;
			try
			{
				Cms.Model.Account.ClsReconGroupInfo objReconGroup = new ClsReconGroupInfo();
				objReconGroup.CREATED_DATETIME = DateTime.Now;
				objReconGroup.RECON_ENTITY_ID = 0;
				objReconGroup.RECON_ENTITY_TYPE = "";
				objReconGroup.CD_LINE_ITEM_ID = 0;

				Cms.BusinessLayer.BlAccount.ClsReconGroup objRecon = new Cms.BusinessLayer.BlAccount.ClsReconGroup();
				objRecon.Add(objReconGroup,objWrapper);
				GroupId = objReconGroup.GROUP_ID;
				return true;
			}
			catch
			{
				return false;
			}
		}

		private bool Delete(DataWrapper objWrapper, int RowNo)
		{
			try
			{
				objWrapper.AddParameter("@IDEN_ROW_NO", RowNo);
				
				int RetVal = objWrapper.ExecuteNonQuery(DELETE_PROC);
				objWrapper.ClearParameteres();

				if ( RetVal > 0)
					return true;
				else
					return false;

			}
			catch
			{
				return false;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityType"></param>
		/// <param name="DepositNo"></param>
		/// <param name="recon_amount"></param>
		/// <param name="amount"></param>
		/// <param name="calledFrom"></param>
		/// <returns></returns>
		/// //Added For Itrack Issue #6362.
		public string GetAgencyRecon(string entityType,string DepositNo,double recon_amount,double amount,string calledFrom)
		{
			string tranDesc = "";
		 if(entityType=="")
			{
				if(calledFrom == "ADD") 
					tranDesc =  ";Deposit No = " + DepositNo + ";Amount to apply = " + String.Format("{0:0,0.00}",recon_amount);
				else
					tranDesc = ";Vendor Name = " + customerName+";Amount Modified from  " + String.Format("{0:0,0.00}",recon_amount) + " to "  +  String.Format("{0:0,0.00}",amount); 
			}
 
			return tranDesc;
		}     
		/// <summary>
		/// used for Transaction lof recon Details
		/// </summary>
		/// <param name="entityType"></param>
		/// <param name="amount"></param>
		/// <returns></returns>
		public string GetReconInfo(string entityType,double recon_amount,double amount,string calledFrom)
		{
			 string tranDesc = "";
			if(entityType=="CUST")
			{
				if(calledFrom == "ADD")
                    tranDesc = ";Customer Name = " + customerName+";Policy # = "+ policyNumber +";Amount to apply = " + String.Format("{0:0,0.00}",recon_amount);
				else
					tranDesc = ";Customer Name = " + customerName+";Policy # = "+ policyNumber +";Amount Modified from  " + String.Format("{0:0,0.00}",recon_amount) + " to "  +  String.Format("{0:0,0.00}",amount);

			}
			else if(entityType=="AGN")
			{
				if(calledFrom == "ADD")
				     tranDesc =  ";Agency Name = " + customerName+";Policy # = "+ policyNumber +";Amount to apply = " + String.Format("{0:0,0.00}",recon_amount);
			    else
                      tranDesc = ";Agency Name = " + customerName+";Policy # = "+ policyNumber +";Amount Modified from  " + String.Format("{0:0,0.00}",recon_amount) + " to "  +  String.Format("{0:0,0.00}",amount);
			}
			else if(entityType=="VEN")
			{
			   if(calledFrom == "ADD") 
					tranDesc =  ";Vendor Name = " + customerName +";Amount to apply = " + String.Format("{0:0,0.00}",recon_amount);
			   else
                    tranDesc = ";Vendor Name = " + customerName+";Amount Modified from  " + String.Format("{0:0,0.00}",recon_amount) + " to "  +  String.Format("{0:0,0.00}",amount); 
			}
 
		   return tranDesc;
		}     
		


	}
}
