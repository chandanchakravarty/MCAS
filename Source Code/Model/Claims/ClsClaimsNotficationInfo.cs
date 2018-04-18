/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	26/04/2006
<End Date				: -	
<Description			: - Models CLM_CLAIM_INFO
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
namespace Cms.Model.Claims
{
	/// <summary>
	/// Database Model for CLM_CLAIM_INFO.
	/// </summary>
	public class ClsClaimsNotficationInfo : Cms.Model.ClsCommonModel
	{
		private const string CLM_CLAIM_INFO = "CLM_CLAIM_INFO";
		public ClsClaimsNotficationInfo()
		{
			base.dtModel.TableName = "CLM_CLAIM_INFO";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table CLM_CLAIM_INFO
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_ID",typeof(int));
			base.dtModel.Columns.Add("POLICY_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("CLAIM_ID",typeof(int));			
			base.dtModel.Columns.Add("CLAIM_NUMBER",typeof(string));
			base.dtModel.Columns.Add("LOSS_DATE",typeof(DateTime));			
			base.dtModel.Columns.Add("ADJUSTER_CODE",typeof(string));			
			//Added by Asfa - 29/Aug/2007
			base.dtModel.Columns.Add("ADJUSTER_ID",typeof(int));			
			base.dtModel.Columns.Add("REPORTED_BY",typeof(string));
			base.dtModel.Columns.Add("CATASTROPHE_EVENT_CODE",typeof(int));
			base.dtModel.Columns.Add("CLAIMANT_INSURED",typeof(bool));
			base.dtModel.Columns.Add("INSURED_RELATIONSHIP",typeof(string));			
			base.dtModel.Columns.Add("CLAIMANT_NAME",typeof(string));
            base.dtModel.Columns.Add("CLAIMANT_TYPE", typeof(int));
			base.dtModel.Columns.Add("COUNTRY",typeof(int));			
			base.dtModel.Columns.Add("ZIP",typeof(string));			
			base.dtModel.Columns.Add("ADDRESS1",typeof(string));
			base.dtModel.Columns.Add("ADDRESS2",typeof(string));
			base.dtModel.Columns.Add("CITY",typeof(string));
			base.dtModel.Columns.Add("HOME_PHONE",typeof(string));
			base.dtModel.Columns.Add("WORK_PHONE",typeof(string));
			base.dtModel.Columns.Add("MOBILE_PHONE",typeof(string));
			base.dtModel.Columns.Add("WHERE_CONTACT",typeof(string));
			base.dtModel.Columns.Add("WHEN_CONTACT",typeof(string));
			base.dtModel.Columns.Add("DIARY_DATE",typeof(DateTime));
			base.dtModel.Columns.Add("CLAIM_STATUS",typeof(int));
			base.dtModel.Columns.Add("OUTSTANDING_RESERVE",typeof(double));			
			base.dtModel.Columns.Add("RESINSURANCE_RESERVE",typeof(double));			
			base.dtModel.Columns.Add("PAID_LOSS",typeof(double));			
			base.dtModel.Columns.Add("PAID_EXPENSE",typeof(double));			
			base.dtModel.Columns.Add("RECOVERIES",typeof(double));			
			base.dtModel.Columns.Add("CLAIM_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("SUB_ADJUSTER",typeof(string));
			base.dtModel.Columns.Add("SUB_ADJUSTER_CONTACT",typeof(string));
			base.dtModel.Columns.Add("EXTENSION",typeof(string));
			base.dtModel.Columns.Add("DUMMY_POLICY_ID",typeof(int));			
			base.dtModel.Columns.Add("LOSS_TIME_AM_PM",typeof(int));
			base.dtModel.Columns.Add("LITIGATION_FILE",typeof(int));
			base.dtModel.Columns.Add("HOMEOWNER",typeof(string));
			base.dtModel.Columns.Add("RECR_VEH",typeof(string));
			base.dtModel.Columns.Add("IN_MARINE",typeof(string));
			base.dtModel.Columns.Add("RECOVERY",typeof(double));	
			base.dtModel.Columns.Add("RECOVERY_OUTSTANDING",typeof(double));
			base.dtModel.Columns.Add("STATE",typeof(int));
			base.dtModel.Columns.Add("CLAIMANT_PARTY",typeof(int));
			base.dtModel.Columns.Add("LINKED_TO_CLAIM",typeof(string));
			base.dtModel.Columns.Add("ADD_FAULT",typeof(string));
			base.dtModel.Columns.Add("TOTAL_LOSS",typeof(string));
			base.dtModel.Columns.Add("NOTIFY_REINSURER",typeof(int));
			base.dtModel.Columns.Add("LOB_ID",typeof(string));
			base.dtModel.Columns.Add("REPORTED_TO",typeof(string));
			base.dtModel.Columns.Add("FIRST_NOTICE_OF_LOSS",typeof(DateTime));
			base.dtModel.Columns.Add("LINKED_CLAIM_ID_LIST",typeof(string));
			base.dtModel.Columns.Add("RECIEVE_PINK_SLIP_USERS_LIST",typeof(string));
			base.dtModel.Columns.Add("PINK_SLIP_TYPE_LIST",typeof(string));
			//following field is added(not there in the physical table though) to store the newly added users 
			//belonging to the pink slip notification..diary entry will be made to only these new users
			//when a new claim is inserted, it will consist of all the selected users; 
			//during update, it will consist of newly added users only
			base.dtModel.Columns.Add("NEW_RECIEVE_PINK_SLIP_USERS_LIST",typeof(string));
			base.dtModel.Columns.Add("CLAIM_STATUS_UNDER",typeof(int));
			base.dtModel.Columns.Add("AT_FAULT_INDICATOR",typeof(int));//Done for Itrack Issue 6620 on 27 Nov 09

            base.dtModel.Columns.Add("OFFCIAL_CLAIM_NUMBER", typeof(string));//Added by Santosh Kumar Gautam on 14 Dec 2010
            base.dtModel.Columns.Add("LAST_DOC_RECEIVE_DATE", typeof(DateTime));

            base.dtModel.Columns.Add("REINSURANCE_TYPE", typeof(int));//Added by Santosh Kumar Gautam on 08 Feb 2011
            base.dtModel.Columns.Add("REIN_CLAIM_NUMBER", typeof(string));//Added by Santosh Kumar Gautam on 08 Feb 2011
            base.dtModel.Columns.Add("REIN_LOSS_NOTICE_NUM", typeof(string));//Added by Santosh Kumar Gautam on 08 Feb 2011
            base.dtModel.Columns.Add("IS_VICTIM_CLAIM", typeof(int));//Added by Santosh Kumar Gautam on 08 Feb 2011
            base.dtModel.Columns.Add("POSSIBLE_PAYMENT_DATE", typeof(DateTime));//Added by Santosh Kumar Gautam on 15 Feb 2011

            
		}
		#region Database schema details
		// model for database field CUSTOMER_ID(int)
		public int CUSTOMER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CUSTOMER_ID"].ToString());
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
				return base.dtModel.Rows[0]["POLICY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["POLICY_ID"].ToString());
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
				return base.dtModel.Rows[0]["POLICY_VERSION_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["POLICY_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POLICY_VERSION_ID"] = value;
			}
		}
		// model for database field CLAIM_ID(int)
		public int CLAIM_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIM_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CLAIM_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIM_ID"] = value;
			}
		}
				
