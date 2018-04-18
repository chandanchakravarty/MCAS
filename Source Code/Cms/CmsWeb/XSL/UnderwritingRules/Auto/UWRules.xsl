<!--	Check  for the PPA Rules-->
<!--	Name : Ashwani  -->
<!--	Date : 10 Sep.,2005  -->
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
			<!--Checking for the rejected rules  -->
			<xsl:call-template name="AUTO_REJECTION_CASES" />
			<xsl:call-template name="AUTO_REFERRED_CASES" />
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
						<!--rejected messages-->
						<!-- <xsl:call-template name="AUTO_REJECTED_RULES" />  -->
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/APPLICATIONS/APPLICATION" />
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/VEHICLES"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/DRIVERS/DRIVER"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/AUTOGENINFOS/AUTOGENINFO"></xsl:apply-templates>
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
						<!-- <xsl:call-template name="AUTO_REFERRED_RULES" />  -->
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/APPLICATIONS/APPLICATION"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/VEHICLES/VEHICLE"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/DRIVERS/DRIVER"></xsl:apply-templates>
							</td>
						</tr>
						<tr>
							<td>
								<xsl:apply-templates select="INPUTXML/AUTOGENINFOS/AUTOGENINFO"></xsl:apply-templates>
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
	<!-- Start Checking for rejected rules -->
	<xsl:template name="AUTO_REJECTION_CASES">
		<!--  1 -->
		<xsl:call-template name="IS_DRIVER_DRINK_VIOLATION" />
		<!--  2  -->
		<xsl:call-template name="IS_ANY_ANTIQUE_AUTO" />
		<!--  3  -->
		<xsl:call-template name="IS_RJ_APP" />
		<!--  4  -->
		<!-- <xsl:call-template name ="IS_REJECT4"/> -->
		<!--  5  -->
		<!-- <xsl:call-template name ="IS_REJECT5"/> -->
		<!--  6  -->
		<!-- <xsl:call-template name ="IS_REJECT6"/> -->
		<!--  7  -->
		<!--<xsl:call-template name="IS_CONT_DRIVER_LICENSE" />-->
		<!--  8  -->
		<!-- <xsl:call-template name ="IS_REJECT8"/> -->
		<!--  9  -->
		<!-- <xsl:call-template name ="IS_REJECT9"/> -->
		<!--  10  -->
		<!-- <xsl:call-template name ="IS_REJECT10"/> -->
		<!--  11 -->
		<!-- <xsl:call-template name ="IS_REJECT11"/> -->
		<!-- 12  -->
		<xsl:call-template name="IS_GARAGING_STATE"></xsl:call-template>
		<!--Major Violation -->
		<xsl:call-template name="IS_MAJOR_VIOLATION" />
		<!--********************** for Indiana only *************************** -->
		<!-- <xsl:if test="INPUTXML/VEHICLES/VEHICLE/STATE_ID = '' "> -->
		<!-- 13  -->
		<!-- <xsl:call-template name ="IS_REJECT13"/> -->
		<!--</xsl:if>  -->
		<!--<GrandFather Cov 27 July 2006>
		<xsl:call-template name="IS_RJ_GRANDFATHER_COV" />
		<GrandFather Limit 8th Mar 2007 >
		<xsl:call-template name="IS_RJ_GRANDFATHER_LIMIT" />
		<GrandFather Deductible 8th Mar 2007>
		<xsl:call-template name="IS_RJ_GRANDFATHER_DEDUCT" />
		<Customer Inactive 6th Apr 2007> -->
		<xsl:call-template name="IS_RJ_INACTIVE_APPLICATION" />
		<!--Agency Inactive 28th May 2007-->
		<xsl:call-template name="IS_RJ_INACTIVE_AGENCY" />
		<xsl:call-template name="IS_RJ_OTHER_VEHICLE" />
		<xsl:call-template name="IS_RJ_PRINC_OCCA_DRIVER" />
		<xsl:call-template name="IS_RJ_ANTIQUE_CLASSIC_CAR" />
		<xsl:call-template name="IS_RJ_PRINCIPLE_OPERATOR" />
		<xsl:call-template name="IS_RJ_COM_BISPLITLIMIT" />
		<!--**********************End for Indiana only *************************** -->
	</xsl:template>
	<!-- End Checking for rejected rules -->
	<!--Start Rejected Templates -->
	<!-- 1. -->
	<xsl:template name="IS_DRIVER_DRINK_VIOLATION">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/DRIVER_DRINK_VIOLATION='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 2. -->
	<xsl:template name="IS_ANY_ANTIQUE_AUTO">
		<xsl:choose>
			<xsl:when test="INPUTXML/AUTOGENINFOS/AUTOGENINFO/ANY_ANTIQUE_AUTO='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 3. -->
	<xsl:template name="IS_RJ_APP">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/LESS_INSURANCE_SCORE='Y' or 
			INPUTXML/APPLICATIONS/APPLICATION/INELIGIBLE_DRIVER='Y' or 
			INPUTXML/APPLICATIONS/APPLICATION/LOSS_AMT_EXCEED='Y' or 
			INPUTXML/APPLICATIONS/APPLICATION/MVR_VER='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 4. -->
	<xsl:template name="IS_REJECT4">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/IS_REJECT4='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 4. -->
	<xsl:template name="IS_REJECT5">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/IS_REJECT5='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 6. -->
	<xsl:template name="IS_REJECT6">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/IS_REJECT6='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 7. -->
	<!--<xsl:template name="IS_CONT_DRIVER_LICENSE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/CONT_DRIVER_LICENSE='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>-->
	<!-- 8. -->
	<xsl:template name="IS_REJECT8">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/IS_REJECT8='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 9. -->
	<xsl:template name="IS_REJECT9">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/IS_REJECT9='Y'">
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
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/IS_REJECT10='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 11. -->
	<xsl:template name="IS_REJECT11">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/IS_REJECT11='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- *************** Start for Indiana Only  ******************-->
	<!--13. -->
	<xsl:template name="IS_REJECT13">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/IS_REJECT13='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 14. -->
	<xsl:template name="IS_MAJOR_VIOLATION">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/MAJOR_VIOLATION='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ***************End for Indiana Only   ******************-->
	<!-- 17.GrandFather Coverage -->
	<xsl:template name="IS_RJ_GRANDFATHER_COV">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/COVERAGE/COV/COV_DES !='' and INPUTXML/VEHICLES/VEHICLE/COVERAGE/WOLVERINE_USER='N'">
				<xsl:if test="user:VehicleInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:VehicleInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 18.GrandFather Coverage(Limit) -->
	<xsl:template name="IS_RJ_GRANDFATHER_LIMIT">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/COVERAGE/LIMIT/LIMIT_DES !='' and INPUTXML/VEHICLES/VEHICLE/COVERAGE/WOLVERINE_USER='N'">
				<xsl:if test="user:VehicleInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:VehicleInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 18.GrandFather Coverage(Deductible) -->
	<xsl:template name="IS_RJ_GRANDFATHER_DEDUCT">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/COVERAGE/DEDUCT/DEDUCT_DES !='' and INPUTXML/VEHICLES/VEHICLE/COVERAGE/DEDUCT/DEDUCT_DES !='N' and INPUTXML/VEHICLES/VEHICLE/COVERAGE/WOLVERINE_USER='N'">
				<xsl:if test="user:VehicleInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:VehicleInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Garaging State -->
	<xsl:template name="IS_GARAGING_STATE">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/GRG_STATE_RULE='Y'">
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
	<!--if no Vehcile other then Utility Trailer Comper and travel trailer commercial Trailer added by pravesh-->
	<xsl:template name="IS_RJ_OTHER_VEHICLE">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/OTHER_VEHICLE='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RJ_PRINC_OCCA_DRIVER">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/PRINC_OCCA_DRIVER='N'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RJ_ANTIQUE_CLASSIC_CAR">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/ANTIQUE_CLASSIC_CAR='Y'">
				<xsl:if test="user:VehicleInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Principle Operator-->
	<xsl:template name="IS_RJ_PRINCIPLE_OPERATOR">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/PRINCIPLE_OPERATOR='Y'">
				<xsl:if test="user:VehicleInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Com Vehicle Bi split Not apllicable limit  -->
	<xsl:template name="IS_RJ_COM_BISPLITLIMIT">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/BICOMNA='Y'">
				<xsl:if test="user:VehicleInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:IsApplicationAcceptable(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- =========================End Rejected Templates ======================================== -->
	<!--========================= Start checking for referred Cases============================== -->
	<xsl:template name="AUTO_REFERRED_CASES">
		<!-- 1. -->
		<xsl:call-template name="IS_USE_AS_TRANSPORT_FEE"></xsl:call-template>
		<!-- 2. -->
		<!-- <xsl:call-template name ="IS_REFERED2"></xsl:call-template> -->
		<!-- 3. -->
		<!-- <xsl:call-template name ="IS_REFERED3"></xsl:call-template> -->
		<!-- 4. -->
		<xsl:call-template name="IS_COVERAGE_DECLINED"></xsl:call-template>
		<!-- 5. -->
		<xsl:call-template name="IS_US_CITIZEN"></xsl:call-template>
		<!-- 6. -->
		<!-- <xsl:call-template name ="IS_REFERED6"></xsl:call-template> -->
		<!-- 7. -->
		<xsl:call-template name="IS_MEM_IN_MILITRY"></xsl:call-template>
		<!-- 8. -->
		<xsl:call-template name="IS_DISTANT_STUDENT"></xsl:call-template>
		<xsl:call-template name="IS_PARENT_ELSEWHERE"></xsl:call-template>
		<!-- 9. -->
		<xsl:call-template name="IS_DRIVER_SUS_REVOKED"></xsl:call-template>
		<!-- 10-->
		<xsl:call-template name="IS_DRIVER_MVR_ORDERED"></xsl:call-template>
		<!-- 10. -->
		<xsl:call-template name="IS_VEHICLE_AMOUNT"></xsl:call-template>
		<!-- 11. -->
		<xsl:call-template name="IS_PHY_MENTAL_CHALLENGED"></xsl:call-template>
		<!-- 12. -->
		<!-- <xsl:call-template name ="IS_REFERED12"></xsl:call-template>	 -->
		<!-- 13. -->
		<xsl:call-template name="IS_CAR_MODIFIED"></xsl:call-template>
		<!-- 14. -->
		<!-- <xsl:call-template name ="IS_REFERED14"></xsl:call-template>	 -->
		<!-- 15. -->
		<!-- <xsl:call-template name="IS_MODEL_NAME"></xsl:call-template> -->
		<!--Rules added from Ebix Application Process Document or Additional Requests  -->
		<!-- 16. -->
		<xsl:call-template name="IS_ANY_NON_OWNED_VEH"></xsl:call-template>
		<!-- 17. -->
		<xsl:call-template name="IS_EXISTING_DMG"></xsl:call-template>
		<!-- 18. -->
		<xsl:call-template name="IS_ANY_CAR_AT_SCH"></xsl:call-template>
		<!-- 19. -->
		<xsl:call-template name="IS_ANY_OTH_INSU_COMP"></xsl:call-template>
		<!-- 20. -->
		<xsl:call-template name="IS_ANY_FINANCIAL_RESPONSIBILITY"></xsl:call-template>
		<!-- 21. -->
		<!-- <xsl:call-template name="IS_SAFE_DRIVER"></xsl:call-template> -->
		<!-- 22. -->
		<xsl:call-template name="IS_DRIVER_DOB"></xsl:call-template>
		<!-- 23. -->
		<xsl:call-template name="IS_CHARGE_OFF_PRMIUM"></xsl:call-template>
		<!-- 24. -->
		<xsl:call-template name="IS_SYMBOL"></xsl:call-template>
		<xsl:call-template name="IS_SD_POINTS"></xsl:call-template>
		<xsl:call-template name="IS_AUTO_SD_POINTS"></xsl:call-template>
		<xsl:call-template name="IS_RENEW_AUTO_SD_POINTS"></xsl:call-template>
		<xsl:call-template name="IS_CONT_DRIVER_LICENSE" />
		<!-- 25. -->
		<xsl:call-template name="IS_PUNCS" />
		<xsl:call-template name="IS_UNCSL" />
		<xsl:call-template name="IS_UMPD" />
		<xsl:call-template name="IS_UNDSP" />
		<xsl:call-template name="IS_PUMSP" />
		<xsl:call-template name="IS_IS_OTHER_THAN_INSURED" />
		<xsl:call-template name="IS_DRIVER_LIC_STATE" />
		<xsl:call-template name="IS_DRIVER_LIC_STATE_APP_STATE" />
		<xsl:call-template name="IS_LLGC" />
		<xsl:call-template name="IS_ENO" />
		<xsl:call-template name="IS_DRIVER_DRIV_TYPE" />
		<!--GrandFather Cov 27 July 2006-->
		<xsl:call-template name="IS_RF_GRANDFATHER_COV" />
		<!--GrandFather Cov(Limit) 8th Mar 2007-->
		<xsl:call-template name="IS_RF_GRANDFATHER_LIMIT" />
		<!--GrandFather Cov (Deductible)-->
		<xsl:call-template name="IS_RF_GRANDFATHER_DEDUCT" />
		<!--GrandFather Territory Code 31 oct 2007-->
		<xsl:call-template name="IS_RF_GRANDFATHER_TERR" />
		<xsl:call-template name="IS_RF_QUALIFIESTRAIBLAZERPROGRAM" />
		<xsl:call-template name="IS_AUTO_DRIVER_FAULT" />
		<xsl:call-template name="IS_EPENDO_SIGN" />
		<xsl:call-template name="IS_REG_STATE" />
		<xsl:call-template name="IS_MAKE_NAME" />
		<!-- <xsl:call-template name="IS_ANY_EXC_DRIVER_TYPE" />		 -->
		<xsl:call-template name="IS_COMM_RADIUS_OF_USE" />
		<xsl:call-template name="IS_VEHICLE_AMOUNT_COM" />
		<xsl:call-template name="IS_SALVAGE_TITLE" />
		<xsl:call-template name="IS_WAIVER_WORK_LOSS" />
		<xsl:call-template name="IS_INSNOW_FULL" />
		<xsl:call-template name="IS_CREG_STATE" />
		<xsl:call-template name="IS_MODEL_NAME_MULTI_DIS" />
		<!--Multicar Discount-->
		<xsl:call-template name="IS_MULTICAR_DISCOUNT_ELIGIBLE"></xsl:call-template>
		<!--Refer Coverage at Renewal Level ,10th Apr 2007 -->
		<xsl:call-template name="IS_RF_COPY_COVERAGE_AT_RENEWAL" />
		<xsl:call-template name="IS_VEHICLE_GRT_15"></xsl:call-template>
		<xsl:call-template name="IS_VEHICLE_GRT_15_OT_COLL"></xsl:call-template>
		<xsl:call-template name="IS_VEHICLE_CLAIM_ELIGIBLE"></xsl:call-template>
		<xsl:call-template name="IS_VEHICLE_EXTRA_EQUIP_COV"></xsl:call-template>
		<xsl:call-template name="IS_LEASED_PURCHASED"></xsl:call-template>
		<xsl:call-template name="IS_ADD_INT_LOAN_LEAN"></xsl:call-template>
		<xsl:call-template name="IS_BI_CSL_EXISTS"></xsl:call-template>
		<!-- <xsl:call-template name="IS_NEGATIVE_VIOLATION_POINT"></xsl:call-template> -->
		<xsl:call-template name="IS_GOOD_STUDENT_DISCOUNT"></xsl:call-template>
		<xsl:call-template name="IS_HOME_AS400_RENEWAL"></xsl:call-template>
		<xsl:call-template name="IS_BEYOND_50_MILES"></xsl:call-template>
		<xsl:call-template name="IS_DRIVER_ABOVE_EITEEN"></xsl:call-template>
		<xsl:call-template name="IS_DRIVER_ABOVE_TWENTYFOUR"></xsl:call-template>
		<xsl:call-template name="IS_UNDERWRITING_TIER"></xsl:call-template> <!-- Added by Charles on 29-Dec-09 for Itrack 6830 -->
				
	</xsl:template>
	<!--Start Reffered Templates -->
	<xsl:template name="IS_MODEL_NAME_MULTI_DIS">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/MODEL_NAME_MULTI_DIS='Y' or
				INPUTXML/VEHICLES/VEHICLE/YOUTHFUL_PRINC_DRIVER='Y' or 
				INPUTXML/VEHICLES/VEHICLE/VEHICLE_TYPE_MHT='Y' or INPUTXML/VEHICLES/VEHICLE/COMM_LONGHAUL='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--10TH APR 2007 -->
	<xsl:template name="IS_RF_COPY_COVERAGE_AT_RENEWAL">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/COVERAGE/COPY_COVERAGE_AT_RENEWAL='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- -->
	<xsl:template name="IS_WAIVER_WORK_LOSS">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/WAIVER_WORK_LOSS='Y' or INPUTXML/DRIVERS/DRIVER/YOUTH_DRIVER='Y'
			or INPUTXML/DRIVERS/DRIVER/VIOLATION_POINT='Y' or INPUTXML/DRIVERS/DRIVER/DRV_WITHPOINTS='Y' or INPUTXML/DRIVERS/DRIVER/DRV_WITHOUTPOINTS='Y'
			or INPUTXML/DRIVERS/DRIVER/DRIVER_VOLUNTEER_POLICE_FIRE='Y' 
			or INPUTXML/DRIVERS/DRIVER/COLLEGE_INSELSE='Y'  or INPUTXML/DRIVERS/DRIVER/COLLEGE_CAR_STATE='Y' or 
			 INPUTXML/DRIVERS/DRIVER/COLLEGE_CAR_STATE_VEHCILE='Y' or INPUTXML/DRIVERS/DRIVER/DRV_MIL_INSELSE='Y'
			 or INPUTXML/DRIVERS/DRIVER/PARNT_USTERR_CAR_STA='Y' or 
			  INPUTXML/DRIVERS/DRIVER/MI_OLDDRIVER='Y' or INPUTXML/DRIVERS/DRIVER/DRIVER_MVR_ORDERED='Y' ">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- -->
	<xsl:template name="IS_INSNOW_FULL">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/INSNOW_FULL='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- -->
	<xsl:template name="IS_CREG_STATE">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/CREG_STATE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- -->
	<xsl:template name="IS_COMM_RADIUS_OF_USE">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/COMM_INTER_LH='Y' or 
			INPUTXML/VEHICLES/VEHICLE/COMM_LOCALHAUL='Y' or
			INPUTXML/VEHICLES/VEHICLE/COMM_RADIUS_OF_USE='Y' ">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--12. -->
	<xsl:template name="IS_SALVAGE_TITLE">
		<xsl:choose>
			<xsl:when test="INPUTXML/AUTOGENINFOS/AUTOGENINFO/SALVAGE_TITLE='Y' or 
							INPUTXML/AUTOGENINFOS/AUTOGENINFO/ANY_OTH_AUTO_INSU='Y' or 
							INPUTXML/AUTOGENINFOS/AUTOGENINFO/PRIOR_LOSS_Y='Y'  or 
							INPUTXML/AUTOGENINFOS/AUTOGENINFO/PRIOR_LOSS_N='Y' or 
							INPUTXML/AUTOGENINFOS/AUTOGENINFO/MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_VEHICLE_AMOUNT_COM">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/VEHICLE_AMOUNT_COM='Y' ">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_VEHICLE_GRT_15_OT_COLL">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/ANTIQUE_VECH='Y' and (INPUTXML/VEHICLES/VEHICLE/OT_COLL_COUNT!='0')">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_VEHICLE_GRT_15">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/ANTIQUE_VECH='Y' ">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_VEHICLE_CLAIM_ELIGIBLE">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/EXCCESS_CLAIM='Y' ">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_VEHICLE_EXTRA_EQUIP_COV">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/MIS_EQUIP_COV='Y' ">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_LEASED_PURCHASED">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/LEASED_PURCHASED='Y' ">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_ADD_INT_LOAN_LEAN">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/ADD_INT_LOAN_LEAN='Y' ">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_BI_CSL_EXISTS">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/BI_CSL_EXISTS='Y' ">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_NEGATIVE_VIOLATION_POINT">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/NEGATIVE_VIOLATION_POINT='Y' ">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_GOOD_STUDENT_DISCOUNT">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/GOOD_STUDENT_DISCOUNT='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_HOME_AS400_RENEWAL">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/HOME_AS400_RENEWAL='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_BEYOND_50_MILES">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/BEYOND_50_MILES='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_DRIVER_ABOVE_EITEEN">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/DRIVERTURNEITTEEN='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_DRIVER_ABOVE_TWENTYFOUR">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/DRIVERTURNTWENTYFIVE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 1. -->
	<xsl:template name="IS_USE_AS_TRANSPORT_FEE">
		<xsl:choose>
			<xsl:when test="INPUTXML/AUTOGENINFOS/AUTOGENINFO/USE_AS_TRANSPORT_FEE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 2. -->
	<xsl:template name="IS_REFERED2">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/IS_REFERED2='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 3. -->
	<xsl:template name="IS_REFERED3">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/IS_REFERED3='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--  4. -->
	<xsl:template name="IS_COVERAGE_DECLINED">
		<xsl:choose>
			<xsl:when test="INPUTXML/AUTOGENINFOS/AUTOGENINFO/COVERAGE_DECLINED='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 5. -->
	<xsl:template name="IS_US_CITIZEN">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/US_CITIZEN='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 6. -->
	<xsl:template name="IS_REFERED6">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/IS_REFERED6='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 7. -->
	<xsl:template name="IS_MEM_IN_MILITRY">
		<xsl:choose>
			<xsl:when test="INPUTXML/AUTOGENINFOS/AUTOGENINFO/H_MEM_IN_MILITARY='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 8. -->
	<xsl:template name="IS_DISTANT_STUDENT">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/DISTANT_STUDENT='Y' 
			or INPUTXML/DRIVERS/DRIVER/DRIVER_SUPPORTING_DOCUMENT='Y' ">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_PARENT_ELSEWHERE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/DRIVER_PARENT_ELSEWHERE='Y' and INPUTXML/APPLICATIONS/APPLICATION/STATE_ID='22' ">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 9 -->
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
	<!-- 10. -->
	<xsl:template name="IS_DRIVER_SUS_REVOKED">
		<xsl:choose>
			<xsl:when test="INPUTXML/AUTOGENINFOS/AUTOGENINFO/DRIVER_SUS_REVOKED='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 11. -->
	<xsl:template name="IS_VEHICLE_AMOUNT">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/VEHICLE_AMOUNT='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 11. -->
	<xsl:template name="IS_PHY_MENTAL_CHALLENGED">
		<xsl:choose>
			<xsl:when test="INPUTXML/AUTOGENINFOS/AUTOGENINFO/PHY_MENTL_CHALLENGED='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 12. -->
	<xsl:template name="IS_REFERED12">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/IS_REFERED12='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 13. -->
	<xsl:template name="IS_CAR_MODIFIED">
		<xsl:choose>
			<xsl:when test="INPUTXML/AUTOGENINFOS/AUTOGENINFO/CAR_MODIFIED='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 14. -->
	<xsl:template name="IS_REFERED14">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/IS_REFERED14='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 15. -->
	<xsl:template name="IS_MODEL_NAME">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/MODEL_NAME='Y'">
				<xsl:if test="user:VehicleInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:VehicleInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 16. -->
	<xsl:template name="IS_ANY_NON_OWNED_VEH">
		<xsl:choose>
			<xsl:when test="INPUTXML/AUTOGENINFOS/AUTOGENINFO/ANY_NON_OWNED_VEH='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 17. -->
	<xsl:template name="IS_EXISTING_DMG">
		<xsl:choose>
			<xsl:when test="INPUTXML/AUTOGENINFOS/AUTOGENINFO/EXISTING_DMG='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 18. -->
	<xsl:template name="IS_ANY_CAR_AT_SCH">
		<xsl:choose>
			<xsl:when test="INPUTXML/AUTOGENINFOS/AUTOGENINFO/ANY_CAR_AT_SCH='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 19. -->
	<xsl:template name="IS_ANY_OTH_INSU_COMP">
		<xsl:choose>
			<xsl:when test="INPUTXML/AUTOGENINFOS/AUTOGENINFO/ANY_OTH_INSU_COMP='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 20. -->
	<xsl:template name="IS_ANY_FINANCIAL_RESPONSIBILITY">
		<xsl:choose>
			<xsl:when test="INPUTXML/AUTOGENINFOS/AUTOGENINFO/ANY_FINANCIAL_RESPONSIBILITY='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 21. -->
	<!-- <xsl:template name="IS_SAFE_DRIVER">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/SAFE_DRIVER='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template> -->
	<!-- 22. -->
	<xsl:template name="IS_DRIVER_DOB">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/DRIVER_DOB='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 23. -->
	<xsl:template name="IS_CHARGE_OFF_PRMIUM">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/CHARGE_OFF_PRMIUM='Y' 
					or INPUTXML/APPLICATIONS/APPLICATION/PRIOR_POLICY_INFO='Y' or
					INPUTXML/APPLICATIONS/APPLICATION/PRIOR_POLICY_INFO_EXPIRE='Y' or 
					INPUTXML/APPLICATIONS/APPLICATION/RENW_LOSS='Y' or 
					INPUTXML/APPLICATIONS/APPLICATION/COMP_LOSSES='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_SYMBOL">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/SYMBOL='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 1-->
	<xsl:template name="IS_PUNCS">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/PUNCS='Y'">
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 2-->
	<xsl:template name="IS_UNCSL">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/UNCSL='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 3-->
	<xsl:template name="IS_UMPD">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/UMPD='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 4-->
	<xsl:template name="IS_UNDSP">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/UNDSP='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 5-->
	<xsl:template name="IS_PUMSP">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/PUMSP='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0" />
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- -->
	<xsl:template name="IS_IS_OTHER_THAN_INSURED">
		<xsl:choose>
			<xsl:when test="INPUTXML/AUTOGENINFOS/AUTOGENINFO/IS_OTHER_THAN_INSURED='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- -->
	<xsl:template name="IS_DRIVER_LIC_STATE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/DRIVER_LIC_STATE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- -->
	<xsl:template name="IS_DRIVER_LIC_STATE_APP_STATE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/DRIVER_LIC_STATE_APP_STATE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- -->
	<xsl:template name="IS_LLGC">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/LLGC='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- -->
	<xsl:template name="IS_ENO">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/ENO='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_SD_POINTS">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/SD_POINTS='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_AUTO_SD_POINTS">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/AUTO_SD_POINTS='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_RENEW_AUTO_SD_POINTS">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/RENEW_AUTO_SD_POINTS='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_CONT_DRIVER_LICENSE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/CONT_DRIVER_LICENSE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_DRIVER_DRIV_TYPE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DRIVER/DRIVER_DRIV_TYPE='Y' or INPUTXML/DRIVERS/DRIVER/DRIVER_DRIV_TYPE='N'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_AUTO_DRIVER_FAULT">
		<xsl:choose>
			<xsl:when test="INPUTXML/APPLICATIONS/APPLICATION/AUTO_DRIVER_FAULT='Y' or
			 INPUTXML/APPLICATIONS/APPLICATION/NO_AT_FAULT='Y' or 
			 INPUTXML/APPLICATIONS/APPLICATION/EFF_EXP_DATE='Y' or 
			 INPUTXML/APPLICATIONS/APPLICATION/CARRIER='Y' or 
			 INPUTXML/APPLICATIONS/APPLICATION/DFI_ACC_NO_RULE='Y' or 
			 INPUTXML/APPLICATIONS/APPLICATION/CREDIT_CARD='Y' or
			 INPUTXML/APPLICATIONS/APPLICATION/APPEFFECTIVEDATE='Y' or
			 INPUTXML/APPLICATIONS/APPLICATION/TOTAL_PREMIUM_AT_RENEWAL='Y' or
			 INPUTXML/APPLICATIONS/APPLICATION/CLAIM_EFFECTIVE='Y' ">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- -->
	<xsl:template name="IS_EPENDO_SIGN">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/EPENDO_SIGN='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="IS_REG_STATE">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/REG_STATE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- -->
	<xsl:template name="IS_MAKE_NAME">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/MAKE_NAME='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- -->
	<xsl:template name="IS_ANY_EXC_DRIVER_TYPE">
		<xsl:choose>
			<xsl:when test="INPUTXML/DRIVERS/DERIVER/ANY_EXC_DRIVER_TYPE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 17.GrandFather Coverage -->
	<xsl:template name="IS_RF_GRANDFATHER_COV">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/COVERAGE/COV/COV_DES !='' and INPUTXML/VEHICLES/VEHICLE/COVERAGE/WOLVERINE_USER='Y'">
				<xsl:if test="user:VehicleInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:VehicleInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 18.GrandFather Coverage(Limit) -->
	<xsl:template name="IS_RF_GRANDFATHER_LIMIT">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/COVERAGE/LIMIT/LIMIT_DES !='' and INPUTXML/VEHICLES/VEHICLE/COVERAGE/WOLVERINE_USER='Y'">
				<xsl:if test="user:VehicleInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:VehicleInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 18.GrandFather Coverage(Deductible) -->
	<xsl:template name="IS_RF_GRANDFATHER_DEDUCT">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/COVERAGE/DEDUCT/DEDUCT_DES !='' and INPUTXML/VEHICLES/VEHICLE/COVERAGE/DEDUCT/DEDUCT_DES !='N' and INPUTXML/VEHICLES/VEHICLE/COVERAGE/WOLVERINE_USER='Y'">
				<xsl:if test="user:VehicleInfoCount(0) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:VehicleInfoCount(1) = 0"></xsl:if>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- 19.GrandFather Territory() -->
	<xsl:template name="IS_RF_GRANDFATHER_TERR">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/GRANDFATHER_TERRITORY='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--RULE TrailBlazer-->
	<xsl:template name="IS_RF_QUALIFIESTRAIBLAZERPROGRAM">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/QUALIFIESTRAIBLAZERPROGRAM='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--RULE TrailBlazer END-->
	<!--Rule Multidiscount-->
	<xsl:template name="IS_MULTICAR_DISCOUNT_ELIGIBLE">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/MULTICAR_DISCOUNT_ELIGIBLE='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--END Rule Discount-->
	<!-- Added by Charles on 29-Dec-09 for Itrack 6830 -->
	<xsl:template name="IS_UNDERWRITING_TIER">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/VEHICLE/COVERAGE/UNDERWRITING_TIER='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Added till here -->
	<!--End Reffered Templates -->
	<!-- Application Information  -->
	<xsl:template match="INPUTXML/APPLICATIONS/APPLICATION">
		<xsl:choose>
			<!-- Rejected  Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="LESS_INSURANCE_SCORE ='Y' or INELIGIBLE_DRIVER='Y' or 
					 LOSS_AMT_EXCEED='Y' or MVR_VER='Y' or INACTIVE_APPLICATION='Y' or INACTIVE_AGENCY='Y' or OTHER_VEHICLE='Y'">
						<xsl:call-template name="LESS_INSURANCE_SCORE" />
						<xsl:call-template name="INELIGIBLE_DRIVER" />
						<xsl:call-template name="LOSS_AMT_EXCEED" />
						<xsl:call-template name="MVR_VER" />
						<xsl:call-template name="INACTIVE_APPLICATION" />
						<xsl:call-template name="INACTIVE_AGENCY" />
						<xsl:call-template name="OTHER_VEHICLE" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred Info -->
				<xsl:choose>
					<xsl:when test=" CARRIER='Y' or CHARGE_OFF_PRMIUM='Y' or PRIOR_POLICY_INFO='Y' or PRIOR_POLICY_INFO_EXPIRE='Y' or  AUTO_DRIVER_FAULT ='Y' or 
					NO_AT_FAULT='Y' or DFI_ACC_NO_RULE='Y' or CREDIT_CARD='Y' or EFF_EXP_DATE='Y' or COMP_LOSSES='Y' or RENW_LOSS='Y' or APPEFFECTIVEDATE='Y' or TOTAL_PREMIUM_AT_RENEWAL='Y' or CLAIM_EFFECTIVE='Y' or  EXCCESS_CLAIM='Y'">
						<xsl:call-template name="CHARGE_OFF_PRMIUM" />
						<xsl:call-template name="PRIOR_POLICY_INFO" />
						<xsl:call-template name="PRIOR_POLICY_INFO_EXPIRE" />
						<xsl:call-template name="AUTO_DRIVER_FAULT" />
						<xsl:call-template name="NO_AT_FAULT" />
						<xsl:call-template name="EFF_EXP_DATE" />
						<xsl:call-template name="CARRIER" />
						<xsl:call-template name="COMP_LOSSES" />
						<xsl:call-template name="RENW_LOSS" />
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
	<xsl:template match="INPUTXML/VEHICLES">
		<xsl:if test="user:VehicleInfoCount(1) = 0">
			<tr>
				<td>
					<xsl:apply-templates select="VEHICLE" />
				</td>
			</tr>
			<tr>
				<td>
					<!-- <xsl:apply-templates select="COVERAGE" /> --></td>
			</tr>
		</xsl:if>
	</xsl:template>
	<!--Vehicle -->
	<xsl:template match="VEHICLE">
		<xsl:choose>
			<!-- Rejected messages -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test="(GRG_STATE_RULE='Y' or PRINC_OCCA_DRIVER='N' or ANTIQUE_CLASSIC_CAR='Y' or PRINCIPLE_OPERATOR='Y' or BICOMNA='Y')">
						<tr>
							<td class="pageheader" width="36%">For Vehicle:</td>
							<tr>
								<td colspan="4">
									<table>
										<tr>
											<td class="pageheader" width="18%">VIN:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="VIN" />
											</td>
											<td class="pageheader" width="18%">Model:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="MODEL" />
											</td>
										</tr>
										<tr>
											<td class="pageheader" width="18%">Vehicle Year:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="VEHICLE_YEAR" />
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
						<!--Rules-->
						<xsl:call-template name="GRG_STATE_RULE" />
						<xsl:call-template name="PRINC_OCCA_DRIVER" />
						<xsl:call-template name="ANTIQUE_CLASSIC_CAR" />
						<xsl:call-template name="PRINCIPLE_OPERATOR" />
						<xsl:call-template name="COM_BISPLITLIMIT" />
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
						<!-- **********************End  for Indiana only *************************** -->
						<!--/Rules-->
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!--*********************** Checking for referred rules (Messages)********************* -->
				<!-- Vechile Info -->
				<xsl:choose>
					<xsl:when test="VEHICLE_AMOUNT='Y' or SYMBOL='Y' or PUNCS='Y' or 
						UNCSL = 'Y' or UMPD= 'Y' or UNDSP='Y' or PUMSP='Y' or  LLGC ='Y' or EPENDO_SIGN='Y'
						or REG_STATE='Y' or MAKE_NAME='Y' or COMM_RADIUS_OF_USE='Y' or COMM_LOCALHAUL='Y'
						or COMM_INTER_LH='Y' or VEHICLE_AMOUNT_COM='Y' or INSNOW_FULL='Y' or CREG_STATE='Y'
						or MULTICAR_DISCOUNT_ELIGIBLE = 'Y' 
						or MODEL_NAME_MULTI_DIS='Y' or VEHICLE_TYPE_MHT='Y' or YOUTHFUL_PRINC_DRIVER='Y' or ENO='Y' 
						or COMM_LONGHAUL='Y' or GRANDFATHER_TERRITORY='Y' or QUALIFIESTRAIBLAZERPROGRAM = 'Y' or ANTIQUE_VECH='Y' or MIS_EQUIP_COV='Y' or LEASED_PURCHASED='Y' or ADD_INT_LOAN_LEAN='Y' or BI_CSL_EXISTS='Y' or GOOD_STUDENT_DISCOUNT='Y' or HOME_AS400_RENEWAL='Y' or BEYOND_50_MILES='Y'
						or ((COVERAGE/DEDUCT/DEDUCT_DES!='' or COVERAGE/DEDUCT/DEDUCT_DES!='N' or COVERAGE/COV/COV_DES != '' or  COVERAGE/LIMIT/LIMIT_DES!='' or COPY_COVERAGE_AT_RENEWAL='Y') and  COVERAGE/WOLVERINE_USER='Y') or COVERAGE/UNDERWRITING_TIER='Y'">
						<tr>
							<td class="pageheader" width="36%">For Vehicle:</td>
							<tr>
								<td colspan="4">
									<table>
										<tr>
											<td class="pageheader" width="18%">VIN:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="VIN" />
											</td>
											<td class="pageheader" width="18%">Model:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="MODEL" />
											</td>
										</tr>
										<tr>
											<td class="pageheader" width="18%">Vehicle Year:</td>
											<td class="midcolora" width="36%">
												<xsl:value-of select="VEHICLE_YEAR" />
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
						<!-- <xsl:call-template name="MODEL_NAME" /> -->
						<!-- 2. -->
						<!-- <<xsl:call-template name ="REFERED2"></xsl:call-template> -->
						<!-- 3. -->
						<!-- <xsl:call-template name ="REFERED3"></xsl:call-template> -->
						<!-- 6. -->
						<!-- <xsl:call-template name ="REFERED6"></xsl:call-template> -->
						<!-- 10. -->
						<xsl:call-template name="VEHICLE_AMOUNT" />
						<xsl:call-template name="VEHICLE_AMOUNT_COM" />
						<!-- 12. -->
						<!-- <xsl:call-template name ="REFERED12"></xsl:call-template>	 -->
						<!-- 14. -->
						<!-- <xsl:call-template name ="REFERED14"></xsl:call-template>	 -->
						<!-- 15. -->
						<!-- <xsl:call-template name ="REFERED15"/>	 -->
						<xsl:call-template name="PUNCS" />
						<xsl:call-template name="UNCSL" />
						<xsl:call-template name="UMPD" />
						<xsl:call-template name="UNDSP" />
						<xsl:call-template name="PUMSP" />
						<xsl:call-template name="SYMBOL" />
						<xsl:call-template name="LLGC" />
						<xsl:call-template name="EPENDO_SIGN" />
						<xsl:call-template name="REG_STATE" />
						<xsl:call-template name="MAKE_NAME" />
						<xsl:call-template name="COMM_RADIUS_OF_USE" />
						<xsl:call-template name="COMM_LOCALHAUL" />
						<xsl:call-template name="COMM_INTER_LH" />
						<xsl:call-template name="INSNOW_FULL" />
						<xsl:call-template name="CREG_STATE" />
						<xsl:call-template name="MODEL_NAME_MULTI_DIS" />
						<xsl:call-template name="YOUTHFUL_PRINC_DRIVER" />
						<xsl:call-template name="VEHICLE_TYPE_MHT" />
						<xsl:call-template name="COMM_LONGHAUL" />
						<xsl:call-template name="GRANDFATHER_TERRITORY" />
						<xsl:call-template name="QUALIFIESTRAIBLAZERPROGRAM" />
						<xsl:call-template name="VEHICLE_GRT_15" />
						<xsl:call-template name="MIS_EQUIP_COV" />
						<xsl:call-template name="LEASED_PURCHASED" />
						<xsl:call-template name="ADD_INT_LOAN_LEAN" />
						<xsl:call-template name="MULTICAR_DISCOUNT_ELIGIBLE" />
						<xsl:call-template name="BI_CSL_EXISTS" />
						<xsl:call-template name="GOOD_STUDENT_DISCOUNT" />
						<xsl:call-template name="HOME_AS400_RENEWAL" />
						<xsl:call-template name="BEYOND_50_MILES" />
						<xsl:call-template name="ENO" /> <!-- Moved from Driver level Itrack 4771  -->						
						<xsl:apply-templates select="COVERAGE" />
						<xsl:choose>
							<xsl:when test="ANTIQUE_VECH='Y' and (OT_COLL_COUNT!='0')">
								<xsl:call-template name="VEHICLE_GRT_15_OT_COLL" />
							</xsl:when>
						</xsl:choose>
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
					<xsl:when test="COV/COV_DES != '' or  LIMIT/LIMIT_DES!='' or DEDUCT/DEDUCT_DES!='' or DEDUCT/DEDUCT_DES!='N' or COPY_COVERAGE_AT_RENEWAL='Y' or UNDERWRITING_TIER='Y' ">
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
						<xsl:call-template name="UNDERWRITING_TIER" /> <!-- Added by Charles on 29-Dec-09 for Itrack 6830 -->
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
					<xsl:when test=" DRIVER_DRINK_VIOLATION='Y' or MAJOR_VIOLATION='Y' ">
						<tr>
							<td class="pageheader" width="36%">for Driver:</td>
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
												<xsl:if test="DRIVER_DRIV_LIC != 'N'">
													<xsl:value-of select="DRIVER_DRIV_LIC" />
												</xsl:if>
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
						<xsl:call-template name="DRIVER_DRINK_VIOLATION" />
						<xsl:call-template name="MAJOR_VIOLATION" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred Driver Detail -->
				<xsl:choose>
					<xsl:when test=" US_CITIZEN='Y' or DISTANT_STUDENT='Y' or DRIVER_SUPPORTING_DOCUMENT='Y' or DRIVER_DOB='Y' 
								or DRIVER_LIC_STATE='Y' or DRIVER_LIC_STATE_APP_STATE='Y' or DRIVER_DRIV_TYPE='Y' or DRIVER_DRIV_TYPE='N' or WAIVER_WORK_LOSS='Y'
								or YOUTH_DRIVER='Y' or VIOLATION_POINT='Y' or DRV_WITHOUTPOINTS='Y' or 
								DRV_WITHPOINTS='Y' or DRIVER_VOLUNTEER_POLICE_FIRE='Y'
								or DRIVER_PARENT_ELSEWHERE='Y'  or COLLEGE_INSELSE='Y' 
								or COLLEGE_CAR_STATE='Y' or COLLEGE_CAR_STATE_VEHCILE='Y' or PARNT_USTERR_CAR_STA='Y'
								or DRV_MIL_INSELSE='Y' or MI_OLDDRIVER ='Y' or DRIVER_MVR_ORDERED='Y' or  SD_POINTS='Y' or AUTO_SD_POINTS='Y' or RENEW_AUTO_SD_POINTS='Y'
								or CONT_DRIVER_LICENSE='Y' or DRIVERTURNEITTEEN='Y' or DRIVERTURNTWENTYFIVE ='Y'">
						<tr>
							<td class="pageheader">for Driver:</td>
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
												<xsl:if test="DRIVER_DRIV_LIC != 'N'">
													<xsl:value-of select="DRIVER_DRIV_LIC" />
												</xsl:if>
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
						<xsl:call-template name="US_CITIZEN"></xsl:call-template>
						<xsl:call-template name="DISTANT_STUDENT"></xsl:call-template>
						<!-- <xsl:call-template name="DRIVER_SUS_REVOKED"></xsl:call-template> -->
						<!-- <xsl:call-template name="SAFE_DRIVER"></xsl:call-template> -->
						<xsl:call-template name="DRIVER_DOB" />
						<xsl:call-template name="DRIVER_LIC_STATE" />
						<xsl:call-template name="DRIVER_DRIV_TYPE" />
						<xsl:call-template name="WAIVER_WORK_LOSS" />
						<xsl:call-template name="YOUTH_DRIVER" />
						<xsl:call-template name="VIOLATION_POINT" />
						<xsl:call-template name="DRV_WITHOUTPOINTS" />
						<xsl:call-template name="DRV_WITHPOINTS" />
						<xsl:call-template name="DRIVER_PARENT_ELSEWHERE" />
						<xsl:call-template name="DRIVER_MVR_ORDERED" />
						<!--xsl:call-template name="DRIVER_GOOD_STUDENT" /--> <!--ITrack 4593-->
						<xsl:call-template name="COLLEGE_INSELSE" />
						<xsl:call-template name="COLLEGE_CAR_STATE" />
						<xsl:call-template name="COLLEGE_CAR_STATE_VEHCILE" />
						<xsl:call-template name="PARNT_USTERR_CAR_STA" />
						<xsl:call-template name="DRV_MIL_INSELSE" />
						<xsl:call-template name="MI_OLDDRIVER" />
						<!-- xsl:call-template name="ENO" /  Moved to Vehicle level-->
						<xsl:call-template name="DRIVER_VOLUNTEER_POLICE_FIRE" />
						<xsl:call-template name="DRIVER_LIC_STATE_APP_STATE" />
						<xsl:call-template name="DRIVER_SUPPORTING_DOCUMENT" />
						<xsl:call-template name="SD_POINTS" />
						<xsl:call-template name="AUTO_SD_POINTS" />
						<xsl:call-template name="RENEW_SD_POINTS" />
						<xsl:call-template name="CONT_DRIVER_LICENSE" />
						<xsl:call-template name="DRIVER_ABOVE_EITEEN" />
						<xsl:call-template name="DRIVER_ABOVE_TWENTYFOUR" />
						<!-- <xsl:call-template name="NEGATIVE_VIOLATION_POINT" /> -->
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- General Info -->
	<xsl:template match="INPUTXML/AUTOGENINFOS/AUTOGENINFO">
		<xsl:choose>
			<!-- Rejected General Info -->
			<xsl:when test="user:IsApplicationAcceptable(1) = 0">
				<xsl:choose>
					<xsl:when test=" ANY_ANTIQUE_AUTO ='Y' ">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader" width="36%">For Underwriting Questions:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="ANY_ANTIQUE_AUTO" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!-- Referred General Info -->
				<xsl:choose>
					<xsl:when test="COVERAGE_DECLINED='Y' or H_MEM_IN_MILITARY='Y' or PHY_MENTL_CHALLENGED='Y' or CAR_MODIFIED='Y' or
						ANY_NON_OWNED_VEH='Y' or EXISTING_DMG='Y' or ANY_CAR_AT_SCH='Y' or ANY_OTH_INSU_COMP='Y' or ANY_OTH_INSU_COMP='Y'
						or ANY_FINANCIAL_RESPONSIBILITY='Y' or DRIVER_SUS_REVOKED='Y' or USE_AS_TRANSPORT_FEE ='Y'	or IS_OTHER_THAN_INSURED = 'Y'
						or  SALVAGE_TITLE='Y' or ANY_OTH_AUTO_INSU='Y' or PRIOR_LOSS_Y='Y' or PRIOR_LOSS_N='Y' or MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y' ">
						<tr>
							<td colspan="4">
								<table>
									<tr>
										<td class="pageheader" width="36%">For Underwriting Questions:</td>
									</tr>
								</table>
							</td>
						</tr>
						<xsl:call-template name="COVERAGE_DECLINED"></xsl:call-template>
						<xsl:call-template name="H_MEM_IN_MILITARY"></xsl:call-template>
						<xsl:call-template name="PHY_MENTL_CHALLENGED"></xsl:call-template>
						<xsl:call-template name="CAR_MODIFIED"></xsl:call-template>
						<xsl:call-template name="ANY_NON_OWNED_VEH"></xsl:call-template>
						<xsl:call-template name="EXISTING_DMG"></xsl:call-template>
						<xsl:call-template name="ANY_CAR_AT_SCH"></xsl:call-template>
						<xsl:call-template name="ANY_OTH_INSU_COMP"></xsl:call-template>
						<xsl:call-template name="ANY_FINANCIAL_RESPONSIBILITY"></xsl:call-template>
						<xsl:call-template name="DRIVER_SUS_REVOKED"></xsl:call-template>
						<xsl:call-template name="USE_AS_TRANSPORT_FEE"></xsl:call-template>
						<xsl:call-template name="IS_OTHER_THAN_INSURED"></xsl:call-template>
						<xsl:call-template name="SALVAGE_TITLE" />
						<xsl:call-template name="ANY_OTH_AUTO_INSU" />
						<xsl:call-template name="PRIOR_LOSS_N" />
						<xsl:call-template name="PRIOR_LOSS_Y" />
						<xsl:call-template name="MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- *************** Start Templates for showing rejected rules messages **************** -->
	<!--1. -->
	<xsl:template name="DRIVER_DRINK_VIOLATION">
		<xsl:choose>
			<xsl:when test="DRIVER_DRINK_VIOLATION ='Y'">
				<TR>
					<td class="midcolora">Convicted of a major violation in the preceding 5 years.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--2. -->
	<xsl:template name="REJECT2">
		<xsl:choose>
			<xsl:when test="REJECT2 ='Y'">
				<TR>
					<td class="midcolora">Convicted of a major violation in the preceding 5 years.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--3. -->
	<xsl:template name="REJECT3">
		<xsl:choose>
			<xsl:when test="REJECT3 ='Y'">
				<TR>
					<td class="midcolora">Convicted of a major violation in the preceding 5 years.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--4. -->
	<xsl:template name="REJECT4">
		<xsl:choose>
			<xsl:when test="REJECT4 ='Y'">
				<TR>
					<td class="midcolora">Convicted of a major violation in the preceding 5 years.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--5. -->
	<xsl:template name="REJECT5">
		<xsl:choose>
			<xsl:when test="REJECT5 ='Y'">
				<TR>
					<td class="midcolora">Convicted of a major violation in the preceding 5 years.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--6. -->
	<xsl:template name="REJECT6">
		<xsl:choose>
			<xsl:when test="REJECT6='Y'">
				<TR>
					<td class="midcolora">Convicted of a major violation in the preceding 5 years.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--7. -->
	<xsl:template name="CONT_DRIVER_LICENSE">
		<xsl:choose>
			<xsl:when test="CONT_DRIVER_LICENSE ='Y'">
				<TR>
					<td class="midcolora">Has not had a valid, continuous drivers license for past 12 months.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--8. -->
	<xsl:template name="REJECT8">
		<xsl:choose>
			<xsl:when test="REJECT8 ='Y'">
				<TR>
					<td class="midcolora">Convicted of a major violation in the preceding 5 years.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--9. -->
	<xsl:template name="REJECT9">
		<xsl:choose>
			<xsl:when test="REJECT9 ='Y'">
				<TR>
					<td class="midcolora">Convicted of a major violation in the preceding 5 years.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--10. -->
	<xsl:template name="REJECT10">
		<xsl:choose>
			<xsl:when test="REJECT10 ='Y'">
				<TR>
					<td class="midcolora">Convicted of a major violation in the preceding 5 years.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--11. -->
	<xsl:template name="REJECT11">
		<xsl:choose>
			<xsl:when test="REJECT11 ='Y'">
				<TR>
					<td class="midcolora">Convicted of a major violation in the preceding 5 years.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- MAJOR_VIOLATION-->
	<!--12. -->
	<xsl:template name="MAJOR_VIOLATION">
		<xsl:choose>
			<xsl:when test="MAJOR_VIOLATION ='Y'">
				<TR>
					<td class="midcolora">Convicted of major violation be eligible 5 years from the conviction date of the violation.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--13. -->
	<xsl:template name="SALVAGE_TITLE">
		<xsl:choose>
			<xsl:when test="SALVAGE_TITLE='Y'">
				<TR>
					<td class="midcolora">Has a salvage title.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!-- -->
	<xsl:template name="ANY_OTH_AUTO_INSU">
		<xsl:choose>
			<xsl:when test="ANY_OTH_AUTO_INSU='Y'">
				<TR>
					<td class="midcolora">Other auto insurance in the household.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PRIOR_LOSS_Y">
		<xsl:choose>
			<xsl:when test="PRIOR_LOSS_Y='Y'">
				<TR>
					<td class="midcolora">Prior Losses occurred but no information provided.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PRIOR_LOSS_N">
		<xsl:choose>
			<xsl:when test="PRIOR_LOSS_N='Y'">
				<TR>
					<td class="midcolora">Prior Losses not occurred but information provided.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS">
		<xsl:choose>
			<xsl:when test="MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y'">
				<TR>
					<td class="midcolora">Selected(entered) Policy in Multipolicy Discount Description is(are) no eligible for discount.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--13. -->
	<xsl:template name="REJECT13">
		<xsl:choose>
			<xsl:when test="REJECT13 ='Y'">
				<TR>
					<td class="midcolora">Convicted of a major violation in the preceding 5 years.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="GRG_STATE_RULE">
		<xsl:choose>
			<xsl:when test="GRG_STATE_RULE ='Y'">
				<TR>
					<td class="midcolora"> Garaging state of all vehicle must be same as of main application state .</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PRINC_OCCA_DRIVER">
		<xsl:choose>
			<xsl:when test="PRINC_OCCA_DRIVER ='N'">
				<TR>
					<td class="midcolora"> Vehicle not having principal or youthful principal or youthful occasional driver.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ANTIQUE_CLASSIC_CAR">
		<xsl:choose>
			<xsl:when test="ANTIQUE_CLASSIC_CAR ='Y'">
				<TR>
					<td class="midcolora">Ineligible Vehicle Type Antique Car/Classic Car.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PRINCIPLE_OPERATOR">
		<xsl:choose>
			<xsl:when test="PRINCIPLE_OPERATOR ='Y'">
				<TR>
					<td class="midcolora">Vehicle has more than one principal driver.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="COM_BISPLITLIMIT">
		<xsl:choose>
			<xsl:when test="BICOMNA ='Y'">
				<TR>
					<td class="midcolora">Policy has been rejected - Commercial vehicles not eligible for $300/$500,000 Bodily Injury.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
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
	<xsl:template name="OTHER_VEHICLE">
		<xsl:if test="OTHER_VEHICLE='Y'">
			<TR>
				<td class="midcolora">App/Pol must have at least one of any other vehicles types other then Utility Trailer/Camper &amp; Trailer/Trailer.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- ***************End  Templates for showing rejected rules messages **************** -->
	<!--End Rejected Templates -->
	<!-- *************** Templates for showing refered rules messages **************** -->
	<!--1 -->
	<xsl:template name="REFERED1">
		<xsl:if test="REFERED1='Y'">
			<TR>
				<td class="midcolora">On new business, the accumulation of more than 5 eligibility points during the preceding 3 years for any one operator in the insured household.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--2 -->
	<xsl:template name="REFERED2">
		<xsl:if test="REFERED2='Y'">
			<TR>
				<td class="midcolora">Failing to disclose all losses, moving violations, or claims occurring in the preceding 5 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--3 -->
	<xsl:template name="REFERED3">
		<xsl:if test="REFERED3='Y'">
			<TR>
				<td class="midcolora">Failing to prove that required insurance was maintained in force with respect to the vehicle during the preceding 6 months.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--4 -->
	<xsl:template name="COVERAGE_DECLINED">
		<xsl:if test="COVERAGE_DECLINED='Y'">
			<TR>
				<td class="midcolora">Coverage been declined, cancelled (including for Non Payment) or non-renewed during the last 3 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--5. -->
	<xsl:template name="US_CITIZEN">
		<xsl:if test="US_CITIZEN='Y'">
			<TR>
				<td class="midcolora">Is not a U.S. Citizen.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--6. -->
	<xsl:template name="REFERED6">
		<xsl:if test="REFERED6='Y'">
			<TR>
				<td class="midcolora">Does not have a valid state driver license.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--7. -->
	<xsl:template name="H_MEM_IN_MILITARY">
		<xsl:if test="H_MEM_IN_MILITARY='Y'">
			<TR>
				<!-- <td class="midcolora">Military career personnel (unless permanently assigned in State).</td> -->
				<td class="midcolora">Household member using vehicle out of State over 6 months a year.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--8. -->
	<xsl:template name="DISTANT_STUDENT">
		<xsl:if test="DISTANT_STUDENT='Y'">
			<TR>
				<td class="midcolora">College students.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--9. -->
	<xsl:template name="DRIVER_SUPPORTING_DOCUMENT">
		<xsl:if test="DRIVER_SUPPORTING_DOCUMENT='Y'">
			<TR>
				<td class="midcolora">Good Student with no Supporting Document.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--10. -->
	<xsl:template name="DRIVER_SUS_REVOKED">
		<xsl:if test="DRIVER_SUS_REVOKED='Y'">
			<TR>
				<td class="midcolora">Drivers license been Suspended, restricted or revoked.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--11. -->
	<xsl:template name="VEHICLE_AMOUNT">
		<xsl:if test="VEHICLE_AMOUNT='Y'">
			<TR>
				<td class="midcolora">Value in excess of $80,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="VEHICLE_GRT_15">
		<xsl:if test="ANTIQUE_VECH='Y'">
			<TR>
				<td class="midcolora">Vehicle (+ 15 Years) will be subject to underwriter approval to determine physical damage coverage eligibility.</td>
			</TR>
			<TR>
				<td class="midcolora">Vehicle (+ 15 Years) written with Physical Damage coverages (Comprehensive and/or Collision) will require a photo.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="VEHICLE_GRT_15_OT_COLL">
		<xsl:if test="ANTIQUE_VECH='Y' and (OT_COLL_COUNT!='0')">
			<TR>
				<td class="midcolora">Vehicle is over 15 years of age and has the coverage Other than Collision (comprehensive).</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="MIS_EQUIP_COV">
		<xsl:if test="MIS_EQUIP_COV='Y'">
			<TR>
				<td class="pageheader">Miscellaneous Equipment:</td>
			</TR>
			<TR>
				<td class="midcolora">No coverage has been applied to the Miscellaneous Equipment.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="LEASED_PURCHASED">
		<xsl:if test="LEASED_PURCHASED='Y'">
			<TR>
				<td class="midcolora">Loan/Lease Gap coverage A-11 selected - no purchase date or purchase date is over 90 days.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="ADD_INT_LOAN_LEAN">
		<xsl:if test="ADD_INT_LOAN_LEAN='Y'">
			<TR>
				<td class="midcolora">No Additional Interest attached to vehicle with the loan lease coverage.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="BI_CSL_EXISTS">
		<xsl:if test="BI_CSL_EXISTS='Y'">
			<TR>
				<td class="pageheader">Coverages:</td>
			</TR>
			<TR>
				<!-- <td class="midcolora">Any of the following coverages are not checked off.</td> -->
				<td class="midcolora">Few/all of the following coverages are available but not checked off.</td>
			</TR>
			<xsl:if test="IS_CSL='Y'">
				<TR>
					<td class="midcolora">Uninsured Motorist (CSL).</td>
				</TR>
				<TR>
					<td class="midcolora">Underinsured Motorist (CSL).</td>
				</TR>
				<TR>
					<td class="midcolora">Uninsured Motorist PD.</td>
				</TR>
			</xsl:if>
			<xsl:if test="IS_CSL='N'">
				<TR>
					<td class="midcolora">Uninsured Motorist (BI-Split Limit).</td>
				</TR>
				<TR>
					<td class="midcolora">Underinsured Motorist (BI- Split Limit).</td>
				</TR>
				<TR>
					<td class="midcolora">Uninsured Motorist PD.</td>
				</TR>
			</xsl:if>
		</xsl:if>
	</xsl:template>
	<xsl:template name="BEYOND_50_MILES">
		<xsl:if test="BEYOND_50_MILES='Y'">
			<TR>
				<td class="midcolora">Risk located beyond 50 miles from agency.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="NEGATIVE_VIOLATION_POINT">
		<xsl:if test="NEGATIVE_VIOLATION_POINT='Y'">
			<TR>
				<td class="midcolora">Violations with Negative values.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="GOOD_STUDENT_DISCOUNT">
		<xsl:if test="GOOD_STUDENT_DISCOUNT='Y'">
			<TR>
				<td class="midcolora">Rated Driver is set to Yes for Good Student but Not Eligible for Good Student Discount.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="HOME_AS400_RENEWAL">
		<xsl:if test="HOME_AS400_RENEWAL='Y'">
			<TR>
				<td class="midcolora">Compare Deductibles with the AS400.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="EXCCESS_CLAIM">
		<xsl:if test="EXCCESS_CLAIM='Y'">
			<TR>
				<!--Message Modified by Sibin on 2 Feb 2009 for Itrack Issue 5381-->
				<td class="midcolora">A risk has incurred an excessive number (5 or more) paid claims within the past five years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="PHY_MENTL_CHALLENGED">
		<xsl:if test="PHY_MENTL_CHALLENGED='Y'">
			<TR>
				<td class="midcolora">Driver physically or mentally impaired.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--11. -->
	<xsl:template name="VEHICLE_AMOUNT_COM">
		<xsl:if test="VEHICLE_AMOUNT_COM='Y'">
			<TR>
				<td class="midcolora">Value in excess of $50,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--12. -->
	<xsl:template name="REFERED12">
		<xsl:if test="REFERED12='Y'">
			<TR>
				<td class="midcolora">Vehicle is titled to an individual other than the applicant.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--13. -->
	<!--  Modified meaning any vehicle modified for speed or with raised rear-end suspension or lowered front end. -->
	<xsl:template name="CAR_MODIFIED">
		<xsl:if test="CAR_MODIFIED='Y'">
			<TR>
				<td class="midcolora">Car other than Customized truck or van has been modified, assembled or kit vehicle or have special equipment. </td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--14. -->
	<xsl:template name="REFERED14">
		<xsl:if test="REFERED14='Y'">
			<TR>
				<td class="midcolora">Corvette.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--15. -->
	<xsl:template name="REFERED15">
		<xsl:if test="REFERED15='Y'">
			<TR>
				<td class="midcolora">Value in excess of $80,000.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--16. -->
	<!-- Any vehicles not solely owned -->
	<xsl:template name="ANY_NON_OWNED_VEH">
		<xsl:if test="ANY_NON_OWNED_VEH='Y'">
			<TR>
				<td class="midcolora">Vehicle(s) not solely owned.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--17. -->
	<!-- Vehicles with existing damage (including damaged glass) -->
	<xsl:template name="EXISTING_DMG">
		<xsl:if test="EXISTING_DMG='Y'">
			<TR>
				<td class="midcolora">Vehicle(s) with existing damage.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--18. -->
	<!--Any car kept at school -->
	<xsl:template name="ANY_CAR_AT_SCH">
		<xsl:if test="ANY_CAR_AT_SCH='Y'">
			<TR>
				<td class="midcolora">Vehicle(s) kept at school.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--19. -->
	<!-- Any other auto insurance in household -->
	<xsl:template name="ANY_OTH_INSU_COMP">
		<xsl:if test="ANY_OTH_INSU_COMP='Y'">
			<TR>
				<td class="midcolora">Other Auto insurance in household.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--20. -->
	<!--Any financial responsibility filing -->
	<xsl:template name="ANY_FINANCIAL_RESPONSIBILITY">
		<xsl:if test="ANY_FINANCIAL_RESPONSIBILITY='Y'">
			<TR>
				<td class="midcolora">Financial responsibility filing.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--21. -->
	<!-- <xsl:template name="SAFE_DRIVER">
		<xsl:if test="SAFE_DRIVER='Y'">
			<TR>
				<td class="midcolora">Any rollover policy.</td>
			</TR>
		</xsl:if>
	</xsl:template> -->
	<!--22. -->
	<!-- On new business,the accumulation of more than 5 eligibility points during the preceding 3 years for any one operator in the insureds household. -->
	<xsl:template name="SD_POINTS">
		<xsl:if test="SD_POINTS='Y'">
			<TR>
				<td class="midcolora">Accumulation of more than 5 eligibility points during the preceding 3 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- when an DRIVER has an ineligible automobile driving record.Same rules as auto program apply (5 points in 3 years , 5 years for major violations for new business, and 8 points for renewals Major violation(with in 5 years) and Minor Violation(with in 3 years) is/are ) -->
	<xsl:template name="AUTO_SD_POINTS">
		<xsl:if test="AUTO_SD_POINTS='Y'">
			<TR>
				<td class="midcolora">Accumulation of more than 5 eligibility points or negative points.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Minor violations at Renewal. -->
	<xsl:template name="RENEW_SD_POINTS">
		<xsl:if test="RENEW_AUTO_SD_POINTS='Y'">
			<TR>
				<td class="midcolora">Accumulation of more than 5 eligibility points or negative points.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="ANY_ANTIQUE_AUTO">
		<xsl:if test="ANY_ANTIQUE_AUTO='Y'">
			<TR>
				<td class="midcolora">Vehicle(s) is an antique car.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="COMM_LONGHAUL">
		<xsl:if test="COMM_LONGHAUL='Y'">
			<TR>
				<td class="midcolora">Long Haul vehicle type is used as commercial.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Any vehicle considered an antique car?Mandatory question If yes On new business - Risk is Declined 
	Grandfathered for existing policies only where the inception date is prior to 01/01/2003 -->
	<!-- 23. -->
	<xsl:template name="MODEL_NAME">
		<xsl:if test="MODEL_NAME='Y'">
			<TR>
				<td class="midcolora">Model of Vehicle is Corvette.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- 24. -->
	<!-- Principal driver who is under the age of 21, unless the parents are already with Company or application accompanies underage risk -->
	<xsl:template name="DRIVER_DOB">
		<xsl:if test="DRIVER_DOB='Y'">
			<TR>
				<td class="midcolora">Driver is under the age of 21.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- 25. -->
	<!-- Any rollover policy -->
	<xsl:template name="CHARGE_OFF_PRMIUM">
		<xsl:if test="CHARGE_OFF_PRMIUM='Y'">
			<TR>
				<td class="midcolora">Is Rollover policy.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<!-- Billing Information-->
	<xsl:template name="DFI_ACC_NO_RULE">
		<xsl:if test="DFI_ACC_NO_RULE='Y'">
			<TR>
				<td class="midcolora">Complete DFI Account Number or Transit/Routing Number in Billing Info.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Credit Card-->
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
	<!-- TOTAL Premium Date-->
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
	<!--27 -->
	<xsl:template name="PRIOR_POLICY_INFO">
		<xsl:if test="PRIOR_POLICY_INFO='Y'">
			<TR>
				<td class="midcolora">Must have at least one Prior Policy for auto.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="PRIOR_POLICY_INFO_EXPIRE">
		<xsl:if test="PRIOR_POLICY_INFO_EXPIRE='Y'">
			<TR>
				<td class="midcolora">All prior policy is/are expiring before NBS effective date.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="SYMBOL">
		<xsl:if test="SYMBOL='Y'">
			<TR>
				<td class="midcolora">Symbol exceeds the limit of 26.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="USE_AS_TRANSPORT_FEE">
		<xsl:if test="USE_AS_TRANSPORT_FEE='Y'">
			<TR>
				<td class="midcolora">Vehicle(s) used for Livery, rental, passenger hire, or to transport persons to work for a fee.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="IS_OTHER_THAN_INSURED">
		<xsl:if test="IS_OTHER_THAN_INSURED='Y'">
			<TR>
				<td class="midcolora">Other licensed drivers in the household that are not listed or rated on this policy.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="AUTO_DRIVER_FAULT">
		<xsl:if test="AUTO_DRIVER_FAULT='Y'">
			<TR>
				<td class="midcolora">Insufficient driver information  for an Automobile loss (driver/fault).</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="NO_AT_FAULT">
		<xsl:if test="NO_AT_FAULT='Y'">
			<TR>
				<td class="midcolora">More than 5  losses, within the last 5 years, where the applicant was not at fault.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="EFF_EXP_DATE">
		<xsl:if test="EFF_EXP_DATE='Y'">
			<TR>
				<td class="midcolora">Difference between prior-policy effective date and expiration date is greater than 06 months.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="CARRIER">
		<xsl:if test="CARRIER='Y'">
			<TR>
				<td class="midcolora">Prior carrier is not filled.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="RENW_LOSS">
		<xsl:if test="RENW_LOSS='Y'">
			<TR>
				<td class="midcolora">Prior Losses occurred.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="COMP_LOSSES">
		<xsl:if test="COMP_LOSSES='Y'">
			<TR>
				<td class="midcolora">More than 5 comprehensive losses, within the last 5 years.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<xsl:template name="DRIVER_LIC_STATE">
		<xsl:if test="DRIVER_LIC_STATE='Y'">
			<TR>
				<td class="midcolora">License State is not Michigan.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="DRIVER_LIC_STATE_APP_STATE">
		<xsl:if test="DRIVER_LIC_STATE_APP_STATE='Y'">
			<TR>
				<td class="midcolora">License State is not same as that of application state.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="DRIVER_DRIV_TYPE">
		<xsl:if test="DRIVER_DRIV_TYPE='Y'">
			<TR>
				<td class="midcolora">Excluded Driver with signed form.</td>
			</TR>
		</xsl:if>
		<xsl:if test="DRIVER_DRIV_TYPE='N'">
			<TR>
				<td class="midcolora">Excluded Driver with no signed form.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="WAIVER_WORK_LOSS">
		<xsl:if test="WAIVER_WORK_LOSS='Y'">
			<TR>
				<td class="midcolora">Driver having Waiver Work Loss Benefits without Signed Waiver of Benefits Form.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="VIOLATION_POINT">
		<xsl:if test="VIOLATION_POINT='Y'">
			<TR>
				<td class="midcolora">Violation(s) having more than 5 points or negative points.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="DRV_WITHPOINTS">
		<xsl:if test="DRV_WITHPOINTS='Y'">
			<TR>
				<td class="midcolora">Driver is assigned "with points" and no MVR information is provided.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="DRIVER_VOLUNTEER_POLICE_FIRE">
		<xsl:if test="DRIVER_VOLUNTEER_POLICE_FIRE='Y'">
			<TR>
				<td class="midcolora">Driver is volunteer Fireman or Policeman.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="DRIVER_PARENT_ELSEWHERE">
		<xsl:if test="DRIVER_PARENT_ELSEWHERE='Y'">
			<TR>
				<td class="midcolora">Driver is under the age of 21 and Parents Insured Elsewhere.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--  -->
	<xsl:template name="DRIVER_MVR_ORDERED">
		<xsl:if test="DRIVER_MVR_ORDERED='Y'">
			<TR>
				<td class="midcolora">MVR Ordered is Yes and MVR Information not available or vice versa or MVR Ordered is No or Blank.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--  -->
	<xsl:template name="DRIVER_ABOVE_EITEEN">
		<xsl:if test="DRIVERTURNEITTEEN='Y'">
			<TR>
				<td class="midcolora">Youthful driver now 18 years of age.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<!--  -->
	<xsl:template name="DRIVER_ABOVE_TWENTYFOUR">
		<xsl:if test="DRIVERTURNTWENTYFIVE='Y'">
			<TR>
				<td class="midcolora">Youthful driver now 25 years of age.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="DRIVER_GOOD_STUDENT">
		<xsl:if test="DRIVER_GOOD_STUDENT='Y'">
			<TR>
				<td class="midcolora">Driver is under the age of 25 and not a Good Student.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="COLLEGE_INSELSE">
		<xsl:if test="COLLEGE_INSELSE='Y'">
			<TR>
				<td class="midcolora">Driver is college student and Parents Insured Elsewhere.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="COLLEGE_CAR_STATE">
		<xsl:if test="COLLEGE_CAR_STATE='Y'">
			<TR>
				<td class="midcolora">Driver is a college student having car and License state is not same as application state.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="COLLEGE_CAR_STATE_VEHCILE">
		<xsl:if test="COLLEGE_CAR_STATE_VEHCILE='Y'">
			<TR>
				<td class="midcolora">Driver is a college student and assigned vehicle registered state is not same as application state.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="DRV_MIL_INSELSE">
		<xsl:if test="DRV_MIL_INSELSE='Y'">
			<TR>
				<td class="midcolora">Driver is under the age of 25 years is in Military and Parents Insured Elsewhere.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="MI_OLDDRIVER">
		<xsl:if test="MI_OLDDRIVER='Y'">
			<TR>
				<td class="midcolora">Driver age is over 60 and Waiver of Loss is Yes and A-94 not available or vice versa.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="PARNT_USTERR_CAR_STA">
		<xsl:if test="PARNT_USTERR_CAR_STA='Y'">
			<TR>
				<td class="midcolora">Driver is stationed in US or  Canada or Puerto Rico or other US Territories and having car , Licensed state is not same as application state and Parents Insured Elsewhere.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="DRV_WITHOUTPOINTS">
		<xsl:if test="DRV_WITHOUTPOINTS='Y'">
			<TR>
				<td class="midcolora">Driver is assigned "with no points" and MVR information is provided.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="YOUTH_DRIVER">
		<xsl:if test="YOUTH_DRIVER='Y'">
			<TR>
				<td class="midcolora">Youthful driver(s) who is/are either
				i. Not stationed in the Policy Territory(United States, its territories and possessions, Puerto Rico and Canada)Or
				ii. Have a car with them on base.</td>
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
	<xsl:template name="LLGC">
		<xsl:if test="LLGC='Y'">
			<TR>
				<td class="midcolora">Loan/Lease Gap Coverage (A-11)is checked off.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="ENO">
		<xsl:if test="ENO='Y'">
			<TR>
				<td class="midcolora">Number of drivers having "Extended Non Owned Coverages Required" must be equal to the number mentioned 
							against the Coverage "Extended Non- Owned Coverage" in the Coverage section.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="EPENDO_SIGN">
		<xsl:if test="EPENDO_SIGN='Y' and //INPUTXML/APPLICATIONS/APPLICATION/STATE_ID='14'">
			<TR>
				<!--	<td class="midcolora">Excluded Person(s)Endorsement A-96 is checked off not having signature and  without Excluded driver.</td> -->
				<td class="midcolora">Excluded Person(s)Endorsement A-96 coverage not selected for Excluded Driver with signed form or vice-versa.</td>
			</TR>
		</xsl:if>
		<xsl:if test="EPENDO_SIGN='Y' and //INPUTXML/APPLICATIONS/APPLICATION/STATE_ID='22'">
			<TR>
				<td class="midcolora">Excluded Person(s)Endorsement A-95 coverage not selected for Excluded Driver with signed form or vice-versa.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="REG_STATE">
		<xsl:if test="REG_STATE='Y'">
			<TR>
				<td class="midcolora">Vehicle Registered State is other than Application State.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="MAKE_NAME">
		<xsl:if test="MAKE_NAME='Y'">
			<TR>
				<td class="midcolora">Make of Vehicle (Delorean, Ferrari, GEM, Lamborghini, Maserati, Rantera or Shelby Cobra).</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="COMM_RADIUS_OF_USE">
		<xsl:if test="COMM_RADIUS_OF_USE='Y'">
			<TR>
				<td class="midcolora">Vehicle is being used as commercial having radius greater than 150.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="COMM_LOCALHAUL">
		<xsl:if test="COMM_LOCALHAUL='Y'">
			<TR>
				<td class="midcolora">Local Haul vehicle is being used as commercial having radius greater than 75.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="COMM_INTER_LH">
		<xsl:if test="COMM_INTER_LH='Y'">
			<TR>
				<td class="midcolora">Local Haul - Intermittent vehicle is being used as commercial not having radius between 75 and 150.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="INSNOW_FULL">
		<xsl:if test="INSNOW_FULL='Y'">
			<TR>
				<td class="midcolora">Used for Full time snowplowing.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="CREG_STATE">
		<xsl:if test="CREG_STATE='Y'">
			<TR>
				<td class="midcolora">Vehicle garaging state or registered state is other than application state.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="MODEL_NAME_MULTI_DIS">
		<xsl:if test="MODEL_NAME_MULTI_DIS='Y'">
			<TR>
				<td class="midcolora">Model is corvette and policy does not offer multi policy discount.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="YOUTHFUL_PRINC_DRIVER">
		<xsl:if test="YOUTHFUL_PRINC_DRIVER='Y'">
			<TR>
				<td class="midcolora">Vehicle not having principal or youthful principal driver.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="VEHICLE_TYPE_MHT">
		<xsl:if test="VEHICLE_TYPE_MHT='Y'">
			<TR>
				<td class="midcolora">Vehicle Type is Motor Home,Truck or Van Camper and used for Business or Permanent Residence.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="LESS_INSURANCE_SCORE">
		<xsl:if test="LESS_INSURANCE_SCORE='Y'">
			<TR>
				<td class="midcolora">For trailblazer vehicle the insurance score of customer/applicant is less than 700.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="INELIGIBLE_DRIVER">
		<xsl:if test="INELIGIBLE_DRIVER='Y'">
			<TR>
				<td class="midcolora">For trailblazer vehicle the age of Named Insured does not lie between 35-69.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="LOSS_AMT_EXCEED">
		<xsl:if test="LOSS_AMT_EXCEED='Y'">
			<TR>
				<td class="midcolora">For trailblazer vehicle the prior loss amount for one/more  losses within 3 yrs exceeds $75.00.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- -->
	<xsl:template name="MVR_VER">
		<xsl:if test="MVR_VER='Y'">
			<TR>
				<td class="midcolora">For trailblazer vehicle, one or  more driver is under 35 yrs and the violation is not ordered.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Multicar Discount-->
	<xsl:template name="MULTICAR_DISCOUNT_ELIGIBLE">
		<xsl:if test="MULTICAR_DISCOUNT_ELIGIBLE='Y'">
			<TR>
				<td class="midcolora">Other car on policy may be eligible for Multi Car discount.</td>
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
	<!-- Added by Charles on 29-Dec-09 for Itrack 6830 -->
	<xsl:template name="UNDERWRITING_TIER">
		<xsl:if test="UNDERWRITING_TIER='Y'">
			<TR>
				<td class="midcolora">Must have an Underwriting Tier in order to process the policy.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!-- Added till here -->
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
			<xsl:if test="DEDUCT_DES!='' and DEDUCT_DES!='N' ">
				<TR>
					<td class="midcolora">
					    Deductible selected for '<xsl:value-of select="DEDUCT_DES" />' is ineligible.
					</td>
				</TR>
			</xsl:if>
		</xsl:for-each>
	</xsl:template>
	<!--GrandFathered Limit (Rjected and Refered)-->
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
	<!--TrailBlazer Rule-->
	<xsl:template name="QUALIFIESTRAIBLAZERPROGRAM">
		<xsl:if test="QUALIFIESTRAIBLAZERPROGRAM='Y'">
			<TR>
				<td class="midcolora">Policy is no longer eligible for trailblazer.</td>
			</TR>
		</xsl:if>
	</xsl:template>
	<!--TrailBlazer Rule END-->
	<!--End Reffered Templates -->
	<xsl:template name="VEHICLETOP">
		<tr>
			<td colspan="4">
				<table>
					<tr>
						<td class="pageheader" width="18%">VIN:</td>
						<td class="midcolora" width="36%">
							<xsl:value-of select="VEHICLE/VIN" />
						</td>
						<td class="pageheader" width="18%">Model:</td>
						<td class="midcolora" width="36%">
							<xsl:value-of select="VEHICLE/MODEL" />
						</td>
					</tr>
					<tr>
						<td class="pageheader" width="18%">Vehicle Year:</td>
						<td class="midcolora" width="36%">
							<xsl:value-of select="VEHICLE/VEHICLE_YEAR" />
						</td>
						<td class="pageheader" width="18%">Make:</td>
						<td class="midcolora" width="36%">
							<xsl:value-of select="VEHICLE/MAKE" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</xsl:template>
</xsl:stylesheet>