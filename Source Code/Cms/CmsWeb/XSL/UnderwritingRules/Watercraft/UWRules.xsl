<!-- ==================================================================================================
File Name			:	UWRules.xsl
Purpose				:	Watercraft rules implementation
Name				:	Praveen
Date				:	22 Sep.2005  
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
			<!--Modified 8 dec 2005(start)-->
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
			<!--Modified 8 dec 2005(End)-->
			<xsl:call-template name="WATERCRAFT_REJECTION_CASES" />
			<xsl:call-template name="WATERCRAFT_REFERRED_CASES" />
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
						<!--Rejected Messages (WATERCRAFT)-->
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/APPLICATIONS/APPLICATION"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/BOATS/BOAT"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/TRAILERS/TRAILER"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/OPERATORS/OPERATOR"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO"></xsl:apply-templates>
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
						<!--Referred Messages (WATERCRAFT)-->
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/APPLICATIONS/APPLICATION"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/BOATS/BOAT"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/TRAILERS/TRAILER"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/OPERATORS/OPERATOR"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO"></xsl:apply-templates>
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
	<!--Checking for rejected cases -->
	<xsl:template name="WATERCRAFT_REJECTION_CASES">
		<!--  1  -->
		<xsl:call-template name="IS_RJ_MAX_SPEED" />
		<!--  2  -->
		<xsl:call-template name="IS_RENTED_OTHERS" />
		
		<!--  3  -->
		<!-- Principle Operator-->
		<xsl:call-template name="IS_RJ_PRINCIPLE_OPERATOR" />
		
		<!-- 3 Move to Refered Case 03 may 2007-->
		<!--<xsl:call-template name="IS_REGISTERED_OTHERS" />-->
		<!--  4  -->
		<!--MAY 06 Commented-->
		<!--<xsl:call-template name="IS_CONVICTED_ACCIDENT" />-->
		<!--MAY 06 Commented-->
		<!-- 5 -->
		<!--MAY 06 Commented-->
		<!--<xsl:call-template name="IS_RJ_STATE_REGD" />-->
		<!--MAY 06 Commented-->
		<!-- 6 -->
		<xsl:call-template name="IS_RJ_RACING" />
		<!--  4  -->		
		<!--  7  May 5 06-->
		<xsl:call-template name="IS_BOAT_AMPHIBIOUS" />
		<!--MAY 06 carry passengers-->
		<!--  8  -->
		<xsl:call-template name="IS_CARRY_PASSENGER_FOR_CHARGE" />
		<!--MAY 09 commented-->
		<!--<xsl:call-template name ="IS_BOAT_COOWNED"/> -->
		<!--MAY 09 commented-->
		<xsl:call-template name="IS_ANY_BOAT_RESIDENCE" />
		<!--MAY 06 carry passengers-->
		<!--  9  -->
		<!-- <xsl:call-template name ="IS_RJ_DEGREE_CONVICTION"/> -->
		<!--xsl:call-template name="IS_RJ_COV" />
		<xsl:call-template name="IS_RJ_LIMIT" />
		<xsl:call-template name="IS_RJ_DEDUCT" />
		<Trailer Rule-->
		<xsl:call-template name="IS_RJ_JETSKI_TYPE_TRAILERINFO" />
		<!--Customer Inactive Rule -->
	    <xsl:call-template name="IS_RJ_INACTIVE_APPLICATION" />
	    <!--Agency Inactive 28th May 2007-->
		<xsl:call-template name="IS_RJ_INACTIVE_AGENCY" />
	</xsl:template>
	<!--Start Rejected Templates -->
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
	
	<!-- Principle Operator-->
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
	<!-- 3. Moved to Refered Case 03 May 2005-->
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
	<!--MAY 06 Commented for Rejected cases-->
	<!--<xsl:template name="IS_CONVICTED_ACCIDENT">
		<xsl:choose>
			<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/IS_CONVICTED_ACCIDENT='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>-->
	<!--MAY 06 Commented for Rejected cases-->
	<!-- 7. -->
	<!--MAY 06 Commented for Rejected cases-->
	<!--<xsl:template name="IS_RJ_STATE_REGD">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/STATE_REG='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>-->
	<!--MAY 06 Commented for Rejected cases-->
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
	<!--MAY 09 commented refer case-->
	<!--<xsl:template name="IS_BOAT_COOWNED">
	<xsl:choose>
    	<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/IS_BOAT_COOWNED='Y'">
    		<xsl:if test= "user:IsApplicationAcceptable(0) = 0"></xsl:if>
    	</xsl:when>
    	<xsl:otherwise>
    		<xsl:if test= "user:IsApplicationAcceptable(1) = 0"></xsl:if>
    	</xsl:otherwise>
	</xsl:choose>
