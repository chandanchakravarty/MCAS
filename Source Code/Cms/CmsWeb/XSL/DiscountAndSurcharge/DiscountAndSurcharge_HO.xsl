<!-- ============================================================================================ -->
<!-- File Name		:	discountAndSurcharges_HO.xsl																  -->
<!-- Description	:	This xsl file is for generating 
						the Final Primium (For all Homeowner Products)INDIANA and MICHIGAN		  -->
<!-- Developed By	:	Praveen Singh 															  -->
<!-- ============================================================================================ -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:user="urn:user-namespace-here">
	<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml" />
	<!-- ============================================================================= -->
	<msxsl:script language="c#" implements-prefix="user">
<![CDATA[ 
		
		int intCSS=1;
		public int ApplyColor(int colorvalue)
		{
			intCSS=colorvalue;			
			return intCSS;
		}
		
		
		int statusFlag=1;
		public void SetFlag(int flag)
		{
			statusFlag=flag;
		
		}
		
		public int GetFlag()
		{
			return statusFlag;
		}
		
		
		int statusFor=1;
		public void SetForLoop(int flag)
		{
			statusFor=flag;
		
		}
		
		public int GetForLoop()
		{
			return statusFor;
		}
		
		
		
		int PDFlag=1;
		public void SetFlag_protectiveDevice(int flag)
		{
			PDFlag=flag;
		
		}
		
		public int GetFlag_protectiveDevice()
		{
			return PDFlag;
		}
		
			
]]></msxsl:script>
	<!-- ============================================================================= -->
	<xsl:variable name="myDoc" select="document('FactorPath')"></xsl:variable>
	<xsl:template match="/">
		<html>
			<head>
				<xsl:variable name="myName" select="QUICKQUOTE/DWELLINGDETAILS/CSSNUM/@CSSVALUE"></xsl:variable>
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
			<body class="midcolora">
				<br></br>
				<xsl:apply-templates select="QUICKQUOTE" />
			</body>
		</html>
	</xsl:template>
	<!-- ==============================Functions Defination ====================================== -->
	<!-- ============================================================================================ -->
	<!--								DEWLLINGDETAILS Template(START)								  -->
	<!-- ============================================================================================ -->
	<xsl:template match="QUICKQUOTE">
		<xsl:value-of select="user:SetFlag(1)" />
		<table border="0" align="center" width='90%'>
			<xsl:value-of select="user:SetFlag(1)" />
			<xsl:call-template name="POLICY_LEVEL"></xsl:call-template>
			<!--	 <xsl:call-template name="FinalDisplay" /> -->
			<xsl:value-of select="user:SetFlag(1)" />
			<xsl:call-template name="DWELLING_LEVEL"></xsl:call-template>
				 <xsl:call-template name="FinalDisplay" /> 
			<xsl:value-of select="user:SetFlag(1)" />
		</table>
	</xsl:template>
	<!-- ********************calling template level wise ********************  -->
	<xsl:template name="POLICY_LEVEL">
		<table class="tableeffectTopHeader" cellSpacing="0" cellPadding="0" width="90%" align="center">
			<tr>
				<td width="70%" class="midcolora">
					<b>
						<u>Policy Level</u>
					</b>
				</td>
				<td width="30%" class="midcolora">
					<b>Discount / Surcharge</b>
				</td>
			</tr>
			<xsl:value-of select="user:SetForLoop(0)" />
			<xsl:variable name="VARSTATEMENT">
			<xsl:for-each select="DWELLINGDETAILS">
				<xsl:variable name="VARFOR" select="user:GetForLoop()"></xsl:variable>
				<xsl:choose>
					<xsl:when test="$VARFOR ='0'">
						<xsl:value-of select="STATENAME"></xsl:value-of>
						<xsl:value-of select="user:SetForLoop(1)" />
					</xsl:when>
				</xsl:choose>
			</xsl:for-each>
		  </xsl:variable>
		     <xsl:choose>
		         <xsl:when test="$VARSTATEMENT = 'INDIANA'">
		         <xsl:call-template name="SMOKER_DISPLAY" />
		     </xsl:when>	
		     </xsl:choose>	
			<xsl:call-template name="INSURANCESCORE_SCORE_CREDIT" />
			<xsl:call-template name="EXPERIENCE_CREDIT_DISPLAY"></xsl:call-template>
			<xsl:call-template name="MULTIPOLICY_DISPLAY"></xsl:call-template>
			
			<xsl:call-template name="VALUEDCUSTOMER"></xsl:call-template>
			<xsl:call-template name="SEASONALPRIMARY"></xsl:call-template>
			<xsl:call-template name="SEASONALSECONDARY"></xsl:call-template>
			<xsl:call-template name="PRIOR_LOSS_DISPLAY"></xsl:call-template>
			
		    
		  
		  <xsl:choose>
		  <xsl:when test="$VARSTATEMENT = 'MICHIGAN'">
			<xsl:call-template name="BREEDDOG" />
		</xsl:when>	
		  </xsl:choose>	
			<!--<xsl:call-template name="hhh"/>-->
			<tr>
				<td height="5%">
					<p></p>
					<br></br>
				</td>
			</tr>
		</table>
	</xsl:template>
	
	<!-- ********************DWELLING LEVEL template level wise ********************  -->
	<xsl:template name="DWELLING_LEVEL">
		<table class="tableeffectTopHeader" cellSpacing="1" cellPadding="1" width="90%" align="center">
			<tr class="midcolora">
				<td colspan="2">
					<b>
						<U> Dwelling Level </U>
					</b>
				</td>
			</tr>
			<xsl:for-each select="DWELLINGDETAILS">
			<xsl:value-of select="user:SetFlag(1)" />
				<tr>
					<td>
						<br></br>
					</td>
				</tr>
				<tr>
					<td width="70%" class="midcolora">
						<b><xsl:call-template name="DWELLING_DESC" />&#xa0; <xsl:value-of select="@ID" />   </b>
					</td>
					<td width="30%" class="midcolora">
						<b> Discount / Surcharge </b>
					</td>
				</tr>
				<xsl:choose>
					<xsl:when test="ISACTIVE = 'Y'">
						<xsl:choose>
							<xsl:when test="CONSTRUCTIONCREDIT != 'Y'">
								<xsl:call-template name="AGEOFHOME_DISPLAY"></xsl:call-template>
							</xsl:when> 
						</xsl:choose>
						<xsl:call-template name="DWELLING_UNDER_CONSTRUCTION_DISPLAY"></xsl:call-template>
						<xsl:call-template name="WSTOVE_DISPLAY"></xsl:call-template>
						<xsl:choose>
							<xsl:when test="CONSTRUCTIONCREDIT != 'Y'">
								<xsl:call-template name="PROTECTIVEDEVICE_DISPLAY"></xsl:call-template>
							</xsl:when> 
						</xsl:choose>
						<xsl:call-template name="REDUCTIONCOVERAGEC"></xsl:call-template>
					</xsl:when>
					<xsl:otherwise>
				  			<xsl:call-template name="FinalDisplay" /> 
					</xsl:otherwise>
					</xsl:choose>
				<!-- <xsl:call-template name ="MAX_DISCOUNT_APPLIED_DISPLAY"/>-->
				<!-- <xsl:value-of select="user:SetFlag(1)" />  -->
				<tr>
					<td height="5%">
						<br></br>
					</td>
				</tr>
				
			</xsl:for-each>
		</table>
	</xsl:template>
	<xsl:template name="REDUCTIONCOVERAGEC">

		<xsl:variable name="VAR_DWELLING">
			<xsl:choose>
				<xsl:when test="REDUCTION_IN_COVERAGE_C = 'TRUE'">
					2
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_DWELLING &gt; 0">
			<tr class="midcolora">
				<td>
					<li> Reduction in Limit  Coverage C </li>
				</td>
				<td>Included</td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" />
		</xsl:if>
	</xsl:template>
	
	<!-- ********************End of template level wise ********************  -->
	<!-- ********************Dewelling Description ************************* -->
	<xsl:template name="DWELLING_DESC">
		<xsl:value-of select="@ADDRESS"></xsl:value-of>
	</xsl:template>
	<!-- ********************End Dewelling Description ************************* -->
	<!-- ============================================================================================ -->
	<!--						 Credit - Seasonal / Secondary, Wolverine Insures Primary    		  -->
	<!--	If same customer has any Home policy with wolverine with one primary location			  -->
	<!-- ============================================================================================ -->
	
	<xsl:template name="SEASONALPRIMARY">
		<xsl:value-of select="user:SetForLoop(0)" />
		<xsl:variable name="VARWOLINSURESPRIMARY">
			<xsl:for-each select="DWELLINGDETAILS">
				<xsl:variable name="VARFOR" select="user:GetForLoop()"></xsl:variable>
				<xsl:choose>
					<xsl:when test="$VARFOR ='0'">
						<xsl:value-of select="WOLVERINEINSURESPRIMARY"></xsl:value-of>
						<xsl:value-of select="user:SetForLoop(1)" />
					</xsl:when>
				</xsl:choose>
			</xsl:for-each>
		</xsl:variable>
		
		<xsl:variable name="VAR_WOLINSURESPRIMARY">
		  <xsl:choose>
			<xsl:when test="$VARWOLINSURESPRIMARY = 'Y'">
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID ='SEASONAL_CREDIT']/ATTRIBUTES[@ID = 'INSURE_PRIMARY']/@CREDIT"/>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
	 	</xsl:choose>
	  </xsl:variable>	   	
	<xsl:if test="$VAR_WOLINSURESPRIMARY &gt; 0">
			<tr class="midcolora">
				<td>
					<li>Discounts for Credit Seasonal/Secondary,Wolverine Insures Primary</li>
				</td>
				<td> $<xsl:value-of select="$VAR_WOLINSURESPRIMARY" /></td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" />
		</xsl:if>
	</xsl:template>		
