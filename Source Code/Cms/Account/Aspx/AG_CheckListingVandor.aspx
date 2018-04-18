<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page validateRequest=false language="c#" Codebehind="AG_CheckListingVandor.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.AG_CheckListingVandor" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>BRICS-<%=Request.QueryString["Mode"]==null?"":"Add "%>  Items to Reconcile</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta http-equiv="Page-Enter" content="revealtrans(duration=0.0)">
		<meta http-equiv="Page-Exit" content="revealtrans(duration=0.0)">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=base.GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script src="../../cmsweb/scripts/AJAXcommon.js"></script>
		<script language="javascript">
		var SelectedCheckBoxes=<%=NoOfRows%>;
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
		function FocusLoad()
		{
		   if(document.getElementById('grdReconcileItems__ctl2_chkselect')!=null)
		   {
			document.getElementById('grdReconcileItems__ctl2_chkselect').focus();
		   }
		}
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
		function OpenDistributePopup(queryString)
		{
			var url="DistributeCashReceipt.aspx?"+queryString+"&opener=AutoCheckMisc";
			ShowPopup(url,'DistributeCheck',900,600);	
			
		//	return false;			
		}
		var checkedCheckBoxes =0;
		function SetCheckedCheckBoxesCount()
		{
			checkedCheckBoxes = parseInt(document.getElementById("hidNoOfRowsDisplyed").value);
		}
		function CheckBoxClicked(objCheckBox)	
		{
			if(objCheckBox==null)
			{
				if(checkedCheckBoxes>0)
				{
					if(document.getElementById("btnCreateChecks")!=null)
						document.getElementById("btnCreateChecks").setAttribute("disabled",false);
				//	document.getElementById("btnCreateChecksConfirm").setAttribute("disabled",false);
				   if(document.getElementById("btnSave")!=null)
						document.getElementById("btnSave").setAttribute("disabled",false);
					
					if(document.getElementById("btnDelete")!=null)
						document.getElementById("btnDelete").setAttribute("disabled",false);
				}
				else
				{
				
					if(document.getElementById("btnCreateChecks")!=null)
						document.getElementById("btnCreateChecks").setAttribute("disabled",true);
				//	document.getElementById("btnCreateChecksConfirm").setAttribute("disabled",true);
					if(document.getElementById("btnSave")!=null)
						document.getElementById("btnSave").setAttribute("disabled",true);
				
					if(document.getElementById("btnDelete")!=null)
						document.getElementById("btnDelete").setAttribute("disabled",true);
				}
				SetValidators(objCheckBox);
				return;
			}
			if(objCheckBox.checked)
				checkedCheckBoxes++;
			else
				checkedCheckBoxes--;
			if(checkedCheckBoxes>0)
			{
				if(document.getElementById("btnCreateChecks")!=null)
					document.getElementById("btnCreateChecks").setAttribute("disabled",false);
				//document.getElementById("btnCreateChecksConfirm").setAttribute("disabled",false);
				if(document.getElementById("btnSave")!=null)
					document.getElementById("btnSave").setAttribute("disabled",false);
				
				if(document.getElementById("btnDelete")!=null)	
					document.getElementById("btnDelete").setAttribute("disabled",false);
			}
			else
			{
				if(document.getElementById("btnCreateChecks")!=null)
					document.getElementById("btnCreateChecks").setAttribute("disabled",true);
				//document.getElementById("btnCreateChecksConfirm").setAttribute("disabled",true);
				if(document.getElementById("btnSave")!=null)
					document.getElementById("btnSave").setAttribute("disabled",true);
					
				if(document.getElementById("btnDelete")!=null)	
					document.getElementById("btnDelete").setAttribute("disabled",true);
			}
		SetValidators(objCheckBox);
		}
		function SetValidators(objCheckBox)
		{
			prefix="grdReconcileItems__ctl";
			suffix="_";
			var controlIDs = new Array("rfvReinsuranceCompany");
			
			if(objCheckBox==null)
			{
				if(checkedCheckBoxes>0)
				{
					var i=0;
					for(;document.getElementById(prefix+i+suffix+"chkselect")==null;i++);
					rows=checkedCheckBoxes+i;
					for(j=0;j<checkedCheckBoxes;j++,i++)
					{
						controlPrefix = prefix+i+suffix;
						for(k=0;k<controlIDs.length;k++)				
						{
							document.getElementById(controlPrefix+controlIDs[k]).setAttribute("enabled",true);
						}
					}
				}
				return;
			}
		/**************************************************************/
			index=objCheckBox.id.substring(objCheckBox.id.indexOf("ctl")+3,objCheckBox.id.lastIndexOf("_"));
			controlPrefix=prefix+index+suffix;
			if(objCheckBox.checked)
			{
				for(i=0;i<controlIDs.length;i++)
				{
					document.getElementById(controlPrefix+controlIDs[i]).setAttribute("enabled",true);
				}
			}					
			else			
			{
				for(i=0;i<controlIDs.length;i++)
				{
					document.getElementById(controlPrefix+controlIDs[i]).setAttribute("enabled",false);
					document.getElementById(controlPrefix+controlIDs[i]).style.display = "None";
				}
			}
			
		}
		
		// Shows Vendor Address in next column when Vendor is selected from dropdown
		function ShowVendorAddress(lblVendorAddress,cmbVendor)
		{
			combo = document.getElementById(cmbVendor);
			if(combo==null || combo.selectedIndex==-1 || combo.selectedIndex==0)
				return;
			var VendorData = new String(combo.options[combo.selectedIndex].value);
			var VendorArray = VendorData.split('~');
			if(VendorArray.length==0)
				return;
			VendorID = VendorArray[0];
			// Assign corresponding Vendor Address into the Label field
			document.getElementById(lblVendorAddress).innerText = VendorArray[2] + ' ' + VendorArray[3] + ' ' + VendorArray[4] + ' ' + VendorArray[5];
		}
		// open vendor Pending Invoices
		function OpenVendorInvoices(cmbVendor,CheckId)
		{
			combo = document.getElementById(cmbVendor);
			if(combo==null || combo.selectedIndex==-1 || combo.selectedIndex==0)
				return;
			var VendorData = new String(combo.options[combo.selectedIndex].value);
			var VendorArray = VendorData.split('~');
			if(VendorArray.length==0)
				return;
			VendorID = VendorArray[0];
			window.open("PendingVendorInvoices.aspx?VENDOR_ID=" + VendorID+ "&CHECK_ID="+CheckId,'','toolbar=no, directories=no, location=no, status=no, menubar=no, resizable=yes, scrollbars=yes,  width=800, height=600'); 
		}
		
		function ValidateEFTValue(PaymentCombo)
		{
			var cmbVen = new String();
			cmbVen = PaymentCombo;
			var VendorCombo = cmbVen.replace("cmbpaymentmode","cmbReinsuranceCompany");
			combo = document.getElementById(VendorCombo);
			if(combo==null || combo.selectedIndex==-1 || combo.selectedIndex==0)
				return;
			var VendorData = new String(combo.options[combo.selectedIndex].value);
			var VendorArray = VendorData.split('~');
			if(VendorArray.length==0)
				return;
			var AllowEFT = VendorArray[6];
		   if(AllowEFT == '10964')//No
			{
				document.getElementById(PaymentCombo).selectedIndex = 0;
				alert('EFT is not allowed for the selected Vendor');
			}
		}
		
		// Checks with -ve amt cannot be created.
			function fnCheckForNegAmt()
			{
				var flag = true;
				var ctr = 1;
				var total = 0;
				
				while (flag == true)
				{
					rowNo = ctr + 1;
					prefix = "grdReconcileItems__ctl" + rowNo;
					ctrl = document.getElementById(prefix + "_txtPremiumAmount");
				
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
			
		/*	Modified Praveen kasana :
			The user puts in 3 entires and then does a select all 
			The program check all the boxes even for row items that have no detail 
			User can not save the page ... looking for details 
			When they do select all - just ckeck off the rows that have detail
			
		 */	
			
		function chkSelDeSelAll(chkAllctrl)
		{
			if (chkAllctrl.checked)
			{
				aspCheckBoxID = 'chkSelect';	//for seond checkbox section
				re = new RegExp(':' + aspCheckBoxID + '$')  
				for(i = 0; i < document.Form1.elements.length; i++)
				{
				    var ctrl = "grdReconcileItems__ctl" + i +"_cmbReinsuranceCompany";
				    var elem = document.getElementById(ctrl);
				    if(elem!=null)
				    {
						if(elem.value!="")
						{
							chkAllctrl = "grdReconcileItems__ctl" + i +"_chkSelect"; 
							var Only_chks = document.getElementById(chkAllctrl)
							Only_chks.checked = true;CheckBoxClicked(Only_chks);
													
							
						}
				    }			    
				    
				    /*chkAllctrl = document.Form1.elements[i]
						if (chkAllctrl.type == 'checkbox')
						{
							if (re.test(chkAllctrl.name)) 
							{chkAllctrl.checked = true;CheckBoxClicked(chkAllctrl);	}
						}
					*/
						
						
				}
			}
			else
			{
				aspCheckBoxID = 'chkSelect';	//for seond checkbox section
				re = new RegExp(':' + aspCheckBoxID + '$')  
				for(i = 0; i < document.Form1.elements.length; i++)
				 {
				  var ctrl = "grdReconcileItems__ctl" + i +"_cmbReinsuranceCompany";
				    var elem = document.getElementById(ctrl);
				    if(elem!=null)
				    {
						if(elem.value!="")
						{
							chkAllctrl = "grdReconcileItems__ctl" + i +"_chkSelect"; 
							var Only_chks = document.getElementById(chkAllctrl)
							Only_chks.checked = false;CheckBoxClicked(Only_chks);
													
							
						}
				    }			
					/*chkAllctrl = document.Form1.elements[i]
					if (chkAllctrl.type == 'checkbox')
					{
						if (re.test(chkAllctrl.name)) 
						{chkAllctrl.checked = false;CheckBoxClicked(chkAllctrl);}	
					}*/
				}
			}
		}
		
		/*****************Modified on 18 June 2008 *****************/
		//Added on 17 oct 2007
		var cntrlFlag=0;
		function EnableSave(row)
		{
			var prefix = "grdReconcileItems__ctl";
			var ID = row
			
			var suffix_Name = "_cmbReinsuranceCompany";
			var suffiex_chk = "_chkSelect";
		
			//Make payee Name control value
			var payeeNamectrl = prefix + ID + suffix_Name;
			var payeeChkctrl = prefix + ID + suffiex_chk;
			
			val1 =  document.getElementById(payeeNamectrl).value;
			chkRow =  document.getElementById(payeeChkctrl);
			
			if(chkRow.checked==false)
			{
				if(val1!="")
				{
					document.getElementById("btnSave").setAttribute("disabled",false);				
				}
				else
				{
				//checking all other ///
				//While loop
					var flag = true;
					var ctr = 1;
								
					while (flag == true)
					{
						rowNo = ctr + 1;
						prefix = "grdReconcileItems__ctl" + rowNo;
						ctrlName = document.getElementById(prefix + "__cmbReinsuranceCompany");
						
									
						if (ctrlName != null)
						{
							if(ctrlName.value!="")
								cntrlFlag = 1;		
							else
								cntrlFlag = 0;
					    }
						else
							flag = false;
							
						ctr++;
					}
				//End Checking	
							
				if(cntrlFlag == 1)
					document.getElementById("btnSave").setAttribute("disabled",false);
								
				}
				
				//ENABLE VALIDATORS
				var controlIDs = new Array("rfvReinsuranceCompany");
				prefix="grdReconcileItems__ctl";
				suffix="_";
				controlPrefix=prefix+ID+suffix;
				if(val1!="")
				{
					for(i=0;i<controlIDs.length;i++)
					{
						document.getElementById(controlPrefix+controlIDs[i]).setAttribute("enabled",true);
					}
				}					
				else			
				{
					for(i=0;i<controlIDs.length;i++)
					{
						document.getElementById(controlPrefix+controlIDs[i]).setAttribute("enabled",false);
						document.getElementById(controlPrefix+controlIDs[i]).style.display = "None";
					}
				}	
			}		
			EnableDisableSave(); //Added on 17 June 2008
		}
		//**************************** Added on 17 June
		function EnableDisableSave()
		{
		
			var flag = true;
			var ctr = 1;	
					while (flag == true)
					{
						rowNo = ctr + 1;
						prefix = "grdReconcileItems__ctl" + rowNo;
						ctrlName = document.getElementById(prefix + "_cmbReinsuranceCompany");
													
						if (ctrlName != null)
						{
							if(ctrlName.value!="")
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
		
		
		//End
	
		</script>
</HEAD>
	<body oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;showScroll();findMouseIn();SetCheckedCheckBoxesCount();CheckBoxClicked(null);FocusLoad();EnableDisableSave();"
		MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		<DIV><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu></DIV>			
			<!-- To add bottom menu -->
			<!-- To add bottom menu ends here -->
			<div class="pageContent" id="bodyHeight">
				<TABLE class="tablewidth" cellSpacing="0" cellPadding="0" align="center" border="0">
					<tr>
						<td colSpan="3">
							<TABLE class="tablewidthheader" id="Table1" cellSpacing="1" cellPadding="0" align="center"
								border="0">
								<tr>
									<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer><asp:placeholder id="GridHolder" runat="server"></asp:placeholder></td>
								</tr>
								<tr>
									<TD class="headereffectCenter" colSpan="2">Vendor Checks</TD>
								</tr>
								<tr>
									<TD class="pageHeader" colSpan="2">Please note that all operations are performed on 
										selected checks only.</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="2"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capACCOUNT_ID" runat="server">Bank Account</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="100%"><asp:dropdownlist id="cmbACCOUNT_ID" onfocus="SelectComboIndex('cmbACCOUNT_ID')" runat="server">
											<asp:ListItem Value="0">0</asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvACCOUNT_ID" runat="server" Display="Dynamic" ErrorMessage="ACCOUNT_ID can't be blank."
											ControlToValidate="cmbACCOUNT_ID"></asp:requiredfieldvalidator></TD>
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
						<td colSpan="3"><asp:datagrid id="grdReconcileItems" runat="server" DataKeyField="CHECK_ID" AutoGenerateColumns="False"
								HeaderStyle-CssClass="headereffectCenter" ItemStyle-CssClass="datarow" AlternatingItemStyle-CssClass="alternatedatarow"
								Width="100%">
								<AlternatingItemStyle CssClass="alternatedatarow"></AlternatingItemStyle>
								<ItemStyle CssClass="datarow"></ItemStyle>
								<HeaderStyle CssClass="headereffectCenter"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Select" ItemStyle-Width="10%">
										<ItemTemplate>
											<asp:CheckBox onclick="CheckBoxClicked(this);" ID="chkSelect" Runat="server"></asp:CheckBox>
											<input type="hidden" id="hidTEMP_CHECK_ID" runat ="server" value = '<%# DataBinder.Eval(Container.DataItem,"CHECK_ID")%>'>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Vendor" ItemStyle-Width="25%">
										<ItemTemplate>											
											<SELECT id="cmbReinsuranceCompany"  runat="server" ></SELECT>
											<asp:requiredfieldvalidator id="rfvReinsuranceCompany" Enabled="False" runat="server" ControlToValidate="cmbReinsuranceCompany" ErrorMessage="ACCOUNT_ID can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Vendor Address" ItemStyle-Width="25%">
										<ItemTemplate>
											<asp:Label ID="lblVENDOR_ADDRESS" Runat="server"></asp:Label><br />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Payment Amount" ItemStyle-Width="18%">
										<ItemTemplate>
											<asp:TextBox ID="txtPremiumAmount" maxlength="9" class="INPUTCURRENCY" Runat="server" text='<%# DataBinder.Eval(Container.DataItem,"CHECK_AMOUNT")%>' onBlur="FormatAmount(this)" ReadOnly="True">
											</asp:TextBox>
											<img runat="server" id="imgVendorInvoices" onclick="OpenVendorInvoices()" alt="" src="../../cmsweb/images/selecticon.gif"><br />
											<asp:RegularExpressionValidator ID="revPremiumAmount" Enabled="False" Runat="server" ControlToValidate="txtPremiumAmount" Display="Dynamic" ErrorMessage="expression"></asp:RegularExpressionValidator>
											</asp:requiredfieldvalidator>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:templatecolumn headertext="Mode of Payment">
										<itemtemplate>
											<asp:dropdownlist runat="server" id="cmbpaymentmode" width="90"></asp:dropdownlist>
											<INPUT id="hidAllowEFT" type="hidden" name="hidAllowEFT" runat="server">
										</itemtemplate>
									</asp:templatecolumn>
									<asp:TemplateColumn HeaderText="Note" ItemStyle-Width="20%">
										<ItemTemplate>
											<asp:TextBox ID="txtNote" maxlength="100" Runat="server" text='<%# DataBinder.Eval(Container.DataItem,"CHECK_NOTE")%>'>
											</asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></td>
					</tr>
					<tr>
						<td class="midcolora" colSpan="3">&nbsp;</td>
					</tr>
					<tr>
						<td class="midcolora" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnCreateChecks" runat="server" enabled="false" Text="Create Checks"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" enabled="false" Text="Delete Checks"
								CausesValidation="false"></cmsb:cmsbutton></td>
						<td class="midcolorr" colSpan="1">
							<cmsb:cmsbutton class="clsButton" id="btnBack" runat="server" Text="Back"></cmsb:cmsbutton>
							<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" enabled="false" Text="Save"></cmsb:cmsbutton>
						</td>
					</tr>
					<tr>
						<td><webcontrol:footer id="pageFooter" runat="server"></webcontrol:footer>
				<INPUT id="hidNoOfRowsDisplyed" type="hidden" name="hidNoOfRowsDisplyed" runat="server">
						</td>
					</tr>
				</TABLE>
				
			</div>
		</form>
	</body>
</HTML>
