<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PolicyTop.ascx.cs" Inherits="Cms.CmsWeb.WebControls.PolicyTop" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script>
	var refWindow;
	
	var policyTop = {};
	policyTop.imageIcon		=	new Array();
	policyTop.imageIcon[0]	=	new Image();
	policyTop.imageIcon[1]	=	new Image();
	
	policyTop.imageIcon[0].src	=	"/cms/cmsweb/Images/plus2.gif";
	policyTop.imageIcon[1].src	=	"/cms/cmsweb/Images/minus2.gif";
	
	function ShowDialog()
	{				
		window.open('/cms/application/Aspx/ShowDialog.aspx?CALLEDFROM=<%=Cms.CmsWeb.cmsbase.CALLED_FROM_PROCESS%>&CUSTOMER_ID=<%=intCustomerID %>&POLICY_ID=<%=intPolicyID%>&POLICY_VERSION_ID=<%=intPolicyVersionID%>&','Quote',"resizable=yes, scrollbars=yes,width=750,height=500");
	}
	
	function ShowPopup(url)
	{
		var nuWin=window.open(url,'AttentionNotes','menubar=no,toolbar=no,location=no,resizable=no,scrollbars=no,status=no,width=600,height=200,top=286,left=240');
	}
	
	function ShowCustomerDetail()
	{
		top.botframe.callItemClicked('1,2,0','/cms/client/aspx/CustomerTabIndex.aspx?CalledFrom=CLTTOP&CUSTOMER_ID=<%=CustomerID%>&');
	}
	
	function ShowPolicyDetail()
	{
		top.botframe.callItemClicked('1,2,3','/cms/policies/aspx/policytab.aspx?CUSTOMER_ID=<%=CustomerID%>&POLICY_ID=<%=PolicyID%>&POLICY_VERSION_ID=<%=PolicyVersionID%>&');
	}
	function ShowClaim(ClaimId,PolVersionID)//Added Parameter PolVersionID by Charles on 14-Sep-09 for Itrack 6317
	{
		top.botframe.location.href = "/cms/Claims/aspx/ClaimsTab.aspx?CLAIM_ID=" + ClaimId + "&POLICY_VERSION_ID=" + PolVersionID; //POLICY_VERSION_ID added by Charles on 14-Sep-09 for Itrack 6317
	}
	
	function showHidePolicyTop()
	{
		for(var j=7; j > 4; j--)
		{
			if(document.getElementById('P' + j).style.display	==	'none')
			{
				document.getElementById('P' + j).style.display	=	'inline';
			}
			else
			{
				document.getElementById('P' + j).style.display	=	'none';
			}
		}
		if(document.getElementById('policyTopLink').src == policyTop.imageIcon[0].src)
		{
			document.getElementById('policyTopLink').src	=	policyTop.imageIcon[1].src;
		}
		else
		{
			document.getElementById('policyTopLink').src	=	policyTop.imageIcon[0].src;
		}
	}
	
</script>
<table border="0" cellspacing="0" cellpadding="0" width="100%" class="headereffectCenter">
	<tr>
		<td width="95%">
		  <asp:Label ID="capheader" runat="server"></asp:Label>  <%--Policy Information--%>
		<td width="5%" align="right"><img id="policyTopLink" src=policyTop.imageIcon[0].src   onclick="javascript:showHidePolicyTop();"
				style="cursor:hand;"></td>
	</tr>
