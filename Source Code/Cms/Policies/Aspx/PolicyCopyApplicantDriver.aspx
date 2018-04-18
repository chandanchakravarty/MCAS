<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="PolicyCopyApplicantDriver.aspx.cs" AutoEventWireup="false" Inherits="Policies.Aspx.PolicyCopyApplicantDriver" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>BRICS - Copy Applicants</title>
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
		/*if(("<%=gIntSaved%>")==1)
		{			 
			window.close();			
		}*/	
		function SetTitle()
		{
			document.title=document.getElementById('hidTitle').value;
		}		
		</script>
</HEAD>
	<body onload="ChangeColor();ApplyColor();showScroll();">
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
									<TD class="headereffectCenter"><asp:label id="lblHeader" Runat="server">Copy Applicants</asp:label></TD>
								</TR>
								<TR>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</TR>
								<tr>
									<td class="midcolora"><asp:datagrid id="dgrExistingDriver" Runat="server" DataKeyField="Applicant_ID" AutoGenerateColumns="False"
											HeaderStyle-CssClass="HeadRow" Width="100%">
											<Columns>
												<asp:TemplateColumn HeaderText="Select">
													<ItemTemplate>
														<asp:CheckBox ID="chkSelect" Runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<%--<asp:BoundColumn DataField="Applicant_ID" ItemStyle-CssClass="DataRow" Visible="True" HeaderText="Applicant ID"></asp:BoundColumn>--%>
												<asp:BoundColumn DataField="IS_PRIMARY_APPLICANT" ItemStyle-CssClass="DataRow" Visible="True" HeaderText="Primary Applicant"></asp:BoundColumn>
												<asp:BoundColumn DataField="APPLICANT_NAME" ItemStyle-CssClass="DataRow" HeaderText="Applicant Name"></asp:BoundColumn>
												<asp:BoundColumn DataField="APPLICANT_ADDRESS" ItemStyle-CssClass="DataRow" HeaderText="Address"></asp:BoundColumn>
												<asp:BoundColumn DataField="City" ItemStyle-CssClass="DataRow" HeaderText="City"></asp:BoundColumn>
												<asp:BoundColumn DataField="State" ItemStyle-CssClass="DataRow" HeaderText="State"></asp:BoundColumn>
												<asp:BoundColumn DataField="Zip_Code" ItemStyle-CssClass="DataRow" HeaderText="Zip"></asp:BoundColumn>
											</Columns>
										</asp:datagrid></td>
								</tr>
								<TR>
									<TD class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnSubmit" runat="server" Text="Submit"></cmsb:cmsbutton></TD>
								</TR>
								<TR>
									<TD class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnClose" runat="server" Text="Close" Visible="False"></cmsb:cmsbutton></TD>
								</TR>
								<tr>
									<td class="midcolorr"><INPUT id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server">
										<INPUT id="hidPolicyVersionID" type="hidden" value="0" name="hidPolicyVersionID" runat="server">
										<INPUT id="hidPolicyID" type="hidden" value="0" name="hidPolicyID" runat="server"> <INPUT id="hidCalledFrom" type="hidden" value="0" name="hidCalledFrom" runat="server">
										<INPUT id="hidCalledFor" type="hidden" value="0" name="hidCalledFor" runat="server">
										<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
										<INPUT id="hidTitle" type="hidden" value="0" name="hidTitle" runat="server">
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
