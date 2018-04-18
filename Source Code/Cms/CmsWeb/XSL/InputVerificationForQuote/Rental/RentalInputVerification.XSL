 

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
		
<xsl:variable name="RDCoveragesDoc" select="document('FactorPath')"></xsl:variable>
<!-- Lookup id 1203,1223 in masterlookup_values. these are distinct codes. 
	 Indiana and michigan states have same code for same policy types
	 ANY CHANGES HERE WILL NEED TO BE INCORPORATED IN RESP. FACTORMASTERPATH FILE TOO
	  --> 
<xsl:variable name="POLICYCODE_DP2REPAIR" select="'DP-2^REPAIR'"/>
<xsl:variable name="POLICYCODE_DP2REPLACE" select="'DP-2^REPLACE'"/>
<xsl:variable name="POLICYCODE_DP3PREMIER" select="'DP-3^PREMIER'"/>
<xsl:variable name="POLICYCODE_DP3REPAIR" select="'DP-3^REPAIR'"/>
<xsl:variable name="POLICYCODE_DP3REPLACE" select="'DP-3^REPLACE'"/>


<!-- Break up of the above -->
<!-- Policy types -->
<xsl:variable name="POLICYTYPE_DP2" select="'DP-2'"/>
<xsl:variable name="POLICYTYPE_DP3" select="'DP-3'"/>

<!-- to check ProductPremier node-->
<xsl:variable name="POLICYDESC_REPAIR" select="'REPAIR'"/>
<xsl:variable name="POLICYDESC_REPLACEMENT" select="'REPLACE'"/>
<xsl:variable name="POLICYDESC_PREMIER" select="'PREMIER'"/>
 
<!-- ============================================================================================ 
								Loading ProductFactorMaster File (END)						  
 ============================================================================================ -->
		 
	<xsl:template match="/">
		<html>
			<head>
				<!--<xsl:variable name="myName" select="QUICKQUOTE/DWELLINGDETAILS/CSSNUM/@CSSVALUE"></xsl:variable>-->
				<xsl:variable name="myName" select="1"></xsl:variable>
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
				 
				<xsl:for-each select="DWELLINGDETAILS"> 
					<xsl:call-template name="MAIN_FOR_INPUT_XML"/>  
				</xsl:for-each>
			
				 <xsl:if test="user:AllVerified(1) = 1">   
					<xsl:for-each select="DWELLINGDETAILS"> 
						<xsl:call-template name="MAIN_FOR_MASTER_XML"/>  
					</xsl:for-each>
				  </xsl:if>  	
				</td>
			</tr>
		 </ul>
		</table>
		
	</xsl:template>
	
	
	
	
<xsl:template name ="MAIN_FOR_MASTER_XML">
 <!--
<xsl:if test="user:AllVerified(1) = 1">
		<tr> <td class="midcolora"> <b> Error In Master XML </b></td></tr>	
</xsl:if>
-->

	 	
<!-- Call Adjusted base --> 	
 <xsl:call-template name ="CALL_ADJUSTEDBASE"/>    
 
<!-- Dwelling under construction -->
<!-- <xsl:call-template name = "DUC_DISCOUNT_DISPLAY"/>		--> 

<!-- protective discount -->
<!-- <xsl:call-template name = "PROTECTIVE_DEVICE_DISCOUNT_DISPLAY"/> -->

<!-- Discount : Valued Customer 	--> 	
<!-- <xsl:call-template name = "VALUEDCUSTOMER_DISCOUNT_DISPLAY"/> -->


<!-- Age of Home -->
<!-- <xsl:call-template name = "AGEOFHOME_DISPLAY"/> -->


<!-- Multi policy -->
<!-- <xsl:call-template name = "MULTIPOLICY_DISPLAY"/> -->		

	
<!-- LP 124 Landlords Liability -->
 <xsl:call-template name = "LP_124_LANDLORDS_LIABILITY"/>  

<!-- Medical Payments To Others Each Person Creditdisplay -->
<!-- <xsl:call-template name = "MEDICAL_PAYMENTS_TO_OTHERS_EACH_PERSON_CREDITDISPLAY"/> -->

<!-- Incidentaloffice Display --> 
<!-- <xsl:call-template name = "INCIDENTALOFFICE_DISPLAY"/>	 -->

 
<!-- Earth Quake -->
  <xsl:call-template name ="EARTHQUAKE"/>  

<!-- Mine Subsidence Coverage Premium -->
<xsl:call-template name ="MINE_SUBSIDENCE_COVERAGE_PREMIUM"/>	 


<!-- COVERAGE:B - ADDITIONAL -->
<xsl:call-template name ="COVERAGE_B_ADDITIONAL"/> 


<!-- BUILDING IMPROVEMENTS ADDITIONAL --> 		
 <xsl:call-template name ="BUILDING_IMPROVEMENTS_ADDITIONAL"/>	

<!-- COVERAGE C ADDITIONAL  --> 
<xsl:call-template name ="COVERAGE_C_ADDITIONAL"/> 


<!-- INCREASED COVERAGE D ADDITIONAL --> 
<xsl:call-template name ="INCREASED_COVERAGE_D_ADDITIONAL"/> 


<!-- CONTENTS IN STORAGE ADDITIONAL --> 
<xsl:call-template name ="CONTENTS_IN_STORAGE_ADDITIONAL"/>


<!--  TREES LAWNS AND SHRUBS ADDITIONAL --> 
<!-- <xsl:variable name="v11">
</xsl:variable>   -->

 <xsl:call-template name ="TREES_LAWNS_AND_SHRUBS_ADDITIONAL"/> 



<!-- RADIO AND TV EQUIPMENT ADDITIONAL --> 
  <xsl:call-template name ="RADIO_AND_TV_EQUIPMENT_ADDITIONAL"/>    



<!-- SATELLITE DISHES ADDITIONAL -->
  <xsl:call-template name ="SATELLITE_DISHES_ADDITIONAL"/>  

<!-- AWNINGS AND CANOPIES ADDITIONAL -->
  <xsl:call-template name ="AWNINGS_AND_CANOPIES_ADDITIONAL"/>  
 
<!-- INSTALLATION FLOATER IF 184 BUILDING MATERIALS -->
<xsl:call-template name ="INSTALLATION_FLOATER_IF_184_BUILDING_MATERIALS"/> 

<!-- INSTALLATION FLOATER IF 184 NON STRUCTURAL EQUIPMENT -->
<xsl:call-template name ="INSTALLATION_FLOATER_IF_184_NON_STRUCTURAL_EQUIPMENT"/> 


<!-- PROPFEE --> 
 <xsl:call-template name ="PROPFEE"/>  

<!-- Minimum  Premium -->
<!-- <xsl:call-template name="MINIMUM_PREMIUM"/>  -->

<!-- Final Primium -->
<!--  <xsl:call-template name ="FINALPRIMIUM"/> -->


</xsl:template>



<!-- ============================================================================================ -->
<!--								Base Premium Groups Template(START)								  -->
<!--								  FOR MIcHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="DEWLLING-MAIN_MICHIGAN">
	<xsl:variable name="P_PRODUCT_PREMIER" select="PRODUCT_PREMIER"></xsl:variable>
	<xsl:variable name="PRODUCT_PREMIER_IN_CAPS"  select="translate(translate($P_PRODUCT_PREMIER,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/>
	
	<!-- Divert in case of Premier product -->
	<xsl:choose>
		<xsl:when test="$PRODUCT_PREMIER_IN_CAPS =$POLICYDESC_PREMIER">
			<xsl:call-template name="DEWLLING-MAIN_PREMIER_MICHIGAN"/>
		</xsl:when>
		<xsl:otherwise>
			 <xsl:call-template name="DEWLLING-MAIN"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- For DP3 - Premier -Michigan State-->
<xsl:template name="DEWLLING-MAIN_PREMIER_MICHIGAN">
	<!-- Get the premium group -->
	<xsl:variable name="TERCODES" select="TERRITORYCODES"/>
	<xsl:variable name="GROUP_ID" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES[@TID = $TERCODES]/@GROUPID"/>			

		<xsl:if test="normalize-space($GROUP_ID) = ''">
				<tr>
					<td class="midcolora">	<li> Group ID not found  (Path - "TERRITORY/TERRGROUPING/GROUPID") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
	<!-- Get the Form group -->
	<xsl:variable name="F_CODE" select="FORM_CODE"/>
	<xsl:variable name="PCODE" select="PRODUCTNAME"/>
	
	<xsl:variable name="FORMGROUP">
		 <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING']/ATTRIBUTES[@FORMID = $F_CODE]/@GROUPID"/>
	</xsl:variable> 

	<xsl:if test="normalize-space($FORMGROUP) = ''">
				<tr>
					<td class="midcolora">	<li> Form Group not found  (Path - "FORMGROUP/FORMGROUPING/GROUPID")</li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
				
	<!-- Fetch the factor from the table -->
	<xsl:variable name= "VAR_PRM_GROUP_CODE"> 
		 
	<xsl:choose> 
			<xsl:when test="$FORMGROUP ='1'">  
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1"/>			
			</xsl:when> 
			<xsl:when test="$FORMGROUP ='2'">  
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2"/>
			</xsl:when> 
			<xsl:when test="$FORMGROUP ='3'">  
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3"/>		
			</xsl:when> 
			<xsl:when test="$FORMGROUP ='4'">  
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4"/>	
			</xsl:when> 
			<xsl:when test="$FORMGROUP ='5'">  
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5"/>
			</xsl:when> 
			<xsl:when test="$FORMGROUP ='6'">  
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6"/>
			</xsl:when> 
			<xsl:when test="$FORMGROUP ='7'">  
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7"/>
			</xsl:when> 
			<xsl:when test="$FORMGROUP ='8'">  
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code8"/>
			</xsl:when> 
			<xsl:when test="$FORMGROUP ='9'">  
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code9"/>
			</xsl:when> 
			<xsl:otherwise></xsl:otherwise>
				
	</xsl:choose>
	</xsl:variable>
		
			<xsl:if test="normalize-space($VAR_PRM_GROUP_CODE) = ''">
				<tr>
					<td class="midcolora">	<li> Premium  Group Code  not found  (Path - "PREMIUM/PREMIUMTERRITORY/PrmGroup_Code")</li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
	
	
</xsl:template>

<!-- For all products of michigan other than DP3 - Premier -->
<xsl:template name="DEWLLING-MAIN">	
	<xsl:variable name="TERCODES" select="TERRITORYCODES"/>
	<xsl:variable name="GROUP_ID" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES[@TID = $TERCODES]/@GROUPID"/>			
	<xsl:if test="normalize-space($GROUP_ID) = '' or normalize-space($GROUP_ID) ='NaN'">
				<tr>
					<td class="midcolora">	 <li> Group ID not found  (Path - "TERRITORY/TERRGROUPING/GROUPID")</li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
	
	<xsl:variable name="F_CODE" select="FORM_CODE"/>
	
	<xsl:variable name="FORMGROUP">
		<xsl:choose>
			<xsl:when test ="TERRITORYCODES = 1">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING-1']/ATTRIBUTES[@FORMID = $F_CODE]/@GROUPID"/>
			</xsl:when>
			<xsl:when test ="TERRITORYCODES = 2">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING-2']/ATTRIBUTES[@FORMID = $F_CODE]/@GROUPID"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:variable> 
	
	<xsl:if test="normalize-space($FORMGROUP) = ''">
				<tr>
					<td class="midcolora"><li>	 Form Code  not found  (Path - "FORMGROUP/FORMGROUPING-(1/2)/GROUPID") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
	
	
	<xsl:variable name="PCODE" select="PRODUCTNAME"/>
	
	<xsl:choose>
		<xsl:when test ="TERRITORYCODES = 1">
		
		<xsl:variable name="VAR_PRMGROUP_CODE_T1"> 
			<xsl:choose> 
					<xsl:when test="$FORMGROUP = '1'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1"/>			
					</xsl:when> 
					<xsl:when test="$FORMGROUP = '2'">	
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2"/>
					</xsl:when> 
					<xsl:when test="$FORMGROUP = '3'">	
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3"/>		
					</xsl:when> 
					<xsl:when test="$FORMGROUP = '4'">	
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4"/>
					</xsl:when> 			
					<xsl:when test="$FORMGROUP = '5'"> 
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5"/>
					</xsl:when>
					<xsl:when test="$FORMGROUP = '6'"> 
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6"/>
					</xsl:when>

					<xsl:when test="$FORMGROUP = '7'"> 
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7"/>
					</xsl:when>
					<xsl:when test="$FORMGROUP = '8'"> 
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code8"/>
					</xsl:when>
					<xsl:when test="$FORMGROUP = '9'"> 
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code9"/>
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>	
			</xsl:variable>	
	
	
				
				<xsl:if test="normalize-space($VAR_PRMGROUP_CODE_T1) = ''">
				<tr>
					<td class="midcolora"><li>	 Premium  Group Code  not found  (Path - "PREMIUM/PREMIUMTERRITORY/PrmGroup_Code") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
				
		</xsl:when>
		<xsl:when test ="TERRITORYCODES = 2">
		
			<xsl:variable name="VAR_PRMGROUP_CODE_T2"> 
				<xsl:choose> 
						<xsl:when test="$FORMGROUP = '10'"> 
							<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1"/>			
						</xsl:when>
						<xsl:when test="$FORMGROUP = '11'"> 
							<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2"/>
						</xsl:when> 
						<xsl:when test="$FORMGROUP = '12'"> 
							<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3"/>		
						</xsl:when> 
						<xsl:when test="$FORMGROUP = '13'"> 
							<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4"/>
						</xsl:when> 
						<xsl:when test="$FORMGROUP = '14'"> 
							<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5"/>
						</xsl:when> 
						<xsl:when test="$FORMGROUP = '15'"> 
							<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6"/>
						</xsl:when> 
						<xsl:when test="$FORMGROUP = '16'"> 
							<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7"/>
						</xsl:when> 
						<xsl:when test="$FORMGROUP = '17'"> 
							<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code8"/>
						</xsl:when> 
						<xsl:when test="$FORMGROUP = '18'"> 
							<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code9"/>
						</xsl:when> 
						<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:variable>
			
				<xsl:if test="normalize-space($VAR_PRMGROUP_CODE_T2) = ''">
				<tr>
					<td class="midcolora"><li> Premium  Group Code  not found  (Path - "PREMIUM/PREMIUMTERRITORY/PrmGroup_Code") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
				
			
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
	
</xsl:template>

 

<xsl:template name="DEWLLING-MAIN_INDIANA">	

  
 
	<xsl:variable name="TERCODES" select="TERRITORYCODES"/>
	<xsl:variable name="GROUP_ID" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERRITORY']/NODE[@ID='TERRGROUPING']/ATTRIBUTES[@TID = $TERCODES]/@GROUPID"/>			
	
	<xsl:variable name="F_CODE" select="FORM_CODE"/>
	<xsl:variable name="FORMGROUP">
		<xsl:choose>
			<xsl:when test ="TERRITORYCODES = 1">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING-1']/ATTRIBUTES[@FORMID = $F_CODE]/@GROUPID"/>
			</xsl:when>
			<xsl:when test ="TERRITORYCODES = 2">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='FORMGROUP']/NODE[@ID='FORMGROUPING-2']/ATTRIBUTES[@FORMID = $F_CODE]/@GROUPID"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise> <!-- give territory 1 by default -->
		</xsl:choose>
	</xsl:variable> 
	
	
			<xsl:if test="normalize-space($FORMGROUP) = ''">
				<tr>
					<td class="midcolora"><li> Form Group Code  not found  (Path - "FORMGROUP/FORMGROUPING-(1/2)/GROUPID") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
	<xsl:variable name="PCODE" select="PRODUCTNAME"/>
	
	
	<xsl:choose>
		<xsl:when test ="TERRITORYCODES = 1">
		<xsl:variable name="VAR_PRMGROUP_CODE_T1"> 
				
			<xsl:choose> 
					<xsl:when test="$FORMGROUP = '1'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1"/>			
					</xsl:when>
					
					<xsl:when test="$FORMGROUP = '2'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2"/>
					</xsl:when>
					
					<xsl:when test="$FORMGROUP = '3'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3"/>		
					</xsl:when>
					
					<xsl:when test="$FORMGROUP = '4'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4"/>
					</xsl:when>
					
					<xsl:when test="$FORMGROUP = '5'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5"/>
					</xsl:when>
					
					<xsl:when test="$FORMGROUP = '6'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6"/>
					</xsl:when>
					
					<xsl:when test="$FORMGROUP = '7'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7"/>
					</xsl:when>
					
					<xsl:when test="$FORMGROUP = '8'"> 
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code8"/>
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
			</xsl:variable>
		
			<xsl:if test="normalize-space($VAR_PRMGROUP_CODE_T1) = ''">
				<tr>
					<td class="midcolora"><li> Premium Group Code  not found  (Path - "PREMIUM/PREMIUMTERRITORY/PrmGroup_Code") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
			
		</xsl:when>
		<xsl:when test ="TERRITORYCODES = 2">
		<xsl:variable name="VAR_PRMGROUP_CODE_T2"> 
			<xsl:choose> 
					<xsl:when test="$FORMGROUP = '9'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code1"/>			
					</xsl:when>
					
					<xsl:when test="$FORMGROUP = '10'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code2"/>
					</xsl:when>
							
					<xsl:when test="$FORMGROUP = '11'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code3"/>		
					</xsl:when>
					
					<xsl:when test="$FORMGROUP = '12'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code4"/>
					</xsl:when>
					
					<xsl:when test="$FORMGROUP = '13'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code5"/>
					</xsl:when>
					
					<xsl:when test="$FORMGROUP = '14'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code6"/>
					</xsl:when>
					
					<xsl:when test="$FORMGROUP = '15'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code7"/>
					</xsl:when>
					
					<xsl:when test="$FORMGROUP = '16'">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT[@ID='DP']/FACTOR[@ID='PREMIUM']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@Group_Code = $GROUP_ID and @Form_Code = $PCODE ]/@PrmGroup_Code8"/>
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
			</xsl:variable>
				
			<xsl:if test="normalize-space($VAR_PRMGROUP_CODE_T2) = ''">
				<tr>
					<td class="midcolora"><li>	 Premium Group Code  not found  (Path - "PREMIUM/PREMIUMTERRITORY/PrmGroup_Code") </li></td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
	
