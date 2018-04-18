<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>

<%@ Page ValidateRequest="false" Language="c#" CodeBehind="AddRegCommSetup_Agency.aspx.cs"
    AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Accounting.AddRegCommSetup_Agency" %>

<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<html>
<head>
    <title>Regular Commission Setup - Agency</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet">


    <script src="/cms/cmsweb/scripts/xmldom.js"></script>

    <script src="/cms/cmsweb/scripts/common.js"></script>

    <script src="/cms/cmsweb/scripts/form.js"></script>

    <script src="/cms/cmsweb/scripts/Calendar.js"></script>

    <!-- For JQuery -->

    <script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>

    <script language="javascript">
		//date comparision
		var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
		var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		
		var State_id ;
		var SUB_LOB_IDId;
		var LOBId ;
		function AddData()
		{
			DisableValidators();
			document.getElementById('hidCOMM_ID').value	=	'New';

			document.getElementById('btnCopy').setAttribute('disabled',false);
				
			if("<%= Request.QueryString["COMMISSION_TYPE"]%>"!="A")
			{
				document.getElementById('agencyRow').style.display="none";
				/*try
				{
					if(document.getElementById('cmbSTATE_ID'))
						document.getElementById('cmbSTATE_ID').focus();
				}
				catch(e)
				{
					return false;
				}*/
			}
			else
			//	document.getElementById('cmbAGENCY_ID').focus();

			//document.getElementById('cmbSTATE_ID').options.selectedIndex = -1;
			//document.getElementById('cmbSTATE_ID').options.selectedIndex = -1;
			document.getElementById('cmbLOB_ID').options.selectedIndex = -1;
			document.getElementById('cmbSUB_LOB_ID').options.selectedIndex = -1;
			document.getElementById('cmbCLASS_RISK').options.selectedIndex = -1;
			document.getElementById('cmbTERM').options.selectedIndex = -1;
			document.getElementById('txtEFFECTIVE_FROM_DATE').value  = '';
			document.getElementById('txtEFFECTIVE_TO_DATE').value  = '';
			document.getElementById('txtCOMMISSION_PERCENT').value  = '';
			document.getElementById('txtREMARKS').value  = '';
			if(document.getElementById('btnActivateDeactivate'))
			document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			ChangeColor();
		}
		function ResetPage()
		{
			parent.changeTab(0, 0);
			//ResetForm('ACT_REG_COMM_SETUP');
			//populateXML();FillSubLOB();populateXML();ChangeColor();
			//return false;
		}
		function populateXML()
		{ 
			document.getElementById('btnCopy').setAttribute('disabled',true);
			if("<%= Request.QueryString["COMMISSION_TYPE"]%>"!="A")
			{
				document.getElementById('agencyRow').style.display="none";
			}
			var tempXML = document.getElementById('hidOldData').value;
			
			
			if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
			{
				if(tempXML!="")
				{
				  //alert('tempXML' +tempXML);
					populateFormData(tempXML,ACT_REG_COMM_SETUP);
					 for(i=0;i<document.getElementById('cmbCOUNTRY_ID').options.length;i++)
					{
						if(document.getElementById('cmbCOUNTRY_ID').options[i].value==document.getElementById('hidCOUNTRY_ID').value)	
							document.getElementById('cmbCOUNTRY_ID').options[i].selected=true;
					}
					setTimeout("selectedstate()",800);
//					for(i=0;i<document.getElementById('cmbSTATE_ID').options.length;i++)
//					{
//						if(document.getElementById('cmbSTATE_ID').options[i].value==document.getElementById('hidState').value)	
//							document.getElementById('cmbSTATE_ID').options[i].selected=true;
//					}
//					
//				  
					
				
					//alert(document.getElementById('cmbSUB_LOB_ID').options[document.getElementById('cmbSUB_LOB_ID').selectedIndex].value);									
				}
				else
				{
					AddData();
				}
			}
			
			 else if (document.getElementById('hidFormSaved').value == '2')
			 {
			    setTimeout("selectedstate()",800);
			    if(document.getElementById('btnActivateDeactivate'))
			document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			 }
			else
			{
				if(document.getElementById('hidCOMM_ID').value	== 'New')
					document.getElementById('btnCopy').setAttribute('disabled',false);
                //Added By Raghav Deactivted the button after saving the record
				if(document.getElementById('btnActivateDeactivate'))
					document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
							
				SelectComboOption("cmbSUB_LOB_ID",document.getElementById('hidSUB_LOB_ID').value)
			}
			return false;
		}

		function Validate(objSource , objArgs)
		{	
			var comm = parseFloat(document.getElementById('txtCOMMISSION_PERCENT').value);
			if(comm < 0 || comm > 100)
			{
			//	alert("Commission percent must be between 0 - 100.000 range.");
				document.getElementById('txtCOMMISSION_PERCENT').select();
				objArgs.IsValid=false;
			}
			else
				objArgs.IsValid=true;
		}

		  function setSubLobBlank() {

            
            if ((document.getElementById('cmbSTATE_ID').value) == "") {
               
                document.getElementById('cmbLOB_ID').innerHTML = "";
               
            }
             document.getElementById('cmbSUB_LOB_ID').innerHTML = "";
            
        }
		function FillSubLOB()
		{
		
		
			if("<%= Request.QueryString["COMMISSION_TYPE"]%>"!="P")
			{
			var stID="";
			document.getElementById('cmbSUB_LOB_ID').innerHTML = '';
			var Xml = document.getElementById('hidLOBXML').value;
			//Balank check Added For Itrack #5235
			if(Xml!="")
			{
			//var LOBId = document.getElementById('cmbLOB_ID').options[document.getElementById('cmbLOB_ID').selectedIndex].value;
			
			//alert('2' + document.getElementById('cmbSTATE_ID').selectedIndex);
			//LOB id is not selected then returning 
			if(document.getElementById('cmbLOB_ID').selectedIndex == -1)
			{
				document.getElementById('hidSUB_LOB_ID').value = '';
				return false;
			}
			if(document.getElementById('cmbLOB_ID').options[document.getElementById('cmbLOB_ID').selectedIndex].value!="")
			{
				LOBId = document.getElementById('cmbLOB_ID').options[document.getElementById('cmbLOB_ID').selectedIndex].value;
			}
			stID = document.getElementById('hidState').value;
			//stID	= document.getElementById('cmbSTATE_ID').options[document.getElementById('cmbSTATE_ID').selectedIndex].value;
			//Inserting the lobid in hidden control
			//document.getElementById('hidLOBId').value = document.getElementById('cmbLOB_ID').options[document.getElementById('cmbLOB_ID').selectedIndex].value;
		
			var objXmlHandler = new XMLHandler();
			var tree = objXmlHandler.quickParseXML(Xml).childNodes[0];
			//adding a blank option
			oOption = document.createElement("option");
			oOption.value = "";
			oOption.text = "";
			document.getElementById('cmbSUB_LOB_ID').add(oOption);
				
			//adding a all option
			oOption = document.createElement("option");
			oOption.value = "0";
			oOption.text = "All";
			document.getElementById('cmbSUB_LOB_ID').add(oOption);
			
		/*	if(document.getElementById('cmbLOB_ID').selectedIndex==1)
			{
				document.getElementById('cmbCLASS_RISK').selectedIndex = 0;
				document.getElementById('cmbCLASS_RISK').disabled = true;
				return;
			}
			else
			{
				document.getElementById('cmbCLASS_RISK').disabled = false;
			}
		*/
			for(i=0; i<tree.childNodes.length; i++)
			{
				nodValue = tree.childNodes[i].getElementsByTagName('LOB_ID');
				stateValue = tree.childNodes[i].getElementsByTagName('STATE_ID');
				if (nodValue != null)
				{
					if (nodValue[0].firstChild == null)
						continue;
					
					if (stateValue[0].firstChild == null)
						continue;

					//alert(stID);
					//alert(document.getElementById('cmbSTATE_ID').selectedIndex);
					if(stID!="0")
					{						
					if (nodValue[0].firstChild.text == LOBId && stateValue[0].firstChild.text==stID)
					{
						
						SubLobId = tree.childNodes[i].getElementsByTagName('SUB_LOB_ID');
						SubLobDesc = tree.childNodes[i].getElementsByTagName('SUB_LOB_DESC');
						//alert(SubLobId + ' ' +SubLobDesc);
						if (SubLobId != null && SubLobDesc != null)
						{
							if ((SubLobId[0] != null || SubLobId[0] == 'undefined' ) 
								&&  (SubLobDesc[0] != null || SubLobDesc[0] == 'undefined'))
							{
						
								oOption = document.createElement("option");
								oOption.value = SubLobId[0].firstChild.text;
								oOption.text = SubLobDesc[0].firstChild.text;
								document.getElementById('cmbSUB_LOB_ID').add(oOption);
								
							}
						}
					}
					}
					else
					{
					
						if (nodValue[0].firstChild.text == LOBId)
						{
						SubLobId = tree.childNodes[i].getElementsByTagName('SUB_LOB_ID');
						SubLobDesc = tree.childNodes[i].getElementsByTagName('SUB_LOB_DESC');
						
						if (SubLobId != null && SubLobDesc != null)
						{
							if ((SubLobId[0] != null || SubLobId[0] == 'undefined' ) 
								&&  (SubLobDesc[0] != null || SubLobDesc[0] == 'undefined'))
							{
								
								oOption = document.createElement("option");
								oOption.value = SubLobId[0].firstChild.text;
								oOption.text = SubLobDesc[0].firstChild.text;
								document.getElementById('cmbSUB_LOB_ID').add(oOption);
								
							}
						}
						}
					}
				}
			}
			}
			if(document.getElementById('cmbLOB_ID').options[document.getElementById('cmbLOB_ID').selectedIndex].value!="")
				document.getElementById('hidLOB_ID').value = document.getElementById('cmbLOB_ID').options[document.getElementById('cmbLOB_ID').selectedIndex].value;
			//document.getElementById('cmbSUB_LOB_ID').selectedIndex=-1;
			PopulateClassDropDown();
			}		 
		}
		 //Parse Class Xml To Populate Class Drop Down On The Bases Of LOB and Sub Lob
		function PopulateClassDropDown()
		{
		    document.getElementById('cmbCLASS_RISK').length=0;
			var xmlDoc = new ActiveXObject("Microsoft.XMLDOM") ;
			//xmlDoc.async=false;
            
			var strXML=document.getElementById('hidClass').value;
//			if(document.getElementById('cmbSTATE_ID').options[document.getElementById('cmbSTATE_ID').selectedIndex].value!=null)
//			{
//				State_id = document.getElementById('cmbSTATE_ID').options[document.getElementById('cmbSTATE_ID').selectedIndex].value;
//			}
            if(document.getElementById('hidState').value!="")
            {
                State_id = document.getElementById('hidState').value;
            }
			
			if(State_id == '') return;
			
			if(document.getElementById('cmbLOB_ID').options.selectedIndex == -1) return;
			if(document.getElementById('cmbLOB_ID').options[document.getElementById('cmbLOB_ID').selectedIndex].value!="")
			{
				LOBId = document.getElementById('cmbLOB_ID').options[document.getElementById('cmbLOB_ID').selectedIndex].value;
			}
		
				if(document.getElementById('cmbSUB_LOB_ID').selectedIndex==-1)
				{
					SUB_LOB_IDId =0;
				}
				else if(document.getElementById('cmbSUB_LOB_ID').options[document.getElementById('cmbSUB_LOB_ID').selectedIndex].value!=null)
				{
					SUB_LOB_IDId = document.getElementById('cmbSUB_LOB_ID').options[document.getElementById('cmbSUB_LOB_ID').selectedIndex].value;
				}
				if(document.getElementById('hidSUB_LOB_ID').value!="")
						SUB_LOB_IDId =document.getElementById('hidSUB_LOB_ID').value;
    				//alert('SLOB : ' + parseInt(SUB_LOB_IDId));
	    		
			xmlDoc.loadXML(strXML);
			xmlTableNodes = xmlDoc.selectNodes('NewDataSet/row[@LOB_ID=' +  parseInt(LOBId) +' and @SUB_LOB_ID=' + parseInt(SUB_LOB_IDId) +' and @STATE_ID=' + parseInt(State_id)+ ']' );
			for(var i = 0; i < xmlTableNodes.length; i++ )
			{			 
				var text = 	xmlTableNodes[i].attributes[4].value;
				var value = 	xmlTableNodes[i].attributes[3].value;
				document.getElementById('cmbCLASS_RISK').options[document.getElementById('cmbCLASS_RISK').length]= new Option(text,value);
			}
			//setHidClass();		
			FillClassDropDown();
		}
		function FillClassDropDown()
		{
		   SelectComboOption('cmbCLASS_RISK',document.getElementById('hidSelectedClass').value);
		}
		function setHidSubLob()
		{
			if(document.getElementById('cmbSUB_LOB_ID').options[document.getElementById('cmbSUB_LOB_ID').selectedIndex].value!="")
				document.getElementById('hidSUB_LOB_ID').value = document.getElementById('cmbSUB_LOB_ID').options[document.getElementById('cmbSUB_LOB_ID').selectedIndex].value;
			
			PopulateClassDropDown();
			//setHidClass();
		}
		//Set Hidden Field for Class variable
		function setHidClass()
		{
			//if(document.getElementById('cmbCLASS_RISK').options[document.getElementById('cmbCLASS_RISK').selectedIndex].value!="")
				//document.getElementById('hidSelectedClass').value = document.getElementById('cmbCLASS_RISK').options[document.getElementById('cmbCLASS_RISK').selectedIndex].value;
		}
		
		function ChkDate(objSource , objArgs)
		{
			var fromDate=document.getElementById('txtEFFECTIVE_FROM_DATE').value;				
			var toDate=document.getElementById('txtEFFECTIVE_TO_DATE').value;
			objArgs.IsValid = DateComparer(toDate,fromDate, jsaAppDtFormat);
		}	
			
	
		function showPageLookupLayer(controlId)
			{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbCLASS_RISK":
						lookupMessage	=	"DRTCD.";
						break;
					default:
						lookupMessage	=	"Look up code not found";
						break;
						
				}
				showLookupLayer(controlId,lookupMessage);							
			}
			
		function OpenLookup(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args)
		{
			var win = window.open(Url + '?DataValueField=' + DataValueField + '&DataTextField=' + 
								DataTextField + '&DataValueFieldID=' + DataValueFieldID + 
								'&DataTextFieldID=' + DataTextFieldID + '&LookupCode=' + LookupCode + 
								'&Args='+ "",
								'review','height=500, width=500,status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no' )
			
		}
		
		function splitRegularCommission()
		{
				var RegularCommissionNumber = document.getElementById('hidREGULAR_COMMISSION_ID').value.split('~');
			
		}	
		function CopyRecords()
		{
			//window.open('CopyCommission.aspx?COMMISSION_TYPE=R','CopyCommission','800','800','Yes','Yes','No','No','No');
			window.open('CopyCommission.aspx?COMMISSION_TYPE=<%=strCalledFrom%>','CopyCommission',"width=800,height=600,screenX=50,screenY=150,top=80,left=80,scrollbars=yes,resizable=no,menubar=no,toolbar=no,status=no","");
			return false;
		}	
			
		function formReset()
		{
			document.location.href = document.location.href; 
			return false;
		}
		
		function selectedstate()
		{
		var CountryID = document.getElementById('hidCOUNTRY_ID').value;
		
	
            if(document.getElementById('hidState').value!="")
            {
				SelectComboOption('cmbSTATE_ID',document.getElementById('hidState').value);  
				}
				if (document.getElementById('hidSaveLob').value != "") {
                SelectComboOption('cmbLOB_ID', document.getElementById('hidSaveLob').value);
                }  
               
                 if (document.getElementById('hidSaveSLob').value != "")
                 {
                SelectComboOption('cmbSUB_LOB_ID', document.getElementById('hidSaveSLob').value);
                }
                 PopulateClassDropDown();
                 if(document.getElementById('hidSaveClass').value!="")
                 {
				SelectComboOption('cmbCLASS_RISK',document.getElementById('hidSaveClass').value);
				}
                
			
		}
		
		

    </script>

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {


            // put all your jQuery goodness in here.
            var sysID = '<%=GetSystemId() %>'
            if ((sysID.toString().toUpperCase() != "S001") && (sysID.toString().toUpperCase() != "SUAT")) {



                if (document.getElementById("hidState").value != '') {
                    //var Parameters = "{'Param':'" + document.getElementById("hidState").value + "'}";
                    var STATE_ID = document.getElementById("hidState").value;
                    CallAJAX("AddRegCommSetup_Agency.aspx/GetSubLOBs", ["STATE_ID", STATE_ID], "outputDTSUBLOB", "ShowError", "#cmbLOB_ID", "LOB_ID", "LOB_DESC");

                }

                if (document.getElementById("hidCOUNTRY_ID").value != '') {
                    //var Parameters = "{'Param':'" + document.getElementById("hidCOUNTRY_ID").value + "'}";
                    var COUNTRY_ID = document.getElementById("hidCOUNTRY_ID").value;
                    CallAJAX("AddRegCommSetup_Agency.aspx/AjaxFillState", ["COUNTRY_ID", COUNTRY_ID], "outputDTSUBLOB", "ShowError", "#cmbSTATE_ID", "STATE_ID", "STATE_NAME");

                }

                if (document.getElementById("hidState").value != '' && document.getElementById("hidSaveLob").value != '') {
                    //var Parameters = "{'Param':'" + document.getElementById("hidCOUNTRY_ID").value + "'}";
                    var LOB_ID = document.getElementById("hidSaveLob").value;
                    var STATE_ID = document.getElementById("hidState").value;
                    CallAJAX("AddRegCommSetup_Agency.aspx/GetSubSubLOBs", ["STATE_ID", STATE_ID, "LOB_ID", LOB_ID], "outputDTSUBLOB", "ShowError", "#cmbSUB_LOB_ID", "SUB_LOB_ID", "SUB_LOB_DESC");

                }
                //setTimeout("copyfunctions()", 100);

                $("#cmbSTATE_ID").change(function () {
                    document.getElementById("hidState").value = this.value;
                    //var Parameters = "{'Param':'" + document.getElementById("cmbSTATE_ID").value + "'}";
                    var STATE_ID = document.getElementById("hidState").value;
                    CallAJAX("AddRegCommSetup_Agency.aspx/GetSubLOBs", ["STATE_ID", STATE_ID], "outputDTSUBLOB", "ShowError", "#cmbLOB_ID", "LOB_ID", "LOB_DESC");
                })

                $("#cmbCOUNTRY_ID").change(function () {
                    document.getElementById("hidCOUNTRY_ID").value = this.value;
                    // var Parameters = "{'Param':'" + document.getElementById("cmbCOUNTRY_ID").value + "'}";
                    var COUNTRY_ID = document.getElementById("hidCOUNTRY_ID").value;
                    CallAJAX("AddRegCommSetup_Agency.aspx/AjaxFillState", ["COUNTRY_ID", COUNTRY_ID], "outputDTSUBLOB", "ShowError", "#cmbSTATE_ID", "STATE_ID", "STATE_NAME");
                })

                $("#cmbLOB_ID").change(function () {
                    document.getElementById("hidSaveLob").value = this.value;
                    // var Parameters = "{'Param':'" + document.getElementById("cmbCOUNTRY_ID").value + "'}";
                    var LOB_ID = document.getElementById("hidSaveLob").value;
                    var STATE_ID = document.getElementById("hidState").value;
                    CallAJAX("AddRegCommSetup_Agency.aspx/GetSubSubLOBs", ["STATE_ID", STATE_ID, "LOB_ID", LOB_ID], "outputDTSUBLOB", "ShowError", "#cmbSUB_LOB_ID", "SUB_LOB_ID", "SUB_LOB_DESC");
                })

            }
        });




        //sET sLOB	
        //alert('SUB : ' + document.getElementById('hidSaveSLob').value);

        function CallAJAX(ServerMethod, paramArray, SuccessMethod, ErrorMethod, ControlID, ItemValue, ItemText) {

            var paramList = '';
            if (paramArray.length > 0) {
                for (var i = 0; i < paramArray.length; i += 2) {
                    if (paramList.length > 0) paramList += ',';
                    paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';

                }
            }
            paramList = '{' + paramList + '}';

            $.ajax({

                type: "POST",
                url: ServerMethod,
                data: paramList,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: function(xhr, status, errorThrown) {
                    Error = xhr;
                    eval(ErrorMethod + "()");
                },
                success: function(msg) {
                    Result = msg;
                    var rr = SuccessMethod + "('" + ControlID + "','" + ItemValue + "','" + ItemText + "');";
                    eval(rr);
                    //eval(SuccessMethod + "()");//without parameter
                }
            });

        }

        function outputDT(ControlID, ItemValue, ItemText) {
            BindDropdown(ControlID, ItemValue, ItemText);

        }

        function outputDTSUBLOB(ControlID, ItemValue, ItemText) {

            BindDropdownForSUBLOB(ControlID, ItemValue, ItemText);
        }


        function BindDropdown(ControlID, ItemValue, ItemText) {

            var opt = '';
            opt += '<option value=-1>' + " " + '</option>';
            if (Result.d.Table.length > 0) {
                for (var row in Result.d.Table) {

                    if (row >= 0) {
                        opt += '<option value="' + eval('Result.d.Table[row].' + ItemValue) + '">' + eval('Result.d.Table[row].' + ItemText) + '</option>';
                    }
                }
            }

            $(ControlID).html(opt);

        }
        function BindDropdownForSUBLOB(ControlID, ItemValue, ItemText) {

            var opt = '';
            opt += '<option value=-1>' + "" + '</option>';
            opt += '<option value=0>' + "All" + '</option>';
            if (Result.d.Table.length > 0) {
                for (var row in Result.d.Table) {

                    if (row >= 0) {

                        opt += '<option value="' + eval('Result.d.Table[row].' + ItemValue) + '">' + eval('Result.d.Table[row].' + ItemText) + '</option>';
                    }
                }
            }

            $(ControlID).html(opt);

        }

        function ShowError() {
            alert(Error.responseText);
        }
          

            



       
    

    </script>

