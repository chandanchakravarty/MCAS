<%@ Page language="c#" Codebehind="PolicyNameIns.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.PolicyNameIns" ValidateRequest="false" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Add Policy Insured</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<style  type="text/css" >
		.displayNone
		{display:none
			}
		</style>
		
		<script language="javascript">
		
		var ShowSaveMsgAlways = true;//Done on 26 May 2009 to enable showing of Prompt box(getUserConfirmation()) on tabbing from NameInsured tab to other tabs
			function AddNewCoApplicant()
			{
				//top.botframe.location.href="/cms/client/aspx/ApplicantInsuedIndex.aspx?Customer_ID=" + document.getElementById("hidCustomerID").value + "&Customer_Type='" + document.getElementById("hidCustomerType").value + "'";				
				parent.document.location.href="/cms/client/aspx/CustomerTabIndex.aspx?CalledFrom=Direct&TabNumber=2";
				return false;
			}
			function VerifyCheck(radio) {

			    //for master policy user can not change primary co-app
			    //i-track # 930
			    if('<%=GetTransaction_Type() %>'=='<%=Cms.CmsWeb.cmsbase.MASTER_POLICY %>'){
			        var PrimaryAppID = document.getElementById('hidPRIMARYAPP_ID').value
			        var ID = radio.id.split("_")

			        var strRadioName = ID[0] + "_" + ID[1] + "_" + "APP_ID";
			        var SelectedID = document.getElementById(strRadioName).innerText;
			        if (PrimaryAppID != SelectedID) {
			            radio.checked = false;
			        }
			        return
			    }
			    var frm = document.POL_CO_APPLICANT;
				if (radio.checked==true)
				{
					var strRadioId = radio.name;
					var strCommonId=strRadioId.split("$");
					var strCheckName=strCommonId[0]+"_"+strCommonId[1]+"_"+"chkSECONDARY_APPLICANT";
					document.all[strCheckName].checked=true;				
				}
				
				for(i=0;i< frm.length;i++)
				 {
					e=frm.elements[i];
					if ( e.type=='radio' && e.name.indexOf("rdbPRIMARY_APPLICANT") != -1 )
					{	
						if (e.id!=radio.id)
						{			
							e.checked=false;				
						}
					}
				}
				}	
				
			function BackToCustomer()
			{
			//alert('jdf');
			top.botframe.location.href="/cms/client/aspx/CustomerManagerIndex.aspx";
			return false;
			}
			function ShowApplicantDetails(applcantID) {
			   
			    Url = "/Cms/client/Aspx/AddApplicantInsued.aspx?customer_id=" + document.getElementById("hidCustomerID").value + "&APPLICANT_ID=" + applcantID + "&CALLED_FROM=POLICIES";
			    //DrawTab(0, parent, "Co-Applicant Details", Url);
			    document.getElementById("tabLayer").src = Url;
			}

			function FormatAmountForSum(num) {
			    num = ReplaceAll(num, sGroupSep, '');
			    num = ReplaceAll(num, sDecimalSep, '.');
                return num;
			}
			
			function ValidateCommissionPercentage(objSource, objArgs) {
			    var comm = parseFloat(FormatAmountForSum(document.getElementById(objSource.controltovalidate).value));
			    if (comm < 0 || comm > 100) {
			        document.getElementById(objSource.controltovalidate).select();
			        objArgs.IsValid = false;
			    }
			    else
			        objArgs.IsValid = true;
			}

			function EnableRfv() {
			    if (document.getElementById('hidPRODUCT_TYPE').value == '<%= MASTER_POLICY %>') {
			        if (typeof (Page_Validators) == "undefined")
			            return;
			        var i, val;
			        for (i = 0; i < Page_Validators.length; i++) {
			            val = Page_Validators[i];
			            var id = val.id.split('_');
			            if (id != null && id != 'undefined') {
			                var checkboxid = id[0] + '_' + id[1] + '_' + 'chkSECONDARY_APPLICANT';
			                if (document.getElementById(checkboxid) != null && document.getElementById(checkboxid).checked == true) {
			                    if (document.getElementById('hidLobID').value != '17' && document.getElementById('hidLobID').value != '18')
			                        val.setAttribute('enabled', true);
			                }
			                else
			                    val.setAttribute('enabled', false);
			            }
			        }
			        return Page_ClientValidate();
			    } else {
			    return true;
			     }

			 }
			 function Validate() {
			     if (!CheckPrimaryApplicant())
			         return false;
			     else {
			         return EnableRfv();
			     }
			  }
			function CheckPrimaryApplicant() {
			    var msg = '<%=PrimaryApplicantmsg %>'
			    msg = msg.split(',');
			    var a = 0;
			    var frm = document.POL_CO_APPLICANT;
			    for (i = 0; i < frm.length; i++) {
			        var e = frm.elements[i];
			        if (e.id.indexOf('rdbPRIMARY_APPLICANT') != -1 && e.checked == true) {
			            a = parseInt(a) + 1;
			        }
			    }
			    if (parseInt(a) < 1) {
			        alert(msg[0]);
			        return false
			    }
			    else if (parseInt(a) > 1) {
			        alert(msg[1]);
			        return false;
			    }
			    return true;
			}
			function onselectEnableRfvs(obj) {
			    if (document.getElementById('hidPRODUCT_TYPE').value == '<%= MASTER_POLICY %>') {
			        var Len = obj.id.length;
			        var ReplaceText = '_chkSECONDARY_APPLICANT';
			        var id = obj.id.substring(0, (Len - ReplaceText.length))
			        if (document.getElementById(id + '_rfvCOMMISSION_PERCENT') != null && document.getElementById(id + '_rfvFEES_PERCENT') != null) {
			            if (obj.checked == true) {
			                document.getElementById(id + '_rfvCOMMISSION_PERCENT').setAttribute('enabled', true);
			                document.getElementById(id + '_rfvFEES_PERCENT').setAttribute('enabled', true)
			                document.getElementById(id + '_rfvPRO_LABORE_PERCENT').setAttribute('enabled', true);

			                document.getElementById(id + '_revCOMMISSION_PERCENT').setAttribute('enabled', true);
			                document.getElementById(id + '_revFEES_PERCENT').setAttribute('enabled', true)
			                document.getElementById(id + '_revPRO_LABORE_PERCENT').setAttribute('enabled', true);
			                
			                document.getElementById(id + '_csvCOMMISSION_PERCENT').setAttribute('enabled', true);
			                document.getElementById(id + '_csvFEES_PERCENT').setAttribute('enabled', true)
			                document.getElementById(id + '_csvPRO_LABORE_PERCENT').setAttribute('enabled', true);
			                
			                
			            } else {
			                document.getElementById(id + '_rfvCOMMISSION_PERCENT').setAttribute('enabled', false);
			                document.getElementById(id + '_rfvFEES_PERCENT').setAttribute('enabled', false);
			                document.getElementById(id + '_rfvPRO_LABORE_PERCENT').setAttribute('enabled', false);
			            }

			            //if master policy in Facultative Liability,Civil Liability Transportation
			            //then prolabore and, enrollment fee percentages should readonly ,i-track-637
			            //changed by Lalit chauhan,dec 29,2010
			            
			            if (document.getElementById('hidLobID').value == '17' || document.getElementById('hidLobID').value == '18') {
			                document.getElementById(id + '_txtPRO_LABORE_PERCENT').setAttribute('readOnly', true);
			                document.getElementById(id + '_txtFEES_PERCENT').setAttribute('readOnly', true);
			                document.getElementById(id + '_rfvFEES_PERCENT').setAttribute('enabled', false);
			                document.getElementById(id + '_rfvPRO_LABORE_PERCENT').setAttribute('enabled', false);
			             }  
			            
			            
			        }
			    }
			}
			function LockFieldFormasterPolicy() {
			var frm = document.POL_CO_APPLICANT;
			    if (document.getElementById('hidLobID').value == '17' || document.getElementById('hidLobID').value == '18') {
			        for (i = 0; i < frm.length; i++) {
			            e = frm.elements[i]
			            if (e.id.indexOf('_txtFEES_PERCENT') != -1 || e.id.indexOf('_txtPRO_LABORE_PERCENT') != -1) {
			                document.getElementById(e.id).setAttribute('readOnly', true);
			                document.getElementById(e.id).value = '';

			            }
			        }
			        DisableValidator();
			        
			     }
			 }

			 function DisableValidator() {
			     if (typeof (Page_Validators)!= 'undefined') {
			         for (i = 0; i < Page_Validators.length; i++) {
			             val = Page_Validators[i];
			             if (val.id.indexOf('_rfvFEES_PERCENT') != -1 || val.id.indexOf('_rfvPRO_LABORE_PERCENT') != -1) {
			                 val.setAttribute('enabled', false);
			             }
			         }
			     }
			 }
			 function DisablePrimaryApplicant() {
			     
			     
			  }		 
		</script>
	</HEAD>
	<body oncontextmenu="return true;" MS_POSITIONING="GridLayout" leftMargin="0" topMargin="0" onload = "javascript:LockFieldFormasterPolicy();EnableRfv();">
		<form id="POL_CO_APPLICANT" method="post" runat="server">
			<TABLE class="tablewidthHeader" width="100%" cellSpacing="0" cellPadding="0" align="center"
				border="0">
				<TR class="midcolora">
					<TD class="headereffectcenter">
					<asp:Label runat="server" ID="lblHeader" >Named Insured Details</asp:Label>
					</TD>
				</TR>
				<br>
				<TR>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				<TR class="midcolora">
					<TD align="center"><asp:datagrid id="dgrApplicant"  runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headereffectWebGrid"
							Width="100%" DataKeyField="APPLICANT_ID" AlternatingItemStyle-CssClass="AlternateDataRow2" ItemStyle-CssClass="DataRow2"
							HeaderStyle-VerticalAlign="Top">
							<Columns>
								<asp:TemplateColumn HeaderText="Add/Remove" ItemStyle-HorizontalAlign="Center">
								<ItemStyle  Width="5%"/>
									<ItemTemplate>
										<asp:CheckBox ID="chkSECONDARY_APPLICANT" Runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Primary" ItemStyle-HorizontalAlign="Center" ><%--Applicant--%>
								<ItemStyle  Width="5%"/>
									<ItemTemplate>
										<input type="radio" runat="server" onclick="VerifyCheck(this);" id="rdbPRIMARY_APPLICANT"
											name="rdbPRIMARY_APPLICANT" VALUE="rdbPRIMARY_APPLICANT" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="POSITION" HeaderText="Position" HeaderStyle-Width="15%"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Name" HeaderStyle-Width="10%">								
								<ItemTemplate>
							       <a href="javascript:ShowApplicantDetails(<%# Eval("APPLICANT_ID") %>)" style="color:Black;" ><%# Eval("APPLICANTNAME") %></a>