</xsl:template>


<xsl:template name="C_VALUE">
	 
	<!-- Divide by 1000 because product factor master has values in 1000 -->
	<xsl:variable name="CVALUE" select='format-number((DWELLING_LIMITS div 1000), "#")'/>   <!-- converting to integer. -->
	  
	<xsl:variable name="P_GROUP">
		<xsl:choose>
		<xsl:when test="contains(FORM_CODE,'M')">G1</xsl:when>
		<xsl:when test="contains(FORM_CODE,'F')">G2</xsl:when>
		<xsl:otherwise>G1</xsl:otherwise>	
	</xsl:choose>
	</xsl:variable>	
	 

	
	<xsl:variable name="Var_CVALUE"> 	
			<!-- get the max value of coverages in the database -->
			<xsl:variable name="P_MAX_VALUE" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $P_GROUP]/@MAXVALUE"/>	
			
			<xsl:if test="normalize-space($P_MAX_VALUE) = ''">
				<tr>
					<td class="midcolora"><li> Maximum Base Value not found  (Path - "BASE/GROUP/MAXVALUE") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
						 
			
			<xsl:choose>
				<!-- If the coverage selected is less than the max value then pick straight -->
				<xsl:when test="$CVALUE &lt;= $P_MAX_VALUE">
					
					<xsl:variable name="VAR_BASE_FACTOR"  select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $P_GROUP]/ATTRIBUTES[@CovA = $CVALUE]/@Factor"/>	
						<!-- <xsl:value of select="$VAR_BASE_FACTOR"/> -->				 
						
					<xsl:if test="normalize-space($VAR_BASE_FACTOR) = ''">
						<tr>
							<td class="midcolora"><li> Base Factor not found  (Path - "BASE/GROUP/ATTRIBUTES/Factor") </li> </td>
						</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
					</xsl:if>
			
				</xsl:when>
				<!-- otherwise use the additional concept -->
				<xsl:otherwise>
					<!-- For each Additional amount-->
					<xsl:variable name="P_ADDITIONAL_VALUE">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $P_GROUP]/ATTRIBUTES[@CovA = 'ADDITIONAL']/@AMOUNT"/>	
					</xsl:variable>
					
					<!-- value -->
					<xsl:variable name="P_ADDITIONAL_FACTOR">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $P_GROUP]/ATTRIBUTES[@CovA = 'ADDITIONAL']/@Factor"/>	
					</xsl:variable>
					
					<!-- value for the max factor -->
					<xsl:variable name="P_MAX_VALUE_FACTOR">
						<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASE']/NODE[@GROUP = $P_GROUP]/ATTRIBUTES[@CovA = $P_MAX_VALUE]/@Factor"/>	
					</xsl:variable>
					
					<!-- Calculations 
					e.g : factor for 250 + ((difference in the cov and the max value)/additional amount)* (factor for additional)-->
					<!-- 
					<xsl:value-of select="($P_MAX_VALUE_FACTOR + ( (($CVALUE - $P_MAX_VALUE) div $P_ADDITIONAL_VALUE) * $P_ADDITIONAL_FACTOR ))"/>
					-->
					
					<xsl:if test="normalize-space($P_ADDITIONAL_VALUE) = ''">
						<tr>
							<td class="midcolora"><li> Additional ammount  not found  (Path - "BASE/GROUP/ATTRIBUTES[@CovA = 'ADDITIONAL']/@AMOUNT") </li></td>
						</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
					</xsl:if>
					
					<xsl:if test="normalize-space($P_ADDITIONAL_FACTOR) = ''">
						<tr>
							<td class="midcolora"> <li> Additional value not found  (Path - "BASE/GROUP/ATTRIBUTES[@CovA = 'ADDITIONAL']/Factor")</li> </td>
						</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
					</xsl:if>
					
					<xsl:if test="normalize-space($P_MAX_VALUE_FACTOR) = ''">
						<tr>
							<td class="midcolora"><li> Maximum base value factor not found  (Path - "BASE/GROUP/ATTRIBUTES/Factor") </li> </td>
						</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
					</xsl:if>
					
					
					
				</xsl:otherwise>
			</xsl:choose>
  </xsl:variable>   
	<!--
	<xsl:choose>
		<xsl:when test="$Var_CVALUE = '' or $Var_CVALUE ='0'">	 
			<xsl:value-of select="1"/>
		</xsl:when>
		<xsl:otherwise>
		  <xsl:value-of select="$Var_CVALUE"/>  
			
		</xsl:otherwise>
	</xsl:choose>
	 -->
	
</xsl:template>
<!-- ============================================================================================ -->
<!--								Base Premium Template(END)									  -->
<!--								  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--					Templates for Replacement Cost [Factor,Credit,Display](SATRT)			  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- In the case of Replacement Cost depends on the percentage use -->       
<xsl:template name="REPLACEMENT_COST">
	<xsl:variable name="P_REPLACEMENT_COST">
		<xsl:value-of select="round((DWELLING_LIMITS div REPLACEMENTCOSTFACTOR)*100)"/>
	</xsl:variable>
	
	<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='REPLACEMENT_COST_FACTOR']/NODE[@ID ='REPLACEMENT_COST']/ATTRIBUTES[@LOWER_PERCENTAGE_LIMIT &lt;= $P_REPLACEMENT_COST  and @UPPER_PERCENTAGE_LIMIT &gt;= $P_REPLACEMENT_COST]/@FACTOR"/>
	
	
	 
</xsl:template>


<!-- For Display Only -->
<xsl:template name="REPLACEMENT_COST_DISPLAY">
	<!--Applied-->
	0
</xsl:template>


<!-- ============================================================================================ -->
<!--					Templates for Replacement Cost [Factor,Credit,Display](end)				  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name ="TERM_FACTOR">
	<!-- Not Less Than Annual on Builders Risk). If dwelling under construction then months=12 by default-->	 
	<xsl:variable name="PTERM" select="TERMFACTOR"/>

	
		<xsl:choose>
			<xsl:when test="DWELL_UND_CONSTRUCTION_DP1143 ='Y'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERM_FACTOR']/NODE[@ID ='TERMFACTOR']/ATTRIBUTES[@MONTHS=12]/@FACTOR"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TERM_FACTOR']/NODE[@ID ='TERMFACTOR']/ATTRIBUTES[@MONTHS=$PTERM]/@FACTOR"/>
			</xsl:otherwise>
		</xsl:choose>	
	
</xsl:template>

<xsl:template name ="NO_OF_FAMILY_UNITS_DWELLINGS_FACTOR">
<!-- No. of Families Units In Dwelling 
			Units Primary Seasonal
			1-Family 1.000 1.100
			2-Family 1.100 1.210  -->
	<xsl:variable name="NO_OF_FAMILIES" select="NUMBEROFFAMILIES"/>
	
	<xsl:choose>
		<xsl:when test="$NO_OF_FAMILIES ='' or $NO_OF_FAMILIES ='0'">
			1.00
		</xsl:when>
		<xsl:when test="$NO_OF_FAMILIES &gt; 0">
			<!-- we assume that if the number of families is greater than 2 ,the input xml sends 2-->
			<xsl:choose>
			<xsl:when test="SEASONALSECONDARY ='Y'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS=$NO_OF_FAMILIES]/@SEASONAL"/>
			</xsl:when>
			<xsl:when test="SEASONALSECONDARY ='N'">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS=$NO_OF_FAMILIES]/@PRIMARY"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS=$NO_OF_FAMILIES]/@PRIMARY"/>
			</xsl:otherwise>
			</xsl:choose>
			
		</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
	
</xsl:template>
<!-- ============================================================================================ -->
<!--						Templates for Discount - Deductible Factor (START)					  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="DFACTOR">	
	<xsl:variable name="DEDUCTIBLEAMT" select="DEDUCTIBLE"/>
	<xsl:choose>
		<xsl:when test = "DEDUCTIBLE &gt;= 500">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLES']/NODE[@ID ='OPTIONALDEDUCTIBLE']/ATTRIBUTES[@AMOUNT=$DEDUCTIBLEAMT]/@FACTOR"/>
		</xsl:when>
		<xsl:otherwise>
			1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--						Templates for Discount - Deductible Factor (END)					  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<!-- ============================================================================================ -->
<!--		Templates for Discount - Age Of Home Factor  [Factor,Credit,Display]  (SATRT)		  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="AGEOFHOME_FACTOR">		
	
	<xsl:variable name="MAX_AGE_LIMIT" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/@MAX_AGE_LIMIT"/>
			<xsl:if test="normalize-space($MAX_AGE_LIMIT) = ''">
				<tr>
					<td class="midcolora"><li>Max age  limit for home not found  (Path - "CREDITS/AOHCREDIT/MAX_AGE_LIMIT") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
		
		
	
	
	<xsl:variable name="AGE_OF_HOME" select="AGEOFHOME"/>					
	
	<xsl:variable name="VAR_AGE_OF_HOME_FACTOR"> 
	<xsl:choose>
		<xsl:when test ="(DWELL_UND_CONSTRUCTION_DP1143 !='Y') and ($AGE_OF_HOME &lt;= $MAX_AGE_LIMIT)">
		  <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/ATTRIBUTES[@AGE = $AGE_OF_HOME]/@FACTOR"/>  
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>	
	</xsl:variable>
	
	<xsl:if test="normalize-space($VAR_AGE_OF_HOME_FACTOR) = ''">
				<tr>
					<td class="midcolora"><li> Age of home factor not found  (Path - "CREDITS/AOHCREDIT/ATTRIBUTES/FACTOR") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
	
</xsl:template>


<!--Age Of Home CREDIT -->
<xsl:template name="AGEOFHOME_CREDIT">	
	<xsl:variable name="MAX_AGE_LIMIT" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/@MAX_AGE_LIMIT"/>
			
	<xsl:variable name="AGE_OF_HOME" select="AGEOFHOME"/>					
	
	<xsl:variable name="AGE_OF_HOME_CREDIT"> 
	<xsl:choose>
		<xsl:when test ="(DWELL_UND_CONSTRUCTION_DP1143 ='N') and ($AGE_OF_HOME &lt;= $MAX_AGE_LIMIT)">
		 	<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/ATTRIBUTES[@AGE = $AGE_OF_HOME]/@CREDIT"/>   
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
	</xsl:variable>
	
	<xsl:if test="normalize-space($AGE_OF_HOME_CREDIT) = ''">
				<tr>
					<td class="midcolora"><li> Age of home factor not found  (Path - "CREDITS/AOHCREDIT/ATTRIBUTES/CREDIT") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
</xsl:template>

<!--Age Of Home Display -->
<xsl:template name="AGEOFHOME_DISPLAY">	


	<xsl:variable name="MAX_AGE_LIMIT"  select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/@MAX_AGE_LIMIT"/>
	<xsl:variable name="AGE_OF_HOME" select="AGEOFHOME"/>					

 	<xsl:choose>
		<xsl:when test ="(DWELL_UND_CONSTRUCTION_DP1143 ='N') and ($AGE_OF_HOME &lt;= $MAX_AGE_LIMIT)">
			'Applied'		
		</xsl:when>
		<xsl:otherwise>
		0
		</xsl:otherwise> 
	</xsl:choose>  
</xsl:template>

<!-- ============================================================================================ -->
<!--		Templates for Discount - Age Of Home   [Factor,Credit,Display]  (END)				  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--		Templates for Discount - Protective Devices  [Factor,Credit,Display]  (START)		  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name = "PROTECTIVE_DEVICE_DISCOUNT_FACTOR">
	<!-- This discount is applicable only when the dwelling is not under construction -->
	
	
 
	<xsl:choose>
		<xsl:when test ="N0_LOCAL_ALARM = 1 and DWELL_UND_CONSTRUCTION_DP1143 = 'N'">
		<xsl:variable name="VAR_PD_FACTOR_LA1"> 
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='PROTECTIVEDEVICES']/ATTRIBUTES[@ID = 'OLSA']/@FACTOR"/>
		</xsl:variable>	
		
		<xsl:if test="normalize-space($VAR_PD_FACTOR_LA1) = ''">
				<tr>
					<td class="midcolora"><li> Protective device discount factor not found  (Path - "CREDITS/PROTECTIVEDEVICES/ATTRIBUTES[@ID = 'OLSA']/@FACTOR") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
		
		
		
		</xsl:when> 
		<xsl:when test ="N0_LOCAL_ALARM &gt; 1 and DWELL_UND_CONSTRUCTION_DP1143 = 'N'">
			
		<xsl:variable name="VAR_PD_FACTOR_LA2"> 	
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='PROTECTIVEDEVICES']/ATTRIBUTES[@ID = 'LFA']/@FACTOR"/>
		
		</xsl:variable>	
		
		<xsl:if test="normalize-space($VAR_PD_FACTOR_LA2) = ''">
				<tr>
					<td class="midcolora"><li> Protective device discount factor not found  (Path - "CREDITS/PROTECTIVEDEVICES/ATTRIBUTES[@ID = 'LFA']/@FACTOR") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			 
		</xsl:when>
		<xsl:otherwise>
		1.00
		</xsl:otherwise>
	</xsl:choose>
 
</xsl:template>


<xsl:template name = "PROTECTIVE_DEVICE_DISCOUNT_CREDIT">
<xsl:variable name="VAR_PROT_DEVICE_CR"> 
	<xsl:choose>
		<xsl:when test ="N0_LOCAL_ALARM = 1 and DWELL_UND_CONSTRUCTION_DP1143 = 'N'">
			
			<xsl:variable name="VAR_PD_FACTOR_LA1"> 
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='PROTECTIVEDEVICES']/ATTRIBUTES[@ID = 'OLSA']/@CREDIT"/>
			</xsl:variable>	
		
		<xsl:if test="normalize-space($VAR_PD_FACTOR_LA1) = ''">
				<tr>
					<td class="midcolora"><li>  Protective device discount Credit not found  (Path - "CREDITS/PROTECTIVEDEVICES/ATTRIBUTES[@ID = 'OLSA']/CREDIT") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
		
		
		</xsl:when>
		<xsl:when test ="N0_LOCAL_ALARM &gt; 1 and DWELL_UND_CONSTRUCTION_DP1143 = 'N'">
		<xsl:variable name="VAR_PD_FACTOR_LA2"> 
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='PROTECTIVEDEVICES']/ATTRIBUTES[@ID = 'LFA']/@CREDIT"/>
		</xsl:variable>	
		
		<xsl:if test="normalize-space($VAR_PD_FACTOR_LA2) = ''">
				<tr>
					<td class="midcolora"><li>  Protective device discount Credit not found  (Path - "CREDITS/PROTECTIVEDEVICES/ATTRIBUTES[@ID = 'LFA']/CREDIT") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
				
		
		
		</xsl:when>
		<xsl:otherwise>
		0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:variable>
 
	
</xsl:template>

<xsl:template name = "PROTECTIVE_DEVICE_DISCOUNT_DISPLAY">
<xsl:variable name="VAR_PROT_DEVICE"> 
	<xsl:choose>
		<xsl:when test ="N0_LOCAL_ALARM = 1 and DWELL_UND_CONSTRUCTION_DP1143 = 'N'">
			'Applied'
		</xsl:when>
		<xsl:when test ="N0_LOCAL_ALARM &gt; 1 and DWELL_UND_CONSTRUCTION_DP1143 = 'N'">
			'Applied'
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:variable>

 
	
</xsl:template>

<!-- ============================================================================================ -->
<!--		Templates for Discount - Protective Devices  [Factor,Credit,Display]  (END)			  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--	Templates for Discount -  Discount - Under Construction [Factor,Credit,Display](START)	  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name = "DUC_DISCOUNT">
<xsl:variable name="VAR_DUC_DISCOUNT"> 
	<xsl:choose>
		<xsl:when test ="DWELL_UND_CONSTRUCTION_DP1143 = 'Y'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DUC']/NODE[@ID ='DUC_CREDIT']/ATTRIBUTES/@FACTOR"/> 		
		</xsl:when>
		<xsl:otherwise>
			1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:variable>
		<xsl:if test="normalize-space($VAR_DUC_DISCOUNT) = ''">
				<tr>
					<td class="midcolora"><li>  Dwelling Under Consctruction factor not found  (Path - "DUC/DUC_CREDIT/DUC_CREDIT/ATTRIBUTES/FACTOR") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
		
	
	
</xsl:template>

<xsl:template name = "DUC_DISCOUNT_DISPLAY">
<xsl:variable name="VAR1"> 
	<xsl:choose>
		<xsl:when test ="DWELL_UND_CONSTRUCTION_DP1143 = 'Y'">
		'Applied'
		</xsl:when>
		<xsl:otherwise>
		0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:variable>
	
</xsl:template>

<xsl:template name = "DUC_DISCOUNT_CREDIT">
<xsl:variable name="VAR1"> 
	<xsl:choose>
		<xsl:when test ="DWELL_UND_CONSTRUCTION_DP1143 = 'Y'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DUC']/NODE[@ID ='DUC_CREDIT']/ATTRIBUTES/@CREDIT"/> 		
		</xsl:when>
		<xsl:otherwise>
		0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:variable>
 

</xsl:template>
<!-- ============================================================================================ -->
<!--	Templates for Discount -  Discount - Under Construction [Factor,Credit,Display](END)	  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--		Templates for Discount - Multi policy Factor [Factor,Credit,Display]  (START)		  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name="MULTIPOLICY_DISCOUNT_FACTOR">	
	<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT_FACTOR"> 
	<xsl:choose>
		<xsl:when test="MULTIPLEPOLICYFACTOR = 'Y'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MULTIPOLICY']/ATTRIBUTES/@FACTOR"/> 		
		</xsl:when>
		<xsl:otherwise> 
		1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:variable>	
	
	<xsl:if test="normalize-space($VAR_MULTIPOLICY_DISCOUNT_FACTOR) = ''">
				<tr>
					<td class="midcolora"><li> Multi policy Discount Factor not found  (Path - "CREDITS/MULTIPOLICY/ATTRIBUTES/FACTOR") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
		
