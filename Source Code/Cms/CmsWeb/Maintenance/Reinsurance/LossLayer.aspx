<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="LossLayer.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Reinsurance.LossLayer" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<head>
		<title>LossLayer</title>
		<meta content="False" name="vs_showGrid">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script>
		<script language="javascript" type="text/javascript">
		
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";

			function AddData()
			{
			ChangeColor();
			DisableValidators();
			
			document.getElementById('hidLOSS_LAYER_ID').value	=	'New';
			
//			if(document.getElementById('trBody').style.display != "none")
//				document.getElementById('txtLAYER').focus();
			document.getElementById('txtLAYER').value ='';
			document.getElementById('txtLAYER_AMOUNT').value ='';
			document.getElementById('txtRETENTION_AMOUNT').value ='';
			document.getElementById('txtRETENTION_PERCENTAGE').value ='';
			document.getElementById('txtREIN_CEDED').value ='';
			document.getElementById('cmbCOMPANY_RETENTION').options.selectedIndex = 0;
			if(document.getElementById('btnActivate'))
				document.getElementById('btnActivate').setAttribute('disabled',true);
			if(document.getElementById('btnDelete'))
				document.getElementById('btnDelete').style.display = "none";	
					
			
			
			}
		

			function populateXML()
			{
				var tempXML;
				tempXML=document.getElementById("hidOldData").value;
				//alert(document.getElementById("hidOldData").value);
				if(document.getElementById('hidFormSaved')!=null &&( document.getElementById('hidFormSaved').value == '0'||document.getElementById('hidFormSaved').value == '1'))
				{				
					if(tempXML!="" && tempXML!="0")
					{
		
						//populateFormData(tempXML,"LossLayer");	
						//FormatAmount(document.getElementById('txtLAYER'));
						//FormatAmount(document.getElementById('txtLAYER_AMOUNT'));
						//FormatAmount(document.getElementById('txtRETENTION_AMOUNT'));
						//FormatAmount(document.getElementById('txtRETENTION_PERCENTAGE'));
						//FormatAmount(document.getElementById('txtREIN_CEDED'));					
					}
					else
					{
						AddData();
					}
				}
				else
				{
					AddData();
				}
				
				
				return false;
			}
								  
			function Reset()
			{
				DisableValidators();
				document.LossLayer.reset();
//				if(document.getElementById('trBody').style.display != "none")
//				{
//					document.getElementById('txtLAYER').focus();				
//				}
				ChangeColor();
				return false;
			}						  
			
			//Formats the amount and convert 111 into 1.11
		function FormatAmount(txtAmount)
		{
		    alert("test");				
			if (txtAmount.value != "")
			{
				amt = txtAmount.value;
				
				amt = ReplaceAll(amt,".","");
				
				if (amt.length == 1)
					amt = amt + "0";
				
				if ( ! isNaN(amt))
				{
					
					DollarPart = amt.substring(0, amt.length - 2);
					CentPart = amt.substring(amt.length - 2);
					
					txtAmount.value = InsertDecimal(amt);
				}
			}
		} 
