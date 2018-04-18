using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Application;
using Cms.BusinessLayer.BlCommon;
using System.Web.UI.WebControls;
using System.Collections;
using Cms.Model.Application.HomeOwners;
using Cms.Model.Policy;

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsWatercraftCoverages.
	/// </summary>
	public class ClsWatercraftCoverages :  ClsCoverages
	{
		//DataSet dsWatercraftInfo;
		private const string RuleXML =  "/cmsweb/support/coverages/WaterDefaultCoverageRule.xml";
		private int thiscreatedby;
		private int thisModifiedby;


		public ClsWatercraftCoverages()
		{
			if(IsEODProcess )
			{
				string strTemp = RuleXML.Replace("/",@"\");
				filePath = WebAppUNCPath + strTemp;
				filePath=  System.IO.Path.GetFullPath(filePath);
			}
			else
			{
				filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + RuleXML);
			}
			RuleDoc = new XmlDocument();
			RuleDoc.Load(filePath); 
		}

		#region Private Utility Function 

		/// <summary>
		/// Gets watercraft coverages from database
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="boatID"></param>
		/// <param name="appType"></param>
		/// <returns></returns>
		private DataSet GetWatercraftCoverages(int customerID, int appID, 
			int appVersionID, int boatID, string appType)
		{
			string	strStoredProc =	"Proc_GetAPP_WATERCRAFT_COVERAGES";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@WATERCRAFT_ID",boatID);
			objWrapper.AddParameter("@APP_TYPE",appType);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
			
		}

		/// <summary>
		/// Retrives the watercraft coverages for policy from the database.
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <param name="boatID"></param>
		/// <param name="polType"></param>
		/// <returns></returns>
		private DataSet GetWatercraftCoveragesForPolicy(int customerID, int polID, 
			int polVersionID, int boatID, string polType)
		{
			//string	strStoredProc =	"Proc_GetPOL_WATERCRAFT_COVERAGES";
			string	strStoredProc =	"Proc_GetPOL_WATERCRAFT_COVERAGES_NEW";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POL_ID",polID);
			objWrapper.AddParameter("@POL_VERSION_ID",polVersionID);
			objWrapper.AddParameter("@WATERCRAFT_ID",boatID);
			objWrapper.AddParameter("@POL_TYPE",polType);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;

			
		}



		#endregion 
		
		
		#region Copy Policy Watercraft Coverages

		public  DataTable GetPolicyWatercraftsToCopy(int customerID, 
			int polID, int polVersionID, int vehicleID,string calledFrom)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@POL_ID",polID);
			sqlParams[1] = new SqlParameter("@POL_VERSION_ID",polVersionID);
			sqlParams[2] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[3] = new SqlParameter("@VEHICLE_ID",vehicleID);
			sqlParams[4] = new SqlParameter("@CALLED_FROM",calledFrom);

			DataSet ds = DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,"Proc_GetPolicyWatercraftsToCopy",sqlParams);

			return ds.Tables[0];
		}
		#endregion		

		#region Watercraft Coverages
		
		public  DataTable GetWatercraftsToCopy(int customerID, 
			int appID, int appVersionID, int vehicleID,string calledFrom)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@APP_ID",appID);
			sqlParams[1] = new SqlParameter("@APP_VERSION_ID",appVersionID);
			sqlParams[2] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[3] = new SqlParameter("@VEHICLE_ID",vehicleID);
			sqlParams[4] = new SqlParameter("@CALLED_FROM",calledFrom);

			DataSet ds = DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,"Proc_GetWatercraftsToCopy",sqlParams);

			return ds.Tables[0];
		}


		//start
		/// <summary>
		/// Gets all the coverages from the selected WaterCraft
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <returns></returns>
		public  DataSet GetPolicyWatercraftCoveragesToCopy(int customerID, 
			int polID, int polVersionID,int vehicleID,string calledFrom)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@POL_ID",polID);
			sqlParams[1] = new SqlParameter("@POL_VERSION_ID",polVersionID);
			sqlParams[2] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[3] = new SqlParameter("@VEHICLE_ID",vehicleID);
			sqlParams[4] = new SqlParameter("@CALLED_FROM",calledFrom);

			DataSet ds = DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,"Proc_GetPOL_WATERCRAFT_COVERAGES_COPY",sqlParams);

			return ds;

  
		}
		


		//end


		/// <summary>
		/// Gets all the coverages from the selected WaterCraft
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <returns></returns>
		public DataSet GetWatercraftCoveragesForPolicy(DataSet dsCoverages,int customerID, int polID, int polVersionID, int boatID, string appType,string calledFrom)
		{
			//fetching dataset with all coverages
			//DataSet dsCoverages=null;
			/*
			dsCoverages = ClsCoverages.GetWatCoverages(customerID,
				appID,
				appVersionID,
				boatID,appType
				);	
			*/


			//fetching XML string with all coverages to remove
			clsWatercraftInformation objboat=new clsWatercraftInformation();  
			string covXML = this.GetWatCoveragesToRemoveForPolicy(customerID,
				polID,
				polVersionID,
				boatID,dsCoverages,calledFrom
				);	
			
			/*Reading from the XML file and saving the text in string until the function start giving the XML string */
			
			/*TextReader tr=new StreamReader(System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath  + "/working/TestCoverage_Watercraft.XML"));
			covXML=tr.ReadToEnd(); 
			tr.Close();*/
			
			 
			  

			//if XML string is not blank		
			if(covXML!="" )
			{
				Cms.BusinessLayer.BlApplication.clsapplication 	objCovInformation = new Cms.BusinessLayer.BlApplication.clsapplication();
				//function call to delete coverage
				dsCoverages=this.DeleteCoverage(dsCoverages,covXML);			

				//function call to delete coverage limits
				dsCoverages=this.DeleteCoverageOptions(dsCoverages,covXML);			

				//function call to update mandatory field
				dsCoverages=this.UpdateCoverageMandatory(dsCoverages,covXML);			

				//function call to update default field
				dsCoverages=this.OverwriteCoverageDefaultValue(dsCoverages,covXML);			
			}
			return dsCoverages;                
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <param name="calledFrom"></param>
		/// <param name="newvehicleID"></param>
		/// <returns></returns>
		public  DataSet GetPolicyWatercraftCoveragesCopyDisplay(int customerID, 
			int polID, int polVersionID, int vehicleID,string calledFrom,int newvehicleID)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@POLICY_ID",polID);
			sqlParams[1] = new SqlParameter("@POLICY_VERSION_ID",polVersionID);
			sqlParams[2] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[3] = new SqlParameter("@VEHICLE_ID",vehicleID);
			
              //Get The Coverage From Source WaterCraft
			DataSet ds = DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,"Proc_GetPOL_WATERCRAFT_COVERAGESCOPY_DISPLAY",sqlParams);

			sqlParams[0] = new SqlParameter("@POLICY_ID",polID);
			sqlParams[1] = new SqlParameter("@POLICY_VERSION_ID",polVersionID);
			sqlParams[2] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[3] = new SqlParameter("@WATERCRAFT_ID",newvehicleID);
			
			//Get The Information OF  Destination WaterCraft 

			DataSet dsWaterCraftInfo=DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,"PROC_POL_WATERCRAFT_COPY_COVERAGE_INFO",sqlParams);
			int i=3;
			//Add The WaterCraft To Source Table
			foreach(DataTable dt in dsWaterCraftInfo.Tables)
			{
					
				DataTable dtclone=dt.Copy();
				dtclone.TableName="Table" +i;
				ds.Tables.Add(dtclone);
				i++;
			}

			DataSet  dsReturn;
			//Get The Applicable Coverage for destination watercraft
			dsReturn = GetWatercraftCoveragesForPolicy(ds,customerID,polID, polVersionID, newvehicleID,"N",calledFrom);
			return dsReturn;
		}
		

		/// <summary>
		/// Called while copying coverages
		/// </summary>
		/// <param name="dsCoverages"></param>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <param name="boatID"></param>
		/// <param name="appType"></param>
		/// <param name="calledFrom"></param>
		/// <returns></returns>
		

		
			public  DataSet GetWatercraftCoveragesCopyDisplay(int customerID, 
				int appID, int appVersionID, int vehicleID,string calledFrom,int newvehicleID)
			{
				SqlParameter[] sqlParams = new SqlParameter[5];

				sqlParams[0] = new SqlParameter("@APP_ID",appID);
				sqlParams[1] = new SqlParameter("@APP_VERSION_ID",appVersionID);
				sqlParams[2] = new SqlParameter("@CUSTOMER_ID",customerID);
				sqlParams[3] = new SqlParameter("@VEHICLE_ID",vehicleID);
			
                //Get The Coverage From Source WaterCraft
				DataSet ds = DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,"Proc_GetAPP_WATERCRAFT_COVERAGESCOPY_Display",sqlParams);

				sqlParams[0] = new SqlParameter("@APP_ID",appID);
				sqlParams[1] = new SqlParameter("@APP_VERSION_ID",appVersionID);
				sqlParams[2] = new SqlParameter("@CUSTOMER_ID",customerID);
				sqlParams[3] = new SqlParameter("@WATERCRAFT_ID",newvehicleID);
			   
				//Get The Information OF  Destination WaterCraft 

				DataSet dsWaterCraftInfo=DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,"PROC_APP_WATERCRAFT_COPY_COVERAGE_INFO",sqlParams);
				int i=3;
				
				//Add The WaterCraft To Source Table
				foreach(DataTable dt in dsWaterCraftInfo.Tables)
				{
					
                    DataTable dtclone=dt.Copy();
					dtclone.TableName="Table" +i;
					ds.Tables.Add(dtclone);
					i++;
				}

				DataSet  dsReturn;

				//Get The Applicable Coverage for destination watercraft
				dsReturn = GetWatercraftCoverages(ds,customerID,appID, appVersionID, newvehicleID,"N",calledFrom);
				return dsReturn;
			}
		
		
		/// <summary>
		/// Gets all the coverages from the selected vehicle
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <returns></returns>
		public  DataSet GetWatercraftCoveragesToCopy(int customerID, 
			int appID, int appVersionID, int vehicleID,string calledFrom)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@APP_ID",appID);
			sqlParams[1] = new SqlParameter("@APP_VERSION_ID",appVersionID);
			sqlParams[2] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[3] = new SqlParameter("@VEHICLE_ID",vehicleID);
			sqlParams[4] = new SqlParameter("@CALLED_FROM",calledFrom);

			DataSet ds = DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,"Proc_GetAPP_WATERCRAFT_COVERAGES_COPY",sqlParams);

			return ds;
		}
		
		


		//start
		/// <summary>
		/// Copies coverages from one vehicle to another For Policy
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <param name="strOldXML"></param>
		/// <returns></returns>
		public int CopyPolicyWatercraftCoverages(ArrayList alNewCoverages,int customerID, int polID,
			int polVersionID,int vehicleID,string strOldXML)
		{
			string	strDelProc =	"Proc_DeletePOL_WATERCRAFT_COVERAGES_ALL";
			string	strStoredProc =	"Proc_Save_PolicyWATERCRAFT_COVERAGES";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			//Delete linked Endorsements and coverages///////////////////////////
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POL_ID",polID);
			objWrapper.AddParameter("@POL_VERSION_ID",polVersionID);
			objWrapper.AddParameter("@VEHICLE_ID",vehicleID);
			
			try
			{
				objWrapper.ExecuteNonQuery(strDelProc);
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}

			objWrapper.ClearParameteres();
			///////////////////////////
			

			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			

			if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}
			
		
			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Policy.ClsPolicyCoveragesInfo  objNew = (ClsPolicyCoveragesInfo)alNewCoverages[i];
					
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@POL_ID",objNew.POLICY_ID );
					objWrapper.AddParameter("@POL_VERSION_ID",objNew.POLICY_VERSION_ID );
					objWrapper.AddParameter("@VEHICLE_ID",objNew.RISK_ID);
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
					objWrapper.AddParameter("@LIMIT_1_TYPE",objNew.LIMIT_1_TYPE);
					objWrapper.AddParameter("@LIMIT_2_TYPE",objNew.LIMIT_2_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_1",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
					objWrapper.AddParameter("@DEDUCTIBLE_2",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_2));
					objWrapper.AddParameter("@LIMIT_1",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
					objWrapper.AddParameter("@LIMIT_2",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_2));
					objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",objNew.LIMIT1_AMOUNT_TEXT);
					objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",objNew.LIMIT2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",objNew.DEDUCTIBLE1_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",objNew.DEDUCTIBLE2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE",objNew.DEDUCTIBLE_1_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE",objNew.DEDUCTIBLE_2_TYPE);
					objWrapper.AddParameter("@WRITTEN_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
					objWrapper.AddParameter("@FULL_TERM_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
					objWrapper.AddParameter("@LIMIT_ID",DefaultValues.GetIntNullFromNegative(objNew.LIMIT_ID));
					objWrapper.AddParameter("@DEDUC_ID",DefaultValues.GetIntNullFromNegative(objNew.DEDUC_ID));
					
				
					string strTranXML = "";
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					if ( objNew.COVERAGE_ID == -1 )
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/watercraft/PolicyWatercraftCoverages.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.POLICY_ID  = objNew.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID  = objNew.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vehicle cverages added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

					}
					else
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/watercraft/PolicyWatercraftCoverages.aspx.resx");
				
						strTranXML = this.GetPolicyTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);
					}
				
					if ( strTranXML.Trim() == "" )
					{
						//SqlHelper.ExecuteNonQuery(tran,"Proc_SAVE_VEHICLE_COVERAGES",param);
						//SqlHelper.ExecuteNonQuery(tran,"Proc_SAVE_VEHICLE_COVERAGES",param);
				
					}
					else
					{
						
					
						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.POLICY_ID  = objNew.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID  = objNew.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vehicle coverages added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						
						//SqlHelper.ExecuteNonQuery(tran,CommandType.StoredProcedure,"Proc_SAVE_VEHICLE_COVERAGES",param);
						//int retVal = cmdCoverage.ExecuteNonQuery();
						//ClsCommon.AddTransactionLog(objTransactionInfo,tran);
						
						
					}
					
					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					objWrapper.ClearParameteres();

				}
				objWrapper.ClearParameteres();
				SavePolicyDefaultCoverages(customerID,polID,polVersionID,vehicleID,objWrapper);
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
		

		private void SavePolicyDefaultCoverages(int customerID, int polID,
			int polVersionID,int vehicleID,DataWrapper objWrapper)
	    	{
			
			objWrapper.AddParameter("@POL_ID",polID );
			objWrapper.AddParameter("@POL_VERSION_ID",polVersionID );
			objWrapper.AddParameter("@BOAT_ID",vehicleID);
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);

			objWrapper.ExecuteNonQuery("Proc_Update_POL_WATERCRAFT_COVERAGES_ON_RULE");
			//*********************************************************

			//Update endorsements//////////////////////////////////////////
			objWrapper.ClearParameteres();

			objWrapper.AddParameter("@POLICY_ID",polID );
			objWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID );
			objWrapper.AddParameter("@BOAT_ID",vehicleID);
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);

			objWrapper.ExecuteNonQuery("Proc_Update_WATERCRAFT_ENDORSEMENTS_POLICY");
			/////////////////////////////////////////////////////////////
			objWrapper.ClearParameteres();

			objWrapper.AddParameter("@POLICY_ID",polID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);
			objWrapper.AddParameter("@BOAT_ID",vehicleID);
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.ExecuteNonQuery("Proc_UPDATE_WATERCRAFT_HOME_COVERAGES_POLICY");
			/////////////////////////////////////////////////////////////
		}
		//End
	
		//added by pravesh on 14 jan 2008 to save default coverages from database which are entered from system 
		protected override  void SaveDefaultCoveragesFromDB(DataWrapper objDataWrapper,int CustomerId,int AppPolId,int VersionId,int RiskId,string CalledFor)
		{
			
			string strStoredProc="";
			objDataWrapper.ClearParameteres();
			if (CalledFor=="APP")
			{
				strStoredProc="Proc_SaveWatercraftDefaultCoverages";
				objDataWrapper.AddParameter("@APP_ID",AppPolId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",VersionId);
				objDataWrapper.AddParameter("@BOAT_ID",RiskId);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objDataWrapper.AddParameter("@CREATED_BY",this.createdby); 
				objDataWrapper.ExecuteNonQuery(strStoredProc);
			}
			else if(CalledFor=="POLICY")
			{
				strStoredProc="Proc_SaveWatercraftDefaultCoverages_POL";
				objDataWrapper.AddParameter("@POLICY_ID",AppPolId);
				objDataWrapper.AddParameter("@POL_VERSION_ID",VersionId);
				objDataWrapper.AddParameter("@BOAT_ID",RiskId);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objDataWrapper.AddParameter("@CREATED_BY",this.createdby); 
				objDataWrapper.ExecuteNonQuery(strStoredProc);
			}

			objDataWrapper.ClearParameteres();
		}
	//end here
		private void SaveDeafaultCoverages(int customerID, int appID,
			int appVersionID,int vehicleID,DataWrapper objWrapper)
		{
			
			//*********************************************************

			objWrapper.ClearParameteres();

			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@BOAT_ID",vehicleID);
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);

			objWrapper.ExecuteNonQuery("Proc_UPDATE_WATERCRAFT_HOME_COVERAGES");
			//*********************************************************

			//Update endorsements//////////////////////////////////////////
			objWrapper.ClearParameteres();

			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@BOAT_ID",vehicleID);
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);

			objWrapper.ExecuteNonQuery("Proc_Update_WATERCRAFT_ENDORSEMENTS");
			/////////////////////////////////////////////////////////////
		}
		/// <summary>
		/// Save The Watercraft Coverages
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <param name="strOldXML"></param>
		/// <returns></returns>
		
		/// <summary>
		/// Copies coverages from one vehicle to another
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <param name="strOldXML"></param>
		/// <returns></returns>
		public int CopyWatercraftCoverages(ArrayList alNewCoverages,int customerID, int appID,
			int appVersionID,int vehicleID,string strOldXML)
		{
			string	strDelProc =	"Proc_DeleteAPP_WATERCRAFT_COVERAGES_ALL";
			string	strStoredProc =	"Proc_SAVE_WATERCRAFT_COVERAGES";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			//Delete linked Endorsements and coverages///////////////////////////
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@VEHICLE_ID",vehicleID);
			
			try
			{
				objWrapper.ExecuteNonQuery(strDelProc);
				objWrapper.ClearParameteres();
            }
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}

			objWrapper.ClearParameteres();
			///////////////////////////
			

			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			

			if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}
			
		
			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Application.ClsCoveragesInfo objNew = (ClsCoveragesInfo)alNewCoverages[i];
					
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
					objWrapper.AddParameter("@VEHICLE_ID",objNew.RISK_ID);
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
					objWrapper.AddParameter("@LIMIT_1_TYPE",objNew.LIMIT_1_TYPE);
					objWrapper.AddParameter("@LIMIT_2_TYPE",objNew.LIMIT_2_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_1",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
					objWrapper.AddParameter("@DEDUCTIBLE_2",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_2));
					objWrapper.AddParameter("@LIMIT_1",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
					objWrapper.AddParameter("@LIMIT_2",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_2));
					objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",objNew.LIMIT1_AMOUNT_TEXT);
					objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",objNew.LIMIT2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",objNew.DEDUCTIBLE1_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",objNew.DEDUCTIBLE2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE",objNew.DEDUCTIBLE_1_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE",objNew.DEDUCTIBLE_2_TYPE);
					objWrapper.AddParameter("@WRITTEN_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
					objWrapper.AddParameter("@FULL_TERM_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
					objWrapper.AddParameter("@LIMIT_ID",DefaultValues.GetIntNullFromNegative(objNew.LIMIT_ID));
					objWrapper.AddParameter("@DEDUC_ID",DefaultValues.GetIntNullFromNegative(objNew.DEDUC_ID));
					
					
				
					string strTranXML = "";
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					if ( objNew.COVERAGE_ID == -1 )
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
						objTransactionInfo.TRANS_DESC		=	"Vehicle coverages added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

					}
					else
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Coverages.aspx.resx");
				
						strTranXML = this.GetTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);
					}
				
					if ( strTranXML.Trim() == "" )
					{
						//SqlHelper.ExecuteNonQuery(tran,"Proc_SAVE_VEHICLE_COVERAGES",param);
						//SqlHelper.ExecuteNonQuery(tran,"Proc_SAVE_VEHICLE_COVERAGES",param);
				
					}
					else
					{
						
					
						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vehicle cverages added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						
						//SqlHelper.ExecuteNonQuery(tran,CommandType.StoredProcedure,"Proc_SAVE_VEHICLE_COVERAGES",param);
						//int retVal = cmdCoverage.ExecuteNonQuery();
						//ClsCommon.AddTransactionLog(objTransactionInfo,tran);
						
						
					}
					
					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					objWrapper.ClearParameteres();
					

				}
				objWrapper.ClearParameteres();

				
				UpdateCoveragesByRuleApp(objWrapper, customerID, appID, appVersionID,RuleType.RiskDependent , vehicleID);


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
		/// <summary>
		/// 
		/// </summary>
		/// <param name="objDataWrapper"></param>
		/// <param name="CustomerId"></param>
		/// <param name="AppId"></param>
		/// <param name="AppVersionId"></param>
		/// <param name="RiskId"></param>
		/// <param name="cov"></param>
		#region  property for created by and modify by

	
		public int modifiedby
		{
			
			set
			{
				thisModifiedby=value;
			}
			get
			{ return thisModifiedby;
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
		#endregion
		
		
	
		#region Overrides for Save/Update/Delete Logic
		/*Overrides************************/
		
		protected override  void SaveCoverageApp(DataWrapper objDataWrapper,int CustomerId, int AppId, int AppVersionId,int RiskId,Coverage cov)
		{
			
			string strStoredProc="Proc_SaveWatercraftCoverage";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@APP_ID",AppId);
			objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionId);
			objDataWrapper.AddParameter("@VEHICLE_ID",RiskId);
			objDataWrapper.AddParameter("@COVERAGE_CODE",cov.COV_CODE);
			if(cov.DEDUCTIBLE1_AMOUNT != 0)
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_1",cov.DEDUCTIBLE1_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_1",DBNull.Value);
			}
			if(cov.DEDUCTIBLE2_AMOUNT  != 0)
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_2",cov.DEDUCTIBLE2_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_2",DBNull.Value  );
			}
			if(cov.LIMIT1_AMOUNT !=0)
			{
				objDataWrapper.AddParameter("@LIMIT_1",cov.LIMIT1_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@LIMIT_1",DBNull.Value );
			}
			if(cov.LIMIT2_AMOUNT !=0)
			{
				objDataWrapper.AddParameter("@LIMIT_2",cov.LIMIT2_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@LIMIT_2",DBNull.Value);
			}
			objDataWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",cov.LIMIT1_TEXT );
			objDataWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",cov.LIMIT2_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",cov.DEDUCTIBLE1_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",cov.DEDUCTIBLE2_TEXT );
			
			objDataWrapper.AddParameter("@CREATED_BY",this.createdby);
			objDataWrapper.AddParameter("@MODIFIED_BY",this.modifiedby);
			int Cov_ID=objDataWrapper.ExecuteNonQuery(strStoredProc); 
			//added by pravesh for Transaction log while default coverages saved
			Cms.Model.Application.ClsCoveragesInfo objNew = new ClsCoveragesInfo();
			if (Cov_ID >=0)
			{	
				base.PupulateCoverageModel(objNew,cov); 
				objNew.COVERAGE_CODE_ID =Cov_ID ;
				objNew.APP_ID =AppId;
				objNew.APP_VERSION_ID =AppVersionId;
				objNew.CUSTOMER_ID	= CustomerId; 
				objNew.CREATED_BY = this.createdby; 
				if (objNew.CREATED_BY==0)
					objNew.CREATED_BY=  int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());      
		
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				string strTranXML="";
				objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Watercrafts/WatercraftCoverages.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				int count = 0;
				string strNode = cov.COV_CODE;
				string strXml= sbDefaultTranXML.ToString();
				try
				{
					if(strXml.IndexOf(strNode)>0)
					{
						count=1;
					}
				}
				catch 
				{

				}
				if (count !=1)
				{
					if(Convert.ToInt32(cov.DEDUCTIBLE1_AMOUNT)!=0|| Convert.ToInt32(cov.DEDUCTIBLE2_AMOUNT)!= 0 || Convert.ToInt32(cov.LIMIT1_AMOUNT)!=0 || Convert.ToInt32(cov.LIMIT2_AMOUNT)!=0)
					{
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						//				objTransactionInfo.TRANS_TYPE_ID	=	2;
						//				objTransactionInfo.APP_ID = objNew.APP_ID;
						//				objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						//				objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//				objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
						//				objTransactionInfo.TRANS_DESC		=	"Watercarft coverages modified.";
						//				objTransactionInfo.CUSTOM_INFO		=	"Vehicle Id= " + RiskId.ToString()  + " and Coverage Code=" + objNew.COVERAGE_CODE ;
						//				objTransactionInfo.CHANGE_XML		=	strTranXML;
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbDefaultTranXML.Append(strTranXML);
					}
				}
				//objDataWrapper.ExecuteNonQuery(strStoredProc); //,objTransactionInfo);
				//end here
				//objDataWrapper.ExecuteNonQuery("Proc_SaveWatercraftCoverage"); 
			}
			objDataWrapper.ClearParameteres();
		}
		protected override  void WriteTranLogApp(DataWrapper objDataWrapper,int CustomerId, int AppId, int AppVersionId,int RiskId,Coverage cov)
		{
			objDataWrapper.ClearParameteres(); 
			sbDefaultTranXML.Append("</root>"); 
			Cms.Model.Application.ClsCoveragesInfo objNew = new ClsCoveragesInfo();
			base.PupulateCoverageModel(objNew,cov); 
			objNew.APP_ID =AppId;
			objNew.APP_VERSION_ID =AppVersionId;
			objNew.CUSTOMER_ID	= CustomerId; 
			objNew.CREATED_BY = this.createdby; 
			if (objNew.CREATED_BY==0)
				objNew.CREATED_BY=  int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());      
		
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			string strTranXML=sbDefaultTranXML.ToString();
			if (strTranXML !="<root></root>")
			{							   
				objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Watercrafts/WatercraftCoverages.aspx.resx");
				//SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				//strTranXML = objBuilder.GetTransactionLogXML(objNew);
				
				objTransactionInfo.TRANS_TYPE_ID	=	2;
				objTransactionInfo.APP_ID = objNew.APP_ID;
				objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
				objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
				objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
				objTransactionInfo.TRANS_DESC		=	"Watercarft coverages modified.";
				objTransactionInfo.CUSTOM_INFO		=	"Boat Id= " + RiskId.ToString();  //+ " and Coverage Code=" + objNew.COVERAGE_CODE ;
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
			//end here
		}
		protected override  void WriteTranLogPol(DataWrapper objDataWrapper,int  CustomerId, int PolId, int PolVersionId,int RiskId,Coverage cov)
		{
			objDataWrapper.ClearParameteres(); 
			sbDefaultTranXML.Append("</root>"); 
			ClsPolicyCoveragesInfo objNew = new ClsPolicyCoveragesInfo();
			base.PupulateCoverageModel(objNew,cov); 
			objNew.POLICY_ID =PolId;
			objNew.POLICY_VERSION_ID=PolVersionId;
			objNew.CUSTOMER_ID	= CustomerId; 
			objNew.CREATED_BY = this.createdby; 
			if (objNew.CREATED_BY==0)
				objNew.CREATED_BY=  int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());      
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			string strTranXML=sbDefaultTranXML.ToString();
			if (strTranXML !="<root></root>")
			{							   
				objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/watercraft/PolicyWatercraftCoverages.aspx.resx");

				objTransactionInfo.TRANS_TYPE_ID	=	2;
				objTransactionInfo.POLICY_ID = objNew.POLICY_ID;
				objTransactionInfo.POLICY_VER_TRACKING_ID = objNew.POLICY_VERSION_ID;
				objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
				objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
				objTransactionInfo.TRANS_DESC		=	"Watercarft coverages modified.";
				objTransactionInfo.CUSTOM_INFO		=	"Boat Id= " + RiskId.ToString();  //+ " and Coverage Code=" + objNew.COVERAGE_CODE ;
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
			//end here
		}
		protected override void SaveCoveragePolicy(DataWrapper objDataWrapper, int CustomerId, int PolicyId, int PolicyVersionId, int RiskId, Coverage cov)
		{
			
			string strStoredProc="Proc_SavePolicyWatercraftCoverage";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@POLICY_ID",PolicyId);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionId);
			objDataWrapper.AddParameter("@VEHICLE_ID",RiskId);
			objDataWrapper.AddParameter("@COVERAGE_CODE",cov.COV_CODE);
			if(cov.DEDUCTIBLE1_AMOUNT != 0)
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_1",cov.DEDUCTIBLE1_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_1",DBNull.Value);
			}
			if(cov.DEDUCTIBLE2_AMOUNT  != 0)
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_2",cov.DEDUCTIBLE2_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_2",DBNull.Value  );
			}
			if(cov.LIMIT1_AMOUNT !=0)
			{
				objDataWrapper.AddParameter("@LIMIT_1",cov.LIMIT1_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@LIMIT_1",DBNull.Value );
			}
			if(cov.LIMIT2_AMOUNT !=0)
			{
				objDataWrapper.AddParameter("@LIMIT_2",cov.LIMIT2_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@LIMIT_2",DBNull.Value);
			}
			objDataWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",cov.LIMIT1_TEXT );
			objDataWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",cov.LIMIT2_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",cov.DEDUCTIBLE1_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",cov.DEDUCTIBLE2_TEXT );
			//added by pravesh for Transaction log while default coverages saved
			ClsPolicyCoveragesInfo objNew = new ClsPolicyCoveragesInfo(); 
			base.PupulateCoverageModel(objNew,cov); 
			objNew.POLICY_ID  = PolicyId; 
			objNew.POLICY_VERSION_ID =PolicyVersionId ;
			objNew.CUSTOMER_ID	= CustomerId; 
			objNew.CREATED_BY = this.createdby; 
			if (objNew.CREATED_BY==0)
				objNew.CREATED_BY=  int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());      
		
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			string strTranXML="";
			objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/watercraft/PolicyWatercraftCoverages.aspx.resx");
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			int count = 0;
			string strNode = cov.COV_CODE;
			string strXml= sbDefaultTranXML.ToString();
			try
			{
				if(strXml.IndexOf(strNode)>0)
				{
					count=1;
				}
			}
			catch 
			{

			}
			if (count !=1)
			{
				if(Convert.ToInt32(cov.DEDUCTIBLE1_AMOUNT)!=0|| Convert.ToInt32(cov.DEDUCTIBLE2_AMOUNT)!= 0 || Convert.ToInt32(cov.LIMIT1_AMOUNT)!=0 || Convert.ToInt32(cov.LIMIT2_AMOUNT)!=0)
				{
					strTranXML = objBuilder.GetTransactionLogXML(objNew);

					//			objTransactionInfo.TRANS_TYPE_ID	=	2;
					//			objTransactionInfo.POLICY_ID  = objNew.POLICY_ID; 
					//			objTransactionInfo.POLICY_VER_TRACKING_ID  = objNew.POLICY_VERSION_ID ;
					//			objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
					//			objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
					//			objTransactionInfo.TRANS_DESC		=	"Watercarft coverages modified.";
					//			objTransactionInfo.CUSTOM_INFO		=	"Vehicle Id= " + RiskId.ToString()  + " and Coverage Code=" + objNew.COVERAGE_CODE ;
					//			objTransactionInfo.CHANGE_XML		=	strTranXML;
					if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
						sbDefaultTranXML.Append(strTranXML);	
				}
			}
				objDataWrapper.ExecuteNonQuery(strStoredProc);//,objTransactionInfo);
			//end here
			//objDataWrapper.ExecuteNonQuery("Proc_SavePolicyWatercraftCoverage"); 
			objDataWrapper.ClearParameteres();
		}
		
		protected override void DeleteCoverageApp(DataWrapper objDataWrapper,int CustomerId, int AppId, int AppVersionId,int RiskId,string strCov_Code)
		{
			string strStoredProc="Proc_DeleteWatercraftCoverage";

			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@APP_ID",AppId);
			objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionId);
			objDataWrapper.AddParameter("@VEHICLE_ID",RiskId);
			objDataWrapper.AddParameter("@COVERAGE_CODE",strCov_Code);
			
			//added by pravesh for Transaction log while default coverages saved
			Cms.Model.Application.ClsCoveragesInfo objNew = new ClsCoveragesInfo();
			objNew.COVERAGE_CODE = strCov_Code;
			objNew.APP_ID =AppId;
			objNew.APP_VERSION_ID =AppVersionId;
			objNew.CUSTOMER_ID	= CustomerId; 
			objNew.CREATED_BY = this.createdby; 
			if (objNew.CREATED_BY==0 )
				objNew.CREATED_BY=  int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());      
		
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			//string strTranXML="";
			objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Watercrafts/WatercraftCoverages.aspx.resx");
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			//strTranXML = objBuilder.GetTransactionLogXML(objNew);
			//strTranXML = objBuilder.GetDeleteTransactionLogXML(objNew);

