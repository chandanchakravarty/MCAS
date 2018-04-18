<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page validateRequest=false language="c#" Codebehind="AddBankInformation.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Accounting.AddBankInformation" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>ACT_BANK_INFORMATION</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/jquery/jquery.js" type="text/javascript" ></script>  
        <script src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js" type="text/javascript"></script>  
        <link href="/cms/cmsweb/css/jQRichTextBox.css" type="text/css" rel="Stylesheet" />
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jQRichTextBox.js"></script>
        <script language="javascript" type="text/javascript">
            //Added for itrack-927 by Pradeep Kushwaha
            $(document).ready(function () {
                fnCallWebMethod($("#txtBANK_NAME").val(), "SerachFor", 'Bank', 'BankName', 'txtBANK_NAME');
                fnCallWebMethod($("#txtBANK_NAME").val(), "SerachFor", 'Bank', 'BANKNUMBER', 'txtBANK_NUMBER');
                fnCallWebMethod($("#txtBANK_NAME").val(), "SerachFor", 'Bank', 'BRANCHNUMBER', 'txtBRANCH_NUMBER');
                function fnCallWebMethod(SearchData, SerachFor, CalledFor, Calledfrom, fieldtoPolulate) {
                    var pagePath = window.location.pathname;
                    $.ajax({
                        type: "POST",
                        url: pagePath + "/" + "GetBankDetails",
                        dataType: "json",
                        data: "{'SearchData':'" + SearchData + "','SerachFor':'" + SerachFor + "','CalledFor':'" + CalledFor + "', 'Calledfrom':'" + Calledfrom + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            var datafromServer = data.d;
                            $("#hidBankdetails").val("1");
                            $("#hidBankNumber").val("1");
                            $("#hidBranchNumber").val("1");
                            $("#" + fieldtoPolulate + "").autocomplete(datafromServer);
                        }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                }
                //Commented by Ruchika Chauhan on 24-Jan-2012 for TFS Bug # 846
//                $("#txtBANK_NAME").blur(function () {
//                    var SearchData = $("#txtBANK_NAME").val()
//                    var SerachFor = $("#txtBANK_NAME").val()
//                    var CalledFor = "VALIDATATION";
//                    var Calledfrom = "BANKNAME";
//                    $("#hidCalledFrom").val("BANKNAME");
//                    PageMethod("GetBankDetails", ["SearchData", SearchData, "SerachFor", SerachFor, "CalledFor", CalledFor, "Calledfrom", Calledfrom], AjaxSucceeded, AjaxFailed);
//                });
                $("#txtBANK_NUMBER").blur(function () {
                    var SearchData = $("#txtBANK_NAME").val()
                    var SerachFor = $("#txtBANK_NUMBER").val()
                    var CalledFor = "VALIDATATION";
                    var Calledfrom = "BANKNUMBER";
                    $("#hidCalledFrom").val("BANKNUMBER");
                    PageMethod("GetBankDetails", ["SearchData", SearchData, "SerachFor", SerachFor, "CalledFor", CalledFor, "Calledfrom", Calledfrom], AjaxSucceeded, AjaxFailed);
                });
                //Commented by Ruchika Chauhan on 24-Jan-2012 for TFS Bug # 846
//                $("#txtBRANCH_NUMBER").blur(function () {
//                    var SearchData = $("#txtBANK_NAME").val()
//                    var SerachFor = $("#txtBRANCH_NUMBER").val()
//                    var CalledFor = "VALIDATATION";
//                    var Calledfrom = "BRANCHNUMBER";
//                    $("#hidCalledFrom").val("BRANCHNUMBER");

//                    PageMethod("GetBankDetails", ["SearchData", SearchData, "SerachFor", SerachFor, "CalledFor", CalledFor, "Calledfrom", Calledfrom], AjaxSucceeded, AjaxFailed);
//                });
                function PageMethod(fn, paramArray, successFn, errorFn) {
                    var pagePath = window.location.pathname;
                    var paramList = '';
                    if (paramArray.length > 0) {
                        for (var i = 0; i < paramArray.length; i += 2) {
                            if (paramList.length > 0) paramList += ',';
                            paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
                        }
                    }
                    paramList = '{' + paramList + '}';
                    $.ajax({ type: "POST", url: pagePath + "/" + fn, contentType: "application/json; charset=utf-8", data: paramList, dataType: "json", success: successFn, error: errorFn
                    });
                }
                function AjaxSucceeded(result) {

                    var numbers = result.d;
                    var calledfrom = $("#hidCalledFrom").val();
                    switch (calledfrom) {
                        case "BANKNAME":
                            if (result.d.length > 0) 
                                $("#hidBankdetails").val("1");
                            else  
                               $("#hidBankdetails").val("0");
                            break;
                        case "BANKNUMBER":
                            if (result.d.length > 0) 
                                $("#hidBankNumber").val("1");                             
                            else  
                                $("#hidBankNumber").val("0");
                            break;
                        case "BRANCHNUMBER":
                            if (result.d.length > 0)  
                                $("#hidBranchNumber").val("1");
                            else  
                                $("#hidBranchNumber").val("0");
                            break;
                        default:
                            break;
                    }
                }
                function AjaxFailed(result) { }
            });
            function ValidateBank(objSource, objArgs) {
              
                if (objSource.id == "csvBANK_NAME") {
                    if ($("#hidBankdetails").val() == "0")
                        objArgs.IsValid = false;
                    else
                        objArgs.IsValid = true;
               }
               else if (objSource.id == "csvBANK_NUMBER") {
                    if ($("#hidBankNumber").val() == "0")
                        objArgs.IsValid = false;
                    else
                        objArgs.IsValid = true;
               }
               else {
                   if ($("#hidBranchNumber").val() == "0")
                        objArgs.IsValid = false;
                    else
                        objArgs.IsValid = true;
               }
            }
            //Added till here 
		function AddData()
		{

			DisableValidators();
			document.getElementById('hidGL_ID').value	=	'New';
			document.getElementById("txtSIGN_FILE_1").value = '';
			document.getElementById("txtSIGN_FILE_2").value = '';
			document.getElementById('txtBANK_NAME').value = '';
			//document.getElementById('txtBANK_NAME').focus();
		    document.getElementById('txtBANK_ADDRESS1').value  = '';
			document.getElementById('txtBANK_ADDRESS2').value  = '';
			document.getElementById('txtBANK_CITY').value  = '';
			document.getElementById('cmbBANK_STATE').options.selectedIndex = -1;
			document.getElementById('txtBANK_ZIP').value  = '';
			document.getElementById('txtBANK_ACC_TITLE').value  = '';
			document.getElementById('txtBANK_NUMBER').value  = '';
			document.getElementById('txtSTARTING_DEPOSIT_NUMBER').value  = '';
			document.getElementById('txtROUTE_POSITION_CODE1').value  = '';
			document.getElementById('txtROUTE_POSITION_CODE2').value  = '';
			document.getElementById('txtROUTE_POSITION_CODE3').value  = '';
			document.getElementById('txtROUTE_POSITION_CODE4').value  = '';
			document.getElementById('txtBANK_MICR_CODE').value  = '';
			document.getElementById("txtSIGN_FILE_1").style.display='inline';
			document.getElementById("txtSIGN_FILE_2").style.display='inline';
			document.getElementById("lblSIGN_FILE_1").style.display='inline';
			document.getElementById("lblSIGN_FILE_2").style.display='inline';
			document.getElementById("hidFileInputVisible1").value="Y";
			document.getElementById("hidFileInputVisible2").value="Y";
			document.getElementById("chkIS_CHECK_ISSUED").checked=false;
			EnableDisableVals(document.getElementById("chkIS_CHECK_ISSUED"))
			
			ChangeColor();
		}
		
		function populateXML()
		{
			
			var tempXML = document.getElementById('hidOldData').value;
			
				if(tempXML!="")
				{

				    populateFormData(tempXML, ACT_BANK_INFORMATION);
				    
					var fileName1 = document.getElementById("lblSIGN_FILE_1").innerText;
					var fileName2 = document.getElementById("lblSIGN_FILE_2").innerText;
					var RootPath = document.getElementById('hidRootPath').value;
					var RootPath2 = document.getElementById('hidRootPath2').value; // changed by praveer for TFS# 763
					var AccID = '<%=AccID%>';
					var RowID = document.getElementById("hidGL_ID").value;
					var BankID = document.getElementById("hidBANK_ID").value;
					
					// hidFileInputVisible has been used to determine whether
					// to get Attached File name from label or from textbox
					if(fileName1 != "")
					{
						document.getElementById("txtSIGN_FILE_1").style.display='none';
						document.getElementById("hidFileInputVisible1").value="N";
					}
					else
						document.getElementById("hidFileInputVisible1").value="Y";
					
					
					if(fileName2 != "")	
					{
						document.getElementById("txtSIGN_FILE_2").style.display='none';
						document.getElementById("hidFileInputVisible2").value="N";
					}
					else
						document.getElementById("hidFileInputVisible2").value="Y";
						
				
					document.getElementById("lblSIGN_FILE_1").style.display='inline';
					document.getElementById("lblSIGN_FILE_2").style.display='inline';

					if (AccID != "" && AccID != 0 && RowID != "" && RowID != 0) {
					    document.getElementById("lblSIGN_FILE_1").innerHTML = "<a href = '" + RootPath + "' target='blank'>" + fileName1 + "</a>"; // changed by praveer for TFS# 763
					    document.getElementById("lblSIGN_FILE_2").innerHTML = "<a href = '" + RootPath2 + "' target='blank'>" + fileName2 + "</a>"; // changed by praveer for TFS# 763
					}
					else {
					    document.getElementById("lblSIGN_FILE_1").innerHTML = "<a href = '" + RootPath + "' target='blank'>" + fileName1 + "</a>"; // changed by praveer for TFS# 763
					    document.getElementById("lblSIGN_FILE_2").innerHTML = "<a href = '" + RootPath2 + "' target='blank'>" + fileName2 + "</a>"; // changed by praveer for TFS# 763
					  					
					}

					EnableDisableVals(document.getElementById('chkIS_CHECK_ISSUED'));
				}
				else
				{
					AddData();
				}
				
		//	}
			setTab();
			return false;
		}
		
	function setTab()
	{
	    //alert(document.getElementById('hidCalledFor').value);
	    if (document.getElementById('hidCalledFor').value == 'BANKINFO') return;
	
		var AccID1 = '<%=AccID%>';
		if (document.getElementById('hidOldData').value	!= '')
		{		
			Url="../Maintenance/AttachmentIndex.aspx?EntityType=BankInformation&EntityId="+AccID1+"&";
			DrawTab(3,this.parent,'Attachment',Url,null,1);	
		}
		else
		{							
			//RemoveTab(3,this.parent);			
		}
	}
	
		// Validate the Transit Routing Number
		// First Digit cannot be 5
		function ValidateTranNo(objSource, objArgs)
		{
	
			var tranNum = document.getElementById('txtTRANSIT_ROUTING_NUMBER').value;
			var firstDigit = tranNum.slice(0,1);
			if(firstDigit == "5")
				objArgs.IsValid = false;
			else
				objArgs.IsValid = true;
		}
		// Calculate the Check Digit
		function VerifyTranNo(objSource, objArgs)
		{
			var boolval = ValidateTransitNumber(document.getElementById('txtTRANSIT_ROUTING_NUMBER'));
			if(boolval == false)
			{
				objArgs.IsValid = false;
			} 
		}
		// Length should be 8/9 digits
		function ValidateTranNoLength(objSource, objArgs)
		{
		
			var boolval = ValidateTransitNumberLen(document.getElementById('txtTRANSIT_ROUTING_NUMBER'));
			
			if(boolval == false)
			{
				objArgs.IsValid = false;
			}
			
		}
		
		// Length should be 9 digits
		function ValidateAccountIDLength(objSource, objArgs)
		{
			
			var strAccNum = new String(document.getElementById('txtCOMPANY_ID').value.trim());
				if(isNaN(strAccNum) == false)
				{
				if(strAccNum.length < 9)
					objArgs.IsValid = false;
				}	
		}

		function EnableDisableVals(obj)
		{
			var chkStatus = document.getElementById(obj.id).checked;
			if(chkStatus) // make mandatory
			{
				EnableValidator('rfvSTART_CHECK_NUMBER',true);
				EnableValidator('rfvEND_CHECK_NUMBER',true);
				EnableValidator('rfvBANK_MICR_CODE',true);
				EnableValidator('rfvROUTE_POSITION_CODE1',true);
				EnableValidator('rfvROUTE_POSITION_CODE2',true);
				EnableValidator('rfvROUTE_POSITION_CODE4',true);
				
				ObjectDisplay('spnSTART_CHECK_NUMBER',true);
				ObjectDisplay('spnEND_CHECK_NUMBER',true);
				ObjectDisplay('spnBANK_MICR_CODE',true);
				ObjectDisplay('spnROUTE_POSITION_CODE1',true);
				ObjectDisplay('spnROUTE_POSITION_CODE2',true);
				ObjectDisplay('spnROUTE_POSITION_CODE4',true);
				ApplyColor();ChangeColor();
			}
			else //// make non-mandatory
			{
				EnableValidator('rfvSTART_CHECK_NUMBER',false);
				EnableValidator('rfvEND_CHECK_NUMBER',false);
				EnableValidator('rfvBANK_MICR_CODE',false);
				EnableValidator('rfvROUTE_POSITION_CODE1',false);
				EnableValidator('rfvROUTE_POSITION_CODE2',false);
				EnableValidator('rfvROUTE_POSITION_CODE4',false);
				
				ObjectDisplay('spnSTART_CHECK_NUMBER',false);
				ObjectDisplay('spnEND_CHECK_NUMBER',false);
				ObjectDisplay('spnBANK_MICR_CODE',false);
				ObjectDisplay('spnROUTE_POSITION_CODE1',false);
				ObjectDisplay('spnROUTE_POSITION_CODE2',false);
				ObjectDisplay('spnROUTE_POSITION_CODE4',false);
				
				document.getElementById("txtSTART_CHECK_NUMBER").setAttribute("className","");
				document.getElementById("txtEND_CHECK_NUMBER").setAttribute("className","");
				document.getElementById("txtBANK_MICR_CODE").setAttribute("className","");
				document.getElementById("txtROUTE_POSITION_CODE1").setAttribute("className","");
				document.getElementById("txtROUTE_POSITION_CODE2").setAttribute("className","");
				document.getElementById("txtROUTE_POSITION_CODE4").setAttribute("className","");
				
			}
			
		}
		
		function fnToggleValidators()
		{
			if(document.getElementById('txtCOMPANY_ID').value != "" )
			{
				if(document.getElementById('rfvCOMPANY_ID'))
					document.getElementById('rfvCOMPANY_ID').style.display = 'none';
			}
			if(document.getElementById('txtTRANSIT_ROUTING_NUMBER').value != "")
			{
				if(document.getElementById('rfvTRANSIT_ROUTING_NUMBER'))
					document.getElementById('rfvTRANSIT_ROUTING_NUMBER').style.display = 'none';
			}
			
		}
		
		</script>
</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();ChangeColor();fnToggleValidators();">
		<FORM id="ACT_BANK_INFORMATION" method="post" runat="server">
			<TABLE id="tabBankInfo" runat="server" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4">
								   <asp:Label ID="capMessages" runat="server"></asp:Label> 
								</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4">
								    <asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label>
								</td>
							</tr>
							<tr>
							<TD class="midcolora" width="18%">
								    <asp:label id="capACCOUNT_TYPE" runat="server">Bank Name</asp:label>
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:dropdownlist id="cmbACCOUNT_TYPE" runat="server" 
                                        onselectedindexchanged="cmbACCOUNT_TYPE_SelectedIndexChanged"></asp:dropdownlist><br />
								    <asp:RequiredFieldValidator ID="rfvACCOUNT_TYPE" runat="server" ControlToValidate="cmbACCOUNT_TYPE" Display="Dynamic"></asp:RequiredFieldValidator>
								 </TD>
								<td class="midcolora" width="18%">
								</td>
								<td class="midcolora" width="32%">
								</td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%">
								    <asp:label id="capBANK_NAME" runat="server">Bank Name</asp:label><span class="mandatory">*</span>
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:textbox id="txtBANK_NAME" runat="server" maxlength="100" size="30"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvBANK_NAME" runat="server" Display="Dynamic" ErrorMessage=""	ControlToValidate="txtBANK_NAME"></asp:requiredfieldvalidator><%--BANK_NAME can't be blank.--%>
                                    <asp:customvalidator id="csvBANK_NAME" Runat="server" ClientValidationFunction="ValidateBank" Display="Dynamic" ControlToValidate="txtBANK_NAME"></asp:customvalidator>

								</TD>
								
								<TD class="midcolora" width="18%">
								    <asp:label id="capBANK_NUMBER" runat="server">Bank Number</asp:label>
								    <span class="mandatory" id="spnBANK_NUMBER" runat="server">*</span>
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:textbox id="txtBANK_NUMBER" runat="server" maxlength="25" size="30"></asp:textbox><BR>
								    <asp:requiredfieldvalidator id="rfvBANK_NUMBER" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtBANK_NUMBER"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revBANK_NUMBER" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtBANK_NUMBER"></asp:regularexpressionvalidator><%--BANK_NUMBER can't be blank.--%><%--RegularExpressionValidator--%>
                                    <asp:customvalidator id="csvBANK_NUMBER" Runat="server" ClientValidationFunction="ValidateBank" Display="Dynamic" ControlToValidate="txtBANK_NUMBER"></asp:customvalidator>
								</TD>
							</tr>
							
							
							<tr>
							
							  <td class="midcolora" width="18%">
                        <asp:label id="capBRANCH_NUMBER" runat="server">BRANCH_NUMBER</asp:label><span class="mandatory">*</span>
                        </td>
                        
                        <td class="midcolora" width="32%">
                          <asp:textbox id="txtBRANCH_NUMBER" runat="server"  size="20" MaxLength="20" ></asp:textbox><br />
                          <asp:requiredfieldvalidator id="rfvBRANCH_NUMBER" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtBRANCH_NUMBER"></asp:requiredfieldvalidator>
                          <asp:customvalidator id="csvBRANCH_NUMBER" Runat="server" ClientValidationFunction="ValidateBank" Display="Dynamic" ControlToValidate="txtBRANCH_NUMBER"></asp:customvalidator>
                         
                          </td>
                          <TD class="midcolora" width="18%">
								    <asp:label id="capAGREEMENT_NUMBER" runat="server">AGREEMENT_NUMBER</asp:label><span class="mandatory">*</span>
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:textbox id="txtAGREEMENT_NUMBER" runat="server" maxlength="10" size="30"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvAGREEMENT_NUMBER" runat="server" Display="Dynamic" ErrorMessage=""
										ControlToValidate="txtAGREEMENT_NUMBER"></asp:requiredfieldvalidator>
								</TD>
                          
							</tr>
							<tr>
								<TD class="midcolora" width="18%">
								    <asp:label id="capBANK_ADDRESS1" runat="server">Address1</asp:label>
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:textbox id="txtBANK_ADDRESS1" runat="server" maxlength="145" size="30"></asp:textbox>
								</TD>
								<TD class="midcolora" width="18%">
								    <asp:label id="capBANK_ADDRESS2" runat="server">Compliment</asp:label>
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:textbox id="txtBANK_ADDRESS2" runat="server" maxlength="145" size="30"></asp:textbox>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%">
								    <asp:label id="capBANK_CITY" runat="server">City</asp:label>
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:textbox id="txtBANK_CITY" runat="server" maxlength="35" size="30"></asp:textbox>
								</TD>
								<TD class="midcolora" width="18%">
								    <asp:label id="capBANK_STATE" runat="server">State</asp:label>
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:dropdownlist id="cmbBANK_STATE" onfocus="SelectComboIndex('cmbBANK_STATE')" runat="server">
									    <asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%">
								<asp:label id="capADD_NUMBER" runat="server"></asp:label>
								</TD>
								<TD class="midcolora" width="18%">
								<asp:textbox id="txtADD_NUMBER" runat="server" maxlength="20" size="30"></asp:textbox>
								</TD>
								<TD class="midcolora" width="18%">
								    <asp:label id="capBANK_COUNTRY" runat="server">BANK_COUNTRY</asp:label>
								</TD>
								<TD class="midcolora" width="32%">
								    <%--<asp:textbox id="txtBANK_COUNTRY" runat="server" maxlength="20" size="30"></asp:textbox>--%>
                                    <asp:DropDownList ID="cmbBANK_COUNTRY" runat="server"></asp:DropDownList>
								    
									
								</TD>
							</tr>
							<tr id="ttrBankInformation" runat="server">
								<TD class="midcolora" width="18%" id="ttdBankInfo" runat="server">
								    <asp:label id="capBANK_ZIP" runat="server">Zip</asp:label>
                                    <span id="spnBANK_ZIP" runat="server" class="mandatory">*</span>
								</TD>
								<TD class="midcolora" id="ttdbankZip" runat="server" width="32%">
								    <asp:textbox id="txtBANK_ZIP" runat="server" maxlength="20" size="13"></asp:textbox>
								    <BR>
									<asp:regularexpressionvalidator id="revBANK_ZIP" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtBANK_ZIP"></asp:regularexpressionvalidator><%--RegularExpressionValidator--%>
                                    <asp:RequiredFieldValidator ID="rfvBANK_ZIP" runat="server" Display="Dynamic" ControlToValidate="txtBANK_ZIP"></asp:RequiredFieldValidator>
								</TD>
								<TD class="midcolora" width="18%">
								    <asp:label id="capBANK_ACC_TITLE" runat="server">Account Title</asp:label>
								    <span id="spnBANK_ACC_TITLE" runat="server" class="mandatory">*</span>
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:textbox id="txtBANK_ACC_TITLE" runat="server" maxlength="20" size="30"></asp:textbox>
								    <BR>
									<asp:requiredfieldvalidator id="rfvBANK_ACC_TITLE" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtBANK_ACC_TITLE"></asp:requiredfieldvalidator><%--BANK_ACC_TITLE can't be blank.--%>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%">
								    <asp:label id="capIS_CHECK_ISSUED" runat="server">Will Checks be produced</asp:label>
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:checkbox id="chkIS_CHECK_ISSUED" runat="server" onClick="EnableDisableVals(this);"></asp:checkbox>
								</TD>
								<TD class="midcolora" width="18%">
								    <asp:label id="capSTARTING_DEPOSIT_NUMBER" runat="server">Starting Deposit Number</asp:label>
								    <span id="spnSTARTING_DEPOSIT_NUMBER" runat="server" class="mandatory">*</span>
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:textbox id="txtSTARTING_DEPOSIT_NUMBER" runat="server" maxlength="4" size="30"></asp:textbox>
								    <BR>
								    <asp:requiredfieldvalidator id="rfvSTARTING_DEPOSIT_NUMBER" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtSTARTING_DEPOSIT_NUMBER"></asp:requiredfieldvalidator><%--STARTING_DEPOSIT_NUMBER can't be blank.--%>
								    <asp:regularexpressionvalidator id="revSTARTING_DEPOSIT_NUMBER" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtSTARTING_DEPOSIT_NUMBER"></asp:regularexpressionvalidator><%--RegularExpressionValidator--%>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capSTART_CHECK_NUMBER" runat="server">Start Check Number</asp:label>
								<span class="mandatory" id="spnSTART_CHECK_NUMBER">*</span>
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:textbox id="txtSTART_CHECK_NUMBER" runat="server" maxlength="6" size="30"></asp:textbox>
								    <BR>
								    <asp:requiredfieldvalidator id="rfvSTART_CHECK_NUMBER" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtSTART_CHECK_NUMBER"></asp:requiredfieldvalidator><%--START_CHECK_NUMBER can't be blank.--%>
								    <asp:regularexpressionvalidator id="revSTART_CHECK_NUMBER" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtSTART_CHECK_NUMBER"></asp:regularexpressionvalidator><%--RegularExpressionValidator--%>
								</TD>
								<TD class="midcolora" width="18%">
								    <asp:label id="capEND_CHECK_NUMBER" runat="server">End Check Number</asp:label>
								    <span class="mandatory" id="spnEND_CHECK_NUMBER">*</span>
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:textbox id="txtEND_CHECK_NUMBER" runat="server" maxlength="6" size="30"></asp:textbox>
								    <BR>
								    <asp:requiredfieldvalidator id="rfvEND_CHECK_NUMBER" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtEND_CHECK_NUMBER"></asp:requiredfieldvalidator><%--END_CHECK_NUMBER can't be blank.--%>
								    <asp:regularexpressionvalidator id="revEND_CHECK_NUMBER" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtEND_CHECK_NUMBER"></asp:regularexpressionvalidator><%--RegularExpressionValidator--%>
								</TD>
							<tr>
								<TD class="midcolora" width="18%">
								    <asp:Label ID="capNUMBER" runat="server" ></asp:Label>
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:TextBox ID="txtNUMBER" runat="server" size="30" MaxLength="20"></asp:TextBox>
								</TD>
								<TD class="midcolora" width="18%">
								    <asp:Label ID="capREGISTERED" runat="server"></asp:Label> 
								</TD>
								<TD class="midcolora" width="32%">
								     <asp:dropdownlist id="cmbREGISTERED" runat="server" Height="16px">
									    <%--<asp:ListItem Value='1'>Yes</asp:ListItem>
									     <asp:ListItem Value="0">No</asp:ListItem>--%>
									</asp:dropdownlist>
								</TD>
								<tr>
								<TD class="midcolora" width="18%">
								    <asp:Label ID="capSTARTING_OUR_NUMBER" runat="server"></asp:Label>
								</TD>
								<TD class="midcolora" width="32%">
								  <asp:TextBox ID="txtSTARTING_OUR_NUMBER" runat="server" size="30" MaxLength="15"></asp:TextBox></br>
								  <asp:RegularExpressionValidator ID="revSTARTING_OUR_NUMBER" Display="Dynamic" runat="server" ControlToValidate="txtSTARTING_OUR_NUMBER" ErrorMessage=""></asp:RegularExpressionValidator> 
								</TD>
								<TD class="midcolora" width="18%">
								    <asp:Label ID="capENDING_OUR_NUMBER" runat="server"></asp:Label> 
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:TextBox ID="txtENDING_OUR_NUMBER" runat="server" size="30" MaxLength="15"></asp:TextBox></br>
								    <asp:RegularExpressionValidator ID="revENDING_OUR_NUMBER" Display="Dynamic" runat="server" ControlToValidate="txtENDING_OUR_NUMBER" ErrorMessage=""></asp:RegularExpressionValidator>
								</TD>
                                <tr>
                                <TD class="midcolora" width="18%">  <%--added by aditya for itrack # 1505 on 08-8-2011--%>
								    <asp:label id="capBANK_TYPE" runat="server">Bank Type</asp:label>
								</TD>
                                <TD class="midcolora" colSpan="3">
								    <asp:dropdownlist id="cmbBANK_TYPE" runat="server" ></asp:dropdownlist><br />
								   
								 </TD>

                                </tr>

							<tr>
								<TD class="headerEffectSystemParams" colSpan="4"><asp:Label ID="capBNKRUTNO" runat="server"></asp:Label>
							
							</TD><%--Bank Route Number--%>
								<TD class="headerEffectSystemParams"></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%">
								<asp:label id="capROUTE_POSITION_CODE1" runat="server">Position Code 1</asp:label>
								    <span class="mandatory" id="spnROUTE_POSITION_CODE1">*</span>
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtROUTE_POSITION_CODE1" runat="server" maxlength="5" size="30"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvROUTE_POSITION_CODE1" runat="server" Display="Dynamic" ErrorMessage=""
										ControlToValidate="txtROUTE_POSITION_CODE1"></asp:requiredfieldvalidator></TD><%--Please enter Position Code 1.--%>
								<TD class="midcolora" width="18%">
								    <asp:label id="capROUTE_POSITION_CODE2" runat="server">Position Code 2</asp:label>
								    <span class="mandatory" id="spnROUTE_POSITION_CODE2">*</span></TD>
								<TD class="midcolora" width="32%">
								    <asp:textbox id="txtROUTE_POSITION_CODE2" runat="server" maxlength="5" size="30"></asp:textbox>
								    <BR>
									<asp:requiredfieldvalidator id="rfvROUTE_POSITION_CODE2" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtROUTE_POSITION_CODE2"></asp:requiredfieldvalidator><%--Please enter Position Code 2.--%>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%">
								    <asp:label id="capROUTE_POSITION_CODE3" runat="server">Position Code 3</asp:label>
								    </TD>
								<TD class="midcolora" width="32%">
								    <asp:textbox id="txtROUTE_POSITION_CODE3" runat="server" maxlength="5" size="30"></asp:textbox>
								</TD>
								<TD class="midcolora" width="18%">
								    <asp:label id="capROUTE_POSITION_CODE4" runat="server">Position Code 4</asp:label><span class="mandatory" id="spnROUTE_POSITION_CODE4">*</span></TD>
								<TD class="midcolora" width="32%">
								    <asp:textbox id="txtROUTE_POSITION_CODE4" runat="server" maxlength="10" size="30"></asp:textbox>
								    <BR>
									<asp:requiredfieldvalidator id="rfvROUTE_POSITION_CODE4" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtROUTE_POSITION_CODE4"></asp:requiredfieldvalidator><%--Please enter Position Code 4.--%>
								</TD>
							</tr>
							<TR>
								<TD class="midcolora" width="18%">
								    <asp:label id="capBANK_MICR_CODE" runat="server">Bank MICR Code</asp:label>
								    <span class="mandatory" id="spnBANK_MICR_CODE">*</span>
								 </TD>
								 <TD class="midcolora" width="32%" colSpan="3">
								    <asp:textbox id="txtBANK_MICR_CODE" runat="server" maxlength="19" size="30"></asp:textbox>
								    <BR>
									    <asp:requiredfieldvalidator id="rfvBANK_MICR_CODE" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtBANK_MICR_CODE"></asp:requiredfieldvalidator><%--Please enter Bank MICR Code.--%>
									    <asp:regularexpressionvalidator id="revBANK_MICR_CODE" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtBANK_MICR_CODE"></asp:regularexpressionvalidator><%--RegularExpressionValidator--%>
							    </TD>
							<tr>
								<td class="midcolora" width="25%">
								<asp:label id="capSIGN_FILE_1" runat="server"></asp:label>
								</td>
								<td class="midcolora" width="25%" colSpan="3">
								    <input id="txtSIGN_FILE_1" type="file" size="50" runat="server" />
									<asp:label id="lblSIGN_FILE_1" Runat="server"></asp:label>
							    </td>
							</tr>
							<tr>
								<td class="midcolora" width="25%">
								    <asp:label id="capSIGN_FILE_2" runat="server">Signature File 2</asp:label>
								</td>
								<td class="midcolora" width="25%" colSpan="3">
								    <input id="txtSIGN_FILE_2" type="file" size="50" runat="server">
									<asp:label id="lblSIGN_FILE_2" Runat="server"></asp:label>
							    </td>
							</tr>
							<tr>
								<TD class="headerEffectSystemParams" colSpan="4"><asp:Label ID="capEFTINFO" runat="server"></asp:Label></TD><%--EFT Information--%>
								<TD class="headerEffectSystemParams"></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%">
								    <asp:label id="capCOMPANY_ID" runat="server">Account ID</asp:label>
								    <span class="mandatory" id="spnCOMPANY_ID" runat="server">*</span>
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:textbox id="txtCOMPANY_ID" runat="server" maxlength="9" size="11"></asp:textbox>
								    <BR>
								    <asp:requiredfieldvalidator id="rfvCOMPANY_ID" runat="server" Display="Dynamic" Enabled="False" ErrorMessage="" ControlToValidate="txtCOMPANY_ID"></asp:requiredfieldvalidator><%--Please enter Immediate Origin (Federal ID or ID provided by Bank).--%>
								    <br>
									<%--<asp:regularexpressionvalidator id="revCOMPANY_ID" Display="Dynamic" ControlToValidate="txtCOMPANY_ID" Runat="server"></asp:regularexpressionvalidator>--%>
									<asp:regularexpressionvalidator id="revCOMPANY_ID" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtCOMPANY_ID"></asp:regularexpressionvalidator><%--RegularExpressionValidator--%>
									<asp:customvalidator id="csvCOMPANY_ID" Runat="server" ClientValidationFunction="ValidateAccountIDLength" ErrorMessage="Length has to be exactly 9 digits." Display="Dynamic" ControlToValidate="txtCOMPANY_ID"></asp:customvalidator>
								</TD>
									<%--<asp:CustomValidator ID="csvCOMPANY_ID" Runat="server" ClientValidationFunction="ValidateAccountIDLength" ControlToValidate="txtCOMPANY_ID" ErrorMessage="Length has to be exactly 9 digits." Display="Dynamic"></asp:CustomValidator></TD>--%>
								<TD class="midcolora" width="18%">
								    <asp:label id="capTRANSIT_ROUTING_NUMBER" runat="server">Transit Routing Number</asp:label>
								    <span class="mandatory" id="spnTRANSIT_ROUTING_NUMBER" runat="server">*</span>
								</TD>
								<TD class="midcolora" width="32%">
								<asp:textbox id="txtTRANSIT_ROUTING_NUMBER" runat="server" maxlength="9" size="12"></asp:textbox>
								<BR>
									<asp:requiredfieldvalidator id="rfvTRANSIT_ROUTING_NUMBER" runat="server" Display="Dynamic" ErrorMessage="Please enter Immediate Destination (Routing and Transit Identification Number of Bank)." ControlToValidate="txtTRANSIT_ROUTING_NUMBER" Enabled="False"></asp:requiredfieldvalidator>
									<asp:customvalidator id="csvTRANSIT_ROUTING_NUMBER" Runat="server" ClientValidationFunction="ValidateTranNo" ErrorMessage="Number starting with 5 is invalid." Display="Dynamic" ControlToValidate="txtTRANSIT_ROUTING_NUMBER"></asp:customvalidator>
									<asp:customvalidator id="csvVERIFY_TRANSIT_ROUTING_NUMBER" Runat="server" ClientValidationFunction="VerifyTranNo" ErrorMessage="Please Verify the 9th Digit (Check digit)." Display="Dynamic" ControlToValidate="txtTRANSIT_ROUTING_NUMBER"></asp:customvalidator>
									<asp:customvalidator id="csvVERIFY_TRANSIT_ROUTING_NUMBER_LENGHT" Runat="server" ClientValidationFunction="ValidateTranNoLength" ErrorMessage="Length has to be exactly 8/9 digits." Display="Dynamic" ControlToValidate="txtTRANSIT_ROUTING_NUMBER"></asp:customvalidator>
									<asp:regularexpressionvalidator id="revTRANSIT_ROUTING_NUMBER" Runat="server" ErrorMessage="Please Enter Valid Transit Number."	Display="Dynamic" ControlToValidate="txtTRANSIT_ROUTING_NUMBER"></asp:regularexpressionvalidator>
								</TD>
							</tr>
							<tr>
								<td class="midcolora" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></td>
								<td class="midcolora" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnResetSeries" runat="server" Text="Reset Check Number Series" Visible="false" ></cmsb:cmsbutton></td>
								<td class="midcolora" colSpan="1"></td>
								<td class="midcolorr" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<INPUT id="hidGL_ID" type="hidden" name="hidGL_ID" runat="server"> <INPUT id="hidRootPath" type="hidden" runat="server" NAME="hidRootPath">
            <INPUT id="hidRootPath2" type="hidden" runat="server" NAME="hidRootPath2"> <%--changed by praveer for TFS# 763--%>
            <INPUT id="hidRootPathMain" type="hidden" runat="server" NAME="hidRootPathMain"> <%--changed by praveer for TFS# 763--%>
			<INPUT id="hidFileInputVisible1" name="hidFileInputVisible1" type="hidden"> 
			<INPUT id="hidFileInputVisible2" name="hidFileInputVisible2" type="hidden">
			<INPUT id="hidCalledFor" name="hidCalledFor" runat="server" type="hidden">
			<INPUT id="hidBANK_ID" name="hidBANK_ID" runat="server" type="hidden">
            <input id="hidBankdetails" name="hidBankdetails" value="0" runat="server" type="hidden"/>
            <input id="hidBankNumber" name="hidBankNumber" value="0" runat="server" type="hidden"/>
            <input id="hidBranchNumber" name="hidBranchNumber" value="0" runat="server" type="hidden"/>
            <input id="hidCalledFrom" name="hidCalledFrom" value="" runat="server" type="hidden"/>
		</FORM>
		 <script type="text/javascript" >
		     if (document.getElementById('hidFormSaved').value == "1") {
		         try {
		             if (document.getElementById('hidCalledFor').value == "BANKINFO")
		                RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidBANK_ID').value);
		              
		         }
		         catch (err) {

		         }


		     }
		</script>
	</BODY>
</HTML>
    