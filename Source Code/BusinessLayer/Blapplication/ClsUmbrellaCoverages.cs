/******************************************************************************************
<Author				: -   Pravesh
<Start Date			: -	 10/10/2006 4:50:05 PM
<End Date			: -	
<Description		: - coverage Class for Umbrella
<Review Date		: - 
<Reviewed By		: - 	
*******************************************************************************************/ 
using System;
using System.Data; 
using System.Data.SqlClient; 
using System.Text; 
using System.Xml; 
using System.Collections;   
using Cms.DataLayer;
using Cms.Model.Application;
using Cms.BusinessLayer.BlCommon;
namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsUmbrellaCoverages.
	/// </summary>
	public class ClsUmbrellaCoverages:ClsCoverages
	{
		bool UmbRemoveXMLLoaded;
		bool boolTransactionRequired;
		
		private int thiscreatedby;
		private int thisModifiedby;
		
		public ClsUmbrellaCoverages()
		{
			
			boolTransactionRequired = base.TransactionLogRequired;
			filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/coverages/UmbDefaultCoverageRule.xml");
			RuleDoc = new XmlDocument();
			RuleDoc.Load(filePath); 
			UmbRemoveXMLLoaded=false;
			//
			// TODO: Add constructor logic here
			//
		}
		#region Public Properties
		public bool TransactionRequired
		{
			get
			{
				return boolTransactionRequired;
			}
			set
			{
				boolTransactionRequired=value;
			}
		}
		public int createdby
		{
			
			set
			{
				thiscreatedby=value;
			}
			get
			{
				return thiscreatedby;
			}
		}

		public int modifiedby
		{
			
			set
			{
				thisModifiedby=value;
			}
			get
			{
				return thisModifiedby;
			}
		}
		#endregion

		#region GetCoverages Function -- Fetch All Coverages for Umbrella regardless of business rules
		/// <summary>
		/// Gets Umbrella coverages from database
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <param name="appType"></param>
		/// <returns>Dataset Containing all coverages without filter</returns>
		private  DataSet GetUmbCoverages(int customerID,int appID, 
			int appVersionID,string appType)
		{
			string	strStoredProc =	"Proc_GetAPP_UMBRELLA_COVERAGES";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@APP_TYPE","N");
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			return ds;
		}
		#endregion
	
		#region
		/// <summary>
		/// Gets polilcy vehicle resultset from the database
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <param name="polType"></param>
		/// <returns></returns>
		public  DataSet GetPolicyUmbrellaCoverage(int customerID, int polID, 
			int polVersionID, string polType)
		{
			//string	strStoredProc =	"Proc_Get_POL_VEHICLE_COVERAGES";
			string	strStoredProc =	"Proc_Get_POL_UMBRELLA_COVERAGES";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POL_ID",polID);
			objWrapper.AddParameter("@POL_VERSION_ID",polVersionID);
			objWrapper.AddParameter("@POL_TYPE",polType);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		
		}

		#endregion


		#region GetUmbrellaCoverages Function to fetch coverages after Filteration as per Business rules
		/// <summary>
		/// 
		/// </summary>
		/// <returns>dataset after filteration of records (coverages) that are no longer required </returns>
		public DataSet GetUmbrellaCoverages(int customerID, int appID, int appVersionID,string appType)
		{
			//fetching dataset with all coverages
			
			DataSet dsCoverages=null;
			
			//Get master list of Coverages from Database
			dsCoverages = this.GetUmbCoverages(customerID,appID,
				appVersionID,
				appType
				);	
			
			

			return dsCoverages;             
		}

		#endregion 

		#region  Save function for Umbrella Coverages
		public int SaveUmbrellaCoverages(ArrayList alNewCoverages,string strOldXML, string strCustomInfo)
		{
			string	strStoredProc =	"Proc_SAVE_UMBRELLA_COVERAGES";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			SqlCommand cmdCoverage = new SqlCommand();
			cmdCoverage.CommandText = strStoredProc;
			cmdCoverage.CommandType = CommandType.StoredProcedure;
			
			SqlParameter[] param = new SqlParameter[9];
			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			
			StringBuilder sbTranXML = new StringBuilder();
			
			sbTranXML.Append("<root>");

			if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}
			
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			
			
			
			int customerID = 0;
			int appID = 0;
			int appVersionID = 0;
			

			try
			{
				//Loop thru aray list
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Application.ClsCoveragesInfo objNew = (ClsCoveragesInfo)alNewCoverages[i];
					
					customerID = objNew.CUSTOMER_ID;
					appID = objNew.APP_ID;
					appVersionID = objNew.APP_VERSION_ID;
					

					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
					objWrapper.AddParameter("@CREATED_BY",objNew.CREATED_BY);
					//objWrapper.AddParameter("@CREATED_DATETIME",objNew.CREATED_DATETIME);
					objWrapper.AddParameter("@CREATED_DATETIME",DateTime.Now);
					objWrapper.AddParameter("@MODIFIED_BY",objNew.MODIFIED_BY);
					objWrapper.AddParameter("@LAST_UPDATED_DATETIME",DateTime.Now );  
  
					string strTranXML = "";
				
					objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;

					if ( objNew.ACTION == "I" )
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Coverages.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);
						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Umbrella coverage added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						sbTranXML.Append(strTranXML);
						//objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();

					}
					else if ( objNew.ACTION == "U")
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Coverages.aspx.resx");
						objTransactionInfo.TRANS_DESC		=	"Umbrella coverage updated.";
						strTranXML = this.GetUmbTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Heading");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='COV_DESC']","OldValue"," ");
							sbTranXML.Append(strTranXML);
						objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						//objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();
					}
				
				}

				objWrapper.ClearParameteres();

				//Delete Coverages/////////////////////////////////////
				//string strCustomInfo="Following coverages have been deleted:",str="";
				//strCustomInfo = "";
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					
					Cms.Model.Application.ClsCoveragesInfo objDelete = (ClsCoveragesInfo)alNewCoverages[i];
					
					if ( objDelete.ACTION == "D" )
					{
						objWrapper.AddParameter("@CUSTOMER_ID",objDelete.CUSTOMER_ID);
						objWrapper.AddParameter("@APP_ID",objDelete.APP_ID);
						objWrapper.AddParameter("@APP_VERSION_ID",objDelete.APP_VERSION_ID);
						objWrapper.AddParameter("@COVERAGE_ID",objDelete.COVERAGE_ID);
						//str+=";" + objDelete.COV_DESC;
						//Delete the coverage
						objWrapper.ExecuteNonQuery("Proc_DeleteAPP_UMBRELLA_COVERAGES");

						//Get Tran log
						objDelete.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/AddUmbrellaLimits1.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						
						string strTranXML = objBuilder.GetDeleteTransactionLogXML(objDelete);

						sbTranXML.Append(strTranXML);

						objWrapper.ClearParameteres();
					}
				}
				//////////////////////////////////////////////////////////

				sbTranXML.Append("</root>");
				if(sbTranXML.ToString()!="<root></root>")// || strCustomInfo!="")
				{
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = appID;
					objTransactionInfo.APP_VERSION_ID = appVersionID;
					objTransactionInfo.CLIENT_ID = customerID;
				
					objTransactionInfo.TRANS_DESC		=	"Umbrella coverages updated.";
					if(sbTranXML.ToString()!="<root></root>")
						objTransactionInfo.CHANGE_XML		=	sbTranXML.ToString();

					objTransactionInfo.CUSTOM_INFO=strCustomInfo;
				
					objWrapper.ClearParameteres();

					objWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				//////////////////
				
				
				objWrapper.ClearParameteres();

				
			}
			catch(Exception ex)
			{
				//tran.Rollback();
				//conn.Close();
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				if ( ex.InnerException != null)
				{
					string message = ex.InnerException.Message.ToLower();
				

					if ( message.StartsWith("violation of primary key"))
					{
						return -2;
					}

				}

				throw(ex);
			} 
			
			//tran.Commit();
			//conn.Close();
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;
		}


		#endregion

		#region SaveUmbrellaPolicyCoverages function for Policy level
		public int SavePolicyUmbrellaCoverages(ArrayList alNewCoverages,string strOldXML, string strCustomInfo)
		{
			
			string	strStoredProc =	"Proc_SAVE_POL_UMBRELLA_DEFAULT_COVERAGES";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			SqlCommand cmdCoverage = new SqlCommand();
			cmdCoverage.CommandText = strStoredProc;
			cmdCoverage.CommandType = CommandType.StoredProcedure;
			
			SqlParameter[] param = new SqlParameter[9];
			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			
			StringBuilder sbTranXML = new StringBuilder();
			
			sbTranXML.Append("<root>");

			if ( strOldXML != "" )
			{
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			
			int customerID = 0;
			int policyID = 0;
			int policyVersionID = 0;

			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Policy.ClsPolicyCoveragesInfo objNew = (Cms.Model.Policy.ClsPolicyCoveragesInfo)alNewCoverages[i];
					
					customerID = objNew.CUSTOMER_ID;
					policyID = objNew.POLICY_ID;
					policyVersionID = objNew.POLICY_VERSION_ID;
					//vehicleID = objNew.RISK_ID ;

					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@POLICY_ID",objNew.POLICY_ID);
					objWrapper.AddParameter("@POLICY_VERSION_ID",objNew.POLICY_VERSION_ID);
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
					string strTranXML = "";
					objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
					if ( objNew.ACTION == "I" )
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/PolicyCoverages.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.POLICY_ID = objNew.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID = objNew.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Umbrella coverage added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						sbTranXML.Append(strTranXML);
						//objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();

					}
					else if ( objNew.ACTION == "U")
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/PolicyCoverages.aspx.resx");
						objTransactionInfo.TRANS_DESC		=	"Umbrella coverage updated.";
						strTranXML = this.GetPolicyTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Heading");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='COV_DESC']","OldValue"," ");
							sbTranXML.Append(strTranXML);
						objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						//objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();
					}
				
				}
				objWrapper.ClearParameteres();
				//Delete Coverages/////////////////////////////////////
				
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					
					Cms.Model.Policy.ClsPolicyCoveragesInfo objDelete = (Cms.Model.Policy.ClsPolicyCoveragesInfo)alNewCoverages[i];
					if ( objDelete.ACTION == "D" )
					{
						objWrapper.ClearParameteres();
						objWrapper.AddParameter("@CUSTOMER_ID",objDelete.CUSTOMER_ID);
						objWrapper.AddParameter("@POLICY_ID",objDelete.POLICY_ID);
						objWrapper.AddParameter("@POLICY_VERSION_ID",objDelete.POLICY_VERSION_ID);
						objWrapper.AddParameter("@COVERAGE_CODE",objDelete.COVERAGE_CODE);
						//Delete the coverage
						objWrapper.ExecuteNonQuery("Proc_Delete_POL_UMBRELLA_COVERAGES_BY_CODE");

						//Get Tran log
						objDelete.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/PolicyCoverages.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						//string strTranXML = objBuilder.GetTransactionLogXML(objDelete);
						string strTranXML = objBuilder.GetDeleteTransactionLogXML(objDelete);

						sbTranXML.Append(strTranXML);

						objWrapper.ClearParameteres();
					}
				}
								
				sbTranXML.Append("</root>");

				if(sbTranXML.ToString()!="<root></root>")// || strCustomInfo!="")
				{
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.POLICY_ID= policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = policyVersionID;
					objTransactionInfo.CLIENT_ID = customerID;
				
					objTransactionInfo.TRANS_DESC		=	"Umbrella coverages updated.";
					if(sbTranXML.ToString()!="<root></root>")
						objTransactionInfo.CHANGE_XML		=	sbTranXML.ToString();

					objTransactionInfo.CUSTOM_INFO=strCustomInfo;
				
					objWrapper.ClearParameteres();

					objWrapper.ExecuteNonQuery(objTransactionInfo);
				}
								
				//Update Policy Coverages///////////////
				if(alNewCoverages.Count>0)
				{
					//UpdatePolicyCoverages(alNewCoverages,objWrapper,vehicleID,customerID,policyID,policyVersionID);
				}
				
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				if ( ex.InnerException != null)
				{
					string message = ex.InnerException.Message.ToLower();
				

					if ( message.StartsWith("violation of primary key"))
					{
						return -2;
					}

				}

				throw(ex);
			} 
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;
		}
		

		#endregion

	
		//added by pravesh on 14 jan 2008 to save default coverages from database which are entered from system 
		protected override  void SaveDefaultCoveragesFromDB(DataWrapper objDataWrapper,int CustomerId,int AppPolId,int VersionId,int RiskId,string CalledFor)
		{
			
			string strStoredProc="";
			objDataWrapper.ClearParameteres();
			if (CalledFor=="APP")
			{
				strStoredProc="Proc_SAVE_UMBRELLA_DEFAULT_COVERAGES";
				objDataWrapper.AddParameter("@APP_ID",AppPolId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",VersionId);
				objDataWrapper.AddParameter("@VEHICLE_ID",RiskId);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objDataWrapper.AddParameter("@CREATED_BY",this.createdby); 
				objDataWrapper.ExecuteNonQuery(strStoredProc);
			}
			else if(CalledFor=="POLICY")
			{
				strStoredProc="Proc_SAVE_UMBRELLA_DEFAULT_COVERAGES_POLICY";
				objDataWrapper.AddParameter("@POLICY_ID",AppPolId);
				objDataWrapper.AddParameter("@POL_VERSION_ID",VersionId);
				objDataWrapper.AddParameter("@VEHICLE_ID",RiskId);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objDataWrapper.AddParameter("@CREATED_BY",this.createdby); 
				objDataWrapper.ExecuteNonQuery(strStoredProc);
			}

			objDataWrapper.ClearParameteres();
		}
		#region Save Default/Rule Coverages APPLICATION		
		protected override void SaveCoverageApp(DataWrapper objDataWrapper, int CustomerId, int AppId, int AppVersionId, int RiskId, Coverage cov)
		{
			
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@APP_ID",AppId);
			objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionId);
		    objDataWrapper.AddParameter("@COVERAGE_ID",-1);
			objDataWrapper.AddParameter("@COVERAGE_CODE",cov.COV_CODE);
//			objDataWrapper.ExecuteNonQuery("Proc_SAVE_UMBRELLA_COVERAGES"); 
			int Cov_ID=objDataWrapper.ExecuteNonQuery("Proc_SAVE_UMBRELLA_COVERAGES"); 
			Cms.Model.Application.ClsCoveragesInfo objNew = new ClsCoveragesInfo();
			if(Cov_ID >=0)
			{
				base.PupulateCoverageModel(objNew,cov); 
				objNew.COVERAGE_CODE_ID =Cov_ID ;
				objNew.APP_ID =AppId;
				objNew.APP_VERSION_ID =AppVersionId;
				objNew.CUSTOMER_ID	= CustomerId; 
//				objNew.CREATED_BY	= this.createdby; 
				if (objNew.CREATED_BY==0)
					objNew.CREATED_BY=  int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());         
		
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				string strTranXML="";
				objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/AddUmbrellaLimits1.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objNew);