</xsl:template>

<!--Multipolicy CREDIT -->
<xsl:template name="MULTIPOLICY_CREDIT">	
	
	<xsl:variable name="VAR_MULTIPOLICY_DISCOUNT_CREDIT"> 
	
	<xsl:choose>
		<xsl:when test="MULTIPLEPOLICYFACTOR = 'Y'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MULTIPOLICY']/ATTRIBUTES/@CREDIT"/> 		
		</xsl:when>
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
</xsl:variable>
			<xsl:if test="normalize-space($VAR_MULTIPOLICY_DISCOUNT_CREDIT) = ''">
				<tr>
					<td class="midcolora"><li> Multi policy Discount Credit not found  (Path - "CREDITS/MULTIPOLICY/ATTRIBUTES/CREDIT")</li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
				
</xsl:template>

<!--Multipolicy Display -->
<xsl:template name="MULTIPOLICY_DISPLAY">	

<xsl:variable name="VAR_MULTIPOLICY_DISPLAY"> 
	<xsl:choose>
		<xsl:when test="MULTIPLEPOLICYFACTOR = 'Y'">
		'Applied'
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:variable>
	
</xsl:template>

<!-- ============================================================================================ -->
<!--		Templates for Discount - Multipolicy Factor [Factor,Credit,Display]  (END)			  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--	   Templates for Liability - Each Occurrence - Each Person [Factor,Credit,Display] START) -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="LIABILITY_EACH_OCCURRENCE_DISPLAY"> 
<xsl:variable name ="VAR_LIABILITY_EACH_OCCURRENCE_DISPLAY"> 
	<xsl:choose>
		<xsl:when test ="PERSONALLIABILITY_LIMIT = 'No Coverage' or PERSONALLIABILITY_LIMIT = 'NO COVERAGE'">0</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="PERSONALLIABILITY_LIMIT"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:variable>
 
 </xsl:template>
 

<!-- ####################################### landlord liability #################################-->
<xsl:template name="LP_124_LANDLORDS_LIABILITY">

	<xsl:variable name="P_PERSONALLIABILITY_LIMIT">
		<xsl:choose>
			<xsl:when test="PERSONALLIABILITY_LIMIT = 'No Coverage' or PERSONALLIABILITY_LIMIT = 'NO COVERAGE'">0</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="PERSONALLIABILITY_LIMIT" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:variable>

	<xsl:variable name="P_MEDICALPAYMENTSTOOTHERS_LIMIT">
		<xsl:choose>
			<xsl:when test="MEDICALPAYMENTSTOOTHERS_LIMIT = 'No Coverage' or MEDICALPAYMENTSTOOTHERS_LIMIT = 'NO COVERAGE'">0</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="MEDICALPAYMENTSTOOTHERS_LIMIT" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:variable>
	
	
	 
	<xsl:choose>
		<xsl:when test="$P_PERSONALLIABILITY_LIMIT &gt; 0 and $P_MEDICALPAYMENTSTOOTHERS_LIMIT &gt; 0">
				<xsl:variable name="P_NUMBER_OF_FAMILIES" select="NUMBEROFFAMILIES" />
				
				<!-- Each Addition Medical Payments -->
				<xsl:variable name="P_FOR_EACH_ADDITIONAL_MED_PAY">
					<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDS_LIABILITY']/NODE[@ID='LANDLORDS_LIABILITY_LIMIT']/@FOR_EACH_ADDITIONAL_MEDICAL" />
				</xsl:variable>
				
				<xsl:if test="normalize-space($P_FOR_EACH_ADDITIONAL_MED_PAY) = ''">
				<tr>
					<td class="midcolora"><li> Medical pay for each additional not found  (Path - "LANDLORDS_LIABILITY/LANDLORDS_LIABILITY_LIMIT/FOR_EACH_ADDITIONAL_MEDICAL")</li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>


				
				<!-- Value for single family -->
				<xsl:variable name="P_1_FAMILY" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDS_LIABILITY']/NODE[@ID='LANDLORDS_LIABILITY_LIMIT']/ATTRIBUTES[@LIABILITY_LIMIT = $P_PERSONALLIABILITY_LIMIT]/@MED_PAY_1_FAMILY" />
		
				<xsl:if test="normalize-space($P_1_FAMILY) = ''">
					<tr>
						<td class="midcolora"><li>Medical pay for 1 family not found  (Path - "LANDLORDS_LIABILITY/LANDLORDS_LIABILITY_LIMIT/ATTRIBUTES/MED_PAY_1_FAMILY") </li></td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>


				
				<xsl:variable name="P_1_FAMILY_MED_PAY_LIMIT" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDS_LIABILITY']/NODE[@ID='LANDLORDS_LIABILITY_LIMIT']/ATTRIBUTES[@LIABILITY_LIMIT = $P_PERSONALLIABILITY_LIMIT]/@MED_PAY_LIMIT" />
				
				<xsl:if test="normalize-space($P_1_FAMILY_MED_PAY_LIMIT) = ''">
					<tr>
						<td class="midcolora"><li> Medical pay limit not found  (Path - "LANDLORDS_LIABILITY/LANDLORDS_LIABILITY_LIMIT/ATTRIBUTES/MED_PAY_LIMIT") </li></td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>


				
				<!-- Additional Medical for 1 family -->
				<xsl:variable name="P_ADDITIONAL_1_FAMILY" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDS_LIABILITY']/NODE[@ID='LANDLORDS_LIABILITY_LIMIT']/ATTRIBUTES[@LIABILITY_LIMIT = $P_PERSONALLIABILITY_LIMIT]/@ADDITIONAL_1_FAMILY" />
			
				<xsl:if test="normalize-space($P_ADDITIONAL_1_FAMILY) = ''">
					<tr>
						<td class="midcolora"> <li>Additional Medical for 1 family not found  (Path - "LANDLORDS_LIABILITY/LANDLORDS_LIABILITY_LIMIT/ATTRIBUTES/ADDITIONAL_1_FAMILY") </li></td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>


				
				<!-- Medical for each additional family (over 1 family)-->
				<xsl:variable name="P_EACH_ADDL_FAMILY" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDS_LIABILITY']/NODE[@ID='LANDLORDS_LIABILITY_LIMIT']/ATTRIBUTES[@LIABILITY_LIMIT = $P_PERSONALLIABILITY_LIMIT]/@EACH_ADDL_FAMILY" />
				
				<xsl:if test="normalize-space($P_EACH_ADDL_FAMILY) = ''">
					<tr>
						<td class="midcolora"><li> Medical for each additional family not found  (Path - "LANDLORDS_LIABILITY/LANDLORDS_LIABILITY_LIMIT/ATTRIBUTES/EACH_ADDL_FAMILY") </li> </td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				
				
				<xsl:variable name="P_EACH_ADDL_FAMILY_MED_PAY_LIMIT"  select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDS_LIABILITY']/NODE[@ID='LANDLORDS_LIABILITY_LIMIT']/ATTRIBUTES[@LIABILITY_LIMIT = $P_PERSONALLIABILITY_LIMIT]/@MED_PAY_LIMIT" />
							
				<!-- Additional Medical for each additional family (over 1 family)-->
				<xsl:variable name="P_ADDITIONAL_EACH_ADDL_FAMILY" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LANDLORDS_LIABILITY']/NODE[@ID='LANDLORDS_LIABILITY_LIMIT']/ATTRIBUTES[@LIABILITY_LIMIT = $P_PERSONALLIABILITY_LIMIT]/@ADDITIONAL_EACH_ADDL_FAMILY" />
				
					<xsl:if test="normalize-space($P_ADDITIONAL_EACH_ADDL_FAMILY) = ''">
					<tr>
						<td class="midcolora"> <li> Additional Medical for each additional family  not found  (Path - "LANDLORDS_LIABILITY/LANDLORDS_LIABILITY_LIMIT/ATTRIBUTES/ADDITIONAL_EACH_ADDL_FAMILY") </li> </td>
					</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
	
				
				<!-- main formula -->
				<!-- 
					1. Fetch the Liability rates for 1Family against the liability limit and medical payment limit =1000
					2. For the first family, the additional medical payment limit per thousand will be added.
					3. From second family onwards, the additional medical payment limit per thousand for each addl family will be added. 
				-->
				 
						<xsl:variable name="FINAL_VALUE">
						
							<!-- %%%%%%%%%%%%% -->
							<xsl:variable name="V1" select="$P_1_FAMILY"/>
                            <xsl:variable name="V2" select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/>
                            <xsl:variable name="V3" select="$P_1_FAMILY_MED_PAY_LIMIT"/>
                            <xsl:variable name="V4" select="$P_FOR_EACH_ADDITIONAL_MED_PAY"/> 
                            <xsl:variable name="V5" select="$P_ADDITIONAL_1_FAMILY"/>
                            <xsl:variable name="V6" select="$P_NUMBER_OF_FAMILIES - 1"/> 
                            <xsl:variable name="V7" select="$P_EACH_ADDL_FAMILY"/> 
                            <xsl:variable name="V8" select="$P_MEDICALPAYMENTSTOOTHERS_LIMIT"/>
                            <xsl:variable name="V9" select="$P_EACH_ADDL_FAMILY_MED_PAY_LIMIT"/>
                            <xsl:variable name="V10" select="$P_FOR_EACH_ADDITIONAL_MED_PAY"/>
                            <xsl:variable name="V11" select="$P_ADDITIONAL_EACH_ADDL_FAMILY"/>

							<xsl:value-of select="$V1 + ((($V2 - $V3) div $V4 ) * $V5)+ ($V6 * ( $V7   + ((($V8 - $V9) div $V10) * $V11)))"/>
				</xsl:variable>
				
				<!-- <xsl:value-of select="$FINAL_VALUE" />	 -->
		
		</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
	
</xsl:template>






<!-- ============================================================================================ -->
<!--	   Templates for Liability - Each Occurrence - Each Person [Factor,Credit,Display] END)   -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--		Templates for Medical Payments to Others - Each Person [Factor,Credit,Display] START) -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="MEDICAL_PAYMENTS_TO_OTHERS_EACH_PERSON_DISPLAY">
	<xsl:choose>
		<xsl:when test ="MEDICALPAYMENTSTOOTHERS_LIMIT = 'No Coverage' or MEDICALPAYMENTSTOOTHERS_LIMIT = 'NO COVERAGE'">0</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="MEDICALPAYMENTSTOOTHERS_LIMIT"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<!-- MEDICAL_PAYMENTS_TO_OTHERS_EACH_PERSON_CREDITDISPLAY -->
<xsl:template name="MEDICAL_PAYMENTS_TO_OTHERS_EACH_PERSON_CREDITDISPLAY">
	<xsl:variable name="VAR1"><xsl:call-template name="LIABILITY_EACH_OCCURRENCE_DISPLAY"/></xsl:variable>
 
<xsl:choose>
	<xsl:when test="$VAR1 &gt; 1">
			<xsl:choose>
				<xsl:when test ="MEDICALPAYMENTSTOOTHERS_LIMIT = 'No Coverage' or MEDICALPAYMENTSTOOTHERS_LIMIT = 'NO COVERAGE'">
					0
				</xsl:when>
				<xsl:otherwise>
					'Included'
				</xsl:otherwise>
			</xsl:choose>
	</xsl:when>
	<xsl:otherwise>0</xsl:otherwise>
</xsl:choose>
 
 

 

</xsl:template>
<!-- ============================================================================================ -->
<!--		Templates for Medical Payments to Others - Each Person [Factor,Credit,Display]  (END) -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--		Templates for With Incidental Office Occupancy (By Insured Only) [Display]  (START)  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name="INCIDENTALOFFICE_DISPLAY">
	<xsl:choose>
		<xsl:when test ="INCIDENTALOFFICE = 'Y'">
		'Included'
		</xsl:when>
		<xsl:otherwise>
		0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--		Templates for With Incidental Office Occupancy (By Insured Only) [Display]  (END)	  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<!-- ============================================================================================ -->
<!--			Templates for Discount - VALUED CUSTOMER [Factor,Credit,Display]  (START)		  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- The amount of discount depends on the number of consecutive years,
	 ending with the current renewal date, that the policy has been with Wolverine Mutual. The discount is
	 applied per policy and per location at the beginning of the third policy year continuously inforce. Any loss
	 last 12 months reduces discount. -->
<!-- ============================================================================================ -->
<xsl:template name="VALUEDCUSTOMER_DISCOUNT_MICHIGAN">
	<xsl:variable name="P_NO_YEARS_WITH_WOLVERINE" select="NO_YEARS_WITH_WOLVERINE"/>
	<xsl:choose>
		<xsl:when test="NEWBUSINESSFACTOR ='N' or NEWBUSINESSFACTOR='' or NEWBUSINESSFACTOR='FALSE'">	
			<!-- Check if it is loss free or nt loss free -->
			<xsl:choose>
				<xsl:when test="LOSSFREE ='Y'"> 
					
					<xsl:variable name="VAR_LOSSFREEFACTOR"  select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@MINRENEWALDURATION &lt;= $P_NO_YEARS_WITH_WOLVERINE and @MAXRENEWALDURATION &gt;= $P_NO_YEARS_WITH_WOLVERINE]/@LOSSFREEFACTOR"/>	
									
						<xsl:if test="normalize-space($VAR_LOSSFREEFACTOR) = ''">
						<tr>
						<td class="midcolora"><li>Loss Free Factor not found  (Path - "CREDITS/VALUEDCUSTOMER_RENEWAL_DISCOUNT/ATTRIBUTES/LOSSFREEFACTOR") </li> </td>
						</tr>
						<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>
			
					<xsl:value-of select="$VAR_LOSSFREEFACTOR"/>
		
				</xsl:when>
				<xsl:otherwise>
					
					<xsl:variable name="VAR_LOSSPRIOR12MONTHSFACTOR"  select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@MINRENEWALDURATION &lt;= $P_NO_YEARS_WITH_WOLVERINE and @MAXRENEWALDURATION &gt;= $P_NO_YEARS_WITH_WOLVERINE]/@LOSSPRIOR12MONTHSFACTOR"/>	
				
						<xsl:if test="normalize-space($VAR_LOSSPRIOR12MONTHSFACTOR) = ''">
						<tr>
						<td class="midcolora"><li> Loss prior 12 months Factor not found  (Path - "CREDITS/VALUEDCUSTOMER_RENEWAL_DISCOUNT/ATTRIBUTES/LOSSPRIOR12MONTHSFACTOR") </li> </td>
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
</xsl:template>

<xsl:template name="VALUEDCUSTOMER_DISCOUNT_CREDIT_MICHIGAN">
	<xsl:variable name="P_NO_YEARS_WITH_WOLVERINE" select="NO_YEARS_WITH_WOLVERINE"/>

	<xsl:choose>
		<xsl:when test="NEWBUSINESSFACTOR ='N' or NEWBUSINESSFACTOR='' or NEWBUSINESSFACTOR='FALSE'">
			<!-- Check if it is loss free or nt loss free -->
			<xsl:choose>
				<xsl:when test="LOSSFREE ='Y'"> 
					
					<xsl:variable name="VAR_LOSSFREECREDIT" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@MINRENEWALDURATION &lt;= $P_NO_YEARS_WITH_WOLVERINE and @MAXRENEWALDURATION &gt;= $P_NO_YEARS_WITH_WOLVERINE]/@LOSSFREECREDIT"/>	
		      
		      <xsl:if test="normalize-space($VAR_LOSSFREECREDIT) = ''">
						<tr>
						<td class="midcolora"><li> Loss Free Credit not found  (Path - "CREDITS/VALUEDCUSTOMER_RENEWAL_DISCOUNT/ATTRIBUTES/LOSSFREECREDIT") </li> </td>
						</tr>
						<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>
			
					<xsl:value-of select="$VAR_LOSSFREECREDIT"/>
					
					
		      
		      </xsl:when>
				<xsl:otherwise>
					<xsl:variable name="VAR_LOSSPRIOR12MONTHSCREDIT"  select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@MINRENEWALDURATION &lt;= $P_NO_YEARS_WITH_WOLVERINE and @MAXRENEWALDURATION &gt;= $P_NO_YEARS_WITH_WOLVERINE]/@LOSSPRIOR12MONTHSCREDIT"/>	
				
				<xsl:if test="normalize-space($VAR_LOSSPRIOR12MONTHSCREDIT) = ''">
						<tr>
						<td class="midcolora"><li>	 Loss prior 12 months Credit not found  (Path - "CREDITS/VALUEDCUSTOMER_RENEWAL_DISCOUNT/ATTRIBUTES/LOSSPRIOR12MONTHSCREDIT")</li>  </td>
						</tr>
						<xsl:if test="user:AllVerified(0)=1"></xsl:if>
						</xsl:if>
						
						
				</xsl:otherwise>
			</xsl:choose>			
		</xsl:when> 
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="VALUEDCUSTOMER_DISCOUNT_DISPLAY_MICHIGAN">

	<!-- 
	1. It is applicable for Renewal case only
	2. Year to be taken (in case of input xml) will be current effective date minus the inception date 
	-->
	<xsl:choose>
		<xsl:when test="NEWBUSINESSFACTOR='N' or NEWBUSINESSFACTOR=''">
			<xsl:choose>
				<xsl:when test="NO_YEARS_WITH_WOLVERINE ='' or NO_YEARS_WITH_WOLVERINE &lt;= 2">
					0
				</xsl:when>
				<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 2">
					'Applied'
				</xsl:when>				
				<xsl:otherwise>
					0
				</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>
					0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<!-- For INDIANA -->
<xsl:template name="VALUEDCUSTOMER_DISCOUNT_DISPLAY_INDIANA">
	<!-- 
		1. It is applicable for Renewal case only
		2. Year to be taken (in case of input xml) will be current effective date minus the inception date 
	-->

	 <xsl:choose>
		<xsl:when test="NEWBUSINESSFACTOR='N' or NEWBUSINESSFACTOR=''">
			<xsl:choose>
				<xsl:when test="NO_YEARS_WITH_WOLVERINE &lt;= 2">
					0
				</xsl:when>
				<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 2 and NO_YEARS_WITH_WOLVERINE &lt;= 4 and LOSSFREE = 'N'">
					'Applied'
				</xsl:when>
				<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 2 and NO_YEARS_WITH_WOLVERINE &lt;= 4 and LOSSFREE = 'Y'">
					'Applied'
				</xsl:when>
				<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 4 and LOSSFREE = 'N'">
					'Applied'
				</xsl:when>
				<xsl:when test="NO_YEARS_WITH_WOLVERINE &gt; 4 and LOSSFREE = 'Y'">
					'Applied'
				</xsl:when>
				<xsl:otherwise>
					0
				</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
	