</head>
<body oncontextmenu="return false;" leftmargin="0" topmargin="0" onload="populateXML();ApplyColor();">
    <form id="ACT_REG_COMM_SETUP" method="post" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table width="100%" align="center" border="0">
                    <tr>
                        <td class="pageHeader" colspan="4">
                           <asp:label ID="capHeader" runat ="server"></asp:label> <%--Please note that all fields marked with * are mandatory--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolorc" align="right" colspan="4">
                            <asp:Label ID="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
                        </td>
                    </tr>
                    <tr id="agencyRow">
                        <td class="midcolora" width="18%"><asp:Label ID="capAgency" runat="server">Agency</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%" colspan="3">
                            <asp:DropDownList ID="cmbAGENCY_ID" onfocus="SelectComboIndex('cmbAGENCY_ID')" runat="server">
                                <asp:ListItem Value='0'></asp:ListItem>
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvAGENCY_ID" runat="server" Display="Dynamic" ErrorMessage="Please select Agency."
                                ControlToValidate="cmbAGENCY_ID"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capCOUNTRY_ID" runat="server">Country</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbCOUNTRY_ID" onfocus="SelectComboIndex('cmbCOUNTRY_ID')"
                                runat="server" SuccessMethod="outputDTSUBLOB" TargetControl="cmbSTATE_ID" ErrorMethod="ShowError"
                                ItemValue="STATE_ID" ItemText="STATE_NAME" ServerMethod="AddRegCommSetup_Agency.aspx/AjaxFillState">
                                <asp:ListItem Value='0'></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvCOUNTRY_ID" runat="server" Display="Dynamic" ErrorMessage="COUNTRY_ID can't be blank."
                                ControlToValidate="cmbCOUNTRY_ID"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cpvCOUNTRY_ID" ControlToValidate="cmbCOUNTRY_ID"
                                Display="Dynamic" runat="server"  Type="Integer"
                                Operator="NotEqual" ValueToCompare="0" ErrorMessage="COUNTRY_ID can't be blank."></asp:CompareValidator>
                            <asp:Label ID="lblCOUNTRY_NAME" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capSTATE_ID" runat="server">State</asp:Label><span id="spnSTATE_ID" runat="server" class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList class="FillDD" ID="cmbSTATE_ID" SuccessMethod="outputDTSUBLOB"
                                TargetControl="cmbLOB_ID" ErrorMethod="ShowError" ItemValue="LOB_ID" ItemText="LOB_DESC"
                                ServerMethod="AddRegCommSetup_Agency.aspx/GetSubLOBs" onchange="setSubLobBlank();"
                                runat="server">
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvSTATE_ID" runat="server" Display="Dynamic" ErrorMessage="STATE_ID can't be blank."
                                ControlToValidate="cmbSTATE_ID"></asp:RequiredFieldValidator>
                                 <asp:CompareValidator ID="cpvSTATE_ID" ControlToValidate="cmbSTATE_ID"
                                Display="Dynamic" runat="server"  Type="Integer"
                                Operator="NotEqual" ValueToCompare="-1" ErrorMessage="STATE_ID can't be blank."></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capLOB_ID" runat="server">Product</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbLOB_ID" onfocus="SelectComboIndex('cmbLOB_ID')" runat="server"
                                SuccessMethod="outputDTSUBLOB" TargetControl="cmbSTATE_ID" ErrorMethod="ShowError"
                                ItemValue="SUB_LOB_ID" ItemText="SUB_LOB_DESC" ServerMethod="AddRegCommSetup_Agency.aspx/GetSubSubLOBs">
                                <asp:ListItem Value='0'></asp:ListItem>
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvLOB_ID" runat="server" Display="Dynamic" ErrorMessage="LOB_ID can't be blank."
                                ControlToValidate="cmbLOB_ID"></asp:RequiredFieldValidator>
                                 <asp:CompareValidator ID="cpvLOB_ID" ControlToValidate="cmbLOB_ID"
                                Display="Dynamic" runat="server"  Type="Integer"
                                Operator="NotEqual" ValueToCompare="-1" ErrorMessage="LOB_ID can't be blank."></asp:CompareValidator>
                        </td>
                        <td class="midcolora" id="subLOBBlankCell" style="display: none" width="18%" colspan="2">
                            <td class="midcolora" id="subLOBCell" width="18%">
                                <asp:Label ID="capSUB_LOB_ID" runat="server">Line of Business</asp:Label><span id="spnSUB_LOB_ID" runat="server" class="mandatory">*</span>
                            </td>
                            <td class="midcolora" id="subLOBCell2" width="32%">
                                <asp:DropDownList ID="cmbSUB_LOB_ID" onfocus="SelectComboIndex('cmbSUB_LOB_ID')"
                                    runat="server" onchange="setHidSubLob()">
                                    <asp:ListItem Value='0'></asp:ListItem>
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvSUB_LOB_ID" runat="server" Display="Dynamic" ErrorMessage="SUB_LOB_ID can't be blank."
                                    ControlToValidate="cmbSUB_LOB_ID"></asp:RequiredFieldValidator>
                            </td>
                    </tr>
                    <tr id="classRow">
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capCLASS_RISK" runat="server">Classes/Risks</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbCLASS_RISK" onfocus="SelectComboIndex('cmbCLASS_RISK')"
                                runat="server" onchange="setHidClass()">
                                <asp:ListItem Value=''></asp:ListItem>
                            </asp:DropDownList>
                            <a class="calcolora" href="javascript:showPageLookupLayer('cmbCLASS_RISK')">
                                <img height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></a>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capTERM" runat="server">Term</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbTERM" onfocus="SelectComboIndex('cmbTERM')" runat="server">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="F">First Term(NBS)</asp:ListItem>
                                <asp:ListItem Value="O">Other Term</asp:ListItem>
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvTERM" runat="server" Display="Dynamic" ErrorMessage="TERM can't be blank."
                                ControlToValidate="cmbTERM"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capEFFECTIVE_FROM_DATE" runat="server">Effective From Date</asp:Label><span
                                class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtEFFECTIVE_FROM_DATE" runat="server" MaxLength="10" size="12"></asp:TextBox><asp:HyperLink
                                ID="hlkFROM_DATE" runat="server" CssClass="HotSpot">
                                <asp:Image ID="Image1" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif">
                                </asp:Image>
                            </asp:HyperLink><br>
                            <asp:RequiredFieldValidator ID="rfvEFFECTIVE_FROM_DATE" runat="server" Display="Dynamic"
                                ErrorMessage="EFFECTIVE_FROM_DATE can't be blank." ControlToValidate="txtEFFECTIVE_FROM_DATE"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                    ID="revEFFECTIVE_FROM_DATE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                                    ControlToValidate="txtEFFECTIVE_FROM_DATE"></asp:RegularExpressionValidator>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capEFFECTIVE_TO_DATE" runat="server">Effective To Date</asp:Label><span
                                class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtEFFECTIVE_TO_DATE" runat="server" MaxLength="10" size="12"></asp:TextBox><asp:HyperLink
                                ID="hlkTO_DATE" runat="server" CssClass="HotSpot">
                                <asp:Image ID="imgCHECK_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif">
                                </asp:Image>
                            </asp:HyperLink><br>
                            <asp:RequiredFieldValidator ID="rfvEFFECTIVE_TO_DATE" runat="server" Display="Dynamic"
                                ErrorMessage="EFFECTIVE_TO_DATE can't be blank." ControlToValidate="txtEFFECTIVE_TO_DATE"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                    ID="revEFFECTIVE_TO_DATE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                                    ControlToValidate="txtEFFECTIVE_TO_DATE"></asp:RegularExpressionValidator><asp:CustomValidator
                                        ID="csvCHECK_DATE" Display="Dynamic" ControlToValidate="txtEFFECTIVE_TO_DATE"
                                        ClientValidationFunction="ChkDate" runat="server"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capCOMMISSION_PERCENT" runat="server">Commission Percent</asp:Label><span
                                class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtCOMMISSION_PERCENT" runat="server" MaxLength="5" size="10" CssClass="INPUTCURRENCY"
                                Style="text-align: right"></asp:TextBox><br>
                            <asp:RequiredFieldValidator ID="rfvCOMMISSION_PERCENT" runat="server" Display="Dynamic"
                                ErrorMessage="COMMISSION_PERCENT can't be blank." ControlToValidate="txtCOMMISSION_PERCENT"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                    ID="revCOMMISSION_PERCENT" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                                    ControlToValidate="txtCOMMISSION_PERCENT"></asp:RegularExpressionValidator><asp:CustomValidator
                                        ID="csvCOMMISSION_PERCENT" Display="Dynamic" ControlToValidate="txtCOMMISSION_PERCENT"
                                        ClientValidationFunction="Validate" runat="server"></asp:CustomValidator>
                        </td>
                        <td class="midcolora" width="18%" colspan="2">
                        </td>
                    </tr>
                    <tr id="RemarksRow" style="display: none">
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capREMARKS" runat="server">Remarks</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox onkeypress="MaxLength(this,256);" ID="txtREMARKS" runat="server" MaxLength="250"
                                Width="100%" TextMode="MultiLine"></asp:TextBox><br>
                        </td>
                        <td class="midcolora" width="18%" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" colspan="2">
                            <cmsb:CmsButton class="clsButton" ID="btnReset" runat="server" Text="Reset"></cmsb:CmsButton>
                            <cmsb:CmsButton class="clsButton" ID="btnActivateDeactivate" runat="server" Text="Activate/Deactivate">
                            </cmsb:CmsButton>
                        </td>
                        <td class="midcolorr" colspan="2">
                            <cmsb:CmsButton class="clsButton" ID="btnCopy" runat="server" Text="Copy" CausesValidation="false">
                            </cmsb:CmsButton>
                            <cmsb:CmsButton class="clsButton" ID="btnSave" runat="server" Text="Save"></cmsb:CmsButton>
                        </td>
                    </tr>
                    <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
                    <input id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server">
                    <input id="hidOldData" type="hidden" name="hidOldData" runat="server">
                    <input id="hidCOMM_ID" type="hidden" name="hidCOMM_ID" runat="server">
                    <input id="hidCOUNTRY_ID" type="hidden" name="hidCOUNTRY_ID" runat="server">
                    <input id="hidLOBXML" type="hidden" name="hidLOBXML" runat="server">
                    <input id="hidSUB_LOB_ID" type="hidden" name="hidSUB_LOB_ID" runat="server">
                    <input id="hidState" type="hidden" value="3" name="hidState" runat="server">
                    <input id="hidClass" type="hidden" name="hidClass" runat="server">
                    <input id="hidREGULAR_COMMISSION_ID" type="hidden" name="hidREGULAR_COMMISSION_ID"
                        runat="server">
                    <input id="hidCOMMI_ID" type="hidden" name="hidCOMMI_ID" runat="server">
                    <input id="hidSelectedClass" type="hidden" name="hidSelectedClass" runat="server">
                    <input id="hidLobXMLForClass" type="hidden" name="hidLobXMLForClass" runat="server">
                    <input id="hidLOB_ID" type="hidden" name="hidLOB_ID" runat="server">
                    <input id="hidSaveLob" type="hidden" name="hidSaveLob" runat="server">
                    <input id="hidSaveSLob" type="hidden" name="hidSaveSLob" runat="server">
                    <input id="hidSaveClass" type="hidden" name="hidSaveClass" runat="server">
                </table>
            </td>
        </tr>
    </table>
    </form>
    <!-- For lookup layer -->
    <div id="lookupLayerFrame" style="display: none; z-index: 101; position: absolute">
        <iframe id="lookupLayerIFrame" style="display: none; z-index: 1001; filter: alpha(opacity=0);
            background-color: #000000" width="0" height="0" left="0px" top="0px;"></iframe>
    </div>
    <div id="lookupLayer" onmouseover="javascript:refreshLookupLayer();" style="z-index: 101;
        visibility: hidden; position: absolute">
        <table class="TabRow" cellspacing="0" cellpadding="2" width="100%">
            <tr class="SubTabRow">
                <td>
                    <b>Add LookUp</b>
                </td>
                <td>
                    <p align="right">
                        <a onclick="javascript:hideLookupLayer();" href="javascript:void(0)">
                            <img height="14" alt="Close" src="/cms/cmsweb/images/cross.gif" width="16" border="0"></a></p>
                </td>
            </tr>
            <tr class="SubTabRow">
                <td colspan="2">
                    <span id="LookUpMsg"></span>
                </td>
            </tr>
        </table>
    </div>
    <!-- For lookup layer ends here-->

    <script>
        RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidCOMM_ID').value, false);
    </script>

</body>
</html>
