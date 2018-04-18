<!-- ==================================================================================================
File Name			:	HOInputVerification.XSL 
Purpose				:	HO  Input Verification 
Name				:	Praveen kumar Singh
Date				:	17 july 2006  
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
	<xsl:variable name="HOCoveragesDoc" select="document('FactorPath')"></xsl:variable>
	<!-- Lookup id 1195,1216 in masterlookup_values. these are distinct codes. 
	 Indiana and michigan states have same code for same policy types -->
	<xsl:variable name="POLICYCODE_HO2REPAIR" select="'HO-2^REPAIR'" />
	<xsl:variable name="POLICYCODE_HO2REPLACE" select="'HO-2^REPLACE'" />
	<xsl:variable name="POLICYCODE_HO3PREMIER" select="'HO-3^PREMIER'" />
	<xsl:variable name="POLICYCODE_HO3REPAIR" select="'HO-3^REPAIR'" />
	<xsl:variable name="POLICYCODE_HO3REPLACE" select="'HO-3^REPLACE'" />
	<xsl:variable name="POLICYCODE_HO4DELUXE" select="'HO-4^DELUXE'" />
	<xsl:variable name="POLICYCODE_HO4TENANT" select="'HO-4^TENANT'" />
	<xsl:variable name="POLICYCODE_HO5PREMIER" select="'HO-5^PREMIER'" />
	<xsl:variable name="POLICYCODE_HO5REPLACE" select="'HO-5^REPLACE'" />
	<xsl:variable name="POLICYCODE_HO6DELUXE" select="'HO-6^DELUXE'" />
	<xsl:variable name="POLICYCODE_HO6UNIT" select="'HO-6^UNIT'" />
	<!-- Break up of the above -->
	<!-- Policy types -->
	<xsl:variable name="POLICYTYPE_HO2" select="'HO-2'" />
	<xsl:variable name="POLICYTYPE_HO3" select="'HO-3'" />
	<xsl:variable name="POLICYTYPE_HO4" select="'HO-4'" />
	<xsl:variable name="POLICYTYPE_HO5" select="'HO-5'" />
	<xsl:variable name="POLICYTYPE_HO6" select="'HO-6'" />
	<!-- to check ProductPremier node -->
	<xsl:variable name="POLICYDESC_REPAIR" select="'REPAIR'" />
	<xsl:variable name="POLICYDESC_REPLACEMENT" select="'REPLACE'" />
	<xsl:variable name="POLICYDESC_DELUXE" select="'DELUXE'" />
	<xsl:variable name="POLICYDESC_TENANT" select="'TENANT'" />
	<xsl:variable name="POLICYDESC_PREMIER" select="'PREMIER'" />
	<xsl:variable name="POLICYDESC_UNIT" select="'UNIT'" />
	<!-- ============================================================================================ 
								Loading ProductFactorMaster File (END)						  
 ============================================================================================ -->
	<xsl:template match="/">
		<html>
			<head>
				<!--<xsl:variable name="myName" select="QUICKQUOTE/DWELLINGDETAILS/CSSNUM/@CSSVALUE"></xsl:variable> -->
				<xsl:variable name="myName" select="1" />
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
				<br />
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
						<xsl:for-each select="DWELLINGDETAILS">
							<!-- <xsl:call-template name="MAIN_FOR_INPUT_XML"/>    --></xsl:for-each>
						<xsl:if test="user:AllVerified(1) = 1">
							<xsl:for-each select="DWELLINGDETAILS">
								<xsl:call-template name="MAIN_FOR_MASTER_XML" />
							</xsl:for-each>
						</xsl:if>
					</td>
				</tr>
			</ul>
		</table>
	</xsl:template>
	<xsl:template name="MAIN_FOR_MASTER_XML">
		<!-- test start -->
		<xsl:choose>
			<!-- Not applicable in HO-4 policy type -->
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4"></xsl:when>
			<!-- HO-6 policy type -->
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO6">
				<xsl:call-template name="COVERAGE_A_PREMIUM_FOR_HO6" />
			</xsl:when>
			<!-- Applicable in all others -->
			<xsl:otherwise>
				<xsl:call-template name="CALL_ADJUSTEDBASE" />
			</xsl:otherwise>
		</xsl:choose>
		<!-- COVERAGE - C -->
		<xsl:choose>
			<!-- Coverage C considered for HO4 and HO-6 -->
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6">
				<xsl:call-template name="CALL_ADJUSTEDBASE" />
			</xsl:when>
			<!-- For all the others, nothing to  be displayed in premium column -->
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
		<!-- COVERAGE - D -->
		<xsl:variable name="VAR1">
			<xsl:call-template name="COVERAGE_D_PREMIUM" />
		</xsl:variable>
		<!-- Error1 end -->
		<!-- Term Factor  -->
		<xsl:call-template name="TERM_FACTOR" />
		<!-- Information: Maximum Discount -->
		<xsl:call-template name="MAX_DISCOUNT_APPLIED_DISPLAY" />
		<!-- Experience Factor -->
		<xsl:call-template name="EXPERIENCEFACTOR_DISPLAY" />
		<!-- Age of Home -->
		<xsl:call-template name="AGEOFHOME_DISPLAY" />
		<!-- Protective Device -->
		<xsl:call-template name="PROTECTIVEDEVICE_DISPLAY" />
		<!-- Breed of Dog -->
		<xsl:call-template name="BREEDDOG" />
		<!-- Preferred Plus -->
		<xsl:call-template name="PREFERRED_PLUS_DISPLAY" />
		<!-- Error 2 Start -->
		<!--  HO-34 Replacement Cost Personal Property -->
		<!-- <xsl:call-template name="SELECT_HO_34_DISPLAY"/>  -->
		<!-- Error 2 end -->
		<!--  **************************************  OK ********************************************* -->
		<!-- HO-11 Expanded Replacement Coverage for Building -->
		<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY_DISPLAY" />
		<!-- HO-96 Increased Fire Dept. Service Charge -->
		<xsl:call-template name="HO_96_REPLACEMENT_COST_PERSONAL_PROPERTY" />
		<!-- HO-312 Business Property Increased Limits -->
		<xsl:call-template name="HO_312_BUSINESS_PROPERTY_INCREASED_LIMITS" />
		<!--HO-48 Other Structures - Increased Limits -->
		<xsl:call-template name="HO_48_OTHER_STRUCTURE_INCREASED_LIMITS" />
		<!-- HO-40 Other Structures - Rented to Others -->
		<xsl:call-template name="HO_40_OTHER_STRUCTURE_RENTED_TO_OTHERS" />
		<!-- HO-42 Other Structure With Incidental -->
		<xsl:call-template name="HO_42_OTHER_STRUCTURE_WITH_INCIDENTAL" />
		<!-- HO-53 Increased Credit Card -->
		<xsl:call-template name="HO_53_INCREASED_CREDIT_CARD" />
		<!-- Unscheduled Jewelry, Watches & Fur -->
		<xsl:call-template name="UNSCHEDULED_JEWELRY_ADDITIONAL" />
		<!-- Unscheduled Jewelry, Watches & Fur -->
		<xsl:call-template name="MONEY" />
		<!-- Securities -->
		<xsl:call-template name="SECURITIES" />
		<!-- Silverware, Goldware, Etc. -->
		<xsl:call-template name="SILVERWARE_GOLDWARE" />
		<!-- Firearms -->
		<xsl:call-template name="FIREARMS" />
		<!-- HO-277 Ordinance or Law coverage Forms -->
		<xsl:call-template name="ORDINANCE" />
		<!-- HO-455 Identity Fraud Expense Coverage -->
		<xsl:call-template name="HO_455_IDENTITY_FRAUD" />
		<!-- HO-315 Earthquake -->
		<xsl:call-template name="HO_315_EARTHQUAKE" />
		<!-- HO-327 Water Back-Up and Sump Pump Overflow -->
		<xsl:call-template name="HO_327_WATER_BACK_UP" />
		<!-- HO-33 Condo-Unit Owner Rental to Others. -->
		<!-- <xsl:call-template name="HO_33_CONDO_UNIT"/>	--> <!-- LEFT - this has to be modified -->
		<!-- MINE SUBSIDENCE COVERAGE -->
		<xsl:call-template name="HO_287_MINE_SUBSIDENCE_COVERAGE" />
		<!-- HO-9 Collapse From Sub-Surface Water -->
		<xsl:call-template name="HO_9_COLLAPSE_FROM_SUB_SURFACE_WATER" />
		<!-- HO-490 Specific Structure(s) Away From Premises -->
		<xsl:call-template name="HO_490_SPECIFIC_STRUCTURED" />
		<!-- HO-489 Other Structures (Repair Cost Coverage) -->
		<xsl:call-template name="HO_489_SPECIFIC_STRUCTURED" />
		<!-- primary Residence (1 or 2 Family) -->
		<xsl:call-template name="PRIMARY_RESIDENCE" />
		<!-- Residence Employees  -->
		<xsl:call-template name="RESIDENCE_EMPLOYEES" />
		<!-- Additional Premises-Occupied By Insured -->
		<xsl:call-template name="ADDITIONAL_PREMISES" />
		<!-- HO-40 Additional Premises Rented to Others - Residence Premises -->
		<xsl:call-template name="ADDITIONAL_RESIDENCE_PREMISES" />
		<!-- HO-70 Additional Premises Rented - Other Location - 1 Family -->
		<xsl:call-template name="ADDITIONAL_OTHER_LOCATION_PREMISES_FAMILY1" />
		<!-- HO-70 Additional Premises Rented - Other Location - 2 Family -->
		<xsl:call-template name="ADDITIONAL_OTHER_LOCATION_PREMISES_FAMILY2" />
		<!-- HO-42 Incidental Office Private School or Studio On Premises -->
		<xsl:call-template name="HO_42_ON_PREMISES" />
		<!-- Business Located in an Other Structure -->
		<xsl:call-template name="HO42_BUSINESS_OTHER_STRUCTURE_WITH_INCIDENTAL" />
		<!-- HO-42 Incidental Office Private School or Studio Instruction Only -->
		<xsl:call-template name="HO_42_INSTRUCTION_ONLY" />
		<!-- HO-200 Waterbed Liability -->
		<xsl:call-template name="HO_200_WATERBED_LIABILITY" />
		<!-- HO-43 Incidental Office Private School or Studio Off Premises -->
		<xsl:call-template name="HO_43_OFF_PREMISES" />
		<!-- HO-71 Business Pursuits:Class A  -->
		<xsl:call-template name="HO_71_CLASSA" />
		<!-- HO-71 Business Pursuits:Class B -->
		<xsl:call-template name="HO_71_CLASSB" />
		<!-- HO-71 Business Pursuits:Class C -->
		<xsl:call-template name="HO_71_CLASSC" />
		<!-- HO-71 Business Pursuits:Class D  -->
		<xsl:call-template name="HO_71_CLASSD" />
		<!-- HO-72 Farm Liability - Incidental Farming Residence Premises -->
		<xsl:call-template name="HO_72_INCIDENTAL_FARMING" />
		<!-- HO-73 Farm Liability - Owned By Insured Rented to Others -->
		<xsl:call-template name="HO_73_OTH_LOC_OPR_OTHERS" />
		<!-- HO-73 Farm Liability - Owned/Operated By Insured/Insured Employee -->
		<xsl:call-template name="HO_73_OTH_LOC_OPR_EMPL" />
		<!-- HO-82 Personal Injury -->
		<xsl:call-template name="HO_82_PERSONAL_INJURY" />
		<!-- Bicycle (HO-900) -->
		<xsl:call-template name="HO_900_BICYCLES_PREMIUM" />
		<!-- Cameras (Non-Professional) -->
		<xsl:call-template name="CAMERAS_PREMIUM" />
		<!-- Cellular Phones (HO-900) -->
		<xsl:call-template name="HO_900_CELLULAR_PHONES_PREMIUM" />
		<!-- Furs -->
		<xsl:call-template name="FURS_PREMIUM" />
		<!-- Golf Equipment -->
		<xsl:call-template name="GOLF_EQUIPMENT_PREMIUM" />
		<!-- Guns -->
		<xsl:call-template name="GUNS_PREMIUM" />
		<!-- Jewelry -->
		<xsl:call-template name="JEWELRY_PREMIUM" />
		<!-- Musical (Non-Professional) -->
		<xsl:call-template name="MUSICAL_NON_PROFESSIONAL_PREMIUM" />
		<!-- Personal Computers (HO-214) -->
		<xsl:call-template name="PERSONAL_COMPUTERS_PREMIUM" />
		<!-- Silver -->
		<xsl:call-template name="SILVER_PREMIUM" />
		<!-- Stamps -->
		<xsl:call-template name="STAMPS_PREMIUM" />
		<!-- Rare Coins -->
		<xsl:call-template name="RARE_COINS_PREMIUM" />
		<!-- Fine Arts - Without Breakage -->
		<xsl:call-template name="FINE_ARTS_WITHOUT_BREAKAGE_PREMIUM" />
		<!-- Fine Arts - With Breakage -->
		<xsl:call-template name="FINE_ARTS_BREAKAGE_PREMIUM" />
		<!-- Property Expense Fee -->
		<xsl:call-template name="PROPFEE" />
		<!-- HO-32 Unit Owners Coverage A Special Coverage -->
		<xsl:call-template name="HO32_UNIT_OWNER" />
		<!-- HO-50 Personal Property Away From Premises  -->
		<xsl:call-template name="HO50_PERSONAL_PROPERTY_AWAY_PREMISES" />
		<!-- Building Additions and Alterations (HO-51)  -->
		<xsl:call-template name="HO51_BULDING_ALTER" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--								Base Premium Template(START)								  -->
	<!--								  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="DEWLLING-MAIN">
		<xsl:variable name="TERCODES" select="TERRITORYCODES" />
		<xsl:variable name="GROUP_ID" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES[@TID=$TERCODES]/@GROUPID" />
		<xsl:if test="normalize-space($GROUP_ID) = ''">
			<tr>
				<td class="midcolora">
					<li> Group ID not found  (Path - "FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES[@TID=$TERCODES]/@GROUPID") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="F_CODE" select="FORM_CODE" />
		<xsl:variable name="FORMGROUP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING']/ATTRIBUTES[@FORMID=$F_CODE]/@GROUPID" />
		<xsl:if test="normalize-space($FORMGROUP) = ''">
			<tr>
				<td class="midcolora">
					<li> Form Group not found  (Path - "FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING']/ATTRIBUTES[@FORMID=$F_CODE]/@GROUPID") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_NO_OF_UNITS_4" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='NOOFDWELLINGUNITS']/NODE[@ID='NOOFUNITS']/ATTRIBUTES/@NO_OF_UNITS_LESS_THAN" />
		<xsl:if test="normalize-space($VAR_NO_OF_UNITS_4) = ''">
			<tr>
				<td class="midcolora">
					<li> no of units less than not found  (Path - "FACTOR[@ID='NOOFDWELLINGUNITS']/NODE[@ID='NOOFUNITS']/ATTRIBUTES/@NO_OF_UNITS_LESS_THAN") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_NO_OF_UNITS_5" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='NOOFDWELLINGUNITS']/NODE[@ID='NOOFUNITS']/ATTRIBUTES/@NO_OF_UNITS_GREATER_THAN" />
		<xsl:if test="normalize-space($VAR_NO_OF_UNITS_5) = ''">
			<tr>
				<td class="midcolora">
					<li> no of units greater than not found  (Path - "FACTOR[@ID='NOOFDWELLINGUNITS']/NODE[@ID='NOOFUNITS']/ATTRIBUTES/@NO_OF_UNITS_GREATER_THAN") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="PCODE1" select="PRODUCTNAME" />
		<xsl:variable name="PCODE">
			<xsl:choose>
				<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 and NUMBEROFUNITS &lt;= $VAR_NO_OF_UNITS_4 ">HO-4</xsl:when>
				<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 and NUMBEROFUNITS &gt;= $VAR_NO_OF_UNITS_5 ">HO-4 CO II</xsl:when>
				<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO6 and NUMBEROFUNITS &lt;= $VAR_NO_OF_UNITS_4 ">HO-6</xsl:when>
				<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO6 and NUMBEROFUNITS &gt;= $VAR_NO_OF_UNITS_5 ">HO-6 CO II</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="PRODUCTNAME" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_PRMGROUP_CODE">
			<xsl:choose>
				<xsl:when test="normalize-space($FORMGROUP) = '1'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1" />
				</xsl:when>
				<xsl:when test="normalize-space($FORMGROUP) = '2'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2" />
				</xsl:when>
				<xsl:when test="normalize-space($FORMGROUP) = '3'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3" />
				</xsl:when>
				<xsl:when test="normalize-space($FORMGROUP) = '4'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4" />
				</xsl:when>
				<xsl:when test="normalize-space($FORMGROUP) = '5'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5" />
				</xsl:when>
				<xsl:when test="normalize-space($FORMGROUP) = '6'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6" />
				</xsl:when>
				<xsl:when test="normalize-space($FORMGROUP) = '7'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7" />
				</xsl:when>
				<xsl:when test="normalize-space($FORMGROUP) = '8'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code8" />
				</xsl:when>
				<xsl:when test="normalize-space($FORMGROUP) = '9'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code9" />
				</xsl:when>
				<xsl:when test="normalize-space($FORMGROUP) = '10'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code10" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="normalize-space($VAR_PRMGROUP_CODE) = ''">
			<tr>
				<td class="midcolora">
					<li> Premium Group Code not found  (Path - "FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:value-of select="$VAR_PRMGROUP_CODE" />
	</xsl:template>
	<!-- For INDINA -->
	<xsl:template name="DEWLLING_MAIN_INDIANA">
		<!--<xsl:value-of select ="TERRITORYCODES"></xsl:value-of>-->
		<xsl:variable name="TERCODES" select="TERRITORYCODES" />
		<xsl:variable name="GROUP_ID" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES [@TID = $TERCODES]/@GROUPID" />
		<xsl:if test="normalize-space($GROUP_ID) = ''">
			<tr>
				<td class="midcolora">
					<li> Group ID not found  (Path - "FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES[@TID=$TERCODES]/@GROUPID") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="F_CODE" select="FORM_CODE" />
		<xsl:variable name="FORMGROUP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING']/ATTRIBUTES [@FORMID = $F_CODE]/@GROUPID" />
		<xsl:if test="normalize-space($FORMGROUP) = ''">
			<tr>
				<td class="midcolora">
					<li> Form Group not found  (Path - "FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING']/ATTRIBUTES [@FORMID = $F_CODE]/@GROUPID") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_NO_OF_UNITS_4" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='NOOFDWELLINGUNITS']/NODE[@ID='NOOFUNITS']/ATTRIBUTES/@NO_OF_UNITS_LESS_THAN" />
		<xsl:if test="normalize-space($VAR_NO_OF_UNITS_4) = ''">
			<tr>
				<td class="midcolora">
					<li> no of units less than not found  (Path - "FACTOR[@ID='NOOFDWELLINGUNITS']/NODE[@ID='NOOFUNITS']/ATTRIBUTES/@NO_OF_UNITS_LESS_THAN") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_NO_OF_UNITS_5" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='NOOFDWELLINGUNITS']/NODE[@ID='NOOFUNITS']/ATTRIBUTES/@NO_OF_UNITS_GREATER_THAN" />
		<xsl:if test="normalize-space($VAR_NO_OF_UNITS_5) = ''">
			<tr>
				<td class="midcolora">
					<li> no of units greater than not found  (Path - "FACTOR[@ID='NOOFDWELLINGUNITS']/NODE[@ID='NOOFUNITS']/ATTRIBUTES/@NO_OF_UNITS_GREATER_THAN") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="PCODE">
			<xsl:choose>
				<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 and NUMBEROFUNITS &lt;= $VAR_NO_OF_UNITS_4">HO-4 CO I</xsl:when>
				<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 and NUMBEROFUNITS &gt;= $VAR_NO_OF_UNITS_5">HO-4 CO II</xsl:when>
				<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO6 and NUMBEROFUNITS &lt;= $VAR_NO_OF_UNITS_4">HO-6 CO I</xsl:when>
				<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO6 and NUMBEROFUNITS &gt;= $VAR_NO_OF_UNITS_5">HO-6 CO II</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="PRODUCTNAME" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_PRMGROUP_CODE">
			<xsl:choose>
				<xsl:when test="$FORMGROUP = '1'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1" />
				</xsl:when>
				<xsl:when test="$FORMGROUP = '2'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2" />
				</xsl:when>
				<xsl:when test="$FORMGROUP = '3'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3" />
				</xsl:when>
				<xsl:when test="$FORMGROUP = '4'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4" />
				</xsl:when>
				<xsl:when test="$FORMGROUP = '5'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5" />
				</xsl:when>
				<xsl:when test="$FORMGROUP = '6'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6" />
				</xsl:when>
				<xsl:when test="$FORMGROUP = '7'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="normalize-space($VAR_PRMGROUP_CODE) = ''">
			<tr>
				<td class="midcolora">
					<li> Premium Group Code not found  (Path - "FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:value-of select="$VAR_PRMGROUP_CODE" />
	</xsl:template>
	<!-- INDIANA HO-4 Reguler-->
	<xsl:template name="DEWLLING_MAIN_INDIANA_HO_4">
		<xsl:variable name="TERCODES" select="TERRITORYCODES" />
		<xsl:variable name="GROUP_ID" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES [@TID = $TERCODES]/@GROUPID" />
		<xsl:if test="normalize-space($GROUP_ID) = ''">
			<tr>
				<td class="midcolora">
					<li> Group ID not found  (Path - "FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES[@TID=$TERCODES]/@GROUPID") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="F_CODE" select="FORM_CODE" />
		<xsl:variable name="FORMGROUP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING']/ATTRIBUTES [@FORMID = $F_CODE]/@GROUPID" />
		<xsl:if test="normalize-space($FORMGROUP) = ''">
			<tr>
				<td class="midcolora">
					<li> Form Group not found  (Path - "FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING']/ATTRIBUTES [@FORMID = $F_CODE]/@GROUPID") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="PCODE" select="PRODUCTNAME" />
		<xsl:variable name="VAR_PRMGROUP_CODE">
			<xsl:choose>
				<xsl:when test="$FORMGROUP = '1'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1" />
				</xsl:when>
				<xsl:when test="$FORMGROUP = '2'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2" />
				</xsl:when>
				<xsl:when test="$FORMGROUP = '3'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3" />
				</xsl:when>
				<xsl:when test="$FORMGROUP = '4'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4" />
				</xsl:when>
				<xsl:when test="$FORMGROUP = '5'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5" />
				</xsl:when>
				<xsl:when test="$FORMGROUP = '6'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='HO-P']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="normalize-space($VAR_PRMGROUP_CODE) = ''">
			<tr>
				<td class="midcolora">
					<li> Premium Group Code not found  (Path - "FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:value-of select="$VAR_PRMGROUP_CODE" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--								Base Premium Template(END)									  -->
	<!--								  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		     						Template For COVERAGE VALUE  (START)					  -->
	<!--			    					  FOR MICHIGAN and INDIANA								  -->
	<!-- ============================================================================================ -->
	<xsl:template name="C-VALUE">
		<!-- Divide by 1000 because product factor master has values in 1000 -->
		<xsl:variable name="CVALUE" select='format-number((DWELLING_LIMITS div 1000), "#")' /> <!-- converting to integer. -->
		<!-- Set the Group -->
		<xsl:variable name="TERCODES" select="TERRITORYCODES" />
		<xsl:variable name="VAR_PRODUCTNAME" select="PRODUCTNAME" />
		<xsl:variable name="F_CODE" select="FORM_CODE" />
		<xsl:variable name="FORMGROUP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING']/ATTRIBUTES [@FORMID = $F_CODE]/@GROUPID" />
		<xsl:if test="normalize-space($FORMGROUP) = ''">
			<tr>
				<td class="midcolora">
					<li> Form group not found  (Path - "FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING']/ATTRIBUTES [@FORMID = $F_CODE]/@GROUPID") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_GROUP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COVERAGE_GROUP']/NODE[@ID='COVERAGEGROUP']/ATTRIBUTES [@FORMTYPE=$VAR_PRODUCTNAME and @FORMGROUP=$FORMGROUP]/@COVERAGEGROUP" />
		<xsl:if test="normalize-space($VAR_GROUP) = ''">
			<tr>
				<td class="midcolora">
					<li> Coverage group  not found  (Path - "FACTOR[@ID='COVERAGE_GROUP']/NODE[@ID='COVERAGEGROUP']/ATTRIBUTES [@FORMTYPE=$VAR_PRODUCTNAME and @FORMGROUP=$FORMGROUP]/@COVERAGEGROUP") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- get the max value of coverages in the database -->
		<xsl:variable name="VAR_MAX_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/@MAXVALUE" />
		<xsl:if test="normalize-space($VAR_MAX_VALUE) = ''">
			<tr>
				<td class="midcolora">
					<li> Max base value not found  (Path - "FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/@MAXVALUE") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:choose>
			<xsl:when test="$CVALUE &gt; $VAR_MAX_VALUE">
				<!-- For each Additional amount-->
				<xsl:variable name="VAR_ADDITIONAL_VALUE">
					<xsl:choose>
						<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/ATTRIBUTES[@CovC = 'ADDITIONAL']/@AMOUNT" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/ATTRIBUTES[@CovA = 'ADDITIONAL']/@AMOUNT" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_ADDITIONAL_VALUE) = ''">
					<tr>
						<td class="midcolora">
							<li> Additional value (for CovA or CovC )not found  (Path - "FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/ATTRIBUTES[@CovA = 'ADDITIONAL']/@AMOUNT") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!-- value -->
				<xsl:variable name="VAR_ADDITIONAL_FACTOR">
					<xsl:choose>
						<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/ATTRIBUTES[@CovC = 'ADDITIONAL']/@Factor" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/ATTRIBUTES[@CovA = 'ADDITIONAL']/@Factor" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_ADDITIONAL_FACTOR) = ''">
					<tr>
						<td class="midcolora">
							<li> Additional Factor (for CovA or CovC )not found  (Path - "FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/ATTRIBUTES[@CovA = 'ADDITIONAL']/@Factor") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!-- value for the max factor -->
				<xsl:variable name="VAR_MAX_VALUE_FACTOR">
					<xsl:choose>
						<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP=$VAR_GROUP]/ATTRIBUTES[@CovC=$VAR_MAX_VALUE]/@Factor" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP=$VAR_GROUP]/ATTRIBUTES[@CovA=$VAR_MAX_VALUE]/@Factor" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_MAX_VALUE_FACTOR) = ''">
					<tr>
						<td class="midcolora">
							<li> Max Value Factor (for CovA or CovC )not found  (Path - "FACTOR[@ID='BASE']/NODE[@GROUP=$VAR_GROUP]/ATTRIBUTES[@CovC=$VAR_MAX_VALUE]/@Factor") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!-- Calculations 
					e.g : factor for 400 + ((difference in the cov and the max value)/additional amount)* (factor for additional)-->
				<xsl:value-of select="$VAR_MAX_VALUE_FACTOR + ((($CVALUE - $VAR_MAX_VALUE) div $VAR_ADDITIONAL_VALUE) * $VAR_ADDITIONAL_FACTOR)" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR_BASE_FACTOR">
					<xsl:choose>
						<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/ATTRIBUTES[@CovC =$CVALUE]/@Factor" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/ATTRIBUTES[@CovA =$CVALUE]/@Factor" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_BASE_FACTOR) = ''">
					<tr>
						<td class="midcolora">
							<li> Base Factor (for CovA or CovC )not found  (Path - "FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/ATTRIBUTES[@CovA =$CVALUE]/@Factor") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:value-of select="$VAR_BASE_FACTOR" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Coverage value for HO-3 INDIANA -->
	<xsl:template name="C_VALUE_INDIANA">
		<xsl:variable name="CVALUE" select="COVERAGEVALUE" />
		<xsl:variable name="CVALUE_FACTOR">
			<xsl:choose>
				<xsl:when test="FORM_CODE = '01F'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
				</xsl:when>
				<xsl:when test="FORM_CODE = '02F'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
				</xsl:when>
				<xsl:when test="FORM_CODE = '03F'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
				</xsl:when>
				<xsl:when test="FORM_CODE = '04F'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
				</xsl:when>
				<xsl:when test="FORM_CODE = '05F'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
				</xsl:when>
				<xsl:when test="FORM_CODE = '06F'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
				</xsl:when>
				<xsl:when test="FORM_CODE = '07F'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
				</xsl:when>
				<xsl:when test="FORM_CODE = '08F'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
				</xsl:when>
				<xsl:when test="FORM_CODE = '09F'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
				</xsl:when>
				<xsl:when test="FORM_CODE = '10F'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
				</xsl:when>
				<xsl:when test="FORM_CODE = '01M'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
				</xsl:when>
				<xsl:when test="FORM_CODE = '02M'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
				</xsl:when>
				<xsl:when test="FORM_CODE = '3M'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
				</xsl:when>
				<xsl:when test="FORM_CODE = '04M'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
				</xsl:when>
				<xsl:when test="FORM_CODE = '05M'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
				</xsl:when>
				<xsl:when test="FORM_CODE = '06M'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
				</xsl:when>
				<xsl:when test="FORM_CODE = '07M'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
				</xsl:when>
				<xsl:when test="FORM_CODE = '08M'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
				</xsl:when>
				<xsl:when test="FORM_CODE = '09M'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
				</xsl:when>
				<xsl:when test="FORM_CODE = '10M'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $CVALUE]/@Factor" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="normalize-space($CVALUE_FACTOR) = ''">
			<tr>
				<td class="midcolora">
					<li> coverage value factor Factor not found  (Path - "FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $CVALUE]/@Factor") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:value-of select="$CVALUE_FACTOR" />
	</xsl:template>
	<!-- Coverage Value For HO-4 and HO-6 (Regular) -->
	<xsl:template name="C_VALUE_HO_4_AND_6">
		<xsl:variable name="CVALUE" select="COVERAGEVALUE" />
		<!-- Set the Group -->
		<xsl:variable name="VAR_GROUP">G1</xsl:variable>
		<!-- get the max value of coverages in the database -->
		<xsl:variable name="VAR_MAX_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/@MAXVALUE" />
		<xsl:if test="normalize-space($VAR_MAX_VALUE) = ''">
			<tr>
				<td class="midcolora">
					<li> Max coverage value not found  (Path - "FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/@MAXVALUE") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:choose>
			<xsl:when test="COVERAGEVALUE &gt; $VAR_MAX_VALUE">
				<xsl:variable name="VAR_ADDITIONAL_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/ATTRIBUTES[@CovA = 'ADDITIONAL']/@AMOUNT" />
				<xsl:if test="normalize-space($VAR_ADDITIONAL_VALUE) = ''">
					<tr>
						<td class="midcolora">
							<li> Coverage A additional amount not found  (Path - "FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/ATTRIBUTES[@CovA = 'ADDITIONAL']/@AMOUNT") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_ADDITIONAL_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/ATTRIBUTES[@CovA = 'ADDITIONAL']/@Factor" />
				<xsl:if test="normalize-space($VAR_ADDITIONAL_FACTOR) = ''">
					<tr>
						<td class="midcolora">
							<li> Coverage A additional factor not found  (Path - "FACTOR[@ID='BASE']/NODE[@GROUP = $VAR_GROUP]/ATTRIBUTES[@CovA = 'ADDITIONAL']/@Factor") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!-- value for the max factor -->
				<xsl:variable name="VAR_MAX_VALUE_FACTOR">
					<xsl:choose>
						<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $VAR_MAX_VALUE]/@Factor" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G2']/ATTRIBUTES[@CovA = $VAR_MAX_VALUE]/@Factor" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_MAX_VALUE_FACTOR) = ''">
					<tr>
						<td class="midcolora">
							<li> Max value factor (Group G1 or G2) not found  (Path - "FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $VAR_MAX_VALUE]/@Factor") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!-- Calculations 
					e.g : factor for 400 + ((difference in the cov and the max value)/additional amount)* (factor for additional)-->
				<xsl:value-of select="$VAR_MAX_VALUE_FACTOR + ((($CVALUE - $VAR_MAX_VALUE) div $VAR_ADDITIONAL_VALUE) * $VAR_ADDITIONAL_FACTOR)" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR_CVALUE_FACTOR">
					<xsl:choose>
						<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA =$CVALUE]/@Factor" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA =$CVALUE]/@Factor" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_CVALUE_FACTOR) = ''">
					<tr>
						<td class="midcolora">
							<li> Coverage value factor not found  (Path - "FACTOR[@ID='BASE']/NODE[@GROUP = 'G1']/ATTRIBUTES[@CovA = $VAR_MAX_VALUE]/@Factor") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:value-of select="$VAR_CVALUE_FACTOR" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		     						Template For COVERAGE VALUE  (END)						  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--						Templates For Coverage A,C,D  (START)							  -->
	<!-- ============================================================================================ -->
	<!-- Coverage A Building Property Increased Limits for HO6 -->
	<xsl:template name="COVERAGE_A_PREMIUM_FOR_HO6">
		<xsl:variable name="PCOVERAGE_A_BLDG_PROPERTY_VALUE" select="PERSONALPROPERTYINCREASEDLIMITADDITIONAL"></xsl:variable>
		<xsl:variable name="PCOVERAGE_A_BLDG_PROPERTY_ADDITIONAL_COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='ADDITIONAL_BULDING_PROPERTY']/ATTRIBUTES/@COST"></xsl:variable>
		<xsl:if test="normalize-space($PCOVERAGE_A_BLDG_PROPERTY_ADDITIONAL_COST) = ''">
			<tr>
				<td class="midcolora">
					<li> coverage A bulding property additional cost not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='ADDITIONAL_BULDING_PROPERTY']/ATTRIBUTES/@COST") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="PCOVERAGE_A_BLDG_PROPERTY_RATE_PER_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='ADDITIONAL_BULDING_PROPERTY']/ATTRIBUTES/@INCREASEDAMOUNT"></xsl:variable>
		<xsl:if test="normalize-space($PCOVERAGE_A_BLDG_PROPERTY_RATE_PER_VALUE) = ''">
			<tr>
				<td class="midcolora">
					<li> coverage A bulding property rate per value not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='ADDITIONAL_BULDING_PROPERTY']/ATTRIBUTES/@INCREASEDAMOUNT") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--	<xsl:value-of select="round(($PCOVERAGE_A_BLDG_PROPERTY_VALUE div $PCOVERAGE_A_BLDG_PROPERTY_RATE_PER_VALUE) * $PCOVERAGE_A_BLDG_PROPERTY_ADDITIONAL_COST)"/> -->
	</xsl:template>
	<!--Coverage C - Personal Property - Limit And Deductible -->
	<xsl:template name="COVERAGE_C_PREMIUM">
		<xsl:variable name="PNAME" select="PRODUCTNAME"></xsl:variable>
		<xsl:variable name="PCOVERAGE_C_ADDITIONAL_VALUE" select="PERSONALPROPERTYINCREASEDLIMITADDITIONAL"></xsl:variable>
		<xsl:variable name="PCOVERAGE_C_ADDITIONAL_COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = $PNAME]/@COST"></xsl:variable>
		<xsl:if test="normalize-space($PCOVERAGE_C_ADDITIONAL_COST) = ''">
			<tr>
				<td class="midcolora">
					<li> Coverage C additional cost  not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = $PNAME]/@COST") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="PCOVERAGE_C_ADDITIONAL_INCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = $PNAME]/@INCREASEDAMOUNT"></xsl:variable>
		<xsl:if test="normalize-space($PCOVERAGE_C_ADDITIONAL_INCREASEDAMOUNT) = ''">
			<tr>
				<td class="midcolora">
					<li> Coverage C additional Amount not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = $PNAME]/@INCREASEDAMOUNT") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--
	<xsl:choose>
		<xsl:when test="$PCOVERAGE_C_ADDITIONAL_INCREASEDAMOUNT &gt; 0.00">
			<xsl:value-of select ="round($PCOVERAGE_C_ADDITIONAL_COST * ($PCOVERAGE_C_ADDITIONAL_VALUE div $PCOVERAGE_C_ADDITIONAL_INCREASEDAMOUNT))"/> 	
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
	-->
	</xsl:template>
	<!-- Coverage D - Loss Of Use - Limit And Deductible-->
	<xsl:template name="COVERAGE_D_PREMIUM">
		<xsl:variable name="PCOVERAGE_D_ADDITIONAL_VALUE" select="ADDITIONALLIVINGEXPENSEADDITIONAL"></xsl:variable>
		<xsl:variable name="PCOVERAGE_D_ADDITIONAL_COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='ADDITIONAL_LIVING_EXPENSE']/ATTRIBUTES/@COST"></xsl:variable>
		<xsl:if test="normalize-space($PCOVERAGE_D_ADDITIONAL_COST) = ''">
			<tr>
				<td class="midcolora">
					<li> Coverage D additional Cost not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='ADDITIONAL_LIVING_EXPENSE']/ATTRIBUTES/@COST") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="PCOVERAGE_D_ADDITIONAL_INCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='ADDITIONAL_LIVING_EXPENSE']/ATTRIBUTES/@INCREASEDAMOUNT"></xsl:variable>
		<xsl:if test="normalize-space($PCOVERAGE_D_ADDITIONAL_INCREASEDAMOUNT) = ''">
			<tr>
				<td class="midcolora">
					<li> Coverage C additional Amount not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='ADDITIONAL_LIVING_EXPENSE']/ATTRIBUTES/@INCREASEDAMOUNT") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--
    <xsl:choose>
		<xsl:when test="$PCOVERAGE_D_ADDITIONAL_INCREASEDAMOUNT &gt; 0.00">
			  <xsl:value-of select ="round($PCOVERAGE_D_ADDITIONAL_COST * ($PCOVERAGE_D_ADDITIONAL_VALUE div $PCOVERAGE_D_ADDITIONAL_INCREASEDAMOUNT))"/>
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
	
	-->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--						Templates For Coverage A,C,D  (END)							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--						Templates for Insurance Score Credit, Display (START)				  -->
	<!-- ============================================================================================ -->
	<xsl:template name="INSURANCE_SCORE_CREDIT"> <!-- used in ADJUSTEDBASE_REGULAR_MICHIGAN_HO2 -->
		<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:choose>
				<xsl:when test="INSURANCESCORE &gt;= 0">
					<xsl:variable name="INS" select="INSURANCESCORE" />
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='CREDIT'  ]/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@FACTOR" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="normalize-space($VAR_INSURANCESCORE) = ''">
			<tr>
				<td class="midcolora">
					<li> Insurance score credit not found  (Path - "FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='CREDIT'  ]/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@FACTOR") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:value-of select="$VAR_INSURANCESCORE" />
	</xsl:template>
	<!-- For Display Only -->
	<xsl:template name="INSURANCE_SCORE_CREDIT_DISPLAY"> <!-- used in step 6 -->
		<xsl:variable name="INS" select="INSURANCESCORE" />
		<xsl:variable name="INS_RESULT">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='CREDIT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@CREDIT" />
		</xsl:variable>
		<xsl:if test="normalize-space($INS_RESULT) = ''">
			<tr>
				<td class="midcolora">
					<li> Insurance score credit not found  (Path - "FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='CREDIT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@CREDIT") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
	</xsl:template>
	<xsl:template name="INSURANCE_SCORE_DISPLAY"> <!-- used in step 6 , with text-->
		<xsl:variable name="INS" select="INSURANCESCORE" />
		<xsl:variable name="INS_RESULT">
			<xsl:variable name="VAR_INS_RESULT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='CREDIT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@CREDIT" />
			<xsl:if test="normalize-space($VAR_INS_RESULT) = ''">
				<tr>
					<td class="midcolora">
						<li> Insurance Score credit not found  (Path - "FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='CREDIT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@CREDIT") </li>
					</td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$INS_RESULT &gt; 0 and $INS_RESULT !='' ">
				<xsl:value-of select="INSURANCESCORE" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- INSURANCE SCORE Credit in percent for display -->
	<xsl:template name="INSURANCESCORE_PERCENT">
		<xsl:variable name="INS" select="INSURANCESCORE" />
		<xsl:variable name="INS_RESULT">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='CREDIT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@CREDIT" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$INS_RESULT &lt;=0"></xsl:when>
			<xsl:when test="$INS_RESULT &gt;0">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='CREDIT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@CREDIT" />%
		</xsl:when>
			<xsl:otherwise></xsl:otherwise> <!-- pass blank in this otherwise -->
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--						Templates for Insurance Score Credit, Display (END)					  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--						Templates for Term Factor  (START)									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="TERM_FACTOR">
		<xsl:variable name="VAR_TERM" select="TERMFACTOR" />
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERMFACTOR']/NODE[@ID ='TERM_FACTOR']/ATTRIBUTES[@MONTH=$VAR_TERM]/@FACTOR" />
		</xsl:variable>
		<xsl:if test="normalize-space($VAR_TERMFACTOR) = ''">
			<tr>
				<td class="midcolora">
					<li> Term factor not found  (Path - "FACTOR[@ID='TERMFACTOR']/NODE[@ID ='TERM_FACTOR']/ATTRIBUTES[@MONTH=$VAR_TERM]/@FACTOR") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--						Templates for Term Factor  (END)									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Under Construction [Factor,Credit,Display]  (START)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="DWELLING_UNDER_CONSTRUCTION">
		<xsl:choose>
			<xsl:when test="CONSTRUCTIONCREDIT = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='DWELLINGUNDERCONSTRUCTION']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DWELLING_UNDER_CONSTRUCTION_CREDIT">
		<xsl:choose>
			<xsl:when test="CONSTRUCTIONCREDIT = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='DWELLINGUNDERCONSTRUCTION']/ATTRIBUTES/@CREDIT" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Under Construction [Factor,Credit,Display]  (END)			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--						Templates for Discount - Deductible Factor (START)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="DFACTOR">
		<xsl:variable name="DEDUCTIBLEAMT" select="DEDUCTIBLE" />
		<xsl:variable name="VAR_DFACTOR">
			<xsl:choose>
				<xsl:when test="normalize-space($DEDUCTIBLEAMT) != ''">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLE']/NODE[@ID ='OPTIONALDEDUCTIBLE']/ATTRIBUTES[@AMOUNT=$DEDUCTIBLEAMT]/@FACTOR" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="normalize-space($VAR_DFACTOR) = ''">
			<tr>
				<td class="midcolora">
					<li> Dedutible factor not found  (Path - "FACTOR[@ID='DEDUCTIBLE']/NODE[@ID ='OPTIONALDEDUCTIBLE']/ATTRIBUTES[@AMOUNT=$DEDUCTIBLEAMT]/@FACTOR") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:value-of select="$VAR_DFACTOR" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--						Templates for Discount - Deductible Factor (END)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Experince Factor [Factor,Credit,Display]  (SATRT)			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="EXPERIENCEFACTOR">
		<xsl:variable name="VAR_MINAGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MATURITY']/@MINAGE" />
		<xsl:choose>
			<xsl:when test="EXPERIENCE &gt; $VAR_MINAGE ">
				<xsl:variable name="EF" select="EXPERIENCE" />
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MATURITY']/ATTRIBUTES[@MINAGE &lt;= $EF and @MAXAGE &gt;= $EF]/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Experince Credit -->
	<xsl:template name="EXPERIENCECREDIT">
		<xsl:variable name="VAR_MINAGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MATURITY']/@MINAGE" />
		<xsl:if test="normalize-space($VAR_MINAGE) = ''">
			<tr>
				<td class="midcolora">
					<li> Min Age for Experience credit not found  (Path - "FACTOR[@ID='CREDITS']/NODE[@ID ='MATURITY']/@MINAGE" </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:choose>
			<xsl:when test="EXPERIENCE &gt; $VAR_MINAGE ">
				<xsl:variable name="EF" select="EXPERIENCE" />
				<xsl:variable name="VAR_EXP_CREDIT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MATURITY']/ATTRIBUTES[@MINAGE &lt;= $EF and @MAXAGE &gt;= $EF]/@CREDIT" />
				<xsl:if test="normalize-space($VAR_EXP_CREDIT) = ''">
					<tr>
						<td class="midcolora">
							<li> Experience credit not found  (Path - "FACTOR[@ID='CREDITS']/NODE[@ID ='MATURITY']/ATTRIBUTES[@MINAGE &lt;= $EF and @MAXAGE &gt;= $EF]/@CREDIT") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For Display Only -->
	<xsl:template name="EXPERIENCEFACTOR_DISPLAY">
		<xsl:variable name="VAR_MINAGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MATURITY']/@MINAGE" />
		<xsl:if test="normalize-space($VAR_MINAGE) = ''">
			<tr>
				<td class="midcolora">
					<li> Min age for experience factor not found  (Path - "FACTOR[@ID='CREDITS']/NODE[@ID ='MATURITY']/@MINAGE") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Experince Factor [Factor,Credit,Display]  (END)			  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Age Of Home Factor  [Factor,Credit,Display]  (SATRT)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="AGEOFHOME_FACTOR">
		<xsl:variable name="VAR_MAXAGEOFHOME" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/@MAXAGEOFHOME" />
		<xsl:if test="normalize-space($VAR_MAXAGEOFHOME) = ''">
			<tr>
				<td class="midcolora">
					<li> Max Age of home not found  (Path - "FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/@MAXAGEOFHOME") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
		<xsl:if test="normalize-space($VAR_MAXCREDIT_FACTOR) = ''">
			<tr>
				<td class="midcolora">
					<li> Age of home credit factor not found  (Path - "FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:choose>
			<xsl:when test="AGEOFHOME &gt; $VAR_MAXAGEOFHOME">1.00</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="CONSTRUCTIONCREDIT = 'N'">
						<xsl:variable name="AGE_OF_HOME" select="AGEOFHOME" />
						<xsl:variable name="VAR_AGE_OF_HOME_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/ATTRIBUTES[@AGE = $AGE_OF_HOME]/@FACTOR" />
						<xsl:if test="normalize-space($VAR_AGE_OF_HOME_FACTOR) = ''">
							<tr>
								<td class="midcolora">
									<li> Age of home credit factor not found  (Path - "FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR") </li>
								</td>
							</tr>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>
						<xsl:value-of select="$VAR_AGE_OF_HOME_FACTOR" />
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Age Of Home CREDIT -->
	<xsl:template name="AGEOFHOME_CREDIT">
		<xsl:variable name="VAR_MAXAGEOFHOME" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/@MAXAGEOFHOME" />
		<xsl:choose>
			<xsl:when test="AGEOFHOME &gt; $VAR_MAXAGEOFHOME">1.00</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="CONSTRUCTIONCREDIT = 'N'">
						<xsl:variable name="AGE_OF_HOME" select="AGEOFHOME" />
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/ATTRIBUTES[@AGE = $AGE_OF_HOME]/@CREDIT" />
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Age Of Home Display -->
	<xsl:template name="AGEOFHOME_DISPLAY">
		<xsl:variable name="VAR_MAXAGEOFHOME" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/@MAXAGEOFHOME" />
		<xsl:if test="normalize-space($VAR_MAXAGEOFHOME) = ''">
			<tr>
				<td class="midcolora">
					<li> Max age of home not found  (Path - "FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/@MAXAGEOFHOME") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--
	<xsl:choose>
		<xsl:when test = "AGEOFHOME &gt; $VAR_MAXAGEOFHOME">0.00</xsl:when>
		<xsl:otherwise>
			<xsl:choose>
				<xsl:when test = "CONSTRUCTIONCREDIT = 'N'">Applied</xsl:when>
				<xsl:otherwise>0.00</xsl:otherwise>
			</xsl:choose>
		</xsl:otherwise>
	</xsl:choose>
	-->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Age Of Home   [Factor,Credit,Display]  (END)				  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Protective Device   [Factor,Credit,Display]  (SATRT)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="PROTECTIVEDEVICE_DISPLAY">
		<xsl:variable name="IS_CALCULATEABLE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$IS_CALCULATEABLE = 1.00"></xsl:when>
			<xsl:otherwise>
				<xsl:variable name="PROTECTIVE_DEVICE_VALUE">
					<xsl:variable name="VAR1">
						<xsl:call-template name="DIRECTBURGLERANDFIRE" />
					</xsl:variable>
					<xsl:variable name="VAR2">
						<xsl:call-template name="DIRECTFIREANDPOLICE" />
					</xsl:variable>
					<xsl:variable name="VAR3">
						<xsl:call-template name="LOCALFIREGASALARM" />
					</xsl:variable>
					<!--	<xsl:value-of select="$VAR1 + $VAR2 + $VAR3"/>  -->
				</xsl:variable>
				<!--
			<xsl:choose> 
				<xsl:when test="$PROTECTIVE_DEVICE_VALUE = 0.00">0</xsl:when>
				<xsl:otherwise>
					<xsl:choose> 
						<xsl:when test="$PROTECTIVE_DEVICE_VALUE &gt; 0.00">'Applied'</xsl:when>
						<xsl:otherwise>'Applied'</xsl:otherwise>
					</xsl:choose>
				</xsl:otherwise>
			</xsl:choose>
			-->
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PROTECTIVEDEVICE"> <!-- Returns a fraction value. factor and not credit percentage -->
		<xsl:choose>
			<xsl:when test="CONSTRUCTIONCREDIT = 'N'"> <!-- Not applicable when dwelling is under construction -->
				<xsl:variable name="VAR1">
					<xsl:call-template name="DIRECTBURGLERANDFIRE" />
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="DIRECTFIREANDPOLICE" />
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="LOCALFIREGASALARM" />
				</xsl:variable>
				<xsl:variable name="PROTECTIVE_DEVICE_VALUE">
					<xsl:value-of select="$VAR1+$VAR2+$VAR3" />
				</xsl:variable>
				<!-- Fetch Protection Device credit limit-->
				<xsl:variable name="VAR_MAXCREDIT_PROCDEVICE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='PROTECTIVEDEVICES']/@MAXCREDIT" />
				<xsl:choose>
					<xsl:when test="$PROTECTIVE_DEVICE_VALUE = 0">1.00</xsl:when>
					<!-- Max credit allowed is 15%. We are sending factor therefore (100 - 15)/100 -->
					<xsl:when test="$PROTECTIVE_DEVICE_VALUE &gt; $VAR_MAXCREDIT_PROCDEVICE">
						<xsl:value-of select="((100.00 - $VAR_MAXCREDIT_PROCDEVICE) div 100.00)" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="(100 - $PROTECTIVE_DEVICE_VALUE) div 100.00" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Central Stations Burglary & Fire Alarm System -->
	<xsl:template name="DIRECTBURGLERANDFIRE">
		<xsl:choose>
			<xsl:when test="BURGLAR = 'Y' and CENTRAL_FIRE = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='CSBF']/@CREDIT" />
			</xsl:when>
			<xsl:when test="BURGLAR = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='CSB']/@CREDIT" />
			</xsl:when>
			<xsl:when test="CENTRAL_FIRE = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='CSF']/@CREDIT" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Direct to Fire and Police -->
	<xsl:template name="DIRECTFIREANDPOLICE">
		<xsl:choose>
			<xsl:when test="BURGLER_ALERT_POLICE = 'Y' and FIRE_ALARM_FIREDEPT = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='DFP']/@CREDIT" />
			</xsl:when>
			<xsl:when test="BURGLER_ALERT_POLICE = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='DP']/@CREDIT" />
			</xsl:when>
			<xsl:when test="FIRE_ALARM_FIREDEPT = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='DF']/@CREDIT" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Local Fire or Local Gas Alarm -->
	<xsl:template name="LOCALFIREGASALARM">
		<xsl:variable name="VAR_MIN_NO_ALARMS" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/@MIN_N0_OF_ALARMS_FOR_CREDIT" />
		<xsl:choose>
			<xsl:when test="N0_LOCAL_ALARM = $VAR_MIN_NO_ALARMS">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='LFLG']/@CREDIT" />
			</xsl:when>
			<xsl:when test="N0_LOCAL_ALARM &gt;= $VAR_MIN_NO_ALARMS">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='LFA']/@CREDIT" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PROTECTIVEDEVICE_CREDIT">
		<xsl:variable name="VAR1">
			<xsl:call-template name="DIRECTBURGLERANDFIRE" />
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="DIRECTFIREANDPOLICE" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="LOCALFIREGASALARM" />
		</xsl:variable>
		<!--     <xsl:variable name="PROTECTIVE_DEVICE_VALUE" select ="$VAR1 + $VAR2 + $VAR3"/>  -->
		<xsl:variable name="VAR_MAXCREDIT_PROCDEVICE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='PROTECTIVEDEVICES']/@MAXCREDIT" />
		<xsl:if test="normalize-space($VAR_MAXCREDIT_PROCDEVICE) = ''">
			<tr>
				<td class="midcolora">
					<li> Max credit for protective Device not found  (Path - "FACTOR[@ID='CREDITS']/NODE[@ID ='PROTECTIVEDEVICES']/@MAXCREDIT") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--
	<xsl:choose> 
		<xsl:when test="$PROTECTIVE_DEVICE_VALUE &lt;= 0.00"></xsl:when>
		<xsl:otherwise> 
			<xsl:choose>
				<xsl:when test="$PROTECTIVE_DEVICE_VALUE &gt;= $VAR_MAXCREDIT_PROCDEVICE">
				<xsl:value-of select ="$VAR_MAXCREDIT_PROCDEVICE"/>
				</xsl:when>	
				<xsl:otherwise><xsl:value-of select ="$PROTECTIVE_DEVICE_VALUE"/></xsl:otherwise>
			</xsl:choose>
		</xsl:otherwise>
	</xsl:choose>	-->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Protective Device   [Factor,Credit,Display]  (END)			  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Multi policy Factor [Factor,Credit,Display]  (SATRT)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="MULTIPOLICY_FACTOR">
		<xsl:choose>
			<xsl:when test="MULTIPLEPOLICYFACTOR = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MULTIPOLICY']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Multipolicy CREDIT -->
	<xsl:template name="MULTIPOLICY_CREDIT">
		<xsl:choose>
			<xsl:when test="MULTIPLEPOLICYFACTOR = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MULTIPOLICY']/ATTRIBUTES/@CREDIT" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Multipolicy Factor [Factor,Credit,Display]  (END)			  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Valued Customer [Factor,Credit,Display]  (SATRT)			  -->
	<!-- The discount is applied per policy and per location at the beginning of the third policy 
	year renewal. Example policy issued 01/01/02 then at 01/01/03 no credit but after 
	two (2) full years and at the 01/01/04 renewal then apply credit.							  -->
	<!-- ============================================================================================ -->
	<!-- Rating xsl, NOTLOSSFREE or LOSSFREE node ='Y' implies tht the case is valid for Valued Customer wrt #yrs. -->
	<xsl:template name="VALUEDCUSTOMER">
		<xsl:choose>
			<xsl:when test="normalize-space(NEWBUSINESSFACTOR) ='N' or normalize-space(NEWBUSINESSFACTOR)='' or normalize-space(NEWBUSINESSFACTOR)='FALSE'">
				<xsl:choose>
					<xsl:when test="NOTLOSSFREE ='Y'">
						<xsl:variable name="VAR_NOTLOSSFREE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_WITH_CLAIMS']/ATTRIBUTES/@FACTOR" />
						<xsl:if test="normalize-space($VAR_NOTLOSSFREE) = ''">
							<tr>
								<td class="midcolora">
									<li> No Loss free factor not not found  (Path - "FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_WITH_CLAIMS']/ATTRIBUTES/@FACTOR") </li>
								</td>
							</tr>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>
						<xsl:value-of select="$VAR_NOTLOSSFREE" />
					</xsl:when>
					<xsl:when test="LOSSFREE ='Y'">
						<xsl:variable name="VAR_LOSSFREE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_WITHOUT_CLAIMS']/ATTRIBUTES/@FACTOR" />
						<xsl:if test="normalize-space($VAR_LOSSFREE) = ''">
							<tr>
								<td class="midcolora">
									<li> Loss free factor not not found  (Path - "FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_WITHOUT_CLAIMS']/ATTRIBUTES/@FACTOR") </li>
								</td>
							</tr>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>
						<xsl:value-of select="$VAR_LOSSFREE" />
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Valued Customer-Renewal Credit CREDIT -->
	<xsl:template name="VALUEDCUSTOMER_FACTOR">
		<xsl:choose>
			<xsl:when test="normalize-space(NEWBUSINESSFACTOR) ='N' or normalize-space(NEWBUSINESSFACTOR)='' or normalize-space(NEWBUSINESSFACTOR)='FALSE'">
				<xsl:choose>
					<xsl:when test="NOTLOSSFREE ='Y'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_WITH_CLAIMS']/ATTRIBUTES/@CREDIT" />
					</xsl:when>
					<xsl:when test="LOSSFREE ='Y'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_WITHOUT_CLAIMS']/ATTRIBUTES/@CREDIT" />
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Valued Customer [Factor,Credit,Display]  (END)				  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--Templates for Charge-Insured to 80% to 99% of Replacement Cost [Factor,Credit,Display](SATRT) -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- Charge - Prior Loss -->
	<xsl:template name="PRIOR_LOSS">
		<xsl:choose>
			<xsl:when test="PRIOR_LOSS_SURCHARGE = 'Y'">
				<xsl:variable name="VAR_PRIOR_LOSS" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='NEWBUSINESSPRIORLOSS']/ATTRIBUTES/@FACTOR" />
				<xsl:if test="normalize-space($VAR_PRIOR_LOSS) = ''">
					<tr>
						<td class="midcolora">
							<li> Prior loss factor not found  (Path - "FACTOR[@ID='SURCHARGE']/NODE[@ID='NEWBUSINESSPRIORLOSS']/ATTRIBUTES/@FACTOR") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:value-of select="$VAR_PRIOR_LOSS" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For Display In The Label -->
	<xsl:template name="PRIOR_LOSS_LABEL_VALUE">
		<xsl:choose>
			<xsl:when test="PRIOR_LOSS_SURCHARGE = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='NEWBUSINESSPRIORLOSS']/ATTRIBUTES/@SURCHARGE" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--Templates for Charge-Insured to 80% to 99% of Replacement Cost [Factor,Credit,Display](END)   -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for Charge - Wood Stove [Factor,Credit,Display](START)			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="WSTOVE">
		<xsl:choose>
			<xsl:when test="WOODSTOVE_SURCHARGE = 'Y'">
				<xsl:variable name="VAR_WSTOVE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='WOODSTOVE']/ATTRIBUTES/@FACTOR" />
				<xsl:if test="normalize-space($VAR_WSTOVE) = ''">
					<tr>
						<td class="midcolora">
							<li> Wood stove factor not found  (Path - "FACTOR[@ID='SURCHARGE']/NODE[@ID='WOODSTOVE']/ATTRIBUTES/@FACTOR") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:value-of select="$VAR_WSTOVE" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Woodstove Surcharge-->
	<xsl:template name="WSTOVE_SURCHARGE">
		<xsl:choose>
			<xsl:when test="WOODSTOVE_SURCHARGE = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='WOODSTOVE']/ATTRIBUTES/@SURCHARGE" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for Charge - Wood Stove [Factor,Credit,Display](END)  			  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for Surcharges BREEDDOG [Factor,Display](SATRT)		  			  -->
	<!-- ============================================================================================ -->
	<xsl:template name="BREEDDOG">
		<xsl:choose>
			<xsl:when test="STATENAME='MICHIGAN' and normalize-space(DOGFACTOR) = 'Y'">
				<xsl:variable name="VAR_DOG_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='SPECIFICBREEDOFDOGS']/ATTRIBUTES/@FACTOR" />
				<xsl:variable name="VAR_DOGSURCHARGE">
					<xsl:value-of select="DOGSURCHARGE" />
				</xsl:variable>
				<xsl:variable name="TERMFACTOR">
					<xsl:call-template name="TERM_FACTOR" />
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_DOG_FACTOR) = ''">
					<tr>
						<td class="midcolora">
							<li> Dog factor not found  (Path - "FACTOR[@ID='SURCHARGE']/NODE[@ID='SPECIFICBREEDOFDOGS']/ATTRIBUTES/@FACTOR") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--
			((<xsl:call-template name ="CALL_ADJUSTEDBASE"/>)
				*
				(<xsl:value-of select ="$VAR_DOG_FACTOR"/>)
			)
			* <xsl:value-of select="$VAR_DOGSURCHARGE"/>   
			* <xsl:value-of select="$TERMFACTOR"/>
			-->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- No OF Dog Surcharge-->
	<xsl:template name="NO_OF_DOGS">
		<xsl:choose>
			<xsl:when test="STATENAME='MICHIGAN' and DOGSURCHARGE &gt; 0">
				<xsl:value-of select="DOGSURCHARGE" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DOGS_CHARGE_INPERCENT">
		<xsl:choose>
			<xsl:when test="STATENAME='MICHIGAN' and (DOGSURCHARGE &gt; 0 or BREEDOFDOG != 'OTH')">
				<xsl:variable name="VAR1">
					<xsl:value-of select="DOGSURCHARGE" />
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='SPECIFICBREEDOFDOGS']/ATTRIBUTES/@SURCHARGE" />
				</xsl:variable>
				<xsl:if test="normalize-space($VAR2) = ''">
					<tr>
						<td class="midcolora">
							<li> Dog Surcharge not found  (Path - "FACTOR[@ID='SURCHARGE']/NODE[@ID='SPECIFICBREEDOFDOGS']/ATTRIBUTES/@SURCHARGE") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for Surcharges BREEDDOG [Factor,Credit,Display](END)  			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Surcharges PREFERRED_PLUS_DISPLAY[HO-20,HO-21,HO-24,HO-25](START)		  -->
	<!-- This Template has been created because in PREFERRED_PLUS_DISPLAY option (ADDITIONAL COVERAGES)
	 has been displayed as  'Included' 															  -->
	<!-- ============================================================================================ -->
	<!-- For Display Only -->
	<xsl:template name="PREFERRED_PLUS_DISPLAY">
		<!-- Will be included by default for HO-5 otherwise calculate -->
		<xsl:choose>
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO5"></xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For Calculation [Including in Additional Coverages] -->
	<xsl:template name="PREFERRED_PLUS_COVERAGE">
		<!-- Will be included by default for HO-5 therefore pass 0 otherwise calculate -->
		<xsl:choose>
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO5">0.00</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Surcharges PREFERRED_PLUS_DISPLAY[HO-20,HO-21,HO-24,HO-25](END)  		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for HO-20 Preferred Plus [Factor](SATRT)				  			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP">
		<xsl:variable name="TERMMONTHS" select="TERMFACTOR" />
		<xsl:choose>
			<xsl:when test="HO20 ='Y'">
				<xsl:variable name="VAR_HO20" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO20']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST" />
				<xsl:if test="normalize-space($VAR_HO20) = ''">
					<tr>
						<td class="midcolora">
							<li> Cost for HO20 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO20']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
			</xsl:when>
			<xsl:when test="HO21 ='Y' and STATENAME = 'MICHIGAN'">
				<xsl:variable name="VAR_HO21_MI" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO21']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST" />
				<xsl:if test="normalize-space($VAR_HO21_MI) = ''">
					<tr>
						<td class="midcolora">
							<li> Cost for HO21 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO21']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
			</xsl:when>
			<xsl:when test="HO21 ='Y' and STATENAME = 'INDIANA'">
				<xsl:variable name="VAR_HO21_IND" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO22']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST" />
				<xsl:if test="normalize-space($VAR_HO21_IND) = ''">
					<tr>
						<td class="midcolora">
							<li> Cost for HO21 (Indiana) not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO22']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
			</xsl:when>
			<xsl:when test="HO23 ='Y' and  STATENAME = 'INDIANA'">
				<xsl:variable name="VAR_HO23_IND" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO24']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST" />
				<xsl:if test="normalize-space($VAR_HO23_IND) = ''">
					<tr>
						<td class="midcolora">
							<li> Cost for HO23 (Indiana) not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO24']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
			</xsl:when>
			<xsl:when test="HO23 ='Y' and  STATENAME = 'MICHIGAN'">
				<xsl:variable name="VAR_HO23_MI" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO23']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST" />
				<xsl:if test="normalize-space($VAR_HO23_MI) = ''">
					<tr>
						<td class="midcolora">
							<li> Cost for HO23 (Michigan) not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO23']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
			</xsl:when>
			<xsl:when test="HO25 ='Y'">
				<xsl:variable name="VAR_HO25" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PREMIER_COVERAGE_HO25']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST" />
				<xsl:if test="normalize-space($VAR_HO25) = ''">
					<tr>
						<td class="midcolora">
							<li> Cost for HO25 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PREMIER_COVERAGE_HO25']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-20 Preferred Plus [Factor](END)				  			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for HO-25 PREMIER VIP COVERAGE [Factor](SATRT)  					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_25_PREMIER_VIP_COVERAGE">
		<xsl:variable name="TERMMONTHS" select="TERMFACTOR" />
		<xsl:choose>
			<xsl:when test="STATENAME = 'INDIANA'">0.00</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="HO25 ='Y'">
						<xsl:variable name="VAR_HO25" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PREMIER_COVERAGE_HO25']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST" />
						<xsl:if test="normalize-space($VAR_HO25) = ''">
							<tr>
								<td class="midcolora">
									<li> Cost for HO25 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PREMIER_COVERAGE_HO25']/ATTRIBUTES[@MONTHS=$TERMMONTHS]/@COST") </li>
								</td>
							</tr>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-11 REPLACEMENT COST PERSONAL PROPERTY [Factor](SATRT)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY">
		<xsl:variable name="TERMMONTHS" select="TERMFACTOR" />
		<xsl:choose>
			<xsl:when test="HO11 ='Y'">
				<xsl:variable name="VAR_HO11" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO11']/ATTRIBUTES[@MONTHS]/@COST" />
				<xsl:if test="normalize-space($VAR_HO11) = ''">
					<tr>
						<td class="midcolora">
							<li> Cost for HO11 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO11']/ATTRIBUTES[@MONTHS]/@COST") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For Display Only -->
	<xsl:template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY_DISPLAY">
		<xsl:choose>
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO5"></xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For Calculation [Including in Additional Coverages] -->
	<xsl:template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY_COVERAGE">
		<xsl:choose>
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO5"></xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-11 REPLACEMENT COST PERSONAL PROPERTY [Factor](END)		  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for HO-34 Replacement Cost Personal Property [Factor](SATRT)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_34_REPLACEMENT_COST">
		<xsl:variable name="PNAME" select="PRODUCTNAME" />
		<xsl:variable name="TERM" select="TERMFACTOR" />
		<xsl:variable name="VAR_REPLACEMENT_COST_FACTOR">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO34']/ATTRIBUTES[@FORM= $PNAME and @MONTHS=$TERM]/@FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="normalize-space(HO34) = 'Y'">
				<xsl:variable name="P_MINIMUMVALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO34']/ATTRIBUTES[@FORM = $PNAME and @MONTHS=$TERM]/@MINIMUM_PREMIUM" />
				<xsl:variable name="VAR_HO34_RC">
					<xsl:choose>
						<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO5">
							<xsl:variable name="VAR_BASE">
								<xsl:call-template name="DEWLLING-MAIN" />
							</xsl:variable>
							<xsl:variable name="VAR_COVERAGE">
								<xsl:call-template name="C-VALUE" />
							</xsl:variable>
							<xsl:variable name="VAR_REPLACEMENTCOST">
								<xsl:call-template name="REPLACEMENT_COST_FACTOR" />
							</xsl:variable>
							<xsl:variable name="VAR_TERMFACTOR">
								<xsl:call-template name="TERM_FACTOR" />
							</xsl:variable>
							<!--
						<xsl:value-of select="round($VAR_BASE * $VAR_COVERAGE * $VAR_REPLACEMENTCOST *$VAR_TERMFACTOR) *$VAR_REPLACEMENT_COST_FACTOR"/>
						 -->
						</xsl:when>
						<xsl:otherwise>
							<xsl:call-template name="HO_34_REPLACEMENT_COST_FACTORVALUE" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<!--
			<xsl:choose>
				<xsl:when test="$VAR_HO34_RC &gt; $P_MINIMUMVALUE">
					<xsl:value-of select="$VAR_HO34_RC"/>  
				</xsl:when>			
				<xsl:when test="$VAR_HO34_RC &lt;= $P_MINIMUMVALUE">
					<xsl:value-of select="$P_MINIMUMVALUE"/>   
				</xsl:when>
				<xsl:otherwise><xsl:value-of select="$VAR_HO34_RC"/></xsl:otherwise>
			</xsl:choose> 
			-->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-34 Replacement Cost Personal Property [Factor](END)		  -->
	<!--		    					  FOR MICHIGAN												  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for HO-34 Replacement Cost Personal Property [Factor](SATRT)		  -->
	<!--		    					  FOR INDIANA												  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_34_REPLACEMENT_COST_INDINA">
		<xsl:variable name="TERM" select="TERMFACTOR" />
		<xsl:variable name="PNAME" select="PRODUCTNAME" />
		<xsl:variable name="VAR_REPLACEMENT_COST_FACTOR">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO34']/ATTRIBUTES[@FORM= $PNAME and @MONTHS=$TERM]/@FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="HO34 = 'Y'">
				<!-- Calculate the value -->
				<xsl:variable name="VAR_HO34_COMPONENT_VALUE">
					<xsl:choose>
						<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO5 and  PRODUCT_PREMIER = $POLICYDESC_PREMIER">
							<xsl:variable name="VAR_BASE">
								<xsl:call-template name="DEWLLING-MAIN" />
							</xsl:variable>
							<xsl:variable name="VAR_COVERAGE">
								<xsl:call-template name="C-VALUE" />
							</xsl:variable>
							<xsl:variable name="VAR_REPLACEMENTCOST">
								<xsl:call-template name="REPLACEMENT_COST_FACTOR" />
							</xsl:variable>
							<xsl:variable name="VAR_INSURANCESCORE">
								<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
							</xsl:variable>
							<xsl:variable name="VAR_TERMFACTOR">
								<xsl:call-template name="TERM_FACTOR" />
							</xsl:variable>
							<xsl:variable name="VAR_DFACTOR">
								<xsl:call-template name="DFACTOR" />
							</xsl:variable>
							<xsl:value-of select="($VAR_BASE * $VAR_COVERAGE * $VAR_REPLACEMENTCOST *$VAR_INSURANCESCORE *$VAR_TERMFACTOR *$VAR_DFACTOR) *$VAR_REPLACEMENT_COST_FACTOR" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:call-template name="HO_34_REPLACEMENT_COST_FACTORVALUE" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<!-- Get the maximum and minimum values against form and term months -->
				<xsl:variable name="P_MINIMUM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO34']/ATTRIBUTES[@FORM= $PNAME and @MONTHS=$TERM]/@MINIMUM_PREMIUM" />
				<xsl:variable name="P_MAXIMUM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO34']/ATTRIBUTES[@FORM= $PNAME and @MONTHS=$TERM]/@MAXIMUM_PREMIUM" />
				<!-- Check for minimum and maximum values. If less than min. send minimum, if greater than max. send max value else calculated val 
			 -->
				<xsl:choose>
					<xsl:when test="format-number($VAR_HO34_COMPONENT_VALUE,'#.00') &lt;= $P_MINIMUM">
						<xsl:value-of select="$P_MINIMUM" />
					</xsl:when>
					<xsl:when test="normalize-space($P_MAXIMUM)!='' and format-number($VAR_HO34_COMPONENT_VALUE,'#.00') &gt;= $P_MAXIMUM">
						<xsl:value-of select="$P_MAXIMUM" />
					</xsl:when>
					<xsl:when test="normalize-space($P_MAXIMUM)='' and format-number($VAR_HO34_COMPONENT_VALUE,'#.00') &gt; $P_MINIMUM">
						<xsl:value-of select="$VAR_HO34_COMPONENT_VALUE" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$VAR_HO34_COMPONENT_VALUE" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SELECT_HO_34">
		<xsl:choose>
			<xsl:when test="normalize-space(STATENAME) = 'INDIANA'">
				<xsl:call-template name="HO_34_REPLACEMENT_COST_INDINA" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="HO_34_REPLACEMENT_COST" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HO_34_REPLACEMENT_COST_FACTORVALUE">
		<xsl:variable name="PNAME" select="PRODUCTNAME" />
		<xsl:variable name="TERM" select="TERMFACTOR" />
		<xsl:variable name="VAR_ADJUSTED_BASE">
			<xsl:call-template name="CALL_ADJUSTEDBASE" />
		</xsl:variable>
		<xsl:variable name="VAR_REPLACEMENT_COST_FACTOR">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO34']/ATTRIBUTES[@FORM= $PNAME and @MONTHS=$TERM]/@FACTOR" />
		</xsl:variable>
		<xsl:value-of select="round($VAR_ADJUSTED_BASE * $VAR_REPLACEMENT_COST_FACTOR)" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-34 Replacement Cost Personal Property [Factor](END)		  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Surcharges PREFERRED_PLUS_DISPLAY[HO-20,HO-21,HO-24,HO-25](START)		  -->
	<!-- This Template has been created because in PREFERRED_PLUS_DISPLAY option (ADDITIONAL COVERAGES)
	 has been displayed as  'Included' 															  -->
	<!-- ============================================================================================ -->
	<!-- For Display Only -->
	<xsl:template name="SELECT_HO_34_DISPLAY">
		<xsl:choose>
			<!-- For Michigan State -->
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO5"></xsl:when>
			<xsl:when test="STATENAME ='MICHIGAN' and  PRODUCTNAME != $POLICYTYPE_HO5 and  PRODUCT_PREMIER != $POLICYDESC_PREMIER">
				<xsl:call-template name="HO_34_REPLACEMENT_COST" />
			</xsl:when>
			<xsl:when test="STATENAME ='INDIANA' and  PRODUCTNAME != $POLICYTYPE_HO5 "> <!--and  PRODUCT_PREMIER !=$POLICYDESC_REPLACEMENT" -->
				<xsl:call-template name="SELECT_HO_34" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For Calculation [Including in Additional Coverages] -->
	<xsl:template name="SELECT_HO_34_COVERAGE">
		<xsl:choose>
			<!-- For Michigan State -->
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO5 "></xsl:when>
			<xsl:when test="STATENAME ='MICHIGAN' and  PRODUCTNAME != $POLICYTYPE_HO5 and  PRODUCT_PREMIER != $POLICYDESC_PREMIER">
				<xsl:call-template name="HO_34_REPLACEMENT_COST" />
			</xsl:when>
			<!-- For Indiana State -->
			<xsl:when test="STATENAME ='INDIANA' and  PRODUCTNAME != $POLICYTYPE_HO5 and  PRODUCT_PREMIER !=$POLICYDESC_REPLACEMENT">
				<xsl:call-template name="SELECT_HO_34" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Surcharges PREFERRED_PLUS_DISPLAY[HO-20,HO-21,HO-24,HO-25](END)		  -->
	<!-- This Template has been created because in PREFERRED_PLUS_DISPLAY option (ADDITIONAL COVERAGES)
	 has been displayed as  'Included' 															  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for HO-96 REPLACEMENT COST PERSONAL PROPERTY [Factor](SATRT)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_96_REPLACEMENT_COST_PERSONAL_PROPERTY">
		<xsl:variable name="TERM" select="TERMFACTOR" />
		<xsl:variable name="PHO96FINALVALUE" select="HO96FINALVALUE"></xsl:variable>
		<xsl:choose>
			<xsl:when test="$PHO96FINALVALUE = 0.00"></xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR_HO96_COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO96']/ATTRIBUTES[@LIMITS = $PHO96FINALVALUE and @MONTHS=$TERM ]/@COST" />
				<xsl:if test="normalize-space($VAR_HO96_COST) = ''">
					<tr>
						<td class="midcolora">
							<li> Cost for HO96   not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='REPLACEMENT_COST_PERSONAL_PROPERTY_HO96']/ATTRIBUTES[@LIMITS = $PHO96FINALVALUE and @MONTHS=$TERM ]/@COST") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-96 REPLACEMENT COST PERSONAL PROPERTY [Factor](END)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for HO-312 BUSINESS PROPERTY INCREASED LIMITS [Factor](START)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_312_BUSINESS_PROPERTY_INCREASED_LIMITS">
		<xsl:variable name="PHO312VALUE" select="HO312ADDITIONAL"></xsl:variable>
		<xsl:variable name="PHO312COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='BUSINESS_PROPERTY_INCREASED_LIMITS_HO312']/ATTRIBUTES/@COST"></xsl:variable>
		<xsl:if test="normalize-space($PHO312COST) = ''">
			<tr>
				<td class="midcolora">
					<li> Cost for HO312 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='BUSINESS_PROPERTY_INCREASED_LIMITS_HO312']/ATTRIBUTES/@COST") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="PHO312INCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='BUSINESS_PROPERTY_INCREASED_LIMITS_HO312']/ATTRIBUTES/@INCREASEDAMOUNT"></xsl:variable>
		<xsl:if test="normalize-space($PHO312INCREASEDAMOUNT) = ''">
			<tr>
				<td class="midcolora">
					<li> Increased Amount for HO312 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='BUSINESS_PROPERTY_INCREASED_LIMITS_HO312']/ATTRIBUTES/@INCREASEDAMOUNT") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--
	<xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable>
	<xsl:choose>
		<xsl:when test="normalize-space($PHO312VALUE) !='' and $PHO312VALUE &gt; 0">
			<xsl:value-of select="($PHO312COST * ($PHO312VALUE div $PHO312INCREASEDAMOUNT)) * $TERMFACTOR"/>	
		</xsl:when>
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
	-->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-312 BUSINESS PROPERTY INCREASED LIMITS [Factor](END)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for HO-48 OTHER STRUCTURE INCREASED LIMITS [Factor](START)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_48_OTHER_STRUCTURE_INCREASED_LIMITS">
		<xsl:variable name="PHO48VALUE" select="HO48ADDITIONAL"></xsl:variable>
		<xsl:variable name="PHO48COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_INCREASED_LIMITS_HO48']/ATTRIBUTES/@COST"></xsl:variable>
		<xsl:if test="normalize-space($PHO48COST) = ''">
			<tr>
				<td class="midcolora">
					<li> Cost for HO48 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_INCREASED_LIMITS_HO48']/ATTRIBUTES/@COST") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="PHO48INCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_INCREASED_LIMITS_HO48']/ATTRIBUTES/@INCREASEDAMOUNT"></xsl:variable>
		<xsl:if test="normalize-space($PHO48INCREASEDAMOUNT) = ''">
			<tr>
				<td class="midcolora">
					<li> Increased Amount for HO48 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_INCREASED_LIMITS_HO48']/ATTRIBUTES/@INCREASEDAMOUNT") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- 	 <xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable> -->
		<!--
	<xsl:choose>
		<xsl:when test="$PHO48VALUE !='' and $PHO48VALUE &gt; 0">
			<xsl:value-of select="(($PHO48COST * $PHO48VALUE) div $PHO48INCREASEDAMOUNT) * $TERMFACTOR"/>
		</xsl:when>
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
-->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-48 OTHER STRUCTURE INCREASED LIMITS [Factor](END)			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for HO-40 OTHER STRUCTURE RENTED TO OTHERS [Factor](START)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_40_OTHER_STRUCTURE_RENTED_TO_OTHERS">
		<xsl:variable name="PHO40VALUE" select="HO40ADDITIONAL"></xsl:variable>
		<xsl:variable name="PHO40COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_RENTED_TO_OTHERS_HO40']/ATTRIBUTES/@COST"></xsl:variable>
		<xsl:if test="normalize-space($PHO40COST) = ''">
			<tr>
				<td class="midcolora">
					<li> Cost for HO40 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_RENTED_TO_OTHERS_HO40']/ATTRIBUTES/@COST") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="PHO40INCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_RENTED_TO_OTHERS_HO40']/ATTRIBUTES/@INCREASEDAMOUNT"></xsl:variable>
		<xsl:if test="normalize-space($PHO40INCREASEDAMOUNT) = ''">
			<tr>
				<td class="midcolora">
					<li> Increased Amount for HO40 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_RENTED_TO_OTHERS_HO40']/ATTRIBUTES/@INCREASEDAMOUNT") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--
	 <xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable>
	 
	 <xsl:choose>
		<xsl:when test="$PHO40VALUE  !='' and $PHO40VALUE &gt; 0"> 
			<xsl:value-of select="(($PHO40COST * ($PHO40VALUE div $PHO40INCREASEDAMOUNT))* $TERMFACTOR)"/>	
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	 </xsl:choose>
	-->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-40 OTHER STRUCTURE RENTED TO OTHERS [Factor](END)			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for HO-42 OTHER STRUCTURE WITH INCIDENTAL  [Factor](START)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_42_OTHER_STRUCTURE_WITH_INCIDENTAL">
		<xsl:variable name="TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="normalize-space(HO42ADDITIONAL) ='' or HO42ADDITIONAL = 0"></xsl:when>
			<xsl:otherwise>
				<xsl:variable name="PHO42VALUE" select="HO42ADDITIONAL"></xsl:variable>
				<xsl:variable name="PHO42COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_WITH_INCIDENTAL_HO42']/ATTRIBUTES/@COST"></xsl:variable>
				<xsl:if test="normalize-space($PHO42COST) = ''">
					<tr>
						<td class="midcolora">
							<li> Cost for HO42 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_WITH_INCIDENTAL_HO42']/ATTRIBUTES/@COST") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="PHO42INCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_WITH_INCIDENTAL_HO42']/ATTRIBUTES/@INCREASEDAMOUNT"></xsl:variable>
				<xsl:if test="normalize-space($PHO42INCREASEDAMOUNT) = ''">
					<tr>
						<td class="midcolora">
							<li> Increased Amount for HO42 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_WITH_INCIDENTAL_HO42']/ATTRIBUTES/@INCREASEDAMOUNT") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--	<xsl:value-of select="(($PHO42COST * $PHO42VALUE) div $PHO42INCREASEDAMOUNT) * $TERMFACTOR"/> -->
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-42 OTHER STRUCTURE WITH INCIDENTAL  [Factor](END)			  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Template for HO-53 INCREASED CREDIT CARD [Factor](START)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_53_INCREASED_CREDIT_CARD">
		<xsl:variable name="PHO53VALUE" select="HO53ADDITIONAL+HO53INCLUDE"></xsl:variable>
		<xsl:variable name="TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PHO53VALUE &gt; 0">
				<xsl:variable name="VAR_H053_COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_CREDIT_CARD']/ATTRIBUTES[@LIMIT = $PHO53VALUE]/@COST" />
				<xsl:if test="normalize-space($VAR_H053_COST) = ''">
					<tr>
						<td class="midcolora">
							<li> Cost for HO53 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_CREDIT_CARD']/ATTRIBUTES[@LIMIT = $PHO53VALUE]/@COST") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Template for HO-53 INCREASED CREDIT CARD [Factor](START)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Template for UNSCHEDULED_JEWELRY_ADDITIONAL [Additional](START)				  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="UNSCHEDULED_JEWELRY_ADDITIONAL">
		<xsl:variable name="PJEWELRYVALUE" select="UNSCHEDULEDJEWELRYADDITIONAL"></xsl:variable>
		<xsl:variable name="PJEWELRYCOST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='A']/@COST"></xsl:variable>
		<xsl:if test="normalize-space($PJEWELRYCOST) = ''">
			<tr>
				<td class="midcolora">
					<li> Cost for jewellery not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='A']/@COST") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="PJEWELRYINCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='A']/@INCREMENT_AMOUNT"></xsl:variable>
		<xsl:if test="normalize-space($PJEWELRYINCREASEDAMOUNT) = ''">
			<tr>
				<td class="midcolora">
					<li> Increased Amount for jewellery not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='A']/@INCREMENT_AMOUNT") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--
	 <xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable>
	 <xsl:choose>
		<xsl:when test="normalize-space($PJEWELRYVALUE)!='' and $PJEWELRYVALUE &gt; 0">
			<xsl:value-of select="round((($PJEWELRYCOST * $PJEWELRYVALUE) div $PJEWELRYINCREASEDAMOUNT ) * $TERMFACTOR)"/></xsl:when>
		<xsl:otherwise>0.00</xsl:otherwise>
	 </xsl:choose>	
	-->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Template for UNSCHEDULED_JEWELRY_ADDITIONAL [Additional](END)				  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template for MONEY [Additional](START)							  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="MONEY">
		<xsl:variable name="PMONEYVALUE" select="MONEYADDITIONAL"></xsl:variable>
		<xsl:variable name="PMONEYCOST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='B']/@COST"></xsl:variable>
		<xsl:if test="normalize-space($PMONEYCOST) = ''">
			<tr>
				<td class="midcolora">
					<li> Cost for Money not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='B']/@COST") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="PMONEYINCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='B']/@INCREMENT_AMOUNT"></xsl:variable>
		<xsl:if test="normalize-space($PMONEYINCREASEDAMOUNT) = ''">
			<tr>
				<td class="midcolora">
					<li> Increased Amount for Money  not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='B']/@INCREMENT_AMOUNT") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--
	 <xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable>
	<xsl:choose>
		<xsl:when test="normalize-space($PMONEYCOST) !='' and $PMONEYCOST &gt; 0"><xsl:value-of select="round((($PMONEYCOST * $PMONEYVALUE) div $PMONEYINCREASEDAMOUNT ) * $TERMFACTOR)"/></xsl:when>
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
	-->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template for MONEY [Additional](END)							  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template for SECURITIES [Additional](START)						  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="SECURITIES">
		<xsl:variable name="PSECURITIESVALUE" select="SECURITIESADDITIONAL"></xsl:variable>
		<xsl:variable name="PSECURITIESCOST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='C']/@COST"></xsl:variable>
		<xsl:if test="normalize-space($PSECURITIESCOST) = ''">
			<tr>
				<td class="midcolora">
					<li> Cost for Securities not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='C']/@COST") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="PSECURITIESINCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='C']/@INCREMENT_AMOUNT"></xsl:variable>
		<xsl:if test="normalize-space($PSECURITIESINCREASEDAMOUNT) = ''">
			<tr>
				<td class="midcolora">
					<li> Increased Amount for Securities not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='C']/@INCREMENT_AMOUNT") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--
	 <xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable>  
	 <xsl:choose>
		<xsl:when test="normalize-space($PSECURITIESVALUE)!= '' and $PSECURITIESVALUE &gt; 0"><xsl:value-of select="round((($PSECURITIESCOST * $PSECURITIESVALUE) div $PSECURITIESINCREASEDAMOUNT ) * $TERMFACTOR)"/></xsl:when>
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
	-->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template for SECURITIES [Additional](END)						  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Template for SILVERWARE, GOLDWARE [Additional](START)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="SILVERWARE_GOLDWARE">
		<xsl:variable name="PSILVERWARE_GOLDWAREVALUE" select="SILVERWAREADDITIONAL"></xsl:variable>
		<xsl:variable name="PSILVERWARE_GOLDWARECOST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='D']/@COST"></xsl:variable>
		<xsl:if test="normalize-space($PSILVERWARE_GOLDWARECOST) = ''">
			<tr>
				<td class="midcolora">
					<li> Cost for gold and silver not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='D']/@COST") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="PSILVERWARE_GOLDWAREINCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='D']/@INCREMENT_AMOUNT"></xsl:variable>
		<xsl:if test="normalize-space($PSILVERWARE_GOLDWAREINCREASEDAMOUNT) = ''">
			<tr>
				<td class="midcolora">
					<li> Increased Amount for gold and silver ware not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='D']/@INCREMENT_AMOUNT") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--
	 <xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable>  
	<xsl:choose>
		<xsl:when test="normalize-space($PSILVERWARE_GOLDWAREVALUE) !='' and $PSILVERWARE_GOLDWAREVALUE &gt; 0">
			<xsl:value-of select="round((($PSILVERWARE_GOLDWARECOST * $PSILVERWARE_GOLDWAREVALUE) div $PSILVERWARE_GOLDWAREINCREASEDAMOUNT ) * $TERMFACTOR)"/>
		</xsl:when>
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
	-->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Template for SILVERWARE, GOLDWARE [Additional](END)						  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template for FIREARMS[Additional](START)						  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="FIREARMS">
		<xsl:variable name="PFIREARMSVALUE" select="FIREARMSADDITIONAL"></xsl:variable>
		<xsl:variable name="PFIREARMSCOST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='E']/@COST"></xsl:variable>
		<xsl:if test="normalize-space($PFIREARMSCOST) = ''">
			<tr>
				<td class="midcolora">
					<li> Cost for firearms not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='E']/@COST") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="PFIREARMSINCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='E']/@INCREMENT_AMOUNT"></xsl:variable>
		<xsl:if test="normalize-space($PFIREARMSINCREASEDAMOUNT) = ''">
			<tr>
				<td class="midcolora">
					<li> Increased Amount for firarms not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='INCREASED_SPECIAL_LIMITS']/ATTRIBUTES[@ID ='E']/@INCREMENT_AMOUNT") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--
	 <xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable>  
	<xsl:choose>
		<xsl:when test="normalize-space($PFIREARMSVALUE) !='' and $PFIREARMSVALUE &gt; 0">
			<xsl:value-of select="round((($PFIREARMSCOST * $PFIREARMSVALUE) div $PFIREARMSINCREASEDAMOUNT) * $TERMFACTOR)"/>
		</xsl:when>
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
	-->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template for FIREARMS[Additional](END)							  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Templates for HO-277 Ordinance or Law[Additional](START)				  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ORDINANCE">
		<xsl:choose>
			<xsl:when test="normalize-space(HO277) = 'Y'">
				<xsl:variable name="P_MINIMUMVALUE_ANNUAL" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='LAW_COVERAGE']/ATTRIBUTES/@MINIMUM" />
				<xsl:if test="normalize-space($P_MINIMUMVALUE_ANNUAL) = ''">
					<tr>
						<td class="midcolora">
							<li> min annual value for ordinance not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='LAW_COVERAGE']/ATTRIBUTES/@MINIMUM") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_ORDINANCE_COST_FACTORVALUE">
					<xsl:call-template name="ORDINANCE_COST_FACTORVALUE" />
				</xsl:variable>
				<!--
			<xsl:variable name="TERM"><xsl:call-template name = "TERM_FACTOR"/></xsl:variable>
			<xsl:variable name="P_MINIMUMVALUE" select="round($P_MINIMUMVALUE_ANNUAL * $TERM)"/>
			 
				
		<xsl:choose>
			<xsl:when test="$VAR_ORDINANCE_COST_FACTORVALUE &lt; $P_MINIMUMVALUE"><xsl:value-of select="$P_MINIMUMVALUE"/></xsl:when>
			<xsl:otherwise><xsl:value-of select="round($VAR_ORDINANCE_COST_FACTORVALUE * $TERM)"/></xsl:otherwise>
		</xsl:choose>
		-->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ORDINANCE_COST_FACTORVALUE">
		<xsl:variable name="VAR_ADJUSTED">
			<xsl:call-template name="CALL_ADJUSTEDBASE" />
		</xsl:variable>
		<xsl:variable name="TERM">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='LAW_COVERAGE']/ATTRIBUTES/@CHARGE" />
		<xsl:if test="normalize-space($VAR_FACTOR) = ''">
			<tr>
				<td class="midcolora">
					<li> Ordinance cost factor not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='LAW_COVERAGE']/ATTRIBUTES/@CHARGE") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--	
	<xsl:value-of select="$VAR_ADJUSTED * $VAR_FACTOR"/>
	-->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for HO-277 Ordinance or Law[Additional](END)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--   Templates for HO-455 Identity Fraud Expense Coverage[Factor, Limit, DEductible](SATRT)	  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_455_IDENTITY_FRAUD">
		<xsl:choose>
			<xsl:when test="HO455 = 'Y'">
				<xsl:variable name="VAR_HO455" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='FRAUD_EXPENSE']/ATTRIBUTES/@RATE" />
				<xsl:if test="normalize-space($VAR_HO455) = ''">
					<tr>
						<td class="midcolora">
							<li> rates for HO55 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='FRAUD_EXPENSE']/ATTRIBUTES/@RATE") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--LIMITS -->
	<xsl:template name="HO_455_LIMIT">
		<xsl:choose>
			<xsl:when test="HO455 = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='FRAUD_EXPENSE']/ATTRIBUTES/@COVERAGE" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- DEDUCTIBLE -->
	<xsl:template name="HO_455_DEDUCTIBLE">
		<xsl:choose>
			<xsl:when test="HO455 = 'Y'">
				<xsl:variable name="VAR_HO455" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='FRAUD_EXPENSE']/ATTRIBUTES/@DEDUCTIBLE" />
				<xsl:if test="normalize-space($VAR_HO455) = ''">
					<tr>
						<td class="midcolora">
							<li> Deductible for HO455 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='FRAUD_EXPENSE']/ATTRIBUTES/@DEDUCTIBLE") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--   Templates for HO-455 Identity Fraud Expense Coverage[Factor, Limit, DEductible](SATRT)	  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for HO-315 Earthquake (Zone 0)[Additional](SATRT)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_315_EARTHQUAKE">
		<!-- Variables for FORM, ZONE, INCREASEDCOVERAGEC, INCREASEDCOVGD -->
		<xsl:variable name="PEXTERIOR_CONSTRUCTION_F_M" select="EXTERIOR_CONSTRUCTION_F_M"></xsl:variable>
		<xsl:variable name="PPERSONALPROPERTYINCREASEDLIMITADDITIONAL" select="PERSONALPROPERTYINCREASEDLIMITADDITIONAL"></xsl:variable>
		<xsl:variable name="PADDITIONALLIVINGEXPENSEADDITIONAL" select="ADDITIONALLIVINGEXPENSEADDITIONAL"></xsl:variable>
		<xsl:variable name="PFORM" select="PRODUCTNAME"></xsl:variable>
		<xsl:variable name="PZONE" select="TERRITORYZONE" />
		<!--IF('<xsl:value-of select="HO315"/>' = 'Y') THEN	-->
		<xsl:choose>
			<xsl:when test="HO315 = 'Y'">
				<!--	1. Get Rate for Frame per thousand by policy form and construction
							2. Multiply Rate with Coverage A Limit
							3. Multiply Rate with increased coverage C limit 
							4. Multiply Rate with increased coverage D limit 
							5. Add Steps 2. , 3. , 4. -->
				<xsl:variable name="VAR_RATE_PER_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='EARTHQUAKE']/@RATE_PER_VALUE" />
				<xsl:if test="normalize-space($VAR_RATE_PER_VALUE) = ''">
					<tr>
						<td class="midcolora">
							<li> Earth quake rate per value not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='EARTHQUAKE']/@RATE_PER_VALUE") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--
					<xsl:choose>
							<xsl:when test="$PEXTERIOR_CONSTRUCTION_F_M = 'F'">
									<xsl:variable name="VAR_FRAME" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='EARTHQUAKE']/ATTRIBUTES[@FORM = $PFORM and @ZONE = $PZONE]/@FRAME" />
									<xsl:variable name="VAR1">
										<xsl:value-of select="$VAR_FRAME * DWELLING_LIMITS div $VAR_RATE_PER_VALUE" />
									</xsl:variable>
									<xsl:variable name="VAR2">
										<xsl:choose>
											<xsl:when test="$PPERSONALPROPERTYINCREASEDLIMITADDITIONAL &gt; 0.00">
												<xsl:value-of select="($VAR_FRAME * $PPERSONALPROPERTYINCREASEDLIMITADDITIONAL) div $VAR_RATE_PER_VALUE" />
											</xsl:when>
											<xsl:otherwise>0.00</xsl:otherwise>
										</xsl:choose>
									</xsl:variable>
									<xsl:variable name="VAR3">
										<xsl:choose>
											<xsl:when test="$PADDITIONALLIVINGEXPENSEADDITIONAL &gt; 0.00">
												<xsl:value-of select="($VAR_FRAME * $PADDITIONALLIVINGEXPENSEADDITIONAL) div $VAR_RATE_PER_VALUE" />
											</xsl:when>
											<xsl:otherwise>0.00</xsl:otherwise>
										</xsl:choose>
									</xsl:variable>
									<xsl:variable name="VAR4">
										<xsl:call-template name="TERM_FACTOR" />
									</xsl:variable>
									  <xsl:value-of select="($VAR1 + $VAR2 + $VAR3) * $VAR4" />  
									 
								</xsl:when>
								<xsl:when test="$PEXTERIOR_CONSTRUCTION_F_M = 'M'">
									<xsl:variable name="VAR_MASONARY">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='EARTHQUAKE']/ATTRIBUTES[@FORM = $PFORM and @ZONE = $PZONE]/@MASONRY" />
									</xsl:variable>
									<xsl:variable name="VAR1">
										<xsl:value-of select="$VAR_MASONARY * DWELLING_LIMITS div $VAR_RATE_PER_VALUE" />
									</xsl:variable>
									<xsl:variable name="VAR2">
										<xsl:choose>
											<xsl:when test="$PPERSONALPROPERTYINCREASEDLIMITADDITIONAL &gt; 0.00">
												<xsl:value-of select="($VAR_MASONARY * $PPERSONALPROPERTYINCREASEDLIMITADDITIONAL) div $VAR_RATE_PER_VALUE" />
											</xsl:when>
											<xsl:otherwise>0.00</xsl:otherwise>
										</xsl:choose>
									</xsl:variable>
									<xsl:variable name="VAR3">
										<xsl:choose>
											<xsl:when test="$PADDITIONALLIVINGEXPENSEADDITIONAL &gt; 0.00">
												<xsl:value-of select="($VAR_MASONARY * $PADDITIONALLIVINGEXPENSEADDITIONAL) div $VAR_RATE_PER_VALUE" />
											</xsl:when>
											<xsl:otherwise>0.00</xsl:otherwise>
										</xsl:choose>
									</xsl:variable>
									<xsl:variable name="VAR4">
										<xsl:call-template name="TERM_FACTOR" />
									</xsl:variable>
									  <xsl:value-of select="($VAR1 + $VAR2 + $VAR3) * $VAR4" />  
									 
								</xsl:when>
								<xsl:otherwise>0.00</xsl:otherwise>
						</xsl:choose>
						-->
			</xsl:when>
			<xsl:otherwise>
				<!-- 0.00 --></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for HO-315 Earthquake (Zone 0)[Additional](END)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Template for HO-327 Water Back-Up and Sump Pump Overflow(START)				  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_327_WATER_BACK_UP">
		<xsl:choose>
			<xsl:when test="normalize-space(HO327) ='' or HO327 = 0">
				<!-- 0 --></xsl:when>
			<xsl:otherwise>
				<xsl:variable name="PHO327VALUE" select="HO327"></xsl:variable>
				<xsl:variable name="PHO327COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='WATER_BACKUP']/ATTRIBUTES/@COST"></xsl:variable>
				<xsl:if test="normalize-space($PHO327COST) = ''">
					<tr>
						<td class="midcolora">
							<li> Cost for HO327 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='WATER_BACKUP']/ATTRIBUTES/@COST") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="PHO327INCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='WATER_BACKUP']/ATTRIBUTES/@INCREMENTAL_AMOUNT"></xsl:variable>
				<xsl:if test="normalize-space($PHO327INCREASEDAMOUNT) = ''">
					<tr>
						<td class="midcolora">
							<li> Increased Amount for HO327 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='WATER_BACKUP']/ATTRIBUTES/@INCREMENTAL_AMOUNT") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--
			<xsl:variable name="VAR_TERM_FACTOR"><xsl:call-template name ="TERM_FACTOR"/></xsl:variable>
			<xsl:value-of select="(($PHO327COST * $PHO327VALUE) div $PHO327INCREASEDAMOUNT)*$VAR_TERM_FACTOR"/>
			-->
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Template for HO-327 Water Back-Up and Sump Pump Overflow(END)				  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Template for  HO-33 Condo-Unit Owners Rental to Others(START)				  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_33_CONDO_UNIT"> <!-- i was here on saturday 19/8/2006 -->
		<xsl:variable name="VAR_TERM_FACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="HO33 = 'Not Rented'">
		0
		</xsl:when>
			<xsl:when test="HO33 = 'Under 2 Months'">
			<xsl:variable name="PMINIMUMVALUE1" select="round($HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'OPTIONALSECTIONCOVG']/NODE[@ID='HO_33_CONDO_UNIT']/ATTRIBUTES[@OPTIONS = '1']/@MINAMOUNT * $VAR_TERM_FACTOR)" />
			
			IF (((<xsl:call-template name="CALL_ADJUSTEDBASE" />) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'OPTIONALSECTIONCOVG']/NODE[@ID='HO_33_CONDO_UNIT']/ATTRIBUTES[@OPTIONS = '1']/@FACTOR" />)) &lt; <xsl:value-of select="$PMINIMUMVALUE1" />)
			THEN
				 (<xsl:value-of select="$PMINIMUMVALUE1" />) 
				 
			ELSE
				((<xsl:call-template name="CALL_ADJUSTEDBASE" />) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'OPTIONALSECTIONCOVG']/NODE[@ID='HO_33_CONDO_UNIT']/ATTRIBUTES[@OPTIONS = '1']/@FACTOR" />) * <xsl:value-of select="$VAR_TERM_FACTOR" />) 
				 
		</xsl:when>
			<xsl:when test="HO33 = '2 to 6 Months'">
 			
			<xsl:variable name="PMINIMUMVALUE2" select="round($HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'OPTIONALSECTIONCOVG']/NODE[@ID='HO_33_CONDO_UNIT']/ATTRIBUTES[@OPTIONS = '1']/@MINAMOUNT * $VAR_TERM_FACTOR)" />
			
			IF (((<xsl:call-template name="CALL_ADJUSTEDBASE" />) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'OPTIONALSECTIONCOVG']/NODE[@ID='HO_33_CONDO_UNIT']/ATTRIBUTES[@OPTIONS = '2']/@FACTOR" />)) &lt; <xsl:value-of select="$PMINIMUMVALUE2" />)
			THEN
				 (<xsl:value-of select="$PMINIMUMVALUE2" />) 
				 
			ELSE
				((<xsl:call-template name="CALL_ADJUSTEDBASE" />) * (<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'OPTIONALSECTIONCOVG']/NODE[@ID='HO_33_CONDO_UNIT']/ATTRIBUTES[@OPTIONS = '2']/@FACTOR" />) * <xsl:value-of select="$VAR_TERM_FACTOR" />) 
		
		</xsl:when>
			<xsl:otherwise>
		0	
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Template for  HO-33 Condo-Unit Owners Rental to Others(END)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Template for HO-287 Mine Subsidence Coverage (Start )						  -->
	<!--		    					  FOR INDIANA												  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_287_MINE_SUBSIDENCE_COVERAGE">
		<xsl:variable name="PCOVERAGES" select="DWELLING_LIMITS" />
		<xsl:variable name="VAR_TERM_FACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="STATENAME = 'INDIANA'">
				<xsl:choose>
					<xsl:when test="HO287 = 'Y'">
						<xsl:variable name="VAR_PREM_HO287" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'OTHER']/NODE[@ID='MINE_SUBSIDENCE_COVERAGE']/ATTRIBUTES[@MINCOVERAGE &lt;= $PCOVERAGES and @MAXCOVERAGE &gt;= $PCOVERAGES]/@PREMIUM" />
						<xsl:if test="normalize-space($VAR_PREM_HO287) = ''">
							<tr>
								<td class="midcolora">
									<li> Premium for HO287 mines subsidence not found  (Path - "FACTOR[@ID = 'OTHER']/NODE[@ID='MINE_SUBSIDENCE_COVERAGE']/ATTRIBUTES[@MINCOVERAGE &lt;= $PCOVERAGES and @MAXCOVERAGE &gt;= $PCOVERAGES]/@PREMIUM") </li>
								</td>
							</tr>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Template for HO-287 Mine Subsidence Coverage (End)							  -->
	<!--		    					  FOR INDIANA												  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Template for HO-9 Collapse From Sub Surface Water (Start )					  -->
	<!--		    					  FOR INDIANA												  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_9_COLLAPSE_FROM_SUB_SURFACE_WATER">
		<xsl:choose>
			<xsl:when test="STATENAME = 'INDIANA'">
				<xsl:choose>
					<xsl:when test="HO9 = 'Y'">
						<xsl:variable name="VAR_HO9" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID = 'COLLAPSE_FROM_SUB_SURFACE_WATER_HO9']/ATTRIBUTES/@COST" />
						<xsl:if test="normalize-space($VAR_HO9) = ''">
							<tr>
								<td class="midcolora">
									<li> Cost for HO9 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID = 'COLLAPSE_FROM_SUB_SURFACE_WATER_HO9']/ATTRIBUTES/@COST") </li>
								</td>
							</tr>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Template for HO-9 Collapse From Sub Surface Water (End )					  -->
	<!--		    					  FOR INDIANA												  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Template for HO-200 Waterbed Liability (Start )								 -->
	<!--		    					  FOR INDIANA												  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_200_WATERBED_LIABILITY">
		<xsl:variable name="VAR_TERM_FACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="HO200 = 'Y'">
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='WATERBED_LIABILITY']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:if test="normalize-space($VAR_LIABILITY_LIMIT_GP) = ''">
					<tr>
						<td class="midcolora">
							<li> Liablity limit group for HO200  not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='WATERBED_LIABILITY']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--
			<xsl:choose> 
				<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0"> 
					<xsl:value-of select="round($VAR_LIABILITY_LIMIT_GP * $VAR_TERM_FACTOR)"/>
			 	</xsl:when>
			 	<xsl:otherwise>0.00</xsl:otherwise>	
			</xsl:choose>	
		-->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Template for HO-200 Waterbed Liability (End )								  -->
	<!--		    					  FOR INDIANA												  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template for HO-490 SPECIFIC STRUCTURED (START)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_490_SPECIFIC_STRUCTURED">
		<xsl:choose>
			<xsl:when test="SPECIFICSTRUCTURESADDITIONAL = 0.00"></xsl:when>
			<xsl:otherwise>
				<xsl:variable name="PHO490VALUE" select="SPECIFICSTRUCTURESADDITIONAL"></xsl:variable>
				<xsl:variable name="PHO490COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='SPECIFIC_STRUC_AWAY']/ATTRIBUTES/@RATE"></xsl:variable>
				<xsl:if test="normalize-space($PHO490COST) = ''">
					<tr>
						<td class="midcolora">
							<li> Cost for PHO490 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='SPECIFIC_STRUC_AWAY']/ATTRIBUTES/@RATE") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_PHO490INCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='SPECIFIC_STRUC_AWAY']/ATTRIBUTES/@PER_EACH_ADDITION_OF"></xsl:variable>
				<xsl:if test="normalize-space($VAR_PHO490INCREASEDAMOUNT) = ''">
					<tr>
						<td class="midcolora">
							<li> Increased amount for PHO490 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='SPECIFIC_STRUC_AWAY']/ATTRIBUTES/@PER_EACH_ADDITION_OF") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--
			<xsl:variable name="VAR_TERM_FACTOR"><xsl:call-template name ="TERM_FACTOR"/></xsl:variable>
			<xsl:value-of select="round(($PHO490COST * $PHO490VALUE) div $PHO490INCREASEDAMOUNT * $VAR_TERM_FACTOR)"/>
		-->
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template for HO-490 SPECIFIC STRUCTURED (END)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template for HO-489 SPECIFIC STRUCTURED (START)					  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_489_SPECIFIC_STRUCTURED">
		<xsl:variable name="PHO489VALUE" select="REPAIRCOSTADDITIONAL"></xsl:variable>
		<xsl:variable name="PHO489COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUC_REPAIR']/ATTRIBUTES/@RATE"></xsl:variable>
		<xsl:if test="normalize-space($PHO489COST) = ''">
			<tr>
				<td class="midcolora">
					<li> Cost for HO489 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUC_REPAIR']/ATTRIBUTES/@RATE") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="PHO489INCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUC_REPAIR']/ATTRIBUTES/@PER_EACH_ADDITION_OF"></xsl:variable>
		<xsl:if test="normalize-space($PHO489INCREASEDAMOUNT) = ''">
			<tr>
				<td class="midcolora">
					<li> Increased Amount for HO489 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUC_REPAIR']/ATTRIBUTES/@PER_EACH_ADDITION_OF") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--
	<xsl:variable name="VAR_TERM_FACTOR"><xsl:call-template name ="TERM_FACTOR"/></xsl:variable>
	<xsl:choose>
		<xsl:when test="normalize-space($PHO489VALUE) !='' and $PHO489VALUE &gt; 0"><xsl:value-of select="round(($PHO489VALUE div $PHO489INCREASEDAMOUNT) * $PHO489COST * $VAR_TERM_FACTOR)"/></xsl:when>
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
-->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template for HO-489 SPECIFIC STRUCTURED (END)	  				  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template for PRIMARY RESIDENCE (START)							  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="PRIMARY_RESIDENCE">
		<xsl:variable name="VAR_TERM_FACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="RESIDENCE_EMP_NUMBER = 0.00"></xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR_PRIMARY_RES_EMP_NO" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID ='PRIMARY_RESIDENCE']/@PRIMARY_RES_EMP_NO" />
				<xsl:if test="normalize-space($VAR_PRIMARY_RES_EMP_NO) = ''">
					<tr>
						<td class="midcolora">
							<li> primary residence employee number not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID ='PRIMARY_RESIDENCE']/@PRIMARY_RES_EMP_NO") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:choose>
					<xsl:when test="RESIDENCE_EMP_NUMBER &lt;= $VAR_PRIMARY_RES_EMP_NO">
						<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
						<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
						<xsl:if test="normalize-space($VAR_RATE_PERVAL_MEDPAY) = ''">
							<tr>
								<td class="midcolora">
									<li> medical payement for rate par value not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY") </li>
								</td>
							</tr>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>
						<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='PRIMARY_RESIDENCE']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
						<xsl:if test="normalize-space($VAR_LIABILITY_LIMIT_GP) = ''">
							<tr>
								<td class="midcolora">
									<li> Liablity limit group not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='PRIMARY_RESIDENCE']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP") </li>
								</td>
							</tr>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>
						<xsl:variable name="VAR_FOR_EACHADDITIONAL" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='PRIMARY_RESIDENCE']/@FOR_EACH_ADDITION" />
						<xsl:if test="normalize-space($VAR_FOR_EACHADDITIONAL) = ''">
							<tr>
								<td class="midcolora">
									<li> Each additional for primary residence not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='PRIMARY_RESIDENCE']/@FOR_EACH_ADDITION") </li>
								</td>
							</tr>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>
						<!--
					<xsl:choose> 
						<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0"> 
							<xsl:value-of select="round(($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) * $VAR_TERM_FACTOR)"/>
				 		</xsl:when>
				 		<xsl:otherwise></xsl:otherwise>	
			 		</xsl:choose>
			 		-->
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template for PRIMARY RESIDENCE (END)							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template for RESIDENCE EMPLOYEES (START)						  -->
	<!-- ============================================================================================ -->
	<xsl:template name="RESIDENCE_EMPLOYEES">
		<!-- <xsl:variable name="VAR_TERM_FACTOR"><xsl:call-template name ="TERM_FACTOR"/></xsl:variable> -->
		<xsl:choose>
			<xsl:when test="RESIDENCE_EMP_NUMBER=0.00"></xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR_PRIMARY_RES_EMP_NO" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID ='RESIDENCE_EMPLOYEES']/@PRIMARY_RES_EMP_NO" />
				<xsl:if test="normalize-space($VAR_PRIMARY_RES_EMP_NO) = ''">
					<tr>
						<td class="midcolora">
							<li> primary residence employee number not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID ='PRIMARY_RESIDENCE']/@PRIMARY_RES_EMP_NO") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:choose>
					<xsl:when test="RESIDENCE_EMP_NUMBER &gt;  $VAR_PRIMARY_RES_EMP_NO">
						<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
						<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
						<xsl:if test="normalize-space($VAR_RATE_PERVAL_MEDPAY) = ''">
							<tr>
								<td class="midcolora">
									<li> medical payement for rate par value not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY") </li>
								</td>
							</tr>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>
						<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='RESIDENCE_EMPLOYEES']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
						<xsl:if test="normalize-space($VAR_LIABILITY_LIMIT_GP) = ''">
							<tr>
								<td class="midcolora">
									<li> Liablity limit group not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='PRIMARY_RESIDENCE']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP") </li>
								</td>
							</tr>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>
						<xsl:variable name="VAR_FOR_EACHADDITIONAL">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='RESIDENCE_EMPLOYEES']/@FOR_EACH_ADDITION" />
						</xsl:variable>
						<xsl:if test="normalize-space($VAR_FOR_EACHADDITIONAL) = ''">
							<tr>
								<td class="midcolora">
									<li> Each additional for primary residence not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='PRIMARY_RESIDENCE']/@FOR_EACH_ADDITION") </li>
								</td>
							</tr>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>
						<!--
						<xsl:choose> 
							<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0"> 
								<xsl:value-of select="round(($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) * (RESIDENCE_EMP_NUMBER - $VAR_PRIMARY_RES_EMP_NO)*$VAR_TERM_FACTOR)"/>
			 				</xsl:when>
			 				<xsl:otherwise>0.00</xsl:otherwise>	
			 			</xsl:choose>	
			 		-->
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template for RESIDENCE EMPLOYEES (END)							  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Template for Additional Premises Occupied By Insured (START)				  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADDITIONAL_PREMISES">
		<!-- <xsl:variable name="VAR_TERM_FACTOR"><xsl:call-template name ="TERM_FACTOR"/></xsl:variable> -->
		<xsl:choose>
			<xsl:when test="OCCUPIED_INSURED = 0.00"></xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_OCC_INSURED']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:if test="normalize-space($VAR_LIABILITY_LIMIT_GP) = ''">
					<tr>
						<td class="midcolora">
							<li> Liablity limit group not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='PRIMARY_RESIDENCE']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:if test="normalize-space($VAR_RATE_PERVAL_MEDPAY) = ''">
					<tr>
						<td class="midcolora">
							<li> medical payement for rate par value not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_OCC_INSURED']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_FOR_EACHADDITIONAL) = ''">
					<tr>
						<td class="midcolora">
							<li> Each additional for additional premises not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_OCC_INSURED']/@FOR_EACH_ADDITION") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--	<xsl:choose> 
					<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0"> 
						<xsl:value-of select="round(OCCUPIED_INSURED *($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY)*$VAR_TERM_FACTOR)"/>
			 		</xsl:when>
			 		<xsl:otherwise>0.00</xsl:otherwise>	
			 	</xsl:choose>
			 -->
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Template for Additional Premises Occupied By Insured (END)					  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	Template for HO-40 Additional Premises Rented to Others - Residence Premises  (START)	  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADDITIONAL_RESIDENCE_PREMISES">
		<!-- <xsl:variable name="VAR_TERM_FACTOR"><xsl:call-template name ="TERM_FACTOR"/></xsl:variable> -->
		<xsl:choose>
			<xsl:when test="RESIDENCE_PREMISES = 0.00"></xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:if test="normalize-space($VAR_RATE_PERVAL_MEDPAY) = ''">
					<tr>
						<td class="midcolora">
							<li> medical payement for rate par value not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_RENTED_AT_PREMISES']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:if test="normalize-space($VAR_LIABILITY_LIMIT_GP) = ''">
					<tr>
						<td class="midcolora">
							<li> Liablity limit group not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_RENTED_AT_PREMISES']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_RENTED_AT_PREMISES']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_FOR_EACHADDITIONAL) = ''">
					<tr>
						<td class="midcolora">
							<li> Each additional for additional premises not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_RENTED_AT_PREMISES']/@FOR_EACH_ADDITION") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--
			<xsl:choose> 
				<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0"> 
					<xsl:value-of select="round(RESIDENCE_PREMISES * ($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY)*$VAR_TERM_FACTOR)"/>
			 	</xsl:when>
			 	<xsl:otherwise>0.00</xsl:otherwise>	
			</xsl:choose>	
		-->
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--	Template for HO-40 Additional Premises Rented to Others - Residence Premises  (END)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	Template for HO-70 Additional Premises Rented - Other Location - 1 Family (1)(START)	  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADDITIONAL_OTHER_LOCATION_PREMISES_FAMILY1">
		<!-- <xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable> -->
		<xsl:choose>
			<xsl:when test="OTHER_LOC_1FAMILY = 0.00"></xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:if test="normalize-space($VAR_RATE_PERVAL_MEDPAY) = ''">
					<tr>
						<td class="midcolora">
							<li> medical payement for rate par value not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES1']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:if test="normalize-space($VAR_LIABILITY_LIMIT_GP) = ''">
					<tr>
						<td class="midcolora">
							<li> Liablity limit group not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES1']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES1']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_FOR_EACHADDITIONAL) = ''">
					<tr>
						<td class="midcolora">
							<li> Each additional for rented out side premises1 not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES1']/@FOR_EACH_ADDITION") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--
			 <xsl:choose> 
				<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0"> 
					<xsl:value-of select="round(($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY)*$TERMFACTOR)"/>
			 	</xsl:when>
			 	<xsl:otherwise>0.00</xsl:otherwise>	
			 </xsl:choose>	
			-->
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--	Template for HO-70 Additional Premises Rented - Other Location - 1 Family (1)(END	)	  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	Template for HO-70 Additional Premises Rented - Other Location - 1 Family (2)(START)	  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADDITIONAL_OTHER_LOCATION_PREMISES_FAMILY2">
		<xsl:choose>
			<xsl:when test="OTHER_LOC_2FAMILY = 0.00"></xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:if test="normalize-space($VAR_RATE_PERVAL_MEDPAY) = ''">
					<tr>
						<td class="midcolora">
							<li> medical payement for rate par value not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES2']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:if test="normalize-space($VAR_LIABILITY_LIMIT_GP) = ''">
					<tr>
						<td class="midcolora">
							<li> Liablity limit group for rented outside premises not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES2']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES2']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_FOR_EACHADDITIONAL) = ''">
					<tr>
						<td class="midcolora">
							<li> Each additional for additional premises rented out side not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='ADD_PREMISES_RENTED_OUTSIDE_PREMISES2']/@FOR_EACH_ADDITION") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--
			<xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable>
			<xsl:choose> 
				<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0"> 
					<xsl:value-of select="($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY)*$TERMFACTOR"/>
				</xsl:when>
				<xsl:otherwise>0.00</xsl:otherwise>	
			</xsl:choose>			
		-->
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--	Template for HO-70 Additional Premises Rented - Other Location - 1 Family (3)(END)        -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	Template for HO-42 Incidental Office Private School or Studio On Premises (START)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_42_ON_PREMISES">
		<xsl:choose>
			<xsl:when test="normalize-space(ONPREMISES_HO42) = 'Y'">
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INCIDENTAL_OFCE_ON_PREMISES']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:if test="normalize-space($VAR_LIABILITY_LIMIT_GP) = ''">
					<tr>
						<td class="midcolora">
							<li> Liablity limit group HO42 not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INCIDENTAL_OFCE_ON_PREMISES']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INCIDENTAL_OFCE_ON_PREMISES']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_FOR_EACHADDITIONAL) = ''">
					<tr>
						<td class="midcolora">
							<li> Each additional for HO42  not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INCIDENTAL_OFCE_ON_PREMISES']/@FOR_EACH_ADDITION") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--
            <xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable>
			<xsl:choose> 
				<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0"> 
					<xsl:value-of select="($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) * $TERMFACTOR"/>
				</xsl:when>
				<xsl:otherwise>0.00</xsl:otherwise>	
			</xsl:choose>	
			-->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--	Template for HO-42 Incidental Office Private School or Studio On Premises (END)			  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	Template for HO-42 Other Structure with incidental business coverage readded (START)	  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO42_BUSINESS_OTHER_STRUCTURE_WITH_INCIDENTAL">
		<xsl:choose>
			<xsl:when test="LOCATED_OTH_STRUCTURE = 'Y'">
				<xsl:variable name="VAR_OTHER_STRUCTURE_WITH_INCIDENTAL_HO42" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_WITH_INCIDENTAL_HO42']/ATTRIBUTES/@COST" />
				<xsl:if test="normalize-space($VAR_OTHER_STRUCTURE_WITH_INCIDENTAL_HO42) = ''">
					<tr>
						<td class="midcolora">
							<li> cost for other struct. with incidental HO42 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_WITH_INCIDENTAL_HO42']/ATTRIBUTES/@COST") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_RATEPERVAL_HO42" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_WITH_INCIDENTAL_HO42']/ATTRIBUTES/@RATEPERVAL_HO42" />
				<xsl:if test="normalize-space($VAR_RATEPERVAL_HO42) = ''">
					<tr>
						<td class="midcolora">
							<li> Rate per value for HO42 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='OTHER_STRUCTURE_WITH_INCIDENTAL_HO42']/ATTRIBUTES/@RATEPERVAL_HO42") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--
            <xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable>
			<xsl:value-of select="(((HO48INCLUDE+HO48ADDITIONAL) div $VAR_RATEPERVAL_HO42) * $VAR_OTHER_STRUCTURE_WITH_INCIDENTAL_HO42) * $TERMFACTOR"/>
		-->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--	Template for HO-42 HO-42 Other Structure with incidental business coverage readded (END)  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	Template for HO-42 Incidental Office Private School or Studio Instruction Only (START)	  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_42_INSTRUCTION_ONLY">
		<xsl:choose>
			<xsl:when test="INSTRUCTIONONLY_HO42 = 'Y'">
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INCIDENTAL_OFCE_ON_INSTRUCTN']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:if test="normalize-space($VAR_LIABILITY_LIMIT_GP) = ''">
					<tr>
						<td class="midcolora">
							<li> Liablity limit group for HO42 instruction only not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INCIDENTAL_OFCE_ON_INSTRUCTN']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INCIDENTAL_OFCE_ON_INSTRUCTN']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_FOR_EACHADDITIONAL) = ''">
					<tr>
						<td class="midcolora">
							<li> Each additional for HO42 instruction only not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INCIDENTAL_OFCE_ON_INSTRUCTN']/@FOR_EACH_ADDITION") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--<xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable>
			<xsl:choose> 
				<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0"> 
					<xsl:value-of select="($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) * $TERMFACTOR"/>
				</xsl:when>
				<xsl:otherwise>0.00</xsl:otherwise>	
			</xsl:choose>
	-->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--	Template for HO-42 Incidental Office Private School or Studio Instruction Only (END)	  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	Template for HO-43 Incidental Office Private School or Studio Off Premises  (START)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_43_OFF_PREMISES">
		<xsl:choose>
			<xsl:when test="OFF_PREMISES_HO43 = 'Y'">
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='OFF_PERSUITS']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:if test="normalize-space($VAR_LIABILITY_LIMIT_GP) = ''">
					<tr>
						<td class="midcolora">
							<li> Liablity limit group for HO43 off premises  not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='OFF_PERSUITS']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='OFF_PERSUITS']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_FOR_EACHADDITIONAL) = ''">
					<tr>
						<td class="midcolora">
							<li> Each additional for HO43 off premises  not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='OFF_PERSUITS']/@FOR_EACH_ADDITION") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--
			<xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable>
			<xsl:choose> 
				<xsl:when test="normalize-space($VAR_LIABILITY_LIMIT_GP) != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0"> 
					<xsl:value-of select="($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) div $VAR_RATE_PERVAL_MEDPAY)* $VAR_FOR_EACHADDITIONAL) * $TERMFACTOR "/>
				</xsl:when>
				<xsl:otherwise>0.00</xsl:otherwise>	
			</xsl:choose>
		-->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--	Template for HO-43 Incidental Office Private School or Studio Off Premises  (END)		  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--			Template for HO-71 Business Pursuits Class A - (1 persons)  (START)				  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_71_CLASSA">
		<xsl:choose>
			<xsl:when test="CLERICAL_OFFICE_HO71 = 'Y'">
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSA']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:if test="normalize-space($VAR_LIABILITY_LIMIT_GP) = ''">
					<tr>
						<td class="midcolora">
							<li> Liablity limit group for class-A  not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSA']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSA']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_FOR_EACHADDITIONAL) = ''">
					<tr>
						<td class="midcolora">
							<li> Each additional for class-A  not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSA']/@FOR_EACH_ADDITION") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--
			<xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable>
			<xsl:choose> 
				<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0"> 
					<xsl:value-of select="($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) * $TERMFACTOR"/>
			 	</xsl:when>
			 	<xsl:otherwise>0.00</xsl:otherwise>	
			</xsl:choose>
			-->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--			Template for  HO-71 Business Pursuits -    Class B - (1 persons)  (START)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_71_CLASSB">
		<xsl:choose>
			<xsl:when test="SALESMEN_INC_INSTALLATION = 'Y'">
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSB']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:if test="normalize-space($VAR_LIABILITY_LIMIT_GP) = ''">
					<tr>
						<td class="midcolora">
							<li> Liablity limit group for class-B  not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSB']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSB']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_FOR_EACHADDITIONAL) = ''">
					<tr>
						<td class="midcolora">
							<li> Each additional for class-B  not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSB']/@FOR_EACH_ADDITION") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--
			<xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable>
			<xsl:choose> 
				<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0"> 
					<xsl:value-of select="($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) * $TERMFACTOR"/>
				</xsl:when>
				<xsl:otherwise>0.00</xsl:otherwise>	
			</xsl:choose>	
		-->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--			Template for  HO-71 Business Pursuits -    Class C - (1 persons) (START)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_71_CLASSC">
		<xsl:choose>
			<xsl:when test="TEACHER_ATHELETIC = 'Y'">
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSC']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:if test="normalize-space($VAR_LIABILITY_LIMIT_GP) = ''">
					<tr>
						<td class="midcolora">
							<li> Liablity limit group for class-C  not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSC']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSC']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_FOR_EACHADDITIONAL) = ''">
					<tr>
						<td class="midcolora">
							<li> Each additional for class-C  not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSC']/@FOR_EACH_ADDITION") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--	<xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable>
			<xsl:choose> 
				<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0"> 
					<xsl:value-of select="($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) *$TERMFACTOR"/>
				</xsl:when>
				<xsl:otherwise>0.00</xsl:otherwise>	
			</xsl:choose>	
	-->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--			Template for  HO-71 Business Pursuits -    Class D - (1 persons) (START)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_71_CLASSD">
		<xsl:choose>
			<xsl:when test="TEACHER_NOC = 'Y'">
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSD']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:if test="normalize-space($VAR_LIABILITY_LIMIT_GP) = ''">
					<tr>
						<td class="midcolora">
							<li> Liablity limit group for class-D  not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSD']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSD']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_FOR_EACHADDITIONAL) = ''">
					<tr>
						<td class="midcolora">
							<li> Each additional for class-D  not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='CLASSD']/@FOR_EACH_ADDITION") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--		
			<xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable>
			<xsl:choose> 
				<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0"> 
					<xsl:value-of select="($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) * $TERMFACTOR"/>
				</xsl:when>
				<xsl:otherwise>0.00</xsl:otherwise>	
			</xsl:choose>
		-->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	Template for  HO-72 Farm Liability - Incidental Farming Residence Premises (START)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_72_INCIDENTAL_FARMING">
		<xsl:choose>
			<xsl:when test="INCIDENTAL_FARMING_HO72 = 'Y'">
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INCIDENTAL_FARMING_RESIDENCE']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:if test="normalize-space($VAR_LIABILITY_LIMIT_GP) = ''">
					<tr>
						<td class="midcolora">
							<li> Liablity limit group for incidental farming residence not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INCIDENTAL_FARMING_RESIDENCE']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INCIDENTAL_FARMING_RESIDENCE']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_FOR_EACHADDITIONAL) = ''">
					<tr>
						<td class="midcolora">
							<li> Each additional for incidental farming residence  not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INCIDENTAL_FARMING_RESIDENCE']/@FOR_EACH_ADDITION") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--
			<xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable>
			<xsl:choose> 
				<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0"> 
					<xsl:value-of select="($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) * $TERMFACTOR"/>
				</xsl:when>
				<xsl:otherwise>0.00</xsl:otherwise>	
			</xsl:choose>