</xsl:template>-->
	<!--MAY 09 refer case-->
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
	<!--Coverages 26 July 2006-->
	<xsl:template name="IS_RJ_COV">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/COV/COV_DES !=''  and INPUTXML/BOATS/BOAT/WOLVERINE_USER='N'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Limit 7 TH MAR 2007 -->
	<xsl:template name="IS_RJ_LIMIT">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/LIMIT/LIMIT_DES !=''  and INPUTXML/BOATS/BOAT/WOLVERINE_USER='N'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Deduct 7 TH MAR 2007 -->
	<xsl:template name="IS_RJ_DEDUCT">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/DEDUCT/DEDUCT_DES !=''  and INPUTXML/BOATS/BOAT/WOLVERINE_USER='N'">
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
	<!-- Customer inactinve -->
	<xsl:template name="IS_RJ_INACTIVE_APPLICATION">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/INACTIVE_APPLICATION ='Y'" >				
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
	<!--End Rejected Templates -->
	<!--  **********************************Start checking for referred Cases ***************************-->
	<xsl:template name="WATERCRAFT_REFERRED_CASES">
		<!-- 1. -->
		<!--<xsl:call-template name ="IS_RF_REFERRED1"/>-->
		<!-- 2. -->
		<xsl:call-template name="IS_RF_MAX_LEN" />
		<!-- 3. -->
		<xsl:call-template name="RF_MAX_LEN_BOAT" />
		<!-- 4. -->
		<xsl:call-template name="IS_MAX_INSURING_VALUE" />
		<!-- 5. -->
		<xsl:call-template name="IS_MAX_AGE" />
		<!-- 6. -->
		<xsl:call-template name="IS_MAX_INSURING_WMOTOR" />
		<!-- 7. -->
		<xsl:call-template name="IS_RF_COVERAGE_DECLINED" />
		<!-- 8 -->
		<xsl:call-template name="IS_CHARGE_OFF_PRMIUM" />
		<xsl:call-template name="IS_DFI_ACC_NO_RULE" />
		<xsl:call-template name="IS_CREDIT_CARD" />
		<!--25 June 2008-->
		<xsl:call-template name="IS_RF_HO_CLAIMS" />
		<xsl:call-template name="IS_TOTAL_PREMIUM_AT_RENEWAL" />
		<xsl:call-template name="IS_CLAIM_EFFECTIVE" />
		<!-- 9 -->
		<xsl:call-template name="IS_INSURED_MARKET_VALUE" />
		<!--May 06 ref case 1 GEN INFO-->
		<xsl:call-template name="IS_PHY_MENTL_CHALLENGED" />
		<xsl:call-template name="IS_DRIVER_SUS_REVOKED" />
		<xsl:call-template name="IS_CONVICTED_ACCIDENT" />
		<xsl:call-template name="IS_DRINK_DRUG_VOILATION" />
		<xsl:call-template name="IS_ANY_LOSS_THREE_YEARS" />
		<xsl:call-template name="IS_MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS" />
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
		<xsl:call-template name="IS_RF_COV" />
		<xsl:call-template name="IS_WATERCRAFT_REMOVE_SAILBOAT" />
		<!-- MAR 8 TH 2007-->
		<xsl:call-template name="IS_RF_LIMIT" />
		<xsl:call-template name="IS_RF_DEDUCT" />
		<!--MAY 11 Wolverine insured-->
		<!-- <xsl:call-template name ="IS_RF_REFERRED3"/>-->
		<!-- 4. -->
		<!--<xsl:call-template name ="IS_RF_REFERRED4"/>-->
		<!-- 5. 
		<xsl:call-template name ="IS_RF_COVERAGE_DECLINED"/>-->
		<!-- 6. -->
		<!--<xsl:call-template name ="IS_RF_REFERRED6"/>-->
		<!-- 7. -->
		<!--<xsl:call-template name ="IS_RF_REFERRED7"/>-->
		<!-- 8. -->
		<!--<xsl:call-template name ="IS_RF_REFERRED8"/>-->
		<!--Refer Coverage at Renewal Level ,10th Apr 2007 -->
		<xsl:call-template name="IS_RF_COPY_COVERAGE_AT_RENEWAL" />
		<!--Refered Case : 03 May 2007-->
		<xsl:call-template name="IS_REGISTERED_OTHERS" />
		<!--Refered Case : 26 June  2007-->
		<xsl:call-template name="IS_TRAILER_DEDUCTIBLE" />
		<!-- Major Violations-->
		<xsl:call-template name="IS_WATER_MAJOR_VIOLATION" />
		<!-- Major Violations At Renewal-->
		<xsl:call-template name="IS_RENEW_WATER_MAJOR_VIOLATION" />
		<!--  6  -->
		<xsl:call-template name="IS_SD_POINTS" />
		<!--  7  -->
		<xsl:call-template name="IS_RENEW_SD_POINTS" />
		<xsl:call-template name="IS_NEGATIVE_VIOLATION" />
	</xsl:template>
	<!--Start Referred Templates -->
	<!--<xsl:template name="IS_RF_REFERRED1">
	<xsl:choose>
    	<xsl:when test="INPUTXML/BOATS/BOAT/REFERRED1 = 'Y' ">
    		<xsl:if test= "user:CheckRefer(0) = 0"></xsl:if>
    	</xsl:when>
    	<xsl:otherwise>
    		<xsl:if test= "user:CheckRefer(1) = 0"></xsl:if>
    	</xsl:otherwise>
	</xsl:choose>
