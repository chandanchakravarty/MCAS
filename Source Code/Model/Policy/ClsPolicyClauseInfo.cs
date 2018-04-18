/******************************************************************************************
<Author				: - Charles Gomes
<Start Date			: -	15/Apr/2010 
<End Date			: -	
<Description		: - Model Class of POL_CLAUSES
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Cms.EbixDataTypes;
using Cms.Model.Support;
using System.Xml;
using Cms.EbixDataLayer;

namespace Cms.Model.Policy
{
    [Serializable]
    public class ClsPolicyClauseInfo : ClsModelBaseClass
    {
        public ClsPolicyClauseInfo()
        {
            this.PropertyCollection();            
        }

        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
            base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);
            base.htPropertyCollection.Add("CLAUSE_ID", CLAUSE_ID);
            base.htPropertyCollection.Add("CLAUSE_TITLE", CLAUSE_TITLE);
            base.htPropertyCollection.Add("CLAUSE_DESCRIPTION", CLAUSE_DESCRIPTION);
            base.htPropertyCollection.Add("POL_CLAUSE_ID", POL_CLAUSE_ID); 
           
            //Added By Praveen Kumar 29/04/2010
            base.htPropertyCollection.Add("SUSEP_LOB_ID", SUSEP_LOB_ID);
            base.htPropertyCollection.Add("CLAUSE_TYPE", CLAUSE_TYPE);
            base.htPropertyCollection.Add("ATTACH_FILE_NAME", ATTACH_FILE_NAME);
            base.htPropertyCollection.Add("CLAUSE_CODE", CLAUSE_CODE);
            base.htPropertyCollection.Add("PREVIOUS_VERSION_ID", PREVIOUS_VERSION_ID);
    
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
        public EbixInt32 PREVIOUS_VERSION_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PREVIOUS_VERSION_ID"]) == null ? new EbixInt32("PREVIOUS_VERSION_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PREVIOUS_VERSION_ID"]);
            }
            set
            {

                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["PREVIOUS_VERSION_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }
        public EbixInt32 CLAUSE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAUSE_ID"]) == null ? new EbixInt32("CLAUSE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAUSE_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAUSE_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 POL_CLAUSE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POL_CLAUSE_ID"]) == null ? new EbixInt32("POL_CLAUSE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POL_CLAUSE_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POL_CLAUSE_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixString CLAUSE_TITLE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLAUSE_TITLE"]) == null ? new EbixString("CLAUSE_TITLE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLAUSE_TITLE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLAUSE_TITLE"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString CLAUSE_DESCRIPTION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLAUSE_DESCRIPTION"]) == null ? new EbixString("CLAUSE_DESCRIPTION") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLAUSE_DESCRIPTION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLAUSE_DESCRIPTION"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixInt32 SUSEP_LOB_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUSEP_LOB_ID"]) == null ? new EbixInt32("SUSEP_LOB_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUSEP_LOB_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["SUSEP_LOB_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 CLAUSE_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAUSE_TYPE"]) == null ? new EbixInt32("CLAUSE_TYPE") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAUSE_TYPE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["CLAUSE_TYPE"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixString ATTACH_FILE_NAME
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ATTACH_FILE_NAME"]) == null ? new EbixString("ATTACH_FILE_NAME") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ATTACH_FILE_NAME"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["ATTACH_FILE_NAME"]).CurrentValue = Convert.ToString(value);
            }
        }
        public EbixString CLAUSE_CODE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLAUSE_CODE"]) == null ? new EbixString("CLAUSE_CODE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLAUSE_CODE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CLAUSE_CODE"]).CurrentValue = Convert.ToString(value);
            }
        }

        /// <summary>
        /// Fetch Clauses
        /// </summary>
        /// <param name="lobID">LOB_ID</param>
        /// <param name="subLobID">SUB_LOB_ID</param>
        /// <param name="customerID">CUSTOMER_ID</param>
        /// <param name="polID">POLICY_ID</param>
        /// <param name="polVersionID">POLICY_VERSION_ID</param>
        /// <returns></returns>
        /// Added by Charles on 13-Apr-10 for Clause Page
        public DataSet FetchClauses(int lobID, int subLobID, int customerID, int polID, int polVersionID)
        {           
            DataSet dstemp = null;

            try
            {
                base.Proc_FetchData = "Proc_FetchClauses";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@LOB_ID", lobID);
                base.htGetDataParamCollections.Add("@SUBLOB_ID", subLobID);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", customerID);
                base.htGetDataParamCollections.Add("@POLICY_ID", polID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", polVersionID);
                
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

        public DataTable FetchUserDefinedClauses(int polClauseID, int CustomerID, int PolicyID, int VersionID)
        {
            DataSet dstemp = null;

            try
            {
                base.Proc_FetchData = "Proc_FetchUserDefinedPolClauseInfo";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@POL_CLAUSE_ID", polClauseID);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CustomerID);
                base.htGetDataParamCollections.Add("@POLICY_ID", PolicyID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", VersionID);
                dstemp = base.GetData();

                if (dstemp != null && dstemp.Tables.Count > 0)
                {
                    return dstemp.Tables[0];
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
        public DataTable FetchDataForSystemDefined(int ClauseID, int CustomerID, int PolicyID, int VersionID, string IsChecked)
        {
            DataSet dstemp = null;

            try
            {
                base.Proc_FetchData = "Proc_GetClausesDetailsData";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CLAUSE_ID", ClauseID);
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CustomerID);
                base.htGetDataParamCollections.Add("@POLICY_ID", PolicyID);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", VersionID);
                base.htGetDataParamCollections.Add("@IS_CHECKED", IsChecked);

                dstemp = base.GetData();

                if (dstemp != null && dstemp.Tables.Count > 0)
                {
                    return dstemp.Tables[0];
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
        public int AddPolClauses()
        {
            int returnValue = 0;
            try
            {

                base.Proc_Add_Name = "Proc_SavePolClauses";

                base.TRANS_TYPE_ID = 241;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                
                base.ReturnIDName = "@POL_CLAUSE_ID";
                
                POL_CLAUSE_ID.IsDBParam = false;
                IS_ACTIVE.IsDBParam = false;
                CREATED_DATETIME.IsDBParam=false;
                LAST_UPDATED_DATETIME.IsDBParam=false;
                MODIFIED_BY.IsDBParam=false;
                LAST_UPDATED_DATETIME.IsDBParam=false;
                
               
                //-------------- Added By Praveen Kumar 30/04/2010  starts --------------
                //Addding New Column SUSEP_LOB_ID value, But Data Will Submitted onl on click of Save Button not 
                //on Add user defind button sot its value false
                //SUSEP_LOB_ID.IsDBParam = false;
                //-------------- Added By Praveen Kumar 30/04/2010  Ends --------------
    
                returnValue = base.Save();

                returnValue = POL_CLAUSE_ID.CurrentValue = ReturnIDNameValue;
                ReturnIDName = String.Empty;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return returnValue;
        }
        public int DeactivatePolClauses()
        {
            int returnValue = 0;

            try
            {
                base.Proc_Update_Name = "Proc_DeactivePolicyClauses";

                CUSTOMER_ID.IsDBParam = true;
                POLICY_ID.IsDBParam = true;
                POLICY_VERSION_ID.IsDBParam = true;
                CLAUSE_ID.IsDBParam = true;
                // changes by praveer for itrack no 1410
             //   POL_CLAUSE_ID.IsDBParam = false;
                MODIFIED_BY.IsDBParam = true;

                IS_ACTIVE.IsDBParam = false;
                CREATED_BY.IsDBParam = false;
                CREATED_DATETIME.IsDBParam = false;
                LAST_UPDATED_DATETIME.IsDBParam = false;
                CLAUSE_TYPE.IsDBParam = false;
                CLAUSE_CODE.IsDBParam = false;
                ATTACH_FILE_NAME.IsDBParam = false;
                CLAUSE_DESCRIPTION.IsDBParam = false;
                CLAUSE_TITLE.IsDBParam = false;
                SUSEP_LOB_ID.IsDBParam = false;
                ATTACH_FILE_NAME.IsDBParam = false;
                PREVIOUS_VERSION_ID.IsDBParam = false;
                base.ReturnIDName = "";              
                base.TRANS_TYPE_ID = 384;               
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                //RequiredTransactionLog = false;
                returnValue = base.Update();

            }
            catch
            {
                returnValue = 0;
            }

            return returnValue;
        }
        public int UpdateDefaultClauses(int lobID)
        {
            int returnValue = 0;
          
            try
            {
                RequiredTransactionLog = false;
                base.Proc_Add_Name = "Proc_UpdateDefaultClauses";

                base.TRANS_TYPE_ID = 241;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
              
                base.ReturnIDName = "@POL_CLAUSE_ID";
                SUSEP_LOB_ID.IsDBParam = true;
                base.htGetDataParamCollections.Add("@SUSEP_LOB_ID", lobID);
                POL_CLAUSE_ID.IsDBParam = false;
                IS_ACTIVE.IsDBParam = false;
                CREATED_DATETIME.IsDBParam = false;
                LAST_UPDATED_DATETIME.IsDBParam = false;
                MODIFIED_BY.IsDBParam = false;
                LAST_UPDATED_DATETIME.IsDBParam = false;
                CLAUSE_ID.IsDBParam = false;
                CLAUSE_TITLE.IsDBParam = false;
                CLAUSE_DESCRIPTION.IsDBParam = false;
                CLAUSE_TYPE.IsDBParam = false;
                ATTACH_FILE_NAME.IsDBParam = false;
                CLAUSE_CODE.IsDBParam = false;
                PREVIOUS_VERSION_ID.IsDBParam = false;
                
                //-------------- Added By Praveen Kumar 30/04/2010  starts --------------
                //Addding New Column SUSEP_LOB_ID value, But Data Will Submitted onl on click of Save Button not 
                //on Add user defind button sot its value false
                //SUSEP_LOB_ID.IsDBParam = false;
                //-------------- Added By Praveen Kumar 30/04/2010  Ends --------------

                returnValue = base.Save();

                //returnValue = POL_CLAUSE_ID.CurrentValue = ReturnIDNameValue;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return returnValue;
        }
        public int DeletePolClauses()
        {
            int returnValue = 0;
            try
            {

                base.Proc_Delete_Name = "Proc_DeletePolClauses";
                base.htGetDataParamCollections.Clear();
                
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@CLAUSE_ID", CLAUSE_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POL_CLAUSE_ID", POL_CLAUSE_ID.CurrentValue);
                
                base.TRANS_TYPE_ID = 104;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                
                base.ProcReturnValue = true;

                returnValue = base.Delete();
                if (returnValue > 0)
                {
                  return returnValue;
                }
                else
                {
                    return base.Proc_ReturnValue;
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }           

        }

        public int UpdatePolClauses()
        {
            int returnValue = 0;

            try
            {
                base.Proc_Update_Name = "Proc_UpdatePolClauses";

                CUSTOMER_ID.IsDBParam = true;
                POLICY_ID.IsDBParam = true;
                POLICY_VERSION_ID.IsDBParam = true;
                CLAUSE_ID.IsDBParam = false;

                POL_CLAUSE_ID.IsDBParam = true;
                MODIFIED_BY.IsDBParam = true;

                IS_ACTIVE.IsDBParam = false;
                CREATED_BY.IsDBParam = false;
                CREATED_DATETIME.IsDBParam = false;
                LAST_UPDATED_DATETIME.IsDBParam = false;
                CLAUSE_TYPE.IsDBParam = true;
                CLAUSE_CODE.IsDBParam = true;
                ATTACH_FILE_NAME.IsDBParam = true;            
                base.ReturnIDName = "";
                base.TRANS_TYPE_ID = 118;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                base.ProcReturnValue = true;
                //RequiredTransactionLog = false;
                returnValue = base.Update();
                
            }
            catch 
            {
                returnValue = 0;
            }

            return Proc_ReturnValue;
        }

        public int UpdatePolicyClauses()
        {
            int returnValue = 0;

            try
            {
                base.Proc_Update_Name = "Proc_UpdatePolicyClauses";

                CUSTOMER_ID.IsDBParam = true;
                POLICY_ID.IsDBParam = true;
                POLICY_VERSION_ID.IsDBParam = true;
                CLAUSE_ID.IsDBParam = false;
                CLAUSE_CODE.IsDBParam = false;
                POL_CLAUSE_ID.IsDBParam = true;
                MODIFIED_BY.IsDBParam = true;
                ATTACH_FILE_NAME.IsDBParam = false;
                CLAUSE_TYPE.IsDBParam = false;
                CLAUSE_DESCRIPTION.IsDBParam = false;
                IS_ACTIVE.IsDBParam = false;
                CREATED_BY.IsDBParam = false;
                CREATED_DATETIME.IsDBParam = false;
                LAST_UPDATED_DATETIME.IsDBParam = false;
                PREVIOUS_VERSION_ID.IsDBParam = false;
                // changes by praveer for itrack no 1410
                CLAUSE_TITLE.IsDBParam = false;
                base.ReturnIDName = "";
                base.TRANS_TYPE_ID = 118;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                //RequiredTransactionLog = false;
                returnValue = base.Update();

            }
            catch
            {
                returnValue = 0;
            }

            return returnValue;
        }

        public int UpdateAttachment(string pol,string Attach)
        {
            int returnValue = 0;

            try
            {
                base.Proc_Update_Name = "Proc_UpdatePolClausesAttachment";

                CUSTOMER_ID.IsDBParam = true;
                POLICY_ID.IsDBParam = true;
                POLICY_VERSION_ID.IsDBParam = true;
                PREVIOUS_VERSION_ID.IsDBParam = false;
                CLAUSE_ID.IsDBParam = false;

                
                POL_CLAUSE_ID.CurrentValue = int.Parse(pol);
                ATTACH_FILE_NAME.CurrentValue = Attach;
                MODIFIED_BY.IsDBParam = true;
                ATTACH_FILE_NAME.IsDBParam = true;
                IS_ACTIVE.IsDBParam = false;
                CREATED_BY.IsDBParam = false;
                CREATED_DATETIME.IsDBParam = false;
                LAST_UPDATED_DATETIME.IsDBParam = false;
                CLAUSE_DESCRIPTION.IsDBParam = false;
                CLAUSE_TITLE.IsDBParam = false;
                CLAUSE_TYPE.IsDBParam = false;
                SUSEP_LOB_ID.IsDBParam = false;
                CLAUSE_CODE.IsDBParam = false;
                base.ReturnIDName = "";
                //base.htGetDataParamCollections.Add("@POL_CLAUSE_ID", pol);
                //base.htGetDataParamCollections.Add("@ATTACH_FILE_NAME", Attach);
                //base.TRANS_TYPE_ID = 118;
                //base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                //base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                //base.POLICYID = POLICY_ID.CurrentValue;
                //base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                RequiredTransactionLog = false;
                returnValue = base.Update();

            }
            catch
            {
                returnValue = 0;
            }

            return returnValue;
        
        }

        public int UpdateSysClauses()
        {
            int returnValue = 0;

            try
            {
                base.Proc_Update_Name = "Proc_UpdateSysClauses";

                POL_CLAUSE_ID.IsDBParam = false;
                //MODIFIED_BY.IsDBParam = false;
                SUSEP_LOB_ID.IsDBParam = false;
                IS_ACTIVE.IsDBParam = false;
                CREATED_BY.IsDBParam = false;
                CREATED_DATETIME.IsDBParam = false;
                LAST_UPDATED_DATETIME.IsDBParam = false;
               

                base.TRANS_TYPE_ID = 118;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = MODIFIED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;

                returnValue = base.Update();
            }
            catch
            {
                returnValue = 0;
            }

            return returnValue;
        }


        public string GetClausesTranXML(Cms.Model.Policy.ClsPolicyClauseInfo objNew, string xml, int Clauses_ID, XmlElement root)
        {
            if (root == null) return "";
            XmlNode node = root.SelectSingleNode("Table[POL_CLAUSE_ID=" + Clauses_ID.ToString() + "]");

            //For making Old Object of model class
            XmlDocument XmlDoc = new XmlDocument();
            //XmlDoc.LoadXml("<NewDataSet><Table>" + node.ToString() + "</NewDataSet></Table>");
            //XmlNodeList oldNodeList = XmlDoc.SelectNodes("NewDataSet/Table");
            //XmlNode oldNode = oldNodeList.Item(0);
            //oldNode = oldNodeList.Item(0);
            string strTranXML = "";

            Cms.Model.Policy.ClsPolicyClauseInfo objOld = new Cms.Model.Policy.ClsPolicyClauseInfo();

            objOld.POLICY_ID.CurrentValue = objNew.POLICY_ID.CurrentValue;
            objOld.POLICY_VERSION_ID.CurrentValue = objNew.POLICY_VERSION_ID.CurrentValue;
            objOld.CUSTOMER_ID.CurrentValue = objNew.CUSTOMER_ID.CurrentValue;
            objOld.CLAUSE_ID.CurrentValue = objNew.CLAUSE_ID.CurrentValue;           
            objOld.TransactLabel = objNew.TransactLabel;
            XmlNode element = null;



            element = node.SelectSingleNode("POL_CLAUSE_ID");

            if (element.InnerXml != "")
            {
                objOld.POL_CLAUSE_ID.CurrentValue = Convert.ToInt32(element.InnerXml);
            }
            if (objOld.POL_CLAUSE_ID != objNew.POL_CLAUSE_ID)
            {

                element = node.SelectSingleNode("POL_CLAUSE_ID");

                if (element != null)
                {
                    objOld.POL_CLAUSE_ID.CurrentValue = int.Parse(element.InnerXml);
                }


                element = node.SelectSingleNode("SUSEP_LOB_ID");

                if (element != null)
                {
                    objOld.SUSEP_LOB_ID.CurrentValue = element.InnerXml==""?0:Convert.ToInt32(element.InnerXml);
                }
                strTranXML = this.GetTransactionLogXML(objOld, objNew);

            }
            return strTranXML;
        }
        private string GetTransactionLogXML(Cms.Model.Policy.ClsPolicyClauseInfo objOld, Cms.Model.Policy.ClsPolicyClauseInfo objNew)
        {
            string TransXML = string.Empty;

            objOld.POL_CLAUSE_ID.CurrentValue = objNew.POL_CLAUSE_ID.CurrentValue;
            objOld.SUSEP_LOB_ID.CurrentValue = objNew.SUSEP_LOB_ID.CurrentValue;
          
            //if (objOld.SUSEP_LOB_ID.IsChanged)
            //{
            //    objOld.SUSEP_LOB_ID.CurrentValue = objNew.SUSEP_LOB_ID.CurrentValue;
            //}
            //else
            //{
            //    objOld.SUSEP_LOB_ID.CurrentValue = objNew.SUSEP_LOB_ID.CurrentValue;
            //}
            TransXML = objOld.GenerateTransactionLogXML_New(false);
            return TransXML;
        }
        public int SaveTransaction(string oldxml, int CustomerId, int PolicyId, int Policy_version_Id, int Recordedby, string Trandesc)
        {
            int returnval = 0;
            String ConnStr = EbixDataLayer.DataWrapper.ConnString = DBConnString;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

            objTransactionInfo.TRANS_TYPE_ID = 103;
            objTransactionInfo.CHANGE_XML = oldxml;
            objTransactionInfo.CLIENT_ID = CustomerId;
            objTransactionInfo.POLICY_ID = PolicyId;
            objTransactionInfo.POLICY_VER_TRACKING_ID = Policy_version_Id;
            objTransactionInfo.RECORDED_BY = Recordedby;
            objTransactionInfo.TRANS_DESC = Trandesc;


            returnval = base.MaintainTrans(objDataWrapper, objTransactionInfo);
            objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            return returnval;

        }  //Save Complete Save/Update transaction

 }
}