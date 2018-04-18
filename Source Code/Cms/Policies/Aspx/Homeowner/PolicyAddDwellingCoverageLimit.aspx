<%@ Page language="c#" Codebehind="PolicyAddDwellingCoverageLimit.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.aspx.HomeOwners.PolicyAddDwellingCoverageLimit" validateRequest=false %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>POL_DWELLING_COVERAGE</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		
		function Reset()
		{
			populateXML();
			DisableValidators();
			return false;
		}
		
	function AddData()
	{
		if ('<%=strCalledFrom%>'=='Rental' || '<%=strCalledFrom%>'=='RENTAL')
		{
			document.getElementById('cmbDWELLING_REPLACE_COST').options.selectedIndex = 2;
			document.getElementById('cmbREPLACEMENT_COST_CONTS').options.selectedIndex = 2;
		}
		else
		{
			document.getElementById('cmbDWELLING_REPLACE_COST').options.selectedIndex = -1;
			document.getElementById('cmbREPLACEMENT_COST_CONTS').options.selectedIndex = -1;
		}
		
	document.getElementById('txtDWELLING_LIMIT').value  = '';
	//document.getElementById('txtDWELLING_PREMIUM').value  = '';
	//document.getElementById('cmbDWELLING_REPLACE_COST').options.selectedIndex = -1;
	document.getElementById('txtOTHER_STRU_LIMIT').value  = '';
	document.getElementById('txtOTHER_STRU_DESC').value  = '';
	document.getElementById('txtPERSONAL_PROP_LIMIT').value  = '';
	//document.getElementById('cmbREPLACEMENT_COST_CONTS').options.selectedIndex = -1;
	document.getElementById('txtLOSS_OF_USE').value  = '';
	//document.getElementById('txtLOSS_OF_USE_PREMIUM').value  = '';
	//document.getElementById('cmbPERSONAL_LIAB_LIMIT').options.selectedIndex = -1;
	//document.getElementById('txtPERSONAL_LIAB_PREMIUM').value  = '';
	document.getElementById('cmbMED_PAY_EACH_PERSON').options.selectedIndex = 0;
	//document.getElementById('txtMED_PAY_EACH_PERSON_PREMIUM').value  = '';
	//document.getElementById('txtINFLATION_GUARD').value  = '';
	document.getElementById('cmbALL_PERILL_DEDUCTIBLE_AMT').options.selectedIndex = 1;
	//document.getElementById('txtWIND_HAIL_DEDUCTIBLE_AMT').value  = '';
	document.getElementById('txtTHEFT_DEDUCTIBLE_AMT').value  = '';
	//document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
	
	SetDefault();
	SetHidden();
	ApplyColor();	
	ChangeColor();
	DisableValidators();
	}
	
	//Set default value
	function SetDefault()
	{
		var policyType = document.getElementById('hidPolcyType').value;
		var ReplValue = document.getElementById('hidReplValue').value;
		var DefaultValue = document.getElementById('hidDefaultValue').value;
		
		//New record (for rental dwelling)
			if ( document.getElementById('hidOldData').value == "0" || document.getElementById('hidOldData').value == "" )
				{
				if ('<%=strCalledFrom%>'=='Rental' || '<%=strCalledFrom%>'=='RENTAL')
				{
					var CoverageATextBox = document.getElementById('txtDWELLING_LIMIT');
					CoverageATextBox.value = ReplValue;
					CalculateLimits('A',CoverageATextBox);
					
					
					document.getElementById('cmbDWELLING_REPLACE_COST').options.selectedIndex = 2;
					document.getElementById('cmbREPLACEMENT_COST_CONTS').options.selectedIndex = 2;
					
				}
				else
				{//For Home
						
					//Repair cost, no default yes
					if ( policyType == "HO-3 Repair Cost" || policyType == "HO-2 Repair Cost")
					{
						var CoverageATextBox = document.getElementById('txtDWELLING_LIMIT');
						//CoverageATextBox.value = ReplValue;
						CoverageATextBox.value = DefaultValue;
						CalculateLimits('A',CoverageATextBox);
					}		
					else if(policyType == "HO-4 Deluxe" || policyType == "HO-4" || policyType == "HO-6" || policyType == "HO-6 Deluxe")
					{
						//C is the base coverage
						var CoverageCTextBox = document.getElementById('txtPERSONAL_PROP_LIMIT');
						//CoverageCTextBox.value = ReplValue;
						CoverageCTextBox.value = DefaultValue;
						CalculateLimits('C',CoverageCTextBox);
						
						//Default yes
						document.getElementById('cmbDWELLING_REPLACE_COST').options.selectedIndex = 1;
						
					}	
					else
					{
						//All other , A is base coverage, default yes
						var CoverageATextBox = document.getElementById('txtDWELLING_LIMIT');
						//CoverageATextBox.value = ReplValue;
						CoverageATextBox.value = DefaultValue;
						CalculateLimits('A',CoverageATextBox);
						
						document.getElementById('cmbDWELLING_REPLACE_COST').options.selectedIndex = 1;
						resetTextBox();
					}
				}
			}
	}
	
	function ChkNocoverage()
	{
		var liability =document.getElementById('cmbPERSONAL_LIAB_LIMIT').value ;
		var medpay =document.getElementById('cmbMED_PAY_EACH_PERSON').value ;
		
		if ('<%=strCalledFrom%>'=='Rental' || '<%=strCalledFrom%>'=='RENTAL')
		{
			if(liability == "-1" && medpay != "-1")
			{
				document.getElementById('cmbMED_PAY_EACH_PERSON').selectedIndex=0;
				return false;
	
			}
			if(liability != "-1" && medpay == "-1")
			{
				document.getElementById('cmbMED_PAY_EACH_PERSON').selectedIndex=1;
				return false;
			}
			
			//Validate the page
			Page_ClientValidate();
			
			//alert(Page_IsValid);
			
			if ( Page_IsValid == true )
			{
				return true ;
			}
			else
			{
				return false ;
			}
				
		}
		
		
					
	}



