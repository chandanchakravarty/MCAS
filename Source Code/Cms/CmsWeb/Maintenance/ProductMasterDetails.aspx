<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductMasterDetails.aspx.cs" Inherits="Cms.CmsWeb.Maintenance.ProductMasterDetails" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>MNT_LOB_MASTER</title>
    
     <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet"/ >
        <script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/calendar.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/json2.js"></script> 
		<script language="javascript" type="text/javascript">
		    function Init() {
		        ApplyColor();
		        ChangeColor();
		        setTab();
		    }
            //Added by Pradeep on 09-Aug-2011 
            //Set the Tab for Product Susep Code master details - itrack-1493
		    function setTab() {
		         
		        if (document.getElementById('hidLOB_ID') != null && document.getElementById('hidLOB_ID').value != "NEW" && document.getElementById('hidLOB_ID').value != "") {
		            
		            if (document.getElementById('hidLOB_ID') != null)
		                Lob_id = document.getElementById('hidLOB_ID').value;

		            var TAB_TITLES = document.getElementById('hidTAB_TITLES').value; // changed by praveer TFS# 736
		            var tabtitles = TAB_TITLES.split(','); // changed by praveer TFS# 736

		            Url = "ProductSusepCodeMasterIndex.aspx?pageTitle=" + tabtitles +"&";
		            DrawTab(2, top.frames[1], tabtitles, Url);
		        }
		        else {
		            RemoveTab(2, top.frames[1]);

		        }
		        return false;
		    }
            //Added till here 
             function ResetTheForm() {
                document.MNT_LOB_MASTER.reset();
             }
             
		 </script>
		 <script language="javascript" type="text/javascript">

		     $(document).ready(function() {
		         $('body').bind('keydown', function(event) {

		             if (event.keyCode == '13') {
		                 $("#btnSave").trigger('click');
		                 return false;
		             }
		         });
		         //Added by Pradeep Kushwaha on 17-Oct-2010
		         $("#lstbSUSEP_PROCESS_NO").dblclick(function() {
		             $("#lstbSUSEP_PROCESS_NO option:selected").remove();
		         }
                 );
		         $("#btnAddProcessNo").click(function(e) {
		             var itemExists = false;
		             e.preventDefault();
		             $("#lstbSUSEP_PROCESS_NO option").each(function() {
		                 if ($(this).text() == $.trim($("#txtSUSEP_PROCESS_NO").val())) {
		                     itemExists = true;
		                 }
		             });
		             if (!itemExists) {
		                 if ($('input[name=txtSUSEP_PROCESS_NO]').val().trim() != '')
		                     $("#lstbSUSEP_PROCESS_NO").append("<option value=" + $("#hidLOB_ID").val() + ">" + $("#txtSUSEP_PROCESS_NO").val() + "</option>");
		             }
		             return false;
		         });


		         $("#btnSave").click(function() {
		             // Initialize the object, before adding data to it.
		             //  { } is declarative ,shorthand for new Object().

		             if (Page_ClientValidate()) {
		                 $('#btnSave').attr("disabled", "false");

		                 $('#lblMessage').text('');
		                 var ProductMasterInfo = {};
		                 //debugger;
		                 ProductMasterInfo.LOB_CODE = $("#txtLOB_CODE").val();
		                 ProductMasterInfo.LOB_DESC = $("#txtLOB_DESC").val();
		                 ProductMasterInfo.LOB_CATEGORY = $("#txtLOB_CATEGORY").val();
		                 ProductMasterInfo.LOB_TYPE = $("#txtLOB_TYPE").val();
		                 ProductMasterInfo.LOB_ACORD_STD = $("#txtLOB_ACORD_STD").val();
		                 ProductMasterInfo.LOB_PREFIX = $("#txtLOB_PREFIX").val();
		                 ProductMasterInfo.LOB_SUFFIX = $("#txtLOB_SUFFIX").val();
		                 ProductMasterInfo.SUSEP_LOB_ID = CCcmbSUSEP_LOB_DESC.getValue(); //Get the Lob id from combobox
		                 ProductMasterInfo.SUSEP_LOB_CODE = $("#CCcmbSUSEP_LOB_DESC").val().split("-")[0]; //Get the Lob Code from combobox
		                 ProductMasterInfo.COMMISSION_LEVEL = CCcmbCOMMISSION_LEVEL.getValue(); // $("#CCcmbCOMMISSION_LEVEL").val();
		                 ProductMasterInfo.ADMINISTRATIVE_EXPENSE = $("#txtADMINISTRATIVE_EXPENSE").val();
		                 var AppCommSelectedValue = '';
		                 $('#lstbAPPLICABLE_COMMISSION :selected').each(function(i, selected) {

		                     AppCommSelectedValue = AppCommSelectedValue + $(selected).val() + ",";
		                     if (AppCommSelectedValue.length > 0)
		                         var commaindex = AppCommSelectedValue.lastIndexOf(',');
		                     if (commaindex != -1) {
		                         ProductMasterInfo.APPLICABLE_COMMISSION = AppCommSelectedValue.substring(0, (AppCommSelectedValue.length - (AppCommSelectedValue.length - commaindex)));
		                     }
		                 });

		                 var SUSEP_PROCESS_NO = '';
		                 $("#lstbSUSEP_PROCESS_NO option").each(function() {

		                     SUSEP_PROCESS_NO = SUSEP_PROCESS_NO + $(this).text() + "~";
		                     if (SUSEP_PROCESS_NO.length > 0)
		                         var commaindex = SUSEP_PROCESS_NO.lastIndexOf('~');
		                     if (commaindex != -1) {
		                         ProductMasterInfo.SUSEP_PROCESS_NUMBERS = SUSEP_PROCESS_NO.substring(0, (SUSEP_PROCESS_NO.length - (SUSEP_PROCESS_NO.length - commaindex)));
		                     }
		                 });

		                 // Create a data transfer object (DTO) with the proper structure.
		                 var DTO = { 'ProductMasterInfo': ProductMasterInfo };		                
		                 CallPageMethod("SaveData", JSON.stringify(DTO), AjaxSucceeded, AjaxFailed);
		                 $('#btnSave').removeAttr('disabled');

		             }
		             return false;

		         });
		     });


  	
		     function CallPageMethod(fn, paramList, successFn, errorFn) {
                var pagePath = window.location.pathname;
		        $.ajax({type: "POST",url: pagePath + "/" + fn,contentType: "application/json; charset=utf-8",
		        data: paramList,dataType: "json",success: successFn,error: errorFn});
		     }
		     function AjaxSucceeded(result) {
		         
		         $('#hidFormSaved').val(result.d.split("-")[0]);
                 $('#hidLOB_ID').val(result.d.split("-")[1]);
                 $('#lblMessage').text(result.d.split("-")[2]);

                 RefreshWebGrid(result.d.split("-")[0], result.d.split("-")[1]);
		        
		     }
		     function AjaxFailed(result) {

		         $('#hidFormSaved').val(result.statusText);
		     }

		     function FormatAmountForSum(num) {

		         num = ReplaceAll(num, sBaseDecimalSep, '.');
		         return num;
		     }
		     function validateLimit(objSource, objArgs) {
		         if (document.getElementById('revADMINISTRATIVE_EXPENSE').isvalid == false)
             return
		         var Limt = document.getElementById(objSource.controltovalidate).value;

		         Limt = FormatAmountForSum(Limt);
		         if (parseFloat(Limt) >= 0 && parseFloat(Limt) <= 100)
		             objArgs.IsValid = true;
		         else
		             objArgs.IsValid = false;
		     }
		     
		    
		     
   </script>
	 