-->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	Template for  HO-73 Farm Liability - Owned By Insured Rented to Others (2) (START)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_73_OTH_LOC_OPR_OTHERS">
		<xsl:variable name="PHO73" select="OTH_LOC_OPR_OTHERS_HO73" />
		<xsl:choose>
			<xsl:when test="$PHO73 &gt; 0">
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='RENTED_TO_OTHERS']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:if test="normalize-space($VAR_LIABILITY_LIMIT_GP) = ''">
					<tr>
						<td class="midcolora">
							<li> Liablity limit group for rented to others not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='RENTED_TO_OTHERS']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='RENTED_TO_OTHERS']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_FOR_EACHADDITIONAL) = ''">
					<tr>
						<td class="midcolora">
							<li> Each additional for rented to others not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='RENTED_TO_OTHERS']/@FOR_EACH_ADDITION") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--
			<xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable>
			<xsl:choose> 
				<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0"> 
					<xsl:value-of select="(($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) * OTH_LOC_OPR_OTHERS_HO73)* $TERMFACTOR"/>
				</xsl:when>
				<xsl:otherwise>0.00</xsl:otherwise>	
			</xsl:choose>
			-->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--	Template for  HO-73 Farm Liability - Owned By Insured Rented to Others (2) (END)		  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--	Template for  HO-73 Farm Liability - Owned/Operated By Insured/Insured Employee (START)	  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_73_OTH_LOC_OPR_EMPL">
		<xsl:variable name="PHO73" select="OTH_LOC_OPR_EMPL_HO73" />
		<xsl:choose>
			<xsl:when test="$PHO73 &gt; 0">
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='OPERATED_BY_INSURED']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:if test="normalize-space($VAR_LIABILITY_LIMIT_GP) = ''">
					<tr>
						<td class="midcolora">
							<li> Liablity limit group for operated by insured not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='OPERATED_BY_INSURED']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='OPERATED_BY_INSURED']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_FOR_EACHADDITIONAL) = ''">
					<tr>
						<td class="midcolora">
							<li> Each additional for operated by insured not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='OPERATED_BY_INSURED']/@FOR_EACH_ADDITION") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--
				<xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable>
				<xsl:choose> 
					<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0"> 
						<xsl:value-of select="(($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) * OTH_LOC_OPR_EMPL_HO73 )*$TERMFACTOR"/>
			 		</xsl:when>
			 		<xsl:otherwise></xsl:otherwise>	
			 	</xsl:choose>
		-->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--	Template for  HO-73 Farm Liability - Owned/Operated By Insured/Insured Employee (END)	  -->
	<!--		    					  FOR MICHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--						Template for  HO-82 Personal Injury (START)							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_82_PERSONAL_INJURY">
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="normalize-space(PIP_HO82) = 'Y'">
				<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
				<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
				<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='PERSONAL_INJURY_COVERAGE']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
				<xsl:if test="normalize-space($VAR_LIABILITY_LIMIT_GP) = ''">
					<tr>
						<td class="midcolora">
							<li> Personal injury  group not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='PERSONAL_INJURY_COVERAGE']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_FOR_EACHADDITIONAL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='PERSONAL_INJURY_COVERAGE']/@FOR_EACH_ADDITION" />
				</xsl:variable>
				<xsl:if test="normalize-space($VAR_FOR_EACHADDITIONAL) = ''">
					<tr>
						<td class="midcolora">
							<li> Each additional for Personal injury not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='PERSONAL_INJURY_COVERAGE']/@FOR_EACH_ADDITION") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--
			<xsl:variable name="TERMFACTOR"><xsl:call-template name="TERM_FACTOR" /></xsl:variable>
			<xsl:choose> 
				<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0"> 
					<xsl:value-of select=" ($VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACHADDITIONAL) div $VAR_RATE_PERVAL_MEDPAY) *$VAR_TERMFACTOR"/>
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>	
			</xsl:choose>
		-->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--						Template INLAND MARINE Bicycles (HO-900) (START)					  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_900_BICYCLES_PREMIUM">
		<xsl:choose>
			<xsl:when test="SCH_BICYCLE_AMOUNT != '' and SCH_BICYCLE_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_DED_AMT" select="SCH_BICYCLE_DED" />
				<!-- <xsl:variable name="VAR_TERMFACTOR"><xsl:call-template name ="TERM_FACTOR"/></xsl:variable> -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<xsl:if test="normalize-space($VAR_RATE_PERVAL_IM) = ''">
					<tr>
						<td class="midcolora">
							<li> Rate per value for Inland marine not found  (Path - "FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='BICYCLES']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:if test="normalize-space($VAR_DED_FACTOR) = ''">
					<tr>
						<td class="midcolora">
							<li> Deductible factor for bicycles not found  (Path - "FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='BICYCLES']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--	<xsl:value-of select="round(round((SCH_BICYCLE_AMOUNT  div $VAR_RATE_PERVAL_IM)* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)"/>   -->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--						Template INLAND MARINE Bicycles (HO-900) (END	)					  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--						Template INLAND MARINE Cameras Non-Professional  (START)			  -->
	<!-- ============================================================================================ -->
	<xsl:template name="CAMERAS_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_CAMERA_AMOUNT) != '' and SCH_CAMERA_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<!-- <xsl:variable name="VAR_TERMFACTOR"><xsl:call-template name ="TERM_FACTOR"/></xsl:variable> -->
				<xsl:variable name="VAR_DED_AMT" select="SCH_CAMERA_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='CAMERA']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:if test="normalize-space($VAR_DED_FACTOR) = ''">
					<tr>
						<td class="midcolora">
							<li> Deductible factor for camera not found  (Path - "FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='CAMERA']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!-- <xsl:value-of select="round(round((SCH_CAMERA_AMOUNT div $VAR_RATE_PERVAL_IM) * $VAR_DED_FACTOR) * $VAR_TERMFACTOR)"/>      -->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--						Template INLAND MARINE Cameras Non-Professional  (END)				  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--						Template INLAND MARINE Cellular Phones (HO-900)  (START)			  -->
	<!-- ============================================================================================ -->
	<xsl:template name="HO_900_CELLULAR_PHONES_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_CELL_AMOUNT) != '' and SCH_CELL_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<!--<xsl:variable name="VAR_TERMFACTOR"><xsl:call-template name ="TERM_FACTOR"/></xsl:variable> -->
				<xsl:variable name="VAR_DED_AMT" select="SCH_CELL_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='CELLULAR']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:if test="normalize-space($VAR_DED_FACTOR) = ''">
					<tr>
						<td class="midcolora">
							<li> Deductible factor for cellular not found  (Path - "FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='CELLULAR']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!-- <xsl:value-of select="round(round((SCH_CELL_AMOUNT div $VAR_RATE_PERVAL_IM ) * $VAR_DED_FACTOR) * $VAR_TERMFACTOR)"/>   -->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--						Template INLAND MARINE Cellular Phones (HO-900)  (END	)			  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template INLAND MARINE  Furs  (START)							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="FURS_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_FURS_AMOUNT) != '' and SCH_FURS_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<!-- <xsl:variable name="VAR_TERMFACTOR"><xsl:call-template name ="TERM_FACTOR"/></xsl:variable> -->
				<xsl:variable name="VAR_DED_AMT" select="SCH_FURS_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='FURS']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:if test="normalize-space($VAR_DED_FACTOR) = ''">
					<tr>
						<td class="midcolora">
							<li> Deductible factor for furs not found  (Path - "FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='FURS']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!-- <xsl:value-of select=" round( round((SCH_FURS_AMOUNT div $VAR_RATE_PERVAL_IM) * $VAR_DED_FACTOR)* $VAR_TERMFACTOR)"/> -->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template INLAND MARINE  Furs  (END)								  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template INLAND MARINE  Golf Equipment  (START)					  -->
	<!-- ============================================================================================ -->
	<xsl:template name="GOLF_EQUIPMENT_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_GOLF_AMOUNT) != '' and SCH_GOLF_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<!-- <xsl:variable name="VAR_TERMFACTOR"><xsl:call-template name ="TERM_FACTOR"/></xsl:variable> -->
				<xsl:variable name="VAR_DED_AMT" select="SCH_GOLF_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='GOLFEQUIP']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:if test="normalize-space($VAR_DED_FACTOR) = ''">
					<tr>
						<td class="midcolora">
							<li> Deductible factor for golf equipment  not found  (Path - "FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='GOLFEQUIP']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!-- <xsl:value-of select="round(round((SCH_GOLF_AMOUNT div $VAR_RATE_PERVAL_IM) * $VAR_DED_FACTOR) * $VAR_TERMFACTOR)"/>      -->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template INLAND MARINE  Golf Equipment  (END)					  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template INLAND MARINE  Guns (START)							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="GUNS_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_GUNS_AMOUNT) != '' and SCH_GUNS_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<!--		<xsl:variable name="VAR_TERMFACTOR"><xsl:call-template name ="TERM_FACTOR"/></xsl:variable> -->
				<xsl:variable name="VAR_DED_AMT" select="SCH_GUNS_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='GUNS']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:if test="normalize-space($VAR_DED_FACTOR) = ''">
					<tr>
						<td class="midcolora">
							<li> Deductible factor for guns not found  (Path - "FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='GUNS']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!-- <xsl:value-of select="round(round((SCH_GUNS_AMOUNT div $VAR_RATE_PERVAL_IM) * $VAR_DED_FACTOR) * $VAR_TERMFACTOR)"/>     -->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template INLAND MARINE  Guns (END)								  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template INLAND MARINE  Jewelry (START)							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="JEWELRY_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_JWELERY_AMOUNT) != '' and SCH_JWELERY_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<!-- <xsl:variable name="VAR_TERMFACTOR"><xsl:call-template name ="TERM_FACTOR"/></xsl:variable> -->
				<xsl:variable name="VAR_DED_AMT" select="SCH_JWELERY_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='JEWELERY']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:if test="normalize-space($VAR_DED_FACTOR) = ''">
					<tr>
						<td class="midcolora">
							<li> Deductible factor for jewelery not found  (Path - "FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='JEWELERY']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!-- <xsl:value-of select="round(round((SCH_JWELERY_AMOUNT div $VAR_RATE_PERVAL_IM) * $VAR_DED_FACTOR) * $VAR_TERMFACTOR)"/>     -->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template INLAND MARINE  Jewelry (END)							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--			Template INLAND MARINE  Musical (Non-Professional) (START)						  -->
	<!-- ============================================================================================ -->
	<xsl:template name="MUSICAL_NON_PROFESSIONAL_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_MUSICAL_AMOUNT) != '' and SCH_MUSICAL_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<!--<xsl:variable name="VAR_TERMFACTOR"><xsl:call-template name ="TERM_FACTOR"/></xsl:variable> -->
				<xsl:variable name="VAR_DED_AMT" select="SCH_MUSICAL_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='MUSICAL']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:if test="normalize-space($VAR_DED_FACTOR) = ''">
					<tr>
						<td class="midcolora">
							<li> Deductible factor for musical non proff. not found  (Path - "FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='MUSICAL']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!-- <xsl:value-of select="round(round((SCH_MUSICAL_AMOUNT div $VAR_RATE_PERVAL_IM ) * $VAR_DED_FACTOR) * $VAR_TERMFACTOR)"/> -->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--			Template INLAND MARINE  Musical (Non-Professional) (END)						  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--			Template INLAND MARINE  Personal Computers (HO-214) (START)						  -->
	<!-- ============================================================================================ -->
	<xsl:template name="PERSONAL_COMPUTERS_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_PERSCOMP_AMOUNT) != '' and SCH_PERSCOMP_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<!--<xsl:variable name="VAR_TERMFACTOR"><xsl:call-template name ="TERM_FACTOR"/></xsl:variable> -->
				<xsl:variable name="VAR_DED_AMT" select="SCH_PERSCOMP_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='PC']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:if test="normalize-space($VAR_DED_FACTOR) = ''">
					<tr>
						<td class="midcolora">
							<li> Deductible factor for PC not found  (Path - "FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='PC']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!-- <xsl:value-of select="round(round((SCH_PERSCOMP_AMOUNT  div $VAR_RATE_PERVAL_IM)* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)"/>     -->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--			Template INLAND MARINE  Personal Computers (HO-214) (END)						  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template INLAND MARINE  Silver (START)							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="SILVER_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_SILVER_AMOUNT) != '' and SCH_SILVER_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<!-- <xsl:variable name="VAR_TERMFACTOR"><xsl:call-template name ="TERM_FACTOR"/></xsl:variable> -->
				<xsl:variable name="VAR_DED_AMT" select="SCH_SILVER_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='SILVER']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:if test="normalize-space($VAR_DED_FACTOR) = ''">
					<tr>
						<td class="midcolora">
							<li> Deductible factor for Silver not found  (Path - "FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='SILVER']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--	<xsl:value-of select="round(round((SCH_SILVER_AMOUNT div $VAR_RATE_PERVAL_IM) * $VAR_DED_FACTOR)   * $VAR_TERMFACTOR)"/>     -->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template INLAND MARINE  Silver (END)							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template INLAND MARINE  Stamps (START)							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="STAMPS_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_STAMPS_AMOUNT) != '' and SCH_STAMPS_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<!-- <xsl:variable name="VAR_TERMFACTOR"><xsl:call-template name ="TERM_FACTOR"/></xsl:variable>  -->
				<xsl:variable name="VAR_DED_AMT" select="SCH_STAMPS_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='STAMPS']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:if test="normalize-space($VAR_DED_FACTOR) = ''">
					<tr>
						<td class="midcolora">
							<li> Deductible factor for Stamps not found  (Path - "FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='STAMPS']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--			<xsl:value-of select="round(round((SCH_STAMPS_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)"/>      -->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template INLAND MARINE  Stamps (END)							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template INLAND MARINE  Rare Coins  (START)						  -->
	<!-- ============================================================================================ -->
	<xsl:template name="RARE_COINS_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_RARECOINS_AMOUNT) != '' and SCH_RARECOINS_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<!-- 	<xsl:variable name="VAR_TERMFACTOR"><xsl:call-template name ="TERM_FACTOR"/></xsl:variable> -->
				<xsl:variable name="VAR_DED_AMT" select="SCH_RARECOINS_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='COINS']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:if test="normalize-space($VAR_DED_FACTOR) = ''">
					<tr>
						<td class="midcolora">
							<li> Deductible factor for rare coins not found  (Path - "FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='COINS']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--	<xsl:value-of select="round(round((SCH_RARECOINS_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)"/>      -->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template INLAND MARINE  Rare Coins  (END)						  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Template INLAND MARINE  Fine Arts without Breakage (START)				  -->
	<!-- ============================================================================================ -->
	<xsl:template name="FINE_ARTS_WITHOUT_BREAKAGE_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_FINEARTS_WO_BREAK_AMOUNT) != '' and SCH_FINEARTS_WO_BREAK_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<!-- <xsl:variable name="VAR_TERMFACTOR"><xsl:call-template name ="TERM_FACTOR"/></xsl:variable> -->
				<xsl:variable name="VAR_DED_AMT" select="SCH_FINEARTS_WO_BREAK_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='FINEARTSWITHOUTBREAKAGE']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:if test="normalize-space($VAR_DED_FACTOR) = ''">
					<tr>
						<td class="midcolora">
							<li> Deductible for finearts (without breakage) not found  (Path - "FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='FINEARTSWITHOUTBREAKAGE']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--<xsl:value-of select="round(round((SCH_FINEARTS_WO_BREAK_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)"/>     -->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Template INLAND MARINE  Fine Arts without Breakage (END)				  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--					Template INLAND MARINE  Fine Arts without Breakage  (START)				  -->
	<!-- ============================================================================================ -->
	<xsl:template name="FINE_ARTS_BREAKAGE_PREMIUM">
		<xsl:choose>
			<xsl:when test="normalize-space(SCH_FINEARTS_BREAK_AMOUNT) != '' and SCH_FINEARTS_BREAK_AMOUNT &gt; 0"> <!-- To be given only when the amount exceeds 0 -->
				<xsl:variable name="VAR_RATE_PERVAL_IM" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='RATE_PERVAL_INLAND_MARINE']/ATTRIBUTES/@RATE_PERVAL_IM" />
				<!-- <xsl:variable name="VAR_TERMFACTOR"><xsl:call-template name ="TERM_FACTOR"/></xsl:variable> -->
				<xsl:variable name="VAR_DED_AMT" select="SCH_FINEARTS_BREAK_DED" />
				<xsl:variable name="VAR_DED_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='FINEARTSWITHBREAKAGE']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP" />
				<xsl:if test="normalize-space($VAR_DED_FACTOR) = ''">
					<tr>
						<td class="midcolora">
							<li> Deductible for finearts (with breakage) not found  (Path - "FACTOR[@ID='ENDORSEMENTS']/NODE[@ID='FINEARTSWITHBREAKAGE']/ATTRIBUTES[@DED_AMT = $VAR_DED_AMT]/@DED_GROUP") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!-- <xsl:value-of select="round(round((SCH_FINEARTS_BREAK_AMOUNT  div $VAR_RATE_PERVAL_IM )* $VAR_DED_FACTOR) * $VAR_TERMFACTOR)"/>      -->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Template INLAND MARINE  Fine Arts without Breakage  (END)				  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template Property Expense Fee (START)							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="PROPFEE">
		<xsl:variable name="TERM" select="TERMFACTOR" />
		<xsl:variable name="VAR_PROPFEE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PROPRTYFEE']/NODE[@ID='PFEE']/ATTRIBUTES[@TERMMONTHS =$TERM]/@FEES" />
		</xsl:variable>
		<xsl:if test="normalize-space($VAR_PROPFEE) = ''">
			<tr>
				<td class="midcolora">
					<li> property fee not found (Path - "FACTOR[@ID='PROPRTYFEE']/NODE[@ID='PFEE']/ATTRIBUTES[@TERMMONTHS =$TERM]/@FEES") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template Property Expense Fee (START)							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template SEASONAL SECONDARY CREDIT  (START)						  -->
	<!--		    							FOR MICHIGAN and INDIANA							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="SEASONALSECONDARYCREDIT">
		<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
		<xsl:variable name="VAR_INSURE_PRIMARY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INSURE_PRIMARY']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@GROUP" />
		<xsl:if test="normalize-space($VAR_INSURE_PRIMARY) = ''">
			<tr>
				<td class="midcolora">
					<li> Insure primary group not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INSURE_PRIMARY']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@GROUP") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_INSURE_NON_PRIMARY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INSURE_NON_PRIMARY']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@GROUP" />
		<xsl:if test="normalize-space($VAR_INSURE_NON_PRIMARY) = ''">
			<tr>
				<td class="midcolora">
					<li> Insure non primary group not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='INSURE_NON_PRIMARY']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@GROUP") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--					
		<xsl:choose> 
			<xsl:when test="SEASONALSECONDARY='Y' and WOLVERINEINSURESPRIMARY='Y'"> 
				<xsl:choose> 
					<xsl:when test="normalize-space($VAR_INSURE_PRIMARY) != '' and $VAR_INSURE_PRIMARY &gt; 0"> 
						<xsl:value-of select="$VAR_INSURE_PRIMARY"/>
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>	
				</xsl:choose>

			</xsl:when>
			<xsl:when test="SEASONALSECONDARY='Y' and WOLVERINEINSURESPRIMARY='N'"> 
				<xsl:choose> 
					<xsl:when test="normalize-space($VAR_INSURE_NON_PRIMARY) != '' and $VAR_INSURE_NON_PRIMARY &gt; 0"> 
							<xsl:value-of select="$VAR_INSURE_NON_PRIMARY"/>
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>	
				</xsl:choose>		
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>	
		</xsl:choose>
		
		-->
	</xsl:template>
	<xsl:template name="SEASONALSECONDARY_TEXT">
		<xsl:choose>
			<xsl:when test="SEASONALSECONDARY = 'Y' and WOLVERINEINSURESPRIMARY='Y'">
			-  Credit - Seasonal/Secondary,Wolverine Insures Primary $
		</xsl:when>
			<xsl:when test="SEASONALSECONDARY = 'Y' and WOLVERINEINSURESPRIMARY='N'">
			-  Credit - Seasonal/Secondary, $ 
		</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template SEASONAL SECONDARY CREDIT  (START)						  -->
	<!--		    							FOR MICHIGAN and INDIANA							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--							Template Described Primary Residence Coverage  (START)			  -->
	<!--		    							FOR MICHIGAN and INDIANA							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="DESCRIBED_PRIMARY_RESIDENCE">
		<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
		<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='DESCRIBED_PRIMARY_RESIDENCE']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
		<xsl:variable name="VAR_FOR_EACHADDITIONAL">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='DESCRIBED_PRIMARY_RESIDENCE']/@FOR_EACH_ADDITION" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="normalize-space($VAR_LIABILITY_LIMIT_GP) != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0 and $VAR_LIABILITY_LIMIT_GP != 'Included'">
				<xsl:value-of select="$VAR_LIABILITY_LIMIT_GP + ((MEDICALPAYMENTSTOOTHERS_LIMIT - 1000)* $VAR_FOR_EACHADDITIONAL) div 1000" />
			</xsl:when>
			<xsl:when test="$VAR_LIABILITY_LIMIT_GP = 'Included'">
				<xsl:value-of select="((MEDICALPAYMENTSTOOTHERS_LIMIT - 1000) * $VAR_FOR_EACHADDITIONAL) div 1000" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							Template SEASONAL SECONDARY CREDIT  (START)						  -->
	<!--		    							FOR MICHIGAN and INDIANA							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--									Template Smoker Factor (START)							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="SMOKER">
		<xsl:choose>
			<xsl:when test="NONSMOKER = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='NONSMOKERCREIT']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SMOKER_CREDIT_DISPLAY">
		<xsl:choose>
			<xsl:when test="NONSMOKER = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='NONSMOKERCREIT']/ATTRIBUTES/@CREDIT" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--									Template Smoker Factor (END)							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--									50% Discount Applied (START)							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="MAX_DISCOUNT_APPLIED_DISPLAY">
		<xsl:variable name="VAR1">
			<xsl:call-template name="AGEOFHOME_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR4">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR5">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="MAX_APPLICABLE_CREDIT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
		<xsl:variable name="MAX_APPLICABLE_CREDIT_PERCENT" select="(1.00-$MAX_APPLICABLE_CREDIT)*100" />
		<!-- Check the applied total percentage -->
		<!--
	<xsl:variable name="FINALCREDIT_EXPERIENCE_PERCENT">
		<xsl:value-of select="((1-$VAR1)*100)+((1-$VAR2)*100)+((1-$VAR3)*100)+((1-$VAR4)*100)+((1-$VAR5)*100)"/> 
	</xsl:variable>
	<xsl:variable name="FINALCREDIT_EXPERIENCE">
		<xsl:value-of select="((100.00-$FINALCREDIT_EXPERIENCE_PERCENT) div 100.00)"/> 
	</xsl:variable>
	
	<xsl:choose>
		<xsl:when test ="STATENAME = 'INDIANA'">
			0.00
		</xsl:when>
		<xsl:otherwise>
			<xsl:choose> 
				<xsl:when test="$FINALCREDIT_EXPERIENCE &gt; $MAX_APPLICABLE_CREDIT_PERCENT"><xsl:value-of select="$FINALCREDIT_EXPERIENCE"/></xsl:when>
				<xsl:otherwise>0.00</xsl:otherwise>
			</xsl:choose>
		</xsl:otherwise>
	</xsl:choose>
	-->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--									50% Discount Applied (END)	    						  -->
	<!-- ============================================================================================ -->
	<xsl:template name="SELECT_HO_20_PREFERRED_PLUS_OR_HO_21_VIP">
		<xsl:choose>
			<xsl:when test="normalize-space(HO20) ='Y'">HO-20 Preferred Plus</xsl:when>
			<xsl:when test="normalize-space(HO25) ='Y'">HO-25 Premier V.I.P</xsl:when>
			<xsl:when test="normalize-space(HO21) ='Y'">HO-22 Preferred Plus VIP</xsl:when>
			<xsl:when test="normalize-space(HO23) ='Y' and PRODUCTNAME = $POLICYTYPE_HO5">HO-23 Preferred Plus VIP</xsl:when>
			<xsl:when test="normalize-space(HO23) ='Y'and PRODUCTNAME != $POLICYTYPE_HO5">HO-24 Premier VIP</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Prior Loss Label Display -->
	<xsl:template name="PRIOR_LOSS_LABEL_DISPLAY">
		<xsl:choose>
			<xsl:when test="PRIOR_LOSS_SURCHARGE = 'Y'">
		(
				((((((
						(
							(<xsl:call-template name="DEWLLING-MAIN"></xsl:call-template>)
							*(<xsl:call-template name="C-VALUE"></xsl:call-template>)
							*(<xsl:call-template name="INSURANCE_SCORE_CREDIT"></xsl:call-template>)
							*(<xsl:call-template name="TERM_FACTOR"></xsl:call-template>)
						)
						+
						(
							(<xsl:call-template name="COVERAGE_C_PREMIUM" /> )
							+ (<xsl:call-template name="COVERAGE_D_PREMIUM" />)
						)
					    
				)
				*
				(<xsl:call-template name="DFACTOR" />)
		)*
				(<xsl:call-template name="MULTIPOLICY_FACTOR" />)
			)*
			   
			(<xsl:call-template name="AGEOFHOME_FACTOR" />))*
			(<xsl:call-template name="PROTECTIVEDEVICE" />))*
			(<xsl:call-template name="EXPERIENCEFACTOR" />))*
			(<xsl:call-template name="PRIOR_LOSS" />)
			-
			(((((
					(
						(<xsl:call-template name="DEWLLING-MAIN"></xsl:call-template>)
						*(<xsl:call-template name="C-VALUE"></xsl:call-template>)
						*(<xsl:call-template name="INSURANCE_SCORE_CREDIT"></xsl:call-template>)
						*(<xsl:call-template name="TERM_FACTOR"></xsl:call-template>)
						
					)
					+
					(
						(<xsl:call-template name="COVERAGE_C_PREMIUM" /> )
						+ (<xsl:call-template name="COVERAGE_D_PREMIUM" />)
					)
				    
			)
			*
			(<xsl:call-template name="DFACTOR" />)
			)*
				(<xsl:call-template name="MULTIPOLICY_FACTOR" />)
			)*
			   
			(<xsl:call-template name="AGEOFHOME_FACTOR" />))*
			(<xsl:call-template name="PROTECTIVEDEVICE" />))*
			(<xsl:call-template name="EXPERIENCEFACTOR" />)
			)
		</xsl:when>
			<xsl:otherwise>
		0
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--									Template for Lables  (START)							  -->
	<!-- ============================================================================================ -->
	<!--									   Group Details										  -->
	<xsl:template name="PRODUCTID0"><xsl:value-of select="PRODUCTNAME" />  - <xsl:value-of select="PRODUCT_PREMIER" /></xsl:template>
	<xsl:template name="GROUPID0">
	<xsl:choose>
			<xsl:when test="SEASONALSECONDARY ='Y'">Seasonal/Secondary Residence,</xsl:when>
			<xsl:otherwise>Primary Residence,</xsl:otherwise>
		</xsl:choose>
<xsl:value-of select="EXTERIOR_CONSTRUCTION_DESC" />(<xsl:value-of select="DOC" />)</xsl:template>
	<xsl:template name="GROUPID1">Fire Class: <xsl:value-of select="PROTECTIONCLASS" /> Hydrant: <xsl:value-of select="FEET2HYDRANT" />, Fire Department: <xsl:value-of select="DISTANCET_FIRESTATION" /> miles</xsl:template>
	<xsl:template name="GROUPID2">Premium Group: <xsl:call-template name="PREMIUM_GROUP" /> , Rated Class:  <xsl:value-of select="PROTECTIONCLASS" />-<xsl:value-of select="EXTERIOR_CONSTRUCTION_F_M" />, 
								<xsl:choose>
			<xsl:when test="PRODUCTNAME =$POLICYTYPE_HO4 or PRODUCTNAME =$POLICYTYPE_HO6"> Units - <xsl:value-of select="NUMBEROFUNITS" /></xsl:when>
			<xsl:otherwise> Family - <xsl:value-of select="NUMBEROFFAMILIES" /></xsl:otherwise>
		</xsl:choose></xsl:template>
	<xsl:template name="GROUPID3">SECTION I - PROPERTY COVERAGES</xsl:template>
	<xsl:template name="GROUPID4">SECTION II - LIABILITY COVERAGES </xsl:template>
	<xsl:template name="GROUPID5">ADDITIONAL COVERAGES</xsl:template>
	<xsl:template name="GROUPID6">INLAND MARINE - SCHEDULED PERSONAL PROPERTY (HO-61)</xsl:template>
	<!--Step Details-->
	<xsl:template name="STEPID0">		-  Coverage A - Dwelling </xsl:template>
	<xsl:template name="STEPID1">		-  Coverage B - Other Structures</xsl:template>
	<xsl:template name="STEPID2">		-  Coverage C - Personal Property</xsl:template>
	<xsl:template name="STEPID3">		-  Coverage D - Loss Of Use  </xsl:template>
	<xsl:template name="STEPID4">		-  Coverage E - Personal Liability - Each Occurrence</xsl:template>
	<xsl:template name="STEPID5">		-  Coverage F - Medical Payments to Others - Each Person</xsl:template>
	<xsl:template name="STEPID6">		-  Discount - Insurance Score Credit (Score - <xsl:call-template name="INSURANCE_SCORE_DISPLAY" />) - <xsl:call-template name="INSURANCESCORE_PERCENT" /> </xsl:template>
	<!--New Steps Added START-->
	<xsl:template name="STEPID7">		-  Discount - Non smoker <xsl:call-template name="SMOKER_CREDIT_DISPLAY" />%</xsl:template>
	<xsl:template name="STEPID8">		-  NOTE: Discounts have 50% MAXIMUM DISCOUNT rule applied</xsl:template>
	<!--New Steps Added END -->
	<xsl:template name="STEPID9">		-  Discount - Under Construction <xsl:call-template name="DWELLING_UNDER_CONSTRUCTION_CREDIT"></xsl:call-template>%</xsl:template>
	<xsl:template name="STEPID10">	-  Discount - Experience <xsl:call-template name="EXPERIENCECREDIT"></xsl:call-template>%</xsl:template>
	<xsl:template name="STEPID11">	-  Discount - Age of Home <xsl:call-template name="AGEOFHOME_CREDIT" />%</xsl:template>
	<xsl:template name="STEPID12">    -  Discount - Protective Devices    <xsl:call-template name="PROTECTIVEDEVICE_CREDIT" />%</xsl:template>
	<xsl:template name="STEPID13">	-  Discount - Multi Policy <xsl:call-template name="MULTIPOLICY_CREDIT" />%</xsl:template>
	<xsl:template name="STEPID14">	-  Discount - Valued Customer <xsl:call-template name="VALUEDCUSTOMER_FACTOR"></xsl:call-template>%</xsl:template>
	<xsl:template name="STEPID15">	-  Charge	- Insured to 80% to 99% of Replacement Cost</xsl:template>
	<xsl:template name="STEPID16">
		<xsl:call-template name="PRIOR_LOSS_LABEL_VALUE" />
	</xsl:template>
	<xsl:template name="STEPID17">
		<xsl:call-template name="WSTOVE_SURCHARGE" />
	</xsl:template>
	<xsl:template name="STEPID18">	-  Charge	- Breed of Dog <xsl:call-template name="NO_OF_DOGS" />  <xsl:call-template name="DOGS_CHARGE_INPERCENT" />%</xsl:template>
	<xsl:template name="STEPID19">
		<xsl:call-template name="SEASONALSECONDARY_TEXT" />
		<xsl:call-template name="SEASONALSECONDARYCREDIT" />
	</xsl:template>
	<xsl:template name="STEPID20">	-  <xsl:call-template name="SELECT_HO_20_PREFERRED_PLUS_OR_HO_21_VIP"></xsl:call-template></xsl:template>
	<xsl:template name="STEPID21">	-  HO-34 Replacement Cost Personal Property</xsl:template>
	<!-- <xsl:template name = "STEPID1001">	-  HO-64 Renters Deluxe Endorsement </xsl:template> -->
	<xsl:template name="STEPID22">	-  HO-11 Expanded Replacement Cost for Building</xsl:template>
	<xsl:template name="STEPID23">	-  HO-96 Fire Department Service Charge (<xsl:value-of select="HO96ADDITIONAL" /> increase)</xsl:template>
	<xsl:template name="STEPID24">	-  HO-312 Business Property Increase Limits (<xsl:value-of select="HO312ADDITIONAL" /> increase)</xsl:template>
	<xsl:template name="STEPID25">	-  HO-48 Other Structures Increased Limit</xsl:template>
	<xsl:template name="STEPID26">	-  HO-40 Other Structure Rented To Others (<xsl:value-of select="HO40ADDITIONAL" /> increase)</xsl:template>
	<xsl:template name="STEPID27">	-  HO-42 Other Structure With Incidental</xsl:template>
	<xsl:template name="STEPID28">	-  HO-53 Credit Card and Depositors Forgery (<xsl:value-of select="HO53ADDITIONAL" /> increase)</xsl:template>
	<xsl:template name="STEPID29">	-    Unscheduled Jewelry And Furs (<xsl:value-of select="UNSCHEDULEDJEWELRYADDITIONAL" /> increase)</xsl:template>
	<xsl:template name="STEPID30">	-    Money (<xsl:value-of select="MONEYADDITIONAL" /> increase)</xsl:template>
	<xsl:template name="STEPID31">	-    Securities (<xsl:value-of select="SECURITIESADDITIONAL" /> increase)</xsl:template>
	<xsl:template name="STEPID32">	-    Silverware, Goldware And Pewterware (<xsl:value-of select="SILVERWAREADDITIONAL" /> increase)</xsl:template>
	<xsl:template name="STEPID33">	-    Firearms (<xsl:value-of select="FIREARMSADDITIONAL" /> increase)</xsl:template>
	<xsl:template name="STEPID34">	-  HO-277 Ordinance or Law</xsl:template>
	<xsl:template name="STEPID35">	-  HO-455 Identity Fraud Expense Coverage</xsl:template>
	<xsl:template name="STEPID36">	-  HO-315 Earthquake (Zone <xsl:value-of select="TERRITORYZONE" />)</xsl:template>
	<xsl:template name="STEPID37">	-  HO-327 Water Back-Up and Sump Pump Overflow</xsl:template>
	<xsl:template name="STEPID38">	-  HO-33 Condo-Unit Owners Rental to Others <xsl:value-of select="HO33" /> </xsl:template>
	<xsl:template name="STEPID39">	-  HO-373 Contingent Workers Compensation</xsl:template>
	<xsl:template name="STEPID40">	-  Mine Subsidence Coverage</xsl:template>
	<xsl:template name="STEPID41">	-  HO-9 Collapse From Sub-Surface Water</xsl:template>
	<xsl:template name="STEPID42">	-  HO-490 Specific Structures Away From Premises</xsl:template>
	<xsl:template name="STEPID43">	-  HO-489 Other Structures Repair Cost Coverage  (<xsl:value-of select="REPAIRCOSTADDITIONAL" /> increase)</xsl:template>
	<xsl:template name="STEPID44">	-  Primary Residence</xsl:template>
	<xsl:template name="STEPID45">	-  Residence Employees(<xsl:value-of select="RESIDENCE_EMP_NUMBER" />)</xsl:template>
	<xsl:template name="STEPID46">	-  Additional Premises Occupied By Insured (<xsl:value-of select="format-number(OCCUPIED_INSURED, '#')" />)</xsl:template>
	<xsl:template name="STEPID47">	-  HO-40 Additional Premises Rented to Others - Residence Premises (<xsl:value-of select="format-number(RESIDENCE_PREMISES,'#')"></xsl:value-of>)</xsl:template>
	<xsl:template name="STEPID48">	-  HO-70 Additional Premises Rented - Other Location - 1 Family (<xsl:value-of select="format-number(OTHER_LOC_1FAMILY,'#')" />)</xsl:template>
	<xsl:template name="STEPID49">	-  HO-70 Additional Premises Rented - Other Location - 2 Family (<xsl:value-of select="format-number(OTHER_LOC_2FAMILY,'#')" />)</xsl:template>
	<xsl:template name="STEPID50">	-  HO-42 Incidental Office Private School or Studio On Premises</xsl:template>
	<xsl:template name="STEPID51">	-      Business Located in an Other Structure</xsl:template> <!-- SKB-->
	<xsl:template name="STEPID52">	-  HO-42 Incidental Office Private School or Studio Instruction Only</xsl:template>
	<xsl:template name="STEPID53">	-  HO-200 Waterbed Liability</xsl:template>
	<xsl:template name="STEPID54">	-  HO-43 Incidental Office Private School or Studio Off Premises</xsl:template>
	<xsl:template name="STEPID55">	-  HO-71 Business Pursuits</xsl:template>
	<xsl:template name="STEPID56">	-    Class A - (1 persons)</xsl:template>
	<xsl:template name="STEPID57">	-    Class B - (1 persons)</xsl:template>
	<xsl:template name="STEPID58">	-    Class C - (1 persons)</xsl:template>
	<xsl:template name="STEPID59">	-    Class D - (1 persons)</xsl:template>
	<xsl:template name="STEPID60">	-  HO-72 Farm Liability - Incidental Farming Residence Premises</xsl:template>
	<xsl:template name="STEPID61">	-  HO-73 Farm Liability - Owned By Insured Rented to Others(<xsl:value-of select="format-number(OTH_LOC_OPR_OTHERS_HO73,'#')" />)</xsl:template>
	<xsl:template name="STEPID62">	-  HO-73 Farm Liability - Owned/Operated By Insured/Insured Employee(<xsl:value-of select="format-number(OTH_LOC_OPR_EMPL_HO73,'#')" />)</xsl:template>
	<xsl:template name="STEPID63">	-  HO-82 Personal Injury</xsl:template>
	<xsl:template name="STEPID64">	-  Bicycle (HO-900)</xsl:template>
	<xsl:template name="STEPID65">	-  Cameras (Non-Professional)</xsl:template>
	<xsl:template name="STEPID66">	-  Cellular Phones (HO-900)</xsl:template>
	<xsl:template name="STEPID67">	-  Furs</xsl:template>
	<xsl:template name="STEPID68">	-  Golf Equipment</xsl:template>
	<xsl:template name="STEPID69">	-  Guns</xsl:template>
	<xsl:template name="STEPID70">	-  Jewelry</xsl:template>
	<xsl:template name="STEPID71">	-  Musical (Non-Professional)</xsl:template>
	<xsl:template name="STEPID72">	-  Personal Computers (HO-214)</xsl:template>
	<xsl:template name="STEPID73">	-  Silver</xsl:template>
	<xsl:template name="STEPID74">	-  Stamps</xsl:template>
	<xsl:template name="STEPID75">	-  Rare Coins</xsl:template>
	<xsl:template name="STEPID76">	-  Fine Arts - Without Breakage</xsl:template>
	<xsl:template name="STEPID77">	-  Fine Arts - With Breakage</xsl:template>
	<xsl:template name="STEPID78">	-  Property Expense Fee</xsl:template>
	<xsl:template name="STEPID79">	-  HO-64 Renters Deluxe Endorsement </xsl:template>
	<xsl:template name="STEPID80">	-  HO-66 Condominium Deluxe Endorsement</xsl:template>
	<xsl:template name="STEPID81">	-  HO-32 Unit Owners Coverage A Special Coverage</xsl:template>
	<xsl:template name="STEPID82">	-   HO-50 Personal Property Away From Premises (Increase <xsl:value-of select="PERSONALPROPERTYAWAYADDITIONAL" />)</xsl:template>
	<xsl:template name="STEPID83">	-   Building Additions and Alterations (HO-51) (Increase <xsl:value-of select="format-number(PERSONALPROPERTYINCREASEDLIMITADDITIONAL,'#')" /> )  </xsl:template>
	<xsl:template name="STEPID84">	-   Final Premium </xsl:template>
	<!--<xsl:template name = "STEPID72">	
	<xsl:variable name ="GET_TOTAL_DISCOUNT_VALUE">
		<xsl:call-template name="GET_TOTAL_DISCOUNT"></xsl:call-template>
	</xsl:variable>
	<xsl:choose>
		<xsl:when test ="$GET_TOTAL_DISCOUNT_VALUE = 1.00">
		1.00
		</xsl:when>
		<xsl:otherwise>
		0.00
		</xsl:otherwise>
	</xsl:choose>
	<xsl:value-of select ="$GET_TOTAL_DISCOUNT_VALUE"/>
	1.00
</xsl:template>-->
	<!--<xsl:template name ="MULTIPOLICY_DISPLAY">
<xsl:value-of select ="MULTIPOLICY_CREDIT"/>
</xsl:template>-->
	<!-- COMBINED PERSONAL PROPERTY INCREASED LIMIT ON   (COVERAGE - C)-->
	<!--
<xsl:template name = "COMBINEDPERSONALPROPERTY"> 
	
	<xsl:variable name="VAR1">
		<xsl:choose>
			<xsl:when test="PERSONALPROPERTYINCREASEDLIMITINCLUDE != ' '">
					<xsl:value-of select="PERSONALPROPERTYINCREASEDLIMITINCLUDE"/>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:variable>
	
	<xsl:variable name="VAR2">
		<xsl:value-of select="PERSONALPROPERTYINCREASEDLIMITADDITIONAL"/>
	</xsl:variable>

	<xsl:variable name="VAR3">
		<xsl:value-of select="REPLACEMENTCOSTFACTOR"/>    
	</xsl:variable>

 
 	
		<xsl:choose>
		
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6">
					<xsl:value-of select="round($VAR3)"/> 
			</xsl:when>
		
			<xsl:when test="PRODUCTNAME != $POLICYTYPE_HO4 or PRODUCTNAME != $POLICYTYPE_HO6">
					<xsl:value-of select="round($VAR1+$VAR2)"/> 
			</xsl:when>
			<xsl:otherwise>
					<xsl:value-of select="$VAR3"/>
			</xsl:otherwise>
		</xsl:choose>
</xsl:template>
-->
	<!-- COMBINED ADDITIONAL LIVING EXPENSE  (COVERAGE - D) -->
	<xsl:template name="COMBINEDADDITIONALLIVINGEXPENSE">
		<xsl:variable name="VAR1">
			<xsl:value-of select="ADDITIONALLIVINGEXPENSEINCLUDE" />
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:value-of select="ADDITIONALLIVINGEXPENSEADDITIONAL" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:value-of select="REPLACEMENTCOSTFACTOR" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="PRODUCTNAME !=$POLICYTYPE_HO4 or PRODUCTNAME !=$POLICYTYPE_HO6">
				<xsl:value-of select="round($VAR1+$VAR2)" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="round($VAR1)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--									Template for Lables  (END)								  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--									Additional Coverage Template (START) 					  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADDITIONALCOVERAGE">
 	(<xsl:call-template name="BREEDDOG" />)  +   
  	(<xsl:call-template name="PREFERRED_PLUS_COVERAGE" />) +    
 	<!-- previous (<xsl:call-template name="HO_34_REPLACEMENT_COST"/>) +  -->
 	(<xsl:call-template name="SELECT_HO_34_COVERAGE" />) +
	(<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY_COVERAGE" />) +
	(<xsl:call-template name="HO_96_REPLACEMENT_COST_PERSONAL_PROPERTY" />) +
	(<xsl:call-template name="HO_312_BUSINESS_PROPERTY_INCREASED_LIMITS" />) +
	(<xsl:call-template name="HO_48_OTHER_STRUCTURE_INCREASED_LIMITS" />) +
	(<xsl:call-template name="HO_40_OTHER_STRUCTURE_RENTED_TO_OTHERS" />) +
	(<xsl:call-template name="HO_42_OTHER_STRUCTURE_WITH_INCIDENTAL" />) +
	(<xsl:call-template name="HO_53_INCREASED_CREDIT_CARD" />) +
	(<xsl:call-template name="UNSCHEDULED_JEWELRY_ADDITIONAL" />) +
	(<xsl:call-template name="MONEY" />) +
	(<xsl:call-template name="SECURITIES" />) +
	(<xsl:call-template name="SILVERWARE_GOLDWARE" />) +
	(<xsl:call-template name="FIREARMS" />) +	 
	(<xsl:call-template name="ORDINANCE" />) +
	(<xsl:call-template name="HO_455_IDENTITY_FRAUD" />) +
	(<xsl:call-template name="HO_315_EARTHQUAKE" />)+
	(<xsl:call-template name="HO_327_WATER_BACK_UP" />)+
	(<xsl:call-template name="HO_287_MINE_SUBSIDENCE_COVERAGE" />)+
	(<xsl:call-template name="HO_9_COLLAPSE_FROM_SUB_SURFACE_WATER" />)+
	(<xsl:call-template name="HO_490_SPECIFIC_STRUCTURED" />)+
	(<xsl:call-template name="HO_489_SPECIFIC_STRUCTURED" />)+
	(<xsl:call-template name="PRIMARY_RESIDENCE" />)+
	(<xsl:call-template name="RESIDENCE_EMPLOYEES" />)+
	(<xsl:call-template name="ADDITIONAL_PREMISES" />)+
	(<xsl:call-template name="ADDITIONAL_RESIDENCE_PREMISES" />)+
	(<xsl:call-template name="ADDITIONAL_OTHER_LOCATION_PREMISES_FAMILY1" />)+
	(<xsl:call-template name="ADDITIONAL_OTHER_LOCATION_PREMISES_FAMILY2" />)+
	(<xsl:call-template name="HO_42_ON_PREMISES" />)+
	(<xsl:call-template name="HO42_BUSINESS_OTHER_STRUCTURE_WITH_INCIDENTAL" />)+
	(<xsl:call-template name="HO_42_INSTRUCTION_ONLY" />)+
	(<xsl:call-template name="HO_43_OFF_PREMISES" />)+
	(<xsl:call-template name="HO_71_CLASSA" />)+
	(<xsl:call-template name="HO_71_CLASSB" />)+
	(<xsl:call-template name="HO_71_CLASSC" />)+
	(<xsl:call-template name="HO_71_CLASSD" />)+
	(<xsl:call-template name="HO_72_INCIDENTAL_FARMING" />)+
	(<xsl:call-template name="HO_73_OTH_LOC_OPR_EMPL" />)+
	(<xsl:call-template name="HO_73_OTH_LOC_OPR_OTHERS" />)+
	(<xsl:call-template name="HO_82_PERSONAL_INJURY" />)+
	(<xsl:call-template name="HO_900_BICYCLES_PREMIUM" />)+
	(<xsl:call-template name="CAMERAS_PREMIUM" />)+
	(<xsl:call-template name="HO_900_CELLULAR_PHONES_PREMIUM" />)+
	(<xsl:call-template name="FURS_PREMIUM" />)+
	(<xsl:call-template name="GOLF_EQUIPMENT_PREMIUM" />)+
	(<xsl:call-template name="GUNS_PREMIUM" />)+
	(<xsl:call-template name="JEWELRY_PREMIUM" />)+
	(<xsl:call-template name="MUSICAL_NON_PROFESSIONAL_PREMIUM" />)+
	(<xsl:call-template name="PERSONAL_COMPUTERS_PREMIUM" />)+
	(<xsl:call-template name="SILVER_PREMIUM" />)+
	(<xsl:call-template name="STAMPS_PREMIUM" />)+
	(<xsl:call-template name="RARE_COINS_PREMIUM" />)+
	(<xsl:call-template name="FINE_ARTS_WITHOUT_BREAKAGE_PREMIUM" />)+
	(<xsl:call-template name="FINE_ARTS_BREAKAGE_PREMIUM" />)+
	(<xsl:call-template name="PROPFEE" />)+
	(<xsl:call-template name="HO_200_WATERBED_LIABILITY" />)+
	(<xsl:call-template name="HO_33_CONDO_UNIT" />)+
	(<xsl:call-template name="HO50_PERSONAL_PROPERTY_AWAY_PREMISES" />)+
	(<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />)
	
	
 
				 
	
</xsl:template>
	<!-- ============================================================================================ -->
	<!--									Additional Coverage Template (END) 	    				  -->
	<!-- ============================================================================================ -->
	<!-- ###################################  START - MICHIGAN ###################################### -->
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR MICHIGAN(REGULAR) HO-2			
		1. Base (Find Base in Territory By Policy Form and Protection Class.) X
		2. Coverage A Factor (Tables based on Protection Class and Construction Frame or Masonry.) X
		3. Replacement Cost Factor X
		4. Insurance Score Discount Factor X
		5. Term Factor: 1.00 Annual or .50 Semi-Annual X
		6. Charge for Increased Coverage C or Coverage D (if any)
		7. Deductible Factor X
		8. Age of Home Factor X
		9. Protective Device Factor X
		10. HO-14 Dwelling Under Construction Credit (if applies then no: Age of Home, or Protective Device Credit) X
		11. Experience (Maturity or Age of Insured) Factor X
		12. Multiple Policy (Auto & Home) Factor X
		13. Valued Customer-Renewal Credit Factor X
		14. Surcharges
		15. Apply Section II Credits if any ($15 if Secondary and we have the Primary; or $2 for Secondary and No
		Primary).
		16. Add -Any Other Endorsement Option Charges.
		17. Add-Section II Liability Charges (if any).
		18. Add-Michigan Property Expense Recoupment Charge. - THIS WILL BE ADDED IN ADDITIONAL COVERAGE SECTION
						  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_REGULAR_MICHIGAN_HO2_HO3">
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="DEWLLING-MAIN" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE">
			<xsl:call-template name="C-VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_REPLACEMENTCOST">
			<xsl:call-template name="REPLACEMENT_COST_FACTOR" />
		</xsl:variable>
		<!--<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT"/> 
	   </xsl:variable> -->
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_C">
			<xsl:call-template name="COVERAGE_C_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_D">
			<xsl:call-template name="COVERAGE_D_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_DFACTOR">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
		<xsl:variable name="VAR_AGEOFHOMEFACTOR">
			<xsl:variable name="AGEOFHOMEFACTOR">
				<xsl:call-template name="AGEOFHOME_FACTOR" />
			</xsl:variable>
			<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
			<xsl:variable name="AGEOFHOME_CREDIT_PERCENT" select="(1.00-$AGEOFHOMEFACTOR) * 100.00" />
			<xsl:variable name="VAR_MAXCREDIT_PERCENT" select="(1.00-$VAR_MAXCREDIT_FACTOR) * 100.00" />
			<xsl:choose>
				<xsl:when test="$AGEOFHOME_CREDIT_PERCENT &lt; $VAR_MAXCREDIT_PERCENT">
					<xsl:value-of select="$AGEOFHOMEFACTOR" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$VAR_MAXCREDIT_FACTOR" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_PROTECTIVE_DEVICE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_DWELLING_UNDER_CONSTR">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_DISCOUNT">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_VALUEDCUSTOMER">
			<xsl:call-template name="VALUEDCUSTOMER" />
		</xsl:variable>
		<!-- TILL HERE -->
		<!-- correct till here -->
		<xsl:variable name="VAR_PRIOR_LOSS">
			<xsl:call-template name="PRIOR_LOSS" />
		</xsl:variable>
		<xsl:variable name="VAR_WSTOVE">
			<xsl:call-template name="WSTOVE" />
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
			<xsl:call-template name="SEASONALSECONDARYCREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:call-template name="LIABLITY_CHARGES" />
		<!-- return value of adjusted base -->
		<!--
		<xsl:variable name="VAR_1" select="round(round(round(round($VAR_BASE * $VAR_COVERAGE)* $VAR_REPLACEMENTCOST)* $VAR_INSURANCESCORE)* $VAR_TERMFACTOR)"/>
		<xsl:variable name="VAR_2" select="$VAR_1 + $VAR_INCREASED_COV_C + $VAR_INCREASED_COV_D"/>
		<xsl:variable name="VAR_3" select="round(round(round(round(round(round(round(round(round($VAR_2 * $VAR_DFACTOR)* $VAR_AGEOFHOMEFACTOR)* $VAR_PROTECTIVE_DEVICE)* $VAR_DWELLING_UNDER_CONSTR)* $VAR_EXPERIENCE_DISCOUNT)* $VAR_MULTIPOLICY_DISCOUNT)* $VAR_VALUEDCUSTOMER)* $VAR_PRIOR_LOSS)* $VAR_WSTOVE)"/>
		<xsl:value-of select="($VAR_3 -$VAR_SEASONALSECONDARYCREDIT) + $VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES" />
	   -->
	</xsl:template>
	<!--  **********************************************************************  -->
	<!-- Wood StoveLAbel Display -->
	<xsl:template name="WOODSTOVE_LABEL_DISPLAY">
((((((((((<xsl:call-template name="DEWLLING-MAIN"></xsl:call-template> )*
			(<xsl:call-template name="C-VALUE"></xsl:call-template>)*
			(<xsl:call-template name="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name="TERM_FACTOR"></xsl:call-template>)
			)
			+
			(
				(<xsl:call-template name="COVERAGE_C_PREMIUM" /> )
				+ (<xsl:call-template name="COVERAGE_D_PREMIUM" />)
			)
		    
	   )
	   *
	   (<xsl:call-template name="DFACTOR" />))*
	   (<xsl:call-template name="AGEOFHOME_FACTOR" />))*
	   (<xsl:call-template name="PROTECTIVEDEVICE" />))*
	   (<xsl:call-template name="EXPERIENCEFACTOR" />))*
	   (<xsl:call-template name="MULTIPOLICY_FACTOR" />))*
	   (<xsl:call-template name="VALUEDCUSTOMER" />))*
	   (<xsl:call-template name="PRIOR_LOSS" />))*
	   (<xsl:call-template name="WSTOVE" />)
	   
	    
</xsl:template>
	<!-- ============================================================================================ -->
	<!--					ADJUSTEDBASE FOR MICHIGAN(REPAIR COST) HO-2	and HO-3					  -->
	<!--    1. Base (Find Base in Territory By Policy Form and Protection Class.) X
		2. Coverage A Factor (Tables based on Protection Class and Construction Frame or Masonry.) X
		3. Repair Cost Factor 1.23 X
		4. Insurance Score Discount Factor X
		5. Term Factor: 1.00 Annual or .50 Semi-Annual X
		6. Charge for Increased Coverage C or Coverage D (if any)
		7. Deductible Factor X
		8. Age of Home Factor X
		9. Protective Device Factor X
		10. HO-14 Dwelling Under Construction Credit (if applies then no: Age of Home, or Protective Device Credit) X
		11. Experience (Maturity or Age of Insured) Factor X
		12. Multiple Policy (Auto & Home) Factor X
		13. Valued Customer-Renewal Credit Factor X
		14. Surcharges
		15. Apply Section II Credits if any ($15 if Secondary and we have the Primary; or $2 for Secondary and No
		Primary).
		16. Add -Any Other Endorsement Option Charges.
		17. Add-Section II Liability Charges (if any).
		18. Add-Michigan Property Expense Recoupment Charge. - THIS WILL BE ADDED IN ADDITIONAL COVERAGE SECTION
		
-->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_REPAIRCOST_MICHIGAN_HO2_HO3">
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="DEWLLING-MAIN" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE">
			<xsl:call-template name="C-VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_REPAIRCOST">
			<xsl:call-template name="REPAIR_COST_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_C">
			<xsl:call-template name="COVERAGE_C_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_D">
			<xsl:call-template name="COVERAGE_D_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_DFACTOR">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
		<xsl:variable name="VAR_AGEOFHOMEFACTOR">
			<xsl:variable name="AGEOFHOMEFACTOR">
				<xsl:call-template name="AGEOFHOME_FACTOR" />
			</xsl:variable>
			<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
			<!--
			<xsl:variable name="AGEOFHOME_CREDIT_PERCENT" select="(1.00-$AGEOFHOMEFACTOR) * 100.00"/>
			<xsl:variable name="VAR_MAXCREDIT_PERCENT" select="(1.00-$VAR_MAXCREDIT_FACTOR) * 100.00"/>
	-->
			<xsl:choose>
				<xsl:when test="$AGEOFHOME_CREDIT_PERCENT &lt; $VAR_MAXCREDIT_PERCENT">
					<xsl:value-of select="$AGEOFHOMEFACTOR" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$VAR_MAXCREDIT_FACTOR" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_PROTECTIVE_DEVICE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_DWELLING_UNDER_CONSTR">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_DISCOUNT">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_VALUEDCUSTOMER">
			<xsl:call-template name="VALUEDCUSTOMER" />
		</xsl:variable>
		<!-- TILL HERE -->
		<xsl:variable name="VAR_PRIOR_LOSS">
			<xsl:call-template name="PRIOR_LOSS" />
		</xsl:variable>
		<xsl:variable name="VAR_WSTOVE">
			<xsl:call-template name="WSTOVE" />
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
			<xsl:call-template name="SEASONALSECONDARYCREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<!-- return value of adjusted base -->
		<!--
		<xsl:variable name="VAR_1" select="round(round(round(round($VAR_BASE * $VAR_COVERAGE)* $VAR_REPAIRCOST)* $VAR_INSURANCESCORE)* $VAR_TERMFACTOR)"/>
		<xsl:variable name="VAR_2" select="$VAR_1 + $VAR_INCREASED_COV_C + $VAR_INCREASED_COV_D"/>
		<xsl:variable name="VAR_3" select="round(round(round(round(round(round(round(round(round($VAR_2 * $VAR_DFACTOR)* $VAR_AGEOFHOMEFACTOR)* $VAR_PROTECTIVE_DEVICE)* $VAR_DWELLING_UNDER_CONSTR)* $VAR_EXPERIENCE_DISCOUNT)* $VAR_MULTIPOLICY_DISCOUNT)* $VAR_VALUEDCUSTOMER)* $VAR_PRIOR_LOSS)* $VAR_WSTOVE)"/>
		<xsl:value-of select="($VAR_3 -$VAR_SEASONALSECONDARYCREDIT) + $VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES" />
		-->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR MICHIGAN(PREMIER) HO-3							  -->
	<!-- 1. Base (Find Base in Territory By Policy Form and Protection Class.) X
	2. Coverage A Factor X (Minimum is $175,000) X
	3. 1.00 if insured at 100% to Replacement Cost. (Premier must be at least 100% or use Regular program.) X
	4. Insurance Score Discount Factor X
	5. Term Factor: 1.00 Annual or .50 Semi-Annual X
	6. Charge for Increased Coverage C or Coverage D (if any)
	7. Deductible Factor X
	8. Multiple Policy (Auto & Home) Factor X
	9. Age of Home Factor X
	10. Protective Device Factor X
	11. HO-14 Dwelling Under Construction Credit (if applies then no: Age of Home, or Protective Device Credit) X
	12. Experience (Maturity or Age of Insured) Factor X
	13. Valued Customer-Renewal Credit Factor X
	14. Surcharges
	15. Apply Section II Credits if any ($15 if Secondary and we have the Primary; or $2 for Secondary and No
	Primary).
	16. Add-Any Other Endorsement Option Charges.
	17. Add-Section II Liability Charges (if any).
	18. Add-Michigan Property Expense Recoupment Charge.  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_PREMIER_MICHIGAN_HO3">
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="DEWLLING-MAIN" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE">
			<xsl:call-template name="C-VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_REPLACEMENTCOST">
			<xsl:call-template name="REPLACEMENT_COST_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_C">
			<xsl:call-template name="COVERAGE_C_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_D">
			<xsl:call-template name="COVERAGE_D_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_DFACTOR">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
		<xsl:variable name="VAR_AGEOFHOMEFACTOR">
			<xsl:variable name="AGEOFHOMEFACTOR">
				<xsl:call-template name="AGEOFHOME_FACTOR" />
			</xsl:variable>
			<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
			<xsl:variable name="AGEOFHOME_CREDIT_PERCENT" select="(1.00-$AGEOFHOMEFACTOR) * 100.00" />
			<xsl:variable name="VAR_MAXCREDIT_PERCENT" select="(1.00-$VAR_MAXCREDIT_FACTOR) * 100.00" />
			<xsl:choose>
				<xsl:when test="$AGEOFHOME_CREDIT_PERCENT &lt; $VAR_MAXCREDIT_PERCENT">
					<xsl:value-of select="$AGEOFHOMEFACTOR" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$VAR_MAXCREDIT_FACTOR" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_PROTECTIVE_DEVICE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_DWELLING_UNDER_CONSTR">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_DISCOUNT">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_VALUEDCUSTOMER">
			<xsl:call-template name="VALUEDCUSTOMER" />
		</xsl:variable>
		<!-- TILL HERE -->
		<xsl:variable name="VAR_PRIOR_LOSS">
			<xsl:call-template name="PRIOR_LOSS" />
		</xsl:variable>
		<xsl:variable name="VAR_WSTOVE">
			<xsl:call-template name="WSTOVE" />
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
			<xsl:call-template name="SEASONALSECONDARYCREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<!-- return value of adjusted base -->
		<!--
		<xsl:variable name="VAR_1" select="round(round(round(round($VAR_BASE * $VAR_COVERAGE)* $VAR_REPLACEMENTCOST)* $VAR_INSURANCESCORE)* $VAR_TERMFACTOR)"/>
		<xsl:variable name="VAR_2" select="$VAR_1 + $VAR_INCREASED_COV_C + $VAR_INCREASED_COV_D"/>
		<xsl:variable name="VAR_3" select="round(round(round(round(round(round(round(round(round($VAR_2 * $VAR_DFACTOR)* $VAR_AGEOFHOMEFACTOR)* $VAR_PROTECTIVE_DEVICE)* $VAR_DWELLING_UNDER_CONSTR)* $VAR_EXPERIENCE_DISCOUNT)* $VAR_MULTIPOLICY_DISCOUNT)* $VAR_VALUEDCUSTOMER)* $VAR_PRIOR_LOSS)* $VAR_WSTOVE)"/>
		<xsl:value-of select="($VAR_3 -$VAR_SEASONALSECONDARYCREDIT) + $VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES" />
	-->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR MICHIGAN(REGULAR) HO-4 OR HO-6					  -->
	<!-- 1. Base (Find Base in Territory By Policy Form 4 or 6, CO-I or CO-II, and Protection Class.) X
	2. Coverage C Factor (Tables based on whether is HO-4 with minimum $10,000 or HO-6 minimum $15,000.) X
	
	4. Insurance Score Discount Factor X
	5. Term Factor: 1.00 Annual or .50 Semi-Annual X
	6. Deductible Factor X
	7. Protective Device Factor X
	8. Non-Smoker or Smoker Factor X
	9. Experience (Maturity or Age of Insured) Factor X
	10. Multiple Policy (Auto & Home) Factor (HO-4 must be minimum $15,000 Coverage C to get credit. HO-6
	no minimum.) X
	11. Valued Customer-Renewal Credit X
	
	13. Surcharges
	3. DELUXE Endorsement (if NO then X 1.00, if YES minimum Coverage C must be $25,000 and then take rate
	X 1.15 Min. $25.)
	12. Coverage A is 10% of Coverage C ($2,000 minimum), if Coverage A increased and there is no HO-66
	endorsement then rate is $2 per $1,000 increase. If an HO-66 Deluxe endorsement then charge is $2.20
	per $1,000 increased above automatic amount.
	14. Apply Section II Credits if any ($9 if Secondary and we have the Primary; or $2 for Secondary and No
	Primary).
	15. Add - Any Other Endorsement Option Charges.
	16. Add-Section II Liability Charges (if any).
	17. Add-Michigan Property Expense Recoupment Fee. -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_REGULAR_MICHIGAN_HO4_HO6">
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="DEWLLING-MAIN" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE">
			<xsl:call-template name="C-VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_DFACTOR">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
		<xsl:variable name="VAR_AGEOFHOMEFACTOR">
			<xsl:variable name="AGEOFHOMEFACTOR">
				<xsl:call-template name="AGEOFHOME_FACTOR" />
			</xsl:variable>
			<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
			<xsl:variable name="AGEOFHOME_CREDIT_PERCENT" select="(1.00-$AGEOFHOMEFACTOR) * 100.00" />
			<xsl:variable name="VAR_MAXCREDIT_PERCENT" select="(1.00-$VAR_MAXCREDIT_FACTOR) * 100.00" />
			<xsl:choose>
				<xsl:when test="$AGEOFHOME_CREDIT_PERCENT &lt; $VAR_MAXCREDIT_PERCENT">
					<xsl:value-of select="$AGEOFHOMEFACTOR" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$VAR_MAXCREDIT_FACTOR" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_PROTECTIVE_DEVICE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_DWELLING_UNDER_CONSTR">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_DISCOUNT">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_VALUEDCUSTOMER">
			<xsl:call-template name="VALUEDCUSTOMER" />
		</xsl:variable>
		<!-- TILL HERE   -->
		<xsl:variable name="VAR_PRIOR_LOSS">
			<xsl:call-template name="PRIOR_LOSS" />
		</xsl:variable>
		<xsl:variable name="VAR_WSTOVE">
			<xsl:call-template name="WSTOVE" />
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
			<xsl:call-template name="SEASONALSECONDARYCREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<!-- return value of adjusted base-->
		<!--
		<xsl:variable name="VAR_1" select="round(round(round($VAR_BASE * $VAR_COVERAGE)* $VAR_TERMFACTOR)* $VAR_DFACTOR)"/>
		<xsl:variable name="VAR_2" select="round(round(round(round(round(round(round(round(round($VAR_1 *   $VAR_INSURANCESCORE)*$VAR_AGEOFHOMEFACTOR)* $VAR_PROTECTIVE_DEVICE)* $VAR_DWELLING_UNDER_CONSTR)* $VAR_EXPERIENCE_DISCOUNT)* $VAR_MULTIPOLICY_DISCOUNT)* $VAR_VALUEDCUSTOMER)* $VAR_PRIOR_LOSS)* $VAR_WSTOVE)"/>
		<xsl:value-of select="(($VAR_2  - $VAR_SEASONALSECONDARYCREDIT) + $VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES)" />
 	 -->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE FOR MICHIGAN(REGULER) HO-5					  -->
	<!-- 1. Base (Find Base in Territory By Policy Form and Protection Class.) X
     2. Coverage A Factor (Note Minimum Coverage A is $125,000.) X
	3. 1.00 if insured to Replacement Cost (HO-5 Must Be at 100% Coverage A to Replacement Cost to be
	eligible.) X
	4. Term Factor: 1.00 Annual or .50 Semi-Annual X
	5. Deductible Factor X
		6. Add HO-34 Replacement Cost Personal Property Endorsement = 10% Minimum $35.	| TO BE ADDED HERE
		7. Add HO-11 Expanded Replacement Coverage for Building = $15 Flat Charge		|
		8. Add HO-23 Premier V.I.P. End = $20										   _|
	9. Insurance Score Discount Factor X
	10. Age of Home Factor X
	11. Protective Device Factor X
	12. HO-14 Dwelling Under Construction Credit (if applies then no: Age of Home, or Protective Device Credit) X
	13. Experience (Maturity or Age of Insured) Factor X
	14. Multiple Policy (Auto & Home) Factor X
	15. Valued Customer-Renewal Credit Factor X
	16. Surcharges
	17. Add Charge for Increased Coverage C or Coverage D (if any).						
		
	18. Apply Section II Credits if any ($15 if Secondary and we have the Primary; or $2 for Secondary and No
	Primary). -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_REGULAR_MICHIGAN_HO5">
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="DEWLLING-MAIN" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE">
			<xsl:call-template name="C-VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_REPLACEMENTCOST">
			<xsl:call-template name="REPLACEMENT_COST_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_C">
			<xsl:call-template name="COVERAGE_C_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_D">
			<xsl:call-template name="COVERAGE_D_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_DFACTOR">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_HO_34_REPLACEMENT_COST">
			<xsl:call-template name="HO_34_REPLACEMENT_COST" />
		</xsl:variable>
		<xsl:variable name="VAR_HO_20_PREFERRED_PLUS_OR_HO_21_VIP">
			<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
		</xsl:variable>
		<xsl:variable name="VAR_HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY">
			<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY" />
		</xsl:variable>
		<!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
		<xsl:variable name="VAR_AGEOFHOMEFACTOR">
			<xsl:variable name="AGEOFHOMEFACTOR">
				<xsl:call-template name="AGEOFHOME_FACTOR" />
			</xsl:variable>
			<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
			<xsl:variable name="AGEOFHOME_CREDIT_PERCENT" select="(1.00-$AGEOFHOMEFACTOR) * 100.00" />
			<xsl:variable name="VAR_MAXCREDIT_PERCENT" select="(1.00-$VAR_MAXCREDIT_FACTOR) * 100.00" />
			<xsl:choose>
				<xsl:when test="$AGEOFHOME_CREDIT_PERCENT &lt; $VAR_MAXCREDIT_PERCENT">
					<xsl:value-of select="$AGEOFHOMEFACTOR" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$VAR_MAXCREDIT_FACTOR" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_PROTECTIVE_DEVICE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_DWELLING_UNDER_CONSTR">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_DISCOUNT">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_VALUEDCUSTOMER">
			<xsl:call-template name="VALUEDCUSTOMER" />
		</xsl:variable>
		<!-- TILL HERE -->
		<xsl:variable name="VAR_PRIOR_LOSS">
			<xsl:call-template name="PRIOR_LOSS" />
		</xsl:variable>
		<xsl:variable name="VAR_WSTOVE">
			<xsl:call-template name="WSTOVE" />
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
			<xsl:call-template name="SEASONALSECONDARYCREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<!-- return value of adjusted base -->
		<!--
		<xsl:variable name="VAR_1" select="round(round(round(round($VAR_BASE * $VAR_COVERAGE)* $VAR_REPLACEMENTCOST)* $VAR_TERMFACTOR)* $VAR_DFACTOR)"/>
		<xsl:variable name="VAR_2" select="round(round(round($VAR_1 + $VAR_HO_34_REPLACEMENT_COST) + $VAR_HO_20_PREFERRED_PLUS_OR_HO_21_VIP) +  $VAR_HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY)"/>
		<xsl:variable name="VAR_3" select="round(round(round(round(round(round(round(round(round($VAR_2 *   $VAR_INSURANCESCORE)*$VAR_AGEOFHOMEFACTOR)* $VAR_PROTECTIVE_DEVICE)* $VAR_DWELLING_UNDER_CONSTR)* $VAR_EXPERIENCE_DISCOUNT)* $VAR_MULTIPOLICY_DISCOUNT)* $VAR_VALUEDCUSTOMER)* $VAR_PRIOR_LOSS)* $VAR_WSTOVE)"/>
		<xsl:value-of select="(($VAR_3 + $VAR_INCREASED_COV_C + $VAR_INCREASED_COV_D) - $VAR_SEASONALSECONDARYCREDIT) + $VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES" />
	   -->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--							ADJUSTEDBASE PREMIER FOR MICHIGAN(PREMIER) HO-5					  -->
	<!--	
		1. Base (Find Base in Territory By Policy Form and Protection Class.) X
		2. Coverage A Factor (Note Minimum Coverage A is $175,000.) X
		3. 1.00 if insured to Replacement Cost (HO-5 Must Be at 100% Coverage A to Replacement Cost to be eligible.)
		X
		4. Term Factor: 1.00 Annual or .50 Semi-Annual X
		5. Deductible Factor X
		6. Add HO-34 Replacement Cost Personal Property Endorsement = 10% Minimum $35.
			7. Add HO-11 Expanded Replacement Coverage for Building = $15 Flat Charge
			8. Add HO-25 Premier V.I.P. End = $30
		9. Insurance Score Discount Factor X
		10. Multiply times Age of Home Factor X
		11. Protective Device Factor X
		12. HO-14 Dwelling Under Construction Credit (if applies then no: Age of Home, or Protective Device Credit) X
		13. Experience (Maturity or Age of Insured) Factor X
		14. Multiple Policy (Auto & Home) Factor X
		15. Valued Customer-Renewal Credit Factor X
		16. Surcharges
		17. Apply Section II Credits if any ($15 if Secondary and we have the Primary; or $2 for Secondary and No
		Primary).
		18. Add Charge for Increased Coverage C or Coverage D (if any).
		19. Add Any Other Endorsement Option Charges.
			
		20. Add Section II Liability Charges (if any).
		21. Add-Michigan Property Expense Recoupment Charges.

-->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_PREMIER_MICHIGAN_HO5">
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="DEWLLING-MAIN" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE">
			<xsl:call-template name="C-VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_REPLACEMENTCOST">
			<xsl:call-template name="REPLACEMENT_COST_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_C">
			<xsl:call-template name="COVERAGE_C_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_D">
			<xsl:call-template name="COVERAGE_D_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_DFACTOR">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_HO_34_REPLACEMENT_COST">
			<xsl:call-template name="HO_34_REPLACEMENT_COST" />
		</xsl:variable>
		<xsl:variable name="VAR_HO_20_PREFERRED_PLUS_OR_HO_21_VIP">
			<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
		</xsl:variable>
		<xsl:variable name="VAR_HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY">
			<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY" />
		</xsl:variable>
		<!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
		<xsl:variable name="VAR_AGEOFHOMEFACTOR">
			<xsl:variable name="AGEOFHOMEFACTOR">
				<xsl:call-template name="AGEOFHOME_FACTOR" />
			</xsl:variable>
			<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
			<xsl:variable name="AGEOFHOME_CREDIT_PERCENT" select="(1.00-$AGEOFHOMEFACTOR) * 100.00" />
			<xsl:variable name="VAR_MAXCREDIT_PERCENT" select="(1.00-$VAR_MAXCREDIT_FACTOR) * 100.00" />
			<xsl:choose>
				<xsl:when test="$AGEOFHOME_CREDIT_PERCENT &lt; $VAR_MAXCREDIT_PERCENT">
					<xsl:value-of select="$AGEOFHOMEFACTOR" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$VAR_MAXCREDIT_FACTOR" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_PROTECTIVE_DEVICE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_DWELLING_UNDER_CONSTR">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_DISCOUNT">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_VALUEDCUSTOMER">
			<xsl:call-template name="VALUEDCUSTOMER" />
		</xsl:variable>
		<!-- TILL HERE -->
		<xsl:variable name="VAR_PRIOR_LOSS">
			<xsl:call-template name="PRIOR_LOSS" />
		</xsl:variable>
		<xsl:variable name="VAR_WSTOVE">
			<xsl:call-template name="WSTOVE" />
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
			<xsl:call-template name="SEASONALSECONDARYCREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<!--  
	   <xsl:variable name="VAR_1" select="round(round(round($VAR_BASE * $VAR_COVERAGE)* $VAR_REPLACEMENTCOST)* $VAR_TERMFACTOR)"/>
		<xsl:variable name="VAR_2" select="round(round(round($VAR_1 + $VAR_HO_34_REPLACEMENT_COST) + $VAR_HO_20_PREFERRED_PLUS_OR_HO_21_VIP) +  $VAR_HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY)"/>
 		<xsl:variable name="VAR_3" select="round(round(round(round(round(round(round(round(round(round($VAR_2  * $VAR_INSURANCESCORE)*$VAR_DFACTOR)* $VAR_AGEOFHOMEFACTOR)* $VAR_PROTECTIVE_DEVICE)* $VAR_DWELLING_UNDER_CONSTR)* $VAR_EXPERIENCE_DISCOUNT)* $VAR_MULTIPOLICY_DISCOUNT)* $VAR_VALUEDCUSTOMER)* $VAR_PRIOR_LOSS)* $VAR_WSTOVE)"/>
		<xsl:value-of select="($VAR_3 -$VAR_SEASONALSECONDARYCREDIT) + $VAR_INCREASED_COV_C + $VAR_INCREASED_COV_D + $VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES" />
	 -->
	</xsl:template>
	<!-- ###################################  END - MICHIGAN ######################################### -->
	<!-- ###################################  START - INDIANA  ####################################### -->
	<!-- ============================================================================================ -->
	<!--								ADJUSTEDBASE FOR INDIANA (REGULAR) HO-2						  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_REGULAR_INDIANA_HO2_HO3">
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="DEWLLING_MAIN_INDIANA" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE">
			<xsl:call-template name="C-VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_REPLACEMENTCOST">
			<xsl:call-template name="REPLACEMENT_COST_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_C">
			<xsl:call-template name="COVERAGE_C_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_D">
			<xsl:call-template name="COVERAGE_D_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_DFACTOR">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 	  -->
		<xsl:variable name="VAR_AGEOFHOMEFACTOR">
			<xsl:call-template name="AGEOFHOME_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_PROTECTIVE_DEVICE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_DWELLING_UNDER_CONSTR">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_SMOKER">
			<xsl:call-template name="SMOKER" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_DISCOUNT">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_VALUEDCUSTOMER">
			<xsl:call-template name="VALUEDCUSTOMER" />
		</xsl:variable>
		<!-- TILL HERE  -->
		<xsl:variable name="VAR_PRIOR_LOSS">
			<xsl:call-template name="PRIOR_LOSS" />
		</xsl:variable>
		<xsl:variable name="VAR_WSTOVE">
			<xsl:call-template name="WSTOVE" />
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
			<xsl:call-template name="SEASONALSECONDARYCREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<!-- return value of adjusted base-->
		<!--
		<xsl:variable name="VAR_1" select="round(round(round(round(round(round($VAR_BASE * $VAR_COVERAGE)* $VAR_REPLACEMENTCOST)* $VAR_INSURANCESCORE)* $VAR_TERMFACTOR)* $VAR_DFACTOR)* $VAR_AGEOFHOMEFACTOR)"/>
		<xsl:variable name="VAR_2" select="$VAR_1 + $VAR_INCREASED_COV_C + $VAR_INCREASED_COV_D"/>
		<xsl:variable name="VAR_3" select="round(round(round(round(round(round(round(round($VAR_2 * $VAR_PROTECTIVE_DEVICE)* $VAR_DWELLING_UNDER_CONSTR) *$VAR_SMOKER) * $VAR_EXPERIENCE_DISCOUNT)* $VAR_MULTIPOLICY_DISCOUNT)* $VAR_PRIOR_LOSS)* $VAR_WSTOVE)* $VAR_VALUEDCUSTOMER)"/>
		<xsl:value-of select="($VAR_3 -$VAR_SEASONALSECONDARYCREDIT) + $VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES" />
	-->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--								ADJUSTEDBASE FOR INDIANA (REGULAR) HO-5						 
	1. Base (Find Base in Territory By Policy Form and Protection Class.) X
	2. Coverage A Factor (Note: Minimum Coverage A is $125,000.) X
	3. 1.00 if insured to Replacement Cost (HO-5 Must Be at 100% Coverage A to Replacement Cost to be eligible.) X
	4. Term Factor: 1.00 Annual or 0.50 Semi-Annual X
	5. Deductible Factor X
	6. Add HO-34 Replacement Cost Personal Property Endorsement = 10% Minimum $35; Maximum $75
	7. Add HO-11 Expanded Replacement Coverage for Building = $15 Flat Charge
	8. Add HO-24 Premier V.I.P. End = $40
	9. Insurance Score Discount Factor X
	10. Age of Home Factor X
	11. Protective Device Factor X
	12. Non-Smoker or smoker Factor X
	13. HO-14 Dwelling Under Construction Credit (if applies then no: Age of Home, or Protective Device Credit) X
	14. Experience (Maturity or Age of Insured) Factor X
	15. Multiple Policy (Auto & Home) Factor X
	16. Valued Customer-Renewal Credit Factor X
	17. Surcharges -(a) Prior Loss New Business: (b) Woodstove
	18. Add Charge for Increased Coverage C or Coverage D (if any).
	19. Add Any Other Endorsement Option Charges.
	20. Apply Section II Credits if any ($9 if Secondary and we have the Primary; or $2 for Secondary and No
	Primary).
	21. Add Section II Liability Charges (if any).  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_REGULAR_INDIANA_HO5">
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="DEWLLING-MAIN" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE">
			<xsl:call-template name="C-VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_REPLACEMENTCOST">
			<xsl:call-template name="REPLACEMENT_COST_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_C">
			<xsl:call-template name="COVERAGE_C_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_D">
			<xsl:call-template name="COVERAGE_D_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_DFACTOR">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_HO_34_REPLACEMENT_COST">
			<xsl:call-template name="HO_34_REPLACEMENT_COST" />
		</xsl:variable>
		<xsl:variable name="VAR_HO_20_PREFERRED_PLUS_OR_HO_21_VIP">
			<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
		</xsl:variable>
		<xsl:variable name="VAR_HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY">
			<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY" />
		</xsl:variable>
		<!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 -->
		<xsl:variable name="VAR_AGEOFHOMEFACTOR">
			<xsl:variable name="AGEOFHOMEFACTOR">
				<xsl:call-template name="AGEOFHOME_FACTOR" />
			</xsl:variable>
			<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
			<xsl:variable name="AGEOFHOME_CREDIT_PERCENT" select="(1.00-$AGEOFHOMEFACTOR) * 100.00" />
			<xsl:variable name="VAR_MAXCREDIT_PERCENT" select="(1.00-$VAR_MAXCREDIT_FACTOR) * 100.00" />
			<xsl:choose>
				<xsl:when test="$AGEOFHOME_CREDIT_PERCENT &lt; $VAR_MAXCREDIT_PERCENT">
					<xsl:value-of select="$AGEOFHOMEFACTOR" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$VAR_MAXCREDIT_FACTOR" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_PROTECTIVE_DEVICE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_DWELLING_UNDER_CONSTR">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_DISCOUNT">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_VALUEDCUSTOMER">
			<xsl:call-template name="VALUEDCUSTOMER" />
		</xsl:variable>
		<!-- TILL HERE -->
		<xsl:variable name="VAR_PRIOR_LOSS">
			<xsl:call-template name="PRIOR_LOSS" />
		</xsl:variable>
		<xsl:variable name="VAR_WSTOVE">
			<xsl:call-template name="WSTOVE" />
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
			<xsl:call-template name="SEASONALSECONDARYCREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<!-- return value of adjusted base -->
		<!--
		<xsl:variable name="VAR_1" select="round(round(round(round($VAR_BASE * $VAR_COVERAGE)* $VAR_REPLACEMENTCOST)* $VAR_TERMFACTOR)* $VAR_DFACTOR)"/>
		<xsl:variable name="VAR_2" select="round(round(round($VAR_1 + $VAR_HO_34_REPLACEMENT_COST) + $VAR_HO_20_PREFERRED_PLUS_OR_HO_21_VIP) +  $VAR_HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY)"/>
		<xsl:variable name="VAR_3" select="round(round(round(round(round(round(round(round(round($VAR_2 *   $VAR_INSURANCESCORE)*$VAR_AGEOFHOMEFACTOR)* $VAR_PROTECTIVE_DEVICE)* $VAR_DWELLING_UNDER_CONSTR)* $VAR_EXPERIENCE_DISCOUNT)* $VAR_MULTIPOLICY_DISCOUNT)* $VAR_VALUEDCUSTOMER)* $VAR_PRIOR_LOSS)* $VAR_WSTOVE)"/>
		<xsl:value-of select="(($VAR_3 + $VAR_INCREASED_COV_C + $VAR_INCREASED_COV_D) - $VAR_SEASONALSECONDARYCREDIT) + $VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES" />
	   -->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--								ADJUSTEDBASE FOR INDIANA (REPAIR COST) HO-2 and HO-3		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_REPAIRCOST_INDIANA_HO2_HO3">
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="DEWLLING_MAIN_INDIANA" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE">
			<xsl:call-template name="C-VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_REPAIRCOST">
			<xsl:call-template name="REPAIR_COST_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_C">
			<xsl:call-template name="COVERAGE_C_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_INCREASED_COV_D">
			<xsl:call-template name="COVERAGE_D_PREMIUM" />
		</xsl:variable>
		<xsl:variable name="VAR_DFACTOR">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<!--PLEASE DO NOT CHANGE THE SEQUENCE OF THE NEXT 5 	  -->
		<xsl:variable name="VAR_AGEOFHOMEFACTOR">
			<xsl:call-template name="AGEOFHOME_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_PROTECTIVE_DEVICE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_DWELLING_UNDER_CONSTR">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_SMOKER">
			<xsl:call-template name="SMOKER" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_DISCOUNT">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_VALUEDCUSTOMER">
			<xsl:call-template name="VALUEDCUSTOMER" />
		</xsl:variable>
		<!-- TILL HERE  -->
		<xsl:variable name="VAR_PRIOR_LOSS">
			<xsl:call-template name="PRIOR_LOSS" />
		</xsl:variable>
		<xsl:variable name="VAR_WSTOVE">
			<xsl:call-template name="WSTOVE" />
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
			<xsl:call-template name="SEASONALSECONDARYCREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<!-- return value of adjusted base-->
		<!--
		<xsl:variable name="VAR_1" select="round(round(round(round(round(round($VAR_BASE * $VAR_COVERAGE)* $VAR_REPAIRCOST)* $VAR_INSURANCESCORE)* $VAR_TERMFACTOR)* $VAR_DFACTOR)* $VAR_AGEOFHOMEFACTOR)"/>
		<xsl:variable name="VAR_2" select="$VAR_1 + $VAR_INCREASED_COV_C + $VAR_INCREASED_COV_D"/>
		<xsl:variable name="VAR_3" select="round(round(round(round(round(round(round(round($VAR_2 * $VAR_PROTECTIVE_DEVICE)* $VAR_DWELLING_UNDER_CONSTR) *$VAR_SMOKER) * $VAR_EXPERIENCE_DISCOUNT)* $VAR_MULTIPOLICY_DISCOUNT)* $VAR_PRIOR_LOSS)* $VAR_WSTOVE)* $VAR_VALUEDCUSTOMER)"/>
		<xsl:value-of select="($VAR_3 -$VAR_SEASONALSECONDARYCREDIT) + $VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES" />
	-->
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--								ADJUSTEDBASE FOR INDIANA (REGULAR) HO-4					  -->
	<!-- 1. Base (Find Base in Territory By Policy Form 4 or 6, CO-I or CO-II, and Protection Class.) X
		2. Coverage C Factor (Tables based on whether is HO-4 with minimum $10,000 or HO-6 minimum $15,000.) X
		3. DELUXE Endorsement (if NO then X 1.00, if YES minimum Coverage C must be $25,000 and then take rate
		X 1.15-Min. $25.) NO MORE REQD AS WOLVERINE WILL NOT WRITE DELUXE ENDORSEMENTS
		4. Insurance Score Discount Factor X
		5. Term Factor: 1.00 Annual or .50 Semi-Annual X
		6. Deductible Factor X
		7. Protective Device Factor X
		8. Non-Smoker or Smoker Factor X
		9. Experience (Maturity or Age of Insured) Factor X
		10. Multiple Policy (Auto & Home) Factor (HO-4 must be minimum $25,000 Coverage C to get credit. HO-6
		no minimum.) X
		11. Valued Customer-Renewal Credit X
		12. Coverage A is 10% of Coverage C ($2,000 minimum), if Coverage A increased and there is no HO-66
		endorsement then rate is $2 per $1,000 increase. If an HO-66 Deluxe endorsement then charge is $2.20
		per $1,000 increased above automatic amount.
		13. Surcharges- (a) Prior Loss New Business; (b) Woodstove
		14. Apply Section II Credits if any ($9 if Secondary and we have the Primary; or $2 for Secondary and No
		Primary).
		15. Add-Any Other Endorsement Option Charges.
		16. Add-Section II Liability Charges (if any). -->
	<!-- ============================================================================================ -->
	<xsl:template name="ADJUSTEDBASE_REGULAR_INDIANA_HO4_HO6">
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="DEWLLING_MAIN_INDIANA" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE">
			<xsl:call-template name="C-VALUE" />
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:call-template name="INSURANCE_SCORE_CREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_TERMFACTOR">
			<xsl:call-template name="TERM_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_DFACTOR">
			<xsl:call-template name="DFACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR_PROTECTIVE_DEVICE">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_SMOKER">
			<xsl:call-template name="SMOKER" />
		</xsl:variable>
		<xsl:variable name="VAR_EXPERIENCE_DISCOUNT">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT">
			<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR_VALUEDCUSTOMER">
			<xsl:call-template name="VALUEDCUSTOMER" />
		</xsl:variable>
		<!-- TILL HERE  -->
		<xsl:variable name="VAR_PRIOR_LOSS">
			<xsl:call-template name="PRIOR_LOSS" />
		</xsl:variable>
		<xsl:variable name="VAR_WSTOVE">
			<xsl:call-template name="WSTOVE" />
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARYCREDIT">
			<xsl:call-template name="SEASONALSECONDARYCREDIT" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICAL_CHARGES">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABLITY_CHARGES">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<!-- return value of adjusted base-->
		<!--
		<xsl:variable name="VAR_1" select="round(round(round(round(round($VAR_BASE * $VAR_COVERAGE)* $VAR_INSURANCESCORE)* $VAR_TERMFACTOR)* $VAR_DFACTOR)* $VAR_PROTECTIVE_DEVICE)"/>
		<xsl:variable name="VAR_2" select="round(round(round(round(round(round($VAR_1 * $VAR_SMOKER) * $VAR_EXPERIENCE_DISCOUNT)* $VAR_MULTIPOLICY_DISCOUNT)*$VAR_VALUEDCUSTOMER)* $VAR_PRIOR_LOSS)* $VAR_WSTOVE)"/>
		<xsl:value-of select="($VAR_2 - $VAR_SEASONALSECONDARYCREDIT) + $VAR_MEDICAL_CHARGES + $VAR_LIABLITY_CHARGES" />
		-->
	</xsl:template>
	<!-- ###################################  END - INDIANA  ######################################## -->
	<!-- ============================================================================================ -->
	<!--						TEMPLATE FOR CALLING ADJUSTEDBASE AS PER THE INPUT-XML (START)		  -->
	<!-- ============================================================================================ -->
	<xsl:template name="CALL_ADJUSTEDBASE">
		<xsl:choose>
			<!-- For Michigan REPAIR COST -HO2 and HO3 have same formula -->
			<xsl:when test=" STATENAME ='MICHIGAN' and PRODUCT_PREMIER =$POLICYDESC_REPAIR and (PRODUCTNAME = $POLICYTYPE_HO2 or PRODUCTNAME = $POLICYTYPE_HO3 )">
				<xsl:call-template name="ADJUSTEDBASE_REPAIRCOST_MICHIGAN_HO2_HO3" />
			</xsl:when>
			<!-- For Michigan Replacement - HO2 and HO3 have same formula -->
			<xsl:when test="STATENAME ='MICHIGAN'   and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT  and (PRODUCTNAME = $POLICYTYPE_HO2 or PRODUCTNAME = $POLICYTYPE_HO3 )">
				<xsl:call-template name="ADJUSTEDBASE_REGULAR_MICHIGAN_HO2_HO3" />
			</xsl:when>
			<!-- For Michigan Replacement - HO5 -->
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO5 and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT">
				<xsl:call-template name="ADJUSTEDBASE_REGULAR_MICHIGAN_HO5" />
			</xsl:when>
			<!-- For Michigan - HO4 and HO6 have same formula -->
			<xsl:when test="STATENAME ='MICHIGAN' and (PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6)">
				<xsl:call-template name="ADJUSTEDBASE_REGULAR_MICHIGAN_HO4_HO6" />
			</xsl:when>
			<!-- For Michigan - HO3 PREMIER -->
			<xsl:when test="STATENAME ='MICHIGAN' and PRODUCTNAME = $POLICYTYPE_HO3 and PRODUCT_PREMIER = $POLICYDESC_PREMIER">
				<xsl:call-template name="ADJUSTEDBASE_PREMIER_MICHIGAN_HO3" />
			</xsl:when>
			<!-- For Michigan - HO5 PREMIER -->
			<xsl:when test="STATENAME ='MICHIGAN' and PRODUCTNAME = $POLICYTYPE_HO5 and PRODUCT_PREMIER = $POLICYDESC_PREMIER">
				<xsl:call-template name="ADJUSTEDBASE_PREMIER_MICHIGAN_HO5" />
			</xsl:when>
			<!-- For INDIANA REPLACEMENT -->
			<xsl:when test="STATENAME ='INDIANA' and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT and (PRODUCTNAME = $POLICYTYPE_HO2 or PRODUCTNAME = $POLICYTYPE_HO3 )">
				<xsl:call-template name="ADJUSTEDBASE_REGULAR_INDIANA_HO2_HO3" />
			</xsl:when>
			<xsl:when test="STATENAME ='INDIANA' and PRODUCTNAME = $POLICYTYPE_HO5 and PRODUCT_PREMIER = $POLICYDESC_REPLACEMENT">
				<xsl:call-template name="ADJUSTEDBASE_REGULAR_INDIANA_HO5" />
			</xsl:when>
			<!-- For INDIANA REPAIR COST -->
			<xsl:when test=" STATENAME ='INDIANA' and PRODUCT_PREMIER =$POLICYDESC_REPAIR and (PRODUCTNAME = $POLICYTYPE_HO2 or PRODUCTNAME = $POLICYTYPE_HO3 )">
				<xsl:call-template name="ADJUSTEDBASE_REPAIRCOST_INDIANA_HO2_HO3" />
			</xsl:when>
			<!-- For INDIANA -HO4, HO6-->
			<xsl:when test="STATENAME ='INDIANA' and (PRODUCTNAME = $POLICYTYPE_HO4 or PRODUCTNAME = $POLICYTYPE_HO6)">
				<xsl:call-template name="ADJUSTEDBASE_REGULAR_INDIANA_HO4_HO6" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--						TEMPLATE FOR CALLING ADJUSTEDBASE AS PER THE INPUT-XML (END)		  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--									TEST TEMPLATE 	(START)									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="TestRound">
	<xsl:param name="MyValue" />
	{
	<xsl:value-of select="round(normalize-space($VAR-DEWLLING-MAIN))"></xsl:value-of>
	}
</xsl:template>
	<xsl:template name="GET_PROTECTIVE_DEVICE_DISCOUNT_new">
		<!-- Get max applicable credit factor (for eg. 0.50 = 50%)                                                                                                                                                                                                                                                                ) -->
		<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
		<xsl:choose>
			<xsl:when test="CONSTRUCTIONCREDIT = 'N'"> <!-- Prot. Device credit Applicable only if not under construction -->
				<!-- Calculate the final credit uptil age of home 
				 If the calculated credit is greater than= max applicable credit factor then it means 
				 that total discount is greater than 50% and we do not need to send Protective Device discount -->
				<xsl:variable name="VAR_AGEOFHOME_FACTOR">
					<xsl:call-template name="AGEOFHOME_FACTOR" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$VAR_AGEOFHOME_FACTOR &gt;= $VAR_MAXCREDIT_FACTOR">
					0.00	<!-- No need to calculate Prot. Device discount as credit limit has exceeded -->
				</xsl:when>
					<xsl:otherwise> <!-- Else calculate the protective device -->
						<xsl:variable name="PROTDEVICE_CALCULATEDFACTOR">
							<xsl:call-template name="PROTECTIVEDEVICE" />
						</xsl:variable>
						<xsl:variable name="FINALCREDIT_UPTO_PROTDEVICE">
							<!-- (1-FACTOR because 10% credit = 0.9 factor, 20% credit = 0.8  and total credit  30% = ((1-0.9)+(1-0.8))=(0.1+0.2 =0.3))-->
							<xsl:value-of select="(1- $VAR_AGEOFHOME_FACTOR) + (1-$PROTDEVICE_CALCULATEDFACTOR)" />
						</xsl:variable>
						<xsl:choose>
							<!--including prot device, the credit is less than=  max total credit applicable -->
							<xsl:when test="$FINALCREDIT_UPTO_PROTDEVICE &lt;= $VAR_MAXCREDIT_FACTOR">
								<xsl:value-of select="PROTDEVICE_CALCULATEDFACTOR" />
							</xsl:when>
							<xsl:otherwise> <!--including prot device, the credit exceeds max total credit applicable -->
								<!-- Send the factor balance after subtracting age of home factor from maxcredit.-->
								<xsl:value-of select="($FINALCREDIT_UPTO_PROTDEVICE  - $VAR_MAXCREDIT_FACTOR) + $PROTDEVICE_CALCULATEDFACTOR" />
								<xsl:value-of select="$VAR_MAXCREDIT_FACTOR - (1 - $VAR_AGEOFHOME_FACTOR)" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
		1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="GET_PROTECTIVE_DEVICE_DISCOUNT_old"> <!-- TO BE REMOVED -->
		<!-- Get max applicable credit factor -->
		<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
		<xsl:choose>
			<xsl:when test="CONSTRUCTIONCREDIT = 'N'">
				<xsl:variable name="VAR1">
					<xsl:call-template name="AGEOFHOME_FACTOR" />
				</xsl:variable>
				<xsl:variable name="FINALCREDIT_AGEOFHOME"> <!-- Credit left after age of home -->
					<xsl:value-of select="1 - $VAR1" />
				</xsl:variable>
				<!-- Calculate the final credit uptil age of home 
				 If the calculated credit is less than 0.50 then it means 
				 that total discount is greater than 50%  and we do not need to send Protective Device discount-->
				<xsl:choose>
					<xsl:when test="$FINALCREDIT_AGEOFHOME &gt; $VAR_MAXCREDIT_FACTOR">
						<xsl:value-of select="$FINALCREDIT_AGEOFHOME" />
					</xsl:when>
					<xsl:otherwise> <!-- Else calculate the protective device -->
						<xsl:variable name="PROTDEVICE_CALCULATEDFACTOR">
							<xsl:call-template name="PROTECTIVEDEVICE" />
						</xsl:variable>
						<xsl:variable name="FINALCREDIT_PROTDEVICE"> <!-- Sum of credits uptil prot. device -->
							<xsl:value-of select="$FINALCREDIT_AGEOFHOME + (1 - $PROTDEVICE_CALCULATEDFACTOR)" />
						</xsl:variable>
						<xsl:choose>
							<xsl:when test="$FINALCREDIT_PROTDEVICE = 0.00">
								<xsl:value-of select="PROTDEVICE_CALCULATEDFACTOR" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:choose>
									<xsl:when test="$FINALCREDIT_PROTDEVICE  &gt; $VAR_MAXCREDIT_FACTOR">
										<xsl:value-of select="($FINALCREDIT_PROTDEVICE  - $VAR_MAXCREDIT_FACTOR) + $PROTDEVICE_CALCULATEDFACTOR" />
									</xsl:when>
									<xsl:otherwise>
										<xsl:choose>
											<xsl:when test="$FINALCREDIT_PROTDEVICE  &gt; $VAR_MAXCREDIT_FACTOR">
												<xsl:variable name="PROTDEVICEFACTOR">
												<xsl:value-of select="$VAR_MAXCREDIT_FACTOR - (1 - $FINALCREDIT_PROTDEVICE)" />)) 
											</xsl:variable>
												<xsl:value-of select="$PROTDEVICEFACTOR" />
											</xsl:when>
											<xsl:when test="$FINALCREDIT_PROTDEVICE  &lt; $VAR_MAXCREDIT_FACTOR">
												<xsl:value-of select="$PROTDEVICE_CALCULATEDFACTOR" />
											</xsl:when>
											<xsl:otherwise>1.00</xsl:otherwise>
										</xsl:choose>
									</xsl:otherwise>
								</xsl:choose>
							</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
				<!--including prot device, the credit becomes > 50% -->
			</xsl:when>
			<xsl:otherwise>
		1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="GET_PROTECTIVE_DEVICE_DISCOUNT">
		<!-- Get max applicable credit factor -->
		<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
		<xsl:variable name="VAR_MAXCREDIT_PERCENT" select="(1.00-$VAR_MAXCREDIT_FACTOR)*100" />
		<xsl:choose> <!-- Applicable only when building is not under construction-->
			<xsl:when test="CONSTRUCTIONCREDIT = 'N'">
				<xsl:variable name="VAR_AOH_FACTOR">
					<xsl:call-template name="AGEOFHOME_FACTOR" />
				</xsl:variable>
				<xsl:variable name="FINALCREDIT_AGEOFHOME"> <!-- Credit Percentage age of home -->
					<xsl:value-of select="(1.00 - $VAR_AOH_FACTOR)*100" />
				</xsl:variable>
				<!-- Calculate the final credit percentage uptil age of home 
				 1. If the calculated credit PERCENTAGE is greater than the max credit 
				 then send 1.00 for proptective device
				 2. If the calculated credit PERCENTAGE is lesser than the max credit 
				 then check the final credit percentage uptil protective device 
				 3. If the calculated credit PERCENTAGE is lesser than the max credit 
				 then send protective device  factor
				 4. If the calculated credit PERCENTAGE is greater than the max credit 
				 then send the (final credit percentage uptil protective device - aoh credit) factor				 
				 -->
				<xsl:choose>
					<xsl:when test="$FINALCREDIT_AGEOFHOME &gt; $VAR_MAXCREDIT_PERCENT">
					1.00
				</xsl:when>
					<xsl:otherwise> <!-- Else calculate the protective device -->
						<xsl:variable name="PROTDEVICE_CALCULATEDFACTOR">
							<xsl:call-template name="PROTECTIVEDEVICE" />
						</xsl:variable>
						<xsl:variable name="FINALCREDIT_PROTDEVICE"> <!-- Sum of credits uptil prot. device -->
							<xsl:value-of select="$FINALCREDIT_AGEOFHOME + ((1.00 - $PROTDEVICE_CALCULATEDFACTOR)*100)" />
						</xsl:variable>
						<xsl:choose>
							<xsl:when test="$FINALCREDIT_PROTDEVICE  &lt;= $VAR_MAXCREDIT_PERCENT">
								<xsl:value-of select="$PROTDEVICE_CALCULATEDFACTOR" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="(100 - ($VAR_MAXCREDIT_PERCENT - $FINALCREDIT_AGEOFHOME)) div 100.00" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
				<!--including prot device, the credit becomes > 50% -->
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="GET_DUC_DISCOUNT">
		<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
		<xsl:variable name="VAR1">
			<xsl:call-template name="AGEOFHOME_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="FINALCREDIT_PROT_DEVICE_DISCOUNT">
			<xsl:value-of select="(1.00 - $VAR1) + (1.00 - $VAR2)" />
		</xsl:variable>
		<!-- Calculate the final credit of home and protective device. 
				 If the credit is greater than 0.50 then it means that total discount is less than 50%  -->
		<xsl:choose>
			<xsl:when test="$FINALCREDIT_PROT_DEVICE_DISCOUNT &gt; $VAR_MAXCREDIT_FACTOR">
				<xsl:value-of select="$FINALCREDIT_PROT_DEVICE_DISCOUNT - $VAR_MAXCREDIT_FACTOR" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="DUC">
					<xsl:call-template name="DWELLING_UNDER_CONSTRUCTION" />
				</xsl:variable>
				<xsl:variable name="FINALCREDIT_PROT_DEVICE_NEW">
					<!--(
							(<xsl:value-of select="$FINALCREDIT_PROT_DEVICE_DISCOUNT"/>)
							+
							(1.00 - (<xsl:value-of select="$DUC"/>))
						) -->
					<xsl:value-of select="$FINALCREDIT_PROT_DEVICE_DISCOUNT + (1.00 - $DUC)" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$FINALCREDIT_PROT_DEVICE_NEW = 0.00">
						<xsl:call-template name="DWELLING_UNDER_CONSTRUCTION" />
					</xsl:when>
					<xsl:otherwise>
						<!--
							IF(<xsl:value-of select ="$FINALCREDIT_PROT_DEVICE_NEW"/> &gt; 0.50)
							THEN
								(<xsl:value-of select="$FINALCREDIT_PROT_DEVICE_NEW"/> - 0.50  ) + 
								(<xsl:value-of select = "$DUC"/>)
							ELSE
									IF (<xsl:value-of select="$FINALCREDIT_PROT_DEVICE_NEW"/> &gt; 0.50) 
									THEN
										<xsl:variable name="DUCFACTOR">
											(0.50-(1.00-<xsl:value-of select="$FINALCREDIT_PROT_DEVICE_NEW"/>)) 
										</xsl:variable>
										<xsl:value-of select="$DUCFACTOR"/>					
									ELSE IF (<xsl:value-of select="$FINALCREDIT_PROT_DEVICE_NEW"/> &lt;= 0.50) THEN
										<xsl:value-of select="$DUC"/>
									ELSE
										1.00	
							-->
						<xsl:choose>
							<xsl:when test="$FINALCREDIT_PROT_DEVICE_NEW  &gt; $VAR_MAXCREDIT_FACTOR">
								<xsl:value-of select="($FINALCREDIT_PROT_DEVICE_NEW  - $VAR_MAXCREDIT_FACTOR) + $DUC" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:choose>
									<xsl:when test="$FINALCREDIT_PROT_DEVICE_NEW &gt; $VAR_MAXCREDIT_FACTOR">
										<xsl:variable name="DUCFACTOR">
											<xsl:value-of select="$VAR_MAXCREDIT_FACTOR - (1 - $FINALCREDIT_PROT_DEVICE_NEW)" />
										</xsl:variable>
										<xsl:value-of select="$DUCFACTOR" />
									</xsl:when>
									<xsl:when test="$FINALCREDIT_PROT_DEVICE_NEW &lt;= $VAR_MAXCREDIT_FACTOR">
										<xsl:value-of select="$DUC" />
									</xsl:when>
									<xsl:otherwise>1.00 </xsl:otherwise>
								</xsl:choose>
							</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
		<!--including prot device, the credit becomes > 50% -->
	</xsl:template>
	<xsl:template name="GET_EXPERIENCE_DISCOUNT">
		<xsl:variable name="VAR1">
			<xsl:call-template name="AGEOFHOME_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="FINALCREDIT_DUC">
			<xsl:value-of select="(1.00-$VAR1)+(1.00-$VAR2)+(1.00-$VAR3)" />
		</xsl:variable>
		<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
		<xsl:choose>
			<xsl:when test="$FINALCREDIT_DUC &gt; 0.50">
				<xsl:value-of select="$FINALCREDIT_DUC - $VAR_MAXCREDIT_FACTOR" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="EXPERIENCE">
					<xsl:call-template name="EXPERIENCEFACTOR" />
				</xsl:variable>
				<xsl:variable name="FINALCREDIT_EXPERIENCE">
					<xsl:value-of select="$FINALCREDIT_DUC + (1- $EXPERIENCE)" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$FINALCREDIT_EXPERIENCE = 0.00">
						<xsl:call-template name="EXPERIENCEFACTOR" />
					</xsl:when>
					<xsl:otherwise>
						<!--
								IF( <xsl:value-of select ="$FINALCREDIT_EXPERIENCE"/> &gt; 0.50)
								THEN
									(<xsl:value-of select="$FINALCREDIT_EXPERIENCE"/> - 0.50  ) + 
									(<xsl:value-of select="$EXPERIENCE"/>)
								ELSE IF (<xsl:value-of select="$FINALCREDIT_EXPERIENCE"/> &lt;= 0.50) 
								THEN
									<xsl:value-of select="$EXPERIENCE"/>
								ELSE
									1.00
								-->
						<xsl:choose>
							<xsl:when test="$FINALCREDIT_EXPERIENCE &gt; $VAR_MAXCREDIT_FACTOR">
								<xsl:value-of select="($FINALCREDIT_EXPERIENCE - $VAR_MAXCREDIT_FACTOR) + $EXPERIENCE" />
							</xsl:when>
							<xsl:when test="$FINALCREDIT_EXPERIENCE &lt; $VAR_MAXCREDIT_FACTOR">
								<xsl:value-of select="$EXPERIENCE" />
							</xsl:when>
							<xsl:otherwise>1.00</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
		<!--including prot device, the credit becomes > 50% -->
	</xsl:template>
	<xsl:template name="GET_MULTIPOLICY_DISCOUNT">
		<xsl:variable name="VAR1">
			<xsl:call-template name="AGEOFHOME_FACTOR" />
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="GET_DUC_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="VAR4">
			<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />
		</xsl:variable>
		<xsl:variable name="FINALCREDIT_EXPERIENCE">
			<xsl:value-of select="(1.00-$VAR1)+(1.00-$VAR2)+(1.00-$VAR3)+(1.00-$VAR4)" />
		</xsl:variable>
		<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
		<xsl:choose>
			<xsl:when test="$FINALCREDIT_EXPERIENCE &gt; $VAR_MAXCREDIT_FACTOR">
				<xsl:value-of select="$FINALCREDIT_EXPERIENCE - $VAR_MAXCREDIT_FACTOR" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="MULTIPOLICY">
					<xsl:call-template name="MULTIPOLICY_FACTOR" />
				</xsl:variable>
				<xsl:variable name="FINALCREDIT_MULTIPOLICY">
					<xsl:value-of select="$FINALCREDIT_EXPERIENCE + (1 - $MULTIPOLICY)" />
				</xsl:variable>
				<!--<xsl:value-of select="$FINALCREDIT_MULTIPOLICY"/>	-->
				<!--
						IF(<xsl:value-of select ="$FINALCREDIT_MULTIPOLICY"/> &gt; 0.50)
						THEN
							<xsl:variable name="MULTIPOLICYFACTOR">
								(<xsl:value-of select="$FINALCREDIT_MULTIPOLICY"/> - 0.50  ) + 
								(<xsl:value-of select="$MULTIPOLICY"/>)
							</xsl:variable>
							<xsl:value-of select="$MULTIPOLICYFACTOR"/>		
						ELSE IF	(<xsl:value-of select ="$FINALCREDIT_MULTIPOLICY"/> &lt; 0.50)
						THEN
							<xsl:value-of select="$MULTIPOLICY"/>
						ELSE
							1.00			
						-->
				<xsl:choose>
					<xsl:when test="$FINALCREDIT_MULTIPOLICY &gt; $VAR_MAXCREDIT_FACTOR">
						<xsl:variable name="MULTIPOLICYFACTOR">
							<xsl:value-of select="($FINALCREDIT_MULTIPOLICY - $VAR_MAXCREDIT_FACTOR) + $MULTIPOLICY" />
						</xsl:variable>
						<xsl:value-of select="$MULTIPOLICYFACTOR" />
					</xsl:when>
					<xsl:when test="$FINALCREDIT_MULTIPOLICY &lt; $VAR_MAXCREDIT_FACTOR">
						<xsl:value-of select="$MULTIPOLICY" />
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="GET_TOTAL_DISCOUNT">
	<xsl:variable name="FINALCREDIT_EXPERIENCE">
		(1.00-(<xsl:call-template name="AGEOFHOME_FACTOR" />))
		+	(1.00-(<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />))
		+	(1.00-(<xsl:call-template name="GET_DUC_DISCOUNT" />))
		+	(1.00-(<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />))
		+	(1.00-(<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />))
	</xsl:variable>
	
	
<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />	
	IF (<xsl:value-of select="$FINALCREDIT_EXPERIENCE" /> &gt; <xsl:value-of select="$VAR_MAXCREDIT_FACTOR" />) 
	THEN
		1.00
	ELSE
		0.00
	
</xsl:template>
	<xsl:template name="REGULAR_INDIANA_HO3_WOODSTOVE_DISPLAY">
((((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name="C_VALUE_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name="DFACTOR" />)*
			(<xsl:call-template name="AGEOFHOME_FACTOR" />)
			)
			+
			(
				(<xsl:call-template name="COVERAGE_C_PREMIUM" /> )
				+ (<xsl:call-template name="COVERAGE_D_PREMIUM" />)
			)
		    
	   )
	   *
	   (<xsl:call-template name="PROTECTIVEDEVICE" />))*
	   (<xsl:call-template name="GET_DUC_DISCOUNT" />))*
	   (<xsl:call-template name="SMOKER" />))*
	   (<xsl:call-template name="EXPERIENCEFACTOR" />))*
	   (<xsl:call-template name="MULTIPOLICY_FACTOR" />))*
	   (<xsl:call-template name="PRIOR_LOSS" />))*
	   (<xsl:call-template name="WSTOVE" />)) - 
	   (((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name="C_VALUE_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name="DFACTOR" />)*
			(<xsl:call-template name="AGEOFHOME_FACTOR" />)
			)
			+
			(
				(<xsl:call-template name="COVERAGE_C_PREMIUM" /> )
				+ (<xsl:call-template name="COVERAGE_D_PREMIUM" />)
			)
		    
	   )
	   *
	   (<xsl:call-template name="PROTECTIVEDEVICE" />))*
	   (<xsl:call-template name="GET_DUC_DISCOUNT" />))*
	   (<xsl:call-template name="SMOKER" />))*
	   (<xsl:call-template name="EXPERIENCEFACTOR" />))*
	   (<xsl:call-template name="MULTIPOLICY_FACTOR" />))*
	   (<xsl:call-template name="PRIOR_LOSS" />))
	  
</xsl:template>
	<xsl:template name="REGULAR_INDIANA_HO3_PRIOR_LOSS_DISPLAY">
(((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name="C_VALUE_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name="DFACTOR" />)*
			(<xsl:call-template name="AGEOFHOME_FACTOR" />)
			)
			+
			(
				(<xsl:call-template name="COVERAGE_C_PREMIUM" /> )
				+ (<xsl:call-template name="COVERAGE_D_PREMIUM" />)
			)
		    
	   )
	   *
	   (<xsl:call-template name="PROTECTIVEDEVICE" />))*
	   (<xsl:call-template name="GET_DUC_DISCOUNT" />))*
	   (<xsl:call-template name="SMOKER" />))*
	   (<xsl:call-template name="EXPERIENCEFACTOR" />))*
	   (<xsl:call-template name="MULTIPOLICY_FACTOR" />))*
	   (<xsl:call-template name="PRIOR_LOSS" />))-
	   ((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name="C_VALUE_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name="DFACTOR" />)*
			(<xsl:call-template name="AGEOFHOME_FACTOR" />)
			)
			+
			(
				(<xsl:call-template name="COVERAGE_C_PREMIUM" /> )
				+ (<xsl:call-template name="COVERAGE_D_PREMIUM" />)
			)
		    
	   )
	   *
	   (<xsl:call-template name="PROTECTIVEDEVICE" />))*
	   (<xsl:call-template name="GET_DUC_DISCOUNT" />))*
	   (<xsl:call-template name="SMOKER" />))*
	   (<xsl:call-template name="EXPERIENCEFACTOR" />))*
	   (<xsl:call-template name="MULTIPOLICY_FACTOR" />))
	  
