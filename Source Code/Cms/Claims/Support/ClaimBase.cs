/******************************************************************************************
	<Author					: Sumit Chhabra
	<Start Date				: April 26,2006>
	<End Date				: 
	<Description			: Base class for Claims Module
	<Review Date			: 
	<Reviewed By			: 
	
	Modification History

	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >	                          
*******************************************************************************************/



using System;
using Cms;
using Cms.CmsWeb;
using System.Web.UI.WebControls;

namespace Cms.Claims
{
	/// <summary>
	/// Summary description for ClaimBase.
	/// </summary>
	public class ClaimBase:Cms.CmsWeb.cmsbase 
	{
		//To check that Calim Reserves are Added or Not.
		public string IS_RESERVE_ADDED = "0";
		//Lookup value of Claim status option - Closed 
		public static string CLAIM_STATUS_CLOSED = "11740";
		//Months to be added to diary date for review when the claim status is closed
		public static string DIARY_DATE_ADD_MONTHS = "6";
		//Constants defined for called from used at payment details and expense activity for Payee Index page
		public static string CALLED_FROM_EXPENSE = "EXPENSE";
		public static string CALLED_FROM_PAYMENT = "PAYMENT";
		//Watercraft Reserve Page Name
		public static string WATERCRAFT_RESERVE_PAGE = "ReserveWatercraftDetails.aspx";
		//Rental Reserve Page Name
		public static string RENTAL_RESERVE_PAGE = "ReserveRentalDetails.aspx";
		//Home Reserve Page Name
		public static string HOME_RESERVE_PAGE = "ReserveHomeDetails.aspx";
		//Motor Reserve Page Name
		public static string MOTOR_RESERVE_PAGE = "ReserveMotorDetails.aspx";
		//Umbrella Reserve Page Name
		public static string UMB_RESERVE_PAGE = "ReserveUmbDetails.aspx";
		//Watercraft Payments Page Name
		public static string WATERCRAFT_PAYMENT_PAGE = "PaymentWatercraftDetails.aspx";
		//Rental Payments Page Name
		public static string RENTAL_PAYMENT_PAGE = "PaymentRentalDetails.aspx";
		//Home Payments Page Name
		public static string HOME_PAYMENT_PAGE = "PaymentHomeDetails.aspx";
		//Motor Payments Page Name
		public static string MOTOR_PAYMENT_PAGE = "PaymentMotorDetails.aspx";
		//Umbrella Payments Page Name
		public static string UMB_PAYMENT_PAGE = "PaymentUmbDetails.aspx";
		//Watercraft Recovery Page Name
		public static string WATERCRAFT_RECOVERY_PAGE = "AddActivityWatercraftRecovery.aspx";
		//Rental Recovery Page Name
		public static string RENTAL_RECOVERY_PAGE = "AddActivityRentalRecovery.aspx";
		//Home Recovery Page Name
		public static string HOME_RECOVERY_PAGE = "AddActivityHomeRecovery.aspx";
		//Motor Recovery Page Name
		public static string MOTOR_RECOVERY_PAGE = "AddActivityMotorRecovery.aspx";
		//Umbrella Recovery Page Name
		public static string UMB_RECOVERY_PAGE = "AddActivityUmbRecovery.aspx";
		//Text value for MCCA Attachment Point column
		public static string MCCA_ATTACHMENT_TEXT = "MCCA Attachment Point";
		//Text value for MCCA Reserve column
		public static string MCCA_RESERVE_TEXT = "MCCA Reserve";

		//Watercraft Expense Page Name
		public static string WATERCRAFT_EXPENSE_PAGE = "AddActivityExpenseWatercraft.aspx";
		//Rental Expense Page Name
		public static string RENTAL_EXPENSE_PAGE = "AddActivityExpenseRental.aspx";
		//Home Expense  Page Name
		public static string HOME_EXPENSE_PAGE = "AddActivityExpenseHome.aspx";
		//Motor Expense Page Name
		public static string MOTOR_EXPENSE_PAGE = "AddActivityExpenseMotor.aspx";
		//Umbrella Expense Page Name
		public static string UMB_EXPENSE_PAGE = "AddActivityExpenseUmb.aspx";

		//Lookup Unique ids of Vehicle Owner being enumerated
		public enum enumVEHICLE_OWNER
		{
			NAMED_INSURED = 11752,//Text at lookup changed to Additional Insured
			INSURED = 11753,
			NOT_ON_POLICY = 11754,
			RATED_DRIVER = 14151
			
		}
		
