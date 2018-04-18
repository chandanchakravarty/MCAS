/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	31-03-2010
<End Date			: -	
<Description		: - 
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 7 April 2010
<Modified By		: - P K Chandel
<Purpose			: - 
*******************************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using Cms.Model.Policy.NamedPerils;
using System.Data;
using Cms.Model.Support;
using System.Web.UI.WebControls;
using System.Reflection;
 using System.Xml;
namespace Cms.Blapplication
{
    public class ClsNamedPerils:ClsModelBaseClass
    {
        
        public ClsNamedPerils()
        { }
        public int ActivateDeactivateNamedPeril(ClsNamedPerilsInfo objNamedPerilsinfo, String CalledFrom)
        {
            int returnValue = 0;
            if (objNamedPerilsinfo.RequiredTransactionLog)
            {
                objNamedPerilsinfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/NamedPerils/AddNamedPerils.aspx.resx");
                switch (CalledFrom.ToUpper())
                {
                    case "NAMEDPERILS":
                        if (objNamedPerilsinfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                            objNamedPerilsinfo.TRANS_TYPE_ID = 96;   //TRANS_TYPE_ID For Add Location Information Activate
                        else
                            objNamedPerilsinfo.TRANS_TYPE_ID = 97;   //TRANS_TYPE_ID For Add Location Information Deactivate

                        break;
                        
                    case "ERISK":
                        if (objNamedPerilsinfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                            objNamedPerilsinfo.TRANS_TYPE_ID = 360;   //TRANS_TYPE_ID For Add Location Information Activate
                        else
                            objNamedPerilsinfo.TRANS_TYPE_ID = 361;   //TRANS_TYPE_ID For Add Location Information Deactivate

                        break;

                    default:
                        break;
                }//switch (CalledFrom)

                
                returnValue = objNamedPerilsinfo.ActivateDeactivateNamedPerils();
            }
            return returnValue;
        }
        public int DeleteNamedPeril(ClsNamedPerilsInfo objNamedPerilsinfo, Int32 ConfirmValue, String CalledFrom)
        {
            int returnValue = 0;
            if (objNamedPerilsinfo.RequiredTransactionLog)
            {
                objNamedPerilsinfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/NamedPerils/AddNamedPerils.aspx.resx");
                switch (CalledFrom.ToUpper())
                {
                    case "NAMEDPERILS":
                        objNamedPerilsinfo.TRANS_TYPE_ID = 94;
                        break;
                    case "ERISK":
                        objNamedPerilsinfo.TRANS_TYPE_ID = 358;
                        break;

                    default:
                        break;
                }//switch (CalledFrom)
                returnValue = objNamedPerilsinfo.DeleteNamedParilsData(ConfirmValue);
            }
            return returnValue;
        }
        public int AddNamedPeril(ClsNamedPerilsInfo objNamedPerilsinfo, String CalledFrom)
        {
            int returnValue = 0;

            if (objNamedPerilsinfo.RequiredTransactionLog)
            {
                objNamedPerilsinfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/NamedPerils/AddNamedPerils.aspx.resx");

                switch (CalledFrom.ToUpper())
                {
                    case "NAMEDPERILS":
                        objNamedPerilsinfo.TRANS_TYPE_ID = 93;  
                        break;
                    case "ERISK":
                        objNamedPerilsinfo.TRANS_TYPE_ID = 357;
                        break;
                  
                    default:
                        break;
                }//switch (CalledFrom)

                returnValue = objNamedPerilsinfo.AddNamedParilsData();

            }
            return returnValue;
        }
        public int UpdateNamedParils(ClsNamedPerilsInfo objNamedPerilsinfo, String CalledFrom)
        {
            int returnValue = 0;

            if (objNamedPerilsinfo.RequiredTransactionLog)
            {
                objNamedPerilsinfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/NamedPerils/AddNamedPerils.aspx.resx");

                switch (CalledFrom.ToUpper())
                {
                    case "NAMEDPERILS":
                        objNamedPerilsinfo.TRANS_TYPE_ID = 95;
                        break;
                    case "ERISK":
                        objNamedPerilsinfo.TRANS_TYPE_ID = 359;
                        break;

                    default:
                        break;
                }//switch (CalledFrom)
                returnValue = objNamedPerilsinfo.UpdateNamedParilsData( );

            }
            return returnValue;
        }
        public void GeLocationNumNAddress(DropDownList cmbLOCATION ,Int32 CustomerID)
        {
            ClsNamedPerilsInfo objNamedPerilsinfo = new ClsNamedPerilsInfo();
             
            DataSet ds=new DataSet();

            ds = objNamedPerilsinfo.GetLocationNumNAddress(CustomerID);
            
            if (ds.Tables[0].Rows.Count != 0)
            {
                DataView dv = ds.Tables[0].DefaultView;
                dv.Sort = "LOCATION_ADDRESS";
                cmbLOCATION.DataSource = dv;
                cmbLOCATION.DataTextField = "LOCATION_ADDRESS";
                cmbLOCATION.DataValueField = "LOCATION_ID";
                cmbLOCATION.DataBind();
                cmbLOCATION.Items.Insert(0, "");
                    
            }
            else
            {
                cmbLOCATION.Items.Insert(0, ""); 
            }

             
        }
        /// <summary>
        /// Use to get the location details for the Named perils product based on customer id and policy id
        /// </summary>
        /// <param name="cmbLOCATION"></param>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        public void GetLocationDetailsForNamedPerilsinfo(System.Web.UI.WebControls.DropDownList cmbLOCATION, Int32 CustomerID, Int32 PolicyID)
        {
            ClsNamedPerilsInfo objNamedPerilsinfo = new ClsNamedPerilsInfo();
            DataSet ds = new DataSet();

            ds = objNamedPerilsinfo.GetLocationDetailsForNamedPerils(CustomerID, PolicyID);

            if (ds.Tables[0].Rows.Count != 0)
            {
                DataView dv = ds.Tables[0].DefaultView;
                dv.Sort = "LOCATION_ADDRESS";
                cmbLOCATION.DataSource = dv;
                cmbLOCATION.DataTextField = "LOCATION_ADDRESS";
                cmbLOCATION.DataValueField = "LOCATION_ID";
                cmbLOCATION.DataBind();
                cmbLOCATION.Items.Insert(0, "");

            }
            else
            {
                cmbLOCATION.Items.Insert(0, "");
            }


        }//public void GetLocationDetailsForNamedPerilsinfo(System.Web.UI.WebControls.DropDownList cmbLOCATION, Int32 CustomerID, Int32 PolicyID)
        public Boolean FetchData(ref ClsNamedPerilsInfo objNamedPerilsInfo)
        {

            Boolean returnValue = false;
            DataSet dsCount = null;

            try
            {
                dsCount = objNamedPerilsInfo.FetchData();


                if (dsCount.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(dsCount, objNamedPerilsInfo);
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
       
        /*
        public void PopulateEbixPageModel(DataSet ds,ref ClsNamedPerilsInfo objNamedPerilInfo)
        {
            for (int Count = 0; Count < ds.Tables[0].Rows.Count; Count++)
            {
                objNamedPerilInfo.PERIL_ID.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["PERIL_ID"]);
                objNamedPerilInfo.POLICY_ID.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["POLICY_ID"]);
                objNamedPerilInfo.POLICY_VERSION_ID.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["POLICY_VERSION_ID"]);
                objNamedPerilInfo.CUSTOMER_ID.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["CUSTOMER_ID"]);
                objNamedPerilInfo.IS_ACTIVE.CurrentValue = (String)Convert.ToString(ds.Tables[0].Rows[Count]["IS_ACTIVE"]);

                objNamedPerilInfo.LOCATION.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["LOCATION"]);
                objNamedPerilInfo.VR.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["VR"]);
                objNamedPerilInfo.BUILDING.CurrentValue = (Int32)ds.Tables[0].Rows[Count]["BUILDING"];

                objNamedPerilInfo.CONTENT_VALUE.CurrentValue = (String)Convert.ToString(ds.Tables[0].Rows[Count]["CONTENT_VALUE"]);

                objNamedPerilInfo.RAW_MATERIAL_VALUE.CurrentValue = (String)Convert.ToString(ds.Tables[0].Rows[Count]["RAW_MATERIAL_VALUE"]);

                objNamedPerilInfo.RAWVALUES.CurrentValue = (String)Convert.ToString(ds.Tables[0].Rows[Count]["RAWVALUES"]);

                objNamedPerilInfo.LMI.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["LMI"]);

                objNamedPerilInfo.MRI.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["MRI"]);

                objNamedPerilInfo.CLAIM_RATIO.CurrentValue = (String)Convert.ToString(ds.Tables[0].Rows[Count]["CLAIM_RATIO"]);

                objNamedPerilInfo.LOSS.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["LOSS"]);

                objNamedPerilInfo.TYPE.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["TYPE"]);

                objNamedPerilInfo.LOYALTY.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["LOYALTY"]);

                objNamedPerilInfo.PERC_LOYALTY.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["PERC_LOYALTY"]);

                objNamedPerilInfo.DEDUCTIBLE_OPTION.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["DEDUCTIBLE_OPTION"]);

                objNamedPerilInfo.MULTIPLE_DEDUCTIBLE.CurrentValue = (String)Convert.ToString(ds.Tables[0].Rows[Count]["MULTIPLE_DEDUCTIBLE"]);

                objNamedPerilInfo.PARKING_SPACES.CurrentValue = (String)Convert.ToString(ds.Tables[0].Rows[Count]["PARKING_SPACES"]);

                objNamedPerilInfo.ACTIVITY_TYPE.CurrentValue = (String)ds.Tables[0].Rows[Count]["ACTIVITY_TYPE"];

                objNamedPerilInfo.OCCUPANCY.CurrentValue = (String)Convert.ToString(ds.Tables[0].Rows[Count]["OCCUPANCY"]);

                objNamedPerilInfo.CONSTRUCTION.CurrentValue = (String)Convert.ToString(ds.Tables[0].Rows[Count]["CONSTRUCTION"]);

                objNamedPerilInfo.CATEGORY.CurrentValue = (String)Convert.ToString(ds.Tables[0].Rows[Count]["CATEGORY"]);

                objNamedPerilInfo.ASSIST24.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["ASSIST24"]);

                objNamedPerilInfo.E_FIRE.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["E_FIRE"]);

                objNamedPerilInfo.S_FIRE_UNIT.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["S_FIRE_UNIT"]);

                objNamedPerilInfo.S_FIXED_FOAM.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["S_FIXED_FOAM"]);

                objNamedPerilInfo.S_FOAM_PER_MANUAL.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["S_FOAM_PER_MANUAL"]);

                objNamedPerilInfo.S_FIXED_INSERT_GAS.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["S_FIXED_INSERT_GAS"]);

                objNamedPerilInfo.S_MANUAL_INERT_GAS.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["S_MANUAL_INERT_GAS"]);

                objNamedPerilInfo.CAR_COMBAT.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["CAR_COMBAT"]);

                objNamedPerilInfo.CORRAL_SYSTEM.CurrentValue = (String)Convert.ToString(ds.Tables[0].Rows[Count]["CORRAL_SYSTEM"]);

                objNamedPerilInfo.S_DETECT_ALARM.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["S_DETECT_ALARM"]);

                objNamedPerilInfo.HYDRANTS.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["HYDRANTS"]);

                objNamedPerilInfo.SHOWERS.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["SHOWERS"]);

                objNamedPerilInfo.SHOWER_CLASSIFICATION.CurrentValue = (String)Convert.ToString(ds.Tables[0].Rows[Count]["SHOWER_CLASSIFICATION"]);

                objNamedPerilInfo.FIRE_CORPS.CurrentValue = (String)Convert.ToString(ds.Tables[0].Rows[Count]["FIRE_CORPS"]);

                objNamedPerilInfo.PUNCTUATION_QUEST.CurrentValue = (Int32)Convert.ToInt32(ds.Tables[0].Rows[Count]["PUNCTUATION_QUEST"]);

                objNamedPerilInfo.REMARKS.CurrentValue = (String)Convert.ToString(ds.Tables[0].Rows[Count]["REMARKS"]);

            }
        }*/
    }
}
