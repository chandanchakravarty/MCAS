 

<!-- ==================================================================================================
File Name			:	RentalInputVerification.XSL 
Purpose				:	Rental Input Verification 
Name				:	Praveen Singh
Date				:	17 july 2006  
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
		 
		int intVerifyXML=1;
		public int AllVerified(int status)
		{
			intVerifyXML=intVerifyXML * status;			
			return intVerifyXML;
		}	 
		


			
]]></msxsl:script>

 

	<!-- ============================================================================= -->
		

<xsl:variable name="HOCoveragesDoc" select="document('FactorPath')"/>

<!-- Following boat types present in the database. 
	These are mapped with the values in the table 'mnt_lookup_values' against lookup_id=1201
	These are constant global variables.
				 
			IO	Inboard / Outboard Boat
			I	Inboard
			S	Sailboat
			MJB	Mini Jet Boat
			PIO	Pontoon Inboard / Outboard Boat
			CN	Canoe
			RB	Row Boat		
			W	Waverunner
			PWT	Jetski (w/Lift Bar)
			JS	Jet Ski		
			OWM	Outboard (w/Motor)
			SWOB	Sailboat w/outboard
			PO	Pontoon Outboard
			PDB	Paddleboat
			JDO	Jet Drive Outboards

		Distinct Styles:
			I - Inboard
			IO - Inboard/Outboard
			JS - Jet Ski
			O - Outboard
			S - Sailboat
			T - Trailer

-->
<xsl:variable name="BOATTYPE_CANOE" select="'CN'"/>
<xsl:variable name="BOATTYPE_INBOARD_OUTBOARD" select="'IO'"/>
<xsl:variable name="BOATTYPE_INBOARD" select="'I'"/>
<xsl:variable name="BOATTYPE_JETDRIVE_OUTBOARD" select="'JDO'"/>
<xsl:variable name="BOATTYPE_JETSKI_WITH_LB" select="'PWT'"/>
<xsl:variable name="BOATTYPE_JETSKI" select="'JS'"/>
<xsl:variable name="BOATTYPE_MINI_JET" select="'MJB'"/>
<xsl:variable name="BOATTYPE_OUTBOARD_WITH_MOTOR" select="'OWM'"/>
<xsl:variable name="BOATTYPE_PADDLEBOAT" select="'PDB'"/>
<xsl:variable name="BOATTYPE_PONTOON_INBOARD_OUTBOARD" select="'PIO'"/>
<xsl:variable name="BOATTYPE_PONTOON_OUTBOARD" select="'PO'"/>
<xsl:variable name="BOATTYPE_ROW_BOAT" select="'RB'"/>
<xsl:variable name="BOATTYPE_SAILBOAT" select="'S'"/>
<xsl:variable name="BOATTYPE_SAILBOAT_WITH_OUTBOARD" select="'SWOB'"/>
<xsl:variable name="BOATTYPE_WAVERUNNER" select="'W'"/>
<xsl:variable name="BOAT_TYPE_OUTBOARD" select="'O'"/>
<xsl:variable name="BOATTYPE_JETSKI_TRAILER" select="'JT'"/>
<xsl:variable name="BOATTYPE_JETSKI_TRAILER_FROM_APP" select="'TRSK'"/>

<xsl:variable name="BOATTYPE_WAVERUNNER_TRAILER" select="'WRT'"/>
<xsl:variable name="BOATTYPE_TRAILER" select="'TRAI'"/>

<xsl:variable name="BOATSTYLE_OUTBOARD" select="'O'"/>


<xsl:variable name="BOATSTYLE_INBOARD_OUTBOARD" select="'IO'"/>
<xsl:variable name="BOATSTYLE_INBOARD" select="'I'"/>
<xsl:variable name="BOATSTYLE_JETSKI" select="'JS'"/>
<xsl:variable name="BOATSTYLE_SAILBOAT" select="'S'"/>
<xsl:variable name="BOATSTYLE_TRAILER" select="'T'"/>

<xsl:variable name="WATERTYPE_GREATLAKES" select="'G'"/>
<xsl:variable name="CALLED_FROM_LIABILITY" select="'LIABILITY'"/>
<xsl:variable name="CALLED_FROM_PD" select="'PD'"/>
<xsl:variable name="CONSTRUCTIONCODE_WOOD" select="'W'"/> <!-- Same as that in default xml for watercraft and masterlookup codes --> 
<xsl:variable name="CONSTRUCTIONCODE_FIBREGLASS" select="'F'"/> <!-- Same as that in default xml  for watercraft and masterlookup codes --> 

<!-- ============================================================================================ 
								Loading ProductFactorMaster File (END)						  
 ============================================================================================ -->
		 
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
			<body class="midcolora" bgcolor="ffffff">
				<br/>
				<xsl:apply-templates select="QUICKQUOTE" />
			</body>
		<span style="display:none">
			<returnValue>
				<xsl:value-of select="user:AllVerified(1)" />
			</returnValue>
		</span>
		</html>
		
		
		
		
	</xsl:template>
	
	
<xsl:template match="QUICKQUOTE">
		
		<table border="0" align="center" width='90%'>
		<tr class="headereffectCenter">
			<td colspan='6' style="FONT-SIZE: 14pt">
				<b>
					Error Report
				</b>
			</td>
		</tr>
		<tr><td></td></tr>
		<tr><td></td></tr>
		<tr><td></td></tr>
		
		</table>
		
		<table border="0" align="center" width='90%' bgcolor="ffffff">
		  <ul type="disc"> 
		  	 <tr>
				<td class="midcolora">
				
					   <xsl:call-template name="MAIN_FOR_INPUT_XML"/>  
				
				 <xsl:if test="user:AllVerified(1) = 1">   
					 
						<xsl:call-template name="MAIN_FOR_MASTER_XML"/>
					  
				  </xsl:if>  	
				</td>
			</tr>
		 </ul>
		</table>
		
	</xsl:template>
	
	
	
	
<xsl:template name ="MAIN_FOR_MASTER_XML">

    	 

<!-- Physical Damage -->

 	<xsl:call-template name="PHYSICAL_DAMAGE" />    

<!-- Watercraft Liability -->

	<xsl:call-template name="WATERCRAFT_LIABILITY_DISPLAY_EXTENDED" />
	<xsl:call-template name="WATERCRAFT_LIABILITY" />   

<!-- Medical Payments to others -->

	<xsl:call-template name="MEDICAL_PAYMENTS_DISPLAY" />  
	<xsl:call-template name="MEDICAL_PAYMENTS" />

<!-- Uninsured Boaters -Included-->

	<!--xsl:call-template name="UNINSURED_BOATERS_DISPLAY" /-->

<!-- Uninsured Boaters-Value -->

	<xsl:call-template name="UNINSURED_BOATERS_COVERAGE" />

<!-- Boat Towing and Emergency Services -->

<!--	<xsl:call-template name="BOAT_TOWING_PREMIUM"/> --> 

<!-- Boat Replacement Cost Coverage -->

	<!-- <xsl:call-template name="BOAT_REPLACEMENT_DISPLAY" /> -->

<!-- Client Entertainment, Form OP-720  -->

	<xsl:call-template name="CLIENT_ENTERTAINMENT" />

<!-- Watercraft Liability Pollution Coverage, Form OP-900  -->

	<xsl:call-template name="LIABILITY_POLLUTION_COVERAGE" />

<!-- Agreed Value Endorsement, Form AV-100    -->

	<xsl:call-template name="AGREED_VALUE_ENDORSEMENT" />   

<!-- Discount - Insurance Score Credit (Score 700)  -->

	<!-- <xsl:call-template name="INSURANCESCORE_DISPLAY" /> -->

<!-- Discount Wolverine Homeowners  -->

	<!-- <xsl:call-template name="DISCOUNT_BOAT_HOME"/> -->

<!-- Discount - 5 Years Experience or Navigation Course   -->

<!-- <xsl:call-template name="EXPERIENCE_CREDIT_PREMIUM"/> -->

<!-- Discount - Diesel Engine   -->

	<!-- <xsl:call-template name="DIESEL_ENGINE_PREMIUM"/> -->

<!-- Discount - Halon Fire Extinguishing System   -->

<!--	<xsl:call-template name="HALON_FIRE_PREMIUM"/> --> 

<!-- Discount - Loran, GPS or other navigation System  -->

<!--	<xsl:call-template name="LORAN_GPS_PREMIUM"/> -->

<!-- Discount - Shore Station -->

<!--	<xsl:call-template name="SHORE_STATION_PREMIUM"/>  -->

<!-- Discount - Multi-Boat -->

	<!-- <xsl:call-template name="MULTI_BOAT_PREMIUM"/> -->

<!-- Discount - Age of Boat -->

	<!-- <xsl:call-template name="AGE_DISCOUNT_DISPLAY"/> -->

<!-- Discount - Wooden Boat -->

<!--	<xsl:call-template name="WOODEN_BOAT_PREMIUM"/>  -->

<!-- Charge - Dual Ownership -->

<!--	<xsl:call-template name="DUAL_OWNERSHIP_PREMIUM"/> -->

<!-- Charge - Waverunner 10 %-->

<!--	<xsl:call-template name="WAVERUNNER_PREMIUM_DISPLAY"/> -->

<!-- Charge - Length over 26 Feet-->

<!-- 	<xsl:call-template name="LENGTH_26FEET_PREMIUM" /> -->

<!-- Charge - Length over 26 Feet and speed 55-65 miles per hour-->

<!--	<xsl:call-template name="BOAT_LENGTH_SPEED_SURCHARGE_PREMIUM" /> -->

<!-- Charge - FibreGlass Boats over 15 year old-->

<!--<xsl:call-template name="FIBRE_GLASS_PREMIUM" />  --> 

<!-- Charge - Remove Sailboat Racing Exclusion-->

<!--	<xsl:call-template name="REMOVE_SAILBOAT_PREMIUM" /> -->

<!-- Charge - MINIJET BOAT 10 % -->

<!-- 	<xsl:call-template name="MINIJET_PREMIUM" /> --> 

<!-- Charge County of Operation -->

<!-- <xsl:call-template name="COUNTY_OF_OPERATION_DISPLAY" /> -->

<!-- Unattached Equipment -->

<!--	<xsl:call-template name="UNATTACHED_PREMIUM_VALUE" /> -->

<!-- Unattached Equipment -->

<!-- 	<xsl:call-template name="UNATTACHED_PREMIUM" /> --> 


<!-- Minimum  Premium -->

<!-- <xsl:call-template name="MINIMUM_PREMIUM" /> -->

<!-- Total  Premium -->

<!-- 	<xsl:call-template name="FINAL_PREMIUM" /> --> 
	
	 	
</xsl:template>



 

<!-- ============================================================================================ -->
<!--				Templates for optional endorsements Display (START)							  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
<!-- ============================================================================================ -->
<xsl:template name="OPTIONAL_ENDORSEMENTS">
 
<!-- 
<xsl:variable name="VAR1"><xsl:call-template name="CLIENT_ENTERTAINMENT"/></xsl:variable>
 <xsl:variable name="VAR2"><xsl:call-template name="LIABILITY_POLLUTION_COVERAGE"/></xsl:variable>
 <xsl:variable name="VAR3"><xsl:call-template name="AGREED_VALUE_ENDORSEMENT"/></xsl:variable>
 
 <xsl:value-of select="$VAR1+$VAR2+$VAR3"/>
 -->
 	
	
</xsl:template>
<!-- ============================================================================================ -->
<!--				Templates for optional endorsements, Display (END)							  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
<!-- ============================================================================================ -->



<!-- ============================================================================================ -->
<!--				Templates for Physical Damage, Display (END)								  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN				              -->
<!-- ============================================================================================ -->
	
	<xsl:template name="PHYSICAL_DAMAGE">
		<xsl:choose>
		     
			<xsl:when test ="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:call-template name ="PHYSICAL_DAMAGE_COMPONENT"/>				 
			</xsl:when>			
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<!--Watercraft Physical Damage - INDIANA-->
	<xsl:template name="PHYSICAL_DAMAGE_COMPONENT">
		
		<!-- Physical Damage calculations are dependant on the style of the boat.
			 However, as per the manuals, templates for Minijet, Waverunner, 
			 Jet Ski(w Lift-bar) are different from the other styles 'Inboard,Inboard/Outboard, Sailboat'-->
			 
		<!-- Fetch type -->
		<xsl:variable name="P_BOATTYPECODE">			
			<xsl:value-of select="BOATS/BOAT/BOATTYPECODE"/>
		</xsl:variable>
		
		<!-- Fetch style -->
		<xsl:variable name="P_BOAT_STYLE">			
			<xsl:value-of select="BOATS/BOAT/BOATSTYLECODE"/>
		</xsl:variable>
		
		<xsl:choose>
			<xsl:when test="$P_BOATTYPECODE = $BOATTYPE_WAVERUNNER">  <!-- Waverunner -no lift bar-->
				<xsl:call-template name='WAVE_RATE_PHYSICAL_DAMAGE'/>
			</xsl:when>	
			<xsl:when test="$P_BOATTYPECODE = $BOATTYPE_MINI_JET"> <!-- Mini Jet Boat -->
				<xsl:call-template name='MINIJET_RATE_CALC'/>		 <!-- i was here on sat -->		  
			</xsl:when>
			<xsl:when test="$P_BOATTYPECODE = $BOATTYPE_JETSKI_WITH_LB"> <!-- Jet Ski with Liftbar -->
			   <xsl:call-template name='JETSKI_W_LIFTBAR_RATE_CALC'/>  
			</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="$P_BOAT_STYLE = $BOATSTYLE_OUTBOARD"> <!-- Outboard -->
						  <xsl:call-template name='OUTBOARD_RATE_CALC'/>  
					</xsl:when>			
					<!-- Inboard, Inboard/Outboard, Sailboat have the same calculations -->
					<xsl:when test="$P_BOAT_STYLE = $BOATSTYLE_INBOARD or $P_BOAT_STYLE = $BOATSTYLE_INBOARD_OUTBOARD or $P_BOAT_STYLE = $BOATSTYLE_SAILBOAT"> 
						   
						   <xsl:call-template name='INBOARD_IO_SAILBOAT_RATE_CALC'/>    
						 
					</xsl:when>								
					<xsl:when test="$P_BOAT_STYLE = $BOATSTYLE_JETSKI ">
						<xsl:call-template name='JETSKI_W_LIFTBAR_RATE_CALC'/> <!-- Same as Jet Ski with Liftbar -->
					</xsl:when>	
					
					<!-- Waverunner Trailer and JetSki Trailer and Trialer have same calculations -->	 
					<xsl:when test="$P_BOAT_STYLE = $BOATSTYLE_TRAILER"> 			 
						<xsl:call-template name='TRAILOR_RATE_CALC'/>
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>	
		</xsl:choose>
	</xsl:template>	
	
	
	<!-- END OF MAIN FACTOR TEMPLATES -->
	
	
	
	<!-- BEGINNING OF FACTOR TEMPLATES -->
	<!-- ============================================================================================ -->
	<!--				Templates for WaveRunner, Display (START)									  -->
	<!--		    					  FOR INDIANA									              -->
	<!-- ============================================================================================ -->
	
	<xsl:template name="WAVE_RATE_PHYSICAL_DAMAGE">
	
	<!-- 
		Use Inboard/Outboard Great Lakes Base Rate multiplied by a factor of 1.10. Use Deductible options from the
			Credits Section. Follow the formula listed below to determine total premium.
			1) Find Inboard/Outboard Great Lakes Rate.
			2) Multiply by a factor of WaveRunner charge (1.10)
			3) Multiply by Coverage Relativity (Use Inboard/Outboard Table)
			4) Multiply Territory Factor. (NOT REQUIRED as we are picking factors territory wise. We do not take base as territory 1)
			5) Multiply Insurance Score Credit.
			6) Apply Deductible Relativity.
			7) Apply Dual Ownership Surcharge (If Eligible)
			8) Term Factor: 1.00 if annual or 0.50 if semi-annual. 
			====================================================			
			9) Add: Agreed Value Endorsement.					|
			10) Add: Liability from Table Below.				| These will be added in the final premium formula if applicable
			11) Add: OP-900 Watercraft Pollution Coverage.		|
			====================================================			
			
	-->
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="BASE_RATES_WAVERUNNER"/> 
		</xsl:variable>
		 
		 
		<xsl:variable name="VAR_WAVERUNNER_CHARGE">
			<xsl:call-template name="WAVERUNNER_CHARGE_FACTOR"/> 
		</xsl:variable>		
	
		<xsl:variable name="VAR_COVERAGE_RELATIVITY">
			<xsl:call-template name="COVERAGE_RELATIVITY_WAVERUNNER"/>
		</xsl:variable>
	
		<xsl:variable name="VAR_INSURANCE_SCORE">
			<xsl:call-template name="INSURANCESCORE"/>
		</xsl:variable>
	
		<xsl:variable name="VAR_DEDUCTIBLE">
			<xsl:call-template name="DED_WAVERUNNER"/> 
		</xsl:variable>
	
		<xsl:variable name="VAR_DUAL_OWNERSHIP">
			 <xsl:call-template name="DUAL_OWNERSHIP_FACTOR">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_PD"/>
			 </xsl:call-template>
		</xsl:variable>	
	
		<xsl:variable name="VAR_POLICYTERMS">
			<xsl:call-template  name="POLICYTERM"/>
		</xsl:variable>
		
				
			<xsl:if test="normalize-space($VAR_DUAL_OWNERSHIP) = ''">
				<tr>
					<td class="midcolora"><li> Liability for dual ownership factor not found  (Path - "FACTOR[@ID='LISURCHARGE']/NODE[@ID ='LIDUALOWNERSHIP']/ATTRIBUTES/@FACTOR") </li> </td>
				</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
				<xsl:if test="normalize-space($VAR_POLICYTERMS) = ''">
					<tr>
						<td class="midcolora"><li> Policy term not found  (Path - "FACTOR[@ID='TERM_FACTOR']/NODE[@ID ='TERMFACTOR']/ATTRIBUTES[@MONTHS = $P_POLICYTERMS]/@FACTOR") </li> </td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
		<!--
		<xsl:value-of select="round(round(round(round(round(round($VAR_BASE * $VAR_WAVERUNNER_CHARGE)* $VAR_COVERAGE_RELATIVITY )* $VAR_INSURANCE_SCORE) *$VAR_DEDUCTIBLE )*$VAR_DUAL_OWNERSHIP) *$VAR_TERMS)"/>
		-->
			
		
		
	</xsl:template>
	<!--End Calculation-->
	<!-- ============================================================================================ -->
	
	<!--Coverage Relativity of Waverunner -->
	<xsl:template name="COVERAGE_RELATIVITY_WAVERUNNER">
		 
		<!-- Same as that of Inboard/Outboard -->
		<xsl:variable name ="COVERGE_RELATIVITY_VALUE">
			<xsl:call-template name="GET_COVERAGE">				 
				<xsl:with-param name="STYLE_FOR_RELATIVITY" select="'INOUTBOARD'"/>
			</xsl:call-template>
		</xsl:variable>
		<xsl:value-of select="$COVERGE_RELATIVITY_VALUE"/>
		
	</xsl:template>
	
	<!--Base Rates values of Waverunner:Find Inboard/Outboard Great Lakes Rate.-->
	<xsl:template name ="BASE_RATES_WAVERUNNER">
	    <xsl:variable name ="PBOATTYPE" select= "BOATS/BOAT/BOATTYPE" />
		<xsl:variable name ="WATERTYPE" select= "BOATS/BOAT/WATERSCODE"/> 
		<xsl:variable name ="PTERRITORY" select= "BOATS/BOAT/TERRITORYDOCKEDIN"/>  
		 
		 <xsl:variable name ="VAR_INOUTBOARD"> 
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERTYPE]/ATTRIBUTES[@TERRITORYCODE=$PTERRITORY]/@INOUTBOARD"/>
	    </xsl:variable>
	  
	    <xsl:if test="normalize-space($VAR_INOUTBOARD) = ''">
				<tr>
					<td class="midcolora"><li> Base Rates values of Waverunner  not found  (Path - "BASERATES/NODE[@ID]/ATTRIBUTES/INOUTBOARD")</li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
		<xsl:value-of select="$VAR_INOUTBOARD"/>
			
	</xsl:template>
	
	<!-- Waverunner charge -->
	<xsl:template name="WAVERUNNER_CHARGE_FACTOR">
	<xsl:variable name="VAR_WAVERUNNER_CHARGE_FACTOR"> 
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='WAVERUNNER_CHARGE']/NODE[@ID='WAVERUNNER_CHARGES']/ATTRIBUTES/@FACTOR"/>
	</xsl:variable>
	
	 <xsl:if test="normalize-space($VAR_WAVERUNNER_CHARGE_FACTOR) = ''">
				<tr>
					<td class="midcolora"><li> Base Rates values of Waverunner  not found  (Path - "FACTOR[@ID='WAVERUNNER_CHARGE']/NODE[@ID='WAVERUNNER_CHARGES']/ATTRIBUTES/FACTOR")</li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
		<xsl:value-of select="$VAR_WAVERUNNER_CHARGE_FACTOR"/>
		
		
	
	</xsl:template>
	
	<!--Wave runner Deduction-->
	<xsl:template name="DED_WAVERUNNER">
		<xsl:variable name ="DED_DESC" select= "BOATS/BOAT/DEDUCTIBLE"></xsl:variable>
		<xsl:variable name ="VAR_DED_WAVERUNNER"> 
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID='DEDUCTIBLE']/ATTRIBUTES[@DESC = $DED_DESC]/@FACTOR"/>	
		</xsl:variable>
		
		 <xsl:if test="normalize-space($VAR_DED_WAVERUNNER) = ''">
				<tr>
					<td class="midcolora"><li> deductible for waverunner  not found  (Path - "FACTOR[@ID='PDCREDIT']/NODE[@ID='DEDUCTIBLE']/ATTRIBUTES[@DESC = $DED_DESC]/@FACTOR")</li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
		<xsl:value-of select="$VAR_DED_WAVERUNNER"/>
		
		
	</xsl:template>

	<!-- Display -->
	<xsl:template name="WAVERUNNER_PREMIUM_DISPLAY">
		<xsl:choose>			 
			<xsl:when test ="POLICY/STATENAME ='INDIANA'  or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:choose>
					<xsl:when test = "BOATS/BOAT/BOATTYPECODE=$BOATTYPE_WAVERUNNER">Included</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>		
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
		
		
		
		
	</xsl:template>