//			objTransactionInfo.TRANS_TYPE_ID	=	2;
//			objTransactionInfo.APP_ID = objNew.APP_ID;
//			objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
//			objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
//			objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
//			objTransactionInfo.TRANS_DESC		=	"Watercarft coverages deleted.";
//			objTransactionInfo.CUSTOM_INFO		=	"Vehicle Id= " + RiskId.ToString()  + " and Coverage Code=" + objNew.COVERAGE_CODE ;
//			objTransactionInfo.CHANGE_XML		=	strTranXML;
//			if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
//				sbDefaultTranXML.Append(strTranXML);		
			objDataWrapper.ExecuteNonQuery(strStoredProc);//,objTransactionInfo);
			//end here
			//objDataWrapper.ExecuteNonQuery("Proc_DeleteWatercraftCoverage"); 
			objDataWrapper.ClearParameteres();

		}

		protected override void DeleteCoveragePolicy(DataWrapper objDataWrapper, int CustomerId, int PolicyId, int PolicyVersionId, int RiskId, string strCov_Code)
		{
			string strStoredProc="Proc_DeletePolicyWatercraftCoverage";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@POLICY_ID",PolicyId);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionId);
			objDataWrapper.AddParameter("@VEHICLE_ID",RiskId);
			objDataWrapper.AddParameter("@COVERAGE_CODE",strCov_Code);
			//added by pravesh for Transaction log while default coverages saved
			ClsPolicyCoveragesInfo objNew = new ClsPolicyCoveragesInfo(); 
			objNew.COVERAGE_CODE =strCov_Code; 
			objNew.POLICY_ID  = PolicyId; 
			objNew.POLICY_VERSION_ID =PolicyVersionId ;
			objNew.CUSTOMER_ID	= CustomerId; 
			objNew.CREATED_BY = this.createdby; 
			if (objNew.CREATED_BY==0)
				objNew.CREATED_BY = objNew.MODIFIED_BY = int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());         
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			//string strTranXML="";
			objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/watercraft/PolicyWatercraftCoverages.aspx.resx");
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			//strTranXML = objBuilder.GetDeleteTransactionLogXML(objNew);
//			objTransactionInfo.TRANS_TYPE_ID	=	2;
//			objTransactionInfo.POLICY_ID  = objNew.POLICY_ID; 
//			objTransactionInfo.POLICY_VER_TRACKING_ID  = objNew.POLICY_VERSION_ID ;
//			objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
//			objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
//			objTransactionInfo.TRANS_DESC		=	"Watercarft coverages deleted.";
//			objTransactionInfo.CUSTOM_INFO		=	"Vehicle Id= " + RiskId.ToString()  + " and Coverage Code=" + objNew.COVERAGE_CODE ;
//			objTransactionInfo.CHANGE_XML		=	strTranXML;
//			if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
//				sbDefaultTranXML.Append(strTranXML);	
			objDataWrapper.ExecuteNonQuery(strStoredProc);//,objTransactionInfo);
			//end here

			//objDataWrapper.ExecuteNonQuery("Proc_DeletePolicyWatercraftCoverage"); 
			objDataWrapper.ClearParameteres();

		}

		
		protected override void UpdateCoverageApp(DataWrapper objDataWrapper,int CustomerId, int AppId, int AppVersionId,int RiskId,Coverage cov)
		{
			string strStoredProc="Proc_UpdateWatercraftCoverage";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@APP_ID",AppId);
			objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionId);
			objDataWrapper.AddParameter("@VEHICLE_ID",RiskId);
			objDataWrapper.AddParameter("@COVERAGE_CODE",cov.COV_CODE);
			if(cov.DEDUCTIBLE1_AMOUNT != 0)
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_1",cov.DEDUCTIBLE1_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_1",DBNull.Value);
			}
			if(cov.DEDUCTIBLE2_AMOUNT  != 0)
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_2",cov.DEDUCTIBLE2_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_2",DBNull.Value  );
			}
			if(cov.LIMIT1_AMOUNT !=0)
			{
				objDataWrapper.AddParameter("@LIMIT_1",cov.LIMIT1_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@LIMIT_1",DBNull.Value );
			}
			if(cov.LIMIT2_AMOUNT !=0)
			{
				objDataWrapper.AddParameter("@LIMIT_2",cov.LIMIT2_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@LIMIT_2",DBNull.Value);
			}
			objDataWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",cov.LIMIT1_TEXT );
			objDataWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",cov.LIMIT2_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",cov.DEDUCTIBLE1_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",cov.DEDUCTIBLE2_TEXT );
			int Cov_Id=	objDataWrapper.ExecuteNonQuery(strStoredProc);
			if (Cov_Id>=0)
			{
				//Added by Pravesh for transactionlog while Defauld coverages upated
				Cms.Model.Application.ClsCoveragesInfo objNew = new ClsCoveragesInfo();
				base.PupulateCoverageModel(objNew,cov); 
				objNew.COVERAGE_CODE_ID =Cov_Id; 
				objNew.APP_ID =AppId;
				objNew.APP_VERSION_ID =AppVersionId;
				objNew.CUSTOMER_ID	= CustomerId; 
				objNew.CREATED_BY = this.createdby; 
				if (objNew.CREATED_BY==0)
					objNew.CREATED_BY=  int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());      
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				string strTranXML="";
////////////////////////////////
				int count = 0;
				string strNode = cov.COV_CODE;
				string strXml= sbDefaultTranXML.ToString();
				try
				{
					if(strXml.IndexOf(strNode)>0)
					{
						count=1;
					}
				}
				catch 
				{

				}
				if (count !=1)
				{
					//////////////////////////////////////
					objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Watercrafts/WatercraftCoverages.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				
					if(Convert.ToInt32(cov.DEDUCTIBLE1_AMOUNT)!=0|| Convert.ToInt32(cov.DEDUCTIBLE2_AMOUNT)!= 0 || Convert.ToInt32(cov.LIMIT1_AMOUNT)!=0 || Convert.ToInt32(cov.LIMIT2_AMOUNT)!=0)
					{
						strTranXML = objBuilder.GetTransactionLogXML(objNew);
						//				objTransactionInfo.TRANS_TYPE_ID	=	2;
						//				objTransactionInfo.APP_ID = objNew.APP_ID;
						//				objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						//				objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//				objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						//				objTransactionInfo.TRANS_DESC		=	"Watercraft coverages updated.";
						//				objTransactionInfo.CUSTOM_INFO		=	"Vehicle Id= " + RiskId.ToString()  + " and Coverage Code=" + objNew.COVERAGE_CODE ;
						//				objTransactionInfo.CHANGE_XML		=	strTranXML;
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbDefaultTranXML.Append(strTranXML);	
					}
				}
				//sbTranXML.Append(strTranXML);
				//objDataWrapper.ExecuteNonQuery(strStoredProc);//,objTransactionInfo);
				//objDataWrapper.ClearParameteres();
				//end here
				//objDataWrapper.ExecuteNonQuery("Proc_UpdateWatercraftCoverage"); 
				//objDataWrapper.ClearParameteres();
			}
			objDataWrapper.ClearParameteres();
		}

		protected override void UpdateCoveragePolicy(DataWrapper objDataWrapper, int CustomerId, int PolicyId, int PolicyVersionId, int RiskId, Coverage cov)
		{
			string strStoredProc="Proc_UpdatePolicyWatercraftCoverage";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@POLICY_ID",PolicyId);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionId);
			objDataWrapper.AddParameter("@VEHICLE_ID",RiskId);
			objDataWrapper.AddParameter("@COVERAGE_CODE",cov.COV_CODE);
			if(cov.DEDUCTIBLE1_AMOUNT != 0)
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_1",cov.DEDUCTIBLE1_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_1",DBNull.Value);
			}
			if(cov.DEDUCTIBLE2_AMOUNT  != 0)
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_2",cov.DEDUCTIBLE2_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_2",DBNull.Value  );
			}
			if(cov.LIMIT1_AMOUNT !=0)
			{
				objDataWrapper.AddParameter("@LIMIT_1",cov.LIMIT1_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@LIMIT_1",DBNull.Value );
			}
			if(cov.LIMIT2_AMOUNT !=0)
			{
				objDataWrapper.AddParameter("@LIMIT_2",cov.LIMIT2_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@LIMIT_2",DBNull.Value);
			}
			objDataWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",cov.LIMIT1_TEXT );
			objDataWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",cov.LIMIT2_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",cov.DEDUCTIBLE1_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",cov.DEDUCTIBLE2_TEXT );
			//added by pravesh for Transaction log while default coverages saved
			ClsPolicyCoveragesInfo objNew = new ClsPolicyCoveragesInfo(); 
			base.PupulateCoverageModel(objNew,cov); 
			objNew.POLICY_ID  = PolicyId; 
			objNew.POLICY_VERSION_ID =PolicyVersionId ;
			objNew.CUSTOMER_ID	= CustomerId; 
			objNew.CREATED_BY = this.createdby; 
			if (objNew.CREATED_BY==0)
				objNew.CREATED_BY=  int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());      
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			string strTranXML="";
			objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/watercraft/PolicyWatercraftCoverages.aspx.resx");
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			
			int count = 0;
			string strNode = cov.COV_CODE;
			string strXml= sbDefaultTranXML.ToString();
			try
			{
				if(strXml.IndexOf(strNode)>0)
				{
					count=1;
				}
			}
			catch 
			{

			}
			if (count !=1)
			{
				if(Convert.ToInt32(cov.DEDUCTIBLE1_AMOUNT)!=0|| Convert.ToInt32(cov.DEDUCTIBLE2_AMOUNT)!= 0 || Convert.ToInt32(cov.LIMIT1_AMOUNT)!=0 || Convert.ToInt32(cov.LIMIT2_AMOUNT)!=0)
				{
					strTranXML = objBuilder.GetTransactionLogXML(objNew);
					//			objTransactionInfo.TRANS_TYPE_ID	=	2;
					//			objTransactionInfo.POLICY_ID  = objNew.POLICY_ID; 
					//			objTransactionInfo.POLICY_VER_TRACKING_ID  = objNew.POLICY_VERSION_ID ;
					//			objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
					//			objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
					//			objTransactionInfo.TRANS_DESC		=	"Watercarft coverages updated.";
					//			objTransactionInfo.CUSTOM_INFO		=	"Vehicle Id= " + RiskId.ToString()  + " and Coverage Code=" + objNew.COVERAGE_CODE ;
					//			objTransactionInfo.CHANGE_XML		=	strTranXML;
					if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
						sbDefaultTranXML.Append(strTranXML);	
				}
			}
			objDataWrapper.ExecuteNonQuery(strStoredProc);//,objTransactionInfo);
			//end here
			//objDataWrapper.ExecuteNonQuery("Proc_UpdatePolicyWatercraftCoverage"); 
			objDataWrapper.ClearParameteres();
		}


		protected override void UpdateEndorsmentApp(DataWrapper objDataWrapper, int CustomerId, int AppId, int AppVersionId, int RiskId)
		{
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@APP_ID",AppId);
			objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionId);
			objDataWrapper.AddParameter("@BOAT_ID",RiskId);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.ExecuteNonQuery("Proc_Update_WATERCRAFT_ENDORSEMENTS");
			objDataWrapper.ClearParameteres();
		}

		protected override void UpdateEndorsmentPolicy(DataWrapper objDataWrapper, int CustomerId, int PolicyId, int PolicyVersionId, int RiskId)
		{
			///Update endorsements//////////////////////////////////////////
			objDataWrapper.ClearParameteres();

			objDataWrapper.AddParameter("@POLICY_ID",PolicyId);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionId);
			objDataWrapper.AddParameter("@BOAT_ID",RiskId);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);

			objDataWrapper.ExecuteNonQuery("Proc_Update_WATERCRAFT_ENDORSEMENTS_POLICY");
			/////////////////////////////////////////////////////////////
			objDataWrapper.ClearParameteres();
		}


		public override DataTable GetRisksForLobApp(int CustomerId, int AppId, int AppVersionId, int RiskId)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@APP_ID",AppId);
			sqlParams[1] = new SqlParameter("@APP_VERSION_ID",AppVersionId);
			sqlParams[2] = new SqlParameter("@CUSTOMER_ID",CustomerId);
			sqlParams[3] = new SqlParameter("@VEHICLE_ID",RiskId);
			sqlParams[4] = new SqlParameter("@CALLED_FROM","WAT");

			DataSet ds = DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,"Proc_GetWatercraftsToCopy",sqlParams);

			return ds.Tables[0];
		}

		public override DataTable GetRisksForLobPolicy(int CustomerId, int PolicyId, int PolicyVersionId, int RiskId)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			
			sqlParams[0] = new SqlParameter("@POL_ID",PolicyId);
			sqlParams[1] = new SqlParameter("@POL_VERSION_ID",PolicyVersionId );
			sqlParams[2] = new SqlParameter("@CUSTOMER_ID",CustomerId);
			sqlParams[3] = new SqlParameter("@VEHICLE_ID",RiskId);
			sqlParams[4] = new SqlParameter("@CALLED_FROM","WAT");

			DataSet ds = DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,"Proc_GetPolicyWatercraftsToCopy",sqlParams);

			return ds.Tables[0];
		}
		#endregion 
		


		/// <summary>
		/// Saves coverages records in APP_WATERCRAFT_COBVERAGE_INFO table
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <param name="strOldXML"></param>
		/// <returns></returns>
		public int SaveWatercraftCoveragesAcord(ArrayList alNewCoverages,string strOldXML,DataWrapper objWrapper)
		{
			
			string	strStoredProc =	"Proc_Save_WATERCRAFT_COVERAGES_ACORD";

			Cms.Model.Application.ClsCoveragesInfo objNew = null;			
		
			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			

			if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}
			
			int tempFlagAdd = 0,tempFlagModi = 0;
			for(int i = 0; i < alNewCoverages.Count; i++ )
			{
				objNew = (ClsCoveragesInfo)alNewCoverages[i];
					
				objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
				objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
				objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
				objWrapper.AddParameter("@VEHICLE_ID",objNew.RISK_ID);
				objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
				objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
				objWrapper.AddParameter("@COVERAGE_CODE",objNew.COVERAGE_CODE);
				objWrapper.AddParameter("@LIMIT_1_TYPE",objNew.LIMIT_1_TYPE);
				objWrapper.AddParameter("@LIMIT_2_TYPE",objNew.LIMIT_2_TYPE);
				objWrapper.AddParameter("@DEDUCTIBLE_1",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
				objWrapper.AddParameter("@DEDUCTIBLE_2",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_2));
				objWrapper.AddParameter("@LIMIT_1",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
				objWrapper.AddParameter("@LIMIT_2",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_2));
				objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",objNew.LIMIT1_AMOUNT_TEXT);
				objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",objNew.LIMIT2_AMOUNT_TEXT);
				objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",objNew.DEDUCTIBLE1_AMOUNT_TEXT);
				objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",objNew.DEDUCTIBLE2_AMOUNT_TEXT);
				objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE",objNew.DEDUCTIBLE_1_TYPE);
				objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE",objNew.DEDUCTIBLE_2_TYPE);		
					

				SqlParameter retVal = (SqlParameter)objWrapper.AddParameter("@RETVAL",SqlDbType.Int,ParameterDirection.ReturnValue);

				string strTranXML = "";
					
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				if(tempFlagAdd !=1)
				{
					tempFlagAdd=1;
					if ( objNew.COVERAGE_ID == -1 )
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Watercrafts/WatercraftCoverages.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vehicle coverages added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

					}
					else
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Watercrafts/WatercraftCoverages.aspx.resx");
				
						strTranXML = this.GetTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);
					}
				}
					
				//Insert Coverage
				objWrapper.ExecuteNonQuery(strStoredProc);

				//Insert transaction log
				
				if ( strTranXML.Trim() != "" && tempFlagModi!= 1 )	
				{	
					tempFlagModi=1;
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = objNew.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Vehicle coverages modified.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					objWrapper.ClearParameteres();

					objWrapper.ExecuteNonQuery(objTransactionInfo);
					//SqlHelper.ExecuteNonQuery(tran,CommandType.StoredProcedure,"Proc_SAVE_VEHICLE_COVERAGES",param);
					//int retVal = cmdCoverage.ExecuteNonQuery();
					//ClsCommon.AddTransactionLog(objTransactionInfo,tran);
						
						
				}
					
				//Insert dependent Endorsements***************************************************
				objWrapper.ClearParameteres();
					
				objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
				objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
				objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
				objWrapper.AddParameter("@VEHICLE_ID",objNew.RISK_ID);
				objWrapper.AddParameter("@COVERAGE_CODE_ID",Convert.ToInt32(retVal.Value));

				objWrapper.ExecuteNonQuery("Proc_Update_WATERCRAFT_ENDORSEMENTS_ACORD");

				//************************************************************************************

				objWrapper.ClearParameteres();

			}
			
			
			
