using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataParser;
using System.Xml;
using System.Text;
using Ebix.DataTypes;

namespace Cms.CmsWeb.Maintenance
{
    public partial class RetentionLimitIndex : Cms.CmsWeb.cmsbase
    {
        string UserCulture = DataParser.BaseParser.CULTURE_EN;

        string SchemaName = "Maintenance/PageXML/SUSUPRetentionLimits.xml";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (AdvCommon.ClsCommon.CONNECTION_STRING == null || AdvCommon.ClsCommon.CONNECTION_STRING == "")
            {
                AdvCommon.ClsCommon.CONNECTION_STRING = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
            }

            if (!IsPostBack)
            {
                lnkCSS.Href = "/cms/cmsweb/css/css" + GetColorScheme() + ".css";

                MultiKeyDictionary<int, string, IBaseDataType> PrimaryKeys = new MultiKeyDictionary<int, string, IBaseDataType>();

                WebFormDataParser objParser = new WebFormDataParser(SchemaName, Cms.BusinessLayer.BlCommon.ClsCommon.CmsWebUrl + @"/");

                IDataReader objDataReader = new DataReader(SchemaName, Cms.BusinessLayer.BlCommon.ClsCommon.CmsWebUrl + @"/");

                advLister.CheckBoxColumnIndex = 0;
                advLister.RowClickEnabled = true;
                advLister.RowClickToolTip = "Click row to view details";
                advLister.DBConnectionString = AdvCommon.ClsCommon.CONNECTION_STRING;
                advLister.QueryXML = objDataReader.GetQueryXMLForLister();
                advLister.DataBind();

            }

            advLister.RowClicked += new Ebix.WebControls.RowClickEventHandler(advLister_RowClicked);
        }

        void advLister_RowClicked(object sender, Ebix.WebControls.GridViewRowClickedEventArgs e)
        {
            string LimitID = e.Row.Cells[0].Text;

            frmDetail.Attributes.Add("src", "AddRetentionLimit.aspx?M=E&LimitID=" + LimitID);

        }


        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            frmDetail.Attributes.Add("src", "AddRetentionLimit.aspx?M=N");

            // Response.Redirect("DetailPage.aspx");       
        }

    }
}