		// model for database field CLAIM_NUMBER(string)
		public string CLAIM_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIM_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CLAIM_NUMBER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CLAIM_NUMBER"] = value;
			}
		}
		// model for database field LOSS_DATE(DateTime)
		public DateTime LOSS_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LOSS_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_DATE"] = value;
			}
		}		
		// model for database field ADJUSTER_CODE(string)
		public string ADJUSTER_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["ADJUSTER_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ADJUSTER_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADJUSTER_CODE"] = value;
			}
		}
		// model for database field ADJUSTER_ID(int)
		public int ADJUSTER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["ADJUSTER_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["ADJUSTER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["ADJUSTER_ID"] = value;
			}
		}
		// model for database field REPORTED_BY(string)
		public string REPORTED_BY
		{
			get
			{
				return base.dtModel.Rows[0]["REPORTED_BY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REPORTED_BY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REPORTED_BY"] = value;
			}
		}
		// model for database field CATASTROPHE_EVENT_CODE(int)
		public int CATASTROPHE_EVENT_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["CATASTROPHE_EVENT_CODE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CATASTROPHE_EVENT_CODE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CATASTROPHE_EVENT_CODE"] = value;
			}
		}	
		// model for database field CLAIMANT_INSURED(bool)
		public bool CLAIMANT_INSURED
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIMANT_INSURED"] == DBNull.Value ? Convert.ToBoolean(null) :bool.Parse( base.dtModel.Rows[0]["CLAIMANT_INSURED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIMANT_INSURED"] = value;
			}
		}
		// model for database field INSURED_RELATIONSHIP(string)		
		public string INSURED_RELATIONSHIP
		{
			get
			{
				return base.dtModel.Rows[0]["INSURED_RELATIONSHIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["INSURED_RELATIONSHIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["INSURED_RELATIONSHIP"] = value;
			}
		}	
		// model for database field CLAIMANT_NAME(string)
		public string CLAIMANT_NAME
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIMANT_NAME"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CLAIMANT_NAME"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CLAIMANT_NAME"] = value;
			}
		}
        public int CLAIMANT_TYPE
		{
			get
			{
                return base.dtModel.Rows[0]["CLAIMANT_TYPE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CLAIMANT_TYPE"].ToString());
			}
			set
			{
                base.dtModel.Rows[0]["CLAIMANT_TYPE"] = value;
			}
		}

        
		// model for database field COUNTRY(int)
		public int COUNTRY
		{
			get
			{
				return base.dtModel.Rows[0]["COUNTRY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["COUNTRY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["COUNTRY"] = value;
			}
		}
		// model for database field ZIP(string)
		public string ZIP
		{
			get
			{
				return base.dtModel.Rows[0]["ZIP"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ZIP"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ZIP"] = value;
			}
		}
		// model for database field ADDRESS1(string)
		public string ADDRESS1
		{
			get
			{
				return base.dtModel.Rows[0]["ADDRESS1"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ADDRESS1"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADDRESS1"] = value;
			}
		}
		// model for database field ADDRESS2(string)
		public string ADDRESS2
		{
			get
			{
				return base.dtModel.Rows[0]["ADDRESS2"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ADDRESS2"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADDRESS2"] = value;
			}
		}
		// model for database field CITY(string)
		public string CITY
		{
			get
			{
				return base.dtModel.Rows[0]["CITY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CITY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CITY"] = value;
			}
		}
		// model for database field HOME_PHONE(string)
		public string HOME_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["HOME_PHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOME_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOME_PHONE"] = value;
			}
		}
		// model for database field WORK_PHONE(string)
		public string WORK_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["WORK_PHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["WORK_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["WORK_PHONE"] = value;
			}
		}
		
		// model for database field MOBILE_PHONE(string)
		public string MOBILE_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["MOBILE_PHONE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["MOBILE_PHONE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["MOBILE_PHONE"] = value;
			}
		}		
		// model for database field WHERE_CONTACT(string)
		public string WHERE_CONTACT
		{
			get
			{
				return base.dtModel.Rows[0]["WHERE_CONTACT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["WHERE_CONTACT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["WHERE_CONTACT"] = value;
			}
		}		
		// model for database field WHEN_CONTACT(string)
		public string WHEN_CONTACT
		{
			get
			{
				return base.dtModel.Rows[0]["WHEN_CONTACT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["WHEN_CONTACT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["WHEN_CONTACT"] = value;
			}
		}		
		// model for database field DIARY_DATE(DateTime)
		public DateTime DIARY_DATE
		{
			get
			{
				return base.dtModel.Rows[0]["DIARY_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["DIARY_DATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DIARY_DATE"] = value;
			}
		}
		// model for database field CLAIM_STATUS(int)
		public int CLAIM_STATUS
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIM_STATUS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CLAIM_STATUS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIM_STATUS"] = value;
			}
		}
		// model for database field CLAIM_STATUS_UNDER(int)
		public int CLAIM_STATUS_UNDER
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIM_STATUS_UNDER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CLAIM_STATUS_UNDER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIM_STATUS_UNDER"] = value;
			}
		}		
		// model for database field OUTSTANDING_RESERVE(double)
		public double OUTSTANDING_RESERVE
		{
			get
			{
				return base.dtModel.Rows[0]["OUTSTANDING_RESERVE"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["OUTSTANDING_RESERVE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OUTSTANDING_RESERVE"] = value;
			}
		}
		// model for database field RESINSURANCE_RESERVE(double)
		public double RESINSURANCE_RESERVE
		{
			get
			{
				return base.dtModel.Rows[0]["RESINSURANCE_RESERVE"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["RESINSURANCE_RESERVE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RESINSURANCE_RESERVE"] = value;
			}
		}
		// model for database field PAID_LOSS(double)
		public double PAID_LOSS
		{
			get
			{
				return base.dtModel.Rows[0]["PAID_LOSS"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["PAID_LOSS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PAID_LOSS"] = value;
			}
		}
		// model for database field PAID_EXPENSE(double)
		public double PAID_EXPENSE
		{
			get
			{
				return base.dtModel.Rows[0]["PAID_EXPENSE"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["PAID_EXPENSE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PAID_EXPENSE"] = value;
			}
		}
		
		// model for database field RECOVERIES(double)
		public double RECOVERIES
		{
			get
			{
				return base.dtModel.Rows[0]["RECOVERIES"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["RECOVERIES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RECOVERIES"] = value;
			}
		}
		
		// model for database field CLAIM_DESCRIPTION(string)
		public string CLAIM_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIM_DESCRIPTION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["CLAIM_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["CLAIM_DESCRIPTION"] = value;
			}
		}	
		// model for database field SUB_ADJUSTER(string)
		public string SUB_ADJUSTER
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_ADJUSTER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SUB_ADJUSTER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_ADJUSTER"] = value;
			}
		}	
		// model for database field SUB_ADJUSTER_CONTACT(string)
		public string SUB_ADJUSTER_CONTACT
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_ADJUSTER_CONTACT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SUB_ADJUSTER_CONTACT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_ADJUSTER_CONTACT"] = value;
			}
		}	
		// model for database field EXTENSION(string)
		public string EXTENSION
		{
			get
			{
				return base.dtModel.Rows[0]["EXTENSION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["EXTENSION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["EXTENSION"] = value;
			}
		}	
		// model for database field DUMMY_POLICY_ID(int)
		public int DUMMY_POLICY_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DUMMY_POLICY_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DUMMY_POLICY_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DUMMY_POLICY_ID"] = value;
			}
		}
		// model for database field LOSS_TIME_AM_PM(int)
		public int LOSS_TIME_AM_PM
		{
			get
			{
				return base.dtModel.Rows[0]["LOSS_TIME_AM_PM"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LOSS_TIME_AM_PM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOSS_TIME_AM_PM"] = value;
			}
		}
		// model for database field LITIGATION_FILE(string)

        public int LITIGATION_FILE
		{
			get
			{
                return base.dtModel.Rows[0]["LITIGATION_FILE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["LITIGATION_FILE"].ToString());
			}
			set
			{
                base.dtModel.Rows[0]["LITIGATION_FILE"] = value;
			}
		}
		// model for database field HOMEOWNER(string)
		public string HOMEOWNER
		{
			get
			{
				return base.dtModel.Rows[0]["HOMEOWNER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["HOMEOWNER"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["HOMEOWNER"] = value;
			}
		}	
		// model for database field RECR_VEH(string)
		public string RECR_VEH
		{
			get
			{
				return base.dtModel.Rows[0]["RECR_VEH"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["RECR_VEH"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RECR_VEH"] = value;
			}
		}	
		// model for database field IN_MARINE(string)
		public string IN_MARINE
		{
			get
			{
				return base.dtModel.Rows[0]["IN_MARINE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IN_MARINE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["IN_MARINE"] = value;
			}
		}	
		// model for database field RECOVERY(double)
		public double RECOVERY
		{
			get
			{
				return base.dtModel.Rows[0]["RECOVERY"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["RECOVERY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RECOVERY"] = value;
			}
		}
		// model for database field RECOVERY_OUTSTANDING(double)
		public double RECOVERY_OUTSTANDING
		{
			get
			{
				return base.dtModel.Rows[0]["RECOVERY_OUTSTANDING"] == DBNull.Value ? Convert.ToDouble(null) : double.Parse(base.dtModel.Rows[0]["RECOVERY_OUTSTANDING"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["RECOVERY_OUTSTANDING"] = value;
			}
		}		

		// model for database field STATE(int)
		public int STATE
		{
			get
			{
				return base.dtModel.Rows[0]["STATE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["STATE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["STATE"] = value;
			}
		}

		// model for database field CLAIMANT_PARTY(int)
		public int CLAIMANT_PARTY
		{
			get
			{
				return base.dtModel.Rows[0]["CLAIMANT_PARTY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["CLAIMANT_PARTY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CLAIMANT_PARTY"] = value;
			}
		}

//		// model for database field LINKED_TO_CLAIM(string)
//		public string LINKED_TO_CLAIM
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["LINKED_TO_CLAIM"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LINKED_TO_CLAIM"].ToString();
//			}
//			set
//			{
//				base.dtModel.Rows[0]["LINKED_TO_CLAIM"] = value;
//			}
//		}	

		// model for database field ADD_FAULT(string)
		public string ADD_FAULT
		{
			get
			{
				return base.dtModel.Rows[0]["ADD_FAULT"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["ADD_FAULT"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["ADD_FAULT"] = value;
			}
		}	

		// model for database field TOTAL_LOSS(string)
		public string TOTAL_LOSS
		{
			get
			{
				return base.dtModel.Rows[0]["TOTAL_LOSS"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["TOTAL_LOSS"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["TOTAL_LOSS"] = value;
			}
		}	

		// model for database field NOTIFY_REINSURER(string)
        public int NOTIFY_REINSURER
		{
			get
			{
                return base.dtModel.Rows[0]["NOTIFY_REINSURER"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["NOTIFY_REINSURER"].ToString());
			}
			set
			{
                base.dtModel.Rows[0]["NOTIFY_REINSURER"] = value;
			}
		}
		// model for database field LOB_ID(string)
		public string LOB_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOB_ID"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOB_ID"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOB_ID"] = value;
			}
		}	

		// model for database field REPORTED_TO(string)
		public string REPORTED_TO
		{
			get
			{
				return base.dtModel.Rows[0]["REPORTED_TO"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REPORTED_TO"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REPORTED_TO"] = value;
			}
		}	

		// model for database field FIRST_NOTICE_OF_LOSS(DateTime)
		public DateTime FIRST_NOTICE_OF_LOSS
		{
			get
			{
				return base.dtModel.Rows[0]["FIRST_NOTICE_OF_LOSS"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["FIRST_NOTICE_OF_LOSS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["FIRST_NOTICE_OF_LOSS"] = value;
			}
		}

        // model for database field LAST_DOC_RECEIVE_DATE(DateTime)
        public DateTime LAST_DOC_RECEIVE_DATE
		{
			get
			{
                return base.dtModel.Rows[0]["LAST_DOC_RECEIVE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["LAST_DOC_RECEIVE_DATE"].ToString());
			}
			set
			{
                base.dtModel.Rows[0]["LAST_DOC_RECEIVE_DATE"] = value;
			}
		}
		
		// model for database field LINKED_CLAIM_ID_LIST(string)
		public string LINKED_CLAIM_ID_LIST
		{
			get
			{
				return base.dtModel.Rows[0]["LINKED_CLAIM_ID_LIST"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LINKED_CLAIM_ID_LIST"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LINKED_CLAIM_ID_LIST"] = value;
			}
		}	
		// model for database field RECIEVE_PINK_SLIP_USERS_LIST(string)
		public string RECIEVE_PINK_SLIP_USERS_LIST
		{
			get
			{
				return base.dtModel.Rows[0]["RECIEVE_PINK_SLIP_USERS_LIST"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["RECIEVE_PINK_SLIP_USERS_LIST"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["RECIEVE_PINK_SLIP_USERS_LIST"] = value;
			}
		}		
		// model for database field PINK_SLIP_TYPE_LIST(string)
		public string PINK_SLIP_TYPE_LIST
		{
			get
			{
				return base.dtModel.Rows[0]["PINK_SLIP_TYPE_LIST"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PINK_SLIP_TYPE_LIST"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PINK_SLIP_TYPE_LIST"] = value;
			}
		}		
		// model for database field NEW_RECIEVE_PINK_SLIP_USERS_LIST(string)
		public string NEW_RECIEVE_PINK_SLIP_USERS_LIST
		{
			get
			{
				return base.dtModel.Rows[0]["NEW_RECIEVE_PINK_SLIP_USERS_LIST"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NEW_RECIEVE_PINK_SLIP_USERS_LIST"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NEW_RECIEVE_PINK_SLIP_USERS_LIST"] = value;
			}
		}	
		//Done for Itrack Issue 6620 on 27 Nov 09
		// model for database field AT_FAULT_INDICATOR(int)
		public int AT_FAULT_INDICATOR
		{
			get
			{
				return base.dtModel.Rows[0]["AT_FAULT_INDICATOR"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["AT_FAULT_INDICATOR"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AT_FAULT_INDICATOR"] = value;
			}
		}


        public string OFFCIAL_CLAIM_NUMBER
		{
			get
			{
                return base.dtModel.Rows[0]["OFFCIAL_CLAIM_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["OFFCIAL_CLAIM_NUMBER"].ToString();
			}
			set
			{
                base.dtModel.Rows[0]["OFFCIAL_CLAIM_NUMBER"] = value;
			}
		}

        public string REIN_CLAIM_NUMBER
        {
            get
            {
                return base.dtModel.Rows[0]["REIN_CLAIM_NUMBER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REIN_CLAIM_NUMBER"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["REIN_CLAIM_NUMBER"] = value;
            }
        }

        public string REIN_LOSS_NOTICE_NUM
        {
            get
            {
                return base.dtModel.Rows[0]["REIN_LOSS_NOTICE_NUM"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REIN_LOSS_NOTICE_NUM"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["REIN_LOSS_NOTICE_NUM"] = value;
            }
        }

        public int REINSURANCE_TYPE
        {
            get
            {
                return base.dtModel.Rows[0]["REINSURANCE_TYPE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["REINSURANCE_TYPE"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["REINSURANCE_TYPE"] = value;
            }
        }

         public int IS_VICTIM_CLAIM
        {
            get
            {
                return base.dtModel.Rows[0]["IS_VICTIM_CLAIM"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["IS_VICTIM_CLAIM"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["IS_VICTIM_CLAIM"] = value;
            }
        }

         // model for database field POSSIBLE_PAYMENT_DATE(DateTime)
         public DateTime POSSIBLE_PAYMENT_DATE
         {
             get
             {
                 return base.dtModel.Rows[0]["POSSIBLE_PAYMENT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(base.dtModel.Rows[0]["POSSIBLE_PAYMENT_DATE"].ToString());
             }
             set
             {
                 base.dtModel.Rows[0]["POSSIBLE_PAYMENT_DATE"] = value;
             }
         }
		


		#endregion
	}
}
