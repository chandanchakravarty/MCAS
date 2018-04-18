<!-- ==================================================================================================
File Name			:	DiscountAndSurcharge_Cycle.xsl
Purpose				:	To display Discount and Surcharge applicable for MotorCycle on application basis
Name				:	Praveen Singh
Date				:	23 jan 2006  
======================================================================================================== -->
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
		
]]></msxsl:script>
	<!-- ============================================================================= -->
	<xsl:variable name="myDoc" select="document('FactorPath')"></xsl:variable>
	
	<xsl:variable name="Flag" select="0"></xsl:variable>
	<xsl:template match="/">
		<html>
			<head>
				<xsl:variable name="myName" select="QUICKQUOTE/CSSNUM/@CSSVALUE"></xsl:variable>
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
	<xsl:template match="QUICKQUOTE">
		<table border="0" align="center" width='90%'>
			<!-- <td class="midcolora"> -->
		 
					<xsl:value-of select="user:SetFlag(1)" /> 
					<xsl:call-template name="Policy_Level"></xsl:call-template>
				 	<xsl:call-template name="FinalDisplay" />  
				 
					<xsl:value-of select="user:SetFlag(1)" /> 
					<xsl:call-template name="Vehicle_Level"></xsl:call-template>
					<xsl:call-template name="FinalDisplay" />
				 
					<xsl:value-of select="user:SetFlag(1)" /> 
					<xsl:call-template name="Diver_Level"></xsl:call-template>
					<xsl:call-template name="FinalDisplay" /> 
				 
	
			 
		</table>
	</xsl:template>
	<!-- *********************************************************************************************** -->
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Insurance Score  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	<!-- *********************************************************************************************** -->
	<xsl:template name="Policy_Level">
		
		<table class="tableeffectTopHeader" cellSpacing="0" cellPadding="0" width="90%" align="center">			
			<tr>
     			<td width="70%" class="midcolora"><b><u>Policy Level</u></b></td>
				<td width="30%" class="midcolora"><b>Discount / Surcharge</b></td>
			</tr>
		
		 	<xsl:call-template name="INSURANCESCORE"></xsl:call-template>
			<xsl:call-template name="MULTIPOLICY"></xsl:call-template>
				<xsl:call-template name="TRANSFER_RENEWAL"></xsl:call-template>
			
		 	<tr><td height="5%"><p></p><br></br></td></tr>
		</table>
		
	</xsl:template>
	
	<!-- *********************************************************************************************** -->
	
	<xsl:template name="Vehicle_Level">
		 
	 <table class="tableeffectTopHeader" cellSpacing="0" cellPadding="0" width="90%" align="center">	
		<tr class="midcolora">
			<td colspan="2"><b><u> Vehicle Level </u></b></td>
		</tr>
		<xsl:for-each select="VEHICLES/VEHICLE">
		 <tr><td><br></br></td></tr>
			<tr>				
				<td width="70%" class="midcolora">
					<b> <xsl:value-of select="@ID" />.  <xsl:call-template name="VEHICLE_DESC"/>   </b>
				</td>
				<td width="30%" class="midcolora">
					<b> Discount / Surcharge </b>
				</td>
			</tr>		
			
			<xsl:call-template name="MULTIVEHICLE"></xsl:call-template>
			<!--<xsl:call-template name="TRANSFER_RENEWAL"></xsl:call-template>-->
			<xsl:call-template name="TOUR_BIKE_DISPLAY"></xsl:call-template>
			<!--<xsl:call-template name="NO_CYCLE_ENDORSEMENT"></xsl:call-template>-->
			<xsl:call-template name="VEHCLESURCHARGE"></xsl:call-template> 
							
		</xsl:for-each>
		
		<tr><td height="5%"><br></br></td></tr>
	 </table>
	</xsl:template>
	
