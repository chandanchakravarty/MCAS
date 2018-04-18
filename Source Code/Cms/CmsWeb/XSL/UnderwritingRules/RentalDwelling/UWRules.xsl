<!-- ==================================================================================================
File Name			:	UWRules.xsl
Purpose				:	Rental Dwelling rules implementation
Name				:	Ashwani
Date				:	29 Sep.2005  
========================================================================================================-->
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
		int intIsReferred = 1;
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
			<!-- =========================== Checking for the rejected & referred rules ===============================-->
			<xsl:call-template name="RD_REJECTION_CASES" />
			<xsl:call-template name="RD_REFERRED_CASES" />
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
						<!-- ================================ rejected messages =========================================-->
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/APPLICATIONS/APPLICATION"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/LOCATIONS/LOCATION" />
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/DWELLINGS/DWELLINGINFO"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/RDGENINFOS/RDGENINFO"></xsl:apply-templates>
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
						<!--referred messages-->
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/APPLICATIONS/APPLICATION"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/DWELLINGS/DWELLINGINFO"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/RDGENINFOS/RDGENINFO"></xsl:apply-templates>
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
	<!-- =================================== Checking for rejected rules =================================== -->
	<xsl:template name="RD_REJECTION_CASES">
		<!--  1 -->
		<xsl:call-template name="IS_RJ_OWNER_OCCUPIED" />
		<!--  2  Michigan Only-->
		<!--xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/STATE_ID=22 or INPUTXML/APPLICATIONS/APPLICATION/STATE_ID=14"-->
		<xsl:call-template name="IS_RJ_IS_RENTED_IN_PART" />
		<!--/xsl:if-->
		<!--  3  -->
		<xsl:call-template name="IS_RJ_NO_OF_FAMILIES" />
		<!--  4  -->
		<xsl:call-template name="IS_RJ_IS_VACENT_OCCUPY" />
		<!--  5  -->
		<!-- <xsl:call-template name ="IS_RJ_REJECT5"/> -->
		<!--  6  -->
		<!-- <xsl:call-template name ="IS_RJ_REJECT6"/> -->
		<!--  7  -->
		<xsl:call-template name="IS_RJ_DP3_REPLACEMENT_COST" />
		<xsl:call-template name="IS_RJ_DP2_REPLACEMENT_COST" />
		<!--  8  -->
		<!-- <xsl:call-template name ="IS_RJ_REJECT8"/> -->
		<!--  9  -->
		<xsl:call-template name="IS_RJ_ANY_HEATING_SOURCE" />
		<!--  10  -->
		<xsl:call-template name="IS_RJ_RENTERS" />
		<!--  11 -->
		<xsl:call-template name="IS_RJ_CONVICTION_DEGREE_IN_PAST" />
		<!--  12  -->
		<xsl:call-template name="IS_RJ_NO_OF_AMPS" />
		<!--  13  -->
		<xsl:call-template name="IS_RJ_ROOF_TYPE_DP3_REPAIR" />
		<!--  14  -->
		<xsl:call-template name="IS_RJ_IS_SWIMPOLL_HOTTUB" />
		<!--  15  -->
		<xsl:call-template name="IS_RJ_PROPERTY_USED_WHOLE_PART" />
		<xsl:call-template name="IS_RJ_DWELLING_MOBILE_HOME" />
		<xsl:call-template name="IS_RJ_MODULAR_MANU_HOME" />
		<xsl:call-template name="IS_RJ_MODULAR_MANU_HOME_COV" />
		<xsl:call-template name="IS_RJ_MAX_COV_DP2DP3_REPAIR" />
		<xsl:call-template name="IS_RJ_INACTIVE_APPLICATION" />
		<!--Agency Inactive 28th May 2007-->
		<xsl:call-template name="IS_RJ_INACTIVE_AGENCY" />
		<!-- Bill Mortgagee-->
		<xsl:call-template name="IS_RJ_BILLMORTAGAGEE" />
		<!-- ======================Michigan Only ====================================================== -->
		<!--<xsl:call-template name="IS_RJ_CUSTOMER_INSURANCE_SCORE" />-->
		<!--  16  -->
		<!-- <xsl:call-template name ="IS_RJ_REJECT16"/> -->
		<!--  17  -->
		<!-- <xsl:call-template name ="IS_RJ_REJECT17"/>-->
		<!--  18  -->
		<!-- <xsl:call-template name ="IS_RJ_REJECT18"/> -->
		<!--  19  -->
		<!-- <xsl:call-template name ="IS_RJ_REJECT19"/> -->
		<!--  20  -->
		<!-- <xsl:call-template name ="IS_RJ_REJECT20"/> -->
		<!--  21  -->
		<!-- <xsl:call-template name ="IS_RJ_REJECT21"/> -->
		<!--  22 >
		<xsl:call-template name="IS_RJ_IN_YEAR_INSURANCESCORE" /-->
		<!--4 July 2006>
		<xsl:call-template name="IS_RJ_ELIGIBLE_YEAR" />
		<4 July 2006-->
		<!--Location info 5 July 2006-->
		<xsl:call-template name="IS_RJ_LOCATION_RENTED" />
		<xsl:call-template name="IS_RJ_STATE_LOC_APP" />
		<xsl:call-template name="IS_RJ_ZIP_LOC_APP" />
		<xsl:call-template name="IS_RJ_PRIMARY_HEAT_TYPE" />
		<xsl:call-template name="IS_RJ_SECONDARY_HEAT_TYPE" />
		<xsl:call-template name="IS_RJ_CIRCUIT_BREAKERS" />
		<xsl:call-template name="IS_RJ_DWELL_UNDER_CONSTRUCTION" />
		<!--5 July 2006-->
		<!--7 july 2006-->
		<xsl:call-template name="IS_RJ_REPLACEMENT_COST_PREMIER" />
		<xsl:call-template name="IS_RJ_ROOF_TYPE_DP3_REPLACEMENT" />
		<!--7 july 2006-->
		<!--26 July 2006-->
		<xsl:call-template name="IS_RJ_MIN_COVA_BUILDING" />
		<!--GrandFather Cov 27 July 2006>
		<xsl:call-template name="IS_RJ_GRANDFATHER_COV" />
		<GrandFather Limit 8th Mar 2007 >
		<xsl:call-template name="IS_RJ_GRANDFATHER_LIMIT" />
		<GrandFather Deductible 8th mar 2007>
		<xsl:call-template name="IS_RJ_GRANDFATHER_DEDUCT" />
		<GrandFather Additional Deductible 8th Mar 2007>
		<xsl:call-template name="IS_RJ_GRANDFATHER_ADDDEDUCT" /-->
		<!--GrandFather Cov 27 July 2006-->
	</xsl:template>
	<!-- ========================================== Rejected templates ================================== -->
	<!-- 1. -->
	<xsl:template name="IS_RJ_OWNER_OCCUPIED">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/OCCUPANCY='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 2. -->
	<xsl:template name="IS_RJ_IS_RENTED_IN_PART">
		<xsl:choose>
			<xsl:when test="INPUTXML/RDGENINFOS/RDGENINFO/IS_RENTED_IN_PART='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 3. -->
	<xsl:template name="IS_RJ_NO_OF_FAMILIES">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/NO_OF_FAMILIES='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 4. -->
	<xsl:template name="IS_RJ_IS_VACENT_OCCUPY">
		<xsl:choose>
			<xsl:when test="INPUTXML/RDGENINFOS/RDGENINFO/IS_VACENT_OCCUPY='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 5. -->
	<xsl:template name="IS_RJ_REJECT5">
		<xsl:choose>
			<xsl:when test="INPUTXML/RENTALDWELLINGS/RENTALDWELLING/REJECT5='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 6. -->
	<xsl:template name="IS_RJ_REJECT6">
		<xsl:choose>
			<xsl:when test="INPUTXML/RENTALDWELLINGS/RENTALDWELLING/REJECT6='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 7. -->
	<xsl:template name="IS_RJ_DP3_REPLACEMENT_COST">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/DP3_REPLACEMENT_COST ='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RJ_DP2_REPLACEMENT_COST">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/DP2_REPLACEMENT_COST ='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 8. -->
	<xsl:template name="IS_RJ_REJECT8">
		<xsl:choose>
			<xsl:when test="INPUTXML/RENTALDWELLINGS/RENTALDWELLING/REJECT8 ='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 9. -->
	<xsl:template name="IS_RJ_ANY_HEATING_SOURCE">
		<xsl:choose>
			<xsl:when test="INPUTXML/RDGENINFOS/RDGENINFO/ANY_HEATING_SOURCE='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 10. -->
	<xsl:template name="IS_RJ_RENTERS">
		<xsl:choose>
			<xsl:when test="INPUTXML/RDGENINFOS/RDGENINFO/RENTERS='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 11. -->
	<xsl:template name="IS_RJ_CONVICTION_DEGREE_IN_PAST">
		<xsl:choose>
			<xsl:when test="INPUTXML/RDGENINFOS/RDGENINFO/CONVICTION_DEGREE_IN_PAST='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 12. -->
	<xsl:template name="IS_RJ_NO_OF_AMPS">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/NO_OF_AMPS ='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 13. -->
	<xsl:template name="IS_RJ_REJECT13">
		<xsl:choose>
			<xsl:when test="INPUTXML/RENTALDWELLINGS/RENTALDWELLING/REJECT13='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 14. -->
	<xsl:template name="IS_RJ_IS_SWIMPOLL_HOTTUB">
		<xsl:choose>
			<xsl:when test="INPUTXML/RDGENINFOS/RDGENINFO/IS_SWIMPOLL_HOTTUB='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 15. 
	<xsl:template name="IS_RJ_CUSTOMER_INSURANCE_SCORE">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/MI_CUSTOMER_INSURANCE_SCORE='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>-->
	<!-- 16. -->
	<xsl:template name="IS_RJ_REJECT16">
		<xsl:choose>
			<xsl:when test="INPUTXML/RENTALDWELLINGS/RENTALDWELLING/REJECT16='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 17. -->
	<xsl:template name="IS_RJ_REJECT17">
		<xsl:choose>
			<xsl:when test="INPUTXML/RENTALDWELLINGS/RENTALDWELLING/REJECT17 ='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 18. -->
	<xsl:template name="IS_RJ_REJECT18">
		<xsl:choose>
			<xsl:when test="INPUTXML/RENTALDWELLINGS/RENTALDWELLING/REJECT18 ='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 19. -->
	<xsl:template name="IS_RJ_REJECT19">
		<xsl:choose>
			<xsl:when test="INPUTXML/RENTALDWELLINGS/RENTALDWELLING/REJECT19='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 20. -->
	<xsl:template name="IS_RJ_REJECT20">
		<xsl:choose>
			<xsl:when test="INPUTXML/RENTALDWELLINGS/RENTALDWELLING/REJECT20='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 21. -->
	<xsl:template name="IS_RJ_REJECT21">
		<xsl:choose>
			<xsl:when test="INPUTXML/RENTALDWELLINGS/RENTALDWELLING/REJECT21='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 22. -->
	<xsl:template name="IS_RJ_IN_YEAR_INSURANCESCORE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/IN_YEAR_INSURANCESCORE='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--23-->
	<!--<xsl:template name="IS_RJ_ELIGIBLE_YEAR">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/ELIGIBLE_YEAR='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>-->
	<!--24-->
	<xsl:template name="IS_RJ_LOCATION_RENTED">
		<xsl:choose>
			<xsl:when test="INPUTXML/LOCATIONS/LOCATION/LOCATION_RENTED='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--25-->
	<xsl:template name="IS_RJ_ROOF_TYPE_DP3_REPAIR">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/ROOF_TYPE_DP3_REPAIR ='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--26-->
	<xsl:template name="IS_RJ_PRIMARY_HEAT_TYPE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/PRIMARY_HEATING_TYPE ='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--27-->
	<xsl:template name="IS_RJ_SECONDARY_HEAT_TYPE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/SECONDARY_HEATING_TYPE ='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--28-->
	<xsl:template name="IS_RJ_CIRCUIT_BREAKERS">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/CIRCUIT_BREAKERS ='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--29-->
	<xsl:template name="IS_RJ_PROPERTY_USED_WHOLE_PART">
		<xsl:choose>
			<xsl:when test="INPUTXML/RDGENINFOS/RDGENINFO/PROPERTY_USED_WHOLE_PART='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--30-->
	<xsl:template name="IS_RJ_DWELLING_MOBILE_HOME">
		<xsl:choose>
			<xsl:when test="INPUTXML/RDGENINFOS/RDGENINFO/DWELLING_MOBILE_HOME='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--31-->
	<xsl:template name="IS_RJ_MODULAR_MANU_HOME">
		<xsl:choose>
			<xsl:when test="INPUTXML/RDGENINFOS/RDGENINFO/MODULAR_MANU_HOME='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--31-->
	<xsl:template name="IS_RJ_MODULAR_MANU_HOME_COV">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/COVA_MODULAR_MANU_HOME='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--32-->
	<xsl:template name="IS_RJ_MAX_COV_DP2DP3_REPAIR">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/MAX_COV_DP2DP3_REPAIR='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--33-->
	<xsl:template name="IS_RJ_DWELL_UNDER_CONSTRUCTION">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/DWELL_UNDER_CONSTRUCTION ='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--34-->
	<xsl:template name="IS_RJ_REPLACEMENT_COST_PREMIER">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/REPLACEMENT_COST_PREMIER='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--35-->
	<xsl:template name="IS_RJ_ROOF_TYPE_DP3_REPLACEMENT">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/ROOF_TYPE_DP3_REPLACEMENT ='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--36-->
	<xsl:template name="IS_RJ_MIN_COVA_BUILDING">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/MIN_COVA_BUILDING='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--37-->
	<!-- 38 GrandFather Coverage -->
	<xsl:template name="IS_RJ_GRANDFATHER_COV">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/COV/COV_DES !='' and INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/WOLVERINE_USER='N'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 38 GrandFather Coverage Limit -->
	<xsl:template name="IS_RJ_GRANDFATHER_LIMIT">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/LIMIT/LIMIT_DES !='' and INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/WOLVERINE_USER='N'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 38 GrandFather Coverage Deductible >
	<xsl:template name="IS_RJ_GRANDFATHER_DEDUCT">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/DEDUCT/DEDUCT_DES !='' and INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/WOLVERINE_USER='N'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<38 GrandFather Coverage Additional Deductible >
	<xsl:template name="IS_RJ_GRANDFATHER_ADDDEDUCT">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/ADDDEDUCT/ADDDEDUCT_DES !='' and INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/WOLVERINE_USER='N'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<139-->
	<xsl:template name="IS_RJ_STATE_LOC_APP">
		<xsl:choose>
			<xsl:when test="INPUTXML/LOCATIONS/LOCATION/STATE_LOC_APP='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--40-->
	<xsl:template name="IS_RJ_ZIP_LOC_APP">
		<xsl:choose>
			<xsl:when test="INPUTXML/LOCATIONS/LOCATION/ZIP_LOC_APP='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
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
	<!--BILL MORTAGAGEE 28th May  2007 -->
	<xsl:template name="IS_RJ_BILLMORTAGAGEE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/ADDINTEREST/BILLMORTAGAGEE='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--End Rejected Templates -->
	<!-- =========================================== Checking for referred Cases ========================== -->
	<xsl:template name="RD_REFERRED_CASES">
		<!-- 1. -->
		<xsl:call-template name="IS_RF_ANY_FARMING_BUSINESS_COND" />
		<!-- 2. -->
		<xsl:call-template name="IS_RF_IS_DWELLING_OWNED_BY_OTHER" />
		<!-- 3. -->
		<!-- <xsl:call-template name ="IS_RF_REFERRED3"/>-->
		<!-- 4. -->
		<!--<xsl:call-template name ="IS_RF_REFERRED4"/>-->
		<!-- 5. -->
		<!-- <xsl:call-template name ="IS_RF_REFERRED5"/>-->
		<!-- 6. -->
		<!--<xsl:call-template name ="IS_RF_REFERRED6"/>-->
		<!-- 7. -->
		<xsl:call-template name="IS_RF_ANY_COV_DECLINED_CANCELED" />
		<!-- 8. -->
		<xsl:call-template name="IS_RF_ANIMALS_EXO_PETS_HISTORY" />
		<!-- 9. -->
		<!--<xsl:call-template name ="IS_RF_REFERRED9"/>-->
		<!-- 10. -->
		<!-- <xsl:call-template name ="IS_RF_REFERRED10"/> -->
		<!-- 11. -->
		<!-- <xsl:call-template name ="IS_RF_REFERRED11"/>-->
		<!-- 12. -->
		<!--<xsl:call-template name ="IS_RF_REFERRED12"/>-->
		<!-- 13. -->
		<xsl:call-template name="IS_CHARGE_OFF_PRMIUM" />		
		<xsl:call-template name="IS_RF_PICH_OF_LOC" />
		<xsl:call-template name="IS_RF_PROPERTY_INSP_CREDIT" />
		<!-- 14 -->
		<xsl:call-template name="IS_DFI_ACC_NO_RULE" />
		<!--Effective Date -->
		<xsl:call-template name="IS_APPEFFECTIVEDATE" />
		<!--Effective Date -->
		<xsl:call-template name="IS_TOTAL_PREMIUM_AT_RENEWAL" />
		<xsl:call-template name="IS_CLAIM_EFFECTIVE" />
		<!-- 15 -->
		<!--21 Aug 2007 -->
		<xsl:call-template name="IS_CREDIT_CARD" />
		<!--4 July 2006-->
		<xsl:call-template name="IS_RF_HO_CLAIMS" />
		<xsl:call-template name="IS_RF_COVA_REAPAIR_COST" />
		<xsl:call-template name="IS_RF_COVA_BUILDING" />
		<xsl:call-template name="IS_RF_PRIOR_LOSS_INFO_EXISTS" />
		<xsl:call-template name="IS_RF_MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS" />
		
		<!--<xsl:call-template name="IS_RF_MIN_COVA_BUILDING" />-->
		<xsl:call-template name="IS_RF_COVA_DP3_PREMIUM" />
		<!--4 July 2006-->
		<!--5 July 2006-->
		<xsl:call-template name="IS_RF_MARKET_VALUE_DP3P" />
		<xsl:call-template name="IS_RF_ELIGIBLE_YEAR" />		
		<xsl:call-template name="IS_RF_CUSTOMER_INSURANCE_SCORE" />
		<xsl:call-template name="IS_RF_COVA_DP3" />
		<xsl:call-template name="IS_RF_COVA_DP3P" />
		<xsl:call-template name="IS_RF_COVA_DP2DP3" />
		<xsl:call-template name="IS_RF_UPDATE_YEAR_DP3REPAIR" />
		<xsl:call-template name="IS_RF_ROOF_UPDATE_RENOVATION" />
		<xsl:call-template name="IS_RF_UNDER_CONSTRUCTION_POLICYTERM" />
		<!--5 July 2006-->
		<xsl:call-template name="IS_RF_IN_YEAR_INSURANCESCORE" />
		<!--7 July 2006-->
		<xsl:call-template name="IS_RF_ELIGIBLE_YEAR_DP3" />
		<xsl:call-template name="IS_RF_COVA_DP3_REPLACEMENT" />
		<!--Refer Coverage at Renewal Level ,10th Apr 2007 -->
		<xsl:call-template name="IS_RF_COPY_COVERAGE_AT_RENEWAL" />
		<!--7 July 2006-->
		<!--GrandFather Cov 27 July 2006-->
		<xsl:call-template name="IS_RF_GRANDFATHER_COV" />
		<!--GrandFather Limit 8 th Mar 2007-->
		<xsl:call-template name="IS_RF_GRANDFATHER_LIMIT" />
		<!--GrandFather  Deductible 8 th Mar 2007-->
		<xsl:call-template name="IS_RF_GRANDFATHER_DEDUCT" />
		<!--GrandFather Additional Deductible 8 th Mar 2007 -->
		<xsl:call-template name="IS_RF_GRANDFATHER_ADDDEDUCT" />
		<!--GrandFather Cov 27 July 2006-->
		<!--  ====================================  Indiania Only ======================================-->
		<!--xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/STATE_ID=14 ">
			<xsl:call-template name="IS_RF_IS_RENTED_IN_PART" />
		</xsl:if-->
	</xsl:template>
	<!--================================= Start Reffered Templates ========================================= -->
	<!-- 1. -->
	<xsl:template name="IS_RF_ANY_FARMING_BUSINESS_COND">
		<xsl:choose>
			<xsl:when test="INPUTXML/RDGENINFOS/RDGENINFO/ANY_FARMING_BUSINESS_COND ='Y' ">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 2. -->
	<xsl:template name="IS_RF_IS_DWELLING_OWNED_BY_OTHER">
		<xsl:choose>
			<xsl:when test="INPUTXML/RDGENINFOS/RDGENINFO/IS_DWELLING_OWNED_BY_OTHER='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 3. -->
	<!--xsl:template name="IS_RF_IS_RENTED_IN_PART">
		<xsl:choose>
			<xsl:when test="INPUTXML/RDGENINFOS/RDGENINFO/IS_RENTED_IN_PART='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template-->
	<!--  4. -->
	<xsl:template name="IS_RF_REFERRED4">
		<xsl:choose>
			<xsl:when test="INPUTXML/RENTALDWELLINGS/RENTALDWELLING/REFERRED4='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 5. -->
	<xsl:template name="IS_RF_REFERRED5">
		<xsl:choose>
			<xsl:when test="INPUTXML/RENTALDWELLINGS/RENTALDWELLING/REFERRED5='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 6. -->
	<xsl:template name="IS_RF_REFERRED6">
		<xsl:choose>
			<xsl:when test="INPUTXML/RENTALDWELLINGS/RENTALDWELLING/REFERRED6='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 7. -->
	<xsl:template name="IS_RF_ANY_COV_DECLINED_CANCELED">
		<xsl:choose>
			<xsl:when test="INPUTXML/RDGENINFOS/RDGENINFO/ANY_COV_DECLINED_CANCELED='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 8. -->
	<xsl:template name="IS_RF_ANIMALS_EXO_PETS_HISTORY">
		<xsl:choose>
			<xsl:when test="INPUTXML/RDGENINFOS/RDGENINFO/ANIMALS_EXO_PETS_HISTORY='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 9. -->
	<xsl:template name="IS_RF_REFERRED9">
		<xsl:choose>
			<xsl:when test="INPUTXML/RENTALDWELLINGS/RENTALDWELLING/REFERRED9 = 'Y' ">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 10. -->
	<xsl:template name="IS_RF_REFERRED10">
		<xsl:choose>
			<xsl:when test="INPUTXML/RENTALDWELLINGS/RENTALDWELLING/REFERRED10='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 11. -->
	<xsl:template name="IS_RF_REFERRED11">
		<xsl:choose>
			<xsl:when test="INPUTXML/RENTALDWELLINGS/RENTALDWELLING/REFERRED11='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--  12. -->
	<xsl:template name="IS_RF_REFERRED12">
		<xsl:choose>
			<xsl:when test="INPUTXML/RENTALDWELLINGS/RENTALDWELLING/REFERRED12='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 13. -->
	<xsl:template name="IS_CHARGE_OFF_PRMIUM">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/CHARGE_OFF_PRMIUM='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--24 Dec 2007-->
	<xsl:template name="IS_RF_PICH_OF_LOC">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/PICH_OF_LOC='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_PROPERTY_INSP_CREDIT">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/PROPERTY_INSP_CREDIT='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 13. -->
	<xsl:template name="IS_DFI_ACC_NO_RULE">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/DFI_ACC_NO_RULE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Effective Date -->
	<xsl:template name="IS_APPEFFECTIVEDATE">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/APPEFFECTIVEDATE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_TOTAL_PREMIUM_AT_RENEWAL">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/TOTAL_PREMIUM_AT_RENEWAL='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_CLAIM_EFFECTIVE">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/CLAIM_EFFECTIVE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<!--Credit Card -->
	<xsl:template name="IS_CREDIT_CARD">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/CREDIT_CARD='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--4 July 2006-->
	<xsl:template name="IS_RF_HO_CLAIMS">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/HO_CLAIMS='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_COVA_REAPAIR_COST">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/COVA_REAPAIR_COST='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_COVA_BUILDING">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/COVA_BUILDING='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_PRIOR_LOSS_INFO_EXISTS">
		<xsl:choose>
			<xsl:when test="INPUTXML/RDGENINFOS/RDGENINFO/PRIOR_LOSS_INFO_EXISTS ='Y' ">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS">
		<xsl:choose>
			<xsl:when test="INPUTXML/RDGENINFOS/RDGENINFO/MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y' ">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--<xsl:template name="IS_RF_MIN_COVA_BUILDING">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/MIN_COVA_BUILDING='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>-->
	<xsl:template name="IS_RF_COVA_DP3_PREMIUM">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/COVA_DP3_PREMIUM='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--4 July 2006-->
	<!--5 July 2006-->
	<xsl:template name="IS_RF_MARKET_VALUE_DP3P">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/MARKET_VALUE_DP3P='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_ELIGIBLE_YEAR">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/ELIGIBLE_YEAR='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<xsl:template name="IS_RF_CUSTOMER_INSURANCE_SCORE">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/MI_CUSTOMER_INSURANCE_SCORE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_COVA_DP3">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/COVA_DP3='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_COVA_DP3P">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/COVA_DP3P='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_COVA_DP2DP3">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/COVA_DP2DP3='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_UPDATE_YEAR_DP3REPAIR">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/UPDATE_YEAR_DP3REPAIR='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<xsl:template name="IS_RF_ROOF_UPDATE_RENOVATION">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/ROOF_UPDATE_RENOVATION='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<xsl:template name="IS_RF_UNDER_CONSTRUCTION_POLICYTERM">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/UNDER_CONSTRUCTION_POLICYTERM='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<!--5 July 2006-->
	<!-- 22. -->
	<xsl:template name="IS_RF_IN_YEAR_INSURANCESCORE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/IN_YEAR_INSURANCESCORE='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--7 July 2006-->
	<xsl:template name="IS_RF_ELIGIBLE_YEAR_DP3">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/ELIGIBLE_YEAR_DP3='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_COVA_DP3_REPLACEMENT">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/COVA_DP3_REPLACEMENT='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--10TH APR 2007 -->
	<xsl:template name="IS_RF_COPY_COVERAGE_AT_RENEWAL">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/COPY_COVERAGE_AT_RENEWAL='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--7 July 2006-->
	<!-- 17.GrandFather Coverage -->
	<xsl:template name="IS_RF_GRANDFATHER_COV">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/COV/COV_DES !='' and INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/WOLVERINE_USER='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 17.GrandFather Coverage -->
	<xsl:template name="IS_RF_GRANDFATHER_LIMIT">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/LIMIT/LIMIT_DES !='' and INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/WOLVERINE_USER='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template> <!-- 17.GrandFather Coverage -->
	<xsl:template name="IS_RF_GRANDFATHER_DEDUCT">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/DEDUCT/DEDUCT_DES !='' and INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/WOLVERINE_USER='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template> <!-- 17.GrandFather Coverage -->
	<xsl:template name="IS_RF_GRANDFATHER_ADDDEDUCT">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/ADDDEDUCT/ADDDEDUCT_DES !='' and INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/WOLVERINE_USER='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ======================================= End Reffered Templates ==================================== -->
	<!--  ================================== Calling for rejected rules (Messages) ========================= -->
	<!-- Application Information  -->
	<xsl:template match="INPUTXML/APPLICATIONS/APPLICATION">
		<xsl:choose>
			<!-- Rejected  Info -->
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
				<!-- Referred Info -->
				<xsl:choose>
					<xsl:when test="CHARGE_OFF_PRMIUM='Y' or DFI_ACC_NO_RULE='Y' or CREDIT_CARD='Y' or HO_CLAIMS='Y' or MI_CUSTOMER_INSURANCE_SCORE='Y' or APPEFFECTIVEDATE='Y' or TOTAL_PREMIUM_AT_RENEWAL='Y' or CLAIM_EFFECTIVE='Y' or PICH_OF_LOC='Y' 
					or PROPERTY_INSP_CREDIT='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Application information:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="MI_CUSTOMER_INSURANCE_SCORE" />
						<xsl:call-template name="CHARGE_OFF_PRMIUM" />
						<xsl:call-template name="HO_CLAIMS" />
						<xsl:call-template name="DFI_ACC_NO_RULE" />
						<xsl:call-template name="CREDIT_CARD" />
						<xsl:call-template name="APPEFFECTIVEDATE" />
						<xsl:call-template name="TOTAL_PREMIUM_AT_RENEWAL" />
						<xsl:call-template name="CLAIM_EFFECTIVE" />						
						<xsl:call-template name="PICH_OF_LOC"></xsl:call-template>
						<xsl:call-template name="PROPERTY_INSP_CREDIT"></xsl:call-template>						
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--Shows the location Info-->
	<xsl:template match="INPUTXML/LOCATIONS/LOCATION">
		<xsl:choose>
			<!-- Rejected  Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="LOCATION_RENTED='Y' or STATE_LOC_APP='Y' or ZIP_LOC_APP='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Location Info:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="LOCATION_RENTED" />
						<xsl:call-template name="STATE_LOC_APP" />
						<xsl:call-template name="ZIP_LOC_APP" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--End Location Info-->
	<!-- Shows the dwelling Information -->
	<xsl:template match="INPUTXML/DWELLINGS/DWELLINGINFO">
		<xsl:if test="user:DwellingInfoCount(1) = 0">
			<tr>
				<td class="pageheader">For Dwelling Information:</td>
			</tr>
			<tr>
				<td>
					<xsl:call-template name="DWELLINGTOP" />
				</td>
			</tr>
			<tr>
				<td>
					<xsl:apply-templates select="DWELLING" />
				</td>
			</tr>
			<tr>
				<td>
					<xsl:apply-templates select="RATINGINFO" />
				</td>
			</tr>
			<tr>
				<td>
					<xsl:apply-templates select="ADDINTEREST" />
				</td>
			</tr>
			<tr>
				<td>
					<xsl:apply-templates select="COVERAGE" />
				</td>
			</tr>
		</xsl:if>
	</xsl:template>
	<!-- Dwelling Info -->
	<xsl:template match="DWELLING">
		<xsl:choose>
			<!-- Rejected  Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="OCCUPANCY='Y' or DP3_REPLACEMENT_COST='Y' or DP2_REPLACEMENT_COST='Y'
					   or REPLACEMENT_COST_PREMIER='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Dwelling Info:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="OCCUPANCY" />
						<xsl:call-template name="DP3_REPLACEMENT_COST" />
						<xsl:call-template name="DP2_REPLACEMENT_COST" />					
						<xsl:call-template name="REPLACEMENT_COST_PREMIER" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred Info -->
				<xsl:choose>
					<xsl:when test="MARKET_VALUE_DP3P='Y' or ELIGIBLE_YEAR='Y' or IN_YEAR_INSURANCESCORE='Y' or ELIGIBLE_YEAR_DP3='Y' or MARKET_VALUE_DP3_REPLACEMENT='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Dwelling Info:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="MARKET_VALUE_DP3P" />
						<xsl:call-template name="ELIGIBLE_YEAR_DP3" />
						<xsl:call-template name="MARKET_VALUE_DP3_REPLACEMENT" />
						<xsl:call-template name="IN_YEAR_INSURANCESCORE" />						
						<xsl:call-template name="ELIGIBLE_YEAR" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Rating Info -->
	<xsl:template match="RATINGINFO">
		<xsl:choose>
			<!-- Rejected  Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="NO_OF_FAMILIES='Y' or NO_OF_AMPS='Y' or ROOF_TYPE_DP3_REPAIR='Y' or PRIMARY_HEATING_TYPE='Y' or SECONDARY_HEATING_TYPE='Y' or CIRCUIT_BREAKERS='Y' or DWELL_UNDER_CONSTRUCTION='Y'or ROOF_TYPE_DP3_REPLACEMENT='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Rating Info:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="NO_OF_FAMILIES" />
						<xsl:call-template name="NO_OF_AMPS" />
						<xsl:call-template name="ROOF_TYPE_DP3_REPAIR" />
						<xsl:call-template name="PRIMARY_HEATING_TYPE" />
						<xsl:call-template name="SECONDARY_HEATING_TYPE" />
						<xsl:call-template name="CIRCUIT_BREAKERS" />
						<xsl:call-template name="DWELL_UNDER_CONSTRUCTION" />
						<xsl:call-template name="ROOF_TYPE_DP3_REPLACEMENT" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred Info -->
				<xsl:choose>
					<xsl:when test="UPDATE_YEAR_DP3REPAIR='Y' or ROOF_UPDATE_RENOVATION='Y' or UNDER_CONSTRUCTION_POLICYTERM='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Rating Info:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="UPDATE_YEAR_DP3REPAIR" />						
						<xsl:call-template name="ROOF_UPDATE_RENOVATION" />
						<xsl:call-template name="UNDER_CONSTRUCTION_POLICYTERM" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Additional Interest-->
	<!-- Rating Info -->
	<xsl:template match="ADDINTEREST">
		<xsl:choose>
			<!-- Rejected  Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="BILLMORTAGAGEE='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Additional Interest:
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="BILLMORTAGAGEE" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<!-- Referred Info -->
			<!--<xsl:when test="user:CheckRefer(1) = 0">
				<xsl:choose>
					<xsl:when test="'Y'='N'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Additional Interest:</td>
									</tr>
								</table>
							</td>
						</tr>
					</xsl:when>
				</xsl:choose>
			</xsl:when>-->
		</xsl:choose>
	</xsl:template>
	<!-- Coverage Info -->
	<xsl:template match="COVERAGE">
		<xsl:choose>
			<!-- Rejected  Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<!--xsl:when test="COVA_MODULAR_MANU_HOME='Y' or MAX_COV_DP2DP3_REPAIR='Y' or MIN_COVA_BUILDING='Y' or COV/COV_DES != ''
					or LIMIT/LIMIT_DES!='' or DEDUCT/DEDUCT_DES!='' or ADDDEDUCT/ADDDEDUCT_DES!=''"-->
					<xsl:when test="COVA_MODULAR_MANU_HOME='Y' or MAX_COV_DP2DP3_REPAIR='Y' or MIN_COVA_BUILDING='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For SectionI/SectionII:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="COVA_MODULAR_MANU_HOME" />
						<xsl:call-template name="MAX_COV_DP2DP3_REPAIR" />
						<xsl:call-template name="MIN_COVA_BUILDING" />
						<!--GrandFather Coverages>
						<xsl:call-template name="COV_DESCRIPTION" />
						<xsl:call-template name="LIMIT_DESCRIPTION" />
						<xsl:call-template name="DEDUCT_DESCRIPTION" />
						<xsl:call-template name="ADDDEDUCT_DESCRIPTION" /-->
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred Info -->
				<xsl:choose>
					<xsl:when test="COVA_REAPAIR_COST='Y' or COVA_BUILDING='Y'  or COVA_DP3_PREMIUM='Y' or COVA_DP3='Y' or COVA_DP3P='Y' or COVA_DP2DP3='Y'
					                or COVA_DP3_REPLACEMENT='Y' or COV/COV_DES != ''  or LIMIT/LIMIT_DES!='' or DEDUCT/DEDUCT_DES!=''
					                 or ADDDEDUCT/ADDDEDUCT_DES!='' or COPY_COVERAGE_AT_RENEWAL='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For SectionI/SectionII:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="COVA_REAPAIR_COST" />
						<xsl:call-template name="COVA_BUILDING" />
						<!--<xsl:call-template name="MIN_COVA_BUILDING" />-->
						<xsl:call-template name="COVA_DP3_PREMIUM" />
						<!--5 july 2006-->
						<xsl:call-template name="COVA_DP3" />
						<xsl:call-template name="COVA_DP3P" />
						<xsl:call-template name="COVA_DP2DP3" />
						<!--5 july 2006-->
						<!--7 july 2006-->
						<xsl:call-template name="COVA_DP3_REPLACEMENT" />
						<!--7 july 2006-->
						<!-- 10th Apr 2007 -->
						<xsl:call-template name="COPY_COVERAGE_AT_RENEWAL" />
						<!--GrandFather Coverages-->
						<xsl:call-template name="COV_DESCRIPTION" />
						<xsl:call-template name="LIMIT_DESCRIPTION" />
						<xsl:call-template name="DEDUCT_DESCRIPTION" />
						<xsl:call-template name="ADDDEDUCT_DESCRIPTION" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- General Info -->
	<xsl:template match="INPUTXML/RDGENINFOS/RDGENINFO">
		<xsl:choose>
			<!-- Rejected General Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="IS_RENTED_IN_PART='Y' or IS_VACENT_OCCUPY='Y' or ANY_HEATING_SOURCE='Y'
					or CONVICTION_DEGREE_IN_PAST='Y' or IS_SWIMPOLL_HOTTUB='Y' or RENTERS='Y' or PROPERTY_USED_WHOLE_PART='Y' or DWELLING_MOBILE_HOME='Y' or MODULAR_MANU_HOME='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Underwriting Questions:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="IS_RENTED_IN_PART" />
						<xsl:call-template name="IS_VACENT_OCCUPY" />
						<xsl:call-template name="ANY_HEATING_SOURCE" />
						<xsl:call-template name="RENTERS" />
						<xsl:call-template name="CONVICTION_DEGREE_IN_PAST" />
						<xsl:call-template name="IS_SWIMPOLL_HOTTUB" />
						<xsl:call-template name="PROPERTY_USED_WHOLE_PART" />
						<xsl:call-template name="DWELLING_MOBILE_HOME" />
						<xsl:call-template name="MODULAR_MANU_HOME" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred General Info -->
				<xsl:choose>
					<xsl:when test="ANY_FARMING_BUSINESS_COND='Y' or IS_DWELLING_OWNED_BY_OTHER='Y'
					 or ANY_COV_DECLINED_CANCELED='Y' or ANIMALS_EXO_PETS_HISTORY='Y' or PRIOR_LOSS_INFO_EXISTS='Y' or 
					 MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Underwriting Questions:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="ANY_FARMING_BUSINESS_COND" />
						<xsl:call-template name="IS_DWELLING_OWNED_BY_OTHER" />
						<xsl:call-template name="ANY_COV_DECLINED_CANCELED" />
						<xsl:call-template name="ANIMALS_EXO_PETS_HISTORY" />
						<xsl:call-template name="PRIOR_LOSS_INFO_EXISTS" />
						<xsl:call-template name="MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS" />						
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- *************** Templates for showing rejected rules messages **************** -->
	<!--1. Owner occupied. -->
	<xsl:template name="OCCUPANCY">
		<xsl:choose>
			<xsl:when test="OCCUPANCY='Y'">
				<TR>
					<td class="midcolora">Dwelling is occupied by owner.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--2. Dwellings rented to college students under 30 years old.-->
	<xsl:template name="IS_RENTED_IN_PART">
		<xsl:choose>
			<xsl:when test="IS_RENTED_IN_PART ='Y'">
				<TR>
					<td class="midcolora">Dwellings rented to college students under 30 years old.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--3. Dwellings with more than 2 families.-->
	<xsl:template name="NO_OF_FAMILIES">
		<xsl:choose>
			<xsl:when test="NO_OF_FAMILIES ='Y'">
				<TR>
					<td class="midcolora">Dwelling has more than 2 families.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--4. Vacant or unoccupied property. -->
	<xsl:template name="IS_VACENT_OCCUPY">
		<xsl:choose>
			<xsl:when test="IS_VACENT_OCCUPY ='Y'">
				<TR>
					<td class="midcolora">Dwelling is currently vacant or unoccupied.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--5. -->
	<xsl:template name="RJ_REJECT5">
		<xsl:choose>
			<xsl:when test="REJECT5 ='Y'">
				<TR>
					<td class="midcolora">Dwelling is used for illegal or demonstrably hazardous purposes.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--6. Dwellings, outbuildings or premises in poor physical condition or lacking maintenance-->
	<xsl:template name="RJ_REJECT6">
		<xsl:choose>
			<xsl:when test="REJECT6 ='Y'">
				<TR>
					<td class="midcolora">Dwelling is in poor physical condition or lacking maintenance.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- 7. Modular or manufactured homes less than $75,000 in value.-->
	<!-- DP-3 should only come up when the replacement cost is less than $75,000. -->
	<xsl:template name="DP3_REPLACEMENT_COST">
		<xsl:choose>
			<xsl:when test="DP3_REPLACEMENT_COST ='Y'">
				<TR>
					<td class="midcolora">Replacement Cost of dwelling is less than $75,000.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- DP-2 should only come up when the replacement cost is less than $30,000. -->
	<xsl:template name="DP2_REPLACEMENT_COST">
		<xsl:choose>
			<xsl:when test="DP2_REPLACEMENT_COST ='Y'">
				<TR>
					<td class="midcolora">Replacement Cost of dwelling is less than $30,000.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--8.Dwellings that are self-constructed unless by a licensed contractor -->
	<xsl:template name="RJ_REJECT8">
		<xsl:choose>
			<xsl:when test="REJECT8 ='Y'">
				<TR>
					<td class="midcolora">Dwelling is self-constructed.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--9. Dwellings with a wood stove or coal heating stove or fireplace insert other than a normal fireplace. -->
	<xsl:template name="ANY_HEATING_SOURCE">
		<xsl:choose>
			<xsl:when test="ANY_HEATING_SOURCE ='Y'">
				<TR>
					<td class="midcolora">Dwelling(s) having supplemental heating source (wood stove,fireplace inserts,etc.).</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--10. Any dwelling that has rooming or boarding rented by the day of the week. -->
	<xsl:template name="RENTERS">
		<xsl:choose>
			<xsl:when test="RENTERS ='Y'">
				<TR>
					<td class="midcolora">Dwelling(s) having rooming or boarding rented by the day or the week (not of the week).</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--11. Any person who has been convicted of arson or any other felony. -->
	<xsl:template name="CONVICTION_DEGREE_IN_PAST">
		<xsl:choose>
			<xsl:when test="CONVICTION_DEGREE_IN_PAST ='Y'">
				<TR>
					<td class="midcolora">Convicted of arson in last 10 years.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--12. -->
	<xsl:template name="NO_OF_AMPS">
		<xsl:choose>
			<xsl:when test="NO_OF_AMPS ='Y'">
				<TR>
					<td class="midcolora">Dwelling has less than 100 Amps electrical service.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--13. -->
	<xsl:template name="RJ_REJECT13">
		<xsl:choose>
			<xsl:when test="REJECT13 ='Y'">
				<TR>
					<td class="midcolora">Liability claim in the preceding 3 years arising out of the negligence of an insured.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--14. -->
	<xsl:template name="IS_SWIMPOLL_HOTTUB">
		<xsl:choose>
			<xsl:when test="IS_SWIMPOLL_HOTTUB ='Y'">
				<TR>
					<td class="midcolora">Dwelling(s) with an enclosed swimming pool or jetted hot tub.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--15. -->
	<xsl:template name="MI_CUSTOMER_INSURANCE_SCORE">
		<xsl:choose>
			<xsl:when test="MI_CUSTOMER_INSURANCE_SCORE ='Y'">
				<tr>
					<td colspan="4">
						<table width="60%">
							<tr>
								<td class="pageheader" width="18%">Customer Name:</td>
								<td class="midcolora" width="36%">
									<xsl:value-of select="CUSTOMER_NAME" />
								</td>
								<td class="pageheader">Address:</td>
								<td class="midcolora">
									<xsl:value-of select="ADDRESS" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="pageheader">For Customer Details:</td>
				</tr>
				<TR>
					<td class="midcolora">Insurance Score of the applicant is less than 550.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--16. -->
	<xsl:template name="RJ_REJECT16">
		<xsl:choose>
			<xsl:when test="REJECT16 ='Y'">
				<TR>
					<td class="midcolora">Structural damage or lack of maintenance.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- 17. -->
	<xsl:template name="RJ_REJECT17">
		<xsl:choose>
			<xsl:when test="REJECT17 ='Y'">
				<TR>
					<td class="midcolora">Any structure with an outside stairway exposure that is not protected from weather elements..</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- 18. -->
	<xsl:template name="RJ_REJECT18">
		<xsl:choose>
			<xsl:when test="REJECT18 ='Y'">
				<TR>
					<td class="midcolora">Broken, sagging or unsupported steps or stairs that create an extreme tripping hazard.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- 19. -->
	<xsl:template name="RJ_REJECT19">
		<xsl:choose>
			<xsl:when test="REJECT19 ='Y'">
				<TR>
					<td class="midcolora">Severely broken or uneven sidewalks, holes in driveways or other areas of the grounds that in all likelihood would cause a person to fall.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--20. -->
	<xsl:template name="RJ_REJECT20">
		<xsl:choose>
			<xsl:when test="REJECT20='Y'">
				<TR>
					<td class="midcolora">Unrepaired prior damage rendering the dwelling uninhabitable.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- 21. -->
	<xsl:template name="RJ_REJECT21">
		<xsl:choose>
			<xsl:when test="REJECT21 ='Y'">
				<TR>
					<td class="midcolora">Gutters, down spouts, chimneys or retaining walls that are so poorly maintained that they could collapse and cause personal injury to passers by or property damage to adjacent property.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--22. Any home build prior to 1950 and an applicant for new business with an insurance score below 600. -->
	<xsl:template name="IN_YEAR_INSURANCESCORE">
		<xsl:choose>
			<xsl:when test="IN_YEAR_INSURANCESCORE='Y'">
				<TR>
					<td class="midcolora">Dwelling was built prior to 1950 and the applicant has an Insurance Score less than 600.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--23-->
	<xsl:template name="ELIGIBLE_YEAR">
		<xsl:choose>
			<xsl:when test="ELIGIBLE_YEAR='Y'">
				<TR>
					<td class="midcolora">Year Built is showing prior to 1970 and not Eligible for the Premier Program.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--24-->
	<xsl:template name="ROOF_TYPE_DP3_REPAIR">
		<xsl:choose>
			<xsl:when test="ROOF_TYPE_DP3_REPAIR='Y'">
				<TR>
					<td class="midcolora">Roof Type is 'Flat Buildup'.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--25-->
	<xsl:template name="PRIMARY_HEATING_TYPE">
		<xsl:choose>
			<xsl:when test="PRIMARY_HEATING_TYPE='Y'">
				<TR>
					<td class="midcolora">Primary Heating Type(Coal Professionally/Non Professionally Installed ,Wood Stove - Professionally/Non Professionally Installed , Pellet/Corn Burner, Stove Fireplace Insert, Add on Furnace)</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--26-->
	<xsl:template name="SECONDARY_HEATING_TYPE">
		<xsl:choose>
			<xsl:when test="SECONDARY_HEATING_TYPE='Y'">
				<TR>
					<td class="midcolora">Secondary Heating Type(Coal Professionally/Non Professionally Installed ,Wood Stove - Professionally/Non Professionally Installed , Pellet/Corn Burner, Stove Fireplace Insert, Add on Furnace).</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--27-->
	<xsl:template name="CIRCUIT_BREAKERS">
		<xsl:choose>
			<xsl:when test="CIRCUIT_BREAKERS='Y'">
				<TR>
					<td class="midcolora">No Circuit Breakers.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--28-->
	<xsl:template name="PROPERTY_USED_WHOLE_PART">
		<xsl:choose>
			<xsl:when test="PROPERTY_USED_WHOLE_PART='Y'">
				<TR>
					<td class="midcolora">Property Used in whole or in part for farming, commercial, industrial, professional or business purposes except if use is incidental.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--29-->
	<xsl:template name="DWELLING_MOBILE_HOME">
		<xsl:choose>
			<xsl:when test="DWELLING_MOBILE_HOME='Y'">
				<TR>
					<td class="midcolora">Dwelling a mobile home or double wide.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--30-->
	<xsl:template name="MODULAR_MANU_HOME">
		<xsl:choose>
			<xsl:when test="MODULAR_MANU_HOME='Y'">
				<TR>
					<td class="midcolora">Modular/Manufactured home not built on a continuous foundation.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--31-->
	<xsl:template name="COVA_MODULAR_MANU_HOME">
		<xsl:choose>
			<xsl:when test="COVA_MODULAR_MANU_HOME='Y'">
				<TR>
					<td class="midcolora">Modular/Manufactured home built on a continuous foundation but Coverage A is less than $75,000.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--31-->
	<xsl:template name="MAX_COV_DP2DP3_REPAIR">
		<xsl:choose>
			<xsl:when test="MAX_COV_DP2DP3_REPAIR='Y'">
				<TR>
					<td class="midcolora">Policy type DP-2 Repair or DP-3 Repair,Coverage A Building is not equal to or greater than Market Value.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--32-->
	<xsl:template name="DWELL_UNDER_CONSTRUCTION">
		<xsl:choose>
			<xsl:when test="DWELL_UNDER_CONSTRUCTION='Y'">
				<TR>
					<td class="midcolora">Policy type DP-3 Premier: Building Under Construction - Builders Risk.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--34-->
	<xsl:template name="REPLACEMENT_COST_PREMIER">
		<xsl:choose>
			<xsl:when test="REPLACEMENT_COST_PREMIER='Y'">
				<TR>
					<td class="midcolora">Market Value is less than 80% of the Replacement Cost.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--35 Added by manoj -->
	<xsl:template name="ROOF_TYPE_DP3_REPLACEMENT">
		<xsl:choose>
			<xsl:when test="ROOF_TYPE_DP3_REPLACEMENT='Y'">
				<TR>
					<td class="midcolora">Roof type is flat buildup.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
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
	<!--BILL MORTAGAGEE-->
	<xsl:template name="BILLMORTAGAGEE">
		<xsl:if test="BILLMORTAGAGEE='Y'">
			<TR>
				<td class="midcolora">Select At least one 'Yes' to Bill this mortgagee.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--========================================= End Rejected Templates =================================== -->
	<!--=====================================Templates for showing refered rules messages=================== -->
	<!-- 1. Property used in whole or in part for farming, commercial, industrial, professional or business purposes (with exceptions). -->
	<xsl:template name="ANY_FARMING_BUSINESS_COND">
		<xsl:choose>
			<xsl:when test="ANY_FARMING_BUSINESS_COND ='Y'">
				<TR>
					<td class="midcolora">Business is conducted on Premises(Including day or child care)</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- 2 Rental dwelling(s) whose owner(s) is a corporation. -->
	<xsl:template name="IS_DWELLING_OWNED_BY_OTHER">
		<xsl:if test="IS_DWELLING_OWNED_BY_OTHER='Y'">
			<TR>
				<td class="midcolora">Dwelling owned by anyone other than an individual.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- 3 -->
	<xsl:template name="RF_REFERRED3">
		<xsl:if test="REFERRED3='Y'">
			<TR>
				<td class="midcolora">Dwelling(s) exposed to an adjacent physical hazard.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- 4 -->
	<xsl:template name="RF_REFERRED4">
		<xsl:if test="REFERRED4='Y'">
			<TR>
				<td class="midcolora">Modular or manufactured homes valued at or above $75,000 with certain exceptions.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--5. A person who refuses to purchase for a replacement cost policy, at least 100% of the full replacement value (DP-2, DP-3) where the market value is not less than 80% of the replacement value. -->
	<!-- 
	<xsl:template name="RF_COVERAGE_DECLINED">
		<xsl:if test="COV_DECLINED='Y'">
			<TR>
				<td class="midcolora">A person who refuses to purchase for a replacement cost policy, at least 100% of the full replacement value (DP-2, DP-3) where the market value is not less than 80% of the replacement value.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	-->
	<!--6.A person who refuses to purchase for a repair cost policy using DP-289 on DP-2 or DP-3, insured to at least 100% of market value. -->
	<xsl:template name="RF_REFERRED6">
		<xsl:if test="REFERRED6='Y'">
			<TR>
				<td class="midcolora">A person who refuses to purchase for a repair cost policy using DP-289 on DP-2 or DP-3, insured to at least 100% of market value.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--7. A person whose dwelling insurance has been canceled because of nonpayment of premium with the preceding 2 years, unless paid in full before the issuance or renewal of the policy. -->
	<xsl:template name="ANY_COV_DECLINED_CANCELED">
		<xsl:if test="ANY_COV_DECLINED_CANCELED='Y'">
			<TR>
				<td class="midcolora">Dwelling(s) having coverage declined, cancelled or non-renewed during the last 3 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--8. -->
	<xsl:template name="RF_REFERRED8">
		<xsl:if test="REFERRED8='Y'">
			<TR>
				<td class="midcolora">2 paid claims totaling $1,000 or more.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- 9 Possession or ownership of a vicious dog or other pet. -->
	<xsl:template name="ANIMALS_EXO_PETS_HISTORY">
		<xsl:if test="ANIMALS_EXO_PETS_HISTORY='Y'">
			<TR>
				<td class="midcolora">Possession or ownership of a vicious dog or other pet.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--10. -->
	<xsl:template name="RF_IS_RENTED_IN_PART">
		<xsl:if test="IS_RENTED_IN_PART='Y'">
			<TR>
				<td class="midcolora">Dwellings rented to unmarried college students.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--11. -->
	<xsl:template name="RF_REFERRED11">
		<xsl:if test="REFERRED11='Y'">
			<TR>
				<td class="midcolora">A person who refuses to purchase for a replacement cost policy, at least 80 to 100% of the full replacement value (DP-2, DP-3) where the market value is not less than 80% of the replacement value.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--12. -->
	<xsl:template name="RF_REFERRED12">
		<xsl:if test="REFERRED12='Y'">
			<TR>
				<td class="midcolora">Any dwelling being non-renewed by another carrier.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--13. -->
	<xsl:template name="CHARGE_OFF_PRMIUM">
		<xsl:if test="CHARGE_OFF_PRMIUM='Y'">
			<TR>
				<td class="midcolora">Is a Rollover Policy.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--24 Dec 2007-->
	<xsl:template name="PICH_OF_LOC">
		<xsl:if test="PICH_OF_LOC='Y'">
			<TR>
				<td class="midcolora">Picture of location is not attached.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="PROPERTY_INSP_CREDIT">
		<xsl:if test="PROPERTY_INSP_CREDIT='Y'">
			<TR>
				<td class="midcolora">Property Inspection/Cost Estimator is not attached.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="PRIOR_LOSS_INFO_EXISTS">
		<xsl:choose>
			<xsl:when test="PRIOR_LOSS_INFO_EXISTS ='Y'">
				<TR>
					<td class="midcolora">Prior Losses occurred but information is not provided in Prior Loss or vice versa.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS">
		<xsl:choose>
			<xsl:when test="MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS ='Y'">
				<TR>
					<td class="midcolora">Selected (entered) Policy in Multipolicy Discount Description is (are) not eligible for discount.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Billing Information-->
	<xsl:template name="DFI_ACC_NO_RULE">
		<xsl:if test="DFI_ACC_NO_RULE='Y'">
			<TR>
				<td class="midcolora">Complete DFI Account Number or Transit/Routing Number in Billing Info.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--Credit Card -->
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
	<!--4 july 2006-->
	<xsl:template name="HO_CLAIMS">
		<xsl:if test="HO_CLAIMS='Y'">
			<TR>
				<td class="midcolora">For Prior Losses,paid claims in the last 3 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="COVA_REAPAIR_COST">
		<xsl:if test="COVA_REAPAIR_COST='Y'">
			<TR>
				<td class="midcolora">For DP-2 and DP-3 Repair Programs: Coverage A is not equal or greater than Market Value/Repair Cost.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="COVA_BUILDING">
		<xsl:if test="COVA_BUILDING='Y'">
			<TR>
				<td class="midcolora">For DP-2 and DP-3 Repair Programs: Coverage A Building is greater than $150,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="MIN_COVA_BUILDING">
		<xsl:if test="MIN_COVA_BUILDING='Y'">
			<TR>
				<td class="midcolora">For DP2-Repair or DP-2 Replacement: Minimum coverage is $30,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="COVA_DP3_PREMIUM">
		<xsl:if test="COVA_DP3_PREMIUM='Y'">
			<TR>
				<td class="midcolora">For DP-3 Premier Programs: Coverage A Building must be equal or greater than the Replacement cost.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--4 july 2006-->
	<!--5 July 2006-->
	<xsl:template name="MARKET_VALUE_DP3P">
		<xsl:if test="MARKET_VALUE_DP3P='Y'">
			<TR>
				<td class="midcolora">For DP-3 Premier Programs: Replacement Value can not be greater than the Market Value by 20%. </td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="LOCATION_RENTED">
		<xsl:if test="LOCATION_RENTED='Y'">
			<TR>
				<td class="midcolora">Is location rented on a Weekly Basis is selected and the number of weeks is greater than 4 .</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="STATE_LOC_APP">
		<xsl:if test="STATE_LOC_APP='Y'">
			<TR>
				<td class="midcolora">Location State and Application State are different.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="ZIP_LOC_APP">
		<xsl:if test="ZIP_LOC_APP='Y'">
			<TR>
				<td class="midcolora">Location zip does not belong to Application/Policy state.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="COVA_DP3">
		<xsl:if test="COVA_DP3='Y'">
			<TR>
				<td class="midcolora">For Policy type DP3-Repair,DP-3 Replacement or DP-3 Premier, Minimum coverages is $75,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="COVA_DP3P">
		<xsl:if test="COVA_DP3P='Y'">
			<TR>
				<td class="midcolora">For Policy type DP3-Premier, Coverage A is greater than $300,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="COVA_DP2DP3">
		<xsl:if test="COVA_DP2DP3='Y'">
			<TR>
				<td class="midcolora">Policy Type is DP-2 Replacement or DP-3 Replacement ,Coverage A - Building is greater than $250,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="UPDATE_YEAR_DP3REPAIR">
		<xsl:if test="UPDATE_YEAR_DP3REPAIR='Y'">
			<TR>
				<td class="midcolora">For DP-3 Repair,Any of the following years are greater than 10 : Wiring Update Year,Plumbing Update Year,Electrical Update Year,Roof Update Year.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="ROOF_UPDATE_RENOVATION">
		<xsl:if test="ROOF_UPDATE_RENOVATION='Y'">
			<TR>
				<td class="midcolora">For DP-3 Repair,Year Built is prior to 1940 and Any of the following years are Not Updated : Wiring Update Year,Plumbing Update Year,Electrical Update Year,Roof Update Year.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="UNDER_CONSTRUCTION_POLICYTERM">
		<xsl:if test="UNDER_CONSTRUCTION_POLICYTERM='Y'">
			<TR>
				<td class="midcolora">Building Under Construction (Builder Risk) is selected but No Construction supervised by licensed building contractor.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="ELIGIBLE_YEAR_DP3">
		<xsl:if test="ELIGIBLE_YEAR_DP3='Y'">
			<TR>
				<td class="midcolora">Year Built is prior to 1940.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--5 July 2006-->
	<!--7 July 2006-->
	<xsl:template name="COVA_DP3_REPLACEMENT">
		<xsl:if test="COVA_DP3_REPLACEMENT='Y'">
			<TR>
				<td class="midcolora">For DP-3 Replacement: Coverage A Building must be equal or greater than the Replacement cost.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="MARKET_VALUE_DP3_REPLACEMENT">
		<xsl:if test="MARKET_VALUE_DP3_REPLACEMENT='Y'">
			<TR>
				<td class="midcolora">For DP-3 Replacement Programs: Replacement Value can not be greater than the Market Value by 20%.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Coverage at Renewal-->
	<!--10th Apr 2007-->
	<xsl:template name="COPY_COVERAGE_AT_RENEWAL">
		<xsl:if test="COPY_COVERAGE_AT_RENEWAL='Y'">
			<TR>
				<td class="midcolora">Ineligible Coverages/Limits/Deductibles/Endorsements are not copied to Renewed.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--GrandFathered Coverages (Rjected and Refered)-->
	<xsl:template name="COV_DESCRIPTION">
		<xsl:for-each select="COV">
			<xsl:if test="COV_DES!='' ">
				<TR>
					<td class="midcolora">
					   Coverage selected for '<xsl:value-of select="COV_DES" />' is ineligible.
					</td>
				</TR>
			</xsl:if>
		</xsl:for-each>
	</xsl:template>
	<!--GrandFathered Coverages (Rjected and Refered)-->
	<!--MAR 07 Grandfather Limit Carry passengers others Rejected case-->
	<xsl:template name="LIMIT_DESCRIPTION">
		<xsl:for-each select="LIMIT">
			<xsl:if test="LIMIT_DES!='' ">
				<TR>
					<td class="midcolora">
					    Limit selected for '<xsl:value-of select="LIMIT_DES" />' is ineligible.
					</td>
				</TR>
			</xsl:if>
		</xsl:for-each>
	</xsl:template>
	<!--MAR 07 Grandfather Deductible Carry passengers others Rejected case-->
	<xsl:template name="DEDUCT_DESCRIPTION">
		<xsl:for-each select="DEDUCT">
			<xsl:if test="DEDUCT_DES!='' ">
				<TR>
					<td class="midcolora">
					   Additiional Deductible selected for '<xsl:value-of select="DEDUCT_DES" />' is ineligible.
					</td>
				</TR>
			</xsl:if>
		</xsl:for-each>
	</xsl:template>
	<!--MAR 07 Grandfather Adddeductible Carry passengers others Rejected case-->
	<xsl:template name="ADDDEDUCT_DESCRIPTION">
		<xsl:for-each select="ADDDEDUCT">
			<xsl:if test="ADDDEDUCT_DES!='' ">
				<TR>
					<td class="midcolora">
					    Deductible selected for '<xsl:value-of select="ADDDEDUCT_DES" />' is ineligible.
					</td>
				</TR>
			</xsl:if>
		</xsl:for-each>
	</xsl:template>
	<!--============================End Reffered Templates ================================================= -->
	<xsl:template name="DWELLINGTOP">
		<tr>
			<td colspan="4">
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
	</xsl:template>
</xsl:stylesheet>