		public ClaimBase()
		{
			
		}
		
		public enum enumTYPE_OF_OWNER
		{
			INSURED_VEHICLE = 1,
			PROPERTY_DAMAGED = 2,
			LIABILITY_TYPE_OWNER = 3,
			LIABILITY_TYPE_MANUFACTURER = 4	
		}
		public enum enumTYPE_OF_DRIVER
		{
			INSURED_VEHICLE = 1,
			PROPERTY_DAMAGED = 2			
		}
		public bool IsDate(string sdate) 
		{ 
			DateTime dt; 
			bool isDate = true; 
			try 
			{ 
				if(sdate=="") isDate = false;
				dt = DateTime.Parse(sdate); 
			} 
			catch 
			{ 
				isDate = false; 
			} 
			return isDate; 
		} 

		public void ClearClaimsSessionValues()
		{
			SetCustomerID("");
			SetPolicyID("");
			SetPolicyVersionID("");
			SetLOBID("");
		}

		public enum enumTYPE_OF_HOME
		{
			HOME_OWNER = 1,
			RECR_VEHICLE = 2,
			INLAND_MARINE =3,
			BOAT = 4
		}
		//Enumeration of Transaction Codes as defined in clm_type_detail table and lookup
		public enum enumTransactionLookup
		{
			RESERVE_UPDATE = 11773,
			EXPENSE_PAYMENT = 11774,
			CLAIM_PAYMENT = 11775,
			RECOVERY = 11776,
			REINSURANCE = 11777			

		}
		//Enumeration of Claim Actiivty Reasons
		public enum enumActivityReason
		{
			RESERVE_UPDATE = 11773,
			EXPENSE_PAYMENT = 11774,
			CLAIM_PAYMENT = 11775,
			RECOVERY = 11776,
			REINSURANCE = 11777,
			FIRST_NOTIFICATION = 11805,
			NEW_RESERVE = 11836

		}
		//Enumeration for Claim Activity Status
		public enum enumClaimActivityStatus
		{
			INCOMPLETE=11800,
			COMPLETE=11801,
			DEACTIVATE=11802,
			AWAITING_AUTHORIZATION=11803,
			AUTHORISED=11804,
			VOID=11986 //Added for Itrack Issue 7169 on 15 Oct 2010
		}
		//Enumeration for Claim Actions on Payment
		public enum enumClaimActionOnPayment
		{
			NEW_RESERVE = 165,
            CHANGE_RESERVE = 166,
			FIRST_NOTIFICATION = 192,
            PAYMENT_PARTIAL = 180,

		}
		
		//Coverage ID for Personal Injury Protection
		public const string PIP_COV_ID = "116";
		//Dummy Coverage IDs for Medical,Work Loss, Death Benefits and Survivor's Benefits
		public const int MEDICAL_COV_ID = 50001;
		public const int WORK_LOSS_COV_ID = 50002;
		public const int DEATH_BENEFITS_COV_ID = 50003;
		public const int SURVIVOR_COV_ID = 50004;

		public const string MEDICAL_COV_ID_1 = "50001";
		public const string WORK_LOSS_COV_ID_1 = "50002";
		public const string DEATH_BENEFITS_COV_ID_1 = "50003";
		public const string SURVIVOR_COV_ID_1 = "50004";

		//Coverage Codes for Single Limits Liability
		public const string SLL_COV_ID = "1";
		public const string RLCSL_COV_ID = "113";
		//Coverage Codes for Additional Physical Damage Coverage(M-14)
		public const string M14_IN_COV_ID = "1023";//14
		public const string M14_MI_COV_ID= "1024";//22
		//Coverage Codes for Helmet & Riding Apparel Coverage(M-15)
		public const string M15_IN_COV_ID = "203";//14
		public const string M15_MI_COV_ID = "219";//22		
		//Coverage Codes for Loan / Lease Gap Coverage (A-11)
		public const string LLGC_IN_COV_ID = "46";
		public const string LLGC_MI_COV_ID = "249";
	

