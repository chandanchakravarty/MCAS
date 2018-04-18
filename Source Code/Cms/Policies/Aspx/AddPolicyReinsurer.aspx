<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPolicyReinsurer.aspx.cs" Inherits="Cms.Policies.Aspx.AddPolicyReinsurer" %>--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPolicyReinsurer.aspx.cs" validateRequest="false" Inherits="Cms.Policies.Aspx.AddPolicyReinsurer" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>POL_REINSURANCE_INFO</title>
     <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
     <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
      <LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet" />      
    	<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
	    <script type="text/javascript" src="/cms/cmsweb/scripts/Calendar.js"></script>
         <STYLE>
            .hide { OVERFLOW: hidden; TOP: 5px }
            .show { OVERFLOW: hidden; TOP: 5px }
            #tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
	<script language="javascript" type="text/javascript">

	    function initPage() {
	        ApplyColor();
	    }

	    function FormatAmountForSum(num) {
	        num = ReplaceAll(num, sGroupSep, '');
	        num = ReplaceAll(num, sDecimalSep, '.');
	        return num;
	    }

	    function onRowClicked(num, msDg) {

	        rowNum = num;
	        if (parseInt(num) == 0)
	            strXML = "";
	        populateXML(num, msDg);
	        changeTab(0, 0);
	    }
	    
	    function Validate(objSource, objArgs) {

	        var comm = document.getElementById(objSource.controltovalidate).value;
	        comm = FormatAmountForSum(comm);
	        if (parseFloat(comm) < 0 || parseFloat(comm) > 100) {
	            document.getElementById(objSource.controltovalidate).select();
	            objArgs.IsValid = false;
	        }
	        else
	            objArgs.IsValid = true;
	    }

	    function check() {
	        var confirmmessage = '<%= confirmmessage %>';
	        var alertMsg = '<%= selectChk %>';
	        var frm = document.POL_REINSURANCE_INFO;
	        var boolAllChecked;
	        boolAllChecked = false;
	        for (i = 0; i < frm.length; i++) {

	            e = frm.elements[i];
	            if (e.type == 'checkbox' && e.name.indexOf('chkSELECT') != -1)
	                if (e.checked == true) {

	                boolAllChecked = true;
	                break;
	            }
	        }
	        if (boolAllChecked == false) {
	            alert(alertMsg);
	            return false;
	        }
	        else {
	            var k = confirm(confirmmessage);
	            return k;
	        }
	    }

	    function checksave() {
	       //debugger;
	        var confirmmessage = '<%= confirmmessage %>';
	        var alertMsg = '<%= selectChk %>';
	        var frm = document.POL_REINSURANCE_INFO;
	        var boolAllChecked;
	        boolAllChecked = false;
	        validatordisable();
	        for (i = 0; i < frm.length; i++){
	            e = frm.elements[i];
	            if (e.type == 'checkbox' && e.name.indexOf('chkSELECT') != -1)
	                if (e.checked == true) {
	                boolAllChecked = true;
	                var splitid = e.id.split('_');
	                var rfvREINSURANCE_CEDED = splitid[0] + '_' + splitid[1] + '_' + 'rfvREINSURANCE_CEDED';
	                var rfvREINSURANCE_COMMISSION = splitid[0] + '_' + splitid[1] + '_' + 'rfvREINSURANCE_COMMISSION';
	                var rfvREINSURER_NUMBER = splitid[0] + '_' + splitid[1] + '_' + 'rfvREINSURER_NUMBER'; //ashish
	                var rfvCONTRACT_FACULTATIVE = splitid[0] + '_' + splitid[1] + '_' + 'rfvCONTRACT_FACULTATIVE';
	                var txtREINSURANCE_CEDED = splitid[0] + '_' + splitid[1] + '_' + 'txtREINSURANCE_CEDED';
	                var txtREINSURANCE_COMMISSION = splitid[0] + '_' + splitid[1] + '_' + 'txtREINSURANCE_COMMISSION';
	                var txtREINSURER_NUMBER = splitid[0] + '_' + splitid[1] + '_' + 'txtREINSURER_NUMBER';//ashish
	                var cmbCONTRACT_FACULTATIVE = splitid[0] + '_' + splitid[1] + '_' + 'cmbCONTRACT_FACULTATIVE';
	                var revREINSURANCE_CEDED = splitid[0] + '_' + splitid[1] + '_' + 'revREINSURANCE_CEDED';
	                var revREINSURANCE_COMMISSION = splitid[0] + '_' + splitid[1] + '_' + 'revREINSURANCE_COMMISSION';
	                //var revREINSURER_NUMBER = splitid[0] + '_' + splitid[1] + '_' + 'revREINSURER_NUMBER';//ashish
	                var csvREINSURANCE_CEDED = splitid[0] + '_' + splitid[1] + '_' + 'csvREINSURANCE_CEDED';
	                var csvREINSURANCE_COMMISSION = splitid[0] + '_' + splitid[1] + '_' + 'csvREINSURANCE_COMMISSION';
	                //var csvREINSURER_NUMBER = splitid[0] + '_' + splitid[1] + '_' + 'csvREINSURER_NUMBER';//ashish
	                //var cmbMAX_NO_INSTALLMENT = splitid[0] + '_' + splitid[1] + '_' + 'cmbMAX_NO_INSTALLMENT'; //Added by Aditya for TFS BUG # 2514
	               // var rfvMAX_NO_INSTALLMENT = splitid[0] + '_' + splitid[1] + '_' + 'rfvMAX_NO_INSTALLMENT'; //Added by Aditya for TFS BUG # 2514
	                var cmbRISK_ID = splitid[0] + '_' + splitid[1] + '_' + 'cmbRISK_ID'; //Added by Aditya for TFS BUG # 2514
	                var rfvRISK_ID = splitid[0] + '_' + splitid[1] + '_' + 'rfvRISK_ID'; //Added by Aditya for TFS BUG # 2514
	                if (document.getElementById(txtREINSURANCE_CEDED).value == '') {
	                    document.getElementById(rfvREINSURANCE_CEDED).setAttribute('enabled', true);
	                    document.getElementById(rfvREINSURANCE_CEDED).setAttribute('isValid', false);
	                    document.getElementById(rfvREINSURANCE_CEDED).style.display = "inline";
	                }                   

                    if (document.getElementById(cmbRISK_ID).value == '') { 
                        document.getElementById(rfvRISK_ID).setAttribute('enabled', true);
                        document.getElementById(rfvRISK_ID).setAttribute('isValid', false);
                        document.getElementById(rfvRISK_ID).style.display = "inline";

                    }
	                else {
	                    document.getElementById(revREINSURANCE_CEDED).setAttribute('enabled', true);
	                    document.getElementById(revREINSURANCE_CEDED).setAttribute('isValid', false);
	                    document.getElementById(revREINSURANCE_CEDED).style.display = "inline";
	                    document.getElementById(csvREINSURANCE_CEDED).setAttribute('enabled', true);
	                    document.getElementById(csvREINSURANCE_CEDED).setAttribute('isValid', false);
	                    document.getElementById(csvREINSURANCE_CEDED).style.display = "inline";




	                }

	                if (document.getElementById(txtREINSURANCE_COMMISSION).value == '') {
	                    document.getElementById(rfvREINSURANCE_COMMISSION).setAttribute('enabled', true);
	                    document.getElementById(rfvREINSURANCE_COMMISSION).setAttribute('isValid', false);
	                    document.getElementById(rfvREINSURANCE_COMMISSION).style.display = "inline";

	                }
	                else {
	                    document.getElementById(revREINSURANCE_COMMISSION).setAttribute('enabled', true);
	                    document.getElementById(revREINSURANCE_COMMISSION).setAttribute('isValid', false);
	                    document.getElementById(revREINSURANCE_COMMISSION).style.display = "inline";
	                    document.getElementById(csvREINSURANCE_COMMISSION).setAttribute('enabled', true);
	                    document.getElementById(csvREINSURANCE_COMMISSION).setAttribute('isValid', false);
	                    document.getElementById(csvREINSURANCE_COMMISSION).style.display = "inline";

	                }

	               if (document.getElementById(cmbCONTRACT_FACULTATIVE).value == 0) {
	                    document.getElementById(rfvCONTRACT_FACULTATIVE).setAttribute('enabled', true);
	                    document.getElementById(rfvCONTRACT_FACULTATIVE).setAttribute('isValid', false);
	                    document.getElementById(rfvCONTRACT_FACULTATIVE).style.display = "inline";
	                }
	                Page_ClientValidate();

	            } 
	        }
	        if (boolAllChecked == false) {
	            alert(alertMsg);
	            return false;
	        }

	    }
	    function validatordisable() {
	        if (typeof (Page_Validators) == "undefined")
	            return;
	        var i, val;
	        for (i = 0; i < Page_Validators.length; i++) {
	            val = Page_Validators[i];
	           val.setAttribute('enabled', false);
	         }

	   }
	   function SelectReinsurar() {
	       if (document.getElementById('cmbREIN_COMAPANY_NAME').value == "") {
               var message = document.getElementById("hidmessage1").value;
               document.getElementById('lblMessage').innerHTML = message;
	           return false;
	       } else
	           return true;
	   }

	   function validateLengthOfAmount(objSource, objArgs) { 
//	       if (document.getElementById('txtCOMM_AMOUNT').isvalid == false || document.getElementById('revLAYER_AMOUNT').isvalid == false || document.getElementById('revREIN_PREMIUM').isvalid == false)
//	           return
	       var amtlength = document.getElementById(objSource.controltovalidate).value;
	       if (parseFloat(amtlength) > parseFloat('9999999999999.99') || parseFloat(amtlength) < parseFloat('00.00')) {
	           objArgs.IsValid = false;
	       }
	       else {
	           objArgs.IsValid = true;
	       }
	   }