<!-- ============================================================================================ -->
<!--				Templates for WaveRunner, Display (END) 									  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
<!-- ============================================================================================ -->
	
<!-- ============================================================================================ -->
<!--				Templates for MINIJET, Display (START)									  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
<!-- ============================================================================================ -->

<xsl:template name="MINIJET_RATE_CALC">
	<!-- 
		1) Base Rate.
		2) Multiply by Mini Jet Boat Surcharge 1.10 | Not requried since factormaster has data accordingly.
		3) Choose Great Lake or Inland Lakes (0.95).
		4) Choose Territory.(NOT REQUIRED as we are picking factors territory wise. We do not take base as territory 1)
		5) Multiply by Coverage Relativity. Use Inboard/Outboard Table. If Coverage is $1.00 over coverage amount, then
		use next Coverage Relativity. (Example: For a coverage amount of $26,100 use $27,000 coverage relativity.)
		6) Multiply by Insurance Score Factor.
		7) Multiply by Deductible Relativity.
		8) Multiply by Dual Ownership Surcharge, if eligible.
		9) Term Factor: 1.00 if annual or 0.50 if semi-annual.
		====================================================			
		10) Add: Agreed Value Endorsement.					|
		11) Add: Liability from Table Below.				| These will be added in the final premium formula if applicable
		12) Add: OP-900 Watercraft Pollution Coverage.		|
		====================================================	
		-->
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="BASE_RATES_MINIJET"/> 
		</xsl:variable>
	 
		<xsl:variable name="VAR_COVERAGE_RELATIVITY">
			<xsl:call-template name="COVERAGE_RELATIVITY_MINIJET"/>
		</xsl:variable>
		<xsl:variable name="VAR_INSURANCE_SCORE">
			<xsl:call-template name="INSURANCESCORE"/>
		</xsl:variable>
		<xsl:variable name="VAR_DEDUCTIBLE">
			<xsl:call-template name="DED_MINIJET"/> 
		</xsl:variable>
		<xsl:variable name="VAR_DUAL_OWNERSHIP">  <!--  i was here on friday   -->
			 <xsl:call-template name="DUAL_OWNERSHIP_FACTOR">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_PD"/>
			 </xsl:call-template>
		</xsl:variable>			
		  
		<xsl:variable name="VAR_TERMS">
			<xsl:call-template  name="POLICYTERM"/>
		</xsl:variable>
		
		
		<xsl:if test="normalize-space($VAR_DUAL_OWNERSHIP) = ''"> 
			<tr>
				<td class="midcolora"><li> Dual ownership factor for PD not found  (Path - "FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDDUALOWNERSHIP']/ATTRIBUTES/@FACTOR") </li> </td>
			</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>		
		</xsl:if>
				
		<!--
		<xsl:value-of select="round(round(round(round(round($VAR_BASE * $VAR_COVERAGE_RELATIVITY) * $VAR_INSURANCE_SCORE )*$VAR_DEDUCTIBLE )*$VAR_DUAL_OWNERSHIP )*$VAR_TERMS)"/>
		 -->
	  
 
		
</xsl:template>

<!-- ============================================================================================ -->
	
	<!--Coverage Relativity of Minijet -->
	<xsl:template name="COVERAGE_RELATIVITY_MINIJET">
	
		<!-- Same as that of Inboard/Outboard -->
		<xsl:variable name ="COVERGE_RELATIVITY_VALUE">
			<xsl:call-template name="GET_COVERAGE">				 
				<xsl:with-param name="STYLE_FOR_RELATIVITY" select="'INOUTBOARD'"/>
			</xsl:call-template>
		</xsl:variable>
		<xsl:value-of select="$COVERGE_RELATIVITY_VALUE"/>
	</xsl:template>
	
	<!--Base Rates values of Minijet:Find Inboard/Outboard Great Lakes Rate.-->
	<xsl:template name ="BASE_RATES_MINIJET">
	    <xsl:variable name ="PBOATTYPE" select= "BOATS/BOAT/BOATTYPE" />
		<xsl:variable name ="WATERTYPE" select= "BOATS/BOAT/WATERSCODE"/>
		<xsl:variable name ="PTERRITORY" select= "BOATS/BOAT/TERRITORYDOCKEDIN"/>  
		
		<xsl:variable name="VAR_BASE_RATES_MINIJET" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERTYPE]/ATTRIBUTES[@TERRITORYCODE=$PTERRITORY]/@MINIJET"/>
	
				<xsl:if test="normalize-space($VAR_BASE_RATES_MINIJET) = ''">
					<tr>
						<td class="midcolora"><li> Base rate for mini jet not found  (Path - "FACTOR[@ID='BASERATES']/NODE[@ID=$WATERTYPE]/ATTRIBUTES[@TERRITORYCODE=$PTERRITORY]/@MINIJET") </li> </td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
	
	<xsl:value-of select ="$VAR_BASE_RATES_MINIJET"/> 
	
	</xsl:template>
	
	
	
	<!-- Minijet charge -->
	<xsl:template name="MINIJET_CHARGE_FACTOR">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIJET_CHARGE']/NODE[@ID='MINIJET_CHARGES']/ATTRIBUTES/@FACTOR"/>
	</xsl:template>
	
	<!--Minijet Deduction-->
	<xsl:template name="DED_MINIJET">
		<xsl:variable name ="DED_DESC" select= "BOATS/BOAT/DEDUCTIBLE"></xsl:variable>
		
		<xsl:variable name="VAR_DED_MINIJET"> 
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID='DEDUCTIBLE']/ATTRIBUTES[@DESC = $DED_DESC]/@FACTOR"/>	
		</xsl:variable>
		
		<xsl:if test="normalize-space($VAR_DED_MINIJET) = ''">
					<tr>
						<td class="midcolora"><li> Deductible for mini jet not found  (Path - "FACTOR[@ID='PDCREDIT']/NODE[@ID='DEDUCTIBLE']/ATTRIBUTES[@DESC = $DED_DESC]/@FACTOR") </li> </td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
		<xsl:value-of select="$VAR_DED_MINIJET"/>
				
		
	</xsl:template>

	<!-- Display -->
	<xsl:template name="MINIJET_PREMIUM_DISPLAY">
		<xsl:choose>			 
			<xsl:when test ="POLICY/STATENAME ='INDIANA'  or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:choose>
					<xsl:when test = "BOATS/BOAT/BOATTYPE=$BOATTYPE_MINIJET">Included</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>		
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	
<!-- ============================================================================================ -->
<!--				Templates for Minijet, Display (START)									  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--				Templates for JETSKI_WITH_LIFTBAR, Display (START)									  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
<!-- ============================================================================================ -->

<xsl:template name="JETSKI_W_LIFTBAR_RATE_CALC">
	<!-- 
		1) Find Coverage Base Rate. Rate per $100 and by Deductible option listed above.
		2) Multiply Territory Factor.  | Ths is required 
		3) Multiply by Insurance Score Credit.
		4) Apply Dual Ownership Surcharge (If Eligible).		
		5) Term Factor: 1.00 if annual or 0.50 if semi-annual.
		====================================================			
		6) Add: Agreed Value Endorsement.					|
		7) Add: Liability from Table Below.					| These will be added in the final premium formula if applicable
		8) Add: OP-900 Watercraft Pollution Coverage.		|
		====================================================	
		-->
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="BASE_RATES_JETSKI_W_LIFTBAR"/> 
		</xsl:variable> 
		
		<xsl:variable name="VAR_TERRITORY_FACTOR">
			<xsl:call-template name="COUNTY_OPERATION_TERRITORY_FACTOR"/>
		</xsl:variable>
		
		<xsl:variable name="VAR_INSURANCE_SCORE">
			<xsl:call-template name="INSURANCESCORE"/>
		</xsl:variable>
				
		<xsl:variable name="VAR_DUAL_OWNERSHIP">
			 <xsl:call-template name="DUAL_OWNERSHIP_FACTOR">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_PD"/>
			 </xsl:call-template>
		</xsl:variable>			
		  
		<xsl:variable name="VAR_TERMS">
			<xsl:call-template  name="POLICYTERM"/>
		</xsl:variable>
		
		 <!--
		<xsl:value-of select="round(round(round(round($VAR_BASE * $VAR_TERRITORY_FACTOR)*$VAR_INSURANCE_SCORE )*$VAR_DUAL_OWNERSHIP )*$VAR_TERMS)"/>
		 -->
		 
	 
	 
		
</xsl:template>

<!-- ============================================================================================ -->
	

	<!--Base Rates values of JetSki with liftbar:  Rate.-->
	<xsl:template name ="BASE_RATES_JETSKI_W_LIFTBAR">    
		 
		<!--  <xsl:variable name="P_COVERAGE_AMOUNT" select="BOATS/BOAT/MARKETVALUE" /> -->
		 <xsl:variable name="P_COVERAGE_AMOUNT"> 
				<xsl:call-template name="MARKETVALUE_IN_THOUSANDS"> 
					<xsl:with-param name="FACTOR_MARKETVAL" select="BOATS/BOAT/MARKETVALUE"/>
				</xsl:call-template>
		 </xsl:variable>
		 
		 
		<xsl:variable name ="PDEDUCTIBLE" select= "BOATS/BOAT/DEDUCTIBLE"/>
		
		<xsl:variable name ="P_RATE_PER_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='PDJETSKI']/ATTRIBUTES[@DEDUCTIBLE=$PDEDUCTIBLE]/@RATE_PER_VALUE"/>
		<xsl:variable name ="P_RATE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='PDJETSKI']/ATTRIBUTES[@DEDUCTIBLE=$PDEDUCTIBLE]/@RATE"/>
		<xsl:value-of select="round(($P_COVERAGE_AMOUNT div $P_RATE_PER_VALUE) * $P_RATE)"/> 
		
		<xsl:if test="normalize-space($P_RATE_PER_VALUE) = ''">
			<tr>
				<td class="midcolora"><li> Rate per value for jet ski not found  (Path - "FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='PDJETSKI']/ATTRIBUTES[@DEDUCTIBLE=$PDEDUCTIBLE]/@RATE_PER_VALUE") </li> </td>
			</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		
		<xsl:if test="normalize-space($P_RATE) = ''"> 
			<tr>
				<td class="midcolora"><li> Rate for jet ski not found  (Path - "FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='PDJETSKI']/ATTRIBUTES[@DEDUCTIBLE=$PDEDUCTIBLE]/@RATE") </li> </td>
			</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		 </xsl:if>
		 
		
	</xsl:template>
	
	
	
	
<!-- ============================================================================================ -->
<!--				Templates for Jet Ski , Display (START)									  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
<!-- ============================================================================================ -->




<!-- ============================================================================================ -->
<!--				Templates for Outboard, Display (START)									  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
<!-- ============================================================================================ -->
<xsl:template name="OUTBOARD_RATE_CALC">
	<!-- 
		1) Find Outboard Base Rate.															|
		2) Choose Territory and multiply by appropriate relativity.							|
		3) Multiply by Coverage Relativity. If Coverage is $1.00 over coverage amount,		|Same.Just calculate base
			then use next Coverage Relativity.												|
			(Example: For a coverage amount of $26,100 use $27,000 coverage relativity.)	|
		4) Multiply by Insurance Score Factor.
		5) Apply Credits in order as listed. Multiply and Round to the Dollar after each calculation.
		6) Apply Surcharges in order as listed. Multiply and Round to the Dollar after each calculation.
		7) Term Factor: 1.00 if annual or 0.50 if semi-annual.
		====================================================			
		8) Add: Agreed Value Endorsement.					|
		9) Add: Liability from Table Below.					| These will be added in the final premium formula if applicable
		10) Add: OP-900 Watercraft Pollution Coverage.		|
		====================================================	
		-->
		
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="BASE_RATES_OUTBOARD"/> 
		</xsl:variable> 
		
		<!-- Same as that of Outboard -->
		<xsl:variable name ="COVERGE_RELATIVITY_VALUE">
			<xsl:call-template name="GET_COVERAGE">				 
				<xsl:with-param name="STYLE_FOR_RELATIVITY" select="'OUTBOARD'"/>
			</xsl:call-template>
		</xsl:variable>
		
		<xsl:variable name="VAR_INSURANCE_SCORE">
			<xsl:call-template name="INSURANCESCORE"/>
		</xsl:variable>
		
		
		<!-- Credits in listed order 
			1.Multi-Policy Credit with Home
			2.Experience Credit
			3.Diesel Engine Craft
			4.Deductible
			5.Installation of Halon Fire
			6. Loran Navigation System,
			7. Shore Station Credit
			8. Multi-Boat
			9. Age of Watercraft  
		-->
		
		<xsl:variable name="VAR_BOAT_HOME">
			<xsl:call-template name="DISCOUNT_BOAT_HOME_FACTOR"/>	
		</xsl:variable>	
							
		 
		
		<xsl:variable name="VAR_EXPERIENCE_CREDIT">
			<xsl:call-template name="EXPERIENCE_CREDIT_FACTOR"/>	
		</xsl:variable>		
		
				
			
		
		<xsl:variable name="VAR_DIESEL_ENGINE">
			<xsl:call-template name="DIESEL_ENGINE_FACTOR"/>	
		</xsl:variable>	
		
			
		
		
		<xsl:variable name="VAR_DEDUCTIBLE">
			<xsl:call-template name="DEDUCTIBLE_FACTOR"/>	
		</xsl:variable>		
		
						
			


		
		<xsl:variable name="VAR_HALON_FIRE">
			<xsl:call-template name="HALON_FIRE_FACTOR"/>	
		</xsl:variable>	
		
			
	
		
		<xsl:variable name="VAR_LORAN_GPS_FACTOR">
			<xsl:call-template name="LORAN_GPS_FACTOR"/>	
		</xsl:variable>		
		
		
			
		
		
		<xsl:variable name="VAR_SHORE_STATION">
			<xsl:call-template name="SHORE_STATION_FACTOR"/>	
		</xsl:variable>
		
			
		
		
		<xsl:variable name="VAR_MULTI_BOAT">
			<xsl:call-template name="MULTI_BOAT_FACTOR"/>	
		</xsl:variable>	
					
		
		<xsl:variable name="VAR_AGE_FACTOR">
			<xsl:call-template name="AGE_FACTOR"/>	
		</xsl:variable>	
		
					
					
			
		
		<!-- Surcharges in listed order 
			1. Wooden boats over 10 years old.
			2. Fiberglass boats over 15 years old.
			3. Removal of sailboat racing exclusion under 26 feet only.
			4. Dual Ownership
			5. Boats over 26 feet and speeds 55-65 miles per hour.
		
		-->
		
			  
		 <xsl:variable name="VAR_WOODEN_BOAT_FACTOR">
			 <xsl:call-template name="WOODEN_BOAT_FACTOR"/>
		</xsl:variable>		
		
		 <xsl:variable name="VAR_FIBRE_GLASS_FACTOR">
			 <xsl:call-template name="FIBRE_GLASS_FACTOR"/>
		</xsl:variable>	 
		
		<xsl:variable name="VAR_REMOVE_SAILBOAT_FACTOR">
			 <xsl:call-template name="REMOVE_SAILBOAT_FACTOR">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_PD"/>
			 </xsl:call-template>
		</xsl:variable>	
		
		
		
		<xsl:variable name="VAR_DUAL_OWNERSHIP">
			 <xsl:call-template name="DUAL_OWNERSHIP_FACTOR">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_PD"/>
			 </xsl:call-template>
		</xsl:variable>	
		
		<xsl:variable name="VAR_LENGTH_SPEED_SURCHARGE">
			<xsl:call-template name="BOAT_LENGTH_SPEED_SURCHARGE">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_PD"/>
			</xsl:call-template>
		</xsl:variable>		
	
		<!-- Term Factor -->
		<xsl:variable name="VAR_TERMS">
			<xsl:call-template  name="POLICYTERM"/>
		</xsl:variable>
		
		<!--
		<xsl:variable name="VAR_FIRSTCOMPONENT" select="round(round(round(round(round(round(round(round($VAR_BASE * $COVERGE_RELATIVITY_VALUE)* $VAR_INSURANCE_SCORE )*$VAR_BOAT_HOME )*$VAR_EXPERIENCE_CREDIT)*$VAR_DIESEL_ENGINE)*$VAR_DEDUCTIBLE)*$VAR_HALON_FIRE)*$VAR_LORAN_GPS_FACTOR)"/>
		<xsl:value-of select="round(round(round(round(round(round(round(round(round($VAR_FIRSTCOMPONENT*$VAR_SHORE_STATION)*$VAR_MULTI_BOAT)*$VAR_AGE_FACTOR)*$VAR_WOODEN_BOAT_FACTOR)*$VAR_FIBRE_GLASS_FACTOR)*$VAR_REMOVE_SAILBOAT_FACTOR)*$VAR_DUAL_OWNERSHIP)*$VAR_LENGTH_SPEED_SURCHARGE)*$VAR_TERMS)"/>
	 -->
	  
 
		
</xsl:template>

<!-- ============================================================================================ -->
	

	<!--Base Rates values of Outboard:  Rate.-->
	<xsl:template name ="BASE_RATES_OUTBOARD">    
		 
		<xsl:variable name ="PBOATTYPE" select= "BOATS/BOAT/BOATTYPE" />
		<xsl:variable name ="WATERTYPE" select= "BOATS/BOAT/WATERSCODE"/>
		<xsl:variable name ="PTERRITORY" select= "BOATS/BOAT/TERRITORYDOCKEDIN"/> 
		
		<xsl:variable name ="VAR_BASE_RATES_OUTBOARD"  select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERTYPE]/ATTRIBUTES[@TERRITORYCODE=$PTERRITORY]/@OUTBOARD"/>
		
		<xsl:if test="normalize-space($VAR_BASE_RATES_OUTBOARD) = ''">
			<tr>
				<td class="midcolora"><li> Base rate of outboard not found  (Path - "FACTOR[@ID='BASERATES']/NODE[@ID=$WATERTYPE]/ATTRIBUTES[@TERRITORYCODE=$PTERRITORY]/@OUTBOARD") </li> </td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>


	</xsl:template>
	
	
<!-- ============================================================================================ -->
<!--				Templates for Outboard , Display (START)									  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
<!-- ============================================================================================ -->