<!-- *********************************************************************************************** -->
	
	<xsl:template name="Diver_Level">
		<table class="tableeffectTopHeader" cellSpacing="1" cellPadding="1" width="90%" align="center">
		
		<tr class="midcolora">
			<td colspan="2">
				<b>
					<U> Driver Level </U>
				</b>
			</td>
		</tr>
		
		<xsl:for-each select="DRIVERS/DRIVER">
		  <tr><td height="5%"><br></br></td></tr>
					 <tr>
				<td width="70%" class="midcolora">
					<b><xsl:call-template name="DRIVER_DESC"/>  </b>
				</td>
					<td width="30%" class="midcolora">
					<b> Discount / Surcharge </b>
				</td>
			</tr>
				 
			<xsl:call-template name="MATURE_CREDIT"></xsl:call-template>
			<xsl:call-template name="NO_CYCLE_ENDORSEMENT"></xsl:call-template>
			
			<xsl:choose>
			<xsl:when test="LICUNDER3YRS =  'N' and SUMOFACCIDENTPOINTS  &lt;  3 and SUMOFVIOLATIONPOINTS &lt; 3 and AGEOFDRIVER &gt; 18 ">
	          <xsl:call-template name="PREFERRED_RISK"></xsl:call-template>
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
			
			
									 
			
			
		<!-- <xsl:choose><xsl:when test="LICUNDER3YRS &gt;  3 and SUMOFACCIDENTPOINTS  &lt;  2 and SUMOFVIOLATIONPOINTS &lt; 2"> -->
					 
		</xsl:for-each>
	  </table>	
	</xsl:template>
	
	
	 
	<!-- ^^^^^^^^^^^^^^^^^^  Start of Insurance Score  ^^^^^^^^^^^^^^^^^^^^^^^^ -->
	
	
	<xsl:template name="INSURANCESCORE">
	 <xsl:variable name="VAR_INSURANCESCORE">
		<xsl:variable name="INS" select="POLICY/INSURANCESCOREDIS"></xsl:variable>
		<xsl:choose>
		<xsl:when test="$INS &gt;= 0 and $INS &lt;=599">
		    0
		</xsl:when>
		<xsl:when test="$INS = ''">
			0
		</xsl:when>
		<xsl:when test ="$INS = 'N'">
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = 'N']/@CREDIT"/>
		</xsl:when>
		<xsl:when test="$INS &gt;= 751">
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = 751]/@CREDIT" />
		</xsl:when>
		<xsl:otherwise>
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@CREDIT" />
		</xsl:otherwise>
		</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_INSURANCESCORE &gt; 0">
			<tr class="midcolora">
				<td><li> Insurance Score Discount </li></td>
				<td>-<xsl:value-of select="$VAR_INSURANCESCORE" />%</td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		 </xsl:if>
		 </xsl:template>
	
	
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Mature Credit  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	 
	
	<xsl:template name="MATURE_CREDIT">
	
	<xsl:variable name="VAR_MATURE_CREDIT"> 
		<xsl:choose>
			<xsl:when test="AGEOFDRIVER &gt; 45">
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MATUREOPERATOR']/NODE[@ID='MATUREOPERATOR_CREDIT']/ATTRIBUTES/@CREDIT" />
			</xsl:when>
		</xsl:choose>
	</xsl:variable> 
	
	
		<xsl:if test="$VAR_MATURE_CREDIT &gt; 0">
			<tr class="midcolora">
				<td><li>Discount for Matured </li></td> 
				<td>-<xsl:value-of select="$VAR_MATURE_CREDIT" />%</td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		 </xsl:if>
		 
		 
	</xsl:template>
	
	
	
 
	<!-- ^^^^^^^^^^^^^^^ Start of Multi Vehicle  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
 
	 
	
	<xsl:template name="MULTIVEHICLE">
		<xsl:variable name="VAR_MULTIVEHICLE"> 
			<xsl:choose>
				<xsl:when test="MULTICYCLE ='Y'">    
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTICYCLE']/NODE[@ID='MULTICYCLEDISCOUNT']/ATTRIBUTES/@CREDIT" />
				</xsl:when>
			 <xsl:otherwise></xsl:otherwise>
			</xsl:choose>  
		</xsl:variable>
		
		<xsl:if test="$VAR_MULTIVEHICLE  &gt; 0">
			<tr class="midcolora">
				<td><li>Discount For Multi Cycle </li></td>
				<td>-<xsl:value-of select="$VAR_MULTIVEHICLE" />%</td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		 </xsl:if>
		  
	</xsl:template>
	
	
	
 
	<!-- ^^^^^^^^^^^^^^^ Start of Multi-policy  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	 
	 
	<xsl:template name="MULTIPOLICY">
	
	<xsl:variable name="VAR_MULTIPOLICY"> 
		<xsl:choose>
			<xsl:when test="POLICY/MULTIPOLICYAUTOHOMEDISCOUNT = 'Y'">
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIPOLICY']/NODE[@ID ='MULTIPOLICYDISCOUNT']/ATTRIBUTES/@CREDIT" />
			</xsl:when>
		</xsl:choose>
	</xsl:variable>
   
	<xsl:if test="$VAR_MULTIPOLICY &gt; 0">
				<tr class="midcolora">
					<td ><li>Discount for Multi Policy</li></td>
					<td width="212">-<xsl:value-of select="$VAR_MULTIPOLICY" />%</td>
				</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		 </xsl:if>
	</xsl:template>
	
		
	
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^ Start of Preferred Risk  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	
	
	
	<xsl:template name="PREFERRED_RISK">
		<!-- A preferred risk credit will be given when
		 1. the operator has been licensed for three yr to drive an auto or motorcycle
		 2. had no surchargeeable accidents or violations exceeding 2 points.-->
		 <!--test case
		 <xsl:variable name="l" select  = "SUMOFACCIDENTPOINTS"></xsl:variable>
		 <xsl:value-of select="$l" />-->
		 			 
		 <!--test case end
		 
		<xsl:variable name="PLICUNDER3YRS">-->
		
			<!--<xsl:value-of select="LICUNDER3YRS" />
			<xsl:choose>
				<xsl:when test ="LICUNDER3YRS ='Y'">
					1
				</xsl:when>
				<xsl:otherwise> 
					0
				</xsl:otherwise>
			</xsl:choose>
		
		</xsl:variable>
		
		<xsl:variable name="PSUMOFVIOLATIONPOINTS">
			<xsl:value-of select="SUMOFVIOLATIONPOINTS" />
		</xsl:variable>
		
		<xsl:variable name="PSUMOFACCIDENTPOINTS">
			<xsl:value-of select="SUMOFACCIDENTPOINTS" />
		</xsl:variable>-->
		
		
		<!-- 
		
			<xsl:when test="($PSUMOFVIOLATIONPOINTS !='' and $PSUMOFVIOLATIONPOINTS &lt; 12) 
			 	and
			    ($PSUMOFACCIDENTPOINTS   !='' and  $PSUMOFACCIDENTPOINTS  &lt; 12)
			    and 
				($PSUMOFVIOLATIONPOINTS    !='' and  $PSUMOFACCIDENTPOINTS !='' and (($PSUMOFVIOLATIONPOINTS    +  $PSUMOFACCIDENTPOINTS)  &lt; 12))
				 and
				($PLICUNDER3YRS !='' and  $PLICUNDER3YRS &gt; 3))">
	    
		     
		<xsl:variable name="var_Prefferd_Disc"> 
			<xsl:choose> 
			
			<xsl:when test="($PSUMOFVIOLATIONPOINTS !='' and $PSUMOFVIOLATIONPOINTS &lt; 12)
			 	 			and
							($PSUMOFACCIDENTPOINTS   !='' and  $PSUMOFACCIDENTPOINTS  &lt; 12)
							and 
							($PSUMOFVIOLATIONPOINTS    !='' and  $PSUMOFACCIDENTPOINTS !='' and (($PSUMOFVIOLATIONPOINTS    +  $PSUMOFACCIDENTPOINTS)  &lt; 12))
							and $PLICUNDER3YRS =1">  -->
							<!--and
							($PLICUNDER3YRS !='' and  $PLICUNDER3YRS &gt; 3) ">
					1
							
			</xsl:when>
			
			<xsl:otherwise> 
					0
			</xsl:otherwise>
		
		</xsl:choose>
	 </xsl:variable>
	 
	
	
		<xsl:choose>
			<xsl:when test="$var_Prefferd_Disc =1" >-->
				  <xsl:call-template name="PREFERRED_RISK_CREDIT"></xsl:call-template>  
		<!--	</xsl:when>
			<xsl:otherwise>
			</xsl:otherwise>
		</xsl:choose>-->
	</xsl:template>
	
	
	<xsl:template name="PREFERRED_RISK_CREDIT">
		<xsl:variable name="VAR_PREFERRED_RISK"> 
		
	 		<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PREFERREDRISK']/NODE[@ID ='PREFERREDRISKDISCOUNT']/ATTRIBUTES/@CREDIT" />
		</xsl:variable>
		
		<xsl:if test="$VAR_PREFERRED_RISK &gt; 0">
			<tr class="midcolora">
				<td><li>Discount for Preferred Risk  </li></td>
				<td>-<xsl:value-of select="$VAR_PREFERRED_RISK" />%</td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		 </xsl:if>
	</xsl:template>
	 
	<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Preferred Risk  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	 
	 
	 
	 
	<!-- ^^^^^^^^^^^^^^^^ Start of Tranfer/Renewal ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	 
	<xsl:template name="TRANSFER_RENEWAL">
	
	 <xsl:variable name="VAR_TRANSFER_RENEWAL"> 
	 <xsl:choose>
	 <xsl:when test="POLICY/YEARSCONTINSURED != '' and POLICY/YEARSCONTINSURED &gt; 0">
	 <!--   <xsl:when test="(POLICY/YEARSCONTINSURED != '' and POLICY/YEARSCONTINSURED &gt; 0) or (POLICY/YEARSCONTINSUREDWITHWOLVERINE != '' and POLICY/YEARSCONTINSUREDWITHWOLVERINE &gt;=1)">-->
			 <xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TRANSFER_RENEWAL']/NODE[@ID='TRANSFER_RENEWAL_DISCOUNT']/ATTRIBUTES/@CREDIT" />
	    </xsl:when>
	    <xsl:otherwise></xsl:otherwise>
	 </xsl:choose>
	</xsl:variable>
	
	<!--<xsl:variable name="VAR_TRANSFER_RENEWAL"> 
		<xsl:choose>
			<xsl:when test="(POLICY/YEARSCONTINSURED != '' and POLICY/YEARSCONTINSURED &gt; 0) or (POLICY/YEARSCONTINSUREDWITHWOLVERINE != '' and POLICY/YEARSCONTINSUREDWITHWOLVERINE &gt;=1) ">
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TRANSFER_RENEWAL']/NODE[@ID='TRANSFER_RENEWAL_DISCOUNT']/ATTRIBUTES/@CREDIT" />%
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
		
	</xsl:variable>-->
		<xsl:if test="$VAR_TRANSFER_RENEWAL &gt; 0">
			<tr class="midcolora">
				<td><li> Discount For Transfer Experience / Renewal Credit </li></td>
				<td width="212">-<xsl:value-of select="$VAR_TRANSFER_RENEWAL" />%</td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		 </xsl:if>
		 
	</xsl:template>
	
	
	
	
	
	
	 
	 
	 
	<!-- ^^^^^^^^^^^^^^^^ Start of Tour Bike ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	 
	<xsl:template name="TOUR_BIKE_DISPLAY">
		<xsl:choose>
			<xsl:when test="VEHICLETYPE != ''">
				<xsl:call-template name="TOUR_BIKE"></xsl:call-template> 
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	
	
	<xsl:template name="TOUR_BIKE">
		<xsl:variable name="BikeType">
			<xsl:value-of select="VEHICLETYPE" />
		</xsl:variable>
		<xsl:variable name="VEHICLECLASS">
			<xsl:value-of select="CLASS" />
		</xsl:variable>
		
		<xsl:variable name="VAR1">
					<xsl:choose>
						<xsl:when test="$BikeType = 'T'"> 
			    				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPE_OF_BIKE']/NODE[@ID='BIKETYPE']/ATTRIBUTES[@TYPE=$BikeType]/@CLASSA" />			
						</xsl:when>
						<xsl:otherwise>
							0
						</xsl:otherwise>
	        			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR1 &gt; 0">
					<tr>
						<td class="midcolora"><li>Discount for Tour Bike Credit </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR1 * 100)"/>% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:if>
		<xsl:variable name="VAR2">
					<xsl:choose>
						<xsl:when test="$BikeType = 'G'"> 
			    				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPE_OF_BIKE']/NODE[@ID='BIKETYPE']/ATTRIBUTES[@TYPE=$BikeType]/@CLASSA" />			
						</xsl:when>
						<xsl:otherwise>
							0
						</xsl:otherwise>
	        			</xsl:choose>
		</xsl:variable>
				<xsl:if test="$VAR2 &gt; 0">
					<tr>
						<td class="midcolora"><li>Discount for Gold Wing All Models </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR2  * 100)"/>% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:if>
		
		<xsl:variable name="VAR3">
					<xsl:choose>
						<xsl:when test="$BikeType = 'E'"> 
			    				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPE_OF_BIKE']/NODE[@ID='BIKETYPE']/ATTRIBUTES[@TYPE=$BikeType]/@CLASSA" />			
						</xsl:when>
						<xsl:otherwise>
							0
						</xsl:otherwise>
	        			</xsl:choose>
		</xsl:variable>
		
				<xsl:if test="$VAR3 &gt; 0">
					<tr>
						<td class="midcolora"><li>Surcharge Extra Hazard(Scooter and Dual Purpose) </li></td>
						<td class="midcolora">+<xsl:value-of select="(100-round(($VAR3 * 100) * (1))) * -1 "/>% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:if>
		
		<xsl:variable name="VAR4">
					<xsl:choose>
						<xsl:when test="$BikeType = 'H'  and $VEHICLECLASS = 'B'"> 
			    				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPE_OF_BIKE']/NODE[@ID='BIKETYPE']/ATTRIBUTES[@TYPE=$BikeType]/@CLASSB" />			
						</xsl:when>
						<xsl:when test="$BikeType = 'H'  and ($VEHICLECLASS = 'A' or $VEHICLECLASS = 'C')"> 
			    			 <xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPE_OF_BIKE']/NODE[@ID='BIKETYPE']/ATTRIBUTES[@TYPE=$BikeType]/@CLASSC" />			
						</xsl:when>
						
						<xsl:otherwise>
							0
						</xsl:otherwise>
	        			</xsl:choose>
	        			 
		</xsl:variable>
		
				<xsl:if test="$VAR4 &gt; 0">
					<tr>
						<td class="midcolora"><li>Surcharge For Sport Bikes (Racing Design) or High-Performance (Turbo)  </li></td>
						<td class="midcolora">+<xsl:value-of select="round(($VAR4 * 100) * (1)) "/>% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:if>
		
		
		
		
		
	
	<!-- test -->
		
	
	
	
	
	<!-- test -->
	
	
	
	  
	
	</xsl:template>
	
	<!-- ^^^^^^^^^^^^^^^ End of Tour Bike ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	
	
	<!--  *************************************************************  -->
	
	
	<!-- ^^^^^^^^^^^^ Start of No Cycle Endorsement on License ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	
	
	<xsl:template name="NO_CYCLE_ENDORSEMENT">
	
	 <xsl:variable name="VAR1"> 
		<xsl:choose>
			<xsl:when test="NOCYCLENDMT = 'Y'">
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='NO_CYCLE_ENDORSEMENT']/NODE[@ID ='NO_CYCLE_ENDORSEMENT_SURCHARGE']/ATTRIBUTES/@SURCHARGE" />	
			</xsl:when>
		</xsl:choose>
		
		</xsl:variable>
		
		
		<xsl:if test="$VAR1 &gt; 0">
			<tr class="midcolora">
				<td><li>Surcharge for No Cycle Endorsement </li></td>  
				 <td>+<xsl:value-of select="$VAR1" />%</td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
			
		 </xsl:if>
	</xsl:template>
		
	
	
	<!-- - - - - - - - - - - -VEHICLE DESCRIPTION - - - - - - - - - - - - - - - - - - -  -->
	
