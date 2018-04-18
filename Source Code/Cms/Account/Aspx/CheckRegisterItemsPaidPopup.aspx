<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="CheckRegisterItemsPaidPopup.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.CheckRegisterItemsPaidPopup" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Brics-Check Items To Be Paid</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script>
		var oldValue=0.00;//variable to old value of amount before change
		//Calls when user check the Apply all checkbox
		function OnApplyAll(ctrl)
		{
				prefix = "dgOpenItems__Ctl";
				var tmp = ctrl.id.substring(prefix.length, ctrl.id.length);
				var rowIndex = tmp.substring(0,tmp.indexOf('_'))
				txtAmtCtrl = document.getElementById(prefix + rowIndex + "_txtRECON_AMOUNT");
				oldValue =  txtAmtCtrl.value.length==0 || isNaN(txtAmtCtrl.value)?0:txtAmtCtrl.value;
				txtAmtCtrl.value = document.getElementById(prefix + rowIndex + "_lblDUE").innerHTML;
				SetTotal(txtAmtCtrl);			
		}
		//Checking whether the total reconcile amuont is greater then the balance amount
		function CheckTotalAmount(objSource, objArgs)
		{
			var ctrl = objSource.id.replace("csvRECON_AMOUNT","lblDUE");
			if (parseFloat(document.getElementById(ctrl).innerHTML) < parseFloat(document.getElementById(objSource.controltovalidate).value))
				objArgs.IsValid = false;
			else
				objArgs.IsValid = true;
		}
	
		function SetTotal(textCtrl)
		{
			var total = document.getElementById('txtTotal').value;
			
			ControlVal=textCtrl.value;				
			total	   = RemoveAllOccrancesOfChar(total,",");
			ControlVal = RemoveAllOccrancesOfChar(ControlVal,",");
			oldValue   = RemoveAllOccrancesOfChar(oldValue,",");
			
			total	   = parseFloat(total);
			ControlVal = parseFloat(ControlVal);
			oldValue   = parseFloat(oldValue);
						
			document.getElementById('txtTotal').value = formatCurrency((total + ControlVal)-oldValue)+" ";
		}
		function CalculateTotalOnLoad()
		{
			var prefix = "dgOpenItems__Ctl";
			var suffix = "_txtRECON_AMOUNT";
			var total=0;
			var i=0;
			var mode = '<%=Mode%>';
			while(document.getElementById(prefix+i+suffix)==null)
				i++;
			for(;document.getElementById(prefix+i+suffix)!=null;i++)
			{
				total+=parseFloat(document.getElementById(prefix+i+suffix).value);
				if(mode=="Register")//ie is popup is opened by check register
				{
					document.getElementById(prefix+i+suffix).style.border = "none";
					document.getElementById(prefix+i+suffix).setAttribute("disabled",true);
				}
			}		
			document.getElementById('txtTotal').value=formatCurrency(total)+" ";
			
		}
		
		function  ResetMyForm()
		{
			 document.location.href = document.location.href;
		}
		
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="CalculateTotalOnLoad();">
		<form id="ItemToBePaid" method="post" runat="server">
			<TABLE class="tablewidth" border="0" align="center">
				<tr>
					<td class="headereffectCenter" colSpan="6">
						<asp:label id="lblHeader" runat="server" text="Check Items To Be Paid"></asp:label>
					</td>
				</tr>
				<tr>
					<td class="midcolorc" align="right" colSpan="6"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<tr>
					<td class="midcolora" colSpan="6">
						<asp:datagrid id="dgOpenItems" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyField="IDEN_ROW_NO">
							<ALTERNATINGITEMSTYLE CssClass="midcolora"></ALTERNATINGITEMSTYLE>
							<ITEMSTYLE CssClass="midcolora"></ITEMSTYLE>
							<HEADERSTYLE CssClass="headereffectWebGrid"></HEADERSTYLE>
							<COLUMNS>
								<ASP:TEMPLATECOLUMN Visible="False">
									<ITEMTEMPLATE>
										<INPUT id="hidIDEN_ROW_NO" type="hidden" name="hidIDEN_ROW_NO" runat="server">
									</ITEMTEMPLATE>
								</ASP:TEMPLATECOLUMN>
								<ASP:TEMPLATECOLUMN Visible="true">
									<ITEMTEMPLATE>
										<INPUT id="hidSOURCE_ROW_ID" type="hidden" name="hidSOURCE_ROW_ID" value='<%# DataBinder.Eval(Container.DataItem, "SOURCE_ROW_ID")%>' runat="server">
									</ITEMTEMPLATE>
								</ASP:TEMPLATECOLUMN>
								<ASP:TEMPLATECOLUMN HeaderText="Select">
									<ITEMTEMPLATE>
										<ASP:CHECKBOX id="chkSelect" runat="server" onclick=""></ASP:CHECKBOX>
									</ITEMTEMPLATE>
								</ASP:TEMPLATECOLUMN>
								<ASP:TEMPLATECOLUMN HeaderText="Fetch" ItemStyle-HorizontalAlign="Center">
									<ITEMTEMPLATE>
										<asp:Image runat="server" ID="imgAPPLY_ALL" ImageUrl="../../cmsweb/images/selecticon.gif" style="CURSOR: hand"
											onclick="javascript:OnApplyAll(this);"></asp:Image>
									</ITEMTEMPLATE>
								</ASP:TEMPLATECOLUMN>
								<ASP:TEMPLATECOLUMN HeaderText="Amount To Apply" ItemStyle-Width="10%">
									<ITEMTEMPLATE>
										<ASP:TEXTBOX id="txtRECON_AMOUNT" onblur="javascript:this.value = formatCurrency(this.value);" onfocus="javascript:oldValue=this.value;" onchange="SetTotal(this);" size="12" CssClass="InputCurrency" text='<%# DataBinder.Eval(Container.DataItem, "RECON_AMOUNT")%>' Runat="server">
										</ASP:TEXTBOX>
										<ASP:REGULAREXPRESSIONVALIDATOR id="revRECON_AMOUNT" Runat="server" ControlToValidate="txtRECON_AMOUNT" Display="Dynamic"></ASP:REGULAREXPRESSIONVALIDATOR>
										<ASP:CustomValidator ID="csvRECON_AMOUNT" Runat="server" ControlToValidate="txtRECON_AMOUNT" Display="Dynamic"
											ClientValidationFunction="CheckTotalAmount" ErrorMessage="Amount can not be greater then net due amount."></ASP:CustomValidator>
										<input type="hidden" id="hidGROUP_ID" value ='<%# DataBinder.Eval(Container.DataItem, "GROUP_ID")%>'  runat="server" NAME="hidGROUP_ID">
									</ITEMTEMPLATE>
								</ASP:TEMPLATECOLUMN>
								<ASP:TEMPLATECOLUMN HeaderText="Item Type">
									<ITEMTEMPLATE>
										<ASP:LABEL id="lblITEM_TYPE" Runat="server" text='<%# DataBinder.Eval(Container.DataItem, "DISPLAY_UPDATED_FROM")%>' >
										</ASP:LABEL>
										<INPUT id="hidUPDATED_FROM" type="hidden" name="hidUPDATED_FROM" value='<%# DataBinder.Eval(Container.DataItem, "UPDATED_FROM")%>' runat="server">
									</ITEMTEMPLATE>
								</ASP:TEMPLATECOLUMN>
								<ASP:TEMPLATECOLUMN HeaderText="Number">
									<ITEMTEMPLATE>
										<ASP:LABEL id="ITEM_REFERENCE_ID" text='<%# DataBinder.Eval(Container.DataItem, "SOURCE_ROW_ID")%>' Runat="server">
										</ASP:LABEL>
									</ITEMTEMPLATE>
								</ASP:TEMPLATECOLUMN>
								<ASP:TEMPLATECOLUMN HeaderText="Transaction Code">
									<ITEMTEMPLATE>
										<ASP:LABEL id="lblTranCode" Runat="server"></ASP:LABEL>
									</ITEMTEMPLATE>
								</ASP:TEMPLATECOLUMN>
								<ASP:TEMPLATECOLUMN HeaderText="Effective Date">
									<ITEMTEMPLATE>
										<ASP:LABEL id="txtEffectiveDate" text='<%# DataBinder.Eval(Container.DataItem, "SOURCE_EFF_DATE")%>' Runat="server">
										</ASP:LABEL>
									</ITEMTEMPLATE>
								</ASP:TEMPLATECOLUMN>
								<ASP:TEMPLATECOLUMN HeaderText="Transaction Date">
									<ITEMTEMPLATE>
										<ASP:LABEL id="lblTransactionDate" text='<%# DataBinder.Eval(Container.DataItem, "SOURCE_TRAN_DATE")%>' Runat="server">
										</ASP:LABEL>
									</ITEMTEMPLATE>
								</ASP:TEMPLATECOLUMN>
								<ASP:TEMPLATECOLUMN HeaderText="Total Due" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="8%"
									ItemStyle-CssClass="midcolorr" ItemStyle-Wrap="False">
									<ITEMTEMPLATE>
										<ASP:LABEL id="Label1" Width="10%" text='<%# DataBinder.Eval(Container.DataItem, "TOTAL_DUE")%>' Runat="server">
										</ASP:LABEL>
									</ITEMTEMPLATE>
								</ASP:TEMPLATECOLUMN>
								<ASP:TEMPLATECOLUMN HeaderText="Net Due" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="8%" ItemStyle-CssClass="midcolorr"
									ItemStyle-Wrap="False">
									<ITEMTEMPLATE>
										<ASP:LABEL id="lblDUE" Width="10%" text='<%# DataBinder.Eval(Container.DataItem, "BALANCE")%>' Runat="server">
										</ASP:LABEL>
									</ITEMTEMPLATE>
								</ASP:TEMPLATECOLUMN>
								<ASP:TEMPLATECOLUMN HeaderText="Balance" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="15%" ItemStyle-CssClass="midcolorr"
									ItemStyle-Wrap="False">
									<ITEMTEMPLATE>
										<ASP:LABEL id="lblBALANCE" Width="10%" text='<%# DataBinder.Eval(Container.DataItem, "BALANCE")%>' Runat="server">
										</ASP:LABEL>
									</ITEMTEMPLATE>
								</ASP:TEMPLATECOLUMN>
							</COLUMNS>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td class="midcolora" width="2%" colSpan="1">
						Total:
					</td>
					<td class="midcolora" colSpan="5">
						<asp:TextBox ReadOnly="True" ID="txtTotal" Text="0.00" Runat="server" CssClass="midcolorr" style="BORDER-RIGHT:medium none; BORDER-TOP:medium none; BORDER-LEFT:medium none; BORDER-BOTTOM:medium none"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></td>
					<td class="midcolorr" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
				</tr>
				<tr>
					<td>
						<input id="hidENTITY_ID" type="hidden" name="hidENTITY_ID" runat="server"> <input id="hidCD_LINE_ITEM_ID" type="hidden" name="hidCD_LINE_ITEM_ID" runat="server">
						<input id="hidRECEIPT_AMOUNT" type="hidden" name="hidRECEIPT_AMOUNT" runat="server">
						<input type="hidden" id="hidPARENT_GROUP_ID" name="hidPARENT_GROUP_ID" runat="server">
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
