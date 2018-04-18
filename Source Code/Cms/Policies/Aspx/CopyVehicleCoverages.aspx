<%@ Page language="c#" Codebehind="CopyVehicleCoverages.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.CopyVehicleCoverages" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Copy Coverages</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script src="../../cmsweb/scripts/xmldom.js"></script>
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<LINK href="../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script language="javascript">
		
	
		var rowSelected =0;
		function onSave()
		{
			if(rowSelected ==0)
			{
				var lblMsg =document.getElementById('lblMessage');
				lblMsg.innerText ="Please select a vehicle first";	
				lblMsg.style.display="inline";
				return false;
			}
				
		}
		
		function VerifyCheck(radio)
		{				
			var frm = document.Form1 ;
			
			/*
			if (radio.checked==true)
			{
				var strRadioId = radio.name;
				var strCommonId=strRadioId.split(":");
				var strCheckName=strCommonId[0]+ "_" + strCommonId[1] + "_" + "chkSECONDARY_APPLICANT";
				document.all[strCheckName].checked=true;				
			}*/
			if (radio.checked==true)
			{
				rowSelected=1;
			}
			
			for(i=0;i< frm.length;i++)
			{
				e=frm.elements[i];
				if ( e.type=='radio' && e.name.indexOf("rdbSELECT") != -1 )
				{	
					if (e.id!=radio.id)
					{			
						e.checked=false;				
					}
				}
			}
		}	
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td align="center">
						<asp:label id="lblMessage" runat="server" EnableViewState="False"  CssClass="errmsg"></asp:label></td>
				</tr>
				<tr>
					<td>
						<asp:DataGrid id="dgVehicles" runat="server" AutoGenerateColumns="False" Width="100%">
							<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
							<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
							<ItemStyle CssClass="midcolora"></ItemStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Select">
									<ItemTemplate>
										<INPUT type="radio" runat="server" onclick="VerifyCheck(this);" id="rdbSELECT" name="rdbSELECT"
											VALUE="rdbSELECT">
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="INSURED_VEH_NUMBER" HeaderText="Insured Veh Number"></asp:BoundColumn>
								<asp:BoundColumn DataField="VIN" HeaderText="VIN"></asp:BoundColumn>
								<asp:BoundColumn DataField="VEHICLE_YEAR" HeaderText="Year"></asp:BoundColumn>
								<asp:BoundColumn DataField="MAKE" HeaderText="Make"></asp:BoundColumn>
								<asp:BoundColumn DataField="MODEL" HeaderText="Model"></asp:BoundColumn>
								<asp:TemplateColumn Visible="False">
									<ItemTemplate>
										<asp:Label id=lblVEHICLE_ID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VEHICLE_ID") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%">
							<tr>
								<td class="midcolora" width="50%">
									<INPUT type="button" value="Close" class="clsButton" onclick="javascript:window.close();">
								</td>
								<td class="midcolorr" align="right" width="50%">
									<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidREC_VEH_ID" type="hidden" value="0" name="hidREC_VEH_ID" runat="server">
						<INPUT id="hidPolID" type="hidden" name="hidPolID" runat="server"> <INPUT id="hidPolVersionID" type="hidden" name="hidPolVersionID" runat="server">
						<INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server"> <INPUT id="hidROW_COUNT" type="hidden" value="0" name="hidAPP_LOB" runat="server">
						<INPUT id="hidCoverageXML" type="hidden" runat="server" NAME="hidCoverageXML"> <INPUT id="hidUserID" type="hidden" name="hidUserID" runat="server">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
