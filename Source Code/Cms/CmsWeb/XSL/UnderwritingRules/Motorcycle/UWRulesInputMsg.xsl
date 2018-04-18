<!-- ==================================================================================================
File Name			:	UWRulesInputMsg.xsl
Purpose				:	To display Messages for Motorcycle 
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
]]>
	</msxsl:script>
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
			<!-- ================================= Client Header ========================================== -->
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
			<!-- =================================== Call Mandatory Message Templates ===================== -->
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
				<xsl:choose>
					<!-- ============================  when there is no motorcycle and driver ==================== -->
					<xsl:when test="INPUTXML/MOTORCYCLES/ERRORS/ERROR/@ERRFOUND='T' and INPUTXML/DRIVERS/ERRORS/ERROR/@ERRFOUND='T'">
						<!-- =================== Display all the mandatory fields ========================== -->
						<xsl:if test="user:AssignUnderwritter(0) = 0"></xsl:if>
						<xsl:call-template name="INTMOTORCYCLEDETAIL" />
					</xsl:when>
					<xsl:otherwise>
						<!-- =======================Display all the incomplete information (Mandatory + Rules)  ======== -->
						<xsl:call-template name="MOTORCYCLEDETAIL" />
					</xsl:otherwise>
				</xsl:choose>
			</table>
		</html>
	</xsl:template>
	<!-- ======================================(Mandatory and Rules) incomplete Info ======================== -->
	<xsl:template name="MOTORCYCLEDETAIL">
		<xsl:apply-templates select="INPUTXML/APPLICATIONS/APPLICATION"></xsl:apply-templates>
		<xsl:apply-templates select="INPUTXML/MVRS/MVR"></xsl:apply-templates>
		<!--  ========================================== Motorcycle ============================= -->
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTORCYCLES/@COUNT> 0">
				<xsl:for-each select="INPUTXML/MOTORCYCLES">
					<xsl:apply-templates select="MOTORCYCLE"></xsl:apply-templates>
				</xsl:for-each>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
				<xsl:call-template name="MOTORCYCLEMESSAGE" />
				<!-- <xsl:call-template name="ADDITIONALINTMESSAGE"/> -->
			</xsl:otherwise>
		</xsl:choose>
		<!-- ================================== Driver ================================================ -->
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/@COUNT> 0">
				<xsl:for-each select="INPUTXML/DRIVERS">
					<xsl:apply-templates select="DRIVER"></xsl:apply-templates>
				</xsl:for-each>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0" />
				<xsl:call-template name="DRIVERMESSAGE" />
			</xsl:otherwise>
		</xsl:choose>
		<!-- ============================= General Info  ===================================== -->
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO/IS_RECORD_EXISTS='Y'">
				<xsl:call-template name="GENINFOMESSAGE" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:apply-templates select="INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ================================== Application Info =================================== -->
	<xsl:template match="INPUTXML/APPLICATIONS/APPLICATION">
		<xsl:choose>
			<xsl:when test="STATE_ID=-1 or STATE_ID='' or APP_LOB='' or APP_TERMS='' or APP_EFFECTIVE_DATE='' or APP_EFFECTIVE_DATE='NULL' or
				APP_EXPIRATION_DATE='' or APP_AGENCY_ID=-1 or APP_AGENCY_ID='' or BILL_TYPE='' or PROXY_SIGN_OBTAINED=-1 
				or PROXY_SIGN_OBTAINED='' or CHARGE_OFF_PRMIUM='' or IS_PRIMARY_APPLICANT='' or IS_PRIMARY_APPLICANT='Y'
				or IS_MODIFIED_INCREASE_SPEED='' or IS_MODIFIED_KIT='' or DOWN_PAY_MENT='' or POL_UNDERWRITER=''">
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
				<xsl:call-template name="PROXY_SIGN_OBTAINED" />
				<xsl:call-template name="IS_PRIMARY_APPLICANT" />
				<xsl:call-template name="CHARGE_OFF_PRMIUM" />
				<xsl:call-template name="IS_MODIFIED_INCREASE_SPEED" />
				<xsl:call-template name="IS_MODIFIED_KIT" />
				<xsl:call-template name="DOWN_PAY_MENT" />
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
	<!-- ======================================== Motorcycle =========================================  -->
	<xsl:template match="MOTORCYCLE">
		<xsl:choose>
			<xsl:when test="VEHICLE_YEAR='' or MAKE='' or MODEL='' or GRG_ADD1='' or GRG_CITY='' or 
				GRG_STATE='' or GRG_ZIP='' or TERRITORY=''  or AMOUNT='' or MOTORCYCLE_TYPE=''
				or VEHICLE_CC='0' or VEHICLE_CC='' or SYMBOL='' or CYCL_REGD_ROAD_USE=''">
				<tr>
					<td class="pageheader">For Motorcycle Information: </td>
					<tr>
						<td>
							<table width="60%">
								<tr>
									<xsl:if test="VIN != '' and VIN!='N'">
										<td class="pageheader" width="18%">VIN:</td>
										<td class="midcolora" width="36%">
											<xsl:value-of select="VIN" />
										</td>
									</xsl:if>
									<xsl:if test="MODEL != ''">
										<td class="pageheader" width="18%">Model:</td>
										<td class="midcolora" width="36%">
											<xsl:value-of select="MODEL" />
										</td>
									</xsl:if>
								</tr>
								<tr>
									<xsl:if test="VEHICLE_YEAR != ''">
										<td class="pageheader" width="18%">Vehicle Year:</td>
										<td class="midcolora" width="36%">
											<xsl:value-of select="VEHICLE_YEAR" />
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
				<xsl:call-template name="VEHICLE_YEAR" />
				<xsl:call-template name="MAKE" />
				<xsl:call-template name="MODEL" />
				<xsl:call-template name="MOTORCYCLE_TYPE" />
				<xsl:call-template name="VEHICLE_CC" />
				<xsl:call-template name="GRG_ADD1" />
				<xsl:call-template name="GRG_CITY" />
				<xsl:call-template name="GRG_STATE" />
				<xsl:call-template name="GRG_ZIP" />
				<xsl:call-template name="TERRITORY" />
				<xsl:call-template name="AMOUNT" />
				<xsl:call-template name="SYMBOL" />
				<xsl:call-template name="CYCL_REGD_ROAD_USE" />
				<!-- <xsl:call-template name="VEHICLE_AGE" /> -->
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
	<!-- ===================================== Driver info ======================================== -->
	<xsl:template match="DRIVER">
		<xsl:choose>
			<xsl:when test="DRIVER_FNAME='' or DRIVER_LNAME='' or DRIVER_CODE='' or DRIVER_SEX='' or 
			DRIVER_ADD1='' or DRIVER_CITY='' or DRIVER_STATE='-1' or DRIVER_STATE='0' or DRIVER_STATE='' or 
			DRIVER_ZIP='' or DRIVER_DOB=''or DRIVER_LIC_STATE='' or RELATIONSHIP='0' or VEHICLE_ID='' or DRIVER_DRIV_LIC='' 
			or DRIVER_US_CITIZEN='' or DATE_LICENSED=NULL or DATE_LICENSED='' or APP_VEHICLE_PRIN_OCC_ID='' or DRIVER_DRINK_VIOLATION='' or DRIVER_DRIV_TYPE=''">
				<tr>
					<td class="pageheader">For Driver Information:</td>
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
									<xsl:if test="DRIVER_DRIV_LIC != 'N'">
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
				<xsl:call-template name="DRIVER_SEX" />
				<xsl:call-template name="DRIVER_ADD1" />
				<xsl:call-template name="DRIVER_CITY" />
				<xsl:call-template name="DRIVER_STATE" />
				<xsl:call-template name="DRIVER_ZIP" />
				<xsl:call-template name="DRIVER_DOB" />
				<xsl:call-template name="DRIVER_LIC_STATE" />
				<xsl:call-template name="DRIVER_DRIV_LIC" />
				<xsl:call-template name="DRIVER_DRINK_VIOLATION" />
				<!--xsl:call-template name="RELATIONSHIP" /-->
				<xsl:call-template name="VEHICLE_ID" />
				<xsl:call-template name="DRIVER_US_CITIZEN" />
				<xsl:call-template name="DATE_LICENSED" />
				<xsl:call-template name="APP_VEHICLE_PRIN_OCC_ID" />
				<xsl:call-template name="DRIVER_DRIV_TYPE" />				
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
	<!-- ============================= MVR INFORMATION       ======================== -->
	<xsl:template match="INPUTXML/MVRS/MVR">
		<xsl:choose>
			<xsl:when test="MVR_VIOLATION_ID='' or MVR_VIOLATION_ID=0 or CONVICTION_DATE='' or OCCURENCE_DATE='' ">
				<tr>
					<td class="pageheader">For MVR Information:</td>
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
	<!-- End MVR Information-->
	<!-- ============================= Underwriting Question ====================== -->
	<xsl:template match="INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO">
		<xsl:choose>
			<xsl:when test="DRIVER_SUS_REVOKED='' or DRIVER_SUS_REVOKED_PP_DESC='' or PHY_MENTL_CHALLENGED='' or 
				  PHY_MENTL_CHALLENGED_PP_DESC='' or ANY_FINANCIAL_RESPONSIBILITY='' or ANY_FINANCIAL_RESPONSIBILITY_PP_DESC='' or 
				  MULTI_POLICY_DISC_APPLIED='' or MULTI_POLICY_DISC_APPLIED_PP_DESC='' or DATE_OF_BIRTH= '' or FULLNAME='' or 
				   IS_EXTENDED_FORKS='' or IS_COMMERCIAL_USE='' or ANY_NON_OWNED_VEH='' or IS_USEDFOR_RACING='' or 
				   IS_TAKEN_OUT='' or IS_MORE_WHEELS='' or IS_CONVICTED_CARELESS_DRIVE='' or IS_COST_OVER_DEFINED_LIMIT='' or 
				  EXISTING_DMG= '' or   COVERAGE_DECLINED='' or SALVAGE_TITLE='' or IS_LICENSED_FOR_ROAD='' or ANY_PRIOR_LOSSES=''
				  or ANY_PRIOR_LOSSES_DESC='' or CURR_RES_TYPE='' or APPLY_PERS_UMB_POL_DESC =''">
				<tr>
					<td class="pageheader">For Underwriting Questions:</td>
				</tr>
				<xsl:call-template name="DRIVER_SUS_REVOKED" />
				<xsl:call-template name="DRIVER_SUS_REVOKED_PP_DESC" />
				<xsl:call-template name="PHY_MENTL_CHALLENGED" />
				<xsl:call-template name="PHY_MENTL_CHALLENGED_PP_DESC" />
				<xsl:call-template name="ANY_FINANCIAL_RESPONSIBILITY" />
				<xsl:call-template name="ANY_FINANCIAL_RESPONSIBILITY_PP_DESC" />
				<xsl:call-template name="MULTI_POLICY_DISC_APPLIED" />
				<xsl:call-template name="MULTI_POLICY_DISC_APPLIED_PP_DESC" />
				<xsl:call-template name="FULLNAME" />
				<xsl:call-template name="DATE_OF_BIRTH" />
				<xsl:call-template name="IS_EXTENDED_FORKS" />
				<xsl:call-template name="IS_COMMERCIAL_USE" />
				<xsl:call-template name="ANY_NON_OWNED_VEH" />
				<xsl:call-template name="IS_USEDFOR_RACING" />
				<xsl:call-template name="IS_TAKEN_OUT" />
				<xsl:call-template name="IS_MORE_WHEELS" />
				<xsl:call-template name="IS_CONVICTED_CARELESS_DRIVE" />
				<xsl:call-template name="IS_COST_OVER_DEFINED_LIMIT" />
				<xsl:call-template name="EXISTING_DMG" />
				<xsl:call-template name="COVERAGE_DECLINED" />
				<xsl:call-template name="SALVAGE_TITLE" />
				<xsl:call-template name="IS_LICENSED_FOR_ROAD" />
				<xsl:call-template name="ANY_PRIOR_LOSSES" />
				<xsl:call-template name="ANY_PRIOR_LOSSES_DESC" />
				<xsl:call-template name="CURR_RES_TYPE" />
				<xsl:call-template name="APPLY_PERS_UMB_POL_DESC" />
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
	<!-- ============================ Templates =============================================== -->
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
	<!-- ================================== Motorcycle Info (start) ========================================== -->
	<xsl:template name="VIN">
		<xsl:choose>
			<xsl:when test="VIN=''">
				<tr>
					<td class="midcolora">Please select/insert VIN Number.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="VEHICLE_YEAR">
		<xsl:choose>
			<xsl:when test="VEHICLE_YEAR=''">
				<tr>
					<td class="midcolora">Please insert Year.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MAKE">
		<xsl:choose>
			<xsl:when test="MAKE=''">
				<tr>
					<td class="midcolora">Please select/insert Make of Vehicle.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MODEL">
		<xsl:choose>
			<xsl:when test="MODEL=''">
				<tr>
					<td class="midcolora">Please select/insert Model of Vehicle.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="GRG_ADD1">
		<xsl:choose>
			<xsl:when test="GRG_ADD1=''">
				<tr>
					<td class="midcolora">Please insert Address 1.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="GRG_CITY">
		<xsl:choose>
			<xsl:when test="GRG_CITY=''">
				<tr>
					<td class="midcolora">Please insert City.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="GRG_STATE">
		<xsl:choose>
			<xsl:when test="GRG_STATE=''">
				<tr>
					<td class="midcolora">Please select State.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="GRG_ZIP">
		<xsl:choose>
			<xsl:when test="GRG_ZIP=''">
				<tr>
					<td class="midcolora">Please insert Zip.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="AMOUNT">
		<xsl:choose>
			<xsl:when test="AMOUNT=''">
				<tr>
					<td class="midcolora">Please insert Amount.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SYMBOL">
		<xsl:choose>
			<xsl:when test="SYMBOL=''">
				<tr>
					<td class="midcolora">Please insert Symbol.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--ADDED BY PRAVEEN KUMAR(11-12-2008):ITRACK 5121 -->
	<xsl:template name="CYCL_REGD_ROAD_USE">
		<xsl:choose>
			<xsl:when test="CYCL_REGD_ROAD_USE=''">
				<tr>
					<td class="midcolora">Please select Is cycle registered for Road Use.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--END PRAVEEN KUMAR-->
	<xsl:template name="MOTORCYCLE_TYPE">
		<xsl:choose>
			<xsl:when test="MOTORCYCLE_TYPE='0' or MOTORCYCLE_TYPE=''">
				<tr>
					<td class="midcolora">Please select Motorcycle Type.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="TERRITORY">
		<xsl:choose>
			<xsl:when test="TERRITORY=''">
				<tr>
					<td class="midcolora">Please insert Territory.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="VEHICLE_CC">
		<xsl:choose>
			<xsl:when test="VEHICLE_CC='' or VEHICLE_CC='0'">
				<tr>
					<td class="midcolora">Please insert CC.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="VEHICLE_USE">
		<xsl:choose>
			<xsl:when test="VEHICLE_USE=''">
				<tr>
					<td class="midcolora">Please select Vehicle Use.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- ================================== Motorcycle Info (end) ========================================== -->
	<xsl:template name="EXTENDED_FORKS">
		<xsl:choose>
			<xsl:when test="EXTENDED_FORKS=''">
				<tr>
					<td class="midcolora">Please select Any cycle with extended forks over 6 inches?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="COMMERCIAL_USE">
		<xsl:param name="COMMERCIAL_USE" />
		<xsl:choose>
			<xsl:when test="COMMERCIAL_USE=''">
				<tr>
					<td class="midcolora">Please select Any commercial, business, or parade use?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ANY_NON_OWNED_VEH">
		<xsl:choose>
			<xsl:when test="ANY_NON_OWNED_VEH=''">
				<tr>
					<td class="midcolora">Please select Any cycle not solely owned?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_USEDFOR_RACING">
		<xsl:choose>
			<xsl:when test="IS_USEDFOR_RACING=''">
				<tr>
					<td class="midcolora">Please select Any cycle used for racing, hill climbing, motocross or any sporting event?</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="USEDFOR_RACING">
		<xsl:choose>
			<xsl:when test="USEDFOR_RACING=''">
				<tr>
					<td class="midcolora">Please select Any cycle used for racing, hill climbing, motocross or any sporting event?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_TAKEN_OUT">
		<xsl:choose>
			<xsl:when test="IS_TAKEN_OUT=''">
				<tr>
					<td class="midcolora"> Please select Any cycle taken out of state exceeding 30 days .</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_MORE_WHEELS">
		<xsl:choose>
			<xsl:when test="IS_MORE_WHEELS=''">
				<tr>
					<td class="midcolora">Please select Does cycle have more than 2 wheels.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DRIVER_SUS_REVOKED">
		<xsl:choose>
			<xsl:when test="DRIVER_SUS_REVOKED=''">
				<tr>
					<td class="midcolora">Please select Any drivers license been suspended, revoked Or restricted in last 5 years?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DRIVER_SUS_REVOKED_PP_DESC">
		<xsl:choose>
			<xsl:when test="DRIVER_SUS_REVOKED_PP_DESC=''">
				<tr>
					<td class="midcolora">Please insert Driver License Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PHY_MENTL_CHALLENGED">
		<xsl:choose>
			<xsl:when test="PHY_MENTL_CHALLENGED=''">
				<tr>
					<td class="midcolora">Please select Any driver physically or mentally impaired?</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PHY_MENTL_CHALLENGED_PP_DESC">
		<xsl:choose>
			<xsl:when test="PHY_MENTL_CHALLENGED_PP_DESC=''">
				<tr>
					<td class="midcolora">Please insert Driver Impairment Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_CONVICTED_CARELESS_DRIVE">
		<xsl:choose>
			<xsl:when test="IS_CONVICTED_CARELESS_DRIVE=''">
				<tr>
					<td class="midcolora">Please select In the last 5 years - have you been convicted of reckless or careless driving, DUIL, DWI?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--<xsl:template name="DRIVER_US_CITIZEN">
		<xsl:choose>
			<xsl:when test="US_CITIZEN=''">
				<tr>
					<td class="midcolora">Please select U.S. Citizen.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template> -->
	<!-- <xsl:template name="VEHICLE_AGE">
		<xsl:choose>
			<xsl:when test="VEHICLE_AGE=''">
				<tr>
					<td class="midcolora">Please insert Age.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template> -->
	<xsl:template name="EXISTING_DMG">
		<xsl:choose>
			<xsl:when test="EXISTING_DMG=''">
				<tr>
					<td class="midcolora">Please select Any existing damage to a cycle?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ANY_FINANCIAL_RESPONSIBILITY">
		<xsl:choose>
			<xsl:when test="ANY_FINANCIAL_RESPONSIBILITY=''">
				<tr>
					<td class="midcolora">Please select Any financial responsibility filing?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DATE_OF_BIRTH">
		<xsl:choose>
			<xsl:when test="DATE_OF_BIRTH=''">
				<tr>
					<td class="midcolora">Please insert/select Date Of Birth.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_EXTENDED_FORKS">
		<xsl:choose>
			<xsl:when test="IS_EXTENDED_FORKS=''">
				<tr>
					<td class="midcolora">Please select Any cycle with extended forks over 6 inches?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_COMMERCIAL_USE">
		<xsl:choose>
			<xsl:when test="IS_COMMERCIAL_USE=''">
				<tr>
					<td class="midcolora">Please select Any commercial, business, or parade use?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="FULLNAME">
		<xsl:choose>
			<xsl:when test="FULLNAME=''">
				<tr>
					<td class="midcolora">Please insert Full Name.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ANY_FINANCIAL_RESPONSIBILITY_PP_DESC">
		<xsl:choose>
			<xsl:when test="ANY_FINANCIAL_RESPONSIBILITY_PP_DESC=''">
				<tr>
					<td class="midcolora">Please insert Driver Number And Date of Filing.</td>
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
	<xsl:template name="MULTI_POLICY_DISC_APPLIED_PP_DESC">
		<xsl:choose>
			<xsl:when test="MULTI_POLICY_DISC_APPLIED_PP_DESC=''">
				<tr>
					<td class="midcolora">Please insert Multipolicy Discount Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="COVERAGE_DECLINED">
		<xsl:choose>
			<xsl:when test="COVERAGE_DECLINED=''">
				<tr>
					<td class="midcolora">Please select Has any coverage been declined, cancelled or non-renewed during the last 3 years?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SALVAGE_TITLE">
		<xsl:choose>
			<xsl:when test="SALVAGE_TITLE=''">
				<tr>
					<td class="midcolora">Please select  Salvage title.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_LICENSED_FOR_ROAD">
		<xsl:choose>
			<xsl:when test="IS_LICENSED_FOR_ROAD=''">
				<tr>
					<td class="midcolora">Please select Is any cycle not licensed for road use?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- -->
	<xsl:template name="ANY_PRIOR_LOSSES">
		<xsl:choose>
			<xsl:when test="ANY_PRIOR_LOSSES=''">
				<tr>
					<td class="midcolora">Please select Any Prior Losses.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- -->
	<xsl:template name="ANY_PRIOR_LOSSES_DESC">
		<xsl:choose>
			<xsl:when test="ANY_PRIOR_LOSSES_DESC=''">
				<tr>
					<td class="midcolora">Please insert Prior Losses Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="CURR_RES_TYPE">
		<xsl:choose>
			<xsl:when test="CURR_RES_TYPE=''">
				<tr>
					<td class="midcolora">Please select Current Residence is owned or Rented.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="APPLY_PERS_UMB_POL_DESC">
		<xsl:choose>
			<xsl:when test="APPLY_PERS_UMB_POL_DESC=''">
				<tr>
					<td class="midcolora">Please enter Personal Umbrella Policy Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- -->
	<xsl:template name="IS_COST_OVER_DEFINED_LIMIT">
		<xsl:choose>
			<xsl:when test="IS_COST_OVER_DEFINED_LIMIT=''">
				<tr>
					<td class="midcolora">Please select Any cycle have cost new or value over $30,000?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- ======================================== Application Info ======================================= -->
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
	<xsl:template name="APP_EXPIRATION_DATE">
		<xsl:choose>
			<xsl:when test="APP_EXPIRATION_DATE='' or APP_EXPIRATION_DATE='NULL'">
				<tr>
					<td class="midcolora">Please insert Expiration Date.</td>
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
	<xsl:template name="PROXY_SIGN_OBTAINED">
		<xsl:choose>
			<xsl:when test="PROXY_SIGN_OBTAINED='-1' or PROXY_SIGN_OBTAINED=''">
				<tr>
					<td class="midcolora">Please select Proxy Signature Obtained?.</td>
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
	<xsl:template name="CHARGE_OFF_PRMIUM">
		<xsl:choose>
			<xsl:when test="CHARGE_OFF_PRMIUM='' or CHARGE_OFF_PRMIUM='NULL'">
				<tr>
					<td class="midcolora">Please select Charge Off Premium.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_MODIFIED_KIT">
		<xsl:choose>
			<xsl:when test="IS_MODIFIED_KIT=''">
				<tr>
					<td class="midcolora">Please select Any cycle on modified frame, a kit or assembled bike?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_MODIFIED_INCREASE_SPEED">
		<xsl:choose>
			<xsl:when test="IS_MODIFIED_INCREASE_SPEED=''">
				<tr>
					<td class="midcolora">Please select Any cycle modified to increase speed?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- ================= Additional Interest ========================================= -->
	<!--
	<xsl:template name="HOLDER_ADD1">
		<xsl:choose>
			<xsl:when test="HOLDER_ADD1=''">
				<tr>
					<td class="midcolora">Please insert Holder address.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HOLDER_CITY">
		<xsl:choose>
			<xsl:when test="HOLDER_CITY=''">
				<tr>
					<td class="midcolora">Please insert Holder city.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HOLDER_STATE">
		<xsl:choose>
			<xsl:when test="HOLDER_STATE=''">
				<tr>
					<td class="midcolora">Please select Holder State.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HOLDER_ZIP">
		<xsl:choose>
			<xsl:when test="HOLDER_ZIP=''">
				<tr>
					<td class="midcolora">Please insert Holder Zip.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="NATURE_OF_INTEREST">
		<xsl:choose>
			<xsl:when test="NATURE_OF_INTEREST=''">
				<tr>
					<td class="midcolora">Please select Nature of Interest.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HOLDER_NAME">
		<xsl:choose>
			<xsl:when test="HOLDER_NAME=''">
				<tr>
					<td class="midcolora">Please insert/select Selected Holder/Interest.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	-->
	<!-- ==================== Driver info msg ================================== -->
	<xsl:template name="DATE_LICENSED">
		<xsl:choose>
			<xsl:when test="DATE_LICENSED='' or DATE_LICENSED='null'">
				<tr>
					<td class="midcolora">Please insert Date licensed.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DRIVER_FNAME">
		<xsl:choose>
			<xsl:when test="DRIVER_FNAME=''">
				<tr>
					<td class="midcolora">Please insert First Name.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--ADDED BY PRAVEEN KUMAR FOR ITRACK ISSUE 5118 ON 9-12-2008  -->
	<xsl:template name="DRIVER_DRIV_LIC">
		<xsl:choose>
			<xsl:when test="normalize-space(DRIVER_DRIV_LIC)=''">
				<tr>
					<td class="midcolora">Please insert License Number.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DRIVER_DRINK_VIOLATION">
		<xsl:choose>
			<xsl:when test="normalize-space(DRIVER_DRINK_VIOLATION)=''">
				<tr>
					<td class="midcolora">Please select Drinking or Drug related violation in last 5 years.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- END BY PRAVEEN KUMAR  -->
	<xsl:template name="DRIVER_LNAME">
		<xsl:choose>
			<xsl:when test="DRIVER_LNAME=''">
				<tr>
					<td class="midcolora">Please insert Last Name.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DRIVER_CODE">
		<xsl:choose>
			<xsl:when test="DRIVER_CODE=''">
				<tr>
					<td class="midcolora">Please insert Code.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DRIVER_SEX">
		<xsl:choose>
			<xsl:when test="DRIVER_SEX=''">
				<tr>
					<td class="midcolora">Please select Gender.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DRIVER_ADD1">
		<xsl:choose>
			<xsl:when test="DRIVER_ADD1=''">
				<tr>
					<td class="midcolora">Please insert Address1.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DRIVER_CITY">
		<xsl:choose>
			<xsl:when test="DRIVER_CITY=''">
				<tr>
					<td class="midcolora">Please insert City.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DRIVER_STATE">
		<xsl:choose>
			<xsl:when test="DRIVER_STATE=''">
				<tr>
					<td class="midcolora">Please select State.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DRIVER_ZIP">
		<xsl:choose>
			<xsl:when test="DRIVER_ZIP=''">
				<tr>
					<td class="midcolora">Please insert Zip.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DRIVER_DOB">
		<xsl:choose>
			<xsl:when test="DRIVER_DOB=''">
				<tr>
					<td class="midcolora">Please insert Date of Birth.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DRIVER_LIC_STATE">
		<xsl:choose>
			<xsl:when test="DRIVER_LIC_STATE=''">
				<tr>
					<td class="midcolora">Please select License State.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--xsl:template name="RELATIONSHIP">
		<xsl:choose>
			<xsl:when test="RELATIONSHIP='' or RELATIONSHIP='0'">
				<tr>
					<td class="midcolora">Please insert Relationship.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template-->
	<xsl:template name="VEHICLE_ID">
		<xsl:choose>
			<xsl:when test="VEHICLE_ID='0' or VEHICLE_ID='' ">
				<tr>
					<td class="midcolora">Please assign vehicle to driver.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="APP_VEHICLE_PRIN_OCC_ID">
		<xsl:choose>
			<xsl:when test="APP_VEHICLE_PRIN_OCC_ID=''">
				<tr>
					<td class="midcolora">Please select Principal/Occasional driver.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DRIVER_US_CITIZEN">
		<xsl:choose>
			<xsl:when test="DRIVER_US_CITIZEN=''">
				<tr>
					<td class="midcolora">Please select U.S. Citizen.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DRIVER_DRIV_TYPE">
		<xsl:choose>
			<xsl:when test="DRIVER_DRIV_TYPE=''">
				<tr>
					<td class="midcolora">Please select Driver Type.</td>
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
	<!-- ================================= Template for showing Info at the initial stage ================ -->
	<xsl:template name="INTMOTORCYCLEDETAIL">
		<!-- Application List -->
		<xsl:call-template name="APPLICATION_LIST_MESSAGESGES" />
		<!-- Vehicle -->
		<xsl:call-template name="MOTORCYCLEMESSAGE" />
		<!-- Additional Interest -->
		<!-- <xsl:call-template name="ADDITIONALINTMESSAGE"/> -->
		<!--Driver  -->
		<xsl:call-template name="DRIVERMESSAGE" />
		<!-- MVR Info -->
		<!-- <xsl:call-template name="MVRMESSAGE"/> -->
		<!-- Gen Info -->
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO/IS_RECORD_EXISTS='Y'">
				<xsl:call-template name="GENINFOMESSAGE" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:apply-templates select="INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO" />
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
	<!-- =================================== General Info ============================ -->
	<xsl:template name="GENINFOMESSAGE">
		<tr>
			<td class="pageheader">For Underwriting Questions: </td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any cycle not solely owned.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Any cycle not solely owned? is 'Yes', then Please insert Vehicle Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any existing damage to a cycle?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Any existing damage to a cycle? is 'Yes', then Please insert Damaged Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any other insurance with this company?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Any other insurance with this company? is 'Yes', then Please insert Auto Insurance Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any drivers license been suspended, revoked Or restricted in last 5 years?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Any drivers license been suspended, revoked Or restricted in last 5 years? is 'Yes', then Please insert Driver License Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any driver physically or mentally impaired?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Any driver physically or mentally impaired? is 'Yes', then Please insert Driver Impairment Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any financial responsibility filing?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Any financial responsibility filing? is 'Yes', then Please insert Driver Number And Date of Filing.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Has insurance been transferred within the agency?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Has insurance been transferred within the agency? is 'Yes', then Please insert Insurance Transfer Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Has any coverage been declined, cancelled or non-renewed during the last 3 years?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Has any coverage been declined, cancelled or non-renewed during the last 3 years? is 'Yes', then Please insert Coverage Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Has the agent inspected the cycle?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Has the agent inspected the cycle? is 'Yes', then Please insert Agent Inspection Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Salvage title.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Salvage title is 'Yes', then Please insert Salvage Title Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any commercial, business, or parade use?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Any commercial, business, or parade use? is 'Yes', then Please insert Commercial Use Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any cycle used for racing, hill climbing, motocross or any sporting event?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Any cycle used for racing, hill climbing, motocross or any sporting event? is 'Yes', then Please insert Racing Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any cycle have cost new or value over $40,000?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Any cycle have cost new or value over $40,000? is 'Yes', then Please insert Limit Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select  Does cycle have more than 2 wheels and/or a side car?.</td>
		</tr>
		<tr>
			<td class="midcolora">If,Does cycle have more than 2 wheels and/or a side car? is 'Yes', then Please insert Wheel Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any cycle with extended forks over 6 inches?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Any cycle with extended forks over 6 inches? is 'Yes', then Please insert Forks Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is any cycle not licensed for road use?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Is any cycle not licensed for road use? is 'Yes', then Please insert Road License Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any cycle modified to increase speed?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Any cycle modified to increase speed? is 'Yes', then Please insert Speed Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any cycle on modified frame, a kit or assembled bike?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Any cycle on modified frame, a kit or assembled bike? is 'Yes', then Please insert Modification Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select In the last 5 years - have you been convicted of reckless or careless driving, DUIL, DWI?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, In the last 5 years - have you been convicted of reckless or careless driving, DUIL, DWI? is 'Yes', then Please insert Drive Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select In the last 5 years - have you been convicted of leaving scene of accident or drug use?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, In the last 5 years - have you been convicted of leaving scene of accident or drug use? is 'Yes', then Please insert Accident Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is multipolicy discount applied?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Is multipolicy discount applied? is 'Yes', then Please insert Multipolicy Discount Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Current Residence is Owned or Rented?.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Anyone other than insured listed, living in household?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Anyone other than insured listed, living in household? is 'Yes', then Please insert Full Name and Date Of Birth.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any prior losses?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Any prior losses? is 'Yes', then Please insert Any prior losses description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Does the insured have or applying for a Personal Umbrella Policy?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Does the insured have or applying for a Personal Umbrella Policy? is 'Yes', then Please insert Personal Umbrella Policy Description.</td>
		</tr>
	</xsl:template>
	<!-- =========================== Motorcycle Info =================================== -->
	<xsl:template name="MOTORCYCLEMESSAGE">
		<tr>
			<td class="pageheader">You must have at least one active motorcycle with following information:</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Year.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select/insert Make of Motorcycle.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select/insert Model of Motorcycle.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Motorcycle Type.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert CC.</td>
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
			<td class="midcolora">Please insert Territory.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Amount.</td>
		</tr>
		<!--ADDED BY PRAVEEN KUMAR(11-12-2008):ITRACK 5121 -->
		<tr>
			<td class="midcolora">Please select Is cycle registered for Road Use.</td>
		</tr>
		<!--END PRAVEEN KUMAR-->
	</xsl:template>
	<!-- ============================ Additional interest ================================= -->
	<!-- <xsl:template name="ADDITIONALINTMESSAGE">
		<tr>
			<td class="pageheader">For Additional Interest:</td>
		</tr>
		<tr>
			<td class="midcolora">Please select/insert Selected Holder/Interest.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Holder Address.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Holder City.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Holder State.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Holder Zip.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Nature Of Interest/Legal Entity.</td>
		</tr>
	</xsl:template> -->
	<!-- ============================= Driver Info ============================================== -->
	<xsl:template name="DRIVERMESSAGE">
		<tr>
			<td class="pageheader">You must have at least one active driver with following information:</td>
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
			<td class="midcolora">Please select Gender.</td>
		</tr>
		<!--tr>
			<td class="midcolora">Please insert Address1.</td>
		</tr-->
		<!--tr>
			<td class="midcolora">Please insert City.</td>
		</tr-->
		<tr>
			<td class="midcolora">Please select State.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Zip.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Date of Birth.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select License State.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert License Number.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Drinking or Drug related violation in last 5 years.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Date Licensed.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Driver Type.</td>
		</tr>
		<!--tr>
			<td class="midcolora">Please select Relationship.</td>
		</tr-->
		<tr>
			<td class="midcolora">Please assign vehicle to driver.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Principal/Occasional driver.</td>
		</tr>
	</xsl:template>
	<!-- ========================== MVR Information ================================== -->
	<xsl:template name="MVRMESSAGE">
		<tr>
			<td class="pageheader">For MVR Information:</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Violation.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert date.</td>
		</tr>
	</xsl:template>
</xsl:stylesheet>