</xsl:template>

<xsl:template name="VALUEDCUSTOMER_DISCOUNT_INDIANA"> 
	<!-- 
		1. It is applicable for Renewal case only
		2. Year to be taken (in case of input xml) will be current effective date minus the inception date 
	-->
    <xsl:variable name="P_NO_YEARS_WITH_WOLVERINE" select="NO_YEARS_WITH_WOLVERINE"/>

	<xsl:choose>
		<xsl:when test="(NEWBUSINESSFACTOR='N' or NEWBUSINESSFACTOR='') and LOSSFREE = 'Y'">
	
		<xsl:variable name="VAR_NO_LOSS_FACTOR"  select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@MINRENEWALDURATION &lt;= $P_NO_YEARS_WITH_WOLVERINE and @MAXRENEWALDURATION &gt;= $P_NO_YEARS_WITH_WOLVERINE]/@NO_LOSS_FACTOR"/>
				
				<xsl:if test="normalize-space($VAR_NO_LOSS_FACTOR) = ''">
				<tr>
					<td class="midcolora"><li>	 No loss factor not found  (Path - "CREDITS/VALUEDCUSTOMER_RENEWAL_DISCOUNT/ATTRIBUTES/NO_LOSS_FACTOR") </li> </td>
				</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
						
		<xsl:value-of select ="$VAR_NO_LOSS_FACTOR"/> 
		
		</xsl:when>
		<xsl:when test="(NEWBUSINESSFACTOR='N' or NEWBUSINESSFACTOR='') and (LOSSFREE = '' or LOSSFREE='N')">
			
			<xsl:variable name="VAR_LOSS_FACTOR" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@MINRENEWALDURATION &lt;= $P_NO_YEARS_WITH_WOLVERINE and @MAXRENEWALDURATION &gt;= $P_NO_YEARS_WITH_WOLVERINE]/@LOSS_FACTOR"/>
			<xsl:if test="normalize-space($VAR_LOSS_FACTOR) = ''">
				<tr>
					<td class="midcolora"><li> loss factor not found  (Path - "CREDITS/VALUEDCUSTOMER_RENEWAL_DISCOUNT/ATTRIBUTES/LOSS_FACTOR") </li> </td>
				</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
			
			<xsl:value-of select ="$VAR_LOSS_FACTOR"/> 
				
		</xsl:when>
		<xsl:otherwise>
			1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="VALUEDCUSTOMER_DISCOUNT_CREDIT_INDIANA">

    <xsl:variable name="P_NO_YEARS_WITH_WOLVERINE" select="NO_YEARS_WITH_WOLVERINE"/>

	<xsl:choose>
	<xsl:when test="(NEWBUSINESSFACTOR='N' or NEWBUSINESSFACTOR='') and LOSSFREE = 'Y'">
			<xsl:variable name="VAR_NO_LOSS_CREDIT"  select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@MINRENEWALDURATION &lt;= $P_NO_YEARS_WITH_WOLVERINE and @MAXRENEWALDURATION &gt;= $P_NO_YEARS_WITH_WOLVERINE]/@NO_LOSS_CREDIT"/>
				
				<xsl:if test="normalize-space($VAR_NO_LOSS_CREDIT) = ''">
				<tr>
					<td class="midcolora"><li> No loss credit not found  (Path - "CREDITS/VALUEDCUSTOMER_RENEWAL_DISCOUNT/ATTRIBUTES/NO_LOSS_CREDIT") </li> </td>
				</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				<xsl:value-of select ="$VAR_NO_LOSS_CREDIT"/> 
				
	
	
		</xsl:when>
		<xsl:when test="(NEWBUSINESSFACTOR='N' or NEWBUSINESSFACTOR='') and (LOSSFREE = '' or LOSSFREE='N')">
			<xsl:variable name="VAR_LOSS_CREDIT" select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@MINRENEWALDURATION &lt;= $P_NO_YEARS_WITH_WOLVERINE and @MAXRENEWALDURATION &gt;= $P_NO_YEARS_WITH_WOLVERINE]/@LOSS_CREDIT"/>
				
				<xsl:if test="normalize-space($VAR_LOSS_CREDIT) = ''">
				<tr>
					<td class="midcolora"><li>loss credit not found  (Path - "CREDITS/VALUEDCUSTOMER_RENEWAL_DISCOUNT/ATTRIBUTES/LOSS_CREDIT") </li> </td>
				</tr>
					<xsl:if test="user:AllVerified(0)=1"></xsl:if>
				</xsl:if>
				
				<xsl:value-of select ="$VAR_LOSS_CREDIT"/> 
		
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<xsl:template name="VALUEDCUSTOMER_DISCOUNT">
	<xsl:choose>
		<xsl:when test ="STATENAME ='MICHIGAN'">
			<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT_MICHIGAN"/>
		</xsl:when>
		<xsl:when test ="STATENAME ='INDIANA'">
			<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT_INDIANA"/>
		</xsl:when>
		<xsl:otherwise>
		1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="VALUEDCUSTOMER_DISCOUNT_CREDIT">
	<xsl:choose>
		<xsl:when test ="STATENAME ='MICHIGAN'">
			<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT_CREDIT_MICHIGAN"/>
		</xsl:when>
		<xsl:when test ="STATENAME ='INDIANA'">
			<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT_CREDIT_INDIANA"/>
		</xsl:when>
		<xsl:otherwise>
		0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="VALUEDCUSTOMER_DISCOUNT_DISPLAY">
   
	<xsl:choose>
		<xsl:when test ="STATENAME ='MICHIGAN'">
			<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT_DISPLAY_MICHIGAN"/>
		</xsl:when>
		<xsl:when test ="STATENAME ='INDIANA'">
			<xsl:call-template name="VALUEDCUSTOMER_DISCOUNT_DISPLAY_INDIANA"/>
		</xsl:when>
		<xsl:otherwise>
		1.00
		</xsl:otherwise>
	</xsl:choose>
 

 

	
</xsl:template>


<!-- ============================================================================================ -->
<!--			Templates for Discount - VALUED CUSTOMER [Factor,Credit,Display]  (END)			  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--									Earthquake(START)										  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- 
		1. When added to the Fire policy, this peril shall apply to the same coverages and 
			for the same limits that apply	to the peril of Fire.
		2. Deductible: The deductible is 10% of the limit of liability for each coverage and 
			is subject to a $250 minimum.
			The deductible applies separately to loss under the various coverages of the policy. 
			If the limit of liability on certain property is increased by endorsement, the total 
			limit of liability is used to determine the deductible.
		3. Binder: Earthquake Coverage may be bound subject to a 14-day waiting period. 
			The waiting period ends at 12:01 A.M. fourteen (14) days following the date the insured 
			requests coverage. The binder or insured's request must be received by the company prior 
			to the expiration of the 14-day waiting period. Earthquake coverage may not be deleted from 
			new, renewal or rewritten policies prior to the policy expiration. -->
<!-- ============================================================================================ -->

<xsl:template name ="EARTHQUAKE">
  
    
   
   <xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE"/>
   <xsl:variable name="VAR_COV_A"> <xsl:call-template name ="EARTHQUAKE_COV_A"/>  </xsl:variable>
    
			
   <xsl:variable name="VAR_COV_B"> 
		<xsl:choose>
			<xsl:when test="APPURTENANTSTRUCTURES_ADDITIONAL != '' and APPURTENANTSTRUCTURES_ADDITIONAL &gt;0">
					<xsl:call-template name ="EARTHQUAKE_COV_B"/>  	
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:variable>
   <xsl:variable name="VAR_COV_D"> <xsl:call-template name ="EARTHQUAKE_COV_D"/>  </xsl:variable>
   <xsl:variable name="VAR_COV_E"> <xsl:call-template name ="EARTHQUAKE_COV_E"/>  </xsl:variable>
   <xsl:variable name="VAR_COV_C"> <xsl:call-template name ="EARTHQUAKE_COV_C"/>  </xsl:variable>
  
  
 <!-- call each function once to create the row -->
 <!-- start   -->
  <xsl:call-template name ="EARTHQUAKE_COV_A_DISP"/>  
   		<xsl:choose>
			<xsl:when test="APPURTENANTSTRUCTURES_ADDITIONAL != '' and APPURTENANTSTRUCTURES_ADDITIONAL &gt;0">
					<xsl:call-template name ="EARTHQUAKE_COV_B_DISP"/>  	
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>

<xsl:call-template name ="EARTHQUAKE_COV_D_DISP"/>  
<xsl:call-template name ="EARTHQUAKE_COV_E_DISP"/>  
<xsl:call-template name ="EARTHQUAKE_COV_C_DISP"/>  
  <!-- till here --> 
  
  
  
   
  
   <!-- Building Improvements/Alterations
		Contents In Storage
		Trees, Lawns, and Shrubs
		Radio and TV Equipment'
		Satellite Dishes
		Awnings and Canopies		
    -->
    
  
		<xsl:call-template name ="EARTHQUAKE_CALC_COV_A">
				<xsl:with-param name="FACTORELEMENT" select="$VAR_COV_A"></xsl:with-param>
		</xsl:call-template>
  
   
  
		<xsl:call-template name ="EARTHQUAKE_CALC_COV_B">
				<xsl:with-param name="FACTORELEMENT" select="$VAR_COV_B"></xsl:with-param>
		</xsl:call-template>
  
   
  
		<xsl:call-template name ="EARTHQUAKE_CALC_COV_C">
				<xsl:with-param name="FACTORELEMENT" select="$VAR_COV_C"></xsl:with-param>
		</xsl:call-template>
  
   
  
		<xsl:call-template name ="EARTHQUAKE_CALC_COV_D">
				<xsl:with-param name="FACTORELEMENT" select="$VAR_COV_D"></xsl:with-param>
		</xsl:call-template>
  
   
  
		<xsl:call-template name ="EARTHQUAKE_CALC_COV_E">
				<xsl:with-param name="FACTORELEMENT" select="$VAR_COV_E"></xsl:with-param>
		</xsl:call-template>
  
   
   		 
   
      <!-- Building Improvements/Alterations
		Contents In Storage
		Trees, Lawns, and Shrubs
		Radio and TV Equipment'
		Satellite Dishes
		Awnings and Canopies		
    -->
    
    <xsl:variable name="P_EARTHQUAKE_OTHER_COVERAGES_FACTOR">
		<xsl:call-template name ="EARTHQUAKE_OTHER_COVERAGES_FACTOR"/>
    </xsl:variable>
    
    <xsl:call-template name ="EARTHQUAKE_OTHER_COVERAGES_FACTOR_DISP"/>
    
    
   
		<xsl:call-template name ="EARTHQUAKE_CALC_BUILDINGIMPROVEMENTS_ADDITIONAL">
				<xsl:with-param name="FACTORELEMENT" select="$P_EARTHQUAKE_OTHER_COVERAGES_FACTOR"></xsl:with-param>
		</xsl:call-template>
		
    
   
		<xsl:call-template name ="EARTHQUAKE_CALC_CONTENTSINSTORAGE_ADDITIONAL">
				<xsl:with-param name="FACTORELEMENT" select="$P_EARTHQUAKE_OTHER_COVERAGES_FACTOR"></xsl:with-param>
		</xsl:call-template>
   
   
		<xsl:call-template name ="EARTHQUAKE_CALC_TREESLAWNSSHRUBS_ADDITIONAL">
				<xsl:with-param name="FACTORELEMENT" select="$P_EARTHQUAKE_OTHER_COVERAGES_FACTOR"></xsl:with-param>
		</xsl:call-template>
   
   
		<xsl:call-template name ="EARTHQUAKE_CALC_RADIOTV_ADDITIONAL">
				<xsl:with-param name="FACTORELEMENT" select="$P_EARTHQUAKE_OTHER_COVERAGES_FACTOR"></xsl:with-param>
		</xsl:call-template>
   
   
		<xsl:call-template name ="EARTHQUAKE_CALC_SATELLITEDISHES_ADDITIONAL">
				<xsl:with-param name="FACTORELEMENT" select="$P_EARTHQUAKE_OTHER_COVERAGES_FACTOR"></xsl:with-param>
		</xsl:call-template>
   
   
		<xsl:call-template name ="EARTHQUAKE_CALC_AWNINGSCANOPIES_ADDITIONAL">
				<xsl:with-param name="FACTORELEMENT" select="$P_EARTHQUAKE_OTHER_COVERAGES_FACTOR"></xsl:with-param>
		</xsl:call-template>
   
    
 
     
  
</xsl:template>

<!--  ********************************************************************************************************************** -->
<!-- EARTH QUAKE for Coverage A -->

<xsl:template name ="EARTHQUAKE_COV_A">
  <xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE"/>
  <xsl:choose>
		<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'F'">
		
		<xsl:variable name="VAR_EXT_CONS_COVA_F"> 
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovA' and @TERRZONE = $VAR_ZONE]/@FRAME"/> 
		</xsl:variable>
			<xsl:value-of select="$VAR_EXT_CONS_COVA_F"/>
		<xsl:if test="normalize-space($VAR_EXT_CONS_COVA_F) = ''">
				<tr>
					<td class="midcolora"> <li>Frame factor of Exterior construction not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/FRAME") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
		 
		</xsl:when>
    	<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'M'">
    	
    	<xsl:variable name="VAR_EXT_CONS_COVA_M">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovA' and @TERRZONE = $VAR_ZONE]/@MASONRY"/> 
		</xsl:variable> 
			
			<xsl:if test="normalize-space($VAR_EXT_CONS_COVA_M) = ''">
				<tr>
					<td class="midcolora"> <li> Masonary factor of Exterior construction not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/MASONARY")</li>  </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
			<xsl:value-of select="$VAR_EXT_CONS_COVA_M"/>
		
		</xsl:when>
    <xsl:otherwise>0</xsl:otherwise>
    </xsl:choose>
    
</xsl:template>

<!-- calculation of value for earthquake against coverage A  -->

