using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Application;
using Cms.Model.Policy;
using Cms.Model.Policy.Homeowners ;
using Cms.BusinessLayer.BlCommon;
using System.Web.UI.WebControls;
using System.Collections;
using Cms.BusinessLayer.BlApplication.HomeOwners;

namespace Cms.BusinessLayer.BlApplication
{
	
	public struct RuleData
	{
		public string BaseCoverage;
		public string MinValue;
		public string MaxValue;
		public string OnBlurFunction;
		public string ErrorMessage;
		public string ErrRequired;
		public string OnClickCoverage;
		public string OnClickFunction;
	}
	/// <summary>
	/// Summary description for ClsHomeCoverages.
	/// </summary>
	/// 
    public class ClsHomeCoverages : ClsCoverages
	{

		XmlDocument  homeRemoveXml;
		bool HomeRemoveXMLLoaded;	
		
		private int thiscreatedby;
		private int thisModifiedby;
		private const string HomeRuleXML = "/cmsweb/support/coverages/HomeCoveragesRule.xml";
		private const string RentalRuleXML ="/cmsweb/support/coverages/RentalCoveragesRule.xml";
	
		// State
		public enum enumState
		{
			Michigan = 22,
			Indiana  = 14,
			Wisconsin= 49
		}

		public ClsHomeCoverages()
		{
			if(IsEODProcess )
			{
				string strTemp = HomeRuleXML.Replace("/",@"\");
				filePath = WebAppUNCPath + strTemp;
				filePath=  System.IO.Path.GetFullPath(filePath);
			}
			else
			{
				filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + HomeRuleXML);
			}
			RuleDoc = new XmlDocument();
			RuleDoc.Load(filePath); 
			homeRemoveXml = new XmlDocument();
			HomeRemoveXMLLoaded=false;
			
		}
		public ClsHomeCoverages(string strRental)
		{
			
			if(IsEODProcess)
			{
				string strTemp = RentalRuleXML.Replace("/",@"\");
				filePath = WebAppUNCPath + strTemp;
				filePath=  System.IO.Path.GetFullPath(filePath);
			}
			else
			{
				filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + RentalRuleXML);
			}
			
			RuleDoc = new XmlDocument();
			RuleDoc.Load(filePath); 
			homeRemoveXml = new XmlDocument();
			HomeRemoveXMLLoaded=false;

		}
		#region  property for created by and modify by

	
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
		
		#region "Acord"

