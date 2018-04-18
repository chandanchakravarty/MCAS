<%@ Page language="c#" Codebehind="AddCoverageRange.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AddCoverageRange" validateRequest=false %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MNT_COVERAGE_RANGES</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		function FormatCurrency(obj)
		{
			obj.value=formatCurrency(obj.value);				
		}
		
		function AddData()
		{
			ChangeColor();
			DisableValidators();
			//document.getElementById('hidLIMIT_DEDUC_ID').value	=	'New';
			//document.getElementById('cmbLIMIT_DEDUC_TYPE').focus();
			//document.getElementById('cmbLIMIT_DEDUC_TYPE').selected = '';
			//document.getElementById('cmbLIMIT_DEDUC_TYPE').options.selectedIndex = -1;
			//document.getElementById('txtLIMIT_DEDUC_AMOUNT').value  = '';
		}
	function populateXML()
	{		
		if(document.getElementById('hidFormSaved')!=null &&( document.getElementById('hidFormSaved').value == '0'||document.getElementById('hidFormSaved').value == '1'))
				{
					var tempXML='';
					tempXML=document.getElementById('hidOldData').value;		
					if(tempXML!="" && tempXML!=0)
					{											
						populateFormData(tempXML,MNT_COVERAGE_RANGES);
					}
					else
					{
						AddData();
					}
				}
			return false;
			}
			
			function CheckAll( checkAllBox )
			{	
				var frm = document.MNT_COVERAGE_RANGES;			
				var actVar = checkAllBox.checked ;
				for(i=0;i< frm.length;i++)
					{					
					 e=frm.elements[i];
					 if ( e.type=='checkbox' && e.name.indexOf("chkActivate") != -1 )
						  e.checked= actVar ;
					}
			}
			function UnCheck(chkBox)
			{			
				var count=0;
				var frm = document.MNT_COVERAGE_RANGES ;
				for(i=0;i< frm.length;i++)
				{
					e=frm.elements[i];
					if ( e.type=='checkbox' && e.name.indexOf("chkSelectAll") != -1 )
					{
						e.checked= false ;
						break;
					}
				}
			}
		/*	if (document.Form1.wbtnSubmit.isDisabled == true)
			{			
			for(i=0;i< frm.length;i++)
			{	
				e=frm.elements[i];
				if ( e.type=='checkbox' && e.name.indexOf("ChkChannelChecked") != -1 && e.id!=chkBox.id)
				{					
					e.checked=false;					
				}
			}
					document.all.item("Hidden1").value="show";
					frm.submit();
			}*/			 
			
		
			
		function CheckEndDate(objSource , objArgs)
		{
			var enddate =objArgs.Value;
			var StartDateId=objSource.getAttribute("StartDateId");
			var startdate=document.getElementById(StartDateId).value;
			objArgs.IsValid = CompareTwoDate(startdate,enddate,jsaAppDtFormat);
		}	
		
		function CheckDisabledDate(objSource , objArgs)
		{
			
			var disableddate=objArgs.Value;
			var txtEndDate=objSource.getAttribute("EndDateId");
			var enddate=document.getElementById(txtEndDate).value;
			objArgs.IsValid = CompareTwoDate(enddate,disableddate,jsaAppDtFormat);
			
		}	
		
		function CompareTwoDate(DateFirst, DateSec, FormatOfComparision)
		{
			if(DateFirst == "" ||DateFirst ==null )
				return false;
			if(DateSec == "" || DateSec==null)
				return true;
			var saperator = '/';
			var firstDate, secDate;
			var strDateFirst = DateFirst.split("/");
			var strDateSec = DateSec.split("/");
			if(FormatOfComparision.toLowerCase() == "dd/mm/yyyy")
			{			
				firstDate = (strDateFirst[1].length != 2 ? '0' + strDateFirst[1] : strDateFirst[1]) + '/' + (strDateFirst[0].length != 2 ? '0' + strDateFirst[0] : strDateFirst[0])  + '/' + (strDateFirst[2].length != 4 ? '0' + strDateFirst[2] : strDateFirst[2]);
				secDate = (strDateSec[1].length != 2 ? '0' + strDateSec[1] : strDateSec[1]) + '/' + (strDateSec[0].length != 2 ? '0' + strDateSec[0] : strDateSec[0])  + '/' + (strDateSec[2].length != 4 ? '0' + strDateSec[2] : strDateSec[2]);
			}
			if(FormatOfComparision.toLowerCase() == "mm/dd/yyyy")
			{				
				firstDate = DateFirst
				secDate = DateSec;
			}
			firstDate = new Date(firstDate);
			secDate = new Date(secDate);
			firstSpan = Date.parse(firstDate);
			secSpan = Date.parse(secDate);
			//alert(firstSpan + '    '  + secSpan);
			if(firstSpan <= secSpan) 
				return true;	// first is less than or equal
			else 
				return false;	// First date is greater
		}
	
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="ApplyColor();">
		<FORM id="MNT_COVERAGE_RANGES" method="post" runat="server">
			<TABLE class="tableWidthHeader" cellSpacing="0" cellPadding="0" align="center" border="0">
				<TR class="midcolora">
					<% if (hidCalledFor.Value =="Limit") {%>
					<TD class="headereffectcenter" colSpan="2"><asp:label id="headerLimit" runat="server">Limit Ranges</asp:label></TD>
					<% } else if(hidCalledFor.Value =="Deduct" && (hidLOBID.Value=="6" || hidLOBID.Value=="1")) {%>
					<TD class="headereffectcenter" colSpan="2"><asp:label id="headerAdditional" runat="server">Additional Ranges</asp:label></TD>
					
					<% } else {%>
					<TD class="headereffectcenter" colSpan="2"><asp:label  id="headerDeductible" runat="server">Deductible Ranges</asp:label></TD>
					<% }%>
				</TR>
				<TR>
					<td class="midcolorc" align="right" colSpan="2"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</TR>
				<TR class="midcolora">
					<td colSpan="2"><asp:datagrid id="dgrLimitRanges" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="HeadRow"
							DataKeyField="LIMIT_DEDUC_ID" Width="100%">
							<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
							<ItemStyle CssClass="midcolora"></ItemStyle>
							<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="LIMIT_DEDUC_ID" Visible="false"></asp:BoundColumn>
								<asp:BoundColumn DataField="IS_ACTIVE" Visible="False"></asp:BoundColumn>
								<asp:TemplateColumn  HeaderText="Rank" HeaderStyle-Width="5%">
									<ItemTemplate>
										<asp:TextBox size=5 ID="txtRank" Runat=server MaxLength=4 CssClass="INPUTCURRENCY" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RANK")%>'>
										</asp:TextBox>
										<input id="hidLIMIT_DEDUC_ID" type="hidden" runat="server" value='<%# DataBinder.Eval(Container.DataItem,"LIMIT_DEDUC_ID")%>'>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Amount 1" HeaderStyle-Width="10%">
									<ItemTemplate>
										<asp:TextBox ID="txtAmount" size=13 CssClass="INPUTCURRENCY" Runat="server" MaxLength="20" Text='<%#String.Format("{0:,#,###}",DataBinder.Eval(Container.DataItem, "LIMIT_DEDUC_AMOUNT"))%>' onblur="FormatCurrency(this);">
										</asp:TextBox>
										<asp:RegularExpressionValidator ID="revLIMIT_DEDUC_AMOUNT" Runat="server" ControlToValidate="txtAmount" Display="Dynamic"></asp:RegularExpressionValidator>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Description" HeaderStyle-Width="18%">
									<ItemTemplate>
										<asp:TextBox ID="txtAmountText" size=27 Runat="server" MaxLength="20" Text='<%# DataBinder.Eval(Container.DataItem, "LIMIT_DEDUC_AMOUNT_TEXT")%>'>
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<asp:Label ID="lblSlash" Runat="server">/</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Amount 2" HeaderStyle-Width="10%">
									<ItemTemplate>
										<asp:TextBox ID="txtAmount1" size="13" CssClass="INPUTCURRENCY" Runat="server" MaxLength="20" Text='<%#String.Format("{0:,#,###}",DataBinder.Eval(Container.DataItem, "LIMIT_DEDUC_AMOUNT1"))%>'>
										</asp:TextBox>
										<br>
										<asp:RegularExpressionValidator ID="revAmoun1" Runat="server" ControlToValidate="txtAmount1" Display="Dynamic"></asp:RegularExpressionValidator>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Description" HeaderStyle-Width="18%">
									<ItemTemplate>
										</asp:TextBox>
										<asp:TextBox ID="txtAmount1Text" size=27 Runat="server" MaxLength="20" Text='<%# DataBinder.Eval(Container.DataItem, "LIMIT_DEDUC_AMOUNT1_TEXT")%>'>
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="From" HeaderStyle-Width="12%">
									<ItemTemplate>
										<asp:textbox id="txtEFFECTIVE_FROM_DATE" MaxLength="10" size="12" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EFFECTIVE_FROM_DATE")%>'>
										</asp:textbox>
										<asp:hyperlink id="hlkEFFECTIVE_FROM_DATE" runat="server" CssClass="HotSpot">
											<asp:Image runat="server" ID="imgEFFECTIVE_FROM_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></asp:Image>
										</asp:hyperlink>
										<br>
										<asp:RequiredFieldValidator Enabled="False" ID="rfvEFFECTIVE_FROM_DATE" ControlToValidate="txtEFFECTIVE_FROM_DATE"
											Runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator ID="revEFFECTIVE_FROM_DATE" ControlToValidate="txtEFFECTIVE_FROM_DATE" Runat="server"
											Display="Dynamic"></asp:RegularExpressionValidator>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Till" HeaderStyle-Width="12%">
									<ItemTemplate>
										<asp:textbox id="txtEFFECTIVE_TO_DATE" MaxLength="10" size="12" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EFFECTIVE_TO_DATE")%>'>
										</asp:textbox>
										<asp:hyperlink id="hlkEFFECTIVE_TO_DATE" runat="server" CssClass="HotSpot">
											<asp:Image runat="server" ID="imgEFFECTIVE_TO_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></asp:Image>
										</asp:hyperlink>
										<br>
										<asp:RegularExpressionValidator ID="revEFFECTIVE_TO_DATE" Display="Dynamic" Runat="server" ControlToValidate="txtEFFECTIVE_TO_DATE"></asp:RegularExpressionValidator>
										<asp:CustomValidator ID="csvEFFECTIVE_TO_DATE" Display="Dynamic" Runat="server" ControlToValidate="txtEFFECTIVE_TO_DATE"
											ClientValidationFunction="CheckEndDate"></asp:CustomValidator>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Disabled" HeaderStyle-Width="12%">
									<ItemTemplate>
										<asp:textbox id="txtDISABLED_DATE" MaxLength="10" size="12" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DISABLED_DATE")%>'>
										</asp:textbox>
										<asp:hyperlink id="hlkDISABLED_DATE" runat="server" CssClass="HotSpot">
											<asp:Image runat="server" ID="imgDISABLED_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></asp:Image>
										</asp:hyperlink><br>
										<asp:RegularExpressionValidator ID="revDISABLED_DATE" ControlToValidate="txtDISABLED_DATE" Runat="server" Display="Dynamic"></asp:RegularExpressionValidator>
										<asp:CustomValidator ID="csvDISABLED_DATE" ControlToValidate="txtDISABLED_DATE" Runat="server" Display="Dynamic"
											ClientValidationFunction="CheckDisabledDate"></asp:CustomValidator>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</TR>
				<TR>
					<td class="midcolora" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate1" runat="server" Text="Activate"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Deactivate"></cmsb:cmsbutton><asp:button id="btnPrevious" CssClass="clsButton" Text="Previous" CommandName="Previous" OnCommand="Navigation_Click"
							Runat="server"></asp:button><asp:button id="btnNext" CssClass="clsButton" Text="Next" CommandName="Next" OnCommand="Navigation_Click"
							Runat="server"></asp:button></td>
					<td class="midcolorr" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
				</TR>
			</TABLE>
			<INPUT id="hidCOV_ID" type="hidden" value="0" name="hidCOV_ID" runat="server"> <INPUT id="hidtotalPages" type="hidden" value="0" name="hidtotalPages" runat="server">
			<INPUT id="hidCurrentPage" type="hidden" value="0" name="hidCurrentPage" runat="server">
			<INPUT id="hidLineItemId" type="hidden" value="0" name="hidLineItemId" runat="server">
			<INPUT id="hidCalledFor" type="hidden" value="0" name="hidCalledFor" runat="server">
			<INPUT id="hidpageDefaultsize" type="hidden" value="0" name="hidpageDefaultsize" runat="server">
			<INPUT id="hidLimitType" type="hidden" value="0" name="hidLimitType" runat="server">
			<input type="hidden" id="hidOLD_DATA" runat="server">
			<INPUT id="hidLOBID" type="hidden" value="0" name="hidLOBID" runat="server">
		</FORM>
	</BODY>
</HTML>