<xsl:template name ="EARTHQUAKE_CALC_COV_A">
	<xsl:param name="FACTORELEMENT" />
	<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE"/>
	<xsl:variable name="P_COVERAGE_A" select="DWELLING_LIMITS"/>
	
	<xsl:variable name="P_RATE_PER">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovA' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE"/> 
	</xsl:variable>
	 	
			<xsl:if test="normalize-space($P_RATE_PER) = ''">
				<tr>
					<td class="midcolora"><li> Ext. Const. rate per value(cov A) not found(Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/RATE_PER_VALUE") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
	<!--
	<xsl:choose> 
		<xsl:when test="EARTHQUAKEDP469 = 'Y' and $P_COVERAGE_A &gt; 0" >
		 
		 <xsl:value-of select="($P_COVERAGE_A div $P_RATE_PER)* $FACTORELEMENT"/>
		</xsl:when>
		 <xsl:otherwise>0</xsl:otherwise>
   </xsl:choose>
   -->

</xsl:template>

<!--  ********************************************************************************************************************** -->
<!-- EARTH QUAKE for Coverage B -->

<xsl:template name ="EARTHQUAKE_COV_B">
 <xsl:variable name="VAR_ZONE"  select="EARTHQUAKEZONE"/>
 
 <xsl:choose>
		<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'F'">
		<xsl:variable name="EXT_CONS_COVB_F">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovB' and @TERRZONE = $VAR_ZONE]/@FRAME"/> 
		</xsl:variable>
			
			<xsl:value-of select="$EXT_CONS_COVB_F"/>
			
			<xsl:if test="normalize-space($EXT_CONS_COVB_F) = ''">
				<tr>
					<td class="midcolora"> <li> Frame factor for coverage B not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/FRAME") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
		
		</xsl:when>
    	<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'M'">
    	<xsl:variable name="EXT_CONS_COVB_M">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovB' and @TERRZONE = $VAR_ZONE]/@MASONRY"/> 
		</xsl:variable>
			
			<xsl:value-of select="$EXT_CONS_COVB_M"/>
			
			<xsl:if test="normalize-space($EXT_CONS_COVB_M) = ''">
				<tr>
					<td class="midcolora"> <li> Masonary factor for coverage B not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/MASONARY") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
		
		
		</xsl:when>
    <xsl:otherwise>0</xsl:otherwise>
    </xsl:choose>
    
</xsl:template>

<!-- calculation of value for earthquake against coverage B  -->

<xsl:template name ="EARTHQUAKE_CALC_COV_B">
	<xsl:param name="FACTORELEMENT" />
	<xsl:variable name="P_APPURTENANTSTRUCTURES_ADDITIONAL" select="APPURTENANTSTRUCTURES_ADDITIONAL"/>
	 <xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE"/>
	<xsl:variable name="P_RATE_PER">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovB' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE"/> 
	</xsl:variable>
	
	<xsl:if test="normalize-space($P_RATE_PER) = ''">
				<tr>
					<td class="midcolora"><li>  Ext. Const. rate per value(cov B) not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/RATE_PER_VALUE")</li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
 <!--
 	<xsl:choose> 
		<xsl:when test="EARTHQUAKEDP469 = 'Y' and  $P_APPURTENANTSTRUCTURES_ADDITIONAL &gt; 0">
			<xsl:value-of select="($P_APPURTENANTSTRUCTURES_ADDITIONAL div $P_RATE_PER)* $FACTORELEMENT"/>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
   </xsl:choose> 
-->

</xsl:template>


<!--  ********************************************************************************************************************** -->
<!-- EARTH QUAKE for Coverage C -->

<xsl:template name ="EARTHQUAKE_COV_C">
 <xsl:variable name="VAR_ZONE"  select="EARTHQUAKEZONE"/>
 
 <xsl:choose>
		<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'F'">
				
		<xsl:variable name="EXT_CONS_COVC_F">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovC' and @TERRZONE = $VAR_ZONE]/@FRAME"/> 
		</xsl:variable>
			
			<xsl:value-of select="$EXT_CONS_COVC_F"/>
			
			<xsl:if test="normalize-space($EXT_CONS_COVC_F) = ''">
				<tr>
					<td class="midcolora"><li>  Frame factor for coverage C not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/FRAME") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		 
		</xsl:when>
    	<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'M'">
		
		<xsl:variable name="EXT_CONS_COVC_M">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovC' and @TERRZONE = $VAR_ZONE]/@MASONRY"/> 
		</xsl:variable>
			
			<xsl:value-of select="$EXT_CONS_COVC_M"/>
			
			<xsl:if test="normalize-space($EXT_CONS_COVC_M) = ''">
				<tr>
					<td class="midcolora"> <li> Masonary factor for coverage C not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/MASONARY") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
		</xsl:when>
    <xsl:otherwise>0</xsl:otherwise>
    </xsl:choose>
    
</xsl:template>

<xsl:template name ="EARTHQUAKE_CALC_COV_C">
	<xsl:param name="FACTORELEMENT" />
	 <xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE"/>
	<xsl:variable name="P_COV_C" select="PERSONALPROPERTY_ADDITIONAL"/>
	<xsl:variable name="P_RATE_PER">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovC' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE"/> 
	</xsl:variable>
	
	<xsl:if test="normalize-space($P_RATE_PER) = ''">
				<tr>
					<td class="midcolora"> <li> Ext. Const. rate per value(cov C) not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/RATE_PER_VALUE")</li>  </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
	<!--
	<xsl:choose> 
		<xsl:when test="EARTHQUAKEDP469 = 'Y' and $P_COV_C &gt; 0 " >
			<xsl:value-of select="($P_COV_C div $P_RATE_PER)* $FACTORELEMENT"/>
		</xsl:when>
		 <xsl:otherwise>0</xsl:otherwise>
   </xsl:choose>
-->

</xsl:template>


<!--  ********************************************************************************************************************** -->

<!-- EARTH QUAKE for Coverage D -->

<xsl:template name ="EARTHQUAKE_COV_D">
 <xsl:variable name="VAR_ZONE"  select="EARTHQUAKEZONE"/>
 
 <xsl:choose>
		<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'F'">
				
		
		<xsl:variable name="EXT_CONS_COVD_F">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovD' and @TERRZONE = $VAR_ZONE]/@FRAME"/> 
		</xsl:variable>
			
			<xsl:value-of select="$EXT_CONS_COVD_F"/>
			
			<xsl:if test="normalize-space($EXT_CONS_COVD_F) = ''">
				<tr>
					<td class="midcolora"><li> Frame factor for coverage D not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/FRAME")</li>  </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
		
		
		</xsl:when>
    	<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'M'">
		
		<xsl:variable name="EXT_CONS_COVD_M">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovD' and @TERRZONE = $VAR_ZONE]/@MASONRY"/> 
		</xsl:variable>
			
			<xsl:value-of select="$EXT_CONS_COVD_M"/>
			
			<xsl:if test="normalize-space($EXT_CONS_COVD_M) = ''">
				<tr>
					<td class="midcolora"> <li> Masonary factor for coverage D not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/MASONARY")</li>  </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
		</xsl:when>
    <xsl:otherwise>0</xsl:otherwise>
    </xsl:choose>
    
</xsl:template>

<xsl:template name ="EARTHQUAKE_CALC_COV_D">
	<!--
	<xsl:param name="FACTORELEMENT" />
	<xsl:variable name="P_COV_D" select="RENTALVALUE_ADDITIONAL"/>
	<xsl:variable name="P_RATE_PER">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovD' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE"/> 
	</xsl:variable>
	<xsl:choose> 
		<xsl:when test="EARTHQUAKEDP469 = 'Y'">
		 
			<xsl:value-of select="round(($P_COV_D div $P_RATE_PER)* $FACTORELEMENT)"/>
		</xsl:when>
		 <xsl:otherwise>0</xsl:otherwise>
   </xsl:choose>
  -->
   <!-- TEMPORARILY SENDING 0. WILL BE REMOVED AFTER CLARIFICATION WITH ASHISH -->
</xsl:template>

<!--  ********************************************************************************************************************** -->
<!-- EARTH QUAKE for Coverage E -->

<xsl:template name ="EARTHQUAKE_COV_E">
 <xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE"/>
 
 <xsl:choose>
		<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'F'">
		 
		<xsl:variable name="EXT_CONS_COVE_F">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovE' and @TERRZONE = $VAR_ZONE]/@FRAME"/> 
		</xsl:variable>
			
			<xsl:value-of select="$EXT_CONS_COVE_F"/>
			
			<xsl:if test="normalize-space($EXT_CONS_COVE_F) = ''">
				<tr>
					<td class="midcolora"> <li> Frame factor for coverage E not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/FRAME")</li>  </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			 
		</xsl:when>
    	<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'M'">
				
			<xsl:variable name="EXT_CONS_COVE_M">
					<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovE' and @TERRZONE = $VAR_ZONE]/@MASONRY"/> 
			</xsl:variable>
			
			<xsl:value-of select="$EXT_CONS_COVE_M"/>
			
			<xsl:if test="normalize-space($EXT_CONS_COVE_M) = ''">
				<tr>
					<td class="midcolora"> <li> Masonary factor for coverage E not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/MASONARY") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
				
		</xsl:when>
    <xsl:otherwise>0</xsl:otherwise>
    </xsl:choose>
    
</xsl:template>

<xsl:template name ="EARTHQUAKE_CALC_COV_E">
	<!--
	<xsl:param name="FACTORELEMENT" />
	<xsl:variable name="P_LIABILITY_EACH_OCCURRENCE_DISPLAY" select="LIABILITY_EACH_OCCURRENCE_DISPLAY"/>
	<xsl:variable name="P_RATE_PER">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovD' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE"/> 
	</xsl:variable>
	<xsl:choose> 
		<xsl:when test="EARTHQUAKEDP469 = 'Y'">		 
			<xsl:value-of select="round(($P_LIABILITY_EACH_OCCURRENCE_DISPLAY div $P_RATE_PER)* $FACTORELEMENT)"/>
		</xsl:when>
		 <xsl:otherwise>0</xsl:otherwise>
   </xsl:choose>
  -->
  
</xsl:template>

<!-- Factor for other coverages -->
<xsl:template name ="EARTHQUAKE_OTHER_COVERAGES_FACTOR">
 <xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE"/>
 
 <xsl:choose>
		<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'F'">
				
		<xsl:variable name="EXT_CONS_OTH_COV_F">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'Other' and @TERRZONE = $VAR_ZONE]/@FRAME"/> 
		</xsl:variable>
			
			<xsl:value-of select="$EXT_CONS_OTH_COV_F"/>
			
			<xsl:if test="normalize-space($EXT_CONS_OTH_COV_F) = ''">
				<tr>
					<td class="midcolora"> <li> Frame factor for Other coverage not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/FRAME") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
		
		</xsl:when>
    	<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'M'">
				
		
		<xsl:variable name="EXT_CONS_OTH_COV_M">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'Other' and @TERRZONE = $VAR_ZONE]/@MASONRY"/> 
		</xsl:variable>
			
			<xsl:value-of select="$EXT_CONS_OTH_COV_M"/>
			
			<xsl:if test="normalize-space($EXT_CONS_OTH_COV_M) = ''">
				<tr>
					<td class="midcolora"> <li> Masonary factor for Other coverage not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/MASONARY") </li>  </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
		
		
		</xsl:when>
    <xsl:otherwise></xsl:otherwise>
    </xsl:choose>
    
</xsl:template>

<!--  ********************************************************************************************************************** -->
<!-- EARTH QUAKE for Building Improvements/Alterations -->

<xsl:template name ="EARTHQUAKE_CALC_BUILDINGIMPROVEMENTS_ADDITIONAL">
	<xsl:param name="FACTORELEMENT" />
	<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE"/>
	<xsl:variable name="P_BUILDINGIMPROVEMENTS_ADDITIONAL" select="BUILDINGIMPROVEMENTS_ADDITIONAL"/>
	<xsl:variable name="P_RATE_PER">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'Other' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE"/> 
	</xsl:variable>

	<xsl:if test="normalize-space($P_RATE_PER) = ''">
				<tr>
					<td class="midcolora"> <li>rate per value for bulding Imp. add. not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/RATE_PER_VALUE") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>

	<!--
	<xsl:choose>
		<xsl:when test="$P_BUILDINGIMPROVEMENTS_ADDITIONAL !='' and $P_BUILDINGIMPROVEMENTS_ADDITIONAL !='NO COVERAGE' and $P_BUILDINGIMPROVEMENTS_ADDITIONAL &gt; 0">
			<xsl:choose> 
				<xsl:when test="EARTHQUAKEDP469 = 'Y'">
					<xsl:value-of select="($P_BUILDINGIMPROVEMENTS_ADDITIONAL div $P_RATE_PER)* $FACTORELEMENT"/>
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
	-->

</xsl:template>


<!--  ********************************************************************************************************************** -->
<!-- EARTH QUAKE for Contents In Storage -->

<xsl:template name ="EARTHQUAKE_CALC_CONTENTSINSTORAGE_ADDITIONAL">
	<xsl:param name="FACTORELEMENT" />
	<xsl:variable name="VAR_ZONE"  select="EARTHQUAKEZONE"/>
	<xsl:variable name="P_CONTENTSINSTORAGE_ADDITIONAL" select="CONTENTSINSTORAGE_ADDITIONAL"/>
	<xsl:variable name="P_RATE_PER">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'Other' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE"/> 
	</xsl:variable>
	
			<xsl:if test="normalize-space($P_RATE_PER) = ''">
				<tr>
					<td class="midcolora"> <li> rate per value for contents instorage additional not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/RATE_PER_VALUE")</li>  </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
	
	<!--
	<xsl:choose>
		<xsl:when test="$P_CONTENTSINSTORAGE_ADDITIONAL !='' and $P_CONTENTSINSTORAGE_ADDITIONAL !='NO COVERAGE' and $P_CONTENTSINSTORAGE_ADDITIONAL &gt; 0">
			<xsl:choose> 
					<xsl:when test="EARTHQUAKEDP469 = 'Y'">
						<xsl:value-of select="($P_CONTENTSINSTORAGE_ADDITIONAL div $P_RATE_PER)* $FACTORELEMENT"/>
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
	-->
	

</xsl:template>


<!--  ********************************************************************************************************************** -->
<!-- EARTH QUAKE for Trees, Lawns, and Shrubs -->
<xsl:template name ="EARTHQUAKE_CALC_TREESLAWNSSHRUBS_ADDITIONAL">
	<xsl:param name="FACTORELEMENT" />
	<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE"/>
	<xsl:variable name="P_TREESLAWNSSHRUBS_ADDITIONAL" select="TREESLAWNSSHRUBS_ADDITIONAL"/>
	<xsl:variable name="P_RATE_PER">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'Other' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE"/> 
	</xsl:variable>
	
	
	<!--
	<xsl:choose>
	<xsl:when test="$P_TREESLAWNSSHRUBS_ADDITIONAL !='' and $P_TREESLAWNSSHRUBS_ADDITIONAL !='NO COVERAGE' and $P_TREESLAWNSSHRUBS_ADDITIONAL &gt; 0">
		<xsl:choose> 
				<xsl:when test="EARTHQUAKEDP469 = 'Y'">
					<xsl:value-of select="($P_TREESLAWNSSHRUBS_ADDITIONAL div $P_RATE_PER)* $FACTORELEMENT"/>
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:when>
	<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
	-->
	

</xsl:template>


<!--  ********************************************************************************************************************** -->
<!-- EARTH QUAKE for Radio and TV Equipment' -->
<xsl:template name ="EARTHQUAKE_CALC_RADIOTV_ADDITIONAL">
	<xsl:param name="FACTORELEMENT" />
	<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE"/>
	<xsl:variable name="P_RADIOTV_ADDITIONAL" select="RADIOTV_ADDITIONAL"/>
	<xsl:variable name="P_RATE_PER">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'Other' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE"/> 
	</xsl:variable>
	
	<!--
	<xsl:choose>
		<xsl:when test="$P_RADIOTV_ADDITIONAL !='' and $P_RADIOTV_ADDITIONAL !='NO COVERAGE' and $P_RADIOTV_ADDITIONAL &gt; 0">
			<xsl:choose> 
					<xsl:when test="EARTHQUAKEDP469 = 'Y'">
						<xsl:value-of select="($P_RADIOTV_ADDITIONAL div $P_RATE_PER)* $FACTORELEMENT"/>
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
	-->
	

</xsl:template>

<!--  ********************************************************************************************************************** -->
<!-- EARTH QUAKE for Satellite Dishes -->
<xsl:template name ="EARTHQUAKE_CALC_SATELLITEDISHES_ADDITIONAL">
	<xsl:param name="FACTORELEMENT" />
	<xsl:variable name="VAR_ZONE"   select="EARTHQUAKEZONE"/>
	<xsl:variable name="P_SATELLITEDISHES_ADDITIONAL" select="SATELLITEDISHES_ADDITIONAL"/>
	<xsl:variable name="P_RATE_PER">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'Other' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE"/> 
	</xsl:variable>
	
	<!--

	<xsl:choose>
		<xsl:when test="$P_SATELLITEDISHES_ADDITIONAL !='' and $P_SATELLITEDISHES_ADDITIONAL !='NO COVERAGE' and $P_SATELLITEDISHES_ADDITIONAL &gt; 0">
				<xsl:choose> 
						<xsl:when test="EARTHQUAKEDP469 = 'Y'">
							<xsl:value-of select="($P_SATELLITEDISHES_ADDITIONAL div $P_RATE_PER)* $FACTORELEMENT"/>
						</xsl:when>
						<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
  --> 
  	


</xsl:template>

<!--  ********************************************************************************************************************** -->
<!-- EARTH QUAKE for Awnings and Canopies -->

<xsl:template name ="EARTHQUAKE_CALC_AWNINGSCANOPIES_ADDITIONAL">
	<xsl:param name="FACTORELEMENT" />
	<xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE"/>
	<xsl:variable name="P_AWNINGSCANOPIES_ADDITIONAL" select="AWNINGSCANOPIES_ADDITIONAL"/>
	<xsl:variable name="P_RATE_PER">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'Other' and @TERRZONE = $VAR_ZONE]/@RATE_PER_VALUE"/> 
	</xsl:variable>
	<!--
	<xsl:choose>
		<xsl:when test="$P_AWNINGSCANOPIES_ADDITIONAL !='' and $P_AWNINGSCANOPIES_ADDITIONAL !='NO COVERAGE' and $P_AWNINGSCANOPIES_ADDITIONAL &gt; 0">
			<xsl:choose> 
				<xsl:when test="EARTHQUAKEDP469 = 'Y'">
					<xsl:value-of select="($P_AWNINGSCANOPIES_ADDITIONAL div $P_RATE_PER)* $FACTORELEMENT"/>
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
	
	-->
	

</xsl:template>

<!-- ============================================================================================ -->
<!--									Earthquake(END)											  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					Installation Floater Building Materials IF-184(START)					  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<xsl:template name ="INSTALLATION_FLOATER_IF_184_BUILDING_MATERIALS">
	<xsl:variable name="P_FLOATERBUILDINGMATERIALS_ADDITIONAL" select="FLOATERBUILDINGMATERIALS_ADDITIONAL"/> 
	<xsl:variable name="P_RATES_PER_VALUE">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSTALLATIONFLOATER']/NODE[@ID ='INSTALLATION_FLOATER']/@RATES_PER_VALUE"/> 
	</xsl:variable>

		<xsl:if test="normalize-space($P_RATES_PER_VALUE) = ''">
				<tr>
					<td class="midcolora"> <li> Rate per value for  installation floater not found  (Path - "INSTALLATIONFLOATER/INSTALLATION_FLOATER/RATES_PER_VALUE") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>

	
	<xsl:variable name="P_FLOATERBUILDINGMATERIALS_ADDITIONAL_FACTOR_AMOUNT">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSTALLATIONFLOATER']/NODE[@ID ='INSTALLATION_FLOATER']/ATTRIBUTES/@BUILDING_MATERIALS"/> 
	</xsl:variable>

		<xsl:if test="normalize-space($P_FLOATERBUILDINGMATERIALS_ADDITIONAL_FACTOR_AMOUNT) = ''">
				<tr>
					<td class="midcolora"> <li> Additional Amount for installation floater not found  (Path - "INSTALLATIONFLOATER/INSTALLATION_FLOATER/ATTRIBUTES/@BUILDING_MATERIALS") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>




<!--
	<xsl:choose>
		<xsl:when test ="$P_FLOATERBUILDINGMATERIALS_ADDITIONAL =''">
			0.00
		</xsl:when>			 
		<xsl:when test ="DWELL_UND_CONSTRUCTION_DP1143 = 'Y' and $P_FLOATERBUILDINGMATERIALS_ADDITIONAL &gt; 0">			 
			<xsl:value-of select="round(($P_FLOATERBUILDINGMATERIALS_ADDITIONAL_FACTOR_AMOUNT div $P_RATES_PER_VALUE) * $P_FLOATERBUILDINGMATERIALS_ADDITIONAL )"/>
		</xsl:when>
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
	-->