//			if (alNewCoverages.Count > 0)
//			{
//				UpdateCoveragesByRuleApp(objWrapper, objNew.CUSTOMER_ID
//					, objNew.APP_ID, objNew.APP_VERSION_ID,RuleType.RiskDependent,  objNew.RISK_ID);
//
//				objWrapper.ClearParameteres();
//
//			}
//
			return 1;
		}


		//Saves Watercraft Policy coverages
		public int SaveWatercraftPolicyCoverages(ArrayList alNewCoverages,string strOldXML,string hiddenCustomInfo)
		{
			//Proc_Save_PolicyWATERCRAFT_COVERAGES
			string	strStoredProc =	"Proc_Save_PolicyWATERCRAFT_COVERAGES";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			SqlCommand cmdCoverage = new SqlCommand();
			cmdCoverage.CommandText = strStoredProc;
			cmdCoverage.CommandType = CommandType.StoredProcedure;
		
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
			int polID = 0;
			int polVersionID = 0;
			
		

			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Policy.ClsPolicyCoveragesInfo  objNew = (Cms.Model.Policy.ClsPolicyCoveragesInfo )alNewCoverages[i];

					objTransactionInfo.RECORDED_BY = objNew.MODIFIED_BY;
					customerID = objNew.CUSTOMER_ID;
					polID = objNew.POLICY_ID ;
					polVersionID = objNew.POLICY_VERSION_ID ;
					
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@POL_ID",objNew.POLICY_ID );
					objWrapper.AddParameter("@POL_VERSION_ID",objNew.POLICY_VERSION_ID );
					objWrapper.AddParameter("@VEHICLE_ID",objNew.RISK_ID);
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
					objWrapper.AddParameter("@LIMIT_1_TYPE",objNew.LIMIT_1_TYPE);
					objWrapper.AddParameter("@LIMIT_2_TYPE",objNew.LIMIT_2_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_1",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
					objWrapper.AddParameter("@DEDUCTIBLE_2",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_2));
					objWrapper.AddParameter("@LIMIT_1",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
					objWrapper.AddParameter("@LIMIT_2",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_2));
					objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",objNew.LIMIT1_AMOUNT_TEXT);
					objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",objNew.LIMIT2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",objNew.DEDUCTIBLE1_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",objNew.DEDUCTIBLE2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE",objNew.DEDUCTIBLE_1_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE",objNew.DEDUCTIBLE_2_TYPE);
					objWrapper.AddParameter("@WRITTEN_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
					objWrapper.AddParameter("@FULL_TERM_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
					objWrapper.AddParameter("@LIMIT_ID",DefaultValues.GetIntNullFromNegative(objNew.LIMIT_ID));
					objWrapper.AddParameter("@DEDUC_ID",DefaultValues.GetIntNullFromNegative(objNew.DEDUC_ID));

					string strTranXML = "";
					
					//Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;

					if ( objNew.ACTION == "I")
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/watercraft/PolicyWatercraftCoverages.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.POLICY_ID  = objNew.POLICY_ID ;
						objTransactionInfo.POLICY_VER_TRACKING_ID  = objNew.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
						objTransactionInfo.TRANS_DESC		=	"Watercraft coverages Updated.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);						
						//objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();

					}
					else if (  objNew.ACTION == "U")
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/watercraft/PolicyWatercraftCoverages.aspx.resx");
				
						strTranXML = this.GetPolicyTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);
						
						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.POLICY_ID  = objNew.POLICY_ID ;
						objTransactionInfo.POLICY_VER_TRACKING_ID  = objNew.POLICY_VERSION_ID ;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Watercraft coverages modified.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						//sbTranXML.Append(strTranXML);
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);
						//objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();
					}
					/*else if(objNew.ACTION =="D")
					{						
						objWrapper.ClearParameteres();
						strStoredProc =	"Proc_DeleteWATERCRAFT_COVERAGES";
						objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
						objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
						objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
						objWrapper.AddParameter("@BOAT_ID",objNew.VEHICLE_ID);
						objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
						objWrapper.ExecuteNonQuery(strStoredProc);	
						objWrapper.ClearParameteres();

					}*/
				
				
					//objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					objWrapper.ClearParameteres();

				}
				//Insert/update is complete, start with delete operation
				strStoredProc =	"Proc_DeletePolicyWATERCRAFT_COVERAGES";
				string strCustomInfo="Following coverages have been deleted:",str="";
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Policy.ClsPolicyCoveragesInfo  objDelete = (Cms.Model.Policy.ClsPolicyCoveragesInfo)alNewCoverages[i];
					if(objDelete.ACTION=="D")
					{
						objWrapper.ClearParameteres();
						objWrapper.AddParameter("@CUSTOMER_ID",objDelete.CUSTOMER_ID);
						objWrapper.AddParameter("@POL_ID",objDelete.POLICY_ID );
						objWrapper.AddParameter("@POL_VERSION_ID",objDelete.POLICY_VERSION_ID);
						objWrapper.AddParameter("@BOAT_ID",objDelete.RISK_ID);
						objWrapper.AddParameter("@COVERAGE_ID",objDelete.COVERAGE_ID);
						str+=";" + objDelete.COV_DESC;
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();
						
					}
				}

				if(str!="")
					strCustomInfo+=str;
				else
					strCustomInfo="";
				sbTranXML.Append("</root>");
				if(sbTranXML.ToString()!="<root></root>")
					strCustomInfo+=";Following coverages have been added/updated:";
				if(sbTranXML.ToString()!="<root></root>" || strCustomInfo!="")
				{
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.POLICY_ID  = polID;
					objTransactionInfo.POLICY_VER_TRACKING_ID  = polVersionID;
					objTransactionInfo.CLIENT_ID = customerID;
				
					objTransactionInfo.TRANS_DESC		=	"Watercraft coverages updated.";
					objTransactionInfo.CUSTOM_INFO = hiddenCustomInfo + ";" + strCustomInfo;
					if(sbTranXML.ToString()!="<root></root>")
						objTransactionInfo.CHANGE_XML		=	sbTranXML.ToString();
					else
						objTransactionInfo.CHANGE_XML ="";
					

					objWrapper.ExecuteNonQuery(objTransactionInfo);
					objWrapper.ClearParameteres();
				}
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




		//end

		public int SaveWatercraftCoveragesNew(ArrayList alNewCoverages,string strOldXML,string hiddenCustomInfo)
		{
			
			string	strStoredProc =	"Proc_Save_WATERCRAFT_COVERAGES";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			SqlCommand cmdCoverage = new SqlCommand();
			cmdCoverage.CommandText = strStoredProc;
			cmdCoverage.CommandType = CommandType.StoredProcedure;
		
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
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Application.ClsCoveragesInfo objNew = (ClsCoveragesInfo)alNewCoverages[i];

					objTransactionInfo.RECORDED_BY = objNew.MODIFIED_BY;
					customerID = objNew.CUSTOMER_ID;
					appID = objNew.APP_ID;
					appVersionID = objNew.APP_VERSION_ID;
					
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
					objWrapper.AddParameter("@VEHICLE_ID",objNew.RISK_ID);
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
					objWrapper.AddParameter("@LIMIT_1_TYPE",objNew.LIMIT_1_TYPE);
					objWrapper.AddParameter("@LIMIT_2_TYPE",objNew.LIMIT_2_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_1",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
					objWrapper.AddParameter("@DEDUCTIBLE_2",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_2));
					objWrapper.AddParameter("@LIMIT_1",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
					objWrapper.AddParameter("@LIMIT_2",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_2));
					objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",objNew.LIMIT1_AMOUNT_TEXT);
					objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",objNew.LIMIT2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",objNew.DEDUCTIBLE1_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",objNew.DEDUCTIBLE2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE",objNew.DEDUCTIBLE_1_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE",objNew.DEDUCTIBLE_2_TYPE);
					objWrapper.AddParameter("@WRITTEN_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
					objWrapper.AddParameter("@FULL_TERM_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
					objWrapper.AddParameter("@LIMIT_ID",DefaultValues.GetIntNullFromNegative(objNew.LIMIT_ID));
					objWrapper.AddParameter("@DEDUC_ID",DefaultValues.GetIntNullFromNegative(objNew.DEDUC_ID));
					//added by pravesh
					objWrapper.AddParameter("@CREATED_BY",objNew.CREATED_BY);
					objWrapper.AddParameter("@CREATED_DATETIME",DateTime.Now  );
					objWrapper.AddParameter("@MODIFIED_BY",objNew.MODIFIED_BY);
					objWrapper.AddParameter("@LAST_UPDATED_DATETIME",DateTime.Now);
					//end here
					string strTranXML = "";
					
					//Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;

					if ( objNew.ACTION == "I")
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Watercrafts/WatercraftCoverages.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
						objTransactionInfo.TRANS_DESC		=	"Watercraft coverages Updated.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);						
						//objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();

					}
					else if (  objNew.ACTION == "U")
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Watercrafts/WatercraftCoverages.aspx.resx");
				                       
						strTranXML = GetTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);
						
						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vehicle coverages modified.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						//sbTranXML.Append(strTranXML);
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);
						//objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();
					}
					/*else if(objNew.ACTION =="D")
					{						
						objWrapper.ClearParameteres();
						strStoredProc =	"Proc_DeleteWATERCRAFT_COVERAGES";
						objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
						objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
						objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
						objWrapper.AddParameter("@BOAT_ID",objNew.VEHICLE_ID);
						objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
						objWrapper.ExecuteNonQuery(strStoredProc);	
						objWrapper.ClearParameteres();

					}*/
				
				
					//objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					objWrapper.ClearParameteres();

				}
				//Insert/update is complete, start with delete operation
				strStoredProc =	"Proc_DeleteWATERCRAFT_COVERAGES";
				string strCustomInfo="Following coverages have been deleted:",str="";
				strCustomInfo="";
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Application.ClsCoveragesInfo objDelete = (ClsCoveragesInfo)alNewCoverages[i];
					if(objDelete.ACTION=="D")
					{
						objWrapper.ClearParameteres();
						objWrapper.AddParameter("@CUSTOMER_ID",objDelete.CUSTOMER_ID);
						objWrapper.AddParameter("@APP_ID",objDelete.APP_ID);
						objWrapper.AddParameter("@APP_VERSION_ID",objDelete.APP_VERSION_ID);
						objWrapper.AddParameter("@BOAT_ID",objDelete.RISK_ID);
						objWrapper.AddParameter("@COVERAGE_ID",objDelete.COVERAGE_ID);
						str+=";" + objDelete.COV_DESC;
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();
						objDelete.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Coverages.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						string strTranXML = objBuilder.GetDeleteTransactionLogXML(objDelete);
						sbTranXML.Append(strTranXML);
						
					}
				}

				/*if(str!="")
					strCustomInfo+=str;
				else
					strCustomInfo="";*/
				sbTranXML.Append("</root>");
				//if(sbTranXML.ToString()!="<root></root>")
				//	strCustomInfo+=";Following coverages have been added/updated:";
				if(sbTranXML.ToString()!="<root></root>")// || strCustomInfo!="")
				{
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = appID;
					objTransactionInfo.APP_VERSION_ID = appVersionID;
					objTransactionInfo.CLIENT_ID = customerID;
				
					objTransactionInfo.TRANS_DESC		=	"Watercraft coverages updated.";
					objTransactionInfo.CUSTOM_INFO = hiddenCustomInfo + ";" + strCustomInfo;
					if(sbTranXML.ToString()!="<root></root>")
						objTransactionInfo.CHANGE_XML		=	sbTranXML.ToString();
					else
						objTransactionInfo.CHANGE_XML ="";
					

					objWrapper.ExecuteNonQuery(objTransactionInfo);
					objWrapper.ClearParameteres();
				}
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


		public int DeleteWaterCraftCoverages(ArrayList alNewCoverages)
		{
			
			string	strStoredProc =	"Proc_DeleteWATERCRAFT_COVERAGES";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			SqlParameter sCustomerID = (SqlParameter)objWrapper.AddParameter("@CUSTOMER_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppID = (SqlParameter)objWrapper.AddParameter("@APP_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppVersionID = (SqlParameter)objWrapper.AddParameter("@APP_VERSION_ID",SqlDbType.SmallInt,ParameterDirection.Input);
			SqlParameter sCoverageID = (SqlParameter)objWrapper.AddParameter("@COVERAGE_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sBoatID = (SqlParameter)objWrapper.AddParameter("@BOAT_ID",SqlDbType.SmallInt,ParameterDirection.Input);

			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					sAppID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).APP_ID;
					sAppVersionID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).APP_VERSION_ID;
					sCustomerID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).CUSTOMER_ID;
					sCoverageID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).COVERAGE_ID;
					sBoatID.Value =  ((ClsCoveragesInfo)alNewCoverages[i]).RISK_ID;

					objWrapper.ExecuteNonQuery(strStoredProc);				
				}
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;

		}


		#endregion
		

		/// <summary>
		/// Returns a dataset containign relevant coverages for the current watercraft
		/// </summary>
		/// <returns>dataset after filteration of records (coverages) that are no longer required </returns>
		public DataSet GetWatercraftCoverages(DataSet dsCoverages,int customerID, int appID, int appVersionID, int boatID, string appType,string calledFrom)
		{
			

			//fetching XML string with all coverages to remove
		
			string covXML=this.GetWatCoveragesToRemove(customerID,
				appID,
				appVersionID,
				boatID,dsCoverages,calledFrom
				);	
			
			/*Reading from the XML file and saving the text in string until the function start giving the XML string */
			
			/*TextReader tr=new StreamReader(System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath  + "/working/TestCoverage_Watercraft.XML"));
			covXML=tr.ReadToEnd(); 
			tr.Close();*/
			
			 
			  

			//if XML string is not blank		
			if(covXML!="" )
			{
				Cms.BusinessLayer.BlApplication.clsapplication 	objCovInformation = new Cms.BusinessLayer.BlApplication.clsapplication();
				//function call to delete coverage
				dsCoverages=this.DeleteCoverage(dsCoverages,covXML);			

				//function call to delete coverage limits
				dsCoverages=this.DeleteCoverageOptions(dsCoverages,covXML);			

				//function call to update mandatory field
				dsCoverages=this.UpdateCoverageMandatory(dsCoverages,covXML);			

				//function call to update default field
				dsCoverages=this.OverwriteCoverageDefaultValue(dsCoverages,covXML);			
			}
			return dsCoverages;             
		}


		/// <summary>
		/// Sets each coverage to mandatory or optional based on business rules
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="boatID"></param>
		/// <param name="objDataSet"></param>
		/// <param name="calledFrom"></param>
		/// <returns></returns>
		public string GetMandatoryCoverages(int customerID,int appID, int appVersionID, 
			int boatID, DataSet objDataSet,string calledFrom)
		{
			if ( objDataSet == null ) return "";

			if ( objDataSet.Tables.Count == 0 ) return "";
			
			int age = 0;
			string watercraftType  = "";
			string waterCraftStyle = "";
			int length = 0;
			int lobID = 0;
			int appEffDate = 0;

			
			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtState = objDataSet.Tables[2];
			DataTable dtWater = objDataSet.Tables[3];
			
			if ( dtState != null )
			{
				if ( dtState.Rows.Count > 0 )
				{
					lobID = Convert.ToInt32(dtState.Rows[0]["APP_LOB"]);
					appEffDate=Convert.ToInt32(dtState.Rows[0]["APP_EFFECTIVE_DATE"]);
				}
			}

			if ( dtWater != null )
			{
				if ( dtWater.Rows.Count > 0 )
				{
					if (dtWater.Rows[0]["TYPE_OF_WATERCRAFT"] != DBNull.Value && dtWater.Rows[0]["TYPE"] != DBNull.Value )
					{
						watercraftType  = Convert.ToString(dtWater.Rows[0]["TYPE_OF_WATERCRAFT"]).Trim();
						waterCraftStyle = Convert.ToString(dtWater.Rows[0]["TYPE"]).Trim();
					}

					if (dtWater.Rows[0]["LENGTH"] != DBNull.Value )
					{
						length = Convert.ToInt32(dtWater.Rows[0]["LENGTH"]);
					}

				}
			}

			
			StringBuilder sbXML = new StringBuilder();
			

			if ( dtWater.Rows[0]["YEAR"] != System.DBNull.Value )
			{
				int year = Convert.ToInt32(dtWater.Rows[0]["YEAR"]);
				age = appEffDate - year;				
			}
			
			sbXML.Append("<Coverages>");
			
			
			DataRow[] drMand = dtCoverage.Select("COV_CODE IN ('MCPAY')");
			
			foreach(DataRow dr in drMand )
			{
				string covID = dr["COV_ID"].ToString();
				string covCode = dr["COV_CODE"].ToString();
				string mand = "N";

				if (
					 !( 
					waterCraftStyle.ToString().Trim() == "JS" || watercraftType.ToString().Trim() == "11373" || watercraftType.ToString().Trim() == "11386"
					 ) && 
					lobID == 1
					)  
				{
					mand = "Y";
				}
				else
				{
					mand = "N";
				}

				sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='" + mand + "'>");
				sbXML.Append("</Coverage>");
			}
		   drMand = dtCoverage.Select("COV_CODE IN ('LCCSL')");
			
			foreach(DataRow dr in drMand )
			{
				string covID = dr["COV_ID"].ToString();
				string covCode = dr["COV_CODE"].ToString();
				string mand = "N";
               //Mandatory For Homeowner
				if (lobID == 1)  
				{
					mand = "Y";
				}
				else
				{
					mand = "N";
				}

				sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='" + mand + "'>");
				sbXML.Append("</Coverage>");
			}
			
			DataRow[] drRep = dtCoverage.Select("COV_CODE IN ('BRCC','EBIUE')");
				
			foreach(DataRow dr in drRep )
			{
				string covID = dr["COV_ID"].ToString();
				string covCode = dr["COV_CODE"].ToString();

				sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
				sbXML.Append("</Coverage>");

			}
			//Section 1 - Covered Property Damage - Actual Cash Value
			//Section 1 - Covered Property Damage - Agreed Value

			if(waterCraftStyle != "JS")
			{
				DataRow[] dr = dtCoverage.Select("COV_CODE='EBPPDACV'");
				if(dr != null && dr.Length > 0)
				{
					foreach(DataRow dr1 in dr)
					{
						if ( dr1["COVERAGE_ID"] != System.DBNull.Value )						
						{
							string covID = dr1["COV_ID"].ToString();
							string covCode = dr1["COV_CODE"].ToString();
							sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
							sbXML.Append("</Coverage>");
						}
					}

				}
				DataRow[] drEBPPDAV = dtCoverage.Select("COV_CODE='EBPPDAV'");
				if(drEBPPDAV != null && drEBPPDAV.Length > 0)
				{
					foreach(DataRow dr1 in drEBPPDAV)
					{
						if ( dr1["COVERAGE_ID"] != System.DBNull.Value )						
						{
							string covID = dr1["COV_ID"].ToString();
							string covCode = dr1["COV_CODE"].ToString();
							sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
							sbXML.Append("</Coverage>");
						}
						
					}

				}


			}
			
			sbXML.Append("</Coverages>");

			return sbXML.ToString();
			
		}
		

		
		
		///<summary>
		///Retrieves the watercraft coverages from teh database
		/// </summary>
		/// <returns>dataset after filteration of records (coverages) that are no longer required </returns>
		public DataSet GetWatCoverages(int customerID, int appID, int appVersionID, 
			int boatID, string appType,string calledFrom)
		{
			//fetching dataset with all coverages
			DataSet dsCoverages=null;

			ClsCoverages objCoverages = new ClsCoverages();

			dsCoverages = this.GetWatercraftCoverages(customerID,
				appID,
				appVersionID,
				boatID,appType
				);	

			//fetching XML string with all coverages to remove
			clsWatercraftInformation objboat=new clsWatercraftInformation();  
			string covXML = this.GetWatCoveragesToRemove(customerID,
				appID,
				appVersionID,
				boatID,dsCoverages,calledFrom
				);	
			
			//fetching XML string with all mandatory/optional coverages
			string covMandatoryXML=this.GetMandatoryCoverages(customerID,
				appID,
				appVersionID,
				boatID,dsCoverages,calledFrom
				);	

			Cms.BusinessLayer.BlApplication.clsapplication 	objCovInformation = new Cms.BusinessLayer.BlApplication.clsapplication();

			//if XML string is not blank		
			if(covXML!="" )
			{
				
				//function call to delete coverage
				dsCoverages=this.DeleteCoverage(dsCoverages,covXML);			

				//function call to delete coverage limits
				dsCoverages=this.DeleteCoverageOptions(dsCoverages,covXML);			
				
				

				//function call to update default field
				dsCoverages=this.OverwriteCoverageDefaultValue(dsCoverages,covXML);			
			}

			//Update mandatory
			if ( covMandatoryXML != "" )
			{
				//function call to update mandatory field
				dsCoverages=this.UpdateCoverageMandatory(dsCoverages,covMandatoryXML);			
			}
			return dsCoverages;             
		}


		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="objNew"></param>
		/// <param name="xml"></param>
		/// <param name="coverageID"></param>
		/// <param name="root"></param>
		/// <returns></returns>
		private string GetPolicyTranXML(Cms.Model.Policy.ClsPolicyCoveragesInfo objNew,string xml,int coverageID, XmlElement root)
		{
			XmlNode node = root.SelectSingleNode("Table[COVERAGE_ID=" + coverageID.ToString() + "]");
						
			Cms.Model.Policy.ClsPolicyCoveragesInfo  objOld = new Cms.Model.Policy.ClsPolicyCoveragesInfo();
						
			objOld.POLICY_ID = objNew.POLICY_ID;
			objOld.POLICY_VERSION_ID = objNew.POLICY_VERSION_ID;
			objOld.CUSTOMER_ID = objNew.CUSTOMER_ID;
			objOld.COVERAGE_ID = objNew.COVERAGE_ID;
						
			XmlNode element = null;
			
			element = node.SelectSingleNode("COV_DESC");

			if ( element != null)
			{
				objOld.COV_DESC = element.InnerXml.Trim();
			}

			element = node.SelectSingleNode("COVERAGE_ID");

			if ( element != null)
			{
				objOld.COVERAGE_ID = Convert.ToInt32(element.InnerXml);
			}
						
			element = node.SelectSingleNode("COVERAGE_CODE_ID");
						
			if ( element != null )
			{
				objOld.COVERAGE_CODE_ID = Convert.ToInt32(ClsCommon.DecodeXMLCharacters(element.InnerXml));
			}
						
			element = node.SelectSingleNode("LIMIT_1_TYPE");
						
			if ( element != null )
			{
				string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
				if ( str != "" )
				{
					objOld.LIMIT_1_TYPE = str;
				}
			}
						
			element = node.SelectSingleNode("LIMIT_2_TYPE");
						
			if ( element != null )
			{
				string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
				if ( str != "" )
				{
					objOld.LIMIT_2_TYPE =str;
				}
			}
			
			element = node.SelectSingleNode("LIMIT_1");
						
			if ( element != null )
			{
				string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
				if ( str != "" )
				{
					objOld.LIMIT_1 = Convert.ToDouble(str);
				}
			}
						
			element = node.SelectSingleNode("LIMIT_2");
						
			if ( element != null )
			{
				string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
				if ( str != "" )
				{
					objOld.LIMIT_2 = Convert.ToDouble(str);
				}
			}

			element = node.SelectSingleNode("DEDUCTIBLE_1");
						
			if ( element != null )
			{
				string str = ClsCommon.DecodeXMLCharacters(element.InnerXml);
							
				if ( str != "" )
				{
					objOld.DEDUCTIBLE_1 = Convert.ToDouble(str);
				}
			}

			element = node.SelectSingleNode("DEDUCTIBLE_1_TYPE");
						
			if ( element != null )
			{
				string str = ClsCommon.DecodeXMLCharacters(element.InnerXml);
							
				if ( str != "" )
				{
					objOld.DEDUCTIBLE_1_TYPE = str;
				}
			}
			
			element = node.SelectSingleNode("DEDUCTIBLE_2_TYPE");
						
			if ( element != null )
			{
				string str = ClsCommon.DecodeXMLCharacters(element.InnerXml);
							
				if ( str != "" )
				{
					objOld.DEDUCTIBLE_2_TYPE = str;
				}
			}

			element = node.SelectSingleNode("WRITTEN_PREMIUM");
			if ( element != null )
			{
				string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
				if ( str != "" )
				{
					objOld.WRITTEN_PREMIUM = Convert.ToDouble(str);
				}
			}

			element = node.SelectSingleNode("FULL_TERM_PREMIUM");
			if ( element != null )
			{
				string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
				if ( str != "" )
				{
					objOld.FULL_TERM_PREMIUM = Convert.ToDouble(str);
				}
			}
			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

			string strTranXML = objBuilder.GetTransactionLogXML(objOld,objNew);

			return strTranXML;
		}

		
		#region Policy Get Mandatory
		/// <summary>
		/// 
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <param name="boatID"></param>
		/// <param name="objDataSet"></param>
		/// <param name="calledFrom"></param>
		/// <returns></returns>
		public string GetPolicyMandatoryCoverages(int customerID,int polID, int polVersionID, 
			int boatID, DataSet objDataSet,string calledFrom)
		{
			if ( objDataSet == null ) return "";

			if ( objDataSet.Tables.Count == 0 ) return "";
			
			int age = 0;
			string watercraftType = "";
			string waterCraftStyle = "";
			
			int length = 0;
			int lobID = 0;
			int appEffDate = 0;


			
			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtState = objDataSet.Tables[2];
			DataTable dtWater = objDataSet.Tables[3];
			
			StringBuilder sbXML = new StringBuilder();

			if ( dtState != null )
			{
				if ( dtState.Rows.Count > 0 )
				{
					lobID = Convert.ToInt32(dtState.Rows[0]["LOB_ID"]);
					appEffDate=Convert.ToInt32(dtState.Rows[0]["APP_EFFECTIVE_DATE"]);
				}
			}

			if ( dtWater != null )
			{
				if ( dtWater.Rows.Count > 0 )
				{
					if (dtWater.Rows[0]["TYPE_OF_WATERCRAFT"] != DBNull.Value )
					{
						watercraftType = Convert.ToString(dtWater.Rows[0]["TYPE_OF_WATERCRAFT"]).Trim();
						waterCraftStyle = Convert.ToString(dtWater.Rows[0]["TYPE"]).Trim();
					}

					if (dtWater.Rows[0]["LENGTH"] != DBNull.Value )
					{
						length = Convert.ToInt32(dtWater.Rows[0]["LENGTH"]);
					}

				}
			}

			
			
			

			if ( dtWater.Rows[0]["YEAR"] != System.DBNull.Value )
			{
				int year = Convert.ToInt32(dtWater.Rows[0]["YEAR"]);
				age = appEffDate - year;				
			}
			
			sbXML.Append("<Coverages>");
			
			
			DataRow[] drMand = dtCoverage.Select("COV_CODE IN ('MCPAY')");
			
			foreach(DataRow dr in drMand )
			{
				string covID = dr["COV_ID"].ToString();
				string covCode = dr["COV_CODE"].ToString();
				string mand = "N";

				if (
					!( 
					waterCraftStyle.ToString() == "JS" || watercraftType.ToString() == "11373" || watercraftType == "11386"
					) && 
					lobID == 1
					)
				{
					mand = "Y";
				}
				else
				{
					mand = "N";
				}

				sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='" + mand + "'>");
				sbXML.Append("</Coverage>");
			}
			drMand = dtCoverage.Select("COV_CODE IN ('LCCSL')");
			
			foreach(DataRow dr in drMand )
			{
				string covID = dr["COV_ID"].ToString();
				string covCode = dr["COV_CODE"].ToString();
				string mand = "N";
				//Mandatory For Homeowner
				if (lobID == 1)  
				{
					mand = "Y";
				}
				else
				{
					mand = "N";
				}

				sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='" + mand + "'>");
				sbXML.Append("</Coverage>");
			}
			
			DataRow[] drRep = dtCoverage.Select("COV_CODE IN ('BRCC','EBIUE')");
				
			foreach(DataRow dr in drRep )
			{
				string covID = dr["COV_ID"].ToString();
				string covCode = dr["COV_CODE"].ToString();

				sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
				sbXML.Append("</Coverage>");

			}
			//Section 1 - Covered Property Damage - Actual Cash Value
			//Section 1 - Covered Property Damage - Agreed Value

			if(waterCraftStyle != "JS")
			{
				DataRow[] dr = dtCoverage.Select("COV_CODE='EBPPDACV'");
				if(dr != null && dr.Length > 0)
				{
					foreach(DataRow dr1 in dr)
					{
						if ( dr1["COVERAGE_ID"] != System.DBNull.Value )						
						{
							string covID = dr1["COV_ID"].ToString();
							string covCode = dr1["COV_CODE"].ToString();
							sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
							sbXML.Append("</Coverage>");
						}
					}

				}
				DataRow[] drEBPPDAV = dtCoverage.Select("COV_CODE='EBPPDAV'");
				if(drEBPPDAV != null && drEBPPDAV.Length > 0)
				{
					foreach(DataRow dr1 in drEBPPDAV)
					{
						if ( dr1["COVERAGE_ID"] != System.DBNull.Value )						
						{
							string covID = dr1["COV_ID"].ToString();
							string covCode = dr1["COV_CODE"].ToString();
							sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
							sbXML.Append("</Coverage>");
						}
						
					}

				}


			}
			
			/*

			if ( dtVehicle.Rows[0]["YEAR"] != System.DBNull.Value )
			{
				int year = Convert.ToInt32(dtVehicle.Rows[0]["YEAR"]);
				age = DateTime.Now.Year - year;				
			}
			
			sbXML.Append("<Coverages>");
			foreach(DataRow dr in dtCoverage.Rows )
			{
				string covID = dr["COV_ID"].ToString();
				string covCode = dr["COV_CODE"].ToString();
				
				if ( covCode == "BRCC" )
				{
				//	if ( age <= 5 )
				//	{
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
						sbXML.Append("</Coverage>");
				//	}
				}
				else if (covCode == "EBIUE")
				{
					//Increase in "Unattached Equipment" And Personal Effects Coverage
					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
					sbXML.Append("</Coverage>");
				}
				else
				{
					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='N'>");
					sbXML.Append("</Coverage>");
				}
			}	
			*/
			sbXML.Append("</Coverages>");

			return sbXML.ToString();
			
		}
		


		#endregion
