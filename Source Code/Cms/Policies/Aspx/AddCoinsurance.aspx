<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCoinsurance.aspx.cs" Inherits="Cms.Policies.Aspx.AddCoinsurance" ValidateRequest="false"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>POL_CO_INSURANCE</title>
   <%-- <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
    <link rel="stylesheet" type="text/css" href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" />

    <script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>

    <script type="text/javascript" language="javascript">

        var leadercheckmsg = '<%=leadercheckmsg %>'
        var msg;
        var sum;
        function init() {
            ChangeColor();
            ApplyColor();
            DisableValidator();
          

        }

       

        function SelectRow() { 
            var obj = window.event.srcElement;
            if (obj.tagName == "INPUT")    //this is a checkbox
            {
                checkRowOfObject(obj);
            }
            else if (obj.tagName == "TD") //this a table cell
            {
                //get a pointer to the tablerow
                var row = obj.parentNode;
                var chk = row.cells[0].firstChild;
                chk.checked = !chk.checked;
                if (chk.checked) {
                    row.className = "SelectedRow";
                }
                else {
                    row.className = "";
                }
            }
        }

        function checkRowOfObject(obj) {
            if (obj.checked) {
                obj.parentNode.parentNode.className = "SelectedRow";
            }
            else {
                obj.parentNode.parentNode.className = "";
            }
        }

        function SelectAllRows() {
            var chkAll = window.event.srcElement;
            var tbl = chkAll.parentNode.parentNode.parentNode.parentNode;

            if (chkAll) {
                for (var i = 1; i < tbl.rows.length - 1; i++) {
                    var chk = tbl.rows[i].cells[0].firstChild;
                    chk.checked = chkAll.checked;
                    checkRowOfObject(chk);
                }
            }
        }

        function check() {
            var confirmmessage = '<%= confirmmessage %>';
            var alertMsg = '<%= selectChk %>';
            var deleteleaderalert = '<%=deleteleaderalert %>'
            var frm = document.POL_CO_INSURANCE;
            var boolAllChecked;
            boolAllChecked = false;
            for (i = 0; i < frm.length; i++) {

                e = frm.elements[i];
                if (e.type == 'checkbox' && e.name.indexOf('chkSelect') != -1)
                    if (e.checked == true) {

                        var splitid = e.id.split('_');
                     
                        var hdnCOMAPANY_CODE = splitid[0] + '_' + splitid[1] + '_' + 'hdnCOMAPANY_CODE';
                   
                        if (document.getElementById(hdnCOMAPANY_CODE).value == '<%=GetSystemId() %>')
                        {
                            alert(deleteleaderalert);
                         return false;
                        }
                        else
                        {
                         boolAllChecked = true;
                            break;
                        }
                       
                   
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

        function Validate(objSource, objArgs) { 


            var comm = (document.getElementById(objSource.controltovalidate).value);
            comm = comm.replace(/[,]/g, ".");
            //alert(objSource.getAttribute("controltovalidate").);
            if (comm < 0 || comm > 100) {

                //document.getElementById('txtCOINSURANCE_PERCENT').select();
                objArgs.IsValid = false;
            }
            else
                objArgs.IsValid = true;
        }

        function ValidateCoinsuranceFee(objSource, objArgs) 
        {
            var comm = (document.getElementById(objSource.controltovalidate).value);

            comm = comm.replace(/[,]/g, ".");
           
            //alert(objSource.getAttribute("controltovalidate").);
            if (comm < 0 || comm > 100) {

                //document.getElementById('txtCOINSURANCE_PERCENT').select();
                objArgs.IsValid = false;
            }
            else
                objArgs.IsValid = true;
        }

//        function CalculateTotal() {
//            

//          
//          var inputs = document.getElementsByTagName('tr');
//         
//          sum = 0;
//          //var temp = parseInt(document.getElementById('hidvalue').value) + 1;
//          for (var i = 1; i <= inputs.length-10; i++) {
//                  //if (chkSelect.checked) {
//                 
//                      //if ('rep_ctl0' + i + '_txtCOINSURANCE_PERCENT') {
//                      var txt = FormatAmountForSum(document.getElementById('rep_ctl0' + i + '_txtCOINSURANCE_PERCENT').value);

//                      sum = parseInt(sum) + parseInt(txt);

//                      if (sum > 100) {


//                          msg = document.getElementById("hidmsg1").value;
//                          alert(msg);
//                          return msg;



//                      }
//           
//           
//                     

//          }
//          
//        
//         
//        }

//        function FormatAmountForSum(num) {
//            num = ReplaceAll(num, sGroupSep, '');
//            num = ReplaceAll(num, sDecimalSep, '.');
//            return num;
//        }
       

        function DisableValidator() {
            //            document.getElementById("rfvCarrier").setAttribute('enabled', false);
            //            document.getElementById("rfvCarrier").setAttribute('isValid', true);
            //            document.getElementById("rfvCarrier").style.display = "none";
          

        }

        function EnableValidator() {
            document.getElementById("rfvCarrier").setAttribute('enabled', true);
            document.getElementById("rfvCarrier").setAttribute('isValid', false);
            document.getElementById("rfvCarrier").style.display = "inline";
        }

        function checksave() {
            
            //CalculateTotal();
           // if (sum<=100) {
                var alertMsg = '<%= selectChk %>';
                var frm = document.POL_CO_INSURANCE;
                var boolAllChecked;
                boolAllChecked = false;
                validatordisable();
                for (i = 0; i < frm.length; i++) {
                    e = frm.elements[i];
                    if (e.type == 'checkbox' && e.name.indexOf('chkSelect') != -1)
                        if (e.checked == true) {


                        boolAllChecked = true;

                        var splitid = e.id.split('_');
                        var rfvCOINSURANCE_PERCENT = splitid[0] + '_' + splitid[1] + '_' + 'rfvCOINSURANCE_PERCENT';
                        var txtCOINSURANCE_PERCENT = splitid[0] + '_' + splitid[1] + '_' + 'txtCOINSURANCE_PERCENT';
                        var txtCOINSURANCE_FEE = splitid[0] + '_' + splitid[1] + '_' + 'txtCOINSURANCE_FEE';
                        var rfvTRANSACTION_ID = splitid[0] + '_' + splitid[1] + '_' + 'rfvTRANSACTION_ID';
                        var txtTRANSACTION_ID = splitid[0] + '_' + splitid[1] + '_' + 'txtTRANSACTION_ID';
                        var rfvENDORSEMENT_POLICY_NUMBER = splitid[0] + '_' + splitid[1] + '_' + 'rfvENDORSEMENT_POLICY_NUMBER';
                        var txtENDORSEMENT_POLICY_NUMBER = splitid[0] + '_' + splitid[1] + '_' + 'txtENDORSEMENT_POLICY_NUMBER';
                        // by praveer panghal for itrack no 1439
                        var revENDORSEMENT_POLICY_NUMBER = splitid[0] + '_' + splitid[1] + '_' + 'revENDORSEMENT_POLICY_NUMBER';                        
                        var rfvLEADER_POLICY_NUMBER = splitid[0] + '_' + splitid[1] + '_' + 'rfvLEADER_POLICY_NUMBER';
                        var txtLEADER_POLICY_NUMBER = splitid[0] + '_' + splitid[1] + '_' + 'txtLEADER_POLICY_NUMBER';
                        // by praveer panghal for itrack no 1439
                        var revLEADER_POLICY_NUMBER = splitid[0] + '_' + splitid[1] + '_' + 'revLEADER_POLICY_NUMBER';                        
                        var revCOINSURANCE_PERCENT = splitid[0] + '_' + splitid[1] + '_' + 'revCOINSURANCE_PERCENT';
                        var csvCOINSURANCE_PERCENT = splitid[0] + '_' + splitid[1] + '_' + 'csvCOINSURANCE_PERCENT';
                        var rfvCOINSURANCE_FEE = splitid[0] + '_' + splitid[1] + '_' + 'rfvCOINSURANCE_FEE';
                        var revCOINSURANCE_FEE = splitid[0] + '_' + splitid[1] + '_' + 'revCOINSURANCE_FEE';
                        var csvCOINSURANCE_FEE = splitid[0] + '_' + splitid[1] + '_' + 'csvCOINSURANCE_FEE';
                        var revTRANSACTION_ID = splitid[0] + '_' + splitid[1] + '_' + 'revTRANSACTION_ID';
                        var cmbLEADER_FOLLOWER = splitid[0] + '_' + splitid[1] + '_' + 'cmbLEADER_FOLLOWER';
                        var totleader = document.getElementById("hidCO_INSLEADER").value
                        var rfvLEADER_FOLLOWER = splitid[0] + '_' + splitid[1] + '_' + 'rfvLEADER_FOLLOWER';
                        if (document.getElementById(txtCOINSURANCE_PERCENT).value == '') {
                            document.getElementById(rfvCOINSURANCE_PERCENT).setAttribute('enabled', true);
                            document.getElementById(rfvCOINSURANCE_PERCENT).setAttribute('isValid', false);
                            document.getElementById(rfvCOINSURANCE_PERCENT).style.display = "inline";

                        }
                        else {
                            document.getElementById(revCOINSURANCE_PERCENT).setAttribute('enabled', true);
                            document.getElementById(revCOINSURANCE_PERCENT).setAttribute('isValid', false);
                            document.getElementById(revCOINSURANCE_PERCENT).style.display = "inline";
                            document.getElementById(csvCOINSURANCE_PERCENT).setAttribute('enabled', true);
                            document.getElementById(csvCOINSURANCE_PERCENT).setAttribute('isValid', false);
                            document.getElementById(csvCOINSURANCE_PERCENT).style.display = "inline";
                        }

                        if (document.getElementById(txtTRANSACTION_ID).value == '' && document.getElementById("hidCO_INSURANCE").value == "14549") {
                            document.getElementById(rfvTRANSACTION_ID).setAttribute('enabled', true);
                            document.getElementById(rfvTRANSACTION_ID).setAttribute('isValid', false);
                            document.getElementById(rfvTRANSACTION_ID).style.display = "inline";

                        }
                        //                   else {
                        document.getElementById(revTRANSACTION_ID).setAttribute('enabled', true);
                        document.getElementById(revTRANSACTION_ID).setAttribute('isValid', false);
                        //                        document.getElementById(revTRANSACTION_ID).style.display = "inline";

                        //                    }

                        if (document.getElementById(txtLEADER_POLICY_NUMBER).value == '' && document.getElementById("hidCO_INSURANCE").value == "14549") {
                            document.getElementById(rfvLEADER_POLICY_NUMBER).setAttribute('enabled', true);
                            document.getElementById(rfvLEADER_POLICY_NUMBER).setAttribute('isValid', false);
                            document.getElementById(rfvLEADER_POLICY_NUMBER).style.display = "inline";
                        } // by praveer panghal for itrack no 1439
                        else {
                            document.getElementById(revLEADER_POLICY_NUMBER).setAttribute('enabled', true);
                            document.getElementById(revLEADER_POLICY_NUMBER).setAttribute('isValid', false);
                            document.getElementById(revLEADER_POLICY_NUMBER).style.display = "inline";
                        }
                        if (document.getElementById(txtLEADER_POLICY_NUMBER).value == '' && document.getElementById("hidCO_INSURANCE").value == "14548") {
                            document.getElementById(rfvLEADER_POLICY_NUMBER).setAttribute('enabled', false);
                            document.getElementById(rfvLEADER_POLICY_NUMBER).style.display = "none";
                        }

                        if (document.getElementById(txtCOINSURANCE_FEE).value == '') {
                            document.getElementById(rfvCOINSURANCE_FEE).setAttribute('enabled', true);
                            document.getElementById(rfvCOINSURANCE_FEE).setAttribute('isValid', false);
                            document.getElementById(rfvCOINSURANCE_FEE).style.display = "inline";

                        }
                        else
                            document.getElementById(revCOINSURANCE_FEE).setAttribute('enabled', true);
                        document.getElementById(revCOINSURANCE_FEE).setAttribute('isValid', false);
                        document.getElementById(revCOINSURANCE_FEE).style.display = "inline";
                        document.getElementById(csvCOINSURANCE_FEE).setAttribute('enabled', true);
                        document.getElementById(csvCOINSURANCE_FEE).setAttribute('isValid', false);
                        document.getElementById(csvCOINSURANCE_FEE).style.display = "inline";
                        //                    if (document.getElementById(cmbLEADER_FOLLOWER).value == '14549') {
                        //                        document.getElementById(rfvLEADER_POLICY_NUMBER).setAttribute('enabled', false);
                        //                        document.getElementById(rfvLEADER_POLICY_NUMBER).style.display = "none";
                        //                    
                        //                    }

                        //                    else {
                        //                        document.getElementById(rfvLEADER_POLICY_NUMBER).setAttribute('enabled', true);
                        //                        document.getElementById(rfvLEADER_POLICY_NUMBER).setAttribute('isValid', false);
                        if (document.getElementById(cmbLEADER_FOLLOWER).value == '14548') {
                            document.getElementById(rfvCOINSURANCE_PERCENT).setAttribute('enabled', false);
                            document.getElementById(rfvCOINSURANCE_PERCENT).style.display = "none";
                            document.getElementById(rfvCOINSURANCE_FEE).setAttribute('enabled', false);
                            document.getElementById(rfvCOINSURANCE_FEE).style.display = "none";
                            document.getElementById(rfvLEADER_POLICY_NUMBER).setAttribute('enabled', false);
                            document.getElementById(rfvLEADER_POLICY_NUMBER).style.display = "none";
                            document.getElementById(rfvENDORSEMENT_POLICY_NUMBER).setAttribute('enabled', false);
                            document.getElementById(rfvENDORSEMENT_POLICY_NUMBER).style.display = "none";
                            document.getElementById(rfvTRANSACTION_ID).setAttribute('enabled', false);
                            document.getElementById(rfvTRANSACTION_ID).style.display = "none";
                            //                     


                        }
                        if (document.getElementById(cmbLEADER_FOLLOWER).value == '') {
                            document.getElementById(rfvLEADER_FOLLOWER).setAttribute('enabled', true);
                            document.getElementById(rfvLEADER_FOLLOWER).style.display = "inline";
                        }
                        if (document.getElementById("hidpolicystatus").value == "UENDRS") {
                            if (document.getElementById(txtENDORSEMENT_POLICY_NUMBER).value == '' && document.getElementById(cmbLEADER_FOLLOWER).value == '14549') {
                                document.getElementById(rfvENDORSEMENT_POLICY_NUMBER).setAttribute('enabled', true);
                                document.getElementById(rfvENDORSEMENT_POLICY_NUMBER).style.display = "inline";
                            }
                            // changes by praveer for itrack no 1439
                            else {
                                document.getElementById(revENDORSEMENT_POLICY_NUMBER).setAttribute('enabled', true);
                                document.getElementById(revENDORSEMENT_POLICY_NUMBER).setAttribute('isValid', false);
                                document.getElementById(revENDORSEMENT_POLICY_NUMBER).style.display = "inline";
                            }
                        }




                        Page_ClientValidate();

                    }
                }
                if (boolAllChecked == false) {
                    alert(alertMsg);
                    return false;
                }
//            }
//            else {
//               // alert(msg);
//                return false;
//            }
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

        function CheckLeader(txtleader, leaderval, cmbleaderid, coinsuranceid, transactionid) {
      
            var totleader = document.getElementById("hidCO_INSLEADER").value
            var coid = document.getElementById("hidCO_INSURANCE").value
            if (leaderval == 14548) {
                document.getElementById(transactionid).value = "";
                document.getElementById(transactionid).setAttribute('disabled', true);             
                if (totleader > 0 && coinsuranceid != coid) {
                    alert(leadercheckmsg);
                    document.getElementById(cmbleaderid).selectedIndex = 0;
                   
                    return false;
                }


            }
            if (coid == 14549 && leaderval == 14549) {
                document.getElementById(transactionid).setAttribute('disabled', false);
              
            }          
        }	    

    </script>
</head>
<body oncontextmenu="return false;" leftmargin="0" onload="init();" rightmargin="0"
    ms_positioning="GridLayout" class="bodyBackGround">
    <form id="POL_CO_INSURANCE" method="post" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" class="tableWidthHeader" border="0">
        <tr>
            <td class="headereffectCenter">
                <asp:Label ID="lblPolicyCaption" runat="server"></asp:Label>
            </td>
        </tr>
        <%--<tr><td class="pageHeader" >
						<br />
						<asp:label id="lblManHeader" Runat="server" colspan="2"></asp:label>
						</td>
					</tr>--%>
        <tr>
            <td align="center">
                <asp:Label ID="lblMessage" runat="server" CssClass="errmsg" EnableViewState="False"></asp:Label>
            </td>
        </tr>
        <asp:Panel ID="pnlShowHide" runat="server" Width="100%"><%--Added by Charles on 7-Jun-2010--%>
        <tr>
            <td class="midcolora">
                <b>
                    <asp:Label ID="capCarrier" runat="server"></asp:Label><span class="mandatory">*</span></b>
            </td>
        </tr>
        <tr>
            <td class="midcolora">
                <asp:DropDownList ID="cmbCarrier" runat="server" AutoPostBack="false" >
                </asp:DropDownList>
                <cmsb:CmsButton class="clsButton" ID="btnSelect" runat="server" OnClick="btnSelect_Click"
                    CausesValidation="false" />
                <br />
                <%-- <asp:requiredfieldvalidator id="rfvCarrier" runat="server" ControlToValidate="cmbCarrier" Display="Dynamic"></asp:requiredfieldvalidator>--%>
            </td>
        </tr>
        <tr>
            <td class="midcolora">
                <%--<asp:GridView id="dgCoInsurance" runat="server" DataKeyField="COINSURANCE_ID" 
                            Width="100%" AutoGenerateColumns="false" 
					AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" OnRowCancelingEdit="dgCoInsurance_RowCancelingEdit" 
                    OnRowDeleting="dgCoInsurance_RowDeleting" OnRowEditing="dgCoInsurance_RowEditing" 
                            OnRowUpdating="dgCoInsurance_RowUpdating" ondatabound="dgCoInsurance_DataBound">--%>
                <%--<asp:GridView id="dgCoInsurance" runat="server" DataKeyField="COINSURANCE_ID" 
                             onrowcreated="dgCoInsurance_RowCreated" AutoGenerateColumns="false" width="100%"
                             AutoGenerateEditButton="True" OnRowEditing="dgCoInsurance_RowEditing" 
                            OnRowUpdating="dgCoInsurance_RowUpdating" onrowdatabound="dgCoInsurance_RowDataBound"
                             OnRowCancelingEdit="dgCoInsurance_RowCancelingEdit"
                           >						 
                        <Columns>  
                         <asp:TemplateField>
                    <HeaderStyle HorizontalAlign="left" />
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkSelectAll" ToolTip="Click here to select/deselect all rows" 
                             runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>
                                                                          
                            <asp:TemplateField HeaderText="Coinsurer Name">									
									<EditItemTemplate>
									    <asp:textbox ID="txtCO_INSURER_NAME" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_INSURER_NAME") %>'>
										</asp:textbox>
									</EditItemTemplate>
									<ItemTemplate>
										<asp:label ID="capCO_INSURER_NAME" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_INSURER_NAME") %>'>
										</asp:label>								
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Leader/Follower">									
									<EditItemTemplate>
									    <asp:DropDownList ID="cmbLEADER_FOLLOWER" Runat="server">
									        <asp:ListItem Value="0">Leader</asp:ListItem>
									        <asp:ListItem Value="1">Follower</asp:ListItem>									        
										</asp:DropDownList>
									</EditItemTemplate>
									<ItemTemplate>
									    <asp:label id="capLEADER_FOLLOWER" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LEADER_FOLLOWER") %>'>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Coinsurance %">		
								    <EditItemTemplate>
								        <asp:textbox ID="txtCOINSURANCE_PERCENT" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COINSURANCE_PERCENT") %>'>
										</asp:textbox>	
								    </EditItemTemplate>							
									<ItemTemplate>
										<asp:label ID="capCOINSURANCE_PERCENT" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COINSURANCE_PERCENT") %>'>
										</asp:label>	
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Coinsurance Fee">	
								    <EditItemTemplate>
								        <asp:textbox ID="txtCOINSURANCE_FEE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COINSURANCE_FEE") %>'>
										</asp:textbox>	
								    </EditItemTemplate>								
									<ItemTemplate>
										<asp:label ID="capCOINSURANCE_FEE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COINSURANCE_FEE") %>'>
										</asp:label>	
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Broker Commission %">	
								    <EditItemTemplate>
								        <asp:textbox ID="txtBROKER_COMMISSION" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BROKER_COMMISSION") %>'>
										</asp:textbox>
								    </EditItemTemplate>								
									<ItemTemplate>
										<asp:label ID="capBROKER_COMMISSION" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BROKER_COMMISSION") %>'>
										</asp:label>	
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Transaction ID">		
								    <EditItemTemplate>
								        <asp:textbox ID="txtTRANSACTION_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TRANSACTION_ID") %>'>
										</asp:textbox>	
								    </EditItemTemplate>							
									<ItemTemplate>
										<asp:label ID="capTRANSACTION_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TRANSACTION_ID") %>'>
										</asp:label>	
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Leader Policy Number">
								    <EditItemTemplate>
								        <asp:textbox ID="txtLEADER_POLICY_NUMBER" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LEADER_POLICY_NUMBER") %>'>
										</asp:textbox>	
								    </EditItemTemplate>									
									<ItemTemplate>
										<asp:label ID="capLEADER_POLICY_NUMBER" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LEADER_POLICY_NUMBER") %>'>
										</asp:label>	
									</ItemTemplate>
								</asp:TemplateField>	
								<asp:TemplateField Visible="false">
									<ItemTemplate>
										<asp:Label ID="capCOINSURANCE_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COINSURANCE_ID") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
                        </Columns>
                     <HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>                     
					</asp:GridView>--%>
                <asp:Repeater ID="rep" runat="server" OnItemDataBound="rep_ItemDataBound">
                    <HeaderTemplate>
                        <table id="trrep" width="100%" cellspacing="0" cellpadding="0" class="tableWidthHeader">
                            <tr>
                                <td class="headereffectWebGrid" width="2%">
                                    <b>
                                        <asp:Label ID="cap_Select" runat="server"></asp:Label></b>
                                </td>
                                <td class="headereffectWebGrid" width="10%">
                                    <b>
                                        <asp:Label ID="cap_Carriername" runat="server"></asp:Label></b>
                                </td>
                                <td class="headereffectWebGrid">
                                    <b>
                                        <asp:Label ID="cap_CO_INSURER_NAME" runat="server" Visible="false">
                                        </asp:Label></b>
                                </td>
                                <td class="headereffectWebGrid" width="10%">
                                    <b>
                                        <asp:Label ID="cap_LEADER_FOLLOWER" runat="server"></asp:Label></b>
                                </td>
                                <td class="headereffectWebGrid" width="10%">
                                    <b>
                                        <asp:Label ID="cap_COINSURANCE_PERCENT" runat="server"></asp:Label></b>
                                </td>
                                <td class="headereffectWebGrid" width="10%">
                                    <b>
                                        <asp:Label ID="cap_COINSURANCE_FEE" runat="server"></asp:Label></b>
                                </td>
                                <%--<td class="headereffectWebGrid" width="12%">
                                    <b>
                                        <asp:Label ID="cap_BROKER_COMMISSION" runat="server"></asp:Label>
                                    </b>
                                </td>--%>
                                <td class="headereffectWebGrid" width="10%">
                                    <b>
                                        <asp:Label ID="cap_TRANSACTION_ID" runat="server"></asp:Label></b>
                                </td>
                                <td class="headereffectWebGrid" width="10%">
                                    <b>
                                        <asp:Label ID="cap_LEADER_POLICY_NUMBER" runat="server"></asp:Label></b>
                                </td>
                                <td class="headereffectWebGrid" width="10%">
                                    <b>
                                        <asp:Label ID="cap_ENDORSEMENT_POLICY_NUMBER" runat="server"></asp:Label></b>
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="midcolorba">
                            <td align="left" width="10%">  <%--Changed by aditya on 08-08-2011 for itrack # 1504--%>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </td>
                            <td align="left" width="15%">  <%--Changed by aditya on 08-08-2011 for itrack # 1504--%>
                                <asp:TextBox ID="txtREIN_COMAPANY_NAME" size="21" Enabled="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.REIN_COMAPANY_NAME") %>'>
                                </asp:TextBox>
                            </td>
                            <td align="left">  <%--Changed by aditya on 08-08-2011 for itrack # 1504--%>
                                <asp:TextBox ID="txtCO_INSURER_NAME" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_INSURER_NAME") %>'>
                                </asp:TextBox>
                                 <asp:HiddenField ID="hidCOMPANY_ID"  runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.REIN_COMAPANY_ID") %>'>
                                </asp:HiddenField>
                            </td>
                            <td align="left" width="15%"> <%--Changed by aditya on 08-08-2011 for itrack # 1504--%>
                                <asp:DropDownList ID="cmbLEADER_FOLLOWER" runat="server" >
                                   <%-- <asp:ListItem Value="0" Text=""></asp:ListItem>
                                    <asp:ListItem Value="14548">Leader</asp:ListItem>
                                    <asp:ListItem Value="14549">Follower</asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:Label ID="lblLEADER_FOLLOWER" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LEADER_FOLLOWER") %>'
                                    Visible="false"></asp:Label>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvLEADER_FOLLOWER" runat="server" ControlToValidate="cmbLEADER_FOLLOWER"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" width="15%">  <%--Changed by aditya on 08-08-2011 for itrack # 1504--%>
                                <asp:TextBox ID="txtCOINSURANCE_PERCENT"  CssClass="INPUTCURRENCY" onkeypress="MaxLength(this,7)"  
                                    onpaste="MaxLength(this,7);" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COINSURANCE_PERCENT") %>'>
                                </asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="rfvCOINSURANCE_PERCENT" runat="server" ControlToValidate="txtCOINSURANCE_PERCENT"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revCOINSURANCE_PERCENT" runat="server" ControlToValidate="txtCOINSURANCE_PERCENT"
                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                <asp:CustomValidator ID="csvCOINSURANCE_PERCENT" Display="Dynamic" ControlToValidate="txtCOINSURANCE_PERCENT"
                                    ClientValidationFunction="Validate" runat="server"></asp:CustomValidator>
                            </td>
                            <td align="left" width="15%">  <%--Changed by aditya on 08-08-2011 for itrack # 1504--%>
                                <asp:TextBox ID="txtCOINSURANCE_FEE" size="22"  class="midcolora" onkeypress="MaxLength(this,8)" onpaste="MaxLength(this,8)" 
                                    runat="server" CssClass="INPUTCURRENCY" Text='<%# DataBinder.Eval(Container, "DataItem.COINSURANCE_FEE") %>'>
                                </asp:TextBox><br />                           
                                <asp:RequiredFieldValidator ID="rfvCOINSURANCE_FEE" runat="server" ControlToValidate="txtCOINSURANCE_FEE"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="csvCOINSURANCE_FEE" Display="Dynamic" ControlToValidate="txtCOINSURANCE_FEE"
                                    ClientValidationFunction="ValidateCoinsuranceFee" runat="server"></asp:CustomValidator>
                                <asp:RegularExpressionValidator ID="revCOINSURANCE_FEE" runat="server" ControlToValidate="txtCOINSURANCE_FEE"
                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                  
                            </td>
                         <%-- <td class="midcolora" width="12%">
                                <asp:TextBox ID="txtBROKER_COMMISSION" CssClass="INPUTCURRENCY" onkeypress="MaxLength(this,7)" ReadOnly="true"
                                    onpaste="MaxLength(this,7)" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BROKER_COMMISSION")%>' EnableViewState="True">
                                </asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="revBROKER_COMMISSION" runat="server" ControlToValidate="txtBROKER_COMMISSION"
                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                <asp:CustomValidator ID="csvBROKER_COMMISSION" Display="Dynamic" ControlToValidate="txtBROKER_COMMISSION"
                                    ClientValidationFunction="Validate" runat="server"></asp:CustomValidator>
                            </td>--%>
                            <td align="left" width="15%">  <%--Changed by aditya on 08-08-2011 for itrack # 1504--%>
                                <asp:TextBox ID="txtTRANSACTION_ID" runat="server"  size="18" MaxLength="13" Text='<%# DataBinder.Eval(Container, "DataItem.TRANSACTION_ID") %>' >
                                </asp:TextBox><br />
                              <asp:RequiredFieldValidator ID="rfvTRANSACTION_ID" runat="server" ControlToValidate="txtTRANSACTION_ID"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revTRANSACTION_ID" runat="server" ControlToValidate="txtTRANSACTION_ID"
                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                            </td>
                            <td align="left" width="25%">  <%--Changed by aditya on 08-08-2011 for itrack # 1504--%>
                                <asp:TextBox ID="txtLEADER_POLICY_NUMBER"  runat="server"  size="22" MaxLength="25" Text='<%# DataBinder.Eval(Container, "DataItem.LEADER_POLICY_NUMBER") %>'>
                                </asp:TextBox><br />
                              <asp:RequiredFieldValidator ID="rfvLEADER_POLICY_NUMBER" runat="server" ControlToValidate="txtLEADER_POLICY_NUMBER"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revLEADER_POLICY_NUMBER" runat="server" ControlToValidate="txtLEADER_POLICY_NUMBER"
                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                   
                            </td>
                             <td align="left" width="25%">  <%--Changed by aditya on 08-08-2011 for itrack # 1504--%>
                                <asp:TextBox ID="txtENDORSEMENT_POLICY_NUMBER" runat="server"  size="22" MaxLength="25" Text='<%# DataBinder.Eval(Container, "DataItem.ENDORSEMENT_POLICY_NUMBER") %>' >
                                </asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="rfvENDORSEMENT_POLICY_NUMBER" runat="server" ControlToValidate="txtENDORSEMENT_POLICY_NUMBER"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                   <%-- changes by praveer for itrack no 1439 --%>                                
                                     <asp:RegularExpressionValidator ID="revENDORSEMENT_POLICY_NUMBER" runat="server" ControlToValidate="txtENDORSEMENT_POLICY_NUMBER"
                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                               
                            </td>
                            <td width="12%" class="midcolora">
                                <asp:TextBox ID="txtCOINSURANCE_ID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COINSURANCE_ID") %>'>
                                </asp:TextBox>
                            </td>
                            <td width="12%" class="midcolora">
                            <asp:HiddenField ID="hidREIN_COMAPANY_ID" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.REIN_COMAPANY_ID") %>' />
                                <asp:TextBox ID="txtREIN_COMAPANY_ID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.REIN_COMAPANY_ID") %>'>
                                </asp:TextBox><asp:Label ID="lblREIN_COMAPANY_CODE" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.REIN_COMAPANY_CODE") %>'></asp:Label>
                                <asp:HiddenField ID="hdnCOMAPANY_CODE" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.REIN_COMAPANY_CODE") %>' />
                               
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
        <tr>
            <td class="midcolora">
                <cmsb:CmsButton class="clsButton" ID="btnDelete" runat="server" Text="Delete" OnClientClick="return check()"
                    OnClick="btnDelete_Click" CausesValidation="false" />
            </td>
            <td align="right" colspan="8">
                <cmsb:CmsButton class="clsButton" ID="btnSave" runat="server" OnClick="btnSave_Click"
                    OnClientClick="return checksave(); " />
            </td>
        </tr>
        </asp:Panel>
        <tr>
            <td>
                <input id="hidOldData" type="hidden" name="hidOldData" runat="server">
                <input id="hidPolID" type="hidden" name="hidPolID" runat="server">
                <input id="hidPolVersionID" type="hidden" name="hidPolVersionID" runat="server">
                <input id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server">
                <input id="hidROW_COUNT" type="hidden" value="0" name="hidROW_COUNT" runat="server">
                <input id="hidPOLICY_ROW_COUNT" type="hidden" value="0" name="hidPOLICY_ROW_COUNT"
                    runat="server">
                <input id="hidLOBState" type="hidden" name="hidLOBState" runat="server">
                <input id="hidCO_INSURANCE" type="hidden" value="0" name="hidCO_INSURANCE" runat="server"><%--Added by Charles on 7-Jun-2010--%>
                 <input id="hidCO_INSLEADER" type="hidden" value="0" name="hidCO_INSLEADER" runat="server">
                 <input id="hidPOL_APPNUMBER" type="hidden" name="hidPOL_APPNUMBER" runat="server">
                 <input id="hidCOINSURANCE_ID" type="hidden" name="hidCOINSURANCE_ID" runat="server">
                 <input id="hidpolicystatus" type="hidden" name="hidpolicystatus" runat="server">
                 <input id="hidmsg1" type="hidden" name="hidmsg1" runat="server">
                 <input id="hidvalue" type="hidden" name="hidvalue" runat="server">
                  
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