</xsl:template>
	<!-- For HO5 INDIANA START-->
	<xsl:template name="REGULAR_INDIANA_HO5_PRIOR_LOSS_DISPLAY">
	(((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template> )*
		(<xsl:call-template name="C_VALUE_INDIANA"></xsl:call-template>)*
		(<xsl:call-template name="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
		(<xsl:call-template name="TERM_FACTOR"></xsl:call-template>)*
		(<xsl:call-template name="DFACTOR" />)*
		(<xsl:call-template name="AGEOFHOME_FACTOR" />)
		)
		+
		(
			(<xsl:call-template name="COVERAGE_C_PREMIUM" /> )
			+ (<xsl:call-template name="COVERAGE_D_PREMIUM" />)
			+ (<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />)
			+ (75.00<!--xsl:call-template name="HO_34_REPLACEMENT_COST"/-->)
			+ (<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY" />)
		)
		
	)
	*
	(<xsl:call-template name="PROTECTIVEDEVICE" />))*
	(<xsl:call-template name="GET_DUC_DISCOUNT" />))*
	(<xsl:call-template name="SMOKER" />))*
	(<xsl:call-template name="EXPERIENCEFACTOR" />))*
	(<xsl:call-template name="MULTIPOLICY_FACTOR" />))*
	(<xsl:call-template name="PRIOR_LOSS" />))
	
	-
	((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template> )*
			(<xsl:call-template name="C_VALUE_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name="DFACTOR" />)*
			(<xsl:call-template name="AGEOFHOME_FACTOR" />)
			)
			+
			(
				(<xsl:call-template name="COVERAGE_C_PREMIUM" /> )
				+ (<xsl:call-template name="COVERAGE_D_PREMIUM" />)
				+ (<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />)
				+ (75.00<!--xsl:call-template name="HO_34_REPLACEMENT_COST"/-->)
				+ (<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY" />)
			)
		    
	   )
	   *
	   (<xsl:call-template name="PROTECTIVEDEVICE" />))*
	   (<xsl:call-template name="GET_DUC_DISCOUNT" />))*
	   (<xsl:call-template name="SMOKER" />))*
	   (<xsl:call-template name="EXPERIENCEFACTOR" />))*
	   (<xsl:call-template name="MULTIPOLICY_FACTOR" />))
	   
