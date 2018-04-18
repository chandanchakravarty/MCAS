<%@ Page language="c#" Codebehind="PolicyCustomerVehicle.aspx.cs" AutoEventWireup="false" Inherits="Policies.Aspx.PolicyCustomerVehicle" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title> <%=strTitle%>
		</title>
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content=C# name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema><LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
<script src="/cms/cmsweb/scripts/xmldom.js"></script>

<script src="/cms/cmsweb/scripts/common.js"></script>

<script src="/cms/cmsweb/scripts/form.js"></script>

<script>
		
		function ShowWhenSaved()
		{
			if(("<%=gIntSaved%>")==1)
			{			 
				var calledfrom='';				 
				if(document.getElementById('hidCalledFrom')!=null)
				{
					calledfrom = document.getElementById('hidCalledFrom').value;
				}
				 
				window.opener.self.location.href="PolicyVehicleIndex.aspx?CalledFrom="+calledfrom;
				window.close();
			}		
		}		
		</script>
</HEAD>
<body oncontextmenu = "return false;" leftMargin=0 topMargin=0 onload=ShowWhenSaved();>
<FORM id=CustomerVehicle method=post runat="server">
<TABLE class=tablewidth cellSpacing=0 cellPadding=0 align=center border=0>
  <TR class=midcolora>
    <TD class=headereffectcenter><asp:label id=lblHeader Runat="server"></asp:label></TD></TR><br 
  >
  <TR>
    <td class=midcolorc align=right colSpan=4><asp:label id=lblMessage runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
  <TR></TR>
  <TR class=midcolora>
    <TD align=center><asp:datagrid id=dgrCustVehicle runat="server" DataKeyField="VEHICLE_ID" Width="100%" HeaderStyle-CssClass="HeadRow" AutoGenerateColumns="False" >
							<Columns>
								<asp:TemplateColumn HeaderText="Select">
									<ItemTemplate>
										<asp:CheckBox ID="chkSelect" Runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="POLICY_ID" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="POLICY_VERSION_ID" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="APPPOL_NUMBER" Visible="true" ItemStyle-CssClass="DataRow" HeaderText="Pol #" HeaderStyle-Width="20%"></asp:BoundColumn>
								<asp:BoundColumn DataField="APPPOL_VERSION_ID" Visible="true" ItemStyle-CssClass="DataRow" HeaderText="Pol Ver." HeaderStyle-Width="5%"></asp:BoundColumn>
								<asp:BoundColumn DataField="VIN" ItemStyle-CssClass="DataRow" HeaderText="VIN" HeaderStyle-Width="20%"></asp:BoundColumn>
								<asp:BoundColumn DataField="INSURED_VEH_NUMBER" ItemStyle-CssClass="DataRow" HeaderStyle-Width="7%"></asp:BoundColumn>
								<asp:BoundColumn DataField="VEHICLE_YEAR" ItemStyle-CssClass="DataRow" HeaderText="Year" HeaderStyle-Width="5%"></asp:BoundColumn>
								<asp:BoundColumn DataField="MAKE" ItemStyle-CssClass="DataRow" HeaderText="Make" HeaderStyle-Width="20%"></asp:BoundColumn>
								<asp:BoundColumn DataField="MODEL" ItemStyle-CssClass="DataRow" HeaderText="Model" HeaderStyle-Width="15%"></asp:BoundColumn>
								<asp:BoundColumn DataField="BODY_TYPE" ItemStyle-CssClass="DataRow" HeaderText="Body Type" HeaderStyle-Width="15%" Visible="False"></asp:BoundColumn>
								<%--<asp:BoundColumn DataField="APP_STATUS" ItemStyle-CssClass="DataRow" HeaderStyle-Width="15%" Visible="true"></asp:BoundColumn>--%>
							</Columns>
						</asp:datagrid></TD></TR>
  <TR class=midcolora id="trCOVERAGES" runat="server">
    <TD colSpan=4><asp:checkbox id=chkCopyCoverage Runat="server" CssClass="INPUT" Text="Copy Coverage" TextAlign="Right"></asp:checkbox></TD></TR>
  <TR>
    <TD class=midcolorr><cmsb:cmsbutton class=clsButton id=btnClose runat="server" Text="Close" CausesValidation="False"></cmsb:cmsbutton><cmsb:cmsbutton class=clsButton id=btnSubmit runat="server" Text="Submit"></cmsb:cmsbutton></TD></TR>
  <TR>
    <TD><INPUT id=hidCustomerID type=hidden value=0 
      name=hidCustomerID runat="server"> <INPUT 
      id=hidPolVersionID type=hidden value=0 name=hidPolVersionID 
       runat="server"> <INPUT id=hidPolID type=hidden 
      value=0 name=hidPolID runat="server"> <INPUT 
      id=hidCalledFrom type=hidden value=0 name=hidCalledFrom 
       runat="server"> <INPUT id=hidSavedStatus type=hidden 
      value=0 name=hidSavedStatus runat="server"> 
  </TD></TR></TABLE></FORM>
<script>
	//	window.onbeforeunload=UpdateGrid;
		//function UpdateGrid()
	//	{			
		//	if (document.CustomerVehicle.hidSavedStatus.value=='1')
		//	{
		//		window.opener.RefreshWebgrid(1,'',true);
		///		window.close();
	//		}
	//	}
		</script>


	</body>
</HTML>
