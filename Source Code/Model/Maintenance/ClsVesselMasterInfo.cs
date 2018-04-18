/******************************************************************************************
<Author				: -   
<Start Date				: -	8/3/2005 2:18:51 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/
using System;
using System.Data;
using Cms.Model;

namespace Cms.Model.Maintenance
{
    public class ClsVesselMasterInfo : Cms.Model.ClsCommonModel
    {
        private const string MNT_VESSEL_MASTER = "MNT_VESSEL_MASTER";
        public ClsVesselMasterInfo()
		{
            base.dtModel.TableName = "MNT_VESSEL_MASTER";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table MNT_LOOKUP_VALUES
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
        private void AddColumns()
        {
            base.dtModel.Columns.Add("VESSEL_ID", typeof(int));
            base.dtModel.Columns.Add("VESSEL_NAME", typeof(string));
            base.dtModel.Columns.Add("GRT", typeof(string));
            base.dtModel.Columns.Add("IMO", typeof(string));
            base.dtModel.Columns.Add("NRT", typeof(string));
            base.dtModel.Columns.Add("FLAG", typeof(string));
            base.dtModel.Columns.Add("CLASSIFICATION", typeof(string));
            base.dtModel.Columns.Add("YEAR_BUILT", typeof(int));
            base.dtModel.Columns.Add("LINER", typeof(int));
            base.dtModel.Columns.Add("TYPE_OF_VESSEL", typeof(int));
            base.dtModel.Columns.Add("CLASS_TYPE", typeof(int));
            base.dtModel.Columns.Add("CLASS", typeof(string));
            base.dtModel.Columns.Add("DAY_PRIOR_TO_SAILING_DATE", typeof(string));
            base.dtModel.Columns.Add("TOTAL_SHIPMENT_DAY", typeof(string));
            //base.dtModel.Columns.Add("IS_ACTIVE", typeof(string));

        }
        public int VESSEL_ID
        {
            get
            {
                return base.dtModel.Rows[0]["VESSEL_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["VESSEL_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["VESSEL_ID"] = value;
            }
        }
        public int YEAR_BUILT
        {
            get
            {
                return base.dtModel.Rows[0]["YEAR_BUILT"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["YEAR_BUILT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["YEAR_BUILT"] = value;
            }
        }
        public int LINER
        {
            get
            {
                return base.dtModel.Rows[0]["LINER"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LINER"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["LINER"] = value;
            }
        }
        public int TYPE_OF_VESSEL
        {
            get
            {
                return base.dtModel.Rows[0]["TYPE_OF_VESSEL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["TYPE_OF_VESSEL"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["TYPE_OF_VESSEL"] = value;
            }
        }
        public int CLASS_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["CLASS_TYPE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CLASS_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CLASS_TYPE"] = value;
            }
        }
        public string VESSEL_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["VESSEL_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["VESSEL_NAME"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["VESSEL_NAME"] = value;
            }
        }
        public string GRT
        {
            get
            {
                return base.dtModel.Rows[0]["GRT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["GRT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["GRT"] = value;
            }
        }
        public string IMO
        {
            get
            {
                return base.dtModel.Rows[0]["IMO"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IMO"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["IMO"] = value;
            }
        }
        public string NRT
        {
            get
            {
                return base.dtModel.Rows[0]["NRT"] == DBNull.Value ? "" : base.dtModel.Rows[0]["NRT"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["NRT"] = value;
            }
        }
        public string FLAG
        {
            get
            {
                return base.dtModel.Rows[0]["FLAG"] == DBNull.Value ? "" : base.dtModel.Rows[0]["FLAG"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["FLAG"] = value;
            }
        }
        public string CLASSIFICATION
        {
            get
            {
                return base.dtModel.Rows[0]["CLASSIFICATION"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CLASSIFICATION"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CLASSIFICATION"] = value;
            }
        }
        public string CLASS
        {
            get
            {
                return base.dtModel.Rows[0]["CLASS"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CLASS"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CLASS"] = value;
            }
        }
        public string DAY_PRIOR_TO_SAILING_DATE
        {
            get
            {
                return base.dtModel.Rows[0]["DAY_PRIOR_TO_SAILING_DATE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["DAY_PRIOR_TO_SAILING_DATE"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["DAY_PRIOR_TO_SAILING_DATE"] = value;
            }
        }
        public string TOTAL_SHIPMENT_DAY
        {
            get
            {
                return base.dtModel.Rows[0]["TOTAL_SHIPMENT_DAY"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TOTAL_SHIPMENT_DAY"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["TOTAL_SHIPMENT_DAY"] = value;
            }
        }
        //public string IS_ACTIVE
        //{
        //    get
        //    {
        //        return base.dtModel.Rows[0]["IS_ACTIVE"] == DBNull.Value ? "" : base.dtModel.Rows[0]["IS_ACTIVE"].ToString();
        //    }
        //    set
        //    {
        //        base.dtModel.Rows[0]["IS_ACTIVE"] = value;
        //    }
        //}
       
    }
}
