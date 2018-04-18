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
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.CmsWeb;
using System.Reflection;
namespace Cms.CmsWeb.Maintenance
{
    public partial class RatesTab : Cms.CmsWeb.cmsbase    {    
   
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "560_0";
            //SetTabUrl();
            if (!Page.IsPostBack)
            {
                TabCtl.TabURLs = "InterestRateIndex.aspx"
                + ","
                + "AddFeeLimit.aspx"
                + ","
                + "IOFIndex.aspx";

                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2100");
            }

        }

        private void SetTabUrl()
        {            
            TabCtl.TabURLs = "InterestRateIndex.aspx" 
                + ","
                + "AddFeeLimit.aspx" 
                + ","
                + "IOFIndex.aspx";           

        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {            
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
        
    }
}