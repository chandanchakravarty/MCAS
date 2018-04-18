<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddOccurrenceDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddOccurrenceDetails" validateRequest=false  %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>OCCURRANCE DETAILS</title>
<meta content="Microsoft Visual Studio 7.0" name=GENERATOR>
<meta content=C# name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema>
<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
<script src="/cms/cmsweb/scripts/xmldom.js"></script>

<script src="/cms/cmsweb/scripts/common.js"></script>
<script src="/cms/cmsweb/scripts/form.js"></script>
<script src="/cms/cmsweb/scripts/Calendar.js"></script>
<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script> 
<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
<script>
			function ValidateDescLength(objSource , objArgs)
			{
				if(document.getElementById('txtDESCRIPTION').value.length>300)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			}
			function ValidateLossLength(objSource , objArgs)
			{
				if(document.getElementById('txtLOSS_LOCATION').value.length>300)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			
			}
			function ValidateViolationLength(objSource , objArgs)
			{
				if(document.getElementById('txtVIOLATION').value.length>300)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			}
			function ResetTheForm()
			{
				DisableValidators();	
				document.CLM_OCCURANCE_DETAILS.reset();
				Init();
				resetmultiselect();
				addLossTypes();
				return false;										
			}
			function EnableDisableDesc(cmbCombo,txtDesc,capDesc)
			{
				var count = 0;
				strLOB_ID = document.getElementById("hidLOB_ID").value;
				if(strLOB_ID!=<%=((int)enumLOB.GENL).ToString()%> && strLOB_ID!=<%=((int)enumLOB.REDW).ToString()%> && strLOB_ID!=<%=((int)enumLOB.HOME).ToString()%>)
					return;
				if(!(cmbCombo)) return;		
				if(document.getElementById("hidLOSS_TYPE").value!= "")						
				//if (cmbCombo.selectedIndex > -1)
				{
					
					var LossTypes = document.getElementById("hidLOSS_TYPE").value;
					var LossType = LossTypes.split(",");
					for(x = 0; x < LossType.length-1 ;x++)
					{
						if(LossType[x]==62)
						{
							count = 1;
						}
						
					}
					if(count==1)
					{
						hideshow(cmbCombo,txtDesc,capDesc,1);
					}
					else
					{
						hideshow(cmbCombo,txtDesc,capDesc,0);
					}
					//Checking value only if item is selected
				//if (cmbCombo.options[cmbCombo.selectedIndex].text == "Other")
				/*if(count ==1)
					{
						//Disabling the description field, if No is selected
						txtDesc.style.display = "inline";
						capDesc.style.display = "inline";
						
						//Enabling the validators
						if (document.getElementById("rfv" + txtDesc.id.substring(3)) != null)
						{
							document.getElementById("rfv" + txtDesc.id.substring(3)).setAttribute("enabled",true);
							
							if (document.getElementById("rfv" + txtDesc.id.substring(3)).isvalid == false)
								document.getElementById("rfv" + txtDesc.id.substring(3)).style.display = "inline";
						}
						
						//making the * sign visible					
						if (document.getElementById("spn" + txtDesc.id.substring(3)) != null)
						{
							document.getElementById("spn" + txtDesc.id.substring(3)).style.display = "inline";
						}
						ChangeColor();
											
					}
					else
					{
					
						//Enabling the description field, if yes is selected
						txtDesc.style.display = "none";
					
						capDesc.style.display = "none";
						//lblNA.innerHTML="NA";
						
						//Disabling the validators					
						if (document.getElementById("rfv" + txtDesc.id.substring(3)) != null)
						{
							document.getElementById("rfv" + txtDesc.id.substring(3)).setAttribute("enabled",false);
							document.getElementById("rfv" + txtDesc.id.substring(3)).style.display = "none";
						}
						
						//making the * sign invisible					
						if (document.getElementById("spn" + txtDesc.id.substring(3)) != null)
						{
							document.getElementById("spn" + txtDesc.id.substring(3)).style.display = "none";
						}
					}*/
				}
				else
				{
					//Disabling the description field, if No is selected
					txtDesc.style.display = "none";
					capDesc.style.display = "none";
					//lblNA.innerHTML="NA";
					
					//Disabling the validators					
					if (document.getElementById("rfv" + txtDesc.id.substring(3)) != null)
					{
						document.getElementById("rfv" + txtDesc.id.substring(3)).setAttribute("enabled",false);
						document.getElementById("rfv" + txtDesc.id.substring(3)).style.display = "none";
					}
					
					//making the * sign invisible					
					if (document.getElementById("spn" + txtDesc.id.substring(3)) != null)
					{
						document.getElementById("spn" + txtDesc.id.substring(3)).style.display = "none";
					}
				}
			}
		
		function hideshow(cmbCombo,txtDesc,capDesc,count)
		{
			if(count ==1)
					{
						//Disabling the description field, if No is selected
						txtDesc.style.display = "inline";
						capDesc.style.display = "inline";
						
						//Enabling the validators
						if (document.getElementById("rfv" + txtDesc.id.substring(3)) != null)
						{
							document.getElementById("rfv" + txtDesc.id.substring(3)).setAttribute("enabled",true);
							
							if (document.getElementById("rfv" + txtDesc.id.substring(3)).isvalid == false)
								document.getElementById("rfv" + txtDesc.id.substring(3)).style.display = "inline";
						}
						
						//making the * sign visible					
						if (document.getElementById("spn" + txtDesc.id.substring(3)) != null)
						{
							document.getElementById("spn" + txtDesc.id.substring(3)).style.display = "inline";
						}
						ChangeColor();
											
					}
					else
					{
					
						//Enabling the description field, if yes is selected
						txtDesc.style.display = "none";
					
						capDesc.style.display = "none";
						//lblNA.innerHTML="NA";
						
						//Disabling the validators					
						if (document.getElementById("rfv" + txtDesc.id.substring(3)) != null)
						{
							document.getElementById("rfv" + txtDesc.id.substring(3)).setAttribute("enabled",false);
							document.getElementById("rfv" + txtDesc.id.substring(3)).style.display = "none";
						}
						
						//making the * sign invisible					
						if (document.getElementById("spn" + txtDesc.id.substring(3)) != null)
						{
							document.getElementById("spn" + txtDesc.id.substring(3)).style.display = "none";
						}
					}
		}	
		
		//functions for multi select box : started  --swarup
		function setLossTypes()
		{
			document.CLM_OCCURANCE_DETAILS.hidLOSS_TYPE.value = '';
			for (var i=0;i< document.getElementById('cmbLOSS_TYPE').options.length;i++)
			{
				document.CLM_OCCURANCE_DETAILS.hidLOSS_TYPE.value = document.CLM_OCCURANCE_DETAILS.hidLOSS_TYPE.value + document.getElementById('cmbLOSS_TYPE').options[i].value + ',';
			}	
			Page_ClientValidate();						
			var returnVal = funcValidateLossTypes();
			return Page_IsValid && returnVal;
		}
		
		function setLossType(catvalue)
		{
			for(s = document.getElementById('cmbFROMLOSS_TYPE').length-1; s >=0;s--)
			{
				if(document.getElementById('cmbFROMLOSS_TYPE').options[s].value == catvalue)
				{	
					document.getElementById('cmbLOSS_TYPE').options[document.getElementById('cmbLOSS_TYPE').length-1].text = document.getElementById('cmbFROMLOSS_TYPE').options[s].text;
					document.getElementById('cmbFROMLOSS_TYPE').options[s]=null;
					break;
				}
			}
				
		}
		function addLossTypes()
		{	
			//Done for Itrack Issue 6932 on 21 Jan 2010
			//if(document.getElementById("hidLOSS_TYPE").value == "")
			//   return false;
			//var LossTypes = document.getElementById("hidLOSS_TYPE").value + ',';
			var LossTypes = document.getElementById("hidLOSS_TYPE").value;
			var LossType = LossTypes.split(",");
			for(j = document.getElementById('cmbLOSS_TYPE').length-1; j >=0;j--)
			{
				document.getElementById('cmbLOSS_TYPE').options[j].value= null;
			}
			for(j = 0; j < LossType.length-1 ;j++)
			{
				document.getElementById('cmbLOSS_TYPE').options.length=document.getElementById('cmbLOSS_TYPE').length+1;
				document.getElementById('cmbLOSS_TYPE').options[document.getElementById('cmbLOSS_TYPE').length-1].value=LossType[j];
				setLossType(LossType[j]);
			}
		}
		
		function addOption(selectElement, text, value) 
		{ 
			var foundIndex = selectElement.options.length; 

			for (i = 0; i < selectElement.options.length; i++) 
			{ 
				if (selectElement.options[i].innerText.toLowerCase() > text.toLowerCase()) 
				{ 
					foundIndex = i; 
					break; 
				} 
			} 

			var oOption = new Option(text,value); 
			selectElement.options.add(oOption, foundIndex); 

		} 
		function selectLossTypes()
		{
			var sel = 0;
			for (var i=document.getElementById('cmbFROMLOSS_TYPE').options.length-1;i>=0;i--)
			{
					if (document.getElementById('cmbFROMLOSS_TYPE').options[i].selected == true)
					{
						if(document.getElementById('cmbFROMLOSS_TYPE').options[i].value == 62)
						{
							sel = 1;
						}
						if(document.getElementById('cmbLOSS_TYPE').length<1)
						{
						    addOption(document.getElementById('cmbLOSS_TYPE'), document.getElementById('cmbFROMLOSS_TYPE').options[i].text, document.getElementById('cmbFROMLOSS_TYPE').options[i].value);
						    document.getElementById('cmbFROMLOSS_TYPE').options[i] = null;
						}
					}
					
		  	}
		  	
		  	if(strLOB_ID!=<%=((int)enumLOB.GENL).ToString()%> && strLOB_ID!=<%=((int)enumLOB.REDW).ToString()%> && strLOB_ID!=<%=((int)enumLOB.HOME).ToString()%>)
		  		return;
		  	if(sel==1)
		  	{
				hideshow(document.getElementById('cmbLOSS_TYPE'),document.getElementById('txtOTHER_DESCRIPTION'),document.getElementById('capOTHER_DESCRIPTION'),1);		  	
		  	}
		  	else
			{
				hideshow(document.getElementById('cmbLOSS_TYPE'),document.getElementById('txtOTHER_DESCRIPTION'),document.getElementById('capOTHER_DESCRIPTION'),0);
			}
		  	
		}
		
		function deselectLossTypes()
		{	
			var des=0;
		  for (var i=document.getElementById('cmbLOSS_TYPE').options.length-1;i>=0;i--)
			{
			
				if (document.getElementById('cmbLOSS_TYPE').options[i].selected == true)
				{
						if(document.getElementById('cmbLOSS_TYPE').options[i].value == 62)
						{
							des =1;
						}
					addOption(document.getElementById('cmbFROMLOSS_TYPE'), document.getElementById('cmbLOSS_TYPE').options[i].text, document.getElementById('cmbLOSS_TYPE').options[i].value);
					document.getElementById('cmbLOSS_TYPE').options[i] = null;
				}
				
		  	}
		  
		  	if(strLOB_ID!=<%=((int)enumLOB.GENL).ToString()%> && strLOB_ID!=<%=((int)enumLOB.REDW).ToString()%> && strLOB_ID!=<%=((int)enumLOB.HOME).ToString()%>)
		  		return false;	
		  	if(des==1 || document.getElementById('cmbLOSS_TYPE').options.length != 0 || document.getElementById('cmbLOSS_TYPE').options.length == 0)//document.getElementById('cmbLOSS_TYPE').options.length == 0'--->Done for Itrack Issue 7559 on 1 July 2010  //Done for Itrack Issue 6892 on 18 Jan 2010
		  	{
				hideshow(document.getElementById('cmbLOSS_TYPE'),document.getElementById('txtOTHER_DESCRIPTION'),document.getElementById('capOTHER_DESCRIPTION'),0);		  	
		  	}
		  	else
			{
				hideshow(document.getElementById('cmbLOSS_TYPE'),document.getElementById('txtOTHER_DESCRIPTION'),document.getElementById('capOTHER_DESCRIPTION'),1);
			}
		  //	EnableDisableDesc(document.getElementById('cmbLOSS_TYPE'),document.getElementById('txtOTHER_DESCRIPTION'),document.getElementById('capOTHER_DESCRIPTION'));
		  	return false;			
		

		}
		
		function funcValidateLossTypes()
		{var app=document.getElementById('hidText').value;
			if(document.getElementById('cmbLOSS_TYPE').options.length == 0)
			{
				document.getElementById('cmbLOSS_TYPE').className = "MandatoryControl";
				document.getElementById("cmbLOSS_TYPE").style.display="inline";
				document.getElementById("csvLOSS_TYPE").style.display="inline";
				document.getElementById("csvLOSS_TYPE").innerText = app //"Please select Loss Type";
				document.getElementById("csvLOSS_TYPE").isvalid=false;
				return false;
			}
			else
			{
				document.getElementById('cmbLOSS_TYPE').className = "none";
				document.getElementById("csvLOSS_TYPE").isvalid=true;
				return true;
			}
		}
		function resetmultiselect()
		{
			for(j = document.getElementById('cmbLOSS_TYPE').length-1; j >=0;j--)
			{
				if(document.getElementById('cmbLOSS_TYPE').options[j].text.trim()!="" && document.getElementById('cmbLOSS_TYPE').options[j].text!=null)
				{
					addOption(document.getElementById('cmbFROMLOSS_TYPE'), document.getElementById('cmbLOSS_TYPE').options[j].text, document.getElementById('cmbLOSS_TYPE').options[j].value);
				}
				document.getElementById('cmbLOSS_TYPE').options[j]= null;
			}
		}
		function resetmultiselect()
		{
			for(j = document.getElementById('cmbLOSS_TYPE').length-1; j >=0;j--)
			{
				if(document.getElementById('cmbLOSS_TYPE').options[j].text.trim()!="" && document.getElementById('cmbLOSS_TYPE').options[j].text!=null)
				{
					addOption(document.getElementById('cmbFROMLOSS_TYPE'), document.getElementById('cmbLOSS_TYPE').options[j].text, document.getElementById('cmbLOSS_TYPE').options[j].value);
				}
				document.getElementById('cmbLOSS_TYPE').options[j]= null;
			}
		}
		//functions for multi select box :end
			function Init()
			{
				ApplyColor();
				ChangeColor();
				EnableDisableDesc(document.getElementById('cmbLOSS_TYPE'),document.getElementById('txtOTHER_DESCRIPTION'),document.getElementById('capOTHER_DESCRIPTION'));
				if(document.getElementById("txtESTIMATE_AMOUNT"))
					document.getElementById("txtESTIMATE_AMOUNT").value = formatCurrencyWithCents(document.getElementById("txtESTIMATE_AMOUNT").value);				
				document.getElementById("cmbLOSS_TYPE").focus();
			}
		
		//Done for Itrack Issue 6640 on 10 Dec 09
		function Weather_Related_Loss()
		{
			//Done for Itrack Issue 6640 on 18 Dec 09
			if(document.getElementById('hidLOB_ID').value == "1")
			{
				combo = document.getElementById('cmbWeather_Related_Loss');
				if(combo.selectedIndex !=-1 && combo.selectedIndex !=0)
				{
					document.getElementById("rfvWeather_Related_Loss").setAttribute("enabled",false);
					document.getElementById("rfvWeather_Related_Loss").setAttribute("isValid",false);
					document.getElementById('rfvWeather_Related_Loss').style.display ="none";	
					return true;
				}
				else
				{
					document.getElementById("rfvWeather_Related_Loss").setAttribute("enabled",true);
					document.getElementById("rfvWeather_Related_Loss").setAttribute("isValid",true);
					document.getElementById('rfvWeather_Related_Loss').style.display ="inline";	
					return false;
				}
			}
		}
		
		</script>
	
	    <%--Populate the State, City  based on the ZipeCode using jQuery ------- Added by Santosh Kumar Gautam on 25 Nov 2010--%>
    <script language="javascript" type="text/javascript">

        $(document).ready(function(){
          $("#txtLOSS_LOCATION_ZIP").change(function(){
            if (trim($('#txtLOSS_LOCATION_ZIP').val()) != '') 
                {
                   var ZIPCODE = $("#txtLOSS_LOCATION_ZIP").val();
                    var COUNTRYID = "5";
                    PageMethod("GetCustomerAddressDetailsUsingZipeCode", ["ZIPCODE", ZIPCODE, "COUNTRYID", COUNTRYID], AjaxSucceeded, AjaxFailed); //With parameters
                }
                else
                 {                   
                    $("#txtLOSS_LOCATION_CITY").val('');
                 }
          } );
        });
         
        function PageMethod(fn, paramArray, successFn, errorFn) {
            var pagePath = window.location.pathname;
            var paramList = '';
            if (paramArray.length > 0) 
            {
                for (var i = 0; i < paramArray.length; i += 2) 
                {
                    if (paramList.length > 0) paramList += ',';
                    paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';

                }
            }
            paramList = '{' + paramList + '}';
            $.ajax({ type: "POST", url: pagePath + "/" + fn, contentType: "application/json; charset=utf-8",
                data: paramList, dataType: "json", success: successFn, error: errorFn
            });

        }
        function AjaxSucceeded(result) 
        {
           
               var Addresses = result.d;
                 var Addresse = Addresses.split('^');
                   if (result.d != "" && Addresse[1] != 'undefined') 
                   {
                       $("#cmbLOSS_LOCATION_STATE").val(Addresse[1]);
                       $("#txtLOSS_LOCATION_ZIP").val(Addresse[2]);
                       $("#txtLOSS_LOCATION_CITY").val(Addresse[6]) ;                 
                      }
                   else {
                      // alert($("#hidZipeCodeVerificationMsg").val());// commented by avijit goswami for singapore dev
                    //   $("#cmbLOSS_LOCATION_STATE").val(''); // changed by praveer panghal for itrack no 1601 /TFS # 810 
                    //    $("#txtLOSS_LOCATION_ZIP").val(''); // changed by praveer panghal for itrack no 1601 /TFS # 810 
                    //   $("#txtDISTRICT").val('');  // changed by praveer panghal for itrack no 1601 /TFS # 810 
                    //    $("#txtLOSS_LOCATION_CITY").val(''); // changed by praveer panghal for itrack no 1601 /TFS # 810 
                      }
                 
             

          }

        function AjaxFailed(result) {

            //alert(result.d);
        }
        
    </script> 
       <%-- End jQuery Implimentation for ZipeCode --%>
