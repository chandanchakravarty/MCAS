<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Page validateRequest=false language="c#" Codebehind="AG_CheckListing.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.AG_CheckListing" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>BRICS-<%=Request.QueryString["Mode"]==null?"":"Add "%>  Items to Reconcile</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=base.GetColorScheme()%>.css" type=text/css rel=stylesheet >
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
		
		function SetBankAccountDD()
		{
			var tempXML = document.getElementById('hidBnkACMappingXML').value;								
			var checkType = document.getElementById('cmbCheckType').options[document.getElementById('cmbCheckType').selectedIndex].value;
			var objXmlHandler = new XMLHandler();			
			
			if(tempXML != null && tempXML !="")
			{
					var tree = (objXmlHandler.quickParseXML(tempXML).getElementsByTagName('Table')[0]);
					var i=0;
					for(i=0;i<tree.childNodes.length;i++)
					{	
						var nodeName = tree.childNodes[i].nodeName;						
						var nodeValue = tree.childNodes[i].firstChild.text;							
						
						switch(checkType)
						{
							case "1": // over
								//cmbACCOUNT_ID									
								if(nodeName=='BNK_OVER_PAYMENT')
									SelectDropdownOptionByVal(document.getElementById('cmbACCOUNT_ID'),nodeValue);
								break;
							case "2": // suspense
								if(nodeName=='BNK_SUSPENSE_AMOUNT')	
									SelectDropdownOptionByVal(document.getElementById('cmbACCOUNT_ID'),nodeValue);
									break;
							case "3": // return						
								if(nodeName=='BNK_RETURN_PRM_PAYMENT')	
									SelectDropdownOptionByVal(document.getElementById('cmbACCOUNT_ID'),nodeValue);
								break;			
						}					
					}		
				}
			
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
		</script>
</HEAD>
	<body oncontextmenu="return false;" leftmargin="0" topmargin="0" MS_POSITIONING="GridLayout" onload="ChangeColor();DisableValidators();ApplyColor();Uncheck();">
		
		<!-- To add bottom menu -->
		<!-- To add bottom menu ends here -->
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
									<td class="midcolorc" align="right" colSpan="5" style="WIDTH: 678px"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
								<tr>
									<td class="midcolora" width="18%"><asp:label id="lblCheckType" runat="server">Select Check Type</asp:label></td>
									<td class="midcolora" colSpan="5"><asp:dropdownlist id="cmbCheckType" runat="server" AutoPostBack="True">
											<ASP:LISTITEM Value="1">Checks for Over Payment</ASP:LISTITEM>
											<ASP:LISTITEM Value="2">Checks for Suspense Amount</ASP:LISTITEM>
											<ASP:LISTITEM Value="3">Checks for Cancellation and Change Premium Payment</ASP:LISTITEM>
										</asp:dropdownlist></td>
								</tr>
								<tr>
									<TD class="midcolora" colSpan="1">From Date</TD>
									<TD class="midcolora" colSpan="1" width="250"><asp:textbox id="txtFromDate" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkFromDate" runat="server" CssClass="HotSpot">
											<ASP:IMAGE id="imgFromDate" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
										</asp:hyperlink><%--<br>
										<asp:requiredfieldvalidator id="rfvFromDate" runat="server" ControlToValidate="txtFromDate" ErrorMessage="txtFromDate can't be blank."
											Display="Dynamic"></asp:requiredfieldvalidator>--%><br>
										<asp:RegularExpressionValidator ID="revFromDate" ControlToValidate="txtFromDate" Display="Dynamic" Runat="server"></asp:RegularExpressionValidator><br>
										<asp:CustomValidator ID="csvFromDate" Runat="server" ControlToValidate="txtFromDate" Display="Dynamic"
											ClientValidationFunction="ChkFromDate"></asp:CustomValidator>
									</TD>
									<TD class="midcolora" colSpan="1">To Date</TD>
									<TD class="midcolora" colSpan="2" width="212" style="WIDTH: 187px"><asp:textbox id="txtToDate" runat="server" size="12" maxlength="10"></asp:textbox>
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
									<TD class="midcolora" >Policy Number</TD>
									<TD class="midcolora" colSpan="4" ><asp:textbox id="txtPolicyNumber" runat="server" size="12" maxlength="10"></asp:textbox><TD></TD>
								</tr>
								<tr>
									<TD class="midcolorr" colSpan="5"><cmsb:cmsbutton class="clsButton" id="btnDisplay" runat="server" Text="Search"></cmsb:cmsbutton></TD>
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
							<asp:datagrid id="grdReconcileItems" runat="server" DataKeyField="IDEN_ROW_ID" Width="100%" AlternatingItemStyle-CssClass="alternatedatarow"
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
									<asp:TemplateColumn HeaderText="Policy Version" SortExpression="POLICY_DISP_VERSION" HeaderStyle-ForeColor="white">
										<HeaderStyle Width="2%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<ASP:LABEL id="lblPOLICY_DISP_VERSION" Runat="server" text='<%# DataBinder.Eval(Container.DataItem,"POLICY_DISP_VERSION")%>'>
											</ASP:LABEL>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Date" SortExpression="SOURCE_TRAN_DATE" HeaderStyle-ForeColor="white">
										<HeaderStyle Width="7%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<ASP:LABEL id="lblTrans_Date" Runat="server" text='<%# DataBinder.Eval(Container.DataItem,"SOURCE_TRAN_DATE")%>'>
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
									<asp:TemplateColumn SortExpression="OP_AMOUNT" HeaderStyle-ForeColor="white">
										<HeaderStyle HorizontalAlign="Right" Width="6%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<ASP:LABEL id="lblOP_AMOUNT" Runat="server" text='<%# DataBinder.Eval(Container.DataItem,"OP_AMOUNT")%>'>
											</ASP:LABEL>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Last Payment Received" SortExpression="PAYMENT_DATE" HeaderStyle-ForeColor="white">
										<HeaderStyle Width="6%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<ASP:LABEL id="lblPAYMENT_DATE" Runat="server" text='<%# DataBinder.Eval(Container.DataItem,"PAYMENT_DATE")%>'>
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
		</div></FORM>
	</body>
</HTML>