</xsl:template>
	<xsl:template name="REGULAR_INDIANA_HO5_WOODSTOVE_DISPLAY">
	((((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template> )*
		(<xsl:call-template name="C_VALUE_INDIANA"></xsl:call-template>)*
		(<xsl:call-template name="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
		(<xsl:call-template name="TERM_FACTOR"></xsl:call-template>)*
		(<xsl:call-template name="DFACTOR" />)*
		(<xsl:call-template name="AGEOFHOME_FACTOR" />)
		)
		+
		(
			(<xsl:call-template name="COVERAGE_C_PREMIUM" /> )
			+ (<xsl:call-template name="COVERAGE_D_PREMIUM" />)
			+ (<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />)
			+ (75.00<!--xsl:call-template name="HO_34_REPLACEMENT_COST"/-->)
			+ (<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY" />)
		)
		
	)
	*
	(<xsl:call-template name="PROTECTIVEDEVICE" />))*
	(<xsl:call-template name="GET_DUC_DISCOUNT" />))*
	(<xsl:call-template name="SMOKER" />))*
	(<xsl:call-template name="EXPERIENCEFACTOR" />))*
	(<xsl:call-template name="MULTIPOLICY_FACTOR" />))*
	(<xsl:call-template name="PRIOR_LOSS" />))*
	(<xsl:call-template name="WSTOVE" />))
	-
	((((((((<xsl:call-template name="DEWLLING_MAIN_INDIANA"></xsl:call-template> )*
			(<xsl:call-template name="C_VALUE_INDIANA"></xsl:call-template>)*
			(<xsl:call-template name="INSURANCE_SCORE_CREDIT"></xsl:call-template>)*
			(<xsl:call-template name="TERM_FACTOR"></xsl:call-template>)*
			(<xsl:call-template name="DFACTOR" />)*
			(<xsl:call-template name="AGEOFHOME_FACTOR" />)
			)
			+
			(
				(<xsl:call-template name="COVERAGE_C_PREMIUM" /> )
				+ (<xsl:call-template name="COVERAGE_D_PREMIUM" />)
				+ (<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />)
				+ (75.00<!--xsl:call-template name="HO_34_REPLACEMENT_COST"/-->)
				+ (<xsl:call-template name="HO_11_REPLACEMENT_COST_PERSONAL_PROPERTY" />)
			)
		    
	   )
	   *
	   (<xsl:call-template name="PROTECTIVEDEVICE" />))*
	   (<xsl:call-template name="GET_DUC_DISCOUNT" />))*
	   (<xsl:call-template name="SMOKER" />))*
	   (<xsl:call-template name="EXPERIENCEFACTOR" />))*
	   (<xsl:call-template name="MULTIPOLICY_FACTOR" />))
	
	   
