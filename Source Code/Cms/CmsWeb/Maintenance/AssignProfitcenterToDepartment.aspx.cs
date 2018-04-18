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
using Cms.CmsWeb;
using System.Resources;
using System.Reflection;
using Cms.BusinessLayer.BlCommon;
/******************************************************************************************
	<Author					:Priya Arora - >
	<Start Date				: -	>
	<End Date				: - >
	<Description			: - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: May 27, 2005- >
	<Modified By			: Anshuman - >
	<Purpose				: setting screenid according to menuid - >
*******************************************************************************************/

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for AssignProfitcenterToDepartment.
	/// </summary>
	public class AssignProfitcenterToDepartment : Cms.CmsWeb.cmsbase
	{
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPCID;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DropDownList cmbDepartments;
		protected System.Web.UI.WebControls.ListBox lbUnAssignPC;
		protected System.Web.UI.WebControls.ListBox lbAssignPC;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		private Cms.BusinessLayer.BlCommon.ClsDepartment objDepartment;
        protected System.Web.UI.WebControls.Label Header1;
        protected System.Web.UI.WebControls.Label Messages;
        protected System.Web.UI.WebControls.Label Department;
        protected System.Web.UI.WebControls.Label Unassigned;
        protected System.Web.UI.WebControls.Label assigned;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidunassgnPC;
        private string NewscreenId;
        ResourceManager objResourceMgr = null;
        public string Unassignalert = "";
      

		#region page_load()
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "30";
            NewscreenId = "478_1";
			lbUnAssignPC.Attributes.Add("ondblclick","javascript:AssignPC();");
			lbAssignPC.Attributes.Add("ondblclick","javascript:UnAssignPC();");
			
			btnSave.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;
            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AssignProfitcenterToDepartment", System.Reflection.Assembly.GetExecutingAssembly());
			if (!IsPostBack)
			{
				objDepartment=new Cms.BusinessLayer.BlCommon.ClsDepartment();
                Setcaptions();
				try
				{
					DataSet dsRecieve =new DataSet();
					dsRecieve=objDepartment.PopulatePC();
					this.cmbDepartments.DataSource =	dsRecieve.Tables[0];
					this.cmbDepartments.DataValueField = dsRecieve.Tables[0].Columns[0].ToString();
					this.cmbDepartments.DataTextField = dsRecieve.Tables[0].Columns[1].ToString();
					this.cmbDepartments.DataBind();
					this.cmbDepartments.SelectedIndex = 0;
					PopulateUnassignedPC();
					PopulateAssignedPC();
					this.btnSave.Attributes.Add("onClick","javascript:CountAssignPC();");		
					
				}
				catch(Exception ex)
				{
					throw ex;
				}
			}

		}

		#endregion
	

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
			this.cmbDepartments.SelectedIndexChanged += new System.EventHandler(this.cmbDepartments_SelectedIndexChanged);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
        void Setcaptions()
        {
            Header1.Text = objResourceMgr.GetString("Header");
            Department.Text = objResourceMgr.GetString("Department");
            Messages.Text = objResourceMgr.GetString("Messages");
            assigned.Text = objResourceMgr.GetString("lbAssignPC");
            Unassigned.Text = objResourceMgr.GetString("lbUnAssignPC");
            Unassignalert = objResourceMgr.GetString("lblUnassignalert");

        }
       #region cmbDepartments_SelectedIndexChanged()
		private void cmbDepartments_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				PopulateUnassignedPC();
				PopulateAssignedPC();
                Setcaptions();
			}
			catch(Exception ex)
			{
				throw ex;
			}

		}

		#endregion

		#region PopulateUnassignedPC() & PopulateAssignedPC()Function
		private void PopulateUnassignedPC()
		{
			//fill the listbox with all the unassigned Profit Centers
			objDepartment =new Cms.BusinessLayer.BlCommon.ClsDepartment();
			int intDeptID =Convert.ToInt32(this.cmbDepartments.SelectedValue);
			try
			{
				if(Convert.ToInt32(this.cmbDepartments.SelectedIndex) >= 0)
				{
					DataSet dsRecieve =new DataSet();
					dsRecieve=objDepartment.PopulateUnassignedPC(intDeptID);
					this.lbUnAssignPC.DataSource = dsRecieve.Tables[0];
					this.lbUnAssignPC.DataValueField = dsRecieve.Tables[0].Columns[0].ToString();
					this.lbUnAssignPC.DataTextField = dsRecieve.Tables[0].Columns[1].ToString();
					this.lbUnAssignPC.DataBind();
                    this.lbUnAssignPC.Items.Insert(0, (ClsCommon.BL_LANG_ID == 2 ? ">>... Selecione Centro de Custo" : ">>...Select Profit Centers"));
					this.lbUnAssignPC.SelectedIndex =0;
				}
			}
                	
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private void PopulateAssignedPC()
		{
			//fill the listbox with all the Assigned Profit Centers
			objDepartment =new Cms.BusinessLayer.BlCommon.ClsDepartment();
			
			int intDeptID = Convert.ToInt32(this.cmbDepartments.SelectedValue);
			try
			{
				if(Convert.ToInt32(this.cmbDepartments.SelectedIndex)>=0)
				{
					DataSet dsRecieve =new DataSet();
                   
					dsRecieve=objDepartment.PopulateassignedPC(intDeptID);
					this.lbAssignPC.DataSource = dsRecieve.Tables[0];
					this.lbAssignPC.DataValueField = dsRecieve.Tables[0].Columns[0].ToString();
					this.lbAssignPC.DataTextField = dsRecieve.Tables[0].Columns[1].ToString();
					this.lbAssignPC.DataBind();
                    this.lbAssignPC.Items.Insert(0, (ClsCommon.BL_LANG_ID == 2 ? ">>... Selecione Centro de Custo" : ">>...Select Profit Centers"));
					this.lbAssignPC.SelectedIndex =0;

                   
                }
                DataSet dsassignPC = new DataSet();
                string AssignPC = "";

                dsassignPC = objDepartment.PopulateassignedPC(intDeptID);
                if (dsassignPC != null && dsassignPC.Tables.Count > 0)
                {
                    foreach (DataRow dr in dsassignPC.Tables[1].Rows)
                    {
                        if (dr["PC_ID"] != null)
                        {
                            AssignPC = AssignPC + dr["PC_ID"] + ",";
                        }
                    }



                }
                if (AssignPC != "")
                {
                    AssignPC = AssignPC.Substring(0, AssignPC.Length - 1);
                }
                hidunassgnPC.Value = AssignPC; 
			}
			catch(Exception ex)
			{
				throw ex;
			}	
		}
          
		#endregion

		#region btnSave_Click()
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			objDepartment=new Cms.BusinessLayer.BlCommon.ClsDepartment();
			int intDeptID ;
			intDeptID = Convert.ToInt32(this.cmbDepartments.SelectedValue);
			string DeptDesc = cmbDepartments.Items[cmbDepartments.SelectedIndex].Text;
			char[] arrDelimeter={','};
			string [] arrPCId ;
			arrPCId = hidPCID.Value.ToString().Split(',');
			int i;
			int[] intPC = new int[arrPCId.Length];
			for (i=1;i<arrPCId.Length;i++)
			{
				intPC[i] = int.Parse(arrPCId[i]);
			}
			
				int intResult;			
				intResult = objDepartment.Save(intDeptID,intPC);		
				if(intResult >= 1)
				{
                    this.lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1369");//"Profit Centers have been successfully assigned/unassigned to Departments.";

				}
				else
				{
                    this.lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1370"); // "Unable to assigned/unassigned Profit Centers to Departments.";			
				}
				this.lblMessage.Visible=true;

				PopulateUnassignedPC();
				PopulateAssignedPC();
                Setcaptions();
			Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGen = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
			//string trans_desc ="Profit Centers have been assigned/unassigned to Departments.";
            string trans_desc = ClsMessages.GetMessage(NewscreenId, "6");
            string trans_custom = ";" + ClsMessages.GetMessage(NewscreenId, "14") + DeptDesc+";";
            trans_custom += ";" + ClsMessages.GetMessage(NewscreenId, "15")+":;";
			for(int unasgnindex =1; unasgnindex< lbUnAssignPC.Items.Count; unasgnindex++)
			{
				if(unasgnindex > 1)
					trans_custom += ", " + lbUnAssignPC.Items[unasgnindex].Text;
				else
					trans_custom += lbUnAssignPC.Items[unasgnindex].Text;	
			}

            trans_custom += ";" + ClsMessages.GetMessage(NewscreenId, "16") + ":;";
			for(int asgnindex =1; asgnindex< lbAssignPC.Items.Count; asgnindex++)
			{
				if(asgnindex > 1)
					trans_custom += ", " + lbAssignPC.Items[asgnindex].Text;
				else
					trans_custom += lbAssignPC.Items[asgnindex].Text;	
			}
			objGen.WriteTransactionLog(0, 0, 0, trans_desc, int.Parse(GetUserId()),trans_custom, "Application");

			}

			#endregion

		}
	}



