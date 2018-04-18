<!-- ==================================================================================================
File Name			:	DiscountAndSurcharge_Watercraft.xsl
Purpose				:	To display Discount and Surcharge applicable for Watercraft on application basis
Name				:	Praveen Singh
Date				:	17 jan 2006  
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
	<xsl:variable name="LIABILTY">(Liability)</xsl:variable>
	<xsl:variable name="PHYSICAL">(Physical Damage)</xsl:variable>
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
	<!-- ================================================================================= -->
	<xsl:template match="QUICKQUOTE">
		<table border="0" align="center" width='90%'>
			
			<xsl:value-of select="user:SetFlag(1)" /> 
			<xsl:call-template name="POLICY_LEVEL" />
			<xsl:call-template name="FinalDisplay" />
			
 			<xsl:value-of select="user:SetFlag(1)" /> 
			<xsl:call-template name="BOAT_LEVEL" />
		    <xsl:call-template name="FinalDisplay" />
			
			<xsl:value-of select="user:SetFlag(1)" /> 
			<xsl:call-template name="DRIVER_LEVEL" />
			<!-- <tr class="midcolora"><td> <xsl:call-template name="FinalDisplay" /></td></tr>  -->
			<xsl:call-template name="FinalDisplay" />
			
			
		</table>
	</xsl:template>
	
	
	
	<!-- ================Template Level Wise ================================================ -->
	
	<!-- Discounts and surcharges on policy level -->
	
		<xsl:template name="POLICY_LEVEL">
		<table class="tableeffectTopHeader" cellSpacing="0" cellPadding="0" width="90%" align="center">			
			<tr>
     			<td width="70%" class="midcolora"><b><u>Policy Level</u></b></td>
				<td width="30%" class="midcolora"><b>Discount / Surcharge</b></td>
			</tr>
			
				<!--<xsl:call-template name="DISCOUNT_WOLVERINE"></xsl:call-template>
				Insurance Score Discount 16 feb 2006-->
				<xsl:call-template name="INSURANCESCORE_DISPLAY"></xsl:call-template>
				<xsl:call-template name="MULTIPOLICY_DISPLAY"></xsl:call-template>
				
			
			<tr><td height="5%"><p></p><br></br></td></tr>
			</table>
		</xsl:template>
	
	
	
	<!-- Discounts and surcharges on BOAT level -->
	<xsl:template name="BOAT_LEVEL">
	
	
	<table class="tableeffectTopHeader" cellSpacing="0" cellPadding="0" width="90%" align="center">	
		<tr class="midcolora">
			<td colspan="2"><b><u> Boat Level </u></b></td>
		</tr>
		
		<xsl:for-each select="BOATS/BOAT">
		<tr><td><br></br></td></tr>
			<tr>				
				<td width="70%" class="midcolora">
					<b> <xsl:value-of select="@ID" />. <xsl:call-template name="BOAT_DESCRIPTION"/>   </b>
				</td>
				<td width="30%" class="midcolora">
					<b> Discount / Surcharge </b>
				</td>
			</tr>
			
			 <xsl:choose>
			 <!--MJB W PWT JS-->
			<xsl:when test="BOATTYPECODE ='MJB' or BOATTYPECODE ='W' or BOATTYPECODE ='PWT' or BOATTYPECODE = 'JS'">
			 </xsl:when>
			<xsl:otherwise>
					 <xsl:call-template name="DIESEL_ENGINE"></xsl:call-template>
			
					 <xsl:call-template name="HALON_FIRE"></xsl:call-template>
				
					 <xsl:call-template name="LORAN_GPS"></xsl:call-template>
				
					 <xsl:call-template name="SHORE_STATION"></xsl:call-template>
						
					 <xsl:call-template name="FIBRE_GLASS"></xsl:call-template>
						
					 <xsl:call-template name="REMOVE_SAILBOAT"></xsl:call-template>
					
					 <xsl:call-template name="MULTI_BOAT"></xsl:call-template>
				 <!--<xsl:call-template name="DEDUCTIBLE_DISCOUNT"></xsl:call-template>-->
					 
					<xsl:choose>
						<xsl:when test="ISGRAND='N'">
							<xsl:call-template name="AGE_WATERCRAFT"></xsl:call-template>
						</xsl:when>		
					</xsl:choose>
					</xsl:otherwise>	
				</xsl:choose>
				<xsl:call-template name="WOODEN_BOAT"></xsl:call-template>
		        <xsl:call-template name="DUAL_OWNERSHIP"></xsl:call-template>
			    <!-- Applies Only To Mini Jet Boat -->
			    <xsl:call-template name="MINI_JET_BOAT"></xsl:call-template>
			    <xsl:call-template name="WAVERUNNER_SURCHARGE"></xsl:call-template>
			    <xsl:call-template name="LENGTH_26FEET_SPEED65"></xsl:call-template>
			    <xsl:call-template name="LENGTH_26FEET"></xsl:call-template>
			    <xsl:call-template name="COUNTY_OPERATION_CHARGE"></xsl:call-template>
			    
			    

				
				
				
			
			
			
		</xsl:for-each>
		<tr><td height="5%"><br></br></td></tr>
		</table>
		
	</xsl:template>
	
		<!--  Multi Policy discount  -->
	
	<xsl:template name="MINI_JET_BOAT">
	 <xsl:variable name="VARMINIJET">
	  <xsl:choose>
           	<xsl:when test="BOATTYPECODE = 'MJB'">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='MINIJETBOAT']/ATTRIBUTES/@CREDIT" />
			</xsl:when>
		 <xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VARMINIJET &gt; 0">
			<tr class="midcolora">
				<td><li>Surcharge for Mini Jet Boat</li></td>
				<td>+<xsl:value-of select="$VARMINIJET" />% </td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:if>		
 </xsl:template>
	<!--  Multi Policy discount  -->
 
 <xsl:template name="AGE_WATERCRAFT">
            <xsl:variable name="VARBOATAGE">
				<xsl:value-of select="AGE"/>
			</xsl:variable>
			
     <xsl:variable name="VAR_AGE_BOAT">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='AGEWATER']/ATTRIBUTES[@MINAGE &lt;= $VARBOATAGE and @MAXAGE &gt;= $VARBOATAGE]/@CREDIT" />
			
				<!--<xsl:when test="$VARBOATCOUNT &gt; 1">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='MULTIBOAT']/ATTRIBUTES/@CREDIT" />
			-->
		</xsl:variable>
		<xsl:if test="$VAR_AGE_BOAT &gt; 0">
			<tr class="midcolora">
				<td><li>Discount for Age of Watercraft</li></td>
				<td>-<xsl:value-of select="$VAR_AGE_BOAT" />% </td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:if>		
 </xsl:template>
