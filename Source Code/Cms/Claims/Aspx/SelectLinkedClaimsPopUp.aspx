<%@ Page language="c#" Codebehind="SelectLinkedClaimsPopUp.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.SelectLinkedClaimsPopUp" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title><%=headerstr%> </title><%--BRICS - Link Claim #--%>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script>
		function errMsg()
		{
			 document.getElementById('spnErrMessage').style.display="none";
		}
		
		function submitForm()
		{
			var GridPrefix = document.getElementById('hidDataGridID').value;			
			var NumberRows = document.getElementById('hidNumberRows').value;
			var ClaimDataPrefix = "lblCLAIM_CONCAT_STRING";
			var SelectPrefix = 'chkClaimNum';
			var SelectedData = new String();
			for(var iCount=NumberRows+1;iCount>0;iCount--)
			//for(var iCount=0;iCount<NumberRows+1;iCount++)
			{			
				var SelectID = GridPrefix + '__ctl' + iCount + '_' + SelectPrefix;
				if(document.getElementById(SelectID)==null || document.getElementById(SelectID)=='undefined')
					continue;
				if(document.getElementById(SelectID).checked)
				{
					var strClaimConcatData = GridPrefix + '__ctl' + iCount + '_' + ClaimDataPrefix;
					if(document.getElementById(strClaimConcatData)==null || document.getElementById(strClaimConcatData)=='undefined' || document.getElementById(strClaimConcatData).innerText=='')
						continue;
					/*if(SelectedData=='')
						SelectedData = document.getElementById(strClaimConcatData).innerText;
					else*/
						SelectedData = SelectedData + '~' + document.getElementById(strClaimConcatData).innerText;
				}				
			}
			if(SelectedData!='')
				SelectedData = SelectedData.substring(1,SelectedData.length);
			//alert(SelectedData);
			/*var Count=0;
			var strClaimList = "";					
			*/
			//We will send the data in the form of "~CLAIM_NUMBER^CUSTOMER_ID^POLICY_ID^POLICY_VERSION_ID^CLAIM_ID^LOB_ID^ADD_NEW^HOMEOWNER^RECR_VEH^IN_MARINE^LOSS_DATE^DIARY_ID"
			//SelectedData = "0106-0011^928^1^1^566^2^0^0^0^0^0^0~0106-0012^928^1^1^570^2^0^0^0^0^0^0";			
			
			if(window.opener.CreateLinkedClaimsLink!=null && window.opener.CreateLinkedClaimsLink!='undefined')
				window.opener.CreateLinkedClaimsLink(false,SelectedData);
			window.close();
			return false;
		}
		
		function closeForm()
		{
			window.close();	
		}		
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td class="headereffectCenter" colSpan="2">
						<asp:label id="lblTitle" runat="server"></asp:label><%--Select Claims to be linked--%>
					</td>
				</tr>
				<tr>
					<td align="center" colspan="2">
						<asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
					</td>
				</tr>
				<tr>
					<td class="midcolora" colspan="2">
						<B><asp:Label ID="capSearchOPtion" runat="server"></asp:Label>  </B>&nbsp;<%--Search Option:--%>
						<asp:DropDownList Runat="server" ID="drpSearchCols" AutoPostBack="false">
							<asp:ListItem Value="IN">Insured Name</asp:ListItem>
							<asp:ListItem Value="ST">Status</asp:ListItem>
							<asp:ListItem Value="CN">Claim Number</asp:ListItem>
							<%--<asp:ListItem Value="PN">Policy Number</asp:ListItem>--%>
							<%--<asp:ListItem Value="DL">Date of Loss</asp:ListItem>--%>
						</asp:DropDownList>
						&nbsp;&nbsp; <B><asp:Label ID="capSearchCriteria" runat="server"></asp:Label> &nbsp;</B> &nbsp;<%--Search Criteria:--%>
						<asp:TextBox Runat="server" ID="txtSearchCriteria"></asp:TextBox>
						&nbsp;&nbsp;
						<asp:Button Runat="server" ID="btnSearchResults" Text="Search" CssClass="clsButton"></asp:Button>
					</td>
				</tr>
				<tr>
					<td class="midcolora" colspan="2">
						<DIV style="OVERFLOW: scroll; HEIGHT: 295px;WIDTH: 575px">
							<asp:DataGrid id="dgClaimList" runat="server" AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
								<ItemStyle CssClass="midcolora"></ItemStyle>
								<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="">
										<ItemTemplate>
											<asp:CheckBox id="chkClaimNum" name="chkClaimNum" Runat="server"></asp:CheckBox>
											<asp:Label ID="lblCLAIM_CONCAT_STRING" Runat="server" style="display:none" Text='<%# DataBinder.Eval(Container, "DataItem.CLAIM_CONCAT_STRING") %>'>></asp:Label>
											<asp:Label ID="lblCLAIM_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.CLAIM_ID") %>'>></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn HeaderText="" DataField="CLAIM_NUMBER"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="" DataField="LOSS_DATE"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="" DataField="ADJUSTER_NAME"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="" DataField="CLAIMANT_NAME"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="" DataField="CLAIM_STATUS_DESC"></asp:BoundColumn>
								</Columns>
							</asp:DataGrid>
						</DIV>
					</td>
				</tr>
				<tr>
					<td class="midcolora">
						<cmsb:cmsbutton class="clsButton" id="btnClose" runat="server" Text="Close"></cmsb:cmsbutton>
					</td>
					<td class="midcolorr">
						<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Submit"></cmsb:cmsbutton>
					</td>
				</tr>
			</table>
			<input type="hidden" id="hidExistingClaimNumList" name="hidExistingClaimNumList" runat="server">
			<input type="hidden" id="hidExistingClaimID" name="hidExistingClaimID" runat="server">
			<input type="hidden" id="hidNumberRows" name="hidNumberRows" runat="server"> <input type="hidden" id="hidDataGridID" name="hidDataGridID" runat="server">
		</form>
	</body>
</HTML>
