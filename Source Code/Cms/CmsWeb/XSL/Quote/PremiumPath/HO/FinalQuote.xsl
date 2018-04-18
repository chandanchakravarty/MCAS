<?xml version="1.0" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:user="urn:user-namespace-here">
	<msxsl:script language="c#" implements-prefix="user">
<![CDATA[ 
		
		int intCSS=1;
		public int ApplyColor(int colorvalue)
		{
			intCSS=colorvalue;			
			return intCSS;
		}
		
		int val=0;
		int intTotalPremium= 0; 
		int RiskPremium= 0; 
		public int  CalculatePremium(int tt)
		{
			
			if(tt < 0)
				{
					intTotalPremium=0;	
				}
			else
				{
					val = tt;
					intTotalPremium = (intTotalPremium) + (val);
			
				}
				return intTotalPremium;
		}				
]]></msxsl:script>
	<xsl:template match="/">
		<html>
			<head>
				<!-- <xsl:variable name="myName" select="PRIMIUM/DWELLINGDETAILS/HEADER/@CSSNUM"></xsl:variable>   -->
				<xsl:variable name="Col">
					<xsl:value-of select="PRIMIUM/RISK/HEADER/@CSSNUM"></xsl:value-of>
				</xsl:variable>
				<xsl:variable name="myName">
					<xsl:choose>
						<xsl:when test="$Col = '' or $Col=' ' or $Col &lt; 0 ">
							<xsl:value-of select="1" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$Col" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<!--  <xsl:variable name="myName" select="1"/>     -->
				<xsl:if test="user:ApplyColor($myName) = 0"></xsl:if>
				<xsl:choose>
					<xsl:when test="user:ApplyColor($myName) = 1">
						<LINK id="lk" href="/cms/cmsweb/css/css1.css" type="text/css" rel="stylesheet" />
					</xsl:when>
					<xsl:when test="user:ApplyColor($myName) = 2">
						<LINK href="/cms/cmsweb/css/css2.css" type="text/css" rel="stylesheet" />
					</xsl:when>
					<xsl:when test="user:ApplyColor($myName) = 3">
						<LINK href="/cms/cmsweb/css/css3.css" type="text/css" rel="stylesheet" />
					</xsl:when>
					<xsl:when test="user:ApplyColor($myName) = 4">
						<LINK href="/cms/cmsweb/css/css4.css" type="text/css" rel="stylesheet" />
					</xsl:when>
					<xsl:otherwise>
						<LINK href="/cms/cmsweb/css/css1.css" type="text/css" rel="stylesheet" />
					</xsl:otherwise>
				</xsl:choose>
				<title>Ebix Advantage Premiums</title>
			</head>
			<script language="javascript">
		<![CDATA[
			function CloseWindow()
			{
				window.close(true);
			}
			function printReport()
			{
				window.print();
			}
		]]>
	</script>
			<body bgcolor="ffffff">
				<table border="0" cellSpacing='1' cellPadding='1' align="center" width='100%'>
					<xsl:apply-templates select="PRIMIUM" />
					<br />
					<!-- Disclaimer -->
					<xsl:call-template name="DISCLAIMER" />
				</table>
			</body>
		</html>
		<!--Value of total premium of Home -->
		<span style="display:none">
			<returnHome>
				<!--xsl:value-of select="user:CalculatePremium(0)" /--></returnHome>
		</span>
	</xsl:template>
	<!-- Main Template -->
	<xsl:template match="PRIMIUM">
		<!-- Main Heading -->
		<tr class="headereffectCenter">
			<td colspan='6' style="FONT-SIZE: 14pt">
				<b>
					<xsl:value-of select="RISK/HEADER/@TITLE" />
				</b>
			</td>
		</tr>
		<!-- Dispaly Dates 		-->
		<xsl:call-template name="QDATE" />
		<!-- Sub Headings -->
		<xsl:call-template name="SUBHEADINGS" />
		<!-- Display Dwelling -->
		<xsl:apply-templates select="RISK" />
		<!-- Display RVs-->
		<!-- Total Premium -->
		<xsl:call-template name="TOTALPREMIUM" />
		<!-- Print Button -->
		<xsl:call-template name="PRINTBTN" />
	</xsl:template>
	<!-- To Display Dwelling -->
	<xsl:template match="RISK">
		<tr class="midcolora">
			<td>
				<b>
					PROPERTY: <xsl:value-of select="@ADDRESS" /> </b>
			</td>
			<td></td>
			<td></td>
			<td></td>
		</tr>
		<xsl:apply-templates select="STEP" />
	</xsl:template>
	<!-- Call each step-->
	<xsl:template match="STEP">
		<xsl:choose>
			<xsl:when test="(@STEPPREMIUM) = 0"></xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="addRow" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="addRow">
		<xsl:choose>
			<xsl:when test="@STEPPREMIUM !='' and @STEPPREMIUM !='0' and @STEPPREMIUM !='Included' and @STEPPREMIUM !='Applied' and @STEPPREMIUM !='Extended'  and @STEPPREMIUM !='SUMTOTAL'">
				<xsl:variable name="total" select="@STEPPREMIUM" />
				<xsl:if test="user:CalculatePremium(string($total))" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
		<!-- <tr bgcolor="ffdab9">   -->
		<tr class="midcolora">
			<td>
				<xsl:choose>
					<xsl:when test="contains(@GROUPDESC,'Final Premium')"></xsl:when>
					<xsl:when test="contains(@GROUPDESC,'NODESCRIPTION')">
						<Br />
					</xsl:when>
					<xsl:otherwise>
						<B>
							<xsl:value-of select="@GROUPDESC" />
						</B>
					</xsl:otherwise>
				</xsl:choose>
				<xsl:value-of select="@STEPDESC" />
			</td>
			<!-- <td style="FONT-WEIGHT: normal; FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"> -->
			<td align="right">
				<xsl:choose>
					<xsl:when test="normalize-space(@DEDUCTIBLEVALUE) ='$0' ">
						<xsl:value-of select="normalize-space(@DEDUCTIBLEVALUE)" />
					</xsl:when>
					<xsl:when test="contains(@DEDUCTIBLEVALUE,'%')">
						<xsl:value-of select="normalize-space(@DEDUCTIBLEVALUE)" />
					</xsl:when>
					<xsl:when test="normalize-space(@DEDUCTIBLEVALUE) !='' and normalize-space(@DEDUCTIBLEVALUE) !='0'">
							<!--$<xsl:value-of select="normalize-space(@DEDUCTIBLEVALUE)" />-->
						</xsl:when>
					<xsl:when test="normalize-space(@DEDUCTIBLEVALUE)='0'"></xsl:when>
					<xsl:otherwise>
						<!--<xsl:value-of select="normalize-space(@DEDUCTIBLEVALUE)" />-->
					</xsl:otherwise>
				</xsl:choose>
			</td>
			<td align="right">
				<xsl:choose>
					<xsl:when test="normalize-space(@LIMITVALUE)!='' and normalize-space(@LIMITVALUE) !='0'and normalize-space(@LIMITVALUE) !=0">
							<!--$<xsl:value-of select="normalize-space(@LIMITVALUE)" />-->
						</xsl:when>
					<xsl:otherwise>
						<xsl:choose>
							<xsl:when test="@LIMITVALUE &gt; 0">
								<!--<xsl:value-of select="normalize-space(@LIMITVALUE)" />-->
							</xsl:when>
							<xsl:when test="@LIMITVALUE ='0'"></xsl:when>
							<xsl:otherwise></xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</td>
			<!-- <td style="FONT-WEIGHT: bold; FONT-SIZE: 9pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: right"> -->
			<td align="right">
				<b>
					<xsl:choose>
						<xsl:when test="@STEPPREMIUM ='' or @STEPPREMIUM ='0' or @STEPPREMIUM ='Included' or @STEPPREMIUM ='Applied' or @STEPPREMIUM ='Extended'">
							<!--<xsl:value-of select="@STEPPREMIUM" />-->
						</xsl:when>
						<xsl:otherwise>
							<!--$<xsl:value-of select="@STEPPREMIUM" />-->
						</xsl:otherwise>
					</xsl:choose>
				</b>
			</td>
		</tr>
	</xsl:template>
	<!-- Display Dates -->
	<xsl:template name="QDATE">
		<tr bgcolor="ffffff" valign="top">
			<td colspan='6' class="boldtxt" width="100%" valign="top">
				<table width='100%'>
					<td class="boldtxt" align="left" width="41%" valign="top">
					Primary Applicant:<br />
					<xsl:value-of select="RISK/CLIENT_TOP_INFO/@PRIMARY_APP_NAME" />&#xa0;<xsl:value-of select="RISK/CLIENT_TOP_INFO/@PRIMARY_APP_LNAME" />
			<xsl:choose>
							<xsl:when test="RISK/CLIENT_TOP_INFO/@PRIMARY_APP_ADDRESS2=''">
								<br />
								<xsl:value-of select="RISK/CLIENT_TOP_INFO/@PRIMARY_APP_ADDRESS1" />
							</xsl:when>
							<xsl:otherwise>
			<br /><xsl:value-of select="RISK/CLIENT_TOP_INFO/@PRIMARY_APP_ADDRESS1" />,
			</xsl:otherwise>
						</xsl:choose>
			
			 <xsl:value-of select="RISK/CLIENT_TOP_INFO/@PRIMARY_APP_ADDRESS2" />
			 <br /><xsl:value-of select="RISK/CLIENT_TOP_INFO/@PRIMARY_APP_CITY" /> 
			 <xsl:value-of select="' '" /> 
			  <br /><xsl:value-of select="RISK/CLIENT_TOP_INFO/@PRIMARY_APP_STATE" /> 
			 <xsl:value-of select="' '" />
			  <xsl:value-of select="RISK/CLIENT_TOP_INFO/@PRIMARY_APP_ZIPCODE" />
			  </td>
					<td class="boldtxt" align="left" width="41%" valign="top">
			  Agency :<xsl:value-of select="RISK/CLIENT_TOP_INFO/@AGENCY_NAME" />		
				<br /><xsl:value-of select="RISK/CLIENT_TOP_INFO/@AGENCY_PHONE" />
				<br /><xsl:value-of select="RISK/CLIENT_TOP_INFO/@AGENCY_ADD1" />
				<xsl:choose>
							<xsl:when test="RISK/CLIENT_TOP_INFO/@AGENCY_ADD2!=''">
								<br />
								<xsl:value-of select="RISK/CLIENT_TOP_INFO/@AGENCY_ADD2" />
							</xsl:when>
							<xsl:otherwise></xsl:otherwise>
						</xsl:choose> 
				
				<br /><xsl:value-of select="RISK/CLIENT_TOP_INFO/@AGENCY_CITY" /> 
				<br /><xsl:value-of select="RISK/CLIENT_TOP_INFO/@AGENCY_STATE" /> 
					<br /><xsl:value-of select="RISK/CLIENT_TOP_INFO/@AGENCY_ZIP" />
					<xsl:choose>
							<xsl:when test="RISK/CLIENT_TOP_INFO/@AGENCY_ADD2 =''">
								<br />
								<br />
								<xsl:value-of select="' '" />
							</xsl:when>
						</xsl:choose>
			  </td>
					<td class="boldtxt" width="38%" valign="top">
			  Quoted On - <xsl:value-of select="RISK/HEADER/@QUOTE_DATE" />
			<br />Quote Effective -<xsl:value-of select="RISK/HEADER/@QUOTE_EFFECTIVE_DATE" />
			<br />Rates Effective - <xsl:value-of select="RISK/HEADER/@RATE_EFFECTIVE_DATE" />
					(<xsl:value-of select="RISK/HEADER/@BUSINESS_TYPE" />)
				<xsl:choose>
							<xsl:when test="RISK/CLIENT_TOP_INFO/@QQ_NUMBER != ''">
								<br />
								<xsl:value-of select="RISK/CLIENT_TOP_INFO/@QQ_NUMBER" />
							</xsl:when>
							<xsl:otherwise> <!--APP/POL Check-->
								<xsl:choose>
									<xsl:when test="RISK/CLIENT_TOP_INFO/@APP_NUMBER != ''">
							<br />APP #-<xsl:value-of select="RISK/CLIENT_TOP_INFO/@APP_NUMBER" />
							<br />Ver.-<xsl:value-of select="RISK/CLIENT_TOP_INFO/@APP_VERSION" />
							</xsl:when>
									<xsl:when test="RISK/CLIENT_TOP_INFO/@POL_NUMBER != ''">
							<br />POL #-<xsl:value-of select="RISK/CLIENT_TOP_INFO/@POL_NUMBER" />
							<br />Ver.-<xsl:value-of select="RISK/CLIENT_TOP_INFO/@POL_VERSION" />
							</xsl:when>
									<xsl:otherwise></xsl:otherwise>
								</xsl:choose>
							</xsl:otherwise>
						</xsl:choose>				
				</td>
					<!--td colspan='1' class="boldtxt">Primary Applicant:<br />
			<xsl:value-of select="RISK/CLIENT_TOP_INFO/@PRIMARY_APP_NAME" />&#xa0;
			<xsl:value-of select="RISK/CLIENT_TOP_INFO/@PRIMARY_APP_LNAME" />
			 <br /><xsl:value-of select="RISK/CLIENT_TOP_INFO/@PRIMARY_APP_ADDRESS1" />, 
			 <xsl:value-of select="RISK/CLIENT_TOP_INFO/@PRIMARY_APP_ADDRESS2" />
			 <br /><xsl:value-of select="RISK/CLIENT_TOP_INFO/@PRIMARY_APP_CITY" />, 
			 <xsl:value-of select="' '" /> 
			  <br /><xsl:value-of select="RISK/CLIENT_TOP_INFO/@PRIMARY_APP_STATE" /> 
			 <xsl:value-of select="' '" />
			  <xsl:value-of select="RISK/CLIENT_TOP_INFO/@PRIMARY_APP_ZIPCODE" /></td>
			<td colspan='2' class="boldtxt">		
				<br />Agency :<xsl:value-of select="RISK/CLIENT_TOP_INFO/@AGENCY_NAME" />		
				<br /><xsl:value-of select="RISK/CLIENT_TOP_INFO/@AGENCY_PHONE" />
				<br /><xsl:value-of select="RISK/CLIENT_TOP_INFO/@AGENCY_ADD1" />
				<xsl:choose>
					<xsl:when test="RISK/CLIENT_TOP_INFO/@AGENCY_ADD2!=''">
						<br />
						<xsl:value-of select="RISK/CLIENT_TOP_INFO/@AGENCY_ADD2" />
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose> 
				
				<br /><xsl:value-of select="RISK/CLIENT_TOP_INFO/@AGENCY_CITY" /> 
				<br /><xsl:value-of select="RISK/CLIENT_TOP_INFO/@AGENCY_STATE" /> 
					<br /><xsl:value-of select="RISK/CLIENT_TOP_INFO/@AGENCY_ZIP" />
					<xsl:choose>
					<xsl:when test="RISK/CLIENT_TOP_INFO/@AGENCY_ADD2 =''">
						<br />
						<br />
						<xsl:value-of select="' '" />
					</xsl:when>
				</xsl:choose	 				
			</td>
			<td colspan='1' class="boldtxt">Quoted On - <xsl:value-of select="RISK/HEADER/@QUOTE_DATE" />
			<br />Quote Effective -<xsl:value-of select="RISK/HEADER/@QUOTE_EFFECTIVE_DATE" />
			<br />Rates Effective - <xsl:value-of select="RISK/HEADER/@RATE_EFFECTIVE_DATE" />
					(<xsl:value-of select="RISK/HEADER/@BUSINESS_TYPE" />)
				<xsl:choose>
					<xsl:when test="RISK/CLIENT_TOP_INFO/@QQ_NUMBER != ''">
						<br />
						<xsl:value-of select="RISK/CLIENT_TOP_INFO/@QQ_NUMBER" />
					</xsl:when>
					<xsl:otherwise> 
						<xsl:choose>
							<xsl:when test="RISK/CLIENT_TOP_INFO/@APP_NUMBER != ''">
							<br />Brics APP-<xsl:value-of select="RISK/CLIENT_TOP_INFO/@APP_NUMBER" />
							<br />Ver.-<xsl:value-of select="RISK/CLIENT_TOP_INFO/@APP_VERSION" />
							</xsl:when>
							<xsl:when test="RISK/CLIENT_TOP_INFO/@POL_NUMBER != ''">
							<br />Brics POL-<xsl:value-of select="RISK/CLIENT_TOP_INFO/@POL_NUMBER" />
							<br />Ver.-<xsl:value-of select="RISK/CLIENT_TOP_INFO/@POL_VERSION" />
							</xsl:when>
							<xsl:otherwise></xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>					
			</td>-->
				</table>
			</td>
		</tr>
		<tr class="headereffectCenter">
			<td colspan='4'>Fire (<xsl:choose>
					<xsl:when test="RISK/STEP/@PRODUCTDESC='HO-4 DELUXE'">Tenants</xsl:when>
					<xsl:when test="RISK/STEP/@PRODUCTDESC='HO-2 REPLACE'">Replacement</xsl:when>
					<xsl:when test="RISK/STEP/@PRODUCTDESC='HO-2 REPLACE'">Replacement</xsl:when>
					<xsl:when test="RISK/STEP/@PRODUCTDESC='HO-6 DELUXE'">Unit Owners</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="RISK/STEP/@PRODUCTDESC" />
					</xsl:otherwise>
				</xsl:choose>)</td>
		</tr>
	</xsl:template>
	<!-- Sub Headings -->
	<xsl:template name="SUBHEADINGS">
		<tr class="headereffectCenter">
			<td align="left"> Description </td>
			<td>Deductible</td>
			<td>Limit</td>
			<td>Premium</td>
		</tr>
	</xsl:template>
	<!-- Print Button -->
	<xsl:template name="PRINTBTN">
		<tr>
			<td valign="bottom" colspan="4" align="right">
				<input type="button">
					<xsl:attribute name="id">btnPrint</xsl:attribute>
					<xsl:attribute name="value">Print</xsl:attribute>
					<xsl:attribute name="onClick">printReport();</xsl:attribute>
					<xsl:attribute name="vAlign">bottom</xsl:attribute>
					<xsl:attribute name="class">clsButton</xsl:attribute>
				</input>
			</td>
		</tr>
		<!--tr class="boldtxt">Agency Phone :<xsl:value-of select="RISK/CLIENT_TOP_INFO/@AGENCY_PHONE" > </tr-->
	</xsl:template>
	<!-- Total Premium -->
	<xsl:template name="TOTALPREMIUM">
		<tr class="midcolora">
			<td>
				<b>Total Premium</b>
			</td>
			<td></td>
			<td></td>
			<td class="midcolorr">
				<b>$<xsl:value-of select="GRANDTOTAL" /></b>
			</td>
		</tr>
	</xsl:template>
	<xsl:template name="DISCLAIMER">
		<tr>
			<td valign="botton" colspan="4" class="boldtxt">
				This is a quotation only and does not bind any coverages. The coverages and premiums are subject to change without notice by the insurance company.   
			</td>
		</tr>
	</xsl:template>
</xsl:stylesheet>
