
#region using Declare Namespaces
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using WC = System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.CmsWeb;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.BusinessLayer.BlClient;
using System.Xml;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Client;
using Cms.CmsWeb.Utils;
using System.Linq;
#endregion

namespace Cms.client.Aspx
{
    public partial class AddNewCustomer : Cms.Client.clientbase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "0";
            btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;
            btnReset.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Execute;
            btnReset.PermissionString = gstrSecurityXML;
            btnAddNewApplication.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Read;
            btnAddNewApplication.PermissionString = gstrSecurityXML;
            FillDropdowns();
            setPageControls(Page, @"D:\Projects\EBIX-ADVANTAGE-Brazil\Source Code\Cms\CmsWeb\support\Resources\ALBA\AddCustomer.xml");
        }
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            // Saving the values into the database
            String strisSaved = "0";

        }
        private void FillDropdowns()
        {
            //DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
            DataTable dt;

            DataSet dsLookup = null;
            dsLookup = ClsCustomer.GetLookups();
            //Binding the customer type combo box
            dt = dsLookup.Tables[2].Select("", "LOOKUP_VALUE_DESC").CopyToDataTable<DataRow>();
            cmbCUSTOMER_TYPE.DataSource = dt;
            cmbCUSTOMER_TYPE.DataTextField = "LOOKUP_VALUE_DESC";
            cmbCUSTOMER_TYPE.DataValueField = "LOOKUP_UNIQUE_ID";
            cmbCUSTOMER_TYPE.DataBind();
            cmbCUSTOMER_TYPE.Items.Insert(0, "");
            cmbCUSTOMER_TYPE.SelectedIndex = -1;

        }
        private void setPageControls(System.Web.UI.Page CallerPage,string strPageResourceFile)
        {

            string PageLangCode = GetLanguageCode() == "" ? "en-US" : GetLanguageCode();
            XmlDocument PageResource = new XmlDocument();
            try
            {
                PageResource.Load(strPageResourceFile);
            }
            catch (Exception ex)
            { throw ( new Exception("Exception : Not a valid Page Resource file",ex)); }
            string PageTitle = PageResource.SelectSingleNode("Root").SelectSingleNode("PageTitle/Culture[@Code='" + PageLangCode +"']").InnerText;
            //Page.Title = PageTitle;
            XmlNodeList PageControls = PageResource.SelectSingleNode("Root").SelectNodes("PageElement");
            string ctlName, ctlType, strCap, strDefaultValue, IsMand, rfvMessage, IsReadOnly, IsDisabled, IsFormatingRequired, JsFunctionToFormat, OnClickJSFunction, OnChangeJSFunction;
            string IsDisplay, revExpEnabled, revExpKey, revExpMessage, csvEnabled, csvMessage, csvValidationFunction, leftPosition, topPosiion, strCSS,strPosition;
            foreach (XmlNode Ctl in PageControls)
            {
                ctlName         = Ctl.Attributes["name"].Value;
                ctlType         = Ctl.Attributes["type"].Value;
                
                IsMand          = Ctl.SelectSingleNode("IsMandatory") != null ? Ctl.SelectSingleNode("IsMandatory").InnerText : "";
                IsReadOnly      = Ctl.SelectSingleNode("IsReadOnly") != null ? Ctl.SelectSingleNode("IsReadOnly").InnerText : "";
                IsDisabled      = Ctl.SelectSingleNode("IsDisabled") != null ? Ctl.SelectSingleNode("IsDisabled").InnerText : "";
                IsFormatingRequired = Ctl.SelectSingleNode("IsFormatingRequired") != null ? Ctl.SelectSingleNode("IsFormatingRequired").InnerText : "";
                JsFunctionToFormat  = Ctl.SelectSingleNode("JsFunctionToFormat") != null ? Ctl.SelectSingleNode("JsFunctionToFormat").InnerText : "";
                OnClickJSFunction   = Ctl.SelectSingleNode("OnClickJSFunction") != null ? Ctl.SelectSingleNode("OnClickJSFunction").InnerText : "";
                OnChangeJSFunction  = Ctl.SelectSingleNode("OnChangeJSFunction") != null ? Ctl.SelectSingleNode("OnChangeJSFunction").InnerText : "";
                IsDisplay       = Ctl.SelectSingleNode("IsDisplay") != null ? Ctl.SelectSingleNode("IsDisplay").InnerText : "";
                //initialze Regular Expression attributes
                revExpEnabled   = Ctl.SelectSingleNode("rev/revExpEnabled") != null ? Ctl.SelectSingleNode("rev/revExpEnabled").InnerText : "";
                revExpKey       = Ctl.SelectSingleNode("rev/revExpKey") != null ? Ctl.SelectSingleNode("rev/revExpKey").InnerText : "";
                //initialze Custom validator attributes
                csvEnabled      = Ctl.SelectSingleNode("csv/csvEnabled") != null ? Ctl.SelectSingleNode("csv/csvEnabled").InnerText : "";
                csvValidationFunction = Ctl.SelectSingleNode("csv/csvValidationFunction") != null ? Ctl.SelectSingleNode("csv/csvValidationFunction").InnerText : "";
                
                leftPosition    = Ctl.SelectSingleNode("Style/Top") != null ? Ctl.SelectSingleNode("Style/Top").InnerText : "";
                topPosiion      = Ctl.SelectSingleNode("Style/Left") != null ? Ctl.SelectSingleNode("Style/Left").InnerText : "";
                strCSS          = Ctl.SelectSingleNode("Style/CssClass") != null ? Ctl.SelectSingleNode("Style/CssClass").InnerText : "midcolora";
                strPosition     = Ctl.SelectSingleNode("Style/Position") != null ? Ctl.SelectSingleNode("Style/Position").InnerText : "Absolute";
                //Fetching Culture Specific Message and Labels
                strCap = Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/Caption") != null ? Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/Caption").InnerText : "";
                strDefaultValue = Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/DefaultValue") != null ? Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/DefaultValue").InnerText : "";
                rfvMessage = Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/rfvMessage") != null ? Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/rfvMessage").InnerText : "";
                revExpMessage = Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/revExpMessage") != null ? Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/revExpMessage").InnerText : "";
                csvMessage = Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/csvMessage") != null ? Ctl.SelectSingleNode("Culture[@Code='" + PageLangCode + "']/csvMessage").InnerText : "";
                WC.WebControl ctl = null;
                if (CallerPage.FindControl(ctlName) == null)
                {
                    if (ctlType == "TextBox")
                    {
                        WC.TextBox ctl1 = new System.Web.UI.WebControls.TextBox();
                        ctl1.Text = strDefaultValue;

                        WC.Label cap = new System.Web.UI.WebControls.Label();
                        cap.ID = "cap" + ctlName.Substring(3);
                        cap.Text = strCap;
                        
                        //CallerPage.Form.Controls.Add(new LiteralControl("<br>"));
                        //CallerPage.Form.Controls.Add(cap);
                        //LiteralControl dv = new LiteralControl("<div>");
                        System.Web.UI.HtmlControls.HtmlGenericControl dv = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                        dv.ID = "dv" + ctlName.Substring(3);
                        dv.Style["Position"] = strPosition; // "Absolute";
                        dv.Style["Top"] = topPosiion+ "px";
                        dv.Style["Left"] = leftPosition + "px";
                        dv.Style["Class"] = strCSS;
                        dv.Controls.Add(cap);
                       
                        ctl1.Text = strDefaultValue;
                        ctl1.ReadOnly = IsReadOnly == "Yes" ? true : false;
                        ctl1.ID = ctlName;
                        ctl1.Enabled = true;
                        ctl1.Style["Position"] = "Absolute";
                        ctl1.MaxLength = 10;
                        ctl1.Style["size"] = "10";
                        //CallerPage.Form.Controls.Add(rev);
                        //CallerPage.Form.Controls.Add(new LiteralControl("<br>"));
                        //CallerPage.Form.Controls.Add(ctl1);
                        dv.Controls.Add(new LiteralControl("<br>"));
                        dv.Controls.Add(ctl1);
                        CallerPage.Form.Controls.Add(dv);
                        ctl = ctl1;
                    }
                           
                }

                if (CallerPage.FindControl(ctlName) != null)
                {
                    //WC.WebControl ctl=null;
                    if (ctlType == "TextBox")
                    {
                        WC.TextBox ctl1 = (WC.TextBox)CallerPage.FindControl(ctlName);
                        ctl1 = (WC.TextBox)CallerPage.FindControl(ctlName);
                        ctl1.Text = strDefaultValue;
                        ctl1.ReadOnly = IsReadOnly == "Yes" ? true : false;
                        ctl = ctl1;
                    }
                    else if (ctlType == "DDL")
                    {
                        WC.DropDownList ctl1 = (WC.DropDownList)CallerPage.FindControl(ctlName);
                        XmlNode DBObject = Ctl.SelectSingleNode("Bindings/DBObjectName");
                        string DBobjectName, objectType,SourceTextField,SourceValueField,whereClause;
                        if(DBObject!=null)
                        {
                            DBobjectName =  DBObject.InnerText;
                            if (DBobjectName != "")
                            {
                                objectType = DBObject.Attributes["type"].Value;
                                SourceTextField = Ctl.SelectSingleNode("Bindings/TextField") != null ? Ctl.SelectSingleNode("Bindings/TextField").InnerText : "";
                                SourceValueField = Ctl.SelectSingleNode("Bindings/ValueField") != null ? Ctl.SelectSingleNode("Bindings/ValueField").InnerText : "";
                                whereClause = Ctl.SelectSingleNode("Bindings/WhereClause") != null ? Ctl.SelectSingleNode("Bindings/WhereClause").InnerText : "";
                                try
                                {
                                    ctl1.DataSource = (new ClsCommon()).GetDDLDataSource(DBobjectName, objectType, SourceTextField, SourceValueField, whereClause);
                                    ctl1.DataTextField = SourceTextField;
                                    ctl1.DataValueField = SourceValueField;
                                    ctl1.DataBind();
                                    ctl1.Items.Insert(0, "");
                                    ctl1.SelectedIndex = -1;
                                }
                                catch { }
                            }
                        }
                        ctl1.SelectedIndex = ctl1.Items.IndexOf(ctl1.Items.FindByValue(strDefaultValue));
                        ctl = ctl1;
                    }
                    else if (ctlType == "CmsButton")
                    {
                        Cms.CmsWeb.Controls.CmsButton ctl1 = (Cms.CmsWeb.Controls.CmsButton)CallerPage.FindControl(ctlName);
                        ctl1.Text = strCap;
                        ctl = ctl1;
                    }
                    else if (ctlType == "Label")
                    {
                        WC.Label ctl1 = (WC.Label)CallerPage.FindControl(ctlName);
                        ctl1.Text = strCap;
                        ctl = ctl1;
                    }
                    else if (ctlType == "CheckBox")
                    {
                        WC.CheckBox ctl1 = (WC.CheckBox)CallerPage.FindControl(ctlName);
                        ctl1.Text = strCap;
                        ctl1.Checked = strDefaultValue == "True" ? true : false;
                        ctl = ctl1;
                    }
                    if (ctl != null)
                    {

                        ctl.Enabled = IsDisabled == "Yes" ? false : true;
                        WC.Label cap = (WC.Label)CallerPage.FindControl("cap" + ctlName.Substring(3));
                        if (cap != null) cap.Text = strCap;

                        WC.RequiredFieldValidator rfv = (WC.RequiredFieldValidator)CallerPage.FindControl("rfv" + ctlName.Substring(3));
                        if (rfv != null && IsMand == "Yes")
                        {
                            rfv.Enabled = true;
                            rfv.ErrorMessage = rfvMessage;
                        }
                        else if (rfv != null)
                        {
                            rfv.Enabled = false;
                            System.Web.UI.HtmlControls.HtmlGenericControl spn = (System.Web.UI.HtmlControls.HtmlGenericControl)CallerPage.FindControl("spn" + ctlName.Substring(3));
                            if (spn != null) spn.Attributes.Add("style", "display:none");
                        }
                        else if (rfv == null && IsMand == "Yes")
                        {
                            System.Web.UI.HtmlControls.HtmlGenericControl spn = (System.Web.UI.HtmlControls.HtmlGenericControl)CallerPage.FindControl("spn" + ctlName.Substring(3));
                            if (spn == null)
                                spn = new System.Web.UI.HtmlControls.HtmlGenericControl("span");
                            spn.ID = "spn" + ctlName.Substring(3);
                            spn.InnerText = "*";
                            spn.Style["Position"] = "Absolute";
                            spn.Attributes.Add("Class", "mandatory");
                            if (cap != null) cap.Parent.Controls.Add(spn);
        
                            rfv = new System.Web.UI.WebControls.RequiredFieldValidator();
                            rfv.ID = "rfv" + ctlName.Substring(3);
                            rfv.Enabled = true;
                            rfv.ErrorMessage = rfvMessage;
                            rfv.ControlToValidate = ctlName;
                            rfv.Style["Position"] = "Absolute";
                            //CallerPage.Form.Controls.Add(rfv);
                            ctl.Parent.Controls.Add(new LiteralControl("<br>"));
                            ctl.Parent.Controls.Add(rfv);
                        }

                        WC.RegularExpressionValidator rev = (WC.RegularExpressionValidator)Page.FindControl("rev" + ctlName.Substring(3));
                        if (rev != null)
                        {
                            rev.ValidationExpression = revExpKey;
                            rev.ErrorMessage = revExpMessage;
                            if (revExpEnabled == "Yes")
                                rev.Enabled = true;
                            else
                                rev.Enabled = false;
                        }
                        else if (rev == null && revExpEnabled == "Yes")
                        {
                            rev = new System.Web.UI.WebControls.RegularExpressionValidator();
                            rev.ID = "rev" + ctlName.Substring(3);
                            rev.Enabled = true;
                            rev.ErrorMessage = revExpMessage;
                            rev.ValidationExpression = revExpKey;
                            rev.ControlToValidate = ctlName;
                            rev.Style["Position"] = "Absolute";
                            //CallerPage.Form.Controls.Add(rev);
                            ctl.Parent.Controls.Add(new LiteralControl("<br>"));
                            ctl.Parent.Controls.Add(rev);
                        }
                        WC.CustomValidator csv = (WC.CustomValidator)Page.FindControl("csv" + ctlName.Substring(3));
                        if (csv != null)
                        {
                            csv.ClientValidationFunction = csvValidationFunction;
                            csv.ErrorMessage = csvMessage;
                            if (csvEnabled == "Yes")
                                csv.Enabled = true;
                            else
                                csv.Enabled = false;
                        }
                        else if (csv == null && csvEnabled == "Yes")
                        {
                            csv = new System.Web.UI.WebControls.CustomValidator();
                            csv.ID = "csv" + ctlName.Substring(3);
                            csv.Enabled = true;
                            csv.ErrorMessage = revExpMessage;
                            csv.ClientValidationFunction = csvValidationFunction;
                            csv.ControlToValidate = ctlName;
                            csv.Style["Position"] = "Absolute";
                            //CallerPage.Form.Controls.Add(rev);
                            ctl.Parent.Controls.Add(new LiteralControl("<br>"));
                            ctl.Parent.Controls.Add(csv);
                        }
                        if (IsDisplay == "No")
                        {
                            ctl.Attributes.Add("style", "display:none");
                            if (cap != null) cap.Attributes.Add("style", "display:none");
                            if (rfv != null) rfv.Enabled = false;
                            if (csv != null) csv.Enabled = false;
                            if (csv != null) csv.Enabled = false;
                        }
                        if (IsFormatingRequired == "Yes" && JsFunctionToFormat != "")
                            ctl.Attributes.Add("onblur", "javascript:" + JsFunctionToFormat + ";");
                        if (OnClickJSFunction != "")
                            ctl.Attributes.Add("onclick", "javascript:" + OnClickJSFunction + ";");
                        if (OnChangeJSFunction != "")
                            ctl.Attributes.Add("onchange", "javascript:" + OnChangeJSFunction + ";");
                    }

                }
            }
            PageResource = null;
        }
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
    }
}