//				
		function showhideRetention()
		{
		//alert(document.getElementById("cmbCOMPANY_RETENTION").options[document.getElementById("cmbCOMPANY_RETENTION").selectedIndex].value);
			if(document.getElementById("cmbCOMPANY_RETENTION").options[document.getElementById("cmbCOMPANY_RETENTION").selectedIndex].value=="1")
				{
					document.getElementById("spnRETENTION_PERCENTAGE").style.display="inline";
					document.getElementById('txtRETENTION_PERCENTAGE').className = "MandatoryControl";
					document.getElementById("txtRETENTION_PERCENTAGE").readOnly = false;
					document.getElementById("rfvRETENTION_PERCENTAGE").setAttribute("enabled",true);					
					document.getElementById("rfvRETENTION_PERCENTAGE").setAttribute("isValid",true);	
					document.getElementById("revRETENTION_PERCENTAGE").setAttribute("enabled",true);					
					document.getElementById("revRETENTION_PERCENTAGE").setAttribute("isValid",true);
					document.getElementById("rngRETENTION_PERCENTAGE").setAttribute("enabled",true);					
					document.getElementById("rngRETENTION_PERCENTAGE").setAttribute("isValid",true);
					
					
					document.getElementById("spnRETENTION_AMOUNT").style.display="inline";
					document.getElementById('txtRETENTION_AMOUNT').className = "MandatoryControl";
					document.getElementById("txtRETENTION_AMOUNT").readOnly = false;
					document.getElementById("rfvLOSS_COMPANY_RETENTION_TXT").setAttribute("enabled",true);					
					document.getElementById("rfvLOSS_COMPANY_RETENTION_TXT").setAttribute("isValid",true);	
					document.getElementById("revLOSS_COMPANY_RETENTION_TXT").setAttribute("enabled",true);					
					document.getElementById("revLOSS_COMPANY_RETENTION_TXT").setAttribute("isValid",true);					
				}
				else
				{
					//document.getElementById("txtRETENTION_PERCENTAGE").value = "0";
					document.getElementById("rfvRETENTION_PERCENTAGE").style.display="none";
					document.getElementById("revRETENTION_PERCENTAGE").style.display="none";
					document.getElementById("spnRETENTION_PERCENTAGE").style.display="none";
					document.getElementById('txtRETENTION_PERCENTAGE').className = "none";
					document.getElementById("txtRETENTION_PERCENTAGE").readOnly = true;
					document.getElementById("rfvRETENTION_PERCENTAGE").setAttribute("enabled",false);					
					document.getElementById("rfvRETENTION_PERCENTAGE").setAttribute("isValid",false);
					document.getElementById("revRETENTION_PERCENTAGE").setAttribute("enabled",false);					
					document.getElementById("revRETENTION_PERCENTAGE").setAttribute("isValid",false);	
					document.getElementById("rngRETENTION_PERCENTAGE").setAttribute("enabled",false);					
					document.getElementById("rngRETENTION_PERCENTAGE").setAttribute("isValid",false);
					//document.getElementById("txtREIN_CEDED_PERCENTAGE").value = 100;

					//document.getElementById("txtRETENTION_AMOUNT").value = "0";
					document.getElementById("rfvLOSS_COMPANY_RETENTION_TXT").style.display="none";
					document.getElementById("revLOSS_COMPANY_RETENTION_TXT").style.display="none";
					document.getElementById("spnRETENTION_AMOUNT").style.display="none";
					document.getElementById('txtRETENTION_AMOUNT').className = "none";
					document.getElementById("txtRETENTION_AMOUNT").readOnly = true;
					document.getElementById("rfvLOSS_COMPANY_RETENTION_TXT").setAttribute("enabled",false);					
					document.getElementById("rfvLOSS_COMPANY_RETENTION_TXT").setAttribute("isValid",false);
					document.getElementById("revLOSS_COMPANY_RETENTION_TXT").setAttribute("enabled",false);					
					document.getElementById("revLOSS_COMPANY_RETENTION_TXT").setAttribute("isValid",false);	
					//document.getElementById("txtREIN_CEDED").value = document.getElementById("txtLAYER_AMOUNT").value; 

				}
					//calc_percentage('cmbCOMPANY_RETENTION');
					//calc_amount('cmbCOMPANY_RETENTION');

		}																					
			
		function calc_all()
		{
			if(document.getElementById('hidLOSS_LAYER_ID').value== 'New' || document.getElementById("cmbCOMPANY_RETENTION").options[document.getElementById("cmbCOMPANY_RETENTION").selectedIndex].value=="0")
				{
					calc_percentage('cmbCOMPANY_RETENTION');
					calc_amount('cmbCOMPANY_RETENTION');
				}
}

function FormatAmountForSum(num) {

    num = ReplaceAll(num, sBaseDecimalSep, '.');
    return num;
}

