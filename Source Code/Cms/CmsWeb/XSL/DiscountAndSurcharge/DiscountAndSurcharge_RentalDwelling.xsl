<!-- ==================================================================================================
File Name			:	DiscountAndSurcharge_RentalDwelling.xsl
Purpose				:	To display Discount and Surcharge applicable for Rental Dwelling on application basis
Name				:	Praveen Singh
Date				:	31 jan 2006  
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
		int statusFor=1;
		public void SetForLoop(int flag)
		{
			statusFor=flag;
		
		}
		
		public int GetForLoop()
		{
			return statusFor;
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
	<xsl:template match="QUICKQUOTE">
		
		<table border="0" align="center" width='90%'>
			
			 <tr>
				<td class="midcolora">
					<xsl:value-of select="user:SetFlag(1)" /> 
					<xsl:call-template name="POLICY_LEVEL"></xsl:call-template>
					<xsl:call-template name="FinalDisplay" />
				</td>
			</tr>
			<tr>
				<td class="midcolora">
					<xsl:value-of select="user:SetFlag(1)" /> 
					<xsl:call-template name="DWELLING_LEVEL"></xsl:call-template>
					<xsl:call-template name="FinalDisplay" />
				</td>
			</tr>
			
					  
		</table>
		
			 
		 
	</xsl:template>
	 
	
	 
	
	
	<xsl:template name="POLICY_LEVEL">
			<table class="tableeffectTopHeader" cellSpacing="0" cellPadding="0" width="90%" align="center">			
			<tr>
     			<td width="70%" class="midcolora"><b><u>Policy Level</u></b></td>
				<td width="30%" class="midcolora"><b>Discount / Surcharge</b></td>
			</tr>
			
	
		
				<xsl:call-template name="VALUEDCUSROMER_DISCOUNT_CREDIT"></xsl:call-template> 
			
			 	<!--<xsl:call-template name="AGEOFHOME_DISPLAY"></xsl:call-template>  -->
			 	<xsl:call-template name="MULTIPOLICY_DISPLAY"></xsl:call-template> 
				<tr><td height="5%"><p></p><br></br></td></tr>
			</table>
			
	</xsl:template>
	
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
				  <xsl:when test = "DWELL_UND_CONSTRUCTION_DP1143 != 'Y'">
		            <xsl:call-template name="AGEOFHOME_DISPLAY"></xsl:call-template>
		            <xsl:call-template name="PROTECTIVE_DEVICE_DISCOUNT_DISPLAY"></xsl:call-template>
		          </xsl:when>  
		        </xsl:choose>    
		            <xsl:call-template name="SURCHARGES_DISPLAY"></xsl:call-template>
				    <xsl:call-template name="UNDER_CONSTRUCTION_DISCOUNT_DISPLAY"></xsl:call-template>  
					
					
			
					<tr>
					<td height="5%">
						<br></br>
					</td>
				</tr>
			</xsl:for-each>
		</table>
		
	</xsl:template>
	


	
	

 
<!-- ============================================================================================ -->
<!--						Templates for Discount - Deductible Factor (START)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
<!-- ********************Dewelling Description ************************* -->
	<xsl:template name="DWELLING_DESC">
		<xsl:value-of select="@ADDRESS"></xsl:value-of>
	</xsl:template>
	<!-- ********************End Dewelling Description ************************* -->
<xsl:template name="DFACTOR">	
	<xsl:variable name="DEDUCTIBLEAMT" select="DEDUCTIBLE"/>
	<xsl:choose>
		<xsl:when test = "DEDUCTIBLE >= 500">
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLES']/NODE[@ID ='OPTIONALDEDUCTIBLE']/ATTRIBUTES[@AMOUNT=$DEDUCTIBLEAMT]/@FACTOR"/>
		</xsl:when>
		<xsl:otherwise>
			 
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- ============================================================================================ -->
<!--						Templates for Discount - Deductible Factor (END)					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->


<!-- ============================================================================================ -->
<!--		Templates for Discount - Age Of Home Factor  [Factor,Credit,Display]  (SATRT)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!--Age Of Home Display -->
<xsl:template name="AGEOFHOME_DISPLAY">
 <xsl:variable name="AGE_OF_HOME">
     <xsl:value-of select="AGEOFHOME"></xsl:value-of>
 </xsl:variable>
  
 <xsl:variable name="VARAGEHOME">
	<xsl:choose>
		<xsl:when test = "$AGE_OF_HOME &lt;= 20">
		<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='AOHCREDIT']/ATTRIBUTES[@AGE = '16']/@CREDIT"/></xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
	</xsl:variable>
		 <xsl:choose>
				<xsl:when test = "$VARAGEHOME &gt; 0">
				<tr class="midcolora">
				 <td>
					<li>Discount for Age Of Home</li> </td><td>-<xsl:value-of select="$VARAGEHOME" />% 
				 </td>
				</tr>	
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:when>
		</xsl:choose>
		
