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
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Maintenance;
using Cms.Model.Diary;
using System.Resources;
using System.Reflection;
using System.Text.RegularExpressions;


namespace Cms.CmsWeb.Maintenance
{
    /// <summary>
    /// Summary description for DiarySetup.
    /// </summary>
    public class DiarySetup : Cms.CmsWeb.cmsbase
    {
        #region CONTROL DECLARATION
        protected System.Web.UI.HtmlControls.HtmlForm Form1;
        protected System.Web.UI.HtmlControls.HtmlTableCell trMessage;
        protected System.Web.UI.WebControls.DropDownList cmbModuleName;
        protected System.Web.UI.WebControls.DropDownList cmbDiaryType;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFlag;
        protected System.Web.UI.WebControls.Label capModuleName;
        protected System.Web.UI.WebControls.Label capDiaryListType;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB;
        protected System.Web.UI.WebControls.Label lblMess;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDiaryID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckFlag;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidmsg1;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidmsg2;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidmsg3;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidmsg4;
        #endregion

        #region VALRIABLE DECLARATION
        ClsDiary objDiary = new ClsDiary();
        DataSet dsModule = new DataSet();
        DataSet dsModuleName = new DataSet();
        string mName = "", dName = "";
        int mID = 0, dID = 0;
        int LOBFlag = 0;
        static int maxlob_id;
        private System.Resources.ResourceManager objResourceMgr;

        #endregion

        private void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page
            base.ScreenId = "397_0";
            btnSave.Attributes.Add("onclick", "return checkMandatory();");

            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.DiarySetup", Assembly.GetExecutingAssembly());
            SetCaptions();

            if (!Page.IsPostBack)
            {
                dsModule = objDiary.GetModuleDiaryType();
                ClsDiary.GetModule(cmbModuleName, "D");
            }



            btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;
        }

        private void SetCaptions()
        {
            hidmsg1.Value = objResourceMgr.GetString("lblmsg1");
            hidmsg2.Value = objResourceMgr.GetString("lblmsg2");
            hidmsg3.Value = objResourceMgr.GetString("lblmsg3");
            hidmsg4.Value = objResourceMgr.GetString("lblmsg4");
            capModuleName.Text = objResourceMgr.GetString("capModuleName");
            capDiaryListType.Text = objResourceMgr.GetString("capDiaryListType");
        }
        private void MakeTablewithValues(int mID, int dID, string flag)
        {
            dsModuleName = objDiary.GetModuleDetails(mID, dID);
            if (dsModuleName != null)
            {
                if (dsModuleName.Tables.Count > 0)
                {
                    if (dsModuleName.Tables[0].Rows.Count > 0)
                    {
                        if (dsModuleName.Tables[0].Rows[0]["MDD_LOB_ID"].ToString() != "")
                        {
                            CreateTable(flag);
                        }
                    }
                    else
                        CreateTable(flag);
                }
                else
                    CreateTable(flag);

            }
            else
                CreateTable(flag);
        }


        private void CreateTable(string flag)
        {
            if (mName != "")
            {
                HtmlTable hTable = new HtmlTable();
                //hTable.Width = Unit.Percentage(100).ToString() ; 
                //hTable.Align= "Center";
                //hTable.Border=1; 
                hTable.ID = "hTable";
                hTable.Attributes.Add("class", "tableWidthheader");

                hTable.Rows.Add(MakeTextRow());
                int iCnt = 0;
                int count = 0;

                if (flag.Equals("Y"))
                {
                    count = 7;
                    hidFlag.Value = flag;
                }
                else
                {
                    count = 1;
                    hidFlag.Value = flag;
                }
                DataSet dsLOB = new DataSet();
                ClsStates objState = new ClsStates();
                dsLOB = objState.PoplateLob();

                string strDiaryID = "";
                if (flag.Equals("Y"))
                {
                    if (dsLOB != null)
                    {
                        if (dsLOB.Tables.Count > 0)
                        {
                             
                           maxlob_id = Convert.ToInt32(dsLOB.Tables[0].Compute("max(LOB_ID)", string.Empty));

                            
                            for (iCnt = 0; iCnt < dsLOB.Tables[0].Rows.Count; iCnt++)
                            {
                                strDiaryID += dsLOB.Tables[0].Rows[iCnt]["LOB_ID"].ToString() + ",";
                                hTable.Rows.Add(MakePanel(iCnt, flag, dsLOB.Tables[0].Rows[iCnt]["LOB_ID"].ToString(), dsLOB.Tables[0].Rows[iCnt]["LOB_DESC"].ToString()));
                            }
                        }
                    }
                }
                else
                    hTable.Rows.Add(MakePanel(iCnt, flag, "1", ""));

                if (strDiaryID.LastIndexOf(",") != -1)
                {
                    strDiaryID = strDiaryID.Substring(0, strDiaryID.Length - 1);
                }

                hidDiaryID.Value = strDiaryID;

                trMessage.Controls.Add(hTable);

            }
        }

