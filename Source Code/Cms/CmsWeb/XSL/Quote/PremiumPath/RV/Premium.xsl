<!-- ============================================================================================ 
	File Name		:	Primium.xsl																  
	Description		:	Generate the final premium for the Rental Dwelling 
	Developed By	:	Nidhi 
	Date			:   Aug 21 2006
	Modified By		:														  		
 ============================================================================================ -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:user="urn:user-namespace-here">
	<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml" />
	<!-- ============================================================================================ 
	This is the input structure
	<RV ID="">
		<YEAR></YEAR>
		<MAKE></MAKE>
		<MODEL></MODEL>
		<SERIALNUMBER></SERIALNUMBER>
		<MANUFACTURER></MANUFACTURER>
		<VEHICLETYPECODE></VEHICLETYPECODE>
		<HORSEPOWER></HORSEPOWER>
		<COVG_AMOUNT/>
		<DEDUCTIBLE/>
		<PERSONALLIABILITY_LIMIT/>
		<MEDICALPAYMENTSTOOTHERS_LIMIT/>
		<LIABILITY/>
		<MEDICAL_PAYMENTS/>
		<PHYSICAL_DAMAGE/>
	</RV>
-->
	<!-- ============================================================================================ 
								Loading ProductFactorMaster File and other variables (START)					  
 ============================================================================================ -->
	<xsl:variable name="RVFactorMaster" select="document('FactorPath')"></xsl:variable>
	<!-- Lookup id 1219 in masterlookup_values. these are distinct codes. 
	 Indiana and michigan states have same code for same policy types
	 ANY CHANGES HERE WILL NEED TO BE INCORPORATED IN RESP. FACTORMASTERPATH FILE TOO
	 Snowmobiles,Golf Carts	,ATV/4-6 Wheeler	,Trail Bike	,Mini Bike (No Mo-peds)	
