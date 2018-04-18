<!-- ==================================================================================================
File Name			:	DiscountAndSurcharge_Auto.xsl
Purpose				:	To display Discount and Surcharge applicable for Auto on application basis
Name				:	Praveen Singh
Date				:	27 jan 2006  
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
		
		int INS_SCORE=1;
	
		public void SET_INSURANCE_SCORE(int VAR_INS_SCORE)
		{
			INS_SCORE=VAR_INS_SCORE;
		
		}
		
		public int GET_INSURANCE_SCORE()
		{
			return INS_SCORE;
		}
		

       string TRAILER="";
	
		public void SET_TRAILER_BLAZE(string VAR_TRAILER)
		{
			TRAILER=VAR_TRAILER;
		
		}
		
		public string GET_TRAILER_BLAZE()
		{
			return TRAILER;
		}

        int InitWOL=0;
		public void SetWOL(int WOL_VAL)
		{
			InitWOL=WOL_VAL;
		
		}
		
		public int GetWOL()
		{
			return InitWOL;
		}
		
		string StrState="";
		
		public void SetState(string STATE_VAL)
		{
			StrState=STATE_VAL;
		
		}
		
		public string GetState()
		{
			return StrState;
		}
		 
	
		
		
		
]]></msxsl:script>
	<!-- ============================================================================= -->
	<xsl:variable name="myDoc"    select="document('FactorPathPer')"></xsl:variable>
	<xsl:variable name="myDocCom" select="document('FactorPathCom')"></xsl:variable>
	
	
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
					<xsl:call-template name="Driver_Level"></xsl:call-template>
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
				<xsl:call-template name="INSURANCESCORE_DISPLAY"></xsl:call-template>
		
				<xsl:call-template name="MULTIPOLICY_DISPLAY"></xsl:call-template>
		
				<xsl:call-template name="TRAILER_BLAZER_DISPLAY"></xsl:call-template>
				
				<xsl:call-template name="INSURED_WITH_WOL"></xsl:call-template>
				
				
				<!--<xsl:value-of select="user:SetWOL($CALCULATED_WOL)" />-->
		
		
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
					<b> <xsl:value-of select="@ID" />. <xsl:call-template name="AUTO_DESC"/>   </b>
				</td>
				<td width="30%" class="midcolora">
					<b> Discount / Surcharge </b>
				</td>
			</tr>
				<!--Multi Car Discount Is Applicable To Only Personal vehicles - Motorhome, Customized van or Truck. -->
				    <xsl:variable name="lcletters">abcdefghijklmnopqrstuvwxyz</xsl:variable>
				    <xsl:variable name="ucletters">ABCDEFGHIJKLMNOPQRSTUVWXYZ</xsl:variable>
					<xsl:choose>
					 <xsl:when  test="VEHICLETYPE='CV' or VEHICLETYPE='PP' or VEHICLETYPE='MH'">
					    <xsl:call-template name="MULTIVEHICLE_DISPLAY"></xsl:call-template>
					    <xsl:call-template name="ABS_DISPLAY"></xsl:call-template>
						<xsl:call-template name="AIRBAG_DISPLAY"></xsl:call-template>
						<xsl:call-template name="VEHICLEUSE"></xsl:call-template>
				     </xsl:when>
					 <xsl:otherwise></xsl:otherwise>
					</xsl:choose> 
				   
				   
				    <xsl:choose> 
				     <xsl:when  test="VEHICLETYPEUSE = 'PERSONAL'">
						
						<xsl:call-template name="SEATBELT_DISPLAY"></xsl:call-template>
						<xsl:call-template name="PIP_DISPLAY"></xsl:call-template>
						<xsl:call-template name="VEHCLESURCHARGE"></xsl:call-template> 
					</xsl:when>
					 <xsl:otherwise></xsl:otherwise>
					</xsl:choose>
					
			</xsl:for-each>
		    <tr><td height="5%"><br></br></td></tr>
		</table>
	</xsl:template>
	
<!-- *********************************************************************************************** -->
	
	<xsl:template name="Driver_Level">
		
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
						<b><xsl:call-template name="DRIVER_DESC" />&#xa0; <xsl:value-of select="DRIVERCODE" />   </b>
					</td>
						<td width="30%" class="midcolora">
						<b> Discount / Surcharge </b>
					</td>
				</tr>
					
			<xsl:choose>
				<xsl:when test="AGEOFDRIVER &lt; 26">
				 	<xsl:call-template name="GOODSTUDENT"></xsl:call-template>  
				</xsl:when>
				
				<xsl:when test="AGEOFDRIVER &gt; 44">	 
						<!--<xsl:call-template name="DRIVERDISCOUNT"></xsl:call-template>  
						 this line is to be uncomented  -->
						
                <!-- <xsl:call-template name="SEATBELT_DISPLAY"></xsl:call-template>-->
				</xsl:when>
				
			</xsl:choose>
			
			<xsl:choose>
				<xsl:when test="SUMOFACCIDENTPOINTS  &lt;  1 and SUMOFVIOLATIONPOINTS &lt; 1">
					<!--<xsl:call-template name="PREFERRED_RISK"></xsl:call-template>-->
					<xsl:call-template name="DRIVERDISCOUNT"></xsl:call-template>  
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
			
				
					
				
		</xsl:for-each>
	   </table>		
	</xsl:template>
	
	
	
	
	<!-- *************************INSURENCE WITH WOLURINE ********************* -->
	
	<xsl:template name ="INSURED_WITH_WOL">

	  <xsl:variable name="WOL" select="POLICY/YEARSCONTINSUREDWITHWOLVERINE"></xsl:variable>
	  <xsl:variable name="STATE" select="POLICY/STATENAME"></xsl:variable>
	  <xsl:variable name="VARQUALIFIESTRAIBLAZERPROGRAM" select="POLICY/QUALIFIESTRAIBLAZERPROGRAM"></xsl:variable>
	  
	  <xsl:value-of select="user:SetWOL($WOL)" />
	  <xsl:value-of select="user:SetState($STATE)" />
	  <xsl:value-of select="user:SET_TRAILER_BLAZE($VARQUALIFIESTRAIBLAZERPROGRAM)" />
	  

	  
</xsl:template>
	
	
	<!-- ************************* Insurance Score ******************************* -->
	
	 
	

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
		<xsl:when test = "$INS ='N' ">
		 <xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = 'N']/@CREDIT"/>
		</xsl:when>
		<xsl:when test = "$INS &gt;= 751">
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = 751]/@CREDIT"/>
		</xsl:when>		
		<xsl:otherwise>						
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@CREDIT"/>
		</xsl:otherwise>
	</xsl:choose>
	</xsl:variable>
	
	
	<xsl:if test="$VAR_INS_SCORE &gt; 0">
		<tr>
			<td class="midcolora"><li> Insurance Score Discount </li></td>
			<td class="midcolora">-<xsl:value-of select="$VAR_INS_SCORE" />% 
				<!-- <xsl:value-of select="user:SET_INSURANCE_SCORE($VAR_INS_SCORE)" /> Set INS SCORE AT POLICY LEVEL-->
			</td>
		</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
	</xsl:if>
	
