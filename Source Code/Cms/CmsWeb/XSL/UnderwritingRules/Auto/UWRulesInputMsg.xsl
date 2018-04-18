<!-- ==================================================================================================
File Name			:	UWRulesInputMsg.xsl
Purpose				:	To display Messages for Private Passenger Automobiles (PPA)
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
					<!--  when there is no vehicle and driver -->
					<xsl:when test="INPUTXML/VEHICLES/ERRORS/ERROR/@ERRFOUND='T' and INPUTXML/DRIVERS/ERRORS/ERROR/@ERRFOUND='T'">
						<!-- call a template shows all the messages  -->
						<xsl:if test="user:AssignUnderwritter(0) = 0"></xsl:if>
						<xsl:call-template name="PPADETAIL" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:call-template name="SHOWPPAINPUTDETAIL" />
					</xsl:otherwise>
				</xsl:choose>
			</table>
		</html>
	</xsl:template>
	<!-- This template shows the input detail of PPA-->
	<xsl:template name="SHOWPPAINPUTDETAIL">
		<xsl:apply-templates select="INPUTXML/APPLICATIONS/APPLICATION"></xsl:apply-templates>
		<xsl:apply-templates select="INPUTXML/MVRS/MVR"></xsl:apply-templates>
		<!-- Vehicle -->
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/@COUNT > 0">
				<xsl:for-each select="INPUTXML/VEHICLES">
					<xsl:apply-templates select="VEHICLE"></xsl:apply-templates>
				</xsl:for-each>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
				<xsl:call-template name="VEHICLEMESSAGE" />
				<!-- <xsl:call-template name="ADDITIONALINTMESSAGE"/> -->
			</xsl:otherwise>
		</xsl:choose>
		<!-- Driver -->
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/@COUNT > 0">
				<xsl:for-each select="INPUTXML/DRIVERS">
					<xsl:apply-templates select="DRIVER"></xsl:apply-templates>
				</xsl:for-each>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
				<xsl:call-template name="DRIVERMESSAGE" />
				<!-- <xsl:call-template name="MVRMESSAGE"/> -->
			</xsl:otherwise>
		</xsl:choose>
		<!-- Gen Info  -->
		<xsl:choose>
			<xsl:when test="INPUTXML/AUTOGENINFOS/AUTOGENINFO/IS_RECORD_EXISTS='Y'">
				<xsl:call-template name="GENINFOMESSAGE" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:apply-templates select="INPUTXML/AUTOGENINFOS/AUTOGENINFO"></xsl:apply-templates>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template match="INPUTXML/APPLICATIONS/APPLICATION">
		<xsl:choose>
			<xsl:when test="STATE_ID=-1 or STATE_ID='' or APP_TERMS='' or APP_EFFECTIVE_DATE='' or APP_LOB='' or
				APP_AGENCY_ID=-1 or APP_AGENCY_ID='' or BILL_TYPE='' or PROXY_SIGN_OBTAINED=-1 or PROXY_SIGN_OBTAINED=''
				 or CHARGE_OFF_PRMIUM='' or IS_PRIMARY_APPLICANT='' or IS_PRIMARY_APPLICANT='Y' or DOWN_PAY_MENT='' or POL_UNDERWRITER=''">
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
				<!-- 11 -->
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
	<xsl:template match="VEHICLE">
		<xsl:choose>
			<xsl:when test="VEHICLE_YEAR='' or MAKE=''or MODEL='' or VIN=''or VEHICLE_USE='' or  APP_VEHICLE_PERTYPE_ID = '' or
			 GRG_ADD1=''or  GRG_CITY=''or GRG_STATE=''or GRG_ZIP='' or RADIUS_OF_USE='' or RADIUS_OF_USE='0' or TRANSPORT_CHEMICAL='' or COVERED_BY_WC_INSU='' or VEHICLE_AMOUNT=0 or VEHICLE_AMOUNT='' or   
    	ADDITIONALINTERESTS/ADDITIONALINTEREST/HOLDER_ADD1=''  or ADDITIONALINTERESTS/ADDITIONALINTEREST/HOLDER_CITY=''or 
    	ADDITIONALINTERESTS/ADDITIONALINTEREST/HOLDER_STATE='' or ADDITIONALINTERESTS/ADDITIONALINTEREST/HOLDER_ZIP=''or 
    	ADDITIONALINTERESTS/ADDITIONALINTEREST/NATURE_OF_INTEREST='' or USE_VEHICLE='0' or USE_VEHICLE='' or VEHICLE_TYPE_PER='' or VEHICLE_TYPE_PER='0' or 
    	 VEHICLE_TYPE_COM ='' or HOLDER_NAME='' or ADDITIONALINTERESTS/ERRORS/ERROR/@ERRFOUND='T' or UMPD_UNINS_PROPERTY_DAMAGE_COVE_EXIST=''
    	 or UNDSP_UNDERINS_MOTOR_INJURY_COVE_EXIST='' or PUMSP_SIGNATURE_OBTAINED_EXIST='' or SYMBOL='' or 
    	 CLASS_PER='' or CLASS_COM='' or HELTH_CARE='' or AUTO_POL_NO=''">
				<tr>
					<td class="pageheader">For Vehicle Information: </td>
					<tr>
						<td>
							<table width="60%">
								<tr>
									<xsl:if test="VIN != '' and VIN !='N'">
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
				<!--   1   -->
				<xsl:call-template name="VEHICLE_USE" />
				<!--   2   -->
				<xsl:call-template name="VIN" />
				<!--   3   -->
				<!-- <xsl:call-template name="INSURED_VEH_NUMBER" /> -->
				<!--   4   -->
				<xsl:call-template name="VEHICLE_YEAR" />
				<!--   5   -->
				<xsl:call-template name="MAKE" />
				<!--  6   -->
				<xsl:call-template name="MODEL" />
				<!--   7   -->
				<xsl:call-template name="GRG_ADD1" />
				<!--   8   -->
				<xsl:call-template name="GRG_CITY" />
				<!--   9   -->
				<xsl:call-template name="GRG_STATE" />
				<!--   10   -->
				<xsl:call-template name="GRG_ZIP" />
				<!--   11   -->
				<!-- <xsl:call-template name="TERRITORY" /> -->
				<!--   12   -->
				<xsl:call-template name="VEHICLE_AMOUNT" />
				<!--   13   -->
				<xsl:call-template name="USE_VEHICLE" />
				<!-- 14 -->
				<xsl:call-template name="UMPD_UNINS_PROPERTY_DAMAGE_COVE_EXIST" />
				<!-- 15 -->
				<xsl:call-template name="UNDSP_UNDERINS_MOTOR_INJURY_COVE_EXIST" />
				<!-- 16 -->
				<xsl:call-template name="PUMSP_SIGNATURE_OBTAINED_EXIST" />
				<!--17-->
				<xsl:call-template name="SYMBOL" />
				<xsl:call-template name="CLASS_COM" />
				<xsl:call-template name="CLASS_PER" />
				<!--19-->
				<xsl:call-template name="APP_VEHICLE_PERTYPE_ID" />
				<xsl:call-template name="RADIUS_OF_USE" />
				<xsl:call-template name="TRANSPORT_CHEMICAL" />
				<xsl:call-template name="COVERED_BY_WC_INSU" />
				<xsl:call-template name="AUTO_POL_NO" />
				<xsl:call-template name="HELTH_CARE" />
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
	<!-- <xsl:template match="ADDITIONALINTEREST">
		<xsl:choose>
			<xsl:when test="HOLDER_ADD1='' or HOLDER_CITY=''or HOLDER_STATE='' or
    	  HOLDER_ZIP=''or NATURE_OF_INTEREST='' or HOLDER_NAME='' ">
				<tr>
					<td class="pageheader">for Additional Interest:</td>
					<tr>
						<td >
							<table>
								<tr>
									<xsl:if test="HOLDER_NAME != ''">
										<td class="pageheader" width="18%">Selected Holder/Interest:</td>
										<td class="midcolora" width="36%">
											<xsl:value-of select="HOLDER_NAME" />
										</td>
									</xsl:if>
								</tr>
							</table>
						</td>
					</tr>
				</tr>
				<xsl:call-template name="HOLDER_ADD1" />
				<xsl:call-template name="HOLDER_CITY" />
				<xsl:call-template name="HOLDER_STATE" />
				<xsl:call-template name="HOLDER_ZIP" />
				<xsl:call-template name="NATURE_OF_INTEREST" />
				<xsl:call-template name="HOLDER_NAME" />
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
	</xsl:template> -->
	<xsl:template match="DRIVER">
		<xsl:choose>
			<xsl:when test="DRIVER_DRINK_VIOLATION=''or US_CITIZEN=''or DISTANT_STUDENT=''or  DRIVER_DRIV_LIC=''
			 or DRIVER_CODE=''or DRIVER_SEX=''or DRIVER_FNAME='' or DRIVER_LNAME='' or DRIVER_STATE=''or DRIVER_ZIP='' or 
			 DRIVER_LIC_STATE='' or DRIVER_DRIV_TYPE='' or	VIOLATIONS='' or APP_VEHICLE_PRIN_OCC_ID='' or DRIVER_VOLUNTEER_POLICE_FIRE='' or MVRINFORMATIONS/MVRINFORMATION/MVR_DATE=''
			  or MVRINFORMATIONS/MVRINFORMATION/VIOLATION_ID='' or DATE_LICENSED=NULL or DATE_LICENSED=''or DRIVER_DOB='' or IN_MILITARY='' or HAVE_CAR=''
			  or STATIONED_IN_US_TERR='' or VEHICLE_ID='0' or VEHICLE_ID='' or MVRINFORMATIONS/ERRORS/ERROR/@ERRFOUND='T' or DEACTIVATEVEHICLE='Y' or PARENTS_INSURANCE=''">
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
									<xsl:if test="DRIVER_DRIV_LIC != ''">
										<td class="pageheader" width="18%">License Number:</td>
										<td class="midcolora" width="36%">
									    <xsl:if test="DRIVER_DRIV_LIC != 'N'">
											<xsl:value-of select="DRIVER_DRIV_LIC" />
										</xsl:if>
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
				<!--   1   -->
				<xsl:call-template name="DRIVER_FNAME" />
				<!--   2   -->
				<xsl:call-template name="DRIVER_LNAME" />
				<!--   3   -->
				<xsl:call-template name="DRIVER_CODE" />
				<!--   4   -->
				<!-- <xsl:call-template name="DRIVER_NAME" /> -->
				<!--   5   -->
				<xsl:call-template name="DRIVER_STATE" />
				<!--   6   -->
				<xsl:call-template name="DRIVER_ZIP" />
				<xsl:call-template name="DRIVER_DOB" />
				<!--   7   -->
				<xsl:call-template name="DRIVER_SEX" />
				<!--   8   -->
				<xsl:call-template name="DRIVER_DRIV_LIC" />
				<!--   9   -->
				<xsl:call-template name="DRIVER_LIC_STATE" />
				<!--   10   -->
				<xsl:call-template name="DATE_LICENSED" />
				<xsl:call-template name="DRIVER_DRIV_TYPE" />
				<xsl:call-template name="VIOLATIONS" />
				<!--   11   -->
				<xsl:call-template name="DRIVER_VOLUNTEER_POLICE_FIRE" />
				<!--   13   -->
				<xsl:call-template name="DRIVER_DRINK_VIOLATION" />
				<!--   14   -->
				<xsl:call-template name="DISTANT_STUDENT" />
				<!--   15   -->
				<xsl:call-template name="US_CITIZEN" />
				<!--   16   -->
				<xsl:call-template name="DEACTIVATEVEHICLE" />
				<!--   17   -->
				<xsl:call-template name="VEHICLE_ID" />
				<!--   18   -->
				<xsl:call-template name="APP_VEHICLE_PRIN_OCC_ID" />
				<!--   19 ADDED BY PRAVEEN KUMAR(22-01-09)  -->
				<xsl:call-template name="IN_MILITARY" />
				<!--   20 ADDED BY PRAVEEN KUMAR(22-01-09)  -->
				<xsl:call-template name="STATIONED_IN_US_TERR" />
				<!--   21 ADDED BY PRAVEEN KUMAR(22-01-09)  -->
				<xsl:call-template name="HAVE_CAR" />
				<xsl:call-template name="PARENTS_INSURANCE" /> <!-- Added by Charles on 18-Nov-09 for Itrack 6725 -->
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
				<xsl:call-template name="MVR_VIOLATION_ID" />
				<xsl:call-template name="CONVICTION_DATE" />
				<xsl:call-template name="OCCURENCE_DATE" />
			</xsl:when>
			<xsl:otherwise>
						<xsl:if test="user:AssignUnderwritter(1)=0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--=============== End MVR Information =================-->
	
	
	<!-- 
	<xsl:template match="MVRINFORMATION">
		<xsl:choose>
			<xsl:when test="MVR_DATE='' or VIOLATION_ID=''">
				<tr>
					<td class="pageheader">For MVR information:</td>
					<tr>
						<td >
							<table>
								<tr>
									<xsl:if test="VIOLATION_DES != ''">
										<td class="pageheader">Violation:</td>
										<td class="midcolora">
											<xsl:value-of select="VIOLATION_DES" />
										</td>
									</xsl:if>
								</tr>
							</table>
						</td>
					</tr>
				</tr>
				<xsl:call-template name="VIOLATION_ID" />
				<xsl:call-template name="MVR_DATE" />
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
	-->
	<xsl:template match="INPUTXML/AUTOGENINFOS/AUTOGENINFO">
		<xsl:choose>
			<xsl:when test="COVERAGE_DECLINED=''or COVERAGE_DECLINED_PP_DESC=''or H_MEM_IN_MILITARY=''or H_MEM_IN_MILITARY_DESC='' or ANY_FINANCIAL_RESPONSIBILITY=''or ANY_FINANCIAL_RESPONSIBILITY_PP_DESC=''
    	or CAR_MODIFIED=''or CAR_MODIFIED_DESC='' or SALVAGE_TITLE='' or 
    	 SALVAGE_TITLE_PP_DESC=''or ANY_NON_OWNED_VEH='' or ANY_NON_OWNED_VEH_PP_DESC='' or EXISTING_DMG='' or EXISTING_DMG_PP_DESC='' or 
    	 ANY_CAR_AT_SCH='' or ANY_CAR_AT_SCH_DESC=''  or DRIVER_SUS_REVOKED='' or DRIVER_SUS_REVOKED_PP_DESC='' or PHY_MENTL_CHALLENGED='' or PHY_MENTL_CHALLENGED_PP_DESC='' or ANY_OTH_AUTO_INSU='' or
    	 ANY_OTH_AUTO_INSU_DESC='' or AGENCY_VEH_INSPECTED='' or AGENCY_VEH_INSPECTED_PP_DESC='' or 
    	 ANY_ANTIQUE_AUTO='' or ANY_ANTIQUE_AUTO_DESC='' or MULTI_POLICY_DISC_APPLIED='' or MULTI_POLICY_DISC_APPLIED_PP_DESC='' or IS_OTHER_THAN_INSURED='' or
    	 FULLNAME='' or DATE_OF_BIRTH='' or CUSTOMER_ID=-1 or CUSTOMER_ID=NULL  or POLICYNUMBER='' 
    	 or POLICYNUMBER=NULL or COMPANYNAME=NULL or COMPANYNAME='' or ANY_PRIOR_LOSSES='' or ANY_PRIOR_LOSSES_DESC=''
    	 or USE_AS_TRANSPORT_FEE='' or USE_AS_TRANSPORT_FEE_DESC='' or COST_EQUIPMENT_DESC='' ">
				<tr>
					<td class="pageheader">For Underwriting Questions:</td>
				</tr>
				<xsl:call-template name="ANY_NON_OWNED_VEH" />
				<xsl:call-template name="ANY_NON_OWNED_VEH_PP_DESC" />
				<xsl:call-template name="CAR_MODIFIED" />
				<xsl:call-template name="CAR_MODIFIED_DESC" />
				<xsl:call-template name="COST_EQUIPMENT_DESC" />
				<xsl:call-template name="EXISTING_DMG" />
				<xsl:call-template name="EXISTING_DMG_PP_DESC" />
				<xsl:call-template name="ANY_CAR_AT_SCH" />
				<xsl:call-template name="ANY_CAR_AT_SCH_DESC" />
				<xsl:call-template name="ANY_OTH_AUTO_INSU" />
				<xsl:call-template name="ANY_OTH_AUTO_INSU_DESC" />
				<xsl:call-template name="H_MEM_IN_MILITARY" />
				<xsl:call-template name="H_MEM_IN_MILITARY_DESC" />
				<xsl:call-template name="DRIVER_SUS_REVOKED" />
				<xsl:call-template name="DRIVER_SUS_REVOKED_PP_DESC" />
				<xsl:call-template name="PHY_MENTL_CHALLENGED" />
				<xsl:call-template name="PHY_MENTL_CHALLENGED_PP_DESC" />
				<xsl:call-template name="ANY_FINANCIAL_RESPONSIBILITY" />
				<xsl:call-template name="ANY_FINANCIAL_RESPONSIBILITY_PP_DESC" />
				<xsl:call-template name="INS_AGENCY_TRANSFER" />
				<xsl:call-template name="INS_AGENCY_TRANSFER_PP_DESC" />
				<xsl:call-template name="COVERAGE_DECLINED" />
				<xsl:call-template name="COVERAGE_DECLINED_PP_DESC" />
				<xsl:call-template name="AGENCY_VEH_INSPECTED" />
				<xsl:call-template name="AGENCY_VEH_INSPECTED_PP_DESC" />
				<xsl:call-template name="SALVAGE_TITLE" />
				<xsl:call-template name="SALVAGE_TITLE_PP_DESC" />
				<xsl:call-template name="ANY_ANTIQUE_AUTO" />
				<xsl:call-template name="ANY_ANTIQUE_AUTO_DESC" />
				<xsl:call-template name="MULTI_POLICY_DISC_APPLIED" />
				<xsl:call-template name="MULTI_POLICY_DISC_APPLIED_PP_DESC" />
				<xsl:call-template name="IS_OTHER_THAN_INSURED" />
				<xsl:call-template name="FULLNAME" />
				<xsl:call-template name="DATE_OF_BIRTH" />
				<xsl:call-template name="COMPANYNAME" />
				<xsl:call-template name="POLICYNUMBER" />
				<xsl:call-template name="ANY_PRIOR_LOSSES" />
				<xsl:call-template name="ANY_PRIOR_LOSSES_DESC" />
				<xsl:call-template name="USE_AS_TRANSPORT_FEE" />
				<xsl:call-template name="USE_AS_TRANSPORT_FEE_DESC" />
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
	<!-- ================================= TEMPLATES =============================================-->
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
	<!-- ================================== For Proxy Sign ========================================= -->
	<xsl:template name="UMPD_UNINS_PROPERTY_DAMAGE_COVE_EXIST">
		<xsl:choose>
			<xsl:when test="UMPD_UNINS_PROPERTY_DAMAGE_COVE_EXIST=''">
				<tr>
					<td class="midcolora">You must have proxy signature to reject uninsured motorist property damage coverage.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="UNDSP_UNDERINS_MOTOR_INJURY_COVE_EXIST">
		<xsl:choose>
			<xsl:when test="UNDSP_UNDERINS_MOTOR_INJURY_COVE_EXIST=''">
				<tr>
					<td class="midcolora">You must have proxy signature to reject underinsured motorist bodily injury coverage.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PUMSP_SIGNATURE_OBTAINED_EXIST">
		<xsl:choose>
			<xsl:when test="PUMSP_SIGNATURE_OBTAINED_EXIST=''">
				<tr>
					<td class="midcolora">You must have proxy signature to reject uninsured motorist bodily injury coverage.</td>
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
	<!--   1   -->
	<xsl:template name="DRIVER_DRINK_VIOLATION">
		<xsl:choose>
			<xsl:when test="DRIVER_DRINK_VIOLATION=''">
				<tr>
					<td class="midcolora">Please select Drinking or Drug Related violation in last 5 years?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   2   -->
	<xsl:template name="US_CITIZEN">
		<xsl:param name="US_CITIZEN" />
		<xsl:choose>
			<xsl:when test="US_CITIZEN=''">
				<tr>
					<td class="midcolora">Please select U.S. Citizen.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   3   -->
	<xsl:template name="DISTANT_STUDENT">
		<xsl:choose>
			<xsl:when test="DISTANT_STUDENT=''">
				<tr>
					<td class="midcolora">Please select College Student W/NO Car.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   4   -->
	<xsl:template name="LIC_SUSPENDED">
		<xsl:choose>
			<xsl:when test="LIC_SUSPENDED=''">
				<tr>
					<td class="midcolora"> Please select License suspended, restricted, or revoked in the last 5 years?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   5   -->
	<xsl:template name="SAFE_DRIVER">
		<xsl:choose>
			<xsl:when test="SAFE_DRIVER=''">
				<tr>
					<td class="midcolora"> Please select Safe Driver-Rollover.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--  6   -->
	<xsl:template name="COVERAGE_DECLINED">
		<xsl:choose>
			<xsl:when test="COVERAGE_DECLINED=''">
				<tr>
					<td class="midcolora">Please select Has any coverage been declined, cancelled or non-renewed during the last 3 years?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   7   -->
	<xsl:template name="MEM_IN_MILITRY">
		<xsl:choose>
			<xsl:when test="MEM_IN_MILITRY=''">
				<tr>
					<td class="midcolora">Please select Any household member in military service?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   8   -->
	<xsl:template name="PHY_MENTAL_CHALLENGED">
		<xsl:choose>
			<xsl:when test="PHY_MENTAL_CHALLENGED=''">
				<tr>
					<td class="midcolora">Please select Any driver physically or mentally impaired?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   9   -->
	<xsl:template name="CAR_MODIFIED">
		<xsl:choose>
			<xsl:when test="CAR_MODIFIED=''">
				<tr>
					<td class="midcolora">Please select Any car modified special equipment?(Include customized vans and pickups indicate cost.).</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   10   -->
	<xsl:template name="SALVAGE_TITLE">
		<xsl:choose>
			<xsl:when test="SALVAGE_TITLE=''">
				<tr>
					<td class="midcolora">Please select Salvage title.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   11   -->
	<xsl:template name="ANY_NON_OWNED_VEH">
		<xsl:choose>
			<xsl:when test="ANY_NON_OWNED_VEH=''">
				<tr>
					<td class="midcolora">Please select Any vehicles not solely owned.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   12   -->
	<xsl:template name="EXISTING_DMG">
		<xsl:choose>
			<xsl:when test="EXISTING_DMG=''">
				<tr>
					<td class="midcolora">Please select Any existing damage to a vehicle?(Include damaged glass).</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   13   -->
	<xsl:template name="ANY_CAR_AT_SCH">
		<xsl:choose>
			<xsl:when test="ANY_CAR_AT_SCH=''">
				<tr>
					<td class="midcolora">Please select Any car kept at school.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   14   -->
	<xsl:template name="ANY_OTH_INSU_COMP">
		<xsl:choose>
			<xsl:when test="ANY_OTH_INSU_COMP=''">
				<tr>
					<td class="midcolora">Please select Any other insurance with this company?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   15   -->
	<xsl:template name="ANY_FINANCIAL_RESPONSIBILITY">
		<xsl:choose>
			<xsl:when test="ANY_FINANCIAL_RESPONSIBILITY=''">
				<tr>
					<td class="midcolora">Please select Any financial responsibility filing?(Give driver number and date of filing).</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   16   --> <!--   Screen Vehical Info   -->
	<!-- <xsl:template name="USE_VEHICLE">
		<xsl:choose>
			<xsl:when test="USE_VEHICLE=''">
				<tr>
					<td class="midcolora">Please select vehical use.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template> -->
	<!--   17   --> <!--   Screen Vehical Info   -->
	<xsl:template name="VIN">
		<xsl:choose>
			<xsl:when test="VIN=''">
				<tr>
					<td class="midcolora">Please select/insert VIN Number.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   18   --> <!--   Screen Vehical Info   -->
	<xsl:template name="VEHICLE_YEAR">
		<xsl:choose>
			<xsl:when test="VEHICLE_YEAR=''">
				<tr>
					<td class="midcolora">Please insert Year.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   19   --> <!--   Screen Vehical Info   -->
	<xsl:template name="MAKE">
		<xsl:choose>
			<xsl:when test="MAKE=''">
				<tr>
					<td class="midcolora">Please insert Make of Vehicle.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   20   --> <!--   Screen Vehical Info   -->
	<xsl:template name="MODEL">
		<xsl:choose>
			<xsl:when test="MODEL=''">
				<tr>
					<td class="midcolora">Please insert Model of Vehicle.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   21   --> <!--   Screen Vehical Info   -->
	<xsl:template name="GRG_ADD1">
		<xsl:choose>
			<xsl:when test="GRG_ADD1=''">
				<tr>
					<td class="midcolora">Please insert Address 1.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   22   --> <!--   Screen Vehical Info   -->
	<xsl:template name="GRG_CITY">
		<xsl:choose>
			<xsl:when test="GRG_CITY=''">
				<tr>
					<td class="midcolora">Please insert City.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   23   --> <!--   Screen Vehical Info   -->
	<xsl:template name="GRG_STATE">
		<xsl:choose>
			<xsl:when test="GRG_STATE=''">
				<tr>
					<td class="midcolora">Please select State.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   24   --> <!--   Screen Vehical Info   -->
	<xsl:template name="GRG_ZIP">
		<xsl:choose>
			<xsl:when test="GRG_ZIP=''">
				<tr>
					<td class="midcolora">Please insert Zip.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   25   --> <!--   Screen Vehical Info   -->
	<xsl:template name="VEHICLE_AMOUNT">
		<xsl:choose>
			<xsl:when test="VEHICLE_AMOUNT='0' or VEHICLE_AMOUNT=''">
				<tr>
					<td class="midcolora">Please insert Amount.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   26   --> <!--   Screen Vehical Info   -->
	<xsl:template name="VEHICLE_USE">
		<xsl:choose>
			<xsl:when test="VEHICLE_USE=''">
				<tr>
					<td class="midcolora">Please select Vehicle Use.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   27   --> <!--   Screen AddDriver Details   -->
	<xsl:template name="DRIVER_FNAME">
		<xsl:choose>
			<xsl:when test="DRIVER_FNAME=''">
				<tr>
					<td class="midcolora">Please insert First Name.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   28   --> <!--   Screen AddDriver Details   -->
	<xsl:template name="DRIVER_LNAME">
		<xsl:choose>
			<xsl:when test="DRIVER_LNAME=''">
				<tr>
					<td class="midcolora">Please insert Last Name.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   29   --> <!--   Screen AddDriver Details   -->
	<xsl:template name="DRIVER_CODE">
		<xsl:choose>
			<xsl:when test="DRIVER_CODE=''">
				<tr>
					<td class="midcolora">Please insert Code.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   30   --> <!--   Screen AddDriver Details   -->
	<xsl:template name="DRIVER_STATE">
		<xsl:choose>
			<xsl:when test="DRIVER_STATE=''">
				<tr>
					<td class="midcolora">Please insert State.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   31   --> <!--   Screen AddDriver Details   -->
	<xsl:template name="DRIVER_ZIP">
		<xsl:choose>
			<xsl:when test="DRIVER_ZIP=''">
				<tr>
					<td class="midcolora">Please insert Zip.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   32   --> <!--   Screen AddDriver Details   -->
	<xsl:template name="DRIVER_SEX">
		<xsl:choose>
			<xsl:when test="DRIVER_SEX=''">
				<tr>
					<td class="midcolora">Please select Gender.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   33   --> <!--   Screen AddDriver Details   -->
	<xsl:template name="DRIVER_DRIV_LIC">
		<xsl:choose>
			<xsl:when test="DRIVER_DRIV_LIC=''">
				<tr>
					<td class="midcolora">Please insert License Number.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   34   --> <!--   Screen AddDriver Details   -->
	<xsl:template name="DRIVER_LIC_STATE">
		<xsl:choose>
			<xsl:when test="DRIVER_LIC_STATE=''">
				<tr>
					<td class="midcolora">Please insert License State.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   35   --> <!--   Screen AddDriver Details   -->
	<xsl:template name="DRIVER_DRIV_TYPE">
		<xsl:choose>
			<xsl:when test="DRIVER_DRIV_TYPE=''">
				<tr>
					<td class="midcolora">Please insert Driver Type.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   35     --> <!--  Violations  -->
	<xsl:template name="VIOLATIONS">
		<xsl:choose>
			<xsl:when test="VIOLATIONS=''">
				<tr>
					<td class="midcolora">Please select Violations.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   36   --> <!--   Screen AddDriver Details   -->
	<xsl:template name="DRIVER_VOLUNTEER_POLICE_FIRE">
		<xsl:choose>
			<xsl:when test="DRIVER_VOLUNTEER_POLICE_FIRE=''">
				<tr>
					<td class="midcolora">Please select Volunteer fireman or policeman.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   37   --> <!--   Screen AddDriver Details   -->
	<xsl:template name="DRIVER_STUD_DIST_OVER_HUNDRED">
		<xsl:choose>
			<xsl:when test="DRIVER_STUD_DIST_OVER_HUNDRED=''">
				<tr>
					<td class="midcolora">Please select Student W/NO car.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   38   --> <!--   Screen AddDriver Details   -->
	<xsl:template name="DRIVER_US_CITIZEN">
		<xsl:choose>
			<xsl:when test="DRIVER_US_CITIZEN=''">
				<tr>
					<td class="midcolora">Please select US citizen.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   39   --> <!--   Screen AddDriver Details   -->
	<xsl:template name="VEHICLE_ID">
		<xsl:choose>
			<xsl:when test="VEHICLE_ID='0' or VEHICLE_ID=''">
				<tr>
					<td class="midcolora">Please assign vehicle to driver.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   40   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="CAR_MODIFIED_DESC">
		<xsl:choose>
			<xsl:when test="CAR_MODIFIED_DESC=''">
				<tr>
					<td class="midcolora">Please insert Car Modified Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   40(a)  --> <!--   Screen PP General Information Details   -->
	<xsl:template name="COST_EQUIPMENT_DESC">
		<xsl:choose>
			<xsl:when test="COST_EQUIPMENT_DESC=''">
				<tr>
					<td class="midcolora">Please insert Cost of the customized equipment.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   41   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="ANY_OTH_AUTO_INSU">
		<xsl:choose>
			<xsl:when test="ANY_OTH_AUTO_INSU=''">
				<tr>
					<td class="midcolora">Please select Any other auto insurance in household.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   42   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="H_MEM_IN_MILITARY">
		<xsl:choose>
			<xsl:when test="H_MEM_IN_MILITARY=''">
				<tr>
					<td class="midcolora">Please select Any household member using vehicle out of State over 6 months a year.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   43   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="DRIVER_SUS_REVOKED">
		<xsl:choose>
			<xsl:when test="DRIVER_SUS_REVOKED=''">
				<tr>
					<td class="midcolora">Please select Any drivers license been Suspended, restricted or revoked.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   44   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="PHY_MENTL_CHALLENGED">
		<xsl:choose>
			<xsl:when test="PHY_MENTL_CHALLENGED=''">
				<tr>
					<td class="midcolora">Please select Any driver physically or mentally impaired.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   45   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="INS_AGENCY_TRANSFER">
		<xsl:choose>
			<xsl:when test="INS_AGENCY_TRANSFER=''">
				<tr>
					<td class="midcolora">Please select insurance transferred within the agency.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   46   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="AGENCY_VEH_INSPECTED">
		<xsl:choose>
			<xsl:when test="AGENCY_VEH_INSPECTED=''">
				<tr>
					<td class="midcolora">Please select agent inspected the vehicle.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   47   -->
	<xsl:template name="USE_AS_TRANSPORT_FEE">
		<xsl:choose>
			<xsl:when test="USE_AS_TRANSPORT_FEE=''">
				<tr>
					<td class="midcolora">Please select Is any vehicle used for Livery, rental, passenger hire, or to transport persons to work for a fee.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   48   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="ANY_ANTIQUE_AUTO">
		<xsl:choose>
			<xsl:when test="ANY_ANTIQUE_AUTO=''">
				<tr>
					<td class="midcolora">Please select Any vehicle considered an antique auto.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   49  --> <!--   Screen PP General Information Details   -->
	<xsl:template name="MULTI_POLICY_DISC_APPLIED">
		<xsl:choose>
			<xsl:when test="MULTI_POLICY_DISC_APPLIED=''">
				<tr>
					<td class="midcolora">Please select multipolicy discount applied.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   50   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="FullName">
		<xsl:choose>
			<xsl:when test="FullName=''">
				<tr>
					<td class="midcolora">Please select First name.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   51   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="DATE_OF_BIRTH">
		<xsl:choose>
			<xsl:when test="DATE_OF_BIRTH=''">
				<tr>
					<td class="midcolora">Please insert Date of Birth.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   52   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="ANY_NON_OWNED_VEH_PP_DESC">
		<xsl:choose>
			<xsl:when test="ANY_NON_OWNED_VEH_PP_DESC=''">
				<tr>
					<td class="midcolora">Please insert Vehicle Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   53   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="EXISTING_DMG_PP_DESC">
		<xsl:choose>
			<xsl:when test="EXISTING_DMG_PP_DESC=''">
				<tr>
					<td class="midcolora">Please insert Damaged Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   54   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="ANY_CAR_AT_SCH_DESC">
		<xsl:choose>
			<xsl:when test="ANY_CAR_AT_SCH_DESC=''">
				<tr>
					<td class="midcolora">Please insert Car Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   55   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="ANY_OTH_AUTO_INSU_DESC">
		<xsl:choose>
			<xsl:when test="ANY_OTH_AUTO_INSU_DESC=''">
				<tr>
					<td class="midcolora">Please select Auto Insurance Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   56   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="ANY_OTH_INSU_COMP_PP_DESC">
		<xsl:choose>
			<xsl:when test="ANY_OTH_INSU_COMP_PP_DESC=''">
				<tr>
					<td class="midcolora">Please insert Other Insurance Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   57   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="H_MEM_IN_MILITARY_DESC">
		<xsl:choose>
			<xsl:when test="H_MEM_IN_MILITARY_DESC=''">
				<tr>
					<td class="midcolora">Please insert Member Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   58   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="DRIVER_SUS_REVOKED_PP_DESC">
		<xsl:choose>
			<xsl:when test="DRIVER_SUS_REVOKED_PP_DESC=''">
				<tr>
					<td class="midcolora">Please insert Driver License Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   59   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="PHY_MENTL_CHALLENGED_PP_DESC">
		<xsl:choose>
			<xsl:when test="PHY_MENTL_CHALLENGED_PP_DESC=''">
				<tr>
					<td class="midcolora">Please insert Driver Impairment Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   60   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="ANY_FINANCIAL_RESPONSIBILITY_PP_DESC">
		<xsl:choose>
			<xsl:when test="ANY_FINANCIAL_RESPONSIBILITY_PP_DESC=''">
				<tr>
					<td class="midcolora">Please insert Driver Number And Date Of Filing.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   61   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="INS_AGENCY_TRANSFER_PP_DESC">
		<xsl:choose>
			<xsl:when test="INS_AGENCY_TRANSFER_PP_DESC=''">
				<tr>
					<td class="midcolora">Please insert Insurance Transfer Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   62   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="COVERAGE_DECLINED_PP_DESC">
		<xsl:choose>
			<xsl:when test="COVERAGE_DECLINED_PP_DESC=''">
				<tr>
					<td class="midcolora">Please insert Coverage Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   63   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="AGENCY_VEH_INSPECTED_PP_DESC">
		<xsl:choose>
			<xsl:when test="AGENCY_VEH_INSPECTED_PP_DESC=''">
				<tr>
					<td class="midcolora">Please insert Agent Inspection Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   64   -->
	<xsl:template name="USE_AS_TRANSPORT_FEE_DESC">
		<xsl:choose>
			<xsl:when test="USE_AS_TRANSPORT_FEE_DESC=''">
				<tr>
					<td class="midcolora">Please insert Transport Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   65   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="SALVAGE_TITLE_PP_DESC">
		<xsl:choose>
			<xsl:when test="SALVAGE_TITLE_PP_DESC=''">
				<tr>
					<td class="midcolora">Please insert Salvage Title Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   66   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="ANY_ANTIQUE_AUTO_DESC">
		<xsl:choose>
			<xsl:when test="ANY_ANTIQUE_AUTO_DESC=''">
				<tr>
					<td class="midcolora">Please insert Vehicle Consideration Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   67   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="MULTI_POLICY_DISC_APPLIED_PP_DESC">
		<xsl:choose>
			<xsl:when test="MULTI_POLICY_DISC_APPLIED_PP_DESC=''">
				<tr>
					<td class="midcolora">Please insert Multipolicy Discount Description.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   68   --> <!--   Screen PP General Information Details   -->
	<xsl:template name="IS_OTHER_THAN_INSURED">
		<xsl:choose>
			<xsl:when test="IS_OTHER_THAN_INSURED=''">
				<tr>
					<td class="midcolora">Please select Anyone other than insured listed, living in household.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   69   --> <!--   Screen Driver Details   -->
	<xsl:template name="DRIVER_NAME">
		<xsl:choose>
			<xsl:when test="DRIVER_NAME=''">
				<tr>
					<td class="midcolora">Please insert Driver name.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   70   --> <!--   Screen Vehical Info   -->
	<xsl:template name="INSURED_VEH_NUMBER">
		<xsl:choose>
			<xsl:when test="INSURED_VEH_NUMBER='0'or INSURED_VEH_NUMBER=''">
				<tr>
					<td class="midcolora">Please insert Insured Vehical Number.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   71   --> <!--   Screen Vehical Info   -->
	<xsl:template name="TERRITORY">
		<xsl:choose>
			<xsl:when test="TERRITORY=''">
				<tr>
					<td class="midcolora">Please insert Territory.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   72   --> <!--   Screen  Info   -->
	<xsl:template name="FULLNAME">
		<xsl:choose>
			<xsl:when test="FULLNAME=''">
				<tr>
					<td class="midcolora">Please insert Full name.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   73   --> <!--   Screen MVR Driver Details    -->
	<xsl:template name="MVR_DATE">
		<xsl:choose>
			<xsl:when test="MVR_DATE=''">
				<tr>
					<td class="midcolora">Please insert date.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   74   --> <!--   Screen Additional Interest    -->
	<xsl:template name="HOLDER_ADD1">
		<xsl:choose>
			<xsl:when test="HOLDER_ADD1=''">
				<tr>
					<td class="midcolora">Please insert Holder address.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   75   --> <!--   Screen Additional Interest (vehicle Info)   -->
	<xsl:template name="HOLDER_CITY">
		<xsl:choose>
			<xsl:when test="HOLDER_CITY=''">
				<tr>
					<td class="midcolora">Please insert Holder city.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   76   --> <!--   Screen Additional Interest (vehicle Info)   -->
	<xsl:template name="HOLDER_STATE">
		<xsl:choose>
			<xsl:when test="HOLDER_STATE=''">
				<tr>
					<td class="midcolora">Please select Holder State.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   77   --> <!--   Screen Additional Interest (vehicle Info)   -->
	<xsl:template name="HOLDER_ZIP">
		<xsl:choose>
			<xsl:when test="HOLDER_ZIP=''">
				<tr>
					<td class="midcolora">Please insert Holder Zip.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   78   --> <!--   Screen Additional Interest (vehicle Info)   -->
	<xsl:template name="NATURE_OF_INTEREST">
		<xsl:choose>
			<xsl:when test="NATURE_OF_INTEREST=''">
				<tr>
					<td class="midcolora">Please select Nature of Interest.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   79   --> <!--   Screen MVR Driver Details    -->
	<xsl:template name="VIOLATION_ID">
		<xsl:choose>
			<xsl:when test="VIOLATION_ID=''">
				<tr>
					<td class="midcolora">Please select Violation.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   80   --> <!--   Screen MVR Driver Details    -->
	<xsl:template name="DATE_LICENSED">
		<xsl:choose>
			<xsl:when test="DATE_LICENSED='' or DATE_LICENSED='null'">
				<tr>
					<td class="midcolora">Please insert Date licensed.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   81   --> <!--   Screen Application detail Details    -->
	<xsl:template name="APP_EFFECTIVE_DATE">
		<xsl:choose>
			<xsl:when test="APP_EFFECTIVE_DATE='' or APP_EFFECTIVE_DATE='NULL'">
				<tr>
					<td class="midcolora">Please insert Effective Date.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   82   --> <!--   Screen Application detail Details    -->
	<xsl:template name="DRIVER_DOB">
		<xsl:choose>
			<xsl:when test="DRIVER_DOB=''">
				<tr>
					<td class="midcolora">Please insert Date of Birth.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   85   -->
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
	<!--   86   -->
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
	<!--   87   -->
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
	<!--   88   -->
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
	<!--   88   -->
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
	<!--   89   -->
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
	<!--   90   -->
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
	<!--   95   -->
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
	<!--   90   -->
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
	<!--   91   -->
	<!--   Screen Vehicle Details    -->
	<xsl:template name="USE_VEHICLE">
		<xsl:choose>
			<xsl:when test="USE_VEHICLE='0' or USE_VEHICLE=''">
				<tr>
					<td class="midcolora">Please select Use.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   92   -->
	<!--   Screen Gen info    -->
	<xsl:template name="POLICYNUMBER">
		<xsl:choose>
			<xsl:when test="POLICYNUMBER='' or POLICYNUMBER=NULL">
				<tr>
					<td class="midcolora">Please insert Policy Number.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   93   -->
	<!--   Screen Gen info    -->
	<xsl:template name="COMPANYNAME">
		<xsl:choose>
			<xsl:when test="COMPANYNAME='' or COMPANYNAME=NULL">
				<tr>
					<td class="midcolora">Please insert Company Name.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   94   -->
	<!--  Additional Int   -->
	<xsl:template name="HOLDER_NAME">
		<xsl:choose>
			<xsl:when test="HOLDER_NAME=''">
				<tr>
					<td class="midcolora">Please insert/select Selected Holder/Interest.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   94   -->
	<!--  Additional Int   -->
	<xsl:template name="DEACTIVATEVEHICLE">
		<xsl:choose>
			<xsl:when test="DEACTIVATEVEHICLE='Y'">
				<tr>
					<td class="midcolora">Please select activated vehicle only in Assigned Vehicle.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- 95 -->
	<xsl:template name="APP_VEHICLE_PRIN_OCC_ID">
		<xsl:choose>
			<xsl:when test="APP_VEHICLE_PRIN_OCC_ID=''">
				<tr>
					<td class="midcolora">Please select Principal/Occasional driver.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- 96 -->
	<!-- Added by Charles on 18-Nov-09 for Itrack 6725 -->
	<xsl:template name="PARENTS_INSURANCE">
		<xsl:choose>
			<xsl:when test="PARENTS_INSURANCE=''">
				<tr>
					<td class="midcolora">Please select Parents Insurance.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Added till here -->
	<!--   98 ADDED BY PRAVEEN KUMAR(22-01-09)  --> <!--   Screen AddDriver Details   -->
	<xsl:template name="IN_MILITARY">
		<xsl:choose>
			<xsl:when test="IN_MILITARY=''">
				<tr>
					<td class="midcolora">Please select Are you in the Military?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   99 ADDED BY PRAVEEN KUMAR(22-01-09)  --> <!--   Screen AddDriver Details   -->
	<xsl:template name="STATIONED_IN_US_TERR">
		<xsl:choose>
			<xsl:when test="STATIONED_IN_US_TERR=''">
				<tr>
					<td class="midcolora">Please select Are you stationed in U.S?.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--   100 ADDED BY PRAVEEN KUMAR(22-01-09)  --> <!--   Screen AddDriver Details   -->
	<xsl:template name="HAVE_CAR">
		<xsl:choose>
			<xsl:when test="HAVE_CAR=''">
				<tr>
					<td class="midcolora">Please select Do you have the car with you.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="APP_VEHICLE_PERTYPE_ID">
		<xsl:choose>
			<xsl:when test="APP_VEHICLE_PERTYPE_ID='' or VEHICLE_TYPE_COM='' or VEHICLE_TYPE_PER=''">
				<tr>
					<td class="midcolora">Please select Vehicle Type.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>	
	<xsl:template name="RADIUS_OF_USE">
		<xsl:choose>
			<xsl:when test="RADIUS_OF_USE='' or RADIUS_OF_USE=0">
				<tr>
					<td class="midcolora">Please insert Radius of Use.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="TRANSPORT_CHEMICAL">
		<xsl:choose>
			<xsl:when test="TRANSPORT_CHEMICAL=''">
				<tr>
					<td class="midcolora">Please select Is Vehicle involved in transporting chemical,pollution or hazardous materials.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="COVERED_BY_WC_INSU">
		<xsl:choose>
			<xsl:when test="COVERED_BY_WC_INSU=''">
				<tr>
					<td class="midcolora">Please select Are all drivers covered by WC Insurance.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="HELTH_CARE">
		<xsl:choose>
			<xsl:when test="HELTH_CARE=''">
				<tr>
					<td class="pageheader">Vehicle Coverages:</td>
				</tr>
				<tr>
					<td class="midcolora">Please Enter Health Care Primary Carrier in Additional Information section.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--ADDED BY PRAVEEN KUMAR:ITRACK 5544 -->
	<xsl:template name = "AUTO_POL_NO">
		<xsl:choose>
			<xsl:when test="AUTO_POL_NO=''">
				<tr>
					<td class = "midcolora">Please select Auto Policy Number.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- END -->
	<!--   Screen Application detail Details    -->
	<xsl:template name="DOWN_PAY_MENT">
		<xsl:choose>
			<xsl:when test="DOWN_PAY_MENT=''">
				<tr>
					<td class="midcolora">Please select Down Payment.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Underwriter    -->
	<xsl:template name="POL_UNDERWRITER">
		<xsl:choose>
			<xsl:when test="POL_UNDERWRITER=''">
				<tr>
					<td class="midcolora">Please select Underwriter.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	
	<!--   90   -->
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
	<!-- -->
	<xsl:template name="CLASS_COM">
		<xsl:choose>
			<xsl:when test="CLASS_COM=''">
				<tr>
					<td class="midcolora">Please select Class.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- -->
	<xsl:template name="CLASS_PER">
		<xsl:choose>
			<xsl:when test="CLASS_PER=''">
				<tr>
					<td class="midcolora">Please select Class.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- For error messages -->
	<!-- 1 -->
	<xsl:template name="VEHICLEERRORMESSAGE">
		<tr>
			<td class="pageheader">You must have at least one Vehicle.</td>
		</tr>
	</xsl:template>
	<!-- 2. -->
	<xsl:template name="DRIVEREERRORMESSAGE">
		<tr>
			<td class="pageheader">You must have at least one Driver.</td>
		</tr>
	</xsl:template>
	<!-- 3 -->
	<xsl:template name="VEHICLEDRIVERERRORMESSAGE">
		<tr>
			<td class="pageheader">You must have at least one Vehicle and one Driver.</td>
		</tr>
	</xsl:template>
	<!-- 4 -->
	<xsl:template name="AUTOERRORMESSAGE">
		<tr>
			<td class="pageheader">You must fill all the mandatory information of Auto LOB.</td>
		</tr>
	</xsl:template>
	<!-- 5 -->
	<xsl:template name="AUTOADDINTERRORMESSAGE">
		<tr>
			<td class="pageheader">You must have mandatory Additional Interest Information of Vehicle.</td>
		</tr>
	</xsl:template>
	<!-- 5 -->
	<xsl:template name="AUTOMVRERRORMESSAGE">
		<tr>
			<td class="pageheader">You must have mandatory MVR Information of Driver.</td>
		</tr>
	</xsl:template>
	<!-- Template for showing Info at the initial stage -->
	<xsl:template name="PPADETAIL">
		<!-- Application List -->
		<xsl:call-template name="APPLICATION_LIST_MESSAGESGES" />
		<!-- Vehicle -->
		<xsl:call-template name="VEHICLEMESSAGE" />
		<!-- Additional Interest -->
		<!-- <xsl:call-template name="ADDITIONALINTMESSAGE" /> -->
		<!--Driver  -->
		<xsl:call-template name="DRIVERMESSAGE" />
		<!-- MVR Info -->
		<!-- <xsl:call-template name="MVRMESSAGE" /> -->
		<!-- Gen Info -->
		<xsl:choose>
			<xsl:when test="INPUTXML/AUTOGENINFOS/AUTOGENINFO/IS_RECORD_EXISTS='Y'">
				<xsl:call-template name="GENINFOMESSAGE" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:apply-templates select="INPUTXML/AUTOGENINFOS/AUTOGENINFO"></xsl:apply-templates>
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
			<td class="midcolora">Please select Any vehicles not solely owned.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any vehicles not solely owned' is 'Yes' then Please insert Vehicle Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Has any car been modified, assembled or kit vehicle or have special equipment? (include customized vans and pickups indicate cost).</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Has any car been modified, assembled or kit vehicle or have special equipment? (include customized vans and pickups indicate cost)' is 'Yes' then  Please insert Car Modified Description
			and Cost of the customized equipment.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any existing damage to a vehicle?(Include damaged glass).</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any existing damage to a vehicle?(Include damaged glass)' is 'Yes' then Please insert Damaged Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any car kept at school.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any car kept at school' is 'Yes' then   Please insert Car Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any other auto insurance in household.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any other auto insurance in household' is 'Yes' then Please select Auto Insurance Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Any household member using vehicle out of State over 6 months a year'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any household member using vehicle out of State over 6 months a year' is 'Yes' then Please select State Used.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any drivers license been Suspended, restricted or revoked.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any drivers license been Suspended, restricted or revoked' is 'Yes' then Please insert Driver License Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any driver physically or mentally impaired.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any driver physically or mentally impaired' is 'Yes' then Please insert Driver Impairment Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any financial responsibility filing?(Give driver number and date of filing).</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any financial responsibility filing?(Give driver number and date of filing)' is 'Yes' then Please insert Driver Number And Date Of Filing.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Has insurance been transferred within the agency?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Has insurance been transferred within the agency?' is 'Yes' then Please insert Insurance Transfer Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Has any coverage been declined, cancelled (including for Non Payment) or non-renewed during the last 3 years?'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Has any coverage been declined, cancelled (including for Non Payment) or non-renewed during the last 3 years?' is 'Yes' then Please insert Coverage Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Has the agent inspected the vehicle?.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Has the agent inspected the vehicle?' is 'Yes' then Please insert Agent Inspection Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is any vehicle used for Livery, rental, passenger hire, or to transport persons to work for a fee?'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Is any vehicle used for Livery, rental, passenger hire, or to transport persons to work for a fee?' is 'Yes' then Please insert Transport Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Salvage title.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Salvage title' is 'Yes' then Please insert Salvage Title Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any vehicle considered an antique auto.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any vehicle considered an antique auto' is 'Yes' then Please insert Vehicle Consideration Description.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select 'Is multipolicy discount applied?'.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Is multipolicy discount applied?' is 'Yes' then Please insert Multipolicy Discount Description.</td>
		</tr>
		<tr>
			<td class="midcolora"> Please select Any other licensed drivers in the household that are not listed or rated on this policy.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any other licensed drivers in the household that are not listed or rated on this policy' is 'Yes' then Please insert Full name.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any other licensed drivers in the household that are not listed or rated on this policy' is 'Yes' then Please insert Date of Birth.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Any Prior Losses.</td>
		</tr>
		<tr>
			<td class="midcolora">If 'Any Prior Losses' is 'Yes' then insert Prior Losses Description.</td>
		</tr>
	</xsl:template>
	<!-- Vehicle -->
	<xsl:template name="VEHICLEMESSAGE">
		<tr>
			<td class="pageheader">You must have at least one active vehicle with following information.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Vehicle Use.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Class.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Vehicle Type.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select/insert VIN Number.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Year.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Make of Vehicle.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Model of Vehicle.</td>
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
			<td class="midcolora">Please insert Symbol.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Amount if vehicle type is other than Private Passenger Auto.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Use.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Miles Each Way if use is Drive to Work/School.</td>
		</tr>
		
		<tr>
			<td class="midcolora">Please insert Radius of Use.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Is Vehicle involved in transporting chemical,pollution or hazardous materials.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Are all drivers covered by WC Insurance.</td>
		</tr>
	</xsl:template>
	<!--MVR  -->
	<xsl:template name="MVRMESSAGE">
		<tr>
			<td class="pageheader">For MVR Information:</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Violation .</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert date.</td>
		</tr>
	</xsl:template>
	<!--Driver -->
	<xsl:template name="DRIVERMESSAGE">
		<tr>
			<td class="pageheader">You must have at least one licensed active driver with following information.</td>
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
			<td class="midcolora">Please insert State.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Zip.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Date of Birth.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Gender.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert License Number.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert License State.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Date licensed.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Driver Type.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert Income.</td>
		</tr>
		<tr>
			<td class="midcolora">Please insert No. Of Dependents.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Volunteer Fireman or Policeman.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Are you in the Military?.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Are you stationed in U.S?.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Do you have the car with you.</td>
		</tr>
		<tr>
			<td class="midcolora">If Driver Type is 'Licensed Driver' then Please select Drinking or Drug Related violation in last 5 years.</td>
		</tr>
		<!--
		<tr>
			<td class="midcolora">Please select College Student W/NO Car.</td>
		</tr>
		 -->
		<tr>
			<td class="midcolora">Please select U.S. Citizen.</td>
		</tr>
		<tr>
			<td class="midcolora">Please assign vehicle to driver.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Are you a college student.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Are you in the Military.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Principal/Occasional driver.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Violations. </td>
		</tr>
	</xsl:template>
	<!-- Additional Int -->
	<xsl:template name="ADDITIONALINTMESSAGE">
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
	</xsl:template>
</xsl:stylesheet>