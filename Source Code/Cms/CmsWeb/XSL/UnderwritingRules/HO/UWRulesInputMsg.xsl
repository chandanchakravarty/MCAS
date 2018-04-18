<!-- ==================================================================================================
File Name			:	UWRulesInputMsg.xsl
Purpose				:	To display Messages for Homeowners 
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
			<!--==================================== Client Top ====================================-->
			<table border="1" align="center" width='90%'>
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
						<td class="headereffectcenter" colspan="6">Application verification status</td>
					</xsl:if>
					<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=1">
						<td class="headereffectcenter" colspan="6">Policy verification status</td>
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
						<xsl:call-template name="HODETAIL" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:call-template name="SHOWHOINPUTDETAIL" />
					</xsl:otherwise>
				</xsl:choose>
				<xsl:choose>
					<!-- ================== when there is no location  and dwelling ===================== -->
					<xsl:when test="INPUTXML/BillingInfoS/ERRORS/ERROR/@ERRFOUND='T'">
						<!-- =============== call a template shows all the messages ======== -->
						<xsl:if test="user:AssignUnderwritter(0) = 0" />
						<xsl:call-template name="BILLINGMESSAGE" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:call-template name="SHOWHOINPUTDETAIL" />
					</xsl:otherwise>
				</xsl:choose>
			</table>
		</html>
	</xsl:template>
	<!-- ===============This template shows the input detail (rules + mandatory) of HO ================= -->
	<xsl:template name="SHOWHOINPUTDETAIL">
		<xsl:apply-templates select="INPUTXML/APPLICATIONS/APPLICATION"></xsl:apply-templates>
		<xsl:apply-templates select="INPUTXML/LOCATIONS/LOCATION"></xsl:apply-templates>
		<xsl:apply-templates select="INPUTXML/BillingInfoS/Plan"></xsl:apply-templates>
		
		<!-- Trailers Info -->
		<xsl:call-template name="TRAILER_INFO" /> 
		<!-- Gen Info  -->
		<!-- ============================== Dwelling Infos =========================================== -->
		
				<xsl:choose>
					<xsl:when test="INPUTXML/DWELLINGS/@COUNT > 0">
						<xsl:for-each select="INPUTXML/DWELLINGS/DWELLINGINFO">
							<xsl:if test="DWELLING/DWELLING_NUMBER='' or DWELLING/DWELLING_NUMBER=0 or DWELLING/LOCATION_ID='' or DWELLING/YEAR_BUILT='' or DWELLING/YEAR_BUILT=0
							or DWELLING/REPLACEMENT_COST='' or DWELLING/MARKET_VALUE='' or DWELLING/BUILDING_TYPE='' or DWELLING/OCCUPANCY='' or DWELLING/REPAIRCOST_MARKETVALUE='' or DWELLING/DETACHED_OTHER_STRUCTURES='' or DWELLING/PREMISES_LOCATION=''
							">
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
								<!--<xsl:apply-templates select="RATINGINFO" />-->
								<!--<xsl:apply-templates select="COVERAGE" />-->
							</xsl:if>
						</xsl:for-each>
					</xsl:when>
					<xsl:otherwise>
						<xsl:if test="user:AssignUnderwritter(1)=0" />
						<!-- Dwelling Info -->
						<xsl:call-template name="DWELLINGINFOMESSAGE" />
						<!-- Dwelling Rating Info -->
						<!--<xsl:call-template name="DWELLINGRATINGINFOMESSAGE" />-->
						<!-- Section 1  -->
						<xsl:call-template name="COVERAGELIMITSMESSAGE" />
						<!-- Section 2  -->
						<xsl:call-template name="COVERAGELIMITSMESSAGE_SECII" />
					</xsl:otherwise>
				</xsl:choose>
			
		<!-- For RV Vehicles --> 
		<xsl:apply-templates select="INPUTXML/RV_VEHICLES/RV_VEHICLE"></xsl:apply-templates>		
		
		<!-- Gen Info  -->
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/IS_RECORD_EXISTS='N'">
				<xsl:apply-templates select="INPUTXML/HOGENINFOS/HOGENINFO" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
				<xsl:call-template name="GENINFOMESSAGE" />
			</xsl:otherwise>
		</xsl:choose>
		<!-- ================================== Solid Fuel ================================================= -->
		<xsl:if test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/SECONDARY_HEAT_TYPE='Y'">
			<xsl:choose>
				<xsl:when test="INPUTXML/FUELINFOS/@COUNT>0">
					<tr>
						<td>
							<xsl:apply-templates select="INPUTXML/FUELINFOS/FUELINFO" />
						</td>
					</tr>
				</xsl:when>
				<xsl:otherwise>
					<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
					<xsl:call-template name="SOLIDFUELINFOMESSAGE" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:if>
		<!-- ================= if one of the following screen has been saved =========================-->
		<!-- <xsl:if test="INPUTXML/BOATS/BOAT/BOAT_NO='' or INPUTXML/BOATS/BOAT/BOAT_YEAR=''or INPUTXML/BOATS/BOAT/MODEL=''
		 or INPUTXML/BOATS/BOAT/LENGTH=''or INPUTXML/BOATS/BOAT/TYPE_OF_WATERCRAFT=''or	 INPUTXML/BOATS/BOAT/MAKE=''or  
		 INPUTXML/BOATS/BOAT/HULL_ID_NO=''or INPUTXML/BOATS/BOAT/STATE_REG=''or INPUTXML/BOATS/BOAT/HULL_MATERIAL='' or 
		 INPUTXML/BOATS/BOAT/WATER_NAVIGATED='' or INPUTXML/BOATS/BOAT/MAX_SPEED=''	 or INPUTXML/BOATS/BOAT/TERRITORY='' or INPUTXML/BOATS/BOAT/INSURING_VALUE='' 
		 or INPUTXML/OPERATORS/OPERATOR/DRIVER_FNAME=''or INPUTXML/OPERATORS/OPERATOR/DRIVER_LNAME=''or INPUTXML/OPERATORS/OPERATOR/DRIVER_CODE=''or 
		 INPUTXML/OPERATORS/OPERATOR/DRIVER_DRIV_LIC='' or INPUTXML/OPERATORS/OPERATOR/DRIVER_SEX='' or INPUTXML/OPERATORS/OPERATOR/DRIVER_SEX='0' 
		 or INPUTXML/OPERATORS/OPERATOR/DRIVER_STATE=''or INPUTXML/OPERATORS/OPERATOR/DRIVER_ZIP='' or  INPUTXML/OPERATORS/OPERATOR/DRIVER_DOB=''or
		 INPUTXML/OPERATORS/OPERATOR/DRIVER_LIC_STATE='' or INPUTXML/OPERATORS/OPERATOR/DRIVER_LIC_STATE='' or INPUTXML/OPERATORS/OPERATOR/VEHICLE_ID='0' 
		 or INPUTXML/OPERATORS/OPERATOR/VEHICLE_ID='' or INPUTXML/OPERATORS/OPERATOR/PRIN_OCC_ID='0' or INPUTXML/OPERATORS/OPERATOR/PRIN_OCC_ID='' or INPUTXML/OPERATORS/OPERATOR/DEACTIVATEBOAT='Y'
		 or INPUTXML/OPERATORS/OPERATOR/DRIVER_ADD1='' or INPUTXML/OPERATORS/OPERATOR/DRIVER_CITY=''
		 or INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/PHY_MENTL_CHALLENGED=''or INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/PHY_MENTL_CHALLENGED_DESC='' or 
		 INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/DRIVER_SUS_REVOKED=''or INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/DRIVER_SUS_REVOKED_DESC=''
    	 or INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/IS_CONVICTED_ACCIDENT=''or INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/IS_CONVICTED_ACCIDENT_DESC='' or
    	 INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/DRINK_DRUG_VOILATION='' or INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/MINOR_VIOLATION=''or 
    	 INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/ANY_OTH_INSU_COMP='' or INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/OTHER_POLICY_NUMBER_LIST='' or INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/ANY_LOSS_THREE_YEARS='' 
    	 or INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/ANY_LOSS_THREE_YEARS_DESC='' or INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/COVERAGE_DECLINED='' or
    	 INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/COVERAGE_DECLINED_DESC='' or INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/IS_RENTED_OTHERS='' or INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/IS_RENTED_OTHERS_DESC='' or 
    	 INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/IS_REGISTERED_OTHERS='' or INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/PARTICIPATE_RACE='' or
    	 INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/CARRY_PASSENGER_FOR_CHARGE='' or INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/TOT_YRS_OPERATORS_EXP=''"> -->
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/@COUNT>0 or INPUTXML/OPERATORS/@COUNT>0 or INPUTXML/WATERCRAFTGENINFOS/@COUNT>0">
				<!-- <td class="pageheader">You  must have the following information for Watercraft:</td> -->
				<xsl:call-template name="SHOWWCINPUTDETAIL" />
			</xsl:when>
		</xsl:choose>
		<!-- </xsl:if> -->
	</xsl:template>
	<!-- ============================== Application Detail ================================= -->
	<xsl:template match="INPUTXML/APPLICATIONS/APPLICATION">
		<xsl:choose>
			<xsl:when test="STATE_ID=-1 or STATE_ID='' or  APP_LOB='' or APP_TERMS='' or APP_EFFECTIVE_DATE=''  or
			APP_EXPIRATION_DATE='' or APP_AGENCY_ID=-1 or APP_AGENCY_ID=''or DOWN_PAY_MENT='' or BILL_TYPE='' or PROXY_SIGN_OBTAINED=-1 or PROXY_SIGN_OBTAINED=''
				 or CHARGE_OFF_PRMIUM='' or IS_PRIMARY_APPLICANT='' or DFI_ACC_NO_RULE='' or TRANSIT_ROUTING_RULE='' or  IS_PRIMARY_APPLICANT='Y' or PIC_OF_LOC='' or PROPRTY_INSP_CREDIT='' or POL_UNDERWRITER=''">
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
				<xsl:call-template name="PIC_OF_LOC" />
				<xsl:call-template name="PROPRTY_INSP_CREDIT" />
				<xsl:call-template name="TRANSIT_ROUTING_RULE" />
				<xsl:call-template name="DFI_ACC_NO_RULE" />
				<xsl:call-template name="POL_UNDERWRITER" />
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
	<!-- ==============================RV INFO ================================= -->
	<!--xsl:template match="INPUTXML/APPLICATIONS/APPLICATION">
		<xsl:choose>
			<xsl:when test="YEAR='' 
			or MAKE='' or
			 MODEL=''  or
			SERIAL='' or 
			STATE_REGISTERED=-1 
			or HORSE_POWER='' or
			 VEHICLE_TYPE=-1 or 
			INSURING_VALUE='' or
			 DEDUCTIBLE=-1
				 ">
				<tr>
					<xsl:choose>
						<xsl:when test="CALLED_FROM=1">
							<td class="pageheader">For RV information:</td>
						</xsl:when>
						<xsl:otherwise>
							<td class="pageheader">For RV information:</td>
						</xsl:otherwise>
					</xsl:choose>
				</tr>
				<xsl:call-template name="RV_YEAR" />
				<xsl:call-template name="RV_MAKE" />
				<xsl:call-template name="RV_MODEL" />
				<xsl:call-template name="RV_SERIAL" />
				<xsl:call-template name="RV_STATE_REGISTERED" />
				<xsl:call-template name="RV_HORSE_POWER" />
				<xsl:call-template name="RV_TYPE" />
				<xsl:call-template name="RV_INSURING_VALUE" />
				
				<xsl:call-template name="PIC_OF_LOC" />
				
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
	</xsl:template -->
	<!-- ==============================RV INFO =============================================== -->
	<xsl:template match="INPUTXML/RV_VEHICLES/RV_VEHICLE">
		<xsl:choose><!--REC_VEHICLE_PRIN_OCC_ID,Charles,7-Dec-09,Itrack 6798--><!--Removed RV_USED_IN_RACE_SPEED, Charles (10-Dec-09), Itrack 6841 -->
			<xsl:when test="COMPANY_ID_NUMBER='' or COMPANY_ID_NUMBER=0 or RV_YEAR='' or RV_MAKE='' or RV_MODEL='' or RV_SERIAL=''or RV_TYPE='' or REC_VEHICLE_PRIN_OCC_ID=''
			 or RV_STATE_REGISTERED='' or RV_HORSE_POWER='' or RV_INSURING_VALUE='' or RV_INSURING_VALUE=0 or RV_DEDUCTIBLE='' or RV_DEDUCTIBLE=0 ">
				<tr>
					<td class="pageheader">For Recreational Vehicle Information: </td>
					<tr>
						<td>
							<table width="60%">
								<tr>
									<xsl:if test="COMPANY_ID_NUMBER!= ''">
										<td class="pageheader" width="18%">RV #:</td>
										<td class="midcolora" width="36%">
											<xsl:value-of select="COMPANY_ID_NUMBER" />
										</td>
									</xsl:if>
								</tr>
							</table>
						</td>
					</tr>
				</tr>
				<xsl:call-template name="RV_INFO_MESSAGES" />
				<xsl:call-template name="REC_VEHICLE_PRIN_OCC_ID" /> <!-- Added by Charles on 7-Dec-09 for Itrack 6798 -->
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
			<xsl:when test=" LOC_NUM='' or LOC_ADD1='' or LOC_CITY='' or LOC_ZIP=''">
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
				<!--<xsl:call-template name="LOC_STATE" />-->
				<xsl:call-template name="LOC_ZIP" />
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
	
	<!--===============================Billing Info======================================-->
	<xsl:template match="INPUTXML/BillingInfoS/Plan">
		<xsl:choose>
			<xsl:when test="NET_AMOUNT=''">
				<tr>
					<td class="pageheader">For Billing Information: </td>
					<tr>
						<td>
							<table width="60%">
								<tr>
									<xsl:if test="NET_AMOUNT != ''">
										<td class="pageheader" width="18%">Plan ID:</td>
										<td class="midcolora" width="36%">
											<xsl:value-of select="PLAN_ID" />
										</td>
									</xsl:if>
								</tr>
							</table>
						</td>
					</tr>
				</tr>
				<xsl:call-template name="NET_AMOUNT" />
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
			<xsl:when test="DWELLING_NUMBER='' or DWELLING_NUMBER=0 or LOCATION_ID='' or YEAR_BUILT='' or YEAR_BUILT=0 or REPLACEMENT_COST='' or 
			MARKET_VALUE='' or BUILDING_TYPE='' or OCCUPANCY='' or REPAIRCOST_MARKETVALUE='' or DETACHED_OTHER_STRUCTURES='' or PREMISES_LOCATION=''">
				<tr>
					<td class="pageheader">For Dwelling Information: </td>
				</tr>
				<xsl:call-template name="DWELLING_NUMBER" />
				<xsl:call-template name="LOCATION_ID" />
				<xsl:call-template name="YEAR_BUILT" />
				<xsl:call-template name="MARKET_VALUE" />
				<xsl:call-template name="REPLACEMENT_COST" />
				<xsl:call-template name="BUILDING_TYPE" />
				<xsl:call-template name="OCCUPANCY" />
				<xsl:call-template name="REPAIRCOST_MARKETVALUE" />
				<xsl:call-template name="DETACHED_OTHER_STRUCTURES" />
				<xsl:call-template name="PREMISES_LOCATION" />
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
					<xsl:when test="HYDRANT_DIST='' or FIRE_STATION_DIST='' or PROT_CLASS=''
			 or NO_OF_AMPS='' or  CIRCUIT_BREAKERS='' or EXTERIOR_CONSTRUCTION='' or WIRING_UPDATE_YEAR=''
			 or PLUMBING_UPDATE_YEAR='' or HEATING_UPDATE_YEAR='' or ROOFING_UPDATE_YEAR=''
			 or NO_OF_FAMILIES='' or NEED_OF_UNITS='' or ROOF_TYPE='' or DWELL_UNDER_CONSTRUCTION='' or LOCATION_TEN_HOME=''">
						<tr>
							<td class="pageheader">For Rating Information: </td>
						</tr>
						<xsl:call-template name="HYDRANT_DIST" />
						<xsl:call-template name="FIRE_STATION_DIST" />
						<xsl:call-template name="PROT_CLASS" />
						<xsl:call-template name="WIRING_UPDATE_YEAR" />
						<xsl:call-template name="PLUMBING_UPDATE_YEAR" />
						<xsl:call-template name="HEATING_UPDATE_YEAR" />
						<xsl:call-template name="ROOFING_UPDATE_YEAR" />
						<xsl:call-template name="NO_OF_AMPS" />
						<xsl:call-template name="NO_OF_FAMILIES" />
						<xsl:call-template name="NEED_OF_UNITS" />
						<xsl:call-template name="CIRCUIT_BREAKERS" />
						<xsl:call-template name="EXTERIOR_CONSTRUCTION" />
						<xsl:call-template name="ROOF_TYPE" />
						<xsl:call-template name="DWELL_UNDER_CONSTRUCTION" />
						<xsl:call-template name="LOCATION_TEN_HOME" />
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
			<xsl:when test="DWELLING_LIMIT='' or PERSONAL_LIAB_LIMIT='' or MED_PAY_EACH_PERSON='' or PERSONAL_PROP_LIMIT=''
				or COVA_NOT_REPCOST='' or COVA_NOT_MAKVALUE='' or COVC_NOT_REPCOST=''">
				<tr>
					<td class="pageheader">For Coverage Section1 Information:</td>
				</tr>
				<xsl:call-template name="DWELLING_LIMIT" />
				<!--<xsl:call-template name="PERSONAL_LIAB_LIMIT" />
				<xsl:call-template name="PERSONAL_PROP_LIMIT" />-->
				<!--<xsl:call-template name="MED_PAY_EACH_PERSON" />-->
				<xsl:call-template name="COVA_NOT_REPCOST" />
				<xsl:call-template name="COVA_NOT_MAKVALUE" />
				<xsl:call-template name="COVC_NOT_REPCOST" />
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
	<xsl:template match="INPUTXML/HOGENINFOS/HOGENINFO">
		<xsl:choose>
			<xsl:when test=" IS_VACENT_OCCUPY='' or DESC_VACENT_OCCUPY='' or IS_RENTED_IN_PART='' or DESC_RENTED_IN_PART=''
			    or  IS_DWELLING_OWNED_BY_OTHER='' or DESC_DWELLING_OWNED_BY_OTHER='' or ANY_FARMING_BUSINESS_COND='' or DESC_FARMING_BUSINESS_COND='' or 
				IS_PROP_NEXT_COMMERICAL='' or DESC_PROPERTY='' or ARE_STAIRWAYS_PRESENT='' or DESC_STAIRWAYS='' or 
				ANIMALS_EXO_PETS_HISTORY='' or BREED='' or NO_OF_PETS='' or OTHER_DESCRIPTION='' or IS_SWIMPOLL_HOTTUB='' 
				or HAS_INSU_TRANSFERED_AGENCY='' or DESC_INSU_TRANSFERED_AGENCY='' or IS_OWNERS_DWELLING_CHANGED='' or 
				DESC_OWNER='' or ANY_COV_DECLINED_CANCELED='' or  DESC_COV_DECLINED_CANCELED='' or CONVICTION_DEGREE_IN_PAST='' or 
				DESC_CONVICTION_DEGREE_IN_PAST='' or LEAD_PAINT_HAZARD='' or DESC_LEAD_PAINT_HAZARD='' or MULTI_POLICY_DISC_APPLIED='' or DESC_MULTI_POLICY_DISC_APPLIED=''
				or ANY_RESIDENCE_EMPLOYEE='' or ANY_OTH_INSU_COMP='' or DESC_OTHER_INSURANCE='' or DESC_RESIDENCE_EMPLOYEE='' or DESC_OTHER_RESIDENCE='' or ANY_OTHER_RESI_OWNED='' or DESC_RESIDENCE_EMPLOYEE='' or 
				ANY_RENOVATION='' or DESC_RENOVATION='' or TRAMPOLINE='' or 
				DESC_TRAMPOLINE='' or RENTERS='' or DESC_RENTERS='' or ANY_HEATING_SOURCE='' or DESC_ANY_HEATING_SOURCE='' or 
				BUILD_UNDER_CON_GEN_CONT='' or NON_SMOKER_CREDIT='' or SWIMMING_POOL='' or ANY_FORMING='' or 
				 PREMISES='' or OF_ACRES='' or FLOCATION='' or DESC_LOCATION='' or   ISANY_HORSE='' 
				 or NO_HORSES='' or MODULAR_MANUFACTURED_HOME='' or VALUED_CUSTOMER_DISCOUNT_OVERRIDE='' or VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC='' or ANY_PRIOR_LOSSES='' or ANY_PRIOR_LOSSES_DESC='' 
				 or BOAT_WITH_HOMEOWNER=''">
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
				<xsl:call-template name="DESC_FARMING_BUSINESS_COND" />
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
				<xsl:call-template name="MULTI_POLICY_DISC_APPLIED" />
				<xsl:call-template name="DESC_MULTI_POLICY_DISC_APPLIED" />
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
				<xsl:call-template name="DESC_OTHER_RESIDENCE" />
				<!-- New Fields added <start> -->
				<xsl:call-template name="ANY_FORMING" />
				<xsl:call-template name="PREMISES" />
				<xsl:call-template name="OF_ACRES" />
				<xsl:call-template name="FLOCATION" />
				<xsl:call-template name="DESC_LOCATION" />
				<xsl:call-template name="ISANY_HORSE" />
				<!--xsl:call-template name="OF_ACRES_P" /-->
				<xsl:call-template name="NO_HORSES" />
				<xsl:call-template name="MODULAR_MANUFACTURED_HOME" />
				<xsl:call-template name="VALUED_CUSTOMER_DISCOUNT_OVERRIDE" />
				<xsl:call-template name="VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC" />
				<xsl:call-template name="ANY_PRIOR_LOSSES" />
				<xsl:call-template name="ANY_PRIOR_LOSSES_DESC" />
				<xsl:call-template name="BOAT_WITH_HOMEOWNER" />
				<!-- <end> -->
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
	<!-- =============================== Solid Fuels ================================================ -->
	<xsl:template match="INPUTXML/FUELINFOS/FUELINFO">
		<xsl:choose>
			<xsl:when test="MANUFACTURER='' or BRAND_NAME='' or MODEL_NUMBER='' or FUEL='' or 
			STOVE_TYPE='' or HAVE_LABORATORY_LABEL='' or IS_UNIT='' or LOCATION='' or CONSTRUCTION='' 
			or YEAR_DEVICE_INSTALLED='' or INSTALL_INSPECTED_BY='' or WAS_PROF_INSTALL_DONE='' or HEATING_USE='' 
			or HEATING_SOURCE=''">
				<tr>
					<td class="pageheader">For Solid Fuels:</td>
					<tr>
						<td>
							<table>
								<tr>
									<xsl:if test="MANUFACTURER != ''">
										<td class="pageheader">Manufacturer:</td>
										<td class="midcolora">
											<xsl:value-of select="MANUFACTURER" />
										</td>
									</xsl:if>
									<xsl:if test="BRAND_NAME != ''">
										<td class="pageheader" width="18%">Brand Name:</td>
										<td class="midcolora" width="36%">
											<xsl:value-of select="BRAND_NAME" />
										</td>
									</xsl:if>
									<xsl:if test="MODEL_NUMBER != ''">
										<td class="pageheader" width="18%">Model Number:</td>
										<td class="midcolora" width="36%">
											<xsl:value-of select="MODEL_NUMBER" />
										</td>
									</xsl:if>
								</tr>
							</table>
						</td>
					</tr>
				</tr>
				<xsl:call-template name="MANUFACTURER" />
				<xsl:call-template name="BRAND_NAME" />
				<xsl:call-template name="MODEL_NUMBER" />
				<xsl:call-template name="FUEL" />
				<xsl:call-template name="STOVE_TYPE" />
				<xsl:call-template name="HAVE_LABORATORY_LABEL" />
				<xsl:call-template name="IS_UNIT" />
				<xsl:call-template name="LOCATION" />
				<xsl:call-template name="CONSTRUCTION" />
				<xsl:call-template name="YEAR_DEVICE_INSTALLED" />
				<xsl:call-template name="INSTALL_INSPECTED_BY" />
				<xsl:call-template name="WAS_PROF_INSTALL_DONE" />
				<xsl:call-template name="HEATING_USE" />
				<xsl:call-template name="HEATING_SOURCE" />
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
	<!-- ============================== Solid Fuel ================================================== -->
	<xsl:template name="MANUFACTURER">
		<xsl:choose>
			<xsl:when test="MANUFACTURER=''">
				<tr>
					<td class="midcolora">Please insert Manufacturer.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="BRAND_NAME">
		<xsl:choose>
			<xsl:when test="BRAND_NAME=''">
				<tr>
					<td class="midcolora">Please insert Brand Name.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MODEL_NUMBER">
		<xsl:choose>
			<xsl:when test="MODEL_NUMBER=''">
				<tr>
					<td class="midcolora">Please insert Model Number.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="FUEL">
		<xsl:choose>
			<xsl:when test="FUEL=''">
				<tr>
					<td class="midcolora">Please insert Fuel.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="STOVE_TYPE">
		<xsl:choose>
			<xsl:when test="STOVE_TYPE=''">
				<tr>
					<td class="midcolora">Please select Stove Type.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HAVE_LABORATORY_LABEL">
		<xsl:choose>
			<xsl:when test="HAVE_LABORATORY_LABEL=''">
				<tr>
					<td class="midcolora">Please select Does the unit have a testing laboratory label (UL, other).</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_UNIT">
		<xsl:choose>
			<xsl:when test="IS_UNIT=''">
				<tr>
					<td class="midcolora">Please select Is the unit.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="LOCATION">
		<xsl:choose>
			<xsl:when test="LOCATION=''">
				<tr>
					<td class="midcolora">Please select Location.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="CONSTRUCTION">
		<xsl:choose>
			<xsl:when test="CONSTRUCTION=''">
				<tr>
					<td class="midcolora">Please select Construction.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="YEAR_DEVICE_INSTALLED">
		<xsl:choose>
			<xsl:when test="YEAR_DEVICE_INSTALLED=''">
				<tr>
					<td class="midcolora">Please insert Year Device Installed.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="INSTALL_INSPECTED_BY">
		<xsl:choose>
			<xsl:when test="INSTALL_INSPECTED_BY=''">
				<tr>
					<td class="midcolora">Please select Installation was inspected by.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="WAS_PROF_INSTALL_DONE">
		<xsl:choose>
			<xsl:when test="WAS_PROF_INSTALL_DONE=''">
				<tr>
					<td class="midcolora">Please select Was installation done by a professional installer such as a contractor?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HEATING_USE">
		<xsl:choose>
			<xsl:when test="HEATING_USE=''">
				<tr>
					<td class="midcolora">Please select Heating use.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HEATING_SOURCE">
		<xsl:choose>
			<xsl:when test="HEATING_SOURCE=''">
				<tr>
					<td class="midcolora">Please select What other type of heating source is used?.</td>
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
					<td class="midcolora">Please insert Vacant Description.</td>
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
					<td class="midcolora">Please insert Rented Description.</td>
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
					<td class="midcolora">Please insert Dwelling Description.</td>
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
	<xsl:template name="DESC_FARMING_BUSINESS_COND">
		<xsl:choose>
			<xsl:when test="DESC_FARMING_BUSINESS_COND=''">
				<tr>
					<td class="midcolora">Please insert Business Description.</td>
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
					<td class="midcolora">Please insert Property Description.</td>
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
					<td class="midcolora">Please insert Stairway Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ANIMALS_EXO_PETS_HISTORY">
		<xsl:choose>
			<xsl:when test="ANIMALS_EXO_PETS_HISTORY=''">
				<tr>
					<td class="midcolora">Please select Any animal,livestock or pets?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="BREED">
		<xsl:choose>
			<xsl:when test="BREED=''">
				<tr>
					<td class="midcolora">Please insert Other than Breed of Dogs.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="NO_OF_PETS">
		<xsl:choose>
			<xsl:when test="NO_OF_PETS=''">
				<tr>
					<td class="midcolora">Please select Number/breed of dogs.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="OTHER_DESCRIPTION">
		<xsl:choose>
			<xsl:when test="OTHER_DESCRIPTION=''">
				<tr>
					<td class="midcolora">Please select Breed of Dog.</td>
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
					<td class="midcolora">Please insert Insurance Transferred Description.</td>
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
					<td class="midcolora">Please insert Description of 'Has ownership of this dwelling changed more than one time in the last 3 years?'.</td>
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
					<td class="midcolora">Please insert Declined Description.</td>
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
	<xsl:template name="MULTI_POLICY_DISC_APPLIED">
		<xsl:choose>
			<xsl:when test="MULTI_POLICY_DISC_APPLIED=''">
				<tr>
					<td class="midcolora">Please select Is multi-policy discount applied?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_MULTI_POLICY_DISC_APPLIED">
		<xsl:choose>
			<xsl:when test="DESC_MULTI_POLICY_DISC_APPLIED=''">
				<tr>
					<td class="midcolora">Please insert multi-policy discount description.</td>
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
					<td class="midcolora">Please select Any residence employees?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_OTHER_RESIDENCE">
		<xsl:choose>
			<xsl:when test="DESC_OTHER_RESIDENCE=''">
				<tr>
					<td class="midcolora">Please insert Description other residence.</td>
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
					<td class="midcolora">Please select Roomers or Boarders.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_RENTERS">
		<xsl:choose>
			<xsl:when test="DESC_RENTERS=''">
				<tr>
					<td class="midcolora">Please insert '# of Roomers and Boarders'.</td>
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
	<!-- ======================= Modified by Ashwani on 27 Feb. 2006 ====================================== -->
	<!-- start -->
	<xsl:template name="ANY_FORMING">
		<xsl:choose>
			<xsl:when test="ANY_FORMING=''">
				<tr>
					<td class="midcolora">Please select Any Farming?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PREMISES">
		<xsl:choose>
			<xsl:when test="PREMISES=''">
				<tr>
					<td class="midcolora">Please select Farming Details.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="OF_ACRES">
		<xsl:choose>
			<xsl:when test="OF_ACRES=''">
				<tr>
					<td class="midcolora">Please insert # of Farming acres.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	
	<xsl:template name="FLOCATION">
		<xsl:choose>
			<xsl:when test="FLOCATION=''">
				<tr>
					<td class="midcolora">Please insert # of Locations.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESC_LOCATION">
		<xsl:choose>
			<xsl:when test="DESC_LOCATION=''">
				<tr>
					<td class="midcolora">Please insert Legal Description of Locations.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ISANY_HORSE">
		<xsl:choose>
			<xsl:when test="ISANY_HORSE=''">
				<tr>
					<td class="midcolora">Please select Any Horses?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--xsl:template name="OF_ACRES_P">
		<xsl:choose>
			<xsl:when test="OF_ACRES_P=''">
				<tr>
					<td class="midcolora">Please insert # of incidental income acres.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template -->
	<xsl:template name="NO_HORSES">
		<xsl:choose>
			<xsl:when test="NO_HORSES=''">
				<tr>
					<td class="midcolora">Please insert # of Horses.</td>
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
	<xsl:template name="ANY_PRIOR_LOSSES">
		<xsl:choose>
			<xsl:when test="ANY_PRIOR_LOSSES=''">
				<tr>
					<td class="midcolora">Please select Any Prior Losses.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="BOAT_WITH_HOMEOWNER">
		<xsl:choose>
			<xsl:when test="BOAT_WITH_HOMEOWNER=''">
				<tr>
					<td class="midcolora">Please select Do you want to add a boat with this Homeowner policy?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ANY_PRIOR_LOSSES_DESC">
		<xsl:choose>
			<xsl:when test="ANY_PRIOR_LOSSES_DESC=''">
				<tr>
					<td class="midcolora">Please insert Prior Losses Description.</td>
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
	<!-- end -->
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
	<!--22 June 2006-->
	<xsl:template name="PIC_OF_LOC">
		<xsl:choose>
			<xsl:when test="PIC_OF_LOC=''">
				<tr>
					<td class="midcolora">Please select Picture of Location attached?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PROPRTY_INSP_CREDIT">
		<xsl:choose>
			<xsl:when test="PROPRTY_INSP_CREDIT=''">
				<tr>
					<td class="midcolora">Please select Property Inspection/Cost Estimator.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--22 June 2006-->
	<!-- You must have at least one primary applicant.Please select Primary Applicant in CO Applicant Detail.-->
	<xsl:template name="IS_PRIMARY_APPLICANT">
		<xsl:choose>
			<xsl:when test="IS_PRIMARY_APPLICANT='' or IS_PRIMARY_APPLICANT='Y'">
				<tr>
					<td class="midcolora">You must have at least one primary applicant.</td>
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
	<xsl:template name="TRANSIT_ROUTING_RULE">
		<xsl:choose>
			<xsl:when test="TRANSIT_ROUTING_RULE=''">
				<tr>
					<td class="midcolora">Please insert Transit/Routing Number .</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	
	<xsl:template name="DFI_ACC_NO_RULE">
		<xsl:choose>
			<xsl:when test="DFI_ACC_NO_RULE=''">
				<tr>
					<td class="midcolora">Please insert DFI Account Number.</td>
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
	<!-- =========================================RV INFO  ===============================================-->
	<!--xsl:template name="RV_YEAR">
		<xsl:choose>
			<xsl:when test="RV_YEAR=''">
				<tr>
					<td class="midcolora">Please insert year.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="RV_MAKE">
		<xsl:choose>
			<xsl:when test="RV_MAKE=''">
				<tr>
					<td class="midcolora">Please insert make.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="RV_MODEL">
		<xsl:choose>
			<xsl:when test="RV_MODEL=''">
				<tr>
					<td class="midcolora">Please insert model.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="RV_SERIAL">
		<xsl:choose>
			<xsl:when test="RV_SERIAL=''">
				<tr>
					<td class="midcolora">Please select serial.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="RV_STATE_REGISTERED">
		<xsl:choose>
			<xsl:when test="RV_STATE_REGISTERED=''">
				<tr>
					<td class="midcolora">Please select state registered.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="RV_HORSE_POWER">
		<xsl:choose>
			<xsl:when test="RV_HORSE_POWER=''">
				<tr>
					<td class="midcolora">Please insert horse power.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="RV_TYPE">
		<xsl:choose>
			<xsl:when test="RV_TYPE=''">
				<tr>
					<td class="midcolora">Please select type.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="RV_INSURING_VALUE">
		<xsl:choose>
			<xsl:when test="RV_INSURING_VALUE=''">
				<tr>
					<td class="midcolora">Please insert insuring value.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="RV_DEDUCTIBLE">
		<xsl:choose>
			<xsl:when test="RV_DEDUCTIBLE=''">
				<tr>
					<td class="midcolora">Please select deductible.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template -->
	<!-- start ====================================RV Info Messages =================================== -->
	<xsl:template name="RV_INFO_MESSAGES">
		<xsl:if test="COMPANY_ID_NUMBER='' or COMPANY_ID_NUMBER=0">
			<tr>
				<td class="midcolora">Please insert RV #.</td>
			</tr>
		</xsl:if>
		<xsl:if test="RV_YEAR=''">
			<tr>
				<td class="midcolora">Please insert Year.</td>
			</tr>
		</xsl:if>
		<xsl:if test="RV_MAKE=''">
			<tr>
				<td class="midcolora">Please insert Make.</td>
			</tr>
		</xsl:if>
		<xsl:if test="RV_MODEL=''">
			<tr>
				<td class="midcolora">Please insert Model.</td>
			</tr>
		</xsl:if>
		<xsl:if test="RV_SERIAL=''">
			<tr>
				<td class="midcolora">Please insert serial #.</td>
			</tr>
		</xsl:if>
		<xsl:if test="RV_HORSE_POWER=''">
			<tr>
				<td class="midcolora">Please insert H.P./CC'S.</td>
			</tr>
		</xsl:if>
		<xsl:if test="RV_INSURING_VALUE='' or RV_INSURING_VALUE=0">
			<tr>
				<td class="midcolora">Please insert Insuring value.</td>
			</tr>
		</xsl:if>
		<xsl:if test="RV_DEDUCTIBLE='' or RV_DEDUCTIBLE=0">
			<tr>
				<td class="midcolora">Please select deductible.</td>
			</tr>
		</xsl:if>
		<xsl:if test="RV_STATE_REGISTERED=''">
			<tr>
				<td class="midcolora">Please select State Registered.</td>
			</tr>
		</xsl:if>
		<xsl:if test="RV_TYPE=''">
			<tr>
				<td class="midcolora">Please select Vehicle Type.</td>
			</tr>
		</xsl:if>
		<xsl:if test="VEHICLE_MODIFIED=''">
			<tr>
				<td class="midcolora">Please select Has this vehicle been modified.</td>
			</tr>
		</xsl:if>
		<xsl:if test="REC_VEH_TYPE=''">
			<tr>
				<td class="midcolora">Please select Type of Recreational Vehicle.</td>
			</tr>
		</xsl:if>
		<xsl:if test="VEHICLE_MODIFIED_DETAILS=''">
			<tr>
				<td class="midcolora">Please insert Modified Details.</td>
			</tr>
		</xsl:if>
		<!-- Commented by Charles on 10-Dec-09 for Itrack 6841 -->
		<!--xsl:if test="RV_USED_IN_RACE_SPEED=''">
			<tr>
				<td class="midcolora">Please select Used to participate in any race or speed contest.</td>
			</tr>
		</xsl:if-->
		<xsl:if test="USED_IN_RACE_SPEED_CONTEST=''">
			<tr>
				<td class="midcolora">Please insert Recreational Vehicle Contest Description.</td>
			</tr>
		</xsl:if>
	</xsl:template >
	<!-- end ====================================RV Info Messages =================================== -->
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

	<!-- ========================================= Billing Detail ======================================= -->
	<xsl:template name="NET_AMOUNT">
		<xsl:choose>
			<xsl:when test="NET_AMOUNT=''">
				<tr>
					<td class="midcolora">Please generate Installments</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	
	<!-- ================================= Dwelling Info ==================================== -->
	<xsl:template name="DWELLING_NUMBER">
		<xsl:choose>
			<xsl:when test="DWELLING_NUMBER='' or DWELLING_NUMBER=0">
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
	<xsl:template name="BUILDING_TYPE">
		<xsl:choose>
			<xsl:when test="BUILDING_TYPE=''">
				<tr>
					<td class="midcolora">Please select Building type.</td>
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
	<xsl:template name="REPAIRCOST_MARKETVALUE">
		<xsl:choose>
			<xsl:when test="REPAIRCOST_MARKETVALUE=''">
				<tr>
					<td class="midcolora">For HO-2 repair cost or HO-3 repair cost product, Market Value and Repair Cost must be same.</td>
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
	<xsl:template name="YEAR_BUILT">
		<xsl:choose>
			<xsl:when test="YEAR_BUILT='' or YEAR_BUILT=0">
				<tr>
					<td class="midcolora">Please insert Year built.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--22 June 2006-->
	<xsl:template name="DETACHED_OTHER_STRUCTURES">
		<xsl:choose>
			<xsl:when test="DETACHED_OTHER_STRUCTURES=''">
				<tr>
					<td class="midcolora">Please select Do You have any Detached Other Structures?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PREMISES_LOCATION">
		<xsl:choose>
			<xsl:when test="PREMISES_LOCATION=''">
				<tr>
					<td class="midcolora">Please select PREMISES_LOCATION.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--22 June 2006-->
	<xsl:template name="REPLACEMENT_COST">
		<xsl:choose>
			<xsl:when test="REPLACEMENT_COST=''">
				<tr>
					<xsl:choose>
						<!-- HO-4,HO-4 Deluxe, HO-6, HO-6 Deluxe -->
						<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11405 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11407
					     or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11406 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11408
					     or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11195 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11245
					     or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11196 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11246">
							<td class="midcolora">Please insert Contents Insurance Amount.</td>
						</xsl:when>
						<!-- HO-2 Repair Cost,HO-3 Repair Cost -->
						<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11403 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11404
						or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11193  or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11194">
							<td class="midcolora">Please insert Repair Cost.</td>
						</xsl:when>
						<!--<xsl:otherwise>
							<td class="midcolora">Please insert Replacement Cost.</td>
						</xsl:otherwise>-->
					</xsl:choose>
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
	<xsl:template name="NEED_OF_UNITS">
		<xsl:choose>
			<xsl:when test="NEED_OF_UNITS=''">
				<tr>
					<td class="midcolora">Please insert If HO-4/HO-6, #units/apts.</td>
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
	<xsl:template name="DWELL_UNDER_CONSTRUCTION">
		<xsl:choose>
			<xsl:when test="DWELL_UNDER_CONSTRUCTION=''">
				<tr>
					<td class="midcolora">Please select Is the dwelling under construction?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
		<xsl:template name="LOCATION_TEN_HOME">
		<xsl:choose>
			<xsl:when test="LOCATION_TEN_HOME=''">
				<tr>
					<td class="midcolora">Please select Located in a subdivision consisting of at least 10 homes.</td>
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
					<td class="midcolora">Please select primary exterior construction.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ROOF_TYPE">
		<xsl:choose>
			<xsl:when test="ROOF_TYPE=''">
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
				<xsl:choose>
					<xsl:when test="DWELLING_LIMIT_COVA=''">
						<tr>
							<td class="midcolora">Please insert Coverage A.</td>
						</tr>
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<!--Commented on 22 June  2006<xsl:when test="DWELLING_LIMIT=''">
				<xsl:choose><xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11195 and INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11245 and 
		         INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11196 and INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11246 and INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11405  and INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11407">
			<tr>
				<td class="midcolora">Please insert Coverage A.</td>
			</tr>
		</xsl:when>
		<xsl:otherwise>
			<tr>
				<td class="midcolora">Please insert Coverage C.</td>
			</tr>
		</xsl:otherwise>
		</xsl:choose>
			</xsl:when>-->
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PERSONAL_LIAB_LIMIT">
		<xsl:choose>
			<xsl:when test="PERSONAL_LIAB_LIMIT=''">
				<tr>
					<td class="midcolora">Please select Personal Liability Limits.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MED_PAY_EACH_PERSON">
		<xsl:choose>
			<xsl:when test="MED_PAY_EACH_PERSON=''">
				<tr>
					<td class="midcolora">Please select Medical Payments each Person.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="COVA_NOT_REPCOST">
		<xsl:choose>
			<xsl:when test="COVA_NOT_REPCOST=''">
				<tr>
					<td class="midcolora">Coverage A in case of HO-2,HO-3,HO-5 regular and premier cannot be greater than 100% of Replacement Cost.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="COVA_NOT_MAKVALUE">
		<xsl:choose>
			<xsl:when test="COVA_NOT_MAKVALUE=''">
				<tr>
					<td class="midcolora">Coverage A in case of HO-2 and HO-3 Repair Cost must be equal to 100% of Market Value.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Coverage C in case of HO-4,HO-6 Regular and Deluxe cannot be greater than 100% of Replacement Cost. -->
	<xsl:template name="COVC_NOT_REPCOST">
		<xsl:choose>
			<xsl:when test="COVC_NOT_REPCOST=''">
				<tr>
					<td class="midcolora">Coverage C in case of HO-4,HO-6 Regular and Deluxe cannot be greater than 100% of Contents Insurance Amount.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Template for showing Info at the initial stage (Mandatory only) -->
	<xsl:template name="HODETAIL">
		<!-- Application List -->
		<xsl:call-template name="APPLICATION_LIST_MESSAGESGES" />
		<!-- Location Detail -->
		<xsl:call-template name="LOCATIONMESSAGE" />
		<!-- Dwelling Info -->
		<xsl:call-template name="DWELLINGINFOMESSAGE" />
		<!-- Dwelling Rating Info -->
		<!--<xsl:call-template name="DWELLINGRATINGINFOMESSAGE" />-->
		<!-- Coverage/Limt  -->
		<xsl:call-template name="COVERAGELIMITSMESSAGE" />
		<xsl:call-template name="COVERAGELIMITSMESSAGE_SECII" />
		<!--<xsl:call-template name="BILLINGMESSAGE" />-->
		<!--RV Info-->
		<!--xsl:call-template name="RECREATIONALVEHICLEMESSAGE"/-->
		<!--xsl:apply-templates select="INPUTXML/RVEHICLES/RVEHICLE"></xsl:apply-templates-->
		<!-- Gen Info -->
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/IS_RECORD_EXISTS='N'">
				<xsl:apply-templates select="INPUTXML/HOGENINFOS/HOGENINFO" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
				<xsl:call-template name="GENINFOMESSAGE" />
			</xsl:otherwise>
		</xsl:choose>
		<!-- Solid Fuel (This template will be called only if Secondary Heat Type is 'Wood Stove' -->
		<xsl:if test="INPUTXML/DWELLINGS/RATINGINFO/PRIMARY_HEAT_TYPE='Y' or INPUTXML/DWELLINGS/RATINGINFO/SECONDARY_HEAT_TYPE='Y'">
			<xsl:call-template name="SOLIDFUELINFOMESSAGE" />
		</xsl:if>
		<!-- ================= if one of the following screen has been saved =========================-->
		<xsl:if test="INPUTXML/BOATS/@COUNT>0 or INPUTXML/OPERATORS/@COUNT>0 or INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/IS_RECORD_EXISTS='N'">
			<!-- Boat Info -->
			<xsl:if test="INPUTXML/BOATS/@COUNT=0">
				<xsl:call-template name="BOATMESSAGE" />
			</xsl:if>
			<!-- Operator Info -->
			<xsl:if test="INPUTXML/OPERATORS/@COUNT=0">
				<xsl:call-template name="OPERATORMESSAGE" />
			</xsl:if>
			<!-- Gen Info  -->
			<tr>
				<td>
					<xsl:choose>
						<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/@COUNT>0">
							<xsl:apply-templates select="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
							<xsl:call-template name="WCGENINFOMESSAGE" />
						</xsl:otherwise>
					</xsl:choose>
				</td>
			</tr>
		</xsl:if>
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
		<!--<tr>
			<td class="midcolora">Please select Is dwelling currently Vacant or Unoccupied?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Is dwelling currently Vacant or Unoccupied?' is 'Yes' then Please insert Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is dwelling rented in whole or part to students under age 30?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Is dwelling rented in whole or part to students under age 30?' is 'Yes' then  Please insert Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is dwelling owned by anyone other than an individual?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Is dwelling owned by anyone other than an individual?' is 'Yes' then Please insert Description.</td>
		</tr>-->
		<tr>
			<td class="midcolora">Please select Any business conducted on Premises?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any business conducted on Premises?' is 'Yes' then Please insert Business Description.</td>
		</tr>
		<!--<tr>
			<td class="midcolora">Please select Is this a Modular/Manufactured Home?.</td>
		</tr>
		<tr>
			<td class="midcolora">Please Is property located next to a commercial building?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Is property located next to a commercial building?' is 'Yes' then   Please insert Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Are any outside stairways present?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Are any outside stairways present?' is 'Yes' then Please select Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any animal,livestock or pets?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any animal,livestock or pets?' is 'Yes' then Please Other than Breed of Dogs.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Number/breed of dogs.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Number/breed of dogs' is greater than 0 then please select Breed of Dog.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is there an enclosed swimming pool or hot tub (not including bathroom type Jacuzzi or hot tub)?.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Valued Customer Discount Override.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Valued Customer Discount Override' is 'Yes' then Please Insert Valued Customer Discount Override Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Has insurance been transferred within the agency?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Has insurance been transferred within the agency?' is 'Yes' then Please insert Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Has ownership of this dwelling changed more than one time in the last 3 years?.</td>
		</tr>-->
		<tr>
			<td class="midcolora">If 'Has ownership of this dwelling changed more than one time in the last 3 years?' is 'Yes' then Please insert Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any coverage declined, cancelled or non-renewed during the last 3 years?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any coverage declined, cancelled or non-renewed during the last 3 years?' is 'Yes' then Please insert Description.</td>
		</tr>
		<!--<tr>
			<td class="midcolora">Please select Conviction of any degree of arson in past 3 years?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Conviction of any degree of arson in past 3 years?' is 'Yes' then Please insert Description of conviction.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any lead paint hazard?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any lead paint hazard?'is 'Yes' then Please insert Description of lead paint hazard.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is multi-policy discount applied?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Is multi-policy discount applied?' is 'Yes' then Please insert Multi-policy discount description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any residence employees?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any residence employees?' is 'Yes' then Please insert Number of resident employees.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any other residence owned, occupied or rented?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any other residence owned, occupied or rented?' is 'Yes' then Please insert Description other residence.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any other insurance with this company?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any other insurance with this company?' is 'Yes' then Please insert Description other insurance(List policy #s).</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any current renovation/remodeling.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any current renovation/remodeling' is 'Yes' then Please insert Est. completion date and value, if undergoing renovation.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Trampoline?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Trampoline?' is 'Yes' then Please insert Description of trampoline.</td>
		</tr>
		<tr>
			<td class="midcolora"> Please select Roomers or Boarders.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Roomers or Boarders' is 'Yes' then Please insert '# of Roomers and Boarders'.</td>
		</tr>
		<tr>
			<td class="midcolora"> Please select Any supplemental heating source(wood stove,fireplace inserts,etc.)?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any supplemental heating source(wood stove,fireplace inserts,etc.)?' is 'Yes' then Please insert Wood stove supplement.</td>
		</tr>
		<tr>
			<td class="midcolora"> Please select If Building is Under Construction, is the Applicant the General Contractor?.</td>
		</tr>-->
		<!-- For Indiana Only  -->
		<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/STATE_ID=14">
			<!--<tr>
				<td class="midcolora"> Please select Non smoker credit.</td>
			</tr>-->
		</xsl:if>
		<!--<tr>
			<td class="midcolora">Please select Swimming Pool.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any Farming?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any Farming?' is 'Yes' then Please select 'Farming Details' and insert '# of Farming acres'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Farming Details' is 'Off premises operated by insured' or 'Off premises rented to others'
			 then Please insert '# of Locations' and 'Legal Description of Locations'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Farming Details' is 'Off premises Incidental Income Only' then insert 'Legal Description of Locations'.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Any Horses?'</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any Horses?' is 'Yes' then Please insert '# of Horses'.</td>
		</tr>-->
		<tr>
			<td class="midcolora">Please select Any Prior Losses.</td>
		</tr>
		<tr>
			<td class="midcolora">If, 'Any Prior Losses' is 'Yes', then Please insert Prior Losses Description.</td>
		</tr>
		<!--<tr>
			<td class="midcolora">Please select Do you want to add a boat with this Homeowner policy?.</td>
		</tr>-->
	</xsl:template>
	<!-- ======================================= Location Details  ============================ -->
	<xsl:template name="LOCATIONMESSAGE">
			<tr>
			<td class="pageheader">For Location Information:</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Location Number.</td>
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
	<xsl:template name="BILLINGMESSAGE">
		<tr>
			<td class="pageheader">For Billing Information:</td>
		</tr>
		<tr>
			<td class="midcolora">Please generate installments.</td>
		</tr>
	
	</xsl:template>
	<!-- ========================================= Dwelling Info  ========================================= -->
	<xsl:template name="DWELLINGINFOMESSAGE">
		<tr>
			<td class="pageheader">For Dwelling Info:</td>
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
				<!--  HO-2 Repair Cost,HO-3 Repair Cost  -->
				<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11403 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11404
						or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11193  or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11194">
					<td class="midcolora">Please insert Market Value/Repair Cost.</td>
				</xsl:when>
				<xsl:otherwise>
					<td class="midcolora">Please insert Market Value.</td>
				</xsl:otherwise>
			</xsl:choose>
		</tr>
		<!-- <tr>
			<td class="midcolora">Please insert Replacement Cost.</td>
		</tr>-->
		<!--<xsl:choose>REMOVED ON 7 July 2006
			 HO-4,HO-4 Deluxe, HO-6, HO-6 Deluxe
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11405 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11407
					     or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11406 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11408
					     or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11195 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11245
					     or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11196 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11246">
				<td class="midcolora">Please insert Contents Insurance Amount.</td>
			</xsl:when>
			 HO-2 Repair Cost,HO-3 Repair Cost 
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11403 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11404
						or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11193  or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11194">
				<td class="midcolora">Please insert Repair Cost.</td>
			</xsl:when>
			<xsl:otherwise>
				<td class="midcolora">Please insert Replacement Cost.</td>
			</xsl:otherwise>
		</xsl:choose>-->
		<!--Added on 7 July 2006-->
		<tr>
			<xsl:choose>
				<!-- HO-4,HO-4 Deluxe, HO-6, HO-6 Deluxe-->
				<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11405 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11407
					     or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11406 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11408
					     or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11195 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11245
					     or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11196 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE=11246">
					<td class="midcolora">Please insert Contents Insurance Amount.</td>
				</xsl:when>
				<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE!=11403 and INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE!=11404
				and INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE!=11193 and INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE!=11194">
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
		
		<!--<tr>
			<td class="midcolora">Please select Do You have any Detached Other Structures?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Do You have any Detached Other Structures?' is 'Yes' then Please select Location of Premises.</td>
		</tr>-->
	</xsl:template>
	<!-- ====================================== RV Info ======================================= -->	
	<!--xsl:template name="RECREATIONALVEHICLEMESSAGE">
		<tr>
			<td class="pageheader">For Recreational Vehicle Info:</td>
		</tr>
		<tr>
			<td class="modcolora">Please enter year.</td>
		</tr>
		<tr>
			<td class="modcolora">Please enter make</td>
		</tr>
		<tr>
			<td class="modcolora">Please enter model</td>
		</tr>
		<tr>
			<td class="modcolora">Please enter serial #</td>
		</tr>
		<tr>
			<td class="modcolora">Please select registered state</td>
		</tr>
		<tr>
			<td class="modcolora">Please select vehicle type</td>
		</tr>
		<tr>
			<td class="modcolora">Please enter H.P/CC'S</td>
		</tr>
		<tr>
			<td class="modcolora">Please enter insuring value</td>
		</tr>
		<tr>
			<td class="modcolora">Please select deductible</td>
		</tr>
		<tr>
			<td class="modcolora">Please enter year</td>
		</tr>
		<tr>
			<td class="midcolora">Have there been any prior losses either with the vehicles or any other Recreational vehicle	</td>
		</tr>
	</xsl:template-->
	<!-- ====================================== Dwelling Rating Info ======================================= -->
	<xsl:template name="DWELLINGRATINGINFOMESSAGE">
		<tr>
			<td class="pageheader">For Rating Info:</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Distance to the fire station.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert # of Miles from Fire Station.</td>
		</tr>
		<xsl:call-template name="DWELL_UNDER_CONSTRUCTION" />
		<tr>
			<td class="midcolora">Please insert Protection class.</td>
		</tr>
		<tr>
			<td class="midcolora">If selected value of Wiring Update is not 'None' then please insert Wiring update year.</td>
		</tr>
		<tr>
			<td class="midcolora">If selected value of Plumbing Update is not 'None' then please insert Plumbing update year.</td>
		</tr>
		<tr>
			<td class="midcolora">If selected value of Heating Update is not 'None' then please insert Heating update year.</td>
		</tr>
		<tr>
			<td class="midcolora">If selected value of Roofing Update is not 'None' then please insert Roofing update year.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Number of Amps (Elec Sys).</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Circuit Breakers.</td>
		</tr>
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11195 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11196">
		<tr>
			<td class="midcolora">Please insert Number of Families.</td>
		</tr>
		</xsl:when>	
		</xsl:choose>
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE =11195 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE =11196">
				<tr>
					<td class="midcolora">Please insert If HO-4/HO-6, #units/apts.</td>
				</tr>
			</xsl:when>	
		</xsl:choose>
		<tr>
			<td class="midcolora">Please select primary exterior construction.</td>
		</tr>
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE =11409 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE =11148 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE =11149 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE =11194 or INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE =11192">
				<tr>
					<td class="midcolora">Please select Roof Type.</td>
				</tr>
			</xsl:when>	
		</xsl:choose>
		<!--<tr>
			<td class="midcolora">if selected value of Exterior construction is 'Other' then please insert Other Description.</td>
		</tr> -->
		<tr>
			<td class="midcolora">If selected value of Roof Type is 'Other' then please insert Other Description.</td>
		</tr>
		<tr>
			<td class="midcolora">If selected value of Primary Heat Type is 'Other' then please insert Other Description.</td>
		</tr>
		<tr>
			<td class="midcolora">If selected value of Secondary Heat Type is 'Other' then please insert Other Description.</td>
		</tr>
	</xsl:template>
	
	<!-- ====================================== Coverage/Limit ======================================= -->
	<xsl:template name="COVERAGELIMITSMESSAGE">
		<tr>
			<td class="pageheader">For Coverage Section1 Information:</td>
		</tr>
		<!-- HO4/HO6, HO4 Deluxe/HO6 Deluxe
               coverage C is mandotary
               <option value="11406">HO-6</option>
			<option selected="selected" value="11408">HO-6 Deluxe</option>
         -->
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11195 and INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11245 and 
			INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11196 and INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11406 and INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11246 and INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11405  and INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11407">
				<tr>
					<td class="midcolora">Please insert Coverage A.</td>
				</tr>
			</xsl:when>
			<xsl:otherwise>
				<tr>
					<td class="midcolora">Please insert Coverage C.</td>
				</tr>
			</xsl:otherwise>
		</xsl:choose>
		<!--<tr>
			<td class="midcolora">Please select Personal Liability Limits.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Medical Payments each Person.</td>
		</tr>-->
	</xsl:template>
	<xsl:template name="COVERAGELIMITSMESSAGE_SECII">
		<tr>
			<td class="pageheader">For Coverage Section2 Information:</td>
		</tr>
		<!-- HO4/HO6, HO4 Deluxe/HO6 Deluxe
               coverage C is mandotary
               <option value="11406">HO-6</option>
			<option selected="selected" value="11408">HO-6 Deluxe</option>
        
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11195 and INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11245 and 
		INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11196 and INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11406 and INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11246 and INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11405  and INPUTXML/APPLICATIONS/APPLICATION/POLICY_TYPE !=11407">
				<tr>
					<td class="midcolora">Please insert Coverage A.</td>
				</tr>
			</xsl:when>
			<xsl:otherwise>
				<tr>
					<td class="midcolora">Please insert Coverage C.</td>
				</tr>
			</xsl:otherwise>
		</xsl:choose> -->
		<tr>
			<td class="midcolora">Please select Personal Liability Limits.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Medical Payments each Person.</td>
		</tr>
	</xsl:template>
	
	
	<!-- ====================================== Solid Fuel Info ======================================= -->
	<xsl:template name="SOLIDFUELINFOMESSAGE">
		<tr>
			<td class="pageheader">For Solid Fuel Details:</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Manufacturer.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Brand Name.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Model Number.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Fuel.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Stove Type.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Does the unit have a testing laboratory label (UL, other).</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is the unit.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Is the unit' is 'Other(Describe)' then Please insert Other Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Location.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Location' is 'Other(Describe)' then Please insert Other Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Construction.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Year Device Installed.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Installation was inspected by.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Installation was inspected by' is 'Other(Describe)' then Please insert Other Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Was installation done by a professional installer such as a contractor?.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select What other type of heating source is used?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'What other type of heating source is used?' is 'Other(Describe)' then Please insert Other Description.</td>
		</tr>
	</xsl:template>
	<!-- =============================== For Watercraft (Start)========================================= -->
	<!-- This template shows the input detail of Watercraft-->
	<xsl:template name="SHOWWCINPUTDETAIL">
		<xsl:apply-templates select="INPUTXML/MVRS/MVR"></xsl:apply-templates>
		<!-- BOAT -->
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/@COUNT>0">
				<xsl:for-each select="INPUTXML/BOATS">
					<xsl:apply-templates select="BOAT"></xsl:apply-templates>
				</xsl:for-each>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
				<xsl:call-template name="BOATMESSAGE" />
			</xsl:otherwise>
		</xsl:choose>
		<!-- OPERATOR-->
		<xsl:choose>
			<xsl:when test="INPUTXML/OPERATORS/@COUNT>0">
				<xsl:for-each select="INPUTXML/OPERATORS">
					<xsl:apply-templates select="OPERATOR"></xsl:apply-templates>
				</xsl:for-each>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
				<xsl:call-template name="OPERATORMESSAGE" />
			</xsl:otherwise>
		</xsl:choose>
		<!-- Trailers Info -->
		<!-- <xsl:call-template name="TRAILER_INFO" /> --> <!-- Commented by Charles on 28-Oct-09 for Itrack 6656  -->
		<!-- Gen Info  -->
		<xsl:choose>
			<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/@COUNT>0">
				<xsl:apply-templates select="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
				<xsl:call-template name="WCGENINFOMESSAGE" />
			</xsl:otherwise>
		</xsl:choose>
		<!--EQUIPMENT MAY 29-->
		<xsl:apply-templates select="INPUTXML/WATERCRAFTEQUIPMENTS/WATERCRAFTEQUIPMENT"></xsl:apply-templates>
		<!--EQUIPMENT-->
	</xsl:template>
	<!-- Trailer Info -->
	<xsl:template name="TRAILER_INFO">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/TRAILER_ASSOCIATED_BOAT=''">
				<tr>
					<td class="pageheader">For Trailer Information: </td>
				</tr>
				<tr>
					<td class="midcolora">You must have trailer(s) attached to a boat.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--Template BOAT-->
	<xsl:template match="BOAT">
		<xsl:choose>
			<xsl:when test="BOAT_NO='' or BOAT_YEAR=''or MODEL='' or LENGTH=''or TYPE_OF_WATERCRAFT=''or  
			 MAKE=''or  HULL_ID_NO=''or STATE_REG=''or HULL_MATERIAL='' or WATER_NAVIGATED='' or MAX_SPEED=''
			 or TERRITORY='' or COV_TYPE_BASIS ='' or INSURING_VALUE='' or LOCATION_ADDRESS='' or LOCATION_CITY='' or LOCATION_STATE='' or LOCATION_ZIP=''">
				<tr>
					<td class="pageheader">For Boat Information: </td>
					<tr>
						<td>
							<table width="60%">
								<tr>
									<xsl:if test="BOAT_NO != ''">
										<td class="pageheader" width="18%">Boat No:</td>
										<td class="midcolora" width="36%">
											<xsl:value-of select="BOAT_NO" />
										</td>
									</xsl:if>
									<xsl:if test="BOAT_YEAR != ''">
										<td class="pageheader" width="18%">Boat Year:</td>
										<td class="midcolora" width="36%">
											<xsl:value-of select="BOAT_YEAR" />
										</td>
									</xsl:if>
								</tr>
								<tr>
									<xsl:if test="MODEL != ''">
										<td class="pageheader" width="18%">Model:</td>
										<td class="midcolora" width="36%">
											<xsl:value-of select="MODEL" />
										</td>
									</xsl:if>
									<xsl:if test="MAKE != ''">
										<td class="pageheader" width="18%">Make:</td>
										<td class="midcolora" width="36%">
											<xsl:value-of select="MAKE" />
										</td>
									</xsl:if>
								</tr>
							</table>
						</td>
					</tr>
				</tr>
				<xsl:call-template name="BOAT_NO" />
				<xsl:call-template name="MAX_SPEED" />
				<xsl:call-template name="TYPE_OF_WATERCRAFT" />
				<xsl:call-template name="BOAT_YEAR" />
				<xsl:call-template name="MAKE" />
				<xsl:call-template name="MODEL" />
				<xsl:call-template name="HULL_ID_NO" />
				<xsl:call-template name="LENGTH" />
				<xsl:call-template name="STATE_REG" />
				<xsl:call-template name="HULL_MATERIAL" />
				<xsl:call-template name="TERRITORY" />
				<xsl:call-template name="COV_TYPE_BASIS" />
				<xsl:call-template name="INSURING_VALUE" />
				<xsl:call-template name="LOCATION_ADDRESS" />
				<xsl:call-template name="LOCATION_CITY" />
				<xsl:call-template name="LOCATION_STATE" />
				<xsl:call-template name="LOCATION_ZIP" />
				<xsl:call-template name="WATER_NAVIGATED" />
				<xsl:if test="user:AssignUnderwritter(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
		<!-- </table>	-->
	</xsl:template>
	<!--Template Operator-->
	<xsl:template match="OPERATOR">
		<xsl:choose>
			<xsl:when test="DRIVER_FNAME=''or DRIVER_LNAME=''or DRIVER_CODE=''or  DRIVER_DRIV_LIC=''
			 or DRIVER_SEX='' or DRIVER_SEX='0' or DRIVER_STATE=''or DRIVER_ZIP='' or  DRIVER_DOB=''or DRIVER_LIC_STATE='' or 
			 DRIVER_LIC_STATE='' or VEHICLE_ID='0' or VEHICLE_ID='' or PRIN_OCC_ID='0' or PRIN_OCC_ID='' or DEACTIVATEBOAT='Y' or 
			 DRIVER_ADD1='' or DRIVER_CITY='' or DRIVER_COST_GAURAD_AUX=''"><!--DRIVER_COST_GAURAD_AUX,Charles,20-Nov-09,Itrack 6743-->
				<tr>
					<td class="pageheader">For Operator/Household Members Information:</td>
					<tr>
						<td>
							<table width="60%">
								<tr>
									<xsl:if test="DRIVER_NAME != ''">
										<td class="pageheader" width="18%">Name:</td>
										<td class="midcolora" width="36%">
											<xsl:value-of select="DRIVER_NAME" />
										</td>
									</xsl:if>
									<xsl:if test="DRIVER_CODE != ''">
										<td class="pageheader" width="18%">Code:</td>
										<td class="midcolora" width="36%">
											<xsl:value-of select="DRIVER_CODE" />
										</td>
									</xsl:if>
								</tr>
								<tr>
									<xsl:if test="DRIVER_DRIV_LIC != ''">
										<td class="pageheader" width="18%">License Number:</td>
										<td class="midcolora" width="36%">
											<xsl:value-of select="DRIVER_DRIV_LIC" />
										</td>
									</xsl:if>
									<xsl:if test="DRIVER_SEX != ''">
										<td class="pageheader" width="18%">Sex:</td>
										<td class="midcolora" width="36%">
											<xsl:value-of select="DRIVER_SEX" />
										</td>
									</xsl:if>
								</tr>
							</table>
						</td>
					</tr>
				</tr>
				<xsl:call-template name="DRIVER_FNAME" />
				<xsl:call-template name="DRIVER_LNAME" />
				<xsl:call-template name="DRIVER_CODE" />
				<xsl:call-template name="DRIVER_ADD1" />
				<xsl:call-template name="DRIVER_CITY" />
				<xsl:call-template name="DRIVER_STATE" />
				<xsl:call-template name="DRIVER_ZIP" />
				<xsl:call-template name="DRIVER_DOB" />
				<xsl:call-template name="DRIVER_SEX" />
				<xsl:call-template name="DRIVER_DRIV_LIC" />
				<xsl:call-template name="DRIVER_LIC_STATE" />
				<xsl:call-template name="VEHICLE_ID" />
				<xsl:call-template name="PRIN_OCC_ID" />
				<xsl:call-template name="DEACTIVATEBOAT" />				
				<xsl:call-template name="DRIVER_COST_GAURAD_AUX" /><!-- Added by Charles on 20-Nov-09 for Itrack 6743 -->
				<xsl:if test="user:AssignUnderwritter(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
		<!-- </table>	-->
	</xsl:template>
	<!-- ============================= MVR INFORMATION       ======================== -->
	<xsl:template match="INPUTXML/MVRS/MVR">
		<xsl:choose>
			<xsl:when test="MVR_VIOLATION_ID='' or MVR_VIOLATION_ID=0 or CONVICTION_DATE='' or OCCURENCE_DATE=''">
				<tr>
					<td class="pageheader">For MVR Information:</td>
				</tr>
				<tr>
					<td class="midcolora">Driver :
						<xsl:value-of select="DRIVER_NAME" /> 
					</td>
				</tr>
				<xsl:call-template name="MVR_VIOLATION_ID" />
				<xsl:call-template name="CONVICTION_DATE" />
				<xsl:call-template name="OCCURENCE_DATE" />
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
	<!--=============== End MVR Information =================-->
	<!--Template Watercraft General Info-->
	<xsl:template match="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO">
		<xsl:choose>
			<xsl:when test="PHY_MENTL_CHALLENGED=''or PHY_MENTL_CHALLENGED_DESC='' or DRIVER_SUS_REVOKED=''or DRIVER_SUS_REVOKED_DESC=''
    	or IS_CONVICTED_ACCIDENT=''or IS_CONVICTED_ACCIDENT_DESC='' or DRINK_DRUG_VOILATION='' or 
    	 MINOR_VIOLATION=''or OTHER_POLICY_NUMBER_LIST='' or ANY_LOSS_THREE_YEARS='' or ANY_LOSS_THREE_YEARS_DESC='' or 
    	 COVERAGE_DECLINED='' or COVERAGE_DECLINED_DESC='' 
    	 or IS_RENTED_OTHERS='' or IS_RENTED_OTHERS_DESC='' or IS_REGISTERED_OTHERS='' or PARTICIPATE_RACE='' or CARRY_PASSENGER_FOR_CHARGE=''
    		or IS_BOAT_COOWNED='' or IS_BOAT_COOWNED_DESC=''
    		or ANY_BOAT_AMPHIBIOUS='' or ANY_BOAT_AMPHIBIOUS_DESC=''
    		or MULTI_POLICY_DISC_APPLIED='' or MULTI_POLICY_DISC_APPLIED_DESC='' or ANY_BOAT_RESIDENCE='' or ANY_BOAT_RESIDENCE_DESC=''
    		or IS_BOAT_USED_IN_ANY_WATER='' or IS_BOAT_USED_IN_ANY_WATER_DESC=''">
				<tr>
					<td class="pageheader">For Watercraft's Underwriting Questions:</td>
				</tr>
				<!-- <xsl:call-template name="HAS_CURR_ADD_THREE_YEARS" /> -->
				<!-- <xsl:call-template name="HAS_CURR_ADD_THREE_YEARS_DESC" /> -->
				<xsl:call-template name="PHY_MENTL_CHALLENGED" />
				<xsl:call-template name="PHY_MENTL_CHALLENGED_DESC" />
				<xsl:call-template name="DRIVER_SUS_REVOKED" />
				<xsl:call-template name="DRIVER_SUS_REVOKED_DESC" />
				<xsl:call-template name="IS_CONVICTED_ACCIDENT" />
				<xsl:call-template name="IS_CONVICTED_ACCIDENT_DESC" />
				<xsl:call-template name="DRINK_DRUG_VOILATION" />
				<xsl:call-template name="MINOR_VIOLATION" />
				<!--<xsl:call-template name="ANY_OTH_INSU_COMP" />-->
				<xsl:call-template name="OTHER_POLICY_NUMBER_LIST" />
				<xsl:call-template name="ANY_LOSS_THREE_YEARS" />
				<xsl:call-template name="ANY_LOSS_THREE_YEARS_DESC" />
				<xsl:call-template name="COVERAGE_DECLINED" />
				<xsl:call-template name="COVERAGE_DECLINED_DESC" />
				<!-- <xsl:call-template name="IS_CREDIT" /> -->
				<!-- <xsl:call-template name="CREDIT_DETAILS" /> -->
				<xsl:call-template name="IS_RENTED_OTHERS" />
				<xsl:call-template name="IS_RENTED_OTHERS_DESC" />
				<xsl:call-template name="IS_REGISTERED_OTHERS" />
				<xsl:call-template name="PARTICIPATE_RACE" />
				<xsl:call-template name="CARRY_PASSENGER_FOR_CHARGE" />
				<!--May 05-->
				<xsl:call-template name="IS_BOAT_COOWNED" />
				<xsl:call-template name="IS_BOAT_COOWNED_DESC" />
				<xsl:call-template name="ANY_BOAT_AMPHIBIOUS" />
				<xsl:call-template name="ANY_BOAT_AMPHIBIOUS_DESC" />
				<xsl:call-template name="MULTI_POLICY_DISC_APPLIED_BOAT" />
				<xsl:call-template name="MULTI_POLICY_DISC_APPLIED_DESC" />
				<!--End May 05-->
				<!--May 11 06 Boat residence-->
				<xsl:call-template name="ANY_BOAT_RESIDENCE" />
				<xsl:call-template name="ANY_BOAT_RESIDENCE_DESC" />
				<!--May 11 06 Boat residence-->
				<!--Sep 21 07 Boat Boat used in any water description-->
				<xsl:call-template name="IS_BOAT_USED_IN_ANY_WATER" />
				<xsl:call-template name="IS_BOAT_USED_IN_ANY_WATER_DESC" />
				<!--Sep 21 07 Boat Boat used in any water description-->
				<xsl:if test="user:AssignUnderwritter(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Template for the Equipment Type MAY 29-->
	<xsl:template match="INPUTXML/WATERCRAFTEQUIPMENTS/WATERCRAFTEQUIPMENT">
		<xsl:choose>
			<xsl:when test="DESCRIPTION='' or EQUIPMENT_TYPE='' or OTHER_DESCRIPTION_EQUIP_TYPE=''">
				<tr>
					<td class="pageheader">For Equipment Information: </td>
					<tr>
						<td>
							<table width="60%">
								<tr>
									<xsl:if test="EQUIP_NO != ''">
										<td class="pageheader" width="18%">Equipment No:</td>
										<td class="midcolora" width="36%">
											<xsl:value-of select="EQUIP_NO" />
										</td>
									</xsl:if>
								</tr>
								<tr>
									<xsl:if test="EQUIP_TYPE != ''">
										<td class="pageheader" width="18%">Equipment Type:</td>
										<td class="midcolora" width="36%">
											<xsl:value-of select="EQUIP_TYPE" />
										</td>
									</xsl:if>
								</tr>
							</table>
						</td>
					</tr>
				</tr>
				<xsl:call-template name="DESCRIPTION" />
				<xsl:call-template name="EQUIPMENT_TYPE" />
				<xsl:call-template name="OTHER_DESCRIPTION_EQUIP_TYPE" />
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
	<!--Template for the Equipment Type MAY 29-->
	<!--*********************************TEMPLATES************************************************************  -->
	<!--   Screen Watercraft Rating Info    -->
	<xsl:template name="BOAT_NO">
		<xsl:choose>
			<xsl:when test="BOAT_NO=''">
				<tr>
					<td class="midcolora">Please insert Boat Number.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   13   -->
	<!--   Screen Watercraft Rating Info    -->
	<xsl:template name="BOAT_YEAR">
		<xsl:choose>
			<xsl:when test="BOAT_YEAR=''">
				<tr>
					<td class="midcolora">Please insert Boat Year.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MAX_SPEED">
		<xsl:choose>
			<xsl:when test="MAX_SPEED=''">
				<tr>
					<td class="midcolora">Please insert Maximum Speed.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   14   -->
	<!--   Screen Watercraft Rating Info    -->
	<xsl:template name="MODEL">
		<xsl:choose>
			<xsl:when test="MODEL=''">
				<tr>
					<td class="midcolora">Please insert Model.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   15   -->
	<!--   Screen Watercraft Rating Info    -->
	<xsl:template name="LENGTH">
		<xsl:choose>
			<xsl:when test="LENGTH=''">
				<tr>
					<td class="midcolora">Please insert Length.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   16   -->
	<!--   Screen Watercraft Rating Info    -->
	<xsl:template name="TYPE_OF_WATERCRAFT">
		<xsl:choose>
			<xsl:when test="TYPE_OF_WATERCRAFT=''">
				<tr>
					<td class="midcolora">Please select Type of Watercraft.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   17   -->
	<!--   Screen Watercraft Rating Info    -->
	<xsl:template name="MAKE">
		<xsl:choose>
			<xsl:when test="MAKE=''">
				<tr>
					<td class="midcolora">Please insert Make.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   18   -->
	<!--   Screen Watercraft Rating Info    -->
	<xsl:template name="HULL_ID_NO">
		<xsl:choose>
			<xsl:when test="HULL_ID_NO=''">
				<tr>
					<td class="midcolora">Please insert Serial #.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   19   -->
	<!--   Screen Watercraft Rating Info    -->
	<xsl:template name="STATE_REG">
		<xsl:choose>
			<xsl:when test="STATE_REG=''">
				<tr>
					<td class="midcolora">Please select State Registered.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   20   -->
	<!--   Screen Watercraft Rating Info    -->
	<xsl:template name="HULL_MATERIAL">
		<xsl:choose>
			<xsl:when test="HULL_MATERIAL=''">
				<tr>
					<td class="midcolora">Please select Hull Material.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="INSURING_VALUE">
		<xsl:choose>
			<xsl:when test="INSURING_VALUE=''">
				<tr>
					<td class="midcolora">Please insert Insuring Value.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="TERRITORY">
		<xsl:choose>
			<xsl:when test="TERRITORY=''">
				<tr>
					<td class="midcolora">Please select Territory Docked In.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="COV_TYPE_BASIS">
		<xsl:choose>
			<xsl:when test="COV_TYPE_BASIS=''">
				<tr>
					<td class="midcolora">Please select Coverage Type Basis.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="WATER_NAVIGATED">
		<xsl:choose>
			<xsl:when test="WATER_NAVIGATED=''">
				<tr>
					<td class="midcolora">Please select Waters Navigated.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--LOCATION INFO ADDED ON 22 MAY 2006-->
	<xsl:template name="LOCATION_ADDRESS">
		<xsl:choose>
			<xsl:when test="LOCATION_ADDRESS=''">
				<tr>
					<td class="midcolora">Please insert Location Address.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="LOCATION_CITY">
		<xsl:choose>
			<xsl:when test="LOCATION_CITY=''">
				<tr>
					<td class="midcolora">Please insert Location City.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="LOCATION_STATE">
		<xsl:choose>
			<xsl:when test="LOCATION_STATE=''">
				<tr>
					<td class="midcolora">Please select Location State.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="LOCATION_ZIP">
		<xsl:choose>
			<xsl:when test="LOCATION_ZIP=''">
				<tr>
					<td class="midcolora">Please insert Location Zip.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   21   --> <!--   Screen Operator Information   -->
	<xsl:template name="DRIVER_FNAME">
		<xsl:choose>
			<xsl:when test="DRIVER_FNAME=''">
				<tr>
					<td class="midcolora">Please insert First Name.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   22   --> <!--   Screen Operator Information   -->
	<xsl:template name="DRIVER_LNAME">
		<xsl:choose>
			<xsl:when test="DRIVER_LNAME=''">
				<tr>
					<td class="midcolora">Please insert Last Name.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   23   --> <!--   Screen Operator Information   -->
	<xsl:template name="DRIVER_CODE">
		<xsl:choose>
			<xsl:when test="DRIVER_CODE=''">
				<tr>
					<td class="midcolora">Please insert Code.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   24   --> <!--   Screen Operator Information   -->
	<xsl:template name="DRIVER_STATE">
		<xsl:choose>
			<xsl:when test="DRIVER_STATE=''">
				<tr>
					<td class="midcolora">Please select State.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DRIVER_ADD1">
		<xsl:choose>
			<xsl:when test="DRIVER_ADD1=''">
				<tr>
					<td class="midcolora">Please insert Address 1.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DRIVER_CITY">
		<xsl:choose>
			<xsl:when test="DRIVER_CITY=''">
				<tr>
					<td class="midcolora">Please insert city.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   25   --> <!--   Screen Operator Information   -->
	<xsl:template name="DRIVER_ZIP">
		<xsl:choose>
			<xsl:when test="DRIVER_ZIP=''">
				<tr>
					<td class="midcolora">Please insert Zip.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   26   --> <!--   Screen Operator Information   -->
	<xsl:template name="DRIVER_SEX">
		<xsl:choose>
			<xsl:when test="DRIVER_SEX='' or DRIVER_SEX='0'">
				<tr>
					<td class="midcolora">Please select Gender.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   27   --> <!--   Screen Operator Information   -->
	<xsl:template name="DRIVER_DRIV_LIC">
		<xsl:choose>
			<xsl:when test="DRIVER_DRIV_LIC=''">
				<tr>
					<td class="midcolora">Please insert License #.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   28--> <!--   Screen Operator Information   -->
	<xsl:template name="DRIVER_LIC_STATE">
		<xsl:choose>
			<xsl:when test="DRIVER_LIC_STATE=''">
				<tr>
					<td class="midcolora">Please select License State.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   29   --> <!--   Screen Operator Information   -->
	<xsl:template name="VEHICLE_ID">
		<xsl:choose>
			<xsl:when test="VEHICLE_ID='' or  VEHICLE_ID='0'">
				<tr>
					<td class="midcolora">Please assign boat to Operator.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- 30 -->
	<xsl:template name="DEACTIVATEBOAT">
		<xsl:choose>
			<xsl:when test="DEACTIVATEBOAT='Y'">
				<tr>
					<td class="midcolora">Please select activated boat only in Assigned Boats.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Added by Charles on 20-Nov-09 for Itrack 6743 -->
	<xsl:template name="DRIVER_COST_GAURAD_AUX">
		<xsl:choose>
			<xsl:when test="DRIVER_COST_GAURAD_AUX=''">
				<tr>
					<td class="midcolora">Please insert Boating Experience Since.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Added till here -->
	<!-- Driver DOB -->
	<xsl:template name="DRIVER_DOB">
		<xsl:choose>
			<xsl:when test="DRIVER_DOB=''">
				<tr>
					<td class="midcolora">Please insert Date of Birth.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   32   --> <!--   Screen WC General Information Details   -->
	<xsl:template name="PHY_MENTL_CHALLENGED">
		<xsl:choose>
			<xsl:when test="PHY_MENTL_CHALLENGED=''">
				<tr>
					<td class="midcolora">Please insert Any operator physically or mentally impaired?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   33   --> <!--   Screen WC General Information Details   -->
	<xsl:template name="PHY_MENTL_CHALLENGED_DESC">
		<xsl:choose>
			<xsl:when test="PHY_MENTL_CHALLENGED_DESC=''">
				<tr>
					<td class="midcolora">Please insert Driver Impairment Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   34   --> <!--   Screen WC General Information Details   -->
	<xsl:template name="DRIVER_SUS_REVOKED">
		<xsl:choose>
			<xsl:when test="DRIVER_SUS_REVOKED=''">
				<tr>
					<td class="midcolora">Please select Anyone had a license suspended, restricted or revoked in the last 5 years?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   35   --> <!--   Screen WC General Information Details   -->
	<xsl:template name="DRIVER_SUS_REVOKED_DESC">
		<xsl:choose>
			<xsl:when test="DRIVER_SUS_REVOKED_DESC=''">
				<tr>
					<td class="midcolora">Please insert Driver License Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   36   --> <!--   Screen WC General Information Details   -->
	<xsl:template name="IS_CONVICTED_ACCIDENT">
		<xsl:choose>
			<xsl:when test="IS_CONVICTED_ACCIDENT=''">
				<tr>
					<td class="midcolora">Please select Any operator convicted of a major violation in the last 5 years?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   37   --> <!--   Screen WC General Information Details   -->
	<xsl:template name="IS_CONVICTED_ACCIDENT_DESC">
		<xsl:choose>
			<xsl:when test="IS_CONVICTED_ACCIDENT_DESC=''">
				<tr>
					<td class="midcolora">Please insert Violation Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   38   --> <!--   Screen WC General Information Details   -->
	<xsl:template name="DRINK_DRUG_VOILATION">
		<xsl:choose>
			<xsl:when test="DRINK_DRUG_VOILATION=''">
				<tr>
					<td class="midcolora">Please select Anyone had a drinking or drug related violation in last 5 years?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   39   --> <!--   Screen WC General Information Details   -->
	<xsl:template name="MINOR_VIOLATION">
		<xsl:choose>
			<xsl:when test="MINOR_VIOLATION=''">
				<tr>
					<td class="midcolora">Please select Any operator convicted of a minor violation in the last 3 years?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   40   --> <!--   Screen WC General Information Details   -->
	<!-->
	<xsl:template name="ANY_OTH_INSU_COMP">
		<xsl:choose>
			<xsl:when test="ANY_OTH_INSU_COMP=''">
				<tr>
					<td class="midcolora">Please select Any other insurance with this company?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	-->
	<!--   41   --> <!--   Screen WC General Information Details   -->
	<xsl:template name="OTHER_POLICY_NUMBER_LIST">
		<xsl:choose>
			<xsl:when test="OTHER_POLICY_NUMBER_LIST=''">
				<tr>
					<td class="midcolora">Please insert Other Policy Details(Provide operator, insurance co, vehicle # and policy #).</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   42   --> <!--   Screen WC General Information Details   -->
	<xsl:template name="ANY_LOSS_THREE_YEARS">
		<xsl:choose>
			<xsl:when test="ANY_LOSS_THREE_YEARS=''">
				<tr>
					<td class="midcolora">Please select Anyone had any losses with this or other vessels?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   43   --> <!--   Screen WC General Information Details   -->
	<xsl:template name="ANY_LOSS_THREE_YEARS_DESC">
		<xsl:choose>
			<xsl:when test="ANY_LOSS_THREE_YEARS_DESC=''">
				<tr>
					<td class="midcolora">Please insert Loss Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   44   --> <!--   Screen WC General Information Details   -->
	<xsl:template name="COVERAGE_DECLINED">
		<xsl:choose>
			<xsl:when test="COVERAGE_DECLINED=''">
				<tr>
					<td class="midcolora">Please select Has applicant had any watercraft coverage declined, cancelled, or non-renewed in the last 12 months by any insurer.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   45   --> <!--   Screen WC General Information Details   -->
	<xsl:template name="COVERAGE_DECLINED_DESC">
		<xsl:choose>
			<xsl:when test="COVERAGE_DECLINED_DESC=''">
				<tr>
					<td class="midcolora">Please insert Coverage Declined Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   48   --> <!--   Screen WC General Information Details   -->
	<xsl:template name="IS_RENTED_OTHERS">
		<xsl:choose>
			<xsl:when test="IS_RENTED_OTHERS=''">
				<tr>
					<td class="midcolora">Please select Is any boat rented to others?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   49   --> <!--   Screen WC General Information Details   -->
	<xsl:template name="IS_RENTED_OTHERS_DESC">
		<xsl:choose>
			<xsl:when test="IS_RENTED_OTHERS_DESC=''">
				<tr>
					<td class="midcolora">Please insert Rented Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   50   --> <!--   Screen WC General Information Details   -->
	<xsl:template name="IS_REGISTERED_OTHERS">
		<xsl:choose>
			<xsl:when test="IS_REGISTERED_OTHERS=''">
				<tr>
					<td class="midcolora">Please select Is any boat registered to anyone other than applicant?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   51   --> <!--   Screen WC General Information Details   -->
	<xsl:template name="PARTICIPATE_RACE">
		<xsl:choose>
			<xsl:when test="PARTICIPATE_RACE=''">
				<tr>
					<td class="midcolora">Please select Is any boat used to participate in any race,speed, or fishing contest other than a Sailboat under 26ft?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   52   --> <!--   Screen WC General Information Details   -->
	<xsl:template name="CARRY_PASSENGER_FOR_CHARGE">
		<xsl:choose>
			<xsl:when test="CARRY_PASSENGER_FOR_CHARGE=''">
				<tr>
					<td class="midcolora">Please select Is any boat used to carry passengers for a charge?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   54   --> <!--   Screen Operator Information   -->
	<xsl:template name="PRIN_OCC_ID">
		<xsl:choose>
			<xsl:when test="PRIN_OCC_ID='0' or PRIN_OCC_ID=''">
				<tr>
					<td class="midcolora">Please select Principal/Occasional operator.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Added by Charles on 19-Nov-09 for Itrack 6737 -->
	<xsl:template name="REC_VEHICLE_PRIN_OCC_ID">
		<xsl:choose>
			<xsl:when test="REC_VEHICLE_PRIN_OCC_ID=''">
				<tr>
					<td class="midcolora">Please assign Principal/Occasional operator for Recreational Vehicle in Operator/Household Members page.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Added till here -->
	<!--MAY 06 2006-->
	<xsl:template name="IS_BOAT_COOWNED">
		<xsl:choose>
			<xsl:when test="IS_BOAT_COOWNED=''">
				<tr>
					<td class="midcolora">Please select Is Boat Co-owned?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_BOAT_COOWNED_DESC">
		<xsl:choose>
			<xsl:when test="IS_BOAT_COOWNED_DESC=''">
				<tr>
					<td class="midcolora">Please insert Is Boat Co-owned Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--May 05-->
	<!--Tempalte for BOAT AMPHI-->
	<xsl:template name="ANY_BOAT_AMPHIBIOUS">
		<xsl:choose>
			<xsl:when test="ANY_BOAT_AMPHIBIOUS=''">
				<tr>
					<td class="midcolora">Please select Are any of the boats Hydrofoils, Swamp Buggies, Collapsible, Experimental, Converted Military Craft Single Passenger or Amphibious?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ANY_BOAT_AMPHIBIOUS_DESC">
		<xsl:choose>
			<xsl:when test="ANY_BOAT_AMPHIBIOUS_DESC=''">
				<tr>
					<td class="midcolora">Please insert Boat Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MULTI_POLICY_DISC_APPLIED_BOAT">
		<xsl:choose>
			<xsl:when test="MULTI_POLICY_DISC_APPLIED=''">
				<tr>
					<td class="midcolora">Please select Is multi-policy discount applied?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MULTI_POLICY_DISC_APPLIED_DESC">
		<xsl:choose>
			<xsl:when test="MULTI_POLICY_DISC_APPLIED_DESC=''">
				<tr>
					<td class="midcolora">Please insert Multi-policy Discount Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--MAY 06 2006-->
	<!--Boat Residence May 11 06-->
	<xsl:template name="ANY_BOAT_RESIDENCE">
		<xsl:choose>
			<xsl:when test="ANY_BOAT_RESIDENCE=''">
				<tr>
					<td class="midcolora">Please select Is any boat used as a residence?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ANY_BOAT_RESIDENCE_DESC">
		<xsl:choose>
			<xsl:when test="ANY_BOAT_RESIDENCE_DESC=''">
				<tr>
					<td class="midcolora">Please insert Boat Residence Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--Boat Residence May 11 06-->
	<!--MAY 29 Equipment Tempaltes-->
	<xsl:template name="EQUIPMENT_TYPE">
		<xsl:choose>
			<xsl:when test="EQUIPMENT_TYPE=''">
				<tr>
					<td class="midcolora">Please select Equipment Type.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DESCRIPTION">
		<xsl:choose>
			<xsl:when test="DESCRIPTION=''">
				<tr>
					<td class="midcolora">Please select Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>	
	<xsl:template name="OTHER_DESCRIPTION_EQUIP_TYPE">
		<xsl:choose>
			<xsl:when test="OTHER_DESCRIPTION_EQUIP_TYPE=''">
				<tr>
					<td class="midcolora">Please enter other Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--Sep 21 07-->
	<!--Boat state other than indiana , michigan,wisconsin 21 Sep 2007-->
	<xsl:template name="IS_BOAT_USED_IN_ANY_WATER">
		<xsl:choose>
			<xsl:when test="IS_BOAT_USED_IN_ANY_WATER=''">
				<tr>
					<td class="midcolora">Please select Is any boat used in any water other than Indiana, Michigan or Wisconsin?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_BOAT_USED_IN_ANY_WATER_DESC">
		<xsl:choose>
			<xsl:when test="IS_BOAT_USED_IN_ANY_WATER_DESC=''">
				<tr>
					<td class="midcolora">Please insert Boat used in any water Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- ================================ MVR Info ====================================================== -->	
	<xsl:template name="MVR_VIOLATION_ID">
		<xsl:choose>
			<xsl:when test="MVR_VIOLATION_ID='' or MVR_VIOLATION_ID=0">
				<tr>
					<td class="midcolora">Please select Violation.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="CONVICTION_DATE">
		<xsl:choose>
			<xsl:when test="CONVICTION_DATE='' or CONVICTION_DATE='null'">
				<tr>
					<td class="midcolora">Please insert Conviction Date.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="OCCURENCE_DATE">
		<xsl:choose>
			<xsl:when test="OCCURENCE_DATE='' or OCCURENCE_DATE='null'">
				<tr>
					<td class="midcolora">Please insert Occurrence Date.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- End MVR INFORMATION --> 
	<!--Sep 21 07-->
	<!--MAY 29 Equipment-->
	<!-- Template for showing Info at the initial stage - Added on 08 Dec 2005-->
	<xsl:template name="WCDETAIL">
		<!-- Boat -->
		<xsl:call-template name="BOATMESSAGE" />
		<!-- Trailers Info -->
		<xsl:call-template name="TRAILER_INFO" />
		<!-- Additional Interest -->
		<!-- <xsl:call-template name="ADDITIONALINTMESSAGE" /> -->
		<!--Operator  -->
		<xsl:call-template name="OPERATORMESSAGE" />
		<!-- Gen Info  -->
		<xsl:choose>
			<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/@COUNT>0">
				<xsl:apply-templates select="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
				<xsl:call-template name="WCGENINFOMESSAGE" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Vehicle -->
	<xsl:template name="BOATMESSAGE">
		<tr>
			<td class="pageheader">You must have at least one active Boat with following information:</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Boat Number.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Maximum Speed.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Type of Watercraft.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Year.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Make.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Model.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Serial #.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Length.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select State Registered.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Hull Material.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Territory Docked In.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Coverage Type Basis.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Insuring Value.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Waters Navigated.</td>
		</tr>
		<!--Added for Location 22 may 2006-->
		<tr>
			<td class="midcolora">Please insert Location Address.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Location City.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Location State.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Location Zip.</td>
		</tr>
		<!--End Location-->
	</xsl:template>
	<!--Driver -->
	<xsl:template name="OPERATORMESSAGE">
		<tr>
			<td class="pageheader">You must have at least one active Operator with following information:</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert First Name.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Last Name.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Code.</td>
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
		<tr>
			<td class="midcolora">Please select Date of Birth.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Gender.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert License Number.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select License State.</td>
		</tr>
		<tr>
			<td class="midcolora">Please assign boat to Operator.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Principal/Occasional operator.</td>
		</tr>
	</xsl:template>
	<!-- Gen Info -->
	<xsl:template name="WCGENINFOMESSAGE">
		<tr>
			<td class="pageheader">For Watercraft's Underwriting Questions:</td>
		</tr>
		<!--<tr>
			<td class="midcolora">If 'Has applicant lived at current address for less than 3 years?' is 'Yes' then Please insert Years Description.</td>
		</tr>-->
		<tr>
			<td class="midcolora">Please select Any operator physically or mentally impaired.</td>
		</tr>
		<tr>
			<td class="midcolora">If Any operator physically or mentally impaired? is 'Yes' then Please insert Driver Impairment Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Anyone had a license suspended, restricted or revoked in the last 5 years.</td>
		</tr>
		<tr>
			<td class="midcolora">If Anyone had a license suspended, restricted or revoked in the last 5 years? is 'Yes' then Please insert Driver License Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any operator convicted of a major violation in the last 5 years.</td>
		</tr>
		<tr>
			<td class="midcolora">If Any operator convicted of a major violation in the last 5 years? is 'Yes' then Please insert Violation Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Anyone had a drinking or drug related violation in last 5 years?.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any operator convicted of a minor violation in the last 3 years?.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any other insurance with this company.</td>
		</tr>
		<tr>
			<td class="midcolora">If Any other insurance with this company? is 'Yes' then Please insert Other Policy Details(Provide operator, insurance co, vehicle # and policy #).</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Anyone had any losses with this or other vessels.</td>
		</tr>
		<tr>
			<td class="midcolora">If Anyone had any losses with this or other vessels? is 'Yes' then Please insert Loss Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select If any applicant whose watercraft coverage has been refused, cancelled or non renewed in the last 12 months by any insurer.</td>
		</tr>
		<tr>
			<td class="midcolora">If any applicant whose watercraft coverage has been refused, cancelled or non renewed in the last 12 months by any insurer? is 'Yes' then Please insert Coverage Declined Description.</td>
		</tr>
		<!--<tr>
			<td class="midcolora">If 'Credit' is 'Yes' then Please insert Details.</td>
		</tr>-->
		<tr>
			<td class="midcolora">Please select Is any boat rented to others.</td>
		</tr>
		<tr>
			<td class="midcolora">If Is any boat rented to others? is 'Yes' then Please insert Rented Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is any boat registered to anyone other than applicant.</td>
		</tr>
		<tr>
			<td class="midcolora">If Is any boat registered to anyone other than applicant? is 'Yes' then Please insert Registration Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is any boat used to participate in any race,speed, or fishing contest other than a Sailboat under 26ft.</td>
		</tr>
		<tr>
			<td class="midcolora">If Is any boat used to participate in any race,speed, or fishing contest other than a Sailboat under 26ft? is 'Yes' then Please insert Boat Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is any boat used to carry passengers for a charge.</td>
		</tr>
		<tr>
			<td class="midcolora">If Is any boat used to carry passengers for a charge? is 'Yes' then Please insert Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is any Prior Carrier.</td>
		</tr>
		<tr>
			<td class="midcolora">If Is any Prior Carrier? is 'Yes' then Please insert Name of Carrier.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Total number of years of operating experience.</td>
		</tr>
		<!--Added new added Questions in UQ-->
		<tr>
			<td class="midcolora">Please select Does Wolverine Insure the Homeowners Policy?.</td>
		</tr>
		<tr>
			<td class="midcolora">If Does Wolverine Insure the Homeowners Policy? is 'Yes' then Please insert Policy Number.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Are any of the boats Hydrofoils, Swamp Buggies, Collapsible, Experimental, Converted Military Craft Single Passenger or Amphibious?.</td>
		</tr>
		<tr>
			<td class="midcolora">If Are any of the boats Hydrofoils, Swamp Buggies, Collapsible, Experimental, Converted Military Craft Single Passenger or Amphibious? is 'Yes' then Please insert Boat Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is any boat used as a residence?.</td>
		</tr>
		<tr>
			<td class="midcolora">If Is any boat used as a residence? is 'Yes' then Please select Boat Residence Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is any boat used in any water other than Indiana, Michigan or Wisconsin?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Is any boat used in any water other than Indiana, Michigan or Wisconsin? is 'Yes', then Please insert Boat used in any water Description.</td>
		</tr>
	</xsl:template>
	<!--EQUIPMENT MAY 29-->
	<xsl:template name="EQUIPMENTMESSAGE">
		<tr>
			<td class="pageheader">For Equipment Information:</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Equipment Type.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Description.</td>
		</tr>
	</xsl:template>
	<!--MAY 29-->
</xsl:stylesheet>