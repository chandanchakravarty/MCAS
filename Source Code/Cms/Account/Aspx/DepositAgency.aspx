<%@ Page language="c#" Codebehind="DepositAgency.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.DepositAgency" validaterequest="false" %>
<%@ Register TagPrefix="cc1" Namespace="Cms.WebControls" Assembly="AjaxLookupTextbox" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DepositAgency</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="../../cmsweb/scripts/xmldom.js"></script>
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<script language="javascript">
		
		function AddData()
		{
			document.getElementById("txtAGENCY_NAME").value = "";
			document.getElementById("cmbPOLICY_MONTH").selectedIndex = -1;
			//Commented on Nov 20 2006:
			//document.getElementById("txtRECEIPT_AMOUNT").value = "";
			
			if (document.getElementById("btnDelete") != null)
			{
				document.getElementById("btnDelete").style.display = "none";
			}
			if (document.getElementById("btnReconcile") != null)
			{
				document.getElementById("btnReconcile").style.display = "none";
			}
		}
		
		function populateXML()
		{
			if(document.getElementById('hidFormSaved').value == '0')
			{
				if(document.getElementById('hidOldData').value != "")
				{
					populateFormData(document.getElementById('hidOldData').value, ACT_CURRENT_DEPOSITS);
					
					if (document.getElementById("btnDelete") != null)
					{
						document.getElementById("btnDelete").style.display = "inline";
					}
					if (document.getElementById("btnReconcile") != null)
					{
						document.getElementById("btnReconcile").style.display = "inline";
					}
				}
				else
				{
					AddData();
				}
			}
			
			return false;
		}
		
		//Formats the amount and convert 111 into 1.11
		function FormatAmount(txtReceiptAmount)
		{			
			if (txtReceiptAmount.value != "")
			{
				amt = txtReceiptAmount.value;
				amt = ReplaceAll(amt,".","");
				
				if (amt.length == 1)
					amt = amt + "0";
				
				if ( ! isNaN(amt))
				{
					
					DollarPart = amt.substring(0, amt.length - 2);
					CentPart = amt.substring(amt.length - 2);					
					txtReceiptAmount.value = InsertDecimal(amt);
				}
			}		
		}
			
		//This function open the agency lookup window
		function OpenAgencyLookup()
		{
			var url='<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>';
			var idField,valueField,lookUpTagName,lookUpTitle;
	
			idField			=	'AGENCY_ID';
			valueField		=	'Name';
			lookUpTagName	=	'Agency';
			lookUpTitle		=	"Agency Names";
			
			OpenLookup( url,idField,valueField,"hidAGENCY_ID","txtAGENCY_NAME",lookUpTagName,lookUpTitle,'');
		}
		
	
		function Reset()
		{
			//DisableValidators();
			//document.ACT_CURRENT_DEPOSITS.reset();
			document.getElementById('txtAGENCY_NAME').value = "";
			document.getElementById('txtYEAR').value ="";	
			//Set Month
			var d = new Date();	
			var month=new Array(12);
			//Set Value according to dropDown Values
			month[0]="1";
			month[1]="2";
			month[2]="3";
			month[3]="4";
			month[4]="5";
			month[5]="6";
			month[6]="7";
			month[7]="8";
			month[8]="9";
			month[9]="10";
			month[10]="11";
			month[11]="12";
			document.getElementById("cmbPOLICY_MONTH").value = month[d.getMonth()];
			return false;
		}
		function Uncheck()
		{
			document.getElementById('chSelectAll').checked = false;
		}
		
		
		
		</script>
		<script language="javascript">
		
		function OpenReconcileWindow(LineItemId, Agency_ID, Month,Year, CD_Line_Item_ID, Receipt_Amount)
		{
			if (LineItemId == null)
				return;
					
			var url ="DepositAgencyDistribution.aspx?AGENCY_ID="+Agency_ID+"&MONTH="+ Month +"&YEAR="+ Year +"&CD_LINE_ITEM_ID="+ CD_Line_Item_ID +"&RECEIPT_AMOUNT="+ Receipt_Amount +"";			
			
			mywin = window.open(url,'DistributeAgencyReceipts', "height=500,width=900,status= no,resizable= no,scrollbars=yes,toolbar=no,location=no,menubar=no");		
			mywin.moveTo(0,0);
			
			/*mywin = window.open('DepositAgencyDistribution.aspx?AGENCY_ID=' + " + Agency_ID
						+ "+ "&MONTH=" + Month 
						+ "+ "&YEAR=" + Year 
						+ "+ "&CD_LINE_ITEM_ID=" + CD_Line_Item_ID 
						+ "+ "&RECEIPT_AMOUNT=" + Receipt_Amount 
						+ "+ "&" ,"DistributeAgencyReceipts","height=500,width=1000,status= no,resizable= no,scrollbars=no,toolbar=no,location=no,menubar=no");*/
						
			
			
			
			
		}
		//Fill Amount on Select all : kasana
			function FillPayAmounts(chkAll)
			{
			
				var flag = true;
				var ctr = 1;
				if(chkAll.checked)
				{
					while(flag == true)
					{
						rowNo = ctr + 1;
						prefix = "dgAgencyDeposit__ctl" + rowNo;
						balCtrl = document.getElementById(prefix + "_lbl_BalAmount");
						ctrl = document.getElementById(prefix + "_txtReceipt_Amt");
						 			 
						if(balCtrl!=null && ctrl!=null)
						{
						
							var balAmt = new String();
							balAmt =  balCtrl.innerHTML;
							if(ctrl.value=="")
								ctrl.value = balAmt;
						}
						else
						{
							flag = false;
						}										 
						ctr++;
					 
					}
				}
				else
				{
					while(flag == true)
					{
						rowNo = ctr + 1;
						prefix = "dgAgencyDeposit__ctl" + rowNo;
						balCtrl = document.getElementById(prefix + "_lbl_BalAmount");
						ctrl = document.getElementById(prefix + "_txtReceipt_Amt");
						 			 
						if(balCtrl!=null && ctrl!=null)
						{
						
							var balAmt = new String();
							balAmt =  balCtrl.innerHTML;
							if(ctrl.value!="")
								ctrl.value = "";
						}
						else
						{
							flag = false;
						}										 
						ctr++;
					 
					}
					
				}
			}
		
		//Select deselect Checkboxs : PK
			function confun(elm)
				{					
						if (elm.checked)
						{
								aspCheckBoxID = 'chkAgenDepot';	//for seond checkbox section
								re = new RegExp(':' + aspCheckBoxID + '$')  //generated controlname starts with a colon
								for(i = 0; i < document.ACT_CURRENT_DEPOSITS.elements.length; i++) {
									elm = document.ACT_CURRENT_DEPOSITS.elements[i]
									if (elm.type == 'checkbox') {
										if (re.test(elm.name)) 
										elm.checked = true;
										
									}
								}
						}
						else
						{
							
								aspCheckBoxID = 'chkAgenDepot';	//for seond checkbox section
								re = new RegExp(':' + aspCheckBoxID + '$')  //generated controlname starts with a colon
								for(i = 0; i < document.ACT_CURRENT_DEPOSITS.elements.length; i++) {
									elm = document.ACT_CURRENT_DEPOSITS.elements[i]
									if (elm.type == 'checkbox') {
										if (re.test(elm.name)) 
										elm.checked = false;
									}
								}
						}
				}
				//Count No of checkboxes checked:
			var chkCount
			function CountCheckBoxes() 
			{
				chkCount = 0
				var checkBoxID = 'chkAgenDepot';	
				re = new RegExp(':' + checkBoxID + '$')  //generated controlname starts with a colon
				
				for(i = 0; i < document.ACT_CURRENT_DEPOSITS.elements.length; i++) 
				{
					elm = document.ACT_CURRENT_DEPOSITS.elements[i]

					if (elm.type == 'checkbox') 
					{
					
						if (re.test(elm.name)) 
						if (elm.checked)
						{
						chkCount = chkCount + 1;
						}
					}
				}
			}
			
			function Select()
			{
			  var Result = true;
			  CountCheckBoxes(); 
			  if (chkCount == 0)
			  {
					alert("Please Select at least One Record. !")
					Result = false;
					return false;
			  }
			  return (Page_IsValid && Result)
			}
			 
			 
		function SetParentElement()
		{
		
			var Deopsit_Id = document.getElementById('hidDEPOSIT_ID').value;
			//alert(window.parent.self.document.forms[0].hidSELECTED_DEP_ID.value);
			window.parent.self.document.forms[0].hidSELECTED_DEP_ID.value = Deopsit_Id ;
			//window.parent.self.document.forms[0].hidlocQueryStr.value=Deopsit_Id  + "&DEPOSIT_TYPE";
			//alert(window.parent.self.document.forms[0].hidSELECTED_DEP_ID.value);

		}
		
		function fillBalAmtOnChkChng(ctr,Amt)
		{
			var selChkID  = "dgAgencyDeposit__ctl" + ctr + "_chkAgenDepot";
			var objChk    = document.getElementById(selChkID);
			var txtAmt = "dgAgencyDeposit__ctl" + ctr + "_txtReceipt_Amt";
			if(objChk.checked)
				{
					if(document.getElementById(txtAmt).value=="")
					{
						document.getElementById(txtAmt).value = Amt;
						FormatAmount(document.getElementById(txtAmt));
					}
				}
			else
				{
				document.getElementById(txtAmt).value = "";}
		}
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="ApplyColor();ChangeColor();SetParentElement();Uncheck();">
		<form id="ACT_CURRENT_DEPOSITS" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td>
						<table width="100%" align="center" border="0">
							<tr>
								<td class="pageheader" colSpan="2"><asp:Label ID="capMandatory" runat="server"></asp:Label>
								</td>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="2"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<td class="midcolora" colspan="2"><b><asp:Label ID="lblDepositNum" Runat="server">Deposit Number :</asp:Label></b>
									<b>
										<asp:Label ID="lblDEPOSIT_NUM" Runat="server"></asp:Label></b></td>
							</tr>
							<tr>
								<td class="midcolora" width="25%"><asp:label id="capAGENCY_NAME" runat="server">Agency Name</asp:label><span class="mandatory"></span></td>
								<td class="midcolora" width="75%"><asp:textbox id="txtTest" style="DISPLAY: none" runat="server" ReadOnly="True" size="60"></asp:textbox><IMG id="imgAGENCY_NAME" style="DISPLAY: none" onclick="OpenAgencyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
										runat="server">
									<cc1:ajaxlookup id="txtAGENCY_NAME" runat="server" size="40" LookupNodeName="Agency" ScriptFile="../../cmsweb/scripts/lookup.js"
										DataValueField="AGENCY_ID" DataTextField="NAME" CallBackFunction="AccountBase.GetSearch" DataValueFieldID="hidRECEIPT_FROM_ID"></cc1:ajaxlookup><BR>
									<asp:requiredfieldvalidator id="rfvAGENCY_NAME" runat="server" Enabled="False" ControlToValidate="txtAGENCY_NAME"
										ErrorMessage="Please select agency." Display="Dynamic"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<td class="midcolora" width="25%"><asp:label id="capMONTH" runat="server">Month/Year</asp:label><span class="mandatory">*</span>
								</td>
								<td class="midcolora" width="75%"><asp:dropdownlist id="cmbPOLICY_MONTH" runat="server">
										<ASP:LISTITEM Value="1">January</ASP:LISTITEM>
										<ASP:LISTITEM Value="2">February</ASP:LISTITEM>
										<ASP:LISTITEM Value="3">March</ASP:LISTITEM>
										<ASP:LISTITEM Value="4">April</ASP:LISTITEM>
										<ASP:LISTITEM Value="5">May</ASP:LISTITEM>
										<ASP:LISTITEM Value="6">June</ASP:LISTITEM>
										<ASP:LISTITEM Value="7">July</ASP:LISTITEM>
										<ASP:LISTITEM Value="8">August</ASP:LISTITEM>
										<ASP:LISTITEM Value="9">September</ASP:LISTITEM>
										<ASP:LISTITEM Value="10">October</ASP:LISTITEM>
										<ASP:LISTITEM Value="11">November</ASP:LISTITEM>
										<ASP:LISTITEM Value="12">December</ASP:LISTITEM>
									</asp:dropdownlist><asp:textbox id="txtYEAR" size="4" MaxLength="4" Runat="server"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvYEAR" runat="server" ControlToValidate="txtYEAR" ErrorMessage="Please enter year."
										Display="Dynamic"></asp:requiredfieldvalidator><asp:rangevalidator id="rngYEAR" ControlToValidate="txtYEAR" Display="Dynamic" Runat="server" MinimumValue="2000"
										Type="Integer"></asp:rangevalidator><asp:requiredfieldvalidator id="rfvMONTH" runat="server" ControlToValidate="cmbPOLICY_MONTH" ErrorMessage="Please select month."
										Display="Dynamic"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<td class="midcolora" width="25%"><asp:label id="capRECEIPT_AMOUNT" runat="server" Visible="False">Receipt Amount</asp:label><span class="mandatory"></span></td>
								<td class="midcolora" width="75%"><asp:textbox id="txtRECEIPT_AMOUNT" runat="server" Visible="False" size="10" maxlength="10"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revRECEIPT_AMOUNT" ControlToValidate="txtRECEIPT_AMOUNT" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator></td>
							</tr>
							<tr>
								<td class="midcolora"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnReconcile" runat="server" Visible="False" Text="Reconciliation"></cmsb:cmsbutton></td>
								<td class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnFind" runat="server" Text="Find"></cmsb:cmsbutton></td>
							</tr>
						</table>
						<TABLE id="Table1" borderColor="#7da2c8" width="100%">
							<TR class="headereffectWebGrid">
								<TD><asp:checkbox id="chSelectAll" onclick="confun(this);FillPayAmounts(this);" Runat="server"></asp:checkbox> 
									
								</TD>
							</TR>
						</TABLE>
						<asp:datagrid id="dgAgencyDeposit" runat="server" DataKeyField="CD_LINE_ITEM_ID" AutoGenerateColumns="False"
							Width="100%">
							<ALTERNATINGITEMSTYLE CssClass="midcolora"></ALTERNATINGITEMSTYLE>
							<ITEMSTYLE CssClass="midcolora"></ITEMSTYLE>
							<HEADERSTYLE CssClass="headereffectWebGrid"></HEADERSTYLE>
							<COLUMNS>
								<ASP:TEMPLATECOLUMN HeaderText="&nbsp;Select&nbsp;">
									<ITEMTEMPLATE>
										<ASP:CHECKBOX id="chkAgenDepot" Runat="server" name="chkAgenDepot"></ASP:CHECKBOX>
									</ITEMTEMPLATE>
								</ASP:TEMPLATECOLUMN>
								<ASP:BOUNDCOLUMN HeaderText="Agency Id" DataField="AGENCY_ID" Visible="False"></ASP:BOUNDCOLUMN>
								<ASP:BOUNDCOLUMN HeaderText="Agency Name" DataField="AGENCY_DISPLAY_NAME"></ASP:BOUNDCOLUMN>
								<ASP:BOUNDCOLUMN HeaderText="Agency Code" DataField="AGENCY_CODE"></ASP:BOUNDCOLUMN>
								<ASP:BOUNDCOLUMN HeaderText="Month" DataField="MONTH" Visible="False"></ASP:BOUNDCOLUMN>
								<ASP:BOUNDCOLUMN HeaderText="Year" DataField="YEAR" Visible="False"></ASP:BOUNDCOLUMN>
								<ASP:BOUNDCOLUMN HeaderText="Stmt Month/Year" DataField="MONTHYEAR"></ASP:BOUNDCOLUMN>								
								<ASP:BOUNDCOLUMN HeaderText="Stmt Amount" DataField="STMT_AMOUNT" ItemStyle-HorizontalAlign="right"></ASP:BOUNDCOLUMN>
								<ASP:TEMPLATECOLUMN HeaderText="Bal Amount" ItemStyle-HorizontalAlign="right">
								<ITEMTEMPLATE>
									<ASP:LABEL runat ="server" id="lbl_BalAmount" Text = '<%# FormatMoney(DataBinder.Eval(Container,"DataItem.BALANCE_AMOUNT"))%>'></ASP:LABEL>
								</ITEMTEMPLATE>
								</ASP:TEMPLATECOLUMN>
								<ASP:TEMPLATECOLUMN HeaderText="Receipt Amt">
									<ITEMTEMPLATE>
										<ASP:TEXTBOX id="txtReceipt_Amt" Runat="server" CssClass="InputCurrency" MaxLength="11" Text='<%# FormatMoney(DataBinder.Eval(Container, "DataItem.RECEIPT_AMOUNT")) %>'>
										</ASP:TEXTBOX>
										<br>
										<ASP:RegularExpressionValidator ID="revReceipt_Amt" Runat="server" Display="Dynamic" ControlToValidate="txtReceipt_Amt"></ASP:RegularExpressionValidator>
									</ITEMTEMPLATE>
								</ASP:TEMPLATECOLUMN>
								<ASP:TEMPLATECOLUMN HeaderText="Reconcile Policies">
									<ITEMTEMPLATE>
										<ASP:HYPERLINK id="hlnkReconcile" runat="server">Reconcile</ASP:HYPERLINK>
									</ITEMTEMPLATE>
								</ASP:TEMPLATECOLUMN>
								<asp:TemplateColumn HeaderText="Reconcile Status" Visible="False">
									<ItemTemplate>
										<ASP:Label id="lblStatus" Runat="server" Visible="False" CssClass="errmsg" Text='<%# DataBinder.Eval(Container, "DataItem.IS_RECONCILED") %>'>
										</ASP:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</COLUMNS>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td>
						<table width="100%">
							<tr>
								<td class="midcolora"><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" CausesValidation="False"></cmsb:cmsbutton></td>
								<td class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" CausesValidation="False"></cmsb:cmsbutton></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="hidAGENCY_ID" type="hidden" name="hidAGENCY_ID" runat="server"> <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<input id="hidOldData" type="hidden" name="hidOldData" runat="server"> <input id="hidDEPOSIT_ID" type="hidden" name="hidDEPOSIT_ID" runat="server">
			<input id="hidCD_LINE_ITEM_ID" type="hidden" name="hidCD_LINE_ITEM_ID" runat="server">
			<input id="hidMonth" type="hidden" name="hidMonth" runat="server"> <input id="hidYear" type="hidden" name="hidYear" runat="server">
		</form>
	</body>
</HTML>
