<!--=========================================================================================================
	Purose	:	Check for the Umbrella Rules
	Name	:	Ashwani  
	Date	:	13 Oct. 2006
=========================================================================================================-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:user="urn:user-namespace-here">
	<msxsl:script language="c#" implements-prefix="user">
<![CDATA[ 
		int intIsRejected = 1;
		public int IsApplicationAcceptable(int tt)

		{
			intIsRejected = intIsRejected*tt;
			return intIsRejected;
		}
		int  intIsReferred = 1;
		public int CheckRefer(int tt)
		{
			intIsReferred = intIsReferred*tt;
			return intIsReferred;
		}
			
		int intCSS=1;
		public int ApplyColor(int colorvalue)
		{
			intCSS=colorvalue;			
			return intCSS;
		}	
		int intDwellingInfoCount=1;
		public int DwellingInfoCount(int dwellingint)
		{
			intDwellingInfoCount=dwellingint*intDwellingInfoCount;			
			return intDwellingInfoCount;
		}
		int intVerify=1;
		public int AllVerified(int status)
		{
			intVerify=intVerify*status;			
			return intVerify;
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
			<!-- ============================================= Client Top ====================================== -->
			<table border="2" align="center" width='90%'>
				<tr>
					<td class="pageheader" width="18%">Customer Name :</td>
					<td class="midcolora" width="36%">
						<xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/CUSTOMER_NAME" />
					</td>
					<td class="pageheader" width="18%">Customer Type :</td>
					<td class="midcolora" width="36%">
						<xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/CUSTOMER_TYPE" />
					</td>
				</tr>
				<tr>
					<td class="pageheader">Address :</td>
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
			<!-- ======================= Checking for the rejected & referred rules =============================-->
			<xsl:call-template name="UM_REJECTION_CASES" />
			<xsl:call-template name="UM_REFERRED_CASES" />
			<table border="1" align="center" width='100%'>
				<tr>
					<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=0">
						<td class="headereffectcenter">Application verification status</td>
					</xsl:if>
					<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=1">
						<td class="headereffectcenter">Policy verification status</td>
					</xsl:if>
				</tr>
				<xsl:choose>
					<xsl:when test="user:IsApplicationAcceptable(1) = 0">
						<tr>
							<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=0">
								<td class="pageheader">This application has been rejected on the following grounds</td>
							</xsl:if>
							<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=1">
								<td class="pageheader">This Policy has been rejected on the following grounds</td>
							</xsl:if>
						</tr>
						<!-- ================================ rejected messages ==============================-->
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/APPLICATIONS/APPLICATION" />
							</td>
						</tr>
						<!-- Gen info -->
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/HOGENINFOS/HOGENINFO" />
							</td>
						</tr>
						<!-- RV Info -->
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/RVEHICLES/RVEHICLE" />
							</td>
						</tr>
					</xsl:when>
					<xsl:when test="user:CheckRefer(1) = 0">
						<tr>
							<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=0">
								<td class="pageheader">This application has been referred on the following grounds</td>
							</xsl:if>
							<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=1">
								<td class="pageheader">This Policy has been referred on the following grounds</td>
							</xsl:if>
						</tr>
						<!-- ================================ referred messages ==============================-->
						<tr>
							<td>
								<xsl:call-template name="RF_CLIENT_OCCUPATION"></xsl:call-template>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/APPLICATIONS/APPLICATION" />
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/UMGENINFOS/UMGENINFO" />
							</td>
						</tr>
						<!--Locations -->
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/LOCATIONS/LOCATION" />
							</td>
						</tr>
						<!--Boats -->
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/BOATS/BOAT" />
							</td>
						</tr>
						<!--RV -->
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/RVEHICLES/RVEHICLE" />
							</td>
						</tr>
						<!--Schedule of underlying-->
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/SCHEDULES/SCHEDULE" />
							</td>
						</tr>
					</xsl:when>
					<xsl:otherwise>
						<tr>
							<td class="midcolora">All the underwriting rules are verified.</td>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</tr>
					</xsl:otherwise>
				</xsl:choose>
			</table>
		</html>
		<span style="display:none">
			<returnValue>
				<xsl:value-of select="user:IsApplicationAcceptable(1)" />
			</returnValue>
			<verifyStatus>
				<xsl:value-of select="user:AllVerified(1)" />
			</verifyStatus>
			<ReferedStatus>
				<xsl:value-of select="user:CheckRefer(1)" />
			</ReferedStatus>
		</span>
	</xsl:template>
	<!-- start ========================== Checking for rejected cases =================================== -->
	<xsl:template name="UM_REJECTION_CASES">
		<xsl:call-template name="IS_RJ_INACTIVE_APPLICATION" />
		<!--Agency Inactive 28th May 2007-->
		<xsl:call-template name="IS_RJ_INACTIVE_AGENCY" />
	</xsl:template>
	<!-- ========================================== Rejected templates ================================== -->
	<!-- 1. -->
	<xsl:template name="IS_RJ_INACTIVE_APPLICATION">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/INACTIVE_APPLICATION='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Agency Inactive 28th May  2007 -->
	<xsl:template name="IS_RJ_INACTIVE_AGENCY">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/INACTIVE_AGENCY='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--End Rejected Templates-->
	<!-- end ======================================= rejected templates ================================== -->
	<!-- start ===================================== Checking for referred Cases ========================== -->
	<xsl:template name="UM_REFERRED_CASES">
		<xsl:choose>
			<xsl:when test="INPUTXML/UMGENINFOS/UMGENINFO/ENGAGED_IN_FARMING='Y' or 
			INPUTXML/UMGENINFOS/UMGENINFO/REDUCED_LIMIT_OF_LIBLITY='Y' or INPUTXML/UMGENINFOS/UMGENINFO/WAT_DWELL='N'			or INPUTXML/UMGENINFOS/UMGENINFO/RECR_VEH='N' or INPUTXML/UMGENINFOS/UMGENINFO/HOME_RENT_DWELL='N' or 
			INPUTXML/UMGENINFOS/UMGENINFO/AUTO_CYCL_TRUCKS='N' or INPUTXML/UMGENINFOS/UMGENINFO/APPLI_UNDERSTAND_LIABILITY_EXCLUDED='N' or INPUTXML/UMGENINFOS/UMGENINFO/								REAL_STATE_VEHICLE_USED='Y' or 	INPUTXML/UMGENINFOS/UMGENINFO/REAL_STATE_VEH_OWNED_HIRED='Y' or 
			INPUTXML/UMGENINFOS/UMGENINFO/PENDING_LITIGATIONS='Y' or INPUTXML/UMGENINFOS/UMGENINFO/INS_DOMICILED_OUTSIDE='Y' or INPUTXML/UMGENINFOS/UMGENINFO/HOME_DAY_CARE='Y' or INPUTXML/UMGENINFOS/UMGENINFO/ANIMALS_EXOTIC_PETS='Y' or INPUTXML/UMGENINFOS/UMGENINFO/ANY_OPERATOR_IMPIRED='Y' or INPUTXML/UMGENINFOS/UMGENINFO/ANY_OPERATOR_CON_TRAFFIC='Y' or INPUTXML/UMGENINFOS/UMGENINFO/ANY_FULL_TIME_EMPLOYEE='Y' or INPUTXML/UMGENINFOS/UMGENINFO/ANY_COVERAGE_DECLINED='Y' or			
			INPUTXML/UMGENINFOS/UMGENINFO/ANY_AIRCRAFT_OWNED_LEASED='Y' or INPUTXML/UMGENINFOS/UMGENINFO/						BUSINESS_PROF_ACTIVITY='Y'  or INPUTXML/LOCATIONS/LOCATION/BUSS_FARM_PURSUITS_OTHER='Y' 
			or INPUTXML/UMGENINFOS/UMGENINFO/FAMILIES='Y' or INPUTXML/UMGENINFOS/UMGENINFO/RENTAL_DWELLINGS_UNIT='Y' or INPUTXML/LOCATIONS/LOCATION/BUSS_FARM_PURSUITS_SCH_OFF='Y' or INPUTXML/LOCATIONS/LOCATION/						PERS_INJ_COV_82='Y' or INPUTXML/LOCATIONS/LOCATION/NUM_FAMILIES='Y'  or 			
			INPUTXML/LOCATIONS/LOCATION/LOC_EXCLUDED='Y' or 
			INPUTXML/BOATS/BOAT/RF_USED_PARTICIPATE='Y' or  INPUTXML/BOATS/BOAT/WC_LEN_MAXSPD_HORSEPWR='Y' or  
			INPUTXML/BOATS/BOAT/WC_LEN_MAXSPD_HPOVER='Y' or  INPUTXML/BOATS/BOAT/WC_LEN_MAXSPD_HPOVER600='Y' or  
			INPUTXML/BOATS/BOAT/WC_LENOVER40_MAXSPD_HPOVER600='Y' or INPUTXML/BOATS/BOAT/WC_LEN_MAXSPD56_HPOVER600='Y' or INPUTXML/BOATS/BOAT/WC_LEN27_MAXSPD56_HPOVER600='Y' or INPUTXML/BOATS/BOAT/WC_LEN40_MAXSPD56_HPOVER600='Y' or
			INPUTXML/BOATS/BOAT/WATERCRAFT_TYPE='Y'
			or INPUTXML/RVEHICLES/RVEHICLE/USED_IN_RACE_SPEED ='Y'  or INPUTXML/RVEHICLES/RVEHICLE/RF_STATE_REGISTERED = 'Y'         or INPUTXML/RVEHICLES/RVEHICLE/VEHICLE_MODIFIED ='Y' or 
			 INPUTXML/RVEHICLES/RVEHICLE/RV_DUNEBUGGIE ='Y' or INPUTXML/APPLICATIONS/APPLICATION/APPLICANT_OCCU ='Y' or INPUTXML/SCHEDULES/SCHEDULE/AUTO_VEHICLE_DRIVER_RULE='Y' or INPUTXML/SCHEDULES/SCHEDULE/MOTOR_MOTORINFO_DRIVER_RULE='Y' or INPUTXML/SCHEDULES/SCHEDULE/WATERCRAFT_WATERCRAFTINFO_RULE='Y' 
			or INPUTXML/SCHEDULES/SCHEDULE/HOME_DWELLINGINFO_RULE='Y' or INPUTXML/SCHEDULES/SCHEDULE/BODILY_INJURY_LIABILITY='Y' or INPUTXML/SCHEDULES/SCHEDULE/PROPERTY_DAMAGE_LIABILITY='Y' or INPUTXML/SCHEDULES/SCHEDULE/UNDERLYING_PERSONAL_LIABILITY='Y' 
			or INPUTXML/SCHEDULES/SCHEDULE/SINGLE_LIMITS_LIABILITY='Y' or INPUTXML/SCHEDULES/SCHEDULE/UNDERINSURED_UNDERINSUREDCSL_MOTORIST='Y'
			or INPUTXML/SCHEDULES/SCHEDULE/UNINSURED_UNINSUREDCSL_MOTORIST='Y' or INPUTXML/SCHEDULES/SCHEDULE/DRIVERTYPE_DOB_SLIABILITY='Y'
			 or INPUTXML/APPLICATIONS/APPLICATION/DFI_ACC_NO_RULE='Y' or INPUTXML/APPLICATIONS/APPLICATION/CREDIT_CARD='Y' or INPUTXML/SCHEDULES/SCHEDULE/PER_INJURY_82='Y' or INPUTXML/APPLICATIONS/APPLICATION/APPEFFECTIVEDATE='Y' or INPUTXML/APPLICATIONS/APPLICATION/TOTAL_PREMIUM_AT_RENEWAL='Y' or INPUTXML/APPLICATIONS/APPLICATION/CLAIM_EFFECTIVE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- template -->
	<!-- end ===================================== Checking for referred Cases ========================== -->
	<!--  ================================== calling rejected/referred messages  ============================= -->
	<!-- Application Information  -->
	<xsl:template match="INPUTXML/APPLICATIONS/APPLICATION">
		<xsl:choose>
			<!-- Rejected Application Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="INACTIVE_APPLICATION='Y' or INACTIVE_AGENCY='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Application information:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="INACTIVE_APPLICATION"></xsl:call-template>
						<xsl:call-template name="INACTIVE_AGENCY"></xsl:call-template>
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred Application Info -->
				<xsl:choose>
					<xsl:when test="DFI_ACC_NO_RULE='Y' or CREDIT_CARD='Y' or APPEFFECTIVEDATE='Y' or TOTAL_PREMIUM_AT_RENEWAL='Y'
					or CLAIM_EFFECTIVE='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Application information:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="RF_APPLICATION_INFO_MEG"></xsl:call-template>
						<xsl:call-template name="DFI_ACC_NO_RULE"></xsl:call-template>
						<xsl:call-template name="CREDIT_CARD" />
						<xsl:call-template name="APPEFFECTIVEDATE" />
						<xsl:call-template name="TOTAL_PREMIUM_AT_RENEWAL" />
						<xsl:call-template name="CLAIM_EFFECTIVE" />												
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- General Info -->
	<xsl:template match="INPUTXML/UMGENINFOS/UMGENINFO">
		<xsl:choose>
			<!-- Rejected Underwriting Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="'Y'='N'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Underwriting Questions:</td>
									</tr>
								</table>
							</td>
						</tr>
						<!--<xsl:call-template name="ANY_FARMING_BUSINESS_COND" />-->
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred Underwriting Info -->
				<xsl:choose>
					<xsl:when test="ENGAGED_IN_FARMING='Y' or REDUCED_LIMIT_OF_LIBLITY='Y' or WAT_DWELL='N'
					or RECR_VEH='N' or HOME_RENT_DWELL='N' or AUTO_CYCL_TRUCKS ='N' or APPLI_UNDERSTAND_LIABILITY_EXCLUDED='N' or REAL_STATE_VEHICLE_USED='Y'
					or REAL_STATE_VEH_OWNED_HIRED='Y' or INS_DOMICILED_OUTSIDE='Y' or HOME_DAY_CARE='Y' or ANY_OPERATOR_IMPIRED='Y' or ANIMALS_EXOTIC_PETS='Y' or  ANY_OPERATOR_CON_TRAFFIC='Y' or PENDING_LITIGATIONS='Y' or ANY_FULL_TIME_EMPLOYEE='Y'
					or ANY_COVERAGE_DECLINED='Y' or ANY_AIRCRAFT_OWNED_LEASED='Y' or BUSINESS_PROF_ACTIVITY='Y' or FAMILIES='Y' or RENTAL_DWELLINGS_UNIT='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Underwriting Questions:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="RF_UNDERWRITING_QUS_MSG" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--Schedule of underlying-->
	<xsl:template match="INPUTXML/SCHEDULES/SCHEDULE">
		<xsl:choose>
			<!-- Rejected Schedule of underlying -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="'Y'='N'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Schedule Of Underlying:</td>
									</tr>
								</table>
							</td>
						</tr>
						<!--<xsl:call-template name="ANY_FARMING_BUSINESS_COND" />-->
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred Schedule Of Underlying  or MOTOR_MOTORINFO_DRIVER_RULE='Y' -->
				<xsl:choose>
					<xsl:when test="AUTO_VEHICLE_DRIVER_RULE='Y' or MOTOR_MOTORINFO_DRIVER_RULE='Y' or HOME_DWELLINGINFO_RULE='Y'or WATERCRAFT_WATERCRAFTINFO_RULE='Y'
					or UNDERINSURED_UNDERINSUREDCSL_MOTORIST='Y' or UNINSURED_UNINSUREDCSL_MOTORIST='Y' or  UNDERLYING_PERSONAL_LIABILITY='Y' 
					or BODILY_INJURY_LIABILITY='Y' or PROPERTY_DAMAGE_LIABILITY='Y' or SINGLE_LIMITS_LIABILITY='Y' or DRIVERTYPE_DOB_SLIABILITY='Y' or PER_INJURY_82 ='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Schedule Of Underlying:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="RF_SCHEDULE_QUS_MSG" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Location Information  -->
	<xsl:template match="INPUTXML/LOCATIONS/LOCATION">
		<xsl:choose>
			<!-- Rejected Loc Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="'Y'='N'"></xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred Loc Info -->
				<xsl:choose>
					<xsl:when test="BUSS_FARM_PURSUITS_OTHER='Y' or BUSS_FARM_PURSUITS_SCH_OFF = 'Y' or	PERS_INJ_COV_82='Y' or NUM_FAMILIES='Y' or LOC_EXCLUDED='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Location information:</td>
									</tr>
								</table>
							</td>
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
						<xsl:call-template name="RF_LOCATION_MEG"></xsl:call-template>
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Boats Information  -->
	<xsl:template match="INPUTXML/BOATS/BOAT">
		<xsl:choose>
			<!-- Rejected Boat Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="'Y'='N'"></xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred Boat Info -->
				<xsl:choose>
					<xsl:when test="RF_USED_PARTICIPATE='Y' or WC_LEN_MAXSPD_HORSEPWR='Y' or WC_LEN_MAXSPD_HPOVER='Y'
					or WC_LEN_MAXSPD_HPOVER600='Y' or WC_LENOVER40_MAXSPD_HPOVER600='Y' or 
					WC_LEN_MAXSPD56_HPOVER600='Y' or WC_LEN27_MAXSPD56_HPOVER600='Y' or WC_LEN40_MAXSPD56_HPOVER600='Y' or WATERCRAFT_TYPE='Y' ">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Boat information:</td>
									</tr>
								</table>
							</td>
							<tr>
								<td>
									<table width="60%">
										<tr>
											<xsl:if test=" BOAT_NO!= ''">
												<td class="pageheader" width="18%">Boat Number:</td>
												<td class="midcolora" width="36%">
													<xsl:value-of select="BOAT_NO" />
												</td>
											</xsl:if>
										</tr>
									</table>
								</td>
							</tr>
						</tr>
						<xsl:call-template name="RF_BOAT_MEG"></xsl:call-template>
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- RV  Information  -->
	<xsl:template match="INPUTXML/RVEHICLES/RVEHICLE">
		<xsl:choose>
			<!-- Rejected RV Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="'Y'='N'"></xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred RV Info -->
				<xsl:choose>
					<xsl:when test="USED_IN_RACE_SPEED ='Y' or RF_STATE_REGISTERED='Y' or VEHICLE_MODIFIED='Y' or RV_DUNEBUGGIE='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For RV information:</td>
									</tr>
								</table>
							</td>
							<tr>
								<td>
									<table width="60%">
										<tr>
											<xsl:if test="COMPANY_ID_NUMBER!= ''">
												<td class="pageheader" width="18%">RV#: </td>
												<td class="midcolora" width="36%">
													<xsl:value-of select="COMPANY_ID_NUMBER" />
												</td>
											</xsl:if>
										</tr>
									</table>
								</td>
							</tr>
						</tr>
						<xsl:call-template name="RF_RV_MEG"></xsl:call-template>
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--  start ============================== messages for RV referred case ========================== -->
	<xsl:template name="RF_RV_MEG">
		<xsl:if test="USED_IN_RACE_SPEED ='Y'">
			<tr>
				<td class='midcolora'>Used in a race or speed contest.</td>
			</tr>
		</xsl:if>
		<xsl:if test="RF_STATE_REGISTERED='Y'">
			<tr>
				<td class='midcolora'>Vehicle registered state is other than Michigan or Indiana.</td>
			</tr>
		</xsl:if>
		<xsl:if test="VEHICLE_MODIFIED ='Y'">
			<tr>
				<td class='midcolora'>Vehicle has been modified.</td>
			</tr>
		</xsl:if>
		<xsl:if test="RV_DUNEBUGGIE ='Y'">
			<tr>
				<td class='midcolora'>Recreational Vehicle - Motorcycle is of type Dune Buggies.</td>
			</tr>
		</xsl:if>
	</xsl:template>
	<!--  end ============================== messages for RV referred case ========================== -->
	<!--  start ============================== messages for Boat referred case ========================== -->
	<xsl:template name="RF_BOAT_MEG">
		<xsl:if test="WC_LEN_MAXSPD56_HPOVER600 ='Y'">
			<tr>
				<td class='midcolora'>Length of watercraft is lies between 16 to 26 feet, Max Speed lies between 56 to 65 and horsepower is over 600.
				</td>
			</tr>
		</xsl:if>
		<xsl:if test="WC_LEN27_MAXSPD56_HPOVER600 ='Y'">
			<tr>
				<td class='midcolora'>Length of watercraft is lies between 27 to 40 feet, Max Speed lies between 56 to 65 and horsepower is over 600.
				</td>
			</tr>
		</xsl:if>
		<xsl:if test="WC_LEN40_MAXSPD56_HPOVER600 ='Y'">
			<tr>
				<td class='midcolora'>Length of watercraft is lies over 40 feet, Max Speed lies between 56 to 65 and horsepower is over 600.
				</td>
			</tr>
		</xsl:if>
		<xsl:if test="WC_LENOVER40_MAXSPD_HPOVER600 ='Y'">
			<tr>
				<td class='midcolora'>Length of watercraft is over 40 feet, Max Speed lies between 0 to 55 and horsepower is over 600.
				</td>
			</tr>
		</xsl:if>
		<xsl:if test="WC_LEN_MAXSPD_HPOVER600 ='Y'">
			<tr>
				<td class='midcolora'>Length of watercraft lies between 27 to 40 feet, Max Speed lies between 0 to	55 and horsepower is over 600.
				</td>
			</tr>
		</xsl:if>
		<xsl:if test="WC_LEN_MAXSPD_HPOVER ='Y'">
			<tr>
				<td class='midcolora'>Length of watercraft lies between 16 to 26 feet, Max Speed lies between 0 to	55 and horsepower is over 260.
				</td>
			</tr>
		</xsl:if>
		<xsl:if test="RF_USED_PARTICIPATE ='Y'">
			<tr>
				<td class='midcolora'>Watercraft Used to participate in race,speed or fishing contest.</td>
			</tr>
		</xsl:if>
		<xsl:if test="WC_LEN_MAXSPD_HORSEPWR ='Y'">
			<tr>
				<td class='midcolora'>Length of watercraft is less than 16 feet, Max Speed lies between 0 to 55 and	horsepower is over 160.
				</td>
			</tr>
		</xsl:if>
		<xsl:if test="WATERCRAFT_TYPE ='Y'">
			<tr>
				<td class='midcolora'>Personal Liability Limit is under 300,000 and Type of Watercraft is Canoe or Paddleboat or Row Boat.
				</td>
			</tr>
		</xsl:if>
	</xsl:template>
	<!--  start ============================== messages for Boat referred case ========================== -->
	<!--  start ============================== messages for Location referred case ========================== -->
	<xsl:template name="RF_LOCATION_MEG">
		<xsl:if test="BUSS_FARM_PURSUITS_OTHER ='Y'">
			<tr>
				<td class='midcolora'>Business/Farming Pursuits is selected as Other Business.</td>
			</tr>
		</xsl:if>
		<xsl:if test="BUSS_FARM_PURSUITS_SCH_OFF ='Y'">
			<tr>
				<td class='midcolora'>Business/Farming Pursuits is selected as Office/School or Studio.</td>
			</tr>
		</xsl:if>
		<xsl:if test="PERS_INJ_COV_82='Y'">
			<tr>
				<td class='midcolora'>Not carry Personal Injury Coverage - HO-82.</td>
			</tr>
		</xsl:if>
		<xsl:if test="NUM_FAMILIES='Y'">
			<tr>
				<td class='midcolora'>Number of families is greater than 4.</td>
			</tr>
		</xsl:if>
		<xsl:if test="LOC_EXCLUDED='Y'">
			<tr>
				<td class='midcolora'>No location excluded under this policy or No Exclusion - All Hazards in Connection with Designated Premises Endorsement.</td>
			</tr>
		</xsl:if>
		
	</xsl:template>
	<!--  end ============================== messages for Location referred case ========================== -->
	<!--  start ============================== messages for UQ referred case  ============================= -->
	<xsl:template name="RF_UNDERWRITING_QUS_MSG">
		<xsl:if test="ENGAGED_IN_FARMING='Y'">
			<tr>
				<td class='midcolora'>Engaged in farming operation.</td>
			</tr>
		</xsl:if>
		<xsl:if test="REDUCED_LIMIT_OF_LIBLITY='Y'">
			<tr>
				<td class='midcolora'>Primary policy have reduced limits of liability or eliminate coverage						for specific exposures or list excluded drivers.</td>
			</tr>
		</xsl:if>
		<xsl:if test="WAT_DWELL='N'">
			<tr>
				<td class='midcolora'>No underlying 'Watercraft' policy with Wolverine.</td>
			</tr>
		</xsl:if>
		<xsl:if test="RECR_VEH='N'">
			<tr>
				<td class='midcolora'>No underlying 'Recreational Vehicles' policy with Wolverine.</td>
			</tr>
		</xsl:if>
		<xsl:if test="HOME_RENT_DWELL='N'">
			<tr>
				<td class='midcolora'>No underlying 'Home/Rental Dwellings' policy with Wolverine.</td>
			</tr>
		</xsl:if>
		<xsl:if test="AUTO_CYCL_TRUCKS='N'">
			<tr>
				<td class='midcolora'>No underlying 'Automobiles/Cycles/Trucks' policy with Wolverine.</td>
			</tr>
		</xsl:if>
		<xsl:if test="APPLI_UNDERSTAND_LIABILITY_EXCLUDED ='N'">
			<tr>
				<td class='midcolora'>No applicant understand that Business Pursuits, Professional Liability and Aircraft Liability are excluded.</td>
			</tr>
		</xsl:if>
		<xsl:if test="INS_DOMICILED_OUTSIDE='Y'">
			<tr>
				<td class='midcolora'>The Named insured or applicant permanently domiciled outside of Michigan or Indiana for more than 6 months.</td>
			</tr>
		</xsl:if>
		<xsl:if test="ANY_OPERATOR_IMPIRED='Y'">
			<tr>
				<td class='midcolora'>Any operator physically or mentally impaired.</td>
			</tr>
		</xsl:if>
		<xsl:if test="ANY_OPERATOR_CON_TRAFFIC='Y'">
			<tr>
				<td class='midcolora'>Any operators convicted for any traffic violations during the last 3 years.</td>
			</tr>
		</xsl:if>
		<xsl:if test="ANIMALS_EXOTIC_PETS='Y'">
			<tr>
				<td class='midcolora'>Applicant or any tenant have any animals or exotic pets.</td>
			</tr>
		</xsl:if>
		<xsl:if test="HOME_DAY_CARE='Y'">
			<tr>
				<td class='midcolora'>You providing home day care service to a person other than a relative and  receiving money or other compensation.</td>
			</tr>
		</xsl:if>
		<xsl:if test="REAL_STATE_VEHICLE_USED='Y'">
			<tr>
				<td class='midcolora'>Any real estate, vehicles, watercraft, aircraft used commercially or for	business purposes.</td>
			</tr>
		</xsl:if>
		<xsl:if test="REAL_STATE_VEH_OWNED_HIRED='Y'">
			<tr>
				<td class='midcolora'>Any real estate, vehicles, watercraft, aircraft owned, hired, leased or								regularly used not covered by primary policies.</td>
			</tr>
		</xsl:if>
		<xsl:if test="PENDING_LITIGATIONS='Y'">
			<tr>
				<td class='midcolora'>Pending litigations court proceedings or judgments.</td>
			</tr>
		</xsl:if>
		<xsl:if test="ANY_FULL_TIME_EMPLOYEE='Y'">
			<tr>
				<td class='midcolora'>Full time employees.</td>
			</tr>
		</xsl:if>
		<xsl:if test="FAMILIES='Y'">
			<tr>
				<td class='midcolora'>Rental Dwellings/Total # of Families is greater than 4.</td>
			</tr>
		</xsl:if>
		<xsl:if test="RENTAL_DWELLINGS_UNIT='Y'">
			<tr>
				<td class='midcolora'>Number of Rental Dwelling is greater than 4.</td>
			</tr>
		</xsl:if>
		<xsl:if test="ANY_COVERAGE_DECLINED='Y'">
			<tr>
				<td class='midcolora'>Coverage declined, cancelled or non-renewed during the last 3 years.</td>
			</tr>
		</xsl:if>
		<xsl:if test="ANY_AIRCRAFT_OWNED_LEASED='Y'">
			<tr>
				<td class='midcolora'>Aircraft owned, leased, chartered or furnished for regular use.</td>
			</tr>
		</xsl:if>
		<xsl:if test="BUSINESS_PROF_ACTIVITY='Y'">
			<tr>
				<td class='midcolora'>Any business or professional activities included in the primary policies.</td>
			</tr>
		</xsl:if>
	</xsl:template>
	<!--  end ============================== messages for UQ referred case  ============================= -->
	<!-- start============================== messages for Schedule Of Underlying referred case ========== -->
	<xsl:template name="RF_SCHEDULE_QUS_MSG">
		<xsl:if test="AUTO_VEHICLE_DRIVER_RULE='Y'">
			<tr>
				<td class='midcolora'> Enter At Least One Entry in Vehicle Info And Driver/Operator Information .</td>
			</tr>
		</xsl:if>
		<xsl:if test="MOTOR_MOTORINFO_DRIVER_RULE='Y'">
			<tr>
				<td class='midcolora'> Enter At Least One Entry in Motor Info And Driver/Operator Information .</td>
			</tr>
		</xsl:if>
		<xsl:if test="HOME_DWELLINGINFO_RULE='Y'">
			<tr>
				<td class='midcolora'> Enter At Least One  Entry In  Locations - Other Carriers.</td>
			</tr>
		</xsl:if>
		<xsl:if test="WATERCRAFT_WATERCRAFTINFO_RULE='Y'">
			<tr>
				<td class='midcolora'> Enter At Least One Boat and One Operator.</td>
			</tr>
		</xsl:if>
		<xsl:if test="UNDERINSURED_UNDERINSUREDCSL_MOTORIST='Y'">
			<tr>
				<td class='midcolora'> Underinsured Motorist or Underinsured Motorist CSL are not the same.</td>
			</tr>
		</xsl:if>
		<xsl:if test="UNINSURED_UNINSUREDCSL_MOTORIST='Y'">
			<tr>
				<td class='midcolora'> Uninsured Motorist or Uninsured Motorist CSL are not the same.</td>
			</tr>
		</xsl:if>
		<xsl:if test="UNDERLYING_PERSONAL_LIABILITY='Y'">
			<tr>
				<td class='midcolora'>Personal Liability Limit is under 300,000.</td>
			</tr>
		</xsl:if>
		<xsl:if test="PROPERTY_DAMAGE_LIABILITY='Y'">
			<tr>
				<td class='midcolora'>Property Damage Limit is Less than $100,000 .</td>
			</tr>
		</xsl:if>
		<xsl:if test="BODILY_INJURY_LIABILITY='Y'">
			<tr>
				<td class='midcolora'>Bodily Injury Split Limit and the limit is less than $250,000/$500,000.</td>
			</tr>
		</xsl:if>
		<xsl:if test="SINGLE_LIMITS_LIABILITY='Y'">
			<tr>
				<td class='midcolora'>Single Limit Liability is less than $300,000 .</td>
			</tr>
		</xsl:if>
		<xsl:if test="DRIVERTYPE_DOB_SLIABILITY='Y'">
			<tr>
				<td class='midcolora'>Driver is under 25 years and Driver License type - other than Excluded and Single Limit Liability is less than $500,000 .</td>
			</tr>
		</xsl:if>
		<xsl:if test="PER_INJURY_82='Y'">
			<tr>
				<td class='midcolora'>Primary Homeowner Wolverine Policy does not has Endorsement Personal Injury (HO-82)(Covg Sec 2).</td>
			</tr>
		</xsl:if>
	</xsl:template>
	<!-- end  ============================== messages for Schedule Of Underlying referred case ========== -->
	<!--Start============================Messages for application Info Reject case ====================== -->
	<xsl:template name="INACTIVE_APPLICATION">
		<xsl:if test="INACTIVE_APPLICATION='Y'">
			<TR>
				<td class="midcolora">This Customer is Inactive.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--Agency Inactive-->
	<xsl:template name="INACTIVE_AGENCY">
		<xsl:if test="INACTIVE_AGENCY='Y'">
			<TR>
				<td class="midcolora">Selected Agency is Inactive.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Billing Information-->
	<xsl:template name="DFI_ACC_NO_RULE">
		<xsl:if test="DFI_ACC_NO_RULE='Y'">
			<TR>
				<td class="midcolora">Complete DFI Account Number or Transit/Routing Number in Billing Info.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Credit Card -->
	<xsl:template name="CREDIT_CARD">
		<xsl:if test="CREDIT_CARD='Y'">
			<tr>
				<td class="midcolora">Complete First Name and Last Name and Card Type and Card CVV/CCV # and Credit Card # and Valid To (Month/Year) in Billing Info.</td>
			</tr>
		</xsl:if>
	</xsl:template>
	<!--Effective Date -->
	<xsl:template name="APPEFFECTIVEDATE">
		<xsl:if test="APPEFFECTIVEDATE='Y'">
			<tr>
				<td class="midcolora">Effective Date is less than 2000.</td>
			</tr>
		</xsl:if>
	</xsl:template>
	<xsl:template name="TOTAL_PREMIUM_AT_RENEWAL">
		<xsl:if test="TOTAL_PREMIUM_AT_RENEWAL='Y'">
			<tr>
				<td class="midcolora">Balance Due on policy is greater than equal to Past Due at Renewal.</td>
			</tr>
		</xsl:if>
	</xsl:template>
	<xsl:template name="CLAIM_EFFECTIVE">
		<xsl:if test="CLAIM_EFFECTIVE='Y'">
			<tr>
				<td class="midcolora">Policy had claim(s) reported.</td>
			</tr>
		</xsl:if>
	</xsl:template>
	<!-- end-->
	<!--  start ============================== messages for application Info referred case  ============= -->
	<xsl:template name="RF_APPLICATION_INFO_MEG">
		<!--Empty --></xsl:template>
	<xsl:template name="RF_CLIENT_OCCUPATION">
		<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/APPLICANT_OCCU='Y'">
			<tr>
				<td class="pageheader">For Customer information:</td>
			</tr>
			<tr>
				<td class='midcolora'>Customer occupation is either Actor/Actress or Athletes or Author/Writer or Celebrity or Government Appointee - State or Government Appointee - Federal or Newspaper Editor/Columnist or Entertainer or Labor Leaders other than Shop Stewards or Newspaper Publisher				or Public Lecturer or	Public Office Holder or Radio and Television Announcer.</td>
			</tr>
		</xsl:if>
	</xsl:template>
	<!-- end ============================== messages for application Info referred case  ============= -->
</xsl:stylesheet>