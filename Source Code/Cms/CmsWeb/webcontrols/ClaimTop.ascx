<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ClaimTop.ascx.cs" Inherits="Cms.CmsWeb.WebControls.ClaimTop" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<script>
	var refWindow;
	var refSubmitWindow;
	
	function ShowDialogEx()
	{
		if(parent.refSubmitWin !=null)
		{
			parent.refSubmitWin.close();
		}
		parent.refSubmitWin=window.open('/cms/application/Aspx/ShowDialog.aspx','BRICS','resizable=yes,scrollbars=yes,left=150,top=50,width=800,height=500');
		wBody=parent.refSubmitWin.document.body;			
		parent.refSubmitWin.document.write(document.getElementById("hidHTML").value);
		parent.refSubmitWin.document.title = "BRICS - Rules Mandatory Information";
	}
	function EnableMenu()
	{
		//top.topframe.enableMenu("1,2,3");
	}
	function ShowPopupClaimTop(url)
	{
		if(<%=intCustomerID%> != '0')
			var nuWin=window.open(url,'AttentionNotes','menubar=no,toolbar=no,location=no,resizable=no,scrollbars=no,status=no,width=600,height=200,top=286,left=240');
	}
	
	function ShowCustomerDetail()
	{
		if(<%=intCustomerID%> != '0')
			top.botframe.callItemClicked('1,2,0','/cms/client/aspx/CustomerTabIndex.aspx?CalledFrom=CLTTOP&CUSTOMER_ID=<%=CustomerID%>&');
	}
	
	function DisplayMessage()
	{
		if(document.getElementById('cltClaimTop:hidReturnPolicyStatus').value=="1")
		{
		 	alert('Application can be submitted only with in 15 days of Effective Date');
		}
		
		else if( document.getElementById('cltClaimTop:hidReturnPolicyStatus').value=="3")
		{
		       
			alert('Policy already exists.');
		      
		}
		else if(document.getElementById('cltClaimTop:hidReturnPolicyStatus').value=="4")
		{
			alert('Policy could not be created');
		  
		}
		else if(document.getElementById('cltClaimTop:hidCltNewBusinessProcess').value=="5")
		{
			alert('Policy Number ' + document.getElementById('cltClaimTop:hidReturnPolicyStatus').value  +  ' has been created successfully and accepted.');
		}
		else
		 {
		  alert('Policy Number ' + document.getElementById('cltClaimTop:hidReturnPolicyStatus').value  +  ' has been created successfully.');
		  
		 }
	}
	function CalledFromShow()
	{
		setTimeout('DisplayMessage()',1000);
	}
	
	
	function ShowApplicationDetail()
	{
		if(<%=intCustomerID%> != '0')
		{
			showType = '<%=ShowHeaderBand%>'
			if (showType == "Application")
				top.botframe.location.href = '/Cms/Application/aspx/ApplicationTab.aspx?CalledFrom=APP';
			else
				top.botframe.location.href = '/Cms/Policies/aspx/PolicyTab.aspx?Customer_ID=<%=intCustomerID%>&APP_ID=<%=intPolicyId%>&APP_VERSION_ID=<%=intPolicyVersionId%>&CalledFrom=POL';
		}
	}
	function ShowClaimDetail()
	{
		//top.botframe.location.href = 'ClaimsTab.aspx?Customer_ID=<%=intCustomerID%>&Policy_ID=<%=intPolicyId%>&Policy_version_ID=<%=intPolicyVersionId%>&Claim_ID=<%=intClaimId%>&LOB_ID=<%=intLobId%>';
		top.botframe.location.href = '/cms/Claims/aspx/ClaimsTab.aspx?Customer_ID=<%=intCustomerID%>&Policy_ID=<%=intPolicyId%>&Policy_version_ID=<%=intPolicyVersionId%>&Claim_ID=<%=intClaimId%>&LOB_ID=<%=intLobId%>';
	}
	
	