</xsl:template>


<!-- ============================================================================================ -->
<!--		Templates for Discount - Age Of Home   [Factor,Credit,Display]  (END)				  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--		Templates for Discount - Protective Devices  [Factor,Credit,Display]  (START)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

 <!-- Protective Device display -->
<xsl:template name = "PROTECTIVE_DEVICE_DISCOUNT_DISPLAY">
	<xsl:choose>
	
		<xsl:when test ="N0_LOCAL_ALARM = 1">
		 	<tr class="midcolora">
				<td><li> Discount for protective device </li></td>
				<td><xsl:variable name="VARLOCALALARM"><xsl:call-template name="PROTECTIVE_DEVICE_DISCOUNT_CREDIT" /></xsl:variable>
				    -<xsl:value-of select="round($VARLOCALALARM)"></xsl:value-of> % </td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:when>
		<xsl:when test ="N0_LOCAL_ALARM &gt; 1">
		 	<tr class="midcolora">
				<td><li> Discount for protective device </li></td>
				<td><xsl:variable name="VARLOCALALARM2"><xsl:call-template name="PROTECTIVE_DEVICE_DISCOUNT_CREDIT" /></xsl:variable>
				-<xsl:value-of select="round($VARLOCALALARM2)"></xsl:value-of>% </td>
			</tr>	
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:when>
		 
	</xsl:choose>
</xsl:template>

<!-- Protective Device credit -->
<xsl:template name = "PROTECTIVE_DEVICE_DISCOUNT_CREDIT">
	<xsl:choose>
		<xsl:when test ="N0_LOCAL_ALARM = 1">
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='PROTECTIVEDEVICES']/ATTRIBUTES[@ID = 'OLSA']/@CREDIT"/>
		</xsl:when>
		<xsl:when test ="N0_LOCAL_ALARM &gt; 1">
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='PROTECTIVEDEVICES']/ATTRIBUTES[@ID = 'LFA']/@CREDIT"/>
		</xsl:when>
		<xsl:otherwise>
		0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<!-- ============================================================================================ -->
<!--		Templates for Discount - Protective Devices  [Factor,Credit,Display]  (END)			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--	Templates for Discount -  Discount - Under Construction [Factor,Credit,Display](START)	  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!--<xsl:template name = "UNDER_CONSTRUCTION_DISCOUNT_DISPLAY">
	<xsl:choose>
		<xsl:when test ="DWELL_UND_CONSTRUCTION_DP1143 = 'Y'">
		<li> Discount for under construction </li>	
	<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:when>
		
	</xsl:choose>
	
</xsl:template>

<xsl:template name = "DUC_DISCOUNT_CREDIT">
	<xsl:choose>
		<xsl:when test ="DWELL_UND_CONSTRUCTION_DP1143 = 'Y'">
			
		</xsl:when>
		
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
</xsl:template>-->
<!--test-->
<xsl:template name="UNDER_CONSTRUCTION_DISCOUNT_DISPLAY">	
   <xsl:variable name="VAR_CONSTRUCTION"> 
	<xsl:choose>
		<xsl:when test="DWELL_UND_CONSTRUCTION_DP1143 = 'Y'">
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DUC']/NODE[@ID ='DUC_CREDIT']/ATTRIBUTES/@CREDIT"/> 		
		</xsl:when>
		 
	</xsl:choose>
	</xsl:variable>
	
	<xsl:if test="$VAR_CONSTRUCTION &gt; 0">
	 <tr class="midcolora">
		<td><li> Discount for under construction  </li></td>
		<td>-<xsl:value-of select="$VAR_CONSTRUCTION" />% </td>
	</tr>
	  
	
	<xsl:value-of select="user:SetFlag(0)" /> 
	</xsl:if>
	
	
	