</xsl:template>
	<!-- ============================================================================================ -->
	<!--									TEST TEMPLATE 	(END)									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="PREMIUM_GROUP">
		<!--
	<xsl:variable name="TERCODES" select="TERRITORYCODES"/>
	<xsl:variable name="GROUP_ID" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES [@TID = $TERCODES]/@GROUPID"/>
	-->
		<xsl:variable name="F_CODE" select="FORM_CODE" />
		<xsl:choose>
			<xsl:when test="normalize-space($F_CODE) != ''">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING']/ATTRIBUTES [@FORMID = $F_CODE]/@GROUPID" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Calculate the final credit uptil age of home 
				 If the credit is less than 0.50 then it means 
				 that total discount is greater than 50%  and we do not need to send Protective Device discount-->
	<xsl:template name="GET_PROTECTIVE_DEVICE_DISCOUNT_1">
	<!--<xsl:choose>
		<xsl:when test ="CONSTRUCTIONCREDIT = 'N'">
		
			<xsl:variable name="FINALCREDIT_AGEOFHOME_TOTAL_DISCOUNT">
					<xsl:call-template name ="AGEOFHOME_FACTOR"/>
			</xsl:variable>
			
			<xsl:variable name="FINALCREDIT_AGEOFHOME">
					<xsl:value-of select ="1.00 - $FINALCREDIT_AGEOFHOME_TOTAL_DISCOUNT"/>
			</xsl:variable>
		
			
			<xsl:choose>
				<xsl:when test = "$FINALCREDIT_AGEOFHOME &gt; 0.50">
					<xsl:value-of select="$FINALCREDIT_AGEOFHOME"/>
				</xsl:when>
				<xsl:otherwise>
						<xsl:variable name="PROTDEVICE_1">
							<xsl:call-template name ="PROTECTIVEDEVICE_TEMP"/>
						</xsl:variable>
						<xsl:variable name="FINALCREDIT_PROTDEVICE">
							<xsl:value-of select="$FINALCREDIT_AGEOFHOME + (1.00 - ($PROTDEVICE_1))"/>
						</xsl:variable>
						<xsl:choose>
						<xsl:when test = "$FINALCREDIT_PROTDEVICE = 0.00">
								<xsl:call-template name ="PROTECTIVEDEVICE_TEMP"/>
						</xsl:when>		
						<xsl:otherwise>
							<xsl:choose>
								<xsl:when test ="FINALCREDIT_PROTDEVICE &gt; 0.50">
									<xsl:value-of select="($FINALCREDIT_PROTDEVICE - 0.50 ) + $PROTDEVICE_1"/>
								</xsl:when>
								<xsl:otherwise>
									<xsl:choose>
										<xsl:when test ="$FINALCREDIT_PROTDEVICE &gt; 0.50">
											<xsl:variable name="PROTDEVICEFACTOR">
												<xsl:value-of select="(0.50-(1.00-$FINALCREDIT_PROTDEVICE))"/> 
											</xsl:variable>
											<xsl:value-of select="$PROTDEVICEFACTOR"/>	
										</xsl:when>
										<xsl:when test ="$FINALCREDIT_PROTDEVICE &lt;= 0.50">
											<xsl:value-of select="$PROTDEVICE_1"/>
										</xsl:when>
										<xsl:otherwise>1.00</xsl:otherwise>
									</xsl:choose>	
								</xsl:otherwise>
							</xsl:choose>
						</xsl:otherwise>					
					</xsl:choose>
				</xsl:otherwise>				
			</xsl:choose>	
		</xsl:when>
		<xsl:otherwise>
		1.00
		</xsl:otherwise>
	</xsl:choose>-->
	1.00