<!-- ============================================================================================ -->
<!--				Templates for Inboard, Inboard/Outboard, Sailboat, Display (START)									  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
<!-- ============================================================================================ -->
<xsl:template name="INBOARD_IO_SAILBOAT_RATE_CALC">
	<!-- 
		1. Find Base Rate depending on Type of Boat. Great Lakes, Territory 1 is the base for all.
		2. Multiply by Inland Lake Credit Factor 0.95, if eligible.
		3. Choose Territory and multiply by appropriate relativity.
		4. If Inboard or Inboard/Outboard over 26 ft. multiply base by 1.10.
		5. Multiply by Coverage Relativity. If Coverage is 1.00 over coverage amount, then use next Coverage Relativity.
		(Example: Fora coverage amount of 26,100 use 27,000 coverage relativity.)
		6. Multiply by Insurance Score Factor.
		7. Apply Credits in order as listed. Multiply and Round to the Dollar after each calculation.
		8. Apply Surcharges in order as listed. Multiply and Round to the Dollar after each calculation.
		9. Term Factor = 1.00 if annual   0.50 if semi-annual.
		====================================================			
		10. Add: Agreed Value Endorsement.					-
		11. Add: Liability from Table Below.					- These will be added in the final premium formula if applicable
		12. Add: OP-900 Watercraft Pollution Coverage.		-
		====================================================	
		-->
		
		<xsl:variable name="VAR_BASE">
			<xsl:call-template name="BASE_RATES_INBOARD_IO_SAILBOAT_RATE"/> 
		</xsl:variable> 
		
		
		<!--  If Inboard or Inboard/Outboard over 26 ft. multiply base by the factor (1.10) ASK RAJAN since we are already charging 10% for length ovr 26 ft--> 
		<xsl:variable name="VAR_LENGTH_SURCHARGE">
			<xsl:call-template name="LENGTH_26FEET_FACTOR"/>
		</xsl:variable>
		
		<!-- Different for respective boats -->
		<xsl:variable name ="PBOATSTYLECODE" select= "BOATS/BOAT/BOATSTYLECODE" />		
		<xsl:variable name ="COVERGE_RELATIVITY_VALUE">	
			<xsl:choose>
				<xsl:when test="BOATS/BOAT/BOATSTYLECODE =$BOATSTYLE_INBOARD">
						<xsl:call-template name="GET_COVERAGE">				 
							<xsl:with-param name="STYLE_FOR_RELATIVITY" select="'INBOARD'"/>
						</xsl:call-template>
						
				</xsl:when>
				<xsl:when test="BOATS/BOAT/BOATSTYLECODE =$BOATSTYLE_SAILBOAT">
						<xsl:call-template name="GET_COVERAGE">				 
							<xsl:with-param name="STYLE_FOR_RELATIVITY" select="'SAILBOAT'"/>
						</xsl:call-template>
				</xsl:when>
				<xsl:when test="BOATS/BOAT/BOATSTYLECODE =$BOATSTYLE_INBOARD_OUTBOARD">
						<xsl:call-template name="GET_COVERAGE">				 
							<xsl:with-param name="STYLE_FOR_RELATIVITY" select="'INOUTBOARD'"/>
						</xsl:call-template>
				</xsl:when>
				<xsl:otherwise>1.00</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		
		<xsl:variable name="VAR_INSURANCE_SCORE">
			<xsl:call-template name="INSURANCESCORE"/>
		</xsl:variable>
		
		
		<!-- Credits in listed order 
			1.Multi-Policy Credit with Home
			2.Experience Credit
			3.Diesel Engine Craft
			4.Deductible
			5.Installation of Halon Fire
			6. Loran Navigation System,
			7. Shore Station Credit
			8. Multi-Boat
			9. Age of Watercraft  
		-->
		
		<xsl:variable name="VAR_BOAT_HOME">
			<xsl:call-template name="DISCOUNT_BOAT_HOME_FACTOR"/>	
		</xsl:variable>			
		
		<xsl:variable name="VAR_EXPERIENCE_CREDIT">
			<xsl:call-template name="EXPERIENCE_CREDIT_FACTOR"/>	
		</xsl:variable>			
		
		<xsl:variable name="VAR_DIESEL_ENGINE">
			<xsl:call-template name="DIESEL_ENGINE_FACTOR"/>	
		</xsl:variable>			
		
		
		
		<xsl:variable name="VAR_DEDUCTIBLE">
			<xsl:call-template name="DEDUCTIBLE_FACTOR"/>	
		</xsl:variable>		
		
		<xsl:variable name="VAR_HALON_FIRE">
			<xsl:call-template name="HALON_FIRE_FACTOR"/>	
		</xsl:variable>		
		
		<xsl:variable name="VAR_LORAN_GPS_FACTOR">
			<xsl:call-template name="LORAN_GPS_FACTOR"/>	
		</xsl:variable>		
		
		<xsl:variable name="VAR_SHORE_STATION">
			<xsl:call-template name="SHORE_STATION_FACTOR"/>	
		</xsl:variable>		
		
		<xsl:variable name="VAR_MULTI_BOAT">
			<xsl:call-template name="MULTI_BOAT_FACTOR"/>	
		</xsl:variable>		
		
		<xsl:variable name="VAR_AGE_FACTOR">
			<xsl:call-template name="AGE_FACTOR"/>	
		</xsl:variable>		
		
		<!-- Surcharges in listed order 
			1. Wooden boats over 10 years old.
			2. Fiberglass boats over 15 years old.
			3. Removal of sailboat racing exclusion under 26 feet only.
			4. Dual Ownership
			5. Boats over 26 feet and speeds 55-65 miles per hour.
		
		-->
		
			  
		 <xsl:variable name="VAR_WOODEN_BOAT_FACTOR">
			 <xsl:call-template name="WOODEN_BOAT_FACTOR"/>
		</xsl:variable>		
		
		 <xsl:variable name="VAR_FIBRE_GLASS_FACTOR">
			 <xsl:call-template name="FIBRE_GLASS_FACTOR"/>
		</xsl:variable>	 
		
		<xsl:variable name="VAR_REMOVE_SAILBOAT_FACTOR">
			 <xsl:call-template name="REMOVE_SAILBOAT_FACTOR">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_PD"/>
			 </xsl:call-template>
		</xsl:variable>	
		
		<xsl:variable name="VAR_DUAL_OWNERSHIP">
			 <xsl:call-template name="DUAL_OWNERSHIP_FACTOR">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_PD"/>
			 </xsl:call-template>
		</xsl:variable>	
		
		
		
		<xsl:variable name="VAR_LENGTH_SPEED_SURCHARGE">
			<xsl:call-template name="BOAT_LENGTH_SPEED_SURCHARGE">
				<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_PD"/>
			</xsl:call-template>
		</xsl:variable>		
	
		<!-- Term Factor -->
		<xsl:variable name="VAR_TERMS">
			<xsl:call-template  name="POLICYTERM"/>
		</xsl:variable>
			<!--	
		<xsl:variable name="VAR_FIRSTCOMPONENT" select="round(round(round(round(round(round(round(round(round($VAR_BASE *$VAR_LENGTH_SURCHARGE) * $COVERGE_RELATIVITY_VALUE)* $VAR_INSURANCE_SCORE )*$VAR_BOAT_HOME )*$VAR_EXPERIENCE_CREDIT)*$VAR_DIESEL_ENGINE)*$VAR_DEDUCTIBLE)*$VAR_HALON_FIRE)*$VAR_LORAN_GPS_FACTOR)"/>
	    <xsl:value-of select="round(round(round(round(round(round(round(round(round($VAR_FIRSTCOMPONENT*$VAR_SHORE_STATION)*$VAR_MULTI_BOAT)*$VAR_AGE_FACTOR)*$VAR_WOODEN_BOAT_FACTOR)*$VAR_FIBRE_GLASS_FACTOR)*$VAR_REMOVE_SAILBOAT_FACTOR)*$VAR_DUAL_OWNERSHIP)*$VAR_LENGTH_SPEED_SURCHARGE)*$VAR_TERMS)"/>
	  -->
</xsl:template>

<!-- ============================================================================================ -->
	

	<!--Base Rates values of Inboard, Inboard/Outboard, Sailboat:  Rate.-->
	<xsl:template name ="BASE_RATES_INBOARD_IO_SAILBOAT_RATE">    
		 
		<xsl:variable name ="PBOATSTYLECODE" select= "BOATS/BOAT/BOATSTYLECODE" />
		<xsl:variable name ="WATERTYPE" select= "BOATS/BOAT/WATERSCODE"/>
		<xsl:variable name ="PTERRITORY" select= "BOATS/BOAT/TERRITORYDOCKEDIN"/>  
		
		<xsl:choose>
			<xsl:when test="$PBOATSTYLECODE = $BOATSTYLE_INBOARD">
			
				  <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERTYPE]/ATTRIBUTES[@TERRITORYCODE=$PTERRITORY]/@INBOARD"/> 
			
			
			
			
			</xsl:when>
			<xsl:when test="$PBOATSTYLECODE = $BOATSTYLE_SAILBOAT">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERTYPE]/ATTRIBUTES[@TERRITORYCODE=$PTERRITORY]/@SAILBOAT"/>
			</xsl:when>
			<xsl:when test="$PBOATSTYLECODE = $BOATSTYLE_INBOARD_OUTBOARD">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASERATES']/NODE[@ID=$WATERTYPE]/ATTRIBUTES[@TERRITORYCODE=$PTERRITORY]/@INOUTBOARD"/>
			</xsl:when>

			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	
<!-- ============================================================================================ -->
<!--				Templates for Inboard, Inboard/Outboard, Sailboat , Display (START)									  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
<!-- ============================================================================================ -->



<!-- ============================================================================================ -->
<!--				Templates for Inboard, Inboard/Outboard, Sailboat, Display (START)									  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
<!-- ============================================================================================ -->
<xsl:template name="TRAILOR_RATE_CALC">
	<!-- 
		1. Find Base Rate depending on deductible.		
		-->
		
		 
			<xsl:call-template name="BASE_RATES_TRAILERS"/> 
		 
		
		 
	
		
</xsl:template>

<!-- ============================================================================================ -->
	

	<!--Base Rates values of Trailers :  Rate.-->
	<xsl:template name ="BASE_RATES_TRAILERS">    
		 
		<xsl:variable name ="PDEDUCTIBLE" select= "BOATS/BOAT/DEDUCTIBLE"/>		 
		<xsl:variable name ="PMARKETVALUE"> 
			<xsl:call-template name="MARKETVALUE_IN_THOUSANDS">
				<xsl:with-param name="FACTOR_MARKETVAL" select="BOATS/BOAT/MARKETVALUE"/>
			</xsl:call-template>
		</xsl:variable>
		
		<!--Deductible  varies for jetski trailer,waverunner trailer,trailer	on the basis of year.
			It may  be percentage deductible in one yr and flat in the next. Trailer other than JetSki trailer
			will be treated as Waverunner Trailer -->
		<xsl:variable name="P_TRAILER_TYPE">
			<xsl:choose>
				<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_JETSKI_TRAILER or  BOATS/BOAT/BOATTYPECODE =$BOATTYPE_JETSKI_TRAILER_FROM_APP"><xsl:value-of select="'TRAILER_JETSKI'"/></xsl:when>
				<xsl:otherwise><xsl:value-of select="'TRAILER_WAVERUNNER'"/></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		
		
		<xsl:choose>
			<xsl:when test="contains($PDEDUCTIBLE ,'%-')"> <!--  implies it is a percentage deductible -->
					<xsl:variable name ="PRATE_PER_VALUE" select= "$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID=$P_TRAILER_TYPE]/@RATE_PER_VALUE"/>
					<xsl:variable name ="PRATE_PER" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID=$P_TRAILER_TYPE]/ATTRIBUTES[@DESC = $PDEDUCTIBLE]/@FACTOR"/>
					

					<xsl:if test="normalize-space($PRATE_PER_VALUE) = ''">
					<tr>
						<td class="midcolora"><li> Rate per value for trailer (Per. ded.) not found  (Path - "FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID=$P_TRAILER_TYPE]/@RATE_PER_VALUE") </li> </td>
					</tr>
						<xsl:if test="user:AllVerified(0)=1"></xsl:if>
					</xsl:if>

					<xsl:if test="normalize-space($PRATE_PER) = ''">
					<tr>
						<td class="midcolora"><li> Rate per value for trailer not found  (Path - "FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID=$P_TRAILER_TYPE]/ATTRIBUTES[@DESC = $PDEDUCTIBLE]/@FACTOR") </li> </td>
					</tr>
						<xsl:if test="user:AllVerified(0)=1"></xsl:if>
					</xsl:if>
					<!-- <xsl:value-of select="round($PRATE_PER * $PMARKETVALUE div $PRATE_PER_VALUE)"/>  -->
				 	 
			</xsl:when> 	
			<xsl:otherwise>
					<xsl:variable name ="PRATE_PER_VALUE" select= "$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID=$P_TRAILER_TYPE]/@RATE_PER_VALUE"/>	
					<xsl:variable name ="PRATE_PER" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID=$P_TRAILER_TYPE]/ATTRIBUTES[@DEDUCTIBLE = $PDEDUCTIBLE]/@FACTOR"/>
										
			
					<xsl:if test="normalize-space($PRATE_PER_VALUE) = ''">
					<tr>
						<td class="midcolora"><li> Rate per value for trailer not found  (Path - "FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID=$P_TRAILER_TYPE]/@RATE_PER_VALUE") </li> </td>
					</tr>
						<xsl:if test="user:AllVerified(0)=1"></xsl:if>
					</xsl:if>

					<xsl:if test="normalize-space($PRATE_PER) = ''">
					<tr>
						<td class="midcolora"><li> Rate per value for trailer not found  (Path - "FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID=$P_TRAILER_TYPE]/ATTRIBUTES[@DEDUCTIBLE = $PDEDUCTIBLE]/@FACTOR") </li> </td>
					</tr>
						<xsl:if test="user:AllVerified(0)=1"></xsl:if>
					</xsl:if>
					<!-- <xsl:value-of select="round($PRATE_PER * $PMARKETVALUE div $PRATE_PER_VALUE)"/>  -->
			</xsl:otherwise>
		</xsl:choose>	 
		
		
	</xsl:template>
	
	
<!-- ============================================================================================ -->
<!--				Templates for Trailer, Display (END)									  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN									              -->
<!-- ============================================================================================ -->


<!-- Template for Coverage -->
<xsl:template name="GET_COVERAGE">
<xsl:param name="STYLE_FOR_RELATIVITY"/>

		<!-- Get the market/coverage value of the boat -->
		<xsl:variable name ="CVALUE">
			<!-- <xsl:value-of select="BOATS/BOAT/MARKETVALUE"/>  -->
			<xsl:call-template name="MARKETVALUE_IN_THOUSANDS">
			<xsl:with-param name="FACTOR_MARKETVAL" select="BOATS/BOAT/MARKETVALUE"/>
				</xsl:call-template>
	 	</xsl:variable>
				
		<!-- get the max value of coverages in the database -->
		<xsl:variable name="P_MAX_VALUE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RATERELATIVITY']/NODE[@ID=$STYLE_FOR_RELATIVITY]/@MAXVALUE"/>	
		</xsl:variable>
		
		<xsl:if test="normalize-space($P_MAX_VALUE) = ''">
					<tr>
						<td class="midcolora"><li> max value of coverages not found  (Path - "FACTOR[@ID='RATERELATIVITY']/NODE[@ID=$STYLE_FOR_RELATIVITY]/@MAXVALUE") </li> </td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
			
		
		<xsl:choose>
				<!-- If the coverage selected is less than the max value then pick straight -->
				<xsl:when test="$CVALUE &lt;= $P_MAX_VALUE">
				
				<xsl:variable name="VAR_RELATIVITY"> 
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RATERELATIVITY']/NODE[@ID=$STYLE_FOR_RELATIVITY]/ATTRIBUTES[@COVERAGE = $CVALUE]/@RELATIVITY"/>	
				</xsl:variable>		
				<xsl:if test="normalize-space($VAR_RELATIVITY) = ''">
					<tr>
						<td class="midcolora"><li> Coverage Relativity not found  (Path - "FACTOR[@ID='RATERELATIVITY']/NODE[@ID=$STYLE_FOR_RELATIVITY]/ATTRIBUTES[@COVERAGE = $CVALUE]/@RELATIVITY") </li> </td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
	
	
						
				</xsl:when>
				<!-- otherwise use the additional concept -->
				<xsl:otherwise>
					<!-- For each Additional amount-->
					<xsl:variable name="P_ADDITIONAL_VALUE">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RATERELATIVITY']/NODE[@ID=$STYLE_FOR_RELATIVITY]/ATTRIBUTES[@COVERAGE = 'ADDITIONAL']/@AMOUNT"/>	
					</xsl:variable>
					
					<xsl:if test="normalize-space($P_ADDITIONAL_VALUE) = ''">
					<tr>
						<td class="midcolora"><li> Additional value for coverage not found  (Path - "FACTOR[@ID='RATERELATIVITY']/NODE[@ID=$STYLE_FOR_RELATIVITY]/ATTRIBUTES[@COVERAGE = 'ADDITIONAL']/@AMOUNT") </li> </td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				
					
					<!-- value -->
					<xsl:variable name="P_ADDITIONAL_FACTOR">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RATERELATIVITY']/NODE[@ID=$STYLE_FOR_RELATIVITY]/ATTRIBUTES[@COVERAGE = 'ADDITIONAL']/@RELATIVITY"/>	
					</xsl:variable>
					<xsl:if test="normalize-space($P_ADDITIONAL_FACTOR) = ''">
					<tr>
						<td class="midcolora"><li> Additional factor for coverage not found  (Path - "FACTOR[@ID='RATERELATIVITY']/NODE[@ID=$STYLE_FOR_RELATIVITY]/ATTRIBUTES[@COVERAGE = 'ADDITIONAL']/@RELATIVITY") </li> </td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				
					
					
					<!-- value for the max factor -->
					<xsl:variable name="P_MAX_VALUE_FACTOR">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RATERELATIVITY']/NODE[@ID=$STYLE_FOR_RELATIVITY]/ATTRIBUTES[@COVERAGE = $P_MAX_VALUE]/@RELATIVITY"/>	
					</xsl:variable>
					
					<xsl:if test="normalize-space($P_MAX_VALUE_FACTOR) = ''">
					<tr>
						<td class="midcolora"><li> Max Value for for coverage not found  (Path - "FACTOR[@ID='RATERELATIVITY']/NODE[@ID=$STYLE_FOR_RELATIVITY]/ATTRIBUTES[@COVERAGE = $P_MAX_VALUE]/@RELATIVITY") </li> </td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
					</xsl:if>
					
					
					<!-- Calculations 
					e.g : factor for 250 + ((difference in the cov and the max value)/additional amount)* (factor for additional)-->
					<xsl:value-of select="($P_MAX_VALUE_FACTOR + ( (($CVALUE - $P_MAX_VALUE) div $P_ADDITIONAL_VALUE) * $P_ADDITIONAL_FACTOR ))"/>
					
				</xsl:otherwise>
			</xsl:choose>

</xsl:template>
	
<!-- ********************************************************************************************************************** -->
<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ Start of Insurance Score  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
<!-- ********************************************************************************************************************** -->
<xsl:template name ="INSURANCESCORE">
	<xsl:variable name="INS1" select="POLICY/INSURANCESCORE"></xsl:variable>
	<xsl:variable name="INS"  select="translate(translate($INS1,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/>
	
	<!-- FOR MAXIMUM SCORE -->
	<xsl:variable name="VAR_MAX_SCORE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MAX = 'MAX_SCORE']/@MINSCORE"/>
	
	<xsl:choose> 
		<xsl:when test = "$INS = 0 or $INS = ''">
			1.00  
		</xsl:when>
		<xsl:when test = "$INS = 'NOHITNOSCORE'">  <!-- NO HIT NO SCORE -->
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MINSCORE = 'N']/@FACTOR"/>
		</xsl:when>
		<xsl:when test = "$INS &gt;= $VAR_MAX_SCORE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MINSCORE = $VAR_MAX_SCORE]/@FACTOR"/>
		</xsl:when>		
		<xsl:otherwise>						
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@FACTOR"/>
		</xsl:otherwise>
	</xsl:choose>
 
</xsl:template>

<xsl:template name ="INSURANCESCORE_DISPLAY">
	<xsl:variable name="ISCORE" select="POLICY/INSURANCESCORE"></xsl:variable>
 	<xsl:variable name="INS" select="translate(translate($ISCORE,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/>
  
	
	
	
	
	<!-- FOR MAXIMUM SCORE -->
	<xsl:variable name="VAR_MAX_SCORE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MAX = 'MAX_SCORE']/@MINSCORE"/>
	
	
	<xsl:variable name="INSDISCOUNT">
		<xsl:choose>
			<xsl:when test = "$INS = 0">
				<xsl:value-of select= "$INS"></xsl:value-of>
			</xsl:when>
			<xsl:when test = "$INS = ''">
				1.00
			</xsl:when>
			<xsl:when test = "$INS = 'NOHITNOSCORE'">  <!-- NO HIT NO SCORE -->
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MINSCORE = 'N']/@FACTOR"/>
			</xsl:when>
			<xsl:when test = "$INS &gt;= $VAR_MAX_SCORE">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MINSCORE = $VAR_MAX_SCORE]/@FACTOR"/>
			</xsl:when>			
			<xsl:otherwise>						
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@FACTOR"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:variable>
	<xsl:choose>
		<!-- Triailer style - Not reqd for trailer type-->
		<xsl:when test="BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_TRAILER">		
		0
		</xsl:when>
		<xsl:when test = "$INSDISCOUNT = 0 or $INSDISCOUNT='' or $INSDISCOUNT=1">
			0
		</xsl:when>	
		<xsl:when test = "$INSDISCOUNT &gt; 0 and $INSDISCOUNT &lt;= 1">
			Included
		</xsl:when>		 
		<xsl:otherwise>	
			0		 
		</xsl:otherwise>
	</xsl:choose>
	</xsl:template>
	
	
	<xsl:template name ="INSURANCESCORE_PERCENT">
		<xsl:variable name ="VAR_SCORE"> <xsl:call-template name ="INSURANCESCORE" /> </xsl:variable>
	
		<xsl:variable name="VAR_SCORE_PERCENT"> 
			<xsl:choose> 
 				<xsl:when test ="$VAR_SCORE != '' and $VAR_SCORE &lt; 1.00">
					<xsl:value-of select="round((1 - $VAR_SCORE) * 100)"/>%
				</xsl:when>
				
				<xsl:when test ="$VAR_SCORE != '' and $VAR_SCORE &gt; 1.00">
					<xsl:value-of select="round(($VAR_SCORE -1 ) * 100)"/>%
				</xsl:when>
								
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
 
		<xsl:value-of select="$VAR_SCORE_PERCENT"/>
	</xsl:template>


<!-- ********************************************************************************************************************** -->
<!-- ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ End of Insurance Score ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ -->
<!-- ********************************************************************************************************************** -->




<!-- ============================================================================================ -->
<!--					Templates for Agreed Value Endorsement (AV100), Display (START)			  -->
<!--		    					  FOR INDIANA	,MICHIGAN,WISCONSIN										              -->
<!-- 
	1. The maximum Agreed Value will not exceed $75,000. The maximum length of the watercraft 
	 will not exceed 26'.
	2. The Agreed Value will not exceed the high retail value as established in the BUC book.
	 If the value is higher, a current marine survey (last 12 months) or other substantiating evidence 
	 is required prior to binding, such as original purchase agreement if within last 12 months.
	3. The Company will accept a copy of the original purchase agreement or receipt as the Agreed Value 
		if the original	purchase was within 12 months of the date of the application.
	4. The maximum age of the boat will not exceed 20 years at the time the policy is written.
	5. The hull values and outboard motor values will be reviewed and adjusted at company discretion 
		on a periodic basis.Any adjustments made by the company will be provided to the agent and insured.
	6. Marine surveys and/or photos may be required at any time at the discretion of the company.  
	7. MINIMUM =10   -->