<!--waverunners, jetskis (with or without lift bar) or mini-jetboats...there is only 1 discount available            
to them at the policy level and that is Insurance Score-->

<xsl:template name ="MULTIPOLICY_DISPLAY">

		<xsl:variable name="VAR_MULTIPOLICY"> 
		 <xsl:choose>
				<xsl:when test="POLICY/MULTIPOLICYAUTOHOMEDISCOUNT = 'Y' and POLICY/MULTIPOLICYDISAPPLICABLE &gt;= 1">
				   <xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='MULTIPOLICY']/ATTRIBUTES/@CREDIT"/> 		
			</xsl:when>
		  <xsl:otherwise>0</xsl:otherwise>
		 </xsl:choose>
		</xsl:variable>

    	    <xsl:if test="$VAR_MULTIPOLICY &gt; 0">
				<tr>
						<td class="midcolora"><li>Discount For Multi Policy </li></td>
						<td class="midcolora">-<xsl:value-of select="$VAR_MULTIPOLICY" />% </td>
						
				</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
				</xsl:if>
		
		
	
</xsl:template>
	
	<!-- Discounts and surcharges on Driver Level level -->
	<xsl:template name="DRIVER_LEVEL">
	<table class="tableeffectTopHeader" cellSpacing="1" cellPadding="1" width="90%" align="center">
		
		<tr class="midcolora">
			<td colspan="2">
				<b>
					<U> Operator Level </U>
				</b>
			</td>
		</tr>
		<xsl:for-each select="OPERATORS/OPERATOR">
		<tr><td height="5%"><br></br></td></tr>
			<tr>
				<td width="70%" class="midcolora">
					<b><xsl:value-of select="@ID" />. <xsl:call-template name="OPERATOR_DESC" />   </b>
				</td>
					<td width="30%" class="midcolora">
					<b> Discount / Surcharge </b>
				</td>
			</tr>
			<xsl:call-template name="EXPERIENCE_CREDIT"></xsl:call-template>
		</xsl:for-each>
	 </table>	
	</xsl:template>
	<!-- ============================================================================= -->
	<!--Insurance Score Discount : 16 feb 2006-->
   <xsl:template name ="INSURANCESCORE_DISPLAY">
    <xsl:variable name="INS" select="POLICY/INSURANCESCOREDIS"></xsl:variable>
    
    
	<xsl:variable name="VAR_INS_SCORE" > 
	<xsl:choose>
		<xsl:when test = "$INS = 0">
			<xsl:value-of select= "$INS"></xsl:value-of>
		</xsl:when>
		<xsl:when test = "$INS = ''">
			1.00
		</xsl:when>
		<xsl:when test = "$INS = 'N'">
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MINSCORE = 'N']/@CREDIT"/>
		</xsl:when>
		<xsl:when test = "$INS &gt;= 751">
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MINSCORE = 751]/@CREDIT"/>
		</xsl:when>		
		<xsl:otherwise>						
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCESCORECREDIT']/NODE[@ID ='INSURANCE']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@CREDIT"/>
		</xsl:otherwise>
	</xsl:choose>
	</xsl:variable>
	
	
	<xsl:if test="$VAR_INS_SCORE &gt; 0">
		<tr class="midcolora">
			<td><li> Insurance Score Discount</li> </td>
			<td width="212">-<xsl:value-of select="$VAR_INS_SCORE" />%</td>
		</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
	</xsl:if>
	
