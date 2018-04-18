/******************************************************************************************
<Author				: - Lalit Kumar Chauhan
<Start Date			: -	13-04-2010
<End Date			: -	
<Description		: - 
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
using Cms.EbixDataTypes;
using Cms.Model.Support;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Xml;
using Cms.EbixDataLayer;

namespace Cms.Model.Policy
{
    [Serializable]
    public class ClsDiscountSurchargeInfo : ClsModelBaseClass
    {
       // private String _ACTION;        
        
        public ClsDiscountSurchargeInfo()
        {          
            this.PropertyCollection();
        }       //Constructor

        private void PropertyCollection()
        {
            base.htPropertyCollection.Add("CUSTOMER_ID", CUSTOMER_ID);
            base.htPropertyCollection.Add("POLICY_ID", POLICY_ID);
            base.htPropertyCollection.Add("POLICY_VERSION_ID", POLICY_VERSION_ID);
            base.htPropertyCollection.Add("DISCOUNT_ID", DISCOUNT_ID);
            base.htPropertyCollection.Add("PERCENTAGE", PERCENTAGE);
            base.htPropertyCollection.Add("DISCOUNT_ROW_ID", DISCOUNT_ROW_ID);
            base.htPropertyCollection.Add("RISK_ID", RISK_ID);
            base.htPropertyCollection.Add("DISCOUNT_DESCRIPTION", DISCOUNT_DESCRIPTION);

        }               //Add Property In base Property Collection Object

        #region Declare Property For Data Columns
        
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
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]) == null ? new EbixInt32("POLICY_VERSION_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["POLICY_VERSION_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 POLICY_VERSION_ID 

        public EbixInt32 DISCOUNT_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DISCOUNT_ID"]) == null ? new EbixInt32("DISCOUNT_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DISCOUNT_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DISCOUNT_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 DISCOUNT_ID 

        public EbixDouble PERCENTAGE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["PERCENTAGE"]) == null ? new EbixDouble("PERCENTAGE") : ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["PERCENTAGE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixDouble)htPropertyCollection["PERCENTAGE"]).CurrentValue = Convert.ToDouble(value);
            }
        }//public EbixInt32 PERCENTAGE 

        public EbixInt32 DISCOUNT_ROW_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DISCOUNT_ROW_ID"]) == null ? new EbixInt32("DISCOUNT_ROW_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DISCOUNT_ROW_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["DISCOUNT_ROW_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }//public EbixInt32 DISCOUNT_ROWID_ID 

        public EbixInt32 RISK_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RISK_ID"]) == null ? new EbixInt32("RISK_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RISK_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["RISK_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        } //public EbixInt32 DISCOUNT_ROWID_ID 

        public EbixString DISCOUNT_DESCRIPTION 
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DISCOUNT_DESCRIPTION"]) == null ? new EbixString("DISCOUNT_DESCRIPTION") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DISCOUNT_DESCRIPTION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DISCOUNT_DESCRIPTION"]).CurrentValue = Convert.ToString(value);
            }
        } //public EbixInt32 DISCOUNT_DESCRIPTION FOR MAINTAIN TRANSACTION

        #endregion

        //public String ACTION 
        //{
        //    get { return _ACTION; }
        //    set { _ACTION = value; }
        //}

        public DataSet GetPolicyDiscountSurcharge(int Customer_Id, int Policy_Id, int Policy_version_id, String CalledFrom, int Risk_id)
        {
            DataSet dsCount = null;
            base.Proc_FetchData = "Proc_GetPolicyDiscountSurcharge";

            //Set Delete Paramenter
            base.htGetDataParamCollections.Clear();
            base.htGetDataParamCollections.Add("@CUSTOMER_ID", Customer_Id);
            base.htGetDataParamCollections.Add("@POLICY_ID", Policy_Id);
            base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", Policy_version_id);
            base.htGetDataParamCollections.Add("@CALLEDFROM", CalledFrom);
            base.htGetDataParamCollections.Add("@RISK_ID", Risk_id);
            
            dsCount = base.GetData();

            return dsCount;

        }        //get Customer Discount Percent List

        public int AddDiscountPercentage() 
        {
            int returnResult = 0;
            try
            {
                base.Proc_Add_Name = "Proc_Insert_Pol_DiscountSurchage";
                base.ReturnIDName = "@DISCOUNT_ROW_ID";

                //For Transaction Log
                base.TRANS_TYPE_ID = 110;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                //End

                //Set insert Proc Parameter
                base.MODIFIED_BY.IsDBParam = false;
                base.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.DISCOUNT_ROW_ID.IsDBParam = false;
                this.DISCOUNT_DESCRIPTION.IsDBParam = false;
                RequiredTransactionLog = false;
                returnResult = base.Save();

                this.DISCOUNT_ROW_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
            }


            catch { }
            finally { }
            return returnResult;
      
        }         //Add New Discount Percent In user Discount Percent List

        public int UpdateDiscountPercentage()
        {
            int returnResult = 0;
            try
            {
                base.Proc_Update_Name = "Proc_Update_Pol_DiscountSurchage";
                //base.ReturnIDName = "@DISCOUNT_ROW_ID";

                //For Transaction Log
                base.TRANS_TYPE_ID = 111;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
                //End

                //Set insert Proc Parameter
                base.CREATED_BY.IsDBParam = false;
                base.CREATED_DATETIME.IsDBParam = false;
                this.CUSTOMER_ID.IsDBParam = true;
                this.POLICY_ID.IsDBParam = true;
                this.POLICY_VERSION_ID.IsDBParam = true;
                this.DISCOUNT_ID.IsDBParam = false;
                this.DISCOUNT_DESCRIPTION.IsDBParam = false;
                RequiredTransactionLog = false;
                returnResult = base.Update();

               // this.DISCOUNT_ROWID_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter
            }


            catch { }
            finally { }
            return returnResult;

        }   //Update Model For Update Discount Percent 

        public int DeleteDiscountSurchargePercent(int DiscountRowId)         
        {
            int returnval = 0;
            try {

                base.Proc_Delete_Name = "Proc_Pol_DeleteDiscountSurcharge";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@CUSTOMER_ID", CUSTOMER_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_ID", POLICY_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@POLICY_VERSION_ID", POLICY_VERSION_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@DISCOUNT_ROW_ID", DiscountRowId);



                //For Transaction Log
               
                base.TRANS_TYPE_ID = 112;
                base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
                base.RECORDED_BY = CREATED_BY.CurrentValue;
                base.POLICYID = POLICY_ID.CurrentValue;
                base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;

                //end 

                returnval = base.Delete();


            }
            catch (Exception ex) { throw ex; }

            return returnval;

        }  //model For Delete Discount Percent From  userId List

        public string GetDiscountSurchargeTranXML(Cms.Model.Policy.ClsDiscountSurchargeInfo objNew, string xml, int DISCOUNT_ROW_ID , XmlElement root)
        {
            if (root == null) return "";
            XmlNode node = root.SelectSingleNode("Table[DISCOUNT_ROW_ID=" + DISCOUNT_ROW_ID.ToString() + "]");
            
            //For making Old Object of model class
            XmlDocument XmlDoc = new XmlDocument();
            //XmlDoc.LoadXml("<NewDataSet><Table>" + node.ToString() + "</NewDataSet></Table>");
            //XmlNodeList oldNodeList = XmlDoc.SelectNodes("NewDataSet/Table");
            //XmlNode oldNode = oldNodeList.Item(0);
            //oldNode = oldNodeList.Item(0);
            string strTranXML = "";

            Cms.Model.Policy.ClsDiscountSurchargeInfo objOld = new Cms.Model.Policy.ClsDiscountSurchargeInfo();

            objOld.POLICY_ID.CurrentValue = objNew.POLICY_ID.CurrentValue;
            objOld.POLICY_VERSION_ID.CurrentValue = objNew.POLICY_VERSION_ID.CurrentValue;
            objOld.CUSTOMER_ID.CurrentValue = objNew.CUSTOMER_ID.CurrentValue;
            objOld.DISCOUNT_ROW_ID.CurrentValue = objNew.DISCOUNT_ROW_ID.CurrentValue;
            objOld.TransactLabel = objNew.TransactLabel;
            XmlNode element = null;



            element = node.SelectSingleNode("DISCOUNT_ROW_ID");

            if (element.InnerXml != "")
            {
                objOld.DISCOUNT_ROW_ID.CurrentValue = Convert.ToInt32(element.InnerXml);
            }
            if (objOld.DISCOUNT_ROW_ID != objNew.DISCOUNT_ROW_ID)
            {

                element = node.SelectSingleNode("DISCOUNT_ID");

                if (element != null)
                {
                    objOld.DISCOUNT_ID.CurrentValue = int.Parse(element.InnerXml);
                }


                element = node.SelectSingleNode("PERCENTAGE");

                if (element != null)
                {
                    objOld.PERCENTAGE.CurrentValue = Convert.ToDouble(element.InnerXml);                    
                }

                element = node.SelectSingleNode("DISCOUNT_DESCRIPTION");

               
                if (element != null)
                {
                    objOld.DISCOUNT_DESCRIPTION.CurrentValue = Convert.ToString(element.InnerXml);          
                   
                }
                
                 strTranXML = this.GetTransactionLogXML(objOld, objNew);
               
            }
            return strTranXML;
        }      //Genrate populate old object from old dataser xml 

        private string GetTransactionLogXML(Cms.Model.Policy.ClsDiscountSurchargeInfo objOld, Cms.Model.Policy.ClsDiscountSurchargeInfo objNew)
        {
            string TransXML = string.Empty;
            //base.TRANS_TYPE_ID = 111;
           // base.CLIENT_ID = CUSTOMER_ID.CurrentValue;
            //base.RECORDED_BY = CREATED_BY.CurrentValue;
            //base.POLICYID = POLICY_ID.CurrentValue;
            //base.POLICYVERTRACKING_ID = POLICY_VERSION_ID.CurrentValue;
            
            objOld.DISCOUNT_ID.CurrentValue = objNew.DISCOUNT_ID.CurrentValue;
            objOld.PERCENTAGE.CurrentValue = objNew.PERCENTAGE.CurrentValue;
            //objOld.DISCOUNT_DESCRIPTION.CurrentValue = objNew.DISCOUNT_DESCRIPTION.CurrentValue.Trim();
            if (objOld.PERCENTAGE.IsChanged)
            {
                objOld.DISCOUNT_DESCRIPTION.CurrentValue = objNew.DISCOUNT_DESCRIPTION.CurrentValue.Trim() + " ";
            }
            else 
            {
                objOld.DISCOUNT_DESCRIPTION.CurrentValue = objNew.DISCOUNT_DESCRIPTION.CurrentValue.Trim();
            }
            TransXML = objOld.GenerateTransactionLogXML_New(false);
            return TransXML;
        }            //Genrate Transaction Xml from old and New value Of XML

        public int SaveTransaction(string oldxml, int CustomerId, int PolicyId, int Policy_version_Id, int Recordedby, string Trandesc)
        {
            int returnval = 0;
            String ConnStr = EbixDataLayer.DataWrapper.ConnString = DBConnString;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

            objTransactionInfo.TRANS_TYPE_ID = 111;
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
