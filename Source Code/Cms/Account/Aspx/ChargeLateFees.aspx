<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="ChargeLateFees.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.ChargeLateFees" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ChargeLateFees</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta http-equiv="Cache-Control" content="no-cache">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet>
		<script src="../../cmsweb/scripts/xmldom.js"></script>
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<script src="../../cmsweb/scripts/AJAXcommon.js"></script>	
		<script language="vbscript">
		
			Function getUserConfirmationForSave
				getUserConfirmationForSave= msgbox("Do you want to save the record?",35,"CMS")
			End function
		</script>
		
	<script language = javascript>
		var gError=false;
		var strRowNum="";
		function CallBackFun(AJAXREsponse)
		{	
			
			var lblStatus = document.getElementById('dgDepositDetails__ctl'+ strRowNum +  '_lblStatus');
			if(AJAXREsponse == "0^0^0^0^0")
			{			    
				alert('Entered Policy number does not exist. Please try again.');
				document.getElementById('dgDepositDetails__ctl' + strRowNum + '_txtPolicyNo').value = '';
				document.getElementById('dgDepositDetails__ctl' + strRowNum + '_txtTOTAL_DUE').value = '';	
				document.getElementById('dgDepositDetails__ctl' + strRowNum + '_txtPolicyNo').focus();
				return false;
			}
			if(AJAXREsponse == "-1^-1^-1^-1^-1") 
			{				
					//Alert replaced By lblMessage For Itrack Issue #6190
					//alert('Entered Policy Number is of AB type. Please enter only DB type Policy Number.');
					document.getElementById('dgDepositDetails__ctl' + strRowNum + '_lblStatus').innerHTML = "Entered Policy Number is of AB type. Please enter only DB type Policy Number.";
					document.getElementById('dgDepositDetails__ctl' + strRowNum + '_txtPolicyNo').value = '';
					document.getElementById('dgDepositDetails__ctl' + strRowNum + '_txtTOTAL_DUE').value = '';	
					document.getElementById('dgDepositDetails__ctl' + strRowNum + '_txtPolicyNo').focus();
					//document.getElementById('dgDepositDetails__ctl' + strRowNum + '_lblStatus').innerHTML ='';
					return false;					
			}	
			var strResponse = AJAXREsponse;					
			var PolicyStatus = strResponse.split("^");		
							
			var ResinFee = document.getElementById('dgDepositDetails__ctl'+ strRowNum + '_txtTOTAL_DUE');		
				
			if (ResinFee != null)
			ResinFee.value = formatAmount(PolicyStatus[0]);	
			//document.getElementById('hidCustomer').value = PolicyStatus[2];
			document.getElementById('dgDepositDetails__ctl' + strRowNum + '_hidCustomerId').value = PolicyStatus[2];			
			//alert(document.getElementById('hidCustomer').value);
			//document.getElementById('hidPolicy').value = PolicyStatus[3];
			document.getElementById('dgDepositDetails__ctl' + strRowNum + '_hidPOLICY_ID').value = PolicyStatus[3];
			//alert(document.getElementById('hidPolicy').value)
			//document.getElementById('hidPolicyVersion').value = PolicyStatus[4];
			document.getElementById('dgDepositDetails__ctl' + strRowNum + '_hidPOLICY_VERSION_ID').value = PolicyStatus[4];
						 
			 if(lblStatus !=null)
			{		
			
			//alert(PolicyStatus)
			    if(PolicyStatus[1]== 5)
			    {
			         
			                lblStatus.innerHTML = "Cancellation in progress.";
			    }
			    else if(PolicyStatus[1]== 6)
			    {
			         
			                lblStatus.innerHTML = "Marked for Cancellation.";
			    }
			    else if(PolicyStatus[1]== 7)
			    {
			         
			                lblStatus.innerHTML = "Cancelled.";
			    }
			    else if(PolicyStatus[1]== 8)
			    {
			         
			                lblStatus.innerHTML = "Cancelled.";
			    }
			    
			    else if(PolicyStatus[1]== 9)
			    {
			         
			                lblStatus.innerHTML = "Rescind in progress.";
			    }
				else
			   {          
							//alert(PolicyStatus[1])
			                //if(lblPolStatus !=null)  
			                //alert('blank')
			                lblStatus.innerHTML = "	";
			   }
			
			}
			var txtPolicyNo = document.getElementById('dgDepositDetails__ctl' + strRowNum + 'txtPolicyNo');
			if(txtPolicyNo != null)
			{
				txtPolicyNo.value = "";					
				txtPolicyNo.focus();
			}	
		}
		function OpenPolicyLookup(RowNo)
		{
			var url='<%=URL%>';
			var idField,valueField,lookUpTagName,lookUpTitle;
			var txtCtrl = "dgDepositDetails__ctl" + RowNo + "_txtPolicyNo";	
			var txtCtrl2 = "dgDepositDetails__ctl" + RowNo + "_txtCheckPolicyNo";
			var txtCtrl3 = "dgDepositDetails__ctl" + RowNo + "_txtTOTAL_DUE";			
		
			OpenLookupWithFunction(url,'POLICY_APP_NUMBER','CUSTOMER_ID_NAME','hidPOLICYINFO',txtCtrl,'DBPolicyForNSF','Policy','','splitPolicy(' + RowNo + ')');					
									
		}
		
		
		function splitPolicy(RowNo)
		{
			var ctrlPolicyId = "dgDepositDetails__ctl" + RowNo + "_hidPOLICY_ID"
			var ctrlPolicyVerId = "dgDepositDetails__ctl" + RowNo + "_hidPOLICY_VERSION_ID"
			var ctrlCustomerId = "dgDepositDetails__ctl" + RowNo + "_hidCustomerId"
			var ctrlPolicyNo = "dgDepositDetails__ctl" + RowNo + "_txtPolicyNo"
			var cltTOTAL_DUE = "dgDepositDetails__ctl" + RowNo + "_txtTOTAL_DUE"
			//var ctrlPolicyStatus = "dgDepositDetails__ctl" + RowNo + "_hidPOLICYSTATUS"	
			var txtCtrl = "dgDepositDetails__ctl" + RowNo + "_txtPolicyNo";
		    //var lblMsg = "dgDepositDetails__ctl" + RowNo + "_lblStatus";			
			if (document.getElementById("hidPOLICYINFO").value.length > 0)
			{
				
				var arr = document.getElementById("hidPOLICYINFO").value.split("~");
				document.getElementById(ctrlPolicyId).value = arr[0];
				document.getElementById(ctrlPolicyVerId).value = arr[1];
				document.getElementById(ctrlPolicyNo).value = arr[2];
				document.getElementById(txtCtrl).value = arr[2];
				//document.getElementById(ctrlPolicyStatus).value = arr[3];				
				document.getElementById(ctrlCustomerId).value = arr[6];		
				//document.getElementById(lblMsg).innerHTML = document.getElementById(ctrlPolicyStatus).value;				
				FillPolDetails(ctrlPolicyNo,RowNo);
			}
		}
		//Function commented For Itrack issue #6190 and Added a new Changes .
		/*function FillPolDetails(Ctrl,RowNo)
		{
		
			var PolNum;
			PolNum = document.getElementById(Ctrl).value;
			if (PolNum != "")
				FetchXML(PolNum,RowNo);
				
		}*/
		//Function Added  For Itrack Issue #6190
		function FillPolDetails(Ctrl,RowNo)
		{  
		   
		    var prefix = "dgDepositDetails__ctl" + RowNo;		 
			var txtPolicyNo = document.getElementById(prefix + "_txtPolicyNo");
			var txtPolicyNo2 = document.getElementById(prefix + "_txtCheckPolicyNo");
			var lblStatus = document.getElementById(prefix + "_lblStatus");			
			
			if ((txtPolicyNo.value != "" && txtPolicyNo2.value != "") && (txtPolicyNo.value.toUpperCase() != txtPolicyNo2.value.toUpperCase()))
			{			
				lblStatus.innerHTML = "Please check the policy number. Both policy numbers are not the same.";
				return false;
			}						
			
			else if ((txtPolicyNo.value != "" && txtPolicyNo2.value != "") && 
			(txtPolicyNo.value.toUpperCase() == txtPolicyNo2.value.toUpperCase()))
		   {
			var PolNum;
			PolNum = document.getElementById(Ctrl).value;						
			if (PolNum != "")
			{
				FetchXML(PolNum,RowNo);
			}
			}
			else if (txtPolicyNo.value != "") 
			{
				var PolNum ;
				PolNum = document.getElementById(Ctrl).value;
				//alert(PolNum)
				if(PolNum != "")
				{
					FetchXML(PolNum,RowNo);
				}			    
			}	
		}
		
		
		
		function FetchXML(PolNum,RowNo)
		{
			var ParamArray = new Array();
			obj1 = new Parameter('POLICY_NUMBER', PolNum)
			ParamArray.push(obj1);
			var objRequest = _CreateXMLHTTPObject();			
			var Action = 'LATE_FEES';			
			strRowNum = RowNo;			
			_SendAJAXRequest(objRequest,'LATE_FEES',ParamArray,CallBackFun);
		}		
				
		//This function checks the validation of controls
		function DoValidationCheckOnSave()
		{ 
	     if(Page_IsValid == true)
		{	     
		   returnValue= getUserConfirmationForSave();					
			if(returnValue==7 || returnValue==2)
			{ 
			  return false;
			}
		  
			var rowno = 0;
			var blnIsValid = true;
			for (ctr=1; ctr<=20; ctr++)
			{
				rowno = ctr + 1;
				var prefix = "dgDepositDetails__ctl" + rowno;
				var lblStatus = document.getElementById(prefix + "_lblStatus");
				var amtCtrl = document.getElementById(prefix + "_txtTOTAL_DUE");
				
				
				if (amtCtrl != null)
				{
					if (amtCtrl.value != "")
					{
						//Checking whether the other controls r properly filled or not
						
						//Checking for policy number
						var policyNo = document.getElementById(prefix + "_txtPolicyNo");
						var ctrlcheckPolicyNo = document.getElementById(prefix + "_txtCheckPolicyNo");
						
						if (policyNo.value.trim() == "")
						{
							document.getElementById(prefix + "_csvPolicyNo").style.display = "inline";
							blnIsValid = false;
						}
						
						//Checking for seconf policy number
						if (ctrlcheckPolicyNo.value.trim() == "")
						{
							document.getElementById(prefix + "_csvCheckPolicyNo").style.display = "inline";
							blnIsValid = false;
						}
						/*if (checkPolicyNo(rowno) == false)
						{
							blnIsValid = false;
						}*/
						if ((policyNo.value != "" && ctrlcheckPolicyNo.value != "") && (policyNo.value.toUpperCase() != ctrlcheckPolicyNo.value.toUpperCase()))
						{
							lblStatus.innerHTML = "Please check the policy number. Both policy numbers are not the same.";
								return false;
						}	
						
					}
					else
					{
						//Checking whether all other fields are empty or not
						var policyNo = document.getElementById(prefix + "_txtPolicyNo");
						var ctrlcheckPolicyNo = document.getElementById(prefix + "_txtCheckPolicyNo");
						
						if (policyNo.value.trim() != "")
						{
							blnIsValid = false;
							document.getElementById(prefix + "_csvTOTAL_DUE").style.display = "inline";
						}
						
						//Checking for seconf policy number
						if (ctrlcheckPolicyNo.value.trim() != "")
						{
							blnIsValid = false;
							document.getElementById(prefix + "_csvTOTAL_DUE").style.display = "inline";
						}
					}
				}
			}			

            //Check The Status Of Validators On Page
			if(blnIsValid==false) return false;;
			
			//returnValue= getUserConfirmationForSave();
					
			//if(returnValue==7 || returnValue==2)
			//{ 
			//  return false;
			//}
			
			if (blnIsValid == false)
			{
				return false;
			}		
		}
		 else
		  {
		    return false;
		  } 
		}
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
					txtReceiptAmount.value = InsertDecimal(amt);
					
					//Validating the amount, againt reguler exp
					//ValidatorOnChange();
				}
			}
		}
		 //Validate the deposit type fields against empty
		function ValidateDEPOSIT_TYPE(objSource, objArgs)
		{
			var ctrlAmt = objSource.id.replace("csvRECEIPT_FROM_ID", "txtTOTAL_DUE");
			ctrlAmt = document.getElementById(ctrlAmt);
			
			if (ctrlAmt.value == "")
			{
				//Validating only if amount id entered else declare as valid field
				objArgs.IsValid = true;
			}
			else
			{
				if (document.getElementById(objSource.controltovalidate).value == "")
				{
					//For empty field is invalid
					objArgs.IsValid = false;
				}
				else
				{
					//For empty field is valid
					objArgs.IsValid = true;
				}
			}
		}
		
		
		function CheckPolicyNumber(objSource, objArgs)
		{
		  
		  if (objSource.controltovalidate != null )
			{
			
				var ctrl = document.getElementById(objSource.controltovalidate);
				var rowNo = ctrl.getAttribute("RowNo");
				
				rowNo = document.getElementById("dgDepositDetails__ctl" + rowNo + "_txtTOTAL_DUE");
				
				if (rowNo.value != "")
				{
					if (ctrl.selectedIndex == -1)
					{
						objArgs.IsValid = false;
						gError=true;
					}
					else
					{
						objArgs.IsValid = true;
					}	
				}
				else
				{
					objArgs.IsValid = true;
				}
			}
		}
		
		
		function InsertPolicyTextBox()
		{
			for (ctr=1; ctr<=20; ctr++)
			{
				rowno = ctr + 1;
				var prefix = "dgDepositDetails__ctl" + rowno;
				var policyNo = document.getElementById(prefix + "_txtPolicyNo");
				var ctrlcheckPolicyNo = document.getElementById(prefix + "_txtCheckPolicyNo");
				if(policyNo== null)
				{
				  break;
				}
				policyNo.value =    ctrlcheckPolicyNo.value.trim()
			}	
			
		}
		
		/*function checkPolicyStatus(rowNo)
		{
			var prefix = "dgDepositDetails__ctl" + rowNo;
			var txtPolicyNo = document.getElementById(prefix + "_txtPolicyNo");
			var txtPolicyNo2 = document.getElementById(prefix + "_txtCheckPolicyNo");
			FetchPolStatusXML(txtPolicyNo.value,rowNo);
		}
		function FetchPolStatusXML(PolNum,RowNo)
		{
			var ParamArray = new Array();
			obj1=new Parameter('POLICY_NUMBER',PolNum);
			ParamArray.push(obj1);
			var objRequest = _CreateXMLHTTPObject();
			var Action = 'DEP_VALIDATE_POLNUM';
			strRowNum = RowNo;
			_SendAJAXRequest(objRequest,Action,ParamArray,CallbackFunPolStatus);
			strRowNum = RowNo;
		}
		function CallbackFunPolStatus(AJAXREsponse)
		{	
			var prefix = "dgDepositDetails__ctl" + strRowNum;
			var lblStatus = document.getElementById(prefix + "_lblStatus");
			var PolNumCtrl = new String();
			PolNumCtrl = lblStatus.id;
			var txtChkPolNum = PolNumCtrl.replace("lblStatus","txtCheckPolicyNo");
			var txtPolNum = PolNumCtrl.replace("lblStatus","txtPolicyNo");
			// If both PolicyNum controls are blank then don't show validation msg.
			if(document.getElementById(txtChkPolNum).value != "" && document.getElementById(txtPolNum).value != "")
			{
				//alert(AJAXREsponse)
				if(AJAXREsponse == "5")
				{
					if(lblStatus !=null)
						lblStatus.innerHTML = "Cancellation in progress.";
				}
				else if(AJAXREsponse == "6")
				{
					if(lblStatus !=null)
						lblStatus.innerHTML = "Marked for Cancellation.";							
				}
				else if(AJAXREsponse == "7")
				{	
					if(lblStatus !=null)
						lblStatus.innerHTML = "Cancelled.";
				}
				else if(AJAXREsponse == "8")
				{  
					if(lblStatus !=null)
						lblStatus.innerHTML = "Cancelled.";						
				}
				else if(AJAXREsponse == "9")
				{
					if(lblStatus !=null)
						lblStatus.innerHTML = "Rescind in progress.";						
				}
				else
				{	
					if(lblStatus !=null)
						lblStatus.innerHTML = "	";
				}
			}
		}*/
		
		
		//validate amount.
		function ValidateRECEIPT_AMOUNT(objSource, objArgs)
		{
			
			var ctrlAmt = objSource.id.replace("csvRECEIPT_FROM_ID", "txtTOTAL_DUE");
			ctrlAmt = document.getElementById(ctrlAmt);
			
			if (ctrlAmt.value == "")
			{
				//Validating only if amount id entered else declare as valid field
				objArgs.IsValid = true;
			}
			else
			{
				if (document.getElementById(objSource.controltovalidate).value == "")
				{
					//For empty field is invalid
					objArgs.IsValid = false;
				}
				else
				{
					//For empty field is valid
					objArgs.IsValid = true;
				}
			}
		}
		
		//Checking the policy number whther the policy number 1st and 3rd row are same or not
		//Function Commented and added same code in FillPolDetails() to avoid Ajax call again and again for Itrack issue #6190  
		function checkPolicyNo(rowNo)
		{
			//Function commented for Itrack issue 6190.
			/*var prefix = "dgDepositDetails__ctl" + rowNo;
			var txtPolicyNo = document.getElementById(prefix + "_txtPolicyNo");
			var txtPolicyNo2 = document.getElementById(prefix + "_txtCheckPolicyNo");
			var lblStatus = document.getElementById(prefix + "_lblStatus");
			
			if ((txtPolicyNo.value != "" && txtPolicyNo2.value != "") && (txtPolicyNo.value.toUpperCase() != txtPolicyNo2.value.toUpperCase()))
			{
				lblStatus.innerHTML = "Please check the policy number. Both policy numbers are not the same.";
				return false;
			}		
			
		    else if ((txtPolicyNo.value != "" && txtPolicyNo2.value != "") && (txtPolicyNo.value.toUpperCase() == txtPolicyNo2.value.toUpperCase()))
		   {    	    			    			    			    			    			    			    		       		        		        				
		        FetchXML(txtPolicyNo.value,rowNo);	
		   }  
			 
		    
		
		*/
		}	
		</script>
	</HEAD>
	
	<body leftMargin="0" topMargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;showScroll();findMouseIn();"
		rightMargin="0" MS_POSITIONING="GridLayout">
		<webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<form id="Resintatement_Fees" method="post" runat="server">
			<div class="pageContent" id="bodyHeight">
			<table cellSpacing="0" cellPadding="0" width="85%" align="center" border="0">
				<tr>
					<td>
						<TABLE id="Table1" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td class="headereffectcenter">Charge Late Fees For Policies
								</td>
							</tr>
							<tr>
								<td align="center"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False" EnableViewState="False"></asp:label></td>
							</tr>
							<tr>
								<td class="midcolora" width="40%">
									<asp:datagrid id="dgDepositDetails" runat="server" DataKeyField="First" AutoGenerateColumns="False"
										Width="100%" ItemStyle-Height="5">
										<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
										<ItemStyle CssClass="midcolora"></ItemStyle>
										<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
										<ItemStyle Height="4"></ItemStyle>
										<Columns>
											<asp:TemplateColumn ItemStyle-Width="1%">
												<ItemTemplate>
													<input type="hidden" id="hidCD_LINE_ITEM_ID" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "First")%>' NAME="hidCD_LINE_ITEM_ID">
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Policy #" ItemStyle-Width="11%">
												<ItemTemplate>
													<asp:TextBox ID="txtPolicyNo" MaxLength="8" size="10" Runat="server" TextMode="Password"></asp:TextBox>
													<span id="spnPOLICY_NO" runat="server"><IMG id="imgPOLICY_NO" style="CURSOR: hand" onclick="OpenPolicyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
															runat="server"> </span>
													<br>
													<asp:CustomValidator id="csvPolicyNo" runat="server" ControlToValidate="txtPolicyNo" ErrorMessage="Please select policy number."
														ClientValidationFunction="CheckPolicyNumber" Display="Dynamic"></asp:CustomValidator>
													<input type="hidden" id="hidPOLICY_ID" runat="server" NAME="hidPOLICY_ID">
													<input type="hidden" id="hidPOLICY_VERSION_ID" runat="server" NAME="hidPOLICY_VERSION_ID">
													<input type="hidden" id="hidCustomerId" runat="server" NAME="hidCustomerId">
													<input type="hidden" id="hidPOLICYSTATUS" runat="server" NAME="hidPOLICYSTATUS">
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Late Fee" ItemStyle-Width="12%">
												<ItemTemplate>
													<asp:TextBox ID="txtTOTAL_DUE" MaxLength="11" CssClass="InputCurrency" Runat="server" size="12"></asp:TextBox>
													<br>
													<asp:RegularExpressionValidator ID="revTOTAL_DUE" Runat="server" Display="Dynamic" ControlToValidate="txtTOTAL_DUE"></asp:RegularExpressionValidator>
													<asp:CustomValidator ID="csvTOTAL_DUE" Runat="server" ControlToValidate="txtTOTAL_DUE" Display="Dynamic"
														ErrorMessage="Please enter Late Fee  amount" ClientValidationFunction="ValidateRECEIPT_AMOUNT"></asp:CustomValidator>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Policy #" ItemStyle-Width="8%">
												<ItemTemplate>
													<asp:TextBox ID="txtCheckPolicyNo" Runat="server" MaxLength="8" size="10"></asp:TextBox>
													<br>
													<asp:CustomValidator id="csvCheckPolicyNo" runat="server" ControlToValidate="txtCheckPolicyNo" ErrorMessage="Please select policy number."
														ClientValidationFunction="CheckPolicyNumber" Display="Dynamic"></asp:CustomValidator>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Status" ItemStyle-Width="54%">
												<ItemTemplate>
													<asp:Label ID="lblStatus" CssClass="errmsg" Runat="server"></asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
									</asp:datagrid></td>
							</tr>
							<tr>
								<td>
									<table width="100%">
										<tr class="midcolora">
											<td align="right" width="100%">
											<!--<cmsb:cmsbutton class="clsButton" id="Cmsbutton1" runat="server" visible="false" Text="Late Fee Save"></cmsb:cmsbutton>-->
											<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save &amp; Commit"></cmsb:cmsbutton>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td><INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidDEPOSIT_ID" type="hidden" value="0" name="hidDEPOSIT_ID" runat="server">
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td><webcontrol:footer id="pageFooter" runat="server"></webcontrol:footer></td>
				</tr>
			</table>
			<input id="hidGLAccountXML" type="hidden" name="hidGLAccountXML" runat="server">
			<input type="hidden" id="hidPOLICYINFO" runat="server" NAME="hidPOLICYINFO">
			<input type="hidden" id="hidCustomer" runat="server" NAME="hidCustomer">
			<input type="hidden" id="hidPolicy" runat="server" NAME="hidPolicy">
			<input type="hidden" id="hidPolicyVersion" runat="server" NAME="hidPolicyVersion">
			
			 
			</DIV>
		</form>
	</body>
</HTML>