		/// <summary>
		/// Saves covereages from Quick quote interface
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <param name="strOldXML"></param>
		/// <param name="CoverageType"></param>
		/// <returns></returns>
		public int SaveAcordHomeCoverages(ArrayList alNewCoverages,string strOldXML,DataWrapper objWrapper)
		{
			
//			string	strStoredProc =	"Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD";

			int RetVal=0;
			
			try
			{
				RetVal = SaveHomeCoveragesNew(alNewCoverages,strOldXML,"", objWrapper);
				
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

			return RetVal;
		}


		#endregion

		#region "Application Rental"
		
		public int SaveRentalCoveragesNew(ArrayList alNewCoverages,string strOldXML,string CoverageType, string strCustomInfo)
		{
			
			string	strStoredProc =	"Proc_SAVE_HOME_COVERAGES";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			SqlCommand cmdCoverage = new SqlCommand();
			cmdCoverage.CommandText = strStoredProc;
			cmdCoverage.CommandType = CommandType.StoredProcedure;
			
			SqlParameter[] param = new SqlParameter[16];
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
			int dwellingId=0;
			//string strCustomInfo="Following coverages have been deleted:",str="";
			//strCustomInfo="";
		
			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					

					
					Cms.Model.Application.ClsCoveragesInfo objNew=(Cms.Model.Application.ClsCoveragesInfo)alNewCoverages[i];
					
					customerID = objNew.CUSTOMER_ID;
					appID = objNew.APP_ID;
					appVersionID = objNew.APP_VERSION_ID;
					dwellingId=objNew.RISK_ID;
					objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
				
					objWrapper.AddParameter("@DWELLING_ID",objNew.RISK_ID );
					
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
					objWrapper.AddParameter("@LIMIT_1_TYPE",objNew.LIMIT_1_TYPE);
					objWrapper.AddParameter("@LIMIT_2_TYPE",objNew.LIMIT_2_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_1",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
					objWrapper.AddParameter("@DEDUCTIBLE_2",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_2));
					objWrapper.AddParameter("@LIMIT_1",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
					objWrapper.AddParameter("@LIMIT_2",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_2));
					objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE",objNew.DEDUCTIBLE_1_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE",objNew.DEDUCTIBLE_2_TYPE);
					objWrapper.AddParameter("@WRITTEN_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
					objWrapper.AddParameter("@FULL_TERM_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
					objWrapper.AddParameter("@COVERAGE_TYPE",CoverageType);
					objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",objNew.LIMIT1_AMOUNT_TEXT);
					objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",objNew.LIMIT2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",objNew.DEDUCTIBLE1_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",objNew.DEDUCTIBLE2_AMOUNT_TEXT);
					objWrapper.AddParameter("@LIMIT_ID",DefaultValues.GetIntNullFromNegative(objNew.LIMIT_ID));
					objWrapper.AddParameter("@DEDUC_ID",DefaultValues.GetIntNullFromNegative(objNew.DEDUC_ID));
					objWrapper.AddParameter("@DEDUCTIBLE_AMOUNT",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE));
					objWrapper.AddParameter("@DEDUCTIBLE_TEXT",objNew.DEDUCTIBLE_TEXT);
					objWrapper.AddParameter("@ADDDEDUCTIBLE_ID",objNew.ADDDEDUCTIBLE_ID);

					string strTranXML = "";
					
					//Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					//if ( objNew.COVERAGE_ID == -1 )
					if(objNew.ACTION == "I")
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/homeowners/RentalCoverages.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						/*objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Home coverages added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;*/
						sbTranXML.Append(strTranXML);						
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();

					}
					else if ( objNew.ACTION == "U")
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/homeowners/RentalCoverages.aspx.resx");
				
						strTranXML = this.GetHomeTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);
						
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();
					}
				
					
					//Update linked Endorsements/////////////////////////////
					//	if ( CoverageType == "S1" )
					//	{
					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
					if(objNew.RISK_ID==0)
					{
						objWrapper.AddParameter("@DWELLING_ID",System.DBNull.Value);
					}
					else
					{
						objWrapper.AddParameter("@DWELLING_ID",objNew.RISK_ID);
					}
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);

					objWrapper.ExecuteNonQuery("Proc_UPDATE_DWELLING_ENDORSEMENTS");

					objWrapper.ClearParameteres();


					//	}
					//////////////////////////////////////////////////////////
					
					if(objNew.ACTION=="D")
					{
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/homeowners/RentalCoverages.aspx.resx");
						
						objWrapper.ClearParameteres();
						//str+=";" + objNew.COV_DESC;
						objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
						objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
						objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
						objWrapper.AddParameter("@DWELLING_ID",objNew.RISK_ID);					
						objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);						
						objWrapper.ExecuteNonQuery("Proc_DeleteAPP_HOME_COVERAGES");				
						objWrapper.ClearParameteres();
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						string strTransXML = objBuilder.GetDeleteTransactionLogXML(objNew);

						sbTranXML.Append(strTransXML);


						
					}
				}
				//Insert tran log entry//////////////////////////
				/*if(str!="")
					strCustomInfo+=str;
				else
					strCustomInfo="";*/
				
				sbTranXML.Append("</root>");

				//if(sbTranXML.ToString()!="<root></root>")
				//	strCustomInfo+=";Following coverages have been added/updated";

				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@CUSTOMER_ID",customerID );
				objWrapper.AddParameter("@APP_ID",appID );
				objWrapper.AddParameter("@APP_VERSION_ID",appVersionID );
				objWrapper.AddParameter("@DWELLING_ID",dwellingId);	
				objWrapper.ExecuteNonQuery("Proc_UPDATE_DWELLING_LINKED_COVERAGES");
				objWrapper.ClearParameteres();
				

				

				if(sbTranXML.ToString()!="<root></root>")// || strCustomInfo!="")
				{
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = appID;
					objTransactionInfo.APP_VERSION_ID = appVersionID;
					objTransactionInfo.CLIENT_ID = customerID;


                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1731", "");//"Rental coverages updated.";
					if(sbTranXML.ToString()!="<root></root>")
						objTransactionInfo.CHANGE_XML		=	sbTranXML.ToString();

					objTransactionInfo.CUSTOM_INFO=strCustomInfo;
				
					objWrapper.ClearParameteres();

					objWrapper.ExecuteNonQuery(objTransactionInfo);
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

		
		/// <summary>
		/// Gets rental coveages from the database
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <param name="dwellingID"></param>
		/// <param name="polType"></param>
		/// <param name="COVERAGETYPE"></param>
		/// <returns></returns>
		public  DataSet GetRentalDwellingSection1CoveragesForPolicy(int customerID, int polID, 
			int polVersionID, int dwellingID, string polType,string COVERAGETYPE)
		{
			string	strStoredProc =	"Proc_GetPOL_RENTAL_DWELLING_COVERAGES_SECTION1";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",polID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);
			objWrapper.AddParameter("@DWELLING_ID",dwellingID);
			objWrapper.AddParameter("@POLICY_TYPE",polType);
			objWrapper.AddParameter("@COVERAGE_TYPE",COVERAGETYPE);
			//objWrapper.AddParameter("@CURRENT_PAGE_INDEX",currentPageIndex);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		
		}
		
		/// <summary>
		/// Gets rental dwelling coverages from the database
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <param name="dwellingID"></param>
		/// <param name="polType"></param>
		/// <param name="COVERAGETYPE"></param>
		/// <returns></returns>
		public DataSet GetRentalDwellingSection1Coverages(int customerID, int appID, 
			int appVersionID, int dwellingID, string appType,string COVERAGETYPE)
		{
			string	strStoredProc =	"Proc_GetAPP_RENTAL_DWELLING_COVERAGES_SECTION1";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@DWELLING_ID",dwellingID);
			objWrapper.AddParameter("@APP_TYPE",appType);
			objWrapper.AddParameter("@COVERAGE_TYPE",COVERAGETYPE);
			//objWrapper.AddParameter("@CURRENT_PAGE_INDEX",currentPageIndex);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		
		}
		private string GetRentalCoveragesToRemove(DataSet objDataSet)
		{
			string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/Coverages/RentalCoverages.xml");
			StringBuilder sbXML = new StringBuilder();
			DataTable dtCoverage=objDataSet.Tables[0];
			DataTable dtHome = objDataSet.Tables[2];
			

			string stateID="";
			string product="";
			if(dtHome.Rows[0]["STATE_ID"]   != null)
			{
				stateID =dtHome.Rows[0]["STATE_ID"].ToString();

			}
			if(dtHome.Rows[0]["POLICY_TYPE"] != null)
			{
				product =dtHome.Rows[0]["POLICY_TYPE"].ToString();

			}
		
			if(HomeRemoveXMLLoaded == false)
			{
				homeRemoveXml.Load(filePath); 
				HomeRemoveXMLLoaded=true; 
			}
			
		
			XmlNode node = homeRemoveXml.SelectSingleNode("Root/State[@ID=" +  stateID.ToString() + "]");
	
			if ( node == null ) return "";

			XmlNode productNode = node.SelectSingleNode("Product[@ID=" + product.ToString() + "]");

			if ( productNode == null ) return "";

			XmlNode removeNode = productNode.SelectSingleNode("Remove/Coverages");

			if ( removeNode == null ) return "";

			XmlNodeList covList = removeNode.SelectNodes("Coverage");
			
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			string coverageID = "";
			string covCode ="";
				
			//Loop thru each coveages to remove
			foreach(XmlNode remNode in covList)
			{
				coverageID = remNode.Attributes["COV_ID"].Value;
				covCode = remNode.Attributes["Code"].Value;
				
				if ( sb.ToString() == "" )
				{
					sb.Append("'" + covCode + "'");
				}
				else
				{
					sb.Append(",'" + covCode + "'");
				}

			}
			
			//Get the coverages to remove from the master dataset and create XML
			DataRow[] drRemove = dtCoverage.Select("COV_CODE IN (" + sb.ToString() + ")");
			
			sbXML.Append("<Coverages>");
			
			foreach(DataRow dr1 in drRemove)
			{
				string covID = dr1["COV_ID"].ToString();
				string mand = dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
				covCode = dr1["COV_CODE"].ToString();
				
				sbXML.Append("<Coverage COV_ID='" + covID + "' COV_CODE='" + covCode + "' Remove='Y' Mandatory='N'>");
				sbXML.Append("</Coverage>");
			}
			
			//Added By Ravindra (08-20-2006)
			//County Check If Invalid County Remove Min Subsidience Coverage
			DataTable  dtLocations = objDataSet.Tables[5];
			int HAS_VALID_COUNTY = Convert.ToInt32(dtLocations.Rows[0]["HAS_VALID_COUNTY"]); 
			if(HAS_VALID_COUNTY == 0)
			{
				DataRow[] dr = dtCoverage.Select("COV_CODE IN ('MSC480')");
				covCode="MSC480";
				if ( dr != null  && dr.Length > 0 )
				{
					if ( sbXML.ToString() == "" )
					{
						sbXML.Append("<Coverages>");
					}

					coverageID  = dr[0]["COV_ID"].ToString();
					foreach(DataRow dr1 in dr)
					{
						coverageID = dr1["COV_ID"].ToString();
						covCode=dr1["COV_CODE"].ToString();
						sbXML.Append("<Coverage COV_ID='" + coverageID + "' COV_CODE='" + covCode + "' Remove='Y' Mandatory='N'>");
						sbXML.Append("</Coverage>");
					}

				}
			}
		//Added By Ravindra Ends Here
			
			
			sbXML.Append("</Coverages>");
			return sbXML.ToString();
		}

		/// <summary>
		///	Returns the coverages dataset after filtering records 
		/// </summary>
		/// <returns>dataset after filteration of records (coverages) that are no longer required </returns>
		public DataSet GetRentalCoverages(int customerID, int appID, int appVersionID, int dwellingID, string appType,string COVERAGETYPE)
		{
			//fetching dataset with all coverages
			DataSet dsCoverages=null;
			dsCoverages = this.GetRentalDwellingSection1Coverages(customerID,
				appID,
				appVersionID,
				dwellingID,appType,COVERAGETYPE
				);	

			//fetching XML string with all coverages to remove
		
			
			string covXML=this.GetRentalCoveragesToRemove(dsCoverages);	
			
			
			string mandXML = this.GetRentalMandatoryCoverages(customerID,
				appID,
				appVersionID,
				dwellingID,dsCoverages);
			
			/*Reading from the XML file and saving the text in string until the function start giving the XML string */
			
			/*TextReader tr=new StreamReader(System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath  + "/working/coveragedummyxml.xml"));
			string covXML=tr.ReadToEnd(); 
			tr.Close();*/
			
			 	
			
			//if XML string is not blank		
			if(covXML!="" )
			{
				//Cms.BusinessLayer.BlApplication.clsapplication 	objCovInformation = new Cms.BusinessLayer.BlApplication.clsapplication();
				//function call to delete coverage
				dsCoverages=this.DeleteCoverage(dsCoverages,covXML);			

				//function call to delete coverage limits
				dsCoverages=this.DeleteCoverageOptions(dsCoverages,covXML);			

				//function call to update default field
				dsCoverages=this.OverwriteCoverageDefaultValue(dsCoverages,covXML);			
			}

			if ( mandXML != "" )
			{
				//function call to update mandatory field
				dsCoverages=this.UpdateCoverageMandatory(dsCoverages,mandXML);			
			}

			return dsCoverages;             
		}
		

		#endregion


		#region "Application Homeowners"
		/// <summary>
		/// 
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <param name="strOldXML"></param>
		/// <param name="CoverageType"></param>
		/// <param name="strCustomInfo"></param>
		/// <param name="objWrapper"></param>
		/// <returns></returns>
		/// 
		public int SaveHomeCoveragesNew(ArrayList alNewCoverages,string strOldXML,string strCustomInfo,DataWrapper objWrapper)
		{
			return SaveHomeCoveragesNew(alNewCoverages,strOldXML,strCustomInfo,objWrapper,0);
		}
		public int SaveHomeCoveragesNew(ArrayList alNewCoverages,string strOldXML,string strCustomInfo,DataWrapper objWrapper, int LOB_ID)
		{
			string	strStoredProc =	"Proc_SAVE_HOME_COVERAGES";
			SqlCommand cmdCoverage = new SqlCommand();
			cmdCoverage.CommandText = strStoredProc;
			cmdCoverage.CommandType = CommandType.StoredProcedure;
			int customerID=0;
			int appID=0;
			int appVersionID=0;
			int dwellingID=0;
			int retVal=1;
			string CoverageType="";
			
			SqlParameter[] param = new SqlParameter[16];
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
			
			int UserID=0;

			
			//string strCustomInfo="Following coverages have been deleted:",str="";
			//strCustomInfo="";
			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Application.ClsCoveragesInfo objNew = (ClsCoveragesInfo)alNewCoverages[i];
					customerID = objNew.CUSTOMER_ID;
					appID = objNew.APP_ID;
					appVersionID = objNew.APP_VERSION_ID;
					UserID = objNew.MODIFIED_BY;
					dwellingID=objNew.RISK_ID;
					CoverageType=objNew.COVERAGE_TYPE;

					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
					if(objNew.RISK_ID==0)
					{
						objWrapper.AddParameter("@DWELLING_ID",System.DBNull.Value);
					}
					else
					{
						objWrapper.AddParameter("@DWELLING_ID",objNew.RISK_ID );
					}
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
					objWrapper.AddParameter("@LIMIT_1_TYPE",objNew.LIMIT_1_TYPE);
					objWrapper.AddParameter("@LIMIT_2_TYPE",objNew.LIMIT_2_TYPE);
					objWrapper.AddParameter("@COVERAGE_CODE",objNew.COVERAGE_CODE);
					
					
					objWrapper.AddParameter("@DEDUCTIBLE_1",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
					
					
					objWrapper.AddParameter("@DEDUCTIBLE_2",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_2));
					
					
					objWrapper.AddParameter("@LIMIT_1",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
					
					
					objWrapper.AddParameter("@LIMIT_2",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_2));
					
					objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE",objNew.DEDUCTIBLE_1_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE",objNew.DEDUCTIBLE_2_TYPE);
					objWrapper.AddParameter("@WRITTEN_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
					objWrapper.AddParameter("@FULL_TERM_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
					objWrapper.AddParameter("@COVERAGE_TYPE",objNew.COVERAGE_TYPE);
					
					objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",objNew.LIMIT1_AMOUNT_TEXT);
					objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",objNew.LIMIT2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",objNew.DEDUCTIBLE1_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",objNew.DEDUCTIBLE2_AMOUNT_TEXT);
					objWrapper.AddParameter("@LIMIT_ID",DefaultValues.GetIntNullFromNegative(objNew.LIMIT_ID));
					objWrapper.AddParameter("@DEDUC_ID",DefaultValues.GetIntNullFromNegative(objNew.DEDUC_ID));
					objWrapper.AddParameter("@DEDUCTIBLE_AMOUNT",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE));
					objWrapper.AddParameter("@DEDUCTIBLE_TEXT",objNew.DEDUCTIBLE_TEXT);
					objWrapper.AddParameter("@ADDDEDUCTIBLE_ID",objNew.ADDDEDUCTIBLE_ID);
					string strTranXML = "";
					
					//Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					//if ( objNew.COVERAGE_ID == -1 )
					if(objNew.ACTION == "I" || objNew.COVERAGE_ID == -1)
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/homeowners/Coverages_Section1.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						/*objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Home coverages added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;*/
						sbTranXML.Append(strTranXML);						
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();

					}
					else if( (objNew.ACTION =="U" || objNew.COVERAGE_ID != -1) && objNew.ACTION != "D" )
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/homeowners/Coverages_Section1.aspx.resx");
				
						strTranXML = this.GetTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);
						
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();
					}
				
				
					/////////////////////////////////////////////
					if(objNew.ACTION=="D")
					{
						objWrapper.ClearParameteres();
						//str+=";" + objNew.COV_DESC;
						objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
						objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
						objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
						objWrapper.AddParameter("@DWELLING_ID",objNew.RISK_ID);					
						objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);						
						objWrapper.ExecuteNonQuery("Proc_DeleteAPP_HOME_COVERAGES");				
						objWrapper.ClearParameteres();
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/homeowners/Coverages_Section1.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						string strTransXML = objBuilder.GetDeleteTransactionLogXML(objNew);
						sbTranXML.Append(strTransXML);
						
					}
				}

				//Update dependent Coverages////////////////
				objWrapper.ClearParameteres();

				//Get The Lob Id
				ClsGeneralInformation obj=new ClsGeneralInformation();
				string strlob="";
				strlob=obj.Fun_GetLObID(customerID,appID,appVersionID);
                //Update Endorsments
				this.UpdateAppCoveragesAndEndorsements(customerID,appID,appVersionID, dwellingID,CoverageType, objWrapper);
				//////////////

				if(strlob == ((int)enumLOB.HOME).ToString())
				{
					
					//Added By Ravindra(07-19-2006)
					//Update Watercraft Coverages For Boat attached to this Policy
					if(CoverageType == "S2")
					{
						objWrapper.ClearParameteres();
						ClsWatercraftCoverages objWatCov = new ClsWatercraftCoverages();
						objWatCov.UpdateCoveragesByRuleApp(objWrapper,customerID,appID,appVersionID,RuleType.LobDependent);
						objWrapper.ClearParameteres();
					}
				}
					//
			
				sbTranXML.Append("</root>");

				//if(sbTranXML.ToString()!="<root></root>")
				//	strCustomInfo+=";Following coverages have been added/updated";

				if(sbTranXML.ToString()!="<root></root>")// || strCustomInfo!="")
				{
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = appID;
					objTransactionInfo.APP_VERSION_ID = appVersionID;
					objTransactionInfo.CLIENT_ID = customerID;
					objTransactionInfo.RECORDED_BY = UserID;
					if(LOB_ID.ToString()==((int)enumLOB.REDW).ToString())
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1731", "");//"Rental coverages updated.";
					else
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1732", "");// "Homeowner coverages updated.";
					if(sbTranXML.ToString()!="<root></root>")
						objTransactionInfo.CHANGE_XML		=	sbTranXML.ToString();

					objTransactionInfo.CUSTOM_INFO=strCustomInfo;
				
					objWrapper.ClearParameteres();

					objWrapper.ExecuteNonQuery(objTransactionInfo);
				}
			}
			catch(Exception ex)
			{
				    retVal=-1;
					throw(ex);
				    
			} 
			return retVal;


		}
		
		/// <summary>
		/// Saves Dwelling coverages in the database
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <param name="strOldXML"></param>
		/// <param name="CoverageType"></param>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="dwellingID"></param>
		/// <param name="strCustomInfo"></param>
		/// <returns></returns>
		public int SaveHomeCoveragesNew(ArrayList alNewCoverages,string strOldXML,string CoverageType, int customerID, int appID, int appVersionID, int dwellingID, string strCustomInfo)
		{
			return SaveHomeCoveragesNew(alNewCoverages,strOldXML,CoverageType, customerID, appID, appVersionID, dwellingID, strCustomInfo, 0);
		}
		public int SaveHomeCoveragesNew(ArrayList alNewCoverages,string strOldXML,string CoverageType, int customerID, int appID, int appVersionID, int dwellingID, string strCustomInfo, int LOB_ID)
		{
			
			
            int RetVal=0;
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			try
			{
				RetVal = SaveHomeCoveragesNew(alNewCoverages,strOldXML, strCustomInfo, objWrapper, LOB_ID);

				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
			catch(Exception objExp)
			{
				throw(objExp);
			}
			finally
			{
				objWrapper.Dispose();
			}

			return RetVal;
			
			
		}

        //SNEHA SAVE
        public DataSet GetBopliabilityCoverage(int customerID, int appID, int appVersionID,
      string COVERAGETYPE)
        {
            //fetching dataset with all coverages
            DataSet dsCoverages = null;
            dsCoverages = this.GetBopliabilityCoverages(customerID,
                appID,
                appVersionID,
                COVERAGETYPE
                );

              return dsCoverages;
        }
        public DataSet GetBopliabilityCoverages(int customerID, int appID,
       int appVersionID, string COVERAGETYPE)
        {
            string strStoredProc = "Proc_GetBOP_LIABILITY_COVERAGES";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@APP_ID", appID);
            objWrapper.AddParameter("@APP_VERSION_ID", appVersionID);

            objWrapper.AddParameter("@COVERAGE_TYPE", COVERAGETYPE);
            //objWrapper.AddParameter("@CURRENT_PAGE_INDEX",currentPageIndex);

            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

            return ds;

        }

        public int SaveBOPGeneralNew(ArrayList alNewCoverages, string strOldXML, string CoverageType, int customerID, int POLICY_ID, int POLICY_VERSION_ID)
        {
            return SaveBOPGeneralNew(alNewCoverages,strOldXML, CoverageType, customerID, POLICY_ID, POLICY_VERSION_ID,0);
        }
        public int SaveBOPGeneralNew(ArrayList alNewCoverages, string strOldXML, string CoverageType, int customerID, int POLICY_ID, int POLICY_VERSION_ID, int LOB_ID)
        {


            int RetVal = 0;
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                RetVal = SaveBOPGeneralNew(alNewCoverages,strOldXML, objWrapper);

                objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            }
            catch (Exception objExp)
            {
                throw (objExp);
            }
            finally
            {
                objWrapper.Dispose();
            }

            return RetVal;


        }
        public int SaveBOPGeneralNew(ArrayList alNewCoverages,string strOldXML, DataWrapper objWrapper)
        {
            string strStoredProc = "PROC_SAVE_POL_BOP_GENERAL_COVERAGE";

           // DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            SqlCommand cmdCoverage = new SqlCommand();
            cmdCoverage.CommandText = strStoredProc;
            cmdCoverage.CommandType = CommandType.StoredProcedure;


            SqlParameter[] param = new SqlParameter[16];
            XmlElement root = null;
            XmlDocument xmlDoc = new XmlDocument();

            StringBuilder sbTranXML = new StringBuilder();

            sbTranXML.Append("<root>");

            if (strOldXML != "")
            {
                //strOldXML = ReplaceXMLCharacters(strOldXML);
                xmlDoc.LoadXml(strOldXML);
                root = xmlDoc.DocumentElement; //holds the root of the transaction XML
            }
            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

            int customerID = 0;
            int policyID = 0;
            int policyVersionID = 0;
            int retVal = 1;
            int UserID=0;
            string CoverageType = "";


     

          
            try
            {
                for (int i = 0; i < alNewCoverages.Count; i++)
                {
                    Cms.Model.Policy.ClsLiabilityCoverage objNew = (ClsLiabilityCoverage)alNewCoverages[i];
                    customerID = objNew.CUSTOMER_ID;
                    policyID = objNew.POLICY_ID;
                    policyVersionID = objNew.POLICY_VERSION_ID;
                    UserID = objNew.MODIFIED_BY;
                    CoverageType ="PL";

                    objWrapper.ClearParameteres();
                    objWrapper.AddParameter("@CUSTOMER_ID", objNew.CUSTOMER_ID);
                    objWrapper.AddParameter("@POLICY_ID", objNew.POLICY_ID);
                    objWrapper.AddParameter("@POLICY_VERSION_ID", objNew.POLICY_VERSION_ID);
                   
                    objWrapper.AddParameter("@COVERAGE_ID", objNew.COVERAGE_ID);
                    objWrapper.AddParameter("@COVERAGE_CODE_ID", objNew.COVERAGE_CODE_ID);
                    objWrapper.AddParameter("@RI_APPLIES", objNew.RI_APPLIES);
                    objWrapper.AddParameter("@LIMIT_OVERRIDE", objNew.LIMIT_OVERRIDE);
                    objWrapper.AddParameter("@LIMIT_1", DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
                    objWrapper.AddParameter("@LIMIT_1_TYPE", objNew.LIMIT_1_TYPE);
                    objWrapper.AddParameter("@LIMIT_2", (objNew.LIMIT_2));
                    objWrapper.AddParameter("@LIMIT_2_TYPE", objNew.LIMIT_2_TYPE);
                    objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT", objNew.LIMIT1_AMOUNT_TEXT);
                    objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT", objNew.LIMIT2_AMOUNT_TEXT);
                    objWrapper.AddParameter("@DEDUCT_OVERRIDE", objNew.DEDUCT_OVERRIDE);
                    objWrapper.AddParameter("@DEDUCTIBLE_1", DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
                    objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE", objNew.DEDUCTIBLE_1_TYPE);
                    objWrapper.AddParameter("@DEDUCTIBLE_2",(objNew.DEDUCTIBLE_2));
                    objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE", objNew.DEDUCTIBLE_2_TYPE);
                    objWrapper.AddParameter("@MINIMUM_DEDUCTIBLE", objNew.MINIMUM_DEDUCTIBLE);
                    objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT", objNew.DEDUCTIBLE1_AMOUNT_TEXT);
                    objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT", objNew.DEDUCTIBLE2_AMOUNT_TEXT);
                    objWrapper.AddParameter("@DEDUCTIBLE_REDUCES", objNew.DEDUCTIBLE_REDUCES);
                    objWrapper.AddParameter("@INITIAL_RATE", objNew.INITIAL_RATE);
                    objWrapper.AddParameter("@FINAL_RATE", DefaultValues.GetDoubleNullFromNegative(objNew.FINAL_RATE));
                    objWrapper.AddParameter("@AVERAGE_RATE", objNew.AVERAGE_RATE);
                    objWrapper.AddParameter("@WRITTEN_PREMIUM", DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
                    objWrapper.AddParameter("@FULL_TERM_PREMIUM", DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
                    objWrapper.AddParameter("@LIMIT_ID", DefaultValues.GetIntNullFromNegative(objNew.LIMIT_ID));
                    objWrapper.AddParameter("@DEDUC_ID", DefaultValues.GetIntNullFromNegative(objNew.DEDUC_ID));

                    objWrapper.AddParameter("@IS_SYSTEM_COVERAGE", objNew.IS_SYSTEM_COVERAGE);
                    objWrapper.AddParameter("@ADD_INFORMATION", objNew.ADD_INFORMATION);
                    objWrapper.AddParameter("@CREATED_BY", objNew.CREATED_BY);
                    
                    objWrapper.AddParameter("@MODIFIED_BY", objNew.MODIFIED_BY);
                   
                    objWrapper.AddParameter("@INDEMNITY_PERIOD", objNew.INDEMNITY_PERIOD);
                    objWrapper.AddParameter("@CHANGE_INFORCE_PREM", objNew.CHANGE_INFORCE_PREM);
                    objWrapper.AddParameter("@IS_ACTIVE", objNew.IS_ACTIVE);
                    objWrapper.AddParameter("@ACC_CO_DISCOUNT", objNew.ACC_CO_DISCOUNT);

                   

                  


                    if (objNew.CREATED_DATETIME != DateTime.MinValue)
                        objWrapper.AddParameter("@CREATED_DATETIME", objNew.CREATED_DATETIME);
                    else
                        objWrapper.AddParameter("@CREATED_DATETIME", System.DBNull.Value);
                    

                    if (objNew.LAST_UPDATED_DATETIME != DateTime.MinValue)
                        objWrapper.AddParameter("@LAST_UPDATED_DATETIME", objNew.LAST_UPDATED_DATETIME);
                    else
                        objWrapper.AddParameter("@LAST_UPDATED_DATETIME", System.DBNull.Value);

                   
                 

                    string strTranXML = "";

                    objTransactionInfo.RECORDED_BY = objNew.CREATED_BY;

                    if (objNew.ACTION == "I")
                    {
                        //Insert
                        objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/AddPolicyCoverages.aspx.resx");
                        SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                        strTranXML = objBuilder.GetTransactionLogXML(objNew);

                        objTransactionInfo.TRANS_TYPE_ID = 193;
                        objTransactionInfo.POLICY_ID = objNew.POLICY_ID;
                        objTransactionInfo.POLICY_VER_TRACKING_ID = objNew.POLICY_VERSION_ID;
                        objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
                        //objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
                        objTransactionInfo.TRANS_DESC = "Liability Coverage added.";
                        objTransactionInfo.CHANGE_XML = strTranXML;

                        sbTranXML.Append(strTranXML);
                        //objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                        objWrapper.ExecuteNonQuery(strStoredProc);
                        objWrapper.ClearParameteres();

                    }
                    else if (objNew.ACTION == "U")
                    {
                        objTransactionInfo.TRANS_TYPE_ID = 193;
                        //Update	
                        objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/addPolicyCoverages.aspx.resx");
                        objTransactionInfo.TRANS_DESC = "Product coverage updated.";
                        //strTranXML = this.GetPolicyHomeTranXML(objNew, strOldXML, objNew.COVERAGE_ID, root);
                        if (strTranXML != "<LabelFieldMapping></LabelFieldMapping>")
                            sbTranXML.Append(strTranXML);
                        //objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                        objWrapper.ExecuteNonQuery(strStoredProc);
                        objWrapper.ClearParameteres();
                    }

                }
                objWrapper.ClearParameteres();
                //Delete Coverages/////////////////////////////////////
                //strCustomInfo = "";
                for (int i = 0; i < alNewCoverages.Count; i++)
                {

                    Cms.Model.Policy.ClsLiabilityCoverage objDelete = (Cms.Model.Policy.ClsLiabilityCoverage)alNewCoverages[i];

                    if (objDelete.ACTION == "D")
                    {
                        objTransactionInfo.TRANS_TYPE_ID = 193;
                        objWrapper.AddParameter("@CUSTOMER_ID", objDelete.CUSTOMER_ID);
                        objWrapper.AddParameter("@POLICY_ID", objDelete.POLICY_ID);
                        objWrapper.AddParameter("@POLICY_VERSION_ID", objDelete.POLICY_VERSION_ID);
                        
                        objWrapper.AddParameter("@COVERAGE_ID", objDelete.COVERAGE_ID);
                        //Delete the coverage
                        //objWrapper.ExecuteNonQuery("Proc_Delete_POL_PRODUCT_COVERAGES");
                        //Get Tran log
                        objDelete.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/AddPolicyCoverages.aspx.resx");
                        SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                        //string strTranXML = objBuilder.GetTransactionLogXML(objDelete);
                        string strTranXML = objBuilder.GetDeleteTransactionLogXML(objDelete);

                        sbTranXML.Append(strTranXML);

                        objWrapper.ClearParameteres();
                    }
                }
                sbTranXML.Append("</root>");

                //if(sbTranXML.ToString()!="<root></root>")
                //	strCustomInfo+=";Following coverages have been added/updated";

                if (sbTranXML.ToString() != "<root></root>")// || strCustomInfo!="")
                {

                    objTransactionInfo.POLICY_ID = policyID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = policyVersionID;
                    objTransactionInfo.CLIENT_ID = customerID;

                    objTransactionInfo.TRANS_DESC = "";
                    if (sbTranXML.ToString() != "<root></root>")
                        objTransactionInfo.CHANGE_XML = sbTranXML.ToString();

                   // objTransactionInfo.CUSTOM_INFO = strCustomInfo;

                    objWrapper.ClearParameteres();

                    objWrapper.ExecuteNonQuery(objTransactionInfo);
                }
                /*	//Update Policy Coverages///////////////
                    if(alNewCoverages.Count>0)
                    {
                        UpdatePolicyCoverages(alNewCoverages,objWrapper,vehicleID,customerID,policyID,policyVersionID);
                        /////////////////////////////////////////
                        //Update relevant endorsements from coverages/////////
                        UpdateEndorsmentPolicy(objWrapper,customerID,policyID,policyVersionID,vehicleID);
                        //UpdatePolicyEndorsements(customerID,policyID,policyVersionID,vehicleID,objWrapper);
                    }
                    /////////////////////
                    */

            }
            catch (Exception ex)
            {
                //tran.Rollback();
                //conn.Close();
                objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                if (ex.InnerException != null)
                {
                    string message = ex.InnerException.Message.ToLower();


                    if (message.StartsWith("violation of primary key"))
                    {
                        return -2;
                    }

                }

                throw (ex);
            }
            objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            return 1;
        }



        public int SaveBOPLiabilitylNew(ArrayList alNewCoverages, string strOldXML, string CoverageType, int customerID, int POLICY_ID, int POLICY_VERSION_ID,int LOCATION_ID, int PREMISES_ID)
        {
            return SaveBOPLiabilitylNew(alNewCoverages, strOldXML, CoverageType, customerID, POLICY_ID, POLICY_VERSION_ID,LOCATION_ID,PREMISES_ID, 0);
        }
        public int SaveBOPLiabilitylNew(ArrayList alNewCoverages, string strOldXML, string CoverageType, int customerID, int POLICY_ID, int POLICY_VERSION_ID, int LOCATION_ID, int PREMISES_ID, int LOB_ID)
        {


            int RetVal = 0;
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                RetVal = SaveBOPLiabilitylNew(alNewCoverages, strOldXML, objWrapper);

                objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            }
            catch (Exception objExp)
            {
                throw (objExp);
            }
            finally
            {
                objWrapper.Dispose();
            }

            return RetVal;


        }
        public int SaveBOPLiabilitylNew(ArrayList alNewCoverages, string strOldXML, DataWrapper objWrapper)
        {
            string strStoredProc = "PROC_SAVE_POL_BOP_LIABILITY_COVERAGE";

            // DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            SqlCommand cmdCoverage = new SqlCommand();
            cmdCoverage.CommandText = strStoredProc;
            cmdCoverage.CommandType = CommandType.StoredProcedure;


            SqlParameter[] param = new SqlParameter[16];
            XmlElement root = null;
            XmlDocument xmlDoc = new XmlDocument();

            StringBuilder sbTranXML = new StringBuilder();

            sbTranXML.Append("<root>");

            if (strOldXML != "")
            {
                //strOldXML = ReplaceXMLCharacters(strOldXML);
                xmlDoc.LoadXml(strOldXML);
                root = xmlDoc.DocumentElement; //holds the root of the transaction XML
            }
            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

            int customerID = 0;
            int policyID = 0;
            int policyVersionID = 0;
            int retVal = 1;
            int UserID = 0;
            int LocationId = 0;
            int PremisesId = 0;
             string CoverageType = "";




            try
            {
                for (int i = 0; i < alNewCoverages.Count; i++)
                {
                    Cms.Model.Policy.ClsPropertyCoveragesInfo objNew = (ClsPropertyCoveragesInfo)alNewCoverages[i];
                    customerID = objNew.CUSTOMER_ID;
                    policyID = objNew.POLICY_ID;
                    policyVersionID = objNew.POLICY_VERSION_ID;
                    UserID = objNew.MODIFIED_BY;
                    LocationId = objNew.LOCATION_ID;
                    PremisesId = objNew.PREMISES_ID;
                    CoverageType = "RL";
                    objWrapper.ClearParameteres();
                    objWrapper.AddParameter("@CUSTOMER_ID", objNew.CUSTOMER_ID);
                    objWrapper.AddParameter("@POLICY_ID", objNew.POLICY_ID);
                    objWrapper.AddParameter("@POLICY_VERSION_ID", objNew.POLICY_VERSION_ID);
                    objWrapper.AddParameter("@LOCATION_ID", objNew.LOCATION_ID);
                    objWrapper.AddParameter("@PREMISES_ID", objNew.PREMISES_ID);
                    objWrapper.AddParameter("@COVERAGE_ID", objNew.COVERAGE_ID);
                    objWrapper.AddParameter("@COVERAGE_CODE_ID", objNew.COVERAGE_CODE_ID);
                    objWrapper.AddParameter("@RI_APPLIES", objNew.RI_APPLIES);
                    objWrapper.AddParameter("@LIMIT_OVERRIDE", objNew.LIMIT_OVERRIDE);
                    objWrapper.AddParameter("@LIMIT_1", DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
                    objWrapper.AddParameter("@LIMIT_1_TYPE", objNew.LIMIT_1_TYPE);
                    objWrapper.AddParameter("@LIMIT_2", (objNew.LIMIT_2));
                    objWrapper.AddParameter("@LIMIT_2_TYPE", objNew.LIMIT_2_TYPE);
                    objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT", objNew.LIMIT1_AMOUNT_TEXT);
                    objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT", objNew.LIMIT2_AMOUNT_TEXT);
                    objWrapper.AddParameter("@DEDUCT_OVERRIDE", objNew.DEDUCT_OVERRIDE);
                    objWrapper.AddParameter("@DEDUCTIBLE_1", DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
                    objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE", objNew.DEDUCTIBLE_1_TYPE);
                    objWrapper.AddParameter("@DEDUCTIBLE_2", (objNew.DEDUCTIBLE_2));
                    objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE", objNew.DEDUCTIBLE_2_TYPE);
                    objWrapper.AddParameter("@MINIMUM_DEDUCTIBLE", objNew.MINIMUM_DEDUCTIBLE);
                    objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT", objNew.DEDUCTIBLE1_AMOUNT_TEXT);
                    objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT", objNew.DEDUCTIBLE2_AMOUNT_TEXT);
                    objWrapper.AddParameter("@DEDUCTIBLE_REDUCES", objNew.DEDUCTIBLE_REDUCES);
                    objWrapper.AddParameter("@INITIAL_RATE", objNew.INITIAL_RATE);
                    objWrapper.AddParameter("@FINAL_RATE", DefaultValues.GetDoubleNullFromNegative(objNew.FINAL_RATE));
                    objWrapper.AddParameter("@AVERAGE_RATE", objNew.AVERAGE_RATE);
                    objWrapper.AddParameter("@WRITTEN_PREMIUM", DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
                    objWrapper.AddParameter("@FULL_TERM_PREMIUM", DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
                    objWrapper.AddParameter("@LIMIT_ID", DefaultValues.GetIntNullFromNegative(objNew.LIMIT_ID));
                    objWrapper.AddParameter("@DEDUC_ID", DefaultValues.GetIntNullFromNegative(objNew.DEDUC_ID));

                    objWrapper.AddParameter("@IS_SYSTEM_COVERAGE", objNew.IS_SYSTEM_COVERAGE);
                    objWrapper.AddParameter("@ADD_INFORMATION", objNew.ADD_INFORMATION);
                    objWrapper.AddParameter("@CREATED_BY", objNew.CREATED_BY);

                    objWrapper.AddParameter("@MODIFIED_BY", objNew.MODIFIED_BY);

                    objWrapper.AddParameter("@INDEMNITY_PERIOD", objNew.INDEMNITY_PERIOD);
                    objWrapper.AddParameter("@CHANGE_INFORCE_PREM", objNew.CHANGE_INFORCE_PREM);
                    objWrapper.AddParameter("@IS_ACTIVE", objNew.IS_ACTIVE);
                    objWrapper.AddParameter("@ACC_CO_DISCOUNT", objNew.ACC_CO_DISCOUNT);
                    objWrapper.AddParameter("@COV_TYPE", objNew.COV_TYPE);





                    if (objNew.CREATED_DATETIME != DateTime.MinValue)
                        objWrapper.AddParameter("@CREATED_DATETIME", objNew.CREATED_DATETIME);
                    else
                        objWrapper.AddParameter("@CREATED_DATETIME", System.DBNull.Value);


                    if (objNew.LAST_UPDATED_DATETIME != DateTime.MinValue)
                        objWrapper.AddParameter("@LAST_UPDATED_DATETIME", objNew.LAST_UPDATED_DATETIME);
                    else
                        objWrapper.AddParameter("@LAST_UPDATED_DATETIME", System.DBNull.Value);




                    string strTranXML = "";

                    objTransactionInfo.RECORDED_BY = objNew.CREATED_BY;

                    if (objNew.ACTION == "I")
                    {
                        //Insert
                        objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/AddPolicyCoverages.aspx.resx");
                        SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                        strTranXML = objBuilder.GetTransactionLogXML(objNew);

                        objTransactionInfo.TRANS_TYPE_ID = 193;
                        objTransactionInfo.POLICY_ID = objNew.POLICY_ID;
                        objTransactionInfo.POLICY_VER_TRACKING_ID = objNew.POLICY_VERSION_ID;
                        objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
                        //objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
                        objTransactionInfo.TRANS_DESC = "Liability Coverage added.";
                        objTransactionInfo.CHANGE_XML = strTranXML;

                        sbTranXML.Append(strTranXML);
                        //objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                        objWrapper.ExecuteNonQuery(strStoredProc);
                        objWrapper.ClearParameteres();

                    }
                    else if (objNew.ACTION == "U")
                    {
                        objTransactionInfo.TRANS_TYPE_ID = 193;
                        //Update	
                        objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/addPolicyCoverages.aspx.resx");
                        objTransactionInfo.TRANS_DESC = "Product coverage updated.";
                        //strTranXML = this.GetPolicyHomeTranXML(objNew, strOldXML, objNew.COVERAGE_ID, root);
                        if (strTranXML != "<LabelFieldMapping></LabelFieldMapping>")
                            sbTranXML.Append(strTranXML);
                        //objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                        objWrapper.ExecuteNonQuery(strStoredProc);
                        objWrapper.ClearParameteres();
                    }

                }
                objWrapper.ClearParameteres();
                //Delete Coverages/////////////////////////////////////
                //strCustomInfo = "";
                for (int i = 0; i < alNewCoverages.Count; i++)
                {

                    Cms.Model.Policy.ClsPropertyCoveragesInfo objDelete = (Cms.Model.Policy.ClsPropertyCoveragesInfo)alNewCoverages[i];

                    if (objDelete.ACTION == "D")
                    {
                        objTransactionInfo.TRANS_TYPE_ID = 193;
                        objWrapper.AddParameter("@CUSTOMER_ID", objDelete.CUSTOMER_ID);
                        objWrapper.AddParameter("@POLICY_ID", objDelete.POLICY_ID);
                        objWrapper.AddParameter("@POLICY_VERSION_ID", objDelete.POLICY_VERSION_ID);

                        objWrapper.AddParameter("@COVERAGE_ID", objDelete.COVERAGE_ID);
                        //Delete the coverage
                        //objWrapper.ExecuteNonQuery("Proc_Delete_POL_PRODUCT_COVERAGES");
                        //Get Tran log
                        objDelete.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/AddPolicyCoverages.aspx.resx");
                        SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                        //string strTranXML = objBuilder.GetTransactionLogXML(objDelete);
                        string strTranXML = objBuilder.GetDeleteTransactionLogXML(objDelete);

                        sbTranXML.Append(strTranXML);

                        objWrapper.ClearParameteres();
                    }
                }
                sbTranXML.Append("</root>");

             

                if (sbTranXML.ToString() != "<root></root>")// || strCustomInfo!="")
                {

                    objTransactionInfo.POLICY_ID = policyID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = policyVersionID;
                    objTransactionInfo.CLIENT_ID = customerID;

                    objTransactionInfo.TRANS_DESC = "";
                    if (sbTranXML.ToString() != "<root></root>")
                        objTransactionInfo.CHANGE_XML = sbTranXML.ToString();

                   

                    objWrapper.ClearParameteres();

                    objWrapper.ExecuteNonQuery(objTransactionInfo);
                }
          

            }
            catch (Exception ex)
            {
               
                objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                if (ex.InnerException != null)
                {
                    string message = ex.InnerException.Message.ToLower();


                    if (message.StartsWith("violation of primary key"))
                    {
                        return -2;
                    }

                }

                throw (ex);
            }
            objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            return 1;
        }


        public DataSet GetLocatioDetailCoverage(int customerID, int appID, int appVersionID)
        {
            
            DataSet dsCoverages = null;
            dsCoverages = this.GetLocatioDetailCoverages(customerID,
                appID,
                appVersionID);

           return dsCoverages;
        }

        public DataSet GetLocatioDetailCoverages(int customerID, int appID,int appVersionID)
        {
            ClsPropertyCoveragesInfo objnew = new ClsPropertyCoveragesInfo();
            string strStoredProc = "GETLOCATION_PREMISES";
            int APP_ID = 0;
            int returnResult = 0;
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@APP_ID", appID);
            objWrapper.AddParameter("@APP_VERSION_ID", appVersionID);

            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

            return ds;

            

        }

        //END
		
		/// <summary>
		/// Gets the master data set of home coverages
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="dwellingID"></param>
		/// <param name="appType"></param>
		/// <param name="COVERAGETYPE"></param>
		/// <returns></returns>
		public  DataSet GetDwellingSection1Coverages(int customerID, int appID, 
			int appVersionID, int dwellingID, string appType,string COVERAGETYPE)
		{
			string	strStoredProc =	"Proc_GetAPP_DWELLING_COVERAGES_SECTION1";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@DWELLING_ID",dwellingID);
			objWrapper.AddParameter("@APP_TYPE",appType);
			objWrapper.AddParameter("@COVERAGE_TYPE",COVERAGETYPE);
			//objWrapper.AddParameter("@CURRENT_PAGE_INDEX",currentPageIndex);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		
		}

        public DataSet GetBopGeneralCoverages(int customerID, int appID,
        int appVersionID, string COVERAGETYPE)
        {
            string strStoredProc = "Proc_GetBOP_GENERAL_COVERAGES";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@APP_ID", appID);
            objWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
           
            objWrapper.AddParameter("@COVERAGE_TYPE", COVERAGETYPE);
            //objWrapper.AddParameter("@CURRENT_PAGE_INDEX",currentPageIndex);

            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

            return ds;

        }

		/// <summary>
		///Filters the Home coverages according to business rules 
		/// </summary>
		/// <returns>dataset after filteration of records (coverages) that are no longer required </returns>
		public DataSet GetHomeCoverages(int customerID, int appID, int appVersionID, 
			int dwellingID, string appType,string COVERAGETYPE)
		{
			//fetching dataset with all coverages
			DataSet dsCoverages=null;
			dsCoverages = this.GetDwellingSection1Coverages(customerID,
				appID,
				appVersionID,
				dwellingID,appType,COVERAGETYPE
				);	

			//fetching XML string with all coverages to remove
			
			string covXML=this.GetHomeCoveragesToRemove(customerID,
				appID,
				appVersionID,
				dwellingID,dsCoverages
				);	
			

			string mandXML = this.GetHomeMandatoryCoverages(customerID,
				appID,
				appVersionID,
				dwellingID,dsCoverages);
		
		
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

			if ( mandXML != "" )
			{
				//function call to update mandatory field
				dsCoverages=this.UpdateCoverageMandatory(dsCoverages,mandXML);		
			}



			return dsCoverages;             
		}


        public DataSet GetBopGeneralCoverage(int customerID, int appID, int appVersionID,
        string COVERAGETYPE)
        {
            //fetching dataset with all coverages
            DataSet dsCoverages = null;
            dsCoverages = this.GetBopGeneralCoverages(customerID,
                appID,
                appVersionID,
                COVERAGETYPE
                );

            //fetching XML string with all coverages to remove

            //string covXML = this.GetHomeCoveragesToRemove(customerID,
            //    appID,
            //    appVersionID,
            //     dsCoverages
            //    );


            //string mandXML = this.GetHomeMandatoryCoverages(customerID,
            //    appID,
            //    appVersionID,
            //    dwellingID, dsCoverages);


            ////if XML string is not blank		
            //if (covXML != "")
            //{
            //    //function call to delete coverage
            //    dsCoverages = this.DeleteCoverage(dsCoverages, covXML);

            //    //function call to delete coverage limits
            //    dsCoverages = this.DeleteCoverageOptions(dsCoverages, covXML);

            //    //function call to update default field
            //    dsCoverages = this.OverwriteCoverageDefaultValue(dsCoverages, covXML);
            //}

            //if (mandXML != "")
            //{
            //    //function call to update mandatory field
            //    dsCoverages = this.UpdateCoverageMandatory(dsCoverages, mandXML);
            //}



            return dsCoverages;
        }
		#endregion

		#region HOMEOWNERS COVERAGE
		
		/// <summary>
		/// Returns an XML with coverages to remove read from XML file
		/// </summary>
		/// <param name="stateID"></param>
		/// <param name="product"></param>
		/// <returns></returns>
		public string GetHomeCoveragesToRemoveFromXML(int stateID, int product, DataSet objDataSet)
		{
			string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/Coverages/HomeCoverages.xml");
			StringBuilder sbXML = new StringBuilder();
			
			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtLimits = objDataSet.Tables[1];
			DataTable dtHome = objDataSet.Tables[2];
			DataTable dtCovLimits = objDataSet.Tables[3];

			if(HomeRemoveXMLLoaded == false)
			{
				homeRemoveXml.Load(filePath); 
				HomeRemoveXMLLoaded=true; 
			}
			
		
			XmlNode node = homeRemoveXml.SelectSingleNode("Root/State[@ID=" +  stateID.ToString() + "]");
	
			if ( node == null ) return "";

			XmlNode productNode = node.SelectSingleNode("Product[@ID=" + product.ToString() + "]");

			if ( productNode == null ) return "";

			XmlNode removeNode = productNode.SelectSingleNode("Remove/Coverages");

			if ( removeNode == null ) return "";

			XmlNodeList covList = removeNode.SelectNodes("Coverage");
			
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			//Loop thru each coveages to remove
			foreach(XmlNode remNode in covList)
			{
				string coverageID = remNode.Attributes["COV_ID"].Value;
				string covCode = remNode.Attributes["Code"].Value;
				
				if ( sb.ToString() == "" )
				{
					sb.Append("'" + covCode + "'");
				}
				else
				{
					sb.Append(",'" + covCode + "'");
				}

			}
			
			
			//Get the coverages to remove from the master dataset and create XML
			DataRow[] drRemove = dtCoverage.Select("COV_CODE IN (" + sb.ToString() + ")");
			
			foreach(DataRow dr1 in drRemove)
			{
				string covID = dr1["COV_ID"].ToString();
				string mand = dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
				string covCode = dr1["COV_CODE"].ToString();
				
				sbXML.Append("<Coverage COV_ID='" + covID + "' COV_CODE='" + covCode + "' Remove='Y' Mandatory='N'>");
				sbXML.Append("</Coverage>");
			}
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			return sbXML.ToString();
		}


		private static DataSet GetPolicyDwellingSection1Coverages(int customerID, int polID, 
			int polVersionID, int dwellingID, string polType,string COVERAGETYPE)
		{
			string	strStoredProc =	"Proc_GetPOL_DWELLING_COVERAGES_SECTION1";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",polID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);
			objWrapper.AddParameter("@DWELLING_ID",dwellingID);
			objWrapper.AddParameter("@POLICY_TYPE",polType);
			objWrapper.AddParameter("@COVERAGE_TYPE",COVERAGETYPE);
			//objWrapper.AddParameter("@CURRENT_PAGE_INDEX",currentPageIndex);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		
		}

		public int SaveHomeCoveragesNewForPolicy(ArrayList alNewCoverages,string strOldXML,string CoverageType, int customerID, int polID, int polVersionID, int dwellingID, string strCustomInfo)
		{
			 
			string	strStoredProc =	"Proc_SAVE_HOME_COVERAGES_FOR_POLICY";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			SqlCommand cmdCoverage = new SqlCommand();
			cmdCoverage.CommandText = strStoredProc;
			cmdCoverage.CommandType = CommandType.StoredProcedure;
			
			SqlParameter[] param = new SqlParameter[16];
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
			int UserID=0;
			

			
			//string strCustomInfo="Following coverages have been deleted:",str="";
			//strCustomInfo="";
			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					
					Cms.Model.Policy.ClsPolicyCoveragesInfo   objNew = (Cms.Model.Policy.ClsPolicyCoveragesInfo)alNewCoverages[i];
					customerID = objNew.CUSTOMER_ID;
					polID = objNew.POLICY_ID;
					polVersionID = objNew.POLICY_VERSION_ID;
					UserID = objNew.MODIFIED_BY;
					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@POLICY_ID",objNew.POLICY_ID);
					objWrapper.AddParameter("@POLICY_VERSION_ID",objNew.POLICY_VERSION_ID);
					if(objNew.RISK_ID==0)
					{
						objWrapper.AddParameter("@DWELLING_ID",System.DBNull.Value);
					}
					else
					{
						objWrapper.AddParameter("@DWELLING_ID",objNew.RISK_ID);
					}
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
					objWrapper.AddParameter("@LIMIT_1_TYPE",objNew.LIMIT_1_TYPE);
					objWrapper.AddParameter("@LIMIT_2_TYPE",objNew.LIMIT_2_TYPE);
					
					
					objWrapper.AddParameter("@DEDUCTIBLE_1",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
					objWrapper.AddParameter("@DEDUCTIBLE_2",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_2));
					
					objWrapper.AddParameter("@LIMIT_1",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));	
					objWrapper.AddParameter("@LIMIT_2",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_2));
					
					objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE",objNew.DEDUCTIBLE_1_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE",objNew.DEDUCTIBLE_2_TYPE);
					objWrapper.AddParameter("@WRITTEN_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
					objWrapper.AddParameter("@FULL_TERM_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
					objWrapper.AddParameter("@COVERAGE_TYPE",CoverageType);
					
					objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",objNew.LIMIT1_AMOUNT_TEXT);
					objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",objNew.LIMIT2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",objNew.DEDUCTIBLE1_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",objNew.DEDUCTIBLE2_AMOUNT_TEXT);
					objWrapper.AddParameter("@LIMIT_ID",DefaultValues.GetIntNullFromNegative(objNew.LIMIT_ID));
					objWrapper.AddParameter("@DEDUC_ID",DefaultValues.GetIntNullFromNegative(objNew.DEDUC_ID));
					objWrapper.AddParameter("@DEDUCTIBLE_AMOUNT",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE));
					objWrapper.AddParameter("@DEDUCTIBLE_TEXT",objNew.DEDUCTIBLE_TEXT);
					objWrapper.AddParameter("@ADDDEDUCTIBLE_ID",DefaultValues.GetIntNullFromNegative(objNew.ADDDEDUCTIBLE_ID));
					
					string strTranXML = "";
					
					//Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					//if ( objNew.COVERAGE_ID == -1 )
					if(objNew.ACTION == "I")
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/homeowner/PolicyCoverages_Section1.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						/*objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Home coverages added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;*/
						sbTranXML.Append(strTranXML);						
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();

					}
					else if ( objNew.ACTION == "U")
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/homeowner/PolicyCoverages_Section1.aspx.resx");
				
						strTranXML = GetPolicyTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);
						
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();
					}
				
					
					/////////////////////////////////////////////
					if(objNew.ACTION=="D")
					{
						objWrapper.ClearParameteres();
						//str+=";" + objNew.COV_DESC;
						objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
						objWrapper.AddParameter("@POLICY_ID",objNew.POLICY_ID);
						objWrapper.AddParameter("@POLICY_VERSION_ID",objNew.POLICY_VERSION_ID);
						objWrapper.AddParameter("@DWELLING_ID",objNew.RISK_ID);					
						objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);						
						objWrapper.ExecuteNonQuery("Proc_DeletePOL_HOME_COVERAGES");				
						objWrapper.ClearParameteres();
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/homeowner/PolicyCoverages_Section1.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						string strTransXML = objBuilder.GetDeleteTransactionLogXML(objNew);
						sbTranXML.Append(strTransXML);
						
					}
				}

				//Update dependent Coverages////////////////
				objWrapper.ClearParameteres();					
				this.UpdatePolicyCoveragesAndEndorsements(customerID, polID,polVersionID,dwellingID,CoverageType,objWrapper);
				//End of dependent

				//Added By Ravindra(07-19-2006)
				// Update Coverages For Boat Attached to this Policy 
				if(CoverageType == "S2")
				{
					objWrapper.ClearParameteres();
					ClsWatercraftCoverages objWatCov = new ClsWatercraftCoverages();
					objWatCov.UpdateCoveragesByRulePolicy(objWrapper,customerID,polID ,polVersionID,RuleType.LobDependent);
					objWrapper.ClearParameteres();
				}

				//
				sbTranXML.Append("</root>");

			
				if(sbTranXML.ToString()!="<root></root>")// || strCustomInfo!="")
				{
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.POLICY_ID =polID;
					objTransactionInfo.POLICY_VER_TRACKING_ID   = polVersionID;
					objTransactionInfo.CLIENT_ID = customerID;
					objTransactionInfo.RECORDED_BY = UserID;

                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1732", "");//"Homeowner coverages updated.";
					if(sbTranXML.ToString()!="<root></root>")
						objTransactionInfo.CHANGE_XML		=	sbTranXML.ToString();

					objTransactionInfo.CUSTOM_INFO=strCustomInfo;
				
					objWrapper.ClearParameteres();

					objWrapper.ExecuteNonQuery(objTransactionInfo);
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


		

		/// <summary>
		/// Applies business rule on Coverages
		/// </summary>
		/// <returns>dataset after filteration of records (coverages) that are no longer required </returns>
		public DataSet GetPolicyHomeCoverages(int customerID, int polID, int polVersionID, int dwellingID, string polType,string COVERAGETYPE)
		{
			//fetching dataset with all coverages
			DataSet dsCoverages=null;
			dsCoverages = GetPolicyDwellingSection1Coverages(customerID,
				polID,
				polVersionID,
				dwellingID,polType,COVERAGETYPE
				);	

			//fetching XML string with all coverages to remove
			
			//Modified By Shafi Two Functions doing same thing 
//			string covXML=this.GetPolicyHomeCoveragesToRemove(customerID,
//				polID,
//				polVersionID,
//				dwellingID,dsCoverages
//				);
			string covXML=this.GetHomeCoveragesToRemove(customerID,
				polID,
				polVersionID,
				dwellingID,dsCoverages
				);
	
	

			

			string mandXML = this.GetHomeMandatoryCoverages(customerID,
				polID,
				polVersionID,
				dwellingID,dsCoverages);

			/*Reading from the XML file and saving the text in string until the function start giving the XML string */
			
			/*TextReader tr=new StreamReader(System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath  + "/working/coveragedummyxml.xml"));
			string covXML=tr.ReadToEnd(); 
			tr.Close();*/
			
			 
			  

			//if XML string is not blank		
			if(covXML!="" )
			{
				Cms.BusinessLayer.BlApplication.clsapplication 	objCovInformation = new Cms.BusinessLayer.BlApplication.clsapplication();
				//function call to delete coverage
				dsCoverages=this.DeleteCoverage(dsCoverages,covXML);			

				//function call to delete coverage limits
				dsCoverages=this.DeleteCoverageOptions(dsCoverages,covXML);			

					

				//function call to update default field
				dsCoverages=this.OverwriteCoverageDefaultValue(dsCoverages,covXML);			
			}

			if ( mandXML != "" )
			{
				//function call to update mandatory field
				dsCoverages=this.UpdateCoverageMandatory(dsCoverages,mandXML);		
			}

			return dsCoverages;             
		}

		

		#endregion

		#region Home Section2 Coverages
		
		/// <summary>
		/// Gets sectiion 2 coverages from teh database
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="dwellingID"></param>
		/// <param name="appType"></param>
		/// <param name="COVERAGETYPE"></param>
		/// <returns></returns>
		public  DataSet GetDwellingSection2Coverages(int customerID, int appID, 
			int appVersionID, int dwellingID, string appType,string COVERAGETYPE)
		{
			string	strStoredProc =	"Proc_GetAPP_DWELLING_COVERAGES_SECTION2";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@DWELLING_ID",dwellingID);
			objWrapper.AddParameter("@APP_TYPE",appType);
			objWrapper.AddParameter("@COVERAGE_TYPE",COVERAGETYPE);
			//objWrapper.AddParameter("@CURRENT_PAGE_INDEX",currentPageIndex);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		
		}

		
		/// <summary>
		/// Returns a dataset with coverages after applying business rules 
		/// </summary>
		/// <returns>dataset after filteration of records (coverages) that are no longer required </returns>
		public DataSet GetHomeSection2Coverages(int customerID, int appID, int appVersionID, int dwellingID, string appType,string COVERAGETYPE)
		{
			//fetching dataset with all coverages
			DataSet dsCoverages=null;
			dsCoverages = this.GetDwellingSection2Coverages(customerID,
				appID,
				appVersionID,
				dwellingID,appType,COVERAGETYPE
				);	

			//fetching XML string with all coverages to remove
			
			string covXML=this.GetSection2ToRemove(customerID,
				appID,
				appVersionID,
				dwellingID,dsCoverages
				);	
			
			//Get Mandatory coverages XML
			string mandXML = this.GetSection2MandatoryCoverages(customerID,
				appID,
				appVersionID,
				dwellingID,dsCoverages);
			/*Reading from the XML file and saving the text in string until the function start giving the XML string */
			
			/*TextReader tr=new StreamReader(System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath  + "/working/coveragedummyxml.xml"));
			string covXML=tr.ReadToEnd(); 
			tr.Close();*/
			
		
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
			
		
			if ( mandXML != "" )
			{
				//function call to update mandatory field
				dsCoverages=this.UpdateCoverageMandatory(dsCoverages,mandXML);		
			}

			return dsCoverages;             
		}

		
		/// <summary>
		/// Gets section 2 coverages for policy
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <param name="dwellingID"></param>
		/// <param name="polType"></param>
		/// <param name="COVERAGETYPE"></param>
		/// <returns></returns>
		public DataSet GetHomeSection2CoveragesForPolicy(int customerID, int polID, int polVersionID, int dwellingID, string polType,string COVERAGETYPE)
		{
			//fetching dataset with all coverages
			DataSet dsCoverages=null;
			dsCoverages = GetDwellingSection2CoveragesForPolicy(customerID,
				polID,
				polVersionID,
				dwellingID,polType,COVERAGETYPE
				);	

			//fetching XML string with all coverages to remove
			

		//Modified By Shafi

//			string covXML=this.GetSection2ToRemoveForPolicy(customerID,
//				polID,
//				polVersionID,
//				dwellingID,dsCoverages
//				);	
//			//Get Mandatory coverages XML
//			string mandXML = this.GetSection2MandatoryCoveragesForPolicy(customerID,
//				polID,
//				polVersionID,
//				dwellingID,dsCoverages);
			
				string covXML=this.GetSection2ToRemove(customerID,
				polID,
				polVersionID,
				dwellingID,dsCoverages
				);	
			

			
				string mandXML = this.GetSection2MandatoryCoverages(customerID,
				polID,
				polVersionID,
				dwellingID,dsCoverages);
			/*Reading from the XML file and saving the text in string until the function start giving the XML string */
			
			/*TextReader tr=new StreamReader(System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath  + "/working/coveragedummyxml.xml"));
			string covXML=tr.ReadToEnd(); 
			tr.Close();*/
			
			

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
			
		
			if ( mandXML != "" )
			{
				//function call to update mandatory field
				dsCoverages=this.UpdateCoverageMandatory(dsCoverages,mandXML);		
			}

			return dsCoverages;             
		}




		#region GetDwellingSection2Coverages For Policy

		public static DataSet GetDwellingSection2CoveragesForPolicy(int customerID, int polID, 
			int polVersionID, int dwellingID, string polType,string COVERAGETYPE)
		{
			string	strStoredProc =	"Proc_GetPOL_DWELLING_COVERAGES_SECTION2";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",polID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);
			objWrapper.AddParameter("@DWELLING_ID",dwellingID);
			objWrapper.AddParameter("@POLICY_TYPE",polType);
			objWrapper.AddParameter("@COVERAGE_TYPE",COVERAGETYPE);
			//objWrapper.AddParameter("@CURRENT_PAGE_INDEX",currentPageIndex);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		
		}
		#endregion


		#endregion

		#region Rental Section
		
		/// <summary>
		/// Returns an XML with mandatory coverages read from XML file
		/// </summary>
		/// <param name="stateID"></param>
		/// <param name="product"></param>
		/// <returns></returns>
		public string GetRentalMandatoryCoveragesFromXML(int stateID, int product)
		{
			string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/Coverages/RentalCoverages.xml");
			string xml = "";
			StringBuilder sbXML = new StringBuilder();
			
			XmlDocument doc = new XmlDocument();
			doc.Load(filePath);

			XmlNode node = doc.SelectSingleNode("Root/State[@ID=" +  stateID.ToString() + "]");
	
			if ( node == null ) return "";

			XmlNode productNode = node.SelectSingleNode("Product[@ID=" + product.ToString() + "]");

			if ( productNode == null ) return "";

			XmlNode removeNode = productNode.SelectSingleNode("Mandatory/Coverages");

			if ( removeNode == null ) return "";

			XmlNodeList endList = removeNode.SelectNodes("Coverage");

			foreach(XmlNode remNode in endList)
			{
				string coverageID = remNode.Attributes["COV_ID"].Value;
				string covCode = remNode.Attributes["Code"].Value;
				string mand = "Y";

				sbXML.Append("<Coverage COV_ID=\"" + coverageID + "\" COV_CODE=\"" +covCode+"\" Remove=\"N\" Mandatory=\"" + mand + "\">");
				sbXML.Append("</Coverage>");
			}

			return sbXML.ToString();

		}


		/// <summary>
		/// Gets mandatory rental coverages 
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <param name="dwellingid"></param>
		/// <param name="objDataSet"></param>
		/// <returns></returns>
		public string GetRentalMandatoryCoveragesForPolicy(int customerID,
			int polID, 
			int polVersionID, 
			int dwellingid, DataSet objDataSet)
		{
			//Get Coverage/limits info
			DataSet dsCovLmits = ClsDwellingCoverageLimit.GetDwellingCoverageLimitsForPolicy(customerID,
				polID,
				polVersionID,
				dwellingid);
				
			StringBuilder sbXML = new StringBuilder();
			
			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtLimits = objDataSet.Tables[1];
			DataTable dtHome = objDataSet.Tables[2];
			DataTable dtRating = objDataSet.Tables[3];

			string covID="";
			string mand ="";
			string  covCode="";
			int policyType = 0;
			int stateID = 0;
			string buildersRisk = "";
			
			//Builder's Risk
			if ( dtRating != null )
			{
				if ( dtRating.Rows.Count > 0 )
				{
					if ( dtRating.Rows[0]["IS_UNDER_CONSTRUCTION"] != DBNull.Value )
					{
						buildersRisk = dtRating.Rows[0]["IS_UNDER_CONSTRUCTION"].ToString();
					}
				}
			}
				
			if ( dtHome != null )
			{
				if ( dtHome.Rows.Count > 0 )
				{
					if ( dtHome.Rows[0]["POLICY_TYPE"] != DBNull.Value )
					{
						policyType = Convert.ToInt32(dtHome.Rows[0]["POLICY_TYPE"]);
					}

					if ( dtHome.Rows[0]["STATE_ID"] != DBNull.Value )
					{
						stateID = Convert.ToInt32(dtHome.Rows[0]["STATE_ID"]);
					}

				}
			}

			string mandXML = this.GetRentalMandatoryCoveragesFromXML(stateID,policyType);

			//DataRow[] drMand = dtCoverage.Select("COV_CODE='DWELL'or COV_CODE='OSTR' or COV_CODE='LPP' or COV_CODE='RV'");
				
			sbXML.Append("<Coverages>");
			
			sbXML.Append(mandXML);
			
			//Depending on Builders risk in Ratng Info page, make these mandatory*********
			//Installation Floater – Building Materials (IF-184)
			//Installation Floater – Non-Structural Equipment (IF-184)
			DataRow[] drMand = dtCoverage.Select("COV_CODE='IF184'or COV_CODE='IFNSE'");

			foreach(DataRow dr in drMand)
			{
				//Default 
				mand = "N";

				covCode = dr["COV_CODE"].ToString();
				
				//810 IF184	Installation Floater – Building Materials (IF-184)
				//IFNSE	Installation Floater – Non-Structural Equipment (IF-184)
				if ( covCode == "IF184" || covCode == "IFNSE" )
				{
					if ( buildersRisk == "" || buildersRisk == "0" )
					{
						mand = "Y";
					}
				}
				
				/*
				//Coverage A - Dwelling
				//Coverage B - Other Structures
				//Coverage C - Unscheduled Personal Property
				//Coverage D - Loss of Use
				//Coverage E - Personal Liability
				//Coverage F - Medical Payments to Others
				if ( covCode ==  "DWELL" || covCode == "OSTR" || 
					covCode == "LPP" || covCode == "RV" || covCode == "CSL" || covCode == "MEDPM" || covCode == "SD" ||
					covCode == "BR1143"
					)
				{
					mand = "Y";
				}*/
				
			
				covID = dr["COV_ID"].ToString();
				
				covCode=dr["COV_CODE"].ToString();
				sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"" +covCode+"\" Remove=\"N\" Mandatory=\"" + mand + "\">");
				sbXML.Append("</Coverage>");
			}	
			
			sbXML.Append("</Coverages>");

			return sbXML.ToString();

		}


		/// <summary>
		/// Returns an XML containing coverages to be removed
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="dwellingid"></param>
		/// <param name="objDataSet"></param>
		/// <returns></returns>
		public string GetRentalMandatoryCoverages(int customerID,
			int appID, 
			int appVersionID, 
			int dwellingid, DataSet objDataSet)
		{
			//Get Coverage/limits info
			DataSet dsCovLmits = ClsDwellingCoverageLimit.GetDwellingCoverageLimits(customerID,
				appID,
				appVersionID,
				dwellingid);
				
			StringBuilder sbXML = new StringBuilder();
			
			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtLimits = objDataSet.Tables[1];
			DataTable dtHome = objDataSet.Tables[2];
			DataTable dtRating = objDataSet.Tables[3];

			string covID="";
			string mand ="";
			string  covCode="";
			int policyType = 0;
			string buildersRisk = "";
			int stateID = 0;

			//Builder's Risk
			if ( dtRating != null )
			{
				if ( dtRating.Rows.Count > 0 )
				{
					if ( dtRating.Rows[0]["IS_UNDER_CONSTRUCTION"] != DBNull.Value )
					{
						buildersRisk = dtRating.Rows[0]["IS_UNDER_CONSTRUCTION"].ToString();
					}
				}
			}
			
			if ( dtHome != null )
			{
				if ( dtHome.Rows.Count > 0 )
				{
					if ( dtHome.Rows[0]["POLICY_TYPE"] != DBNull.Value )
					{
						policyType = Convert.ToInt32(dtHome.Rows[0]["POLICY_TYPE"]);
					}

					if ( dtHome.Rows[0]["STATE_ID"] != DBNull.Value )
					{
						stateID = Convert.ToInt32(dtHome.Rows[0]["STATE_ID"]);
					}

				}
			}
			
				
			sbXML.Append("<Coverages>");
			
			
			string mandXML = this.GetRentalMandatoryCoveragesFromXML(stateID,policyType);
			
			sbXML.Append(mandXML);

			DataRow[] drMand = dtCoverage.Select("COV_CODE='IF184'or COV_CODE='IFNSE'");
			foreach(DataRow dr in drMand)
			{
				//Default 
				mand = "N";

				covCode = dr["COV_CODE"].ToString();
				
				//810 IF184	Installation Floater – Building Materials (IF-184)
				//IFNSE	Installation Floater – Non-Structural Equipment (IF-184)
				if ( covCode == "IF184" || covCode == "IFNSE" )
				{
					if ( buildersRisk == "" || buildersRisk == "0" )
					{
						mand = "Y";
					}
				}
				
				/*
				//Coverage A - Dwelling
				//Coverage B - Other Structures
				//Coverage C - Unscheduled Personal Property
				//Coverage D - Loss of Use
				//Coverage E - Personal Liability
				//Coverage F - Medical Payments to Others
				if ( covCode ==  "DWELL" || covCode == "OSTR" || 
					covCode == "LPP" || covCode == "RV" || covCode == "CSL" || covCode == "MEDPM" || covCode == "SD" ||
					covCode == "BR1143"
					)
				{
					mand = "Y";
				}*/
				
			
				covID = dr["COV_ID"].ToString();
				
				covCode=dr["COV_CODE"].ToString();
				sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"" +covCode+"\" Remove=\"N\" Mandatory=\"" + mand + "\">");
				sbXML.Append("</Coverage>");
			}	
			
			sbXML.Append("</Coverages>");

			return sbXML.ToString();

		}


		//start
		/// <summary>
		/// 
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <param name="dwellingID"></param>
		/// <param name="polType"></param>
		/// <param name="COVERAGETYPE"></param>
		/// <returns></returns>
		public DataSet GetRentalCoveragesForPolicy(int customerID, int polID, int polVersionID, int dwellingID, string polType,string COVERAGETYPE)
		{
			

			DataSet dsCoverages=null;
			dsCoverages = this.GetRentalDwellingSection1CoveragesForPolicy(customerID,
				polID,
				polVersionID,
				dwellingID,polType,COVERAGETYPE
				);	

			//fetching XML string with all coverages to remove
			

			
			string covXML=this.GetRentalCoveragesToRemove(dsCoverages);	
			
			
			string mandXML = this.GetRentalMandatoryCoveragesForPolicy(customerID,
				polID,
				polVersionID,
				dwellingID,dsCoverages);
			
			/*Reading from the XML file and saving the text in string until the function start giving the XML string */
			
			/*TextReader tr=new StreamReader(System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath  + "/working/coveragedummyxml.xml"));
			string covXML=tr.ReadToEnd(); 
			tr.Close();*/
			
			 
			
			
			
			//if XML string is not blank		
			if(covXML!="" )
			{
				//Cms.BusinessLayer.BlApplication.clsapplication 	objCovInformation = new Cms.BusinessLayer.BlApplication.clsapplication();
				//function call to delete coverage
				dsCoverages=this.DeleteCoverage(dsCoverages,covXML);			

				//function call to delete coverage limits
				dsCoverages=this.DeleteCoverageOptions(dsCoverages,covXML);			

				//function call to update default field
				dsCoverages=this.OverwriteCoverageDefaultValue(dsCoverages,covXML);			
			}

			if ( mandXML != "" )
			{
				//function call to update mandatory field
				dsCoverages=this.UpdateCoverageMandatory(dsCoverages,mandXML);			
			}

			return dsCoverages;             
		}
		   

		public int SaveRentalCoveragesForPolicy(ArrayList alNewCoverages,string strOldXML,string CoverageType,string strCustomInfo)
		{
			
			string	strStoredProc =	"Proc_SAVE_HOME_COVERAGES_FOR_POLICY";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			SqlCommand cmdCoverage = new SqlCommand();
			cmdCoverage.CommandText = strStoredProc;
			cmdCoverage.CommandType = CommandType.StoredProcedure;
			
			SqlParameter[] param = new SqlParameter[16];
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
			int riskId=0;
			//string strCustomInfo="Following coverages have been deleted:",str="";
			//strCustomInfo="";
		
			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					//Cms.Model.Policy.Homeowners.ClsPolicyDwellingSectionCoveragesInfo  objNew = (Cms.Model.Policy.Homeowners.ClsPolicyDwellingSectionCoveragesInfo)alNewCoverages[i];
					Cms.Model.Policy.ClsPolicyCoveragesInfo objNew =(Cms.Model.Policy.ClsPolicyCoveragesInfo)alNewCoverages[i];
					//ClsPolicyCoveragesInfo objNew =new ClsPolicyCoveragesInfo();
					customerID = objNew.CUSTOMER_ID;
					polID = objNew.POLICY_ID;
					polVersionID = objNew.POLICY_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@POLICY_ID",objNew.POLICY_ID);
					objWrapper.AddParameter("@POLICY_VERSION_ID",objNew.POLICY_VERSION_ID);
					riskId=objNew.RISK_ID;
					objWrapper.AddParameter("@DWELLING_ID",objNew.RISK_ID);
    				objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
					objWrapper.AddParameter("@LIMIT_1_TYPE",objNew.LIMIT_1_TYPE);
					objWrapper.AddParameter("@LIMIT_2_TYPE",objNew.LIMIT_2_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_1",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
					objWrapper.AddParameter("@DEDUCTIBLE_2",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_2));
					objWrapper.AddParameter("@LIMIT_1",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
					objWrapper.AddParameter("@LIMIT_2",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_2));
					objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE",objNew.DEDUCTIBLE_1_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE",objNew.DEDUCTIBLE_2_TYPE);
					objWrapper.AddParameter("@WRITTEN_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
					objWrapper.AddParameter("@FULL_TERM_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
					objWrapper.AddParameter("@COVERAGE_TYPE",CoverageType);
					objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",objNew.LIMIT1_AMOUNT_TEXT);
					objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",objNew.LIMIT2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",objNew.DEDUCTIBLE1_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",objNew.DEDUCTIBLE2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE_AMOUNT",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE));
					objWrapper.AddParameter("@DEDUCTIBLE_TEXT",objNew.DEDUCTIBLE_TEXT);
					objWrapper.AddParameter("@ADDDEDUCTIBLE_ID",objNew.ADDDEDUCTIBLE_ID);
					objWrapper.AddParameter("@LIMIT_ID",DefaultValues.GetIntNullFromNegative(objNew.LIMIT_ID));
					objWrapper.AddParameter("@DEDUC_ID",DefaultValues.GetIntNullFromNegative(objNew.DEDUC_ID));
					string strTranXML = "";
					
					//Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					//if ( objNew.COVERAGE_ID == -1 )
					if(objNew.ACTION == "I")
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("/policies/Aspx/Homeowner/PolicyRentalCoverages.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						/*objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Home coverages added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;*/
						sbTranXML.Append(strTranXML);						
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();

					}
					else if ( objNew.ACTION == "U")
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("/policies/Aspx/Homeowner/PolicyRentalCoverages.aspx.resx");
				        
						strTranXML = this.GetPolicyHomeTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);
						
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();
					}
				
					/*if ( strTranXML.Trim() == "" )
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
						objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Home coverages added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						
						//SqlHelper.ExecuteNonQuery(tran,CommandType.StoredProcedure,"Proc_SAVE_VEHICLE_COVERAGES",param);
						//int retVal = cmdCoverage.ExecuteNonQuery();
						//ClsCommon.AddTransactionLog(objTransactionInfo,tran);
						
						
					}*/
					
					//Update Coverages
					//objWrapper.ExecuteNonQuery(strStoredProc);

					//objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					
					///objWrapper.ClearParameteres();

					//Update Transaction log
					//objWrapper.ExecuteNonQuery(objTransactionInfo);
				
					//objWrapper.ClearParameteres();
					
					//Update linked Endorsements/////////////////////////////
					if ( CoverageType == "S1" )
					{
						objWrapper.ClearParameteres();
						objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
						objWrapper.AddParameter("@POLICY_ID",objNew.POLICY_ID);
						objWrapper.AddParameter("@POLICY_VERSION_ID",objNew.POLICY_VERSION_ID);
						
							objWrapper.AddParameter("@DWELLING_ID",objNew.RISK_ID);
						
						objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
						objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);

						objWrapper.ExecuteNonQuery("Proc_UPDATE_DWELLING_ENDORSEMENTS_FOR_POLICY");

						objWrapper.ClearParameteres();
					}
					//////////////////////////////////////////////////////////
					
					if(objNew.ACTION=="D")
					{
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("/policies/Aspx/Homeowner/PolicyRentalCoverages.aspx.resx");
						
						objWrapper.ClearParameteres();
						//str+=";" + objNew.COV_DESC;
						objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
						objWrapper.AddParameter("@POLICY_ID",objNew.POLICY_ID);
						objWrapper.AddParameter("@POLICY_VERSION_ID",objNew.POLICY_VERSION_ID);
						objWrapper.AddParameter("@DWELLING_ID",objNew.RISK_ID);					
						objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);						
						objWrapper.ExecuteNonQuery("Proc_DeletePOL_HOME_COVERAGES");				
						objWrapper.ClearParameteres();
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						string strTransXML = objBuilder.GetDeleteTransactionLogXML(objNew);

						sbTranXML.Append(strTransXML);


						
					}
				}
				//Insert tran log entry//////////////////////////
				/*if(str!="")
					strCustomInfo+=str;
				else
					strCustomInfo="";*/
				
				sbTranXML.Append("</root>");

				objWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objWrapper.AddParameter("@POL_ID",polID );
				objWrapper.AddParameter("@POL_VERSION_ID",polVersionID);
				objWrapper.AddParameter("@DWELLING_ID",riskId);
				objWrapper.ExecuteNonQuery("Proc_UPDATE_DWELLING_LINKED_COVERAGES_FOR_POLICY");
				objWrapper.ClearParameteres();
				


				//if(sbTranXML.ToString()!="<root></root>")
				//	strCustomInfo+=";Following coverages have been added/updated";

				if(sbTranXML.ToString()!="<root></root>")// || strCustomInfo!="")
				{
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.POLICY_ID =polID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =polVersionID;
					objTransactionInfo.CLIENT_ID = customerID;


                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1731", "");// "Rental coverages updated.";
					if(sbTranXML.ToString()!="<root></root>")
						objTransactionInfo.CHANGE_XML		=	sbTranXML.ToString();

					objTransactionInfo.CUSTOM_INFO=strCustomInfo;
				
					objWrapper.ClearParameteres();

					objWrapper.ExecuteNonQuery(objTransactionInfo);
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



		#endregion
		
		#region "Home rules"
			
		/// <summary>
		/// Returns an XML containing coverages to be removed
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="dwellingid"></param>
		/// <param name="objDataSet"></param>
		/// <returns></returns>
		public string GetSection2ToRemove(int customerID,int appID, int appVersionID, int dwellingid, DataSet objDataSet)
		{
			//Get Coveage/Limits info
			//DataSet ds = this.GetVehicleInfo(customerID,appID,appVersionID,dwellingid);
			//DataTable dtHome = ds.Tables[0];
	
			StringBuilder sbXML = new StringBuilder();
			
			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtLimits = objDataSet.Tables[1];
			DataTable dtHome = objDataSet.Tables[2];
			
			string product = "";
			int policyType = 0;
			string covID="";
			string mand ="";
			string  covCode="";

	
			if ( dtHome.Rows.Count > 0 )
			{
				if ( dtHome.Rows[0]["LOOKUP_VALUE_CODE"] != System.DBNull.Value )
				{
					product = dtHome.Rows[0]["LOOKUP_VALUE_CODE"].ToString();
				}

				if ( dtHome.Rows[0]["POLICY_TYPE"] != System.DBNull.Value )
				{
					policyType = Convert.ToInt32(dtHome.Rows[0]["POLICY_TYPE"]);
				}
			}

			
			
			sbXML.Append("<Coverages>");
    
			

		// Remove Waterbed liability for all except :
		//HO4, HO4 Deluxe HO6 and HO 6 Deluxe
		//HO-2///////////////////////////////////////////////////////////////////////////////	

		if ( policyType ==  11402 ||  policyType == 11403 || policyType == 11400 ||
		policyType == 11409 || policyType == 11404 || policyType == 11401 || policyType == 11410 ||
		policyType ==  11192 ||  policyType == 11193 || policyType == 11148 || policyType == 11194 ||
		policyType == 11149

		)
	{
		DataRow[] dr = dtCoverage.Select("COV_ID = 812 or COV_ID = 813");
					
		if ( dr != null  && dr.Length > 0 )
	{
					
		covID = dr[0]["COV_ID"].ToString();
		foreach(DataRow dr1 in dr)
	{
		covID = dr1["COV_ID"].ToString();
		mand=dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
		covCode=dr1["COV_CODE"].ToString();
		sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"" +covCode+"\" Remove=\"Y\" Mandatory=\""+mand+"\">");
		sbXML.Append("</Coverage>");
	}

}
					
}
			
			sbXML.Append("</Coverages>");

			return sbXML.ToString();
		}

		/// <summary>
		/// Returns an XML containing coverages to be removed
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="dwellingid"></param>
		/// <param name="objDataSet"></param>
		/// <returns></returns>
		public string GetHomeCoveragesToRemove(int customerID,int appID, int appVersionID, int dwellingid, DataSet objDataSet)
		{
			//Get Coveage/Limits info
			//DataSet ds = this.GetVehicleInfo(customerID,appID,appVersionID,dwellingid);
			//DataTable dtHome = ds.Tables[0];
	
			StringBuilder sbXML = new StringBuilder();
			
			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtLimits = objDataSet.Tables[1];
			DataTable dtHome = objDataSet.Tables[2];
			DataTable dtCovLimits = objDataSet.Tables[3];
			DataTable dtLocation = objDataSet.Tables[4];
			int numWatercrafts = 0;
			
			DataTable dtWater = objDataSet.Tables[7];
			

			if ( dtWater != null )
			{
				if ( dtWater.Rows.Count > 0)
				{
					if ( dtWater.Rows[0]["COUNT_WATERCRAFTS"] != DBNull.Value )
					{
						numWatercrafts = Convert.ToInt32(dtWater.Rows[0]["COUNT_WATERCRAFTS"]);
					}
				}
			}

			StringBuilder sb = new StringBuilder();	
			
			// If boat added to a home, HO-865 Watercraft Endorsement 
			//should show up on the home policy (HO-2, HO-3, HO-4, HO-5, HO-6, HO-2 Repair, HO-3 Repair)		 
			

			string product = "";
			int stateID = 0;

            if (dtHome.Rows.Count  > 0)
            {
                if (dtHome.Rows[0]["LOOKUP_VALUE_CODE"] != System.DBNull.Value)
                {
                    product = dtHome.Rows[0]["LOOKUP_VALUE_CODE"].ToString();
                }

                if (dtHome.Rows[0]["STATE_ID"] != System.DBNull.Value)
                {
                    stateID = Convert.ToInt32(dtHome.Rows[0]["STATE_ID"]);
                }





                int policyType = 0;

                if (dtHome.Rows[0]["POLICY_TYPE"] != System.DBNull.Value)
                {
                    policyType = Convert.ToInt32(dtHome.Rows[0]["POLICY_TYPE"]);
                }


                sbXML.Append("<Coverages>");

                string xml = this.GetHomeCoveragesToRemoveFromXML(Convert.ToInt32(stateID), Convert.ToInt32(policyType), objDataSet);

                sbXML.Append(xml);
            }

			//County Check 
			string covID ;
			string covCode;
			int HAS_VALID_COUNTY = Convert.ToInt32(dtLocation.Rows[0]["HAS_VALID_COUNTY"]); 
			if(HAS_VALID_COUNTY == 0)
			{
				DataRow[] dr = dtCoverage.Select("COV_CODE IN ('MIN##')");
					
				if ( dr != null  && dr.Length > 0 )
				{
					if ( sbXML.ToString() == "" )
					{
						sbXML.Append("<Coverages>");
					}

					covID = dr[0]["COV_ID"].ToString();
					foreach(DataRow dr1 in dr)
					{
						covID = dr1["COV_ID"].ToString();
						covCode=dr1["COV_CODE"].ToString();
						sbXML.Append("<Coverage COV_ID='" + covID + "' COV_CODE='" + covCode + "' Remove='Y' Mandatory='N'>");
						sbXML.Append("</Coverage>");
					}

				}
			}

			if(numWatercrafts == 0)
			{
				DataRow[] dr = dtCoverage.Select("COV_CODE IN ('WP865')");
					
				if ( dr != null  && dr.Length > 0 )
				{
					if ( sbXML.ToString() == "" )
					{
						sbXML.Append("<Coverages>");
					}

					covID = dr[0]["COV_ID"].ToString();
					foreach(DataRow dr1 in dr)
					{
						covID = dr1["COV_ID"].ToString();
						covCode=dr1["COV_CODE"].ToString();
						sbXML.Append("<Coverage COV_ID='" + covID + "' COV_CODE='" + covCode + "' Remove='Y' Mandatory='N'>");
						sbXML.Append("</Coverage>");
					}

				}
			}
			/////////End Of County check


			if ( sbXML.ToString() != "" )
			{
				sbXML.Append("</Coverages>");
			}

			return sbXML.ToString();


		}

		
		/// <summary>
		/// Returns an XML containing coverages to be removed
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="dwellingid"></param>
		/// <param name="objDataSet"></param>
		/// <returns></returns>
		public string GetHomeMandatoryCoverages(int customerID,
			int appID, 
			int appVersionID, 
			int dwellingid, DataSet objDataSet)
		{
			
			StringBuilder sbXML = new StringBuilder();
			string isPrimary = "";
			int locType=0;
			string covID="";
			string mand ="";
			string  covCode="";
			int policyType = 0;
			int stateID = 0;
			decimal dwellLimit = 0;
			bool ho42Taken = false;

			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtLimits = objDataSet.Tables[1];
			DataTable dtHome = objDataSet.Tables[2];
			DataTable dtCovLimits = objDataSet.Tables[3];
			DataTable dtLocation = objDataSet.Tables[4];
			DataTable dtSection2 = objDataSet.Tables[5];

			//Get Location details
			if ( dtLocation != null )
			{
				if ( dtLocation.Rows.Count > 0 )
				{
					if ( dtLocation.Rows[0]["IS_PRIMARY"] != DBNull.Value )
					{
						isPrimary =dtLocation.Rows[0]["IS_PRIMARY"].ToString();;
					}
					if ( dtLocation.Rows[0]["LOC_TYPE"] != DBNull.Value )
					{
						locType = Convert.ToInt32(dtLocation.Rows[0]["LOC_TYPE"]);
					}
				}
			}

            if (dtHome.Rows.Count > 0)
            {
                if (dtHome.Rows[0]["POLICY_TYPE"] != System.DBNull.Value)
                {
                    policyType = Convert.ToInt32(dtHome.Rows[0]["POLICY_TYPE"]);
                }

                if (dtHome.Rows[0]["STATE_ID"] != System.DBNull.Value)
                {
                    stateID = Convert.ToInt32(dtHome.Rows[0]["STATE_ID"]);
                }

            }
			
			
			if ( dtCovLimits != null )
			{
				if ( dtCovLimits.Rows.Count > 0 )
				{

					if (int.Parse(dtCovLimits.Rows[0]["HO42"].ToString()) != 0 )
					{
						ho42Taken = true;
					}
					else
					{
                       ho42Taken  =false; 
					}

					
				}
			}

			

			

			DataRow[] drDwell = dtCoverage.Select("COV_CODE IN ('DWELL')");
			//Get the coverages to make mandatory from the master dataset and create XML
			DataRow[] drMandatory ;

			sbXML.Append("<Coverages>");


			 drMandatory=dtCoverage.Select("(COV_CODE='EBP24'or COV_CODE='EBP25') and COVERAGE_ID <> NULL");
			if(drMandatory.Length > 0)
			{
				 drMandatory=dtCoverage.Select("COV_CODE='WBSPO'");
				foreach(DataRow dr in drMandatory)
				{
					covID = dr["COV_ID"].ToString();
					covCode=dr["COV_CODE"].ToString();
					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"" +covCode+"\" Remove=\"N\" Mandatory=\"Y\" >");
					sbXML.Append("</Coverage>");

				}

			}




			
			


          //MICHIGAN STATE*****************************

			if ( stateID == 22 )
			{
				
				
			    drMandatory=dtCoverage.Select("COV_CODE='FRAUD'or COV_CODE='REDUC'");
				foreach(DataRow dr in drMandatory)
				{
					//Default 
					mand = "N";

					covCode = dr["COV_CODE"].ToString();
					covID = dr["COV_ID"].ToString();

				/*
					//For All Products 
					if ( covCode=="EBAIRP" || covCode == "EBOS40" || covCode== "EBOS489" || covCode == "EBSS490")
					{
						mand = "Y";
					}
					//Repair Cost Homeowners (HO-289) Will Be Diabled For Repair Cost Programms

					if (policyType == 11404 || policyType == 11403)
					{
						if ( covCode=="RECST")
						{
							mand = "Y";
						}
					}
					if (policyType == 11403)
					{
						if(covCode=="BUMC")
						{
							mand="Y";
						}
					}
                   */
					//If Seasonal/Seconary = "Y" disable Identify Fraud Expense
					if ( covCode == "FRAUD")
					{
						if ( locType ==11812)
						{
							mand = "N";
						}
						else
						{
							mand = "Y";
						}
					
					}	

					if ( covCode == "REDUC")
					{	
						//For 4, 4D, 6, 6D, 5 not available
						if ( ho42Taken == true)
						{
							mand = "Y";	
						}	
						else
						{
							mand = "N";	
						}
						
					}
					covID = dr["COV_ID"].ToString();
				
					covCode=dr["COV_CODE"].ToString();
					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"" +covCode+"\" Remove=\"N\" Mandatory=\"" + mand + "\">");
					sbXML.Append("</Coverage>");
					/////////////////////	
					
				//Changed By Ravindra now Mandatory in case of HO-5 products

//					//Replacement Cost Personal Property (HO-34)
//					//Mandatory for all
//					if ( covCode == "EBRCPP")
//					{	
//						mand = "Y";
//						
//					}
//					//Expanded Replacement Coverages A+B (HO-11) EBEP11
//					//Mandatory for HO-5, HO-5 Premier and HO-3 products
//					if (covCode == "EBEP11")
//					{
//						if ( policyType == 11401 || policyType == 11410 || policyType == 11400 || policyType == 11409 )
//						{
//							mand = "Y";
//						}
//					}
					
					/*	if ( policyType == 11401 || policyType == 11410 )
						{
							if ( covCode == "EBRCPP" || covCode == "EBEP11" || covCode == "ECOC")
							{
								mand = "Y";
							}
						}
					
					if(policyType == 11406)
					{
						if ( covCode == "EBCASP")
						{
							mand = "Y";	
						}
					}
					if(covCode == "OSTDISH" || covCode == "WBSPO")
					{
						mand="Y";
					}

					//Reduction in Limit - Coverage C
					//Available only if repl cost is not taken and HO-42 endorsement is not taken
				
					

				//HO-5
					//Preferred Plus V.I.P. Coverage (HO-23)
					if ( covCode == "EBP23")
					{
						if ( policyType == 11401)
						{
							mand = "Y";
						}
					}
			
					//HO-5 Premier
					//Premier V.I.P. (HO-25)
					if ( covID == "196" )
					{
						if ( policyType == 11410)
						{
							mand = "Y";
						}
					}

					//HO-6 Deluxe ///
					//Unit Owners Coverage A Special Coverage (HO-32) is mandatory
					if ( covCode == "EBCASP" || covCode == "LAC")
					{
						if ( policyType == 11408 )
						{
							mand = "Y";
						}
					}
					////////
					
					//HO-6
					//Loss Assessment Coverage (HO-35)
					if ( covCode == "LAC" )
					{
						if ( policyType == 11406 ||  policyType == 11408)
						{
							mand = "Y";
						}
					}
					
					//HO-4 and HO-4 Deluxe
					if ( covCode == "EBRDC" )
					{
						if ( policyType == 11405 ||  policyType == 11407)
						{
							mand = "Y";
						}
					}
					
					//HO-6 and HO-6 Deluxe
					//Make Condominium Deluxe Coverage (HO-66) mandatory 93
					if ( covID == "93" )
					{
						if ( policyType == 11408)
						{
							mand = "Y";
						}
					}
					
					//HO-4 and HO-4 Deluxe
					//Make Renters Deluxe Coverage (HO-64) mandatory 92
					
					if ( covID == "92" )
					{
						if ( policyType == 11407)
						{
							mand = "Y";
						}
					}
                
					//Coverage A - Dwelling
					//Coverage B - Other Structures
					//Coverage C - Unscheduled Personal Property
					//Coverage D - Loss of Use
					//Coverage E - Personal Liability
					//Coverage F - Medical Payments to Others
					//EBIF96 Fire Department Service Charge (HO-96)
					//EBOS48 Other Structures Increased Limit (HO-48)
					//EBCCSL Unscheduled Jewelry & Furs
					//EBCCSS  Securities
					//EBCCSI Silverware, Goldware & Pewterware
					//EBCCSF	Firearms
					//IBUSP Business Property Increase Limits (HO-312)
					//EBICC53 Credit Card and Depositors Forgery (HO-53)
					//Dwelling Under Construction (HO-14) EBDUC
					if ( covCode ==  "DWELL" || covCode == "OS" || 
						covCode == "EBUSPP" || covCode == "LOSUR" || covCode == "PL" || 
						covCode == "MEDPM" || covCode == "EBIF96" || covCode == "EBOS48" || 
						covCode == "EBCCSL" || covCode == "EBCCSS" || covCode == "EBCCSI" ||
						covCode == "EBCCSF" || covCode == "IBUSP" || covCode == "EBICC53" || 
						covCode == "EBCCSM" || covCode == "ESCCSS" || covCode == "EBDUC"
						)
					{
						mand = "Y";
					}
				
					Ho- 4 
					ho6
					ho 4 deluxe
					ho 6 deluxe
					Ordinace disabled
					if ( covCode == "SEWER" && 
						(policyType == 11405 || policyType ==  11407 || policyType ==  11406 || policyType == 11408)
						)
					{
						mand = "Y";
					}
					*/
						
					//If Central Station or Direct to Fire and Police attach HO-216

					//End of Ho-216

					
				}	
			
			}
			//END of MICHIGAN STATE*******************************************
			
			//INDIANA STATE///////////////////////////////////////////////////
			if ( stateID == 14 )
			{
				drMandatory=dtCoverage.Select("COV_CODE='FRAUD' or COV_CODE='REDUC' or COV_CODE='EBP24'");
				foreach(DataRow dr in drMandatory)
				{
					//Default 
					mand = "N";

					covCode = dr["COV_CODE"].ToString();
					covID = dr["COV_ID"].ToString();
					
					/*
					//For All Products 
					if ( covCode=="EBAIRP" || covCode == "EBOS40" || covCode== "EBOS489" || covCode == "EBSS490")
					{
						mand = "Y";
					}

					//Repair Cost Homeowners (HO-289) Will Be Diabled For Repair Cost Programms
					if (policyType == 11193|| policyType == 11194)
					{
						if ( covCode == "RECST")
						{
							mand = "Y";
						}
					}

					//If Seasonal/Seconary = "Y" disable Identify Fraud Expense
					*/
					if ( covCode == "FRAUD")
					{
						if ( locType==11812)
						{
							mand = "N";
						}
						else
						{
							mand = "Y";
						}
					
					}
					if ( covCode == "REDUC")
					{	
						//For 4, 4D, 6, 6D, 5 not available
						if (ho42Taken == true)
						{
							mand = "Y";	
						}	
						else
						{
							mand = "N";	
						}

					}
					if ( policyType == 11148 || policyType ==  11149 )
					{
						if ( covCode == "EBP24")
						{
							if ( dwellLimit < 75000 )
							{
								mand = "Y";
							}
							
						}
					}
					///////////////////////////

					covID = dr["COV_ID"].ToString();
					
					covCode=dr["COV_CODE"].ToString();
					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"" +covCode+"\" Remove=\"N\" Mandatory=\"" + mand + "\">");
					sbXML.Append("</Coverage>");
					/////////////////////	
					///
					/*
					
					if (policyType == 11193)
					{
						if(covCode=="BUMC")
						{
							mand="Y";
						}
					}
                   */
					//Reduction in Limit - Coverage C
					//Available only if repl cost is not taken and HO-42 endorsement is not taken
					
					//Expanded Replacement Coverages A+B (HO-11) EBEP11
					//Mandatory for HO-5, HO-3 products
				/*
					if (covCode == "EBEP11")
					{
						if ( policyType == 11149 || policyType == 11148)
						{
							mand = "Y";
						}
					}
					if ( covCode == "EBRCPP" || covCode == "EBOC" || covCode == "ECOC")
					{
						if ( policyType == 11149 )
						{
							mand = "Y";
						}
					}
					if(policyType == 11196)
					{
						if ( covCode == "EBCASP")
						{
							mand = "Y";	
						}
					}

					if(covCode == "OSTDISH" || covCode == "WBSPO")
					{
						mand="Y";
					}

					//HO-6 and HO-6 Deluxe
					//Make Condominium Deluxe Coverage (HO-66) mandatory 93
					if ( covID == "166" )
					{
						if ( policyType == 11246)
						{
							mand = "Y";
						}
					}
					

					//HO-4 and HO-4 Deluxe
					if ( covCode == "EBRDC" )
					{
						if ( policyType == 11195 )
						{
							mand = "Y";
						}
					}

					//HO-4 Deluxe
					//Make Renters Deluxe Coverage (HO-64) mandatory 92
					if ( covID == "165" )
					{
						if ( policyType == 11245)
						{
							mand = "Y";
						}
					}

					//HO-6 Deluxe ///
					//Unit Owners Coverage A Special Coverage (HO-32) EBCASP
					//Loss Assessment Coverage LAC 
					if ( covCode == "EBCASP" || covCode == "LAC")
					{
						if ( policyType == 11246 )
						{
							mand = "Y";
						}
					}
					////////
					
					//HO-6
					//Loss Assessment Coverage (HO-35)
					if ( covCode == "LAC" )
					{
						if ( policyType == 11196)
						{
							mand = "Y";
						}
					}
					
					
					//HO-5
					//Replacement Cost Personal Property (HO-34) EBRCPP
					//Premier V.I.P.(HO-24) EBP24
					//Water Backup and Sump Pump Overflow (HO-327) WBSPO
					if ( covCode == "EBP24" || covCode == "WBSPO")
					{
						if ( policyType == 11149)
						{
							mand = "Y";
						}
					}

					//Coverage A - Dwelling
					//Coverage B - Other Structures
					//Coverage C - Unscheduled Personal Property
					//Coverage D - Loss of Use
					//Coverage E - Personal Liability
					//Coverage F - Medical Payments to Others
					//EBIF96 Fire Department Service Charge (HO-96)
					//EBOS48 Other Structures Increased Limit (HO-48)
					//EBCCSL Unscheduled Jewelry & Furs
					//EBCCSS  Securities
					//EBCCSI Silverware, Goldware & Pewterware
					//EBCCSF	Firearms
					//IBUSP Business Property Increase Limits (HO-312)
					//EBICC53 Credit Card and Depositors Forgery (HO-53)
					//Dwelling Under Construction (HO-14) EBDUC
					if ( covCode ==  "DWELL" || covCode == "OS" || 
						covCode == "EBUSPP" || covCode == "LOSUR" || covCode == "PL" || 
						covCode == "MEDPM" || covCode == "EBIF96" || covCode == "EBOS48" || 
						covCode == "EBCCSL" || covCode == "EBCCSI" ||
						covCode == "EBCCSF" || covCode == "IBUSP" || covCode == "EBICC53" || covCode == "EBDUC" || 
						covCode == "EBCCSM" || covCode == "ESCCSS"
						)
					{
						mand = "Y";
					}
					
					Ho- 4 
						ho6
						ho 4 deluxe
						ho 6 deluxe
						Ordinace disabled
					if ( covCode == "SEWER" && 
						(policyType == 11405 || policyType ==  11407 || policyType ==  11406 || policyType == 11408)
						)
					{
						mand = "Y";
					}
					*/
					//For HO-3 and HO-5 , if Coveage A < 75,000,  then  HO-24 cannot be taken///
					
				}	
			}
			/////END OF INDIANA
			string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/Coverages/HomeCoverages.xml");
			
			XmlDocument homeMandatory=new XmlDocument();
			homeMandatory.Load(filePath);
			
			XmlNode node = homeMandatory.SelectSingleNode("Root/State[@ID=" +  stateID.ToString() + "]");
	
			if ( node == null ) return sbXML.Append("</Coverages>").ToString();

			XmlNode productNode = node.SelectSingleNode("Product[@ID=" + policyType.ToString() + "]");

			if ( productNode == null ) return sbXML.Append("</Coverages>").ToString();

			XmlNode mandatoryNode = productNode.SelectSingleNode("Mandatory/Coverages");

			if ( mandatoryNode == null ) return sbXML.Append("</Coverages>").ToString();

			XmlNodeList covList = mandatoryNode.SelectNodes("Coverage");
			
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			string coverageID;



			//Loop thru each coveages to make mandatory
			foreach(XmlNode manNode in covList)
			{
				coverageID = manNode.Attributes["COV_ID"].Value;
				covCode = manNode.Attributes["Code"].Value;
				
				if ( sb.ToString() == "" )
				{
					sb.Append("'" + covCode + "'");
				}
				else
				{
					sb.Append(",'" + covCode + "'");
				}

			}
			
			
			
			drMandatory = dtCoverage.Select("COV_CODE IN (" + sb.ToString() + ")");
		
			
			foreach(DataRow dr1 in drMandatory)
			{
				covID = dr1["COV_ID"].ToString();
				mand = "Y";
				covCode = dr1["COV_CODE"].ToString();
				sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"" +covCode+"\" Remove=\"N\" Mandatory=\"" + mand + "\">");
				sbXML.Append("</Coverage>");
			}
			
			
			sbXML.Append("</Coverages>");

			return sbXML.ToString();

		}

		/// <summary>
		/// Returns an XML containing coverages to be removed
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="dwellingid"></param>
		/// <param name="objDataSet"></param>
		/// <returns></returns>
		public string GetSection2MandatoryCoverages(int customerID,int appID, 
			int appVersionID, int dwellingid, DataSet objDataSet)
		{
			//Get Coveage/Limits info
			//DataSet ds = this.GetVehicleInfo(customerID,appID,appVersionID,dwellingid);
			//DataTable dtHome = ds.Tables[0];
	
			StringBuilder sbXML = new StringBuilder();
			string isPrimary="";
			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtLimits = objDataSet.Tables[1];
			DataTable dtHome = objDataSet.Tables[2];
			DataTable dtLocation = objDataSet.Tables[4];
			DataTable dtCoApplicant= objDataSet.Tables[7];
			
			//Get Location details
			if ( dtLocation != null )
			{
				if ( dtLocation.Rows.Count > 0 )
				{
					if ( dtLocation.Rows[0]["IS_PRIMARY"] != DBNull.Value )
					{
						isPrimary =dtLocation.Rows[0]["IS_PRIMARY"].ToString();;
					}
				}
			}

			DataTable dtSection1 = null;
			dtSection1 = objDataSet.Tables[6];	

			string product = "";
			int policyType = 0;
			string covID="";
			string mand ="";
			string  covCode="";
			int stateID = 0;
	
			if ( dtHome.Rows.Count > 0 )
			{
				if ( dtHome.Rows[0]["LOOKUP_VALUE_CODE"] != System.DBNull.Value )
				{
					product = dtHome.Rows[0]["LOOKUP_VALUE_CODE"].ToString();
				}

				if ( dtHome.Rows[0]["POLICY_TYPE"] != System.DBNull.Value )
				{
					policyType = Convert.ToInt32(dtHome.Rows[0]["POLICY_TYPE"]);
				}

				if ( dtHome.Rows[0]["STATE_ID"] != System.DBNull.Value )
				{
					stateID = Convert.ToInt32(dtHome.Rows[0]["STATE_ID"]);
				}
			}
			
			sbXML.Append("<Coverages>");

			/*
			  if primary applicant or 
			  any of the the co-applicant is having  occupation, then also it wont be available			 
			*/ 
			if(dtCoApplicant.Rows.Count >0)
			{
				if(int.Parse(dtCoApplicant.Rows[0]["Occupation"].ToString())>0)
				{
					DataRow[] dr = dtCoverage.Select("COV_ID = 274 or COV_ID = 275");
					
					if ( dr != null  && dr.Length > 0 )
					{
					
						covID = dr[0]["COV_ID"].ToString();
						foreach(DataRow dr1 in dr)
						{
							covID = dr1["COV_ID"].ToString();
							mand="Y";
							covCode=dr1["COV_CODE"].ToString();
							sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"" +covCode+"\" Remove=\"Y\" Mandatory=\""+mand+"\">");
							sbXML.Append("</Coverage>");
						}
					}
				}
			} 
			
			DataRow[] drHO17 = dtCoverage.Select("COV_ID IN (902,903)");					
			if ( drHO17 != null  && drHO17.Length > 0 )
			{
				covID = drHO17[0]["COV_ID"].ToString();
				foreach(DataRow dr1 in drHO17)
				{
					covID = dr1["COV_ID"].ToString();
					mand = "Y";
					covCode=dr1["COV_CODE"].ToString();
					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"" +covCode+"\" Remove=\"N\" Mandatory=\""+mand+"\">");
					sbXML.Append("</Coverage>");
				}
			}
			
			//Added by Charles on 10-Dec-09 for Itrack 6840
			DataRow[] drOFF_LIABILITY_EXTENDED = dtCoverage.Select("COV_ID IN (258,259)");
			DataTable dtOFF_LIABILITY_EXTENDED = null;
			dtOFF_LIABILITY_EXTENDED = objDataSet.Tables[9];
			if(drOFF_LIABILITY_EXTENDED != null)
			{
                if (objDataSet.Tables[9].Columns.Contains("COV_ID"))
                {
                    covID = drOFF_LIABILITY_EXTENDED[0]["COV_ID"].ToString();
                    covCode = drOFF_LIABILITY_EXTENDED[0]["COV_CODE"].ToString();
                    if (dtOFF_LIABILITY_EXTENDED.Rows.Count > 0)
                    {
                        if (int.Parse(dtOFF_LIABILITY_EXTENDED.Rows[0]["OFF_LIABILITY_EXTENDED"].ToString()) > 0)
                        {
                            mand = "Y";
                            sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"" + covCode + "\" Remove=\"N\" Mandatory=\"" + mand + "\">");
                            sbXML.Append("</Coverage>");
                        }
                        else
                        {
                            mand = "N";
                            sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"" + covCode + "\" Remove=\"N\" Mandatory=\"" + mand + "\">");
                            sbXML.Append("</Coverage>");
                        }
                    }
                }
			}//Added till here			

			// Make mandatory for HO-5 Indiana
			//Personal Injury (HO-82)
			//HO-5///////////////////////////////////////////////////////////////////////////////	
			if ( stateID == 14 )
			{
				if ( policyType ==  11401 ||  policyType == 11149 )
				{
					DataRow[] dr = dtCoverage.Select("COV_ID IN (274,275)");
					
					if ( dr != null  && dr.Length > 0 )
					{
						covID = dr[0]["COV_ID"].ToString();
						foreach(DataRow dr1 in dr)
						{
							covID = dr1["COV_ID"].ToString();
							mand = "Y";
							covCode=dr1["COV_CODE"].ToString();
							sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"" +covCode+"\" Remove=\"N\" Mandatory=\""+mand+"\">");
							sbXML.Append("</Coverage>");
						}

					}
					
				}

				//For HO-3, if HO-24 is selected, make it mandatory
				//If HO-24 is selected for HO-3, then Personal Injury (HO-82) is disabled and checked
				if ( policyType == 11148 )
				{
					if ( dtSection1 != null && dtSection1.Rows.Count > 0 )
					{
						DataRow[] dr82 = dtSection1.Select("COV_ID=143");

						if ( dr82.Length > 0 )
						{
							DataRow[] drMand = dtCoverage.Select("COV_ID = 274");
					
							foreach(DataRow dr1 in drMand)
							{
								covID = dr1["COV_ID"].ToString();
								mand = "Y";
								covCode=dr1["COV_CODE"].ToString();
								sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"" +covCode+"\" Remove=\"y\" Mandatory=\"" + mand + "\">");
								sbXML.Append("</Coverage>");
							}	
						}

					}
				}
				//////////////
				
			}
			
			sbXML.Append("</Coverages>");

			return sbXML.ToString();
		}


		#endregion

		#region "Policy Home rules"
		
		/// <summary>
		/// Applies business rule to section 2 home coverages
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <param name="dwellingid"></param>
		/// <param name="objDataSet"></param>
		/// <returns></returns>
		public string GetSection2ToRemoveForPolicy(int customerID,int polID, int polVersionID, int dwellingid, DataSet objDataSet)
		{
			//Get Coveage/Limits info
			//DataSet ds = this.GetVehicleInfo(customerID,appID,appVersionID,dwellingid);
			//DataTable dtHome = ds.Tables[0];
	
			StringBuilder sbXML = new StringBuilder();
			
			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtLimits = objDataSet.Tables[1];
			DataTable dtHome = objDataSet.Tables[2];
			string product = "";
			int policyType = 0;
			string covID="";
			string mand ="";
			string  covCode="";

	
			if ( dtHome.Rows.Count > 0 )
			{
				if ( dtHome.Rows[0]["LOOKUP_VALUE_CODE"] != System.DBNull.Value )
				{
					product = dtHome.Rows[0]["LOOKUP_VALUE_CODE"].ToString();
				}

				if ( dtHome.Rows[0]["POLICY_TYPE"] != System.DBNull.Value )
				{
					policyType = Convert.ToInt32(dtHome.Rows[0]["POLICY_TYPE"]);
				}
			}
			
			sbXML.Append("<Coverages>");

			// Remove Waterbed liability for all except :
			//HO4, HO4 Deluxe HO6 and HO 6 Deluxe
			//HO-2///////////////////////////////////////////////////////////////////////////////	
			if ( policyType ==  11402 ||  policyType == 11403 || policyType == 11400 ||
				policyType == 11409 || policyType == 11404 || policyType == 11401 || policyType == 11410 ||
				policyType ==  11192 ||  policyType == 11193 || policyType == 11148 || policyType == 11194 ||
				policyType == 11149

				)
			{
				DataRow[] dr = dtCoverage.Select("COV_ID = 812 or COV_ID = 813");
					
				if ( dr != null  && dr.Length > 0 )
				{
					
					covID = dr[0]["COV_ID"].ToString();
					foreach(DataRow dr1 in dr)
					{
						covID = dr1["COV_ID"].ToString();
						mand=dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
						covCode=dr1["COV_CODE"].ToString();
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"" +covCode+"\" Remove=\"Y\" Mandatory=\""+mand+"\">");
						sbXML.Append("</Coverage>");
					}

				}
					
			}
			
			sbXML.Append("</Coverages>");

			return sbXML.ToString();
		}


		/// <summary>
		/// Returns an XML containing coverages to be removed
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="dwellingid"></param>
		/// <param name="objDataSet"></param>
		/// <returns></returns>
		public string GetPolicyHomeCoveragesToRemove(int customerID,int polID, int polVersionID, int dwellingid, DataSet objDataSet)
		{
			StringBuilder sbXML = new StringBuilder();
			
			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtLimits = objDataSet.Tables[1];
			DataTable dtHome = objDataSet.Tables[2];
			int policyType = 0;
			int stateID = 0;

			if ( dtHome.Rows[0]["STATE_ID"] != System.DBNull.Value )
			{
				stateID = Convert.ToInt32(dtHome.Rows[0]["STATE_ID"]);
			}
			

			if ( dtHome.Rows[0]["POLICY_TYPE"] != System.DBNull.Value )
			{
				policyType = Convert.ToInt32(dtHome.Rows[0]["POLICY_TYPE"]);
			}
	
			sbXML.Append("<Coverages>");

			string xml = this.GetHomeCoveragesToRemoveFromXML(Convert.ToInt32(stateID),Convert.ToInt32(policyType),objDataSet);
	
			sbXML.Append(xml);
			
			sbXML.Append("</Coverages>");
			
			return sbXML.ToString();


		}
		
		/// <summary>
		/// Gets the mandatory section 2 coverages for policy
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <param name="dwellingid"></param>
		/// <param name="objDataSet"></param>
		/// <returns></returns>
		public string GetSection2MandatoryCoveragesForPolicy(int customerID,int polID, 
			int polVersionID, int dwellingid, DataSet objDataSet)
		{
			//Get Coveage/Limits info
			//DataSet ds = this.GetVehicleInfo(customerID,appID,appVersionID,dwellingid);
			//DataTable dtHome = ds.Tables[0];
	
			StringBuilder sbXML = new StringBuilder();
			
			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtLimits = objDataSet.Tables[1];
			DataTable dtHome = objDataSet.Tables[2];
			DataTable dtCoApplicant= objDataSet.Tables[7];

			DataTable dtSection1 = null;

			if ( objDataSet.Tables.Count == 7 )
			{
				dtSection1 = objDataSet.Tables[6];
			}

			string product = "";
			int policyType = 0;
			string covID="";
			string mand ="";
			string  covCode="";
			int stateID = 0;

	
			if ( dtHome.Rows.Count > 0 )
			{
				if ( dtHome.Rows[0]["LOOKUP_VALUE_CODE"] != System.DBNull.Value )
				{
					product = dtHome.Rows[0]["LOOKUP_VALUE_CODE"].ToString();
				}

				if ( dtHome.Rows[0]["POLICY_TYPE"] != System.DBNull.Value )
				{
					policyType = Convert.ToInt32(dtHome.Rows[0]["POLICY_TYPE"]);
				}

				if ( dtHome.Rows[0]["STATE_ID"] != System.DBNull.Value )
				{
					stateID = Convert.ToInt32(dtHome.Rows[0]["STATE_ID"]);
				}

			}
			
			sbXML.Append("<Coverages>");

			if(dtCoApplicant.Rows.Count >0)
			{
				if(int.Parse(dtCoApplicant.Rows[0]["Occupation"].ToString())>0)
				{
					DataRow[] dr = dtCoverage.Select("COV_ID = 274 or COV_ID = 275");
					
					if ( dr != null  && dr.Length > 0 )
					{
					
						covID = dr[0]["COV_ID"].ToString();
						foreach(DataRow dr1 in dr)
						{
							covID = dr1["COV_ID"].ToString();
							mand="Y";
							covCode=dr1["COV_CODE"].ToString();
							sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"" +covCode+"\" Remove=\"Y\" Mandatory=\""+mand+"\">");
							sbXML.Append("</Coverage>");
						}

					}


				}
			}
 
			
			DataRow[] drHO17 = dtCoverage.Select("COV_ID IN (902,903)");
					
			if ( drHO17 != null  && drHO17.Length > 0 )
			{
				covID = drHO17[0]["COV_ID"].ToString();
				foreach(DataRow dr1 in drHO17)
				{
					covID = dr1["COV_ID"].ToString();
					mand = "Y";
					covCode=dr1["COV_CODE"].ToString();
					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"" +covCode+"\" Remove=\"N\" Mandatory=\""+mand+"\">");
					sbXML.Append("</Coverage>");
				}
			}

			// Make mandatory for HO-5 Indiana
			//Personal Injury (HO-82)
			//HO-5///////////////////////////////////////////////////////////////////////////////	
			if ( stateID == 14 )
			{
				if ( policyType ==  11401 ||  policyType == 11149 )
				{
					DataRow[] dr = dtCoverage.Select("COV_ID IN (274,275)");
					
					if ( dr != null  && dr.Length > 0 )
					{
						covID = dr[0]["COV_ID"].ToString();
						foreach(DataRow dr1 in dr)
						{
							covID = dr1["COV_ID"].ToString();
							mand = "Y";
							covCode=dr1["COV_CODE"].ToString();
							sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"" +covCode+"\" Remove=\"N\" Mandatory=\""+mand+"\">");
							sbXML.Append("</Coverage>");
						}

					}
					
				}

				//For HO-3, if HO-24 is selected, make it mandatory
				//If HO-24 is selected for HO-3, then Personal Injury (HO-82) is disabled and checked
				if ( policyType == 11148 )
				{
					if ( dtSection1 != null && dtSection1.Rows.Count > 0 )
					{
						DataRow[] dr82 = dtSection1.Select("COV_ID=143");

						if ( dr82.Length > 0 )
						{
							DataRow[] drMand = dtCoverage.Select("COV_ID = 274");
					
							foreach(DataRow dr1 in drMand)
							{
								covID = dr1["COV_ID"].ToString();
								mand = "Y";
								covCode=dr1["COV_CODE"].ToString();
								sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"" +covCode+"\" Remove=\"N\" Mandatory=\"" + mand + "\">");
								sbXML.Append("</Coverage>");
							}	
						}

					}
				}
				//////////////
				
			}
			
			sbXML.Append("</Coverages>");

			return sbXML.ToString();
		}




		#endregion
		
		/// <summary>
		/// Updates application coverages and endorsements
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="dwellingID"></param>
		/// <param name="objWrapper"></param>
		public void UpdateAppCoveragesAndEndorsements(int customerID, int appID, 
						int appVersionID, int dwellingID,string coverageType, DataWrapper objWrapper)
		{
			if ( objWrapper.CommandParameters.Length > 0 )
			{
				objWrapper.ClearParameteres();
			}



			if(coverageType!="S2")
			{
				objWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objWrapper.AddParameter("@APP_ID",appID);
				objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);	
				objWrapper.AddParameter("@DWELLING_ID",dwellingID);
				
				objWrapper.ExecuteNonQuery("Proc_UPDATE_DWELLING_LINKED_COVERAGES");
			}

				objWrapper.ClearParameteres();
				
				objWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objWrapper.AddParameter("@APP_ID",appID);
				objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);	
				objWrapper.AddParameter("@DWELLING_ID",dwellingID);
				
				objWrapper.ExecuteNonQuery("Proc_UPDATE_DWELLING_ENDORSEMENTS_FROM_COVERAGES");
			
		}
		
		/// <summary>
		/// Updates application coverages and endorsements
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polID"></param>
		/// <param name="dwellingID"></param>
		/// <param name="objWrapper"></param>
		public void UpdatePolicyCoveragesAndEndorsements(int customerID, int polID, 
			int polVersionID, int dwellingID,string coverageType, DataWrapper objWrapper)
		{
			if ( objWrapper.CommandParameters.Length > 0 )
			{
				objWrapper.ClearParameteres();
			}

			if(coverageType != "S2")
			{
				objWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objWrapper.AddParameter("@POL_ID",polID);
				objWrapper.AddParameter("@POL_VERSION_ID",polVersionID);	
				objWrapper.AddParameter("@DWELLING_ID",dwellingID);
				objWrapper.ExecuteNonQuery("Proc_UPDATE_DWELLING_LINKED_COVERAGES_FOR_POLICY");
			}

			objWrapper.ClearParameteres();
	
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",polID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);	
			objWrapper.AddParameter("@DWELLING_ID",dwellingID);
				
			objWrapper.ExecuteNonQuery("Proc_UPDATE_DWELLING_ENDORSEMENTS_FROM_COVERAGES_POLICY");
		}
		//added by pravesh on 14 jan 2008 to save default coverages from database which are entered from system 
		protected override  void SaveDefaultCoveragesFromDB(DataWrapper objDataWrapper,int CustomerId,int AppPolId,int VersionId,int RiskId,string CalledFor)
		{
			
			string strStoredProc="";
			objDataWrapper.ClearParameteres();
			if (CalledFor=="APP")
			{
				strStoredProc="Proc_SAVE_HOME_DEFAULT_COVERAGES";
				objDataWrapper.AddParameter("@APP_ID",AppPolId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",VersionId);
				objDataWrapper.AddParameter("@DWELLING_ID",RiskId);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objDataWrapper.AddParameter("@CREATED_BY",this.createdby); 
				objDataWrapper.ExecuteNonQuery(strStoredProc);
			}
			else if(CalledFor=="POLICY")
			{
				strStoredProc="Proc_SAVE_HOME_DEFAULT_COVERAGES_POLICY";
				objDataWrapper.AddParameter("@POLICY_ID",AppPolId);
				objDataWrapper.AddParameter("@POL_VERSION_ID",VersionId);
				objDataWrapper.AddParameter("@DWELLING_ID",RiskId);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objDataWrapper.AddParameter("@CREATED_BY",this.createdby); 
				objDataWrapper.ExecuteNonQuery(strStoredProc);
			}

			objDataWrapper.ClearParameteres();
		}
		//Added By Ravindra Gupta
		protected override void SaveCoverageApp(DataWrapper objDataWrapper, int CustomerId, int AppId, int AppVersionId, int RiskId, Coverage cov)
		{
			string strStoredProc="Proc_SAVE_HOME_COVERAGES";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@APP_ID",AppId);
			objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionId);
			objDataWrapper.AddParameter("@DWELLING_ID",RiskId);
			objDataWrapper.AddParameter("@COVERAGE_ID",-1);
			objDataWrapper.AddParameter("@COVERAGE_CODE_ID",-1);
			objDataWrapper.AddParameter("@COVERAGE_TYPE",DBNull.Value );
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
			if(cov.AD_DEDUCTIBLE_AMOUNT  !=0)
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_AMOUNT",cov.AD_DEDUCTIBLE_AMOUNT  );
			}
			else
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_AMOUNT",DBNull.Value);
			}


			objDataWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",cov.LIMIT1_TEXT );
			objDataWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",cov.LIMIT2_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",cov.DEDUCTIBLE1_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",cov.DEDUCTIBLE2_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE_TEXT",cov.AD_DEDUCTIBLE_TEXT);
			//added by pravesh for Transaction log while default coverages saved
			//Get The Lob Id
			//ClsGeneralInformation obj=new ClsGeneralInformation();
			//string strlob="";
			//strlob=obj.Fun_GetLObID(CustomerId,AppId ,AppVersionId);
			int Cov_ID=objDataWrapper.ExecuteNonQuery(strStoredProc); 
			Cms.Model.Application.ClsCoveragesInfo objNew = new ClsCoveragesInfo();
			if (Cov_ID >=0)
			{
				base.PupulateCoverageModel(objNew,cov); 
				objNew.COVERAGE_CODE_ID=Cov_ID;
				objNew.APP_ID =AppId;
				objNew.APP_VERSION_ID =AppVersionId;
				objNew.CUSTOMER_ID	= CustomerId; 
				objNew.CREATED_BY=this.createdby ;
				if (objNew.CREATED_BY==0)
					objNew.CREATED_BY= objNew.MODIFIED_BY = int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());      
		
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				string strTranXML="";
				objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/homeowners/Coverages_Section1.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objNew);

