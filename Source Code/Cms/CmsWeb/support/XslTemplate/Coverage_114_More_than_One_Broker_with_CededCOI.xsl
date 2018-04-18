﻿<?xml version="1.0" encoding="UTF-8"?>
<!--Designed and generated by Altova StyleVision Enterprise Edition 2011 - see http://www.altova.com/stylevision for more information.-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  xmlns:msxsl="urn:schemas-microsoft-com:xslt"  xmlns:user="urn:user-namespace-here"    xmlns:clitype="clitype" xmlns:fn="http://www.w3.org/2005/xpath-functions" xmlns:iso4217="http://www.xbrl.org/2003/iso4217" xmlns:ix="http://www.xbrl.org/2008/inlineXBRL" xmlns:java="java" xmlns:link="http://www.xbrl.org/2003/linkbase" xmlns:xbrldi="http://xbrl.org/2006/xbrldi" xmlns:xbrli="http://www.xbrl.org/2003/instance" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xs="http://www.w3.org/2001/XMLSchema"    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:altova="http://www.altova.com" exclude-result-prefixes="clitype fn iso4217 ix java link xbrldi xbrli xlink xs xsi altova">
  <xsl:output version="4.0" method="html" indent="no" encoding="UTF-8" doctype-public="-//W3C//DTD HTML 4.01 Transitional//EN" doctype-system="http://www.w3.org/TR/html4/loose.dtd"/>
  <xsl:param name="SV_OutputFormat" select="'HTML'"/>
  <xsl:variable name="XML" select="/"/>
  <xsl:variable name="altova:nPxPerIn" select="96"/>
	<xsl:template name="br-replace">
		<xsl:param name="word"/>
		<!-- </xsl:text> on next line on purpose to get newline -->
		<xsl:variable name="cr" select="'~'">
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="contains($word,$cr)">
				<xsl:value-of select="substring-before($word,$cr)"/>
				<br/>
				<xsl:call-template name="br-replace">
					<xsl:with-param name="word"

							select="substring-after($word,$cr)"/>

				</xsl:call-template>

			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$word"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
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
    
]]>
  </msxsl:script>
  <xsl:template match="/">
    <html >
      <body>
        <xsl:for-each select="$XML">
          <table border="0" width="100%" cellpadding="0" cellspacing="1">
            <xsl:variable name="altova:CurrContextGrid_089A99B0" select="."/>
            <tbody>
              <xsl:for-each select="POLICY_DOCUMENTS">
                <xsl:for-each select="RISKINFO">
                  <xsl:for-each select="RISKS">
                    <xsl:value-of select="user:SetRiskID(RISK_ID)"/>
                    <tr style="height:0.20in; ">
                      <td>
                        <span style="font-family:Arial; font-size:12pt ">
                          <xsl:text>LOCAL DE RISCO</xsl:text>
                        </span>
                        <span>
                          <xsl:text>&#160;-&#160;</xsl:text>
                        </span>
                        <span style="font-family:Arial; font-size:12pt ">
                          <xsl:for-each select="LOCATION_NUMBER">
                            <xsl:apply-templates/>
                          </xsl:for-each>
                        </span>
                        <span style="font-family:Arial; font-size:12pt ">
                          <xsl:text>&#160;&#160; ITEM </xsl:text>
                        </span>
                        <span>
                          <xsl:text>&#160;-&#160;</xsl:text>
                        </span>
                        <span style="font-family:Arial; font-size:12pt ">
                          <xsl:for-each select="ITEM_NUMBER">
                            <xsl:apply-templates/>
                          </xsl:for-each>
                        </span>
                        <span>
                          <xsl:text>&#160;&#160;&#160; </xsl:text>
                        </span>
                      </td>
                    </tr>

                    <tr style="height:0.18in; ">
                      <td style="text-align:left; ">
                        <br/>

                        <span style="font-family:Arial; font-size:12pt ">
                          <xsl:for-each select="LOCATION">
                            <xsl:apply-templates/>
                          </xsl:for-each>
                        </span>
                        <span>
                          <xsl:text>&#160; </xsl:text>
                        </span>
                        <br/>

                        <br />
						  <xsl:if test ="PRE_CLASS_FIELD !='' and CURRENT_CLASS_FIELD != '' and string(PRE_CLASS_FIELD)!='0' and string(CURRENT_CLASS_FIELD) != '0'">
							  <span style="font-family:Arial; font-size:12pt ">
								  <xsl:text>ATIVIDADE: </xsl:text>
							  </span>
							  <span>
								  <xsl:text>&#160;</xsl:text>
							  </span>
							  <span style="font-family:Arial; font-size:12pt ">
								  <xsl:for-each select="CLASS_FIELD">
									  <xsl:apply-templates/>
								  </xsl:for-each>
							  </span>
						  </xsl:if >
                        <br />
                        <br />
                        <span style="font-family:Arial; font-size:12pt; ">
                          <xsl:text>VALOR EM RISCO:&#160;R$&#160;</xsl:text>
                        </span>
                        <span>
                          <xsl:text>&#160;</xsl:text>
                        </span>
                        <span style="font-family:Arial; font-size:12pt; ">
                          <xsl:for-each select="VALUE_AT_RISK">
                            <xsl:apply-templates/>
                          </xsl:for-each>
                        </span>
                        <span style="font-family:Arial; font-size:12pt; text-align:right">
                          <xsl:text>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;LIMITE MAXIMO DE RESPONSABILIDADE:&#160;R$&#160;</xsl:text>
                        </span>
                        <span>
                          <xsl:text>&#160;</xsl:text>
                        </span>
                        <span style="font-family:Arial; font-size:12pt; ">
                          <xsl:for-each select="MAXIMUM_LIMIT">
                            <xsl:apply-templates/>
                          </xsl:for-each>
                        </span>
                        <br/>
                        <span>
                          <xsl:text>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </xsl:text>
                        </span>
						  <table border="0" width="100%" cellpadding="0" cellspacing="0">
							  <xsl:variable name="altova:CurrContextGrid_08E2FBA0" select="."/>
							  <tbody>
								  <tr>
									  <td style="width:30%" align="left" nowrap="nowrap">
										  <span style="font-family:Arial;vertical-align:top; font-size:12pt;">
											  <xsl:text>COBERTURAS CONTRATADAS:&#160;&#160; </xsl:text>
										  </span>
									  </td>
									  <td style="width:8%">
										  <span style="font-family:Arial;vertical-align:top; font-size:12pt;">
											  <xsl:text>&#160;</xsl:text>
										  </span>
									  </td>
									  <td style="width:25%" align="right">
										  <span style="font-family:Arial;vertical-align:top; font-size:12pt;">
											  <xsl:text>IMP. SEGURADA-R$&#160;</xsl:text>
										  </span>
										  <span>
											  <xsl:text>:&#160;&#160;</xsl:text>
										  </span>
									  </td>
									  <td  style="width:6%" align="right">
										  <span style="font-family:Arial;vertical-align:top; font-size:12pt;">
											  <xsl:text>PsB</xsl:text>
										  </span>
										  <span>
											  <xsl:text>&#160;&#160;&#160;</xsl:text>
										  </span>
									  </td>
									  <td style="width:33%" align="left">
										  <span style="font-family:Arial;vertical-align:top; font-size:12pt;">
											  <xsl:text>FRANQUIAS:</xsl:text>
										  </span>
									  </td>
								  </tr>
							  </tbody>
						  </table>
						  <table border="0"  width="100%" cellpadding="0" cellspacing="0">
							  <xsl:variable name="altova:CurrContextGrid_08EFDD18" select="."/>
							  <tbody>
								  <xsl:for-each select="COVERAGES">
									  <xsl:for-each select="COVERAGE">
										  <tr>
											  <td style="width:30%;" align="left" valign ="top">
												  <span style="font-family:Arial;vertical-align:top; font-size:12pt;">
													  <xsl:for-each select="COV_DES">
														  <br></br>
														  <xsl:apply-templates/>
													  </xsl:for-each>
												  </span>
											  </td>
											  <td style="width:8%;" align="left" valign ="top">
												  <span style="font-family:Arial;vertical-align:top; font-size:12pt;">
													  <xsl:for-each select="INDEMNITY_PERIOD">
														  <br></br>
														  <xsl:apply-templates/>
													  </xsl:for-each>
												  </span>
											  </td>
											  <td style="width:25%;" align="right" valign ="top">
												  <span style="font-family:Arial;vertical-align:top; font-size:12pt;">
													  <xsl:for-each select="LIMIT_1">
														  <br></br>
														  <xsl:apply-templates/>
													  </xsl:for-each>
												  </span>
												  <span>
													  <xsl:text>&#160;&#160;</xsl:text>
												  </span>
											  </td>
											  <td style="width:6%;" align="right" valign ="top">
												  <span style="font-family:Arial;vertical-align:top;font-size:12pt">
													  <xsl:for-each select="PsB">
														  <br></br>
														  <xsl:apply-templates/>
													  </xsl:for-each>
												  </span>
												  <span>
													  <xsl:text>&#160;&#160;&#160;</xsl:text>
												  </span>
											  </td>
											  <td style="width:33%" valign ="top">
												  <span style="font-family:Arial;vertical-align:top; font-size:12pt;">
													  <xsl:choose>
														  <xsl:when test ="string(DEDUCTIBLE_1) != '0,00%' and string(DEDUCTIBLE_1) != '0.00%' and string(DEDUCTIBLE_1) != '0,00' and string(DEDUCTIBLE_1) != '0.00'">
															  <xsl:for-each select="DEDUCTIBLE_1">
																  <br></br>
																  <xsl:apply-templates/>
															  </xsl:for-each>
														  </xsl:when>
														  <xsl:otherwise>
															  <xsl:text>&#160;</xsl:text>
														  </xsl:otherwise>
													  </xsl:choose>
												  </span>
												  <xsl:text>&#160;</xsl:text>
												  <span style="font-family:Arial;vertical-align:top; font-size:12pt;" nowrap ="nowrap">

													  <xsl:choose>

														  <xsl:when test ="string(DEDUCTIBLE_1) != '0.00%' and string(DEDUCTIBLE_1) != '0,00%'">
															  <xsl:choose>

																  <xsl:when test ="string(DEDUCTIBLE_1) != '0.00%' and string(DEDUCTIBLE_1) != '0,00%'">
																	  <xsl:choose>

																		  <xsl:when test ="string(DEDUCTIBLE_1_Text) != '0.00%' and string(DEDUCTIBLE_1_Text) != '0,00%'">
																			  <xsl:for-each select="DEDUCTIBLE_1_Text">
																				  <xsl:apply-templates />
																			  </xsl:for-each>
																		  </xsl:when >
																	  </xsl:choose >
																  </xsl:when >
																  <xsl:otherwise>
																  </xsl:otherwise>
															  </xsl:choose >
														  </xsl:when >
														  <xsl:otherwise>
														  </xsl:otherwise>
													  </xsl:choose >
												  </span>
												  <xsl:text>&#160;</xsl:text>
												  <span style="font-family:Arial;vertical-align:top; font-size:12pt;">
													  <!--<xsl:choose>
                                        <xsl:when test ="string(DEDUCTIBLE_1) != '0,00%' and string(DEDUCTIBLE_1) != '0.00%'">-->
													  <xsl:for-each select="DEDUCTIBLE2_AMOUNT_TEXT">
														  <xsl:apply-templates/>
													  </xsl:for-each>
													  <!--</xsl:when>
                                        <xsl:otherwise>
                                          <xsl:text>&#160;</xsl:text>
                                        </xsl:otherwise>
                                      </xsl:choose>-->
												  </span>
											  </td>
										  </tr>
									  </xsl:for-each>
								  </xsl:for-each>
							  </tbody>
						  </table>
                        <br/>
                        <span style="font-family:Arial; font-size:12pt ">
                          <xsl:text>Obs: Outras Coberturas constantes das condições ou Manual, não foram contratadas pelo Segurado.</xsl:text>
                        </span>
                        <br/>
                        <br />
                        <span style="font-family:Arial; font-size:12pt">
                          <xsl:text>.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.&#160;.Prêmio: </xsl:text>

                        </span>

                        <span style="font-family:Arial; font-size:12pt ">
                          <xsl:for-each select="RISK_PREMIUM">
                            <xsl:apply-templates/>
                          </xsl:for-each>
                        </span>
                        <br />
                        <br />
									  <xsl:if test ="count($XML/POLICY_DOCUMENTS/DISCOUNTS/DISCOUNT[RISK_ID = user:GetRiskID()]) &gt; '0'">
										  <table border="0"  width="100%" cellpadding="0" cellspacing="0">
											  <tr>

												  <td>
													  <span style="font-family:Arial; font-size:12pt ">DESCONTOS</span>
												  </td>
												  <td></td>
												  <td></td>
												  <td></td>
											  </tr>
										  </table>
									  </xsl:if >

						  <table border="0"  width="100%" cellpadding="0" cellspacing="0">
                          <xsl:variable name="altova:CurrContextGrid_08EFDD18" select="."/>
                          <!--<xsl:variable name="var1">
									<xsl:value-of select="user:GetRiskID()" />
								</xsl:variable>-->
                          <tbody>
                            <xsl:for-each select="../../DISCOUNTS">
                              <xsl:for-each select="DISCOUNT">
                                <xsl:choose>
                                  <xsl:when test="user:GetRiskID() =  RISK_ID">

                                    <tr>
                                      <td   style="width:33%;vertical-align:top" align="left" >
                                        <span style="font-family:Arial; font-size:12pt">
                                          <xsl:for-each select="DISCOUNT_DESCRIPTION">
                                            <br></br>
                                            <xsl:apply-templates/>
                                          </xsl:for-each>
                                        </span>
                                      </td>
                                      <td style="width:22%;vertical-align:top" align="right" >
                                        <span style="font-family:Arial; font-size:12pt">
                                          <xsl:for-each select="PERCENTAGE">
                                            <br />
                                            <xsl:apply-templates/>
                                          </xsl:for-each>
                                        </span>
                                        <span>
                                          <xsl:text>&#160;&#160;</xsl:text>
                                        </span>
                                      </td>
                                      <td>
                                        &#160;&#160;
                                      </td>
                                      <td>
                                        &#160;&#160;
                                      </td>
                                    </tr>
                                  </xsl:when>
                                  <xsl:otherwise>
                                  </xsl:otherwise>
                                </xsl:choose >

                              </xsl:for-each>
                            </xsl:for-each>
                          </tbody>
                        </table>
                        <BR></BR>
                        <BR></BR>
						  <xsl:if test ="REMARKS != ''">
							  <span  id="Morethemonebroker"   style="font-family:Arial; font-size:12pt;width:100%;word-break:break-all;">
								  RELAÇÃO DE OBJETOS:
								  <BR></BR>
								  <BR></BR>
								  &#160;  <xsl:call-template name="br-replace">
									  <xsl:with-param name="word" select="REMARKS"/>
								  </xsl:call-template>
							  </span>
						  </xsl:if >
                      </td>
                    </tr>

                  </xsl:for-each>
                </xsl:for-each>
                <xsl:for-each select="$XML">
                  <table  id="multitable"  style="border-collapse:collapse; font-family:Arial Narrow; table-layout:auto;" border="0" width="100%">
                    <tr>
                      <td colspan="3" >
                        <span style="font-family:Arial; font-size:12pt ">
                          <xsl:text>NO PRESENTE SEGURO PARTICIPAM AS SEGUINTES CORRETORES:&#160; </xsl:text>
                        </span>
                      </td>
                    </tr>
                    <xsl:variable name="altova:CurrContextGrid_0548CC00" select="."/>
                    <tbody>
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
                        <td>
                          <span style="font-family:Arial; font-size:12pt ">
                            <xsl:text>Participação</xsl:text>
                          </span>
                        </td>
                        <td id="Commisio" align ="right">
                          <span style="font-family:Arial; font-size:12pt;">
                            <xsl:text>Comissão</xsl:text>
                          </span>
                        </td>
                      </tr>
                      <tr>
                        <td valign ="top" style="width:20%" >
                          <table style="font-family:Arial Narrow; font-size:12pt; " border="0" width="99%">
                            <xsl:variable name="altova:CurrContextGrid_087FA198" select="."/>
                            <tbody>
                              <xsl:for-each select="POLICY_DOCUMENTS">
                                <xsl:for-each select="MULTIBROKER">
                                  <xsl:for-each select="POLR">
                                    <xsl:for-each select="BROKER_CODE">
                                      <tr>
                                        <td>
                                          <xsl:apply-templates/>
                                        </td>
                                      </tr>
                                    </xsl:for-each>
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
                                    <xsl:for-each select="BANK_BRANCH">
                                      <tr>
                                        <td>
                                          <xsl:apply-templates/>
                                        </td>
                                      </tr>
                                    </xsl:for-each>
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
                                    <xsl:for-each select="BROKER_NAME">
                                      <tr>
                                        <td nowrap="nowrap">
                                          <xsl:apply-templates/>
                                        </td>
                                      </tr>
                                    </xsl:for-each>
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
                                    <xsl:for-each select="BROKER_COMMISION">
                                      <tr>
                                        <td>
                                          <xsl:apply-templates/>
                                        </td>
                                      </tr>
                                    </xsl:for-each>
                                  </xsl:for-each>
                                </xsl:for-each>
                              </xsl:for-each>
                            </tbody>
                          </table>
                        </td>
                        <td id="Value"  style="width:25%;text-align:right" valign ="top">
                          <table border="0" width="100%">
                            <xsl:variable name="altova:CurrContextGrid_08800900" select="."/>
                            <tbody>
                              <xsl:for-each select="POLICY_DOCUMENTS">
                                <xsl:for-each select="MULTIBROKER">
                                  <xsl:for-each select="POLR">
                                    <xsl:for-each select="COMMISSION_PERCENT">
                                      <tr>
                                        <td>
                                          <xsl:apply-templates/>
                                        </td>
                                      </tr>
                                    </xsl:for-each>
                                  </xsl:for-each>
                                </xsl:for-each>
                              </xsl:for-each>
                            </tbody>
                          </table>
                        </td>
                      </tr>
                    </tbody>
                  </table>
                </xsl:for-each>
                <br />

                <table  id="ceded_table" style="dis"   border="0" width="100%"  >
                  <tbody>
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
                        <span style="font-family:Arial ; font-size:12pt; ">
                          <xsl:text>Participacao</xsl:text>
                        </span>
                      </td>
                      <td id="CedededComissao"  style="width:20%;vertical-align:top;" align="right">
                        <span style="font-family:Arial ; font-size:12pt; ">
                          <xsl:text>Comissão</xsl:text>
                        </span>
                      </td>
                    </tr>
                    <tr>
                      <td valign ="top" style="width:20%" >
                        <table style="font-family:Arial Narrow; font-size:12pt; " border="0" width="100%">
                          <xsl:variable name="altova:CurrContextGrid_087FA198" select="."/>
                          <tbody>
                            <xsl:for-each select="COINSURANCE">
                              <xsl:for-each select="COMPANY">
                                <xsl:for-each select="SUSEP_NUM">
                                  <tr>
                                    <td>
                                      <xsl:apply-templates/>
                                    </td>
                                  </tr>
                                </xsl:for-each>
                              </xsl:for-each>
                            </xsl:for-each>
                          </tbody>
                        </table>
                      </td>
                      <td valign ="top" style="width:40%" nowrap ="nowrap">
                        <table style="font-family:Arial Narrow; font-size:12pt; " border="0" width="99%">
                          <xsl:variable name="altova:CurrContextGrid_087FA198" select="."/>
                          <tbody>
                            <xsl:for-each select="COINSURANCE">
                              <xsl:for-each select="COMPANY">
                                <xsl:for-each select="REIN_COMAPANY_NAME">
                                  <tr>
                                    <td nowrap ="nowrap">
                                      <xsl:apply-templates/>
                                    </td>
                                  </tr>
                                </xsl:for-each>
                              </xsl:for-each>
                            </xsl:for-each>
                          </tbody>
                        </table>
                      </td>
                      <td align="right"  valign ="top" style="width:20%;text-align:right" >
                        <table style="font-family:Arial Narrow; font-size:12pt; " border="0" width="99%">
                          <xsl:variable name="altova:CurrContextGrid_087FA198" select="."/>
                          <tbody>
                            <xsl:for-each select="COINSURANCE">
                              <xsl:for-each select="COMPANY">
                                <xsl:for-each select="COINSURANCE_PERCENT">
                                  <tr>
                                    <td align="right" >
                                      <xsl:apply-templates/>
                                    </td>
                                  </tr>
                                </xsl:for-each>
                              </xsl:for-each>
                            </xsl:for-each>
                          </tbody>
                        </table>
                      </td>

                      <td  id="feeCeded" style="width:20%;vertical-align:top" >
                        <table style="font-family:Arial Narrow; font-size:12pt; " border="0" width="99%">
                          <xsl:variable name="altova:CurrContextGrid_087FA198" select="."/>
                          <tbody>
                            <xsl:for-each select="COINSURANCE">
                              <xsl:for-each select="COMPANY">
                                <xsl:for-each select="COINSURANCE_FEE">
                                  <tr>
                                    <td align="right">
                                      <xsl:apply-templates/>
                                    </td>
                                  </tr>
                                </xsl:for-each>
                              </xsl:for-each>
                            </xsl:for-each>
                          </tbody>
                        </table>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </xsl:for-each>
            </tbody>
          </table>
        </xsl:for-each>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>