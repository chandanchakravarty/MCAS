<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="CopyTemplate.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.CopyTemplate" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CopyTemplate</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		if(("<%=gIntSaved%>")==1)
		{			 
			window.close();	
			loc=window.opener.location.href;  
			window.opener.location=loc.toString(); 		
		}
		function CloseCopy()
		{
			window.close();	
			loc=window.opener.location.href; 
			loc = "AddJournalEntryMaster.aspx?GROUP_TYPE=" + document.getElementById('hidCalledFrom').value + "&CalledFrom=" + "&JOURNAL_ID=" + document.getElementById('hidJournal_ID').value + "&";	
			window.opener.location=loc.toString(); 		
		
		}
		function SubmitCopy()
		{
			loc=window.opener.location.href; 
			loc = "AddJournalEntryMaster.aspx?GROUP_TYPE=" + document.getElementById('hidCalledFrom').value + "&CalledFrom=" + "&JOURNAL_ID=" + document.getElementById('hidJournal_ID').value + "&";	
			window.opener.location=loc.toString(); 		
		
		}
		
		function SetTitle()
		{	
			document.title = "List of Journal Entries - Template";
		}
		</script>
	</HEAD>
	<body onload="ChangeColor();ApplyColor();SetTitle();">
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
									<TD class="headereffectCenter"><asp:label id="lblHeader" Runat="server">List of Journal Entries - Template</asp:label></TD>
								</TR>
								<TR>
									<td class="midcolorc" align="right" colSpan="3"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</TR>
								<tr>
									<td class="midcolora"><asp:datagrid id="dgrTemplate" Runat="server" Visible="False" AutoGenerateColumns="false" HeaderStyle-CssClass="HeadRow"
											Width="100%">
											<Columns>
												<asp:TemplateColumn HeaderText="Select">
													<ItemTemplate>
														<asp:CheckBox ID="chkSelect" Runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="JOURNAL_ENTRY_NO" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Entry Number"></asp:BoundColumn>
												<asp:BoundColumn DataField="AJM" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Date Created"></asp:BoundColumn>
												<asp:BoundColumn DataField="TRANS_DATE" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Transaction Date"></asp:BoundColumn>
												<asp:BoundColumn DataField="DESCRIPTION" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Description"></asp:BoundColumn>
												<asp:BoundColumn DataField="PROFF" Visible="True" ItemStyle-CssClass="DataRow" HeaderText="Proof"></asp:BoundColumn>
												<asp:BoundColumn DataField="JOURNAL_ID" Visible="False" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
											</Columns>
										</asp:datagrid></td>
								</tr>
								<TR>
									<TD class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnSavClos" runat="server" Text="Submit &amp; Close"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnClose" runat="server" Text="Close" ></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnSubmit" runat="server" Text="Submit" ></cmsb:cmsbutton></TD>
								</TR>
								<tr>
									<td class="midcolorr"><INPUT id="hidCommisionType" type="hidden" value="0" name="hidCommisionType" runat="server">
										<INPUT id="hidFillgrid" type="hidden" value="0" name="hidFillgrid" runat="server">
										<INPUT id="hidAppID" type="hidden" value="0" name="hidAppID" runat="server"> <INPUT id="hidCalledFrom" type="hidden" value="0" name="hidCalledFrom" runat="server">
										<INPUT id="hidCalledFor" type="hidden" value="0" name="hidCalledFor" runat="server">
										<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
										<INPUT id="hidTitle" type="hidden" value="0" name="hidTitle" runat="server">
										<INPUT id="hidJournal_ID" type="hidden" value="0" name="hidJournal_ID" runat="server">
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