//				objTransactionInfo.TRANS_TYPE_ID	=	2;
//				objTransactionInfo.APP_ID = objNew.APP_ID;
//				objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
//				objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
//				objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
//				if(strlob == ((int)enumLOB.HOME).ToString())
//					objTransactionInfo.TRANS_DESC		=	"Homeowner coverages modified.";
//				else
//					objTransactionInfo.TRANS_DESC		=	"Rental coverages modified.";
//				objTransactionInfo.CUSTOM_INFO		=	"Dwelling Id= " + RiskId.ToString()  + " and Coverage Code=" + objNew.COVERAGE_CODE ;
//				objTransactionInfo.CHANGE_XML		=	strTranXML;
				if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
					sbDefaultTranXML.Append(strTranXML);	
				//objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				//end here
				//objDataWrapper.ExecuteNonQuery("Proc_SAVE_HOME_COVERAGES"); 
			}
			objDataWrapper.ClearParameteres();
			
		}

		protected override void DeleteCoverageApp(DataWrapper objDataWrapper, int CustomerId, int AppId, int AppVersionId, int RiskId, string strCov_Code)
		{
			string strStoredProc="Proc_DeleteHomeCoverage";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@APP_ID",AppId);
			objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionId);
			objDataWrapper.AddParameter("@DWELLING_ID",RiskId);
			objDataWrapper.AddParameter("@COVERAGE_CODE",strCov_Code);
			//added by pravesh for Transaction log while default coverages saved
			//Get The Lob Id
			//ClsGeneralInformation obj=new ClsGeneralInformation();
			//string strlob="";
			//strlob=obj.Fun_GetLObID(CustomerId,AppId ,AppVersionId);
			Cms.Model.Application.ClsCoveragesInfo objNew = new ClsCoveragesInfo();
			objNew.APP_ID =AppId;
			objNew.APP_VERSION_ID =AppVersionId;
			objNew.CUSTOMER_ID	= CustomerId; 
			objNew.COVERAGE_CODE =strCov_Code;
			objNew.CREATED_BY=this.createdby ;
			if (objNew.CREATED_BY==0)
				objNew.CREATED_BY= objNew.MODIFIED_BY = int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());      
		