		//Dummy Coverage IDs for BI and PD for SLL and RLCSL
		//Commented and changed by Shikha as codes were needed seperately for Michigan and Indiana 
		//to avoid any ambiguity.
		//public const string SLL_BI_ID = "50005";
		//public const string SLL_PD_ID = "50006";
		public const string SLL_BI_ID_MI = "50005";
		public const string SLL_PD_ID_MI = "50006";
		public const string SLL_BI_ID_IN = "50011";
		public const string SLL_PD_ID_IN = "50012";
		//Coverage IDs for Uninsured Motorists
		public const string UNINSURED_MOTORISTS_COV_ID_IN = "9";
		public const string UNINSURED_MOTORISTS_COV_ID_MI = "119";
		//Dummy Coverage IDs for Uninsured Motorists
		/*public const string UNINSURED_MOTORISTS_BI_ID = "50007";
		public const string UNINSURED_MOTORISTS_PD_ID = "50008";*/
		public const string UNINSURED_MOTORISTS_BI_ID_MI = "50007";
		public const string UNINSURED_MOTORISTS_PD_ID_MI = "50008";
		public const string UNINSURED_MOTORISTS_BI_ID_IN = "50013";
		public const string UNINSURED_MOTORISTS_PD_ID_IN = "50014";
		//Coverage Code for Underinsured Motorists
		public const string UNDERINSURED_MOTORISTS_COV_ID_IN = "14";
		public const string UNDERINSURED_MOTORISTS_COV_ID_MI = "304";
		//Dummy Coverage IDs for Underinsured Motorists
		/*public const string UNDERINSURED_MOTORISTS_BI_ID = "50009";
		public const string UNDERINSURED_MOTORISTS_PD_ID = "50010";*/
		public const string UNDERINSURED_MOTORISTS_BI_ID_MI = "50009";
		public const string UNDERINSURED_MOTORISTS_PD_ID_MI = "50010";
		public const string UNDERINSURED_MOTORISTS_BI_ID_IN = "50015";
		public const string UNDERINSURED_MOTORISTS_PD_ID_IN = "50016";
		//Added by ankit for (M-14),(M-15),(A-11) date:31 aug 2010
		public const string M_14_Collision_MI="50017";
		public const string M_14_Collision_IN="50018";
		public const string M_14_Other_Than_Collision_MI="50019";		
		public const string M_14_Other_Than_Collision_IN="50020";
		
		public const string M_15_Collision_IN="50021";
		public const string M_15_Collision_MI="50022";
		public const string M_15_Other_Than_Collision_IN ="50023";
		public const string M_15_Other_Than_Collision_MI="50024";

		public const string LLGC_MI_Collision="50025";
		public const string LLGC_MI_Other_Than_Collision="50026";
		public const string LLGC_IN_Collision="50027";
		public const string LLGC_IN_Other_Than_Collision="50028";

        public const int Brazil_Country_ID = 5;

		//Coverage IDs for Trailer
		//Done for Itrack Issue 6299 on 26 Aug 09
		public const int TrailerCovID_IN = 20001;
		public const int TrailerCovID_MI = 20002;
		public const int TrailerCovID_WI = 20003;//Done for Itrack Issue 6299 on 15 Sept 09

		public static void HideShowColumns(DataGrid dg, string ColumnHeaderText,  bool displayColumn)
		{
			//When the grid is null or when we want to show an already visible column, return
			if(dg == null || displayColumn==true || dg.Visible == false) 
			{
				return;
			}
			// Loop through all of the columns in the grid.
			foreach(DataGridColumn col in dg.Columns)
			{
				if(col.HeaderText == ColumnHeaderText)
				{
					col.Visible = displayColumn;
					return;
				}
			}
		}


		#region ScreenID
		//Overriding the screen id property of cmsbase class
		public new string ScreenId
		{
			get
			{
				return base.ScreenId;
			}
			set
			{
				base.ScreenId = value;
				//Here we will check whether Activity Status in the Session has been maintained 
				//If the Activity Session is not null then we will check the Activity Status should
				//not be complete if it completed then the corresponding screen is in view mode.				
				try
				{
					string strActivityStatus = GetActivityStatus();
					if (strActivityStatus.ToString().ToUpper().Trim() == "COMPLETE" || strActivityStatus.ToString().ToUpper().Trim() == "VOID")
					{
						//Changing the security xml to view mode only
						gstrSecurityXML = "<Security><Read>Y</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>";
						base.InitializeSecuritySettings(); 
					}
				}
				catch{}
			}
		}
		#endregion

	}
}