<!--	 ============================================================================================ -->
	<!--						  Credit - Seasonal / Secondary                             		  -->
	<!--	                       If the location is secondary	                    				  -->
	<!-- ============================================================================================ -->
	
	<xsl:template name="SEASONALSECONDARY">
		<xsl:value-of select="user:SetForLoop(0)" />
		<xsl:variable name="VARSEASONALSECONDARY">
			<xsl:for-each select="DWELLINGDETAILS">
				<xsl:variable name="VARFOR" select="user:GetForLoop()"></xsl:variable>
				<xsl:choose>
					<xsl:when test="$VARFOR ='0'">
						<xsl:value-of select="SEASONALSECONDARY"></xsl:value-of>
						<xsl:value-of select="user:SetForLoop(1)" />
					</xsl:when>
				</xsl:choose>
			</xsl:for-each>
		</xsl:variable>
		<xsl:variable name="VAR_SEASONALSECONDARY">
		  <xsl:choose>
			<xsl:when test="$VARSEASONALSECONDARY = 'Y'">
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALLIABILITYCOVG']/NODE[@ID ='SEASONAL_CREDIT']/ATTRIBUTES[@ID = 'INSURE_NON_PRIMARY']/@CREDIT"/>
			</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	 	</xsl:choose>
	  </xsl:variable>	   	
	<xsl:if test="$VAR_SEASONALSECONDARY &gt; 0">
			<tr class="midcolora">
				<td>
					<li> Discounts for  Credit Seasonal/Secondary</li>
				</td>
				<td> $ <xsl:value-of select="  $VAR_SEASONALSECONDARY" /></td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" />
		</xsl:if>
	</xsl:template>	
	
	
	<!-- ============================================================================================ -->
	<!--						Templates for Insurance Score Credit, Display (START)				  -->
	<!--		    					  FOR MISHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="INSURANCESCORE_SCORE_CREDIT">
		<xsl:value-of select="user:SetForLoop(0)" />
		<xsl:variable name="INS">
			<xsl:for-each select="DWELLINGDETAILS">
				<xsl:variable name="VARFOR" select="user:GetForLoop()"></xsl:variable>
				<xsl:choose>
					<xsl:when test="$VARFOR ='0'">
						<xsl:value-of select="INSURANCESCOREDIS"></xsl:value-of>
						<xsl:value-of select="user:SetForLoop(1)" />
					</xsl:when>
				</xsl:choose>
			</xsl:for-each>
		</xsl:variable>
		
		<xsl:variable name="VAR_INSURANCESCORE">
			<xsl:choose>
				<xsl:when test="$INS = 0">
					<xsl:value-of select="$INS"></xsl:value-of>
				</xsl:when>
				<xsl:when test="$INS = ''">
					0
				</xsl:when>
				<xsl:when test = "$INS = 'N'">
						<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='CREDIT']/ATTRIBUTES[@MINSCORE = 'N']/@CREDIT"/>
				</xsl:when>
				<xsl:when test="$INS &gt;= 751">
						<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='CREDIT']/ATTRIBUTES[@MINSCORE = 751]/@CREDIT" />
				</xsl:when>
				<xsl:otherwise>
						<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='CREDIT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@CREDIT" />
				</xsl:otherwise>
	       </xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_INSURANCESCORE &gt; 0">
			<tr class="midcolora">
				<td>
					<li> Insurance Score Discount </li>
				</td>
				<td>-<xsl:value-of select="$VAR_INSURANCESCORE" />%</td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" />
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--						Templates for Insurance Score Credit, Display (END)					  -->
	<!--		    					  FOR MISHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Under Construction [Factor,Credit,Display]  (START)		  -->
	<!--		    					  FOR MISHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="DWELLING_UNDER_CONSTRUCTION_DISPLAY">
		<xsl:variable name="VAR_DWELLING">
			<xsl:choose>
				<xsl:when test="CONSTRUCTIONCREDIT = 'Y'">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='DWELLINGUNDERCONSTRUCTION']/ATTRIBUTES/@CREDIT" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_DWELLING &gt; 0">
			<tr class="midcolora">
				<td>
					<li> Discount for dwelling under construction  </li>
				</td>
				<td>-<xsl:value-of select="$VAR_DWELLING" />% </td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" />
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Experince Factor [Factor,Credit,Display]  (SATRT)			  -->
	<!--		    					  FOR MISHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!--Experince Credit -->
	<xsl:template name="EXPERIENCE_CREDIT_DISPLAY">
		<xsl:value-of select="user:SetForLoop(0)" />
		<xsl:variable name="VAREXPERIENCE">
			<xsl:for-each select="DWELLINGDETAILS">
				<xsl:variable name="VARFOR" select="user:GetForLoop()"></xsl:variable>
				<xsl:choose>
					<xsl:when test="$VARFOR ='0'">
						<xsl:value-of select="EXPERIENCE"></xsl:value-of>
						<xsl:value-of select="user:SetForLoop(1)" />
					</xsl:when>
				</xsl:choose>
			</xsl:for-each>
		</xsl:variable>
		
		<xsl:variable name="VAR_EXP">
			<xsl:choose>
				<xsl:when test="$VAREXPERIENCE &gt; 35 ">
					<xsl:variable name="EF" select="EXPERIENCE"></xsl:variable>
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MATURITY']/ATTRIBUTES[@MINAGE &lt;= $VAREXPERIENCE and @MAXAGE &gt;= $VAREXPERIENCE]/@CREDIT" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_EXP &gt; 0">
			<tr class="midcolora">
				<td>
					<li> Discount for Experience</li>
				</td>
				<td>-<xsl:value-of select="$VAR_EXP" />% </td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" />
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Age Of Home Factor  [Factor,Credit,Display]  (SATRT)		  -->
	<!--		    					  FOR MISHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!--Age Of Home CREDIT -->
	<xsl:template name="AGEOFHOME_DISPLAY">
		<xsl:variable name="VAR_AGEOFHOME">
			<xsl:choose>
				<xsl:when test="AGEOFHOME &lt;= 20 and STATENAME = 'MICHIGAN'">
					<xsl:choose>
						<xsl:when test="AGEHOMECREDIT = 'Y'">
							<xsl:variable name="AGE_OF_HOME" select="AGEOFHOME" />
							<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/ATTRIBUTES[@AGE = $AGE_OF_HOME]/@CREDIT" />
						</xsl:when>
					</xsl:choose>
				</xsl:when>
				<xsl:when test="AGEOFHOME &lt;= 20 and STATENAME = 'INDIANA'">
					<xsl:choose>
						<xsl:when test="AGEHOMECREDIT = 'Y'">
							<xsl:variable name="AGE_OF_HOME" select="AGEOFHOME" />
							<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/ATTRIBUTES[@AGE = $AGE_OF_HOME]/@CREDIT" />
						</xsl:when>
					</xsl:choose>
				</xsl:when>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_AGEOFHOME &gt; 0">
			<tr class="midcolora">
				<td>
					<li> Discount for Age Of Home </li>
				</td>
				<td>-<xsl:value-of select="$VAR_AGEOFHOME" />% </td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" />
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Age Of Home   [Factor,Credit,Display]  (END)				  -->
	<!--		    					  FOR MISHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Protective Device   [Factor,Credit,Display]  (SATRT)		  -->
	<!--		    					  FOR MISHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="PROTECTIVEDEVICE_DISPLAY">
		<tr class="midcolora">
			<td colspan="2">
				<b>
					<U> Protective device </U>
				</b>
			</td>
		</tr>
		<xsl:value-of select="user:SetFlag_protectiveDevice(1)" />
		<xsl:call-template name="CENTRAL_STATION_BURGULARY_FAS" />
		<xsl:call-template name="DIRECT_TO_FIRE_AND_POLICE" />
		<xsl:call-template name="LOCAL_FIRE_OR_LOCAL_GAS_ALARM" />
		<xsl:call-template name="TWO_MORE_LOCAL_FIRE" />	
		<xsl:call-template name="FinalDisplay_PD" />
	</xsl:template>
	<!-- ****************** START OF PARAMETERS FOR  PROTECTIVE DEVICE  ******************** -->
	<!-- ================ CENTRAL STATION BURGULARY FAS   ======================== -->
	<xsl:template name="CENTRAL_STATION_BURGULARY_FAS">
		<xsl:variable name="VAR_BURGLAR" select="BURGLAR" />
		<xsl:variable name="VAR_CENTRAL_FIRE" select="CENTRAL_FIRE" />
		<xsl:variable name="VARCENT_ST_BURG_FIRE" select="CENT_ST_BURG_FIRE" />
		
		

			<xsl:choose>
				<xsl:when test="$VARCENT_ST_BURG_FIRE = 'Y'">
				    <xsl:variable name="VAR_CENTRAL_STATION_BURGULARY_FAS1">
					 <xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='CSBF']/@CREDIT" />
					</xsl:variable>
					<xsl:if test="$VAR_CENTRAL_STATION_BURGULARY_FAS1 &gt; 0">
						<tr class="midcolora">
							<td>
								<li> Discount for Central station burgulary and fire alarm system </li>
							</td>
							<td>-<xsl:value-of select="round($VAR_CENTRAL_STATION_BURGULARY_FAS1)" />% </td>
						</tr>
						<xsl:value-of select="user:SetFlag(0)" />
						<xsl:value-of select="user:SetFlag_protectiveDevice(0)" />
					</xsl:if>
				</xsl:when>
			</xsl:choose>
			<xsl:choose>
				<xsl:when test="$VAR_BURGLAR = 'Y'">
				 <xsl:variable name="VAR_CENTRAL_STATION_BURGULARY_FAS2">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='CSB']/@CREDIT" />
				 </xsl:variable> 
				 
					<xsl:if test="$VAR_CENTRAL_STATION_BURGULARY_FAS2 &gt; 0">
						<tr class="midcolora">
							<td>
								<li> Discount for Central Stations Burglary </li>
							</td>
							<td>-<xsl:value-of select="round($VAR_CENTRAL_STATION_BURGULARY_FAS2)" />% </td>
						</tr>
						<xsl:value-of select="user:SetFlag(0)" />
						<xsl:value-of select="user:SetFlag_protectiveDevice(0)" />
					</xsl:if>	
				</xsl:when>
			</xsl:choose>	
			<xsl:choose>
				<xsl:when test="$VAR_CENTRAL_FIRE = 'Y'">
				<xsl:variable name="VAR_CENTRAL_STATION_BURGULARY_FAS3">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='CSF']/@CREDIT" />
				</xsl:variable>
				<xsl:if test="$VAR_CENTRAL_STATION_BURGULARY_FAS3 &gt; 0">
						<tr class="midcolora">
							<td>
								<li> Discount for  Central Stations Fire </li>
							</td>
							<td>-<xsl:value-of select="round($VAR_CENTRAL_STATION_BURGULARY_FAS3)" />% </td>
						</tr>
						<xsl:value-of select="user:SetFlag(0)" />
						<xsl:value-of select="user:SetFlag_protectiveDevice(0)" />
					</xsl:if>	
				</xsl:when>
			</xsl:choose>

	</xsl:template>
	<!-- ================DIRECT TO FIRE AND POLICE  ======================== -->
	<xsl:template name="DIRECT_TO_FIRE_AND_POLICE">
		<xsl:variable name="VAR_BURGLER_ALERT_POLICE" select="BURGLER_ALERT_POLICE" />
		<xsl:variable name="VAR_FIRE_ALARM_FIREDEPT" select="FIRE_ALARM_FIREDEPT" />
		<xsl:variable name="VARDIR_FIRE_AND_POLICE" select="DIR_FIRE_AND_POLICE"/>
		<xsl:choose>
				<xsl:when test="$VARDIR_FIRE_AND_POLICE = 'Y'">
				    <xsl:variable name="VAR_CENTRAL_STATION_BURGULARY_FAS1">
					 <xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='DFP']/@CREDIT" />
					</xsl:variable>
					<xsl:if test="$VAR_CENTRAL_STATION_BURGULARY_FAS1 &gt; 0">
						<tr class="midcolora">
							<td>
								<li> Discount for Direct to Fire and Police </li>
							</td>
							<td>-<xsl:value-of select="round($VAR_CENTRAL_STATION_BURGULARY_FAS1)" />% </td>
						</tr>
						<xsl:value-of select="user:SetFlag(0)" />
						<xsl:value-of select="user:SetFlag_protectiveDevice(0)" />
					</xsl:if>
				</xsl:when>
			</xsl:choose>
			<xsl:choose>
				<xsl:when test="$VAR_BURGLER_ALERT_POLICE = 'Y'">
				 <xsl:variable name="VAR_CENTRAL_STATION_BURGULARY_FAS2">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='DP']/@CREDIT" />
				 </xsl:variable> 
				 
					<xsl:if test="$VAR_CENTRAL_STATION_BURGULARY_FAS2 &gt; 0">
						<tr class="midcolora">
							<td>
								<li> Discount for Direct to Police </li>
							</td>
							<td>-<xsl:value-of select="round($VAR_CENTRAL_STATION_BURGULARY_FAS2)" />% </td>
						</tr>
						<xsl:value-of select="user:SetFlag(0)" />
						<xsl:value-of select="user:SetFlag_protectiveDevice(0)" />
					</xsl:if>	
				</xsl:when>
			</xsl:choose>	
			<xsl:choose>
				<xsl:when test="$VAR_FIRE_ALARM_FIREDEPT = 'Y'">
				<xsl:variable name="VAR_CENTRAL_STATION_BURGULARY_FAS3">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='DF']/@CREDIT" />
				</xsl:variable>
				<xsl:if test="$VAR_CENTRAL_STATION_BURGULARY_FAS3 &gt; 0">
						<tr class="midcolora">
							<td>
								<li> Discount for Direct to Fire </li>
							</td>
							<td>-<xsl:value-of select="round($VAR_CENTRAL_STATION_BURGULARY_FAS3)" />% </td>
						</tr>
						<xsl:value-of select="user:SetFlag(0)" />
						<xsl:value-of select="user:SetFlag_protectiveDevice(0)" />
					</xsl:if>	
				</xsl:when>
			</xsl:choose>
	</xsl:template>
	<!-- ================LOCAL FIRE OR LOCAL GAS ALARM  ======================== -->
	<xsl:template name="LOCAL_FIRE_OR_LOCAL_GAS_ALARM">
		<xsl:variable name="VAR_N0_LOCAL_ALARM" select="LOC_FIRE_GAS"/>
		
		<xsl:variable name="VAR_LOCAL_FIRE_OR_LOCAL_GAS_ALARM">
			<xsl:choose>
				<xsl:when test="LOC_FIRE_GAS = 'Y'">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='LFLG']/@CREDIT" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_LOCAL_FIRE_OR_LOCAL_GAS_ALARM &gt; 0">
			<tr class="midcolora">
				<td>
					<li> Discount for local fire or local gas alarm </li>
				</td>
				<td>-<xsl:value-of select="round($VAR_LOCAL_FIRE_OR_LOCAL_GAS_ALARM)" />% </td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" />
			<xsl:value-of select="user:SetFlag_protectiveDevice(0)" />
		</xsl:if>
	</xsl:template>
	<!-- ================Two or more Local Fire Alarms  ======================== -->
	<xsl:template name="TWO_MORE_LOCAL_FIRE">
		
		
		<xsl:variable name="VAR_LOCAL_FIRE_OR_LOCAL_GAS_ALARM">
			<xsl:choose>
				<xsl:when test="TWO_MORE_FIRE = 'Y'">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID='PROTECTIVEDEVICES']/ATTRIBUTES[@ID='LFA']/@CREDIT" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_LOCAL_FIRE_OR_LOCAL_GAS_ALARM &gt; 0">
			<tr class="midcolora">
				<td>
					<li> Discount for two or more local fire alarms </li>
				</td>
				<td>-<xsl:value-of select="round($VAR_LOCAL_FIRE_OR_LOCAL_GAS_ALARM)" />% </td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" />
			<xsl:value-of select="user:SetFlag_protectiveDevice(0)" />
		</xsl:if>
	</xsl:template>
	<!-- ************ END OF PARAMETERS FOR  PROTECTIVE DEVICE  ***********************  -->
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Multi policy Factor [Factor,Credit,Display]  (SATRT)		  -->
	<!--		    					  FOR MISHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!--Multipolicy CREDIT -->
	<xsl:template name="MULTIPOLICY_DISPLAY">
		<xsl:value-of select="user:SetForLoop(0)" />
		<xsl:variable name="VARMULTIPLEPOLICYFACTOR">
			<xsl:for-each select="DWELLINGDETAILS">
				<xsl:variable name="VARFOR" select="user:GetForLoop()"></xsl:variable>
				<xsl:choose>
					<xsl:when test="$VARFOR ='0'">
						<xsl:value-of select="MULTIPLEPOLICYFACTOR"></xsl:value-of>
						<xsl:value-of select="user:SetForLoop(1)" />
					</xsl:when>
				</xsl:choose>
			</xsl:for-each>
		</xsl:variable>
		<xsl:variable name="VAR_MULTIPOLICY">
			<xsl:choose>
				<xsl:when test="$VARMULTIPLEPOLICYFACTOR = 'Y'">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MULTIPOLICY']/ATTRIBUTES/@CREDIT" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_MULTIPOLICY &gt; 0">
			<tr class="midcolora">
				<td>
					<li> Discount for Multipolicy </li>
				</td>
				<td>-<xsl:value-of select="$VAR_MULTIPOLICY" />% </td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" />
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Multipolicy Factor [Factor,Credit,Display]  (END)			  -->
	<!--		    					  FOR MISHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Valued Customer [Factor,Credit,Display]  (SATRT)			  -->
	<!--		    					  FOR MISHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="VALUEDCUSTOMER">
	
	<xsl:value-of select="user:SetForLoop(0)" />
		<xsl:variable name="VARNEWBUSINESSFACTOR">
			<xsl:for-each select="DWELLINGDETAILS">
				<xsl:variable name="VARFOR" select="user:GetForLoop()"></xsl:variable>
				<xsl:choose>
					<xsl:when test="$VARFOR ='0'">
						<xsl:value-of select="NEWBUSINESSFACTOR"></xsl:value-of>
						<xsl:value-of select="user:SetForLoop(1)" />
					</xsl:when>
				</xsl:choose>
			</xsl:for-each>
		</xsl:variable>
	
	<xsl:value-of select="user:SetForLoop(0)" />
		<xsl:variable name="VARVALUESCUSTOMERAPP">
			<xsl:for-each select="DWELLINGDETAILS">
				<xsl:variable name="VARFOR" select="user:GetForLoop()"></xsl:variable>
				<xsl:choose>
					<xsl:when test="$VARFOR ='0'">
						<xsl:value-of select="VALUESCUSTOMERAPP"></xsl:value-of>
						<xsl:value-of select="user:SetForLoop(1)" />
					</xsl:when>
				</xsl:choose>
			</xsl:for-each>
		</xsl:variable>
		
		<xsl:value-of select="user:SetForLoop(0)" />
		<xsl:variable name="VARLOSSFREE">
			<xsl:for-each select="DWELLINGDETAILS">
				<xsl:variable name="VARFOR" select="user:GetForLoop()"></xsl:variable>
				<xsl:choose>
					<xsl:when test="$VARFOR ='0'">
						<xsl:value-of select="LOSSFREE"></xsl:value-of>
						<xsl:value-of select="user:SetForLoop(1)" />
					</xsl:when>
				</xsl:choose>
			</xsl:for-each>
		</xsl:variable>
		<xsl:variable name="VAR_VALUEDCUSTOMER">
			<xsl:choose>
				<xsl:when test="$VARVALUESCUSTOMERAPP ='Y' and $VARLOSSFREE='N' and VARNEWBUSINESSFACTOR='N'">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_WITH_CLAIMS']/ATTRIBUTES[@MINRENEWALDURATION = '3']/@CREDIT" />
				</xsl:when>
				<xsl:when test="$VARVALUESCUSTOMERAPP ='Y' and $VARLOSSFREE='Y' and VARNEWBUSINESSFACTOR='N'">
				    <xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_WITHOUT_CLAIMS']/ATTRIBUTES[@MINRENEWALDURATION = '3']/@CREDIT" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_VALUEDCUSTOMER &gt; 0">
			<tr class="midcolora">
				<td>
					<li> Discount for Valued Customer </li>
				</td>
				<td>-<xsl:value-of select="$VAR_VALUEDCUSTOMER" />% </td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" />
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Discount - Valued Customer [Factor,Credit,Display]  (END)				  -->
	<!--		    					  FOR MISHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--Templates for Charge-Insured to 80% to 99% of Replacement Cost [Factor,Credit,Display](SATRT) -->
	<!--		    					  FOR MISHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- In the case of Premium Quote HO Factor will be 1-->
	<xsl:template name="REPLACEMENT_COST">
		<xsl:choose>
			<xsl:when test="DWELLING_LIMITS = REPLACEMENTCOSTFACTOR">
		1.00
		</xsl:when>
			<xsl:when test="DWELLING_LIMITS &lt; REPLACEMENTCOSTFACTOR">
		1.18
		</xsl:when>
			<xsl:otherwise>
		1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For Display Only -->
	<xsl:template name="REPLACEMENT_COST_DISPLAY">
	<!--Applied-->
	0
