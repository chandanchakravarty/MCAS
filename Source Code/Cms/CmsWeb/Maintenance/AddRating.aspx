<%@ Page language="c#" Codebehind="AddRating.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AddRating" validateRequest="false"  %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>MNT_AGENCY_RATING_LIST</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="javascript">

		    var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
		    var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
		    var jsaAppDtFormat = "<%=aAppDtFormat  %>";

		    function AddData() {
		        ChangeColor();
		       // DisableValidators();

		    }

//		    function populateXML() {
//		        if (document.getElementById('hidFormSaved').value == '0') {
//		            var tempXML;
//		            if (document.getElementById("hidOldData").value != "") {
//		                tempXML = document.getElementById("hidOldData").value;
//		                populateFormData(tempXML, MNT_RATING_LIST);
//		            }
//		            else {
//		                //SetTimeOut has been added as the page gives javascript error at control focus
//		                setTimeout("AddData();", 500);
//		            }

//		        }
//		        return false;
//		    }

		    function ResetTheForm() {
		        //DisableValidators();
		        document.getElementById("cmbAGENCY_ID").disabled = false;
		        document.getElementById('txtRATING_YEAR').disabled = false;
		        document.getElementById('txtRATING_YEAR').value = "";
		        document.getElementById('txtAGENCY_RATING').value = "";
		        document.getElementById("cmbAGENCY_ID").options.selectedIndex = -1;
		        if (document.getElementById("lblMessage"))
		            document.getElementById('lblMessage').innerHTML = "";
		       // document.forms[0].reset();
		        Init();
		        return false;
		    }
		    function Init() {
		       // populateXML();
		        ApplyColor();
		        //document.getElementById("cmbAGENCY_ID").focus();
		    }
		    function EditViewRating() {
//		        debugger;
		        var URL = "<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>";

//		        document.getElementById('hidRATING_ID').value = '';

//		        document.getElementById('txtRATING_YEAR').value = "";
//		        document.getElementById('txtAGENCY_RATING').value = "";
//		        document.getElementById("cmbAGENCY_ID").selectedindex = 0;
		        OpenLookupWithFunction(URL, 'RATING_ID', 'RATING_ID', 'hidRATING_ID', 'hidRATING_ID', 'EditViewRating', 'Credit Rating', '@COMPANY_ID=' + document.getElementById('hidREIN_COMAPANY_ID').value, 'FetchDivisionAddress()');
		        return false;
		    }

		    function FetchDivisionAddress() 
            {
               var RATING_ID = document.getElementById('hidRATING_ID').value;
//               if (RATING_ID != "") 
//                {
//                    AddRating.AjaxFillRating(RATING_ID, fillState);
//                }

            }
            function fillState(result) {

                if (result.error) 
                {
                    var xfaultcode = result.errorDetail.code;
                    var xfaultstring = result.errorDetail.string;
                    var xfaultsoap = result.errorDetail.raw;
                }
                else 
                {
                    var AGENCYList = document.getElementById("cmbAGENCY_ID");
                    ds = result.value;
                    if (ds != null && typeof (ds) == "object" && ds.Tables != null) 
                   {
                       for (var i = 0; i < ds.Tables[0].Rows.length; ++i) 
                      {
                          for (var j = 0; j < AGENCYList.options.length; j++) 
                          {
                              if (AGENCYList.options[j].value == ds.Tables[0].Rows[0]["AGENCY_ID"].toString()) 
                              {
                                  // Item is found. Set its property and exit
                                  AGENCYList[j].selected = true;
                                  break;
                              } 
                          }
                          document.getElementById('txtRATING_YEAR').value =ds.Tables[0].Rows[i]["EFFECTIVE_YEAR"].toString();
                          document.getElementById('txtAGENCY_RATING').value =ds.Tables[0].Rows[i]["RATED"].toString();
                        }
                    }

                    // setStateID();
                    //document.getElementById('cmbPC_STATE').value = document.getElementById('hidSTATE_ID_OLD').value;
                }

                return false;
            }

		</script>
</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="Init();">
		<FORM id="MNT_RATING_LIST" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
								<tr>
									<td class="midcolora" width="18%"><asp:Label ID="capAGENCY_ID" Runat="server"></asp:Label><span class="mandatory">*</span></td>
									<td class="midcolora" colspan="3">
										<asp:DropDownList ID="cmbAGENCY_ID" Runat="server" onfocus="SelectComboIndex('cmbAGENCY_ID')"></asp:DropDownList><br>
										<asp:RequiredFieldValidator ID="rfvAGENCY_ID" Runat="server" Display="Dynamic" ControlToValidate="cmbAGENCY_ID"></asp:RequiredFieldValidator>
									</td>
								</tr>
								<tr>
									<td class="midcolora" width="18%"><asp:Label ID="capAGENCY_RATING" Runat="server"></asp:Label><span id="spnAGENCY_RATING" runat="server" class="mandatory">*</span></td>
									<td class="midcolora" width="32%">
										<asp:TextBox ID="txtAGENCY_RATING" Runat="server" MaxLength="3" size="10"></asp:TextBox><br>
										<asp:RequiredFieldValidator ID="rfvAGENCY_RATING" Runat="server" Display="Dynamic" ControlToValidate="txtAGENCY_RATING"></asp:RequiredFieldValidator>
									</td>
									<td class="midcolora" width="18%"><asp:Label ID="capRATING_YEAR" Runat="server"></asp:Label><span ID="spnRATING_YEAR" Runat="server" class="mandatory">*</span></td>
									<td class="midcolora" width="32%">
										<asp:TextBox ID="txtRATING_YEAR" MaxLength="4" Runat="server"></asp:TextBox><br>
										<asp:RequiredFieldValidator ID="rfvRATING_YEAR" MaxLength="20" size="34" Runat="server" Display="Dynamic" ControlToValidate="txtRATING_YEAR"></asp:RequiredFieldValidator>
									    <asp:RegularExpressionValidator ID="revRATING_YEAR" Runat="server" Display="Dynamic" ControlToValidate="txtRATING_YEAR"></asp:RegularExpressionValidator>										
									    <asp:CompareValidator ID="cpvRATING_YEAR" runat="server" Type="Integer" Display="Dynamic"  Operator="GreaterThan" ControlToValidate="txtRATING_YEAR"></asp:CompareValidator>
                                        <br />
                                        
									</td>
								</tr>
								<tr>
									<td class="midcolora" colSpan="2">
										<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" Visible="False" runat="server" Text="Activate/Deactivate"
											CausesValidation="false"></cmsb:cmsbutton>
									</td>
									<td class="midcolorr" colSpan="2">
                                    <cmsb:cmsbutton class="clsButton" id="btnViewPastRating" runat="server" Text="View Past Rating"></cmsb:cmsbutton>
                                    <cmsb:cmsbutton class="clsButton" id="btnAddNewRating" runat="server" Text="Add New Rating"></cmsb:cmsbutton>
                                    <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidAGENCY_ID" type="hidden" value="0" name="hidAGENCY_ID" runat="server">
			<INPUT id="hidREIN_COMAPANY_ID" type="hidden" value="0" name="hidREIN_COMAPANY_ID" runat="server">
            <input id="hidRATING_ID" type="hidden" value="0" name="hidRATING_ID" runat="server" />
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
		</FORM>
		<%--<script>
		    RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidSTATE_ID').value, true);			
		</script>--%>
	</BODY>
	
</HTML>