</script>
<script language="vbscript">
	Function getUserConfirmationToSubmitApp				
		getUserConfirmationToSubmitApp= msgbox("Would you like to submit the application to be converted into policy?",35,"CMS")
	End function
</script>
<table cellSpacing="0" cellPadding="0" width="100%" align="center">
	<input id="hidLOB" type="hidden" name="hidLOB" runat="server">
	<TR>
		<TD><asp:panel id="PanelClient" Visible="False" Runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%">
					<TR>
						<TD>
							<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer></TD>
					</TR>
				</TABLE>
				<TABLE class="tableeffectTopHeader" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD class="midcolora" vAlign="top" width="33%">
							<asp:Label id="lblName" Runat="server" Font-Bold="True"></asp:Label>:<br>
						<A class=CalLink 
            style="TEXT-DECORATION: underline" 
            href="javascript:ShowCustomerDetail()">
							<asp:Label id="lblFullName" Runat="server"></asp:Label></A>&nbsp;<A 
            href="/Cms/client/aspx/CustomerManagerIndex.aspx?customer_id=<%=intCustomerID%>" >
								<asp:Image id="CustomerDetail" Runat="server" Visible="True" Height="15" ToolTip="Customer Assistant"
									ImageAlign="absMiddle" BorderStyle="None" BorderWidth="0"></asp:Image></A><A 
            href="javascript:ShowPopupClaimTop('/Cms/Client/Aspx/AttentionNotes.aspx?ClientID=<%=intPolicyId%>')">
								<asp:Image id="AspImageNote" Runat="server" Visible="True" ImageAlign="absMiddle" BorderStyle="None"
									BorderWidth="0"></asp:Image></A>
						</TD>
						<TD class="midcolora" vAlign="top"  width="34%">
							<asp:Label id="lblAddress" Runat="server" Font-Bold="True"></asp:Label>:<br>
							<asp:Label id="lblClientAddress" Runat="server"></asp:Label>
						</TD>
						<TD class="midcolora" vAlign="top" width="33%">
							<asp:Label id="lblType" Runat="server" Font-Bold="True"></asp:Label>:<br>
							<asp:Label id="lblClientType" Runat="server"></asp:Label>
						</TD>
					</TR>
					<TR>
					    <TD class="midcolora" vAlign="top" width="33%">
							<asp:Label id="lblStatus" Runat="server" Font-Bold="True"></asp:Label>:<br>
							<asp:Label id="lblClientStatus" Runat="server"></asp:Label></TD>
						<TD class="midcolora" vAlign="top" width="34%">
							<asp:Label id="lblTitle" Runat="server" Font-Bold="True"></asp:Label>:<br>
							<asp:Label id="lblClientTitle" Runat="server"></asp:Label>
						</TD>
						<TD class="midcolora" vAlign="top" width="33%"">
							<asp:Label id="lblPhone" Runat="server" Font-Bold="True"></asp:Label>:</TD>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblClientPhone" Runat="server"></asp:Label></TD>
					</TR>
					<TR>
	
						
					</TR>
				</TABLE>
			</asp:panel></TD>
	</TR>
	<%--<TR>
		<TD><asp:panel id="PanelQuote" Visible="False" Runat="server">
      <TABLE cellSpacing=0 width="100%">
        <TR>
          <TD class=midcolora vAlign=top width="35%">
<asp:Label id=lblNameQ Runat="server" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblFullNameQ Runat="server"></asp:Label>&nbsp;<A 
            href="/Cms/client/aspx/CustomerManagerIndex.aspx?customer_id=<%=intCustomerID^>"> 
<asp:Image id=CustomerDetailQ Runat="server" Visible="True" BorderWidth="0" BorderStyle="None" ImageAlign="absMiddle" ToolTip="Customer Assistant" Height="15"></asp:Image></A>&nbsp; 
            <A 
            href="javascript:ShowPopupClaimTop('../Clients/AttentionNote.aspx?ClientID=<%=strClientID^>&amp;Type=Popup&amp;imgid=AspImageNoteQ','Attention',600,300)">
