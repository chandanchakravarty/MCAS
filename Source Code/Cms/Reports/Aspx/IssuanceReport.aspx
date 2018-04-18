<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IssuanceReport.aspx.cs" Inherits="Cms.Reports.Aspx.IssuanceReport" %>


<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Issuance Report</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script>
		<script type="text/javascript" language="javascript">


		    function OpenPolicyLookup() {
		        var strPolicy = document.getElementById('hidPolicy').value;
		        var url = '<%=URL%>';
		        OpenLookupWithFunction(url, 'POLICY_APP_NUMBER', 'CUSTOMER_ID_NAME', 'hidPOLICYINFO', $("#txtPOLICY_NUMBER").text().trim(), 'DBPolicy', strPolicy , '', 'splitPolicy()');
		    }
		    //This function splits the policyid and policy version id and put it in different controls
		    function splitPolicy() {

		        if (document.getElementById("hidPOLICYINFO").value.length > 0) {

		            var arr = document.getElementById("hidPOLICYINFO").value.split("~");
		            $("#hidPOLICY_ID").val(arr[0]);
		            $("#hidPOLICY_VERSION_ID").val(arr[1]);
		            $("#txtPOLICY_NUMBER").val(arr[2]);
		            $("#hidCUSTOMER_ID").val(arr[6]);

		        }
		    }

		    //This function is used for opening a pop up window(Here RIbreakdown page is used as pop up)
		    function open_popup() {
		        var str;
//		        str = "/cms/Reports/Aspx/RIBreakdown.aspx?CUSTOMER_ID=" + $("#hidCUSTOMER_ID").val() + "&POLICY_ID=" + $("#hidPOLICY_ID").val() + "&POLICY_VERSION_ID=" + $("#hidPOLICY_VERSION_ID").val();
		        //		        window.open(str, "RIBreakdown", "resizable=yes,toolbar=0,location=0, directories=0, status=0, menubar=0,scrollbars=yes,width=800,height=500,left=150,top=50");
		        
		    }

    </script>
	</HEAD>
	<body oncontextmenu = "return false;" MS_POSITIONING="GridLayout" onload="top.topframe.main1.mousein = false;findMouseIn();">
		<form id="Form1" method="post" runat="server">
		  <DIV><webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu></DIV>	
			<!-- To add bottom menu -->
			
			<!-- To add bottom menu ends here -->
			<asp:Panel ID="Panel1" Runat="server">
				<TABLE class="tableWidth" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD class="pageHeader" id="tdClientTop" colSpan="4">
							<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer></TD>
					</TR>
					<TR>
						<TD class="headereffectCenter" colSpan="4" align="center"><asp:Label ID=capHeader runat="server"></asp:Label></TD><%--Issuance Report--%>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolora" width="36%">
							<asp:label id="lblRI" runat="server">Policy Number</asp:label></TD>
						<TD class="midcolora" width="64%" colSpan="3">
							<asp:TextBox id="txtPOLICY_NUMBER" runat="server" ReadOnly="True" size="40" 
                                ontextchanged="txtPOLICY_NUMBER_TextChanged"></asp:TextBox><IMG id="imgAGENCY_NAME" style="CURSOR: hand" onclick="OpenPolicyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"	runat="server"></TD>
					</TR>
					
					<TR>
						<TD class="midcolorc" colSpan="4">
						<cmsb:cmsbutton class="clsbutton" id="btnReport" Runat="server" Text="Display Report"   onclick="btnReport_Click" ></cmsb:cmsbutton>
						    
						</TD>
					</TR>
				</TABLE>
				
				<div>
				<asp:GridView ID="gv" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="SEGURADORA" HeaderText="SEGURADORA" 
                    SortExpression="SEGURADORA" />
                <asp:BoundField DataField="COD SUSEP" HeaderText="COD SUSEP" 
                    SortExpression="COD SUSEP" />
                 <asp:BoundField DataField="PRODUTO" HeaderText="PRODUTO" 
                    SortExpression="PRODUTO" />
                 <asp:BoundField DataField="PROCESSO" HeaderText="PROCESSO" 
                    SortExpression="PROCESSO" /> 
                 <asp:BoundField DataField="RAMO CONTÁBIL" HeaderText="RAMO CONTÁBIL" 
                    SortExpression="RAMO CONTÁBIL" /> 
                 <asp:BoundField DataField="FILIAL EMISSORA" ItemStyle-HorizontalAlign ="left" HeaderText="FILIAL EMISSORA" 
                    SortExpression="FILIAL EMISSORA" />
                 <asp:BoundField DataField="COD FILIAL EMISSORA" HeaderText="COD FILIAL EMISSORA" 
                    SortExpression="COD FILIAL EMISSORA" />
               <asp:TemplateField HeaderText="NRO APOLICE">
                    <ItemTemplate>
                    <div style='mso-number-format:"\@";'> <%# Eval("[NRO APOLICE]") %></div>         
                    </ItemTemplate>                   
                </asp:TemplateField>
                 <asp:BoundField DataField="NRO ENDOSSO" ItemStyle-HorizontalAlign ="Center" HeaderText="NRO ENDOSSO" 
                    SortExpression="NRO ENDOSSO" />
                 <asp:BoundField DataField="DATA BASE" ItemStyle-HorizontalAlign ="Center" HeaderText="DATA BASE" 
                    SortExpression="DATA BASE" /> 
                 <asp:BoundField DataField="DATA EMISSÃO" ItemStyle-HorizontalAlign ="Center" HeaderText="DATA EMISSÃO" 
                    SortExpression="DATA EMISSÃO" /> 
                 <asp:BoundField DataField="INÍCIO VIGÊNCIA" ItemStyle-HorizontalAlign ="Center" HeaderText="INÍCIO VIGÊNCIA" 
                    SortExpression="INÍCIO VIGÊNCIA" />
                 <asp:BoundField DataField="FINAL VIGÊNCIA" ItemStyle-HorizontalAlign ="Center" HeaderText="FINAL VIGÊNCIA" 
                    SortExpression="FINAL VIGÊNCIA" />
                <asp:BoundField DataField="ESTIPULANTE/SUB-ESTIPULANTE" HeaderText="ESTIPULANTE/SUB-ESTIPULANTE" 
                    SortExpression="ESTIPULANTE/SUB-ESTIPULANTE" />
                 <asp:BoundField DataField="CPF/CNPJ" HeaderText="CPF/CNPJ" 
                    SortExpression="CPF/CNPJ" />
                 <asp:BoundField DataField="MOEDA" HeaderText="MOEDA" 
                    SortExpression="MOEDA" /> 
                 <asp:BoundField DataField="PREMIO EMITIDO" ItemStyle-HorizontalAlign ="right" HeaderText="PREMIO EMITIDO" 
                    SortExpression="PREMIO EMITIDO" /> 
                 <asp:BoundField DataField="PREMIO COSSEGURO ACEITO" ItemStyle-HorizontalAlign ="right" HeaderText="PREMIO COSSEGURO ACEITO" 
                    SortExpression="PREMIO COSSEGURO ACEITO" /> 
                 <asp:BoundField DataField="PREMIO CANCELAMENTO SEGURO" ItemStyle-HorizontalAlign ="right" HeaderText="PREMIO CANCELAMENTO SEGURO DIRETO"/>
                <asp:BoundField DataField="PREMIO CANCELAMENTO COSSEGURO" ItemStyle-HorizontalAlign ="right"  HeaderText="PREMIO CANCELAMENTO COSSEGURO ACEITO"/>
                <asp:BoundField DataField="RESTITUICAO PREMIO SEGURO DIRETO" ItemStyle-HorizontalAlign ="right"  HeaderText="RESTITUICAO PREMIO SEGURO DIRETO"/>
                <asp:BoundField DataField="RESTITUICAO PREMIO COSSEGURO ACEITO" HeaderText="RESTITUICAO PREMIO COSSEGURO ACEITO"/>
                 <asp:BoundField DataField="PREMIO COSSEGURO CEDIDO" ItemStyle-HorizontalAlign ="right"  HeaderText="PREMIO COSSEGURO CEDIDO"/>
                 <asp:BoundField DataField="PREMIO TOTAL" ItemStyle-HorizontalAlign ="right"  HeaderText="PREMIO TOTAL"/>
                <asp:BoundField DataField="PREMIO RESSEGURO FACULTATIVO" ItemStyle-HorizontalAlign ="right"  HeaderText="PREMIO RESSEGURO FACULTATIVO"/>
                <asp:BoundField DataField="PREMIO RESSEGURO CONTRATO" ItemStyle-HorizontalAlign ="right"  HeaderText="PREMIO RESSEGURO CONTRATO"/>
                <asp:BoundField DataField="TOTAL PREMIO RESSEGURO" ItemStyle-HorizontalAlign ="right"  HeaderText="TOTAL PREMIO RESSEGURO"/>
                 <asp:BoundField DataField="PREMIO RETIDO" ItemStyle-HorizontalAlign ="right"  HeaderText="PREMIO RETIDO"/>
                <asp:BoundField DataField="PPNG" ItemStyle-HorizontalAlign ="right"  HeaderText="PPNG"/>
                <asp:BoundField DataField="PREMIO GANHO" ItemStyle-HorizontalAlign ="right"  HeaderText="PREMIO GANHO"/>
                <asp:BoundField DataField="NOME CORRETOR LIDER" ItemStyle-HorizontalAlign ="left"  HeaderText="NOME CORRETOR LIDER"/>
                 <asp:BoundField DataField="COD CORRETOR LIDER" ItemStyle-HorizontalAlign ="center"  HeaderText="COD CORRETOR LIDER"/>
                <asp:BoundField DataField="VALOR COMISSOA CORRETAGEM" ItemStyle-HorizontalAlign ="right"  HeaderText="VALOR COMISSOA CORRETAGEM"/>
                              <asp:BoundField DataField="VALOR COMISSAO COSSEGURO" ItemStyle-HorizontalAlign ="right"  HeaderText="VALOR COMISSAO COSSEGURO"/>
                <asp:BoundField DataField="TOTAL COMISSAO PAGA (AG + AH)" ItemStyle-HorizontalAlign ="right"  HeaderText="TOTAL COMISSAO PAGA (AG + AH)"/>
                <asp:BoundField DataField="COMISSAO COSSEGURO ACEITO" ItemStyle-HorizontalAlign ="right"  HeaderText="COMISSAO COSSEGURO ACEITO"/>
                <asp:BoundField DataField="COMISSAO DE RESSEGURO EM COSSEGURO ACEITO" ItemStyle-HorizontalAlign ="right"  HeaderText="COMISSAO DE RESSEGURO EM COSSEGURO ACEITO"/>
                <asp:BoundField DataField="COMISSAO COSSEGURO CEDIDO" ItemStyle-HorizontalAlign ="right"  HeaderText="COMISSAO COSSEGURO CEDIDO"/>
                <asp:BoundField DataField="TOTAL COMISSAO RECEBIDA(AK + AL)" ItemStyle-HorizontalAlign ="right"  HeaderText="TOTAL COMISSAO RECEBIDA(AK + AL)"/>
                <asp:BoundField DataField="COMISSAO LÍQUIDA(AI - AM)" ItemStyle-HorizontalAlign ="right"  HeaderText="COMISSAO LÍQUIDA(AI - AM)"/>
                 <asp:BoundField DataField="RESERVA DESPESA DE COMERCIALIZACAO DIFERIDA" ItemStyle-HorizontalAlign ="right"  HeaderText="RESERVA DESPESA DE COMERCIALIZACAO DIFERIDA"/>
                <asp:BoundField DataField="COMISSAO DIFERIDA" ItemStyle-HorizontalAlign ="right"  HeaderText="COMISSAO DIFERIDA"/>
                <asp:BoundField DataField="CUSTO DE APOLICE" ItemStyle-HorizontalAlign ="right"  HeaderText="CUSTO DE APOLICE"/>
                <asp:BoundField DataField="ADICIONAL FRACIONAMENTO" ItemStyle-HorizontalAlign ="right"  HeaderText="ADICIONAL FRACIONAMENTO"/>
                 <asp:BoundField DataField="ADICIONAL FRACIONAMENTO RECEBIDO DO COSSEGURO ACEITO" ItemStyle-HorizontalAlign ="right"  HeaderText="ADICIONAL FRACIONAMENTO RECEBIDO DO COSSEGURO ACEITO"/>
                <asp:BoundField DataField="ADICIONAL FRACIONAMENTO PAGO EM COSSEGURO CEDIDO" ItemStyle-HorizontalAlign ="right"  HeaderText="ADICIONAL FRACIONAMENTO PAGO EM COSSEGURO CEDIDO"/>
                <asp:BoundField DataField="ADICIONAL FRACIONAMENTO PAGO AO RESSEGURADOR" ItemStyle-HorizontalAlign ="right"  HeaderText="ADICIONAL FRACIONAMENTO PAGO AO RESSEGURADOR"/>
                <asp:BoundField DataField="TOTAL ADICIONAL DE FRACIONAMENTO(AR + AS + AT + AU)" ItemStyle-HorizontalAlign ="right"  HeaderText="TOTAL ADICIONAL DE FRACIONAMENTO(AR + AS + AT + AU)"/>
                <asp:BoundField DataField="IOF" HeaderText="IOF"/>                
            </Columns>
        </asp:GridView>
				</div>
			</asp:Panel>
                <input id="hidHierarchySelected" type="hidden" name="hidHierarchySelected" runat="server"/>
			    <input id="hidPOLICYINFO" type="hidden" runat="server"/>
			    <input id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server"/> 
			    <input id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server"/>
			    <input id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server"/>  
			    <input id="hidPOLICY_NUM" type="hidden" name="hidPOLICY_NUM" runat="server"/>  
			    <input id="hidPolicy" type="hidden" runat="server" />
			    
		</form>
	</body>
</HTML>