//			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//			string strTranXML="";
//			objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/homeowners/Coverages_Section1.aspx.resx");
//			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
//			strTranXML = objBuilder.GetDeleteTransactionLogXML(objNew);

//			objTransactionInfo.TRANS_TYPE_ID	=	2;
//			objTransactionInfo.APP_ID = objNew.APP_ID;
//			objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
//			objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
//			objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
//			if(strlob == ((int)enumLOB.HOME).ToString())
//				objTransactionInfo.TRANS_DESC		=	"Homeowner coverages deleted.";
//			else
//				objTransactionInfo.TRANS_DESC		=	"Rental coverages deleted.";
//			objTransactionInfo.CUSTOM_INFO		=	"Dwelling Id= " + RiskId.ToString()  + " and Coverage Code=" + objNew.COVERAGE_CODE ;
//			objTransactionInfo.CHANGE_XML		=	strTranXML;
//			if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
//				sbDefaultTranXML.Append(strTranXML);	
			objDataWrapper.ExecuteNonQuery(strStoredProc);// ,objTransactionInfo);
			//end here
			//objDataWrapper.ExecuteNonQuery("Proc_DeleteHomeCoverage"); 
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
			ClsGeneralInformation obj=new ClsGeneralInformation();
			string strlob="";
			strlob=obj.Fun_GetLObID(CustomerId,AppId ,AppVersionId);
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			string strTranXML=sbDefaultTranXML.ToString();
			if (strTranXML !="<root></root>")
			{	
				
				if(strlob == ((int)enumLOB.HOME).ToString())
				{
					objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/homeowners/Coverages_Section1.aspx.resx");
				}
				else
				{
					objNew.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/HomeOwners/RentalCoverages.aspx.resx");		   
				}
				objTransactionInfo.TRANS_TYPE_ID	=	2;
				objTransactionInfo.APP_ID = objNew.APP_ID;
				objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
				objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
				objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
				if(strlob == ((int)enumLOB.HOME).ToString())
					objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1733","");//"Homeowner coverages modified.";
				else
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1734", "");// "Rental coverages modified.";
				objTransactionInfo.CUSTOM_INFO		=	"Dwelling #= " + RiskId.ToString();  //+ " and Coverage Code=" + objNew.COVERAGE_CODE ;
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
			string strlob="";
			strlob=	ClsCommon.GetPolicyLOBID(CustomerId,PolId ,PolVersionId).ToString() ;  
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
				objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/homeowner/PolicyCoverages_Section1.aspx.resx");

				objTransactionInfo.TRANS_TYPE_ID	=	2;
				objTransactionInfo.POLICY_ID = objNew.POLICY_ID;
				objTransactionInfo.POLICY_VER_TRACKING_ID = objNew.POLICY_VERSION_ID;
				objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
				objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
				if(strlob == ((int)enumLOB.HOME).ToString())
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1733", "");//"Homeowner coverages modified.";
				else
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1734", "");//"Rental coverages modified.";
				objTransactionInfo.CUSTOM_INFO		=	"Dwelling #= " + RiskId.ToString();  //+ " and Coverage Code=" + objNew.COVERAGE_CODE ;
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
		protected override void UpdateCoverageApp(DataWrapper objDataWrapper, int CustomerId, int AppId, int AppVersionId, int RiskId, Coverage cov)
		{
			string strStoredProc="Proc_UpdateHomeCoverage";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@APP_ID",AppId);
			objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionId);
			objDataWrapper.AddParameter("@DWELLING_ID",RiskId);
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
			//Get The Lob Id
			int Cov_Id=	objDataWrapper.ExecuteNonQuery(strStoredProc);
			ClsGeneralInformation obj=new ClsGeneralInformation();
			//string strlob="";
			//strlob=obj.Fun_GetLObID(CustomerId,AppId,AppVersionId);
			Cms.Model.Application.ClsCoveragesInfo objNew = new ClsCoveragesInfo();
			if (Cov_Id>=0)
			{
				base.PupulateCoverageModel(objNew,cov); 
				objNew.COVERAGE_CODE_ID =Cov_Id; 
				objNew.APP_ID =AppId;
				objNew.APP_VERSION_ID =AppVersionId;
				objNew.CUSTOMER_ID	= CustomerId; 
				objNew.CREATED_BY=this.createdby ;
				if (objNew.CREATED_BY==0)
					objNew.CREATED_BY= objNew.MODIFIED_BY = int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());      
		
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				string strTranXML="";
				objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/homeowners/Coverages_Section1.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objNew);