</xsl:template>

	
<xsl:template name ="INSURANCESCORE">

	<xsl:variable name="INS" select="POLICY/INSURANCESCORE"> </xsl:variable>
	 
	<xsl:choose>
		<xsl:when test = "$INS = 0">
			<xsl:value-of select= "$INS"></xsl:value-of>
		</xsl:when>
		<xsl:when test = "$INS = ''">
			1.00
		</xsl:when>
		<xsl:when test = "$INS = 'No Hit No Score'">
			0.87
		</xsl:when>
		<xsl:when test = "$INS &gt;= 751">
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = 751]/@CREDIT"/>
		</xsl:when>		
		<xsl:otherwise>						
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@CREDIT"/>
		</xsl:otherwise>
	</xsl:choose>
	
	
	
	
	
</xsl:template>



<!-- Anti Breaking System -->

<xsl:template name ="ABS_DISPLAY">
	<xsl:variable name="VAR_ABS"> 
		<xsl:choose>
			<xsl:when test="ISANTILOCKBRAKESDISCOUNTS = 'TRUE'">
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ANTILOCKBREAKSYSTEM']/NODE[@ID='ABS']/ATTRIBUTES/@CREDIT"/>
			</xsl:when>
		</xsl:choose>
	</xsl:variable>
	
	<xsl:if test="$VAR_ABS &gt; 0">
	<tr class="midcolora">
			<td><li>Discount for Anti Lock Brake System	</li></td>
			<td> -<xsl:value-of select="$VAR_ABS" />%</td>
		</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
	</xsl:if>
</xsl:template>

<!-- ************************* Seat Belt ******************************* -->
 

<xsl:template name ="SEATBELT_DISPLAY">
	<!--Shafi<xsl:variable name="VAR_ABS"> -->
	
	
	<xsl:variable name="VAR_SEATBELT">
		<xsl:choose>
			<!-- <xsl:when test="VEHICLES/VEHICLE/WEARINGSEATBELT = 'TRUE'"> -->
			<xsl:when test="WEARINGSEATBELT = 'TRUE'">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SEATBELTCREDIT']/NODE[@ID='SEATBELT']/ATTRIBUTES/@CREDIT"/>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise> 
		</xsl:choose>
	</xsl:variable>
	
	<xsl:if test="$VAR_SEATBELT &gt; 0">
		<tr class="midcolora">
			<td >
				<li> Discount for wearing seat belt  </li>
			</td>
			 <td>-<xsl:value-of select="$VAR_SEATBELT" />%</td>
		</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
	</xsl:if>
		
	
</xsl:template>


<!-- ************************* Air Bag ******************************* -->

<xsl:template name ="AIRBAG_DISPLAY">
	<xsl:variable name="PAIRBAGDISCOUNT" select="AIRBAGDISCOUNT" />
	<xsl:variable name="VAR_AIRBAG"> 
		<xsl:choose>
			<xsl:when test="$PAIRBAGDISCOUNT != ''">
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='AIRBAGCREDIT']/NODE[@ID='AIRBAG']/ATTRIBUTES[@ID=$PAIRBAGDISCOUNT]/@CREDIT"/>			
			</xsl:when>
		</xsl:choose>
	</xsl:variable> 
	
	
	<xsl:if test="$VAR_AIRBAG &gt; 0">
		<tr  class="midcolora">
			<td><li> Discount for Airbag </li></td>
			 <td>-<xsl:value-of select="$VAR_AIRBAG" />% </td>
		</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
	</xsl:if>
	
	
</xsl:template>



<!-- ************************* Loan/Lease Gap ******************************* -->


 
	<xsl:template name ="LOANLEASEGAP">
	<!-- 1. ONLY ON NEW (CURRENT) VEHICLES ... 
		 2. LEASED: 10% TO EACH COMPREHENSIVE AND COLLISION AND COMPARE TO MIN
		 3. LOAN: 5% TO EACH COMPREHENSIVE AND COLLISION AND COMPARE TO MIN -->
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/LOANLEASEGAP =0 or VEHICLES/VEHICLE/LOANLEASEGAP =''">
			0.00
		</xsl:when>
		<xsl:otherwise>
			<xsl:variable name="GAPFACTOR">
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/LOANLEASEGAP = 'LOAN'">
						<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='A11LOANLEASEGAP']/NODE[@ID='LOANLEASEGAP']/ATTRIBUTES/@LOANFACTOR"/>	
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/LOANLEASEGAP = 'LEASE'">
						<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='A11LOANLEASEGAP']/NODE[@ID='LOANLEASEGAP']/ATTRIBUTES/@LEASEFACTOR"/>	
					</xsl:when>
					<xsl:otherwise>
						0.00
					</xsl:otherwise>
				</xsl:choose>
			</xsl:variable>
			
			<xsl:variable name="MINVALUECOMP">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='A11LOANLEASEGAP']/NODE[@ID='LOANLEASEGAP']/ATTRIBUTES/@MINPREMIUMPERCARCOMP"/>	
			</xsl:variable>
			<xsl:variable name="MINVALUECOLL">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='A11LOANLEASEGAP']/NODE[@ID='LOANLEASEGAP']/ATTRIBUTES/@MINPREMIUMPERCARCOLL"/>	
			</xsl:variable> 
			
			<!--Final COMP Value -->
			<xsl:variable name="COVERAGECOMPVALUE">				
					(
						<xsl:call-template name="COMP" />
					)
					*
					(
						<xsl:value-of select="$GAPFACTOR"/>		
					)				
			</xsl:variable>
			<xsl:variable name="FINALCOMPVALUE">
					IF (<xsl:value-of select ="$COVERAGECOMPVALUE" /> &lt;  <xsl:value-of select ="$MINVALUECOMP" />)
					THEN
							<xsl:value-of select="$MINVALUECOMP"/>
					ELSE
							<xsl:value-of select="$COVERAGECOMPVALUE"/>
			</xsl:variable>
			
			<!-- Final COLL Value -->
			<xsl:variable name="COVERAGECOLLVALUE">				
					(
					<xsl:call-template name="COLL" />
					)
					*
					(
					<xsl:value-of select="$GAPFACTOR"/>		
					)				
			</xsl:variable>
			<xsl:variable name="FINALCOLLVALUE">					
					IF (<xsl:value-of select="$COVERAGECOLLVALUE"/> &lt; <xsl:value-of select="$MINVALUECOLL"/>)
					THEN
							<xsl:value-of select="$MINVALUECOLL"/>
					ELSE
							<xsl:value-of select="$COVERAGECOLLVALUE"/>
			</xsl:variable>
				
			<!-- return the sum  -->	
			(<xsl:value-of select="$FINALCOLLVALUE" />) + (<xsl:value-of select="$FINALCOMPVALUE"/>)
			 
		</xsl:otherwise>
	</xsl:choose>	