</xsl:template>
<!-- ============================================================================================ -->
<!--							Installation Floater IF-184(end)								  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--				Installation Floater NON_Structural Equipment IF-184(START)					  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name ="INSTALLATION_FLOATER_IF_184_NON_STRUCTURAL_EQUIPMENT">
	<xsl:variable name="P_FLOATERNONSTRUCTURAL_ADDITIONAL" select="FLOATERNONSTRUCTURAL_ADDITIONAL"/> 
	
	<xsl:variable name="P_RATES_PER_VALUE">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSTALLATIONFLOATER']/NODE[@ID ='INSTALLATION_FLOATER']/@RATES_PER_VALUE"/> 
	</xsl:variable>
	
	<xsl:variable name="P_FLOATERNONSTRUCTURAL_ADDITIONAL_FACTOR_AMOUNT">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSTALLATIONFLOATER']/NODE[@ID ='INSTALLATION_FLOATER']/ATTRIBUTES/@NON_STRUCTURAL_EQUIPMENT"/> 
	</xsl:variable>
	
		<xsl:if test="normalize-space($P_FLOATERNONSTRUCTURAL_ADDITIONAL_FACTOR_AMOUNT) = ''">
				<tr>
					<td class="midcolora"><li>  Additional Amount for floater non structural not found  (Path - "INSTALLATIONFLOATER/INSTALLATION_FLOATER/ATTRIBUTES/@NON_STRUCTURAL_EQUIPMENT")</li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
	<!--
	<xsl:choose>
		<xsl:when test ="$P_FLOATERNONSTRUCTURAL_ADDITIONAL =''">
			0.00
		</xsl:when>			 
		<xsl:when test ="DWELL_UND_CONSTRUCTION_DP1143 = 'Y' and $P_FLOATERNONSTRUCTURAL_ADDITIONAL &gt; 0">			 
			<xsl:value-of select="round(($P_FLOATERNONSTRUCTURAL_ADDITIONAL_FACTOR_AMOUNT div $P_RATES_PER_VALUE) * $P_FLOATERNONSTRUCTURAL_ADDITIONAL )"/>
		</xsl:when>
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
-->	
</xsl:template>
<!-- ============================================================================================ -->
<!--				Installation Floater NON_Structural Equipment IF-184(START)					  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--			Templates for Mine Subsidence Coverage[Premium]  (START)						  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name ="MINE_SUBSIDENCE_COVERAGE_PREMIUM">
	<xsl:variable name = "P_REPLACEMENTCOSTFACTOR" select ="REPLACEMENTCOSTFACTOR"/>
	 
	 <xsl:variable name ="VAR_MINE_SUB_COV_PREMIUM"> 
	<xsl:choose>
		<xsl:when test ="MINESUBSIDENCEDP480 ='Y' and  STATENAME='INDIANA'">
			 <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINE']/NODE[@ID ='MINE_PREMIUM']/ATTRIBUTES[@MINCOVERAGE &lt;= $P_REPLACEMENTCOSTFACTOR and @MAXCOVERAGE &gt;= $P_REPLACEMENTCOSTFACTOR]/@PREMIUM"/>  
		</xsl:when>
		<xsl:otherwise>
		0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:variable>

		<xsl:if test="normalize-space($VAR_MINE_SUB_COV_PREMIUM) = ''">
			<tr>
				<td class="midcolora"><li> Mine subsidence coverage premium not found  (Path - "MINE/MINE_PREMIUM/ATTRIBUTES/PREMIUM")</li> </td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		

</xsl:template>

<!-- ============================================================================================ -->
<!--			Templates for Mime Subsidence Coverage[Premium]  (END)							  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->




<!-- ============================================================================================ -->
<!--		Templates for Surcharge - Surcharges Factor [Factor]  (START)						  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<xsl:template name ="SURCHARGES">
	<xsl:choose>
		<xsl:when test ="NUMBEROFFAMILIES = 1 and SEASONALSECONDARY != 'Y'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS = '1']/@PRIMARY"/>
		</xsl:when>		
		<xsl:when test ="NUMBEROFFAMILIES = 2 and SEASONALSECONDARY != 'Y'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS = '2']/@PRIMARY"/>
		</xsl:when>		
		<xsl:when test ="NUMBEROFFAMILIES = 1 and SEASONALSECONDARY = 'Y'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS = '1']/@SEASONAL"/>
		</xsl:when>		
		<xsl:when test ="NUMBEROFFAMILIES = 2 and SEASONALSECONDARY = 'Y'">
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS = '2']/@SEASONAL"/>
		</xsl:when>		
		<xsl:otherwise>
		1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- ============================================================================================ -->
<!--		Templates for Surcharge - Surcharges Factor [Factor]  (END)							  -->
<!--		    					  FOR MICHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--						TEMPLATE FOR CALLING ADJUSTEDBASE AS PER THE INPUT-XML (START)		  -->
<!-- ============================================================================================ -->

<xsl:template name ="CALL_ADJUSTEDBASE">
 
	 
	<xsl:call-template name="DEWLLING-MAIN_MICHIGAN"/> 
	<xsl:call-template name ="C_VALUE"/> 
	 
	<!-- ******************************************************************************************* --> 
	 
	 <xsl:variable name="VAR_REPLACEMENT_COST"> 
	 	 <xsl:call-template name ="REPLACEMENT_COST"/>
	 </xsl:variable>
	 	 
	 
	
			<xsl:if test="normalize-space($VAR_REPLACEMENT_COST) = '' ">
				<tr>
					<td class="midcolora">replacement cost factor not found  (Path - "REPLACEMENT_COST_FACTOR/REPLACEMENT_COST/ATTRIBUTES/@FACTOR")</td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if> 
			
	
	<!-- ******************************************************************************************* --> 
	<xsl:variable name="VAR_TERM_FACTOR"> 	 
		<xsl:call-template name ="TERM_FACTOR"/>  
	</xsl:variable>

			<xsl:if test="normalize-space($VAR_TERM_FACTOR) = ''">
				<tr>
					<td class="midcolora">	<li> Term Factor not found  (Path - "TERM_FACTOR/TERMFACTOR/ATTRIBUTES/FACTOR") </li></td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
	<!-- ******************************************************************************************* --> 
	 
	<xsl:variable name="VAR_NO_OF_FAMILY_UNITS_DWELLINGS_FACTOR"> 
		<xsl:call-template name ="NO_OF_FAMILY_UNITS_DWELLINGS_FACTOR"/> 
	</xsl:variable>

			<xsl:if test="normalize-space($VAR_NO_OF_FAMILY_UNITS_DWELLINGS_FACTOR) = ''">
				<tr>
					<td class="midcolora"><li>	No of family units dwellings factor not found (Path - "SURCHARGES/FAMILYUNITSINDWELL/ATTRIBUTES/PRIMARY") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
<!-- ******************************************************************************************* --> 
	
	<xsl:variable name="VAR_DFACTOR">
		<xsl:call-template name = "DFACTOR"/>
	</xsl:variable>
	
		<xsl:if test="normalize-space($VAR_DFACTOR) = ''">
				<tr>
					<td class="midcolora"><li>	 Dwelling Factor not found (Path - "DEDUCTIBLES/OPTIONALDEDUCTIBLE/ATTRIBUTES/FACTOR") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
<!-- ******************************************************************************************* --> 
	
	 <xsl:variable name="V1"><xsl:call-template name = "AGEOFHOME_FACTOR"/></xsl:variable> 
	 <xsl:variable name="V2"><xsl:call-template name = "DUC_DISCOUNT"/></xsl:variable>     
	 <xsl:variable name="V3"><xsl:call-template name = "MULTIPOLICY_DISCOUNT_FACTOR"/></xsl:variable> 
	 <xsl:variable name="V4"><xsl:call-template name = "VALUEDCUSTOMER_DISCOUNT"/></xsl:variable>  
	 <xsl:variable name="V5"><xsl:call-template name ="REPAIR_COST_FACTOR"/></xsl:variable>   
	 
</xsl:template>
		 

<xsl:template name ="TREES_LAWNS_AND_SHRUBS_ADDITIONAL">

	<xsl:variable name="P_RATES_PER_VALUE">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_2']/@RATES_PER_VALUE"/>
	</xsl:variable>
	
		<xsl:if test="normalize-space($P_RATES_PER_VALUE) = ''">
				<tr>
					<td class="midcolora"><li> Rate per value not found  (Path - "OTHERPROPCOVGOPTIONS/DEDUCTIBLE_2/RATES_PER_VALUE") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
 
	
	<!-- get form  code-->
	<xsl:variable name="P_FORM_CODE" select="PRODUCTNAME"/>	
	
	<!-- get deductible -->
	<xsl:variable name="P_DEDUCTIBLE" select="DEDUCTIBLE"/>	
	
	<!-- get factor for trees,lawns etc -->
	<xsl:variable name ="PTREES_LAWNS">	
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_2']/ATTRIBUTES[@NAME = 'TREES_LAWNS_AND_SHRUBS' and @FORM_CODE=$P_FORM_CODE and @DEDUCTIBLE=$P_DEDUCTIBLE]/@DEDUCTIBLE_FACTOR"/>
	</xsl:variable>
	
		<xsl:if test="normalize-space($PTREES_LAWNS) = ''">
				<tr>
					<td class="midcolora"> <li> Deductible factor for tree lawns not found  (Path - "OTHERPROPCOVGOPTIONS/DEDUCTIBLE_2/ATTRIBUTES/DEDUCTIBLE_FACTOR")</li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
	
	<!-- calculate only for additional amount -->
	<!--
	<xsl:choose>
		<xsl:when test = "TREESLAWNSSHRUBS_ADDITIONAL = '' or TREESLAWNSSHRUBS_ADDITIONAL &lt;= 0">
		0.00
		</xsl:when>
		<xsl:otherwise>
				<xsl:variable name="VAR1" select ="TREESLAWNSSHRUBS_ADDITIONAL" />  
				<xsl:value-of select="round(($VAR1 div $P_RATES_PER_VALUE) * $PTREES_LAWNS)"/>
		</xsl:otherwise>
	</xsl:choose>
	-->
</xsl:template>
<!-- ============================================================================================ -->
<!--									Trees, Lawns, and Shrubs (END)		  					  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--									Radio and TV Equipment (START)		  					  -->
<!-- ============================================================================================ -->

<xsl:template name ="RADIO_AND_TV_EQUIPMENT_ADDITIONAL">
	<xsl:variable name="P_RATES_PER_VALUE">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_2']/@RATES_PER_VALUE"/>
	</xsl:variable>
	<!-- get form  code-->
	<xsl:variable name="P_FORM_CODE" select="PRODUCTNAME"/>	
	
	<!-- get deductible -->
	<xsl:variable name="P_DEDUCTIBLE" select="DEDUCTIBLE"/>	
	
	<!-- get factor for Radio and TV Equipment -->
	<xsl:variable name ="PRADIOTV_ADDITIONAL">	
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_2']/ATTRIBUTES[@NAME = 'RADIO_AND_TV_EQUIPMENT' and @FORM_CODE=$P_FORM_CODE and @DEDUCTIBLE=$P_DEDUCTIBLE]/@DEDUCTIBLE_FACTOR"/>
	</xsl:variable>
		
		<xsl:if test="normalize-space($PRADIOTV_ADDITIONAL) = ''">
				<tr>
					<td class="midcolora"><li> Deductible factor for radio TV additional not found  (Path - "OPTIONALSECTIONCOVGES/DEDUCTIBLE_2/ATTRIBUTES/DEDUCTIBLE_FACTOR") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
 
 
		
	<!-- calculate only for additional amount -->
	<!--
	<xsl:choose>
		<xsl:when test = "RADIOTV_ADDITIONAL = '' or RADIOTV_ADDITIONAL &lt;= 0">
		0.00
		</xsl:when>
		<xsl:otherwise>
			<xsl:variable name="VAR1" select ="RADIOTV_ADDITIONAL" />  
			<xsl:value-of select="round(($VAR1 div $P_RATES_PER_VALUE) * $PRADIOTV_ADDITIONAL)"/>
		</xsl:otherwise>
	</xsl:choose>
-->	
</xsl:template>
<!-- ============================================================================================ -->
<!--									Radio and TV Equipment (END)		  					  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--									Satellite Dishes  (START)			  					  -->
<!-- ============================================================================================ -->
<xsl:template name ="SATELLITE_DISHES_ADDITIONAL">
	<xsl:variable name="P_RATES_PER_VALUE">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_2']/@RATES_PER_VALUE"/>
	</xsl:variable>
	
	<!-- get form  code-->
	<xsl:variable name="P_FORM_CODE" select="PRODUCTNAME"/>	
	
	<!-- get deductible -->
	<xsl:variable name="P_DEDUCTIBLE" select="DEDUCTIBLE"/>	
	
	<!-- get factor for Satellite Dishes -->
	<xsl:variable name ="PSATELLITE_DISHES_ADDITIONAL">	
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_2']/ATTRIBUTES[@NAME = 'SATELLITE_DISHES' and @FORM_CODE=$P_FORM_CODE and @DEDUCTIBLE=$P_DEDUCTIBLE]/@DEDUCTIBLE_FACTOR"/>
	</xsl:variable>

		<xsl:if test="normalize-space($PSATELLITE_DISHES_ADDITIONAL) = ''">
				<tr>
					<td class="midcolora"> <li> Deductible factor for satellite dishes additional not found  (Path - "OPTIONALSECTIONCOVGES/DEDUCTIBLE_2/ATTRIBUTES[SATELLITE_DISHES]/DEDUCTIBLE_FACTOR") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
		
	<!-- calculate only for additional amount -->
	<!--
	<xsl:choose>
		<xsl:when test = "SATELLITEDISHES_ADDITIONAL = '' or SATELLITEDISHES_ADDITIONAL &lt;= 0">
			0.00
		</xsl:when>
		<xsl:otherwise>
			<xsl:variable name="VAR1" select ="SATELLITEDISHES_ADDITIONAL" />  
			<xsl:value-of select="round(($VAR1 div $P_RATES_PER_VALUE) * $PSATELLITE_DISHES_ADDITIONAL)"/>
		</xsl:otherwise>
	</xsl:choose>
-->	
</xsl:template>

<xsl:template name ="SATELLITEDISHES_COMBINE">
	<xsl:variable name="VAR1" select="SATELLITEDISHES_INCLUDE"/>
	<xsl:variable name="VAR2" select="SATELLITEDISHES_ADDITIONAL"/>
	<xsl:value-of select="$VAR1+$VAR2"></xsl:value-of>


</xsl:template>

<!-- ============================================================================================ -->
<!--									Satellite Dishes  (END)				  					  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--									Awnings and Canopies  (START)			  				  -->
<!-- ============================================================================================ -->
<xsl:template name ="AWNINGS_AND_CANOPIES_ADDITIONAL">
	<xsl:variable name="P_RATES_PER_VALUE">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_2']/@RATES_PER_VALUE"/>
	</xsl:variable>
	<!-- get form  code-->
	<xsl:variable name="P_FORM_CODE" select="PRODUCTNAME"/>	
	
	<!-- get deductible -->
	<xsl:variable name="P_DEDUCTIBLE" select="DEDUCTIBLE"/>	
	
	<!-- get factor for Awnings and Canopies -->
	<xsl:variable name ="PAWNINGS_AND_CANOPIES_ADDITIONAL">	
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_2']/ATTRIBUTES[@NAME = 'AWNINGS_CANOPIES' and @FORM_CODE=$P_FORM_CODE and @DEDUCTIBLE=$P_DEDUCTIBLE]/@DEDUCTIBLE_FACTOR"/>
	</xsl:variable>
		
		<xsl:if test="normalize-space($PAWNINGS_AND_CANOPIES_ADDITIONAL) = ''">
				<tr>
					<td class="midcolora"><li>  Deductible factor for pawnings and canopies additional not found  (Path - "OPTIONALSECTIONCOVGES/DEDUCTIBLE_2/ATTRIBUTES[AWNINGS_CANOPIES]/DEDUCTIBLE_FACTOR")  </li></td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>

		
		
	<!-- calculate only for additional amount -->
	<!--
	<xsl:choose>
		<xsl:when test = "AWNINGSCANOPIES_ADDITIONAL = '' or AWNINGSCANOPIES_ADDITIONAL &lt;= 0">
		0.00
		</xsl:when>
		<xsl:otherwise>
			<xsl:variable name="VAR1" select ="AWNINGSCANOPIES_ADDITIONAL" />  
			<xsl:value-of select="round(($VAR1 div $P_RATES_PER_VALUE) * $PAWNINGS_AND_CANOPIES_ADDITIONAL)"/>
		</xsl:otherwise>
	</xsl:choose>
	-->
</xsl:template>
<!-- ============================================================================================ -->
<!--									Awnings and Canopies  (END)				  				  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--							Appurtenant Structures (Coverage B)  (START)			  		  -->
<!-- ============================================================================================ -->
<xsl:template name ="COVERAGE_B_ADDITIONAL">
	<xsl:variable name="P_RATES_PER_VALUE">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/@RATES_PER_VALUE"/>
	</xsl:variable>
	
	
<xsl:if test="normalize-space($P_RATES_PER_VALUE) =''">
	<tr>
		<td class="midcolora">	<li> cov B additional value not found (Path - "OTHERPROPCOVGOPTIONS/DEDUCTIBLE_1/RATES_PER_VALUE") </li> </td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
</xsl:if>



	<!-- get form  code-->
	<xsl:variable name="P_FORM_CODE" select="PRODUCTNAME"/>	
	
	<!-- get deductible -->
	<xsl:variable name="P_DEDUCTIBLE" select="DEDUCTIBLE"/>	
	
	<!-- get factor for Appurtenant Structures (Coverage B) -->
	<xsl:variable name ="PCOVERAGE_B_ADDITIONAL">	
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'COVERAGE_B' and @FORM_CODE=$P_FORM_CODE and @DEDUCTIBLE=$P_DEDUCTIBLE]/@DEDUCTIBLE_FACTOR"/>
	</xsl:variable>
	
	<xsl:if test="normalize-space($PCOVERAGE_B_ADDITIONAL) =''">
	<tr>
		<td class="midcolora"><li>	 cov B Deductible factor not found (Path - "OTHERPROPCOVGOPTIONS/DEDUCTIBLE_1/ATTRIBUTES/DEDUCTIBLE_FACTOR") </li> </td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
</xsl:if>


		
	<!-- calculate only for additional amount -->
	<xsl:variable name="VAR_APPURTENANTSTRUCTURES_ADDITIONAL"> 
	<xsl:choose>
		<xsl:when test = "APPURTENANTSTRUCTURES_ADDITIONAL ='' or APPURTENANTSTRUCTURES_ADDITIONAL &lt;= 0">
		0.00
		</xsl:when>
		<xsl:otherwise>
			 
			<xsl:variable name="VAR1" select ="APPURTENANTSTRUCTURES_ADDITIONAL" />			
			<xsl:value-of select="round(($VAR1 div $P_RATES_PER_VALUE) * $PCOVERAGE_B_ADDITIONAL)"/> 
			  
		</xsl:otherwise>
	</xsl:choose>
