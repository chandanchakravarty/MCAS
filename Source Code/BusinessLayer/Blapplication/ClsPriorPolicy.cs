/******************************************************************************************
<Author				: -   Vijay Joshi
<Start Date				: -	4/26/2005 12:27:59 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: -  4/5/2005
<Modified By				: - Anurag Verma
<Purpose				: - Removing use of app_id and app_version_id

<Modified Date			: - Anshuman
<Modified By			: - June 07, 2005
<Purpose				: - transaction description modified
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Application;
using Cms.BusinessLayer.BlCommon;  

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsPriorPolicy : Cms.BusinessLayer.BlApplication.clsapplication
	{
		private const	string		APP_PRIOR_CARRIER_INFO	=	"APP_PRIOR_CARRIER_INFO";
		private const	string		INSERT_UPDATE_PROC		=	"Proc_InsertPriorPolicy";
		private const	string		GET_PRIOR_POLICY_PROC	=	"Proc_GetPriorPolicy";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivatePriorPolicy";
		private SqlParameter objParam;
		
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
		
		/// <summary>
		/// Add the parameters of proc from model object.
		/// </summary>
		/// <param name="objPriorPolicyInfo">Model class object.</param>
		/// <param name="objDatawrapper">Datawrapper class object.</param>
		/// <returns>void</returns>
		private void AddParameters(Cms.Model.Application.ClsPriorPolicyInfo objPriorPolicyInfo, DataWrapper objDataWrapper,char InsertUpdate)
		{
			objDataWrapper.AddParameter("@CUSTOMER_ID",objPriorPolicyInfo.CUSTOMER_ID);
			//objDataWrapper.AddParameter("@APP_PRIOR_CARRIER_INFO_ID",null,System.Data.SqlDbType.SmallInt,System.Data.ParameterDirection.Output );
				
			objDataWrapper.AddParameter("@OLD_POLICY_NUMBER",objPriorPolicyInfo.OLD_POLICY_NUMBER);
			objDataWrapper.AddParameter("@CARRIER",objPriorPolicyInfo.CARRIER);
			if (objPriorPolicyInfo.LOB != "0")
			{
				objDataWrapper.AddParameter("@LOB",objPriorPolicyInfo.LOB);
			}
			else
			{
				objDataWrapper.AddParameter("@LOB",null);
			}
			objDataWrapper.AddParameter("@SUB_LOB",null);

			if(objPriorPolicyInfo.EFF_DATE.Ticks != 0)
				objDataWrapper.AddParameter("@EFF_DATE",objPriorPolicyInfo.EFF_DATE);
			else
				objDataWrapper.AddParameter("@EFF_DATE",null);

			if(objPriorPolicyInfo.EXP_DATE.Ticks != 0)
				objDataWrapper.AddParameter("@EXP_DATE",objPriorPolicyInfo.EXP_DATE);
			else
				objDataWrapper.AddParameter("@EXP_DATE",null);

			objDataWrapper.AddParameter("@POLICY_CATEGORY",objPriorPolicyInfo.POLICY_CATEGORY);
			objDataWrapper.AddParameter("@POLICY_TERM_CODE",objPriorPolicyInfo.POLICY_TERM_CODE);
			objDataWrapper.AddParameter("@POLICY_TYPE",objPriorPolicyInfo.POLICY_TYPE);
			if(objPriorPolicyInfo.YEARS_PRIOR_COMP==0)
			{
				objDataWrapper.AddParameter("@YEARS_PRIOR_COMP",null);
			}
			else
			{
				objDataWrapper.AddParameter("@YEARS_PRIOR_COMP",objPriorPolicyInfo.YEARS_PRIOR_COMP);
			}
			if(objPriorPolicyInfo.ACTUAL_PREM==0)
			{
				objDataWrapper.AddParameter("@ACTUAL_PREM",null);
			}
			else
			{
				objDataWrapper.AddParameter("@ACTUAL_PREM",objPriorPolicyInfo.ACTUAL_PREM);
			}
			objDataWrapper.AddParameter("@ASSIGNEDRISKYN",objPriorPolicyInfo.ASSIGNEDRISKYN);
			
			if (objPriorPolicyInfo.PRIOR_PRODUCER_INFO_ID != 0)
			{
				objDataWrapper.AddParameter("@PRIOR_PRODUCER_INFO_ID",objPriorPolicyInfo.PRIOR_PRODUCER_INFO_ID);
			}
			else
			{
				objDataWrapper.AddParameter("@PRIOR_PRODUCER_INFO_ID",null);
			}
			if (objPriorPolicyInfo.RISK_NEW_AGENCY == "Y")
			{
				objDataWrapper.AddParameter("@RISK_NEW_AGENCY",objPriorPolicyInfo.RISK_NEW_AGENCY);
			}
			else
			{
				objDataWrapper.AddParameter("@RISK_NEW_AGENCY",null);
			}

			//Added for Itrack issue 6449 on 23 Oct 09
			if(objPriorPolicyInfo.PRIOR_BI_CSL_LIMIT == "")
			{
				objDataWrapper.AddParameter("@PRIOR_BI_CSL_LIMIT",System.DBNull.Value);
			}
			else
			{
				objDataWrapper.AddParameter("@PRIOR_BI_CSL_LIMIT",objPriorPolicyInfo.PRIOR_BI_CSL_LIMIT);
			}
			objDataWrapper.AddParameter("@MOD_FACTOR",objPriorPolicyInfo.MOD_FACTOR);
			objDataWrapper.AddParameter("@ANNUAL_PREM",objPriorPolicyInfo.ANNUAL_PREM);
			objDataWrapper.AddParameter("@INSERTUPDATE",InsertUpdate.ToString());
			

		}

		#endregion

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsPriorPolicy()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objPriorPolicyInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int AddPolicy(ClsPriorPolicyInfo objPriorPolicyInfo,DataWrapper objDataWrapper)
		{
			DateTime	RecordDate		=	DateTime.Now;
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

				
				//Adding the parameters to the datawrapper class object
				AddParameters(objPriorPolicyInfo,objDataWrapper,'I');
				objParam = (SqlParameter)objDataWrapper.AddParameter("@APP_PRIOR_CARRIER_INFO_ID",null,System.Data.SqlDbType.SmallInt,System.Data.ParameterDirection.Output );
				objDataWrapper.AddParameter("@CREATED_BY",objPriorPolicyInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objPriorPolicyInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY", null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", null);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objPriorPolicyInfo.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel("application/Aspx/AddPriorPolicy.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objPriorPolicyInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					//objTransactionInfo.APP_ID = objPriorPolicyInfo.APP_ID;
					//objTransactionInfo.APP_VERSION_ID = objPriorPolicyInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objPriorPolicyInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objPriorPolicyInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New Prior Policy is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(INSERT_UPDATE_PROC);//,objTransactionInfo);
					
					//Filling the APP_PRIOR_CARRIER_INFO_ID fiels, which proc has set
					objPriorPolicyInfo.APP_PRIOR_CARRIER_INFO_ID = int.Parse(objParam.Value.ToString());

					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(INSERT_UPDATE_PROC);
				}
				
				//objDataWrapper.ClearParameteres();
				//objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			
		}

		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objPriorPolicyInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsPriorPolicyInfo objPriorPolicyInfo)
		{
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				
				//Adding the parameters to the datawrapper class object
				AddParameters(objPriorPolicyInfo,objDataWrapper,'I');
				objParam = (SqlParameter)objDataWrapper.AddParameter("@APP_PRIOR_CARRIER_INFO_ID",null,System.Data.SqlDbType.SmallInt,System.Data.ParameterDirection.Output );
				objDataWrapper.AddParameter("@CREATED_BY",objPriorPolicyInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objPriorPolicyInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY", null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", null);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objPriorPolicyInfo.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel("application/Aspx/AddPriorPolicy.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objPriorPolicyInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					//objTransactionInfo.APP_ID = objPriorPolicyInfo.APP_ID;
					//objTransactionInfo.APP_VERSION_ID = objPriorPolicyInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objPriorPolicyInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objPriorPolicyInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New Prior Policy is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(INSERT_UPDATE_PROC,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(INSERT_UPDATE_PROC);
				}
				
				//Filling the APP_PRIOR_CARRIER_INFO_ID fiels, which proc has set
				objPriorPolicyInfo.APP_PRIOR_CARRIER_INFO_ID = int.Parse(objParam.Value.ToString());

				objDataWrapper.ClearParameteres();

				//UT Tier
				if(objPriorPolicyInfo.LOB == "2")
				{
					ClsUnderwritingTier objTier = new ClsUnderwritingTier();
					objTier.UpdateUTierPriorInfo(objPriorPolicyInfo.CUSTOMER_ID,objDataWrapper);
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
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}
		#endregion

		#region Update method

		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldPriorPolicyInfo">Model object having old information</param>
		/// <param name="objPriorPolicyInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int UpdatePolicy(ClsPriorPolicyInfo objPriorPolicyInfo,DataWrapper objDataWrapper)
		{
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			//string strTranXML;
			int returnResult = 0;
			//objBuilder.TableName = objOldPriorPolicyInfo.TableInfo.TableName;
			//objBuilder.WhereClause = " WHERE APP_ID = " + objOldPriorPolicyInfo.APP_ID.ToString();
			
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				
			//Adding the parameters to the datawrapper class object
			AddParameters(objPriorPolicyInfo,objDataWrapper,'U');								

			objDataWrapper.AddParameter("@APP_PRIOR_CARRIER_INFO_ID", objPriorPolicyInfo.APP_PRIOR_CARRIER_INFO_ID);
			objDataWrapper.AddParameter("@CREATED_BY", null);
			objDataWrapper.AddParameter("@CREATED_DATETIME", null);
			objDataWrapper.AddParameter("@MODIFIED_BY", objPriorPolicyInfo.MODIFIED_BY);
			objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objPriorPolicyInfo.LAST_UPDATED_DATETIME);
	
			returnResult = objDataWrapper.ExecuteNonQuery(INSERT_UPDATE_PROC);
				
			return returnResult;
			
		}

		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldPriorPolicyInfo">Model object having old information</param>
		/// <param name="objPriorPolicyInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsPriorPolicyInfo objOldPriorPolicyInfo,ClsPriorPolicyInfo objPriorPolicyInfo)
		{
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			string strTranXML;
			int returnResult = 0;
			//objBuilder.TableName = objOldPriorPolicyInfo.TableInfo.TableName;
			//objBuilder.WhereClause = " WHERE APP_ID = " + objOldPriorPolicyInfo.APP_ID.ToString();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				
				//Adding the parameters to the datawrapper class object
				AddParameters(objPriorPolicyInfo,objDataWrapper,'U');								

				objDataWrapper.AddParameter("@APP_PRIOR_CARRIER_INFO_ID", objOldPriorPolicyInfo.APP_PRIOR_CARRIER_INFO_ID);
				objDataWrapper.AddParameter("@CREATED_BY", null);
				objDataWrapper.AddParameter("@CREATED_DATETIME", null);
				objDataWrapper.AddParameter("@MODIFIED_BY", objPriorPolicyInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objPriorPolicyInfo.LAST_UPDATED_DATETIME);

				try 
				{
					if(TransactionLogRequired) 
					{
						objPriorPolicyInfo.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel(
							"Application/Aspx/AddPriorPolicy.aspx.resx");
						
						strTranXML		=	objBuilder.GetTransactionLogXML(objOldPriorPolicyInfo,objPriorPolicyInfo);

						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						//objTransactionInfo.APP_ID = objPriorPolicyInfo.APP_ID;
						//objTransactionInfo.APP_VERSION_ID = objPriorPolicyInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objPriorPolicyInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objPriorPolicyInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Prior Policy is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(INSERT_UPDATE_PROC,objTransactionInfo);

					}
					else
					{
						returnResult = objDataWrapper.ExecuteNonQuery(INSERT_UPDATE_PROC);
					}

					//UT Tier
					if(objPriorPolicyInfo.LOB == "2")
					{
						ClsUnderwritingTier objTier = new ClsUnderwritingTier();
						objTier.UpdateUTierPriorInfo(objPriorPolicyInfo.CUSTOMER_ID,objDataWrapper);
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

		#region GetPriorPolicyInfo
		public static string GetPriorPolicyInfo(int intCustomerId,int intCarrerInfoId )
		{

			DataSet dsPolicyInfo = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
				objDataWrapper.AddParameter("@APP_PRIOR_CARRIER_INFO_ID",intCarrerInfoId);
                objDataWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);


				dsPolicyInfo = objDataWrapper.ExecuteDataSet(GET_PRIOR_POLICY_PROC);
				
				if (dsPolicyInfo.Tables[0].Rows.Count != 0)
				{
					return dsPolicyInfo.GetXml();
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

		#region GetLOBXML

		/// <summary>
		/// This is used to Generate XML for LOB AND SUB LOB
		///  this function will stop post back of page to reterive value of other drop down on selected index change
		/// </summary>
		/// <returns></returns>
		public static string GetXmlForLob()
		{
			string strSql = "Proc_GetLobAndSubLOBs";
			string strReturnValue;

			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,strSql);

			if (objDataSet.Tables[0].Rows.Count == 0)
			{
				strReturnValue = "";
			}
			else
			{
				strReturnValue = objDataSet.GetXml();
			}

			return strReturnValue;
		}

		#endregion

		#region Delete

		public int Delete(int intCustomerId, int intPolicyId,int intUserId )
		{
			
			string strStoredProc = "Proc_DeletePriorPolicy";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	

			objWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
			objWrapper.AddParameter("@APP_PRIOR_CARRIER_INFO_ID",intPolicyId);
			SqlParameter sqlParamRetVal = (SqlParameter) objWrapper.AddParameter("@RETVAL",SqlDbType.Int,ParameterDirection.ReturnValue);
			try
			{
				//obj.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel("application/Aspx/AddPriorPolicy.aspx.resx");
				//SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				//string strTranXML = objBuilder.GetTransactionLogXML(objPriorPolicyInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.CLIENT_ID = intCustomerId;
				objTransactionInfo.RECORDED_BY=	intUserId;
				objTransactionInfo.TRANS_DESC		=	"Prior Policy is deleted successfully.";
				//objTransactionInfo.CHANGE_XML		=	strTranXML;
					
				objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			//UT Tier
			ClsUnderwritingTier objTier = new ClsUnderwritingTier();
			objTier.UpdateUTierPriorInfo(intCustomerId,objWrapper);
			

			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);	

			return 1;
		}
		#endregion
		
		public int CheckPriorPolicyExistence(ClsPriorPolicyInfo objInfo,DataWrapper objDataWrapper)
		{
			string strStoredProc =	"Proc_CheckPriorPolicyExistence";

			objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@OLD_POLICY_NUMBER",objInfo.OLD_POLICY_NUMBER);
			objDataWrapper.AddParameter("@CARRIER",objInfo.CARRIER);
			objDataWrapper.AddParameter("@LOB",null);

			SqlParameter retVal = (SqlParameter)objDataWrapper.AddParameter("@APP_PRIOR_CARRIER_INFO_ID",SqlDbType.Int,ParameterDirection.Output);


			objDataWrapper.ExecuteNonQuery(strStoredProc);
			
			int returnResult = Convert.ToInt32(retVal.Value);

			return returnResult;

		}

		//Done for Itrack Issue 6708 on 19 Nov 09
		#region fetching prior policy from IIX and Inserting in prior policy Table
		
		public string InsertToPriorPolicyTable(Cms.Model.Client.ClsCustomerInfo objCustomerInfo, string retPriorPolicy, string customer_name,int userID)
		{
			ClsPriorPolicyInfo objPriorPolicyInfo;
			objPriorPolicyInfo  = new ClsPriorPolicyInfo();
			string lblMessage="";
			int IntCUSTOMER_ID;//,IntAPP_POL_ID,IntVERSION_ID;

			IntCUSTOMER_ID		=int.Parse(objCustomerInfo.CustomerId.ToString());

			try
			{
				retPriorPolicy = retPriorPolicy.Replace("&","&amp;");

				XmlDocument priorxml = new XmlDocument();
				priorxml.LoadXml(retPriorPolicy);

				XmlNodeList priornodes = priorxml.SelectNodes("CoverageVerifier");
				if(priornodes.Count>0)
				{
					foreach(XmlNode priornode in priornodes)
					{
						objPriorPolicyInfo.CUSTOMER_ID = objCustomerInfo.CustomerId;
						objPriorPolicyInfo.CREATED_BY = userID;
						objPriorPolicyInfo.CREATED_DATETIME = DateTime.Now;//Done for Itrack Issue 6708 on 26 Nov 09
						try 
						{ 
							if(priornode.SelectSingleNode("SummarySection/PolicySearchInfo/PolicyNumber")!=null && priornode.SelectSingleNode("SummarySection/PolicySearchInfo/PolicyNumber").InnerText !="")
							{
								objPriorPolicyInfo.OLD_POLICY_NUMBER =  priornode.SelectSingleNode("SummarySection/PolicySearchInfo/PolicyNumber").InnerText; 
							}
						}
						catch(Exception){}
						
						
						try 
						{
							if(priornode.SelectSingleNode("SummarySection/PolicySearchInfo/CarrierName")!=null && priornode.SelectSingleNode("SummarySection/PolicySearchInfo/CarrierName").InnerText !="")
							{
								objPriorPolicyInfo.CARRIER =  priornode.SelectSingleNode("SummarySection/PolicySearchInfo/CarrierName").InnerText; 
							}
						}
						catch(Exception){}

						
						try
						{
							if(priornode.SelectSingleNode("SummarySection/RequestLevelInfo/LOB")!=null && priornode.SelectSingleNode("SummarySection/RequestLevelInfo/LOB").InnerText !="")
							{
								if(priornode.SelectSingleNode("SummarySection/RequestLevelInfo/LOB").InnerText == "PP")
								objPriorPolicyInfo.LOB = "2";
							}
						}
						catch(Exception){}

						try 
						{ 
							if(priornode.SelectSingleNode("Policy/DetailSection/PolicyDetailInfo/TermEffectiveDate")!=null && priornode.SelectSingleNode("Policy/DetailSection/PolicyDetailInfo/TermEffectiveDate").InnerText !="")
							{
								objPriorPolicyInfo.EFF_DATE = Convert.ToDateTime(priornode.SelectSingleNode("Policy/DetailSection/PolicyDetailInfo/TermEffectiveDate").InnerText.Substring(0,4) + "-" + priornode.SelectSingleNode("Policy/DetailSection/PolicyDetailInfo/TermEffectiveDate").InnerText.Substring(4,2) + "-" + priornode.SelectSingleNode("Policy/DetailSection/PolicyDetailInfo/TermEffectiveDate").InnerText.Substring(6,2)); 
							}
						} 
						catch(Exception){}

						try 
						{ 
							if(priornode.SelectSingleNode("Policy/DetailSection/PolicyDetailInfo/TermExpirationDate")!=null && priornode.SelectSingleNode("Policy/DetailSection/PolicyDetailInfo/TermExpirationDate").InnerText !="")
							{
								objPriorPolicyInfo.EXP_DATE = Convert.ToDateTime(priornode.SelectSingleNode("Policy/DetailSection/PolicyDetailInfo/TermExpirationDate").InnerText.Substring(0,4) + "-" + priornode.SelectSingleNode("Policy/DetailSection/PolicyDetailInfo/TermExpirationDate").InnerText.Substring(4,2) + "-" + priornode.SelectSingleNode("Policy/DetailSection/PolicyDetailInfo/TermExpirationDate").InnerText.Substring(6,2)); 
							}
						} 
						catch(Exception){}

						try 
						{ 
							if(priornode.SelectSingleNode("Policy/DetailSection/PolicyDetailInfo/PolicyType")!=null && priornode.SelectSingleNode("Policy/DetailSection/PolicyDetailInfo/PolicyType").InnerText !="")
							{
								if(priornode.SelectSingleNode("Policy/DetailSection/PolicyDetailInfo/PolicyType").InnerText =="AU")
								 objPriorPolicyInfo.POLICY_CATEGORY = "8501"; 
							}
						} 
						catch(Exception){}

						try 
						{ 
							if(priornode.SelectSingleNode("SummarySection/PolicySearchInfo/NumOfRenewals")!=null && priornode.SelectSingleNode("SummarySection/PolicySearchInfo/NumOfRenewals").InnerText !="")
							{
								objPriorPolicyInfo.YEARS_PRIOR_COMP = int.Parse(priornode.SelectSingleNode("SummarySection/PolicySearchInfo/NumOfRenewals").InnerText); 
							} 
						}
						catch(Exception){}

						try 
						{
							if(priornode.SelectSingleNode("Policy/DetailSection/PolicyDetailInfo/RiskClassCode")!=null && priornode.SelectSingleNode("Policy/DetailSection/PolicyDetailInfo/RiskClassCode").InnerText !="")
							{
								if(priornode.SelectSingleNode("Policy/DetailSection/PolicyDetailInfo/RiskClassCode").InnerText == "S")
								{
									objPriorPolicyInfo.ASSIGNEDRISKYN = "N";
								}
								else
								{
									objPriorPolicyInfo.ASSIGNEDRISKYN = "Y";
								}
							}
						} 
						catch(Exception){}

						try 
						{ 
							if(priornode.SelectSingleNode("Policy/DetailSection/PolicyDetailInfo/PolicyPremium")!=null && priornode.SelectSingleNode("Policy/DetailSection/PolicyDetailInfo/PolicyPremium").InnerText !="")
							{
								objPriorPolicyInfo.ANNUAL_PREM = String.Format("{0:n}",double.Parse(priornode.SelectSingleNode("Policy/DetailSection/PolicyDetailInfo/PolicyPremium").InnerText)).Replace(".00","");
							}
						} 
						catch(Exception){}

						try 
						{ 
							if(priornode.SelectSingleNode("Policy/DetailSection/CoverageInfo/AddressCoverageInfo/CSILimit")!=null && priornode.SelectSingleNode("Policy/DetailSection/CoverageInfo/AddressCoverageInfo/CSILimit").InnerText !="" && priornode.SelectSingleNode("Policy/DetailSection/CoverageInfo/AddressCoverageInfo/CSILimit").InnerText != "00000000")
							{
								objPriorPolicyInfo.PRIOR_BI_CSL_LIMIT = Convert.ToString(int.Parse(priornode.SelectSingleNode("Policy/DetailSection/CoverageInfo/AddressCoverageInfo/CSILimit").InnerText)/1);
							}
							else
							{
								if(priornode.SelectSingleNode("Policy/DetailSection/CoverageInfo/AddressCoverageInfo/OccuranceLimit")!=null && priornode.SelectSingleNode("Policy/DetailSection/CoverageInfo/AddressCoverageInfo/OccuranceLimit").InnerText !="" && priornode.SelectSingleNode("Policy/DetailSection/CoverageInfo/AddressCoverageInfo/OccuranceLimit").InnerText != "00000000")
									objPriorPolicyInfo.PRIOR_BI_CSL_LIMIT = Convert.ToString(int.Parse(priornode.SelectSingleNode("Policy/DetailSection/CoverageInfo/AddressCoverageInfo/IndividualLimit").InnerText)/1000) + "/" + String.Format("{0:n}",double.Parse(priornode.SelectSingleNode("Policy/DetailSection/CoverageInfo/AddressCoverageInfo/OccuranceLimit").InnerText)).Replace(".00","");
								else
								{
									objPriorPolicyInfo.PRIOR_BI_CSL_LIMIT = Convert.ToString(int.Parse(priornode.SelectSingleNode("Policy/DetailSection/CoverageInfo/AddressCoverageInfo/IndividualLimit").InnerText)/1000);
								}
							}
						} 
						catch(Exception){}

						//Calling the add method of business layer class
						#region Check Update
						DataSet dsPriorPolicy = GetExistingPriorPolicy(objPriorPolicyInfo);
						#endregion
						if(dsPriorPolicy.Tables[0].Rows.Count == 0)
						{	
							int intRetVal = this.Add(objPriorPolicyInfo);
							//lossadded = 1;
							lblMessage += "Prior Policy Record added for: " + customer_name + "<br>";
							UpdatePriorInfoOrdered(int.Parse(objPriorPolicyInfo.CUSTOMER_ID.ToString()));
					
						}
					}
				}
				return lblMessage;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		#endregion
		
		public DataSet GetExistingPriorPolicy(ClsPriorPolicyInfo objPriorPolicyInfo)
		{
			string strStoredProc = "Proc_CheckPriorPolicyExists";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			DataSet ds = new DataSet();
			objDataWrapper.AddParameter("@CUSTOMER_ID",objPriorPolicyInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@OLD_POLICY_NUMBER",objPriorPolicyInfo.OLD_POLICY_NUMBER);
			objDataWrapper.AddParameter("@LOB",objPriorPolicyInfo.LOB);
			ds = objDataWrapper.ExecuteDataSet(strStoredProc);

			return ds;
		}
	
		public void UpdatePriorInfoOrdered(int customerID)
		{
			string strStoredProc = "Proc_UpdatePriorInfoOrdered";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objDataWrapper.ExecuteNonQuery(strStoredProc);
		}
		//Added till here
	}
}