</xsl:template>
			 
<!-- ****************  Work Loss Waiver *************** -->

<xsl:template name="WAIVEWORKLOSS_DISPLAY">
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/WAIVEWORKLOSS = 'TRUE'">
			Included	
		</xsl:when>
		<xsl:otherwise>
			0
		</xsl:otherwise>	
	</xsl:choose>		
</xsl:template>

<xsl:template name="WAIVEWORKLOSS">
	
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/WAIVEWORKLOSS = 'TRUE'">
			
			<!--xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='SOUNDRECVTRANSEQUIPMENT']/ATTRIBUTES/@PREMIUM"/>
			*
			(<xsl:value-of select="SOUNDRECEIVINGTRANSMITTINGSYSTEM"/>		
			  DIV
			 <xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='SOUNDRECVTRANSEQUIPMENT']/ATTRIBUTES/@PER_EACH_ADDITION_OF"/>
			)	-->
			0.00	
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>	
	</xsl:choose>		
</xsl:template>



<!-- **************** Driver Class **************** -->
<xsl:template name ="CLASS">
	<xsl:param name="FACTORELEMENT" /> <!-- Depending on the factor element BI/PD..etc -->
	<xsl:variable name="PDRIVERCLASS" >
		<xsl:value-of select="DRIVERS/DRIVER/DRIVERCLASS" />   <!-- need to determine driver class first-->
	</xsl:variable>
	<xsl:choose>
		<xsl:when  test="DRIVERS/DRIVER/DRIVERCLASS =''">
			1.00
		</xsl:when>
		<xsl:otherwise>
			IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'BI')THEN 
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSRELATIVITY']/NODE[@ID='DCR']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'PD')THEN 
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSRELATIVITY']/NODE[@ID='DCR']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'PPI')THEN 
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSRELATIVITY']/NODE[@ID='DCR']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'PIP')THEN 
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSRELATIVITY']/NODE[@ID='DCR']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'UM') THEN 
				1.00
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'UIM') THEN 
				1.00
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'COMP') THEN 
						<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSCOMPREHENSIVE']/NODE[@ID='DRIVERCLASSCOMP']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'COLL') THEN 
						<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSCOLLISION']/NODE[@ID='DRIVERCLASSCOLL']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'LTDCOLL') THEN 
						<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSCOLLISION']/NODE[@ID='DRIVERCLASSCOLL']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY"/>
			ELSE
				1.00
		</xsl:otherwise>	
	</xsl:choose>	
	
</xsl:template>

<!-- **************** Limit **************** -->
<xsl:template name ="LIMIT">
	<xsl:param name="FACTORELEMENT" />
	<xsl:variable name="PVEHICLEROWID" select="VEHICLES/VEHICLE/VEHICLEROWID"></xsl:variable>	<!-- Ask Deepak -->
	<xsl:choose>	
		<xsl:when test = "$FACTORELEMENT = 'BI'"> <!-- BI -->
			<xsl:variable name="COVERAGELL" select="VEHICLES/VEHICLE/BILIMIT1"/> <!-- Lower Limit CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
			<xsl:variable name="COVERAGEUL" select="VEHICLES/VEHICLE/BILIMIT2"/> <!-- Upper Limit -->
			<xsl:choose>
				<xsl:when test="$COVERAGELL ='' or $COVERAGELL='0'">
					1.00
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIMITS']/NODE[@ID= $FACTORELEMENT ]/ATTRIBUTES[@MINCOVERAGE= $COVERAGELL and @MAXCOVERAGE = $COVERAGEUL ]/@RELATIVITY"/>	
				</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:when test = "$FACTORELEMENT='UM'"> <!-- UM -->
			<xsl:variable name="COVERAGELL" select="VEHICLES/VEHICLE/UMSPLITLIMIT1"/> <!-- Lower Limit CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
			<xsl:variable name="COVERAGEUL" select="VEHICLES/VEHICLE/UMSPLITLIMIT2"/> <!-- Upper Limit -->
			<xsl:choose>
				<!-- Check if split limit or combined single limit -->
				<xsl:when test="$COVERAGELL ='' or $COVERAGELL='0'"><!-- If combined single limit -->
					<xsl:variable name="PUMCSL" select="VEHICLES/VEHICLE/UMCSL" />
					<xsl:choose>
						<xsl:when test ="$PVEHICLEROWID &gt; 0 and $PVEHICLEROWID =1">
							<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID ='CSLUMBI']/ATTRIBUTES[@CSLUMBILIMITS = $PUMCSL]/@CSLUMRATES1"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID ='CSLUMBI']/ATTRIBUTES[@CSLUMBILIMITS = $PUMCSL]/@CSLUMRATES2"/>
						</xsl:otherwise>
					</xsl:choose>						
				</xsl:when>
				<xsl:otherwise> <!-- If  split limit -->							
						<xsl:variable name="PMINLIMIT" select="VEHICLES/VEHICLE/UMSPLITLIMIT1"></xsl:variable>
						<xsl:variable name="PMAXLIMIT" select="VEHICLES/VEHICLE/UMSPLITLIMIT2"></xsl:variable>
						<xsl:choose>
							<xsl:when test ="$PVEHICLEROWID &gt; 0 and $PVEHICLEROWID =1">
								<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID ='UMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMIT and @MAXLIMIT=$PMAXLIMIT]/@UMRATE1"/>
							</xsl:when>
							<xsl:when test="$PVEHICLEROWID &gt; 1">
								<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID ='UMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMIT and @MAXLIMIT=$PMAXLIMIT]/@UMRATE2"/>
							</xsl:when>
							<xsl:otherwise>
								1.00
							</xsl:otherwise>
						</xsl:choose>	
				</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:when test = "$FACTORELEMENT = 'UIM'"> <!-- UIM -->
			<xsl:variable name="COVERAGELL" select="VEHICLES/VEHICLE/UMSPLITLIMIT1"/> <!-- Lower Limit CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
			<xsl:variable name="COVERAGEUL" select="VEHICLES/VEHICLE/UMSPLITLIMIT2"/> <!-- Upper Limit -->
			<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/ISUNDERINSUREDMOTORISTS ='TRUE'">
						<xsl:choose><!-- Check if split limit or combined single limit -->
							<xsl:when test="$COVERAGELL ='' or $COVERAGELL='0'"><!-- If combined single limit -->						
								<xsl:choose>
									<xsl:when test="UMCSL &gt; 0"> 
											<xsl:variable name="PUMCSL" select="VEHICLES/VEHICLE/UMCSL" />
											<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUIMBILIMITS']/NODE[@ID ='CSLUIMBI']/ATTRIBUTES[@CSLUIMBILIMITS = $PUMCSL]/@CSLUIMRATES"/>
									</xsl:when>
									<xsl:otherwise> 
										0.00
									</xsl:otherwise>		
								</xsl:choose>
							</xsl:when>
							<xsl:otherwise><!-- If  split limit -->
								<!--xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIMITS']/NODE[@ID= $FACTORELEMENT ]/ATTRIBUTES[@MINCOVERAGE= $COVERAGELL and @MAXCOVERAGE = $COVERAGEUL ]/@RELATIVITY"/-->
									<xsl:variable name="PMINLIMIT" select="VEHICLES/VEHICLE/UMSPLITLIMIT1"></xsl:variable>
									<xsl:variable name="PMAXLIMIT" select="VEHICLES/VEHICLE/UMSPLITLIMIT2"></xsl:variable>
									<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UIMBILIMITS']/NODE[@ID ='UIMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMIT and @MAXLIMIT=$PMAXLIMIT]/@UIMRATES"/>
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>
						0.00
					</xsl:otherwise>
			</xsl:choose>	
		</xsl:when>
		<xsl:otherwise> <!-- PD -->
			<xsl:variable name="COVERAGE" select="VEHICLES/VEHICLE/PD"/> <!-- CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
			<xsl:choose>
				<xsl:when test="$COVERAGE ='' or $COVERAGE='0'">
					1.00
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIMITS']/NODE[@ID = $FACTORELEMENT ]/ATTRIBUTES[@COVERAGE = $COVERAGE ]/@RELATIVITY"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:otherwise>
	</xsl:choose>	

