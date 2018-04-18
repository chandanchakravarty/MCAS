/******************************************************************************************
<Author				: - Santosh Kumar Gautam
<Start Date			: - 09 Nov, 2010
<End Date			: -	
<Description		: - Model Class for Risk Information page functionality.
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: -
<Modified By		: - 
<Purpose			: - 
*******************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.Model.Support;
using Cms.EbixDataTypes;
using System.Data;
using System.Collections;
using Cms.EbixDataLayer;
using System.Data.SqlClient;


namespace Cms.Model.Claims
{
    /// <summary>
    /// Database Model for Reserve Details
    ///  </summary>
    [Serializable]
    public class ClsAddReserveDetailsInfo : ClsModelBaseClass
    {
        /// <summary>
        /// Initialize the default value 
        /// </summary>
       public ClsAddReserveDetailsInfo()
        {
            //this.SetColumnsName();
          this.PropertyCollection();
        }
       #region Delare the add the parameter collection for the data wrapper class
       /// <summary>
       /// Use to add the parameter collection for the data wrapper class
       /// </summary>
       private void PropertyCollection()
       {
           /* if (ClauseName == base.SelectClause)
            {
                base.htPropertyCollection.Add("PERIL_ID", PERIL_ID);
            }*/
           //if (ClauseName == base.AddClause || ClauseName == "")
           //{

           base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
           base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
           base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);

           base.htPropertyCollection.Add("RISK_ID", RISK_ID);
           base.htPropertyCollection.Add("ACTIVITY_ID", ACTIVITY_ID);
           base.htPropertyCollection.Add("COVERAGE_ID", COVERAGE_ID);
           base.htPropertyCollection.Add("CLAIM_ID", CLAIM_ID);
           base.htPropertyCollection.Add("RESERVE_ID", RESERVE_ID);
           base.htPropertyCollection.Add("OUTSTANDING", OUTSTANDING);
           base.htPropertyCollection.Add("RI_RESERVE", RI_RESERVE);
           base.htPropertyCollection.Add("CO_RESERVE", CO_RESERVE);
           base.htPropertyCollection.Add("PREV_OUTSTANDING", PREV_OUTSTANDING);
           base.htPropertyCollection.Add("PAYMENT_AMOUNT", PAYMENT_AMOUNT);
           base.htPropertyCollection.Add("RECOVERY_AMOUNT", RECOVERY_AMOUNT);
           base.htPropertyCollection.Add("ACTIVITY_TYPE", ACTIVITY_TYPE);
           base.htPropertyCollection.Add("ACTION_ON_PAYMENT", ACTION_ON_PAYMENT);
           base.htPropertyCollection.Add("DEDUCTIBLE_1", DEDUCTIBLE_1);
           base.htPropertyCollection.Add("ADJUSTED_AMOUNT", ADJUSTED_AMOUNT);
           base.htPropertyCollection.Add("PERSONAL_INJURY", PERSONAL_INJURY);
           base.htPropertyCollection.Add("VICTIM_ID", VICTIM_ID);
        
       }//private void PropertyCollection()s


       #endregion

       #region Declare the Property for every data table columns

       /// <summary>
       /// Declare the Property for every data table columns 
       /// </summary>
       public EbixInt32 CUSTOMER_ID
       {
           get
           {
               return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CUSTOMER_ID"]) == null ? new EbixInt32("CUSTOMER_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CUSTOMER_ID"]);
           }
           set
           {
               ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CUSTOMER_ID"]).CurrentValue = Convert.ToInt32(value);
           }
       }//public EbixInt32 CUSTOMER_ID 

       public EbixInt32 POLICY_ID
       {
           get
           {
               return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_ID"]) == null ? new EbixInt32("POLICY_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_ID"]);
           }
           set
           {
               ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_ID"]).CurrentValue = Convert.ToInt32(value);
           }
       }//public EbixInt32 POLICY_ID

       public EbixInt32 POLICY_VERSION_ID
       {
           get
           { //return _POLICY_VERSION_ID;
               return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]) == null ? new EbixInt32("POLICY_VERSION_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]);
           }
           set
           {
               //_POLICY_VERSION_ID.CurrentValue = Convert.ToInt32(value);
               ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]).CurrentValue = Convert.ToInt32(value);
           }
       }//public EbixInt32 RISK_ID 
       public EbixInt32 RISK_ID
       {
           get
           { //return RISK_ID;
               return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RISK_ID"]) == null ? new EbixInt32("RISK_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RISK_ID"]);
           }
           set
           {
               //RISK_ID.CurrentValue = Convert.ToInt32(value);
               ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RISK_ID"]).CurrentValue = Convert.ToInt32(value);
           }
       }//public EbixInt32 RISK_ID 

       public EbixInt32 ACTIVITY_ID
       {
           get
           {
               return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ACTIVITY_ID"]) == null ? new EbixInt32("ACTIVITY_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ACTIVITY_ID"]);
           }
           set
           {

               ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ACTIVITY_ID"]).CurrentValue = Convert.ToInt32(value);
           }
       }
       public EbixInt32 CLAIM_ID
       {
           get
           {
               return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAIM_ID"]) == null ? new EbixInt32("CLAIM_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAIM_ID"]);
           }
           set
           {

               ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAIM_ID"]).CurrentValue = Convert.ToInt32(value);
           }
       }


       public EbixInt32 COVERAGE_ID
       {
           get
           {
               return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COVERAGE_ID"]) == null ? new EbixInt32("COVERAGE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COVERAGE_ID"]);
           }
           set
           {

               ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COVERAGE_ID"]).CurrentValue = Convert.ToInt32(value);
           }
       }

       public EbixInt32 RESERVE_ID
       {
           get
           {
               return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RESERVE_ID"]) == null ? new EbixInt32("RESERVE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RESERVE_ID"]);
           }
           set
           {

               ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RESERVE_ID"]).CurrentValue = Convert.ToInt32(value);
           }
       }
       public EbixDouble OUTSTANDING
       {
           get
           {
               return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["OUTSTANDING"]) == null ? new EbixDouble("OUTSTANDING") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["OUTSTANDING"]);
           }
           set
           {
               ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["OUTSTANDING"]).CurrentValue = Convert.ToDouble(value);
           }
       }

       public EbixDouble RI_RESERVE
       {
           get
           {
               return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RI_RESERVE"]) == null ? new EbixDouble("RI_RESERVE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RI_RESERVE"]);
           }
           set
           {
               ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RI_RESERVE"]).CurrentValue = Convert.ToDouble(value);
           }
       }
       public EbixDouble CO_RESERVE
       {
           get
           {
               return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["CO_RESERVE"]) == null ? new EbixDouble("CO_RESERVE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["CO_RESERVE"]);
           }
           set
           {
               ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["CO_RESERVE"]).CurrentValue = Convert.ToDouble(value);
           }
       }

       public EbixDouble PREV_OUTSTANDING
       {
           get
           {
               return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["PREV_OUTSTANDING"]) == null ? new EbixDouble("PREV_OUTSTANDING") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["PREV_OUTSTANDING"]);
           }
           set
           {
               ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["PREV_OUTSTANDING"]).CurrentValue = Convert.ToDouble(value);
           }
       }
       public EbixDouble PAYMENT_AMOUNT
       {
           get
           {
               return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["PAYMENT_AMOUNT"]) == null ? new EbixDouble("PAYMENT_AMOUNT") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["PAYMENT_AMOUNT"]);
           }
           set
           {
               ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["PAYMENT_AMOUNT"]).CurrentValue = Convert.ToDouble(value);
           }
       }

       public EbixDouble RECOVERY_AMOUNT
       {
           get
           {
               return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RECOVERY_AMOUNT"]) == null ? new EbixDouble("RECOVERY_AMOUNT") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RECOVERY_AMOUNT"]);
           }
           set
           {
               ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["RECOVERY_AMOUNT"]).CurrentValue = Convert.ToDouble(value);
           }
       }
       public EbixInt32 ACTIVITY_TYPE
       {
           get
           {
               return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ACTIVITY_TYPE"]) == null ? new EbixInt32("ACTIVITY_TYPE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ACTIVITY_TYPE"]);
           }
           set
           {

               ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ACTIVITY_TYPE"]).CurrentValue = Convert.ToInt32(value);
           }
       }

       public EbixInt32 ACTION_ON_PAYMENT
       {
           get
           {
               return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ACTION_ON_PAYMENT"]) == null ? new EbixInt32("ACTION_ON_PAYMENT") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ACTION_ON_PAYMENT"]);
           }
           set
           {

               ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["ACTION_ON_PAYMENT"]).CurrentValue = Convert.ToInt32(value);
           }
       }

       public EbixDouble DEDUCTIBLE_1
       {
           get
           {
               return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["DEDUCTIBLE_1"]) == null ? new EbixDouble("DEDUCTIBLE_1") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["DEDUCTIBLE_1"]);
           }
           set
           {
               ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["DEDUCTIBLE_1"]).CurrentValue = Convert.ToDouble(value);
           }
       }

       public EbixDouble ADJUSTED_AMOUNT
       {
           get
           {
               return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["ADJUSTED_AMOUNT"]) == null ? new EbixDouble("ADJUSTED_AMOUNT") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["ADJUSTED_AMOUNT"]);
           }
           set
           {
               ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["ADJUSTED_AMOUNT"]).CurrentValue = Convert.ToDouble(value);
           }
       }

       public EbixString PERSONAL_INJURY
       {
           get
           {
               return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERSONAL_INJURY"]) == null ? new EbixString("PERSONAL_INJURY") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERSONAL_INJURY"]);
           }
           set
           {
               ((Cms.EbixDataTypes.EbixString)htPropertyCollection["PERSONAL_INJURY"]).CurrentValue = Convert.ToString(value);
           }
       }

       public EbixInt32 VICTIM_ID
       {
           get
           {
               return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["VICTIM_ID"]) == null ? new EbixInt32("VICTIM_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["VICTIM_ID"]);
           }
           set
           {

               ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["VICTIM_ID"]).CurrentValue = Convert.ToInt32(value);
           }
       }
       #endregion
       public DataSet GetClaimCoveragesReserveDetails(Int32 ClaimID, Int32 ActivityID, Int32 LobID, Int32 LangID, string FetchMode)
       {
           DataSet dsCount = null;

           try
           {
               base.Proc_FetchData = "Proc_GetClaimCoveragesReserveDetails";
               base.htGetDataParamCollections.Clear();
               base.htGetDataParamCollections.Add("@CLAIM_ID", ClaimID);
               base.htGetDataParamCollections.Add("@ACTIVITY_ID", ActivityID);
               base.htGetDataParamCollections.Add("@LOB_ID", LobID);
               base.htGetDataParamCollections.Add("@LANG_ID", LangID);
               base.htGetDataParamCollections.Add("@FETCH_MODE", FetchMode);
              

           
               dsCount = base.GetData();
            
           }//try
           catch (Exception ex)
           {
               throw (ex);
           }//catch (Exception ex)
           return dsCount;
       }//public DataSet FetchData(int Peril_ID)


       public DataSet GetClaimPaymentAmountWithPersonalInjury(Int32 ClaimID, Int32 ActivityID)
       {
           DataSet dsCount = null;

           try
           {
               base.Proc_FetchData = "Proc_GetClaimPaymentAmountWithPersonalInjury";
               base.htGetDataParamCollections.Clear();
               base.htGetDataParamCollections.Add("@CLAIM_ID", ClaimID);
               base.htGetDataParamCollections.Add("@ACTIVITY_ID", ActivityID);          
                         
               dsCount = base.GetData();
            
           }//try
           catch (Exception ex)
           {
               throw (ex);
           }//catch (Exception ex)
           return dsCount;
       }//public DataSet FetchData(int Peril_ID)

        // ADDED BY SANTOSH KR GAUATM ON 08 AUG 20111 FOR ITRACK 1043 AND TFS NO 38
       public DataSet GetTotalClaimPaymentofCoverage(Int32 ClaimID, Int32 CoverageID)
       {
           DataSet dsCount = null;

           try
           {
               base.Proc_FetchData = "Proc_GetTotalClaimPaymentofCoverage";
               base.htGetDataParamCollections.Clear();
               base.htGetDataParamCollections.Add("@CLAIM_ID", ClaimID);
               base.htGetDataParamCollections.Add("@COVERAGE_ID", CoverageID);

               dsCount = base.GetData();

           }//try
           catch (Exception ex)
           {
               throw (ex);
           }//catch (Exception ex)
           return dsCount;
       }//public DataSet FetchData(int Peril_ID)

       public string GetActivityID(Int32 ClaimID)        
          {
            DataSet dsCount = null;
            string activityID=string.Empty;
           try
           {
               base.Proc_FetchData = "Proc_GetClaimActivityID";
               base.htGetDataParamCollections.Clear();
               base.htGetDataParamCollections.Add("@CLAIM_ID", ClaimID);
               dsCount = base.GetData();
               if (dsCount.Tables.Count > 0 && dsCount.Tables[0].Rows.Count > 0)
                   activityID=dsCount.Tables[0].Rows[0][0].ToString();
              

           }//try
           catch (Exception ex)
           {
               throw (ex);
           }//catch (Exception ex)
           return activityID;
          }


       public int AddReserveDetails(System.Collections.ArrayList aList, int ActivityID)
        {
           
            int returnResult = 0;
            base.Proc_Add_Name = "Proc_InsertClaimCoveragesReserveDetails";
            base.Proc_Update_Name = "Proc_UpdateClaimCoveragesReserveDetails";
        
          
            StringBuilder sbTranXml = new StringBuilder();
            sbTranXml.Append("<root>");

            String ConnStr = EbixDataLayer.DataWrapper.ConnString = DBConnString; 
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                for (int i = 0; i < aList.Count; i++)
                {

                    ClsAddReserveDetailsInfo objAddReserveDetailsInfo ;
                    

                    objAddReserveDetailsInfo = aList[i] as ClsAddReserveDetailsInfo;
                    objDataWrapper.ClearParameteres();
                    string strTranXML = "";
                    objAddReserveDetailsInfo.TransactLabel = this.TransactLabel;
                    objAddReserveDetailsInfo.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                    objAddReserveDetailsInfo.POLICYID = POLICY_ID.CurrentValue;
                    objAddReserveDetailsInfo.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                    objAddReserveDetailsInfo.RequiredTransactionLog = false;
                    objAddReserveDetailsInfo.ACTIVITY_ID.CurrentValue = ActivityID; 
                    this.TRANS_TYPE_ID = 286;

                   if (objAddReserveDetailsInfo.ACTION == "I")
                   {
                       objAddReserveDetailsInfo.Proc_Add_Name = "Proc_InsertClaimCoveragesReserveDetails";
                       objAddReserveDetailsInfo.ReturnIDName = "@RESERVE_ID";


                        objAddReserveDetailsInfo.RECORDED_BY = objAddReserveDetailsInfo.CREATED_BY.CurrentValue;

                        objAddReserveDetailsInfo.MODIFIED_BY.IsDBParam = false;
                       objAddReserveDetailsInfo.CUSTOMER_ID.IsDBParam = false;
                       objAddReserveDetailsInfo.POLICY_ID.IsDBParam = false;
                       objAddReserveDetailsInfo.POLICY_VERSION_ID.IsDBParam = false;

                       objAddReserveDetailsInfo.IS_ACTIVE.IsDBParam = false;
                       objAddReserveDetailsInfo.RESERVE_ID.IsDBParam = false;
                       objAddReserveDetailsInfo.PAYMENT_AMOUNT.IsDBParam = false;

                       objAddReserveDetailsInfo.RECOVERY_AMOUNT.IsDBParam = false;
                       objAddReserveDetailsInfo.PREV_OUTSTANDING.IsDBParam = false;
                       objAddReserveDetailsInfo.ACTIVITY_TYPE.IsDBParam = false;
                       objAddReserveDetailsInfo.ACTION_ON_PAYMENT.IsDBParam = false;


                        
                        objAddReserveDetailsInfo.LAST_UPDATED_DATETIME.IsDBParam = false;
                       // objAddReserveDetailsInfo.REINSURANCE_ID.IsDBParam = false;

                        returnResult = objAddReserveDetailsInfo.Save(objDataWrapper);
                        objAddReserveDetailsInfo.RESERVE_ID.CurrentValue = objAddReserveDetailsInfo.ReturnIDNameValue;
                        strTranXML = objAddReserveDetailsInfo.GenerateTransactionLogXML_New(true);
                        sbTranXml.Append(strTranXML);
                    }
                   else if (objAddReserveDetailsInfo.ACTION == "U")
                    {
                        objAddReserveDetailsInfo.Proc_Update_Name = "Proc_UpdateClaimCoveragesReserveDetails";
                        this.TRANS_TYPE_ID = 287;
                        objAddReserveDetailsInfo.RECORDED_BY = objAddReserveDetailsInfo.MODIFIED_BY.CurrentValue;

                        objAddReserveDetailsInfo.CUSTOMER_ID.IsDBParam = false;
                        objAddReserveDetailsInfo.POLICY_ID.IsDBParam = false;
                        objAddReserveDetailsInfo.POLICY_VERSION_ID.IsDBParam = false;                        

                        objAddReserveDetailsInfo.PREV_OUTSTANDING.IsDBParam = false;
                        objAddReserveDetailsInfo.VICTIM_ID.IsDBParam = false;

                        objAddReserveDetailsInfo.IS_ACTIVE.IsDBParam = false;
                        objAddReserveDetailsInfo.CREATED_BY.IsDBParam = false;
                        objAddReserveDetailsInfo.CREATED_DATETIME.IsDBParam = false;
                        returnResult = objAddReserveDetailsInfo.Update(objDataWrapper);
                        strTranXML = objAddReserveDetailsInfo.GenerateTransactionLogXML_New(false);
                        if (strTranXML != "<LabelFieldMapping></LabelFieldMapping>")
                            sbTranXml.Append(strTranXML);
                    }                 
                    else
                    {
                        objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                        throw (new Exception("Error: DB Action not set for any Model object."));
                    }



                }
                sbTranXml.Append("</root>");
                if (sbTranXml.ToString() != "<root></root>")// || strCustomInfo!="")
                {
                    
                    int Tranreturnval = this.SaveTransaction(objDataWrapper, sbTranXml.ToString());
                }
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (new Exception("Error while saving record.", ex.InnerException));
            }
            finally { objDataWrapper.Dispose(); }
            return returnResult;
        }

       public DataSet GetClaimReserveDetails(Int32 ClaimID, Int32 ActivityID, Int32 ReserveID, Int32 LangID)
       {
         
           try
           {
               base.Proc_FetchData = "Proc_GetClaimReserveDetails";
               base.htGetDataParamCollections.Clear();
               base.htGetDataParamCollections.Add("@CLAIM_ID", ClaimID);
               base.htGetDataParamCollections.Add("@ACTIVITY_ID", ActivityID);
               base.htGetDataParamCollections.Add("@RESERVE_ID", ReserveID);
               base.htGetDataParamCollections.Add("@LANG_ID", LangID);
               return base.GetData();
              


           }//try
           catch (Exception ex)
           {
               throw (ex);
           }//catch (Exception ex)
          
       }

       public int SaveTransaction(DataWrapper objDataWrapper, string oldxml)
       {
           int returnval = 0;
           Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

           objTransactionInfo.TRANS_TYPE_ID = this.TRANS_TYPE_ID;
           objTransactionInfo.CHANGE_XML = oldxml;
           objTransactionInfo.CLIENT_ID = CUSTOMER_ID.CurrentValue;
           objTransactionInfo.POLICY_ID = POLICY_ID.CurrentValue;
           objTransactionInfo.POLICY_VER_TRACKING_ID = POLICY_VERSION_ID.CurrentValue;
           objTransactionInfo.RECORDED_BY = CREATED_BY.CurrentValue;
           objTransactionInfo.TRANS_DESC = "";

           returnval = base.MaintainTrans(objDataWrapper, objTransactionInfo);
           return returnval;

       }  //Save Complete Save/Update transaction


       public int CompleteClaimActivity(Int32 ClaimID, Int32 ActivityID, Int32 CompletedBy)
       {
           int returnValue = 0;
           try
           {

             
               base.Proc_FetchData = "Proc_CompleteClaimActivities";

               base.htGetDataParamCollections.Clear();

               

               base.htGetDataParamCollections.Add("@CLAIM_ID", ClaimID);
               base.htGetDataParamCollections.Add("@ACTIVITY_ID", ActivityID);
               base.htGetDataParamCollections.Add("@ACTIVITY_REASON", 0);
               base.htGetDataParamCollections.Add("@ACTION_ON_PAYMENT", 0);


               base.htGetDataParamCollections.Add("@COMPETED_BY", CompletedBy);
              
               DataSet dsCount = base.GetData();
               int ss = base.Proc_ReturnValue;
               if (dsCount.Tables.Count > 0 && dsCount.Tables[0].Rows.Count > 0)
                   returnValue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());

                           

           }//try
           catch (Exception ex)
           {
               throw (ex);
           }//catch (Exception ex)
           finally { }
           return returnValue;
       }//public int AddNamedParilsData()
        
       public int CalculateBreakdown(Int32 ClaimID, Int32 ActivityID)
       {
           int returnValue = 0;
           try
           {



               base.Proc_FetchData = "Proc_CalculateBreakdown";
               base.htGetDataParamCollections.Clear();
               base.htGetDataParamCollections.Add("@CLAIM_ID", ClaimID);
               base.htGetDataParamCollections.Add("@ACTIVITY_ID", ActivityID);
             
               
               DataSet dsCount = base.GetData();
               if (dsCount.Tables.Count > 0 && dsCount.Tables[0].Rows.Count > 0)
                   returnValue = 1;



           }//try
           catch (Exception ex)
           {
               throw (ex);
           }//catch (Exception ex)
           finally { }
           return returnValue;
       }
    }
}
