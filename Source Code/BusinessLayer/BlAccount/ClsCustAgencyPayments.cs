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
	/// Summary description for ClsCustAgencyPayments.
	/// </summary>
	public class ClsCustAgencyPayments : Cms.BusinessLayer.BlAccount.ClsAccount
	{
		private const	string		ACT_CUSTOMER_PAYMENTS_FROM_AGENCY =	"ACT_CUSTOMER_PAYMENTS_FROM_AGENCY";
	
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
		public ClsCustAgencyPayments()
		{
			boolTransactionLog	= base.TransactionLogRequired;
		}
		public ClsCustAgencyPayments(bool transactionLogRequired):this()
		{
			base.TransactionLogRequired = transactionLogRequired;
		}
		#endregion
        
		#region GetPolicyInformationForAddCustAgencyPayemnts
		/// <summary>
		/// Added on 17 Sep 2009 (CustomerAgency pymnts) For Itrack Issue ####6172.
		/// </summary>
		/// <param name="PolNumber"></param>
		/// <returns></returns>
		public static string GetPolicyInformationForAddCustAgencyPayemnts(string PolNumber)
		{
			string	strStoredProc	=	"Proc_GetPolicyInformationFromPolNumber";	
			DataWrapper objDataWrapper=new DataWrapper (ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@POLICY_NUMBER",PolNumber);	
			DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
			if(ds.Tables[0].Rows.Count > 0)	
			{
				return (ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString() + "-"+ ds.Tables[0].Rows[0]["POLICY_ID"].ToString() + "-" +  ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
			}
			else
			{
			  return "";
			}
			
		}
        	#endregion




		#region Fetch Functions
		// Fetches the Customer BALANCE from ACT_CUSTOMER_OPEN_ITEMS :: (TOTAL DUE - TOTAL PAID)
		public static string GetCustomerBal(int CustID, int PolID, int PolVerID,string PolNum, string CalledFrom)
		{
			try
			{
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objWrapper.AddParameter("@CUSTOMER_ID",CustID);
				objWrapper.AddParameter("@POLICY_ID",PolID);
				objWrapper.AddParameter("@POLICY_VERSION_ID",PolVerID);
				objWrapper.AddParameter("@POLICY_NUMBER",PolNum);
				objWrapper.AddParameter("@CALLED_FROM",CalledFrom);
				string strSQL = "Proc_Act_FetchCustomerBalance";
				DataSet objDS = objWrapper.ExecuteDataSet(strSQL);
				if(objDS != null && objDS.Tables.Count > 0 && objDS.Tables[0].Rows.Count > 0)
				{
					//Check for AB : Policy Type reIncluded on 1 Aprli 08
					if(objDS.Tables[0].Columns.Contains("POL_TYPE"))
					{
						string polType = objDS.Tables[0].Rows[0]["POL_TYPE"].ToString();
						if(polType == "AB")
							return "-1";
					}
					
					int	 AgencyID; 
					string CustName="";
					string Dues="";
					string PolId="";
					string PolVerId="";
					string CustId="";
					string polStatus = "";
					string Status = "";
					int AllowEFT;
					double dblNSFamt;
					double dblINSamt;
					double TotDue = double.Parse(objDS.Tables[0].Rows[0]["TOTAL_DUE"].ToString());
					double MinDue = double.Parse(objDS.Tables[0].Rows[0]["MINIMUM_DUE"].ToString());
					if(objDS.Tables.Count > 1 )
					{
						// Check made for Policy Num entered through Lookup or directly into textbox.
						if(objDS.Tables[0].Columns.Contains("AGENCY_ID"))
						{
							if(objDS.Tables[0].Rows[0]["AGENCY_ID"] != System.DBNull.Value)
							{
								if(objDS.Tables[0].Rows.Count > 0)
									AgencyID = int.Parse(objDS.Tables[0].Rows[0]["AGENCY_ID"].ToString());
								else
									AgencyID = 0;
								
								if(objDS.Tables[1].Rows.Count > 0)
									CustName = objDS.Tables[1].Rows[0]["CUSTOMER_NAME"].ToString();

								if(objDS.Tables[2].Rows.Count > 0)
									AllowEFT = int.Parse(objDS.Tables[2].Rows[0]["ALLOWS_EFT"].ToString());
								else
									AllowEFT = 0;

								// NSF Fee
								if(objDS.Tables[3].Rows.Count > 0 && objDS.Tables[3].Rows[0]["NON_SUFFICIENT_FUND_FEES"].ToString() != "")
									dblNSFamt= double.Parse(objDS.Tables[3].Rows[0]["NON_SUFFICIENT_FUND_FEES"].ToString());
								else
									dblNSFamt = 0;

								// INS Fee
								if(objDS.Tables[3].Rows.Count > 0 && objDS.Tables[3].Rows[0]["INSTALLMENT_FEES"].ToString() != "")
									dblINSamt= double.Parse(objDS.Tables[3].Rows[0]["INSTALLMENT_FEES"].ToString());
								else
									dblINSamt = 0;

								// PolicyInfo
								if(objDS.Tables[4].Rows.Count > 0)
								{
									CustId  = objDS.Tables[4].Rows[0]["CUSTOMER_ID"].ToString();
									PolId   = objDS.Tables[4].Rows[0]["POLICY_ID"].ToString();
									PolVerId= objDS.Tables[4].Rows[0]["POLICY_VERSION_ID"].ToString();
									polStatus = objDS.Tables[4].Rows[0]["POLICY_STATUS"].ToString();
									Status    = objDS.Tables[4].Rows[0]["STATUS"].ToString();   
								}
								else
								{
									CustId = "";
									PolId = "";
									PolVerId = "";
								}

								Dues = TotDue.ToString() + '~' + MinDue.ToString() + '~' + AgencyID.ToString() + '~' + CustName.ToString() + '~' + AllowEFT.ToString() + '~' + dblNSFamt.ToString()+ '~' + CustId + '~' + PolId + '~' + PolVerId + '~' + polStatus + '~' + dblINSamt.ToString() + '~' + Status;
							}
							else
								return "0";
						}
					}
					else
						Dues = TotDue.ToString() + '~' + MinDue.ToString();
					//Check for Agency code
					string agencyCode = System.Web.HttpContext.Current.Session["SystemId"].ToString().ToUpper().Trim();
					string strAgencyID = objDS.Tables[0].Rows[0]["AGENCYCODE"].ToString().Trim().ToUpper();
                    string CarrierSystemID = ClsCommon.CarrierSystemID;//System.Configuration.ConfigurationSettings.AppSettings["CarrierSystemID"].ToString().ToUpper().Trim();
						
					if(CarrierSystemID!= agencyCode)
					{
						if(agencyCode!= strAgencyID)
						{
							return "1";
						}
					}
					


						return Dues;
				}
				else
					return "0";
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
									
		}

		// Fetches data from temporary table
		public DataSet GetCustAgencyPayments(string AgenCode)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@AGENCY_CODE", AgenCode);
				DataSet dsTemp = new DataSet();
				dsTemp	= objWrapper.ExecuteDataSet("Proc_GetTempCustAgencyPayments");
				return dsTemp;
			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in Agency/Customer Payments\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}
		}
		//Fetch Customer ID from Policy Number
		public static int GetCustomerId(string policyNumber)
		{
			int custID = 0;
			Cms.DataLayer.DataWrapper objWrapper = null;
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@POLICY_NUMBER", policyNumber);
				DataSet dsTemp = new DataSet();
				dsTemp	= objWrapper.ExecuteDataSet("Proc_FetchCustomerId");
				if(dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
				{
                    custID = int.Parse(dsTemp.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				}
				return custID;
			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in GetCustomerId\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}
		}

		//fetch Data from Agency Data
		public DataSet FetchTempCustAgencyPayments(int IdenRowID)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@IDEN_ROW_ID",IdenRowID);
				DataSet dsTemp = new DataSet();
				dsTemp	= objWrapper.ExecuteDataSet("Proc_FetchTempCustAgencyPayments");
				return dsTemp;
			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in Agency/Customer Payments\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}
		}
		#endregion

		#region Add Functions
		// Adds the records to temporary table on SAVE button click
		public int AddTempCustAgencyPayments(ClsCustAgencyPaymentsInfo objInfo,bool TranLog)
		{
			string		strStoredProc	=	"Proc_InsertTempCustAgencyPayments";
			DateTime	RecordDate		=	DateTime.Now;
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@POLICY_ID",objInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_NUMBER",objInfo.POLICY_NUMBER);
				objDataWrapper.AddParameter("@AGENCY_ID",objInfo.AGENCY_ID);
				objDataWrapper.AddParameter("@TOTAL_DUE",objInfo.TOTAL_DUE);
				objDataWrapper.AddParameter("@MIN_DUE",objInfo.MIN_DUE);
				objDataWrapper.AddParameter("@MODE",objInfo.MODE);
				objDataWrapper.AddParameter("@AMOUNT",objInfo.AMOUNT);
				objDataWrapper.AddParameter("@CREATED_BY_USER",objInfo.CREATED_BY_USER);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@IDEN_ROW_ID",objInfo.IDEN_ROW_ID,SqlDbType.Int,ParameterDirection.Output);
				int RetVal = 0;
				//tran Log
				if(TranLog == true)
				{
					if(TransactionLogRequired)
					{	
						objInfo.TransactLabel = ClsCommon.MapTransactionLabel("account/aspx/AddCustAgencyPayments.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						//objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY_USER;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1671", "");// "Customer Payments from Agency Saved successfully.";
						objTransactionInfo.TRANS_TYPE_ID	=	94;
						objTransactionInfo.CLIENT_ID		=	objInfo.CUSTOMER_ID;
						objTransactionInfo.POLICY_ID		=	objInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID		= objInfo.POLICY_VERSION_ID;




						string strMode = "";
						if(objInfo.MODE == 11975)
							strMode = "Check";
						else
							strMode = "EFT-Sweep";
					
						//Get Cust name
						DataSet dsCust= null;
						dsCust = Cms.BusinessLayer.BlClient.ClsCustomer.GetCustomerInfo(objInfo.CUSTOMER_ID);
						string strCustomerName= "";
						if(dsCust!=null)
						{
							if(dsCust.Tables[0].Rows.Count > 0)
							{
								strCustomerName = dsCust.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString() +  " " + 
									dsCust.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString() + " " +
									dsCust.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"].ToString();

							}
						}
						  //Format Amount Column For Itrack Issue #6172.

						objTransactionInfo.CUSTOM_INFO		=	"Customer Name :" + strCustomerName + ";Total Due :" + "$" + String.Format("{0:n}",objInfo.TOTAL_DUE) + ";Minimum Due :" + "$" + String.Format("{0:n}",objInfo.MIN_DUE) + ";Receipt Amount :" + "$" + String.Format("{0:n}",objInfo.AMOUNT)  + ";Mode :" + strMode;
						

						RetVal	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
					else
					{
						RetVal	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					}
				}
				else
				{
					RetVal	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				//end tran Log
				
				int IDEN_ROW_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objInfo.IDEN_ROW_ID = IDEN_ROW_ID;
				return RetVal; 
		 	}
			catch(Exception ex)
			{
				throw(ex);
			}
		}

		// Adds the records to main table from temporary table on CONFIRM button click
		public int AddActualCustAgencyPayments(int idenRowID,int userID)
		{
			string		strStoredProc	=	"Proc_InsertCustAgencyPayments";
			DateTime	RecordDate		=	DateTime.Now;
			try
			{
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@IDEN_ROW_ID",idenRowID);
				int RetVal = 0;
				//tran Log
				if(TransactionLogRequired)
				{	
					//objInfo.TransactLabel = ClsCommon.MapTransactionLabel("account/aspx/AddCustAgencyPayments.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					//objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	userID;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1672", "");// "Customer Payments from Agency Confirmed successfully.";
					
					//GET AGENCY DETAILS
					string strPolicyNumber = "";
					string strTotalDue = "";
					string strMinimumDue = "";
					string strReceiptAmount = "";
					string strMode = "";
					int intCustomerID =  0,intPolicyId = 0,intPolicyVerId = 0;
					DataSet dsAgency = null;
					dsAgency = FetchTempCustAgencyPayments(idenRowID);
					if(dsAgency!=null)
					{
						if(dsAgency.Tables[0].Rows.Count > 0)
						{
							strPolicyNumber = dsAgency.Tables[0].Rows[0]["POLICY_NUMBER"].ToString();
							strTotalDue = dsAgency.Tables[0].Rows[0]["TOTAL_DUE"].ToString();
							strMinimumDue = dsAgency.Tables[0].Rows[0]["MIN_DUE"].ToString();
							strReceiptAmount = dsAgency.Tables[0].Rows[0]["AMOUNT"].ToString();
							strMode = dsAgency.Tables[0].Rows[0]["MODE"].ToString();    
							intCustomerID = Convert.ToInt32(dsAgency.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
							//Added on March 25 Itrack 3933
							intPolicyId  =  Convert.ToInt32(dsAgency.Tables[0].Rows[0]["POLICY_ID"].ToString());
							intPolicyVerId  =  Convert.ToInt32(dsAgency.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());


						}
					}
					if(strMode == "11975")
						strMode = "Check";
					else
						strMode = "EFT-Sweep";
                                        					
					//Get Cust name
					DataSet dsCust= null;
					dsCust = Cms.BusinessLayer.BlClient.ClsCustomer.GetCustomerInfo(intCustomerID);
					string strCustomerName= "";
					if(dsCust!=null)
					{
						if(dsCust.Tables[0].Rows.Count > 0)
						{
							strCustomerName = dsCust.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString() +  " " + 
								dsCust.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString() + " " +
								dsCust.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"].ToString();

						}
					}
					//Added on March 25 Itrack #3933
					objTransactionInfo.TRANS_TYPE_ID	=	94;
					objTransactionInfo.CLIENT_ID		=	intCustomerID;
					objTransactionInfo.POLICY_ID		=	intPolicyId;
					objTransactionInfo.POLICY_VER_TRACKING_ID		=	intPolicyVerId;
					
					//objTransactionInfo.CUSTOM_INFO		=	"Policy Number :" + strPolicyNumber +";Customer Name :" + strCustomerName + ";Total Due :" + strTotalDue + ";Minimum Due :" + strMinimumDue + ";Receipt Amount :" + strReceiptAmount + ";Mode :" + strMode;
					//Formated FOr Itrack issue # 6598.
					objTransactionInfo.CUSTOM_INFO		=	"Customer Name :" + strCustomerName + ";Total Due :" + "$" +  String.Format("{0:n}",double.Parse(strTotalDue)) + ";Minimum Due :" + "$" +   String.Format("{0:n}",double.Parse(strMinimumDue)) + ";Receipt Amount :" + "$" + String.Format("{0:n}",double.Parse(strReceiptAmount)) + ";Mode :" + strMode;
					RetVal	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					RetVal	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				//end tran Log
				return RetVal; 
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}

		#endregion

		#region Update/Delete Functions
		// Updates the records in temporary table
		public int UpdateTempCustAgencyPayments(ClsCustAgencyPaymentsInfo objInfo,bool TranLog)
		{
			string		strStoredProc	=	"Proc_UpdateTempCustAgencyPayments";
			DateTime	RecordDate		=	DateTime.Now;
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@IDEN_ROW_ID",objInfo.IDEN_ROW_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_NUMBER",objInfo.POLICY_NUMBER);
				objDataWrapper.AddParameter("@AGENCY_ID",objInfo.AGENCY_ID);
				objDataWrapper.AddParameter("@TOTAL_DUE",objInfo.TOTAL_DUE);
				objDataWrapper.AddParameter("@MIN_DUE",objInfo.MIN_DUE);
				objDataWrapper.AddParameter("@MODE",objInfo.MODE);
				objDataWrapper.AddParameter("@AMOUNT",objInfo.AMOUNT);
				objDataWrapper.AddParameter("@CREATED_BY_USER",objInfo.CREATED_BY_USER);
				int RetVal = 0;
				//tran Log
				if(TranLog == true)
				{
					if(TransactionLogRequired)
					{	
						objInfo.TransactLabel = ClsCommon.MapTransactionLabel("account/aspx/AddCustAgencyPayments.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						//objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY_USER;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1673", "");// "Customer Payments from Agency Modified successfully.";
						string strMode = "";
						if(objInfo.MODE == 11975)
							strMode = "Check";
						else
							strMode = "EFT-Sweep";
					
						//Get Cust name
						DataSet dsCust= null;
						dsCust = Cms.BusinessLayer.BlClient.ClsCustomer.GetCustomerInfo(objInfo.CUSTOMER_ID);
						string strCustomerName= "";
						if(dsCust!=null)
						{
							if(dsCust.Tables[0].Rows.Count > 0)
							{
								strCustomerName = dsCust.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString() +  " " + 
									dsCust.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString() + " " +
									dsCust.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"].ToString();

							}
						}

						//Added on March 25 Itrack #3933
						objTransactionInfo.TRANS_TYPE_ID				=	94;
						objTransactionInfo.CLIENT_ID					=	objInfo.CUSTOMER_ID;
						objTransactionInfo.POLICY_ID					=	objInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID		=	objInfo.POLICY_VERSION_ID;
						//Formated Added For Itrack Issue # 6598	
						objTransactionInfo.CUSTOM_INFO		=	"Customer Name :" + strCustomerName + ";Total Due :" + "$" +  String.Format("{0:n}",objInfo.TOTAL_DUE) + ";Minimum Due :" + "$" +  String.Format("{0:n}",objInfo.MIN_DUE) + ";Receipt Amount :" + "$"  + String.Format("{0:n}",objInfo.AMOUNT) + ";Mode :" + strMode;

					
					
						RetVal	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
					else
					{
						RetVal	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					}
				}
				else
				{
                    RetVal	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				return RetVal; 
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}

		// Deletes from Temporary table
		public int DeleteTempCustAgencyPayments(ClsCustAgencyPaymentsInfo objInfo)
		{
			string		strStoredProc	=	"Proc_DeleteTempCustAgencyPayments";
			DateTime	RecordDate		=	DateTime.Now;
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@ROW_ID",objInfo.IDEN_ROW_ID);
				int RetVal = 0;
				//tran Log
				if(TransactionLogRequired)
				{	
					objInfo.TransactLabel = ClsCommon.MapTransactionLabel("account/aspx/AddCustAgencyPayments.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					//objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY_USER;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1674", "");// "Customer Payments from Agency Deleted successfully.";
					
					//GET AGENCY DETAILS
					string strPolicyNumber = "";
					string strTotalDue = "";
					string strMinimumDue = "";
					string strReceiptAmount = "";
					string strMode = "";
					int intCustomerID =  0,intPolicyId=0,intPolicyVerId=0;
					DataSet dsAgency = null;
					dsAgency = FetchTempCustAgencyPayments(objInfo.IDEN_ROW_ID);
					if(dsAgency!=null)
					{
						if(dsAgency.Tables[0].Rows.Count > 0)
						{
							strPolicyNumber = dsAgency.Tables[0].Rows[0]["POLICY_NUMBER"].ToString();
							strTotalDue = dsAgency.Tables[0].Rows[0]["TOTAL_DUE"].ToString();
							strMinimumDue = dsAgency.Tables[0].Rows[0]["MIN_DUE"].ToString();
							strReceiptAmount = dsAgency.Tables[0].Rows[0]["AMOUNT"].ToString();
							strMode = dsAgency.Tables[0].Rows[0]["MODE"].ToString();    
							intCustomerID = Convert.ToInt32(dsAgency.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
							intPolicyId = Convert.ToInt32(dsAgency.Tables[0].Rows[0]["POLICY_ID"].ToString());
							intPolicyVerId = Convert.ToInt32(dsAgency.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

						}
					}
					if(strMode == "11975")
						strMode = "Check";
					else
						strMode = "EFT-Sweep";
                                        					
					//Get Cust name
					DataSet dsCust= null;
					dsCust = Cms.BusinessLayer.BlClient.ClsCustomer.GetCustomerInfo(intCustomerID);
					string strCustomerName= "";
					if(dsCust!=null)
					{
						if(dsCust.Tables[0].Rows.Count > 0)
						{
							strCustomerName = 
								dsCust.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString() +  " " + 
								dsCust.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString() + " " +
								dsCust.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"].ToString();

						}
					}

					//Added on March 25 Itrack #3933
					objTransactionInfo.TRANS_TYPE_ID	=	94;
					objTransactionInfo.CLIENT_ID		=	intCustomerID;
					objTransactionInfo.POLICY_ID		=	intPolicyId;
					objTransactionInfo.POLICY_VER_TRACKING_ID		=	intPolicyVerId;

					objTransactionInfo.CUSTOM_INFO	=	"Customer Name :" + strCustomerName + ";Total Due :" + strTotalDue + ";Minimum Due :" + strMinimumDue + ";Receipt Amount :" + strReceiptAmount + ";Mode :" + strMode;
					RetVal	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					RetVal	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				//end tran Log
				return RetVal; 
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		
		#endregion

		
	}
}