<!-- ============================================================================================ -->

<xsl:template name="AGREED_VALUE_ENDORSEMENT">
	<!-- <xsl:variable name="P_AGREED_VALUE" select="BOATS/BOAT/MARKETVALUE"/> -->
	<xsl:variable name="P_AGREED_VALUE"> 
		<xsl:call-template name="MARKETVALUE_IN_THOUSANDS">
			<xsl:with-param name="FACTOR_MARKETVAL" select="BOATS/BOAT/MARKETVALUE"/>
		</xsl:call-template>
	</xsl:variable>
	<xsl:variable name="P_MAX_BOAT_LENGTH" select="BOATS/BOAT/LENGTH"/>
	<xsl:variable name="P_MAX_AGREED_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='AGREED_VALUE_ENDORSEMENT']/NODE[@ID ='AGREED_VALUE']/ATTRIBUTES/@MAX_VALUE"/>
	<xsl:variable name="P_MIN_AGREED_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='AGREED_VALUE_ENDORSEMENT']/NODE[@ID ='AGREED_VALUE']/ATTRIBUTES/@MINIMUM_ENDORSEMENT"/>


			<xsl:if test="normalize-space($P_MAX_AGREED_VALUE) = ''">
				<tr>
					<td class="midcolora"><li> Max value for agreed value not found (Path - "FACTOR[@ID='AGREED_VALUE_ENDORSEMENT']/NODE[@ID ='AGREED_VALUE']/ATTRIBUTES/@MAX_VALUE") </li> </td>
				</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				

				<xsl:if test="normalize-space($P_MIN_AGREED_VALUE) = ''">
				<tr>
					<td class="midcolora"><li> Min Endorsement for agreed value not found (Path - "FACTOR[@ID='AGREED_VALUE_ENDORSEMENT']/NODE[@ID ='AGREED_VALUE']/ATTRIBUTES/@MINIMUM_ENDORSEMENT") </li> </td>
				</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
			 
	 <xsl:variable name="VAR_AGREED_VALUE_END"> 
           <xsl:choose>
				<!-- <xsl:when test="(BOATS/BOAT/AV100 = 'Y') and (BOATS/BOAT/LENGTH &lt;= $P_MAX_BOAT_LENGTH) and (BOATS/BOAT/MARKETVALUE &lt;= $P_MAX_AGREED_VALUE) "> -->
				<xsl:when test="(BOATS/BOAT/AV100 = 'Y') and (BOATS/BOAT/LENGTH &lt;= $P_MAX_BOAT_LENGTH) and ($P_AGREED_VALUE &lt;= $P_MAX_AGREED_VALUE) "> 
						<xsl:variable name="P_RATE_OF_ENDORSEMENT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='AGREED_VALUE_ENDORSEMENT']/NODE[@ID ='AGREED_VALUE']/ATTRIBUTES/@CHARGE"/>							
						<xsl:variable name="P_RATE_PER_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='AGREED_VALUE_ENDORSEMENT']/NODE[@ID ='AGREED_VALUE']/ATTRIBUTES/@RATE_PER_VALUE"/>							
						<xsl:variable name="P_AGREED_VALUE_ENDORSEMENT"><xsl:value-of select="round(($P_AGREED_VALUE div $P_RATE_PER_VALUE)* $P_RATE_OF_ENDORSEMENT)"/></xsl:variable> 
						<xsl:choose>
							<xsl:when test="$P_AGREED_VALUE_ENDORSEMENT &lt; $P_MIN_AGREED_VALUE"><xsl:value-of select="$P_MIN_AGREED_VALUE"/></xsl:when>
							<xsl:otherwise><xsl:value-of select="$P_AGREED_VALUE_ENDORSEMENT"/></xsl:otherwise>
						</xsl:choose>
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
	</xsl:variable>		
	 
</xsl:template>
 
 
<!-- ============================================================================================ -->
<!--					Templates for Agreed Value Endorsement (AV100), Display (END)			  -->
<!--		    					  FOR INDIANA,MICHGAN,WISCONSIN					              -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--				Templates for Client Entertainment (OP720), Display (START)					  -->
<!--		    					  FOR INDIANA	, MICHIGAN, WISCONSIN			              -->
<!-- ============================================================================================ -->
<xsl:template name="CLIENT_ENTERTAINMENT">

<xsl:variable name="VAR_CLIENT_ENTERTAINMENT">

	<xsl:choose>		     
		<xsl:when test ="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
			<xsl:choose>
				<xsl:when test="BOATS/BOAT/OP720 = 'Y'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='CLTENTERTAINMENT']/ATTRIBUTES/@RATE"/>			
				</xsl:when>
				<xsl:otherwise>
					0
				</xsl:otherwise>
			</xsl:choose>
		</xsl:when>			
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:variable>

		<xsl:if test="normalize-space($VAR_CLIENT_ENTERTAINMENT) = ''">
				<tr>
					 <td class="midcolora"><li> Client entertainment factor not found  (Path - "FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='CLTENTERTAINMENT']/ATTRIBUTES/@RATE") </li> </td>
				
				 
	
				</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
		<!--		
	  <xsl:value-of select="$VAR_CLIENT_ENTERTAINMENT"/> 
	 -->
	
	

	
	
</xsl:template>
<!-- ============================================================================================ -->
<!--				Templates for Client Entertainment (OP720), Display (END)					  -->
<!--		    					  FOR INDIANA	, MICHIGAN, WISCONSIN			              -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--			Templates for Watercraft Liability Poolution Coverage (OP900), Display (START)	  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->

<xsl:template name="LIABILITY_POLLUTION_COVERAGE">
<xsl:variable name="VAR_LIABILITY_POLLUTION_COVERAGE"> 
		<xsl:choose>           
		    <xsl:when test ="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:choose>
					<xsl:when test="BOATS/BOAT/OP900 = 'Y'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='POLLUTIONCOV']/ATTRIBUTES/@RATE"/>			
					</xsl:when>
					<xsl:otherwise>
						0
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>			
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:variable>	
	
	<xsl:if test="normalize-space($VAR_LIABILITY_POLLUTION_COVERAGE) = ''">
		<tr>
			<td class="midcolora"><li> Factor for Liability Pollution Coverage  not found  (Path - "FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='POLLUTIONCOV']/ATTRIBUTES/@RATE") </li> </td>
		</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
	<!--			
	<xsl:value-of select="$VAR_LIABILITY_POLLUTION_COVERAGE"/>
-->



	
</xsl:template>
<!-- ============================================================================================ -->
<!--				Templates for Watercraft Liability Polution Coverage (OP900), Display (END)  -->
<!--		    					  FOR INDIANA									              -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					Templates for length over 26 Feet, Display (START)						  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN		              -->
<!-- ============================================================================================ -->
	<xsl:template name="LENGTH_26FEET_DISPLAY">
		<xsl:variable name="P_MAX_LENGTH" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDBOATLENGTH']/ATTRIBUTES/@BOAT_LENGTH_OVER"/>

		<xsl:choose>
			<xsl:when test="BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER ">
				0
			</xsl:when>
			<xsl:when test = "(BOATS/BOAT/LENGTH &gt; $P_MAX_LENGTH and (BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD  or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD_OUTBOARD or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_SAILBOAT))">Charge - Length over <xsl:value-of select="$P_MAX_LENGTH"/> Feet +<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDBOATLENGTH']/ATTRIBUTES/@SURCHARGE"/>%</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="LENGTH_26FEET_PREMIUM">
		<xsl:variable name="P_MAX_LENGTH" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDBOATLENGTH']/ATTRIBUTES/@BOAT_LENGTH_OVER"/>

			<xsl:choose>	
				<xsl:when test ="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
					<xsl:choose>
						<xsl:when test = "(BOATS/BOAT/LENGTH &gt; $P_MAX_LENGTH and (BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD  or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD_OUTBOARD or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_SAILBOAT))">Included</xsl:when>
						<xsl:otherwise>0</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template name="LENGTH_26FEET_FACTOR">
		<xsl:variable name="P_MAX_LENGTH" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDBOATLENGTH']/ATTRIBUTES/@BOAT_LENGTH_OVER"/>
			<xsl:choose>			 
				<xsl:when test ="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
					<xsl:choose>
						<xsl:when test = "(BOATS/BOAT/LENGTH &gt; $P_MAX_LENGTH and (BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD  or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD_OUTBOARD or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_SAILBOAT))">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDBOATLENGTH']/ATTRIBUTES/@FACTOR"/>
						</xsl:when>
						<xsl:otherwise>1.00</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

<!-- ============================================================================================ -->
<!--					Templates for length over 26 Feet, Display (END)						  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN			              -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--					Templates for Policy Term Display (START)-->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
<!--																							  -->
<!-- ============================================================================================ -->

<xsl:template name="POLICYTERM">
	<xsl:variable name="P_POLICYTERMS" select="POLICY/POLICYTERMS"/>
	<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERM_FACTOR']/NODE[@ID ='TERMFACTOR']/ATTRIBUTES[@MONTHS = $P_POLICYTERMS]/@FACTOR"/>							
</xsl:template>
 

<!-- ============================================================================================ -->
<!--					Templates for Policy Term, Display (END)			  -->
<!--		    					  FOR INDIANA,MICHGAN,WISCONSIN					              -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--				Templates for Boat Towinge, Display (START)									  -->
<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN				          -->
<!-- ============================================================================================ -->
<xsl:template name="BOAT_TOWING_PREMIUM">
	<!-- Fetch style -->
	<xsl:variable name="P_BOAT_STYLE">			
		<xsl:value-of select="BOATS/BOAT/BOATSTYLECODE"/>
	</xsl:variable>
	
	<xsl:choose> 
		<xsl:when test ="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
			<xsl:choose>
				<!-- Will not be included for Trailer types  -->
 				<xsl:when test = "$P_BOAT_STYLE = $BOATSTYLE_TRAILER">
 					0
 				</xsl:when>
 				<xsl:otherwise>
 		   			Included
 				</xsl:otherwise>
 			</xsl:choose>
		</xsl:when>			
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<xsl:template name="BOAT_TOWING_LIMIT">
	<!-- Fetch style -->
	<xsl:variable name="P_BOAT_STYLE">			
		<xsl:value-of select="BOATS/BOAT/BOATSTYLECODE"/>
	</xsl:variable>
 	<xsl:variable name="P_MARKET_VALUE" select="BOATS/BOAT/MARKETVALUE"/>  
	
	<xsl:choose> 
		<xsl:when test ="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
			<xsl:choose>
				<!-- Will not be included for Trailer types  -->
 				<xsl:when test = "($P_BOAT_STYLE != $BOATSTYLE_TRAILER) and ($P_MARKET_VALUE !='') and ($P_MARKET_VALUE &gt; 0) ">
 					 <xsl:variable name="P_BOAT_TOWING_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BOAT_TOWING_FACTOR']/NODE[@ID ='BOAT_TOWING']/ATTRIBUTES/@FACTOR"/>							
 					 <xsl:value-of select="round($P_MARKET_VALUE * $P_BOAT_TOWING_FACTOR)"/>
 				</xsl:when>
 				<xsl:otherwise>
 		   			 0
 				</xsl:otherwise>
 			</xsl:choose>
		</xsl:when>			
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--				END Templates for Boat Towinge, Display (START)								  -->
<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN				          -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--				Templates for WATERCRAFT LIABILITY, Display (START)							  -->
<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN								              -->
<!-- ============================================================================================ -->
<xsl:template name="WATERCRAFT_LIABILITY_TEXT">
	<xsl:variable name="P_MAX_SAILBOAT_LENGTH_FOR_HOME" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SAILBOAT_LENGTH_FOR_HOME"/>
	<!-- Fetch style -->
	<xsl:variable name="P_BOAT_STYLE">			
		<xsl:value-of select="BOATS/BOAT/BOATSTYLECODE"/>
	</xsl:variable>
	
	<xsl:choose>
		<!-- Will not be included for Trailer types  -->
 		<xsl:when test = "$P_BOAT_STYLE = $BOATSTYLE_TRAILER">
 			0
 		</xsl:when>
		<xsl:when test="
		((POLICY/ATTACHTOWOLVERINE = 'Y' or POLICY/BOATHOMEDISC = 'Y')
		 and (BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_OUTBOARD))
		  or
		((POLICY/ATTACHTOWOLVERINE = 'Y' or POLICY/BOATHOMEDISC = 'Y')
		 and
		 (BOATS/BOAT/LENGTH &lt; $P_MAX_SAILBOAT_LENGTH_FOR_HOME and BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_SAILBOAT))">Watercraft Liability - extended from Homeowners</xsl:when>
		<xsl:otherwise>Watercraft Liability</xsl:otherwise>		
	</xsl:choose>
</xsl:template> 

<xsl:template name = "WATERCRAFT_LIABILITY">
	
	<xsl:variable name="LIABILITYAMOUNT" select="BOATS/BOAT/PERSONALLIABILITY"/>
	<xsl:variable name="LENGTH" select="BOATS/BOAT/LENGTH"/>
	<xsl:variable name="LIABILITY_AMOUNT"  select="translate(translate($LIABILITYAMOUNT,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/>
	<xsl:variable name="P_MAX_SAILBOAT_LENGTH_FOR_HOME" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SAILBOAT_LENGTH_FOR_HOME"/>
	<xsl:variable name="P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SAILBOAT_LENGTH_FOR_LIABILITY"/>
	
	<!-- Common variables/factors to be used below -->
	
	<!-- Liability Surcharge for Length and Speed -->
	<xsl:variable name="VAR_LIABILITY_SURCHARGES">
		<xsl:call-template name="BOAT_LENGTH_SPEED_SURCHARGE">
			<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_LIABILITY"/>
		</xsl:call-template>
	</xsl:variable>		
	
	<!-- Dual Ownership -->
	<xsl:variable name="VAR_DUAL_OWNERSHIP">
		<xsl:call-template name="DUAL_OWNERSHIP_FACTOR">
			<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_LIABILITY"/>
		</xsl:call-template>
	</xsl:variable>
	
	<!-- Remove Sailboat -->
	<xsl:variable name="VAR_REMOVE_SAILBOAT_FACTOR">
		<xsl:call-template name="REMOVE_SAILBOAT_FACTOR">
			<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_LIABILITY"/>
		</xsl:call-template>
	</xsl:variable>
	
	
	<xsl:choose>
		<xsl:when test="$LIABILITY_AMOUNT ='NOCOVERAGE' or $LIABILITY_AMOUNT =''">0</xsl:when>
		<xsl:when test="$LIABILITY_AMOUNT &lt;= 0">0</xsl:when>
		
		<!-- Waverunner Type -->		 
		<xsl:when test="BOATS/BOAT/BOATTYPECODE =$BOATTYPE_WAVERUNNER">
			<xsl:variable name="VAR_LIABILITY_AMOUNT">			
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='SECTIONIILIABILITY']/ATTRIBUTES[@LIABILITY=$LIABILITY_AMOUNT]/@WAVERUNNERRATE"/>
			</xsl:variable>
				
			<!-- <xsl:value-of select="$VAR_LIABILITY_AMOUNT * $VAR_LIABILITY_SURCHARGES"/>  -->
			<xsl:if test="normalize-space($VAR_LIABILITY_AMOUNT) = ''">
				<tr>
					<td class="midcolora"><li> Liablity Amount for waverunner not found  (Path - "FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='SECTIONIILIABILITY']/ATTRIBUTES[@LIABILITY=$LIABILITY_AMOUNT]/@WAVERUNNERRATE") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>

		</xsl:when>	
		
		<!-- Mini Jet Boat -->
		<xsl:when test="BOATS/BOAT/BOATTYPECODE =$BOATTYPE_MINI_JET">
			<xsl:variable name="VAR_LIABILITY_AMOUNT">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID='MINIJETBOARD']/ATTRIBUTES[@LIABILITY=$LIABILITY_AMOUNT]/@RATE"/>
			</xsl:variable>
			
			<xsl:if test="normalize-space($VAR_LIABILITY_AMOUNT) = ''">
				<tr>
					<td class="midcolora"><li> Liablity Amount for Mini jet not found  (Path - "FACTOR[@ID='LIABILITYRATES']/NODE[@ID='MINIJETBOARD']/ATTRIBUTES[@LIABILITY=$LIABILITY_AMOUNT]/@RATE") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			<!-- <xsl:value-of select="round($VAR_LIABILITY_AMOUNT * $VAR_DUAL_OWNERSHIP)"></xsl:value-of> -->
			
		</xsl:when>		
		
		
		<!-- Outboard style/Sailboat less than 26 feet and Attached to Home -->
		<xsl:when test="((POLICY/ATTACHTOWOLVERINE = 'Y' or POLICY/BOATHOMEDISC = 'Y')
						and ((BOATS/BOAT/BOATTYPECODE != $BOATTYPE_WAVERUNNER and BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_OUTBOARD) 
							  or (BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_SAILBOAT and BOATS/BOAT/LENGTH &lt;= $P_MAX_SAILBOAT_LENGTH_FOR_HOME)))">
			 
				0
			
		</xsl:when>
			
		<!-- Jet Ski Style-->
		<xsl:when test="normalize-space(BOATS/BOAT/BOATSTYLECODE) = normalize-space($BOATSTYLE_JETSKI)">			
			<xsl:variable name="VAR_LIABILITY_AMOUNT">			
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='SECTIONIILIABILITY']/ATTRIBUTES[@LIABILITY=$LIABILITY_AMOUNT]/@JETSKIRATE"/>
			</xsl:variable>		
			
			<xsl:if test="normalize-space($VAR_LIABILITY_AMOUNT) = ''">
				<tr>
					<td class="midcolora"><li> Liablity Amount for Jet Ski not found  (Path - "FACTOR[@ID='PERSONALWATATTACHMENT']/NODE[@ID='SECTIONIILIABILITY']/ATTRIBUTES[@LIABILITY=$LIABILITY_AMOUNT]/@JETSKIRATE") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
				
			<!-- <xsl:value-of select="$VAR_LIABILITY_AMOUNT * $VAR_LIABILITY_SURCHARGES"/>  -->
		</xsl:when>
		
		<!-- Triailer style -->
		<xsl:when test="BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_TRAILER">		
		0
		</xsl:when>
		 
		 
		<!-- Outboard style. Speed not available for Outboard -->
		<xsl:when test="BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_OUTBOARD">		
			<xsl:variable name="VAR_LIABILITY">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID='LIOUTBOARD']/ATTRIBUTES[@LIABILITY=$LIABILITY_AMOUNT]/@RATE" />
			</xsl:variable>
		
			<xsl:if test="normalize-space($VAR_LIABILITY) = ''">
				<tr>
					<td class="midcolora"><li> Liablity Rate for outboard not found  (Path - "FACTOR[@ID='LIABILITYRATES']/NODE[@ID='LIOUTBOARD']/ATTRIBUTES[@LIABILITY=$LIABILITY_AMOUNT]/@RATE") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
		<!-- 	<xsl:value-of select="round(round($VAR_LIABILITY * $VAR_DUAL_OWNERSHIP) * $VAR_REMOVE_SAILBOAT_FACTOR)" /> -->
		</xsl:when>
		 	
		<!-- Sailboat style -->
		<xsl:when test="(normalize-space(BOATS/BOAT/BOATSTYLECODE) = normalize-space($BOATSTYLE_SAILBOAT)) and (BOATS/BOAT/LENGTH &lt; $P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY)">
				<xsl:variable name="VAR_LIABILITY">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID='LIOUTBOARD']/ATTRIBUTES[@LIABILITY=$LIABILITY_AMOUNT]/@RATE"/>
				</xsl:variable>	
				
				<xsl:if test="normalize-space($VAR_LIABILITY) = ''">
				<tr>
					<td class="midcolora"><li> Liablity Rate for Sailboat not found  (Path - "FACTOR[@ID='LIABILITYRATES']/NODE[@ID='LIOUTBOARD']/ATTRIBUTES[@LIABILITY=$LIABILITY_AMOUNT]/@RATE") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
							
				<!-- <xsl:value-of select="round($VAR_LIABILITY*$VAR_DUAL_OWNERSHIP*$VAR_LIABILITY_SURCHARGES*$VAR_REMOVE_SAILBOAT_FACTOR)"/>  -->
		</xsl:when>		
		 
		 <!-- Other boat types -->
		<xsl:otherwise>							 
			<xsl:variable name="VAR_LIABILITY">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID='ALLOUTBOARD']/ATTRIBUTES[@LIABILITY=$LIABILITY_AMOUNT]/@RATE"/>
			</xsl:variable>
			
			<xsl:if test="normalize-space($VAR_LIABILITY) = ''">
				<tr>
					<td class="midcolora"><li> Liablity Rate for Other boat not found  (Path - "FACTOR[@ID='LIABILITYRATES']/NODE[@ID='ALLOUTBOARD']/ATTRIBUTES[@LIABILITY=$LIABILITY_AMOUNT]/@RATE") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			<!-- <xsl:value-of select="round(round($VAR_LIABILITY * $VAR_DUAL_OWNERSHIP) * $VAR_LIABILITY_SURCHARGES)"/> -->
		</xsl:otherwise>	
		
	</xsl:choose>	
</xsl:template>



<xsl:template name="WATERCRAFT_LIABILITY_DISPLAY_EXTENDED">
			<xsl:variable name="P_MAX_SAILBOAT_LENGTH_FOR_HOME" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SAILBOAT_LENGTH_FOR_HOME"/>
			<xsl:if test="normalize-space($P_MAX_SAILBOAT_LENGTH_FOR_HOME) = ''">
				<tr>
					<td class="midcolora"><li> maximum sailboat length for home  not found  (Path - "COMPONENT_LIMITS/LIMITS_VALUE/ATTRIBUTES/MAX_SAILBOAT_LENGTH_FOR_HOME")</li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
	
