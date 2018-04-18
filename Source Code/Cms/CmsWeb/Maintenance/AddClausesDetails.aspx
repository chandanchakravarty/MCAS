<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>

<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="AddClausesDetails.aspx.cs" Inherits="Cms.CmsWeb.Maintenance.AddClausesDetails" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>MNT_CLAUSES</title>
<meta content="Microsoft Visual Studio 7.0" name="GENERATOR"/>
    <meta content="C#" name="CODE_LANGUAGE"/>
    <meta content="JavaScript" name="vs_defaultClientScript"/>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet />
    <script src="/cms/cmsweb/scripts/xmldom.js" type="text/javascript"></script>
    <script src="/cms/cmsweb/scripts/common.js" type="text/javascript"></script>
    <script src="/cms/cmsweb/scripts/form.js" type="text/javascript"></script>
    <script src="/cms/cmsweb/scripts/Calendar.js" type="text/javascript"></script>
    <!-- For JQuery -->
	<script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery.js"></script> 
	<script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script> 
	<script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/JQCommon.js"></script> 
	<link rel="stylesheet" href="/cms/cmsweb/css/jQRichTextBox.css" type="text/css" /> 
    <script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jQRichTextBox.js"></script> 
        
        
    
    <script type="text/javascript" language="javascript">
    //Added by Pradeep Kushwaha on 16-march-2011
        $(function() {
            //$("textarea").htmlarea(); // Initialize all TextArea's as jHtmlArea's with default values

        //$("#txtareaCLAUSE_DESCRIPTION").htmlarea(); // Initialize jHtmlArea's with all default values

        $("#txtareaCLAUSE_DESCRIPTION").htmlarea({
                // Override/Specify the Toolbar buttons to show
                toolbar: [
                    ["bold", "italic", "underline", "strikethrough", "|", "subscript", "superscript"],
                    ["increasefontsize", "decreasefontsize"],
                    ["orderedlist", "unorderedlist"],
                    ["indent", "outdent"],
                    ["justifyleft", "justifycenter", "justifyright"],
                    ["link", "unlink","horizontalrule"],
                    ["p", "h1", "h2", "h3", "h4", "h5", "h6"],
                    ["cut", "copy", "paste"]],

                // Override any of the toolbarText values - these are the Alt Text / Tooltips shown
                // when the user hovers the mouse over the Toolbar Buttons
                
//                toolbarText: $.extend({}, jHtmlArea.defaultOptions.toolbarText, {
//                "bold": iLangID == 2 ? "Negrito" : "bold",
//                "italic": iLangID == 2 ? "itálico" : "italic",
//                "underline": iLangID == 2 ? "sublinhado" : "underline"
//                }),

                // Specify a specific CSS file to use for the Editor
                css: "/cms/cmsweb/css/jQRichTextBox.css",

                // Do something once the editor has finished loading
                loaded: function() {
                    //// 'this' is equal to the jHtmlArea object
                    //alert("jHtmlArea has loaded!");
                    //this.showHTMLView(); // show the HTML view once the editor has finished loading
                }
            });
        });
        //Add Till here 
        function ResetTheForm() {
            document.MNT_CLAUSES.reset();
            populateXML();
            
        }

        function setHidSubLob() {
        
            document.getElementById('hidSUB_LOB').value = document.getElementById('cmbSUBLOB_ID').value;
            var ctl = document.getElementById('cmbSUBLOB_ID');
            
            if (ctl.selectedIndex == -1)
                return Page_ClientValidate()
            var ctlValue = ctl[ctl.selectedIndex].value;
            ValidatorID = 'rfvSUBLOB_ID';
            if( <%=GetLanguageID().ToString() %>!="3")
            {
                if (ctlValue == "-1") {
                    document.getElementById(ValidatorID).setAttribute('enabled', true);
                    document.getElementById(ValidatorID).style.display = "inline";
                    document.getElementById(ValidatorID).setAttribute('isValid', true);
                    Page_IsValid = false;
                    return false;
                }
                else
                {
                    DisableValidatorsById('rfvSUBLOB_ID');
                    }
            }
            else
            {
             	 document.getElementById(ValidatorID).setAttribute('enabled', false);
                 document.getElementById(ValidatorID).style.display = "none";
                 document.getElementById(ValidatorID).setAttribute('isValid', false);
            }

            return Page_ClientValidate();

        }
        
        function setSubLobBlank() {

            if ((document.getElementById('cmbLOB_ID').value) == "") {
                document.getElementById('cmbSUBLOB_ID').innerHTML = "";
            }
            
        }
        function InitPage() {
          
            ApplyColor(); ChangeColor();
            if (document.getElementById('hidSUB_LOB').value != "-1") {
                document.getElementById("rfvSUBLOB_ID").setAttribute('enabled', true);
                document.getElementById("rfvSUBLOB_ID").style.display = "none";
            }



             //attachment
            if (document.getElementById('cmbCLAUSE_TYPE').value == "14696") {

                document.getElementById('trATTACH_FILE_NAME').style.display = "inline";

                document.getElementById('trCLAUSE_DESCRIPTION').style.display = "none";

            }

            else {
                document.getElementById('trATTACH_FILE_NAME').style.display = "none";
                document.getElementById('trCLAUSE_DESCRIPTION').style.display = "inline";
                document.getElementById('rfvATTACH_FILE_NAME').style.display = "none";
                document.getElementById('rfvATTACH_FILE_NAME').setAttribute('enabled', false);
            }
        }

        function ValidateDesc(objSource, objArgs) {
            if (objSource.controltovalidate != null) {
                var ctrl = document.getElementById(objSource.controltovalidate);

              if (ctrl.value.length > 0)
                    objArgs.IsValid = true;
                else
                    objArgs.IsValid = false;
          
            }
        }
        function OnClauseTypeChange() {
          

      //Manual
            if (document.getElementById('cmbCLAUSE_TYPE').value == "14695") {
               
                document.getElementById('trATTACH_FILE_NAME').style.display = "none";
              
                document.getElementById('trCLAUSE_DESCRIPTION').style.display = "inline";

                document.getElementById('rfvATTACH_FILE_NAME').setAttribute('enabled', false);
                document.getElementById('rfvATTACH_FILE_NAME').style.display = "none";
                
            }
           //Attachment 
            else {
              
                document.getElementById('trATTACH_FILE_NAME').style.display = "inline"
                document.getElementById('trCLAUSE_DESCRIPTION').style.display = "none"
                // change by praveer for itrack no 1482
                if (document.getElementById("hidATTACH_FILE_NAME").value == "Y")
                document.getElementById('rfvATTACH_FILE_NAME').setAttribute('enabled', true);
                
            }
        
        }

        function populateXML() {
            if (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1') {
                if (document.getElementById('hidOldData').value != "") {
                    populateFormData(document.getElementById('hidOldData').value, MNT_CLAUSES);
                    document.getElementById("revATTACH_FILE").style.display = 'none';
                    var fileName1 = document.getElementById("lblATTACH_FILE_NAME").innerText;
                    var RootPath = document.getElementById('hidRootPath').value;
                    var ClauID = document.getElementById('hidCLAUSEID').value;
                    if (fileName1 != "") {
                        document.getElementById("fileATTACH_FILE_NAME").style.display = 'none';
                        document.getElementById("hidATTACH_FILE_NAME").value = "N";
                    }
                    else
                        document.getElementById("hidATTACH_FILE_NAME").value = "Y";
                    document.getElementById("lblATTACH_FILE_NAME").style.display = 'inline';
                    //document.getElementById("lblRTL_FILE").innerHTML ="<a href = '" + RootPath +  "/" + DepID + "_" + fileName1 + "' target='blank'>" + fileName1 + "</a>";
                    document.getElementById("lblATTACH_FILE_NAME").innerHTML = "<a href = '" + document.getElementById("hidfileLink").value + "' target='blank'>" + fileName1 + "</a>";
                    //	ChangeAccountBalance(); //added to display amt and Deposit Number:
                }
                else {
                    AddData();
                }
            }

          
            return false;
        }

        function AddData() {
            document.getElementById("lblATTACH_FILE_NAME").style.display = 'inline';
            document.getElementById("hidATTACH_FILE_NAME").value = "Y";

        }
