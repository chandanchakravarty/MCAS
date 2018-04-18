<!-- ==================================================================================================
File Name			:	UWRulesInputMsg.xsl
Purpose				:	To display Messages for Private Passenger Automobiles (PPA)
Name				:	Pravesh K Chadnel
Date				:	15 Jan 2009
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
					<xsl:when test="INPUTXML/VEHICLES/ERRORS/ERROR/@ERRFOUND='T'">
						<!-- call a template shows all the messages  -->
						<xsl:if test="user:AssignUnderwritter(0) = 0"></xsl:if>
						<xsl:call-template name="AVIATIONDETAIL" />
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
			<xsl:when test="VEHICLE_YEAR='' or MAKE=''or MODEL=''">
				<tr>
					<td class="pageheader">For Vehicle Information: </td>
					<tr>
						<td>
							<table width="60%">
								<tr>
									<xsl:if test="VIN != '' and VIN !='N'">
										<td class="pageheader" width="18%">VIN:</td>
										<td class="midcolora" width="36%">
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
				<xsl:call-template name="VEHICLE_YEAR" />
				<xsl:call-template name="MAKE" />
				<xsl:call-template name="MODEL" />
				<xsl:call-template name="USE_VEHICLE" />
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
	<xsl:template name="RADIUS_OF_USE">
		<xsl:choose>
			<xsl:when test="RADIUS_OF_USE='' or RADIUS_OF_USE=0">
				<tr>
					<td class="midcolora">Please insert Radius of Use.</td>
				</tr>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
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
	<xsl:template name="AVIATIONDETAIL">
		<!-- Application List -->
		<xsl:call-template name="APPLICATION_LIST_MESSAGESGES" />
		<!-- Vehicle -->
		<xsl:call-template name="VEHICLEMESSAGE" />
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
	<xsl:template name="VEHICLEMESSAGE">
		<tr>
			<td class="pageheader">You must have at least one active vehicle with following information.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Vehicle Use.</td>
		</tr>
		<tr>
			<td class="midcolora">Please select Vehicle Type.</td>
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
	</xsl:template>
</xsl:stylesheet>