</xsl:template>
<!-- ============================================================================================ -->
<!--				Templates for WATERCRAFT LIABILITY, Display (END)							  -->
<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN								              -->
<!-- ============================================================================================ -->



<!-- ============================================================================================ -->
<!--				Templates for MEDICAL PAYMENTS, Display (START)								 -->
<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN									              -->
<!-- ============================================================================================ -->
<xsl:template name="MEDICAL_PAYMENTS_DISPLAY">	

	<xsl:variable name="P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SAILBOAT_LENGTH_FOR_LIABILITY"/>
	
	  <xsl:if test="normalize-space($P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY) = ''">
		<tr>
			<td class="midcolora"><li> max sailboat length for liability additional not found  (Path - "COMPONENT_LIMITS/LIMITS_VALUE/ATTRIBUTES/MAX_SAILBOAT_LENGTH_FOR_LIABILITY")</li> </td>
		</tr>
		   <xsl:if test="user:AllVerified(0)=1"></xsl:if>
	 </xsl:if>
			
			
</xsl:template>

<xsl:template name="MEDICAL_PAYMENTS_TEXT">
	<xsl:variable name="P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SAILBOAT_LENGTH_FOR_LIABILITY"/>

	<xsl:choose>
		<!-- Not for trailer -->
		<xsl:when test="BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER">
			0
		</xsl:when>
		<xsl:when test="
		((POLICY/ATTACHTOWOLVERINE = 'Y' or POLICY/BOATHOMEDISC = 'Y')
		 and (BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_OUTBOARD))
		  or
		((POLICY/ATTACHTOWOLVERINE = 'Y' or POLICY/BOATHOMEDISC = 'Y')
		 and
		 (BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_SAILBOAT and BOATS/BOAT/LENGTH &lt;= $P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY))">
		 Medical Payments To Others - extended from Homeowners
		 </xsl:when>
		<xsl:otherwise>Medical Payments To Others</xsl:otherwise>		
	</xsl:choose>
</xsl:template> 

<xsl:template name="MEDICAL_PAYMENTS">
	<xsl:variable name="P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SAILBOAT_LENGTH_FOR_LIABILITY"/>
	<xsl:variable name="P_MEDICAL_PAYMENT" select="BOATS/BOAT/MEDICALPAYMENT"/>
	<xsl:variable name="P_MEDICAL_PAYMENT_AMOUNT"  select="translate(translate($P_MEDICAL_PAYMENT,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/>
	
	<!-- Liability Surcharge for Length and Speed -->
	<xsl:variable name="VAR_LIABILITY_SURCHARGES">
		<xsl:call-template name="BOAT_LENGTH_SPEED_SURCHARGE">
			<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_LIABILITY"/>
		</xsl:call-template>
	</xsl:variable>	
	
	<!-- Dual Ownership -->
	<xsl:variable name="VAR_DUAL_OWNERSHIP">
		<xsl:call-template name="DUAL_OWNERSHIP_FACTOR">
			<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_LIABILITY"/>
		</xsl:call-template>
	</xsl:variable>
	
	<!-- Remove Sailboat -->
	<xsl:variable name="VAR_REMOVE_SAILBOAT_FACTOR">
		<xsl:call-template name="REMOVE_SAILBOAT_FACTOR">
			<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_LIABILITY"/>
		</xsl:call-template>
	</xsl:variable>
		
	<xsl:choose>
		<!-- No coverage -->
		<xsl:when test="$P_MEDICAL_PAYMENT_AMOUNT ='' or normalize-space($P_MEDICAL_PAYMENT_AMOUNT) ='NOCOVERAGE'">0</xsl:when>

		<!-- Not for trailer -->
		<xsl:when test="BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER">0</xsl:when>
		<!-- Not applicable if 
			1. Attached to Home and Outboard style/Sailboat less than 26 feet  
			2. Personal watercraft-->
		<xsl:when test="(
							(POLICY/ATTACHTOWOLVERINE = 'Y' or POLICY/BOATHOMEDISC = 'Y')
							and ((BOATS/BOAT/BOATTYPECODE != $BOATTYPE_WAVERUNNER and BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_OUTBOARD) 
									or (BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_SAILBOAT and BOATS/BOAT/LENGTH &lt;= $P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY))
						) 
								or 
						(
							BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_JETSKI or  BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET
						)">
		0
		</xsl:when>
		<xsl:otherwise>
		
	
				<xsl:variable name="LENGTH" select="BOATS/BOAT/LENGTH"/>
					
				<xsl:choose>
					<!-- Not applicable for Mini Jet Boat -->
					<xsl:when test="BOATS/BOAT/BOATSTYLECODE = $BOATTYPE_MINI_JET">
						0			
					</xsl:when>
							
					<!-- Outboard style and Sailboat over 26 feet -->
					<xsl:when test="(BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_OUTBOARD) or 
									(BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_SAILBOAT and BOATS/BOAT/LENGTH &lt; $P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY)">		
						
						<xsl:variable name="P_MED_PAY_RATE"		 select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='LIOUTBOARD']/ATTRIBUTES/@MED_PAY_RATE"/>
						<xsl:variable name="P_MIN_MED_PAY_LIMIT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='LIOUTBOARD']/ATTRIBUTES/@MEDPAY_MIN"/>
						<xsl:variable name="P_MED_PAY_RATE_PER"	 select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='LIOUTBOARD']/ATTRIBUTES/@MED_PAY_RATE_PER"/>
			
						<xsl:if test="normalize-space($P_MED_PAY_RATE) = ''">
						<tr>
							<td class="midcolora"><li> medical Payment Rate not found  (Path - "FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='LIOUTBOARD']/ATTRIBUTES/@MED_PAY_RATE") </li> </td>
						</tr>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>			
						
						<xsl:if test="normalize-space($P_MIN_MED_PAY_LIMIT) = ''">
						<tr>
							<td class="midcolora"><li> Min. medical Payment Limit not found  (Path - "FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='LIOUTBOARD']/ATTRIBUTES/@MEDPAY_MIN") </li> </td>
						</tr>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>			
			
			
						<xsl:if test="normalize-space($P_MED_PAY_RATE_PER) = ''">
						<tr>
							<td class="midcolora"><li> rate per value for medical payment not found  (Path - "FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='LIOUTBOARD']/ATTRIBUTES/@MED_PAY_RATE_PER") </li> </td>
						</tr>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>			
			
						
						<!--
						<xsl:choose>
							<xsl:when test="$P_MEDICAL_PAYMENT_AMOUNT &gt;= $P_MIN_MED_PAY_LIMIT">
								 
								<xsl:value-of  select="round(round(round(round(round(($P_MEDICAL_PAYMENT_AMOUNT - $P_MED_PAY_RATE_PER) div $P_MED_PAY_RATE_PER )*$P_MED_PAY_RATE)*$VAR_LIABILITY_SURCHARGES)*$VAR_DUAL_OWNERSHIP)*$VAR_REMOVE_SAILBOAT_FACTOR)" />	
							</xsl:when>
							<xsl:otherwise>0</xsl:otherwise>
						</xsl:choose>
						-->
					</xsl:when>
										
					<!-- All others -->
					<xsl:otherwise>
			 			<xsl:variable name="P_MED_PAY_RATE"		 select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='ALLOUTBOARD']/ATTRIBUTES/@MED_PAY_RATE"/>
						<xsl:variable name="P_MIN_MED_PAY_LIMIT"   select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='ALLOUTBOARD']/ATTRIBUTES/@MEDPAY_MIN"/>
						<xsl:variable name="P_MED_PAY_RATE_PER"		 select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='ALLOUTBOARD']/ATTRIBUTES/@MED_PAY_RATE_PER"/>
						
						
						<xsl:if test="normalize-space($P_MED_PAY_RATE) = ''">
						<tr>
							<td class="midcolora"><li> medical Payment Rate for not found  (ALLOUTBOARD)(Path - "FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='ALLOUTBOARD']/ATTRIBUTES/@MED_PAY_RATE") </li> </td>
						</tr>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>			
						
						<xsl:if test="normalize-space($P_MIN_MED_PAY_LIMIT) = ''">
						<tr>
							<td class="midcolora"><li> Min. medical Payment Limit for not found (ALLOUTBOARD) (Path - "FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='ALLOUTBOARD']/ATTRIBUTES/@MEDPAY_MIN") </li> </td>
						</tr>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>			
			
			
						<xsl:if test="normalize-space($P_MED_PAY_RATE_PER) = ''">
						<tr>
							<td class="midcolora"><li> rate per value for medical payment  not found (ALLOUTBOARD) (Path - "FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='ALLOUTBOARD']/ATTRIBUTES/@MED_PAY_RATE_PER") </li> </td>
						</tr>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>		
				<!--		
						<xsl:choose>
							<xsl:when test="$P_MEDICAL_PAYMENT_AMOUNT ='' or $P_MEDICAL_PAYMENT_AMOUNT ='NOCOVERAGE'">0</xsl:when>
							<xsl:when test="$P_MEDICAL_PAYMENT_AMOUNT &gt;= $P_MIN_MED_PAY_LIMIT">
								<xsl:value-of select="round(round(round(round(($P_MEDICAL_PAYMENT_AMOUNT - $P_MED_PAY_RATE_PER)div $P_MED_PAY_RATE_PER)*$P_MED_PAY_RATE)*$VAR_LIABILITY_SURCHARGES)*$VAR_DUAL_OWNERSHIP) " />	
							</xsl:when>
							<xsl:otherwise>0</xsl:otherwise>
						</xsl:choose>
				-->
					</xsl:otherwise>
				
				</xsl:choose>
		</xsl:otherwise>
	</xsl:choose>	
</xsl:template>
<!-- ============================================================================================ -->
<!--				Templates for MEDICAL PAYMENTS, Display (END)								 -->
<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN									              -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--				Templates for Uninsured Boaters Coverage, Display (START)					  -->
<!--		    			  FOR INDIANA,MICHIGAN and WISCONSIN					              -->
<!-- ============================================================================================ -->
<xsl:template name="UNINSURED_BOATERS_COVERAGE">
		<xsl:choose>
		    <!--Watercraft INDIANA-->
			<xsl:when test ="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:call-template name ="UNINSURED_BOATERS"/>
			</xsl:when>		
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
</xsl:template>

<!--Uninsured Boaters coverage-->
<xsl:template name="UNINSURED_BOATERS">
	<xsl:variable name="P_UNINSURED_BOATERS_LIABILITY" select="BOATS/BOAT/UNINSUREDBOATERS"/>
	<xsl:variable name="P_UNINSURED_BOATERS" select="BOATS/BOAT/UNINSUREDBOATERS"/>
	<xsl:variable name="P_UNINSURED_BOATERS_LIABILITY_AMOUNT"  select="translate(translate($P_UNINSURED_BOATERS_LIABILITY,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/>
	
	<xsl:variable name="P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SAILBOAT_LENGTH_FOR_LIABILITY"/>
	<xsl:variable name="P_INCLUDED" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='UNINSURED_BOATERS_COV']/ATTRIBUTES[@COVERAGE= $P_UNINSURED_BOATERS]/@INCLUDED"/>
	
	
	
	<xsl:if test="normalize-space($P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY) = ''">
		<tr>
			<td class="midcolora"><li> max sailboat length for liability  (Path - "FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SAILBOAT_LENGTH_FOR_LIABILITY") </li> </td>
		</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
				
		
		
	<xsl:if test="translate($P_INCLUDED,' ','') = 'NOCOVERAGE'">
		<tr>
			<td class="midcolora"><li> Include for uninsured boaters not found (Path - "FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='UNINSURED_BOATERS_COV']/ATTRIBUTES[@COVERAGE= $P_UNINSURED_BOATERS]/@INCLUDED") </li> </td>
		</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>		
				
	
	
	<xsl:choose>
		<xsl:when test="translate($P_UNINSURED_BOATERS_LIABILITY_AMOUNT,' ','') = 'NOCOVERAGE' or $P_UNINSURED_BOATERS_LIABILITY_AMOUNT = '' or $P_UNINSURED_BOATERS_LIABILITY_AMOUNT = 0">
			 
		</xsl:when>
		<!-- Not applicable to personal watercraft and trailers -->
		<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_JETSKI or  BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER">
			 
		</xsl:when>
	
		
			
		<xsl:otherwise>
		
		<!--		<xsl:variable name="P_INCLUDED" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='UNINSURED_BOATERS_COV']/ATTRIBUTES[@INCLUDED = $P_UNINSURED_BOATERS_LIABILITY]/@INCLUDED"/>-->
				<xsl:choose>
					<xsl:when test="(POLICY/ATTACHTOWOLVERINE !='Y' and POLICY/BOATHOMEDISC !='Y' and $P_INCLUDED ='TRUE')">
						 
					</xsl:when>
					<xsl:otherwise>
						<!-- Fetch base rates -->
						<xsl:variable name="P_BASE_RATE">
		 						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='UNINSURED_BOATERS_COV']/ATTRIBUTES[@COVERAGE = $P_UNINSURED_BOATERS_LIABILITY_AMOUNT]/@RATE"/>
						</xsl:variable>
						
						<xsl:if test="normalize-space($P_BASE_RATE) = ''">
						<tr>
							<td class="midcolora"><li> Base Rate for uninsured boaters not found (Path - "FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='UNINSURED_BOATERS_COV']/ATTRIBUTES[@COVERAGE = $P_UNINSURED_BOATERS_LIABILITY_AMOUNT]/@RATE") </li> </td>
						</tr>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>	
						
						
						<!-- Rate when attached to home -->
						<xsl:variable name="P_ATTACHED_TO_HOME_UNB">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='UNINSURED_BOATERS_COV_HOME']/ATTRIBUTES/@RATE"/>
						</xsl:variable>
						
						<xsl:if test="normalize-space($P_ATTACHED_TO_HOME_UNB) = ''">
						<tr>
							<td class="midcolora"><li>  Rate for uninsured boaters attached to home not found (Path - "FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='UNINSURED_BOATERS_COV_HOME']/ATTRIBUTES/@RATE") </li> </td>
						</tr>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>	
						
						
						
						<!-- fetch the  rates when boat attached to home add $12 -->
						<xsl:variable name="P_UNINSURED_BOATERS_RATE">
							<xsl:choose>
								  <xsl:when test="(POLICY/ATTACHTOWOLVERINE ='Y' or POLICY/BOATHOMEDISC ='Y')
												and
												((BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_OUTBOARD) or 
														(BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_SAILBOAT and BOATS/BOAT/LENGTH &lt; $P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY))">		
									 
									
									 
									<xsl:value-of select="$P_BASE_RATE + $P_ATTACHED_TO_HOME_UNB"/>
								</xsl:when>
								<xsl:otherwise><xsl:value-of select="$P_BASE_RATE"/> </xsl:otherwise>	
							</xsl:choose>
						</xsl:variable>
						
						<!-- Apply surcharges on this liability -->
						<!-- Liability Surcharge for Length and Speed -->
						<xsl:variable name="VAR_LIABILITY_SURCHARGES">
							<xsl:call-template name="BOAT_LENGTH_SPEED_SURCHARGE">
								<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_LIABILITY"/>
							</xsl:call-template>
						</xsl:variable>		
						
						<!-- Dual Ownership -->
						<xsl:variable name="VAR_DUAL_OWNERSHIP">
							<xsl:call-template name="DUAL_OWNERSHIP_FACTOR">
								<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_LIABILITY"/>
							</xsl:call-template>
						</xsl:variable>
						
						<!-- Remove Sailboat -->
						<xsl:variable name="VAR_REMOVE_SAILBOAT_FACTOR">
							<xsl:call-template name="REMOVE_SAILBOAT_FACTOR">
								<xsl:with-param name="FACTORELEMENT" select="$CALLED_FROM_LIABILITY"/>
							</xsl:call-template>
						</xsl:variable>
					 
						<!-- Send uninsured boaters value -->		
						<!--			
					    <xsl:value-of select="round(round(round($P_UNINSURED_BOATERS_RATE * $VAR_LIABILITY_SURCHARGES) * $VAR_DUAL_OWNERSHIP) * $VAR_REMOVE_SAILBOAT_FACTOR) "/> 
						-->
					
					
				</xsl:otherwise>				
			</xsl:choose>
		</xsl:otherwise>	
	</xsl:choose>	
</xsl:template>


 
<xsl:template name="UNINSURED_BOATERS_DISPLAY">
	<xsl:variable name="P_UNINSURED_BOATERS_LIABILITY" select="BOATS/BOAT/PERSONALLIABILITY"/>
	
	<xsl:variable name="P_UNINSURED_BOATERS" select="BOATS/BOAT/UNINSUREDBOATERS"/>
	
	
	<xsl:variable name="P_UNINSURED_BOATERS_LIABILITY_AMOUNT"  select="translate(translate($P_UNINSURED_BOATERS_LIABILITY,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/>
	<xsl:variable name="P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SAILBOAT_LENGTH_FOR_LIABILITY"/>


	<xsl:choose>
		<xsl:when test="($P_UNINSURED_BOATERS_LIABILITY_AMOUNT = 'NOCOVERAGE' or $P_UNINSURED_BOATERS_LIABILITY_AMOUNT = '') and (BOATS/BOAT/BOATSTYLECODE != O or BOATS/BOAT/BOATSTYLECODE != S)">
			0
		</xsl:when>
		<!-- Not applicable to personal watercraft and trailers -->
		<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_JETSKI  
						or  BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER ">
			0
		</xsl:when>
		<xsl:otherwise>
		
			<xsl:variable name="P_INCLUDED" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITYRATES']/NODE[@ID ='UNINSURED_BOATERS_COV']/ATTRIBUTES[@COVERAGE= $P_UNINSURED_BOATERS]/@INCLUDED"/>
			
			<xsl:choose>
				<!-- outboard and sailboat less than 26 ft. will always be charged so send 0 for displaying 'Included' -->
				<xsl:when test="(POLICY/ATTACHTOWOLVERINE ='Y' or POLICY/BOATHOMEDISC ='Y')
												and
												((BOATS/BOAT/BOATSTYLECODE =  $BOATSTYLE_OUTBOARD) or 
														(BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_SAILBOAT and BOATS/BOAT/LENGTH &lt; $P_MAX_SAILBOAT_LENGTH_FOR_LIABILITY))">		
					0	
				</xsl:when>
				<!-- When the boat is standalone( for all boat types other than Mini Jet Boat, Wave runner or Jet Ski ), then Uninsured boaters is always 'Included' without any charge (for 10K or 50K) -->
				<xsl:when test="(POLICY/ATTACHTOWOLVERINE !='Y' or POLICY/BOATHOMEDISC !='Y')and $P_INCLUDED ='TRUE'">		
					Included			
				</xsl:when>
				<xsl:when test="(POLICY/ATTACHTOWOLVERINE ='Y' or POLICY/BOATHOMEDISC ='Y') and $P_INCLUDED ='TRUE'"> 
				 	Included
				</xsl:when>
				 
				<xsl:otherwise>
					0 
				</xsl:otherwise>				
			</xsl:choose>
			
		</xsl:otherwise>	
	</xsl:choose>	
</xsl:template>

<!-- ============================================================================================ -->
<!--				Templates for Uninsured Boaters Coverage, Display (END)						  -->
<!--		    			  FOR INDIANA,MICHIGAN and WISCONSIN					              -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--				Templates for WATERCRAFT LIABILITY/PD SURCHARGES,(START)					  -->
<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN								              -->
<!-- ============================================================================================ -->


<xsl:template name="CONSTRUCTION_TYPE_SURCHARGE"> 
	<xsl:variable name="VAR_WOOD_BOAT_AGE">	<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='WOODENBOAT']/ATTRIBUTES/@BOAT_AGE"/></xsl:variable>
	<xsl:variable name="VAR_FIBER_BOAT_AGE">	<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='FIBERGLASSBOAT']/ATTRIBUTES/@BOAT_AGE"/></xsl:variable>
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/CONSTRUCTIONCODE = $CONSTRUCTIONCODE_WOOD and BOATS/BOAT/AGE &gt; $VAR_WOOD_BOAT_AGE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='WOODENBOAT']/ATTRIBUTES/@FACTOR"/>	
		</xsl:when>
		<xsl:when test = "BOATS/BOAT/CONSTRUCTIONCODE = $CONSTRUCTIONCODE_FIBREGLASS and BOATS/BOAT/AGE &gt; $VAR_FIBER_BOAT_AGE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='FIBERGLASSBOAT']/ATTRIBUTES/@FACTOR"/>	
		</xsl:when>
		<xsl:otherwise>
		1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
	