</xsl:template>
<!--test-->
<!-- ============================================================================================ -->
<!--	Templates for Discount -  Discount - Under Construction [Factor,Credit,Display](END)	  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!-- ============================================================================================ -->
<!--		Templates for Discount - Multi policy Factor [Factor,Credit,Display]  (START)		  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

<!--Multipolicy Display -->
 
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
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='MULTIPOLICY']/ATTRIBUTES/@CREDIT"/> 		
		</xsl:when>
		 
	</xsl:choose>
	</xsl:variable>
	
	<xsl:if test="$VAR_MULTIPOLICY &gt; 0">
	 <tr class="midcolora">
		<td><li> Discount for multipolicy </li></td>
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
<!--						Valued Customer Renewal Discount                					  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!--      The amount of discount depends on the
          number of consecutive years, ending with the current renewal date, that the policy 
          has been with Wolverine Mutual Insurance Company.                                      -->
<!-- ============================================================================================ -->
 
 

<xsl:template name="VALUEDCUSROMER_DISCOUNT_CREDIT">
<xsl:value-of select="user:SetForLoop(0)" />
		<xsl:variable name="VARSTATENAME">
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
	<xsl:choose>
		<xsl:when test ="$VARSTATENAME ='MICHIGAN' and VARNEWBUSINESSFACTOR='N'">
			<xsl:call-template name="VALUEDCUSROMER_DISCOUNT_CREDIT_MICHIGAN"/> 
		</xsl:when>
		<xsl:when test ="$VARSTATENAME ='INDIANA' and VARNEWBUSINESSFACTOR='N'">
			<xsl:call-template name="VALUEDCUSROMER_DISCOUNT_CREDIT_INDIANA"/> 
		</xsl:when>
	</xsl:choose>
</xsl:template>


<xsl:template name="VALUEDCUSROMER_DISCOUNT_CREDIT_MICHIGAN">
	<xsl:value-of select="user:SetForLoop(0)" />
		<xsl:variable name="VARNO_YEARS_WITH_WOLVERINE">
			<xsl:for-each select="DWELLINGDETAILS">
				<xsl:variable name="VARFOR" select="user:GetForLoop()"></xsl:variable>
				<xsl:choose>
					<xsl:when test="$VARFOR ='0'">
						<xsl:value-of select="INSUREWITHWOL"></xsl:value-of>
						<xsl:value-of select="user:SetForLoop(1)" />
					</xsl:when>
				</xsl:choose>
			</xsl:for-each>
		</xsl:variable>
	
	<xsl:variable name="VAR_CREDIT"> 
	<xsl:choose>
		<xsl:when test="$VARNO_YEARS_WITH_WOLVERINE &lt;= 2">
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V1']/@CREDIT"/>
		</xsl:when>
		<xsl:when test="$VARNO_YEARS_WITH_WOLVERINE &gt; 2 and $VARNO_YEARS_WITH_WOLVERINE &lt;= 5">
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V2']/@CREDIT"/>
		</xsl:when>
		<xsl:when test="$VARNO_YEARS_WITH_WOLVERINE &gt; 5">
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V3']/@CREDIT"/>
		</xsl:when>
		 <!-- <xsl:otherwise>0.00 </xsl:otherwise> -->
	</xsl:choose>
	</xsl:variable>
	
	
	<xsl:if test="$VAR_CREDIT &gt; 0">
		<tr class="midcolora">
		 <td><li> Discount for valued customer </li></td>
		 <td><xsl:value-of select="$VAR_CREDIT" />%</td>
		</tr> 
	<xsl:value-of select="user:SetFlag(0)" /> 
	</xsl:if>
	
</xsl:template>




