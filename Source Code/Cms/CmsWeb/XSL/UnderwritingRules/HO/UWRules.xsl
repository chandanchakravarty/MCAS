<!--******************************************************************************************-->
<!--	Check  for the Homeowners Rules-->
<!--	Name : Ashwani  -->
<!--	Date : 29 Aug.2005  -->
<!--******************************************************************************************-->
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
		/*DwellingTopCount added by Charles on 26-Oct-09 for Itrack 6634 */	
		int intDwellingTopCount=1;
		public int DwellingTopCount(int dwellingtopint)
		{
			intDwellingTopCount=dwellingtopint*intDwellingTopCount;			
			return intDwellingTopCount;
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
				<!--/xsl:if-->
			</table>
			<!-- =========================== Checking for the rejected & referred rules =============================-->
			<!--<xsl:call-template name="HO_REJECTION_CASES" />
			<xsl:call-template name="HO_REFERRED_CASES" />-->
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
								<xsl:apply-templates select="INPUTXML/APPLICATIONS/APPLICATION" />
							</td>
						</tr>
						<!-- Added by Charles on 18-Dec-09 for Itrack 6818 -->
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/LOCATIONS/LOCATION" />
							</td>
						</tr>
						<!-- Added till here -->
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/DWELLINGS/DWELLINGINFO" />
							</td>
						</tr>
						<!-- Added a field at 7th Dec -->
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/RV_VEHICLES/RV_VEHICLE" />
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/HOGENINFOS/HOGENINFO" />
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM" />
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/FUELINFOS/FUELINFO" />
							</td>
						</tr>
						<!-- Watercraft section -->
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/BOATS/BOAT" />
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/OPERATORS/OPERATOR" />
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO" />
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/TRAILERS/TRAILER"></xsl:apply-templates>
							</td>
						</tr>
					</xsl:when>
					<xsl:when test="user:CheckRefer(1) = 0">
						<xsl:text>fsafsafsafsa</xsl:text>
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
								<xsl:apply-templates select="INPUTXML/APPLICATIONS/APPLICATION" />
							</td>
						</tr>
						<!--<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/LOCATIONS/LOCATION" />
							</td>
						</tr>-->
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/DWELLINGS/DWELLINGINFO" />
							</td>
						</tr>
						<!-- Added by Charles on 27-Jul-09 for Itrack 6176 -->
						<!--<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/RV_VEHICLES/RV_VEHICLE" />
							</td>
						</tr>-->
						<!-- Added till here -->
						<!-- Removed by Charles on 7-Oct-09 for Itrack 6370 
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/DWELLINGS/DWELLINGINFO/ADDINTEREST" />
							</td>
						</tr>-->						
						<!--<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/HOGENINFOS/HOGENINFO" />
							</td>
						</tr>-->
						<!--<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM" />
							</td>
						</tr>-->
						<!-- Watercraft section -->
						<!--<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/BOATS/BOAT" />
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/OPERATORS/OPERATOR" />
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO" />
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/TRAILERS/TRAILER"></xsl:apply-templates>
							</td>
						</tr>-->
						<!--<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/FUELINFOS/FUELINFO" />
							</td>
						</tr>-->
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
	<xsl:template name="HO_REJECTION_CASES">
		<!-- 1. -->
		<xsl:call-template name="RJ_IS_NO_OF_FAMILIES" />
		<!--  2. (HO-5)   -->
		<xsl:call-template name="RJ_IS_HO5_REP_COST_COVA" />
		<xsl:call-template name="RJ_IS_HO5_REP_COST_COVA_NEW" /> <!-- Added by Charles on 18-Dec-09 for Itrack 6681 -->
		<!--  3 -->
		<!-- <xsl:call-template name="IS_DESC_RENTERS" /> --><!-- Commented by Charles on 22-Sep-09 for Itrack 6440 -->
		<!--  4 -->
		<!-- <xsl:call-template name ="RJ_IS_REJECT4"/> -->
		<!--  5 -->
		<!-- <xsl:call-template name ="RJ_IS_REJECT5"/> -->
		<!--  6 -->
		<xsl:call-template name="RJ_IS_ANY_FARMING_BUSINESS_COND" />
		<!--  7 -->
		<!-- <xsl:call-template name ="RJ_IS_REJECT7"/> -->
		<!--  8 -->
		<!-- <xsl:call-template name ="RJ_IS_REJECT8"/> -->
		<!--  9 -->
		<!-- <xsl:call-template name ="RJ_IS_REJECT9"/> -->
		<!--  10 -->
		<!-- <xsl:call-template name="RJ_IS_VACENT_OCCUPY" /> --> <!-- Commented by Charles on 22-Sep-09 for Itrack 6440 -->
		<!--  11 -->
		<!-- <xsl:call-template name ="RJ_IS_REJECT11"/> -->
		<!--  12 -->
		<xsl:call-template name="RJ_IS_BUILDING_TYPE" />
		<!--  13 -->
		<!-- <xsl:call-template name ="RJ_IS_REJECT13"/> -->
		<!--  14 -->
		<!-- <xsl:call-template name ="RJ_IS_REJECT14"/> -->
		<!--  15 -->
		<!--<xsl:call-template name ="RJ_IS_REJECT15"/>	 -->
		<!--  16 -->
		<xsl:call-template name="RJ_IS_NO_OF_AMPS" />
		<!--  17 -->
		<xsl:call-template name="IS_SD_POINTS" />
		<xsl:call-template name="IS_RENEW_SD_POINTS" />
		<!-- 18 -->
		<xsl:call-template name="RJ_IS_ROOF_TYPE" />
		<!-- 19 -->
		<!-- Bill Mortgagee-->
		<xsl:call-template name="IS_RJ_HOME_BILLMORTAGAGEE" />
		<xsl:call-template name="RJ_RV_HORSE_POWER" />
		<xsl:call-template name="RJ_RV_TYPE" />
		<xsl:call-template name="RJ_RV_USED_IN_RACE_SPEED" />
		<xsl:call-template name="RJ_RV_HORSE_POWER_OVER_800" />
		<!--Trailer Rule-->
		<xsl:call-template name="IS_RJ_JETSKI_TYPE_TRAILERINFO" />
		<!--Customer Inactive 6th Apr 2007-->
		<xsl:call-template name="IS_RJ_INACTIVE_APPLICATION" />
		<!--Agency Inactive 28th May 2007-->
		<xsl:call-template name="IS_RJ_INACTIVE_AGENCY" />
		<!--26 SEP 2007 -->
		<xsl:call-template name="IS_RJ_BOAT_WITH_HOMEOWNER_POLICY" />
		<!--GrandFather Cov 27 July 2006>
		<xsl:call-template name="IS_RJ_GRANDFATHER_COV" />
		<GrandFather Limit 8 th Mar 2007>
		<xsl:call-template name="IS_RJ_GRANDFATHER_LIMIT" />
		<GrandFather Deduct 8 th Mar 2007>
		<xsl:call-template name="IS_RJ_GRANDFATHER_DEDUCT" />
		<GrandFather Additional Deductible 8 th Mar 2007>
		<xsl:call-template name="IS_RJ_GRANDFATHER_ADDDEDUCT" /-->
		<!--GrandFather Cov 27 July 2006-->
		<xsl:call-template name="IS_RJ_RV_COVERAGE"/>
		<!--===================================== For Both Michigan /Indiana State ==================================== -->
		<!--xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/STATE_ID=22 "-->
		<!--  18 -->
		<!-- <xsl:call-template name="RJ_IS_SWIMPOLL_HOTTUB" /> --> <!-- Moved to referred case by Charles on 22-Sep-09 for Itrack 6440 -->
		<!--/xsl:if-->
		<!--===================================== for Indiana only ==================================== -->
		<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/STATE_ID=14 ">
			<!--  19 -->
			<!-- <xsl:call-template name ="RJ_IS_REJECT19"/> -->
			<!--  20 -->
			<!--<xsl:call-template name="RJ_IS_REJECT20" /> -->
			<!--  21 -->
			<!-- <xsl:call-template name="RJ_IS_REJECT21" /> --></xsl:if>
		<!--  22. -->
		<!-- <xsl:call-template name="IS_RF_PRIMARY_HEAT_TYPE" /> --> <!-- Moved to referred case by Charles on 22-Sep-09 for Itrack 6440 -->
		<xsl:call-template name="IS_RJ_IN_REJ_MIXED_BREED" />
		<!-- 12 June 2006-->
		<!-- <xsl:call-template name="IS_FARMING_FIELD" /> --><!-- Moved to referred case by Charles on 22-Sep-09 for Itrack 6440 -->
		<!-- <xsl:call-template name="IS_RJ_ANY_COV_DECLINED_CANCELED" /> --> <!-- Moved to referred case by Charles on 22-Sep-09 for Itrack 6440 -->
		<!--xsl:call-template name="RJ_IS_SUPERVISED" /-->
		<xsl:call-template name="RJ_OCCUPANCY" />
		<!--12 june 2006-->
		<!--13 june 2006-->
		<!--xsl:call-template name="IS_PRIOR_YEAR_BUILT" /-->
		<!-- <xsl:call-template name="IS_SECONDARY_TYPE" /> --> <!-- Moved to referred case by Charles on 22-Sep-09 for Itrack 6440 -->
		<!--13 june 2006-->
		<!--15 June 2006-->
		<!--<xsl:call-template name="IS_RJ_CONVICTION_DEGREE_IN_PAST" />--> <!-- Moved to referred case by Charles on 22-Sep-09 for Itrack 6440 -->
		<!-- <xsl:call-template name="IS_RJ_MAIN_HEATING_USE" /> --> <!-- Moved to referred case by Charles on 22-Sep-09 for Itrack 6440 -->
		<xsl:call-template name="IS_RJ_LABORATORY_LABEL" />
		<xsl:call-template name="IS_RJ_PROF_INSTALL_DONE" />
		<xsl:call-template name="IS_RJ_STOVE_INSTALLATION" />
		<!--15 June 2006-->
		<!--19 June 2006-->
		<!-- <xsl:call-template name="IS_RJ_BUILT_ON_CONTINUOUS_FOUNDATION" /> --> <!-- Moved to referred case by Charles on 22-Sep-09 for Itrack 6440 -->
		<!-- <xsl:call-template name="IS_RJ_BUILT_ON_CONTINUOUS_FOUNDATION_COV" /> --> <!-- Moved to referred case by Charles on 22-Sep-09 for Itrack 6440 -->
		<!--<xsl:call-template name="IS_RJ_REPLACEMENT_COST_HO3" />-->
		<!--<xsl:call-template name="IS_RJ_SUPP_HEATING_SOURCE"/>-->
		<xsl:call-template name="IS_RJ_REPLACEMENT_COST_HO3_HO5_PREMIER" />
		<!--19 June 2006-->
		<xsl:call-template name="IS_RJ_REPLACEMENT_COST_HO5_PREMIER" /><!-- Added by Charles on 30-Nov-09 for Itrack 6681 -->
		<xsl:call-template name="IS_RJ_REPLACEMENT_COST_HO5_PREMIER_NEW" /><!-- Added by Charles on 18-Dec-09 for Itrack 6681 -->		
		<!--20 June 2006-->
		<!--<xsl:call-template name="IS_RJ_OTHERS_LOCATION" />-->
		<!--20 June 2006-->
		<!-- ===========================Rejected templates for Watercraft section (start)================-->
		<!--  1  -->
		<xsl:call-template name="IS_RJ_MAX_SPEED" />
		<!--  2  -->
		<xsl:call-template name="IS_RENTED_OTHERS" />
		<!--  3  -->
		<xsl:call-template name="IS_RJ_PRINCIPLE_OPERATOR" />
		<!--  3 Moved to Refered Case-->
		<!--<xsl:call-template name="IS_REGISTERED_OTHERS" />-->
		<!--  4  -->
		<!--MAY 06 Remove BOAT-->
		<!--<xsl:call-template name="IS_CONVICTED_ACCIDENT" />-->
		<!-- 5 -->
		<!--MAY 06 Remove BOAT-->
		<!--<xsl:call-template name="IS_RJ_STATE_REGD" />-->
		<!-- 6 -->
		<xsl:call-template name="IS_RJ_RACING" />
		<!--7-->
		<!--May 5 06-->
		<xsl:call-template name="IS_BOAT_AMPHIBIOUS" />
		<!--MAY 06 carry passengers-->
		<!--  8  -->
		<xsl:call-template name="IS_CARRY_PASSENGER_FOR_CHARGE" />
		<xsl:call-template name="IS_ANY_BOAT_RESIDENCE" />
		<!--Trailer Rule-->
		<xsl:call-template name="IS_RJ_JETSKI_TYPE_TRAILER" />
		<xsl:call-template name="IS_RJ_LOCATION_STATE_APP_STATE" /> <!-- Added by Charles on 18-Dec-09 for Itrack 6818 -->
		<!--MAY 06 carry passengers-->
		<!--<xsl:call-template name ="IS_RJ_HOUSE_BOAT"/>  -->
		<!--  6  -->
		<!-- <xsl:call-template name ="IS_RJ_REJECT6"/> -->
		<!--  7  -->
		<!-- <xsl:call-template name ="IS_RJ_REJECT7"/>-->
		<!--  8  -->
		<!-- <xsl:call-template name ="IS_RJ_REJECT8"/> -->
		<!--  9  -->
		<!-- <xsl:call-template name ="IS_RJ_DEGREE_CONVICTION"/> -->
		<!-- ===========================Rejected templates for Watercraft section (end)================-->
	</xsl:template>
	<!-- ========================================== Rejected templates ================================== -->
	<!-- 1. -->
	<xsl:template name="RJ_IS_NO_OF_FAMILIES">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/NO_OF_FAMILIES ='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(0) = 0"></xsl:if><!-- Added by Charles on 26-Oct-09 for Itrack 6634 -->
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(1) = 0"></xsl:if><!-- Added by Charles on 26-Oct-09 for Itrack 6634 -->
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 2. -->
	<xsl:template name="RJ_IS_HO5_REP_COST_COVA">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/HO5_REP_COST_COVA='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(0) = 0"></xsl:if><!-- Added by Charles on 26-Oct-09 for Itrack 6634 -->
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(1) = 0"></xsl:if><!-- Added by Charles on 26-Oct-09 for Itrack 6634 -->
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="RJ_IS_HO5_REP_COST_COVA_NEW">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/HO5_REP_COST_COVA_NEW='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(0) = 0"></xsl:if><!-- Added by Charles on 26-Oct-09 for Itrack 6634 -->
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(1) = 0"></xsl:if><!-- Added by Charles on 26-Oct-09 for Itrack 6634 -->
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 3. -->
	<!--<xsl:template name="IS_DESC_RENTERS">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/DESC_RENTERS='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>--><!-- Moved to Referred case by Charles on 22-Sep-09 for Itrack 6440 -->
	<!--12 june 2006-->
	<!--xsl:template name="RJ_IS_SUPERVISED">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/IS_SUPERVISED ='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template-->
	<!--12 june 2006-->
	<xsl:template name="RJ_OCCUPANCY">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/OCCUPANCY ='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(0) = 0"></xsl:if><!-- Added by Charles on 26-Oct-09 for Itrack 6634 -->
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(1) = 0"></xsl:if><!-- Added by Charles on 26-Oct-09 for Itrack 6634 -->
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 4. -->
	<!-- <xsl:template name="RJ_IS_REJECT4">
	<xsl:choose>
    	<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/REJECT4='Y'">
    		<xsl:if test= "user:IsApplicationAcceptable(0) = 0"></xsl:if>
    	</xsl:when>
    	<xsl:otherwise>
    		<xsl:if test= "user:IsApplicationAcceptable(1) = 0"></xsl:if>
    	</xsl:otherwise>
	</xsl:choose>
</xsl:template> -->
	<!-- 5. -->
	<!-- <xsl:template name="RJ_IS_REJECT5">
	<xsl:choose>
    	<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/REJECT5='Y'">
    		<xsl:if test= "user:IsApplicationAcceptable(0) = 0"></xsl:if>
    	</xsl:when>
    	<xsl:otherwise>
    		<xsl:if test= "user:IsApplicationAcceptable(1) = 0"></xsl:if>
    	</xsl:otherwise>
	</xsl:choose>
</xsl:template> -->
	<!-- 6. -->
	<xsl:template name="RJ_IS_ANY_FARMING_BUSINESS_COND">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/ANY_FARMING_BUSINESS_COND='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 7. -->
	<!-- <xsl:template name="RJ_IS_REJECT7">
	<xsl:choose>
    	<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/REJECT7='Y'">
    		<xsl:if test= "user:IsApplicationAcceptable(0) = 0"></xsl:if>
    	</xsl:when>
    	<xsl:otherwise>
    		<xsl:if test= "user:IsApplicationAcceptable(1) = 0"></xsl:if>
    	</xsl:otherwise>
	</xsl:choose>
</xsl:template> -->
	<!-- 8. -->
	<!-- <xsl:template name="RJ_IS_REJECT8">
	<xsl:choose>
    	<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/REJECT8='Y'">
    		<xsl:if test= "user:IsApplicationAcceptable(0) = 0"></xsl:if>
    	</xsl:when>
    	<xsl:otherwise>
    		<xsl:if test= "user:IsApplicationAcceptable(1) = 0"></xsl:if>
    	</xsl:otherwise>
	</xsl:choose>
</xsl:template> -->
	<!-- 9. -->
	<!-- <xsl:template name="RJ_IS_REJECT9">
	<xsl:choose>
    	<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/REJECT9='Y'">
    		<xsl:if test= "user:IsApplicationAcceptable(0) = 0"></xsl:if>
    	</xsl:when>
    	<xsl:otherwise>
    		<xsl:if test= "user:IsApplicationAcceptable(1) = 0"></xsl:if>
    	</xsl:otherwise>
	</xsl:choose>
</xsl:template> -->
	<!-- 10. -->
	<!-- <xsl:template name="RJ_IS_VACENT_OCCUPY">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/IS_VACENT_OCCUPY='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template> -->
	<!-- 11. -->
	<!-- <xsl:template name="RJ_IS_REJECT11">
	<xsl:choose>
    	<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/REJECT11='Y'">
    		<xsl:if test= "user:IsApplicationAcceptable(0) = 0"></xsl:if>
    	</xsl:when>
    	<xsl:otherwise>
    		<xsl:if test= "user:IsApplicationAcceptable(1) = 0"></xsl:if>
    	</xsl:otherwise>
	</xsl:choose>
</xsl:template> -->
	<!-- 12. -->
	<xsl:template name="RJ_IS_BUILDING_TYPE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/BUILDING_TYPE='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(0) = 0"></xsl:if><!-- Added by Charles on 26-Oct-09 for Itrack 6634 -->
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(1) = 0"></xsl:if><!-- Added by Charles on 26-Oct-09 for Itrack 6634 -->
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 13. -->
	<!-- <xsl:template name="RJ_IS_REJECT13">
	<xsl:choose>
    	<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/REJECT13='Y'">
    		<xsl:if test= "user:IsApplicationAcceptable(0) = 0"></xsl:if>
    	</xsl:when>
    	<xsl:otherwise>
    		<xsl:if test= "user:IsApplicationAcceptable(1) = 0"></xsl:if>
    	</xsl:otherwise>
	</xsl:choose>
</xsl:template> -->
	<!-- 14. -->
	<!-- <xsl:template name="RJ_IS_REJECT13">
	<xsl:choose>
    	<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/REJECT13='Y'">
    		<xsl:if test= "user:IsApplicationAcceptable(0) = 0"></xsl:if>
    	</xsl:when>
    	<xsl:otherwise>
    		<xsl:if test= "user:IsApplicationAcceptable(1) = 0"></xsl:if>
    	</xsl:otherwise>
	</xsl:choose>
</xsl:template> -->
	<!-- 15. -->
	<!-- <xsl:template name="RJ_IS_REJECT15">
	<xsl:choose>
    	<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/REJECT15='Y'">
    		<xsl:if test= "user:IsApplicationAcceptable(0) = 0"></xsl:if>
    	</xsl:when>
    	<xsl:otherwise>
    		<xsl:if test= "user:IsApplicationAcceptable(1) = 0"></xsl:if>
    	</xsl:otherwise>
	</xsl:choose>
</xsl:template> -->
	<!-- 16. -->
	<xsl:template name="RJ_IS_NO_OF_AMPS">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/NO_OF_AMPS='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(0) = 0"></xsl:if><!-- Added by Charles on 26-Oct-09 for Itrack 6634 -->
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(1) = 0"></xsl:if><!-- Added by Charles on 26-Oct-09 for Itrack 6634 -->
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="RJ_IS_ROOF_TYPE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/ROOF_TYPE='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(0) = 0"></xsl:if><!-- Added by Charles on 26-Oct-09 for Itrack 6634 -->
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(1) = 0"></xsl:if><!-- Added by Charles on 26-Oct-09 for Itrack 6634 -->
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- SD POINTS (Minor Violation)-->	
	<xsl:template name="IS_SD_POINTS">
		<xsl:choose>
			<xsl:when test="INPUTXML/OPERATORS/OPERATOR/SD_POINTS='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- SD POINTS at Renewal(Minor Violation)-->	
	<xsl:template name="IS_RENEW_SD_POINTS">
		<xsl:choose>
			<xsl:when test="INPUTXML/OPERATORS/OPERATOR/RENEW_SD_POINTS ='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
					<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<xsl:template name="RJ_RV_HORSE_POWER">
		<xsl:choose>
			<xsl:when test="INPUTXML/RV_VEHICLES/RV_VEHICLE/RV_HORSE_POWER='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="RJ_RV_HORSE_POWER_OVER_800">
		<xsl:choose>
			<xsl:when test="INPUTXML/RV_VEHICLES/RV_VEHICLE/RV_HORSE_POWER_OVER_800='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="RJ_RV_TYPE">
		<xsl:choose>
			<xsl:when test="INPUTXML/RV_VEHICLES/RV_VEHICLE/RV_TYPE='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RJ_RV_COVERAGE">
		<xsl:choose>
			<xsl:when test="INPUTXML/RV_VEHICLES/RV_VEHICLE/RV_COV_SELC='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="RJ_RV_USED_IN_RACE_SPEED">
		<xsl:choose>
			<xsl:when test="INPUTXML/RV_VEHICLES/RV_VEHICLE/RV_USED_IN_RACE_SPEED='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Customer Inactive 6th Apr  2007 -->
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
	<!--Do you want to add a boat with this Homeowner policy 26th Sep  2007 -->
	<xsl:template name="IS_RJ_BOAT_WITH_HOMEOWNER_POLICY">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/BOAT_WITH_HOMEOWNER_POLICY='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Trailer Rule-->
	<xsl:template name="IS_RJ_JETSKI_TYPE_TRAILERINFO">
		<xsl:choose>
			<xsl:when test="INPUTXML/TRAILERS/TRAILER/JETSKI_TYPE_TRAILERINFO ='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<!-- 17.GrandFather Coverage -->
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
	<!-- 17.GrandFather Coverage -->
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
	<!-- 17.GrandFather Coverage -->
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
	<!-- 17.GrandFather Coverage -->
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
	<!-- <xsl:template name="RJ_IS_REJECT17">
	<xsl:choose>
    	<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RJ_IS_REJECT17='Y'">
    		<xsl:if test= "user:IsApplicationAcceptable(0) = 0"></xsl:if>
    	</xsl:when>
    	<xsl:otherwise>
    		<xsl:if test= "user:IsApplicationAcceptable(1) = 0"></xsl:if>
    	</xsl:otherwise>
	</xsl:choose>
</xsl:template> -->
	<!-- 18. -->
	<!-- <xsl:template name="RJ_IS_SWIMPOLL_HOTTUB">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/IS_SWIMPOLL_HOTTUB='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template> --><!-- Moved to referred case by Charles on 22-Sep-09 for Itrack 6440 -->
	
	<!--BILL MORTAGAGEE 25th Oct  2007 -->
	<xsl:template name="IS_RJ_HOME_BILLMORTAGAGEE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/ADDINTEREST/HOME_BILLMORTAGAGEE='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(0) = 0"></xsl:if><!-- Added by Charles on 26-Oct-09 for Itrack 6634 -->
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(1) = 0"></xsl:if><!-- Added by Charles on 26-Oct-09 for Itrack 6634 -->
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ====================================== start for Indiana only  =================================== -->
	<!-- 19. -->
	<!-- <xsl:template name="RJ_IS_REJECT19">
	<xsl:choose>
    	<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RJ_IS_REJECT19='Y'">
    		<xsl:if test= "user:IsApplicationAcceptable(0) = 0"></xsl:if>
    	</xsl:when>
    	<xsl:otherwise>
    		<xsl:if test= "user:IsApplicationAcceptable(1) = 0"></xsl:if>
    	</xsl:otherwise>
	</xsl:choose>
</xsl:template> -->
	<!-- 20. -->
	<!--<xsl:template name="RJ_IS_REJECT20">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/ANIMALS_EXO_PETS_HISTORY='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template> -->
	<!-- 21. -->
	<!--
	<xsl:template name="RJ_IS_REJECT21">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/YEAR_BUILT ='N' or INPUTXML/DWELLINGS/DWELLINGINFO/CUSTOMER_INSURANCE_SCORE='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template> -->
	<!-- 22. -->
	<!-- <xsl:template name="IS_RF_PRIMARY_HEAT_TYPE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/PRIMARY_HEAT_TYPE='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template> --><!-- Moved to referred case by Charles on 22-Sep-09 for Itrack 6440 -->
	<xsl:template name="IS_RJ_IN_REJ_MIXED_BREED">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/IN_REJ_MIXED_BREED='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--12 June 06-->
	<!-- <xsl:template name="IS_FARMING_FIELD">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/FARMING_FIELD='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>--><!-- Moved to referred case by Charles on 22-Sep-09 for Itrack 6440 -->
	<!-- <xsl:template name="IS_RJ_ANY_COV_DECLINED_CANCELED">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/ANY_COV_DECLINED_CANCELED='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template> --><!-- Moved to referred case by Charles on 22-Sep-09 for Itrack 6440 -->
	<!--13 June 2006-->
	<!--xsl:template name="IS_PRIOR_YEAR_BUILT">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/PRIOR_YEAR_BUILT='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template-->
	<!-- <xsl:template name="IS_SECONDARY_TYPE">
		<xsl:choose>
			<xsl:when test="INPUTXML/FUELINFOS/FUELINFO/SECONDARY_HEATING_SOURCE='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template> --> <!-- Moved to Referred Case by Charles on 22-Sep-09for Itrack 6440 -->
	<!--13 June 2006-->
	<!--15 June 2006--><!--
	<xsl:template name="IS_RJ_CONVICTION_DEGREE_IN_PAST">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/CONVICTION_DEGREE_IN_PAST='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template> --><!-- Moved to Referred Case by Charles on 22-Sep-09for Itrack 6440 -->
	<!-- <xsl:template name="IS_RJ_MAIN_HEATING_USE">
		<xsl:choose>
			<xsl:when test="INPUTXML/FUELINFOS/FUELINFO/MAIN_HEATING_USE='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template> -->
	<xsl:template name="IS_RJ_LABORATORY_LABEL">
		<xsl:choose>
			<xsl:when test="INPUTXML/FUELINFOS/FUELINFO/LABORATORY_LABEL='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RJ_PROF_INSTALL_DONE">
		<xsl:choose>
			<xsl:when test="INPUTXML/FUELINFOS/FUELINFO/PROF_INSTALL_DONE='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RJ_STOVE_INSTALLATION">
		<xsl:choose>
			<xsl:when test="INPUTXML/FUELINFOS/FUELINFO/STOVE_INSTALLATION='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--15 June 2006-->
	<!--19 June 2006-->
	<!--<xsl:template name="IS_RJ_BUILT_ON_CONTINUOUS_FOUNDATION">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/BUILT_ON_CONTINUOUS_FOUNDATION='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template> --><!-- Moved to referred case by Charles on 22-Sep-09 for Itrack 6440 -->
	<!-- <xsl:template name="IS_RJ_BUILT_ON_CONTINUOUS_FOUNDATION_COV">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/BUILT_ON_CONTINUOUS_FOUNDATION_COV='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template> --><!-- Moved to referred case by Charles on 22-Sep-09 for Itrack 6440 -->
	<!--<xsl:template name="IS_RJ_REPLACEMENT_COST_HO3">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/REPLACEMENT_COST_HO3='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>-->
	<!--<xsl:template name="IS_RJ_SUPP_HEATING_SOURCE">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/SUPP_HEATING_SOURCE='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>-->
	<xsl:template name="IS_RJ_REPLACEMENT_COST_HO3_HO5_PREMIER">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/REPLACEMENT_COST_HO3_HO5_PREMIER='N'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(0) = 0"></xsl:if><!-- Added by Charles on 26-Oct-09 for Itrack 6634 -->
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(1) = 0"></xsl:if><!-- Added by Charles on 26-Oct-09 for Itrack 6634 -->
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Added by Charles on 30-Nov-09 for Itrack 6681 -->
	<xsl:template name="IS_RJ_REPLACEMENT_COST_HO5_PREMIER">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/REPLACEMENT_COST_HO5_PREMIER='N'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Added till here -->
	<!-- Added by Charles on 18-Dec-09 for Itrack 6681 -->
	<xsl:template name="IS_RJ_REPLACEMENT_COST_HO5_PREMIER_NEW">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/REPLACEMENT_COST_HO5_PREMIER_NEW='N'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
				<xsl:if test="user:DwellingTopCount(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Added till here -->
	<!--20 June 2006-->
	<!--<xsl:template name="IS_RJ_OTHERS_LOCATION">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/ENDORSEMENT/OTHERS_LOCATION='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>-->
	<!--20 June 2006-->
	<!-- ====================================== end for Indiana only  =================================== -->
	<!-- Added by Charles on 8-Dec-09 for Itrack 6818 -->
	<xsl:template name="IS_RJ_LOCATION_STATE_APP_STATE">
		<xsl:choose>
			<xsl:when test="INPUTXML/LOCATIONS/LOCATION/LOCATION_STATE_APP_STATE='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Added till here -->
	<!-- ===================================== Watercraft section (start) =============================== -->
	<!-- 1. -->
	<xsl:template name="IS_RJ_MAX_SPEED">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/MAX_SPEED='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 2. -->
	<xsl:template name="IS_RENTED_OTHERS">
		<xsl:choose>
			<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/IS_RENTED_OTHERS='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<!-- Principal operator. -->
	<xsl:template name="IS_RJ_PRINCIPLE_OPERATOR">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/PRINCIPLE_OPERATOR='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 3.Moved to Refer Case 03 May 2007 -->
	<!--<xsl:template name="IS_REGISTERED_OTHERS">
		<xsl:choose>
			<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/IS_REGISTERED_OTHERS='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>-->
	<!-- 5. -->
	<!--MAY 06 Remove Accident and State-->
	<!--<xsl:template name="IS_CONVICTED_ACCIDENT">
		<xsl:choose>
			<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/IS_CONVICTED_ACCIDENT='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<xsl:template name="IS_RJ_STATE_REGD">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/STATE_REG='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>-->
	<!-- 8 -->
	<xsl:template name="IS_RJ_RACING">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/PARTICIPATE_RACE ='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--May 05 06-->
	<xsl:template name="IS_BOAT_AMPHIBIOUS">
		<xsl:choose>
			<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/ANY_BOAT_AMPHIBIOUS='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--MAY 06 Carry passengers-->
	<xsl:template name="IS_CARRY_PASSENGER_FOR_CHARGE">
		<xsl:choose>
			<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/CARRY_PASSENGER_FOR_CHARGE='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_ANY_BOAT_RESIDENCE">
		<xsl:choose>
			<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/ANY_BOAT_RESIDENCE='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--MAY 06 Carry passengers-->
	<!--Trailer Rule-->
	<xsl:template name="IS_RJ_JETSKI_TYPE_TRAILER">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/JETSKI_TYPE_TRAILER ='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--
<xsl:template name="IS_RJ_REJECT7">
	<xsl:choose>
    	<xsl:when test="INPUTXML/BOATS/BOAT/REJECT7 ='Y'">
    		<xsl:if test= "user:IsApplicationAcceptable(0) = 0"></xsl:if>
    	</xsl:when>
    	<xsl:otherwise>
    		<xsl:if test= "user:IsApplicationAcceptable(1) = 0"></xsl:if>
    	</xsl:otherwise>
	</xsl:choose>
</xsl:template>-->
	<!-- 8. 
<xsl:template name="IS_RJ_REJECT8">
	<xsl:choose>
    	<xsl:when test="INPUTXML/BOATS/BOAT/REJECT8 ='Y'">
    		<xsl:if test= "user:IsApplicationAcceptable(0) = 0"></xsl:if>
    	</xsl:when>
    	<xsl:otherwise>
    		<xsl:if test= "user:IsApplicationAcceptable(1) = 0"></xsl:if>
    	</xsl:otherwise>
	</xsl:choose>
</xsl:template>-->
	<!-- 9. 
<xsl:template name="IS_RJ_DEGREE_CONVICTION">
	<xsl:choose>
    	<xsl:when test="INPUTXML/BOATS/BOAT/DEG_CONVICTION='Y'">
    		<xsl:if test= "user:IsApplicationAcceptable(0) = 0"></xsl:if>
    	</xsl:when>
    	<xsl:otherwise>
    		<xsl:if test= "user:IsApplicationAcceptable(1) = 0"></xsl:if>
    	</xsl:otherwise>
	</xsl:choose>
</xsl:template>-->
	<!-- ===================================== Rejected Watercraft section (end) ==============================-->
	<!-- =========================================== Checking for referred Cases ========================== -->
	<xsl:template name="HO_REFERRED_CASES">
		<!--   1. -->
		<xsl:call-template name="IS_RF_DWELLING_LIMIT" />
		<!-- 2. HO-5  -->
		<xsl:call-template name="IS_RF_HO5_YEAR_UPDATE" />
		<!-- 3.  Ho-6 -->
		<xsl:call-template name="IS_RF_COVAC_HO6" />
		<!-- 4.  -->
		<!-- <xsl:call-template name ="IS_RF_REFERRED4"/> -->
		<!--  5.  HO-2 or HO-3 -->
		<xsl:call-template name="IS_RF_REPLACEMENT_COST_POLICY_HO2_HO3" />
		<!--  6.  HO-5 -->
		<xsl:call-template name="IS_RF_REPLACEMENT_COST_POLICY_HO5" />
		<!--  7.   -->
		<xsl:call-template name="IS_RF_REPAIR_COST_POLICY" />
		<!--  8. Commented 12 June 06   -->
		<xsl:call-template name="IS_RF_ANY_COV_DECLINED_CANCELED" /><!-- Uncommented by Charles on 22-Sep-09 for Itrack 6440 -->
		<!--  9.   -->
		<xsl:call-template name="IS_RF_ANY_HEATING_SOURCE" />
		<!-- 10-->
		<xsl:call-template name="IS_RF_PROVIDE_HOME_DAY_CARE" />
		<xsl:call-template name="IS_RF_PRIOR_LOSS_INFO_EXISTS" />
		<xsl:call-template name="IS_RF_MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS" />
		<!-- Major Violations-->
		<xsl:call-template name="IS_WATER_MAJOR_VIOLATION" />
		<!-- Major Violations At Renewal-->
		<xsl:call-template name="IS_RENEW_WATER_MAJOR_VIOLATION" />
		<!-- For additional premises-->
		<!-- 13th Mar 2007-->
		<xsl:call-template name="IS_RF_ADDITIONAL_PREMISES_COV_DESC" />
		<!--  10.   -->
		<xsl:call-template name="IS_RF_CONVICTION_DEGREE_IN_PAST" /><!-- Uncommented by Charles on 22-Sep-09 for Itrack 6440 -->
		<!--  11.   -->
		<!-- <xsl:call-template name ="IS_RF_REFERRED11">-->
		<!--  12. -->
		<!--<xsl:call-template name ="IS_RF_REFERRED12"/>-->
		<!--  13. -->
		<xsl:call-template name="IS_RF_HYDRANT_DIST" />
		<!--  14. -->
		<!-- <xsl:call-template name ="IS_RF_REFERRED14"/> -->
		<!--  15. -->
		<!-- <xsl:call-template name ="IS_RF_REFERRED15"/>-->
		<!--  16. -->
		<!-- <xsl:call-template name ="IS_RF_REFERRED16"/>-->
		
		<!-- Added by Charles on 22-Sep-09 for Itrack 6440 -->
		<xsl:call-template name="IS_DESC_RENTERS" />
		<xsl:call-template name="IS_FARMING_FIELD" />
		<xsl:call-template name="IS_RF_BUILT_ON_CONTINUOUS_FOUNDATION" />
		<xsl:call-template name="RF_IS_VACENT_OCCUPY" />
		<xsl:call-template name="IS_RF_PRIMARY_HEAT_TYPE" />
		<xsl:call-template name="IS_SECONDARY_TYPE" />
		<xsl:call-template name="IS_RF_MAIN_HEATING_USE" />
		<xsl:call-template name="RF_IS_SWIMPOLL_HOTTUB" />
		<!-- Added till here -->
		
		<!-- Added by Charles on 26-Nov-09 for Itrack 6679 -->
		<xsl:call-template name="IS_RF_BUILT_ON_CONTINUOUS_FOUNDATION_PREMIER" />
						
		<!--13 June 2005-->
		<xsl:call-template name="IS_RF_ROOF_UPDATE" />
		<xsl:call-template name="IS_RF_ROOF_UPDATE_YEAR" />
		
		<!-- Added by Charles on 26-Nov-09 for Itrack 6679 -->
		<xsl:call-template name="IS_RF_ROOF_UPDATE_PREMIER" />
		<xsl:call-template name="IS_RF_ROOF_UPDATE_YEAR_PREMIER" />
		
		<xsl:call-template name="IS_RF_LOCATION_TYPE" />		
		<xsl:call-template name="IS_RF_NO_WEEKS_RENTED" />
		<xsl:call-template name="IS_RF_HO_CLAIMS" />
		<xsl:call-template name="IS_RF_WBSPO_LOSS" /> <!-- Added by Charles on 30-Nov-09 for Itrack 6647 -->
		<!--Refered Case : 1 NOV  2007-->
		<xsl:call-template name="IS_TRAILER_DEDUCTIBLE" />
		<!-- Refer case for PRIOR_YEAR_BUILT  -->
		<!-- 15TH MAR 2007-->
		<xsl:call-template name="IS_RF_PRIOR_YEAR_BUILT" />
		<!-- 3TH May 2007-->
		<xsl:call-template name="IS_RF_NO_HORSES" />
		<!-- 25 Sep 2009-->
		<xsl:call-template name="IS_OTHERS_LOCATION" />		
		<xsl:call-template name="IS_RF_SOLID_FUEL_DEVICE" /> <!--Added by Charles on 27-Nov-09 for Itrack 6681-->
		
		<!-- ========================= For Michigan only ====================================================== -->
		<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/STATE_ID=22">
			<!--  17. -->
			<xsl:call-template name="IS_RF_ANIMALS_EXO_PETS_HISTORY" />
			<xsl:call-template name="IS_RF_PIC_OF_LOC" />
			<xsl:call-template name="IS_RF_PROPRTY_INSP_CREDIT" />
			<xsl:call-template name="IS_RF_HO_CLAIMS" />
		</xsl:if>
		<!--  18. -->
		<xsl:call-template name="IS_RF_IS_UNDER_CONSTRUCTION" />
		<!--  20. -->
		<!--<xsl:call-template name ="IS_RF_REFERRED20"/>-->
		<!--  21. -->
		<xsl:call-template name="IS_RF_SINGLEITEM" />		
		<!--  22. -->
		<xsl:call-template name="IS_RF_AGGREGATE" />
		<!--  23 -->
		<xsl:call-template name="IS_RF_CIRCUIT_BREAKERS" />
		<!-- 12 June 2007-->
		<xsl:call-template name="IS_RF_CENT_BURG_FIRE_ALARM_CERT_ATTACHED" />
		<xsl:call-template name="IS_RF_PROT_DEVC_ALARM_CERT_ATTACHED" /> <!-- Added by Charles on 20-Oct-09 for Itrack 6586-->
		<xsl:call-template name="IS_RF_IS_SUPERVISED" />
		<!-- 24 -->
		<xsl:call-template name="IS_RF_PERSONALCOMPUTER" />
		<xsl:call-template name="IS_RF_JEWELLERYITEM_PICTURE" />
		<xsl:call-template name="IS_RF_JEWELLERYITEM_APPRAISAL" />
		<xsl:call-template name="IS_RF_BREAKAGEITEM" />
		<!-- 23 July 2009 -->
		<xsl:call-template name="IS_RF_SCH_CAMERAS_NON_PROFESNL" />
		<xsl:call-template name="IS_RF_SCH_FURS_HO61" />
		<xsl:call-template name="IS_RF_SCH_GUNS_HO61" />
		
		<!-- Itrack 6455 -->
		<!--<xsl:call-template name="IS_RF_SCH_JEWELRY_HO61" />--> 
		<xsl:call-template name="IS_RF_SCH_JEWELRY_HO61_EXCT_TEN" />
		<xsl:call-template name="IS_RF_SCH_JEWELRY_HO61_EXCT_TEN_PIC" />
		<xsl:call-template name="IS_RF_SCH_JEWELRY_HO61_EXCT_TWO" />
		
		<xsl:call-template name="IS_RF_SCH_MUSICAL_INSTRMNT_HO61" />
		<xsl:call-template name="IS_RF_SCH_SILVER_HO61" />
		<xsl:call-template name="IS_RF_SCH_STAMPS_HO61" />
		<xsl:call-template name="IS_RF_SCH_RARECOINS_HO61" />
		<xsl:call-template name="IS_RF_SCH_FINEARTS_WO_BREAK_HO61" />
		<xsl:call-template name="IS_RF_SCH_FINEARTS_BREAK_HO61" />
		<xsl:call-template name="IS_RF_SCH_FINEARTS_HO61" /> <!-- Added by Charles on 6-Oct-09 for Itrack 6488-->
		
		<!-- ========================================== for Indiana only ================================== -->
		<!--Billing Info -->
		<xsl:call-template name="IS_DFI_ACC_NO_RULE" />
		<!-- Effective Date-->
		<xsl:call-template name="IS_APPEFFECTIVEDATE" />
		<xsl:call-template name="IS_TOTAL_PREMIUM_AT_RENEWAL" />
		<xsl:call-template name="IS_CLAIM_EFFECTIVE" />
		<!--21 Aug 2007 -->
		<xsl:call-template name="IS_CREDIT_CARD" />
		<!--End Billing Info -->
		<xsl:if test="INPUTXML/APPLICATIONS/APPLICATION/STATE_ID=14 ">
			<xsl:call-template name="IS_RF_CHARGE_OFF_PRMIUM" />			
			<xsl:call-template name="IS_RF_PIC_OF_LOC" />
			<xsl:call-template name="IS_RF_PROPRTY_INSP_CREDIT" />
			<xsl:call-template name="IS_MORE_MORTAGAGEE" /> <!-- Added by Charles on 9-Sep-09 for Itrack 6370 -->
		</xsl:if>
		<xsl:call-template name="IS_RF_MI_MIXED_BREED" />
		<xsl:call-template name="IS_RF_IN_MIXED_BREED" />
		<!-- ===================== Watercraft referred start ============================================= -->
		<!--<xsl:call-template name ="IS_RF_REFERRED1"/>-->
		<xsl:call-template name="IS_RF_MAX_LEN" />
		<xsl:call-template name="RF_MAX_LEN_BOAT" />
		<xsl:call-template name="IS_MAX_INSURING_VALUE" />
		<xsl:call-template name="IS_MAX_AGE" />
		<xsl:call-template name="IS_MAX_INSURING_WMOTOR" />
		<xsl:call-template name="IS_RF_COVERAGE_DECLINED" />
		<xsl:call-template name="IS_CHARGE_OFF_PRMIUM" />
		<xsl:call-template name="IS_INSURED_MARKET_VALUE" />
		<!--May 06 ref case 1 GEN INFO-->
		<xsl:call-template name="IS_PHY_MENTL_CHALLENGED" />
		<xsl:call-template name="IS_DRIVER_SUS_REVOKED" />
		<xsl:call-template name="IS_CONVICTED_ACCIDENT" />
		<xsl:call-template name="IS_DRINK_DRUG_VOILATION" />
		<xsl:call-template name="IS_ANY_LOSS_THREE_YEARS" />
		<!--May 06 GEN INFO-->
		<!--MAY 06 -->
		<xsl:call-template name="IS_PARTICIPATE_RACE" />
		<xsl:call-template name="IS_RJ_STATE_REGD" />
		<!--MAY 06 -->
		<!--MAY 06 Effective date-->
		<xsl:call-template name="IS_EFFECTIVE_DATE" />
		<xsl:call-template name="IS_EFFECTIVE_DATE_OTHER" />
		<!--MAY 06 Effective date-->
		<!--MAY 09 Boat Coowned-->
		<xsl:call-template name="IS_BOAT_COOWNED" />
		<!--MAY 09 Boat Coowned-->
		<!--MAY 09 Boat Coowned-->
		<xsl:call-template name="IS_OP_UNDER_21" />
		<xsl:call-template name="IS_OP_UNDER_16" />
		<!--MAY 09 Boat Coowned-->
		<!--MAY 11 Wolverine insured-->
		<xsl:call-template name="IS_WOLVERINE_INSURE" />
		<!--MAY 11 Wolverine insured-->
		<!--27 June 2006-->
		<xsl:call-template name="IS_SUPP_HEATING_SOURCE" />
		<!--27 June 2006-->
		<!--GrandFather Cov 27 July 2006-->
		<xsl:call-template name="IS_RF_GRANDFATHER_COV" />
		<!--GrandFather Limit 7 MAR 2007-->
		<xsl:call-template name="IS_RF_GRANDFATHER_LIMIT" />
		<!--GrandFather Limit 7 MAR 2007-->
		<xsl:call-template name="IS_RF_GRANDFATHER_DEDUCT" />
		<!--GrandFather Limit 7 MAR 2007-->
		<xsl:call-template name="IS_RF_GRANDFATHER_ADDDEDUCT" />
		<!--GrandFather Cov 27 July 2006-->
		<!--<xsl:call-template name ="IS_RF_REFERRED3"/>-->
		<!--<xsl:call-template name ="IS_RF_REFERRED4"/>-->
		<!--<xsl:call-template name="IS_RF_COVERAGE_DECLINED"/> -->
		<!--<xsl:call-template name ="IS_RF_REFERRED6"/>-->
		<!--<xsl:call-template name ="IS_RF_REFERRED7"/>-->
		<!--<xsl:call-template name ="IS_RF_REFERRED8"/>-->
		<xsl:call-template name="IS_RF_REPLACEMENT_COST_HO3" />
		<!--Refered Case Boat 03 May 2007-->
		<xsl:call-template name="IS_REGISTERED_OTHERS" />
		<!-- ===================== Watercraft referred end ============================================= -->
		
		<!-- Added by Charles on 27-Jul-09 for Itrack 6176 -->
		<xsl:call-template name="IS_RF_RV_HORSE_POWER_OVER_750" />
		<xsl:call-template name="IS_RF_SUBURBAN_CLASS_PRIORTERM"/>
		<xsl:call-template name="IS_RF_SUBURBAN_CLASS" />		
	</xsl:template>
	<!--================================= Start Reffered Templates ========================================= -->
	<!--  1. -->
	<xsl:template name="IS_RF_DWELLING_LIMIT">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/DWELLING_LIMIT='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 2. -->
	<xsl:template name="IS_RF_HO5_YEAR_UPDATE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/HO5_YEAR_UPDATE='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 3. -->
	<xsl:template name="IS_RF_COVAC_HO6">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/COVAC_HO6='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 4. -->
	<xsl:template name="IS_RF_REFERRED4">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/REFERRED7='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 5. -->
	<xsl:template name="IS_RF_REPLACEMENT_COST_POLICY_HO2_HO3">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/REPLACEMENT_COST_POLICY_HO2_HO3='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--13th Mar 2007-->
	<!-- Reject case for additional premises -->
	<xsl:template name="IS_RF_ADDITIONAL_PREMISES_COV_DESC">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/ADDITIONAL_PREMISES_COV_DESC='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 6. -->
	<xsl:template name="IS_RF_REPLACEMENT_COST_POLICY_HO5">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/REPLACEMENT_COST_POLICY_HO5='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 7. -->
	<xsl:template name="IS_RF_REPAIR_COST_POLICY">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/REPAIR_COST_POLICY='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 8. -->
	<!--13 June 2006-->
	<xsl:template name="IS_RF_ROOF_UPDATE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/ROOF_UPDATE='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_ROOF_UPDATE_YEAR">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/ROOF_UPDATE_YEAR='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<!-- Added by Charles on 26-Nov-09 for Itrack 6679 -->
	<xsl:template name="IS_RF_ROOF_UPDATE_PREMIER">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/ROOF_UPDATE_PREMIER='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_ROOF_UPDATE_YEAR_PREMIER">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/ROOF_UPDATE_YEAR_PREMIER='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Added till here -->
	
	<xsl:template name="IS_RF_PRIOR_YEAR_BUILT">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/PRIOR_YEAR_BUILT='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_SOLID_FUEL_DEVICE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/SOLID_FUEL_DEVICE='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--. Trailer Information-->
	<xsl:template name="IS_TRAILER_DEDUCTIBLE">
		<xsl:choose>
			<xsl:when test="INPUTXML/TRAILERS/TRAILER/TRAILER_DEDUCTIBLE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--. Endorsement Information-->
	<xsl:template name="IS_OTHERS_LOCATION">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/ENDORSEMENT/OTHERS_LOCATION='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<xsl:template name="IS_RF_CENT_BURG_FIRE_ALARM_CERT_ATTACHED">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/CENT_BURG_FIRE_ALARM_CERT_ATTACHED='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Added by Charles on 20-Oct-09 for Itrack 6586-->
	<xsl:template name="IS_RF_PROT_DEVC_ALARM_CERT_ATTACHED">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/PROT_DEVC_ALARM_CERT_ATTACHED='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Added till here-->
	<xsl:template name="IS_RF_LOCATION_TYPE">
		<xsl:choose>
			<xsl:when test="INPUTXML/LOCATIONS/LOCATION/LOCATION_TYPE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>	
	<xsl:template name="IS_RF_NO_WEEKS_RENTED">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/NO_WEEKS_RENTED='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--13 June 2006-->
	<xsl:template name="IS_RF_REPLACEMENT_COST_YEAR">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/REPLACEMENT_COST_YEAR='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Uncommented by Charles on 22-Sep-09 for Itrack 6440 -->
	<xsl:template name="IS_RF_ANY_COV_DECLINED_CANCELED">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/ANY_COV_DECLINED_CANCELED='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 9. -->
	<xsl:template name="IS_RF_ANY_HEATING_SOURCE">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/ANY_HEATING_SOURCE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 10. -->
	<xsl:template name="IS_RF_PROVIDE_HOME_DAY_CARE">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/PROVIDE_HOME_DAY_CARE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_PRIOR_LOSS_INFO_EXISTS">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/PRIOR_LOSS_INFO_EXISTS='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Major Violations -->
	<xsl:template name="IS_WATER_MAJOR_VIOLATION">
		<xsl:choose>
			<xsl:when test="INPUTXML/OPERATORS/OPERATOR/WATER_MAJOR_VIOLATION='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	 <!--Major Violations At Renewal-->
	<xsl:template name="IS_RENEW_WATER_MAJOR_VIOLATION">
		<xsl:choose>
			<xsl:when test="INPUTXML/OPERATORS/OPERATOR/RENEW_WATER_MAJOR_VIOLATION='Y'">
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
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/REFERRED11='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 12. -->
	<xsl:template name="IS_RF_REFERRED12">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/REFERRED12='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 13. -->
	<xsl:template name="IS_RF_HYDRANT_DIST">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/HYDRANT_DIST='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 14. -->
	<xsl:template name="IS_RF_REFERRED14">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/REFERRED14='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 15. -->
	<xsl:template name="IS_RF_REFERRED15">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/REFERRED15='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 16. -->
	<xsl:template name="IS_RF_REFERRED16">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/REFERRED16='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 17. -->
	<xsl:template name="IS_RF_ANIMALS_EXO_PETS_HISTORY">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/ANIMALS_EXO_PETS_HISTORY='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--  18 -->
	<xsl:template name="IS_RF_CIRCUIT_BREAKERS">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/CIRCUIT_BREAKERS='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_IS_SUPERVISED">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/IS_SUPERVISED='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 19. -->
	<xsl:template name="IS_RF_IS_UNDER_CONSTRUCTION">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/UNDER_CONSTRUCTION='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 20. -->
	<xsl:template name="IS_RF_REFERRED20">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/REFERRED20='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 21. -->
	<xsl:template name="IS_RF_SINGLEITEM">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/REFERRED21='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 22. -->
	<xsl:template name="IS_RF_AGGREGATE">
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM/SINGLEITEM='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 23 -->
	<xsl:template name="IS_RF_CHARGE_OFF_PRMIUM">
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM/AGGREGATE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 24. -->
	<xsl:template name="IS_RF_PERSONALCOMPUTER">
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM/PERSONALCOMPUTER='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 25. -->
	<xsl:template name="IS_RF_JEWELLERYITEM_PICTURE">
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM/JEWELLERYITEM_PICTURE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 26. -->
	<xsl:template name="IS_RF_JEWELLERYITEM_APPRAISAL">
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM/JEWELLERYITEM_APPRAISAL='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 27. -->
	<xsl:template name="IS_RF_BREAKAGEITEM">
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM/BREAKAGEITEM='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_MI_MIXED_BREED">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/MI_MIXED_BREED='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_IN_MIXED_BREED">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/IN_MIXED_BREED='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--03 May 2007-->
	<xsl:template name="IS_RF_NO_HORSES">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/NO_HORSES='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 23 July 2009 -->
	<xsl:template name="IS_RF_SCH_CAMERAS_NON_PROFESNL">
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM/SCH_CAMERAS_NON_PROFESNL='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_SCH_FURS_HO61">
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM/SCH_FURS_HO61='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_SCH_GUNS_HO61">
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM/SCH_GUNS_HO61='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Itrack 6455 -->
	<!--<xsl:template name="IS_RF_SCH_JEWELRY_HO61">
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM/SCH_JEWELRY_HO61='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>-->
	<xsl:template name="IS_RF_SCH_JEWELRY_HO61_EXCT_TEN">
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM/SCH_JEWELRY_HO61_EXCT_TEN='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_SCH_JEWELRY_HO61_EXCT_TWO">
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM/SCH_JEWELRY_HO61_EXCT_TWO='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_SCH_JEWELRY_HO61_EXCT_TEN_PIC">
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM/SCH_JEWELRY_HO61_EXCT_TEN_PIC='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_SCH_MUSICAL_INSTRMNT_HO61">
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM/SCH_MUSICAL_INSTRMNT_HO61='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_SCH_SILVER_HO61">
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM/SCH_SILVER_HO61='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_SCH_STAMPS_HO61">
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM/SCH_STAMPS_HO61='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_SCH_RARECOINS_HO61">
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM/SCH_RARECOINS_HO61='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_SCH_FINEARTS_WO_BREAK_HO61">
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM/SCH_FINEARTS_WO_BREAK_HO61='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_SCH_FINEARTS_BREAK_HO61">
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM/SCH_FINEARTS_BREAK_HO61='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Added by Charles on 6-Oct-09 for Itrack 6488-->
	<xsl:template name="IS_RF_SCH_FINEARTS_HO61">
		<xsl:choose>
			<xsl:when test="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM/SCH_FINEARTS_HO61='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Added by Charles on 22-Sep-09 for Itrack 6440 -->
	<xsl:template name="IS_RF_CONVICTION_DEGREE_IN_PAST">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/CONVICTION_DEGREE_IN_PAST='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_DESC_RENTERS">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/DESC_RENTERS='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_FARMING_FIELD">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/FARMING_FIELD='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_BUILT_ON_CONTINUOUS_FOUNDATION">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/BUILT_ON_CONTINUOUS_FOUNDATION='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Added by Charles on 26-Nov-09 for Itrack 6679 -->
	<xsl:template name="IS_RF_BUILT_ON_CONTINUOUS_FOUNDATION_PREMIER">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/BUILT_ON_CONTINUOUS_FOUNDATION_PREMIER='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Added till here -->
	<xsl:template name="IS_RF_BUILT_ON_CONTINUOUS_FOUNDATION_COV">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/COVERAGE/BUILT_ON_CONTINUOUS_FOUNDATION_COV='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>	
	<xsl:template name="RF_IS_VACENT_OCCUPY">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/IS_VACENT_OCCUPY='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_PRIMARY_HEAT_TYPE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/PRIMARY_HEAT_TYPE='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_SECONDARY_TYPE">
		<xsl:choose>
			<xsl:when test="INPUTXML/FUELINFOS/FUELINFO/SECONDARY_HEATING_SOURCE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_MAIN_HEATING_USE">
		<xsl:choose>
			<xsl:when test="INPUTXML/FUELINFOS/FUELINFO/MAIN_HEATING_USE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="RF_IS_SWIMPOLL_HOTTUB">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/IS_SWIMPOLL_HOTTUB='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>	
	<xsl:template name="IS_RF_SUBURBAN_CLASS">
		<xsl:choose>
			<xsl:when test="contains(INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/SUBURBAN_CLASS,'Y')">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_SUBURBAN_CLASS_PRIORTERM">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/RATINGINFO/SUBURBAN_RENEWAL_REFERAL='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Added till here -->
	
	<!-- ============================= Watercraft referred section (start) ======================================== -->
	<!-- 1 -->
	<xsl:template name="IS_RF_MAX_LEN">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/LENGTH='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 2 -->
	<xsl:template name="RF_MAX_LEN_BOAT">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/MAX_LENGTH='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 3 -->
	<xsl:template name="IS_MAX_INSURING_VALUE">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/INSURING_VALUE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 4 -->
	<xsl:template name="IS_MAX_AGE">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/AGE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 5 -->
	<xsl:template name="IS_MAX_INSURING_WMOTOR">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/MAX_INSURING_WMOTOR='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 6 -->
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
	<!-- Billing Information-->
	<!-- 6 -->
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
	<!-- Credit Card -->
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
	<!-- Effective Date-->
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
	
	<!-- Total Premium-->
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
	<!-- Total Premium-->
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
	
	<!--13 June 2006-->
	<xsl:template name="IS_RF_PIC_OF_LOC">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/PIC_OF_LOC='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_PROPRTY_INSP_CREDIT">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/PROPRTY_INSP_CREDIT='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--13 June 2006-->
	<!-- 7 -->
	<xsl:template name="IS_RF_COVERAGE_DECLINED">
		<xsl:choose>
			<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/COVERAGE_DECLINED='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 7 -->
	<xsl:template name="IS_INSURED_MARKET_VALUE">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/COVERAGE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--MAY 05 ref. GEN INFO-->
	<!--1-->
	<xsl:template name="IS_PHY_MENTL_CHALLENGED">
		<xsl:choose>
			<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/PHY_MENTL_CHALLENGED='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--2-->
	<xsl:template name="IS_DRIVER_SUS_REVOKED">
		<xsl:choose>
			<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/DRIVER_SUS_REVOKED='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--3-->
	<xsl:template name="IS_CONVICTED_ACCIDENT">
		<xsl:choose>
			<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/IS_CONVICTED_ACCIDENT='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--4-->
	<xsl:template name="IS_DRINK_DRUG_VOILATION">
		<xsl:choose>
			<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/DRINK_DRUG_VOILATION='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--5-->
	<xsl:template name="IS_ANY_LOSS_THREE_YEARS">
		<xsl:choose>
			<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/ANY_LOSS_THREE_YEARS='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--MAY 05 ref. GEN INFO-->
	<!--MAY 06  Part. race -->
	<xsl:template name="IS_PARTICIPATE_RACE">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/IS_PARTICIPATE_RACE ='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--MAY 06 removed Part. race-->
	<!--MAY 06  Ref. cases-->
	<xsl:template name="IS_RJ_STATE_REGD">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/STATE_REG='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--MAY 06  Ref. cases-->
	<!--MAY 06 Effective date-->
	<xsl:template name="IS_EFFECTIVE_DATE">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/IS_EFFECTIVE_DATE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_EFFECTIVE_DATE_OTHER">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/IS_EFFECTIVE_DATE_OTHER='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--MAY 06 Effective date-->
	<!--MAY 09 Refer case-->
	<xsl:template name="IS_BOAT_COOWNED">
		<xsl:choose>
			<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/IS_BOAT_COOWNED='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--MAY 09 refer case-->
	<!--MAY 10 Refer case-->
	<xsl:template name="IS_OP_UNDER_21">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/IS_OP_UNDER_21='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_OP_UNDER_16">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/IS_OP_UNDER_16='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--MAY 10 Refer case-->
	<!--MAY 11 Wolverine Insured-->
	<xsl:template name="IS_WOLVERINE_INSURE">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/WOLVERINE_INSURE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--MAY 11 Wolverine Insured-->
	<!--19 June 2006-->
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
	<!--19 June 2006-->
	<!-- Added by Charles on 30-Nov-09 for Itrack 6647 -->
	<xsl:template name="IS_RF_WBSPO_LOSS">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/WBSPO_LOSS='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Added till here -->
	<!--27 June 2006-->
	<xsl:template name="IS_SUPP_HEATING_SOURCE">
		<xsl:choose>
			<xsl:when test="INPUTXML/HOGENINFOS/HOGENINFO/SUPP_HEATING_SOURCE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--27 June 2006-->
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
	<!-- 18.GrandFather lIMIT -->
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
	</xsl:template>
	<!-- 18.GrandFather Deductible -->
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
	</xsl:template>
	<!-- 18.GrandFather Addditional Deductible -->
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
	<xsl:template name="IS_RF_REPLACEMENT_COST_HO3">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/REPLACEMENT_COST_HO3='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RF_REPLACEMENT_COST_HO3SECONDARY">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/DWELLING/REPLACEMENT_COST_HO3SECONDARY='Y'">
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Refered Case 03 may 2007-->
	<xsl:template name="IS_REGISTERED_OTHERS">
		<xsl:choose>
			<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/IS_REGISTERED_OTHERS='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================= Watercraft referred section (end) ======================================== -->
	
	<!-- Added by Charles on 27-Jul-09 for Itrack 6176 -->
	<xsl:template name="IS_RF_RV_HORSE_POWER_OVER_750">
		<xsl:choose>
			<xsl:when test="INPUTXML/RV_VEHICLES/RV_VEHICLE/RV_HORSE_POWER_OVER_750='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Added till here -->
	<!-- Added by Charles on 9-Sep-09 for Itrack 6370 -->
	<xsl:template name="IS_MORE_MORTAGAGEE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DWELLINGS/DWELLINGINFO/ADDINTEREST/MORE_MORTAGAGEE='Y'">				
				<xsl:if test="user:DwellingInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>	
				<xsl:if test="user:DwellingInfoCount(1) = 0"></xsl:if>			
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Added till here -->
	<!-- ======================================= End reffered Templates ==================================== -->
	<!--  ================================== Calling for rejected rules (Messages) ========================= -->
	<!-- Application Information  -->
	<xsl:template match="INPUTXML/APPLICATIONS/APPLICATION">
		<xsl:choose>
			<!-- Rejected  Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="INACTIVE_APPLICATION='Y' or INACTIVE_AGENCY='Y'"></xsl:when>
				</xsl:choose>
				<xsl:call-template name="INACTIVE_APPLICATION" />
				<xsl:call-template name="INACTIVE_AGENCY" />
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred Info -->
				<xsl:choose><!--WBSPO_LOSS Added by Charles on 30-Nov-09 for Itrack 6647 -->
					<xsl:when test="CHARGE_OFF_PRMIUM='Y' or DFI_ACC_NO_RULE='Y' or CREDIT_CARD='Y' or PIC_OF_LOC='Y' or WBSPO_LOSS='Y'
					or PROPRTY_INSP_CREDIT='Y' or HO_CLAIMS='Y' or APPEFFECTIVEDATE='Y' or TOTAL_PREMIUM_AT_RENEWAL='Y' or CLAIM_EFFECTIVE='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Application information:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="CHARGE_OFF_PRMIUM"></xsl:call-template>
						<xsl:call-template name="PIC_OF_LOC"></xsl:call-template>
						<xsl:call-template name="PROPRTY_INSP_CREDIT"></xsl:call-template>
						<xsl:call-template name="HO_CLAIMS"></xsl:call-template>
						<xsl:call-template name="DFI_ACC_NO_RULE"></xsl:call-template>
						<xsl:call-template name="CREDIT_CARD" />
						<xsl:call-template name="APPEFFECTIVEDATE"></xsl:call-template>
						<xsl:call-template name="TOTAL_PREMIUM_AT_RENEWAL"></xsl:call-template>
						<xsl:call-template name="CLAIM_EFFECTIVE"></xsl:call-template>	
						<xsl:call-template name="WBSPO_LOSS" />		<!-- Added by Charles on 30-Nov-09 for Itrack 6647 -->			
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Shows the dwelling Information -->
	<xsl:template match="INPUTXML/DWELLINGS/DWELLINGINFO">
		<xsl:if test="user:DwellingInfoCount(1) = 0">
			<!--<tr>
				<td>
					<xsl:call-template name="DWELLINGINFOTOP"></xsl:call-template>--><!-- Added by Charles on 26-Oct-09 for Itrack 6634 --><!--			
				</td>
			</tr>-->
			<tr>
				<td>
					<xsl:apply-templates select="DWELLING" />
				</td>
			</tr>
			<!--<tr>
				<td>
					<xsl:apply-templates select="RATINGINFO" />
				</td>
			</tr>-->
			
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
			<!--tr>
				<td>
					<xsl:apply_template select="COVERAGESECTION2" />
				</td>
			</tr-->
			<tr>
				<td>
					<xsl:apply-templates select="ENDORSEMENT" />
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
					<xsl:when test="HO5_REP_COST_COVA='Y' or BUILDING_TYPE='Y'  or OCCUPANCY='Y' or HO5_REP_COST_COVA_NEW='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Dwelling Info:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="HO5_REP_COST_COVA" />
						<xsl:call-template name="HO5_REP_COST_COVA_NEW" /> <!-- Added by Charles on 18-Dec-09 for Itrack 6681 -->
						<xsl:call-template name="BUILDING_TYPE" />
						<!--19 june 2006
						<xsl:call-template name="REPLACEMENT_COST_HO3" />-->
						<!--19 jUne 2006-->
						<xsl:call-template name="OCCUPANCY" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred Info -->
				<xsl:choose><!--ROOF_UPDATE_PREMIER,ROOF_UPDATE_YEAR_PREMIER added by Charles on 26-Nov-09 for Itrack 6679 --><!--Added SOLID_FUEL_DEVICE,Charles,27-Nov-09,Itrack 6681-->
					<xsl:when test="REPAIR_COST_POLICY='Y'  or ROOF_UPDATE='Y' or ROOF_UPDATE_YEAR='Y' or NO_WEEKS_RENTED='Y' or ROOF_UPDATE_PREMIER='Y' or ROOF_UPDATE_YEAR_PREMIER='Y' 
					 or REPLACEMENT_COST_YEAR = 'Y' or PRIOR_YEAR_BUILT='Y' or REPLACEMENT_COST_HO3 = 'Y' or REPLACEMENT_COST_HO3SECODARY='Y' or SOLID_FUEL_DEVICE='Y'"> 
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Dwelling Info:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="REPAIR_COST_POLICY" />
						<!--13 June 2006-->
						<xsl:call-template name="ROOF_UPDATE" />
						<xsl:call-template name="ROOF_UPDATE_YEAR" />
						
						<!-- Added by Charles on 26-Nov-09 for Itrack 6679 -->
						<xsl:call-template name="ROOF_UPDATE_PREMIER" />
						<xsl:call-template name="ROOF_UPDATE_YEAR_PREMIER" />
						
						<xsl:call-template name="NO_WEEKS_RENTED" />
						<xsl:call-template name="REPLACEMENT_COST_YEAR" />
						<xsl:call-template name="REPLACEMENT_COST_HO3" />
						<xsl:call-template name="REPLACEMENT_COST_HO3SECONDARY" />
						<xsl:call-template name="PRIOR_YEAR_BUILT" />
						<xsl:call-template name="SOLID_FUEL_DEVICE" /><!--Added by Charles on 27-Nov-09 for Itrack 6681-->
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
				<xsl:choose><!--PRIMARY_HEAT_TYPE='Y' moved to referred case by Charles on 22-Sep-09 for Itrack 6440 -->
					<xsl:when test="NO_OF_FAMILIES='Y' or NO_OF_AMPS='Y' or ROOF_TYPE='Y'">
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
						<!-- <xsl:call-template name="PRIMARY_HEAT_TYPE" /> --><!--Commented by Charles on 22-Sep-09 for Itrack 6440 -->
						<xsl:call-template name="ROOF_TYPE" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred Info -->
				<xsl:choose><!--PRIMARY_HEAT_TYPE='Y' added by Charles on 22-Sep-09 for Itrack 6440 --> <!--PROT_DEVC_ALARM_CERT_ATTACHED='Y' added by Charles on 20-Oct-09 for Itrack 6586 -->
					<xsl:when test="HYDRANT_DIST='Y' or HO5_YEAR_UPDATE='Y' or CIRCUIT_BREAKERS='Y' or IS_SUPERVISED='Y' or UNDER_CONSTRUCTION='Y' or CENT_BURG_FIRE_ALARM_CERT_ATTACHED ='Y' or PROT_DEVC_ALARM_CERT_ATTACHED='Y' or PRIMARY_HEAT_TYPE='Y' or normalize-space(SUBURBAN_RENEWAL_REFERAL)='Y' or contains(SUBURBAN_CLASS,'Y')">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Rating Info:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="HYDRANT_DIST" />
						<xsl:call-template name="HO5_YEAR_UPDATE" />
						<xsl:call-template name="UNDER_CONSTRUCTION" />
						<xsl:call-template name="CIRCUIT_BREAKERS" />
						<xsl:call-template name="IS_SUPERVISED" />
						<xsl:call-template name="CENT_BURG_FIRE_ALARM_CERT_ATTACHED" />
						<xsl:call-template name="PROT_DEVC_ALARM_CERT_ATTACHED" /><!-- Added by Charles on 20-Oct-09 for Itrack 6586 -->
						<xsl:call-template name="PRIMARY_HEAT_TYPE" /><!-- Added by Charles on 22-Sep-09 for Itrack 6440 -->
						<xsl:call-template name="SUBURBAN_RENEWAL_REFERAL"/>
						<xsl:call-template name="SUBURBAN_CLASS"/><!-- itrck 6624   -->						
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- RV INFO  Reject Info -->
	<xsl:template match="RV_VEHICLE">
		<xsl:choose>
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="RV_TYPE='Y' or RV_HORSE_POWER='Y'  or RV_USED_IN_RACE_SPEED='Y' or RV_HORSE_POWER_OVER_800='Y' or RV_COV_SELC='Y'">
						<tr>
							<td class="pageheader" width="36%">For RV Info:</td>
							<tr><!-- RV#, Model, Year, Make Added by Charles on 27-Jul-09 for Itrack 6176 -->
								<td colspan="4">
									<table width="60%">																
										<tr>
											<xsl:if test="COMPANY_ID_NUMBER != ''">
												<td class="pageheader" width="18%">RV #:</td>
												<td class="midcolora" width="36%">
												<xsl:value-of select="COMPANY_ID_NUMBER" /></td>
											</xsl:if>
											<xsl:if test="RV_MODEL != ''">
												<td class="pageheader" width="18%">Model:</td>
												<td class="midcolora" width="36%">
												<xsl:value-of select="RV_MODEL" /></td>
											</xsl:if>
										</tr>
										<tr>
											<xsl:if test="RV_YEAR != ''">
												<td class="pageheader" width="18%">Year:</td>
												<td class="midcolora" width="36%">
												<xsl:value-of select="RV_YEAR" /></td>
											</xsl:if>
											<xsl:if test="RV_MAKE != ''">
												<td class="pageheader" width="18%">Make:</td>
												<td class="midcolora" width="36%">
												<xsl:value-of select="RV_MAKE" /></td>
											</xsl:if>
										</tr>
									</table>
								</td>
							</tr>							
						</tr>
						<xsl:call-template name="RV_TYPE" />
						<xsl:call-template name="RV_HORSE_POWER" />
						<xsl:call-template name="RV_USED_IN_RACE_SPEED" />
						<xsl:call-template name="RV_HORSE_POWER_OVER_800" />
						<xsl:call-template name="RV_RV_COV_SELC"/>
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<!-- Added by Charles on 27-Jul-09 for Itrack 6176 -->
			<xsl:when test="user:CheckRefer(1) = 0">
				<xsl:choose>
					<xsl:when test="RV_HORSE_POWER_OVER_750='Y'">
						<tr>
							<td class="pageheader" width="36%">For RV Info:</td>
							<tr>
								<td colspan="4">
									<table width="60%">																
										<tr>
											<xsl:if test="COMPANY_ID_NUMBER != ''">
												<td class="pageheader" width="18%">RV #:</td>
												<td class="midcolora" width="36%">
												<xsl:value-of select="COMPANY_ID_NUMBER" /></td>
											</xsl:if>
											<xsl:if test="RV_MODEL != ''">
												<td class="pageheader" width="18%">Model:</td>
												<td class="midcolora" width="36%">
												<xsl:value-of select="RV_MODEL" /></td>
											</xsl:if>
										</tr>
										<tr>
											<xsl:if test="RV_YEAR != ''">
												<td class="pageheader" width="18%">Year:</td>
												<td class="midcolora" width="36%">
												<xsl:value-of select="RV_YEAR" /></td>
											</xsl:if>
											<xsl:if test="RV_MAKE != ''">
												<td class="pageheader" width="18%">Make:</td>
												<td class="midcolora" width="36%">
												<xsl:value-of select="RV_MAKE" /></td>
											</xsl:if>
										</tr>
									</table>
								</td>
							</tr>							
						</tr>
						<xsl:call-template name="RV_HORSE_POWER_OVER_750" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<!-- Added till here -->
		</xsl:choose>
	</xsl:template>
	<!--<xsl:when test="user:CheckRefer(1) = 0"></xsl:when>-->
	<!-- Additional Interest-->
	<!-- Rating Info -->
	<xsl:template match="ADDINTEREST">
		<xsl:choose>
			<!-- Rejected  Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="HOME_BILLMORTAGAGEE='Y'">
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
						<xsl:call-template name="HOME_BILLMORTAGAGEE" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<!-- Referred Info --><!--Referred Info Uncommented by Charles on 9-Sep-09 for Itrack 6370 -->
			<xsl:when test="user:CheckRefer(1) = 0">
				<xsl:choose>
					<xsl:when test="MORE_MORTAGAGEE='Y'"><!--Changed test from 'Y'='N' by Charles on 9-Sep-09 for Itrack 6370-->
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Additional Interest:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="MORE_MORTAGAGEE" /><!-- Added by Charles on 9-Sep-09 for Itrack 6370 -->
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Coverage Info -->
	<xsl:template match="COVERAGE">
		<xsl:choose>
			<!-- Rejected  Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<!--xsl:when test="BUILT_ON_CONTINUOUS_FOUNDATION_COV='Y' or REPLACEMENT_COST_HO3_HO5_PREMIER='N' or COV/COV_DES != ''
					or LIMIT/LIMIT_DES!='' or DEDUCT/DEDUCT_DES!='' or ADDDEDUCT/ADDDEDUCT_DES!='' "-->
					<!--moved BUILT_ON_CONTINUOUS_FOUNDATION_COV='Y' to referred case by Charles on 22-Sep-09 for Itrack 6440 -->
					<!--REPLACEMENT_COST_HO5_PREMIER Added by Charles on 30-Nov-09 for Itrack 6681 -->
					<!--REPLACEMENT_COST_HO5_PREMIER_NEW Added by Charles on 18-Dec-09 for Itrack 6681 -->
					<xsl:when test="REPLACEMENT_COST_HO3_HO5_PREMIER='N' or REPLACEMENT_COST_HO5_PREMIER='N' or REPLACEMENT_COST_HO5_PREMIER_NEW='N'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Coverage Section1 Information:</td>
									</tr>
								</table>
							</td>
						</tr>
						<!-- <xsl:call-template name="BUILT_ON_CONTINUOUS_FOUNDATION_COV" /> --><!-- Commented by Charles on 22-Sep-09 for Itrack 6440 -->
						<xsl:call-template name="REPLACEMENT_COST_HO3_HO5_PREMIER" />
						<xsl:call-template name="REPLACEMENT_COST_HO5_PREMIER" /><!-- Added by Charles on 30-Nov-09 for Itrack 6681 -->
						<xsl:call-template name="REPLACEMENT_COST_HO5_PREMIER_NEW" /><!--REPLACEMENT_COST_HO5_PREMIER_NEW Added by Charles on 18-Dec-09 for Itrack 6681 -->
						<!--GrandFather Coverages>
						<xsl:call-template name="COV_DESCRIPTION" />
						<xsl:call-template name="LIMIT_DESCRIPTION" />
						<xsl:call-template name="DEDUCTIBLE_DESCRIPTION" />
						<xsl:call-template name="ADDDEDUCTIBLE_DESCRIPTION" /-->
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred Info -->
				<xsl:choose>
					<xsl:when test=" DWELLING_LIMIT='Y' or COVAC_HO6='Y' or REPLACEMENT_COST_POLICY_HO2_HO3='Y' or REPLACEMENT_COST_POLICY_HO5='Y' or COV/COV_DES != ''
					or BUILT_ON_CONTINUOUS_FOUNDATION_COV='Y' or LIMIT/LIMIT_DES!='' or DEDUCT/DEDUCT_DES!='' or ADDDEDUCT/ADDDEDUCT_DES!='' or ADDITIONAL_PREMISES_COV_DESC='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Coverage Section1 Information:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="REPLACEMENT_COST_POLICY_HO2_HO3" />
						<xsl:call-template name="REPLACEMENT_COST_POLICY_HO5" />
						<xsl:call-template name="DWELLING_LIMIT" />
						<xsl:call-template name="COVAC_HO6" />
						<xsl:call-template name="ADDITIONAL_PREMISES_COV_DESC" />
						<xsl:call-template name="BUILT_ON_CONTINUOUS_FOUNDATION_COV" /><!-- Added by Charles on 22-Sep-09 for Itrack 6440 -->						
						<!--GrandFather Coverages-->
						<xsl:call-template name="COV_DESCRIPTION" />
						<xsl:call-template name="LIMIT_DESCRIPTION" />
						<xsl:call-template name="DEDUCTIBLE_DESCRIPTION" />
						<xsl:call-template name="ADDDEDUCTIBLE_DESCRIPTION" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Coverage section 2-->
	<!--End Coverage section 2 -->
	<!-- General Info -->
	<xsl:template match="INPUTXML/HOGENINFOS/HOGENINFO">
		<xsl:choose>
			<!-- Rejected General Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose><!-- moved CONVICTION_DEGREE_IN_PAST='Y',DESC_RENTERS='Y',FARMING_FIELD='Y',BUILT_ON_CONTINUOUS_FOUNDATION='Y',IS_VACENT_OCCUPY='Y',ANY_COV_DECLINED_CANCELED='Y',IS_SWIMPOLL_HOTTUB='Y' to referred case by Charles on 22-Sep-09 for Itrack 6440 -->
					<xsl:when test="ANY_FARMING_BUSINESS_COND='Y' or IN_REJ_MIXED_BREED='Y' or BOAT_WITH_HOMEOWNER_POLICY='Y'">
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
						<!-- <xsl:call-template name="IS_VACENT_OCCUPY" /> --> <!-- Commented by Charles on 22-Sep-09 for Itrack 6440 -->
						<!-- <xsl:call-template name="IS_SWIMPOLL_HOTTUB" /> --><!-- Commented by Charles on 22-Sep-09 for Itrack 6440 -->
						<!-- <xsl:call-template name="DESC_RENTERS" /> --><!-- Commented by Charles on 22-Sep-09 for Itrack 6440 -->
						<xsl:call-template name="IN_REJ_MIXED_BREED" />
						<!-- 12 June 06-->
						<!-- <xsl:call-template name="FARMING_FIELD" /> --><!-- Commented by Charles on 22-Sep-09 for Itrack 6440 -->
						<!-- <xsl:call-template name="ANY_COV_DECLINED_CANCELED" /> --><!-- Commented by Charles on 22-Sep-09 for Itrack 6440 -->
						<!-- <xsl:call-template name="CONVICTION_DEGREE_IN_PAST" /> --><!-- Commented by Charles on 22-Sep-09 for Itrack 6440 -->
						<!--19 June 2006-->
						<!-- <xsl:call-template name="BUILT_ON_CONTINUOUS_FOUNDATION" /> --> <!-- Commented by Charles on 22-Sep-09 for Itrack 6440 -->
						<!--26 Sep 2007-->
						<xsl:call-template name="BOAT_WITH_HOMEOWNER_POLICY" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred General Info -->
				<xsl:choose><!-- Added CONVICTION_DEGREE_IN_PAST='Y',DESC_RENTERS='Y',FARMING_FIELD='Y',BUILT_ON_CONTINUOUS_FOUNDATION='Y',IS_VACENT_OCCUPY='Y',ANY_COV_DECLINED_CANCELED='Y',IS_SWIMPOLL_HOTTUB='Y' by Charles on 22-Sep-09 for Itrack 6440 -->
					<xsl:when test="ANY_HEATING_SOURCE='Y' or ANIMALS_EXO_PETS_HISTORY='Y' or CONVICTION_DEGREE_IN_PAST='Y' or DESC_RENTERS='Y' or FARMING_FIELD='Y' or IS_SWIMPOLL_HOTTUB='Y'
					 or MI_MIXED_BREED='Y' or IN_MIXED_BREED='Y' or SUPP_HEATING_SOURCE='Y' or BUILT_ON_CONTINUOUS_FOUNDATION='Y' or IS_VACENT_OCCUPY='Y' or ANY_COV_DECLINED_CANCELED='Y'
					or PROVIDE_HOME_DAY_CARE='Y' or NO_HORSES='Y' or PRIOR_LOSS_INFO_EXISTS='Y' or MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y' or BUILT_ON_CONTINUOUS_FOUNDATION_PREMIER='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Underwriting Questions:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="ANY_COV_DECLINED_CANCELED" /><!-- Uncommented by Charles on 22-Sep-09 for Itrack 6440 -->
						<xsl:call-template name="ANY_HEATING_SOURCE" />
						<xsl:call-template name="ANIMALS_EXO_PETS_HISTORY" />
						<xsl:call-template name="IN_MIXED_BREED" />
						<xsl:call-template name="MI_MIXED_BREED" />
						<xsl:call-template name="SUPP_HEATING_SOURCE" />
						<xsl:call-template name="PROVIDE_HOME_DAY_CARE" />
						<xsl:call-template name="NO_HORSES" />
						<xsl:call-template name="PRIOR_LOSS_INFO_EXISTS" />
						<xsl:call-template name="MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS" />
						<!-- Added by Charles on 22-Sep-09 for Itrack 6440 -->
						<xsl:call-template name="CONVICTION_DEGREE_IN_PAST" />
						<xsl:call-template name="DESC_RENTERS" />						
						<xsl:call-template name="FARMING_FIELD" />
						<xsl:call-template name="BUILT_ON_CONTINUOUS_FOUNDATION" />
						<xsl:call-template name="IS_VACENT_OCCUPY" />
						<xsl:call-template name="IS_SWIMPOLL_HOTTUB" />
						<!-- Added till here -->
						<!-- Added by Charles on 26-Nov-09 for Itrack 6679 -->
						<xsl:call-template name="BUILT_ON_CONTINUOUS_FOUNDATION_PREMIER" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template match="INPUTXML/SCHEDULEDITEMS/SCHEDULEDITEM">
		<xsl:choose>
			<!-- Rejected General Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="'T'='N'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Scheduled Items/Coverages:</td>
									</tr>
								</table>
							</td>
						</tr>
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred General Info -->
				<xsl:choose><!--Removed SCH_JEWELRY_HO61='Y', Itrack 6455 -->
					<xsl:when test="SINGLEITEM='Y' or AGGREGATE='Y' or PERSONALCOMPUTER='Y' or JEWELLERYITEM_PICTURE='Y' or 
					JEWELLERYITEM_APPRAISAL='Y' or BREAKAGEITEM='Y' or SCH_CAMERAS_NON_PROFESNL='Y' or SCH_FURS_HO61='Y'
					or SCH_GUNS_HO61='Y' or SCH_JEWELRY_HO61_EXCT_TEN='Y' or SCH_JEWELRY_HO61_EXCT_TWO='Y' or SCH_JEWELRY_HO61_EXCT_TEN_PIC='Y'  
					or SCH_MUSICAL_INSTRMNT_HO61='Y' or SCH_SILVER_HO61='Y' 
					or SCH_STAMPS_HO61='Y' or SCH_RARECOINS_HO61='Y' or SCH_FINEARTS_WO_BREAK_HO61='Y' or SCH_FINEARTS_BREAK_HO61='Y' or SCH_FINEARTS_HO61='Y' ">
					  <!-- Added SCH_FINEARTS_HO61 by Charles on 6-Oct-09 for Itrack 6488 -->
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Scheduled Items/Coverages:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="SINGLEITEM" />
						<xsl:call-template name="AGGREGATE" />
						<xsl:call-template name="PERSONALCOMPUTER" />
						<xsl:call-template name="JEWELLERYITEM_PICTURE" />
						<xsl:call-template name="JEWELLERYITEM_APPRAISAL" />
						<xsl:call-template name="BREAKAGEITEM" />
						<xsl:call-template name="SCH_CAMERAS_NON_PROFESNL" />
						<xsl:call-template name="SCH_FURS_HO61" />
						<xsl:call-template name="SCH_GUNS_HO61" />
						<!-- Itrack 6455 -->
						<!--<xsl:call-template name="SCH_JEWELRY_HO61" />-->
						<xsl:call-template name="SCH_JEWELRY_HO61_EXCT_TEN" />
						<xsl:call-template name="SCH_JEWELRY_HO61_EXCT_TWO" />
						<xsl:call-template name="SCH_JEWELRY_HO61_EXCT_TEN_PIC" />
						
						<xsl:call-template name="SCH_MUSICAL_INSTRMNT_HO61" />
						<xsl:call-template name="SCH_SILVER_HO61" />
						<xsl:call-template name="SCH_STAMPS_HO61" />
						<xsl:call-template name="SCH_RARECOINS_HO61" />
						<xsl:call-template name="SCH_FINEARTS_WO_BREAK_HO61" />
						<xsl:call-template name="SCH_FINEARTS_BREAK_HO61" />
						<xsl:call-template name="SCH_FINEARTS_HO61" /> <!-- Added by Charles on 6-Oct-09 for Itrack 6488 -->
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>	
	<!--========================== Trailer Infomation ========================-->
	<xsl:template match="INPUTXML/TRAILERS/TRAILER">
		<xsl:choose>
			<!-- Rejected General Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="JETSKI_TYPE_TRAILERINFO='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Trailer Infomation:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="JETSKI_TYPE_TRAILERINFO" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred General Info -->
				<xsl:choose>
					<xsl:when test="TRAILER_DEDUCTIBLE='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Trailer Infomation:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="TRAILER_DEDUCTIBLE"></xsl:call-template>
						
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--================================  END Trailer Info    =============================-->
	<!--========================================SOLID FUEL(HOME) 13 June 2006================-->
	<!-- Solid Fuel Info -->
	<xsl:template match="INPUTXML/FUELINFOS/FUELINFO">
		<xsl:choose>
			<!-- Rejected solid fuel -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose><!--SECONDARY_HEATING_SOURCE='Y',MAIN_HEATING_USE='Y' moved to Referred Case by Charles on 22-Sep-09for Itrack 6440 -->
					<xsl:when test="LABORATORY_LABEL='Y' or PROF_INSTALL_DONE='Y' or STOVE_INSTALLATION='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Solid Fuel:</td>
									</tr>
								</table>
							</td>
						</tr>
						<!-- <xsl:call-template name="SECONDARY_HEATING_SOURCE" /> --><!-- Moved to Referred Case by Charles on 22-Sep-09for Itrack 6440 -->
						<!-- <xsl:call-template name="MAIN_HEATING_USE" /> --> <!-- Moved to Referred Case by Charles on 22-Sep-09for Itrack 6440 -->
						<xsl:call-template name="STOVE_INSTALLATION" />
						<xsl:call-template name="PROF_INSTALL_DONE" />
						<xsl:call-template name="LABORATORY_LABEL" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0"><!--Referral added by Charles on 22-Sep-09 for Itrack 6440 -->
				<xsl:choose>
					<xsl:when test="SECONDARY_HEATING_SOURCE='Y' or MAIN_HEATING_USE='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Solid Fuel:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="MAIN_HEATING_USE" />
						<xsl:call-template name="SECONDARY_HEATING_SOURCE" />
					</xsl:when>
				</xsl:choose>
			</xsl:when><!-- Added till here -->
		</xsl:choose>
	</xsl:template>
	<!--========================================END SOLID FUEL  ===========================-->
	<!--ENDORSEMENT-->
	<!--========================================ENDORSEMENT 20 June 2006================-->
	<!-- ENDORSEMENT Info -->
	<xsl:template match="ENDORSEMENT">
		<xsl:choose>
			<!--<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="OTHERS_LOCATION='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Endorsement Info:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="OTHERS_LOCATION" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>-->
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred General Info -->
				<xsl:choose>
					<xsl:when test="OTHERS_LOCATION='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Endorsement Info:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="OTHERS_LOCATION"></xsl:call-template>						
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--========================================END ENDORSEMENT  ===========================-->
	<!--ENDORSEMENT-->
	<!--==============LOCATION HOME 13 June 2006=================-->
	<!-- Location Info -->
	<xsl:template match="INPUTXML/LOCATIONS/LOCATION">
		<xsl:choose>			
			<!--Rejected  Info added by Charles on 18-Dec-09 for Itrack 6818 -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="LOCATION_STATE_APP_STATE='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Location Info:</td>
									</tr>
								</table>
							</td>
						</tr>						
						<xsl:call-template name="LOCATION_STATE_APP_STATE" /> 	
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<!-- Added till here -->
			<xsl:when test="user:CheckRefer(1) = 0">
				<xsl:choose>
					<xsl:when test="LOCATION_TYPE='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Location Info:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="LOCATION_TYPE" />						
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--==============END LOCATION HOME 13 June 2006=================-->
	<!-- ================================== Watercraft section (start)===================================== -->
	<!--========================================== Boat Info ==========================================-->
	<xsl:template match="INPUTXML/BOATS/BOAT">
		<xsl:choose>
			<!-- Rejected messages -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="MAX_SPEED='Y' or JETSKI_TYPE_TRAILER='Y' or PRINCIPLE_OPERATOR='Y'">
						<tr>
							<td class="pageheader" width="36%">For Boat:</td>
							<tr>
								<td colspan="4">
									<table>
										<tr>
											<td class="pageheader" width="18%">Boat No:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="BOAT_NO" />
											</td>
											<td class="pageheader" width="18%">Model:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="MODEL" />
											</td>
										</tr>
										<tr>
											<td class="pageheader" width="18%">Boat Year:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="BOAT_YEAR" />
											</td>
											<td class="pageheader" width="18%">Make:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="MAKE" />
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</tr>
						<xsl:call-template name="MAX_SPEED" />
						<xsl:call-template name="JETSKI_TYPE_TRAILER" />
						<xsl:call-template name="PRINCIPLE_OPERATOR" />
						<!--MAY 06 removed state and P.race as rejected cases-->
						<!--<xsl:call-template name="STATE_REG" />
						<xsl:call-template name="PARTICIPATE_RACE" />-->
						<!-- <xsl:call-template name ="REJECT2"/> -->
						<!-- <xsl:call-template name ="REJECT3"/> -->
						<!-- <xsl:call-template name ="REJECT4"/> -->
						<!-- <xsl:call-template name ="REJECT5"/> -->
						<!-- <xsl:call-template name ="REJECT6"/> -->
						<!-- <xsl:call-template name ="REJECT7"/> -->
						<!-- <xsl:call-template name ="REJECT8"/> -->
						<!-- <xsl:call-template name ="REJECT9"/> -->
						<!-- <xsl:call-template name ="REJECT10"/> -->
						<!-- <xsl:call-template name ="REJECT11"/> -->
						<!-- ========================== Start for Indiana only ========================== -->
						<!-- <xsl:if test="INPUTXML/VEHICLES/VEHICLE/STATE_ID = 14 ">
							<RULE RULEID="13" DESC="">
								<xsl:call-template name="REJECT13" />
							</RULE>
						</xsl:if> -->
						<!-- ==========================End  for Indiana only ========================== -->
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- ========================== Checking for referred rules (Messages) ===================== -->
				<xsl:choose>
					<xsl:when test="LENGTH='Y' or MAX_LENGTH='Y' or INSURING_VALUE='Y' or AGE='Y'
					 or MAX_INSURING_WMOTOR='Y' or COVERAGE='Y' or IS_PARTICIPATE_RACE='Y' or STATE_REG='Y' or IS_EFFECTIVE_DATE='Y' or IS_EFFECTIVE_DATE_OTHER='Y' or IS_OP_UNDER_21='Y' or IS_OP_UNDER_16='Y' or WOLVERINE_INSURE='Y'">
						<tr>
							<td class="pageheader" width="36%">For Boat:</td>
							<tr>
								<td colspan="4">
									<table>
										<tr>
											<td class="pageheader" width="18%">Boat No:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="BOAT_NO" />
											</td>
											<td class="pageheader" width="18%">Model:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="MODEL" />
											</td>
										</tr>
										<tr>
											<td class="pageheader" width="18%">Boat Year:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="BOAT_YEAR" />
											</td>
											<td class="pageheader" width="18%">Make:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="MAKE" />
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</tr>
						<!-- 1. -->
						<xsl:call-template name="BOAT_LENGTH" />
						<!-- 2. -->
						<xsl:call-template name="MAX_LENGTH_BOAT" />
						<!-- 3. -->
						<xsl:call-template name="MAX_INSURING_VALUE" />
						<!-- 4. -->
						<xsl:call-template name="MAX_AGE" />
						<!-- 5. -->
						<xsl:call-template name="MAX_INSURING_WMOTOR" />
						<!-- 6 -->
						<xsl:call-template name="MUST_INSURED" />
						<!--MAY 06 Participate race  Refered case-->
						<xsl:call-template name="PARTICIPATE_RACE" />
						<!--MAY 06 Participate race  Refered case-->
						<!--MAY 06  State reg -->
						<xsl:call-template name="STATE_REG" />
						<!--MAY 06  State reg -->
						<!--MAY 06 Effective date-->
						<xsl:call-template name="EFFECTIVE_DATE" />
						<xsl:call-template name="EFFECTIVE_DATE_OTHER" />
						<!--MAY 06 Effective date-->
						<!--MAY 10 Operator under  21-->
						<xsl:call-template name="OP_UNDER_21" />
						<xsl:call-template name="OP_UNDER_16" />
						<!--MAY 10 Operator under  21-->
						<!--MAY 11 Wolverine Insured-->
						<xsl:call-template name="WOLVERINE_INSURE" />
						<!--MAY 11 Wolverine Insured-->
						<!-- <<xsl:call-template name ="REFERED2"></xsl:call-template> -->
						<!-- 3. -->
						<!-- <xsl:call-template name ="REFERED3"></xsl:call-template> -->
						<!-- 6. -->
						<!-- <xsl:call-template name ="REFERED6"></xsl:call-template> -->
						<!-- 10. -->
						<!-- <xsl:call-template name ="REFERED10"></xsl:call-template> -->
						<!-- 12. -->
						<!-- <xsl:call-template name ="REFERED12"></xsl:call-template>	 -->
						<!-- 14. -->
						<!-- <xsl:call-template name ="REFERED14"></xsl:call-template>	 -->
						<!-- 15. -->
						<!-- <xsl:call-template name ="REFERED15"></xsl:call-template>	 -->
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- ======================== Operator Detail messages ========================================== -->
	<xsl:template match="INPUTXML/OPERATORS/OPERATOR">
		<xsl:choose>
			<!-- Rejected Operator Detail -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="SD_POINTS='Y' or RENEW_SD_POINTS='Y'">
						<tr>
							<td class="pageheader">For Operator:</td>
							<tr>
								<td colspan="4">
									<table>
										<tr>
											<td class="pageheader" width="18%">Name:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="DRIVER_NAME" />
											</td>
											<td class="pageheader" width="18%">Code:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="DRIVER_CODE" />
											</td>
										</tr>
										<tr>
											<td class="pageheader" width="18%">License Number:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="DRIVER_DRIV_LIC" />
											</td>
											<td class="pageheader" width="18%">Sex:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="DRIVER_SEX" />
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</tr>
						<xsl:call-template name="SD_POINTS"></xsl:call-template>
						<xsl:call-template name="RENEW_SD_POINTS"></xsl:call-template>
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred Driver Detail -->
				<xsl:choose>
					<xsl:when test="WATER_MAJOR_VIOLATION='Y' or RENEW_WATER_MAJOR_VIOLATION='Y'">
						<tr>
							<td class="pageheader">For Operator:</td>
							<tr>
								<td colspan="4">
									<table>
										<tr>
											<td class="pageheader" width="18%">Name:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="DRIVER_NAME" />
											</td>
											<td class="pageheader" width="18%">Code:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="DRIVER_CODE" />
											</td>
										</tr>
										<tr>
											<td class="pageheader" width="18%">License Number:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="DRIVER_DRIV_LIC" />
											</td>
											<td class="pageheader" width="18%">Sex:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="DRIVER_SEX" />
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</tr>
						<xsl:call-template name="WATER_MAJOR_VIOLATION"></xsl:call-template>
						<xsl:call-template name="RENEW_WATER_MAJOR_VIOLATION"></xsl:call-template>
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- ========================================== General Info ========================================= -->
	<xsl:template match="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO">
		<xsl:choose>
			<!-- Rejected General Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="IS_RENTED_OTHERS='Y'  or ANY_BOAT_AMPHIBIOUS='Y' or CARRY_PASSENGER_FOR_CHARGE='Y' or ANY_BOAT_RESIDENCE='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Watercraft Underwriting Questions:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="RENTED_OTHERS" />
						<!--MAY 06 commented for rejected cases-->
						<!--<xsl:call-template name="CONVICTED_ACCIDENT" />-->
						<!--MAY 06 commented for rejected cases-->
						<!--May 05-->
						<xsl:call-template name="BOAT_AMPHIBIOUS" />
						<!--May 05-->
						<!--MAY 06 Carry passengers-->
						<xsl:call-template name="CARRY_PASSENGER_FOR_CHARGE" />
						<xsl:call-template name="BOAT_COOWNED" />
						<xsl:call-template name="ANY_BOAT_RESIDENCE" />
						<!--MAY 06 Carry passengers-->
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred General Info -->
				<xsl:choose>
					<xsl:when test="COVERAGE_DECLINED='Y' or PHY_MENTL_CHALLENGED='Y' or DRIVER_SUS_REVOKED='Y' or IS_CONVICTED_ACCIDENT ='Y' or DRINK_DRUG_VOILATION='Y' or ANY_LOSS_THREE_YEARS='Y' or IS_BOAT_COOWNED='Y' or IS_REGISTERED_OTHERS='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Watercraft Underwriting Questions:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="COVERAGE_DECLINED"></xsl:call-template>
						<!--MAY 05 GEN INFO -->
						<xsl:call-template name="PHY_MENTL_CHALLENGED"></xsl:call-template>
						<xsl:call-template name="DRIVER_SUS_REVOKED"></xsl:call-template>
						<xsl:call-template name="CONVICTED_ACCIDENT"></xsl:call-template>
						<xsl:call-template name="DRINK_DRUG_VOILATION"></xsl:call-template>
						<xsl:call-template name="ANY_LOSS_THREE_YEARS"></xsl:call-template>
						<!--MAY 05-->
						<!--MAY 09-->
						<xsl:call-template name="BOAT_COOWNED" />
						<!--MAY 09-->
						<!--MAY 03 2007-->
						<xsl:call-template name="REGISTERED_OTHERS" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- ================================== Watercraft section (end)======================================= -->
	<!-- *************** Templates for showing rejected rules messages **************** -->
	<!--1.  HO-2, HO-3, and HO-5: Dwelling must not contain more than 2 families.-->
	<xsl:template name="NO_OF_FAMILIES">
		<xsl:choose>
			<xsl:when test="NO_OF_FAMILIES='Y'">
				<TR>
					<td class="midcolora">Dwelling has more than 2 families.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--2. HO-5:Must be insured to 100% of replacement cost and coverage A must be equal to $125,000 or more. -->
	<xsl:template name="HO5_REP_COST_COVA">
		<xsl:choose>
			<xsl:when test="HO5_REP_COST_COVA='Y'">
				<TR>
					<td class="midcolora">Must be insured to 100% of replacement cost and coverage A must be equal to $125,000 or more.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Added by Charles on 18-Dec-09 for Itrack 6681 -->
	<xsl:template name="HO5_REP_COST_COVA_NEW">
		<xsl:choose>
			<xsl:when test="HO5_REP_COST_COVA_NEW='Y'">
				<TR>
					<td class="midcolora">Must be insured to 100% of replacement cost and coverage A must be equal to $200,000 or more.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Added till here -->
	<!--3. A person who insures or seeks to insure a dwelling being used for illegal or demonstrably hazardous purposes-->
	<xsl:template name="IS_REJECT3">
		<xsl:choose>
			<xsl:when test="IS_REJECT3='Y'">
				<TR>
					<td class="midcolora">Dwelling is used for illegal or demonstrably hazardous purposes.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--4. -->
	<xsl:template name="IS_REJECT4">
		<xsl:choose>
			<xsl:when test="IS_REJECT4 ='Y'">
				<TR>
					<td class="midcolora">Dwellings, outbuildings or premises in poor physical condition or lacking in maintenance.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--5. -->
	<xsl:template name="IS_REJECT5">
		<xsl:choose>
			<xsl:when test="IS_REJECT5='Y'">
				<TR>
					<td class="midcolora">At renewal only: After written notice, failure by the insured within 30 days to correct or have taken definite action to correct a physical condition directly related to a paid claim or one which presents a clear risk of significant loss under the policy.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--6. An insured that regularly provides day care services to a person or persons other than insured. -->
	<xsl:template name="ANY_FARMING_BUSINESS_COND">
		<xsl:choose>
			<xsl:when test="ANY_FARMING_BUSINESS_COND='Y'">
				<TR>
					<td class="midcolora">Business is conducted on Premises(Including day or child care).</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--7. -->
	<xsl:template name="IS_REJECT7">
		<xsl:choose>
			<xsl:when test="IS_REJECT7='Y'">
				<TR>
					<td class="midcolora">3 paid claims totaling $1,500 or more, exclusive to weather related claims, in the preceding 3 years.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--8. -->
	<xsl:template name="IS_REJECT8">
		<xsl:choose>
			<xsl:when test="IS_REJECT8='Y'">
				<TR>
					<td class="midcolora">3 paid claims totaling $2,000 or more, including weather related claims, in the preceding 3 years.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--9. -->
	<xsl:template name="IS_REJECT9">
		<xsl:choose>
			<xsl:when test="IS_REJECT9='Y'">
				<TR>
					<td class="midcolora">Liability claim in the preceding 3 years arising out of the negligence of an insured.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--10.  Primary dwelling is unoccupied or vacant. -->
	<xsl:template name="IS_VACENT_OCCUPY">
		<xsl:choose>
			<xsl:when test="IS_VACENT_OCCUPY='Y'">
				<TR>
					<td class="midcolora">Dwelling is unoccupied or vacant.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--11. -->
	<xsl:template name="IS_REJECT11">
		<xsl:choose>
			<xsl:when test="IS_REJECT11='Y'">
				<TR>
					<td class="midcolora">Property taxes with respect to the dwelling have been or are delinquent.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--12. Mobile homes or house trailers. -->
	<xsl:template name="BUILDING_TYPE">
		<xsl:choose>
			<xsl:when test="BUILDING_TYPE='Y'">
				<TR>
					<td class="midcolora">Type of dwelling is Mobile Homes or House Trailers.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--13. -->
	<xsl:template name="IS_REJECT13">
		<xsl:choose>
			<xsl:when test="IS_REJECT13='Y'">
				<TR>
					<td class="midcolora">Modular/Manufactured homes with a value of less than $75,000.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--14. -->
	<xsl:template name="IS_REJECT14">
		<xsl:choose>
			<xsl:when test="IS_REJECT14='Y'">
				<TR>
					<td class="midcolora">Risk with more than 3 horses.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--15. -->
	<xsl:template name="IS_REJECT15">
		<xsl:choose>
			<xsl:when test="IS_REJECT15='Y'">
				<TR>
					<td class="midcolora">Failure to disclose all losses or claims that occurred in preceding 3 years.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--16. Any dwelling with less than 100amp electrical service. -->
	<xsl:template name="NO_OF_AMPS">
		<xsl:choose>
			<xsl:when test="NO_OF_AMPS='Y'">
				<TR>
					<td class="midcolora">Dwelling has less than 100amp electrical service.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--17. -->
	<xsl:template name="IS_REJECT17">
		<xsl:choose>
			<xsl:when test="IS_REJECT17='Y'">
				<TR>
					<td class="midcolora">Any dwelling where an inspection of value and condition is refused by the insured for renewals, or an applicant for new business.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--18. Any dwelling with an enclosed swimming pool or jetted hot tub (not included bathroom type Jacuzzi or hot tub). -->
	<xsl:template name="IS_SWIMPOLL_HOTTUB">
		<xsl:choose>
			<xsl:when test="IS_SWIMPOLL_HOTTUB='Y'">
				<TR>
					<td class="midcolora">Dwelling(s) with an enclosed swimming pool or jetted hot tub.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- HO-2, HO-3, and HO-5: Dwelling must not contain  more than 2 roomers or boarders. -->
	<xsl:template name="DESC_RENTERS">
		<xsl:choose>
			<xsl:when test="DESC_RENTERS='Y'">
				<TR>
					<td class="midcolora">Dwelling(s) having more than 2 roomers or boarders.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IN_REJ_MIXED_BREED">
		<xsl:choose>
			<xsl:when test="IN_REJ_MIXED_BREED='Y'">
				<TR>
					<td class="midcolora">Selected Breed of Dog is ineligible.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--19. -->
	<xsl:template name="IS_REJECT19">
		<xsl:choose>
			<xsl:when test="IS_REJECT19='Y'">
				<TR>
					<td class="midcolora">2 paid claims totaling $1,000 or more in the preceding 3 years.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--20.  Owner of vicious dogs, dangerous animals, or farm animals (see list). -->
	<xsl:template name="IS_DANGANIMAL">
		<xsl:choose>
			<xsl:when test="ANIMALS_EXO_PETS_HISTORY='Y'">
				<TR>
					<td class="midcolora">Owner of vicious dogs, dangerous animals, or farm animals.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--21. Any home built prior to 1950 and an applicant for new business with an insurance score below 600.  -->
	<xsl:template name="IS_HOMEBUILT">
		<xsl:choose>
			<xsl:when test="YEAR_BUILT ='N' or CUSTOMER_INSURANCE_SCORE='Y'">
				<TR>
					<td class="midcolora">Dwelling was built prior to 1950 or an applicant for new business has an Insurance Score less than 600.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MI_MIXED_BREED">
		<xsl:choose>
			<xsl:when test="MI_MIXED_BREED ='Y'">
				<TR>
					<td class="midcolora">Breed of Dog is Mixed breed.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IN_MIXED_BREED">
		<xsl:choose>
			<xsl:when test="IN_MIXED_BREED ='Y'">
				<TR>
					<td class="midcolora">Breed of Dog is Mixed breed.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--12 June 2006-->
	<xsl:template name="FARMING_FIELD">
		<xsl:choose>
			<xsl:when test="FARMING_FIELD ='Y'">
				<TR>
					<td class="midcolora"> Farming Acres Field is greater then 500.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PRIOR_YEAR_BUILT">
		<xsl:choose>
			<xsl:when test="PRIOR_YEAR_BUILT='Y'">
				<TR>
					<td class="midcolora">Dwelling Details Year Built Field is prior to 1950 and insurance score is less than 600.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--Added by Charles on 27-Nov-09 for Itrack 6681-->
	<xsl:template name="SOLID_FUEL_DEVICE">
		<xsl:choose>
			<xsl:when test="SOLID_FUEL_DEVICE='Y'">
				<TR>
					<td class="midcolora">Other Structure with a Solid Fuel Device.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Added till here -->
	<!--12 June 2006-->
	<!--13 June 2006-->
	<xsl:template name="SECONDARY_HEATING_SOURCE">
		<xsl:choose>
			<xsl:when test="SECONDARY_HEATING_SOURCE='Y'">
				<TR>
					<td class="midcolora">Secondary Heating Type (Coal Professionally/Non Professionally Installed , Wood Stove - Professionally/Non Professionally Installed , Pellet/Corn Burner , Stove Fireplace Insert ,Add on Furnace) and Either Installation was not done by a professional installer such as a contractor or the unit does not have a testing laboratory label (UL, other).</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--13 June 2006-->
	<!--15 June 2006-->
	<xsl:template name="MAIN_HEATING_USE">
		<xsl:choose>
			<xsl:when test="MAIN_HEATING_USE='Y'">
				<TR>
					<td class="midcolora">Heating use is Primary.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="LABORATORY_LABEL">
		<xsl:choose>
			<xsl:when test="LABORATORY_LABEL='Y'">
				<TR>
					<td class="midcolora">No unit have a testing laboratory label (UL, other).</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PROF_INSTALL_DONE">
		<xsl:choose>
			<xsl:when test="PROF_INSTALL_DONE='Y'">
				<TR>
					<td class="midcolora">No installation done by a professional installer such as a contractor.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="STOVE_INSTALLATION">
		<xsl:choose>
			<xsl:when test="STOVE_INSTALLATION='Y'">
				<TR>
					<td class="midcolora">No stove installation and use conform to all of its manufacturers specifications and local fire codes.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--15 June 2006-->
	<!--19 June 2006-->
	<xsl:template name="BUILT_ON_CONTINUOUS_FOUNDATION">
		<xsl:choose>
			<xsl:when test="BUILT_ON_CONTINUOUS_FOUNDATION='Y'">
				<TR>
					<td class="midcolora">Modular/Manufactured home not built on a continuous foundation.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Added by Charles on 26-Nov-09 for Itrack 6679 -->
	<xsl:template name="BUILT_ON_CONTINUOUS_FOUNDATION_PREMIER">
		<xsl:choose>
			<xsl:when test="BUILT_ON_CONTINUOUS_FOUNDATION_PREMIER='Y'">
				<TR>
					<td class="midcolora">Modular/Manufactured home.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Added till here -->
	<xsl:template name="BUILT_ON_CONTINUOUS_FOUNDATION_COV">
		<xsl:choose>
			<xsl:when test="BUILT_ON_CONTINUOUS_FOUNDATION_COV='Y'">
				<TR>
					<td class="midcolora">Modular/Manufactured home is built on a continuous foundation and Coverage A is less than $75,000.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Added by Charles on 30-Nov-09 for Itrack 6647 -->
	<xsl:template name="WBSPO_LOSS">
		<xsl:choose>
			<xsl:when test="WBSPO_LOSS='Y'">
				<TR>
					<td class="midcolora">Water back up or Sump Pump Loss in the last 36 months.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Added till here -->
	<xsl:template name="REPLACEMENT_COST_HO3">
		<xsl:choose>
			<xsl:when test="REPLACEMENT_COST_HO3='Y'">
				<TR>
					<td class="midcolora">Policy type HO-3 requires minimum coverage of $50,000.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="REPLACEMENT_COST_HO3SECONDARY">
		<xsl:choose>
			<xsl:when test="REPLACEMENT_COST_HO3SECONDARY='Y'">
				<TR>
					<td class="midcolora">Policy type HO-3 requires minimum coverage of $40,000.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SUPP_HEATING_SOURCE">
		<xsl:choose>
			<xsl:when test="SUPP_HEATING_SOURCE='Y'">
				<TR>
					<td class="midcolora">Please provide the Solid Fuel details in 'Risk Information -> Supplemental Application-Others' section.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Removed HO-5 Premier, Charles (30-Nov-09), Itrack 6681 -->
	<xsl:template name="REPLACEMENT_COST_HO3_HO5_PREMIER">
		<xsl:choose>
			<xsl:when test="REPLACEMENT_COST_HO3_HO5_PREMIER='N'">
				<TR>
					<td class="midcolora">For Policy types HO-3 Premier, Covg. A should be 100% of Full Replacement value and Replacement Value must be equal and over $175,000.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Added by Charles on 30-Nov-09 for Itrack 6681 -->
	<xsl:template name="REPLACEMENT_COST_HO5_PREMIER">
		<xsl:choose>
			<xsl:when test="REPLACEMENT_COST_HO5_PREMIER='N'">
				<TR>
					<td class="midcolora">For Policy types HO-5 Premier, Covg. A should be 100% of Full Replacement value and Replacement Value must be equal and over $175,000.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>	
	<!-- Added till here -->
	<!-- Added by Charles on 18-Dec-09 for Itrack 6681 -->
	<xsl:template name="REPLACEMENT_COST_HO5_PREMIER_NEW">
		<xsl:choose>
			<xsl:when test="REPLACEMENT_COST_HO5_PREMIER_NEW='N'">
				<TR>
					<td class="midcolora">For Policy types HO-5 Premier, Covg. A should be 100% of Full Replacement value and Replacement Value must be equal and over $200,000.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>	
	<!-- Added till here -->
	<!--20 June 2006-->
	<xsl:template name="OTHERS_LOCATION">
		<xsl:choose>
			<xsl:when test="OTHERS_LOCATION='Y'">
				<TR>
					<td class="midcolora">Endorsement(HO-70 Additional Residence- Other Locations- Rented to others- 1 family or HO-70 Additional Residence- Other Locations- Rented to others- 2 family or HO-40 other Structures Rented to Others) is checked and Other Loc/Liability is not filled.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--20 June 2006-->
	<xsl:template name="OCCUPANCY">
		<xsl:choose>
			<xsl:when test="OCCUPANCY='Y'">
				<TR>
					<td class="midcolora">Dwelling is unoccupied or vacant.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--18. Any dwelling with a flat roof for either HO-3 or HO-5. -->
	<xsl:template name="ROOF_TYPE">
		<xsl:if test="ROOF_TYPE='Y'">
			<TR>
				<td class="midcolora">Dwelling has a flat roof.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Do you want to add a boat with this Homeowner policy. -->
	<xsl:template name="BOAT_WITH_HOMEOWNER_POLICY">
		<xsl:if test="BOAT_WITH_HOMEOWNER_POLICY='Y'">
			<TR>
				<td class="midcolora">Boat with this Homeowner policy is selected 'Yes' but Boat(s) are not added or vice versa.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--BILL MORTAGAGEE-->
	<xsl:template name="HOME_BILLMORTAGAGEE">
		<xsl:if test="HOME_BILLMORTAGAGEE='Y'">
			<TR>
				<td class="midcolora">Select At least one 'Yes' to Bill this mortgagee.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--Trailer Rule-->
	<xsl:template name="JETSKI_TYPE_TRAILERINFO">
		<xsl:if test="JETSKI_TYPE_TRAILERINFO='Y'">
			<TR>
				<td class="midcolora">Trailer Info > Jet Ski type trailer can only be attached with Jet Ski type boat or Vice versa.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--*********************************End Rejected Templates************************************************ -->
	<!-- *************** *****************Templates for showing refered rules messages **************** -->
	<!--1 Any risk with coverage A over $400,000. -->
	<xsl:template name="DWELLING_LIMIT">
		<xsl:if test="DWELLING_LIMIT='Y'">
			<TR>
				<td class="midcolora">Coverage A over $400,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--2 HO-5: Home must have been built during or since 1950 unless verification of updates before last 10 years of roof, plumbing, heating, and electric. -->
	<xsl:template name="HO5_YEAR_UPDATE">
		<xsl:if test="HO5_YEAR_UPDATE='Y'">
			<TR>
				<td class="midcolora">Dwelling was built prior to 1950 and updates received of Wiring ,Plumbing, Heating, Roofing or electric before last 10 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--3  HO-6: Coverage A must be a minimum of 10% of coverage C or $2,000. -->
	<xsl:template name="COVAC_HO6">
		<xsl:if test="COVAC_HO6='Y'">
			<TR>
				<td class="midcolora">Dwelling Limit is either less than 10% of Unscheduled Personal Property or less than $2,000 (whichever is higher).</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--4. -->
	<xsl:template name="RF_REFERRED4">
		<xsl:if test="RF_REFERRED4='Y'">
			<TR>
				<td class="midcolora">Incidental office, private school, and studio occupancies unless certain conditions are met.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--5. Repair Cost Policy on HO-2 or HO-3: Any person who refuses to purchase at least 80% of the full replacement value as figured. -->
	<xsl:template name="REPLACEMENT_COST_POLICY_HO2_HO3">
		<xsl:if test="REPLACEMENT_COST_POLICY_HO2_HO3='Y'">
			<TR>
				<td class="midcolora">Dwelling limit is less than 80% of the full replacement cost.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--6. Replacement Cost Policy on HO-5: Any person who refuses to purchase at least 100% of the full replacement value. -->
	<xsl:template name="REPLACEMENT_COST_POLICY_HO5">
		<xsl:if test="REPLACEMENT_COST_POLICY_HO5='Y'">
			<TR>
				<td class="midcolora">Dwelling Limit is not 100% of the full replacement cost.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--7. Repair Cost Policy on all forms: Any person who refuses to purchase at least 100% of the market value. -->
	<xsl:template name="REPAIR_COST_POLICY">
		<xsl:if test="REPAIR_COST_POLICY='Y'">
			<TR>
				<td class="midcolora">Repair Cost is greater than 100% of the market value.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--8. A person whose policy has been canceled for nonpayment of premium within the preceding 2 years, unless the premium is paid in full before issuance or renewal. -->
	<xsl:template name="ANY_COV_DECLINED_CANCELED">
		<xsl:if test="ANY_COV_DECLINED_CANCELED='Y'">
			<TR>
				<td class="midcolora">Dwelling(s) having coverage declined, cancelled or non-renewed during the last 3 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--9. Presence of a wood or coal-heating stove or furnace, free-standing fireplace, or fireplace insert unless requirements are met. -->
	<xsl:template name="ANY_HEATING_SOURCE">
		<xsl:if test="ANY_HEATING_SOURCE='Y'">
			<TR>
				<td class="midcolora">Dwelling(s) having supplemental heating source (wood stove, fireplace inserts, etc.).</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--10. A person who has been successfully denied payment of a claim within the past 5 years or been convicted of arson, conspiracy to commit arson, misrepresentation, fraud or conspiracy to commit fraud. -->
	<xsl:template name="CONVICTION_DEGREE_IN_PAST">
		<xsl:if test="CONVICTION_DEGREE_IN_PAST='Y'">
			<TR>
				<td class="midcolora">Conviction of degree of arson in past 10 years.</td><!-- Changed from 3 years by Charles on 22-Sep-09 for Itrack 6440 -->
			</TR>
		</xsl:if>
	</xsl:template>
	<!--11. Existence of an adjacent physical hazard.-->
	<xsl:template name="RF_REFERRED11">
		<xsl:if test="RF_REFERRED11='Y'">
			<TR>
				<td class="midcolora">Adjacent physical hazard exists.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--12 -->
	<xsl:template name="RF_REFERRED12">
		<xsl:if test="RF_REFERRED12='Y'">
			<TR>
				<td class="midcolora">Dwellings that are self-constructed or remodeled unless the wiring, heating and plumbing systems have been professional installed and inspected.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--13. Dwellings that are isolated, inaccessible, or where the response time of a fire department is considered excessive. -->
	<xsl:template name="HYDRANT_DIST">
		<xsl:if test="HYDRANT_DIST='Y'">
			<TR>
				<td class="midcolora">Distance to the fire station is over 1000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--14. -->
	<xsl:template name="RF_REFERRED14">
		<xsl:if test="RF_REFERRED14='Y'">
			<TR>
				<td class="midcolora">Modular/Manufactured homes with a value of $75,000 or greater unless certain conditions are met.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--15. -->
	<xsl:template name="RF_REFERRED15">
		<xsl:if test="RF_REFERRED15='Y'">
			<TR>
				<td class="midcolora">A farm or ranch dwelling unless certain conditions are met.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--16. -->
	<xsl:template name="RF_REFERRED16">
		<xsl:if test="RF_REFERRED16='Y'">
			<TR>
				<td class="midcolora">Risks that are subject to an above-average theft exposure.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--17. Owner of vicious dogs(see list), dangerous animals or farm animals. -->
	<xsl:template name="ANIMALS_EXO_PETS_HISTORY">
		<xsl:if test="ANIMALS_EXO_PETS_HISTORY='Y'">
			<TR>
				<td class="midcolora">Owner of vicious dogs,dangerous animals or farm animals.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- 13th Mar 2007 for Additional Premises-->
	<xsl:template name="ADDITIONAL_PREMISES_COV_DESC">
		<xsl:choose>
			<xsl:when test="ADDITIONAL_PREMISES_COV_DESC='Y'">
				<TR>
					<td class="midcolora">Additional Premises (Number of Premises) are greater than 4.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_SUPERVISED">
		<xsl:choose>
			<xsl:when test="IS_SUPERVISED='Y'">
				<TR>
					<td class="midcolora">Construction not supervised by Licensed Building Contractor.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="CIRCUIT_BREAKERS">
		<xsl:if test="CIRCUIT_BREAKERS='Y'">
			<TR>
				<td class="midcolora">No Circuit Breakers.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--20. -->
	<xsl:template name="UNDER_CONSTRUCTION">
		<xsl:if test="UNDER_CONSTRUCTION ='Y'">
			<TR>
				<td class="midcolora">Dwelling is under construction.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--21. Scheduled item exceeds $10,000 in value (see binder limit table page GR-5). -->
	<xsl:template name="SINGLEITEM">
		<xsl:if test="SINGLEITEM='Y'">
			<TR>
				<td class="midcolora">Value of Scheduled item exceeds $10,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--22 Aggregate schedule exceeds $50,000 in value (see binder limit table page GR-5) -->
	<xsl:template name="AGGREGATE">
		<xsl:if test="AGGREGATE='Y'">
			<TR>
				<td class="midcolora">Aggregate value of schedule items exceeds $50,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--23 Total Insoring value of Personal Computer Desktop or Personal Computers - Laptop categories is over $8,000  -->
	<xsl:template name="PERSONALCOMPUTER">
		<xsl:if test="PERSONALCOMPUTER='Y'">
			<TR>
				<td class="midcolora">Total Insuring value of Personal Computer Desktop or Personal Computers Laptop categories is over $8,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--24. -->
	<xsl:template name="CHARGE_OFF_PRMIUM">
		<xsl:if test="CHARGE_OFF_PRMIUM='Y'">
			<TR>
				<td class="midcolora">Is a Rollover Policy.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--25. -->
	<xsl:template name="JEWELLERYITEM_PICTURE">
		<xsl:if test="JEWELLERYITEM_PICTURE='Y'">
			<TR>
				<td class="midcolora">Insuring Value of Jewelry Category is exceeds $10,000 and Picture Attached.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--26. -->
	<xsl:template name="JEWELLERYITEM_APPRAISAL">
		<xsl:if test="JEWELLERYITEM_APPRAISAL='Y'">
			<TR>
				<td class="midcolora">Insuring Value of Jewelry Category is exceeds $5,000 and No Appraisal/Bill of Sale Attached.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--27. -->
	<xsl:template name="BREAKAGEITEM">
		<xsl:if test="BREAKAGEITEM='Y'">
			<TR>
				<td class="midcolora">Insuring Value of Fine Arts with Breakage or Fine Arts without Breakage Category is exceeds $5,000 and No Appraisal/Bill of Sale Attached.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--28. -->
	<xsl:template name="CENT_BURG_FIRE_ALARM_CERT_ATTACHED">
		<xsl:if test="CENT_BURG_FIRE_ALARM_CERT_ATTACHED='Y'">
			<TR>
				<td class="midcolora">Central Stations Burglary and Fire Alarm System Checked but No Alarm Certificate Attached.</td>
			</TR>
		</xsl:if>
	</xsl:template>	
	<!-- Added by Charles on 20-Oct-09 for Itrack 6586-->
	<xsl:template name="PROT_DEVC_ALARM_CERT_ATTACHED">
		<xsl:if test="PROT_DEVC_ALARM_CERT_ATTACHED='Y'">
			<TR>
				<td class="midcolora">Protective Devices Checked but No Alarm Certificate Attached.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Added till here -->
	<!--Cameras (Non-Professional)(above $15,000) -->
	<xsl:template name="SCH_CAMERAS_NON_PROFESNL">
		<xsl:if test="SCH_CAMERAS_NON_PROFESNL='Y'">
			<TR>
				<td class="midcolora">Scheduled Cameras (Non-Professional) exceeds $15,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>	
	<!--Furs (above $30,000) -->
	<xsl:template name="SCH_FURS_HO61">
		<xsl:if test="SCH_FURS_HO61='Y'">
			<TR>
				<td class="midcolora">Scheduled Furs exceed $30,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--Guns (above $4,000) -->
	<xsl:template name="SCH_GUNS_HO61">
		<xsl:if test="SCH_GUNS_HO61='Y'">
			<TR>
				<td class="midcolora">Scheduled Guns exceed $4,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--Jewelry (above $30,000) -->
	<!-- Itrack 6455 -->
	<!--<xsl:template name="SCH_JEWELRY_HO61">
		<xsl:if test="SCH_JEWELRY_HO61='Y'">
			<TR>
				<td class="midcolora">Scheduled Jewelry exceed $30,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>-->
	<xsl:template name="SCH_JEWELRY_HO61_EXCT_TEN">
		<xsl:if test="SCH_JEWELRY_HO61_EXCT_TEN='Y'">
			<TR>
				<td class="midcolora">Scheduled Jewelry exceeds $10,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="SCH_JEWELRY_HO61_EXCT_TWO">
		<xsl:if test="SCH_JEWELRY_HO61_EXCT_TWO='Y'">
			<TR>
				<td class="midcolora">Scheduled Jewelry has a value of $2,000 or more and Appraisal/Bill of Sale not attached.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="SCH_JEWELRY_HO61_EXCT_TEN_PIC">
		<xsl:if test="SCH_JEWELRY_HO61_EXCT_TEN_PIC='Y'">
			<TR>
				<td class="midcolora">Scheduled Jewelry has a value of $10,000 or more and Picture not attached.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	
	<!--Musical (Non-Professional) (above $15,000) -->
	<xsl:template name="SCH_MUSICAL_INSTRMNT_HO61">
		<xsl:if test="SCH_MUSICAL_INSTRMNT_HO61='Y'">
			<TR>
				<td class="midcolora">Scheduled Musical (Non-Professional) exceeds $15,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--Silver (above $15,000) -->
	<xsl:template name="SCH_SILVER_HO61">
		<xsl:if test="SCH_SILVER_HO61='Y'">
			<TR>
				<td class="midcolora">Scheduled Silver exceeds $15,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--Stamps (above $10,000) -->
	<xsl:template name="SCH_STAMPS_HO61">
		<xsl:if test="SCH_STAMPS_HO61='Y'">
			<TR>
				<td class="midcolora">Scheduled Stamps exceed $10,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--Rare Coins (above $10,000) -->
	<xsl:template name="SCH_RARECOINS_HO61">
		<xsl:if test="SCH_RARECOINS_HO61='Y'">
			<TR>
				<td class="midcolora">Scheduled Rare Coins exceed $10,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--Fine Arts without Breakage (above $30,000) -->
	<xsl:template name="SCH_FINEARTS_WO_BREAK_HO61">
		<xsl:if test="SCH_FINEARTS_WO_BREAK_HO61='Y'">
			<TR>
				<td class="midcolora">Scheduled Fine Arts without Breakage exceeds $30,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--Fine Arts with Breakage (above $30,000) -->
	<xsl:template name="SCH_FINEARTS_BREAK_HO61">
		<xsl:if test="SCH_FINEARTS_BREAK_HO61='Y'">
			<TR>
				<td class="midcolora">Scheduled Fine Arts with Breakage exceeds $30,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Added by Charles on 6-Oct-09 for Itrack 6488-->
	<xsl:template name="SCH_FINEARTS_HO61">
		<xsl:if test="SCH_FINEARTS_HO61='Y'">
			<TR>
				<td class="midcolora">Fine Arts Item(s) exceeds $2,000 with no Appraisal/Bill of Sale.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--Major Violations -->
	<xsl:template name="WATER_MAJOR_VIOLATION">
		<xsl:if test="WATER_MAJOR_VIOLATION='Y'">
			<TR>
				<td class="midcolora">Major violations are 6 or more points in 5 years</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--Major Violations At RENEWAL-->
	<xsl:template name="RENEW_WATER_MAJOR_VIOLATION">
		<xsl:if test="RENEW_WATER_MAJOR_VIOLATION='Y'">
			<TR>
				<td class="midcolora">Major violations are 9 or more points in 5 years</td>
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
	<xsl:template name="CREDIT_CARD">
		<xsl:if test="CREDIT_CARD='Y'">
			<tr>
				<td class="midcolora">Complete First Name and Last Name and Card Type and Card CVV/CCV # and Credit Card # and Valid To (Month/Year) in Billing Info.</td>
			</tr>
		</xsl:if>
	</xsl:template>
	<!-- Effective Date-->
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
	<!--End Billing Info -->
	<!--24. -->
	<xsl:template name="PRIMARY_HEAT_TYPE">
		<xsl:if test="PRIMARY_HEAT_TYPE='Y'">
			<TR>
				<td class="midcolora">Primary Heat Type(Coal Professionally/Non Professionally Installed , Wood Stove - Professionally/Non Professionally Installed , Pellet/Corn Burner , Stove Fireplace Insert ,Add on Furnace).</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="SUBURBAN_CLASS">
		<xsl:if test="SUBURBAN_CLASS='Y'">
				<td class="midcolora">Suburban Class Credit Applied - meets all Criteria.</td>
		</xsl:if>
		<xsl:if test="contains(SUBURBAN_CLASS,'~')">
			<TR>
				<td class="midcolora"><b>Suburban Discount has been applied however does not meet the following Criteria(s):</b></td>
			</TR>		
		</xsl:if>
		<xsl:if test="contains(SUBURBAN_CLASS,'PROT_CLASS')">
			<TR>
				<td class="midcolora">Fire Protection Class must be 8B or 9.</td>
			</TR>
		</xsl:if>
		<xsl:if test="contains(SUBURBAN_CLASS,'RPMR200')">
			<TR>
				<td class="midcolora">Replacement Value &amp; Insurance Amount must be $200,000 or greater.</td>
			</TR>
		</xsl:if>
		<xsl:if test="contains(SUBURBAN_CLASS,'RPMRNOTEQ')">
			<TR>
				<td class="midcolora">Replacement Value &amp; Insurance Amount must be the same.</td>
			</TR>
		</xsl:if>
		<xsl:if test="contains(SUBURBAN_CLASS,'DWELL20')">
			<TR>
				<td class="midcolora">Year Built not within the last 20 years.</td>
			</TR>
		</xsl:if>
		<xsl:if test="contains(SUBURBAN_CLASS,'MILES5')">
			<TR>
				<td class="midcolora"># of Miles from Fire Station must be 5 or less.</td>
			</TR>
		</xsl:if>
		<xsl:if test="contains(SUBURBAN_CLASS,'SLDHEATDEV')">
			<TR>
				<td class="midcolora">Dwelling contains a Solid Fuel Heating Device.</td>
			</TR>
		</xsl:if>
		<xsl:if test="contains(SUBURBAN_CLASS,'MODMANHOME')">
			<TR>
				<td class="midcolora">Residence is a modular or manufacture home.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="SUBURBAN_RENEWAL_REFERAL">
		<xsl:if test="SUBURBAN_RENEWAL_REFERAL='Y'">
			<TR>
				<td class="midcolora">The Suburban Class Discount was given last term but now no longer eligible.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="RV_TYPE">
		<xsl:if test="RV_TYPE='Y'">
			<TR>
				<td class="midcolora">Recreational vehicle is  ATV/4-6 wheeler and  Snowmobiles</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="RV_USED_IN_RACE_SPEED">
		<xsl:if test="RV_USED_IN_RACE_SPEED='Y'">
			<TR>
				<td class="midcolora">Used to participate in any race or speed contest</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="RV_HORSE_POWER">
		<xsl:if test="RV_HORSE_POWER='Y'">
			<TR>
				<td class="midcolora">HP/CCs lies between 600 cc to 800 cc</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="RV_HORSE_POWER_OVER_800">
		<xsl:if test="RV_HORSE_POWER_OVER_800='Y'">
			<TR>
				<td class="midcolora">HP/CCs is more than 800 cc</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="RV_RV_COV_SELC">
		<xsl:if test="RV_COV_SELC='Y'">
			<TR>
				<td class="midcolora">Please select atleast one coverage (Physical Damage or Liability Coverages).</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--. An insured that regularly provides day care services to a person or persons other than insured. -->
	<xsl:template name="PROVIDE_HOME_DAY_CARE">
		<xsl:choose>
			<xsl:when test="PROVIDE_HOME_DAY_CARE='Y'">
				<TR>
					<td class="midcolora">Business is conducted on Premises (Including day or child care).</td>
				</TR>
			</xsl:when>
		</xsl:choose>
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
					<td class="midcolora">Selected(entered) Policy in Multipolicy Discount Description is(are) no eligible for discount.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--13 June 2006-->
	<xsl:template name="PIC_OF_LOC">
		<xsl:if test="PIC_OF_LOC='Y'">
			<TR>
				<td class="midcolora">Picture of location is not attached.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="PROPRTY_INSP_CREDIT">
		<xsl:if test="PROPRTY_INSP_CREDIT='Y'">
			<TR>
				<td class="midcolora">Property Inspection/Cost Estimator is not attached.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="ROOF_UPDATE">
		<xsl:if test="ROOF_UPDATE='Y'">
			<TR>
				<td class="midcolora">The home is over 25 year old and wiring, roofing, plumbing, or heating have not been updated.</td> <!-- Itrack 6454 -->
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="ROOF_UPDATE_YEAR">
		<xsl:if test="ROOF_UPDATE_YEAR='Y'">
			<TR>
				<td class="midcolora"> Home is over 25 years old and roof, plumbing, wiring or heating have not been updated in the last 10 years.</td><!-- Itrack 6464 -->
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Added by Charles on 26-Nov-09 for Itrack 6679 -->
	<xsl:template name="ROOF_UPDATE_PREMIER">
		<xsl:if test="ROOF_UPDATE_PREMIER='Y'">
			<TR>
				<td class="midcolora">The home is over 25 year old and wiring, roofing, plumbing, or heating have not been updated.</td> <!-- Changed, Charles(15-Dec-09), Itrack 6679 -->
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="ROOF_UPDATE_YEAR_PREMIER">
		<xsl:if test="ROOF_UPDATE_YEAR_PREMIER='Y'">
			<TR>
				<td class="midcolora">Home is over 25 years old and roof, plumbing, wiring or heating have not been updated in the last 10 years.</td> <!-- Changed, Charles(15-Dec-09), Itrack 6679 -->
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Added till here -->
	<!--Location type-->
	<xsl:template name="LOCATION_TYPE">
		<xsl:variable name="POLICY_NAME" select="POLICY_NAME"></xsl:variable>
		<xsl:if test="LOCATION_TYPE='Y'">
			<TR>
				<td class="midcolora">Policy type is <xsl:value-of select="$POLICY_NAME"></xsl:value-of> and location is Seasonal.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Added by Charles on 8-Dec-09 for Itrack 6818 -->
	<xsl:template name="LOCATION_STATE_APP_STATE">
		<xsl:if test="LOCATION_STATE_APP_STATE='Y'">
			<TR>
				<td class="midcolora">Location State is not same as that of application state.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Added till here -->
	<!--No of weeks Dwelling Info:-->
	<xsl:template name="NO_WEEKS_RENTED">
		<xsl:if test="NO_WEEKS_RENTED='Y'">
			<TR>
				<td class="midcolora">Location is Seasonal and number of weeks rented is greater than 4.</td> <!-- Changed message by Charles on 5-Oct-09 for Itrack 6491 -->
			</TR>
		</xsl:if>
	</xsl:template>
	<!--13 June 2006-->
	<xsl:template name="REPLACEMENT_COST_YEAR">
		<xsl:if test="REPLACEMENT_COST_YEAR='Y'">
			<TR>
				<td class="midcolora">Year built is prior to 1940 and Market Value is less than 80% of the Replacement Cost.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--19 June 2006-->
	<xsl:template name="HO_CLAIMS">
		<xsl:if test="HO_CLAIMS='Y'">
			<TR>
				<td class="midcolora">For Prior Losses, HO claims in the last 3 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--19 June 2006-->
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
	<xsl:template name="DEDUCTIBLE_DESCRIPTION">
		<xsl:for-each select="DEDUCT">
			<xsl:if test="DEDUCT_DES!='' ">
				<TR>
					<td class="midcolora">
					    Deductible selected for '<xsl:value-of select="DEDUCT_DES" />' is ineligible.
					</td>
				</TR>
			</xsl:if>
		</xsl:for-each>
	</xsl:template>
	<!--MAR 07 Grandfather Adddeductible Carry passengers others Rejected case-->
	<xsl:template name="ADDDEDUCTIBLE_DESCRIPTION">
		<xsl:for-each select="ADDDEDUCT">
			<xsl:if test="ADDDEDUCT_DES!='' ">
				<TR>
					<td class="midcolora">
					    Additiional Deductible selected for '<xsl:value-of select="ADDDEDUCT_DES" />' is ineligible.
					</td>
				</TR>
			</xsl:if>
		</xsl:for-each>
	</xsl:template>
	<!--03 May 2007 -->
	<xsl:template name="NO_HORSES">
		<xsl:if test="NO_HORSES='Y'">
			<TR>
				<td class="midcolora"># of Horses is greater than 3.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--Customer Inactive-->
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
	<!-- Trailer Information. -->
	<xsl:template name="TRAILER_DEDUCTIBLE">
		<xsl:if test="TRAILER_DEDUCTIBLE='Y'">
			<TR>
				<td class="midcolora">Selected Trailer Deductible is ineligible.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	
	<!-- Added by Charles on 27-Jul-09 for Itrack 6176 -->
	<xsl:template name="RV_HORSE_POWER_OVER_750">
		<xsl:if test="RV_HORSE_POWER_OVER_750='Y'">
			<TR>
				<td class="midcolora">HP/CCs is more than 750 cc</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Added till here -->
	<!-- Added by Charles on 9-Sep-09 for Itrack 6370 -->
	<xsl:template name="MORE_MORTAGAGEE">
		<xsl:if test="MORE_MORTAGAGEE='Y'">
			<TR>
				<td class="midcolora">3 or more Additional Interests on a location</td><!-- Changed by Charles on 5-Oct-09 for Itrack 6370 -->
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Added till here -->
	<!--End Reffered Templates -->
	<!-- ======================================= Watercraft secttion ================================ -->
	<!-- *************** Templates for showing refered rules messages **************** -->
	<!--1 -->
	<xsl:template name="BOAT_LENGTH">
		<xsl:if test="LENGTH='Y'">
			<TR>
				<td class="midcolora">Length exceeds 40 feet.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--2 The maximum length must not exceed 26 feet-->
	<xsl:template name="MAX_LENGTH_BOAT">
		<xsl:if test="MAX_LENGTH='Y'">
			<TR>
				<td class="midcolora">Maximum length of agreed value boats exceeds 26 feet.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--3 -->
	<xsl:template name="MAX_INSURING_VALUE">
		<xsl:if test="INSURING_VALUE='Y'">
			<TR>
				<td class="midcolora">Agreed Value boats cannot exceed $75,000 or be higher than the high retail value established in the BUC book. </td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--4 The maximum age of the boat must not exceed 20 years.-->
	<xsl:template name="MAX_AGE">
		<xsl:if test="AGE='Y'">
			<TR>
				<td class="midcolora">Age of the boat exceeds 20 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--5 Boats, including motor, with a total value over $200,000.-->
	<xsl:template name="MAX_INSURING_WMOTOR">
		<xsl:if test="MAX_INSURING_WMOTOR='Y'">
			<TR>
				<td class="midcolora">Total value of boats exceeds $200,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--6  Any applicant whose watercraft coverage has been refused, canceled or non-renewed in the last 12 months by any insurer.-->
	<xsl:template name="COVERAGE_DECLINED">
		<xsl:if test="COVERAGE_DECLINED='Y'">
			<TR>
				<td class="midcolora">Watercraft coverage has been refused, canceled or non-renewed in the last 12 months.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--7 Must be insured for 100% of market value. -->
	<xsl:template name="MUST_INSURED">
		<xsl:if test="COVERAGE='Y'">
			<TR>
				<td class="midcolora">Boat must be insured for 100% of market value.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--MAY 05 ref.rules GEN INFO-->
	<xsl:template name="PHY_MENTL_CHALLENGED">
		<xsl:if test="PHY_MENTL_CHALLENGED='Y'">
			<TR>
				<td class="midcolora">Operator physically or mentally impaired.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="DRIVER_SUS_REVOKED">
		<xsl:if test="DRIVER_SUS_REVOKED='Y'">
			<TR>
				<td class="midcolora">One of the operators had a license suspended, restricted or revoked in the last 5 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="CONVICTED_ACCIDENT">
		<xsl:if test="IS_CONVICTED_ACCIDENT='Y'">
			<TR>
				<td class="midcolora">One or more operators convicted of a major violation in the preceding 5 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="DRINK_DRUG_VOILATION">
		<xsl:if test="DRINK_DRUG_VOILATION='Y'">
			<TR>
				<td class="midcolora">Drinking or drug related violation in last 5 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="ANY_LOSS_THREE_YEARS">
		<xsl:if test="ANY_LOSS_THREE_YEARS='Y'">
			<TR>
				<td class="midcolora">Suffered losses with this or other vessels.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--MAY 05 ref.rules GEN INFO-->
	<!--MAY 06 EFFECTIVE DATE-->
	<xsl:template name="EFFECTIVE_DATE">
		<xsl:if test="IS_EFFECTIVE_DATE='Y'">
			<TR>
				<td class="midcolora">Boat (other than Outboards, Jet Ski, Mini Jet Boat or Wave runner ) over 15 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="EFFECTIVE_DATE_OTHER">
		<xsl:if test="IS_EFFECTIVE_DATE_OTHER='Y'">
			<TR>
				<td class="midcolora">Jet Ski, Mini Jet Boat or Wave runner over 10 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--MAY 06 EFFECTIVE DATE-->
	<!--MAY 10 Operator age check-->
	<xsl:template name="OP_UNDER_21">
		<xsl:if test="IS_OP_UNDER_21='Y'">
			<TR>
				<td class="midcolora">Principal operator(s) under 21 year old.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="OP_UNDER_16">
		<xsl:if test="IS_OP_UNDER_16='Y'">
			<TR>
				<td class="midcolora">Occasional operator(s) under 16 year old who have not attended the water safety course.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--MAY 10 Operator age check-->
	<!--MAY 11 Wolverine Insured-->
	<xsl:template name="WOLVERINE_INSURE">
		<xsl:if test="WOLVERINE_INSURE='Y'">
			<TR>
				<td class="midcolora">Either Wolverine does not insure home policy or this application does not have any boat type other than Jetski or Waverunner.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--MAY 11 Wolverine Insured-->
	<!-- *************** Templates for showing Rejected rules messages **************** -->
	<!--1  Boats with speeds in excess of 65 miles per hour.-->
	<xsl:template name="MAX_SPEED">
		<xsl:if test="MAX_SPEED='Y'">
			<TR>
				<td class="midcolora">Boat has speed more than 65 miles per hour.</td>
			</TR>
		</xsl:if>
	</xsl:template>	
	<!--2 Boat when registered in a state other than Indiana, Michigan, or Wisconsin. -->
	<xsl:template name="STATE_REG">
		<xsl:if test="STATE_REG='Y'">
			<TR>
				<td class="midcolora">Boat is registered in a state other than Indiana, Michigan, or Wisconsin.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--3 Charter boats; boats with a paid crew; used commercially in any way; or rented. -->
	<xsl:template name="RENTED_OTHERS">
		<xsl:if test="IS_RENTED_OTHERS='Y'">
			<TR>
				<td class="midcolora">Boat(s) rented to others.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--4 Boat owned by a corporation. -->
	<xsl:template name="REGISTERED_OTHERS">
		<xsl:if test="IS_REGISTERED_OTHERS='Y'">
			<TR>
				<td class="midcolora">Boat registered to someone other than applicant.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--5 Convicted of a Major violation in the preceding 5 years. -->
	<!--MAY 05 commented for Rejected cases-->
	<!--<xsl:template name="CONVICTED_ACCIDENT">
		<xsl:if test="IS_CONVICTED_ACCIDENT='Y'">
			<TR>
				<td class="midcolora">Convicted of a Major violation in the preceding 5 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>-->
	<!--6 Boat when an operator has an ineligible automobile driving record.Same rules as auto program apply (5 points in 3 years , 5 years for major violations for new business, and 8 points for renewals) -->
	<xsl:template name="SD_POINTS">
		<xsl:if test="SD_POINTS='Y'">
			<TR>
				<td class="midcolora">Boat when an operator has an ineligible Watercraft driving record.(more than 5 points in 3 years).</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Minor violations at Renewal. -->
	<xsl:template name="RENEW_SD_POINTS">
		<xsl:if test="RENEW_SD_POINTS='Y'">
			<TR>
				<td class="midcolora">Operator has an ineligible Watercraft driving record.(9 or more than 9 points in 3 years).</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- 7 Any racing or performance models other than sailboats less than 26 feet. -->
	<xsl:template name="PARTICIPATE_RACE">
		<xsl:if test="IS_PARTICIPATE_RACE='Y'">
			<TR>
				<td class="midcolora">Boat other than sailboat is used for race, speed or fishing contest other than a Sailboat under 26ft.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--May 05 06 Added Rejected cases-->
	<xsl:template name="BOAT_AMPHIBIOUS">
		<xsl:if test="ANY_BOAT_AMPHIBIOUS='Y'">
			<TR>
				<td class="midcolora">One of the boats falls under the category of Hydrofoils, Swamp Buggies, Collapsible, Experimental, Converted Military Craft Single Passenger or is Amphibious.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--MAY 06 Carry passengers others Rjected case-->
	<xsl:template name="CARRY_PASSENGER_FOR_CHARGE">
		<xsl:if test="CARRY_PASSENGER_FOR_CHARGE='Y'">
			<TR>
				<td class="midcolora">Boat used to carry passengers for a charge.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--18 Sep 2007 Principal operator Rjected case-->
	<xsl:template name="PRINCIPLE_OPERATOR">
		<xsl:if test="PRINCIPLE_OPERATOR='Y'">
			<TR>
				<td class="midcolora">Either Boat has no principal operator or  more than one principal operator.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	
	<xsl:template name="BOAT_COOWNED">
		<xsl:if test="IS_BOAT_COOWNED='Y'">
			<TR>
				<td class="midcolora">Boat Co-owned.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="ANY_BOAT_RESIDENCE">
		<xsl:if test="ANY_BOAT_RESIDENCE='Y'">
			<TR>
				<td class="midcolora">Boat used as a Residence.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--MAY 06 Carry passengers others Rejected case-->
	<!--Trailer Rule-->
	<xsl:template name="JETSKI_TYPE_TRAILER">
		<xsl:if test="JETSKI_TYPE_TRAILER='Y'">
			<TR>
				<td class="midcolora">Trailer Info > Jet Ski type trailer can only be attached with Jet Ski type boat.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- =============================== Watercraft section ====================================== -->
	<!-- Added by Charles on 26-Oct-09 for Itrack 6634 -->
	<xsl:template name="DWELLINGINFOTOP">	
	<xsl:choose>
		<xsl:when test="((user:IsApplicationAcceptable(1) = 0) and (user:DwellingTopCount(1) = 0)) or ((user:IsApplicationAcceptable(1) = 1) and (user:CheckRefer(1) = 0))">
			<tr>
				<td class="pageheader">For Dwelling Information:</td>
			</tr>
			<tr>
				<td>
					<xsl:call-template name="DWELLINGTOP" />
				</td>
			</tr>
		</xsl:when>		
	</xsl:choose>							
	</xsl:template>
	<!-- Added till here -->
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