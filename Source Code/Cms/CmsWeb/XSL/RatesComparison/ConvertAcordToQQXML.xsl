<!-- ==================================================================================================
File Name			:	ACORD_QUICKQUOTE.xsl
Purpose				:	Convert ACORD xml into QuickQuote xml 
Name				:	Ashwani
Date				:	16 Nov. 2006  
======================================================================================================== -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:user="urn:user-namespace-here">
	<msxsl:script language="c#" implements-prefix="user">
<![CDATA[ 
		int intCSS=1;
		int lossescount=0;
		string AsinedVehId="";
		public int GetlossCount(int cntloss)
		{
			int val=0;
			if(cntloss < 0)
			{
				lossescount=0;	
			}
			else
			{
			
				val=cntloss;
				lossescount = lossescount + val;
				
			}
			return lossescount;
		}
		public string GetAssinedVehicleIds(string IDs)
		{
			int Val=0;
			if(IDs=="")
			{
				AsinedVehId="";
			}
			else if(IDs=="-1")
			{
				AsinedVehId = AsinedVehId;
			}
			else
			{
				AsinedVehId = AsinedVehId + '^' + IDs;
				Val++;
			}
			return AsinedVehId;
		}
				
]]></msxsl:script>
	<xsl:variable name="AutoMappingDoc" select="document('FactorPath')"></xsl:variable>
	<xsl:variable name="VarYearNineeffYear" select="2009" />
	<xsl:variable name="VarMonthNineeffMonth" select="07" />
	<xsl:key name="driverList" match="PersonInfo" use="DriverIncome" />
	<xsl:template match="/">
		&lt;QUICKQUOTE&gt;			
				<!-- Call Main Template -->
				<xsl:call-template name="POLICYNODES" />			
		&lt;/QUICKQUOTE&gt;
	</xsl:template>
	<!-- Start Main Template  -->
	<xsl:template name="POLICYNODES">
		<!--Start Policy Level -->
		&lt;POLICY&gt;
			&lt;STATENAME&gt;<xsl:call-template name="DISPLAY_STATE_NAME" />&lt;/STATENAME&gt;
			&lt;NEWBUSINESS&gt;True&lt;/NEWBUSINESS&gt; <!--Always a new business-->
			&lt;RENEWAL&gt;False&lt;/RENEWAL&gt;			
			&lt;QUOTEEFFDATE&gt;<xsl:call-template name="QQ_EFFECTIVE_DATE" />&lt;/QUOTEEFFDATE&gt;							
			&lt;POLICYTERMS&gt;<xsl:call-template name="GET_TERM" />&lt;/POLICYTERMS&gt;	
			<!-- Fetch the postal code of Garage Location -->
			&lt;ZIPCODE&gt;<xsl:value-of select="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/Location/Addr/PostalCode" />&lt;/ZIPCODE&gt;		
			<!--Here we need to fetch territory from DB-->					
			&lt;TERRITORY&gt;36&lt;/TERRITORY&gt;					
			&lt;YEARSCONTINSURED&gt;<xsl:call-template name="YEARS_CONTS_INSURED" />&lt;/YEARSCONTINSURED&gt;		
			<!--Since its a new business case so it will be zero by default --> 		
			&lt;YEARSCONTINSUREDWITHWOLVERINE&gt;<xsl:call-template name="YEARS_CONTS_INSURED_WOLV" />&lt;/YEARSCONTINSUREDWITHWOLVERINE&gt;		
			<!-- Not Found --> 
			&lt;LOSSES_CHARGEABLE_NONCHARGEABLE&gt;
				<xsl:call-template name="LOSSES_CHARGEABLE_NONCHARGEABLE" />			
			&lt;/LOSSES_CHARGEABLE_NONCHARGEABLE&gt;						
			<!--Start Coverages at policy level -->	
			<xsl:for-each select="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/OtherOrPriorPolicy">
				<!-- BI -->
				&lt;BI&gt;<xsl:call-template name="BI_COVERAGE"></xsl:call-template>&lt;/BI&gt;										
				&lt;BILIMIT1&gt;<xsl:call-template name="BILIMIT1_COVERAGE"></xsl:call-template>&lt;/BILIMIT1&gt;
				&lt;BILIMIT2&gt;<xsl:call-template name="BILIMIT2_COVERAGE"></xsl:call-template>&lt;/BILIMIT2&gt;
				<!-- PD -->
				&lt;PD&gt;<xsl:call-template name="PDCOVERAGE" />
				&lt;/PD&gt;
				<!--CSL -->								
				&lt;CSL&gt;<xsl:call-template name="CSLCOVERAGE" />&lt;/CSL&gt;	
				<!--MEDPM-->	
				&lt;MEDPM&gt;
				<xsl:call-template name="PIPCOVERAGE" />
				&lt;/MEDPM&gt;					
				&lt;ISUNDERINSUREDMOTORISTS&gt;<xsl:choose>
				<xsl:when test="Coverage[CoverageCd='PUMSP' or CoverageCd='UM']/Limit[LimitAppliesToCd='PerPerson']/FormatCurrencyAmt/Amt >0">True</xsl:when>
				<xsl:when test="Coverage[CoverageCd='UMCSL']/Limit[LimitAppliesToCd='CSL']/FormatCurrencyAmt/Amt >0">True</xsl:when>
				<xsl:otherwise>False</xsl:otherwise>
			</xsl:choose>&lt;/ISUNDERINSUREDMOTORISTS&gt;		
			
				&lt;UMSPLIT&gt;<xsl:call-template name="UMSPLIT_COVERAGE"></xsl:call-template>&lt;/UMSPLIT&gt;
				&lt;UMSPLITLIMIT1&gt;<xsl:call-template name="UMSPLITLIMIT1_COVERAGE"></xsl:call-template>&lt;/UMSPLITLIMIT1&gt;
				&lt;UMSPLITLIMIT2&gt;<xsl:call-template name="UMSPLITLIMIT2_COVERAGE"></xsl:call-template>&lt;/UMSPLITLIMIT2&gt;				
				&lt;UMCSL&gt;<!--<xsl:choose>
				<xsl:when test="Coverage[CoverageCd='UMCSL']/Limit[LimitAppliesToCd='CSL']/FormatCurrencyAmt/Amt >0">
				<xsl:value-of select="Coverage[CoverageCd='UMCSL']/Limit[LimitAppliesToCd='CSL']/FormatCurrencyAmt/Amt " />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			    </xsl:choose>--><xsl:call-template name="UMCSLCOVERAGE" />&lt;/UMCSL&gt;			
				&lt;PDLIMIT&gt;<xsl:choose>
				<xsl:when test="Coverage[CoverageCd='PD']/Limit[LimitAppliesToCd='PerAcc']/FormatCurrencyAmt/Amt >0">
					<xsl:value-of select="Coverage[CoverageCd='PD']/Limit[LimitAppliesToCd='PerPerson']/FormatCurrencyAmt/Amt" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>&lt;/PDLIMIT&gt;
				<!-- Not Found --> 						
				&lt;TYPE&gt;<xsl:choose>
				<xsl:when test="normalize-space(./Location/Addr/StateProvCd)='IN'">
					<xsl:choose>
						<xsl:when test="Coverage[CoverageCd='PUMSP' or CoverageCd='UM']/Limit[LimitAppliesToCd='PerPerson']/FormatCurrencyAmt/Amt >0">BI ONLY</xsl:when>
						<xsl:when test="Coverage[CoverageCd='PUMSP' or CoverageCd='UM']/Limit[LimitAppliesToCd='PerPerson']/FormatCurrencyAmt/Amt >0 and Coverage[CoverageCd='PUNPD']/Limit[LimitAppliesToCd='PerPerson']/FormatCurrencyAmt/Amt >0">BIPD</xsl:when>
						<xsl:otherwise>NO COVERAGE</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:when test="normalize-space(./Location/Addr/StateProvCd)='MI'">UNINSURED MOTORISTS</xsl:when>
				<xsl:otherwise>NO COVERAGE</xsl:otherwise>
			</xsl:choose>&lt;/TYPE&gt;	
				&lt;PDDEDUCTIBLE&gt;<xsl:choose>
				<xsl:when test="normalize-space(./Coverage[CoverageCd='UM']/Deductible/FormatCurrencyAmt/Amt)!=''">
					<xsl:value-of select="./Coverage[CoverageCd='UM']/Deductible/FormatCurrencyAmt/Amt" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>&lt;/PDDEDUCTIBLE&gt;	
			</xsl:for-each>	
			<!--End Coverages at policy level -->											
				<!-- MULTI POLICY DISCOUNT --> 		
				&lt;MULTIPOLICYAUTOHOMEDISCOUNT&gt;<xsl:call-template name="MULTIPOLICYAUTOHOMEDISCOUNT"></xsl:call-template>&lt;/MULTIPOLICYAUTOHOMEDISCOUNT&gt;
				<!-- Not Found --> 		
				&lt;WEARINGSEATBELT&gt;<xsl:call-template name="WEARINGSEATBELT" />&lt;/WEARINGSEATBELT&gt;		
				<!-- Not Found --> 		
				&lt;QUALIFIESTRAIBLAZERPROGRAM&gt;False&lt;/QUALIFIESTRAIBLAZERPROGRAM&gt;		
				<!-- INSURANCE SCORE --> 		
				&lt;INSURANCESCORE&gt;<xsl:call-template name="INSURANCESCORE"></xsl:call-template>&lt;/INSURANCESCORE&gt;
				&lt;CUSTOMERINSURANCERECEIVEDDATE&gt;&lt;/CUSTOMERINSURANCERECEIVEDDATE&gt;
				&lt;REASONCODE1&gt;&lt;/REASONCODE1&gt;
				&lt;REASONCODE2&gt;&lt;/REASONCODE2&gt;
				&lt;REASONCODE3&gt;&lt;/REASONCODE3&gt;
				&lt;REASONCODE4&gt;&lt;/REASONCODE4&gt;
				<!-- Not Required -->
				&lt;CALLED&gt;QQ&lt;/CALLED&gt;
				<!-- Here we are integrating extra nodes which are required to calculate premium -->
				&lt;LOBID&gt;<xsl:call-template name="LOBID" />&lt;/LOBID&gt; <!--LOBID -->
				&lt;STATEID&gt;<xsl:call-template name="STATEID" />&lt;/STATEID&gt; <!--State ID-->								
				<!--For Violation section -->	
				&lt;ACCIDENTVIOLATIONS&gt; 			
					<xsl:for-each select="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/AccidentViolation">
						 	&lt;ACCIDENTVIOLATION DRIVERREF=&quot;<xsl:value-of select="substring(./@DriverRef,11,1)" />&quot; ACCID=&quot;<xsl:value-of select="./@AccId" />&quot; ACCREF=&quot;<xsl:value-of select="./@AccRef" />&quot; &gt; 
								&lt;ACCIDENTVIOLATIONDT&gt;<xsl:value-of select="./AccidentViolationDt" />&lt;/ACCIDENTVIOLATIONDT&gt;						
								&lt;ACCIDENTVIOLATIONCD&gt;<xsl:value-of select="./AccidentViolationCd" />&lt;/ACCIDENTVIOLATIONCD&gt;
								&lt;DAMAGETOTALAMT&gt;<xsl:value-of select="./DamageTotalAmt" />&lt;/DAMAGETOTALAMT&gt;
							&lt;/ACCIDENTVIOLATION&gt;														
					</xsl:for-each>	
				&lt;/ACCIDENTVIOLATIONS&gt;	
				&lt;RQUID&gt;<xsl:value-of select="ACORD/InsuranceSvcRq/RqUID" /><!--xsl:call-template name="RQUID" /-->&lt;/RQUID&gt; <!-- GUID -->											
			&lt;/POLICY&gt;			
		<!--End Policy Level -->	
		<!--Start Vehicle level -->		
		&lt;VEHICLES&gt;	
			<xsl:for-each select="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersVeh">
			&lt;VEHICLE ID=&quot;<xsl:value-of select="substring(./@id,8,1)" />&quot; &gt;						
				&lt;VEHICLEROWID&gt;<xsl:value-of select="substring(./@id,8,1)" />&lt;/VEHICLEROWID&gt;					
				&lt;AGE&gt;<xsl:call-template name="VEHICLE_AGE" />&lt;/AGE&gt;									
				&lt;VEHICLETYPEUSE&gt;<xsl:call-template name="VEHICLE_TYPE_USE" />&lt;/VEHICLETYPEUSE&gt;								
				&lt;VEHICLETYPE&gt;<xsl:call-template name="VEHICLEUSE_DEFAULT" />&lt;/VEHICLETYPE&gt;
				&lt;VEHICLETYPEDESC&gt;<xsl:value-of select="./VehBodyTypeCd" />&lt;/VEHICLETYPEDESC&gt;
				<!--Suspened Comp Type Vehicle Implementation Pending Waiting for discusion  -->
				&lt;VEHICLETYPE_SCO&gt;<xsl:call-template name="VEHICLE_COMPONLY" />&lt;/VEHICLETYPE_SCO&gt;	
				<!--Governing Driver Node-->			
				&lt;DRIVESVEHICLE&gt;Principal&lt;/DRIVESVEHICLE&gt;
				&lt;GOVERNINGDRIVER&gt;<xsl:call-template name="DRIVERSVEHICLE">
				<xsl:with-param name="VEHICLEID" select="substring(./@id,8,1)" />
			</xsl:call-template>&lt;/GOVERNINGDRIVER&gt;
				<!--&lt;DRIVESVEHICLE&gt;<xsl:value-of select="PersDriverInfo/DriverType"></xsl:value-of>&lt;/DRIVESVEHICLE&gt;
				&lt;GOVERNINGDRIVER&gt;<xsl:value-of select="PersDriverInfo/GoverningDriver"></xsl:value-of>&lt;/GOVERNINGDRIVER&gt;-->
				<!--END Governing Driver Node-->			
				<!--Not Required -->
				&lt;VEHICLERATINGCODE&gt;&lt;/VEHICLERATINGCODE&gt;			
				<!-- Not found -->
				&lt;VEHICLECLASS&gt;PA&lt;/VEHICLECLASS&gt;				
				&lt;VEHICLECLASS_DESC&gt;PA&lt;/VEHICLECLASS_DESC&gt;
				<!-- Not found -->
				&lt;VEHICLECLASSCOMPONENT1&gt;P&lt;/VEHICLECLASSCOMPONENT1&gt;												
				<!-- Not found -->
				&lt;VEHICLECLASSCOMPONENT2&gt;A&lt;/VEHICLECLASSCOMPONENT2&gt;												
				&lt;VEHICLECLASS_COMM&gt;				
					<!-- Not found -->
				&lt;/VEHICLECLASS_COMM&gt;												
				&lt;VEHICLECLASS_DESC_COMM&gt;				
					<!-- Not found -->
				&lt;/VEHICLECLASS_DESC_COMM&gt;												
				&lt;VEHICLECLASSCOMPONENT_COMM1&gt;				
					<!-- Not found -->
				&lt;/VEHICLECLASSCOMPONENT_COMM1&gt;								
				&lt;VEHICLECLASSCOMPONENT_COMM2&gt;				
					<!-- Not found -->
				&lt;/VEHICLECLASSCOMPONENT_COMM2&gt;						
				&lt;YEAR&gt;<xsl:value-of select="./ModelYear" />&lt;/YEAR&gt;								
				&lt;MAKE&gt;<xsl:value-of select="./Manufacturer" />&lt;/MAKE&gt;				
				&lt;MODEL&gt;<xsl:value-of select="./Model" />&lt;/MODEL&gt;					
				&lt;VIN&gt;<xsl:value-of select="./VehIdentificationNumber" />&lt;/VIN&gt;				
				<!-- Not Found -->
				&lt;SYMBOL&gt;
				<xsl:choose>
				<xsl:when test="substring(./VehIdentificationNumber,6,1)=''">
					<xsl:value-of select="./VehIdentificationNumber" />
				</xsl:when>
				<xsl:otherwise>1</xsl:otherwise>
			</xsl:choose>
				&lt;/SYMBOL&gt;				
				<!-- Not Found -->
				&lt;COST&gt;						
				<xsl:choose>
				<xsl:when test="normalize-space(./CostNewAmt/Amt)!=''">
					<xsl:value-of select="./CostNewAmt/Amt" />
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>				
				&lt;/COST&gt;				
				&lt;ANNUALMILES&gt;<xsl:value-of select="./EstimatedAnnualDistance/NumUnits" />&lt;/ANNUALMILES&gt;
				<!--Not Required-->
				&lt;RADIUSOFUSE&gt;0&lt;/RADIUSOFUSE&gt;					
				<!--This is temporary soluation,needs to be remove later-->				
				<xsl:variable name="VAR_NUMUNITS">
				<xsl:value-of select="./DistanceOneWay/NumUnits" />
			</xsl:variable>	
				<xsl:variable name="V_USE">
				<xsl:choose>
					<xsl:when test="normalize-space(./VehUseCd)='DU' and ($VAR_NUMUNITS &gt; '11' and $VAR_NUMUNITS &lt; '25')">WS</xsl:when>
					<xsl:when test="normalize-space(./VehUseCd)='DU' and ($VAR_NUMUNITS &lt; '11')">PP</xsl:when>
					<xsl:when test="normalize-space(./VehUseCd)='DO' and ($VAR_NUMUNITS &gt; '25')">CPW</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="./VehUseCd" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:variable>						
				&lt;USE&gt;<xsl:value-of select="$V_USE" />&lt;/USE&gt;															
				&lt;VEHICLEUSE&gt;<xsl:call-template name="VEHICLEUSE_RATING_CODE">
				<xsl:with-param name="VEH_USE" select="$V_USE"></xsl:with-param>
			</xsl:call-template>&lt;/VEHICLEUSE&gt;				
				&lt;VEHICLEUSEDESC&gt;<xsl:call-template name="VEHICLEUSEDESC" />&lt;/VEHICLEUSEDESC&gt;	
				&lt;MILESEACHWAY&gt;<xsl:call-template name="MILESEACHWAY"></xsl:call-template>&lt;/MILESEACHWAY&gt; 
				<!--Not found -->
				&lt;CARPOOL&gt;<xsl:call-template name="CARPOOL" />&lt;/CARPOOL&gt;													
				<!--Not found -->
				&lt;SNOWPLOW&gt;&lt;/SNOWPLOW&gt;									
				&lt;SNOWPLOWCODE&gt;&lt;/SNOWPLOWCODE&gt;
				<!--DriverIncome and NoDependents-->	
			<xsl:variable name="VAR_ASSIGNED_DRIVER">
				<xsl:call-template name="DRIVERSVEHICLE" />
			</xsl:variable>
				<xsl:variable name="VAR_INCOME">
				<xsl:for-each select="//PersDriver[@id=$VAR_ASSIGNED_DRIVER]">
					<xsl:if test="current()/DriverInfo/PersonInfo/DriverIncome &lt; 9">LOW</xsl:if>
				</xsl:for-each>
			</xsl:variable>
			&lt;DRIVERINCOME&gt;
			<!--xsl:choose>
				<xsl:when test="$VAR_INCOME =''">HIGH</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="normalize-space($VAR_INCOME)" />
				</xsl:otherwise>
			</xsl:choose-->
			<xsl:variable name="VAR_DRIVERINCOMEDISC">
				<xsl:call-template name="DRIVERINCOMEDISC" />
			</xsl:variable>
			<xsl:choose>
				<xsl:when test="$VAR_DRIVERINCOMEDISC='LOW'">LOW</xsl:when>
				<xsl:otherwise>HIGH</xsl:otherwise>
			</xsl:choose>
			&lt;/DRIVERINCOME&gt;
			<xsl:variable name="VAR_DEPENDENTS">
				<xsl:for-each select="//PersDriver[@id=$VAR_ASSIGNED_DRIVER]">
					<xsl:if test="current()/DriverInfo/PersonInfo/NumDependents &gt; 0">1MORE</xsl:if>
				</xsl:for-each>
			</xsl:variable>
			&lt;DEPENDENTS&gt;<!--xsl:choose>
				<xsl:when test="$VAR_DEPENDENTS =''">NDEP</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="normalize-space($VAR_DEPENDENTS)" />
				</xsl:otherwise>
			</xsl:choose-->
			<xsl:variable name="VAR_NODEPENDENTDISC">
				<xsl:call-template name="NODEPENDENTDISC" />
			</xsl:variable>
			<xsl:choose>
				<xsl:when test="$VAR_NODEPENDENTDISC='NDEP'">NDEP</xsl:when>
				<xsl:otherwise>1MORE</xsl:otherwise>
			</xsl:choose>
			&lt;/DEPENDENTS&gt;			
			<!--Check for WAIVEWORKLOSS-->
				<xsl:variable name="WAVERLOSS" select="./Coverage[CoverageCd='PIP']/Option[OptionTypeCd='Bene3']/OptionCd" />
				&lt;WAIVEWORKLOSS&gt;
				<xsl:choose>
				<xsl:when test="$WAVERLOSS='R'">TRUE</xsl:when>
				<xsl:otherwise>FALSE</xsl:otherwise>
			</xsl:choose>
				&lt;/WAIVEWORKLOSS&gt;
				 
				&lt;COMPREHENSIVEDEDUCTIBLE&gt;<xsl:call-template name="COMPREHENSIVEDEDUCTIBLE" />&lt;/COMPREHENSIVEDEDUCTIBLE&gt;					
				&lt;COLLISIONDEDUCTIBLE&gt;<xsl:call-template name="COLLISIONDEDUCTIBLE" />  <xsl:call-template name="COVERAGECOLLISIONTYPE" />&lt;/COLLISIONDEDUCTIBLE&gt;	
				&lt;COVGCOLLISIONTYPE&gt;<xsl:call-template name="COVERAGECOLLISIONTYPE" />&lt;/COVGCOLLISIONTYPE&gt;	
				<!--Not found -->
				&lt;COVGCOLLISIONDEDUCTIBLE&gt;<xsl:call-template name="COLLISIONDEDUCTIBLE" />&lt;/COVGCOLLISIONDEDUCTIBLE&gt;	
				&lt;ROADSERVICE&gt;
					<xsl:call-template name="ROADSERVICE" />
				&lt;/ROADSERVICE&gt;				
				&lt;RENTALREIMBURSEMENT&gt;<xsl:call-template name="RENTALREMBURSEMENT" />&lt;/RENTALREIMBURSEMENT&gt;
				&lt;RENTALREIMLIMITDAY&gt;<xsl:call-template name="RENTALREMBURSEMENTMIN" />&lt;/RENTALREIMLIMITDAY&gt;
				&lt;RENTALREIMMAXCOVG&gt;<xsl:call-template name="RENTALREMBURSEMENTMAX" />&lt;/RENTALREIMMAXCOVG&gt;
				<!--Not found -->
				&lt;MINITORTPDLIAB&gt;<xsl:choose>
				<xsl:when test="Coverage[CoverageCd='LPD']/Limit[LimitAppliesToCd='Coverage']/FormatCurrencyAmt/Amt >0">True</xsl:when>
				<xsl:otherwise>False</xsl:otherwise>
			</xsl:choose>&lt;/MINITORTPDLIAB&gt;
				&lt;LOANLEASEGAP&gt;
				<xsl:choose>
				<xsl:when test="Coverage[CoverageCd='LOAN']!=''">LOAN</xsl:when>
				<xsl:when test="Coverage[CoverageCd='LEASE']!=''">LEASE</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>						
				&lt;/LOANLEASEGAP&gt;				
				&lt;ISANTILOCKBRAKESDISCOUNTS&gt;<xsl:call-template name="ANTIBREAKDISCOUNTS"></xsl:call-template>&lt;/ISANTILOCKBRAKESDISCOUNTS&gt;
				&lt;AIRBAGDISCOUNT&gt;
				<xsl:variable name="AIRBAGTYPE" select="normalize-space(./AirBagTypeCd)" />
				<xsl:choose>
				<xsl:when test="$AIRBAGTYPE = 'Driver'">C</xsl:when>
				<xsl:when test="$AIRBAGTYPE = 'FrontBoth'">D</xsl:when>
				<xsl:when test="$AIRBAGTYPE = 'FrontSide'">B</xsl:when>
				<xsl:when test="$AIRBAGTYPE = ''"></xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
				&lt;/AIRBAGDISCOUNT&gt;
				<!--Not found -->
				&lt;IS200SOUNDREPRODUCING&gt;False&lt;/IS200SOUNDREPRODUCING&gt;
				&lt;SOUNDRECEIVINGTRANSMITTINGSYSTEM&gt;
					<!--Not found -->
				&lt;/SOUNDRECEIVINGTRANSMITTINGSYSTEM&gt;
				<!--Not found -->
				&lt;MULTICARDISCOUNTDESC&gt;Not applicable&lt;/MULTICARDISCOUNTDESC&gt;				
				<!--Not found -->
				&lt;MULTICARDISCOUNTCODE&gt;NA&lt;/MULTICARDISCOUNTCODE&gt;
				<!--Not found -->
				&lt;MULTICARDISCOUNT&gt;<xsl:call-template name="MULTIDISCOUNT"></xsl:call-template>&lt;/MULTICARDISCOUNT&gt;
				<!--Not found -->
				&lt;INSURANCEAMOUNT&gt;0&lt;/INSURANCEAMOUNT&gt;								
				&lt;EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE&gt;&lt;/EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE&gt; <!--Not Required -->
				&lt;COLLISIONTYPEDED&gt;
					<!--Not found -->
				&lt;/COLLISIONTYPEDED&gt;				
				&lt;EXTRAEQUIPCOLLISIONTYPE&gt;&lt;/EXTRAEQUIPCOLLISIONTYPE&gt; <!--Not Required -->
				&lt;EXTRAEQUIPCOLLISIONDEDUCTIBLE&gt;0&lt;/EXTRAEQUIPCOLLISIONDEDUCTIBLE&gt; <!--Not Required -->				
				<!--Not found -->
				&lt;ZIPCODEGARAGEDLOCATION&gt;<xsl:value-of select="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/Location/Addr/PostalCode" />&lt;/ZIPCODEGARAGEDLOCATION&gt;
				&lt;GARAGEDLOCATION&gt;<xsl:value-of select="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/Location/Addr/PostalCode" />&lt;/GARAGEDLOCATION&gt;		<!--Need to fetch from DB -->			
				&lt;TERRCODEGARAGEDLOCATION&gt;&lt;/TERRCODEGARAGEDLOCATION&gt; <!--Not Required -->				
				<!--Start Coverages at vehicle level -->	
				<!-- BI -->
				&lt;BI&gt;<xsl:call-template name="BI_COVERAGE"></xsl:call-template>&lt;/BI&gt;									
				&lt;BILIMIT1&gt;<xsl:call-template name="BILIMIT1_COVERAGE"></xsl:call-template>&lt;/BILIMIT1&gt;
				&lt;BILIMIT2&gt;<xsl:call-template name="BILIMIT2_COVERAGE"></xsl:call-template>&lt;/BILIMIT2&gt;
				<!-- PD -->
				&lt;PD&gt;<xsl:call-template name="PDCOVERAGE" />&lt;/PD&gt;
				<!--CSL -->								
				&lt;CSL&gt;<xsl:call-template name="CSLCOVERAGE" />&lt;/CSL&gt;
				<!--MEDPM-->		
				&lt;MEDPM&gt;
				<xsl:call-template name="PIPCOVERAGE" />
				&lt;/MEDPM&gt;							
				&lt;ISUNDERINSUREDMOTORISTS&gt;<xsl:choose>
				<xsl:when test="Coverage[CoverageCd='PUMSP' or CoverageCd='UM']/Limit[LimitAppliesToCd='PerPerson']/FormatCurrencyAmt/Amt >0">True</xsl:when>
				<xsl:when test="Coverage[CoverageCd='UMCSL']/Limit[LimitAppliesToCd='CSL']/FormatCurrencyAmt/Amt >0">True</xsl:when>
				<xsl:otherwise>False</xsl:otherwise>
			</xsl:choose>&lt;/ISUNDERINSUREDMOTORISTS&gt;
				&lt;UMSPLIT&gt;<xsl:call-template name="UMSPLIT_COVERAGE"></xsl:call-template>&lt;/UMSPLIT&gt;
				&lt;UMSPLITLIMIT1&gt;<xsl:call-template name="UMSPLITLIMIT1_COVERAGE"></xsl:call-template>&lt;/UMSPLITLIMIT1&gt;
				&lt;UMSPLITLIMIT2&gt;<xsl:call-template name="UMSPLITLIMIT2_COVERAGE"></xsl:call-template>&lt;/UMSPLITLIMIT2&gt;				
				&lt;UMCSL&gt;<!--<xsl:choose>
				<xsl:when test="Coverage[CoverageCd='UMCSL']/Limit[LimitAppliesToCd='CSL']/FormatCurrencyAmt/Amt >0">
					<xsl:value-of select="Coverage[CoverageCd='UMCSL']/Limit[LimitAppliesToCd='CSL']/FormatCurrencyAmt/Amt" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
				</xsl:choose--><xsl:call-template name="UMCSLCOVERAGE" />&lt;/UMCSL&gt;		
				&lt;UIMSPLIT&gt;<xsl:call-template name="UMSPLIT_COVERAGE"></xsl:call-template>&lt;/UIMSPLIT&gt;
				&lt;UIMSPLITLIMIT1&gt;<xsl:call-template name="UMSPLITLIMIT1_COVERAGE"></xsl:call-template>&lt;/UIMSPLITLIMIT1&gt;
				&lt;UIMSPLITLIMIT2&gt;<xsl:call-template name="UMSPLITLIMIT2_COVERAGE"></xsl:call-template>&lt;/UIMSPLITLIMIT2&gt;				
				&lt;UIMCSL&gt;<xsl:call-template name="UMCSLCOVERAGE" />&lt;/UIMCSL&gt;
				&lt;PDLIMIT&gt;<xsl:choose>
				<xsl:when test="Coverage[CoverageCd='PD']/Limit[LimitAppliesToCd='PerAcc']/FormatCurrencyAmt/Amt >0">
					<xsl:value-of select="Coverage[CoverageCd='PD']/Limit[LimitAppliesToCd='PerPerson']/FormatCurrencyAmt/Amt" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>&lt;/PDLIMIT&gt;
				<!-- Not Found --> 						
				&lt;TYPE&gt;<xsl:call-template name="TYPE_DISPLAY" />&lt;/TYPE&gt;													
				&lt;PDDEDUCTIBLE&gt;<xsl:call-template name="PDDEDUCTIBLE" />&lt;/PDDEDUCTIBLE&gt;	
				
				<!-- End Coverages at vehicle level -->					
				&lt;MEDPMDEDUCTIBLE&gt;
					<xsl:value-of select="normalize-space(./Coverage[CoverageCd='PIP']/Deductible/FormatCurrencyAmt/Amt)" />
				&lt;/MEDPMDEDUCTIBLE&gt;				
				&lt;WEARINGSEATBELT&gt;<xsl:call-template name="WEARINGSEATBELT" />&lt;/WEARINGSEATBELT&gt;	
				&lt;QUALIFIESTRAIBLAZERPROGRAM&gt;
					<!--Not found -->
				&lt;/QUALIFIESTRAIBLAZERPROGRAM&gt;	
				&lt;GOODSTUDENT&gt;FALSE<!--xsl:call-template name="GOODSTUDENT"></xsl:call-template-->&lt;/GOODSTUDENT&gt;
				&lt;PREMIERDRIVER&gt;TRUE&lt;/PREMIERDRIVER&gt;
				&lt;SAFEDRIVER&gt;&lt;/SAFEDRIVER&gt;
				&lt;MVR&gt;
					<!--Not found -->
				&lt;/MVR&gt;	
				&lt;SUMOFVIOLATIONPOINTS&gt;
					<!--Not found -->
				&lt;/SUMOFVIOLATIONPOINTS&gt;	
				&lt;SUMOFACCIDENTPOINTS&gt;
					<!--Not found -->
				&lt;/SUMOFACCIDENTPOINTS&gt;		 			
			&lt;/VEHICLE&gt;				
			</xsl:for-each> 				
		&lt;/VEHICLES&gt;		
		<!--End Vehicle level -->		
		<!--Start Driver level -->		
		&lt;DRIVERS&gt;
		<xsl:for-each select="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/PersDriver">		
			&lt;DRIVER ID=&quot;<xsl:value-of select="substring(./@id,11,1)" />&quot; 
			AGEOFDRIVER=&quot;<xsl:call-template name="DRIVER_AGE" />&quot; 
			VEHICLEDRIVEDAS=&quot;PRINCIPAL&quot; 
			VEHICLEASSIGNEDASOPERATOR=&quot;<xsl:value-of select="PersDriverInfo/@VehPrincipallyDrivenRef" />&quot; 
			VEHICLESDRIVEDAS=&quot;<!--PRINCIPAL--><xsl:call-template name="DRIVERASSIGNMENTTYPE" />&quot;			 
			VEHICLESASSIGNEDASOPERATOR=&quot;<xsl:call-template name="VAHICLEASSIGEDAS" /><!--xsl:value-of select="PersDriverInfo/@VehPrincipallyDrivenRef" /-->&quot; 
			BIRTHDATE=&quot;<xsl:value-of select="normalize-space(DriverInfo/PersonInfo/BirthDt)" />&quot;  
			GENDER=&quot;<xsl:choose>
				<xsl:when test="DriverInfo/PersonInfo/GenderCd = 'M'">MALE</xsl:when>
				<xsl:when test="DriverInfo/PersonInfo/GenderCd = 'F'">FEMALE</xsl:when>
				<xsl:otherwise>MALE</xsl:otherwise>
			</xsl:choose>&quot;  
			COLLEGESTUDENT=&quot;<xsl:call-template name="COLLEGESTUDENT" />&quot; 
			HAVE_CAR=&quot;0&quot; 
			&gt;								
				&lt;DRIVERFNAME&gt;<xsl:value-of select="normalize-space(GeneralPartyInfo/NameInfo/PersonName/GivenName)" />&lt;/DRIVERFNAME&gt;				
				&lt;DRIVERMNAME&gt;<xsl:value-of select="normalize-space(GeneralPartyInfo/NameInfo/PersonName/OtherGivenName)" />&lt;/DRIVERMNAME&gt;				
				&lt;DRIVERLNAME&gt;<xsl:value-of select="normalize-space(GeneralPartyInfo/NameInfo/PersonName/Surname)" />&lt;/DRIVERLNAME&gt;				
				&lt;BIRTHDATE&gt;<xsl:value-of select="normalize-space(DriverInfo/PersonInfo/BirthDt)" />&lt;/BIRTHDATE&gt;				
				&lt;AGEOFDRIVER&gt;<xsl:call-template name="DRIVER_AGE" />&lt;/AGEOFDRIVER&gt;
				&lt;GENDER&gt;<xsl:choose>
				<xsl:when test="DriverInfo/PersonInfo/GenderCd = 'M'">MALE</xsl:when>
				<xsl:when test="DriverInfo/PersonInfo/GenderCd = 'F'">FEMALE</xsl:when>
				<xsl:otherwise>MALE</xsl:otherwise>
			</xsl:choose>&lt;/GENDER&gt;		
				&lt;MARITALSTATUS&gt;<xsl:choose>
				<xsl:when test="DriverInfo/PersonInfo/MaritalStatusCd = 'M'">MARRIED</xsl:when>
				<xsl:when test="DriverInfo/PersonInfo/GenderCd = 'U'">UNMARRIED</xsl:when>
				<xsl:otherwise>UNMARRIED</xsl:otherwise>
			</xsl:choose>&lt;/MARITALSTATUS&gt;		
				&lt;DRIVERINCOME&gt;
				<xsl:variable name="INCOME" select="normalize-space(DriverInfo/PersonInfo/DriverIncome)" />
				<xsl:choose>
				<xsl:when test="$INCOME &lt; 9">LOW</xsl:when>
				<xsl:otherwise>HIGH</xsl:otherwise>
			</xsl:choose>
				&lt;/DRIVERINCOME&gt;	
				&lt;DEPENDENTS&gt;
				<xsl:variable name="NUMDEP" select="normalize-space(DriverInfo/PersonInfo/NumDependents)" />
				<xsl:choose>
				<xsl:when test="$NUMDEP &gt; 0">1MORE</xsl:when>
				<xsl:otherwise>NDEP</xsl:otherwise>
			</xsl:choose>
				&lt;/DEPENDENTS&gt;	<!--Not found -->
				&lt;DRIVERLIC&gt;<xsl:value-of select="normalize-space(PersDriver/DriverInfo/License/LicenseTypeCd)" />&lt;/DRIVERLIC&gt;				
				&lt;WAIVEWORKLOSS&gt;
				<xsl:variable name="WAVERLOSS" select="./Coverage[CoverageCd='PIP']/Option[OptionTypeCd='Bene3']/OptionCd" />
				<xsl:choose>
				<xsl:when test="$WAVERLOSS='R'">TRUE</xsl:when>
				<xsl:otherwise>FALSE</xsl:otherwise> <!-- to be checked later--></xsl:choose>
				&lt;/WAIVEWORKLOSS&gt;<!--Not found -->
				<xsl:variable name="YEARS_LICENSED">
				<xsl:choose>
					<xsl:when test="normalize-space(PersDriverInfo/YearsLicensed) != ''">
						<xsl:value-of select="PersDriverInfo/YearsLicensed" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:variable>			
				&lt;NOPREMDRIVERDISC&gt;<xsl:call-template name="NOPREMIUMDRIVERDISCOUNT">
				<xsl:with-param name="LICENSEDYEARS" select="$YEARS_LICENSED" />
			</xsl:call-template> &lt;/NOPREMDRIVERDISC&gt;
				&lt;SAFEDRIVER&gt;No&lt;/SAFEDRIVER&gt;
				&lt;COLLEGESTUDENT&gt;<xsl:call-template name="COLLEGESTUDENT" />&lt;/COLLEGESTUDENT&gt;<!--Not found -->
				&lt;GOODSTUDENT&gt;<xsl:call-template name="GOODSTUDENT"></xsl:call-template>&lt;/GOODSTUDENT&gt;<!--Not found -->
				&lt;DISTANTSTUDENT&gt;False&lt;/DISTANTSTUDENT&gt;	<!--Not found -->
						
				&lt;VEHICLEASSIGNEDASOPERATOR&gt;<xsl:value-of select="PersDriverInfo/@VehPrincipallyDrivenRef" />&lt;/VEHICLEASSIGNEDASOPERATOR&gt;
				&lt;VEHICLEDRIVEDAS&gt;PRINCIPAL&lt;/VEHICLEDRIVEDAS&gt;<!--Later -->				
				&lt;VEHICLEDRIVEDASCODE&gt;
						<!--Not found -->
				&lt;/VEHICLEDRIVEDASCODE&gt;
				&lt;POINTSAPPLIED&gt;
					<xsl:call-template name="POINTAPPLIED" />
				&lt;/POINTSAPPLIED&gt;
				&lt;MVR&gt;0&lt;/MVR&gt;
				&lt;SUMOFVIOLATIONPOINTS&gt;0&lt;/SUMOFVIOLATIONPOINTS&gt;
				&lt;SUMOFACCIDENTPOINTS&gt;0&lt;/SUMOFACCIDENTPOINTS&gt;				
				&lt;DRIVERCLASS&gt;
						<!--Not found -->
				&lt;/DRIVERCLASS&gt;
				&lt;DRIVERCLASS&gt;
						<!--Not found -->
				&lt;/DRIVERCLASS&gt;
				&lt;DRIVERCLASSCOMPONENT1&gt;
						<!--Not found -->
				&lt;/DRIVERCLASSCOMPONENT1&gt;
				&lt;DRIVERCLASSCOMPONENT2&gt;
						<!--Not found -->
				&lt;/DRIVERCLASSCOMPONENT2&gt;				
				&lt;VIOLATIONS&gt;
				&lt;/VIOLATIONS&gt;									
			&lt;/DRIVER&gt;	
			</xsl:for-each>		
		&lt;/DRIVERS&gt;	
		<!--End Driver Level -->	
	</xsl:template>
	<!--Show the state name on the basis of sate code -->
	<xsl:template name="DISPLAY_STATE_NAME">
		<xsl:choose>
			<xsl:when test="normalize-space(ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/Location/Addr/StateProvCd)='MI'">MICHIGAN</xsl:when>
			<xsl:when test="normalize-space(ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/Location/Addr/StateProvCd)='IN'">INDIANA</xsl:when>
			<xsl:otherwise>MICHIGAN</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--If AutoP - Personal and  AutoC - Commercial -->
	<xsl:template name="VEHICLE_TYPE_USE">
		<xsl:choose>
			<xsl:when test="normalize-space(//PersAutoLineBusiness/LOBCd='AutoP')">Personal</xsl:when>
			<xsl:when test="normalize-space(//PersAutoLineBusiness/LOBCd='AutoC')">Commercial</xsl:when>
			<xsl:otherwise>Personal</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="VEHICLE_AGE">
		<!-- We hv to find as (TransactionRequestDt - ModelYr)-->
		<xsl:variable name="effDateYrs" select="//PersPolicy/ContractTerm/EffectiveDt" />
		<xsl:variable name="Years" select="substring($effDateYrs,1,4)" />
		<xsl:variable name="Month" select="substring($effDateYrs,6,2)" />
		<xsl:variable name="MMonth" select="10" />
		<xsl:variable name="MYear" select=".//ModelYear" />
		<xsl:variable name="MYearAge">
			<xsl:value-of select="$Years - $MYear" />
		</xsl:variable>
		<xsl:variable name="MMonthAge">
			<xsl:value-of select="$Month - $MMonth" />
		</xsl:variable>
		<!-- Age to calculate -->
		<xsl:variable name="VAge">
			<!--xsl:choose>
				<xsl:when test="MMonthAge &lt; 1"-->
			<xsl:value-of select="$MYearAge" />
			<!--/xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$MYearAge" />
				</xsl:otherwise>
			</xsl:choose-->
		</xsl:variable>
		<xsl:variable name="VAR1">
			<xsl:choose>
				<xsl:when test="$Month &lt; $MMonth">
					<xsl:value-of select="$VAge + 1 " />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$VAge + 2 " />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!--xsl:variable name="VAR1"><xsl:value-of select="$VAge + 1 " /></xsl:variable-->
		<xsl:choose>
			<xsl:when test="$VAR1 &lt;= 0">1</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$VAR1" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DRIVER_AGE">
		<!-- We hv to find as (D_O_B - TransactionRequestDt)-->
		<xsl:variable name="Driv_DOB" select="substring-before(.//DriverInfo/PersonInfo/BirthDt,'-')" />
		<xsl:variable name="effDateYrs" select="substring-before(//PersPolicy/ContractTerm/EffectiveDt,'-')" />
		<xsl:value-of select="$effDateYrs - $Driv_DOB" />
	</xsl:template>
	<xsl:template name="QQ_EFFECTIVE_DATE">
		<!--Convert given date into mm//dd/yyyy format -->
		<!--xsl:variable name="Date" select="substring-before(ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/TransactionRequestDt,'T')" /-->
		<xsl:variable name="Date" select="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/ContractTerm/EffectiveDt" />
		<xsl:variable name="Year" select="substring($Date,1,4)" />
		<xsl:variable name="Month" select="substring($Date,6,2)" />
		<xsl:variable name="Day" select="substring($Date,9,2)" />
		<xsl:variable name="Formatted_Date" select="concat($Month,'/',$Day)" />
		<xsl:value-of select="concat($Formatted_Date,'/',$Year)" />
	</xsl:template>
	<xsl:template name="GET_TERM">
		<!-- Get term in months (if value is provided in yrs then convert it into months first -->
		<xsl:choose>
			<xsl:when test="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/ContractTerm/DurationPeriod/UnitMeasurementCd='MON'">
				<xsl:value-of select="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/ContractTerm/DurationPeriod/NumUnits" />
			</xsl:when>
			<xsl:when test="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/ContractTerm/DurationPeriod/UnitMeasurementCd='Yr'">
				<xsl:variable name="Year" select="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/ContractTerm/DurationPeriod/NumUnits" />
				<xsl:value-of select="($Year * 12)" />
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--Defautl value should be zero -->
	<xsl:template name="YEARS_CONTS_INSURED">
		<xsl:choose>
			<xsl:when test="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/OtherOrPriorPolicy/ContractTerm/ContinuousInd !=''">
				<xsl:variable name="MonthsContYear" select="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/OtherOrPriorPolicy/ContractTerm/ContinuousInd" />
				<xsl:value-of select="round($MonthsContYear div 12)" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Defautl value should be zero -->
	<xsl:template name="YEARS_CONTS_INSURED_WOLV">
		<xsl:choose>
			<xsl:when test="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/OtherOrPriorPolicy/ContractTerm/YearsContinuousIndWolverine !=''">
				<xsl:value-of select="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/OtherOrPriorPolicy/ContractTerm/YearsContinuousIndWolverine" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Defautl value should be zero -->
	<xsl:template name="LOSSES_CHARGEABLE_NONCHARGEABLE">
		<!--xsl:choose>
			<xsl:when test="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/PersApplicationInfo/AnyLossesAccidentsConvictionsInd !=''">
				<xsl:value-of select="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/PersApplicationInfo/AnyLossesAccidentsConvictionsInd " />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose-->
		<!-- New SurCharge limit of 750 has been implemented for safe and Premier driver in 07/2009 -->
		<xsl:variable name="VareffDate" select="//PersPolicy/ContractTerm/EffectiveDt" />
		<xsl:variable name="VareffYear" select="substring($VareffDate,1,4)" />
		<xsl:variable name="VareffMonth" select="substring($VareffDate,6,2)" />
		<xsl:variable name="VareffDay" select="substring($VareffDate,9,2)" />
		<xsl:variable name="VarthreYear" select="$VareffYear - 3" />
		<xsl:variable name="VAR6">
			<xsl:choose>
				<xsl:when test="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/AccidentViolation !=''">
					<xsl:choose>
						<xsl:when test="$VareffYear &lt;= $VarYearNineeffYear">
							<xsl:choose>
								<xsl:when test="$VareffYear = $VarYearNineeffYear and $VareffMonth &lt; $VarMonthNineeffMonth">
									<xsl:variable name="VAR1">
										<xsl:for-each select="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/AccidentViolation">
											<xsl:variable name="VarAcciDate" select="./AccidentViolationDt" />
											<xsl:variable name="VarAcciYear" select="substring($VarAcciDate,1,4)" />
											<xsl:variable name="VarAcciMonth" select="substring($VarAcciDate,6,2)" />
											<xsl:variable name="VarAcciDay" select="substring($VarAcciDate,9,2)" />
											<xsl:choose>
												<xsl:when test="./DamageTotalAmt !='' and ./DamageTotalAmt &gt; '75' and $VarAcciYear &gt;= $VarthreYear">
													<xsl:choose>
														<xsl:when test="$VarAcciYear = $VarthreYear and $VarAcciMonth &lt; $VareffMonth ">
															<xsl:value-of select="user:GetlossCount(0)" />
														</xsl:when>
														<xsl:otherwise>
															<xsl:value-of select="user:GetlossCount(1)" />
														</xsl:otherwise>
													</xsl:choose>
												</xsl:when>
												<xsl:otherwise>0</xsl:otherwise>
											</xsl:choose>
										</xsl:for-each>
									</xsl:variable>
									<xsl:value-of select="user:GetlossCount(0)" />
								</xsl:when>
								<xsl:when test="$VareffYear &lt; $VarYearNineeffYear">
									<xsl:variable name="VAR2">
										<xsl:for-each select="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/AccidentViolation">
											<xsl:variable name="VarAcciDate" select="./AccidentViolationDt" />
											<xsl:variable name="VarAcciYear" select="substring($VarAcciDate,1,4)" />
											<xsl:variable name="VarAcciMonth" select="substring($VarAcciDate,6,2)" />
											<xsl:variable name="VarAcciDay" select="substring($VarAcciDate,9,2)" />
											<xsl:choose>
												<xsl:when test="./DamageTotalAmt !='' and ./DamageTotalAmt &gt; '75' and $VarAcciYear &gt;= $VarthreYear">
													<xsl:choose>
														<xsl:when test="$VarAcciYear = $VarthreYear and $VarAcciMonth &lt; $VareffMonth ">
															<xsl:value-of select="user:GetlossCount(0)" />
														</xsl:when>
														<xsl:otherwise>
															<xsl:value-of select="user:GetlossCount(1)" />
														</xsl:otherwise>
													</xsl:choose>
												</xsl:when>
												<xsl:otherwise>0</xsl:otherwise>
											</xsl:choose>
										</xsl:for-each>
									</xsl:variable>
									<xsl:value-of select="user:GetlossCount(0)" />
								</xsl:when>
								<xsl:otherwise>
									<xsl:variable name="VAR3">
										<xsl:for-each select="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/AccidentViolation">
											<xsl:variable name="VarAcciDate" select="./AccidentViolationDt" />
											<xsl:variable name="VarAcciYear" select="substring($VarAcciDate,1,4)" />
											<xsl:variable name="VarAcciMonth" select="substring($VarAcciDate,6,2)" />
											<xsl:variable name="VarAcciDay" select="substring($VarAcciDate,9,2)" />
											<xsl:choose>
												<xsl:when test="./DamageTotalAmt !='' and ./DamageTotalAmt &gt; '750' and $VarAcciYear &gt;= $VarthreYear">
													<xsl:choose>
														<xsl:when test="$VarAcciYear = $VarthreYear and $VarAcciMonth &lt; $VareffMonth ">
															<xsl:value-of select="user:GetlossCount(0)" />
														</xsl:when>
														<xsl:otherwise>
															<xsl:value-of select="user:GetlossCount(1)" />
														</xsl:otherwise>
													</xsl:choose>
												</xsl:when>
												<xsl:otherwise>0</xsl:otherwise>
											</xsl:choose>
										</xsl:for-each>
									</xsl:variable>
									<xsl:value-of select="user:GetlossCount(0)" />
								</xsl:otherwise>
							</xsl:choose>
							<!--xsl:for-each select="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/AccidentViolation">
							<xsl:variable name="VarAcciDate" select="./AccidentViolationDt" />
							<xsl:variable name="VarAcciYear" select="substring($VarAcciDate,1,4)" />
							<xsl:variable name="VarAcciMonth" select="substring($VarAcciDate,6,2)" />
							<xsl:variable name="VarAcciDay" select="substring($VarAcciDate,9,2)" />
							<xsl:choose>
								<xsl:when test="./DamageTotalAmt !='' and ./DamageTotalAmt &gt; '75' and $VarAcciYear &gt;= $VarthreYear and $VarAcciMonth &gt;= $VareffMonth and $VarAcciDay &gt;= $VareffDay">
									<xsl:variable name="VAR_SUMOFLOSSCOUNT" select="user:GetlossCount(1)" />
								</xsl:when>
								<xsl:otherwise>0</xsl:otherwise>
							</xsl:choose>
						</xsl:for-each-->
						</xsl:when>
						<xsl:when test="$VareffYear &gt;= $VarYearNineeffYear">
							<xsl:variable name="VAR4">
								<xsl:for-each select="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/AccidentViolation">
									<xsl:variable name="VarAcciDate" select="./AccidentViolationDt" />
									<xsl:variable name="VarAcciYear" select="substring($VarAcciDate,1,4)" />
									<xsl:variable name="VarAcciMonth" select="substring($VarAcciDate,6,2)" />
									<xsl:variable name="VarAcciDay" select="substring($VarAcciDate,9,2)" />
									<xsl:choose>
										<xsl:when test="./DamageTotalAmt !='' and ./DamageTotalAmt &gt; '750' and $VarAcciYear &gt;= $VarthreYear">
											<xsl:choose>
												<xsl:when test="$VarAcciYear = $VarthreYear and $VarAcciMonth &lt; $VareffMonth ">
													<xsl:value-of select="user:GetlossCount(0)" />
												</xsl:when>
												<xsl:otherwise>
													<xsl:value-of select="user:GetlossCount(1)" />
												</xsl:otherwise>
											</xsl:choose>
										</xsl:when>
										<xsl:otherwise>0</xsl:otherwise>
									</xsl:choose>
								</xsl:for-each>
							</xsl:variable>
							<xsl:value-of select="user:GetlossCount(0)" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:variable name="VAR5">
								<xsl:for-each select="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/AccidentViolation">
									<xsl:variable name="VarAcciDate" select="./AccidentViolationDt" />
									<xsl:variable name="VarAcciYear" select="substring($VarAcciDate,1,4)" />
									<xsl:variable name="VarAcciMonth" select="substring($VarAcciDate,6,2)" />
									<xsl:variable name="VarAcciDay" select="substring($VarAcciDate,9,2)" />
									<xsl:choose>
										<xsl:when test="./DamageTotalAmt !='' and ./DamageTotalAmt &gt; '750' and $VarAcciYear &gt;= $VarthreYear ">
											<xsl:choose>
												<xsl:when test="$VarAcciYear = $VarthreYear and $VarAcciMonth &lt; $VareffMonth ">
													<xsl:value-of select="user:GetlossCount(0)" />
												</xsl:when>
												<xsl:otherwise>
													<xsl:value-of select="user:GetlossCount(1)" />
												</xsl:otherwise>
											</xsl:choose>
										</xsl:when>
										<xsl:otherwise>0</xsl:otherwise>
									</xsl:choose>
								</xsl:for-each>
							</xsl:variable>
							<xsl:value-of select="user:GetlossCount(0)" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!--xsl:value-of select="user:GetlossCount(0)" /-->
		<xsl:value-of select="$VAR6" />
	</xsl:template>
	<!--Template returns LOB_ID based on code provided in ACORD xml-->
	<xsl:template name="LOBID">
		<xsl:choose>
			<xsl:when test="normalize-space(ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/LOBCd)='AutoP'">2</xsl:when>
			<xsl:when test="normalize-space(ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersAutoLineBusiness/LOBCd)='AutoC'">2</xsl:when>
			<!--We need to chk code for rest lobs -->
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--This template return the state ID based on the state code provided in ACORD xml -->
	<xsl:template name="STATEID">
		<xsl:choose>
			<xsl:when test="normalize-space(ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/Location/Addr/StateProvCd)='MI'">22</xsl:when>
			<xsl:when test="normalize-space(ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/Location/Addr/StateProvCd)='IN'">14</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--This template return the vehicle desc based on the VehUseCd code provided in ACORD xml -->
	<xsl:template name="VEHICLEUSEDESC">
		<xsl:variable name="VEHICLEUSEDESC" select="normalize-space(./VehUseCd)" />
		<xsl:variable name="VEHICLEUSEDESCACORD" select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEUSE']/NODE[@ID='VEHICLE_USE_DESC']/ATTRIBUTES[@ACORDUSEDESCCODE=$VEHICLEUSEDESC]/@QQUSEDESCCODE" />
		<xsl:choose>
			<xsl:when test="$VEHICLEUSEDESCACORD !=''">
				<xsl:value-of select="$VEHICLEUSEDESCACORD" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEUSE']/NODE[@ID='VEHICLE_USE_DESC']/ATTRIBUTES/@DEFAULT" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--If we hv Carpool then return as Y else N -->
	<xsl:template name="CARPOOL">
		<xsl:choose>
			<xsl:when test="normalize-space(./VehUseCd)='CP'">Y</xsl:when>
			<xsl:otherwise>N</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Return the deductible amount against 'COMP/OTC' as Covg code-->
	<xsl:template name="COMPREHENSIVEDEDUCTIBLE">
		<xsl:choose>
			<xsl:when test="normalize-space(./Coverage[CoverageCd='COMP']/Deductible/FormatCurrencyAmt/Amt)!=''">
				<xsl:value-of select="./Coverage[CoverageCd='COMP']/Deductible/FormatCurrencyAmt/Amt" />
			</xsl:when>
			<xsl:when test="normalize-space(./Coverage[CoverageCd='OTC']/Deductible/FormatCurrencyAmt/Amt)!=''">
				<xsl:value-of select="./Coverage[CoverageCd='COMP']/Deductible/FormatCurrencyAmt/Amt" />
			</xsl:when>
			<xsl:otherwise>N</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Return the amt against UM code -->
	<xsl:template name="PDDEDUCTIBLE">
		<xsl:choose>
			<xsl:when test="normalize-space(./Coverage[CoverageCd='UM']/Deductible/FormatCurrencyAmt/Amt)!=''">
				<xsl:value-of select="./Coverage[CoverageCd='UM']/Deductible/FormatCurrencyAmt/Amt" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Convert the given format into hexadecimal format for GUID 
	<xsl:template name="RQUID">
		<xsl:variable name="TempRqUID" select="ACORD/InsuranceSvcRq/RqUID" />
		<xsl:choose>
			<xsl:when test="substring($TempRqUID,9,1)='-'">
				<xsl:value-of select="$TempRqUID" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="TempRqUID1" select="substring($TempRqUID,0,9)" />
				<xsl:variable name="TempRqUID2" select="substring($TempRqUID,9,4)" />
				<xsl:variable name="TempRqUID3" select="substring($TempRqUID,13,4)" />
				<xsl:variable name="TempRqUID4" select="substring($TempRqUID,17,4)" />
				<xsl:variable name="TempRqUID5" select="substring($TempRqUID,21,12)" />
				<xsl:value-of select="concat(concat(concat(concat($TempRqUID1,'-',$TempRqUID2),'-',$TempRqUID3),'-',$TempRqUID4),'-',$TempRqUID5)" />
			</xsl:otherwise>
		</xsl:choose>-->
	<!--xsl:value-of select="$TempRqUID"/
	</xsl:template>-->
	<!--This is temperory soluation,need to be fetch from common xml.-->
	<xsl:template name="VEHICLEUSE_RATING_CODE">
		<xsl:param name="VEH_USE"></xsl:param>
		<xsl:variable name="VEHICLEUSEACORD" select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEUSE']/NODE[@ID='VEHICLE_USE']/ATTRIBUTES[@ACORDUSECODE=$VEH_USE]/@QQUSECODE" />
		<xsl:choose>
			<xsl:when test="$VEHICLEUSEACORD !=''">
				<xsl:value-of select="$VEHICLEUSEACORD" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEUSE']/NODE[@ID='VEHICLE_USE']/ATTRIBUTES/@DEFAULT" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- THIS TEMPLATE USED FOR PIP COVERAGE -->
	<xsl:template name="PIPCOVERAGE">
		<xsl:choose>
			<xsl:when test="normalize-space(./Coverage[CoverageCd='PIP']/Deductible/FormatCurrencyAmt/Amt)!=''">
				<xsl:variable name="COVERAGE_TYPE">
					<xsl:value-of select="normalize-space(./Coverage[CoverageCd='PIP']/Option[OptionTypeCd='Opt1']/OptionCd)" />
				</xsl:variable>
				<xsl:variable name="VEHICLEPIPCODE">
					<xsl:choose>
						<xsl:when test="$COVERAGE_TYPE=''">
							<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLECOVERAGES']/NODE[@ID='PIP_COVERAGE']/ATTRIBUTES/@DEFAULT" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLECOVERAGES']/NODE[@ID='PIP_COVERAGE']/ATTRIBUTES[@ACORDPIPCODE=$COVERAGE_TYPE]/@QQPIPCODE" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:value-of select="$VEHICLEPIPCODE" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLECOVERAGES']/NODE[@ID='PIP_COVERAGE']/ATTRIBUTES/@DEFAULT" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- template for vehicle use mapping -->
	<xsl:template name="VEHICLEUSE_DEFAULT">
		<xsl:variable name="VEHICLETYPE">
			<xsl:value-of select="./VehBodyTypeCd" />
		</xsl:variable>
		<xsl:variable name="VEHICLETYPEACCORD" select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEBODYTYPE']/NODE[@ID='QQACORDVEHICLEBODYTYPE']/ATTRIBUTES[@ACCORDBODYTPE=$VEHICLETYPE]/@QQBODYTYPE" />
		<!--PP,cv,mh,tr,sco,lh1,lh,tr,tri -->
		<xsl:choose>
			<xsl:when test="$VEHICLETYPEACCORD !=''">
				<xsl:value-of select="$VEHICLETYPEACCORD" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEBODYTYPE']/NODE[@ID='QQACORDVEHICLEBODYTYPE']/ATTRIBUTES/@DEFAULT" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="TYPE_DISPLAY">
		<xsl:choose>
			<xsl:when test="normalize-space(//Location/Addr/StateProvCd)='IN'">
				<xsl:choose>
					<xsl:when test="Coverage[CoverageCd='PUMSP' or CoverageCd='UM']/Limit[LimitAppliesToCd='PerPerson']/FormatCurrencyAmt/Amt >0">BI ONLY</xsl:when>
					<xsl:when test="Coverage[CoverageCd='PUMSP' or CoverageCd='UM']/Limit[LimitAppliesToCd='PerPerson']/FormatCurrencyAmt/Amt >0 and Coverage[CoverageCd='PUNPD']/Limit[LimitAppliesToCd='PerPerson']/FormatCurrencyAmt/Amt >0">BIPD</xsl:when>
					<xsl:otherwise>NO COVERAGE</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="normalize-space(//Location/Addr/StateProvCd)='MI'">UNINSURED MOTORISTS</xsl:when>
			<xsl:otherwise>NO COVERAGE</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- template for collision type deductible -->
	<xsl:template name="COLLISIONDEDUCTIBLE">
		<xsl:choose>
			<xsl:when test="./Coverage[CoverageCd='COLL']/Option/OptionTypeCd='Opt1'">
				<xsl:choose>
					<xsl:when test="./Coverage[CoverageCd='COLL']/Option/OptionCd='R'">
						<xsl:variable name="ACCOR_COLL">
							<xsl:value-of select="./Coverage[CoverageCd='COLL']/Deductible/FormatCurrencyAmt/Amt" />
						</xsl:variable>
						<xsl:variable name="QQ_COLL">
							<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLECOVERAGES']/NODE[@ID='COLL_COVERAGE_TYPE']/ATTRIBUTES[@ACCORD_COLL_VAL=$ACCOR_COLL]/@QQ_COLL_VAL" />
						</xsl:variable>
						<xsl:value-of select="$QQ_COLL" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="./Coverage[CoverageCd='COLL']/Deductible/FormatCurrencyAmt/Amt" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>NO COVERAGE</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- template for coverge collision type -->
	<xsl:template name="COVERAGECOLLISIONTYPE">
		<xsl:variable name="Coveragetext">
			<xsl:value-of select="normalize-space(./Coverage[CoverageCd='COLL']/Option[OptionTypeCd='Opt1']/OptionCd)" />
		</xsl:variable>
		<xsl:variable name="VEHICLECOLLCOVTYPE" select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLECOVERAGES']/NODE[@ID='COLLISION_TYPE']/ATTRIBUTES[@ACORDCOLLCODE=$Coveragetext]/@QQCOLLCODE" />
		<xsl:choose>
			<xsl:when test="$VEHICLECOLLCOVTYPE !=''">
				<xsl:value-of select="$VEHICLECOLLCOVTYPE" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLECOVERAGES']/NODE[@ID='COVERAGE_COLLISION_TYPE']/ATTRIBUTES/@DEFAULT" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- RENTAL REMBUSEMENT COVERAGE -->
	<xsl:template name="RENTALREMBURSEMENT">
		<xsl:variable name="LOWERLIMIT">
			<xsl:value-of select="./Coverage[CoverageCd='RREIM']/Limit[LimitAppliesToCd='PerDay']/FormatCurrencyAmt/Amt" />
		</xsl:variable>
		<xsl:variable name="UPPERLIMIT">
			<xsl:value-of select="./Coverage[CoverageCd='RREIM']/Limit[LimitAppliesToCd='MaxAmount']/FormatCurrencyAmt/Amt" />
		</xsl:variable>
		<xsl:variable name="VEHICLELOWERLIMIT" select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RENTAL_REIMBUSEMENT_LIMIT']/NODE[@ID='BRICS_RENTAL_LIMIT']/ATTRIBUTES[@ACORD_RENTAL_LOWERLIMIT=$LOWERLIMIT]/@QQ_RENTAL_LOWERLIMIT" />
		<xsl:variable name="VEHICLEUPPERLIMIT" select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RENTAL_REIMBUSEMENT_LIMIT']/NODE[@ID='BRICS_RENTAL_LIMIT']/ATTRIBUTES[@ACORD_RENTAL_UPPERLIMIT=$UPPERLIMIT]/@QQ_RENTAL_UPPERLIMIT" />
		<xsl:choose>
			<xsl:when test="./Coverage[CoverageCd='RREIM']/Limit[LimitAppliesToCd='PerDay']/FormatCurrencyAmt > 0">
				<xsl:value-of select="$VEHICLELOWERLIMIT" />/<xsl:value-of select="$VEHICLEUPPERLIMIT" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="RENTALREMBURSEMENTMIN">
		<xsl:variable name="LOWERLIMIT">
			<xsl:value-of select="./Coverage[CoverageCd='RREIM']/Limit[LimitAppliesToCd='PerDay']/FormatCurrencyAmt/Amt" />
		</xsl:variable>
		<xsl:variable name="VEHICLELOWERLIMIT" select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RENTAL_REIMBUSEMENT_LIMIT']/NODE[@ID='BRICS_RENTAL_LIMIT']/ATTRIBUTES[@ACORD_RENTAL_LOWERLIMIT=$LOWERLIMIT]/@QQ_RENTAL_LOWERLIMIT" />
		<xsl:choose>
			<xsl:when test="./Coverage[CoverageCd='RREIM']/Limit[LimitAppliesToCd='PerDay']/FormatCurrencyAmt/Amt>0">
				<xsl:value-of select="$VEHICLELOWERLIMIT" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="RENTALREMBURSEMENTMAX">
		<xsl:variable name="UPPERLIMIT">
			<xsl:value-of select="./Coverage[CoverageCd='RREIM']/Limit[LimitAppliesToCd='MaxAmount']/FormatCurrencyAmt/Amt" />
		</xsl:variable>
		<xsl:variable name="VEHICLEUPPERLIMIT" select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='RENTAL_REIMBUSEMENT_LIMIT']/NODE[@ID='BRICS_RENTAL_LIMIT']/ATTRIBUTES[@ACORD_RENTAL_UPPERLIMIT=$UPPERLIMIT]/@QQ_RENTAL_UPPERLIMIT" />
		<xsl:choose>
			<xsl:when test="./Coverage[CoverageCd='RREIM']/Limit[LimitAppliesToCd='MaxAmount']/FormatCurrencyAmt/Amt>0">
				<xsl:value-of select="$VEHICLEUPPERLIMIT" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Road Service -->
	<xsl:template name="ROADSERVICE">
		<xsl:variable name="VareffDate" select="//PersPolicy/ContractTerm/EffectiveDt" />
		<xsl:variable name="VareffYear" select="substring($VareffDate,1,4)" />
		<xsl:variable name="VareffMonth" select="substring($VareffDate,6,2)" />
		<xsl:variable name="COVERAGE_ACCORD">
			<xsl:choose>
				<xsl:when test="./Coverage[CoverageCd='TL']/Limit[LimitAppliesToCd='Coverage']/FormatCurrencyAmt/Amt > 0">
					<xsl:value-of select="./Coverage[CoverageCd='TL']/Limit[LimitAppliesToCd='Coverage']/FormatCurrencyAmt/Amt" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VEHICLECOVERAGEROADMAX">
			<xsl:choose>
				<xsl:when test="$VareffYear &gt;= $VarYearNineeffYear">
					<xsl:choose>
						<xsl:when test="$VareffMonth &gt;= $VarMonthNineeffMonth">
							<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ROAD_SERVICE_LIMIT']/NODE[@ID='BRICS_ROAD_LIMIT_2009']/@MAX" />
						</xsl:when>
						<xsl:when test="$VareffYear = $VarYearNineeffYear and $VareffMonth &lt; $VarMonthNineeffMonth">
							<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ROAD_SERVICE_LIMIT']/NODE[@ID='BRICS_ROAD_LIMIT_2008']/@MAX" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ROAD_SERVICE_LIMIT']/NODE[@ID='BRICS_ROAD_LIMIT_2009']/@MAX" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ROAD_SERVICE_LIMIT']/NODE[@ID='BRICS_ROAD_LIMIT_2008']/@MAX" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VEHICLECOVERAGEROAD">
			<xsl:choose>
				<xsl:when test="$VareffYear &gt;= $VarYearNineeffYear">
					<xsl:choose>
						<xsl:when test="$VareffMonth &gt;= $VarMonthNineeffMonth">
							<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ROAD_SERVICE_LIMIT']/NODE[@ID='BRICS_ROAD_LIMIT_2009']/ATTRIBUTES[@ACORD_ROAD_LIMIT=$COVERAGE_ACCORD]/@QQ_ROAD_LIMIT" />
						</xsl:when>
						<xsl:when test="$VareffYear = $VarYearNineeffYear and $VareffMonth &lt; $VarMonthNineeffMonth">
							<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ROAD_SERVICE_LIMIT']/NODE[@ID='BRICS_ROAD_LIMIT_2008']/ATTRIBUTES[@ACORD_ROAD_LIMIT=$COVERAGE_ACCORD]/@QQ_ROAD_LIMIT" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ROAD_SERVICE_LIMIT']/NODE[@ID='BRICS_ROAD_LIMIT_2009']/ATTRIBUTES[@ACORD_ROAD_LIMIT=$COVERAGE_ACCORD]/@QQ_ROAD_LIMIT" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ROAD_SERVICE_LIMIT']/NODE[@ID='BRICS_ROAD_LIMIT_2008']/ATTRIBUTES[@ACORD_ROAD_LIMIT=$COVERAGE_ACCORD]/@QQ_ROAD_LIMIT" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$COVERAGE_ACCORD > 0">
				<xsl:choose>
					<xsl:when test="$COVERAGE_ACCORD >= $VEHICLECOVERAGEROADMAX">
						<xsl:value-of select="$VEHICLECOVERAGEROADMAX" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$VEHICLECOVERAGEROAD" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
		<!--xsl:value-of select="$VEHICLECOVERAGEROAD" /-->
	</xsl:template>
	<!--PD-->
	<xsl:template name="PDCOVERAGE">
		<xsl:variable name="COVERAGE_ACORD">
			<xsl:value-of select="normalize-space(Coverage[CoverageCd='PD']/Limit[LimitAppliesToCd='Coverage']/FormatCurrencyAmt/Amt)" />
		</xsl:variable>
		<xsl:variable name="VEHCICLEPDCOV">
			<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLECOVERAGES']/NODE[@ID='PD_COVERAGE_TYPE']/ATTRIBUTES[@ACORD_PD_VAL=$COVERAGE_ACORD]/@QQ_PD_VAL" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$COVERAGE_ACORD > 0">
				<xsl:value-of select="$VEHCICLEPDCOV"></xsl:value-of>
			</xsl:when>
			<xsl:otherwise>No Coverage</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--CSL-->
	<xsl:template name="CSLCOVERAGE">
		<xsl:variable name="COVERAGE_ACORD">
			<xsl:value-of select="normalize-space(Coverage[CoverageCd='CSL']/Limit[LimitAppliesToCd='CSL']/FormatCurrencyAmt/Amt )" />
		</xsl:variable>
		<xsl:variable name="VEHCICLECSLCOV">
			<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLECOVERAGES']/NODE[@ID='CSL_COVERAGE_TYPE']/ATTRIBUTES[@ACORD_CSL_VAL=$COVERAGE_ACORD]/@QQ_CSL_VAL" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$COVERAGE_ACORD > 0">
				<xsl:value-of select="$VEHCICLECSLCOV"></xsl:value-of>
			</xsl:when>
			<xsl:otherwise>No Coverage</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="UMCSLCOVERAGE">
		<xsl:variable name="COVERAGE_ACORD">
			<xsl:value-of select="normalize-space(Coverage[CoverageCd='UMCSL']/Limit[LimitAppliesToCd='CSL']/FormatCurrencyAmt/Amt )" />
		</xsl:variable>
		<xsl:variable name="VEHCICLECSLCOV">
			<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLECOVERAGES']/NODE[@ID='UMCSL_COVERAGE_TYPE']/ATTRIBUTES[@ACORD_UMCSL_VAL=$COVERAGE_ACORD]/@QQ_UMCSL_VAL" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$COVERAGE_ACORD > 0">
				<xsl:value-of select="$VEHCICLECSLCOV"></xsl:value-of>
			</xsl:when>
			<xsl:otherwise>No Coverage</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--BI-->
	<xsl:template name="BI_COVERAGE">
		<xsl:choose>
			<xsl:when test="Coverage[CoverageCd='BI']/Limit[LimitAppliesToCd='PerPerson']/FormatCurrencyAmt/Amt >0 and Coverage[CoverageCd='BI']/Limit[LimitAppliesToCd='PerAcc']/FormatCurrencyAmt/Amt >0">
			<xsl:call-template name="BILIMIT1_COVERAGE"></xsl:call-template>/<xsl:call-template name="BILIMIT2_COVERAGE"></xsl:call-template>
			</xsl:when>
			<xsl:otherwise>No Coverage</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--BILIMIT1 -->
	<xsl:template name="BILIMIT1_COVERAGE">
		<xsl:variable name="BI_LIMIT1">
			<xsl:value-of select="round(Coverage[CoverageCd='BI']/Limit[LimitAppliesToCd='PerPerson']/FormatCurrencyAmt/Amt div 1000)" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$BI_LIMIT1 > 0">
				<xsl:variable name="BI_LIMIT2">
					<xsl:value-of select="round(Coverage[CoverageCd='BI']/Limit[LimitAppliesToCd='PerAcc']/FormatCurrencyAmt/Amt div 1000)" />
				</xsl:variable>
				<xsl:variable name="VEHCICLEBICOV">
					<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLECOVERAGES']/NODE[@ID='BI_COVERAGE_TYPE']/ATTRIBUTES[@ACORD_BI_MIN=$BI_LIMIT1 and @ACORD_BI_MAX=$BI_LIMIT2]/@QQ_BI_MIN" />
				</xsl:variable>
				<xsl:value-of select="$VEHCICLEBICOV"></xsl:value-of>
			</xsl:when>
			<xsl:otherwise>No Coverage</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--BILIMIT2 -->
	<xsl:template name="BILIMIT2_COVERAGE">
		<xsl:variable name="BI_LIMIT2">
			<xsl:value-of select="round(Coverage[CoverageCd='BI']/Limit[LimitAppliesToCd='PerAcc']/FormatCurrencyAmt/Amt div 1000)" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$BI_LIMIT2 > 0">
				<xsl:variable name="BI_LIMIT1">
					<xsl:value-of select="round(Coverage[CoverageCd='BI']/Limit[LimitAppliesToCd='PerPerson']/FormatCurrencyAmt/Amt div 1000)" />
				</xsl:variable>
				<xsl:variable name="VEHCICLEBICOV">
					<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLECOVERAGES']/NODE[@ID='BI_COVERAGE_TYPE']/ATTRIBUTES[@ACORD_BI_MIN=$BI_LIMIT1 and @ACORD_BI_MAX=$BI_LIMIT2]/@QQ_BI_MAX" />
				</xsl:variable>
				<xsl:value-of select="$VEHCICLEBICOV"></xsl:value-of>
			</xsl:when>
			<xsl:otherwise>No Coverage</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--UMSPLIT-->
	<xsl:template name="UMSPLIT_COVERAGE">
		<xsl:choose>
			<xsl:when test="Coverage[CoverageCd='PUMSP' or CoverageCd='UM']/Limit[LimitAppliesToCd='PerPerson']/FormatCurrencyAmt/Amt >0">
			<xsl:call-template name="UMSPLITLIMIT1_COVERAGE"></xsl:call-template>/<xsl:call-template name="UMSPLITLIMIT2_COVERAGE"></xsl:call-template>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--UMSPLIT1-->
	<xsl:template name="UMSPLITLIMIT1_COVERAGE">
		<xsl:variable name="UMSPLIT_LIMIT1">
			<xsl:value-of select="round(Coverage[CoverageCd='PUMSP' or CoverageCd='UM']/Limit[LimitAppliesToCd='PerPerson']/FormatCurrencyAmt/Amt div 1000)" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$UMSPLIT_LIMIT1 > 0">
				<xsl:variable name="UMSPLIT_LIMIT2">
					<xsl:value-of select="round(Coverage[CoverageCd='PUMSP' or CoverageCd='UM']/Limit[LimitAppliesToCd='PerAcc']/FormatCurrencyAmt/Amt div 1000)" />
				</xsl:variable>
				<xsl:variable name="VEHICLE_UM_SPLIT1">
					<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLECOVERAGES']/NODE[@ID='UM_COVERAGE_TYPE']/ATTRIBUTES[@ACORD_UM_MIN=$UMSPLIT_LIMIT1 and @ACORD_UM_MAX=$UMSPLIT_LIMIT2]/@QQ_UM_MIN" />
				</xsl:variable>
				<xsl:value-of select="$VEHICLE_UM_SPLIT1"></xsl:value-of>
			</xsl:when>
			<xsl:otherwise>No Coverage</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--UMSPLIT2-->
	<xsl:template name="UMSPLITLIMIT2_COVERAGE">
		<xsl:variable name="UMSPLIT_LIMIT2">
			<xsl:value-of select="round(Coverage[CoverageCd='PUMSP' or CoverageCd='UM']/Limit[LimitAppliesToCd='PerAcc']/FormatCurrencyAmt/Amt div 1000)" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$UMSPLIT_LIMIT2 > 0">
				<xsl:variable name="UMSPLIT_LIMIT1">
					<xsl:value-of select="round(Coverage[CoverageCd='PUMSP' or CoverageCd='UM']/Limit[LimitAppliesToCd='PerPerson']/FormatCurrencyAmt/Amt div 1000)" />
				</xsl:variable>
				<xsl:variable name="VEHICLE_UM_SPLIT2">
					<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLECOVERAGES']/NODE[@ID='UM_COVERAGE_TYPE']/ATTRIBUTES[@ACORD_UM_MIN=$UMSPLIT_LIMIT1 and @ACORD_UM_MAX=$UMSPLIT_LIMIT2]/@QQ_UM_MAX" />
				</xsl:variable>
				<xsl:value-of select="$VEHICLE_UM_SPLIT2"></xsl:value-of>
			</xsl:when>
			<xsl:otherwise>No Coverage</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--ANTI BREAKS DISCOUNTS-->
	<xsl:template name="ANTIBREAKDISCOUNTS">
		<xsl:variable name="ANTI_BREAK">
			<xsl:value-of select="normalize-space(./AntiLockBrakeCd)"></xsl:value-of>
		</xsl:variable>
		<xsl:variable name="ANTI_BREAK_QQ">
			<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLE_DISCOUNTS']/NODE[@ID='ANTILOCK_BRAKES']/ATTRIBUTES[@ACORD_VAL=$ANTI_BREAK]/@QQ_VAL" />
		</xsl:variable>
		<xsl:value-of select="$ANTI_BREAK_QQ"></xsl:value-of>
	</xsl:template>
	<!--MULTIDISCOUNT-->
	<xsl:template name="MULTIDISCOUNT">
		<xsl:variable name="VAR_NUMOFVEHICLES" select="count(//PersAutoLineBusiness/PersVeh[VehBodyTypeCd!='TR' and VehBodyTypeCd!='CT' and VehBodyTypeCd!='UT'])" />
		<!--xsl:variable name="VAR_NUMOFTRAILERS" select="//PersAutoLineBusiness/PersVeh[VehBodyTypeCd='TR']"/-->
		<xsl:variable name="MULTIDISCOUNT_ACORD">
			<xsl:choose>
				<xsl:when test="$VAR_NUMOFVEHICLES &gt; 1">1</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="normalize-space(./MultiCarDiscountInd)" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="MULTIDISCOUNT_QQ">
			<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLE_DISCOUNTS']/NODE[@ID='MULTICAR']/ATTRIBUTES[@ACORD_VAL=$MULTIDISCOUNT_ACORD]/@QQ_VAL" />
		</xsl:variable>
		<xsl:value-of select="$MULTIDISCOUNT_QQ" />
	</xsl:template>
	<!--GOOD STUDENT-->
	<xsl:template name="GOODSTUDENT">
		<!--xsl:variable name="VEHICLEID">
			<xsl:value-of select="substring(./@id,8,1)" />
		</xsl:variable-->
		<xsl:variable name="GOODSTUDENT">
			<!--xsl:for-each select="//PersDriver">
				<xsl:if test="PersDriverInfo/@VehPrincipallyDrivenRef=$VEHICLEID"-->
			<xsl:value-of select="PersDriverInfo/GoodStudentCd" />
			<!--/xsl:if>
			</xsl:for-each-->
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$GOODSTUDENT ='Y'">True</xsl:when>
			<xsl:otherwise>False</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--COLLEGE STUDENT -->
	<xsl:template name="COLLEGESTUDENT">
		<xsl:variable name="COLLEGESTUDENT">
			<xsl:value-of select="PersDriverInfo/DistantStudentInd" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$COLLEGESTUDENT ='1'">True</xsl:when>
			<xsl:otherwise>False</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--MULTIPOLICYAUTOHOMEDISCOUNT-->
	<xsl:template name="MULTIPOLICYAUTOHOMEDISCOUNT">
		<xsl:variable name="MULTIPOLICY">
			<xsl:value-of select="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/OtherOrPriorPolicy/Coverage/OtherOrPriorPolicy/PolicyCd" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$MULTIPOLICY !=''">Y
		</xsl:when>
			<xsl:otherwise>N</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--INSURANCESCORE-->
	<xsl:template name="INSURANCESCORE">
		<xsl:variable name="ACORD_SCORE">
			<xsl:choose>
				<xsl:when test="ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/CreditScoreInfo/CreditScore != ''">
					<xsl:value-of select="normalize-space(ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/CreditScoreInfo/CreditScore)"></xsl:value-of>
				</xsl:when>
				<xsl:otherwise>
				-1
				<!--xsl:message terminate="no">Insurance score not found in Acord XML.</xsl:message-->
			</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="INSURANCE_SCORE_QQ">
			<xsl:value-of select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCE_SCORE_MAPPING']/NODE[@ID='INSURANCE_SCORE']/ATTRIBUTES[@SCORE_RANGE=$ACORD_SCORE]/@QQ_SCORE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$INSURANCE_SCORE_QQ !=''">
				<xsl:value-of select="$INSURANCE_SCORE_QQ" />
			</xsl:when>
			<xsl:otherwise>-1</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--MILESEACH WAY -->
	<xsl:template name="MILESEACHWAY">
		<xsl:choose>
			<xsl:when test="normalize-space(./VehUseCd) = 'DO'">
				<xsl:value-of select="./DistanceOneWay/NumUnits" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="NOPREMIUMDRIVERDISCOUNT">
		<xsl:param name="LICENSEDYEARS" />
		<xsl:variable name="MIN_YEARS" select="$AutoMappingDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLE_DISCOUNTS']/NODE[@ID='PREMIUMDRIVERDISCOUNT']/ATTRIBUTES/@YEARS_LICENSED_THREASHOLD" />
		<xsl:choose>
			<xsl:when test="$LICENSEDYEARS &gt;= $MIN_YEARS">TRUE</xsl:when>
			<xsl:otherwise>FALSE</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="WEARINGSEATBELT">
		<xsl:variable name="VAR_WEARINGSEATBELT" select="//PersPolicy/PersApplicationInfo/Seatbelt" />
		<xsl:choose>
			<xsl:when test="normalize-space($VAR_WEARINGSEATBELT)='1'">TRUE</xsl:when>
			<xsl:otherwise>
				FALSE<!--xsl:value-of select="$VAR_WEARINGSEATBELT" /-->
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DRIVERSVEHICLE">
		<xsl:param name="FACTOR" />
		<xsl:variable name="VEHICLEID">
			<xsl:value-of select="substring(./@id,8,1)" />
		</xsl:variable>
		<xsl:for-each select="//PersDriver">
			<xsl:if test="PersDriverInfo/@VehPrincipallyDrivenRef=$VEHICLEID">
				<xsl:value-of select="@id" />
			</xsl:if>
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="POINTAPPLIED">
		<xsl:variable name="DRIVERID" select="substring(./@id,11,1)" />
		<xsl:variable name="VIDRVIDs" select="user:GetAssinedVehicleIds('')" />
		<xsl:for-each select="//PersPolicy/DriverVeh">
			<xsl:choose>
				<xsl:when test="substring(./@DriverRef,11,1)=$DRIVERID">
					<xsl:variable name="VEHICLEID" select="substring(./@VehRef,8,1)" />
					<xsl:variable name="POINTSAPP" select="PointsApplied" />
					<xsl:variable name="VEHPOIT">
						<xsl:value-of select="$VEHICLEID" />~<xsl:value-of select="$POINTSAPP" />
					</xsl:variable>
					<xsl:variable name="REFIDs" select="user:GetAssinedVehicleIds($VEHPOIT)" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:for-each>
		<xsl:value-of select="user:GetAssinedVehicleIds('-1')" />
	</xsl:template>
	<xsl:template name="VAHICLEASSIGEDAS">
		<xsl:variable name="DRIVERID" select="substring(./@id,11,1)" />
		<xsl:variable name="VIHIDs" select="user:GetAssinedVehicleIds('')" />
		<xsl:for-each select="//PersPolicy/DriverVeh">
			<xsl:variable name="VEHICLEID" select="substring(./@VehRef,8,1)" />
			<xsl:choose>
				<xsl:when test="substring(./@DriverRef,11,1)=$DRIVERID">
					<xsl:variable name="REFIDs" select="user:GetAssinedVehicleIds($VEHICLEID)" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:for-each>
		<xsl:value-of select="user:GetAssinedVehicleIds('-1')" />
	</xsl:template>
	<xsl:template name="DRIVERASSIGNMENTTYPE">
		<xsl:variable name="DRIVERID" select="substring(./@id,11,1)" />
		<xsl:variable name="VIHIDs" select="user:GetAssinedVehicleIds('')" />
		<xsl:for-each select="//PersPolicy/DriverVeh">
			<xsl:variable name="VEHICLEASIGNTYPE">
				<xsl:choose>
					<xsl:when test="DriverUseCd!=''">
						<xsl:value-of select="DriverUseCd" />
					</xsl:when>
					<xsl:otherwise>UNASSIGNED</xsl:otherwise>
				</xsl:choose>
			</xsl:variable>
			<xsl:choose>
				<xsl:when test="substring(./@DriverRef,11,1)=$DRIVERID">
					<xsl:variable name="REFIDs" select="user:GetAssinedVehicleIds($VEHICLEASIGNTYPE)" />
				</xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
		</xsl:for-each>
		<xsl:value-of select="user:GetAssinedVehicleIds('-1')" />
	</xsl:template>
	<xsl:template name="NODEPENDENTDISC">
		<xsl:variable name="VEHICLEID" select="substring(./@id,8,1)" />
		<xsl:variable name="VAR_NODEP">
			<xsl:for-each select="//PersPolicy/DriverVeh">
				<xsl:variable name="VEHICLEASIGNTYPE" select="DriverUseCd" />
				<xsl:choose>
					<xsl:when test="substring(./@VehRef,8,1)=$VEHICLEID">
						<!--xsl:variable name="REFIDs" select="user:GetAssinedVehicleIds($VEHICLEASIGNTYPE)" /-->
						<xsl:variable name="DRIVERID" select="substring(./@DriverRef,11,1)" />
						<!--xsl:variable name="DRIVERNAME" select="//PersDriver"-->
						<xsl:for-each select="//PersDriver">
							<xsl:choose>
								<xsl:when test="substring(./@id,11,1)=$DRIVERID">
									<xsl:variable name="VAR_DEP_VAL" select="DriverInfo/PersonInfo/NumDependents" />
									<xsl:if test="$VAR_DEP_VAL &lt; 1 or $VAR_DEP_VAL =0">NDEP</xsl:if>
								</xsl:when>
							</xsl:choose>
						</xsl:for-each>
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:for-each>
		</xsl:variable>
		<xsl:if test="contains($VAR_NODEP,'NDEP')">NDEP</xsl:if>
	</xsl:template>
	<xsl:template name="DRIVERINCOMEDISC">
		<xsl:variable name="VEHICLEID" select="substring(./@id,8,1)" />
		<xsl:variable name="VAR_NODEP">
			<xsl:for-each select="//PersPolicy/DriverVeh">
				<xsl:variable name="VEHICLEASIGNTYPE" select="DriverUseCd" />
				<xsl:choose>
					<xsl:when test="substring(./@VehRef,8,1)=$VEHICLEID">
						<!--xsl:variable name="REFIDs" select="user:GetAssinedVehicleIds($VEHICLEASIGNTYPE)" /-->
						<xsl:variable name="DRIVERID" select="substring(./@DriverRef,11,1)" />
						<!--xsl:variable name="DRIVERNAME" select="//PersDriver"-->
						<xsl:for-each select="//PersDriver">
							<xsl:choose>
								<xsl:when test="substring(./@id,11,1)=$DRIVERID">
									<xsl:variable name="VAR_DEP_VAL" select="DriverInfo/PersonInfo/DriverIncome" />
									<xsl:if test="$VAR_DEP_VAL &lt; 9">LOW</xsl:if>
								</xsl:when>
							</xsl:choose>
						</xsl:for-each>
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:for-each>
		</xsl:variable>
		<xsl:if test="contains($VAR_NODEP,'LOW')">LOW</xsl:if>
	</xsl:template>
	<xsl:template name="VEHICLE_COMPONLY">
		<xsl:choose>
			<xsl:when test="Coverage[CoverageCd='COMP']/Option/OptionCd='SUS'">SCO</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End Main Template -->
</xsl:stylesheet>