//	   function showTab() {

//	       if (document.getElementById('hidPolicyStatus').value == "COMPLETE") {
//	           changeTab(0, 0);
//	       }
////	       else {
////	           RemoveTab(0, frames[1]);
////           }
//	   }


	   
	</script>
</head>
<body leftMargin="0"  rightMargin="0" onload="ApplyColor();ChangeColor();setfirstTime();" MS_POSITIONING="GridLayout">
    <form id="POL_REINSURANCE_INFO" runat="server" method="post">


        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0" >
          <tr>
                 <td class="headereffectCenter">
					
					<asp:label id="lblTitle" runat="server"></asp:label><%--Reinsurance--%>
					</td>
              <td class="midcolorc" align="right" colspan="4">
	            <asp:label id="lblFormLoadMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label>
	          </td>
	      </tr>
          <tr>
               <td class="midcolorc" align="right" colspan="4">
	      </td>
	      </tr>
	      <tr>
	         <td> 
	            <table id="Table2" width="100%" align="center" border="0">
	                <tr>
						<td class="pageHeader" >
						<br />
						<asp:label id="lblManHeader" Runat="server" colspan="2"></asp:label>
						</td>
					</tr>
					<tr>
						<td class="midcolorc" align="center" colspan="2" ><asp:label id="lblMessage" runat="server" CssClass="errmsg"></asp:label>
						</td>
					</tr>
					<tr>
						<td class="midcolorc"  style="text-align:left"  width="50%">
						<asp:Label runat="server" ID="capREIN_COMAPANY_NAME" ></asp:Label>
						<br />
						<asp:DropDownList runat="server"  ID="cmbREIN_COMAPANY_NAME" >
						<asp:ListItem  Value="0" Text=""></asp:ListItem>						
						</asp:DropDownList><cmsb:CmsButton runat="server" ID="btnSelect" CausesValidation="false"
                                CssClass="clsButton" onclick="btnSelect_Click" OnClientClick="javascript:return SelectReinsurar();" />
                            
                            
                            </td>
                         
                             <td class="midcolora"   width="50%">
                                    <asp:Label runat = "server" ID="capDISREGARD_RI_CONTRACT" >Reinsurance Contract Applicable</asp:Label><br />
                                    <asp:DropDownList runat="server" id= "cmbDISREGARD_RI_CONTRACT">                                    
                                    </asp:DropDownList>
                             </td>
                         </tr>

                         <tr>  <%--Added by Aditya for tfs bug # 180--%>
                         <td colspan="2" class="midcolorc" style="text-align:right">
					                <cmsb:CmsButton ID="btnSave_Contract" Text="Save" runat="server" CausesValidation="true" 
                                            CssClass="clsButton" ></cmsb:CmsButton>
					                </td>
                         </tr> <%-- Added till here--%>

                         <tr>
                            <td colspan ="2"> 
                             
						
					        <asp:GridView  runat="server" ID="grdReinsurer" AutoGenerateColumns="False" 
                                Width="100%" onrowdatabound="grdReinsurer_RowDataBound">
					            <HeaderStyle CssClass="headereffectWebGrid"/>
					            <RowStyle CssClass="midcolorba" ></RowStyle>
					            <Columns>
					                <asp:TemplateField>	
						            <ItemStyle Width="2%" />					
						            <ItemTemplate>
						            <asp:CheckBox runat="server"  ID="chkSELECT" ></asp:CheckBox>						
						            </ItemTemplate>
						            </asp:TemplateField>
						            
						            <asp:TemplateField Visible="false" >						
						            <ItemTemplate >
						            <asp:Label runat="server" ID="capREINSURANCE_ID"  Text='<%# Eval("REINSURANCE_ID.CurrentValue") %>'></asp:Label>					
						            </ItemTemplate>
						            </asp:TemplateField>
						
						            <asp:TemplateField Visible="false" >						
						            <ItemTemplate>
						            <asp:Label runat="server" ID="capCOMPANY_ID"  Text='<%# Eval("COMPANY_ID.CurrentValue") %>'></asp:Label>					
						            </ItemTemplate>
						            </asp:TemplateField>
						            
						            <asp:TemplateField >
						            <ItemStyle Width="28%" />	
						            <HeaderTemplate >
						            <asp:Label runat="server" ID="capREINSURER_NAME"></asp:Label> 
						            </HeaderTemplate>
						            <ItemTemplate>
						            <asp:Label runat="server" ID="capREINSURER_NAME" Text='<%#Eval("REINSURER_NAME.CurrentValue") %>' ></asp:Label>						
						            </ItemTemplate> 
						            </asp:TemplateField>
						            
						            <asp:TemplateField >
						            <ItemStyle Width="15%" />	
						            <HeaderTemplate >
						            <asp:Label runat="server" ID="capCONTRACT_FACULTATIVE" ></asp:Label><span class="mandatory">*</span>
						            </HeaderTemplate>
					                <ItemTemplate>		
					                <asp:DropDownList runat="server" ID="cmbCONTRACT_FACULTATIVE" AutoPostBack="true"   OnSelectedIndexChanged="cmbCONTRACT_FACULTATIVE_SelectedIndexChanged" >
					                <asp:ListItem Text="" Value="0"></asp:ListItem>
					                </asp:DropDownList>	
					                <br />
					                <asp:requiredfieldvalidator id="rfvCONTRACT_FACULTATIVE" runat="server" ControlToValidate="cmbCONTRACT_FACULTATIVE" Display="Dynamic"></asp:requiredfieldvalidator>					     					  
    					            </ItemTemplate>
						            </asp:TemplateField>
						            
						            <asp:TemplateField >
						            <ItemStyle Width="20%" />	
						            <HeaderTemplate >
						            <asp:Label runat="server" ID="capCONTRACT" ></asp:Label>
						            </HeaderTemplate>
					                <ItemTemplate>		
					                <asp:DropDownList runat="server" ID="cmbCONTRACT"  AutoPostBack="true" OnSelectedIndexChanged="cmbCONTRACT_SelectedIndexChanged"  >
					                <asp:ListItem Text="" Value="0"></asp:ListItem>
					                </asp:DropDownList><br />
					                <%--<asp:requiredfieldvalidator id="rfvCONTRACT" runat="server" ControlToValidate="cmbCONTRACT" Display="Dynamic"></asp:requiredfieldvalidator>	--%>		     					  
    					            </ItemTemplate>
						            </asp:TemplateField>
						            
						            <asp:TemplateField >
						            <ItemStyle Width="15%" />	
						            <HeaderTemplate >
						            <asp:Label runat="server" ID="capREINSURANCE_CEDED"></asp:Label><span class="mandatory">*</span>
						            </HeaderTemplate>
					                <ItemTemplate >
					                <asp:TextBox runat="server" ID="txtREINSURANCE_CEDED" CausesValidation="true" CssClass="INPUTCURRENCY" MaxLength="7" size="10" AutoCompleteType="Disabled"  Text='<%#  Convert.ToDouble(Eval("REINSURANCE_CEDED.CurrentValue")).ToString("N", numberFormatInfo) %>'></asp:TextBox><br />
					                <asp:requiredfieldvalidator id="rfvREINSURANCE_CEDED" runat="server" ControlToValidate="txtREINSURANCE_CEDED" Display="Dynamic"></asp:requiredfieldvalidator>
				                    <asp:regularexpressionvalidator id="revREINSURANCE_CEDED" Runat="server" ControlToValidate="txtREINSURANCE_CEDED" Display="Dynamic"></asp:regularexpressionvalidator>
				                    <asp:customvalidator id="csvREINSURANCE_CEDED" Display="Dynamic" ControlToValidate="txtREINSURANCE_CEDED" ClientValidationFunction="Validate" Runat="server"></asp:customvalidator>
						            </ItemTemplate>
						            </asp:TemplateField>
						            
						            <asp:TemplateField >
						            <ItemStyle Width="15%" />	
						            <HeaderTemplate >
						            <asp:Label runat="server" ID="capREINSURANCE_COMMISSION"></asp:Label><span class="mandatory">*</span>
						            </HeaderTemplate>
					                <ItemTemplate >
					                <asp:TextBox runat="server" ID="txtREINSURANCE_COMMISSION" CausesValidation="true" MaxLength="7" size="10" AutoCompleteType="Disabled" CssClass="INPUTCURRENCY"  Text='<%#  Convert.ToDouble(Eval("REINSURANCE_COMMISSION.CurrentValue")).ToString("N", numberFormatInfo) %>'></asp:TextBox><br />
					                <asp:requiredfieldvalidator id="rfvREINSURANCE_COMMISSION" runat="server" ControlToValidate="txtREINSURANCE_COMMISSION" Display="Dynamic"></asp:requiredfieldvalidator>
				                    <asp:regularexpressionvalidator id="revREINSURANCE_COMMISSION" Runat="server" ControlToValidate="txtREINSURANCE_COMMISSION" Display="Dynamic"></asp:regularexpressionvalidator>
				                    <asp:customvalidator id="csvREINSURANCE_COMMISSION" Display="Dynamic" ControlToValidate="txtREINSURANCE_COMMISSION" ClientValidationFunction="Validate" Runat="server"></asp:customvalidator>
						            </ItemTemplate>
						            </asp:TemplateField>

                                     <asp:TemplateField >
						            <ItemStyle Width="15%" />	 <%--Added by Aditya for tfs bug # 177--%>
						            <HeaderTemplate >
						            <asp:Label runat="server" ID="capCOMM_AMOUNT">Comm Amount</asp:Label><%--<span class="mandatory">*</span>--%>
						            </HeaderTemplate>
					                <ItemTemplate >
					                <asp:TextBox runat="server" ID="txtCOMM_AMOUNT" CssClass="INPUTCURRENCY" onblur="formatAmount(this.value);ValidatorOnChange();" CausesValidation="true" size="14" MaxLength="12"  AutoCompleteType="Disabled"   Text='<%#  Convert.ToDouble(Eval("COMM_AMOUNT.CurrentValue")).ToString("N", numberFormatInfo) %>'></asp:TextBox><br />
					                <%--<asp:requiredfieldvalidator id="rfvCOMM_AMOUNT" runat="server" ControlToValidate="txtCOMM_AMOUNT" Display="Dynamic"></asp:requiredfieldvalidator>--%>
				                   <asp:regularexpressionvalidator id="revCOMM_AMOUNT" Runat="server" ControlToValidate="txtCOMM_AMOUNT" Display="Dynamic"></asp:regularexpressionvalidator>
                                   <asp:CustomValidator ID="csvCOMM_AMOUNT" Display="Dynamic" ControlToValidate="txtCOMM_AMOUNT"
                                                         ClientValidationFunction="validateLengthOfAmount" runat="server"></asp:CustomValidator>
                                   </ItemTemplate>
						            </asp:TemplateField>

                                     <asp:TemplateField >
						            <ItemStyle Width="15%" />	
						            <HeaderTemplate >
						            <asp:Label runat="server" ID="capLAYER_AMOUNT">Layer Amount</asp:Label><%--<span class="mandatory">*</span>--%>
						            </HeaderTemplate>
					                <ItemTemplate >
					                <asp:TextBox runat="server" ID="txtLAYER_AMOUNT" CausesValidation="true" size="14" MaxLength="12"  AutoCompleteType="Disabled"   Text='<%#  Convert.ToDouble(Eval("LAYER_AMOUNT.CurrentValue")).ToString("N", numberFormatInfo) %>'></asp:TextBox><br />
					                <%--<asp:requiredfieldvalidator id="rfvLAYER_AMOUNT" runat="server" ControlToValidate="txtLAYER_AMOUNT" Display="Dynamic"></asp:requiredfieldvalidator>--%>
                                    <asp:regularexpressionvalidator id="revLAYER_AMOUNT" Runat="server" ControlToValidate="txtLAYER_AMOUNT" Display="Dynamic"></asp:regularexpressionvalidator>
				                   <asp:CustomValidator ID="csvLAYER_AMOUNT" Display="Dynamic" ControlToValidate="txtLAYER_AMOUNT"
                                                         ClientValidationFunction="validateLengthOfAmount" runat="server"></asp:CustomValidator>
                                   </ItemTemplate>
						            </asp:TemplateField>

                                     <asp:TemplateField >
						            <ItemStyle Width="15%" />	
						            <HeaderTemplate >
						            <asp:Label runat="server" ID="capREIN_PREMIUM">Rein Premium</asp:Label><%--<span class="mandatory">*</span>--%>
						            </HeaderTemplate>
					                <ItemTemplate >
					                <asp:TextBox runat="server" ID="txtREIN_PREMIUM" CausesValidation="true" size="14" MaxLength="12"  AutoCompleteType="Disabled"   Text='<%#  Convert.ToDouble(Eval("REIN_PREMIUM.CurrentValue")).ToString("N", numberFormatInfo) %>'></asp:TextBox><br />
					               <%-- <asp:requiredfieldvalidator id="rfvREIN_PREMIUM" runat="server" ControlToValidate="txtREIN_PREMIUM" Display="Dynamic"></asp:requiredfieldvalidator>--%>
				                   <asp:regularexpressionvalidator id="revREIN_PREMIUM" Runat="server" ControlToValidate="txtREIN_PREMIUM" Display="Dynamic"></asp:regularexpressionvalidator>
                                   <asp:CustomValidator ID="csvREIN_PREMIUM" Display="Dynamic" ControlToValidate="txtREIN_PREMIUM"
                                                         ClientValidationFunction="validateLengthOfAmount" runat="server"></asp:CustomValidator>
                                   </ItemTemplate>
						            </asp:TemplateField>  <%--Added till here--%>
						           
						           <asp:TemplateField >
						            <ItemStyle Width="15%" />	
						            <HeaderTemplate >
						            <asp:Label runat="server" ID="capREINSURER_NUMBER"></asp:Label><span class="mandatory">*</span>
						            </HeaderTemplate>
					                <ItemTemplate >
					                <asp:TextBox runat="server" ID="txtREINSURER_NUMBER" CausesValidation="true" MaxLength="40" size="12"  AutoCompleteType="Disabled"   Text='<%# Eval("REINSURER_NUMBER.CurrentValue") %>'></asp:TextBox><br />
					                <asp:requiredfieldvalidator id="rfvREINSURER_NUMBER" runat="server" ControlToValidate="txtREINSURER_NUMBER" Display="Dynamic"></asp:requiredfieldvalidator>
				                   </ItemTemplate>
						            </asp:TemplateField>

                                    <asp:TemplateField >   <%--Added by Aditya for TFS BUG # 2514--%>
						            <ItemStyle Width="15%" />	
						            <HeaderTemplate >
						            <asp:Label runat="server" ID="capMAX_NO_INSTALLMENT" size="10">Maximumm Installment</asp:Label><%--<span class="mandatory">*</span>--%>
						            </HeaderTemplate>
					                <ItemTemplate>		
					                <asp:DropDownList runat="server" ID="cmbMAX_NO_INSTALLMENT" >
					                <asp:ListItem Text="" Value="0"></asp:ListItem>
					                </asp:DropDownList>	
					                <br />
					                <asp:requiredfieldvalidator id="rfvMAX_NO_INSTALLMENT" runat="server" ControlToValidate="cmbMAX_NO_INSTALLMENT" Display="Dynamic"></asp:requiredfieldvalidator>					     					  
    					            </ItemTemplate>
						            </asp:TemplateField>

                                    <asp:TemplateField >
						            <ItemStyle Width="15%" />	
						            <HeaderTemplate >
						            <asp:Label runat="server" ID="capRISK_ID">Risk</asp:Label><span class="mandatory">*</span>
						            </HeaderTemplate>
					                <ItemTemplate>		
					                <asp:DropDownList runat="server" ID="cmbRISK_ID">
					                <asp:ListItem Text="" Value="0"></asp:ListItem>
					                </asp:DropDownList>	
					                <br />
					                <asp:requiredfieldvalidator id="rfvRISK_ID" runat="server" ControlToValidate="cmbRISK_ID" Display="Dynamic"></asp:requiredfieldvalidator>					     					  
    					            </ItemTemplate>
						            </asp:TemplateField>   <%--Added till here--%>
                                    
					            </Columns>
						    </asp:GridView>
						<br />
						</td>	
						</tr>
					
					<tr>
					    <td  class="midcolorc" colspan="2" >
					        <table cellspacing="0" cellpadding="0" width="100%" border="0">
					            <tr>
					                <td class="midcolorc" style="text-align:left">
					                <cmsb:CmsButton runat="server" CausesValidation="false"  ID="btndelete" OnClientClick="return check()" 
                                            CssClass="clsButton" onclick="btndelete_Click"></cmsb:CmsButton>
					                </td>
					                <td class="midcolorc" style="text-align:right">
					                <cmsb:CmsButton ID="btnSave" runat="server" CausesValidation="true" 
                                            CssClass="clsButton" onclick="btnSave_Click" OnClientClick="return checksave()" ></cmsb:CmsButton>
					                </td>
					            </tr>
					        </table>
					    </td>
					</tr>
					<tr>
					    <td>
					        <input type="hidden" id="hidAction" name="hidAction" runat="server"/>
					    </td>
					</tr>
	            </table>
	     
        
        
        
        <div>    
            <asp:Repeater ID="rptrein" runat="server" 
                onitemdatabound="rptrein_ItemDataBound">
                    <HeaderTemplate>
                        <table align="center" border="1" cellspacing="0" class='tableWidth'> 
                        <tr> 
                        <td colspan="12" align="center" class="headereffectCenter">
                        <asp:Label ID="lblRI_BREAKDOWN" runat="server"></asp:Label>
                        <%--<b>RI Treaties For Policy</b>--%>
                        </td>
                        </tr>                                
                            <tr>
                                <b>
                                    <td class="midcolora">
                                    <asp:Label ID="lblPOLICY_NUMBER" runat="server"></asp:Label>
                                      <%-- <B>REIN COMAPANY NAME</B>--%>
                                    </td>
                                    <%--<td class="midcolora">
                                       <B>Reinsurer Name</B>
                                    </td>--%>
                                    <td class="midcolora">
                                    <asp:Label ID="lblREINSURER_NAME" runat="server"></asp:Label>
                                       <%--<B>CONTRACT</B>--%>
                                    </td>
                                   
                                    
                                    <td class="midcolora">
                                     <asp:Label ID="lblLAYER" runat="server"></asp:Label>
                                        <%--<B>LAYER</B>--%>
                                    </td>
                                    <td class="midcolora">
                                    <asp:Label ID="lblLAYER_AMOUNT" runat="server"></asp:Label>
                                       <%-- <B>LAYER AMOUNT</B>--%>
                                    </td>
                                    
                                     <td class="midcolora">
                                    <asp:Label ID="lblCONTRACT_NUMBER" runat="server"></asp:Label>
                                    <%--<B>TIV</B>--%>
                                    </td>
                                    <td class="midcolora">
                                    <asp:Label ID="lblRETENTION_PER" runat="server"></asp:Label>
                                    <%--<B>RISK ID</B>--%>
                                    </td>
                                    
                                    <td class="midcolora">
                                    <asp:Label ID="lblTRAN_PREMIUM" runat="server"></asp:Label>
                                       <%-- <B>TRAN PREMIUM</B>--%>
                                    </td>
                                    <%--<td class="midcolora">
                                    <asp:Label ID="lblREIN_PREMIUM" runat="server"></asp:Label>
                                    <B>REIN CEDED</B>
                                    </td>--%>
                                    <td class="midcolora">
                                     <asp:Label ID="lblCOMM_AMOUNT" runat="server"></asp:Label>
                                    <%--<B>RETENTION PER</B>--%>
                                    </td>
                                    
                                    <td class="midcolora">
                                    <asp:Label ID="lblREIN_CEDED" runat="server"></asp:Label>
                                    <%--<B>COMM AMOUNT</B>--%>
                                    </td>
                                    
                                    <td class="midcolora">
                                    <asp:Label ID="lblCOMM_PER" runat="server"></asp:Label>
                                    <%--<B>REIN PREMIUM</B>--%>
                                    </td>
                                    
                                    <td class="midcolora">
                                     <asp:Label ID="lblRATE" runat="server"></asp:Label>
                                    <%--<B>COMM PERCENTAGE</B>--%>
                                    </td>
                                    
                                    
                                    <td class="midcolora">
                                     <asp:Label ID="lblRISK_ID" runat="server"></asp:Label>
                                    <%--<B>COMM PERCENTAGE</B>--%>
                                    </td>
                                    
                                    
                                    
                                    
                                </b>

                            </tr>
                            
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.REIN_COMAPANY_NAME")%>
                            </td>
                            <%--<td class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.REIN_COMAPANY_NAME")%>
                            </td>--%>
                            <td align="right" class='DataGridRow'> 
                                 <%#DataBinder.Eval(Container, "DataItem.CONTRACT")%>
                            </td>
                            
                            <td align="right" class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.LAYER")%>
                                
                            </td>
                            <td align="right" class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.TOTAL_INS_VALUE")%>
                            </td>
                            <td align="right" class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.LAYER_AMOUNT")%>  <%--CHANGED BY ADITYA FOR TFS # 177--%>
                                
                            </td>
                            
                            
                            <td align="right" class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.RISK_ID")%>
                            </td>
                            
                            
                            <td align="right" class='DataGridRow'>
                                 <%#DataBinder.Eval(Container, "DataItem.TRAN_PREMIUM")%>
                            </td>
                            <%--<td align="right" class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.REIN_CEDED")%>
                            </td>--%>
                            <td align="right" class='DataGridRow'>
                            <asp:Label ID="lblDT_RETENTION_PER" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.RETENTION_PER")%>'></asp:Label> <%--changed by aditya on 17-08-2011 for itrack 1415--%>
                                
                            </td>
                            
                            <td align="right" class='DataGridRow'>
                             <asp:Label ID="lblDT_COMM_PER" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.COMM_PERCENTAGE")%>'></asp:Label>  <%--changed by aditya on 17-08-2011 for itrack 1415--%>
                                
                                 
                            <td align="right" class='DataGridRow'>

                            <asp:Label ID="lblDT_REIN_PREMIUM"  runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.REIN_PREMIUM")%>'></asp:Label>  <%--changed by aditya on 17-08-2011 for itrack 1415--%>
                                
                            </td>
                            
                            <td align="right" class='DataGridRow'>
                                 <%#DataBinder.Eval(Container, "DataItem.CONTRACT_COMM_PERCENTAGE")%>
                            </td>
                            <td align="right" class='DataGridRow'>  
                           
                               <asp:Label ID="lblDT_COMM_AMOUNT" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.COMM_AMOUNT")%>'></asp:Label>  <%--changed by aditya on 17-08-2011 for itrack 1415--%>
                               
                            </td>
                         
                            
                        </tr>  
                          
                    </ItemTemplate>   
                    <FooterTemplate>   <%--added by aditya on 17-08-2011 for itrack 1415--%>
                     <tr>
                                    <td class="midcolorc">
                                    <b>
                                    <asp:Label ID="lblTotal" runat="server"></asp:Label></b>  <!--Added by aditya on 18-08-2011 for itrack # 1415-->
                                       <%--<B>Total</B>--%>
                                    </td>
                                    <%--<td class="midcolora">
                                       <B>Reinsurer Name</B>
                                    </td>--%>
                                    <td class="midcolora">&nbsp;
                                   <%-- <asp:Label ID="lblREINSURER_NAME" runat="server"></asp:Label>--%>
                                       <%--<B>CONTRACT</B>--%>
                                    </td>
                                   
                                    
                                    <td class="midcolora">&nbsp;
                                    <%-- <asp:Label ID="lblLAYER" runat="server"></asp:Label>--%>
                                        <%--<B>LAYER</B>--%>
                                    </td>
                                    <td class="midcolora">&nbsp;
                                   <%-- <asp:Label ID="lblLAYER_AMOUNT" runat="server"></asp:Label>--%>
                                       <%-- <B>LAYER AMOUNT</B>--%>
                                    </td>
                                    
                                     <td class="midcolora">&nbsp;
                                   <%-- <asp:Label ID="lblCONTRACT_NUMBER" runat="server"></asp:Label>--%>
                                    <%--<B>TIV</B>--%>
                                    </td>
                                    <td class="midcolora">&nbsp;
                                  <%--  <asp:Label ID="lblRETENTION_PER" runat="server"></asp:Label>--%>
                                    <%--<B>RISK ID</B>--%>
                                    </td>
                                    
                                    <td class="midcolora">&nbsp;
                                   <%-- <asp:Label ID="lblTRAN_PREMIUM" runat="server"></asp:Label>--%>
                                       <%-- <B>TRAN PREMIUM</B>--%>
                                    </td>
                                    <%--<td class="midcolora">
                                    <asp:Label ID="lblREIN_PREMIUM" runat="server"></asp:Label>
                                    <B>REIN CEDED</B>
                                    </td>--%>
                                    <td class="midcolorr">
                                     <asp:Label ID="lblDT_RETENTION_PER" runat="server"></asp:Label>
                                    <%--<B>RETENTION PER</B>--%>
                                    </td>
                                    
                                    <td class="midcolorr">
                                    <asp:Label ID="lblDT_COMM_PER" runat="server"></asp:Label>
                                    <%--<B>COMM AMOUNT</B>--%>
                                    </td>
                                    
                                    <td class="midcolorr">
                                    <asp:Label ID="lblDT_REIN_PREMIUM" runat="server"></asp:Label>
                                    <%--<B>REIN PREMIUM</B>--%>
                                    </td>
                                    
                                    <td class="midcolora">&nbsp;
                                    <%-- <asp:Label ID="lblRATE" runat="server"></asp:Label>--%>
                                    <%--<B>COMM PERCENTAGE</B>--%>
                                    </td>
                                    
                                    
                                    <td class="midcolorr">
                                     <asp:Label ID="lblDT_COMM_AMOUNT"  runat="server"></asp:Label>
                                    <%--<B>COMM PERCENTAGE</B>--%>
                                    </td>
                                    
                                    </tr>
                                     
                    </FooterTemplate>
                   
                                                 
                  </asp:Repeater>                  

                  

             </div>
             <div>   <%--added by aditya on 17-08-2011 for itrack 2705--%>
              <table id="Table3" width="100%" align="center" border="0">

                                        <tr>
								    <td style="text-align:left" >
								          <asp:GridView ID="grdActPolicyReinInstallmentDetails" runat="server" 
                                                                AutoGenerateColumns="False" 
                                                                Width="100%" DataKeyNames="IDEN_ROW_ID">
                                                               <HeaderStyle CssClass="headereffectWebGrid" />
                                                                <RowStyle CssClass="midcolora" />
                                                                <EmptyDataRowStyle CssClass="midcolora" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="REIN_COMAPANY_NAME" HeaderText="REIN_COMAPANY_NAME" />
                                                                    <asp:BoundField DataField="CONTRACT_NUMBER" HeaderText="CONTRACT_NUMBER" />
                                                                    <asp:BoundField DataField="INSTALLMENT_NO" HeaderText="INSTALLMENT_NO" />
                                                                    <asp:BoundField DataField="INSTALLMENT_AMOUNT" HeaderText="INSTALLMENT_AMOUNT" />
                                                                     <asp:BoundField DataField="INSTALLMENT_EFFECTIVE_DATE" HeaderText="INSTALLMENT_EFFECTIVE_DATE" />
                                                                      <asp:BoundField DataField="RELEASED_STATUS" HeaderText="RELEASED_STATUS" />
                                                                        <asp:TemplateField >
						                                                <ItemStyle/>	
						                                                <HeaderTemplate >
						                                                <asp:Label runat="server" ID="capREIN_INSTALLMENT_NO">Rein Installment #</asp:Label><%--<span class="mandatory">*</span>--%>
						                                                </HeaderTemplate>
					                                                    <ItemTemplate >
					                                                    <asp:TextBox runat="server" ID="txtREIN_INSTALLMENT_NO" Text='<%#Eval("REIN_INSTALLMENT_NO") %>' CausesValidation="true" MaxLength="5"></asp:TextBox><br />
                                                                        <%--<asp:requiredfieldvalidator id="rfvREIN_INSTALLMENT_NO" runat="server" ControlToValidate="txtREIN_INSTALLMENT_NO" Display="Dynamic"></asp:requiredfieldvalidator>--%>
					                                                    </ItemTemplate>
						                                                </asp:TemplateField>                                                                 
                                                                </Columns>
                                                            </asp:GridView>
									                        </td>
								                        </tr>
                                                       <tr>
					                                    <td class="midcolorc" style="text-align:right" colspan="4">				        				               
					                                     <cmsb:cmsbutton class="clsButton" id="btnsave_act" Visible="false" Text="saveact" CausesValidation="true" runat="server"></cmsb:cmsbutton>					               
					                                    </td>
					                                </tr>
                                                    </table>
                                                   </div> <%-- Added till here--%>
                                                   <%--<div id="bodyHeight"   class="pageContent">
                     
                           <table  width="100%" border="0" cellpadding="0" cellspacing="0">                         
                                  <tr id="formTable" runat="server">
                                         <td>
                                                <table class="tableWidthHeader" height="100%"  width="100%" cellSpacing="0" cellPadding="0" border="0" align="center">
                                                       <tr>
                                                              <td width="100%">
                                                                     <webcontrol:Tab ID="TabCtl" runat="server" TabURLs=""
                                                                           TabTitles="Reinsurance Surplus Details" TabLength="310"></webcontrol:Tab>
                                                              </td>
                                                       </tr>
                                                       <tr>
                                                              <td>
                                                                     <table class="tableeffect" width="100%" height="100%" cellpadding="0" cellspacing="0" border="0">
                                                                           <tr>
                                                                                  <td>
                                                                                         <iframe class="iframsHeightLong" id="tabLayer"  runat="server" src="" scrolling="no" frameborder="0"
                                                                                                width="100%"></iframe>
                                                                                  </td>
                                                                           </tr>
                                                                     </table>
                                                              </td>
                                                       </tr>
                                                       
                                                       <tr>
                                                              <td><asp:Label ID="lblTemplate" Runat="server" Visible="false"></asp:Label>
                                                                     <webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer>                                         
                                                              </td>
                                                       </tr>                                                  
                                                </table>
                                         </td>
                                  </tr>
                                 <td><input type = "hidden" id = "hidPolicyStatus" name = "hidPolicyStatus" runat = "server"/></td>
                                 <td><input type = "hidden" id = "hidmessage1" name = "hidmessage1" runat = "server"/></td>
                           </table>
                     
              </div>--%>
                                                      </td>
	      </tr>
        </table>
       
    </form>
    
</body>
</html>