function SetHidden()
{
	//Set value n hidden field
	hidDwellReplaceCost = document.getElementById('hidDwellReplaceCost')	
	
	if(document.getElementById('cmbDWELLING_REPLACE_COST') != null )
	{
		hidDwellReplaceCost.value = document.getElementById('cmbDWELLING_REPLACE_COST').value;
		//alert(hidDwellReplaceCost.value);
	}
}

//On Change of Replacement cost dropdown
//For HO-5 disabled
//if yes c = 70% of a	
//if no c = 50% of a	
function resetTextBox()
{
	
		
	
	//For 4, 4D, 6 and 6 D return
	var policyType = document.getElementById('hidPolcyType').value;
	
	if (policyType == "HO-4 Deluxe" || policyType == "HO-4" || 
		policyType == "HO-6" || policyType == "HO-6 Deluxe")
	{
		return;
	}
	
	var txtValue = 0;
	
	//Coverage A
	var txtCoverageLimitValue;
	
	if(document.getElementById('txtDWELLING_LIMIT')!=null)
	{
		//Coverage A value
		txtCoverageLimitValue=ReplaceAll( document.getElementById('txtDWELLING_LIMIT').value,',','');

        if(document.getElementById('txtPERSONAL_PROP_LIMIT') != null )
		{
					//txtValue =roundNumber(parseFloat(parseInt(ReplaceAll(txtCoverageLimitValue,',',''))*.70),8);
					
			if ( ! ('<%=strCalledFrom%>'=='Rental' || '<%=strCalledFrom%>'=='RENTAL') )
			{
				//Yes					
				if(document.getElementById('cmbDWELLING_REPLACE_COST').value == 1)
				{
					//70% of A
					txtValue =  getFixedSimple(txtCoverageLimitValue *.70,2,1);
				}				
				else
				{	
					//No				
					/*
					if(document.getElementById('hidPolcyType').value == "HO-5 Premier")
						txtValue =  getFixedSimple(txtCoverageLimitValue *.70,2,1);
					else if(document.getElementById('hidPolcyType').value=="HO-3 Premier")
						txtValue =  getFixedSimple(txtCoverageLimitValue *.50,2,1);
					else*/
					//50% of A
					txtValue =  getFixedSimple(txtCoverageLimitValue *.50,2,1);
				}
				
				//Coverage C								
				document.getElementById('txtPERSONAL_PROP_LIMIT').value =formatCurrency(txtValue);				
			}
         }
      }
}

