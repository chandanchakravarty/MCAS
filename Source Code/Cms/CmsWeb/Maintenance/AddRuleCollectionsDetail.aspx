<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRuleCollectionsDetail.aspx.cs" Inherits="Cms.CmsWeb.Maintenance.AddRuleCollectionsDetail" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
        <meta content="False" name="vs_showGrid">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script> 
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
		<script language="javascript">

		    function ResetTheForm() {
		        document.MNT_RULE_COLLECTION_DETAILS.reset();
		    }

		</script>
</head>

 <script type="text/javascript" language="javascript">

         function CountryChanged() {

		                GlobalError = true;
		                var CountryID = document.getElementById('cmbCOUNTRY_ID').options[document.getElementById('cmbCOUNTRY_ID').selectedIndex].value;
		                AddRuleCollectionsDetail.AjaxFillState(CountryID, FillState);
		                if (GlobalError) {
		                    return false;
		                }
		                else {
		                    return true;
		                }
		            }
		            function FillState(Result) {
		                //var strXML;
		                if (Result.error) {
		                    var xfaultcode = Result.errorDetail.code;
		                    var xfaultstring = Result.errorDetail.string;
		                    var xfaultsoap = Result.errorDetail.raw;
		                }
		                else {
		                    var statesList = document.getElementById("cmbSTATE_ID");
		                    statesList.options.length = 0;
		                    oOption = document.createElement("option");
		                    oOption.value = "0";
		                    oOption.text = "todos";
		                    statesList.add(oOption);
		                    ds = Result.value;
		                    if (ds != null && typeof (ds) == "object" && ds.Tables != null) {
		                        for (var i = 0; i < ds.Tables[0].Rows.length; ++i) {

		                            statesList.options[statesList.options.length] = new Option(ds.Tables[0].Rows[i]["STATE_NAME"], ds.Tables[0].Rows[i]["STATE_ID"]);
		                        }

		                    }
		                    if (statesList.options.length > 0) {
		                        //statesList.remove(0);
		                        document.getElementById('hidSTATE_ID').value = statesList.options[0].value;
		                    }
		                    document.getElementById("cmbSTATE_ID").value = document.getElementById("cmbSTATE_ID").value;
		                }

		                return false;
		            }
		            

		            function fillSublobfromlob() {
		                GlobalError = true;
		                var LobId = document.getElementById('cmbLOB_ID').options[document.getElementById('cmbLOB_ID').selectedIndex].value;


		                AddRuleCollectionsDetail.AjaxFillSubLob(LobId, fillSubLob);
		                if (GlobalError) {
		                    return false;
		                }
		                else {
		                    return true;
		                }
		            }

		            function fillSubLob(Result) {
		                if (Result.error) {
		                    var xfaultcode = Result.errorDetail.code;
		                    var xfaultstring = Result.errorDetail.string;
		                    var xfaultsoap = Result.errorDetail.raw;
		                }
		                else {
		                    var Subloblist = document.getElementById("cmbSUB_LOB_ID");
		                    Subloblist.options.length = 0;
		                    oOption = document.createElement("option");
		                    oOption.value = "";
		                    oOption.text = "";
		                    Subloblist.add(oOption);
		                    ds = Result.value;
		                    if (ds != null && typeof (ds) == "object" && ds.Tables != null) {
		                        for (var i = 0; i < ds.Tables[0].Rows.length; ++i) {
		                            Subloblist.options[Subloblist.options.length] = new Option(ds.Tables[0].Rows[i]["SUB_LOB_DESC"], ds.Tables[0].Rows[i]["SUB_LOB_ID"]);
		                        }
		                    }
		                    if (Subloblist.options.length > 0) {
		                        //Subloblist.remove(0);
		                        document.getElementById('hidSUB_LOB_ID').value = Subloblist.options[0].value;
		                    }
		                    document.getElementById("cmbSUB_LOB_ID").value = document.getElementById("cmbSUB_LOB_ID").value;
		                }

		                return false;
		            }


		            function populateXML() { 
		                if (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1') {
		                    if (document.getElementById('hidOldData').value != "") {
		                        populateFormData(document.getElementById('hidOldData').value, MNT_CLAUSES);
		                        //document.getElementById("revATTACH_FILE").style.display = 'none';
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
		                        document.getElementById("lblATTACH_FILE_NAME").innerHTML = "<a href = '" + document.getElementById("hidfileLink").value + "' target='blank'>" + fileName1 + "</a>";
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

 <script language="javascript"  type="text/javascript" >
     $(document).ready(function () { 
         $("#cmbSTATE_ID").change(function () { 
             $("#hidSTATE_ID").val($("#cmbSTATE_ID option:selected").val());
         });
     });
		</script>

         <script language="javascript"  type="text/javascript" >

             $(document).ready(function () {
                 $("#cmbSUB_LOB_ID").change(function () {
                     $("#hidSUB_LOB_ID").val($("#cmbSUB_LOB_ID option:selected").val());
                 });
             });
		</script>


<body leftMargin="0" topMargin="0" onload="ApplyColor();populateXML();ChangeColor();" >
    <form id="MNT_RULE_COLLECTION_DETAILS" method="post" runat="server">
    <div>
    <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
                    <tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							
							<tr>
                        <td class="pageHeader" colspan="4">
                            <asp:Label ID="capMessages" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolorc" align="right" colspan="4">
                            <asp:Label ID="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
                        </td>
                    </tr>		
                    
                    <tr id="trATTACH_FILE_NAME">
								<TD class="midcolora" colSpan="4">					
								
								<asp:label id="capATTACH_FILE_NAME" runat="server">File Name</asp:label><span class="mandatory" id="spnFileName" runat="server">*</span></br>
								<input id="fileATTACH_FILE_NAME" type="file" size="70" runat="server"><br>
                                <asp:label id="lblATTACH_FILE_NAME" Runat="server" ForeColor="Blue"></asp:label>
                                    
                                    <br>                                
                                    
                                    </br>									
									<asp:requiredfieldvalidator id="rfvATTACH_FILE_NAME" runat="server" Display="Dynamic" ControlToValidate="fileATTACH_FILE_NAME"></asp:requiredfieldvalidator>
									<%--<asp:regularexpressionvalidator id="revATTACH_FILE" Runat="server" ControlToValidate="fileATTACH_FILE_NAME" Display="Dynamic"></asp:regularexpressionvalidator>
											<asp:regularexpressionvalidator id="revATTACH_FILE_EXT" Runat="server" ControlToValidate="fileATTACH_FILE_NAME" Display="Dynamic"></asp:regularexpressionvalidator>
											<asp:regularexpressionvalidator id="revATTACH_FILE_PDF" Runat="server" ControlToValidate="fileATTACH_FILE_NAME" Display="Dynamic"></asp:regularexpressionvalidator>--%>
									
									
									</TD>
							</tr>							
							<tr>
							<TD class="midcolora" width="18%">
								 <asp:Label ID="capEFFECTIVE_FROM" runat="server">Effective From</asp:Label><span class="mandatory">*</span>   
								</TD>
								<TD class="midcolora" width="32%">
								    <asp:TextBox ID="txtEFFECTIVE_FROM" runat="server" size="12" MaxLength="10" Display="Dynamic"></asp:TextBox>
                                            <asp:HyperLink ID="hlkEFFECTIVE_FROM" runat="server" CssClass="HotSpot" Display="Dynamic">
                                                <asp:Image ID="imgEFFECTIVE_FROM" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                            </asp:HyperLink>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvEFFECTIVE_FROM" runat="server" Display="Dynamic"
                                                ControlToValidate="txtEFFECTIVE_FROM"></asp:RequiredFieldValidator>
                                          <asp:RegularExpressionValidator ID="revEFFECTIVE_FROM" runat="server" Display="Dynamic"
                                                ControlToValidate="txtEFFECTIVE_FROM"></asp:RegularExpressionValidator> 
								 </TD>
								<td class="midcolora" width="18%">
								 <asp:Label ID="capEFFECTIVE_TO" runat="server">Effective To</asp:Label><span
                                                class="mandatory">*</span>
								</td>
								<td class="midcolora" width="32%">
								  <asp:TextBox ID="txtEFFECTIVE_TO" runat="server"  size="12" MaxLength="10"  Display="Dynamic"></asp:TextBox>
                                            <asp:HyperLink ID="hlkEFFECTIVE_TO" runat="server" CssClass="HotSpot" Display="Dynamic">
                                                <asp:Image ID="imgEFFECTIVE_TO" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                            </asp:HyperLink>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvEFFECTIVE_TO" runat="server" Display="Dynamic"
                                                ControlToValidate="txtEFFECTIVE_TO"></asp:RequiredFieldValidator>
                                         <asp:RegularExpressionValidator ID="revEFFECTIVE_TO" runat="server" Display="Dynamic"
                                                ControlToValidate="txtEFFECTIVE_TO"></asp:RegularExpressionValidator>
                                                <asp:comparevalidator id="cpvEFFECTIVE_TO" ControlToValidate="txtEFFECTIVE_TO" Display="Dynamic" Runat="server" ControlToCompare="txtEFFECTIVE_FROM" Type="Date"
										Operator="GreaterThanEqual"></asp:comparevalidator>		
								</td>
							</tr>
							
							<tr >
							<td class="midcolora" width="18%">
							 <asp:Label ID="capCOUNTRY_ID" runat="server" text="Country"></asp:Label><%--<span class="mandatory">*</span>--%>   
								</TD>
								<TD class="midcolora" width="32%">
								 <asp:DropDownList ID="cmbCOUNTRY_ID" runat="server" onfocus="SelectComboIndex('cmbCOUNTRY_ID')" onchange="javascript:CountryChanged();"	>
                                  </asp:DropDownList><br />
                                 
							 </td>
                             <td class="midcolora" width="18%">
							 <asp:Label ID="capSTATE_ID" runat="server" Text="States"></asp:Label><%--<span class="mandatory">*</span>--%>   
								</TD>
								<TD class="midcolora" width="32%">
								  <asp:DropDownList ID="cmbSTATE_ID" runat="server"   
                                  Width="240px"></asp:DropDownList><br />
                                 
							 </td>
							 </tr>
                             <tr >
							<td class="midcolora" width="18%">
							 <asp:Label ID="capLOB_ID" runat="server" text="Products"></asp:Label><span class="mandatory">*</span>   
								</TD>
								<TD class="midcolora" width="32%">
								 <asp:DropDownList ID="cmbLOB_ID" runat="server" onchange="javascript:fillSublobfromlob();">
                                  </asp:DropDownList><br />
                                   <asp:RequiredFieldValidator ID="rfvLOB_ID" runat="server" Display="Dynamic"
                                                ControlToValidate="cmbLOB_ID"></asp:RequiredFieldValidator>
							 </td>
                             <td class="midcolora" width="18%">
							 <asp:Label ID="capSUB_LOB_ID" runat="server" Text="Line of Bussiness"></asp:Label><span class="mandatory">*</span>   
								</TD>
								<TD class="midcolora" width="32%">
								 <asp:DropDownList ID="cmbSUB_LOB_ID" runat="server">
                                  </asp:DropDownList><br />
                                   <asp:RequiredFieldValidator ID="rfvSUB_LOB_ID" runat="server" Display="Dynamic"
                                                ControlToValidate="cmbSUB_LOB_ID"></asp:RequiredFieldValidator>
							 </td>
							 </tr>

                              <tr >
							<td class="midcolora" width="18%">
							 <asp:Label ID="capVALIDATION_ORDER" runat="server" text="Order"></asp:Label><%--<span class="mandatory">*</span>  --%> 
								</TD>
								<TD class="midcolora" width="32%">
								 <asp:DropDownList ID="cmbVALIDATION_ORDER" runat="server">
                                  </asp:DropDownList><br />
							 </td>
                             <td class="midcolora" width="18%">
							 <asp:Label ID="capVALIDATE_NEXT_IF_FAILED" runat="server" Text="Validate Next if failed"></asp:Label><%--<span class="mandatory">*</span>   --%>
								</TD>
								<TD class="midcolora" width="32%">
								 <asp:CheckBox ID="chkVALIDATE_NEXT_IF_FAILED" runat="server" /><br />
							 </td>
							 </tr>
							
							
							
				<tr>
					<td class="midcolora">
					<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>&nbsp;
						
                                        <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server"  causesvalidation="false" align="right" onclick="btnDelete_Click"></cmsb:cmsbutton>
                                     </td>
					<td class="midcolorr" colSpan="3">
					
							
						  <cmsb:cmsbutton class="clsButton" id="btnSave"  runat="server" Text="Save" ></cmsb:cmsbutton>
                            
                            </td>
				</tr>
						
						</TABLE>
					</TD>
				</TR>
				 <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server"/>
				  <input id="hidRuleCollectionID" type="hidden" value="0" name="hidRuleCollectionID" runat="server"/>
				<input id="hidOldData" type="hidden"  name="hidOldData" runat="server"/>
                <INPUT id="hidSTATE" type="hidden" value="" name="hidSTATE" runat="server">
                 <input id="hidGeneral" type="hidden" value="" name="hidGeneral" runat="server"/>
                  <input id="hidLOBXML" type="hidden" value="0" name="hidLOBXML" runat="server"/>
                  <input id="hidSTATE_ID" type="hidden" value="0" name="hidSTATE_ID" runat="server"/>
                  <input id="hidSUB_LOB_ID" type="hidden" value="0" name="hidSUB_LOB_ID" runat="server"/>
                  <input id="hidOldSUB_LOB" type="hidden" value="0" name="hidOldSUB_LOB" runat="server"/>
                  <input id="hidATTACH_FILE_NAME" type="hidden" value="" name="hidATTACH_FILE_NAME" runat="server"/>
			</TABLE>
    </div>
    </form>
    <script type="text/javascript" >        
        if (document.getElementById('hidFormSaved').value == "1") {

            RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidRuleCollectionID').value);
        }
		</script>
</body>
</html>
