<!-- ==================================================================================================
File Name			:	UWRulesInputMsg.xsl
Purpose				:	To display Messages for Rental Dwelling 
Name				:	Ashwani
Date				:	27 Aug.2005  
======================================================================================================== -->
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
		int  intIsValid = 1;
		public int AssignUnderwritter(int assignvalue)
		{
			intIsValid = intIsValid*assignvalue;
			return intIsValid;
		}		
]]></msxsl:script>
	<xsl:template match="/">
		<html>
			<head>
				<xsl:variable name="myName" select="INPUTXML/CSSNUM/@CSSVALUE"></xsl:variable>
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
			<!-- ==================================== Clinet Top ==================================================== -->
			<table border="2" align="center" width='90%'>
				<tr>
					<td class="pageheader" width="18%">Customer Name:</td>
					<td class="midcolora" width="36%">
						<xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/CUSTOMER_NAME" />
					</td>
					<td class="pageheader" width="18%">Customer Type:</td>
					<td class="midcolora" width="36%">
						<xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/CUSTOMER_TYPE" />
					</td>
				</tr>
				<tr>
					<td class="pageheader">Address:</td>
					<td class="midcolora">
						<xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/ADDRESS" />
					</td>
					<td class="pageheader">Phone:</td>
					<td class="midcolora">
						<xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/CUSTOMER_HOME_PHONE" />
					</td>
				</tr>
				<tr>
					<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=0">
						<td class="pageheader">App. No :</td>
					</xsl:if>
					<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=1">
						<td class="pageheader">Policy No :</td>
					</xsl:if>
					<td class="midcolora">
						<xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/APP_NO" />
					</td>
					<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=0">
						<td class="pageheader">App. Version :</td>
						<td class="midcolora">
							<xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/APP_VERSION_NO" />
						</td>
					</xsl:if>
					<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=1">
						<td class="pageheader">Policy Version :</td>
						<td class="midcolora">
							<xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/POLICY_DISP_VERSION" />
						</td>
					</xsl:if>
				</tr>
			</table>
			<table border="1" align="center" width='100%'>
				<tr>
					<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=0">
						<td class="headereffectcenter">Application verification status</td>
					</xsl:if>
					<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=1">
						<td class="headereffectcenter">Policy verification status</td>
					</xsl:if>
				</tr>
				<tr>
					<td class="pageheader">Please complete the following information:</td>
				</tr>
				<!-- Call for different Messages -->
				<xsl:choose>
					<!-- ================== when there is no location  and dwelling ===================== -->
					<xsl:when test="INPUTXML/LOCATIONS/ERRORS/ERROR/@ERRFOUND='T' and INPUTXML/DWELLINGS/ERRORS/ERROR/@ERRFOUND='T'">
						<!-- =============== call a template shows all the messages ======== -->
						<xsl:if test="user:AssignUnderwritter(0) = 0" />
						<xsl:call-template name="RDDETAIL" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:call-template name="SHOWRDINPUTDETAIL" />
					</xsl:otherwise>
				</xsl:choose>
			</table>
		</html>
	</xsl:template>
	<!-- ===============This template shows the input detail (rules + mandatory) of HO ================= -->
	<xsl:template name="SHOWRDINPUTDETAIL">
		<xsl:call-template name="INSURANCESCORE"></xsl:call-template>
		<xsl:apply-templates select="INPUTXML/APPLICATIONS/APPLICATION"></xsl:apply-templates>
		<xsl:apply-templates select="INPUTXML/LOCATIONS/LOCATION"></xsl:apply-templates>
		<!-- ============================== Dwelling Infos =========================================== -->
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/@COUNT > 0">
				<xsl:for-each select="INPUTXML/DWELLINGS/DWELLINGINFO">
					<xsl:if test="DWELLING/DWELLING_NUMBER='' or DWELLING/LOCATION_ID='' or DWELLING/YEAR_BUILT='' 
							or DWELLING/REPLACEMENT_COST='' or DWELLING/OCCUPANCY='' or DWELLING/BUILDING_TYPE='' or DWELLING/MARKET_VALUE='' or DWELLING/DP2_RC_MINVALUE='' 
							or DWELLING/DP3_RC_MINVALUE='' or DWELLING/MARKET_VS_REPCOST='' or 
							RATINGINFO/NO_OF_FAMILIES='' or RATINGINFO/HYDRANT_DIST='' or RATINGINFO/FIRE_STATION_DIST='' or RATINGINFO/IS_UNDER_CONSTRUCTION=''
							or RATINGINFO/PROT_CLASS=''  or RATINGINFO/WIRING_UPDATE_YEAR='' or RATINGINFO/PLUMBING_UPDATE_YEAR='' or RATINGINFO/UNDER_CONSTRUCTION_POLICY_TERM=''
							or RATINGINFO/HEATING_UPDATE_YEAR='' or RATINGINFO/ROOFING_UPDATE_YEAR='' or RATINGINFO/NO_OF_AMPS='' or
							RATINGINFO/CIRCUIT_BREAKERS='' or  RATINGINFO/EXTERIOR_CONSTRUCTION='' or  RATINGINFO/EXTERIOR_OTHER_DESC=''
							or COVERAGE/DWELLING_LIMIT='' or COVERAGE/MIN_COVA_REPCOST='' or COVERAGE/MAXVAL_REPLACEMENT_COST='' 
							or COVERAGE/MAX_COV_REPCOST_RC='' or COVERAGE/MIN_COV_REPCOST_RC='' or COVERAGE/MIN_COVA_DP3RC='' or COVERAGE/COVA_REPCOST_DP3P='' or RATINGINFO/ROOFTYPE=''">
						<tr>
							<td>
								<table width="60%">
									<tr>
										<xsl:if test="DWELLING/DWELLING_NUMBER != ''">
											<td class="pageheader" width="18%">Customer Dwelling:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="DWELLING/DWELLING_NUMBER" />
											</td>
										</xsl:if>
										<xsl:if test="DWELLING/YEAR_BUILT != ''">
											<td class="pageheader" width="18%">Year Built:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="DWELLING/YEAR_BUILT" />
											</td>
										</xsl:if>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:apply-templates select="DWELLING" />
						<xsl:apply-templates select="RATINGINFO" />
						<xsl:apply-templates select="COVERAGE" />
					</xsl:if>
				</xsl:for-each>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0" />
				<!-- Dwelling Info -->
				<xsl:call-template name="DWELLINGINFOMESSAGE" />
				<!-- Dwelling Rating Info -->
				<xsl:call-template name="DWELLINGRATINGINFOMESSAGE" />
				<!-- Coverage/Limt  -->
				<xsl:call-template name="COVERAGELIMITSMESSAGE" />
			</xsl:otherwise>
		</xsl:choose>
		<!-- Gen Info  -->
		<xsl:choose>
			<xsl:when test="INPUTXML/RDGENINFOS/RDGENINFO/IS_RECORD_EXISTS='N'">
				<xsl:apply-templates select="INPUTXML/RDGENINFOS/RDGENINFO" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
				<xsl:call-template name="GENINFOMESSAGE" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ======================================= Customer Details ==================================== -->
	<xsl:template name="INSURANCESCORE">
		<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CUSTOMER_INSURANCE_SCORE=''">
			<tr>
				<td>
					<table width="60%">
						<tr>
							<td class="pageheader" width="18%">Customer Name:</td>
							<td class="midcolora" width="36%">
								<xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/CUSTOMER_NAME" />
							</td>
							<td class="pageheader">Address:</td>
							<td class="midcolora">
								<xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/ADDRESS" />
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td class="pageheader">For Customer Details:</td>
			</tr>
			<tr>
				<td class="midcolora">Please insert Insurance Score.</td>
			</tr>
		</xsl:if>
	</xsl:template>
	<!-- ============================== Application Detail ================================= -->
	<xsl:template match="INPUTXML/APPLICATIONS/APPLICATION">
		<xsl:choose>
			<xsl:when test="STATE_ID=-1 or STATE_ID='' or  APP_LOB='' or APP_TERMS='' or APP_EFFECTIVE_DATE=''  or
			APP_EXPIRATION_DATE='' or APP_AGENCY_ID=-1 or APP_AGENCY_ID='' or BILL_TYPE='' or PROXY_SIGN_OBTAINED=-1 or PROXY_SIGN_OBTAINED=''
				 or CHARGE_OFF_PRMIUM='' or IS_PRIMARY_APPLICANT='' or IS_PRIMARY_APPLICANT='Y' or DOWN_PAY_MENT='' or POL_UNDERWRITER='' or PICH_OF_LOC='' or PROPERTY_INSP_CREDIT=''">
				<tr>
					<xsl:choose>
						<xsl:when test="CALLED_FROM=1">
							<td class="pageheader">For Policy information:</td>
						</xsl:when>
						<xsl:otherwise>
							<td class="pageheader">For Application information:</td>
						</xsl:otherwise>
					</xsl:choose>
				</tr>
				<xsl:call-template name="STATE_ID" />
				<xsl:call-template name="APP_LOB" />
				<xsl:call-template name="APP_TERMS" />
				<xsl:call-template name="APP_EFFECTIVE_DATE" />
				<xsl:call-template name="APP_EXPIRATION_DATE" />
				<xsl:call-template name="APP_AGENCY_ID" />
				<xsl:call-template name="BILL_TYPE" />
				<xsl:call-template name="DOWN_PAY_MENT" />
				<xsl:call-template name="PROXY_SIGN_OBTAINED" />
				<xsl:call-template name="IS_PRIMARY_APPLICANT" />
				<xsl:call-template name="CHARGE_OFF_PRMIUM" />
				<xsl:call-template name="POL_UNDERWRITER" />
				<xsl:call-template name="PICH_OF_LOC" />
				<xsl:call-template name="PROPERTY_INSP_CREDIT" />				
				<xsl:if test="user:AssignUnderwritter(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<tr>
					<td>
						<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
					</td>
				</tr>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================== Location Detail ================================= -->
	<xsl:template match="INPUTXML/LOCATIONS/LOCATION">
		<xsl:choose>
			<xsl:when test="LOC_NUM='' or LOC_ADD1='' or LOC_CITY='' or LOC_STATE='' or LOC_TYPE='' or LOC_ZIP=''">
				<tr>
					<td class="pageheader">For Location Information: </td>
					<tr>
						<td>
							<table width="60%">
								<tr>
									<xsl:if test="LOC_NUM != ''">
										<td class="pageheader" width="18%">Location Number:</td>
										<td class="midcolora" width="36%">
											<xsl:value-of select="LOC_NUM" />
										</td>
									</xsl:if>
								</tr>
							</table>
						</td>
					</tr>
				</tr>
				<xsl:call-template name="LOC_NUM" />
				<xsl:call-template name="LOC_ADD1" />
				<xsl:call-template name="LOC_CITY" />
				<xsl:call-template name="LOC_STATE" />
				<xsl:call-template name="LOC_ZIP" />
				<xsl:call-template name="LOC_TYPE" />
				<xsl:if test="user:AssignUnderwritter(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<tr>
					<td>
						<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
					</td>
				</tr>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ========================================== Dwelling Info ================================= -->
	<xsl:template match="DWELLING">
		<xsl:choose>
			<xsl:when test="DWELLING_NUMBER='' or LOCATION_ID='' or YEAR_BUILT='' or REPLACEMENT_COST='' or BUILDING_TYPE='' or OCCUPANCY=''
				or MARKET_VALUE='' or DP2_RC_MINVALUE='' or DP3_RC_MINVALUE='' or MARKET_VS_REPCOST=''">
				<tr>
					<td class="pageheader">For Dwelling Information: </td>
				</tr>
				<xsl:call-template name="DWELLING_NUMBER" />
				<xsl:call-template name="LOCATION_ID" />
				<xsl:call-template name="YEAR_BUILT" />
				<xsl:call-template name="REPLACEMENT_COST" />
				<xsl:call-template name="OCCUPANCY" />
				<xsl:call-template name="MARKET_VALUE" />
				<xsl:call-template name="MARKET_VS_REPCOST" />
				<xsl:call-template name="DP2_RC_MINVALUE" />
				<xsl:call-template name="DP3_RC_MINVALUE" />
				<xsl:call-template name="BUILDING_TYPE" />
				<xsl:if test="user:AssignUnderwritter(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<tr>
					<td>
						<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
					</td>
				</tr>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ========================================== Rating Info ================================= -->
	<xsl:template match="RATINGINFO">
		<xsl:choose>
			<xsl:when test="IS_RECORD_EXIST='Y'">
				<xsl:choose>
					<xsl:when test="HYDRANT_DIST='' or FIRE_STATION_DIST='' or IS_UNDER_CONSTRUCTION='' or PROT_CLASS=''
			 or WIRING_UPDATE_YEAR='' or PLUMBING_UPDATE_YEAR='' or HEATING_UPDATE_YEAR='' or ROOFING_UPDATE_YEAR='' or 
			 NO_OF_AMPS='' or  CIRCUIT_BREAKERS='' or EXTERIOR_CONSTRUCTION='' or EXTERIOR_OTHER_DESC='' or NO_OF_FAMILIES='' or UNDER_CONSTRUCTION_POLICY_TERM='' or ROOFTYPE=''">
						<tr>
							<td class="pageheader">For Rating Information: </td>
						</tr>
						<xsl:call-template name="HYDRANT_DIST" />
						<xsl:call-template name="FIRE_STATION_DIST" />
						<xsl:call-template name="IS_UNDER_CONSTRUCTION" />
						<xsl:call-template name="PROT_CLASS" />
						<xsl:call-template name="WIRING_UPDATE_YEAR" />
						<xsl:call-template name="PLUMBING_UPDATE_YEAR" />
						<xsl:call-template name="HEATING_UPDATE_YEAR" />
						<xsl:call-template name="ROOFING_UPDATE_YEAR" />
						<xsl:call-template name="NO_OF_AMPS" />
						<xsl:call-template name="CIRCUIT_BREAKERS" />
						<xsl:call-template name="NO_OF_FAMILIES" />
						<xsl:call-template name="ROOFTYPE" />
						<xsl:call-template name="EXTERIOR_CONSTRUCTION" />
						<xsl:call-template name="EXTERIOR_OTHER_DESC" />
						<xsl:call-template name="UNDER_CONSTRUCTION_POLICY_TERM" />
						<!-- xsl:call-template name="UNDER_CONSTRUCTION_DATE" 
						xsl:call-template name="UNDER_CONSTRUCTION_POLICYTERM" 
						-->
						<xsl:if test="user:AssignUnderwritter(0) = 0"></xsl:if>
					</xsl:when>
					<xsl:otherwise>
						<tr>
							<td>
								<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
							</td>
						</tr>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				<tr>
					<td>
						<xsl:call-template name="DWELLINGRATINGINFOMESSAGE" />
					</td>
				</tr>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ======================================= Coverage info ================================= -->
	<xsl:template match="COVERAGE">
		<xsl:choose>
			<xsl:when test="DWELLING_LIMIT='' or MIN_COVA_REPCOST='' or MAXVAL_REPLACEMENT_COST='' or 
			 MAX_COV_REPCOST_RC='' or MIN_COV_REPCOST_RC='' or MIN_COVA_DP3RC='' or COVA_REPCOST_DP3P=''">
				<tr>
					<td class="pageheader">For SectionI/SectionII Information:</td>
				</tr>
				<xsl:call-template name="DWELLING_LIMIT" />
				<xsl:call-template name="MIN_COVA_REPCOST" />
				<xsl:call-template name="MAXVAL_REPLACEMENT_COST" />
				<xsl:call-template name="MAX_COV_REPCOST_RC" />
				<xsl:call-template name="MIN_COV_REPCOST_RC" />
				<xsl:call-template name="MIN_COVA_DP3RC" />
				<xsl:call-template name="COVA_REPCOST_DP3P" />
				<xsl:if test="user:AssignUnderwritter(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<tr>
					<td>
						<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
					</td>
				</tr>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template match="INPUTXML/RDGENINFOS/RDGENINFO">
		<xsl:choose>
			<xsl:when test=" IS_VACENT_OCCUPY='' or DESC_VACENT_OCCUPY='' or IS_RENTED_IN_PART='' or DESC_RENTED_IN_PART=''
			    or  IS_DWELLING_OWNED_BY_OTHER='' or DESC_DWELLING_OWNED_BY_OTHER='' or ANY_FARMING_BUSINESS_COND='' or 
				IS_PROP_NEXT_COMMERICAL='' or DESC_PROPERTY='' or ARE_STAIRWAYS_PRESENT='' or DESC_STAIRWAYS='' or 
				ANIMALS_EXO_PETS_HISTORY='' or BREED='' or NO_OF_PETS='' or OTHER_DESCRIPTION='' or IS_SWIMPOLL_HOTTUB='' 
				or HAS_INSU_TRANSFERED_AGENCY='' or DESC_INSU_TRANSFERED_AGENCY='' or IS_OWNERS_DWELLING_CHANGED='' or 
				DESC_OWNER='' or ANY_COV_DECLINED_CANCELED='' or  DESC_COV_DECLINED_CANCELED='' or CONVICTION_DEGREE_IN_PAST='' or 
				DESC_CONVICTION_DEGREE_IN_PAST='' or LEAD_PAINT_HAZARD='' or DESC_LEAD_PAINT_HAZARD='' or  ANY_RESIDENCE_EMPLOYEE='' or DESC_RESIDENCE_EMPLOYEE='' or ANY_OTHER_RESI_OWNED='' or DESC_RESIDENCE_EMPLOYEE='' or 
				ANY_OTH_INSU_COMP='' or DESC_OTHER_INSURANCE='' or ANY_RENOVATION='' or DESC_RENOVATION='' or TRAMPOLINE='' or 
				DESC_TRAMPOLINE='' or RENTERS='' or DESC_RENTERS='' or ANY_HEATING_SOURCE='' or DESC_ANY_HEATING_SOURCE='' or 
				BUILD_UNDER_CON_GEN_CONT='' or NON_SMOKER_CREDIT='' or SWIMMING_POOL='' or MULTI_POLICY_DISC_APPLIED='' or DESC_MULTI_POLICY_DISC_APPLIED='' or
				PROPERTY_USED_WHOLE_PART='' or PROPERTY_USED_WHOLE_PART_DESC='' or DWELLING_MOBILE_HOME='' or DWELLING_MOBILE_HOME_DESC='' or PROPERTY_ON_MORE_THAN='' or PROPERTY_ON_MORE_THAN_DESC='' 
				or MODULAR_MANUFACTURED_HOME='' or VALUED_CUSTOMER_DISCOUNT_OVERRIDE='' or VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC='' or ANY_PRIOR_LOSSES='' or ANY_PRIOR_LOSSES_DESC=''">
				<tr>
					<td class="pageheader">For Underwriting Questions:</td>
				</tr>
				<xsl:call-template name="IS_VACENT_OCCUPY" />
				<xsl:call-template name="DESC_VACENT_OCCUPY" />
				<xsl:call-template name="IS_RENTED_IN_PART" />
				<xsl:call-template name="DESC_RENTED_IN_PART" />
				<xsl:call-template name="IS_DWELLING_OWNED_BY_OTHER" />
				<xsl:call-template name="DESC_DWELLING_OWNED_BY_OTHER" />
				<xsl:call-template name="ANY_FARMING_BUSINESS_COND" />
				<xsl:call-template name="IS_PROP_NEXT_COMMERICAL" />
				<xsl:call-template name="DESC_PROPERTY" />
				<xsl:call-template name="ARE_STAIRWAYS_PRESENT" />
				<xsl:call-template name="DESC_STAIRWAYS" />
				<xsl:call-template name="ANIMALS_EXO_PETS_HISTORY" />
				<xsl:call-template name="BREED" />
				<xsl:call-template name="NO_OF_PETS" />
				<xsl:call-template name="OTHER_DESCRIPTION" />
				<xsl:call-template name="IS_SWIMPOLL_HOTTUB" />
				<xsl:call-template name="HAS_INSU_TRANSFERED_AGENCY" />
				<xsl:call-template name="DESC_INSU_TRANSFERED_AGENCY" />
				<xsl:call-template name="IS_OWNERS_DWELLING_CHANGED" />
				<xsl:call-template name="DESC_OWNER" />
				<xsl:call-template name="ANY_COV_DECLINED_CANCELED" />
				<xsl:call-template name="DESC_COV_DECLINED_CANCELED" />
				<xsl:call-template name="CONVICTION_DEGREE_IN_PAST" />
				<xsl:call-template name="DESC_CONVICTION_DEGREE_IN_PAST" />
				<xsl:call-template name="LEAD_PAINT_HAZARD" />
				<xsl:call-template name="DESC_LEAD_PAINT_HAZARD" />
				<xsl:call-template name="ANY_RESIDENCE_EMPLOYEE" />
				<xsl:call-template name="DESC_RESIDENCE_EMPLOYEE" />
				<xsl:call-template name="ANY_OTHER_RESI_OWNED" />
				<xsl:call-template name="DESC_RESIDENCE_EMPLOYEE" />
				<xsl:call-template name="ANY_OTH_INSU_COMP" />
				<xsl:call-template name="DESC_OTHER_INSURANCE" />
				<xsl:call-template name="ANY_RENOVATION" />
				<xsl:call-template name="DESC_RENOVATION" />
				<xsl:call-template name="TRAMPOLINE" />
				<xsl:call-template name="DESC_TRAMPOLINE" />
				<xsl:call-template name="RENTERS" />
				<xsl:call-template name="DESC_RENTERS" />
				<xsl:call-template name="ANY_HEATING_SOURCE" />
				<xsl:call-template name="DESC_ANY_HEATING_SOURCE" />
				<xsl:call-template name="BUILD_UNDER_CON_GEN_CONT" />
				<xsl:call-template name="NON_SMOKER_CREDIT" />
				<xsl:call-template name="SWIMMING_POOL" />
				<xsl:call-template name="MULTI_POLICY_DISC_APPLIED" />
				<xsl:call-template name="DESC_MULTI_POLICY_DISC_APPLIED" />
				<xsl:call-template name="PROPERTY_USED_WHOLE_PART" />
				<xsl:call-template name="PROPERTY_USED_WHOLE_PART_DESC" />
				<xsl:call-template name="DWELLING_MOBILE_HOME" />
				<xsl:call-template name="DWELLING_MOBILE_HOME_DESC" />
				<xsl:call-template name="PROPERTY_ON_MORE_THAN" />
				<xsl:call-template name="PROPERTY_ON_MORE_THAN_DESC" />
				<xsl:call-template name="MODULAR_MANUFACTURED_HOME" />
				<xsl:call-template name="VALUED_CUSTOMER_DISCOUNT_OVERRIDE" />
				<xsl:call-template name="VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC" />
				<xsl:call-template name="ANY_PRIOR_LOSSES" />
				<xsl:call-template name="ANY_PRIOR_LOSSES_DESC" />
				<xsl:if test="user:AssignUnderwritter(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<tr>
					<td>
						<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
					</td>
				</tr>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--      **********************TEMPLATES*********************  -->
	<xsl:template name="SHOWASSIGNMESSAGE">
		<xsl:choose>
			<xsl:when test="user:AssignUnderwritter(1)=1">
				<tr>
					<td class="midcolora">
						<span class="labelfont">Please assign application to underwriter.</span>
					</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- ============================== Gen Info ===================================================== -->
	<xsl:template name="IS_VACENT_OCCUPY">
		<xsl:choose>
			<xsl:when test="IS_VACENT_OCCUPY=''">
				<tr>
					<td class="midcolora">Please select Is dwelling currently Vacant or Unoccupied?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_VACENT_OCCUPY">
		<xsl:choose>
			<xsl:when test="DESC_VACENT_OCCUPY=''">
				<tr>
					<td class="midcolora">Please insert Description of Is dwelling currently Vacant or Unoccupied?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RENTED_IN_PART">
		<xsl:choose>
			<xsl:when test="IS_RENTED_IN_PART=''">
				<tr>
					<td class="midcolora">Please select Is dwelling rented in whole or part to students under age 30?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_RENTED_IN_PART">
		<xsl:choose>
			<xsl:when test="DESC_RENTED_IN_PART=''">
				<tr>
					<td class="midcolora">Please insert Description of Is dwelling rented in whole or part to students under age 30?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_DWELLING_OWNED_BY_OTHER">
		<xsl:choose>
			<xsl:when test="IS_DWELLING_OWNED_BY_OTHER=''">
				<tr>
					<td class="midcolora">Please select Is dwelling owned by anyone other than an individual?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_DWELLING_OWNED_BY_OTHER">
		<xsl:choose>
			<xsl:when test="DESC_DWELLING_OWNED_BY_OTHER=''">
				<tr>
					<td class="midcolora">Please insert Description of Is dwelling owned by anyone other than an individual?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ANY_FARMING_BUSINESS_COND">
		<xsl:choose>
			<xsl:when test="ANY_FARMING_BUSINESS_COND=''">
				<tr>
					<td class="midcolora">Please select Any business conducted on Premises?.</td>
					
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_PROP_NEXT_COMMERICAL">
		<xsl:choose>
			<xsl:when test="IS_PROP_NEXT_COMMERICAL=''">
				<tr>
					<td class="midcolora">Please select Is property located next to a commercial building?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_PROPERTY">
		<xsl:choose>
			<xsl:when test="DESC_PROPERTY=''">
				<tr>
					<td class="midcolora">Please insert Description of Is property located next to a commercial building?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ARE_STAIRWAYS_PRESENT">
		<xsl:choose>
			<xsl:when test="ARE_STAIRWAYS_PRESENT=''">
				<tr>
					<td class="midcolora">Please select Are any outside stairways present?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_STAIRWAYS">
		<xsl:choose>
			<xsl:when test="DESC_STAIRWAYS=''">
				<tr>
					<td class="midcolora">Please insert Description of Are any outside stairways present?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ANIMALS_EXO_PETS_HISTORY">
		<xsl:choose>
			<xsl:when test="ANIMALS_EXO_PETS_HISTORY=''">
				<tr>
					<td class="midcolora">Please select Any Animals, Dogs, Horses, Livestock or pets?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="BREED">
		<xsl:choose>
			<xsl:when test="BREED=''">
				<tr>
					<td class="midcolora">Please insert Description of Any animal,livestock or pets?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="NO_OF_PETS">
		<xsl:choose>
			<xsl:when test="NO_OF_PETS=''">
				<tr>
					<td class="midcolora">Please select Number and breed of pets.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="OTHER_DESCRIPTION">
		<xsl:choose>
			<xsl:when test="OTHER_DESCRIPTION=''">
				<tr>
					<td class="midcolora">Please insert Other Description of Number and breed of pets.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_SWIMPOLL_HOTTUB">
		<xsl:choose>
			<xsl:when test="IS_SWIMPOLL_HOTTUB=''">
				<tr>
					<td class="midcolora">Please select Is there an enclosed swimming pool or hot tub (not including bathroom type Jacuzzi or hot tub)?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HAS_INSU_TRANSFERED_AGENCY">
		<xsl:choose>
			<xsl:when test="HAS_INSU_TRANSFERED_AGENCY=''">
				<tr>
					<td class="midcolora">Please select Has insurance been transferred within the agency?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_INSU_TRANSFERED_AGENCY">
		<xsl:choose>
			<xsl:when test="DESC_INSU_TRANSFERED_AGENCY=''">
				<tr>
					<td class="midcolora">Please insert Other Description of Has insurance been transferred within the agency?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_OWNERS_DWELLING_CHANGED">
		<xsl:choose>
			<xsl:when test="IS_OWNERS_DWELLING_CHANGED=''">
				<tr>
					<td class="midcolora">Please select Has ownership of this dwelling changed more than one time in the last 3 years?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_OWNER">
		<xsl:choose>
			<xsl:when test="DESC_OWNER=''">
				<tr>
					<td class="midcolora">Please insert Other Description of Has ownership of this dwelling changed more than one time in the last 3 years?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ANY_COV_DECLINED_CANCELED">
		<xsl:choose>
			<xsl:when test="ANY_COV_DECLINED_CANCELED=''">
				<tr>
					<td class="midcolora">Please select Any coverage declined, cancelled or non-renewed during the last 3 years?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_COV_DECLINED_CANCELED">
		<xsl:choose>
			<xsl:when test="DESC_COV_DECLINED_CANCELED=''">
				<tr>
					<td class="midcolora">Please insert Other Description of Any coverage declined, cancelled or non-renewed during the last 3 years?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="CONVICTION_DEGREE_IN_PAST">
		<xsl:choose>
			<xsl:when test="CONVICTION_DEGREE_IN_PAST=''">
				<tr>
					<td class="midcolora">Please select Conviction of any degree of arson in past 3 years?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_CONVICTION_DEGREE_IN_PAST">
		<xsl:choose>
			<xsl:when test="DESC_CONVICTION_DEGREE_IN_PAST=''">
				<tr>
					<td class="midcolora">Please insert Description of conviction.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="LEAD_PAINT_HAZARD">
		<xsl:choose>
			<xsl:when test="LEAD_PAINT_HAZARD=''">
				<tr>
					<td class="midcolora">Please select Any lead paint hazard?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_LEAD_PAINT_HAZARD">
		<xsl:choose>
			<xsl:when test="DESC_LEAD_PAINT_HAZARD=''">
				<tr>
					<td class="midcolora">Please insert Description of lead paint hazard.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ANY_RESIDENCE_EMPLOYEE">
		<xsl:choose>
			<xsl:when test="ANY_RESIDENCE_EMPLOYEE=''">
				<tr>
					<td class="midcolora">Please select Any residence employees?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_RESIDENCE_EMPLOYEE">
		<xsl:choose>
			<xsl:when test="DESC_RESIDENCE_EMPLOYEE=''">
				<tr>
					<td class="midcolora">Please insert Number of resident employee.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ANY_OTHER_RESI_OWNED">
		<xsl:choose>
			<xsl:when test="ANY_OTHER_RESI_OWNED=''">
				<tr>
					<td class="midcolora">Please select Any other residence owned, occupied or rented?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_OTHER_RESIDENCE">
		<xsl:choose>
			<xsl:when test="DESC_OTHER_RESIDENCE=''">
				<tr>
					<td class="midcolora">Please insert Number of resident employee.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ANY_OTH_INSU_COMP">
		<xsl:choose>
			<xsl:when test="ANY_OTH_INSU_COMP=''">
				<tr>
					<td class="midcolora">Please select Any other insurance with this company?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_OTHER_INSURANCE">
		<xsl:choose>
			<xsl:when test="DESC_OTHER_INSURANCE=''">
				<tr>
					<td class="midcolora">Please insert Description other insurance(List policy).</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ANY_RENOVATION">
		<xsl:choose>
			<xsl:when test="ANY_RENOVATION=''">
				<tr>
					<td class="midcolora">Please select Any current renovation/remodeling.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_RENOVATION">
		<xsl:choose>
			<xsl:when test="DESC_RENOVATION=''">
				<tr>
					<td class="midcolora">Please insert Est. completion date and value, if undergoing renovation.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="TRAMPOLINE">
		<xsl:choose>
			<xsl:when test="TRAMPOLINE=''">
				<tr>
					<td class="midcolora">Please select Trampoline.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_TRAMPOLINE">
		<xsl:choose>
			<xsl:when test="DESC_TRAMPOLINE=''">
				<tr>
					<td class="midcolora">Please insert Description of trampoline.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="RENTERS">
		<xsl:choose>
			<xsl:when test="RENTERS=''">
				<tr>
					<td class="midcolora">Please select Roomers or Boaders.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_RENTERS">
		<xsl:choose>
			<xsl:when test="DESC_RENTERS=''">
				<tr>
					<td class="midcolora">Please insert If renters, then give description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ANY_HEATING_SOURCE">
		<xsl:choose>
			<xsl:when test="ANY_HEATING_SOURCE=''">
				<tr>
					<td class="midcolora">Please select Any supplemental heating source(wood stove,fireplace inserts,etc.)?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ANY_PRIOR_LOSSES">
		<xsl:choose>
			<xsl:when test="ANY_PRIOR_LOSSES=''">
				<tr>
					<td class="midcolora">Please select Any Prior Losses.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ANY_PRIOR_LOSSES_DESC">
		<xsl:choose>
			<xsl:when test="ANY_PRIOR_LOSSES_DESC=''">
				<tr>
					<td class="midcolora">Please enter Prior Losses Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_ANY_HEATING_SOURCE">
		<xsl:choose>
			<xsl:when test="DESC_ANY_HEATING_SOURCE=''">
				<tr>
					<td class="midcolora">Please insert Wood stove supplement.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="BUILD_UNDER_CON_GEN_CONT">
		<xsl:choose>
			<xsl:when test="BUILD_UNDER_CON_GEN_CONT=''">
				<tr>
					<td class="midcolora">Please select If Building is Under Construction, is the Applicant the General Contractor?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="NON_SMOKER_CREDIT">
		<xsl:choose>
			<xsl:when test="NON_SMOKER_CREDIT=''">
				<tr>
					<td class="midcolora">Please select Non smoker credit.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SWIMMING_POOL">
		<xsl:choose>
			<xsl:when test="SWIMMING_POOL=''">
				<tr>
					<td class="midcolora">Please select Swimming Pool.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MULTI_POLICY_DISC_APPLIED">
		<xsl:choose>
			<xsl:when test="MULTI_POLICY_DISC_APPLIED=''">
				<tr>
					<td class="midcolora">Please select Is multipolicy discount applied?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_MULTI_POLICY_DISC_APPLIED">
		<xsl:choose>
			<xsl:when test="DESC_MULTI_POLICY_DISC_APPLIED=''">
				<tr>
					<td class="midcolora">Please insert Multipolicy discount description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PROPERTY_USED_WHOLE_PART">
		<xsl:choose>
			<xsl:when test="PROPERTY_USED_WHOLE_PART=''">
				<tr>
					<td class="midcolora">Please select Is Property Used in whole or in part for farming, commercial, industrial, professional or business purposes except if use is incidental?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PROPERTY_USED_WHOLE_PART_DESC">
		<xsl:choose>
			<xsl:when test="PROPERTY_USED_WHOLE_PART_DESC=''">
				<tr>
					<td class="midcolora">Please insert Description of Property Used in whole or in part for farming, commercial, industrial, professional or business purposes except if use is incidental.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DWELLING_MOBILE_HOME">
		<xsl:choose>
			<xsl:when test="DWELLING_MOBILE_HOME=''">
				<tr>
					<td class="midcolora">Please select Is the dwelling a mobile home or double wide?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DWELLING_MOBILE_HOME_DESC">
		<xsl:choose>
			<xsl:when test="DWELLING_MOBILE_HOME_DESC=''">
				<tr>
					<td class="midcolora">Please insert Description of Is the dwelling a mobile home or double wide.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PROPERTY_ON_MORE_THAN">
		<xsl:choose>
			<xsl:when test="PROPERTY_ON_MORE_THAN=''">
				<tr>
					<td class="midcolora">Please select Is Property situated on more than 5 Acres?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PROPERTY_ON_MORE_THAN_DESC">
		<xsl:choose>
			<xsl:when test="PROPERTY_ON_MORE_THAN_DESC=''">
				<tr>
					<td class="midcolora">Please insert Description of Land Use.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MODULAR_MANUFACTURED_HOME">
		<xsl:choose>
			<xsl:when test="MODULAR_MANUFACTURED_HOME=''">
				<tr>
					<td class="midcolora">Please select Is this a Modular/Manufactured Home?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="VALUED_CUSTOMER_DISCOUNT_OVERRIDE">
		<xsl:choose>
			<xsl:when test="VALUED_CUSTOMER_DISCOUNT_OVERRIDE=''">
				<tr>
					<td class="midcolora">Please select Valued Customer Discount Override.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC">
		<xsl:choose>
			<xsl:when test="VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC=''">
				<tr>
					<td class="midcolora">Please insert Valued Customer Discount Override Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- ====================================Application info ========================================== -->
	<xsl:template name="CHARGE_OFF_PRMIUM">
		<xsl:choose>
			<xsl:when test="CHARGE_OFF_PRMIUM='' or CHARGE_OFF_PRMIUM='NULL'">
				<tr>
					<td class="midcolora">Please select Charge Off Premium.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--24 Dec 2007-->
	<xsl:template name="PICH_OF_LOC">
		<xsl:choose>
			<xsl:when test="PICH_OF_LOC=''">
				<tr>
					<td class="midcolora">Please select Picture of Location attached?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PROPERTY_INSP_CREDIT">
		<xsl:choose>
			<xsl:when test="PROPERTY_INSP_CREDIT=''">
				<tr>
					<td class="midcolora">Please select Property Inspection/Cost Estimator.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- You must have at least one primary applicant.Please select Primary Applicant in CO Applicant Detail. -->
	<xsl:template name="IS_PRIMARY_APPLICANT">
		<xsl:choose>
			<xsl:when test="IS_PRIMARY_APPLICANT='' or IS_PRIMARY_APPLICANT='Y'">
				<tr>
					<td class="midcolora">You must have at least one primary applicant.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DOWN_PAY_MENT">
		<xsl:choose>
			<xsl:when test="DOWN_PAY_MENT=''">
				<tr>
					<td class="midcolora">Please select Down Payment.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="POL_UNDERWRITER">
		<xsl:choose>
			<xsl:when test="POL_UNDERWRITER=''">
				<tr>
					<td class="midcolora">Please select Underwriter.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PROXY_SIGN_OBTAINED">
		<xsl:choose>
			<xsl:when test="PROXY_SIGN_OBTAINED='-1' or PROXY_SIGN_OBTAINED=''">
				<tr>
					<td class="midcolora">Please select Proxy Signature Obtained?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="BILL_TYPE">
		<xsl:choose>
			<xsl:when test="BILL_TYPE=''">
				<tr>
					<td class="midcolora">Please select Bill Type.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="APP_AGENCY_ID">
		<xsl:choose>
			<xsl:when test="APP_AGENCY_ID=-1 or APP_AGENCY_ID=''">
				<tr>
					<td class="midcolora">Please select Agency.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="APP_EXPIRATION_DATE">
		<xsl:choose>
			<xsl:when test="APP_EXPIRATION_DATE='' or APP_EXPIRATION_DATE='NULL'">
				<tr>
					<td class="midcolora">Please insert Expiration Date.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="APP_EFFECTIVE_DATE">
		<xsl:choose>
			<xsl:when test="APP_EFFECTIVE_DATE='' or APP_EFFECTIVE_DATE='NULL'">
				<tr>
					<td class="midcolora">Please insert Effective Date.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="STATE_ID">
		<xsl:choose>
			<xsl:when test="STATE_ID=-1 or STATE_ID='' ">
				<tr>
					<td class="midcolora">Please select State.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="APP_LOB">
		<xsl:choose>
			<xsl:when test="APP_LOB=''or APP_LOB=NULL ">
				<tr>
					<td class="midcolora">Please select Line of Business.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="APP_TERMS">
		<xsl:choose>
			<xsl:when test="APP_TERMS=''">
				<tr>
					<td class="midcolora">Please select Policy Term Months.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- ========================================= Location Detail ======================================= -->
	<xsl:template name="LOC_NUM">
		<xsl:choose>
			<xsl:when test="LOC_NUM=''">
				<tr>
					<td class="midcolora">Please insert Location Number.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="LOC_ADD1">
		<xsl:choose>
			<xsl:when test="LOC_ADD1=''">
				<tr>
					<td class="midcolora">Please insert Address1.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="LOC_TYPE">
		<xsl:choose>
			<xsl:when test="LOC_TYPE=''">
				<tr>
					<td class="midcolora">Please select Location Is.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="LOC_CITY">
		<xsl:choose>
			<xsl:when test="LOC_CITY=''">
				<tr>
					<td class="midcolora">Please insert City.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="LOC_STATE">
		<xsl:choose>
			<xsl:when test="LOC_STATE=''">
				<tr>
					<td class="midcolora">Please select State.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="LOC_ZIP">
		<xsl:choose>
			<xsl:when test="LOC_ZIP=''">
				<tr>
					<td class="midcolora">Please insert Zip.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- ================================= Dwelling Info ==================================== -->
	<xsl:template name="DWELLING_NUMBER">
		<xsl:choose>
			<xsl:when test="DWELLING_NUMBER=''">
				<tr>
					<td class="midcolora">Please insert Customer Dwelling #.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="LOCATION_ID">
		<xsl:choose>
			<xsl:when test="LOCATION_ID=''">
				<tr>
					<td class="midcolora">Please select Location.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="YEAR_BUILT">
		<xsl:choose>
			<xsl:when test="YEAR_BUILT=''">
				<tr>
					<td class="midcolora">Please insert Year built.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="REPLACEMENT_COST">
		<xsl:choose>
			<xsl:when test="REPLACEMENT_COST=''">
				<tr>
					<xsl:choose>
						<!-- DP-2 Repair Cost,DP-3 Repair Cost -->
						<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11290 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11292
						or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11480  or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11482">
							<td class="midcolora">Please insert Repair Cost.</td>
						</xsl:when>
						<xsl:otherwise>
							<td class="midcolora">Please insert Replacement Cost.</td>
						</xsl:otherwise>
					</xsl:choose>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="OCCUPANCY">
		<xsl:choose>
			<xsl:when test="OCCUPANCY=''">
				<tr>
					<td class="midcolora">Please select Occupancy.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="BUILDING_TYPE">
		<xsl:choose>
			<xsl:when test="BUILDING_TYPE=''">
				<tr>
					<td class="midcolora">Please select Building type.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MARKET_VALUE">
		<xsl:choose>
			<xsl:when test="MARKET_VALUE=''">
				<tr>
					<td class="midcolora">Please insert Market Value.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DP2_RC_MINVALUE">
		<xsl:choose>
			<xsl:when test="DP2_RC_MINVALUE=''">
				<tr>
					<td class="midcolora">In case of DP-2 Repair Cost policy minimum value of Repair cost or Market Value is $30,000.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DP3_RC_MINVALUE">
		<xsl:choose>
			<xsl:when test="DP3_RC_MINVALUE=''">
				<tr>
					<td class="midcolora">In case of DP-3 Repair Cost policy minimum value of Repair cost or Market Value is $75,000.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MARKET_VS_REPCOST">
		<xsl:choose>
			<xsl:when test="MARKET_VS_REPCOST=''">
				<tr>
					<td class="midcolora">In case of DP-2 or DP-3 Repair cost Market Value and Repair cost should be the same.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- ============================== Rating Info ========================================== -->
	<xsl:template name="NO_OF_FAMILIES">
		<xsl:choose>
			<xsl:when test="NO_OF_FAMILIES=''">
				<tr>
					<td class="midcolora">Please insert Number of Families.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HYDRANT_DIST">
		<xsl:choose>
			<xsl:when test="HYDRANT_DIST=''">
				<tr>
					<td class="midcolora">Please insert Distance to the fire station.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="FIRE_STATION_DIST">
		<xsl:choose>
			<xsl:when test="FIRE_STATION_DIST=''">
				<tr>
					<td class="midcolora">Please insert # of Miles from Fire Station.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_UNDER_CONSTRUCTION">
		<xsl:choose>
			<xsl:when test="IS_UNDER_CONSTRUCTION=''">
				<tr>
					<td class="midcolora">Please select Building Under Construction - Builders Risk.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PROT_CLASS">
		<xsl:choose>
			<xsl:when test="PROT_CLASS=''">
				<tr>
					<td class="midcolora">Please insert Protection class.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="WIRING_UPDATE_YEAR">
		<xsl:choose>
			<xsl:when test="WIRING_UPDATE_YEAR=''">
				<tr>
					<td class="midcolora">Please select Wiring Update.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PLUMBING_UPDATE_YEAR">
		<xsl:choose>
			<xsl:when test="PLUMBING_UPDATE_YEAR=''">
				<tr>
					<td class="midcolora">Please select Plumbing Update.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HEATING_UPDATE_YEAR">
		<xsl:choose>
			<xsl:when test="HEATING_UPDATE_YEAR=''">
				<tr>
					<td class="midcolora">Please select Heating Update.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ROOFING_UPDATE_YEAR">
		<xsl:choose>
			<xsl:when test="ROOFING_UPDATE_YEAR=''">
				<tr>
					<td class="midcolora">Please select Roofing Update.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="NO_OF_AMPS">
		<xsl:choose>
			<xsl:when test="NO_OF_AMPS=''">
				<tr>
					<td class="midcolora">Please insert Number of Amps (Elec Sys).</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="CIRCUIT_BREAKERS">
		<xsl:choose>
			<xsl:when test="CIRCUIT_BREAKERS=''">
				<tr>
					<td class="midcolora">Please select Circuit Breakers.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="EXTERIOR_CONSTRUCTION">
		<xsl:choose>
			<xsl:when test="EXTERIOR_CONSTRUCTION=''">
				<tr>
					<td class="midcolora">Please select Primary Exterior construction.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="EXTERIOR_OTHER_DESC">
		<xsl:choose>
			<xsl:when test="EXTERIOR_OTHER_DESC=''">
				<tr>
					<td class="midcolora">Please insert Primary Exterior construction's Other Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="UNDER_CONSTRUCTION_POLICY_TERM">
		<xsl:choose>
			<xsl:when test="UNDER_CONSTRUCTION_POLICY_TERM=''">
				<tr>
					<td class="midcolora">Building Under Construction (Builder Risk) is selected,Make sure that the Policy Term under Application/Policy Details is 1 year.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="UNDER_CONSTRUCTION_DATE">
		<xsl:choose>
			<xsl:when test="UNDER_CONSTRUCTION_DATE=''">
				<tr>
					<td class="midcolora">Please insert Construction Started Date.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="UNDER_CONSTRUCTION_POLICYTERM">
		<xsl:choose>
			<xsl:when test="UNDER_CONSTRUCTION_POLICYTERM=''">
				<tr>
					<td class="midcolora">Please select Is Construction supervised by licensed building contractor?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ROOFTYPE">
		<xsl:choose>
			<xsl:when test="ROOFTYPE='' or ROOFTYPE=0">
				<tr>
					<td class="midcolora">Please select Roof Type.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- ======================================== Coverage Info =================================== -->
	<xsl:template name="PERSONAL_PROP_LIMIT">
		<xsl:choose>
			<xsl:when test="PERSONAL_PROP_LIMIT=''">
				<tr>
					<td class="midcolora">Please insert Personal Property Limit C.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DWELLING_LIMIT">
		<xsl:choose>
			<xsl:when test="DWELLING_LIMIT=''">
				<tr>
					<td class="midcolora">Please insert Coverage A.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- ======================================================== -->
	<xsl:template name="MIN_COVA_REPCOST"> <!--Removed Mandatory-->
		<xsl:choose>
			<xsl:when test="MIN_COVA_REPCOST=''">
				<tr>
					<td class="midcolora">DP-2,DP-3 coverage A must be at least 80% of replacement cost.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MAXVAL_REPLACEMENT_COST">
		<xsl:choose>
			<xsl:when test="MAXVAL_REPLACEMENT_COST=''">
				<tr>
					<td class="midcolora">DP-2,DP-3 coverage A can not be more than replacement cost.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- DP-2 Repair Cost coverage A can not be more than full replacement cost. -->
	<xsl:template name="MAX_COV_REPCOST_RC">
		<xsl:choose>
			<xsl:when test="MAX_COV_REPCOST_RC=''">
				<tr>
					<td class="midcolora">DP-2 Repair Cost coverage A can not be more than full repair cost.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MIN_COV_REPCOST_RC">
		<xsl:choose>
			<xsl:when test="MIN_COV_REPCOST_RC=''">
				<tr>
					<td class="midcolora">DP-2 Repair Cost requires minimum coverage A of $10000.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MIN_COVA_DP3RC">
		<xsl:choose>
			<xsl:when test="MIN_COVA_DP3RC=''">
				<tr>
					<td class="midcolora">DP-3 Repair Cost requires minimum coverage A of $75000.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="COVA_REPCOST_DP3P">
		<xsl:choose>
			<xsl:when test="COVA_REPCOST_DP3P=''">
				<tr>
					<td class="midcolora">DP-3 Premier,Michigan requires residence to be insured to 100% of replacement cost.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- ======================================================== -->
	<!-- Template for showing Info at the initial stage (Mandatory only) -->
	<xsl:template name="RDDETAIL">
		<!-- Application List -->
		<xsl:call-template name="APPLICATION_LIST_MESSAGESGES" />
		<!-- Location Detail -->
		<xsl:call-template name="LOCATIONMESSAGE" />
		<!-- Dwelling Info -->
		<xsl:call-template name="DWELLINGINFOMESSAGE" />
		<!-- Dwelling Rating Info -->
		<xsl:call-template name="DWELLINGRATINGINFOMESSAGE" />
		<!-- Coverage/Limt  -->
		<xsl:call-template name="COVERAGELIMITSMESSAGE" />
		<!-- Gen Info -->
		<xsl:choose>
			<xsl:when test="INPUTXML/RDGENINFOS/RDGENINFO/IS_RECORD_EXISTS='Y'">
				<xsl:call-template name="GENINFOMESSAGE" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:apply-templates select="INPUTXML/RDGENINFOS/RDGENINFO" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Application Screen -->
	<xsl:template name="APPLICATION_LIST_MESSAGESGES">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/IS_PRIMARY_APPLICANT=''">
				<tr>
					<td class="midcolora">You must have at least one primary applicant.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Gen Info -->
	<xsl:template name="GENINFOMESSAGE">
		<tr>
			<td class="pageheader">For Underwriting Questions:</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is dwelling currently Vacant or Unoccupied?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Is dwelling currently Vacant or Unoccupied?' is 'Yes', then Please insert Vacant Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is dwelling rented in whole or part to students under age 30?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Is dwelling rented in whole or part to students under age 30?' is 'Yes', then  Please insert Rented Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is dwelling owned by anyone other than an individual?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Is dwelling owned by anyone other than an individual?' is 'Yes', then Please insert Dwelling Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any business conducted on Premises?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Any business conducted on Premises?' is 'Yes', then Please insert Business Description and select 'Provide Home Day Care for Monetary or other compensation?' </td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is this a Modular/Manufactured Home?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Is this a Modular/Manufactured Home?'  is 'Yes' , then Please select 'Is it Built on a continuous foundation?' </td>
		</tr>
		<tr>
			<td class="midcolora">Please Is property located next to a commercial building?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Is property located next to a commercial building?' is 'Yes', then   Please insert Property Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Are any outside stairways present?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Are any outside stairways present?' is 'Yes', then Please select Stairway Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any Animals, Dogs, Horses, Livestock or pets?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Any Animals, Dogs, Horses, Livestock or pets?' is 'Yes', then Please insert Other than Breed of Dogs.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Number/Breed of dogs.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Number/Breed of dogs' is 1 to 10, then Please select Breed of Dog.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is there an enclosed swimming pool or hot tub (not including bathroom type Jacuzzi or hot tub)?.</td>
		</tr>
		<tr>
			<td class="midcolora">If,'Is there an enclosed swimming pool or hot tub (not including bathroom type Jacuzzi or hot tub)' is 'Yes',
			then please insert 'Enclosed swimming pool or hot tub description'.</td>
		</tr>
		
		<tr>
			<td class="midcolora">Please select Valued Customer Discount Override.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Valued Customer Discount Override' is 'Yes', then Please insert Valued Customer Discount Override Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Has insurance been transferred within the agency?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Has insurance been transferred within the agency?' is 'Yes', then Please insert/select Inception date with agency.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Has ownership of this dwelling changed more than one time in the last 3 years?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Has ownership of this dwelling changed more than one time in the last 3 years?' is 'Yes', then Please insert Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any coverage declined, cancelled or non-renewed during the last 3 years?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Any coverage declined, cancelled or non-renewed during the last 3 years?' is 'Yes', then Please insert Declined Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Conviction of any degree of arson in last 3 years?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Conviction of any degree of arson in last 3 years?' is 'Yes', then Please insert Description of conviction.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any lead paint hazard?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Any lead paint hazard?'is 'Yes', then Please insert Description of lead paint hazard.</td>
		</tr>
		<!-- For michigan Only  -->
		<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/STATE_ID=22">
			<tr>
				<td class="midcolora">Please select Is multipolicy discount applied?.</td>
			</tr>
			<tr>
				<td class="midcolora">If,'Is multipolicy discount applied?' is 'Yes' then Please insert Multipolicy discount description.</td>
			</tr>
		</xsl:if>
		<tr>
			<td class="midcolora">Please select Any residence employees?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Any residence employees?' is 'Yes', then Please insert Number of resident employee.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any other residence owned, occupied or rented?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Any other residence owned, occupied or rented?' is 'Yes', then Please insert Description other residence.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any other insurance with this company?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Any other insurance with this company?' is 'Yes', then Please insert Description other insurance(List policy #s).</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any current renovation/remodeling.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Any current renovation/remodeling' is 'Yes', then Please insert Est. completion date and value, if undergoing renovation.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Trampoline?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Trampoline?' is 'Yes', then Please insert Description of trampoline.</td>
		</tr>
		<tr>
			<td class="midcolora"> Please select Roomers or Boaders.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Roomers or Boaders' is 'Yes', then Please insert # of Roomers and Boarders.</td>
		</tr>
		<tr>
			<td class="midcolora"> Please select Any supplemental heating source(wood stove,fireplace inserts,etc.)?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Any supplemental heating source(wood stove,fireplace inserts,etc.)?' is 'Yes', then Please insert Wood stove supplement.</td>
		</tr>
		<tr>
			<td class="midcolora"> Please select If Building is Under Construction, is the Applicant the General Contractor?.</td>
		</tr>
		 <tr>
			<td class="midcolora"> If, 'If Building is Under Construction, is the Applicant the General Contractor?' is 'Yes', then Please insert General Contractor Details.</td>
		</tr> 
		<tr>
			<td class="midcolora">Please select Swimming Pool.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is Property situated on more than 5 Acres?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Is Property situated on more than 5 Acres?' is 'Yes', then Please insert Description of Land Use.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is the dwelling a mobile home or double wide?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Is the dwelling a mobile home or double wide?' is 'Yes', then Please insert Dwelling Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is Property Used in whole or in part for farming, commercial, industrial, professional or business purposes except if use is incidental?</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Is Property Used in whole or in part for farming, commercial, industrial, professional or business purposes except if use is incidental?' is 'Yes', then Please insert Business Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any Prior Losses.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Any Prior Losses' is 'Yes', then Please insert Prior Losses Description.</td>
		</tr>
	</xsl:template>
	<!-- ======================================= Location Details  ============================ -->
	<xsl:template name="LOCATIONMESSAGE">
		<tr>
			<td class="pageheader">You must have at least one active location with the following information:</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Location Number.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Location Is.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Address1.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert City.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select State.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Zip.</td>
		</tr>
	</xsl:template>
	<!-- ========================================= Dwelling Info  ========================================= -->
	<xsl:template name="DWELLINGINFOMESSAGE">
		<tr>
			<td class="pageheader">You must have at least one active dwelling with the following information:</td>
		</tr>
		<tr>
			<td class="pageheader">For Dwelling info:</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Customer Dwelling #.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Location.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Year built.</td>
		</tr>
		<tr>
			<xsl:choose>
				<!-- DP-2 Repair Cost,DP-3 Repair Cost -->
				<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11290 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11292
						or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11480  or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11482">
					<td class="midcolora">Please insert Market Value/Repair Cost.</td>
				</xsl:when>
				<xsl:otherwise>
					<td class="midcolora">Please insert Market Value.</td>
				</xsl:otherwise>
			</xsl:choose>
		</tr>
		<!--<tr>
			<xsl:choose> REMOVED ON 5 JULY 2006
				 DP-2 Repair Cost,DP-3 Repair Cost 
				<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11290 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11292
						or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11480  or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11482">
					<td class="midcolora">Please insert Repair Cost.</td>
				</xsl:when>
				<xsl:otherwise>
					<td class="midcolora">Please insert Replacement Cost.</td>
				</xsl:otherwise>
			</xsl:choose>
		</tr>-->
		<tr> <!--Added on 5 July 2006-->
			<xsl:choose>
				<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE!=11480 and INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE!=11290
				and INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE!=11290 and INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE!=11482">
					<td class="midcolora">Please insert Replacement Cost.</td>
				</xsl:when>
			</xsl:choose>
		</tr>
		<tr>
			<td class="midcolora">Please select Occupancy.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Building type.</td>
		</tr>
	</xsl:template>
	<!-- ====================================== Dwelling Rating Info ======================================= -->
	<xsl:template name="DWELLINGRATINGINFOMESSAGE">
		<tr>
			<td class="pageheader">For Rating Info:</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Distance to the fire station.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert # of Miles from Fire Station.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Building Under Construction - Builders Risk.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Protection class.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Wiring Update.</td>
		</tr>
		<tr>
			<td class="midcolora">If,selected value of Wiring Update  is not 'None', then please insert Wiring update year.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Plumbing Update.</td>
		</tr>
		<tr>
			<td class="midcolora">If, selected value of Plumbing Update is not 'None', then please insert Plumbing update year.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Heating Update.</td>
		</tr>
		<tr>
			<td class="midcolora">If, selected value of Heating Update is not 'None', then please insert Heating update year.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Roofing Update.</td>
		</tr>
		<tr>
			<td class="midcolora">If, selected value of Roofing Update is not 'None', then please insert Roofing update year.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Number of Amps (Elec Sys).</td>
		</tr>
		
		<tr>
			<td class="midcolora">Please select Circuit Breakers.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Number of Families.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Primary Exterior construction.</td>
		</tr>
		<!--xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE =11400 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE =11404 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE =11481 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE =11482 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE =11458"-->
				<tr>
					<td class="midcolora">Please select Roof Type.</td>
				</tr>
			<!--/xsl:when>	
		</xsl:choose-->
		
		<tr>
			<td class="midcolora">If, we selected some value of 'Primary Exterior construction' , then it display 'Rated Class'.</td>
		</tr>
		 
		<tr>
			<td class="midcolora">If, 'Roof Type' is 'Other' then please insert Other Description.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Primary Heat Type' is 'Other' then please insert Other Description.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Secondary Heat Type' is 'Other' then please insert Other Description.</td>
		</tr>
		
	</xsl:template>
	<!-- ====================================== Coverage/Limit ======================================= -->
	<xsl:template name="COVERAGELIMITSMESSAGE">
		<tr>
			<td class="pageheader">For SectionI/SectionII Info:</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Coverage A (Dwelling).</td>
		</tr>
		<!--<tr>
			<td class="midcolora">Please select Personal Liability Limits.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Medical Payments each Person.</td>
		</tr> -->
	</xsl:template>
</xsl:stylesheet>