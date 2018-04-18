<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Page validateRequest=false language="c#" Codebehind="AG_CheckListingMisc.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.AG_CheckListingMisc" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>BRICS-<%=Request.QueryString["Mode"]==null?"":"Add "%>
			Items to Reconcile</title>
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
	/*	function CheckBoxClicked(objCheckBox)
		{alert('this is an unused function')
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
		}*/
		
		//Formats the amount and convert 111 into 1.11
		function FormatAmount(txtAmount)
		{
						
			if (txtAmount.value != "")
			{
				amt = txtAmount.value;
				
				amt = ReplaceAll(amt,".","");
				
				if (amt.length == 1)
					amt = amt + "0";
				
				if ( ! isNaN(amt))
				{
					txtAmount.value = InsertDecimal(amt);
					
					//Validating the amount, againt reguler exp
					ValidatorOnChange();
				}
			}
		}
		
		function OpenDistributePopup(queryString)
		{
			var url="DistributeCashReceipt.aspx?"+queryString+"&opener=AutoCheckMisc";
			ShowPopup(url,'DistributeCheck',960,400);	
			
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
				//Show Save button
					if(document.getElementById("btnCreateChecks")!=null)
						document.getElementById("btnCreateChecks").setAttribute("disabled",false);
					if(document.getElementById("btnCreateChecksConfirm")!=null)
						document.getElementById("btnCreateChecksConfirm").setAttribute("disabled",false);
					if(document.getElementById("btnSaveTemp")!=null)
						document.getElementById("btnSaveTemp").setAttribute("disabled",false);
					if(document.getElementById("btnDelete")!=null)
						document.getElementById("btnDelete").setAttribute("disabled",false);
				}
				else
				{
					if(document.getElementById("btnCreateChecks")!=null)
						document.getElementById("btnCreateChecks").setAttribute("disabled",true);
					if(document.getElementById("btnCreateChecksConfirm")!=null)
						document.getElementById("btnCreateChecksConfirm").setAttribute("disabled",true);
					if(document.getElementById("btnSaveTemp")!=null)	
						document.getElementById("btnSaveTemp").setAttribute("disabled",true);
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
				if(document.getElementById("btnCreateChecksConfirm")!=null)
					document.getElementById("btnCreateChecksConfirm").setAttribute("disabled",false);
				if(document.getElementById("btnSaveTemp")!=null)	
					document.getElementById("btnSaveTemp").setAttribute("disabled",false);
				if(document.getElementById("btnDelete")!=null)	
					document.getElementById("btnDelete").setAttribute("disabled",false);
			}
			else
			{
				if(document.getElementById("btnCreateChecks")!=null)
					document.getElementById("btnCreateChecks").setAttribute("disabled",true);
				if(document.getElementById("btnCreateChecksConfirm")!=null)
					document.getElementById("btnCreateChecksConfirm").setAttribute("disabled",true);
				if(document.getElementById("btnSaveTemp")!=null)		
					document.getElementById("btnSaveTemp").setAttribute("disabled",true);
				if(document.getElementById("btnDelete")!=null)				
					document.getElementById("btnDelete").setAttribute("disabled",true);
			}
			EnableDisableSave() //Added on 17 June 2008
			SetValidators(objCheckBox);
		}
		function SetValidators(objCheckBox)
		{
			prefix="grdReconcileItems__ctl";
			suffix="_";
	//		var controlIDs = new Array("rfvPayeeName","revZip","rfvZip","rfvState","rfvCity","rfvAddress1","revPaymentAmount","rfvPaymentAmount");
			var controlIDs = new Array("rfvPayeeName","revZip","revPaymentAmount","rfvPaymentAmount");
			
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

		//Added by Mohit Agarwal 20-Aug-2007 ITrack 2373
		function setValidatorZip(txtZip)
		{
			prefix="grdReconcileItems__ctl";
			suffix="_";

			if(checkedCheckBoxes>0)
			{
				var i=0;
				for(;document.getElementById(prefix+i+suffix+"chkselect")==null;i++);
				rows=checkedCheckBoxes+i;
				for(j=0;j<checkedCheckBoxes;j++,i++)
				{
					controlPrefix = prefix+i+suffix;
					
					if(document.getElementById(controlPrefix+"txtZip").value == txtZip.value)
						document.getElementById(controlPrefix+"revZip").style.display = 'none';
					
				}
			}
		}
		
		//Added on 17 oct 2007
		var cntrlFlag=0;
		function EnableSave(row)
		{
			var prefix = "grdReconcileItems__ctl";
			var ID = row
			
			var suffix_Name = "_txtPayeeName";
			var suffix_Amount = "_txtPaymentAmount";
			var suffiex_chk = "_chkSelect";
		
			//Make payee Name control value
			var payeeNamectrl = prefix + ID + suffix_Name;
			var payeeAmtctrl = prefix + ID + suffix_Amount;
			var payeeChkctrl = prefix + ID + suffiex_chk;
			
			val1 =  document.getElementById(payeeNamectrl).value;
			val2 =  document.getElementById(payeeAmtctrl).value;
			val3 =  document.getElementById(payeeChkctrl);
			
			if(val3.checked==false)
			{
				if(val1!="" && val2!="")
				{
					document.getElementById("btnSaveTemp").setAttribute("disabled",false);				
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
						ctrlName = document.getElementById(prefix + "_txtPayeeName");
						ctrlAmount = document.getElementById(prefix + "_txtPaymentAmount");
									
						if (ctrlName != null && ctrlAmount != null)
						{
							if(ctrlName.value!="" && ctrlAmount.value!="")
							{
							
								cntrlFlag = 1;		
							// alert(cntrlFlag)		
								}
							else
							{
							// alert(cntrlFlag)	
					     		cntrlFlag = 0;
					     		}
							 
						
						}
						else
						{
							flag = false;
						}
						ctr++;
					}
				//End Checking	
							
				if(cntrlFlag == 1)
				{
					if(document.getElementById("btnSaveTemp")!=null)
						document.getElementById("btnSaveTemp").setAttribute("disabled",false);
				}	
								
				}
				
				//ENABLE VALIDATORS
				var controlIDs = new Array("rfvPayeeName","revZip","revPaymentAmount","rfvPaymentAmount");
				prefix="grdReconcileItems__ctl";
				suffix="_";
				controlPrefix=prefix+ID+suffix;
				if(val1!="" && val2!="")
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
		
		//End

		function chkDigit(zip)
		{
			for(var index =0; index < zip.value.length; index++)
			{
				if(zip.value.substring(index, index+1) <= '9' && zip.value.substring(index, index+1) >= '0')
					continue;
				else
					return false;
			}
			return true;		
		}
		
		function formatzip(txtZip)
		{
			if(txtZip.value.length == 9 && chkDigit(txtZip))
			{
				var value1 = txtZip.value.substring(0, 5);
				var value2 = txtZip.value.substring(5,9);
				txtZip.value = value1 + '-' + value2;
				setValidatorZip(txtZip);
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
					ctrl = document.getElementById(prefix + "_txtPaymentAmount");
				
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
				//Added on 17 June 2008 (Create Check only when Checked)
				if(document.getElementById("btnCreateChecks")!=null)
					document.getElementById("btnCreateChecks").setAttribute("disabled",false);
				if(document.getElementById("btnDelete")!=null)	
					document.getElementById("btnDelete").setAttribute("disabled",false);
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
				//Added on 17 June 2008 (Create Check only when Checked)
				if(document.getElementById("btnCreateChecks")!=null)
					document.getElementById("btnCreateChecks").setAttribute("disabled",true);
				if(document.getElementById("btnDelete")!=null)	
					document.getElementById("btnDelete").setAttribute("disabled",true);
			}
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
						ctrlName = document.getElementById(prefix + "_txtPayeeName");
						ctrlAmount = document.getElementById(prefix + "_txtPaymentAmount");
									
						if (ctrlName != null && ctrlAmount != null)
						{
							if(ctrlName.value!="" && ctrlAmount.value!="")
							{
								if(document.getElementById("btnSaveTemp")!=null)
									document.getElementById("btnSaveTemp").setAttribute("disabled",false);		
								break;
							}
							else
							{
								if(document.getElementById("btnSaveTemp")!=null)
									document.getElementById("btnSaveTemp").setAttribute("disabled",true);		
							}	
						
						
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
	<body oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="ChangeColor();ApplyColor();setfirstTime();top.topframe.main1.mousein = false;showScroll();findMouseIn();SetCheckedCheckBoxesCount();CheckBoxClicked(null);EnableDisableSave();"
		MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		 <DIV><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu></DIV>	
			<!-- To add bottom menu -->
			<!-- To add bottom menu ends here -->
			<div class="pageContent" id="bodyHeight">
				<TABLE cellSpacing="0" cellPadding="0" class="tablewidth" align="center" border="0">
					<tr>
						<td colspan="3">
							<TABLE id="Table1" cellSpacing="1" cellPadding="0" class="tablewidthheader" align="center"
								border="0">
								<tr>
									<td>
										<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
										<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
									</td>
								</tr>
								<tr>
									<TD class="headereffectCenter" colSpan="2">Miscellaneous Checks.</TD>
								</tr>
								<tr>
									<TD class="pageHeader" colSpan="2">Please note that all operations are performed on 
										selected checks only.</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="2"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capACCOUNT_ID" runat="server">Bank Account</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="100%"><asp:dropdownlist id="cmbACCOUNT_ID" onfocus="SelectComboIndex('cmbACCOUNT_ID')" runat="server">
											<asp:ListItem Value="0">0</asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvACCOUNT_ID" runat="server" ControlToValidate="cmbACCOUNT_ID" ErrorMessage="Please select Bank account."
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
						<td colspan="3"><asp:datagrid id="grdReconcileItems" runat="server" Width="100%" AlternatingItemStyle-CssClass="alternatedatarow"
								ItemStyle-CssClass="datarow" HeaderStyle-CssClass="headereffectCenter" AutoGenerateColumns="False" DataKeyField="CHECK_ID">
								<AlternatingItemStyle CssClass="alternatedatarow"></AlternatingItemStyle>
								<ItemStyle CssClass="datarow"></ItemStyle>
								<HeaderStyle CssClass="headereffectCenter"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Select">
										<ItemTemplate>
											<asp:CheckBox onclick="CheckBoxClicked(this);" ID="chkSelect" Runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Payee Name" ItemStyle-Height ="15%">
										<ItemTemplate>
											<asp:TextBox ID="txtPayeeName" maxlength="200"  Runat="server" text='<%# DataBinder.Eval(Container.DataItem,"PAYEE_ENTITY_NAME")%>'>
											</asp:TextBox><br>
											<asp:requiredfieldvalidator id="rfvPayeeName" Enabled="False" runat="server" ControlToValidate="txtPayeeName"
												ErrorMessage="ACCOUNT_ID can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Payment Amount" ItemStyle-Height ="15%">
										<ItemTemplate>
											<asp:TextBox ID="txtPaymentAmount" maxlength="12" Runat="server" text='<%# DataBinder.Eval(Container.DataItem,"CHECK_AMOUNT")%>' CssClass="INPUTCURRENCY" style="text-align:right" >
											</asp:TextBox><br>
											<asp:RegularExpressionValidator ID="revPaymentAmount" Enabled="False" Runat="server" ControlToValidate="txtPaymentAmount"
												Display="Dynamic" ErrorMessage="expression"></asp:RegularExpressionValidator>
											<asp:requiredfieldvalidator id="rfvPaymentAmount" Enabled="False" runat="server" ControlToValidate="txtPaymentAmount"
												ErrorMessage="ACCOUNT_ID can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Address1" ItemStyle-Height ="15%">
										<ItemTemplate>
											<asp:TextBox ID="txtAddress1"  maxlength="70" Runat="server" text='<%# DataBinder.Eval(Container.DataItem,"PAYEE_ADD1")%>'>
											</asp:TextBox><br>
									<%--		<asp:requiredfieldvalidator id="rfvAddress1" Enabled="False" runat="server" ControlToValidate="txtAddress1"
												ErrorMessage="ACCOUNT_ID can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>--%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Address2" ItemStyle-Height ="15%">
										<ItemTemplate>
											<asp:TextBox ID="txtAddress2" maxlength="70" Runat="server" text='<%# DataBinder.Eval(Container.DataItem,"PAYEE_ADD2")%>'>
											</asp:TextBox>											
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="City" ItemStyle-Height ="15%">
										<ItemTemplate>
											<asp:TextBox ID="txtCity" Runat="server" maxlength="40" text='<%# DataBinder.Eval(Container.DataItem,"PAYEE_CITY")%>'>
											</asp:TextBox><br>
										<%--	<asp:requiredfieldvalidator id="rfvCity" Enabled="False" runat="server" ControlToValidate="txtCity" ErrorMessage="ACCOUNT_ID can't be blank."
												Display="Dynamic"></asp:requiredfieldvalidator>--%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="State" ItemStyle-Height ="15%">
										<ItemTemplate>
										<asp:DropDownList ID="cmbState" Runat="server"></asp:DropDownList>
										
										<%--	<asp:requiredfieldvalidator id="rfvState" Enabled="False" runat="server" ControlToValidate="txtState" ErrorMessage="ACCOUNT_ID can't be blank."
												Display="Dynamic"></asp:requiredfieldvalidator>--%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Zip" ItemStyle-Height ="15%">
									
										<ItemTemplate>
											<asp:TextBox ID="txtZip" width="80" Runat="server" maxlength="10" onblur="formatzip(this);" text='<%# DataBinder.Eval(Container.DataItem,"PAYEE_ZIP")%>'>
											</asp:TextBox><br>
										<%--	<asp:requiredfieldvalidator id="rfvZip" Enabled="False" runat="server" ControlToValidate="txtZip" ErrorMessage="ACCOUNT_ID can't be blank."
												Display="Dynamic"></asp:requiredfieldvalidator>--%>
											<asp:RegularExpressionValidator ID="revZip" Enabled="False" Runat="server" ControlToValidate="txtZip" Display="Dynamic"
												ErrorMessage="expression"></asp:RegularExpressionValidator>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="10%"></HeaderStyle>
										<ItemStyle CssClass="midcolora"></ItemStyle>
										<ItemTemplate>
											<asp:HyperLink id="hlkOpenDistibute" runat="server" Visible="false" NavigateUrl=""><asp:label id="lblDistribute" runat="server" text='<%# DataBinder.Eval(Container.DataItem,"DISTRIBUTION_STATUS")%>'></asp:label></asp:HyperLink>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></td>
					</tr>
					<tr>
						<td class="midcolora" colSpan="3">&nbsp;</td>
					</tr>
					<tr>
						<td class="midcolora" colSpan="1">
							<cmsb:cmsbutton class="clsButton" id="btnCreateChecks" runat="server" Text="Create Checks" enabled="false"></cmsb:cmsbutton>
							<cmsb:cmsbutton class="clsButton" id="btnCreateChecksConfirm" runat="server" Text="Confirm Distribution and Create Checks"
								enabled="false"></cmsb:cmsbutton>
							<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete Checks" enabled="false"></cmsb:cmsbutton>
						</td>
						<td class="midcolorr" colSpan="1">
							<cmsb:cmsbutton class="clsButton" id="btnBack" runat="server" Text="Back"></cmsb:cmsbutton>
							<cmsb:cmsbutton class="clsButton" id="btnSaveTemp" runat="server" Text="Save" enabled="false"></cmsb:cmsbutton>
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