Trailer	
	  -->
	<xsl:variable name="RV_SNOWMOBILE" select="'SNWMOB'" />
	<xsl:variable name="RV_GOLFCART" select="'GLFCRT'" />
	<xsl:variable name="RV_4WHEELER" select="'4WHLR'" />
	<xsl:variable name="RV_TRAIL_BIKE" select="'TRLBKE'" />
	<xsl:variable name="RV_MINI_BIKE" select="'MINI_TRLBKE'" />
	<xsl:variable name="RV_TRAILER" select="'TRLR'" />
	<!-- ============================================================================================ 
								Loading ProductFactorMaster File (END)						  
 ============================================================================================ -->
	<xsl:template match="/">
		<xsl:apply-templates select="RECREATIONVEHICLE" />
	</xsl:template>
	<!-- ============================================================================================ -->
	<!--								RV Template(START)								  -->
	<!-- ============================================================================================ -->
	<xsl:template match="RECREATIONVEHICLE">
		<PREMIUM>
			<CREATIONDATE></CREATIONDATE>
			<GETPATH>
				<PRODUCT PRODUCTID="0" DESC="Recreational Vehicles">
					<GROUP GROUPID="0">
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<!-- Vehicle Type -->
						<SUBGROUP>
							<STEP STEPID="0">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Make model -->
						<SUBGROUP>
							<STEP STEPID="1">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Propeerty Damage -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="2">
									<PATH>
						{
						 	<xsl:call-template name="PD" />
						 }
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Liability -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="3">
									<PATH>
						{
							<xsl:call-template name="LIABLITY_CHARGES" />
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Medical Payment -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="4">
									<PATH>
						{
							<xsl:call-template name="MEDICAL_CHARGES" />
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Surcharge -->
						<SUBGROUP>
							<STEP STEPID="5">
								<PATH>
									<xsl:call-template name="SURCHARGE_DISPLAY" />
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Final Primium -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="6">
									<PATH>
						{
							<xsl:call-template name="FINALPRIMIUM" />		
						}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
					</GROUP>
				</PRODUCT>
			</GETPATH>
			<!--=========================================================================================================== 
									 CALCULATIONS PART 
============================================================================================================ -->
			<CALCULATION>
				<PRODUCT PRODUCTID="0">
					<xsl:attribute name="DESC">
						<xsl:call-template name="PRODUCTID0" />
					</xsl:attribute>
					<GROUP GROUPID="0" CALC_ID="10000" PREFIX="" SUFIX="" ISCALCULATE="F">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID0" />
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION />
						</GROUPCONDITION>
						<GROUPRULE />
						<GROUPFORMULA />
						<GDESC />
						<!-- Vehicle Type  -->
						<STEP STEPID="0" CALC_ID="1000" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID0" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE></COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Vehicle Make, Model  -->
						<STEP STEPID="1" CALC_ID="1001" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID1" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE></COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Property Damage  -->
						<STEP STEPID="2" CALC_ID="1002" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID2" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH>
								<xsl:value-of select="DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(COVG_AMOUNT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>PD</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="PD" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<!--xsl:call-template name="COMPONENT_CODE">
							<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
						</xsl:call-template--></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Liability -->
						<STEP STEPID="3" CALC_ID="1003" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID3" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(PERSONALLIABILITY_LIMIT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>LIAB</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="LIABLITY_CHARGES" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Medical Payments -->
						<STEP STEPID="4" CALC_ID="1004" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID4" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="format-number(MEDICALPAYMENTSTOOTHERS_LIMIT, '###,###')" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MEDPAY</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="MEDICAL_CHARGES" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Surcharge-->
						<STEP STEPID="5" CALC_ID="1005" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID5" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>S</COMPONENT_TYPE>
							<COMPONENT_CODE>SURCH</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Final Premium -->
						<STEP STEPID="6" CALC_ID="1006" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID6" />
							</xsl:attribute>
							<PATH>P</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SUMTOTAL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="FINALPRIMIUM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
					</GROUP>
					<PRODUCTFORMULA>
						<!-- Formula for calculation of premium -->
						<GROUP1 GROUPID="10001">
							<VALUE>(@[CALC_ID=10001])</VALUE>
						</GROUP1>
					</PRODUCTFORMULA>
				</PRODUCT>
			</CALCULATION>
		</PREMIUM>
	</xsl:template>
	<!-- =========================================================================================================== 
								DEWLLINGDETAILS Template(END)								  
 ============================================================================================================== -->
	<!-- ========================================================================================================= 
									Templates for Lables  (START)							  
 ============================================================================================================= -->
	<!-- ===============================   Group Details	====================================================== -->
	<xsl:template name="PRODUCTID0">RECREATIONAL VEHICLES</xsl:template>
	<xsl:template name="GROUPID0">Recreational Vehicles</xsl:template>
	<!--xsl:template name ="GROUPID2">Make:<xsl:value-of select="MAKE"/> Model:<xsl:value-of select="MODEL"/></xsl:template>
<xsl:template name ="GROUPID3"><xsl:value-of select="HORSEPOWER"/>HP, Manufacturer:<xsl:value-of select="MANUFACTURER"/></xsl:template-->
	<!--Step Details-->
	<xsl:template name="STEPID0"><xsl:value-of select="VEHICLETYPE" />, <xsl:value-of select="YEAR" /></xsl:template>
	<xsl:template name="STEPID1">Make:<xsl:value-of select="MAKE" /> Model:<xsl:value-of select="MODEL" /></xsl:template>
	<xsl:template name="STEPID2">Physical Damage</xsl:template>
	<xsl:template name="STEPID3">Liability - Each Occurrence</xsl:template>
	<xsl:template name="STEPID4">Medical Payments to Others - Each Person</xsl:template>
	<xsl:template name="STEPID5">Surcharge on Snowmobile()</xsl:template>
	<xsl:template name="STEPID6">Final Premium</xsl:template>
	<!--========================================================================================================== 
									Template for Lables  (END)								  
 ============================================================================================================== -->
	<!-- Other templates   -->
	<xsl:template name="PD">
		<xsl:variable name="VAR_COVERAGE" select="COVG_AMOUNT" />
		<xsl:variable name="VAR_DEDUCTIBLE" select="DEDUCTIBLE" />
		<!-- Calculation depends on the vehicle types -->
		<xsl:choose>
			<xsl:when test="($VAR_COVERAGE='' or $VAR_COVERAGE='0') and normalize-space(PHYSICAL_DAMAGE)='N'">0.00</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<!-- SNOW MOBILE-->
					<xsl:when test="normalize-space(VEHICLETYPECODE) = $RV_SNOWMOBILE or normalize-space(VEHICLETYPECODE) = $RV_4WHEELER ">
						<xsl:variable name="VAR_RATE_PER_VAL" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PHYSICAL_DAMAGE']/NODE[@ID ='PD_SNWMOB_4WHLR']/@FOR_EACH_ADDITION" />
						<xsl:variable name="VAR_RATE_FOR_DEDUCTIBLE" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PHYSICAL_DAMAGE']/NODE[@ID ='PD_SNWMOB_4WHLR']/ATTRIBUTES[@DEDUCTIBLE = $VAR_DEDUCTIBLE]/@RATE" />
						<xsl:value-of select="round(($VAR_COVERAGE div $VAR_RATE_PER_VAL) * $VAR_RATE_FOR_DEDUCTIBLE)" />
					</xsl:when>
					<!-- MINI BIKE , TRAIL BIKE -->
					<xsl:when test="normalize-space(VEHICLETYPECODE) = $RV_TRAIL_BIKE or normalize-space(VEHICLETYPECODE)= $RV_MINI_BIKE">
						<xsl:variable name="VAR_DEDUCTIBLE_FACTOR" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLE_FACTOR']/NODE[@ID ='DED_MNIBIKE_TRLBIKE']/ATTRIBUTES[@DEDUCTIBLE= $VAR_DEDUCTIBLE]/@FACTOR" />
						<xsl:variable name="VAR_MAXLIMIT" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PHYSICAL_DAMAGE']/NODE[@ID ='PD_MNIBIKE_TRLBIKE']/@MAX_LIMIT" />
						<xsl:choose>
							<xsl:when test="$VAR_COVERAGE &lt;= $VAR_MAXLIMIT"> <!-- within limits  -->
								<xsl:variable name="VAR_RATE" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PHYSICAL_DAMAGE']/NODE[@ID ='PD_MNIBIKE_TRLBIKE']/ATTRIBUTES[@MIN_COST &lt; $VAR_COVERAGE and @MAX_COST &gt;=$VAR_COVERAGE]/@RATE" />
								<xsl:value-of select="$VAR_RATE * $VAR_DEDUCTIBLE_FACTOR " />
							</xsl:when>
							<xsl:otherwise> <!-- additional amount -->
								<xsl:variable name="VAR_DIFFERENCE_NRST_THOUSAND">
									<xsl:call-template name="GET_NRST_THOUSAND">
										<xsl:with-param name="NUMERIC_VALUE" select="($VAR_COVERAGE - $VAR_MAXLIMIT)" />
									</xsl:call-template>
								</xsl:variable>
								<xsl:variable name="VAR_RATE" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PHYSICAL_DAMAGE']/NODE[@ID ='PD_MNIBIKE_TRLBIKE']/ATTRIBUTES[@MAX_COST =$VAR_MAXLIMIT]/@RATE" />
								<xsl:variable name="VAR_FOREACH_ADDITIONAL_AMOUNT" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PHYSICAL_DAMAGE']/NODE[@ID ='PD_MNIBIKE_TRLBIKE']/ATTRIBUTES/@FOR_EACH_ADDITION" />
								<xsl:variable name="VAR_ADDITIONAL_AMOUNT_FACTOR" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PHYSICAL_DAMAGE']/NODE[@ID ='PD_MNIBIKE_TRLBIKE']/ATTRIBUTES/@FACTOR" />
								<!-- ITrack # 6177 - 29 July 09 -->
								<!--<xsl:value-of select="($VAR_RATE * $VAR_DEDUCTIBLE_FACTOR) +  round(($VAR_DIFFERENCE_NRST_THOUSAND div $VAR_FOREACH_ADDITIONAL_AMOUNT) * $VAR_ADDITIONAL_AMOUNT_FACTOR)" /> -->
								<xsl:value-of select="round($VAR_RATE * $VAR_DEDUCTIBLE_FACTOR) * (1 + ($VAR_DIFFERENCE_NRST_THOUSAND div $VAR_FOREACH_ADDITIONAL_AMOUNT) * $VAR_ADDITIONAL_AMOUNT_FACTOR)" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<!-- GOLF CARTS -->
					<xsl:when test="VEHICLETYPECODE = $RV_GOLFCART">
						<!-- Deductible factor for minibike -->
						<xsl:variable name="VAR_DEDUCTIBLE_FACTOR" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLE_FACTOR']/NODE[@ID ='DED_MNIBIKE_TRLBIKE']/ATTRIBUTES[@DEDUCTIBLE= $VAR_DEDUCTIBLE]/@FACTOR" />
						<!-- Max Limit for minibike -->
						<xsl:variable name="VAR_MAXLIMIT" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PHYSICAL_DAMAGE']/NODE[@ID ='PD_MNIBIKE_TRLBIKE']/@MAX_LIMIT" />
						<!-- Deductible Factor for Golfcarts is 30% that of minibike -->
						<xsl:variable name="VAR_DEDUCTIBLE_FACTOR_OF_MNIBIKE" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLE_FACTOR']/NODE[@ID ='DED_PD_GOLF']/ATTRIBUTES/@DED_FACTOR_OF_MNIBIKE_TLRBIKE" />
						<xsl:choose>
							<xsl:when test="$VAR_COVERAGE &lt;= $VAR_MAXLIMIT"> <!-- within limits -->
								<xsl:variable name="VAR_RATE" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PHYSICAL_DAMAGE']/NODE[@ID ='PD_MNIBIKE_TRLBIKE']/ATTRIBUTES[@MIN_COST &lt; $VAR_COVERAGE and @MAX_COST &gt;= $VAR_COVERAGE]/@RATE" />
								<xsl:value-of select="round($VAR_RATE * $VAR_DEDUCTIBLE_FACTOR  * $VAR_DEDUCTIBLE_FACTOR_OF_MNIBIKE)" />
							</xsl:when>
							<xsl:otherwise> <!-- additional amount -->
								<xsl:variable name="VAR_DIFFERENCE_NRST_THOUSAND">
									<xsl:call-template name="GET_NRST_THOUSAND">
										<xsl:with-param name="NUMERIC_VALUE" select="($VAR_COVERAGE - $VAR_MAXLIMIT)" />
									</xsl:call-template>
								</xsl:variable>
								<xsl:variable name="VAR_RATE" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PHYSICAL_DAMAGE']/NODE[@ID ='PD_MNIBIKE_TRLBIKE']/ATTRIBUTES[@MAX_COST =$VAR_MAXLIMIT]/@RATE" />
								<xsl:variable name="VAR_FOREACH_ADDITIONAL_AMOUNT" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PHYSICAL_DAMAGE']/NODE[@ID ='PD_GOLF']/ATTRIBUTES/@FOR_EACH_ADDITION" />
								<xsl:variable name="VAR_ADDITIONAL_AMOUNT_FACTOR" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PHYSICAL_DAMAGE']/NODE[@ID ='PD_GOLF']/ATTRIBUTES/@FACTOR" />
								<xsl:value-of select="($VAR_RATE * $VAR_DEDUCTIBLE_FACTOR* $VAR_DEDUCTIBLE_FACTOR_OF_MNIBIKE) +  round(($VAR_DIFFERENCE_NRST_THOUSAND div $VAR_FOREACH_ADDITIONAL_AMOUNT) * $VAR_ADDITIONAL_AMOUNT_FACTOR)" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<!-- TRAILER -->
					<xsl:when test="VEHICLETYPECODE = $RV_TRAILER">
						<xsl:variable name="VAR_RATE_PER_VAL" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PHYSICAL_DAMAGE']/NODE[@ID ='PD_TRAILER']/@FOR_EACH_ADDITION" />
						<xsl:variable name="VAR_RATE_FOR_DEDUCTIBLE" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PHYSICAL_DAMAGE']/NODE[@ID ='PD_TRAILER']/ATTRIBUTES[@DEDUCTIBLE = $VAR_DEDUCTIBLE]/@RATE" />
						7<xsl:value-of select="round(($VAR_COVERAGE div $VAR_RATE_PER_VAL) * $VAR_RATE_FOR_DEDUCTIBLE)" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="FINALPRIMIUM">
		<xsl:variable name="VAR_PD">
			<xsl:call-template name="PD" />
		</xsl:variable>
		<xsl:variable name="VAR_LIABILITY">
			<xsl:call-template name="LIABLITY_CHARGES" />
		</xsl:variable>
		<xsl:variable name="VAR_MEDICALPAYMENT">
			<xsl:call-template name="MEDICAL_CHARGES" />
		</xsl:variable>
		<xsl:value-of select="($VAR_PD + $VAR_LIABILITY + $VAR_MEDICALPAYMENT)" />
	</xsl:template>
	<xsl:template name="MEDICAL_CHARGES">
		<xsl:variable name="VAR_RATE_PERVAL_MEDPAY" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITY_MEDICAL_PAYMENT']/NODE[@ID='MED_PAY_LIMIT']/ATTRIBUTES/@RATE_PERVAL_MEDPAY" />
		<xsl:choose>
			<xsl:when test="MEDICALPAYMENTSTOOTHERS_LIMIT &gt; 0 and normalize-space(MEDICAL_PAYMENTS)='Y'">
				<xsl:choose>
					<!-- 4 wheelers-->
					<xsl:when test="normalize-space(VEHICLETYPECODE) = $RV_4WHEELER ">
						<xsl:variable name="VAR_VALUE_FOR_ECH_ADDTN" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITY_MEDICAL_PAYMENT']/NODE[@ID='4WHLR']/@FOR_EACH_ADDITION" />
						<xsl:variable name="VAR_BASE_MEDPAY" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITY_MEDICAL_PAYMENT']/NODE[@ID='4WHLR']/@MEDICAL_PAYMNT_BASE" />
						<xsl:value-of select="((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_BASE_MEDPAY) div $VAR_RATE_PERVAL_MEDPAY) * $VAR_VALUE_FOR_ECH_ADDTN" />
					</xsl:when>
					<!-- SNOW MOBILE -->
					<xsl:when test="normalize-space(VEHICLETYPECODE) = $RV_SNOWMOBILE">
						<xsl:variable name="VAR_CC" select="CC" />
						<xsl:variable name="VAR_VALUE_FOR_ECH_ADDTN" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITY_MEDICAL_PAYMENT']/NODE[@ID='SNWMOBILE']/ATTRIBUTES[@CC_MIN &lt;= $VAR_CC and @CC_MAX &gt;= $VAR_CC]/@FOR_EACH_ADDITION" />
						<xsl:variable name="VAR_BASE_MEDPAY" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITY_MEDICAL_PAYMENT']/NODE[@ID='SNWMOBILE']/@MEDICAL_PAYMNT_BASE" />
						<xsl:value-of select="((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_BASE_MEDPAY) div $VAR_RATE_PERVAL_MEDPAY) * $VAR_VALUE_FOR_ECH_ADDTN" />
					</xsl:when>
					<!-- Golf cart,MINI BIKE ,TRAIL BIKE -->
					<xsl:when test="normalize-space(VEHICLETYPECODE) = $RV_TRAIL_BIKE or normalize-space(VEHICLETYPECODE)= $RV_MINI_BIKE or normalize-space(VEHICLETYPECODE) = $RV_GOLFCART">
						<xsl:variable name="VAR_VALUE_FOR_ECH_ADDTN" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITY_MEDICAL_PAYMENT']/NODE[@ID='GLFCRT_MINIBK_TRLBKE']/@FOR_EACH_ADDITION" />
						<xsl:variable name="VAR_BASE_MEDPAY" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITY_MEDICAL_PAYMENT']/NODE[@ID='GLFCRT_MINIBK_TRLBKE']/@MEDICAL_PAYMNT_BASE" />
						<xsl:value-of select="((MEDICALPAYMENTSTOOTHERS_LIMIT - $VAR_BASE_MEDPAY) div $VAR_RATE_PERVAL_MEDPAY) * $VAR_VALUE_FOR_ECH_ADDTN" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="LIABLITY_CHARGES_old">
		<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
		<xsl:value-of select="$VAR_LIMIT" />
	</xsl:template>
	<xsl:template name="LIABLITY_CHARGES">
		<xsl:variable name="VAR_LIBILITY" select="LIABILITY" />
		<xsl:variable name="VAR_LIMIT" select="PERSONALLIABILITY_LIMIT" />
		<xsl:variable name="VAR_CC" select="CC" />
		<xsl:choose>
			<xsl:when test="PERSONALLIABILITY_LIMIT &gt; 0 and normalize-space($VAR_LIBILITY) = 'Y'">
				<xsl:choose>
					<!-- 4 wheelers-->
					<xsl:when test="normalize-space(VEHICLETYPECODE) = $RV_4WHEELER ">
						<xsl:value-of select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITY_MEDICAL_PAYMENT']/NODE[@ID='4WHLR']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_RATE" />
					</xsl:when>
					<!-- SNOW MOBILE -->
					<xsl:when test="normalize-space(VEHICLETYPECODE) = $RV_SNOWMOBILE">
						<xsl:value-of select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITY_MEDICAL_PAYMENT']/NODE[@ID='SNWMOBILE']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT and @CC_MIN &lt;= $VAR_CC and @CC_MAX &gt;= $VAR_CC]/@LIABILITY_LIMIT_RATE" />
					</xsl:when>
					<!-- Golf cart,MINI BIKE ,TRAIL BIKE -->
					<xsl:when test="normalize-space(VEHICLETYPECODE) = $RV_TRAIL_BIKE or normalize-space(VEHICLETYPECODE)= $RV_MINI_BIKE or normalize-space(VEHICLETYPECODE) = $RV_GOLFCART">
						<xsl:value-of select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIABILITY_MEDICAL_PAYMENT']/NODE[@ID='GLFCRT_MINIBK_TRLBKE']/ATTRIBUTES[@LIABILITY_LIMIT = $VAR_LIMIT]/@LIABILITY_LIMIT_RATE" />
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="GET_NRST_THOUSAND">
		<!-- ceil thousand -->
		<xsl:param name="NUMERIC_VALUE" />
		<xsl:variable name="VAR1" select="$NUMERIC_VALUE" />
		<xsl:variable name="VAR2">
			<xsl:value-of select="$VAR1 mod 1000" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$VAR2 &gt; 0">
				<xsl:value-of select="$VAR1 + 1000 - $VAR2" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$VAR1" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SURCHARGE_DISPLAY">
		<xsl:variable name="MIN_CC" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='SNOWMOBILE']/ATTRIBUTES/@CC_MIN" />
		<xsl:variable name="MAX_CC" select="$RVFactorMaster/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGE']/NODE[@ID='SNOWMOBILE']/ATTRIBUTES/@CC_MAX" />
		<xsl:choose>
			<xsl:when test="HORSEPOWER &gt; $MIN_CC and HORSEPOWER &lt;= MAX_CC">Included</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>