<%--								<asp:LinkButton runat="server" ID="lnkAPPLICANTNAME" CommandName="Co-Aplicant" CommandArgument='<%# Eval("APPLICANT_ID") %>' Text='<%# Eval("APPLICANTNAME") %>' ForeColor="Black"  Font-Underline="false" ></asp:LinkButton>
--%>							   </ItemTemplate>
								</asp:TemplateColumn>
								<%--<asp:BoundColumn DataField="APPLICANTNAME" HeaderText="Name" HeaderStyle-Width="18%"></asp:BoundColumn>--%>
								<asp:BoundColumn DataField="ADDRESS" HeaderText="" HeaderStyle-Width="15%"></asp:BoundColumn>
								<asp:BoundColumn DataField="CITY" HeaderText="City" HeaderStyle-Width="8%"></asp:BoundColumn>
								<asp:BoundColumn DataField="STATE" HeaderText="" HeaderStyle-Width="8%"></asp:BoundColumn>
								<asp:BoundColumn DataField="ZIP_CODE" HeaderText=""  HeaderStyle-Width="8%"></asp:BoundColumn>
								<asp:TemplateColumn HeaderStyle-CssClass= "displayNone" ItemStyle-CssClass="displayNone">
								<ItemTemplate>
							       <asp:Label runat="server" ID="APP_ID" Text='<%# Eval("APPLICANT_ID") %>'></asp:Label>