function populateXML()
{

		if(document.getElementById('hidFormSaved')!=null &&( document.getElementById('hidFormSaved').value == '0'||document.getElementById('hidFormSaved').value == '1'))
			{
				var tempXML='';
				tempXML=document.getElementById('hidOldData').value;	
				var policyType = document.getElementById('hidPolcyType').value;	
				
		
				if(tempXML!="" && tempXML!=0)
				    {				
						populateFormData(tempXML,POL_DWELLING_COVERAGE);
                      //In Case Of Repair Cost The Expanded Replacement Building Coverage Is Disbaled and with 'No' By Default
					   if ( policyType == "HO-3 Repair Cost" || policyType == "HO-2 Repair Cost")
					   {
					 	
					     document.getElementById('cmbREPLACEMENT_COST_CONTS').options.selectedIndex = 2;
					   }
						SetHidden();
						//DisableValidators();
						
						if ('<%=strCalledFrom%>'=='Rental' || '<%=strCalledFrom%>'=='RENTAL')
						{	
							//Disable and set to no: //Disable Repl cost contents and Expanded Bldg repl cost drop downs
							document.getElementById('cmbDWELLING_REPLACE_COST').options.selectedIndex = 2;
							document.getElementById('cmbREPLACEMENT_COST_CONTS').options.selectedIndex = 2;
									
						}
					}
				else
					{
						AddData();
					}
					//alert(tempXML)
				}
				
		document.getElementById('txtDWELLING_LIMIT').value=formatCurrency(document.getElementById('txtDWELLING_LIMIT').value);
		//document.getElementById('txtDWELLING_PREMIUM').value=formatCurrency(document.getElementById('txtDWELLING_PREMIUM').value);
		document.getElementById('txtOTHER_STRU_LIMIT').value=formatCurrency(document.getElementById('txtOTHER_STRU_LIMIT').value);
		document.getElementById('txtPERSONAL_PROP_LIMIT').value=formatCurrency(document.getElementById('txtPERSONAL_PROP_LIMIT').value);
		
		document.getElementById('txtTHEFT_DEDUCTIBLE_AMT').value=formatCurrency(document.getElementById('txtTHEFT_DEDUCTIBLE_AMT').value);
		
		document.getElementById('txtLOSS_OF_USE').value=formatCurrency(document.getElementById('txtLOSS_OF_USE').value);			
			return false;
}

	function ChkCoverageLimitValue(source, arguments)
		{
			var txtCoverageLimitValue = arguments.Value;
			if(txtCoverageLimitValue < 10000 ) 
			{
				arguments.IsValid = false;
				return;   // invalid userName
			}
			 
			
		}
		
		
	 function CalculateLimits(covType, txtBox)
	 {
		
		if ( covType == 'D' || covType == 'B') return;
			
		var policyType = document.getElementById('hidPolcyType').value;
		
		//alert(covType);
		//alert(policyType);
		
		//HO-6 & HO-6 Deluxe :
		//Coverage A & Covg. D should default on basis of Coverage C.
		//Coverage A should be 10% of Covg. C 
		//Covg. D should be 40% of Covg. C
		if ( policyType == 'HO-6' || policyType == 'HO-6 Deluxe') 
		{
			if ( covType == 'C')
			{
			
				var coverageCAmount = ReplaceAll( txtBox.value,',','');
				
				//Coverage C is empty
				if ( coverageCAmount == '' )
				{
					document.getElementById('txtDWELLING_LIMIT').value = '';
					document.getElementById('txtLOSS_OF_USE').value = '';
					return;
				}
			
				if(isNaN(coverageCAmount)==false)
				{
					var coverageA = getFixedSimple(coverageCAmount * .10 , 2 , 1);
					var coverageD = getFixedSimple(coverageCAmount * .40 , 2 , 1);		
					
					//For HO-6 if calculated amount < 2000,  then default 2000
					if ( policyType == 'HO-6' )
					{
						if ( coverageA < 2000 )
						{
							coverageA = 2000;
						}
					}
					
					document.getElementById('txtDWELLING_LIMIT').value = coverageA;
					document.getElementById('txtLOSS_OF_USE').value = coverageD;
					
					document.getElementById('txtDWELLING_LIMIT').value=formatCurrency(document.getElementById('txtDWELLING_LIMIT').value);
					document.getElementById('txtLOSS_OF_USE').value=formatCurrency(document.getElementById('txtLOSS_OF_USE').value);
				}
				
				return;
			}
			
			return;	
		}
		
		
		
		//HO-4 & HO-4 Deluxe 
		//Covg. D should default on basis of Coverage C 
		//Covg. D should be 40% of Covg. C 
		if ( policyType == 'HO-4' || policyType == 'HO-4 Deluxe') 
		{
			if ( covType == 'C')
			{
				var coverageCAmount = ReplaceAll( txtBox.value,',','');
				
				//Coverage C is empty
				if ( coverageCAmount == '' )
				{
					document.getElementById('txtLOSS_OF_USE').value = '';
					return;
				}
				
				if(isNaN(coverageCAmount)==false)
				{
					//var coverageA = getFixedSimple(txtCoverageLimitValue * .10 , 2 , 1);
					var coverageD = getFixedSimple(coverageCAmount * .40 , 2 , 1);		
					
					//document.getElementById('txtDWELLING_LIMIT').value = coverageA;
					document.getElementById('txtLOSS_OF_USE').value = coverageD;
					document.getElementById('txtLOSS_OF_USE').value=formatCurrency(document.getElementById('txtLOSS_OF_USE').value);
					
				}		
			}
			return;
		}
		
		var txtCoverageLimitValue;
		 		 
		if(document.getElementById('txtDWELLING_LIMIT')!=null)
		{
			txtCoverageLimitValue = ReplaceAll( document.getElementById('txtDWELLING_LIMIT').value,',','');
			 
			//Coverage A is empty
			if ( txtCoverageLimitValue == '' )
			{
				document.getElementById('txtOTHER_STRU_LIMIT').value = '';
				document.getElementById('txtPERSONAL_PROP_LIMIT').value = '';
				document.getElementById('txtLOSS_OF_USE').value = '';
				return;
			}
			
			if(isNaN(txtCoverageLimitValue)==false )
			{	
				var txtValue;
				
				if(document.getElementById('txtOTHER_STRU_LIMIT') != null )
				{ 
					if('<%=strCalledFrom%>'=='Rental' || '<%=strCalledFrom%>'=='RENTAL')
						txtValue = getFixedSimple(txtCoverageLimitValue *.10,2,1);
					else
					{						
						txtValue = getFixedSimple(txtCoverageLimitValue *.10,2,1);
					}
					document.getElementById('txtOTHER_STRU_LIMIT').value =  formatCurrency(txtValue) ;	
					document.getElementById('txtOTHER_STRU_LIMIT').value=formatCurrency(document.getElementById('txtOTHER_STRU_LIMIT').value);			
				}
				
				if(document.getElementById('txtPERSONAL_PROP_LIMIT') != null )
				{
					//txtValue =roundNumber(parseFloat(parseInt(ReplaceAll(txtCoverageLimitValue,',',''))*.70),8);
					if('<%=strCalledFrom%>'=='Rental' || '<%=strCalledFrom%>'=='RENTAL')
					{
						/*txtValue =  getFixedSimple(txtCoverageLimitValue *.10,2,1);
						if(txtValue>10000)
							txtValue=10000;*/
						if(document.getElementById('hidStateId').value==22 && document.getElementById('hidPolcyType').value=="DP-3 Premier")						
						{
							txtValue =  getFixedSimple(txtCoverageLimitValue *.10,2,1);
							if(txtValue>10000)
								txtValue=10000;
						}
						else
						{
							txtValue =  getFixedSimple(txtCoverageLimitValue *.05,2,1);
							if(txtValue>5000)
								txtValue=5000;
						}
					}
					else
					{
					    if(document.getElementById('cmbDWELLING_REPLACE_COST').value==1)
					    {							
							txtValue =  getFixedSimple(txtCoverageLimitValue *.70,2,1);
						}
						else
						{
						//txtValue =  getFixedSimple(txtCoverageLimitValue *.30,2,1);						
							if(document.getElementById('hidPolcyType').value=="HO-5 Premier")
								txtValue =  getFixedSimple(txtCoverageLimitValue *.70,2,1);
							else if(document.getElementById('hidPolcyType').value=="HO-3 Premier")
								txtValue =  getFixedSimple(txtCoverageLimitValue *.50,2,1);
							else
								txtValue =  getFixedSimple(txtCoverageLimitValue *.50,2,1);
							
						}
						
					}	
					document.getElementById('txtPERSONAL_PROP_LIMIT').value =formatCurrency(txtValue);				
				}
				
				if(document.getElementById('txtLOSS_OF_USE') != null )
				{
					//txtValue = roundNumber(parseFloat(parseInt(ReplaceAll(txtCoverageLimitValue,',',''))*.30),4);
					if('<%=strCalledFrom%>'=='Rental' || '<%=strCalledFrom%>'=='RENTAL')
						txtValue =  getFixedSimple(txtCoverageLimitValue *.10,2,1);
					else
						txtValue =  getFixedSimple(txtCoverageLimitValue *.30,2,1);
					document.getElementById('txtLOSS_OF_USE').value = formatCurrency(txtValue) ;				
				}
				
				//Calculate on basis of repl cost
				
				
			}
		}
	}
	
	function roundNumber(numToRound,placesToRound) 
	{
		var numberField = numToRound; // Field where the number appears
		var rlength = placesToRound; // The number of decimal places to round to		
		var newnumber = Math.round(numberField*Math.pow(10,rlength))/Math.pow(10,rlength);
		
		return newnumber;
	}
	function getFixedSimple(val, places, isRounded)
	{
		var factor;
		var i;
		factor = 1;
		for (i=0; i<places; i++)
		{	factor *= 10; }
		val = Math.floor((val * factor) + (isRounded ? 0.50000000001 : 0.00000000001));
		val = val / factor;
		return val;
	}
	function showPageLookupLayer(controlId)
			{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbFORM":
						lookupMessage	=	"CFORM.";
						break;
					case "cmbCOVERAGE":
						lookupMessage	=	"HOMEOT.";
						break;						
					default:
						lookupMessage	=	"Look up code not found";
						break;
						
				}
				showLookupLayer(controlId,lookupMessage);							
			}

