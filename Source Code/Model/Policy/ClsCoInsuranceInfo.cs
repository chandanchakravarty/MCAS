/******************************************************************************************
<Author				: - Charles Gomes
<Start Date			: -	22/Mar/2010 
<End Date			: -	
<Description		: - Model Class of POL_CO_INSURANCE
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/
using System;
using System.Data;
using Cms.Model;
namespace Cms.Model.Policy
{
    /// <summary>
    /// Database Model for POL_CO_INSURANCE.
    /// </summary>
    public class ClsCoInsuranceInfo : Cms.Model.ClsCommonModel
    {
        private const string POL_CO_INSURANCE = "POL_CO_INSURANCE";
        private int Save_flag;
        public ClsCoInsuranceInfo()
        {
            base.dtModel.TableName = "POL_CO_INSURANCE";		// setting table name for data table that holds property values.
            this.AddColumns();								// add columns of the database table POL_CUSTOMER_POLICY_LIST
            base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
        }
        private void AddColumns()
        {
            base.dtModel.Columns.Add("CUSTOMER_ID", typeof(int));
            base.dtModel.Columns.Add("POLICY_ID", typeof(int));
            base.dtModel.Columns.Add("POLICY_VERSION_ID", typeof(int)); 
            base.dtModel.Columns.Add("COINSURANCE_ID", typeof(int)); 
            base.dtModel.Columns.Add("COMPANY_ID", typeof(int)); 
            base.dtModel.Columns.Add("CO_INSURER_NAME", typeof(string));   
            base.dtModel.Columns.Add("LEADER_FOLLOWER", typeof(int));
            base.dtModel.Columns.Add("COINSURANCE_PERCENT", typeof(double));
            base.dtModel.Columns.Add("COINSURANCE_FEE", typeof(double));
            base.dtModel.Columns.Add("BROKER_COMMISSION", typeof(double));
            base.dtModel.Columns.Add("TRANSACTION_ID", typeof(string));
            base.dtModel.Columns.Add("LEADER_POLICY_NUMBER", typeof(string));
            base.dtModel.Columns.Add("ENDORSEMENT_POLICY_NUMBER", typeof(string)); 

        }
        #region Database schema details

        // model for database field CUSTOMER_ID(int)
        public int CUSTOMER_ID
        {
            get
            {
                return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["CUSTOMER_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["CUSTOMER_ID"] = value;
            }
        }
        // model for database field POLICY_ID(int)
        public int POLICY_ID
        {
            get
            {
                return base.dtModel.Rows[0]["POLICY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["POLICY_ID"] = value;
            }
        }
        // model for database field POLICY_VERSION_ID(int)
        public int POLICY_VERSION_ID
        {
            get
            {
                return base.dtModel.Rows[0]["POLICY_VERSION_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POLICY_VERSION_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["POLICY_VERSION_ID"] = value;
            }
        }

        public int COINSURANCE_ID
        {
            get
            {
                return base.dtModel.Rows[0]["COINSURANCE_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COINSURANCE_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["COINSURANCE_ID"] = value;
            }
        }

        public int COMPANY_ID
        {
            get
            {
                return base.dtModel.Rows[0]["COMPANY_ID"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["COMPANY_ID"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["COMPANY_ID"] = value;
            }
        }
        public int LEADER_FOLLOWER
        {
            get
            {
                return base.dtModel.Rows[0]["LEADER_FOLLOWER"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["LEADER_FOLLOWER"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["LEADER_FOLLOWER"] = value;
            }
        }

        public string LEADER_POLICY_NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["LEADER_POLICY_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["LEADER_POLICY_NUMBER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["LEADER_POLICY_NUMBER"] = value;
            }
        }

        public string ENDORSEMENT_POLICY_NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["ENDORSEMENT_POLICY_NUMBER"] == DBNull.Value ? "" : base.dtModel.Rows[0]["ENDORSEMENT_POLICY_NUMBER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["ENDORSEMENT_POLICY_NUMBER"] = value;
            }
        }
        public string TRANSACTION_ID
        {
            get
            {
                return base.dtModel.Rows[0]["TRANSACTION_ID"] == DBNull.Value ? "" : base.dtModel.Rows[0]["TRANSACTION_ID"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["TRANSACTION_ID"] = value;
            }
        }

        public string CO_INSURER_NAME
        {
            get
            {
                return base.dtModel.Rows[0]["CO_INSURER_NAME"] == DBNull.Value ? "" : base.dtModel.Rows[0]["CO_INSURER_NAME"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["CO_INSURER_NAME"] = value;
            }
        }

        public double COINSURANCE_PERCENT
        {
            get
            {
                return base.dtModel.Rows[0]["COINSURANCE_PERCENT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["COINSURANCE_PERCENT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["COINSURANCE_PERCENT"] = value;
            }
        }

        public double COINSURANCE_FEE
        {
            get
            {
                return base.dtModel.Rows[0]["COINSURANCE_FEE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["COINSURANCE_FEE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["COINSURANCE_FEE"] = value;
            }
        }

        public double BROKER_COMMISSION
        {
            get
            {
                return base.dtModel.Rows[0]["BROKER_COMMISSION"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["BROKER_COMMISSION"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BROKER_COMMISSION"] = value;
            }
        }

        public int Action
        {

            get { return Save_flag; }
            set { Save_flag = value; }
        }
        #endregion


    }
}
