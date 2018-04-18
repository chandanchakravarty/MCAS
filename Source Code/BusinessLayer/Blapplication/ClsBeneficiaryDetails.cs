using System;
using Cms.BusinessLayer.BlCommon;
using System.Data; 
using Cms.DataLayer; 
using System.Xml; 
using System.IO;
using Cms.Model.Policy;
//using System.Data;
using System.Text;
//using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
//using Cms.DataLayer;
using Cms.Model.Application;
//using Cms.BusinessLayer.BlCommon;  


namespace Cms.BusinessLayer.BlApplication
{
    public class ClsBeneficiaryDetails : Cms.BusinessLayer.BlCommon.ClsCommon
    {

        public ClsBeneficiaryDetails()
        {
        }
        
        public int AddBeneficiaryInformation(ClsBeneficiaryInfo objBeneficiaryInfo)
        {
            int returnValue = 0;

            if (objBeneficiaryInfo.RequiredTransactionLog)
            {
                objBeneficiaryInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Policies/Aspx/AddBeneficiaryDetails.aspx.resx");
                returnValue = objBeneficiaryInfo.AddBeneficiaryInformation();

            }
            return returnValue;
        }

        public int UpdateBeneficiaryInformation(ClsBeneficiaryInfo objBeneficiaryInfo)
        {
            int returnValue = 0;

            if (objBeneficiaryInfo.RequiredTransactionLog)
            {
                objBeneficiaryInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Policies/Aspx/AddBeneficiaryDetails.aspx.resx");

                returnValue = objBeneficiaryInfo.UpdateBeneficiaryInformation();

            }
            return returnValue;
        }

        public Boolean FetchBenificiaryData(ref ClsBeneficiaryInfo objBeneficiaryInfo)
        {


            Boolean returnValue = false;
            DataSet dsCount = null;

            try
            {
                dsCount = objBeneficiaryInfo.FetchData();


                if (dsCount.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(dsCount, objBeneficiaryInfo);
                    returnValue = true;

                }//if (dsCount.Tables[0].Rows.Count != 0)
                else
                    returnValue = false;


            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return returnValue;

        }


        public string GetTotalShareofBeneficiary(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, int RISK_ID, int BENEFICIARY_ID)
        {


            string TotalShare = "";
            DataSet dsCount = null;

            try
            {
                ClsBeneficiaryInfo ObjBen = new ClsBeneficiaryInfo();

                dsCount = ObjBen.GetTotalShareofBeneficiary(CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,RISK_ID,BENEFICIARY_ID);


                if (dsCount.Tables[0].Rows.Count != 0)
                {                   
                 TotalShare=dsCount.Tables[0].Rows[0]["BENEFICIARY_SHARE"].ToString();

                }

            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return TotalShare;

        }

        public int DeleteBeneficiaryInformation(ClsBeneficiaryInfo objBeneficiaryInfo)
        {
            int returnValue = 0;

            if (objBeneficiaryInfo.RequiredTransactionLog)
            {
                objBeneficiaryInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Policies/Aspx/AddBeneficiaryDetails.aspx.resx");

                returnValue = objBeneficiaryInfo.DeleteBeneficiaryInformation();

            }
            return returnValue;
        }




    }
}