/// <summary>
/// 
/// </summary>
/// <param name="customerID"></param>
/// <param name="polID"></param>
/// <param name="polVersionID"></param>
/// <param name="boatID"></param>
/// <param name="polType"></param>
/// <param name="calledFrom"></param>
/// <returns></returns>
		public DataSet GetWatCoveragesForPolicy(int customerID, int polID, int polVersionID, 
			int boatID, string polType,string calledFrom)
          
		{
			//fetching dataset with all coverages
			DataSet dsCoverages=null;

			

			dsCoverages = GetWatercraftCoveragesForPolicy(customerID,
				polID,
				polVersionID,
				boatID,polType
				);	

			//fetching XML string with all coverages to remove
			clsWatercraftInformation objboat=new clsWatercraftInformation();  						
			string covXML = this.GetWatCoveragesToRemoveForPolicy(customerID,
				polID,
				polVersionID,
				boatID,dsCoverages,calledFrom
				);	
						
			//fetching XML string with all mandatory/optional coverages
			string covMandatoryXML=this.GetPolicyMandatoryCoverages(customerID,
				polID,
				polVersionID,
				boatID,dsCoverages,calledFrom
				);	
			
			/*Reading from the XML file and saving the text in string until the function start giving the XML string */
						
			/*TextReader tr=new StreamReader(System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath  + "/working/TestCoverage_Watercraft.XML"));
						covXML=tr.ReadToEnd(); 
						tr.Close();*/
						
						 
						  
			//Cms.BusinessLayer.BlApplication.clsapplication 	objCovInformation = new Cms.BusinessLayer.BlApplication.clsapplication();

			//if XML string is not blank		
			if(covXML!="" )
			{
							
				//function call to delete coverage
				//dsCoverages=this.DeleteCoverage(dsCoverages,covXML);			
				dsCoverages = this.DeleteCoverage(dsCoverages,covXML);			

				//function call to delete coverage limits
				dsCoverages = this.DeleteCoverageOptions(dsCoverages,covXML);			
									
				//function call to update default field
				dsCoverages = this.OverwriteCoverageDefaultValue(dsCoverages,covXML);			
			}

			//Update mandatory
			if ( covMandatoryXML != "" )
			{
				//function call to update mandatory field
				dsCoverages = this.UpdateCoverageMandatory(dsCoverages,covMandatoryXML);			
			}

			return dsCoverages;             
		}

		#region Commented Code (Ravindra 06-09-2006)