</xsl:template>
	<!-- Charge - Prior Loss " -->
	<!-- For Display In The Label -->
	<xsl:template name="PRIOR_LOSS_DISPLAY">
		<xsl:value-of select="user:SetForLoop(0)" />
		<xsl:variable name="VARPRIORLOSSSURCHARGE">
			<xsl:for-each select="DWELLINGDETAILS">
				<xsl:variable name="VARFOR" select="user:GetForLoop()"></xsl:variable>
				<xsl:choose>
					<xsl:when test="$VARFOR ='0'">
						<xsl:value-of select="PRIORLOSSSURCHARGE"></xsl:value-of>
						<xsl:value-of select="user:SetForLoop(1)" />
					</xsl:when>
				</xsl:choose>
			</xsl:for-each>
		</xsl:variable>
		<xsl:value-of select="user:SetForLoop(0)" />
		<xsl:variable name="VARNEWBUSINESSFACTOR">
			<xsl:for-each select="DWELLINGDETAILS">
				<xsl:variable name="VARFOR" select="user:GetForLoop()"></xsl:variable>
				<xsl:choose>
					<xsl:when test="$VARFOR ='0'">
						<xsl:value-of select="NEWBUSINESSFACTOR"></xsl:value-of>
						<xsl:value-of select="user:SetForLoop(1)" />
					</xsl:when>
				</xsl:choose>
			</xsl:for-each>
		</xsl:variable>
		<xsl:variable name="VAR_PRIOR_LOSS">
				<xsl:choose>
					<xsl:when test="$VARPRIORLOSSSURCHARGE = 'Y' and $VARNEWBUSINESSFACTOR= 'Y' "><xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='NEWBUSINESSPRIORLOSS']/ATTRIBUTES/@SURCHARGE"/></xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:variable>
			<xsl:if test="$VAR_PRIOR_LOSS &gt; 0">
				<tr class="midcolora">
					<td>
						<li> Surcharge for new business prior loss </li>
					</td>
					<td>+<xsl:value-of select="$VAR_PRIOR_LOSS" />% </td>
				</tr>
				<xsl:value-of select="user:SetFlag(0)" />
			</xsl:if>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--Templates for Charge-Insured to 80% to 99% of Replacement Cost [Factor,Credit,Display](END)   -->
	<!--		    					  FOR MISHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for Charge - Wood Stove [Factor,Credit,Display](START)			  -->
	<!--		    					  FOR MISHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- Woodstove Surcharge-->
	<xsl:template name="WSTOVE_DISPLAY">
		<xsl:variable name="VAR_WSTOVE">
			<xsl:choose>
				<xsl:when test="WOODSTOVE_SURCHARGE = 'Y'">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='WOODSTOVE']/ATTRIBUTES/@SURCHARGE" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_WSTOVE &gt; 0">
			<tr class="midcolora">
				<td>
					<li> Surcharge for wood stove </li>
				</td>
				<td>+<xsl:value-of select="$VAR_WSTOVE" />% </td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" />
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for Charge - Wood Stove [Factor,Credit,Display](END)  			  -->
	<!--		    					  FOR MISHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--				Templates for Surcharges BREEDDOG [Factor,Display](SATRT)		  			  -->
	<!--		    					  FOR MISHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<xsl:template name="BREEDDOG">
		<xsl:value-of select="user:SetForLoop(0)" />
		<xsl:variable name="VARNOPETS">
			<xsl:for-each select="DWELLINGDETAILS">
				<xsl:variable name="VARFOR" select="user:GetForLoop()"></xsl:variable>
				<xsl:choose>
					<xsl:when test="$VARFOR ='0'">
						<xsl:value-of select="NOPETS"></xsl:value-of>
						<xsl:value-of select="user:SetForLoop(1)" />
					</xsl:when>
				</xsl:choose>
			</xsl:for-each>
		</xsl:variable>
		
		<xsl:variable name='VARPETSUR'>
			<xsl:choose>
				<xsl:when test="$VARNOPETS &gt; 0">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='SPECIFICBREEDOFDOGS']/ATTRIBUTES/@SURCHARGE" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VARPETSUR &gt; 0">
			<tr class="midcolora">
				<td>
					<li> Surcharge for Specific Bread of Dog </li>
				</td>
				<td>+<xsl:value-of select="$VARPETSUR * $VARNOPETS" />%</td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" />
		</xsl:if>
	</xsl:template>
	<!-- No OF Dog Surcharge-->
	<xsl:template name="NO_OF_DOGS">
		<xsl:choose>
			<xsl:when test="DOGSURCHARGE > 0">
			(<xsl:value-of select="DOGSURCHARGE" />)
		</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--				Templates for Surcharges BREEDDOG [Factor,Credit,Display](END)  			  -->
	<!--		    					  FOR MISHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--		Templates for Surcharges PREFERRED_PLUS_DISPLAY[HO-20,HO-21,HO-24,HO-25](START)		  -->
	<!-- This Template has been created because in PREFERRED_PLUS_DISPLAY option (ADDITIONAL COVERAGES)
	 has been displayed as  'Included' 															  -->
	<!--		    					  FOR MISHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- For Display Only -->
	<xsl:template name="PREFERRED_PLUS_DISPLAY">
		<xsl:choose>
			<!-- For Michigan REPAIR COST -->
			<xsl:when test="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<!-- For Michigan REGULAR -->
			<xsl:when test="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''">
			Included
		</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<!-- For Michigan DELUXE  -->
			<xsl:when test="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<!-- For Michigan PREMIER -->
			<xsl:when test="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'">
			Included
		</xsl:when>
			<!-- For INDIANA -->
			<xsl:when test="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' ">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA' ">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-5' and STATENAME ='INDIANA' ">
			Included
		</xsl:when>
			<!-- REPAIR COST-->
			<xsl:when test="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<!-- DELUXE-->
			<xsl:when test="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For Calculation [Including in Additional Coverages] -->
	<xsl:template name="PREFERRED_PLUS_COVERAGE">
		<xsl:choose>
			<!-- For Michigan REPAIR COST -->
			<xsl:when test="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Repair Cost'">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<!-- For Michigan REGULAR -->
			<xsl:when test="PRODUCTNAME = 'HO-2' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''">
			0.00
		</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = ''">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<!-- For Michigan DELUXE  -->
			<xsl:when test="PRODUCTNAME = 'HO-4' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-6' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Deluxe'">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<!-- For Michigan PREMIER -->
			<xsl:when test="PRODUCTNAME = 'HO-3' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-5' and STATENAME ='MICHIGAN' and PRODUCT_PREMIER = 'Premier'">
			0.00
		</xsl:when>
			<!-- For INDIANA -->
			<xsl:when test="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' ">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA' ">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-5' and STATENAME ='INDIANA' ">
			0.00
		</xsl:when>
			<!-- REPAIR COST-->
			<xsl:when test="PRODUCTNAME = 'HO-2' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-3' and STATENAME ='INDIANA' and PRODUCT_PREMIER ='Repair Cost'">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = ''">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<!-- DELUXE-->
			<xsl:when test="PRODUCTNAME = 'HO-4' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:when test="PRODUCTNAME = 'HO-6' and STATENAME ='INDIANA' and PRODUCT_PREMIER = 'Deluxe'">
				<xsl:call-template name="HO_20_PREFERRED_PLUS_OR_HO_21_VIP" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--		Templates for Surcharges PREFERRED_PLUS_DISPLAY[HO-20,HO-21,HO-24,HO-25](END)  		  -->
	<!--		    					  FOR MISHIGAN and INDIANA									  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--									Template Smoker Factor (START)							  -->
	<!--		    							FOR MISHIGAN and INDIANA							  -->
	<!-- ============================================================================================ -->
	<xsl:template name="SMOKER_DISPLAY">
		<xsl:value-of select="user:SetForLoop(0)" />
		<xsl:variable name="VARNONSMOKER">
			<xsl:for-each select="DWELLINGDETAILS">
				<xsl:variable name="VARFOR" select="user:GetForLoop()"></xsl:variable>
				<xsl:choose>
					<xsl:when test="$VARFOR ='0'">
						<xsl:value-of select="NONSMOKER"></xsl:value-of>
						<xsl:value-of select="user:SetForLoop(1)" />
					</xsl:when>
				</xsl:choose>
			</xsl:for-each>
		</xsl:variable>
		<xsl:variable name="VAR_SMOKER">
			<xsl:choose>
				<xsl:when test="$VARNONSMOKER = 'Y'">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='NONSMOKERCREIT']/ATTRIBUTES/@CREDIT" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_SMOKER &gt; 0">
			<tr class="midcolora">
				<td>
					<li> Discount for Nonsmoker </li>
				</td>
				<td>-<xsl:value-of select="$VAR_SMOKER" />%</td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" />
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--									Template Smoker Factor (END)							  -->
	<!--		    							FOR MISHIGAN and INDIANA							  -->
	<!-- ============================================================================================ -->
	<!-- ============================================================================================ -->
	<!--									50% Discount Applied (START)							  -->
	<!--		    							FOR MISHIGAN										  -->
	<!-- ============================================================================================ -->
	<xsl:template name="MAX_DISCOUNT_APPLIED_DISPLAY">
{
	<xsl:variable name="FINALCREDIT_EXPERIENCE">
		(1.00-(<xsl:call-template name="AGEOFHOME_FACTOR" />))
		+	(1.00-(<xsl:call-template name="GET_PROTECTIVE_DEVICE_DISCOUNT" />))
		+	(1.00-(<xsl:call-template name="GET_DUC_DISCOUNT" />))
		+	(1.00-(<xsl:call-template name="GET_EXPERIENCE_DISCOUNT" />))
		+	(1.00-(<xsl:call-template name="GET_MULTIPOLICY_DISCOUNT" />))
	</xsl:variable>
	<xsl:choose>
			<xsl:when test="STATENAME = 'INDIANA'">
			0.00
		</xsl:when>
			<xsl:otherwise>
			IF (<xsl:value-of select="$FINALCREDIT_EXPERIENCE" /> &gt; 0.50) 
			THEN
				1.00
			ELSE
				0.00		
		</xsl:otherwise>
		</xsl:choose>
}
</xsl:template>
	<!-- ============================================================================================ -->
	<!--									50% Discount Applied (END)	    						  -->
	<!--		    							FOR MISHIGAN										  -->
	<!-- ============================================================================================ -->
	<!-- ****************************  For final display  ****************************  -->
	<xsl:template name="FinalDisplay">
		<xsl:variable name="var1">
			<xsl:value-of select="user:GetFlag()" />
		</xsl:variable>
		<xsl:if test="$var1 &gt; 0">
			<tr class="midcolora">
				<td width="260" align="right"> 
					No Discounts or Surcharges are applicable 
				</td>
			</tr>
		</xsl:if>
	</xsl:template>
	<!-- ************* check Protective device available and then display   -->
	<xsl:template name="FinalDisplay_PD">
		<xsl:variable name="var1">
			<xsl:value-of select="user:GetFlag_protectiveDevice()" />
		</xsl:variable>
		<xsl:if test="$var1 &gt; 0">
			<tr class="midcolora">
				<td width="260" align="right"> 
					No protective device are available
				</td>
			</tr>
		</xsl:if>
	</xsl:template>
</xsl:stylesheet>
