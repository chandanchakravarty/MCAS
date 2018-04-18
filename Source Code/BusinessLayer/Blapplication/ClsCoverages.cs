/******************************************************************************************
<Author				: -   Pradeep Iyer
<Start Date			: -	 5/2/2005 2:35:05 PM
<End Date			: -	
<Description		: - Common base class for all coverage business classes
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		:-  04-28-2006
<Modified By		:-  Ravindra
<Purpose			:-  Added Function BindDropDown and Select Value in DropDown

<Modified Date		:-  06-06-2006
<Modified By		:-  Ravindra
<Purpose			:-  Implementation logic change for Default and Update of coverages on 
<						basis of changes in Dependencies
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Application;
using Cms.BusinessLayer.BlCommon;
using System.Web.UI.WebControls;
using System.Collections;
using Cms.Model.Application.HomeOwners;
using Cms.Model.Policy;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace Cms.BusinessLayer.BlApplication
{
	public enum RuleType
	{
		Default=1,
		RiskDependent =2,
		AppDependent =3,
		LobDependent=4,
		OtherAppDependent=5,
		ProductDependend=6,
		MiscEquipment=7,
		AutoDriverDep=8,
		UnderWriting=9,
		MakeAppDependent = 10,
		ProcessDependend = 11
	}
	public enum Level
	{
		APP=1,
		POLICY =2
	}

	public struct Coverage
	{
		public string  COV_CODE;
		public double  LIMIT1_AMOUNT;
		public string  LIMIT1_TEXT;
		public double  LIMIT2_AMOUNT;
		public string  LIMIT2_TEXT;
		public double  DEDUCTIBLE1_AMOUNT;
		public string  DEDUCTIBLE1_TEXT;
		public double  DEDUCTIBLE2_AMOUNT;
		public string  DEDUCTIBLE2_TEXT;
		public double  AD_DEDUCTIBLE_AMOUNT;
		public string  AD_DEDUCTIBLE_TEXT;
	}


	
	/// <summary>
	/// Summary description for ClsCoverages.
	/// </summary>
	public class ClsCoverages : Cms.BusinessLayer.BlApplication.clsapplication
	{
		protected bool IsInitialised;
		protected string filePath ;
		protected Hashtable coverageKeys;
		protected System.Xml.XmlDocument RuleDoc;
		protected DateTime AppEffectiveDate;
		protected string StateId;
		protected StringBuilder sbDefaultTranXML;

		public ClsCoverages()
		{
			IsInitialised=false;
			filePath="";
			coverageKeys=new Hashtable();
			RuleDoc=new XmlDocument();
			sbDefaultTranXML=new StringBuilder(); 
			sbDefaultTranXML.Append("<root>"); 
		}
		#region populate Coverage Object Model while saving Default Coverages
		/// <summary>
		/// populate Application Model
		/// </summary>
		/// <param name="objCoverageInfo"></param>
		/// <param name="cov"></param>
		public void PupulateCoverageModel(ClsCoveragesInfo objCoverageInfo,Coverage cov)
		{
			objCoverageInfo.COVERAGE_CODE		= cov.COV_CODE;
			objCoverageInfo.LIMIT_1				= cov.LIMIT1_AMOUNT;
			objCoverageInfo.LIMIT1_AMOUNT_TEXT	= cov.LIMIT1_TEXT;
			objCoverageInfo.LIMIT_2				= cov.LIMIT2_AMOUNT;
			objCoverageInfo.LIMIT2_AMOUNT_TEXT 	=	cov.LIMIT2_TEXT;
			objCoverageInfo.DEDUCTIBLE_1		= cov.DEDUCTIBLE1_AMOUNT;
			objCoverageInfo.DEDUCTIBLE1_AMOUNT_TEXT = cov.DEDUCTIBLE1_TEXT;
			objCoverageInfo.DEDUCTIBLE_2		= cov.DEDUCTIBLE2_AMOUNT;
			objCoverageInfo.DEDUCTIBLE2_AMOUNT_TEXT = cov.DEDUCTIBLE2_TEXT;
			objCoverageInfo.DEDUCTIBLE				= cov.AD_DEDUCTIBLE_AMOUNT;
			objCoverageInfo.DEDUCTIBLE_TEXT			= cov.AD_DEDUCTIBLE_TEXT;
		}
		/// <summary>
		/// Poulate Policy model
		/// </summary>
		/// <param name="objCoverageInfo"></param>
		/// <param name="cov"></param>
		public void PupulateCoverageModel(ClsPolicyCoveragesInfo objCoverageInfo,Coverage cov)
		{
			objCoverageInfo.COVERAGE_CODE		= cov.COV_CODE;
			objCoverageInfo.LIMIT_1				= cov.LIMIT1_AMOUNT;
			objCoverageInfo.LIMIT1_AMOUNT_TEXT	= cov.LIMIT1_TEXT;
			objCoverageInfo.LIMIT_2				= cov.LIMIT2_AMOUNT;
			objCoverageInfo.LIMIT2_AMOUNT_TEXT 	=	cov.LIMIT2_TEXT;
			objCoverageInfo.DEDUCTIBLE_1		= cov.DEDUCTIBLE1_AMOUNT;
			objCoverageInfo.DEDUCTIBLE1_AMOUNT_TEXT = cov.DEDUCTIBLE1_TEXT;
			objCoverageInfo.DEDUCTIBLE_2		= cov.DEDUCTIBLE2_AMOUNT;
			objCoverageInfo.DEDUCTIBLE2_AMOUNT_TEXT = cov.DEDUCTIBLE2_TEXT;
			objCoverageInfo.DEDUCTIBLE				= cov.AD_DEDUCTIBLE_AMOUNT;
			objCoverageInfo.DEDUCTIBLE_TEXT			= cov.AD_DEDUCTIBLE_TEXT;
		}
		#endregion

		#region Manipulate coverages

		/// <summary>
		/// Deletes coverages 
		/// </summary>
		/// <param name="dsCoverageHome">contains coverage details for the home lob for the customer</param>
		/// <param name="covXML">XML string</param>
		/// <returns>Dataset after deleting coverage record rows</returns>
		public DataSet DeleteCoverage(DataSet dsCoverageHome,string covXML)
		{
			DataSet dsTemp=new DataSet(); 
			XmlDocument xDoc=new XmlDocument();
			//loading XML 
			xDoc.LoadXml(covXML); 
			
			//filtering nodes accessing all coverage id where remove='Y'
			XmlNodeList xNodeList=xDoc.SelectNodes("/Coverages/Coverage[@Remove='Y']/@COV_ID"); 
			string str="";
			//copying original dataset to local dataset
			dsTemp=dsCoverageHome.Copy(); 
			foreach(XmlNode node in xNodeList)
			{
				str= node.InnerText; 

				if(dsCoverageHome!=null)
				{
					if(dsCoverageHome.Tables[0].Rows.Count>0)
					{
						//selecting rows 
						DataRow [] dRow=dsTemp.Tables[0].Select("COV_ID=" + str);    
						if(dRow!=null)
						{
							int iRow=0;
							for(iRow=0;iRow<dRow.Length;iRow++)
							{
								dsTemp.Tables[0].Rows.Remove(dRow[iRow]);   
							}
						}					
					}
				}
			}
			dsCoverageHome=dsTemp.Copy(); 	
			return dsCoverageHome;			
		}

		/// <summary>
		/// deletes options which are no longer applicable
		/// </summary>
		/// <param name="dsCoverageHome">contains coverage details for the home lob for the customer</param>
		/// <returns>Dataset after deleting coverage limits/deductibles rows</returns>
		public DataSet DeleteCoverageOptions(DataSet dsCoverageHome,string covXML)
		{
			DataSet dsTemp=new DataSet(); 
			XmlDocument xDoc=new XmlDocument();
			//xDoc.Load(Server.MapPath(Request.ApplicationPath  + "/cmsweb/xsl/quote/masterdata/productfactorsmaster_auto.xml")); 
			xDoc.LoadXml(covXML); 
			//XmlNodeList xNodeList=xDoc.SelectNodes("PRODUCTMASTER/PRODUCT[@ID='AUTO-P']/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@SAFEDRIVERCREDIT"); 
			XmlNodeList xNodeList=xDoc.SelectNodes("/Coverages/Coverage"); 
			//string str="";
			dsTemp=dsCoverageHome.Copy(); 
			foreach(XmlNode node in xNodeList)
			{
				//Response.Write(node.Attributes["COV_ID"].InnerText + "--");
				int nodeID=int.Parse(node.Attributes["COV_ID"].InnerText);
				XmlNodeList xLimitNode=node.SelectNodes("/Coverages/Coverage[@COV_ID='" + nodeID + "']/Limit[@Remove='Y']"); 
				if(xLimitNode.Count>=1)
				{					
					foreach(XmlNode xn in xLimitNode) 					
					{
						//Response.Write(xn.Attributes["id"].InnerText + "--" +  xn.Attributes["amount"].InnerText); 
						string limitID=xn.Attributes["id"].InnerText ;

						if(dsCoverageHome!=null)
						{
							if(dsCoverageHome.Tables[1].Rows.Count>0)
							{
							
								DataRow [] dRow=dsTemp.Tables[1].Select("COV_ID=" + nodeID + " and LIMIT_DEDUC_ID=" + limitID);    
								if(dRow!=null)
								{
									int iRow=0;
									for(iRow=0;iRow<dRow.Length;iRow++)
									{
										dsTemp.Tables[1].Rows.Remove(dRow[iRow]);   
									}
								}					
							}
						}
					}
					
				}
				//Response.Write("<br>");   
				//Response.Write(node.FirstChild.InnerText);   
				//str+= node.InnerText; 
			}
			dsCoverageHome=dsTemp.Copy(); 	
			return dsCoverageHome;
			
			
		}

		/// <summary>
		/// setting mandatory/non-mandatory flag for the coverage
		/// </summary>
		/// <param name="dsCoverageHome">contains coverage details for the home lob for the customer</param>
		/// <returns>Dataset after updating coverage mandatory columns</returns>
		public DataSet UpdateCoverageMandatory(DataSet dsCoverageHome,string covXML)
		{
			DataSet dsTemp=new DataSet(); 
			XmlDocument xDoc=new XmlDocument();
			//xDoc.Load(Server.MapPath(Request.ApplicationPath  + "/cmsweb/xsl/quote/masterdata/productfactorsmaster_auto.xml")); 
			xDoc.LoadXml(covXML); 
			XmlNodeList xNodeList=xDoc.SelectNodes("/Coverages/Coverage"); 
			string str="";
			dsTemp=dsCoverageHome.Copy(); 
			int iDs=0;	
			if(dsTemp!=null)
			{
				for(iDs=0;iDs<dsTemp.Tables[0].Rows.Count;iDs++)
				{
					foreach(XmlNode node in xNodeList)
					{
						string covID=node.Attributes["COV_ID"].InnerText;  
						str= node.Attributes["Mandatory"].InnerText;  
						
						if(dsTemp.Tables[0].Rows[iDs]["COV_ID"].ToString()==covID  )
						{
							if ( str == "Y")
							{
								dsTemp.Tables[0].Rows[iDs]["Is_Mandatory"] = "Y";
							}
							else
							{
								dsTemp.Tables[0].Rows[iDs]["Is_Mandatory"]="N";
							}

						}
					}					
				}
			}

			dsCoverageHome=dsTemp.Copy(); 	
			return dsCoverageHome;			
		}

		/// <summary>
		/// updating default value for the coverage
		/// </summary>
		/// <param name="dsCoverageHome">contains coverage details for the home lob for the customer</param>
		/// <returns>Dataset after updating coverage default values</returns>
		public DataSet OverwriteCoverageDefaultValue(DataSet dsCoverageHome,string covXML)
		{
			DataSet dsTemp=new DataSet(); 
			XmlDocument xDoc=new XmlDocument();
			
			xDoc.LoadXml(covXML); 
			XmlNodeList xNodeList=xDoc.SelectNodes("/Coverages/Coverage"); 
			//string str="";
			dsTemp=dsCoverageHome.Copy(); 

			int iDs=0;	
			if(dsTemp!=null)
			{
				for(iDs=0;iDs<dsTemp.Tables[1].Rows.Count;iDs++)
				{
					foreach(XmlNode node in xNodeList)
					{
						string covID=node.Attributes["COV_ID"].InnerText;  
						
						XmlNodeList xLimitNode=node.SelectNodes("/Coverages/Coverage[@COV_ID='" + covID + "']/Limit"); 

						foreach(XmlNode limitNode in xLimitNode)
						{
							if ( limitNode.Attributes["id"] == null ) continue;

							string lID=limitNode.Attributes["id"].InnerText ;
							
							if ( limitNode.Attributes["Default"] != null )
							{
								string defStr=limitNode.Attributes["Default"].InnerText;
 
								if(dsTemp.Tables[1].Rows[iDs]["cov_id"].ToString()==covID &&   dsTemp.Tables[1].Rows[iDs]["LIMIT_DEDUC_ID"].ToString()==lID)
								{
									dsTemp.Tables[1].Rows[iDs]["IS_Default"]=defStr;
								}
							}
						}						
					}
				}
			}


			dsCoverageHome=dsTemp.Copy(); 	
			return dsCoverageHome;
		}

		
		#endregion

		#region Homeowner Recreational Vehicles Coverages
		
		public static DataSet GetHomeRecrVehCoverages(int customerID, int appID, 
			int appVersionID, int recrVehicleID, int currentPageIndex, int pageSize)
		{
			string	strStoredProc =	"Proc_GetHOME_OWNER_REC_VEH_COVERAGES";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@REC_VEH_ID",recrVehicleID);
			objWrapper.AddParameter("@PAGE_SIZE",pageSize);
			objWrapper.AddParameter("@CURRENT_PAGE_INDEX",currentPageIndex);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;

		}
		
		public int SaveHomeRecrVehCoverages(ArrayList alNewCoverages,string strOldXML)
		{
			
			string	strStoredProc =	"Proc_InsertAPP_HOME_OWNER_REC_VEH_COV";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			SqlCommand cmdCoverage = new SqlCommand();
			cmdCoverage.CommandText = strStoredProc;
			cmdCoverage.CommandType = CommandType.StoredProcedure;
		
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
					objWrapper.AddParameter("@REC_VEH_ID",objNew.RISK_ID);
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
					objWrapper.AddParameter("@LIMIT",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
					objWrapper.AddParameter("@DEDUCTIBLE",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
					
					objWrapper.AddParameter("@WRITTEN_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
					objWrapper.AddParameter("@FULL_TERM_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
					objWrapper.AddParameter("@CREATED_BY",System.DBNull.Value);
					
					string strTranXML = "";
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					if ( objNew.COVERAGE_ID == -1 )
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/CoveragesInfo.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vehicle cverages added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

					}
					else
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/CoveragesInfo.aspx.resx");
				
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

		public int DeleteHomeRecrVehCoverages(ArrayList alNewCoverages)
		{
			
			string	strStoredProc =	"Proc_DeleteHOME_OWNER_REC_VEH_COV";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			SqlParameter sCustomerID = (SqlParameter)objWrapper.AddParameter("@CUSTOMER_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppID = (SqlParameter)objWrapper.AddParameter("@APP_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppVersionID = (SqlParameter)objWrapper.AddParameter("@APP_VERSION_ID",SqlDbType.SmallInt,ParameterDirection.Input);
			SqlParameter sCoverageID = (SqlParameter)objWrapper.AddParameter("@COVERAGE_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sRecVehID = (SqlParameter)objWrapper.AddParameter("@REC_VEH_ID",SqlDbType.SmallInt,ParameterDirection.Input);

			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					sAppID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).APP_ID;
					sAppVersionID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).APP_VERSION_ID;
					sCustomerID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).CUSTOMER_ID;
					sCoverageID.Value = ((ClsCoveragesInfo)alNewCoverages[i]).COVERAGE_ID;
					sRecVehID.Value =  ((ClsCoveragesInfo)alNewCoverages[i]).RISK_ID;

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

		#region "Transaction log"
		/// <summary>
		/// Returns the transaction log for Home Coverage Info model object
		/// </summary>
		/// <param name="objNew"></param>
		/// <param name="xml"></param>
		/// <param name="coverageID"></param>
		/// <param name="root"></param>
		/// <returns></returns>
		protected string GetTranXML(ClsCoveragesInfo  objNew,string xml,int coverageID, XmlElement root)
		{
			XmlNode node = root.SelectSingleNode("Table[COVERAGE_ID=" + coverageID.ToString() + "]");
			//Changed by Swarup on 22-01-2008
			//For making Old Object of model class
			XmlDocument XmlDoc = new XmlDocument();
			XmlDoc.LoadXml(xml);
			XmlNodeList oldNodeList = XmlDoc.SelectNodes("NewDataSet/Table");
			XmlNode oldNode = oldNodeList.Item(0);
			oldNode = oldNodeList.Item(0);

			string strTranXML = "";	
			//ClsSchItemsCovgInfo objOld = new ClsSchItemsCovgInfo();
			ClsCoveragesInfo   objOld = new ClsCoveragesInfo  ();
						
			objOld.APP_ID = objNew.APP_ID;
			objOld.APP_VERSION_ID = objNew.APP_VERSION_ID;
			objOld.CUSTOMER_ID = objNew.CUSTOMER_ID;
			objOld.COVERAGE_ID = objNew.COVERAGE_ID;
			
			XmlNode element = null;

			element = oldNode.SelectSingleNode("COVERAGE_ID");
			if ( element.InnerXml != "")
			{
				objOld.COVERAGE_ID = Convert.ToInt32(element.InnerXml);
			}
			if(objOld.COVERAGE_ID != objNew.COVERAGE_ID)
			{
				if ( element != null)
				{
					objOld.COV_DESC = element.InnerXml.Trim();
				}
								
				element = node.SelectSingleNode("COVERAGE_CODE_ID");
						
				if ( element != null )
				{
					objOld.COVERAGE_CODE_ID = Convert.ToInt32(ClsCommon.DecodeXMLCharacters(element.InnerXml));
				}
					
				element = node.SelectSingleNode("LIMIT_1");
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
					if ( str != "" )
					{
						objOld.LIMIT_1  = Convert.ToDouble(str);
					}
				}

				element = node.SelectSingleNode("DEDUCTIBLE_1");
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
					if ( str != "" )
					{
						objOld.DEDUCTIBLE_1  = Convert.ToDouble(str);
					}
				}

				element = node.SelectSingleNode("LIMIT_2");
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
					if ( str != "" )
					{
						objOld.LIMIT_2   = Convert.ToDouble(str);
					}
				}

				element = node.SelectSingleNode("DEDUCTIBLE_2");
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
					if ( str != "" )
					{
						objOld.DEDUCTIBLE_2 = Convert.ToDouble(str);
					}
				}

				element = node.SelectSingleNode("LIMIT1_AMOUNT_TEXT");
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
					if ( str != "" )
					{
						objOld.LIMIT1_AMOUNT_TEXT = str;
					}
				}

				element = node.SelectSingleNode("LIMIT2_AMOUNT_TEXT");
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
					if ( str != "" )
					{
						objOld.LIMIT2_AMOUNT_TEXT = str;
					}
				}

				element = node.SelectSingleNode("DEDUCTIBLE1_AMOUNT_TEXT");
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
					if ( str != "" )
					{
						objOld.DEDUCTIBLE1_AMOUNT_TEXT= str;
					}
				}

				element = node.SelectSingleNode("DEDUCTIBLE2_AMOUNT_TEXT");
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
					if ( str != "" )
					{
						objOld.DEDUCTIBLE2_AMOUNT_TEXT = str;
					}
				}


				element = node.SelectSingleNode("DEDUCTIBLE");
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
					if ( str != "" )
					{
						objOld.DEDUCTIBLE = Convert.ToDouble(str);
					}
				}

				element = node.SelectSingleNode("DEDUCTIBLE_TEXT");
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
					if ( str != "" )
					{
						objOld.DEDUCTIBLE_TEXT  = str;
					}
				}

				element = node.SelectSingleNode("LIMIT_ID");
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
					if ( str != "" )
					{
						objOld.LIMIT_ID   = Convert.ToInt32 (str);
					}
				}


				element = node.SelectSingleNode("DEDUC_ID");
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
					if ( str != "" )
					{
						objOld.DEDUC_ID = Convert.ToInt32(str);
					}
				}

				element = node.SelectSingleNode("ADDDEDUCTIBLE_ID");
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
					if ( str != "" )
					{
						objOld.ADDDEDUCTIBLE_ID  = Convert.ToInt32(str);
					}
				}

				element = node.SelectSingleNode("SIGNATURE_OBTAINED");
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
					if ( str != "" )
					{
						objOld.SIGNATURE_OBTAINED = str;
					}
				}

			
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

				strTranXML = objBuilder.GetTransactionLogXML(objOld,objNew);
			}
			return strTranXML;
		}

		
		//Get Umbrella Transanction Log Xml
		protected string GetUmbTranXML(ClsCoveragesInfo objNew,string xml,int coverageID, XmlElement root)
		{
			XmlNode node = root.SelectSingleNode("Table[COVERAGE_ID=" + coverageID.ToString() + "]");
			
			//For making Old Object of model class
			XmlDocument XmlDoc = new XmlDocument();
			XmlDoc.LoadXml(xml);
			XmlNodeList oldNodeList = XmlDoc.SelectNodes("NewDataSet/Table");
			XmlNode oldNode = oldNodeList.Item(0);
			oldNode = oldNodeList.Item(0);


			string strTranXML = "";	
			
			ClsCoveragesInfo objOld = new ClsCoveragesInfo();
			objOld.APP_ID = objNew.APP_ID;
			objOld.APP_VERSION_ID = objNew.APP_VERSION_ID;
			objOld.CUSTOMER_ID = objNew.CUSTOMER_ID;
			objOld.COVERAGE_ID = objNew.COVERAGE_ID;
						
			XmlNode element = null;

			element = oldNode.SelectSingleNode("COVERAGE_ID");

			if ( element != null)
			{
				objOld.COVERAGE_ID = Convert.ToInt32(element.InnerXml);
			}
			if(objOld.COVERAGE_ID != objNew.COVERAGE_ID)
			{
				element = oldNode.SelectSingleNode("COVERAGE_CODE_ID");
						
				if ( element != null )
				{
					objOld.COVERAGE_CODE_ID = Convert.ToInt32(ClsCommon.DecodeXMLCharacters(element.InnerXml));
				}
						
			
				element = oldNode.SelectSingleNode("COV_DESC");

				if ( element != null)
				{
					objOld.COV_DESC = element.InnerXml.Trim();
				}		
//				if ( element != null )
//				{
//					string str = ClsCommon.DecodeXMLCharacters(element.InnerXml);
//							
//					if ( str != "" )
//					{
//						objOld.DETAILED_DESC = str;
//					}
//				}
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

				strTranXML = objBuilder.GetTransactionLogXML(objOld,objNew);
			}
			
			return strTranXML;
		}
		protected string GetTranXML(ClsSchItemsCovgInfo objNew,string xml,int coverageID, XmlElement root)
		{
			XmlNode node = root.SelectSingleNode("Table[COVERAGE_ID=" + coverageID.ToString() + "]");
						
			ClsSchItemsCovgInfo objOld = new ClsSchItemsCovgInfo();
						
			objOld.APP_ID = objNew.APP_ID;
			objOld.APP_VERSION_ID = objNew.APP_VERSION_ID;
			objOld.CUSTOMER_ID = objNew.CUSTOMER_ID;
			objOld.COVERAGE_ID = objNew.COVERAGE_ID;
						
			XmlNode element = null;

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
						
			
			element = node.SelectSingleNode("DETAILED_DESC");
						
			if ( element != null )
			{
				string str = ClsCommon.DecodeXMLCharacters(element.InnerXml);
							
				if ( str != "" )
				{
					objOld.DETAILED_DESC = str;
				}
			}


			element = node.SelectSingleNode("AMOUNT_OF_INSURANCE");
			if ( element != null )
			{
				string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
				if ( str != "" )
				{
					objOld.AMOUNT_OF_INSURANCE = Convert.ToDouble(str);
				}
			}

			element = node.SelectSingleNode("DEDUCTIBLE");
			if ( element != null )
			{
				string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
				if ( str != "" )
				{
					objOld.DEDUCTIBLE = Convert.ToDouble(str);
				}
			}
			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

			string strTranXML = objBuilder.GetTransactionLogXML(objOld,objNew);

			return strTranXML;
		}

		
		/// <summary>
		/// Returns the Transaction XML for Application Auto, Motor and Watercraft coverage model object
		/// </summary>
		/// <param name="objNew"></param>
		/// <param name="xml"></param>
		/// <param name="coverageID"></param>
		/// <param name="root"></param>
		/// <returns></returns>
		/*protected string GetTranXML(ClsCoveragesInfo objNew,string xml,int coverageID, XmlElement root)
		{
			XmlNode node = root.SelectSingleNode("Table[COVERAGE_ID=" + coverageID.ToString() + "]");
						
			Cms.Model.Application.ClsCoveragesInfo  objOld = new ClsCoveragesInfo();
						
			objOld.APP_ID = objNew.APP_ID;
			objOld.APP_VERSION_ID = objNew.APP_VERSION_ID;
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
*/
		
		/// <summary>
		/// Returns the Transaction XML for Application Auto, Motor and Watercraft coverage model object
		/// </summary>
		/// <param name="objNew"></param>
		/// <param name="xml"></param>
		/// <param name="coverageID"></param>
		/// <param name="root"></param>
		/// <returns></returns>
		protected string GetPolicyTranXML(Cms.Model.Policy.ClsPolicyCoveragesInfo objNew,string xml,int coverageID, XmlElement root)
		{
            if (root == null) return "";
            XmlNode node = root.SelectSingleNode("Table[COVERAGE_ID=" + coverageID.ToString() + "]");
			
			//For making Old Object of model class
			XmlDocument XmlDoc = new XmlDocument();
			XmlDoc.LoadXml(xml);
			XmlNodeList oldNodeList = XmlDoc.SelectNodes("NewDataSet/Table");
			XmlNode oldNode = oldNodeList.Item(0);
			oldNode = oldNodeList.Item(0);
			string strTranXML = "";

			Cms.Model.Policy.ClsPolicyCoveragesInfo  objOld = new Cms.Model.Policy.ClsPolicyCoveragesInfo();
						
			objOld.POLICY_ID = objNew.POLICY_ID;
			objOld.POLICY_VERSION_ID = objNew.POLICY_VERSION_ID;
			objOld.CUSTOMER_ID = objNew.CUSTOMER_ID;
			objOld.COVERAGE_ID = objNew.COVERAGE_ID;
						
			XmlNode element = null;
			
			

			element = oldNode.SelectSingleNode("COVERAGE_ID");

			if ( element.InnerXml!= "")
			{
				objOld.COVERAGE_ID = Convert.ToInt32(element.InnerXml);
			}
			if(objOld.COVERAGE_ID != objNew.COVERAGE_ID)
			{

				element = oldNode.SelectSingleNode("COV_DESC");

				if ( element != null)
				{
					objOld.COV_DESC = element.InnerXml.Trim();
				}			
				element = oldNode.SelectSingleNode("COVERAGE_CODE_ID");
						
				if ( element != null )
				{
					objOld.COVERAGE_CODE_ID = Convert.ToInt32(ClsCommon.DecodeXMLCharacters(element.InnerXml));
				}
						
				element = oldNode.SelectSingleNode("LIMIT_1_TYPE");
						
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
					if ( str != "" )
					{
						objOld.LIMIT_1_TYPE = str;
					}
				}
						
				element = oldNode.SelectSingleNode("LIMIT_2_TYPE");
						
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
					if ( str != "" )
					{
						objOld.LIMIT_2_TYPE =str;
					}
				}
			
				element = oldNode.SelectSingleNode("LIMIT_1");
						
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
					if ( str != "" )
					{
						objOld.LIMIT_1 = Convert.ToDouble(str);
					}
				}
						
				element = oldNode.SelectSingleNode("LIMIT_2");
						
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
					if ( str != "" )
					{
						objOld.LIMIT_2 = Convert.ToDouble(str);
					}
				}

				element = oldNode.SelectSingleNode("DEDUCTIBLE_1");
						
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(element.InnerXml);
							
					if ( str != "" )
					{
						objOld.DEDUCTIBLE_1 = Convert.ToDouble(str);
					}
				}

				element = oldNode.SelectSingleNode("DEDUCTIBLE_1_TYPE");
						
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(element.InnerXml);
							
					if ( str != "" )
					{
						objOld.DEDUCTIBLE_1_TYPE = str;
					}
				}
			
				element = oldNode.SelectSingleNode("DEDUCTIBLE_2_TYPE");
						
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(element.InnerXml);
							
					if ( str != "" )
					{
						objOld.DEDUCTIBLE_2_TYPE = str;
					}
				}

				element = oldNode.SelectSingleNode("WRITTEN_PREMIUM");
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
					if ( str != "" )
					{
						objOld.WRITTEN_PREMIUM = Convert.ToDouble(str);
					}
				}

				element = oldNode.SelectSingleNode("FULL_TERM_PREMIUM");
				if ( element != null )
				{
					string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
					if ( str != "" )
					{
						objOld.FULL_TERM_PREMIUM = Convert.ToDouble(str);
					}
				}
			
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

				strTranXML = objBuilder.GetTransactionLogXML(objOld,objNew);
			}
			return strTranXML;
		}


		/// <summary>
		/// Returns the Transaction XML for Application Auto, Motor and Watercraft coverage model object
		/// </summary>
		/// <param name="objNew"></param>
		/// <param name="xml"></param>
		/// <param name="coverageID"></param>
		/// <param name="root"></param>
		/// <returns></returns>
		protected string GetHomeTranXML(ClsCoveragesInfo  objNew,string xml,int coverageID, XmlElement root)
		{
			XmlNode node = root.SelectSingleNode("Table[COVERAGE_ID=" + coverageID.ToString() + "]");
						
		    ClsCoveragesInfo objOld = new ClsCoveragesInfo();
						
			objOld.APP_ID= objNew.APP_ID;
			objOld.APP_VERSION_ID = objNew.APP_VERSION_ID;
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


		/// <summary>
		/// Returns the Transaction XML for Application Auto, Motor and Watercraft coverage model object
		/// </summary>
		/// <param name="objNew"></param>
		/// <param name="xml"></param>
		/// <param name="coverageID"></param>
		/// <param name="root"></param>
		/// <returns></returns>
		protected string GetPolicyHomeTranXML(Cms.Model.Policy.ClsPolicyCoveragesInfo  objNew,string xml,int coverageID, XmlElement root)
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


		#endregion 

		#region BindDropDown Function
		public static void BindDropDown(HtmlSelect ddlToBind,DataView dvSource,string strDataText,string strDataValue,DateTime AppEffectiveDate)
		{
			int i =0;
			foreach(DataRowView row in dvSource)
			{
				ddlToBind.Items.Add(new ListItem(row[strDataText].ToString(),row[strDataValue].ToString()));
				if(row["EFFECTIVE_FROM_DATE"] != DBNull.Value) 
				{
					if(AppEffectiveDate < Convert.ToDateTime( row["EFFECTIVE_FROM_DATE"]))
					{
						//ddlToBind.Items[i].Attributes.CssStyle.Add ("BACKGROUND-COLOR","Red");
						ddlToBind.Items[i].Attributes.Add ("Class","GrandFatheredRange");
					}
				}
				if( row["EFFECTIVE_TO_DATE"] != DBNull.Value )
				{
					if(AppEffectiveDate > Convert.ToDateTime(row["EFFECTIVE_TO_DATE"]) )
					{
						//ddlToBind.Items[i].Attributes.CssStyle.Add ("BACKGROUND-COLOR","Red");
						ddlToBind.Items[i].Attributes.Add ("Class","GrandFatheredRange");
					}

				}
															  
				i++;
			}
		}
		#endregion 

		#region SelectValueInDropDown
		public static  void SelectValueInDropDown(HtmlSelect cmbDropdown,object objValue)
		{
			if ( objValue == System.DBNull.Value ) return;

			ListItem listItem;
			
			listItem = cmbDropdown.Items.FindByValue(Convert.ToString(objValue));
			cmbDropdown.SelectedIndex = cmbDropdown.Items.IndexOf(listItem);
		}
		#endregion 

		#region Remove Disabled options InDropDown
		public static  void RemoveDisabledInDropDown(HtmlSelect cmbDropdown,DataView dvSource,object objValue,DateTime AppEffectiveDate)
		{
			//if ( objValue == System.DBNull.Value ) return;

			ListItem listItem;
			
			listItem = cmbDropdown.Items.FindByValue(Convert.ToString(objValue));
//
			int i =0;
			foreach(DataRowView row in dvSource)
			{
				if(row["DISABLED_DATE"] != DBNull.Value) 
				{
					if(AppEffectiveDate > Convert.ToDateTime(row["DISABLED_DATE"]))
					{
						//ddlToBind.Items[i].Attributes.CssStyle.Add ("BACKGROUND-COLOR","Red");
						if (listItem !=null)
						{
							if (cmbDropdown.Items[i].Equals(listItem))  
								continue;
							else
								cmbDropdown.Items.Remove(cmbDropdown.Items.FindByValue(row["ENDORSEMENT_ATTACH_ID"].ToString()))  ;
						}
						else
								cmbDropdown.Items.Remove(cmbDropdown.Items.FindByValue(row["ENDORSEMENT_ATTACH_ID"].ToString()))  ;

					}
				}
															  
				i++;
			}
			//



		}
		#endregion 
		#region GetPolicyStatus
		public static string GetPolicyStatus(int customerID, int polID, int polVersionID)
		{
			string	strStoredProc =	"Proc_GetPolicyStatus";
			string polStat="";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POL_ID",polID);
			objWrapper.AddParameter("@POL_VERSION_ID",polVersionID);
					
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			if(ds!=null)
				if(ds.Tables[0].Rows.Count>0)
					polStat=ds.Tables[0].Rows[0]["policy_status"].ToString();

			return polStat;
		
		}
		#endregion

		#region InitialiseRules initialise Hashtable from data fetched from SP
		protected void InitialiseRules(DataWrapper objDataWrapper, int CustomerId, int Id, int VersionId, int RiskId,string strLevel)
		{
			XmlNode masterNode =RuleDoc.SelectSingleNode("Root/Master");
			string strQuery = "";
			if(strLevel == "APP")
			{
				XmlNode queryNode  =masterNode.SelectSingleNode("Query[@Level='APP']" );
				strQuery = queryNode.InnerText;

				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@APP_ID",Id );
				objDataWrapper.AddParameter("@APP_VERSION_ID",VersionId);
				objDataWrapper.AddParameter("@BOAT_ID",RiskId);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			}
			else if(strLevel == "POLICY")
			{
				XmlNode queryNode  =masterNode.SelectSingleNode("Query[@Level='POLICY']" );
				strQuery = queryNode.InnerText;

				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@POLICY_ID",Id);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",VersionId);
				objDataWrapper.AddParameter("@BOAT_ID",RiskId);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			}


			DataSet dtKeyValue= objDataWrapper.ExecuteDataSet(strQuery);

			objDataWrapper.ClearParameteres();

			if(coverageKeys.Count >0)
			{
				coverageKeys.Clear(); 
			}
			XmlNodeList columnNodes = masterNode.SelectNodes("Column");

			if(dtKeyValue.Tables.Count>0)
			{
				if(dtKeyValue.Tables[0].Rows.Count>0)
				{

					foreach(XmlNode node in columnNodes)
					{
						string strKey=node.Attributes["Code"].Value;
						string strValue="";
						if(node.Attributes["MapColumnn"].Value.Trim() != "")
						{
							if(dtKeyValue.Tables[0].Rows[0][strKey] != DBNull.Value)
							{
								strValue=dtKeyValue.Tables[0].Rows[0][strKey].ToString().Trim();
								//If the returned value is Negative make it Zero
								if(strValue.StartsWith("-"))
								{
									strValue="0";
								}
							}
							coverageKeys.Add(strKey,strValue);
						}
					}
				}
			}
			AppEffectiveDate = Convert.ToDateTime(coverageKeys["APP_EFFECTIVE_DATE"]);
			StateId = coverageKeys["STATE_ID"].ToString();
			IsInitialised =true;
		}
		#endregion

		#region Virtual Functions to be Overriden in derived class
		// Functions to be Overriden in Derived class
		//App level
		protected virtual void SaveCoverageApp(DataWrapper objDataWrapper,int CustomerId, int AppId, int AppVersionId,int RiskId,Coverage cov)
		{
		}

		protected virtual void DeleteCoverageApp(DataWrapper objDataWrapper,int CustomerId, int AppId, int AppVersionId,int RiskId,string strCov_Code)
		{
		}

		protected virtual  void UpdateCoverageApp(DataWrapper objDataWrapper,int CustomerId, int AppId, int AppVersionId,int RiskId,Coverage cov)
		{
		}

		protected virtual  void UpdateEndorsmentApp(DataWrapper objDataWrapper,int CustomerId, int AppId, int AppVersionId,int RiskId)
		{
		}

		public virtual DataTable GetRisksForLobApp(int CustomerId, int AppId, int AppVersionId, int RiskId)
		{
			return null;
		}
		protected virtual  void WriteTranLogApp(DataWrapper objDataWrapper,int CustomerId, int AppId, int AppVersionId,int RiskId,Coverage cov)
		{
		}

		
		//Policy Level 
		protected virtual void SaveCoveragePolicy(DataWrapper objDataWrapper,int CustomerId, int PolicyId, int PolicyVersionId,int RiskId,Coverage cov)
		{
		}

		protected virtual void DeleteCoveragePolicy(DataWrapper objDataWrapper,int CustomerId, int PolicyId, int PolicyVersionId,int RiskId,string strCov_Code)
		{
		}

		protected virtual  void UpdateCoveragePolicy(DataWrapper objDataWrapper,int CustomerId, int PolicyId, int PolicyVersionId,int RiskId,Coverage cov)
		{
		}

		public virtual DataTable GetRisksForLobPolicy(int CustomerId, int PolicyId, int PolicyVersionId, int RiskId)
		{
			return null;
		}
		
		protected virtual  void UpdateEndorsmentPolicy(DataWrapper objDataWrapper,int CustomerId, int PolicyId, int PolicyVersionId,int RiskId)
		{
		}
		protected virtual  void SaveDefaultCoveragesFromDB(DataWrapper objDataWrapper,int CustomerId, int Id, int VersionId,int RiskId,string CalledFor)
		{
		}
		#endregion 
		
		public int SaveDefaultCoveragesApp(DataWrapper objDataWrapper, int CustomerId, int AppId, int AppVersionId, int RiskId)
		{
			 SaveDefaultCoveragesFromDB(objDataWrapper,CustomerId,AppId,AppVersionId,RiskId,"APP");
			return SaveDefaultCoverages(objDataWrapper,CustomerId,AppId,AppVersionId,RiskId,"APP");  
		}
		public int SaveDefaultCoveragesPolicy(DataWrapper objDataWrapper, int CustomerId, int PolicyId, int PolicyVersionId, int RiskId)
		{
			 SaveDefaultCoveragesFromDB(objDataWrapper,CustomerId,PolicyId,PolicyVersionId,RiskId,"POLICY");
			return SaveDefaultCoverages(objDataWrapper,CustomerId,PolicyId,PolicyVersionId,RiskId,"POLICY");
		}

		public  int UpdateCoveragesByRuleApp(DataWrapper objDataWrapper, int CustomerId, int AppId, int AppVersionId, RuleType ruleType, int RiskId)
		{
			return UpdateCoveragesByRule(objDataWrapper,CustomerId,AppId,AppVersionId,ruleType,RiskId,"APP");   
		}
		public  int UpdateCoveragesByRulePolicy(DataWrapper objDataWrapper, int CustomerId, int PolicyId, int PolicyVersionId, RuleType ruleType, int RiskId)
		{
			return UpdateCoveragesByRule(objDataWrapper,CustomerId,PolicyId,PolicyVersionId,ruleType,RiskId,"POLICY") ;
		}

		public int UpdateCoveragesByRuleApp(DataWrapper objDataWrapper, int CustomerId, int AppId, int AppVersionId,RuleType ruleType)
		{
			return UpdateCoveragesByRule(objDataWrapper,CustomerId,AppId,AppVersionId,ruleType,"APP"); 
			
		}

		public int UpdateCoveragesByRulePolicy(DataWrapper objDataWrapper, int CustomerId, int PolicyId, int PolicyVersionId,RuleType ruleType)
		{
			return UpdateCoveragesByRule(objDataWrapper,CustomerId,PolicyId,PolicyVersionId,ruleType,"POLICY");
		}
		protected virtual  void WriteTranLogPol(DataWrapper objDataWrapper,int CustomerId, int PolId, int PolVersionId,int RiskId,Coverage cov)
		{
		}


		#region Private Function --SaveDefaultCoverages -- To Be Called When New Vehicle/Motor/Boat is Added
		/// <summary>
		/// Updates coveraage and endorsement for specified newly added boat of specified application based on business rules
		/// </summary>
		/// <param name="objDataWrapper"></param>
		/// <param name="CustomerId"></param>
		/// <param name="AppId"></param>
		/// <param name="AppVersionId"></param>
		/// <param name="BoatId"></param>
		private int SaveDefaultCoverages(DataWrapper objDataWrapper, int CustomerId, int Id, int VersionId, int RiskId,string strLevel)
		{
			if(!IsInitialised )
			{
				InitialiseRules(objDataWrapper,CustomerId,Id,VersionId,RiskId,strLevel);
			}
		
			XmlNode groupNode= RuleDoc.SelectSingleNode("Root/Group[@Code='" + RuleType.Default.ToString() + "']" );

			ArrayList ruleNodeArray = GetEffectiveRules(groupNode,AppEffectiveDate,StateId); 

			UpdateCoveragesFromRuleNode(objDataWrapper,ruleNodeArray,CustomerId,Id,VersionId,RiskId,strLevel);  

			if(coverageKeys["LOB_ID"].ToString() =="1")
			{
				this.UpdateCoveragesByRule(objDataWrapper,CustomerId,Id,VersionId,RuleType.LobDependent ,RiskId,strLevel);
			}
			UpdateCoveragesByRule(objDataWrapper,CustomerId,Id,VersionId,RuleType.AppDependent,RiskId,strLevel);
			UpdateCoveragesByRule(objDataWrapper,CustomerId,Id,VersionId,RuleType.RiskDependent,RiskId,strLevel);
			

			///////////////////////////////////////////////////
			///
			return 1;
		}
		#endregion 

		#region Private Method UpdateCoveragesFromRuleNode
		private  void UpdateCoveragesFromRuleNode(DataWrapper objDataWrapper,ArrayList ruleNodeArray, int CustomerId, int Id, int VersionId,int RiskId,string strLevel)
		{
			Coverage cov = new Coverage();
			for(int i=0;i<ruleNodeArray.Count;i++) //Main Loop
			{
				XmlNode ruleNode =(XmlNode)ruleNodeArray[i];
				XmlNodeList conditionsNodeArray = ruleNode.SelectNodes("Conditions");
				foreach(XmlNode conditionsNode in conditionsNodeArray )
				{
					XmlNodeList conditionNodeArray = conditionsNode.SelectNodes("Condition");
					foreach(XmlNode conditionNode in conditionNodeArray) //Condition Loop
					{
						string strResult=EvalNode(conditionNode);
						if(strResult=="True")
						{
							//Granting Coverage if Condition verified
							XmlNodeList toGrantList = conditionNode.SelectNodes("ToGrant");
							if(toGrantList != null)
							{
								foreach(XmlNode toGrant in toGrantList)
								{
									cov=GetCoverageFromNode(toGrant);
									if(strLevel == "APP")
									{
										SaveCoverageApp(objDataWrapper,CustomerId,Id,VersionId,RiskId,cov);
									}
									else if(strLevel == "POLICY")
									{
										SaveCoveragePolicy(objDataWrapper,CustomerId,Id,VersionId,RiskId,cov);
									}
								}
							}
							//Revoke  logic to be added
							XmlNodeList revokeNodeArray = conditionNode.SelectNodes("ToRevoke");
							if(revokeNodeArray != null)
							{
								foreach(XmlNode revokeNode in revokeNodeArray)
								{
									string strCov_Code = revokeNode.Attributes["CoverageCode"].Value;
									if(strLevel == "APP")
									{
										DeleteCoverageApp(objDataWrapper,CustomerId,Id,VersionId,RiskId,strCov_Code);
									}
									else if(strLevel == "POLICY")
									{
										DeleteCoveragePolicy(objDataWrapper,CustomerId,Id,VersionId,RiskId,strCov_Code);
									}
									
								}
							}
							//Update Logic(ToSet)
							XmlNodeList toSetNodeArray = conditionNode.SelectNodes("ToSet");
							if(toSetNodeArray != null)
							{
								foreach(XmlNode toSetNode in toSetNodeArray)
								{
									cov=GetCoverageFromNode(toSetNode);
								
									if(strLevel == "APP")
									{
										UpdateCoverageApp(objDataWrapper,CustomerId,Id ,VersionId,RiskId,cov);
									}
									else if(strLevel == "POLICY")
									{
										UpdateCoveragePolicy(objDataWrapper,CustomerId,Id,VersionId,RiskId,cov);
									}

								}
							}
							//ToSet(Update) Logic Ends here

							//SubCondition 
							XmlNodeList subConditionsNodeArray = conditionNode.SelectNodes("SubCondition");
							if(subConditionsNodeArray != null)
							{
								foreach(XmlNode subConditionNode in subConditionsNodeArray)
								{
									string strRes =EvalNode(subConditionNode);
									if(strRes == "True")
									{
										//Granting Coverage if Condition verified
										XmlNodeList toGraList = subConditionNode.SelectNodes("ToGrant");
										if(toGraList != null)
										{
											foreach(XmlNode toGra in toGraList)
											{
												cov=GetCoverageFromNode(toGra);
												if(strLevel == "APP")
												{
													SaveCoverageApp(objDataWrapper,CustomerId,Id,VersionId,RiskId,cov);
												}
												else if(strLevel == "POLICY")
												{
													SaveCoveragePolicy(objDataWrapper,CustomerId,Id,VersionId,RiskId,cov);
												}
											}
										}
										//Revoke  logic to be added
										XmlNodeList revokeNodeList = subConditionNode.SelectNodes("ToRevoke");
										if(revokeNodeArray != null)
										{
											foreach(XmlNode revokeNode in revokeNodeList)
											{
												string strCov_Code = revokeNode.Attributes["CoverageCode"].Value;
												if(strLevel == "APP")
												{
													DeleteCoverageApp(objDataWrapper,CustomerId,Id,VersionId,RiskId,strCov_Code);
												}
												else if(strLevel == "POLICY")
												{
													DeleteCoveragePolicy(objDataWrapper,CustomerId,Id,VersionId,RiskId,strCov_Code);
												}
											}
										}
										//Update Logic(ToSet)
										XmlNodeList toSetNodeList= subConditionNode.SelectNodes("ToSet");
										if(toSetNodeList != null)
										{
											foreach(XmlNode toSetNode in toSetNodeList)
											{
												cov=GetCoverageFromNode(toSetNode);
												if(strLevel == "APP")
												{
													UpdateCoverageApp(objDataWrapper,CustomerId,Id ,VersionId,RiskId,cov);
												}
												else if(strLevel == "POLICY")
												{
													UpdateCoveragePolicy(objDataWrapper,CustomerId,Id,VersionId,RiskId,cov);
												}
											}
										}
										//ToSet(Update) Logic Ends here

										break;
									}
								}
							}//End of if condition for Subcond
					
							break;
						}
					}
				}
			}
			//writing transaction log for default coverages
			if(strLevel == "APP")
			{
				WriteTranLogApp(objDataWrapper,CustomerId, Id, VersionId, RiskId,cov);
				sbDefaultTranXML.Length =0;
				sbDefaultTranXML.Append("<root>") ;
			}
			if(strLevel == "POLICY")
			{
				WriteTranLogPol(objDataWrapper,CustomerId, Id, VersionId, RiskId,cov);
				sbDefaultTranXML.Length =0;
				sbDefaultTranXML.Append("<root>") ;
			}
		}
		#endregion 
		
		#region Private Function -- UpdateCoveragesBy Rule -- Risk specific
		/// <summary>
		/// Updates coveraage and endorsement for specified boat/vehicle of specified application based on business rules
		/// </summary>
		/// <param name="objDataWrapper"></param>
		/// <param name="CustomerId"></param>
		/// <param name="AppId"></param>
		/// <param name="AppVersionId"></param>
		/// <param name="BoatId"></param>
		private  int UpdateCoveragesByRule(DataWrapper objDataWrapper, int CustomerId, int Id, int VersionId, RuleType ruleType, int RiskId,string strLevel)
		{
			if(!IsInitialised )
			{
				InitialiseRules(objDataWrapper,CustomerId,Id,VersionId,RiskId,strLevel);
			}

			XmlNode groupNode= RuleDoc.SelectSingleNode("Root/Group[@Code='" + ruleType.ToString() + "']" );
			
			ArrayList masterRuleNodeArray = GetEffectiveMasterRules(groupNode,AppEffectiveDate,StateId); 
			ArrayList ruleNodeArray = GetEffectiveRules(groupNode,AppEffectiveDate,StateId); 

			//Set Values in coverageRule Has Table as per master rules

			SetMasterKeys(masterRuleNodeArray);			
			//Setting values in Master HashTable end here

			//Start Granting/Revoking coverages

			UpdateCoveragesFromRuleNode(objDataWrapper,ruleNodeArray,CustomerId,Id,VersionId,RiskId,strLevel);  
			
			//Update Linked Endorsment
			if(strLevel == "APP")
			{
				UpdateEndorsmentApp(objDataWrapper,CustomerId,Id,VersionId,RiskId);
			}
			if(strLevel == "POLICY")
			{
				UpdateEndorsmentPolicy (objDataWrapper,CustomerId,Id,VersionId,RiskId);
			}
			return 1;
			
		}
		#endregion 

		#region Private Function UpdateCoveragesByRule --For all Risks in an Application
		private int UpdateCoveragesByRule(DataWrapper objDataWrapper, int CustomerId, int Id, int VersionId,RuleType ruleType,string strLevel)
		{
			DataTable dt =null ;
			if(strLevel == "APP")
			{
				dt= this.GetRisksForLobApp(CustomerId, Id, VersionId, -1);
			}
			else if(strLevel == "POLICY")
			{
				dt=this.GetRisksForLobPolicy(CustomerId,Id,VersionId,-1);
			}

			if (dt != null)
			{
				foreach(DataRow dr in dt.Rows)
				{
					//Invalidate initialisation
					InvalidateInitialisation();
					////////////////////////////
					
					UpdateCoveragesByRule(objDataWrapper, CustomerId, Id, VersionId,ruleType, Convert.ToInt32(dr["RISK_ID"]),strLevel);
				}
			}
			return 1;
		}
		#endregion


		public void InvalidateInitialisation()
		{
				IsInitialised=false;
		}

		#region EvalNode
		/// <summary>
		/// Evaluate An XML Condition Node
		/// </summary>
		/// <param name="node">XML Condition Node </param>
		/// <returns>Result Of Expression In Condition Node</returns>
		public string EvalNode(XmlNode node)
		{
			return EvalNode(node,coverageKeys); 
		}
		#endregion 


		#region Private Utility Functions 	
	
	

		/// <summary>
		/// Returns Coverage Data From XML node of ToGrant Type
		/// </summary>
		/// <param name="toGrant"></param>
		/// <returns></returns>
		public Coverage GetCoverageFromNode(XmlNode toGrant)
		{
			Coverage cov =  new Coverage ();
			string Limit,Deductible,AdDeductible;
			cov.COV_CODE= toGrant.Attributes["CoverageCode"].Value;
			XmlNode limitNode = toGrant.SelectSingleNode ("Limit");

			Limit="";
			Deductible="";
			AdDeductible="";
			if(limitNode.Attributes["Value"].Value.Trim() == "")
			{
				//If no value is assigned calculate value based on operands and operator
				Limit = EvalNode(limitNode);
			}
			else
			{
				if(limitNode.Attributes["Value"].Value.StartsWith("$"))
				{
					string strKey=limitNode.Attributes["Value"].Value.Substring(limitNode.Attributes["Value"].Value.IndexOf('$')+1);
					Limit = coverageKeys[strKey].ToString();
				}
				else
				{
					Limit= limitNode.Attributes["Value"].Value;
				}
			}

			XmlNode deducNode = toGrant.SelectSingleNode("Deductible");
			if(deducNode.Attributes["Value"].Value.Trim()=="")
			{
				//If no value is assigned calculate value based on operands and operator
				Deductible=EvalNode(deducNode);;

			}
			else
			{
				if(deducNode.Attributes["Value"].Value.StartsWith("$"))
				{
					string strKey = deducNode.Attributes["Value"].Value.Substring(deducNode.Attributes["Value"].Value.IndexOf('$')+1);
                    if(coverageKeys[strKey] != null)
					Deductible=coverageKeys[strKey].ToString();
				}
				else
				{
					Deductible =deducNode.Attributes["Value"].Value;
				}
			}

			XmlNode adDeducNode = toGrant.SelectSingleNode("AdDeductible");
			if(adDeducNode != null)
			{
				if(adDeducNode .Attributes["Value"].Value.Trim()=="")
				{
					//If no value is assigned calculate value based on operands and operator
					AdDeductible  =EvalNode(adDeducNode);;

				}
				else
				{
					if(adDeducNode.Attributes["Value"].Value.StartsWith("$"))
					{
						string strKey = adDeducNode.Attributes["Value"].Value.Substring(adDeducNode.Attributes["Value"].Value.IndexOf('$')+1);
						AdDeductible =coverageKeys[strKey].ToString();
					}
					else
					{
						AdDeductible =adDeducNode.Attributes["Value"].Value;
					}
				}
			}
			cov=FillLimitDeductible(cov,Limit,Deductible,AdDeductible); 
			return cov;
		}
		
		
		protected void SetMasterKeys(ArrayList masterRuleNodeArray)
		{
			for(int i=0;i<masterRuleNodeArray.Count;i++)
			{
				XmlNode masterNode = (XmlNode)masterRuleNodeArray[i];
				foreach(XmlNode conditionsNode in masterNode.ChildNodes )
				{
					XmlNodeList conditionNodeList = conditionsNode.SelectNodes("Condition");
					foreach (XmlNode conditionNode in conditionNodeList)
					{
						string strResult=EvalNode(conditionNode);
						if(strResult == "True")
						{
							XmlNodeList toSetNodeArray=conditionNode.SelectNodes("ToSet");
							if(toSetNodeArray != null)
							{
								SetKeys(toSetNodeArray,coverageKeys );
							}
							XmlNodeList subConditionsNodeArray =conditionNode.SelectNodes("SubConditions");
							foreach(XmlNode subConditionsNode in  subConditionsNodeArray)
							{
								//Subcondition
								XmlNodeList subConditionNodeArray = subConditionsNode.SelectNodes("SubCondition");
								if(subConditionNodeArray != null)
								{
									foreach(XmlNode subConditionNode in subConditionNodeArray )
									{
										string strRes = EvalNode(subConditionNode);
										if(strRes == "True")
										{
											XmlNodeList toSetNodeList=subConditionNode.SelectNodes("ToSet");
											if(toSetNodeList != null)
											{
												SetKeys(toSetNodeList,coverageKeys);
											}
											break;
										}

									}
								}
								//End OF Subcondition
							}
							break;
						}

					}

				}
			}
		}

		

		/// <summary>
		/// Splits Limit & Deductible & Iniatialises Coverage Structure
		/// </summary>
		/// <param name="cov"></param>
		/// <param name="Limit"></param>
		/// <param name="Deductible"></param>
		/// <returns></returns>
		private Coverage FillLimitDeductible(Coverage cov,string Limit,string Deductible,string AdDeductible)
		{
			string[] strLimitValues=Limit.Split('/');

			if(strLimitValues.Length>0)
			{
				string[] strLimits1 = strLimitValues[0].Split(' ');
				if ( strLimits1.Length > 0 )
				{
					if ( strLimits1[0].Trim() != "" )
					{
						cov.LIMIT1_AMOUNT  =  Convert.ToDouble(strLimits1[0]);
					}
				}

				if ( strLimits1.Length > 1 )
				{
					string strlimit="";
					for(int i=1;strLimits1.Length > i ;i++)
					{
						strlimit =strlimit  +  " " + strLimits1[i].ToString();
					}
					cov.LIMIT1_TEXT  =  strlimit.TrimStart(' ');
				}
			}
			if(strLimitValues.Length>1)
			{
				string[] strLimits2 = strLimitValues[1].Split(' ');
				if ( strLimits2.Length > 0 )
				{
					if ( strLimits2[0] != "" )
					{
						cov.LIMIT2_AMOUNT =  Convert.ToDouble(strLimits2[0]);
					}
				}
									
				if ( strLimits2.Length > 1 )
				{
					cov.LIMIT2_TEXT =  strLimits2[1];
				}

			}
			
			//Split Deductibel and inisialise Coverage Structure aggordingly

			string[] strDeducValues = Deductible.Split('/');

			if(strDeducValues.Length>0)
			{
				string[] strDed1 = strDeducValues [0].Split(' ');
				if ( strDed1.Length > 0 )
				{
					if ( strDed1[0].Trim() != "" )
					{
						cov.DEDUCTIBLE1_AMOUNT =  Convert.ToDouble(strDed1[0]);
					}
				}
				if ( strDed1.Length > 1 )
				{
					cov.DEDUCTIBLE1_TEXT  =  strDed1[1];
				}
			}
			if(strDeducValues.Length>1)
			{
				string[] strDed2 = strDeducValues [1].Split(' ');
				if ( strDed2.Length > 0 )
				{
					if ( strDed2[0].Trim() != "" )
					{
						cov.DEDUCTIBLE2_AMOUNT  =  Convert.ToDouble(strDed2[0]);
					}
				}
									
				if ( strDed2.Length > 1 )
				{
					cov.DEDUCTIBLE2_TEXT  =  strDed2[1];
				}
			}

			//Split AdDeductibel and inisialise Coverage Structure aggordingly

			string[] strAdDeducValues = AdDeductible.Split('/');

			if(strAdDeducValues.Length>0)
			{
				string[] strAdDed1 = strAdDeducValues [0].Split(' ');
				if ( strAdDed1.Length > 0 )
				{
					if ( strAdDed1[0].Trim() != "" )
					{
						cov.AD_DEDUCTIBLE_AMOUNT =  Convert.ToDouble(strAdDed1[0]);
					}
				}
				if ( strAdDed1.Length > 1 )
				{
					cov.AD_DEDUCTIBLE_TEXT =  strAdDed1[1];
				}
			}
			if(strAdDeducValues.Length>1)
			{
				string[] strAdDed2 = strAdDeducValues [1].Split(' ');
				if ( strAdDed2.Length > 0 )
				{
					if ( strAdDed2[0].Trim() != "" )
					{
						cov.AD_DEDUCTIBLE_AMOUNT  =  Convert.ToDouble(strAdDed2[0]);
					}
				}
									
				if ( strAdDed2.Length > 1 )
				{
					cov.AD_DEDUCTIBLE_TEXT  =  strAdDed2[1];
				}
			}
		
			return cov;
		}
		#endregion 

	

	}
}