</script>



    <style type="text/css">
        #txtareaCLAUSE_DESCRIPTION
        {
            width: 672px;
            height: 231px;
        }
    </style>



</head>
<body leftmargin="0" topmargin="0" oncontextmenu="return false;" onload="populateXML();InitPage();">
    <form id="MNT_CLAUSES" runat="server" method="post">
    <table  width="100%" class="tableWidthHeader">
        <tr>
            <td>
                <table width="100%" class="tableWidthHeader">
                    <tr>
                     <td class="midcolorc" align="right" colSpan="3">
                        <asp:Label ID="lblDelete" runat="server" CssClass="errmsg" Visible="False" 
                             meta:resourcekey="lblDeleteResource1"></asp:Label>
                     </td>
                    </tr>
                 </table>
            </td>
        </tr>
        <tr id="trBody" runat="server">
        <td>
        <table width="100%" class="tableWidthHeader">
        <tr>
            <td class="midcolorc" align="right" colspan="2">
                <asp:Label ID="lblMessage" runat="server" CssClass="errmsg" Visible="False" 
                    meta:resourcekey="lblMessageResource1"></asp:Label>
            </td>
        </tr>
        <tr>
        <td class="pageHeader" colspan="2" class="midcolora">
         <asp:Label runat="server" ID="capMAN_MSG" meta:resourcekey="capMAN_MSGResource1"></asp:Label>
        </td>
        </tr>
        <tr>
            <td width="50%" class="midcolora">

                
            <asp:Label ID="capLOB_ID" runat="server" Text="Product" 
                    meta:resourcekey="capLOB_IDResource1"></asp:Label><span class="mandatory">*</span></br>
            
            
             <asp:dropdownlist id="cmbLOB_ID"  class="FillDD" SuccessMethod="outputDTSUBLOB" 
                    TargetControl="cmbSUBLOB_ID" ErrorMethod="ShowError" ItemValue="SUB_LOB_ID" 
                    ItemText="SUB_LOB_DESC"  ServerMethod="AddClausesDetails.aspx/GetSubLOBs" onchange="setSubLobBlank();" 
                    runat="server" ></asp:dropdownlist>
                       <%--onchange="setSubLobBlank()--%>
             <br />
             <asp:requiredfieldvalidator id="rfvLOB_ID" runat="server" 
                    ControlToValidate="cmbLOB_ID" Display="Dynamic" 
                   ></asp:requiredfieldvalidator>
                
            </td>
                <td width="50%" class="midcolora">
               
            <asp:Label ID="capSUBLOB_ID" Text="Line Of Business"  runat="server" 
                        meta:resourcekey="capSUBLOB_IDResource1"></asp:Label><span ID="spnSUBLOB_ID" runat="server" class="mandatory">*</span><br />
            
              <asp:dropdownlist id="cmbSUBLOB_ID"  runat="server" 
                        meta:resourcekey="cmbSUBLOB_IDResource1" ></asp:dropdownlist>
                    <br />
             <asp:requiredfieldvalidator id="rfvSUBLOB_ID" runat="server" 
                    ControlToValidate="cmbSUBLOB_ID" Display="Dynamic" 
                   ></asp:requiredfieldvalidator>
                
            </td>
         <%--   <td width="33%" class="midcolora">
                
            </td>--%>
            </tr>
            <tr>
            <td width="50%" class="midcolora">
             <asp:Label ID="capCLAUSE_CODE" runat="server" Text=""></asp:Label><span class="mandatory">*</span><br />
             <asp:TextBox ID="txtCLAUSE_CODE" runat="server" MaxLength="10" ></asp:TextBox><br />
                <asp:requiredfieldvalidator id="rfvCLAUSE_CODE" runat="server" ControlToValidate="txtCLAUSE_CODE" 
											Display="Dynamic"></asp:requiredfieldvalidator> 
            </td>
       
            <td width="50%" class="midcolora">
               <asp:Label ID="capCLAUSE_TITLE" runat="server" Text="Title" 
                    meta:resourcekey="capCLAUSE_TITLEResource1"></asp:Label><span class="mandatory">*</span></br>
                <asp:TextBox ID="txtCLAUSE_TITLE" runat="server" Width="200px" 
                    onkeypress="MaxLength(this,200)" onpaste="MaxLength(this,200)" 
                    meta:resourcekey="txtCLAUSE_TITLEResource1"></asp:TextBox>
                <br />
                <asp:requiredfieldvalidator id="rfvCLAUSE_TITLE" runat="server" ControlToValidate="txtCLAUSE_TITLE" 
											Display="Dynamic" meta:resourcekey="rfvCLAUSE_TITLEResource1"></asp:requiredfieldvalidator> 
                </td>
           <%-- <td width="33%" class="midcolora">
            </td--%>
        </tr>
        <tr>
            <td width="50%" class="midcolora">
             <asp:Label ID="capPROCESS_TYPE" runat="server" Text=""></asp:Label><span class="mandatory">*</span><br />
                <asp:DropDownList ID="cmbPROCESS_TYPE" runat="server">
                </asp:DropDownList><br />
                 <asp:requiredfieldvalidator id="rfvPROCESS_TYPE" runat="server" ControlToValidate="cmbPROCESS_TYPE" Display="Dynamic"> </asp:requiredfieldvalidator>
                
               
                </td>
            <td width="50%" class="midcolora">
            
             <asp:Label ID="capCLAUSE_TYPE" runat="server" Text="Clause Type"></asp:Label></asp:Label><span class="mandatory">*</span><br />
                <asp:DropDownList ID="cmbCLAUSE_TYPE" runat="server" onchange="OnClauseTypeChange();">
                </asp:DropDownList><br />
                <asp:requiredfieldvalidator id="rfvCLAUSE_TYPE" runat="server" ControlToValidate="cmbCLAUSE_TYPE" Display="Dynamic"> </asp:requiredfieldvalidator>
            </td>
        </tr>
        <tr id="trCLAUSE_DESCRIPTION" collspan="2" >
            <td width="50%" class="midcolora" colspan="2" style="width: 50%" >
           
                <asp:Label ID="capCLAUSE_DESCRIPTION" runat="server" 
                    meta:resourcekey="capCLAUSE_DESCRIPTIONResource1">Clause Description</asp:Label>
                <br />
              <%-- <asp:TextBox runat="server" ID="txtCLAUSE_DESCRIPTION" SIZE="15" MaxLength="10" 
                    TextMode="MultiLine" Width="530px" Height="50px" 
                    meta:resourcekey="txtCLAUSE_DESCRIPTIONResource1"></asp:TextBox>--%>
                    <textarea id="txtareaCLAUSE_DESCRIPTION" name="txtareaCLAUSE_DESCRIPTION" 
                    runat="server"></textarea>
                    
               <br />
               <asp:customvalidator id="csvCLAUSE_DESCRIPTION" Runat="server" ControlToValidate="txtareaCLAUSE_DESCRIPTION" Display="Dynamic"
								ClientValidationFunction="ValidateDesc"></asp:customvalidator>
		<%--	   <asp:requiredfieldvalidator id="rfvCLAUSE_DESCRIPTION" runat="server" ControlToValidate="txtareaCLAUSE_DESCRIPTION" 
											Display="Dynamic" meta:resourcekey="rfvCLAUSE_DESCRIPTIONResource1"></asp:requiredfieldvalidator>--%>
										
											
            </td>
            </tr>
            
        
        <tr id="trATTACH_FILE_NAME">
								<TD class="midcolora" colSpan="2">
								
								
								<asp:label id="capATTACH_FILE_NAME" runat="server"></asp:label><span class="mandatory" id="spnFileName" runat="server">*</span></br>
								<input id="fileATTACH_FILE_NAME"  onchange="RemoveSpecialChar(document.getElementById('fileATTACH_FILE_NAME').value,document.getElementById('revATTACH_FILE'));RemoveExecutableFiles(document.getElementById('fileATTACH_FILE_NAME').value,document.getElementById('revATTACH_FILE_EXT'));AllowEXTFiles(document.getElementById('fileATTACH_FILE_NAME').value,document.getElementById('revATTACH_FILE_PDF'));" 
								 type="file" size="70" runat="server"><br>
                                <asp:label id="lblATTACH_FILE_NAME" Runat="server" ForeColor="Blue"></asp:label>
                                    
                                    <br>
                                    
                                    
                                    </br>
									
									<asp:requiredfieldvalidator id="rfvATTACH_FILE_NAME" runat="server" Display="Dynamic" ControlToValidate="fileATTACH_FILE_NAME"></asp:requiredfieldvalidator>
									<asp:regularexpressionvalidator id="revATTACH_FILE" Runat="server" ControlToValidate="fileATTACH_FILE_NAME" Display="Dynamic"></asp:regularexpressionvalidator>
											<asp:regularexpressionvalidator id="revATTACH_FILE_EXT" Runat="server" ControlToValidate="fileATTACH_FILE_NAME" Display="Dynamic"></asp:regularexpressionvalidator>
											<asp:regularexpressionvalidator id="revATTACH_FILE_PDF" Runat="server" ControlToValidate="fileATTACH_FILE_NAME" Display="Dynamic"></asp:regularexpressionvalidator>
									
									
									</TD>
							</tr>
        <tr>
            <td  class="midcolora" width="50%">
            <cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" 
                    meta:resourcekey="btnResetResource1"></cmsb:cmsbutton>
               <cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" 
                    CausesValidation="False" Text="ActivateDeactivate" 
                    onclick="btnActivateDeactivate_Click" 
                    meta:resourcekey="btnActivateDeactivateResource1"/>
                </td>
             <%--<td  class="midcolora" width="33%"></td>--%>
                <td  class="midcolora" width="50%">
                <table width="100%" class="tableWidthHeader">
                <tr>
                <td align="right" width="90%">
                <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" 
                        onclick="btnSave_Click" meta:resourcekey="btnSaveResource1"></cmsb:cmsbutton>
                </td>
                <td align="right" width="20%">
                <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" 
                    causesvalidation="False" onclick="btnDelete_Click" 
                        meta:resourcekey="btnDeleteResource1"></cmsb:cmsbutton>
                </td>
                </tr>
                </table>          
                </td>
				
           </tr>
        <input id="hidLOBXML" type="hidden" value="0" name="hidLOBXML" runat="server"/>
        <input id="hidLOBID" type="hidden" value="0" name="hidLOBID" runat="server"/> 
        <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server"/>
        <input id="hidCLAUSEID" type="hidden" value="" name="hidCLAUSEID" runat="server"/>
        <input id="hidSUB_LOB" type="hidden" value="-1" name="hidSUB_LOB" runat="server"/>
        <input id="hidOldSUB_LOB" type="hidden" value="0" name="hidOldSUB_LOB" runat="server"/>
         <input id="hidIS_ACTIVE" type="hidden" value="0" name="hidOldSUB_LOB" runat="server"/>
          <input id="hidRowId" type="hidden" value="New" name="RowID" runat="server"/>
          <input id="hidAttachFileName" type="hidden" value="" name="hidAttachFileName" runat="server"/>
         <input id="hidGeneral" type="hidden" value="" name="hidGeneral" runat="server"/>
          <input id="hidATTACH_FILE_NAME" type="hidden" value="" name="hidATTACH_FILE_NAME" runat="server"/>
          <input id="hidRootPath" type="hidden" value="" name="hidRootPath" runat="server"/>
          <input id="hidfileLink" type="hidden" value="" name="hidfileLink" runat="server"/>
          <input id="hidOldData" type="hidden" value="" name="hidOldData" runat="server"/>
         
        </table>
        </td>
        </tr>
    </table>
    </form>
     <script type="text/javascript" >
         if (document.getElementById('hidFormSaved').value == "1") {

             RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidCLAUSEID').value);
         }
		</script>
</body>
</html>

