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
	<Author					: Ashwani Kumar- >
	<Start Date				: -	>
	<End Date				: - >
	<Description			: - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: May 27, 2005- >
	<Modified By			: Anshuman - >
	<Purpose				: updating screenid according to menuid - >
*******************************************************************************************/

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for AssignDepartmentToDivision.
	/// </summary>
	public class AssignDepartmentToDivision : Cms.CmsWeb.cmsbase
	{

        protected System.Web.UI.WebControls.Label Message;
        protected System.Web.UI.WebControls.Label header;
        protected System.Web.UI.WebControls.Label lblUnassigned;
        protected System.Web.UI.WebControls.Label lblAssigned;
        protected System.Web.UI.WebControls.Label lblDivision;
		protected System.Web.UI.WebControls.DropDownList cmbDivisions;
		protected System.Web.UI.WebControls.ListBox lbAssignDept;
		protected System.Web.UI.WebControls.ListBox lbUnAssignDept;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDeptID;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlForm AssignDivDept;
		private Cms.BusinessLayer.BlCommon.ClsDivision objDivision;
        private string NewscreenId;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidunassgndept;
        ResourceManager objResourceMgr = null;
        public string Unassignalert = "";
       
		#region Page_Load()
		/// <summary>
		/// Put user code to initialize the page here
		/// </summary>
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "24";
            NewscreenId = "478_1";
			lbUnAssignDept.Attributes.Add("ondblclick","javascript:AssignDepts();");
			lbAssignDept.Attributes.Add("ondblclick","javascript:UnAssignDepts();");
			btnSave.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;
            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AssignDepartmentToDivision", System.Reflection.Assembly.GetExecutingAssembly());
            Unassignalert = objResourceMgr.GetString("lblUnassignalert");
			if (!IsPostBack)
			{
                Setcaption();
				DataSet dsRecieve;
				try
				{
					dsRecieve =new DataSet();
					objDivision =new Cms.BusinessLayer.BlCommon.ClsDivision();
					dsRecieve=objDivision.PopulateDivision();
					
					this.cmbDivisions.DataSource =	dsRecieve.Tables[0];
					this.cmbDivisions.DataValueField = dsRecieve.Tables[0].Columns[0].ToString();
					this.cmbDivisions.DataTextField = dsRecieve.Tables[0].Columns[1].ToString();
					this.cmbDivisions.DataBind();
					this.cmbDivisions.Items.Insert(0,"");
					this.cmbDivisions.SelectedIndex = 0;
					PopulateUnassignedDepartment();
					PopulateAssignedDepartment();
					this.btnSave.Attributes.Add("onClick","javascript:CountAssignDepts();");					
				}
				catch(Exception objExcep)
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
				}
			}
		}

		#endregion
        void Setcaption()
        {
            lblDivision.Text = objResourceMgr.GetString("cmbDivisions");
            lblAssigned.Text = objResourceMgr.GetString("lbAssignDept");
            lblUnassigned.Text = objResourceMgr.GetString("lbUnAssignDept");
            Message.Text = objResourceMgr.GetString("Message");
            header.Text = objResourceMgr.GetString("header");
            Unassignalert = objResourceMgr.GetString("lblUnassignalert");
           
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
			this.cmbDivisions.SelectedIndexChanged += new System.EventHandler(this.cmbDivisions_SelectedIndexChanged);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion	

		#region cmbDivisions_SelectedIndexChanged()

		/// <summary>
		/// Populate the  assigned listbox and unassigned unassigned 
		/// listbox on the bases of division selected.
		/// </summary>
	
		private void cmbDivisions_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				PopulateUnassignedDepartment();
				PopulateAssignedDepartment();
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
		}

		#endregion

		#region PopulateUnassignedDepartment() & PopulateAssignedDepartment()Function
		
		/// <summary>
		/// Fill the listbox with all the unassigned departments
		/// </summary>
		
		private void PopulateUnassignedDepartment()
		{
			
			
			int intDivisionID =0 ;
			DataSet dsRecieve ;
			try
			{
				objDivision =new Cms.BusinessLayer.BlCommon.ClsDivision();

				if(Convert.ToInt32(this.cmbDivisions.SelectedIndex) >= 0)
				{
					if(this.cmbDivisions.SelectedValue != "")
						intDivisionID = Convert.ToInt32(this.cmbDivisions.SelectedValue);
					dsRecieve = new DataSet();
					dsRecieve = objDivision.PopulateUnassignedDepartment(intDivisionID);
					this.lbUnAssignDept.DataSource = dsRecieve.Tables[0];
					this.lbUnAssignDept.DataValueField = dsRecieve.Tables[0].Columns[0].ToString();
					this.lbUnAssignDept.DataTextField = dsRecieve.Tables[0].Columns[1].ToString();
					this.lbUnAssignDept.DataBind();
                    this.lbUnAssignDept.Items.Insert(0, (ClsCommon.BL_LANG_ID == 2 ? ">>... Selecione Departamento" : ">>...Select Departments"));
					this.lbUnAssignDept.SelectedIndex =0;
				}
			}
                	
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
		}

		/// <summary>
		/// fill the listbox with all the Assigned departments
		/// </summary>
		private void PopulateAssignedDepartment()
		{
			
			objDivision =new Cms.BusinessLayer.BlCommon.ClsDivision();
			DataSet dsRecieve ;
			int intDivisionID =0;	
			try
			{
				if(Convert.ToInt32(this.cmbDivisions.SelectedIndex)>=0)
				{
					if(this.cmbDivisions.SelectedValue != "")
						intDivisionID = Convert.ToInt32(this.cmbDivisions.SelectedValue);
					dsRecieve =new DataSet();
					dsRecieve=objDivision.PopulateassignedDepartment(intDivisionID);
					this.lbAssignDept.DataSource = dsRecieve.Tables[0];
					this.lbAssignDept.DataValueField = dsRecieve.Tables[0].Columns[0].ToString();
					this.lbAssignDept.DataTextField = dsRecieve.Tables[0].Columns[1].ToString();
					this.lbAssignDept.DataBind();
                    this.lbAssignDept.Items.Insert(0, (ClsCommon.BL_LANG_ID == 2 ? ">>... Selecione Departamento" : ">>...Select Departments"));
					this.lbAssignDept.SelectedIndex =0;
				}
                DataSet dsassignDepartment = new DataSet(); 
                string AssignDepartment = "";

                dsassignDepartment = objDivision.PopulateassignedDepartment(intDivisionID);
                if (dsassignDepartment != null && dsassignDepartment.Tables.Count > 0)
                {
                    foreach (DataRow dr in dsassignDepartment.Tables[1].Rows)
                    {
                        if (dr["DEPT_ID"] != null)
                        {
                            AssignDepartment = AssignDepartment + dr["DEPT_ID"] + ",";
                        }
                    }



                }
                if (AssignDepartment != "")
                {
                    AssignDepartment = AssignDepartment.Substring(0, AssignDepartment.Length - 1);
                }
                hidunassgndept.Value = AssignDepartment; 
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
		}
          
		#endregion

		#region btnSave_Click()
		
		/// <summary>
		/// Save the assigned departments to the selected division.
		/// </summary>
		/// <param name= "sender - The object that Fired the event e - Arguements associated with the event "></param>
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				objDivision =new Cms.BusinessLayer.BlCommon.ClsDivision();
				int intDivisionID ;
				intDivisionID = Convert.ToInt32(this.cmbDivisions.SelectedValue);
				string DivisionDesc = cmbDivisions.Items[cmbDivisions.SelectedIndex].Text;
				char[] arrDelimeter={','};
				string [] arrDeptId ;
				arrDeptId = hidDeptID.Value.ToString().Split(',');
				int i;
				int[] intDept = new int[arrDeptId.Length];
				for (i=1;i<arrDeptId.Length;i++)
				{
					intDept[i] = int.Parse(arrDeptId[i]);
				}
			
				int intResult;			
				intResult = objDivision.Save(intDivisionID,intDept);		
				if(intResult >= 1)
				{
					this.lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"1");
				}
				else
				{
					this.lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"2");
				}
				this.lblMessage.Visible=true;
				
				PopulateUnassignedDepartment();
				PopulateAssignedDepartment();

				Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGen = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
				//string trans_desc ="Departments have been assigned/unassigned to Division.";
                string trans_desc = ClsMessages.GetMessage(NewscreenId, "5");
                string trans_custom = ";" + ClsMessages.GetMessage(NewscreenId, "11") + ":" + DivisionDesc + ":;";
                trans_custom += ";" + ClsMessages.GetMessage(NewscreenId, "12") + ":;";
				for(int unasgnindex =1; unasgnindex< lbUnAssignDept.Items.Count; unasgnindex++)
				{
					if(unasgnindex > 1)
						trans_custom += ", " + lbUnAssignDept.Items[unasgnindex].Text;
					else
						trans_custom += lbUnAssignDept.Items[unasgnindex].Text;	
				}

                trans_custom += ";" + ClsMessages.GetMessage(NewscreenId, "13") + ":;";
				for(int asgnindex =1; asgnindex< lbAssignDept.Items.Count; asgnindex++)
				{
					if(asgnindex > 1)
						trans_custom += ", " + lbAssignDept.Items[asgnindex].Text;
					else
						trans_custom += lbAssignDept.Items[asgnindex].Text;	
				}
				objGen.WriteTransactionLog(0, 0, 0, trans_desc, int.Parse(GetUserId()),trans_custom, "Application");
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}

		}

		#endregion

		
	}
}
