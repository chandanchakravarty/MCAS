/******************************************************************************************
<Author				: -   
<Start Date			: -	
<End Date			: -	
<Description		: -
<Review Date		: - 
<Reviewed By		: - 	
Modification History

<Modified Date			: -  30/08/2005	
<Modified By				: - Anurag Verma
<Purpose				: - chainging message id according to screen id
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


namespace Cms.Application.Aspx
{
	/// <summary>
	/// Summary description for AddVinNo.
	/// </summary>
	public class AddVinNo : Cms.Application.appbase  
	{
        protected System.Web.UI.WebControls.Label capVEHICLE_YEAR;
        protected System.Web.UI.WebControls.TextBox txtVEHICLE_YEAR;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_YEAR;
        protected System.Web.UI.WebControls.Label capMAKE;
        protected System.Web.UI.WebControls.TextBox txtMAKE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAKE;
        protected System.Web.UI.WebControls.Label capMODEL;
        protected System.Web.UI.WebControls.TextBox txtMODEL;
        protected System.Web.UI.WebControls.Label capBODY_TYPE;
        protected System.Web.UI.WebControls.TextBox txtBODY_TYPE;
        protected System.Web.UI.WebControls.Label lblVIN;
        protected System.Web.UI.WebControls.TextBox txtVIN;
        protected System.Web.UI.WebControls.Button btnSubmit;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvModel;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvVIN;
        protected System.Web.UI.WebControls.Label lblMessage;
        public string isRefresh="";
        protected System.Web.UI.WebControls.RangeValidator rngVEHICLE_YEAR;
		public string strCalledFrom ="";

        System.Resources.ResourceManager objResourceMgr;
        private void Page_Load(object sender, System.EventArgs e)
        {
            
			#region setting screen id
			if (Request.QueryString["CALLEDFROM"]!=null && Request.QueryString["CALLEDFROM"].ToString().Trim()!="")
			{
				strCalledFrom = Request.QueryString["CALLEDFROM"].ToString().Trim();	
				
			}
			switch(strCalledFrom)
			{
				case "ppa" :
				case "PPA" :
					base.ScreenId	=	"44_0_0_0";
					break;
				case "umb" :
				case "UMB" :
					base.ScreenId	=	"81_0_0_0";
					break;
				default :
					base.ScreenId	=	"44_0_0_0";
					break;
			}
			#endregion
			// Put user code to initialize the page here
            string colorScheme=GetColorScheme();

			
            objResourceMgr = new System.Resources.ResourceManager("Cms.Application.Aspx.AddVinNo" ,System.Reflection.Assembly.GetExecutingAssembly());
                
            if (!Page.IsPostBack)
            {				
                SetErrorMessages();
                SetCaptions();
                rngVEHICLE_YEAR.MaximumValue=DateTime.Now.Year.ToString();    
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
            rfvVEHICLE_YEAR.ErrorMessage 		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
            rfvMAKE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
            rfvModel.ErrorMessage               =   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
            rfvVIN.ErrorMessage               =   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
            rngVEHICLE_YEAR.ErrorMessage        =   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
        }
        #endregion

        #region SetCaptions
        private void SetCaptions()
        {
            capVEHICLE_YEAR.Text		=		objResourceMgr.GetString("txtVEHICLE_YEAR");
            capMAKE.Text				=		objResourceMgr.GetString("txtMAKE");
            capMODEL.Text				=		objResourceMgr.GetString("txtMODEL");
            capBODY_TYPE.Text		    =		objResourceMgr.GetString("txtBODY_TYPE");
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
            ClsVinMaster objVIN=new ClsVinMaster();
            string year=txtVEHICLE_YEAR.Text.Trim();
            string make=txtMAKE.Text.Trim() ;
            string model=txtMODEL.Text.Trim() ;
            string bodyType=txtBODY_TYPE.Text.Trim();
            string VIN=txtVIN.Text.Trim();
         
            try
            {
                
  
                int result=objVIN.AddVINMaster(year,make,model,bodyType,VIN); 

                if(result==-1)
                {
                    lblMessage.Visible=true;
                    lblMessage.Text=Cms.CmsWeb.ClsMessages.GetMessage(this.ScreenId,"6");
                }
                if(result>=1)
                {
                    lblMessage.Visible=true;
                    isRefresh="1";
                    lblMessage.Text=Cms.CmsWeb.ClsMessages.GetMessage(this.ScreenId,"7");
                }   
            }
            catch(Exception ex)
            {
                lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"8") + " - " + ex.Message + " Try again!";
                lblMessage.Visible	=	true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
       
            }
            finally
            {
                if(objVIN!=null)
                    objVIN.Dispose(); 
            }
        }
    }
}