</xsl:variable>






</xsl:template>
<!-- ============================================================================================ -->
<!--							Appurtenant Structures (Coverage B)  (END)				  		  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--							Building Improvements/Alteration  (START)				  		  -->
<!-- ============================================================================================ -->
<xsl:template name ="BUILDING_IMPROVEMENTS_ADDITIONAL">
	<xsl:variable name="P_RATES_PER_VALUE">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/@RATES_PER_VALUE"/>
	</xsl:variable>
	<!-- get form  code-->
	<xsl:variable name="P_FORM_CODE" select="PRODUCTNAME"/>	
	
	<!-- get deductible -->
	<xsl:variable name="P_DEDUCTIBLE" select="DEDUCTIBLE"/>	
	
	<!-- get factor for Building Improvements/Alteration -->
		<xsl:variable name ="PBUILDING_IMPROVEMENTS_ADDITIONAL">	
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'BUILDING_IMPROVEMENTS_AND_ALTERATIONS' and @FORM_CODE=$P_FORM_CODE and @DEDUCTIBLE=$P_DEDUCTIBLE]/@DEDUCTIBLE_FACTOR"/>
		</xsl:variable>
		
		<xsl:if test="normalize-space($PBUILDING_IMPROVEMENTS_ADDITIONAL) = ''">
			<tr>
				<td class="midcolora"> <li> Deductible factor for building improvements additional not found  (Path - "OPTIONALSECTIONCOVGES/DEDUCTIBLE_1/ATTRIBUTES[BUILDING_IMPROVEMENTS_AND_ALTERATIONS]/DEDUCTIBLE_FACTOR") </li> </td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
			
	<!-- calculate only for additional amount -->
	<!--
	<xsl:choose>
		<xsl:when test = "BUILDINGIMPROVEMENTS_ADDITIONAL = '' or BUILDINGIMPROVEMENTS_ADDITIONAL &lt;= 0">
		0.00
		</xsl:when>
		<xsl:otherwise>
			<xsl:variable name="VAR1" select ="BUILDINGIMPROVEMENTS_ADDITIONAL" />  
			<xsl:value-of select="round(($VAR1 div $P_RATES_PER_VALUE) * $PBUILDING_IMPROVEMENTS_ADDITIONAL)"/>
		</xsl:otherwise>
	</xsl:choose>
	-->
</xsl:template>
<!-- ============================================================================================ -->
<!--							Building Improvements/Alteration  (END)					  		  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--							 Increased Coverage D - Rental Value  (START)			  		  -->
<!-- ============================================================================================ -->
<xsl:template name ="INCREASED_COVERAGE_D_ADDITIONAL">
	
	<xsl:variable name="P_RATES_PER_VALUE">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/@RATES_PER_VALUE"/>
	</xsl:variable>
	<!-- get form  code-->
	<xsl:variable name="P_FORM_CODE" select="PRODUCTNAME"/>	
	
	<!-- get deductible -->
	<xsl:variable name="P_DEDUCTIBLE" select="DEDUCTIBLE"/>	
	
	<!-- get factor for Increased Coverage D - Rental Value -->
		<xsl:variable name ="PRENTAL_VALUE_ADDITIONAL">	
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'RENTAL_VALUE' and @FORM_CODE=$P_FORM_CODE and @DEDUCTIBLE=$P_DEDUCTIBLE]/@DEDUCTIBLE_FACTOR"/>
		</xsl:variable>
		
		<xsl:if test="normalize-space($PRENTAL_VALUE_ADDITIONAL) = ''">
			<tr>
				<td class="midcolora"> <li> Deductible factor for rental value additional not found  (Path - "OPTIONALSECTIONCOVGES/DEDUCTIBLE_1/ATTRIBUTES[RENTAL_VALUE]/DEDUCTIBLE_FACTOR") </li> </td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>



	<!-- calculate only for additional amount -->
	<!--
	<xsl:choose>
		<xsl:when test = "RENTALVALUE_ADDITIONAL = '' or RENTALVALUE_ADDITIONAL &lt;= 0">
		0.00
		</xsl:when>
		<xsl:otherwise>
			<xsl:variable name="VAR1" select ="RENTALVALUE_ADDITIONAL" />  
			<xsl:value-of select="($VAR1 div $P_RATES_PER_VALUE) * $PRENTAL_VALUE_ADDITIONAL"/>
		</xsl:otherwise>
	</xsl:choose>
	-->
</xsl:template>
<!-- ============================================================================================ -->
<!--							 Increased Coverage D - Rental Value  (END)				  		  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--					 Increased Coverage C - Landlords Personal Property  (START)			  -->
<!-- ============================================================================================ -->
<xsl:template name ="COVERAGE_C_ADDITIONAL">
	
	<xsl:variable name="P_RATES_PER_VALUE">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/@RATES_PER_VALUE"/>
	</xsl:variable>
	
	<!-- get form  code-->
	<xsl:variable name="P_FORM_CODE" select="PRODUCTNAME"/>	
	
	<!-- get deductible -->
	<xsl:variable name="P_DEDUCTIBLE" select="DEDUCTIBLE"/>	
	
	<!-- get factor for Increased Coverage C - Landlords Personal Property -->
		<xsl:variable name ="PCOVERAGE_C_ADDITIONAL">	
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'COVERAGE_C' and @FORM_CODE=$P_FORM_CODE and @DEDUCTIBLE=$P_DEDUCTIBLE]/@DEDUCTIBLE_FACTOR"/>
		</xsl:variable>
		
		<xsl:if test="normalize-space($PCOVERAGE_C_ADDITIONAL) = ''">
			<tr>
				<td class="midcolora"><li> Deductible factor for Increased Coverage C not found  (Path - "OPTIONALSECTIONCOVGES/DEDUCTIBLE_1/ATTRIBUTES[COVERAGE_C]/DEDUCTIBLE_FACTOR") </li> </td>
			</tr>
			<xsl:if test="user:AllVerified(0)=1"></xsl:if>
		</xsl:if>
		
		
	<!-- calculate only for additional amount -->
	<!--
	<xsl:choose>
		<xsl:when test = "PERSONALPROPERTY_ADDITIONAL = '' or PERSONALPROPERTY_ADDITIONAL &lt;=0">
		0.00
		</xsl:when>
		<xsl:otherwise>
			<xsl:variable name="VAR1" select ="PERSONALPROPERTY_ADDITIONAL" />  
			<xsl:value-of select="($VAR1 div $P_RATES_PER_VALUE) * $PCOVERAGE_C_ADDITIONAL"/>
		</xsl:otherwise>
	</xsl:choose>
-->	
</xsl:template>
<!-- ============================================================================================ -->
<!--							 Increased Coverage C - Landlords Personal Property   (END)		  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--									Contents In Storage (START)								  -->
<!-- ============================================================================================ -->
<xsl:template name ="CONTENTS_IN_STORAGE_ADDITIONAL">
	
	<xsl:variable name="P_RATES_PER_VALUE">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/@RATES_PER_VALUE"/>
	</xsl:variable>
	
	<!-- get form  code-->
	<xsl:variable name="P_FORM_CODE" select="PRODUCTNAME"/>	
	
	<!-- get deductible -->
	<xsl:variable name="P_DEDUCTIBLE" select="DEDUCTIBLE"/>	
	
	<!-- get factor for Contents In Storage -->
		<xsl:variable name ="PCONTENTSINSTORAGE_ADDITIONAL">	
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OTHERPROPCOVGOPTIONS']/NODE[@ID ='DEDUCTIBLE_1']/ATTRIBUTES[@NAME = 'CONTENTS_IN_STORAGE' and @FORM_CODE=$P_FORM_CODE and @DEDUCTIBLE=$P_DEDUCTIBLE]/@DEDUCTIBLE_FACTOR"/>
		</xsl:variable>
		
		<xsl:if test="normalize-space($PCONTENTSINSTORAGE_ADDITIONAL) = ''">
				<tr>
					<td class="midcolora"> <li> Deductible factor Contents instorage additional not found  (Path - "OTHERPROPCOVGOPTIONS/DEDUCTIBLE_1/ATTRIBUTES/DEDUCTIBLE_FACTOR")</li>  </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
 
		
	<!-- calculate only for additional amount -->	 
	<!--
	<xsl:choose>
		<xsl:when test = "CONTENTSINSTORAGE_ADDITIONAL = '' or CONTENTSINSTORAGE_ADDITIONAL &lt;= 0">
			0.00
		</xsl:when>
		<xsl:otherwise>
			<xsl:variable name="VAR1" select ="CONTENTSINSTORAGE_ADDITIONAL" />  
			<xsl:value-of select="round(($VAR1 div $P_RATES_PER_VALUE) * $PCONTENTSINSTORAGE_ADDITIONAL)"/>
		</xsl:otherwise>
	</xsl:choose>
	-->
</xsl:template>
<!-- ============================================================================================ -->
<!--									Contents In Storage (END)								  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--						TEMPLATE FOR SECTION II LANDLORDS LIABILITY(LP-124) (START)			  -->
<!-- ============================================================================================ -->


<xsl:template name="PROPFEE">
	
	<xsl:choose>
		
		<xsl:when test ="TERMFACTOR = 12 and STATENAME ='MICHIGAN'">
			<xsl:variable name="VAR_PROPFEE_MI_ANNUAL"> 
				  <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PROPRTYFEE']/NODE[@ID='PFEE']/ATTRIBUTES/@ANNUAL_FEES"/> 
			</xsl:variable>
	
			<xsl:if test="normalize-space($VAR_PROPFEE_MI_ANNUAL) = ''">
				<tr>
					<td class="midcolora"> <li> Property fee not found  (Path - "PROPRTYFEE/PFEE/ATTRIBUTES/ANNUAL_FEES") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		</xsl:when>
		
		<xsl:when test ="TERMFACTOR = 6 and STATENAME ='MICHIGAN'">
			<xsl:variable name="VAR_PROPFEE_MI_ANNUAL"> 
			 	<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PROPRTYFEE']/NODE[@ID='PFEE']/ATTRIBUTES/@SEMI_ANNUAL_FEES"/> 
			</xsl:variable>
	
			<xsl:if test="normalize-space($VAR_PROPFEE_MI_ANNUAL) = ''">
				<tr>
					<td class="midcolora"> <li> Property fee not found  (Path - "PROPRTYFEE/PFEE/ATTRIBUTES/SEMI_ANNUAL_FEES") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>

		</xsl:when>
		<xsl:otherwise></xsl:otherwise>

	</xsl:choose>
	
</xsl:template>

<!-- ============================================================================================ -->
<!--							Template Property Expense Fee (START)							  -->
<!--		    							FOR MICHIGAN										  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--									Additional Coverage Template (START) 					  -->
<!-- ============================================================================================ -->

<xsl:template name ="ADDITIONALCOVERAGE">
	
	<!-- Section II: Liability Charges --> 	 
	<xsl:variable name="VAR1"><xsl:call-template name="LP_124_LANDLORDS_LIABILITY"/></xsl:variable>	 
	<xsl:variable name="VAR2"><xsl:call-template name="PROPFEE"/></xsl:variable>
	 
	
	<!-- Mine Subsidence -->	 
	<xsl:variable name="VAR3"><xsl:call-template name="MINE_SUBSIDENCE_COVERAGE_PREMIUM"/></xsl:variable>
	 
	<!-- Other Endorsements and Coverages -->	 
	<xsl:variable name="VAR4"><xsl:call-template name="EARTHQUAKE"/></xsl:variable>  
	<xsl:variable name="VAR5"><xsl:call-template name="INSTALLATION_FLOATER_IF_184_BUILDING_MATERIALS"/></xsl:variable>
	<xsl:variable name="VAR6"><xsl:call-template name="INSTALLATION_FLOATER_IF_184_NON_STRUCTURAL_EQUIPMENT"/></xsl:variable>
	<xsl:variable name="VAR7"><xsl:call-template name="TREES_LAWNS_AND_SHRUBS_ADDITIONAL"/></xsl:variable>
	<xsl:variable name="VAR8"><xsl:call-template name="RADIO_AND_TV_EQUIPMENT_ADDITIONAL"/></xsl:variable>
	<xsl:variable name="VAR9"><xsl:call-template name="SATELLITE_DISHES_ADDITIONAL"/></xsl:variable>
	<xsl:variable name="VAR10"><xsl:call-template name="AWNINGS_AND_CANOPIES_ADDITIONAL"/></xsl:variable>
	<xsl:variable name="VAR11"><xsl:call-template name="COVERAGE_B_ADDITIONAL"/></xsl:variable>
	<xsl:variable name="VAR12"><xsl:call-template name="BUILDING_IMPROVEMENTS_ADDITIONAL"/></xsl:variable>
	<xsl:variable name="VAR13"><xsl:call-template name="INCREASED_COVERAGE_D_ADDITIONAL"/></xsl:variable>
	<xsl:variable name="VAR14"><xsl:call-template name="COVERAGE_C_ADDITIONAL"/></xsl:variable>
	<xsl:variable name="VAR15"><xsl:call-template name="CONTENTS_IN_STORAGE_ADDITIONAL"/></xsl:variable>
	 
	<!-- Calculations -->	 
	<xsl:value-of select="round(round(round(round(round(round(round(round(round(round(round(round(round(round($VAR1+$VAR2)+$VAR3)+$VAR4)+$VAR5)+$VAR6)+$VAR7)+$VAR8)+$VAR9)+$VAR10)+$VAR11)+$VAR12)+$VAR13)+$VAR14)+$VAR15)"/>
	
</xsl:template>

<!-- ============================================================================================ -->
<!--									Additional Coverage Template (END)		  				  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--									Final Primium Template (START)   						  -->
<!-- ============================================================================================ -->
<xsl:template name ="FINALPRIMIUM">

<xsl:variable name="VAR_CALLADJUSTED"> 
	<xsl:call-template name="CALL_ADJUSTEDBASE"/> 
</xsl:variable>

<xsl:variable name="VAR_ADDITIONALCOVERAGE"> 
	<xsl:call-template name="ADDITIONALCOVERAGE"/>
</xsl:variable>		

<xsl:variable name="VAR_MINIMUM_PREMIUM"> 
	<xsl:call-template name="MINIMUM_PREMIUM"/>
</xsl:variable>	

<xsl:value-of select="$VAR_CALLADJUSTED + $VAR_ADDITIONALCOVERAGE + $VAR_MINIMUM_PREMIUM"/> 	

</xsl:template>
<!-- ============================================================================================ -->
<!--									Final Primium Template (END)   							  -->
<!-- ============================================================================================ -->

<xsl:template name ="EARTHQUAKE_COVERAGE"> 
	<xsl:if test="STATENAME='MICHIGAN'">
		DP-469 Earthquake
	</xsl:if>
	<xsl:if test="STATENAME='INDIANA'">
		DP-470 Earthquake 
	</xsl:if>
</xsl:template>


