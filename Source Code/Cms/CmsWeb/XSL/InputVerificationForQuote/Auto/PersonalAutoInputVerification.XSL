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
	<xsl:variable name="AutoCoveragesDoc" select="document('FactorPathPer')"></xsl:variable>
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
						
						<xsl:call-template name="MAIN_FOR_INPUT_XML" />
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
		
		<xsl:for-each select="VEHICLES/VEHICLE">
			<xsl:call-template name="VERIFICATION_FOR_VEHICLES" />
		</xsl:for-each>
		<!-- auto database check -->
		<xsl:call-template name="VALUE_AUTO_DATA" />
		<!-- LP 124 Landlords Liability -->
		<xsl:for-each select="DRIVERS/DRIVER">
			<xsl:call-template name="VERIFICATION_FOR_DRIVERS" />
		</xsl:for-each>
	</xsl:template>
	<!-- ==================================================================================================
					Checking the null values for vehicles
	=================================================================================================== -->
	<xsl:template name="VERIFICATION_FOR_VEHICLES">
		<xsl:variable name="VAR_VEHICLETYPEUSE" select="VEHICLETYPEUSE" />
		<xsl:if test="normalize-space($VAR_VEHICLETYPEUSE) = ''">
			<tr>
				<td class="midcolora">
				
					<li> The Data corresponding to vehicle type use is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for VEHICLECLASS
	=================================================================================================== -->
		<xsl:variable name="VAR_VEHICLECLASS" select="VEHICLECLASS" />
		<xsl:if test="normalize-space($VAR_VEHICLECLASS) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to vehicleclass is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for VEHICLECLASS_DESC
	=================================================================================================== -->
		<xsl:variable name="VAR_VEHICLECLASS_DESC" select="VEHICLECLASS_DESC" />
		<xsl:variable name="VAR_VEHICLETYPEUSE_COM" select="VEHICLETYPEUSE" />
		<xsl:if test="$VAR_VEHICLETYPEUSE_COM ='PERSONAL'">
		<xsl:if test="normalize-space($VAR_VEHICLECLASS_DESC) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to vehicle class descr is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for VEHICLECLASSCOMPONENT1
	=================================================================================================== -->
		<xsl:variable name="VAR_VEHICLECLASSCOMPONENT1" select="VEHICLECLASSCOMPONENT1" />
		<xsl:if test="$VAR_VEHICLETYPEUSE_COM ='PERSONAL'">
		<xsl:if test="normalize-space($VAR_VEHICLECLASSCOMPONENT1) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to vehicle class component is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for VEHICLECLASSCOMPONENT2
	=================================================================================================== -->
		<xsl:variable name="VAR_VEHICLECLASSCOMPONENT2" select="VEHICLECLASSCOMPONENT2" />
		<xsl:if test="$VAR_VEHICLETYPEUSE_COM ='PERSONAL'">
		<xsl:if test="normalize-space($VAR_VEHICLECLASSCOMPONENT2) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to vehicle class component is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for PDDEDUCTIBLE
	=================================================================================================== -->
		<xsl:variable name="VAR_PDDEDUCTIBLE" select="PDDEDUCTIBLE" />
		<xsl:if test="normalize-space($VAR_PDDEDUCTIBLE) = ''">
			<tr>
				<td class="midcolora">
					<li>The Data corresponding to PDDEDUCTIBLE is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for YEAR
	=================================================================================================== -->
		<xsl:variable name="VAR_YEAR" select="YEAR" />
		<xsl:if test="normalize-space($VAR_YEAR) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to Year is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for MAKE
	=================================================================================================== -->
		<xsl:variable name="VAR_MAKE" select="MAKE" />
		<xsl:if test="normalize-space($VAR_MAKE) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to Make is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for MODEL
	=================================================================================================== -->
		<xsl:variable name="VAR_Model" select="MODEL" />
		<xsl:if test="normalize-space($VAR_Model) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to Model is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for VIN
	=================================================================================================== -->
		<xsl:variable name="VAR_VIN" select="VIN" />
		<xsl:if test="normalize-space($VAR_VIN) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to Vin is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for SYMBOL
	=================================================================================================== -->
		<xsl:variable name="VAR_SYMBOL" select="SYMBOL" />
		<xsl:if test="normalize-space($VAR_SYMBOL) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to Symbol is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--===========================================================================
				Data for Collision deductible factor
	=============================================================================-->
		<xsl:variable name="VAR_COVGCOLLISIONDEDUCTIBLE_DATA" select="COVGCOLLISIONDEDUCTIBLE" />
		<xsl:if test="$VAR_COVGCOLLISIONDEDUCTIBLE_DATA != '' ">
			<xsl:variable name="VAR_COVGCOLLISIONDEDUCTIBLE_RATE" select="$AutoCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='AUTO-P']/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION_DED_FACTOR']/ATTRIBUTES[@DEDUCTIBLE = $VAR_COVGCOLLISIONDEDUCTIBLE_DATA]/@FACTOR" />
			<xsl:if test="$VAR_COVGCOLLISIONDEDUCTIBLE_RATE = ''">
				<tr>
					<td class="midcolora">
						<li> The Data corresponding to Collision deductible factor is not found </li>
					</td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for RADIUSOFUSE
	=================================================================================================== -->
		<!--<xsl:variable name="VAR_RADIUSOFUSE" select="RADIUSOFUSE" />
		<xsl:if test="normalize-space($VAR_RADIUSOFUSE) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to Radius Of Use is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>-->
		<!-- ==================================================================================================
					Checking the null values for MILESEACHWAY
	=================================================================================================== -->
		<xsl:variable name="VAR_MILESEACHWAY" select="MILESEACHWAY" />
		<xsl:if test="normalize-space($VAR_MILESEACHWAY) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to Miles Each Way is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for DRIVERINCOME
	=================================================================================================== -->
		<xsl:variable name="VAR_DRIVERINCOME" select="DRIVERINCOME" />
		<xsl:if test="normalize-space($VAR_DRIVERINCOME) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to DRIVER INCOME is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for DEPENDENTS
	=================================================================================================== -->
		<xsl:variable name="VAR_DEPENDENTS" select="DEPENDENTS" />
		<xsl:if test="normalize-space($VAR_DEPENDENTS) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to DEPENDENTS is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for IS200SOUNDREPRODUCING
	=================================================================================================== -->
		<xsl:variable name="VAR_IS200SOUNDREPRODUCING" select="IS200SOUNDREPRODUCING" />
		<xsl:if test="normalize-space($VAR_IS200SOUNDREPRODUCING) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to is200SoundReproducing is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for WAIVEWORKLOSS
	=================================================================================================== -->
		<xsl:variable name="VAR_WAIVEWORKLOSS" select="WAIVEWORKLOSS" />
		<xsl:if test="$VAR_WAIVEWORKLOSS != 'FALSE' and $VAR_WAIVEWORKLOSS !='TRUE'">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to WAIVEWORK LOSS is not in proper formate </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for MINITORTPDLIAB
	=================================================================================================== -->
		<xsl:variable name="VAR_MINITORTPDLIAB" select="MINITORTPDLIAB" />
		<xsl:if test="$VAR_MINITORTPDLIAB != 'FALSE' and $VAR_MINITORTPDLIAB !='TRUE'">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to MiniTortPdLiab is not in proper formate </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for ISANTILOCKBRAKESDISCOUNTS
	=================================================================================================== -->
		<xsl:variable name="VAR_ISANTILOCKBRAKESDISCOUNTS" select="ISANTILOCKBRAKESDISCOUNTS" />
		<xsl:if test="$VAR_ISANTILOCKBRAKESDISCOUNTS != 'FALSE' and $VAR_ISANTILOCKBRAKESDISCOUNTS !='TRUE'">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to isAntiLockBrakes Discounts is not in proper formate </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for WEARINGSEATBELT
	=================================================================================================== -->
		<xsl:variable name="VAR_WEARINGSEATBELT_VEHICLE" select="WEARINGSEATBELT" />
		<xsl:if test="$VAR_WEARINGSEATBELT_VEHICLE != 'YES' and $VAR_WEARINGSEATBELT_VEHICLE !='NO'">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to wearingSeatBelt is not in proper formate </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for WEARINGSEATBELT
	=================================================================================================== -->
		<xsl:variable name="VAR_WEARINGSEATBELT" select="WEARINGSEATBELT" />
		<xsl:if test="$VAR_WEARINGSEATBELT != 'Yes' or $VAR_WEARINGSEATBELT != 'No'">
			<tr>
				<td class="midcolora">
					<li>The Data corresponding to wearingSeatBelt is not in proper formate </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for QUALIFIESTRAIBLAZERPROGRAM
	=================================================================================================== -->
		<xsl:variable name="VAR_QUALIFIESTRAIBLAZERPROGRAM_VEHICLE" select="QUALIFIESTRAIBLAZERPROGRAM" />
		<xsl:if test="$VAR_QUALIFIESTRAIBLAZERPROGRAM_VEHICLE != 'TRUE' and $VAR_QUALIFIESTRAIBLAZERPROGRAM_VEHICLE !='FALSE'">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to qualifiesTraiblazerProgram is not in proper formate </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
	</xsl:template>
	<!-- ==================================================================================================
							Checking the null values for DRIVERFNAME
	=================================================================================================== -->
	<xsl:template name="VERIFICATION_FOR_DRIVERS">
		<xsl:variable name="VAR_DRIVERFNAME" select="DRIVERFNAME" />
		<xsl:if test="normalize-space($VAR_DRIVERFNAME) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to DriverFName is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for DRIVERLNAME
	=================================================================================================== -->
		<xsl:variable name="VAR_DRIVERLNAME" select="DRIVERLNAME" />
		<xsl:if test="normalize-space($VAR_DRIVERLNAME) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to DriverLName is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for BIRTHDATE
	=================================================================================================== -->
		<xsl:variable name="VAR_BIRTHDATE" select="BIRTHDATE" />
		<xsl:if test="normalize-space($VAR_BIRTHDATE) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to BirthDate is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for GENDER
	=================================================================================================== -->
		<xsl:variable name="VAR_GENDER" select="GENDER" />
		<xsl:if test="normalize-space($VAR_GENDER) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to Gender is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for NOPREMDRIVERDISC
	=================================================================================================== -->
		<xsl:variable name="VAR_NOPREMDRIVERDISC" select="NOPREMDRIVERDISC" />
		<xsl:if test="$VAR_NOPREMDRIVERDISC != 'TRUE' and $VAR_NOPREMDRIVERDISC !='FALSE'">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to NoPremDriverDisc is not in proper formate </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for COLLEGESTUDENT
	=================================================================================================== -->
		<xsl:variable name="VAR_COLLEGESTUDENT" select="COLLEGESTUDENT" />
		<xsl:if test="$VAR_COLLEGESTUDENT != 'TRUE' and $VAR_COLLEGESTUDENT !='FALSE'">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to CollegeStudent is not in proper formate </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for GOODSTUDENT
	=================================================================================================== -->
		<xsl:variable name="VAR_GOODSTUDENT" select="GOODSTUDENT" />
		<xsl:if test="$VAR_GOODSTUDENT != 'TRUE' and $VAR_GOODSTUDENT !='FALSE'">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to GoodStudent is not in proper formate </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for DISTANTSTUDENT
	=================================================================================================== -->
		<xsl:variable name="VAR_DISTANTSTUDENT" select="DISTANTSTUDENT" />
		<xsl:if test="$VAR_DISTANTSTUDENT != 'TRUE' and $VAR_DISTANTSTUDENT !='FALSE'">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to DistantStudent is not in proper formate </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
	</xsl:template>
	<!-- ==================================================================================================
							Checking values aginst the xmml files(start)
	=================================================================================================== -->
	<xsl:template name="VALUE_AUTO_DATA">
		<!--===========================================================================
						Data for TERRITORY 
	=============================================================================-->
		<xsl:variable name="VAR_TERCODES" select="POLICY/TERRITORY" />
		<xsl:variable name="TERCODES" select="$AutoCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='AUTO-P']/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE=$VAR_TERCODES]/@MEDPAYBASE" />
		<xsl:if test="$TERCODES = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to TERRITORY is not found </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--===========================================================================
				Data for VEHICLE USE 
	=============================================================================-->
		<xsl:variable name="VAR_VEHICLEUSE_DATA" select="VEHICLES/VEHICLE/VEHICLEUSE" />
		<xsl:variable name="VAR_VEHICLECLASSCOMPONENT1_DTA" select="VEHICLES/VEHICLE/VEHICLECLASSCOMPONENT1" />
		<xsl:variable name="VAR_VEHICLECLASSCOMPONENT2_DTA" select="VEHICLES/VEHICLE/VEHICLECLASSCOMPONENT2" />
		<xsl:variable name="VAR_VEHICLEUSE_TYPE" select="$AutoCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='AUTO-P']/FACTOR[@ID='VEHICLEUSERELATIVITY']/NODE[@ID='VEHICLEUSE']/ATTRIBUTES[@USECODE = $VAR_VEHICLEUSE_DATA and @CLASS=$VAR_VEHICLECLASSCOMPONENT1_DTA or @CLASS=$VAR_VEHICLECLASSCOMPONENT2_DTA]/@FACTOR" />
		<xsl:if test="$VAR_VEHICLEUSE_TYPE = ''">
			<tr>
				<td class="midcolora">
				
				
					<li> The Data corresponding to VEHICLE USE is not found </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--===========================================================================
				Data for Road Service
	=============================================================================-->
		<xsl:variable name="VAR_ROADSERVICE_DATA" select="ROADSERVICE" />
		<xsl:variable name="VAR_ROADSERVICE_RATE" select="$AutoCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='AUTO-P']/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='ROADSERVICELIMIT']/ATTRIBUTES[@COVERAGE = $VAR_ROADSERVICE_DATA]/@RATE" />
		<xsl:if test="$VAR_ROADSERVICE_RATE = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to Road Service is not found </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--===========================================================================
						Data for PD 
	=============================================================================-->
		<xsl:variable name="VAR_PD" select="VEHICLES/VEHICLE/PD" />
		<xsl:variable name="VAR_PDS" select="$AutoCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='AUTO-P']/FACTOR[@ID='LIMITS']/NODE[@ID='PD']/ATTRIBUTES[@COVERAGE=$VAR_PD]/@RELATIVITY" />
		<xsl:if test="$VAR_PDS = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to PD is not found </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--===========================================================================
						Data for CSL 
	=============================================================================-->
		<xsl:variable name="VAR_CSL" select="VEHICLES/VEHICLE/CSL" />
		<xsl:variable name="VAR_CSL1" select="$AutoCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='AUTO-P']/FACTOR[@ID='COMBINEDSINGLELIMITS']/NODE[@ID='CSL']/ATTRIBUTES[@COVERAGE=$VAR_PD]/@RELATIVITY" />
		<xsl:if test="$VAR_CSL1 = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to CSL is not found </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--===========================================================================
				Data for insurance score 
	=============================================================================-->
		<xsl:variable name="VAR_INSURANCESCORE_DATA" select="POLICY/INSURANCESCORE" />
		<xsl:variable name="VAR_INSURANCESCORE_CREAIT" select="$AutoCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE &lt;= $VAR_INSURANCESCORE_DATA and @MAXSCORE &gt;= $VAR_INSURANCESCORE_DATA]/@CREDIT" />
		<!--<xsl:value-of select="$AutoCoveragesDoc" /-->
		<xsl:if test="$VAR_INSURANCESCORE_CREAIT = ''">
			<tr>
				<td class="midcolora">
				<!--<xsl:value-of select="$VAR_INSURANCESCORE_DATA"/>-->
					<li> The Data corresponding to InsuranceScore is not found </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--===========================================================================
				Data for VIOLATION POINTS
	=============================================================================-->
		<xsl:variable name="VAR_SUMOFVIOLATIONPOINTS_DATA" select="DRIVERS/DRIVER/SUMOFVIOLATIONPOINTS" />
		<xsl:variable name="VAR_SUMOFVIOLATIONPOINTS_RATE" select="$AutoCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='AUTO-P']/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS = $VAR_SUMOFVIOLATIONPOINTS_DATA]/@SURCHARGE" />
		<xsl:if test="$VAR_SUMOFVIOLATIONPOINTS_RATE = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to VIOLATION POINTS is not found </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--===========================================================================
				Data for ACCIDENT POINTS
	=============================================================================-->
		<xsl:variable name="VAR_SUMOFACCIDENTPOINTS_DATA" select="DRIVERS/DRIVER/SUMOFACCIDENTPOINTS" />
		<xsl:variable name="VAR_SUMOFACCIDENTPOINTS_RATE" select="$AutoCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='AUTO-P']/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID='SURCHARGEVIOLATIONPOINTS']/ATTRIBUTES[@POINTS = $VAR_SUMOFACCIDENTPOINTS_DATA]/@SURCHARGE" />
		<xsl:if test="$VAR_SUMOFACCIDENTPOINTS_RATE = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to ACCIDENT POINTS is not found </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--===========================================================================
						Data for BILimit1 AND  BILimit2
	=============================================================================-->
		<xsl:variable name="VAR_BILIMIT1" select="VEHICLES/VEHICLE/BILIMIT1" />
		<xsl:variable name="VAR_BILIMIT2" select="VEHICLES/VEHICLE/BILIMIT2" />
		<xsl:variable name="VARE_BILIMIT1" select="$AutoCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='AUTO-P']/FACTOR[@ID='LIMITS']/NODE[@ID='BI']/ATTRIBUTES[@MINCOVERAGE=$VAR_BILIMIT1 and @MAXCOVERAGE=$VAR_BILIMIT2]/@RELATIVITY" />
		<xsl:if test="$VARE_BILIMIT1 = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to BILimit is not found </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--===========================================================================
		Data for uninsured motorist 
	=============================================================================-->
		<xsl:variable name="VAR_TYPE_DATA" select="VEHICLES/VEHICLE/TYPE" />
		<xsl:variable name="VAR_UMCSL_DATA" select="VEHICLES/VEHICLE/UMCSL" />
		<xsl:if test="$VAR_TYPE_DATA != ''">
			<xsl:if test="$VAR_TYPE_DATA = 'Uninsured Motorists'">
				<xsl:variable name="VAR_CSL_UMCSL" select="$AutoCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='AUTO-P']/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID='CSLUMBI']/ATTRIBUTES[@CSLUMBILIMITS=$VAR_UMCSL_DATA]/@CSLUMRATES1" />
				<xsl:if test="$VAR_CSL_UMCSL = ''">
					<tr>
						<td class="midcolora">
							<li> The Data corresponding to UMCSL is not found </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
			</xsl:if>
		</xsl:if>
		<!--===========================================================================
				Data for Comprehensive deductible
	=============================================================================-->
		<xsl:variable name="VAR_COMPREHENSIVEDEDUCTIBLE_DATA" select="COMPREHENSIVEDEDUCTIBLE" />
		<xsl:if test="$VAR_COMPREHENSIVEDEDUCTIBLE_DATA != '' ">
			<xsl:variable name="VAR_COMPREHENSIVEDEDUCTIBLE_RATE" select="$AutoCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='AUTO-P']/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COMPREHENSIVE_DED_FACTOR']/ATTRIBUTES[@DEDUCTIBLE = $VAR_COMPREHENSIVEDEDUCTIBLE_DATA]/@FACTOR" />
			<xsl:if test="$VAR_COMPREHENSIVEDEDUCTIBLE_RATE = ''">
				<tr>
					<td class="midcolora">
						<li> The Data corresponding to Comprehensive deductible factor is not found </li>
					</td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		</xsl:if>
	</xsl:template>
	<!-- ==================================================================================================
							Checking values aginst the xmml files(Ends)
	=================================================================================================== -->
	<!-- ==================================================================================================
								checking the policy tag Data for null(Start)
	=================================================================================================== -->
	<xsl:template name="MAIN_FOR_INPUT_XML">
		<xsl:if test="user:AllVerified(1) = 1">
			<b> Error Details of XML </b>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for TERRITORY
	=================================================================================================== -->
		<xsl:variable name="VAR_TERRITORYCODES" select="POLICY/TERRITORY" />
		<xsl:if test="normalize-space($VAR_TERRITORYCODES) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to Territory Code is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for INSURANCESCORE
	=================================================================================================== -->
		<xsl:variable name="VAR_INSURANCESCORE" select="POLICY/INSURANCESCORE" />
		<xsl:if test="normalize-space($VAR_INSURANCESCORE) = ''">
			<tr>
				<td class="midcolora">
					<li>The Data corresponding to insurance score is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for STATENAME
	=================================================================================================== -->
		<xsl:variable name="VAR_STATENAME" select="POLICY/STATENAME" />
		<xsl:if test="normalize-space($VAR_STATENAME) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to state name is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for NEWBUSINESS
	=================================================================================================== -->
		<xsl:variable name="VAR_NEWBUSINESSFACTOR" select="POLICY/NEWBUSINESS" />
		<xsl:if test="$VAR_NEWBUSINESSFACTOR != 'FALSE' and $VAR_NEWBUSINESSFACTOR !='TRUE'">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to newbusiness factor is not in proper formate </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for QUOTEEFFDATE
	=================================================================================================== -->
		<xsl:variable name="VAR_QUOTEEFFDATE" select="POLICY/QUOTEEFFDATE" />
		<xsl:if test="normalize-space($VAR_QUOTEEFFDATE) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to quote effective date is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for RENEWAL
	=================================================================================================== -->
		<xsl:variable name="VAR_RENEWAL" select="POLICY/RENEWAL" />
		<xsl:if test="$VAR_RENEWAL != 'TRUE' and $VAR_RENEWAL != 'FALSE'">
			<tr>
				<td class="midcolora">
					<li>The Data corresponding to renewal is not in proper formate </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for POLICYTERMS
	=================================================================================================== -->
		<xsl:variable name="VAR_POLICYTERMS" select="POLICY/POLICYTERMS" />
		<xsl:if test="normalize-space($VAR_POLICYTERMS) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to policyterms is blank  </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for ZIPCODE
	=================================================================================================== -->
		<xsl:variable name="VAR_ZIPCODE" select="POLICY/ZIPCODE" />
		<xsl:if test="normalize-space($VAR_ZIPCODE) = ''">
			<tr>
				<td class="midcolora">
					<li> The Data corresponding to zipCode is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for ISUNDERINSUREDMOTORISTS
	=================================================================================================== -->
		<xsl:variable name="VAR_ISUNDERINSUREDMOTORISTS" select="POLICY/ISUNDERINSUREDMOTORISTS" />
		<xsl:if test="$VAR_ISUNDERINSUREDMOTORISTS != 'TRUE' and $VAR_ISUNDERINSUREDMOTORISTS != 'FALSE'">
			<tr>
				<td class="midcolora">
					<li>The Data corresponding to isUnderInsuredMotorists is not in proper formate </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for MULTIPOLICYAUTOHOMEDISCOUNT
	=================================================================================================== -->
		<xsl:variable name="VAR_MULTIPOLICYAUTOHOMEDISCOUNT" select="POLICY/MULTIPOLICYAUTOHOMEDISCOUNT" />
		<xsl:if test="$VAR_MULTIPOLICYAUTOHOMEDISCOUNT != 'Y' and $VAR_MULTIPOLICYAUTOHOMEDISCOUNT != 'N'">
			<tr>
				<td class="midcolora">
					<li>The Data corresponding to multiPolicyAutoHomeDiscount  must be in (Y/N) format </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for YEARSCONTINSURED
	=================================================================================================== -->
		<xsl:variable name="VAR_YEARSCONTINSURED" select="POLICY/YEARSCONTINSURED" />
		<xsl:if test="normalize-space($VAR_YEARSCONTINSURED) = ''">
			<tr>
				<td class="midcolora">
					<li>The Data corresponding to yearsContInsured is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for YEARSCONTINSUREDWITHWOLVERINE
	=================================================================================================== -->
		<xsl:variable name="VAR_YEARSCONTINSUREDWITHWOLVERINE" select="POLICY/YEARSCONTINSUREDWITHWOLVERINE" />
		<xsl:if test="normalize-space($VAR_YEARSCONTINSUREDWITHWOLVERINE) = ''">
			<tr>
				<td class="midcolora">
					<li>The Data corresponding to YEARSCONTINSUREDWITHWOLVERINE is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- ==================================================================================================
					Checking the null values for LOSSES_CHARGEABLE_NONCHARGEABLE
	=================================================================================================== -->
		<xsl:variable name="VAR_LOSSES_CHARGEABLE_NONCHARGEABLE" select="POLICY/LOSSES_CHARGEABLE_NONCHARGEABLE" />
		<xsl:if test="normalize-space($VAR_LOSSES_CHARGEABLE_NONCHARGEABLE) = ''">
			<tr>
				<td class="midcolora">
					<li>The Data corresponding to LOSSES CHARGEABLE NONCHARGEABLE is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
	</xsl:template>
</xsl:stylesheet>
<!-- ==================================================================================================
								checking the policy tag Data for null(End)
	=================================================================================================== -->
