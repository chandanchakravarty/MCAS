using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Resources;
using System.Collections;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using Cms.Model.Policy;

namespace Cms.Policies.Aspx
{
    public partial class AddCoinsurance : Cms.Policies.policiesbase
    {
        #region Variables
        static DataTable Dt = new DataTable();
        //static int i = 0;
        bool BtnFlagClick = true;
        //int rowcount;
        System.Resources.ResourceManager objResourceMgr;
        protected System.Web.UI.WebControls.Label cap_BROKER_COMMISSION;
        protected System.Web.UI.WebControls.Label cap_Carriername;
        protected System.Web.UI.WebControls.Label cap_CO_INSURER_NAME;
        protected System.Web.UI.WebControls.Label cap_COINSURANCE_FEE;
        protected System.Web.UI.WebControls.Label cap_COINSURANCE_PERCENT;
        protected System.Web.UI.WebControls.Label cap_LEADER_FOLLOWER;
        protected System.Web.UI.WebControls.Label cap_LEADER_POLICY_NUMBER;
        protected System.Web.UI.WebControls.Label cap_Select;
        protected System.Web.UI.WebControls.Label cap_ENDORSEMENT_POLICY_NUMBER;
      //  protected System.Web.UI.WebControls.Label lblPolicyCaption;
      //  protected System.Web.UI.WebControls.TextBox txtBROKER_COMMISSION;
        protected System.Web.UI.WebControls.TextBox txtREIN_COMAPANY_NAME;
        protected System.Web.UI.WebControls.TextBox txtCO_INSURER_NAME;
        protected System.Web.UI.WebControls.TextBox txtCOINSURANCE_FEE;
        protected System.Web.UI.WebControls.TextBox txtCOINSURANCE_PERCENT;
        protected System.Web.UI.WebControls.TextBox txtLEADER_POLICY_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtTRANSACTION_ID;
        protected System.Web.UI.WebControls.TextBox txtENDORSEMENT_POLICY_NUMBER;
        protected System.Web.UI.WebControls.DropDownList cmbLEADER_FOLLOWER;
        protected System.Web.UI.WebControls.CheckBox chkSelect;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCOINSURANCE_PERCENT;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCOINSURANCE_FEE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvTRANSACTION_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvLEADER_POLICY_NUMBER;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvENDORSEMENT_POLICY_NUMBER;
       // protected System.Web.UI.WebControls.RegularExpressionValidator revBROKER_COMMISSION;
        protected CustomValidator csvCOINSURANCE_PERCENT;
       // protected CustomValidator csvBROKER_COMMISSION;
        protected CustomValidator csvCOINSURANCE_FEE;
        protected System.Web.UI.WebControls.Label cap_TRANSACTION_ID;
        protected RegularExpressionValidator RegExpAlphaNumStrict;

        static DataSet objDataSet = new DataSet();
        public static string confirmmessage;
        public static string selectChk;
        public static string leadercheckmsg;
        public static string deleteleaderalert;
        string policystatus;
        int policy_version_id;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            #region setting screen id
            base.ScreenId = "224_25";
            #endregion

            objResourceMgr = new System.Resources.ResourceManager("Cms.policies.Aspx.AddCoinsurance", System.Reflection.Assembly.GetExecutingAssembly());
            policystatus = GetPolicyStatus();
            hidpolicystatus.Value = policystatus;
            policy_version_id = int.Parse(GetPolicyVersionID());
            //rowcount = rep.Items.Count; 
            //hidvalue.Value = rowcount.ToString();
            //Added by Charles on 7-Jun-2010
            if (!IsPostBack)
            {
                fillcarrier();
                
            }

            if (hidCO_INSURANCE.Value == "14547")//Direct
            {
                pnlShowHide.Visible = false;
                lblMessage.Visible = true;
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "8");
                
                return;
            }
           
            else
            {
                pnlShowHide.Visible = true;
                lblMessage.Visible = false;
            }//Added till here         
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            btnSelect.Text = Cms.CmsWeb.ClsMessages.FetchGeneralButtonsText("btnSelect");
            btnSave.Text = Cms.CmsWeb.ClsMessages.FetchGeneralButtonsText("btnSave");
            btnDelete.Text = Cms.CmsWeb.ClsMessages.FetchGeneralButtonsText("btnDelete");
            //btnDelete.Visible = false;

           
                

            #region setting security Xml
            btnSelect.CmsButtonClass = CmsButtonType.Write;
            btnSelect.PermissionString = gstrSecurityXML;

            btnDelete.CmsButtonClass = CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            //SetCaptions();
            #endregion