<xsl:template name="VALUEDCUSROMER_DISCOUNT_CREDIT_INDIANA">
   <xsl:value-of select="user:SetForLoop(0)" />
		<xsl:variable name="VARNO_YEARS_WITH_WOLVERINE">
			<xsl:for-each select="DWELLINGDETAILS">
				<xsl:variable name="VARFOR" select="user:GetForLoop()"></xsl:variable>
				<xsl:choose>
					<xsl:when test="$VARFOR ='0'">
						<xsl:value-of select="INSUREWITHWOL"></xsl:value-of>
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
<xsl:variable name="VAR_CREDIT"> 
	<xsl:choose>
		<xsl:when test="$VARNO_YEARS_WITH_WOLVERINE &lt; 2">
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V1']/@NO_LOSS_CREDIT"/>
		</xsl:when>
		<xsl:when test="$VARNO_YEARS_WITH_WOLVERINE &gt; 2 and $VARNO_YEARS_WITH_WOLVERINE &lt;= 5 and $VARLOSSFREE = 'N'">
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V2']/@LOSS_CREDIT"/>
		</xsl:when>
		<xsl:when test="$VARNO_YEARS_WITH_WOLVERINE &gt; 2 and $VARNO_YEARS_WITH_WOLVERINE &lt;= 5 and $VARLOSSFREE = 'Y'">
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V2']/@NO_LOSS_CREDIT"/>
		</xsl:when>
		<xsl:when test="$VARNO_YEARS_WITH_WOLVERINE &gt; 5 and $VARLOSSFREE = 'N'">
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V3']/@LOSS_CREDIT"/>
		</xsl:when>
		<xsl:when test="$VARNO_YEARS_WITH_WOLVERINE &gt; 5 and $VARLOSSFREE = 'Y'">
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CREDITS']/NODE[@ID ='VALUEDCUSTOMER_RENEWAL_DISCOUNT']/ATTRIBUTES[@ID = 'V3']/@NO_LOSS_CREDIT"/>
		</xsl:when>
		 
	</xsl:choose>
	</xsl:variable>
	
	<xsl:if test="$VAR_CREDIT &gt; 0">
	<tr class="midcolora">
	<td><li> Discount for valued customer</li></td> <td>-<xsl:value-of select="$VAR_CREDIT" />%</td>
	</tr>
	<xsl:value-of select="user:SetFlag(0)" /> 
	</xsl:if>
	
	
</xsl:template>


<!-- ============================================================================================ -->
<!--			Templates for Discount - VALUED CUSTOMER [Factor,Credit,Display]  (END)			  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

 


<!-- ============================================================================================ -->
<!--		Templates for Surcharge - Surcharges Factor [Factor]  (START)						  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->
 

<xsl:template name ="SURCHARGES_DISPLAY">

<xsl:variable name="VAR_SURCHARGE">

	<xsl:choose>
		<xsl:when test ="NUMBEROFFAMILIES = 1 and SEASONALSECONDARY = 'N'">
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS = '1']/@PCREDIT"/>
		</xsl:when>		
		<xsl:when test ="NUMBEROFFAMILIES = 2 and SEASONALSECONDARY = 'N'">
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS = '2']/@PCREDIT"/>
		</xsl:when>		
		<xsl:when test ="NUMBEROFFAMILIES = 1 and SEASONALSECONDARY = 'Y'">
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS = '1']/@SCREDIT"/>
		</xsl:when>		
		<xsl:when test ="NUMBEROFFAMILIES = 2 and SEASONALSECONDARY = 'Y'">
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGES']/NODE[@ID ='FAMILYUNITSINDWELL']/ATTRIBUTES[@UNITS = '2']/@SCREDIT"/>
		</xsl:when>		
		 
	</xsl:choose>
	</xsl:variable>
	
	<xsl:if test="$VAR_SURCHARGE &gt; 0">
	   	<tr class="midcolora">
			<td><li> Surcharges for No. of family in dwelling </li></td>
			<td>+<xsl:value-of select="$VAR_SURCHARGE" />% </td>
		</tr>	
	<xsl:value-of select="user:SetFlag(0)" /> 
	</xsl:if>
	
</xsl:template>
<!-- ============================================================================================ -->
<!--		Templates for Surcharge - Surcharges Factor [Factor]  (END)							  -->
<!--		    					  FOR MISHIGAN and INDIANA									  -->
<!-- ============================================================================================ -->

 
<!-- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  -->
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
<!-- ============================================================================= -->




</xsl:stylesheet>


