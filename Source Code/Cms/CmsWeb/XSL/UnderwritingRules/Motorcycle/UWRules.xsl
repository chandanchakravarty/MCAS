<!-- ****************************************************************************************************** 	
	File			: Motorcycle Rule XSL
	Name			: Ashwani 
	Date			: 29 Aug. 2005 
	Modified By		:
	Modified Date	:
****************************************************************************************************** 	-->
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
		int intVehicleInfoCount=1;
		public int VehicleInfoCount(int intVehicle)
		{
			intVehicleInfoCount=intVehicle*intVehicleInfoCount;			
			return intVehicleInfoCount;
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
			<!-- ================================ Client Top ======================================-->
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
			<!--====================Checking for the rejected rules =============================-->
			<xsl:call-template name="MOTORCYCLE_REJECTION_CASES" />
			<!--====================Checking for the referred rules =============================-->
			<xsl:call-template name="MOTORCYCLE_REFERRED_CASES" />
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
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/APPLICATIONS/APPLICATION"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/MOTORCYCLES/MOTORCYCLE"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/DRIVERS/DRIVER"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO"></xsl:apply-templates>
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
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/APPLICATIONS/APPLICATION"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/MOTORCYCLES/MOTORCYCLE"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/DRIVERS/DRIVER"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO"></xsl:apply-templates>
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
	<!--================================Checking for rejected cases ====================================-->
	<xsl:template name="MOTORCYCLE_REJECTION_CASES">
		<!-- <xsl:call-template name ="IS_REJECT6"/> -->
		<!--  7  -->
		<xsl:call-template name="IS_TAKEN_OUT" />
		<!--  8  -->
		<!-- <xsl:call-template name ="IS_REJECT8"/> -->
		<!--  10  -->
		<!-- <xsl:call-template name ="IS_REJECT10"/> -->
		<!-- 12  -->
		<!--xsl:call-template name="IS_CONT_DRIVER_LICENSE" /-->
		<!-- 14  -->
		<xsl:call-template name="IS_GRG_STATE_RULE" />
		<xsl:call-template name="IS_VEHICLE_MORETHANONE_PRINCIPAL_DRIVER" />
		<xsl:call-template name="IS_RJ_INACTIVE_APPLICATION" />
		<!--Agency Inactive 28th May 2007-->
		<xsl:call-template name="IS_RJ_INACTIVE_AGENCY" />
		<xsl:call-template name="IS_RJ_PRINCIPAL_DRIVER" />
		
		<xsl:call-template name="IS_RJ_ATLEAST_ONE_SELECTED" /><!-- Added by Charles on 10-Aug-09 for Itrack 6234 -->
		<xsl:call-template name="IS_RJ_ATLEAST_ONE_SELECTED_SUS" /><!-- Added by Charles on 13-Nov-09 for Itrack 6234 -->
	</xsl:template>
	<!--=================================Start Rejected Templates======================================== -->
	<!-- 6. -->
	<xsl:template name="IS_REJECT6">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTORCYCLES/MOTORCYCLE/IS_REJECT6='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 7. -->
	<xsl:template name="IS_TAKEN_OUT">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO/IS_TAKEN_OUT='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 8. -->
	<xsl:template name="IS_REJECT8">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTORCYCLES/MOTORCYCLE/IS_REJECT8 ='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 10. -->
	<xsl:template name="IS_REJECT10">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTORCYCLES/MOTORCYCLE/IS_REJECT10='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--11.-->
	<xsl:template name="IS_GRG_STATE_RULE">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTORCYCLES/MOTORCYCLE/GRG_STATE_RULE='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_VEHICLE_MORETHANONE_PRINCIPAL_DRIVER">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTORCYCLES/MOTORCYCLE/VEHICLE_MORETHANONE_PRINCIPAL_DRIVER='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RJ_PRINCIPAL_DRIVER">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTORCYCLES/MOTORCYCLE/YOUTHFUL_PRIN_DRIVER='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--12. -->
	<!--xsl:template name="IS_CONT_DRIVER_LICENSE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/CONT_DRIVER_LICENSE='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template-->
	<!--11.-->
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
	
	<!-- Added by Charles on 10-Aug-09 for Itrack 6234-->
	<xsl:template name="IS_RJ_ATLEAST_ONE_SELECTED">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTORCYCLES/MOTORCYCLE/ATLEAST_ONE_SELECTED='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Added till here -->
	<!-- Added by Charles on 13-Nov-09 for Itrack 6234-->
	<xsl:template name="IS_RJ_ATLEAST_ONE_SELECTED_SUS">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTORCYCLES/MOTORCYCLE/ATLEAST_ONE_SELECTED_SUS='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Added till here -->
	
	<!--=================================End Rejected Templates======================================== -->
	<!--=================================Start Referred Cases======================================== -->
	<xsl:template name="MOTORCYCLE_REFERRED_CASES">
		<!-- 1. -->
		<xsl:call-template name="IS_ANY_NON_OWNED_VEH" />
		<!-- 2. -->
		<xsl:call-template name="IS_DRIVER_US_CITIZEN" />
		<!-- 3. -->
		<xsl:call-template name="IS_DRIVER_SUS_REVOKED" />
		<!-- 4. -->
		<xsl:call-template name="IS_PHY_MENTL_CHALLENGED" />
		<!-- 5. -->
		<!-- <xsl:call-template name ="IS_REFERED5"/>-->
		<!-- 6. -->
		<xsl:call-template name="IS_VEHICLE_AGE" />
		<!-- 7. -->
		<xsl:call-template name="IS_EXISTING_DMG" />
		<!-- 8. -->
		<xsl:call-template name="IS_ANY_FINANCIAL_RESPONSIBILITY" />
		<!-- 9. -->
		<xsl:call-template name="IS_COVERAGE_DECLINED" />
		<!-- 10. -->
		<xsl:call-template name="IS_CHARGE_OFF_PRMIUM" />
		<!-- 11. -->
		<xsl:call-template name="IS_SYMBOL" />
		<!-- 12. -->
		<xsl:call-template name="IS_PREFERRED_DISC" />
		<xsl:call-template name="IS_MORE_WHEELS" />
		<xsl:call-template name="IS_CONVICTED_CARELESS_DRIVE" />
		<xsl:call-template name="IS_SALVAGE_TITLE" />
		<xsl:call-template name="IS_DFI_ACC_NO_RULE" />
		<xsl:call-template name="IS_CREDIT_CARD" />
		<xsl:call-template name="IS_APPEFFECTIVEDATE" />
		<xsl:call-template name="IS_TOTAL_PREMIUM_AT_RENEWAL" />
		<xsl:call-template name="IS_CLAIM_EFFECTIVE" />
		<xsl:call-template name="IS_EXCCESS_CLAIM" />		
		
		<!-- 13 Added by Charles on 5-May-2009 for Itrack Issue 5802-->
		<xsl:call-template name="IS_DRIVER_MVR_ORDERED"></xsl:call-template>
				
		<!--GrandFather Territory 31 oct 2007-->
		<xsl:call-template name="IS_RF_GRANDFATHER_TERR" />
		<!--GrandFather Cov 27 May 2009-->
		<xsl:call-template name="IS_RF_GRANDFATHER_COV" />
		<!--GrandFather Cov(Limit) 27 May 2009-->
		<xsl:call-template name="IS_RF_GRANDFATHER_LIMIT" />
		<!--GrandFather Cov (Deductible) 27 May 2009-->
		<xsl:call-template name="IS_RF_GRANDFATHER_DEDUCT" />		
	</xsl:template>
	<!--=================================Start Referred Templates ======================================-->
	<xsl:template name="IS_CONVICTED_CARELESS_DRIVE">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO/IS_CONVICTED_CARELESS_DRIVE='Y' or 
			INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO/IS_CONVICTED_ACCIDENT='Y' or 
			INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO/IS_USEDFOR_RACING='Y' or 
			INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO/IS_EXTENDED_FORKS='Y' or 
			INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO/IS_MODIFIED_KIT_INCSPEED='Y' or 
			INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO/IS_COMMERCIAL_USE='Y' or
			INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO/IS_LICENSED_FOR_ROAD='Y' or 
			INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO/PRIOR_LOSS_INFO_EXISTS='Y' or
			INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO/IS_COST_OVER_DEFINED_LIMIT='Y' or
			INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO/MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y' ">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_MORE_WHEELS">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO/IS_MORE_WHEELS='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_ANY_NON_OWNED_VEH">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO/ANY_NON_OWNED_VEH = 'Y' ">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_SALVAGE_TITLE">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO/SALVAGE_TITLE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_DRIVER_US_CITIZEN">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/DRIVER_US_CITIZEN='Y' or 
			INPUTXML/DRIVERS/DRIVER/DRIVER_DRINK_VIOLATION='Y' or 
			INPUTXML/DRIVERS/DRIVER/APP_DATEVS_LIC_DATE='Y' or 
			INPUTXML/DRIVERS/DRIVER/COLLG_STD_CAR='Y'  or 
			INPUTXML/DRIVERS/DRIVER/VIOLATION_POINT='Y' or 
			INPUTXML/DRIVERS/DRIVER/SD_POINTS='Y' or 
			INPUTXML/DRIVERS/DRIVER/CONT_DRIVER_LICENSE='Y' or 
			INPUTXML/DRIVERS/DRIVER/DRV_WITHPOINTS='Y' or
			INPUTXML/DRIVERS/DRIVER/DRV_WITHOUTPOINTS='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_DRIVER_SUS_REVOKED">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO/DRIVER_SUS_REVOKED='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--  4. -->
	<xsl:template name="IS_PHY_MENTL_CHALLENGED">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO/PHY_MENTL_CHALLENGED='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 5. -->
	<xsl:template name="IS_REFERED5">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTORCYCLES/MOTORCYCLE/IS_REFERED5='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 6. -->
	<xsl:template name="IS_VEHICLE_AGE">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTORCYCLES/MOTORCYCLE/VEHICLE_AGE='Y' or 
			INPUTXML/MOTORCYCLES/MOTORCYCLE/OTC_COLL='Y' or 			 
			INPUTXML/MOTORCYCLES/MOTORCYCLE/CYCL_REGD_ROAD_USE='Y' or 
			INPUTXML/MOTORCYCLES/MOTORCYCLE/PUNCS='Y' or 
			INPUTXML/MOTORCYCLES/MOTORCYCLE/UNCSL='Y' or 
			INPUTXML/MOTORCYCLES/MOTORCYCLE/UMPD='Y' or 
			INPUTXML/MOTORCYCLES/MOTORCYCLE/UNDSP='Y' or 
			INPUTXML/MOTORCYCLES/MOTORCYCLE/PUMSP='Y'  or 
			INPUTXML/MOTORCYCLES/MOTORCYCLE/MEDPM ='Y' or 
			INPUTXML/MOTORCYCLES/MOTORCYCLE/MOTORCYCLE_TYPE ='Y' or
			INPUTXML/MOTORCYCLES/MOTORCYCLE/MOT_COST_OVER_DEFINED_LIMIT='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 7. -->
	<xsl:template name="IS_EXISTING_DMG">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO/EXISTING_DMG='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 8. -->
	<xsl:template name="IS_ANY_FINANCIAL_RESPONSIBILITY">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO/ANY_FINANCIAL_RESPONSIBILITY='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 9. -->
	<xsl:template name="IS_COVERAGE_DECLINED">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO/COVERAGE_DECLINED='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_CHARGE_OFF_PRMIUM">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/CHARGE_OFF_PRMIUM='Y'	">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
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
	<xsl:template name="IS_EXCCESS_CLAIM">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/EXCCESS_CLAIM='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 13 -->
	<!-- Added by Charles on 5-May-2009 for Itrack Issue 5802 -->
	<xsl:template name="IS_DRIVER_MVR_ORDERED">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/DRIVER_MVR_ORDERED='Y' ">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Added till here -->	
	
	<xsl:template name="IS_SYMBOL">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTORCYCLES/MOTORCYCLE/SYMBOL='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_PREFERRED_DISC">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/PREFERRED_DISC='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 17.GrandFather Territory -->
	<xsl:template name="IS_RF_GRANDFATHER_TERR">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTORCYCLES/MOTORCYCLE/GRANDFATHER_TERRITORY='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 18.GrandFather Coverage -->
	<xsl:template name="IS_RF_GRANDFATHER_COV">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTORCYCLES/MOTORCYCLE/COVERAGE/COV/COV_DES !='' and INPUTXML/MOTORCYCLES/MOTORCYCLE/COVERAGE/WOLVERINE_USER='Y'">
				<xsl:if test="user:VehicleInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:VehicleInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 19.GrandFather Coverage(Limit) -->
	<xsl:template name="IS_RF_GRANDFATHER_LIMIT">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTORCYCLES/MOTORCYCLE/COVERAGE/LIMIT/LIMIT_DES !='' and INPUTXML/MOTORCYCLES/MOTORCYCLE/COVERAGE/WOLVERINE_USER='Y'">
				<xsl:if test="user:VehicleInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:VehicleInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 20.GrandFather Coverage(Deductible) -->
	<xsl:template name="IS_RF_GRANDFATHER_DEDUCT">
		<xsl:choose>
			<xsl:when test="INPUTXML/MOTORCYCLES/MOTORCYCLE/COVERAGE/DEDUCT/DEDUCT_DES !='' and INPUTXML/MOTORCYCLES/MOTORCYCLE/COVERAGE/WOLVERINE_USER='Y'">
				<xsl:if test="user:VehicleInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:VehicleInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--==========================================End Referred Templates================================== -->
	<!--===================================== Messages ============================================== -->
	<!-- ================================== Application Information  ============================== -->
	<xsl:template match="INPUTXML/APPLICATIONS/APPLICATION">
		<xsl:choose>
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<!-- Rejected  Info -->
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
					<xsl:when test="CHARGE_OFF_PRMIUM='Y' or DFI_ACC_NO_RULE='Y' or CREDIT_CARD='Y' or 
					APPEFFECTIVEDATE='Y' or TOTAL_PREMIUM_AT_RENEWAL='Y' or CLAIM_EFFECTIVE='Y' or EXCCESS_CLAIM='Y'">
						<xsl:call-template name="CHARGE_OFF_PRMIUM" />
						<xsl:call-template name="DFI_ACC_NO_RULE" />
						<xsl:call-template name="CREDIT_CARD" />
						<xsl:call-template name="APPEFFECTIVEDATE" />
						<xsl:call-template name="TOTAL_PREMIUM_AT_RENEWAL" />
						<xsl:call-template name="CLAIM_EFFECTIVE" />
						<xsl:call-template name="EXCCESS_CLAIM" />						
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Vechile Info -->
	<xsl:template match="INPUTXML/MOTORCYCLES/MOTORCYCLE">
		<xsl:choose>
			<!-- Rejected messages -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="GRG_STATE_RULE='Y' or VEHICLE_MORETHANONE_PRINCIPAL_DRIVER='Y' or YOUTHFUL_PRIN_DRIVER='Y' or ATLEAST_ONE_SELECTED='Y' or ATLEAST_ONE_SELECTED_SUS='Y'">
					<!-- ATLEAST_ONE_SELECTED,ATLEAST_ONE_SELECTED_SUS added by Charles on 13-Nov-09 for Itrack 6234 -->
						<tr>
							<td class="pageheader" width="36%">For Vehicle:</td>
							<tr>
								<td colspan="4">
									<table width="60%">
										<tr>
											<xsl:if test="VIN != 'N'">
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
						<xsl:call-template name="GRG_STATE_RULE" />
						<xsl:call-template name="VEHICLE_MORETHANONE_PRINCIPAL_DRIVER" />
						<xsl:call-template name="YOUTHFUL_PRIN_DRIVER" />						
						<xsl:call-template name="ATLEAST_ONE_SELECTED" /><!-- Added by Charles on 10-Aug-09 for Itrack 6234 -->
						<xsl:call-template name="ATLEAST_ONE_SELECTED_SUS" /><!-- Added by Charles on 13-Nov-09 for Itrack 6234 -->
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!--*********************** Checking for referred rules (Messages)********************* -->
				<!-- Vechile Info -->
				<xsl:choose>
					<xsl:when test="VEHICLE_AGE='Y' or SYMBOL='Y' or OTC_COLL='Y' or CYCL_REGD_ROAD_USE='Y' 
					or PUNCS='Y' or UNCSL = 'Y' or UMPD= 'Y' or UNDSP='Y' or PUMSP='Y' or MEDPM='Y' or REG_LIC_STATE='Y' or 
					MOTORCYCLE_TYPE='Y' or MOT_COST_OVER_DEFINED_LIMIT='Y' or GRANDFATHER_TERRITORY='Y' or 
					((COVERAGE/DEDUCT/DEDUCT_DES!='' or COVERAGE/COV/COV_DES != '' or  COVERAGE/LIMIT/LIMIT_DES!='' or COPY_COVERAGE_AT_RENEWAL='Y') and  COVERAGE/WOLVERINE_USER='Y')">
				
						<tr>
							<td class="pageheader" width="36%">For Vehicle:</td>
							<tr>
								<td colspan="4">
									<table width="60%">
										<tr>
											<xsl:if test="VIN != 'N'">
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
						<xsl:call-template name="VEHICLE_AGE" />
						<xsl:call-template name="SYMBOL" />
						<xsl:call-template name="OTC_COLL" />
						<xsl:call-template name="CYCL_REGD_ROAD_USE" />
						<xsl:call-template name="PUNCS" />
						<xsl:call-template name="UNCSL" />
						<xsl:call-template name="UMPD" />
						<xsl:call-template name="UNDSP" />
						<xsl:call-template name="PUMSP" />
						<xsl:call-template name="MEDPM" />
						<xsl:call-template name="REG_LIC_STATE" />
						<xsl:call-template name="MOTORCYCLE_TYPE" />
						<xsl:call-template name="MOT_COST_OVER_DEFINED_LIMIT" />						
						<xsl:call-template name="GRANDFATHER_TERRITORY" />
						<xsl:apply-templates select="COVERAGE" />
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
					<!--xsl:when test="COV/COV_DES!='' or LIMIT/LIMIT_DES!='' or DEDUCT/DEDUCT_DES!=''"-->
					<xsl:when test="Y=N">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Coverage Information:</td>
									</tr>
								</table>
							</td>
						</tr>
						<!--GrandFather Coverages>
						<xsl:call-template name="COV_DESCRIPTION" />
						<xsl:call-template name="LIMIT_DESCRIPTION" />
						<xsl:call-template name="DEDUCT_DESCRIPTION" /-->
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred Info -->
				<xsl:choose>
					<xsl:when test="COV/COV_DES != '' or  LIMIT/LIMIT_DES!='' or DEDUCT/DEDUCT_DES!='' or COPY_COVERAGE_AT_RENEWAL='Y' ">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Coverage Information:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="COPY_COVERAGE_AT_RENEWAL" />
						<!--GrandFather Coverages-->
						<xsl:call-template name="COV_DESCRIPTION" />
						<xsl:call-template name="LIMIT_DESCRIPTION" />
						<xsl:call-template name="DEDUCT_DESCRIPTION" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Driver Detail messages -->
	<xsl:template match="INPUTXML/DRIVERS/DRIVER">
		<xsl:choose>
			<!-- Rejected Driver Detail -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="'Y'='N'"></xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred Driver Detail -->
				<xsl:choose>
					<xsl:when test="DRIVER_US_CITIZEN='Y' or PREFERRED_DISC='Y' or DRIVER_DRINK_VIOLATION='Y'
					or APP_DATEVS_LIC_DATE='Y' or COLLG_STD_CAR='Y' or VIOLATION_POINT='Y'  or SD_POINTS='Y'
					or CONT_DRIVER_LICENSE='Y' or DRV_WITHOUTPOINTS='Y' or DRV_WITHPOINTS='Y' or DRIVER_MVR_ORDERED='Y'">
					<!-- Added by Charles, on 5-May-2009, line: or DRIVER_MVR_ORDERED='Y'. for Itrack 5802-->
						<tr>
							<td class="pageheader">for Driver:</td>
							<tr>
								<td colspan="4">
									<table width="60%">
										<tr>
											<xsl:if test="DRIVER_NAME != 'N'">
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
													<xsl:choose>
														<xsl:when test="normalize-space(DRIVER_DRIV_LIC)='N'"></xsl:when>
														<xsl:otherwise>
															<xsl:value-of select="DRIVER_DRIV_LIC" />
														</xsl:otherwise>
													</xsl:choose>
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
						<xsl:call-template name="DRIVER_US_CITIZEN" />
						<xsl:call-template name="PREFERRED_DISC" />
						<xsl:call-template name="DRIVER_DRINK_VIOLATION" />
						<xsl:call-template name="APP_DATEVS_LIC_DATE" />
						<xsl:call-template name="COLLG_STD_CAR" />
						<xsl:call-template name="VIOLATION_POINT" />
						<xsl:call-template name="SD_POINTS" />
						<xsl:call-template name="CONT_DRIVER_LICENSE" />
						<xsl:call-template name="DRV_WITHOUTPOINTS" />
						<xsl:call-template name="DRV_WITHPOINTS" />
						<!-- Added by Charles on 5-May-2009 for Itrack 5802 -->
						<xsl:call-template name="DRIVER_MVR_ORDERED" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- General Info -->
	<xsl:template match="INPUTXML/MOTOTRCYCLEGENINFOS/MOTOTRCYCLEGENINFO">
		<xsl:choose>
			<!-- Rejected General Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="IS_TAKEN_OUT='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Underwriting Questions:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="TAKEN_OUT" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred General Info -->
				<xsl:choose>
					<xsl:when test="ANY_NON_OWNED_VEH='Y' or DRIVER_SUS_REVOKED='Y' or EXISTING_DMG='Y' or
						ANY_FINANCIAL_RESPONSIBILITY='Y' or COVERAGE_DECLINED='Y' or PHY_MENTL_CHALLENGED='Y'
						or IS_MORE_WHEELS='Y' or  IS_CONVICTED_CARELESS_DRIVE='Y' or IS_CONVICTED_ACCIDENT='Y' 
						or IS_USEDFOR_RACING='Y' or IS_EXTENDED_FORKS='Y' or IS_MODIFIED_KIT_INCSPEED='Y' or 
						IS_COMMERCIAL_USE='Y' or IS_LICENSED_FOR_ROAD='Y' or PRIOR_LOSS_INFO_EXISTS='Y' or 
						IS_COST_OVER_DEFINED_LIMIT ='Y' or SALVAGE_TITLE='Y' or MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y'">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader">For Underwriting Questions:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="EXTENDED_FORKS" />
						<xsl:call-template name="ANY_NON_OWNED_VEH" />
						<xsl:call-template name="DRIVER_SUS_REVOKED" />
						<xsl:call-template name="EXISTING_DMG" />
						<xsl:call-template name="ANY_FINANCIAL_RESPONSIBILITY" />
						<xsl:call-template name="COVERAGE_DECLINED" />
						<xsl:call-template name="PHY_MENTL_CHALLENGED" />
						<xsl:call-template name="MORE_WHEELS" />
						<xsl:call-template name="IS_CONVICTED_ACCIDENT" />
						<xsl:call-template name="CONVICTED_CARELESS_DRIVE" />
						<xsl:call-template name="USEDFOR_RACING" />
						<xsl:call-template name="MODIFIED_KIT_INCSPEED" />
						<xsl:call-template name="COMMERCIAL_USE" />
						<xsl:call-template name="LICENSED_FOR_ROAD" />
						<xsl:call-template name="PRIOR_LOSS_INFO_EXISTS" />
						<xsl:call-template name="COST_OVER_DEFINED_LIMIT" />
						<xsl:call-template name="SALVAGE_TITLE" />
						<xsl:call-template name="MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- =====================  Start Templates for showing rejected rules messages ======================== -->
	<!--1. Any cycle on modified frame, a kit or assembled bike 
		Choppers, show bikes, non-factory build, kit converted, salvaged, rebuilt or bikes modified to increase speed.-->
	<xsl:template name="MODIFIED_KIT_INCSPEED">
		<xsl:choose>
			<xsl:when test="IS_MODIFIED_KIT_INCSPEED ='Y'">
				<TR>
					<td class="midcolora">Cycle modified to increase speed or Modified frame, a kit or assembled bike.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--2. Bikes with extended forks (over six inches) front brake only -->
	<xsl:template name="EXTENDED_FORKS">
		<xsl:choose>
			<xsl:when test="IS_EXTENDED_FORKS ='Y'">
				<TR>
					<td class="midcolora">Bike(s) with extended forks over 6 inches.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--3.Commercial, business use or primary use is for parade(s). -->
	<xsl:template name="COMMERCIAL_USE">
		<xsl:choose>
			<xsl:when test="IS_COMMERCIAL_USE ='Y'">
				<TR>
					<td class="midcolora">Bike(s) used for commercial, business, or parade purpose.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--4.Bikes used for racing, hill climbing, exhibition, motor-cross or other competition or sporting event, or as a rental. -->
	<xsl:template name="USEDFOR_RACING">
		<xsl:choose>
			<xsl:when test="IS_USEDFOR_RACING ='Y'">
				<TR>
					<td class="midcolora">Bike(s) used for racing, hill climbing, motor-cross or any sporting event.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--5. Applicant or regular operator who is required to carry auto insurance in any sub-standard plan because of poor experience.-->
	<xsl:template name="REJECT5">
		<xsl:choose>
			<xsl:when test="REJECT5 ='Y'">
				<TR>
					<td class="midcolora">Applicant or regular operator who is required to carry auto insurance in any sub-standard plan because of poor experience.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--6. Taking bike to college -->
	<xsl:template name="REJECT6">
		<xsl:choose>
			<xsl:when test="REJECT6 ='Y'">
				<TR>
					<td class="midcolora">Taking bike to college.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- 7. Taking bike out-of-state on trips for period to exceed 30 days -->
	<xsl:template name="TAKEN_OUT">
		<xsl:choose>
			<xsl:when test="IS_TAKEN_OUT ='Y'">
				<TR>
					<td class="midcolora">Bike(s) taken out-of-state on trips for a period exceeding 30 days.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--8. Any cycle with a side care.-->
	<xsl:template name="REJECT8">
		<xsl:choose>
			<xsl:when test="REJECT8 ='Y'">
				<TR>
					<td class="midcolora">Any cycle with a side care.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--9. Any three-wheel or four-wheel cycles -->
	<xsl:template name="MORE_WHEELS">
		<xsl:choose>
			<xsl:when test="IS_MORE_WHEELS='Y'">
				<TR>
					<td class="midcolora">Bike(s) having more than 2 wheels.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--10. No valid drivers license -->
	<xsl:template name="REJECT10">
		<xsl:choose>
			<xsl:when test="REJECT10 ='Y'">
				<TR>
					<td class="midcolora">Driver(s) do not have a valid license.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--11. Convicted of a major violation in the preceding 5 years.-->
	<xsl:template name="CONVICTED_CARELESS_DRIVE">
		<xsl:choose>
			<xsl:when test="IS_CONVICTED_CARELESS_DRIVE ='Y'">
				<TR>
					<td class="midcolora">Driver(s) has been convicted of reckless or careless driving, DUIL, DWI in the last 5 years.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--12.Not had drivers license for at least 12 months to operate an automobile -->
	<xsl:template name="CONT_DRIVER_LICENSE">
		<xsl:choose>
			<xsl:when test="CONT_DRIVER_LICENSE='Y'">
				<TR>
					<td class="midcolora">Date Licensed is less than 12 months.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- MVR PTS NOT EXISTS & ANY DRIVER WITH POINTS - REFER -->
	<xsl:template name="DRV_WITHPOINTS">
		<xsl:if test="DRV_WITHPOINTS='Y'">
			<TR>
				<td class="midcolora">Driver is assigned "with points" and no MVR information is provided.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="YOUTHFUL_PRIN_DRIVER">
		<xsl:if test="normalize-space(YOUTHFUL_PRIN_DRIVER)='Y'">
			<TR>
				<td class="midcolora">Cycle not having principal driver.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<!--  MVR PTS EXISTS & ANY DRIVER WITH NO POINTS- REFER -->
	<xsl:template name="DRV_WITHOUTPOINTS">
		<xsl:if test="DRV_WITHOUTPOINTS='Y'">
			<TR>
				<td class="midcolora">Driver is assigned "with no points" and MVR information is provided.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<!-- On new business,the accumulation of more than 5 eligibility points during the preceding 3 years for any one operator in the insureds household. -->
	<xsl:template name="SD_POINTS">
		<xsl:if test="SD_POINTS='Y'">
			<TR>
				<td class="midcolora">Accumulation of more than 5 eligibility points during the preceding 3 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--13. Any cycle with a cost new or value over $30,000 -->
	<xsl:template name="COST_OVER_DEFINED_LIMIT">
		<xsl:choose>
			<xsl:when test="IS_COST_OVER_DEFINED_LIMIT ='Y'">
				<TR>
					<td class="midcolora">Cost of Bike(s) over $40,000.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--14. Any cycle with a salvage title.-->
	<xsl:template name="SALVAGE_TITLE">
		<xsl:choose>
			<xsl:when test="SALVAGE_TITLE ='Y'">
				<TR>
					<td class="midcolora">Bike(s) having a salvage title.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--15. Any cycle not licensed for road use. -->
	<xsl:template name="LICENSED_FOR_ROAD">
		<xsl:choose>
			<xsl:when test="IS_LICENSED_FOR_ROAD ='Y'">
				<TR>
					<td class="midcolora">Bike(s) not licensed for road use.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- Garaging state of all motorcycles must be same as of main application state-->
	<xsl:template name="GRG_STATE_RULE">
		<xsl:choose>
			<xsl:when test="normalize-space(GRG_STATE_RULE)='Y'">
				<TR>
					<td class="midcolora">Garaging state of all motorcycles must be same as that of the main application state.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="VEHICLE_MORETHANONE_PRINCIPAL_DRIVER">
		<xsl:choose>
			<xsl:when test="normalize-space(VEHICLE_MORETHANONE_PRINCIPAL_DRIVER)='Y'">
				<TR>
					<td class="midcolora">More than one principal operator assigned to a cycle.</td>
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
	<xsl:template name="IS_CONVICTED_ACCIDENT">
		<xsl:choose>
			<xsl:when test="IS_CONVICTED_ACCIDENT ='Y'">
				<TR>
					<td class="midcolora">Driver(s)has been convicted of leaving scene of accident or drug use in last 5 years.</td>
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
	<!-- Added by Charles on 10-Aug-09 for Itrack 6234 -->
	<xsl:template name="ATLEAST_ONE_SELECTED">
		<xsl:if test="ATLEAST_ONE_SELECTED='Y'">
			<TR>
				<td class="midcolora">At least one coverage from CSL, BI/PD should be selected</td><!-- Changed by Charles on 13-Nov-09 for Itrack 6234 -->
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Added till here -->
	<!-- Added by Charles on 13-Nov-09 for Itrack 6234 -->
	<xsl:template name="ATLEAST_ONE_SELECTED_SUS">
		<xsl:if test="ATLEAST_ONE_SELECTED_SUS='Y'">
			<TR>
				<td class="midcolora">At least one coverage from Comprehensive or Collision should be selected</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Added till here -->
	<!-- ======================== Templates for showing refered rules messages ====================== -->
	<!-- 1.Owned by other than an individual. -->
	<xsl:template name="ANY_NON_OWNED_VEH">
		<xsl:choose>
			<xsl:when test="ANY_NON_OWNED_VEH ='Y'">
				<TR>
					<td class="midcolora">Bike(s) not solely owned.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- 2 Any risk with physical or mental impairment -->
	<xsl:template name="PHY_MENTL_CHALLENGED">
		<xsl:if test="PHY_MENTL_CHALLENGED='Y'">
			<TR>
				<td class="midcolora">Driver(s) physically or mentally impaired.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- 3 License has been suspended, restricted or revoked for any period of time within past 5 years.-->
	<xsl:template name="DRIVER_SUS_REVOKED">
		<xsl:if test="DRIVER_SUS_REVOKED='Y'">
			<TR>
				<td class="midcolora">Driver(s) license has been suspended, revoked or restricted in last 5 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- 4 Not a U.S. citizen.-->
	<xsl:template name="DRIVER_US_CITIZEN">
		<xsl:if test="DRIVER_US_CITIZEN='Y'">
			<TR>
				<td class="midcolora">Driver is not a U.S. citizen.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--5. -->
	<xsl:template name="REFERED5">
		<xsl:if test="US_CITIZEN='Y'">
			<TR>
				<td class="midcolora">Any vehicle that is titled to an individual other than the applicant.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--6. Any cycle over 25 years old is ineligible for physical damage coverage-->
	<xsl:template name="VEHICLE_AGE">
		<xsl:if test="VEHICLE_AGE='Y'">
			<TR>
				<td class="midcolora">Cycle over 25 years old is ineligible for physical damage coverages.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--7. Vehicles with existing damage (including damaged glass).-->
	<xsl:template name="EXISTING_DMG">
		<xsl:if test="EXISTING_DMG='Y'">
			<TR>
				<td class="midcolora">Bike(s) with existing damage.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--8.Any financial responsibility filing -->
	<xsl:template name="ANY_FINANCIAL_RESPONSIBILITY">
		<xsl:if test="ANY_FINANCIAL_RESPONSIBILITY='Y'">
			<TR>
				<td class="midcolora">Financial responsibility filing present.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--9. Any person having coverage declined, canceled or non-renewed during the last 3 years-->
	<xsl:template name="COVERAGE_DECLINED">
		<xsl:if test="COVERAGE_DECLINED='Y'">
			<TR>
				<td class="midcolora">Coverage has been declined, cancelled or non-renewed during the last 3 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--10.Any rollover policy -->
	<xsl:template name="CHARGE_OFF_PRMIUM">
		<xsl:if test="CHARGE_OFF_PRMIUM='Y'">
			<TR>
				<td class="midcolora">This is a Rollover policy.</td>
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
	<!-- Symbol -->
	<xsl:template name="SYMBOL">
		<xsl:if test="SYMBOL='Y'">
			<TR>
				<td class="midcolora">Symbol exceeds the limit of 9.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="OTC_COLL">
		<xsl:if test="OTC_COLL='Y'">
			<TR>
				<td class="midcolora">Collision/Comprehensive coverage provided for a motorcycle over 25 years old.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="CYCL_REGD_ROAD_USE">
		<xsl:if test="CYCL_REGD_ROAD_USE='Y'">
			<TR>
				<td class="midcolora">Cycle is less than or equal to 50 CC and not registered for Road use.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="PREFERRED_DISC">
		<xsl:if test="PREFERRED_DISC='Y'">
			<TR>
				<td class="midcolora">Driver Discount Preferred Risk is not applicable.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="DRIVER_DRINK_VIOLATION">
		<xsl:if test="DRIVER_DRINK_VIOLATION='Y'">
			<TR>
				<td class="midcolora">Convicted of a Drinking or Drug Related violation in the preceding 5 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="APP_DATEVS_LIC_DATE">
		<xsl:if test="APP_DATEVS_LIC_DATE='Y'">
			<TR>
				<td class="midcolora">Driver's License State is different from application state.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="COLLG_STD_CAR">
		<xsl:if test="COLLG_STD_CAR='Y'">
			<TR>
				<td class="midcolora">Driver is a college student and having motorcycle.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="VIOLATION_POINT">
		<xsl:if test="VIOLATION_POINT='Y'">
			<TR>
				<td class="midcolora">Violation(s) having more than 5 points or negative points.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- For Coverges 1 -->
	<xsl:template name="PUNCS">
		<xsl:if test="PUNCS='Y'">
			<TR>
				<td class="midcolora">Signature not obtained for Uninsured Motorists (CSL).</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- For Coverges 2 -->
	<xsl:template name="UNCSL">
		<xsl:if test="UNCSL='Y'">
			<TR>
				<td class="midcolora">Signature not obtained for Underinsured Motorists (CSL).</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- For Coverges  3 -->
	<xsl:template name="UMPD">
		<xsl:if test="UMPD='Y'">
			<TR>
				<td class="midcolora">Signature not obtained for Uninsured Motorist (PD).</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- For Coverges  4 -->
	<xsl:template name="UNDSP">
		<xsl:if test="UNDSP='Y'">
			<TR>
				<td class="midcolora">Signature not obtained for Underinsured Motorists (BI Split Limit).</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- For Coverges  5 -->
	<xsl:template name="PUMSP">
		<xsl:if test="PUMSP='Y'">
			<TR>
				<td class="midcolora">Signature not obtained for Uninsured Motorists (BI Split Limit).</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- For Coverges  6 -->
	<xsl:template name="MEDPM">
		<xsl:if test="MEDPM='Y'">
			<TR>
				<td class="midcolora">Signature not obtained for Medical Payments.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- For Coverges  6 -->
	<xsl:template name="REG_LIC_STATE">
		<xsl:if test="REG_LIC_STATE='Y'">
			<TR>
				<td class="midcolora">Vehicle's Registered State is different from Policy State.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="MOTORCYCLE_TYPE">
		<xsl:if test="MOTORCYCLE_TYPE='Y'">
			<TR>
				<td class="midcolora">Motorcycle type is ineligible.</td>
			</TR>
		</xsl:if>
	</xsl:template>		
	<xsl:template name="MOT_COST_OVER_DEFINED_LIMIT">
		<xsl:if test="MOT_COST_OVER_DEFINED_LIMIT='Y'">
			<TR>
				<td class="midcolora">Cycle(s) having cost over $40,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>	
	<!-- Added by Charles on 5-May-09 for Itrack 5802 -->
	<xsl:template name="DRIVER_MVR_ORDERED">
		<xsl:if test="DRIVER_MVR_ORDERED='Y'">
			<TR>
				<td class="midcolora">MVR Ordered is Yes and MVR Information not available or vice versa or MVR Ordered is No or Blank.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Added till here -->
	
	<!--GrandFathered Territory ( Refered)-->
	<!--Oct 31 Grandfather Territory Carry passengers others Refered case-->
	<xsl:template name="GRANDFATHER_TERRITORY">
		<xsl:if test="GRANDFATHER_TERRITORY='Y'">
			<TR>
				<td class="midcolora">
					    Selected Territory is ineligible.
					</td>
			</TR>
		</xsl:if>
	</xsl:template>
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
	<xsl:template name="EXCCESS_CLAIM">
		<xsl:if test="EXCCESS_CLAIM='Y'">
			<tr>
				<td class="midcolora">A risk has incurred an excessive number (more than 5) paid claims within the past three years.</td>
			</tr>
		</xsl:if>
	</xsl:template>
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
	<!--GrandFathered Limit (Rjected and Refered)-->
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
	<!--GrandFathered Limit (Rjected and Refered)-->
	<!--GrandFathered deductible (Rjected and Refered)-->
	<xsl:template name="DEDUCT_DESCRIPTION">
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
	<!--End Referred Templates -->
</xsl:stylesheet>