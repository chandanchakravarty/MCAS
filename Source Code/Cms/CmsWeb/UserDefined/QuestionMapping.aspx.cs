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

namespace Cms.CmsWeb.UserDefined
{
	/// <summary>
	/// Summary description for QuestionMapping.
	/// </summary>
	public class QuestionMapping : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.ListBox lstQuestionMapped;
		protected System.Web.UI.WebControls.Label lblMsg;
		protected System.Web.UI.WebControls.DropDownList ddlField;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvField;
		protected System.Web.UI.WebControls.DropDownList ddlQuestion;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvQuestion;
		protected System.Web.UI.WebControls.Button btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputButton Button1;
		//private string TabId = "";
		private string ScreenId = "";
		private string strCarrier;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="128_4";
			ScreenId = Request.QueryString["ScreenID"];
			strCarrier ="1";

			lblMsg.Text = "";
			if (!IsPostBack)
			{
				FillDropDowns();
			}
		}
		private void FillDropDowns()
		{
			UserDefinedOne loUser = new UserDefinedOne();

			ddlField.DataSource=loUser.getMappingFields();
			ddlField.DataTextField="MappingName";
			ddlField.DataValueField="quesmappingid";
			ddlField.DataBind();
			ddlField.Items.Insert(0,new ListItem("Select Field",""));
			ddlField.SelectedIndex=0;
			

			ddlQuestion.DataSource=loUser.getQuestionList(ScreenId,strCarrier);
			ddlQuestion.DataTextField="QSHORTDESC";
			ddlQuestion.DataValueField="qid";
			ddlQuestion.DataBind();
			ddlQuestion.Items.Insert(0,new ListItem("Select Question",""));
			ddlQuestion.SelectedIndex=0;

			FillMappedQuestions();
			
			
		}
		private void FillMappedQuestions()
		{
			UserDefinedOne loUser = new UserDefinedOne();
			lstQuestionMapped.DataSource=loUser.getMappedQuestion(ScreenId,strCarrier);
			lstQuestionMapped.DataTextField="mapping";
			lstQuestionMapped.DataValueField="qid";
			lstQuestionMapped.DataBind();
			lstQuestionMapped.Items.Insert(0,new ListItem("---------Select Mapped Question---------",""));
			lstQuestionMapped.AutoPostBack=true;

		}
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			UserDefinedOne loUser = new UserDefinedOne();
			string Qid= ddlQuestion.SelectedItem.Value;
			string FieldId= ddlField.SelectedItem.Value;

			int iRecordsAffected =  loUser.UpdateQuestionMapping(ScreenId,strCarrier,Qid,FieldId);
			
			if (iRecordsAffected > 0)
			{
				lblMsg.Text   = "Details successfully saved.";
				FillMappedQuestions();
			}
			else
			{
				lblMsg.Text   = "Mapping with the field already exists.";
			}
		}

		private void lstQuestionMapped_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string lsMappedValue =  lstQuestionMapped.SelectedItem.Value;
			if (lsMappedValue != "")
			{
				string[] lArrMappedValues  =lsMappedValue.Split('^');
				if (ddlQuestion.Items.FindByValue(lArrMappedValues[0]) != null)
				{
					ddlQuestion.SelectedIndex=-1;
					ddlQuestion.Items.FindByValue(lArrMappedValues[0]).Selected=true;
				}
				if (ddlField.Items.FindByValue(lArrMappedValues[1]) != null)
				{
					ddlField.SelectedIndex=-1;
					ddlField.Items.FindByValue(lArrMappedValues[1]).Selected=true;
				}
				

			}
			else
			{
				ddlField.SelectedIndex=0;
				ddlQuestion.SelectedIndex=0;
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
			this.lstQuestionMapped.SelectedIndexChanged += new System.EventHandler(this.lstQuestionMapped_SelectedIndexChanged);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
