<%@ Page language="c#" Codebehind="BankReconDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.BankReconDetails" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BankReconDetails</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=base.GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript" type="text/javascript">
			var CheckVisible = 1;
			var DepositVisible = 1;
			var JEVisible = 1;
			var OtherVisible = 1;
						
			function setPointer() 
			{ 
				if (document.all) 
					for (var i=0;i < document.all.length; i++) document.all(i).style.cursor = 'wait'; 
			} 
			
			function resetPointer() 
			{ 
				if (document.all) 
				for (var i=0;i < document.all.length; i++) document.all(i).style.cursor = 'default'; 
			}
			function ShowHideChecks()
			{	
			
				var total_checks = 0;
				if(document.getElementById('hidTOTAL_CHECKS').value != ""
					 || document.getElementById('hidTOTAL_CHECKS').value != "0")
				{
					total_checks= parseInt(document.getElementById('hidTOTAL_CHECKS').value);
				}
				if(CheckVisible == 1)
				{
					for(i=1;i<=total_checks;i++)
					{
						if(document.getElementById("dgRecondetails_trCheckDetail" + i)!=null)
						{
							document.getElementById("dgRecondetails_trCheckDetail" + i).style.display =  'none'; 
							ExpandCollapse("dgRecondetails_trCheckHead","none");
						}
					}
					
					CheckVisible= 0;
				}
				else
				{
					for(i=1;i<=total_checks;i++)
					{
						if(document.getElementById("dgRecondetails_trCheckDetail" + i)!=null)
						{
							document.getElementById("dgRecondetails_trCheckDetail" + i).style.display =  'inline'; 
							ExpandCollapse("dgRecondetails_trCheckHead","inline");
						}
					}
					CheckVisible= 1;
				}
			}
			function ShowHideDeposits()
			{
			
			
				var total_deposits = 0;
				if(document.getElementById('hidTOTAL_DEPOSITS').value != ""
					 || document.getElementById('hidTOTAL_DEPOSITS').value != "0")
				{
					total_deposits= parseInt(document.getElementById('hidTOTAL_DEPOSITS').value);
				}
				if(DepositVisible == 1)
				{
					for(i=1;i<=total_deposits;i++)
					{
						if(document.getElementById("dgRecondetails_trDepositDetail" + i)!=null)
						{
							document.getElementById("dgRecondetails_trDepositDetail" + i).style.display =  'none'; 
							ExpandCollapse("dgRecondetails_trDepositHead","none");
						}
					}
					
					DepositVisible= 0;
				}
				else
				{
					for(i=1;i<=total_deposits;i++)
					{
						if(document.getElementById("dgRecondetails_trDepositDetail" + i)!=null)
						{
							document.getElementById("dgRecondetails_trDepositDetail" + i).style.display =  'inline'; 
							ExpandCollapse("dgRecondetails_trDepositHead","inline");
						}
					}
					DepositVisible= 1;
				}
			}
			
			function ShowHideJEs()
			{
			
				var total_je = 0;
				if(document.getElementById('hidTOTAL_JE').value != ""
					 || document.getElementById('hidTOTAL_JE').value != "0")
				{
					total_je= parseInt(document.getElementById('hidTOTAL_JE').value);
				}
				if(JEVisible == 1)
				{
					for(i=1;i<=total_je;i++)
					{
						if(document.getElementById("dgRecondetails_trJEDetail" + i)!=null)
						{
							document.getElementById("dgRecondetails_trJEDetail" + i).style.display =  'none'; 
							ExpandCollapse("dgRecondetails_trJEHead","none");
						}
					}
					
					JEVisible= 0;
				}
				else
				{
					for(i=1;i<=total_je;i++)
					{
						if(document.getElementById("dgRecondetails_trJEDetail" + i)!=null)
						{
							document.getElementById("dgRecondetails_trJEDetail" + i).style.display =  'inline'; 
							ExpandCollapse("dgRecondetails_trJEHead","inline");
						}
					}
					JEVisible= 1;
				}
			}
			
			function ShowHideOthers()
			{
			
			
				var total_others = 0;
				if(document.getElementById('hidTOTAL_OTHER').value != ""
					 || document.getElementById('hidTOTAL_OTHER').value != "0")
				{
					total_others= parseInt(document.getElementById('hidTOTAL_OTHER').value);
				}
				if(OtherVisible == 1)
				{
					for(i=1;i<=total_others;i++)
					{
						if(document.getElementById("dgRecondetails_trOtherDetail" + i)!=null)
						{
							document.getElementById("dgRecondetails_trOtherDetail" + i).style.display =  'none'; 
							ExpandCollapse("dgRecondetails_trOtherHead","none");
						}
					}
					
					OtherVisible= 0;
				}
				else
				{
					for(i=1;i<=total_others;i++)
					{
						if(document.getElementById("dgRecondetails_trOtherDetail" + i)!=null)
						{
							document.getElementById("dgRecondetails_trOtherDetail" + i).style.display =  'inline'; 
							ExpandCollapse("dgRecondetails_trOtherHead","inline");
						}
					}
					OtherVisible= 1;
				}
			}
			
			function ExpandDetailsOnLoad()
			{
				ShowHideOthers();
				ShowHideJEs();	
				ShowHideDeposits();
				ShowHideChecks();
				
			}
			
			function ExpandCollapse(grid,display)
			{
				var cntrl = grid + "_imgCollaspe";
				if(display == "none")
					document.getElementById(cntrl).src	=	'<%=pathPlus%>';
				else
					document.getElementById(cntrl).src	=	'<%=pathMinus%>';	
				
			}
	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="ExpandDetailsOnLoad();">
		<form id="BANK_RECON_DEETAILS" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="90%" align="center" border="0">
				<tr>
					<TD class="headereffectCenter" colSpan="3">Bank Reconciliation Item Details</TD>
				</tr>
				<tr>
					<TD class="midcolora" colSpan="1">Account Number:
					</TD>
					<TD class="midcolorr" colSpan="2"><asp:label id="lblAcc_Number" runat="server"></asp:label></TD>
				</tr>
				<tr>
					<TD class="midcolora" colSpan="1">General Ledger Balance:
					</TD>
					<TD class="midcolorr" colSpan="2"><asp:label id="lblGLBalance" runat="server"></asp:label></TD>
				</tr>
				<tr>
					<TD class="midcolora" colSpan="1">Outstandings:
					</TD>
					<TD class="midcolorr" colSpan="2"><asp:label id="lblOutStandings" runat="server"></asp:label></TD>
				</tr>
				<tr>
					<td class="midcolora" colSpan="1">Bank Charges:
					</td>
					<td class="midcolorr" colspan="2"><asp:Label ID="lblBankCharges" Runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<TD class="midcolora" colSpan="1">Calculated Balance:
					</TD>
					<TD class="midcolorr" colSpan="2"><asp:label id="lblCalcBalance" runat="server"></asp:label></TD>
				</tr>
				<tr>
					<TD class="midcolora" colSpan="1">Bank Ending Balance:
					</TD>
					<TD class="midcolorr" colSpan="2"><asp:label id="lblEndingBalance" runat="server"></asp:label></TD>
				</tr>
				 <tr>
                    <TD class="midcolora" colSpan="1">Statement Date:
					</TD>
					<TD class="midcolorr" colSpan="2"><asp:label id="lblStatementDate" runat="server"></asp:label></TD>				 				 
				 </tr>
				 
				<tr>
					<td id="Grid" colSpan="3"><asp:datagrid id="dgRecondetails" DataKeyField="IDENTITY_ROW_ID" Width="100%" AlternatingItemStyle-CssClass="alternatedatarow"
							ItemStyle-CssClass="datarow" HeaderStyle-CssClass="headereffectCenter" AutoGenerateColumns="False" Runat="server">
							<Columns>
								<asp:TemplateColumn HeaderText="Select">
									<ItemTemplate>
										<asp:Image ID='imgCollaspe' Runat="server" ImageUrl="/cms/cmsweb/Images/plus2.gif" Visible="False"></asp:Image>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Transaction Type">
									<ItemTemplate>
										<asp:label ID="lblTransactionType" Runat=server text='<%# DataBinder.Eval(Container.DataItem,"TransactionType")%>'>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Transaction Date">
									<ItemTemplate>
										<asp:label ID="lblSOURCE_TRAN_DATE" Runat=server text='<%# DataBinder.Eval(Container.DataItem,"SOURCE_TRAN_DATE")%>'>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Source Number">
									<ItemTemplate>
										<asp:label ID="lblSOURCE_NUM" Runat=server text='<%# DataBinder.Eval(Container.DataItem,"SOURCE_NUM")%>'>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Note/Memo">
									<ItemTemplate>
										<asp:label ID="lblNote" Runat=server text='<%# DataBinder.Eval(Container.DataItem,"NoteMemo")%>'>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Transaction Amount" ItemStyle-CssClass="midcolorr">
									<ItemTemplate>
										<asp:label ID="lblTRANSACTION_AMOUNT" Runat=server text='<%# DataBinder.Eval(Container.DataItem,"TRANSACTION_AMOUNT")%>'>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
				  <td>
				  <cmsb:cmsbutton class="clsButton" aligh="Right"  id="btnPrint" runat="server" Text="Print"></cmsb:cmsbutton>
				   </td>
				</tr>
				
			</TABLE>
			<input id="hidTOTAL_DEPOSITS" type="hidden" name="hidTOTAL_DEPOSITS" runat="server">
			<input id="hidTOTAL_CHECKS" type="hidden" name="hidTOTAL_CHECKS" runat="server">
			<input type="hidden" id="hidTOTAL_JE" runat="server" NAME="hidTOTAL_JE"> <input type="hidden" id="hidTOTAL_OTHER" runat="server" NAME="hidTOTAL_OTHER">
		</form>
	</body>
</HTML>
