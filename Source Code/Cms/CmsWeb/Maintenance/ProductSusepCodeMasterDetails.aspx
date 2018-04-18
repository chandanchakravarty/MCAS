<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductSusepCodeMasterDetails.aspx.cs" Inherits="Cms.CmsWeb.Maintenance.ProductSusepCodeMasterDetails" %>

<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title>MNT_LOB_SUSEPCODE_MASTER</title>
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet"/ >
    <script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/calendar.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
	<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/json2.js"></script> 
    <script language="javascript" type="text/javascript">
        var jsaAppDtFormat = "<%=aAppDtFormat  %>";
        function ResetTheForm() {
            document.MNT_LOB_SUSEPCODE_MASTER.reset();
            return false;
        }
        function fnDateConvert(Date, dateFormate) {
            if (Date == "" || Date.length < 8) return "";
            var returnDate = '';
            var saperator = '/';
            var firstDate, secDate;
            var strDateFirst = Date.split("/");
            if (dateFormate.toLowerCase() == "dd/mm/yyyy") 
                returnDate = (strDateFirst[1].length != 2 ? '0' + strDateFirst[1] : strDateFirst[1]) + '/' + (strDateFirst[0].length != 2 ? '0' + strDateFirst[0] : strDateFirst[0]) + '/' + (strDateFirst[2].length != 4 ? '0' + strDateFirst[2] : strDateFirst[2]);
            if (dateFormate.toLowerCase() == "mm/dd/yyyy") 
                returnDate = Date
            return returnDate;
        }
	
        $(document).ready(function () {
            $('body').bind('keydown', function (event) {
                if (event.keyCode == '13') {
                    $("#btnSave").trigger('click');
                    return false;
                }
            });

            $("#btnSave").click(function () {
                // Initialize the object, before adding data to it.
                //  { } is declarative ,shorthand for new Object().
                if (Page_ClientValidate()) {
                    $('#btnSave').attr("disabled", "false");
                    $('#lblMessage').text('');
                    var ProductMasterInfo = {};
                    //debugger;
                    ProductMasterInfo.SUSEP_LOB_CODE = $("#txtSUSEP_LOB_CODE").val();
                    ProductMasterInfo.EFFECTIVE_FROM = fnDateConvert($("#txtEFFECTIVE_FROM").val(), jsaAppDtFormat);
                    ProductMasterInfo.EFFECTIVE_TO = fnDateConvert($("#txtEFFECTIVE_TO").val(), jsaAppDtFormat);
                    ProductMasterInfo.LOB_ID = $("#hidLOB_ID").val();
                    if ($("#hidLOB_SUSEPCODE_ID").val() == "NEW")
                        ProductMasterInfo.IS_ACTIVE = "N";
                    else
                        ProductMasterInfo.IS_ACTIVE = "U";
                    // Create a data transfer object (DTO) with the proper structure.
                    var DTO = { 'ProductMasterInfo': ProductMasterInfo };
                    CallPageMethod("SaveData", JSON.stringify(DTO), AjaxSucceeded, AjaxFailed);
                    $('#btnSave').removeAttr('disabled');
                    $('#btnDelete').show();  //changed by praveer TFS# 736


                }
                return false;
            });
            $("#btnDelete").click(function () {
                if (Page_ClientValidate()) {
                    $('#lblMessage').text('');
                    var ProductMasterInfo = {};
                    //debugger;
                    ProductMasterInfo.LOB_ID = $("#hidLOB_ID").val();
                    ProductMasterInfo.LOB_SUSEPCODE_ID = $("#hidLOB_SUSEPCODE_ID").val();
                    var DTO = { 'ProductMasterInfo': ProductMasterInfo };
                    CallPageMethod("DeleteData", JSON.stringify(DTO), AjaxSucceeded, AjaxFailed);
                }
                return false;
            });
            function CallPageMethod(fn, paramList, successFn, errorFn) {
                var pagePath = window.location.pathname;
                $.ajax({ type: "POST", url: pagePath + "/" + fn, contentType: "application/json; charset=utf-8",
                    data: paramList, dataType: "json", success: successFn, error: errorFn
                });
            }
            function AjaxSucceeded(result) {
                if (result.d.split("-")[0] == "Delete") {
                    $('#tbody').hide();
                    $('#hidFormSaved').val(result.d.split("-")[1]);
                    $('#hidLOB_ID').val(result.d.split("-")[2]);
                    $('#hidLOB_SUSEPCODE_ID').val("");
                    $('#lblDelete').text(result.d.split("-")[4]);
                    RefreshWebGrid(result.d.split("-")[1], result.d.split("-")[2]);
                }
                else {
                    $('#hidFormSaved').val(result.d.split("-")[0]);
                    $('#hidLOB_ID').val(result.d.split("-")[1]);
                    $('#hidLOB_SUSEPCODE_ID').val(result.d.split("-")[2]);
                    $('#lblMessage').text(result.d.split("-")[3]);
                    RefreshWebGrid(result.d.split("-")[0], result.d.split("-")[1]);
                }
            }
            function AjaxFailed(result) {
                $('#hidFormSaved').val(result.statusText);
            }
        });
         
    </script>

