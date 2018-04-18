<!-- ==================================================================================================
File Name			:	UWRulesInputMsg.xsl
Purpose				:	To display Messages for Watercraft
Name				:	Praveen
Date				:	22 Sep. 2005  
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
			<table border="2" align="center" width='100%'>
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
				<xsl:choose>
					<xsl:when test="INPUTXML/BOATS/ERRORS/BOATERROR/@ERRFOUND='T' and INPUTXML/OPERATORS/ERRORS/OPERATORERROR/@ERRFOUND='T'">
						<!-- call a template shows all the messages -->
						<xsl:if test="user:AssignUnderwritter(0) = 0"></xsl:if>
						<xsl:call-template name="WCDETAIL" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:call-template name="SHOWWCINPUTDETAIL" />
					</xsl:otherwise>
				</xsl:choose>
			</table>
		</html>
	</xsl:template>
	<!--New templates-->
	<!-- This template shows the input detail of Watercraft-->
	<xsl:template name="SHOWWCINPUTDETAIL">
		<xsl:apply-templates select="INPUTXML/APPLICATIONS/APPLICATION"></xsl:apply-templates>
		<xsl:apply-templates select="INPUTXML/MVRS/MVR"></xsl:apply-templates>
		<!-- BOAT -->
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/@COUNT > 0">
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
			<xsl:when test="INPUTXML/OPERATORS/@COUNT > 0">
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
		<xsl:call-template name="TRAILER_INFO" />
		<!-- Gen Info  -->
		<xsl:choose>
			<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/@COUNT>0">
				<xsl:apply-templates select="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
				<xsl:call-template name="GENINFOMESSAGE" />
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
	<xsl:template match="INPUTXML/APPLICATIONS/APPLICATION">
		<xsl:choose>
			<xsl:when test="STATE_ID=-1 or STATE_ID='' or APP_TERMS='' or APP_EFFECTIVE_DATE='' or APP_LOB='' or
				APP_AGENCY_ID=-1 or APP_AGENCY_ID='' or DOWN_PAY_MENT='' or BILL_TYPE='' or PROXY_SIGN_OBTAINED=-1 or PROXY_SIGN_OBTAINED=''
				 or CHARGE_OFF_PRMIUM='' or IS_PRIMARY_APPLICANT='' or IS_PRIMARY_APPLICANT='Y' or POL_UNDERWRITER=''">
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
				<!--   1   -->
				<xsl:call-template name="STATE_ID" />
				<!--   2   -->
				<xsl:call-template name="APP_LOB" />
				<!--   3   -->
				<xsl:call-template name="APP_TERMS" />
				<!--   4   -->
				<xsl:call-template name="APP_EFFECTIVE_DATE" />
				<!--   5   -->
				<xsl:call-template name="APP_EXPIRATION_DATE" />
				<!--   6   -->
				<xsl:call-template name="APP_AGENCY_ID" />
				<!--   7   -->
				<xsl:call-template name="BILL_TYPE" />
				<!--   8   -->
				<xsl:call-template name="PROXY_SIGN_OBTAINED" />
				<!--   9   -->
				<xsl:call-template name="IS_PRIMARY_APPLICANT" />
				<!--   10   -->
				<xsl:call-template name="CHARGE_OFF_PRMIUM" />
				<!-- 11  -->
				<xsl:call-template name="DOWN_PAY_MENT" />				
				<!-- 12  -->
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
	<!--Template BOAT-->
	<xsl:template match="BOAT">
		<xsl:choose>
			<xsl:when test="BOAT_NO='' or BOAT_YEAR=''or MODEL='' or LENGTH=''or TYPE_OF_WATERCRAFT=''or  
			 MAKE=''or  HULL_ID_NO=''or STATE_REG=''or HULL_MATERIAL='' or WATER_NAVIGATED='' or MAX_SPEED=''
			 or TERRITORY='' or COV_TYPE_BASIS='' or INSURING_VALUE='' or LOCATION_ADDRESS='' or LOCATION_CITY='' or LOCATION_STATE='' or LOCATION_ZIP=''">
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
								<tr>
									<xsl:if test="BOATTYPE != ''">
										<td class="pageheader" width="18%">Boat Type:</td>
										<td class="midcolora" width="36%">
											<xsl:value-of select="BOATTYPE" />
										</td>
									</xsl:if>
									<xsl:if test="INT_MAX_LENGTH != ''">
										<td class="pageheader" width="18%">Length:</td>
										<td class="midcolora" width="36%">
											<xsl:value-of select="INT_MAX_LENGTH" />Ft.
											<xsl:value-of select="INT_INCH" />Inches
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
				<tr>
					<td>
						<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
					</td>
				</tr>
			</xsl:otherwise>
		</xsl:choose>
		<!-- </table>	-->
	</xsl:template>
	<!--Template Operator-->
	<xsl:template match="OPERATOR">
		<xsl:choose>
			<xsl:when test="DRIVER_FNAME=''or DRIVER_LNAME=''or DRIVER_CODE=''or  DRIVER_DRIV_LIC='' or DRIVER_DRIV_TYPE=''
			 or DRIVER_SEX='' or DRIVER_SEX='0' or DRIVER_STATE=''or DRIVER_ZIP='' or  DRIVER_DOB=''or DRIVER_LIC_STATE='' or 
			 DRIVER_LIC_STATE='' or VEHICLE_ID='0' or VEHICLE_ID='' or PRIN_OCC_ID='0' or PRIN_OCC_ID='' or DEACTIVATEBOAT='Y' or 
			 DRIVER_ADD1='' or DRIVER_CITY='' or VIOLATIONS='' or DRIVER_COST_GAURAD_AUX='' ">
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
				<xsl:call-template name="DRIVER_DRIV_TYPE" />				
				<xsl:call-template name="DRIVER_LIC_STATE" />
				<xsl:call-template name="VEHICLE_ID" />
				<xsl:call-template name="PRIN_OCC_ID" />
				<xsl:call-template name="DEACTIVATEBOAT" />
				<xsl:call-template name="VIOLATIONS" />
				<xsl:call-template name="DRIVER_COST_GAURAD_AUX" />
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
    	 MINOR_VIOLATION=''or ANY_LOSS_THREE_YEARS='' or ANY_LOSS_THREE_YEARS_DESC='' or 
    	 COVERAGE_DECLINED='' or COVERAGE_DECLINED_DESC='' 
    	 or IS_RENTED_OTHERS='' or IS_RENTED_OTHERS_DESC='' or IS_REGISTERED_OTHERS='' or PARTICIPATE_RACE='' or CARRY_PASSENGER_FOR_CHARGE='' or
    	 IS_BOAT_COOWNED='' or IS_BOAT_COOWNED_DESC=''
    	  or ANY_BOAT_AMPHIBIOUS='' or ANY_BOAT_AMPHIBIOUS_DESC=''
    	  or MULTI_POLICY_DISC_APPLIED='' or MULTI_POLICY_DISC_APPLIED_DESC='' or ANY_BOAT_RESIDENCE='' or ANY_BOAT_RESIDENCE_DESC='' or IS_BOAT_USED_IN_ANY_WATER='' or IS_BOAT_USED_IN_ANY_WATER_DESC=''">
				<tr>
					<td class="pageheader">For Underwriting Questions:</td>
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
				<!--<xsl:call-template name="OTHER_POLICY_NUMBER_LIST" />-->
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
				<xsl:call-template name="IS_BOAT_COOWNED" />
				<xsl:call-template name="IS_BOAT_COOWNED_DESC" />
				<!--May 05-->
				<xsl:call-template name="ANY_BOAT_AMPHIBIOUS" />
				<xsl:call-template name="ANY_BOAT_AMPHIBIOUS_DESC" />
				<xsl:call-template name="MULTI_POLICY_DISC_APPLIED" />
				<xsl:call-template name="MULTI_POLICY_DISC_APPLIED_DESC" />
				<!--End May 05-->
				<!--May 11 06 Boat residence-->
				<xsl:call-template name="ANY_BOAT_RESIDENCE" />
				<xsl:call-template name="ANY_BOAT_RESIDENCE_DESC" />
				<!--May 11 06 Boat residence-->
				<!-- Boat state other than Indiana, Michigan or Wisconsin-->
				<xsl:call-template name="IS_BOAT_USED_IN_ANY_WATER" />
				<xsl:call-template name="IS_BOAT_USED_IN_ANY_WATER_DESC" />
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
	<!--   1   -->
	<!--   Screen Application detail Details    -->
	<xsl:template name="APP_EFFECTIVE_DATE">
		<xsl:choose>
			<xsl:when test="APP_EFFECTIVE_DATE='' or APP_EFFECTIVE_DATE='NULL'">
				<tr>
					<td class="midcolora">Please insert Effective Date.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   2   -->
	<!--   Screen Application detail Details    -->
	<xsl:template name="DRIVER_DOB">
		<xsl:choose>
			<xsl:when test="DRIVER_DOB=''">
				<tr>
					<td class="midcolora">Please insert Date of Birth.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   3   -->
	<!--   Screen Application detail Details    -->
	<xsl:template name="STATE_ID">
		<xsl:choose>
			<xsl:when test="STATE_ID=-1 or STATE_ID='' ">
				<tr>
					<td class="midcolora">Please select State.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   4   -->
	<!--   Screen Application detail Details    -->
	<xsl:template name="APP_LOB">
		<xsl:choose>
			<xsl:when test="APP_LOB=''or APP_LOB=NULL ">
				<tr>
					<td class="midcolora">Please select Line of Business.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   5   -->
	<!--   Screen Application detail Details    -->
	<xsl:template name="APP_TERMS">
		<xsl:choose>
			<xsl:when test="APP_TERMS=''">
				<tr>
					<td class="midcolora">Please select Policy Term Months.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   6   -->
	<!--   Screen Application detail Details    -->
	<xsl:template name="APP_EXPIRATION_DATE">
		<xsl:choose>
			<xsl:when test="APP_EXPIRATION_DATE='' or APP_EXPIRATION_DATE='NULL'">
				<tr>
					<td class="midcolora">Please insert Expiration Date.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   7   -->
	<!--   Screen Application detail Details    -->
	<xsl:template name="APP_AGENCY_ID">
		<xsl:choose>
			<xsl:when test="APP_AGENCY_ID=-1 or APP_AGENCY_ID=''">
				<tr>
					<td class="midcolora">Please select Agency.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   8   -->
	<!--   Screen Application detail Details    -->
	<xsl:template name="BILL_TYPE">
		<xsl:choose>
			<xsl:when test="BILL_TYPE=''">
				<tr>
					<td class="midcolora">Please select Bill Type.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--  9  -->
	<!-- Down Paymant Mode-->
	<xsl:template name="DOWN_PAY_MENT">
		<xsl:choose>
			<xsl:when test="DOWN_PAY_MENT=''">
				<tr>
					<td class="midcolora">Please select Down Payment.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   9   -->
	<!--   Screen Application detail Details    -->
	<xsl:template name="PROXY_SIGN_OBTAINED">
		<xsl:choose>
			<xsl:when test="PROXY_SIGN_OBTAINED='-1' or PROXY_SIGN_OBTAINED=''">
				<tr>
					<td class="midcolora">Please select Proxy Signature Obtained?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   10   -->
	<!--   Screen Application detail Details    -->
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
	<!--   11   -->
	<!--   Screen Application detail Details    -->
	<xsl:template name="CHARGE_OFF_PRMIUM">
		<xsl:choose>
			<xsl:when test="CHARGE_OFF_PRMIUM='' or CHARGE_OFF_PRMIUM='NULL'">
				<tr>
					<td class="midcolora">Please select Charge Off Premium.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   12   -->
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
	<!-- 14 UNDERWRITER-->
	<xsl:template name="POL_UNDERWRITER">
		<xsl:choose>
			<xsl:when test="POL_UNDERWRITER=''">
				<tr>
					<td class="midcolora">Please select Underwriter.</td>
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
	<!---->
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
	<!-- Violations -->
	<xsl:template name="VIOLATIONS">
		<xsl:choose>
			<xsl:when test="VIOLATIONS=''">
				<tr>
					<td class="midcolora">Please select violations.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Boating Experience Since -->
	<xsl:template name="DRIVER_COST_GAURAD_AUX">
		<xsl:choose>
			<xsl:when test="DRIVER_COST_GAURAD_AUX=''">
				<tr>
					<td class="midcolora">Please insert Boating Experience Since.</td>
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
			<xsl:when test="VEHICLE_ID='0' or VEHICLE_ID=''">
				<tr>
					<td class="midcolora">Please assign boat to Operator.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	
	<!--   Screen Operator Information   -->
	<xsl:template name="DRIVER_DRIV_TYPE">
		<xsl:choose>
			<xsl:when test="DRIVER_DRIV_TYPE='0' or DRIVER_DRIV_TYPE=''">
				<tr>
					<td class="midcolora">Please select Driver Type.</td>
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
	<!--<xsl:template name="ANY_OTH_INSU_COMP">
		<xsl:choose>
			<xsl:when test="ANY_OTH_INSU_COMP=''">
				<tr>
					<td class="midcolora">Please select Any other insurance with this company?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>-->
	<!--   41   --> <!--   Screen WC General Information Details   -->
	<!--<xsl:template name="OTHER_POLICY_NUMBER_LIST">
		<xsl:choose>
			<xsl:when test="OTHER_POLICY_NUMBER_LIST=''">
				<tr>
					<td class="midcolora">Please insert Other Policy Details(Provide operator, insurance co, vehicle # and policy #).</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>-->
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
					<td class="midcolora">Please select If applicant had any watercraft coverage declined, canceled, or non-renewed in the last 12 months by any insurer.</td>
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
					<td class="midcolora">Please select Is any boat used to carry passengers for a charge ?.</td>
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
					<td class="midcolora">Please insert Boat Co-owned Description.</td>
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
	<xsl:template name="MULTI_POLICY_DISC_APPLIED">
		<xsl:choose>
			<xsl:when test="MULTI_POLICY_DISC_APPLIED=''">
				<tr>
					<td class="midcolora">Please select Is multipolicy discount applied?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MULTI_POLICY_DISC_APPLIED_DESC">
		<xsl:choose>
			<xsl:when test="MULTI_POLICY_DISC_APPLIED_DESC=''">
				<tr>
					<td class="midcolora">Please insert Policy Number.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--May 05-->
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
	<!--MAY 29 Equipment-->
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
	<!-- Template for showing Info at the initial stage - Added on 08 Dec 2005-->
	<xsl:template name="WCDETAIL">
		<!-- Application List -->
		<xsl:call-template name="APPLICATION_LIST_MESSAGESGES" />
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
				<xsl:call-template name="GENINFOMESSAGE" />
			</xsl:otherwise>
		</xsl:choose>
		<!--EQUIP MAY 29-->
		<!--<xsl:choose>
			<xsl:when test="INPUTXML/WATERCRAFTEQUIPMENTS/WATERCRAFTEQUIPMENT/IS_RECORD_EXISTS='Y'">
				<xsl:apply-templates select="INPUTXML/WATERCRAFTEQUIPMENTS/WATERCRAFTEQUIPMENT" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
				<xsl:call-template name="EQUIPMENTMESSAGE" />
			</xsl:otherwise>
		</xsl:choose>-->
		<!---->
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
		<!--<tr>
			<td class="midcolora">Please insert Model.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Serial #.</td>
		</tr>-->
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
			<td class="midcolora">Please select Driver Type.</td>
		</tr>		
		<tr>
			<td class="midcolora">Please select License State.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Boating Experience Since. </td>
		</tr>
		<tr>
			<td class="midcolora">Please assign boat to Operator.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Violations.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Principal/Occasional operator.</td>
		</tr>
	</xsl:template>
	<!-- Gen Info -->
	<xsl:template name="GENINFOMESSAGE">
		<tr>
			<td class="pageheader">For Underwriting Questions:</td>
		</tr>
		<!--<tr>
			<td class="midcolora">If 'Has applicant lived at current address for less than 3 years?' is 'Yes' then Please insert Years Description.</td>
		</tr>-->
		<tr>
			<td class="midcolora">Please select Any operator physically or mentally impaired.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Any operator physically or mentally impaired? is 'Yes', then Please insert Driver Impairment Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Anyone had a license suspended, restricted or revoked in the last 5 years.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Anyone had a license suspended, restricted or revoked in the last 5 years? is 'Yes', then Please insert Driver License Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any operator convicted of a major violation in the last 5 years.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Any operator convicted of a major violation in the last 5 years? is 'Yes', then Please insert Violation Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Anyone had a drinking or drug related violation in last 5 years?.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any operator convicted of a minor violation in the last 3 years?.</td>
		</tr>
		<!--<tr>
			<td class="midcolora">Please select Any other insurance with this company.</td>
		</tr>
		<tr>
			<td class="midcolora">If Any other insurance with this company? is 'Yes' then Please insert Other Policy Details(Provide operator, insurance co, vehicle # and policy #).</td>
		</tr>-->
		<tr>
			<td class="midcolora">Please select Anyone had any losses with this or other vessels.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Anyone had any losses with this or other vessels? is 'Yes', then Please insert Loss Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select If any applicant's watercraft coverage has been refused, cancelled or non renewed in the last 12 months by any insurer.</td>
		</tr>
		<tr>
			<td class="midcolora">If,Has any applicant's watercraft coverage has been refused, cancelled or non renewed in the last 12 months by any insurer? is 'Yes', then Please insert Coverage Declined Description.</td>
		</tr>
		<!--<tr>
			<td class="midcolora">If 'Credit' is 'Yes' then Please insert Details.</td>
		</tr>-->
		<tr>
			<td class="midcolora">Please select Is any boat rented to others.</td>
		</tr>
		<tr>
			<td class="midcolora">If,Is any boat rented to others? is 'Yes', then Please insert Rented Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is boat co-owned.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Is  boat co-owned? is 'Yes', then Please insert Boat co-owned Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is any boat registered to anyone other than applicant.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Is any boat registered to anyone other than applicant? is 'Yes', then Please insert Registration Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is any boat used to participate in any race,speed, or fishing contest other than a Sailboat under 26ft.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Is any boat used to participate in any race,speed, or fishing contest other than a Sailboat under 26ft? is 'Yes', then Please insert Boat Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is any boat used to carry passengers for a charge. </td>
		</tr>
		<tr>
			<td class="midcolora">If, Is any boat used to carry passengers for a charge? is 'Yes', then Please insert boat used to carry passengers for a charge description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any Prior Carrier.</td>
		</tr>
		<tr>
			<td class="midcolora">If,Any Prior Carrier? is 'Yes', then Please insert Name of Carrier.</td>
		</tr>
		<!--Added new added Questions in UQ-->
		<tr>
			<td class="midcolora">Please select Is multipolicy discount applied.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Is multipolicy discount applied? is 'Yes', then Please insert Policy Number.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Are any of the boats Hydrofoils, Swamp Buggies, Collapsible, Experimental, Converted Military Craft Single Passenger or Amphibious?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Are any of the boats Hydrofoils, Swamp Buggies, Collapsible, Experimental, Converted Military Craft Single Passenger or Amphibious? is 'Yes', then Please insert Boat Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is any boat used as a residence?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Is any boat used as a residence? is 'Yes', then Please select Boat Residence Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is any boat used in any water other than Indiana, Michigan or Wisconsin?.</td>
		</tr>
		<tr>
			<td class="midcolora">If, Is any boat used in any water other than Indiana, Michigan or Wisconsin? is 'Yes', then Please insert Boat used in any water Description.</td>
		</tr>
	</xsl:template>
	<!--EQUIPMENT MAY 29-->
	<!--<xsl:template name="EQUIPMENTMESSAGE">
	<tr>
		<td class="pageheader">For Equipment Information:</td>
	</tr>
	<tr>
		<td class="midcolora">Please select Equipment Type.</td>
	</tr>
	<tr>
		<td class="midcolora">Please select Description.</td>
	</tr>
	</xsl:template>-->
	<!--MAY 29-->
</xsl:stylesheet>