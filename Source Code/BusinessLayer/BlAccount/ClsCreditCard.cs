/******************************************************************************************
<Author					: -   Swastika Gaur
<Start Date				: -	  28th May'07
<End Date				: -	
<Description			: -   BL for Credit Card Processing 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/  

using System;
using System.Data;
using Cms.DataLayer;
using Cms.Model.Account;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Collections;


namespace Cms.BusinessLayer.BlAccount
{
	public class CreditCardSpoolInfo
	{
		private int mSpoolID;
		private int mRefDepositID;
		private int mRefDepositDetailID;
		private int mRefCheckID;
		private string  mProcessed;
		private string mErrorDescription;
		private string mPayPalResult;
		private string mPayPalRefID;
		private DateTime mProcessedDatetime;
		private int mCreatedBy;
		private string mNote;

		public int EntityID;
		public string EntityType;
		public string PolicyNumber;
		public int PolicyID;
		public int PolicyVersionID;
		public double Amount;
		
		public CreditCardSpoolInfo()
		{
			mSpoolID =-1;
			mRefDepositID= -1;
			mRefDepositDetailID = -1;
			mRefCheckID  = -1;
			mProcessed = "";
			mErrorDescription = "";
			mPayPalRefID = "";
			mPayPalResult ="";
			mCreatedBy =0;
			mNote = "";
		}
			

		#region Public Properties
		
		public int REF_CHECK_ID 
		{
			get
			{
				return mRefCheckID;
			}
			set
			{
				mRefCheckID = value;
			}
		}

		public string PayPalResult
		{
			get
			{
				return mPayPalResult;
			}
			set
			{
				mPayPalResult = value;
			}
		}
		public string PayPalRefID
		{
			get
			{
				return mPayPalRefID;
			}
			set
			{
				mPayPalRefID = value;
			}
		}

		public int SPOOL_ID
		{
			get 
			{
				return mSpoolID;
			}
			set
			{
				mSpoolID = value;
			}
		}
	
		public int REF_DEPOSIT_ID
		{
			get
			{
				return mRefDepositID;
			}
			set 
			{
				mRefDepositID=value;
			}
		}
		public int REF_DEPOSIT_DETAIL_ID 
		{
			get
			{
				return mRefDepositDetailID;
			}
			set 
			{
				mRefDepositDetailID=value;
			}
		}
		public string PROCESSED
		{
			get
			{
				return mProcessed;
			}
			set 
			{
				mProcessed = value;
			}
		}
		public DateTime PROCESSED_DATETIME
		{
			get
			{
				return mProcessedDatetime;
			}
			set
			{
				mProcessedDatetime = value;
			}
		}
		public string ERROR_DESCRIPTION
		{
			get
			{
				return mErrorDescription ;
			}
			set 
			{
				mErrorDescription = value;
			}
		}
		public int CREATED_BY 
		{
			get
			{
				return mCreatedBy;
			}
			set
			{
				mCreatedBy = value;
			}
		}
		public string NOTE
		{
			get
			{
				return mNote ;
			}
			set 
			{
				mNote = value;
			}
		}
		#endregion


	}
	

	public class ClsCreditCard :Cms.BusinessLayer.BlAccount.ClsAccount
	{
		

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		private DataWrapper objWrapper;
		private string CreatedFrom = "CC";
		private PayPalInterface objPayPal ;
		public const string AUTHORISATION_AMOUNT ="1.00";
		
		public const int CREDIT_CARD_PHRASE	= 1 ; 
		
		#endregion

		#region Public Properties
		public PayPalInterface PayPalAPI
		{
			get
			{
				return objPayPal;
			}
			set
			{
				objPayPal = value;
			}
		}


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
		public ClsCreditCard()
		{
			objPayPal = new PayPalInterface();
			boolTransactionLog	= base.TransactionLogRequired;
		}
		public ClsCreditCard(bool transactionLogRequired):this()
		{
			base.TransactionLogRequired = transactionLogRequired;
		}
		#endregion

		#region APP LEVEL FUNCTIONS : Save / Get
		public PayPalResponse Save(ClsEFTInfo objEFTinfo, int userID)
		{

			TransactionInfo objCardInfo = new TransactionInfo(); 
			
			objCardInfo.AddressVerificationRequired = true;
			objCardInfo.Amount = AUTHORISATION_AMOUNT;
			objCardInfo.CardNumber = objEFTinfo.CARD_NO ; 
			objCardInfo.City  = objEFTinfo.CUSTOMER_CITY;
			objCardInfo.CVV2		= objEFTinfo.CARD_CVV_NUMBER ; 
			objCardInfo.ExpiryDate = objEFTinfo.CARD_DATE_VALID_TO ; 
			objCardInfo.FirstName  = objEFTinfo.CUSTOMER_FIRST_NAME ; 
			objCardInfo.LastName   = objEFTinfo.CUSTOMER_LAST_NAME ; 
			objCardInfo.MiddleName = objEFTinfo.CUSTOMER_MIDDLE_NAME ; 
			objCardInfo.State	 = objEFTinfo.CUSTOMER_STATE ;
			objCardInfo.Street  = objEFTinfo.CUSTOMER_ADDRESS1 + " " + objEFTinfo.CUSTOMER_ADDRESS2 ; 
			objCardInfo.Zip     = objEFTinfo.CUSTOMER_ZIP ;

			PayPalResponse objResponse =  DoCardAuthorization(objCardInfo);

			string PayPalRefID = "";
			int retval = 0;
			int result = 0;

			if(Convert.ToInt32(objResponse.Result.Trim()) == (int)PayPalResult.Approved) 
			{
				BlCommon.Security.ClsCrypto objCrypto = new Cms.BusinessLayer.BlCommon.Security.ClsCrypto
					(BlCommon.Security.ClsSecurity.GetPasswordPhrase(objEFTinfo.CUSTOMER_ID, CREDIT_CARD_PHRASE)); 

				PayPalRefID = objCrypto.Encrypt(objResponse.PNRefrence).ToString() ; 
				string		strStoredProc	=	"Proc_InsertACT_APP_CREDIT_CARD_DETAILS";
				
				DateTime	RecordDate		=	DateTime.Now;
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				try
				{
				
					objDataWrapper.AddParameter("@CUSTOMER_ID",objEFTinfo.CUSTOMER_ID);
					objDataWrapper.AddParameter("@APP_ID",objEFTinfo.APP_ID);
					objDataWrapper.AddParameter("@APP_VERSION_ID",objEFTinfo.APP_VERSION_ID);
					objDataWrapper.AddParameter("@PAY_PAL_REF_ID",PayPalRefID);
				
					SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RetVal", SqlDbType.Int, ParameterDirection.ReturnValue);
										
					#region Trans Log
					//Transaction Log Succesful Transactions.
					if(base.TransactionLog)
					{
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						if (IsEODProcess)
							objTransactionInfo.RECORDED_BY		=	EODUserID;
						else
							objTransactionInfo.RECORDED_BY		=	userID;

						objTransactionInfo.CLIENT_ID = objEFTinfo.CUSTOMER_ID;
						objTransactionInfo.APP_ID = objEFTinfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objEFTinfo.APP_VERSION_ID;

                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1668", "");// "Credit card information saved successfully with processor.";
											
						int returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

					}
					else
					{
						result	= objDataWrapper.ExecuteNonQuery(strStoredProc);
						retval = int.Parse(objSqlParameter.Value.ToString());
					}
					#endregion
					
				}
				catch(Exception ex)
				{
					//throw(ex);
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}
				finally
				{
					if(objDataWrapper != null) objDataWrapper.Dispose();
				}
			}
			
			return objResponse;

			
//			string		strStoredProc	=	"Proc_InsertACT_APP_CREDIT_CARD_DETAILS";
//				
//			DateTime	RecordDate		=	DateTime.Now;
//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
//			try
//			{
//				
//				objDataWrapper.AddParameter("@CUSTOMER_ID",objEFTinfo.CUSTOMER_ID);
//				objDataWrapper.AddParameter("@APP_ID",objEFTinfo.APP_ID);
//				objDataWrapper.AddParameter("@APP_VERSION_ID",objEFTinfo.APP_VERSION_ID);
//				objDataWrapper.AddParameter("@CARD_HOLDER_NAME",objEFTinfo.CARD_HOLDER_NAME);
//				objDataWrapper.AddParameter("@CARD_NO",objEFTinfo.CARD_NO);
//				objDataWrapper.AddParameter("@CARD_TYPE",objEFTinfo.CARD_TYPE);
//				objDataWrapper.AddParameter("@CARD_DATE_VALID_TO",objEFTinfo.CARD_DATE_VALID_TO);
//				objDataWrapper.AddParameter("@CARD_DATE_VALID_FROM",objEFTinfo.CARD_DATE_VALID_FROM);
//				objDataWrapper.AddParameter("@CARD_CVV_NUMBER",objEFTinfo.CARD_CVV_NUMBER);
//				objDataWrapper.AddParameter("@CUSTOMER_FIRST_NAME",objEFTinfo.CUSTOMER_FIRST_NAME);
//				objDataWrapper.AddParameter("@CUSTOMER_MIDDLE_NAME",objEFTinfo.CUSTOMER_MIDDLE_NAME);
//				objDataWrapper.AddParameter("@CUSTOMER_LAST_NAME",objEFTinfo.CUSTOMER_LAST_NAME);
//				objDataWrapper.AddParameter("@CUSTOMER_ADDRESS1",objEFTinfo.CUSTOMER_ADDRESS1);	
//				objDataWrapper.AddParameter("@CUSTOMER_ADDRESS2",objEFTinfo.CUSTOMER_ADDRESS2);	
//				objDataWrapper.AddParameter("@CUSTOMER_CITY",objEFTinfo.CUSTOMER_CITY);			
//				objDataWrapper.AddParameter("@CUSTOMER_COUNTRY",objEFTinfo.CUSTOMER_COUNTRY);
//				objDataWrapper.AddParameter("@CUSTOMER_STATE",objEFTinfo.CUSTOMER_STATE);		
//				objDataWrapper.AddParameter("@CUSTOMER_ZIP",objEFTinfo.CUSTOMER_ZIP);
//				
//				SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RetVal", SqlDbType.Int, ParameterDirection.ReturnValue);
//
//				result	= objDataWrapper.ExecuteNonQuery(strStoredProc);
//
//				if(TransactionLog)
//				{
//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//					string strTranXML="";
//					objEFTinfo.TransactLabel = BlCommon.ClsCommon.MapTransactionLabel("/application/aspx/InstallmentInfo.aspx.resx");
//					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
//					if(objOldEFTinfo==null)
//					{
//						strTranXML = objBuilder.GetTransactionLogXML(objEFTinfo);
//						strTranXML = ProcessTransXML(strTranXML); //Modifeid
//						objTransactionInfo.TRANS_DESC		=	"Customer Credit Card Information Has Been Saved";
//					}
//					else
//					{
//						strTranXML = objBuilder.GetTransactionLogXML(objOldEFTinfo,objEFTinfo);
//						strTranXML = ProcessTransXML(strTranXML); //Modifeid
//						objTransactionInfo.TRANS_DESC		=	"Customer Credit Card Information Has Been Updated";
//					}					
//					objTransactionInfo.TRANS_TYPE_ID	=	1;
//					objTransactionInfo.RECORDED_BY		=	objEFTinfo.CREATED_BY;
//					objTransactionInfo.CLIENT_ID		=	objEFTinfo.CUSTOMER_ID;
//					objTransactionInfo.APP_ID			=	objEFTinfo.APP_ID;
//					objTransactionInfo.APP_VERSION_ID	=	objEFTinfo.APP_VERSION_ID;					
//					objTransactionInfo.CHANGE_XML		=	strTranXML;
//					//Executing the query					
//					result	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
//				}
//				else
//				{
//					result	= objDataWrapper.ExecuteNonQuery(strStoredProc);
//				}
//
//				retval = int.Parse(objSqlParameter.Value.ToString());
//				objDataWrapper.ClearParameteres();
//				return retval;
//			}
//			catch(Exception ex)
//			{
//				throw(ex);
//			}
//			finally
//			{
//				if(objDataWrapper != null) objDataWrapper.Dispose();
//			}
		}
		public string ProcessTransXML(string transXML)
		{
			System.Xml.XmlDocument transDoc = new System.Xml.XmlDocument();
			string strOuterXml = "";
            
			try
			{
				transDoc.LoadXml(transXML);
				System.Xml.XmlNode mapNodeFEDERAL_ID = transDoc.SelectSingleNode("//Map[@field='FEDERAL_ID']");
				if(mapNodeFEDERAL_ID!=null)
                    transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(mapNodeFEDERAL_ID);		

				System.Xml.XmlNode mapNodeDFI_ACC_NO = transDoc.SelectSingleNode("//Map[@field='DFI_ACC_NO']");
				if(mapNodeDFI_ACC_NO!=null)
                    transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(mapNodeDFI_ACC_NO);		
				
				System.Xml.XmlNode mapNodeTRANSIT_ROUTING_NO = transDoc.SelectSingleNode("//Map[@field='TRANSIT_ROUTING_NO']");
				if(mapNodeTRANSIT_ROUTING_NO!=null)
                    transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(mapNodeTRANSIT_ROUTING_NO);	
	
				System.Xml.XmlNode mapNodeEFT_TENTATIVE_DATE = transDoc.SelectSingleNode("//Map[@field='EFT_TENTATIVE_DATE']");
				if(mapNodeEFT_TENTATIVE_DATE!=null)
                    transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(mapNodeEFT_TENTATIVE_DATE);	

				//Format date
				System.Xml.XmlNode mapNodeCARD_DATE_VALID_TO = transDoc.SelectSingleNode("//Map[@field='CARD_DATE_VALID_TO']");
				if(mapNodeCARD_DATE_VALID_TO!=null)
				{
					string month="",year="";
					System.Xml.XmlAttribute attrCARD_DATE_VALID_TO_OLD = mapNodeCARD_DATE_VALID_TO.Attributes["OldValue"];
					if(attrCARD_DATE_VALID_TO_OLD!=null && attrCARD_DATE_VALID_TO_OLD.Value!="")
					{
						month = attrCARD_DATE_VALID_TO_OLD.InnerText.ToString().Substring(0,2);
						year = attrCARD_DATE_VALID_TO_OLD.InnerText.ToString().Substring(2,2);
						attrCARD_DATE_VALID_TO_OLD.InnerText = month + "/" + year;

					}

					System.Xml.XmlAttribute attrCARD_DATE_VALID_TO_NEW = mapNodeCARD_DATE_VALID_TO.Attributes["NewValue"];
					if(attrCARD_DATE_VALID_TO_NEW!=null && attrCARD_DATE_VALID_TO_NEW.Value!="")
					{
						month = attrCARD_DATE_VALID_TO_NEW.InnerText.ToString().Substring(0,2);
						year = attrCARD_DATE_VALID_TO_NEW.InnerText.ToString().Substring(2,2);
						attrCARD_DATE_VALID_TO_NEW.InnerText = month + "/" + year;
					}
				}
				System.Xml.XmlNode mapNodeACCOUNT_TYPE = transDoc.SelectSingleNode("//Map[@field='ACCOUNT_TYPE']");
				if(mapNodeACCOUNT_TYPE!=null)
					transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(mapNodeACCOUNT_TYPE);

				strOuterXml = transDoc.OuterXml;
				//Remove Modifeid Label
				if(!transDoc.SelectSingleNode("//LabelFieldMapping").HasChildNodes)
				{
                   strOuterXml = "";
				}
				
				return strOuterXml;

                
			}
			catch(Exception ex)
			{
				//throw(ex);
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{

			}
			return strOuterXml;
			
            
		}

		public DataSet GetAppCCCustInfo(int CustomerID, int AppID, int AppVersionID)
		{
			try
			{
				string	strStoredProc =	"PROC_GetACT_APP_CREDIT_CARD_DETAILS";			
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
				objWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objWrapper.AddParameter("@APP_ID",AppID);
				objWrapper.AddParameter("@APP_VERSION_ID",AppVersionID);
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);			
				if(ds!=null)
					return ds;
				else
					return null;
			}
			catch(Exception ex)
			{
				//throw ex;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
			}
			return null;
		}
		#endregion

		#region POL LEVEL FUNCTIONS : Save / Get
		public PayPalResponse PolSave(ClsEFTInfo objEFTinfo ,int userID)
		{

			TransactionInfo objCardInfo = new TransactionInfo(); 
			
			objCardInfo.AddressVerificationRequired = true;
			objCardInfo.Amount = AUTHORISATION_AMOUNT;
			objCardInfo.CardNumber = objEFTinfo.CARD_NO ; 
			objCardInfo.City  = objEFTinfo.CUSTOMER_CITY;
			objCardInfo.CVV2		= objEFTinfo.CARD_CVV_NUMBER ; 
			objCardInfo.ExpiryDate = objEFTinfo.CARD_DATE_VALID_TO ; 
			objCardInfo.FirstName  = objEFTinfo.CUSTOMER_FIRST_NAME ; 
			objCardInfo.LastName   = objEFTinfo.CUSTOMER_LAST_NAME ; 
			objCardInfo.MiddleName = objEFTinfo.CUSTOMER_MIDDLE_NAME ; 
			objCardInfo.State	 = objEFTinfo.CUSTOMER_STATE ;
			objCardInfo.Street  = objEFTinfo.CUSTOMER_ADDRESS1 + " " + objEFTinfo.CUSTOMER_ADDRESS2 ; 
			objCardInfo.Zip     = objEFTinfo.CUSTOMER_ZIP ;

			PayPalResponse objResponse =  DoCardAuthorization(objCardInfo);

			string PayPalRefID = "";
			
			if(Convert.ToInt32(objResponse.Result.Trim()) == (int)PayPalResult.Approved) 
			{
				BlCommon.Security.ClsCrypto objCrypto = new Cms.BusinessLayer.BlCommon.Security.ClsCrypto
					(BlCommon.Security.ClsSecurity.GetPasswordPhrase(objEFTinfo.CUSTOMER_ID, CREDIT_CARD_PHRASE)); 

				PayPalRefID = objCrypto.Encrypt(objResponse.PNRefrence).ToString() ; 

				string		strStoredProc	=	"Proc_InsertACT_POL_CREDIT_CARD_DETAILS";
				
				DateTime	RecordDate		=	DateTime.Now;
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				try
				{
					int retval = 0;
					int result = 0;
					objDataWrapper.AddParameter("@CUSTOMER_ID",objEFTinfo.CUSTOMER_ID);
					objDataWrapper.AddParameter("@POLICY_ID",objEFTinfo.POLICY_ID);
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",objEFTinfo.POLICY_VERSION_ID);
					objDataWrapper.AddParameter("@PAY_PAL_REF_ID",PayPalRefID);					
				
					SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RetVal", SqlDbType.Int, ParameterDirection.ReturnValue);

					#region Trans Log
					//Transaction Log Succesful Transactions.
					if(base.TransactionLog)
					{
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						if (IsEODProcess)
							objTransactionInfo.RECORDED_BY		=	EODUserID;
						else
							objTransactionInfo.RECORDED_BY		=	userID;

						objTransactionInfo.CLIENT_ID = objEFTinfo.CUSTOMER_ID;
						objTransactionInfo.POLICY_ID = objEFTinfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID = objEFTinfo.POLICY_VERSION_ID;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1668", "");// "Credit card information saved successfully with processor.";
						
						int returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

					}
					else
					{
						result	= objDataWrapper.ExecuteNonQuery(strStoredProc);
						retval = int.Parse(objSqlParameter.Value.ToString());
					}
					#endregion
				
				}
				catch(Exception ex)
				{
					//throw(ex);
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}
				finally
				{
					if(objDataWrapper != null) objDataWrapper.Dispose();
				}
			}
			return objResponse;
		}

		public DataSet GetPolCCCustInfo(int CustomerID, int PolID, int PolVersionID)
		{
			try
			{
				string	strStoredProc =	"PROC_GetACT_POL_CREDIT_CARD_DETAILS";			
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
				objWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objWrapper.AddParameter("@POLICY_ID",PolID);
				objWrapper.AddParameter("@POLICY_VERSION_ID",PolVersionID);
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);			
				if(ds!=null)
					return ds;
				else
					return null;
			}
			catch(Exception ex)
			{
				//throw ex;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
			}
			return null;
		}
		#endregion

		#region UPDATE OVER PAYMENT
		private void UpdateStatusOverPayment(string payPalRefID)
		{
			DataWrapper objWrapper= new DataWrapper(ConnStr , CommandType.StoredProcedure, 
				DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
			
			try
			{
				objWrapper.AddParameter("@PAY_PAL_REF_ID",payPalRefID);
				objWrapper.ExecuteNonQuery("Proc_UpdateStatusOverPayment");
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction (DataLayer.DataWrapper.CloseConnection.YES);
				//throw(ex);
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			objWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);

		}

		#endregion


		public PayPalResponse DoCardAuthorization(TransactionInfo objInfo)
		{
			PayPalResponse objPPRes  = new PayPalResponse();
			objInfo.AddressVerificationRequired = true;
			objPPRes = objPayPal.DoAuthorisation(objInfo);
		
			string ReferenceID = objPPRes.PNRefrence ;

			if(Convert.ToInt32(objPPRes.Result.Trim()) == (int)PayPalResult.Approved) 
			{
				PayPalResponse objVoidRes  =  objPayPal.VoidAuthorisation(ReferenceID) ;
			}
			return objPPRes;
		}

	
		#region Credit Card Processing Functions
		private void CreateCheckEntry(string PolicyNumber,int CustomerID, int PolicyID, 
			int PolicyVersionID,double Amount,string UserID ,out int CheckID , int DepositOpenItem)
		{
			
			objWrapper = new DataWrapper(ConnStr , CommandType.StoredProcedure, 
				DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

			try
			{
				// Insert into deposits table
				ClsChecks  objCheck = new ClsChecks();
				ClsChecksInfo  objCheckInfo = new ClsChecksInfo();

				int BankAccountID =0 ,FiscalID = 0 ;// Fetch from Database
				CheckID = 0 ;

				DataSet ds = objWrapper.ExecuteDataSet("Proc_GetCreditCardParams"); 
				if(ds != null && ds.Tables.Count >0 && ds.Tables[0].Rows.Count > 0 )
				{
					if(ds.Tables[0].Rows[0]["BANK_ACCOUNT"] != null)
					{
						BankAccountID = Convert.ToInt32(ds.Tables[0].Rows[0]["BANK_ACCOUNT"]);
					}
					if(ds.Tables[0].Rows[0]["FISCAL_ID"]!= null)
					{
						FiscalID = Convert.ToInt32(ds.Tables[0].Rows[0]["FISCAL_ID"]);
					}
				}

				string CheckNote = "Check Created from Credit Card sweep";
				//Create And commit check

				objCheckInfo.ACCOUNT_ID = BankAccountID;
				objCheckInfo.CHECK_AMOUNT = Amount;
				objCheckInfo.CHECK_DATE = DateTime.Now;
				objCheckInfo.CHECK_MEMO = CheckNote;
				objCheckInfo.CHECK_NOTE = CheckNote;
				objCheckInfo.PAYMENT_MODE = (int)PaymentModes.CreditCard; //CREDIT CARD
				objCheckInfo.CHECK_TYPE = ClsChecks.CHECK_TYPES.PREMIUM_REFUND_CHECKS_FOR_OVER_PAYMENT;
				objCheckInfo.COMMITED_BY = Convert.ToInt32(UserID);
				if (IsEODProcess)
					objCheckInfo.CREATED_BY = objCheckInfo.MODIFIED_BY = EODUserID;
				else
					objCheckInfo.CREATED_BY = objCheckInfo.MODIFIED_BY = Convert.ToInt32(UserID);
				objCheckInfo.CREATED_DATETIME = DateTime.Now;
				objCheckInfo.LAST_UPDATED_DATETIME = DateTime.Now;
								
				objCheckInfo.CUSTOMER_ID = CustomerID;
				objCheckInfo.POLICY_ID  =PolicyID;
				objCheckInfo.POLICY_VER_TRACKING_ID = PolicyVersionID;
				objCheckInfo.PAYEE_ENTITY_ID = CustomerID;
				objCheckInfo.MANUAL_CHECK = "N";
				objCheckInfo.IS_ACTIVE = "Y";
				
				

				objCheck.Add(objCheckInfo,objWrapper,DepositOpenItem);

				CheckID = objCheckInfo.CHECK_ID ;

				objCheckInfo.IS_COMMITED = "Y";
				objCheckInfo.DATE_COMMITTED = DateTime.Now;
				objCheck.Commit(objCheckInfo,objWrapper);

				
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				//throw(ex);
				CheckID = 0 ;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			objWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);
		}

		public PayPalResponse ProcessCC(TransactionInfo objInfo, string PolicyNumber,int CustomerID,
			int PolicyID,int PolicyVersionID,string UserID)
		{
			PayPalResponse objPPRes  = new PayPalResponse();
			objInfo.AddressVerificationRequired = true;
			objPPRes = objPayPal.DoSalesTransaction(objInfo);

			CreditCardSpoolInfo objSpoolInfo = new CreditCardSpoolInfo();
			
			int DepositID = 0,DepositLineItemID = 0;
			int RetValue = 0;
			// If Transaction has been Approved(successfull), create Deposit entry
			if(objPPRes.Result != null)
			{
				if(Convert.ToInt32(objPPRes.Result) == (int)PayPalResult.Approved)
				{
					//In case of Deadlock - Void Transaction
					//-1 : Commit Fails
					RetValue = -1;
					try
					{
						RetValue = CreateCCDepositEntry(PolicyNumber,CustomerID,PolicyID,PolicyVersionID , 
							Convert.ToDouble(objInfo.Amount),UserID ,out DepositID, out DepositLineItemID);
					}
					catch(Exception objExc)
					{
						Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExc);
					}

					#region REVERSE TRANSACTION WITH PAYPAL 
					if(RetValue == -1) 
					{
						if(objPPRes.PNRefrence!=null)
						{
							objInfo.RefrenceID = objPPRes.PNRefrence;
							objPPRes = objPayPal.VoidSalesTransaction(objInfo);
							objPPRes.Result = ((int)PayPalResult.Exception).ToString();
							objSpoolInfo.PayPalResult = objPPRes.Result;
							objPPRes.ResponseMessage =  "Unable to post transaction in GL." ;

						}						
					}
					#endregion

				}
			}

			#region INSERT TRANSACTION ENTRY INTO EOD CC SPOOL TABLE
	
			objSpoolInfo.EntityID = CustomerID;
			objSpoolInfo.EntityType = "CUST";
			objSpoolInfo.PolicyNumber = PolicyNumber.ToString();
			objSpoolInfo.PolicyID = PolicyID;
			objSpoolInfo.PolicyVersionID = PolicyVersionID;
			objSpoolInfo.REF_DEPOSIT_ID = DepositID;
			objSpoolInfo.REF_DEPOSIT_DETAIL_ID = DepositLineItemID;
			objSpoolInfo.Amount = Convert.ToDouble(objInfo.Amount);
			objSpoolInfo.PROCESSED = "Y";
			objSpoolInfo.PayPalRefID = objPPRes.PNRefrence ;
			objSpoolInfo.PayPalResult = objPPRes.Result ;
			objSpoolInfo.ERROR_DESCRIPTION = objPPRes.ResponseMessage ;
			objSpoolInfo.NOTE = objInfo.NOTE;
			objSpoolInfo.PROCESSED_DATETIME = DateTime.Now;
			if (IsEODProcess)
				objSpoolInfo.CREATED_BY = EODUserID;
			else
				objSpoolInfo.CREATED_BY =Convert.ToInt32(UserID);
			InsertEODCreditCardSpoolEntry(objSpoolInfo);

			#endregion
			return objPPRes;

		}

		private int GetOpenItemIdForCCDeposit(string PayPalRefID)
		{
			int CustOpenItemID = 0 ; 
			DataWrapper objWrapper= new DataWrapper(ConnStr , CommandType.StoredProcedure);

            objWrapper.AddParameter("@PAYPAL_REF_ID",PayPalRefID);
			SqlParameter objParam = (SqlParameter) objWrapper.AddParameter("@OPEN_ITEM_ID",SqlDbType.Int , ParameterDirection.Output);

			objWrapper.ExecuteNonQuery("Proc_GetOpenItemIDForCCDeposit");
			if(objParam != null)
			{
				CustOpenItemID = Convert.ToInt32(objParam.Value);
			}

			return CustOpenItemID ; 
		}


		public bool ReverseSalesTransaction(string RefrenceID, double Amount, string PolicyNumber, 
			int CustomerID , int PolicyID , int PolicyVersionID, int UserID, int Date_Flag)
		{
			bool ReturnCode = false; 

			TransactionInfo objInfo = new TransactionInfo();
			objInfo.RefrenceID = RefrenceID ; 
			objInfo.Amount = Amount.ToString();
			objInfo.PolicyNumber = PolicyNumber ; 

//			CreditCardSpoolInfo objSpoolInfo = new CreditCardSpoolInfo();
//			PayPalResponse objPPRes  =  objPayPal.DoCreditTransaction (objInfo);

			CreditCardSpoolInfo objSpoolInfo = new CreditCardSpoolInfo();
			PayPalResponse objPPRes = new PayPalResponse();
			
			if (Date_Flag == 1)
			{
				objPPRes = objPayPal.VoidSalesTransaction(objInfo);
			}
			else
			{
				objPPRes  =  objPayPal.DoCreditTransaction (objInfo);
			}

			if(objPPRes.Result != null)
			{
				if(Convert.ToInt32(objPPRes.Result) == (int)PayPalResult.Approved)
				{
					ReturnCode = true; 
				}
			}

			
			objSpoolInfo.EntityID = CustomerID;
			objSpoolInfo.EntityType = "CUST";
			objSpoolInfo.PolicyID = PolicyID;
			objSpoolInfo.PolicyVersionID = PolicyVersionID;
			objSpoolInfo.Amount = Amount * -1;
			if(ReturnCode)
			{
				objSpoolInfo.PROCESSED = "Y";
			}
			else
			{
				objSpoolInfo.PROCESSED = "F";
			}
			objSpoolInfo.PayPalRefID = objPPRes.PNRefrence ;
			objSpoolInfo.PayPalResult = objPPRes.Result ;
			objSpoolInfo.ERROR_DESCRIPTION = objPPRes.ResponseMessage ;
			objSpoolInfo.PROCESSED_DATETIME = DateTime.Now;
			objSpoolInfo.CREATED_BY =UserID; 
			InsertEODCreditCardSpoolEntry(objSpoolInfo);

			return ReturnCode;
		}
		public PayPalResponse ProcessCreditTransaction(TransactionInfo objInfo, string PolicyNumber,
			int CustomerID,int PolicyID,int PolicyVersionID,string UserID)
		{
			PayPalResponse objPPRes  = new PayPalResponse();
			objPPRes = objPayPal.DoCreditTransaction (objInfo);
			
			int CheckID = 0;
			// If Transaction has been Approved(successfull), create Deposit entry
			if(objPPRes.Result != null)
			{
				if(Convert.ToInt32(objPPRes.Result) == (int)PayPalResult.Approved)
				{
					int DepositOpenItem = GetOpenItemIdForCCDeposit(objInfo.RefrenceID.Trim()); 
					CreateCheckEntry(PolicyNumber,CustomerID,PolicyID,PolicyVersionID , 
						Convert.ToDouble(objInfo.Amount),UserID ,out  CheckID , DepositOpenItem);

					UpdateStatusOverPayment(objInfo.RefrenceID);
				}
			}

			// Insert transaction entry into EOD CC Spool Table
			CreditCardSpoolInfo objSpoolInfo = new CreditCardSpoolInfo();
			objSpoolInfo.EntityID = CustomerID;
			objSpoolInfo.EntityType = "CUST";
			objSpoolInfo.PolicyID = PolicyID;
			objSpoolInfo.PolicyVersionID = PolicyVersionID;
			objSpoolInfo.REF_CHECK_ID  = CheckID;
			objSpoolInfo.Amount = Convert.ToDouble(objInfo.Amount) * -1;
			objSpoolInfo.PROCESSED = "Y";
			objSpoolInfo.PayPalRefID = objPPRes.PNRefrence ;
			objSpoolInfo.PayPalResult = objPPRes.Result ;
			objSpoolInfo.ERROR_DESCRIPTION = objPPRes.ResponseMessage ;
			objSpoolInfo.NOTE = objInfo.NOTE;
			objSpoolInfo.PROCESSED_DATETIME = DateTime.Now;
			if (IsEODProcess)
				objSpoolInfo.CREATED_BY = EODUserID;
			else
				objSpoolInfo.CREATED_BY =Convert.ToInt32(UserID); 
			InsertEODCreditCardSpoolEntry(objSpoolInfo);
			return objPPRes;
		}

		public void InsertEODCreditCardSpoolEntry(CreditCardSpoolInfo objSpoolInfo)
		{
			DataWrapper objWrapper= new DataWrapper(ConnStr , CommandType.StoredProcedure, 
				DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

			string strStoredProc = "Proc_EOD_AddREcordToCreditCardSpool";
			
			try
			{
				if (IsEODProcess)
					objSpoolInfo.CREATED_BY = EODUserID;

				objWrapper.AddParameter("@ENTITY_ID",objSpoolInfo.EntityID);
				objWrapper.AddParameter("@ENTITY_TYPE",objSpoolInfo.EntityType );
				objWrapper.AddParameter("@POLICY_NUMBER",objSpoolInfo.PolicyNumber);
				objWrapper.AddParameter("@POLICY_ID",objSpoolInfo.PolicyID );
				objWrapper.AddParameter("@POLICY_VERSION_ID",objSpoolInfo.PolicyVersionID);
				objWrapper.AddParameter("@REF_DEPOSIT_ID",objSpoolInfo.REF_DEPOSIT_ID);
				objWrapper.AddParameter("@REF_DEP_DETAIL_ID", objSpoolInfo.REF_DEPOSIT_DETAIL_ID);
				objWrapper.AddParameter("@TRANSACTION_AMOUNT", objSpoolInfo.Amount);
				objWrapper.AddParameter("@PROCESSED",objSpoolInfo.PROCESSED);
				objWrapper.AddParameter("@ERROR_DESCRIPTION",objSpoolInfo.ERROR_DESCRIPTION);
				objWrapper.AddParameter("@PAYPALREFID",objSpoolInfo.PayPalRefID );
				objWrapper.AddParameter("@PAYPALRESULT",objSpoolInfo.PayPalResult );
				objWrapper.AddParameter("@PROCESSED_DATETIME",objSpoolInfo.PROCESSED_DATETIME  );
				objWrapper.AddParameter("@REF_CHECK_ID",objSpoolInfo.REF_CHECK_ID );
				objWrapper.AddParameter("@CREATED_BY",objSpoolInfo.CREATED_BY);
				objWrapper.AddParameter("@NOTE",objSpoolInfo.NOTE);
				//Transaction Log For Failed transactions.
				if(base.TransactionLog && objSpoolInfo.PayPalResult != "0")
				{
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					if (IsEODProcess)
						objTransactionInfo.RECORDED_BY		=	EODUserID;
					else
						objTransactionInfo.RECORDED_BY		=	objSpoolInfo.CREATED_BY;

					objTransactionInfo.CLIENT_ID = objSpoolInfo.EntityID;
					objTransactionInfo.POLICY_ID = objSpoolInfo.PolicyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objSpoolInfo.PolicyVersionID;


                    objTransactionInfo.TRANS_DESC   = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1669", "");// "Unable to Process Credit Card Transaction.";
					objTransactionInfo.CUSTOM_INFO		=	"Error Description = " + objSpoolInfo.ERROR_DESCRIPTION;
					
					int returnResult = objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
                    objWrapper.ExecuteNonQuery(strStoredProc);
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction (DataLayer.DataWrapper.CloseConnection.YES);
				//throw(ex);
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			objWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);
			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="PolicyNumber"></param>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		/// <param name="Amount"></param>
		/// <param name="UserID"></param>
		/// <param name="DepositID"></param>
		/// <param name="DepositLineItemID"></param>
		/// <returns>
		/// 1 - DoSalesTransaction  the Transaction 
		/// 0 - DoCreditTransaction the Transaction 
		/// </returns>
		private int CreateCCDepositEntry(string PolicyNumber,int CustomerID, int PolicyID, 
			int PolicyVersionID,double Amount,string UserID ,out int DepositID ,out int DepositLineItemID )
		{
			
			objWrapper = new DataWrapper(ConnStr , CommandType.StoredProcedure, 
						   DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

			try
			{
				// Insert into deposits table
				ClsDeposit objDeposit = new ClsDeposit();
				ClsDepositInfo objDepositInfo = new ClsDepositInfo();

				int BankAccountID =0 ,FiscalID = 0,Receipt_Mode= 0;// Fetch from Database
				DepositID = 0;
				DepositLineItemID = 0;
				DataSet ds = objWrapper.ExecuteDataSet("Proc_GetCreditCardParams"); 
				if(ds != null && ds.Tables.Count >0 && ds.Tables[0].Rows.Count > 0 )
				{
					if(ds.Tables[0].Rows[0]["BANK_ACCOUNT"] != null)
					{
						BankAccountID = Convert.ToInt32(ds.Tables[0].Rows[0]["BANK_ACCOUNT"]);
					}
					if(ds.Tables[0].Rows[0]["FISCAL_ID"]!= null)
					{
						FiscalID = Convert.ToInt32(ds.Tables[0].Rows[0]["FISCAL_ID"]);
					}
				}

				string DepositNote = "Deposit Created from Credit Card sweep";
				//Create Deposit Group
				objDepositInfo.ACCOUNT_ID = BankAccountID;  
				objDepositInfo.DEPOSIT_NOTE =DepositNote;
				objDepositInfo.DEPOSIT_TRAN_DATE = DateTime.Now;
				objDepositInfo.RECEIPT_MODE = (int)PaymentModes.CreditCard ;
				objDepositInfo.TOTAL_DEPOSIT_AMOUNT = 0; 
				objDepositInfo.CREATED_DATETIME = DateTime.Now;
				if (IsEODProcess)
					objDepositInfo.CREATED_BY = EODUserID;
				else
					objDepositInfo.CREATED_BY = int.Parse(UserID);
				string strDEPOSIT_NO = Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts.GetDepositNumberByAccountID(FiscalID,BankAccountID);
				objDepositInfo.DEPOSIT_NUMBER = Convert.ToInt32(strDEPOSIT_NO);
		
				objDeposit.Add(objDepositInfo,objWrapper);
				DepositID = objDepositInfo.DEPOSIT_ID;
				Receipt_Mode = objDepositInfo.RECEIPT_MODE;
				//Create Customer Deposit Line item
				ClsDepositDetailsInfo objDetailInfo = new ClsDepositDetailsInfo();
				if (IsEODProcess)
					objDetailInfo.CREATED_BY = EODUserID;
				else
					objDetailInfo.CREATED_BY = int.Parse(UserID);
				objDetailInfo.DEPOSIT_ID  = DepositID;
				objDetailInfo.DEPOSIT_TYPE = "CUST";
				objDetailInfo.PAYOR_TYPE ="CUST";
				objDetailInfo.CUSTOMER_ID = CustomerID;
				objDetailInfo.RECEIPT_FROM_ID = PolicyID;
				objDetailInfo.POLICY_ID = PolicyID;
				objDetailInfo.POLICY_VERSION_ID = PolicyVersionID;
				objDetailInfo.POLICY_NO = PolicyNumber;
				objDetailInfo.RECEIPT_AMOUNT = Amount;
				objDetailInfo.CREATED_DATETIME = DateTime.Now;
			
				ClsDepositDetails objDepositDetail = new ClsDepositDetails(); 	
				objDepositDetail.AddDepositDetail(objWrapper,objDetailInfo,CreatedFrom,Receipt_Mode);
				//Need to Fetch line item id of created detail
				DepositLineItemID = objDetailInfo.CD_LINE_ITEM_ID;

				//objDeposit.Commit(DepositID,Convert.ToInt32(UserID));
				int retVal = 0;
				retVal = objDeposit.Commit(DepositID,Convert.ToInt32(UserID),objWrapper,Receipt_Mode);
				objWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				return retVal;
			
			}
			catch(Exception objExc)
			{
				objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				DepositID = 0;
				DepositLineItemID =  0;
				System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
				addInfo.Add("Err Descriptor ","Error while creating deposit at Credit Card sweep");
				addInfo.Add("Policy No.", PolicyNumber);
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExc,addInfo);
				return -1;
			}
			finally
			{
				objWrapper.Dispose();
			}
			
		}

		#endregion
	
		#region Batch Processing Methods
		public string StartBatchSweep(int StartedBy)
		{
			objWrapper = new DataWrapper(ConnStr , CommandType.StoredProcedure, 
				DataWrapper.MaintainTransaction.YES,"CCARD Batch", DataWrapper.SetAutoCommit.OFF);	
			string strResult="";
			int Success = 0 , Failed = 0 ; 
			try
			{
				
				ClsDeposit objDeposit = null;
				ClsDepositDetails objDepositDetail = new ClsDepositDetails();

				string DepositNote = "Deposit created from Credit Card batch job";
				int BankAccountID = 0 , iDepositID = 0 , iDepositLineItemID =0, FiscalID = 0,
					iSpoolID = 0;
				string strDEPOSIT_NO = "";

				objWrapper.ClearParameteres();
				DataSet ds = objWrapper.ExecuteDataSet("Proc_GetCreditCardParams"); 
				if(ds != null && ds.Tables.Count >0 && ds.Tables[0].Rows.Count > 0 )
				{
					if(ds.Tables[0].Rows[0]["BANK_ACCOUNT"] != null)
					{
						BankAccountID = Convert.ToInt32(ds.Tables[0].Rows[0]["BANK_ACCOUNT"]);
					}
					if(ds.Tables[0].Rows[0]["FISCAL_ID"]!= null)
					{
						FiscalID = Convert.ToInt32(ds.Tables[0].Rows[0]["FISCAL_ID"]);
					}
				}
				
				objWrapper.ClearParameteres();
				ds = null;

				DataSet dsRecordsToSweep = FetchRecordsForBatchSweep();

				strResult = dsRecordsToSweep.Tables[0].Rows.Count.ToString() + " Records to process, ";


				if(dsRecordsToSweep != null && dsRecordsToSweep.Tables.Count>0 &&
					dsRecordsToSweep.Tables[0].Rows.Count > 0)
				{
					
					//Create a New Deposit with mode Credit Card
					objDeposit = new ClsDeposit();
					ClsDepositInfo objDepositInfo = new ClsDepositInfo();

					//Populate Deposit Model object
					objDepositInfo.ACCOUNT_ID = BankAccountID;  
					objDepositInfo.DEPOSIT_NOTE =DepositNote;
					objDepositInfo.DEPOSIT_TRAN_DATE = DateTime.Now;
					objDepositInfo.RECEIPT_MODE = (int)PaymentModes.CreditCard ;
					objDepositInfo.TOTAL_DEPOSIT_AMOUNT = 0; 
					objDepositInfo.CREATED_DATETIME = DateTime.Now;
					strDEPOSIT_NO = Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts.GetDepositNumberByAccountID(FiscalID,BankAccountID);
					objDepositInfo.DEPOSIT_NUMBER = Convert.ToInt32(strDEPOSIT_NO);
					
					if (IsEODProcess)
						objDepositInfo.CREATED_BY = EODUserID;
					else
						objDepositInfo.CREATED_BY = StartedBy;

					objDeposit.Add(objDepositInfo,objWrapper);
					iDepositID = objDepositInfo.DEPOSIT_ID;
					
					PayPalResponse objResponse;

					for(int i = 0 ; i < dsRecordsToSweep.Tables[0].Rows.Count ;i++)
					{
						objWrapper.ClearParameteres();
			
						iSpoolID =	Convert.ToInt32(dsRecordsToSweep.Tables[0].Rows[i]["SPOOL_RECORD_ID"]);

						CreditCardSpoolInfo objSpool = new CreditCardSpoolInfo();
						objSpool.SPOOL_ID = iSpoolID;
					
//						if(dsRecordsToSweep.Tables[0].Rows[i]["CARD_NUMBER"] == DBNull.Value )
//						{
//							objSpool.PROCESSED = "F";
//							Failed++;
//							objSpool.ERROR_DESCRIPTION  = "Credit card number is either invalid or missing";
//						}
//						else if(dsRecordsToSweep.Tables[0].Rows[i]["CARD_EXPIRY_DATE"] == DBNull.Value )
//						{
//							objSpool.PROCESSED = "F";
//							Failed++;
//							objSpool.ERROR_DESCRIPTION = "Credit expiry date is either invalid or missing";
//						}
//						else if(dsRecordsToSweep.Tables[0].Rows[i]["CVV_NUMBER"] == DBNull.Value )
//						{
//							objSpool.PROCESSED = "F";
//							Failed++;
//							objSpool.ERROR_DESCRIPTION = "Credit cvv number is either invalid or missing";
//						}

						if(dsRecordsToSweep.Tables[0].Rows[i]["CARD_REFERENCE_ID"] == DBNull.Value )
						{
							objSpool.PROCESSED = "F";
							Failed++;
							objSpool.ERROR_DESCRIPTION  = "Credit card information is either invalid or missing";
							//Set Properties for Failed Transactions EOD : Itrack 4571
							if(dsRecordsToSweep.Tables[0].Rows.Count>0)
							{
								objSpool.EntityID = Convert.ToInt32(dsRecordsToSweep.Tables[0].Rows[i]["ENTITY_ID"]);
								objSpool.PolicyID = Convert.ToInt32(dsRecordsToSweep.Tables[0].Rows[i]["POLICY_ID"]);
								objSpool.PolicyVersionID = Convert.ToInt32(dsRecordsToSweep.Tables[0].Rows[i]["POLICY_VERSION_ID"]);
								objSpool.CREATED_BY = EODUserID;	
							}

						}
						else
						{

							// Process Credit Card Sweep
							/*TransactionInfo objInfo = new TransactionInfo();
							objInfo.CardNumber = dsRecordsToSweep.Tables[0].Rows[i]["CARD_NUMBER"].ToString();
							objInfo.ExpiryDate = dsRecordsToSweep.Tables[0].Rows[i]["CARD_EXPIRY_DATE"].ToString();
							objInfo.CVV2 = dsRecordsToSweep.Tables[0].Rows[i]["CVV_NUMBER"].ToString();
							objInfo.Amount = dsRecordsToSweep.Tables[0].Rows[i]["TRANSACTION_AMOUNT"].ToString();
							if(dsRecordsToSweep.Tables[0].Rows[i]["POLICY_NUMBER"] != DBNull.Value)
							{
								objInfo.PolicyNumber  = dsRecordsToSweep.Tables[0].Rows[i]["POLICY_NUMBER"].ToString();
							}
							else
							{
								objInfo.PolicyNumber = "";
							}
							if(dsRecordsToSweep.Tables[0].Rows[i]["CUSTOMER_NAME"] != DBNull.Value)
							{
								objInfo.CustomerName = dsRecordsToSweep.Tables[0].Rows[i]["CUSTOMER_NAME"].ToString();
							}
							else
							{
								objInfo.PolicyNumber = "";
							}
							objResponse =  objPayPal.DoSalesTransaction(objInfo);*/

							
							string strTemp= dsRecordsToSweep.Tables[0].Rows[i]["CARD_REFERENCE_ID"].ToString();
							string Amount = dsRecordsToSweep.Tables[0].Rows[i]["TRANSACTION_AMOUNT"].ToString();

							string PolicyNumber = "", CustomerName = "";

							if(dsRecordsToSweep.Tables[0].Rows[i]["POLICY_NUMBER"] != DBNull.Value)
							{
								PolicyNumber  = dsRecordsToSweep.Tables[0].Rows[i]["POLICY_NUMBER"].ToString();
							}
							
							if(dsRecordsToSweep.Tables[0].Rows[i]["CUSTOMER_NAME"] != DBNull.Value)
							{
								CustomerName = dsRecordsToSweep.Tables[0].Rows[i]["CUSTOMER_NAME"].ToString();
							}

							int CustomerID = Convert.ToInt32(dsRecordsToSweep.Tables[0].Rows[i]["ENTITY_ID"]) ; 
							BlCommon.Security.ClsCrypto objCrypto = new Cms.BusinessLayer.BlCommon.Security.ClsCrypto
								(BlCommon.Security.ClsSecurity.GetPasswordPhrase(CustomerID, CREDIT_CARD_PHRASE)); 

							string OrigID = ""; 

							try
							{
								OrigID = objCrypto.Decrypt(strTemp);
							}
							catch(Exception ex)
							{

								Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(new Exception("Error while decrypring PayPal Ref for Spool ID : --> " + iSpoolID.ToString() + "  ;",ex));
							}

							if(OrigID != "") 
							{
								objResponse = objPayPal.DoSalesFromReference(OrigID,Amount,PolicyNumber, CustomerName);

								if(objResponse.Result == ((int)PayPalResult.Approved).ToString() )
								{
									//Create Customer Deposit Line item
									ClsDepositDetailsInfo objDetailInfo = new ClsDepositDetailsInfo();
									objDetailInfo.DEPOSIT_ID  = iDepositID;
									objDetailInfo.DEPOSIT_TYPE = "CUST";
									objDetailInfo.PAYOR_TYPE ="CUST";
									objDetailInfo.CUSTOMER_ID = Convert.ToInt32(dsRecordsToSweep.Tables[0].Rows[i]["ENTITY_ID"]);
									objDetailInfo.RECEIPT_FROM_ID = Convert.ToInt32(dsRecordsToSweep.Tables[0].Rows[i]["ENTITY_ID"]);
									objDetailInfo.POLICY_ID = Convert.ToInt32(dsRecordsToSweep.Tables[0].Rows[i]["POLICY_ID"]);
									objDetailInfo.POLICY_VERSION_ID = Convert.ToInt32(dsRecordsToSweep.Tables[0].Rows[i]["POLICY_VERSION_ID"]);
									objDetailInfo.POLICY_NO = dsRecordsToSweep.Tables[0].Rows[i]["POLICY_NUMBER"].ToString();
									objDetailInfo.RECEIPT_AMOUNT =Convert.ToDouble(dsRecordsToSweep.Tables[0].Rows[i]["TRANSACTION_AMOUNT"]);
									objDetailInfo.CREATED_DATETIME = DateTime.Now;								
								
									objDetailInfo.CREATED_BY = StartedBy;
								

									objDepositDetail.AddDepositDetail(objWrapper,objDetailInfo,CreatedFrom);
									iDepositLineItemID = objDetailInfo.CD_LINE_ITEM_ID;
									objSpool.REF_DEPOSIT_ID = iDepositID;
									objSpool.REF_DEPOSIT_DETAIL_ID = iDepositLineItemID;
									objSpool.PROCESSED = "Y";
									objSpool.PayPalRefID = objResponse.PNRefrence;
									objSpool.PayPalResult = objResponse.Result;
									objSpool.ERROR_DESCRIPTION = objResponse.ResponseMessage;

									//Update Ref ID
									UpdatePayPalRefIDCreditCard(objWrapper,objDetailInfo,objSpool.PayPalRefID);
							
								}
								else
								{
									objSpool.PROCESSED = "F";
									objSpool.PayPalRefID = objResponse.PNRefrence ;
									objSpool.PayPalResult = objResponse.Result;
									objSpool.ERROR_DESCRIPTION = objResponse.ResponseMessage;
									//Set Properties for Failed Transactions EOD
									objSpool.EntityID = Convert.ToInt32(dsRecordsToSweep.Tables[0].Rows[i]["ENTITY_ID"]);
									objSpool.PolicyID = Convert.ToInt32(dsRecordsToSweep.Tables[0].Rows[i]["POLICY_ID"]);
									objSpool.PolicyVersionID = Convert.ToInt32(dsRecordsToSweep.Tables[0].Rows[i]["POLICY_VERSION_ID"]);
									objSpool.CREATED_BY = EODUserID;				


								}
							}
							else
							{
								objSpool.PROCESSED = "F";
								objSpool.ERROR_DESCRIPTION  = "Unable to decrypt PayPal reference, please report this error to System Administrator.";
								objSpool.EntityID = Convert.ToInt32(dsRecordsToSweep.Tables[0].Rows[i]["ENTITY_ID"]);
								objSpool.PolicyID = Convert.ToInt32(dsRecordsToSweep.Tables[0].Rows[i]["POLICY_ID"]);
								objSpool.PolicyVersionID = Convert.ToInt32(dsRecordsToSweep.Tables[0].Rows[i]["POLICY_VERSION_ID"]);
								objSpool.CREATED_BY = EODUserID;	
							
							}
						
							// Update Spool Status
							if(objSpool.PROCESSED == "Y")
							{
								Success++;
							}
							else
							{
								Failed++;
							}

							objSpool.PROCESSED_DATETIME = DateTime.Now;
							
						}
						objWrapper.ClearParameteres();
						UpdateSpoolStatus(objSpool);
					}

					if(Success > 0)
					{
						objWrapper.ClearParameteres();
						objDeposit.Commit(iDepositID,StartedBy,objWrapper);	
						objWrapper.ClearParameteres();
						
					}
					else
					{
						objWrapper.ClearParameteres();
						objDeposit.Delete(iDepositID,objDepositInfo,StartedBy,objWrapper);
						objWrapper.ClearParameteres();
					}
					strResult = strResult + Success  + "   Succeded and " + Failed + " Failed. See Sweep History for further details";
				}
				objWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);
			}
			catch(Exception ex)
			{

				objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				//throw(ex);
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				objWrapper.Dispose();
			}

			
			return strResult ;
		}

		public void UpdateSpoolStatus(CreditCardSpoolInfo objSpoolInfo)
		{
			string strStoredProc = "Proc_UpdateCreditCardSpool";

			objWrapper.AddParameter("@SPOOL_ID",objSpoolInfo.SPOOL_ID);			
			if(objSpoolInfo.REF_DEPOSIT_ID != -1)
				objWrapper.AddParameter("@REF_DEPOSIT_ID",objSpoolInfo.REF_DEPOSIT_ID);
			if(objSpoolInfo.REF_DEPOSIT_DETAIL_ID != -1)
				objWrapper.AddParameter("@REF_DEP_DETAIL_ID", objSpoolInfo.REF_DEPOSIT_DETAIL_ID);
			if(objSpoolInfo.PROCESSED != "")
				objWrapper.AddParameter("@PROCESSED",objSpoolInfo.PROCESSED);
			if(objSpoolInfo.ERROR_DESCRIPTION != "")
				objWrapper.AddParameter("@ERROR_DESCRIPTION",objSpoolInfo.ERROR_DESCRIPTION);
			if(objSpoolInfo.PayPalRefID != "")
				objWrapper.AddParameter("@PAYPALREFID",objSpoolInfo.PayPalRefID );
			if(objSpoolInfo.PayPalResult != "")
				objWrapper.AddParameter("@PAYPALRESULT",objSpoolInfo.PayPalResult );

			//Transaction Log For Failed transactions For EOD Process Itrack 4157.
			if(base.TransactionLog && objSpoolInfo.PayPalResult != "0")
			{
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				if (IsEODProcess)
					objTransactionInfo.RECORDED_BY		=	EODUserID;
				else
					objTransactionInfo.RECORDED_BY		=	objSpoolInfo.CREATED_BY;

				objTransactionInfo.ENTITY_ID				=	objSpoolInfo.EntityID;
				objTransactionInfo.POLICY_ID				=	objSpoolInfo.PolicyID;
				objTransactionInfo.POLICY_VER_TRACKING_ID	=	objSpoolInfo.PolicyVersionID;
                objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1670", "");// "Unable to Process Credit Card Transaction (EOD).";
				objTransactionInfo.CUSTOM_INFO				=	"Error Description = " + objSpoolInfo.ERROR_DESCRIPTION;
					
				int returnResult = objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

			}
			else
				objWrapper.ExecuteNonQuery(strStoredProc);
			
			//objWrapper.ExecuteNonQuery("Proc_UpdateCreditCardSpool");

		}

		public DataSet FetchRecordsForBatchSweep()
		{
			string	strStoredProc =	"Proc_EOD_GetRecordsForCreditCardSweep";
			DataSet dsResult = objWrapper.ExecuteDataSet(strStoredProc);
			objWrapper.ClearParameteres();
			return dsResult;
		}

		#endregion
		#region Batch Processing Process Credit Card
		/// <summary>
		/// Credit Card Spool
		/// </summary>
		/// <param name="StartedBy"></param>
		/// <returns></returns>
		/*public string CommitFailedCreditCardDeposit(int StartedBy)
		{
			objWrapper = new DataWrapper(ConnStr , CommandType.StoredProcedure, 
				DataWrapper.MaintainTransaction.YES,"CCARD Batch", DataWrapper.SetAutoCommit.OFF);	
			string strResult="";
			int Success = 0 , Failed = 0 ; 
			try
			{				
				ClsDeposit objDeposit = null;
				ClsDepositDetails objDepositDetail = new ClsDepositDetails();

				string DepositNote = "Deposit created from Credit Card batch job";
				int BankAccountID = 0 , iDepositID = 0 , iDepositLineItemID =0, FiscalID = 0,
					iSpoolID = 0;
				string strDEPOSIT_NO = "";

				objWrapper.ClearParameteres();
				DataSet ds = objWrapper.ExecuteDataSet("Proc_GetCreditCardParams"); 
				if(ds != null && ds.Tables.Count >0 && ds.Tables[0].Rows.Count > 0 )
				{
					if(ds.Tables[0].Rows[0]["BANK_ACCOUNT"] != null)
					{
						BankAccountID = Convert.ToInt32(ds.Tables[0].Rows[0]["BANK_ACCOUNT"]);
					}
					if(ds.Tables[0].Rows[0]["FISCAL_ID"]!= null)
					{
						FiscalID = Convert.ToInt32(ds.Tables[0].Rows[0]["FISCAL_ID"]);
					}
				}
				
				objWrapper.ClearParameteres();
				ds = null;

				DataSet dsRecordsToSweep = FetchRecordsForDepositBatchSweep();

				strResult = dsRecordsToSweep.Tables[0].Rows.Count.ToString() + " Records to process, ";


				if(dsRecordsToSweep != null && dsRecordsToSweep.Tables.Count>0 &&
					dsRecordsToSweep.Tables[0].Rows.Count > 0)
				{
					
					//Create a New Deposit with mode Credit Card
					objDeposit = new ClsDeposit();
					ClsDepositInfo objDepositInfo = new ClsDepositInfo();

					//Populate Deposit Model object
					objDepositInfo.ACCOUNT_ID = BankAccountID;  
					objDepositInfo.DEPOSIT_NOTE =DepositNote;
					objDepositInfo.DEPOSIT_TRAN_DATE = DateTime.Now;
					objDepositInfo.RECEIPT_MODE = (int)PaymentModes.CreditCard ;
					objDepositInfo.TOTAL_DEPOSIT_AMOUNT = 0; 
					objDepositInfo.CREATED_DATETIME = DateTime.Now;
					strDEPOSIT_NO = Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts.GetDepositNumberByAccountID(FiscalID,BankAccountID);
					objDepositInfo.DEPOSIT_NUMBER = Convert.ToInt32(strDEPOSIT_NO);
					
					if (IsEODProcess)
						objDepositInfo.CREATED_BY = EODUserID;
					else
						objDepositInfo.CREATED_BY = StartedBy;

					objDeposit.Add(objDepositInfo,objWrapper);
					iDepositID = objDepositInfo.DEPOSIT_ID;
					
					
					for(int i = 0 ; i < dsRecordsToSweep.Tables[0].Rows.Count ;i++)
					{
						objWrapper.ClearParameteres();
			
						iSpoolID =	Convert.ToInt32(dsRecordsToSweep.Tables[0].Rows[i]["SPOOL_RECORD_ID"]);

						CreditCardSpoolInfo objSpool = new CreditCardSpoolInfo();
						objSpool.SPOOL_ID = iSpoolID;						

						
							string strTemp= dsRecordsToSweep.Tables[0].Rows[i]["CARD_REFERENCE_ID"].ToString();
							string Amount = dsRecordsToSweep.Tables[0].Rows[i]["TRANSACTION_AMOUNT"].ToString();

							string PolicyNumber = "", CustomerName = "";

							if(dsRecordsToSweep.Tables[0].Rows[i]["POLICY_NUMBER"] != DBNull.Value)
							{
								PolicyNumber  = dsRecordsToSweep.Tables[0].Rows[i]["POLICY_NUMBER"].ToString();
							}
							
							if(dsRecordsToSweep.Tables[0].Rows[i]["CUSTOMER_NAME"] != DBNull.Value)
							{
								CustomerName = dsRecordsToSweep.Tables[0].Rows[i]["CUSTOMER_NAME"].ToString();
							}

							int CustomerID = Convert.ToInt32(dsRecordsToSweep.Tables[0].Rows[i]["ENTITY_ID"]) ; 

                            //Creating Customer Deposit Line item
							ClsDepositDetailsInfo objDetailInfo = new ClsDepositDetailsInfo();
							objDetailInfo.DEPOSIT_ID  = iDepositID;
							objDetailInfo.DEPOSIT_TYPE = "CUST";
							objDetailInfo.PAYOR_TYPE ="CUST";
							objDetailInfo.CUSTOMER_ID = Convert.ToInt32(dsRecordsToSweep.Tables[0].Rows[i]["ENTITY_ID"]);
							objDetailInfo.RECEIPT_FROM_ID = Convert.ToInt32(dsRecordsToSweep.Tables[0].Rows[i]["ENTITY_ID"]);
							objDetailInfo.POLICY_ID = Convert.ToInt32(dsRecordsToSweep.Tables[0].Rows[i]["POLICY_ID"]);
							objDetailInfo.POLICY_VERSION_ID = Convert.ToInt32(dsRecordsToSweep.Tables[0].Rows[i]["POLICY_VERSION_ID"]);
							objDetailInfo.POLICY_NO = dsRecordsToSweep.Tables[0].Rows[i]["POLICY_NUMBER"].ToString();
							objDetailInfo.RECEIPT_AMOUNT =Convert.ToDouble(dsRecordsToSweep.Tables[0].Rows[i]["TRANSACTION_AMOUNT"]);
							objDetailInfo.CREATED_DATETIME = DateTime.Now;
							objDetailInfo.CREATED_BY = StartedBy;


                            objDepositDetail.AddDepositDetail(objWrapper,objDetailInfo,CreatedFrom);
							iDepositLineItemID = objDetailInfo.CD_LINE_ITEM_ID;
							objSpool.REF_DEPOSIT_ID = iDepositID;
							objSpool.REF_DEPOSIT_DETAIL_ID = iDepositLineItemID;
							objSpool.PROCESSED = "Y";
													
							// Update Spool Status
							if(objSpool.PROCESSED == "Y")
							{
								Success++;
							}
							else
							{
								Failed++;
							}

							objSpool.PROCESSED_DATETIME = DateTime.Now;
							
						
						objWrapper.ClearParameteres();
						UpdateCreditCardDepositSpool(objSpool);
					}

					if(Success > 0)
					{
						objWrapper.ClearParameteres();
						objDeposit.Commit(iDepositID,StartedBy,objWrapper);	
						objWrapper.ClearParameteres();
						
					}
					else
					{
						objWrapper.ClearParameteres();
						objDeposit.Delete(iDepositID,objDepositInfo,StartedBy,objWrapper);
						objWrapper.ClearParameteres();
					}
					strResult = strResult + Success  + "   Succeded and " + Failed + " Failed. See Sweep History for further details";
				}
				objWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				objWrapper.Dispose();
			}

			
			return strResult ;
		}
*/
		/// <summary>
		/// [Proc_EOD_GetRecordsACT_CREDIT_CARD_DEPOSIT_SPOOL]
		/// </summary>
		/// <returns></returns>
		/*public DataSet FetchRecordsForDepositBatchSweep()
		{
			string	strStoredProc =	"Proc_EOD_GetRecordsACT_CREDIT_CARD_DEPOSIT_SPOOL";
			DataSet dsResult = objWrapper.ExecuteDataSet(strStoredProc);
			objWrapper.ClearParameteres();
			return dsResult;
		}*/

		/*
		public void UpdateCreditCardDepositSpool(CreditCardSpoolInfo objSpoolInfo)
		{
			string strStoredProc = "Proc_UpdateACT_CREDIT_CARD_DEPOSIT_SPOOL";
			objWrapper.AddParameter("@SPOOL_ID",objSpoolInfo.SPOOL_ID);
			if(objSpoolInfo.REF_DEPOSIT_ID != -1)
				objWrapper.AddParameter("@REF_DEPOSIT_ID",objSpoolInfo.REF_DEPOSIT_ID);
			if(objSpoolInfo.REF_DEPOSIT_DETAIL_ID != -1)
				objWrapper.AddParameter("@REF_DEP_DETAIL_ID", objSpoolInfo.REF_DEPOSIT_DETAIL_ID);
		

			if(base.TransactionLog)
			{
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				if (IsEODProcess)
					objTransactionInfo.RECORDED_BY		=	EODUserID;
				else
					objTransactionInfo.RECORDED_BY		=	objSpoolInfo.CREATED_BY;

				objTransactionInfo.ENTITY_ID				=	objSpoolInfo.EntityID;
				objTransactionInfo.POLICY_ID				=	objSpoolInfo.PolicyID;
				objTransactionInfo.POLICY_VER_TRACKING_ID	=	objSpoolInfo.PolicyVersionID;
				objTransactionInfo.TRANS_DESC				=	"Updated Credit card deposit spool(EOD).";
				//objTransactionInfo.CUSTOM_INFO				=	"Error Description = " + objSpoolInfo.ERROR_DESCRIPTION;
					
				int returnResult = objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

			}
			else
				objWrapper.ExecuteNonQuery(strStoredProc);
			
			

		}
		*/
		/// <summary>
		/// Update Reference ID of Policy for all the Terms.
		/// </summary>
		/// <param name="objDataWrapper"></param>
		/// <param name="objDepositDetailsInfo"></param>
		/// <param name="strReferenceID"></param>
		/// <returns></returns>
		private int UpdatePayPalRefIDCreditCard(DataLayer.DataWrapper objDataWrapper, ClsDepositDetailsInfo objDepositDetailsInfo,string strReferenceID)
		{
			
			string		strStoredProc	=	"Proc_UpdatePAY_PAL_REF_ID_POL_CREDIT_CARD_DETAILS";
			DateTime	RecordDate		=	DateTime.Now;
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID", objDepositDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objDepositDetailsInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objDepositDetailsInfo.POLICY_VERSION_ID);
				
				BlCommon.Security.ClsCrypto objCrypto = new Cms.BusinessLayer.BlCommon.Security.ClsCrypto
					(BlCommon.Security.ClsSecurity.GetPasswordPhrase(objDepositDetailsInfo.CUSTOMER_ID, CREDIT_CARD_PHRASE)); 

				string PayPalRefID = objCrypto.Encrypt(strReferenceID); 

				objDataWrapper.AddParameter("@PAY_PAL_REF_ID",PayPalRefID);
				
                SqlParameter objRetVal = (SqlParameter) objDataWrapper.AddParameter("@RetValue",SqlDbType.Int,ParameterDirection.ReturnValue);

				int returnResult = 0;
				objDataWrapper.ExecuteNonQuery(strStoredProc);

				returnResult = int.Parse(objRetVal.Value.ToString());

				objDataWrapper.ClearParameteres();

				return returnResult;
			}
			catch(Exception ex)
			{
				throw(ex);
			}

		}
		
		#endregion
	}
}