/*
			
			public static DataSet FetchWatercraftInfo(int CustomerID,int AppID, int AppVersionID, int BoatID,string calledFrom)
			{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPVERSIONID",AppVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@BOAT_ID",BoatID,SqlDbType.Int);

				if(calledFrom=="WAT")
					dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetWatercraftInfo");
				else if(calledFrom=="UMB")
					dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetUmbrellaWatercraftInfo");
			
				return dsTemp;
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}
		
			public static DataSet GetWatercraftInfo(int CustomerID,int AppID, int AppVersionID, int BoatID)
			{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPVERSIONID",AppVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@BOAT_ID",BoatID,SqlDbType.Int);

				//if(calledFrom=="WAT")
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetWatercraftInfo");
				//else if(calledFrom=="UMB")
				//	dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetUmbrellaWatercraftInfo");
			
				return dsTemp;
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}

			public static DataSet GetPolicyWatercraftInfo(int CustomerID,int polID, int polVersionID, int BoatID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",polID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@BOAT_ID",BoatID,SqlDbType.Int);

				//if(calledFrom=="WAT")
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_PolicyGetWatercraftInfo");
				//else if(calledFrom=="UMB")
				//	dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetUmbrellaWatercraftInfo");
			
				return dsTemp;
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <returns></returns>
		public int DeleteCoverages(ArrayList alNewCoverages)
		{
			
			string	strStoredProc =	"Proc_DeleteAPP_VEHICLE_COVERAGES";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			SqlParameter sCustomerID = (SqlParameter)objWrapper.AddParameter("@CUSTOMER_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppID = (SqlParameter)objWrapper.AddParameter("@APP_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppVersionID = (SqlParameter)objWrapper.AddParameter("@APP_VERSION_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sCoverageID = (SqlParameter)objWrapper.AddParameter("@COVERAGE_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sVehID = (SqlParameter)objWrapper.AddParameter("@VEHICLE_ID",SqlDbType.Int,ParameterDirection.Input);

			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					sAppID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).APP_ID;
					sAppVersionID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).APP_VERSION_ID;
					sCustomerID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).CUSTOMER_ID;
					sCoverageID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).COVERAGE_ID;
					sVehID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).VEHICLE_ID;

					objWrapper.ExecuteNonQuery(strStoredProc);				
				}
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;

		}

/// <summary>
/// 
/// </summary>
/// <param name="alNewCoverages"></param>
/// <returns></returns>
		public int DeleteUmbrellaCoverages(ArrayList alNewCoverages)
		{
			
			string	strStoredProc =	"Proc_DeleteAPP_UMBRELLA_VEHICLE_COVERAGES";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			SqlParameter sCustomerID = (SqlParameter)objWrapper.AddParameter("@CUSTOMER_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppID = (SqlParameter)objWrapper.AddParameter("@APP_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppVersionID = (SqlParameter)objWrapper.AddParameter("@APP_VERSION_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sCoverageID = (SqlParameter)objWrapper.AddParameter("@COVERAGE_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sVehID = (SqlParameter)objWrapper.AddParameter("@VEHICLE_ID",SqlDbType.Int,ParameterDirection.Input);

			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					sAppID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).APP_ID;
					sAppVersionID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).APP_VERSION_ID;
					sCustomerID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).CUSTOMER_ID;
					sCoverageID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).COVERAGE_ID;
					sVehID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).VEHICLE_ID;

					objWrapper.ExecuteNonQuery(strStoredProc);				
				}
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;

		}
		
/// <summary>
/// 
/// </summary>
/// <param name="alNewCoverages"></param>
/// <returns></returns>
		public int DeleteUmbrellaWaterCraftCoverages(ArrayList alNewCoverages)
		{
			
			string	strStoredProc =	"Proc_DeleteUMBRELLA_WATERCRAFT_COVERAGES";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			SqlParameter sCustomerID = (SqlParameter)objWrapper.AddParameter("@CUSTOMER_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppID = (SqlParameter)objWrapper.AddParameter("@APP_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppVersionID = (SqlParameter)objWrapper.AddParameter("@APP_VERSION_ID",SqlDbType.SmallInt,ParameterDirection.Input);
			SqlParameter sCoverageID = (SqlParameter)objWrapper.AddParameter("@COVERAGE_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sBoatID = (SqlParameter)objWrapper.AddParameter("@BOAT_ID",SqlDbType.SmallInt,ParameterDirection.Input);

			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					sAppID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).APP_ID;
					sAppVersionID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).APP_VERSION_ID;
					sCustomerID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).CUSTOMER_ID;
					sCoverageID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).COVERAGE_ID;
					sBoatID.Value =  ((ClsCoveragesInfo)alNewCoverages[i]).VEHICLE_ID;

					objWrapper.ExecuteNonQuery(strStoredProc);				
				}
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;

		}

/// <summary>
/// 
/// </summary>
/// <param name="alNewCoverages"></param>
/// <returns></returns>
	

		public int SaveAppCoverages(ArrayList alNewCoverages,string strOldXML)
		{
			
			string	strStoredProc =	"Proc_SAVE_COVERAGES";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			SqlCommand cmdCoverage = new SqlCommand();
			cmdCoverage.CommandText = strStoredProc;
			cmdCoverage.CommandType = CommandType.StoredProcedure;
			
			//cmdCoverage.Parameters = 
	

			SqlParameter[] param = new SqlParameter[16];
		
			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			

			if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}
			
			//SqlConnection conn = new SqlConnection(ConnStr);
			//conn.Open();
			
			//cmdCoverage.Connection = conn;
			//SqlTransaction tran = conn.BeginTransaction();
			
			//cmdCoverage.Transaction = tran;

			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Application.ClsCoveragesInfo objNew = (ClsCoveragesInfo)alNewCoverages[i];
					
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
					objWrapper.AddParameter("@VEHICLE_ID",objNew.VEHICLE_ID);
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
					objWrapper.AddParameter("@LIMIT_1_TYPE",objNew.LIMIT_1_TYPE);
					objWrapper.AddParameter("@LIMIT_2_TYPE",objNew.LIMIT_2_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_1",objNew.DEDUCTIBLE_1);
					objWrapper.AddParameter("@DEDUCTIBLE_2",objNew.DEDUCTIBLE_2);
					objWrapper.AddParameter("@LIMIT_1",objNew.LIMIT_1);
					objWrapper.AddParameter("@LIMIT_2",objNew.LIMIT_2);
					objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE",objNew.DEDUCTIBLE_1_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE",objNew.DEDUCTIBLE_2_TYPE);
					objWrapper.AddParameter("@WRITTEN_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
					objWrapper.AddParameter("@FULL_TERM_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
					
					

					string strTranXML = "";
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					if ( objNew.COVERAGE_ID == -1 )
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
						objTransactionInfo.TRANS_DESC		=	"Vehicle coverages added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

					}
					else
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Coverages.aspx.resx");
				
						strTranXML = this.GetTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);
					}
				
					if ( strTranXML.Trim() == "" )
					{
						//SqlHelper.ExecuteNonQuery(tran,"Proc_SAVE_VEHICLE_COVERAGES",param);
						//SqlHelper.ExecuteNonQuery(tran,"Proc_SAVE_VEHICLE_COVERAGES",param);
				
					}
					else
					{
						
					
						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vehicle coverages added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						
						//SqlHelper.ExecuteNonQuery(tran,CommandType.StoredProcedure,"Proc_SAVE_VEHICLE_COVERAGES",param);
						//int retVal = cmdCoverage.ExecuteNonQuery();
						//ClsCommon.AddTransactionLog(objTransactionInfo,tran);
						
						
					}
					
					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					objWrapper.ClearParameteres();

				}
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
		*/

		
		/*
		public int SaveUmbrellaWatercraftCoveragesNew(ArrayList alNewCoverages,string strOldXML,string hiddenCustomInfo)
		{
			
			string	strStoredProc =	"Proc_Save_UMBRELLA_WATERCRAFT_COVERAGES";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			SqlCommand cmdCoverage = new SqlCommand();
			cmdCoverage.CommandText = strStoredProc;
			cmdCoverage.CommandType = CommandType.StoredProcedure;
		
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

			string strCustomInfo="Following coverages have been deleted:",str="";
			strCustomInfo="";
			
			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					
					Cms.Model.Application.ClsCoveragesInfo objNew = (ClsCoveragesInfo)alNewCoverages[i];
					objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
					customerID = objNew.CUSTOMER_ID;
					appID = objNew.APP_ID;
					appVersionID = objNew.APP_VERSION_ID;
					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
					objWrapper.AddParameter("@BOAT_ID",objNew.VEHICLE_ID);
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
					objWrapper.AddParameter("@LIMIT_1_TYPE",objNew.LIMIT_1_TYPE);
					objWrapper.AddParameter("@LIMIT_2_TYPE",objNew.LIMIT_2_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_1",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
					objWrapper.AddParameter("@DEDUCTIBLE_2",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_2));
					objWrapper.AddParameter("@LIMIT_1",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
					objWrapper.AddParameter("@LIMIT_2",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_2));
					objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",objNew.LIMIT1_AMOUNT_TEXT);
					objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",objNew.LIMIT2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",objNew.DEDUCTIBLE1_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",objNew.DEDUCTIBLE2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE",objNew.DEDUCTIBLE_1_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE",objNew.DEDUCTIBLE_2_TYPE);
					objWrapper.AddParameter("@WRITTEN_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
					objWrapper.AddParameter("@FULL_TERM_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
					
					string strTranXML = "";
					
					//Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					//Insert case
					//if ( objNew.COVERAGE_ID == -1 )
					if(objNew.ACTION=="I")
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Watercrafts/WatercraftCoverages.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);						
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();

					}
						
					else if(objNew.ACTION=="U")
					{
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Watercrafts/WatercraftCoverages.aspx.resx");
						strTranXML = this.GetTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);					
						
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);						
						
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();
						
						
					}
					else if(objNew.ACTION=="D")
					{
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Watercrafts/WatercraftCoverages.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

						objWrapper.ClearParameteres();						
						objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
						objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
						objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
						objWrapper.AddParameter("@BOAT_ID",objNew.VEHICLE_ID);
						objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
						str+=";" + objNew.COV_DESC;
						objWrapper.ExecuteNonQuery("Proc_DeleteUMBRELLA_WATERCRAFT_COVERAGES");
						objWrapper.ClearParameteres();
						string strTransXML = objBuilder.GetDeleteTransactionLogXML(objNew);
						sbTranXML.Append(strTransXML);

					}
				}
	
				sbTranXML.Append("</root>");
	
				if(sbTranXML.ToString()!="<root></root>")// || strCustomInfo!="")
				{
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = appID;
					objTransactionInfo.APP_VERSION_ID = appVersionID;
					objTransactionInfo.CLIENT_ID = customerID;
				
					objTransactionInfo.TRANS_DESC		=	"Umbrella coverages updated.";
					objTransactionInfo.CUSTOM_INFO = hiddenCustomInfo + ";" +  strCustomInfo;
					if(sbTranXML.ToString()!="<root></root>")
						objTransactionInfo.CHANGE_XML		=	sbTranXML.ToString();
					else
						objTransactionInfo.CHANGE_XML ="";
					

					objWrapper.ExecuteNonQuery(objTransactionInfo);
					objWrapper.ClearParameteres();
				}
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
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;
		}


		/// <summary>
		/// Gets Umbrella watercraft coverages from database
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="boatID"></param>
		/// <param name="appType"></param>
		/// <returns></returns>
		public DataSet GetUmbrellaWatercraftCoverages(int customerID, int appID, 
			int appVersionID, int boatID, string appType)
		{
			string	strStoredProc =	"Proc_GetAPP_UMBRELLA_WATERCRAFT_COVERAGES";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@VEHICLE_ID",boatID);
			objWrapper.AddParameter("@APP_TYPE",appType);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;

			
		}
		*/

		/// <summary>
		/// Saves coverages records in APP_WATERCRAFT_COBVERAGE_INFO table
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <param name="strOldXML"></param>
		/// <returns></returns>
		/*public int SaveWatercraftCoverages(ArrayList alNewCoverages,string strOldXML)
		{
			//Temporary call added/ To be removed once the WatercraftCoverages page is checked-in			
			string	strStoredProc =	"Proc_Save_WATERCRAFT_COVERAGES";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			SqlCommand cmdCoverage = new SqlCommand();
			cmdCoverage.CommandText = strStoredProc;
			cmdCoverage.CommandType = CommandType.StoredProcedure;
		
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
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Application.ClsCoveragesInfo objNew = (ClsCoveragesInfo)alNewCoverages[i];

					customerID = objNew.CUSTOMER_ID;
					appID = objNew.APP_ID;
					appVersionID = objNew.APP_VERSION_ID;
					
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
					objWrapper.AddParameter("@VEHICLE_ID",objNew.VEHICLE_ID);
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
					objWrapper.AddParameter("@LIMIT_1_TYPE",objNew.LIMIT_1_TYPE);
					objWrapper.AddParameter("@LIMIT_2_TYPE",objNew.LIMIT_2_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_1",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
					objWrapper.AddParameter("@DEDUCTIBLE_2",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_2));
					objWrapper.AddParameter("@LIMIT_1",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
					objWrapper.AddParameter("@LIMIT_2",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_2));
					objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",objNew.LIMIT1_AMOUNT_TEXT);
					objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",objNew.LIMIT2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",objNew.DEDUCTIBLE1_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",objNew.DEDUCTIBLE2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE",objNew.DEDUCTIBLE_1_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE",objNew.DEDUCTIBLE_2_TYPE);
					objWrapper.AddParameter("@WRITTEN_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
					objWrapper.AddParameter("@FULL_TERM_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
					
					string strTranXML = "";
					
					//Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;

					if ( objNew.ACTION == "I")
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Watercrafts/WatercraftCoverages.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vehicle coverages Updated.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						sbTranXML.Append(strTranXML);
						//objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();

					}
					else if (  objNew.ACTION == "U")
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Watercrafts/WatercraftCoverages.aspx.resx");
				
						strTranXML = this.GetTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);
						
						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vehicle coverages modified.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						sbTranXML.Append(strTranXML);
						//objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();
					}
				
				
					//objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					objWrapper.ClearParameteres();

				}
				sbTranXML.Append("</root>");

				objTransactionInfo.TRANS_TYPE_ID	=	2;
				objTransactionInfo.APP_ID = appID;
				objTransactionInfo.APP_VERSION_ID = appVersionID;
				objTransactionInfo.CLIENT_ID = customerID;
				
				objTransactionInfo.TRANS_DESC		=	"Watercraft coverages updated.";
				
				objTransactionInfo.CHANGE_XML		=	sbTranXML.ToString();
				
				objWrapper.ClearParameteres();

				objWrapper.ExecuteNonQuery(objTransactionInfo);
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
		
		/// <summary>
		/// Returns an XML containing coverages to be removed
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <param name="objDataSet"></param>
		/// <returns></returns>
		public string GetUmbrellaWatCoveragesToRemove(int customerID,int appID, int appVersionID, 
			int boatID, DataSet objDataSet,string calledFrom)
		{
			//Get Vehicle information
			//dsWatercraftInfo = this.FetchWatercraftInfo(customerID,appID,appVersionID,boatID,"UMB");
			
			StringBuilder sbXML = new StringBuilder();
			
			if( dsWatercraftInfo==null ) return "";

			if ( dsWatercraftInfo.Tables.Count == 0 ) return "";

			DataTable dtVehicle = objDataSet.Tables[3];

			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtLimits = objDataSet.Tables[1];	
		
			if ( sbXML.ToString() == "" )
			{
				sbXML.Append("<Coverages>");
			}

			string strWatercraftType = "";

			if(dtVehicle.Rows[0]["TYPE_OF_WATERCRAFT"]!=System.DBNull.Value)	
			{
				strWatercraftType = dtVehicle.Rows[0]["TYPE_OF_WATERCRAFT"].ToString().Trim();
				
				//Mini Jet Boat
				if ( strWatercraftType == "11373" )
				{
					//Remove Client Entertainment Liability (OP 720) ***************
					//Section II - Uninsured Watercraft Liability (CSL)
					DataRow[] dr = dtCoverage.Select("COV_CODE='EBSMECE' OR COV_CODE='UMBCS'");
				
					if ( dr != null  && dr.Length > 0 )
					{				
						foreach(DataRow dr1 in dr)
						{
							string covID = dr1["COV_ID"].ToString();
							string mand = dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
							sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand  + "'>");
							sbXML.Append("</Coverage>");
						}
						
		
					}	
				}
				
				//Jet ski
				if ( strWatercraftType == "11390" )
				{
					//Remove these:
					//1.Trailers
					//Remove Trailer - Jet Ski
					DataRow[] dr = dtCoverage.Select("COV_CODE='EBSMT'");
				
					if ( dr != null  && dr.Length > 0 )
					{				
						foreach(DataRow dr1 in dr)
						{
							string covID = dr1["COV_ID"].ToString();
							string mand=dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
							sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='N'>");
							sbXML.Append("</Coverage>");
						}
						
		
					}
				}

				//Jet ski with Lift bar
				if ( strWatercraftType == "11387" )
				{
					//Remove these:
					//1. Section I - Covered Property, Physical Damage - Actual cash value
					//2. Section I - Covered Property, Physical Damage - Agreed Value
					//3. Client Entertainment Liability (OP 720)
					//4. Trailers
					//5. Section II - Uninsured Watercraft Liability (CSL) 

					DataRow[] dr = dtCoverage.Select("COV_CODE='EBPPDACV' OR COV_CODE='EBPPDAV' OR COV_CODE='EBSMECE' OR COV_CODE='UMBCS' OR COV_CODE='EBSMT' ");
				
					if ( dr != null  && dr.Length > 0 )
					{				
						foreach(DataRow dr1 in dr)
						{
							string covID = dr1["COV_ID"].ToString();
							string mand=dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
							sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand + "'>");
							sbXML.Append("</Coverage>");
						}
						
		
					}	
				}
				else
				{
					//Remove 
					//1. Trailer - Jet Ski
					//2. Section I - Covered Property, Physical Damage Jet Ski with Life Bar 
					DataRow[] dr = dtCoverage.Select("COV_CODE='EBSMETJ' OR COV_CODE='EBPPDJ'");
				
					if ( dr != null  && dr.Length > 0 )
					{				
						foreach(DataRow dr1 in dr)
						{
							string covID = dr1["COV_ID"].ToString();
							string mand=dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
							sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='N'>");
							sbXML.Append("</Coverage>");
						}
						
		
					}
				}
				
				//Waverunner
				if ( strWatercraftType == "11386" )
				{
					DataRow[] dr = dtCoverage.Select("COV_CODE='EBSMECE' OR COV_CODE='UMBCS' OR COV_CODE='EBSCEAV' ");
				
					if ( dr != null  && dr.Length > 0 )
					{				
						foreach(DataRow dr1 in dr)
						{
							string covID = dr1["COV_ID"].ToString();
							string mand=dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
							sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand  + "'>");
							sbXML.Append("</Coverage>");
						}
						
		
					}	
				}
				
				

							
			}//end of if


			if(dtVehicle.Rows[0]["LENGTH"]!=null)
			{
				double len=dtVehicle.Rows[0]["LENGTH"].ToString()==""?0:double.Parse(dtVehicle.Rows[0]["LENGTH"].ToString()); ;
				double insuredValue=dtVehicle.Rows[0]["INSURING_VALUE"].ToString()==""?0:double.Parse(dtVehicle.Rows[0]["INSURING_VALUE"].ToString()); ;
				int year=dtVehicle.Rows[0]["YEAR"].ToString()==""?0:int.Parse(dtVehicle.Rows[0]["YEAR"].ToString()); ;
				
				int age = DateTime.Today.Year - year;

				if(len>26 || insuredValue>75000 || age > 20)
				{
					DataRow[] dr = dtCoverage.Select("COV_CODE='EBSCEAV'");
							
					if ( dr != null  && dr.Length > 0 )
					{
						foreach(DataRow dr1 in dr)
						{
							string covID = dr1["COV_ID"].ToString();
							string mand=dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
							sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand + "'>");
							sbXML.Append("</Coverage>");
						}
					}						
				}				
			}
			

			//If called from Homeowners, ***********************************
			if ( calledFrom == "HWAT" )
			{
				//Remove options for Section II - Liability (CSL) 
				DataRow[] drLiab = dtCoverage.Select("COV_CODE='LCCSAY'");
			
				if ( drLiab != null  && drLiab.Length > 0 )
				{
					string covID = drLiab[0]["COV_ID"].ToString();

					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"COMP\" Remove=\"N\" Mandatory='" + drLiab[0]["IS_MANDATORY"].ToString() + "'>");
			
					DataRow[] drLimits = dtLimits.Select("COV_ID=" + covID);

					foreach(DataRow drLimit in drLimits)
					{	
						if ( drLimit["LIMIT_DEDUC_AMOUNT"] != System.DBNull.Value )
						{
							if ( drLimit["LIMIT_DEDUC_AMOUNT"].ToString() == "25000" ||  drLimit["LIMIT_DEDUC_AMOUNT"].ToString() == "50000" )
							{
								sbXML.Append("<Limit id='"+drLimit["LIMIT_DEDUC_ID"]+"' amount='"+drLimit["LIMIT_DEDUC_AMOUNT"]+"' type='"+drLimit["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
								sbXML.Append("</Limit>");	
							}

						}
					}
		
					sbXML.Append("</Coverage>");
				}
			}
			//***End of called from Home************************************************

			sbXML.Append("</Coverages>");
		
	
			return sbXML.ToString();
			

		}
		
		*/

		#endregion

	
		#region "Manipulate coverages"
			
		/// <summary>
		/// Returns an XML containing coverages to be removed
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <param name="objDataSet"></param>
		/// <returns></returns>
		public string GetWatCoveragesToRemoveForPolicy(int customerID,int polID, int polVersionID, 
			int boatID, DataSet objDataSet,string calledFrom)
		{
			
			StringBuilder sbXML = new StringBuilder();
			
			//if( dsWatercraftInfo==null ) return "";

			//if ( dsWatercraftInfo.Tables.Count == 0 ) return "";

			DataTable dtVehicle = objDataSet.Tables[3];

			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtLimits = objDataSet.Tables[1];	
			
			if( dtVehicle==null ) return "";

			if ( dtVehicle.Rows.Count == 0 ) return "";

			int age = 0;
			int year = 0;
			
			double len = 0;
			double insuredValue = 0;
			
			string strWatercraftType  = "";
			string strWaterCraftStyle ="";
			string strCovType  = "";
			//Store The Coverage
			if(dtVehicle.Rows[0]["COV_TYPE_BASIS"]!=System.DBNull.Value)	
			{
				strCovType = dtVehicle.Rows[0]["COV_TYPE_BASIS"].ToString().Trim();
			}
			//Store The Type Of WaterCraft
			if(dtVehicle.Rows[0]["TYPE_OF_WATERCRAFT"]!=System.DBNull.Value)	
			{
				strWatercraftType = dtVehicle.Rows[0]["TYPE_OF_WATERCRAFT"].ToString().Trim();
			}
			//Store The Style Of WaterCraft
			if(dtVehicle.Rows[0]["TYPE"]!=System.DBNull.Value)	
			{
				strWaterCraftStyle = dtVehicle.Rows[0]["TYPE"].ToString().Trim();
			}
			
			
			sbXML.Append("<Coverages>");

			if(dtVehicle.Rows[0]["TYPE_OF_WATERCRAFT"]!=System.DBNull.Value && dtVehicle.Rows[0]["TYPE"]!=System.DBNull.Value )	
			{

				string strRemove = this.GetCoveragesToRemoveBasedOnType(strWatercraftType,strWaterCraftStyle,objDataSet);
				sbXML.Append(strRemove);
								
			}//end of if
			
			
			

			if ( dtVehicle.Rows[0]["LENGTH"] != DBNull.Value )
			{
				len=dtVehicle.Rows[0]["LENGTH"].ToString()==""?0:double.Parse(dtVehicle.Rows[0]["LENGTH"].ToString()); ;
			}

			if ( dtVehicle.Rows[0]["INSURING_VALUE"] != DBNull.Value )
			{
				insuredValue=dtVehicle.Rows[0]["INSURING_VALUE"].ToString()==""?0:double.Parse(dtVehicle.Rows[0]["INSURING_VALUE"].ToString()); ;
			}

			//Age of Watercraft
			
			if ( dtVehicle.Rows[0]["YEAR"] != System.DBNull.Value )
			{
				year = Convert.ToInt32(dtVehicle.Rows[0]["YEAR"]);
						
			}
			if ( objDataSet.Tables[2].Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
			{
				age = Convert.ToInt32(objDataSet.Tables[2].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) - year;			
			}

			//If Age Of WaterCraft Is Greater Than 5 years Then  Remove The Coverage
			//Boat Replacement Cost Coverage (BRCC)
			if(age > 5)
			{
				DataRow[] dr = dtCoverage.Select("COV_CODE='BRCC'");
							
				if ( dr != null  && dr.Length > 0 )
				{
					foreach(DataRow dr1 in dr)
					{
						string covID = dr1["COV_ID"].ToString();
						string mand=dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand + "'>");
						sbXML.Append("</Coverage>");
					}
				}
			}
	
	

			if ( sbXML.ToString() == "" )
			{
				sbXML.Append("<Coverages>");
			}

	
			//AV-100
			if(len>26 || insuredValue>75000 || age > 20)
			{
				DataRow[] dr = dtCoverage.Select("COV_CODE='EBSCEAV'");
						
				if ( dr != null  && dr.Length > 0 )
				{
					foreach(DataRow dr1 in dr)
					{
						string covID = dr1["COV_ID"].ToString();
						string mand=dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand + "'>");
						sbXML.Append("</Coverage>");
					}
				}						
			}		
			//If Coverage Type is not applicable 
			//Remove Coverage --Section 1 - Covered Property Damage Jet Ski

			if(strCovType == "11978" )
			{
				DataRow[] dr ;
				dr = dtCoverage.Select("COV_CODE='EBPPDJ'");
				if(dr != null && dr.Length > 0)
				{
					foreach(DataRow dr1 in dr)
					{
						string covID = dr1["COV_ID"].ToString();
						string mand=   dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand + "'>");
						sbXML.Append("</Coverage>");
					}
				}
				dr = dtCoverage.Select("COV_CODE='EBPPDACV'");
				if(dr != null && dr.Length > 0)
				{
					foreach(DataRow dr1 in dr)
					{
						string covID = dr1["COV_ID"].ToString();
						string mand=   dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand + "'>");
						sbXML.Append("</Coverage>");
					}
				}
				dr = dtCoverage.Select("COV_CODE='EBPPDAV'");
				if(dr != null && dr.Length > 0)
				{
					foreach(DataRow dr1 in dr)
					{
						string covID = dr1["COV_ID"].ToString();
						string mand=   dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand + "'>");
						sbXML.Append("</Coverage>");
					}
				}
			}
			// added by Pravesh to Remove Agreed Value (AV 100) if coverage basis is Actual Cash Value or Not Applicable
			if(strCovType == "11758" || strCovType == "11978" )
			{
				DataRow[] dr = dtCoverage.Select("COV_CODE='EBSCEAV'");
				if(dr != null && dr.Length > 0)
				{
					foreach(DataRow dr1 in dr)
					{
						string covID = dr1["COV_ID"].ToString();
						string mand=   dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand + "'>");
						sbXML.Append("</Coverage>");
					}
				}

			}
			// end here by Pravesh
			//Section 1 - Covered Property Damage - Actual Cash Value
			//Section 1 - Covered Property Damage - Agreed Value

			//if(strWaterCraftStyle != "JS")			//commented by Pravesh on 19 sep 2007 as Coverage --Section 1 - Covered Property Damage Jet Ski  has been removed 
														//and Actual cash value or Agreed Value will be applicable base on Covarage Base
			//{
				DataRow[] drEBPPDACV = dtCoverage.Select("COV_CODE='EBPPDACV'");
				if(drEBPPDACV != null && drEBPPDACV.Length > 0)
				{
					foreach(DataRow dr1 in drEBPPDACV)
					{
						if ( strCovType   != "11758" )						
						{
							string covID = dr1["COV_ID"].ToString();
							string mand=   dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
							sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand + "'>");
							sbXML.Append("</Coverage>");
						}
						

					}

				}
				DataRow[] drEBPPDAV = dtCoverage.Select("COV_CODE='EBPPDAV'");
				if(drEBPPDAV != null && drEBPPDAV.Length > 0)
				{
					foreach(DataRow dr1 in drEBPPDAV)
					{
						if ( strCovType   !=  "11759" )						
						{
							string covID = dr1["COV_ID"].ToString();
							string mand=   dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
							sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand + "'>");
							sbXML.Append("</Coverage>");
						}
						
					}

				}


			//}
			
			//by pravesh remove limit "Extented from HO" if length is less than 26 and boat type is Sailboat
			if(len>=26 && (strWatercraftType=="11372" || strWatercraftType =="11672") )
			{
				DataRow[] dr = dtCoverage.Select("COV_CODE in ('LCCSL','MCPAY')");
							
				if ( dr != null  && dr.Length > 0 )
				{
					foreach(DataRow dr1 in dr)
					{
						string covID = dr1["COV_ID"].ToString();
						string mand=dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='" + mand + "'>");
						DataRow[] drLimits = dtLimits.Select("COV_ID=" + covID + " and LIMIT_DEDUC_ID IN (1412,1413,1414,1415,1416,1417)");
						foreach(DataRow drLimit in drLimits)
						{	
							if ( drLimit["LIMIT_DEDUC_ID"] != System.DBNull.Value )
							{
						
								sbXML.Append("<Limit id='"+drLimit["LIMIT_DEDUC_ID"]+"' amount='"+drLimit["LIMIT_DEDUC_AMOUNT"]+"' type='"+drLimit["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
								sbXML.Append("</Limit>");	
						
							}
						}

						sbXML.Append("</Coverage>");
					}
				}			
			}				
			
			//end here

			//If called from Homeowners, ***********************************
			if ( calledFrom == "HWAT" )
			{
				//Remove options for Section II - Liability (CSL) 
				DataRow[] drLiab = dtCoverage.Select("COV_CODE='LCCSAY'");
			
				if ( drLiab != null  && drLiab.Length > 0 )
				{
					string covID = drLiab[0]["COV_ID"].ToString();

					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"COMP\" Remove=\"N\" Mandatory='" + drLiab[0]["IS_MANDATORY"].ToString() + "'>");
			
					DataRow[] drLimits = dtLimits.Select("COV_ID=" + covID);

					foreach(DataRow drLimit in drLimits)
					{	
						if ( drLimit["LIMIT_DEDUC_AMOUNT"] != System.DBNull.Value )
						{
							if ( drLimit["LIMIT_DEDUC_AMOUNT"].ToString() == "25000" ||  drLimit["LIMIT_DEDUC_AMOUNT"].ToString() == "50000" )
							{
								sbXML.Append("<Limit id='"+drLimit["LIMIT_DEDUC_ID"]+"' amount='"+drLimit["LIMIT_DEDUC_AMOUNT"]+"' type='"+drLimit["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
								sbXML.Append("</Limit>");	
							}

						}
					}
		
					sbXML.Append("</Coverage>");
				}
			}
			//***End of called from Home************************************************

			sbXML.Append("</Coverages>");
		
	
			return sbXML.ToString();
			

		}
		


		//END
		
	
		/// <summary>
		/// Returns an XML with Coverages to be removed
		/// </summary>
		/// <param name="strWatercraftType"></param>
		/// <param name="objDataSet"></param>
		/// <returns></returns>
		public string GetCoveragesToRemoveBasedOnType(string strWatercraftType ,string strWaterCraftStyle, DataSet objDataSet)
		{
			string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/Coverages/WatercraftCoverages.xml");
			//string xml = "";
			StringBuilder sbXML = new StringBuilder();
			
			DataTable dtVehicle = objDataSet.Tables[3];
			
			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtLimits = objDataSet.Tables[1];	
			DataTable dtStateLob =objDataSet.Tables[2];	
			string LOBID=dtStateLob.Rows[0]["LOB_ID"].ToString();
			//Read Coverages to remove from XML file
			XmlDocument doc = new XmlDocument();

			doc.Load(filePath);

			XmlNode node = doc.SelectSingleNode("Root/Boat[@ID='" +  strWaterCraftStyle + "']");	
			XmlNode removeNode = node.SelectSingleNode("Remove");
			XmlNodeList covList = removeNode.SelectNodes("Coverage");

			foreach(XmlNode remNode in covList)
			{
				if ( remNode.Attributes["Code"] != null )
				{
					string covCode = remNode.Attributes["Code"].Value;
					
					//Find ID from datatable
					DataRow[] dr = dtCoverage.Select("COV_CODE='" + covCode + "'");
					

					foreach(DataRow dr1 in dr)
					{
						string covID = dr1["COV_ID"].ToString();
						string mand = dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand  + "'>");
						sbXML.Append("</Coverage>");
					}
				}
			}

			//Remove The Coverages On Ths Bases Of Type Of watercraft
			node = doc.SelectSingleNode("Root/Boat[@ID='" +  strWatercraftType + "']");	
			if ( node != null)
			{
				removeNode = node.SelectSingleNode("Remove");
			    covList = removeNode.SelectNodes("Coverage");

				foreach(XmlNode remNode in covList)
				{
					if ( remNode.Attributes["Code"] != null )
					{
						string covCode = remNode.Attributes["Code"].Value;
					
						//Find ID from datatable
						DataRow[] dr = dtCoverage.Select("COV_CODE='" + covCode + "'");
					
						foreach(DataRow dr1 in dr)
						{
							string covID = dr1["COV_ID"].ToString();
							string mand = dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
							sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand  + "'>");
							sbXML.Append("</Coverage>");
						}
					}
				}
			}

			
			//For Jetski's, waverunner and Mini jet boats, min Medical Payments is 1000************
			if ( strWatercraftType == "11390" || strWatercraftType == "11387" || strWatercraftType == "11373" || strWatercraftType == "11386" )
			{
				//Get the limit options
				DataRow[] drMed = dtLimits.Select("COV_ID IN (21,68,821) AND LIMIT_DEDUC_AMOUNT > 1000");
				
				if ( drMed.Length > 0) 
				{
					int covID = Convert.ToInt32(drMed[0]["COV_ID"]);
					sbXML.Append("<Coverage COV_ID='" + covID.ToString() + "'" + " COV_CODE='Code'" + " Remove='N' Mandatory='N'>");
				
					foreach(DataRow dr in drMed)
					{
						if ( dr["LIMIT_DEDUC_AMOUNT"] != System.DBNull.Value )
						{
							string amt = dr["LIMIT_DEDUC_AMOUNT"].ToString();
		
							sbXML.Append("<Limit id='"+dr["LIMIT_DEDUC_ID"]+"' amount='"+dr["LIMIT_DEDUC_AMOUNT"]+"' type='"+dr["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
							sbXML.Append("</Limit>");	
						
						}
					}

					sbXML.Append("</Coverage>");
				}
			}
			//*************************************************************************************
			//added by pravesh
			//Get All The Coverage whose Limit,Deductible Is to Be removed  based on Commercial/Personal
			XmlNode node1 = doc.SelectSingleNode("Root/Boat[@ID='" +  strWaterCraftStyle + "']");	
			XmlNode removeNode1 = node1.SelectSingleNode("Remove");
			XmlNodeList removeNodeLimit = removeNode1.SelectNodes("CoverageLimit");
			StringBuilder  sbRemoveLimit =new StringBuilder(); 
			foreach(XmlNode removeNodeList in removeNodeLimit )
			{
				string covID = removeNodeList.Attributes["COV_ID"].Value;
				XmlNodeList removeLimitList = removeNodeList.SelectNodes("LIMIT_DEDUC");
				sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"COMP\" Remove=\"N\" Mandatory=\"N\">");
				//Loop thru each coveages to remove
				
				foreach(XmlNode remNode in removeLimitList)
				{
					string strLIMIT_DEDUC_ID = remNode.Attributes["LIMIT_DEDUC_ID"].Value;
					string Amount = remNode.Attributes["Amount"].Value;
					if ( sbRemoveLimit.ToString() == "" )
					{
						sbRemoveLimit.Append(strLIMIT_DEDUC_ID);
					}
					else
					{
						sbRemoveLimit.Append("," + strLIMIT_DEDUC_ID );
					}
				
				}
				DataRow[] drLimits = dtLimits.Select("COV_ID=" + covID  + " and LIMIT_DEDUC_ID IN (" + sbRemoveLimit.ToString() + ")");
				foreach(DataRow drLimit in drLimits)
				{	
					if ( drLimit["LIMIT_DEDUC_ID"] != System.DBNull.Value )
					{
						
						sbXML.Append("<Limit id='"+drLimit["LIMIT_DEDUC_ID"]+"' amount='"+drLimit["LIMIT_DEDUC_AMOUNT"]+"' type='"+drLimit["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
						sbXML.Append("</Limit>");	
						
					}
				}
				sbXML.Append("</Coverage>");
			}
			//BY PRAVESH
			XmlNode node2 = doc.SelectSingleNode("Root/Boat[@ID='" +  strWatercraftType + "']");	
			if (node2!=null)
			{
				XmlNode removeNode2 = node2.SelectSingleNode("Remove");
				XmlNodeList removeNodeLimit2 = removeNode2.SelectNodes("CoverageLimit");
				StringBuilder  sbRemoveLimit2 =new StringBuilder(); 
				foreach(XmlNode removeNodeList in removeNodeLimit2 )
				{
					string covID = removeNodeList.Attributes["COV_ID"].Value;
					XmlNodeList removeLimitList = removeNodeList.SelectNodes("LIMIT_DEDUC");
					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"COMP\" Remove=\"N\" Mandatory=\"N\">");
					//Loop thru each coveages to remove
				
					foreach(XmlNode remNode in removeLimitList)
					{
						string strLIMIT_DEDUC_ID = remNode.Attributes["LIMIT_DEDUC_ID"].Value;
						string Amount = remNode.Attributes["Amount"].Value;
						if ( sbRemoveLimit2.ToString() == "" )
						{
							sbRemoveLimit2.Append(strLIMIT_DEDUC_ID);
						}
						else
						{
							sbRemoveLimit2.Append("," + strLIMIT_DEDUC_ID );
						}
				
					}
					DataRow[] drLimits = dtLimits.Select("COV_ID=" + covID  + " and LIMIT_DEDUC_ID IN (" + sbRemoveLimit2.ToString() + ")");
					foreach(DataRow drLimit in drLimits)
					{	
						if ( drLimit["LIMIT_DEDUC_ID"] != System.DBNull.Value )
						{
						
							sbXML.Append("<Limit id='"+drLimit["LIMIT_DEDUC_ID"]+"' amount='"+drLimit["LIMIT_DEDUC_AMOUNT"]+"' type='"+drLimit["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
							sbXML.Append("</Limit>");	
						
						}
					}
					sbXML.Append("</Coverage>");
				}
			}
			//Removing Limt on the basis of LOB
			XmlNode node3 = doc.SelectSingleNode("Root/LOB[@ID='" +  LOBID + "']");	
			if (node3!=null)
			{
				XmlNode removeNode3 = node3.SelectSingleNode("Remove");
				XmlNodeList removeNodeLimit3 = removeNode3.SelectNodes("CoverageLimit");
				StringBuilder  sbRemoveLimit3 =new StringBuilder(); 
				foreach(XmlNode removeNodeList in removeNodeLimit3 )
				{
					string covID = removeNodeList.Attributes["COV_ID"].Value;
					XmlNodeList removeLimitList = removeNodeList.SelectNodes("LIMIT_DEDUC");
					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"COMP\" Remove=\"N\" Mandatory=\"N\">");
					//Loop thru each coveages to remove
				
					foreach(XmlNode remNode in removeLimitList)
					{
						string strLIMIT_DEDUC_ID = remNode.Attributes["LIMIT_DEDUC_ID"].Value;
						string Amount = remNode.Attributes["Amount"].Value;
						if ( sbRemoveLimit3.ToString() == "" )
						{
							sbRemoveLimit3.Append(strLIMIT_DEDUC_ID);
						}
						else
						{
							sbRemoveLimit3.Append("," + strLIMIT_DEDUC_ID );
						}
				
					}
					DataRow[] drLimits = dtLimits.Select("COV_ID=" + covID  + " and LIMIT_DEDUC_ID IN (" + sbRemoveLimit3.ToString() + ")");
					foreach(DataRow drLimit in drLimits)
					{	
						if ( drLimit["LIMIT_DEDUC_ID"] != System.DBNull.Value )
						{
						
							sbXML.Append("<Limit id='"+drLimit["LIMIT_DEDUC_ID"]+"' amount='"+drLimit["LIMIT_DEDUC_AMOUNT"]+"' type='"+drLimit["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
							sbXML.Append("</Limit>");	
						
						}
					}
					sbXML.Append("</Coverage>");
				}
			}

			//end here

			return sbXML.ToString();
		}


		/// <summary>
		/// Returns an XML containing coverages to be removed
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <param name="objDataSet"></param>
		/// <returns></returns>
		public string GetWatCoveragesToRemove(int customerID,int appID, int appVersionID, 
			int boatID, DataSet objDataSet,string calledFrom)
		{
						
			StringBuilder sbXML = new StringBuilder();
			
			

			DataTable dtVehicle = objDataSet.Tables[3];

			if( dtVehicle==null ) return "";

			if ( dtVehicle.Rows.Count == 0 ) return "";

			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtLimits = objDataSet.Tables[1];	
			
			int age = 0;
			int year = 0;
			
			double len = 0;
			double insuredValue = 0;
			
			string strWatercraftType  = "";
			string strWaterCraftStyle = ""; 
			string strCovType= ""; 
            /*
			11758 Actual Cash Value
		     11759 Agreed Value
			 */
			//Store The Coverage
			if(dtVehicle.Rows[0]["COV_TYPE_BASIS"]!=System.DBNull.Value)	
			{
				strCovType = dtVehicle.Rows[0]["COV_TYPE_BASIS"].ToString().Trim();
			}
			//Store The Type Of WaterCraft
			if(dtVehicle.Rows[0]["TYPE_OF_WATERCRAFT"]!=System.DBNull.Value)	
			{
				strWatercraftType = dtVehicle.Rows[0]["TYPE_OF_WATERCRAFT"].ToString().Trim();
			}
            //Store The Style Of WaterCraft
			if(dtVehicle.Rows[0]["TYPE"]!=System.DBNull.Value)	
			{
				strWaterCraftStyle = dtVehicle.Rows[0]["TYPE"].ToString().Trim();
			}
			
			
			if ( dtVehicle.Rows[0]["LENGTH"] != DBNull.Value )
			{
				len=dtVehicle.Rows[0]["LENGTH"].ToString()==""?0:double.Parse(dtVehicle.Rows[0]["LENGTH"].ToString()); ;
			}

			if ( dtVehicle.Rows[0]["INSURING_VALUE"] != DBNull.Value )
			{
				insuredValue=dtVehicle.Rows[0]["INSURING_VALUE"].ToString()==""?0:double.Parse(dtVehicle.Rows[0]["INSURING_VALUE"].ToString()); ;
			}

			//Age of Watercraft
			if ( dtVehicle.Rows[0]["YEAR"] != System.DBNull.Value )
			{
				year = Convert.ToInt32(dtVehicle.Rows[0]["YEAR"]);
						
			}
			if ( objDataSet.Tables[2].Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
			{
				   age = Convert.ToInt32(objDataSet.Tables[2].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) - year;			
			}
	

	

			if ( sbXML.ToString() == "" )
			{
				sbXML.Append("<Coverages>");
			}

			
			/////////////////////////////////////////

		

			if(dtVehicle.Rows[0]["TYPE_OF_WATERCRAFT"]!=System.DBNull.Value && dtVehicle.Rows[0]["TYPE"]!=System.DBNull.Value )	
			{

				string strRemove = this.GetCoveragesToRemoveBasedOnType(strWatercraftType,strWaterCraftStyle,objDataSet);
				sbXML.Append(strRemove);
								
			}//end of if

			//If Coverage Type is not applicable 
			//Remove Coverage --Section 1 - Covered Property Damage Jet Ski

			if(strCovType == "11978")
			{
				DataRow[] dr ;
				dr = dtCoverage.Select("COV_CODE='EBPPDJ'");
				if(dr != null && dr.Length > 0)
				{
					foreach(DataRow dr1 in dr)
					{
						string covID = dr1["COV_ID"].ToString();
						string mand=   dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand + "'>");
						sbXML.Append("</Coverage>");
					}
				}
				dr = dtCoverage.Select("COV_CODE='EBPPDACV'");
				if(dr != null && dr.Length > 0)
				{
					foreach(DataRow dr1 in dr)
					{
						string covID = dr1["COV_ID"].ToString();
						string mand=   dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand + "'>");
						sbXML.Append("</Coverage>");
					}
				}

				dr = dtCoverage.Select("COV_CODE='EBPPDAV'");
				if(dr != null && dr.Length > 0)
				{
					foreach(DataRow dr1 in dr)
					{
						string covID = dr1["COV_ID"].ToString();
						string mand=   dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand + "'>");
						sbXML.Append("</Coverage>");
					}
				}
			}
			// added by Pravesh to Remove Agreed Value (AV 100) if coverage basis is Actual Cash Value or not Applicable
			if(strCovType == "11758" || strCovType == "11978" )
			{
				DataRow[] dr = dtCoverage.Select("COV_CODE='EBSCEAV'");
				if(dr != null && dr.Length > 0)
				{
					foreach(DataRow dr1 in dr)
					{
						string covID = dr1["COV_ID"].ToString();
						string mand=   dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand + "'>");
						sbXML.Append("</Coverage>");
					}
				}

			}
			// end here by Pravesh
		   //Section 1 - Covered Property Damage - Actual Cash Value
		   //Section 1 - Covered Property Damage - Agreed Value
			//if(strWaterCraftStyle != "JS")     //commented by Pravesh on 19 sep 2007 as Coverage --Section 1 - Covered Property Damage Jet Ski  has been removed 
												//and Actual cash value or Agreed Value will be applicable base on Covarage Base
			//{
				DataRow[] drEBPPDACV = dtCoverage.Select("COV_CODE='EBPPDACV'");
				if(drEBPPDACV != null && drEBPPDACV.Length > 0)
				{
					foreach(DataRow dr1 in drEBPPDACV)
					{
						if ( strCovType   != "11758" )						
						{
							string covID = dr1["COV_ID"].ToString();
							string mand=   dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
							sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand + "'>");
							sbXML.Append("</Coverage>");
						}
						

					}

				}
				DataRow[] drEBPPDAV = dtCoverage.Select("COV_CODE='EBPPDAV'");
				if(drEBPPDAV != null && drEBPPDAV.Length > 0)
				{
					foreach(DataRow dr1 in drEBPPDAV)
					{
						if (  strCovType   !=  "11759" )						
						{
							string covID = dr1["COV_ID"].ToString();
							string mand=   dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
							sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand + "'>");
							sbXML.Append("</Coverage>");
						}
						
					}

				}


			//}
			
			
			
			//If Age Of WaterCraft Is Greater Than 5 years Then  Remove The Coverage
			//Boat Replacement Cost Coverage (BRCC)
			if(age > 5)
			{
				DataRow[] dr = dtCoverage.Select("COV_CODE='BRCC'");
							
				if ( dr != null  && dr.Length > 0 )
				{
					foreach(DataRow dr1 in dr)
					{
						string covID = dr1["COV_ID"].ToString();
						string mand=dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand + "'>");
						sbXML.Append("</Coverage>");
					}
				}
			}
		
			//AV-100
			if(len>26 || insuredValue>75000 || age > 20)
			{
				DataRow[] dr = dtCoverage.Select("COV_CODE='EBSCEAV'");
							
				if ( dr != null  && dr.Length > 0 )
				{
					foreach(DataRow dr1 in dr)
					{
						string covID = dr1["COV_ID"].ToString();
						string mand=dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"Y\" Mandatory='" + mand + "'>");
						sbXML.Append("</Coverage>");
					}
				}						
			}				
			//by pravesh remove limit "Extented from HO" if length is less than 26 and boat type is Sailboat
			if(len>=26 && (strWatercraftType=="11372" || strWatercraftType =="11672") )
			{
				DataRow[] dr = dtCoverage.Select("COV_CODE in ('LCCSL','MCPAY')");
							
				if ( dr != null  && dr.Length > 0 )
				{
					foreach(DataRow dr1 in dr)
					{
						string covID = dr1["COV_ID"].ToString();
						string mand=dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr1["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='" + mand + "'>");
						DataRow[] drLimits = dtLimits.Select("COV_ID=" + covID + " and LIMIT_DEDUC_ID IN (1412,1413,1414,1415,1416,1417)");
						foreach(DataRow drLimit in drLimits)
						{	
							if ( drLimit["LIMIT_DEDUC_ID"] != System.DBNull.Value )
							{
						
								sbXML.Append("<Limit id='"+drLimit["LIMIT_DEDUC_ID"]+"' amount='"+drLimit["LIMIT_DEDUC_AMOUNT"]+"' type='"+drLimit["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
								sbXML.Append("</Limit>");	
						
							}
						}

						sbXML.Append("</Coverage>");
					}
				}			
			}				
			
			//end here
			

			//If called from Homeowners, ***********************************
			if ( calledFrom == "HWAT" )
			{
				//Remove options for Section II - Liability (CSL) 
				DataRow[] drLiab = dtCoverage.Select("COV_CODE='LCCSAY'");
			
				if ( drLiab != null  && drLiab.Length > 0 )
				{
					string covID = drLiab[0]["COV_ID"].ToString();

					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"COMP\" Remove=\"N\" Mandatory='" + drLiab[0]["IS_MANDATORY"].ToString() + "'>");
			
					DataRow[] drLimits = dtLimits.Select("COV_ID=" + covID);

					foreach(DataRow drLimit in drLimits)
					{	
						if ( drLimit["LIMIT_DEDUC_AMOUNT"] != System.DBNull.Value )
						{
							if ( drLimit["LIMIT_DEDUC_AMOUNT"].ToString() == "25000" ||  drLimit["LIMIT_DEDUC_AMOUNT"].ToString() == "50000" )
							{
								sbXML.Append("<Limit id='"+drLimit["LIMIT_DEDUC_ID"]+"' amount='"+drLimit["LIMIT_DEDUC_AMOUNT"]+"' type='"+drLimit["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
								sbXML.Append("</Limit>");	
							}

						}
					}
		
					sbXML.Append("</Coverage>");
				}
			}
			//***End of called from Home************************************************

			sbXML.Append("</Coverages>");
		
		/*	//added by pravesh
			//Get All The Coverage whose Limit,Deductible Is to Be removed  based on Commercial/Personal
			XmlNodeList removeNodeLimit = vehUseNode.SelectNodes("Remove/CoverageLimit");
			foreach(XmlNode removeNodeList in removeNodeLimit )
			{
				string covID = removeNodeList.Attributes["COV_ID"].Value;
				XmlNodeList removeLimitList = removeNodeList.SelectNodes("LIMIT_DEDUC");
				sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"COMP\" Remove=\"N\" Mandatory=\"N\">");
				//Loop thru each coveages to remove
				
				foreach(XmlNode remNode in removeLimitList)
				{
					string strLIMIT_DEDUC_ID = remNode.Attributes["LIMIT_DEDUC_ID"].Value;
					string Amount = remNode.Attributes["Amount"].Value;
					if ( sbRemoveLimit.ToString() == "" )
					{
						sbRemoveLimit.Append(strLIMIT_DEDUC_ID);
					}
					else
					{
						sbRemoveLimit.Append("," + strLIMIT_DEDUC_ID );
					}
					
					
				
				}
				DataRow[] drLimits = dtLimits.Select("COV_ID=" + covID  + " and LIMIT_DEDUC_ID IN (" + sbRemoveLimit.ToString() + ")");
				foreach(DataRow drLimit in drLimits)
				{	
					if ( drLimit["LIMIT_DEDUC_ID"] != System.DBNull.Value )
					{
						
						sbXML.Append("<Limit id='"+drLimit["LIMIT_DEDUC_ID"]+"' amount='"+drLimit["LIMIT_DEDUC_AMOUNT"]+"' type='"+drLimit["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
						sbXML.Append("</Limit>");	
						
					}
				}
				sbXML.Append("</Coverage>");
			}
			//end here */
			return sbXML.ToString();
			

		}
	
		#endregion


		/// <summary>
		/// Updates relevant endorsments and coverages when watercraft is deleted
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="objDataWrapper"></param>
		public void UpdateAppCoveragesAndEndorsementsOnDelete(int customerID,int appID, int appVersionID, DataWrapper objDataWrapper)
		{
			if ( objDataWrapper.CommandParameters.Length > 0 )
			{
				objDataWrapper.ClearParameteres();
			}

			objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objDataWrapper.AddParameter("@APP_ID",appID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionID);

			objDataWrapper.ExecuteNonQuery("Proc_Update_WATERCRAFT_HOME_ENDORSEMENTS_DELETE");

		}

		/// <summary>
		/// Updates relevant endorsments and coverages when watercraft is deleted
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="objDataWrapper"></param>
		public void UpdatePolicyCoveragesAndEndorsementsOnDelete(int customerID,int polID, int polVersionID, DataWrapper objDataWrapper)
		{
			if ( objDataWrapper.CommandParameters.Length > 0 )
			{
				objDataWrapper.ClearParameteres();
			}

			objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objDataWrapper.AddParameter("@POLICY_ID",polID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);

			objDataWrapper.ExecuteNonQuery("Proc_Update_WATERCRAFT_HOME_ENDORSEMENTS_DELETE_FOR_POLICY");

		}

       
		
	}
}