//				objTransactionInfo.TRANS_TYPE_ID	=	2;
//				objTransactionInfo.APP_ID = objNew.APP_ID;
//				objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
//				objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
//				objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
//				if(strlob == ((int)enumLOB.HOME).ToString())
//					objTransactionInfo.TRANS_DESC		=	"Homeowner coverages updated.";
//				else
//					objTransactionInfo.TRANS_DESC		=	"Rental coverages updated.";
//				objTransactionInfo.CUSTOM_INFO		=	"Dwelling Id= " + RiskId.ToString()  + " and Coverage Code=" + objNew.COVERAGE_CODE ;
//				objTransactionInfo.CHANGE_XML		=	strTranXML;
				if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
					sbDefaultTranXML.Append(strTranXML);
				//objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				//end here
				//objDataWrapper.ExecuteNonQuery("Proc_UpdateHomeCoverage");  
			}
			objDataWrapper.ClearParameteres();
		}
		
		
		protected override void UpdateEndorsmentApp(DataWrapper objDataWrapper, int CustomerId, int AppId, int AppVersionId, int RiskId)
		{
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@APP_ID",AppId);
			objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionId);
			objDataWrapper.AddParameter("@DWELLING_ID",RiskId);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.ExecuteNonQuery("Proc_UPDATE_HOME_ENDORSEMENTS");
			objDataWrapper.ClearParameteres();
		}

		public override DataTable GetRisksForLobApp(int CustomerId, int AppId, int AppVersionId, int RiskId)
		{
			SqlParameter[] sqlParams = new SqlParameter[3];
			
			sqlParams[0] = new SqlParameter("@APP_ID",AppId);
			sqlParams[1] = new SqlParameter("@APP_VERSION_ID",AppVersionId );
			sqlParams[2] = new SqlParameter("@CUSTOMER_ID",CustomerId);
			DataSet ds = DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,"Proc_FetchAllDwellingsAPP",sqlParams);

			return ds.Tables[0];
				
		}

		protected override void SaveCoveragePolicy(DataWrapper objDataWrapper, int CustomerId, int PolicyId, int PolicyVersionId, int RiskId, Coverage cov)
		{
			string strStoredProc="Proc_SavePolicyHomeCoverage";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@POLICY_ID",PolicyId);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionId);
			objDataWrapper.AddParameter("@DWELLING_ID",RiskId);
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
            
			if(cov.AD_DEDUCTIBLE_AMOUNT  !=0)
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_AMOUNT",cov.AD_DEDUCTIBLE_AMOUNT  );
			}
			else
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_AMOUNT",DBNull.Value);
			}

			objDataWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",cov.LIMIT1_TEXT );
			objDataWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",cov.LIMIT2_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",cov.DEDUCTIBLE1_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",cov.DEDUCTIBLE2_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE_TEXT",cov.AD_DEDUCTIBLE_TEXT);
			//added by pravesh for Transaction log while default coverages saved
			//Get The Lob Id
