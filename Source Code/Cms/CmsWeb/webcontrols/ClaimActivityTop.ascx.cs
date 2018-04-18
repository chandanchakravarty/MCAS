namespace Cms.CmsWeb.WebControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Resources;
	using System.Reflection;
	using System.Xml;	
	using Cms.BusinessLayer;
    using System.Globalization;

	
	/// <summary>
	///		Summary description for ClaimActivityTop.
	/// </summary>
	public abstract class ClaimActivityTop : System.Web.UI.UserControl
	{
        protected ResourceManager objResourceMgr;
		protected int intClaimId,intActivityId;		
		protected string strClaimNumber="";
		protected System.Web.UI.WebControls.Label lblActivityNumber,lblSActivityNumber,lblActivityDate,lblSActivityDate,lblTotalPayment,lblSTotalPayment,lblSActivityTranDesc,lblActivityTranDesc;		
		protected System.Web.UI.WebControls.Panel pnlApplication;
		protected string colorScheme;
		public int ActivityID
		{
			get{return intActivityId;}
			set{intActivityId = value;}
		}

		public int ClaimID
		{
			get{return intClaimId;}
			set{intClaimId = value;}
		}

		public string PanelClientID
		{
			get{return pnlApplication.ClientID;}			
		}
		public string PanelPaymentClientID
		{
			get{return lblSTotalPayment.ClientID;}			
		}



        public NumberFormatInfo nfi;
		private void Page_Load(object sender, System.EventArgs e)
		{
            cmsbase Obj=new cmsbase();

            if (Obj.GetPolicyCurrency() != String.Empty && Obj.GetPolicyCurrency() == cmsbase.enumCurrencyId.BR)
                nfi = new CultureInfo(cmsbase.enumCulture.BR, true).NumberFormat;
            else
                nfi = new CultureInfo(cmsbase.enumCulture.US, true).NumberFormat;

            objResourceMgr = new System.Resources.ResourceManager("Cms.Cmsweb.WebControls.ClaimActivityTop", System.Reflection.Assembly.GetExecutingAssembly());
			ShowActivityDetails();
            SetCaption();
		}

        private void SetCaption()
        {
            //declaring object for resource manager	
            //(new cmsbase()).SetCultureThread((new cmsbase()).GetLanguageCode());//Added by santosh kumar gautam on 17 dec 2010 for Multilingual Implementation
           
            lblActivityNumber.Text = objResourceMgr.GetString("lblActivityNumber");
            lblActivityTranDesc.Text = objResourceMgr.GetString("lblActivityTranDesc");           
            lblActivityDate.Text = objResourceMgr.GetString("lblActivityDate");
            
           
        }
		private void ShowActivityDetails()
		{	
			if(intClaimId==0 || intActivityId==0)
			{
				pnlApplication.Visible = false;
			}
			else
			{
				pnlApplication.Visible = true;
				lblActivityNumber.Text = "Activity #";
				lblActivityDate.Text = "Activity Date";				
				lblActivityTranDesc.Text = "";
                cmsbase Obj=new cmsbase();
                int LangId = int.Parse(Obj.GetLanguageID());
                DataTable dtActivityData = Cms.BusinessLayer.BLClaims.ClsPaymentBreakDown.GetActivityData(intClaimId.ToString(), intActivityId.ToString(), LangId);
				if(dtActivityData!=null && dtActivityData.Rows.Count>0)
				{
					lblSActivityNumber.Text = intActivityId.ToString();
					string ActivityDate = dtActivityData.Rows[0]["ACTIVITY_DATE"].ToString();

                    if (!string.IsNullOrEmpty(ActivityDate))
                        lblSActivityDate.Text = cmsbase.ConvertDBDateToCulture(ActivityDate);

					if(dtActivityData.Rows[0]["PAYMENT_AMOUNT"]!=null && dtActivityData.Rows[0]["PAYMENT_AMOUNT"].ToString()!="" && dtActivityData.Rows[0]["PAYMENT_AMOUNT"].ToString()!="0")
                        lblSTotalPayment.Text = Convert.ToDouble(dtActivityData.Rows[0]["PAYMENT_AMOUNT"].ToString().Trim()).ToString("N", nfi); //String.Format("{0:,##,###,###.##}",dtActivityData.Rows[0]["PAYMENT_AMOUNT"]);
					else
						lblSTotalPayment.Text = "0";

					lblSActivityTranDesc.Text = dtActivityData.Rows[0]["ACTIVITY_DESC"].ToString();
					lblTotalPayment.Text = dtActivityData.Rows[0]["PAYMENT_DESC"].ToString();
                    //Added by santosh kumar gautam on 18 dec 2010
                    string DescType = dtActivityData.Rows[0]["PAYMENT_DESC"].ToString();

                    if( DescType=="TO") //TOTAL OUTSTANDING
                        lblTotalPayment.Text = objResourceMgr.GetString("TotalOutstanding");
                    else //TOTAL PAYMENT OR TOTAL RECOVERY
                        lblTotalPayment.Text = objResourceMgr.GetString("TotalPayment");
				}
				else
				{
					lblSActivityNumber.Text = "N.A.";
					lblSActivityDate.Text = "N.A.";
					lblSTotalPayment.Text = "N.A.";
					lblTotalPayment.Text = "Total";	
				}				
			}			
		}               
		

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