//				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='LIMIT_1' and @NewValue='0']","NewValue"," ");
//				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='LIMIT_2' and @NewValue='0']","NewValue"," ");
//				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='DEDUCTIBLE_1' and @NewValue='0']","NewValue"," ");
//				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='DEDUCTIBLE_2' and @NewValue='0']","NewValue"," ");
				if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
					sbDefaultTranXML.Append(strTranXML);	
			
			}
			objDataWrapper.ClearParameteres();
					
		}

		protected override void DeleteCoverageApp(DataWrapper objDataWrapper, int CustomerId, int AppId, int AppVersionId, int RiskId, string strCov_Code)
		{
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@APP_ID",AppId);
			objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionId);
			objDataWrapper.AddParameter("@COVERAGE_CODE",strCov_Code);
			objDataWrapper.ExecuteNonQuery("Proc_Delete_APP_UMB_COVERAGES_BY_CODE"); 
			objDataWrapper.ClearParameteres();
		}
		#endregion
		#region Save umbrella coverages for Policy Level ////
		protected override void SaveCoveragePolicy(DataWrapper objDataWrapper, int CustomerId, int PolicyId, int PolicyVersionId, int RiskId, Coverage cov)
		{
			
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@Policy_ID",PolicyId);
			objDataWrapper.AddParameter("@Policy_VERSION_ID",PolicyVersionId);
			objDataWrapper.AddParameter("@COVERAGE_ID",-1);
			objDataWrapper.AddParameter("@COVERAGE_CODE",cov.COV_CODE);
			//objDataWrapper.AddParameter("@CREATED_BY", );
			objDataWrapper.AddParameter("@CREATED_DATETIME",DateTime.Now);
			objDataWrapper.ExecuteNonQuery("Proc_SAVE_POL_UMBRELLA_DEFAULT_COVERAGES"); 
			objDataWrapper.ClearParameteres();
			
		}

		protected override void DeleteCoveragePolicy(DataWrapper objDataWrapper, int CustomerId, int PolicyId, int PolicyVersionId, int RiskId, string strCov_Code)
		{
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@Policy_ID",PolicyId);
			objDataWrapper.AddParameter("@Policy_VERSION_ID",PolicyVersionId);
			objDataWrapper.AddParameter("@COVERAGE_CODE",strCov_Code);
			objDataWrapper.ExecuteNonQuery("Proc_Delete_POL_UMBRELLA_COVERAGES_BY_CODE"); 
			objDataWrapper.ClearParameteres();
		}
		
		#endregion

		#region WriteTranLog App/Pol Coverage
		protected override  void WriteTranLogApp(DataWrapper objDataWrapper,int CustomerId, int AppId, int AppVersionId,int RiskId,Coverage cov)
		{
			objDataWrapper.ClearParameteres(); 
			sbDefaultTranXML.Append("</root>"); 
			Cms.Model.Application.ClsCoveragesInfo objNew = new ClsCoveragesInfo();
			base.PupulateCoverageModel(objNew,cov); 
			objNew.APP_ID =AppId;
			objNew.APP_VERSION_ID =AppVersionId;
			objNew.CUSTOMER_ID	= CustomerId; 
//			objNew.CREATED_BY = this.createdby; 
			if (objNew.CREATED_BY==0)
				objNew.CREATED_BY=  int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());      
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			string strTranXML=sbDefaultTranXML.ToString();
			if (strTranXML !="<root></root>")
			{							   
				objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/AddUmbrellaLimits1.aspx.resx");
				
				objTransactionInfo.TRANS_TYPE_ID	=	2;
				objTransactionInfo.APP_ID = objNew.APP_ID;
				objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
				objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
				objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
				objTransactionInfo.TRANS_DESC		=	"Umbrella coverages modified.";
				objTransactionInfo.CUSTOM_INFO		=	"Risk Id= " + RiskId.ToString();  //+ " and Coverage Code=" + objNew.COVERAGE_CODE ;
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='LIMIT_1' and @NewValue='0']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='DEDUCTIBLE_1' and @NewValue='0']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='LIMIT_1' and @NewValue='-1']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='DEDUCTIBLE_1' and @NewValue='-1']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='LIMIT_2' and @NewValue='0']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='DEDUCTIBLE_2' and @NewValue='0']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='LIMIT_2' and @NewValue='-1']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='DEDUCTIBLE_2' and @NewValue='-1']","root/LabelFieldMapping","NewValue"," ");
				objTransactionInfo.CHANGE_XML		=	strTranXML;
				objDataWrapper.ExecuteNonQuery(objTransactionInfo);
			}

		}
		protected override  void WriteTranLogPol(DataWrapper objDataWrapper,int  CustomerId, int PolId, int PolVersionId,int RiskId,Coverage cov)
		{
			objDataWrapper.ClearParameteres(); 
			sbDefaultTranXML.Append("</root>"); 
			Cms.Model.Policy.ClsPolicyCoveragesInfo objNew = new Cms.Model.Policy.ClsPolicyCoveragesInfo(); 
			base.PupulateCoverageModel(objNew,cov); 
			objNew.POLICY_ID =PolId;
			objNew.POLICY_VERSION_ID=PolVersionId;
			objNew.CUSTOMER_ID	= CustomerId; 
//			objNew.CREATED_BY = this.createdby; 
			if (objNew.CREATED_BY==0)
				objNew.CREATED_BY=  int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());      
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			string strTranXML=sbDefaultTranXML.ToString();
			if (strTranXML !="<root></root>")
			{							   
				objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/PolicyCoverages.aspx.resx");

				objTransactionInfo.TRANS_TYPE_ID	=	2;
				objTransactionInfo.POLICY_ID = objNew.POLICY_ID;
				objTransactionInfo.POLICY_VER_TRACKING_ID = objNew.POLICY_VERSION_ID;
				objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
				objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
				objTransactionInfo.TRANS_DESC		=	"Umbrella Policy coverages modified.";
				objTransactionInfo.CUSTOM_INFO		=	"Vehicle Id= " + RiskId.ToString();  //+ " and Coverage Code=" + objNew.COVERAGE_CODE ;
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='LIMIT_1' and @NewValue='0']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='DEDUCTIBLE_1' and @NewValue='0']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='LIMIT_1' and @NewValue='-1']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='DEDUCTIBLE_1' and @NewValue='-1']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='LIMIT_2' and @NewValue='0']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='DEDUCTIBLE_2' and @NewValue='0']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='LIMIT_2' and @NewValue='-1']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='DEDUCTIBLE_2' and @NewValue='-1']","root/LabelFieldMapping","NewValue"," ");
				objTransactionInfo.CHANGE_XML		=	strTranXML;
				objDataWrapper.ExecuteNonQuery(objTransactionInfo);
			}
		}
		#endregion 
	}
}
