<!-- ==================================================================================================
File Name			:	UWRulesInputMsg.xsl
Purpose				:	To display Messages for Umbrella 
Name				:	Ashwani
Date				:	13 Oct. 2006  
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
		public int AssignUnderwritter( int assignvalue)
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
						<LINK id="lk" href="/cms/cmsweb/css/css1.css"
							type="text/css" rel="stylesheet" />
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
					</xsl:if>
					<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/CALLED_FROM=1">
						<td class="pageheader">Policy Version :</td>
					</xsl:if>
					<td class="midcolora">
						<xsl:value-of select="INPUTXML/APPLICATIONS/APPLICATION/APP_VERSION_NO" />
					</td>
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
				<xsl:choose>
					<xsl:when test="INPUTXML/UMGENINFOS/UMGENINFO/IS_RECORD_EXISTS='Y' ">
						<xsl:if test="user:AssignUnderwritter(0) = 0" />
						<xsl:call-template name="UMDETAIL" />
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
		<!-- App Info -->
		<xsl:apply-templates select="INPUTXML/APPLICATIONS/APPLICATION"></xsl:apply-templates>
		<!--Schedule of Underlying -->
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULES/SCHEDULE/IS_RECORD_EXISTS='N'">
				<xsl:apply-templates select="INPUTXML/SCHEDULES/SCHEDULE"></xsl:apply-templates>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
				<xsl:call-template name="SCHEDULEMESSAGE" />
			</xsl:otherwise>
		</xsl:choose>
		<!-- End -->
		<!-- Excess Limit -->
		<xsl:choose>
			<xsl:when test="INPUTXML/EXCESSLIMITS/EXCESSLIMIT/IS_RECORD_EXISTS='N'">
				<xsl:apply-templates select="INPUTXML/EXCESSLIMITS/EXCESSLIMIT"></xsl:apply-templates>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
				<xsl:call-template name="EXCESSLIMITMESSAGE" />
			</xsl:otherwise>
		</xsl:choose>
		<!-- Gen Info  -->
		<xsl:choose>
			<xsl:when test="INPUTXML/UMGENINFOS/UMGENINFO/IS_RECORD_EXISTS='N'">
				<xsl:apply-templates select="INPUTXML/UMGENINFOS/UMGENINFO" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
				<xsl:call-template name="UNDERWRITING_QUES_UM" />
			</xsl:otherwise>
		</xsl:choose>
		<!--Location Info -->
		<xsl:apply-templates select="INPUTXML/LOCATIONS/LOCATION"></xsl:apply-templates>
		<!--Watercraft Info -->
		<xsl:apply-templates select="INPUTXML/BOATS/BOAT"></xsl:apply-templates>
		<!--R Vehicle -->
		<xsl:apply-templates select="INPUTXML/RVEHICLES/RVEHICLE"></xsl:apply-templates>
	</xsl:template>
	<!--start  ============================== RV Detail =============================== -->
	<xsl:template match="INPUTXML/RVEHICLES/RVEHICLE">
		<xsl:choose>
			<xsl:when test="COMPANY_ID_NUMBER='' or YEAR='' or MAKE='' or MODEL='' or VEHICLE_TYPE=''or
			VEHICLE_MODIFIED_DETAILS='' or RF_STATE_REGISTERED='' or USED_IN_RACE_SPEED='' 
			or USED_IN_RACE_SPEED_CONTEST ='' or VEHICLE_MODIFIED='' or REC_VEH_TYPE='' ">
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
	<!--end  ============================== RV Detail =============================== -->
	<!--start  ============================== Boats Detail =============================== -->
	<xsl:template match="INPUTXML/BOATS/BOAT">
		<xsl:choose>
			<xsl:when test="BOAT_NO='' or MAX_SPEED='' or TYPE_OF_WATERCRAFT='' or YEAR='' or 
			LENGTH='' or WATERS_NAVIGATED='' or INSURING_VALUE='' or WATERCRAFT_CONTEST='' or 
			USED_PARTICIPATE='' ">
				<tr>
					<td class="pageheader">For Boat Information: </td>
					<tr>
						<td>
							<table width="60%">
								<tr>
									<xsl:if test="BOAT_NO!= ''">
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
				<xsl:call-template name="BOATS_INFO_MESSAGES" />
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
	<!--end    ============================== Boats Detail =============================== -->
	<!--start  ============================== Location Detail =============================== -->
	<xsl:template match="INPUTXML/LOCATIONS/LOCATION">
		<xsl:choose>
			<xsl:when test="BUSS_FARM_PURSUITS='' or PERS_INJ_COV_82='' or NUM_FAMILIES=''
				or LOC_NUM=''">
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
				<xsl:call-template name="LOCATIONS_INFO_MESSAGES" />
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
	<!--end  ============================== Location Detail ================================= -->
	<!-- ============================== Application Detail ================================= -->
	<xsl:template match="INPUTXML/APPLICATIONS/APPLICATION">
		<xsl:choose>
			<xsl:when test="STATE_ID=-1 or STATE_ID='' or  APP_LOB='' or APP_TERMS='' or APP_EFFECTIVE_DATE=''  or
				APP_EXPIRATION_DATE='' or APP_AGENCY_ID=-1 or APP_AGENCY_ID='' or BILL_TYPE='' or 
				PROXY_SIGN_OBTAINED=-1	or PROXY_SIGN_OBTAINED=''  or CHARGE_OFF_PRMIUM='' or IS_PRIMARY_APPLICANT='' or	
				IS_PRIMARY_APPLICANT='Y' or PIC_OF_LOC='' or PROPRTY_INSP_CREDIT='' or  POL_UNDERWRITER=''">
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
				<xsl:call-template name="APPLICATION_INFO_MESSAGES" />
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
	<xsl:template match="INPUTXML/UMGENINFOS/UMGENINFO">
		<xsl:choose>
			<xsl:when test="ENGAGED_IN_FARMING='' or ENGAGED_IN_FARMING_DESC='' or REDUCED_LIMIT_OF_LIBLITY='' 
			or REDUCED_LIMIT_OF_LIBLITY_DESC =''or WAT_DWELL='' or WAT_DWELL_DESC='' or RECR_VEH='' or 
			RECR_VEH_DESC='' or  HOME_RENT_DWELL ='' or HOME_RENT_DWELL_DESC ='' or AUTO_CYCL_TRUCKS='' 
			or AUTO_CYCL_TRUCKS_DESC='' or REAL_STATE_VEHICLE_USED = '' or REAL_STATE_VEHICLE_USED_DESC='' or 
			REAL_STATE_VEH_OWNED_HIRED ='' or REAL_STATE_VEH_OWNED_HIRED_DESC='' or PENDING_LITIGATIONS ='' 
			or PENDING_LITIGATIONS_DESC='' or ANY_FULL_TIME_EMPLOYEE= '' or ANY_FULL_TIME_EMPLOYEE_DESC='' 
			or ANY_COVERAGE_DECLINED='' or ANY_COVERAGE_DECLINED_DESC='' or ANY_AIRCRAFT_OWNED_LEASED ='' 
			or ANY_AIRCRAFT_OWNED_LEASED_DESC='' or BUSINESS_PROF_ACTIVITY='' or BUSINESS_PROF_ACTIVITY_DESC=''
			
			or ANY_OPERATOR_CON_TRAFFIC_DESC='' or ANY_OPERATOR_IMPIRED_DESC='' or ANY_SWIMMING_POOL_DESC='' or 
			HOLD_NON_COMP_POSITION_DESC=''  or NON_OWNED_PROPERTY_CARE_DESC =''  or ANIMALS_EXOTIC_PETS_DESC=''
			or INSU_TRANSFERED_IN_AGENCY_DESC ='' or IS_TEMPOLINE_DESC='' or HAVE_NON_OWNED_AUTO_POL_DESC=''
			or HOME_DAY_CARE_DESC='' or INS_DOMICILED_OUTSIDE_DESC='' or APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC=''
			or FAMILIES='' ">
				<tr>
					<td class="pageheader">For Underwriting Questions:</td>
				</tr>
				<xsl:call-template name="UM_UQUESTIONS" />
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
<!-- Schedule Of Underlying-->
	<xsl:template match="INPUTXML/SCHEDULES/SCHEDULE">
		<xsl:choose>
			<xsl:when test="POLICY_LOB='' or POLICY_NUMBER='' or POLICY_START_DATE = '' or POLICY_EXPIRATION_DATE= '' ">
				<tr>
					<td class="pageheader">For Schedule Of Underlying:</td>
				</tr>
				<xsl:call-template name="UM_SCHEDULEQUESTIONS" />
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
<!-- Excess Limits and Endorsements-->
	<xsl:template match="INPUTXML/EXCESSLIMITS/EXCESSLIMIT">
		<xsl:choose>
			<xsl:when test="POLICY_LIMITS='' or POLICY_LIMITS=0 or TERRITORY='' or TERRITORY=0">
				<tr>
					<td class="pageheader">For Excess Limits and Endorsements:</td>
				</tr>
				<xsl:call-template name="UM_EXCESSLIMITQUESTIONS" />
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
	<!-- start ====================================RV Info Messages =================================== -->
	<xsl:template name="RV_INFO_MESSAGES">
		<xsl:if test="COMPANY_ID_NUMBER=''">
			<tr>
				<td class="midcolora">Please insert RV #.</td>
			</tr>
		</xsl:if>
		<xsl:if test="YEAR=''">
			<tr>
				<td class="midcolora">Please insert Year.</td>
			</tr>
		</xsl:if>
		<xsl:if test="MAKE=''">
			<tr>
				<td class="midcolora">Please insert Make.</td>
			</tr>
		</xsl:if>
		<xsl:if test="MODEL=''">
			<tr>
				<td class="midcolora">Please insert Model.</td>
			</tr>
		</xsl:if>
		<xsl:if test="RF_STATE_REGISTERED=''">
			<tr>
				<td class="midcolora">Please select State Registered.</td>
			</tr>
		</xsl:if>
		<xsl:if test="VEHICLE_TYPE=''">
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
		<xsl:if test="USED_IN_RACE_SPEED=''">
			<tr>
				<td class="midcolora">Please select Used to participate in any race or speed contest.</td>
			</tr>
		</xsl:if>
		<xsl:if test="USED_IN_RACE_SPEED_CONTEST=''">
			<tr>
				<td class="midcolora">Please insert Recreational Vehicle Contest Description.</td>
			</tr>
		</xsl:if>
	</xsl:template>
	<!-- end ====================================RV Info Messages =================================== -->
	<!-- start ====================================Boats Info Messages =================================== -->
	<xsl:template name="BOATS_INFO_MESSAGES">
		<xsl:if test="BOAT_NO=''">
			<tr>
				<td class="midcolora">Please insert Boat Number.</td>
			</tr>
		</xsl:if>
		<xsl:if test="MAX_SPEED=''">
			<tr>
				<td class="midcolora">Please insert Maximum Speed.</td>
			</tr>
		</xsl:if>
		<xsl:if test="TYPE_OF_WATERCRAFT=''">
			<tr>
				<td class="midcolora">Please select Type of Watercraft.</td>
			</tr>
		</xsl:if>
		<xsl:if test="YEAR=''">
			<tr>
				<td class="midcolora">please insert Year.</td>
			</tr>
		</xsl:if>
		<xsl:if test="LENGTH=''">
			<tr>
				<td class="midcolora">Please insert Length.</td>
			</tr>
		</xsl:if>
		<!--xsl:if test="WATERS_NAVIGATED=''">
			<tr>
				<td class="midcolora">Please select Waters Navigated.</td>
			</tr>
		</xsl:if-->
		<xsl:if test="INSURING_VALUE=''">
			<tr>
				<td class="midcolora"> Please insert Insuring Value (incl. motor).</td>
			</tr>
		</xsl:if>
		<xsl:if test="USED_PARTICIPATE=''">
			<tr>
				<td class="midcolora">Please select Watercraft Used to participate in any race, speed or fishing                        contest.</td>
			</tr>
		</xsl:if>
		<xsl:if test="WATERCRAFT_CONTEST=''">
			<tr>
				<td class="midcolora">Please insert Watercraft Contest.</td>
			</tr>
		</xsl:if>
	</xsl:template>
	<!-- end ====================================Boats Info Messages ===================================== -->
	<!-- start====================================Location Info Messages ===================================== -->
	<xsl:template name="LOCATIONS_INFO_MESSAGES">
		<xsl:if test="BUSS_FARM_PURSUITS=''">
			<tr>
				<td class="midcolora">Please select Business/Farming Pursuits.</td>
			</tr>
		</xsl:if>
		<xsl:if test="PERS_INJ_COV_82=''">
			<tr>
				<td class="midcolora">Please select Do you carry Personal Injury Coverage - HO-82.</td>
			</tr>
		</xsl:if>
		<xsl:if test="NUM_FAMILIES=''">
			<tr>
				<td class="midcolora">Please insert # of Families.</td>
			</tr>
		</xsl:if>
		<xsl:if test="LOC_NUM=''">
			<tr>
				<td class="midcolora">Please insert Location Number.</td>
			</tr>
		</xsl:if>
	</xsl:template>
	<!-- end====================================Location Info Messages ===================================== -->
	<!-- start====================================Application Info ===================================== -->
	<xsl:template name="APPLICATION_INFO_MESSAGES">
		<xsl:if test="STATE_ID=''">
			<tr>
				<td class="midcolora">Please select State.</td>
			</tr>
		</xsl:if>
		<xsl:if test="APP_LOB=''">
			<tr>
				<td class="midcolora">Please select Line of Business.</td>
			</tr>
		</xsl:if>
		<xsl:if test="APP_TERMS=''">
			<tr>
				<td class="midcolora">Please select Policy Term Months.</td>
			</tr>
		</xsl:if>
		<xsl:if test="APP_EFFECTIVE_DATE=''">
			<tr>
				<td class="midcolora">Please insert Effective Date.</td>
			</tr>
		</xsl:if>
		<xsl:if test="APP_EXPIRATION_DATE=''">
			<tr>
				<td class="midcolora">Please insert Expiration Date.</td>
			</tr>
		</xsl:if>
		<xsl:if test="APP_AGENCY_ID=''">
			<tr>
				<td class="midcolora">Please select Agency.</td>
			</tr>
		</xsl:if>
		<xsl:if test="BILL_TYPE=''">
			<tr>
				<td class="midcolora">Please select Bill Type.</td>
			</tr>
		</xsl:if>
		<xsl:if test="PROXY_SIGN_OBTAINED=''">
			<tr>
				<td class="midcolora">Please select Proxy Signature Obtained?.</td>
			</tr>
		</xsl:if>
		<xsl:if test="IS_PRIMARY_APPLICANT=''">
			<tr>
				<td class="midcolora">You must have at least one primary applicant.</td>
			</tr>
		</xsl:if>
		<xsl:if test="CHARGE_OFF_PRMIUM=''">
			<tr>
				<td class="midcolora">Please select Charge Off Premium.</td>
			</tr>
		</xsl:if>
		<xsl:if test="PIC_OF_LOC=''">
			<tr>
				<td class="midcolora">Please select Picture of Location attached?.</td>
			</tr>
		</xsl:if>
		<xsl:if test="PROPRTY_INSP_CREDIT=''">
			<tr>
				<td class="midcolora">Please select Property Inspection/Cost Estimator.</td>
			</tr>
		</xsl:if>
		<xsl:if test="POL_UNDERWRITER=''">
			<tr>
				<td class="midcolora">Please select Underwriter.</td>
			</tr>
		</xsl:if>
	</xsl:template>
	<!-- end====================================Application Info ===================================== -->
	<!-- start====================================Underwriting Questions ===================================== -->
	<xsl:template name="UM_UQUESTIONS">
		<xsl:if test="ANY_AIRCRAFT_OWNED_LEASED=''">
			<tr>
				<td class="midcolora">Please select Are any aircraft owned, leased, chartered or furnished for								regular use?.</td>
			</tr>
		</xsl:if>
		<xsl:if test="ANY_AIRCRAFT_OWNED_LEASED_DESC=''">
			<tr>
				<td class="midcolora">Please insert Aircraft Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="BUSINESS_PROF_ACTIVITY_DESC=''">
			<tr>
				<td class="midcolora">Please insert Business/Professional Primary Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="ANY_OPERATOR_CON_TRAFFIC_DESC=''">
			<tr>
				<td class="midcolora">Please insert Traffic Violation Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="ANY_OPERATOR_IMPIRED_DESC=''">
			<tr>
				<td class="midcolora">Please insert Impaired Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="ANY_SWIMMING_POOL_DESC=''">
			<tr>
				<td class="midcolora">Please insert Pool/Tub or Spa Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="HOLD_NON_COMP_POSITION_DESC=''">
			<tr>
				<td class="midcolora">Please insert Non-compensated Positions Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="NON_OWNED_PROPERTY_CARE_DESC=''">
			<tr>
				<td class="midcolora">Please insert Non-Owned Property Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="ANIMALS_EXOTIC_PETS_DESC=''">
			<tr>
				<td class="midcolora">Please insert Exotic Pets Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="INSU_TRANSFERED_IN_AGENCY_DESC=''">
			<tr>
				<td class="midcolora">Please insert Transferred Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="IS_TEMPOLINE_DESC=''">
			<tr>
				<td class="midcolora">Please insert Trampoline Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="HAVE_NON_OWNED_AUTO_POL_DESC=''">
			<tr>
				<td class="midcolora">Please insert Non Owned Auto Policy Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="INS_DOMICILED_OUTSIDE_DESC=''">
			<tr>
				<td class="midcolora">Please insert Permanently Domiciled Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="HOME_DAY_CARE_DESC=''">
			<tr>
				<td class="midcolora">Please insert Home Day Care Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="REAL_STATE_VEHICLE_USED=''">
			<tr>
				<td class="midcolora">Please select Any real estate, vehicles, watercraft, aircraft used commercially							or for business purposes?.</td>
			</tr>
		</xsl:if>
		<xsl:if test="REAL_STATE_VEHICLE_USED_DESC=''">
			<tr>
				<td class="midcolora">Please insert Commercial/Business Use.</td>
			</tr>
		</xsl:if>
		<xsl:if test="REAL_STATE_VEH_OWNED_HIRED=''">
			<tr>
				<td class="midcolora">Please select Any real estate, vehicles, watercraft, aircraft owned, hired,leased or regularly used not covered by primary policies?.</td>
			</tr>
		</xsl:if>
		<xsl:if test="REAL_STATE_VEH_OWNED_HIRED_DESC=''">
			<tr>
				<td class="midcolora">Please insert No primary Coverage Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="ENGAGED_IN_FARMING=''">
			<tr>
				<td class="midcolora">Please select Do you engage in any type of farming operation?.</td>
			</tr>
		</xsl:if>
		<xsl:if test="ENGAGED_IN_FARMING_DESC=''">
			<tr>
				<td class="midcolora">Please insert Farming Operation Details..</td>
			</tr>
		</xsl:if>
		<xsl:if test="ANY_FULL_TIME_EMPLOYEE=''">
			<tr>
				<td class="midcolora">Please select Any full time employees?.</td>
			</tr>
		</xsl:if>
		<xsl:if test="ANY_FULL_TIME_EMPLOYEE_DESC=''">
			<tr>
				<td class="midcolora">Please insert Employee Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="BUSINESS_PROF_ACTIVITY=''">
			<tr>
				<td class="midcolora">Please select Any business or professional activities included in the primary							policies?.</td>
			</tr>
		</xsl:if>
		<xsl:if test="BUSINESS_PROF_ACTIVITY_DESC=''">
			<tr>
				<td class="midcolora">Please insert Business/Professional Primary Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="REDUCED_LIMIT_OF_LIBLITY=''">
			<tr>
				<td class="midcolora">Please select Does any primary policy have reduced limits of liability or							eliminate coverage for specific exposures or list excluded drivers?.</td>
			</tr>
		</xsl:if>
		<xsl:if test="REDUCED_LIMIT_OF_LIBLITY_DESC=''">
			<tr>
				<td class="midcolora">Please insert Reduced Limits Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="ANY_COVERAGE_DECLINED=''">
			<tr>
				<td class="midcolora">Please select Any coverage declined, cancelled or non-renewed during the last 3					years?.</td>
			</tr>
		</xsl:if>
		<xsl:if test="ANY_COVERAGE_DECLINED_DESC=''">
			<tr>
				<td class="midcolora">Please insert Decline/Cancelled Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="PENDING_LITIGATIONS=''">
			<tr>
				<td class="midcolora">Please select Any pending litigations court proceedings or judgments?.</td>
			</tr>
		</xsl:if>
		<xsl:if test="PENDING_LITIGATIONS_DESC=''">
			<tr>
				<td class="midcolora">Please insert Litigation Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="WAT_DWELL=''">
			<tr>
				<td class="midcolora">Please select Watercraft.</td>
			</tr>
		</xsl:if>
		<xsl:if test="WAT_DWELL_DESC=''">
			<tr>
				<td class="midcolora">Please insert Watercraft Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="RECR_VEH=''">
			<tr>
				<td class="midcolora">Please select Recreational Vehicles.</td>
			</tr>
		</xsl:if>
		<xsl:if test="RECR_VEH_DESC=''">
			<tr>
				<td class="midcolora">Please insert Recreational Vehicles Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="FAMILIES=''">
			<tr>
				<td class="midcolora">Please insert Number of Families.</td>
			</tr>
		</xsl:if>
		<xsl:if test="HOME_RENT_DWELL=''">
			<tr>
				<td class="midcolora">Please select  Home/Rental Dwellings.</td>
			</tr>
		</xsl:if>
		<xsl:if test="HOME_RENT_DWELL_DESC=''">
			<tr>
				<td class="midcolora">Please insert Home/Rental Dwelling Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="AUTO_CYCL_TRUCKS=''">
			<tr>
				<td class="midcolora">Please select Automobiles/Cycles/Trucks.</td>
			</tr>
		</xsl:if>
		<xsl:if test="AUTO_CYCL_TRUCKS_DESC=''">
			<tr>
				<td class="midcolora">Please insert Auto/cycle/truck Details.</td>
			</tr>
		</xsl:if>
		<xsl:if test="APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC=''">
			<tr>
				<td class="midcolora">Please insert Understand Coverage Details.</td>
			</tr>
		</xsl:if>
	</xsl:template>
	<!-- ====================================Application info ========================================== -->
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

	<!--================ Template for showing Info at the initial stage (Mandatory only) ========================-->
	<xsl:template name="UMDETAIL">
		<!--================================ Application List ===================================================-->
		<xsl:call-template name="APPLICATION_LIST_MESSAGESGES" />
		<!--xsl:call-template name="EXCESSLIMITMESSAGE" /-->
		<!--================================ Underwriting Questions =============================================-->
		<xsl:choose>
			<xsl:when test="INPUTXML/UMGENINFOS/UMGENINFO/IS_RECORD_EXISTS='N'">
				<xsl:apply-templates select="INPUTXML/UMGENINFOS/UMGENINFO" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
				<xsl:call-template name="UNDERWRITING_QUES_UM" />
			</xsl:otherwise>
		</xsl:choose>
		<!-- Schedule of Underlying-->
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULES/SCHEDULE/IS_RECORD_EXISTS='N'">
				<xsl:apply-templates select="INPUTXML/SCHEDULES/SCHEDULE" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
				<xsl:call-template name="SCHEDULEMESSAGE" />
			</xsl:otherwise>
		</xsl:choose>
		<!-- End-->
		<!--Excess Limit -->
		<xsl:choose>
			<xsl:when test="INPUTXML/EXCESSLIMITS/EXCESSLIMIT/IS_RECORD_EXISTS='N'">
				<xsl:apply-templates select="INPUTXML/EXCESSLIMITS/EXCESSLIMIT" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
				<xsl:call-template name="EXCESSLIMITMESSAGE" />
			</xsl:otherwise>
		</xsl:choose>
		<!-- End-->			
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
		<!--================================ Underwriting Questions Messages =====================================-->
	<xsl:template name="UNDERWRITING_QUES_UM">
		<tr>
			<td class="pageheader">For Underwriting Questions:</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Are any aircraft owned, leased, chartered or furnished for regular	use?'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Are any aircraft owned, leased, chartered or furnished for regular use?' is 'yes' then then please insert 'Aircraft Details'.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Any operators convicted for any traffic violations during the last 3 years?'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any operators convicted for any traffic violations during the last 3 years?' is 'yes' then then please insert 'Traffic Violation Details'.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Any operator physically or mentally impaired?'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any operator physically or mentally impaired?' is 'yes' then then please insert 'Impaired Details'.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Any swimming pool, hot tub or spa on premises?'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any swimming pool, hot tub or spa on premises?' is 'yes' then then please	insert	'Pool/Tub or Spa Details'.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Any real estate, vehicles, watercraft, aircraft used commercially or for business purposes?'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any real estate, vehicles, watercraft, aircraft used commercially or for	business purposes?' is 'yes' then please insert 'Commercial/Business Use'.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Any real estate, vehicles, watercraft, aircraft owned, hired, leased	or regularly used not covered by primary policies?'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any real estate, vehicles, watercraft, aircraft owned, hired, leased or	regularly used not covered by primary policies?' is 'yes' then please insert 'No primary Coverage					Details'.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Do you engage in any type of farming operation?'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Do you engage in any type of farming operation?' is 'yes' then please insert'Farming Operation Details'.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Do you hold any non-compensated positions?'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Do you hold any non-compensated positions?' is 'yes' then please insert 'Non-compensated Positions Details'.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Any full time employees?'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any full time employees?' is 'yes' then please insert 'Employee Details'.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Any non-owned property exceeding $1000 in value in your care, custody or	control?'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any non-owned property exceeding $1000 in value in your care, custody or	control?' is 'yes' then please insert 'Non-Owned Property Details'.</td>
		</tr>
		
		<tr>
			<td class="midcolora">Please select 'Any business or professional activities included in the primary policies?'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any business or professional activities included in the primary policies?' is	'yes' then please insert 'Business/Professional Primary Details'.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Does any primary policy have reduced limits of liability or eliminate coverage for specific exposures or list excluded drivers?'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Does any primary policy have reduced limits of liability or eliminate coverage for specific exposures or list excluded drivers?' is 'yes' then please insert 'Reduced Limits Details'.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Does applicant or any tenant have any animals or exotic pets?'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Does applicant or any tenant have any animals or exotic pets?' is 'yes' then  please insert 'Exotic Pets Details'.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Any coverage declined, cancelled or non-renewed during the last 3 years?'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any coverage declined, cancelled or non-renewed during the last 3 years?' is 'yes' then please insert 'Decline/Cancelled Details'.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Any pending litigations court proceedings or judgments?'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any pending litigations court proceedings or judgments?' is 'yes' then please	insert 'Litigation Details'</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Has insurance been transferred within the agency?'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Has insurance been transferred within the agency?' is 'yes' then please insert 'Transferred Details'.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Is there a Trampoline on the premises?'.</td>
		</tr>		
		<tr>
			<td class="midcolora">If 'Is there a Trampoline on the premises?' is 'yes' then please	insert 'Trampoline Details'.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Do you have a Non Owned Auto Policy?'.</td>
		</tr>		
		<tr>
			<td class="midcolora">If 'Do you have a Non Owned Auto Policy?' is 'yes' then please insert 'Non Owned Auto Policy Details'.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Is the Named insured or applicant permanently domiciled outside of Michigan or Indiana for more than 6 months?'.</td>
		</tr>		
		<tr>
			<td class="midcolora">If 'Is the Named insured or applicant permanently domiciled outside of Michigan or Indiana for more than 6 months?' is 'yes' then please insert 'Permanently Domiciled Details'.		</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Are you providing home day care service to a person other than a relative and	receiving money or other compensation?'.</td>
		</tr>		
		<tr>
			<td class="midcolora">If 'Are you providing home day care service to a person other than a relative and	receiving money or other compensation?' is 'yes' then please insert 'Home Day Care Details'.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Number of Families.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Home/Rental Dwellings'.</td>
		</tr>	
		<tr>
			<td class="midcolora">If 'Home/Rental Dwellings' is 'no' then please insert 'Home/Rental Dwelling Details'</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Watercraft'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Watercraft' is 'no' then please insert 'Watercraft Details'</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Recreational Vehicles'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Recreational Vehicles' is 'no' then please insert 'Recreational Vehicles Details'.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Automobiles/Cycles/Trucks'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Automobiles/Cycles/Trucks' is 'no' then please insert 'Auto/cycle/truck Details'.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Does applicant understand that Business Pursuits, Professional	Liability and Aircraft Liability are excluded?'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Does applicant understand that Business Pursuits, Professional Liability and	Aircraft Liability are excluded?' is 'no' then please insert 'Understand Coverage Details'.</td>
		</tr>
	</xsl:template>
	<!--Excess Limits and Endorsements -->
	<!--<xsl:template name="POLICY_LIMITS">
		<xsl:choose>
			<xsl:when test="POLICY_LIMITS= -1  ">
				<tr>
					<td class="midcolora">Please select Policy Excess Limits.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="TERRITORY">
		<xsl:choose>
			<xsl:when test="TERRITORY=-1">
				<tr>
					<td class="midcolora">Please select Territory.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>-->
	
	<!-- Schedule of Underlying-->
	<xsl:template name="UM_SCHEDULEQUESTIONS">		
		<xsl:if test="POLICY_LOB=''">
			<tr>
				<td class="midcolora">Please select Line Of Business.</td>
			</tr>
		</xsl:if>
		<xsl:if test="POLICY_NUMBER=''">
			<tr>
				<td class="midcolora">Please select Policy Number.</td>
			</tr>
		</xsl:if>
		<xsl:if test="POLICY_START_DATE=''">
			<tr>
				<td class="midcolora">Please enter Effective Date.</td>
			</tr>
		</xsl:if>
		<xsl:if test="POLICY_EXPIRATION_DATE=''">
			<tr>
				<td class="midcolora">Please enter Expiration Date.</td>
			</tr>
		</xsl:if>
	</xsl:template>	
	<!-- End Schedule of Underlying -->
	<!-- Schedule of Underlying -->
	<xsl:template name="SCHEDULEMESSAGE">
			<tr>
					<td class="pageheader">For Schedule of Underlying:</td>
			</tr>
			<tr>
					<td class="midcolora">Please select Line Of Business.</td>
			</tr>		
			<tr>
					<td class="midcolora">Please select Policy Number.</td>
			</tr>
			<tr>
					<td class="midcolora">Please enter Effective Date.</td>
			</tr>
			<tr>
					<td class="midcolora">Please enter Expiration Date.</td>
			</tr>
	</xsl:template>
	
	<!-- End Schedule of Underlying -->
		 
	<!-- Excess Limits and Endorsements -->
	<xsl:template name="UM_EXCESSLIMITQUESTIONS">		
		<xsl:if test="POLICY_LIMITS='' or POLICY_LIMITS=0">
			<tr>
				<td class="midcolora">Please select Policy Excess Limits.</td>
			</tr>
		</xsl:if>
		<xsl:if test="TERRITORY=''">
			<tr>
				<td class="midcolora">Please select Territory.</td>
			</tr>
		</xsl:if>
	</xsl:template>	
	<!--Excess Limits and Endorsements -->
	<xsl:template name="EXCESSLIMITMESSAGE">
		<tr>
			<td class="pageheader">For Excess Limit and Endorsements:</td>
		</tr>		
		<tr>
			<td class="midcolora">Please select Policy Excess Limits.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Territory.</td>
		</tr>
		
	</xsl:template>
	
</xsl:stylesheet>