        /// <summary>
        /// Creating row for diaplaying control description
        /// </summary>
        /// <returns>HTML row with control description</returns>
        private HtmlTableRow MakeTextRow()
        {
            HtmlTableRow hRowtext = new HtmlTableRow();
            HtmlTableCell hCelltext1 = new HtmlTableCell();
            HtmlTableCell hCelltext2 = new HtmlTableCell();
            HtmlTableCell hCelltext3 = new HtmlTableCell();
            //HtmlTableCell hCelltext4=new HtmlTableCell();
            HtmlTableCell hCelltext5 = new HtmlTableCell();
            HtmlTableCell hCelltext6 = new HtmlTableCell();
            HtmlTableCell hCelltext7 = new HtmlTableCell();



            hCelltext7.InnerHtml = "<b>" + objResourceMgr.GetString("lblLOB") + "</b>";
            //hCelltext7.InnerHtml ="<b>LOB</b>"; 

            hCelltext7.Attributes.Add("class", "midcolora");

            hCelltext1.InnerHtml = "<b>" + objResourceMgr.GetString("lblSubject") + "</b>";
            //hCelltext1.InnerHtml="<b>Subject</b>"; 

            hCelltext1.Attributes.Add("class", "midcolora");
            hCelltext2.InnerHtml = "<b>" + objResourceMgr.GetString("lblFollowUp") + "</b>";
            //hCelltext2.InnerHtml="<b>Follow up</b>";

            hCelltext2.Attributes.Add("class", "midcolora");
            hCelltext3.InnerHtml = "<b>" + objResourceMgr.GetString("lblPriority") + "</b>";
            //hCelltext3.InnerHtml="<b>Priority</b>";

            hCelltext3.Attributes.Add("class", "midcolora");
            //hCelltext4.InnerHtml="<b>Notification List</b>";
            //	hCelltext4.Attributes.Add("class","midcolora");

            hCelltext5.InnerHtml = "<b>" + objResourceMgr.GetString("lblUserGroupList") + "</b>";
            //hCelltext5.InnerHtml="<b>User Group List</b>";

            hCelltext5.Attributes.Add("class", "midcolora");
            hCelltext6.InnerHtml = "<b>" + objResourceMgr.GetString("lblUserList") + "</b>";
            //hCelltext6.InnerHtml="<b>User List</b>";

            hCelltext6.Attributes.Add("class", "midcolora");

            hRowtext.Cells.Add(hCelltext7);
            hRowtext.Cells.Add(hCelltext1);
            hRowtext.Cells.Add(hCelltext2);
            hRowtext.Cells.Add(hCelltext3);
            //hRowtext.Cells.Add(hCelltext4)  ;
            hRowtext.Cells.Add(hCelltext5);
            hRowtext.Cells.Add(hCelltext6);

            return hRowtext;

        }

