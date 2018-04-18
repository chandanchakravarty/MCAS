/******************************************************************************************
<Author				: -   Vijay Joshi
<Start Date				: -	6/7/2005 1:21:30 PM
<End Date				: -	
<Description				: - 	Model class for MNT_INSTALLMENT_PLAN
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
namespace Cms.Model.Maintenance.Accounting
{
	/// <summary>
	/// Database Model for ACT_INSTALL_PLAN_DETAIL.
	/// </summary>
	public class ClsInstallmentPlanInfo : Cms.Model.ClsCommonModel
	{
		private const string ACT_INSTALL_PLAN_DETAIL = "ACT_INSTALL_PLAN_DETAIL";
		public ClsInstallmentPlanInfo()
		{
			base.dtModel.TableName = "ACT_INSTALL_PLAN_DETAIL";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table ACT_INSTALL_PLAN_DETAIL
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		private void AddColumns()
		{
			base.dtModel.Columns.Add("IDEN_PLAN_ID",typeof(int));
			base.dtModel.Columns.Add("PLAN_CODE",typeof(string));
			base.dtModel.Columns.Add("PLAN_DESCRIPTION",typeof(string));
			base.dtModel.Columns.Add("PLAN_TYPE",typeof(string));
			base.dtModel.Columns.Add("NO_OF_PAYMENTS",typeof(int));
			base.dtModel.Columns.Add("MONTHS_BETWEEN",typeof(int));

			base.dtModel.Columns.Add("PERCENT_BREAKDOWN1",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWN2",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWN3",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWN4",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWN5",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWN6",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWN7",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWN8",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWN9",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWN10",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWN11",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWN12",typeof(double));

			base.dtModel.Columns.Add("PERCENT_BREAKDOWNRP1",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWNRP2",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWNRP3",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWNRP4",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWNRP5",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWNRP6",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWNRP7",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWNRP8",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWNRP9",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWNRP10",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWNRP11",typeof(double));
			base.dtModel.Columns.Add("PERCENT_BREAKDOWNRP12",typeof(double));

			base.dtModel.Columns.Add("INSTALLMENT_FEES",typeof(double));
			base.dtModel.Columns.Add("LATE_FEES",typeof(double));

			base.dtModel.Columns.Add("NON_SUFFICIENT_FUND_FEES",typeof(double));
			base.dtModel.Columns.Add("REINSTATEMENT_FEES",typeof(double));
			base.dtModel.Columns.Add("SERVICE_CHARGE",typeof(double));
			base.dtModel.Columns.Add("CONVENIENCE_FEES",typeof(double));
            base.dtModel.Columns.Add("GRACE_PERIOD",typeof(int));

			base.dtModel.Columns.Add("MIN_INSTALLMENT_AMOUNT",typeof(double));
			base.dtModel.Columns.Add("MIN_INSTALL_PLAN",typeof(double));
			base.dtModel.Columns.Add("AMTUNDER_PAYMENT",typeof(double));
			base.dtModel.Columns.Add("MINDAYS_PREMIUM",typeof(int));
			base.dtModel.Columns.Add("MINDAYS_CANCEL",typeof(int));
			base.dtModel.Columns.Add("POST_PHONE",typeof(int));
			base.dtModel.Columns.Add("POST_CANCEL",typeof(int));
			base.dtModel.Columns.Add("DEFAULT_PLAN",typeof(bool));
			//added by pravesh on 30 nov 2006 
			base.dtModel.Columns.Add("APPLABLE_POLTERM",typeof(int));
			base.dtModel.Columns.Add("PLAN_PAYMENT_MODE",typeof(int));
			base.dtModel.Columns.Add("NO_INS_DOWNPAY",typeof(int));
			base.dtModel.Columns.Add("MODE_OF_DOWNPAY",typeof(int));
			base.dtModel.Columns.Add("NO_INS_DOWNPAY_RENEW",typeof(int));
			base.dtModel.Columns.Add("MODE_OF_DOWNPAY_RENEW",typeof(int));

			base.dtModel.Columns.Add("MODE_OF_DOWNPAY1",typeof(int));
			base.dtModel.Columns.Add("MODE_OF_DOWNPAY2",typeof(int));
			base.dtModel.Columns.Add("MODE_OF_DOWNPAY_RENEW1",typeof(int));
			base.dtModel.Columns.Add("MODE_OF_DOWNPAY_RENEW2",typeof(int));
			base.dtModel.Columns.Add("PAST_DUE_RENEW",typeof(double));
			base.dtModel.Columns.Add("PRO_STATMNT_BEFORE_DAYS",typeof(int));
			base.dtModel.Columns.Add("DAYS_DUE_PREM_NOTICE_PRINTD",typeof(int));

            base.dtModel.Columns.Add("INTREST_RATES", typeof(double));
            base.dtModel.Columns.Add("DAYS_SUBSEQUENT_INSTALLMENTS", typeof(int));//
            base.dtModel.Columns.Add("BASE_DATE_DOWNPAYMENT", typeof(int));
            base.dtModel.Columns.Add("DUE_DAYS_DOWNPYMT", typeof(int));
            base.dtModel.Columns.Add("BDATE_INSTALL_NXT_DOWNPYMT", typeof(int));
            base.dtModel.Columns.Add("DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT", typeof(int));
            base.dtModel.Columns.Add("DOWN_PAYMENT_PLAN", typeof(int));
            base.dtModel.Columns.Add("DOWN_PAYMENT_PLAN_RENEWAL", typeof(int));
            base.dtModel.Columns.Add("APP_LOB", typeof(int));
            base.dtModel.Columns.Add("SUBSEQUENT_INSTALLMENTS_OPTION", typeof(string));
            base.dtModel.Columns.Add("RECVE_NOTIFICATION_DOC", typeof(int));

		}
		#region Database schema details
		
		// model for database field IDEN_PLAN_ID(int)
		public bool DEFAULT_PLAN
		{
			get
			{
				return base.dtModel.Rows[0]["DEFAULT_PLAN"] == DBNull.Value ? false : Convert.ToBoolean(base.dtModel.Rows[0]["DEFAULT_PLAN"]);
			}
			set
			{
				base.dtModel.Rows[0]["DEFAULT_PLAN"] = value;
			}
		}

		// model for database field IDEN_PLAN_ID(int)
		public int IDEN_PLAN_ID
		{
			get
			{
				return base.dtModel.Rows[0]["IDEN_PLAN_ID"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["IDEN_PLAN_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["IDEN_PLAN_ID"] = value;
			}
		}
		// model for database field PLAN_CODE(string)
		public string PLAN_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["PLAN_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PLAN_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PLAN_CODE"] = value;
			}
		}
		// model for database field PLAN_DESCRIPTION(string)
		public string PLAN_DESCRIPTION
		{
			get
			{
				return base.dtModel.Rows[0]["PLAN_DESCRIPTION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PLAN_DESCRIPTION"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PLAN_DESCRIPTION"] = value;
			}
		}
		// model for database field PLAN_TYPE(string)
		public string PLAN_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["PLAN_TYPE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["PLAN_TYPE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["PLAN_TYPE"] = value;
			}
		}
		// model for database field NO_OF_PAYMENTS(int)
		public int NO_OF_PAYMENTS
		{
			get
			{
				return base.dtModel.Rows[0]["NO_OF_PAYMENTS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["NO_OF_PAYMENTS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NO_OF_PAYMENTS"] = value;
			}
		}
		// model for database field MONTHS_BETWEEN(int)
		public int MONTHS_BETWEEN
		{
			get
			{
				return base.dtModel.Rows[0]["MONTHS_BETWEEN"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["MONTHS_BETWEEN"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MONTHS_BETWEEN"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWN1(double)
		public double PERCENT_BREAKDOWN1
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWN1"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWN1"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWN1"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWN2(double)
		public double PERCENT_BREAKDOWN2
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWN2"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWN2"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWN2"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWN3(double)
		public double PERCENT_BREAKDOWN3
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWN3"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWN3"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWN3"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWN4(double)
		public double PERCENT_BREAKDOWN4
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWN4"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWN4"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWN4"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWN5(double)
		public double PERCENT_BREAKDOWN5
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWN5"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWN5"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWN5"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWN6(double)
		public double PERCENT_BREAKDOWN6
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWN6"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWN6"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWN6"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWN7(double)
		public double PERCENT_BREAKDOWN7
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWN7"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWN7"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWN7"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWN8(double)
		public double PERCENT_BREAKDOWN8
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWN8"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWN8"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWN8"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWN9(double)
		public double PERCENT_BREAKDOWN9
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWN9"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWN9"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWN9"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWN10(double)
		public double PERCENT_BREAKDOWN10
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWN10"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWN10"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWN10"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWN11(double)
		public double PERCENT_BREAKDOWN11
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWN11"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWN11"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWN11"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWN12(double)
		public double PERCENT_BREAKDOWN12
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWN12"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWN12"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWN12"] = value;
			}
		}
		// model for database field INSTALLMENT_FEES(double)
		public double INSTALLMENT_FEES
		{
			get
			{
				return base.dtModel.Rows[0]["INSTALLMENT_FEES"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["INSTALLMENT_FEES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["INSTALLMENT_FEES"] = value;
			}
		}
		
			// model for database field LATE_FEES(double)
			public double LATE_FEES
			{
				get
				{
					return base.dtModel.Rows[0]["LATE_FEES"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["LATE_FEES"].ToString());
				}
				set
				{
					base.dtModel.Rows[0]["LATE_FEES"] = value;
				}
			}
		// model for database field NON_SUFFICIENT_FUND_FEES(double)
		public double NON_SUFFICIENT_FUND_FEES
		{
			get
			{
				return base.dtModel.Rows[0]["NON_SUFFICIENT_FUND_FEES"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["NON_SUFFICIENT_FUND_FEES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NON_SUFFICIENT_FUND_FEES"] = value;
			}
		}
		// model for database field SERVICE_CHARGE(double)
		public double SERVICE_CHARGE
		{
			get
			{
				return base.dtModel.Rows[0]["SERVICE_CHARGE"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["SERVICE_CHARGE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SERVICE_CHARGE"] = value;
			}
		}
		// model for database field CONVENIENCE_FEES(double)
		public double CONVENIENCE_FEES
		{
			get
			{
				return base.dtModel.Rows[0]["CONVENIENCE_FEES"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["CONVENIENCE_FEES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CONVENIENCE_FEES"] = value;
			}
		}
		// model for database field CONVENIENCE_FEES(double)
		public double GRACE_PERIOD
		{
			get
			{
				return base.dtModel.Rows[0]["GRACE_PERIOD"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["GRACE_PERIOD"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["GRACE_PERIOD"] = value;
			}
		}

		// model for database field REINSTATEMENT_FEES(double)
		public double REINSTATEMENT_FEES
		{
			get
			{
				return base.dtModel.Rows[0]["REINSTATEMENT_FEES"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["REINSTATEMENT_FEES"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REINSTATEMENT_FEES"] = value;
			}
		}
		// model for database field MIN_INSTALLMENT_AMOUNT(double)
		public double MIN_INSTALLMENT_AMOUNT
		{
			get
			{
				return base.dtModel.Rows[0]["MIN_INSTALLMENT_AMOUNT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["MIN_INSTALLMENT_AMOUNT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MIN_INSTALLMENT_AMOUNT"] = value;
			}
		}
		// model for database field MIN_INSTALL_PLAN(double)
		public double MIN_INSTALL_PLAN
		{
			get
			{
				return base.dtModel.Rows[0]["MIN_INSTALL_PLAN"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["MIN_INSTALL_PLAN"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MIN_INSTALL_PLAN"] = value;
			}
		}
		// model for database field AMTUNDER_PAYMENT(double)
		public double AMTUNDER_PAYMENT
		{
			get
			{
				return base.dtModel.Rows[0]["AMTUNDER_PAYMENT"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["AMTUNDER_PAYMENT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["AMTUNDER_PAYMENT"] = value;
			}
		}
		// model for database field MINDAYS_PREMIUM(int)
		public int MINDAYS_PREMIUM
		{
			get
			{
				return base.dtModel.Rows[0]["MINDAYS_PREMIUM"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["MINDAYS_PREMIUM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MINDAYS_PREMIUM"] = value;
			}
		}
		// model for database field MINDAYS_CANCEL(int)
		public int MINDAYS_CANCEL
		{
			get
			{
				return base.dtModel.Rows[0]["MINDAYS_CANCEL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["MINDAYS_CANCEL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MINDAYS_CANCEL"] = value;
			}
		}
		// model for database field POST_PHONE(int)
		public int POST_PHONE
		{
			get
			{
				return base.dtModel.Rows[0]["POST_PHONE"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POST_PHONE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POST_PHONE"] = value;
			}
		}
		// model for database field POST_CANCEL(int)
		public int POST_CANCEL
		{
			get
			{
				return base.dtModel.Rows[0]["POST_CANCEL"] == DBNull.Value ? 0 : int.Parse(base.dtModel.Rows[0]["POST_CANCEL"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["POST_CANCEL"] = value;
			}
		}


		// model for database field PERCENT_BREAKDOWN1(double)
		public double PERCENT_BREAKDOWNRP1
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP1"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP1"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP1"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWNRP2(double)
		public double PERCENT_BREAKDOWNRP2
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP2"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP2"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP2"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWN3(double)
		public double PERCENT_BREAKDOWNRP3
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP3"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP3"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP3"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWNRP4(double)
		public double PERCENT_BREAKDOWNRP4
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP4"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP4"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP4"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWNRP5(double)
		public double PERCENT_BREAKDOWNRP5
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP5"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP5"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP5"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWNRP6(double)
		public double PERCENT_BREAKDOWNRP6
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP6"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP6"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP6"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWNRP7(double)
		public double PERCENT_BREAKDOWNRP7
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP7"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP7"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP7"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWNRP8(double)
		public double PERCENT_BREAKDOWNRP8
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP8"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP8"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP8"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWNRP9(double)
		public double PERCENT_BREAKDOWNRP9
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP9"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP9"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP9"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWNRP10(double)
		public double PERCENT_BREAKDOWNRP10
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP10"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP10"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP10"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWNRP11(double)
		public double PERCENT_BREAKDOWNRP11
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP11"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP11"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP11"] = value;
			}
		}
		// model for database field PERCENT_BREAKDOWNRP12(double)
		public double PERCENT_BREAKDOWNRP12
		{
			get
			{
				return base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP12"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP12"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PERCENT_BREAKDOWNRP12"] = value;
			}
		}
		// model for database field APPLABLE_POLTERM(int)
		public int APPLABLE_POLTERM
		{
			get
			{
				return base.dtModel.Rows[0]["APPLABLE_POLTERM"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APPLABLE_POLTERM"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APPLABLE_POLTERM"] = value;
			}
		}
		// model for database field APPLABLE_POLTERM(int)
		public int PLAN_PAYMENT_MODE
		{
			get
			{
				return base.dtModel.Rows[0]["PLAN_PAYMENT_MODE"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["PLAN_PAYMENT_MODE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PLAN_PAYMENT_MODE"] = value;
			}
		}
		// model for database field NO_INS_DOWNPAY(int)
		public int NO_INS_DOWNPAY
		{
			get
			{
				return base.dtModel.Rows[0]["NO_INS_DOWNPAY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["NO_INS_DOWNPAY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NO_INS_DOWNPAY"] = value;
			}
		}
		// model for database field NO_INS_DOWNPAY(int)
		public int MODE_OF_DOWNPAY
		{
			get
			{
				return base.dtModel.Rows[0]["MODE_OF_DOWNPAY"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["MODE_OF_DOWNPAY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MODE_OF_DOWNPAY"] = value;
			}
		}
		// model for database field NO_INS_DOWNPAY_RENEW(int)
		public int NO_INS_DOWNPAY_RENEW
		{
			get
			{
				return base.dtModel.Rows[0]["NO_INS_DOWNPAY_RENEW"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["NO_INS_DOWNPAY_RENEW"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NO_INS_DOWNPAY_RENEW"] = value;
			}
		}
		// model for database field MODE_OF_DOWNPAY_RENEW(int)
		public int MODE_OF_DOWNPAY_RENEW
		{
			get
			{
				return base.dtModel.Rows[0]["MODE_OF_DOWNPAY_RENEW"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["MODE_OF_DOWNPAY_RENEW"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MODE_OF_DOWNPAY_RENEW"] = value;
			}
		}
		// model for database field MODE_OF_DOWNPAY_RENEW1(int)
		public int MODE_OF_DOWNPAY_RENEW1
		{
			get
			{
				return base.dtModel.Rows[0]["MODE_OF_DOWNPAY_RENEW1"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["MODE_OF_DOWNPAY_RENEW1"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MODE_OF_DOWNPAY_RENEW1"] = value;
			}
		}
		// model for database field MODE_OF_DOWNPAY_RENEW2(int)
		public int MODE_OF_DOWNPAY_RENEW2
		{
			get
			{
				return base.dtModel.Rows[0]["MODE_OF_DOWNPAY_RENEW2"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["MODE_OF_DOWNPAY_RENEW2"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MODE_OF_DOWNPAY_RENEW2"] = value;
			}
		}
		// model for database field MODE_OF_DOWNPAY1(int)
		public int MODE_OF_DOWNPAY1
		{
			get
			{
				return base.dtModel.Rows[0]["MODE_OF_DOWNPAY1"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["MODE_OF_DOWNPAY1"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MODE_OF_DOWNPAY1"] = value;
			}
		}
		// model for database field MODE_OF_DOWNPAY2(int)
		public int MODE_OF_DOWNPAY2
		{
			get
			{
				return base.dtModel.Rows[0]["MODE_OF_DOWNPAY2"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["MODE_OF_DOWNPAY2"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MODE_OF_DOWNPAY2"] = value;
			}
		}

		// model for database field PAST_DUE_RENEW(double)
		public double PAST_DUE_RENEW
		{
			get
			{
				return base.dtModel.Rows[0]["PAST_DUE_RENEW"] == DBNull.Value ? Convert.ToInt32(null) : double.Parse(base.dtModel.Rows[0]["PAST_DUE_RENEW"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PAST_DUE_RENEW"] = value;
			}
		}

		// model for database field PRO_STATMNT_BEFORE_DAYS(double)
		public double PRO_STATMNT_BEFORE_DAYS
		{
			get
			{
				return base.dtModel.Rows[0]["PRO_STATMNT_BEFORE_DAYS"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["PRO_STATMNT_BEFORE_DAYS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PRO_STATMNT_BEFORE_DAYS"] = value;
			}
		}

		// model for database field DAYS_DUE_PREM_NOTICE_PRINTD(double)
		public double DAYS_DUE_PREM_NOTICE_PRINTD
		{
			get
			{
				return base.dtModel.Rows[0]["DAYS_DUE_PREM_NOTICE_PRINTD"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["DAYS_DUE_PREM_NOTICE_PRINTD"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DAYS_DUE_PREM_NOTICE_PRINTD"] = value;
			}
		}
        public int APPLICABLE_LOB
        {
            get
            {
                return base.dtModel.Rows[0]["APP_LOB"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["APP_LOB"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["APP_LOB"] = value;
            }
        }

        public int DOWN_PAYMENT_PLAN_RENEWAL
        {
            get
            {
                return base.dtModel.Rows[0]["DOWN_PAYMENT_PLAN_RENEWAL"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DOWN_PAYMENT_PLAN_RENEWAL"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DOWN_PAYMENT_PLAN_RENEWAL"] = value;
            }
        }

        public int DOWN_PAYMENT_PLAN
        {
            get
            {
                return base.dtModel.Rows[0]["DOWN_PAYMENT_PLAN"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DOWN_PAYMENT_PLAN"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DOWN_PAYMENT_PLAN"] = value;
            }
        }

        public int DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT
        {
            get
            {
                return base.dtModel.Rows[0]["DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT"] = value;
            }
        }
        public int DUE_DAYS_DOWNPYMT
        {
            get
            {
                return base.dtModel.Rows[0]["DUE_DAYS_DOWNPYMT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DUE_DAYS_DOWNPYMT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DUE_DAYS_DOWNPYMT"] = value;
            }
        }
        public double INTREST_RATES
        {
            get
            {
                return base.dtModel.Rows[0]["INTREST_RATES"] == DBNull.Value ? 0 : double.Parse(base.dtModel.Rows[0]["INTREST_RATES"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["INTREST_RATES"] = value;
            }
        }
        public int DAYS_SUBSEQUENT_INSTALLMENTS
        {
            get
            {
                return base.dtModel.Rows[0]["DAYS_SUBSEQUENT_INSTALLMENTS"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["DAYS_SUBSEQUENT_INSTALLMENTS"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["DAYS_SUBSEQUENT_INSTALLMENTS"] = value;
            }
        }
        public int BASE_DATE_DOWNPAYMENT
        {
            get
            {
                return base.dtModel.Rows[0]["BASE_DATE_DOWNPAYMENT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["BASE_DATE_DOWNPAYMENT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BASE_DATE_DOWNPAYMENT"] = value;
            }
        }

        public int BDATE_INSTALL_NXT_DOWNPYMT
        {
            get
            {
                return base.dtModel.Rows[0]["BDATE_INSTALL_NXT_DOWNPYMT"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["BDATE_INSTALL_NXT_DOWNPYMT"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["BDATE_INSTALL_NXT_DOWNPYMT"] = value;
            }
        }

        public string SUBSEQUENT_INSTALLMENTS_OPTION
        {
            get
            {
                return base.dtModel.Rows[0]["SUBSEQUENT_INSTALLMENTS_OPTION"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SUBSEQUENT_INSTALLMENTS_OPTION"].ToString();
            }
            set
            {
                base.dtModel.Rows[0]["SUBSEQUENT_INSTALLMENTS_OPTION"] = value;
            }
        }
        public int RECVE_NOTIFICATION_DOC
        {
            get
            {
                return base.dtModel.Rows[0]["RECVE_NOTIFICATION_DOC"] == DBNull.Value ? Convert.ToInt32(null) : int.Parse(base.dtModel.Rows[0]["RECVE_NOTIFICATION_DOC"].ToString());
            }
            set
            {
                base.dtModel.Rows[0]["RECVE_NOTIFICATION_DOC"] = value;
            }
        }
        
		

		#endregion

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        //public void ActivateDeactivateDefaultValues(global::Cms.BusinessLayer.BlCommon.Accounting.ClsInstallmentPlan objInstallment, string p)
        //{
        //    throw new NotImplementedException();
        //}

        //public void ActivateDeactivateDefaultValues(global::Cms.BusinessLayer.BlCommon.Accounting.ClsInstallmentPlan objInstallment, string p)
        //{
        //    throw new NotImplementedException();
        //}

        //public void ActivateDeactivateDefaultValues(global::Cms.BusinessLayer.BlCommon.Accounting.ClsInstallmentPlan objInstallment, string p)
        //{
        //    throw new NotImplementedException();
        //}

        //public void ActivateDeactivateDefaultValues(global::Cms.BusinessLayer.BlCommon.Accounting.ClsInstallmentPlan objInstallment, string p)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