</xsl:template>
	
	<!--End Insurance Score-->
	<!-- ============================================================================= -->
	<xsl:template name="DISCOUNT_WOLVERINE">
		<xsl:variable name="VAR_DISCOUNT_WOLVERINE">
			<xsl:choose>
				<xsl:when test="POLICY/BOATHOMEDISC = 'Y'">
				15
			</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_DISCOUNT_WOLVERINE &gt; 0">
			
			<tr class="midcolora">
			   	<!--<td ><li>Discount for Wolverine Homeowners</li></td>-->
			   	<td ><li>Boat-Home Discount</li></td>
				<td width="212">-<xsl:value-of select="$VAR_DISCOUNT_WOLVERINE" />%</td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================= -->
	<xsl:template name="DIESEL_ENGINE">
		<xsl:variable name="VAR_DIESEL_ENGINE">
			<xsl:choose>
				<xsl:when test="DIESELENGINE = 'Y'">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='DIESELENGINECRAFT']/ATTRIBUTES/@CREDIT" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_DIESEL_ENGINE &gt; 0">
			<tr class="midcolora">
				<td><li> Discount - Diesel Engine </li></td>
				<td>-<xsl:value-of select="$VAR_DIESEL_ENGINE" />%</td>
			</tr>			
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================= -->
	<xsl:template name="HALON_FIRE">
		<xsl:variable name="VAR_HALON_FIRE">
			<xsl:choose>
				<xsl:when test="HALONFIRE = 'Y'">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='FIREEXTINGUISHER']/ATTRIBUTES/@CREDIT" />
				</xsl:when>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_HALON_FIRE &gt; 0">
			 
			<tr class="midcolora">
				<td><li>	Discount - Halon Fire Extinguishing System	</li></td>
				<td>-<xsl:value-of select="$VAR_HALON_FIRE" />% </td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================= -->
	<xsl:template name="LORAN_GPS">
		<xsl:variable name="VAR_LORAN_GPS">
			<xsl:choose>
				<xsl:when test="LORANNAVIGATIONSYSTEM = 'Y'">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='NAVIGATIONSYSTEM']/ATTRIBUTES/@CREDIT" />
				</xsl:when>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_LORAN_GPS &gt; 0">
			<tr class="midcolora">
				<td><li>Discount - Loran Navigation System, GPS or Depth Sounder and Ship to Shore Radio </li></td>
				<td>-<xsl:value-of select="$VAR_LORAN_GPS" />% </td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
			
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================= -->
	<xsl:template name="SHORE_STATION">
		<xsl:variable name="VAR_SHORE_STATION">
			<xsl:choose>
				<xsl:when test="SHORESTATION = 'Y'">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='SHORESTATIONCREDIT']/ATTRIBUTES/@CREDIT" />
				</xsl:when>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_SHORE_STATION &gt; 0">
			<tr class="midcolora">
				<td><li>Discount - Shore Station  </li></td>
				<td>-<xsl:value-of select="$VAR_SHORE_STATION" />%</td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================= -->
	<xsl:template name="EXPERIENCE_CREDIT">
		<xsl:variable name="VAR_EXPERIENCE_CREDIT">
			<xsl:choose>
				<xsl:when test="BOATDRIVEDAS = 'PRINCIPAL' and (COASTGUARDAUXILARYCOURSE = 'Y' or HAS_5_YEARSOPERATOREXPERIENCE = 'Y')">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='EXPNAVIGATIONCOURSE']/ATTRIBUTES/@CREDIT" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_EXPERIENCE_CREDIT &gt; 0">
		
			<tr class="midcolora">
				<td><li> Discount for 5 Years Experience or Navigation Course</li></td>   
				<td width="212">-<xsl:value-of select="$VAR_EXPERIENCE_CREDIT" />% </td>   
			</tr>
			
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================= -->
	<xsl:template name="MULTI_BOAT">
	
		<xsl:variable name="VAR_MULTI_BOAT">
			<xsl:variable name="VARBOATCOUNT">
				<xsl:value-of select="BOATCOUNT" />
			</xsl:variable>
			<xsl:choose>
				<xsl:when test="MULTIBOATCREDIT = 'Y' and BOATTYPECODE != 'OTHER' and BOATTYPECODE != 'MJB' ">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='MULTIBOAT']/ATTRIBUTES/@CREDIT" />
				</xsl:when>
				<!--<xsl:when test="$VARBOATCOUNT &gt; 1">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='MULTIBOAT']/ATTRIBUTES/@CREDIT" />
				</xsl:when>-->
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_MULTI_BOAT &gt; 0">
			<tr class="midcolora">
				<td><li>Discount for Multi-Boat</li></td>
				<td>-<xsl:value-of select="$VAR_MULTI_BOAT" />% </td>
				
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================= -->
	<!--Modified WOOD surcharge: 26 feb 2006-->
	<xsl:template name="WOODEN_BOAT">
		<xsl:variable name="VAR_WOODEN_BOAT">
			<xsl:choose>
				<xsl:when test="CONSTRUCTION = 'WOOD' and AGE &gt; 10">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='WOODENBOAT']/ATTRIBUTES/@SURCHARGE" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_WOODEN_BOAT &gt; 0">
		
			<tr class="midcolora">
				<td><li>Surcharge for Wooden Boat. <xsl:value-of select="$PHYSICAL"></xsl:value-of> </li></td>
				<td>+<xsl:value-of select="$VAR_WOODEN_BOAT" />%</td>
			</tr>
			
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================= -->
	<xsl:template name="DUAL_OWNERSHIP">
		<xsl:variable name="VAR_DUAL_OWNERSHIP">
			<xsl:choose>
				<xsl:when test="DUALOWNERSHIP = 'Y'  ">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDDUALOWNERSHIP']/ATTRIBUTES/@SURCHARGE" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		
		<xsl:if test="$VAR_DUAL_OWNERSHIP &gt; 0"> 
			<tr class="midcolora">
				<td><li> Surcharge for Dual Ownership for Unrelated Parties. <xsl:value-of select="$LIABILTY"></xsl:value-of> </li></td>
				<td>+<xsl:value-of select="$VAR_DUAL_OWNERSHIP" />% </td>
			</tr>
			<tr class="midcolora">
				<td><li> Surcharge for Dual Ownership for Unrelated Parties. <xsl:value-of select="$PHYSICAL"></xsl:value-of> </li></td>
				<td>+<xsl:value-of select="$VAR_DUAL_OWNERSHIP" />% </td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:if>
		
	</xsl:template>
	<!-- ============================================================================= -->
	<!--Modified the Waverunner Surcharge-->
	<xsl:template name="WAVERUNNER_SURCHARGE">
		<!--<xsl:variable name="VAR_WAVERUNNER">-->
		<xsl:variable name="VAR_DUAL_OWNERSHIP">
			<xsl:choose>
				<xsl:when test="BOATTYPE='WAVERUNNER'">
					 <!-- <xsl:value-of select="10" />fixed value of 10 %-->
					 10
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_DUAL_OWNERSHIP &gt; 0">
			<tr class="midcolora">
				<td><li>Surcharge for Waverunner</li></td>
				<td>+<xsl:value-of select="round($VAR_DUAL_OWNERSHIP)"/>%</td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================= -->
	<!--Set BOATTYPE and LENGHT  
	    Any boat over 26 feet length will be charged 10%, 
	    This should appear as surcharge in discount/surcharge screen, it is only applicable 
	    to PD (Physical Damage) for boat sytles - Inboard, Inboard/Outboard and Sailboat-->
	<xsl:template name="LENGTH_26FEET">
	  		<xsl:variable name="VAR_LENGTH_26FEET">
			<xsl:choose>
				<xsl:when test="LENGTH &gt; 26 and ( BOATSTYLECODE = 'IO'  or BOATSTYLECODE='S' or BOATSTYLECODE='I')">
		<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDBOATLENGTH']/ATTRIBUTES/@SURCHARGE" />		  
			</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_LENGTH_26FEET &gt; 0 ">
			<tr class="midcolora">
				<td ><li>Surcharge for Length over 26 Feet. <xsl:value-of select="$PHYSICAL"></xsl:value-of>  </li></td>
				<td>+<xsl:value-of select="round($VAR_LENGTH_26FEET)"/>% </td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:if>
	</xsl:template>
	<xsl:template name="COUNTY_OPERATION_CHARGE">
		 
		<xsl:variable name="P_COUNTY_TERRITORY" select="TERRITORYDOCKEDIN"/>
		<xsl:variable name="COUNTYOPERATIONCHARGE">
			<xsl:choose>
				<xsl:when test="normalize-space($P_COUNTY_TERRITORY) !='' and BOATSTYLECODE != 'T'">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY_FACTOR']/NODE[@ID ='TERRITORYFACTOR']/ATTRIBUTES[@TERRITORY_CODE=$P_COUNTY_TERRITORY]/@CREDIT"/>				
				</xsl:when>
				<xsl:otherwise>
					0	
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$COUNTYOPERATIONCHARGE &gt; 0 ">
			<tr class="midcolora">
				<td ><li>County of Operation.</li></td>
				<td>+<xsl:value-of select="round($COUNTYOPERATIONCHARGE)"/>% </td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:if>
	</xsl:template>
	
	<!-- ============================================================================= -->
	<!--Modified the lenght Speed Surcharges: 16 feb 2006-->
	<xsl:template name="LENGTH_26FEET_SPEED65">
	 <xsl:variable name="VAR_LENGTH_26FEET_SPEED65">
			<xsl:choose>
				<xsl:when test="LENGTH &gt; 26 and (CAPABLESPEED &gt; 54 and CAPABLESPEED &lt; 66) and BOATSTYLECODE != 'JS'">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LISURCHARGE']/NODE[@ID ='LIBOATSPEED']/ATTRIBUTES/@SURCHARGE" />
				</xsl:when>
				<!--     <xsl:otherwise>0</xsl:otherwise>    -->
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_LENGTH_26FEET_SPEED65 &gt; 0">
			<tr class="midcolora">
				<td ><li>Surcharge for length over 26 feet and speeds 55-65 miles per hour. <xsl:value-of select="$LIABILTY"></xsl:value-of> </li></td>
				<td>+<xsl:value-of select="$VAR_LENGTH_26FEET_SPEED65" />% </td>
			</tr>
			<tr class="midcolora">
				<td ><li>Surcharge for length over 26 feet and speeds 55-65 miles per hour. <xsl:value-of select="$PHYSICAL"></xsl:value-of> </li></td>
				<td>+<xsl:value-of select="$VAR_LENGTH_26FEET_SPEED65" />% </td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================= -->
	<!--Modified the fibre glass surcharge:16 feb 2006-->
	<xsl:template name="FIBRE_GLASS">
	  <xsl:variable name="VAR_FIBRE_GLASS">
			<xsl:choose>
				<xsl:when test="CONSTRUCTION = 'FIBERGLASS' and AGE &gt; 15">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='FIBERGLASSBOAT']/ATTRIBUTES/@SURCHARGE" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_FIBRE_GLASS &gt; 0">
			<tr class="midcolora">
				<td><li>Surcharge for Fiberglass Boat. <xsl:value-of select="$PHYSICAL"></xsl:value-of> </li></td>
				<td>+<xsl:value-of select="$VAR_FIBRE_GLASS" />%</td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================= -->
	<xsl:template name="REMOVE_SAILBOAT">

	    <xsl:variable name="VAR_REMOVE_SAILBOAT">
			<xsl:choose>
				<xsl:when test="((REMOVESAILBOAT = 'Y' and LENGTH &lt; 26) or (ISGRAND='Y')) and (BOATSTYLECODE= 'S') ">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDSURCHARGE']/NODE[@ID ='PDREMOVALSAILBOAT']/ATTRIBUTES/@SURCHARGE" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:if test="$VAR_REMOVE_SAILBOAT &gt; 0">
			<tr class="midcolora">
				<td><li>Surcharge for Sailboat Racing Exclusion Waiver. <xsl:value-of select="$LIABILTY"></xsl:value-of> </li></td>
				<td>+<xsl:value-of select="$VAR_REMOVE_SAILBOAT" />% </td>
			</tr>
			<tr class="midcolora">
				<td><li>Surcharge for Sailboat Racing Exclusion Waiver. <xsl:value-of select="$PHYSICAL"></xsl:value-of> </li></td>
				<td>+<xsl:value-of select="$VAR_REMOVE_SAILBOAT" />% </td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:if>
	</xsl:template>
	<!-- ============================================================================= -->
	
	<!--============================================================================== -->
	<xsl:template name="DEDUCTIBLE_DISCOUNT">
	
	<!--<xsl:variable name="VAR_DED">
			  <xsl:variable name="INS" select="DEDUCTIBLE"></xsl:variable>
		</xsl:variable>-->
		 <!-- <xsl:variable name="INS" select="DEDUCTIBLE"></xsl:variable>
		 
	  <xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='DEDUCTIBLE']/ATTRIBUTES[@DESC=$INS]/@FACTOR"/>-->
	  
      <!--Modiifed on 20 feb-->
	<xsl:variable name="VAR_DED" > 
		<xsl:variable name="INS" select="DEDUCTIBLE"></xsl:variable>
		<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PDCREDIT']/NODE[@ID ='DEDUCTIBLE']/ATTRIBUTES[@DESC=$INS]/@CREDIT"/>
	</xsl:variable>
				
	<xsl:if test="$VAR_DED &gt; 0">
		<tr class="midcolora">
			<td ><li>Deductible </li></td>
			<td>-<xsl:value-of select="$VAR_DED" />%</td>
		</tr>
		<xsl:value-of select="user:SetFlag(0)" /> 
	</xsl:if>
	
	
	</xsl:template>
	<!--============================================================================== -->
	<xsl:template name="FinalDisplay">
	<table class="tableeffectTopHeader" cellSpacing="0" cellPadding="0" width="90%" align="center">
		<xsl:variable name="var1">
			<xsl:value-of select="user:GetFlag()" />
		</xsl:variable>
		<xsl:if test="$var1 &gt; 0">
			<tr>
				 
				<td colspan="2" class="midcolora"> 
					No Discounts or Surcharges are applicable 
				</td>
			</tr>
	
	</xsl:if>
	 </table>
	</xsl:template>

<!-- ============================================================================= -->

<xsl:template name="BOAT_DESCRIPTION">
  
		<xsl:value-of select="BOATTYPE" /> -
		<xsl:value-of select="MODEL" /> -
		<xsl:value-of select="YEAR" />
 

</xsl:template>
<!-- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  -->
<xsl:template name="OPERATOR_DESC">

		<xsl:value-of select="OPERATORFNAME" /> 
		<xsl:value-of select="' '" /> 
		 <xsl:value-of select="OPERATORMNAME" />   
		<xsl:value-of select="' '" /> 
		 <xsl:value-of select="OPERATORLNAME" />
		<xsl:value-of select="' '" /> 
		 <xsl:value-of select="DRIVER_CODE" /> 

</xsl:template>

<!-- ============================================================================= -->

</xsl:stylesheet>