</xsl:template>



<!-- **************** Multi-vehicle **************** -->
<xsl:template name ="MULTIVEHICLE_DISPLAY">	

<!--1.Applicable if More Than One Vehicle For The Application and Vechicle Class % Dependents upon Class 
    2.In case Of MICHIGAN If Class is 1,2,5 Then Percentage May Vary-->

  <xsl:variable name ="VARCLASS"><xsl:call-template name ="VEHICLE_CLASS"/></xsl:variable>
  <xsl:variable name="VARSTATE"><xsl:value-of select="user:GetState()"/></xsl:variable>   

  <xsl:variable name="VARMULTICARDISCOUNT" select="MULTICARDISCOUNT"/> 
	 <xsl:choose>
		<xsl:when test="$VARMULTICARDISCOUNT ='TRUE' and  $VARCLASS!= '0' and $VARSTATE!= 'MICHIGAN'"> 
			<tr class="midcolora">
				<td ><li> Multi Vehicle Discount </li></td>
				<td>-<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEBI']/ATTRIBUTES[@CLASS = $VARCLASS]/@DISCOUNT"/>%</td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:when>
		 <xsl:when test="$VARMULTICARDISCOUNT ='TRUE' and  ($VARCLASS= '3' or $VARCLASS= '2' or $VARCLASS= '4' or $VARCLASS= '6')  and $VARSTATE= 'MICHIGAN'"> 
			<tr class="midcolora">
				<td ><li> Multi Vehicle Discount</li></td>
				<td>-<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEBI']/ATTRIBUTES[@CLASS = $VARCLASS]/@DISCOUNT"/>%</td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:when>
		<xsl:when test="$VARMULTICARDISCOUNT ='TRUE' and  ($VARCLASS= '1'  or $VARCLASS= '5' or $VARCLASS= 'P')  and $VARSTATE= 'MICHIGAN'"> 
			<tr class="midcolora">
				<td ><li> Multi Vehicle Discount</li></td>
				<td> - % may vary</td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:when>
	</xsl:choose>
</xsl:template>


 
 
 <!-- Get The Class Of The Vehicle -->
  <xsl:template name ="VEHICLE_CLASS">
    <xsl:variable name="VCLASS" select="VEHICLECLASS"></xsl:variable>
    
     <xsl:choose>
		<xsl:when test ="$VCLASS = 'PA' or $VCLASS ='PB' or $VCLASS ='PC' or $VCLASS ='PD' or $VCLASS ='PE' or $VCLASS ='PF'">P</xsl:when>
		<xsl:when test ="$VCLASS = '1A' or $VCLASS ='1B' or $VCLASS ='1C' or $VCLASS ='1D' or $VCLASS ='1E' or $VCLASS ='1F'">1</xsl:when>
		<xsl:when test ="$VCLASS ='2A' or $VCLASS ='2B' or $VCLASS ='2C'">2</xsl:when>
		<xsl:when test ="$VCLASS ='3A' or $VCLASS ='3B' or $VCLASS ='3C'">3</xsl:when>
		<xsl:when test ="$VCLASS ='4A' or $VCLASS ='4B' or $VCLASS ='4C'">4</xsl:when>
		<xsl:when test ="$VCLASS ='5C'">5</xsl:when>
		<xsl:when test ="$VCLASS ='6A' or $VCLASS ='6B' or $VCLASS ='6C'">6</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
    </xsl:choose>
 </xsl:template>
 

<xsl:template name ="MULTIVEHICLE">
	<xsl:param name="FACTORELEMENT" /> <!-- Depending on the factor element BI/PD..etc -->
	<xsl:variable name="PDRIVERCLASS" select="DRIVERS/DRIVER[@ID='1']/DRIVERCLASSCOMPONENT1"/>  <!-- ASK RAJAN --> 
	
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/MULTICARDISCOUNT != '' and VEHICLES/VEHICLE/MULTICARDISCOUNT !='FALSE'">
			IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'BI')THEN 
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEBI']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'PD')THEN 
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEPD']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'PPI')THEN 
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEPPI']/ATTRIBUTES[@CLASS  = $PDRIVERCLASS]/@FACTOR"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'PIP')THEN 
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEPIP']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'COLL') THEN 
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLECOLL']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'LTDCOLL') THEN 
				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLECOLL']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR"/>
			ELSE
				1.00
		</xsl:when>
		<xsl:otherwise>
			1.00
		</xsl:otherwise>
	</xsl:choose>	
</xsl:template>



<!-- ****************  Multicar discount CSL **************** -->
<xsl:template name ="MULTICARDISCOUNTCSL">	 
	 
	<xsl:variable name="PDRIVERCLASS">
		<xsl:value-of select="DRIVERS/DRIVER[@ID='1']/DRIVERCLASS" /><xsl:value-of select="VEHICLEUSE" />		
	</xsl:variable>
	<xsl:choose>
		<xsl:when test="$PDRIVERCLASS ='P1' or $PDRIVERCLASS ='P2'">
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTICARCSL']/ATTRIBUTES[@DRIVERCLASS = $PDRIVERCLASS]/@FACTOR"/>	
		</xsl:when> 
		<xsl:otherwise>
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTICARCSL']/ATTRIBUTES[@DRIVERCLASS = 'OTHER']/@FACTOR"/>
		</xsl:otherwise>	
	</xsl:choose>
