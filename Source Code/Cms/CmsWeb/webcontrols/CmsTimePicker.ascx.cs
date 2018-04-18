namespace Cms.CmsWeb.WebControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/******************************************************************************************
		<Author					: Anshuman Sharan- >
		<Start Date				: March 10, 2005-	>
		<End Date				: - >
		<Description			: - >
		<Review Date			: - >
		<Reviewed By			: - >
	
		Modification History

		<Modified Date			: - >
		<Modified By			: - >
		<Purpose				: - >
	*******************************************************************************************/

	/// <summary>
	///		Summary description for TimePicker.
	/// </summary>
	public class CmsTimePicker : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DropDownList timePickerHour;
		protected System.Web.UI.WebControls.DropDownList timePickerMeridian;
		protected System.Web.UI.WebControls.DropDownList timePickerMinute;

		private int intSelectedHour = -1, intSelectedMinute = -1;
		private string strSelectedMeridian = "-1";
		public int test=100;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			int intLoopCount;
			// filling the Hour drop down list
			timePickerHour.Items.Add(new ListItem("Hour","-1"));
			for(intLoopCount = 1; intLoopCount < 13; intLoopCount++)
			{
				if(intLoopCount < 10)
				{
					timePickerHour.Items.Add(new ListItem("0" + intLoopCount.ToString(),intLoopCount.ToString()));
				}
				else
				{
					timePickerHour.Items.Add(new ListItem(intLoopCount.ToString(),intLoopCount.ToString()));
				}
			}
			timePickerHour.SelectedIndex = intSelectedHour;
			//filling  the Minute drop down list
			timePickerMinute.Items.Add(new ListItem("Minute","-1"));
			for(intLoopCount = 0; intLoopCount < 61; intLoopCount++)
			{
				if(intLoopCount < 10)
				{
					timePickerMinute.Items.Add(new ListItem("0" + intLoopCount.ToString(),intLoopCount.ToString()));
				}
				else
				{
					timePickerMinute.Items.Add(new ListItem(intLoopCount.ToString(),intLoopCount.ToString()));
				}
			}
			timePickerMinute.SelectedIndex = intSelectedMinute;
			//filling  the Meridian drop down list
			timePickerMeridian.Items.Add(new ListItem("AM / PM","-1"));
			timePickerMeridian.Items.Add(new ListItem("AM","AM"));
			timePickerMeridian.Items.Add(new ListItem("PM","PM"));

			if(strSelectedMeridian.ToUpper() == "AM")
				timePickerMeridian.SelectedIndex	=	1;
			if(strSelectedMeridian.ToUpper() == "PM")
				timePickerMeridian.SelectedIndex	=	2;
		}

		public int SelectedHour
		{
			get
			{
				return int.Parse(timePickerHour.SelectedItem.Value) == -1 ? 0 : int.Parse(timePickerHour.SelectedItem.Value);
			}
			set
			{
				intSelectedHour		=	value;
			}
		}
		
		public int SelectedMinute
		{
			get
			{
				return int.Parse(timePickerMinute.SelectedItem.Value) == -1 ? 0 : int.Parse(timePickerMinute.SelectedItem.Value);
			}
			set
			{
				intSelectedMinute	=	value;
			}
		}
		
		public string SelectedMeridian
		{
			get
			{
				return timePickerMeridian.SelectedItem.Value == "-1" ? "PM" : timePickerMeridian.SelectedItem.Value;
			}
			set
			{
				strSelectedMeridian	=	value;
			}
		}

		public string SelectedTime
		{
			get
			{
				return SelectedHour + ":" + SelectedMinute + " " + SelectedMeridian;
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
		
		/// <summary>
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
