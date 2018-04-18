/******************************************************************************************
<Author					: -  Ravindra Kumar Gupta
<Start Date				: -	 03/06/2006
<End Date				: -	
<Description			: -  Business Logic for SCHEDULE OF UNDERLYING
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: -	Ravindra	
<Modified By			: - 03-22-206
<Purpose				: - To Add Policy Level Functions
************************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using Cms.Model.Application;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
using System.Collections;
using System.Xml;
using System.Text;

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ScheduleOfUnderlying.
	/// </summary>
	public class ClsScheduleOfUnderlying : Cms.BusinessLayer.BlApplication.clsapplication
	{
		public ClsScheduleOfUnderlying()
		{
            
		}

		#region Add Function

		public int Add(ScheduleOfUnderlyingInfo objInfo)
		{
			string		strStoredProc	=	"Proc_INSERT_UMBRELLA_UNDERLYING_POLICIES";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
				if(objInfo.POLICY_NUMBER !="")
				{
					objDataWrapper.AddParameter("@POLICY_NUMBER",objInfo.POLICY_NUMBER);
				}
				else
				{
					objDataWrapper.AddParameter("@POLICY_NUMBER",null);
				}				
				objDataWrapper.AddParameter("@IS_POLICY",objInfo.IS_POLICY);				
				if(objInfo.POLICY_LOB!="")
				{
					objDataWrapper.AddParameter("@POLICY_LOB",objInfo.POLICY_LOB);
				}
				else
				{
					objDataWrapper.AddParameter("@POLICY_LOB",null);
				}
				if(objInfo.POLICY_COMPANY!="")
				{
					objDataWrapper.AddParameter("@POLICY_COMPANY",objInfo.POLICY_COMPANY);
				}
				else
				{
					objDataWrapper.AddParameter("@POLICY_COMPANY",null);
				}
				if(objInfo.POLICY_TERMS != "")
				{
					objDataWrapper.AddParameter("@POLICY_TERMS",objInfo.POLICY_TERMS);
				}
				else
				{
					objDataWrapper.AddParameter("@POLICY_TERMS",null);
				}
				if(objInfo.POLICY_START_DATE.Ticks !=0)
				{
					objDataWrapper.AddParameter("@POLICY_START_DATE",objInfo.POLICY_START_DATE);
				}
				else
				{
					objDataWrapper.AddParameter("@POLICY_START_DATE",null);
				}
				if(objInfo.POLICY_EXPIRATION_DATE.Ticks !=0)
				{
					objDataWrapper.AddParameter("@POLICY_EXPIRATION_DATE",objInfo.POLICY_EXPIRATION_DATE);
				}
				else
				{
					objDataWrapper.AddParameter("@POLICY_EXPIRATION_DATE",null);
				}
				
				//added by Manoj Rathore
				if(objInfo.EXCLUDE_UNINSURED_MOTORIST !=Convert.ToInt32(null))
				{
					objDataWrapper.AddParameter("@EXCLUDE_UNINSURED_MOTORIST",objInfo.EXCLUDE_UNINSURED_MOTORIST);
				}
				else
				{
					objDataWrapper.AddParameter("@EXCLUDE_UNINSURED_MOTORIST",null);
				}
				/*added by Manoj Rathore
				if(objInfo.OFFICE_PREMISES !=Convert.ToInt32(null))
				{
					objDataWrapper.AddParameter("@OFFICE_PREMISES",objInfo.OFFICE_PREMISES);
				}
				else
				{
					objDataWrapper.AddParameter("@OFFICE_PREMISES",null);
				}
				if(objInfo.RENTAL_DWELLINGS_UNIT !=Convert.ToInt32(null))
				{
					objDataWrapper.AddParameter("@RENTAL_DWELLINGS_UNIT",objInfo.RENTAL_DWELLINGS_UNIT);
				}
				else
				{
					objDataWrapper.AddParameter("@RENTAL_DWELLINGS_UNIT",null);
				}
				*/
				
				if(objInfo.QUESTION != "")
				{
					objDataWrapper.AddParameter("@QUESTION",objInfo.QUESTION);
				}
				else
				{
					objDataWrapper.AddParameter("@QUESTION",null);
				}

				if(objInfo.QUES_DESC != "")
				{
					objDataWrapper.AddParameter("@QUES_DESC",objInfo.QUES_DESC);
				}
				else
				{
					objDataWrapper.AddParameter("@QUES_DESC",null);
				}
				
				objDataWrapper.AddParameter("@HAS_MOTORIST_PROTECTION",objInfo.HAS_MOTORIST_PROTECTION);
				objDataWrapper.AddParameter("@HAS_SIGNED_A9",objInfo.HAS_SIGNED_A9);
				objDataWrapper.AddParameter("@LOWER_LIMITS",objInfo.LOWER_LIMITS);
				objDataWrapper.AddParameter("@POLICY_PREMIUM",objInfo.POLICY_PREMIUM);
				objDataWrapper.AddParameter("@STATE_ID",objInfo.STATE_ID);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@UNDERLYING_POL_ID",objInfo.UNDERLYING_POL_ID,SqlDbType.Int ,ParameterDirection.Output);
				//objDataWrapper.AddParameter("@UNDERLYING_POL_ID",objInfo.UNDERLYING_POL_ID);
				
				int returnResult = 0;
				if(base.TransactionLogRequired)
				{
					objInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/ScheduleOfUnderlying.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC	=	"New " + objInfo.POLICY_COMPANY + " Policy is added in Umbrella Schedule Of Underlying";
					if(objInfo.POLICY_LOB != "2" || objInfo.POLICY_LOB != "3")
					{
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='HAS_MOTORIST_PROTECTION']");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='HAS_SIGNED_A9']");											
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='LOWER_LIMITS']");
					}
					else if (objInfo.HAS_MOTORIST_PROTECTION == 0 && objInfo.LOWER_LIMITS == 0)
					{
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='HAS_SIGNED_A9']");											
					}
					//objTransactionInfo.TRANS_DESC		=	"New Umbrella Schedule Of Underlying is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
									
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
                
				ClsUmbrellaCoverages objCoverage =new ClsUmbrellaCoverages();
  			    returnResult=objCoverage.SaveDefaultCoveragesApp(objDataWrapper,objInfo.CUSTOMER_ID,objInfo.APP_ID,objInfo.APP_VERSION_ID,0);    
				
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				               
				return returnResult;


			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);	
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}
		#endregion


		#region AddPolicy Function

		public int AddPolicy(Cms.Model.Policy.Umbrella.ClsUnderlyingPoliciesInfo  objInfo)
		{
			string		strStoredProc	=	"Proc_INSERT_POL_UMBRELLA_UMDERLYING_POLICIES";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@IS_POLICY",objInfo.IS_POLICY);
				if(objInfo.POLICY_NUMBER !="")
				{
					objDataWrapper.AddParameter("@POLICY_NUMBER",objInfo.POLICY_NUMBER);
				}
				else
				{
					objDataWrapper.AddParameter("@POLICY_NUMBER",null);
				}
				if(objInfo.POLICY_LOB!="")
				{
					objDataWrapper.AddParameter("@POLICY_LOB",objInfo.POLICY_LOB);
				}
				else
				{
					objDataWrapper.AddParameter("@POLICY_LOB",null);
				}
				if(objInfo.POLICY_COMPANY!="")
				{
					objDataWrapper.AddParameter("@POLICY_COMPANY",objInfo.POLICY_COMPANY);
				}
				else
				{
					objDataWrapper.AddParameter("@POLICY_COMPANY",null);
				}
				if(objInfo.POLICY_TERMS != "")
				{
					objDataWrapper.AddParameter("@POLICY_TERMS",objInfo.POLICY_TERMS);
				}
				else
				{
					objDataWrapper.AddParameter("@POLICY_TERMS",null);
				}
				if(objInfo.POLICY_START_DATE.Ticks !=0)
				{
					objDataWrapper.AddParameter("@POLICY_START_DATE",objInfo.POLICY_START_DATE);
				}
				else
				{
					objDataWrapper.AddParameter("@POLICY_START_DATE",null);
				}
				if(objInfo.POLICY_EXPIRATION_DATE.Ticks !=0)
				{
					objDataWrapper.AddParameter("@POLICY_EXPIRATION_DATE",objInfo.POLICY_EXPIRATION_DATE);
				}
				else
				{
					objDataWrapper.AddParameter("@POLICY_EXPIRATION_DATE",null);
				}
				
				if(objInfo.QUESTION != "")
				{
					objDataWrapper.AddParameter("@QUESTION",objInfo.QUESTION);
				}
				else
				{
					objDataWrapper.AddParameter("@QUESTION",null);
				}
				//added by Manoj Rathore
				if(objInfo.EXCLUDE_UNINSURED_MOTORIST !=Convert.ToInt32(null))
				{
					objDataWrapper.AddParameter("@EXCLUDE_UNINSURED_MOTORIST",objInfo.EXCLUDE_UNINSURED_MOTORIST);
				}
				else
				{
					objDataWrapper.AddParameter("@EXCLUDE_UNINSURED_MOTORIST",null);
				}
				/*added by Manoj Rathore
				if(objInfo.OFFICE_PREMISES !=Convert.ToInt32(null))
				{
					objDataWrapper.AddParameter("@OFFICE_PREMISES",objInfo.OFFICE_PREMISES);
				}
				else
				{
					objDataWrapper.AddParameter("@OFFICE_PREMISES",null);
				}
				if(objInfo.RENTAL_DWELLINGS_UNIT !=Convert.ToInt32(null))
				{
					objDataWrapper.AddParameter("@RENTAL_DWELLINGS_UNIT",objInfo.RENTAL_DWELLINGS_UNIT);
				}
				else
				{
					objDataWrapper.AddParameter("@RENTAL_DWELLINGS_UNIT",null);
				}
				*/

				if(objInfo.QUES_DESC != "")
				{
					objDataWrapper.AddParameter("@QUES_DESC",objInfo.QUES_DESC);
				}
				else
				{
					objDataWrapper.AddParameter("@QUES_DESC",null);
				}
				objDataWrapper.AddParameter("@HAS_MOTORIST_PROTECTION",objInfo.HAS_MOTORIST_PROTECTION);
				objDataWrapper.AddParameter("@HAS_SIGNED_A9",objInfo.HAS_SIGNED_A9);
				objDataWrapper.AddParameter("@LOWER_LIMITS",objInfo.LOWER_LIMITS);
				objDataWrapper.AddParameter("@POLICY_PREMIUM",objInfo.POLICY_PREMIUM);
				objDataWrapper.AddParameter("@STATE_ID",objInfo.STATE_ID);

				int returnResult = 0;
				if(base.TransactionLogRequired)
				{
					objInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/Aspx/Umbrella/PolicyAddScheduleOfUnderlying.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    
					objTransactionInfo.TRANS_TYPE_ID	 =	1;
					objTransactionInfo.POLICY_ID		 =  objInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID   =  objInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC	=	"New " + objInfo.POLICY_COMPANY + " Policy is added in Umbrella Schedule Of Underlying";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
									
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
                
									
				
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				               
				return returnResult;


			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);	
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}
		#endregion

		#region AddCoverage Function

		public int AddCoverage(ArrayList alNewCoverages)
		{
			string	strStoredProc =	"Proc_INSERT_UMBRELLA_UMDERLYING_POLICY_COVERAGES";

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.ON);	
			SqlCommand cmdCoverage = new SqlCommand();
			cmdCoverage.CommandText = strStoredProc;
			cmdCoverage.CommandType = CommandType.StoredProcedure;
			
			SqlParameter[] param = new SqlParameter[10];
			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			
			StringBuilder sbTranXML = new StringBuilder();
			
			sbTranXML.Append("<root>");
			string strPolicyCompany = "";
			/*if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}*/
			
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			
			int customerID = 0;
			int appID = 0;
			int appVersionID = 0;
			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Application.ClsUnderlyingPolicyCoveragesInfo objInfo = (Cms.Model.Application.ClsUnderlyingPolicyCoveragesInfo)alNewCoverages[i];
					
					customerID = objInfo.CUSTOMER_ID;
					appID = objInfo.APP_ID;
					appVersionID = objInfo.APP_VERSION_ID;
					objDataWrapper.ClearParameteres();
					objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
					objDataWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
					objDataWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
					objDataWrapper.AddParameter("@COV_CODE",objInfo.COV_CODE);
					if(objInfo.POLICY_NUMBER !="")
					{
						objDataWrapper.AddParameter("@POLICY_NUMBER",objInfo.POLICY_NUMBER);
					}
					else
					{
						objDataWrapper.AddParameter("@POLICY_NUMBER",null);
					}				
					objDataWrapper.AddParameter("@IS_POLICY",objInfo.IS_POLICY);				
					if(objInfo.COVERAGE_DESC != "")
					{
						objDataWrapper.AddParameter("@COVERAGE_DESC",objInfo.COVERAGE_DESC );
					}
					else
					{
						objDataWrapper.AddParameter("@COVERAGE_DESC",null );
					}
					if(objInfo.COVERAGE_AMOUNT !=null)
					{
						objDataWrapper.AddParameter("@COVERAGE_AMOUNT",objInfo.COVERAGE_AMOUNT );
					}
					else
					{
						objDataWrapper.AddParameter("@COVERAGE_AMOUNT",null );
					}
					if(objInfo.POLICY_TEXT  != "")
					{
						objDataWrapper.AddParameter("@POLICY_TEXT",objInfo.POLICY_TEXT );
					}
					else
					{
						objDataWrapper.AddParameter("@POLICY_TEXT",null );
					}
					if(objInfo.POLICY_COMPANY  != "")
					{
						objDataWrapper.AddParameter("@POLICY_COMPANY",objInfo.POLICY_COMPANY );
					}
					else
					{
						objDataWrapper.AddParameter("@POLICY_COMPANY",null );
					}

					if(objInfo.COVERAGE_TYPE != "")
					{
						objDataWrapper.AddParameter("@COVERAGE_TYPE",objInfo.COVERAGE_TYPE );
					}
					else
					{
						objDataWrapper.AddParameter("@COVERAGE_TYPE",null );
					}
					string strTranXML = "";
				
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
					strPolicyCompany = objInfo.POLICY_COMPANY;
					//if ( objInfo.ACTION == "I" )
					//{
						//Insert
						objInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/ScheduleOfUnderlying.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objInfo);
						strTranXML=Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='COVERAGE_TYPE' and @NewValue='S']","NewValue","SPLIT");
						strTranXML=Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='COVERAGE_TYPE' and @NewValue='C']","NewValue","CSL");
						/*objTransactionInfo.TRANS_TYPE_ID			=	2;
						objTransactionInfo.APP_ID					=	objInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID			=	objInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID				=	objInfo.CUSTOMER_ID;
						objTransactionInfo.TRANS_DESC				=	"Vehicle coverage added.";
						objTransactionInfo.CHANGE_XML				=	strTranXML;
						*/
						sbTranXML.Append(strTranXML);
						objDataWrapper.ExecuteNonQuery(strStoredProc);
						//objDataWrapper.ClearParameteres();

				}
				//objWrapper.ClearParameteres();
					sbTranXML.Append("</root>");
					if(sbTranXML.ToString()!="<root></root>")
					{
						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID= appID;
						objTransactionInfo.APP_VERSION_ID = appVersionID;
						objTransactionInfo.CLIENT_ID = customerID;
						objTransactionInfo.TRANS_DESC		=	"New " + strPolicyCompany + " Policy is added in Umbrella Schedule Of Underlying";
						if(sbTranXML.ToString()!="<root></root>")	

						{
							//sbTranXML=Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(sbTranXML,"LabelFieldMapping/Map[@field='COVERAGE_TYPE' and @NewValue='S']","NewValue","SPLIT");
							//sbTranXML=Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(sbTranXML,"LabelFieldMapping/Map[@field='COVERAGE_TYPE' and @NewValue='C']","NewValue","CSL");
							XmlDocument TranXML = new XmlDocument();
							TranXML.LoadXml(sbTranXML.ToString());
							XmlNodeList TransList = TranXML.SelectNodes("root/LabelFieldMapping");
							int index = 0;
							foreach(XmlNode TranNode in TransList)
							{
								XmlNode PolNode = TranNode.SelectSingleNode("Map[@field='POLICY_COMPANY']");
								XmlNode PolNumNode = TranNode.SelectSingleNode("Map[@field='POLICY_NUMBER']");
								XmlNode PolCovType = TranNode.SelectSingleNode("Map[@field='COVERAGE_TYPE']");
								if(index > 0 && PolNode != null)
									TranNode.RemoveChild(PolNode);
								if(index > 0 && PolNumNode != null)
									TranNode.RemoveChild(PolNumNode);
								if(index > 0 && PolCovType != null)
									TranNode.RemoveChild(PolCovType);
								index++;
							}
							objTransactionInfo.CHANGE_XML = TranXML.InnerXml;
						}
						else
							objTransactionInfo.CHANGE_XML		=	sbTranXML.ToString();
						//objTransactionInfo.CUSTOM_INFO=strCustomInfo;
						objDataWrapper.ExecuteNonQuery(objTransactionInfo);
						objDataWrapper.ClearParameteres();
					}
				return 1;
			}
			
		
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);	
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}


		public int AddCoverage(ClsUnderlyingPolicyCoveragesInfo objInfo)
		{
			string		strStoredProc	=	"Proc_INSERT_UMBRELLA_UMDERLYING_POLICY_COVERAGES";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@COV_CODE",objInfo.COV_CODE);
				if(objInfo.POLICY_NUMBER !="")
				{
					objDataWrapper.AddParameter("@POLICY_NUMBER",objInfo.POLICY_NUMBER);
				}
				else
				{
					objDataWrapper.AddParameter("@POLICY_NUMBER",null);
				}				
				objDataWrapper.AddParameter("@IS_POLICY",objInfo.IS_POLICY);				
				if(objInfo.COVERAGE_DESC != "")
				{
					objDataWrapper.AddParameter("@COVERAGE_DESC",objInfo.COVERAGE_DESC );
				}
				else
				{
					objDataWrapper.AddParameter("@COVERAGE_DESC",null );
				}
				if(objInfo.COVERAGE_AMOUNT !=null)
				{
					objDataWrapper.AddParameter("@COVERAGE_AMOUNT",objInfo.COVERAGE_AMOUNT );
				}
				else
				{
					objDataWrapper.AddParameter("@COVERAGE_AMOUNT",null );
				}
				if(objInfo.POLICY_TEXT  != "")
				{
					objDataWrapper.AddParameter("@POLICY_TEXT",objInfo.POLICY_TEXT );
				}
				else
				{
					objDataWrapper.AddParameter("@POLICY_TEXT",null );
				}
				if(objInfo.POLICY_COMPANY  != "")
				{
					objDataWrapper.AddParameter("@POLICY_COMPANY",objInfo.POLICY_COMPANY );
				}
				else
				{
					objDataWrapper.AddParameter("@POLICY_COMPANY",null );
				}

				if(objInfo.COVERAGE_TYPE != "")
				{
					objDataWrapper.AddParameter("@COVERAGE_TYPE",objInfo.COVERAGE_TYPE );
				}
				else
				{
					objDataWrapper.AddParameter("@COVERAGE_TYPE",null );
				}

				/*added by Manoj Rathore
				if(objInfo.EXCLUDE_UNINSURED_MOTORIST !=Convert.ToInt32(null))
				{
					objDataWrapper.AddParameter("@EXCLUDE_UNINSURED_MOTORIST",objInfo.EXCLUDE_UNINSURED_MOTORIST);
				}
				else
				{
					objDataWrapper.AddParameter("@EXCLUDE_UNINSURED_MOTORIST",null);
				}*/

				/* if(objInfo.OFFICE_PREMISES  != Convert.ToInt32(null))
				{
					objDataWrapper.AddParameter("@OFFICE_PREMISES",objInfo.OFFICE_PREMISES );
				}
				else
				{
					objDataWrapper.AddParameter("@OFFICE_PREMISES",null );
				}
				if(objInfo.RENTAL_DWELLINGS_UNIT  !=Convert.ToInt32(null) )
				{
					objDataWrapper.AddParameter("@RENTAL_DWELLINGS_UNIT",objInfo.RENTAL_DWELLINGS_UNIT );
				}
				else
				{
					objDataWrapper.AddParameter("@RENTAL_DWELLINGS_UNIT",null );
				}
				*/

				int returnResult = 0;
				if(base.TransactionLogRequired)
				{
					objInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/ScheduleOfUnderlying.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC	=	"New " + objInfo.POLICY_COMPANY + " Policy is added in Umbrella Schedule Of Underlying";
					//objTransactionInfo.TRANS_DESC		=	"New Umbrella Schedule Of Underlying Coverage is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
									
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
                
									
				
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				               
				return returnResult;

			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);	
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}

		}
		#endregion

		#region AddPolicyCoverage Function
		public int AddPolicyCoverage(ArrayList alNewCoverages)
		{
			string	strStoredProc =	"Proc_INSERT_POL_UMDERLYING_POLICY_COVERAGES";

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.ON);	
			SqlCommand cmdCoverage = new SqlCommand();
			cmdCoverage.CommandText = strStoredProc;
			cmdCoverage.CommandType = CommandType.StoredProcedure;
			
			SqlParameter[] param = new SqlParameter[10];
			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			
			StringBuilder sbTranXML = new StringBuilder();
			
			sbTranXML.Append("<root>");
			string strPolicyCompany = "";
			/*if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}*/
			
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			
			int customerID = 0;
			int polID = 0;
			int polVersionID = 0;
			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Policy.Umbrella.ClsUnderlyingPolicyCoverages objInfo = (Cms.Model.Policy.Umbrella.ClsUnderlyingPolicyCoverages)alNewCoverages[i];
					
					customerID = objInfo.CUSTOMER_ID;
					polID = objInfo.POLICY_ID;
					polVersionID = objInfo.POLICY_VERSION_ID;
					objDataWrapper.ClearParameteres();
					objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
					objDataWrapper.AddParameter("@POLICY_ID",objInfo.POLICY_ID);
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",objInfo.POLICY_VERSION_ID);
					objDataWrapper.AddParameter("@COV_CODE",objInfo.COV_CODE);
								
					objDataWrapper.AddParameter("@IS_POLICY",objInfo.IS_POLICY);				
					if(objInfo.COVERAGE_DESC != "")
					{
						objDataWrapper.AddParameter("@COVERAGE_DESC",objInfo.COVERAGE_DESC );
					}
					else
					{
						objDataWrapper.AddParameter("@COVERAGE_DESC",null );
					}
					if(objInfo.COVERAGE_AMOUNT !=null)
					{
						objDataWrapper.AddParameter("@COVERAGE_AMOUNT",objInfo.COVERAGE_AMOUNT );
					}
					else
					{
						objDataWrapper.AddParameter("@COVERAGE_AMOUNT",null );
					}
					if(objInfo.POLICY_TEXT  != "")
					{
						objDataWrapper.AddParameter("@POLICY_TEXT",objInfo.POLICY_TEXT );
					}
					else
					{
						objDataWrapper.AddParameter("@POLICY_TEXT",null );
					}
					if(objInfo.POLICY_NUMBER  != "")
					{
						objDataWrapper.AddParameter("@POLICY_NUMBER",objInfo.POLICY_NUMBER );
					}
					else
					{
						objDataWrapper.AddParameter("@POLICY_NUMBER",null );
					}

					if(objInfo.COVERAGE_TYPE != "")
					{
						objDataWrapper.AddParameter("@COVERAGE_TYPE",objInfo.COVERAGE_TYPE );
					}
					else
					{
						objDataWrapper.AddParameter("@COVERAGE_TYPE",null );
					}
					objDataWrapper.AddParameter("@IS_POLICY",objInfo.IS_POLICY);		

					if(objInfo.POLICY_COMPANY  != "")
					{
						objDataWrapper.AddParameter("@POLICY_COMPANY",objInfo.POLICY_COMPANY );
					}
					else
					{
						objDataWrapper.AddParameter("@POLICY_COMPANY",null );
					}

					string strTranXML = "";
				
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
					strPolicyCompany = objInfo.POLICY_TEXT;
					//if ( objInfo.ACTION == "I" )
					//{
					//Insert
					objInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/Aspx/Umbrella/PolicyAddScheduleOfUnderlying.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					strTranXML = objBuilder.GetTransactionLogXML(objInfo);
					
					strTranXML=Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='COVERAGE_TYPE' and @NewValue='S']","NewValue","SPLIT");
					strTranXML=Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='COVERAGE_TYPE' and @NewValue='C']","NewValue","CSL");
					/*objTransactionInfo.TRANS_TYPE_ID			=	2;
						objTransactionInfo.APP_ID					=	objInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID			=	objInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID				=	objInfo.CUSTOMER_ID;
						objTransactionInfo.TRANS_DESC				=	"Vehicle coverage added.";
						objTransactionInfo.CHANGE_XML				=	strTranXML;
						*/
					sbTranXML.Append(strTranXML);
					objDataWrapper.ExecuteNonQuery(strStoredProc);
					//objDataWrapper.ClearParameteres();

				}
				//objWrapper.ClearParameteres();
				sbTranXML.Append("</root>");
				if(sbTranXML.ToString()!="<root></root>")
				{
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.POLICY_ID= polID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = polVersionID;
					objTransactionInfo.CLIENT_ID = customerID;
					objTransactionInfo.TRANS_DESC		=	"New " + strPolicyCompany + " Policy is added in Umbrella Schedule Of Underlying";
					if(sbTranXML.ToString()!="<root></root>")
					{
						XmlDocument TranXML = new XmlDocument();
						TranXML.LoadXml(sbTranXML.ToString());
						XmlNodeList TransList = TranXML.SelectNodes("root/LabelFieldMapping");
						int index = 0;
						foreach(XmlNode TranNode in TransList)
						{
							XmlNode PolNode = TranNode.SelectSingleNode("Map[@field='POLICY_COMPANY']");
							XmlNode PolNumNode = TranNode.SelectSingleNode("Map[@field='POLICY_NUMBER']");
							XmlNode PolCovType = TranNode.SelectSingleNode("Map[@field='COVERAGE_TYPE']");
							if(index > 0 && PolNode != null)
								TranNode.RemoveChild(PolNode);
							if(index > 0 && PolNumNode != null)
								TranNode.RemoveChild(PolNumNode);
							if(index > 0 && PolCovType != null)
								TranNode.RemoveChild(PolCovType);
							index++;
						}
						objTransactionInfo.CHANGE_XML = TranXML.InnerXml;
					}
					else
						objTransactionInfo.CHANGE_XML		=	sbTranXML.ToString();
					//objTransactionInfo.CUSTOM_INFO=strCustomInfo;
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
					objDataWrapper.ClearParameteres();
				}
				return 1;
			}
			
		
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);	
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}


		
		public int AddPolicyCoverage(Cms.Model.Policy.Umbrella.ClsUnderlyingPolicyCoverages  objInfo)
		{
			string		strStoredProc	=	"Proc_INSERT_POL_UMDERLYING_POLICY_COVERAGES";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objInfo.POLICY_VERSION_ID );
				objDataWrapper.AddParameter("@COV_CODE",objInfo.COV_CODE);
				if(objInfo.POLICY_NUMBER !="")
				{
					objDataWrapper.AddParameter("@POLICY_NUMBER",objInfo.POLICY_NUMBER);
				}
				else
				{
					objDataWrapper.AddParameter("@POLICY_NUMBER",null);
				}
				objDataWrapper.AddParameter("@IS_POLICY",objInfo.IS_POLICY);
				if(objInfo.COVERAGE_DESC != "")
				{
					objDataWrapper.AddParameter("@COVERAGE_DESC",objInfo.COVERAGE_DESC );
				}
				else
				{
					objDataWrapper.AddParameter("@COVERAGE_DESC",null );
				}
				if(objInfo.COVERAGE_AMOUNT !=null)
				{
					objDataWrapper.AddParameter("@COVERAGE_AMOUNT",objInfo.COVERAGE_AMOUNT );
				}
				else
				{
					objDataWrapper.AddParameter("@COVERAGE_AMOUNT",null );
				}
				if(objInfo.POLICY_TEXT  != "")
				{
					objDataWrapper.AddParameter("@POLICY_TEXT",objInfo.POLICY_TEXT );
				}
				else
				{
					objDataWrapper.AddParameter("@POLICY_TEXT",null );
				}
				if(objInfo.COVERAGE_TYPE != "")
				{
					objDataWrapper.AddParameter("@COVERAGE_TYPE",objInfo.COVERAGE_TYPE );
				}
				else
				{
					objDataWrapper.AddParameter("@COVERAGE_TYPE",null );
				}


				int returnResult = 0;
				if(base.TransactionLogRequired)
				{
					objInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/Aspx/Umbrella/PolicyAddScheduleOfUnderlying.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID  = objInfo.POLICY_ID ;
					objTransactionInfo.POLICY_VER_TRACKING_ID  = objInfo.POLICY_VERSION_ID ;
					objTransactionInfo.CLIENT_ID = objInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New "+ objInfo.POLICY_NUMBER + " Policy is added in Umbrella Schedule Of Underlying";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
									
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
                
									
				
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				               
				return returnResult;

			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);	
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}

		}
		#endregion

		#region Delete Function

		public void Delete(int customerID,int appID,int appVersionID,string policyNumber)
		{
			string	strStoredProc =	"Proc_DeleteScheduleOfUnderlying";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@POLICY_NO",policyNumber);
			
			try
			{ 
				objWrapper.ExecuteNonQuery(strStoredProc);
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);	

				

		}

		public int Delete(ScheduleOfUnderlyingInfo objInfo)
		{
			int returnResult = 0;

			string	strStoredProc =	"Proc_DeleteScheduleOfUnderlying";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
			objWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
			if(objInfo.POLICY_NUMBER !="")
			{
				objWrapper.AddParameter("@POLICY_NO",objInfo.POLICY_NUMBER);
			}
			else
			{
				objWrapper.AddParameter("@POLICY_NO",null);
			}
			
			try
			{
				//if transaction required
				if ( base.TransactionLogRequired)
				{
					objInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\Aspx\ScheduleOfUnderlying.aspx.resx");
				}
				
				if( base.TransactionLogRequired) 
				{
					returnResult	=					objWrapper.ExecuteNonQuery(strStoredProc);

					if(returnResult > 0)
					{
//						objInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/ScheduleOfUnderlying.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
//						string strTranXML = objBuilder.GetTransactionLogXML(objInfo);	
							
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.APP_ID			= objInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	= objInfo.APP_VERSION_ID;
						objTransactionInfo.CUSTOM_INFO		= ";Company: " +  objInfo.POLICY_COMPANY;
						objTransactionInfo.CLIENT_ID		= objInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
						objTransactionInfo.TRANS_DESC		=	objInfo.POLICY_COMPANY + " Policy is deleted from Umbrella Schedule Of Underlying";
//						objTransactionInfo.CHANGE_XML		=	strTranXML;
						objTransactionInfo.CHANGE_XML		=	"";


						objWrapper.ExecuteNonQuery(objTransactionInfo);
					
						returnResult	=	1;
					}
				}
				else//if no transaction required
				{
					returnResult	=		objWrapper.ExecuteNonQuery(strStoredProc);

		
				}
					
									
				objWrapper.ClearParameteres();
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);

				return returnResult;
			}

			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);	

				

		}


		#endregion

		#region DeletePolicy Function

		public void DeletePolicy(int customerID,int policyID,int policyVersionID,string policyNumber)
		{
			string	strStoredProc =	"Proc_DeletePolicyScheduleOfUnderlying";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",policyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
			objWrapper.AddParameter("@POLICY_NO",policyNumber);
			
			try
			{ 
				objWrapper.ExecuteNonQuery(strStoredProc);
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);	

				

		}


		#endregion

		#region GetPolicyForCustomer

		public static DataTable GetPolicyForCustomer(int customerID,int lobID)
		{
			string	strStoredProc	=	"Proc_Get_Policy_For_Customer";	
			DataWrapper objDataWrapper=new DataWrapper (ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);	
			objDataWrapper.AddParameter("@LOB_ID",lobID);	
			DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
			return ds.Tables[0];

		}
		#endregion 

		#region GetPolicyInformationForUmbrella
		public static string GetPolicyInformationForUmbrella(string PolNumber)
		{
			string	strStoredProc	=	"Proc_GetPolicyInformationFromPolNumber";	
			DataWrapper objDataWrapper=new DataWrapper (ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@POLICY_NUMBER",PolNumber);	
			DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
			if(ds.Tables[0].Rows.Count > 0)
				return ds.Tables[0].Rows[0]["POLICY_INFO"].ToString();
			return "";
		}


		#endregion
		#region CheckMotorist
		public void CheckMotorist(int customerID,int appID,int appVersionID, string calledFrom, out int protectionRejected, out int protectionLower)
		{
			string	strStoredProc	=	"Proc_CheckMotorist";	
			protectionRejected = protectionLower = 2;
			DataWrapper objDataWrapper=new DataWrapper (ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);	
			objDataWrapper.AddParameter("@APP_ID",appID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objDataWrapper.AddParameter("@CALLED_FROM",calledFrom);
			
			//SqlParameter ObjSqlParameter= (SqlParameter) objDataWrapper.AddParameter("@RESULT",0,SqlDbType.Int ,ParameterDirection.Output);
			SqlParameter ObjSqlParameterReject= (SqlParameter) objDataWrapper.AddParameter("@RESULT_REJECTED",0,SqlDbType.Int ,ParameterDirection.Output);
			SqlParameter ObjSqlParameterLower= (SqlParameter) objDataWrapper.AddParameter("@RESULT_LOWER",0,SqlDbType.Int ,ParameterDirection.Output);

			objDataWrapper.ExecuteNonQuery(strStoredProc);
			if (ObjSqlParameterReject.Value!=System.DBNull.Value)
				protectionRejected = int.Parse(ObjSqlParameterReject.Value.ToString());
			
			if (ObjSqlParameterLower.Value!=System.DBNull.Value)
				protectionLower = int.Parse(ObjSqlParameterLower.Value.ToString());

		}
		#endregion 
		#region CheckSignedForm 
		public int CheckSignedForm(int customerID,int appID,int appVersionID, string calledFrom)
		{
			string	strStoredProc	=	"Proc_CheckSignedForm";	
			int returnResult = 0;
			DataWrapper objDataWrapper=new DataWrapper (ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);	
			objDataWrapper.AddParameter("@APP_ID",appID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objDataWrapper.AddParameter("@CALLED_FROM",calledFrom);
			
			SqlParameter ObjSqlParameter= (SqlParameter) objDataWrapper.AddParameter("@RESULT",0,SqlDbType.Int ,ParameterDirection.Output);

			returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
			if (ObjSqlParameter.Value!=System.DBNull.Value)
				return int.Parse(ObjSqlParameter.Value.ToString());
			else
				return returnResult;

		}
		#endregion 

		#region GetScheduleOfUnderlying Function
		public static DataTable GetScheduleOfUnderlying(int customerId ,int appId,int appVersionId,string policyNumber)
		{
			return  GetScheduleOfUnderlying(customerId ,appId,appVersionId,policyNumber,"");
				   
		}

		public static DataTable GetScheduleOfUnderlying(int customerId ,int appId,int appVersionId,string policyNumber,string policyCompany)
		{
			string		strStoredProc	=	"Proc_GetScheduleOfUnderlyingInfo";
			
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",appId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_NO",policyNumber,SqlDbType.VarChar );
				objDataWrapper.AddParameter("@POLICY_COMPANY",policyCompany,SqlDbType.VarChar );
            

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
	
			return dsCount.Tables[0];
		}
		#endregion

		#region GetPolicyScheduleOfUnderlying Function
		public static DataTable GetPolicyScheduleOfUnderlying(int customerId ,int policyId,int policyVersionId,string policyNumber)
		{
			string		strStoredProc	=	"Proc_GetPolicyScheduleOfUnderlyingInfo";
			
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",policyId ,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionId ,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_NO",policyNumber,SqlDbType.VarChar );
                

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
	
			return dsCount.Tables[0];
		}
		#endregion

		#region GetScheduleOfUnderlyingCoverages Function
		public static DataTable GetScheduleOfUnderlyingCoverages(int customerId ,int appId,int appVersionId,string policyNumber)
		{
				return GetScheduleOfUnderlyingCoverages(customerId ,appId,appVersionId,policyNumber,"");
		}

		public static DataTable GetScheduleOfUnderlyingCoverages(int customerId ,int appId,int appVersionId,string policyNumber,string policyCompany)
		{
			string		strStoredProc	=	"Proc_GetScheduleOfUnderlyingCoverages";
			
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",appId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_NO",policyNumber,SqlDbType.VarChar );
				objDataWrapper.AddParameter("@POLICY_COMPANY",policyCompany,SqlDbType.VarChar );
                

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
	
			return dsCount.Tables[0];
		}
		#endregion

		#region GetPolicyScheduleOfUnderlyingCoverages Function
		public static DataTable GetPolicyScheduleOfUnderlyingCoverages(int customerId ,int policyId,int policyVersionId,string policyNumber)
		{
			string		strStoredProc	=	"Proc_GetPolicyScheduleOfUnderlyingCoverages";
			
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",policyId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_NO",policyNumber,SqlDbType.VarChar );
                

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
	
			return dsCount.Tables[0];
		}
		#endregion

		#region GetPolicyCoveragesAuto Function
		public static DataTable GetPolicyCoveragesAuto(int customerId ,int policyId,int policyVerId, string strCalledFrom)
		{
			string		strStoredProc	=	"Proc_GetPolicyCoveragesAuto";
			
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",policyId,SqlDbType.Int  );
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVerId,SqlDbType.Int  );
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom,SqlDbType.VarChar);
                

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
	
			return dsCount.Tables[0];
		}
		#endregion

		#region GetPolicyCoveragesWatercraft Function
		public static DataTable GetPolicyCoveragesWatercraft(int customerId ,int policyId,int policyVerId, string strCalledFrom)
		{
			string		strStoredProc	=	"Proc_GetPolicyCoveragesWatercraft";
			
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",policyId,SqlDbType.Int  );
                objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom,SqlDbType.VarChar);
				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
	
			return dsCount.Tables[0];
		}
		#endregion

		#region GetPolicyCoveragesHome Function
		public static DataTable GetPolicyCoveragesHome(int customerId ,int policyId,int policyVerId, string strCalledFrom)
		{
			string		strStoredProc	=	"Proc_GetPolicyCoveragesHome";
			
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",policyId,SqlDbType.Int  );
                objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVerId,SqlDbType.Int); 
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom,SqlDbType.VarChar);

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
	
			return dsCount.Tables[0];
		}
		#endregion

		#region GetPolicyTerms Function
		public static DataTable GetPolicyTerms(int customerId ,int policyId,int policyVerId,string strCalledFrom)
		{
			string		strStoredProc	=	"Proc_GetPolicyTerms";
		
			DataSet dsCount=null;
          		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",policyId,SqlDbType.Int  );
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom,SqlDbType.VarChar);
				    
				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
	
			}
			catch(Exception ex)
			{
				throw(ex);
			}

			return dsCount.Tables[0];
		}
		#endregion

		#region GetUmbrellaLob Function
		public static DataTable GetUmbrellaLob(int customerId ,int appId,int appVerId)
		{
			string		strStoredProc	=	"Proc_GetUmbrellaLobForCustomer";
		
			DataSet dsCount=null;
          		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",appId,SqlDbType.Int  );
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVerId,SqlDbType.Int);
				    
				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
	
			}
			catch(Exception ex)
			{
				throw(ex);
			}

			return dsCount.Tables[0];
		}
		#endregion







	}
}