<asp:Image id=AspImageNoteQ Runat="server" Visible="True" BorderWidth="0" BorderStyle="None" ImageAlign="absMiddle"></asp:Image></A></TD>
          <TD class=midcolora vAlign=top width="35%">
<asp:Label id=lblQuoteQ Runat="server" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblQuoteNumberQ Runat="server"></asp:Label></TD>
          <TD class=midcolora vAlign=top width="35%">
<asp:Label id=lblAddressQuote Runat="server" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblClientAddressQuote Runat="server"></asp:Label></TD></TR>
        <TR>
          <TD class=midcolora vAlign=top width="35%">
<asp:Label id=lblQuoteVersion Runat="server" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblDataQuoteVersion Runat="server"></asp:Label></TD>
          <TD class=midcolora vAlign=top width="35%">
<asp:Label id=lblBrokerQ Runat="server" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblBrokerNameQ Runat="server"></asp:Label></TD>
          <TD class=midcolora vAlign=top width="35%">
<asp:Label id=lblBrokerContact Runat="server" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblBrokerContactName Runat="server"></asp:Label></TD></TR>
        <TR>
          <TD class=midcolora vAlign=top>
<asp:Label id=lblEffectiveDate Runat="server" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblEffectiveDateValue Runat="server"></asp:Label></TD>
          <TD class=midcolora vAlign=top>
<asp:Label id=lblQuoteStatus Runat="server" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblQuoteStatusValue Runat="server"></asp:Label></TD>
          <TD class=midcolora vAlign=top>
<asp:Label id=lblProductQ Runat="server" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblProductNameQ Runat="server"></asp:Label></TD></TR></TABLE>
			</asp:panel></TD>
	</TR>--%>
	<%--<TR>
		<TD><asp:panel id="pnlPolicy" Visible="False" Runat="server">
      <TABLE cellSpacing=0 width="100%">
        <TR>
          <TD class=midcolora vAlign=top align=left width="30%">
<asp:Label id=lblNameQP Runat="server" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblFullNameQP Runat="server"></asp:Label>&nbsp;<A 
            href="/Cms/client/aspx/CustomerManagerIndex.aspx?customer_id=<%=intCustomerID^>"> 
<asp:Image id=CustomerDetail2 Runat="server" Visible="True" BorderWidth="0" BorderStyle="None" ImageAlign="absMiddle" ToolTip="Customer Assistant" Height="15"></asp:Image></A>&nbsp; 
            <A 
            href="javascript:ShowPopupClaimTop('../Clients/AttentionNote.aspx?ClientID=<%=strClientID^>&amp;Type=Popup&amp;imgid=AspImageNote1','Attention',600,300)">
<asp:Image id=AspImageNote1 Runat="server" Visible="True" BorderWidth="0" BorderStyle="None" ImageAlign="absMiddle"></asp:Image></A></TD>
          <TD class=midcolora vAlign=top align=left width="25%">
<asp:Label id=lblAddressQP Runat="server" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblClientAddressQP Runat="server"></asp:Label></TD>
          <TD class=midcolora vAlign=top align=left width="15%">
<asp:Label id=lblBrokerQP Runat="server" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblBrokerNameQP Runat="server"></asp:Label></TD>
          <TD class=midcolora vAlign=top align=left width="30%">
<asp:Label id=lblBrokerContactNameP Runat="server" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblBrokerContactNamePText Runat="server"></asp:Label></TD></TR>
        <TR>
          <TD class=midcolora vAlign=top>
<asp:Label id=lblPolicy Runat="server" Visible="false" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblPolicyNumber Runat="server" Visible="false"></asp:Label></TD>
          <TD class=midcolora vAlign=top>
<asp:Label id=lblVer Runat="server" Visible="false" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblVersion Runat="server" Visible="false"></asp:Label></TD>
          <TD class=midcolora vAlign=top colSpan=2>
