/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date			: -	  24-10-2005
<End Date			: -	  24-10-2005
<Description		: -   To add the motor cycle vin number.
<Review Date		: - 
<Reviewed By		: - 	
*******************************************************************************************/ 

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.CmsWeb;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon; 


namespace Cms.Application.Aspx
{
	/// <summary>
	/// Summary description for AddVinNo.
	/// </summary>
	public class AddVinNoMotorcycle : Cms.Application.appbase  
	{
		protected System.Web.UI.WebControls.Label capModel_Year;
		protected System.Web.UI.WebControls.TextBox txtModel_Year;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvModel_Year;
		protected System.Web.UI.WebControls.Label capManufacturer;
		protected System.Web.UI.WebControls.TextBox txtManufacturer;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvManufacturer;
		protected System.Web.UI.WebControls.Label capModel;
		protected System.Web.UI.WebControls.TextBox txtModel;
		protected System.Web.UI.WebControls.Label capModel_CC;
		protected System.Web.UI.WebControls.TextBox txtModel_CC;
		protected System.Web.UI.WebControls.Label lblID;
		protected System.Web.UI.WebControls.TextBox txtID;
		protected System.Web.UI.WebControls.Button btnSubmit;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvModel;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvID;
		protected System.Web.UI.WebControls.Label lblMessage;
		public string isRefresh="";
		protected System.Web.UI.WebControls.RangeValidator rngModel_Year;
		public string strCalledFrom ="";

		System.Resources.ResourceManager objResourceMgr;
		private void Page_Load(object sender, System.EventArgs e)
		{
            
			#region setting screen id
			{
					base.ScreenId	=	"55_55_55_55";
			}
			#endregion
			
			string colorScheme=GetColorScheme();
			
			objResourceMgr = new System.Resources.ResourceManager("Cms.Application.Aspx.AddVinNoMotorcycle" ,System.Reflection.Assembly.GetExecutingAssembly());
                
			if (!Page.IsPostBack)
			{				
				SetErrorMessages();
				SetCaptions();
				rngModel_Year.MaximumValue=DateTime.Now.Year.ToString();    
				rngModel_Year.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"411");
				rfvManufacturer.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"105");
				rfvModel.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"106");
				rfvID.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"163");
			}
		}

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvModel_Year.ErrorMessage 		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvManufacturer.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvModel.ErrorMessage           =   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvID.ErrorMessage              =   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
		}
		#endregion

		#region SetCaptions
		private void SetCaptions()
		{
			capModel_Year.Text		=		objResourceMgr.GetString("txtModel_Year");
			capManufacturer.Text	=		objResourceMgr.GetString("txtManufacturer");
			capModel.Text			=		objResourceMgr.GetString("txtModel");
			capModel_CC.Text		=		objResourceMgr.GetString("txtModel_CC");
			lblID.Text				=		objResourceMgr.GetString("txtID");
			
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web  Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			ClsVinMotorcycle objMotorCycleVIN=new ClsVinMotorcycle();
         
			try
			{
  
				int vehicleCC = 0;

				if (txtModel_CC.Text != "")
				{
					vehicleCC = Convert.ToInt32(txtModel_CC.Text); 
				}
				
				int result=objMotorCycleVIN.AddVINMotorCycleMaster(Convert.ToInt32(txtID.Text),txtManufacturer.Text,txtModel.Text,txtModel_Year.Text,vehicleCC);

				if(result==-1)
				{
					lblMessage.Visible=true;
					lblMessage.Text=Cms.CmsWeb.ClsMessages.GetMessage(this.ScreenId,"5");
				}
				if(result>=1)
				{
					lblMessage.Visible=true;
					isRefresh="1";
					lblMessage.Text=Cms.CmsWeb.ClsMessages.GetMessage(this.ScreenId,"6");
				}   
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"7") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
       
			}
			finally
			{
				if(objMotorCycleVIN!=null)
					objMotorCycleVIN.Dispose(); 
			}
		}
	}
}

