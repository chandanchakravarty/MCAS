<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page language="c#" Codebehind="PolicyAddLocation.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.PolicyAddLocation" validateRequest=false %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>POL_LOCATIONS</title>
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
		var GlobalError = false;
		var CalledFromSave = false;
		function Validate()
		{
			//var result = GetZipForState(true);
			Page_ClientValidate();
			Page_IsValid = Page_IsValid; //&& result;
			return Page_IsValid;
		}
		
		function ShowHideRentedWeekly()
		{	
			if (document.getElementById('cmbRENTED_WEEKLY').options.selectedIndex == "2" && document.getElementById('cmbLOCATION_TYPE').value == "11849")
			{
				//document.getElementById('trWEEKS_RENTED').style.display='inline';
				document.getElementById('tdWeeksRentedTextBox').style.display='inline';
				document.getElementById('tdWeeksRentedLabel').style.display='inline';
			}
			else
			{
				//document.getElementById('trWEEKS_RENTED').style.display='none';
				document.getElementById('tdWeeksRentedTextBox').style.display='none';
				document.getElementById('tdWeeksRentedLabel').style.display='none';
			}
		}
		
		function GetCounty()
		{
			if(document.getElementById('txtLOC_ZIP').value!="")
			{
				if(isNaN(document.getElementById('txtLOC_ZIP').value))
					return;
				//Added by Manoj Rathore on 29 Jun. 2009 Itrack # 6029
				var strZip = document.getElementById('txtLOC_ZIP').value;				
				PolicyAddLocation.AjaxGetCountyForZip(strZip,PutTerritory);									
			}			
		}
		function PutTerritory(Result)
		{
			if(Result.error)
			{
				var xfaultcode   = Result.errorDetail.code;
				var xfaultstring = Result.errorDetail.string;
				var xfaultsoap   = Result.errorDetail.raw;        				
			}
			else		
				document.getElementById("txtLOC_COUNTY").value= Result.value;
		}
		
		function AddData()
		{			
			//Added By Raghav on 07/17/2008
			if(document.getElementById('hidGenerallocation').value != '0')
			{
				document.getElementById('trBody').style.display='none';
				alert("Can't add new record as an active Location already exists here.");
				return; 
			}
			
			document.getElementById('hidLOCATION_ID').value	=	'New';
			//document.getElementById('txtLOC_NUM').value  = '';
			if(document.getElementById('hidLOCATION_ID').value=='New');
			{
			document.getElementById('txtLOC_NUM').value = document.getElementById('hidLocationCode').value;
			}
			document.getElementById('cmbIS_PRIMARY').options.selectedIndex = 1;
			document.getElementById('txtLOC_ADD1').value  = '';
			document.getElementById('txtLOC_ADD2').value  = '';
			document.getElementById('txtLOC_CITY').value  = '';
			document.getElementById('txtLOC_COUNTY').value  = '';
		
			document.getElementById('txtLOC_ZIP').value  = '';
			document.getElementById('txtDESCRIPTION').value='';
			document.getElementById('cmbLOCATION_TYPE').options.selectedIndex = -1;
			if(document.getElementById('btnDelete'))
				document.getElementById('btnDelete').setAttribute('disabled',true);
			
			
			//RP
			document.getElementById('cmbRENTED_WEEKLY').options.selectedIndex=0;			
			document.getElementById('txtWEEKS_RENTED').value="";	
			//document.getElementById('trWEEKS_RENTED').style.display='none';
			document.getElementById('tdWeeksRentedTextBox').style.display='none';
			document.getElementById('tdWeeksRentedLabel').style.display='none';
			document.getElementById('trRENTED_WEEKLY').style.display='none';
			
			ChangeColor();
			DisableValidators();
			document.getElementById('txtLOC_NUM').focus();
		}
		function populateXML()
		{
		
			if(document.getElementById('hidFormSaved').value == '0')
			{
				
				if ( document.getElementById('hidSubmitZip').value == '' )
				{
					if(document.getElementById('hidOldData').value != "")
					{
						//alert(document.getElementById('hidOldData').value);
						populateFormData(document.getElementById('hidOldData').value, POL_LOCATIONS);
					}
					else
					{					
						AddData();
					}
				}
			}
			ShowHidePrimary();
			ShowHideRentedWeekly();
			return false;
		}
		
		function OpenLookupWindow(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Arg1,Arg2,Arg3,Arg4)
			{
					
				var win = window.open(Url + '?DataValueField=' + DataValueField + '&DataTextField=' + 
									DataTextField + '&DataValueFieldID=' + DataValueFieldID + 
									'&DataTextFieldID=' + DataTextFieldID + '&LookupCode=' + LookupCode + 
									'&Arg1=' + Arg1 + '&Arg2=' + Arg2 + '&Arg3=' + Arg3 + '&Arg4=' + Arg4,
									'review','height=500, width=500,status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no' );
				
			}
		
		function ChkTextAreaLength(source, arguments)
		{
			var txtArea = arguments.Value;
			if(txtArea.length > 500 ) 
			{
				arguments.IsValid = false;
				return;   // invalid userName
			}
		}
		function ShowHidePrimary()
		{ 
			if (document.getElementById('hidCalledFrom').value == "Rental")
		    {
		    
				document.getElementById('cmbIS_PRIMARY').style.display = "none";
				document.getElementById('capIS_PRIMARY').style.display = "none";
				document.getElementById('lblWARNING').style.display = "none";
				document.getElementById('cmbIS_PRIMARY').selectedIndex = -1;
				
				document.getElementById('hidGenerallocation').value = '0'
					
				var SelIndex = document.getElementById('cmbLOCATION_TYPE').options.selectedIndex;
				var SelVal;
				if(SelIndex >-1)
				{
					SelVal = document.getElementById('cmbLOCATION_TYPE').options[SelIndex].value;
					if(SelVal == "11849")//If Seasonal/Rental
					{
						document.getElementById('trRENTED_WEEKLY').style.display='inline';												
						ShowHideRentedWeekly();
					}
					else
					{
						//document.getElementById('cmbRENTED_WEEKLY').options.selectedIndex=0;
						//document.getElementById('txtWEEKS_RENTED').value="";
						//document.getElementById('trWEEKS_RENTED').style.display='none';
						document.getElementById('tdWeeksRentedTextBox').style.display='none';
						document.getElementById('tdWeeksRentedLabel').style.display='none';         
						document.getElementById('trRENTED_WEEKLY').style.display='none';						
					}
				}
				ShowHideRentedWeekly();
		    }
		    else
		    {
					//Addef FOR Reset Button 
					document.getElementById('hidGenerallocation').value = '0'
					
					//document.getElementById('trWEEKS_RENTED').style.display='none';
					document.getElementById('tdWeeksRentedTextBox').style.display='none';
					document.getElementById('tdWeeksRentedLabel').style.display='none';
					document.getElementById('trRENTED_WEEKLY').style.display='none';
						
					var locType;
					var index ;
					var polType=document.POL_LOCATIONS.hidPOLICY_TYPE.value; 
					
					if(document.getElementById('cmbLOCATION_TYPE').options.selectedIndex >0)
					{
						index  = document.getElementById('cmbLOCATION_TYPE').options.selectedIndex;
						locType= document.getElementById('cmbLOCATION_TYPE').options[index].value;
						//if((locType=="11814" || locType=="11813")&& (polType == "11402" || polType == "11401" || polType =="11410"  || polType == "11149" )) //Seasonal and Policy type HO-5 Premier/Replacement
						if((locType=="11814" || locType=="11813")&& (polType == "11401" || polType =="11410"  || polType == "11149" )) //Seasonal and Policy type HO-5 Premier/Replacement
						{
								document.getElementById('cmbIS_PRIMARY').style.display = "none";
								document.getElementById('capIS_PRIMARY').style.display = "none";
								//document.getElementById('lblWARNING').style.display = "inline";
								alert('Seasonal /Secondary location types are not eligible for HO-5');
								document.getElementById('cmbLOCATION_TYPE').selectedIndex = -1;
								return;
						}
						if(locType=="11813" || locType=="11814")//Sessional or Secondary
						{
								document.getElementById('cmbIS_PRIMARY').style.display = "inline";
								document.getElementById('capIS_PRIMARY').style.display = "inline";
								document.getElementById('lblWARNING').style.display = "none";
								return;
						}
					  
					}
		
					document.getElementById('cmbIS_PRIMARY').style.display = "none";
					document.getElementById('capIS_PRIMARY').style.display = "none";
					document.getElementById('lblWARNING').style.display = "none";
					document.getElementById('cmbIS_PRIMARY').selectedIndex = -1;
				}//else
		}
		

		
		///////////////////////////////////////////////AJAX CALLS FOR ZIP/////////////////////////////////////
		function GetZipForState(flag)
		{		    
			GlobalError=true;
			if(document.getElementById('cmbLOC_STATE').value==14 ||document.getElementById('cmbLOC_STATE').value==22)
			{ 
			 if(document.getElementById('txtLOC_ZIP').value!="")
			 {
				//if(isNaN(document.getElementById('txtLOC_ZIP').value))
				//	return;
				var intStateID = document.getElementById('cmbLOC_STATE').options[document.getElementById('cmbLOC_STATE').options.selectedIndex].value;
				var strZipID = document.getElementById('txtLOC_ZIP').value;	
				var result = PolicyAddLocation.AjaxFetchZipForState(intStateID,strZipID);
				AjaxCallFunction_CallBack(result);
				
				if(GlobalError)
				{
					document.getElementById('csvLOC_ZIP').setAttribute('enabled',true);
					document.getElementById('csvLOC_ZIP').setAttribute('isValid',true);
					document.getElementById('csvLOC_ZIP').style.display = 'inline';			
					return false;
				}
				else
				{
					document.getElementById('csvLOC_ZIP').setAttribute('enabled',false); 
			   		document.getElementById('csvLOC_ZIP').setAttribute('isValid',false);
					document.getElementById('csvLOC_ZIP').style.display = 'none';
			
					//if(window.event.srcElement.id == "btnSave")
					//	document.forms[0].submit();
						
					return true;
				}			
			
			 }	
			 return false;	
			}
			else 
				return true;		
		}
		function AjaxCallFunction_CallBack(response)
		{		
			if(document.getElementById('cmbLOC_STATE').value==14 ||document.getElementById('cmbLOC_STATE').value==22)
			{ 
				if(document.getElementById('txtLOC_ZIP').value!="")
				{
					handleResults(response);
					if(GlobalError)
					{
						return false;
					}
					else
					{
						return true;
					}
				}	
				return false;
			}
			else 
				return true;		
		}
		/////EMP ZIP AJAX////////////////
		function handleResults(res) 
		{
		if(!res.error)
			{
			if (res.value!="" && res.value!=null ) 
				{
					GlobalError=false;
				}
				else
				{
					GlobalError=true;
				}
			}
			else
			{
				GlobalError=true;		
			}
		}
		function handleResult(res) 
		{
			if(!res.error)
			{
				//if (res.value==true) 
				if (res.value!="") 
				{		
					if(!CalledFromSave)
						document.getElementById("txtLOC_COUNTY").value= res.value;
					GlobalError=false;
				}
				else
				{
					GlobalError=true;
				}
			}
			else
			{
				GlobalError=true;		
			}
		}

		function ChkResult(objSource , objArgs)
		{
			if(GlobalError==true)
			{
				Page_IsValid = false;
				objArgs.IsValid = false;
			}
			else
			{
				objArgs.IsValid = true;				
			}
			//setInterval("GetCounty()", 1000);
			//document.getElementById("btnSave").click();
		}
		/*function GetZip(Result)
		{	
			if(Result.error)
			{ 
				var xfaultcode   = Result.errorDetail.code;
				var xfaultstring = Result.errorDetail.string;
				var xfaultsoap   = Result.errorDetail.raw; 
			}			
			else		
			{	
				if(Result.value==true) // Zip belongs to the specified state
				{
			   		document.getElementById('csvLOC_ZIP').setAttribute('enabled',false); 
			   		document.getElementById('csvLOC_ZIP').setAttribute('isValid',false);
					document.getElementById('csvLOC_ZIP').style.display = 'none';
					GlobalError = false;
				
				}
				else
				{
					document.getElementById('csvLOC_ZIP').setAttribute('enabled',true);
					document.getElementById('csvLOC_ZIP').setAttribute('isValid',true);
					document.getElementById('csvLOC_ZIP').style.display = 'inline';									
					GlobalError = true;
			
				}								
			}
		
		}		
		function ChkResult(objSource , objArgs)
		{
		    
			if(GlobalError==true)
			{
				Page_IsValid = false;
				objArgs.IsValid = false;
			}
			else
			{
				objArgs.IsValid = true;
			}			
		}*/