<asp:Label id=lblDate Runat="server" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblDateValue Runat="server" Visible="false"></asp:Label></TD></TR></TABLE>
			</asp:panel></TD>
	</TR>--%>
	<%--<TR>
		<TD><asp:panel id="pnlClaims" Visible="False" Runat="server">
      <TABLE cellSpacing=0 width="100%">
        <TR>
          <TD class=midcolora vAlign=top>
<asp:Label id=lblClaimant Runat="server" Visible="false" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblClaimantVal Runat="server"></asp:Label></TD>
          <TD class=midcolora vAlign=top>
<asp:Label id=lblInsuredName Runat="server" Visible="false" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblInsuredNameVal Runat="server"></asp:Label></TD>
          <TD class=midcolora vAlign=top>
<asp:Label id=lblPolVersion Runat="server" Visible="false" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblPolVersionVal Runat="server"></asp:Label></TD>
          <TD class=midcolora vAlign=top>
<asp:Label id=lblPolicyEfDate Runat="server" Visible="false" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblPolicyEfDateData Runat="server"></asp:Label></TD></TR>
        <TR>
          <TD class=midcolora vAlign=top>
<asp:Label id=lblClaimNo Runat="server" Visible="false" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblClaimNoVal Runat="server"></asp:Label></TD>
          <TD class=midcolora vAlign=top>
<asp:Label id=lblClaimClass Runat="server" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblClaimClassData Runat="server"></asp:Label></TD>
          <TD class=midcolora vAlign=top>
<asp:Label id=lblLossType Runat="server" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblLossTypeData Runat="server"></asp:Label></TD>
          <TD class=midcolora vAlign=top>
<asp:Label id=lblLosSubType Runat="server" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblLosSubTypeData Runat="server"></asp:Label></TD></TR>
        <TR>
          <TD class=midcolora vAlign=top>
<asp:Label id=lblInsuredClaimant Runat="server" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblInsureClaimantVal Runat="server"></asp:Label></TD>
          <TD class=midcolora vAlign=top>
<asp:Label id=lblDateofLoss Runat="server" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblDateofLossVal Runat="server"></asp:Label></TD>
          <TD class=midcolora vAlign=top>
<asp:Label id=lblAllocatedAdjuster Runat="server" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblAllocatedAdjusterVal Runat="server"></asp:Label></TD>
          <TD class=midcolora vAlign=top>&nbsp;</TD></TR>
        <TR>
          <TD class=midcolora vAlign=top colSpan=4>