</head>
<body leftmargin="0" topmargin="0"  onload="Init();" >
		<form id='MNT_LOB_MASTER' method='post' runat='server'>
        <ext:ScriptManager ID="ScriptManager1" runat="server"></ext:ScriptManager>
     
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
	    
	    <tr>
		    <td class="midcolora" colspan="3" >
		    <br />
		     </td>
	    </tr>
	    
	    <tr>
		    <td class="midcolora" Width="33%">
		    <asp:Label runat="server" id="capLOB_CODE"></asp:Label>
		    <br />
		        <asp:TextBox ID="txtLOB_CODE" runat="server" ReadOnly="true" Width="240px"></asp:TextBox>
		    <br />
		    </td>
		    <td class="midcolora" Width="34%" >
		    <asp:Label runat="server" id="capLOB_DESC"></asp:Label>
		    <br />
		        <asp:TextBox ID="txtLOB_DESC" runat="server" MaxLength="70" Width="240px"></asp:TextBox>  <br />
		        <asp:RegularExpressionValidator ID="revLOB_DESC" runat="server" Display="Dynamic" ControlToValidate="txtLOB_DESC" ></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfvLOB_DESC" runat="server" Display="Dynamic" ControlToValidate="txtLOB_DESC"></asp:RequiredFieldValidator>
		  </td>	
		    <td class="midcolora" Width="33%" >
		    <asp:Label ID="capLOB_CATEGORY" runat="server"></asp:Label>
               <br />
		        <asp:TextBox ID="txtLOB_CATEGORY" runat="server" MaxLength="10" Width="240px" 
                    ReadOnly="True"></asp:TextBox>
		    <br />
		    <asp:RegularExpressionValidator ID="revLOB_CATEGORY" runat="server" Display="Dynamic" ControlToValidate="txtLOB_CATEGORY" ></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfvLOB_CATEGORY" runat="server" Display="Dynamic" ControlToValidate="txtLOB_CATEGORY"></asp:RequiredFieldValidator>
		    </td>	
		    
	  </tr>
	    <tr id="td2">
		    <td class="midcolora" Width="33%" >
		      <asp:Label ID="capLOB_TYPE" runat="server"></asp:Label>
                <br />
		        <asp:TextBox ID="txtLOB_TYPE" runat="server" MaxLength="1" Width="240px" 
                    ReadOnly="True"></asp:TextBox>
		    <br />
		        <asp:RegularExpressionValidator ID="revLOB_TYPE" runat="server" Display="Dynamic" ControlToValidate="txtLOB_TYPE" ></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfvLOB_TYPE" runat="server" Display="Dynamic" ControlToValidate="txtLOB_TYPE"></asp:RequiredFieldValidator>
                
		    </td>	
		    
		     <td class="midcolora" Width="33%" >
		   
		       <asp:Label runat="server" id="capLOB_ACORD_STD"></asp:Label>
		        <br />
		         <asp:TextBox ID="txtLOB_ACORD_STD" runat="server" ReadOnly="true" Width="240px"></asp:TextBox>
		    <br />
            
		    </td>	
		     <td class="midcolora" Width="33%" >
		      <asp:Label ID="capLOB_PREFIX" runat="server"></asp:Label>
		    <br />
		         <asp:TextBox ID="txtLOB_PREFIX" runat="server" MaxLength="5" Width="240px" 
                     ReadOnly="True"></asp:TextBox>
		    <br />
                <asp:RegularExpressionValidator ID="revLOB_PREFIX" runat="server" Display="Dynamic" ControlToValidate="txtLOB_PREFIX" ></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfvLOB_PREFIX" runat="server" Display="Dynamic" ControlToValidate="txtLOB_PREFIX"></asp:RequiredFieldValidator>
		    </td>	
	    </tr>
	    <tr >
	        <td class="midcolora" Width="33%" >
	        
		      <asp:Label ID="capLOB_SUFFIX" runat="server"></asp:Label>
	         <br />
		        <asp:TextBox ID="txtLOB_SUFFIX" runat="server" MaxLength="5" Width="240px" 
                    ReadOnly="True"></asp:TextBox>
		    <br />
		     <asp:RegularExpressionValidator ID="revLOB_SUFFIX" runat="server" Display="Dynamic" ControlToValidate="txtLOB_SUFFIX" ></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfvLOB_SUFFIX" runat="server" Display="Dynamic" ControlToValidate="txtLOB_SUFFIX"></asp:RequiredFieldValidator>
	        </td>
	        <td class="midcolora" Width="34%" >
	         <asp:Label ID="capSUSEP_LOB_DESC" runat="server"></asp:Label>
	                 <br />
		            <ext:Store ID="StoreSUSEP_LOB_DESC" runat="server" >
                        <Reader>
                            <ext:JsonReader ReaderID="Id">
                                <Fields>
                                    <ext:RecordField Name="SUSEP_LOB_ID" Type="String" Mapping="SUSEP_LOB_ID" />
                                    <ext:RecordField Name="SUSEP_LOB_CODE" Type="String" Mapping="SUSEP_LOB_CODE" />
                                    <ext:RecordField Name="SUSEP_LOB_DESC" Type="String" Mapping="SUSEP_LOB_DESC" />
                                </Fields>
                            </ext:JsonReader>
                        </Reader> 
                      <Listeners>
                            <Load Handler="#{CCcmbSUSEP_LOB_DESC}.setValue(this.getAt(0).data[#{CCcmbSUSEP_LOB_DESC}.valueField]);#{CCcmbSUSEP_LOB_DESC}.fireEvent('select', #{StoreSUSEP_LOB_DESC}.getAt(0), 0);" Single="false" />
                       </Listeners>      
                    </ext:Store>
            
		             <ext:ComboBox ID="CCcmbSUSEP_LOB_DESC"  AllowBlank="false"  ForceSelection="true"  ItemCls="required" FieldClass="midcolora"  Resizable="true" Editable="false" runat="server" Width="244px" ></ext:ComboBox>
		    <br />
		     
    	 </td>
           <td class="midcolora" Width="33%" >
    	        <asp:Label ID="capCOMMISSION_LEVEL" runat="server"></asp:Label> 
                <ext:Store ID="StoreCOMMISSION_LEVEL" runat="server">
                    <Reader>
                    <ext:ArrayReader>
                            <Fields>
                                <ext:RecordField Name="LookupCode" />
                                <ext:RecordField Name="LookupDesc" />
                            </Fields>
                     </ext:ArrayReader>
                      </Reader> 
                   <Listeners>
                               <Load  Handler="#{CCcmbCOMMISSION_LEVEL}.setValue(this.getAt(0).data[#{CCcmbCOMMISSION_LEVEL}.valueField]);#{CCcmbCOMMISSION_LEVEL}.fireEvent('select', #{StoreCOMMISSION_LEVEL}.getAt(0), 0);" Single="false" />
                      </Listeners> 
                    </ext:Store>
                <ext:ComboBox ID="CCcmbCOMMISSION_LEVEL" FieldClass="midcolora" Editable="false"  runat="server"></ext:ComboBox>
                
	        </td>
	        
	    </tr>
	    
	    <tr >
	        <td class="midcolora" Width="33%" >
	        
		      <asp:Label ID="capAPPLICABLE_COMMISSION" runat="server"></asp:Label><br />
                <asp:ListBox ID="lstbAPPLICABLE_COMMISSION" runat="server"   
                    SelectionMode="Multiple" Width="240px"></asp:ListBox><br />
                <br />
	        </td>
	        <TD width="33%" class="midcolora" valign="top">
                                                <asp:label id="capADMINISTRATIVE_EXPENSE" runat="server"></asp:label>
                                               <br />
                                                <asp:textbox id="txtADMINISTRATIVE_EXPENSE"  style="TEXT-ALIGN: right" 
                                                    CssClass="INPUTCURRENCY" runat="server" size="32" maxlength="5" 
                                                    Width="90px"></asp:textbox>
                                                <br />
                                               <%-- <asp:RequiredFieldValidator ID="rfvADMINISTRATIVE_EXPENSE" runat="server" Display="Dynamic"
                                ErrorMessage="Administrative Expense can't be blank." ControlToValidate="txtADMINISTRATIVE_EXPENSE"></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator ID="revADMINISTRATIVE_EXPENSE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                                    ControlToValidate="txtADMINISTRATIVE_EXPENSE"></asp:RegularExpressionValidator><asp:CustomValidator
                                        ID="csvADMINISTRATIVE_EXPENSE" Display="Dynamic" ControlToValidate="txtADMINISTRATIVE_EXPENSE"
                                        ClientValidationFunction="validateLimit" runat="server"></asp:CustomValidator>
                                                </TD>
	        
	        <td class="midcolora" >
	         </td>
            
	    </tr>
	    
	    <tr >
	        <td class="midcolora" Width="33%" >
	        
                       <asp:Label ID="capSUSEP_PROCESS_NO" runat="server"></asp:Label><br />
	            <asp:TextBox ID="txtSUSEP_PROCESS_NO" runat="server" MaxLength="25" Width="240px"  ></asp:TextBox><br />
	            <br />
	              <cmsb:CmsButton  runat="server" ID="btnAddProcessNo" Text="Add SUSEP Process No"  CssClass="clsButton"  CausesValidation="true" />
	        </td>
	        <td class="midcolora" colspan="2"  >
	         
	            
	           
	            <asp:ListBox ID="lstbSUSEP_PROCESS_NO" runat="server" SelectionMode="Multiple" 
                    Width="240px" Rows="8"></asp:ListBox>
                
               
	            
            </td>
            
	    </tr>
	    
	        <td class="midcolora" >
	            <cmsb:CmsButton  runat="server"  ID="btnReset" Text="Reset"  
                    CssClass="clsButton"  CausesValidation="false" onclick="btnReset_Click"/>


	        </td> 
	        <td class="midcolorr" colspan="2" > 
	            
	            <input  type="hidden" runat="server" ID="hidFormSaved"  value=""  name="hidFormSaved"/>  
	            <input  type="hidden" runat="server" ID="hidCALLED_FROM"  value=""  name="hidCALLED_FROM"/>   
	            <input  type="hidden" runat="server" ID="hidLOB_ID"  value=""  name="hidLOB_ID"/>   
                <input  type="hidden" runat="server" ID="hidTAB_TITLES"  value=""  name="hidTAB_TITLES"/>    <%--changed by praveer TFS# 736--%>
	            
                <cmsb:CmsButton  runat="server" ID="btnSave" Text="Save"  CssClass="clsButton"  CausesValidation="true" />
 	        </td>
	    </tr>
	    </tbody>
	   </table>
		</form>
		<script type="text/javascript" language="javascript">
		     
		    if (document.getElementById('hidCALLED_FROM').value != "true") {
		       
		        RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidLOB_ID').value);
		    }
		    
		</script>
	</body>
</html>
