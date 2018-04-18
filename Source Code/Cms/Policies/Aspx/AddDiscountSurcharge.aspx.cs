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
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.CmsWeb.Controls;
using Cms.Model.Policy;

namespace Cms.Policies.aspx
{
    public partial class AddDiscountSurcharge : Cms.Policies.policiesbase
    {
        static DataTable dt = new DataTable();
        static DataSet ds = new DataSet();
        public string alertmsg, Confirmmsg;
        static string oldxml;
        System.Resources.ResourceManager objResourceMgr;
        static DataView Tempdv = new DataView();
        string CalledFrom = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            #region setting screen id
            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"].ToString() != "")
                CalledFrom = Request.QueryString["CalledFrom"].ToString();
            switch (CalledFrom)
            {
                case "CompCondo":
                    base.ScreenId = COMPREHENSIVE_CONDOMINIUMscreenId.DISCOUNTS_SURCHARGES;
                    break;
                case "RISK":
                    base.ScreenId = DIVERSIFIED_RISKSscreenId.DISCOUNTS_SURCHARGES;
                    break;
                case "DWELLING":
                    base.ScreenId = DWELLINGscreenId.DISCOUNTS_SURCHARGES;
                    break;
                case "ROBBERY":
                    base.ScreenId = ROBBERYscreenId.DISCOUNTS_SURCHARGES;
                    break;
                case "GenCvlLib":
                    base.ScreenId = GENERAL_CIVIL_LIABILITYscreenId.DISCOUNTS_SURCHARGES;
                    break;
                // for itrack 1161

                case "INDPA":
                    base.ScreenId = INDIVIDUAL_PERSONAL_ACCIDENTscreenId.DISCOUNTS_SURCHARGES;
                    break;
                default:
                    base.ScreenId = "224_29";
                    break;

            }

            #endregion

            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
                hidCustomer_Id.Value = Request.QueryString["CUSTOMER_ID"];
            else
                hidCustomer_Id.Value = GetCustomerID();

            if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
                hidPolicy_Id.Value = Request.QueryString["POLICY_ID"];
            else
                hidPolicy_Id.Value = GetPolicyID();

            if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
                hidPolicy_Version_Id.Value = Request.QueryString["POLICY_VERSION_ID"];
            else
                hidPolicy_Version_Id.Value = GetPolicyVersionID();

            if (Request.QueryString["RISK_ID"] != null && Request.QueryString["RISK_ID"].ToString() != "")
                hidRisk_Id.Value = Request.QueryString["RISK_ID"];

            if (Request.QueryString["CO_APPLICANT_ID"] != null && Request.QueryString["CO_APPLICANT_ID"].ToString() != "")
                hidCO_APPLICANT_ID.Value = Request.QueryString["CO_APPLICANT_ID"];



            objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.AddDiscountSurcharge", System.Reflection.Assembly.GetExecutingAssembly());

            alertmsg = objResourceMgr.GetString("alertmsg");  //Set javascript Alert message from resource file for multilingual Support
            Confirmmsg = objResourceMgr.GetString("Confirmmsg");  //Set javascript delete confirmation message from resource file for multilingual Support
            btnSave.Attributes.Add("onclick", "javascript:return ShowAlertMessageForDelete('chkDELETE',false);");
            if (!Page.IsPostBack)
            {
                DisCountType();
                FillCombo();
                GridBind();
                ErrorMessage();
                SetCaptions();

            }

            btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnDelete.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;

            btnAdd.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnAdd.PermissionString = gstrSecurityXML;

            SetCustomSecurityxml(hidCO_APPLICANT_ID.Value, CalledFrom);
        }

        private void SetCaptions()
        {
            lblHeader.Text = objResourceMgr.GetString("lblHeader");
            capTYPE.Text = objResourceMgr.GetString("capTYPE");
            lblManHeader.Text = objResourceMgr.GetString("lblManHeader");
            btnAdd.Text = ClsMessages.GetButtonsText(ScreenId, "btnSelect");

            //objResourceMgr.GetString("btnadd");
            //btndelete.Text = objResourceMgr.GetString("btndelete");
            //btnSave.Text = objResourceMgr.GetString("btnsave");
        }   //Set Page Caption From Resource File For MultiLingual        