<xsl:template name="VEHICLE_DESC">
 
		<xsl:value-of select="MAKE" /> -
		<xsl:value-of select="MODEL" /> -
		<xsl:value-of select="YEAR" />
		
 </xsl:template>
<!-- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  -->
<xsl:template name="DRIVER_DESC">

		<xsl:value-of select="DRIVERFNAME" /> 
		<xsl:value-of select="' '" /> 
		 <xsl:value-of select="DRIVERMNAME" />   
		<xsl:value-of select="' '" /> 
		 <xsl:value-of select="DRIVERLNAME" />
		 <xsl:value-of select="' '" /> 
		 <xsl:value-of select="DRIVERCODE" />
		
		
	
</xsl:template>
    
    <!-- - - - - - - - - - - -Extra Hazard Surcharge - - - - - - - - - - - - - - - - - - -  -->
    
    <!-- Directly mapped with Extra Hazard option of Motorcycle Type-->
    
    
    
    <!-- End Of The Process -->

    

	<!-- - - - - - - - - - - -Sum Of Accident and Violation Points - - - - - - - - - - - - - - - - - - -  -->

<xsl:template name ="VEHCLESURCHARGE">
	 
	<!-- 1. Get the violation and accident points.
		2. Against each violation/accident point, fetch the surcharge  (After 13 points, calculate the surcharge using "For each additional" value
		3. Add the surcharges
		4. Divide the sum by 100 and add 1
		5.Accident Points= Sum of all Accidents points in previous 3 yrs  
		6.Violation Points=Sum of all violations points in previous 2 yrs-->
		
	
	
	<!--    Accident Surcharge     -->
	<xsl:variable name="ACCIDENTPOINTS"><xsl:value-of select="SUMOFACCIDENTPOINTS"/></xsl:variable>
	
	
	<xsl:variable name="ACCIDENTSURCHARGE">
	
	<xsl:choose>
		<xsl:when test="$ACCIDENTPOINTS !='' and $ACCIDENTPOINTS !='0' and $ACCIDENTPOINTS &gt; 0">
			<xsl:choose>
				<xsl:when test="$ACCIDENTPOINTS &gt; 13" >
								<xsl:variable name="VAR1"><xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=13]/@SURCHARGEPERCENT"/></xsl:variable>
								<xsl:variable name="VAR2"><xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/@FOR_EACH_ADDITIONAL"/></xsl:variable>
								<xsl:value-of select="$VAR1 + round(($ACCIDENTPOINTS - 13) * $VAR2)"/> 
						</xsl:when>			
				<xsl:otherwise>
						<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=$ACCIDENTPOINTS]/@SURCHARGEPERCENT"/> 		
				</xsl:otherwise>			
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
	</xsl:variable> 
	
	<xsl:if test="$ACCIDENTPOINTS &gt; 0">
		<tr  class="midcolora">
			<td><li> Surcharge For Accident  </li></td>
			 <td>+<xsl:value-of select="$ACCIDENTSURCHARGE" />% </td>
		</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
	</xsl:if>
	
	<!-- Violation Surcharge -->	
	<xsl:variable name="VIOLATIONPOINTS"><xsl:value-of select="SUMOFVIOLATIONPOINTS"/></xsl:variable>
	
	<xsl:variable name="VIOLATIONSURCHARGE">
		<xsl:choose>
			<xsl:when test="$VIOLATIONPOINTS !='' and $VIOLATIONPOINTS !='0' and $VIOLATIONPOINTS &gt; 0">
				<xsl:choose>
					<xsl:when test="$VIOLATIONPOINTS &gt; 13" > 
									
									<xsl:variable name="VAR1"><xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEVIOLATIONPOINTS']/ATTRIBUTES[@POINTS=13]/@SURCHARGE"/></xsl:variable>
									<xsl:variable name="VAR2"><xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEVIOLATIONPOINTS']/@FOR_EACH_ADDITIONAL"/></xsl:variable>
									<xsl:value-of select="$VAR1 + round(($VIOLATIONPOINTS - 13) * $VAR2)"/> 		
					</xsl:when>
					<xsl:otherwise>
							<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEVIOLATIONPOINTS']/ATTRIBUTES[@POINTS=$VIOLATIONPOINTS]/@SURCHARGE"/>
					</xsl:otherwise>			
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
	</xsl:variable>
	
	<xsl:if test="$VIOLATIONPOINTS &gt; 0">
		<tr  class="midcolora">
			<td><li> Surcharge For Violation</li></td>
			 <td>+<xsl:value-of select="$VIOLATIONSURCHARGE" />% </td>
		</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
	</xsl:if>
	<!--Modified-->
	<xsl:variable name="VAR_SURCHARGE">
	
		<!--<xsl:value-of select="(($ACCIDENTSURCHARGE + $VIOLATIONSURCHARGE) div 100) + 1"/>-->
	<xsl:value-of select="($ACCIDENTSURCHARGE + $VIOLATIONSURCHARGE)"/>
		
	 
	</xsl:variable>
	<xsl:if test="$VAR_SURCHARGE &gt; 0">
		<tr  class="midcolora">
			<td><li> Total Surcharge For Accident and Violation</li></td>
			 <td>+<xsl:value-of select="$VAR_SURCHARGE" />% </td>
		</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
	</xsl:if>
	<!---->
	
	<!--(
		(
			(<xsl:value-of select="$ACCIDENTSURCHARGE"/>)
			+
			(<xsl:value-of select="$VIOLATIONSURCHARGE"/>)
		)
		DIV 100.00
	)
	+ 1.00-->

 
</xsl:template>






	
	<!-- ^^^^^^^^^^^^^^^^^^^^^FINAL DISPLAY  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
	

<xsl:template name="FinalDisplay">
 <table class="tableeffectTopHeader" cellSpacing="0" cellPadding="0" width="90%" align="center">
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
 </table>
	</xsl:template>
	
	
</xsl:stylesheet>