<!-- ============================================================================================ -->
<!-- ============================================================================================ -->
<xsl:template name="BOAT_LENGTH_SPEED_SURCHARGE_PREMIUM">
	<xsl:param name="FACTORELEMENT" /> <!-- Depending on the PD or Liability sections from which this template is called -->
	<xsl:variable name="P_MAX_LENGTH" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_LENGTH_OF_BOAT"/>
	<xsl:variable name="P_MAX_SPEED" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SPEED_OF_BOAT"/>
	<xsl:choose>			
			<xsl:when test = "BOATS/BOAT/LENGTH &gt; $P_MAX_LENGTH and BOATS/BOAT/CAPABLESPEED &gt; $P_MAX_SPEED  and (BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD  or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD_OUTBOARD or BOATS/BOAT/BOATTYPECODE=$BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATTYPECODE=$BOATTYPE_OUTBOARD_WITH_MOTOR)">
				Included
			</xsl:when>
			<xsl:otherwise>
				0
			</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="BOAT_LENGTH_SPEED_SURCHARGE">
	<xsl:param name="FACTORELEMENT" /> <!-- Depending on the PD or Liability sections from which this template is called -->
	<xsl:variable name="P_MAX_LENGTH" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_LENGTH_OF_BOAT"/>
	<xsl:variable name="P_MAX_SPEED" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SPEED_OF_BOAT"/>
	

							
					
					<xsl:if test="normalize-space($P_MAX_LENGTH) = ''">
						<tr>
							<td class="midcolora"><li> Max Boat Length not found  (Path - "FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_LENGTH_OF_BOAT") </li> </td>
						</tr>
						<xsl:if test="user:AllVerified(0)=1"></xsl:if>
					</xsl:if>
					
												
					
					<xsl:if test="normalize-space($P_MAX_SPEED) = ''">
						<tr>
							<td class="midcolora"><li> Max Speed of boat not found  (Path - "FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SPEED_OF_BOAT") </li> </td>
						</tr>
						<xsl:if test="user:AllVerified(0)=1"></xsl:if>
					</xsl:if>
						
	
	
	<xsl:choose>			
			<xsl:when test = "BOATS/BOAT/LENGTH &gt; $P_MAX_LENGTH and BOATS/BOAT/CAPABLESPEED &gt; $P_MAX_SPEED  ">
				<xsl:choose>					
					<xsl:when test="$FACTORELEMENT = $CALLED_FROM_LIABILITY">
						<xsl:variable name="BL_SURCHARGE_LI"> 
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LISURCHARGE']/NODE[@ID ='LIBOATSPEED']/ATTRIBUTES/@FACTOR"/>
						</xsl:variable>
						
						<xsl:if test="normalize-space($BL_SURCHARGE_LI) = ''">
						<tr>
							<td class="midcolora"><li> surcharge for Boat length(Liablity) (Path - "FACTOR[@ID='LISURCHARGE']/NODE[@ID ='LIBOATSPEED']/ATTRIBUTES/@FACTOR") </li> </td>
						</tr>
						<xsl:if test="user:AllVerified(0)=1"></xsl:if>
					</xsl:if>
						
						
							
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = $CALLED_FROM_PD">
					
						<xsl:variable name="BL_SURCHARGE_PD">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDBOATSPEED']/ATTRIBUTES/@FACTOR"/>
						</xsl:variable>
						
					<xsl:if test="normalize-space($BL_SURCHARGE_PD) = ''">
						<tr>
							<td class="midcolora"><li> surcharge for Boat length(PD) (Path - "FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDBOATSPEED']/ATTRIBUTES/@FACTOR") </li> </td>
						</tr>
						<xsl:if test="user:AllVerified(0)=1"></xsl:if>
					</xsl:if>
					
					
					</xsl:when>
					<xsl:otherwise>
						1.00
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				1.00
			</xsl:otherwise>
	</xsl:choose>
</xsl:template>
 

<xsl:template name="BOAT_LENGTH_SPEED_SURCHARGE_DISPLAY">
	<xsl:param name="FACTORELEMENT" /> <!-- Depending on the PD or Liability sections from which this template is called -->

	<xsl:variable name="P_MAX_LENGTH" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_LENGTH_OF_BOAT"/>
	<xsl:variable name="P_MAX_SPEED" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPONENT_LIMITS']/NODE[@ID ='LIMITS_VALUE']/ATTRIBUTES/@MAX_SPEED_OF_BOAT"/>
	
	<xsl:choose>			 
			<xsl:when test = "BOATS/BOAT/LENGTH &gt; $P_MAX_LENGTH and BOATS/BOAT/CAPABLESPEED &gt; $P_MAX_SPEED  and (BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD  or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_INBOARD_OUTBOARD or BOATS/BOAT/BOATTYPECODE=$BOATTYPE_WAVERUNNER  or BOATS/BOAT/BOATTYPECODE=$BOATTYPE_OUTBOARD_WITH_MOTOR)">
				<xsl:choose>
					<xsl:when test="$FACTORELEMENT = $CALLED_FROM_LIABILITY">
							Charge - Speed over <xsl:value-of select="$P_MAX_SPEED"/> MPH +<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LISURCHARGE']/NODE[@ID ='LIBOATSPEED']/ATTRIBUTES/@SURCHARGE"/>% 
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = $CALLED_FROM_PD">
							Charge - Speed over <xsl:value-of select="$P_MAX_SPEED"/> MPH +<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDBOATSPEED']/ATTRIBUTES/@SURCHARGE"/>% 
					</xsl:when>
					<xsl:otherwise>
						 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				 
			</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<!-- ============================================================================================ -->
<!-- ============================================================================================ -->

<xsl:template name="FIBRE_GLASS_DISPLAY">
	<xsl:variable name="VAR_FIBER_BOAT_AGE">	<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='FIBERGLASSBOAT']/ATTRIBUTES/@BOAT_AGE"/></xsl:variable>
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/CONSTRUCTIONCODE = $CONSTRUCTIONCODE_FIBREGLASS and BOATS/BOAT/AGE &gt; $VAR_FIBER_BOAT_AGE">
			Charge - Fibre Glass Boat + <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='FIBERGLASSBOAT']/ATTRIBUTES/@SURCHARGE"/>%			
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="FIBRE_GLASS_PREMIUM">
		<xsl:variable name="VAR_FIBER_BOAT_AGE">	<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='FIBERGLASSBOAT']/ATTRIBUTES/@BOAT_AGE"/></xsl:variable>

		<xsl:choose>
            <!--watercraft INDIANA-->
			<xsl:when test ="POLICY/STATENAME ='INDIANA'">
				<xsl:choose>
					<xsl:when test = "BOATS/BOAT/CONSTRUCTIONCODE = $CONSTRUCTIONCODE_FIBREGLASS and BOATS/BOAT/AGE &gt; $VAR_FIBER_BOAT_AGE">
						<xsl:choose>
							<!-- Not applicable to personal watercraft and trailers -->
							<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_JETSKI  
											or  BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER "> 	
								0
 							</xsl:when>
 							<xsl:otherwise>
 		   						Included
 							</xsl:otherwise>
 						</xsl:choose>
 					</xsl:when>
					<xsl:otherwise>
					0
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>		
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	

	<xsl:template name="FIBRE_GLASS_FACTOR">
		<xsl:variable name="VAR_FIBER_BOAT_AGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='FIBERGLASSBOAT']/ATTRIBUTES/@BOAT_AGE"/> 
			
			<xsl:if test="normalize-space($VAR_FIBER_BOAT_AGE) = ''">
				<tr>
					<td class="midcolora"><li> Age of fiber boat not found  (Path - "FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='FIBERGLASSBOAT']/ATTRIBUTES/@BOAT_AGE") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>

		
		
		<xsl:choose>            
			<xsl:when test ="POLICY/STATENAME ='INDIANA'">
				<xsl:choose>
					<xsl:when test = "BOATS/BOAT/CONSTRUCTIONCODE = $CONSTRUCTIONCODE_FIBREGLASS and BOATS/BOAT/AGE &gt; $VAR_FIBER_BOAT_AGE">
						<xsl:choose>
							<!-- Not applicable to personal watercraft and trailers -->
							<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_JETSKI  
											or  BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER "> 	
								1.00
 							</xsl:when>
 							<xsl:otherwise>
								<xsl:variable name="VAR_FIBERGLASSBOAT_FACTOR"> 
 		   							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='FIBERGLASSBOAT']/ATTRIBUTES/@FACTOR"/>
 								</xsl:variable> 
 							
 							<xsl:if test="normalize-space($VAR_FIBERGLASSBOAT_FACTOR) = ''">
								<tr>
									<td class="midcolora"><li> Fiber glass boat factor not found  (Path - "FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='FIBERGLASSBOAT']/ATTRIBUTES/@FACTOR") </li> </td>
								</tr>
								<xsl:if test="user:AllVerified(0)=1"></xsl:if>
							</xsl:if>

 							</xsl:otherwise>
 						</xsl:choose>
 					</xsl:when>
					<xsl:otherwise>
					1.00
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>		
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
<!-- ============================================================================================ -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					Templates for WOODEN BOAT, Display (START)								  -->
<!--		    					 FOR INDIANA,MICHIGAN and WISCONSIN	
			Surcharge -Wooden boats over 10 years old. 								              -->
<!-- ============================================================================================ -->

<xsl:template name="WOODEN_BOAT_DISPLAY">
	<xsl:variable name="VAR_WOOD_BOAT_AGE">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='WOODENBOAT']/ATTRIBUTES/@BOAT_AGE"/>								
	</xsl:variable>
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/CONSTRUCTIONCODE = $CONSTRUCTIONCODE_WOOD and BOATS/BOAT/AGE &gt; $VAR_WOOD_BOAT_AGE">
		Charge - Wooden Boat +<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='WOODENBOAT']/ATTRIBUTES/@SURCHARGE"/>%</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="WOODEN_BOAT_PREMIUM">
	<xsl:choose>
		<xsl:when test ="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
			<xsl:variable name="VAR_WOOD_BOAT_AGE">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='WOODENBOAT']/ATTRIBUTES/@BOAT_AGE"/>								
			</xsl:variable>

			<xsl:choose>
				<xsl:when test = "BOATS/BOAT/CONSTRUCTIONCODE = $CONSTRUCTIONCODE_WOOD and BOATS/BOAT/AGE &gt; $VAR_WOOD_BOAT_AGE">
					<xsl:choose>
						<!-- Not applicable to personal watercraft and trailers -->
						<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_JETSKI  
									or  BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER "> 					
						0
 						</xsl:when>
 						<xsl:otherwise>
 		   				Included
 						</xsl:otherwise>
 					</xsl:choose>
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<xsl:template name="WOODEN_BOAT_FACTOR">
	<xsl:choose>
		<xsl:when test ="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
			<xsl:variable name="VAR_WOOD_BOAT_AGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='WOODENBOAT']/ATTRIBUTES/@BOAT_AGE"/>								
			
			<xsl:if test="normalize-space($VAR_WOOD_BOAT_AGE) = ''">
				<tr>
					<td class="midcolora"><li> age for wooden baot not found  (Path - "FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='WOODENBOAT']/ATTRIBUTES/@BOAT_AGE") </li> </td>
				</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>

			<xsl:choose>
				<xsl:when test = "BOATS/BOAT/CONSTRUCTIONCODE = $CONSTRUCTIONCODE_WOOD and BOATS/BOAT/AGE &gt; $VAR_WOOD_BOAT_AGE">
					<xsl:choose>
						<!-- Not applicable to personal watercraft and trailers -->
						<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_JETSKI  
									or  BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER "> 					
						1.00
 						</xsl:when>
 						<xsl:otherwise>
   				 		
   				 				<xsl:variable name="VAR_WOOD_BOAT_AGE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='WOODENBOAT']/ATTRIBUTES/@FACTOR"/>								
 						
 						<xsl:if test="normalize-space($VAR_WOOD_BOAT_AGE_FACTOR) = ''">
							<tr>
								<td class="midcolora"><li> wooden boat age factor not found  (Path - "FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='WOODENBOAT']/ATTRIBUTES/@FACTOR") </li> </td>
							</tr>
							<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>
						
						<xsl:value-of select="$VAR_WOOD_BOAT_AGE_FACTOR"/> 
						
 						
 						</xsl:otherwise>
 					</xsl:choose>
				</xsl:when>
				<xsl:otherwise>1.00</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
</xsl:template>


 
<!-- ============================================================================================ -->
<!-- ============================================================================================ -->

<xsl:template name="REMOVE_SAILBOAT_DISPLAY">
	<xsl:param name="FACTORELEMENT" /> <!-- Depending on the PD or Liability sections from which this template is called -->
	<xsl:variable name="P_BOAT_LENGTH_FOR_REMOVALSAILBOAT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDREMOVALSAILBOAT']/ATTRIBUTES/@BOAT_LENGTH_UNDER"/>

	<xsl:choose>
		<xsl:when test = "(BOATS/BOAT/REMOVESAILBOAT ='Y' and BOATS/BOAT/LENGTH &lt; $P_BOAT_LENGTH_FOR_REMOVALSAILBOAT) ">Charge - Sailboat Racing Exclusion Waiver 
				<xsl:choose>
					<xsl:when test="$FACTORELEMENT = $CALLED_FROM_LIABILITY">
						+<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LISURCHARGE']/NODE[@ID ='LIREMOVALSAILBOAT']/ATTRIBUTES/@SURCHARGE"/>
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = $CALLED_FROM_PD">
						+<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDREMOVALSAILBOAT']/ATTRIBUTES/@SURCHARGE"/>
					</xsl:when>
					<xsl:otherwise>
						0
					</xsl:otherwise>
				</xsl:choose>%
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="REMOVE_SAILBOAT_PREMIUM">
	<xsl:param name="FACTORELEMENT" /> <!-- Depending on the PD or Liability sections from which this template is called -->
	<xsl:variable name="P_BOAT_LENGTH_FOR_REMOVALSAILBOAT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDREMOVALSAILBOAT']/ATTRIBUTES/@BOAT_LENGTH_UNDER"/>
	<xsl:choose>
		<xsl:when test = "(BOATS/BOAT/REMOVESAILBOAT ='Y' and BOATS/BOAT/LENGTH &lt; $P_BOAT_LENGTH_FOR_REMOVALSAILBOAT)">
			Included
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="REMOVE_SAILBOAT_FACTOR">
	<xsl:param name="FACTORELEMENT" /> <!-- Depending on the PD or Liability sections from which this template is called -->
	<xsl:variable name="P_BOAT_LENGTH_FOR_REMOVALSAILBOAT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDREMOVALSAILBOAT']/ATTRIBUTES/@BOAT_LENGTH_UNDER"/>
	<xsl:choose>
		<xsl:when test = "(BOATS/BOAT/REMOVESAILBOAT ='Y' and BOATS/BOAT/LENGTH &lt; $P_BOAT_LENGTH_FOR_REMOVALSAILBOAT)">
				<xsl:choose>
					<xsl:when test="$FACTORELEMENT = $CALLED_FROM_LIABILITY">

					<xsl:variable name ="VAR_REMOVE_SAILBOAT_FACTOR_LI">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LISURCHARGE']/NODE[@ID ='LIREMOVALSAILBOAT']/ATTRIBUTES/@FACTOR"/>
					</xsl:variable>

					<xsl:if test="normalize-space($VAR_REMOVE_SAILBOAT_FACTOR_LI) = ''">
						<tr>
							<td class="midcolora"><li> Sail boat factor(Liablity) not found  (Path - "FACTOR[@ID='LISURCHARGE']/NODE[@ID ='LIREMOVALSAILBOAT']/ATTRIBUTES/@FACTOR") </li> </td>
						</tr>
						<xsl:if test="user:AllVerified(0)=1"></xsl:if>
					</xsl:if>



					</xsl:when>
					<xsl:when test="$FACTORELEMENT = $CALLED_FROM_PD">
				
				<xsl:variable name ="VAR_REMOVE_SAILBOAT_FACTOR_PD"> 
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDREMOVALSAILBOAT']/ATTRIBUTES/@FACTOR"/>
				</xsl:variable>
				
					<xsl:if test="normalize-space($VAR_REMOVE_SAILBOAT_FACTOR_PD) = ''">
						<tr>
							<td class="midcolora"><li> Sail boat factor not found  (Path - "FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDREMOVALSAILBOAT']/ATTRIBUTES/@FACTOR") </li> </td>
						</tr>
						<xsl:if test="user:AllVerified(0)=1"></xsl:if>
					</xsl:if>
				
					
					</xsl:when>
					<xsl:otherwise>
						1.00
					</xsl:otherwise>
				</xsl:choose>
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
</xsl:template>



<!-- ============================================================================================ -->
<!-- ============================================================================================ -->
<xsl:template name="DUAL_OWNERSHIP_PREMIUM">
	<xsl:choose>       
		<xsl:when test ="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
			<xsl:choose>
		<xsl:when test = "BOATS/BOAT/DUALOWNERSHIP = 'Y'">Included</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="DUAL_OWNERSHIP_FACTOR">  
	<xsl:param name="FACTORELEMENT" /> <!-- Depending on the PD or Liability sections from which this template is called -->
	
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/DUALOWNERSHIP = 'Y'">
			<xsl:choose>
				<xsl:when test="$FACTORELEMENT = $CALLED_FROM_LIABILITY">
					<xsl:value-of  select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LISURCHARGE']/NODE[@ID ='LIDUALOWNERSHIP']/ATTRIBUTES/@FACTOR"/>							
				</xsl:when>
				<xsl:when test="$FACTORELEMENT = $CALLED_FROM_PD">
					<xsl:value-of  select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDDUALOWNERSHIP']/ATTRIBUTES/@FACTOR"/>							
				</xsl:when>
				<xsl:otherwise>
					1.00
				</xsl:otherwise>
			</xsl:choose>			
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
	 
		
	
</xsl:template>


<!-- ============================================================================================ -->
<!--				Templates for WATERCRAFT LIABILITY SURCHARGES,(END)							  -->
<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN								              -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--					Templates for Credits , Display (START)									  -->
<!--		    					  FOR INDIANA, MICHIGAN, WISCONSIN									              -->
<!-- ============================================================================================ -->

<xsl:template name="DIESEL_ENGINE_DISPLAY">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/DIESELENGINE = 'Y'">Discount - Diesel Engine -<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='DIESELENGINECRAFT']/ATTRIBUTES/@CREDIT"/>%</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>


<xsl:template name="DIESEL_ENGINE_PREMIUM">
	<xsl:choose>         
		<xsl:when test ="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
			<xsl:choose>
				<xsl:when test = "BOATS/BOAT/DIESELENGINE = 'Y'">Included</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:when>			
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<xsl:template name="DIESEL_ENGINE_FACTOR">
<xsl:variable name="VAR_DIESEL_ENGINE"> 
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/DIESELENGINE = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='DIESELENGINECRAFT']/ATTRIBUTES/@FACTOR"/>			
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
	</xsl:variable>
	
		<xsl:if test="normalize-space($VAR_DIESEL_ENGINE) = ''">
			<tr>
				<td class="midcolora"><li> Diesel engine factor not found  (Path - "FACTOR[@ID='PDCREDIT']/NODE[@ID ='DIESELENGINECRAFT']/ATTRIBUTES/@FACTOR") </li> </td>
			</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
				
		<xsl:value-of select="$VAR_DIESEL_ENGINE"/>
			
</xsl:template>

<!-- ============================================================================================ -->
<!--					Templates for Discounts , Display (END)							      -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN												  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					Templates for Halon Extinguisher, Display (START)			      		  -->
<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN									              -->
<!-- ============================================================================================ -->

<xsl:template name="HALON_FIRE_DISPLAY">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/HALONFIRE = 'Y'">Discount - Halon Fire Extinguishing System	-<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='FIREEXTINGUISHER']/ATTRIBUTES/@CREDIT"/>%</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="HALON_FIRE_PREMIUM">
		<xsl:choose>            
			<xsl:when test ="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				<xsl:choose>
					<xsl:when test = "BOATS/BOAT/HALONFIRE = 'Y'">Included</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>


<xsl:template name="HALON_FIRE_FACTOR">

<xsl:variable name="VAR_HALON_FIRE"> 

	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/HALONFIRE = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='FIREEXTINGUISHER']/ATTRIBUTES/@FACTOR"/>		
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
</xsl:variable>

	
	<xsl:if test="normalize-space($VAR_HALON_FIRE) = ''">
				<tr>
					<td class="midcolora"><li> halon fire factor  not found  (Path - "FACTOR[@ID='PDCREDIT']/NODE[@ID ='FIREEXTINGUISHER']/ATTRIBUTES/@FACTOR") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
				<xsl:value-of select="$VAR_HALON_FIRE"/>

</xsl:template>

<!-- ============================================================================================ -->
<!--					Templates for Halon Extinguisher, Display (END)							  -->
<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN												  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					Templates for Loran GPS or other Navigation System, Display (START)		  -->
<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN									              -->
<!-- ============================================================================================ -->
<xsl:template name="LORAN_GPS_DISPLAY">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/LORANNAVIGATIONSYSTEM = 'Y'">Discount - Loran Navigation system, GPS or Depth Sounder and Ship to shore Radio -<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='NAVIGATIONSYSTEM']/ATTRIBUTES/@CREDIT"/>%</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<xsl:template name="LORAN_GPS_PREMIUM">
	<xsl:choose>        
		<xsl:when test ="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
			<xsl:choose>
				<xsl:when test = "BOATS/BOAT/LORANNAVIGATIONSYSTEM = 'Y'">Included</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="LORAN_GPS_FACTOR">

<xsl:variable name="VAR_LORAN_GPS_FACTOR"> 

	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/LORANNAVIGATIONSYSTEM = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='NAVIGATIONSYSTEM']/ATTRIBUTES/@FACTOR"/>
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
</xsl:variable>
	
	
	<xsl:if test="normalize-space($VAR_LORAN_GPS_FACTOR) = ''">
				<tr>
					<td class="midcolora"><li> Lorangps factor  not found  (Path - "FACTOR[@ID='PDCREDIT']/NODE[@ID ='NAVIGATIONSYSTEM']/ATTRIBUTES/@FACTOR") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
	<xsl:value-of select="$VAR_LORAN_GPS_FACTOR"/>

			
			
			
</xsl:template>


<!-- ============================================================================================ -->
<!--					Templates for Loran GPS or other Navigation System, Display (END)		  -->
<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN												  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					Templates for Shore Station, Display (START)							  -->
<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN			              -->
<!-- ============================================================================================ -->