        private void FillCombo()
        {

            cmbMAIN_TYPE.DataSource = dt.DefaultView;
            cmbMAIN_TYPE.DataTextField = "DISCOUNT_DESCRIPTION";
            cmbMAIN_TYPE.DataValueField = "DISCOUNT_ID";
            cmbMAIN_TYPE.DataBind();
            cmbMAIN_TYPE.Items.Insert(0, "");

        }  //Fill Discount/Surcharge Type  DropDown

        private void BindDisCountType(DropDownList cmbType, ref DataTable dt)
        {
            cmbType.DataSource = dt.DefaultView;
            cmbType.DataTextField = "DISCOUNT_DESCRIPTION";
            cmbType.DataValueField = "DISCOUNT_ID";
            cmbType.DataBind();
            cmbType.Items.Insert(0, "");

        }  //Fill GridView DropDown For Edit/Update 

        private void DisCountType()
        {
            if (CalledFrom.ToString().Trim().ToUpper() == "POLICY")
                dt = ClsGeneralInformation.getDiscountSurchargeList(int.Parse(hidCustomer_Id.Value), int.Parse(hidPolicy_Id.Value), int.Parse(hidPolicy_Version_Id.Value), "POL");
            else
                dt = ClsGeneralInformation.getDiscountSurchargeList(int.Parse(hidCustomer_Id.Value), int.Parse(hidPolicy_Id.Value), int.Parse(hidPolicy_Version_Id.Value), "RSK");

        }   //Bind Type DropDown From MNT_DISCOUNT_SURCHARGE Table

        protected void grdDISCOUNTS_SURCHARGES_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Label capHEADER_TYPE = (Label)e.Row.FindControl("capHEADER_TYPE");
                capHEADER_TYPE.Text = objResourceMgr.GetString("capHEADER_TYPE");