<%--								<asp:LinkButton runat="server" ID="lnkAPPLICANTNAME" CommandName="Co-Aplicant" CommandArgument='<%# Eval("APPLICANT_ID") %>' Text='<%# Eval("APPLICANTNAME") %>' ForeColor="Black"  Font-Underline="false" ></asp:LinkButton>
--%>							   </ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
								<ItemStyle Width="10%" />
								    <HeaderTemplate>
								        <asp:Label runat="server" ID="capCOMMISSION_PERCENT"  Text="Commission%"></asp:Label> 
								    </HeaderTemplate>
								    <ItemTemplate>
								        <asp:TextBox Width="100%"  runat="server" ID="txtCOMMISSION_PERCENT" CssClass="INPUTCURRENCY"  Text='<%# Eval("COMMISSION_PERCENT") %>'></asp:TextBox>
								        <asp:RequiredFieldValidator runat="server" Display="Dynamic"  ErrorMessage="" ID="rfvCOMMISSION_PERCENT" ControlToValidate="txtCOMMISSION_PERCENT" ></asp:RequiredFieldValidator>
								        <asp:RegularExpressionValidator runat="server" Display="Dynamic"  ErrorMessage="" ID="revCOMMISSION_PERCENT" ControlToValidate="txtCOMMISSION_PERCENT" ></asp:RegularExpressionValidator>
								    <asp:CustomValidator runat="server" id="csvCOMMISSION_PERCENT" ControlToValidate="txtCOMMISSION_PERCENT" Display="Dynamic" ClientValidationFunction="ValidateCommissionPercentage" ErrorMessage=""></asp:CustomValidator>
								    </ItemTemplate>
								</asp:TemplateColumn>								
								<asp:TemplateColumn>
								<ItemStyle Width="10%" />
								    <HeaderTemplate>    
								        <asp:Label runat="server" ID="capFEES_PERCENT"  Text="Fees%"></asp:Label> 
								    </HeaderTemplate>
								    <ItemTemplate>
								        <asp:TextBox  Width="100%"  runat="server" ID="txtFEES_PERCENT" CssClass="INPUTCURRENCY"  Text='<%# Eval("FEES_PERCENT") %>' ></asp:TextBox>
								        <asp:RequiredFieldValidator runat="server" Display="Dynamic" ErrorMessage="" ID="rfvFEES_PERCENT" ControlToValidate="txtFEES_PERCENT" ></asp:RequiredFieldValidator>
								        <asp:RegularExpressionValidator runat="server" Display="Dynamic"  ErrorMessage="" ID="revFEES_PERCENT" ControlToValidate="txtFEES_PERCENT" ></asp:RegularExpressionValidator>
                                        <asp:CustomValidator runat="server" id="csvFEES_PERCENT" ControlToValidate="txtFEES_PERCENT" Display="Dynamic" ClientValidationFunction="ValidateCommissionPercentage" ErrorMessage=""></asp:CustomValidator>

								    </ItemTemplate>
								</asp:TemplateColumn>								
								<asp:TemplateColumn>
								<ItemStyle Width="10%" />
								    <HeaderTemplate>    
								        <asp:Label runat="server" ID="capPRO_LABORE_PERCENT"  Text="Pro-Labore%"></asp:Label> 
								    </HeaderTemplate>
								    <ItemTemplate>
								        <asp:TextBox Width="100%"   runat="server" ID="txtPRO_LABORE_PERCENT" CssClass="INPUTCURRENCY"   Text = '<%# Eval("PRO_LABORE_PERCENT") %>'></asp:TextBox><br />
								        <asp:RequiredFieldValidator runat="server" Display="Dynamic" ErrorMessage="" ID="rfvPRO_LABORE_PERCENT" ControlToValidate="txtPRO_LABORE_PERCENT" ></asp:RequiredFieldValidator>
								        <asp:RegularExpressionValidator runat="server" Display="Dynamic"  ErrorMessage="" ID="revPRO_LABORE_PERCENT" ControlToValidate="txtPRO_LABORE_PERCENT" ></asp:RegularExpressionValidator>
                                        <asp:CustomValidator runat="server" id="csvPRO_LABORE_PERCENT" ControlToValidate="txtPRO_LABORE_PERCENT" Display="Dynamic" ClientValidationFunction="ValidateCommissionPercentage" ErrorMessage=""></asp:CustomValidator>
								    </ItemTemplate>
								</asp:TemplateColumn>								
							</Columns>
						</asp:datagrid></TD>
				</TR>
				
				<tr>
					<td class="midcolora">
						<table width="100%">
							<tr>
								<td colspan="2">
								<cmsb:cmsbutton class="clsButton" id="btnShowAllApplicants" runat="server" Text=""></cmsb:cmsbutton><br />
								<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
									
								</td>
								<td align="right" colspan="1">
									<cmsb:cmsbutton class="clsButton" id="btnCustomerCoApplicant" runat="server" Text="Add New Co-Applicant"
										></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnCustomerAssistant" runat="server" Visible="False" 
										Width="195px"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				 <tr id="tabCtlRow">
									<td><webcontrol:tab id="TabCtl" runat="server" TabLength="150" TabTitles="Co-Applicant Details" TabURLs=""></webcontrol:tab>
									</td>
								</tr>
								<tr>
									<td>
										<table class="tableeffect" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr>
												<td><iframe class="iframsHeightLong" id="tabLayer" src="" frameBorder="0" width="100%" scrolling="no"
														runat="server"></iframe>
												</td>
											</tr>
										</table>
									</td>
								</tr>
				</tr>
				<TR>
					<TD><INPUT id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server">
						<INPUT id="hidAppVersionID" type="hidden" value="0" name="hidAppVersionID" runat="server">
						<INPUT id="hidAppID" type="hidden" value="0" name="hidAppID" runat="server"> <INPUT id="hidLobID" type="hidden" value="0" name="hidLobID" runat="server">
						<INPUT id="hidCalledFrom" type="hidden" value="0" name="hidCalledFrom" runat="server">
						<INPUT id="hidSavedStatus" type="hidden" value="0" name="hidSavedStatus" runat="server">
						<INPUT id="hidMode" type="hidden" value="0" name="hidMode" runat="server">
						<INPUT id="hidPolicyID" type="hidden" value="0" name="hidPolicyID" runat="server">
						<INPUT id="hidPolicyVersionID" type="hidden" value="0" name="hidPolicyVersionID" runat="server">
						<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server"><%--Done on 26 May 2009 to enable showing of Prompt box(getUserConfirmation()) on tabbing from NameInsured tab to other tabs--%>
						<INPUT id="hidPRIMARYAPP_ID" type="hidden" value="0" name="hidPRIMARYAPP_ID" runat="server">
					<INPUT id="hidPRODUCT_TYPE" type="hidden" value="0" name="hidPRODUCT_TYPE" runat="server">
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