</xsl:template>-->
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
	<!--7 -->
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
	<!--25 June 2008-->
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
	<!-- 8-->
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
	<xsl:template name="IS_MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS">
		<xsl:choose>
			<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y'">
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
			<xsl:when test="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO/PARTICIPATE_RACE ='Y'">
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
	<!--1 MAR 2007 -->
	<xsl:template name="IS_WATERCRAFT_REMOVE_SAILBOAT">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/WATERCRAFT_REMOVE_SAILBOAT='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--10TH APR 2007 -->
	<xsl:template name="IS_RF_COPY_COVERAGE_AT_RENEWAL">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/COPY_COVERAGE_AT_RENEWAL='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--27 july Wolverine Usre-->
	<xsl:template name="IS_RF_COV">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/COV/COV_DES !='' and INPUTXML/BOATS/BOAT/WOLVERINE_USER='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--MAR 2007-->
	<xsl:template name="IS_RF_LIMIT">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/LIMIT/LIMIT_DES !='' and INPUTXML/BOATS/BOAT/WOLVERINE_USER='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template >
	<!--MAR 2007-->
	<xsl:template name="IS_RF_DEDUCT">
		<xsl:choose>
			<xsl:when test="INPUTXML/BOATS/BOAT/DEDUCT/DEDUCT_DES !='' and INPUTXML/BOATS/BOAT/WOLVERINE_USER='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template >
	<!--03 May 2007-->
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
	<!-- 4. Trailer Information-->
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
	<!-- SD POINTS (Minor Violation)-->	
	<xsl:template name="IS_SD_POINTS">
		<xsl:choose>
			<xsl:when test="INPUTXML/OPERATORS/OPERATOR/SD_POINTS ='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
					<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- SD POINTS at Renewal(Minor Violation)-->	
	<xsl:template name="IS_RENEW_SD_POINTS">
		<xsl:choose>
			<xsl:when test="INPUTXML/OPERATORS/OPERATOR/RENEW_SD_POINTS ='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
					<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_NEGATIVE_VIOLATION">
		<xsl:choose>
			<xsl:when test="INPUTXML/OPERATORS/OPERATOR/NEGATIVE_VIOLATION='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
					<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--
 <xsl:template name="IS_RF_REFERRED3">
	<xsl:choose>
    	<xsl:when test="INPUTXML/BOATS/BOAT/REFERRED3='Y'">
    		<xsl:if test= "user:CheckRefer(0) = 0"></xsl:if>
    	</xsl:when>
    	<xsl:otherwise>
    		<xsl:if test= "user:CheckRefer(1) = 0"></xsl:if>
    	</xsl:otherwise>
	</xsl:choose>
