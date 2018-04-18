<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ClientTop.ascx.cs" Inherits="Cms.CmsWeb.WebControls.ClientTop" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<script>
	var refWindow;
	var refSubmitWindow;
	try {
	    if (ShowQuoteStatus != 'undefined') {
	        ShowQuoteStatus = '<%=intShowQuoteStatus %>';
	    }
	} catch (er) { }
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
	function ShowPopupClientTop(url)
	{
		var nuWin=window.open(url,'AttentionNotes','menubar=no,toolbar=no,location=no,resizable=no,scrollbars=no,status=no,width=600,height=200,top=286,left=240');
	}
	
	function ShowCustomerDetail()
	{
		top.botframe.callItemClicked('1,2,0','/cms/client/aspx/CustomerTabIndex.aspx?CalledFrom=CLTTOP&CUSTOMER_ID=<%=CustomerID%>&');
	}
	
	function DisplayMessage()
	{
		
		if(document.getElementById('cltClientTop:hidReturnPolicyStatus').value=="1")
		{
		 	alert('Application can be submitted only with in 15 days of Effective Date');
		}		
		else if( document.getElementById('cltClientTop:hidReturnPolicyStatus').value=="3")
		{		       
			alert('Policy already exists.');		      
		}
		else if(document.getElementById('cltClientTop:hidReturnPolicyStatus').value=="4")
		{
			alert('Policy could not be created');		  
		}
		else if(document.getElementById('cltClientTop:hidCltNewBusinessProcess').value=="5")
		{
			alert('Policy Number ' + document.getElementById('cltClientTop:hidReturnPolicyStatus').value  +  ' has been created successfully and accepted.');
			//Added to reload the page, so that save, del etc. buttons are not shown
			document.location.reload();			
		}
		else if(document.getElementById('cltClientTop:hidReturnPolicyStatus').value=="6")
		{
			alert('Application Cannot Converted to Policy. Contact Carrier - no underwriter assigned.');		  
		}
		else
		{
			alert('Policy Number ' + document.getElementById('cltClientTop:hidReturnPolicyStatus').value  +  ' has been created successfully.');
			//Added to reload the page, so that save, del etc. buttons are not shown
			//document.location.reload();
			document.location.href = '/Cms/Client/Aspx/CustomerManagerIndex.aspx';
		}
	}
	function CalledFromShow()
	{
		//DisplayMessage();
		setTimeout('DisplayMessage()',50);
	}
	function ShowClaim(ClaimId,PolVersionID)//Added Parameter PolVersionID by Charles on 14-Sep-09 for Itrack 6317
	{
		top.botframe.location.href = "/cms/Claims/aspx/ClaimsTab.aspx?CLAIM_ID=" + ClaimId + "&POLICY_VERSION_ID=" + PolVersionID; //POLICY_VERSION_ID added by Charles on 3-Sep-09 for Itrack 6317
	}
	
	
	function ShowApplicationDetail()
	{
		showType = '<%=ShowHeaderBand%>'
		if (showType == "Application")
		    top.botframe.location.href = '/Cms/Application/aspx/ApplicationTab.aspx?CalledFrom=APP';
		else
		    top.botframe.location.href = '/Cms/Policies/aspx/PolicyTab.aspx?'; //CalledFrom=APP'; //Removed by Charles on 21-Apr-10 for Policy Page
	}
	
	function SubmitAnyway(aintCustomerID,aintApplicationID,aintAppVersionID)
	{
		if(document.getElementById('cltClientTop_hidIsAgencyTerminated').value == '1')
		{
			alert("Commit process cannot be launched on this application/policy because the NBS termination date for the agency has been reached.");
			return;
		}
		if(refSubmitWindow!=null)
		{
			refSubmitWindow.close();				
		}				
		var url
		url="/cms/application/Aspx/ShowDialog.aspx?CUSTOMER_ID=" + aintCustomerID + "&APP_ID=" + aintApplicationID  + "&APP_VERSION_ID=" +  aintAppVersionID + "&LOBID=" +top.frames[0].strLobId + "&CALLEDFROM=SUBMITANYWAY" ;
		refSubmitWindow=window.open(url,"Quote","resizable=yes,scrollbars=yes,width=800,height=500,left=150,top=50");	
		
	}
	
	function SubmitApplication(aintCustomerID,aintApplicationID,aintAppVersionID)
	{
		//alert(document.getElementById('cltClientTop_hidIsAgencyTerminated').value);
		if(document.getElementById('cltClientTop_hidIsAgencyTerminated').value == '1')
		{
			alert("Commit process cannot be launched on this application/policy because the NBS termination date for the agency has been reached.");
			return;
		}
		
		//Prompt the user for confirmation to submit the application
		returnVal = getUserConfirmationToSubmitApp();		
		//2--Cancel....6--Yes...7---NO
		//If the user selects anything other than Yes, then leave
		if(returnVal!=6) return;
		//document.getElementById('cltClientTop:hidCustomer_Id').value=aintCustomerID;
		//document.getElementById('cltClientTop:hidApp_Id').value=aintApplicationID;
		//document.getElementById('cltClientTop:hidApp_Version_Id').value=aintAppVersionID;
		
		
		//if(document.getElementById('cltClientTop:hidSubmitApp').value=="0")
		//{
		//	document.getElementById('cltClientTop:hidSubmitApp').value="Submit";
		//	__doPostBack('','');
		//}
	
		if(refSubmitWindow!=null)
		{
			refSubmitWindow.close();				
		}				
		var url
		//url="/cms/application/Aspx/ShowDialog.aspx?CUSTOMER_ID=" + aintCustomerID + "&APP_ID=" + aintApplicationID  + "&APP_VERSION_ID=" +  aintAppVersionID + "&LOBID=" +top.frames[0].strLobId + "&CALLEDFROM=Image" ;
		url="/cms/application/Aspx/ShowDialog.aspx?CUSTOMER_ID=" + aintCustomerID + "&APP_ID=" + aintApplicationID  + "&APP_VERSION_ID=" +  aintAppVersionID + "&LOBID=" +top.frames[0].strLobId + "&CALLEDFROM=SUBMITAPP" ;
		refSubmitWindow=window.open(url,"Quote","resizable=yes,scrollbars=yes,width=800,height=500,left=150,top=50");	
		
		
		//window.s
	//	if(document.getElementById('cltClientTop:hidReturnPolicyStatus').value=="2")
		//	{
		// 	   alert("Application can be submitted only with in 15 days of Effective Date");
		 //   }
			
			/*
		else if(document.getElementById('cltClientTop:hidReturnPolicyStatus').value=="3")
		{
		  alert("Policy Created Successfully");
		}
		else if(document.getElementById('cltClientTop:hidReturnPolicyStatus').value=="-1")
		{
		  alert('Policy already exists.');
		}*/
		
	}
	function VerifyApplication(aintCustomerID,aintApplicationID,aintAppVersionID)
	{
		// if window is already open then first close that window then open.
		if(refWindow!=null)
		{
			refWindow.close();				
		}				
		var url
		url="/cms/application/Aspx/ShowDialog.aspx?CUSTOMER_ID=" + aintCustomerID + "&APP_ID=" + aintApplicationID  + "&APP_VERSION_ID=" +  aintAppVersionID + "&LOBID=" +top.frames[0].strLobId ;
		refWindow=window.open(url,"Quote","resizable=yes,scrollbars=yes,width=800,height=500,left=150,top=50");	
	}
	function VerifyPolicy(aintCustomerID, aintPolicyID, aintPolVersionID) {
	   
	    // if window is already open then first close that window then open.
	    if (refWindow != null) {
	        refWindow.close();
	    }
	    var url
	    url = "/cms/application/Aspx/ShowDialog.aspx?CALLEDFROM=<%=Cms.CmsWeb.cmsbase.CALLED_FROM_PROCESS_POLICY%>&CUSTOMER_ID=" + aintCustomerID + "&POLICY_ID=" + aintPolicyID + "&POLICY_VERSION_ID=" + aintPolVersionID + "&LOBID=" + top.frames[0].strLobId;
	    refWindow = window.open(url, "Quote", "resizable=yes,scrollbars=yes,width=800,height=500,left=150,top=50");
	}

	function ShowQuote(aintCustomerID, aintApplicationID, aintAppVersionID)
	{
	    // if window is already open then first close that window then open.
	    if (refWindow != null) {
	        refWindow.close();
	    }
	    var strLobId = '<%=strLOB_ID %>'; //get the Lob Id 
			
		var url
	//		 /cms/cmsweb/aspx/QuickQuoteRatingReport.aspx?ClientId=936&QuoteId=100&LOB=BOAT
		//url="/cms/application/Aspx/Quote/Quote.aspx?CUSTOMER_ID=" + aintCustomerID + "&APP_ID=" + aintApplicationID  + "&APP_VERSION_ID=" +  aintAppVersionID + "&LOBID=" +top.frames[0].strLobId ;
		
		/* Modified By Lalit For Pass  Policy_id And Policy_Version_id */
		url = "/cms/application/Aspx/QuoteGenerator.aspx?CUSTOMER_ID=" + aintCustomerID + "&APP_ID=" + aintApplicationID + "&APP_VERSION_ID=" + aintAppVersionID + "&LOB_ID=" + strLobId + "&POLICY_ID=" + aintApplicationID + "&POLICY_VERSION_ID=" + aintAppVersionID;
		refWindow=window.open(url,"Quote","resizable=yes,scrollbars=yes,width=800,height=500,left=150,top=50");	
	}
	
	function FetchUndisc(aintCustomerID,aintApplicationID,aintAppVersionID)
	{
	
		// if window is already open then first close that window then open.
		if(refWindow!=null)
		{
			refWindow.close();				
		}				
		var url
		
		//url="/cms/application/Aspx/QuoteGenerator.aspx?CUSTOMER_ID=" + aintCustomerID + "&APP_ID=" + aintApplicationID  + "&APP_VERSION_ID=" +  aintAppVersionID + "&LOB_ID=" +top.frames[0].strLobId ;
		//refWindow=window.open(url,"Fetch Undisclosed Driver/Vehicle","resizable=yes,scrollbars=yes,width=800,height=500,left=150,top=50");	
		url = "/cms/cmsweb/aspx/MvrForm.aspx?CUSTOMER_ID=" + aintCustomerID + "&APP_ID=" + aintApplicationID  + "&APP_VERSION_ID=" +  aintAppVersionID + "&LOB_ID=" +top.frames[0].strLobId + "&CalledFor=UDV";
		refWindow=window.open(url,null,"width=600,height=600,scrollbars=1,menubar=0,resizable=1"); 

	}
		
	function FetchUndiscPolicy(aintCustomerID,aintPolicyID,aintPolVersionID)
	{
		// if window is already open then first close that window then open.
		
			if(refWindow!=null)
			{
				refWindow.close();				
			}				
			var url
			
			//url="/cms/application/Aspx/QuoteGenerator.aspx?CUSTOMER_ID=" + aintCustomerID + "&APP_ID=" + aintApplicationID  + "&APP_VERSION_ID=" +  aintAppVersionID + "&LOB_ID=" +top.frames[0].strLobId ;
			//refWindow=window.open(url,"Fetch Undisclosed Driver/Vehicle","resizable=yes,scrollbars=yes,width=800,height=500,left=150,top=50");	
			url = "/cms/policies/aspx/PolMvrForm.aspx?CUSTOMER_ID=" + aintCustomerID + "&POLICY_ID=" + aintPolicyID  + "&POLICY_VERSION_ID=" +  aintPolVersionID + "&LOB_ID=" +top.frames[0].strLobId + "&CalledFor=UDV";
			refWindow=window.open(url,null,"width=600,height=600,scrollbars=1,menubar=0,resizable=1"); 
		
		

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
		<TD><asp:panel id="PanelClient" Runat="server" Visible="False">
				<TABLE cellSpacing="0" cellPadding="0" width="100%">
					<TR>
						<TD>
							<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer></TD>
					</TR>
				</TABLE>
				<TABLE class="tableeffectTopHeader" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD class="midcolora" vAlign="top" width="30%">
							<asp:Label id="lblName" Runat="server" Font-Bold="True"></asp:Label>:<br />
							<A class="CalLink" style="TEXT-DECORATION: underline" href="javascript:ShowCustomerDetail()">
								<asp:Label id="lblFullName" Runat="server"></asp:Label></A>&nbsp;<A 
            href="<%= EncryptCustomeridQstr%>" > 
								<asp:Image id="CustomerDetail" Runat="server"  BorderWidth="0" BorderStyle="None"
									ImageAlign="absMiddle" ToolTip='' Height="15"></asp:Image></A><A 
            href="javascript:ShowPopupClientTop('/Cms/Client/Aspx/AttentionNotes.aspx?ClientID=<%=intCustomerID%>')">
								<asp:Image id="AspImageNote" Runat="server" Visible="True" BorderWidth="0" BorderStyle="None"
									ImageAlign="absMiddle"></asp:Image></A>
						</TD>
						<TD class="midcolora" vAlign="top" width="30%">
							<asp:Label id="lblAddress" Runat="server" Font-Bold="True"></asp:Label>:<br />
							<asp:Label id="lblClientAddress" Runat="server"></asp:Label>
						</TD>
							
						<TD class="midcolora" vAlign="top" width="20%">
							<asp:Label id="lblType" Runat="server" Font-Bold="True"></asp:Label>:<br />
							<asp:Label id="lblClientType" Runat="server"></asp:Label></TD>
					    <TD class="midcolora" vAlign="top" width="20%">
							<asp:Label id="lblCustAgencyPhone" Runat="server" Font-Bold="True">Agency Phone</asp:Label>:<br />
							<asp:Label id="lblSCustAgencyPhone" Runat="server"></asp:Label></TD>		
					</TR>
					<TR>
						<TD class="midcolora" vAlign="top" width="30%">
							<asp:Label id="lblStatus" Runat="server" Font-Bold="True"></asp:Label>:<br/>
							<asp:Label id="lblClientStatus" Runat="server"></asp:Label>
						</TD>
						<TD class="midcolora" vAlign="top" width="30%">
							<asp:Label id="lblPhone" Runat="server" Font-Bold="True"></asp:Label>:<br />
							<asp:Label id="lblClientPhone" Runat="server"></asp:Label></TD>
						<TD class="midcolora" vAlign="top" colspan="2">
							<asp:Label id="lblTitle" Runat="server" Font-Bold="True"></asp:Label>:<br />
							<asp:Label id="lblClientTitle" Runat="server"></asp:Label></TD>
					</TR>
				</TABLE>
			</asp:panel></TD>
	</TR>
	<TR>
		<TD><asp:panel id="PanelQuote" Runat="server" Visible="False">
				<TABLE cellSpacing="0" width="100%">
					<TR>
						<TD class="midcolora" vAlign="top" width="35%">
							<asp:Label id="lblNameQ" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblFullNameQ" Runat="server"></asp:Label>&nbsp;<A 
            href="<%= EncryptCustomeridQstr%>">
								<asp:Image id="CustomerDetailQ" Runat="server" Visible="True" BorderWidth="0" BorderStyle="None"
									ImageAlign="absMiddle" ToolTip='' Height="15"></asp:Image></A>&nbsp;
							<A 
            href="javascript:ShowPopupClientTop('../Clients/AttentionNote.aspx?ClientID=<%=strClientID%>&amp;Type=Popup&amp;imgid=AspImageNoteQ','Attention',600,300)">
								<asp:Image id="AspImageNoteQ" Runat="server" Visible="True" BorderWidth="0" BorderStyle="None"
									ImageAlign="absMiddle"></asp:Image></A></TD>
						<TD class="midcolora" vAlign="top" width="35%">
							<asp:Label id="lblQuoteQ" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblQuoteNumberQ" Runat="server"></asp:Label></TD>
						<TD class="midcolora" vAlign="top" width="35%">
							<asp:Label id="lblAddressQuote" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblClientAddressQuote" Runat="server"></asp:Label></TD>
					</TR>
					<TR>
						<TD class="midcolora" vAlign="top" width="35%">
							<asp:Label id="lblQuoteVersion" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblDataQuoteVersion" Runat="server"></asp:Label></TD>
						<TD class="midcolora" vAlign="top" width="35%">
							<asp:Label id="lblBrokerQ" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblBrokerNameQ" Runat="server"></asp:Label></TD>
						<TD class="midcolora" vAlign="top" width="35%">
							<asp:Label id="lblBrokerContact" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblBrokerContactName" Runat="server"></asp:Label></TD>
					</TR>
					<TR>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblEffectiveDate" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblEffectiveDateValue" Runat="server"></asp:Label></TD>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblQuoteStatus" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblQuoteStatusValue" Runat="server"></asp:Label></TD>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblProductQ" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblProductNameQ" Runat="server"></asp:Label></TD>
					</TR>
				</TABLE>
			</asp:panel></TD>
	</TR>
	<TR>
		<TD><asp:panel id="pnlPolicy" Runat="server" Visible="False">
				<TABLE cellSpacing="0" width="100%">
					<TR>
						<TD class="midcolora" vAlign="top" align="left" width="33%">
							<asp:Label id="lblNameQP" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblFullNameQP" Runat="server"></asp:Label>&nbsp;<A 
            href="<%= EncryptCustomeridQstr%>">
								<asp:Image id="CustomerDetail2" Runat="server" Visible="True" BorderWidth="0" BorderStyle="None"
									ImageAlign="absMiddle" ToolTip='' Height="15"></asp:Image></A>&nbsp;
							<A 
            href="javascript:ShowPopupClientTop('../Clients/AttentionNote.aspx?ClientID=<%=strClientID%>&amp;Type=Popup&amp;imgid=AspImageNote1','Attention',600,300)">
								<asp:Image id="AspImageNote1" Runat="server" Visible="True" BorderWidth="0" BorderStyle="None"
									ImageAlign="absMiddle"></asp:Image></A><br />
						
							<asp:Label id="lblAddressQP" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblClientAddressQP" Runat="server"></asp:Label>
						</TD>
						<TD class="midcolora" vAlign="top" align="left" width="15%">
							<asp:Label id="lblBrokerQP" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblBrokerNameQP" Runat="server"></asp:Label></TD>
						<TD class="midcolora" vAlign="top" align="left" width="30%">
							<asp:Label id="lblBrokerContactNameP" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblBrokerContactNamePText" Runat="server"></asp:Label></TD>
					</TR>
					<TR>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblPolicy" Runat="server" Visible="false" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblPolicyNumber" Runat="server" Visible="false"></asp:Label></TD>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblVer" Runat="server" Visible="false" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblVersion" Runat="server" Visible="false"></asp:Label></TD>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblDate" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblDateValue" Runat="server" Visible="false"></asp:Label></TD>
					</TR>
				</TABLE>
			</asp:panel></TD>
	</TR>
	<TR>
		<TD><asp:panel id="pnlClaims" Runat="server" Visible="False">
				<TABLE cellSpacing="0" width="100%">
					<TR>
						<TD class="midcolora" vAlign="top" width="33%">
							<asp:Label id="lblClaimant" Runat="server" Visible="false" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblClaimantVal" Runat="server"></asp:Label><br />
							<asp:Label id="lblInsuredName" Runat="server" Visible="false" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblInsuredNameVal" Runat="server"></asp:Label></TD>
						<TD class="midcolora" vAlign="top" width="34%">
							<asp:Label id="lblPolVersion" Runat="server" Visible="false" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblPolVersionVal" Runat="server"></asp:Label></TD>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblPolicyEfDate" Runat="server" Visible="false" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblPolicyEfDateData" Runat="server"></asp:Label></TD>
					</TR>
					<TR>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblClaimNo" Runat="server" Visible="false" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblClaimNoVal" Runat="server"></asp:Label></TD>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblClaimClass" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblClaimClassData" Runat="server"></asp:Label></TD>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblLossType" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblLossTypeData" Runat="server"></asp:Label></TD>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblLosSubType" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblLosSubTypeData" Runat="server"></asp:Label></TD>
					</TR>
					<TR>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblInsuredClaimant" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblInsureClaimantVal" Runat="server"></asp:Label></TD>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblDateofLoss" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblDateofLossVal" Runat="server"></asp:Label></TD>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblAllocatedAdjuster" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblAllocatedAdjusterVal" Runat="server"></asp:Label></TD>
						<TD class="midcolora" vAlign="top">&nbsp;</TD>
					</TR>
					<TR>
						<TD class="midcolora" vAlign="top" colSpan="4">
							<asp:Label id="lblClaimDesc" Runat="server" Font-Bold="True"></asp:Label>:&nbsp;
							<asp:Label id="lblClaimDescVal" Runat="server"></asp:Label></TD>
					</TR>
				</TABLE>
			</asp:panel></TD>
	</TR>
	<TR>
		<TD><asp:panel id="pnlApplication" Runat="server" Visible="False" Width="100%">
				<%--<TABLE cellSpacing="0" cellPadding="0" width="100%">
					<TR>
						<TD>
							<webcontrol:GridSpacer id="GridspacerApp1" runat="server"></webcontrol:GridSpacer></TD>
					</TR>
				</TABLE>--%>
				<TABLE class="tableeffectTopHeader" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD class="midcolora" vAlign="top" width="30%">
							<asp:Label id="lblCustomerName" Runat="server" Visible="false" Font-Bold="True"></asp:Label>:<br />
						<A class="CalLink" style="TEXT-DECORATION: underline" href="javascript:ShowCustomerDetail()">
								<asp:Label id="lblSCustomerName" Runat="server"></asp:Label></A>&nbsp;<A 
            href="<%= EncryptCustomeridQstr%>">
								<asp:Image id="CustomerDetailQ2" Runat="server" Visible="True" BorderWidth="0" BorderStyle="None"
									ImageAlign="absMiddle" ToolTip='' Height="15"></asp:Image></A><A 
            href="javascript:ShowPopupClientTop('/Cms/Client/Aspx/AttentionNotes.aspx?ClientID=<%=intApplicationID%>')">
								<asp:Image id="Image1" Runat="server" Visible="True" BorderWidth="0" BorderStyle="None" ImageAlign="absMiddle"></asp:Image></A></TD>
						<TD class="midcolora" vAlign="top" width="32%">
							<asp:Label id="lblCustomerAddress" Runat="server" Visible="false" Font-Bold="True"></asp:Label>:<br/>
							<asp:Label id="lblSCustomerAddress" Runat="server"></asp:Label></TD>
						<TD class="midcolora" vAlign="top" width="10%">
							<asp:Label id="lblCustomerType" Runat="server" Visible="false" Font-Bold="True"></asp:Label>:<br/>
							<asp:Label id="lblSCustomerType" Runat="server"></asp:Label>
						</TD>
						<TD class="midcolora" vAlign="top" width="10%">
							<asp:Label id="lblCustomerPhone" Runat="server" Font-Bold="True"></asp:Label>:<br>
							<asp:Label id="lblSCustomerPhone" Runat="server"></asp:Label>
						</TD>
						<TD class="midcolora" vAlign="top" width="20%">
							<asp:Label id="lblAgencyPhone" Runat="server" Font-Bold="True">Agency Phone</asp:Label>:<br>
							<asp:Label id="lblSAgencyPhone" Runat="server"></asp:Label>
						</TD>		
					</TR>
					<TR>
						<TD class="midcolora" vAlign="top" colspan="2" width="70%">
							<asp:Label id="lblAppNo" Runat="server" Font-Bold="True"></asp:Label>:<br />
						<%
				if(strFlagApp!="APP")

				{
			%><A class="CalLink" style="TEXT-DECORATION: underline" href="javascript:ShowApplicationDetail()">
								<asp:Label id="lblSAppNo" Runat="server"></asp:Label></A>&nbsp;
							<%
				}
				else
				{				
			%>
							<asp:Label id="lblSAppNo1" Runat="server"></asp:Label>&nbsp;
							<%


				}
			%>
							<%if (ShowHeaderBand.ToUpper() == "POLICY")
			{%>
							<A 
            href="javascript:VerifyPolicy(<%=intCustomerID %>,<%=intPolicyId%>,<%=intPolicyVersionId%>)">
								<%
			}else
			{%>
								<A 
            href="javascript:VerifyApplication(<%=intCustomerID %>,<%=intApplicationID%>,<%=intAppVersionID%>)">
									<%}%>
									<asp:Image id="imgVerifyApp" Runat="server" style="display:none" Visible="True" BorderWidth="0" BorderStyle="None"
										ImageAlign="absMiddle" ToolTip="Verify" Height="15"></asp:Image></A>&nbsp;
								<%if (ShowHeaderBand.ToUpper() == "POLICY")
			{%>
							<A 
            href="javascript:FetchUndiscPolicy(<%=intCustomerID %>,<%=intPolicyId%>,<%=intPolicyVersionId%>)">
								<%
			}else
			{%>
								<A 
            href="javascript:FetchUndisc(<%=intCustomerID %>,<%=intApplicationID%>,<%=intAppVersionID%>)">
									<%}%>
									<asp:Image id="imgFetchUndisc" Runat="server" Visible="True" BorderWidth="0" BorderStyle="None"
										ImageAlign="absMiddle" ToolTip="Undisclosed Driver/Vehicle" Height="15"></asp:Image></A>&nbsp;
								<A 
            href="javascript:ShowQuote(<%=intCustomerID %>,<%=PolicyID%>,<%=PolicyVersionID%>)">
									<asp:Image id="imgQuote" Runat="server" Visible="True" BorderWidth="0" BorderStyle="None" ImageAlign="absMiddle"
										ToolTip="Quote" Height="15"></asp:Image></A>&nbsp; <A 
            href="javascript:SubmitAnyway(<%=intCustomerID %>,<%=intApplicationID%>,<%=intAppVersionID%>)">
									<asp:Image id="imgSubmitAnyway" Runat="server" Visible="True" BorderWidth="0" BorderStyle="None"
										ImageAlign="absMiddle" ToolTip="Submit Anyway" Height="15"></asp:Image></A>&nbsp;
								<A 
            href="javascript:SubmitApplication(<%=intCustomerID %>,<%=intApplicationID%>,<%=intAppVersionID%>)">
									<asp:Image id="imgSubmitApp" Runat="server" Visible="True" BorderWidth="0" BorderStyle="None"
										ImageAlign="absMiddle" ToolTip="Submit Application" Height="15"></asp:Image></A>
					    </TD>

                        <TD class="midcolora" vAlign="top" width="17%">
                          <asp:Label id="lblAppVersion" Runat="server" Font-Bold="True"></asp:Label>: 
                          <asp:Label id="lblSAppVersion" Runat="server"></asp:Label> 
                        </TD>
                        <TD class="midcolora" vAlign="top" colspan="2">
                           <asp:Label id="lblPolCurrency" Runat="server" Font-Bold="True"></asp:Label>:<br />
                           <asp:Label id="lblSPolCurrency" Runat="server"></asp:Label>
                        </TD>
            									
			        </TR>
			        <tr>
                        
			        
			        <td class="midcolora" vAlign="top" colspan="2" width="70%">
			        <asp:Label ID="lblEND" runat="server" Visible="false" Font-Bold="True" ></asp:Label>
			        <asp:Label ID="lblEND_DETAILS" runat="server" ></asp:Label>
			        </td>
			          <td class="midcolora" vAlign="top" width="17%">
			        </td>
			          <td class="midcolora" vAlign="top" width="17%" >
			        </td>
			           </td>
			          <td class="midcolora" vAlign="top" width="17%" >
			        </td>
			        </tr>
					<TR id="trClaimRow" style="DISPLAY: none" runat="server">
						<TD class="midcolora" vAlign="top" width="36%">
							<asp:Label id="lblClaimText" Runat="server" Font-Bold="True" Text="Claim No(s)."></asp:Label>:</TD>
						<TD class="midcolora" colSpan="2"><%=sbClaimLink.ToString()%></TD>
					</TR>
				</TABLE>
			</asp:panel></TD>
	</TR>
</table>
<script language="javascript">
if (top.frames[0]!=null)
{
    if(document.getElementById('cltClientTop:hidLOB'))
    top.frames[0].strLobId = document.getElementById('cltClientTop:hidLOB').value;
}
</script>
<input id="hidAppEffectiveDate" type="hidden" value="0" name="hidAppEffectiveDate" runat="server">
<input id="hidAppAgency" type="hidden" value="0" name="hidAppAgency" runat="server">
<input id="hidIsAgencyTerminated" type="hidden" value="0" name="hidIsAgencyTerminated" runat="server">
<input id="hidSubmitApp" type="hidden" value="0" name="hidSubmitApp" runat="server">
<input id="hidPolicyId" type="hidden" value="0" name="hidPolicyId" runat="server">
<input id="hidCustomer_Id" type="hidden" value="0" name="hidCustomer_Id" runat="server">
<input id="hidApp_Id" type="hidden" value="0" name="hidApp_Id" runat="server"> 
<input id="hidApp_Version_Id" type="hidden" value="0" name="hidApp_Version_Id" runat="server">
<input id="hidReturnPolicyStatus" type="hidden" value="-10" name="hidReturnPolicyStatus"
	runat="server"> <input id="hidCltNewBusinessProcess" type="hidden" value="-100" name="hidCltNewBusinessProcess"
	runat="server">
