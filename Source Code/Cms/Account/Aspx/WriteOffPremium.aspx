<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Page language="c#" Codebehind="WriteOffPremium.aspx.cs" AutoEventWireup="false" Inherits="Account.Aspx.WriteOffPremium" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Write Off Premium</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=base.GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		//COUNT NO OF CHECKBOXES CHECKED:
			var chkCount
			function CountCheckBoxes() 
			{
				chkCount = 0
				var checkBoxID = 'chkSelect';	
				re = new RegExp(':' + checkBoxID + '$')  //generated controlname starts with a colon
				
				for(i = 0; i < document.frmWriteoff.elements.length; i++) 
				{
					elm = document.frmWriteoff.elements[i]

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
			  if (chkCount == 0)
			  {
					alert("Please Select atleast One Record. !")
					return false;
			  }		
			}
			
		function formReset()
		{
			document.location.href = "WriteOffPremium.aspx";
		    return false;
		}
		
		//Select all Calaculation
		////Calculates the total Balance on Policy
		function CalculateTotalAmtOnSelectAll(obj)
		{
			var flag = true;
			var ctr = 1;
			var total = 0;
			
			while (flag == true)
			{
				strAmount = "grdWriteOff__ctl" + (ctr + 1) + "_lblBALANCE";
				
								
				if (document.getElementById(strAmount) != null)
				{
					bal  = document.getElementById(strAmount).innerHTML.trim();				
					if (bal!= "" && ! isNaN(bal))
						fillBalAmtOnChkChng(ctr+1,bal);
				}
				else
				{
					flag = false;
				}
				ctr++;
			}
			if(obj.checked== false)
			{
				//alert(totamt);
				document.getElementById('lblTotalAmount').innerHTML = "0.00";
				totamt = 0.00;
			}
		}
		
		function Uncheck()
		{
			document.getElementById('chSelectAll').checked = false;
		}
		
		
		function fillBalAmtOnChkChng(ctr,Amt)
		{
		
				var selChkID  = "grdWriteOff__ctl" + ctr + "_chkSelect";
				var objChk    = document.getElementById(selChkID);
				var txtPayAmt = "grdWriteOff__ctl" + ctr + "_txtAmtWriteOff";
				
				
					if(objChk.checked)
					{
						if(document.getElementById(txtPayAmt).value =="")
						{
							document.getElementById(txtPayAmt).value = Amt;
							//CalculateTotalWriteOffAmount(Amt);	
							UpdateWriteOffAmount(ctr);
							
						}
					}
					else
					{
						document.getElementById(txtPayAmt).value = "";
						//totamt = parseFloat(totamt) - parseFloat(Amt);
						UpdateWriteOffAmount(ctr);
						//document.getElementById('lblTotalAmount').innerHTML = totamt.toFixed(2);
					}
				
			
					
		}
		
		//Calculates the total due amount (Function Not In Use)
		var totamt=0.00;
		function CalculateTotalWriteOffAmount(Amt)
		{
			totamt = parseFloat(totamt) + parseFloat(Amt);
			//document.getElementById('lblTotalAmount').innerHTML = totamt.toFixed(2);
		}
		
		//Calculate Aggregate Write Off Amount on Blur / Select / Deselect
		function UpdateWriteOffAmount(ctr1)
		{
			var flag = true;
			var ctr = 1;
			var writeOffAmt = 0;
			
			while (flag == true)
			{
				strAmount = "grdWriteOff__ctl" + (ctr + 1) + "_txtAmtWriteOff";				
											
				if (document.getElementById(strAmount) != null)
				{
					bal  = document.getElementById(strAmount).value.trim().replace(",","");		
					if (bal!= "" && ! isNaN(bal))
					{
						writeOffAmt = writeOffAmt + parseFloat(bal);						
					}
				}
				else
				{
					flag = false;
				}
				ctr++;
			}
			document.getElementById("lblTotalAmount").innerHTML = (writeOffAmt.toFixed(2));
			FormatAmountLbl(document.getElementById("lblTotalAmount"));
				
		}
		
		//Formats the amount and convert ### into #.##
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
				
					CentPart = amt.substring(0,amt.length - 2);					
				 	txtReceiptAmount.value = InsertDecimal_WriteOff(amt);
				    
				}
			}
		}
		
				
		function InsertDecimal_WriteOff(AmtValues)
		{
			AmtValues = ReplaceAll(AmtValues,".","");
			DollarPart = AmtValues.substring(0, AmtValues.length - 2);
			CentPart = AmtValues.substring(AmtValues.length - 2);
			tmp = formatCurrency_WriteOff( DollarPart) + "." + CentPart;
			return tmp;
			
		}
		//Formats the amount and convert ### into #.## FOR LABEL INNERHTML
		function FormatAmountLbl(txtReceiptAmount)
		{
						
			if (txtReceiptAmount.innerHTML != "")
			{
				amt = txtReceiptAmount.innerHTML;
				
				amt = ReplaceAll(amt,".","");
				
				if (amt.length == 1)
					amt = amt + "0";
				
				if ( ! isNaN(amt))
				{
					
					DollarPart = amt.substring(0, amt.length - 2);
					CentPart = amt.substring(amt.length - 2);
					txtReceiptAmount.innerHTML = InsertDecimal_WriteOff(amt);
				}
			}
		}
		
		////Calculates the TOTAL BALANCE on Policy
		function CalculateBalanceOnPolicy()
		{
			var flag = true;
			var ctr = 1;
			var total = 0;
			
			while (flag == true)
			{
				strAmount = "grdWriteOff__ctl" + (ctr + 1) + "_lblBALANCE";
				
				if (document.getElementById(strAmount) != null)
				{
					var varBal = document.getElementById(strAmount).innerHTML.trim().replace(",","");		
					if (varBal != "" && ! isNaN(varBal))
						total = total + parseFloat(varBal);
				}
				else
				{
					flag = false;
				}
				ctr++;
			}
			document.getElementById("lblTotalBalance").innerHTML = (total.toFixed(2));
			FormatAmountLbl(document.getElementById("lblTotalBalance"));
			document.getElementById("hidBalanceAmount").value = (total.toFixed(2));
		}
		function SetFocus()
		{
			document.getElementById("txtPOLICY_ID").focus();	
			Uncheck();		
		}
		
		//SELECT DESELECT CHECKBOXES : PK
			function confun(elm)
				{					
						if (elm.checked)
						{
								aspCheckBoxID = 'chkSelect';	//for seond checkbox section
								re = new RegExp(':' + aspCheckBoxID + '$')  //generated controlname starts with a colon
								for(i = 0; i < document.frmWriteoff.elements.length; i++) {
									elm = document.frmWriteoff.elements[i]
									if (elm.type == 'checkbox') {
										if (re.test(elm.name)) 
										elm.checked = true;
										
									}
								}
							MakeReadOnly(false);	
						}
						else
						{
							
								aspCheckBoxID = 'chkSelect';	//for seond checkbox section
								re = new RegExp(':' + aspCheckBoxID + '$')  //generated controlname starts with a colon
								for(i = 0; i < document.frmWriteoff.elements.length; i++) {
									elm = document.frmWriteoff.elements[i]
									if (elm.type == 'checkbox') {
										if (re.test(elm.name)) 
										elm.checked = false;
									}
								}
							MakeReadOnly(true);		
						}
				}
				//Itrack 4361
		function ValidateWriteOffAmt(AmtCtrlID)
		  { 
				
					var writeOffAmtVal = document.getElementById(AmtCtrlID).value;
					var tmpStr = new String();
					tmpStr = AmtCtrlID;					
					var lblAmt = tmpStr.replace("txtAmtWriteOff","lblBALANCE");
					var TotAmtVal = document.getElementById(lblAmt).innerHTML;				    
					var csvVal = tmpStr.replace("txtAmtWriteOff","csvAMT_WRITE_OFF");										
				//Absolute Condition added for Itrack Issue #5590.	
				
				
				
				if(parseFloat(writeOffAmtVal.replace(",","")) < 0 && parseFloat(TotAmtVal) < 0)
				 {					  
				
						if((parseFloat(Math.abs(writeOffAmtVal.replace(",",""))) > parseFloat(Math.abs(TotAmtVal))) || parseFloat(writeOffAmtVal.replace(",","")) == 0)
						{								
				
							document.getElementById(csvVal).setAttribute('IsValid',false);
							document.getElementById(csvVal).setAttribute('enabled',true);
							document.getElementById(csvVal).style.display = 'inline';
							Page_IsValid = false;
						}	
						else
						{
							document.getElementById(csvVal).setAttribute('IsValid',true);
							document.getElementById(csvVal).setAttribute('enabled',false);
							document.getElementById(csvVal).style.display = 'none';
							Page_IsValid = true;
						}
				}
				else
				{
				
						if((parseFloat(writeOffAmtVal.replace(",","")) > parseFloat(TotAmtVal)) || parseFloat(writeOffAmtVal.replace(",","")) == 0 ||  parseFloat(writeOffAmtVal.replace(",","")) < 0 )
						{	
				
							document.getElementById(csvVal).setAttribute('IsValid',false);
							document.getElementById(csvVal).setAttribute('enabled',true);
							document.getElementById(csvVal).style.display = 'inline';
							Page_IsValid = false;
						}	
						else
						{
				
							document.getElementById(csvVal).setAttribute('IsValid',true);
							document.getElementById(csvVal).setAttribute('enabled',false);
							document.getElementById(csvVal).style.display = 'none';
							Page_IsValid = true;
						}  
					        
				}
				
				
		 }
			
		function EnableValidators(ctr)
		{
		
			var txtAmtWriteOff = document.getElementById("grdWriteOff__ctl" + ctr + "_txtAmtWriteOff");
			var rfvAMT_WRITE_OFF = document.getElementById("grdWriteOff__ctl" + ctr + "_rfvAMT_WRITE_OFF");			
			var revAMT_WRITE_OFF = document.getElementById("grdWriteOff__ctl" + ctr + "_revAMT_WRITE_OFF");			
			var csvAMT_WRITE_OFF = document.getElementById("grdWriteOff__ctl" + ctr + "_csvAMT_WRITE_OFF");			
			
			if (document.getElementById("grdWriteOff__ctl" + (ctr)+ "_chkSelect").checked == true)
			{		
			
				rfvAMT_WRITE_OFF.setAttribute("enabled",true);
				rfvAMT_WRITE_OFF.setAttribute("isValid",false);		
				rfvAMT_WRITE_OFF.style.display = "none";		
						
				revAMT_WRITE_OFF.setAttribute("enabled",true);
				revAMT_WRITE_OFF.setAttribute("isValid",false);
												
				csvAMT_WRITE_OFF.setAttribute("enabled",true);
				csvAMT_WRITE_OFF.setAttribute("isValid",false);
				
				txtAmtWriteOff.readOnly = false;
				
				
			}
			else if (document.getElementById("grdWriteOff__ctl" + (ctr)+ "_chkSelect").checked == false)
			{
				
				rfvAMT_WRITE_OFF.setAttribute("enabled",false);
				rfvAMT_WRITE_OFF.setAttribute("isValid",true);	
				rfvAMT_WRITE_OFF.style.display = "none";	
				
				revAMT_WRITE_OFF.setAttribute("enabled",false);
				revAMT_WRITE_OFF.setAttribute("isValid",true);	
				revAMT_WRITE_OFF.style.display = "none";	
				
				csvAMT_WRITE_OFF.setAttribute("enabled",false);
				csvAMT_WRITE_OFF.setAttribute("isValid",true);					
				csvAMT_WRITE_OFF.style.display ="none";
				
				txtAmtWriteOff.readOnly = true;
				
			}
			
		}		
		
		function ClearValidators()
		{
			
			var ctr = 2;
			var rfvValidator = document.getElementById("grdWriteOff__ctl" + ctr + "_rfvAMT_WRITE_OFF");
			var AmtWriteOff = document.getElementById("grdWriteOff__ctl" + ctr + "_txtAmtWriteOff");			
	
			
			while (rfvValidator != null)
			{
				if (rfvValidator == null )//|| rnvFeeAmtReversed == null)
				{
					break;
				}
				else
				{
					rfvValidator.setAttribute("enabled", false);
					rfvValidator.style.display = "none";
				}
				ctr++;
				rfvValidator = document.getElementById("grdWriteOff__ctl" + ctr + "_rfvAMT_WRITE_OFF");								
				AmtWriteOff = document.getElementById("grdWriteOff__ctl" + ctr + "_txtAmtWriteOff");			

			}
		}
		
		function MakeReadOnly(status)
		{
			var flag = true;
			var ctr = 1;
						
			while (flag == true)
			{
				
				txtAmtWriteOff = "grdWriteOff__ctl" + (ctr + 1) + "_txtAmtWriteOff";
				if (document.getElementById(txtAmtWriteOff) != null)
				{
					document.getElementById(txtAmtWriteOff).readOnly = status;
					EnableValidators(ctr + 1);
				}	
				else
					flag = false;
				ctr++;
			}
		
		}
			
		</script>
	</HEAD>
	<body oncontextmenu="return false;" leftmargin="0" rightmargin="0" onload="setfirstTime();ApplyColor();ChangeColor();top.topframe.main1.mousein = false;showScroll();findMouseIn();CalculateBalanceOnPolicy();SetFocus();ClearValidators();"
		MS_POSITIONING="GridLayout" onkeydown = "if(event.keyCode==13){ __doPostBack('btnFind',null); return false;}" >
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<form id="frmWriteoff" method="post" runat="server">
			<div id="bodyHeight" class="pageContent">
				<table cellSpacing="0" cellPadding="0" align="center" border="0" width="90%">
					<tr>
						<td>
							<TABLE id="Table1" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
								<tr>
									<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></td>
								</tr>
								<tr>
									<td class="pageheader" align="center" colSpan="4"></td>
								</tr>
								<tr>
									<td class="headereffectcenter" colSpan="4">Write Off Premium Amount</td>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="5"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPOLICY_ID" runat="server">Policy Number</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtPOLICY_ID" ReadOnly="False" runat="server" size="30" maxlength="21"></asp:textbox><IMG id="imgSelect" style="CURSOR: hand" alt="" src="../../cmsweb/images/selecticon.gif"
											runat="server"> <INPUT id="hidPolicyID" type="hidden" runat="server" NAME="hidPolicyID"><br>
										<asp:regularexpressionvalidator id="revPOLICY_ID" Runat="server" Display="Dynamic" ControlToValidate="txtPOLICY_ID"></asp:regularexpressionvalidator>
										<br>
										<asp:requiredfieldvalidator id="rfvPOLICY_ID" runat="server" ControlToValidate="txtPOLICY_ID" ErrorMessage="Please enter Policy Number."
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<td class="midcolorr" colSpan="4"><cmsb:cmsbutton class="clsButton" id="btnFind" runat="server" Text="Find"></cmsb:cmsbutton></td>
								</tr>
							</TABLE>
						</td>
					</tr>
					<tr>
						<TD class="pageHeader" colSpan="4">Please note that all operations are performed on 
							selected check boxes only.</TD>
					</tr>
				
					<TR class="headereffectWebGrid">
						<TD><asp:checkbox id="chSelectAll" Runat="server" Onclick="confun(this);CalculateTotalAmtOnSelectAll(this);"></asp:checkbox>Select All</TD>
					</TR>
		
					<tr>
						<td><asp:datagrid id="grdWriteOff" runat="server" DataKeyField="IDEN_ROW_ID" Width="100%" AlternatingItemStyle-CssClass="alternatedatarow"
								ItemStyle-CssClass="datarow" HeaderStyle-CssClass="headereffectCenter" AutoGenerateColumns="False"
								PagerStyle-HorizontalAlign="Center" PagerStyle-CssClass="datarow">
								<Columns>
									<asp:TemplateColumn HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center" HeaderText="Select">
										<ItemTemplate>
											<asp:CheckBox id="chkSelect" Runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Width="7%" HeaderText="Installment Description." HeaderStyle-ForeColor="white">
										<ItemTemplate>
											<asp:Label ID="lblPLAN_DESCRIPTION" Runat="server" text='<%#DataBinder.Eval(Container.DataItem,"PLAN_DESCRIPTION")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Width="6%" HeaderText="Due Date" HeaderStyle-ForeColor="white">
										<ItemTemplate>
											<asp:Label ID="lblDUE_DATE" Runat="server" text='<%#DataBinder.Eval(Container.DataItem,"DUE_DATE")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Width="8%" HeaderText="Amount Due" HeaderStyle-ForeColor="white">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblAMOUNT_DUE" Runat="server" text='<%#DataBinder.Eval(Container.DataItem,"AMOUNT_DUE")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Width="8%" HeaderText="Amount Paid" HeaderStyle-ForeColor="white">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblAMOUNT_PAID" Runat="server" text='<%#DataBinder.Eval(Container.DataItem,"AMOUNT_PAID")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Width="8%" HeaderText="Balance" HeaderStyle-ForeColor="white">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblBALANCE" Runat="server" text='<%#DataBinder.Eval(Container.DataItem,"BALANCE")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Width="10%" HeaderText="Amount to be Written Off" HeaderStyle-HorizontalAlign="center">
										<ItemTemplate>
											<asp:TextBox CssClass="INPUTCURRENCY" ID="txtAmtWriteOff" Runat="server" ReadOnly="True" size="12"
												maxlength="10"></asp:TextBox>
											<br>	
											<asp:requiredfieldvalidator id="rfvAMT_WRITE_OFF" runat="server" ControlToValidate="txtAmtWriteOff" ErrorMessage="Write Off Amount can not be blank."
												Display="Dynamic"></asp:requiredfieldvalidator>
											<asp:RegularExpressionValidator ID="revAMT_WRITE_OFF" Runat="server" ControlToValidate="txtAmtWriteOff" Display="Dynamic"></asp:RegularExpressionValidator>
											<asp:CustomValidator ID="csvAMT_WRITE_OFF" Runat="server" ControlToValidate="txtAmtWriteOff" Display="Dynamic"
												ErrorMessage="Premium Amount to be written off can neither be 0 nor can be greater than Balance Amount."></asp:CustomValidator>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></td>
					</tr>
					<tr>
						<td class="midcolorr" align="left" colSpan="4">
						Total Balance : 
							<asp:label id="lblTotalBalance" runat="server" CssClass="errmsg" text="0.00"></asp:label>
						Total Write Off Amount: 
							<asp:label id="lblTotalAmount" runat="server" CssClass="errmsg" text="0.00"></asp:label><asp:label id="lblSpace" runat="server" Width="150"></asp:label></td>
					</tr>
					<tr>
						<td class="midcolora">
							<table width="100%">
								<tr>
									<td class="midcolora">
										<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" enabled="false"></cmsb:cmsbutton>
									</td>
									<td class="midcolorr">
										<cmsb:cmsbutton class="clsButton" id="btnWriteoff" runat="server" Text="Write off"></cmsb:cmsbutton>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td><webcontrol:footer id="pageFooter" runat="server"></webcontrol:footer></td>
						<INPUT id="hidBalanceAmount" type="hidden" runat="server" NAME="hidBalanceAmount">
					</tr>
				</table>
			</div>
		</form>
	</body>
</HTML>
