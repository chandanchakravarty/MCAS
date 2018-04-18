<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarrierMenuSetup.aspx.cs" Inherits="Cms.CmsWeb.Maintenance.CarrierMenuSetup" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title>Carrier Menu Setup</title>  
    <link rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css" />
	<script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/JQCommon.js"></script>    
		
		<script type="text/javascript" language='javascript'>

		    var newscreenid = '';
		    var newmodifiedscreen = '';

		    function init() {
		        ChangeColor();
		        ApplyColor();

		    }

		    function fillSubmodulefromModule() {      
		        GlobalError = true;
		        var ModuleId = document.getElementById('cmbMODULE').options[document.getElementById('cmbMODULE').selectedIndex].value;
		       
		            
		        CarrierMenuSetup.AjaxFillSubModule(ModuleId, fillSubModule);
		        if (GlobalError) {
		            return false;
		        }
		        else {
		            return true;
		        }
		    }

		    function fillSubModule(Result) {
		        if (Result.error) {
		            var xfaultcode = Result.errorDetail.code;
		            var xfaultstring = Result.errorDetail.string;
		            var xfaultsoap = Result.errorDetail.raw;
		        }
		        else {
		            var modulesList = document.getElementById("cmbSUB_MODULE");
		            modulesList.options.length = 0;
		            oOption = document.createElement("option");
		            oOption.value = "0";
		            oOption.text = "All";
		            modulesList.add(oOption);		           
		            ds = Result.value;
		            if (ds != null && typeof (ds) == "object" && ds.Tables != null) {
		                for (var i = 0; i < ds.Tables[0].Rows.length; ++i) {
		                    modulesList.options[modulesList.options.length] = new Option(ds.Tables[0].Rows[i]["SUB_MODULE_NAME"], ds.Tables[0].Rows[i]["SUB_MODULE_ID"]);
		                }
		            }

		        }

		        return false;
		    }


		    function populateXML() {
		        if (document.getElementById('hidOldDatas').value != "") {
		            populateData();
		        }
		    }

		    function populateData() {
		        var tree = document.getElementById('hidOldDatas').value;
		        if (tree == null || tree == 'undefined') return;
		        var objXmlHandler = new XMLHandler();
		        var screenList = {};
		        screenList = objXmlHandler.quickParseXML(tree).getElementsByTagName('screen');
		        populateScreenData(screenList);
		    }
		    function populateScreenData(screenList) {
		        var objXmlHandler = new XMLHandler();
		        var permissionXML = {};
		        var strHTML = '<TABLE cellSpacing=0 cellPadding=0 width=100% border=0 >';
		        var header, prevHeader, secHeader, subSecHeader, subSubSecHeader, screenIds, typeId;
		        header = '';
		        prevHeader = '';
		        screenIds = '';
		        isactives = '';
		        var i, j, k, l;

		        for (i = 0; i < screenList.length; i++) {
		            secHeader = '';
		            subSecHeader = '';
		            subSubSecHeader = '';
		            newscreenid = '';
		            //New header added for policy
		            subPolicyHeader = '';
		            screenIds += screenList[i].screen_id + '-' + screenList[i].is_active + '~';

		            if (screenList[i].level1_id) {
		                if (secHeader != screenList[i].level1)
		                    secHeader = screenList[i].level1;
		            }
		            if (screenList[i].level2_id) {
		                if (subSecHeader != screenList[i].level2)
		                    subSecHeader = screenList[i].level2;
		            }


		            header = secHeader;
		            if (subSecHeader != "") header += "&nbsp;&gt;&gt;&nbsp;" + subSecHeader;
		            if (subSubSecHeader != "") header += "&nbsp;&gt;&gt;&nbsp;" + subSubSecHeader;
		            //Policy header				
		            if (subPolicyHeader != "") header += "&nbsp;(" + subPolicyHeader + ")";

		            if (header != prevHeader) {
		                prevHeader = header;

		                //		                if (secHeader != "")
		                //                         typeId = screenList[i].level1_id;
		                if (subSecHeader != "")
		                    typeId = screenList[i].level2_id;
		                if (subSubSecHeader != "")
		                    typeId = screenList[i].level3_id;
		                if (typeId != "") {
		                    strHTML += '<TR><TD class="pageHeader" width="40%">' + header + '</TD>';
		                    strHTML += '<TD class="pageHeader" width="10%"><input type=checkbox name= like' + typeId + '_R onclick="checkAll(this)"> Allow All</TD>'
		                    strHTML += '</TR>';

		                }
		            }
		            strHTML += '<TR><TD class=midcolora style="padding-left:20px">' + screenList[i].screen_desc + '</TD>';

		            if (screenList[i].is_active == 'Y') {

		                strHTML += '<TD class=midcolora><input  type=checkbox name=' + typeId + '-' + screenList[i].screen_id + '_read checked onClick="checkSelf(this)"> Allow</TD>';

		            }
		            else {
		                strHTML += '<TD class=midcolora><input type=checkbox name=' + typeId + '-' + screenList[i].screen_id + '_read onClick="checkSelf(this)"> Allow</TD>';

		            }

		            strHTML += '</TR>';
		        }
		        strHTML += '</TABLE>';

		        document.getElementById('hidSCREENLIST').value = screenIds.substring(0, screenIds.length - 1);
		        document.getElementById('screenRights').innerHTML = strHTML;

		    }

		    function checkAll(obj) {
		        var checkBoxName = obj.name.substring(4, obj.name.length - 2);
		        var checkBoxType = obj.name.substring(obj.name.length - 1, obj.name.length);
		        var objname = obj.name;
		        var loop = new Array();

		        if (checkBoxType == 'R') {

		            for (var i = 0; i < document.forms[0].elements.length; i++) {
		                var e = document.forms[0].elements[i];
		                var Name = e.name;
		                var newcheckbox = e.name.substring(4, e.name.length - 1);
		                var checkBoxType = e.name.substring(e.name.lastIndexOf("_") + 1);
		                var NewcheckBoxName = Name.split("-")[0];
		                var NewMenuId = Name.split("-")[1];
		                if (NewMenuId != 'undefined' && NewMenuId != '' && NewMenuId != null) {
		                    NewMenuId = NewMenuId.substring(0, NewMenuId.lastIndexOf("_"));
		                }
		                if ((e.name.indexOf(checkBoxName) == 0) && NewcheckBoxName == checkBoxName && (e.type == 'checkbox') && (checkBoxType == 'read') && (e.name.indexOf(checkBoxType) != -1)) {

		                    if (obj.checked == true) {
		                        e.checked = true;
		                        newscreenid += NewMenuId + '-' + 'Y' + '~';
		                    }
		                    else if (obj.checked == false && checkBoxType == 'read') {
		                        e.checked = false;
		                        newscreenid += NewMenuId + '-' + 'N' + '~';
		                    }

		                }

		            }
		        }
		        document.getElementById('hidIS_ACTIVE').value = newscreenid.substring(0, newscreenid.length - 1);


		    }
		    //Till here

		    function checkSelf(obj) {

		        var checkBoxName = obj.name.substring(0, obj.name.lastIndexOf("_"));
		        checkBoxName = checkBoxName.split("-")[1];
		        for (var i = 0; i < document.forms[0].elements.length; i++) {
		            var e = document.forms[0].elements[i];
		            var checkBoxType = obj.name.substring(obj.name.length - 1, obj.name.length);
		            if ((e.type == 'checkbox') && (e.name.indexOf(checkBoxType) != -1)) {
		                if (obj.checked == true) {
		                    obj.checked = true;
		                    newscreenid += checkBoxName + '-' + 'Y' + '~';
		                }
		                else if (obj.checked == false) {
		                    obj.checked = false;
		                    newscreenid += checkBoxName + '-' + 'N' + '~';
		                }


		            }
		        }
		        document.getElementById('hidIS_ACTIVE').value = newscreenid.substring(0, newscreenid.length - 1);

		    }

		   

        
        </script>
        <script type="text/javascript" language='javascript'>
            $(document).ready(function () {
                $("#cmbSUB_MODULE").change(function () {

                    $("#hidsubmoduleid").val($("#cmbSUB_MODULE option:selected").val());
                });
            });
        </script>
       
   
      
        
