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
  <xsl:variable name="MyDoc_Path" select="PRIMIUM/MESSAGE_FILE_PATH"></xsl:variable>
  <xsl:variable name="MyDoc" select="document($MyDoc_Path)"></xsl:variable>
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
						<LINK id="lk" href="/cms/cmsweb/css/css1.css" type="text/css" rel="stylesheet"></LINK>
					</xsl:when>
					<xsl:when test="user:ApplyColor($myName) = 2">
						<LINK href="/cms/cmsweb/css/css2.css" type="text/css" rel="stylesheet"></LINK>
					</xsl:when>
					<xsl:when test="user:ApplyColor($myName) = 3">
						<LINK href="/cms/cmsweb/css/css3.css" type="text/css" rel="stylesheet"></LINK>
					</xsl:when>
					<xsl:when test="user:ApplyColor($myName) = 4">
						<LINK href="/cms/cmsweb/css/css4.css" type="text/css" rel="stylesheet"></LINK>
					</xsl:when>
					<xsl:otherwise>
						<LINK href="/cms/cmsweb/css/css1.css" type="text/css" rel="stylesheet"></LINK>
					</xsl:otherwise>
				</xsl:choose>
				<title>Ebix Advantage</title>
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
		<!-- Dispaly Primary Client Info -->
		<!--xsl:call-template name="PRIMARY_APPLICANT_INFO" / -->
		<!-- Sub Headings -->
		<xsl:call-template name="SUBHEADINGS" />
		<!-- Display Dwelling -->
		<xsl:apply-templates match="PRIMIUM" />
		<!-- Total Premium -->
		<!--xsl:call-template name="TOTALPREMIUM" / -->
		<!-- Gross Premium -->
		<xsl:call-template name="GROSSPREMIUM" />
		<!-- Print Button -->
		<xsl:call-template name="PRINTBTN" />
	</xsl:template>
	<!--*********TEMPLATE FOR PRIMARY***********-->
	<xsl:template name="PRIMARY_APPLICANT_INFO"> <!--Not used-->
		<tr bgcolor="ffffff">
			<td colspan='4' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: LEFT">Primary Applicant: <xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_NAME" /> </td>
		</tr>
		<tr bgcolor="ffffff">
			<td colspan='4' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: LEFT">Address1 :<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_ADDRESS1" /> </td>
		</tr>
		<tr bgcolor="ffffff">
			<td colspan='4' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: LEFT">Address2 :<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_ADDRESS2" /> </td>
		</tr>
		<tr bgcolor="ffffff">
			<td colspan='4' style="FONT-SIZE: 8pt;vertical-align:TOP; COLOR: #000000; FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; TEXT-ALIGN: LEFT">ZIP Code:<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_ZIPCODE" /> </td>
		</tr>
	</xsl:template>
	<!--****END*****TEMPLATE FOR PRIMARY***********-->
	<!-- ************************** TEMPLATE FOR DATE  *****************************************************  -->
	<xsl:template name="QDATE">
		<tr bgcolor="ffffff" valign="top">
			<td colspan='6' class="boldtxt" width="100%" align="left" valign="top">
				<table width='100%'>
					<td class="boldtxt" align="left" width="41%" valign="top">
            <xsl:value-of select="$MyDoc//message[@messageid='210']" />:<br />
						<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_NAME" />&#xa0;<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_LNAME" />
						<xsl:choose>
							<xsl:when test="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_ADDRESS2 !=''">
						<br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_ADDRESS1" />,
						</xsl:when>
						<xsl:otherwise>
								<br />
								<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_ADDRESS1" />
							</xsl:otherwise>
						</xsl:choose>
						<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_ADDRESS2" />
						<br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_CITY" /> 
						<xsl:value-of select="' '" /> 
						<br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_STATE" /> 
						<xsl:value-of select="' '" />
						<br /> <xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@PRIMARY_APP_ZIPCODE" />
					</td>
					<td class="boldtxt" align="left" width="41%" valign="top">
            <xsl:value-of select="$MyDoc//message[@messageid='209']" /> :<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_NAME" />		
						<br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_PHONE" />
						<xsl:choose>
							<xsl:when test="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_ADD2 !=''">
								<br />
								<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_ADD1" />,
							</xsl:when>
							<xsl:otherwise>
							<br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_ADD1" /> 
							</xsl:otherwise>
						</xsl:choose>
						<br /><xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@AGENCY_ADD2" />
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
            <xsl:value-of select="$MyDoc//message[@messageid='208']"/> -
            <xsl:value-of select="PRIMIUM/HEADER/@QUOTE_DATE" />
            <!--br />Quote Effective -<xsl:value-of select="PRIMIUM/HEADER/@QUOTE_EFFECTIVE_DATE" />
						<br />Rates Effective - <xsl:value-of select="PRIMIUM/HEADER/@RATE_EFFECTIVE_DATE" />-->
            <!--(<xsl:value-of select="PRIMIUM/HEADER/@BUSINESS_TYPE" />)-->
            <xsl:choose>
              <xsl:when test="PRIMIUM/CLIENT_TOP_INFO/@QQ_NUMBER != ''">
                <br />
                <xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@QQ_NUMBER" />
              </xsl:when>
              <xsl:otherwise>
                <xsl:choose>
                  <xsl:when test="PRIMIUM/CLIENT_TOP_INFO/@APP_NUMBER != ''">
                    <br /><xsl:value-of select="$MyDoc//message[@messageid='207']" />-<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@APP_NUMBER" />
                    <br /><xsl:value-of select="$MyDoc//message[@messageid='205']" />-<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@APP_VERSION" />
                  </xsl:when>
                  <xsl:when test="PRIMIUM/CLIENT_TOP_INFO/@POL_NUMBER != ''">
                    <br /><xsl:value-of select="$MyDoc//message[@messageid='206']" />-<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@POL_NUMBER" />
                    <br /><xsl:value-of select="$MyDoc//message[@messageid='205']"/>-<xsl:value-of select="PRIMIUM/CLIENT_TOP_INFO/@POL_VERSION" />
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
			<td colspan='4'></td>
		</tr>
	</xsl:template>
	<!-- ************************** TEMPLATE FOR SUB HEADINGS *****************************************************  -->
	<xsl:template name="SUBHEADINGS">
		<tr class="headereffectCenter">
			<td align="left" colspan="3">
        <xsl:value-of select="$MyDoc//message[@messageid='204']" />
      </td>
			<!-- td>          </td>
			<td>     </td -->
			<td>
        <xsl:value-of select="$MyDoc//message[@messageid='203']" />
      </td>
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
					<xsl:attribute name="value">
            <xsl:value-of select="$MyDoc//message[@messageid='211']" />
          </xsl:attribute>
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
			<xsl:if test="user:CalculatePremium(0)!='0'">
				<td>
					<b>
            <xsl:value-of select="$MyDoc//message[@messageid='201']" />
          </b><!--Total Premium-->
        </td>
				<td></td>
				<td></td>
				<td class="midcolorr">
					<b>$<xsl:value-of select="user:CalculatePremium(0)" /></b>
				</td>
			</xsl:if>
		</tr>
	</xsl:template>
	<!-- ************************** TEMPLATE FOR EFFECTIVE PREMIUM  *****************************************************  -->
	<xsl:template name="GROSSPREMIUM">
		<tr class="midcolora">
			<xsl:if test="PRIMIUM/TOTALOLDPREMIUM !=''">
				<td colspan="3">
					<b><xsl:value-of select="$MyDoc//message[@messageid='197']" /></b><!--Already Charged Premium-->
        </td>
			</xsl:if>
			<!--td></td>
			<td></td -->
			<td class="midcolorr">
				<b>
					<xsl:value-of select="PRIMIUM/TOTALOLDPREMIUM" />
				</b>
			</td>
		</tr>
		<tr class="midcolora">
			<xsl:if test="PRIMIUM/TOTALNEWPREMIUM !=''">
				<td colspan="3">
					<b><xsl:value-of select="$MyDoc//message[@messageid='198']" /></b><!--Total Due as per the Latest Changes-->
        </td>
			</xsl:if>
			<!--td></td>
			<td></td -->
			<td class="midcolorr">
				<b>
					<xsl:value-of select="PRIMIUM/TOTALNEWPREMIUM" />
				</b>
			</td>
		</tr>
		<tr class="midcolora">
			<xsl:if test="PRIMIUM/EFFECTIVEPREMIUM !=''">
				<td colspan="3">
					<b>
            <xsl:value-of select="$MyDoc//message[@messageid='199']" />
          </b><!--Effective Due to Change-->
        </td>
			</xsl:if>
			<!--td></td>
			<td></td -->
			<td class="midcolorr">
				<b>
					<xsl:value-of select="PRIMIUM/EFFECTIVEPREMIUM" />
				</b>
			</td>
		</tr>
		<tr class="midcolora" >
			<xsl:if test="PRIMIUM/EFFECTIVEPREMIUMPRORATA !=''">
				<td>
					<b>
            <xsl:value-of select="$MyDoc//message[@messageid='200']" />
          </b><!--Net Effective-->
        </td>
			</xsl:if>
      <td colspan="2"></td>
      
			<td class="midcolorr">
				<b>
					<xsl:value-of select="PRIMIUM/EFFECTIVEPREMIUMPRORATA" />
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
		<!--<xsl:apply-templates select="RISK[@TYPE='MSE']/STEP" />-->
	</xsl:template>
	<!-- ***************************************************************************************************  -->
	<xsl:template name="DISCLAIMER">
		<tr>
			<td valign="botton" colspan="4" class="boldtxt">
        <xsl:value-of select="$MyDoc//message[@messageid='202']" />
			</td><!--This is a quotation only and does not bind any coverages. The coverages and premiums are subject to change without notice by the insurance company.-->
    </tr>
	</xsl:template>
</xsl:stylesheet>
