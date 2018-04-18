<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page language="c#" validateRequest=false Codebehind="PolSchItemsCoverages.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.aspx.HomeOwners.PolSchItemsCoverages" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddInlandMarine</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="../../../cmsweb/scripts/xmldom.js"></script>
		<script src="../../../cmsweb/scripts/common.js"></script>
		<script src="../../../cmsweb/scripts/form.js"></script>
		<script language="vbscript">
			FUNCTION AskForDelete
				Dim retVal
				
				retVal = MsgBox ("Unchecking this category will delete all items under this category." + vbCrLf + "Do you wish to continue?",vbYesNo,"Delete")
				
				IF retVal = vbNo THEN
					AskForDelete = false
				END IF				
			END FUNCTION 
		</script>
		<script language="javascript">
		
			var prefix = "dgCoverages__ctl";
			
			function RightAlign(obj)
			{
				if(obj)
					document.getElementById(obj.id).style.textAlign="right";
			}
			
			
			var global_COVG_ID = "";
			var global_COVG_DESC = "";
			
			function ShowCovgGridPage(CovgID)
			{	
				if (document.getElementById('Row_' + CovgID).getElementsByTagName("td")[0].childNodes[0].checked == true)
				{	
					document.getElementById("tabCtlRow").style.display="inline";
					document.cookie  = "CovgID=" + CovgID;// + ";expires=" + 
					
					global_COVG_ID   = CovgID;
					global_COVG_DESC = document.getElementById('Row_' + CovgID).getElementsByTagName("td")[1].innerText;
					
					DrawTab(0,top.frames[1],'','AddInlandMarineMIDDLE.aspx?CalledFrom=' + document.Form1.hidCalledFrom.value + '&');
					changeTab(0,0);				
				}
			}
		
		
		function MakeRowEditable(CovgID, ChkBxCtrl)
		{
			//alert(CovgID+'\n'+ChkBxCtrl)
			var RowNum = ChkBxCtrl.id.split('_')[2].substring(3,ChkBxCtrl.id.split('_')[2].length);
			
			if (ChkBxCtrl.checked == true)//enable dropdown
			{
				document.getElementById('dgCoverages__ctl' + RowNum + '_lblAmount').style.display='inline';
				document.getElementById('dgCoverages__ctl' + RowNum + '_lblDedubtible').style.display='none';
				document.getElementById('dgCoverages__ctl' + RowNum + '_ddlRange').style.display='inline';
				document.getElementById('lbl_' + CovgID).style.cursor="pointer";
			}
			else //disable dropdown
			{
				//alert("Unchecking this category will delete deductible \n and all items under this category after save!")
				/*if (AskForDelete() == false) 
				{
					ChkBxCtrl.checked = true;						
				}	
				else
				{
				*/	document.getElementById('dgCoverages__ctl' + RowNum + '_lblAmount').style.display='inline';
					document.getElementById('dgCoverages__ctl' + RowNum + '_lblDedubtible').style.display='inline';
					document.getElementById('dgCoverages__ctl' + RowNum + '_ddlRange').style.display='none';
					document.getElementById('lbl_' + CovgID).style.cursor="default";
					//CALL JS to delete the covg
					//DeleteSelectedCovg(CovgID);						
				//}
			}
		}
		function AfterDeleteFunction(Result)
		{
		
			//alert(Result.value)			
			if(Result.error)
			{
				var xfaultcode   = Result.errorDetail.code;
				var xfaultstring = Result.errorDetail.string;
				var xfaultsoap   = Result.errorDetail.raw;        				
			}
			else		
				document.location.reload(true);
		}
			
			function SetRowLabelDropDown()
			{
				var tmpXML = document.getElementById('hidFilledCovgXML').value
				
				if(tmpXML==null || tmpXML=="")
					{return};
				
				var objXmlHandler = new XMLHandler();
				var tree = (objXmlHandler.quickParseXML(tmpXML).getElementsByTagName('NewDataSet')[0]);
				
				if(tree)
				{				
					for(i=0;i<tree.childNodes.length;i++)
					{
						//get item id and deductible amt 
						var row_item_id = tree.childNodes[i].childNodes[0].childNodes[0].text; 						
						var row_ded_val = tree.childNodes[i].childNodes[1].childNodes[0].text.split('.')[0];
						var row_Ins_Amt = tree.childNodes[i].childNodes[2].childNodes[0].text; 
						//alert(row_item_id+'\n'+row_ded_val +'\n'+row_Ins_Amt)
						
						//get the row 
						var trCurrent = document.getElementById("Row_" + row_item_id);
						if (trCurrent != null && trCurrent != "")
						{
							trCurrent.getElementsByTagName("td")[0].childNodes[0].checked = true;
							trCurrent.getElementsByTagName("td")[3].childNodes[0].style.display = 'none';							
							var currRowNum	= trCurrent.getElementsByTagName("td")[2].childNodes[0].id.split('_')[2].substring(3,trCurrent.getElementsByTagName("td")[2].childNodes[0].id.split('_')[2].length);							
							document.getElementById('dgCoverages__ctl' + currRowNum + '_ddlRange').style.display="inline";	
							document.getElementById('dgCoverages__ctl' + currRowNum + '_ddlRange').value = row_ded_val;	
							document.getElementById('lbl_' + row_item_id).style.cursor="pointer";
							
							//if (row_Ins_Amt == null || row_Ins_Amt == '' || row_Ins_Amt == "0" || row_Ins_Amt == 0)
							//	document.getElementById('dgCoverages__ctl' + currRowNum + '_lblCoverageTotalAmount').innerHTML = "No Coverage&nbsp;&nbsp;";
						//	else
							//	document.getElementById('dgCoverages__ctl' + currRowNum + '_lblCoverageTotalAmount').innerHTML = formatAmount(row_Ins_Amt).split('.')[0] + "&nbsp;&nbsp;";
													
						}
					}	
				}
			}
		</script>
	</HEAD>
	<body onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();" MS_POSITIONING="GridLayout"> <!--changeTab(0,0);->
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<form id="Form1" method="post" runat="server">				
				<table cellSpacing="0" cellPadding="0" class="tableWidth">
					<tr>
						<td><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow>
						</td>
					</tr>
					<tr>
						<TD id="tdClientTop" class="pageHeader" colSpan="4">
							<webcontrol:ClientTop id="cltClientTop" runat="server" width="98%"></webcontrol:ClientTop>
						</TD>
					</tr>
					<tr>
						<td><webcontrol:gridspacer id="grdspacer" runat="server"></webcontrol:gridspacer></td>
					</tr>
					<tr id="trError" runat="server">
						<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblError" runat="server" CssClass="errmsg"></asp:label></td>
					</tr>
					<TR id="trBody" runat="server">
						<TD>
							<table cellSpacing="0" cellPadding="0" width="100%">
								<tr>
									<td class="headereffectCenter"><asp:label id="lblTitle" runat="server">Scheduled Items / Coverages</asp:label></td>
								</tr>
								<tr>
									<td align="center"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="True" EnableViewState="False"></asp:label></td>
								</tr>
								<tr>
									<td class="midcolora">
										<div style="OVERFLOW: auto;HEIGHT: 125px">
											<asp:datagrid id="dgCoverages" runat="server" DataKeyField="COV_ID" AutoGenerateColumns="False"
												Width="100%">
												<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
												<ItemStyle CssClass="midcolora"></ItemStyle>
												<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
												<Columns>
													<asp:TemplateColumn>
														<ItemTemplate>
															<asp:CheckBox id="cbDelete" runat="server"></asp:CheckBox>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:HyperLinkColumn DataNavigateUrlField="COV_ID" DataNavigateUrlFormatString="Javascript:ShowCovgGridPage('{0}')"
														DataTextField="COV_DESC" HeaderText="Description"></asp:HyperLinkColumn>
														
													<asp:TemplateColumn HeaderText="Total Amount">
														<ItemTemplate>
															<asp:Label id="lblAmount" CssClass="labelfont" Runat="server"></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
																										
													<asp:TemplateColumn HeaderText="Deductible">
														<ItemTemplate>
															<asp:label id="lblDedubtible" CssClass="labelfont" Runat="server">No Coverage</asp:label>
															<asp:DropDownList Runat="server" style="display:none;width:65px" ID="ddlRange">
																<asp:ListItem Value="0">&nbsp;</asp:ListItem>
															</asp:DropDownList>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn Visible="False" HeaderText="COV_ID">
														<ItemTemplate>
															<asp:Label ID="lblCOV_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
															</asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
												</Columns>
											</asp:datagrid>
										</div>
									</td>
								</tr>
								<tr>
									<td>
										<table width="100%">
											<tr>
												<td class="midcolorr">
													<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" CausesValidation="True"></cmsb:cmsbutton>
													<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Save" CausesValidation="True"
														visible="false"></cmsb:cmsbutton>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td><webcontrol:gridspacer id="grdspacer1" runat="server"></webcontrol:gridspacer></td>
								</tr>
								<tr>
									<td>
										<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
											<tr id="tabCtlRow" style="DISPLAY:none">
												<td>
													<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="AddInlandMarineMIDDLE.aspx" TabTitles="Category Details"
														TabLength="150"></webcontrol:Tab>
												</td>
											</tr>
											<tr>
												<td>
													<table class="tableeffect" cellpadding="0" cellspacing="0" border="0">
														<tr>
															<td>
																<iframe id="tabLayer" runat="server" src="" scrolling="no" frameborder="0" width="100%"
																	height="1000"></iframe>
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<asp:Label ID="lblTemplate" Runat="server" Visible="false"></asp:Label>
										</table>
									</td>
								</tr>
								<tr>
									<td>
										<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidREC_VEH_ID" type="hidden" value="0" name="hidREC_VEH_ID" runat="server">
										<INPUT id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server"> <INPUT id="hidPolicyVersionID" type="hidden" name="hidPolicyVersionID" runat="server">
										<INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server"> <INPUT id="hidAPP_LOB" type="hidden" value="0" name="hidAPP_LOB" runat="server">
										<INPUT id="hidOldXml" type="hidden" name="hidOldXml" runat="server"> <INPUT id="hidPolcyType" type="hidden" name="hidPolcyType" runat="server">
										<INPUT id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server"> <INPUT id="hidROW_COUNT" type="hidden" value="0" name="hidAPP_LOB" runat="server">
										<INPUT id="hidFilledCovgXML" type="hidden" value="0" name="hidFilledCovgXML" runat="server">
									</td>
								</tr>
							</table>
						</TD>
					</TR>
				</table>
			</form>
		</div>
		<script>
			SetRowLabelDropDown();
		</script>
	</body>
</HTML>
