/******************************************************************************************
<Author					: -		Sneha
<Start Date				: -		31-10-2011
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
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

namespace Cms.Model.Maintenance
{
    [Serializable]
    public class ClsMasterDetailValueInfo : ClsModelBaseClass
    {
        public ClsMasterDetailValueInfo()
        {
            this.PropertyCollection();
        }
        #region Delare the add the parameter collection for the data wrapper class
        private void PropertyCollection()
        {
           
            base.htPropertyCollection.Add("TYPE_UNIQUE_ID",TYPE_UNIQUE_ID);
            base.htPropertyCollection.Add("TYPE_ID",TYPE_ID);
            base.htPropertyCollection.Add("CODE",CODE);
            base.htPropertyCollection.Add("DESCRIPTION", DESCRIPTION);
            base.htPropertyCollection.Add("RECOVERY_TYPE", RECOVERY_TYPE);
            base.htPropertyCollection.Add("LOSS_TYPE", LOSS_TYPE);
            base.htPropertyCollection.Add("NAME", NAME);
 
        }
        #endregion
        #region Declare the Property for every data table columns

        public EbixInt32 TYPE_UNIQUE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TYPE_UNIQUE_ID"]) == null ? new EbixInt32("TYPE_UNIQUE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TYPE_UNIQUE_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TYPE_UNIQUE_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixInt32 TYPE_ID
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TYPE_ID"]) == null ? new EbixInt32("TYPE_ID") : ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TYPE_ID"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixInt32)htPropertyCollection["TYPE_ID"]).CurrentValue = Convert.ToInt32(value);
            }
        }

        public EbixString CODE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CODE"]) == null ? new EbixString("CODE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CODE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["CODE"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString DESCRIPTION
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DESCRIPTION"]) == null ? new EbixString("DESCRIPTION") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DESCRIPTION"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["DESCRIPTION"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString RECOVERY_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RECOVERY_TYPE"]) == null ? new EbixString("RECOVERY_TYPE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RECOVERY_TYPE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["RECOVERY_TYPE"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString LOSS_TYPE
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOSS_TYPE"]) == null ? new EbixString("LOSS_TYPE") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOSS_TYPE"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["LOSS_TYPE"]).CurrentValue = Convert.ToString(value);
            }
        }

        public EbixString NAME
        {
            get
            {
                return ((Cms.EbixDataTypes.EbixString)htPropertyCollection["NAME"]) == null ? new EbixString("NAME") : ((Cms.EbixDataTypes.EbixString)htPropertyCollection["NAME"]);
            }
            set
            {
                ((Cms.EbixDataTypes.EbixString)htPropertyCollection["NAME"]).CurrentValue = Convert.ToString(value);
            }
        }

        #endregion
        public int AddMasterValueDetailInfo()
        {
            int returnResult = 0;
            try
            {

                base.Proc_Add_Name = "Proc_InsertMasterValue";

                base.ReturnIDName = "@TYPE_UNIQUE_ID";

                this.TYPE_UNIQUE_ID.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                this.RequiredTransactionLog = false;
                this.ProcReturnValue = true;
                returnResult = base.Save();

                this.TYPE_UNIQUE_ID.CurrentValue = base.ReturnIDNameValue; //get the out parameter

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnResult;
        }
        public int UpdateMasterValueInformation()
        {
            int returnValue = 0;
            try
            {


                base.Proc_Update_Name = "Proc_UpdateMasterValue";


                this.IS_ACTIVE.IsDBParam = false;
                this.MODIFIED_BY.IsDBParam = false;
                this.LAST_UPDATED_DATETIME.IsDBParam = false;
                this.CREATED_DATETIME.IsDBParam = false;
                this.CREATED_BY.IsDBParam = false;
                base.IS_ACTIVE.IsDBParam = false;

                returnValue = base.Update();


            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { }
            return returnValue;
        }

        public int ActivateDeactivateMasterValue()
        {
            int returnValue = 0;
            try
            {

                base.Proc_ActivateDeactivate_Name = "Proc_Active_Deactive_MasterValue";

                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@TYPE_UNIQUE_ID", TYPE_UNIQUE_ID.CurrentValue);
                base.htGetDataParamCollections.Add("@IS_ACTIVE", IS_ACTIVE.CurrentValue);
                returnValue = base.ActivateDeactivate();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }//catch (Ex
            return returnValue;

        }

        public DataSet FetchDataValue(int TYPE_UNIQUE_ID)
        {
            DataSet dsCount = null;

            try
            {
                base.Proc_FetchData = "Proc_GetMasterValue";
                base.htGetDataParamCollections.Clear();
                base.htGetDataParamCollections.Add("@TYPE_UNIQUE_ID", TYPE_UNIQUE_ID);
                dsCount = base.GetData();

            }//try
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsCount;
        }
    }
}

 