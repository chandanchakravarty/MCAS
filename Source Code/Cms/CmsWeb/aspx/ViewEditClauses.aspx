<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewEditClauses.aspx.cs" Inherits="CmsWeb.aspx.ViewEditClauses" ValidateRequest="false" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Clause Text</title>
    <link rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
	<script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
	
	<script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script> 
	
	<link rel="stylesheet" href="/cms/cmsweb/css/jQRichTextBox.css" type="text/css" />
    <script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jQRichTextBox.js"></script>
    
  
  
	<script type="text/javascript" language="javascript">
	    //Added by Pradeep Kushwaha on 16-march-2011
	    $(function() {
	        $("#txtareaCLAUSE_DESCRIPTION").htmlarea({
	            // Override/Specify the Toolbar buttons to show
	            toolbar: [
                    ["bold", "italic", "underline", "strikethrough", "|", "subscript", "superscript"],
                    ["increasefontsize", "decreasefontsize"],
                    ["orderedlist", "unorderedlist"],
                    ["indent", "outdent"],
                    ["justifyleft", "justifycenter", "justifyright"],
                    ["link", "unlink", "horizontalrule"],
                    ["p", "h1", "h2", "h3", "h4", "h5", "h6"],
                    ["cut", "copy", "paste"]],

	            // Specify a specific CSS file to use for the Editor
	            css: "/cms/cmsweb/css/jQRichTextBox.css" 
	        });
	    });
	    //Add Till here 
	    
      
	    function refreshParent() {
	        var url = window.opener.location.href;
	        var parent = 'PolicyClauses';
	        if (url != null && url != "" && url.indexOf(parent) != -1) {
	           window.opener.location.href = window.opener.location.href;
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

	       

	        if (document.getElementById('cmbCLAUSE_TYPE').value == "14695") {
	            document.getElementById('attachment').style.display = 'none';
	            document.getElementById('trdescription').style.display = 'inline'; 
	            document.getElementById('rfvATTACH_FILE_NAME').style.display = 'none';
	            document.getElementById('rfvATTACH_FILE_NAME').setAttribute('enabled', false);

	        }


	        else {
	            document.getElementById('trdescription').style.display = 'none';
	            document.getElementById('attachment').style.display = 'inline';
	           // document.getElementById("fileATTACH_FILE_NAME").style.display = 'inline';
	            document.getElementById("lblATTACH_FILE_NAME").style.display = 'inline';
	          //  document.getElementById('fileATTACH_FILE_NAME').style.visibility = "visible";
	            document.getElementById('rfvATTACH_FILE_NAME').setAttribute('enabled', true);

	        }
	      if (document.getElementById("hidDEFIND_ID").value != "2") {	        	        
    	        
    	        document.getElementById("fileATTACH_FILE_NAME").style.display = 'none';
    	        document.getElementById('trdescription').style.display = 'none';
    	        document.getElementById('cmbCLAUSE_TYPE').setAttribute('disabled', true);
    	    }
//    	    if (document.getElementById("hidpolicystatus").value != "" || document.getElementById("hidpolicystatus").value != "ENDORSE") {

//    	     //   document.getElementById("fileATTACH_FILE_NAME").style.display = 'none';
//    	        document.getElementById('trdescription').style.display = 'none';
//    	    }
	    }


	    function populateXML() {
	        
	        if (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1') {
	            if (document.getElementById('hidOldData').value != "" ) {
	                if (document.getElementById('hidPOL_CLAUSE_ID').value != "") {
	                    populateFormData(document.getElementById('hidOldData').value, MNT_CLAUSES);
	                }
	                // debugger;
	                document.getElementById("revATTACH_FILE").style.display = 'none';
	                var fileName1 = document.getElementById("lblATTACH_FILE_NAME").innerText;
	              //  var fileName1 = document.getElementById("hidAttach").value;
	                var RootPath = document.getElementById('hidRootPath').value;
	                var ClauID = document.getElementById('hidPOL_CLAUSE_ID').value;
	                if (fileName1 != "") {
////	                    if (document.getElementById("hidpolicystatus").value != "" || document.getElementById("hidpolicystatus").value != "ENDORSE") {
////	                        document.getElementById("fileATTACH_FILE_NAME").style.display = 'none';
////	                    }
	                    document.getElementById("hidATTACH_FILE_NAME").value = "N";
	                }
	                else
	                    document.getElementById("hidATTACH_FILE_NAME").value = "Y";
	                document.getElementById("lblATTACH_FILE_NAME").style.display = 'inline';
	                //document.getElementById("lblRTL_FILE").innerHTML ="<a href =  '" + RootPath +  "/" + DepID + "_" + fileName1 + "' target='blank'>" + fileName1 + "</a>";
	                document.getElementById("lblATTACH_FILE_NAME").innerHTML = "<a style='font-size:8pt;font-weight:normal'  href  =  '" + document.getElementById("hidfileLink").value + " ' target='blank' >" + fileName1 + "</a>";
	                 //	.....ChangeAccountBalance(); //added to display amt and Deposit Number:                
	                
	                AddData();
	            }
	            else {
	                AddData();
	            }
	        }

	        OnClauseTypeChange();
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
            width: 420px;
            height: 280px;
        }
    </style>
</head>
<body onunload="javascript:refreshParent();" oncontextmenu="return false;" onload="populateXML();javascript:self.focus">
    <form id="MNT_CLAUSES" runat="server" >    
        <table width="100%">
            <tr>							        
                <td class="headereffectCenter" colspan="3">
                    <asp:label id="lblHeader" Runat="server">View/Edit Clause</asp:label><asp:label id="lblHeader1" Runat="server">View</asp:label>
                </td>
            </tr>	
            <tr>
                <td class="midcolorc" colspan="3">
                    <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
             <td class='midcolora' width="25%">
              <asp:Label ID="capCLAUSE_CODE" runat="server" Text=""></asp:Label><span class="mandatory">*</span><br />
             <asp:TextBox ID="txtCLAUSE_CODE" runat="server" MaxLength="10" ></asp:TextBox><br />
                <asp:requiredfieldvalidator id="rfvCLAUSE_CODE" runat="server" ControlToValidate="txtCLAUSE_CODE" 
											Display="Dynamic"></asp:requiredfieldvalidator> 
             
             </td>
                <td class='midcolora' width="50%">
                    <asp:Label ID="capCLAUSE_TITLE" runat="server">Clause Title:</asp:Label><span class="mandatory">*</span>
                    <br />
                    <asp:TextBox ID="txtCLAUSE_TITLE" Width="100%" runat="server"></asp:TextBox>                    
                    <asp:RequiredFieldValidator ID="rfvCLAUSE_TITLE" runat="server" Display="Dynamic" ControlToValidate="txtCLAUSE_TITLE"></asp:RequiredFieldValidator>
                </td>
                <td width="25%" class="midcolora">
                <asp:Label ID="capCLAUSE_TYPE" runat="server" Text=" "></asp:Label></asp:Label><span class="mandatory">*</span><br />
                <asp:DropDownList ID="cmbCLAUSE_TYPE" runat="server" onchange="OnClauseTypeChange();">
                </asp:DropDownList><br />
                <asp:requiredfieldvalidator id="rfvCLAUSE_TYPE" runat="server" ControlToValidate="cmbCLAUSE_TYPE" Display="Dynamic"> </asp:requiredfieldvalidator>
                </td>
            </tr>
           
            <tr id="trdescription">
                <td class='midcolora' colspan="3">
                    <asp:Label ID="capCLAUSE_DESCRIPTION" runat="server">Clause Text:</asp:Label>
                    <br />
                    <%--<asp:TextBox ID="txtCLAUSE_DESCRIPTION" Width="100%" runat="server" TextMode="MultiLine" Rows="15"></asp:TextBox>                   --%>
                      <textarea id="txtareaCLAUSE_DESCRIPTION" runat="server"  rows="5" cols="90"></textarea>
                         <asp:customvalidator id="csvCLAUSE_DESCRIPTION" Runat="server" ControlToValidate="txtareaCLAUSE_DESCRIPTION" Display="Dynamic"
								ClientValidationFunction="ValidateDesc"></asp:customvalidator>
                    <%--<asp:RequiredFieldValidator ID="rfvCLAUSE_DESCRIPTION" runat="server" Display="Dynamic" ControlToValidate="txtCLAUSE_DESCRIPTION"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr id="attachment">
            <td  class='midcolora' colspan="3" >
            <asp:label id="capATTACH_FILE_NAME" runat="server"></asp:label><span class="mandatory" id="spnFileName" runat="server">*</span></br>
								<input id="fileATTACH_FILE_NAME"  onchange="RemoveSpecialChar(document.getElementById('fileATTACH_FILE_NAME').value,document.getElementById('revATTACH_FILE'));RemoveExecutableFiles(document.getElementById('fileATTACH_FILE_NAME').value,document.getElementById('revATTACH_FILE_EXT'));AllowEXTFiles(document.getElementById('fileATTACH_FILE_NAME').value,document.getElementById('revATTACH_FILE_PDF'));" 
								type="file" size="70" runat="server"><br>
                                <asp:label id="lblATTACH_FILE_NAME" Runat="server"  ></asp:label>                                   
                                                                   
                                   
                                    </br>
									
									<asp:requiredfieldvalidator id="rfvATTACH_FILE_NAME" runat="server" Display="Dynamic" ControlToValidate="fileATTACH_FILE_NAME"></asp:requiredfieldvalidator>
									<asp:regularexpressionvalidator id="revATTACH_FILE" Runat="server" ControlToValidate="fileATTACH_FILE_NAME" Display="Dynamic"></asp:regularexpressionvalidator>
											<asp:regularexpressionvalidator id="revATTACH_FILE_EXT" Runat="server" ControlToValidate="fileATTACH_FILE_NAME" Display="Dynamic"></asp:regularexpressionvalidator>
											<asp:regularexpressionvalidator id="revATTACH_FILE_PDF" Runat="server" ControlToValidate="fileATTACH_FILE_NAME" Display="Dynamic"></asp:regularexpressionvalidator>
									</td>
            
            </tr>
            
            <tr>
                <td class="midcolorr">                   
				    <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" 
                        onclick="btnSave_Click"></cmsb:cmsbutton>					                
                </td>
            </tr>
            <tr>
                <td>
                    <input id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server"/>		    						
				    <input id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server"/>								
				    <input id="hidPolicyVersionID" type="hidden" name="hidPolicyVersionID" runat="server"/>
				    <input id="hidPOL_CLAUSE_ID" type="hidden" name="hidPOL_CLAUSE_ID" runat="server"/>
				    <input id="hidCLAUSE_ID" type="hidden" name="hidCLAUSE_ID" runat="server"/>
				    <input id="hidCLAUSE_TITLE" type="hidden" name="hidCLAUSE_TITLE" runat="server"/>
				    <input id="hidCLAUSE_DESCRIPTION" type="hidden" name="hidCLAUSE_DESCRIPTION" runat="server"/>
				    <input id="hidTransactLabel" type="hidden" name="hidTransactLabel" runat="server" />
				     <input id="hidPageTitle" type="hidden" name="hidPageTitle" runat="server" />
				  	<input id="hidCmbSUSEPLOB1" type="hidden" name="hidPageTitle" runat="server" />
				  	<input id="hidDEFIND_ID" type="hidden" name="hidPageTitle" runat="server" />
				  	<input id="hidCLAUSE_ID1" type="hidden" name="hidPageTitle" runat="server" />
				  	<input id="hidIS_CHECKED" value="0" type="hidden" runat="server" />
				  	<input id="hidFormSaved" value="0" type="hidden" runat="server" />
				  	<input id="hidOldData" value="0" type="hidden" runat="server" />
				  	<input id="hidRootPath" value="0" type="hidden" runat="server" />
				  	<input id="hidCLAUSEID" value="0" type="hidden" runat="server" />
				  	<input id="hidATTACH_FILE_NAME" value="0" type="hidden" runat="server" />
				  	<input id="hidfileLink" value="0" type="hidden" runat="server" />
				  	<input id="hidAttach" value="0" type="hidden" runat="server" />
				  	<input id="hidpolicystatus" value="0" type="hidden" runat="server" />
				</td>            
            </tr>
        </table>   
    </form>
    <script type="text/javascript" language="javascript">
        try {
            document.title = document.getElementById('hidPageTitle').value;
        }
        catch (err) { }
    
    </script>
</body>
</html>