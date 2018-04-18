using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class PageXML : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //TreeControlPage1.trvHandler += new TreeControlPage.LinkClicked(TreeControlCustomized1_trvHandler);
    }

    private void TreeControlCustomized1_trvHandler(XmlNodeList strValue)
    {
        /*rpt.DataSource = strValue;
        rpt.DataBind();*/
    }
    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item is RepeaterItem)
        {

            Label lblName = (Label)e.Item.FindControl("lblName");

            TextBox txtName = (TextBox)e.Item.FindControl("txtName");
            DropDownList txtType = (DropDownList)e.Item.FindControl("txtType");
            CheckBox chkIsMandatory = (CheckBox)e.Item.FindControl("chkIsMandatory");
            CheckBox chkIsReadOnly = (CheckBox)e.Item.FindControl("chkIsReadOnly");
            CheckBox chkIsDisabled = (CheckBox)e.Item.FindControl("chkIsDisabled");
            CheckBox chkIsDisplay = (CheckBox)e.Item.FindControl("chkIsDisplay");
            CheckBox chkIsFormatingRequired = (CheckBox)e.Item.FindControl("chkIsFormatingRequired");
            TextBox txtJsFunctionToFormat = (TextBox)e.Item.FindControl("txtJsFunctionToFormat");
            TextBox txtOnClickJSFunction = (TextBox)e.Item.FindControl("txtOnClickJSFunction");
            TextBox txtOnChangeJSFunction = (TextBox)e.Item.FindControl("txtOnChangeJSFunction");

            DropDownList txtPosition = (DropDownList)e.Item.FindControl("txtPosition");
            TextBox txtTop = (TextBox)e.Item.FindControl("txtTop");
            TextBox txtLeft = (TextBox)e.Item.FindControl("txtLeft");
            TextBox txtCssClass = (TextBox)e.Item.FindControl("txtCssClass");
            TextBox txtParentTableCell = (TextBox)e.Item.FindControl("txtParentTableCell");

            CheckBox chkEnabledR = (CheckBox)e.Item.FindControl("chkEnabledR");
            TextBox txtExpKey = (TextBox)e.Item.FindControl("txtExpKey");

            CheckBox chkEnabledC = (CheckBox)e.Item.FindControl("chkEnabledC");
            TextBox txtcsvValidationFunction = (TextBox)e.Item.FindControl("txtcsvValidationFunction");

            DropDownList txtCode0 = (DropDownList)e.Item.FindControl("txtCode0");
            TextBox txtDesc0 = (TextBox)e.Item.FindControl("txtDesc0");
            TextBox txtCaption0 = (TextBox)e.Item.FindControl("txtCaption0");
            TextBox txtrfvMessage0 = (TextBox)e.Item.FindControl("txtrfvMessage0");
            TextBox txtDefaultValue0 = (TextBox)e.Item.FindControl("txtDefaultValue0");
            TextBox txtrevExpMessage0 = (TextBox)e.Item.FindControl("txtrevExpMessage0");

            DropDownList txtCode = (DropDownList)e.Item.FindControl("txtCode");
            TextBox txtDesc = (TextBox)e.Item.FindControl("txtDesc");
            TextBox txtCaption = (TextBox)e.Item.FindControl("txtCaption");
            TextBox txtrfvMessage = (TextBox)e.Item.FindControl("txtrfvMessage");
            TextBox txtDefaultValue = (TextBox)e.Item.FindControl("txtDefaultValue");
            TextBox txtrevExpMessage = (TextBox)e.Item.FindControl("txtrevExpMessage");

            XmlNode dv = (XmlNode)e.Item.DataItem;
            lblName.Text = dv.Attributes["name"].Value;

            try { txtName.Text = (dv).Attributes["name"].Value; }
            catch (Exception ex) { }
            try { txtType.Text = (dv).Attributes["type"].Value; }
            catch (Exception ex) { }
            try { chkIsMandatory.Checked = ((dv).Attributes["IsMandatory"].Value != "Yes") ? false : true; }
            catch (Exception ex) { }
            try { chkIsReadOnly.Checked = ((dv).Attributes["IsReadOnly"].Value != "Yes") ? false : true; }
            catch (Exception ex) { }
            try { chkIsDisabled.Checked = ((dv).Attributes["IsDisabled"].Value != "Yes") ? false : true; }
            catch (Exception ex) { }
            try { chkIsDisplay.Checked = ((dv).Attributes["IsDisplay"].Value != "Yes") ? false : true; }
            catch (Exception ex) { }
            try { chkIsFormatingRequired.Checked = ((dv).Attributes["IsFormatingRequired"].Value != "Yes") ? false : true; }
            catch (Exception ex) { }
            try { txtJsFunctionToFormat.Text = (dv).Attributes["JsFunctionToFormat"].Value; }
            catch (Exception ex) { }
            try { txtOnClickJSFunction.Text = (dv).Attributes["OnClickJSFunction"].Value; }
            catch (Exception ex) { }
            try { txtOnChangeJSFunction.Text = (dv).Attributes["OnChangeJSFunction"].Value; }
            catch (Exception ex) { }

            try { txtPosition.Text = (dv.ChildNodes[0]).Attributes["Position"].Value; }
            catch (Exception ex) { }
            try { txtTop.Text = (dv.ChildNodes[0]).Attributes["Top"].Value; }
            catch (Exception ex) { }
            try { txtLeft.Text = (dv.ChildNodes[0]).Attributes["Left"].Value; }
            catch (Exception ex) { }
            try { txtCssClass.Text = (dv.ChildNodes[0]).Attributes["CssClass"].Value; }
            catch (Exception ex) { }
            try { txtParentTableCell.Text = (dv.ChildNodes[0]).Attributes["ParentTableCell"].Value; }
            catch (Exception ex) { }

            try { chkEnabledR.Checked = ((dv.ChildNodes[1]).Attributes["Enabled"].Value != "Yes") ? false : true; }
            catch (Exception ex) { }
            try { txtExpKey.Text = (dv.ChildNodes[1]).Attributes["ExpKey"].Value; }
            catch (Exception ex) { }

            try { chkEnabledC.Checked = ((dv.ChildNodes[2]).Attributes["Enabled"].Value != "Yes") ? false : true; }
            catch (Exception ex) { }
            try { txtcsvValidationFunction.Text = (dv.ChildNodes[2]).Attributes["csvValidationFunction"].Value; }
            catch (Exception ex) { }

            try { txtCode0.Text = (dv.ChildNodes[3]).Attributes["Code"].Value; }
            catch (Exception ex) { }
            try { txtDesc0.Text = (dv.ChildNodes[3]).Attributes["Desc"].Value; }
            catch (Exception ex) { }
            try { txtCaption0.Text = (dv.ChildNodes[3].ChildNodes[0]).InnerText; }
            catch (Exception ex) { }
            try { txtrfvMessage0.Text = (dv.ChildNodes[3].ChildNodes[1]).InnerText; }
            catch (Exception ex) { }
            try { txtDefaultValue0.Text = (dv.ChildNodes[3].ChildNodes[2]).InnerText; }
            catch (Exception ex) { }
            try { txtrevExpMessage0.Text = (dv.ChildNodes[3].ChildNodes[3]).InnerText; }
            catch (Exception ex) { }

            try { txtCode.Text = (dv.ChildNodes[4]).Attributes["Code"].Value; }
            catch (Exception ex) { }
            try { txtDesc.Text = (dv.ChildNodes[4]).Attributes["Desc"].Value; }
            catch (Exception ex) { }
            try { txtCaption.Text = (dv.ChildNodes[4].ChildNodes[0]).InnerText; }
            catch (Exception ex) { }
            try { txtrfvMessage.Text = (dv.ChildNodes[4].ChildNodes[1]).InnerText; }
            catch (Exception ex) { }
            try { txtDefaultValue.Text = (dv.ChildNodes[4].ChildNodes[2]).InnerText; }
            catch (Exception ex) { }
            try { txtrevExpMessage.Text = (dv.ChildNodes[4].ChildNodes[3]).InnerText; }
            catch (Exception ex) { }

        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {/*
        try
        {
            int iCount = 0;
            XmlDocument xdom = new XmlDocument();
            xdom.LoadXml(Session["XmlStream"].ToString());
            foreach (RepeaterItem rptItem in rpt.Items)
            {
                Label lblName = (Label)rptItem.FindControl("lblName");
                string xPathExpression = "//PageElement[@name='" + lblName.Text + "']";
                XmlNodeList xNode = xdom.SelectNodes(xPathExpression);

                TextBox txtName = (TextBox)rptItem.FindControl("txtName");
                DropDownList txtType = (DropDownList)rptItem.FindControl("txtType");
                CheckBox chkIsMandatory = (CheckBox)rptItem.FindControl("chkIsMandatory");
                CheckBox chkIsReadOnly = (CheckBox)rptItem.FindControl("chkIsReadOnly");
                CheckBox chkIsDisabled = (CheckBox)rptItem.FindControl("chkIsDisabled");
                CheckBox chkIsDisplay = (CheckBox)rptItem.FindControl("chkIsDisplay");
                CheckBox chkIsFormatingRequired = (CheckBox)rptItem.FindControl("chkIsFormatingRequired");
                TextBox txtJsFunctionToFormat = (TextBox)rptItem.FindControl("txtJsFunctionToFormat");
                TextBox txtOnClickJSFunction = (TextBox)rptItem.FindControl("txtOnClickJSFunction");
                TextBox txtOnChangeJSFunction = (TextBox)rptItem.FindControl("txtOnChangeJSFunction");

                DropDownList txtPosition = (DropDownList)rptItem.FindControl("txtPosition");
                TextBox txtTop = (TextBox)rptItem.FindControl("txtTop");
                TextBox txtLeft = (TextBox)rptItem.FindControl("txtLeft");
                TextBox txtCssClass = (TextBox)rptItem.FindControl("txtCssClass");
                TextBox txtParentTableCell = (TextBox)rptItem.FindControl("txtParentTableCell");

                CheckBox chkEnabledR = (CheckBox)rptItem.FindControl("chkEnabledR");
                TextBox txtExpKey = (TextBox)rptItem.FindControl("txtExpKey");

                CheckBox chkEnabledC = (CheckBox)rptItem.FindControl("chkEnabledC");
                TextBox txtcsvValidationFunction = (TextBox)rptItem.FindControl("txtcsvValidationFunction");

                DropDownList txtCode0 = (DropDownList)rptItem.FindControl("txtCode0");
                TextBox txtDesc0 = (TextBox)rptItem.FindControl("txtDesc0");
                TextBox txtCaption0 = (TextBox)rptItem.FindControl("txtCaption0");
                TextBox txtrfvMessage0 = (TextBox)rptItem.FindControl("txtrfvMessage0");
                TextBox txtDefaultValue0 = (TextBox)rptItem.FindControl("txtDefaultValue0");
                TextBox txtrevExpMessage0 = (TextBox)rptItem.FindControl("txtrevExpMessage0");

                DropDownList txtCode = (DropDownList)rptItem.FindControl("txtCode");
                TextBox txtDesc = (TextBox)rptItem.FindControl("txtDesc");
                TextBox txtCaption = (TextBox)rptItem.FindControl("txtCaption");
                TextBox txtrfvMessage = (TextBox)rptItem.FindControl("txtrfvMessage");
                TextBox txtDefaultValue = (TextBox)rptItem.FindControl("txtDefaultValue");
                TextBox txtrevExpMessage = (TextBox)rptItem.FindControl("txtrevExpMessage");

                try { (xNode[iCount]).Attributes["name"].Value = txtName.Text; }
                catch (Exception ex) { }
                try { (xNode[iCount]).Attributes["type"].Value = txtType.Text; }
                catch (Exception ex) { }
                try { (xNode[iCount]).Attributes["IsMandatory"].Value = (chkIsMandatory.Checked == true) ? "Yes" : "No"; }
                catch (Exception ex) { }
                try { (xNode[iCount]).Attributes["IsReadOnly"].Value = (chkIsReadOnly.Checked == true) ? "Yes" : "No"; }
                catch (Exception ex) { }
                try { (xNode[iCount]).Attributes["IsDisabled"].Value = (chkIsDisabled.Checked == true) ? "Yes" : "No"; }
                catch (Exception ex) { }
                try { (xNode[iCount]).Attributes["IsDisplay"].Value = (chkIsDisplay.Checked == true) ? "Yes" : "No"; }
                catch (Exception ex) { }
                try { (xNode[iCount]).Attributes["IsFormatingRequired"].Value = (chkIsFormatingRequired.Checked == true) ? "Yes" : "No"; }
                catch (Exception ex) { }
                try { (xNode[iCount]).Attributes["JsFunctionToFormat"].Value = txtJsFunctionToFormat.Text; }
                catch (Exception ex) { }
                try { (xNode[iCount]).Attributes["OnClickJSFunction"].Value = txtOnClickJSFunction.Text; }
                catch (Exception ex) { }
                try { (xNode[iCount]).Attributes["OnChangeJSFunction"].Value = txtOnChangeJSFunction.Text; }
                catch (Exception ex) { }

                try { (xNode[iCount].ChildNodes[0]).Attributes["Position"].Value = txtPosition.Text; }
                catch (Exception ex) { }
                try { (xNode[iCount].ChildNodes[0]).Attributes["Top"].Value = txtTop.Text; }
                catch (Exception ex) { }
                try { (xNode[iCount].ChildNodes[0]).Attributes["Left"].Value = txtLeft.Text; }
                catch (Exception ex) { }
                try { (xNode[iCount].ChildNodes[0]).Attributes["CssClass"].Value = txtCssClass.Text; }
                catch (Exception ex) { }
                try { (xNode[iCount].ChildNodes[0]).Attributes["ParentTableCell"].Value = txtParentTableCell.Text; }
                catch (Exception ex) { }

                try { (xNode[iCount].ChildNodes[1]).Attributes["Enabled"].Value = (chkEnabledR.Checked == true) ? "Yes" : "No"; }
                catch (Exception ex) { }
                try { (xNode[iCount].ChildNodes[1]).Attributes["ExpKey"].Value = txtExpKey.Text; }
                catch (Exception ex) { }

                try { (xNode[iCount].ChildNodes[2]).Attributes["Enabled"].Value = (chkEnabledC.Checked == true) ? "Yes" : "No"; }
                catch (Exception ex) { }
                try { (xNode[iCount].ChildNodes[2]).Attributes["csvValidationFunction"].Value = txtcsvValidationFunction.Text; }
                catch (Exception ex) { }

                try { (xNode[iCount].ChildNodes[3]).Attributes["Code"].Value = txtCode0.Text; }
                catch (Exception ex) { }
                try { (xNode[iCount].ChildNodes[3]).Attributes["Desc"].Value = txtDesc0.Text; }
                catch (Exception ex) { }
                try { (xNode[iCount].ChildNodes[3].ChildNodes[0]).InnerText = txtCaption0.Text; }
                catch (Exception ex) { }
                try { (xNode[iCount].ChildNodes[3].ChildNodes[1]).InnerText = txtrfvMessage0.Text; }
                catch (Exception ex) { }
                try { (xNode[iCount].ChildNodes[3].ChildNodes[2]).InnerText = txtDefaultValue0.Text; }
                catch (Exception ex) { }
                try { (xNode[iCount].ChildNodes[3].ChildNodes[3]).InnerText = txtrevExpMessage0.Text; }
                catch (Exception ex) { }

                try { (xNode[iCount].ChildNodes[4]).Attributes["Code"].Value = txtCode.Text; }
                catch (Exception ex) { }
                try { (xNode[iCount].ChildNodes[4]).Attributes["Desc"].Value = txtDesc.Text; }
                catch (Exception ex) { }
                try { (xNode[iCount].ChildNodes[4].ChildNodes[0]).InnerText = txtCaption.Text; }
                catch (Exception ex) { }
                try { (xNode[iCount].ChildNodes[4].ChildNodes[1]).InnerText = txtrfvMessage.Text; }
                catch (Exception ex) { }
                try { (xNode[iCount].ChildNodes[4].ChildNodes[2]).InnerText = txtDefaultValue.Text; }
                catch (Exception ex) { }
                try { (xNode[iCount].ChildNodes[4].ChildNodes[3]).InnerText = txtrevExpMessage.Text; }
                catch (Exception ex) { }
                iCount++;
            }
            Session["XmlStream"] = xdom.OuterXml;
        }
        catch (Exception ex)
        {

        }*/
    }
}