<asp:Label id=lblClaimDesc Runat="server" Font-Bold="True"></asp:Label>:&nbsp; 
<asp:Label id=lblClaimDescVal Runat="server"></asp:Label></TD></TR></TABLE>
			</asp:panel></TD>
	</TR>--%>
	<TR>
		<TD><asp:panel id="pnlApplication" Visible="False" Runat="server" Width="100%">
				<TABLE cellSpacing="0" cellPadding="0" width="100%">
					<TR>
						<TD>
							<webcontrol:GridSpacer id="GridspacerApp1" runat="server"></webcontrol:GridSpacer></TD>
					</TR>
				</TABLE>
				<TABLE class="tableeffectTopHeader" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD class="midcolora" vAlign="top" width="30%">
							<asp:Label id="lblCustomerName" Runat="server" Visible="True" Font-Bold="True"></asp:Label>:<br>
							<A class="CalLink" style="TEXT-DECORATION: underline" href="javascript:ShowCustomerDetail()">
								<asp:Label id="lblSCustomerName1" Runat="server"></asp:Label>
							</a>
							&nbsp;<A href="/Cms/client/aspx/CustomerManagerIndex.aspx?customer_id=<%=intCustomerID%>">
									<asp:Image id="CustomerDetailQ2" Runat="server" Visible="True" Height="15" ToolTip="Customer Assistant"
										ImageAlign="absMiddle" BorderStyle="None" BorderWidth="0"></asp:Image></A>
									<A href="javascript:ShowPopupClaimTop('/Cms/Client/Aspx/AttentionNotes.aspx?ClientID=<%=intPolicyId%>')">
									<asp:Image id="Image1" Runat="server" Visible="True" ImageAlign="absMiddle" BorderStyle="None"
										BorderWidth="0"></asp:Image></A>							
							</TD>
						<TD class="midcolora" vAlign="top" width="30%">
							<asp:Label id="lblCustomerAddress" Runat="server" Visible="True" Font-Bold="True"></asp:Label>:<br>
							<asp:Label id="lblSCustomerAddress" Runat="server"></asp:Label></TD>
								
						<TD class="midcolora" vAlign="top" width="20%">
							<asp:Label id="lblCustomerType" Runat="server" Visible="True" Font-Bold="True"></asp:Label>:<BR />
							<asp:Label id="lblSCustomerType" Runat="server"></asp:Label></TD>
                        <TD class="midcolora" vAlign="top" width="20%">
							<asp:Label id="lblAgencyPhone" Runat="server" Font-Bold="True">Agency Phone</asp:Label>:<br>
							<asp:Label id="lblSAgencyPhone" Runat="server"></asp:Label></TD>								
					</TR>
					<TR>
						<TD class="midcolora" vAlign="top" colSpan="2" >
							<asp:Label id="lblAppNo" Runat="server" Font-Bold="True"></asp:Label>:<br>
							<A class="CalLink" style="TEXT-DECORATION: underline" href="javascript:ShowApplicationDetail()">
								<asp:Label id="lblSAppNo1" Runat="server"></asp:Label></A>
						</TD>
						<TD class="midcolora" vAlign="top" width="20%">
							<asp:Label id="lblAppVersion" Runat="server" Font-Bold="True"></asp:Label>:<br>
							<asp:Label id="lblSAppVersion" Runat="server"></asp:Label></TD>
						<TD class="midcolora" vAlign="top" width="20%">
							<asp:Label id="lblCustomerPhone" Runat="server" Font-Bold="True"></asp:Label>:<br>
							<asp:Label id="lblSCustomerPhone" Runat="server"></asp:Label></TD>		

					</TR>
					<TR>
						<TD class="midcolora" vAlign="top" colSpan="4">							
							<asp:Label id="lblClaimNumber" Runat="server" Visible="True" Font-Bold="True"></asp:Label>:<br />
							<%if(intClaimId!=0){%>
							<A class="CalLink" style="TEXT-DECORATION: underline" href="javascript:ShowClaimDetail()">
								<asp:Label id="lblSClaimNumber1" Runat="server"></asp:Label></A>
							<%}else{%>				
								<asp:Label id="lblSClaimNumber2" Runat="server">To be generated</asp:Label>
							<%}%>	
						</TD>
						
					</TR>
				</TABLE>
			</asp:panel></TD>
	</TR>
</table>
<script language="javascript">
top.frames[0].strLobId = document.getElementById('cltClaimTop_hidLOB').value;
</script>
<input id="hidSubmitApp" type="hidden" value="0" runat="server" NAME="hidSubmitApp">
<input id="hidPolicyId" type="hidden" value="0" name="hidPolicyId" runat="server">
<input type="hidden" id="hidCustomer_Id" runat="server" value="0" NAME="hidCustomer_Id">
<input type="hidden" id="hidApp_Id" runat="server" value="0" NAME="hidApp_Id"> <input type="hidden" id="hidApp_Version_Id" runat="server" value="0" NAME="hidApp_Version_Id">
<input type="hidden" id="hidReturnPolicyStatus" value="-10" runat="server" NAME="hidReturnPolicyStatus">
<input type="hidden" id="hidCltNewBusinessProcess" value="-100" runat="server" NAME="hidCltNewBusinessProcess">