</HEAD>
<BODY oncontextmenu="return false;" leftMargin=0 topMargin=0 onload="Init();">
<FORM id=CLM_OCCURANCE_DETAILS method=post runat="server">
<TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>
  <TR id=trBody runat="server">
    <TD>
      <TABLE width="100%" align=center border=0>
        <TBODY>
        <tr>
          <TD class=pageHeader colSpan=3 align="center"><asp:Label ID="capHeader" runat="server"></asp:Label></TD></TR>
        <tr>
          <td class=midcolorc align=center colSpan=3><asp:label id=lblMessage runat="server" CssClass="errmsg" Visible="False"></asp:label></TD></TR>
        
          <TD class=midcolora width="18%"><asp:label id=capDATE_OF_LOSS runat="server"></asp:label>
              <br />
              <asp:label id=lblDATE_OF_LOSS runat="server"></asp:label></TD>

          <TD class=midcolora width="32%"><asp:label id=capLOSS_TIME runat="server"></asp:label>
              <br />
              <asp:label id=lblLOSS_HOUR runat="server"></asp:label>(HH) <asp:label id=lblLOSS_MIN runat="server"></asp:label>
              (MM) <asp:label id=lblLOSS_AM_PM runat="server"></asp:label><br />
			  
			</TD>
          <TD class=midcolora width="18%">
           <span  class=mandatory id=spnOTHER_DESCRIPTION 
            runat="server">&nbsp;<asp:label id=capOTHER_DESCRIPTION Text="" runat="server"></asp:label>
             
              *</span>
                           
              <br />
             
              <asp:textbox id=txtOTHER_DESCRIPTION runat="server" size="40" maxlength="300"></asp:textbox>
             
              <br />
              
              <asp:requiredfieldvalidator id=rfvOTHER_DESCRIPTION runat="server" Display="Dynamic" ControlToValidate="txtOTHER_DESCRIPTION"></asp:requiredfieldvalidator>
            </TD>
          				
					<tr>
										<TD class="midcolora"><asp:label id="capLOSS_TYPE" runat="server"></asp:label><SPAN class="mandatory">*<br />
											<asp:listbox id="cmbFROMLOSS_TYPE" Runat="server" Height="79px" 
                                                AutoPostBack="False" tabIndex="4">
												<ASP:LISTITEM Value="0"><--------Select--------></ASP:LISTITEM>
											</asp:listbox></SPAN></TD>
										<td class="midcolora" align="left" width="18%">
											<br>
											<asp:button class="clsButton" id="btnSELECT" Runat="server" Text=">>" CausesValidation="True" tabIndex="5"></asp:button><br>
											<br>
											<br>
											<asp:button class="clsButton" id="btnDESELECT" Runat="server" Text="<<" CausesValidation="False" tabIndex="7"></asp:button>
										</td>
										<td class="midcolora" align="left" width="18%">
											<asp:listbox id="cmbLOSS_TYPE" onblur="" Runat="server" Height="79px" AutoPostBack="False"
												SelectionMode="Multiple" tabIndex="6"></asp:listbox>
                                            <br />
											<asp:customvalidator id="csvLOSS_TYPE" Display="Dynamic" ControlToValidate="cmbLOSS_TYPE" Runat="server"
												ClientValidationFunction="funcValidateLossTypes" ErrorMessage="" ></asp:customvalidator>
												<span id="spnLOSS_TYPE" style="DISPLAY: none; COLOR: red"><asp:Label ID="capMessage" runat="server"></asp:Label> 
												</td>
						</tr>  
		      
        <tr>
          <TD class=midcolora width="18%">
             
              <asp:label id=capLOSS_LOCATION runat="server"></span></span></asp:label>
              <br />
              <asp:textbox id=txtLOSS_LOCATION runat="server" onkeypress="MaxLength(this,300);" Width="200" MaxLength="300" Height="50" TextMode="MultiLine"></asp:textbox>
              <br />
				<asp:customvalidator id="csvLOSS_LOCATION" Runat="server" ControlToValidate="txtLOSS_LOCATION" Display="Dynamic"
											ClientValidationFunction="ValidateLossLength" ErrorMessage="Error"></asp:customvalidator>
             
              </TD>
          <TD class=midcolora width="32%" >
				<asp:label id=capDESCRIPTION runat="server"></asp:label>
              <br />
               <asp:textbox id=txtDESCRIPTION runat="server" 
                    Width="299px"  Height="55px" TextMode="MultiLine"></asp:textbox>
               <br />
            </TD>
           <TD class=midcolora width="32%" >
              <asp:label id=capREPORT runat="server"></asp:label>
               <br />
              <asp:textbox id=txtREPORT runat="server" MaxLength="20" Size="25"></asp:textbox>
            </TD> 
           <tr>			
		  <TD class=midcolora width="18%"><asp:label id="capLOSS_LOCATION_ZIP" runat="server"></asp:label>
              <br />
              <asp:textbox id="txtLOSS_LOCATION_ZIP"
                  runat="server" MaxLength="11" Size="25" onkeypress="MaxLength(this,8)" onpaste="MaxLength(this,8)"></asp:textbox>
              <br />
              <asp:regularexpressionvalidator id="revLOSS_LOCATION_ZIP" runat="server" ControlToValidate="txtLOSS_LOCATION_ZIP" ErrorMessage="" Display="Dynamic"></asp:regularexpressionvalidator>
                            
                            <asp:RequiredFieldValidator ID="rfvLOSS_LOCATION_ZIP" 
                  runat="server" ControlToValidate="txtLOSS_LOCATION_ZIP" Display="Dynamic"></asp:RequiredFieldValidator>
               </TD>
          <TD class=midcolora width="32%">
				<asp:label id=capLOSS_LOCATION_CITY runat="server"></asp:label>
              <br />
              <asp:textbox id=txtLOSS_LOCATION_CITY 
                  runat="server" MaxLength="100" Size="25"></asp:textbox>
          </TD>
          <TD class=midcolora width="18%">
			   <asp:label id=capLOSS_LOCATION_STATE runat="server"></asp:label>
               
               <br />
               
              <asp:dropdownlist id="cmbLOSS_LOCATION_STATE" 
                  Runat="server" Enabled="false"></asp:dropdownlist>
                           
                            <br />
                           
                            <asp:RequiredFieldValidator ID="rfvLOSS_LOCATION_STATE" 
                  runat="server" ControlToValidate="cmbLOSS_LOCATION_STATE" 
                  Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
              
               </TD>
         </tr>
          
        <tr>			
		  <TD class=midcolora valign="top"><asp:label id=capAUTHORITY runat="server"></asp:label>
              <br />
              <asp:textbox id=txtAUTHORITY runat="server" MaxLength="100" Size="25"></asp:textbox></TD>
          <TD class=midcolora valign="top"><asp:label id=capVIOLATION runat="server"></asp:label>
              <asp:label id=capESTIMATE_AMOUNT runat="server"></asp:label>
              <br />
              <asp:textbox id=txtVIOLATION runat="server" onkeypress="MaxLength(this,300);" Width="200" MaxLength="300" Height="50" TextMode="MultiLine"></asp:textbox><asp:textbox id=txtESTIMATE_AMOUNT runat="server" size="15" MaxLength="10" CssClass="INPUTCURRENCY"></asp:textbox>
              <br />
			<asp:customvalidator id="csvVIOLATION" Runat="server" ControlToValidate="txtVIOLATION" Display="Dynamic"
											ClientValidationFunction="ValidateViolationLength" ErrorMessage="Error"></asp:customvalidator>
          <asp:regularexpressionvalidator id=revESTIMATE_AMOUNT runat="server" Display="Dynamic" ControlToValidate="txtESTIMATE_AMOUNT"></asp:regularexpressionvalidator>
             
          </TD>
          <TD class=midcolora valign="top">
              <br />
			</TD>
         </tr>
          
        <!-- Added by Charles on 1-Dec-09 for Itrack 6647 -->			
		<tr id=trWaterBackup_SumpPump_Loss runat="server">
		<td class="midcolora" width="18%"><asp:label id="capWaterBackup_SumpPump_Loss" Runat="server"></asp:label></td>
		<td class="midcolora" width="32%"><asp:dropdownlist id="cmbWaterBackup_SumpPump_Loss" Runat="server"></asp:dropdownlist></td>
		<td class="midcolora"></td>		
		</tr>
		<!-- Added till here -->     
		
		<!-- Added for Itrack 6640 on 9 Dec 09 Is this a Weather Related Loss-->			
		<tr id=trWeather_Related_Loss runat="server">
		  <td class="midcolora" width="18%"><asp:label id="capWeather_Related_Loss" Runat="server"></asp:label><span id="spnWeather_Related_Loss" class="mandatory"*></span></td>
		  <td class="midcolora" width="32%"><asp:dropdownlist id="cmbWeather_Related_Loss" Runat="server" onChange="Weather_Related_Loss()"></asp:dropdownlist><br/>
				<asp:requiredfieldvalidator id="rfvWeather_Related_Loss" runat="server" Display="Dynamic" ControlToValidate="cmbWeather_Related_Loss" ErrorMessage="Weather_Related_Loss."></asp:requiredfieldvalidator>
		  </td> 
		  <td class="midcolora">&nbsp;</td>
		</tr>    
        <tr>
          <td class=midcolora colSpan=2><cmsb:cmsbutton class=clsButton id=btnReset runat="server" Text="Reset"></cmsb:cmsbutton></TD>
          <td class=midcolorr><cmsb:cmsbutton class=clsButton id=btnSave runat="server" Text="Save"></cmsb:cmsbutton></TD></TR></TBODY></TABLE></TD></TR></TABLE><INPUT 
id=hidFormSaved type=hidden value=0 name=hidFormSaved 
runat="server"> <INPUT id=hidIS_ACTIVE type=hidden value=0 name=hidIS_ACTIVE runat="server"> <INPUT id=hidOldData type=hidden 
name=hidOldData runat="server"> <INPUT 
id=hidOCCURRENCE_DETAIL_ID type=hidden value=0 name=hidOCCURRENCE_DETAIL_ID runat="server"> <INPUT id=hidLOB_ID type=hidden value=0 
name=hidLOB_ID runat="server"> <INPUT id=hidCLAIM_ID 
type=hidden value=0 name=hidCLAIM_ID runat="server"> 
<INPUT id="hidLOSS_TYPE" type="hidden" name="hidLOSS_TYPE" runat="server">
 <input id="hidZipeCodeVerificationMsg" type="hidden" value="" name="hidZipeCodeVerificationMsg" runat="server"/>
 <INPUT id=hidText Type=hidden  runat="server">
</FORM>
<script>
			//RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidOCCURRENCE_DETAIL_ID').value,false);			
				addLossTypes();
		</script>

	</BODY>
</HTML>