<xsl:template name="REPAIR_COST_FACTOR">

	<xsl:variable name="VAR_REPAIR_COST_FACTOR">
		<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='REPAIR_COST_FACTOR']/NODE[@ID='REPAIR_COST']/ATTRIBUTES/@FACTOR"/>
	</xsl:variable>

			<xsl:if test="normalize-space($VAR_REPAIR_COST_FACTOR) = ''">
				<tr>
					<td class="midcolora"> <li> Repair cost factor not found  (Path - "REPAIR_COST_FACTOR/REPAIR_COST/ATTRIBUTES/FACTOR"") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
</xsl:template>




<!-- ============================================================================================ -->
<!--					Templates for MINIMUM PREMIUM (START)									  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
<!-- ============================================================================================ -->
<!--
	<xsl:template name="MINIMUM_PREMIUM">
		 
		<xsl:variable name="DIFFERENCE_AMOUNT" >		
				<xsl:call-template name="PREMIUM_DIFFERENCE"/>
		</xsl:variable>
	
		 
		IF (<xsl:call-template name="PREMIUM_DIFFERENCE"/> &gt; 0)
		THEN
			<xsl:call-template name="PREMIUM_DIFFERENCE"/>
		ELSE
			0
		
	</xsl:template>
	
	
	<xsl:template name="PREMIUM_DIFFERENCE">
		  Subtract Minimum Premium  from the total premium.  
		(
			<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_RENTAL_PREMIUM']/NODE[@ID ='MINIMUM_PREMIUM']/ATTRIBUTES/@MINIMUM_VALUE "/>
			-
			(<xsl:call-template name="CALL_ADJUSTEDBASE"/>  +
			 <xsl:call-template name="ADDITIONALCOVERAGE"/>)
		) 
	</xsl:template>
	-->
<!-- ============================================================================================ -->
<!--					Templates for MINIMUM PREMIUM (END)											  -->
<!--		    					  FOR INDIANA,MICHIGAN,WISCONSIN							  -->
<!-- ============================================================================================ -->


<xsl:template name="MAIN_FOR_INPUT_XML">
<xsl:if test="user:AllVerified(1) = 1">
			<b> Error Details of XML </b>
</xsl:if>
		
<xsl:variable name="VAR_TERRITORYCODES" select="TERRITORYCODES"/> 
<xsl:if test="normalize-space($VAR_TERRITORYCODES) = ''">
	<tr>
		<td class="midcolora"> <li> The Node for Territory Code is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
</xsl:if>

<xsl:variable name="VAR_INSURANCESCORE" select="INSURANCESCORE"/> 
<xsl:if test="normalize-space($VAR_INSURANCESCORE) = ''">
	<tr>
		<td class="midcolora"> <li>The Node for insurance score is blank </li> </td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
</xsl:if>

<xsl:variable name="VAR_TERRITORYZONE" select="TERRITORYZONE"/> 
<xsl:if test="normalize-space($VAR_TERRITORYZONE) = ''">
	<tr>
		<td class="midcolora"> <li> The Node for territory zone is blank </li> </td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
</xsl:if>



<xsl:variable name="VAR_TERRITORYNAME" select="TERRITORYNAME"/> 
<xsl:if test="normalize-space($VAR_TERRITORYNAME) = ''">
	<tr>
		<td class="midcolora"> <li> The Node for territory name is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
</xsl:if>

		 
<xsl:variable name="VAR_TERRITORYCOUNTY" select="TERRITORYCOUNTY"/> 
<xsl:if test="normalize-space($VAR_TERRITORYCOUNTY) = ''">
	<tr>
		<td class="midcolora"> <li> The Node for territory county is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
</xsl:if>

<!--		
<xsl:variable name="VAR_EARTHQUAKEZONE" select="EARTHQUAKEZONE"/> 
<xsl:if test="normalize-space($VAR_EARTHQUAKEZONE) = ''">
	<tr>
		<td class="midcolora">The Node for earthquake zone is blank </td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
</xsl:if>
--> 
		
		<xsl:variable name="VAR_STATENAME" select="STATENAME"/> 
<xsl:if test="normalize-space($VAR_STATENAME) = ''">
	<tr>
		<td class="midcolora"> <li> The Node for state name is blank </li></td>
	</tr>
	<xsl:if test="user:AllVerified(0)=1"></xsl:if>
</xsl:if>

		
<xsl:variable name="VAR_NEWBUSINESSFACTOR" select="normalize-space(NEWBUSINESSFACTOR)"/> 
<xsl:if test="$VAR_NEWBUSINESSFACTOR != 'N' and $VAR_NEWBUSINESSFACTOR !='Y'">
	<tr>
		<td class="midcolora"> <li> The Node for newbusiness factor is blank </li></td>
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


	 
		<xsl:variable name="VAR_TERMFACTOR" select="TERMFACTOR"/> 
			<xsl:if test="normalize-space($VAR_TERMFACTOR) = ''">
				<tr>
					<td class="midcolora"> <li>The node for term factor is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>


		
		<xsl:variable name="VAR_PRODUCTNAME" select="PRODUCTNAME"/> 
			<xsl:if test="normalize-space($VAR_PRODUCTNAME) = ''">
				<tr>
					 <td class="midcolora"><li> The Node for product name is blank  </li></td> 
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>


		
		<xsl:variable name="VAR_PRODUCT_PREMIER" select="PRODUCT_PREMIER"/> 
			<xsl:if test="normalize-space($VAR_PRODUCT_PREMIER) = ''">
				<tr>
					 <td class="midcolora"><li> The Node for product premier is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
		 <xsl:variable name="VAR_SEASONALSECONDARY" select="normalize-space(SEASONALSECONDARY)"/> 
			<xsl:if test="$VAR_SEASONALSECONDARY != 'Y' and $VAR_SEASONALSECONDARY != 'N'">
				<tr>
					<td class="midcolora"><li>The node for seasonal secondary is blank </li> </td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>


		
		<xsl:variable name="VAR_REPLACEMENTCOSTFACTOR" select="REPLACEMENTCOSTFACTOR"/> 
			<xsl:if test="normalize-space($VAR_REPLACEMENTCOSTFACTOR) = ''">
				<tr>
					<td class="midcolora"><li>The Node for replacement cost factor is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>

		
		<xsl:variable name="VAR_DWELLING_LIMITS" select="DWELLING_LIMITS"/> 
			<xsl:if test="normalize-space($VAR_DWELLING_LIMITS) = ''">
				<tr>
					<td class="midcolora"><li>The Node for dwelling limits is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>

		
		<xsl:variable name="VAR_DEDUCTIBLE" select="DEDUCTIBLE"/> 
			<xsl:if test="normalize-space($VAR_DEDUCTIBLE) = ''">
				<tr>
					<td class="midcolora"><li>The Node for deductible is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>

		
		
			
		<xsl:variable name="VAR_PROTECTIONCLASS" select="PROTECTIONCLASS"/> 
			<xsl:if test="normalize-space($VAR_PROTECTIONCLASS) = ''">
				<tr>
					<td class="midcolora"><li>The Node for protection class is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>

<!-- *************************************************************************  -->


		
		<xsl:variable name="VAR_DISTANCET_FIRESTATION" select="DISTANCET_FIRESTATION"/> 
			<xsl:if test="normalize-space($VAR_DISTANCET_FIRESTATION) = ''">
				<tr>
					<td class="midcolora"><li>The node for distancet firestation is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
		
		<xsl:variable name="VAR_FEET2HYDRANT" select="FEET2HYDRANT"/> 
			<xsl:if test="normalize-space($VAR_FEET2HYDRANT) = ''">
				<tr>
					<td class="midcolora"><li>The node for feet to hydrant is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
		
		<xsl:variable name="VAR_EXTERIOR_CONSTRUCTION" select="EXTERIOR_CONSTRUCTION"/> 
			<xsl:if test="normalize-space($VAR_EXTERIOR_CONSTRUCTION) = ''">
				<tr>
					<td class="midcolora"><li>The node for exterior construction is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
		
		
		<xsl:variable name="VAR_EXTERIOR_CONSTRUCTION_DESC" select="EXTERIOR_CONSTRUCTION_DESC"/> 
			<xsl:if test="normalize-space($VAR_EXTERIOR_CONSTRUCTION_DESC) = ''">
				<tr>
					<td class="midcolora"><li>The node for exterior construction description is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
		
		<xsl:variable name="VAR_EXTERIOR_CONSTRUCTION_F_M" select="EXTERIOR_CONSTRUCTION_F_M"/> 
			<xsl:if test="normalize-space($VAR_EXTERIOR_CONSTRUCTION_F_M) = ''">
				<tr>
					<td class="midcolora"><li>The Node for exterior construction Frame Masaonry is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
		
		
		<xsl:variable name="VAR_DWELL_UND_CONSTRUCTION_DP1143" select="DWELL_UND_CONSTRUCTION_DP1143"/> 
			<xsl:if test="normalize-space($VAR_DWELL_UND_CONSTRUCTION_DP1143) = ''">
				<tr>
					<td class="midcolora"><li>The Node for dwelling under construction (DP1143) is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
		
		
		<xsl:variable name="VAR_DOC" select="DOC"/> 
			<xsl:if test="normalize-space($VAR_DOC) = ''">
				<tr>
					<td class="midcolora"><li>The node for Date of construction (DOC) is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
		
		
		<xsl:variable name="VAR_AGEOFHOME" select="AGEOFHOME"/> 
			<xsl:if test="normalize-space($VAR_AGEOFHOME) = ''">
				<tr>
					<td class="midcolora"><li>The node for age of home is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
		
		
		<xsl:variable name="VAR_NUMBEROFFAMILIES" select="NUMBEROFFAMILIES"/> 
			<xsl:if test="normalize-space($VAR_NUMBEROFFAMILIES) = ''">
				<tr>
					<td class="midcolora"><li>The node for number of families is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
		
		
		<xsl:variable name="VAR_N0_LOCAL_ALARM" select="N0_LOCAL_ALARM"/> 
			<xsl:if test="normalize-space($VAR_N0_LOCAL_ALARM) = ''">
				<tr>
					<td class="midcolora"><li>The node for no local alarm is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
		
		<xsl:variable name="VAR_NO_YEARS_WITH_WOLVERINE" select="NO_YEARS_WITH_WOLVERINE"/> 
			<xsl:if test="normalize-space($VAR_NO_YEARS_WITH_WOLVERINE) = ''">
				<tr>
					<td class="midcolora"><li>The node for number of years with wolverine is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
		
		<xsl:variable name="VAR_LOSSFREE" select="LOSSFREE"/> 
			<xsl:if test="normalize-space($VAR_LOSSFREE) = ''">
				<tr>
					<td class="midcolora"><li>The Node for loss free is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
		
		<xsl:variable name="VAR_MULTIPLEPOLICYFACTOR" select="normalize-space(MULTIPLEPOLICYFACTOR)"/> 
			<xsl:if test="$VAR_MULTIPLEPOLICYFACTOR != 'Y' and $VAR_MULTIPLEPOLICYFACTOR != 'N'">
				<tr>
					<td class="midcolora"><li>The node for multiple policy factor must be in (Y/N) format</li> </td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
		
		<xsl:variable name="VAR_FORM_CODE" select="FORM_CODE"/> 
			<xsl:if test="normalize-space($VAR_FORM_CODE) = ''">
				<tr>
					<td class="midcolora"><li>The node for form code is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
			
		<xsl:variable name="VAR_EARTHQUAKEDP469" select="normalize-space(EARTHQUAKEDP469)"/> 
			<xsl:if test="$VAR_EARTHQUAKEDP469 != 'Y' and $VAR_EARTHQUAKEDP469 != 'N'">
				<tr>
					<td class="midcolora"><li>The node for earthquake DP469 must be in (Y/N) format</li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
		
		<xsl:variable name="VAR_INCIDENTALOFFICE" select="normalize-space(INCIDENTALOFFICE)"/> 
			<xsl:if test="$VAR_INCIDENTALOFFICE != 'Y' and $VAR_INCIDENTALOFFICE != 'N'">
				<tr>
					<td class="midcolora"><li>The node for incidental office must be in (Y/N) format</li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
		
		
		<xsl:variable name="VAR_MINESUBSIDENCEDP480" select="normalize-space(MINESUBSIDENCEDP480)"/> 
			<xsl:if test="$VAR_MINESUBSIDENCEDP480 != 'Y' and $VAR_MINESUBSIDENCEDP480 != 'N'">
				<tr>
					<td class="midcolora"><li>The node for mine subsidence - DP480 must be in must be in (Y/N) format </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
		
		<xsl:variable name="VAR_APPURTENANTSTRUCTURES_INCLUDE" select="APPURTENANTSTRUCTURES_INCLUDE"/> 
			<xsl:if test="normalize-space($VAR_APPURTENANTSTRUCTURES_INCLUDE) = ''">
				<tr>
					<td class="midcolora"><li>The node for appurtenant structures include is blank</li> </td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
		
		
		<xsl:variable name="VAR_RENTALVALUE_INCLUDE" select="RENTALVALUE_INCLUDE"/> 
			<xsl:if test="normalize-space($VAR_RENTALVALUE_INCLUDE) = ''">
				<tr>
					<td class="midcolora"><li>The node for rentalvalue include is blank</li> </td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		 
		
		<xsl:variable name="VAR_PERSONALPROPERTY_INCLUDE" select="PERSONALPROPERTY_INCLUDE"/> 
			<xsl:if test="normalize-space($VAR_PERSONALPROPERTY_INCLUDE) = ''">
				<tr>
					<td class="midcolora"><li>  The node for personalproperty include is blank </li></td>
				</tr>
		<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
</xsl:template>


<!-- ######################################################################################### -->
<!-- the below templates are use for display so dont remove it -->


<xsl:template name ="EARTHQUAKE_COV_A_DISP">
  <xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE"/>
  <xsl:choose>
		<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'F'">
		
		<xsl:variable name="VAR_EXT_CONS_COVA_F"> <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovA' and @TERRZONE = $VAR_ZONE]/@FRAME"/> </xsl:variable>
			 
		<xsl:if test="normalize-space($VAR_EXT_CONS_COVA_F) = ''">
				<tr>
					<td class="midcolora"> <li>Frame factor of Exterior construction not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/FRAME") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
		 
		</xsl:when>
    	<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'M'">
    	
    	<xsl:variable name="VAR_EXT_CONS_COVA_M"> <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovA' and @TERRZONE = $VAR_ZONE]/@MASONRY"/> </xsl:variable> 
			
			<xsl:if test="normalize-space($VAR_EXT_CONS_COVA_M) = ''">
				<tr>
					<td class="midcolora"> <li> Masonary factor of Exterior construction not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/MASONARY")</li>  </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		

		
		</xsl:when>
    <xsl:otherwise>0</xsl:otherwise>
    </xsl:choose>
    
</xsl:template>

 

<!--  ********************************************************************************************************************** -->
<!-- EARTH QUAKE for Coverage B -->

<xsl:template name ="EARTHQUAKE_COV_B_DISP">
 <xsl:variable name="VAR_ZONE"  select="EARTHQUAKEZONE"/>
 
 <xsl:choose>
		<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'F'">
		<xsl:variable name="EXT_CONS_COVB_F"> <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovB' and @TERRZONE = $VAR_ZONE]/@FRAME"/> </xsl:variable>
			
			<xsl:if test="normalize-space($EXT_CONS_COVB_F) = ''">
				<tr>
					<td class="midcolora"> <li> Frame factor for coverage B not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/FRAME") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
		
		</xsl:when>
    	<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'M'">
    	
    	<xsl:variable name="EXT_CONS_COVB_M"> <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovB' and @TERRZONE = $VAR_ZONE]/@MASONRY"/> </xsl:variable>
			
			<xsl:if test="normalize-space($EXT_CONS_COVB_M) = ''">
				<tr>
					<td class="midcolora"> <li> Masonary factor for coverage B not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/MASONARY") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
		
		
		</xsl:when>
    <xsl:otherwise>0</xsl:otherwise>
    </xsl:choose>
    
</xsl:template>
 


<!-- EARTH QUAKE for Coverage C -->

<xsl:template name ="EARTHQUAKE_COV_C_DISP">
 <xsl:variable name="VAR_ZONE"  select="EARTHQUAKEZONE"/>
 
 <xsl:choose>
		<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'F'">
				
		<xsl:variable name="EXT_CONS_COVC_F"> <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovC' and @TERRZONE = $VAR_ZONE]/@FRAME"/> </xsl:variable>

			<xsl:if test="normalize-space($EXT_CONS_COVC_F) = ''">
				<tr>
					<td class="midcolora"><li>  Frame factor for coverage C not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/FRAME") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		 
		</xsl:when>
    	<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'M'">
		
		<xsl:variable name="EXT_CONS_COVC_M">
				<xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovC' and @TERRZONE = $VAR_ZONE]/@MASONRY"/> 
		</xsl:variable>
			
		 
			
			<xsl:if test="normalize-space($EXT_CONS_COVC_M) = ''">
				<tr>
					<td class="midcolora"> <li> Masonary factor for coverage C not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/MASONARY") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
			
		</xsl:when>
    <xsl:otherwise>0</xsl:otherwise>
    </xsl:choose>
    
</xsl:template>
  
  
  <!--  ********************************************************************************************************************** -->

<!-- EARTH QUAKE for Coverage D -->

<xsl:template name ="EARTHQUAKE_COV_D_DISP">
 <xsl:variable name="VAR_ZONE"  select="EARTHQUAKEZONE"/>
 
 <xsl:choose>
		<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'F'">
		
		<xsl:variable name="EXT_CONS_COVD_F"> <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovD' and @TERRZONE = $VAR_ZONE]/@FRAME"/> </xsl:variable>
	
			<xsl:if test="normalize-space($EXT_CONS_COVD_F) = ''">
				<tr>
					<td class="midcolora"><li> Frame factor for coverage D not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/FRAME")</li>  </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			 
		</xsl:when>
    	<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'M'">
		
		<xsl:variable name="EXT_CONS_COVD_M"><xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovD' and @TERRZONE = $VAR_ZONE]/@MASONRY"/> </xsl:variable>

			<xsl:if test="normalize-space($EXT_CONS_COVD_M) = ''">
				<tr>
					<td class="midcolora"> <li> Masonary factor for coverage D not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/MASONARY")</li>  </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
		</xsl:when>
    <xsl:otherwise>0</xsl:otherwise>
    </xsl:choose>
    
</xsl:template>
 

<!--  ********************************************************************************************************************** -->
<!-- EARTH QUAKE for Coverage E -->

<xsl:template name ="EARTHQUAKE_COV_E_DISP">
 <xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE"/>
 
 <xsl:choose>
		<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'F'">
		 
		<xsl:variable name="EXT_CONS_COVE_F"> <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovE' and @TERRZONE = $VAR_ZONE]/@FRAME"/> </xsl:variable>
			 
			<xsl:if test="normalize-space($EXT_CONS_COVE_F) = ''">
				<tr>
					<td class="midcolora"> <li> Frame factor for coverage E not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/FRAME")</li>  </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			 
		</xsl:when>
    	<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'M'">
				
			<xsl:variable name="EXT_CONS_COVE_M"> <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'CovE' and @TERRZONE = $VAR_ZONE]/@MASONRY"/> </xsl:variable>
	
			<xsl:if test="normalize-space($EXT_CONS_COVE_M) = ''">
				<tr>
					<td class="midcolora"> <li> Masonary factor for coverage E not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/MASONARY") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
				
		</xsl:when>
    <xsl:otherwise>0</xsl:otherwise>
    </xsl:choose>
    
</xsl:template>


<!-- Factor for other coverages -->
<xsl:template name ="EARTHQUAKE_OTHER_COVERAGES_FACTOR_DISP">
 <xsl:variable name="VAR_ZONE" select="EARTHQUAKEZONE"/>
 
 <xsl:choose>
		<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'F'">
				
		<xsl:variable name="EXT_CONS_OTH_COV_F"> <xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'Other' and @TERRZONE = $VAR_ZONE]/@FRAME"/> </xsl:variable>
			
			<xsl:if test="normalize-space($EXT_CONS_OTH_COV_F) = ''">
				<tr>
					<td class="midcolora"> <li> Frame factor for Other coverage not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/FRAME") </li> </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
		
		</xsl:when>
    	<xsl:when test="EXTERIOR_CONSTRUCTION_F_M = 'M'">
			<xsl:variable name="EXT_CONS_OTH_COV_M"><xsl:value-of select="$RDCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='OPTIONALSECTIONCOVGES']/NODE[@ID ='EARTHQUAKE']/ATTRIBUTES[@APPLYTO = 'Other' and @TERRZONE = $VAR_ZONE]/@MASONRY"/> </xsl:variable>
			
			<xsl:if test="normalize-space($EXT_CONS_OTH_COV_M) = ''">
				<tr>
					<td class="midcolora"> <li> Masonary factor for Other coverage not found  (Path - "OPTIONALSECTIONCOVGES/EARTHQUAKE/ATTRIBUTES/MASONARY") </li>  </td>
				</tr>
				<xsl:if test="user:AllVerified(0)=1"></xsl:if>
			</xsl:if>
			
		</xsl:when>
    <xsl:otherwise></xsl:otherwise>
    </xsl:choose>
    
</xsl:template>



<!-- ##########################################################################################  -->




 	 
</xsl:stylesheet>	
	
	

 
	
	
	
	
	
	