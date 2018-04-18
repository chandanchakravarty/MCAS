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
		
		int intTotalPremium ;
		public int CalculatePremium(int tt)
		{
			intTotalPremium = intTotalPremium + tt;
			return intTotalPremium;
		}
		
]]></msxsl:script>
	<xsl:template match="/">
		<html>
			<head>
				<xsl:variable name="Col">
					<xsl:value-of select="PRIMIUM/HEADER/@CSSNUM"></xsl:value-of>
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
				<!-- <xsl:variable name="myName" select="1"/> -->
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
					<xsl:call-template name="PREMIUMXML" />
					<br />
					<!-- Disclaimer -->
					<xsl:call-template name="DISCLAIMER" />
				</table>
			</body>
		</html>
		<!--Value of total premium of Home -->
		<span style="display:none">
			<returnHome>
				<xsl:value-of select="user:CalculatePremium(0)" />
			</returnHome>
		</span>
	</xsl:template>
	<!-- **************************** MAIN TEMPLATE  *******************************************************-->
	<xsl:template name="PREMIUMXML">
		<!-- Main Heading -->
		<tr class="headereffectCenter">
			<td colspan='6' style="FONT-SIZE: 14pt">
				<b>
					<xsl:value-of select="PRIMIUM/HEADER/@TITLE" />
				</b>
			</td>
		</tr>
		<!-- Dispaly Dates -->
		<xsl:call-template name="QDATE" />
		<!-- Sub Headings -->
		<xsl:call-template name="SUBHEADINGS" />
		<!-- Display Dwelling-->
		<xsl:apply-templates select="PRIMIUM" />
		<!-- Total Premium -->
		<xsl:call-template name="TOTALPREMIUM" />
		<!-- Print Button -->
		<xsl:call-template name="PRINTBTN" />
	</xsl:template>
	<!-- ************************** TEMPLATE FOR DATE  *****************************************************  -->
	<xsl:template name="QDATE">
		<tr bgcolor="ffffff">
			<td colspan='6' class="boldtxt" width="100%" align="left" valign="top">
				<table width='100%'>
					<td align="left" width="41%" class="boldtxt">Primary Applicant:<br />
			<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_NAME" />
			 <br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_ADDRESS1" />, 
			 <xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_ADDRESS2" />
			 <br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_CITY" />, 
			 <xsl:value-of select="' '" /> 
			  <xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_STATE" /> 
			 <xsl:value-of select="' '" />
			  <xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_ZIPCODE" />
			  <br />
			  <br />
			  <br />
			   <br />
			  <br />
			  <br />
			  </td>
					<td class="boldtxt" align="left" width="41%" valign="top">		
				
				Agency :<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_NAME" />		
				<br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_PHONE" />
				<xsl:choose>
							<xsl:when test="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_ADD2 !=''">
							<br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_ADD1" />,
						</xsl:when>
							<xsl:otherwise>
								<br />
								<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_ADD1" />
							</xsl:otherwise>
						</xsl:choose>
				<xsl:choose>
							<xsl:when test="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_ADD2 !=''">
								<br />
								<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_ADD2" />
							</xsl:when>
						</xsl:choose>
				<br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_CITY" /> 
				<BR /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_STATE" /> 
				<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_ZIP" />
				<xsl:choose>
							<xsl:when test="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_ADD2 =''">
								<br />
								<br />
								<xsl:value-of select="' '" />
							</xsl:when>
						</xsl:choose>	 				
				</td>
					<td class="boldtxt" width="38%" valign="top">
				Quoted On - <xsl:value-of select="PRIMIUM/HEADER/@QUOTE_DATE" />
			<br />Quote Effective -<xsl:value-of select="PRIMIUM/HEADER/@QUOTE_EFFECTIVE_DATE" />
			<br />Rates Effective - <xsl:value-of select="PRIMIUM/HEADER/@RATE_EFFECTIVE_DATE" />
					(<xsl:value-of select="PRIMIUM/HEADER/@BUSINESS_TYPE" />)
				<xsl:choose>
							<xsl:when test="PRIMIUM/CLIENT_TOP_INFO/@QQ_NUMBER != ''">
						<br />QQ-<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@QQ_NUMBER" /> 
					</xsl:when>
							<xsl:otherwise> <!--APP/POL Check-->
								<xsl:choose>
									<xsl:when test="PRIMIUM/CLIENT_TOP_INFO/@APP_NUMBER != ''">
							<br />APP #-<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@APP_NUMBER" />
							<br />Ver.-<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@APP_VERSION" />
							</xsl:when>
									<xsl:when test="PRIMIUM/CLIENT_TOP_INFO/@POL_NUMBER != ''">
							<br />POL #-<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@POL_NUMBER" />
							<br />Ver.-<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@POL_VERSION" />
							</xsl:when>
									<xsl:otherwise></xsl:otherwise>
								</xsl:choose>
							</xsl:otherwise>
						</xsl:choose>					
			</td>
				</table>
			</td>
		</tr>
		<tr class="headereffectCenter">
			<td colspan='4'>Personal Umbrella </td>
		</tr>
	</xsl:template>
	<!-- ************************** TEMPLATE FOR SUB HEADINGS *****************************************************  -->
	<xsl:template name="SUBHEADINGS">
		<tr class="headereffectCenter">
			<td align="left"> Description </td>
			<td>Deductible</td>
			<td>Limit</td>
			<td>Premium</td>
		</tr>
	</xsl:template>
	<!-- ************************** TEMPLATE FOR PRINT BUTTON *****************************************************  -->
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
		<tr></tr>
	</xsl:template>
	<!-- ************************** TEMPLATE FOR TOTAL PREMIUM  *****************************************************  -->
	<xsl:template name="TOTALPREMIUM">
		<tr class="midcolora">
			<td>
				<b>Total Premium</b>
			</td>
			<td></td>
			<td></td>
			<td class="midcolorr">
				<xsl:choose>
					<xsl:when test="PRIMIUM/RISK/STEP/@COMPONENT_CODE=normalize-space('SUMTOTAL')">
						<b>$<xsl:value-of select="PRIMIUM/RISK[@TYPE='MSE']/STEP[@TS='27']/@STEPPREMIUM" /></b>
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</td>
		</tr>
	</xsl:template>
	<!-- ******************************** TO DISPLAY PREMIUM ***************************************************************** -->
	<xsl:template match="PRIMIUM">
		<xsl:apply-templates select="RISK/STEP" />
	</xsl:template>
	<!-- ********************************* Call each step  ******************************************************** -->
	<xsl:template match="STEP">
		<xsl:choose>
			<xsl:when test="(@STEPPREMIUM) = 0"></xsl:when>
			<xsl:when test="(@STEPDESC)='Final Premium'"></xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="ADD_NEW_ROW" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ************************************ ADD NEW ROW FOR NEXT STEP ************************************************************** -->
	<xsl:template name="ADD_NEW_ROW">
		<!--xsl:if test="contains(@COMPONENT_CODE,'SUMTOTAL')">
			<xsl:variable name="total" select="@STEPPREMIUM" />
			<xsl:if test="user:CalculatePremium(string($total))" />
		</xsl:if-->
		<xsl:if test="@GROUPDESC != 'DESCRIPTIONNOTREQUIRED' and @GROUPDESC != 'Final Premium'">
			<tr class="midcolora">
				<td>
					<xsl:choose>
						<xsl:when test="contains(@GROUPDESC,'Final Premium')"></xsl:when>
						<xsl:otherwise>
							<b>
								<xsl:value-of select="@GROUPDESC" />
							</b>
						</xsl:otherwise>
					</xsl:choose>
					<xsl:choose>
						<xsl:when test="contains(@STEPDESC,'APOS')">
							<xsl:variable name="VAR_STEPDESC" select="@STEPDESC" />
							<xsl:variable name="VAR_STEPDESC_B">
								<xsl:value-of select="substring-before($VAR_STEPDESC,'APOS;')" />
							</xsl:variable>
							<xsl:variable name="VAR_STEPDESC_A">
								<xsl:value-of select="substring-after($VAR_STEPDESC,'APOS;')" />
							</xsl:variable>
							<xsl:variable name="apos" select='"&apos;"' />
							<!--<xsl:value-of select="$VAR_STEPDESC_B"/>-->
							<xsl:value-of select="concat($VAR_STEPDESC_B,$apos,$VAR_STEPDESC_A)" />
							<!--<xsl:value-of select="$VAR_STEPDESC_A"/>-->
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="@STEPDESC" />
						</xsl:otherwise>
					</xsl:choose>
				</td>
				<td class="midcolorr">
					<xsl:choose>
						<xsl:when test="normalize-space(@DEDUCTIBLEVALUE) !=''
									and normalize-space(@DEDUCTIBLEVALUE) !='0'
									and normalize-space(@DEDUCTIBLEVALUE) !='Unlimited' ">
									
								$<xsl:value-of select="normalize-space(@DEDUCTIBLEVALUE)" />
								
					</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="@DRDUCTIBLEVALUE" />
						</xsl:otherwise>
					</xsl:choose>
				</td>
				<td class="midcolorr">
					<xsl:choose>
						<xsl:when test="normalize-space(@LIMITVALUE) !='' 
									and normalize-space(@LIMITVALUE) !='0' 
									and normalize-space(@LIMITVALUE) !='Unlimited'">
									
						$<xsl:value-of select="normalize-space(@LIMITVALUE)" />
						
					</xsl:when>
						<xsl:otherwise>
							<xsl:choose>
								<xsl:when test="@LIMITVALUE &gt; 0 or normalize-space(@LIMITVALUE) ='Unlimited'">
									<xsl:value-of select="normalize-space(@LIMITVALUE)" />
								</xsl:when>
								<xsl:otherwise></xsl:otherwise>
							</xsl:choose>
						</xsl:otherwise>
					</xsl:choose>
				</td>
				<td class="midcolorr">
					<b>
						<xsl:choose>
							<xsl:when test="normalize-space(@STEPPREMIUM) ='' 
										or normalize-space(@STEPPREMIUM) ='0' 
										or normalize-space(@STEPPREMIUM) ='Applied' 
										or normalize-space(@STEPPREMIUM) ='Included'
										or normalize-space(@STEPPREMIUM) ='Excluded'">
								<xsl:value-of select="@STEPPREMIUM" />
							</xsl:when>
							<xsl:otherwise>$<xsl:value-of select="normalize-space(@STEPPREMIUM)" /></xsl:otherwise>
						</xsl:choose>
					</b>
				</td>
			</tr>
		</xsl:if>
	</xsl:template>
	<!-- ***************************************************************************************************  -->
	<xsl:template name="DISCLAIMER">
		<tr>
			<td valign="botton" colspan="4" class="boldtxt">
				This is a quotation only and does not bind any coverages. The coverages and premiums are subject to change without notice by the insurance company.   
			</td>
		</tr>
	</xsl:template>
</xsl:stylesheet>