</xsl:template>

<!--test 
template for propercaser
	
		<xsl:template name='convertcase'>
	
					<xsl:param name='toconvert' />
						<xsl:call-template name='convertpropercase'>
							<xsl:with-param name='toconvert'>
								<xsl:value-of select="translate($toconvert,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')"/>
							</xsl:with-param>
						</xsl:call-template>
				</xsl:template>
						
						
			 <xsl:template name='convertpropercase'>
			    
				<xsl:param name='toconvert' />
						

				<xsl:if test="string-length($toconvert) > 0">
					<xsl:variable name='f' select='substring($toconvert, 1, 1)' />
					<xsl:variable name='s' select='substring($toconvert, 2)' />
					
					<xsl:call-template name='convertcase'>
						<xsl:with-param name='toconvert' select='$f' />
					</xsl:call-template>

				<xsl:choose>
					<xsl:when test="contains($s,' ')">
						<xsl:value-of select='substring-before($s," ")'/>
						&#160;
						<xsl:call-template name='convertpropercase'>
						<xsl:with-param name='toconvert' select='substring-after($s," ")' />
						</xsl:call-template>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select='$s'/>
					</xsl:otherwise>
				</xsl:choose>
				</xsl:if>
			</xsl:template>		
	template for propercaser
	


 test -->
 
 <!--**************      Title Case Conversion in XSLT ******************-->
 
 <xsl:template name="TitleCase">
      <xsl:param name="text" />
      <xsl:param name="lastletter" select="' '"/>
      <xsl:if test="$text"> 
         <xsl:variable name="thisletter" select="substring($text,1,1)"/> 
         <xsl:choose>
            <xsl:when test="$lastletter=' '">
               <xsl:value-of select="translate($thisletter,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/>
            </xsl:when>
            <xsl:otherwise>
               <xsl:value-of select="$thisletter"/>
            </xsl:otherwise>
         </xsl:choose> 
         <xsl:call-template name="TitleCase">
            <xsl:with-param name="text" select="substring($text,2)"/>
            <xsl:with-param name="lastletter" select="$thisletter"/>
         </xsl:call-template> 
      </xsl:if> 
   </xsl:template> 
   
   <!--************************END OF THE TEMPLATE*************************************--> 




<!-- ****************  Vehicle Use **************** -->
<xsl:template name ="VEHICLEUSE">	
	
	
	<xsl:variable name ="VARCLASS"><xsl:call-template name ="VEHICLE_CLASS"/></xsl:variable>
	<xsl:variable name="PUSECODE" select="VEHICLEUSE" />
	<!-- Get the Vehicle use factor -->	
	<xsl:variable name="VAR_VEHICLEUSE"> 			
		<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEUSERELATIVITY']/NODE[@ID='VEHICLEUSE']/ATTRIBUTES[@USECODE = $PUSECODE and @CLASS = $VARCLASS]/@FACTOR"/>		
	</xsl:variable>
			
	<xsl:variable name="VAR_USEFACTOR"> 
		<xsl:if test="$VAR_VEHICLEUSE !=''"> 
			<xsl:value-of select="100 - round($VAR_VEHICLEUSE * 100)"></xsl:value-of>
		</xsl:if>
	</xsl:variable>
		
	<xsl:variable name="VARVEHICLEUSEDESC">
		 <xsl:value-of select="VEHICLEUSEDESC"/> 
	</xsl:variable>
		
     <xsl:variable name="lcletters">abcdefghijklmnopqrstuvwxyz</xsl:variable>
	 <xsl:variable name="ucletters">ABCDEFGHIJKLMNOPQRSTUVWXYZ</xsl:variable>
				
	<xsl:choose> 
		<xsl:when test="$VAR_USEFACTOR &gt; 0"> 
			
				
				
				<tr class="midcolora">			 
				 <td><li> Discount for 
				<!-- Change Into Title Case --> 
				 <xsl:call-template name="TitleCase">
                      <xsl:with-param name="text"  
                         select="translate(normalize-space($VARVEHICLEUSEDESC),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')"/>
                 </xsl:call-template>
				  </li></td>
				 
				 <td>-<xsl:value-of select="$VAR_USEFACTOR" />% </td>
			    </tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:when>
		
		<!-- We multiply it with -1 to display a positive percentage-->
		<xsl:when test="$VAR_USEFACTOR &lt; 0"> 			
			<tr class="midcolora">
			 
				<td><li> Surcharge for 	
				         <!-- Change Into Title Case --> 
	                       <xsl:call-template name="TitleCase">
                                 <xsl:with-param name="text" select="translate(normalize-space($VARVEHICLEUSEDESC),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')"/>
                           </xsl:call-template> </li></td>
				<td>+<xsl:value-of select="$VAR_USEFACTOR * (-1)" />% </td>				 
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
	
</xsl:template>

<!-- ****************  Premier or Safe driver discount **************** -->
 

<xsl:template name ="DRIVERDISCOUNT">
	<!-- 1. Check for Premier or Safe Discount 
		 2. If Premier then call Premier template.
		 3. If Safe then check for continiuosly with wolurine Greater Than 3.
		 4. If Surcharge > 1 then do not call Safe Driver Template ..return 1.00
		 5.			else call SafeDriver template.-->
 
 	
	<xsl:variable name="VARNOPREMDRIVERDISC" select="NOPREMDRIVERDISC"></xsl:variable>
      
     <xsl:variable name="VARINSUREDWITHWOL">
                       <xsl:value-of select="user:GetWOL()" />
     </xsl:variable>
  
		<xsl:choose>
			<xsl:when test="$VARNOPREMDRIVERDISC='FALSE'  and LICUNDER3YRS = 'Y'">
				<xsl:call-template name ="PREMIERDISCOUNT"/>
			</xsl:when>
			
			
			<xsl:when test="$VARNOPREMDRIVERDISC='TRUE' and $VARINSUREDWITHWOL &gt; 2">		
				<xsl:call-template name ="SAFEDISCOUNT"/>
			</xsl:when>
				      
			    
			
			
			<xsl:otherwise></xsl:otherwise>  
		</xsl:choose> 
	     
	 <!-- Get The No. Of Years Insured With Wolurine -->
	    
	
	
</xsl:template> 

