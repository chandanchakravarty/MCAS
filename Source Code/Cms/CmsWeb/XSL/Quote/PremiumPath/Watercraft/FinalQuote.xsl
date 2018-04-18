<?xml version="1.0" ?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
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
		<xsl:call-template name="SUBHEADINGS" />
		<!-- Display Dwelling -->
		<xsl:apply-templates match="PRIMIUM" />
		<!-- Total Premium -->
		<xsl:call-template name="TOTALPREMIUM" />
		<!-- Gross Premium -->
		<xsl:call-template name="GROSSPREMIUM" />
		<!-- Print Button -->
		<xsl:call-template name="PRINTBTN" />
	</xsl:template>
	<!-- ************************** TEMPLATE FOR DATE  *****************************************************  -->
	<xsl:template name="QDATE">
		<tr bgcolor="ffffff" valign="top">
			<td colspan='6' class="boldtxt" width="100%" align="left" valign="top">
				<table width='100%'>
					<td class="boldtxt" align="left" width="41%" valign="top">
						Primary Applicant:<br />
						<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_NAME" />
						<br />
						<xsl:choose>
							<xsl:when test="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_ADDRESS2=''">
								<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_ADDRESS1" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_ADDRESS1" />, 
							</xsl:otherwise>
						</xsl:choose>
						<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_ADDRESS2" />
						<br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_CITY" /> 
						<xsl:value-of select="' '" /> 
						<br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_STATE" /> 
						<xsl:value-of select="' '" />
						<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_ZIPCODE" />
					</td>
					<td class="boldtxt" align="left" width="41%" valign="top">
						Agency :<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_NAME" />		
						<br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_PHONE" />
						<br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_ADD1" /> 
						<xsl:choose>
							<xsl:when test="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_ADD2 !=''">
								<br />
								<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_ADD2" />
							</xsl:when>
						</xsl:choose>
						<br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_CITY" /> 
						<br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_STATE" /> 
						<br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_ZIP" /> 
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
								<br />
								<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@QQ_NUMBER" />
							</xsl:when>
							<xsl:otherwise>
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
			<!--<td colspan='1' class="boldtxt" align="left">Primary Applicant:<br />
			<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_NAME" />
			 <br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_ADDRESS1" />, 
			 <xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_ADDRESS2" />
			 <br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_CITY" />, 
			 <xsl:value-of select="' '" /> 
			  <br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_STATE" /> 
			 <xsl:value-of select="' '" />
			 <br /> <xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_ZIPCODE" />
			  </td>
			<td colspan='2' class="boldtxt" align="left">		
				<br />Agency :<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_NAME" />		
				<br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_PHONE" />
				<br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_ADD1" /> 
				<xsl:choose>
					<xsl:when test="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_ADD2 !=''">
						<br />
						<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_ADD2" />
					</xsl:when>
				</xsl:choose>
				<br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_CITY" /> 
				<br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_STATE" /> 
				<br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_ZIP" /> 
				<xsl:choose>
					<xsl:when test="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_ADD2 =''">
						<br />
						<br />
						<xsl:value-of select="' '" />
					</xsl:when>
				</xsl:choose>				
			</td>
			<td colspan='3' class="boldtxt">Quoted On - <xsl:value-of select="PRIMIUM/HEADER/@QUOTE_DATE" />
			<br />Quote Effective -<xsl:value-of select="PRIMIUM/HEADER/@QUOTE_EFFECTIVE_DATE" />
			<br />Rates Effective - <xsl:value-of select="PRIMIUM/HEADER/@RATE_EFFECTIVE_DATE" />
					(<xsl:value-of select="PRIMIUM/HEADER/@BUSINESS_TYPE" />)
				<xsl:choose>
					<xsl:when test="PRIMIUM/CLIENT_TOP_INFO/@QQ_NUMBER != ''">
						<br />
						<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@QQ_NUMBER" />
					</xsl:when>
					<xsl:otherwise> 
						<xsl:choose>
							<xsl:when test="PRIMIUM/CLIENT_TOP_INFO/@APP_NUMBER != ''">
							<br />Brics APP-<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@APP_NUMBER" />
							<br />Ver.-<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@APP_VERSION" />
							</xsl:when>
							<xsl:when test="PRIMIUM/CLIENT_TOP_INFO/@POL_NUMBER != ''">
							<br />Brics POL-<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@POL_NUMBER" />
							<br />Ver.-<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@POL_VERSION" />							
							</xsl:when>
							<xsl:otherwise></xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>					
			</td>-->
		</tr>
		<tr class="headereffectCenter">
			<td colspan='4'>Watercraft</td>
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
		<tr bgcolor="ffffff"></tr>
		<tr>
			<td colspan='2' class="boldtxt"></td>
			<td valign="bottom" colspan="2" align="right">
				<input type="button">
					<xsl:attribute name="id">btnPrint</xsl:attribute>
					<xsl:attribute name="value">Print</xsl:attribute>
					<xsl:attribute name="onClick">printReport();</xsl:attribute>
					<xsl:attribute name="vAlign">bottom</xsl:attribute>
					<xsl:attribute name="class">clsButton</xsl:attribute>
				</input>
			</td>
		</tr>
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
				<b>$<xsl:value-of select="user:CalculatePremium(0)" /></b>
			</td>
		</tr>
	</xsl:template>
	<!-- ************************** TEMPLATE FOR EFFECTIVE PREMIUM  *****************************************************  -->
	<xsl:template name="GROSSPREMIUM">
		<tr class="midcolora">
			<xsl:if test="PRIMIUM/EFFECTIVEPREMIUM !=''">
				<td>
					<b>Effective Premium</b>
				</td>
			</xsl:if>
			<td></td>
			<td></td>
			<td class="midcolorr">
				<b>
					<xsl:value-of select="PRIMIUM/EFFECTIVEPREMIUM" />
				</b>
			</td>
		</tr>
	</xsl:template>
	<!-- ********************************* TO DISPLAY PREMIUM ***************************************************************** -->
	<xsl:template match="PRIMIUM">
		<xsl:apply-templates select="OPERATOR/STEP" />
		<xsl:apply-templates select="VIOLATION/STEP" />
		<xsl:apply-templates select="RISK/STEP" />
		<xsl:apply-templates select="SCHEDULEDMISCSPORTS/STEP" />
		<xsl:apply-templates select="MINIM/STEP" />
		<!--<xsl:apply-templates select="RISK[@TYPE='MSE']/STEP" />-->
	</xsl:template>
	<!-- ********************************* Call each step  ******************************************************** -->
	<xsl:template match="STEP">
		<xsl:choose>
			<xsl:when test="(@STEPPREMIUM) = 0"></xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="ADD_NEW_ROW" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ************************************ ADD NEW ROW FOR NEXT STEP ************************************************************** -->
	<xsl:template name="ADD_NEW_ROW">
		<xsl:if test="contains(@COMPONENT_CODE,'SUMTOTAL')  or contains(@COMPONENT_CODE,'BOAT_UNATTACH_PREMIUM') or contains(@COMPONENT_CODE,'BOAT_UNATTACH_INCLUDE')or contains(@COMPONENT_CODE,'SUMTOTAL_S') ">
			<xsl:variable name="total" select="normalize-space(@STEPPREMIUM)" />
			<xsl:choose>
				<xsl:when test="contains($total ,'Included') or contains($total ,'Applied') "></xsl:when>
				<xsl:otherwise>
					<xsl:if test="user:CalculatePremium($total)" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:if>
		<xsl:if test="normalize-space(@GROUPDESC) != 'DESCRIPTIONNOTREQUIRED' and normalize-space(@GROUPDESC) != 'Final Premium' and normalize-space(@STEPDESC) != 'DESCRIPTIONNOTREQUIRED'">
			<tr class="midcolora">
				<td>
					<b>
						<xsl:value-of select="@GROUPDESC" />
					</b>
					<!--xsl:choose>
						<xsl:when test="contains(@GROUPDESC,'Scheduled Miscellaneous Sports Equipment')"></xsl:when>
						<xsl:otherwise>
							
						</xsl:otherwise>
					</xsl:choose-->
					<xsl:choose>
						<xsl:when test="normalize-space(@STEPDESC)='0'"></xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="@STEPDESC" />
						</xsl:otherwise>
					</xsl:choose>
				</td>
				<td class="midcolorr">
					<xsl:choose>
						<xsl:when test="contains(@DEDUCTIBLEVALUE,'%')">
							<xsl:value-of select="normalize-space(@DEDUCTIBLEVALUE)" />
						</xsl:when>
						<xsl:when test="normalize-space(@DEDUCTIBLEVALUE) !='' and normalize-space(@DEDUCTIBLEVALUE) !='0' and normalize-space(@DEDUCTIBLEVALUE) !='None' ">
						$<xsl:value-of select="format-number(normalize-space(@DEDUCTIBLEVALUE), '###,###')" />
					</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="normalize-space(@DEDUCTIBLEVALUE)" />
						</xsl:otherwise>
					</xsl:choose>
				</td>
				<td class="midcolorr">
					<xsl:choose>
						<xsl:when test="normalize-space(@LIMITVALUE) ='None'">
							<xsl:value-of select="normalize-space(@LIMITVALUE)" />
						</xsl:when>
						<xsl:when test="normalize-space(@LIMITVALUE)!='' and normalize-space(@LIMITVALUE) != 0 and normalize-space(@LIMITVALUE) !='No Coverage' and normalize-space(@LIMITVALUE) !='EFH'">
						$<xsl:value-of select="format-number(normalize-space(@LIMITVALUE), '###,###')" />
					</xsl:when>
						<xsl:otherwise>
							<xsl:choose>
								<xsl:when test="@LIMITVALUE &gt; 0">
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
							<xsl:when test="normalize-space(@STEPPREMIUM) ='' or normalize-space(@STEPPREMIUM) ='0' or normalize-space(@STEPPREMIUM) ='Included' or normalize-space(@STEPPREMIUM) ='Applied' or normalize-space(@STEPPREMIUM) ='Extended'">
								<xsl:value-of select="@STEPPREMIUM" />
							</xsl:when>
							<xsl:otherwise>$<xsl:value-of select="format-number(normalize-space(@STEPPREMIUM), '###,###')" /></xsl:otherwise>
						</xsl:choose>
					</b>
				</td>
			</tr>
		</xsl:if>
	</xsl:template>
	<xsl:template name="UNATTCHEDEQUIPMENTPREMIUM">
		<xsl:choose>
			<xsl:when test="contains(@COMPONENT_CODE,'BOAT_UNATTACH_INCLUDE')">
				<xsl:variable name="total">
					<xsl:value-of select="@STEPPREMIUM" />
				</xsl:variable>
				<xsl:if test="user:CalculatePremium(string($total))" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
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
