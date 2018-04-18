<?xml version="1.0" encoding="UTF-8"?>
<!--Designed and generated by Altova StyleVision Enterprise Edition 2011 rel. 3 - see http://www.altova.com/stylevision for more information.-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  xmlns:msxsl="urn:schemas-microsoft-com:xslt"  xmlns:user="urn:user-namespace-here"    xmlns:clitype="clitype" xmlns:fn="http://www.w3.org/2005/xpath-functions" xmlns:iso4217="http://www.xbrl.org/2003/iso4217" xmlns:ix="http://www.xbrl.org/2008/inlineXBRL" xmlns:java="java" xmlns:link="http://www.xbrl.org/2003/linkbase" xmlns:xbrldi="http://xbrl.org/2006/xbrldi" xmlns:xbrli="http://www.xbrl.org/2003/instance" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xs="http://www.w3.org/2001/XMLSchema"    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:altova="http://www.altova.com" exclude-result-prefixes="clitype fn iso4217 ix java link xbrldi xbrli xlink xs xsi altova">
	<xsl:output version="4.0" method="html" indent="no" encoding="UTF-8" doctype-public="-//W3C//DTD HTML 4.01 Transitional//EN" doctype-system="http://www.w3.org/TR/html4/loose.dtd"/>
	<xsl:param name="SV_OutputFormat" select="'HTML'"/>
	<xsl:variable name="XML" select="/"/>
	<xsl:variable name="altova:nPxPerIn" select="96"/>
	<msxsl:script language="c#" implements-prefix="user">
		<![CDATA[ 
		
		int intCSS=1;
		public int ApplyColor(int colorvalue)
		{
			intCSS=colorvalue;			
			return intCSS;
		}
		string Add_Location ="";
		public void ConcatLocation(double strLoc)
		{
		 Add_Location	= Add_Location + "/" +strLoc;
     Add_Location	=Add_Location.Substring(1,Add_Location.Length-1);	
			//return Add_Location;
		}
    
    	public string Getlocation()
		{
	
			return Add_Location;
		}
    
		int intTotalamp=0;
		public int Calculateamp(int tt)
		{
			intTotalamp = intTotalamp + tt;
			return intTotalamp;
		}
    
    string  tempdesc="";
		public string RetunDescription(string desc)
    {
      if(tempdesc!=desc)
      { 
         tempdesc= desc;
         return desc;
      }else
      {
          return "";
      }
    }	
    string strPolicycurrency;
    int RiskID=0;
	public void SetRiskID(int flag)
		{
			RiskID=flag;
		
		}
	
	public int GetRiskID()
		{

			return RiskID;
		}
		int IsPrint=0;
	public void SetIsPrint(int flag)
		{
			IsPrint += flag;
		
		}
	
	public int GetIsPrint()
		{

			return IsPrint;
		}
		
	int IsPrintD=0;
	public void SetIsPrintD(int flag)
		{
			IsPrintD += flag;
		
		}
	
	public int GetIsPrintD()
		{

			return IsPrintD;
		}
    
    
]]>
	</msxsl:script>
	<xsl:template match="/">
		<!--Below template  is applied for COI and Multi Broker Session-->
		<!--Did changes to remove double border, alignment issues, itrack 1298-->
		<html>
			<body>
				<xsl:for-each select="$XML">
					<table   width="100%" cellpadding="0" cellspacing="0">
						<xsl:variable name="altova:CurrContextGrid_06879240" select="."/>
						<tbody>
							<tr >
								<td>
									<span style="font-family:Arial; font-size:12pt; ">
										&#160;DECLARA-SE PARA OS DEVIDOS FINS E EFEITOS QUE, DE CONFORMIDADE COM AS  CONDIÇÕES E
										<br></br>
										&#160;CLÁUSULÁS DA APÓLICE EM REFÊNCIA, E DE POSSE DA RELAÇÃO DE AVERBAÇÃO DE
										<br></br>
										&#160;SEGURADOS FORNECIDA PELO ESTIPULANTE, COBRA-SE O PRÊMIO CORRESPONDENTE AO
										&#160;MOVIMENTO VERIFICADO NO PERÍODO ACIMA:
									</span>
									<br/>
								</td>
							</tr>
						</tbody>
					</table>
					<br></br>

					<table border="1" width="100%" cellpadding="0" cellspacing="0"  style="border-style: solid; border-width:0.00px">

						<tbody>
							<tr>
								<td style="text-align:center;" colspan="4">
									<span style="font-family:Arial; ">
										<xsl:text>APC – ACIDENTES PESSOAIS COLETIVOS</xsl:text>
									</span>
								</td>
							</tr>
							<tr>
								<td width="20%" align="center">
									<span style="font-family:Arial;">
										<xsl:text>Quantidade de&#160; Certificados</xsl:text>
									</span>
								</td>
								<td width="40%" align="center">
									<span style="font-family:Arial; ">
										<xsl:text>Cobertura</xsl:text>
									</span>
								</td>
								<td width="20%" align="center">
									<span style="font-family:Arial; ">
										<xsl:text>Capital Segurado Total</xsl:text>
									</span>
								</td>
								<td width="20%" align="center">
									<span style="font-family:Arial;">
										<xsl:text>Prêmio Total</xsl:text>
									</span>
								</td>
							</tr>

							<xsl:variable name="altova:CurrContextGrid_068E9210" select="."/>

							<xsl:for-each select="POLICY_DOCUMENTS">
								<xsl:for-each select="RISKINFO">
									<xsl:for-each select="RISKS">
										<xsl:value-of select="user:SetRiskID(RISK_ID)"/>

										<tr>
											<td width="20%" align="center">
												<span style="font-family:Arial; font-size:12pt; ">
													<xsl:for-each select="NUMBER_OF_PASSENGERS">
														<span style="font-family:Arial; font-size:12pt; ">
															<xsl:apply-templates/>
														</span>
													</xsl:for-each>
												</span>
											</td>
											<td width="40%">
												<table border="0"    width="100%"  cellpadding="0" cellspacing="0"   style="border-style: solid; border-width:0.00px">

													<tbody>
														<span style="font-family:Arial; font-size:12pt; ">
															<xsl:for-each select="COVERAGE">
																<xsl:for-each select="COV_DES">
																	<tr>
																		<td >
																			<span style="font-family:Arial; font-size:12pt; ">
																				<xsl:apply-templates/>
																			</span>
																		</td>
																	</tr>
																</xsl:for-each>
															</xsl:for-each>
														</span>
													</tbody>
												</table>
											</td>
											<td width="20%">
												<table  border="0"   width="100%" cellpadding="0" cellspacing="0"  style="border-style: solid; border-width:0.00px"  >

													<tbody>
														<span style="font-family:Arial; font-size:12pt; ">
															<xsl:for-each select="COVERAGE">
																<xsl:for-each select="LIMIT_1">
																	<tr>
																		<td width="20%" align="right">
																			<span style="font-family:Arial; font-size:12pt; ">
																				<xsl:apply-templates/>
																			</span>
																		</td>
																	</tr>
																</xsl:for-each>
															</xsl:for-each>
														</span>
													</tbody>
												</table>
											</td>
											<td width="20%">
												<table  border="0"   cellpadding="0" cellspacing="0" width="100%"  style="border-style: solid; border-width:0.00px" >
													<xsl:variable name="altova:CurrContextGrid_06BDD768" select="."/>
													<tbody>
														<span style="font-family:Arial; font-size:12pt; ">
															<xsl:for-each select="COVERAGE">
																<xsl:for-each select="COVERAGE_PREMIUM">
																	<tr>
																		<td width="20%" align="right">
																			<span style="font-family:Arial; font-size:12pt; ">
																				<xsl:apply-templates/>
																			</span>
																		</td>
																	</tr>
																</xsl:for-each>
															</xsl:for-each>
														</span>
													</tbody>
												</table>
											</td>
										</tr>
										<tr>
											<td colspan="3" align="center">
												<span  style="font-family:Arial;font-size=12pt">
													<xsl:text>&#160;&#160; Total</xsl:text>
												</span>
											</td>
											<td align="right">

												<span style="font-family:Arial; font-size:12pt ">
													<xsl:for-each select="../../SUMOFRISKS">
														<xsl:for-each select="POL_PRODUCT_COVERAGES">
															<xsl:choose>
																<xsl:when test="user:GetRiskID() =  RISK_ID">

																	<span  style="font-family:Arial; font-size:12pt">
																		<xsl:for-each select="total_Premium_of_risk">
																			<span style="font-family:Arial; font-size:12pt; ">
																				<xsl:apply-templates/>
																			</span>
																		</xsl:for-each>
																	</span>
																</xsl:when>
																<xsl:otherwise>
																</xsl:otherwise>
															</xsl:choose >
														</xsl:for-each>
													</xsl:for-each>
												</span>
											</td>

										</tr>

									</xsl:for-each>
								</xsl:for-each>
							</xsl:for-each>

						</tbody>
					</table>
					<br/>
					<table   width="100%" cellpadding="0" cellspacing="0">
						<xsl:variable name="altova:CurrContextGrid_068E7C50" select="."/>
						<tbody>
							<tr>
								<td>
									<!--remove the sign  from A ,itrack 1298 , modified by Naveen-->
									<span  style="font-family:Arial;font-size:12pt">
										<xsl:text>PERMANCEM INALTERADOS OS DEMAIS TERMOS, CLÁUSULAS E CONDIÇÕES DA APÓLICE.</xsl:text>
									</span>
								</td>
							</tr>
						</tbody>
					</table>

				</xsl:for-each>
			</body>
		</html>
		<br></br>
		<html>
			<body>
				<table  id="multitable"  style="border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;display:inline" border="0" width="100%">
					<tbody>
						<xsl:for-each select="POLICY_DOCUMENTS">
							<xsl:for-each select="MULTIBROKER">
								<xsl:for-each select="POLR">
									<xsl:value-of select="user:SetIsPrint(IS_PRINT)"/>
								</xsl:for-each>
							</xsl:for-each>
						</xsl:for-each>
					</tbody>
					<xsl:choose>
						<xsl:when test="user:GetIsPrint() &gt;  '0'">
							<tr>
								<td colspan="3" >
									<span style="font-family:Arial; font-size:12pt ">
										<xsl:text>NO PRESENTE SEGURO PARTICIPAM OS SEGUINTES CORRETORES:&#160; </xsl:text>
									</span>
								</td>
							</tr>
						</xsl:when >
						<xsl:otherwise>
							<xsl:text>&#160;&#160;&#160;</xsl:text>
						</xsl:otherwise>
					</xsl:choose>

					<xsl:variable name="altova:CurrContextGrid_0548CC00" select="."/>
					<tbody>
						<xsl:choose>
							<xsl:when test="user:GetIsPrint() &gt;  '0'">
								<tr>
									<td>
										<span style="font-family:Arial; font-size:12pt ">
											<xsl:text>Código </xsl:text>
										</span>
									</td>
									<td>
										<span style="font-family:Arial; font-size:12pt ">
											<xsl:text>Filial </xsl:text>
										</span>
									</td>
									<td>
										<span style="font-family:Arial; font-size:12pt ">
											<xsl:text>Nome </xsl:text>
										</span>
									</td>
									<td align ="right">
										<span style="font-family:Arial; font-size:12pt ">
											<xsl:text>Participação</xsl:text>
										</span>
									</td>
									<td id="Commisio" align ="right">
										<span style="font-family:Arial; font-size:12pt ">
											<xsl:text>Comissão</xsl:text>
										</span>
									</td>
								</tr>
							</xsl:when >
							<xsl:otherwise>
								<xsl:text>&#160;&#160;&#160;</xsl:text>
							</xsl:otherwise>
						</xsl:choose >
						<tr>
							<td valign ="top" style="width:20%" >
								<table style="font-family:Arial Narrow; font-size:12pt; " border="0" width="99%">
									<xsl:variable name="altova:CurrContextGrid_087FA198" select="."/>
									<tbody>
										<xsl:for-each select="POLICY_DOCUMENTS">
											<xsl:for-each select="MULTIBROKER">
												<xsl:for-each select="POLR">
													<xsl:choose>
														<xsl:when test ="IS_PRINT = '1'">
															<xsl:for-each select="BROKER_CODE">
																<tr>
																	<td>
																		<xsl:apply-templates/>
																	</td>
																</tr>
															</xsl:for-each>
														</xsl:when>
														<xsl:otherwise>
															<xsl:text>&#160;&#160;&#160;</xsl:text>
														</xsl:otherwise>
													</xsl:choose>
												</xsl:for-each>
											</xsl:for-each>
										</xsl:for-each>
									</tbody>
								</table>
							</td>
							<td  style="width:20%"    valign ="top">
								<table style="border-collapse:collapse; font-family:Arial Narrow; font-size:12pt; " border="0" width="100%">
									<xsl:variable name="altova:CurrContextGrid_0876B548" select="."/>
									<tbody>
										<xsl:for-each select="POLICY_DOCUMENTS">
											<xsl:for-each select="MULTIBROKER">
												<xsl:for-each select="POLR">
													<xsl:choose>
														<xsl:when test ="IS_PRINT = '1'">
															<xsl:for-each select="BANK_BRANCH">
																<tr>
																	<td>
																		<xsl:apply-templates/>
																	</td>
																</tr>
															</xsl:for-each>
														</xsl:when>
														<xsl:otherwise>
															<xsl:text>&#160;&#160;&#160;</xsl:text>
														</xsl:otherwise>
													</xsl:choose >
												</xsl:for-each>
											</xsl:for-each>
										</xsl:for-each>
									</tbody>
								</table>
							</td>
							<td style="width:40%"  valign ="top" nowrap="nowrap">
								<table border="0" width="100%">
									<xsl:variable name="altova:CurrContextGrid_08800900" select="."/>
									<tbody>
										<xsl:for-each select="POLICY_DOCUMENTS">
											<xsl:for-each select="MULTIBROKER">
												<xsl:for-each select="POLR">
													<xsl:choose>
														<xsl:when test ="IS_PRINT = '1'">
															<xsl:for-each select="BROKER_NAME">
																<tr>
																	<td nowrap="nowrap">
																		<xsl:apply-templates/>
																	</td>
																</tr>
															</xsl:for-each>
														</xsl:when >
														<xsl:otherwise>
															<xsl:text>&#160;&#160;&#160;</xsl:text>
														</xsl:otherwise>
													</xsl:choose >
												</xsl:for-each>
											</xsl:for-each>
										</xsl:for-each>
									</tbody>
								</table>
							</td>
							<td  style="width:12%"  valign ="top">
								<table border="0" width="100%">
									<xsl:variable name="altova:CurrContextGrid_08800900" select="."/>
									<tbody>
										<xsl:for-each select="POLICY_DOCUMENTS">
											<xsl:for-each select="MULTIBROKER">
												<xsl:for-each select="POLR">
													<xsl:choose>
														<xsl:when test ="IS_PRINT = '1'">
															<xsl:for-each select="BROKER_COMMISION">
																<tr>
																	<td style="text-align:right;">
																		<xsl:apply-templates/>
																	</td>
																</tr>
															</xsl:for-each>
														</xsl:when >
														<xsl:otherwise>
															<xsl:text>&#160;&#160;&#160;</xsl:text>
														</xsl:otherwise>
													</xsl:choose >
												</xsl:for-each>
											</xsl:for-each>
										</xsl:for-each>
									</tbody>
								</table>
							</td>
							<td id="Value"  style="width:25%" valign ="top">
								<table border="0" width="100%">
									<xsl:variable name="altova:CurrContextGrid_08800900" select="."/>
									<tbody>
										<xsl:for-each select="POLICY_DOCUMENTS">
											<xsl:for-each select="MULTIBROKER">
												<xsl:for-each select="POLR">
													<xsl:choose>
														<xsl:when test ="IS_PRINT = '1'">
															<xsl:for-each select="COMMISSION_PERCENT">
																<tr>
																	<td style="text-align:right;">
																		<xsl:apply-templates/>
																	</td>
																</tr>
															</xsl:for-each>
														</xsl:when >
														<xsl:otherwise>
															<xsl:text>&#160;&#160;&#160;</xsl:text>
														</xsl:otherwise>
													</xsl:choose >
												</xsl:for-each>
											</xsl:for-each>
										</xsl:for-each>
									</tbody>
								</table>
							</td>
						</tr>
					</tbody>
				</table>
				<br />
				<xsl:if test ="count($XML/POLICY_DOCUMENTS/COINSURANCE/COMPANY[IS_PRINT = '1']) &gt; '0'">
					<table  id="ceded_table" style="display:inline"   border="0" width="100%"  >
						<tbody>
							<tr>
								<!--<xsl:for-each select="POLICY_DOCUMENTS">
                <xsl:for-each select="COINSURANCE">
                  <xsl:for-each select="COMPANY">-->
								<!--<xsl:choose>
                      <xsl:when test ="IS_PRINT = '1'">-->
								<tr>
									<td colspan="4"  style="font-family:Arial;font-size:12pt">
										<span  style="font-family:Arial ;font-size:12pt"  >
											NO PRESENTE SEGURO PARTICIPAM AS SEGUINTES COMPANHIAS:
										</span>
									</td>
								</tr>
								<tr>
									<td style="width:20%;vertical-align:top" align="left">
										<span style="font-family:Arial ; font-size:12pt; ">
											<xsl:text>Cia</xsl:text>
										</span>
									</td>
									<td  style="width:40%;vertical-align:top" align="left">
										<span style="font-family:Arial ; font-size:12pt; ">
											<xsl:text>Nome</xsl:text>
										</span>
									</td>
									<td  style="width:20%;vertical-align:top" align="right" >
										<span style="font-family:Arial; font-size:12pt; ">
											<xsl:text>Participacao</xsl:text>
										</span>
									</td>
									<td id="CedededComissao"  style="width:20%;vertical-align:top;" align="right">
										<span style="font-family:Arial ; font-size:12pt; ">
											<xsl:text>Comissão</xsl:text>
										</span>
									</td>
								</tr>
								<!--</xsl:when>
                    </xsl:choose>-->
								<!--</xsl:for-each>
                </xsl:for-each>
              </xsl:for-each >-->
							</tr>
							<tr>
								<xsl:for-each select="POLICY_DOCUMENTS">
									<xsl:for-each select="COINSURANCE">
										<xsl:for-each select="COMPANY">
											<!--<xsl:choose>
                      <xsl:when test ="IS_PRINT = '1'">-->
											<tr>
												<td valign ="top" style="width:20%;vertical-align:top;font-family:Arial Narrow;" >
													<xsl:value-of select ="SUSEP_NUM"/>
												</td>
												<td valign ="top" style="width:40%;vertical-align:top;font-family:Arial Narrow;" nowrap ="nowrap">
													<xsl:value-of select ="REIN_COMAPANY_NAME"/>
												</td>
												<td valign ="top" style="width:20%;vertical-align:top;font-family:Arial Narrow;" align="right" >
													<xsl:value-of select ="COINSURANCE_PERCENT"/>
												</td>
												<td  align="right" id="feeCeded" style="width:20%;vertical-align:top;font-family:Arial Narrow;" >
													<xsl:value-of select ="COINSURANCE_FEE"/>
												</td>
											</tr>
											<!--</xsl:when >
                    </xsl:choose >-->
										</xsl:for-each>
									</xsl:for-each>
								</xsl:for-each >

							</tr>
						</tbody>
					</table>
				</xsl:if >
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