                Label capHEADER_PERCENT = (Label)e.Row.FindControl("capHEADER_PERCENT");
                capHEADER_PERCENT.Text = objResourceMgr.GetString("capHEADER_PERCENT");


            } //Bind Gridview Header Captions From resource for multilingual
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RequiredFieldValidator rfvPERCENT = (RequiredFieldValidator)e.Row.FindControl("rfvPERCENT");
                rfvPERCENT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1111");
                RegularExpressionValidator revPERCENT = (RegularExpressionValidator)e.Row.FindControl("revPERCENT");
                revPERCENT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("611");
                revPERCENT.ValidationExpression = aRegExpDoublePositiveWithZeroFourDeci;//aRegExpDoublePositiveWithZero
                CustomValidator csvPERCENT = (CustomValidator)e.Row.FindControl("csvPERCENT");
                csvPERCENT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1113");
                //PERCENTAGE
                TextBox txtPERCENT = (TextBox)e.Row.FindControl("txtPERCENT");
                if (txtPERCENT != null)
                {
                    txtPERCENT.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value,2)");
                    txtPERCENT.Attributes.Add("onChange", "javascript:this.value=formatAmount(this.value,2)");
                }
                numberFormatInfo.NumberDecimalDigits = 2;
                if (((TextBox)e.Row.FindControl("txtPERCENT")).Text != "")
                    txtPERCENT.Text = Convert.ToDouble(((TextBox)e.Row.FindControl("txtPERCENT")).Text).ToString("N", numberFormatInfo);



            } //Bind Gridview DataRow DropDown

        } //Bind DataRow in Gridview

        protected void btnadd_Click(object sender, EventArgs e)
        {
            bool exists = false;
            if (cmbMAIN_TYPE.SelectedIndex > 0)
            {
                int dsrow = ds.Tables[0].Rows.Count;
                for (int i = 0; i < dsrow; i++)
                {
                    if (ds.Tables[0].Rows[i]["DISCOUNT_ID"].ToString() == cmbMAIN_TYPE.SelectedValue && ds.Tables[0].Rows[i]["STATUS"].ToString() != "N")
                    {
                        exists = true;
                    }
                }
                if (!exists)
                {
                    lblMessage.Text = "";
                    AddGridrow(cmbMAIN_TYPE.SelectedItem.Text, int.Parse(cmbMAIN_TYPE.SelectedValue));

                }
                else
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1112"); //"Discount Surcharge Already Added";
                }
            }
        }  //Add button  for Add a  New Row In Grid

        private void AddGridrow(string DiscountDesc, int TYPE_ID)
        {

            double percentage = Double.Parse("0.00");
            foreach (DataRow dtdr in dt.Rows)
            {
                if (int.Parse(dtdr["DISCOUNT_ID"].ToString()) == TYPE_ID)
                {
                    //percentage = double.Parse(dtdr["PERCENTAGE"].ToString());
                    percentage=dtdr["PERCENTAGE"].ToString() != string.Empty ? double.Parse(dtdr["PERCENTAGE"].ToString()) : 0.0;//Changed By Amit k Mishra

                }
            }

            DataRow dr = ds.Tables[0].NewRow();
            dr["DISCOUNT_ROW_ID"] = DBNull.Value;
            dr["DISCOUNT_ID"] = TYPE_ID;
            dr["DISCOUNT_DESCRIPTION"] = DiscountDesc;

            if (percentage.ToString() != "0")
                dr["PERCENTAGE"] = percentage;
            else
                dr["PERCENTAGE"] = DBNull.Value;
            dr["STATUS"] = "Y";
            ds.Tables[0].Rows.Add(dr);
            hidAction.Value = "ADDNEW";

            MaintainLastRow(grdDISCOUNTS_SURCHARGES);
            GridBind();

            hidAction.Value = "";

        } //Add New Row If selected row not in selected list

        private void GridBind()
        {
            if (hidAction.Value != "DELETE" && hidAction.Value != "ADDNEW" && hidAction.Value != "AddBlank")
            {
                ClsGeneralInformation objClsGeneralInformation = new ClsGeneralInformation();
                ds = objClsGeneralInformation.GetPolicyDiscountSurcharge(int.Parse(hidCustomer_Id.Value), int.Parse(hidPolicy_Id.Value), int.Parse(hidPolicy_Version_Id.Value), CalledFrom, int.Parse(hidRisk_Id.Value));
                DataTable dtview = ds.Tables[0];
                oldxml = "";
                oldxml = ClsCommon.GetXML(dtview);

            }
            DataView dv = new DataView(ds.Tables[0], "STATUS='Y'", "", DataViewRowState.CurrentRows);
            if (dv.Count > 0)
            {
                Tempdv = dv;
                grdDISCOUNTS_SURCHARGES.DataSource = dv;
                grdDISCOUNTS_SURCHARGES.DataBind();
            }
            else
            {
                DataTable dtblank = new DataTable();
                dtblank.Columns.Add("DISCOUNT_ROW_ID", typeof(String));
                dtblank.Columns.Add("DISCOUNT_ID", typeof(String));
                dtblank.Columns.Add("DISCOUNT_DESCRIPTION", typeof(String));
                dtblank.Columns.Add("PERCENTAGE", typeof(String));
                dtblank.Columns.Add("STATUS", typeof(String));

                DataRow dr = dtblank.NewRow();
                dr[0] = "";
                dr[1] = "";
                dr[2] = "";
                dr[3] = "";
                dr[4] = "";
                dtblank.Rows.Add();

                grdDISCOUNTS_SURCHARGES.DataSource = dtblank;
                grdDISCOUNTS_SURCHARGES.DataBind();
                if (grdDISCOUNTS_SURCHARGES.Rows.Count > 0)
                {
                    for (int i = 0; i < grdDISCOUNTS_SURCHARGES.Rows[0].Cells.Count; i++)
                        grdDISCOUNTS_SURCHARGES.Rows[0].Cells[i].Controls.Clear();
                }
            }

        }   //Bind Grid view From Dataset

        private void MaintainLastRow(GridView Gv)
        {
            foreach (GridViewRow rw in Gv.Rows)
            {


                if (rw.RowType == DataControlRowType.DataRow)
                {

                    Label lblDISCOUNT_ID = (Label)rw.FindControl("DISCOUNT_ID");
                    TextBox txtPERCENT = (TextBox)rw.FindControl("txtPERCENT");
                    int RowIndex = rw.RowIndex;
                    if (ds.Tables[0].Rows[RowIndex] != null)
                    {
                        ds.Tables[0].Rows[RowIndex].BeginEdit();
                        if (lblDISCOUNT_ID.Text != "")
                            ds.Tables[0].Rows[RowIndex]["DISCOUNT_ID"] = int.Parse(lblDISCOUNT_ID.Text.Trim());
                        numberFormatInfo.NumberDecimalDigits = 2;
                        if (txtPERCENT.Text != "")
                            ds.Tables[0].Rows[RowIndex]["PERCENTAGE"] = double.Parse(txtPERCENT.Text.Trim(), numberFormatInfo);// (txtPERCENT.Text.Trim());


                        ds.Tables[0].Rows[RowIndex].EndEdit();
                        ds.AcceptChanges();





                    }

                }

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Tempdv.Count > 0)
            {
                int count = grdDISCOUNTS_SURCHARGES.Rows.Count;
                ArrayList alpremunerationobj = new ArrayList();
                int retunvalue = 0;
                //double dtotalPercent = 0; //Added by Charles on 7-Apr-10 

                ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();

                foreach (GridViewRow rw in grdDISCOUNTS_SURCHARGES.Rows)
                {
                    if (rw.RowType == DataControlRowType.DataRow)
                    {

                        CheckBox chkbox = (CheckBox)rw.Cells[0].FindControl("chkDELETE");
                        if (chkbox.Checked)
                        {
                            ClsDiscountSurchargeInfo objnewClsDiscountSurchargeInfo = new ClsDiscountSurchargeInfo();
                            Label lb = (Label)rw.Cells[1].FindControl("DISCOUNT_ROW_ID");
                            if (lb != null && lb.Text != "0" && lb.Text != "")
                            {//Update Discount surcharge

                                objnewClsDiscountSurchargeInfo = this.getformvalue(rw, ref objnewClsDiscountSurchargeInfo);
                                objnewClsDiscountSurchargeInfo.DISCOUNT_ROW_ID.CurrentValue = int.Parse(lb.Text);
                                objnewClsDiscountSurchargeInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                objnewClsDiscountSurchargeInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                                objnewClsDiscountSurchargeInfo.IS_ACTIVE.CurrentValue = "Y";
                                objnewClsDiscountSurchargeInfo.ACTION = "U";
                                //retunvalue = objGeneralInformation.UpdateBroker(objnewPolicyRemunerationInfo);
                                alpremunerationobj.Add(objnewClsDiscountSurchargeInfo);

                            }
                            else
                            {
                                //Add New Discount Surcharge

                                objnewClsDiscountSurchargeInfo = this.getformvalue(rw, ref objnewClsDiscountSurchargeInfo);
                                objnewClsDiscountSurchargeInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                                objnewClsDiscountSurchargeInfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                objnewClsDiscountSurchargeInfo.IS_ACTIVE.CurrentValue = "Y";
                                objnewClsDiscountSurchargeInfo.ACTION = "I";
                                alpremunerationobj.Add(objnewClsDiscountSurchargeInfo);

                            }
                        }
                    }

                }
                int CreatedBy = int.Parse(GetUserId());
                retunvalue = objGeneralInformation.AddUpdateDiscountSurcharge(alpremunerationobj, oldxml, int.Parse(hidCustomer_Id.Value), int.Parse(hidPolicy_Id.Value), int.Parse(hidPolicy_Version_Id.Value), CreatedBy);
                if (retunvalue > 0)
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("29");
                }
                GridBind();
                //Save Discount surcharge Commission
            }
            else
            { //lblMessage.Text = objResourceMgr.GetString("selectdiscount"); 
                GridBind();
            }
            base.OpenEndorsementDetails();

        }   //Save btn Click For Add New Or Update Discount Percentage

        private ClsDiscountSurchargeInfo getformvalue(GridViewRow rw, ref ClsDiscountSurchargeInfo objnewClsDiscountSurchargeInfo)
        {
            objnewClsDiscountSurchargeInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCustomer_Id.Value);
            objnewClsDiscountSurchargeInfo.POLICY_ID.CurrentValue = int.Parse(hidPolicy_Id.Value);
            objnewClsDiscountSurchargeInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPolicy_Version_Id.Value);
            objnewClsDiscountSurchargeInfo.RISK_ID.CurrentValue = int.Parse(hidRisk_Id.Value);
            Label lblDISCOUNT_ID = (Label)rw.FindControl("DISCOUNT_ID");
            if (lblDISCOUNT_ID != null)
                objnewClsDiscountSurchargeInfo.DISCOUNT_ID.CurrentValue = Convert.ToInt32(lblDISCOUNT_ID.Text.Trim());

            TextBox txtPERCENT = (TextBox)rw.FindControl("txtPERCENT");
            if (txtPERCENT != null)
                if (txtPERCENT.Text != "")
                    objnewClsDiscountSurchargeInfo.PERCENTAGE.CurrentValue = Convert.ToDouble(txtPERCENT.Text.Trim(), numberFormatInfo);

            Label DISCOUNT_DESC = (Label)rw.FindControl("DISCOUNT_DESCRIPTION");
            if (lblDISCOUNT_ID != null)
                objnewClsDiscountSurchargeInfo.DISCOUNT_DESCRIPTION.CurrentValue = DISCOUNT_DESC.Text.Trim();

            return objnewClsDiscountSurchargeInfo;
        }  //GetFormvalue methode for getting the user input values from controls

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int retunvalue = 0;
            ClsGeneralInformation objClsGeneralInformation = new ClsGeneralInformation();
            foreach (GridViewRow row in grdDISCOUNTS_SURCHARGES.Rows)
            {
                ClsDiscountSurchargeInfo objClsDiscountSurchargeInfo = new ClsDiscountSurchargeInfo();

                if (row.RowType == DataControlRowType.DataRow)
                {

                    CheckBox chkbox = (CheckBox)row.Cells[0].FindControl("chkDELETE");
                    if (chkbox.Checked)
                    {
                        hidAction.Value = "DELETE";

                        Label lblDISCOUNT_ROW_ID = (Label)row.Cells[1].FindControl("DISCOUNT_ROW_ID");
                        if (lblDISCOUNT_ROW_ID.Text != null)
                        {
                            if (lblDISCOUNT_ROW_ID.Text != "0" && lblDISCOUNT_ROW_ID.Text != "")
                            {
                                objClsDiscountSurchargeInfo = this.getformvalue(row, ref objClsDiscountSurchargeInfo);
                                objClsDiscountSurchargeInfo.DISCOUNT_ROW_ID.CurrentValue = int.Parse(lblDISCOUNT_ROW_ID.Text);
                                objClsDiscountSurchargeInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                                //objClsDiscountSurchargeInfo.CUSTOMER_ID.CurrentValue = Convert.ToInt32(Request.QueryString["CUSTOMER_ID"]);
                                //objClsDiscountSurchargeInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(Request.QueryString["POLICY_VERSION_ID"]);
                                // objClsDiscountSurchargeInfo.POLICY_ID.CurrentValue = int.Parse(Request.QueryString["POLICY_ID"]);
                                retunvalue = objClsGeneralInformation.DeleteDiscountSurchargePercent(objClsDiscountSurchargeInfo, int.Parse(lblDISCOUNT_ROW_ID.Text));

                            }
                        }
                        Label lblDISCOUNT_ID = (Label)row.Cells[1].FindControl("DISCOUNT_ID");
                        if (lblDISCOUNT_ID != null)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                if (dr["DISCOUNT_ID"].ToString() == lblDISCOUNT_ID.Text)
                                {
                                    dr.BeginEdit();
                                    dr["STATUS"] = "N";
                                    dr.EndEdit();
                                    ds.AcceptChanges();
                                    retunvalue = 1;
                                }
                            }
                        }




                        if (retunvalue > 0)
                        {
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("5");

                        }
                        else
                        {
                            lblMessage.Text = "";
                        }
                    }
                    else
                    {
                        TextBox Commission_Percent = (TextBox)row.Cells[4].FindControl("txtCOMMISSION");
                        ///ArOld.Add(Commission_Percent.Text);
                    }
                }

            }
            GridBind();
            hidAction.Value = "";

        }   //Delete Btn Click For delete discount Percentage from grid as well database

        private void ErrorMessage()
        {
            rfvMAIN_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1110");
        }         //Validator Error Messages
        //master policy imlimentation
        private bool SetCustomSecurityxml(string CO_APP_ID, string CalledFrom)
        {
            bool Valid = true;
            if (CalledFrom.ToUpper() != "PAPEACC" //for personal accident for passenges
                && CalledFrom.ToUpper() != "CLTVEHICLEINFO" //civil Liability Transportation
                && CalledFrom.ToUpper() != "FLVEHICLEINFO" //Facultative Liability
                && CalledFrom.ToUpper() != "GRPLF" //Group life
                && CalledFrom.ToUpper() != "CPCACC" //for group personal accident for passenger

                )
                return Valid;

            if (GetTransaction_Type().Trim() == MASTER_POLICY)
            {
                string SecurityXml = base.CustomSecurityXml(CO_APP_ID);
                btnSave.PermissionString = SecurityXml;
                btnDelete.PermissionString = SecurityXml;

            }
            return Valid;
        }
    }
}