//			string strlob="";
//				strlob=	ClsCommon.GetPolicyLOBID(CustomerId,PolicyId,PolicyVersionId).ToString() ;  
			Cms.Model.Policy.ClsPolicyCoveragesInfo objNew = new Cms.Model.Policy.ClsPolicyCoveragesInfo ();
			base.PupulateCoverageModel(objNew,cov); 
			objNew.CUSTOMER_ID	= CustomerId; 
			objNew.CREATED_BY= objNew.MODIFIED_BY = this.createdby ;
			if (objNew.CREATED_BY==0)
				objNew.CREATED_BY= objNew.MODIFIED_BY = int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());      
		
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			string strTranXML="";
			objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/homeowner/PolicyCoverages_Section1.aspx.resx");
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			strTranXML = objBuilder.GetTransactionLogXML(objNew);

//			objTransactionInfo.TRANS_TYPE_ID	=	2;
//			objTransactionInfo.POLICY_ID  = PolicyId ;
//			objTransactionInfo.POLICY_VER_TRACKING_ID =PolicyVersionId ;
//			objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
//			objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
//			if(strlob == ((int)enumLOB.HOME).ToString())
//				objTransactionInfo.TRANS_DESC		=	"Homeowner coverages modified.";
//			else
//				objTransactionInfo.TRANS_DESC		=	"Rental coverages modified.";
//			objTransactionInfo.CUSTOM_INFO		=	"Dwelling Id= " + RiskId.ToString()  + " and Coverage Code=" + objNew.COVERAGE_CODE ;
//			objTransactionInfo.CHANGE_XML		=	strTranXML;
			if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
				sbDefaultTranXML.Append(strTranXML);	
			objDataWrapper.ExecuteNonQuery(strStoredProc);//,objTransactionInfo);
			//end here
			//objDataWrapper.ExecuteNonQuery("Proc_SavePolicyHomeCoverage"); 
			objDataWrapper.ClearParameteres();
		}
		protected override void DeleteCoveragePolicy(DataWrapper objDataWrapper, int CustomerId, int PolicyId, int PolicyVersionId, int RiskId, string strCov_Code)
		{
			string strStoredProc="Proc_DeletePolicyHomeCoverage";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@POLICY_ID",PolicyId);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionId);
			objDataWrapper.AddParameter("@DWELLING_ID",RiskId);
			objDataWrapper.AddParameter("@COVERAGE_CODE",strCov_Code);
			//added by pravesh for Transaction log while default coverages saved
			//Get The Lob Id
			string strlob="";
			strlob=	ClsCommon.GetPolicyLOBID(CustomerId,PolicyId,PolicyVersionId).ToString() ;  
			Cms.Model.Policy.ClsPolicyCoveragesInfo objNew = new Cms.Model.Policy.ClsPolicyCoveragesInfo (); 
			objNew.CUSTOMER_ID	= CustomerId; 
			objNew.COVERAGE_CODE =strCov_Code;
			objNew.CREATED_BY= objNew.MODIFIED_BY = this.createdby ;
			if (objNew.CREATED_BY==0)
				objNew.CREATED_BY= objNew.MODIFIED_BY = int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());      
		