<xsl:template name ="PREMIERDISCOUNT">	
	<tr class= "midcolora">	
		<td><li> Premier Driver Discount </li></td>
		<td>-<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@PREMIERDRIVERCREDIT"/>% </td>
	</tr>	
	<xsl:value-of select="user:SetFlag(0)" /> 
</xsl:template>

<xsl:template name ="SAFEDISCOUNT">
<tr class= "midcolora">	
	<td><li>	Safe Driver Discount </li></td>
	<td>-<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@SAFEDRIVERCREDIT"/>% </td>
</tr>		
<xsl:value-of select="user:SetFlag(0)" /> 
</xsl:template>

<!-- End of Premier or Safe driver discount -->



<!-- Good student discount -->
<xsl:template name ="GOODSTUDENT">
	
	<xsl:variable name="STUDENTTYPE" select="GOODSTUDENT"> </xsl:variable>
	 
	<xsl:choose>
		<xsl:when test="$STUDENTTYPE ='TRUE'">
				
			<tr class="midcolora">
				<td><li> Good Student Discount</li></td>   
				<td width="212">-<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='GOODSTUDENT']/NODE[@ID ='GOODSTUDENTDISCOUNT']/ATTRIBUTES/@CREDIT"/>% </td>   
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:when>
	
			</xsl:choose>
		
</xsl:template>

<!--    Surcharge  -->
<xsl:template name ="SURCHARGE">
	 
	<!-- 1. Get the violation and accident points.
		2. Against each violation/accident point, fetch the surcharge  (After 13 points, calculate the surcharge using "For each additional" value
		3. Add the surcharges
		4. Divide the sum by 100 and add 1 -->
	
	
	<!--    Accident Surcharge     -->
	<xsl:variable name="ACCIDENTPOINTS"><xsl:value-of select="SUMOFACCIDENTPOINTS"/></xsl:variable>
	
	<xsl:variable name="ACCIDENTSURCHARGE">
	
	<xsl:choose>
		<xsl:when test="$ACCIDENTPOINTS !='' and $ACCIDENTPOINTS !='0' and $ACCIDENTPOINTS &gt; 0">
			<xsl:choose>
				<xsl:when test="$ACCIDENTPOINTS &gt; 13" >
							<xsl:variable name="VAR1"><xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=13]/@SURCHARGE"/></xsl:variable>
							<xsl:variable name="VAR2"><xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/@FOR_EACH_ADDITIONAL"/></xsl:variable>
							<xsl:value-of select="$VAR1 + round(($ACCIDENTPOINTS - 13) * $VAR2)"/> 		
						
				</xsl:when>
				<xsl:otherwise>
						<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=$ACCIDENTPOINTS]/@SURCHARGE"/> 		
				</xsl:otherwise>			
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
	</xsl:variable> 
	<xsl:value-of select="$ACCIDENTPOINTS"></xsl:value-of>
	<xsl:if test="$ACCIDENTSURCHARGE &gt; 0">
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
			<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
	</xsl:variable>
	
	<xsl:if test="$VIOLATIONPOINTS &gt; 0">
		<tr  class="midcolora">
			<td><li> Surcharge For Violation</li></td>
			 <td>+<xsl:value-of select="$VIOLATIONPOINTS" />% </td>
		</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
	</xsl:if>
	<!--Modified-->
	<xsl:variable name="VAR_SURCHARGE">
	
		<xsl:value-of select="(($ACCIDENTSURCHARGE + $VIOLATIONSURCHARGE) div 100) + 1"/>
	 
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

<!--test-->
<!--    Surcharge  -->
<xsl:template name ="VEHCLESURCHARGE">
	 
	<!-- 1. Get the violation and accident points.
		2. Against each violation/accident point, fetch the surcharge  (After 13 points, calculate the surcharge using "For each additional" value
		3. Add the surcharges
		4. Divide the sum by 100 and add 1 -->
	
	
	<!--    Accident Surcharge     -->
	<xsl:variable name="ACCIDENTPOINTS"><xsl:value-of select="SUMOFACCIDENTPOINTS"/></xsl:variable>
	
	<xsl:variable name="ACCIDENTSURCHARGE">
	
	<xsl:choose>
		<xsl:when test="$ACCIDENTPOINTS !='' and $ACCIDENTPOINTS !='0' and $ACCIDENTPOINTS &gt; 0">
			<xsl:choose>
				<xsl:when test="$ACCIDENTPOINTS &gt; 13" >
								<xsl:variable name="VAR1"><xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=13]/@SURCHARGE"/></xsl:variable>
								<xsl:variable name="VAR2"><xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/@FOR_EACH_ADDITIONAL"/></xsl:variable>
								<xsl:value-of select="$VAR1 + round(($ACCIDENTPOINTS - 13) * $VAR2)"/> 		
				</xsl:when>
				<xsl:otherwise>
						<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=$ACCIDENTPOINTS]/@SURCHARGE"/> 		
				</xsl:otherwise>			
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
	</xsl:variable> 
	
	<xsl:if test="$ACCIDENTSURCHARGE &gt; 0">
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
			<xsl:otherwise>0.00</xsl:otherwise>
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

<!--  Multi Policy discount  -->
 

<xsl:template name ="MULTIPOLICY_DISPLAY">

        
		<xsl:variable name="VAR_MULTIPOLICY"> 
		 <xsl:choose>
			<xsl:when test="POLICY/MULTIPOLICYAUTOHOMEDISCOUNT = 'Y'">
				 <!--Check For State     -->
			  <xsl:choose>
				<xsl:when test="POLICY/STATENAME = 'MICHIGAN'">
					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIPOLICY']/NODE[@ID ='MULTIPOLICYDISCOUNT']/ATTRIBUTES/@CREDIT"/> 		
				</xsl:when>
				<xsl:when test="POLICY/STATENAME = 'INDIANA'">
				   <xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIPOLICY']/NODE[@ID ='MULTIPOLICYDISCOUNT']/ATTRIBUTES/@CREDIT"/> 		
				</xsl:when>
			  </xsl:choose>	
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

<!--Trailblazer Discount -->


<xsl:template name ="TRAILER_BLAZER_DISPLAY">
<xsl:variable name="VAR_TRAILBLAZER"> 
	<xsl:choose>
		<xsl:when test="POLICY/QUALIFIESTRAIBLAZERPROGRAM = 'TRUE'">
			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TRAILBLAZER']/NODE[@ID ='TBP']/ATTRIBUTES/@BI_TRAILBLAZERDISCOUNT"/> 		
		</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	
	</xsl:choose>
</xsl:variable>

	<xsl:if test="$VAR_TRAILBLAZER &gt; 0">
			 
			<tr>
				<td class="midcolora"><li>Discount for Trailblazer </li></td>
				<td class="midcolora">-<xsl:value-of select="$VAR_TRAILBLAZER" />% </td>
			</tr>
			<xsl:value-of select="user:SetFlag(0)" /> 
		</xsl:if>
		
		
	