</xsl:template>
	<xsl:template name="PROTECTIVEDEVICE_TEMP">
		<xsl:choose>
			<xsl:when test="CONSTRUCTIONCREDIT = 'N'">
				<xsl:variable name="VER1">
					<xsl:call-template name="DIRECTBURGLERANDFIRE_TEMP" />
				</xsl:variable>
				<xsl:variable name="VER2">
					<xsl:call-template name="DIRECTFIREANDPOLICE_TEMP" />
				</xsl:variable>
				<xsl:variable name="VER3">
					<xsl:call-template name="LOCALFIREGASALARM_TEMP" />
				</xsl:variable>
				<xsl:variable name="PROTECTIVE_DEVICE_VALUE">
					<xsl:value-of select="$VER1 + $VER2 + $VER3"></xsl:value-of>
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$PROTECTIVE_DEVICE_VALUE = 0.00">1.00</xsl:when>
					<xsl:when test="$PROTECTIVE_DEVICE_VALUE &gt; 15.00">0.85</xsl:when>
					<xsl:when test="$PROTECTIVE_DEVICE_VALUE &gt; 0.00 and $PROTECTIVE_DEVICE_VALUE &lt; 15.00">
						<xsl:value-of select="(100.00 - $PROTECTIVE_DEVICE_VALUE) div 100.00"></xsl:value-of>
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Central Stations Burglary & Fire Alarm System -->
	<xsl:template name="DIRECTBURGLERANDFIRE_TEMP">
		<xsl:choose>
			<xsl:when test="BURGLAR = 'Y' and CENTRAL_FIRE = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='CSBF']/@CREDIT" />
			</xsl:when>
			<xsl:when test="BURGLAR = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='CSB']/@CREDIT" />
			</xsl:when>
			<xsl:when test="CENTRAL_FIRE = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='CSF']/@CREDIT" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Direct to Fire and Police -->
	<xsl:template name="DIRECTFIREANDPOLICE_TEMP">
		<xsl:choose>
			<xsl:when test="BURGLER_ALERT_POLICE = 'Y' and FIRE_ALARM_FIREDEPT = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='DFP']/@CREDIT" />
			</xsl:when>
			<xsl:when test="BURGLER_ALERT_POLICE = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='DP']/@CREDIT" />
			</xsl:when>
			<xsl:when test="FIRE_ALARM_FIREDEPT = 'Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='DF']/@CREDIT" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Local Fire or Local Gas Alarm -->
	<xsl:template name="LOCALFIREGASALARM_TEMP">
		<xsl:choose>
			<xsl:when test="N0_LOCAL_ALARM = 1">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='LFLG']/@CREDIT" />
			</xsl:when>
			<xsl:when test="N0_LOCAL_ALARM = 1">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='LFA']/@CREDIT" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="GET_DUC_DISCOUNT_TEMP">
		<xsl:variable name="VAR_MAXCREDIT_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/@MAXCREDITFACTOR" />
		<xsl:variable name="FINALCREDIT_PROT_DEVICE_DISCOUNT_TOTAL_DISCOUNT">
			<xsl:variable name="VAR1">
				<xsl:call-template name="AGEOFHOME_FACTOR" />
			</xsl:variable>
			<xsl:variable name="VAR2">
				<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT_1" />
			</xsl:variable>
			<xsl:value-of select="(1.00 - $VAR1) + (1.00 - $VAR2)" />
		</xsl:variable>
		<xsl:variable name="FINALCREDIT_PROT_DEVICE_DISCOUNT">
					0.00
			</xsl:variable>
		<xsl:choose>
			<xsl:when test="$FINALCREDIT_PROT_DEVICE_DISCOUNT &gt; $VAR_MAXCREDIT_FACTOR">
				<xsl:value-of select="$FINALCREDIT_PROT_DEVICE_DISCOUNT - $VAR_MAXCREDIT_FACTOR" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="DUC">
					<xsl:call-template name="DWELLING_UNDER_CONSTRUCTION" />
				</xsl:variable>
				<xsl:variable name="FINALCREDIT_PROT_DEVICE_NEW">
					<xsl:value-of select="$FINALCREDIT_PROT_DEVICE_DISCOUNT + (1.00 - $DUC)" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$FINALCREDIT_PROT_DEVICE_NEW = 0.00">
						<xsl:call-template name="DWELLING_UNDER_CONSTRUCTION" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:choose>
							<!-- <xsl:when test ="$FINALCREDIT_PROT_DEVICE_NEW &gt; 0.50"> -->
							<xsl:when test="$FINALCREDIT_PROT_DEVICE_NEW &gt; $VAR_MAXCREDIT_FACTOR">
								<!-- <xsl:value-of select="($FINALCREDIT_PROT_DEVICE_NEW - 0.50) + $DUC"/> -->
								<xsl:value-of select="($FINALCREDIT_PROT_DEVICE_NEW - $VAR_MAXCREDIT_FACTOR) + $DUC" />
							</xsl:when>
							<!-- <xsl:when test ="$FINALCREDIT_PROT_DEVICE_NEW &lt;= 0.50"> -->
							<xsl:when test="$FINALCREDIT_PROT_DEVICE_NEW &lt;= $VAR_MAXCREDIT_FACTOR">
								<xsl:value-of select="$DUC" />
							</xsl:when>
							<xsl:otherwise>1.00</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
		<!--including prot device, the credit becomes > 50% -->
	</xsl:template>
	<!-- this template not in use -->
	<xsl:template name="GET_EXPERIENCE_DISCOUNT_TEMP">
		<xsl:variable name="FINALCREDIT_DUC">
			<xsl:variable name="VAR1">
				<xsl:call-template name="AGEOFHOME_FACTOR" />
			</xsl:variable>
			<xsl:variable name="VAR2">
				<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT_1" />
			</xsl:variable>
			<xsl:variable name="VAR3">
				<xsl:call-template name="GET_DUC_DISCOUNT_TEMP" />
			</xsl:variable>
			<xsl:value-of select="(1.00 - $VAR1) + (1.00 - $VAR2) + (1.00 - $VAR3)" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$FINALCREDIT_DUC &gt; 0.50">
				<xsl:value-of select="$FINALCREDIT_DUC - 0.50" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="EXPERIENCE">
					<xsl:call-template name="EXPERIENCEFACTOR" />
				</xsl:variable>
				<xsl:variable name="FINALCREDIT_EXPERIENCE">
					<xsl:value-of select="$FINALCREDIT_DUC	+ (1.00 - $EXPERIENCE)" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$FINALCREDIT_EXPERIENCE = 0.00">
						<xsl:call-template name="EXPERIENCEFACTOR" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:choose>
							<xsl:when test="$FINALCREDIT_EXPERIENCE &gt; 0.50">
								<xsl:value-of select="($FINALCREDIT_EXPERIENCE - 0.50)  + $EXPERIENCE" />
							</xsl:when>
							<xsl:when test="$FINALCREDIT_EXPERIENCE &lt;= 0.50">
								<xsl:value-of select="$EXPERIENCE" />
							</xsl:when>
							<xsl:otherwise>1.00</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
		<!--including prot device, the credit becomes > 50% -->
	</xsl:template>
	<xsl:template name="COMBINE_COVERAGEA">
		<xsl:choose>
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO6"> <!-- for HO-6 and HO-6 deluxe -->
				<xsl:variable name="VAR1">
					<xsl:value-of select="PERSONALPROPERTYINCREASEDLIMITINCLUDE" />
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:value-of select="PERSONALPROPERTYINCREASEDLIMITADDITIONAL" />
				</xsl:variable>
				<xsl:value-of select="$VAR1+$VAR2" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="DWELLING_LIMITS" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HO32_UNIT_OWNER">
		<xsl:choose>
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO6 and HO32 ='Y'"> <!-- for HO-6 products -->
				<xsl:variable name="VAR_HO32_UNIT_OWNER" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'OPTIONALSECTIONCOVG']/NODE[@ID='HO_32_UNIT_OWNER']/ATTRIBUTES[@ID = '1']/@CHARGE" />
				<xsl:if test="normalize-space($VAR_HO32_UNIT_OWNER) = ''">
					<tr>
						<td class="midcolora">
							<li> charges for HO32 unit owner not found  (Path - "FACTOR[@ID = 'OPTIONALSECTIONCOVG']/NODE[@ID='HO_32_UNIT_OWNER']/ATTRIBUTES[@ID = '1']/@CHARGE") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- HO-50 PERSONAL PROPERTY AWAY FROM PREMISES  -->
	<xsl:template name="HO50_PERSONAL_PROPERTY_AWAY_PREMISES">
		<xsl:variable name="PNAME" select="PRODUCTNAME"></xsl:variable>
		<xsl:variable name="PCOVERAGE_C_ADDITIONAL_VALUE" select="PERSONALPROPERTYAWAYADDITIONAL"></xsl:variable>
		<xsl:variable name="PCOVERAGE_C_ADDITIONAL_COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = 'HO-50']/@COST"></xsl:variable>
		<xsl:if test="normalize-space($PCOVERAGE_C_ADDITIONAL_COST) = ''">
			<tr>
				<td class="midcolora">
					<li> coverage C additional value not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = 'HO-50']/@COST") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="PCOVERAGE_C_ADDITIONAL_INCREASEDAMOUNT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = 'HO-50']/@INCREASEDAMOUNT"></xsl:variable>
		<xsl:if test="normalize-space($PCOVERAGE_C_ADDITIONAL_INCREASEDAMOUNT) = ''">
			<tr>
				<td class="midcolora">
					<li> coverage C increase amount not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = 'HO-50']/@INCREASEDAMOUNT") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--
			<xsl:choose>
				<xsl:when test ="$PCOVERAGE_C_ADDITIONAL_VALUE = 0"></xsl:when>
				<xsl:otherwise>
						<xsl:variable name="VAR_ADDITIONALCOST"> 
							<xsl:value-of select ="$PCOVERAGE_C_ADDITIONAL_COST"></xsl:value-of>
						</xsl:variable>
						<xsl:variable name="VAR_ADDITIONAL_COVERAGE"> 
							<xsl:value-of select ="$PCOVERAGE_C_ADDITIONAL_VALUE"></xsl:value-of>
						</xsl:variable>
						<xsl:variable name="VAR_RATE_PER_VALUE"> 
							<xsl:value-of select ="$PCOVERAGE_C_ADDITIONAL_INCREASEDAMOUNT"></xsl:value-of>
						</xsl:variable>
						<xsl:value-of select="($VAR_ADDITIONAL_COVERAGE div $VAR_RATE_PER_VALUE) * $VAR_ADDITIONALCOST  "/> 
				</xsl:otherwise>
			</xsl:choose>
		-->
	</xsl:template>
	<xsl:template name="HO51_BULDING_ALTER">
		<xsl:choose>
			<xsl:when test="PRODUCTNAME = $POLICYTYPE_HO4">
				<xsl:variable name="PNAME" select="PRODUCTNAME"></xsl:variable>
				<xsl:variable name="PBLDG_ALTERATION_ADDITIONAL" select="PERSONALPROPERTYINCREASEDLIMITADDITIONAL"></xsl:variable>
				<xsl:variable name="PHO51_RATE_PER_COST" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = 'HO-51']/@COST"></xsl:variable>
				<xsl:if test="normalize-space($PHO51_RATE_PER_COST) = ''">
					<tr>
						<td class="midcolora">
							<li> Cost for HO51 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = 'HO-51']/@COST") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:variable name="PHO51_RATE_PER" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = 'HO-51']/@INCREASEDAMOUNT"></xsl:variable>
				<xsl:if test="normalize-space($PHO51_RATE_PER) = ''">
					<tr>
						<td class="midcolora">
							<li> Increased amount for HO51 not found  (Path - "FACTOR[@ID='OPTIONALSECTIONCOVG']/NODE[@ID='PERSONAL_PROPERTY_COVERAGE_C']/ATTRIBUTES[@FORM = 'HO-51']/@INCREASEDAMOUNT") </li>
						</td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<!--<xsl:value-of select="($PBLDG_ALTERATION_ADDITIONAL div $PHO51_RATE_PER) * $PHO51_RATE_PER_COST"/> -->
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MEDICAL_CHARGES">
		<xsl:variable name="VAR_FOR_EACH_ADDITION" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='PRIMARY_RESIDENCE']/@FOR_EACH_ADDITION" />
		<xsl:if test="normalize-space($VAR_FOR_EACH_ADDITION) = ''">
			<tr>
				<td class="midcolora">
					<li> Premium Group Code not found  (Path - "FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
		<xsl:if test="normalize-space($VAR_RATE_PERVAL_MEDPAY) = ''">
			<tr>
				<td class="midcolora">
					<li> Medpay rate per value not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--		
		<xsl:choose>
			<xsl:when test="MEDICALPAYMENTSTOOTHERS_LIMIT &gt; 0">
				<xsl:value-of select="((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_RATE_PERVAL_MEDPAY) div $VAR_RATE_PERVAL_MEDPAY) * $VAR_FOR_EACH_ADDITION"></xsl:value-of>	
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
		-->
	</xsl:template>
	<xsl:template name="LIABLITY_CHARGES">
		<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
		<xsl:variable name="VAR_LIABILITY_LIMIT_GP" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='DESCRIBED_PRIMARY_RESIDENCE']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP" />
		<xsl:if test="normalize-space($VAR_LIABILITY_LIMIT_GP) = ''">
			<tr>
				<td class="midcolora">
					<li> Liablity limit group not found  (Path - "FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID='DESCRIBED_PRIMARY_RESIDENCE']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_GP") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- <xsl:value-of select ="$VAR_LIABILITY_LIMIT_GP"/> 

		<xsl:choose>  
			<xsl:when test="$VAR_LIABILITY_LIMIT_GP != '' and $VAR_LIABILITY_LIMIT_GP &gt; 0 and $VAR_LIABILITY_LIMIT_GP != 'Included'"> 
					<xsl:value-of select ="$VAR_LIABILITY_LIMIT_GP"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>	
		</xsl:choose>
		-->
	</xsl:template>
	<xsl:template name="TERRITORY_CODES">
		<xsl:variable name="TCODES" select="TERRITORYCODES" />
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE/ATTRIBUTES[@TID = $TCODES]/@GROUPID" />
	</xsl:template>
	<xsl:template name="REPAIR_COST_FACTOR">
		<xsl:variable name="VAR_REPAIR_COST_FACTOR">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='REPAIR_COST_FACTOR']/NODE[@ID='REPAIR_COST']/ATTRIBUTES/@FACTOR" />
		</xsl:variable>
		<xsl:if test="normalize-space($VAR_REPAIR_COST_FACTOR) = ''">
			<tr>
				<td class="midcolora">
					<li> Repair Cost factor not found  (Path - "FACTOR[@ID='REPAIR_COST_FACTOR']/NODE[@ID='REPAIR_COST']/ATTRIBUTES/@FACTOR") </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0) = 1"></xsl:if>
		</xsl:if>
		<xsl:value-of select="$VAR_REPAIR_COST_FACTOR" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--					Templates for Replacement Cost [Factor,Credit,Display](SATRT)			  -->
	<!-- ============================================================================================ -->
	<!-- In the case of Replacement Cost depends on the percentage use -->
	<xsl:template name="REPLACEMENT_COST_FACTOR">
		<xsl:variable name="P_REPLACEMENT_COST">
			<xsl:value-of select="round((DWELLING_LIMITS div REPLACEMENTCOSTFACTOR)*100)" />
		</xsl:variable>
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='REPLACEMENT_COST_FACTOR']/NODE[@ID ='REPLACEMENT_COST']/ATTRIBUTES[@LOWER_PERCENTAGE_LIMIT &lt;= $P_REPLACEMENT_COST  and @UPPER_PERCENTAGE_LIMIT &gt;= $P_REPLACEMENT_COST]/@FACTOR" />
	</xsl:template>
	<!-- For Display Only -->
	<!-- &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&  -->
	<!-- &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&& START OF MAIN_FOR_INPUT_XML  &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&  -->
	<!-- &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&  -->
	<xsl:template name="MAIN_FOR_INPUT_XML">
		<xsl:if test="user:AllVerified(1) = 1">
			<b> Error Details of XML </b>
		</xsl:if>
		<xsl:variable name="VAR_STATENAME" select="STATENAME" />
		<xsl:if test="normalize-space($VAR_STATENAME) = ''">
			<tr>
				<td class="midcolora">
					<li> The Node for state name is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_NEWBUSINESSFACTOR" select="normalize-space(NEWBUSINESSFACTOR)" />
		<xsl:if test="$VAR_NEWBUSINESSFACTOR != 'N' and $VAR_NEWBUSINESSFACTOR !='Y'">
			<tr>
				<td class="midcolora">
					<li> The Node for newbusiness factor is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_QUOTEEFFDATE" select="normalize-space(QUOTEEFFDATE)" />
		<xsl:if test="normalize-space($VAR_QUOTEEFFDATE) = ''">
			<tr>
				<td class="midcolora">
					<li> The node for quote effective date is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_QUOTEEXPDATE" select="normalize-space(QUOTEEXPDATE)" />
		<xsl:if test="normalize-space($VAR_QUOTEEXPDATE) = ''">
			<tr>
				<td class="midcolora">
					<li> The node for quote expiry date is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_TERMFACTOR" select="TERMFACTOR" />
		<xsl:if test="normalize-space($VAR_TERMFACTOR) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for term factor is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SEASONALSECONDARY" select="normalize-space(SEASONALSECONDARY)" />
		<xsl:if test="$VAR_SEASONALSECONDARY != 'Y' and $VAR_SEASONALSECONDARY != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for seasonal secondary is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_WOLVERINEINSURESPRIMARY" select="normalize-space(WOLVERINEINSURESPRIMARY)" />
		<xsl:if test="$VAR_WOLVERINEINSURESPRIMARY != 'Y' and $VAR_WOLVERINEINSURESPRIMARY != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for wolverine insures primary is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_PRODUCTNAME" select="PRODUCTNAME" />
		<xsl:if test="normalize-space($VAR_PRODUCTNAME) = ''">
			<tr>
				<td class="midcolora">
					<li> The Node for product name is blank  </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_PRODUCT_PREMIER" select="PRODUCT_PREMIER" />
		<xsl:if test="normalize-space($VAR_PRODUCT_PREMIER) = ''">
			<tr>
				<td class="midcolora">
					<li> The Node for product premier is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_REPLACEMENTCOSTFACTOR" select="REPLACEMENTCOSTFACTOR" />
		<xsl:if test="normalize-space($VAR_REPLACEMENTCOSTFACTOR) = ''">
			<tr>
				<td class="midcolora">
					<li>The Node for replacement cost factor is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_DWELLING_LIMITS" select="DWELLING_LIMITS" />
		<xsl:if test="normalize-space($VAR_DWELLING_LIMITS) = ''">
			<tr>
				<td class="midcolora">
					<li>The Node for dwelling limits is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_PROTECTIONCLASS" select="PROTECTIONCLASS" />
		<xsl:if test="normalize-space($VAR_PROTECTIONCLASS) = ''">
			<tr>
				<td class="midcolora">
					<li>The Node for protection class is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_DISTANCET_FIRESTATION" select="DISTANCET_FIRESTATION" />
		<xsl:if test="normalize-space($VAR_DISTANCET_FIRESTATION) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for distancet firestation is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_FEET2HYDRANT" select="FEET2HYDRANT" />
		<xsl:if test="normalize-space($VAR_FEET2HYDRANT) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for feet to hydrant is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_EXTERIOR_CONSTRUCTION" select="EXTERIOR_CONSTRUCTION" />
		<xsl:if test="normalize-space($VAR_EXTERIOR_CONSTRUCTION) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for exterior construction is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_EXTERIOR_CONSTRUCTION_F_M" select="EXTERIOR_CONSTRUCTION_F_M" />
		<xsl:if test="normalize-space($VAR_EXTERIOR_CONSTRUCTION_F_M) = ''">
			<tr>
				<td class="midcolora">
					<li>The Node for exterior construction Frame Masaonry is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_DOC" select="DOC" />
		<xsl:if test="normalize-space($VAR_DOC) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for Date of construction (DOC) is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_DEDUCTIBLE" select="DEDUCTIBLE" />
		<xsl:if test="normalize-space($VAR_DEDUCTIBLE) = ''">
			<tr>
				<td class="midcolora">
					<li>The Node for deductible is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_AGEOFHOME" select="AGEOFHOME" />
		<xsl:if test="normalize-space($VAR_AGEOFHOME) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for age of home is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_NUMBEROFFAMILIES" select="NUMBEROFFAMILIES" />
		<xsl:if test="normalize-space($VAR_NUMBEROFFAMILIES) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for number of families is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_N0_LOCAL_ALARM" select="NUMBEROFUNITS" />
		<xsl:if test="normalize-space($VAR_N0_LOCAL_ALARM) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for number of units is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_PERSONALLIABILITY_LIMIT" select="PERSONALLIABILITY_LIMIT" />
		<xsl:if test="normalize-space($VAR_PERSONALLIABILITY_LIMIT) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for personal liability limit is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_PERSONALLIABILITY_PREMIUM" select="PERSONALLIABILITY_PREMIUM" />
		<xsl:if test="normalize-space($VAR_PERSONALLIABILITY_PREMIUM) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for personalliability premium is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_PERSONALLIABILITY_DEDUCTIBLE" select="PERSONALLIABILITY_DEDUCTIBLE" />
		<xsl:if test="normalize-space($VAR_PERSONALLIABILITY_DEDUCTIBLE) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for Personal liability deductible is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_MEDICALPAYMENTSTOOTHERS_LIMIT" select="MEDICALPAYMENTSTOOTHERS_LIMIT" />
		<xsl:if test="normalize-space($VAR_MEDICALPAYMENTSTOOTHERS_LIMIT) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for Personal liability deductible is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_MEDICALPAYMENTSTOOTHERS_PREMIUM" select="MEDICALPAYMENTSTOOTHERS_PREMIUM" />
		<xsl:if test="normalize-space($VAR_MEDICALPAYMENTSTOOTHERS_PREMIUM) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for medical payments to others premium is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_MEDICALPAYMENTSTOOTHERS_DEDUCTIBLE" select="MEDICALPAYMENTSTOOTHERS_DEDUCTIBLE" />
		<xsl:if test="normalize-space($VAR_MEDICALPAYMENTSTOOTHERS_DEDUCTIBLE) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for medical payments to others deductible is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_FORM_CODE" select="FORM_CODE" />
		<xsl:if test="normalize-space($VAR_FORM_CODE) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for form code is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_NOPETS" select="NOPETS" />
		<xsl:if test="normalize-space($VAR_NOPETS) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for number of pets is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_HO20" select="normalize-space(HO20)" />
		<xsl:if test="$VAR_HO20 != 'Y' and $VAR_HO20 != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for HO20 is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_HO21" select="normalize-space(HO21)" />
		<xsl:if test="$VAR_HO21 != 'Y' and $VAR_HO21 != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for HO21 is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_HO25" select="normalize-space(HO25)" />
		<xsl:if test="$VAR_HO25 != 'Y' and $VAR_HO25 != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for HO25 is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_HO23" select="normalize-space(HO23)" />
		<xsl:if test="$VAR_HO23 != 'Y' and $VAR_HO23 != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for HO23 is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_HO34" select="normalize-space(HO34)" />
		<xsl:if test="$VAR_HO34 != 'Y' and $VAR_HO34 != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for HO34 is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_HO11" select="normalize-space(HO11)" />
		<xsl:if test="$VAR_HO11 != 'Y' and $VAR_HO11 != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for HO11 is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_HO32" select="normalize-space(HO32)" />
		<xsl:if test="$VAR_HO32 != 'Y' and $VAR_HO32 != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for HO32 is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_HO277" select="normalize-space(HO277)" />
		<xsl:if test="$VAR_HO277 != 'Y' and $VAR_HO277 != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for HO277 is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_HO455" select="normalize-space(HO455)" />
		<xsl:if test="$VAR_HO455 != 'Y' and $VAR_HO455 != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for HO455 is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- for HO327 - in case of null the proc pass ''  it is to be correctd at proc level -->
		<!--	
		<xsl:variable name="VAR_HO327" select="HO327"/> 
			<xsl:if test="normalize-space($VAR_HO327) = ''">
				<tr>
					<td class="midcolora"><li>The node for HO327 is blank </li> </td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		-->
		<xsl:variable name="VAR_HO33" select="HO33" />
		<xsl:if test="normalize-space($VAR_HO33) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for HO33 is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_HO315" select="normalize-space(HO315)" />
		<xsl:if test="$VAR_HO315 != 'Y' and $VAR_HO315 != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for HO315 is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_HO9" select="normalize-space(HO9)" />
		<xsl:if test="$VAR_HO9 != 'Y' and $VAR_HO9 != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for HO9 is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_HO287" select="normalize-space(HO287)" />
		<xsl:if test="$VAR_HO287 != 'Y' and $VAR_HO287 != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for HO287 is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_HO96FINALVALUE" select="HO96FINALVALUE" />
		<xsl:if test="normalize-space($VAR_HO96FINALVALUE) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for HO96 final value is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_HO48INCLUDE" select="HO48INCLUDE" />
		<xsl:if test="normalize-space($VAR_HO48INCLUDE) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for HO48 Include is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- correct it at proc level 
		<xsl:variable name="VAR_HO40INCLUDE" select="HO40INCLUDE"/> 
			<xsl:if test="normalize-space($VAR_HO40INCLUDE) = ''">
				<tr>
					<td class="midcolora"><li>The node for HO40 Include is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>	
		-->
		<xsl:variable name="VAR_PERSONALPROPERTYINCREASEDLIMITINCLUDE" select="normalize-space(PERSONALPROPERTYINCREASEDLIMITINCLUDE)" />
		<xsl:if test="$VAR_PERSONALPROPERTYINCREASEDLIMITINCLUDE = ''">
			<tr>
				<td class="midcolora">
					<li>The node for Personal Property Increased Limit Include is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_PERSONALPROPERTYAWAYINCLUDE" select="PERSONALPROPERTYAWAYINCLUDE" />
		<xsl:if test="normalize-space($VAR_PERSONALPROPERTYAWAYINCLUDE) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for Personal Property Away Include is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_UNSCHEDULEDJEWELRYINCLUDE" select="UNSCHEDULEDJEWELRYINCLUDE" />
		<xsl:if test="normalize-space($VAR_UNSCHEDULEDJEWELRYINCLUDE) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for Unscheduled Jewelry Include is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_MONEYINCLUDE" select="MONEYINCLUDE" />
		<xsl:if test="normalize-space($VAR_MONEYINCLUDE) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for Money Include is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SECURITIESINCLUDE" select="SECURITIESINCLUDE" />
		<xsl:if test="normalize-space($VAR_SECURITIESINCLUDE) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for Securities Include is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SILVERWAREINCLUDE" select="SILVERWAREINCLUDE" />
		<xsl:if test="normalize-space($VAR_SILVERWAREINCLUDE) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for Silver Ware Include is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_FIREARMSINCLUDE" select="FIREARMSINCLUDE" />
		<xsl:if test="normalize-space($VAR_FIREARMSINCLUDE) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for Fire Arms Include is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_HO312INCLUDE" select="HO312INCLUDE" />
		<xsl:if test="normalize-space($VAR_HO312INCLUDE) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for HO312 Include is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_LOSSOFUSE_LIMIT" select="LOSSOFUSE_LIMIT" />
		<xsl:if test="normalize-space($VAR_LOSSOFUSE_LIMIT) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for Loss of use Limit is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_ADDITIONALLIVINGEXPENSEINCLUDE" select="ADDITIONALLIVINGEXPENSEINCLUDE" />
		<xsl:if test="normalize-space($VAR_ADDITIONALLIVINGEXPENSEINCLUDE) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for Additional Living Expense Include is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_HO53INCLUDE" select="HO53INCLUDE" />
		<xsl:if test="normalize-space($VAR_HO53INCLUDE) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for HO53 include  is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_HO35INCLUDE" select="HO35INCLUDE" />
		<xsl:if test="normalize-space($VAR_HO35INCLUDE) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for HO35 Include is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_OCCUPIED_INSURED" select="OCCUPIED_INSURED" />
		<xsl:if test="normalize-space($VAR_OCCUPIED_INSURED) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for occupied insured is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_RESIDENCE_PREMISES" select="RESIDENCE_PREMISES" />
		<xsl:if test="normalize-space($VAR_RESIDENCE_PREMISES) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for residence premises  is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_OTHER_LOC_1FAMILY" select="OTHER_LOC_1FAMILY" />
		<xsl:if test="normalize-space($VAR_OTHER_LOC_1FAMILY) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for Other location one family Include is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_OTHER_LOC_2FAMILY" select="OTHER_LOC_2FAMILY" />
		<xsl:if test="normalize-space($VAR_OTHER_LOC_2FAMILY) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for Other location two family is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_ONPREMISES_HO42" select="normalize-space(ONPREMISES_HO42)" />
		<xsl:if test="$VAR_ONPREMISES_HO42 != 'Y' and $VAR_ONPREMISES_HO42 != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for On Premises HO42 is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_LOCATED_OTH_STRUCTURE" select="normalize-space(LOCATED_OTH_STRUCTURE)" />
		<xsl:if test="$VAR_LOCATED_OTH_STRUCTURE != 'Y' and $VAR_LOCATED_OTH_STRUCTURE != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for Located to other structure is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_INSTRUCTIONONLY_HO42" select="normalize-space(INSTRUCTIONONLY_HO42)" />
		<xsl:if test="$VAR_INSTRUCTIONONLY_HO42 != 'Y' and $VAR_INSTRUCTIONONLY_HO42 != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for Instruction only HO42 is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_OFF_PREMISES_HO43" select="normalize-space(OFF_PREMISES_HO43)" />
		<xsl:if test="$VAR_OFF_PREMISES_HO43 != 'Y' and $VAR_OFF_PREMISES_HO43 != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for Off premises HO43  is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_PIP_HO82" select="normalize-space(PIP_HO82)" />
		<xsl:if test="$VAR_PIP_HO82 != 'Y' and $VAR_PIP_HO82 != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for PIP HO82 is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_HO200" select="normalize-space(HO200)" />
		<xsl:if test="$VAR_HO200 != 'Y' and $VAR_HO200 != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for HO200 is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_RESIDENCE_EMP_NUMBER" select="RESIDENCE_EMP_NUMBER" />
		<xsl:if test="normalize-space($VAR_RESIDENCE_EMP_NUMBER) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for residence employee number is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_CLERICAL_OFFICE_HO71" select="normalize-space(CLERICAL_OFFICE_HO71)" />
		<xsl:if test="$VAR_CLERICAL_OFFICE_HO71 != 'Y' and $VAR_CLERICAL_OFFICE_HO71 != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for clerical office HO71  is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SALESMEN_INC_INSTALLATION" select="normalize-space(SALESMEN_INC_INSTALLATION)" />
		<xsl:if test="$VAR_SALESMEN_INC_INSTALLATION != 'Y' and $VAR_SALESMEN_INC_INSTALLATION != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for sales men include installation  is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_TEACHER_ATHELETIC" select="normalize-space(TEACHER_ATHELETIC)" />
		<xsl:if test="$VAR_TEACHER_ATHELETIC != 'Y' and $VAR_TEACHER_ATHELETIC != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for teacher atheletic  is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_TEACHER_NOC" select="normalize-space(TEACHER_NOC)" />
		<xsl:if test="$VAR_TEACHER_NOC != 'Y' and $VAR_TEACHER_NOC != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for teacher noc  is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_INCIDENTAL_FARMING_HO72" select="normalize-space(INCIDENTAL_FARMING_HO72)" />
		<xsl:if test="$VAR_INCIDENTAL_FARMING_HO72 != 'Y' and $VAR_INCIDENTAL_FARMING_HO72 != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for incidental farming HO72 is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_OTH_LOC_OPR_EMPL_HO73" select="OTH_LOC_OPR_EMPL_HO73" />
		<xsl:if test="normalize-space($VAR_OTH_LOC_OPR_EMPL_HO73) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for other location HO73 is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_OTH_LOC_OPR_OTHERS_HO73" select="OTH_LOC_OPR_OTHERS_HO73" />
		<xsl:if test="normalize-space($VAR_OTH_LOC_OPR_OTHERS_HO73) = ''">
			<tr>
				<td class="midcolora">
					<li>The node for oth_loc_opr_others_ho73 is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_LOSSFREE" select="normalize-space(LOSSFREE)" />
		<xsl:if test="$VAR_LOSSFREE != 'Y' and $VAR_LOSSFREE != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for loss free   is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_NOTLOSSFREE" select="normalize-space(NOTLOSSFREE)" />
		<xsl:if test="$VAR_NOTLOSSFREE != 'Y' and $VAR_NOTLOSSFREE != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for not loss free is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_VALUEDCUSTOMER" select="normalize-space(VALUEDCUSTOMER)" />
		<xsl:if test="$VAR_VALUEDCUSTOMER != 'Y' and $VAR_VALUEDCUSTOMER != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for valued customer   is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_MULTIPLEPOLICYFACTOR" select="normalize-space(MULTIPLEPOLICYFACTOR)" />
		<xsl:if test="$VAR_MULTIPLEPOLICYFACTOR != 'Y' and $VAR_MULTIPLEPOLICYFACTOR != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for multiple policy factor is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_NONSMOKER" select="normalize-space(NONSMOKER)" />
		<xsl:if test="$VAR_NONSMOKER != 'Y' and $VAR_NONSMOKER != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for non smoker  is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_EXPERIENCE" select="normalize-space(EXPERIENCE)" />
		<xsl:if test="$VAR_EXPERIENCE = ''">
			<tr>
				<td class="midcolora">
					<li>The node for experience is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_CONSTRUCTIONCREDIT" select="normalize-space(CONSTRUCTIONCREDIT)" />
		<xsl:if test="$VAR_CONSTRUCTIONCREDIT != 'Y' and $VAR_CONSTRUCTIONCREDIT != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for construction credit  is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_BURGLER_ALERT_POLICE" select="normalize-space(BURGLER_ALERT_POLICE)" />
		<xsl:if test="$VAR_BURGLER_ALERT_POLICE != 'Y' and $VAR_BURGLER_ALERT_POLICE != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for burgler alert police   is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_FIRE_ALARM_FIREDEPT" select="normalize-space(FIRE_ALARM_FIREDEPT)" />
		<xsl:if test="$VAR_FIRE_ALARM_FIREDEPT != 'Y' and $VAR_FIRE_ALARM_FIREDEPT != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for fire alarm firedept   is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_BURGLAR" select="normalize-space(BURGLAR)" />
		<xsl:if test="$VAR_BURGLAR != 'Y' and $VAR_BURGLAR != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for burglar is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_CENTRAL_FIRE" select="normalize-space(CENTRAL_FIRE)" />
		<xsl:if test="$VAR_CENTRAL_FIRE != 'Y' and $VAR_CENTRAL_FIRE != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for central fire is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_CENT_ST_BURG_FIRE" select="normalize-space(CENT_ST_BURG_FIRE)" />
		<xsl:if test="$VAR_CENT_ST_BURG_FIRE != 'Y' and $VAR_CENT_ST_BURG_FIRE != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for cent burg fire  is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_DIR_FIRE_AND_POLICE" select="normalize-space(DIR_FIRE_AND_POLICE)" />
		<xsl:if test="$VAR_DIR_FIRE_AND_POLICE != 'Y' and $VAR_DIR_FIRE_AND_POLICE != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for direct fire and police is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_LOC_FIRE_GAS" select="normalize-space(LOC_FIRE_GAS)" />
		<xsl:if test="$VAR_LOC_FIRE_GAS != 'Y' and $VAR_LOC_FIRE_GAS != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for location fire gas is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_TWO_MORE_FIRE" select="normalize-space(TWO_MORE_FIRE)" />
		<xsl:if test="$VAR_TWO_MORE_FIRE != 'Y' and $VAR_TWO_MORE_FIRE != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for two more fire is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_INSURANCESCORE" select="normalize-space(INSURANCESCORE)" />
		<xsl:if test="$VAR_INSURANCESCORE = ''">
			<tr>
				<td class="midcolora">
					<li>The node for Insurancescore is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--
 		<xsl:variable name="VAR_INSURANCESCOREDIS" select="normalize-space(INSURANCESCOREDIS)"/> 
			<xsl:if test="$VAR_INSURANCESCOREDIS != 'Y' and $VAR_INSURANCESCOREDIS != 'N'">
				<tr>
					<td class="midcolora"><li>The node for insurance scored is  is blank </li> </td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		-->
		<xsl:variable name="VAR_WOODSTOVE_SURCHARGE" select="normalize-space(WOODSTOVE_SURCHARGE)" />
		<xsl:if test="$VAR_WOODSTOVE_SURCHARGE != 'Y' and $VAR_WOODSTOVE_SURCHARGE != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for woodstove surcharge is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_PRIOR_LOSS_SURCHARGE" select="normalize-space(PRIOR_LOSS_SURCHARGE)" />
		<xsl:if test="$VAR_PRIOR_LOSS_SURCHARGE != 'Y' and $VAR_PRIOR_LOSS_SURCHARGE != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for prior loss surcharge   is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_DOGSURCHARGE" select="normalize-space(DOGSURCHARGE)" />
		<xsl:if test="$VAR_DOGSURCHARGE = ''">
			<tr>
				<td class="midcolora">
					<li>The node for dog surcharge  is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_DOGFACTOR" select="normalize-space(DOGFACTOR)" />
		<xsl:if test="$VAR_DOGFACTOR != 'Y' and $VAR_DOGFACTOR != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for dog factor is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_BICYCLE_DED" select="normalize-space(SCH_BICYCLE_DED)" />
		<xsl:if test="$VAR_SCH_BICYCLE_DED = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled bicycle deductible is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_BICYCLE_AMOUNT" select="normalize-space(SCH_BICYCLE_AMOUNT)" />
		<xsl:if test="$VAR_SCH_BICYCLE_AMOUNT = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled bicycle Amount is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_CAMERA_DED" select="normalize-space(SCH_CAMERA_DED)" />
		<xsl:if test="$VAR_SCH_CAMERA_DED = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled camera deductible is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_CAMERA_AMOUNT" select="normalize-space(SCH_CAMERA_AMOUNT)" />
		<xsl:if test="$VAR_SCH_CAMERA_AMOUNT = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled camera amount is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_CELL_DED" select="normalize-space(SCH_CELL_DED)" />
		<xsl:if test="$VAR_SCH_CELL_DED = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled cell deductible is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_CELL_AMOUNT" select="normalize-space(SCH_CELL_AMOUNT)" />
		<xsl:if test="$VAR_SCH_CELL_AMOUNT = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled cell Amount is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_FURS_DED" select="normalize-space(SCH_FURS_DED)" />
		<xsl:if test="$VAR_SCH_FURS_DED = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled furs deductible is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!-- handel at proc level -->
		<!--m
		<xsl:variable name="VAR_SCH_FURS_AMOUNT" select="normalize-space(SCH_FURS_AMOUNT)"/> 
			<xsl:if test="$VAR_SCH_FURS_AMOUNT = ''">
				<tr>
					<td class="midcolora"><li>The node for scheduled fur amount is blank</li> </td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		-->
		<xsl:variable name="VAR_SCH_GUNS_DED" select="normalize-space(SCH_GUNS_DED)" />
		<xsl:if test="$VAR_SCH_GUNS_DED = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled gun deductible is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_GUNS_AMOUNT" select="normalize-space(SCH_GUNS_AMOUNT)" />
		<xsl:if test="$VAR_SCH_GUNS_AMOUNT = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled guns amount is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_GOLF_DED" select="normalize-space(SCH_GOLF_DED)" />
		<xsl:if test="$VAR_SCH_GOLF_DED = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled Golf deductible is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_GOLF_AMOUNT" select="normalize-space(SCH_GOLF_AMOUNT)" />
		<xsl:if test="$VAR_SCH_GOLF_AMOUNT = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled golf amount is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_JWELERY_DED" select="normalize-space(SCH_JWELERY_DED)" />
		<xsl:if test="$VAR_SCH_JWELERY_DED = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled jwelery deductible is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_JWELERY_AMOUNT" select="normalize-space(SCH_JWELERY_AMOUNT)" />
		<xsl:if test="$VAR_SCH_JWELERY_AMOUNT = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled jwelery amount is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_MUSICAL_DED" select="normalize-space(SCH_MUSICAL_DED)" />
		<xsl:if test="$VAR_SCH_MUSICAL_DED = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled musical deductible is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_MUSICAL_AMOUNT" select="normalize-space(SCH_MUSICAL_AMOUNT)" />
		<xsl:if test="$VAR_SCH_MUSICAL_AMOUNT = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled musical amount  is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_PERSCOMP_DED" select="normalize-space(SCH_PERSCOMP_DED)" />
		<xsl:if test="$VAR_SCH_PERSCOMP_DED = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled Personal Computer deductible is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_PERSCOMP_AMOUNT" select="normalize-space(SCH_PERSCOMP_AMOUNT)" />
		<xsl:if test="$VAR_SCH_PERSCOMP_AMOUNT = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled personal computer amount is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_SILVER_DED" select="normalize-space(SCH_SILVER_DED)" />
		<xsl:if test="$VAR_SCH_SILVER_DED = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled silver deductible is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_SILVER_AMOUNT" select="normalize-space(SCH_SILVER_AMOUNT)" />
		<xsl:if test="$VAR_SCH_SILVER_AMOUNT = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled silver amount is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_STAMPS_DED" select="normalize-space(SCH_STAMPS_DED)" />
		<xsl:if test="$VAR_SCH_STAMPS_DED = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled stamp deductible is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_STAMPS_AMOUNT" select="normalize-space(SCH_STAMPS_AMOUNT)" />
		<xsl:if test="$VAR_SCH_STAMPS_AMOUNT = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled stamps amount is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_RARECOINS_DED" select="normalize-space(SCH_RARECOINS_DED)" />
		<xsl:if test="$VAR_SCH_RARECOINS_DED = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled rare coins  deductible is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_RARECOINS_AMOUNT" select="normalize-space(SCH_RARECOINS_AMOUNT)" />
		<xsl:if test="$VAR_SCH_RARECOINS_AMOUNT = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled rare coins amount  is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_FINEARTS_WO_BREAK_DED" select="normalize-space(SCH_FINEARTS_WO_BREAK_DED)" />
		<xsl:if test="$VAR_SCH_FINEARTS_WO_BREAK_DED = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled finearts without breakable deductible is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_FINEARTS_WO_BREAK_AMOUNT" select="normalize-space(SCH_FINEARTS_WO_BREAK_AMOUNT)" />
		<xsl:if test="$VAR_SCH_FINEARTS_WO_BREAK_AMOUNT = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled finearts without breakable amount is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_FINEARTS_BREAK_DED" select="normalize-space(SCH_FINEARTS_BREAK_DED)" />
		<xsl:if test="$VAR_SCH_FINEARTS_BREAK_DED = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled breakable finearts deductible is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_SCH_FINEARTS_BREAK_AMOUNT" select="normalize-space(SCH_FINEARTS_BREAK_AMOUNT)" />
		<xsl:if test="$VAR_SCH_FINEARTS_BREAK_AMOUNT = ''">
			<tr>
				<td class="midcolora">
					<li>The node for scheduled breakable finearts amount  is blank</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_TERRITORYCODES" select="TERRITORYCODES" />
		<xsl:if test="normalize-space($VAR_TERRITORYCODES) = ''">
			<tr>
				<td class="midcolora">
					<li> The Node for Territory Code is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_COVERAGEVALUE" select="COVERAGEVALUE" />
		<xsl:if test="normalize-space($VAR_COVERAGEVALUE) = ''">
			<tr>
				<td class="midcolora">
					<li> The Node for coverage value is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_DWELLING" select="normalize-space(DWELLING)" />
		<xsl:if test="$VAR_DWELLING != 'Y' and $VAR_DWELLING != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for dwelling is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_PREMIUMGROUP" select="PREMIUMGROUP" />
		<xsl:if test="normalize-space($VAR_PREMIUMGROUP) = ''">
			<tr>
				<td class="midcolora">
					<li> The Node for premium group is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<!--
		<xsl:variable name="VAR_OTHERSTRUCTURES_LIMIT" select="OTHERSTRUCTURES_LIMIT"/> 
		<xsl:if test="normalize-space($VAR_OTHERSTRUCTURES_LIMIT) = ''">
			<tr>
				<td class="midcolora"> <li> The Node for other structures limit is blank </li></td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		-->
		<xsl:variable name="VAR_PERSONALPROPERTY_LIMIT" select="PERSONALPROPERTY_LIMIT" />
		<xsl:if test="normalize-space($VAR_PERSONALPROPERTY_LIMIT) = ''">
			<tr>
				<td class="midcolora">
					<li> The Node for personal property limit is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_AGEHOMECREDIT" select="normalize-space(AGEHOMECREDIT)" />
		<xsl:if test="$VAR_AGEHOMECREDIT != 'Y' and $VAR_AGEHOMECREDIT != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for age home credit is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_POLICYTYPE" select="POLICYTYPE" />
		<xsl:if test="normalize-space($VAR_POLICYTYPE) = ''">
			<tr>
				<td class="midcolora">
					<li> The Node for policy type is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_VALUESCUSTOMERAPP" select="normalize-space(VALUESCUSTOMERAPP)" />
		<xsl:if test="$VAR_VALUESCUSTOMERAPP != 'Y' and $VAR_VALUESCUSTOMERAPP != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for valued customer app is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_PRIORLOSSSURCHARGE" select="normalize-space(PRIORLOSSSURCHARGE)" />
		<xsl:if test="$VAR_PRIORLOSSSURCHARGE != 'Y' and $VAR_PRIORLOSSSURCHARGE != 'N'">
			<tr>
				<td class="midcolora">
					<li>The node for prior loss surcharge is blank or not in (Y/N) format</li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_TERRITORYNAME" select="TERRITORYNAME" />
		<xsl:if test="normalize-space($VAR_TERRITORYNAME) = ''">
			<tr>
				<td class="midcolora">
					<li> The Node for territory name is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_TERRITORYCOUNTY" select="TERRITORYCOUNTY" />
		<xsl:if test="normalize-space($VAR_TERRITORYCOUNTY) = ''">
			<tr>
				<td class="midcolora">
					<li> The Node for territory county is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		<xsl:variable name="VAR_BREEDOFDOG" select="BREEDOFDOG" />
		<xsl:if test="normalize-space($VAR_BREEDOFDOG) = ''">
			<tr>
				<td class="midcolora">
					<li> The Node for breed of dog is blank </li>
				</td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
	</xsl:template>
	<!-- &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&  -->
	<!-- &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&  END  OF MAIN_FOR_INPUT_XML  &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&  -->
	<!-- &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&  -->
</xsl:stylesheet>
