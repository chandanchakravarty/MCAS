/******************************************************************************************
<Author				    : -   Charles Gomes
<Start Date				: -	  23-Mar-2010
<End Date				: -	
<Description			: -   Multilingual Description Support
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: -   9-Apr-2010
<Modified By			: - 
<Purpose				: - 
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
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.WebControls;
using Cms.Model.Maintenance;
using Cms.CmsWeb.Controls;
using System.Resources;
using System.Reflection;

namespace CmsWeb.Aspx
{
    public partial class MultilingualSupport : Cms.CmsWeb.cmsbase
    {
        ResourceManager objResourceManager = null;     

        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "";

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnClose.CmsButtonClass = CmsButtonType.Write;
            btnClose.PermissionString = gstrSecurityXML;

            if (!IsPostBack)
            {
                btnClose.Attributes.Add("onclick", "javascript:disableAllRfvValidators();");
                getQueryStringParams();
                
                objResourceManager = new ResourceManager("CmsWeb.Aspx.MultilingualSupport", Assembly.GetExecutingAssembly());

                if (hidPRIMARY_ID.Value == "0")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ALERT_MSG", "<script>alert('" + objResourceManager.GetString("lblALERT_MSG") + "');window.close();</script>");
                    return;
                }
                
                lblPageHeader.Text = objResourceManager.GetString("lblPageHeader");
                hidPageTitle.Value = objResourceManager.GetString("PageTitle");
                DataTable dt = Cms.BusinessLayer.BlApplication.ClsGeneralInformation.GetLangCultInfo();
                if (dt != null && dt.Rows.Count > 0)
                    Addcontrols(dt);                
            }

            //if(Session["Multi_Lang_Table"]!=null)
            //    Multi_Lang_Panel.Controls.Add((Table)Session["Multi_Lang_Table"]);

            if (!IsPostBack)
            {
                try
                {
                    DataTable dtMasterChild = Cms.BusinessLayer.BlApplication.ClsGeneralInformation.GetMasterChildInfo(hidPRIMARY_COLUMN.Value, int.Parse(hidPRIMARY_ID.Value),
                        hidMASTER_TABLE_NAME.Value, hidCHILD_TABLE_NAME.Value, hidDESCRIPTION_COLUMN.Value);

                    if (dtMasterChild != null && dtMasterChild.Rows.Count > 0)
                        fillcontrols(dtMasterChild);
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "REFRESH", "<script> window.opener.location.href = window.opener.location.href ; window.close();</script>");
                }
            }
        }

        private void fillcontrols(DataTable dtMasterChild)
        {
            if (int.Parse(hidLANG_COUNT.Value == ""?"0":hidLANG_COUNT.Value) == 0)
                return;
            //int count = int.Parse(hidLANG_COUNT.Value);
            //for (int i = 0; i < count; i++)
            // {
                gvCulLang.DataSource = dtMasterChild;
                gvCulLang.DataBind();
                //TextBox txtCULTURE_LANG_DESC = (TextBox)Page.FindControl("txtCULTURE_LANG_DESC");//("txt" + Convert.ToString(i + 1));
                //if (txtCULTURE_LANG_DESC != null && i < dtMasterChild.Rows.Count)
                //    txtCULTURE_LANG_DESC.Text = dtMasterChild.Rows[i][hidDESCRIPTION_COLUMN.Value.ToUpper().Trim()].ToString();
           // }
        }

        private void getQueryStringParams()
        {
            if (Request.QueryString["PRIMARY_COLUMN"] != null && Request.QueryString["PRIMARY_COLUMN"].ToString().Trim() != "")
			{ 
                hidPRIMARY_COLUMN.Value = Request.QueryString["PRIMARY_COLUMN"].ToString().Trim();
            }
            if (Request.QueryString["PRIMARY_ID"] != null && Request.QueryString["PRIMARY_ID"].ToString().Trim() != "")
			{
                hidPRIMARY_ID.Value = Request.QueryString["PRIMARY_ID"].ToString().Trim();
            }
            if (Request.QueryString["MASTER_TABLE_NAME"] != null && Request.QueryString["MASTER_TABLE_NAME"].ToString().Trim() != "")
			{
                hidMASTER_TABLE_NAME.Value = Request.QueryString["MASTER_TABLE_NAME"].ToString().Trim();
            }
            if (Request.QueryString["CHILD_TABLE_NAME"] != null && Request.QueryString["CHILD_TABLE_NAME"].ToString().Trim() != "")
			{
                hidCHILD_TABLE_NAME.Value = Request.QueryString["CHILD_TABLE_NAME"].ToString().Trim();
            }
            if (Request.QueryString["DESCRIPTION_COLUMN"] != null && Request.QueryString["DESCRIPTION_COLUMN"].ToString().Trim() != "")
            {
                hidDESCRIPTION_COLUMN.Value = Request.QueryString["DESCRIPTION_COLUMN"].ToString().Trim();
            }       
        }

        private void Addcontrols(DataTable dt)
        {
           // gvCulLang.DataSource = dt;
           // gvCulLang.DataBind();
            //Table Multi_Lang_Table = new Table();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
            //    TableRow tr = new TableRow();
            //    TableCell tc =new TableCell();
            //    tc.CssClass = "midcolora";

            //    Label lbl = new Label();
            //    lbl.ID = "lbl" + dt.Rows[i]["LANG_ID"].ToString();
            //    lbl.Text = dt.Rows[i]["LANG_NAME"].ToString();
            //    lbl.Text = lbl.Text ;
            //    tc.Controls.Add(lbl);

            //    Label spn = new Label();
            //    spn.Text = "*" + "<br />";
            //    spn.CssClass = "mandatory";
            //    tc.Controls.Add(spn);
           
            //    TextBox txt = new TextBox();
            //    txt.ID = "txt" + dt.Rows[i]["LANG_ID"].ToString();
            //    txt.Attributes.Add("size", "50");
            //    txt.MaxLength = 50;
            //    tc.Controls.Add(txt);  

            //    RequiredFieldValidator rfv = new RequiredFieldValidator();
            //    rfv.ID = "rfv" + dt.Rows[i]["LANG_ID"].ToString();
            //    rfv.ControlToValidate = txt.ID;
            //    rfv.ErrorMessage = "<br />" + objResourceManager.GetString("lblRFV"); ;
            //    tc.Controls.Add(rfv);

                hidLANG_COUNT.Value = Convert.ToString(int.Parse(hidLANG_COUNT.Value ==""?"0":hidLANG_COUNT.Value) + 1);

            //    tr.Cells.Add(tc);

            //    Multi_Lang_Table.Rows.Add(tr);
            }
           // Session["Multi_Lang_Table"] = Multi_Lang_Table;            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(hidLANG_COUNT.Value == ""?"0":hidLANG_COUNT.Value) == 0)
                    return;

                int intRetVal = 0;

                int count = int.Parse(hidLANG_COUNT.Value);
                //for (int i = 0; i < count; i++)
                //{
              
                    foreach (GridViewRow rw in gvCulLang.Rows)
                    {
                        TextBox t = (TextBox)rw.FindControl("txtCULTURE_LANG_DESC");

                        //TextBox t = (TextBox)Page.FindControl("txtCULTURE_LANG_DESC");//"txt" + Convert.ToString(i + 1));
                        if (t != null)
                        {
                            string strTABLE_NAME = "";
                            if ((rw.RowIndex + 1) == 1)
                                strTABLE_NAME = hidMASTER_TABLE_NAME.Value.ToUpper().Trim();
                            else
                                strTABLE_NAME = hidCHILD_TABLE_NAME.Value.ToUpper().Trim();

                            intRetVal = Cms.BusinessLayer.BlApplication.ClsGeneralInformation.SaveMasterChildInfo(hidPRIMARY_COLUMN.Value, int.Parse(hidPRIMARY_ID.Value), strTABLE_NAME, (rw.RowIndex + 1), hidDESCRIPTION_COLUMN.Value, t.Text.Trim());
                        }
                    }
                //}
                                
                lblMessage.Text = ClsMessages.FetchGeneralMessage("29");                     
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.FetchGeneralMessage("21") + " - " + ex.Message + " Try again!";
                ExceptionManager.Publish(ex);
            }
            finally
            {
                lblMessage.Visible = true;
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            //Session["Multi_Lang_Table"] = null;
            ClientScript.RegisterStartupScript(this.GetType(), "REFRESH", "<script> try {window.opener.refreshFromPopUp(); window.opener.location.href = window.opener.location.href ;} catch(err){window.opener.parent.location.href = window.opener.parent.location.href ;} window.close();</script>");
        }

        protected void gvCulLang_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Label capCULTURE_LANG = (Label)e.Row.FindControl("capCULTURE_LANG");
                capCULTURE_LANG.Text = objResourceManager.GetString("capCULTURE_LANG");

                Label capCULTURE_LANG_DESC = (Label)e.Row.FindControl("capCULTURE_LANG_DESC");
                capCULTURE_LANG_DESC.Text = objResourceManager.GetString("txtCULTURE_LANG_DESC");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RequiredFieldValidator rfvCULTURE_LANG_ENG = (RequiredFieldValidator)e.Row.FindControl("rfvCULTURE_LANG_DESC");
                TextBox txtCULTURE_LANG_ENG = (TextBox)e.Row.FindControl("txtCULTURE_LANG_DESC");                
                Label lblCULTURE_LANG = (Label)e.Row.FindControl("lblCULTURE_LANG");
                rfvCULTURE_LANG_ENG.ErrorMessage = "<br />" + objResourceManager.GetString("lblRFV");//Cms.CmsWeb.ClsMessages.FetchGeneralMessage("");
            }
        }
       
    }
}
