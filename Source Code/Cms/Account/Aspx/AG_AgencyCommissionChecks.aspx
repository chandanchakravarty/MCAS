<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AG_AgencyCommissionChecks.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.AG_AgencyCommissionChecks" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AgencyCommissionChecks</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Page-Enter" content="revealTrans(duration=0.0)">
		<meta http-equiv="Page-Exit" content="revealTrans(duration=0.0)">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		
		/*function OpenReconcileWindow(Agency_ID, Month,Year, Check_ID, Receipt_Amount)
		{
			if (Check_ID == null)
				return;
					
			var url ="DepositAgencyDistribution.aspx?AGENCY_ID=" + Agency_ID + "&MONTH=" + Month + " &YEAR= " + Year +" &CD_LINE_ITEM_ID= " + Check_ID + " &RECEIPT_AMOUNT= " + Receipt_Amount +"";			
			
			mywin = window.open(url,'DistributeAgencyReceipts', "height=500,width=1000,status= no,resizable= no,scrollbars=yes,toolbar=no,location=no,menubar=no");		
			mywin.moveTo(0,0);
			
			
		}*/
		function OpenReconcileWindow(Check_ID)
		{
			if (Check_ID == null)
				return;
			var url ="AgencyCommissionDistribution.aspx?CHECK_ID=" + Check_ID +"";			
			
		//	mywin = window.open(url,'DistributeAgencyReceipts', "height=215,width=800,status= no,resizable= no,scrollbars=yes,toolbar=no,location=no,menubar=no");
		//    mywin = window.open(url,'DistributeAgencyReceipts', "height=215,width=800,status= no,resizable= no,scrollbars=yes,toolbar=no,location=no,menubar=no");		
		//	mywin.moveTo(0,0);
			ShowPopup(url,'DistributeAgencyReceipts',900,300);
			
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
		//Validates the amount
		function txtAMOUNT_Validate(objSource,objArgs)
		{
			value = document.getElementById("txtAMOUNT").value;
			value = ReplaceString(value, ",","");
			if (value != "")
			{
				if (isNaN(value))
				{
					objArgs.IsValid = false;
					return;
				}
				else
				{
					if (parseFloat(value) == 0)
					{
						objArgs.IsValid = false;
						return;
					}
				}
			}
			objArgs.IsValid = true;
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
			
			//Added on 17 June 2008
			EnableDisableSave(); 
		}
		//SELECT  CheckDistribution : PK
		function CheckDistribution()
		{
		    var flag = true;
			var ctr = 1;
			var total = 0;
			
			while (flag == true)
			{
				rowNo = ctr + 1;
				prefix = "dgAgencyCheck__ctl" + rowNo;
				ctrl = document.getElementById(prefix + "_lblStatus");
				chkCtrl = document.getElementById(prefix + "_chkAgenCheck");
							
				if (ctrl != null)
				{
				 strStatus = ctrl.innerHTML;
				 strChkStatus = chkCtrl.checked;
				 if(strStatus == 'Not Distributed' && strChkStatus)
					{
						alert('Can not create Check(s) as one of the Payment amount is Not Distributed.');
						return false ;
					}
				
				}
				else
				{
					flag = false;
				}
				ctr++;
			}
		}
		//SELECT DESELECT CHECKBOXES : PK
			function confun(elm)
				{					
						if (elm.checked)
						{
								aspCheckBoxID = 'chkAgenCheck';	//for seond checkbox section
								re = new RegExp(':' + aspCheckBoxID + '$')  //generated controlname starts with a colon
								for(i = 0; i < document.Agency_CommissionChecks.elements.length; i++) {
									elm = document.Agency_CommissionChecks.elements[i]
									if (elm.type == 'checkbox') {
										if (re.test(elm.name)) 
										elm.checked = true;
										
									}
								}
								//Added on 17 June 2008 (Create Check only when Checked)
									document.getElementById("btnCreateChecks").setAttribute("disabled",false);
									document.getElementById("btnDelete").setAttribute("disabled",false);
						}
						else
						{
							
								aspCheckBoxID = 'chkAgenCheck';	//for seond checkbox section
								re = new RegExp(':' + aspCheckBoxID + '$')  //generated controlname starts with a colon
								for(i = 0; i < document.Agency_CommissionChecks.elements.length; i++) {
									elm = document.Agency_CommissionChecks.elements[i]
									if (elm.type == 'checkbox') {
										if (re.test(elm.name)) 
										elm.checked = false;
									}
								}
								//Added on 17 June 2008 (Create Check only when Checked)
								document.getElementById("btnCreateChecks").setAttribute("disabled",true);
								document.getElementById("btnDelete").setAttribute("disabled",true);
						}
				}
			//COUNT NO OF CHECKBOXES CHECKED:
			var chkCount
			function CountCheckBoxes() 
			{
				chkCount = 0
				var checkBoxID = 'chkAgenCheck';	
				re = new RegExp(':' + checkBoxID + '$')  //generated controlname starts with a colon
				
				for(i = 0; i < document.Agency_CommissionChecks.elements.length; i++) 
				{
					elm = document.Agency_CommissionChecks.elements[i]

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
			  CountCheckBoxes(); 
			  //if (chkCount == 0)
			  //{
				//	alert("Please Select atleast One Record. !")
				//	return false;
			  //}		
			}
			
			function DisableAccountValidator()
			{
				document.getElementById("rfvACCOUNT_ID").setAttribute('disabled',true);
				document.getElementById("rfvACCOUNT_ID").setAttribute('isValid',true);
				//document.getElementById("rfvACCOUNT_ID").style.display ='';
				return true;
			}
			function fillBalAmtOnChkChng(ctr,Amt)
			{
				var selChkID  = "dgAgencyCheck__ctl" + ctr + "_chkAgenCheck";
				var objChk    =  document.getElementById(selChkID);
				var txtPayAmt = "dgAgencyCheck__ctl" + ctr + "_txtPayment_Amt";
			
				if(objChk.checked)
				{
					if(document.getElementById(txtPayAmt).value =="")
					{
						document.getElementById(txtPayAmt).value = Amt;
					}
					//Added on 17 June 2008 (Create Check only when Checked)
					document.getElementById("btnCreateChecks").setAttribute("disabled",false);
					document.getElementById("btnDelete").setAttribute("disabled",false);
				}
				else
				{
					document.getElementById(txtPayAmt).value = "";
					//Added on 17 June 2008 (Create Check only when Checked)
					document.getElementById("btnCreateChecks").setAttribute("disabled",true);
					document.getElementById("btnDelete").setAttribute("disabled",true);
				}
				
				//Added on 17 June 2008
				EnableDisableSave(); 
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
						prefix = "dgAgencyCheck__ctl" + rowNo;
						balCtrl = document.getElementById(prefix + "_lbl_BalAmount");
						ctrl = document.getElementById(prefix + "_txtPayment_Amt");
						 			 
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
					//Blank
					while(flag == true)
					{
						rowNo = ctr + 1;
						prefix = "dgAgencyCheck__ctl" + rowNo;
						balCtrl = document.getElementById(prefix + "_lbl_BalAmount");
						ctrl = document.getElementById(prefix + "_txtPayment_Amt");
						 			 
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
			
			
			//****************** CHECKS WITH -VE AMT CANNOT BE CREATED.
			function fnCheckForNegAmt()
			{
				var flag = true;
				var ctr = 1;
				var total = 0;
				
				while (flag == true)
				{
					rowNo = ctr + 1;
					prefix = "dgAgencyCheck__ctl" + rowNo;
					ctrl = document.getElementById(prefix + "_txtPayment_Amt");
				
					if (ctrl != null)
					{
					var amt = new String();
					amt = ctrl.value;
					
					if(amt.indexOf('-') == "0")
						{
							alert('Can not create Check(s) as one of the Payment amount is Negative.');
							return false ;
						}
					
					}
					else
					{
						flag = false;
					}
					ctr++;
				}
			}
			
		function Uncheck()
		{
			document.getElementById('chSelectAll').checked = false;
		}
		
		//**************************** ADDED ON 17 JUNE ******************************/
		function EnableDisableSave()
		{
		
			var flag = true;
			var ctr = 1;
								
					while (flag == true)
					{
						rowNo = ctr + 1;
						prefix = "dgAgencyCheck__ctl" + rowNo;
						ctrlAmount = document.getElementById(prefix + "_txtPayment_Amt");
									
						if (ctrlAmount != null)
						{
							if(ctrlAmount.value!="")
							{
								document.getElementById("btnSave").setAttribute("disabled",false);		
								break;
							}
							else
								document.getElementById("btnSave").setAttribute("disabled",true);		
						
						
						}
						else
						{
							flag = false;
						}
						ctr++;
					}
			}
			
		</script>
	</HEAD>
	<body oncontextmenu="return false;" class="bodyBackGround" topMargin="0" onload="ApplyColor();ChangeColor();top.topframe.main1.mousein = false;findMouseIn();showScroll();Uncheck();EnableDisableSave();"
		MS_POSITIONING="GridLayout">
		<div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 100%">
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<form id="Agency_CommissionChecks" method="post" runat="server">
			<table class="tableWidth" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
				<tr>
					<td class="headereffectcenter" colSpan="3">Agency Checks</td>
				</tr>
				<tr>
					<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
						mandatory
					</TD>
				</tr>
				<tr>
					<td class="midcolorc" align="right" colSpan="3"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<TR>
					<TD class="midcolora" width="18%">Bank Account</TD>
					<TD class="midcolora" width="32%" colSpan="2"><asp:dropdownlist id="cmbACCOUNT_ID" runat="server">
							<asp:ListItem Value="0">0</asp:ListItem>
						</asp:dropdownlist><br>
						<asp:requiredfieldvalidator id="rfvACCOUNT_ID" runat="server" ControlToValidate="cmbACCOUNT_ID" ErrorMessage="Account can't be blank."
							Display="Dynamic"></asp:requiredfieldvalidator></TD>
				</TR>
				<tr>
					<td class="midcolora" width="18%">Agency</td>
					<td class="midcolora" width="32%" colSpan="2"><asp:textbox id="txtAGENCY_NAME" runat="server" ReadOnly="True" size="30"></asp:textbox><IMG id="imgAGENCY_NAME" style="CURSOR: hand" onclick="OpenAgencyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
							runat="server"><br>
						<asp:requiredfieldvalidator id="rfvAGENCY_NAME" runat="server" Visible="False" ControlToValidate="txtAGENCY_NAME"
							ErrorMessage="Please select agency." Display="Dynamic"></asp:requiredfieldvalidator></td>
				</tr>
				<tr>
					<td class="midcolora" width="18%">Month/Year <span class="mandatory">*</span></td>
					<td class="midcolora" width="32%" colSpan="2"><asp:dropdownlist id="cmbMonth" Runat="server">
							<asp:ListItem Selected="True" Value="1">Jan</asp:ListItem>
							<asp:ListItem Value="2">Feb</asp:ListItem>
							<asp:ListItem Value="3">Mar</asp:ListItem>
							<asp:ListItem Value="4">Apr</asp:ListItem>
							<asp:ListItem Value="5">May</asp:ListItem>
							<asp:ListItem Value="6">Jun</asp:ListItem>
							<asp:ListItem Value="7">Jul</asp:ListItem>
							<asp:ListItem Value="8">Aug</asp:ListItem>
							<asp:ListItem Value="9">Sep</asp:ListItem>
							<asp:ListItem Value="10">Oct</asp:ListItem>
							<asp:ListItem Value="11">Nov</asp:ListItem>
							<asp:ListItem Value="12">Dec</asp:ListItem>
						</asp:dropdownlist><asp:textbox id="txtYear" runat="server" size="4" MaxLength="4"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revYear" runat="server" ControlToValidate="txtYear" ErrorMessage="Please enter valid Year only."
							Display="Dynamic"></asp:regularexpressionvalidator><asp:rangevalidator id="rnvYear" ControlToValidate="txtYear" ErrorMessage="Year should be greater than 1950 and less than 2099."
							Display="Dynamic" Runat="Server" Type="Integer" MaximumValue="2099" MinimumValue="1950"></asp:rangevalidator><asp:requiredfieldvalidator id="rfvYear" runat="server" ControlToValidate="txtYear" ErrorMessage="Please insert Year."
							Display="Dynamic"></asp:requiredfieldvalidator></td>
				</tr>
				<TR>
					<TD class="midcolora" style="HEIGHT: 20px" width="50%">Commission Type <span class="mandatory">
							*</span></TD>
					<TD class="midcolora" style="HEIGHT: 20px" width="50%"><asp:dropdownlist id="CmbCommType" Runat="server" Width="216px">
							<asp:ListItem Value="REG">Regular Commission</asp:ListItem>
							<asp:ListItem Value="ADC">Additional Commission</asp:ListItem>
							<asp:ListItem Value="CAC">Complete App Commission</asp:ListItem>
							<asp:ListItem Value="OP">Refund Agency Over Payment</asp:ListItem>
						</asp:dropdownlist></TD>
				</TR>
				<tr>
					<td class="midcolora" width="50%"></td>
					<td class="midcolora" width="50%"><asp:textbox id="txtAmount" style="TEXT-ALIGN: right" runat="server" CssClass="INPUTCURRENCY"
							Visible="False" size="30" MaxLength="12"></asp:textbox><br>
						<asp:customvalidator id="csvAMOUNT" runat="server" Visible="False" ControlToValidate="txtAmount" ErrorMessage="Please enter valid amount."
							Display="Dynamic" ClientValidationFunction="txtAMOUNT_Validate"></asp:customvalidator><asp:requiredfieldvalidator id="rfvAmount" runat="server" Visible="False" ControlToValidate="txtAmount" ErrorMessage="Please insert Amount."
							Display="Dynamic"></asp:requiredfieldvalidator></td>
				</tr>
				<tr>
					<td class="midcolora"></td>
					<td class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnSave1" runat="server" visible="false" Text="Save"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnFind" runat="server" Text="Find" CausesValidation="False"></cmsb:cmsbutton></td>
				</tr>
			</table>
			<TABLE id="Table1" borderColor="#7da2c8" width="88%" align="center">
				<TR class="headereffectWebGrid">
					<TD><asp:checkbox id="chSelectAll" Runat="server" Onclick="confun(this);FillPayAmounts(this);EnableDisableSave();"></asp:checkbox>Select 
						All
					</TD>
				</TR>
			</TABLE>
			<asp:datagrid id="dgAgencyCheck" runat="server" Width="88%" AutoGenerateColumns="False" HorizontalAlign="Center"
				DataKeyField="CHECK_ID" HeaderStyle-CssClass="headereffectCenter"  AlternatingItemStyle-CssClass="alternatedatarow">
				<ALTERNATINGITEMSTYLE CssClass="alternatedatarow"></ALTERNATINGITEMSTYLE>
				<ITEMSTYLE CssClass="datarow"></ITEMSTYLE>
				<HEADERSTYLE CssClass="headereffectCenter"></HEADERSTYLE>
				<COLUMNS>
					<ASP:TEMPLATECOLUMN HeaderText="&nbsp;Select&nbsp;">
						<ITEMTEMPLATE>
							<ASP:CHECKBOX id="chkAgenCheck" Runat="server" name="chkAgenCheck"></ASP:CHECKBOX>
						</ITEMTEMPLATE>
					</ASP:TEMPLATECOLUMN>
					<ASP:BOUNDCOLUMN HeaderText="Agency Id" DataField="AGENCY_ID" Visible="False"></ASP:BOUNDCOLUMN>
					<ASP:BOUNDCOLUMN HeaderText="Agency Name" DataField="ENTITY_NAME"></ASP:BOUNDCOLUMN>
					<ASP:BOUNDCOLUMN HeaderText="Agency Code" DataField="AGENCY_CODE"></ASP:BOUNDCOLUMN>
					<ASP:BOUNDCOLUMN HeaderText="Month" DataField="MONTH" Visible="False"></ASP:BOUNDCOLUMN>
					<ASP:BOUNDCOLUMN HeaderText="Year" DataField="YEAR" Visible="False"></ASP:BOUNDCOLUMN>
					<ASP:BOUNDCOLUMN HeaderText="Stmt Month/Year" DataField="MONTHYEAR"></ASP:BOUNDCOLUMN>
					<ASP:BOUNDCOLUMN HeaderText="Stmt Amount" DataField="STMT_AMOUNT" ItemStyle-HorizontalAlign="Right"></ASP:BOUNDCOLUMN>
					<ASP:TEMPLATECOLUMN HeaderText="Bal Amount" ItemStyle-HorizontalAlign="right">
						<ITEMTEMPLATE>
							<ASP:LABEL runat ="server" id="lbl_BalAmount" Text = '<%# DataBinder.Eval(Container,"DataItem.BALANCE_AMOUNT")%>' ></ASP:LABEL>
							<input type="hidden" id="hidPAYEE_ID" runat="server" value = '<%# DataBinder.Eval(Container,"DataItem.PAYEE_ENTITY_ID")%>'></input>
						</ITEMTEMPLATE>
					</ASP:TEMPLATECOLUMN>
					<ASP:TEMPLATECOLUMN HeaderText="Payment Amt">
						<ITEMTEMPLATE>
							<ASP:TEXTBOX id=txtPayment_Amt Runat="server" CssClass="INPUTCURRENCY" Text='<%# DataBinder.Eval(Container, "DataItem.PAYMENT_AMOUNT") %>'>
							</ASP:TEXTBOX><br>
							<ASP:RegularExpressionValidator ID="revPAYMENT_AMOUNT" Runat="server" ControlToValidate="txtPayment_Amt" Display="Dynamic"></ASP:RegularExpressionValidator>
						</ITEMTEMPLATE>
					</ASP:TEMPLATECOLUMN>
					<ASP:BOUNDCOLUMN HeaderText="Mode of Payment" DataField="PAYMENT_MODE" Visible="False"></ASP:BOUNDCOLUMN>
					<ASP:TEMPLATECOLUMN HeaderText="Mode of Payment">
						<ITEMTEMPLATE>
							<ASP:DROPDOWNLIST Runat="server" ID="cmbPAYMENT_MODE" Width="90"></ASP:DROPDOWNLIST>
						</ITEMTEMPLATE>
					</ASP:TEMPLATECOLUMN>
					<ASP:TEMPLATECOLUMN HeaderText="Distribute">
						<ITEMTEMPLATE>
							<ASP:HYPERLINK id="hlnkDistribute" runat="server">Distribute</ASP:HYPERLINK>
						</ITEMTEMPLATE>
					</ASP:TEMPLATECOLUMN>
					<asp:TemplateColumn HeaderText="Reconcile Status" Visible="False">
						<ItemTemplate>
							<ASP:Label id="lblStatus" Runat="server" CssClass="errmsg" Text='<%# DataBinder.Eval(Container, "DataItem.IS_RECONCILED") %>'>
							</ASP:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="ALLOW_EFT" Visible="False"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="special handling" Visible="False">
						<ItemTemplate>
							<ASP:Label id="lblREQ_SPECIAL_HANDLING" Runat="server" CssClass="errmsg" Text='<%# DataBinder.Eval(Container, "DataItem.REQ_SPECIAL_HANDLING") %>'>
							</ASP:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
				</COLUMNS>
			</asp:datagrid></TD>
			<tr>
				<td>
					<table width="88%" align="center">
						<tr>
							<td class="midcolora"><cmsb:cmsbutton class="clsButton" id="btnCreateChecks" runat="server" Text="Create Checks"></cmsb:cmsbutton>
							<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" CausesValidation="True"></cmsb:cmsbutton></td>
							<td class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnBack" runat="server" Text="Back"></cmsb:cmsbutton>
							<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" CausesValidation="True"></cmsb:cmsbutton></td>
						</tr>
					</table>
				</td>
			</tr>
			</TABLE><input id="hidAGENCY_ID" type="hidden" value="0" name="hidAGENCY_ID" runat="server">
			<input id="hidCHECK_ID" type="hidden" name="hidCHECK_ID" runat="server"> <input id="hidMonth" type="hidden" name="hidMonth" runat="server">
			<input id="hidYear" type="hidden" name="hidYear" runat="server">
		</form>
		</div>
	</body>
</HTML>