</head>
<body oncontextmenu="return false;"  onload='populateXML();init();' >
<div id="bodyHeight"   >
  <form id="MNT_MENU_LIST" method="post" runat="server" >
		<TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
				<TR>
					<TD>
						<TABLE width='100%' border='0' align='center'>
							<tr>
								<TD class="headereffectCenter" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								
                                <td class='midcolora' colspan='1'>    
                                <asp:Label ID="Label1" Text ="Carrier" runat="server" ></asp:Label><span class="mandatory">*</span>  
									<asp:DropDownList ID="cmbCarrier"  runat="server" ></asp:DropDownList>
                                      <asp:RequiredFieldValidator  ID="rfvCarrier" runat="server" ErrorMessage="Please select Carrier" Display="Dynamic"
                                        ControlToValidate="cmbCarrier"></asp:RequiredFieldValidator>
                                                         
									
								</td>
                                
                                <td class='midcolora' colspan='1'>
                                <asp:Label ID="lblselect" Text ="Module Name" runat="server" ></asp:Label>
									<asp:DropDownList ID="cmbMODULE"  runat="server" onchange="javascript:fillSubmodulefromModule();"></asp:DropDownList>
                                    
								</td>

                                 <td class='midcolora' colspan='2'>
                                <asp:Label ID="Label2" Text ="Sub Module " runat="server" ></asp:Label>
									<asp:DropDownList ID="cmbSUB_MODULE"  runat="server" ></asp:DropDownList>
                                     <cmsb:cmsbutton class="clsButton" id="btnSelect" runat="server" Text="Search"  />
                                    
								</td>
							</tr>
							<tr>
								<td class='midcolora' colspan='4'>
									<div id="screenRights" width="100%"> </div>
								</td>

							</tr>
                            <tr id="screenButtons">
								<td class='midcolora' colspan='2'>
									<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
								</td>
								<td class='midcolorr' colspan="2">
									<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
								</td>
							</tr>
							
							
							<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
							<INPUT id="hidOldDatas" type="hidden" name="hidOldDatas" runat="server">                        
							<INPUT id="hidSCREENLIST" type="hidden" name="hidSCREENLIST" runat="server">
                            <INPUT id="hidFormSaved" type="hidden" name="hidFormSaved" runat="server">
                             <INPUT id="hidsubmoduleid" type="hidden" name="hidsubmoduleid" runat="server">
							
						</TABLE>
					</TD>
				</TR>
			</TABLE>	
  
    
       </form>
     </div>
</body>
</html>