        /// <summary>
        /// Creating control rows and populating combo and list boxes for every diary type
        /// </summary>
        /// <param name="rowCnt">for creating control ID</param>
        /// <param name="moduleId">to be stored within hidden variable</param>
        /// <param name="diaryTypeId">to be stored within hidden variable</param>
        /// <returns>HTML row containing populated controls</returns>
        private HtmlTableRow MakePanel(int rowCnt, string flag, string lobID, string lobDesc)
        {

            HtmlTableRow pRow = new HtmlTableRow();
            HtmlTableCell hCellCtr1 = new HtmlTableCell();
            HtmlTableCell hCellCtr2 = new HtmlTableCell();
            HtmlTableCell hCellCtr3 = new HtmlTableCell();
            //HtmlTableCell hCellCtr4=new HtmlTableCell();
            HtmlTableCell hCellCtr5 = new HtmlTableCell();
            HtmlTableCell hCellCtr6 = new HtmlTableCell();
            HtmlTableCell hCellCtr7 = new HtmlTableCell();


            Label lbBlank = new Label();
            Label lbLOB = new Label();
            CheckBox ckType = new CheckBox();
            HtmlInputHidden hidID = new HtmlInputHidden();
            TextBox txSubject = new TextBox();
            TextBox txFollow = new TextBox();
            DropDownList cmbPriority = new DropDownList();
            //DropDownList cmbNotification=new DropDownList();
            ListBox cmbUserGroup = new ListBox();
            ListBox cmbUserList = new ListBox();


            pRow.ID = "pRow_" + (lobID);
            hidID.ID = "hidLOB_" + (lobID);
            ckType.ID = "ckType_" + (lobID);
            lbBlank.ID = "lbBlank_" + (lobID);
            txSubject.ID = "txSubject_" + (lobID);
            txFollow.ID = "txFollow_" + (lobID);
            cmbPriority.ID = "cmbPriotiry_" + (lobID);
            //cmbNotification.ID = "cmbNotification_" +  (rowCnt+1);
            cmbUserGroup.ID = "cmbUserGroup_" + (lobID);
            cmbUserList.ID = "cmbUserList_" + (lobID);


            ckType.Checked = false;
            if (!lobID.Equals(""))
            {
                ckType.Text = lobDesc;
                hidID.Value = lobID;
            }

            lbBlank.Text = objResourceMgr.GetString("lblAllLOB");// "All LOB";			
            txSubject.TextMode = TextBoxMode.MultiLine;
            txSubject.Rows = 3;
            txSubject.Columns = 25;

            txFollow.MaxLength = 2;
            txFollow.Width = Unit.Pixel(50);

            cmbPriority.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PRIOR");
            cmbPriority.DataTextField = "LookupDesc";
            cmbPriority.DataValueField = "LookupID";
            cmbPriority.DataBind();


            //cmbPriority.Items.Add(new ListItem("Low","L"));
            //cmbPriority.Items.Add(new ListItem("Medium","M"));
            //cmbPriority.Items.Add(new ListItem("High","H"));
            //cmbPriority.Items.Insert(0,"");
            cmbUserGroup.Height = Unit.Pixel(50);
            cmbUserList.Height = Unit.Pixel(50);

            cmbUserGroup.SelectionMode = ListSelectionMode.Multiple;
            ClsUser.GetUserTypeDropDown(cmbUserGroup);
            //cmbUserGroup.Items.Insert(0, objResourceMgr.GetString("lblSelectUserGroups"));// "Select User Groups");
            cmbUserGroup.Items.Insert(0, (ClsCommon.BL_LANG_ID == 2 ? "User Groups Escolha" : "Select User Groups"));  //"Select User Groups");
            cmbUserGroup.Items[0].Value = "";

            cmbUserList.SelectionMode = ListSelectionMode.Multiple;
            cmbUserList.DataSource = ClsCommon.GetUserListForDiarySetup();
            cmbUserList.DataTextField = USERNAME;
            cmbUserList.DataValueField = USERID;
            cmbUserList.DataBind();

            //cmbUserList.Items.Insert(0, objResourceMgr.GetString("lblSelectUsers"));//"Select Users");

            cmbUserList.Items.Insert(0, (ClsCommon.BL_LANG_ID == 2 ? "Selecionar Usuários" : "Select Users")); //"Select Users");
            cmbUserList.Items[0].Value = "";


            if (dsModuleName != null)
            {
                if (dsModuleName.Tables.Count > 0)
                    if (dsModuleName.Tables[0].Rows.Count > 0)
                        //if(dsModuleName.Tables[0].Rows[rowCnt][0].ToString() !="" && dsModuleName.Tables[0].Rows[rowCnt][0].ToString()!="-1")
                        if (lobDesc == "")
                            PopulateControls(ref ckType, ref hidID, ref txSubject, ref cmbPriority, ref cmbUserGroup, ref cmbUserList, ref txFollow, -1);
                        else
                            PopulateControls(ref ckType, ref hidID, ref txSubject, ref cmbPriority, ref cmbUserGroup, ref cmbUserList, ref txFollow, int.Parse(lobID));

            }

            hCellCtr1.Attributes.Add("class", "midcolora");
            hCellCtr2.Attributes.Add("class", "midcolora");
            hCellCtr3.Attributes.Add("class", "midcolora");
            //hCellCtr4.Attributes.Add("class","midcolora");
            hCellCtr5.Attributes.Add("class", "midcolora");
            hCellCtr6.Attributes.Add("class", "midcolora");
            hCellCtr7.Attributes.Add("class", "midcolora");

            hCellCtr1.Controls.Add(txSubject);
            hCellCtr2.Controls.Add(txFollow);
            hCellCtr3.Controls.Add(cmbPriority);
            //hCellCtr4.Controls.Add(cmbNotification);
            hCellCtr5.Controls.Add(cmbUserGroup);
            hCellCtr6.Controls.Add(cmbUserList);
            if (!flag.Equals("Y"))
            {
                hCellCtr7.Controls.Add(lbBlank);

            }
            else
            {
                hCellCtr7.Controls.Add(ckType);
                hCellCtr7.Controls.Add(hidID);
            }


            //hCellCtr7.Controls.Add(hidID);

            pRow.Cells.Add(hCellCtr7);
            pRow.Cells.Add(hCellCtr1);
            pRow.Cells.Add(hCellCtr2);
            pRow.Cells.Add(hCellCtr3);
            //pRow.Cells.Add(hCellCtr4)  ;
            pRow.Cells.Add(hCellCtr5);
            pRow.Cells.Add(hCellCtr6);


            return pRow;
        }