</xsl:template>
<!--End Trailer Discount-->

<!-- ****************************  For final display  ****************************  -->

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

<xsl:template name="AUTO_DESC">
  
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
 
</xsl:template>

<!-- ============================================================================= -->



<!-- ============================================================================ -->

<xsl:template name="PIP_DISPLAY">
 <xsl:variable name="PWAIVEWORKLOSS"  select="WAIVEWORKLOSS"></xsl:variable>
 <xsl:variable name="PDRIVERINCOME"   select="DRIVERINCOME"></xsl:variable> 
 <xsl:variable name="VARMEDPM"        select="MEDPM"></xsl:variable>

	<xsl:variable name="PNODEPENDENT">
   		<xsl:choose>
		<xsl:when test="DEPENDENTS ='1MORE'">1</xsl:when> 
		<xsl:otherwise>0</xsl:otherwise>
 		</xsl:choose>
   </xsl:variable>
         <!-- Full PIP ,Waiver of Work Loss, No dependants -->
       <xsl:choose>
		<xsl:when test="$VARMEDPM = 'PRIMARY'"> <!-- Full PIP --> 
				<xsl:variable name="VAR1">
					<xsl:choose>
						<xsl:when test="$PWAIVEWORKLOSS = 'TRUE' and $PNODEPENDENT ='1'"> 
			    				<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C6' ]/@RELATIVITY"/>
						</xsl:when>
						<xsl:otherwise>
							0
						</xsl:otherwise>
	        			</xsl:choose>
					</xsl:variable>
				<xsl:if test="$VAR1 &gt; 0">
					<tr>
						<td class="midcolora"><li>No Dependents and Waiver of Work Loss (PIP) </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR1 * 100)"/>% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
				</xsl:if>	 
	    <!-- Full PIP , No dependants, Low Income -->
				<xsl:variable name="VAR2">		
				<xsl:choose>
					<xsl:when test="$PNODEPENDENT ='1' and $PDRIVERINCOME = 'LOW'">
						    			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C5' ]/@RELATIVITY"/>
					</xsl:when>
					<xsl:otherwise>
							0
					</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>	
			<xsl:if test="$VAR2 &gt; 0">
					<tr>
						<td class="midcolora"><li>No Dependents and Low Income (PIP) </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR2 * 100)"/>% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
				</xsl:if>	
			<!-- Full PIP , Waiver of work loss -->
				<xsl:variable name="VAR3">		
				<xsl:choose>
		    			<xsl:when test="$PWAIVEWORKLOSS='TRUE'">
			    			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C4' ]/@RELATIVITY"/>
						</xsl:when>
						<xsl:otherwise>
							0
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>	
				<xsl:if test="$VAR3 &gt; 0 and $VAR1=0">
					<tr>
						<td class="midcolora"><li>Waiver Of Work Loss (PIP) </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR3 * 100)" />% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
				</xsl:if>	
				<!-- Full PIP , Low Income -->
				<xsl:variable name="VAR4">		
				<xsl:choose>		
					<xsl:when test="$PDRIVERINCOME='LOW'">
			    			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C3' ]/@RELATIVITY"/>
					</xsl:when>
						<xsl:otherwise>
							0
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="$VAR4 &gt; 0 and $VAR2 =0">
					<tr>
						<td class="midcolora"><li>Low Income (PIP) </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR4 * 100)"/>% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
				</xsl:if>	
		 <!-- Full PIP ,No dependants -->
				<xsl:variable name="VAR5">		
				<xsl:choose>	
					<xsl:when test ="$PNODEPENDENT='1'" > 
							<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C2' ]/@RELATIVITY"/>
					</xsl:when>
						<xsl:otherwise>
							0
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="$VAR5 &gt; 0 and $VAR2 =0 and $VAR1=0">
					<tr>
						<td class="midcolora"><li>No Dependent (PIP) </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR5 * 100)"/>% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
				</xsl:if>	
		         <!-- Full PIP -->		
				<xsl:variable name="VAR6">		
				<xsl:choose>	
					<xsl:when test ="$PWAIVEWORKLOSS = 'FALSE' and  $PNODEPENDENT ='0' and $PDRIVERINCOME ='HIGH'">
						<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C1' ]/@RELATIVITY"/>
					</xsl:when>
						<xsl:otherwise>
							0
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="$VAR6 &gt; 0">
					<tr>
						<td class="midcolora"><li>Full (PIP) </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR6 * 100)"/>% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
				</xsl:if>	
	      </xsl:when>
	  <xsl:when test="$VARMEDPM = 'EXCESSWAGE'">
			<!-- Excess Workloss  means - Excess Wage was selected. It has no link with the Waiver of Workloss 
										- Full PIP is included -->
			
			<!-- Full PIP and Excess Workloss, No dependants, Low Income -->
			<xsl:variable name="VAR1">		
				<xsl:choose>
			      <xsl:when test ="$PNODEPENDENT ='1'  and $PDRIVERINCOME='LOW'">
				 	<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C10' ]/@RELATIVITY"/>
				  </xsl:when>
				  <xsl:otherwise>
						0
				  </xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="$VAR1 &gt; 0">
					<tr>
						<td class="midcolora"><li>No Dependents And Low Income (Excess Wage) </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR1 * 100)"/>% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
				</xsl:if>	
				<!-- Full PIP and Excess Workloss, No dependants -->
				<xsl:variable name="VAR2">		
				<xsl:choose>
	    		      <xsl:when test ="$PNODEPENDENT ='1'">
				         <xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C9' ]/@RELATIVITY"/>
    	              </xsl:when>
  					<xsl:otherwise>
							0
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="$VAR2 &gt; 0 and $VAR1 =0">
					<tr>
						<td class="midcolora"><li>No Dependents (Excess Wage) </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR2 * 100)"/>% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
				</xsl:if>
				
				<!--   Full PIP and Excess Workloss , Low Income                    -->	
				
				<xsl:variable name="VAR3">		
				<xsl:choose>
			      <xsl:when test ="$PDRIVERINCOME = 'LOW'">
				      	<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C8' ]/@RELATIVITY"/>
				  </xsl:when>
						<xsl:otherwise>
							0
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="$VAR3 &gt; 0 and $VAR1 = 0">
					<tr>
						<td class="midcolora"><li>Low Income (Excess Wage) </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR3 * 100)"/>% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
				</xsl:if>
				
					
			<!-- Full PIP and Excess Workloss -->
			<xsl:variable name="VAR4">		
				<xsl:choose>
			           <xsl:when test ="$PDRIVERINCOME = 'HIGH' and $PNODEPENDENT = '0'" >
				     		<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C7' ]/@RELATIVITY"/>
						</xsl:when>
						<xsl:otherwise>
							0
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="$VAR4 &gt; 0">
					<tr>
						<td class="midcolora"><li>Excess Wage </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR4 * 100)" />% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
				</xsl:if>
		 </xsl:when>			
	  <xsl:when test="$VARMEDPM = 'EXCESSMEDICAL'"> <!-- Full PIP -->
	  
	  <!-- Excess Medical, No dependants ,Waiver of Work Loss --> 
				<xsl:variable name="VAR1">
					<xsl:choose>
						<xsl:when test="$PWAIVEWORKLOSS = 'TRUE' and $PNODEPENDENT ='1'"> 
			    					<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C20' ]/@RELATIVITY"/>
						</xsl:when>
						<xsl:otherwise>
							0
						</xsl:otherwise>
	        			</xsl:choose>
					</xsl:variable>
				<xsl:if test="$VAR1 &gt; 0">
					<tr>
						<td class="midcolora"><li>No Dependents and Waiver of Work Loss (Excess Medical) </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR1 * 100)" />% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
				</xsl:if>	 
	    <!-- Excess Medical, No dependants, Low Income -->
				<xsl:variable name="VAR2">		
				<xsl:choose>
					<xsl:when test="$PNODEPENDENT ='1' and $PDRIVERINCOME = 'LOW'">
						   <xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C15' ]/@RELATIVITY"/>
					</xsl:when>
					<xsl:otherwise>
							0
					</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>	
			<xsl:if test="$VAR2 &gt; 0">
					<tr>
						<td class="midcolora"><li>No Dependents and Low Income (Excess Medical) </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR2 * 100)"/>% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
				</xsl:if>	
			<!-- Excess Medical, Waiver of work loss -->
				<xsl:variable name="VAR3">		
				<xsl:choose>
		    			<xsl:when test="$PWAIVEWORKLOSS='TRUE'">
			    			<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C14' ]/@RELATIVITY"/>
						</xsl:when>
						<xsl:otherwise>
							0
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>	
				<xsl:if test="$VAR3 &gt; 0 and $VAR1=0">
					<tr>
						<td class="midcolora"><li>Waiver Of Work Loss (Excess Medical) </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR3 * 100)"/>% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
				</xsl:if>	
				<!-- Excess Medical,Low Income -->
				<xsl:variable name="VAR4">		
				<xsl:choose>		
					<xsl:when test="$PDRIVERINCOME='LOW'">
			    		<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C13' ]/@RELATIVITY"/>
					</xsl:when>
						<xsl:otherwise>
							0
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="$VAR4 &gt; 0 and $VAR2=0">
					<tr>
						<td class="midcolora"><li>Low Income (Excess Medical) </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR4 * 100)"/>% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
				</xsl:if>	
		
			<!-- Excess Medical, No dependants -->
				<xsl:variable name="VAR5">		
				<xsl:choose>	
					<xsl:when test ="$PNODEPENDENT='1'" > 
								<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C12' ]/@RELATIVITY"/>
					</xsl:when>
						<xsl:otherwise>
							0
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="$VAR5 &gt; 0 and $VAR1=0 and $VAR2=0">
					<tr>
						<td class="midcolora"><li>No Dependent (Excess Medical) </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR5 * 100)"/>% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
				</xsl:if>	
		        	<!-- Excess Medical -->	
				<xsl:variable name="VAR6">		
				<xsl:choose>	
					<xsl:when test ="$PWAIVEWORKLOSS = 'FALSE' and  $PNODEPENDENT ='0' and $PDRIVERINCOME ='HIGH'">
						<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C11' ]/@RELATIVITY"/>
					</xsl:when>
						<xsl:otherwise>
							0
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="$VAR6 &gt; 0">
					<tr>
						<td class="midcolora"><li>Excess Medical </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR6 * 100)"/>% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
				</xsl:if>	
	      </xsl:when>
	      
	      
	       <xsl:when test="$VARMEDPM = 'EXCESSBOTH'">
			<!-- Excess Both is for Excess Wage/Medical 
			 Excess Workloss  means - Excess Wage was selected. It has no link with the Waiver of Workloss -->
			
			<!-- Excess Medical and Excess Workloss, No dependants, Low Income -->
			<xsl:variable name="VAR1">		
				<xsl:choose>
			      <xsl:when test ="$PNODEPENDENT ='1'  and $PDRIVERINCOME='LOW'">
				  	<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C10' ]/@RELATIVITY"/>
				  </xsl:when>
			     <xsl:otherwise>
				   0
			  	 </xsl:otherwise>
			  </xsl:choose>
				</xsl:variable>
				<xsl:if test="$VAR1 &gt; 0">
					<tr>
						<td class="midcolora"><li>No Dependents And Low Income (Excess Medical and Excess Workloss) </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR1 * 100)"/>% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
				</xsl:if>	
			<!-- Excess Medical and Excess Workloss, No dependants -->
				<xsl:variable name="VAR2">		
				<xsl:choose>
			      <xsl:when test ="$PNODEPENDENT ='1'">
				 		<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C9' ]/@RELATIVITY"/>
				 </xsl:when>
						<xsl:otherwise>
							0
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="$VAR2 &gt; 0 and $VAR1=0">
					<tr>
						<td class="midcolora"><li>No Dependents (Excess Medical and Excess Workloss) </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR2 * 100)"/>% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
				</xsl:if>
				
		
			<!-- Excess Medical and Excess Workloss , Low Income -->
				
				<xsl:variable name="VAR3">		
				<xsl:choose>
			      <xsl:when test ="$PDRIVERINCOME = 'LOW'">
				 		<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C8' ]/@RELATIVITY"/>
				  </xsl:when>
						<xsl:otherwise>
							0
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="$VAR3 &gt; 0 and $VAR1=0">
					<tr>
						<td class="midcolora"><li>Low Income (Excess Medical and Excess Workloss) </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR3 * 100)"/>% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
				</xsl:if>
				
					
			<!-- Excess Medical and Excess Workloss -->
			<xsl:variable name="VAR4">		
				<xsl:choose>
			      <xsl:when test ="$PDRIVERINCOME = 'HIGH' and $PNODEPENDENT = '0'" >
				  		<xsl:value-of select="$myDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C7' ]/@RELATIVITY"/>
				  </xsl:when>
						<xsl:otherwise>
							0
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="$VAR4 &gt; 0">
					<tr>
						<td class="midcolora"><li>Excess Medical and Excess Workloss </li></td>
						<td class="midcolora">-<xsl:value-of select="100 - round($VAR4 * 100)"/>% </td>
					</tr>
					<xsl:value-of select="user:SetFlag(0)" /> 
				</xsl:if>
		 </xsl:when>	

    <xsl:otherwise>
    
   </xsl:otherwise>
  </xsl:choose>

</xsl:template>




</xsl:stylesheet>