<xsl:template name="SHORE_STATION_DISPLAY">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/SHORESTATION = 'Y'">Discount - Shore Station -<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='SHORESTATIONCREDIT']/ATTRIBUTES/@CREDIT"/>%</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="SHORE_STATION_PREMIUM">
	<xsl:choose>
		<xsl:when test ="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
			<xsl:choose>
				<xsl:when test = "BOATS/BOAT/SHORESTATION = 'Y'">Included</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="SHORE_STATION_FACTOR">

<xsl:variable name="VAR_SHORE_STATION"> 

	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/SHORESTATION = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='SHORESTATIONCREDIT']/ATTRIBUTES/@FACTOR"/>				
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
</xsl:variable>
	

	<xsl:if test="normalize-space($VAR_SHORE_STATION) = ''">
		<tr>
			<td class="midcolora"><li> shore station factor  not found  (Path - "FACTOR[@ID='PDCREDIT']/NODE[@ID ='SHORESTATIONCREDIT']/ATTRIBUTES/@FACTOR") </li> </td>
		</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>	
	
		<xsl:value-of select="$VAR_SHORE_STATION"/>
		
				
</xsl:template>

<!-- ============================================================================================ -->
<!--					Templates for Shore Station, Display (END)								  -->
<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN							  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--					Templates for Multi-Boat, Display (START)								  -->
<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN			              -->
<!-- ============================================================================================ -->

<xsl:template name="MULTI_BOAT_DISPLAY">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/MULTIBOATCREDIT = 'Y'">Discount - Multi-Boat -<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='MULTIBOAT']/ATTRIBUTES/@CREDIT"/>%</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<xsl:template name="MULTI_BOAT_PREMIUM">
	<xsl:choose>
		<xsl:when test ="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
			<xsl:choose>
				<xsl:when test = "BOATS/BOAT/MULTIBOATCREDIT = 'Y'">
					<!--Check condition for trailer types-->
					<xsl:choose>
						<xsl:when test="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_JETSKI  
									or  BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER ">
 							0
 						</xsl:when>
 						<xsl:otherwise>
 		   				Included
 						</xsl:otherwise>
 					</xsl:choose>
				</xsl:when>				
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="MULTI_BOAT_FACTOR">


	<xsl:variable name="VAR_MULTI_BOAT">

	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/MULTIBOATCREDIT = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='MULTIBOAT']/ATTRIBUTES/@FACTOR"/>								
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
</xsl:variable>	

	<xsl:if test="normalize-space($VAR_MULTI_BOAT) = ''">
		<tr>
			<td class="midcolora"><li> Multi Boat Discount factor not found  (Path - "FACTOR[@ID='PDCREDIT']/NODE[@ID ='MULTIBOAT']/ATTRIBUTES/@FACTOR") </li> </td>
		</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>
	
		<xsl:value-of select="$VAR_MULTI_BOAT"/>



				
</xsl:template>

<!-- ============================================================================================ -->
<!--					Templates for  Multi-Boat, Display (END)								  -->
<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN						  -->
<!-- ============================================================================================ -->

 


<!-- ============================================================================================ -->
<!--				Templates for Boat Replacement Cost Coverage, Display (START)				  -->
<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN									              -->
<!-- ============================================================================================ -->
<xsl:template name="BOAT_REPLACEMENT_DISPLAY">
	<xsl:variable name="P_AGE_LIMIT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BOAT_REPLACEMENT_COST']/NODE[@ID ='BOAT_REPLACEMENT']/ATTRIBUTES/@AGE_OF_WATERCRAFT"/>			
	<xsl:choose>			
		<xsl:when test ="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
			<xsl:choose>
				<xsl:when test="BOATS/BOAT/AGE &lt;= $P_AGE_LIMIT" >
					<xsl:choose>
 						<xsl:when test = "BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER ">
 							0
 						</xsl:when>
 						<xsl:otherwise>
 		   					Included
 						</xsl:otherwise>
 					</xsl:choose>
			</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>	
			</xsl:choose> 
		</xsl:when>			
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--				Templates for Boat Replacement Cost Coverage, Display (END)					  -->
<!--		    					  FOR INDIANA	, MICHIGAN, WISCONSIN								              -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--				Templates for Boat-Home(Multipolicy discount), Display (START)				  -->
<!--		    					  FOR INDIANA	, MICHIGAN, WISCONSIN								              -->
<!-- ============================================================================================ -->
<xsl:template name="DISCOUNT_BOAT_HOME">
		<xsl:choose>           
			<xsl:when test ="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
				 	<xsl:choose>
						<xsl:when test="POLICY/BOATHOMEDISC = 'Y'">
							<xsl:choose>
								<!-- Not applicable for personal watercraft adn trailers -->
								<xsl:when test ="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_JETSKI or  BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or
												 BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER " >
									0
								</xsl:when>
								<xsl:otherwise>Included</xsl:otherwise>		
							</xsl:choose>
						</xsl:when>
						<xsl:otherwise>0</xsl:otherwise>		
					</xsl:choose>
			</xsl:when>			
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
</xsl:template>

<xsl:template name="DISCOUNT_BOAT_HOME_FACTOR">
<xsl:variable name="VAR_BOAT_HOME"> 
	<xsl:choose>
		<xsl:when test="POLICY/BOATHOMEDISC='Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID='MULTIPOLICY']/ATTRIBUTES/@FACTOR" /> 
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>		
	</xsl:choose>
</xsl:variable>
	
	
		<xsl:if test="normalize-space($VAR_BOAT_HOME) = ''">
				<tr>
					<td class="midcolora"><li> boat home discount factor not found  (Path - "FACTOR[@ID='PDCREDIT']/NODE[@ID='MULTIPOLICY']/ATTRIBUTES/@FACTOR") </li> </td>
				</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				
	<xsl:value-of select="$VAR_BOAT_HOME"/>
				
				
</xsl:template>
<!-- ============================================================================================ -->
<!--				Templates for Boat-Home(Multipolicy discount), Display (END)				  -->
<!--		    					  FOR INDIANA	, MICHIGAN, WISCONSIN								              -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					Templates for 5 Years Experience or Navigation Course , Display (START)	  -->
<!--		    					  FOR INDIANA									              
		Experience: 5 or More Years Experience and /or Owners completed navigation
		course in either U.S. Coast Guard Auxiliary or Power Squadron.							  -->
<!-- ============================================================================================ -->


<xsl:template name="EXPERIENCE_CREDIT_DISPLAY">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/POWERSQUADRONCOURSE = 'Y' or BOATS/BOAT/COASTGUARDAUXILARYCOURSE = 'Y' or BOATS/BOAT/HAS_5_YEARSOPERATOREXPERIENCE = 'Y'">Discount - 5 Years Experience or Navigation Course -(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='EXPNAVIGATIONCOURSE']/ATTRIBUTES/@CREDIT"/>%)</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="EXPERIENCE_CREDIT_PREMIUM">
	<xsl:choose>
   		<xsl:when test ="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
			<!-- Not applicable to personal watercraft and trailers -->
			<xsl:choose>
				<xsl:when test ="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_JETSKI or  BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER " >
					0
				</xsl:when>
				<xsl:otherwise>
				 		<xsl:variable name="VAR_EXPERIENCE_CREDIT__DISPLAY"> 
							<xsl:call-template name="EXPERIENCE_CREDIT_DISPLAY"/>
						</xsl:variable>
					    
						<xsl:choose> 
								<xsl:when test="$VAR_EXPERIENCE_CREDIT__DISPLAY = 0">
									0
								</xsl:when>
								<xsl:otherwise>Included</xsl:otherwise>
						</xsl:choose>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>
 
<xsl:template name="EXPERIENCE_CREDIT_FACTOR">
<xsl:variable name="VAR_EXPERIENCE_CREDIT">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/POWERSQUADRONCOURSE = 'Y' or BOATS/BOAT/COASTGUARDAUXILARYCOURSE = 'Y' or BOATS/BOAT/HAS_5_YEARSOPERATOREXPERIENCE = 'Y'">
				<!-- Not applicable to personal watercraft and trailers -->
				<xsl:choose>
					<xsl:when test ="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_JETSKI or  BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER " >
						1.00
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='EXPNAVIGATIONCOURSE']/ATTRIBUTES/@FACTOR"/>
					</xsl:otherwise>
				</xsl:choose>
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
	</xsl:variable>
		
			<xsl:if test="normalize-space($VAR_EXPERIENCE_CREDIT) = ''">
				<tr>
					<td class="midcolora"><li> Experience credit factor  not found  (Path - "FACTOR[@ID='PDCREDIT']/NODE[@ID ='EXPNAVIGATIONCOURSE']/ATTRIBUTES/@FACTOR") </li> </td>
				</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				
					<xsl:value-of select="$VAR_EXPERIENCE_CREDIT"/>

</xsl:template>

<!-- ============================================================================================ -->
<!--					Templates for 5 Years Experience or Navigation Course , Display (END)	  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN												  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					Minijet Charge	  -->
<!--		    			FOR INDIANA,MICHIGAN,WISCONSIN											-->
<!-- ============================================================================================ -->
<xsl:template name="MINIJET_DISPLAY">
	<xsl:choose>
		<xsl:when test = "BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET">Charge - Mini Jet Boat +<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='MINIJETBOAT']/ATTRIBUTES/@CREDIT"/>%</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="MINIJET_PREMIUM">
	<xsl:choose>
           
   		<xsl:when test ="POLICY/STATENAME ='INDIANA' or POLICY/STATENAME ='MICHIGAN' or POLICY/STATENAME ='WISCONSIN'">
			<xsl:choose>
				<xsl:when test = "BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET">Included</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>

 



<!-- ============================================================================================ -->
<!--					Templates for Unattached Equip. & Personal Effects Display (START)		  -->
<!--		    					  FOR INDIANA,MICHIGAN and WISCONSIN												  -->
<!-- ============================================================================================ -->
<!-- This is charged on the amount above the unattached equipment value -->
<xsl:template name="UNATTACHED_PREMIUM">
	<xsl:variable name="P_UNATTACHEDEQUIPMENT">
		<xsl:value-of select="BOATS/BOAT/UNATTACHEDEQUIPMENT"/>
	</xsl:variable>
	<xsl:variable name="P_UNATTACHEDEQUIPMENT_VALUE" select="translate($P_UNATTACHEDEQUIPMENT,'$','')"/>
	<xsl:variable name="VAR_MINIMUM">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID ='UNATTACHEDEQUIPMENT']/ATTRIBUTES/@MINIMUM"/>
	</xsl:variable>

	<xsl:choose>
		<xsl:when test ="$P_UNATTACHEDEQUIPMENT_VALUE =$VAR_MINIMUM">Included</xsl:when>
		<xsl:otherwise>			
			0		
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="UNATTACHED_PREMIUM_VALUE">
	<xsl:variable name="P_UNATTACHEDEQUIPMENT">
		<xsl:value-of select="BOATS/BOAT/UNATTACHEDEQUIPMENT"/>
	</xsl:variable>
	<xsl:variable name="P_UNATTACHEDEQUIPMENT_VALUE" select="translate($P_UNATTACHEDEQUIPMENT,'$','')"/>
	
	<xsl:variable name="VAR_MINIMUM">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID ='UNATTACHEDEQUIPMENT']/ATTRIBUTES/@MINIMUM"/>
	</xsl:variable>
	<xsl:variable name="VAR_RATE_PER_VALUE">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID ='UNATTACHEDEQUIPMENT']/ATTRIBUTES/@RATE_PER_VALUE"/>
	</xsl:variable>
	<xsl:variable name="VAR_RATE">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERCOVERAGE']/NODE[@ID ='UNATTACHEDEQUIPMENT']/ATTRIBUTES/@RATE"/>
	</xsl:variable>

	<xsl:choose>
			<xsl:when test ="$P_UNATTACHEDEQUIPMENT_VALUE = $VAR_MINIMUM">0</xsl:when>
			<xsl:otherwise><xsl:value-of select="round(round(round($P_UNATTACHEDEQUIPMENT_VALUE - $VAR_MINIMUM) div $VAR_RATE_PER_VALUE) * $VAR_RATE)"/></xsl:otherwise>
	</xsl:choose>
	 
	
	 
	 
	


</xsl:template>

<!-- ============================================================================================ -->
<!--					Templates for Unattached Equip. & Personal Effects Display (END)		  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN												  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					Templates for Deductible												  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
<!-- ============================================================================================ -->
	<xsl:template name="DEDUCTIBLE_DISPLAY">
		
		<!-- Get the deductible percentage -->	
		<xsl:variable name="DED_DESC" select="BOATS/BOAT/DEDUCTIBLE"/> 
		
		<!-- Market Value -->
		 <xsl:variable name="MARKETVALUE"> 
				<xsl:call-template name="MARKETVALUE_IN_THOUSANDS"> 
					<xsl:with-param name="FACTOR_MARKETVAL" select="BOATS/BOAT/MARKETVALUE"/>
				</xsl:call-template>
		 </xsl:variable>
			
		<xsl:choose>
			<xsl:when test="$DED_DESC = '' or $DED_DESC ='0'">0</xsl:when>
			<xsl:when test="contains($DED_DESC ,'-')">  <!--  implies it is a percentage deductible -->
			
				<!-- Get the minimum deductible -->
				<xsl:variable name="MIN_DEDUCTIBLE">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID='DEDUCTIBLE']/ATTRIBUTES[@DESC=$DED_DESC]/@MINIMUMVALUE"/> 				
				</xsl:variable>
					
				<!-- Get the percentage -->
				<xsl:variable name="DED_PERCENTAGE">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID='DEDUCTIBLE']/ATTRIBUTES[@DESC=$DED_DESC]/@MINIMUMPERCENTAGE"/>					
				</xsl:variable>
								
						
				<!-- compute the deductible -->
				<xsl:variable name ="COMPUTED_DEDUCTIBLE">
						<xsl:value-of select="round(round($MARKETVALUE * $DED_PERCENTAGE) div 100.00) "/>
				</xsl:variable>
				
				<!-- Check if the computed value is less than Minimum Deductible. If less then send minimum deductible. -->
				<xsl:choose>
					<xsl:when test="$COMPUTED_DEDUCTIBLE &lt; $MIN_DEDUCTIBLE">
						<xsl:value-of select="$DED_PERCENTAGE"/>%-<xsl:value-of select="$MIN_DEDUCTIBLE"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$DED_PERCENTAGE"/>%-<xsl:value-of select="round($COMPUTED_DEDUCTIBLE)"/>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>						
			<xsl:otherwise>
				<xsl:value-of select="$DED_DESC"/> 
			</xsl:otherwise>		
		</xsl:choose>
		
	</xsl:template>	
	<xsl:template name="DEDUCTIBLE_FACTOR">
		<xsl:variable name="DED_DESC" select="BOATS/BOAT/DEDUCTIBLE"/> 
		
		<xsl:variable name="VAR_DEDUCTIBLE"> 
		<xsl:choose>
		<!--	<xsl:when test="$DED_DESC = '' or $DED_DESC ='0'">1.00</xsl:when> -->
			<xsl:when test="$DED_DESC = '' or $DED_DESC ='0'"></xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID='DEDUCTIBLE']/ATTRIBUTES[@DESC=$DED_DESC]/@FACTOR"/>					
			</xsl:otherwise>
		</xsl:choose>
	</xsl:variable>
		
		
		<xsl:if test="normalize-space($VAR_DEDUCTIBLE) = ''">
		<tr>
			<td class="midcolora"><li> deductible factor not found  (Path - "FACTOR[@ID='PDCREDIT']/NODE[@ID='DEDUCTIBLE']/ATTRIBUTES[@DESC=$DED_DESC]/@FACTOR") </li> </td>
		</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>

		<xsl:value-of select="$VAR_DEDUCTIBLE"/>

				
		
	</xsl:template>	
<!-- ============================================================================================ -->
<!--					Templates for Deductible (END)											  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					Templates for AGE (start)												  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
<!-- ============================================================================================ -->

	<xsl:template name="AGE_DISCOUNT_DISPLAY">
		<xsl:variable  name="P_AGE" select="BOATS/BOAT/AGE"/>
	
		<!-- FOR MAXIMUM AGE -->
		<xsl:variable name="VAR_MAX_AGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='AGEWATER']/ATTRIBUTES[@MAX = 'MAX_AGE']/@MINAGE"/>
		
		<xsl:choose> 
			<xsl:when test = "$P_AGE = ''">
				0
			</xsl:when>
			<xsl:when test ="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_JETSKI or  BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER " >
				0
			</xsl:when>
			<xsl:otherwise>						
				<xsl:variable name="P_AGE_CREDIT" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='AGEWATER']/ATTRIBUTES[@MINAGE &lt;= $P_AGE and @MAXAGE &gt;= $P_AGE]/@CREDIT"/>
				<xsl:choose>
					<xsl:when test="$P_AGE_CREDIT &gt; 0">
						Included
					</xsl:when>
					<xsl:otherwise>						
						0
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
 
	</xsl:template>	

	<xsl:template name="AGE_FACTOR">
		<xsl:variable  name="P_AGE" select="BOATS/BOAT/AGE"/>
	
		<!-- FOR MAXIMUM AGE -->
		<xsl:variable name="VAR_MAX_AGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='AGEWATER']/ATTRIBUTES[@MAX = 'MAX_AGE']/@MINAGE"/>
		
		<xsl:if test="normalize-space($VAR_MAX_AGE) = ''">
				<tr>
					<td class="midcolora"><li> Max. Age Factor  not found  (Path - "FACTOR[@ID='PDCREDIT']/NODE[@ID ='AGEWATER']/ATTRIBUTES[@MAX = 'MAX_AGE']/@MINAGE") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		 
	<xsl:variable name="VAR_AGE_FACTOR"> 
		<xsl:choose> 
			<xsl:when test = "$P_AGE = ''">
				1.00  
			</xsl:when>
			<xsl:when test ="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_JETSKI or  BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER " >
				1.00
			</xsl:when>
			<xsl:when test = "$P_AGE &gt;= $VAR_MAX_AGE">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='AGEWATER']/ATTRIBUTES[@MINAGE = $VAR_MAX_AGE]/@FACTOR"/>
			</xsl:when>		
			<xsl:otherwise>						
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='AGEWATER']/ATTRIBUTES[@MINAGE &lt;= $P_AGE and @MAXAGE &gt;= $P_AGE]/@FACTOR"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:variable>
 		<xsl:if test="normalize-space($VAR_AGE_FACTOR) = ''">
				<tr>
					<td class="midcolora"><li> Age Factor Discount not found  (Path - "FACTOR[@ID='PDCREDIT']/NODE[@ID ='AGEWATER']/ATTRIBUTES[@MINAGE = $VAR_MAX_AGE]/@FACTOR") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
	<xsl:value-of select="$VAR_AGE_FACTOR"/>

	</xsl:template>	
	
	
	
	<xsl:template name="AGE_CREDIT">
	
		<xsl:variable  name="P_AGE" select="BOATS/BOAT/AGE"/>
	
		<!-- FOR MAXIMUM AGE -->
		<xsl:variable name="VAR_MAX_AGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='AGEWATER']/ATTRIBUTES[@MAX = 'MAX_AGE']/@MINAGE"/>
		<xsl:variable name="VAR_CREDIT">
			<xsl:choose> 
				<xsl:when test = "$P_AGE = ''">	
				0			
				</xsl:when>
				<xsl:when test ="BOATS/BOAT/BOATTYPECODE = $BOATTYPE_WAVERUNNER or BOATS/BOAT/BOATSTYLECODE = $BOATSTYLE_JETSKI or  BOATS/BOAT/BOATTYPECODE = $BOATTYPE_MINI_JET or BOATS/BOAT/BOATSTYLECODE=$BOATSTYLE_TRAILER " >
				0
				</xsl:when>
				<xsl:when test = "$P_AGE &gt;= $VAR_MAX_AGE">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='AGEWATER']/ATTRIBUTES[@MINAGE = $VAR_MAX_AGE]/@CREDIT"/>
				</xsl:when>		
				<xsl:otherwise>						
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='AGEWATER']/ATTRIBUTES[@MINAGE &lt;= $P_AGE and @MAXAGE &gt;= $P_AGE]/@CREDIT"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		
		
		<xsl:choose>
			<xsl:when test="$VAR_CREDIT &gt; 0"><xsl:value-of select="$VAR_CREDIT"/></xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
 