</xsl:template>-->
	<!--  4. -->
	<!--<xsl:template name="IS_RF_REFERRED4">
	<xsl:choose>
    	<xsl:when test="INPUTXML/BOATS/BOAT/REFERRED4='Y'">
    		<xsl:if test= "user:CheckRefer(0) = 0"></xsl:if>
    	</xsl:when>
    	<xsl:otherwise>
    		<xsl:if test= "user:CheckRefer(1) = 0"></xsl:if>
    	</xsl:otherwise>
	</xsl:choose>
</xsl:template>-->
	<!-- 6. 
<xsl:template name="IS_RF_REFERRED6">
	<xsl:choose>
    	<xsl:when test="INPUTXML/BOATS/BOAT/REFERRED6='Y'">
    		<xsl:if test= "user:CheckRefer(0) = 0"></xsl:if>
    	</xsl:when>
    	<xsl:otherwise>
    		<xsl:if test= "user:CheckRefer(1) = 0"></xsl:if>
    	</xsl:otherwise>
	</xsl:choose>
</xsl:template>-->
	<!-- 7. 
<xsl:template name="IS_RF_REFERRED8">
	<xsl:choose>
    	<xsl:when test="INPUTXML/BOATS/BOAT/REFERRED8='Y'">
    		<xsl:if test= "user:CheckRefer(0) = 0"></xsl:if>
    	</xsl:when>
    	<xsl:otherwise>
    		<xsl:if test= "user:CheckRefer(1) = 0"></xsl:if>
    	</xsl:otherwise>
	</xsl:choose>
</xsl:template>-->
	<!-- 8. 
<xsl:template name="IS_RF_REFERRED7">
	<xsl:choose>
    	<xsl:when test="INPUTXML/BOATS/BOAT/IS_RF_REFERRED7='Y'">
    		<xsl:if test= "user:CheckRefer(0) = 0"></xsl:if>
    	</xsl:when>
    	<xsl:otherwise>
    		<xsl:if test= "user:CheckRefer(1) = 0"></xsl:if>
    	</xsl:otherwise>
	</xsl:choose>