            if (!IsPostBack)
            {
                hidCustomerID.Value = GetCustomerID();
                hidPolID.Value = GetPolicyID();
                hidPolVersionID.Value = GetPolicyVersionID();
                //fillcarrier(); //Moved to Top, Charles on 7-Jun-2010
                GridBind();
                SetCaptions();
                if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "Policies/support/PageXML/" + GetSystemId(), "AddCoinsurance.xml"))
                {
                    setPageControls(Page, @Request.PhysicalApplicationPath + "Policies/support/PageXML/" + GetSystemId() + "/AddCoinsurance.xml");
                }
                //SetErrorMessages();
                //rfvCarrier.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_9"); 
                
            }            
        }

        #region Methods

        private void fillcarrier()
        {
            objDataSet = ClsGeneralInformation.GetCoInsurers(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()));
            objDataSet.Tables[0].Columns.Add("Coinsurer", typeof(double));
            cmbCarrier.Items.Clear();
            cmbCarrier.DataSource = ClsGeneralInformation.GetCoInsurers(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()));
            cmbCarrier.DataTextField = "REIN_COMAPANY_NAME";
            cmbCarrier.DataValueField = "REIN_COMAPANY_ID";
            cmbCarrier.DataBind();
            cmbCarrier.Items.Insert(0, new ListItem("", ""));
            cmbCarrier.SelectedIndex = -1;
            //cmbCarrier.Items.Insert(0,new ListItem("Select Carrier","0"));

            //Added by Charles on 7-Jun-2010
            if (objDataSet!=null && objDataSet.Tables.Count>0)
            {
                hidCO_INSURANCE.Value = objDataSet.Tables[1].Rows[0]["CO_INSURANCE"].ToString().Trim();

                if (objDataSet.Tables[2].Rows.Count > 0)
                {
                    hidCO_INSLEADER.Value = objDataSet.Tables[2].Rows[0]["TOTLEADER"].ToString();//added by sonal 26 july 2010 

                    hidCOINSURANCE_ID.Value = objDataSet.Tables[2].Rows[0]["COINSURANCE_ID"].ToString();
                }
                else
                {
                    hidCO_INSLEADER.Value = "0";
                }
                hidPOL_APPNUMBER.Value = objDataSet.Tables[3].Rows[0]["POL_APPNUMBER"].ToString();
               
            }//Added till here

           

        }

        private void GridBind()
        {

            Dt = ClsGeneralInformation.GetPolicyCoInsurers(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), 0, null);
            rep.DataSource = Dt;
            rep.DataBind();
            hidOldData.Value = ClsCommon.GetXMLEncoded(Dt);
            if (Dt.Rows.Count == 0)
            {
                //DataRow dr = Dt.NewRow();
                //Dt.Rows.Add(dr);
                //rep.DataSource = Dt;
                //rep.DataBind();
            }
        }

        private void SetCaptions()
        {
            #region setcaption in resource file
            capCarrier.Text = objResourceMgr.GetString("cmbCarrier");
            confirmmessage = objResourceMgr.GetString("confirmmessage");
            selectChk = objResourceMgr.GetString("selectChk");
            leadercheckmsg = objResourceMgr.GetString("Leadercheck");
            deleteleaderalert = objResourceMgr.GetString("deleteleaderalert");
            //if (Dt.Rows.Count != 0)
            //{
              //  ((Label)rep.Controls[0].FindControl("cap_BROKER_COMMISSION")).Text = objResourceMgr.GetString("txtBROKER_COMMISSION");
                ((Label)rep.Controls[0].FindControl("cap_Carriername")).Text = objResourceMgr.GetString("txtREIN_COMAPANY_NAME");
                ((Label)rep.Controls[0].FindControl("cap_CO_INSURER_NAME")).Text = objResourceMgr.GetString("txtCO_INSURER_NAME");
                ((Label)rep.Controls[0].FindControl("cap_COINSURANCE_FEE")).Text = objResourceMgr.GetString("txtCOINSURANCE_FEE");
                ((Label)rep.Controls[0].FindControl("cap_COINSURANCE_PERCENT")).Text = objResourceMgr.GetString("txtCOINSURANCE_PERCENT");
                ((Label)rep.Controls[0].FindControl("cap_LEADER_POLICY_NUMBER")).Text = objResourceMgr.GetString("txtLEADER_POLICY_NUMBER");
                ((Label)rep.Controls[0].FindControl("cap_TRANSACTION_ID")).Text = objResourceMgr.GetString("txtTRANSACTION_ID");
                ((Label)rep.Controls[0].FindControl("cap_Select")).Text = objResourceMgr.GetString("chkSelect");
                ((Label)rep.Controls[0].FindControl("cap_LEADER_FOLLOWER")).Text = objResourceMgr.GetString("cmbLEADER_FOLLOWER");
                ((Label)rep.Controls[0].FindControl("cap_ENDORSEMENT_POLICY_NUMBER")).Text = objResourceMgr.GetString("cap_ENDORSEMENT_POLICY_NUMBER");
                  lblPolicyCaption.Text = objResourceMgr.GetString("lblPolicyCaption");

            //}
            #endregion
        } 

        private void AddGridrow(string CarrierName, int CarrierId)
        {
            //hidNewValue.Value = "AddNew";
            DataRow dr = Dt.NewRow();

            dr["REIN_COMAPANY_NAME"] = CarrierName;
            dr["REIN_COMAPANY_ID"] = CarrierId;
            Dt.Rows.Add(dr);
            MaintainLastRow();

            rep.DataSource = Dt;
            rep.DataBind();
        }

        private void MaintainLastRow()
        {
            foreach (RepeaterItem i in rep.Items)
            {


                
           

                   // Label lblDISCOUNT_ID = (Label)rw.FindControl("DISCOUNT_ID");
                  //  TextBox txtPERCENT = (TextBox)rw.FindControl("txtPERCENT");
                int ItemIndex = i.ItemIndex;
                if (Dt.Rows[ItemIndex] != null)
                    {
                        Dt.Rows[ItemIndex].BeginEdit();
                        if ((((System.Web.UI.WebControls.DropDownList)i.FindControl("cmbLEADER_FOLLOWER")).SelectedValue)!= "")
                            Dt.Rows[ItemIndex]["LEADER_FOLLOWER"] = int.Parse((((System.Web.UI.WebControls.DropDownList)i.FindControl("cmbLEADER_FOLLOWER")).SelectedValue));

                        if ((((TextBox)i.FindControl("txtCOINSURANCE_PERCENT")).Text) != "")
                            Dt.Rows[ItemIndex]["COINSURANCE_PERCENT"] = ConvertEbixDoubleValue((((TextBox)i.FindControl("txtCOINSURANCE_PERCENT")).Text));
                        if (((TextBox)i.FindControl("txtCOINSURANCE_FEE")).Text != "")
                            Dt.Rows[ItemIndex]["COINSURANCE_FEE"] = ConvertEbixDoubleValue(((TextBox)i.FindControl("txtCOINSURANCE_FEE")).Text);
                        if (((TextBox)i.FindControl("txtTRANSACTION_ID")).Text != "")
                            Dt.Rows[ItemIndex]["TRANSACTION_ID"] = ((TextBox)i.FindControl("txtTRANSACTION_ID")).Text;
                        if (((TextBox)i.FindControl("txtLEADER_POLICY_NUMBER")).Text!= "")
                            Dt.Rows[ItemIndex]["LEADER_POLICY_NUMBER"] = ((TextBox)i.FindControl("txtLEADER_POLICY_NUMBER")).Text;
                        if (((TextBox)i.FindControl("txtENDORSEMENT_POLICY_NUMBER")).Text != "")
                            Dt.Rows[ItemIndex]["ENDORSEMENT_POLICY_NUMBER"] = ((TextBox)i.FindControl("txtENDORSEMENT_POLICY_NUMBER")).Text;

                        Dt.Rows[ItemIndex].EndEdit();
                        Dt.AcceptChanges();





               

                }

            }

        }

        #endregion

        #region Control Events

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            bool exists = false;
            if (cmbCarrier.SelectedIndex != -1 && cmbCarrier.SelectedIndex > 0)
            {
                int dsrow = Dt.Rows.Count;
                
               
                
                for (int i = 0; i < dsrow; i++)
                {
                    if (Dt.Rows[i]["REIN_COMAPANY_NAME"].ToString() == cmbCarrier.SelectedItem.Text)
                    {
                        
                        exists = true;
                    }
                }
                if (!exists)
                {
                    lblMessage.Text = "";
                    AddGridrow(cmbCarrier.SelectedItem.Text, int.Parse(cmbCarrier.SelectedValue));
                }
                else
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_7"); //"Agency Already Added";
                    lblMessage.Visible = true;
                }
            }
            SetCaptions();
            if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "Policies/support/PageXML/" + GetSystemId(), "AddCoinsurance.xml"))
            {
                setPageControls(Page, @Request.PhysicalApplicationPath + "Policies/support/PageXML/" + GetSystemId() + "/AddCoinsurance.xml");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BtnFlagClick = false;
            int result = 0;
            //ds = new DataTable();
            
            ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
            ArrayList arlObjCoinsurance = new ArrayList();
            ArrayList arlObjoldCoinsurance = new ArrayList();
           
            //Label lblCOINSURANCE_ID = null;
            # region "Comment"

            //for (int i = 0; i < rep.Items.Count; i++)
            //{
            //    objCoInsuranceInfo.COINSURANCE_ID = 0;
            //    //foreach(DataRow obj in ds.Rows)
            //    //{
            //    if (((TextBox)rep.Items[i].Controls[29]).Text != "")
            //    {
            //        objCoInsuranceInfo.COINSURANCE_ID = Convert.ToInt32(((TextBox)rep.Items[i].Controls[29]).Text);
            //    }

            //    //}
            //    //lblCOINSURANCE_ID = (Label)dgCoInsurance.Rows[gr.RowIndex].Cells[6].FindControl("capCOINSURANCE_ID");

            //    //objCoInsuranceInfo.COINSURANCE_ID = int.Parse(lblCOINSURANCE_ID.Text.Trim());
            //    if (cmbCarrier.SelectedIndex != -1 && cmbCarrier.SelectedIndex > 0)
            //    {
            //        Dt = ClsGeneralInformation.GetPolicyCoInsurers(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), int.Parse(cmbCarrier.SelectedValue), objCoInsuranceInfo.COINSURANCE_ID.ToString());
            //    }
            //    else
            //    {
            //        Dt = ClsGeneralInformation.GetPolicyCoInsurers(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), 0, objCoInsuranceInfo.COINSURANCE_ID.ToString());
            //    }

            //    DataSet dsTemp = Dt.DataSet;
            //    ClsCoInsuranceInfo objOldCoInsuranceInfo = new ClsCoInsuranceInfo();
            //    base.PopulateModelObject(objOldCoInsuranceInfo, dsTemp.GetXml());
            //    dsTemp.Dispose();
            //    Dt.Dispose();

            //    if (((TextBox)rep.Items[i].Controls[31]).Text != "")
            //    {
            //        objCoInsuranceInfo.COMPANY_ID = Convert.ToInt32(((TextBox)rep.Items[i].Controls[31]).Text);
            //    }
            //    else
            //    {
            //        objCoInsuranceInfo.COMPANY_ID = Convert.ToInt32(cmbCarrier.SelectedValue.ToString());
            //    }
            //    //objCoInsuranceInfo.CO_INSURER_NAME = ((System.Web.UI.WebControls.TextBox) gr.FindControl("txtCO_INSURER_NAME")).Text;

            //    objCoInsuranceInfo.CO_INSURER_NAME = ((TextBox)(rep.Items[i].Controls[5])).Text;

            //    if (((TextBox)rep.Items[i].Controls[15]).Text != "")
            //    {
            //        objCoInsuranceInfo.COINSURANCE_FEE = double.Parse(((TextBox)rep.Items[i].Controls[15]).Text);
            //    }
            //    else
            //    {
            //        objCoInsuranceInfo.COINSURANCE_FEE = 0;
            //    }

            //    if (((TextBox)rep.Items[i].Controls[9]).Text != "")
            //    {
            //        objCoInsuranceInfo.COINSURANCE_PERCENT = double.Parse(((TextBox)rep.Items[i].Controls[9]).Text);
            //    }
            //    else
            //    {
            //        objCoInsuranceInfo.COINSURANCE_PERCENT = 0;
            //    }

            //    if (((TextBox)rep.Items[i].Controls[19]).Text != "")
            //    {
            //        objCoInsuranceInfo.BROKER_COMMISSION = double.Parse(((TextBox)rep.Items[i].Controls[19]).Text);
            //    }
            //    else
            //    {
            //        objCoInsuranceInfo.BROKER_COMMISSION = 0;
            //    }

            //    objCoInsuranceInfo.LEADER_POLICY_NUMBER = ((TextBox)rep.Items[i].Controls[27]).Text;

            //    objCoInsuranceInfo.LEADER_FOLLOWER = int.Parse((((System.Web.UI.WebControls.DropDownList)rep.Items[i].Controls[7].FindControl("cmbLEADER_FOLLOWER")).SelectedValue));

            //    objCoInsuranceInfo.TRANSACTION_ID = ((TextBox)rep.Items[i].Controls[25]).Text;

            //    ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
            //    if (objCoInsuranceInfo.COINSURANCE_ID == 0)
            //    {
            //        result = objGeneralInformation.SavePolCoInsurers(objCoInsuranceInfo, objOldCoInsuranceInfo, 0);//Save
            //    }
            //    else
            //    {
            //        result = objGeneralInformation.SavePolCoInsurers(objCoInsuranceInfo, objOldCoInsuranceInfo, 1);//Update
            //    }
            //}
            #endregion

            foreach (RepeaterItem i in rep.Items)
            {
                CheckBox chk = (CheckBox)i.FindControl("chkSelect");
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        ClsCoInsuranceInfo objCoInsuranceInfo = new ClsCoInsuranceInfo();
                        objCoInsuranceInfo.CUSTOMER_ID = int.Parse(hidCustomerID.Value);
                        objCoInsuranceInfo.POLICY_ID = int.Parse(hidPolID.Value);
                        objCoInsuranceInfo.POLICY_VERSION_ID = int.Parse(hidPolVersionID.Value);
                        objCoInsuranceInfo.CREATED_BY = int.Parse(GetUserId());
                        objCoInsuranceInfo.CREATED_DATETIME = ConvertToDate(DateTime.Now.ToShortDateString());
                        objCoInsuranceInfo.COINSURANCE_ID = 0;
                        if (((TextBox)i.FindControl("txtCOINSURANCE_ID")).Text != "")
                        { objCoInsuranceInfo.COINSURANCE_ID = Convert.ToInt32(((TextBox)i.FindControl("txtCOINSURANCE_ID")).Text); }

                        //if (cmbCarrier.SelectedIndex != -1 && cmbCarrier.SelectedIndex > 0)
                        //{
                        //    Dt = ClsGeneralInformation.GetPolicyCoInsurers(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), int.Parse(cmbCarrier.SelectedValue), objCoInsuranceInfo.COINSURANCE_ID.ToString());
                        //}
                        //else
                        //{
                        //    Dt = ClsGeneralInformation.GetPolicyCoInsurers(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), 0, objCoInsuranceInfo.COINSURANCE_ID.ToString());
                        //}

                        //DataSet dsTemp = Dt.DataSet;
                        //ClsCoInsuranceInfo objOldCoInsuranceInfo = new ClsCoInsuranceInfo();
                        //base.PopulateModelObject(objOldCoInsuranceInfo, dsTemp.GetXml());
                        //arlObjoldCoinsurance.Add(objOldCoInsuranceInfo);
                        //dsTemp.Dispose();
                        //Dt.Dispose();
                        HiddenField hidCOMPANY_ID = (HiddenField)i.FindControl("hidCOMPANY_ID");

                        if (hidCOMPANY_ID != null && hidCOMPANY_ID.Value != "")
                            objCoInsuranceInfo.COMPANY_ID = int.Parse(hidCOMPANY_ID.Value);
                        //if (((TextBox)i.FindControl("txtREIN_COMAPANY_ID")).Text != "")
                        //{
                        //    objCoInsuranceInfo.COMPANY_ID = Convert.ToInt32(((TextBox)i.FindControl("txtREIN_COMAPANY_ID")).Text);
                        //}
                        //else
                        //{
                        //   // objCoInsuranceInfo.COMPANY_ID = Convert.ToInt32(cmbCarrier.SelectedValue.ToString());

                        //   // objCoInsuranceInfo.COMPANY_ID = int.Parse(hidREIN_COMAPANY_ID.Value);
                            
                        //}

                        objCoInsuranceInfo.CO_INSURER_NAME = ((TextBox)(i.FindControl("txtCO_INSURER_NAME"))).Text;

                        if (((TextBox)i.FindControl("txtCOINSURANCE_FEE")).Text != "")
                        {
                            objCoInsuranceInfo.COINSURANCE_FEE = ConvertEbixDoubleValue(((TextBox)i.FindControl("txtCOINSURANCE_FEE")).Text);
                        }
                        else
                        {
                            objCoInsuranceInfo.COINSURANCE_FEE = 0;
                        }

                        if (((TextBox)i.FindControl("txtCOINSURANCE_PERCENT")).Text != "")
                        {
                            objCoInsuranceInfo.COINSURANCE_PERCENT = ConvertEbixDoubleValue((((TextBox)i.FindControl("txtCOINSURANCE_PERCENT")).Text));
                        }

                        //if (((TextBox)i.FindControl("txtBROKER_COMMISSION")).Text != "")
                        //{
                        //    objCoInsuranceInfo.BROKER_COMMISSION = ConvertEbixDoubleValue(((TextBox)i.FindControl("txtBROKER_COMMISSION")).Text);
                        //}
                        
                        //else
                        //{
                        //    objCoInsuranceInfo.BROKER_COMMISSION = 0;
                        //}


                        if ((((System.Web.UI.WebControls.DropDownList)i.FindControl("cmbLEADER_FOLLOWER")).SelectedValue) != "")
                        {
                            objCoInsuranceInfo.LEADER_FOLLOWER = int.Parse((((System.Web.UI.WebControls.DropDownList)i.FindControl("cmbLEADER_FOLLOWER")).SelectedValue));
                        }
                        if (objCoInsuranceInfo.LEADER_FOLLOWER == 14548)
                        {
                            RequiredFieldValidator rfvCOINSURANCE_PERCENT = (RequiredFieldValidator)i.FindControl("rfvCOINSURANCE_PERCENT");
                            RequiredFieldValidator rfvCOINSURANCE_FEE = (RequiredFieldValidator)i.FindControl("rfvCOINSURANCE_FEE");
                            rfvCOINSURANCE_PERCENT.Enabled = false;                           
                            rfvCOINSURANCE_FEE.Enabled = false;
                        }

                        //else
                       // {
                            objCoInsuranceInfo.LEADER_POLICY_NUMBER = ((TextBox)i.FindControl("txtLEADER_POLICY_NUMBER")).Text;
                          //  RequiredFieldValidator rfvLEADER_POLICY_NUMBER = (RequiredFieldValidator)i.FindControl("rfvLEADER_POLICY_NUMBER");
                          //  rfvLEADER_POLICY_NUMBER.Enabled = false;
                            
                       // }

                        objCoInsuranceInfo.TRANSACTION_ID = ((TextBox)i.FindControl("txtTRANSACTION_ID")).Text;
                        if (((TextBox)i.FindControl("txtENDORSEMENT_POLICY_NUMBER")).Text!="")
                            objCoInsuranceInfo.ENDORSEMENT_POLICY_NUMBER = ((TextBox)i.FindControl("txtENDORSEMENT_POLICY_NUMBER")).Text;
                       
                        if (objCoInsuranceInfo.COINSURANCE_ID == 0)
                        {
                            objCoInsuranceInfo.Action = 0;
                            
                        }
                        else
                        {
                            objCoInsuranceInfo.Action = 1;
                           
                        }
                        arlObjCoinsurance.Add(objCoInsuranceInfo);
                    }
                }
            }

            if (arlObjCoinsurance.Count > 0)
            {
              
               
                result = objGeneralInformation.SavePolCoInsurers(arlObjCoinsurance, hidOldData.Value);//Save
                if (result ==2)
                {
                       
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_5");
                    GridBind();
                    fillcarrier();
                }
                if (result ==1)
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_11");

                }
                if (result == 3)
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_5");
                    GridBind();
                    fillcarrier();
                }

                if (result == 4)
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_14");
                   
                }
               
                if(result == 5)
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_13");

                //if (result == 6)
                //{
                //    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_15");

                //}
            }

           
         //// added by sonal july 27 2010
            base.OpenEndorsementDetails();
            SetCaptions();
            lblMessage.Visible = true;
            if (result <= -1)
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_4");
            }
        }


        //private void SetErrorMessages()
        //{

        //    hidmsg1.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_14");
            
        //}

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            BtnFlagClick = false;
            int result = 0;

            ArrayList arlObjDelCoinsurance = new ArrayList();
            foreach (RepeaterItem i in rep.Items)
            {
                CheckBox chk = (CheckBox)i.FindControl("chkSelect");
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        ClsCoInsuranceInfo objCoInsurancedelInfo = new ClsCoInsuranceInfo();
                        objCoInsurancedelInfo.CUSTOMER_ID = int.Parse(hidCustomerID.Value);
                        objCoInsurancedelInfo.POLICY_ID = int.Parse(hidPolID.Value);
                        objCoInsurancedelInfo.POLICY_VERSION_ID = int.Parse(hidPolVersionID.Value);
                        objCoInsurancedelInfo.CREATED_BY = int.Parse(GetUserId());
                        objCoInsurancedelInfo.COINSURANCE_ID = 0;
                        HiddenField hidCOMPANY_ID = (HiddenField)i.FindControl("hidCOMPANY_ID");

                        if (hidCOMPANY_ID != null && hidCOMPANY_ID.Value != "")
                            objCoInsurancedelInfo.COMPANY_ID = int.Parse(hidCOMPANY_ID.Value);
                        if (((TextBox)i.FindControl("txtCOINSURANCE_ID")).Text != "")
                        {
                            objCoInsurancedelInfo.COINSURANCE_ID = Convert.ToInt32(((TextBox)i.FindControl("txtCOINSURANCE_ID")).Text);
                        }
                        arlObjDelCoinsurance.Add(objCoInsurancedelInfo);
                        //if (lblCOINSURANCE_ID != 0)
                        //{
                        //    result = ClsGeneralInformation.DelPolCoInsurers(lblCOINSURANCE_ID);
                        //}
                    }
                    //else
                    //{
                    //    result = 2;
                    //}

                }
            }
            if (arlObjDelCoinsurance.Count > 0)
            {

                result = ClsGeneralInformation.DelPolCoInsurers(arlObjDelCoinsurance);//Delete
                //if (result > 0)
                //{
                //    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_5");
                //}
            }

            GridBind();
            SetCaptions();
            fillcarrier();// added by sonal july 27 2010
            lblMessage.Visible = true;
            if (result <= 0)
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_1");
            }
            //else if (result == 2)
            //{
            //    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_2");
            //}
            else
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_3");
                base.OpenEndorsementDetails();
            }

        }

        protected void rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemIndex != -1)
            {
                DataRowView dataRowView = e.Item.DataItem as DataRowView;
                if (dataRowView != null)
                {

                    ((RegularExpressionValidator)e.Item.FindControl("revCOINSURANCE_PERCENT")).ValidationExpression = aRegExpDouble;
                    ((RegularExpressionValidator)e.Item.FindControl("revCOINSURANCE_PERCENT")).ErrorMessage = ClsMessages.FetchGeneralMessage("224_25_6");
                    ((RegularExpressionValidator)e.Item.FindControl("revCOINSURANCE_FEE")).ValidationExpression = aRegExpDouble;
                    ((RegularExpressionValidator)e.Item.FindControl("revCOINSURANCE_FEE")).ErrorMessage = ClsMessages.FetchGeneralMessage("224_25_6");
                   // ((RegularExpressionValidator)e.Item.FindControl("revBROKER_COMMISSION")).ValidationExpression = aRegExpDouble;
                  //  ((RegularExpressionValidator)e.Item.FindControl("revBROKER_COMMISSION")).ErrorMessage = ClsMessages.FetchGeneralMessage("224_25_6");
                    ((CustomValidator)e.Item.FindControl("csvCOINSURANCE_PERCENT")).ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_8");
                    ((CustomValidator)e.Item.FindControl("csvCOINSURANCE_FEE")).ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_10");
                 //   ((CustomValidator)e.Item.FindControl("csvBROKER_COMMISSION")).ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("224_25_8");
                   // ((TextBox)e.Item.FindControl("txtBROKER_COMMISSION")).Attributes.Add("onblur", "javascript:this.value=formatRate(this.value)");

                    //TextBox txtBROKER_COMMISSION = ((TextBox)e.Item.FindControl("txtBROKER_COMMISSION"));//Added By Pradeep Kushwaha on 11-Oct-2010
                    //if (txtBROKER_COMMISSION.Text.Trim() != "")
                    //    txtBROKER_COMMISSION.Text = Convert.ToDouble(txtBROKER_COMMISSION.Text).ToString("N", numberFormatInfo);

                    ((TextBox)e.Item.FindControl("txtCOINSURANCE_PERCENT")).Attributes.Add("onblur", "javascript:this.value=formatRate(this.value,2)");

                    TextBox txtCOINSURANCE_PERCENT = ((TextBox)e.Item.FindControl("txtCOINSURANCE_PERCENT"));//Added By Pradeep Kushwaha on 11-Oct-2010
                    numberFormatInfo.NumberDecimalDigits = 2;
                    if (txtCOINSURANCE_PERCENT.Text.Trim() != "")
                        txtCOINSURANCE_PERCENT.Text = Convert.ToDouble(txtCOINSURANCE_PERCENT.Text).ToString("N", numberFormatInfo);

                    ((TextBox)e.Item.FindControl("txtCOINSURANCE_FEE")).Attributes.Add("onblur", "javascript:this.value=formatRate(this.value,2)");

                    TextBox txtCOINSURANCE_FEE = ((TextBox)e.Item.FindControl("txtCOINSURANCE_FEE"));//Added By Pradeep Kushwaha on 11-Oct-2010
                    if (txtCOINSURANCE_FEE.Text.Trim() != "")
                        txtCOINSURANCE_FEE.Text = Convert.ToDouble(txtCOINSURANCE_FEE.Text).ToString("N", numberFormatInfo);



                    ((RequiredFieldValidator)e.Item.FindControl("rfvLEADER_FOLLOWER")).ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "1");
                    ((RequiredFieldValidator)e.Item.FindControl("rfvCOINSURANCE_PERCENT")).ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "2");
                    ((RequiredFieldValidator)e.Item.FindControl("rfvTRANSACTION_ID")).ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "3");
                    ((RequiredFieldValidator)e.Item.FindControl("rfvLEADER_POLICY_NUMBER")).ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "4");
                    ((RequiredFieldValidator)e.Item.FindControl("rfvCOINSURANCE_FEE")).ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "9");
                    ((RegularExpressionValidator)e.Item.FindControl("revTRANSACTION_ID")).ValidationExpression = aRegExpTransactionID;
                    ((RegularExpressionValidator)e.Item.FindControl("revTRANSACTION_ID")).ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "7");
                    ((RegularExpressionValidator)e.Item.FindControl("revLEADER_POLICY_NUMBER")).ValidationExpression = aRegExpAlphaNumStrict;
                    ((RegularExpressionValidator)e.Item.FindControl("revLEADER_POLICY_NUMBER")).ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "5");
                    // changes by praveer for itrack no 1439
                    ((RegularExpressionValidator)e.Item.FindControl("revENDORSEMENT_POLICY_NUMBER")).ValidationExpression = aRegExpAlphaNumStrict;
                    ((RegularExpressionValidator)e.Item.FindControl("revENDORSEMENT_POLICY_NUMBER")).ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "5");
                    ((RequiredFieldValidator)e.Item.FindControl("rfvENDORSEMENT_POLICY_NUMBER")).ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "10");  
                    DropDownList cmbLEADER_FOLLOWER = (DropDownList)e.Item.FindControl("cmbLEADER_FOLLOWER");
                    Label lblREIN_COMAPANY_CODE = (Label)e.Item.FindControl("lblREIN_COMAPANY_CODE");
                    CheckBox chkSelect = (CheckBox)e.Item.FindControl("chkSelect");
                    Label lblLEADER_FOLLOWER = (Label)e.Item.FindControl("lblLEADER_FOLLOWER");
                    TextBox txtLEADER_POLICY_NUMBER = (TextBox)e.Item.FindControl("txtLEADER_POLICY_NUMBER");
                    TextBox txtCOINSURANCE_ID = (TextBox)e.Item.FindControl("txtCOINSURANCE_ID");
                    TextBox txtTRANSACTION_ID = (TextBox)e.Item.FindControl("txtTRANSACTION_ID");
                    TextBox txtENDORSEMENT_POLICY_NUMBER = (TextBox)e.Item.FindControl("txtENDORSEMENT_POLICY_NUMBER");

                    cmbLEADER_FOLLOWER.Attributes.Add("onchange", "javascript:CheckLeader('" + txtLEADER_POLICY_NUMBER.ClientID + "',this.value,'" + cmbLEADER_FOLLOWER.ClientID + "','" + txtCOINSURANCE_ID.Text + "' ,'" + txtTRANSACTION_ID.ClientID + "' );");
                    
                    
                    //for(;i<Dt.Rows.Count;i++)
                    //{
                    //    if (Dt.Rows[i]["LEADER_FOLLOWER"].ToString() != "")
                    //    { ((DropDownList)e.Item.FindControl("cmbLEADER_FOLLOWER")).SelectedIndex = Convert.ToInt32(Dt.Rows[i]["LEADER_FOLLOWER"].ToString()); }
                    //    break;
                    //}
                    if (int.Parse(GetLanguageID())== 2)
                    {
                        cmbLEADER_FOLLOWER.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("COINC");
                        cmbLEADER_FOLLOWER.DataTextField = "LookupDesc1";
                        cmbLEADER_FOLLOWER.DataValueField = "LookupID";
                        cmbLEADER_FOLLOWER.DataBind();
                        cmbLEADER_FOLLOWER.Items.Remove(cmbLEADER_FOLLOWER.Items.FindByValue("14547"));
                        cmbLEADER_FOLLOWER.Items.Insert(0, "");
                    }
                    else
                    {
                        cmbLEADER_FOLLOWER.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("COINC");
                        cmbLEADER_FOLLOWER.DataTextField = "LookupDesc";
                        cmbLEADER_FOLLOWER.DataValueField = "LookupID";
                        cmbLEADER_FOLLOWER.DataBind();
                        cmbLEADER_FOLLOWER.Items.Remove(cmbLEADER_FOLLOWER.Items.FindByValue("14547"));
                        cmbLEADER_FOLLOWER.Items.Insert(0, "");
                    }

                    if (lblLEADER_FOLLOWER.Text.ToString() != "")
                    {
                        if (lblLEADER_FOLLOWER.Text.ToString().Trim() == "14548")
                        {

                            cmbLEADER_FOLLOWER.SelectedIndex = 2;
                            ((RequiredFieldValidator)e.Item.FindControl("rfvCOINSURANCE_PERCENT")).Enabled = false;
                            ((RequiredFieldValidator)e.Item.FindControl("rfvCOINSURANCE_FEE")).Enabled = false;
                            ((RequiredFieldValidator)e.Item.FindControl("rfvLEADER_POLICY_NUMBER")).Enabled = false;
                            ((RequiredFieldValidator)e.Item.FindControl("rfvENDORSEMENT_POLICY_NUMBER")).Enabled = false;
                            ((RequiredFieldValidator)e.Item.FindControl("rfvTRANSACTION_ID")).Enabled = false;
                        }

                        else if (lblLEADER_FOLLOWER.Text.ToString().Trim() == "14549")
                        {
                            cmbLEADER_FOLLOWER.SelectedIndex = 1;

                        }
                        else
                        {
                            cmbLEADER_FOLLOWER.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        cmbLEADER_FOLLOWER.SelectedIndex = 0;
                    }

                    if (lblREIN_COMAPANY_CODE.Text == GetSystemId() && lblLEADER_FOLLOWER.Text.ToString().Trim() == "14548")
                    {
                        cmbLEADER_FOLLOWER.Enabled = false;
                        
                    }
                    //if (lblLEADER_FOLLOWER.Text.ToString().Trim() == "14549" && hidCO_INSURANCE.Value == "14548")
                    //{
                    //    txtLEADER_POLICY_NUMBER.Text = hidPOL_APPNUMBER.Value;
                    //    txtLEADER_POLICY_NUMBER.Enabled = false;
                    //    txtTRANSACTION_ID.Enabled = false;
                        
                    //}
                    
                    if (hidCO_INSURANCE.Value == "14548" )
                    {
                        txtTRANSACTION_ID.Enabled = false;
                        txtLEADER_POLICY_NUMBER.Enabled = false;
                        txtENDORSEMENT_POLICY_NUMBER.Enabled = false;
                        ((RequiredFieldValidator)e.Item.FindControl("rfvENDORSEMENT_POLICY_NUMBER")).Enabled = false;
                        ((RequiredFieldValidator)e.Item.FindControl("rfvTRANSACTION_ID")).Enabled = false;
                        ((RequiredFieldValidator)e.Item.FindControl("rfvLEADER_POLICY_NUMBER")).Enabled = false;
                       
                    }

                    if (hidCO_INSURANCE.Value == "14549" && cmbLEADER_FOLLOWER.SelectedValue== "14548") {
                        txtTRANSACTION_ID.Enabled = false;
                        ((RequiredFieldValidator)e.Item.FindControl("rfvTRANSACTION_ID")).Enabled = false;
                    }
                    if (hidCO_INSURANCE.Value == "14548" && (policystatus == "NORMAL" || policystatus == "UENDRS"))
                    {
                        txtLEADER_POLICY_NUMBER.Text = hidPOL_APPNUMBER.Value;
                        txtENDORSEMENT_POLICY_NUMBER.Text = hidPOL_APPNUMBER.Value;
                    }

                    if (policy_version_id >1)
                    {
                        txtENDORSEMENT_POLICY_NUMBER.Visible = true;
                        ((Label)rep.Controls[0].FindControl("cap_ENDORSEMENT_POLICY_NUMBER")).Visible = true;                       

                    }

                    else
                    {
                        txtENDORSEMENT_POLICY_NUMBER.Visible = false;
                        ((Label)rep.Controls[0].FindControl("cap_ENDORSEMENT_POLICY_NUMBER")).Visible = false;
                        //itrack 865
                        //txtCOINSURANCE_FEE.Width = 100;
                        //txtCOINSURANCE_PERCENT.Width = 100;
                        //txtTRANSACTION_ID.Width = 160;
                        //txtLEADER_POLICY_NUMBER.Width = 160;
                        ((RequiredFieldValidator)e.Item.FindControl("rfvENDORSEMENT_POLICY_NUMBER")).Enabled = false;
                        
                       
                    }
                }
            }

        }

        #endregion



    }
}
