<!-- ==================================================================================================
File Name			:	AUTO InputVerification.XSL 
Purpose				:	AUTO Input Verification 
Name				:	Neeraj Singh
Date				:	17 NOV 2006  
======================================================================================================== -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:user="urn:user-namespace-here">
	<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml" />
	<!-- ============================================================================= -->
	<msxsl:script language="c#" implements-prefix="user">
<![CDATA[ 
		
		int intCSS=1;
		public int ApplyColor(int colorvalue)
		{
			intCSS=colorvalue;			
			return intCSS;
		}
		int statusFlag=1;
		 
		 
		int intIsRejected = 1;
		public int IsApplicationAcceptable(int tt)
		{
			intIsRejected = intIsRejected*tt;
			return intIsRejected;
		}
		int intIsReferred = 1;
		public int CheckRefer(int tt)
		{
			intIsReferred = intIsReferred*tt;
			return intIsReferred;
		}
		 
		int intVerifyXML=1;
		public int AllVerified(int status)
		{
			intVerifyXML=intVerifyXML * status;			
			return intVerifyXML;
		}	 
		
			
]]></msxsl:script>
	<!-- ============================================================================= -->
	<xsl:variable name="UmbCoveragesDoc" select="document('FactorPath')"></xsl:variable>
	<!-- ============================================================================================ 
								Loading ProductFactorMaster File (END)						  
 ============================================================================================ -->
	<xsl:template match="/">
		<html>
			<head>
				<!--<xsl:variable name="myName" select="QUICKQUOTE/POLICY/CSSNUM/@CSSVALUE"></xsl:variable>-->
				<xsl:variable name="myName" select="1"></xsl:variable>
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
			</head>
			<body class="midcolora" bgcolor="ffffff">
				<xsl:apply-templates select="QUICKQUOTE" />
			</body>
			<span style="display:none">
				<returnValue>
					<xsl:value-of select="user:AllVerified(1)" />
				</returnValue>
			</span>
		</html>
	</xsl:template>
	<xsl:template match="QUICKQUOTE">
		<table border="0" align="center" width='90%'>
			<tr class="headereffectCenter">
				<td colspan='6' style="FONT-SIZE: 14pt">
					<b>
					Error Report
				</b>
				</td>
			</tr>
			<tr>
				<td></td>
			</tr>
			<tr>
				<td></td>
			</tr>
			<tr>
				<td></td>
			</tr>
		</table>
		<table border="0" align="center" width='90%' bgcolor="ffffff">
			<ul type="disc">
				<tr>
					<td class="midcolora">
						<!--<xsl:call-template name="MAIN_FOR_INPUT_XML" />-->
						<xsl:call-template name="MAIN_FOR_MASTER_XML" />
					</td>
				</tr>
			</ul>
		</table>
	</xsl:template>
	<!-- ==================================================================================================
							
	=================================================================================================== -->
	<xsl:template name="MAIN_FOR_MASTER_XML">
		<!-- vehicle null value check -->
	<xsl:for-each select="UMBRELLA/APPLICANT_INFIORMATION">
			<xsl:call-template name="VERIFICATION_FOR_GENRALINFORMATION" />
	</xsl:for-each>
		<!-- UMBRELLA database check -->
		
			<xsl:call-template name="VALUE_UNDERLYINGPOLICY_DATA" />
		
		
	</xsl:template>
	<!-- ==================================================================================================
					Checking the null values for territory code
	=================================================================================================== -->
	<xsl:template name="VERIFICATION_FOR_GENRALINFORMATION">
		<xsl:variable name="VAR_TERRITORYCODES" select="TERRITORYCODES" />
		<xsl:if test="normalize-space($VAR_TERRITORYCODES) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to Territory Code use is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		
		<!-- ==================================================================================================
					Checking the null values for Umbrella Limit
	=================================================================================================== -->
		<xsl:variable name="VAR_UUMBRELLA_LIMIT" select="UUMBRELLA_LIMIT" />
		<xsl:if test="normalize-space($VAR_UUMBRELLA_LIMIT) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to Umbrella Limit  is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for Lob Id 
	=================================================================================================== -->
		<xsl:variable name="VAR_LOB_ID" select="LOB_ID" />
		<xsl:if test="normalize-space($VAR_LOB_ID) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to Lob Id is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for auto coverages
	=================================================================================================== -->
	
		<xsl:variable name="VAR_PERSONALAUTOPOLICYLOWERLIMIT" select="PERSONALAUTOPOLICYLOWERLIMIT" />
		<xsl:variable name="VAR_PERSONALAUTOPOLICYUPPERLIMIT" select="PERSONALAUTOPOLICYUPPERLIMIT" />
		<xsl:variable name="VAR_AUTOCSL" select="AUTOCSL" />
		<xsl:variable name="VAR_AUTOPD" select="AUTOPD" />
		<xsl:variable name="VAR_AUTOMOBILES" select="UMBRELLA/PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/AUTOMOBILES" />
		<xsl:variable name="VAR_MOTOTHOMES" select="UMBRELLA/PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/MOTOTHOMES" />
	
	
		
		<xsl:choose>
			<xsl:when test="$VAR_AUTOMOBILES !='' or $VAR_MOTOTHOMES!=''">
		<xsl:if test="(normalize-space($VAR_PERSONALAUTOPOLICYLOWERLIMIT) = '' and normalize-space($VAR_PERSONALAUTOPOLICYUPPERLIMIT) = '' and  normalize-space($VAR_AUTOPD) = '')and (normalize-space($VAR_AUTOCSL) = '')">
					<tr>
						<td class="midcolora">
							<li> The Data corresponding to auto coverage  is blank </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
	
		</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
		
		<!-- ==================================================================================================
					Checking the null values for motor cycle coverages
	=================================================================================================== -->
	
		<xsl:variable name="VAR_PERSONALMOTORCYCLEPOLICYLOWERLIMIT" select="UMBRELLA/PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/MOTORCYCLEPOLICYLOWERLIMIT" />
		<xsl:variable name="VAR_PERSONALMOTORCYCLEPOLICYUPPERLIMIT" select="UMBRELLA/PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/MOTORCYCLEPOLICYUPPERLIMIT" />
		<xsl:variable name="VAR_MOTORCSL" select="UMBRELLA/PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/MOTORCSL" />
		<xsl:variable name="VAR_MOTORPD" select="UMBRELLA/PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/MOTORPD" />
		<xsl:variable name="VAR_MOTORCYCLES" select="UMBRELLA/PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/MOTORCYCLES" />
		<xsl:choose>
			<xsl:when test="$VAR_MOTORCYCLES !=''">
		<xsl:if test="(normalize-space($VAR_PERSONALMOTORCYCLEPOLICYLOWERLIMIT) = '' and normalize-space($VAR_PERSONALMOTORCYCLEPOLICYUPPERLIMIT) = '' and  normalize-space($VAR_MOTORPD) = '')and (normalize-space($VAR_MOTORCSL) = '')">
					<tr>
						<td class="midcolora">
							<li> The Data corresponding to motor coverage is blank </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
	
		</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
		
		</xsl:template>
	<!-- ==================================================================================================
							Checking values aginst the xmml files(start)
	=================================================================================================== -->
	<xsl:template name="VALUE_UNDERLYINGPOLICY_DATA">
		<!--===========================================================================
						Data for motorcycle coverage 
	=============================================================================-->
		<xsl:variable name="VAR_TERCODES" select="UMBRELLA/APPLICANT_INFIORMATION/TERRITORYCODES" />
		<xsl:variable name="VAR_PERSONALMOTORPOLICYLOWERLIMIT" select="UMBRELLA/PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/MOTORCYCLEPOLICYLOWERLIMIT" />
		<xsl:variable name="VAR_PERSONALMOTORPOLICYUPPERLIMIT" select="UMBRELLA/PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/MOTORCYCLEPOLICYUPPERLIMIT" />
		<xsl:variable name="VAR_MOTORCSL" select="UMBRELLA/PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/MOTORCSL" />
		<xsl:variable name="VAR_MOTORPD" select="UMBRELLA/PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/MOTORPD" />
		<xsl:variable name="VAR_MOTORCYCLES" select="UMBRELLA/PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/MOTORCYCLES" />
		
		
		<xsl:variable name="PERSONALMOTORPOLICYLOWERLIMIT">
			<xsl:choose>
				<xsl:when test="normalize-space($VAR_PERSONALMOTORPOLICYLOWERLIMIT) = ''">0</xsl:when>
				<xsl:when test="$VAR_PERSONALMOTORPOLICYLOWERLIMIT &gt; 0">
					<xsl:value-of select="format-number(($VAR_PERSONALMOTORPOLICYLOWERLIMIT div 1000),'###')" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		
		
		
		<xsl:variable name="PERSONALMOTORPOLICYUPPERLIMIT">
			<xsl:choose>
				<xsl:when test="normalize-space($VAR_PERSONALMOTORPOLICYUPPERLIMIT) = ''">0</xsl:when>
				<xsl:when test="$VAR_PERSONALMOTORPOLICYUPPERLIMIT &gt; 0">
					<xsl:value-of select="format-number(($VAR_PERSONALMOTORPOLICYUPPERLIMIT div 1000),'###')" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		
		<xsl:variable name="MOTORPD">
			<xsl:choose>
				<xsl:when test="normalize-space($VAR_MOTORPD) = ''">0</xsl:when>
				<xsl:when test="$VAR_MOTORPD &gt; 0">
					<xsl:value-of select="format-number(($VAR_MOTORPD div 1000),'###')" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		
		
		
		<xsl:variable name="MOTORCSL">
			<xsl:choose>
				<xsl:when test="normalize-space($VAR_MOTORCSL) = ''">0</xsl:when>
				<xsl:when test="$VAR_MOTORCSL &gt; 0">
					<xsl:value-of select="format-number(($VAR_MOTORCSL div 1000),'###')" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		
		<xsl:choose>
			<xsl:when test="$VAR_MOTORCYCLES !='' and $VAR_PERSONALMOTORPOLICYLOWERLIMIT!=''">
		<xsl:variable name="VAR_COVERAGE_BI" select="$UmbCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='MOTORCYCLE_MOTOR_HOME_BI']/ATTRIBUTES[@TERRITORY=$VAR_TERCODES and @LOWERLIMITBISPLIT=$PERSONALMOTORPOLICYLOWERLIMIT and @UPPERLIMITBISPLIT=$PERSONALMOTORPOLICYUPPERLIMIT and @PD_LIMIT=$MOTORPD]/@VALUE" />
		<xsl:if test="(normalize-space($VAR_COVERAGE_BI) = '')">
					<tr>
						<td class="midcolora">
							<li> The Data corresponding to motor coverage(BI SPLIT AND PD) is not found </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
			
		</xsl:when>
			<xsl:when test="$VAR_MOTORCYCLES !='' and $VAR_MOTORCSL!=''">
		<xsl:variable name="VAR_COVERAGE_CSL" select="$UmbCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='MOTORCYCLE_MOTOR_HOME_CSL']/ATTRIBUTES[@TERRITORY=$VAR_TERCODES and @CSL_LIMIT=$MOTORCSL]/@VALUE" />
		<xsl:if test="(normalize-space($VAR_COVERAGE_CSL) = '')">
					<tr>
						<td class="midcolora">
							<li> The Data corresponding to motor coverage(CSL) is not found </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				
		</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
		<!--===========================================================================
						Data for auto coverage 
	=============================================================================-->
		<xsl:variable name="VAR_PERSONALAUTOPOLICYLOWERLIMIT" select="UMBRELLA/PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/PERSONALAUTOPOLICYLOWERLIMIT" />
		<xsl:variable name="VAR_PERSONALAUTOPOLICYUPPERLIMIT" select="UMBRELLA/PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/PERSONALAUTOPOLICYUPPERLIMIT" />
		<xsl:variable name="VAR_AUTOCSL" select="UMBRELLA/PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/AUTOCSL" />
		<xsl:variable name="VAR_AUTOPD" select="UMBRELLA/PERSONAL_AUTO_EXPOSURES/UNDERLYINGPOLICY/AUTOPD" />
		<xsl:variable name="VAR_AUTOMOBILE" select="UMBRELLA/PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/AUTOMOBILES" />
		<xsl:variable name="VAR_MOTORHOME" select="UMBRELLA/PERSONAL_AUTO_EXPOSURES/AUTOMOBILE_MOTORVEHICLE_EXPOSURES/MOTOTHOMES" />
		
			<xsl:variable name="PERSONALAUTOPOLICYLOWERLIMIT">
			<xsl:choose>
				<xsl:when test="normalize-space($VAR_PERSONALAUTOPOLICYLOWERLIMIT) = ''">0</xsl:when>
				<xsl:when test="$VAR_PERSONALAUTOPOLICYLOWERLIMIT &gt; 0">
					<xsl:value-of select="format-number(($VAR_PERSONALAUTOPOLICYLOWERLIMIT div 1000),'###')" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		
		
		
		<xsl:variable name="PERSONALAUTOPOLICYUPPERLIMIT">
			<xsl:choose>
				<xsl:when test="normalize-space($VAR_PERSONALAUTOPOLICYUPPERLIMIT) = ''">0</xsl:when>
				<xsl:when test="$VAR_PERSONALAUTOPOLICYUPPERLIMIT &gt; 0">
					<xsl:value-of select="format-number(($VAR_PERSONALAUTOPOLICYUPPERLIMIT div 1000),'###')" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		
		<xsl:variable name="AUTOPD">
			<xsl:choose>
				<xsl:when test="normalize-space($VAR_AUTOPD) = ''">0</xsl:when>
				<xsl:when test="$VAR_AUTOPD &gt; 0">
					<xsl:value-of select="format-number(($VAR_AUTOPD div 1000),'###')" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		
		
		
		<xsl:variable name="CSLLIMIT">
			<xsl:choose>
				<xsl:when test="normalize-space($VAR_AUTOCSL) = ''">0</xsl:when>
				<xsl:when test="$VAR_AUTOCSL &gt; 0">
					<xsl:value-of select="format-number(($VAR_AUTOCSL div 1000),'###')" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		
		<xsl:choose>
			<xsl:when test="$VAR_AUTOMOBILE !='' and $VAR_PERSONALAUTOPOLICYLOWERLIMIT!=''">
		<xsl:variable name="VAR_COVERAGE_AUTO_BI" select="$UmbCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='AUTOMOBILE_BI']/ATTRIBUTES[@TERRITORY=$VAR_TERCODES and @LOWERLIMITBISPLIT=$PERSONALAUTOPOLICYLOWERLIMIT and @UPPERLIMITBISPLIT=$PERSONALAUTOPOLICYUPPERLIMIT and @PD_LIMIT=$AUTOPD]/@VALUE" />
			<xsl:if test="(normalize-space($VAR_COVERAGE_AUTO_BI) = '')">
					<tr>
						<td class="midcolora">
						<!--<xsl:value-of select="$VAR_COVERAGE_AUTO_BI" />-->
							<li> The Data corresponding to AUTO coverage(BI SPLIT AND PD) is not found </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
		
		</xsl:when>
			<xsl:when test="$VAR_AUTOMOBILE !='' and $VAR_AUTOCSL!=''">
		<xsl:variable name="VAR_COVERAGE_AUTO_CSL" select="$UmbCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='AUTOMOBILE_CSL']/ATTRIBUTES[@TERRITORY=$VAR_TERCODES and @CSL_LIMIT=$CSLLIMIT]/@VALUE" />
			<xsl:if test="(normalize-space($VAR_COVERAGE_AUTO_CSL) = '')">
					<tr>
						<td class="midcolora">
							<li> The Data corresponding to AUTO coverage(CSL) is not found </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				
		</xsl:when>
			<xsl:when test="$VAR_MOTORHOME !='' and $VAR_PERSONALAUTOPOLICYLOWERLIMIT!=''">
		<xsl:variable name="VAR_COVERAGE_MOTORHOME_BI" select="$UmbCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='MOTORCYCLE_MOTOR_HOME_BI']/ATTRIBUTES[@TERRITORY=$VAR_TERCODES and @LOWERLIMITBISPLIT=$VAR_PERSONALMOTORPOLICYLOWERLIMIT and @UPPERLIMITBISPLIT=$VAR_PERSONALMOTORPOLICYUPPERLIMIT and @PD_LIMIT=$VAR_MOTORPD]/@VALUE" />
			<xsl:if test="(normalize-space($VAR_COVERAGE_MOTORHOME_BI) = '')">
					<tr>
						<td class="midcolora">
							<li> The Data corresponding to MOTORHOME coverage(BI SPLIT AND PD) is not found </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				
		</xsl:when>
			<xsl:when test="$VAR_AUTOMOBILE !='' and $VAR_AUTOCSL!=''">
		<xsl:variable name="VAR_COVERAGE_MOTORHOME_CSL" select="$UmbCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGES']/NODE[@ID='MOTORCYCLE_MOTOR_HOME_CSL']/ATTRIBUTES[@TERRITORY=$VAR_TERCODES and @CSL_LIMIT=$VAR_MOTORCSL]/@VALUE" />
			<xsl:if test="(normalize-space($VAR_COVERAGE_MOTORHOME_CSL) = '')">
					<tr>
						<td class="midcolora">
							<li> The Data corresponding to MOTORHOME coverage(CSL) is not found </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
		
		</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
		
		
	</xsl:template>
</xsl:stylesheet>