function validateRETENTION_AMOUNT(objSource, objArgs) {

    var RETENTION_AMOUNT = document.getElementById(objSource.controltovalidate).value;

    RETENTION_AMOUNT = FormatAmountForBaseCurrency(RETENTION_AMOUNT);
    
    var LAYER_AMOUNT = document.getElementById("txtLAYER_AMOUNT").value == '' ? document.getElementById("txtLAYER_AMOUNT").value = '0' : document.getElementById("txtLAYER_AMOUNT").value;
    LAYER_AMOUNT = FormatAmountForBaseCurrency(LAYER_AMOUNT);

    if (parseFloat(RETENTION_AMOUNT) <= LAYER_AMOUNT)
        objArgs.IsValid = true;
    else
        objArgs.IsValid = false;
        
}
function validatePercentage(objSource, objArgs) {

    var Limt = document.getElementById(objSource.controltovalidate).value;

    Limt = FormatAmountForSum(Limt);
    if (parseFloat(Limt) >= 0 && parseFloat(Limt) <= 100)
        objArgs.IsValid = true;
    else
        objArgs.IsValid = false;
}

		function calc_percentage(controlId)
		{
	//alert(controlId);
			var retention = document.getElementById("txtRETENTION_PERCENTAGE").value;
			var ceded     = document.getElementById("txtREIN_CEDED_PERCENTAGE").value;
			layeramount =document.getElementById("txtLAYER_AMOUNT").value;
			layeramount = ReplaceAll(layeramount,",","");

			if(document.getElementById("cmbCOMPANY_RETENTION").options[document.getElementById("cmbCOMPANY_RETENTION").selectedIndex].value=="1")
			{
				switch(controlId)
				{
					case "txtRETENTION_PERCENTAGE":
						document.getElementById("txtREIN_CEDED_PERCENTAGE").value = 100 - retention; 
						/*document.getElementById("txtRETENTION_AMOUNT").value = (layeramount*retention)/100
						document.getElementById("txtREIN_CEDED").value = (layeramount - document.getElementById("txtRETENTION_AMOUNT").value )
						document.getElementById("txtRETENTION_AMOUNT").value=formatCurrency(document.getElementById("txtRETENTION_AMOUNT").value);
						document.getElementById("txtREIN_CEDED").value=formatCurrency(document.getElementById("txtREIN_CEDED").value);*/

						break;
					case "txtREIN_CEDED_PERCENTAGE":
						document.getElementById("txtRETENTION_PERCENTAGE").value = 100 - ceded; 
						document.getElementById("txtREIN_CEDED").value = (layeramount*ceded)/100
						document.getElementById("txtRETENTION_AMOUNT").value = (layeramount - document.getElementById("txtREIN_CEDED").value )
						document.getElementById("txtRETENTION_AMOUNT").value=formatCurrency(document.getElementById("txtRETENTION_AMOUNT").value);
						document.getElementById("txtREIN_CEDED").value=formatCurrency(document.getElementById("txtREIN_CEDED").value);
						break;
				}
				if(controlId =='txtRETENTION_PERCENTAGE')
				{
					document.getElementById("txtRETENTION_AMOUNT").readonly = false;
					//document.getElementById("txtRETENTION_AMOUNT").focus();
				}
			}
			else if(document.getElementById("cmbCOMPANY_RETENTION").options[document.getElementById("cmbCOMPANY_RETENTION").selectedIndex].value=="0")
			{
				document.getElementById("txtREIN_CEDED_PERCENTAGE").value = 100;
			}
			else 
				document.getElementById("txtREIN_CEDED_PERCENTAGE").value = '';
			
			
		}
																			  
		function calc_amount(controlId) {
		     
			layeramt =document.getElementById("txtLAYER_AMOUNT").value;
			retamt = document.getElementById("txtRETENTION_AMOUNT").value;
			reinamt = document.getElementById("txtREIN_CEDED").value;
			layeramt = ReplaceAll(layeramt,",","");
			retamt = ReplaceAll(retamt,",","");
			reinamt = ReplaceAll(reinamt,",","");
			retentionper = document.getElementById("txtRETENTION_PERCENTAGE").value;
			cededper     = document.getElementById("txtREIN_CEDED_PERCENTAGE").value;
			if (layeramt != '')
			{
				switch(controlId)
				{
					case "txtRETENTION_AMOUNT":
						document.getElementById("txtREIN_CEDED").value = layeramt - retamt; 
						break;
					case "txtREIN_CEDED":
					if(document.getElementById("cmbCOMPANY_RETENTION").options[document.getElementById("cmbCOMPANY_RETENTION").selectedIndex].value =="0")
					{
						document.getElementById("txtREIN_CEDED").value = layeramt; 
					}
					else
						document.getElementById("txtRETENTION_AMOUNT").value = layeramt - reinamt; 
						break;
					case "cmbCOMPANY_RETENTION":
					if(document.getElementById("cmbCOMPANY_RETENTION").options[document.getElementById("cmbCOMPANY_RETENTION").selectedIndex].value =="0")
					{
						document.getElementById("txtREIN_CEDED").value = layeramt; 
						break;
					}
					case "txtLAYER_AMOUNT":
						document.getElementById("txtREIN_CEDED").value = (layeramt*cededper)/100
						document.getElementById("txtRETENTION_AMOUNT").value = (layeramt*retentionper)/100
						break;
				}
			}
			else 
			{
				document.getElementById("txtREIN_CEDED").value = ''; 
				document.getElementById("txtRETENTION_AMOUNT").value =''; 
			}
			document.getElementById("txtRETENTION_AMOUNT").value=formatCurrency(document.getElementById("txtRETENTION_AMOUNT").value);
			document.getElementById("txtREIN_CEDED").value=formatCurrency(document.getElementById("txtREIN_CEDED").value);
			
		}
		function FormatAmountForBaseCurrency(num) {
		    num = ReplaceAll(num, sBaseGroupSep, '');
		    num = ReplaceAll(num, sBaseDecimalSep, '.');
		    return num;
		}
		       

		
		</script>
		<%--Added by Pradeep Kushwaha on 17-Feb-2011 (Ls 75)--%>
		<script type="text/javascript" language="javascript">
		    $(document).ready(function() {

		        $("#txtRETENTION_PERCENTAGE").blur();
		        $("#txtREIN_CEDED_PERCENTAGE").blur();
		        $("#txtRETENTION_AMOUNT").blur();
		        $("#txtREIN_CEDED").blur();

		        $("#txtREIN_CEDED_PERCENTAGE").attr('readonly', 'true');
		        //$("#txtRETENTION_AMOUNT").attr('readonly', 'true');
		        $("#txtREIN_CEDED").attr('readonly', 'true');

		        function CalculateLossAmount() {
		            var LAYER_AMOUNT = document.getElementById("txtLAYER_AMOUNT").value == '' ? document.getElementById("txtLAYER_AMOUNT").value = '0' : document.getElementById("txtLAYER_AMOUNT").value;
		            LAYER_AMOUNT = FormatAmountForBaseCurrency(LAYER_AMOUNT);

		            var RETENTION_PERCENTAGE = document.getElementById("txtRETENTION_PERCENTAGE").value == '' ? document.getElementById("txtRETENTION_PERCENTAGE").value = '0' : document.getElementById("txtRETENTION_PERCENTAGE").value;
		            RETENTION_PERCENTAGE = FormatAmountForSum(RETENTION_PERCENTAGE);


		            RETENTION_AMOUNT = formatBaseCurrencyAmount((parseFloat(LAYER_AMOUNT) * parseFloat(RETENTION_PERCENTAGE)) / 100);
		            $("#txtRETENTION_AMOUNT").val(RETENTION_AMOUNT);

		            var REIN_CEDED_PERCENTAGE = document.getElementById("txtREIN_CEDED_PERCENTAGE").value == '' ? document.getElementById("txtREIN_CEDED_PERCENTAGE").value = '0' : document.getElementById("txtREIN_CEDED_PERCENTAGE").value;
		            REIN_CEDED_PERCENTAGE = FormatAmountForSum(REIN_CEDED_PERCENTAGE);

		            REIN_CEDED = formatBaseCurrencyAmount((parseFloat(LAYER_AMOUNT) * parseFloat(REIN_CEDED_PERCENTAGE)) / 100);
		            $("#txtREIN_CEDED").val(REIN_CEDED);
		        }

		        $("#txtRETENTION_PERCENTAGE").change(function() {

		            var REIN_CEDED_PERCENTAGE = $("#txtRETENTION_PERCENTAGE").val();


		            REIN_CEDED_PERCENTAGE = FormatAmountForSum(REIN_CEDED_PERCENTAGE);

		            REIN_CEDED_PERCENTAGE = 100 - REIN_CEDED_PERCENTAGE;

		            if (parseFloat(REIN_CEDED_PERCENTAGE) >= 0 && parseFloat(REIN_CEDED_PERCENTAGE) <= 100) {
		                $("#txtREIN_CEDED_PERCENTAGE").val(formatRateBase(REIN_CEDED_PERCENTAGE, 4));
		                $("#txtREIN_CEDED_PERCENTAGE").blur();
		                CalculateLossAmount();
		            }
		        });
		        $("#txtRETENTION_AMOUNT").change(function() {

		            //debugger;
		            var LAYER_AMOUNT = document.getElementById("txtLAYER_AMOUNT").value == '' ? document.getElementById("txtLAYER_AMOUNT").value = '0' : document.getElementById("txtLAYER_AMOUNT").value;
		            LAYER_AMOUNT = FormatAmountForBaseCurrency(LAYER_AMOUNT);

		            var RETENTION_AMOUNT = document.getElementById("txtRETENTION_AMOUNT").value == '' ? document.getElementById("txtRETENTION_AMOUNT").value = '0' : document.getElementById("txtRETENTION_AMOUNT").value;
		            RETENTION_AMOUNT = FormatAmountForBaseCurrency(RETENTION_AMOUNT);
                    
		            var REIN_PERCENTAGE = parseFloat(RETENTION_AMOUNT) / parseFloat(LAYER_AMOUNT) * 100;
		            REIN_PERCENTAGE = formatRateBase(FormatAmountForBaseCurrency(REIN_PERCENTAGE), 4);
		            $("#txtRETENTION_PERCENTAGE").val(REIN_PERCENTAGE);

		            REIN_PERCENTAGE = FormatAmountForBaseCurrency(REIN_PERCENTAGE)
		            var REIN_CEDED_PERCENTAGE = 100 - REIN_PERCENTAGE;
		            $("#txtREIN_CEDED_PERCENTAGE").val(formatRateBase(REIN_CEDED_PERCENTAGE, 4));


		            var REIN_CEDED = formatBaseCurrencyAmount(parseFloat(LAYER_AMOUNT) - parseFloat(RETENTION_AMOUNT));
		            $("#txtREIN_CEDED").val(REIN_CEDED);
		            

		        });

		        $("#txtLAYER_AMOUNT").change(function() {

		            var RETENTION_PERCENTAGE = document.getElementById("txtRETENTION_PERCENTAGE").value == '' ? document.getElementById("txtRETENTION_PERCENTAGE").value = '0' : document.getElementById("txtRETENTION_PERCENTAGE").value;
		            RETENTION_PERCENTAGE = FormatAmountForSum(RETENTION_PERCENTAGE);
		            if (parseFloat(RETENTION_PERCENTAGE) >= 0 && parseFloat(RETENTION_PERCENTAGE) <= 100) {
		                CalculateLossAmount();
		            }
		        });
		      

		    });
		    		
		</script>
		<%--Added Till here --%>
	</head>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();ChangeColor();">
		<FORM id="LossLayer" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									<!--START April 09 2007 Harmanjeet-->
									<TD class="midcolora" width="18%"><asp:label id="capLAYER" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%">
										<asp:textbox style="TEXT-ALIGN:right" cssclass="INPUTCURRENCY" id="txtLAYER" runat="server" MaxLength="2" size="14" tabIndex="1" ></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvLAYER" runat="server" ControlToValidate="txtLAYER" ErrorMessage=""
												Display="Dynamic"></asp:requiredfieldvalidator><%--Please enter layer.--%>
											<asp:regularexpressionvalidator id="revLAYER" runat="server" Display="Dynamic" ControlToValidate="txtLAYER"
											ErrorMessage="Please enter Numeric Value."></asp:regularexpressionvalidator><%--RegularExpressionValidator--%>
										</TD>
										<TD class="midcolora" width="18%"><asp:label id="capLAYER_AMOUNT" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%">
										<asp:textbox style="TEXT-ALIGN:right" class="INPUTCURRENCY" id="txtLAYER_AMOUNT" runat="server" MaxLength="10" tabIndex="2"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvLAYER_AMOUNT" runat="server" ControlToValidate="txtLAYER_AMOUNT" ErrorMessage=""
												Display="Dynamic"></asp:requiredfieldvalidator>
											<asp:regularexpressionvalidator id="revLAYER_AMOUNT" runat="server" Display="Dynamic" ControlToValidate="txtLAYER_AMOUNT" ErrorMessage=""></asp:regularexpressionvalidator>
									</TD>
									</tr>
									<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_RETENTION" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" colSpan="3" width="32%">
										<asp:dropdownlist id="cmbCOMPANY_RETENTION" onfocus="SelectComboIndex('cmbCOMPANY_RETENTION')"	
											runat="server" tabIndex="3"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvLOSS_COMPANY_RETENTION" ControlToValidate="cmbCOMPANY_RETENTION" ErrorMessage="" Display="Dynamic"
												Runat="server"></asp:requiredfieldvalidator><%--Please Select Company retention--%>
									</TD>
									
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capRETENTION_PERCENTAGE" runat="server"></asp:label><span id = "spnRETENTION_PERCENTAGE" class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%">
										<asp:textbox style="TEXT-ALIGN:right" class="INPUTCURRENCY" id="txtRETENTION_PERCENTAGE" runat="server" MaxLength="7" size="14"  tabIndex="4"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvRETENTION_PERCENTAGE" runat="server" ControlToValidate="txtRETENTION_PERCENTAGE" ErrorMessage=""
												Display="Dynamic"></asp:requiredfieldvalidator><%--Please enter Retention Ceded %.--%>
											<asp:regularexpressionvalidator id="revRETENTION_PERCENTAGE" runat="server" Display="Dynamic" ControlToValidate="txtRETENTION_PERCENTAGE"
											ErrorMessage=""></asp:regularexpressionvalidator><%--RegularExpressionValidator--%>
											<asp:CustomValidator
                                        ID="csvRETENTION_PERCENTAGE" Display="Dynamic" ControlToValidate="txtRETENTION_PERCENTAGE"
                                        ClientValidationFunction="validatePercentage" runat="server"></asp:CustomValidator>
											<BR>
			<%--							<asp:rangevalidator id="rngRETENTION_PERCENTAGE" runat="server" ControlToValidate="txtRETENTION_PERCENTAGE" ErrorMessage="RegularExpressionValidator"
												Display="Dynamic" MinimumValue="0" MaximumValue="100" Type="Integer"></asp:rangevalidator>--%>
									</TD>
									<!--<td class="midcolora" colSpan="2"></td>-->
									<TD class="midcolora" width="18%"><asp:label id="capRETENTION_AMOUNT" runat="server"></asp:label><span id = "spnRETENTION_AMOUNT" class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%">
									<asp:textbox style="TEXT-ALIGN:right" class="INPUTCURRENCY" id="txtRETENTION_AMOUNT"  runat="server" size="14" maxlength="10" tabIndex="5" ></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvLOSS_COMPANY_RETENTION_TXT" runat="server" ControlToValidate="txtRETENTION_AMOUNT" ErrorMessage="" Display="Dynamic"></asp:requiredfieldvalidator>
									<asp:regularexpressionvalidator id="revLOSS_COMPANY_RETENTION_TXT" runat="server" Display="Dynamic" ControlToValidate="txtRETENTION_AMOUNT" ErrorMessage=""></asp:regularexpressionvalidator>
									<asp:CustomValidator ID="csvRETENTION_AMOUNT" Display="Dynamic" ControlToValidate="txtRETENTION_AMOUNT" ClientValidationFunction="validateRETENTION_AMOUNT" runat="server"></asp:CustomValidator>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capREIN_CEDED_PERCENTAGE" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%">
										<asp:textbox style="TEXT-ALIGN:right" class="INPUTCURRENCY" id="txtREIN_CEDED_PERCENTAGE" runat="server" MaxLength="5" size="14" tabIndex="6"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvREIN_CEDED_PERCENTAGE" runat="server" ControlToValidate="txtREIN_CEDED_PERCENTAGE" ErrorMessage=""
												Display="Dynamic"></asp:requiredfieldvalidator>
											<asp:regularexpressionvalidator id="revREIN_CEDED_PERCENTAGE" runat="server" Display="Dynamic" ControlToValidate="txtREIN_CEDED_PERCENTAGE"
											ErrorMessage=""></asp:regularexpressionvalidator><%--RegularExpressionValidator--%>
											
											<asp:CustomValidator
                                        ID="csvREIN_CEDED_PERCENTAGE" Display="Dynamic" ControlToValidate="txtREIN_CEDED_PERCENTAGE"
                                        ClientValidationFunction="validatePercentage" runat="server"></asp:CustomValidator>
											<BR>
				<%--						<asp:rangevalidator id="rngREIN_CEDED_PERCENTAGE" runat="server" ControlToValidate="txtREIN_CEDED_PERCENTAGE" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic" MinimumValue="0" MaximumValue="100" Type="Integer"></asp:rangevalidator>--%>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capREIN_CEDED" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%">
										<asp:textbox style="TEXT-ALIGN:right" class="INPUTCURRENCY" id="txtREIN_CEDED" runat="server" MaxLength="10" size="14" tabIndex="7" ></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvREIN_CEDED" runat="server" ControlToValidate="txtREIN_CEDED" ErrorMessage=""
												Display="Dynamic"></asp:requiredfieldvalidator>
											<asp:regularexpressionvalidator id="revREIN_CEDED" runat="server" Display="Dynamic" ControlToValidate="txtREIN_CEDED"
											ErrorMessage=""></asp:regularexpressionvalidator><%--RegularExpressionValidator--%>
									</TD>
									<!--<td class="midcolora" colSpan="2"></td>-->
								</tr>
								<!--END Harmanjeet-->
								<tr>
									<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" tabIndex="8"></cmsb:cmsbutton>&nbsp;
										<cmsb:cmsbutton class="clsButton" id="btnActivate" runat="server" Text="" visible="True"
											CausesValidation="False" tabIndex="9"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" visible="True" CausesValidation="false"></cmsb:cmsbutton></td>
									<td class="midcolorr" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" tabIndex="10"></cmsb:cmsbutton></td>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> 
			<INPUT id="hidCONTRACT_ID" type="hidden" value="0" name="hidCONTRACT_ID" runat="server">
			<INPUT id="hidReset" type="hidden" value="0" name="hidReset" runat="server"> 
			<INPUT id="hidLOSS_LAYER_ID" type="hidden" value="0" name="hidLOSS_LAYER_ID" runat="server">
		</FORM>
		<script>
		RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidLOSS_LAYER_ID').value,true);
			
					//alert(document.getElementById("btnActivateDeactivate").value);	
		</script>
	</BODY>
</HTML>