</table>
<TABLE class="tableeffectTopHeader" cellSpacing="1" cellPadding="0" width="100%" border="0" >
	<TR id="P1">
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capName" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%">
			<A class="CalLink" style="TEXT-DECORATION: underline" href="javascript:ShowCustomerDetail()">
				<asp:Label id="capFullName" Runat="server"></asp:Label></A>&nbsp;
				<A 
            href="/Cms/client/aspx/CustomerManagerIndex.aspx?customer_id=<%=CustomerID%>">
			<asp:Image id="CustomerDetail" Visible="True" Runat="server" Height="15" ToolTip="Customer Assistant"
									ImageAlign="absMiddle" BorderStyle="None" BorderWidth="0"></asp:Image></A>
				&nbsp;<A href="javascript:ShowPopup('/Cms/Client/Aspx/AttentionNotes.aspx?ClientID=<%=CustomerID%>&amp;Type=Popup&amp;imgid=AspImageNote1','Attention',600,300)"><asp:Image id="Image1" Visible="True" Runat="server" BorderWidth="0" BorderStyle="None" ImageAlign="absMiddle"></asp:Image></A>
				
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capNumber" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%">
			<A class="CalLink" style="TEXT-DECORATION: underline" href="javascript:ShowPolicyDetail()">
			<asp:Label id="capPolicyNumber" Runat="server"></asp:Label></a>
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capLOB" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%">
			<asp:Label id="capPolicyLOB" Runat="server"></asp:Label>
		</TD>
	</TR>
	<tr id="P2">
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capStatus" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%">
			<asp:Label id="capPolicyStatus" Runat="server"></asp:Label>
			<IMG id=imgPolicy style="CURSOR: hand" onclick=ShowDialog(); height=15 src="/cms/cmsweb/images<%=colorScheme%>/PolicyStatus.gif" >
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capState" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%">
			<asp:Label id="capPolicyState" Runat="server"></asp:Label>
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capVersion" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%">
			<asp:Label id="capPolicyVersion" Runat="server"></asp:Label>
		</TD>
	</tr>
	<tr id="P3">
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capSLOB" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%">
			<asp:Label id="capPolicySLOB" Runat="server"></asp:Label>
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capTermMonths" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%">
			<asp:Label id="capPolicyTermsMonths" Runat="server"></asp:Label>
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capAgency" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%">
			<asp:Label id="capPolicyAgency" Runat="server"></asp:Label>
		</TD>
	</tr>
	<tr id="P4">
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capEffectiveDate" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%">
			<asp:Label id="capPolicyEffectiveDate" Runat="server"></asp:Label>
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capInceptionDate" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%">
			<asp:Label id="capPolicyInceptionDate" Runat="server"></asp:Label>
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capExpirationDate" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%">
			<asp:Label id="capPolicyExpirationDate" Runat="server"></asp:Label>
		</TD>
		
	
	</tr>
	<tr id="P5">
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capCSR" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%">
			<asp:Label id="capPolicyCSR" Runat="server"></asp:Label>
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capBillType" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%">
			<asp:Label id="capPolicyBillType" Runat="server"></asp:Label>
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capSignature" Visible="false" Runat="server" Font-Bold="True"></asp:Label>&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%">
			<asp:Label id="capPolicySignature" Visible="false" Runat="server"></asp:Label>
		</TD>
	</tr>
	<tr id="P6">
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capUnderWriter" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%">
			<asp:Label id="capPolicyUnderWriter" Runat="server"></asp:Label>
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capInstallmentPlan" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%">
			<asp:Label id="capPolicyInstallmentPlan" Runat="server"></asp:Label>
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capChargeOffPremium" Visible="false" Runat="server" Font-Bold="True"></asp:Label>&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%">
			<asp:Label id="capPolicyChargeOffPremium" Visible="false" Runat="server"></asp:Label>
		</TD>
	</tr>
	<tr id="P7" >
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capReceivedPremium" Visible="false" Runat="server" Font-Bold="True"></asp:Label>&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%">
			<asp:Label id="capPolicyReceivedPremium" Visible="false" Runat="server"></asp:Label>
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capBonus" Visible="false" Runat="server" Font-Bold="True"></asp:Label>&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%">
			<asp:Label id="capPolicyBonus" Visible="false" Runat="server"></asp:Label>
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capYear" Visible="false" Runat="server" Font-Bold="True"></asp:Label>&nbsp;
		</TD>
		<TD class="midcolora" vAlign="top" align="left" width="20%">
			<asp:Label id="capPolicyYear" Visible="false" Runat="server"></asp:Label>
		</TD>
	</tr>
	<tr id="trClaimRow" runat="server">
		<TD class="midcolora" vAlign="top" align="left" width="13%">
			<asp:Label id="capClaimText" Runat="server" Font-Bold="True" Text="Claim No(s)."></asp:Label>:
		</TD>
		<TD class="midcolora" vAlign="top" align="left" colspan="5">
			<%=sbClaimLink.ToString()%>
		</TD>		
	</tr>
	<tr>
		<td>
			<asp:DropDownList ID="cmbPolicyTermMethods" Runat="server" Visible="False"></asp:DropDownList>
			<asp:DropDownList ID="cmbBillType" Runat="server" Visible="False"></asp:DropDownList>
			<asp:DropDownList ID="cmbUnderWriter" Runat="server" Visible="False"></asp:DropDownList>
			<asp:DropDownList ID="cmbInstallmentPlan" Runat="server" Visible="False"></asp:DropDownList>
		</td>
	</tr>	
</TABLE>
<input id="hidLOB" type="hidden" name="hidLOB" runat="server"> <input id="hidAgencyID" type="hidden" name="hidAgencyID" runat="server">
<input id="hidPolicyTermMethods" type="hidden" name="hidPolicyTermMethods" runat="server">
<input id="hidBillType" type="hidden" name="hidBillType" runat="server"> <input id="hidUnderWriter" type="hidden" name="hidUnderWriter" runat="server">
<input id="hidInstallPlanID" type="hidden" name="hidInstallPlanID" runat="server">
<script language="javascript">
    top.frames[0].strLobId = document.getElementById('cltPolicyTop_hidLOB').value; //changed from cltPolicyTop:hidLOB, Charles, 5-Mar-10, policy page implementation
showHidePolicyTop();
</script>
