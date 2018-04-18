using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Cms.CmsWeb.aspx
{
    public partial class QuoteDetailTab : Cms.CmsWeb.cmsbase
    {
        protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.WebControls.Label lblTemplate;
        protected System.Web.UI.HtmlControls.HtmlForm indexForm;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;

        private string strCustomerId;
        private string strSaveMsg;
        private string strCalledFrom = "";

        private void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
            GetQueryString();
            if (strCustomerId != null && strCustomerId.Trim() != "")
            {
                ShowClientTopControl(int.Parse(strCustomerId));

                if (strSaveMsg != null && strSaveMsg.Trim() != "")
                {
                    TabCtl.TabURLs = "QuoteDetails.aspx?CalledFromMenu=Y";
                }
                else
                {
                    TabCtl.TabURLs = "QuoteDetails.aspx?CalledFromMenu=Y";
                }
            }
            else
            {
                cltClientTop.Visible = false;
                TabCtl.TabURLs = "QuoteDetails.aspx?CalledFromMenu=Y";

            }


        }
        private void GetQueryString()
        {

            strCustomerId = GetCustomerID();

        }

        private void ShowClientTopControl(int intCustomerId)
        {
            if (intCustomerId != 0)
            {
                cltClientTop.CustomerID = intCustomerId;
                cltClientTop.Visible = true;
                cltClientTop.ShowHeaderBand = "Client";
            }
            else
            {
                cltClientTop.Visible = false;
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
