using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataParser;
using System.Xml;
using System.Text;
using System.Data;
using System.Collections;

namespace Cms.CmsWeb.Maintenance
{
    public partial class AddRetentionLimit : Cms.CmsWeb.cmsbase
    {
        string UserCulture = DataParser.BaseParser.CULTURE_EN;
        string SchemaName = "Maintenance/PageXML/SUSUPRetentionLimits.xml";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lnkCSS.Href = "/cms/cmsweb/css/css" + GetColorScheme() + ".css";
            }
            lblMessage.Visible = false;
        }

        protected bool IsUpdateMode
        {
            get
            {
                if (ViewState["UpdateMode"] == null || ViewState["UpdateMode"].ToString() != "Y")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (ViewState["UpdateMode"] == null || ViewState["UpdateMode"].ToString() == "")
            {
                string Mode = Request.QueryString["M"].Trim();
                if (Mode == "E")
                {
                    ViewState["UpdateMode"] = "Y";
                }
                else
                {
                    ViewState["UpdateMode"] = "N";
                }
            }
            DataReader objDataReader = new DataReader(SchemaName, Cms.BusinessLayer.BlCommon.ClsCommon.CmsWebUrl + @"/");

            WebFormDataParser objParser = new WebFormDataParser(SchemaName, Cms.BusinessLayer.BlCommon.ClsCommon.CmsWebUrl + @"/");
            objParser.CultureName = UserCulture;


            Ebix.DataTypes.FormData objFormData = new Ebix.DataTypes.FormData();


            objFormData.PrimaryColumns = objParser.GetParentKeys(this.Context);

            if (IsUpdateMode)
            {
                objParser.AddCurrentKeys(objFormData.PrimaryColumns, Request);

                DataSet ds = objDataReader.GetOldData(objFormData.PrimaryColumns);

                objParser.CreateForm(objFormData, pnlDetails, false, ds);

                btnSave.Text = "Update";

            }
            else
            {
                objParser.CreateForm(objFormData, pnlDetails, true, null);
            }



            if (Session["FormData"] != null)
            {
                Session.Remove("FormData");
            }
            Session.Add("FormData", objFormData);

            //Register Client script
            if (!ClientScript.IsStartupScriptRegistered("RuleScript"))
            {
                string ScriptBlock = @"<script>" + objParser.GetClientScriptBlock() + "</script>";

                ClientScript.RegisterClientScriptBlock(this.GetType(), "RuleScript", ScriptBlock);

                string StartUpScript = @"<script>" + objParser.GetStartUpScriptBlock() + "</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "StartUpScript", StartUpScript);

            }

        }


        protected void btnSave_Click(object sender, EventArgs e)
        {

            Ebix.DataTypes.FormData objFormData = (Ebix.DataTypes.FormData)Session["FormData"];
            WebFormDataParser objParser = new WebFormDataParser(SchemaName, Cms.BusinessLayer.BlCommon.ClsCommon.CmsWebUrl + @"/");

            objParser.CultureName = UserCulture;

            objParser.PopulateFormDataObject(Request, objFormData);

            ClsDBBridge objDB = new ClsDBBridge(SchemaName, Cms.BusinessLayer.BlCommon.ClsCommon.CmsWebUrl + @"/");
            if (!IsUpdateMode)
            {
                objDB.AddRecord(objFormData);
                ViewState["UpdateMode"] = "Y";
                lblMessage.Text = "Information saved successfully";

            }
            else
            {
                objDB.UpdateRecord(objFormData);
                lblMessage.Text = "Information updated successfully";
            }


            lblMessage.Visible = true;
        }

    }
}