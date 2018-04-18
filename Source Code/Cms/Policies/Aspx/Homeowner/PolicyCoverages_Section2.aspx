<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page language="c#" Codebehind="PolicyCoverages_Section2.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.aspx.HomeOwners.PolicyCoverages_Section2" ValidateRequest=false %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Coverages_Section2</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="../../../cmsweb/scripts/xmldom.js"></script>
		<script src="../../../cmsweb/scripts/common.js"></script>
		<script src="../../../cmsweb/scripts/form.js"></script>
		<script src="../../../cmsweb/scripts/Coverages.js"></script>
		<script language="javascript">
			var prefix = "dgCoverages__ctl";
			
			//This variable will be used by tab control, for checking ,whether save msg should be shown to user
			//while changing the tabs
			//True means Want to save msg should always be shown to user
			var ShowSaveMsgAlways = true;
				
			function Initialize()
			{
				document.getElementById("btnDelete").disabled = true;
			}
			
		/*	function Reset()
			{
			ChangeColor();
			DisableValidators();
				document.Form1.reset();
				return false;
			}	*/
			
			var noOfRowsSelected = 0;
			function EnableDelete(objChk)
			{
				if(objChk.checked)
				{
					noOfRowsSelected++;
				}
				else
				{
					if ( noOfRowsSelected > 0 )
					{
						noOfRowsSelected--;
					}
				}
				
				if(noOfRowsSelected>0)
					document.getElementById("btnDelete").disabled = false;
				else
					document.getElementById("btnDelete").disabled = true;
			}
			
		
		function EnableDisableControls(str,obj)
			{
			
				
				//alert(str);
				
				
				var arrEnab=new Array();
				
				
				arrEnab=str.split(",");
				
				//arrEnab[0]=437;
				
				if(arrEnab.length>0)
				{
					for(i=0;i<arrEnab.length;i++)
					{
						var chkObj=eval(document.getElementById("Row_" + arrEnab[i])); 
					
						if ( chkObj != null )
						{
							if(arrEnab[i]!=null && arrEnab[i]!="")
							{
								if (  document.getElementById(obj).checked )
								{
									GetTextCtrl( arrEnab[i],true);	
									document.getElementById("Row_" + arrEnab[i]).disabled=true;
									
																	
								}
								else
								{
									GetTextCtrl( arrEnab[i],false);	
									document.getElementById("Row_" + arrEnab[i]).disabled=false;
									
								}
								
								
							}
						}
											
					}
				
				}
									
			}
			
			function GetTextCtrl(intCovId,status)
			{
				var ctr = 0;
				var rowCount = document.Form1.hidROW_COUNT.value;
				for (ctr = 2; ctr<rowCount; ctr++)
				{
					chk = document.getElementById(prefix + ctr + "_lblLIMIT");
					//chk1 = document.getElementById(prefix + ctr + "_cbDelete");
					if (chk != null )
					{
					
						if (chk.getAttribute("COVERAGE_ID") == intCovId)
						{
							///alert('dfd');
							chk.disabled = status;
							//chk1.checked = status;
							checkbox = document.getElementById(prefix + ctr + "_cbDelete");
							 if ( checkbox != null ) 
							 {
							  checkbox.checked = false;
							   }
							
						}
					}
					
				}	
			}
			function OnClickCheck(rowNo)
			{
				//alert('hello');			
				txt = document.getElementById(prefix + rowNo + "_lblLIMIT");
				txtLimit = document.getElementById(prefix + rowNo + "_txtLIMIT");
				//lbl = document.getElementById(prefix + rowNo + "_lblLIMIT");
				chk = document.getElementById(prefix + rowNo + "_cbDelete");
				ddl = document.getElementById(prefix + rowNo + "_ddlLIMIT");
				ddlDeduc=document.getElementById(prefix + rowNo + "_ddlDEDUCTIBLE");
				//Added by Charles on 17-Aug-09 for Itrack 6210
				var rev=document.getElementById(prefix + rowNo + "_revLIMIT_DEDUC_AMOUNT");
				var rng=document.getElementById(prefix + rowNo + "_rngtxtLIMIT");//Added by Charles on 6-Oct-09 for Itrack 6463
				OnDDLChange(ddlDeduc);

				/*
				if ( txtLimit != null )
				{
					if(rowNo=="2" ||rowNo=="3"||rowNo=="4"||rowNo=="5"||rowNo=="11"||rowNo=="17"||rowNo=="18")
					{
						
						txtLimit.style.display='inline';
						return;
					}
					else
					{
						txtLimit.style.display='none';
					}
				
				}*/
								
				if(chk.checked==true)
				{
					
					if ( txtLimit != null )
					{
						txtLimit.style.display='inline';
						//Added by Charles on 10-Dec-09 for Itrack 6840
						var span = chk.parentElement;			
						if ( span != null )
						{			
							var covCode = span.getAttribute("COV_CODE");				
							if ( covCode == 'APOBI' )
							{
								if(chk.disabled && chk.checked)
								{
									txtLimit.readOnly =true;
								}
								else
								{
									txtLimit.readOnly =false;
								}
							}
						}//Added till here
					}
					
				}
				else
				{
					if ( txtLimit != null )
					{
						//Added by Charles on 17-Aug-09 for Itrack 6210, disable validator when checkbox is unchecked					
						rev.setAttribute('isValid',true);
						rev.style.display='none';
						txtLimit.value='';
						//Added till here
						rng.setAttribute('isValid',true);//Added by Charles on 6-Oct-09 for Itrack 6463
						rng.style.display='none';//Added by Charles on 6-Oct-09 for Itrack 6463	
						txtLimit.style.display='none';
					}
					
				}
			}
			
			function CalculateIncluded(intCovID)
			{
				
				var xmlDoc = new ActiveXObject("Microsoft.XMLDOM") ;
				xmlDoc.async=false;
				xmlDoc.loadXML(document.getElementById('hidOldXml').value);
				rowObj = xmlDoc.selectNodes('/Root/Home/coverage/dependancy/coverage[@COV_ID="' + intCovID + '"]')[0];
				
				var len = rowObj.childNodes.length;
				
				if (rowObj != null)
				{
					alert(rowObj.attributes[3].value);
					alert(rowObj.value);
				}
			}
			function OnChange(intCOV_ID)
			{
			
				
				var xmlDoc = new ActiveXObject("Microsoft.XMLDOM") ;
				xmlDoc.async=false;
				xmlDoc.loadXML(document.getElementById('hidOldXml').value);
				rowObj = xmlDoc.selectNodes('/Root/Home/coverage[@COV_ID="' + intCOV_ID + '"]/dependancy')[0];
				
				
				var len = rowObj.childNodes.length;
				
				
				for (ctr = 0 ; ctr< len; ctr++)
				{
								
					txt = GetTextCtrlByCobId(rowObj.childNodes[ctr].attributes[0].value);
					txtCurCtrl = GetTextCtrlByCobId(intCOV_ID);
					if(rowObj.childNodes[ctr].attributes[4].value ==document.getElementById('hidPolcyType').value || rowObj.childNodes[ctr].attributes[4].value =="")
					{
					if (txtCurCtrl.value != "")
					{
						txt.value = parseFloat(txtCurCtrl.value) * ( parseFloat(rowObj.childNodes[ctr].attributes[3].value) / 100);
					}
					
					}
				}
			}
			
			function GetTextCtrlByCobId(intCovId)
			{
				var ctr = 0;
				var rowCount = document.Form1.hidROW_COUNT.value;
				for (ctr = 2; ctr<rowCount; ctr++)
				{
					chk = document.getElementById(prefix + ctr + "_lblLIMIT");
					if (chk != null)
					{
						
						if (chk.getAttribute("COVERAGE_ID") == intCovId)
						{
								return chk;
						}
					}
					else
					{
						break;
					}
				}	
			}
			
			
			function InitInstance()
			{
				var ctr = 0;
				for (ctr = 2; ctr<100; ctr++)
				{
					chk = document.getElementById(prefix + ctr + "_cbDelete");
					if (chk != null)
					{
						OnClickCheck(ctr);
					}
					else
					{
						break;
					}
				}
				//SetIOPSL();
			}
			function LocationPolicy()
			{
				//alert(self.parent.location )
				self.parent.location='cms/policies/aspx/policytab.aspx?customer_id=' + document.Form1.hidCustomerID.value + '&policy_id=' + document.Form1.hidPOLID.value + '&policy_version_id=' + document.Form1.hidPOLVersionID.value + '&app_version_id=' + document.Form1.hidAppVersionID.value + '&app_id=' + document.Form1.hidAppId.value + '&'; 
			}
			function LocationLimit()
			{
			  //cms/policies/aspx/homeowner/PolicyAddDwellingDetails.aspx?CalledFrom=Home&DWELLING_ID=1&
				self.location ='cms/policies/aspx/homeowner/PolicyAddDwellingDetails.aspx?DWELLING_ID=' + document.Form1.hidREC_VEH_ID.value + '&CalledFrom= Home&';
				changetab(4,0);
			}
			
			//Gets the checkbox with the passed in code
			function GetCheckBoxFromCode(covCode)
			{
				var rowCount = 50;
				
				if ( document.Form1.hidROW_COUNT != null )
				{
					rowCount = document.Form1.hidROW_COUNT.value;
				}
						
				for (ctr = 2; ctr < rowCount + 2; ctr++)
				{
					chk = document.getElementById(prefix + ctr + "_cbDelete");
					
					if ( chk != null )
					{
						var span = chk.parentElement;
									
						if ( span != null )
						{
							var coverageCode = span.getAttribute("COV_CODE");
					
							if ( trim(covCode) == trim(coverageCode) )
							{
								//alert(covCode + ' ' + coverageCode );
								return chk;
							}
						}
					}
				}
				
				return null;
						
			}
			function SetIOPSL()
			{
					//var cb = GetCheckBoxFromCode("IOPSI");
					var cb = GetCheckBoxFromCode("IOPSS");
					var cbIOPSL = GetCheckBoxFromCode("IOPSL");
					
					if ( cb.checked == true )
					{
						//cbIOPSL.checked = false;
						cbIOPSL.parentElement.parentElement.parentElement.disabled = false;
						cbIOPSL.disabled = false;
						//DisableItems(cb21.id);		
					}
					else
					{
						cbIOPSL.checked = false;
						cbIOPSL.parentElement.parentElement.parentElement.disabled = true;
						cbIOPSL.disabled = true;	
					}
			
			}
			
			//Executes on the check and uncheck of the check boxes
			function onCheck(CheckBoxID)
			{	
				var cb = document.getElementById(CheckBoxID);
				var span = cb.parentElement;
				
				if ( span == null ) return;
				
				var covID = span.getAttribute("COV_ID");
				var covCode = span.getAttribute("COV_CODE");
				
				if ( covCode == 'IOPSS' )
				{
					var cbIOPSL = GetCheckBoxFromCode("IOPSL");
					
					if ( cb.checked == true )
					{
						//cbIOPSL.checked = false;
						cbIOPSL.parentElement.parentElement.parentElement.disabled = false;
						cbIOPSL.disabled = false;
						//DisableItems(cb21.id);		
					}
					else
					{
						cbIOPSL.checked = false;
						cbIOPSL.parentElement.parentElement.parentElement.disabled = true;
						cbIOPSL.disabled = true;	
					}
				}
				
			}
			function populateAdditionalInfo(NodeName)
			{
				var tempXML=parent.strSelectedRecordXML;	
				var objXmlHandler = new XMLHandler();
				var unqID;
				if(tempXML==null || tempXML=="")
					return;
				var tree = (objXmlHandler.quickParseXML(tempXML).getElementsByTagName('Table')[0]);					
				
				if(tree)
				{				
					for(i=0;i<tree.childNodes.length;i++)
					{
						if(tree.childNodes[i].nodeName==NodeName)
						{
							if(tree.childNodes[i].firstChild)
								unqID=tree.childNodes[i].firstChild.text;
						}					
					}
				}
				return unqID;
			}
		function populateInfo()
		{
			if (this.parent.strSelectedRecordXML == "-1")
			{
				setTimeout('populateInfo();',100);
				return;
			}
				
						document.getElementById('hidDataValue1').value =populateAdditionalInfo("DWELLING_NUMBER");
					document.getElementById('hidDataValue2').value =populateAdditionalInfo("Address");					
					if(document.getElementById('hidDataValue1').value=='undefined')
						document.getElementById('hidDataValue1').value="";
					if(document.getElementById('hidDataValue2').value=='undefined')
						document.getElementById('hidDataValue2').value="";
					document.getElementById('hidCustomInfo').value=";Dwelling # = " + document.getElementById('hidDataValue1').value + ";Address = " + document.getElementById('hidDataValue2').value;										
					
				
				
			
			
		}
		function OnDDLChange(ddl)
			{
				var COV_CODE = '';
				if(ddl == null) return;
				if ( ddl.getAttribute('COV_CODE') == null ) return;
				COV_CODE = ddl.getAttribute('COV_CODE');
				//to set medical limit to "Extended from primary" if Coverage E limit is selected to "Extended from Primary"
				if(COV_CODE	=="PL")
				{
					var limitID=ddl.options[ddl.selectedIndex].value;
					var MedicalDDl = GetControlFromCode('MEDPM','_ddlDEDUCTIBLE')
					if (limitID=="1293" || limitID=="1294")
					{
						if (MedicalDDl!=null)
						{
							for ( i=0;i<MedicalDDl.options.length;i++)
							{
								if (MedicalDDl.options[i].value=="1410" || MedicalDDl.options[i].value=="1411")
								{
									MedicalDDl.options.selectedIndex=i;
									MedicalDDl.disabled=true;
									break;
								}
							}
						}
					}
					else
					{
					 if (MedicalDDl.options.selectedIndex==0)
						MedicalDDl.options.selectedIndex=1;
					MedicalDDl.disabled=false;
					}
					return;
				}
				//if Coverage E limit is not selected to "Extended from Primary" then medical not alowed to limit"Extended from primary" 
				if(COV_CODE	=="MEDPM")
				{
					var limitID=ddl.options[ddl.selectedIndex].value;
					var liabilityDDl = GetControlFromCode('PL','_ddlDEDUCTIBLE')
					var liabilityID=liabilityDDl.options[liabilityDDl.selectedIndex].value;
					if((limitID=="1410" || limitID=="1411") && !(liabilityID=="1293" ||liabilityID=="1294"))
					  {
					     ddl.selectedIndex=1;
					  }
					return;
				}
		 }
		</script>
	</HEAD>
	<body onload="InitInstance();populateInfo();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr id="trError" runat="server">
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblError" runat="server" CssClass="errmsg"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<table cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td class="headereffectCenter"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow><asp:label id="lblTitle" runat="server">Section 2 Coverages</asp:label></td>
							</tr>
							<tr>
								<td align="center"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False" EnableViewState="False"></asp:label></td>
							</tr>
							<tr>
								<td class="midcolora"><asp:datagrid id="dgCoverages" runat="server" DataKeyField="COVERAGE_ID" AutoGenerateColumns="False"
										Width="100%">
										<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="Required /Optional">
												<ItemTemplate>
													<asp:CheckBox id="cbDelete" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' COV_ID='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
													</asp:CheckBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Number" Visible="True">
												<ItemStyle Width="8%"></ItemStyle>
												<ItemTemplate>
													<select id="ddlLIMIT" Visible="False" Runat="server">
													</select>
													<asp:TextBox ID=txtLIMIT Runat=server Width=22 MaxLength=1 Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT_1") %>'>></asp:TextBox>
													<asp:label id="lblLIMIT" CssClass="labelfont" Visible="False" Runat="server"></asp:label><BR>
													<asp:RegularExpressionValidator ID="revLIMIT_DEDUC_AMOUNT" Runat="server" ControlToValidate="txtLIMIT" Display="Dynamic"></asp:RegularExpressionValidator>
													<asp:rangevalidator id="rngtxtLIMIT" Enabled="false" Display="Dynamic" runat="server" ControlToValidate="txtLIMIT" ErrorMessage="There are over 4 Rental Dwellings - A Separate policy must be issued" MinimumValue="1" MaximumValue="4" Type="Integer"></asp:rangevalidator> <!-- Added by Charles on 6-Oct-09 for Itrack 6463 -->
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Coverage">
												<ItemTemplate>
													<asp:Label runat="server" ID="lblCOV_DESC" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn Visible="False" HeaderText="Limit 2">
												<ItemTemplate>
													<select ID="ddlLIMIT_2_TYPE" Runat="server">
													</select>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn Visible="True" HeaderText="Additional">
												<ItemTemplate>
													<select id="ddlDEDUCTIBLE" Visible="False" Runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' onchange="javascript:OnDDLChange(this);">
													</select>
													<asp:TextBox id="txtDEDUCTIBLE_1_TYPE" Visible="False" Runat="server"></asp:TextBox>
													<asp:Label id="lblDEDUCTIBLE" CssClass="labelfont" Runat="server"></asp:Label><BR>
													<asp:RegularExpressionValidator id="revDEDUCTIBLE_1_TYPE" Runat="server" ErrorMessage="Invalid data" Display="Dynamic"
														ControlToValidate="txtDEDUCTIBLE_1_TYPE"></asp:RegularExpressionValidator>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn Visible="False" HeaderText="Limit Type">
												<ItemTemplate>
													<asp:Label ID="lblLIMIT_TYPE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT_TYPE") %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn Visible="False" HeaderText="Deduct Type">
												<ItemTemplate>
													<asp:Label ID="lblDEDUCTIBLE_TYPE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_TYPE") %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn Visible="False" HeaderText="COV_ID">
												<ItemTemplate>
													<asp:Label ID="lblCOV_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="COV_CODE" Visible="False"></asp:BoundColumn>
										</Columns>
									</asp:datagrid></td>
							</tr>
							<tr>
								<td>
									<table width="100%">
										<tr>
											<td class="midcolora" colSpan="3">
												<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Visible="False" Text="Delete" Enabled="False"></cmsb:cmsbutton></td>
											<td class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" CausesValidation="False"></cmsb:cmsbutton></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td><INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidREC_VEH_ID" type="hidden" value="0" name="hidREC_VEH_ID" runat="server">
									<INPUT id="hidAppID" type="hidden" name="hidAppID" runat="server"> <INPUT id="hidAppVersionID" type="hidden" name="hidAppVersionID" runat="server">
									<INPUT id="hidPolID" type="hidden" name="hidPolID" runat="server"> <INPUT id="hidPolVersionID" type="hidden" name="hidPolVersionID" runat="server">
									<INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server"> <INPUT id="hidPol_LOB" type="hidden" value="0" name="hidPol_LOB" runat="server">
									<INPUT id="hidOldXml" type="hidden" name="hidOldXml" runat="server"> <INPUT id="hidPolcyType" type="hidden" name="hidPolcyType" runat="server">
									<INPUT id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server"> <INPUT id="hidROW_COUNT" type="hidden" value="0" name="hidAPP_LOB" runat="server">
									<INPUT id="hidDataValue1" type="hidden" name="hidDataValue1" runat="server"> <INPUT id="hidDataValue2" type="hidden" name="hidDataValue2" runat="server">
									<INPUT id="hidDataValue3" type="hidden" name="hidDataValue3" runat="server"> <INPUT id="hidCustomInfo" type="hidden" name="hidCustomInfo" runat="server">
								</td>
							</tr>
						</table>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>