</head>
<body leftmargin="0" topmargin="0"  onload="ApplyColor();ChangeColor();">
    <form id="MNT_LOB_SUSEPCODE_MASTER"  method='post' runat="server">
    <div>
    
    <table id="Table1" cellspacing="2" cellpadding="0" width="100%" border="0" >
           <tr>
            <td  class="midcolorc" colspan="3"><asp:Label runat="server" ID="lblDelete" CssClass="errmsg"></asp:Label></td>
           </tr>
            <tbody runat="server" id="tbody">
             <tr>
                   <td  align="right" colspan="3">
	                 <asp:label id="lblFormLoadMessage" runat="server" Visible="False" CssClass="errmsg" ></asp:label>
	               </td>
	         </tr>    	    
             <tr>
               <td class="pageHeader" align="left" colspan="3" >		           
		           <asp:label id="lblManHeader" Runat="server" ></asp:label>
		       </td>
	        </tr>
    	
	    <tr>
		    <td class="midcolorc" colspan="3"><asp:label id="lblMessage" runat="server" CssClass="errmsg" ></asp:label>
		    </td>
	    </tr>
    	
	   
	    
	    <tr id="trSUSEPCODE" runat="server">
		  <td class="midcolora" Width="33%">
		    <asp:Label runat="server" id="capSUSEP_LOB_CODE" Text="Product SUSEP Code:"></asp:Label><span class="mandatory">*</span>
		    <br />
		        <asp:TextBox ID="txtSUSEP_LOB_CODE" runat="server" MaxLength="4"  Width="240px"></asp:TextBox>
                <br />
		        <asp:RegularExpressionValidator ID="revSUSEP_LOB_CODE" runat="server" Display="Dynamic" ControlToValidate="txtSUSEP_LOB_CODE" ></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfvSUSEP_LOB_CODE" runat="server" Display="Dynamic" ControlToValidate="txtSUSEP_LOB_CODE"></asp:RequiredFieldValidator>

		    <br />
		  </td>
		  <td class="midcolora" Width="34%" >
		    <asp:Label runat="server" id="capEFFECTIVE_FROM" Text="Effective From:"></asp:Label><span class="mandatory">*</span>
		    <br />
		        <asp:TextBox ID="txtEFFECTIVE_FROM" runat="server" MaxLength="10" Width="150px"></asp:TextBox>  
                <asp:hyperlink id="hlkEFFECTIVE_FROM" runat="server" CssClass="HotSpot">
			    <asp:image id="imgEFFECTIVE_FROM" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image></asp:hyperlink>
			    <br />
		        <asp:RegularExpressionValidator ID="revEFFECTIVE_FROM" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_FROM" ></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfvEFFECTIVE_FROM" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_FROM"></asp:RequiredFieldValidator>
		  </td>	
		  <td class="midcolora" Width="33%" >
		    <asp:Label ID="capEFFECTIVE_TO" runat="server" Text="Effective To:"></asp:Label><span class="mandatory">*</span>
            <br />
		        <asp:TextBox ID="txtEFFECTIVE_TO" runat="server" MaxLength="10" Width="150px" ></asp:TextBox>
		        <asp:hyperlink id="hlkEFFECTIVE_TO" runat="server" CssClass="HotSpot">
			    <asp:image id="imgEFFECTIVE_TO" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image></asp:hyperlink>
			    <br />
		        <asp:RegularExpressionValidator ID="revEFFECTIVE_TO" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_TO" ></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfvEFFECTIVE_TO" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_TO"></asp:RequiredFieldValidator>
		    </td>	
	   </tr >
       <tr >
	        <td class="midcolora" id="tdReset" runat="server" colspan="1" >
	            <cmsb:CmsButton  runat="server"  ID="btnReset" Text="Reset" CssClass="clsButton"  CausesValidation="false" />
	        </td> 
            <td class="midcolora" id="td1" runat="server" colspan="1" >
            </td>
	        <td class="midcolorr" id="tdSave" runat="server" colspan="1" > 
	            <input  type="hidden" runat="server" ID="hidFormSaved"  value=""  name="hidFormSaved"/>  
	            <input  type="hidden" runat="server" ID="hidCALLED_FROM"  value=""  name="hidCALLED_FROM"/>   
	            <input type="hidden" runat="server" id="hidLOB_ID" value="" name="hidLOB_ID"/>
                <input type="hidden" runat="server" id="hidLOB_SUSEPCODE_ID" value="" name="hidLOB_SUSEPCODE_ID"/>
                <input type="hidden" runat="server" id="hidDataRowCount" value="0" name="hidDataRowCount"/>
	            <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" causesvalidation="false" ></cmsb:cmsbutton>
                <cmsb:CmsButton  runat="server" ID="btnSave" Text="Save"  CssClass="clsButton"  CausesValidation="true" />
 	        </td>
	    </tr>
	    </tbody>
	   </table>
    </div>
    </form>
</body>
</html>
