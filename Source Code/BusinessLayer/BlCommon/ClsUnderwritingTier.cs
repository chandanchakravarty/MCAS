/******************************************************************************************
<Author				: -	Praveen Kasana 
<Start Date			: -	12/28/2009
<End Date			: -	
<Description		: - Implementation Underwriting Tier
<Review Date		: - 
<Reviewed By		: - 	
*******************************************************************************************/ 
using System;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
//using Cms.Model.Application;
using Cms.BusinessLayer.BlCommon;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text.RegularExpressions;


namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsUnderwritingTier.
	/// </summary>
	/// 
	
	public class ClsUnderwritingTier
	{
		protected string filePath ;
		protected System.Xml.XmlDocument RuleDoc;
		public Hashtable uTierKeys;
		protected DateTime AppEffectiveDate;
		protected string StateId;

		public ClsUnderwritingTier()
		{
            filePath = ClsCommon.GetKeyValueWithIP("UnderwritingTierPath");
			RuleDoc = new XmlDocument();
			uTierKeys=new Hashtable();
			RuleDoc.Load(filePath); 
		}

		public ClsUnderwritingTier(string strQQ)
		{
			//Called from QQ Process
		}

		#region UNDERWRITING TIER XML PARSER METHODS
		#region InitialiseRules initialise Hashtable from data fetched from SP
		protected int InitialiseRules(int CustomerId, int Id, int VersionId, string strLevel,DataWrapper objDataWrapper)
		{
			//DataWrapper objDataWrapper = new DataWrapper(ClsCommon.ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
			XmlNode masterNode =RuleDoc.SelectSingleNode("Root/Master");
			string strQuery = "";
			SqlParameter paramRetVal = null;
			if(strLevel == "APP")
			{
				XmlNode queryNode  =masterNode.SelectSingleNode("Query[@Level='APP']" );
				strQuery = queryNode.InnerText;

				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@APP_ID",Id );
				objDataWrapper.AddParameter("@APP_VERSION_ID",VersionId);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);

				
			}
			else if(strLevel == "POLICY")
			{
				XmlNode queryNode  =masterNode.SelectSingleNode("Query[@Level='POLICY']" );
				strQuery = queryNode.InnerText;

				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@POLICY_ID",Id);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",VersionId);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);

				
			}

			paramRetVal  = (SqlParameter) objDataWrapper.AddParameter("@RET_VAL",null,SqlDbType.Int,ParameterDirection.ReturnValue);

			DataSet dtKeyValue= objDataWrapper.ExecuteDataSet(strQuery);
			objDataWrapper.ClearParameteres();

			if(Convert.ToInt32(paramRetVal.Value) ==-1)
			{
				return -1;
			}
			objDataWrapper.ClearParameteres();

			if(uTierKeys.Count >0)
			{
				uTierKeys.Clear(); 
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
							uTierKeys.Add(strKey,strValue);
						}
					}
				}
			}
			AppEffectiveDate = Convert.ToDateTime(uTierKeys["APP_EFFECTIVE_DATE"]);
			StateId = uTierKeys["STATE_ID"].ToString();
			return 1;
			
		}

		protected int InitialiseRulesQQ(string lapseDays,string priorLimit,string totalNAF,string limitType,string effectiveDate,string stateID,string strLevel,DataWrapper objDataWrapper)
		{

			uTierKeys.Add("LAPSE_DAYS",lapseDays);
			uTierKeys.Add("PRIOR_BI_LIMIT",priorLimit);
			uTierKeys.Add("TOTAL_NAF",totalNAF);
			uTierKeys.Add("LIMIT_TYPE",limitType);
			uTierKeys.Add("APP_EFFECTIVE_DATE",effectiveDate);
			uTierKeys.Add("STATE_ID",stateID);		
            		
			AppEffectiveDate = Convert.ToDateTime(uTierKeys["APP_EFFECTIVE_DATE"]);
			StateId = uTierKeys["STATE_ID"].ToString();
			return 1;
			
		}


		#endregion
		#region EVALUATE TIER
		/// <summary>
		/// Ftech Underwriting Tier
		/// </summary>
		/// <param name="masterRuleNodeArray"></param>
		protected Hashtable SetMasterKeysUT(ArrayList masterRuleNodeArray)
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
								SetKeys(toSetNodeArray,uTierKeys );
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
												SetKeys(toSetNodeList,uTierKeys);
											}
											//Added one more level
											XmlNodeList subConditionsNodeArrayChild =subConditionNode.SelectNodes("SubConditions");
											foreach(XmlNode subConditionsNodeChild in  subConditionsNodeArrayChild)
											{
												XmlNodeList subConditionNodeArrayChild = subConditionsNodeChild.SelectNodes("SubCondition");
												if(subConditionNodeArrayChild != null)
												{
													foreach(XmlNode subConditionNodeChild in subConditionNodeArrayChild )
													{
														string strReschild = EvalNode(subConditionNodeChild);
														if(strReschild == "True")
														{
															XmlNodeList toSetNodeListChild=subConditionNodeChild.SelectNodes("ToSet");
															if(toSetNodeList != null)
															{
																SetKeys(toSetNodeListChild,uTierKeys);
															}
															//Add Inner Level
															XmlNodeList subConditionsNodeArrayInnerChild =subConditionNodeChild.SelectNodes("SubConditions");
															foreach(XmlNode subConditionsNodeInnerChild in  subConditionsNodeArrayInnerChild)
															{
																XmlNodeList subConditionNodeArrayInnerChild = subConditionsNodeInnerChild.SelectNodes("SubCondition");
																if(subConditionNodeArrayInnerChild!=null)
																{
																	foreach(XmlNode subInnerChild in subConditionNodeArrayInnerChild)
																	{
																		string strResInnerchild = EvalNode(subInnerChild);
																		if(strResInnerchild == "True")
																		{
																			XmlNodeList toSetNodeListInnerChild=subInnerChild.SelectNodes("ToSet");
																			if(toSetNodeListInnerChild != null)
																			{
																				SetKeys(toSetNodeListInnerChild,uTierKeys);
																			}																			
																			break;
																		}
																	}
																}
															}

															//END Inner Level
															break;
														}

													}
												}
											}
											//End Level
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
			return uTierKeys;
		}

		/// <summary>
		/// Returns Effective Master Rules 
		/// </summary>
		/// <param name="parentNode">Parent Group Node</param>
		/// <param name="AppEffectiveDate">Application Effective Date</param>
		/// <param name="StateId">State ID</param>
		/// <returns>Array List Of Master Rules</returns>

		public ArrayList GetEffectiveMasterRules(XmlNode parentNode,DateTime AppEffectiveDate,string StateId)
		{
			//XmlNodeList ruleNodes = new XmlNodeList();
			ArrayList ruleNodes = new ArrayList();
			
			DateTime startDate,endDate;
			XmlNodeList masterNodes = parentNode.SelectNodes("Rule[@Action='Master']");
			foreach (XmlNode effectiveRule in masterNodes)
			{
				startDate=Convert.ToDateTime(effectiveRule.Attributes["StartDate"].Value);   
				endDate = Convert.ToDateTime(effectiveRule.Attributes["EndDate"].Value);
				if(AppEffectiveDate >= startDate && AppEffectiveDate <= endDate)
				{
					if(effectiveRule.Attributes["STATE_ID"] == null ||
						effectiveRule.Attributes["STATE_ID"].Value == StateId)
					{
						ruleNodes.Add(effectiveRule);
					}
				}
			}
			return ruleNodes;
		
		}
		
		#region EvalNode
		/// <summary>
		/// Evaluate An XML Condition Node
		/// </summary>
		/// <param name="node">XML Condition Node </param>
		/// <returns>Result Of Expression In Condition Node</returns>
		public string EvalNode(XmlNode node)
		{
			ClsCommon obj = new ClsCommon();
			return obj.EvalNode(node,uTierKeys); 
		}
	
		public void SetKeys(XmlNodeList toSetNodeArray,Hashtable masterKeys)
		{
			ClsCommon obj = new ClsCommon();
			foreach(XmlNode toSetNode in toSetNodeArray)
			{
				string strKey=toSetNode.Attributes["Key"].Value;
				string strValue=toSetNode.Attributes["Value"].Value;
				if(strKey.StartsWith("$"))
				{
					strKey=strKey.Substring(strKey.IndexOf('$')+1);
				}
				if(strValue.Trim() == "")
				{
					strValue = obj.EvalNode(toSetNode,masterKeys);  
				}
				else if(strValue.StartsWith("$"))
				{
					strValue=strValue.Substring(strValue.IndexOf('$')+1);
					strValue = masterKeys[strValue].ToString();
				}


				if(masterKeys.ContainsKey(strKey))
				{
					masterKeys.Remove(strKey);
				}
				masterKeys.Add(strKey,strValue);
			}
		}
		
		#endregion 

		#endregion
		#endregion		

		#region UPDATE UNDERWRITER TIER METHODS ---APP/POL/PRIOR LOSS/PRIOR POLICY/RENEAL
		private bool IsUnderwritingTierApplicable(int CustomerID, int PolID, int PolVersionID,DataWrapper objDataWrapper)
		{	
			SqlParameter paramRetVal = null;
			string strQuery = "Proc_GetTierForUnderWriterRenewal";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
			objDataWrapper.AddParameter("@POLICY_ID",PolID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolVersionID);			

			paramRetVal  = (SqlParameter) objDataWrapper.AddParameter("@RET_VAL",null,SqlDbType.Int,ParameterDirection.ReturnValue);

			DataSet dtKeyValue= objDataWrapper.ExecuteDataSet(strQuery);
			
			objDataWrapper.ClearParameteres();

			
			if(int.Parse(paramRetVal.Value.ToString())== 1)
				return true;
			else
				return false;

			
		}

		/// <summary>
		/// Commom Underwriting Tier to Update UTIER -
		/// </summary>
		/// <param name="CustomerId"></param>
		/// <param name="Id"></param>
		/// <param name="VersionId"></param>
		/// <param name="strLevel"></param>
		/// <param name="objDataWrapper"></param>
		/// <returns></returns>
		public string UpdateUnderwritingTier(int CustomerId, int Id, int VersionId, string strLevel,DataWrapper objDataWrapper)
		{
			return UpdateUnderwritingTier(CustomerId, Id, VersionId, strLevel, "",objDataWrapper);
		}
		public string UpdateUnderwritingTier(int CustomerId, int Id, int VersionId, string strLevel,string is_Renewal,DataWrapper objDataWrapper)
		{

			if(is_Renewal.ToUpper()=="Y")
			{
				if(!IsUnderwritingTierApplicable(CustomerId, Id, VersionId, objDataWrapper))
					return "";
			}
			
			string UW_TIER = "";

			if(strLevel!="QQ")
                InitialiseRules(CustomerId,Id,VersionId,strLevel,objDataWrapper);			

			string ruleTypeUT = "AUTO";

			XmlNode groupNode= RuleDoc.SelectSingleNode("Root/Group[@Code='" + ruleTypeUT.ToString() + "']" );
			
			ArrayList masterRuleNodeArrayUT = GetEffectiveMasterRules(groupNode,AppEffectiveDate,StateId);

			Hashtable uTier = new Hashtable();
			uTier = SetMasterKeysUT(masterRuleNodeArrayUT);

			if(uTier!=null)
			{
				if(uTier["U_TIER"]!=null)
				{
					UW_TIER =  uTier["U_TIER"].ToString();
				}
			}
            
			if(strLevel.ToUpper() == "QQ")
				return UW_TIER;
			else
                UpdateTier(CustomerId,Id,VersionId,UW_TIER,strLevel,objDataWrapper);

            return "1";
			
		
		}

		/// <summary>
		/// Update Tier APP / POl Main Pages
		/// </summary>
		/// <param name="CustomerId"></param>
		/// <param name="Id"></param>
		/// <param name="VersionId"></param>
		/// <param name="UW_TIER"></param>
		/// <param name="strLevel"></param>
		/// <param name="objDataWrapper"></param>
		/// <returns></returns>
		public int UpdateTier(int CustomerId, int Id, int VersionId,string UW_TIER,string strLevel,DataWrapper objDataWrapper)
		{
			
			int returnResult=0;
			string		strStoredProc	=	"[Proc_Update_UNDERWRITING_TIER]";
			DateTime	RecordDate		=	DateTime.Now;
			
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objDataWrapper.AddParameter("@ID",Id);
				objDataWrapper.AddParameter("@VERSION_ID",VersionId);
				objDataWrapper.AddParameter("@UNDERWRITING_TIER",UW_TIER);
				objDataWrapper.AddParameter("@LEVEL",strLevel);

				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
						
				objDataWrapper.ClearParameteres();
							
				return returnResult;				
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				
			}
		}


		/// <summary>
		/// FETCH POLICIES FOR UT -->PRIOR INFO
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="objDataWrapper"></param>
		/// <returns></returns>
		public DataSet GetUTPolicies(int customerID,DataWrapper objDataWrapper)
		{
			string sqlSelect = "FetchPoliciesForUT";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",customerID );
			DataSet dsUT = objDataWrapper.ExecuteDataSet(sqlSelect);
			return dsUT;
		}

		/// <summary>
		/// UPDATE UT FOR POL/APP
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="objDataWrapper"></param>
		public void UpdateUTierPriorInfo(int customerID,DataWrapper objDataWrapper)
		{
            DataSet dsUT = GetUTPolicies(customerID,objDataWrapper);
			if(dsUT!=null && dsUT.Tables.Count > 0)
			{
				foreach(DataRow drRows in dsUT.Tables[0].Rows)
				{
					UpdateUnderwritingTier(int.Parse(drRows["CUSTOMER_ID"].ToString()),
											int.Parse(drRows["ID"].ToString()),
											int.Parse(drRows["VERSION_ID"].ToString()),
											drRows["LEVEL"].ToString(),
											objDataWrapper);
					
				}
			}
			
		}
		#endregion	

		#region QQ Utier
		public string GetUnderWritingTierQQ(string lapseDays,string priorLimit,string totalNAF,string limitType,string effectiveDate,string stateID)
		{
            string strUtier="";
			Cms.DataLayer.DataWrapper objWrapper = new Cms.DataLayer.DataWrapper(ClsCommon.ConnStr ,CommandType.StoredProcedure);
			ClsUnderwritingTier objTier = new ClsUnderwritingTier();
			objTier.InitialiseRulesQQ(lapseDays,priorLimit,totalNAF,limitType,effectiveDate,stateID,"QQ",objWrapper);
			strUtier = objTier.UpdateUnderwritingTier(0,0,0,"QQ","",objWrapper);
			return strUtier;
		}
		#endregion
	
	}
}