//			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//			string strTranXML="";
//			objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/homeowner/PolicyCoverages_Section1.aspx.resx");
//			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
//			strTranXML = objBuilder.GetDeleteTransactionLogXML(objNew);

//			objTransactionInfo.TRANS_TYPE_ID	=	2;
//			objTransactionInfo.POLICY_ID  = PolicyId ;
//			objTransactionInfo.POLICY_VER_TRACKING_ID =PolicyVersionId ;
//			objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
//			objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
//			if(strlob == ((int)enumLOB.HOME).ToString())
//				objTransactionInfo.TRANS_DESC		=	"Homeowner coverages deleted.";
//			else
//				objTransactionInfo.TRANS_DESC		=	"Rental coverages deleted.";
//			objTransactionInfo.CUSTOM_INFO		=	"Dwelling Id= " + RiskId.ToString()  + " and Coverage Code=" + objNew.COVERAGE_CODE ;
//			objTransactionInfo.CHANGE_XML		=	strTranXML;
//			if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
//				sbDefaultTranXML.Append(strTranXML);	
			objDataWrapper.ExecuteNonQuery(strStoredProc);//,objTransactionInfo);
			//end here
			//objDataWrapper.ExecuteNonQuery("Proc_DeletePolicyHomeCoverage"); 
			objDataWrapper.ClearParameteres();
		}
		protected override void UpdateCoveragePolicy(DataWrapper objDataWrapper, int CustomerId, int PolicyId, int PolicyVersionId, int RiskId, Coverage cov)
		{
			string strStoredProc="Proc_UpdatePolicyHomeCoverage";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@POLICY_ID",PolicyId);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionId);
			objDataWrapper.AddParameter("@DWELLING_ID",RiskId);
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
			//Get The Lob Id
//			string strlob="";
//			strlob=	ClsCommon.GetPolicyLOBID(CustomerId,PolicyId,PolicyVersionId).ToString() ;  
			Cms.Model.Policy.ClsPolicyCoveragesInfo objNew = new Cms.Model.Policy.ClsPolicyCoveragesInfo ();
			base.PupulateCoverageModel(objNew,cov); 
			objNew.CUSTOMER_ID	= CustomerId; 
			objNew.CREATED_BY= objNew.MODIFIED_BY = this.createdby ;
			if (objNew.CREATED_BY==0)
				objNew.CREATED_BY= objNew.MODIFIED_BY = int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());      
		
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			string strTranXML="";
			objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/homeowner/PolicyCoverages_Section1.aspx.resx");
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			strTranXML = objBuilder.GetTransactionLogXML(objNew);