        private void PopulateControls(ref CheckBox ckType, ref HtmlInputHidden hidID, ref TextBox txSubject, ref DropDownList cmbPriority, ref ListBox cmbUserGroup, ref ListBox cmbUserList, ref TextBox txFollow, int rowCnt)
        {
            string[] strGrpArray = { };
            string[] strUserArray = { };
            int iCnt = 0;

            string strLobID = "";
            string strUserGrp = "";
            string strUserList = "";
            string strFollowDt = "";
            string strSubject = "";
            string strPriority = "";

            DataRow[] dRow = dsModuleName.Tables[0].Select("MDD_LOB_ID=" + rowCnt);

            if (dRow != null)
            {
                if (dRow.Length > 0)
                {
                    strLobID = dRow[0]["MDD_LOB_ID"].ToString();
                    strUserGrp = dRow[0]["MDD_USERGROUP_ID"].ToString();
                    strUserList = dRow[0]["MDD_USERLIST_ID"].ToString();
                    strFollowDt = dRow[0]["MDD_FOLLOWUP"].ToString();
                    strSubject = dRow[0]["MDD_SUBJECTLINE"].ToString();
                    strPriority = dRow[0]["MDD_PRIORITY"].ToString();
                    LOBFlag++;
                }
                else
                    return;

            }
            else
                return;

            //string strNotification=dsModuleName.Tables[0].Rows[rowCnt]["MDD_NOTIFICATION_LIST"].ToString();  


            if (dsModuleName != null)
            {
                if (dsModuleName.Tables.Count > 0)
                    if (dsModuleName.Tables[0].Rows.Count > 0)
                    {
                        if (!strLobID.Equals("0"))
                        {
                            ckType.Checked = true;
                            hidCheckFlag.Value = LOBFlag.ToString();
                        }

                        if (strLobID != "0")
                        {
                            if (!strUserGrp.Equals(""))
                                strGrpArray = strUserGrp.Split(new char[] { ',' });


                            if (strGrpArray.Length > 0)
                            {
                                for (iCnt = 0; iCnt < strGrpArray.Length; iCnt++)
                                {
                                    if (strGrpArray[iCnt] != "")
                                    {
                                        ListItem liGrp = new ListItem();
                                        liGrp = cmbUserGroup.Items.FindByValue(strGrpArray[iCnt]);
                                        if (liGrp != null)
                                            liGrp.Selected = true;
                                    }
                                }

                            }


                            if (!strUserList.Equals(""))
                                strUserArray = strUserList.Split(new char[] { ',' });


                            if (strUserArray.Length > 0)
                            {
                                for (iCnt = 0; iCnt < strUserArray.Length; iCnt++)
                                {
                                    if (strUserArray[iCnt] != "")
                                    {
                                        ListItem liUser = new ListItem();
                                        liUser = cmbUserList.Items.FindByValue(strUserArray[iCnt]);
                                        if (liUser != null)
                                            liUser.Selected = true;
                                    }
                                }
                            }


                            if (ckType.Checked)
                                if (!strFollowDt.Equals("0"))  //Modified by Asfa Praveen (04-Feb-2008) - iTrack issue #3526 Part 1.
                                    txFollow.Text = strFollowDt;


                            if (!strSubject.Equals(""))
                            {
                                txSubject.Text = strSubject;
                            }


                            if (!strPriority.Trim().Equals(""))
                            {
                                ListItem li = new ListItem();
                                li = cmbPriority.Items.FindByValue(strPriority.Trim());
                                if (li != null)
                                    li.Selected = true;
                            }

                        }
                    }
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
            this.cmbModuleName.SelectedIndexChanged += new System.EventHandler(this.cmbModuleName_SelectedIndexChanged);
            this.cmbDiaryType.SelectedIndexChanged += new System.EventHandler(this.cmbDiaryType_SelectedIndexChanged);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        #region "Web Event Handlers"
        /// <summary>
        /// If form is posted back then add entry in database using the BL object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            lblMess.Visible = true;
            int count = 0, iCnt = 0;
            TodolistInfo objDefModelInfo = new TodolistInfo();

            if (hidFlag.Value.Equals("Y"))
            {
                count = hidLOB.Value == "" ? -1 : int.Parse(hidLOB.Value);
            }
            else
            {
                count = 1;
            }

            objDiary.DeleteDiarySetup(int.Parse(cmbModuleName.SelectedItem.Value), int.Parse(cmbDiaryType.SelectedItem.Value));

            count = maxlob_id;
            for (iCnt = 0; iCnt < count; iCnt++)
            {
               // count = int.Parse(hidLOB.Value);
                objDefModelInfo = GetFormValue(iCnt);
                int result = objDiary.AddDiarySetup(objDefModelInfo);

                if (result > 0)
                {
                    lblMess.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                }
                else
                {
                    lblMess.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                }
            }

            mName = cmbModuleName.SelectedItem.Text;
            dName = cmbDiaryType.SelectedItem.Text;


            MakeTablewithValues(int.Parse(cmbModuleName.SelectedItem.Value), int.Parse(cmbDiaryType.SelectedItem.Value), hidFlag.Value);
        }


        #region Method code to do form's processing

        /// <summary>
        /// Fetch form's value and stores into variables.
        /// </summary>
        private TodolistInfo GetFormValue(int rowCnt)
        {

            TodolistInfo objModelInfo = new TodolistInfo();
            CheckBox ck = new CheckBox();
            HtmlInputHidden hidId = new HtmlInputHidden();
            ListBox luGrpBx = new ListBox();
            ListBox luLstBx = new ListBox();
            TextBox tSubBox = new TextBox();
            TextBox tFolBox = new TextBox();
            //DropDownList ddlNot=new DropDownList(); 
            DropDownList ddlPri = new DropDownList();

            ck.ID = "ckType_" + (rowCnt + 1);
            tSubBox.ID = "txSubject_" + (rowCnt + 1);
            tFolBox.ID = "txFollow_" + (rowCnt + 1);
            //ddlNot.ID="cmbNotification_" + (rowCnt+1);
            ddlPri.ID = "cmbPriotiry_" + (rowCnt + 1);
            luGrpBx.ID = "cmbUserGroup_" + (rowCnt + 1);
            luLstBx.ID = "cmbUserList_" + (rowCnt + 1);
            hidId.ID = "hidLOB_" + (rowCnt + 1);

            //Modified by Asfa Praveen (04-Feb-2008) - iTrack issue #3526 Part 1.

            //Added By Pradeep Kushwaha on 22-07-2010 

            Regex objNotWholePattern = new Regex("[0-9]");
            if (Request.Form[tFolBox.ID] != null)
                if (objNotWholePattern.IsMatch(Request.Form[tFolBox.ID].ToString()))
                {
                    objModelInfo.FOLLOW_UP = Request.Form[tFolBox.ID].ToString() == "" ? 0 : int.Parse(Request.Form[tFolBox.ID].ToString()); ;
                }
                else
                {
                    objModelInfo.FOLLOW_UP = 0;
                }
            else
                objModelInfo.FOLLOW_UP = 0;
            //added till here 

            objModelInfo.LISTTYPEID = cmbDiaryType.SelectedItem.Value == "" ? -1 : int.Parse(cmbDiaryType.SelectedItem.Value); ;

            if (Request.Form[ddlPri.ID] != null)
                objModelInfo.PRIORITY = Request.Form[ddlPri.ID].ToString();
            if (Request.Form[tSubBox.ID] != null)
            objModelInfo.SUBJECTLINE = Request.Form[tSubBox.ID].ToString();

            //			if(Request.Form[ddlNot.ID]!=null)
            //				objModelInfo.SYSTEMFOLLOWUPID   =   Request.Form[ddlNot.ID].ToString() =="" ? -1 : int.Parse(Request.Form[ddlNot.ID].ToString() );            
            objModelInfo.LISTOPEN = "Y";
            objModelInfo.MODULE_ID = cmbModuleName.SelectedItem.Value == "" ? -1 : int.Parse(cmbModuleName.SelectedItem.Value); ;

            if (Request.Form[luGrpBx.ID] != null)
                objModelInfo.USERGROUP_ID = Request.Form[luGrpBx.ID].ToString();

            if (Request.Form[luLstBx.ID] != null)            {
                objModelInfo.USERLIST_ID = Request.Form[luLstBx.ID].ToString();
                //				try
                //				{
                //					objModelInfo.TOUSERID = long.Parse(Request.Form[luLstBx.ID].ToString());	
                //				}
                //				catch(Exception ex)
                //				{}
            }

            if (hidFlag.Value.Equals("Y"))
            {
                if (Request.Form[ck.ID] != null)
                    if (Request.Form[ck.ID].ToString().ToLower() == "on")
                        objModelInfo.LOB_ID = Request.Form[hidId.ID].ToString() == "" ? 0 : int.Parse(Request.Form[hidId.ID].ToString());
                    else
                        objModelInfo.LOB_ID = 0;
            }
            else
            {
                objModelInfo.LOB_ID = -1;
            }
            objModelInfo.CREATED_BY = int.Parse(GetUserId());
            //objModelInfo.FROMUSERID = int.Parse(GetUserId());


            return objModelInfo;
        }
        #endregion

        private void cmbDiaryType_SelectedIndexChanged(object sender, System.EventArgs e)
        {

            lblMess.Visible = false;
            mID = cmbModuleName.SelectedItem.Value == "" ? 0 : int.Parse(cmbModuleName.SelectedItem.Value);
            dID = cmbDiaryType.SelectedItem.Value == "" ? 0 : int.Parse(cmbDiaryType.SelectedItem.Value);

            mName = cmbModuleName.SelectedItem.Text;
            dName = cmbDiaryType.SelectedItem.Text;

            switch (mID.ToString())
            {
                case "1":
                case "2":
                case "3":
                case "5":
                    MakeTablewithValues(mID, dID, "Y");
                    break;
                case "4":
                case "6":
                    MakeTablewithValues(mID, dID, "N");
                    break;
                case "-1":
                    MakeTablewithValues(mID, dID, "Y");
                    break;
                case "-2":
                    MakeTablewithValues(mID, dID, "N");
                    break;
            }
        }


        private void cmbModuleName_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            lblMess.Visible = false;
            string modID = cmbModuleName.SelectedItem.Value;
            mName = cmbModuleName.SelectedItem.Text;


            if (!modID.Equals("Select Option"))
            {
                DataSet dsDt = ClsDiary.GetDiaryByModule(modID, "D");

                cmbDiaryType.DataSource = dsDt;
                cmbDiaryType.DataValueField = TYPEID;
                cmbDiaryType.DataTextField = TYPEDESC;
                cmbDiaryType.DataBind();
                cmbDiaryType.Items.Insert(0, (ClsCommon.BL_LANG_ID == 2 ? "Selecione todos os tipos de Agendas" : "Select All Diary Type")); //"Select All Diary Type");
                //cmbDiaryType.Items.Insert(0, objResourceMgr.GetString("lblSelectAllDiaryType"));//"Select All Diary Type");

                dName = objResourceMgr.GetString("lblAllDiaryType");//"All Diary Type";
                switch (modID.ToString())
                {
                    case "1":
                    case "2":
                    case "3":
                    case "5":
                        cmbDiaryType.Items[0].Value = "-1";
                        MakeTablewithValues(int.Parse(modID), -1, "Y");
                        break;
                    case "4":
                    case "6":
                        cmbDiaryType.Items[0].Value = "-2";
                        MakeTablewithValues(int.Parse(modID), -2, "N");
                        break;
                }
            }
            else
            {
                cmbDiaryType.Items.Clear();
            }
        }


        #endregion
    }
}

