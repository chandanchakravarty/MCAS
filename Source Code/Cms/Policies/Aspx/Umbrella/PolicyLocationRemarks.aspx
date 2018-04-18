<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page validateRequest="false" CodeBehind="PolicyLocationRemarks.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Umbrella.PolicyLocationRemarks" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
			function AddData()
			{
			ChangeColor();
			DisableValidators();
			document.getElementById('txtREMARKS').focus();
			document.getElementById('txtREMARKS').value  = '';
			}
			function populateXML()
			{
				if(document.getElementById('hidFormSaved')!=null &&( document.getElementById('hidFormSaved').value == '0'||document.getElementById('hidFormSaved').value == '1'))
				{
					var tempXML='';
					tempXML=document.getElementById('hidOldData').value;		
					if(tempXML!="" && tempXML!=0)
					{							
						populateFormData(tempXML,POL_UMB_REAL_ESTATE_LOCATION);
					}
					else
					{
						AddData();
					}
				}
			return false;
			}
								
			function ChkTextAreaLength(source, arguments)
			{
				var txtArea = arguments.Value;
				if(txtArea.length > 200 ) 
				{
				
					//event.returnValue=false;
					arguments.IsValid = false;
					return false;   // invalid userName
				}
			}
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload='populateXML();ApplyColor();ChangeColor();'>
		<form id="POL_UMB_REAL_ESTATE_LOCATION" method="post" runat="server">
			<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
			<TABLE class="tableWidthHeader" align="center" border="0">
				<TR>
					<TD class="midcolorc" align="center" colSpan="2">
						<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
						<asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></TD>
				</TR>
				<TR>
					<TD class="midcolora"><asp:label id="capREMARKS" runat="server">Remarks(Max 200 characters)</asp:label></TD>
					<TD class="midcolora"><asp:textbox id="txtREMARKS" runat="server" Columns="40" Rows="5" TextMode="MultiLine" size="30"
							onkeypress="MaxLength(this,200)" maxlength="200"></asp:textbox><br>
						<asp:CustomValidator ID="csvREMARKS" Runat="server" ControlToValidate="txtREMARKS" ClientValidationFunction="ChkTextAreaLength"
							Display="Dynamic"></asp:CustomValidator>
					</TD>
				</TR>
				<TR>
					<TD class="midcolora" colSpan="1">
						<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
					</TD>
					<TD class="midcolorr" colSpan="1">
						<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidCUSTOMER_ID" type="hidden" name="hidCUSTOMER_ID" runat="server"> <INPUT id="hidOldData" type="hidden" name="Hidden1" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"> <INPUT id="hidLOCATION_ID" type="hidden" value="0" name="hidLOCATION_ID" runat="server">
			<INPUT id="hidPOLICY_ID" type="hidden" name="hidAPP_ID" runat="server"> <INPUT id="hidPOLICY_VERSION_ID" type="hidden" name="hidAPP_VERSION_ID" runat="server">
		</form>
	</body>
</HTML>