//			objTransactionInfo.TRANS_TYPE_ID	=	2;
//			objTransactionInfo.POLICY_ID  = PolicyId ;
//			objTransactionInfo.POLICY_VER_TRACKING_ID =PolicyVersionId ;
//			objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
//			objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
//			if(strlob == ((int)enumLOB.HOME).ToString())
//				objTransactionInfo.TRANS_DESC		=	"Homeowner coverages updated.";
//			else
//				objTransactionInfo.TRANS_DESC		=	"Rental coverages updated.";
//			objTransactionInfo.CUSTOM_INFO		=	"Dwelling Id= " + RiskId.ToString()  + " and Coverage Code=" + objNew.COVERAGE_CODE ;
//			objTransactionInfo.CHANGE_XML		=	strTranXML;
			if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
				sbDefaultTranXML.Append(strTranXML);	
			objDataWrapper.ExecuteNonQuery(strStoredProc);//,objTransactionInfo);
			//end here
			//objDataWrapper.ExecuteNonQuery("Proc_UpdatePolicyHomeCoverage"); 
			objDataWrapper.ClearParameteres();
		}

		protected override void UpdateEndorsmentPolicy(DataWrapper objDataWrapper, int CustomerId, int PolicyId, int PolicyVersionId, int RiskId)
		{
			//Proc_UPDATE_HOME_ENDORSEMENTS_FOR_POLICY
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@POL_ID",PolicyId);
			objDataWrapper.AddParameter("@POL_VERSION_ID",PolicyVersionId);
			objDataWrapper.AddParameter("@DWELLING_ID",RiskId);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.ExecuteNonQuery("Proc_UPDATE_HOME_ENDORSEMENTS_FOR_POLICY");
			objDataWrapper.ClearParameteres();
		}

		public override DataTable GetRisksForLobPolicy(int CustomerId, int PolicyId, int PolicyVersionId, int RiskId)
		{
			SqlParameter[] sqlParams = new SqlParameter[3];
			
			sqlParams[0] = new SqlParameter("@POLICY_ID",PolicyId);
			sqlParams[1] = new SqlParameter("@POLICY_VERSION_ID",PolicyVersionId );
			sqlParams[2] = new SqlParameter("@CUSTOMER_ID",CustomerId);
			DataSet ds = DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,"Proc_FetchAllDwellingsPOL",sqlParams);

			return ds.Tables[0];
		}



		public RuleData GetRuleData(int productID, int CustomerId, int Id, int VersionId, int RiskId,string strLevel)
		{
			StringBuilder sbOnBlur = new StringBuilder();
			StringBuilder sbOnClick = new StringBuilder();
			string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/Coverages/HomeCoverages.xml");
			RuleData homeRuleData = new RuleData();
			XmlDocument  tempDoc= new XmlDocument();
			if(HomeRemoveXMLLoaded == false)
			{
				homeRemoveXml.Load(filePath); 
				HomeRemoveXMLLoaded=true; 
			}
			tempDoc = base.RuleDoc;
			base.RuleDoc = homeRemoveXml;

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			base.InitialiseRules(objDataWrapper,CustomerId,Id ,VersionId,RiskId,strLevel);
			//Coverage cov = new Coverage();
			
			
			XmlNode node = homeRemoveXml.SelectSingleNode("Root/State[@ID=" +  base.StateId + "]");
	
			if ( node == null ) return homeRuleData ;

			XmlNode productNode = node.SelectSingleNode("Product[@ID=" + productID.ToString() + "]");

			if ( productNode == null ) return homeRuleData ;
			
			ArrayList masterRuleNodeArray = GetEffectiveMasterRules(productNode,base.AppEffectiveDate,base.StateId ); 
			SetMasterKeys(masterRuleNodeArray);

			XmlNode baseNode = productNode.SelectSingleNode("BaseCoverage");
			if(baseNode !=null)
			{
				homeRuleData.BaseCoverage = baseNode.Attributes["Code"].Value;
				
				string strMinVal = baseNode.Attributes["MinValue"].Value;
				string strMaxVal = baseNode.Attributes["MaxValue"].Value;

				if(strMinVal.StartsWith("$"))
				{
					strMinVal = strMinVal.Substring(strMinVal.IndexOf('$')+1);
					homeRuleData.MinValue = coverageKeys[strMinVal].ToString();
				}
				if(strMaxVal.StartsWith("$"))
				{
					strMaxVal = strMaxVal.Substring(strMaxVal.IndexOf('$')+1);
					homeRuleData.MaxValue = coverageKeys[strMaxVal].ToString();
				}

			}
			
			string strGMultiplier = "globalMultiplier";
			sbOnBlur.Append(" var " + strGMultiplier +  " =0;");
			sbOnBlur.Append(" function OnCoverageChange() { var sourceCov='");
			sbOnBlur.Append(homeRuleData.BaseCoverage );
			sbOnBlur.Append("' ;var source = GetControlInGridFromCode(sourceCov,'_txtLIMIT');");

			sbOnBlur.Append(@"  var out = ',';var add = ''; var temp = '' + source.value;"); 
			sbOnBlur.Append(@"while (temp.indexOf(out)>-1){ pos= temp.indexOf(out);temp = '' + (temp.substring(0, pos)"); 
			sbOnBlur.Append(@"+ add + temp.substring((pos + out.length), temp.length));}var sourceVal=ReplaceAll(temp,'$','');");		    
			sbOnBlur.Append(@"source.value=formatCurrency(sourceVal);");

			XmlNode onChangeNode = productNode.SelectSingleNode("OnChange");
			XmlNodeList targetNodeArray = onChangeNode.SelectNodes("Target");
			int intCounter=1;
			foreach(XmlNode targetNode in targetNodeArray)
			{
				//Code="OS" Operator="*" Operand="$MULT_B">
				bool ExternalDependent = false;
				string  strCode = targetNode.Attributes["Code"].Value;
				string strOperator = targetNode.Attributes["Operator"].Value;
				string strMultiplier = targetNode.Attributes["Operand"].Value;
				if(strMultiplier.StartsWith("$"))
				{
					strMultiplier = strMultiplier.Substring(strMultiplier.IndexOf('$')+1);
					strMultiplier = coverageKeys[strMultiplier].ToString();
				}
				string strVarTargetCode = "targetCode_"  + intCounter.ToString();
				string strVarTarget = "target_" + intCounter.ToString();
				string strVarResult = "result_" + intCounter.ToString();
				sbOnBlur.Append("var  " + strVarTargetCode);
				sbOnBlur.Append(" ='" + strCode + "'; ");
				sbOnBlur.Append("var  " + strVarTarget + " =GetControlInGridFromCode(" + strVarTargetCode +  ",'_txtLIMIT');"); 
				
				sbOnBlur.Append("var  " + strVarResult + ";" );
				if(targetNode.Attributes["ExternalDependency"] != null)
				{
					string strExternalDep = targetNode.Attributes["ExternalDependency"].Value;
					if(strExternalDep == "True")
					{
						sbOnBlur.Append("if(" + strGMultiplier + " == 0){" ); 
						sbOnBlur.Append( strGMultiplier + " = " + strMultiplier + "; }");
						sbOnBlur.Append( strVarResult + " = parseInt(sourceVal) ");
						sbOnBlur.Append(strOperator + "  ");
						sbOnBlur.Append(strGMultiplier + ";");
						ExternalDependent=true;
					}
					else
					{
						sbOnBlur.Append( strVarResult + " = parseInt(sourceVal) ");
						sbOnBlur.Append(strOperator + "  ");
						sbOnBlur.Append(strMultiplier + ";");
					}
				}
				else
				{
					sbOnBlur.Append( strVarResult + " = parseInt(sourceVal) ");
					sbOnBlur.Append(strOperator + "  ");
					sbOnBlur.Append(strMultiplier + ";");
				}

				if(targetNode.Attributes["IF"] != null)
				{
					if(targetNode.Attributes["IF"].Value == "Y")
					{
						string strConOpr   = targetNode.Attributes["ConOpr"].Value;
						string strConVal   = targetNode.Attributes["ConVal"].Value;
						string strToSetVal = targetNode.Attributes["ToSetVal"].Value;

						sbOnBlur.Append(" if(" + strVarResult + " "  +  strConOpr + " " + strConVal + " )");
						sbOnBlur.Append("{ " + strVarResult + " = " + strToSetVal  + ";}");
					}
				}

				
				sbOnBlur.Append(strVarTarget + ".value = formatCurrency( " + strVarResult + "); ");
				intCounter++;
				if(ExternalDependent)
				{
					XmlNode onClickNode = productNode.SelectSingleNode("OnClick");
					if(onClickNode != null)
					{
						if(onClickNode.Attributes["Code"] != null)
						{
							string strCovCode = onClickNode.Attributes["Code"].Value;
							homeRuleData.OnClickCoverage = strCovCode;
							sbOnClick.Append(" function onDependentClick() { var cbCov='");
							sbOnClick.Append(strCovCode);
							sbOnClick.Append("' ;var cb = GetControlInGridFromCode(cbCov,'_cbDelete');");
						//	sbOnClick.Append("alert('Jai Mai Ki');");

						}
						
						XmlNode checkedNode = onClickNode.SelectSingleNode("Checked");
						if(checkedNode != null)
						{
							XmlNode varSetNode = checkedNode.SelectSingleNode("VarSet");
							string strVariable = varSetNode.Attributes["Varialbe"].Value;
							string strValue = varSetNode.Attributes["Value"].Value;
							sbOnClick.Append(@" if ( cb.checked == true ){ ");
							sbOnClick.Append(strVariable + "=" + strValue + " ;}");
						}

						XmlNode uncheckedNode = onClickNode.SelectSingleNode("Unchecked");	
						if(uncheckedNode != null)
						{
							XmlNode varSetNode = uncheckedNode.SelectSingleNode("VarSet");
							string strVariable = varSetNode.Attributes["Varialbe"].Value;
							string strValue = varSetNode.Attributes["Value"].Value;
							sbOnClick.Append(@" if ( cb.checked == false){ ");
							sbOnClick.Append(strVariable + "=" + strValue + " ;}");
						}
						sbOnClick.Append("	OnCoverageChange();}");
						homeRuleData.OnClickFunction = sbOnClick.ToString();
					}

				}

			}

			if(productID==11405 || productID == 11195)
			{
				sbOnBlur.Append("SetRentalDelux();");

			}
			if(productID==11406 || productID == 11196)
			{
				sbOnBlur.Append("SetCondominiumDelux();");
			}
			if(productID==11148)
			{
				sbOnBlur.Append("PremierVIP24();");
			}
						
			sbOnBlur.Append("}");
			homeRuleData.OnBlurFunction=sbOnBlur.ToString();

			StringBuilder sbErr =  new StringBuilder();
			string strErrTemplate = coverageKeys["ERR_TEMPLATE"].ToString(); 
			XmlNode errNode = productNode.SelectSingleNode("ErrorMessage[@Template='" +strErrTemplate  + "']");
			XmlNodeList txtNodeArray = errNode.SelectNodes("Text");
			foreach(XmlNode txtNode in txtNodeArray)
			{
				string strPart = txtNode.Attributes["Value"].Value;
				if(strPart.StartsWith("$"))
				{
					strPart = strPart.Substring(strPart.IndexOf('$') + 1);
					strPart = coverageKeys[strPart].ToString();
					strPart = Convert.ToDecimal(strPart).ToString("N");
					strPart = strPart.Substring(0,strPart.LastIndexOf("."));
				}
				sbErr.Append(strPart);
			}
			homeRuleData.ErrorMessage= sbErr.ToString();

			errNode = productNode.SelectSingleNode("ErrRequired");
			homeRuleData.ErrRequired = errNode.Attributes["Value"].Value;
			
			base.IsInitialised=false;
			base.RuleDoc=tempDoc;
			return homeRuleData;
		}

		public RuleData GetRuleDataRental(int productID, int CustomerId, int Id, int VersionId, int RiskId,string strLevel)
		{
			StringBuilder sbOnBlur = new StringBuilder();
			StringBuilder sbOnClick = new StringBuilder();
			string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/Coverages/RentalCoverages.xml");
			RuleData homeRuleData = new RuleData();
			XmlDocument  tempDoc= new XmlDocument();
			XmlDocument rentalXml= new XmlDocument();
			rentalXml.Load(filePath); 
			tempDoc = base.RuleDoc;
			base.RuleDoc = rentalXml;

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			base.InitialiseRules(objDataWrapper,CustomerId,Id ,VersionId,RiskId,strLevel);
			//Coverage cov = new Coverage();
			
			
			XmlNode node = rentalXml.SelectSingleNode("Root/State[@ID=" +  base.StateId + "]");
	
			if ( node == null ) return homeRuleData ;

			XmlNode productNode = node.SelectSingleNode("Product[@ID=" + productID.ToString() + "]");

			if ( productNode == null ) return homeRuleData ;
			
			ArrayList masterRuleNodeArray = GetEffectiveMasterRules(productNode,base.AppEffectiveDate,base.StateId ); 
			SetMasterKeys(masterRuleNodeArray);

			XmlNode baseNode = productNode.SelectSingleNode("BaseCoverage");
			if(baseNode !=null)
			{
				homeRuleData.BaseCoverage = baseNode.Attributes["Code"].Value;
				
				string strMinVal = baseNode.Attributes["MinValue"].Value;
				string strMaxVal = baseNode.Attributes["MaxValue"].Value;

				if(strMinVal.StartsWith("$"))
				{
					strMinVal = strMinVal.Substring(strMinVal.IndexOf('$')+1);
					homeRuleData.MinValue = coverageKeys[strMinVal].ToString();
				}
				if(strMaxVal.StartsWith("$"))
				{
					strMaxVal = strMaxVal.Substring(strMaxVal.IndexOf('$')+1);
					homeRuleData.MaxValue = coverageKeys[strMaxVal].ToString();
				}

			}
			
			string strGMultiplier = "globalMultiplier";
			sbOnBlur.Append(" var " + strGMultiplier +  " =0;");
			sbOnBlur.Append(" function OnCoverageChange() { var sourceCov='");
			sbOnBlur.Append(homeRuleData.BaseCoverage );
			sbOnBlur.Append("' ;var source = GetControlInGridFromCode(sourceCov,'_txtLIMIT');");

			sbOnBlur.Append(@"  var out = ',';var add = ''; var temp = '' + source.value;"); 
			sbOnBlur.Append(@"while (temp.indexOf(out)>-1){ pos= temp.indexOf(out);temp = '' + (temp.substring(0, pos)"); 
			sbOnBlur.Append(@"+ add + temp.substring((pos + out.length), temp.length));}var sourceVal=temp;");		    
			sbOnBlur.Append(@"source.value=formatCurrency(sourceVal);");

			XmlNode onChangeNode = productNode.SelectSingleNode("OnChange");
			XmlNodeList targetNodeArray = onChangeNode.SelectNodes("Target");
			int intCounter=1;

			foreach(XmlNode targetNode in targetNodeArray)
			{
				
				bool ExternalDependent = false;
				string  strCode = targetNode.Attributes["Code"].Value;
				string strOperator = targetNode.Attributes["Operator"].Value;
				string strMultiplier = targetNode.Attributes["Operand"].Value;
				if(strMultiplier.StartsWith("$"))
				{
					strMultiplier = strMultiplier.Substring(strMultiplier.IndexOf('$')+1);
					strMultiplier = coverageKeys[strMultiplier].ToString();
				}
				string strVarTargetCode = "targetCode_"  + intCounter.ToString();
				string strVarTarget = "target_" + intCounter.ToString();
				string strVarResult = "result_" + intCounter.ToString();
				sbOnBlur.Append("var  " + strVarTargetCode);
				sbOnBlur.Append(" ='" + strCode + "'; ");
				sbOnBlur.Append("var  " + strVarTarget + " =GetControlInGridFromCode(" + strVarTargetCode +  ",'_txtLIMIT');"); 
				
				sbOnBlur.Append("var  " + strVarResult + ";" );
				if(targetNode.Attributes["ExternalDependency"] != null)
				{
					string strExternalDep = targetNode.Attributes["ExternalDependency"].Value;
					if(strExternalDep == "True")
					{
						sbOnBlur.Append("if(" + strGMultiplier + " == 0){" ); 
						sbOnBlur.Append( strGMultiplier + " = " + strMultiplier + "; }");
						sbOnBlur.Append( strVarResult + " = parseInt(sourceVal) ");
						sbOnBlur.Append(strOperator + "  ");
						sbOnBlur.Append(strGMultiplier + ";");
						ExternalDependent=true;
					}
					else
					{
						sbOnBlur.Append( strVarResult + " = parseInt(sourceVal) ");
						sbOnBlur.Append(strOperator + "  ");
						sbOnBlur.Append(strMultiplier + ";");
					}
				}
				else
				{
					sbOnBlur.Append( strVarResult + " = parseInt(sourceVal) ");
					sbOnBlur.Append(strOperator + "  ");
					sbOnBlur.Append(strMultiplier + ";");
				}

				if(targetNode.Attributes["IF"] != null)
				{
					if(targetNode.Attributes["IF"].Value == "Y")
					{
						string strConOpr   = targetNode.Attributes["ConOpr"].Value;
						string strConVal   = targetNode.Attributes["ConVal"].Value;
						string strToSetVal = targetNode.Attributes["ToSetVal"].Value;

						sbOnBlur.Append(" if(" + strVarResult + " "  +  strConOpr + " " + strConVal + " )");
						sbOnBlur.Append("{ " + strVarResult + " = " + strToSetVal  + ";}");
					}
				}

				
				sbOnBlur.Append(strVarTarget + ".value = formatCurrency( " + strVarResult + "); ");
				intCounter++;
				
				
			}
			//Mine Subsidence Coverage (DP-480) 
			if(StateId == ((int)enumState.Indiana).ToString())
			{
				sbOnBlur.Append("SetMinSubsidenceLimit();");
			}

			sbOnBlur.Append("}");
			homeRuleData.OnBlurFunction=sbOnBlur.ToString();

			StringBuilder sbErr =  new StringBuilder();
			XmlNode errNode = productNode.SelectSingleNode("ErrorMessage");
			XmlNodeList txtNodeArray = errNode.SelectNodes("Text");
			foreach(XmlNode txtNode in txtNodeArray)
			{
				string strPart = txtNode.Attributes["Value"].Value;
				if(strPart.StartsWith("$"))
				{
					strPart = strPart.Substring(strPart.IndexOf('$') + 1);
					strPart = coverageKeys[strPart].ToString();
					strPart = Convert.ToDecimal(strPart).ToString("N");
					strPart = strPart.Substring(0,strPart.LastIndexOf("."));
				}
				sbErr.Append(strPart);
			}
			homeRuleData.ErrorMessage= sbErr.ToString();

			errNode = productNode.SelectSingleNode("ErrRequired");
			homeRuleData.ErrRequired = errNode.Attributes["Value"].Value;
			
			base.IsInitialised=false;
			base.RuleDoc=tempDoc;
			return homeRuleData;
		}
		
		
	}

	
	

	
}
