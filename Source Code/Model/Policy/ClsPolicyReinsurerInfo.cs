using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.Model.Support;
using Cms.EbixDataTypes;
using System.Data;
using System.Collections;
using Cms.EbixDataLayer;

using System.Xml;
namespace Cms.Model.Policy
{
    [Serializable]
    public class ClsPolicyReinsurerInfo : ClsModelBaseClass
    {
        public ClsPolicyReinsurerInfo()
        {
            this.PropertyCollection();
            this.REINSURER_NAME.IsDBParam = false;
        }

        #region Delare the add parameter collection for the data wrapper class
        /// <summary>
        /// Use to add the parameter collection for the data wrapper class
        /// </summary>
        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("REINSURANCE_ID", REINSURANCE_ID);
            base.htPropertyCollection.Add("COMPANY_ID", COMPANY_ID);
            base.htPropertyCollection.Add("REINSURER_NAME", REINSURER_NAME);
            base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
            base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);
            base.htPropertyCollection.Add("CONTRACT_FACULTATIVE", CONTRACT_FACULTATIVE);
            base.htPropertyCollection.Add("CONTRACT", CONTRACT);
            base.htPropertyCollection.Add("REINSURANCE_CEDED", REINSURANCE_CEDED);
            base.htPropertyCollection.Add("REINSURANCE_COMMISSION", REINSURANCE_COMMISSION);
            base.htPropertyCollection.Add("REINSURER_NUMBER", REINSURER_NUMBER);//ashish
            base.htPropertyCollection.Add("MAX_NO_INSTALLMENT", MAX_NO_INSTALLMENT); // Added by Aditya for TFS BUG # 2514
            base.htPropertyCollection.Add("RISK_ID", RISK_ID); // Added by Aditya for TFS BUG # 2514
            base.htPropertyCollection.Add("REIN_INSTALLMENT_NO", REIN_INSTALLMENT_NO); //Added by Aditya for TFS BUG # 2705
            base.htPropertyCollection.Add("IDEN_ROW_ID", IDEN_ROW_ID);  //Added by Aditya for TFS BUG # 2705
            base.htPropertyCollection.Add("COMM_AMOUNT", COMM_AMOUNT); //Added by Aditya for tfs bug # 177
            base.htPropertyCollection.Add("LAYER_AMOUNT", LAYER_AMOUNT); //Added by Aditya for tfs bug # 177
            base.htPropertyCollection.Add("REIN_PREMIUM", REIN_PREMIUM); //Added by Aditya for tfs bug # 177
        }
        #endregion

        #region Declare the Property for every data table columns
        /// <summary>
        /// Declare the Property for every data table columns 
        /// </summary>
        public EbixInt32 REINSURANCE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["REINSURANCE_ID"]) == null ? new EbixInt32("REINSURANCE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["REINSURANCE_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["REINSURANCE_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 COMPANY_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COMPANY_ID"]) == null ? new EbixInt32("COMPANY_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COMPANY_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["COMPANY_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

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
        }

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
        }

        public EbixInt32 POLICY_VERSION_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]) == null ? new EbixInt32("POLICY_VERSION_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 CONTRACT_FACULTATIVE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CONTRACT_FACULTATIVE"]) == null ? new EbixInt32("CONTRACT_FACULTATIVE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CONTRACT_FACULTATIVE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CONTRACT_FACULTATIVE"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 CONTRACT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CONTRACT"]) == null ? new EbixInt32("CONTRACT") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CONTRACT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CONTRACT"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixDouble REINSURANCE_CEDED
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["REINSURANCE_CEDED"]) == null ? new EbixDouble("REINSURANCE_CEDED") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["REINSURANCE_CEDED"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["REINSURANCE_CEDED"]).CurrentValue = Convert.ToDouble(value);
            }
        }

        public EbixDouble REINSURANCE_COMMISSION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["REINSURANCE_COMMISSION"]) == null ? new EbixDouble("REINSURANCE_COMMISSION") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["REINSURANCE_COMMISSION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["REINSURANCE_COMMISSION"]).CurrentValue = Convert.ToDouble(value);
            }
        }

        public EbixDouble COMM_AMOUNT  //Added by Aditya for tfs bug # 177
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["COMM_AMOUNT"]) == null ? new EbixDouble("COMM_AMOUNT") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["COMM_AMOUNT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["COMM_AMOUNT"]).CurrentValue = Convert.ToDouble(value);
            }
        }

        public EbixDouble LAYER_AMOUNT
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["LAYER_AMOUNT"]) == null ? new EbixDouble("LAYER_AMOUNT") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["LAYER_AMOUNT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["LAYER_AMOUNT"]).CurrentValue = Convert.ToDouble(value);
            }
        }

        public EbixDouble REIN_PREMIUM
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["REIN_PREMIUM"]) == null ? new EbixDouble("REIN_PREMIUM") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["REIN_PREMIUM"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["REIN_PREMIUM"]).CurrentValue = Convert.ToDouble(value);
            }
        }  //Added till here

        public EbixString REINSURER_NUMBER
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REINSURER_NUMBER"]) == null ? new EbixString("REINSURER_NUMBER") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REINSURER_NUMBER"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REINSURER_NUMBER"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString REINSURER_NAME
        {

            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REINSURER_NAME"]) == null ? new EbixString("REINSURER_NAME") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REINSURER_NAME"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REINSURER_NAME"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixInt32 MAX_NO_INSTALLMENT  // Added by Aditya for TFS BUG # 2514
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MAX_NO_INSTALLMENT"]) == null ? new EbixInt32("MAX_NO_INSTALLMENT") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MAX_NO_INSTALLMENT"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["MAX_NO_INSTALLMENT"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 RISK_ID  // Added by Aditya for TFS BUG # 2514
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RISK_ID"]) == null ? new EbixInt32("RISK_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RISK_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RISK_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixString REIN_INSTALLMENT_NO  //Added by Aditya for TFS BUG # 2705
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REIN_INSTALLMENT_NO"]) == null ? new EbixString("REIN_INSTALLMENT_NO") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REIN_INSTALLMENT_NO"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["REIN_INSTALLMENT_NO"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixInt32 IDEN_ROW_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["IDEN_ROW_ID"]) == null ? new EbixInt32("IDEN_ROW_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["IDEN_ROW_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["IDEN_ROW_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }        //Added till here


        #endregion

        #region Methods
        public DataSet FetchReinsurers()
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetReinsurers";
                base.htGetDataParamCollections.Clear();
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            return dsCount;
        }


     
        public DataSet FetchReinsurers(int customer_id, int policy_id, int Poversion_id)
        {
            string strStoredProc = "Proc_GetReinsurers";
            String ConnStr = EbixDataLayer.DataWrapper.ConnString = DBConnString;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", customer_id);
                objDataWrapper.AddParameter("@POLICY_ID", policy_id);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", Poversion_id);
                DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }

        }

        public DataSet FetchRiskDetails(int lob_id,int customer_id, int policy_id, int Poversion_id)  //Added by Aditya for tfs bug # 2514
        {
            string strStoredProc = "PROC_GET_RISK_DETAILS";
            String ConnStr = EbixDataLayer.DataWrapper.ConnString = DBConnString;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@LOB_ID", lob_id);
                objDataWrapper.AddParameter("@CUSTOMER_ID", customer_id);
                objDataWrapper.AddParameter("@POLICY_ID", policy_id);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", Poversion_id);
                DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);               
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return ds;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (new Exception("Error while Fetch record.", ex.InnerException));
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }

        }  //Added till here

        /// <summary>
        /// Delete Reinsurer
        /// </summary>
        /// <returns>Int Status</returns>
        public int DeleteReinsurer()
        {

            int returnValue = 0;
            try
            {
                base.Proc_Delete_Name = "Proc_DeleteReinsurance";

                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@REINSURANCE_ID", REINSURANCE_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);

                //For Transaction Log
                base.TRANS_TYPE_ID = 153;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                //End 


                returnValue = base.Delete();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Ex
            return returnValue;



        }//
       
        public int SaveReinsurer(ArrayList arObjReinsurance)
        {
            int returnResult = 0;
            base.Proc_Add_Name = "Proc_SaveReinsurer";
            base.Proc_Update_Name = "Proc_UpdateReinsurerInfo";
            base.Proc_Delete_Name = "Proc_DeleteReinsurance";
           
            StringBuilder sbTranXml = new StringBuilder();
            sbTranXml.Append("<root>");

            String ConnStr = EbixDataLayer.DataWrapper.ConnString = DBConnString;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                for (int i = 0; i < arObjReinsurance.Count; i++)
                {
                    
                    ClsPolicyReinsurerInfo objModel = (ClsPolicyReinsurerInfo)arObjReinsurance[i];
                    objDataWrapper.ClearParameteres();
                    string strTranXML = "";
                    objModel.TransactLabel = this.TransactLabel;
                    objModel.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                    objModel.POLICYID = POLICY_ID.CurrentValue;
                    objModel.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                    objModel.RequiredTransactionLog = false;
                    if (objModel.ACTION == "I")
                    {
                        objModel.Proc_Add_Name = "Proc_SaveReinsurer";
                        objModel.ReturnIDName = "@REINSURANCE_ID";
                        this.TRANS_TYPE_ID = 152;
                        
                        objModel.RECORDED_BY = objModel.CREATED_BY.CurrentValue;
                        
                        objModel.MODIFIED_BY.IsDBParam = false;
                        objModel.LAST_UPDATED_DATETIME.IsDBParam = false;
                        objModel.REINSURANCE_ID.IsDBParam = false;
                        objModel.REIN_INSTALLMENT_NO.IsDBParam = false;  //Added by Aditya for TFS BUG # 2705
                        objModel.IDEN_ROW_ID.IsDBParam = false;  //Added by Aditya for TFS BUG # 2705
                        
                        returnResult = objModel.Save(objDataWrapper);
                        //objModel.REINSURANCE_ID.CurrentValue = objModel.ReturnIDNameValue;
                        strTranXML = objModel.GenerateTransactionLogXML_New(true);
                        sbTranXml.Append(strTranXML);
                    }
                    else if (objModel.ACTION == "U")
                    {
                        objModel.Proc_Update_Name = "Proc_UpdateReinsurerInfo";
                        this.TRANS_TYPE_ID = 152;
                        objModel.RECORDED_BY = objModel.MODIFIED_BY.CurrentValue;
                      
                        objModel.CREATED_BY.IsDBParam = false;
                        objModel.CREATED_DATETIME.IsDBParam = false;
                        objModel.REIN_INSTALLMENT_NO.IsDBParam = false;   //Added by Aditya for TFS BUG # 2705
                        objModel.IDEN_ROW_ID.IsDBParam = false;   //Added by Aditya for TFS BUG # 2705
                        returnResult = objModel.Update(objDataWrapper);
                        strTranXML = objModel.GenerateTransactionLogXML_New(false);
                        if (strTranXML != "<LabelFieldMapping></LabelFieldMapping>")
                            sbTranXml.Append(strTranXML);
                    }

                    else if (objModel.ACTION == "D")
                    {
                        this.TRANS_TYPE_ID = 153;
                        objModel.Proc_Delete_Name = "Proc_DeleteReinsurance";
                        objModel.htGetDataParamCollections.Clear();
                        objModel.RECORDED_BY = objModel.MODIFIED_BY.CurrentValue;
                       
                        objModel.CREATED_BY.IsDBParam = false;
                        objModel.CREATED_DATETIME.IsDBParam = false;
                        objModel.htGetDataParamCollections.Add("@REINSURANCE_ID", objModel.REINSURANCE_ID.CurrentValue);
                        objModel.htGetDataParamCollections.Add("@POLICY_ID", objModel.POLICYID);
                        objModel.htGetDataParamCollections.Add("@POLICY_VERSION_ID", objModel.POLICYVERTRACKING_ID);
                        objModel.htGetDataParamCollections.Add("@CUSTOMER_ID", objModel.CLIENT_ID);

                        returnResult = objModel.Delete(objDataWrapper);
                        strTranXML = objModel.GenerateTransactionLogXML_New(true);
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



        public int SaveTransaction(DataWrapper objDataWrapper,string oldxml)
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
        public int SaveReinsurer()
        {
            int returnResult = 0;
            try
            {
                base.Proc_Add_Name = "Proc_SaveReinsurer";
                base.ReturnIDName = "@REINSURANCE_ID";
                //For Transaction Log
                base.TRANS_TYPE_ID = 151;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                //end 

                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.REINSURANCE_ID.IsDBParam = false;
                this.REIN_INSTALLMENT_NO.IsDBParam = false;   //Added by Aditya for TFS BUG # 2705
                this.IDEN_ROW_ID.IsDBParam = false;   //Added by Aditya for TFS BUG # 2705

                returnResult = base.Save();

                this.REINSURANCE_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;
        }

        public int UpdateReinsurer()
        {
            int returnResult = 0;
            try
            {

                base.Proc_Update_Name = "Proc_UpdateReinsurerInfo";

                
                //For Transaction Log
                base.TRANS_TYPE_ID = 152;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                //end  

                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.REIN_INSTALLMENT_NO.IsDBParam = false;   //Added by Aditya for TFS BUG # 2705
                this.IDEN_ROW_ID.IsDBParam = false;   //Added by Aditya for TFS BUG # 2705
                returnResult = base.Update();

            }
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Exception ex)
            finally { }
            return returnResult;
        }

        public DataSet FetchACT_POLICY_REIN_INSTALLMENT_DETAILS(int CUSTOMER_ID,int POLICY_ID, int POLICY_VERSION_ID)  //Added by Aditya for tfs bug # 2705
        {
            DataSet dstemp = null;

            try
            {
                base.Proc_FetchData = "Proc_GetACT_POLICY_REIN_INSTALLMENT_DETAILS";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID);

                dstemp = base.GetData();

                if (dstemp != null)
                {
                    return dstemp;
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }
        }

        public int UpdateACT_POLICY_REIN_INSTALLMENT_DETAILS()
        {
            int returnValue = 0;
            try
            {
                base.Proc_Update_Name = "Proc_UpdateACT_POLICY_REIN_INSTALLMENT_DETAILS";

                //For Transaction Log
                base.TRANS_TYPE_ID = 323;

                //end                 
                this.REINSURANCE_ID.IsDBParam = false;
                this.COMPANY_ID.IsDBParam = false;
                this.REINSURER_NAME.IsDBParam = false;
                this.CONTRACT_FACULTATIVE.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.CONTRACT.IsDBParam = false;
                this.REINSURANCE_CEDED.IsDBParam = false;
                this.REINSURANCE_COMMISSION.IsDBParam = false;
                this.REINSURER_NUMBER.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.MAX_NO_INSTALLMENT.IsDBParam = false;
                this.RISK_ID.IsDBParam = false;
                base.IS_ACTIVE.IsDBParam = false;

                returnValue = base.Update();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnValue;
        }    //Added till here

        public DataSet FetchPolicy_DETAILS(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID)  //Added by Aditya for tfs bug # 180
        {
            DataSet dstemp = null;

            try
            {
                base.Proc_FetchData = "PROC_GET_RI_CONTRACT_INFO";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID);

                dstemp = base.GetData();

                if (dstemp != null)
                {
                    return dstemp;
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }
        }  //Added till here


        #endregion
    }
}

    

     

        

        