function CheckRules()
{
	var PolicyType = document.getElementById('hidPolcyType').value;
	var stateId = document.getElementById('hidStateId').value;
	
	//var	txtCoverageLimitValue=ReplaceAll( document.getElementById('txtDWELLING_LIMIT').value,',','');
	if(stateId=='14')
	{
		if(PolicyType=="Ho-4" || PolicyType=="Ho-6" || PolicyType=="Ho-4 Deluxe" || PolicyType=="Ho-6 Deluxe")
			{
			
			//alert();
				if(parseInt(txtCoverageLimitValue)<125000)
				{
				//alert('Value is less');
				document.getElementById('txtDWELLING_LIMIT').focus();
				return false;
				
				}
			}
			}
}
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();">
		<FORM id="POL_DWELLING_COVERAGE" method="post" runat="server">
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
								<td class="midcolorc" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="25%"><asp:label id="capALL_PERILL_DEDUCTIBLE_AMT" runat="server">All Peril Deductible</asp:label></TD>
								<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbALL_PERILL_DEDUCTIBLE_AMT" runat="server">
										<asp:ListItem Value=""></asp:ListItem>
										<asp:ListItem Value="500">500</asp:ListItem>
										<asp:ListItem Value="750">750</asp:ListItem>
										<asp:ListItem Value="1000">1000</asp:ListItem>
										<asp:ListItem Value="2500">2500</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD class="midcolora" width="25%"><asp:label id="capDWELLING_LIMIT" runat="server">Dwelling Limits</asp:label><span class="mandatory" id="spnmand" runat="server">*</span>
								</TD>
								<TD class="midcolora" width="25%"><asp:textbox id="txtDWELLING_LIMIT" onblur="CalculateLimits();" runat="server" CssClass="INPUTCURRENCY"
										maxlength="9" size="17"></asp:textbox><asp:label id="capCOVERAGEA" runat="server" Visible="False">N.A.</asp:label><BR>
									<asp:requiredfieldvalidator id="rfvDWELLING_LIMIT" runat="server" Display="Dynamic" ControlToValidate="txtDWELLING_LIMIT"
										Enabled="True"></asp:requiredfieldvalidator><asp:rangevalidator id="rngDWELLING_LIMIT" runat="server" Type="Currency" Display="Dynamic" ControlToValidate="txtDWELLING_LIMIT"
										ErrorMessage="Coverage A"></asp:rangevalidator><asp:regularexpressionvalidator id="revDWELLING_LIMIT" Runat="server" Display="Dynamic" ControlToValidate="txtDWELLING_LIMIT"
										Enabled="False"></asp:regularexpressionvalidator><%--<asp:customvalidator id="csvDWELLING_LIMIT" ControlToValidate="txtDWELLING_LIMIT" Display="Dynamic" Runat="server"
										ClientValidationFunction="ChkCoverageLimitValue"></asp:customvalidator>--%></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="25%"><asp:label id="capDWELLING_REPLACE_COST" runat="server">Replacement Cost Dwelling</asp:label></TD>
								<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbDWELLING_REPLACE_COST" onfocus="SelectComboIndex('cmbDWELLING_REPLACE_COST')"
										runat="server">
										<asp:ListItem Value=""></asp:ListItem>
										<asp:ListItem Value='1'>Yes</asp:ListItem>
										<asp:ListItem Value='0'>No</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD class="midcolora" width="25%"><asp:label id="capOTHER_STRU_LIMIT" runat="server">Other Structures Limit</asp:label></TD>
								<TD class="midcolora" width="25%"><asp:textbox id="txtOTHER_STRU_LIMIT" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="17"
										ReadOnly="True"></asp:textbox><asp:label id="capCOVERAGEB" runat="server" Visible="False">N.A.</asp:label><BR>
									<asp:regularexpressionvalidator id="revOTHER_STRU_LIMIT" Runat="server" Display="Dynamic" ControlToValidate="txtOTHER_STRU_LIMIT"
										Enabled="False"></asp:regularexpressionvalidator><asp:requiredfieldvalidator id="rngOTHER_STRU_LIMIT" runat="server" Display="Dynamic" ControlToValidate="txtOTHER_STRU_LIMIT"
										Enabled="False">COVERAGE B</asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="25%"><asp:label id="capOTHER_STRU_DESC" runat="server">Other Structures Description</asp:label></TD>
								<TD class="midcolora" width="25%"><asp:textbox id="txtOTHER_STRU_DESC" runat="server" maxlength="100" size="30"></asp:textbox></TD>
								<TD class="midcolora" width="25%"><asp:label id="capPERSONAL_PROP_LIMIT" runat="server">Personal Property Limit</asp:label><span class="mandatory" id="spnMandatoryC" runat="server">*</span>
								</TD>
								<TD class="midcolora" width="25%"><asp:textbox id="txtPERSONAL_PROP_LIMIT" runat="server" CssClass="INPUTCURRENCY" maxlength="9"
										size="17" ReadOnly="True"></asp:textbox><asp:label id="capCOVERAGEC" runat="server" Visible="False">N.A.</asp:label><br>
									<asp:requiredfieldvalidator id="rfvPERSONAL_PROP_LIMIT" runat="server" Display="Dynamic" ControlToValidate="txtPERSONAL_PROP_LIMIT"
										ErrorMessage="Please enter Coverage C" Enabled="False"></asp:requiredfieldvalidator><asp:rangevalidator id="rngPERSONAL_PROP_LIMIT" runat="server" Type="Currency" Display="Dynamic" ControlToValidate="txtPERSONAL_PROP_LIMIT"
										ErrorMessage="Coverage C" Enabled="False" MinimumValue="15000" MaximumValue="100000"></asp:rangevalidator><BR>
									<asp:regularexpressionvalidator id="revPERSONAL_PROP_LIMIT" Runat="server" Display="Dynamic" ControlToValidate="txtPERSONAL_PROP_LIMIT"
										Enabled="False"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="25%"><asp:label id="capREPLACEMENT_COST_CONTS" runat="server">Replacement Cost Contents</asp:label></TD>
								<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbREPLACEMENT_COST_CONTS" onfocus="SelectComboIndex('cmbREPLACEMENT_COST_CONTS')"
										runat="server">
										<asp:ListItem Value=""></asp:ListItem>
										<asp:ListItem Value='1'>Yes</asp:ListItem>
										<asp:ListItem Value='0'>No</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD class="midcolora" width="25%"><asp:label id="capLOSS_OF_USE" runat="server">Loss Of Use</asp:label></TD>
								<TD class="midcolora" width="25%"><asp:textbox id="txtLOSS_OF_USE" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="17"
										ReadOnly="True"></asp:textbox><asp:label id="capCOVERAGED" runat="server" Visible="False">N.A.</asp:label><asp:regularexpressionvalidator id="revLOSS_OF_USE" runat="server" Display="Dynamic" ControlToValidate="txtLOSS_OF_USE"
										Enabled="False"></asp:regularexpressionvalidator><asp:requiredfieldvalidator id="rngLOSS_OF_USE" runat="server" Display="Dynamic" ControlToValidate="txtLOSS_OF_USE"
										Enabled="False">COVERAGE D</asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="25%"><asp:label id="capTHEFT_DEDUCTIBLE_AMT" runat="server">Theft Deductible</asp:label></TD>
								<TD class="midcolora" width="25%"><asp:textbox id="txtTHEFT_DEDUCTIBLE_AMT" runat="server" CssClass="INPUTCURRENCY" maxlength="9"
										size="17"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revTHEFT_DEDUCTIBLE_AMT" Runat="server" Display="Dynamic" ControlToValidate="txtTHEFT_DEDUCTIBLE_AMT"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="25%"><asp:label id="capPERSONAL_LIAB_LIMIT" runat="server">Personal Liability Limits</asp:label><span class="mandatory" id="Span1" runat="server">*</span></TD>
								<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbPERSONAL_LIAB_LIMIT" runat="server"  Onchange="ChkNocoverage();"></asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvPERSONAL_LIAB_LIMIT" runat="server" Display="Dynamic" ControlToValidate="cmbPERSONAL_LIAB_LIMIT"
										ErrorMessage="RequiredFieldValidator"></asp:requiredfieldvalidator><BR>
								</TD>
							</tr>
							<tr>
								<td class="midcolora" width="25%"></td>
								<TD class="midcolora" width="25%"></TD>
								<TD class="midcolora" width="25%"><asp:label id="capMED_PAY_EACH_PERSON" runat="server" ALIGN="LEFT">Medical Payments each Person</asp:label><span class="mandatory" id="Span2" runat="server">*</span></TD>
								<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbMED_PAY_EACH_PERSON" onfocus="SelectComboIndex('cmbMED_PAY_EACH_PERSON')"  Onchange="ChkNocoverage();"
										Runat="server">
										<asp:ListItem Value="1000">1000</asp:ListItem>
										<asp:ListItem Value="2000">2000</asp:ListItem>
										<asp:ListItem Value="3000">3000</asp:ListItem>
										<asp:ListItem Value="4000">4000</asp:ListItem>
										<asp:ListItem Value="5000">5000</asp:ListItem>
									</asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvMED_PAY_EACH_PERSON" runat="server" Display="Dynamic" ControlToValidate="cmbMED_PAY_EACH_PERSON"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<td class="midcolora" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></td>
								<td class="midcolora" colSpan="1"></td>
								<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
							<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
							<INPUT id="hidCustomer_ID" type="hidden" value="0" name="hidCustomer_ID" runat="server">
							<INPUT id="hidPol_ID" type="hidden" value="0" name="hidPol_ID" runat="server"> <INPUT id="hidPol_Version_ID" type="hidden" value="0" name="hidPol_Version_ID" runat="server">
							<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
							<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
							<INPUT id="hidDWELLING_ID" type="hidden" value="0" name="hidDWELLING_ID" runat="server">
							<INPUT id="hidPolcyType" type="hidden" name="hidPolcyType" runat="server"> <INPUT id="hidStateId" type="hidden" name="hidStateId" runat="server">
							<INPUT id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server"> <INPUT id="hidReplValue" type="hidden" name="hidReplValue" runat="server">
							<INPUT id="hidDefaultValue" type="hidden" name="hidDefaultValue" runat="server">
							<INPUT id="hidDwellReplaceCost" type="hidden" name="hidDwellReplaceCost" runat="server">
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none; Z-INDEX: 101; POSITION: absolute"><iframe id="lookupLayerIFrame" style="DISPLAY: none; Z-INDEX: 1001; FILTER: alpha(opacity=0); BACKGROUND-COLOR: #000000"
				width="0" height="0" top="0px;" left="0px"></iframe>
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
					<td colSpan="2"><span id="LookUpMsg"></span></td>
				</tr>
			</table>
		</DIV>
		<!-- For lookup layer ends here-->
		<script>
            //   RefreshWindowsGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidDWELLING_ID').value);
		</script>
	</BODY>
</HTML>
