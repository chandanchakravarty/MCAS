<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page validateRequest="false" CodeBehind="PolicyRatingInfo.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Umbrella.PolicyRatingInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
		var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		function ResetForm()
		{
				DisableValidators();
				ChangeColor();
				document.POL_UMB_RATING_INFO.reset();
				Initialize();
				return false;			
		}
		
	    function disableListItems(checkBoxListId, checkBoxIndex, numOfItems)
		{
			 objCtrl = document.getElementById(checkBoxListId);
			if(objCtrl == null)
			{
				return;
			}
		    var i = 0;
			var objItem = null;
			// Get the checkbox to verify.
			var objItemChecked = 
			document.getElementById(checkBoxListId + '_' + checkBoxIndex);
			// Does the individual checkbox exist?
		    if(objItemChecked == null)
			{
				return;
			}

			// Is the checkbox to verify checked?
			 var isChecked = objItemChecked.checked;
    
			 // Loop through the checkboxes in the list.
			for(i = 0; i < numOfItems; i++)
			{
				objItem = document.getElementById(checkBoxListId + '_' + i);

				if(objItem == null)
				{
					continue;
				}
		        // If i does not equal the checkbox that is never to be disabled.
				if(i != checkBoxIndex)
				{
					// Disable/Enable the checkbox.
					objItem.disabled = isChecked;
					// Should the checkbox be disabled?
					if(isChecked)
					{
						// Uncheck the checkbox.
						objItem.checked = false;
					}
				}
			}
		}

		function ChkOccurenceDate(objSource , objArgs)
		{
			var effdateObj=eval('document.POL_UMB_RATING_INFO.' + objSource.getAttribute('ControlTovalidate'));
			var effdate=effdateObj.value;
			var date='<%=DateTime.Now.Year%>';		
		
			//if(effdate.length<4)
			if(effdate<1900)
			{
				objArgs.IsValid = false;
			}
			else
			{
				if(effdate > date)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			}		
		}
			
	
		function Initialize()
		{
				
			    if(document.getElementById('hiddyear').value!="0")
			    {
			    document.getElementById('spnWIRING_RENOVATION').style.display="inline";
			    document.getElementById('spnPLUMBING_RENOVATION').style.display="inline";
			    document.getElementById('spnHEATING_RENOVATION').style.display="inline";
			    document.getElementById('spnROOFING_RENOVATION').style.display="inline";
			     }
			    else
			    {
			    document.getElementById('spnWIRING_RENOVATION').style.display="none";
			    document.getElementById('spnPLUMBING_RENOVATION').style.display="none";
			    document.getElementById('spnHEATING_RENOVATION').style.display="none";
			    document.getElementById('spnROOFING_RENOVATION').style.display="none";
			    }
				if ( document.POL_UMB_RATING_INFO.hidOldData.value == '')
				{
					
				}
				else
				{					
					if (document.getElementById('cmbIS_UNDER_CONSTRUCTION').selectedIndex != '2')
					{
						document.getElementById('txtDWELLING_CONST_DATE').value  = ''; 
					}
				}
				
				setDropdowns();
				DisplayDate();
				
				cmbEXTERIOR_CONSTRUCTION_OnChange();
				combo_OnChange('cmbROOF_TYPE','txtROOF_OTHER_DESC','lblROOF_OTHER_DESC');
				combo_OnChange('cmbPRIMARY_HEAT_TYPE','txtPRIMARY_HEAT_OTHER_DESC','lblPR_HEAT_OTHER_DESC');
				combo_OnChange('cmbSECONDARY_HEAT_TYPE','txtSECONDARY_HEAT_OTHER_DESC','lblSC_HEAT_OTHER_DESC');
				disableListItems('cblBurgFire', '0', '3');
				disableListItems('cblDIRECT', '0', '3');
				ApplyColor();
				ChangeColor();
				
		}
				
		function showPageLookupLayer(controlId)
		{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbFOUNDATION":
						lookupMessage	=	"FNDCD.";
						break;
					case "cmbEXTERIOR_CONSTRUCTION":
						lookupMessage	=	"CONTYP.";
						break;
					case "cmbPRIMARY_HEAT_TYPE":
						lookupMessage	=	"PHEAT.";
						break;
					case "cmbSECONDARY_HEAT_TYPE":
						lookupMessage	=	"PHEAT.";
						break;
					case "cmbROOF_TYPE":
						lookupMessage	=	"RFTYP.";
						break;
					case "cmbSWIMMING_POOL_TYPE":
						lookupMessage	=	"SPLCD.";
						break;
					default:
						lookupMessage	=	"Look up code not found";
						break;
						
				}
				showLookupLayer(controlId,lookupMessage);							
		}
			
		function cmbEXTERIOR_CONSTRUCTION_OnChange()
		{
					var combo = document.getElementById('cmbEXTERIOR_CONSTRUCTION');
					var index = combo.selectedIndex;
					var selectedText;
					
					
					if ( index > -1)
					{
						selectedText = combo.options[index].text;
					}
					
					
					//lblDESC
					
					if ( selectedText == 'undefined' || selectedText == "Other")
					{
						document.getElementById('lblDESC').style.display = "none";
						document.getElementById('txtEXTERIOR_OTHER_DESC').style.display = "inline";
					    document.getElementById("rfvEXTERIOR_OTHER_DESC").setAttribute('enabled',true);
						document.getElementById("rfvEXTERIOR_OTHER_DESC").setAttribute('isValid',true);
						if (document.getElementById("spnEXTERIOR_OTHER_DESC") != null)
                		{
	             	     document.getElementById("spnEXTERIOR_OTHER_DESC").style.display = "inline";
	            	     }
						
					}
					else			
					{
						
						document.getElementById('lblDESC').style.display = "inline";						
						document.getElementById('txtEXTERIOR_OTHER_DESC').style.display = "none";
						document.getElementById("rfvEXTERIOR_OTHER_DESC").setAttribute('isValid',false);
						document.getElementById("rfvEXTERIOR_OTHER_DESC").style.display='none';
						document.getElementById("rfvEXTERIOR_OTHER_DESC").setAttribute('enabled',false);
					    if (document.getElementById("spnEXTERIOR_OTHER_DESC") != null)
			            {
				          document.getElementById("spnEXTERIOR_OTHER_DESC").style.display = "none";
		               	}
					}
					
		}
				
		function setColor(comboBox,txtDesc)
		{
				if(document.getElementById(txtDesc).style.display=="none")
					return;
					
				document.getElementById(txtDesc).focus();
				document.getElementById(comboBox).focus();
		}
																		                                
		function combo_OnChange(comboBox,txtDesc,lblNA)
		{
				
					var combo = document.getElementById(comboBox);
					var index = combo.selectedIndex;
					var selectedText;
				
					if ( index > -1)
					{
						selectedText = combo.options[index].text;
						
					}
					
					if ( selectedText == 'undefined' || selectedText == "Other")
					{
					
						document.getElementById(lblNA).style.display = "none";
						document.getElementById(txtDesc).style.display = "inline";
						document.getElementById("spn" + txtDesc.substring(3)).style.display = "inline";
						document.getElementById("rfv" + txtDesc.substring(3)).setAttribute("enabled",true);
						document.getElementById("rfv" + txtDesc.substring(3)).setAttribute("isValid",true);
						
					}
					else
					{
						document.getElementById(lblNA).style.display = "inline";
						document.getElementById(txtDesc).style.display = "none";
						document.getElementById("spn" + txtDesc.substring(3)).style.display = "none";
						document.getElementById("rfv" + txtDesc.substring(3)).setAttribute("enabled",false);
						document.getElementById("rfv" + txtDesc.substring(3)).setAttribute("isValid",false);
						document.getElementById("rfv" + txtDesc.substring(3)).style.display = "none";
					}
					
		}
		function setDropdowns()
		{			
				if(document.getElementById('cmbWIRING_RENOVATION').selectedIndex == 0 || document.getElementById('cmbWIRING_RENOVATION').selectedIndex == 2)
				{
					document.getElementById('txtWIRING_UPDATE_YEAR').style.display="none";
					document.getElementById('txtWIRING_UPDATE_YEAR').value="";
					document.getElementById('spnWIRING_UPDATE_YEAR').style.display="none";
					document.getElementById('spnWIRING_UPDATE_YEARNA').style.display="inline";
					document.getElementById('rfvWIRING_UPDATE_YEAR').setAttribute("enabled",false);
					document.getElementById('rfvWIRING_UPDATE_YEAR').style.display="none";
				}
				else
				{
					document.getElementById('txtWIRING_UPDATE_YEAR').style.display="inline";
					document.getElementById('spnWIRING_UPDATE_YEAR').style.display="inline";
					document.getElementById('spnWIRING_UPDATE_YEARNA').style.display="none";
					document.getElementById('rfvWIRING_UPDATE_YEAR').setAttribute("enabled",true);				
					
				}
				
				if(document.getElementById('cmbPLUMBING_RENOVATION').selectedIndex ==0 || document.getElementById('cmbPLUMBING_RENOVATION').selectedIndex == 2)
				{
					document.getElementById('txtPLUMBING_UPDATE_YEAR').style.display="none";
					document.getElementById('txtPLUMBING_UPDATE_YEAR').value="";
					document.getElementById('spnPLUMBING_UPDATE_YEAR').style.display="none";
					document.getElementById('spnPLUMBING_UPDATE_YEARNA').style.display="inline";
					document.getElementById('rfvPLUMBING_UPDATE_YEAR').setAttribute("enabled",false);
					document.getElementById('rfvPLUMBING_UPDATE_YEAR').style.display="none";
				}
				else
				{
					document.getElementById('txtPLUMBING_UPDATE_YEAR').style.display="inline";
					document.getElementById('spnPLUMBING_UPDATE_YEAR').style.display="inline";
					document.getElementById('spnPLUMBING_UPDATE_YEARNA').style.display="none";
					document.getElementById('rfvPLUMBING_UPDATE_YEAR').setAttribute("enabled",true);					
					
				}				
				if(document.getElementById('cmbHEATING_RENOVATION').selectedIndex == 0 || document.getElementById('cmbHEATING_RENOVATION').selectedIndex == 2)
				{					
					document.getElementById('txtHEATING_UPDATE_YEAR').style.display="none";
					document.getElementById('spnHEATING_UPDATE_YEAR').style.display="none";
					document.getElementById('spnHEATING_UPDATE_YEARNA').style.display="inline";
					document.getElementById('rfvHEATING_UPDATE_YEAR').setAttribute("enabled",false);
					document.getElementById('rfvHEATING_UPDATE_YEAR').style.display="none";
				}
				else
				{					
					document.getElementById('txtHEATING_UPDATE_YEAR').style.display="inline";
					document.getElementById('spnHEATING_UPDATE_YEAR').style.display="inline";
					document.getElementById('spnHEATING_UPDATE_YEARNA').style.display="none";
					document.getElementById('rfvHEATING_UPDATE_YEAR').setAttribute("enabled",true);					
					
				}				
				if(document.getElementById('cmbROOFING_RENOVATION').selectedIndex  == 0 || document.getElementById('cmbROOFING_RENOVATION').selectedIndex==2 )
				{
					document.getElementById('txtROOFING_UPDATE_YEAR').style.display="none";
					document.getElementById('txtROOFING_UPDATE_YEAR').value="";
					document.getElementById('spnROOFING_UPDATE_YEAR').style.display="none";
					document.getElementById('spnROOFING_UPDATE_YEARNA').style.display="inline";
					document.getElementById('rfvROOFING_UPDATE_YEAR').setAttribute("enabled",false);
					document.getElementById('rfvROOFING_UPDATE_YEAR').style.display="none";
				}
				else
				{
					document.getElementById('txtROOFING_UPDATE_YEAR').style.display="inline";
					document.getElementById('spnROOFING_UPDATE_YEAR').style.display="inline";
					document.getElementById('spnROOFING_UPDATE_YEARNA').style.display="none";
					document.getElementById('rfvROOFING_UPDATE_YEAR').setAttribute("enabled",true);				
					
				}
				
				ApplyColor();
				ChangeColor();
		}
							
		function ChkDwellingStartDate(objSource , objArgs)
		{
		if (document.getElementById("revDWELLING_CONST_DATE").isvalid == true)
			{		
				var effdate=document.POL_UMB_RATING_INFO.txtDWELLING_CONST_DATE.value;
				objArgs.IsValid = DateComparer("<%=DateTime.Now%>", effdate, jsaAppDtFormat);
			}
			else
			{
				objArgs.IsValid = true;	
				
			}
		}
		
	function DisplayDate()
	{ 
		 if(document.getElementById('cmbIS_UNDER_CONSTRUCTION').selectedIndex=='2')  
		 {
			document.getElementById('lblDWELLING_CONST_DATE').style.display ='none';
			document.getElementById('txtDWELLING_CONST_DATE').style.display ='inline'; 
			document.getElementById("revDWELLING_CONST_DATE").setAttribute('enabled',true); 
			document.getElementById("revDWELLING_CONST_DATE").setAttribute('isValid',true); 
			document.getElementById("csvDWELLING_CONST_DATE").setAttribute('enabled',true); 
			document.getElementById("csvDWELLING_CONST_DATE").setAttribute('isValid',true);
			document.getElementById('hlkDWELLING_CONST_DATE').style.display='inline';
		} 
		else
		{		
			document.getElementById('lblDWELLING_CONST_DATE').style.display ='inline';	
			document.getElementById('txtDWELLING_CONST_DATE').style.display ='none';
			document.getElementById('hlkDWELLING_CONST_DATE').style.display='none';			
			document.getElementById("revDWELLING_CONST_DATE").setAttribute('isValid',false); 
			document.getElementById("revDWELLING_CONST_DATE").style.display='none'; 
			document.getElementById("revDWELLING_CONST_DATE").setAttribute('enabled',false); 
			document.getElementById("csvDWELLING_CONST_DATE").setAttribute('isValid',false); 
			document.getElementById("csvDWELLING_CONST_DATE").style.display='none'; 
			document.getElementById("csvDWELLING_CONST_DATE").setAttribute('enabled',false); 
		} 
	}
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="Initialize();ApplyColor();ChangeColor();" MS_POSITIONING="GridLayout">
		<form id="POL_UMB_RATING_INFO" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow>Please 
									note that all fields marked with * are mandatory
								</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capHYDRANT_DIST" runat="server">Distance to the fire hydrant</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbHYDRANT_DIST" onfocus="SelectComboIndex('cmbHYDRANT_DIST')" runat="server"></asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvHYDRANT_DIST" Runat="server" ControlToValidate="cmbHYDRANT_DIST" Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capFIRE_STATION_DIST" runat="server">Distance to the fire station</asp:label><span class="mandatory">*</span>
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtFIRE_STATION_DIST" runat="server" maxlength="4" size="4"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvFIRE_STATION_DIST" Runat="server" ControlToValidate="txtFIRE_STATION_DIST"
										Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revFIRE_STATION_DIST" Runat="server" ControlToValidate="txtFIRE_STATION_DIST"
										Display="Dynamic"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capIS_UNDER_CONSTRUCTION" runat="server">Is the dwelling under construction?</asp:label><span class="mandatory">*</span>
								</TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbIS_UNDER_CONSTRUCTION" onfocus="SelectComboIndex('cmbIS_UNDER_CONSTRUCTION')"
										runat="server">
										<asp:ListItem Value=''></asp:ListItem>
										<asp:ListItem Value='0'>No</asp:ListItem>
										<asp:ListItem Value='1'>Yes</asp:ListItem>
									</asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvIS_UNDER_CONSTRUCTION" runat="server" ControlToValidate="cmbIS_UNDER_CONSTRUCTION"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capDWELLING_CONST_DATE" runat="server">Dwellind start date</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtDWELLING_CONST_DATE" runat="server" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkDWELLING_CONST_DATE" runat="server" CssClass="HotSpot">
										<ASP:IMAGE id="imgDWELLING_CONST_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
									</asp:hyperlink><asp:label id="lblDWELLING_CONST_DATE" runat="server" CssClass="LabelFont">-N.A.-</asp:label><BR>
									<asp:regularexpressionvalidator id="revDWELLING_CONST_DATE" runat="server" ControlToValidate="txtDWELLING_CONST_DATE"
										Display="Dynamic"></asp:regularexpressionvalidator><asp:customvalidator id="csvDWELLING_CONST_DATE" Runat="server" ControlToValidate="txtDWELLING_CONST_DATE"
										Display="Dynamic" ClientValidationFunction="ChkDwellingStartDate"></asp:customvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capPROT_CLASS" runat="server">Protection class</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtPROT_CLASS" runat="server" maxlength="2" size="2"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvPROT_CLASS" Runat="server" ControlToValidate="txtPROT_CLASS" Display="Dynamic"></asp:requiredfieldvalidator><asp:rangevalidator id="rngPROT_CLASS" runat="server" ControlToValidate="txtPROT_CLASS" Display="Dynamic"
										MaximumValue="10" MinimumValue="1" Type="Integer"></asp:rangevalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capIS_AUTO_POL_WITH_CARRIER" runat="server">Does Wolverine Mutual write your Auto policy? </asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbIS_AUTO_POL_WITH_CARRIER" onfocus="SelectComboIndex('cmbIS_AUTO_POL_WITH_CARRIER')"
										runat="server">
										<asp:ListItem Value=''></asp:ListItem>
										<asp:ListItem Value='0'>No</asp:ListItem>
										<asp:ListItem Value='1'>Yes</asp:ListItem>
									</asp:dropdownlist></TD>
							</tr>
							<tr>
								<TD class="headerEffectSystemParams" width="18%" colSpan="4">Dwelling Update 
									Information</TD>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capWIRING_RENOVATION" runat="server">Wiring renovation</asp:label><span class="mandatory" id="spnWIRING_RENOVATION">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbWIRING_RENOVATION" onfocus="SelectComboIndex('cmbWIRING_RENOVATION')" runat="server"
										onchange="JavaScript:setDropdowns();"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvWIRING_RENOVATION" Runat="server" ControlToValidate="cmbWIRING_RENOVATION"
										Display="Dynamic" ErrorMessage="Please Select the wiring renovation"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="123"><asp:label id="capWIRING_UPDATE_YEAR" runat="server">Wiring update year</asp:label><span class="mandatory" id="spnWIRING_UPDATE_YEAR">*</span>
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtWIRING_UPDATE_YEAR" runat="server" maxlength="4" size="4"></asp:textbox><span class="labelfont" id="spnWIRING_UPDATE_YEARNA">N.A.</span>
									<BR>
									<asp:requiredfieldvalidator id="rfvWIRING_UPDATE_YEAR" runat="server" ControlToValidate="txtWIRING_UPDATE_YEAR"
										Display="Dynamic"></asp:requiredfieldvalidator><asp:customvalidator id="csvpWIRING_UPDATE_YEAR" runat="server" ControlToValidate="txtWIRING_UPDATE_YEAR"
										Display="Dynamic" ClientValidationFunction="ChkOccurenceDate" ErrorMessage="Update Year must lie between 1900 and current year."></asp:customvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capPLUMBING_RENOVATION" runat="server">Plumbing renovation</asp:label><span class="mandatory" id="spnPLUMBING_RENOVATION">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPLUMBING_RENOVATION" onfocus="SelectComboIndex('cmbPLUMBING_RENOVATION')"
										runat="server" onchange="JavaScript:setDropdowns();"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvPLUMBING_RENOVATION" Runat="server" ControlToValidate="cmbPLUMBING_RENOVATION"
										Display="Dynamic" ErrorMessage="Please select Plumbing renovation"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" style="WIDTH: 123px" width="123"><asp:label id="capPLUMBING_UPDATE_YEAR" runat="server">Plumbing update year</asp:label><span class="mandatory" id="spnPLUMBING_UPDATE_YEAR">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtPLUMBING_UPDATE_YEAR" runat="server" maxlength="4" size="4"></asp:textbox><span class="labelfont" id="spnPLUMBING_UPDATE_YEARNA">N.A.</span><br>
									<asp:customvalidator id="csvPLUMBING_UPDATE_YEAR" runat="server" ControlToValidate="txtPLUMBING_UPDATE_YEAR"
										Display="Dynamic" ClientValidationFunction="ChkOccurenceDate" ErrorMessage="Update Year must lie between 1900 and current year."></asp:customvalidator><BR>
									<asp:requiredfieldvalidator id="rfvPLUMBING_UPDATE_YEAR" runat="server" ControlToValidate="txtPLUMBING_UPDATE_YEAR"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capHEATING_RENOVATION" runat="server">Heating renovation</asp:label><span class="mandatory" id="spnHEATING_RENOVATION">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbHEATING_RENOVATION" onfocus="SelectComboIndex('cmbHEATING_RENOVATION')" runat="server"
										onchange="JavaScript:setDropdowns();"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvHEATING_RENOVATION" Runat="server" ControlToValidate="cmbHEATING_RENOVATION"
										Display="Dynamic" ErrorMessage="Please select Heating renovation "></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" style="WIDTH: 123px" width="123"><asp:label id="capHEATING_UPDATE_YEAR" runat="server">Heating update year</asp:label><span class="mandatory" id="spnHEATING_UPDATE_YEAR">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtHEATING_UPDATE_YEAR" runat="server" maxlength="4" size="4"></asp:textbox><span class="labelfont" id="spnHEATING_UPDATE_YEARNA">N.A.</span><br>
									<asp:customvalidator id="csvHEATING_UPDATE_YEAR" runat="server" ControlToValidate="txtHEATING_UPDATE_YEAR"
										Display="Dynamic" ClientValidationFunction="ChkOccurenceDate" ErrorMessage="Update Year must lie between 1900 and current year."></asp:customvalidator><BR>
									<asp:requiredfieldvalidator id="rfvHEATING_UPDATE_YEAR" runat="server" ControlToValidate="txtHEATING_UPDATE_YEAR"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capROOFING_RENOVATION" runat="server">Roofing renovation</asp:label><span class="mandatory" id="spnROOFING_RENOVATION">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbROOFING_RENOVATION" onfocus="SelectComboIndex('cmbROOFING_RENOVATION')" runat="server"
										onchange="JavaScript:setDropdowns();"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvROOFING_RENOVATION" Runat="server" ControlToValidate="cmbROOFING_RENOVATION"
										Display="Dynamic" ErrorMessage="Please select Roofing renovation"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" style="WIDTH: 123px" width="123"><asp:label id="capROOFING_UPDATE_YEAR" runat="server">Roofing update year</asp:label><span class="mandatory" id="spnROOFING_UPDATE_YEAR">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtROOFING_UPDATE_YEAR" runat="server" maxlength="4" size="4"></asp:textbox><span class="labelfont" id="spnROOFING_UPDATE_YEARNA">N.A.</span><br>
									<asp:customvalidator id="csvROOFING_UPDATE_YEAR" runat="server" ControlToValidate="txtROOFING_UPDATE_YEAR"
										Display="Dynamic" ClientValidationFunction="ChkOccurenceDate" ErrorMessage="Update Year must lie between 1900 and current year."
										EnableViewState="False"></asp:customvalidator><BR>
									<asp:requiredfieldvalidator id="rfvROOFING_UPDATE_YEAR" runat="server" ControlToValidate="txtROOFING_UPDATE_YEAR"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capNO_OF_AMPS" runat="server">Number of Amps (Elec Sys)</asp:label><span class="mandatory">*</span>
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtNO_OF_AMPS" runat="server" maxlength="4" size="4"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvNO_OF_AMPS" Runat="server" ControlToValidate="txtNO_OF_AMPS" Display="Dynamic"></asp:requiredfieldvalidator><asp:rangevalidator id="rngNO_OF_AMPS" Runat="server" ControlToValidate="txtNO_OF_AMPS" Display="Dynamic"
										MaximumValue="9999" MinimumValue="1" Type="Integer"></asp:rangevalidator></TD>
								<TD class="midcolora" style="WIDTH: 123px" width="123"><asp:label id="capCIRCUIT_BREAKERS" runat="server">Circuit Breakers</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCIRCUIT_BREAKERS" onfocus="SelectComboIndex('cmbCIRCUIT_BREAKERS')" runat="server"></asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvCIRCUIT_BREAKERS" runat="server" ControlToValidate="cmbCIRCUIT_BREAKERS"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="headerEffectSystemParams" width="18%" colSpan="4">Construction 
									Information</TD>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capNO_OF_FAMILIES" runat="server">Number of Families</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtNO_OF_FAMILIES" runat="server" maxlength="3" size="3"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revNO_OF_FAMILIES" runat="server" ControlToValidate="txtNO_OF_FAMILIES" Display="Dynamic"
										Enabled="False"></asp:regularexpressionvalidator><asp:rangevalidator id="rngNO_OF_FAMILIES" runat="server" ControlToValidate="txtNO_OF_FAMILIES" Display="Dynamic"
										MaximumValue="2" MinimumValue="0" Type="Integer"></asp:rangevalidator></TD>
								<TD class="midcolora" colSpan="2"></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCONSTRUCTION_CODE" runat="server">Construction Code</asp:label></TD>
								<TD class="midcolora" colSpan="3"><asp:dropdownlist id="cmbCONSTRUCTION_CODE" runat="server"></asp:dropdownlist></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capEXTERIOR_CONSTRUCTION" runat="server">Exterior construction</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbEXTERIOR_CONSTRUCTION" onfocus="SelectComboIndex('cmbEXTERIOR_CONSTRUCTION')"
										runat="server" OnChange="javascript:cmbEXTERIOR_CONSTRUCTION_OnChange();"></asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbEXTERIOR_CONSTRUCTION')"></A><br>
									<asp:requiredfieldvalidator id="rfvEXTERIOR_CONSTRUCTION" Runat="server" ControlToValidate="cmbEXTERIOR_CONSTRUCTION"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capEXTERIOR_OTHER_DESC" runat="server">Other Description</asp:label><span class="mandatory" id="spnEXTERIOR_OTHER_DESC">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtEXTERIOR_OTHER_DESC" runat="server" maxlength="50" size="30"></asp:textbox><asp:label id="lblDESC" runat="server" CssClass="LabelFont">N.A</asp:label><br>
									<asp:requiredfieldvalidator id="rfvEXTERIOR_OTHER_DESC" runat="server" ControlToValidate="txtEXTERIOR_OTHER_DESC"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capFOUNDATION" runat="server">Foundation</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbFOUNDATION" onfocus="SelectComboIndex('cmbFOUNDATION')" runat="server">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbFOUNDATION')"></A></TD>
								<TD class="midcolora" width="18%"></TD>
								<TD class="midcolora" width="32%"></TD>
							</tr>
							<tr>
								<td class="midcolora" width="18%"><asp:label id="capROOF_TYPE" runat="server">Roof Type</asp:label></td>
								<td class="midcolora" width="32%"><asp:dropdownlist id="cmbROOF_TYPE" onfocus="SelectComboIndex('cmbROOF_TYPE')" runat="server" OnChange="javascript:combo_OnChange('cmbROOF_TYPE','txtROOF_OTHER_DESC','lblROOF_OTHER_DESC');setColor('cmbROOF_TYPE','txtROOF_OTHER_DESC');">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbROOF_TYPE')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
								</td>
								<td class="midcolora" width="18%"><asp:label id="capROOF_TYPE_OTHER_DESC" runat="server">Other Description</asp:label><span class="mandatory" id="spnROOF_OTHER_DESC">*</span></td>
								<td class="midcolora" width="32%"><asp:textbox id="txtROOF_OTHER_DESC" runat="server" maxlength="50" size="30"></asp:textbox><asp:label id="lblROOF_OTHER_DESC" runat="server" CssClass="LabelFont">N.A</asp:label><br>
									<asp:requiredfieldvalidator id="rfvROOF_OTHER_DESC" runat="server" ControlToValidate="txtROOF_OTHER_DESC" Display="Dynamic"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<td class="midcolora" width="18%"><asp:label id="capPRIMARY_HEAT_TYPE" runat="server">Primary Heat Type</asp:label></td>
								<td class="midcolora" width="32%"><asp:dropdownlist id="cmbPRIMARY_HEAT_TYPE" onfocus="SelectComboIndex('cmbPRIMARY_HEAT_TYPE')" runat="server"
										OnChange="javascript:combo_OnChange('cmbPRIMARY_HEAT_TYPE','txtPRIMARY_HEAT_OTHER_DESC','lblPR_HEAT_OTHER_DESC'); setColor('cmbPRIMARY_HEAT_TYPE','txtPRIMARY_HEAT_OTHER_DESC');">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbPRIMARY_HEAT_TYPE')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
								</td>
								<td class="midcolora" width="18%"><asp:label id="capROOF_OTHER_DESC" runat="server">Other Description</asp:label><span class="mandatory" id="spnPRIMARY_HEAT_OTHER_DESC">*</span></td>
								<td class="midcolora" width="32%"><asp:textbox id="txtPRIMARY_HEAT_OTHER_DESC" runat="server" maxlength="30" size="30"></asp:textbox><asp:label id="lblPR_HEAT_OTHER_DESC" runat="server" CssClass="LabelFont">N.A</asp:label><br>
									<asp:requiredfieldvalidator id="rfvPRIMARY_HEAT_OTHER_DESC" runat="server" ControlToValidate="txtPRIMARY_HEAT_OTHER_DESC"
										Display="Dynamic"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capSECONDARY_HEAT_TYPE" runat="server">Secondary Heat</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSECONDARY_HEAT_TYPE" onfocus="SelectComboIndex('cmbSECONDARY_HEAT_TYPE')"
										runat="server" OnChange="javascript:combo_OnChange('cmbSECONDARY_HEAT_TYPE','txtSECONDARY_HEAT_OTHER_DESC','lblSC_HEAT_OTHER_DESC'); setColor('cmbSECONDARY_HEAT_TYPE','txtSECONDARY_HEAT_OTHER_DESC');">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbSECONDARY_HEAT_TYPE')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
								</TD>
								<td class="midcolora" width="18%"><asp:label id="capSECONDARY_HEAT_OTHER_DESC" runat="server">Other Description</asp:label><span class="mandatory" id="spnSECONDARY_HEAT_OTHER_DESC">*</span></td>
								<td class="midcolora" width="32%"><asp:textbox id="txtSECONDARY_HEAT_OTHER_DESC" runat="server" maxlength="30" size="30"></asp:textbox><asp:label id="lblSC_HEAT_OTHER_DESC" runat="server" CssClass="LabelFont">N.A</asp:label><br>
									<asp:requiredfieldvalidator id="rfvSECONDARY_HEAT_OTHER_DESC" runat="server" ControlToValidate="txtSECONDARY_HEAT_OTHER_DESC"
										Display="Dynamic"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<TD class="headerEffectSystemParams" width="18%" colSpan="4">Protective Devices 
									Information</TD>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE width="100%" align="center" border="0">
				<TR id="trProtectiveDevices" runat="server">
					<td class="midcolora" width="18%">Protective devices</td>
					<TD class="midcolora" width="32%"><asp:checkboxlist id="cblBurgFire" runat="server" CssClass="midcolora"></asp:checkboxlist></TD>
					<TD class="midcolora" width="18%"><asp:checkboxlist id="cblDIRECT" runat="server" CssClass="midcolora"></asp:checkboxlist></TD>
					<td class="midcolora" width="32%"><asp:checkboxlist id="cblLOCAL" runat="server" CssClass="midcolora"></asp:checkboxlist></td>
				</TR>
				<%--	<tr id="trArmsSupplies" runat="server">
					<td class="midcolora" width="18%"><asp:label id="lblNUM_LOC_ALARMS_APPLIES" runat="server"></asp:label></td>
					<td class="midcolora" width="32%"><asp:textbox id="txtNUM_LOC_ALARMS_APPLIES" runat="server" size="5" maxlength="4"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revNUM_LOC_ALARMS_APPLIES" Display="Dynamic" ControlToValidate="txtNUM_LOC_ALARMS_APPLIES"
							Enabled="False" Runat="server"></asp:regularexpressionvalidator>
						<asp:rangevalidator id="rngNUM_LOC_ALARMS_APPLIES" Display="Dynamic" ControlToValidate="txtNUM_LOC_ALARMS_APPLIES"
							Runat="server" Type="Integer" MinimumValue="1" MaximumValue="9999"></asp:rangevalidator>
					</td>
					<td colspan="2" class="midcolora">&nbsp;</td>
				</tr>--%>
				<TR>
					<TD class="midcolora" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></TD>
					<TD class="midcolorr" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></TD>
				</TR>
				<tr>
					<td><INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
						<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
						<INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidAPP_ID" runat="server">
						<INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidAPP_VERSION_ID" runat="server">
						<INPUT id="hidDWELLING_ID" type="hidden" value="0" name="hidDWELLING_ID" runat="server">
						<INPUT id="hidRowId" type="hidden" value="0" name="hidRowId" runat="server"> <INPUT id="hidDefaultTerr" type="hidden" value="0" name="hidDefaultTerr" runat="server">
						<INPUT id="hiddyear" type="hidden" value="0" name="dyear" runat="server"> <INPUT id="hidStateID" type="hidden" value="0" name="hidStateID" runat="server">
						<INPUT id="hidLOCATION_ID" type="hidden" value="0" name="hidLOCATION_ID" runat="server">
					</td>
				</tr>
			</TABLE>
		</form>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none; Z-INDEX: 101; POSITION: absolute"><iframe id="lookupLayerIFrame" style="DISPLAY: none; Z-INDEX: 1001; FILTER: alpha(opacity=0); BACKGROUND-COLOR: #000000"
				width="0" height="0" top="0px;" left="0px"></iframe>
		</div>
		<DIV id="lookupLayer" onmouseover="javascript:refreshLookupLayer();" style="Z-INDEX: 102; VISIBILITY: hidden; POSITION: absolute">
			<table class="TabRow" cellSpacing="0" cellPadding="2" width="100%">
				<tr class="SubTabRow">
					<td><b>Add LookUp</b></td>
					<td>
						<p align="right"><A onclick="javascript:hideLookupLayer();" href="javascript:void(0)"><IMG height="14" alt="Close" src="/cms/cmsweb/images/cross.gif" width="16" border="0"></A></p>
					</td>
				</tr>
				<tr class="SubTabRow">
					<td colSpan="2"><span id="LookUpMsg"></span></td>
				</tr>
			</table>
		</DIV>
		<!-- For lookup layer ends here-->
	</body>
</HTML>


