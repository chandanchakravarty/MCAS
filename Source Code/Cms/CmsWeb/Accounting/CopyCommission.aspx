<%@ Page language="c#" Codebehind="CopyCommission.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Accounting.CopyCommission" ValidateRequest="false" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CopyCommission</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		if(("<%=gIntSaved%>")==1)
		{			 
			window.close();			
		}
		
		function SetTitle()
		{	
			if(document.getElementById('hidCommisionType').value=="R")
				document.title = "BRICS - List of Regular Commission Setup - Agency";
			else if (document.getElementById('hidCommisionType').value=="A")
				document.title = "BRICS - List of Additional Commission Setup - Agency";
			else if (document.getElementById('hidCommisionType').value=="P")
				document.title = "BRICS - List of Property Inspection Credit";	
			else if (document.getElementById('hidCommisionType').value=="C")
				document.title = "BRICS - List of Complete App Bonus";		
							
		}
		function SelectComboOptionByTextNew(comboId,selectedValue)
		{
		
		if(window.opener.document.getElementById(comboId).options.length>0)
		{
			for(var j=0; j<window.opener.document.getElementById(comboId).options.length; j++)
			{
			if(selectedValue == window.opener.document.getElementById(comboId).options[j].text)
				{
					//alert(comboId);
				//	alert(selectedValue);
					window.opener.document.getElementById(comboId).options.selectedIndex = j;
					DisableValidatorsById(comboId);
					break;
				}
			}
			}
			else
			{
				window.opener.document.getElementById(comboId).options.selectedValue = selectedValue;
			}
		}
		
		function SelectComboOptionByIdNew(comboId,selectedValue)
		{
		  
		if(window.opener.document.getElementById(comboId).options.length>0)
		{
			for(var j=0; j<window.opener.document.getElementById(comboId).options.length; j++)
			{
			if(selectedValue == window.opener.document.getElementById(comboId).options[j].value)
				{
					window.opener.document.getElementById(comboId).options.selectedIndex = j;
					DisableValidatorsById(comboId);
					break;
				}
			}
			}
			else
			{
				window.opener.document.getElementById(comboId).options.selectedValue = selectedValue;
			}
		}
		
		function fillRegularCommissionOpener()
		{
		    var array = document.getElementById('hidFillgrid').value.split('~');
		   // alert(document.getElementById('hidFillgrid').value);
		   //SelectComboOptionByTextNew("cmbSTATE_ID", array[0]);
		    SelectComboOptionByIdNew("cmbCOUNTRY_ID", array[15]);

		    window.opener.State_id = array[8];
		    window.opener.document.getElementById('hidState').value = array[10]; 
			var COUNTRY_ID = array[15];
			
			window.opener.CallAJAX("AddRegCommSetup_Agency.aspx/AjaxFillState", ["COUNTRY_ID", COUNTRY_ID], "outputDTSUBLOB", "ShowError", "#cmbSTATE_ID", "STATE_ID", "STATE_NAME");
			
			var STATE_ID = array[10];
			
			//window.opener.FillLOB();
			window.opener.CallAJAX("AddRegCommSetup_Agency.aspx/GetSubLOBs", ["STATE_ID", STATE_ID], "outputDTSUBLOB", "ShowError", "#cmbLOB_ID", "LOB_ID", "LOB_DESC");


			var LOB_ID = array[11];
			
			window.opener.CallAJAX("AddRegCommSetup_Agency.aspx/GetSubSubLOBs", ["STATE_ID", STATE_ID, "LOB_ID", LOB_ID], "outputDTSUBLOB", "ShowError", "#cmbSUB_LOB_ID", "SUB_LOB_ID", "SUB_LOB_DESC");

			window.opener.LOBId = array[11] ;
			window.opener.document.getElementById('hidLOB_ID').value= array[11] ;
			window.opener.SUB_LOB_IDId= array[12] ;
			window.opener.document.getElementById('hidSUB_LOB_ID').value= array[12] ;
			//window.opener.FillSubLOB();
			SelectComboOptionByIdNew("cmbSTATE_ID", STATE_ID);
			SelectComboOptionByTextNew("cmbLOB_ID",array[1]);
			SelectComboOptionByTextNew("cmbSUB_LOB_ID",array[2]);

			window.opener.PopulateClassDropDown();
			SelectComboOptionByTextNew("cmbCLASS_RISK", array[3]);
			//window.opener.document.getElementById('hidSelectedClass').value= array[4] ;
			SelectComboOptionByTextNew("cmbTERM",array[4]);
			window.opener.document.getElementById('txtEFFECTIVE_FROM_DATE').value=array[5];
			window.opener.document.getElementById('txtEFFECTIVE_TO_DATE').value=array[6];
			window.opener.document.getElementById('txtCOMMISSION_PERCENT').value=array[7];
			//window.opener.document.getElementById('hidCOMM_ID').value='NEW';
			
		}
		function fillAdditionalCommissionOpener()
		{
		    var array = document.getElementById('hidFillgrid').value.split('~');
		   
		    SelectComboOptionByIdNew("cmbCOUNTRY_ID", array[17]);

			SelectComboOptionByIdNew("cmbAGENCY_ID",array[16]);
			//SelectComboOptionByTextNew("cmbSTATE_ID",array[1]);
			window.opener.State_id =array[11] ;
			//window.opener.FillLOB();

			window.opener.document.getElementById('hidState').value = array[11];
			var COUNTRY_ID = array[17];
			
			window.opener.CallAJAX("AddRegCommSetup_Agency.aspx/AjaxFillState", ["COUNTRY_ID", COUNTRY_ID], "outputDTSUBLOB", "ShowError", "#cmbSTATE_ID", "STATE_ID", "STATE_NAME");
			
			SelectComboOptionByIdNew("cmbSTATE_ID", array[11]);
			var STATE_ID = array[11];
			
			//window.opener.FillLOB();
			window.opener.CallAJAX("AddRegCommSetup_Agency.aspx/GetSubLOBs", ["STATE_ID", STATE_ID], "outputDTSUBLOB", "ShowError", "#cmbLOB_ID", "LOB_ID", "LOB_DESC");


			var LOB_ID = array[12];
			var STATE_ID = array[11];
			
			window.opener.CallAJAX("AddRegCommSetup_Agency.aspx/GetSubSubLOBs", ["STATE_ID", STATE_ID, "LOB_ID", LOB_ID], "outputDTSUBLOB", "ShowError", "#cmbSUB_LOB_ID", "SUB_LOB_ID", "SUB_LOB_DESC");

			window.opener.LOBId = array[12] ;
			window.opener.document.getElementById('hidLOB_ID').value= array[12] ;
			window.opener.SUB_LOB_IDId= array[13] ;
			window.opener.document.getElementById('hidSUB_LOB_ID').value= array[13] ;
			//window.opener.FillSubLOB();
			window.opener.PopulateClassDropDown();
			SelectComboOptionByTextNew("cmbCLASS_RISK", array[3]);
			SelectComboOptionByTextNew("cmbAGENCY_ID",array[1]);
			SelectComboOptionByTextNew("cmbLOB_ID",array[2]);
			SelectComboOptionByTextNew("cmbSUB_LOB_ID",array[3]);
			SelectComboOptionByTextNew("cmbCLASS_RISK",array[4]);
			SelectComboOptionByTextNew("cmbTERM",array[5]);
			window.opener.document.getElementById('txtEFFECTIVE_FROM_DATE').value=array[6];
			window.opener.document.getElementById('txtEFFECTIVE_TO_DATE').value=array[7];
			window.opener.document.getElementById('txtCOMMISSION_PERCENT').value=array[8];
			window.opener.document.getElementById('hidCOMM_ID').value='NEW';
		}
		function fillPropertyInspectionOpener()
		{
			var array = document.getElementById('hidFillgrid').value.split('~');
			SelectComboOptionByTextNew("cmbSTATE_ID",array[0]);
			SelectComboOptionByTextNew("cmbLOB_ID",array[1]);
			SelectComboOptionByTextNew("cmbTERM",array[2]);
			window.opener.document.getElementById('txtEFFECTIVE_FROM_DATE').value=array[3];
			window.opener.document.getElementById('txtEFFECTIVE_TO_DATE').value=array[4];
			window.opener.document.getElementById('txtCOMMISSION_PERCENT').value=array[5];
			if (array.length > 11)
				window.opener.document.getElementById('txtREMARKS').value=array[11];
			else 
				window.opener.document.getElementById('txtREMARKS').value="";
			//window.opener.document.getElementById('hidCOMM_ID').value='NEW';
		}
		function fillCompleteAppOpener()
		{
			var array = document.getElementById('hidFillgrid').value.split('~');
			SelectComboOptionByTextNew("cmbSTATE_ID",array[0]);
			window.opener.FillLOB();
			SelectComboOptionByTextNew("cmbLOB_ID",array[1]);
			window.opener.document.getElementById('hidLOB_ID').value= array[9] ;
			SelectComboOptionByTextNew("cmbTERM",array[2]);
			window.opener.document.getElementById('txtEFFECTIVE_FROM_DATE').value=array[3];
			window.opener.document.getElementById('txtEFFECTIVE_TO_DATE').value=array[4];
			window.opener.document.getElementById('txtCOMMISSION_PERCENT').value=array[5];
			var AmtType = array[13];
			if(AmtType == "P")
			{
				window.opener.document.getElementsByName('rdblAMOUNT_TYPE')[1].checked = true;
			}
			else if(AmtType == "F")
			{
				window.opener.document.getElementsByName('rdblAMOUNT_TYPE')[2].checked = true;
			}
			//window.opener.document.getElementById('hidCOMM_ID').value='NEW';

		}
		</script>
	</HEAD>
	<body onload="SetTitle();ChangeColor();ApplyColor();">
		<div class="pageContent" id="bodyHeight">
			<form id="Form1" method="post" runat="server">
				<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0">
					<tr>
						<td>
							<table cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
								<tr>
									<td>&nbsp;</td>
								</tr>
								<TR class="midcolora">
									<TD class="headereffectCenter"><asp:label id="lblHeader" Runat="server">List of Regular Commission Setup - Agency</asp:label></TD>
								</TR>
								<TR>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</TR>
								<tr>
									<td class="midcolora">
										<asp:datagrid id="dgrRegularCommission" Runat="server" Width="100%" HeaderStyle-CssClass="HeadRow"
											AutoGenerateColumns="false" Visible="False">
											<Columns>
												<asp:TemplateColumn HeaderText="Select">
													<ItemTemplate>
														<asp:CheckBox ID="chkSelect" Runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="STATE_NAME" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="State"></asp:BoundColumn>
												<asp:BoundColumn DataField="LOB_DESC" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Product"></asp:BoundColumn>
												<asp:BoundColumn DataField="SUB_LOB_DESC" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="LOB"></asp:BoundColumn>
												<asp:BoundColumn DataField="LOOKUP_VALUE_DESC" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Risks"></asp:BoundColumn>
												<asp:BoundColumn DataField="TermDesc" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Term"></asp:BoundColumn>
												<asp:BoundColumn DataField="EFFECTIVE_FROM_DATE" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="From Date"></asp:BoundColumn>
												<asp:BoundColumn DataField="EFFECTIVE_TO_DATE" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="To Date"></asp:BoundColumn>
												<asp:BoundColumn DataField="COMMISSION_PERCENT" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Comm %"></asp:BoundColumn>
												<asp:BoundColumn DataField="LAST_UPDATED_DATETIME" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Last Amended"></asp:BoundColumn>
												<asp:BoundColumn DataField="userName" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Amended By"></asp:BoundColumn>
												<asp:BoundColumn DataField="STATE_ID" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="State_id"></asp:BoundColumn>
												<asp:BoundColumn DataField="LOB_ID" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="Lob_id"></asp:BoundColumn>
												<asp:BoundColumn DataField="SUB_LOB_ID" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="Sub_lob"></asp:BoundColumn>
												<asp:BoundColumn DataField="CLASS_RISK" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="Risk"></asp:BoundColumn>
												<asp:BoundColumn DataField="TERM" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="Term"></asp:BoundColumn>
												<asp:BoundColumn DataField="COUNTRY_ID" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="COUNTRY_ID"></asp:BoundColumn>
											</Columns>
										</asp:datagrid>
									</td>
								</tr>
								<tr>
									<td class="midcolora">
										<asp:datagrid id="dgrAdditionalCommission" Runat="server" Width="100%" HeaderStyle-CssClass="HeadRow"
											AutoGenerateColumns="false" Visible="False">
											<Columns>
												<asp:TemplateColumn HeaderText="Select">
													<ItemTemplate>
														<asp:CheckBox ID="chkSelect" Runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="AGENCY_DISPLAY_NAME" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Agency"></asp:BoundColumn>
												<asp:BoundColumn DataField="STATE_NAME" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="State"></asp:BoundColumn>
												<asp:BoundColumn DataField="LOB_DESC" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Product"></asp:BoundColumn>
												<asp:BoundColumn DataField="SUB_LOB_DESC" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="LOB"></asp:BoundColumn>
												<asp:BoundColumn DataField="LOOKUP_VALUE_DESC" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Risks"></asp:BoundColumn>
												<asp:BoundColumn DataField="TermDesc" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Term"></asp:BoundColumn>
												<asp:BoundColumn DataField="EFFECTIVE_FROM_DATE" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="From Date"></asp:BoundColumn>
												<asp:BoundColumn DataField="EFFECTIVE_TO_DATE" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="To Date"></asp:BoundColumn>
												<asp:BoundColumn DataField="COMMISSION_PERCENT" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Comm %"></asp:BoundColumn>
												<asp:BoundColumn DataField="LAST_UPDATED_DATETIME" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Last Amended"></asp:BoundColumn>
												<asp:BoundColumn DataField="username" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Amended By"></asp:BoundColumn>
												<asp:BoundColumn DataField="STATE_ID" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="State_id"></asp:BoundColumn>
												<asp:BoundColumn DataField="LOB_ID" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="Lob_id"></asp:BoundColumn>
												<asp:BoundColumn DataField="SUB_LOB_ID" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="Sub_lob"></asp:BoundColumn>
												<asp:BoundColumn DataField="CLASS_RISK" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="Risk"></asp:BoundColumn>
												<asp:BoundColumn DataField="TERM" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="Term"></asp:BoundColumn>
												<asp:BoundColumn DataField="AGENCY_ID" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="AGENCY_ID"></asp:BoundColumn>
												<asp:BoundColumn DataField="COUNTRY_ID" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="COUNTRY_ID"></asp:BoundColumn>
											</Columns>
										</asp:datagrid>
									</td>
								</tr>
								<tr>
									<td class="midcolora">
										<asp:datagrid id="dgrPropertyInspection" Runat="server" Width="100%" HeaderStyle-CssClass="HeadRow"
											AutoGenerateColumns="false" Visible="False">
											<Columns>
												<asp:TemplateColumn HeaderText="Select">
													<ItemTemplate>
														<asp:CheckBox ID="chkSelect" Runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="STATE_NAME" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="State"></asp:BoundColumn>
												<asp:BoundColumn DataField="LOB_DESC" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="LOB"></asp:BoundColumn>
												<asp:BoundColumn DataField="TermDesc" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="Term"></asp:BoundColumn>
												<asp:BoundColumn DataField="EFFECTIVE_FROM_DATE" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="From Date"></asp:BoundColumn>
												<asp:BoundColumn DataField="EFFECTIVE_TO_DATE" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="To Date"></asp:BoundColumn>
												<asp:BoundColumn DataField="COMMISSION_PERCENT" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Credit %"></asp:BoundColumn>
												<asp:BoundColumn DataField="LAST_UPDATED_DATETIME" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Last Amended"></asp:BoundColumn>
												<asp:BoundColumn DataField="userName" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Amended By"></asp:BoundColumn>
												<asp:BoundColumn DataField="STATE_ID" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="State_id"></asp:BoundColumn>
												<asp:BoundColumn DataField="LOB_ID" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="Lob_id"></asp:BoundColumn>
												<asp:BoundColumn DataField="TERM" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="Term"></asp:BoundColumn>
												<asp:BoundColumn DataField="REMARKS" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="Remarks"></asp:BoundColumn>
											</Columns>
										</asp:datagrid>
									</td>
								</tr>
								<tr>
									<td class="midcolora">
										<asp:datagrid id="dgrCompleteApp" Runat="server" Width="100%" HeaderStyle-CssClass="HeadRow" AutoGenerateColumns="false"
											Visible="False">
											<Columns>
												<asp:TemplateColumn HeaderText="Select">
													<ItemTemplate>
														<asp:CheckBox ID="chkSelect" Runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="STATE_NAME" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="State"></asp:BoundColumn>
												<asp:BoundColumn DataField="LOB_DESC" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="LOB"></asp:BoundColumn>
												<asp:BoundColumn DataField="TermDesc" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="Term"></asp:BoundColumn>
												<asp:BoundColumn DataField="EFFECTIVE_FROM_DATE" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="From Date"></asp:BoundColumn>
												<asp:BoundColumn DataField="EFFECTIVE_TO_DATE" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="To Date"></asp:BoundColumn>
												<asp:BoundColumn DataField="COMMISSION_PERCENT" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Amount"></asp:BoundColumn>
												<asp:BoundColumn DataField="LAST_UPDATED_DATETIME" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Last Amended"></asp:BoundColumn>
												<asp:BoundColumn DataField="username" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Amended By"></asp:BoundColumn>
												<asp:BoundColumn DataField="STATE_ID" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="State_id"></asp:BoundColumn>
												<asp:BoundColumn DataField="LOB_ID" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="Lob_id"></asp:BoundColumn>
												<asp:BoundColumn DataField="SUB_LOB_ID" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="Sub_lob"></asp:BoundColumn>
												<asp:BoundColumn DataField="CLASS_RISK" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="Risk"></asp:BoundColumn>
												<asp:BoundColumn DataField="TERM" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="Term"></asp:BoundColumn>
												<asp:BoundColumn DataField="AMOUNT_TYPE" Visible="False" ItemStyle-CssClass="DataRow" HeaderText="Type"></asp:BoundColumn>
											</Columns>
										</asp:datagrid>
									</td>
								</tr>
								<TR>
									<TD class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnSubmit" runat="server" Text="Submit"></cmsb:cmsbutton></TD>
								</TR>
								<tr>
									<td class="midcolorr">
										<INPUT id="hidCommisionType" type="hidden" value="0" name="hidCommisionType" runat="server">
										<INPUT id="hidFillgrid" type="hidden" value="0" name="hidFillgrid" runat="server">
										<INPUT id="hidAppID" type="hidden" value="0" name="hidAppID" runat="server"> <INPUT id="hidCalledFrom" type="hidden" value="0" name="hidCalledFrom" runat="server">
										<INPUT id="hidCalledFor" type="hidden" value="0" name="hidCalledFor" runat="server">
										<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
										<INPUT id="hidTitle" type="hidden" value="0" name="hidTitle" runat="server">
										<INPUT id="hidLOBxml" type="hidden" name="hidLOBxml" runat="server">
										<INPUT id="hidsubLOBxml" type="hidden" name="hidsubLOBxml" runat="server">
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</form>
		</div>
	</body>
</HTML>
