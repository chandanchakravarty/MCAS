<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Page language="c#" Codebehind="CreateCheck.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.CreateCheck" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>BRICS-<%=Request.QueryString["Mode"]==null?"":"Add "%>  Items to Reconcile</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK href="/cms/cmsweb/css/css<%=base.GetColorScheme()%>.css" type=text/css rel=stylesheet>
    <script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		var SelectedCheckBoxes=<%=NoOfRows%>;
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";

		function CheckBoxClicked(objCheckBox)
		{
			if(objCheckBox.checked)
				SelectedCheckBoxes++;
			else
				SelectedCheckBoxes--;
			if(SelectedCheckBoxes>0)
			{
				if(document.getElementById('btnCreateChecks')!=null)
					document.getElementById('btnCreateChecks').setAttribute('disabled',false);
			}
			else
			{
				if(document.getElementById('btnCreateChecks')!=null)
					document.getElementById('btnCreateChecks').setAttribute('disabled',true);
			}
		}
		
		function CheckboxValidate()
		{
			var chkCount = 0
			var checkBoxID = 'chkSelect';	
				re = new RegExp(':' + checkBoxID + '$')  //generated controlname starts with a colon
				
				for(i = 0; i < document.Form1.elements.length; i++) 
				{
					elm = document.Form1.elements[i]

					if (elm.type == 'checkbox') 
					{
						if (re.test(elm.name)) 
						if (elm.checked)
						{
						//alert(elm.checked)
						 chkCount = chkCount + 1;
						}
					}
				}
			if (chkCount == 0)
			  {
					alert("Please Select atleast One Record.")
					return false;
			  }	
		}
		
		function ChkFromDate(objSource , objArgs)
		{
			//var currentDate="<%=DateTime.Now.Date.ToString().Substring(0,DateTime.Now.Date.ToString().IndexOf(' '))%>";
			//var currentDate=new Date("<%=DateTime.Now.Date.ToString().Substring(0,DateTime.Now.Date.ToString().IndexOf(' '))%>");
			//alert(currentDate);
			var expdate=document.Form1.txtFromDate.value;
			
			//Commented by Ravindra(iTrack 4215): Will allow to issue checks against futute effective transactions
			//objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",expdate,jsaAppDtFormat);
			//if(objArgs.IsValid==false)
			  //document.getElementById('revDRIVER_DOB').style.display="none"
		}	
		function ChkToDate(objSource , objArgs)
		{
			//var currentDate="<%=DateTime.Now.Date.ToString().Substring(0,DateTime.Now.Date.ToString().IndexOf(' '))%>";
			//var currentDate=new Date("<%=DateTime.Now.Date.ToString().Substring(0,DateTime.Now.Date.ToString().IndexOf(' '))%>");
			//alert(currentDate);
			var expdate=document.Form1.txtToDate.value;
			//Commented by Ravindra(iTrack 4215): Will allow to issue checks against futute effective transactions
			//objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",expdate,jsaAppDtFormat);
			//if(objArgs.IsValid==false)
			  //document.getElementById('revDRIVER_DOB').style.display="none"
		}
		
		
		function back()
		{
			
			parent.document.location.href = "AG_CheckTypeSelect.aspx";
			return false;
		}
		
		function  RemoveValidation()
		{			
			return false;			
			document.getElementById('rfvFromDate').setAttribute('enabled',false);
			document.getElementById('rfvToDate').setAttribute('enabled',false);			
			document.getElementById('rfvToDate').style.display = "none";
			document.getElementById('rfvFromDate').style.display = "none";					
			
		}
		
		function SelectDropdownOptionByVal(combo,selectedValue)
		{

			for(var j=0; j<combo.options.length; j++)
			{
				if(selectedValue == combo.options[j].value)
				{
					combo.options.selectedIndex = j;
					break;
				}
			}
		}		
		

		function y2k(number) 
		{ 
		return (number < 1000) ? number + 1900 : number; 
		}

		function daysElapsed(date1,date2) 
		{
			var difference =Date.UTC(y2k(date1.getYear()),date1.getMonth(),date1.getDate(),0,0,0)- Date.UTC(y2k(date2.getYear()),date2.getMonth(),date2.getDate(),0,0,0);
			return difference/1000/60/60/24;
		 }

		function CheckDateDiff() 
		{ 
			
			var date_format = "%m/%d/%y";
			var firstObj, secondObj;
			with (document.Form1) 
			{
				firstObj =  new Date(document.getElementById('txtFromDate').value);
				secondObj = new Date(document.getElementById('txtToDate').value);
			}
		
			if (!(typeof firstObj == "object"))
			{
				alert("The First Date is not in a valid format!!");
				return;
			}
			if (!(typeof secondObj == "object")) 
			{
				alert("The Second Date is not in a valid format!!");
				return;
			}

			/*var days = daysElapsed(firstObj,secondObj);
			if (days < 0) { days *= -1; }
			if (days < 10 )
			{
			 alert('There should me a minimum 10 days gap between the two dates')
			 return false;
			}*/
		return true;
		}
				
		function DateValidators()
		{
			if(document.getElementById('csvToDate').style.display = "inline")
				return false;
			else
				return true;
		}
		
		function chkSelDeSelAll(chkAllctrl)
		{
			if (chkAllctrl.checked)
			{
				aspCheckBoxID = 'chkSelect';	//for seond checkbox section
				re = new RegExp(':' + aspCheckBoxID + '$')  
				for(i = 0; i < document.Form1.elements.length; i++)
				{
					chkAllctrl = document.Form1.elements[i]
					if (chkAllctrl.type == 'checkbox')
					{
						if (re.test(chkAllctrl.name)) 
						{chkAllctrl.checked = true;CheckBoxClicked(chkAllctrl);	}
					}
				}
			}
			else
			{
				aspCheckBoxID = 'chkSelect';	//for seond checkbox section
				re = new RegExp(':' + aspCheckBoxID + '$')  
				for(i = 0; i < document.Form1.elements.length; i++)
				 {
					chkAllctrl = document.Form1.elements[i]
					if (chkAllctrl.type == 'checkbox')
					{
						if (re.test(chkAllctrl.name)) 
						{chkAllctrl.checked = false;CheckBoxClicked(chkAllctrl);}	
					}
				}
			}
		}
		
		function Uncheck()
		{
			document.getElementById('chkSelectAll').checked = false;
		}
		
		function formReset()
		{
			document.location.href = "CreateCheck.aspx";
		    return false;
		}
	</script>	
  </head>
  <body leftmargin="0" topmargin="0" MS_POSITIONING="GridLayout" onload="ChangeColor();DisableValidators();ApplyColor();Uncheck();">
	<div id="bodyHeight" class="pageContent">
		<form id="Form1" method="post" runat="server">
			<table class="tableWidthHeader" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<TABLE id="Table1" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td>
									<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
									<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
								</td>
							</tr>
							<tr>
								<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
									mandatory</TD>
							</tr>
							<tr>
								<td class="midcolorc" colSpan="5" style="WIDTH: 1250px"><asp:label id="lblMessage" runat="server" CssClass="errmsg" align="center" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" colSpan="1" width="25%">Transaction From Date</TD>
								<TD class="midcolora" colSpan="1" width="25%"><asp:textbox id="txtFromDate" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkFromDate" runat="server" CssClass="HotSpot">
										<ASP:IMAGE id="imgFromDate" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
									</asp:hyperlink><%--<br>
									<asp:requiredfieldvalidator id="rfvFromDate" runat="server" ControlToValidate="txtFromDate" ErrorMessage="txtFromDate can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator>--%><br>
									<asp:RegularExpressionValidator ID="revFromDate" ControlToValidate="txtFromDate" Display="Dynamic" Runat="server"></asp:RegularExpressionValidator><br>
									<asp:CustomValidator ID="csvFromDate" Runat="server" ControlToValidate="txtFromDate" Display="Dynamic"
										ClientValidationFunction="ChkFromDate"></asp:CustomValidator>
								</TD>
								<TD class="midcolora" colSpan="1" width="25%">Transaction  To Date</TD>
								<TD class="midcolora" colSpan="2" width="25%" style="WIDTH: 187px"><asp:textbox id="txtToDate" runat="server" size="12" maxlength="10"></asp:textbox>
									<asp:hyperlink id="hlkToDate" runat="server" CssClass="HotSpot">
										<ASP:IMAGE id="imgToDate" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
									</asp:hyperlink><br>
									<%--<asp:requiredfieldvalidator id="rfvToDate" runat="server" ControlToValidate="txtToDate" ErrorMessage="txtToDate can't be blank." Display="Dynamic"></asp:requiredfieldvalidator><br>--%>
									<asp:CustomValidator ID="csvToDate" Runat="server" ControlToValidate="txtToDate" ClientValidationFunction="ChkToDate"
										Display="Dynamic"></asp:CustomValidator><br>
									<asp:RegularExpressionValidator ID="revToDate" Runat="server" ControlToValidate="txtToDate" Display="Dynamic"></asp:RegularExpressionValidator><br>
									<asp:CompareValidator ID="cmpToDate" Runat="server" ControlToValidate="txtToDate" ControlToCompare="txtFromDate"
										Type="Date" Operator="GreaterThanEqual" Display="Dynamic"></asp:CompareValidator>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" colSpan="1">Payment From Date</TD>
								<TD class="midcolora" colSpan="1" width="250"><asp:textbox id="txtFromPayDate" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkFromPayDate" runat="server" CssClass="HotSpot">
										<ASP:IMAGE id="imgFromPayDate" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
									</asp:hyperlink><br>
									<asp:RegularExpressionValidator ID="revFromPayDate" ControlToValidate="txtFromPayDate" Display="Dynamic" Runat="server"></asp:RegularExpressionValidator><br>
									<asp:CustomValidator ID="csvFromPayDate" Runat="server" ControlToValidate="txtFromPayDate" Display="Dynamic"
										ClientValidationFunction="ChkFromDate"></asp:CustomValidator>
								</TD>
								<TD class="midcolora" colSpan="1">Payment To Date</TD>
								<TD class="midcolora" colSpan="2" width="212" style="WIDTH: 187px"><asp:textbox id="txtToPayDate" runat="server" size="12" maxlength="10"></asp:textbox>
									<asp:hyperlink id="hlkToPayDate" runat="server" CssClass="HotSpot">
										<ASP:IMAGE id="imgToPayDate" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
									</asp:hyperlink><br>
									<%--<asp:requiredfieldvalidator id="rfvToDate" runat="server" ControlToValidate="txtToDate" ErrorMessage="txtToDate can't be blank." Display="Dynamic"></asp:requiredfieldvalidator><br>--%>
									<asp:CustomValidator ID="csvToPayDate" Runat="server" ControlToValidate="txtToPayDate" ClientValidationFunction="ChkToDate"
										Display="Dynamic"></asp:CustomValidator><br>
									<asp:RegularExpressionValidator ID="revTopayDate" Runat="server" ControlToValidate="txtToPayDate" Display="Dynamic"></asp:RegularExpressionValidator><br>
									<asp:CompareValidator ID="cmpTopayDate" Runat="server" ControlToValidate="txtToPayDate" ControlToCompare="txtFromPayDate"
										Type="Date" Operator="GreaterThanEqual" Display="Dynamic"></asp:CompareValidator>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" >Policy Number</TD>
								<TD class="midcolora" colSpan="1" ><asp:textbox id="txtPolicyNumber" runat="server" size="12" maxlength="10"></asp:textbox></TD>
								<TD class="midcolora" >Overpayment</TD>
								<TD class="midcolora" colspan="2"><asp:dropdownlist id="cmbIsOverPay" runat="server">
									<asp:listitem value=""> </asp:listitem>
									<asp:listitem value="1">Yes</asp:listitem>
									<asp:listitem value="2">No</asp:listitem></asp:dropdownlist></TD>
							</tr>
							<tr>
								<TD class="midcolorr" colspan = "5">
								<cmsb:cmsbutton class="clsButton" id="btnDisplay" runat="server" Text="Search"></cmsb:cmsbutton>
								<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text=" Reset "></cmsb:cmsbutton>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capACCOUNT_ID" runat="server">Bank Account</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" colSpan="5"><asp:dropdownlist id="cmbACCOUNT_ID" onfocus="SelectComboIndex('cmbACCOUNT_ID')" runat="server">
										<ASP:LISTITEM Value="0">0</ASP:LISTITEM>
									</asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvACCOUNT_ID" runat="server" ControlToValidate="cmbACCOUNT_ID" ErrorMessage="ACCOUNT_ID can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr class="headereffectWebGrid">
					<td colspan="3">
						&nbsp;<asp:CheckBox ID="chkSelectAll" Runat="server" onclick="chkSelDeSelAll(this);"></asp:CheckBox>&nbsp;Select 
						All
					</td>
				</tr>
				<tr>
					<td colSpan="3">
						<asp:datagrid id="grdReconcileItems" runat="server" DataKeyField="IDEN_NO" Width="100%" AlternatingItemStyle-CssClass="alternatedatarow"
							ItemStyle-CssClass="datarow" HeaderStyle-CssClass="headereffectCenter" AutoGenerateColumns="False"
							AllowPaging="True" PageSize="15" PagerStyle-Mode="NumericPages" PagerStyle-HorizontalAlign="Center"
							PagerStyle-CssClass="datarow" OnPageIndexChanged="grdReconcileItems_Paging" AllowSorting="true" OnSortCommand="Sort_Grid">
							<AlternatingItemStyle CssClass="alternatedatarow"></AlternatingItemStyle>
							<ItemStyle CssClass="datarow"></ItemStyle>
							<HeaderStyle CssClass="headereffectCenter"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Select">
									<HeaderStyle Width="4%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<INPUT id="hidPOLICY_ID" type="hidden"  value='<%# DataBinder.Eval(Container.DataItem,"POLICY_ID")%>' name="hidPOLICY_ID" runat="server">
										<INPUT id="hidCUSTOMER_ID" type="hidden" value='<%# DataBinder.Eval(Container.DataItem,"CUSTOMER_ID")%>' name="hidCUSTOMER_ID" runat="server">
										<INPUT id="hidPOLICY_VERSION_ID" type="hidden" value='<%# DataBinder.Eval(Container.DataItem,"POLICY_VERSION_ID")%>' name="hidPOLICY_VERSION_ID" runat="server">
										<INPUT id="hidSuspensePayment" type="hidden"  value='<%# DataBinder.Eval(Container.DataItem,"IS_SUSPENSE_PAYMENT")%>' name="hidSuspensePayment" runat="server">
										<INPUT id="hidIdenRowId" type="hidden"  value='<%# DataBinder.Eval(Container.DataItem,"IDEN_ROW_ID")%>' name="hidIdenRowId" runat="server">
										<ASP:CHECKBOX id="chkSelect" onclick="CheckBoxClicked(this);" Runat="server"></ASP:CHECKBOX>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Customer Name" SortExpression="CUSTOMER_NAME" HeaderStyle-ForeColor="white">
									<HeaderStyle Width="22%"></HeaderStyle>
									<ItemTemplate>
										<ASP:LABEL id="lblCUSTOMER_NAME" Runat="server" text='<%# DataBinder.Eval(Container.DataItem,"CUSTOMER_NAME")%>'>
										</ASP:LABEL>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Policy Number" SortExpression="POLICY_NUMBER" HeaderStyle-ForeColor="white">
									<HeaderStyle Width="4%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<ASP:LABEL id="lblPOLICY_NUMBER" Runat="server" text='<%# DataBinder.Eval(Container.DataItem,"POLICY_NUMBER")%>'>
										</ASP:LABEL>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Policy Version" SortExpression="POLICY_VERSION_ID" HeaderStyle-ForeColor="white">
									<HeaderStyle Width="2%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<ASP:LABEL id="lblPOLICY_DISP_VERSION" Runat="server" text='<%# DataBinder.Eval(Container.DataItem,"POLICY_VERSION_ID")%>'>
										</ASP:LABEL>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Date of Last Payment" SortExpression="PAYMENT_DATE" HeaderStyle-ForeColor="white">
									<HeaderStyle Width="7%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<ASP:LABEL id="lblTrans_Date" Runat="server" text='<%# DataBinder.Eval(Container.DataItem,"PAYMENT_DATE")%>'>
										</ASP:LABEL>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Effective Date of Premium Transaction" SortExpression="SOURCE_TRAN_DATE" HeaderStyle-ForeColor="white">
									<HeaderStyle Width="6%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<ASP:LABEL id="lblEffDate" Runat="server" text='<%# DataBinder.Eval(Container.DataItem,"SOURCE_TRAN_DATE")%>'>
										</ASP:LABEL>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Refund Amount" SortExpression="REFUND_AMOUNT" HeaderStyle-ForeColor="white">
									<HeaderStyle HorizontalAlign="Right" Width="6%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<ASP:LABEL id="lblOP_AMOUNT" Runat="server" text='<%# String.Format("{0:c}",DataBinder.Eval(Container.DataItem,"REFUND_AMOUNT"))%>' >
										</ASP:LABEL>
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="Cancellation Type" SortExpression="CANCELLATION_TYPE" HeaderStyle-ForeColor="white">
									<HeaderStyle Width="6%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<ASP:LABEL id="lblCANCELLATION_TYPE" Runat="server" text='<%# DataBinder.Eval(Container.DataItem,"CANCELLATION_TYPE")%>'>
										</ASP:LABEL>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Overpayment" SortExpression="IS_OVERPAYMENT" HeaderStyle-ForeColor="white">
									<HeaderStyle Width="6%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<ASP:LABEL id="lblOverPayment" Runat="server" text='<%# DataBinder.Eval(Container.DataItem,"IS_OVERPAYMENT")%>'>
										</ASP:LABEL>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td class="midcolora" colSpan="3">&nbsp;</td>
				</tr>
				<tr>
					<td class="midcolora" colSpan="2">
						<cmsb:cmsbutton class="clsButton" id="btnBack" runat="server" Text="Back"></cmsb:cmsbutton>
						<cmsb:cmsbutton class="clsButton" id="btnCreateChecks" runat="server" Text="Create Checks" enabled="false"></cmsb:cmsbutton>
					</td>
				</tr>
				<tr>
					<td><webcontrol:footer id="pageFooter" runat="server"></webcontrol:footer></td>
				</tr>
				<INPUT id="hidBnkACMappingXML" type="hidden" value="0" name="hidBnkACMappingXML" runat="server">
			</table>
		</FORM>
	</div>	
	
  </body>
</html>
