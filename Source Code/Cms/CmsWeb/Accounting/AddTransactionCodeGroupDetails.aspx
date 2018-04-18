<%@ Page validateRequest=false language="c#" Codebehind="AddTransactionCodeGroupDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Accounting.AddTransactionCodeGroupDetails" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>ACT_TRAN_CODE_GROUP_DETAILS</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">

		//This function open the tran lookup window
		function OpenNewLookupEx(RowNo)
		{
			var url='<%=url%>';	
			
			var txtCtrl = "dgTranCodeGrpDetails__ctl" + RowNo + "_txtDISPLAY_DESCRIPTION";
			var hidCtrl = "dgTranCodeGrpDetails__ctl" + RowNo + "_hidTRAN_ID";
			
			OpenLookupWithFunction(url,'TRAN_CODE1','DISPLAY_DESCRIPTION',hidCtrl,txtCtrl,'TransactionCodes','Transaction Codes','','splitPolicy( ' + RowNo + ')');					
		}

		//This function splits the tran id and put it in  controls
		function splitPolicy(RowNo)
		{
			var hidCtrl = "dgTranCodeGrpDetails__ctl" + RowNo + "_hidTRAN_ID";
			var txtTranCode = "dgTranCodeGrpDetails__ctl" + RowNo + "_txtTRAN_CODE";
			var hidTranId = "dgTranCodeGrpDetails__ctl" + RowNo + "_txtTRAN_CODE";
			
			document.getElementById(txtTranCode).value = document.getElementById(hidCtrl).value.split(',')[1];
			document.getElementById(hidTranId).value = document.getElementById(hidTranId).value.split(',')[0];
			EnableValidators(RowNo);
			
		}
		
		function EnableValidators(RowNo)
		{
			
			var rfvValidator = document.getElementById("dgTranCodeGrpDetails__ctl" + RowNo + "_rfvDEF_SEQ");
			rfvValidator.setAttribute("enabled", true);			
		}
		function ClearValidators()
		{
			var ctr = 2;
			var rfvValidator = document.getElementById("dgTranCodeGrpDetails__ctl" + ctr + "_rfvDEF_SEQ");
			
			while (rfvValidator != null)
			{
				if (rfvValidator == null)
				{
					break;
				}
				else
				{
					rfvValidator.setAttribute("enabled", false);
					rfvValidator.style.display = "none";
				}
				ctr++;
				var rfvValidator = document.getElementById("dgTranCodeGrpDetails__ctl" + ctr + "_rfvDEF_SEQ");				
			}
		}
		

		
		
		function CheckDelete()
		{
			for (ctr=1; ctr<=10; ctr++)
			{
				flag = 0;
				if(document.getElementById("dgTranCodeGrpDetails__ctl" + (ctr + 1)+ "_chkDelete") != null)
				{
					if (document.getElementById("dgTranCodeGrpDetails__ctl" + (ctr + 1)+ "_chkDelete").checked == true)
					{
						flag = 1;
						break;					
					}
				}
			}
			
			if (flag == 0)
			{	
				// Not a single row is selected , hence returning false
				alert("Please select the row, which you want to delete.");
				return false;
			}
		}	
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="ClearValidators();">
		<FORM id="ACT_TRAN_CODE_GROUP_DETAILS" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
							</tr>
							<tr>
								<td class="midcolora" colSpan="4"><asp:datagrid id="dgTranCodeGrpDetails" runat="server" DataKeyField="DETAIL_ID" AutoGenerateColumns="False"
										Width="100%" ItemStyle-Height="4">
										<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
										<ItemStyle CssClass="midcolora"></ItemStyle>
										<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
										<ItemStyle Height="4"></ItemStyle>
										<Columns>
											<asp:TemplateColumn ItemStyle-Width="1%">
												<ItemTemplate>
													<input type="hidden" id="hidDETAIL_ID" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "DETAIL_ID")%>' NAME="hidDETAIL_ID">
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Delete" ItemStyle-Width="5%">
												<ItemTemplate>
													<asp:CheckBox id="chkDelete" runat="server"></asp:CheckBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Transaction Code" ItemStyle-Width="12%">
												<ItemTemplate>
													<asp:textbox id="txtTRAN_CODE" runat="server" ReadOnly="True" size="15" maxlength="5" Text='<%# DataBinder.Eval(Container.DataItem, "TRAN_CODE")%>'>
													</asp:textbox>
													<asp:Image id="imgSelect"  style="CURSOR: hand" ImageUrl="../../cmsweb/images/selecticon.gif" Runat="server"></asp:Image>
													<INPUT id="hidTRAN_ID" type="hidden" name="hidTRAN_ID" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "TRAN_ID")%>'>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Transaction Description" ItemStyle-Width="15%">
												<ItemTemplate>
													<asp:textbox class="midcolora" style="border:none" id="txtDISPLAY_DESCRIPTION" runat="server" size="80" maxlength="120" readonly="True" Text='<%# DataBinder.Eval(Container.DataItem, "DISPLAY_DESCRIPTION")%>'>
													</asp:textbox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Default Sequence" ItemStyle-Width="8%">
												<ItemTemplate>
													<asp:textbox class="inputCurrency" id="txtDEF_SEQ" runat="server" size="10" maxlength="2" Text='<%# DataBinder.Eval(Container.DataItem, "DEF_SEQ")%>'>
													</asp:textbox>
													<span id="DefSeq0" style="display:none" class="mandatory">*</span><BR>
													<asp:requiredfieldvalidator id="rfvDEF_SEQ" runat="server" ControlToValidate="txtDEF_SEQ" ErrorMessage="Default Sequence can not be blank."
														Display="Dynamic"></asp:requiredfieldvalidator>
													<asp:regularexpressionvalidator id="revDEF_SEQ" runat="server" ControlToValidate="txtDEF_SEQ" ErrorMessage="Please insert integer value."
														Display="Dynamic"></asp:regularexpressionvalidator>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
									</asp:datagrid></td>
							</tr>
							<tr>
								<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" CausesValidation="false" Text="Reset"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnPrevious" runat="server" OnCommand="Navigation_Click" Text="Previous"
										CommandName="Previous"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnNext" runat="server" OnCommand="Navigation_Click" Text="Next"
										CommandName="Next"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete"></cmsb:cmsbutton></td>
								<td class="midcolorr" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidMaxRows" type="hidden" name="hidMaxRows" runat="server">
			<INPUT id="hidCurrentPage" type="hidden" name="hidCurrentPage" runat="server"> <INPUT id="hidTRAN_GROUP_ID" type="hidden" name="hidTRAN_GROUP_ID" runat="server">
			<INPUT id="hidpageDefaultsize" type="hidden" name="hidpageDefaultsize" runat="server">
		</FORM>
	</BODY>
</HTML>
