<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="PolicyHomeOwnerEndorsements.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.aspx.HomeOwners.PolicyHomeOwnerEndorsements" validateRequest=false %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Endorsements</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="../../../cmsweb/scripts/xmldom.js"></script>
		<script src="../../../cmsweb/scripts/common.js"></script>
		<script src="../../../cmsweb/scripts/form.js"></script>
		<script>
			//New function being added for disabling the remarks/description field until corresponding checkbox is checked
			function DisableControls(strcbDelete,strRemarks)
			{
				 
				var lastIndex = strcbDelete.lastIndexOf('_');
				var prefix = strcbDelete.substring(0,lastIndex);
				var   editionDate=document.getElementById(prefix+'_ddlEDITIONDATE')
				//document.getElementById(strRemarks).style.display="none";
					
				//alert(strRemarks);
				if (document.getElementById(strcbDelete).checked == false )
				{
					document.getElementById(strRemarks).value="";
					document.getElementById(strRemarks).disabled=true;
						editionDate.disabled=true;
					/*lblLimit.style.display = "inline";
					lblLimit.innerText = 'N.A';
					ddlLimit.style.display = "none";
					alert("checked");*/
				}
				else
				{
					document.getElementById(strRemarks).disabled=false;
						editionDate.disabled=false;
					/*lblLimit.style.display = "none";
					lblLimit.innerText = 'N.A';
					ddlLimit.style.display = "inline";*/
				}
					
			}
			///////////////////////
			function ResetForm()
			{
				document.Form1.reset();
				return false;
			}
			
		</script>
	</HEAD>
	<body oncontextmenu="return false;" MS_POSITIONING="GridLayout">
		<div class="pageContent" id="bodyHeight" style="OVERFLOW:auto;POSITION:relative;HEIGHT:1000pt">
			<form id="Form1" method="post" runat="server">
				<table cellSpacing="0" cellPadding="0" width="100%">
					<tr>
						<TD>
							<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
						</TD>
					</tr>
					<tr>
						<td class="headereffectCenter"><asp:label id="lblTitle" runat="server">Endorsements</asp:label></td>
					</tr>
					<tr>
						<td align="center"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False" EnableViewState="False"></asp:label></td>
					</tr>
					<tr>
						<td class="midcolora"><asp:datagrid id="dgEndorsements" runat="server" DataKeyField="DWELLING_ENDORSEMENT_ID" AutoGenerateColumns="False"
								Width="100%">
								
								<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Required /Optional">
										<ItemTemplate>
											<asp:Label ID="lblEND_ID" Visible="False" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ENDORSMENT_ID") %>'>
											</asp:Label>
											<asp:CheckBox ID="cbSelect" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Endorsement">
										<ItemTemplate>
											<asp:Label ID="lblENDORSEMENT" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ENDORSEMENT") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Remarks (Max 500 Chars.)" Visible="True">
										<ItemTemplate>
											<asp:TextBox id=txtREMARKS Width="180px" onkeypress="MaxLength(this,500)" maxlength="500" runat="server"  Text='<%# DataBinder.Eval(Container, "DataItem.Remarks") %>'>
											</asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Edition Date">
									<ItemStyle HorizontalAlign="Right" Width="20%"></ItemStyle>
									<ItemTemplate>
										<select id="ddlEDITIONDATE" Visible="True" Runat="server" ENDORSMENT_ID='<%# DataBinder.Eval(Container, "DataItem.ENDORSMENT_ID") %>' NAME="ddlEDITIONDATE">
										</select>
									</ItemTemplate>
								   </asp:TemplateColumn>
									<asp:BoundColumn DataField="Selected" Visible="False"></asp:BoundColumn>
								</Columns>
							</asp:datagrid></td>
					</tr>
					<tr>
						<td>
							<table width="100%">
								<tr>
									<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Visible="False" Text="Delete" Enabled="False"></cmsb:cmsbutton></td>
									<td class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td><INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidREC_VEH_ID" type="hidden" value="0" name="hidREC_VEH_ID" runat="server">
							<INPUT id="hidPOLID" type="hidden" name="hidPOLID" runat="server"> <INPUT id="hidPOLVersionID" type="hidden" name="hidPOLVersionID" runat="server">
							<INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server"> <INPUT id="hidROW_COUNT" type="hidden" value="0" name="hidPOL_LOB" runat="server">
							<INPUT id="hidCoverageXML" type="hidden" name="hidCoverageXML" runat="server">
						</td>
					</tr>
				</table>
			</form>
		</div>
	</body>
</HTML>