</xsl:template>-->
	<!--End Referred Templates -->
	<!--NEW TEMPLATES 08 dec 2005-->
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
					<xsl:when test="CHARGE_OFF_PRMIUM='Y' or DFI_ACC_NO_RULE='Y' or CREDIT_CARD='Y' or HO_CLAIMS='Y' or TOTAL_PREMIUM_AT_RENEWAL='Y' or CLAIM_EFFECTIVE='Y'">
						<xsl:call-template name="CHARGE_OFF_PRMIUM"></xsl:call-template>
						<xsl:call-template name="HO_CLAIMS" />
						<xsl:call-template name="DFI_ACC_NO_RULE"></xsl:call-template>
						<xsl:call-template name="CREDIT_CARD" />
						<xsl:call-template name="TOTAL_PREMIUM_AT_RENEWAL" />
						<xsl:call-template name="CLAIM_EFFECTIVE" />												
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Boat Info -->
	<xsl:template match="INPUTXML/BOATS/BOAT">
		<xsl:choose>
			<!-- Rejected messages -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<!--xsl:when test="MAX_SPEED='Y' or PRINCIPLE_OPERATOR='Y' or LIMIT/LIMIT_DES!='' or COV/COV_DES!='' or DEDUCT/DEDUCT_DES!='' "-->
					<xsl:when test="MAX_SPEED='Y' or PRINCIPLE_OPERATOR='Y' ">
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
										<tr>
											<td class="pageheader" width="18%">Boat Type:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="BOATTYPE" />
											</td>
											<td class="pageheader" width="18%">Length:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="INT_MAX_LENGTH" />Ft.
													<xsl:value-of select="INT_INCH" />Inches
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</tr>
						<xsl:call-template name="MAX_SPEED" />
						<xsl:call-template name="PRINCIPLE_OPERATOR" />
						<!--xsl:call-template name="COV_DESCRIPTION" />
						<xsl:call-template name="LIMIT_DESCRIPTION" />
						<xsl:call-template name="DEDUCTIBLE_DESCRIPTION" />
						
						<MAY 06 removed the State reg from Rejected case>
						<xsl:call-template name="STATE_REG" />
						<MAY 06 removed the Participate race from Rejected case>
						<xsl:call-template name="PARTICIPATE_RACE" /-->
						<Rules>
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
							<!-- ********************** Start for Indiana only *************************** -->
							<!-- <xsl:if test="INPUTXML/VEHICLES/VEHICLE/STATE_ID = 14 ">
							<RULE RULEID="13" DESC="">
								<xsl:call-template name="REJECT13" />
							</RULE>
						</xsl:if> -->
							<!-- **********************End  for Indiana only *************************** --></Rules>
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!--*********************** Checking for referred rules (Messages)********************* -->
				<!-- Boat Info -->
				<xsl:choose>
					<xsl:when test="LENGTH='Y' or MAX_LENGTH='Y' or INSURING_VALUE='Y' or AGE='Y'
					 or MAX_INSURING_WMOTOR='Y' or COVERAGE='Y' or STATE_REG='Y' or IS_EFFECTIVE_DATE='Y' or IS_EFFECTIVE_DATE_OTHER='Y' or IS_OP_UNDER_21='Y' or IS_OP_UNDER_16='Y' or WOLVERINE_INSURE='Y'
					 or COV/COV_DES!='' or LIMIT/LIMIT_DES!='' or DEDUCT/DEDUCT_DES!='' or WATERCRAFT_REMOVE_SAILBOAT='Y' or COPY_COVERAGE_AT_RENEWAL='Y'">
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
										<tr>
											<td class="pageheader" width="18%">Boat Type:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="BOATTYPE" />
											</td>
											<td class="pageheader" width="18%">Length:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="INT_MAX_LENGTH" />Ft.
													<xsl:value-of select="INT_INCH" />Inches
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
						<xsl:call-template name="COV_DESCRIPTION" />
						<!-- MAR 6 2007 -->
						<xsl:call-template name="LIMIT_DESCRIPTION" />
						<xsl:call-template name="DEDUCTIBLE_DESCRIPTION" />
						<!-- MAR 1 2007-->
						<xsl:call-template name="WATERCRAFT_REMOVE_SAILBOAT" />
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
						<!-- start Rules added from Ebix Application Process Document or Additional Requests -->
						<xsl:call-template name="COPY_COVERAGE_AT_RENEWAL" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Operator Detail messages -->
	<xsl:template match="INPUTXML/OPERATORS/OPERATOR">
		<xsl:choose>
			<!-- Rejected Operator Detail -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="N='Y'">
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
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred Driver Detail -->
				<xsl:choose>
					<xsl:when test="WATER_MAJOR_VIOLATION='Y' or RENEW_WATER_MAJOR_VIOLATION='Y' or SD_POINTS='Y' or RENEW_SD_POINTS='Y' or NEGATIVE_VIOLATION='Y'">
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
						<xsl:call-template name="SD_POINTS"></xsl:call-template>
						<xsl:call-template name="RENEW_SD_POINTS"></xsl:call-template>
						<xsl:call-template name="NEGATIVE_VIOLATION"></xsl:call-template>
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- General Info -->
	<xsl:template match="INPUTXML/WATERCRAFTGENINFOS/WATERCRAFTGENINFO">
		<xsl:choose>
			<!-- Rejected General Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="IS_RENTED_OTHERS='Y' or ANY_BOAT_AMPHIBIOUS='Y' or CARRY_PASSENGER_FOR_CHARGE='Y' or ANY_BOAT_RESIDENCE='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Underwriting Questions:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="RENTED_OTHERS" />
						<!--<xsl:call-template name="REGISTERED_OTHERS" />-->
						<!--MAY 06 commented for rejected cases-->
						<!--<xsl:call-template name="CONVICTED_ACCIDENT" />-->
						<!--MAY 06 commented for rejected cases-->
						<!--May 05-->
						<xsl:call-template name="BOAT_AMPHIBIOUS" />
						<!--May 05-->
						<!--MAY 06 Carry passengers-->
						<xsl:call-template name="CARRY_PASSENGER_FOR_CHARGE" />
						<!--MAY 09 commented-->
						<!--<xsl:call-template name="BOAT_COOWNED" />-->
						<!--MAY 09 commented-->
						<xsl:call-template name="ANY_BOAT_RESIDENCE" />
						<!--MAY 06 Carry passengers-->
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred General Info -->
				<xsl:choose>
					<xsl:when test="COVERAGE_DECLINED='Y' or PHY_MENTL_CHALLENGED='Y' or DRIVER_SUS_REVOKED='Y' or IS_CONVICTED_ACCIDENT ='Y' or DRINK_DRUG_VOILATION='Y' or ANY_LOSS_THREE_YEARS='Y' or IS_BOAT_COOWNED='Y' or PARTICIPATE_RACE='Y' or IS_REGISTERED_OTHERS='Y' or MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Underwriting Questions:</td>
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
						<xsl:call-template name="MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS"></xsl:call-template>
						<!--MAY 05-->
						<!--MAY 09-->
						<xsl:call-template name="BOAT_COOWNED" />
						<!--MAY 09-->
						<xsl:call-template name="PARTICIPATE_RACE" />
						<!--May 03 2007-->
						<xsl:call-template name="REGISTERED_OTHERS" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Trailer Infomation -->
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
	<!-- -->
	<!-- *************** Templates for showing refered rules messages **************** -->
	<!--1 Boats over 40 feet in length. -->
	<xsl:template name="BOAT_LENGTH">
		<xsl:if test="LENGTH='Y'">
			<TR>
				<td class="midcolora">Length is over 40 feet.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--2 The maximum length must not exceed 26 feet. -->
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
	<!--4 The maximum age of the boat must not exceed 20 years. -->
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
				<td class="midcolora">Must be insured for 100% of market value.</td>
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
	<xsl:template name="MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS">
		<xsl:if test="MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y'">
			<TR>
				<td class="midcolora">Selected(entered) Policy in Multipolicy Discount Description is(are) no eligible for discount.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--MAY 05 ref.rules GEN INFO-->
	<!--MAY 06 EFFECTIVE DATE-->
	<xsl:template name="EFFECTIVE_DATE">
		<xsl:if test="IS_EFFECTIVE_DATE='Y'">
			<TR>
				<td class="midcolora">Boat (other than Outboards, Jet Ski, Jetski(w/Lift Bar), Mini Jet Boat or Wave runner ) over 15 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="EFFECTIVE_DATE_OTHER">
		<xsl:if test="IS_EFFECTIVE_DATE_OTHER='Y'">
			<TR>
				<td class="midcolora">Jet Ski, Jetski(w/Lift Bar) , Mini Jet Boat or Wave runner over 10 years.</td>
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
				<td class="midcolora">Either Wolverine does not insure home policy or this application does not have any boat type other than Jetski or Jetski (w/Lift Bar) or Waverunner.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="WATERCRAFT_REMOVE_SAILBOAT">
		<xsl:if test="WATERCRAFT_REMOVE_SAILBOAT='Y'">
			<TR>
				<td class="midcolora">Boat type is  sailboat or sailboat w/outboard , used for race, speed or fishing contest and Remove Sailboat Racing Exclusion is yes. 
				</td>
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
				<td class="midcolora">Boat registered in a state other than Indiana, Michigan, or Wisconsin.</td>
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
	<!--5 Trailer Information. -->
	<xsl:template name="TRAILER_DEDUCTIBLE">
		<xsl:if test="TRAILER_DEDUCTIBLE='Y'">
			<TR>
				<td class="midcolora">Selected Trailer Deductible is ineligible.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Principle Operator-->
	<xsl:template name="PRINCIPLE_OPERATOR">
		<xsl:if test="PRINCIPLE_OPERATOR='Y'">
			<TR>
				<td class="midcolora">Either Boat has no principal operator or  more than one principal operator.</td>
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
	<!--MAY 05-->
	<!--6 Boat when an operator has an ineligible automobile driving record.Same rules as auto program apply (5 points in 3 years , 5 years for major violations for new business, and 8 points for renewals Major violation(with in 5 years) and Minor Violation(with in 3 years) is/are ) -->
	<xsl:template name="SD_POINTS">
		<xsl:if test="SD_POINTS='Y'">
			<TR>
				<td class="midcolora">Operator has an ineligible Watercraft driving record.(6 or more points).</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Minor violations at Renewal. -->
	<xsl:template name="RENEW_SD_POINTS">
		<xsl:if test="RENEW_SD_POINTS='Y'">
			<TR>
				<td class="midcolora">Operator has an ineligible Watercraft driving record.(6 or more points).</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Negative violations. -->
	<xsl:template name="NEGATIVE_VIOLATION">
		<xsl:if test="NEGATIVE_VIOLATION='Y'">
			<TR>
				<td class="midcolora">Violations with Negative values.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- 7 Any racing or performance models other than sailboats less than 26 feet. -->
	<xsl:template name="PARTICIPATE_RACE">
		<xsl:if test="PARTICIPATE_RACE='Y'">
			<TR>
				<td class="midcolora">Boat other than sailboat or sailboat w/outboard is used for race, speed or fishing contest other than a Sailboat or Sailboat w/outboard under 26ft.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- 8 -->
	<xsl:template name="CHARGE_OFF_PRMIUM">
		<xsl:if test="CHARGE_OFF_PRMIUM='Y'">
			<TR>
				<td class="midcolora">Is a Rollover policy.</td>
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
	<xsl:template name="TOTAL_PREMIUM_AT_RENEWAL">
		<xsl:if test="TOTAL_PREMIUM_AT_RENEWAL='Y'">
			<tr>
				<td class="midcolora">Balance due on policy is greater than equal to Past Due at Renewal.</td>
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
	<xsl:template name="HO_CLAIMS">
		<xsl:if test="HO_CLAIMS='Y'">
			<TR>
				<td class="midcolora">For Prior Losses,paid claims in the last 3 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- 9 May 05 06-->
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
	<!--MAY 06 Grandfather Coverage Carry passengers others Rejected case-->
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
	<!--MAR 07 Grandfather Limit Carry passengers others Rejected case-->
	<xsl:template name="LIMIT_DESCRIPTION">
		<xsl:for-each select="LIMIT">
			<xsl:if test="LIMIT_DES!='' ">
				<TR>
					<td class="midcolora">
					    Limit selected for '<xsl:value-of select="LIMIT_DES"/>' is ineligible.
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
					    Deductible selected for '<xsl:value-of select="DEDUCT_DES"/>' is ineligible.
					</td>
				</TR>
			</xsl:if>
		</xsl:for-each>
	</xsl:template>
	<!--Trailer Rule-->
	<xsl:template name="JETSKI_TYPE_TRAILERINFO">
		<xsl:if test="JETSKI_TYPE_TRAILERINFO='Y'">
			<TR>
				<td class="midcolora">Trailer Info > Jet Ski type trailer can only be attached with Jet Ski type boat or Vice versa.</td>
			</TR>
		</xsl:if>
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
	
</xsl:stylesheet>