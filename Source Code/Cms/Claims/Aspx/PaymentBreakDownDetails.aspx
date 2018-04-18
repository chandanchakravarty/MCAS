<%@ Page language="c#" validateRequest=false Codebehind="PaymentBreakDownDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.PaymentBreakDownDetails" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Claims Reserve Breakdown</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="javascript">	
		
		
		function GoBack()
		{
			top.botframe.location.href = "PaymentDetails.aspx?CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&";
			return false;
		}
		function Init()
		{
			ApplyColor();
			ChangeColor();			
		}
		</script>
</HEAD>
	<body MS_POSITIONING="GridLayout" onload="Init();">		
		<div id="bodyHeight" class="pageContent">
			<form id="Form1" method="post" runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%">
					<TR>
						<TD>
							<webcontrol:gridspacer id="grdspacer" runat="server"></webcontrol:gridspacer></TD>
					</TR>
					<TR id="trBody" runat="server">
						<TD>
							<table cellSpacing="0" cellPadding="0" WIDTH="100%">
								<tr>
									<td class="headereffectCenter" colspan="4"><asp:label id="lblTitle" runat="server">Claims Payment Breakdown</asp:label></td>
								</tr>
								<tr>
									<td align="center" colspan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False" EnableViewState="False"></asp:label></td>
								</tr>								
								<%--<tr>
									<td class="midcolora" width="18%">
										<asp:Label ID="capACTIVITY_ID" Runat="server"></asp:Label>
									</td>
									<td class="midcolora" width="32%">
										<asp:TextBox ID="txtACTIVITY_ID" Runat="server" class="midcolora" BorderStyle="None" BorderWidth="0" ReadOnly="True"></asp:TextBox>
									</td>
									<td class="midcolora" width="18%">
										<asp:Label ID="capACTIVITY_DATE" Runat="server"></asp:Label>
									</td>
									<td class="midcolora" width="32%">
										<asp:TextBox ID="txtACTIVITY_DATE" Runat="server" class="midcolora" BorderStyle="None" BorderWidth="0" ReadOnly="True"></asp:TextBox>
									</td>
								</tr>
								<tr>
									<td class="midcolora" width="18%">
										<asp:Label ID="capPAYMENT_AMOUNT" Runat="server"></asp:Label>
									</td>
									<td class="midcolora" width="32%">
										<asp:TextBox ID="txtPAYMENT_AMOUNT" Runat="server" class="midcolora" BorderStyle="None" BorderWidth="0" ReadOnly="True"></asp:TextBox>
									</td>
									<td class="midcolora" colspan="2"></td>
								</tr>
								<tr>
									<TD colSpan="4" height="10"></TD>
								</tr>--%>
								<tr>
									<td class="midcolora" width="18%">
										<asp:Label ID="capTRANSACTION_CODE" Runat="server"></asp:Label><span class="mandatory">*</span>
									</td>
									<td class="midcolora" width="32%">
										<asp:DropDownList ID="cmbTRANSACTION_CODE" Runat="server"></asp:DropDownList><br>
										<asp:RequiredFieldValidator ID="rfvTRANSACTION_CODE" Display="Dynamic" ControlToValidate="cmbTRANSACTION_CODE"
											Runat="server"></asp:RequiredFieldValidator>
									</td>
									<%--<td class="midcolora" width="18%">
										<asp:Label ID="capCOVERAGE_ID" Runat="server"></asp:Label><span class="mandatory">*</span>
									</td>
									<td class="midcolora" width="32%">
										<asp:DropDownList ID="cmbCOVERAGE_ID" Runat="server"></asp:DropDownList>										
									</td>
								</tr>
								<tr>--%>
									<td class="midcolora" width="18%">
										<asp:Label ID="capPAID_AMOUNT" Runat="server"></asp:Label>
									</td>
									<td class="midcolora" width="32%">
										<asp:TextBox ID="txtPAID_AMOUNT" Runat="server"  size="15" MaxLength="10"></asp:TextBox>
									</td>
									<%--<td class="midcolora" colspan="2"></td>--%>
								</tr>
								<tr>
									<TD colSpan="4" height="10"></TD>
								</tr>								
								<tr>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnBack" runat="server" Text="Back"></cmsb:cmsbutton></td>
									<td class="midcolorr" colSpan="2">										
										<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
									</td>
								</tr>
							</table>
						</TD>
					</TR>
				</TABLE>
				<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server">
				<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
				<INPUT id="hidCLAIM_ID" type="hidden" name="hidCLAIM_ID" runat="server">
				<INPUT id="hidACTIVITY_ID" type="hidden" name="hidACTIVITY_ID" runat="server">
				<INPUT id="hidPAYMENT_BREAKDOWN_ID" type="hidden" name="hidPAYMENT_BREAKDOWN_ID" runat="server">
			</form>
		</div>
		<script>
			RefreshWebGrid(1,document.getElementById('hidPAYMENT_BREAKDOWN_ID').value,true);			
		</script>
	</body>
</HTML>