<!-- ============================================================================================ -->
<!--					General Templates (END)											  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
<!-- ============================================================================================ -->

	<!-- template for converting market value in ciel thousands -->
	<xsl:template name="MARKETVALUE_IN_THOUSANDS"> 
		<xsl:param name="FACTOR_MARKETVAL"/>
	
		<xsl:variable  name="VAR1" select="$FACTOR_MARKETVAL"/>
		<xsl:variable  name="VAR2">
			<xsl:value-of select="$VAR1 mod 1000"/>
		</xsl:variable> 
		
		
		<xsl:choose>
			<xsl:when test="$VAR2 &gt; 0">
				<xsl:value-of select="$VAR1 + 1000 - $VAR2"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$VAR1"/>
			</xsl:otherwise>
		</xsl:choose>
		
	</xsl:template>
	
	<!-- To Display County of Operation -->
	<xsl:template name="COUNTY_OF_OPERATION_DISPLAY">	 
		<xsl:variable name="P_COUNTY_CREDIT">
			<xsl:call-template name="COUNTY_OPERATION_CHARGE"/>
		</xsl:variable>
		
		<xsl:choose>
			<xsl:when test="$P_COUNTY_CREDIT &gt; 0">
				Included
			</xsl:when>
			<xsl:otherwise>
				0
			</xsl:otherwise>
		</xsl:choose> 
	</xsl:template>
	
	<!-- To Display County of Operation Credit percentage --> 
	<xsl:template name="COUNTY_OPERATION_CHARGE">
		 
		<xsl:variable name="P_COUNTY_TERRITORY" select="BOATS/BOAT/TERRITORYDOCKEDIN"/>
		<xsl:choose>
			<xsl:when test="normalize-space($P_COUNTY_TERRITORY) !='' and BOATS/BOAT/BOATSTYLECODE !=$BOATSTYLE_TRAILER">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY_FACTOR']/NODE[@ID ='TERRITORYFACTOR']/ATTRIBUTES[@TERRITORY_CODE=$P_COUNTY_TERRITORY]/@CREDIT"/>				
			</xsl:when>
			<xsl:otherwise>
				0	
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<!-- To Display County of Operation territory factor--> 
	<xsl:template name="COUNTY_OPERATION_TERRITORY_FACTOR">
		 
		<xsl:variable name="P_COUNTY_TERRITORY" select="BOATS/BOAT/TERRITORYDOCKEDIN"/>
		<xsl:choose>
			<xsl:when test="normalize-space($P_COUNTY_TERRITORY) !='' and BOATS/BOAT/BOATSTYLECODE !=$BOATSTYLE_TRAILER">
				<xsl:variable name ="VAR_COUNTY_OPER_TER_FACTOR"> 
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY_FACTOR']/NODE[@ID ='TERRITORYFACTOR']/ATTRIBUTES[@TERRITORY_CODE=$P_COUNTY_TERRITORY]/@FACTOR"/>				
				</xsl:variable>
				
				<xsl:if test="normalize-space($VAR_COUNTY_OPER_TER_FACTOR) = ''">
					<tr>
						<td class="midcolora"><li> County Operation territory factor not found  (Path - "FACTOR[@ID='TERRITORY_FACTOR']/NODE[@ID ='TERRITORYFACTOR']/ATTRIBUTES[@TERRITORY_CODE=$P_COUNTY_TERRITORY]/@FACTOR") </li> </td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				 
			</xsl:when>
			<xsl:otherwise>
				1.00	
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>



<xsl:template name="MAIN_FOR_INPUT_XML"> 
 
		<xsl:if test="user:AllVerified(1) = 1">
			<b> Error Details of XML </b>
		</xsl:if>

		<xsl:for-each select="POLICY">   
			<xsl:call-template name="MAIN_FOR_INPUT_XML_POLICY_LEVEL"/>  
		</xsl:for-each>  
	
		<xsl:for-each select="BOATS/BOAT">   
			  <xsl:call-template name="MAIN_FOR_INPUT_XML_BOAT_LEVEL"/>   
		</xsl:for-each>  
	
		<xsl:for-each select="OPERATORS/OPERATOR">   
			<xsl:call-template name="MAIN_FOR_INPUT_XML_OPERATORS_LEVEL"/>  
		</xsl:for-each>  
				  
</xsl:template>


<!-- ******************************************************************************************************************** -->
<!-- *************************    INPUT XML VERIFICATION AT POLICY LEVEL ************************************************** -->
<!-- ******************************************************************************************************************** -->


<xsl:template name="MAIN_FOR_INPUT_XML_POLICY_LEVEL"> 
  
<xsl:variable name="VAR_NEWBUSINESS" select="normalize-space(NEWBUSINESS)"/> 
<xsl:if test="$VAR_NEWBUSINESS != 'N' and $VAR_NEWBUSINESS !='Y'">
	<tr>
		<td class="midcolora"> <li> The Node for newbusiness factor is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
</xsl:if>


<xsl:variable name="VAR_TERRITORYCODES" select="TERRITORYCODE"/> 
<xsl:if test="normalize-space($VAR_TERRITORYCODES) = ''">
	<tr>
		<td class="midcolora"> <li> The Node for Territory Code is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
</xsl:if>


<xsl:variable name="VAR_INSURANCESCORE" select="INSURANCESCORE"/> 
<xsl:variable name="VAR_INSURANCESCORE1" select="INSURANCESCOREDIS"/> 

<xsl:if test="normalize-space($VAR_INSURANCESCORE) = '' or normalize-space($VAR_INSURANCESCORE1) = ''">
	<tr>
		<td class="midcolora"> <li>The Node for insurance score is blank </li> </td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
</xsl:if>

 

 	<xsl:variable name="VAR_STATENAME" select="STATENAME"/> 
		<xsl:if test="normalize-space($VAR_STATENAME) = ''">
		<tr>
			<td class="midcolora"> <li> The Node for state name is blank </li></td>
		</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>


		<xsl:variable name="VAR_QUOTEEFFDATE" select="QUOTEEFFDATE"/> 
			<xsl:if test="normalize-space($VAR_QUOTEEFFDATE) = ''">
				<tr>
					<td class="midcolora"> <li> The node for quote effective date is blank </li></td>
				</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>


		
		<xsl:variable name="VAR_QUOTEEXPDATE" select="QUOTEEXPDATE"/> 
			<xsl:if test="normalize-space($VAR_QUOTEEXPDATE) = ''">
				<tr>
					<td class="midcolora"> <li> The node for quote expiry date is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>


	 
		<xsl:variable name="VAR_POLICYTERMS" select="POLICYTERMS"/> 
			<xsl:if test="normalize-space($VAR_POLICYTERMS) = ''">
				<tr>
					<td class="midcolora"> <li>The node for policy term is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
	<xsl:variable name="VAR_RENEWAL" select="normalize-space(RENEWAL)"/> 
	<xsl:if test="$VAR_RENEWAL != 'N' and $VAR_RENEWAL !='Y'">
	<tr>
		<td class="midcolora"> <li> The Node for renewal factor is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>
			
	<xsl:variable name="VAR_ATTACHTOWOLVERINE" select="normalize-space(ATTACHTOWOLVERINE)"/> 
	<xsl:if test="$VAR_ATTACHTOWOLVERINE != 'N' and $VAR_ATTACHTOWOLVERINE !='Y'">
	<tr>
		<td class="midcolora"> <li> The Node for attachtowolverine factor is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>
		
	
	<xsl:variable name="VAR_BOATHOMEDISC" select="normalize-space(BOATHOMEDISC)"/> 
	<xsl:if test="$VAR_BOATHOMEDISC != 'N' and $VAR_BOATHOMEDISC !='Y'">
	<tr>
		<td class="midcolora"> <li> The Node for boathomedisc factor is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>
	  
	 
</xsl:template>

<!-- ******************************************************************************************************************** -->
<!-- *************************    INPUT XML VERIFICATION AT BOAT LEVEL ************************************************** -->
<!-- ******************************************************************************************************************** -->

<xsl:template name ="MAIN_FOR_INPUT_XML_BOAT_LEVEL">

	<xsl:variable name="VAR_BOATROWID" select="normalize-space(BOATROWID)"/> 
	<xsl:if test="$VAR_BOATROWID = ''">
		<tr>
			<td class="midcolora"> <li> The Node for boatrowid factor is blank </li></td>
		</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>
	
	<xsl:variable name="VAR_BOATTYPE" select="normalize-space(BOATTYPE)"/> 
	<xsl:if test="$VAR_BOATTYPE = ''">
	<tr>
		<td class="midcolora"> <li> The Node for boattype  is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>
		
	<xsl:variable name="VAR_BOATTYPECODE" select="normalize-space(BOATTYPECODE)"/> 
	<xsl:if test="$VAR_BOATTYPECODE = ''">
	<tr>
		<td class="midcolora"> <li> The Node for boat type code is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>
	
	<xsl:variable name="VAR_YEAR" select="normalize-space(YEAR)"/> 
	<xsl:if test="$VAR_YEAR = ''">
	<tr>
		<td class="midcolora"> <li> The Node for year is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>
			 
	
	<xsl:variable name="VAR_AGE" select="normalize-space(AGE)"/> 
	<xsl:if test="$VAR_AGE = ''">
	<tr>
		<td class="midcolora"> <li> The Node for age is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>
			
	
	<xsl:variable name="VAR_MODEL" select="normalize-space(MODEL)"/> 
	<xsl:if test="$VAR_MODEL = ''">
	<tr>
		<td class="midcolora"> <li> The Node for model is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>	 
		
	<xsl:variable name="VAR_MANUFACTURER" select="normalize-space(MANUFACTURER)"/> 
	<xsl:if test="$VAR_MANUFACTURER = ''">
	<tr>
		<td class="midcolora"> <li> The Node for manufacturer is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>
			
		<xsl:variable name="VAR_SERIALNUMBER" select="normalize-space(SERIALNUMBER)"/> 
	<xsl:if test="$VAR_SERIALNUMBER = ''">
	<tr>
		<td class="midcolora"> <li> The Node for serial number is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>	
			
		
	<xsl:variable name="VAR_LENGTH" select="normalize-space(LENGTH)"/> 
	<xsl:if test="$VAR_LENGTH = ''">
	<tr>
		<td class="midcolora"> <li> The Node for boat length is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>	
	
 	
	
		
	<xsl:variable name="VAR_HORSEPOWER" select="normalize-space(HORSEPOWER)"/> 
	<xsl:if test="$VAR_HORSEPOWER = ''">
	<tr>
		<td class="midcolora"> <li> The Node for horsepower of boat engine is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>	
	
		
	<xsl:variable name="VAR_CAPABLESPEED" select="normalize-space(CAPABLESPEED)"/> 
	<xsl:if test="$VAR_CAPABLESPEED = ''">
	<tr>
		<td class="midcolora"> <li> The Node for capable speed of boat is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>	
			
	
		
	<xsl:variable name="VAR_WATERS" select="normalize-space(WATERS)"/> 
	<xsl:if test="$VAR_WATERS = ''">
	<tr>
		<td class="midcolora"> <li> The Node for waters is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>	
	
			
	<xsl:variable name="VAR_WATERSCODE" select="normalize-space(WATERSCODE)"/> 
	<xsl:if test="$VAR_WATERSCODE = ''">
	<tr>
		<td class="midcolora"> <li> The Node for waters code  is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>
	
			
	
	<xsl:variable name="VAR_MARKETVALUE" select="normalize-space(MARKETVALUE)"/> 
	<xsl:if test="$VAR_MARKETVALUE = ''">
	<tr>
		<td class="midcolora"> <li> The Node for market value is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>
	

	<xsl:variable name="VAR_CONSTRUCTION" select="normalize-space(CONSTRUCTION)"/> 
	<xsl:if test="$VAR_CONSTRUCTION = '' and normalize-space(BOATTYPECODE) = 'W' ">
	<tr>
		<td class="midcolora"> <li> The Node for construction is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>
	
	<xsl:variable name="VAR_CONSTRUCTIONCODE" select="normalize-space(CONSTRUCTIONCODE)"/> 
		<xsl:if test="$VAR_CONSTRUCTIONCODE = '' and normalize-space(BOATTYPECODE) = 'W' ">
	<tr>
		<td class="midcolora"> <li> The Node for construction code is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>
	
	<xsl:variable name="VAR_COUNTYOFOPERATION" select="normalize-space(COUNTYOFOPERATION)"/> 
	<xsl:if test="$VAR_COUNTYOFOPERATION = ''">
	<tr>
		<td class="midcolora"> <li> The Node for county of operation code is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>

	<xsl:variable name="VAR_DIESELENGINE" select="normalize-space(DIESELENGINE)"/> 
	<xsl:if test="$VAR_DIESELENGINE = ''">
	<tr>
		<td class="midcolora"> <li> The Node for diesel engine is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>
			
	 
	
		
			
		<xsl:variable name="VAR_SHORESTATION" select="normalize-space(SHORESTATION)"/> 
			<xsl:if test="$VAR_SHORESTATION != 'Y' and $VAR_SHORESTATION != 'N'">
				<tr>
					<td class="midcolora"><li>The node for shorestation is either blank or not in (Y/N) format</li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
		<xsl:variable name="VAR_HALONFIRE" select="normalize-space(HALONFIRE)"/> 
			<xsl:if test="$VAR_HALONFIRE != 'Y' and $VAR_HALONFIRE != 'N'">
				<tr>
					<td class="midcolora"><li>The node for halon fire is either blank or not in (Y/N) format</li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
	
		
		<xsl:variable name="VAR_LORANNAVIGATIONSYSTEM" select="normalize-space(LORANNAVIGATIONSYSTEM)"/> 
			<xsl:if test="$VAR_LORANNAVIGATIONSYSTEM != 'Y' and $VAR_LORANNAVIGATIONSYSTEM != 'N'">
				<tr>
					<td class="midcolora"><li>The node for lorannavigation system is either blank or not in (Y/N) format</li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
		<xsl:variable name="VAR_DUALOWNERSHIP" select="normalize-space(DUALOWNERSHIP)"/> 
			<xsl:if test="$VAR_DUALOWNERSHIP != 'Y' and $VAR_DUALOWNERSHIP != 'N'">
				<tr>
					<td class="midcolora"><li>The node for dualownership is either blank or not in (Y/N) format</li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
					
			
	
		<xsl:variable name="VAR_REMOVESAILBOAT" select="normalize-space(REMOVESAILBOAT)"/> 
			<xsl:if test="$VAR_REMOVESAILBOAT != 'Y' and $VAR_REMOVESAILBOAT != 'N'">
				<tr>
					<td class="midcolora"><li>The node for removesail boat is either blank or not in (Y/N) format</li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
							
			
		<xsl:variable name="VAR_MULTIBOATCREDIT" select="normalize-space(MULTIBOATCREDIT)"/> 
			<xsl:if test="$VAR_MULTIBOATCREDIT != 'Y' and $VAR_MULTIBOATCREDIT != 'N'">
				<tr>
					<td class="midcolora"><li>The node for multiboat credit  is either blank or not in (Y/N) format</li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>		

			
		<xsl:variable name="VAR_ISGRAND" select="normalize-space(ISGRAND)"/> 
			<xsl:if test="$VAR_ISGRAND != 'Y' and $VAR_ISGRAND != 'N'">
				<tr>
					<td class="midcolora"><li>The node for isgrand is either blank or not in (Y/N) format</li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>		

			
				
	<xsl:variable name="VAR_BOATSTYLECODE" select="normalize-space(BOATSTYLECODE)"/> 
	<xsl:if test="$VAR_BOATSTYLECODE = ''">
	<tr>
		<td class="midcolora"> <li> The Node for boat style is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>
	
		
	<xsl:variable name="VAR_PERSONALLIABILITY" select="normalize-space(PERSONALLIABILITY)"/> 
	<xsl:if test="$VAR_PERSONALLIABILITY = ''">
	<tr>
		<td class="midcolora"> <li> The Node for personalliability  is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>

	<xsl:variable name="VAR_MEDICALPAYMENT" select="normalize-space(MEDICALPAYMENT)"/> 
	<xsl:if test="translate($VAR_MEDICALPAYMENT,' ','') = 'NOCOVERAGE'">
	<tr>
		<td class="midcolora"> <li> The Node for medical payment  is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>

	<xsl:variable name="VAR_MEDICALPAYMENTSOTHER" select="normalize-space(MEDICALPAYMENTSOTHER)"/> 
	<xsl:if test="translate($VAR_MEDICALPAYMENTSOTHER,' ','') = 'NOCOVERAGE'">
	<tr>
		<td class="midcolora"> <li> The Node for medical payments to other is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>
			
			
			
	<xsl:variable name="VAR_MEDICALPAYMENTSOTHERLIMIT" select="normalize-space(MEDICALPAYMENTSOTHERLIMIT)"/> 
	<xsl:if test="translate($VAR_MEDICALPAYMENTSOTHERLIMIT,' ','') = 'NOCOVERAGE'">
	<tr>
		<td class="midcolora"> <li> The Node for medical payment limit is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>
	
	<xsl:variable name="VAR_UNINSUREDBOATERS" select="normalize-space(UNINSUREDBOATERS)"/> 
		 <xsl:if test="translate($VAR_UNINSUREDBOATERS,' ','') = 'NOCOVERAGE'">  
	<tr>
		<td class="midcolora"> <li> The Node for uninsuredboaters is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>
	
	
	<xsl:variable name="VAR_UNATTACHEDEQUIPMENT" select="normalize-space(UNATTACHEDEQUIPMENT)"/> 
	<xsl:if test="$VAR_UNATTACHEDEQUIPMENT= ''">
	<tr>
		<td class="midcolora"> <li> The Node for unattached equipment is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>
			
		
	<xsl:variable name="VAR_UNATTACHEDEQUIPMENT_DEDUCTIBLE" select="normalize-space(UNATTACHEDEQUIPMENT_DEDUCTIBLE)"/> 
	<xsl:if test="$VAR_UNATTACHEDEQUIPMENT_DEDUCTIBLE= ''">
	<tr>
		<td class="midcolora"> <li> The Node for unattachedequipment deductible is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>	
			
			
	<xsl:variable name="VAR_TERRITORYDOCKEDIN" select="normalize-space(TERRITORYDOCKEDIN)"/> 
	<xsl:if test="$VAR_TERRITORYDOCKEDIN = ''">
	<tr>
		<td class="midcolora"> <li> The Node for territory docked in is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>	
	
		
	<xsl:variable name="VAR_STATEDOCKEDIN" select="normalize-space(STATEDOCKEDIN)"/> 
	<xsl:if test="$VAR_STATEDOCKEDIN = ''">
	<tr>
		<td class="midcolora"> <li> The Node for state docked in is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>	
		
		
	<xsl:variable name="VAR_COUNTYCODE" select="normalize-space(COUNTYCODE)"/> 
	<xsl:if test="$VAR_COUNTYCODE = ''">
	<tr>
		<td class="midcolora"> <li> The Node for county code is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>	
		
		
		<xsl:variable name="VAR_AV100" select="normalize-space(AV100)"/> 
			<xsl:if test="$VAR_AV100 != 'Y' and $VAR_AV100 != 'N'">
				<tr>
					<td class="midcolora"><li>The node for AV100 is either blank or not in (Y/N) format</li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
					
	
		<xsl:variable name="VAR_OP900" select="normalize-space(OP900)"/> 
			<xsl:if test="$VAR_OP900 != 'Y' and $VAR_OP900 != 'N'">
				<tr>
					<td class="midcolora"><li>The node for OP900 is either blank or not in (Y/N) format</li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
		<xsl:variable name="VAR_OP720" select="normalize-space(OP720)"/> 
			<xsl:if test="$VAR_OP720 != 'Y' and $VAR_OP720 != 'N'">
				<tr>
					<td class="midcolora"><li>The node for OP720 is either blank or not in (Y/N) format</li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
			
						
			<xsl:variable name="VAR_DEDUCTIBLE" select="normalize-space(DEDUCTIBLE)"/> 
	<xsl:if test="$VAR_DEDUCTIBLE = ''">
	<tr>
		<td class="midcolora"> <li> The Node for deductible is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
	</xsl:if>	
		 
	
	
</xsl:template>

<!-- ******************************************************************************************************************** -->
<!-- *************************    INPUT XML VERIFICATION AT OPERATORS LEVEL ************************************************** -->
<!-- ******************************************************************************************************************** -->

<xsl:template name ="MAIN_FOR_INPUT_XML_OPERATORS_LEVEL">

			 
			<xsl:variable name="VAR_OPERATORFNAME" select="normalize-space(OPERATORFNAME)"/> 
			<xsl:if test="$VAR_OPERATORFNAME = ''">
			<tr>
				<td class="midcolora"> <li> The Node for operator first name  is blank </li></td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>

			 

			 
			<xsl:variable name="VAR_OPERATORLNAME" select="normalize-space(OPERATORLNAME)"/> 
			<xsl:if test="$VAR_OPERATORLNAME = ''">
			<tr>
				<td class="midcolora"> <li> The Node for operator last name  is blank </li></td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>


			 
			<xsl:variable name="VAR_BIRTHDATE" select="normalize-space(BIRTHDATE)"/> 
			<xsl:if test="$VAR_BIRTHDATE = ''">
			<tr>
				<td class="midcolora"> <li> The Node for birthdate  is blank </li></td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>

			 
			<xsl:variable name="VAR_GENDER" select="normalize-space(GENDER)"/> 
			<xsl:if test="$VAR_GENDER = ''">
			<tr>
				<td class="midcolora"> <li> The Node for gender of driver is blank </li></td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>

			 
			 
			<xsl:variable name="VAR_AGEOFDRIVER" select="normalize-space(AGEOFDRIVER)"/> 
			<xsl:if test="$VAR_AGEOFDRIVER = ''">
			<tr>
				<td class="midcolora"> <li> The Node for age of driver is blank </li></td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>

			 
			<xsl:variable name="VAR_SUMOFVIOLATIONPOINTS" select="normalize-space(SUMOFVIOLATIONPOINTS)"/> 
			<xsl:if test="$VAR_SUMOFVIOLATIONPOINTS = ''">
			<tr>
				<td class="midcolora"> <li> The Node for sumofviolationpoints factor is blank </li></td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>

			
			<xsl:variable name="VAR_SUMOFACCIDENTPOINTS" select="normalize-space(SUMOFACCIDENTPOINTS)"/> 
			<xsl:if test="$VAR_SUMOFACCIDENTPOINTS = '' ">
			<tr>
				<td class="midcolora"> <li> The Node for sum of accident points is blank </li></td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>

 
</xsl:template>
 


 	 
</xsl:stylesheet>	
	
	

 
	
	
	
	
	
	