//		function ProcessKeypress() 
//			{
//				if (event.keyCode == 13) 
//				{       
//					//__doPostBack('ibtnAdd','click');
//					
//					GetZipForState();
//				}  
//			   
//		    }		    
		    	
		</script>
	</HEAD>
	<BODY oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();ChangeColor();" >
		<FORM id="POL_LOCATIONS" method="post" runat="server">
		<P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>
			
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr>
						<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
					</tr>
					<TBODY>
						<TR id="trBody" runat="server">
							<TD>
								<TABLE width="100%" align="center" border="0">
									<TBODY>
										<tr>
											<TD class="pageHeader" colSpan="4"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow>Please 
												note that all fields marked with * are mandatory
											</TD>
										</tr>
										<tr>
											<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label><asp:label id="lblWARNING" style="DISPLAY: none" CssClass="errmsg" Runat="server"></asp:label></td>
										</tr>
										<tr>
											<TD class="midcolora" width="18%"><asp:label id="capLOC_NUM" runat="server">Num</asp:label><span class="mandatory">*</span></TD>
											<TD class="midcolora" width="32%"><asp:textbox id="txtLOC_NUM" runat="server" size="14" maxlength="10"></asp:textbox><BR>
												<asp:requiredfieldvalidator id="rfvLOC_NUM" runat="server" ControlToValidate="txtLOC_NUM" ErrorMessage="LOC_NUM can't be blank."
													Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revLOC_NUM" runat="server" ControlToValidate="txtLOC_NUM" ErrorMessage="RegularExpressionValidator"
													Display="Dynamic"></asp:regularexpressionvalidator><asp:rangevalidator id="rngLOC_NUM" Runat="server" ControlToValidate="txtLOC_NUM" Display="Dynamic"
													Type="Integer" MinimumValue="0" MaximumValue="2147483647"></asp:rangevalidator></TD>
											<TD class="midcolora" width="18%"><asp:label id="capDESCRIPTION" runat="server">Description</asp:label></TD>
											<TD class="midcolora" width="32%"><asp:textbox onkeypress="MaxLength(this,500);" id="txtDESCRIPTION" Runat="server" MaxLength="500"
													Height="50" Width="200" TextMode="MultiLine"></asp:textbox><br>
												<asp:customvalidator id="csvDESCRIPTION" Runat="server" ControlToValidate="txtDESCRIPTION" Display="Dynamic"
													ClientValidationFunction="ChkTextAreaLength"></asp:customvalidator></TD>
										</tr>
										<tr>
											<td class="midcolora" width="18%"><asp:label id="capLOCATION_TYPE" Runat="server"></asp:label><span class="mandatory">*</span></td>
											<td class="midcolora" width="32%"><asp:dropdownlist id="cmbLOCATION_TYPE" Runat="server"></asp:dropdownlist><BR>
												<asp:RequiredFieldValidator id="rfvLOCATION_TYPE" runat="server" Display="Dynamic" ErrorMessage="Please Select Location Is."
													ControlToValidate="cmbLOCATION_TYPE"></asp:RequiredFieldValidator></td>
											<TD class="midcolora" width="18%"><asp:label id="capIS_PRIMARY" runat="server">Is Primary Location</asp:label></TD>
											<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbIS_PRIMARY" onfocus="SelectComboIndex('cmbIS_PRIMARY')" runat="server">
													<asp:ListItem></asp:ListItem>
													<asp:ListItem Value='Y'>Yes</asp:ListItem>
													<asp:ListItem Value='N'>No</asp:ListItem>
												</asp:dropdownlist><BR>
											</TD>
										</tr>
										<tr id="trRENTED_WEEKLY">
											<TD class="midcolora" width="18%">Is location rented on a Weekly Basis?</TD>
											<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbRENTED_WEEKLY" onfocus="SelectComboIndex('cmbRENTED_WEEKLY')" runat="server">
													<asp:ListItem></asp:ListItem>
													<asp:ListItem Value='N'>No</asp:ListItem>
													<asp:ListItem Value='Y'>Yes</asp:ListItem>
												</asp:dropdownlist></TD>
											<td class="midcolora" width="18%">
												<div id="tdWeeksRentedLabel"># of weeks rented per year
												</div>
											</td>
											<td class="midcolora" width="32%">
												<div id="tdWeeksRentedTextBox"><asp:textbox id="txtWEEKS_RENTED" runat="server" size="4" maxlength="2"></asp:textbox><asp:comparevalidator id="cpvWEEKS_RENTED" Runat="server" ControlToValidate="txtWEEKS_RENTED" ErrorMessage="Please enter numeric value and greater than Zero."
														Type="Integer" ValueToCompare="0" Operator="GreaterThan"></asp:comparevalidator></div>
											</td>
										</tr>
										<tr>
											<td class="midcolora"><asp:label id="Label1" runat="server">Would you like to pull customer address</asp:label></td>
											<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnPullCustomerAddress" runat="server" Text="Pull Customer Address"></cmsb:cmsbutton></td>
										</tr>
										<tr>
											<TD class="midcolora" width="18%"><asp:label id="capLOC_ADD1" runat="server">Address1</asp:label><span class="mandatory">*</span></TD>
											<TD class="midcolora" width="32%"><asp:textbox id="txtLOC_ADD1" runat="server" size="35" maxlength="70"></asp:textbox><BR>
												<asp:requiredfieldvalidator id="rfvLOC_ADD1" runat="server" ControlToValidate="txtLOC_ADD1" ErrorMessage="LOC_ADD1 can't be blank."
													Display="Dynamic"></asp:requiredfieldvalidator></TD>
											<TD class="midcolora" width="18%"><asp:label id="capLOC_ADD2" runat="server">Address2</asp:label></TD>
											<TD class="midcolora" width="32%"><asp:textbox id="txtLOC_ADD2" runat="server" size="35" maxlength="70"></asp:textbox></TD>
										</tr>
										<tr>
											<TD class="midcolora" width="18%"><asp:label id="capLOC_CITY" runat="server">City</asp:label><span class="mandatory">*</span></TD>
											<TD class="midcolora" width="32%"><asp:textbox id="txtLOC_CITY" runat="server" size="35" maxlength="40"></asp:textbox><BR>
												<asp:requiredfieldvalidator id="rfvLOC_CITY" Runat="server" ControlToValidate="txtLOC_CITY" ErrorMessage="LOC_CITY can't be blank."
													Display="Dynamic"></asp:requiredfieldvalidator></TD>
											<TD class="midcolora" width="18%"><asp:label id="capLOC_COUNTRY" runat="server">Country</asp:label></TD>
											<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOC_COUNTRY" onfocus="SelectComboIndex('cmbLOC_COUNTRY')" runat="server"></asp:dropdownlist></TD>
										</tr>
										<tr>
											<TD class="midcolora" width="18%"><asp:label id="capLOC_STATE" runat="server">State</asp:label></TD>
											<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOC_STATE" onfocus="SelectComboIndex('cmbLOC_STATE')" runat="server" Enabled ="false">
													<asp:ListItem Value='0'></asp:ListItem>
												</asp:dropdownlist><BR>
												<asp:requiredfieldvalidator id="rfvLOC_STATE" runat="server" ControlToValidate="cmbLOC_STATE" ErrorMessage="LOC_STATE can't be blank."
													Display="Dynamic" Enabled ="false"></asp:requiredfieldvalidator></TD>
											<TD class="midcolora" width="18%"><asp:label id="capLOC_ZIP" runat="server">Postal Code</asp:label><span class="mandatory">*</span></TD>
											<TD class="midcolora" width="32%"><asp:textbox id="txtLOC_ZIP" runat="server" size="12"
													maxlength="6"></asp:textbox>
													<%-- Added by Swarup on 30-mar-2007 --%>
												<asp:hyperlink id="hlkZipLookup" runat="server" CssClass="HotSpot" Visible ="false">
												<asp:image id="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom" Visible ="false"></asp:image>
												</asp:hyperlink><BR>
												<asp:requiredfieldvalidator id="rfvLOC_ZIP" runat="server" ControlToValidate="txtLOC_ZIP" ErrorMessage="LOC_ZIP can't be blank."
													Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revLOC_ZIP" runat="server" ControlToValidate="txtLOC_ZIP" ErrorMessage="RegularExpressionValidator"
													Display="Dynamic"></asp:regularexpressionvalidator>
                                                    <asp:customvalidator id="csvLOC_ZIP" Runat="server" ControlToValidate="txtLOC_ZIP" ErrorMessage="Zip does not belong to the specifed state."
													Display="Dynamic" ClientValidationFunction="ChkResult" Enabled ="false"></asp:customvalidator></TD>
										</tr>
										<tr>
											<TD class="midcolora" width="18%"><asp:label id="capLOC_COUNTY" runat="server">County</asp:label></TD>
											<TD class="midcolora" width="32%"><asp:textbox id="txtLOC_COUNTY" runat="server" size="15" maxlength="30" ReadOnly="true"></asp:textbox><!-- ReadOnly attribute added by Charles on 7-Sep-09 for Itrack 6296 --><IMG id="imgSelect" style="CURSOR: hand" alt="" src="~/cmsweb/images/selecticon.gif"
													runat="server"><BR>
											</TD>
											<!--The following controls Deductible & Named Peril have been commented as they will not be used here-->
											<!--<TD class="midcolora" width="18%">--><%--<asp:label id="capDEDUCTIBLE" runat="server">Deductible</asp:label>--%><!--<span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%">--><%--<asp:DropDownList ID="cmbDEDUCTIBLE" Runat="server">
												<asp:ListItem Value="500">500</asp:ListItem>
												<asp:ListItem Value="750">750</asp:ListItem>
												<asp:ListItem Value="1000">1000</asp:ListItem>
												<asp:ListItem Value="2500">2500</asp:ListItem>
											</asp:DropDownList>--%>
											<!--</TD>-->
											<td class="midcolora" colSpan="2"></td>
										</tr>
										<!--<tr>
										<TD class="midcolora" width="18%">--><%--<asp:label id="capNAMED_PERILL" runat="server">Perill</asp:label>--%><!--</TD>
										<TD class="midcolora" width="32%">--><%--<asp:dropdownlist id="cmbNAMED_PERILL" onfocus="SelectComboIndex('cmbNAMED_PERILL')" runat="server">
												<asp:ListItem></asp:ListItem>
												<asp:ListItem Value="0">Peril1</asp:ListItem>
												<asp:ListItem Value="1">Peril2</asp:ListItem>
												<asp:ListItem Value="3">Peril3</asp:ListItem>
											</asp:dropdownlist>--%>
										<!--<a class="calcolora" href="javascript:showPageLookupLayer('cmbNAMED_PERILL')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0"></a>
											<BR>
										</TD>
										<td class="midcolora" colSpan="2"></td>
									</tr>-->
										<tr>
											<TD class="headerEffectSystemParams" colSpan="4">Order Loss Report</TD>
											<TD class="headerEffectSystemParams"></TD>
										</tr>
										<tr>
											<TD class="midcolora" width="18%"><asp:label id="capLOSSREPORT_ORDER" runat="server">Property Loss Report Ordered</asp:label></TD>
											<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOSSREPORT_ORDER" onfocus="SelectComboIndex('cmbLOSSREPORT_ORDER')" runat="server"></asp:dropdownlist></TD>
											<TD class="midcolora" width="18%"><asp:label id="capLOSSREPORT_DATETIME" runat="server">Date Ordered</asp:label></TD>
											<TD class="midcolora" width="32%"><asp:textbox id="txtLOSSREPORT_DATETIME" runat="server" size="11" maxlength="10"></asp:textbox><asp:hyperlink id="hlkCalandarDate2" runat="server" CssClass="HotSpot">
											<ASP:IMAGE id="imgCalenderExp2" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
											</asp:hyperlink><br>
											<asp:regularexpressionvalidator id="revLOSSREPORT_DATETIME" runat="server" ControlToValidate="txtLOSSREPORT_DATETIME" ErrorMessage="Please check format of date."
											Display="Dynamic"></asp:regularexpressionvalidator></TD>
										</tr>
										<tr>
											
											<TD class="midcolora" width="18%"><asp:label id="capREPORT_STATUS" runat="server">Report Status</asp:label></TD>
											<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREPORT_STATUS" onfocus="SelectComboIndex('cmbREPORT_STATUS')" runat="server" Height="26px"></asp:dropdownlist></TD>
											<TD class="midcolora" colspan="2"> </TD>
										</tr>
										<tr>
											<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" CausesValidation="False"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"></cmsb:cmsbutton></td>
											<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" CausesValidation="False"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
										</tr>
									</TBODY>
								</TABLE>
							</TD>
						</TR>
					</TBODY>
			</TABLE>
			<input id="hidPOLICY_TYPE" type="hidden" name="hidPOLICY_TYPE" runat="server"> <INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidLOCATION_ID" type="hidden" value="0" name="hidLOCATION_ID" runat="server">
			<INPUT id="hidCalledFrom" type="hidden" value="0" name="hidCalledFrom" runat="server">
			<INPUT id="hidPOL_ID" type="hidden" name="hidPOL_ID" runat="server"> <INPUT id="hidPOL_VERSION_ID" type="hidden" name="hidPOL_VERSION_ID" runat="server">
			<INPUT id="hidCUSTOMER_ID" type="hidden" name="hidCUSTOMER_ID" runat="server"> <input id="hidLocationCode" type="hidden" name="hidLocationCode" runat="server">
			<input id="hidSubmitZip" type="hidden" runat="server" NAME="hidSubmitZip">
			<!--Added By Raghav on 07/17/2008--->
			<input id="hidGenerallocation" type="hidden" name="hidGenerallocation" runat="server">
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none; Z-INDEX: 101; POSITION: absolute"><iframe id="lookupLayerIFrame" style="DISPLAY: none; Z-INDEX: 1001; FILTER: alpha(opacity=0); BACKGROUND-COLOR: #000000"
				width="0" height="0" left="0px" top="0px;"></iframe>
		</div>
		<DIV id="lookupLayer" onmouseover="javascript:refreshLookupLayer();" style="Z-INDEX: 101; VISIBILITY: hidden; POSITION: absolute">
			<table class="TabRow" cellSpacing="0" cellPadding="2" width="100%">
				<tr class="SubTabRow">
					<td><b>Add LookUp</b></td>
					<td>
						<p align="right"><A onclick="javascript:hideLookupLayer();" href="javascript:void(0)"><IMG height="14" alt="Close" src="/cms/cmsweb/images/cross.gif" width="16" border="0"></A></p>
					</td>
				</tr>
				<tr class="SubTabRow">
					<td colSpan="2">
						<span id="LookUpMsg"></span>
					</td>
				</tr>
			</table>
		</DIV>
		<!-- For lookup layer ends here-->
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,"<%=primaryKeyValues%>", true);			
		</script>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</BODY>
</HTML>
