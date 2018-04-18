<%@ Page language="c#" Codebehind="FactorIndex.aspx.cs" validateRequest="false" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.FactorIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BRICS- FACTORS</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<SCRIPT src="/cms/cmsweb/scripts/common.js"></SCRIPT>
		<SCRIPT src="/cms/cmsweb/scripts/form.js"></SCRIPT>
		<script language="javascript">
		var txtRows;
		 
		function AddData()
		{

			document.FACTORS.reset();
			DisableValidators();
			ChangeColor();
			return false;
		}
		function addNewRow()
		{
			var objNewTR;
			var txtLength = 0, selLength = 0, idCounter = 0;
			var txtObjects;
			var curId = 0;
			var trFirstRow;
			
			if(isNaN(txtRows) || txtRows=="0" )
			{
				if(isNaN(document.getElementById('txtRows').value) || txtRows=="0" )
					{txtRows=0;}
				else
				    {txtRows=document.getElementById('txtRows').value;}
			}
					  
			txtRows++;
			curId = (txtRows-1);
			 
			document.getElementById('txtRows').value = txtRows;	
			objNewTR = trValueRows_0.cloneNode(true);
			
			clearValues(objNewTR);
			
		    //Setting the IDs of the row
		    objNewTR.id='trValueRows_'+eval(eval(curId));			   
			
			//Setting the IDs of the textboxes
			txtObjects = objNewTR.getElementsByTagName("INPUT");
			txtValidCounter = 0;
			  
			for (txtLength = 0 ; txtLength < txtObjects.length ; txtLength++)
			{	  
			
				idValue = txtObjects[txtLength].id;
				idName	= idValue.substr(3, idValue.lastIndexOf("_") - 2); 
				txtObjects[txtLength].id = "txt" + idName + eval(eval(curId));
				txtObjects[txtLength].name = "txt" + idName + eval(eval(curId));	
				txtObjects[txtLength].value="";					
				txtValidCounter++;
				  
			}	
			
			
			test.tBodies[0].appendChild(objNewTR);
		 
		}
		
		function clearValues(objNewTR){

				var objInput, colObjects , selObjects;
  				var lngLength;
				
				colObjects = objNewTR.getElementsByTagName("INPUT");
				for (lngLength = 0 ; lngLength < colObjects.length ; lngLength++){
					 
					if(colObjects[lngLength].type != "button")
					colObjects[lngLength].value = "";
				}
	
		 
			}
			
		function onBeforeSubmit()
		{ 
			var noOfRows = txtRows;
			var objTableRow;
			var txtLength = 0, selLength = 0, idCounter = 0, rowCounter=0;
			var txtObjects;
			var curId = 0;
			var trFirstRow;
			var returnString='',returnAttributes='';
			var textBoxName='';
					  
			if (isNaN(txtRows) || txtRows==0)
				{txtRows=document.getElementById('txtRows').value ;}
			
			txtRows++;
			curId = (txtRows-1);
			 
			document.getElementById('txtRows').value = txtRows;	
			if (noOfRows ==null || noOfRows =='undefined')
			{
				noOfRows=document.getElementById('txtRows').value ;
			}
			 
			 
			for (rowCounter = 0 ; rowCounter < noOfRows-1 ; rowCounter++)
			{	 
				objTableRow = document.getElementById('trValueRows_'+rowCounter);
			
				//Setting the IDs of the textboxes
				txtObjects = objTableRow.getElementsByTagName("INPUT");
				txtValidCounter = 0;
				returnAttributes='';
				for (txtLength = 0 ; txtLength < txtObjects.length ; txtLength++)
				{	  
					textBoxName =txtObjects[txtLength].name;
					textBoxName=textBoxName.substr(3, textBoxName.lastIndexOf("_")-3);
						
					returnAttributes = returnAttributes +textBoxName + "='" + txtObjects[txtLength].value +"' ";
				}
				
				returnString = returnString + " <ATTRIBUTES " + returnAttributes + " />";
			}
			document.getElementById('hidAttributeNode').value=returnString;
		}
		</script>
		<!--onkeypress="if(event.keyCode==13){ document.getElementById('btnSave').click();return false;}"-->
	</HEAD>
	<BODY class="bodyBackGround" onresize="SmallScroll();"  leftMargin="0" topMargin="0" onload="top.topframe.main1.mousein = false;findMouseIn();showScroll();ApplyColor();">
		<!--Start: to add bottom menu--><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!--End: bottom menu-->
		<!--Start: to add band space -->
		<!--End: band space -->
		<div class="pageContent" id="bodyHeight">
			<FORM id="FACTORS" method="post" runat="server" onsubmit ="onBeforeSubmit();">
				<TABLE class="tableWidth" cellSpacing="0" cellPadding="0" border="0">
					<tr>
						<td class="headereffectCenter" colSpan="3">Rating Factors</td>
					</tr>
					<tr>
						<TD class="pageHeader" width="100%" colSpan="4">Please note that all fields marked 
							with * are mandatory</TD>
					</tr>
					<tr>
						<td class="midcolora" colspan=4>
							<asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label>
						</td>
					</tr>
								<tr>
									<TD class="headereffectSystemParams" colSpan="4">Products</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%">Products<span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbProducts" runat="server" AutoPostBack="True"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvProducts" runat="server" Display="Dynamic" ControlToValidate="cmbProducts"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="headereffectSystemParams" colSpan="4">Factors</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%">Factor<span class="mandatory">*</span></TD>
									<TD class="midcolora" width="82%"><asp:dropdownlist id="cmbFactors" runat="server" AutoPostBack="True"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvFactors" runat="server" Display="Dynamic" ControlToValidate="cmbFactors"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="headereffectSystemParams" colSpan="4">Nodes</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%">Nodes<span class="mandatory">*</span></TD>
									<TD class="midcolora" width="82%"><asp:dropdownlist id="cmbNodes" runat="server" AutoPostBack="True"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvNodes" runat="server" Display="Dynamic" ControlToValidate="cmbNodes"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<br>
									<TD class="headereffectSystemParams" colSpan="4">Add Attributes</TD>
								</tr>
							<!--	<tr>
									<td colSpan="4"><asp:datagrid id="Screenlisting" Visible="false" CssClass="midcolora" GridLines="Both" cellpadding="1"
											cellspacing="0" BorderWidth="1" ShowFooter="True" OnPageIndexChanged="Screenlisting_PageIndexChanged"
											OnEditCommand="Screenlisting_Edit" OnUpdateCommand="Screenlisting_Update" OnCancelCommand="Screenlisting_Cancel"
											PageSize="5" width="100%" AllowPaging="true" BorderStyle="Inset" autogeneratecolumns="true" Runat="server">
											<HeaderStyle font-bold="true"></HeaderStyle>
											<columns>
												<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel" EditText="Edit"></asp:EditCommandColumn>
											</columns>
											<PagerStyle Mode="NextPrev" Font-Bold="False" Font-Underline="False" NextPageText="<b>          Next></b>"
												ForeColor="black" HorizontalAlign="Center" PrevPageText="<b>Prev          </b>"></PagerStyle>
										</asp:datagrid></td>
								</tr> -->
								<asp:panel id="pnlAttributes" Runat="Server" Width="100%">
									<TR>
										<TD colSpan="4">
											<TABLE id="tblAttributesRow" cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
												<TBODY id="tblAttributesRowBody" style="VISIBILITY: visible">
													<TR id="trAttributes" colSpan="4">
														<TD colSpan="4">
															<asp:label id="lblTemp" Runat="server"></asp:label></TD>
													</TR>
												</TBODY>
											</TABLE>
										</TD>
									</TR>
								</asp:panel>
								<tr>
									<td class="midcolora" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" visible="false" Text="Reset" causesValidation="false"></cmsb:cmsbutton></td>
									<td class="midcolorr" align="right" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" visible="true" Text="Save"></cmsb:cmsbutton></td>
								</tr>
							
					</TR>
					<asp:textbox id="txtRows" runat="server" width="0" NAME="txtRows"></asp:textbox>
					<INPUT id="hidAttributeNode" type="hidden" name="hidAttributeNode" runat="server">
				</TABLE>
			</FORM>
		</div>
	</BODY>
</HTML>
