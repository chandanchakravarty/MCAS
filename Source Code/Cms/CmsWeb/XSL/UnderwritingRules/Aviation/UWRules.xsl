<!--	Check  for the Aviation Rules-->
<!--	Name : Pravesh K Chandel  -->
<!--	Date : 15 Jan.,2010  -->
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
								<xsl:apply-templates select="INPUTXML/VEHICLES/VEHICLE"></xsl:apply-templates>
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
		<!--  3  -->
		<xsl:call-template name="IS_RJ_APP" />
		<xsl:call-template name="IS_RJ_INACTIVE_APPLICATION" />
		<xsl:call-template name="IS_RJ_INACTIVE_AGENCY" />
		<xsl:call-template name="IS_CRAFT_CATEGORY_OTHER" />
		<!--**********************End for Indiana only *************************** -->
	</xsl:template>
	<!-- End Checking for rejected rules -->
	<!--Start Rejected Templates -->
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
	<!-- 1. -->
	<xsl:template name="IS_CRAFT_CATEGORY_OTHER">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLE/CRAFT_CATEGORY_OTHER='Y'">
				<xsl:if test="user:IsApplicationAcceptable(0) = 0"></xsl:if>
			</xsl:when>
			<xsl:otherwise>
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
	<!-- =========================End Rejected Templates ======================================== -->
	<!--========================= Start checking for referred Cases============================== -->
	<xsl:template name="AUTO_REFERRED_CASES">
		<!-- 1. -->
		<xsl:call-template name="IS_DISCOUNT"></xsl:call-template>
	</xsl:template>
	<!--Start Reffered Templates -->
	<xsl:template name="IS_DISCOUNT">
		<xsl:choose>
			<xsl:when test="INPUTXML/VEHICLES/IS_DISCOUNT='Y'">
				<xsl:if test="user:CheckRefer(0) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(0) = 0" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="user:CheckRefer(1) = 0"></xsl:if>
				<xsl:if test="user:VehicleInfoCount(1) = 0" />
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
					<xsl:when test="(CRAFT_CATEGORY_OTHER='Y')">
						<tr>
							<td class="pageheader" width="36%">For Vehicle:</td>
							<tr>
								<td colspan="4">
									<table>
										<tr>
											<td class="pageheader" width="18%">VIN:</td>
											<td class="midcolora" width="36%">
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
						<xsl:call-template name="OTHER_CRAFT" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="user:CheckRefer(1) = 0">
				<!--*********************** Checking for referred rules (Messages)********************* -->
				<!-- Vechile Info -->
				<xsl:choose>
					<xsl:when test=" IS_DISCOUNT='Y'">
						<tr>
							<td class="pageheader" width="36%">For Vehicle:</td>
							<tr>
								<td colspan="4">
									<table>
										<tr>
											<td class="pageheader" width="18%">VIN:</td>
											<td class="midcolora" width="36%">
												
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
						<xsl:call-template name="DISCOUNT" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="OTHER_CRAFT">
		<xsl:choose>
			<xsl:when test="OTHER_CRAFT='Y'">
				<TR>
					<td class="midcolora">Air Craft Category is Other.</td>
				</TR>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DISCOUNT">
		<xsl:choose>
			<xsl:when test="DISCOUNT='Y'">
				<TR>
					<td class="midcolora">Discount is not Eligible.</td>
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
		<xsl:if test="IS_CRAFT_CATEGORY_OTHER='Y'">
			<TR>
				<td class="midcolora">OTHER CRAFTE.</td>
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
	<!-- Coverage at Renewal-->
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