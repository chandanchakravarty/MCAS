<!-- ============================================================================================ -->
<!-- File Name		:	Primium.xsl																  -->
<!-- Description	:	This xsl file is for generating 
						the Final Primium (For vehicle component Auto)INDIANA and MICHIGAN		  -->
<!-- Developed By	:	Nidhi Sahay													  -->
<!-- ============================================================================================ -->
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:user="urn:user-namespace-here">
	<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml" />
	<msxsl:script language="c#" implements-prefix="user">
<![CDATA[ 
		
		int ZONE_ID=2;
		int violationPercentage=0;
		public int GetPvioAcc(int AutoViolationValue)
		{
			int val=0;
			if(AutoViolationValue < 0)
			{
				violationPercentage=0;	
			}
			else
			{
			
				val=AutoViolationValue;
				violationPercentage = violationPercentage + val;
				
			}
			return violationPercentage;
		}
]]></msxsl:script>
	<!-- ============================================================================================ -->
	<!--								Loading ProductFactorMaster File (START)					  -->
	<!-- ============================================================================================ -->
	<xsl:variable name="HOCoveragesDoc" select="document('FactorPath')"></xsl:variable>
	<xsl:variable name="VT_CAMPER_TRAVEL_TRAILER" select="'CTT'" />
	<xsl:variable name="VT_UTILITY_TRAILER" select="'TR'" />
	<xsl:variable name="VT_CUSTOMIZED_VAN" select="'CV'" />
	<xsl:variable name="VT_PRIVATE_PASSENGER" select="'PP'" />
	<xsl:variable name="VT_MOTORHOME" select="'MH'" />
	<xsl:variable name="VT_SUSPENDED_COMP" select="'SCO'" />
	<xsl:variable name="STATE_INDIANA" select="'INDIANA'" />
	<xsl:variable name="STATE_MICHIGAN" select="'MICHIGAN'" />
	<xsl:variable name="VEHICLE_CLASS_PRE_2" select="2" />
	<xsl:variable name="VEHICLE_CLASS_PRE_3" select="3" />
	<xsl:variable name="VEHICLE_CLASS_PRE_4" select="4" />
	<xsl:variable name="VEHICLE_CLASS_PRE_6" select="6" />
	<xsl:variable name="VT_CLASSIC_CAR" select="'CLC'" />
	<xsl:variable name="VT_ANTIQUE_CAR" select="'ANC'" />
	<!-- ============================================================================================ -->
	<!--								Loading ProductFactorMaster File (END)						  -->
	<!-- ============================================================================================ -->
	<xsl:template match="/">
		<xsl:apply-templates select="INPUTXML/QUICKQUOTE" />
	</xsl:template>
	<xsl:template match="INPUTXML/QUICKQUOTE">
		<PREMIUM>
			<CREATIONDATE></CREATIONDATE>
			<GETPATH>
				<PRODUCT PRODUCTID="0">
					<!--Group for Vehicles-->
					<GROUP GROUPID="0">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID0" />
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<!-- Vehicle year VIN -->
						<SUBGROUP>
							<STEP STEPID="0">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Symbol, Class-->
						<SUBGROUP>
							<STEP STEPID="1">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Use ,Miles each way-->
						<SUBGROUP>
							<STEP STEPID="2">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Garaged At, Territory-->
						<SUBGROUP>
							<STEP STEPID="3">
								<PATH>ND</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Residual Bodily Injury BI-->
						<SUBGROUP>
							<IF>
								<STEP STEPID="4">
									<PATH>
									{
									<xsl:call-template name="BI" />
									}
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Residual Property Damage -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="5">
									<PATH>
									{
									<xsl:call-template name="PD" />
									}
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Combined Single Limit (Bodily Injury, PD) -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="6">
									<PATH>
									{
									<xsl:call-template name="CSLBIPD" />
									}
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- PIP -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="7">
									<PATH>
							{
								<xsl:call-template name="PIP" />
							}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Michigan Statutory Assessment -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="8">
									<PATH>
									{
										<xsl:call-template name="MCCAFEE" />
									}
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Michigan Historical Vehicle -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="9">
									<PATH>
									{
										<xsl:call-template name="MCCHAFEE" />
									}
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- PPI -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="10">
									<PATH>
								{
									<xsl:call-template name="PPI" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Medical Payments -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="11">
									<PATH>
								{
									<xsl:call-template name="MEDPAY" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!--Uninsured Motorists -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="12">
									<PATH>
								{
									<xsl:call-template name="UM" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Under Insured Motorists -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="13">
									<PATH>
								{
									<xsl:call-template name="UIM" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!--Uninsured Motorists - Property Damagae-->
						<SUBGROUP>
							<IF>
								<STEP STEPID="14">
									<PATH>
								{
									<xsl:call-template name="UMPD" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Comprehensive -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="15">
									<PATH>
								{
									<xsl:call-template name="COMP" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Collision -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="16">
									<PATH>
								 {
									 <xsl:choose>
											<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONTYPE ='LIMITED'">
												<xsl:call-template name="LTDCOLL" />
											</xsl:when>
											<xsl:otherwise>
												<xsl:call-template name="COLL" />
											</xsl:otherwise>
										</xsl:choose>
								 }
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Mini-tort -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="17">
									<PATH>
									{
										<xsl:call-template name="MINITORT" />
									}
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Road Service -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="18">
									<PATH>
									{
										<xsl:call-template name="ROADSERVICES" />
									}
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Rental Reimbursement -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="19">
									<PATH>
									{
										<xsl:call-template name="RENTALREIMBURSEMENT" />
									}
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Loan/Loss Lease Gap-->
						<SUBGROUP>
							<IF>
								<STEP STEPID="20">
									<PATH>
								{
									 <xsl:call-template name="LOANLEASEGAP" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Sound Reproducing Tapes -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="21">
									<PATH>
								{
								<xsl:call-template name="SOUNDREPRODUCINGTAPES" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Sound Receiving and Transmitting Equipments -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="22">
									<PATH>
								{
								<xsl:call-template name="SOUNDRECVTRANSEQUIPMENT" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Extra Equipment - Comprehensive  -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="23">
									<PATH>
								{
									<xsl:call-template name="EXTRAEQUIPMENTCOMP" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Extra Equipment -Broadened Collision -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="24">
									<PATH>
								{
									 
									<xsl:call-template name="EXTRAEQUIPMENTCOLL" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!--Extended Non-Owned Coverage for Named Individual (A-35)-->
						<SUBGROUP>
							<IF>
								<STEP STEPID="25">
									<PATH>
								{
									 
									<xsl:call-template name="ENOLIABILITY_PREMIUM" />
								}
							</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Discount:PIP Discount Display -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="26">
									<PATH>
										<xsl:call-template name="PIP_DISPLAY" />
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Discount:Wearing Seat Belts -->
						<SUBGROUP>
							<STEP STEPID="27">
								<PATH>
									<xsl:call-template name="SEATBELT_DISPLAY" />
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount:Air Bags -->
						<SUBGROUP>
							<STEP STEPID="28">
								<PATH>
									<xsl:call-template name="AIRBAG_DISPLAY" />
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount:Anti-Lock Breaks System -->
						<SUBGROUP>
							<STEP STEPID="29">
								<PATH>
									<xsl:call-template name="ABS_DISPLAY" />
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount:Multi-Car-->
						<SUBGROUP>
							<STEP STEPID="30">
								<PATH>
									<xsl:call-template name="MULTIVEHICLE_DISPLAY" />
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount:Multi-Policy(Auto/Home)-->
						<SUBGROUP>
							<STEP STEPID="31">
								<PATH>
									<xsl:call-template name="MULTIPOLICY_DISPLAY" />
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount - Trailblazer Program 15% -->
						<SUBGROUP>
							<STEP STEPID="32">
								<PATH>
									<xsl:call-template name="TRAILBLAZER_DISPLAY" />
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount:Insurance Score Credit-->
						<SUBGROUP>
							<STEP STEPID="33">
								<PATH>
									<xsl:call-template name="INSURANCESCORE_DISPLAY" />
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount:Premier Driver -->
						<SUBGROUP>
							<STEP STEPID="34">
								<PATH>
									<xsl:call-template name="DRIVERDISCOUNT_DISPLAY" />
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Discount:Good Student -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="35">
									<PATH>
										<xsl:call-template name="GOODSTUDENT_DISPLAY" />
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Discount:Good Student -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="36">
									<PATH>
										<xsl:call-template name="BUSINESS_SURCHRAGE" />
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
						<!-- Surcharge:Accidents and Violations  -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="37">
									<PATH>
										<xsl:call-template name="ACCIDENT_VIOLATION_SURCHARGE_DISPLAY" />
									</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
					</GROUP>
					<!--Group for Final Premium-->
					<GROUP GROUPID="1">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID1" />
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<SUBGROUP>
							<STEP STEPID="38">
								<PATH>
									<xsl:call-template name="FINAL_MINIMUM_PREMIUM" />
								</PATH>
							</STEP>
						</SUBGROUP>
						<!-- Final Premium -->
						<SUBGROUP>
							<IF>
								<STEP STEPID="39">
									<PATH>
							{
								<xsl:call-template name="FINALPREMIUM" />
							}
						</PATH>
								</STEP>
							</IF>
						</SUBGROUP>
					</GROUP>
				</PRODUCT>
			</GETPATH>
			<CALCULATION>
				<PRODUCT PRODUCTID="0">
					<!--Product Name -->
					<xsl:attribute name="DESC">
						<xsl:call-template name="PRODUCTNAME" />
					</xsl:attribute>
					<!--Group for Vehicles-->
					<GROUP GROUPID="0" CALC_ID="10000">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID0" />
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<GROUPRULE></GROUPRULE>
						<GROUPFORMULA></GROUPFORMULA>
						<GDESC></GDESC>
						<!-- Vehicle year VIN -->
						<STEP STEPID="0" CALC_ID="1000" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID0" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE></COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Symbol, Class-->
						<STEP STEPID="1" CALC_ID="1001" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID1" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE>SYM_CLS</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Use ,Miles each way-->
						<STEP STEPID="2" CALC_ID="1002" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID2" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE>USE</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Garaged At, Territory-->
						<STEP STEPID="3" CALC_ID="1003" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID3" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE></COMPONENT_TYPE>
							<COMPONENT_CODE>GRG_LOC</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Residual Bodily Injury -->
						<STEP STEPID="4" CALC_ID="1004" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID4" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/BI" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>BI</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="BI" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Residual Property Damage -->
						<STEP STEPID="5" CALC_ID="1005" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID5" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:choose>
									<xsl:when test="contains(VEHICLES/VEHICLE/PD,',')">
										<xsl:value-of select="VEHICLES/VEHICLE/PD" />
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="format-number(VEHICLES/VEHICLE/PD,'###,###')" />
									</xsl:otherwise>
								</xsl:choose>
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>PD</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="PD" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Combined Single Limit (Bodily Injury, PD) -->
						<STEP STEPID="6" CALC_ID="1006" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID6" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/CSL" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>CSL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="CSLBIPD" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'CSL'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- PIP -->
						<STEP STEPID="7" CALC_ID="1007" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID7" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:call-template name="PIP_DEDUCTIBLE" />
							</D_PATH>
							<L_PATH>Unlimited</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>PIP</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="PIP" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'PIP'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Michigan Statutory Assessment -->
						<STEP STEPID="8" CALC_ID="1008" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID8" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MCCAFEE</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="MCCAFEE" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'MCCAFEE'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Michigan Statutory Assessment Historical Vehicle -->
						<STEP STEPID="9" CALC_ID="1009" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID9" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MCCAFEE</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="MCCHAFEE" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'MCCAFEE'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- PPI -->
						<STEP STEPID="10" CALC_ID="1010" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID10" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>1,000,000</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>PPI</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="PPI" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'PPI'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Medical Payments -->
						<STEP STEPID="11" CALC_ID="1011" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID11" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/MPLIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MED_PMT</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="MEDPAY" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'MED_PMT'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Uninsured Motorist -->
						<STEP STEPID="12" CALC_ID="1012" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID12" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:call-template name="UMSPLITDISPLAY" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>UM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="UM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'UM'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Underinsured Motorist -->
						<STEP STEPID="13" CALC_ID="1013" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID13" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:call-template name="UIMSPLITDISPLAY" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>UIM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="UIM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'UIM'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Uninsured Motorist -Property Damage-->
						<STEP STEPID="14" CALC_ID="1014" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID14" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:call-template name="PDDEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/PDLIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>UMPD</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="UMPD" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'UMPD'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Comprehensive -->
						<STEP STEPID="15" CALC_ID="1015" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID15" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE" />
							</D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>COMP</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="COMP" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Collision -->
						<STEP STEPID="16" CALC_ID="1016" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID16" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:choose>
									<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONTYPE='LIMITED'">LIMITED</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE" />
									</xsl:otherwise>
								</xsl:choose>
							</D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>COLL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:choose>
									<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONTYPE ='LIMITED'">
										<xsl:call-template name="LTDCOLL" />
									</xsl:when>
									<xsl:otherwise>
										<xsl:call-template name="COLL" />
									</xsl:otherwise>
								</xsl:choose>
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Mini-tort -->
						<STEP STEPID="17" CALC_ID="1017" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID17" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:call-template name="MINITORTLIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>M_TRT</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="MINITORT" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'M_TRT'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Road Service -->
						<STEP STEPID="18" CALC_ID="1018" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID18" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/ROADSERVICE" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>RD_SRVC</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="ROADSERVICES" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'RD_SRVC'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Rental Reimbursement -->
						<STEP STEPID="19" CALC_ID="1019" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID19" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/RENTALREIMLIMITDAY" />/<xsl:choose>
									<xsl:when test="contains(VEHICLES/VEHICLE/RENTALREIMMAXCOVG,',')">
										<xsl:value-of select="VEHICLES/VEHICLE/RENTALREIMMAXCOVG" />
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="format-number(VEHICLES/VEHICLE/RENTALREIMMAXCOVG,'###,###')" />
									</xsl:otherwise>
								</xsl:choose>
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>RNT_RMBRS</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="RENTALREIMBURSEMENT" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'RNT_RMBRS'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Loan/Loss Lease Gap-->
						<STEP STEPID="20" CALC_ID="1020" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID20" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>LN_LSE</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="LOANLEASEGAP" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'LN_LSE'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Sound Reproducing Tapes -->
						<STEP STEPID="21" CALC_ID="1021" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID21" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:call-template name="SOUNDREPRODUCINGTAPESLIMIT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SND_RPR</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="SOUNDREPRODUCINGTAPES" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'SND_RPR'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Sound Receiving and Transmitting Equipments -->
						<STEP STEPID="22" CALC_ID="1022" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID22" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/SOUNDRECEIVINGTRANSMITTINGSYSTEM" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SND_RCV</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="SOUNDRECVTRANSEQUIPMENT" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'SND_RCV'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Extra Equipment - Comprehensive  -->
						<STEP STEPID="23" CALC_ID="1023" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID23" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE" />
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/INSURANCEAMOUNT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>XTR_COMP</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="EXTRAEQUIPMENTCOMP" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'XTR_COMP'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Extra Equipment -Broadened Collision -->
						<STEP STEPID="24" CALC_ID="1024" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID24" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH>
								<xsl:call-template name="EXTRAEQUIPMENT_DEDUC"></xsl:call-template>
							</D_PATH>
							<L_PATH>
								<xsl:value-of select="VEHICLES/VEHICLE/INSURANCEAMOUNT" />
							</L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>XTR_COLL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="EXTRAEQUIPMENTCOLL" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'XTR_COLL'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!--Extended Non-Owned Coverage for Named Individual (A-35)-->
						<STEP STEPID="25" CALC_ID="1025" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID25" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>ENOL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="ENOLIABILITY_PREMIUM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT>
								<xsl:call-template name="COMPONENT_CODE">
									<xsl:with-param name="FACTORELEMENT" select="'ENO'"></xsl:with-param>
								</xsl:call-template>
							</COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Discount:PIP Discount Display -->
						<STEP STEPID="26" CALC_ID="1026" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID26" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_WRK_LSS</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="COVERAGETEXT_DISPLAY" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD>
								<xsl:call-template name="COVERAGETYPE_DISCOUNT_PERCENTAGE" />
							</COM_EXT_AD>
						</STEP>
						<!-- Discount:Wearing Seat Belts -->
						<STEP STEPID="27" CALC_ID="1027" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID27" />
							</xsl:attribute>
							<PATH></PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_ST_BLT</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKSEATBELT" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD><xsl:call-template name="SEAT_BELTS_CREDIT_PERCENT" />%</COM_EXT_AD>
						</STEP>
						<!-- Discount:Air Bags -->
						<STEP STEPID="28" CALC_ID="1028" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID28" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_AR_BG</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKAIRBAG" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD><xsl:call-template name="AIRBAG_DISCOUNT_PERCENT" />%</COM_EXT_AD>
						</STEP>
						<!-- Discount:Anti-Lock Breaks System -->
						<STEP STEPID="29" CALC_ID="1029" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID29" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_ANT_LCK_BRK</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKANTILOCK" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD>
								<xsl:call-template name="ABS_CREDIT" />
							</COM_EXT_AD>
						</STEP>
						<!-- Discount:Multi-Car-->
						<STEP STEPID="30" CALC_ID="1030" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID30" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_MLT_CR</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKMULTICAR" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Discount:Multi-Policy(Auto/Home)-->
						<STEP STEPID="31" CALC_ID="1031" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID31" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_MLT_POL</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKMULTIPOLICY" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD><xsl:call-template name="MULTIPOLICY_DISCOUNT" />%</COM_EXT_AD>
						</STEP>
						<!-- Discount: Trailblazer -->
						<STEP STEPID="32" CALC_ID="1032" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID32" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_TRLBLZR</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKTRAILBLAZER" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD>
								<xsl:call-template name="TRAILBLAZER_DISCOUNT" />%
							</COM_EXT_AD>
						</STEP>
						<!-- Discount:Insurance Score Credit-->
						<STEP STEPID="33" CALC_ID="1033" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID33" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>
								<xsl:call-template name="INSURANCE_SCORE_COMPONENT_TYPE" />
							</COMPONENT_TYPE>
							<COMPONENT_CODE>D_INS_SCR</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKINSURANCESCORE" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD>
								<xsl:call-template name="INSURANCESCORE_PERCENT" />
							</COM_EXT_AD>
						</STEP>
						<!-- Discount:Premier Driver -->
						<STEP STEPID="34" CALC_ID="1034" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID34" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_PRM_DVR</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKSAFEDRIVER" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD>
								<xsl:call-template name="DRIVERDISCOUNT_CREDIT" />
							</COM_EXT_AD>
						</STEP>
						<!-- Discount:Good Student -->
						<STEP STEPID="35" CALC_ID="1035" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID35" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>D</COMPONENT_TYPE>
							<COMPONENT_CODE>D_GD_STDNT</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKGOODSTUDENT" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD><xsl:call-template name="GOODSTUDENT_DISCOUNT_PERCENT" />%</COM_EXT_AD>
						</STEP>
						<!-- Surcharge: Bussiness Use -->
						<STEP STEPID="36" CALC_ID="1036" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID36" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>S</COMPONENT_TYPE>
							<COMPONENT_CODE>S_BUSI_CHAR</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKVEHICLEUSE" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD>
								<xsl:call-template name="BUSINESS_SURCHRAGE_PERCENTAGE" />
							</COM_EXT_AD>
						</STEP>
						<!-- Surcharge: ACCIDENT and VIOLATION -->
						<STEP STEPID="37" CALC_ID="1037" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID37" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>S</COMPONENT_TYPE>
							<COMPONENT_CODE>S_ACCI_VIOL</COMPONENT_CODE>
							<COMP_ACT_PRE></COMP_ACT_PRE>
							<COMP_REMARKS>
								<xsl:call-template name="REMARKACCIDENTVIOL" />
							</COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD>
								<xsl:call-template name="ACCIDENT_VIOLATION_SURCHRAGE_PERCENTAGE" />%
							</COM_EXT_AD>
						</STEP>
					</GROUP>
					<!--Group for Final Premium-->
					<GROUP GROUPID="1" CALC_ID="10001" PREFIX="" SUFIX="" ISCALCULATE="F">
						<xsl:attribute name="DESC">
							<xsl:call-template name="GROUPID1" />
						</xsl:attribute>
						<GROUPCONDITION>
							<CONDITION></CONDITION>
						</GROUPCONDITION>
						<GROUPRULE></GROUPRULE>
						<GROUPFORMULA></GROUPFORMULA>
						<GDESC></GDESC>
						<!-- Total MINI Premium -->
						<STEP STEPID="38" CALC_ID="1038" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID38" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>MINIMUMPREMIUM</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="FINAL_MINIMUM_PREMIUM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
						<!-- Total Premium -->
						<STEP STEPID="39" CALC_ID="1039" PREFIX="" SUFIX="" ISCALCULATE="F">
							<xsl:attribute name="DESC">
								<xsl:call-template name="STEPID39" />
							</xsl:attribute>
							<PATH>0</PATH>
							<D_PATH></D_PATH>
							<L_PATH></L_PATH>
							<COMPONENT_TYPE>P</COMPONENT_TYPE>
							<COMPONENT_CODE>SUMTOTAL</COMPONENT_CODE>
							<COMP_ACT_PRE>
								<xsl:call-template name="FINALPREMIUM" />
							</COMP_ACT_PRE>
							<COMP_REMARKS></COMP_REMARKS>
							<COMP_EXT></COMP_EXT>
							<COM_EXT_AD></COM_EXT_AD>
						</STEP>
					</GROUP>
					<PRODUCTFORMULA>
						<!-- Formula for calculation of premium -->
						<GROUP1 GROUPID="10000">
							<VALUE>(@[CALC_ID=10000])</VALUE>
						</GROUP1>
						<GROUP1 GROUPID="10001">
							<VALUE>(@[CALC_ID=10001])</VALUE>
						</GROUP1>
					</PRODUCTFORMULA>
				</PRODUCT>
			</CALCULATION>
		</PREMIUM>
	</xsl:template>
	<!-- Start of factor templates-->
	<!-- ######################################### FINAL PREMIUM ################# -->
	<xsl:template name="FINALPREMIUM">
		<xsl:variable name="FINAL_PREMIUM">
			<xsl:choose>
				<!-- For Michigan -Personal Automobile -->
				<xsl:when test="POLICY/STATENAME ='MICHIGAN'">
					<xsl:call-template name="FINALPREMIUM_PERSONAL_MICHIGAN" />
				</xsl:when>
				<!-- For Indiana -Personal Automobile -->
				<xsl:when test="POLICY/STATENAME ='INDIANA'">
					<xsl:call-template name="FINALPREMIUM_PERSONAL_INDIANA" />
				</xsl:when>
				<xsl:otherwise>0.00</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:value-of select="$FINAL_PREMIUM" />
	</xsl:template>
	<!-- ######################################### FINAL MINIMUM PREMIUM ################# -->
	<xsl:template name="FINAL_MINIMUM_PREMIUM">
		<xsl:call-template name="MINIMUM_PREMIUM" />
	</xsl:template>
	<xsl:template name="MINIMUM_PREMIUM">
		<xsl:variable name="MIN_PREMIUM">
			<xsl:choose>
				<!-- For Michigan -Personal Automobile -->
				<xsl:when test="POLICY/STATENAME ='MICHIGAN'">
					<xsl:call-template name="FINALPREMIUM_PERSONAL_MICHIGAN" />
				</xsl:when>
				<!-- For Indiana -Personal Automobile -->
				<xsl:when test="POLICY/STATENAME ='INDIANA'">
					<xsl:call-template name="FINALPREMIUM_PERSONAL_INDIANA" />
				</xsl:when>
				<xsl:otherwise>0.00</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<!-- Fetch the Min Premuim -->
		<xsl:variable name="VAR_MIN_PREMIUM_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_VEHICLE_PREMIUM_COMP_ONLY']/NODE[@ID='MINIMUM_PREMIUM']/ATTRIBUTES/@MIN_PREMIUM" />
		<xsl:choose>
			<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE_SCO)=$VT_SUSPENDED_COMP and $MIN_PREMIUM &lt;=$VAR_MIN_PREMIUM_VALUE">
				<xsl:text>Included</xsl:text>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- For Michigan -Personal Automobile -->
	<xsl:template name="FINALPREMIUM_PERSONAL_MICHIGAN">
		<xsl:variable name="VAR1">
			<xsl:choose>
				<xsl:when test="VEHICLES/VEHICLE/BI != 0 or VEHICLES/VEHICLE/BI !='' or VEHICLES/VEHICLE/BI !='NO COVERAGE'">
					<xsl:variable name="VAR_BI">
						<xsl:call-template name="BI" />
					</xsl:variable>
					<xsl:variable name="VAR_PD">
						<xsl:call-template name="PD" />
					</xsl:variable>
					<xsl:value-of select="$VAR_BI + $VAR_PD" />
				</xsl:when>
				<xsl:otherwise>
					0.00
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:choose>
				<xsl:when test="VEHICLES/VEHICLE/CSL != 0 and VEHICLES/VEHICLE/CSL !='' and normalize-space(VEHICLES/VEHICLE/CSL) !='NO COVERAGE'">
					<xsl:call-template name="CSLBIPD" />
				</xsl:when>
				<xsl:otherwise>
					0.00
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR3">
			<xsl:call-template name="PPI" />
		</xsl:variable>
		<xsl:variable name="VAR4">
			<xsl:call-template name="PIP" />
		</xsl:variable>
		<xsl:variable name="VAR5">
			<xsl:call-template name="UM" />
		</xsl:variable>
		<xsl:variable name="VAR6">
			<xsl:choose>
				<xsl:when test="VEHICLES/VEHICLE/ISUNDERINSUREDMOTORISTS ='TRUE'">
					<xsl:call-template name="UIM" />
				</xsl:when>
				<xsl:otherwise>
				0.00
			</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR7">
			<xsl:call-template name="COMP" />
		</xsl:variable>
		<xsl:variable name="VAR8">
			<xsl:choose>
				<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONTYPE ='LIMITED'">
					<xsl:call-template name="LTDCOLL" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:call-template name="COLL" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR9">
			<xsl:call-template name="MINITORT" />
		</xsl:variable>
		<xsl:variable name="VAR10">
			<xsl:call-template name="ROADSERVICES" />
		</xsl:variable>
		<xsl:variable name="VAR11">
			<xsl:call-template name="RENTALREIMBURSEMENT" />
		</xsl:variable>
		<xsl:variable name="VAR12">
			<xsl:call-template name="LOANLEASEGAP" />
		</xsl:variable>
		<xsl:variable name="VAR13">
			<xsl:call-template name="SOUNDREPRODUCINGTAPES" />
		</xsl:variable>
		<xsl:variable name="VAR14">
			<xsl:call-template name="SOUNDRECVTRANSEQUIPMENT" />
		</xsl:variable>
		<xsl:variable name="VAR15">
			<xsl:call-template name="MCCAFEE" />
		</xsl:variable>
		<xsl:variable name="VAR16">
			<xsl:call-template name="EXTRAEQUIPMENTCOMP" />
		</xsl:variable>
		<xsl:variable name="VAR17">
			<xsl:call-template name="EXTRAEQUIPMENTCOLL" />
		</xsl:variable>
		<xsl:variable name="VAR18">
			<xsl:call-template name="MCCHAFEE" />
		</xsl:variable>
		<xsl:variable name="VAR19">
			<xsl:call-template name="ENOLIABILITY_PREMIUM" />
		</xsl:variable>
		<xsl:value-of select="$VAR1 + $VAR2 +  $VAR3 + $VAR4 + $VAR5 + $VAR6 + $VAR7 + $VAR8 + $VAR9 + $VAR10 + $VAR11 + $VAR12 + $VAR13 + $VAR14 + $VAR15 + $VAR16 + $VAR17 + $VAR18 + $VAR19" />
	</xsl:template>
	<!-- End of Michigan -Personal Automobile -->
	<!-- For Indiana -Personal Automobile -->
	<xsl:template name="FINALPREMIUM_PERSONAL_INDIANA">
		<xsl:choose>
			<!--  When vehicle type is suspended comp then only comprehensive will be calculated    -->
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_SUSPENDED_COMP or VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO' ">
				<xsl:call-template name="COMP" />
			</xsl:when>
			<xsl:otherwise>
				<!--  If it is combined single limit then BI and PD will not be calculated separately. -->
				<xsl:variable name="VAR1">
					<xsl:choose>
						<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL='' or VEHICLES/VEHICLE/CSL='NO COVERAGE'">
							<xsl:variable name="VAR_BI">
								<xsl:call-template name="BI" />
							</xsl:variable>
							<xsl:variable name="VAR_PD">
								<xsl:call-template name="PD" />
							</xsl:variable>
							<xsl:value-of select="$VAR_BI+$VAR_PD" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:call-template name="CSLBIPD" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="MEDPAY" />
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="UM" />
				</xsl:variable>
				<xsl:variable name="VAR4">
					<!-- Add UIM only if IsUnderInsuredMotorist is true   -->
					<xsl:choose>
						<xsl:when test="VEHICLES/VEHICLE/ISUNDERINSUREDMOTORISTS ='FALSE'">
							0.00
						</xsl:when>
						<xsl:otherwise>
							<xsl:call-template name="UIM" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="UMPD" />
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="COMP" />
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:choose>
						<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONTYPE !='' and VEHICLES/VEHICLE/COVGCOLLISIONTYPE ='LIMITED'">
							<xsl:call-template name="LTDCOLL" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:call-template name="COLL" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:variable name="VAR8">
					<xsl:call-template name="ROADSERVICES" />
				</xsl:variable>
				<xsl:variable name="VAR9">
					<xsl:call-template name="RENTALREIMBURSEMENT" />
				</xsl:variable>
				<xsl:variable name="VAR10">
					<xsl:call-template name="LOANLEASEGAP" />
				</xsl:variable>
				<xsl:variable name="VAR11">
					<xsl:call-template name="SOUNDREPRODUCINGTAPES" />
				</xsl:variable>
				<xsl:variable name="VAR12">
					<xsl:call-template name="SOUNDRECVTRANSEQUIPMENT" />
				</xsl:variable>
				<xsl:variable name="VAR13">
					<xsl:call-template name="EXTRAEQUIPMENTCOMP" />
				</xsl:variable>
				<xsl:variable name="VAR14">
					<xsl:call-template name="EXTRAEQUIPMENTCOLL" />
				</xsl:variable>
				<xsl:variable name="VAR15">
					<xsl:call-template name="ENOLIABILITY_PREMIUM" />
				</xsl:variable>
				<xsl:value-of select="$VAR1+$VAR2+$VAR3+$VAR4+$VAR5+$VAR6+$VAR7+$VAR8+$VAR9+$VAR10+$VAR11+$VAR12+$VAR13+$VAR14+$VAR15" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of Indiana -Personal Automobile -->
	<!-- ######################################### END OF FINAL PREMIUM ################# -->
	<!-- Start of BI AND PD -->
	<xsl:template name="CSLBIPD">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name="CSLBIPD_PERSONAL_MICHIGAN" />
			</xsl:when>
			<!-- For Indiana -Personal Automobile -->
			<xsl:when test="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name="CSLBIPD_PERSONAL_INDIANA" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="CSLBIPD_PERSONAL_MICHIGAN">
		<xsl:choose>
			<!--   Suspended comp will return 0  -->
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_SUSPENDED_COMP or VEHICLES/VEHICLE/VEHICLETYPE =$VT_CAMPER_TRAVEL_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL='' or VEHICLES/VEHICLE/CSL='NO COVERAGE' or VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO' ">
			0.00
		</xsl:when>
			<xsl:otherwise>
				<!--
		        calculation formula below
		 		(
		 			(1)+(2)
		 		)
		 		*(3)*(4)*(5)*(6)*(7)*(8)*(9)*(10)
			-->
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="TYPEOFVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="MOTORHOME" />
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="CLASS"> <!--	  Driver Clss Relativity   -->
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR6"> <!-- for limit relativity equivalent to limit template -->
					<xsl:call-template name="CSLRELATIVITY" />
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="MULTIVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR8">
					<xsl:call-template name="VEHICLEUSE" />
				</xsl:variable>
				<xsl:variable name="VAR9">
					<xsl:call-template name="DRIVERDISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR10">
					<xsl:call-template name="GOODSTUDENT" />
				</xsl:variable>
				<xsl:variable name="VAR11">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR12">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<xsl:variable name="VAR13">
					<xsl:call-template name="MULTIPOLICY" />
				</xsl:variable>
				<!--xsl:value-of select="round(round(round(round(round(round(round(round(round(round(round(($VAR1+$VAR2)*$VAR11)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR12)*$VAR13)" /-->
				<xsl:variable name="FIRST_STEP" select="($VAR1+$VAR2)" />
				<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
				<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR11" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
				<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR3" />
				<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
				<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR4" />
				<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
				<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR5" />
				<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
				<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR6" />
				<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
				<xsl:variable name="SEVEN_STEP" select="$VARSIX6 * $VAR7" />
				<xsl:variable name="VARSEV7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
				<xsl:variable name="EIGHT_STEP" select="$VARSEV7 * $VAR8" />
				<xsl:variable name="VAREIG8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
				<xsl:variable name="NINE_STEP" select="$VAREIG8 * $VAR9" />
				<xsl:variable name="VARNIN9" select="round(format-number($NINE_STEP,'##.0000'))" />
				<xsl:variable name="TENTH_STEP" select="$VARNIN9 * $VAR10" />
				<xsl:variable name="VARTEN10" select="round(format-number($TENTH_STEP,'##.0000'))" />
				<xsl:variable name="ELEVEN_STEP" select="$VARTEN10 * $VAR12" />
				<xsl:variable name="VARELE11" select="round(format-number($ELEVEN_STEP,'##.0000'))" />
				<xsl:variable name="TWELVE_STEP" select="$VARELE11 * $VAR13" />
				<xsl:variable name="VARTWE12" select="round(format-number($TWELVE_STEP,'##.0000'))" />
				<xsl:value-of select="round($VARTWE12)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="CSLBIPD_PERSONAL_INDIANA">
		<xsl:choose>
			<!--   Suspended comp will return 0  -->
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_SUSPENDED_COMP or VEHICLES/VEHICLE/VEHICLETYPE =$VT_CAMPER_TRAVEL_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL='' or VEHICLES/VEHICLE/CSL='NO COVERAGE' or VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO' ">
			0.00
		</xsl:when>
			<xsl:otherwise>
				<!--
		        calculation formula below
		 		(
		 			(1)+(2)
		 		)
		 		*(3)*(4)*(5)*(6)*(7)*(8)*(9)*(10)
			-->
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="TYPEOFVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="MOTORHOME" />
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="CLASS"> <!--	  Driver Clss Relativity   -->
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR6"> <!-- for limit relativity equivalent to limit template -->
					<xsl:call-template name="CSLRELATIVITY" />
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="MULTIVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR8">
					<xsl:call-template name="VEHICLEUSE" />
				</xsl:variable>
				<xsl:variable name="VAR9">
					<xsl:call-template name="DRIVERDISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR10">
					<xsl:call-template name="GOODSTUDENT" />
				</xsl:variable>
				<xsl:variable name="VAR11">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR12">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<xsl:variable name="VAR13">
					<xsl:call-template name="MULTIPOLICY" />
				</xsl:variable>
				<xsl:variable name="VAR14">
					<xsl:call-template name="TRAILBLAZER">
						<xsl:with-param name="FACTORELEMENT" select="'BI'" />
					</xsl:call-template>
				</xsl:variable>
				<!--xsl:value-of select="round(round(round(round(round(round(round(round(round(round(round(round(($VAR1+$VAR2)*$VAR14)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)*$VAR13)" /-->
				<xsl:variable name="FIRST_STEP" select="($VAR1+$VAR2)" />
				<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
				<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR14" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
				<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR3" />
				<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
				<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR4" />
				<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
				<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR5" />
				<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
				<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR6" />
				<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
				<xsl:variable name="SEVEN_STEP" select="$VARSIX6 * $VAR7" />
				<xsl:variable name="VARSEV7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
				<xsl:variable name="EIGHT_STEP" select="$VARSEV7 * $VAR8" />
				<xsl:variable name="VAREIG8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
				<xsl:variable name="NINE_STEP" select="$VAREIG8 * $VAR9" />
				<xsl:variable name="VARNIN9" select="round(format-number($NINE_STEP,'##.0000'))" />
				<xsl:variable name="TENTH_STEP" select="$VARNIN9 * $VAR10" />
				<xsl:variable name="VARTEN10" select="round(format-number($TENTH_STEP,'##.0000'))" />
				<xsl:variable name="ELEVEN_STEP" select="$VARTEN10 * $VAR11" />
				<xsl:variable name="VARELE11" select="round(format-number($ELEVEN_STEP,'##.0000'))" />
				<xsl:variable name="TWELVE_STEP" select="$VARELE11 * $VAR12" />
				<xsl:variable name="VARTWE12" select="round(format-number($TWELVE_STEP,'##.0000'))" />
				<xsl:variable name="THERTEEN_STEP" select="$VARTWE12 * $VAR13" />
				<xsl:variable name="VARTHER13" select="round(format-number($THERTEEN_STEP,'##.0000'))" />
				<xsl:value-of select="round($VARTHER13)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Bodily Injury -->
	<xsl:template name="BI">
		<xsl:choose>
			<!-- For Michigan -Personal Automobile -->
			<xsl:when test="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name="BI_PERSONAL_MICHIGAN" />
			</xsl:when>
			<!-- For Indiana -Personal Automobile -->
			<xsl:when test="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name="BI_PERSONAL_INDIANA" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Start of BI Personal Michigan -->
	<xsl:template name="BI_PERSONAL_MICHIGAN">
		<!--  ADDED BY PRAVEEN SINGH  -->
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_SUSPENDED_COMP or VEHICLES/VEHICLE/VEHICLETYPE =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE =$VT_CAMPER_TRAVEL_TRAILER or   VEHICLES/VEHICLE/BI ='NO COVERAGE' or VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO' or  VEHICLES/VEHICLE/BI = '' or  VEHICLES/VEHICLE/BI = '0/0'">
			0.00
		</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL='' or VEHICLES/VEHICLE/CSL = 'NO COVERAGE'">
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="OPTIMAL" />
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="TYPEOFVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="LIMIT">
						<xsl:with-param name="FACTORELEMENT" select="'BI'" />
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="MULTIVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR8">
					<xsl:call-template name="VEHICLEUSE" />
				</xsl:variable>
				<xsl:variable name="VAR9">
					<xsl:call-template name="DRIVERDISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR10">
					<xsl:call-template name="GOODSTUDENT" />
				</xsl:variable>
				<xsl:variable name="VAR11">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<xsl:variable name="VAR12">
					<xsl:call-template name="MULTIPOLICY" />
				</xsl:variable>
				<!--xsl:value-of select="round(round(round(round(round(round(round(round(round(round(round(round($VAR1)*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)" /-->
				<xsl:variable name="FIRST_STEP" select="($VAR1*$VAR2)" />
				<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
				<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR3" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
				<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR4" />
				<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
				<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR5" />
				<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
				<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR6" />
				<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
				<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR7" />
				<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
				<xsl:variable name="SEVEN_STEP" select="$VARSIX6 * $VAR8" />
				<xsl:variable name="VARSEV7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
				<xsl:variable name="EIGHT_STEP" select="$VARSEV7 * $VAR9" />
				<xsl:variable name="VAREIG8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
				<xsl:variable name="NINE_STEP" select="$VAREIG8 * $VAR10" />
				<xsl:variable name="VARNIN9" select="round(format-number($NINE_STEP,'##.0000'))" />
				<xsl:variable name="TENTH_STEP" select="$VARNIN9 * $VAR11" />
				<xsl:variable name="VARTEN10" select="round(format-number($TENTH_STEP,'##.0000'))" />
				<xsl:variable name="ELEVEN_STEP" select="$VARTEN10 * $VAR12" />
				<xsl:variable name="VARELE11" select="round(format-number($ELEVEN_STEP,'##.0000'))" />
				<xsl:value-of select="round($VARELE11)" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of BI Personal Michigan -->
	<xsl:template name="BI_PERSONAL_INDIANA">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_SUSPENDED_COMP or VEHICLES/VEHICLE/VEHICLETYPE =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE =$VT_CAMPER_TRAVEL_TRAILER or  VEHICLES/VEHICLE/BI ='NO COVERAGE' or  VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO' or  VEHICLES/VEHICLE/BI = '' or  VEHICLES/VEHICLE/BI = '0/0'">
				0.00
			</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL='' or normalize-space(VEHICLES/VEHICLE/CSL)='NO COVERAGE' ">
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="TYPEOFVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="MOTORHOME" />
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="LIMIT">
						<xsl:with-param name="FACTORELEMENT" select="'BI'" />
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="MULTIVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="VEHICLEUSE" />
				</xsl:variable>
				<xsl:variable name="VAR8">
					<xsl:call-template name="DRIVERDISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR9">
					<xsl:call-template name="GOODSTUDENT" />
				</xsl:variable>
				<xsl:variable name="VAR10">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR11">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<xsl:variable name="VAR12">
					<xsl:call-template name="MULTIPOLICY" />
				</xsl:variable>
				<xsl:variable name="VAR13">
					<xsl:call-template name="TRAILBLAZER">
						<xsl:with-param name="FACTORELEMENT" select="'BI'" />
					</xsl:call-template>
				</xsl:variable>
				<!--xsl:value-of select="round(round(round(round(round(round(round(round(round(round(round(round(round($VAR1)*$VAR13)*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)" /-->
				<xsl:variable name="FIRST_STEP" select="($VAR1*$VAR13)" />
				<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
				<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR2" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
				<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR3" />
				<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
				<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR4" />
				<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
				<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR5" />
				<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
				<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR6" />
				<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
				<xsl:variable name="SEVEN_STEP" select="$VARSIX6 * $VAR7" />
				<xsl:variable name="VARSEV7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
				<xsl:variable name="EIGHT_STEP" select="$VARSEV7 * $VAR8" />
				<xsl:variable name="VAREIG8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
				<xsl:variable name="NINE_STEP" select="$VAREIG8 * $VAR9" />
				<xsl:variable name="VARNIN9" select="round(format-number($NINE_STEP,'##.0000'))" />
				<xsl:variable name="TENTH_STEP" select="$VARNIN9 * $VAR10" />
				<xsl:variable name="VARTEN10" select="round(format-number($TENTH_STEP,'##.0000'))" />
				<xsl:variable name="ELEVEN_STEP" select="$VARTEN10 * $VAR11" />
				<xsl:variable name="VARELE11" select="round(format-number($ELEVEN_STEP,'##.0000'))" />
				<xsl:variable name="TWELVE_STEP" select="$VARELE11 * $VAR12" />
				<xsl:variable name="VARTWE12" select="round(format-number($TWELVE_STEP,'##.0000'))" />
				<xsl:value-of select="round($VARTWE12)" />
			</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of BI Commercial Michigan -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- ********************************  Start of Property Damage**************************************************** -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- Property Damage -->
	<xsl:template name="PD">
		<xsl:choose>
			<!-- For Michigan -Personal Automobile -->
			<xsl:when test="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name="PD_PERSONAL_MICHIGAN" />
			</xsl:when>
			<!-- For Indiana -Personal Automobile -->
			<xsl:when test="POLICY/STATENAME ='INDIANA' ">
				<xsl:call-template name="PD_PERSONAL_INDIANA" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Start of PD Personal Michigan -->
	<xsl:template name="PD_PERSONAL_MICHIGAN">
		<!-- new calculation added by praveen singh -->
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_SUSPENDED_COMP  or VEHICLES/VEHICLE/VEHICLETYPE =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE =$VT_CAMPER_TRAVEL_TRAILER or VEHICLES/VEHICLE/PD = 'NO COVERAGE' or VEHICLES/VEHICLE/PD = '' or VEHICLES/VEHICLE/PD = 0 or  VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO' or  VEHICLES/VEHICLE/PD = '0' or  VEHICLES/VEHICLE/PD = ''">
			0.00
		</xsl:when>
			<xsl:when test="normalize-space(VEHICLES/VEHICLE/CSL) = 0 or normalize-space(VEHICLES/VEHICLE/CSL)='' or normalize-space(VEHICLES/VEHICLE/CSL)='NO COVERAGE'">
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="OPTIMAL" />
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="TYPEOFVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="LIMIT">
						<xsl:with-param name="FACTORELEMENT" select="'PD'" />
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="MULTIVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR8">
					<xsl:call-template name="VEHICLEUSE" />
				</xsl:variable>
				<xsl:variable name="VAR9">
					<xsl:call-template name="DRIVERDISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR10">
					<xsl:call-template name="GOODSTUDENT" />
				</xsl:variable>
				<xsl:variable name="VAR11">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<xsl:variable name="VAR12">
					<xsl:call-template name="MULTIPOLICY" />
				</xsl:variable>
				<!--xsl:value-of select="round(round(round(round(round(round(round(round(round(round(round(round($VAR1)*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)" /-->
				<xsl:variable name="FIRST_STEP" select="($VAR1*$VAR2)" />
				<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
				<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR3" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
				<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR4" />
				<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
				<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR5" />
				<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
				<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR6" />
				<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
				<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR7" />
				<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
				<xsl:variable name="SEVEN_STEP" select="$VARSIX6 * $VAR8" />
				<xsl:variable name="VARSEV7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
				<xsl:variable name="EIGHT_STEP" select="$VARSEV7 * $VAR9" />
				<xsl:variable name="VAREIG8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
				<xsl:variable name="NINE_STEP" select="$VAREIG8 * $VAR10" />
				<xsl:variable name="VARNIN9" select="round(format-number($NINE_STEP,'##.0000'))" />
				<xsl:variable name="TENTH_STEP" select="$VARNIN9 * $VAR11" />
				<xsl:variable name="VARTEN10" select="round(format-number($TENTH_STEP,'##.0000'))" />
				<xsl:variable name="ELEVEN_STEP" select="$VARTEN10 * $VAR12" />
				<xsl:variable name="VARELE11" select="round(format-number($ELEVEN_STEP,'##.0000'))" />
				<xsl:value-of select="round($VARELE11)" />
			</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of Property Damage -Personal Michigan-->
	<!-- Start of PD Commercial Michigan -->
	<xsl:template name="PD_PERSONAL_INDIANA">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_SUSPENDED_COMP  or VEHICLES/VEHICLE/VEHICLETYPE =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE =$VT_CAMPER_TRAVEL_TRAILER or VEHICLES/VEHICLE/PD = 'NO COVERAGE' or  VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO' or  VEHICLES/VEHICLE/PD = '0' or  VEHICLES/VEHICLE/PD = ''">
			0.00
			</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL='' or normalize-space(VEHICLES/VEHICLE/CSL) = 'NO COVERAGE'">
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="TYPEOFVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="MOTORHOME" />
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="LIMIT">
						<xsl:with-param name="FACTORELEMENT" select="'PD'" />
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="MULTIVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="VEHICLEUSE" />
				</xsl:variable>
				<xsl:variable name="VAR8">
					<xsl:call-template name="DRIVERDISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR9">
					<xsl:call-template name="GOODSTUDENT" />
				</xsl:variable>
				<xsl:variable name="VAR10">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR11">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<xsl:variable name="VAR12">
					<xsl:call-template name="MULTIPOLICY" />
				</xsl:variable>
				<xsl:variable name="VAR13">
					<xsl:call-template name="TRAILBLAZER">
						<xsl:with-param name="FACTORELEMENT" select="'PD'" />
					</xsl:call-template>
				</xsl:variable>
				<!--xsl:value-of select="round(round(round(round(round(round(round(round(round(round(round(round(round($VAR1)*$VAR13)*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)" /-->
				<xsl:variable name="FIRST_STEP" select="($VAR1*$VAR13)" />
				<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
				<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR2" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
				<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR3" />
				<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
				<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR4" />
				<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
				<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR5" />
				<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
				<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR6" />
				<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
				<xsl:variable name="SEVEN_STEP" select="$VARSIX6 * $VAR7" />
				<xsl:variable name="VARSEV7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
				<xsl:variable name="EIGHT_STEP" select="$VARSEV7 * $VAR8" />
				<xsl:variable name="VAREIG8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
				<xsl:variable name="NINE_STEP" select="$VAREIG8 * $VAR9" />
				<xsl:variable name="VARNIN9" select="round(format-number($NINE_STEP,'##.0000'))" />
				<xsl:variable name="TENTH_STEP" select="$VARNIN9 * $VAR10" />
				<xsl:variable name="VARTEN10" select="round(format-number($TENTH_STEP,'##.0000'))" />
				<xsl:variable name="ELEVEN_STEP" select="$VARTEN10 * $VAR11" />
				<xsl:variable name="VARELE11" select="round(format-number($ELEVEN_STEP,'##.0000'))" />
				<xsl:variable name="TWELVE_STEP" select="$VARELE11 * $VAR12" />
				<xsl:variable name="VARTWE12" select="round(format-number($TWELVE_STEP,'##.0000'))" />
				<xsl:value-of select="round($VARTWE12)" />
			</xsl:when>
			<xsl:otherwise>
		0.00
	</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of Property Damage - Commercial Michigan-->
	<!-- ********************************************************************************************************************************************* -->
	<!-- ********************************  Start of PPI  **************************************************** -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- PPI -->
	<xsl:template name="PPI">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='MICHIGAN' and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_SUSPENDED_COMP  and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_UTILITY_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_CAMPER_TRAVEL_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO' ">
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'PPI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="OPTIMAL" />
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="TYPEOFVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'PPI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'PPI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="MULTIVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'PPI'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="VEHICLEUSE" />
				</xsl:variable>
				<xsl:variable name="VAR8">
					<xsl:call-template name="DRIVERDISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR9">
					<xsl:call-template name="GOODSTUDENT" />
				</xsl:variable>
				<xsl:variable name="VAR10">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<xsl:variable name="VAR11">
					<xsl:call-template name="MULTIPOLICY" />
				</xsl:variable>
				<!--xsl:value-of select="round(round(round(round(round(round(round(round(round(round(round($VAR1)*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)" /-->
				<xsl:variable name="FIRST_STEP" select="($VAR1*$VAR2)" />
				<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
				<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR3" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
				<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR4" />
				<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
				<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR5" />
				<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
				<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR6" />
				<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
				<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR7" />
				<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
				<xsl:variable name="SEVEN_STEP" select="$VARSIX6 * $VAR8" />
				<xsl:variable name="VARSEV7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
				<xsl:variable name="EIGHT_STEP" select="$VARSEV7 * $VAR9" />
				<xsl:variable name="VAREIG8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
				<xsl:variable name="NINE_STEP" select="$VAREIG8 * $VAR10" />
				<xsl:variable name="VARNIN9" select="round(format-number($NINE_STEP,'##.0000'))" />
				<xsl:variable name="TENTH_STEP" select="$VARNIN9 * $VAR11" />
				<xsl:variable name="VARTEN10" select="round(format-number($TENTH_STEP,'##.0000'))" />
				<xsl:value-of select="round($VARTEN10)" />
			</xsl:when>
			<xsl:otherwise>
		0.00
	</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of PPI -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- *************************************  Start of PIP ******************************************************************* -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="PIP">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE !=$VT_SUSPENDED_COMP  and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_UTILITY_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_CAMPER_TRAVEL_TRAILER  and POLICY/STATENAME ='MICHIGAN' and VEHICLES/VEHICLE/MEDPM != '' and VEHICLES/VEHICLE/MEDPM != '0' and VEHICLES/VEHICLE/MEDPM != 'NO COVERAGE' and  VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO'">
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'PIP'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="OPTIMAL" />
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="TYPEOFVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'PIP'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'PIP'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="COVERAGETYPE" />
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="SEATBELT" />
				</xsl:variable>
				<xsl:variable name="VAR8">
					<xsl:call-template name="AIRBAG" />
				</xsl:variable>
				<xsl:variable name="VAR9">
					<xsl:call-template name="MULTIVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'PIP'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR10">
					<xsl:call-template name="VEHICLEUSE" />
				</xsl:variable>
				<xsl:variable name="VAR11">
					<xsl:call-template name="DRIVERDISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR12">
					<xsl:call-template name="GOODSTUDENT" />
				</xsl:variable>
				<xsl:variable name="VAR13">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<xsl:variable name="VAR14">
					<xsl:call-template name="MULTIPOLICY" />
				</xsl:variable>
				<!--xsl:value-of select="round(round(round(round(round(round(round(round(round(round(round(round(round($VAR1*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)*$VAR13)*$VAR14)" /-->
				<xsl:variable name="FIRST_STEP" select="($VAR1*$VAR2)" />
				<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
				<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR3" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
				<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR4" />
				<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
				<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR5" />
				<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
				<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR6" />
				<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
				<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR7" />
				<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
				<xsl:variable name="SEVEN_STEP" select="$VARSIX6 * $VAR8" />
				<xsl:variable name="VARSEV7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
				<xsl:variable name="EIGHT_STEP" select="$VARSEV7 * $VAR9" />
				<xsl:variable name="VAREIG8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
				<xsl:variable name="NINE_STEP" select="$VAREIG8 * $VAR10" />
				<xsl:variable name="VARNIN9" select="round(format-number($NINE_STEP,'##.0000'))" />
				<xsl:variable name="TENTH_STEP" select="$VARNIN9 * $VAR11" />
				<xsl:variable name="VARTEN10" select="round(format-number($TENTH_STEP,'##.0000'))" />
				<xsl:variable name="ELEVEN_STEP" select="$VARTEN10 * $VAR12" />
				<xsl:variable name="VARELE11" select="round(format-number($ELEVEN_STEP,'##.0000'))" />
				<xsl:variable name="TWELVE_STEP" select="$VARELE11 * $VAR13" />
				<xsl:variable name="VARTWE12" select="round(format-number($TWELVE_STEP,'##.0000'))" />
				<xsl:variable name="THRTEEN_STEP" select="$VARTWE12 * $VAR14" />
				<xsl:variable name="VARTHR13" select="format-number($THRTEEN_STEP,'##.0000')" />
				<xsl:value-of select="round($VARTHR13)" />
			</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of PIP -->
	<!--End of PIP -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- *************************************  Start of Medical  Payment ******************************************************************* -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- Medical  Payment -->
	<xsl:template name="MEDPAY">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME = 'INDIANA' and VEHICLES/VEHICLE/MPLIMIT !='0'  and VEHICLES/VEHICLE/MPLIMIT !='' and VEHICLES/VEHICLE/MPLIMIT !='NO COVERAGE' and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_SUSPENDED_COMP and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_UTILITY_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_CAMPER_TRAVEL_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO'">
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="MOTORHOME" />
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="TYPEOFVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="LIMIT">
						<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'" />
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="AIRBAG" />
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="MULTIVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR8">
					<xsl:call-template name="VEHICLEUSE" />
				</xsl:variable>
				<xsl:variable name="VAR9">
					<xsl:call-template name="DRIVERDISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR10">
					<xsl:call-template name="GOODSTUDENT" />
				</xsl:variable>
				<xsl:variable name="VAR11">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR12">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<xsl:variable name="VAR13">
					<xsl:call-template name="MULTIPOLICY" />
				</xsl:variable>
				<xsl:variable name="VAR14">
					<xsl:call-template name="TRAILBLAZER">
						<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'" />
					</xsl:call-template>
				</xsl:variable>
				<!--xsl:value-of select="round(round(round(round(round(round(round(round(round(round(round(round(round($VAR1*$VAR14)*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)*$VAR13)" /-->
				<xsl:variable name="FIRST_STEP" select="($VAR1*$VAR14)" />
				<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
				<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR2" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
				<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR3" />
				<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
				<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR4" />
				<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
				<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR5" />
				<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
				<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR6" />
				<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
				<xsl:variable name="SEVEN_STEP" select="$VARSIX6 * $VAR7" />
				<xsl:variable name="VARSEV7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
				<xsl:variable name="EIGHT_STEP" select="$VARSEV7 * $VAR8" />
				<xsl:variable name="VAREIG8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
				<xsl:variable name="NINE_STEP" select="$VAREIG8 * $VAR9" />
				<xsl:variable name="VARNIN9" select="round(format-number($NINE_STEP,'##.0000'))" />
				<xsl:variable name="TENTH_STEP" select="$VARNIN9 * $VAR10" />
				<xsl:variable name="VARTEN10" select="round(format-number($TENTH_STEP,'##.0000'))" />
				<xsl:variable name="ELEVEN_STEP" select="$VARTEN10 * $VAR11" />
				<xsl:variable name="VARELE11" select="round(format-number($ELEVEN_STEP,'##.0000'))" />
				<xsl:variable name="TWELVE_STEP" select="$VARELE11 * $VAR12" />
				<xsl:variable name="VARTWE12" select="round(format-number($TWELVE_STEP,'##.0000'))" />
				<xsl:variable name="THERTEEN_STEP" select="$VARTWE12 * $VAR13" />
				<xsl:variable name="VARTWE13" select="round(format-number($THERTEEN_STEP,'##.0000'))" />
				<xsl:value-of select="round($VARTWE13)" />
			</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of Medical  Payment -->
	<!-- ******************************************************************************************************************* -->
	<!-- *************************************  Start of Motor Home  ******************************************************************* -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="MOTORHOME">
		<xsl:choose>
			<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_MOTORHOME"> <!-- or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_CUSTOMIZED_VAN"-->
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MOTORHOME']/NODE[@ID ='MOTORHOME_DISCOUNT']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- *************************************  Start of Uninsured Motorists  ******************************************************************* -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- Uninsured Motorists -->
	<xsl:template name="UM">
		<xsl:choose>
			<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) =$VT_SUSPENDED_COMP  or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) =$VT_CAMPER_TRAVEL_TRAILER    or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO'">
				0.00
			</xsl:when>
			<!-- For Michigan -Personal Automobile -->
			<xsl:when test="normalize-space(POLICY/STATENAME) ='MICHIGAN'">
				<xsl:call-template name="UM_PERSONAL_MICHIGAN" />
			</xsl:when>
			<!-- For Indiana -Personal Automobile -->
			<xsl:when test="normalize-space(POLICY/STATENAME) ='INDIANA'">
				<xsl:call-template name="UM_PERSONAL_INDIANA" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) !=$VT_UTILITY_TRAILER">
						<xsl:if test="normalize-space(POLICY/STATENAME) ='MICHIGAN'">
							<xsl:call-template name="UM_PERSONAL_MICHIGAN" />
						</xsl:if>
						<xsl:if test="normalize-space(POLICY/STATENAME) ='INDIANA'">
							<xsl:call-template name="UM_PERSONAL_INDIANA" />
						</xsl:if>
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="UM_PERSONAL_MICHIGAN">
		<xsl:variable name="VAR1">
			<xsl:call-template name="TERRITORYBASE">
				<xsl:with-param name="FACTORELEMENT" select="'UM'"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<xsl:variable name="VAR2">
			<xsl:choose>
				<xsl:when test="VEHICLES/VEHICLE/TYPE ='NO COVERAGE' or VEHICLES/VEHICLE/TYPE =''">  
					0.00
				</xsl:when>
				<xsl:when test="(VEHICLES/VEHICLE/UMSPLIT !='' and VEHICLES/VEHICLE/UMSPLIT !='0/0' and VEHICLES/VEHICLE/UMSPLIT !='NO COVERAGE' and VEHICLES/VEHICLE/UMSPLIT != '0') ">
					<xsl:call-template name="LIMIT">
						<xsl:with-param name="FACTORELEMENT" select="'UM_MICHIGAN_SPLIT'" />
					</xsl:call-template>
				</xsl:when>
				<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL !='0' and VEHICLES/VEHICLE/UMCSL !='NO COVERAGE' ">
					<xsl:call-template name="LIMIT">
						<xsl:with-param name="FACTORELEMENT" select="'UM_MICHIGAN_CSL'" />
					</xsl:call-template>
				</xsl:when>
				<xsl:otherwise>
					0.00
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_TYPEOFVEHICLE">
			<xsl:call-template name="TYPEOFVEHICLE">
				<xsl:with-param name="FACTORELEMENT" select="'UM'"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<xsl:value-of select="$VAR1*$VAR2*$VAR_TYPEOFVEHICLE" />
	</xsl:template>
	<xsl:template name="UM_PERSONAL_INDIANA">
		<!-- Check if  it is 
			1. No coverage
			2. BI Only
			3. BI and PD -->
		<xsl:variable name="VAR1">
			<xsl:choose>
				<xsl:when test="(VEHICLES/VEHICLE/UMSPLIT !='' and VEHICLES/VEHICLE/UMSPLIT !='0' and VEHICLES/VEHICLE/UMSPLIT !='0/0' and VEHICLES/VEHICLE/UMSPLIT !='NO COVERAGE') "> <!-- Split Limit  	-->
					<xsl:call-template name="LIMIT">
						<xsl:with-param name="FACTORELEMENT" select="'UM_INDIANA_SPLIT'" />
					</xsl:call-template>
				</xsl:when>
				<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL !='0' and VEHICLES/VEHICLE/UMCSL !='NO COVERAGE' "> <!-- Split Limit -->
					<xsl:call-template name="LIMIT">
						<xsl:with-param name="FACTORELEMENT" select="'UM_INDIANA_CSL'" />
					</xsl:call-template>
				</xsl:when>
				<xsl:otherwise>
				0.00
			</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_TYPEOFVEHICLE">
			<xsl:call-template name="TYPEOFVEHICLE">
				<xsl:with-param name="FACTORELEMENT" select="'UM'"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		<xsl:value-of select="$VAR1*$VAR_TYPEOFVEHICLE" />
	</xsl:template>
	<!--Start of UM Property Damage -->
	<xsl:template name="UMPD">
		<xsl:variable name="VAR_PDLIMIT" select="translate(VEHICLES/VEHICLE/PDLIMIT,',','')" />
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA' and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_SUSPENDED_COMP and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_UTILITY_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_CAMPER_TRAVEL_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO'">
				<xsl:variable name="VAR1">
					<xsl:choose>
						<xsl:when test="VEHICLES/VEHICLE/TYPE ='NO COVERAGE' or VEHICLES/VEHICLE/TYPE =''"> <!-- No Coverage -->
					0.00
					</xsl:when>
						<xsl:when test="(VEHICLES/VEHICLE/TYPE != '' and VEHICLES/VEHICLE/TYPE !='BI ONLY' and $VAR_PDLIMIT &gt; 0)"> <!-- UMPD -->
							<xsl:variable name="PPDDEDUCTIBLE" select="VEHICLES/VEHICLE/PDDEDUCTIBLE" />
							<xsl:variable name="PPDLIMIT" select="VEHICLES/VEHICLE/PDLIMIT" />
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMPDLIMITS']/NODE[@ID='UMPD']/ATTRIBUTES[@LIMIT =$VAR_PDLIMIT and @DEDUCTIBLE =$PPDDEDUCTIBLE]/@VALUE" />
						</xsl:when>
						<xsl:otherwise>
					0.00
					</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:variable name="VAR_TYPEOFVEHICLE">
					<xsl:call-template name="TYPEOFVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'UMPD'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:value-of select="$VAR1*$VAR_TYPEOFVEHICLE" />
			</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of Uninsured Motorists-->
	<!-- ********************************************************************************************************************************************* -->
	<!-- **********************************  Start of Under Insured Motorists  ******************************************************************* -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- Underinsured Motorists -->
	<xsl:template name="UIM">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = $VT_SUSPENDED_COMP or VEHICLES/VEHICLE/VEHICLETYPE =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE =$VT_CAMPER_TRAVEL_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO'">
			0.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR1">
					<!-- <xsl:call-template name ="UIM_BASE"/> -->
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'UIM'" />
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="LIMIT">
						<xsl:with-param name="FACTORELEMENT" select="'UIM'" />
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR_TYPEOFVEHICLE">
					<xsl:call-template name="TYPEOFVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'UIM'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:value-of select="$VAR1*$VAR2*$VAR_TYPEOFVEHICLE" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="UIM_BASE">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/BI !='' and VEHICLES/VEHICLE/BI !='0'">
				<xsl:variable name="LOWER_LIMIT" select="VEHICLES/VEHICLE/BILIMIT1" />
				<xsl:variable name="UPPER_LIMIT" select="VEHICLES/VEHICLE/BILIMIT2" />
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UIMBILIMITS']/NODE[@ID='UIMBI']/ATTRIBUTES[@MINLIMIT=$LOWER_LIMIT and @MAXLIMIT=$UPPER_LIMIT ]/@UIMRATES" />
			</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/BI  = '' and VEHICLES/VEHICLE/CSL !=''">
				<xsl:variable name="VAR_UIM_CSL" select="VEHICLES/VEHICLE/CSL" />
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUIMBILIMITS']/NODE[@ID='CSLUIMBI']/ATTRIBUTES[@CSLUIMBILIMITS=$VAR_UIM_CSL ]/@CSLUIMRATES" />
			</xsl:when>
			<xsl:otherwise>
				1.00
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of Underinsured Motorists  -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- ************************** Start of Comprehensive For Indiana and Michigan ******************************************************************* -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- Comprehensive -->
	<xsl:template name="COMP">
		<xsl:choose>
			<!-- For Michigan -Personal Automobile -->
			<xsl:when test="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name="COMP_PERSONAL_MICHIGAN" />
			</xsl:when>
			<!-- For Indiana -Personal Automobile -->
			<xsl:when test="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name="COMP_PERSONAL_INDIANA" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- this template have been made on temprory basis that will be removed on clerification  Neeraj Singh -->
	<xsl:template name="COMP_PERSONAL_MICHIGAN">
		<xsl:variable name="VAR_COST" select="VEHICLES/VEHICLE/COST" />
		<!-- Approach different for Travel Campers -->
		<xsl:variable name="COMP_PREMIUM">
			<xsl:choose>
				<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_CAMPER_TRAVEL_TRAILER or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_UTILITY_TRAILER ">
					<xsl:choose>
						<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE !='' and VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE &gt; 0">
							<!-- Get the comprehensive amount against cost,age and multiply by deductible factor -->
							<xsl:variable name="VAR_AGE" select="VEHICLES/VEHICLE/AGE" />
							<xsl:variable name="VAR_DEDUCTIBLE" select="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE" />
							<!-- Fetch the base amount for the resp age category -->
							<xsl:variable name="VAR_COMM_BASE_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COMPREHENSIVE_BASE_VALUE']/ATTRIBUTES[@AGE_MIN &lt;= $VAR_AGE and @AGE_MAX &gt;= $VAR_AGE]/@BASE_VALUE" />
							<!-- Fetch the max limit present -->
							<xsl:variable name="VAR_MAX_COST_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COMPREHENSIVE']/@MAX_COST" />
							<!-- Fetch the deductible factor -->
							<xsl:variable name="VAR_DEDUCTIBE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COMPREHENSIVE_DED_FACTOR']/ATTRIBUTES[@DEDUCTIBLE = $VAR_DEDUCTIBLE]/@FACTOR" />
							<!-- If the cost exceeds this max limit then -->
							<xsl:choose>
								<xsl:when test="$VAR_COST &gt;= $VAR_MAX_COST_VALUE">
									<!-- Fetch the factor against max limit -->
									<xsl:variable name="VAR_MAX_COST_VALUE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COMPREHENSIVE']/ATTRIBUTES[@COST_MAX = $VAR_MAX_COST_VALUE]/@COMP_RATE_FACTOR" />
									<!-- Fetch the differencr in amount -->
									<xsl:variable name="VAR_DIFFERENCE" select="$VAR_COST - $VAR_MAX_COST_VALUE" />
									<!-- Fetch the each additional amount -->
									<xsl:variable name="VAR_FOR_EACH_ADDTN_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COMPREHENSIVE']/ATTRIBUTES/@COST_ADDITIONAL" />
									<!-- Fetch the factor value for each additional amount -->
									<xsl:variable name="VAR_ADDTN_VALUE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COMPREHENSIVE']/ATTRIBUTES/@COMP_RATE_ADDTNL_FACTOR" />
									<!-- Prepare the formula e.g base value * ( 4.03+((32000-30000)/1000)*0.16)-->
									<xsl:value-of select="round(round($VAR_COMM_BASE_VALUE * ($VAR_MAX_COST_VALUE_FACTOR + (($VAR_DIFFERENCE div $VAR_FOR_EACH_ADDTN_VALUE) * $VAR_ADDTN_VALUE_FACTOR)))*$VAR_DEDUCTIBE_FACTOR)" />
								</xsl:when>
								<xsl:otherwise> <!-- Otherwise if the cost is below the max limit -->
									<!-- Fetch the resp factor -->
									<xsl:variable name="VAR_COMM_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COMPREHENSIVE']/ATTRIBUTES[@COST_MIN &lt;= $VAR_COST and @COST_MAX &gt;= $VAR_COST]/@COMP_RATE_FACTOR" />
									<!-- Multiply factor with  base value -->
									<xsl:value-of select="round(round($VAR_COMM_BASE_VALUE * $VAR_COMM_FACTOR)*$VAR_DEDUCTIBE_FACTOR)" />
								</xsl:otherwise>
							</xsl:choose>
						</xsl:when>
						<xsl:otherwise>0.00</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_CLASSIC_CAR or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_ANTIQUE_CAR  or VEHICLES/VEHICLE/VEHICLETYPE ='SA'">
					<xsl:choose>
						<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE !='' and VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE &gt; 0">
							<xsl:variable name="VAR1">
								<xsl:call-template name="TYPEOFVEHICLE">
									<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
								</xsl:call-template>
							</xsl:variable>
							<xsl:variable name="VAR2">
								<xsl:choose>
									<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='ANC'">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@BASERATECOMPCOLL" />
									</xsl:when>
									<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='CLC'">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@BASERATECOMPCOLL" />
									</xsl:when>
									<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SA'">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@BASERATECOMPCOLL" />
									</xsl:when>
								</xsl:choose>
							</xsl:variable>
							<xsl:variable name="VAR3">
								<xsl:call-template name="DRIVERDISCOUNT" />
							</xsl:variable>
							<xsl:variable name="VAR4">
								<xsl:call-template name="SURCHARGE" />
							</xsl:variable>
							<xsl:variable name="VAR5">
								<xsl:call-template name="MULTIPOLICY" />
							</xsl:variable>
							<xsl:value-of select="round(round(round(round($VAR1*($VAR_COST div $VAR2))*$VAR3)*$VAR4)*$VAR5)" />
						</xsl:when>
						<xsl:otherwise>0.00</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:otherwise>
					<!-- comprehensive michigan -->
					<xsl:variable name="VAR1">
						<xsl:call-template name="TERRITORYBASE">
							<xsl:with-param name="FACTORELEMENT" select="'COMP'" />
						</xsl:call-template>
					</xsl:variable>
					<xsl:variable name="VAR2">
						<xsl:call-template name="INSURANCESCORE" />
					</xsl:variable>
					<xsl:variable name="VAR3">
						<xsl:choose>
							<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE)=$VT_MOTORHOME">
								<xsl:call-template name="MOTORHOME" />
							</xsl:when>
							<xsl:otherwise>1.00</xsl:otherwise>
						</xsl:choose>
					</xsl:variable>
					<xsl:variable name="VAR4">
						<xsl:call-template name="TYPEOFVEHICLE">
							<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
						</xsl:call-template>
					</xsl:variable>
					<xsl:variable name="VAR5">
						<xsl:call-template name="CLASS">
							<xsl:with-param name="FACTORELEMENT" select="'COMP'" />
						</xsl:call-template>
					</xsl:variable>
					<xsl:variable name="VAR6">
						<xsl:call-template name="SYMBOL">
							<xsl:with-param name="FACTORELEMENT" select="'COMP'" />
						</xsl:call-template>
					</xsl:variable>
					<xsl:variable name="VAR7">
						<xsl:call-template name="DEDUCTIBLE">
							<xsl:with-param name="FACTORELEMENT" select="'COMP'" />
						</xsl:call-template>
					</xsl:variable>
					<xsl:variable name="VAR8">
						<xsl:call-template name="AGE">
							<xsl:with-param name="FACTORELEMENT" select="'COMP'" />
						</xsl:call-template>
					</xsl:variable>
					<xsl:variable name="VAR9">
						<xsl:call-template name="MULTIPOLICY" />
					</xsl:variable>
					<!--xsl:value-of select="round(round(round(round(round(round(round(round($VAR1*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)" /-->
					<xsl:variable name="FIRST_STEP" select="($VAR1*$VAR2)" />
					<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
					<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR3" />
					<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
					<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR4" />
					<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
					<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR5" />
					<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
					<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR6" />
					<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
					<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR7" />
					<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
					<xsl:variable name="SEVEN_STEP" select="$VARSIX6 * $VAR8" />
					<xsl:variable name="VARSEV7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
					<xsl:variable name="EIGHT_STEP" select="$VARSEV7 * $VAR9" />
					<xsl:variable name="VAREIG8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
					<xsl:value-of select="round($VAREIG8)" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_MIN_PREMIUM_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_VEHICLE_PREMIUM_COMP_ONLY']/NODE[@ID='MINIMUM_PREMIUM']/ATTRIBUTES/@MIN_PREMIUM" />
		<xsl:choose>
			<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE_SCO)=$VT_SUSPENDED_COMP and $COMP_PREMIUM &lt;=$VAR_MIN_PREMIUM_VALUE">
				<xsl:value-of select="$VAR_MIN_PREMIUM_VALUE" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$COMP_PREMIUM" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="COMP_PERSONAL_INDIANA">
		<xsl:variable name="VAR_COST" select="VEHICLES/VEHICLE/COST" />
		<!-- Approach different for Travel Campers -->
		<xsl:variable name="COMP_PREMIUM">
			<xsl:choose>
				<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_CAMPER_TRAVEL_TRAILER or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_UTILITY_TRAILER">
					<xsl:choose>
						<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE !='' and VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE &gt; 0">
							<!-- Get the comprehensive amount against cost,age and multiply by deductible factor -->
							<xsl:variable name="VAR_AGE" select="VEHICLES/VEHICLE/AGE" />
							<xsl:variable name="VAR_DEDUCTIBLE" select="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE" />
							<!-- Fetch the base amount for the resp age category -->
							<xsl:variable name="VAR_COMM_BASE_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COMPREHENSIVE_BASE_VALUE']/ATTRIBUTES[@AGE_MIN &lt;= $VAR_AGE and @AGE_MAX &gt;= $VAR_AGE]/@BASE_VALUE" />
							<!-- Fetch the max limit present -->
							<xsl:variable name="VAR_MAX_COST_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COMPREHENSIVE']/@MAX_COST" />
							<!-- Fetch the deductible factor -->
							<xsl:variable name="VAR_DEDUCTIBE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COMPREHENSIVE_DED_FACTOR']/ATTRIBUTES[@DEDUCTIBLE = $VAR_DEDUCTIBLE]/@FACTOR" />
							<!-- If the cost exceeds this max limit then -->
							<xsl:choose>
								<xsl:when test="$VAR_COST &gt;= $VAR_MAX_COST_VALUE">
									<!-- Fetch the factor against max limit-->
									<xsl:variable name="VAR_MAX_COST_VALUE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COMPREHENSIVE']/ATTRIBUTES[@COST_MAX = $VAR_MAX_COST_VALUE]/@COMP_RATE_FACTOR" />
									<!-- Fetch the differencr in amount -->
									<xsl:variable name="VAR_DIFFERENCE" select="$VAR_COST - $VAR_MAX_COST_VALUE" />
									<!-- Fetch the each additional amount -->
									<xsl:variable name="VAR_FOR_EACH_ADDTN_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COMPREHENSIVE']/ATTRIBUTES/@COST_ADDITIONAL" />
									<!-- Fetch the factor value for each additional amount -->
									<xsl:variable name="VAR_ADDTN_VALUE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COMPREHENSIVE']/ATTRIBUTES/@COMP_RATE_ADDTNL_FACTOR" />
									<!-- Prepare the formula e.g base value * ( 4.03+((32000-30000)/1000)*0.16)-->
									<xsl:value-of select="round(round($VAR_COMM_BASE_VALUE * ($VAR_MAX_COST_VALUE_FACTOR + round(round($VAR_DIFFERENCE div $VAR_FOR_EACH_ADDTN_VALUE) * $VAR_ADDTN_VALUE_FACTOR)))*$VAR_DEDUCTIBE_FACTOR)" />
								</xsl:when>
								<xsl:otherwise> <!-- Otherwise if the cost is below the max limit -->
									<!-- Fetch the resp factor -->
									<xsl:variable name="VAR_COMM_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COMPREHENSIVE']/ATTRIBUTES[@COST_MIN &lt;= $VAR_COST and @COST_MAX &gt;= $VAR_COST]/@COMP_RATE_FACTOR" />
									<!-- Multiply factor with  base value -->
									<xsl:value-of select="round(round($VAR_COMM_BASE_VALUE * $VAR_COMM_FACTOR )* $VAR_DEDUCTIBE_FACTOR)" />
								</xsl:otherwise>
							</xsl:choose>
						</xsl:when>
						<xsl:otherwise>0.00</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_CLASSIC_CAR or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_ANTIQUE_CAR or VEHICLES/VEHICLE/VEHICLETYPE ='SA'">
					<xsl:choose>
						<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE !='' and VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE &gt; 0">
							<xsl:variable name="VAR1">
								<xsl:call-template name="TYPEOFVEHICLE">
									<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
								</xsl:call-template>
							</xsl:variable>
							<xsl:variable name="VAR2">
								<xsl:choose>
									<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='ANC'">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@BASERATECOMPCOLL" />
									</xsl:when>
									<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='CLC'">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@BASERATECOMPCOLL" />
									</xsl:when>
									<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SA'">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@BASERATECOMPCOLL" />
									</xsl:when>
								</xsl:choose>
							</xsl:variable>
							<xsl:variable name="VAR3">
								<xsl:call-template name="TRAILBLAZER">
									<xsl:with-param name="FACTORELEMENT" select="'COMP'" />
								</xsl:call-template>
							</xsl:variable>
							<xsl:variable name="VAR4">
								<xsl:call-template name="MULTIVEHICLE">
									<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
								</xsl:call-template>
							</xsl:variable>
							<xsl:variable name="VAR5">
								<xsl:call-template name="DRIVERDISCOUNT" />
							</xsl:variable>
							<xsl:variable name="VAR6">
								<xsl:call-template name="GOODSTUDENT" />
							</xsl:variable>
							<xsl:variable name="VAR7">
								<xsl:call-template name="INSURANCESCORE" />
							</xsl:variable>
							<xsl:variable name="VAR8">
								<xsl:call-template name="SURCHARGE" />
							</xsl:variable>
							<xsl:variable name="VAR9">
								<xsl:call-template name="MULTIPOLICY" />
							</xsl:variable>
							<xsl:value-of select="round(round(round(round(round(round(round(round($VAR1*($VAR_COST div $VAR2))*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)" />
							<!--xsl:value-of select="round(round(round(round(round(round(round(round(round(round(round($VAR1*($VAR_COST div $VAR2))*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)"/-->
						</xsl:when>
						<xsl:otherwise>0.00</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="VAR1">
						<xsl:call-template name="TERRITORYBASE">
							<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
						</xsl:call-template>
					</xsl:variable>
					<xsl:variable name="VAR2">
						<xsl:choose>
							<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE)=$VT_MOTORHOME">
								<xsl:call-template name="MOTORHOME" />
							</xsl:when>
							<xsl:otherwise>1.00</xsl:otherwise>
						</xsl:choose>
					</xsl:variable>
					<xsl:variable name="VAR3">
						<xsl:call-template name="TYPEOFVEHICLE">
							<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
						</xsl:call-template>
					</xsl:variable>
					<xsl:variable name="VAR4">
						<xsl:call-template name="CLASS">
							<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
						</xsl:call-template>
					</xsl:variable>
					<xsl:variable name="VAR5">
						<xsl:call-template name="SYMBOL">
							<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
						</xsl:call-template>
					</xsl:variable>
					<xsl:variable name="VAR6">
						<xsl:call-template name="DEDUCTIBLE">
							<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
						</xsl:call-template>
					</xsl:variable>
					<xsl:variable name="VAR7">
						<xsl:call-template name="AGE">
							<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
						</xsl:call-template>
					</xsl:variable>
					<xsl:variable name="VAR8">
						<xsl:call-template name="MULTIVEHICLE">
							<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
						</xsl:call-template>
					</xsl:variable>
					<xsl:variable name="VAR9">
						<xsl:call-template name="VEHICLEUSE" />
					</xsl:variable>
					<xsl:variable name="VAR10">
						<xsl:call-template name="DRIVERDISCOUNT" />
					</xsl:variable>
					<xsl:variable name="VAR11">
						<xsl:call-template name="GOODSTUDENT" />
					</xsl:variable>
					<xsl:variable name="VAR12">
						<xsl:call-template name="INSURANCESCORE" />
					</xsl:variable>
					<xsl:variable name="VAR13">
						<xsl:call-template name="SURCHARGE" />
					</xsl:variable>
					<xsl:variable name="VAR14">
						<xsl:call-template name="MULTIPOLICY" />
					</xsl:variable>
					<xsl:variable name="VAR15">
						<xsl:call-template name="TRAILBLAZER">
							<xsl:with-param name="FACTORELEMENT" select="'COMP'" />
						</xsl:call-template>
					</xsl:variable>
					<!--xsl:value-of select="round(round(round(round(round(round(round(round(round(round(round(round(round(round($VAR1*$VAR15)*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)*$VAR13)*$VAR14)" /-->
					<xsl:variable name="FIRST_STEP" select="($VAR1*$VAR15)" />
					<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
					<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR2" />
					<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
					<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR3" />
					<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
					<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR4" />
					<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
					<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR5" />
					<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
					<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR6" />
					<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
					<xsl:variable name="SEVEN_STEP" select="$VARSIX6 * $VAR7" />
					<xsl:variable name="VARSEV7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
					<xsl:variable name="EIGHT_STEP" select="$VARSEV7 * $VAR8" />
					<xsl:variable name="VAREIG8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
					<xsl:variable name="NINE_STEP" select="$VAREIG8 * $VAR9" />
					<xsl:variable name="VARNIN9" select="round(format-number($NINE_STEP,'##.0000'))" />
					<xsl:variable name="TENTH_STEP" select="$VARNIN9 * $VAR10" />
					<xsl:variable name="VARTEN10" select="round(format-number($TENTH_STEP,'##.0000'))" />
					<xsl:variable name="ELEVEN_STEP" select="$VARTEN10 * $VAR11" />
					<xsl:variable name="VARELE11" select="round(format-number($ELEVEN_STEP,'##.0000'))" />
					<xsl:variable name="TWELVE_STEP" select="$VARELE11 * $VAR12" />
					<xsl:variable name="VARTWE12" select="round(format-number($TWELVE_STEP,'##.0000'))" />
					<xsl:variable name="THERTEEN_STEP" select="$VARTWE12 * $VAR13" />
					<xsl:variable name="VARTWE13" select="round(format-number($THERTEEN_STEP,'##.0000'))" />
					<xsl:variable name="FORTEEN_STEP" select="$VARTWE13 * $VAR14" />
					<xsl:variable name="VARFORT14" select="round(format-number($FORTEEN_STEP,'##.0000'))" />
					<xsl:value-of select="round($VARFORT14)" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="VAR_MIN_PREMIUM_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINIMUM_VEHICLE_PREMIUM_COMP_ONLY']/NODE[@ID='MINIMUM_PREMIUM']/ATTRIBUTES/@MIN_PREMIUM" />
		<xsl:choose>
			<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE_SCO)=$VT_SUSPENDED_COMP and $COMP_PREMIUM &lt;=$VAR_MIN_PREMIUM_VALUE">
				<xsl:value-of select="$VAR_MIN_PREMIUM_VALUE" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$COMP_PREMIUM" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of Comprehensive  -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- *************************************  Start of Collision  ******************************************************************* -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- Collision -->
	<xsl:template name="COLL">
		<xsl:choose>
			<!-- For Michigan -Personal Automobile -->
			<xsl:when test="POLICY/STATENAME ='MICHIGAN' and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_SUSPENDED_COMP and VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO'">
				<xsl:call-template name="COLL_PERSONAL_MICHIGAN" />
			</xsl:when>
			<!-- For Indiana -Personal Automobile -->
			<xsl:when test="POLICY/STATENAME ='INDIANA' and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_SUSPENDED_COMP and VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO'">
				<xsl:call-template name="COLL_PERSONAL_INDIANA" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Start Michigan -Personal Automobile -->
	<xsl:template name="COLL_PERSONAL_MICHIGAN">
		<xsl:variable name="VAR_COST" select="VEHICLES/VEHICLE/COST" />
		<xsl:choose>
			<xsl:when test="(normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_CAMPER_TRAVEL_TRAILER or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_UTILITY_TRAILER)  ">
				<xsl:choose>
					<xsl:when test="(VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE !='' and VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE &gt; 0)">
						<!-- Get the comprehensive amount against cost,age and multiply by deductible factor -->
						<xsl:variable name="VAR_AGE" select="VEHICLES/VEHICLE/AGE" />
						<xsl:variable name="VAR_DEDUCTIBLE" select="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE" />
						<xsl:variable name="VAR_DEDUCTIBLE_TYPE" select="VEHICLES/VEHICLE/COVGCOLLISIONTYPE" />
						<!-- Fetch the base amount for the resp age category -->
						<xsl:variable name="VAR_COMM_BASE_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION_BASE_VALUE']/ATTRIBUTES[@AGE_MIN &lt;= $VAR_AGE and @AGE_MAX &gt;= $VAR_AGE]/@BASE_VALUE" />
						<!-- Fetch the max limit present -->
						<xsl:variable name="VAR_MAX_COST_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION']/@MAX_COST" />
						<!-- Get the deductible factor 	-->
						<xsl:variable name="VAR_DEDUCTIBLE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION_DED_FACTOR']/ATTRIBUTES[@DEDUCTIBLE=normalize-space($VAR_DEDUCTIBLE) and @TYPE=normalize-space($VAR_DEDUCTIBLE_TYPE)]/@FACTOR" />
						<!-- If the cost exceeds this max limit then -->
						<xsl:choose>
							<xsl:when test="$VAR_COST &gt;= $VAR_MAX_COST_VALUE">
								<!-- Fetch the factor against max limit -->
								<xsl:variable name="VAR_MAX_COST_VALUE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION']/ATTRIBUTES[@COST_MAX = $VAR_MAX_COST_VALUE]/@COMP_RATE_FACTOR" />
								<!-- Fetch the differencr in amount -->
								<xsl:variable name="VAR_DIFFERENCE" select="$VAR_COST - $VAR_MAX_COST_VALUE" />
								<!-- Fetch the each additional amount -->
								<xsl:variable name="VAR_FOR_EACH_ADDTN_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION']/ATTRIBUTES/@COST_ADDITIONAL" />
								<!-- Fetch the factor value for each additional amount -->
								<xsl:variable name="VAR_ADDTN_VALUE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION']/ATTRIBUTES/@COMP_RATE_ADDTNL_FACTOR" />
								<!-- Prepare the formula e.g base value * ( 4.03+((32000-30000)/1000)*0.16)-->
								<xsl:value-of select="round(round($VAR_COMM_BASE_VALUE * ($VAR_MAX_COST_VALUE_FACTOR + (($VAR_DIFFERENCE div $VAR_FOR_EACH_ADDTN_VALUE) * $VAR_ADDTN_VALUE_FACTOR)))*$VAR_DEDUCTIBLE_FACTOR)" />
							</xsl:when>
							<xsl:otherwise> <!-- Otherwise if the cost is below the max limit -->
								<!-- Fetch the resp factor -->
								<xsl:variable name="VAR_COMM_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION']/ATTRIBUTES[@COST_MIN &lt;= $VAR_COST and @COST_MAX &gt;= $VAR_COST]/@COMP_RATE_FACTOR" />
								<!-- Multiply factor with  base value-->
								<xsl:value-of select="round(round($VAR_COMM_BASE_VALUE * $VAR_COMM_FACTOR )* $VAR_DEDUCTIBLE_FACTOR)" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_CLASSIC_CAR or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_ANTIQUE_CAR or VEHICLES/VEHICLE/VEHICLETYPE ='SA'">
				<xsl:choose>
					<xsl:when test="(VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE !='' and VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE &gt; 0)">
						<xsl:variable name="VAR1">
							<xsl:call-template name="TYPEOFVEHICLE">
								<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:choose>
								<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='ANC'">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@BASERATECOMPCOLL" />
								</xsl:when>
								<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='CLC'">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@BASERATECOMPCOLL" />
								</xsl:when>
								<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SA'">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@BASERATECOMPCOLL" />
								</xsl:when>
							</xsl:choose>
						</xsl:variable>
						<xsl:variable name="VAR3">
							<xsl:call-template name="MULTIVEHICLE">
								<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR4">
							<xsl:call-template name="DRIVERDISCOUNT" />
						</xsl:variable>
						<xsl:variable name="VAR5">
							<xsl:call-template name="GOODSTUDENT" />
						</xsl:variable>
						<xsl:variable name="VAR6">
							<xsl:call-template name="SURCHARGE" />
						</xsl:variable>
						<xsl:variable name="VAR7">
							<xsl:call-template name="MULTIPOLICY" />
						</xsl:variable>
						<!--xsl:value-of select="round(round(round(round(round(round($VAR1*($VAR_COST div $VAR2))*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)" /-->
						<xsl:variable name="FIRST_STEP" select="($VAR1*($VAR_COST div $VAR2))" />
						<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
						<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR3" />
						<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
						<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR4" />
						<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
						<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR5" />
						<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
						<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR6" />
						<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
						<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR7" />
						<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
						<xsl:value-of select="$VARSIX6" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="OPTIMAL" />
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="TYPEOFVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="SYMBOL">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="DEDUCTIBLE">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR8">
					<xsl:call-template name="AGE">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR9">
					<xsl:call-template name="ABS" />
				</xsl:variable>
				<xsl:variable name="VAR10">
					<xsl:call-template name="MULTIVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR11">
					<xsl:call-template name="VEHICLEUSE" />
				</xsl:variable>
				<xsl:variable name="VAR12">
					<xsl:call-template name="DRIVERDISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR13">
					<xsl:call-template name="GOODSTUDENT" />
				</xsl:variable>
				<xsl:variable name="VAR14">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<xsl:variable name="VAR15">
					<xsl:call-template name="MULTIPOLICY" />
				</xsl:variable>
				<!--xsl:value-of select="round(round(round(round(round(round(round(round(round(round(round(round(round(round(round($VAR1)*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)*$VAR13)*$VAR14)*$VAR15)" /-->
				<xsl:variable name="FIRST_STEP" select="($VAR1*$VAR2)" />
				<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
				<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR3" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
				<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR4" />
				<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
				<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR5" />
				<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
				<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR6" />
				<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
				<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR7" />
				<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
				<xsl:variable name="SEVEN_STEP" select="$VARSIX6 * $VAR8" />
				<xsl:variable name="VARSEV7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
				<xsl:variable name="EIGHT_STEP" select="$VARSEV7 * $VAR9" />
				<xsl:variable name="VAREIG8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
				<xsl:variable name="NINE_STEP" select="$VAREIG8 * $VAR10" />
				<xsl:variable name="VARNIN9" select="round(format-number($NINE_STEP,'##.0000'))" />
				<xsl:variable name="TENTH_STEP" select="$VARNIN9 * $VAR11" />
				<xsl:variable name="VARTEN10" select="round(format-number($TENTH_STEP,'##.0000'))" />
				<xsl:variable name="ELEVEN_STEP" select="$VARTEN10 * $VAR12" />
				<xsl:variable name="VARELE11" select="round(format-number($ELEVEN_STEP,'##.0000'))" />
				<xsl:variable name="TWELVE_STEP" select="$VARELE11 * $VAR13" />
				<xsl:variable name="VARTWE12" select="round(format-number($TWELVE_STEP,'##.0000'))" />
				<xsl:variable name="THRTEEN_STEP" select="$VARTWE12 * $VAR14" />
				<xsl:variable name="VARTHR13" select="round(format-number($THRTEEN_STEP,'##.0000'))" />
				<xsl:variable name="FORTEEN_STEP" select="$VARTHR13 * $VAR15" />
				<xsl:variable name="VARFORT14" select="round(format-number($FORTEEN_STEP,'##.0000'))" />
				<xsl:value-of select="round($VARFORT14)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End Michigan -Personal Automobile -->
	<!-- Start Indiana -Personal Automobile -->
	<xsl:template name="COLL_PERSONAL_INDIANA">
		<xsl:variable name="VAR_COST" select="VEHICLES/VEHICLE/COST" />
		<xsl:choose>
			<xsl:when test="(normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_CAMPER_TRAVEL_TRAILER or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_UTILITY_TRAILER)">
				<xsl:choose>
					<xsl:when test=" (VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE !='' and VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE &gt; 0)">
						<!-- Get the coll amount against cost,age and multiply by deductible factor -->
						<xsl:variable name="VAR_AGE" select="VEHICLES/VEHICLE/AGE" />
						<!-- Fetch the base amount for the resp age category -->
						<xsl:variable name="VAR_COMM_BASE_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION_BASE_VALUE']/ATTRIBUTES[@AGE_MIN &lt;= $VAR_AGE and @AGE_MAX &gt;= $VAR_AGE]/@BASE_VALUE" />
						<!-- Fetch the max limit present -->
						<xsl:variable name="VAR_MAX_COST_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION']/@MAX_COST" />
						<xsl:variable name="VAR_DEDUCTIBLE" select="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE" />
						<!-- Get the deductible factor 	-->
						<xsl:variable name="VAR_DEDUCTIBLE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION_DED_FACTOR']/ATTRIBUTES[@DEDUCTIBLE=normalize-space($VAR_DEDUCTIBLE)]/@FACTOR" />
						<!-- If the cost exceeds this max limit then -->
						<xsl:choose>
							<xsl:when test="$VAR_COST &gt;= $VAR_MAX_COST_VALUE">
								<!-- Fetch the factor against max limit -->
								<xsl:variable name="VAR_MAX_COST_VALUE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION']/ATTRIBUTES[@COST_MAX = $VAR_MAX_COST_VALUE]/@COMP_RATE_FACTOR" />
								<!-- Fetch the differencr in amount -->
								<xsl:variable name="VAR_DIFFERENCE" select="$VAR_COST - $VAR_MAX_COST_VALUE" />
								<!-- Fetch the each additional amount -->
								<xsl:variable name="VAR_FOR_EACH_ADDTN_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION']/ATTRIBUTES/@COST_ADDITIONAL" />
								<!-- Fetch the factor value for each additional amount -->
								<xsl:variable name="VAR_ADDTN_VALUE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION']/ATTRIBUTES/@COMP_RATE_ADDTNL_FACTOR" />
								<!-- Prepare the formula e.g base value * ( 4.03+((32000-30000)/1000)*0.16)-->
								<xsl:value-of select="round(round($VAR_COMM_BASE_VALUE * ($VAR_MAX_COST_VALUE_FACTOR + (($VAR_DIFFERENCE div $VAR_FOR_EACH_ADDTN_VALUE) * $VAR_ADDTN_VALUE_FACTOR)))*$VAR_DEDUCTIBLE_FACTOR)" />
							</xsl:when>
							<xsl:otherwise> <!-- Otherwise if the cost is below the max limit -->
								<!-- Fetch the resp factor -->
								<xsl:variable name="VAR_COMM_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION']/ATTRIBUTES[@COST_MIN &lt;= $VAR_COST and @COST_MAX &gt;= $VAR_COST]/@COMP_RATE_FACTOR" />
								<!-- Multiply factor with  base value -->
								<xsl:value-of select="round(round($VAR_COMM_BASE_VALUE * $VAR_COMM_FACTOR)*$VAR_DEDUCTIBLE_FACTOR)" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_CLASSIC_CAR or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_ANTIQUE_CAR or VEHICLES/VEHICLE/VEHICLETYPE ='SA'">
				<xsl:choose>
					<xsl:when test=" (VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE !='' and VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE &gt; 0)">
						<xsl:variable name="VAR1">
							<xsl:call-template name="TYPEOFVEHICLE">
								<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:choose>
								<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='ANC'">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@BASERATECOMPCOLL" />
								</xsl:when>
								<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='CLC'">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@BASERATECOMPCOLL" />
								</xsl:when>
								<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SA'">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@BASERATECOMPCOLL" />
								</xsl:when>
							</xsl:choose>
						</xsl:variable>
						<xsl:variable name="VAR3">
							<xsl:call-template name="TRAILBLAZER">
								<xsl:with-param name="FACTORELEMENT" select="'COLL'" />
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR4">
							<xsl:call-template name="MULTIVEHICLE">
								<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR5">
							<xsl:call-template name="DRIVERDISCOUNT" />
						</xsl:variable>
						<xsl:variable name="VAR6">
							<xsl:call-template name="GOODSTUDENT" />
						</xsl:variable>
						<xsl:variable name="VAR7">
							<xsl:call-template name="INSURANCESCORE" />
						</xsl:variable>
						<xsl:variable name="VAR8">
							<xsl:call-template name="SURCHARGE" />
						</xsl:variable>
						<xsl:variable name="VAR9">
							<xsl:call-template name="MULTIPOLICY" />
						</xsl:variable>
						<xsl:value-of select="round(round(round(round(round(round(round(($VAR1*($VAR_COST div $VAR2))*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)" />
						<!--xsl:value-of select="round(round(round(round(round(round(round(round(round(round($VAR1*($VAR_COST div $VAR2))*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)" /-->
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="MOTORHOME"></xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="TYPEOFVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="SYMBOL">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="DEDUCTIBLE">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="AGE">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR8">
					<xsl:call-template name="ABS" />
				</xsl:variable>
				<xsl:variable name="VAR9">
					<xsl:call-template name="MULTIVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR10">
					<xsl:call-template name="VEHICLEUSE" />
				</xsl:variable>
				<xsl:variable name="VAR11">
					<xsl:call-template name="DRIVERDISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR12">
					<xsl:call-template name="GOODSTUDENT" />
				</xsl:variable>
				<xsl:variable name="VAR13">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR14">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<xsl:variable name="VAR15">
					<xsl:call-template name="MULTIPOLICY" />
				</xsl:variable>
				<xsl:variable name="VAR16">
					<xsl:call-template name="TRAILBLAZER">
						<xsl:with-param name="FACTORELEMENT" select="'COLL'" />
					</xsl:call-template>
				</xsl:variable>
				<!--xsl:value-of select="round(round(round(round(round(round(round(round(round(round(round(round(round(round(round(round($VAR1)*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)*$VAR13)*$VAR14)*$VAR15)*$VAR16)" /-->
				<xsl:variable name="FIRST_STEP" select="($VAR1*$VAR16)" />
				<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
				<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR2" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
				<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR3" />
				<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
				<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR4" />
				<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
				<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR5" />
				<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
				<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR6" />
				<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
				<xsl:variable name="SEVEN_STEP" select="$VARSIX6 * $VAR7" />
				<xsl:variable name="VARSEV7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
				<xsl:variable name="EIGHT_STEP" select="$VARSEV7 * $VAR8" />
				<xsl:variable name="VAREIG8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
				<xsl:variable name="NINE_STEP" select="$VAREIG8 * $VAR9" />
				<xsl:variable name="VARNIN9" select="round(format-number($NINE_STEP,'##.0000'))" />
				<xsl:variable name="TENTH_STEP" select="$VARNIN9 * $VAR10" />
				<xsl:variable name="VARTEN10" select="round(format-number($TENTH_STEP,'##.0000'))" />
				<xsl:variable name="ELEVEN_STEP" select="$VARTEN10 * $VAR11" />
				<xsl:variable name="VARELE11" select="round(format-number($ELEVEN_STEP,'##.0000'))" />
				<xsl:variable name="TWELVE_STEP" select="$VARELE11 * $VAR12" />
				<xsl:variable name="VARTWE12" select="round(format-number($TWELVE_STEP,'##.0000'))" />
				<xsl:variable name="THRTEEN_STEP" select="$VARTWE12 * $VAR13" />
				<xsl:variable name="VARTHR13" select="round(format-number($THRTEEN_STEP,'##.0000'))" />
				<xsl:variable name="FORTEEN_STEP" select="$VARTHR13 * $VAR14" />
				<xsl:variable name="VARFORT14" select="round(format-number($FORTEEN_STEP,'##.0000'))" />
				<xsl:variable name="FIFTEEN_STEP" select="$VARFORT14 * $VAR15" />
				<xsl:variable name="VARFIF15" select="round(format-number($FIFTEEN_STEP,'##.0000'))" />
				<xsl:value-of select="round($VARFIF15)" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of Collision  -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- *************************************  Start of Limited Collision ******************************************************************* -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- Limited Collision -->
	<xsl:template name="LTDCOLL">
		<!-- Get the coll amount against cost,age and multiply by deductible factor -->
		<xsl:variable name="VAR_AGE" select="VEHICLES/VEHICLE/AGE" />
		<xsl:variable name="VAR_COST" select="VEHICLES/VEHICLE/COST" />
		<xsl:choose>
			<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_CAMPER_TRAVEL_TRAILER or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_UTILITY_TRAILER">
				<!-- Fetch the base amount for the resp age category -->
				<xsl:variable name="VAR_COMM_BASE_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION_BASE_VALUE']/ATTRIBUTES[@AGE_MIN &lt;= $VAR_AGE and @AGE_MAX &gt;= $VAR_AGE]/@BASE_VALUE" />
				<!-- Fetch the max limit present -->
				<xsl:variable name="VAR_MAX_COST_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION']/@MAX_COST" />
				<xsl:variable name="VAR_DEDUCTIBLE" select="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE" />
				<!-- Get the deductible factor 	-->
				<xsl:variable name="VAR_DEDUCTIBLE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION_DED_FACTOR']/ATTRIBUTES[@DEDUCTIBLE=normalize-space($VAR_DEDUCTIBLE)]/@FACTOR" />
				<!-- If the cost exceeds this max limit then -->
				<xsl:choose>
					<xsl:when test="$VAR_COST &gt;= $VAR_MAX_COST_VALUE">
						<!-- Fetch the factor against max limit -->
						<xsl:variable name="VAR_MAX_COST_VALUE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION']/ATTRIBUTES[@COST_MAX = $VAR_MAX_COST_VALUE]/@COMP_RATE_FACTOR" />
						<!-- Fetch the differencr in amount -->
						<xsl:variable name="VAR_DIFFERENCE" select="$VAR_COST - $VAR_MAX_COST_VALUE" />
						<!-- Fetch the each additional amount -->
						<xsl:variable name="VAR_FOR_EACH_ADDTN_VALUE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION']/ATTRIBUTES/@COST_ADDITIONAL" />
						<!-- Fetch the factor value for each additional amount -->
						<xsl:variable name="VAR_ADDTN_VALUE_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION']/ATTRIBUTES/@COMP_RATE_ADDTNL_FACTOR" />
						<!-- Prepare the formula e.g base value * ( 4.03+((32000-30000)/1000)*0.16)-->
						<xsl:value-of select="round(round($VAR_COMM_BASE_VALUE * ($VAR_MAX_COST_VALUE_FACTOR + (($VAR_DIFFERENCE div $VAR_FOR_EACH_ADDTN_VALUE) * $VAR_ADDTN_VALUE_FACTOR)))*$VAR_DEDUCTIBLE_FACTOR)" />
					</xsl:when>
					<xsl:otherwise> <!-- Otherwise if the cost is below the max limit -->
						<!-- Fetch the resp factor -->
						<xsl:variable name="VAR_COMM_FACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CAMPER_TRAVEL_TRAILER']/NODE[@ID='COLLISION']/ATTRIBUTES[@COST_MIN &lt;= $VAR_COST and @COST_MAX &gt;= $VAR_COST]/@COMP_RATE_FACTOR" />
						<!-- Multiply factor with  base value -->
						<xsl:value-of select="round(round($VAR_COMM_BASE_VALUE * $VAR_COMM_FACTOR)*$VAR_DEDUCTIBLE_FACTOR)" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_CLASSIC_CAR or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_ANTIQUE_CAR or VEHICLES/VEHICLE/VEHICLETYPE ='SA'">
				<xsl:choose>
					<xsl:when test="(VEHICLES/VEHICLE/COVGCOLLISIONTYPE !='')">
						<xsl:variable name="VAR1">
							<xsl:call-template name="TYPEOFVEHICLE">
								<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:choose>
								<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='ANC'">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@BASERATECOMPCOLL" />
								</xsl:when>
								<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='CLC'">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@BASERATECOMPCOLL" />
								</xsl:when>
								<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SA'">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@BASERATECOMPCOLL" />
								</xsl:when>
							</xsl:choose>
						</xsl:variable>
						<xsl:variable name="VAR3">
							<xsl:call-template name="MULTIVEHICLE">
								<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:variable name="VAR4">
							<xsl:call-template name="DRIVERDISCOUNT" />
						</xsl:variable>
						<xsl:variable name="VAR5">
							<xsl:call-template name="GOODSTUDENT" />
						</xsl:variable>
						<xsl:variable name="VAR6">
							<xsl:call-template name="SURCHARGE" />
						</xsl:variable>
						<xsl:variable name="VAR7">
							<xsl:call-template name="MULTIPOLICY" />
						</xsl:variable>
						<xsl:value-of select="round(round(round(round(round(round($VAR1*($VAR_COST div $VAR2))*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE !=$VT_SUSPENDED_COMP and VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO'">
				<xsl:variable name="VAR1">
					<xsl:call-template name="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR2">
					<xsl:call-template name="INSURANCESCORE" />
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:call-template name="OPTIMAL" />
				</xsl:variable>
				<xsl:variable name="VAR4">
					<xsl:call-template name="TYPEOFVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR5">
					<xsl:call-template name="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR6">
					<xsl:call-template name="SYMBOL">
						<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR7">
					<xsl:call-template name="LIMITFACTOR" />
				</xsl:variable>
				<xsl:variable name="VAR8">
					<xsl:call-template name="AGE">
						<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR9">
					<xsl:call-template name="ABS" />
				</xsl:variable>
				<xsl:variable name="VAR10">
					<xsl:call-template name="MULTIVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="VAR11">
					<xsl:call-template name="VEHICLEUSE" />
				</xsl:variable>
				<xsl:variable name="VAR12">
					<xsl:call-template name="DRIVERDISCOUNT" />
				</xsl:variable>
				<xsl:variable name="VAR13">
					<xsl:call-template name="GOODSTUDENT" />
				</xsl:variable>
				<xsl:variable name="VAR14">
					<xsl:call-template name="SURCHARGE" />
				</xsl:variable>
				<xsl:variable name="VAR15">
					<xsl:call-template name="MULTIPOLICY" />
				</xsl:variable>
				<!--xsl:value-of select="round(round(round(round(round(round(round(round(round(round(round(round(round(round($VAR1*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)*$VAR13)*$VAR14)*$VAR15)" /-->
				<xsl:variable name="FIRST_STEP" select="($VAR1*$VAR2)" />
				<xsl:variable name="VARFIS1" select="round(format-number($FIRST_STEP,'##.0000'))" />
				<xsl:variable name="SECOND_STEP" select="$VARFIS1 * $VAR3" />
				<xsl:variable name="VARSEC2" select="round(format-number($SECOND_STEP,'##.0000'))" />
				<xsl:variable name="THIRD_STEP" select="$VARSEC2 * $VAR4" />
				<xsl:variable name="VARTHI3" select="round(format-number($THIRD_STEP,'##.0000'))" />
				<xsl:variable name="FORTH_STEP" select="$VARTHI3 * $VAR5" />
				<xsl:variable name="VARFOR4" select="round(format-number($FORTH_STEP,'##.0000'))" />
				<xsl:variable name="FIFTH_STEP" select="$VARFOR4 * $VAR6" />
				<xsl:variable name="VARFIF5" select="round(format-number($FIFTH_STEP,'##.0000'))" />
				<xsl:variable name="SIX_STEP" select="$VARFIF5 * $VAR7" />
				<xsl:variable name="VARSIX6" select="round(format-number($SIX_STEP,'##.0000'))" />
				<xsl:variable name="SEVEN_STEP" select="$VARSIX6 * $VAR8" />
				<xsl:variable name="VARSEV7" select="round(format-number($SEVEN_STEP,'##.0000'))" />
				<xsl:variable name="EIGHT_STEP" select="$VARSEV7 * $VAR9" />
				<xsl:variable name="VAREIG8" select="round(format-number($EIGHT_STEP,'##.0000'))" />
				<xsl:variable name="NINE_STEP" select="$VAREIG8 * $VAR10" />
				<xsl:variable name="VARNIN9" select="round(format-number($NINE_STEP,'##.0000'))" />
				<xsl:variable name="TENTH_STEP" select="$VARNIN9 * $VAR11" />
				<xsl:variable name="VARTEN10" select="round(format-number($TENTH_STEP,'##.0000'))" />
				<xsl:variable name="ELEVEN_STEP" select="$VARTEN10 * $VAR12" />
				<xsl:variable name="VARELE11" select="round(format-number($ELEVEN_STEP,'##.0000'))" />
				<xsl:variable name="TWELVE_STEP" select="$VARELE11 * $VAR13" />
				<xsl:variable name="VARTWE12" select="round(format-number($TWELVE_STEP,'##.0000'))" />
				<xsl:variable name="THRTEEN_STEP" select="$VARTWE12 * $VAR14" />
				<xsl:variable name="VARTHR13" select="round(format-number($THRTEEN_STEP,'##.0000'))" />
				<xsl:variable name="FORTEEN_STEP" select="$VARTHR13 * $VAR15" />
				<xsl:variable name="VARFORT14" select="round(format-number($FORTEEN_STEP,'##.0000'))" />
				<xsl:value-of select="round($VARFORT14)" />
			</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of Limited Collision -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- ***********************************  Mini-Tort  ****************************************************************** -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="MINITORT">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME='MICHIGAN' and VEHICLES/VEHICLE/MINITORTPDLIAB = 'TRUE' and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_SUSPENDED_COMP and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_UTILITY_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_CAMPER_TRAVEL_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINITORT']/NODE[@ID='MINITORTRATE']/ATTRIBUTES/@RATE_PER_VEHICLE" />
			</xsl:when>
			<xsl:otherwise>
			0	
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MINITORTLIMIT">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME='MICHIGAN' and VEHICLES/VEHICLE/MINITORTPDLIAB = 'TRUE' and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_SUSPENDED_COMP and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_UTILITY_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_CAMPER_TRAVEL_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINITORT']/NODE[@ID='MINITORTRATE']/ATTRIBUTES/@LIMIT" />
			</xsl:when>
			<xsl:otherwise>
			0.00		
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- ***********************************  Road Services  ****************************************************************** -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="ROADSERVICES">
		<xsl:variable name="PROADSERVICE" select="VEHICLES/VEHICLE/ROADSERVICE" />
		<xsl:choose>
			<xsl:when test="$PROADSERVICE = '' or $PROADSERVICE = 0 or VEHICLES/VEHICLE/VEHICLETYPE =$VT_SUSPENDED_COMP or VEHICLES/VEHICLE/VEHICLETYPE =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE =$VT_CAMPER_TRAVEL_TRAILER or  VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO'">
			0
		</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='ROADSERVICELIMIT']/ATTRIBUTES[@COVERAGE = $PROADSERVICE]/@RATE" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- ****************************************  Rental Reimbursement ****************************************************************** -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="RENTALREIMBURSEMENT">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_SUSPENDED_COMP or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_CAMPER_TRAVEL_TRAILER  or VEHICLES/VEHICLE/RENTALREIMBURSEMENT ='' or VEHICLES/VEHICLE/RENTALREIMBURSEMENT ='0' or  VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO'">
			0 
		</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/RENTALREIMMAXCOVG ='' or VEHICLES/VEHICLE/RENTALREIMMAXCOVG ='0'">
					0 
				</xsl:when>
					<xsl:otherwise>
						<xsl:variable name="PRENTALREIMLIMITDAY" select="VEHICLES/VEHICLE/RENTALREIMLIMITDAY"></xsl:variable>
						<xsl:variable name="PRENTALREIMMAXCOVG" select="VEHICLES/VEHICLE/RENTALREIMMAXCOVG"></xsl:variable>
						<xsl:variable name="VAR_PRENTALREIMMAXCOVG">
							<xsl:choose>
								<xsl:when test="contains(VEHICLES/VEHICLE/RENTALREIMMAXCOVG,',')">
									<xsl:value-of select="translate($PRENTALREIMMAXCOVG,'###,###','#')" />
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="VEHICLES/VEHICLE/RENTALREIMMAXCOVG" />
								</xsl:otherwise>
							</xsl:choose>
						</xsl:variable>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='RENTALREIMBURSEMENT']/ATTRIBUTES[@DAYS = $PRENTALREIMLIMITDAY and @MAXCOVERAGE = $VAR_PRENTALREIMMAXCOVG]/@RATE" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of Rental Reimbursement-->
	<!-- ********************************************************************************************************************************************* -->
	<!-- ************************************   MCCA Fee  ****************************************************************** -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="MCCAFEE">
		<xsl:variable name="VAR_QUOTEEFFECTIVEDATE" select="POLICY/QUOTEEFFDATE" />
		<xsl:variable name="P_YEAR" select="substring($VAR_QUOTEEFFECTIVEDATE,7,4)" />
		<xsl:variable name="P_MONTH" select="substring($VAR_QUOTEEFFECTIVEDATE,1,2)" />
		<xsl:variable name="P_DAY" select="substring($VAR_QUOTEEFFECTIVEDATE,4,5)" />
		<xsl:variable name="VAR_MCCAFEEEFFECTIVEDATENEW" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MCCAFEE']/NODE[@ID='MCCAFEESEMIANNUAL_NEW']/ATTRIBUTES/@RATE_EFFECTIVEDATE" />
		<xsl:variable name="P_YEAR_MCCAFEEEFFECTIVEDATENEW" select="substring($VAR_MCCAFEEEFFECTIVEDATENEW,7,4)" />
		<xsl:variable name="P_MONTH_MCCAFEEEFFECTIVEDATENEW" select="substring($VAR_MCCAFEEEFFECTIVEDATENEW,1,2)" />
		<xsl:variable name="P_DAY_MCCAFEEEFFECTIVEDATENEW" select="substring($VAR_MCCAFEEEFFECTIVEDATENEW,4,5)" />
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='MICHIGAN' and VEHICLES/VEHICLE/MEDPM != '' and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_UTILITY_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_CAMPER_TRAVEL_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE != $VT_ANTIQUE_CAR and VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO'">
				<xsl:choose>
					<xsl:when test="$P_YEAR &lt;= $P_YEAR_MCCAFEEEFFECTIVEDATENEW and $P_MONTH &lt; $P_MONTH_MCCAFEEEFFECTIVEDATENEW and $P_DAY &lt; $P_DAY_MCCAFEEEFFECTIVEDATENEW">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MCCAFEE']/NODE[@ID='MCCAFEESEMIANNUALOLD']/ATTRIBUTES/@RATE_PER_VEHICLE" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MCCAFEE']/NODE[@ID='MCCAFEESEMIANNUALNEW']/ATTRIBUTES/@RATE_PER_VEHICLE" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of MCCA Fee-->
	<!-- ********************************************************************************************************************************************* -->
	<!--MCCHA FEE -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="MCCHAFEE">
		<xsl:variable name="VAR_QUOTEEFFECTIVEDATE" select="POLICY/QUOTEEFFDATE" />
		<xsl:variable name="P_YEAR" select="substring($VAR_QUOTEEFFECTIVEDATE,7,4)" />
		<xsl:variable name="P_MONTH" select="substring($VAR_QUOTEEFFECTIVEDATE,1,2)" />
		<xsl:variable name="P_DAY" select="substring($VAR_QUOTEEFFECTIVEDATE,4,5)" />
		<xsl:variable name="VAR_MCCAFEEEFFECTIVEDATENEW" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MCCAFEE']/NODE[@ID='HISTORICALASSMNTMCCAFEE_NEW']/ATTRIBUTES/@RATE_EFFECTIVEDATE" />
		<xsl:variable name="P_YEAR_MCCAFEEEFFECTIVEDATENEW" select="substring($VAR_MCCAFEEEFFECTIVEDATENEW,7,4)" />
		<xsl:variable name="P_MONTH_MCCAFEEEFFECTIVEDATENEW" select="substring($VAR_MCCAFEEEFFECTIVEDATENEW,1,2)" />
		<xsl:variable name="P_DAY_MCCAFEEEFFECTIVEDATENEW" select="substring($VAR_MCCAFEEEFFECTIVEDATENEW,4,5)" />
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='MICHIGAN' and  VEHICLES/VEHICLE/MEDPM != '' and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_SUSPENDED_COMP and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_UTILITY_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_CAMPER_TRAVEL_TRAILER">
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = $VT_ANTIQUE_CAR">
						<xsl:choose>
							<xsl:when test="$P_YEAR &lt;= $P_YEAR_MCCAFEEEFFECTIVEDATENEW and $P_MONTH &lt; $P_MONTH_MCCAFEEEFFECTIVEDATENEW and $P_DAY &lt; $P_DAY_MCCAFEEEFFECTIVEDATENEW">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MCCAFEE']/NODE[@ID='HISTORICALASSMNTMCCAFEE_OLD']/ATTRIBUTES/@RATE_PER_VEHICLE" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MCCAFEE']/NODE[@ID='HISTORICALASSMNTMCCAFEE_NEW']/ATTRIBUTES/@RATE_PER_VEHICLE" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of MCCHA Fee  -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- ************************************   AGE   ************************************************************************ -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="AGE">
		<xsl:param name="FACTORELEMENT" /> <!-- Depending on the factor element COMP/COLL..etc -->
		<xsl:variable name="VAR_AGE" select="VEHICLES/VEHICLE/AGE"></xsl:variable>
		<xsl:variable name="VAR_MAXAGE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEAGEGROUPOTHER']/NODE[@ID='VEHICLEAGEGROUPCOMP']/@MAXAGE" />
		<xsl:variable name="PAGE">
			<xsl:choose>
				<xsl:when test="$VAR_AGE &gt; $VAR_MAXAGE">
					<xsl:value-of select="$VAR_MAXAGE" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$VAR_AGE" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PAGE &gt; 0">
				<xsl:choose>
					<xsl:when test="$FACTORELEMENT ='COMP'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEAGEGROUPOTHER']/NODE[@ID='VEHICLEAGEGROUPCOMP']/ATTRIBUTES[@AGE = $PAGE ]/@RELATIVITY" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT ='COLL'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEAGEGROUPCOLLISION']/NODE[@ID='VEHICLEAGEGROUPCOLL']/ATTRIBUTES[@AGE = $PAGE ]/@RELATIVITY" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT ='LTDCOLL'">
					1.00
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- ******************************** Anti Breaking System  (ABS) ************************************************************************ -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="ABS">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE !='' and normalize-space(VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE)!='0'">
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/ISANTILOCKBRAKESDISCOUNTS= 'TRUE' and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_SUSPENDED_COMP and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_UTILITY_TRAILER  and VEHICLES/VEHICLE/VEHICLETYPE != $VT_CLASSIC_CAR and VEHICLES/VEHICLE/VEHICLETYPE != $VT_ANTIQUE_CAR and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_CAMPER_TRAVEL_TRAILER">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ANTILOCKBREAKSYSTEM']/NODE[@ID='ABS']/ATTRIBUTES/@FACTOR" />
					</xsl:when>
					<xsl:otherwise>
			1.00
		</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ABS_DISPLAY">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE !='' and normalize-space(VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE)!='0'">
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/ISANTILOCKBRAKESDISCOUNTS= 'TRUE' and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_SUSPENDED_COMP and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_UTILITY_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE != $VT_CLASSIC_CAR and VEHICLES/VEHICLE/VEHICLETYPE != $VT_ANTIQUE_CAR and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_CAMPER_TRAVEL_TRAILER and  VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO'">
				Included
		</xsl:when>
					<xsl:otherwise>
			0
		</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="ABS_CREDIT">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE !='' and normalize-space(VEHICLES/VEHICLE/COLLISIONDEDUCTIBLE)!='0'">
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/ISANTILOCKBRAKESDISCOUNTS= 'TRUE' and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_SUSPENDED_COMP and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_UTILITY_TRAILER  and VEHICLES/VEHICLE/VEHICLETYPE != $VT_CLASSIC_CAR and VEHICLES/VEHICLE/VEHICLETYPE != $VT_ANTIQUE_CAR and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_CAMPER_TRAVEL_TRAILER">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ANTILOCKBREAKSYSTEM']/NODE[@ID='ABS']/ATTRIBUTES/@CREDIT" />%
		</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- ************************************  Seat Belt   ************************************************************************ -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="SEATBELT">
		<xsl:choose>
			<xsl:when test="(VEHICLES/VEHICLE/WEARINGSEATBELT = 'TRUE' or VEHICLES/VEHICLE/WEARINGSEATBELT = 'Yes' or VEHICLES/VEHICLE/WEARINGSEATBELT = 'YES')  and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_SUSPENDED_COMP and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_UTILITY_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_CAMPER_TRAVEL_TRAILER and POLICY/STATENAME != 'INDIANA' ">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SEATBELTCREDIT']/NODE[@ID='SEATBELT']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SEATBELT_DISPLAY">
		<xsl:choose>
			<xsl:when test="(VEHICLES/VEHICLE/WEARINGSEATBELT = 'TRUE' or VEHICLES/VEHICLE/WEARINGSEATBELT = 'Yes' or VEHICLES/VEHICLE/WEARINGSEATBELT = 'YES') and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_SUSPENDED_COMP and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_UTILITY_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_CAMPER_TRAVEL_TRAILER and POLICY/STATENAME != 'INDIANA' and  VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO'">
			Included
		</xsl:when>
			<xsl:otherwise>
			0
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SEAT_BELTS_CREDIT_PERCENT">
		<xsl:choose>
			<xsl:when test="(normalize-space(VEHICLES/VEHICLE/WEARINGSEATBELT) = 'TRUE' or normalize-space(VEHICLES/VEHICLE/WEARINGSEATBELT) = 'Yes'  or normalize-space(VEHICLES/VEHICLE/WEARINGSEATBELT) = 'YES') and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_SUSPENDED_COMP and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_UTILITY_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_CAMPER_TRAVEL_TRAILER and POLICY/STATENAME != 'INDIANA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SEATBELTCREDIT']/NODE[@ID='SEATBELT']/ATTRIBUTES/@CREDIT" />
			</xsl:when>
			<xsl:otherwise>
			0
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- ************************************  Air Bag  ********************************************************************************************** -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="AIRBAG">
		<xsl:variable name="PAIRBAGDISCOUNT" select="VEHICLES/VEHICLE/AIRBAGDISCOUNT" />
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_SUSPENDED_COMP or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_CAMPER_TRAVEL_TRAILER  or $PAIRBAGDISCOUNT = '' or $PAIRBAGDISCOUNT = '0'">
				1.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='AIRBAGCREDIT']/NODE[@ID='AIRBAG']/ATTRIBUTES[@ID=$PAIRBAGDISCOUNT]/@FACTOR" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="AIRBAG_DISPLAY">
		<xsl:variable name="PAIRBAGDISCOUNT" select="VEHICLES/VEHICLE/AIRBAGDISCOUNT" />
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_SUSPENDED_COMP or   VEHICLES/VEHICLE/VEHICLETYPE  =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_CAMPER_TRAVEL_TRAILER or $PAIRBAGDISCOUNT = '' or $PAIRBAGDISCOUNT = '0' or  VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO'">
				0
		</xsl:when>
			<xsl:otherwise>
			Included
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="AIRBAG_DISCOUNT_PERCENT">
		<xsl:variable name="PAIRBAGDISCOUNT" select="VEHICLES/VEHICLE/AIRBAGDISCOUNT" />
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_SUSPENDED_COMP or   VEHICLES/VEHICLE/VEHICLETYPE  =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_CAMPER_TRAVEL_TRAILER  or $PAIRBAGDISCOUNT = '' or $PAIRBAGDISCOUNT = '0'">
				0
		</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='AIRBAGCREDIT']/NODE[@ID='AIRBAG']/ATTRIBUTES[@ID=$PAIRBAGDISCOUNT]/@CREDIT" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- ********************************** Loan / Lease Gap  ************************************************************************ -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="LOANLEASEGAP">
		<!-- 1. ONLY ON NEW (CURRENT) VEHICLES ... 
		 2. LEASED: 10% TO EACH COMPREHENSIVE AND COLLISION AND COMPARE TO MIN
		 3. LOAN: 5% TO EACH COMPREHENSIVE AND COLLISION AND COMPARE TO MIN -->
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_SUSPENDED_COMP or   VEHICLES/VEHICLE/VEHICLETYPE  =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_CAMPER_TRAVEL_TRAILER  or VEHICLES/VEHICLE/LOANLEASEGAP =0 or VEHICLES/VEHICLE/LOANLEASEGAP ='' or  VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO'">
			0.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="GAPFACTOR">
					<xsl:choose>
						<xsl:when test="VEHICLES/VEHICLE/LOANLEASEGAP = 'LOAN'">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='A11LOANLEASEGAP']/NODE[@ID='LOANLEASEGAP']/ATTRIBUTES/@LOANFACTOR" />
						</xsl:when>
						<xsl:when test="VEHICLES/VEHICLE/LOANLEASEGAP = 'LEASE'">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='A11LOANLEASEGAP']/NODE[@ID='LOANLEASEGAP']/ATTRIBUTES/@LEASEFACTOR" />
						</xsl:when>
						<xsl:otherwise>
						0.00
					</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:variable name="MINVALUECOMP">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='A11LOANLEASEGAP']/NODE[@ID='LOANLEASEGAP']/ATTRIBUTES/@MINPREMIUMPERCARCOMP" />
				</xsl:variable>
				<xsl:variable name="MINVALUECOLL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='A11LOANLEASEGAP']/NODE[@ID='LOANLEASEGAP']/ATTRIBUTES/@MINPREMIUMPERCARCOLL" />
				</xsl:variable>
				<!--Final COMP Value -->
				<xsl:variable name="COVERAGECOMPVALUE">
					<xsl:variable name="VAR1">
						<xsl:call-template name="COMP" />
					</xsl:variable>
					<xsl:value-of select="round($VAR1 * $GAPFACTOR)" />
				</xsl:variable>
				<xsl:variable name="FINALCOMPVALUE">
					<xsl:choose>
						<xsl:when test="$COVERAGECOMPVALUE &lt;  $MINVALUECOMP">
							<xsl:value-of select="$MINVALUECOMP" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$COVERAGECOMPVALUE" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<!-- Final COLL Value -->
				<xsl:variable name="COVERAGECOLLVALUE">
					<xsl:variable name="VAR1">
						<xsl:choose>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONTYPE ='LIMITED'">
								<xsl:call-template name="LTDCOLL" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:call-template name="COLL" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:variable>
					<xsl:value-of select="round($VAR1 * $GAPFACTOR)" />
				</xsl:variable>
				<xsl:variable name="FINALCOLLVALUE">
					<xsl:choose>
						<xsl:when test="$COVERAGECOLLVALUE &lt;  $MINVALUECOLL">
							<xsl:value-of select="$MINVALUECOLL" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$COVERAGECOLLVALUE" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<!-- return the sum  -->
				<xsl:value-of select="$FINALCOLLVALUE + $FINALCOMPVALUE" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- *********************************  Sound reproducing tapes ************************************************************************ -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="SOUNDREPRODUCINGTAPES">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/IS200SOUNDREPRODUCING='TRUE' and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_SUSPENDED_COMP and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_UTILITY_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_CAMPER_TRAVEL_TRAILER and  VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='SOUNDREPRODUCINGTAPES']/ATTRIBUTES/@PREMIUM" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="SOUNDREPRODUCINGTAPESLIMIT">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/IS200SOUNDREPRODUCING = 'TRUE' and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_SUSPENDED_COMP and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_UTILITY_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_CAMPER_TRAVEL_TRAILER">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='SOUNDREPRODUCINGTAPES']/ATTRIBUTES/@LIMIT" />
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Sound receiving and transmitting tapes -->
	<xsl:template name="SOUNDRECVTRANSEQUIPMENT">
		<xsl:variable name="VAR_SOUND_RECEIVING_TRANSMITTING_SYSTEM" select="translate(VEHICLES/VEHICLE/SOUNDRECEIVINGTRANSMITTINGSYSTEM,',','')" />
		<xsl:choose>
			<xsl:when test="$VAR_SOUND_RECEIVING_TRANSMITTING_SYSTEM &gt; 0 and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_SUSPENDED_COMP and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_UTILITY_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_CAMPER_TRAVEL_TRAILER and  VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO'">
				<xsl:variable name="VAR1">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='SOUNDRECVTRANSEQUIPMENT']/ATTRIBUTES/@PREMIUM" />
				</xsl:variable>
				<xsl:variable name="VAR2">
					<!-- <xsl:value-of select="VEHICLES/VEHICLE/SOUNDRECEIVINGTRANSMITTINGSYSTEM"/>	-->
					<xsl:value-of select="$VAR_SOUND_RECEIVING_TRANSMITTING_SYSTEM" />
				</xsl:variable>
				<xsl:variable name="VAR3">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='SOUNDRECVTRANSEQUIPMENT']/ATTRIBUTES/@PER_EACH_ADDITION_OF" />
				</xsl:variable>
				<xsl:value-of select="round($VAR1 * ($VAR2 div $VAR3))" />
			</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- **********************************  Waiver Work Loss Waiver ************************************************************************ -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="WAIVEWORKLOSS">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/WAIVEWORKLOSS = 'TRUE' and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_SUSPENDED_COMP and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_UTILITY_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_CAMPER_TRAVEL_TRAILER ">
			 
			0.00	
		</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PIP_DISPLAY">
		<xsl:variable name="PPIPDISPLAY">
			<xsl:call-template name="COVERAGETYPE" />
		</xsl:variable>
		<xsl:variable name="VAR_COVERAGE_TEXT">
			<xsl:call-template name="COVERAGETYPE_DISPLAY" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE != $VT_SUSPENDED_COMP   and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_UTILITY_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_CAMPER_TRAVEL_TRAILER  and $PPIPDISPLAY &gt; 0.00 and $VAR_COVERAGE_TEXT != 0 and  VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO'">
			'Included'
		</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ************************************************************************************************************************************ -->
	<!-- *****************************  Extra Equipment :Comprehensive ********************************************************************************* -->
	<!-- ************************************************************************************************************************************ -->
	<xsl:template name="EXTRAEQUIPMENTCOMP">
		<xsl:choose>
			<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_CAMPER_TRAVEL_TRAILER or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_UTILITY_TRAILER">0.00</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="PDEDUCTIBLE" select="VEHICLES/VEHICLE/EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE" />
				<xsl:variable name="PINSURANCEAMOUNT" select="VEHICLES/VEHICLE/INSURANCEAMOUNT" />
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE != $VT_SUSPENDED_COMP and $PINSURANCEAMOUNT != '' and $PINSURANCEAMOUNT != 'NO COVERAGE' and $PINSURANCEAMOUNT &gt; 0 and normalize-space($PDEDUCTIBLE) !='' and normalize-space($PDEDUCTIBLE) != '0' and  VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO'">
						<!-- Check for Rate per value -->
						<xsl:variable name="PRATEPERVALUE">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COMP']/@RATE_PER_VALUE" />
						</xsl:variable>
						<!-- Check the Extra Equipment component -->
						<xsl:variable name="PEXTRAEQUIPMENTCOMP">
							<xsl:choose>
								<xsl:when test="'$PDEDUCTIBLE'='' or $PDEDUCTIBLE=0">
							0.00
						</xsl:when>
								<xsl:when test="'$PDEDUCTIBLE'='250'">
									<xsl:variable name="VAR1">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COMP']/@RATE" />
									</xsl:variable>
									<xsl:value-of select="round($VAR1 * ($PINSURANCEAMOUNT div $PRATEPERVALUE))" />
								</xsl:when>
								<!-- Multiply rate with Deductible Relativity-->
								<xsl:otherwise>
									<xsl:variable name="VAR1">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID = 'MISCELLANEOUSCOVERAGES']/NODE[@ID = 'EXTRAEQUIPMENT']/ATTRIBUTES[@RISK = 'COMP']/@RATE" />
									</xsl:variable>
									<xsl:variable name="VAR2">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLEO']/NODE[@ID='DEDTBLO']/ATTRIBUTES[@DEDUCTIBLE=$PDEDUCTIBLE]/@RELATIVITY" />
									</xsl:variable>
									<xsl:value-of select="round($VAR1 *($PINSURANCEAMOUNT div $PRATEPERVALUE) * $VAR2)" />
								</xsl:otherwise>
							</xsl:choose>
						</xsl:variable>
						<!-- Check for Minimum Premium  -->
						<xsl:variable name="PMINIMUMVALUE">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COMP']/@MINIMUM_PREMIUM" />
						</xsl:variable>
						<!-- If the extra equipment component is less than the minimum premium component then return minimum premium -->
						<xsl:choose>
							<xsl:when test="$PEXTRAEQUIPMENTCOMP=0 or '$PEXTRAEQUIPMENTCOMP' =''">
						0.00
					</xsl:when>
							<xsl:otherwise>
								<xsl:choose>
									<xsl:when test="$PEXTRAEQUIPMENTCOMP &gt; $PMINIMUMVALUE">
										<xsl:value-of select="$PEXTRAEQUIPMENTCOMP" />
									</xsl:when>
									<xsl:when test="$PEXTRAEQUIPMENTCOMP &lt;= $PMINIMUMVALUE">
										<xsl:value-of select="$PMINIMUMVALUE" />
									</xsl:when>
									<xsl:otherwise>0.00 </xsl:otherwise>
								</xsl:choose>
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ************************************************************************************************************************************ -->
	<!-- *********************************  Extra Equipment :Collision ********************************************************************************* -->
	<!-- ************************************************************************************************************************************ -->
	<xsl:template name="EXTRAEQUIPMENTCOLL">
		<xsl:choose>
			<xsl:when test="normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_CAMPER_TRAVEL_TRAILER or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_UTILITY_TRAILER">0.00</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="PDEDUCTIBLE" select="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONDEDUCTIBLE" />
				<xsl:variable name="PCOLLISIONTYPE" select="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONTYPE" />
				<xsl:variable name="PINSURANCEAMOUNT" select="VEHICLES/VEHICLE/INSURANCEAMOUNT" />
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE != $VT_SUSPENDED_COMP and '$PINSURANCEAMOUNT' !='' and '$PINSURANCEAMOUNT' !='NO COVERAGE' and $PINSURANCEAMOUNT &gt; 0 and normalize-space($PDEDUCTIBLE) !='' and normalize-space($PDEDUCTIBLE) != '0' and  VEHICLES/VEHICLE/VEHICLETYPE_SCO != 'SCO'">
						<!-- Check for Rate per value -->
						<xsl:variable name="PRATEPERVALUE">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE_PER_VALUE" />
						</xsl:variable>
						<!-- Check the Extra Equipment component -->
						<xsl:variable name="PEXTRAEQUIPMENTCOLL">
							<xsl:choose>
								<xsl:when test="POLICY/STATENAME='MICHIGAN'">
									<xsl:choose>
										<xsl:when test="($PINSURANCEAMOUNT != '' and $PINSURANCEAMOUNT != 'NO COVERAGE' and $PINSURANCEAMOUNT &gt; 0 and $PDEDUCTIBLE !='' and $PDEDUCTIBLE != '0') or ($PINSURANCEAMOUNT !='' and $PINSURANCEAMOUNT !='NO COVERAGE' and $PINSURANCEAMOUNT &gt; 0 and $PDEDUCTIBLE = '0'  and $PCOLLISIONTYPE = 'LIMITED')">
											<xsl:choose>
												<xsl:when test="normalize-space($PDEDUCTIBLE)='250'">
													<xsl:variable name="VAR1">
														<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE" />
													</xsl:variable>
													<xsl:value-of select="round($VAR1*  ($PINSURANCEAMOUNT div $PRATEPERVALUE))" />
												</xsl:when>
												<xsl:when test="normalize-space($PCOLLISIONTYPE)='LIMITED'">
													<xsl:variable name="VAR1">
														<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE" />
													</xsl:variable>
													<xsl:variable name="VAR2">
														<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@COLLISIONTYPE=$PCOLLISIONTYPE]/@RELATIVITY" />
													</xsl:variable>
													<xsl:value-of select="round($VAR1 * ($PINSURANCEAMOUNT div $PRATEPERVALUE) * $VAR2)" />
												</xsl:when>
												<!-- Multiply rate with Deductible Relativity-->
												<xsl:otherwise>
													<xsl:variable name="VAR1">
														<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE" />
													</xsl:variable>
													<xsl:variable name="VAR2">
														<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@COLLISIONTYPE=$PCOLLISIONTYPE and @DEDUCTIBLE=$PDEDUCTIBLE]/@RELATIVITY" />
													</xsl:variable>
													<xsl:value-of select="round($VAR1 * ($PINSURANCEAMOUNT div $PRATEPERVALUE) * $VAR2)" />
												</xsl:otherwise>
											</xsl:choose>
										</xsl:when>
										<xsl:otherwise>0.00</xsl:otherwise>
									</xsl:choose>
								</xsl:when>
								<xsl:when test="normalize-space(POLICY/STATENAME)='INDIANA'">
									<xsl:choose>
										<xsl:when test="($PINSURANCEAMOUNT != '' and $PINSURANCEAMOUNT != 'NO COVERAGE' and $PINSURANCEAMOUNT &gt; 0 and $PDEDUCTIBLE !='' and $PDEDUCTIBLE != '0')">
											<xsl:choose>
												<xsl:when test="'$PDEDUCTIBLE'='0'">0</xsl:when>
												<xsl:when test="$PDEDUCTIBLE=250">
													<xsl:variable name="VAR1">
														<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE" />
													</xsl:variable>
													<xsl:value-of select="round($VAR1*  ($PINSURANCEAMOUNT div $PRATEPERVALUE))" />
												</xsl:when>
												<xsl:when test="$PDEDUCTIBLE &gt; '0'">
													<xsl:variable name="VAR1">
														<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE" />
													</xsl:variable>
													<xsl:variable name="VAR2">
														<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@DEDUCTIBLE=$PDEDUCTIBLE]/@RELATIVITY" />
													</xsl:variable>
													<xsl:value-of select="round($VAR1 * ($PINSURANCEAMOUNT div $PRATEPERVALUE) * $VAR2)" />
												</xsl:when>
												<!-- Multiply rate with Deductible Relativity -->
												<xsl:otherwise> 0.00</xsl:otherwise>
											</xsl:choose>
										</xsl:when>
										<xsl:otherwise>0.00</xsl:otherwise>
									</xsl:choose>
								</xsl:when>
								<xsl:otherwise>0.00</xsl:otherwise>
							</xsl:choose>
						</xsl:variable>
						<!-- Check for Minimum Premium -->
						<xsl:variable name="PMINIMUMVALUE">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@MINIMUM_PREMIUM" />
						</xsl:variable>
						<!-- If the extra equipment component is less than the minimum premium component then return minimum premium -->
						<xsl:choose>
							<xsl:when test="'$PEXTRAEQUIPMENTCOLL'='' or ('$PEXTRAEQUIPMENTCOLL' ='0' and '$PCOLLISIONTYPE' !='LIMITED')">
					0.00
				</xsl:when>
							<xsl:otherwise>
								<xsl:choose>
									<xsl:when test="$PEXTRAEQUIPMENTCOLL &gt; $PMINIMUMVALUE">
										<xsl:value-of select="$PEXTRAEQUIPMENTCOLL" />
									</xsl:when>
									<xsl:when test="$PEXTRAEQUIPMENTCOLL &lt;= $PMINIMUMVALUE">
										<xsl:value-of select="$PMINIMUMVALUE" />
									</xsl:when>
									<xsl:otherwise>0.00</xsl:otherwise>
								</xsl:choose>
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>0.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ??????????????????????????????????????????????????????????????????????????????????????????????????? -->
	<!-- *****************  currently this template not in use GETEXTRAEQUIPCOLLISIONVALUE  ********************************************************************************* -->
	<!-- ??????????????????????????????????????????????????????????????????????????????????????????????????? -->
	<xsl:template name="GETEXTRAEQUIPCOLLISIONVALUE">
	<xsl:variable name="PDEDUCTIBLE" select="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONDEDUCTIBLE" />
	<xsl:variable name="PCOLLISIONTYPE" select="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONTYPE" />
	<xsl:variable name="PINSURANCEAMOUNT" select="VEHICLES/VEHICLE/INSURANCEAMOUNT" />
	
			<!-- Check for Rate per value -->
			<xsl:variable name="PRATEPERVALUE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE_PER_VALUE" />
		</xsl:variable>
			
			<!-- Check the Extra Equipment component -->		
			<xsl:variable name="PEXTRAEQUIPMENTCOLL">
			<xsl:choose>
				<xsl:when test="POLICY/STATENAME='MICHIGAN'">
					<xsl:choose>
						<xsl:when test="$PDEDUCTIBLE=250">
								(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE" />)
								*
								(<xsl:value-of select="$PINSURANCEAMOUNT" /> DIV <xsl:value-of select="$PRATEPERVALUE" />)
							</xsl:when>
						<xsl:when test="'$PCOLLISIONTYPE'='LIMITED'">
								(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE" />)
								*
								(<xsl:value-of select="$PINSURANCEAMOUNT" /> DIV <xsl:value-of select="$PRATEPERVALUE" />)
								*
								(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@COLLISIONTYPE=$PCOLLISIONTYPE]/@RELATIVITY" />)
							</xsl:when>
						<!-- Multiply rate with Deductible Relativity-->
						<xsl:otherwise>
								(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE" />)
								*
								(<xsl:value-of select="$PINSURANCEAMOUNT" /> DIV <xsl:value-of select="$PRATEPERVALUE" />)
								*
								(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@COLLISIONTYPE=$PCOLLISIONTYPE and @DEDUCTIBLE=$PDEDUCTIBLE]/@RELATIVITY" />)
							</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:when test="POLICY/STATENAME = 'INDIANA'">
					<xsl:choose>
						<xsl:when test="$PDEDUCTIBLE=250">
								(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE" />)
								*
								( <xsl:value-of select="$PINSURANCEAMOUNT" /> DIV <xsl:value-of select="$PRATEPERVALUE" />)
							</xsl:when>
						<xsl:when test="'$PCOLLISIONTYPE'='LIMITED'">
								(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE" />)
								*
								(<xsl:value-of select="$PINSURANCEAMOUNT" /> DIV <xsl:value-of select="$PRATEPERVALUE" />)
								*
								(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@COLLISIONTYPE=$PCOLLISIONTYPE]/@RELATIVITY" />)
							
							</xsl:when>
						<!-- Multiply rate with Deductible Relativity -->
						<xsl:otherwise> 
								(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE" />)
								*
								(<xsl:value-of select="$PINSURANCEAMOUNT" /> DIV <xsl:value-of select="$PRATEPERVALUE" />)
								*
								(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@DEDUCTIBLE=$PDEDUCTIBLE]/@RELATIVITY" />)
							</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:otherwise>
						0.00
					</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
			
			<!-- Check for Minimum Premium -->
			<xsl:variable name="PMINIMUMVALUE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@MINIMUM_PREMIUM" />
		</xsl:variable>
				 
			<!-- If the extra equipment component is less than the minimum premium component then return minimum premium -->							
			(
				<xsl:choose>
			<xsl:when test="('$PEXTRAEQUIPMENTCOLL' ='0' and '$PCOLLISIONTYPE' !='LIMITED') or '$PEXTRAEQUIPMENTCOLL' =''">
						0.00
					</xsl:when>
			<xsl:otherwise>
						IF (<xsl:value-of select="$PEXTRAEQUIPMENTCOLL" /> &gt; <xsl:value-of select="$PMINIMUMVALUE" />)
						THEN
							<xsl:value-of select="$PEXTRAEQUIPMENTCOLL" />
						ELSE IF (<xsl:value-of select="$PEXTRAEQUIPMENTCOLL" /> &lt;= <xsl:value-of select="$PMINIMUMVALUE" />)
						THEN
							<xsl:value-of select="$PMINIMUMVALUE" />
						ELSE
							0.00
					</xsl:otherwise>
		</xsl:choose>	
			)	 		
</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- ************************************  Territory Base  **************************************************************************************** -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="TERRITORYBASE">
		<xsl:param name="FACTORELEMENT" /> <!-- Depending on the factor element BI/PD..etc -->
		<xsl:choose>
			<!-- For Michigan Automobile -->
			<xsl:when test="POLICY/STATENAME ='MICHIGAN'">
				<xsl:call-template name="TERRITORYBASE_MICHIGAN">
					<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
				</xsl:call-template>
			</xsl:when>
			<!-- For Indiana Automobile -->
			<xsl:when test="POLICY/STATENAME ='INDIANA'">
				<xsl:call-template name="TERRITORYBASE_INDIANA">
					<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
				</xsl:call-template>
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Territory base michigan -->
	<xsl:template name="TERRITORYBASE_MICHIGAN">
		<xsl:param name="FACTOR" /> <!-- Depending on the factor element BI/PD..etc -->
		<xsl:variable name="PTERRITORYCODE" select="VEHICLES/VEHICLE/TERRCODEGARAGEDLOCATION" />
		<xsl:choose>
			<xsl:when test="$PTERRITORYCODE =''">
			1.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="normalize-space($FACTOR) = 'BI'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE=$PTERRITORYCODE]/@BIBASE" />
					</xsl:when>
					<xsl:when test="normalize-space($FACTOR) = 'PD'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE=$PTERRITORYCODE]/@PDBASE" />
					</xsl:when>
					<xsl:when test="normalize-space($FACTOR) = 'PPI'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE=$PTERRITORYCODE]/@PPIBASE" />
					</xsl:when>
					<xsl:when test="normalize-space($FACTOR) = 'PIP'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE=$PTERRITORYCODE]/@PIPBASE" />
					</xsl:when>
					<xsl:when test="normalize-space($FACTOR) = 'UM'"> 
					1.00
					</xsl:when>
					<xsl:when test="normalize-space($FACTOR) = 'UIM'"> 
					1.00
					</xsl:when>
					<xsl:when test="$FACTOR = 'COMP'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPREHENSIVETERRITORYBASE']/NODE[@ID ='COMPREHENSIVETERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE ]/@BASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'COLL'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLISIONTERRITORYBASE']/NODE[@ID ='COLLISIONTERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE ]/@BASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'LTDCOLL'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLISIONTERRITORYBASE']/NODE[@ID ='COLLISIONTERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE ]/@BASE" />
					</xsl:when>
					<xsl:otherwise> 
				1.00
			</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of Territory base michigan -->
	<!-- Territory base Indiana -->
	<xsl:template name="TERRITORYBASE_INDIANA">
		<xsl:param name="FACTOR" /> <!-- Depending on the factor element BI/PD..etc -->
		<xsl:variable name="PTERRITORYCODE" select="VEHICLES/VEHICLE/TERRCODEGARAGEDLOCATION" /> <!--POLICY/TERRITORY-->
		<xsl:choose>
			<xsl:when test="$PTERRITORYCODE =''">
			1.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="$FACTOR = 'BI'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE= $PTERRITORYCODE ]/@BIBASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'PD'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE= $PTERRITORYCODE ]/@PDBASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'MEDPAY'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE= $PTERRITORYCODE ]/@MEDPAYBASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'UM'">
						1.00
				</xsl:when>
					<xsl:when test="$FACTOR = 'UIM'">
						1.00
				</xsl:when>
					<xsl:when test="$FACTOR = 'COMP'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPREHENSIVETERRITORYBASE']/NODE[@ID ='COMPREHENSIVETERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE ]/@BASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'COLL'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLISIONTERRITORYBASE']/NODE[@ID ='COLLISIONTERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE ]/@BASE" />
					</xsl:when>
					<xsl:when test="$FACTOR = 'LTDCOLL'">
						1.00
				</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--  End of Territory base Indiana -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- ************************************ Start of Insurance Score ************************************************************************ -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="INSURANCESCORE">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE  =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_CAMPER_TRAVEL_TRAILER ">
			 1.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="INS1" select="POLICY/INSURANCESCORE"></xsl:variable>
				<xsl:variable name="INS" select="translate(translate($INS1,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
				<xsl:choose>
					<xsl:when test="normalize-space($INS)='NOHITNOSCORE'"> <!-- NO HIT NO SCORE -->
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = 'N']/@FACTOR" />
					</xsl:when>
					<xsl:when test="normalize-space($INS)&gt;=0">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@FACTOR" />
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="INSURANCESCORE_DISPLAY">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE  =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_CAMPER_TRAVEL_TRAILER ">
			0
		</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="INS1" select="POLICY/INSURANCESCORE"></xsl:variable>
				<xsl:variable name="INS">
					<xsl:value-of select="translate(translate($INS1,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
				</xsl:variable>
				<!-- FOR MAXIMUM SCORE -->
				<xsl:variable name="VAR_MAX_SCORE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MAX = 'MAX_SCORE']/@MINSCORE" />
				<xsl:variable name="INSDISCOUNT">
					<xsl:choose>
						<xsl:when test="$INS = 0">
							<xsl:value-of select="$INS"></xsl:value-of>
						</xsl:when>
						<xsl:when test="$INS = ''">1.00</xsl:when>
						<xsl:when test="$INS = 'NOHITNOSCORE'"> <!-- NO HIT NO SCORE -->
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = 'N']/@FACTOR" />
						</xsl:when>
						<xsl:when test="$INS &gt;= $VAR_MAX_SCORE">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = $VAR_MAX_SCORE]/@FACTOR" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@FACTOR" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$INSDISCOUNT = 0 or $INSDISCOUNT='' or $INSDISCOUNT=1">0</xsl:when>
					<xsl:when test="$INSDISCOUNT &gt; 0">Included</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- ************************************ Start of Optimal (MOTOR HOME) ************************************************************************ -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="OPTIMAL">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = 'MH'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MOTORHOME']/NODE[@ID ='MOTORHOME_DISCOUNT']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>  
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- ************************************ Antique or Classic or Motor home  ************************************************************************ -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="TYPEOFVEHICLE_1">
	1.00
</xsl:template>
	<xsl:template name="TYPEOFVEHICLE">
		<xsl:param name="FACTORELEMENT" /> <!-- Depending on the factor element BI/PD..etc -->
		<xsl:variable name="PVEHICLETYPE" select="VEHICLES/VEHICLE/VEHICLETYPE" />
		<xsl:choose>
			<xsl:when test="$FACTORELEMENT = 'BI'">
				<xsl:choose>
					<xsl:when test="$PVEHICLETYPE='ANC'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@BI" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='CLC'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@BI" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='SA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@BI" />
					</xsl:when>
					<xsl:otherwise>
				1.00
			</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT = 'PD'">
				<xsl:choose>
					<xsl:when test="$PVEHICLETYPE='ANC'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@PD" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='CLC'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@PD" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='SA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@PD" />
					</xsl:when>
					<xsl:otherwise>
				1.00
			</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT = 'MEDPAY'">
				<xsl:choose>
					<xsl:when test="$PVEHICLETYPE='ANC'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@MEDPAY" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='CLC'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@MEDPAY" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='SA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@MEDPAY" />
					</xsl:when>
					<xsl:otherwise>
					1.00
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT = 'PPI'">
				<xsl:choose>
					<xsl:when test="$PVEHICLETYPE='ANC'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@PPI" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='CLC'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@PPI" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='SA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@PPI" />
					</xsl:when>
					<xsl:otherwise>
				1.00
			</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT = 'PIP'">
				<xsl:choose>
					<xsl:when test="$PVEHICLETYPE='ANC'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@PIP" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='CLC'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@PIP" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='SA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@PIP" />
					</xsl:when>
					<xsl:otherwise>
				1.00
			</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT = 'UM'">
				<xsl:choose>
					<xsl:when test="$PVEHICLETYPE='ANC'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@UM" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='CLC'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@UM" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='SA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@UM" />
					</xsl:when>
					<xsl:otherwise>
				1.00
			</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT = 'UIM'">
				<xsl:choose>
					<xsl:when test="$PVEHICLETYPE='ANC'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@UIM" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='CLC'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@UIM" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='SA'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@UIM" />
					</xsl:when>
					<xsl:otherwise>
				1.00
			</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT = 'COMP'">
				<xsl:choose>
					<xsl:when test="$PVEHICLETYPE='ANC'">
						<xsl:variable name="VAR1">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@COMP50" />
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:call-template name="FETCHDEDUCTIBLEFACTOR">
								<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:value-of select="$VAR1*$VAR2" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='CLC'">
						<xsl:variable name="VAR1">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@COMP50" />
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:call-template name="FETCHDEDUCTIBLEFACTOR">
								<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:value-of select="$VAR1*$VAR2" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='SA'">
						<xsl:variable name="VAR1">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@COMP50" />
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:call-template name="FETCHDEDUCTIBLEFACTOR">
								<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:value-of select="$VAR1*$VAR2" />
					</xsl:when>
					<xsl:otherwise>
				1.00
			</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT = 'COLL' or $FACTORELEMENT = 'LTDCOLL'">
				<xsl:choose>
					<xsl:when test="$PVEHICLETYPE='ANC'">
						<xsl:variable name="VAR1">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@COLL250" />
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:call-template name="FETCHDEDUCTIBLEFACTOR">
								<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:value-of select="$VAR1*$VAR2" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='CLC'">
						<xsl:variable name="VAR1">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@COLL250" />
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:call-template name="FETCHDEDUCTIBLEFACTOR">
								<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:value-of select="$VAR1*$VAR2" />
					</xsl:when>
					<xsl:when test="$PVEHICLETYPE='SA'">
						<xsl:variable name="VAR1">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@COLL250" />
						</xsl:variable>
						<xsl:variable name="VAR2">
							<xsl:call-template name="FETCHDEDUCTIBLEFACTOR">
								<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
							</xsl:call-template>
						</xsl:variable>
						<xsl:value-of select="$VAR1*$VAR2" />
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
	<!-- ********************************************************************************************************************************************* -->
	<!--  **********************  Deductible Factor for Comprehensive & Collision ********************************************************************  -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="FETCHDEDUCTIBLEFACTOR">
		<xsl:param name="FACTOR" /> <!-- Depending on the factor  BI/PD..etc -->
		<xsl:choose>
			<xsl:when test="$FACTOR = 'COMP'">
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 50">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE50" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 100">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE100" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 150">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE150" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 200">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE200" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 250">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE250" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 500">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE500" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 750">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE750" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 1000">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE1000" />
					</xsl:when>
					<xsl:otherwise>
					1.00
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTOR = 'COLL' or $FACTOR = 'LTDCOLL'">
				<xsl:choose>
					<xsl:when test="POLICY/STATENAME = 'MICHIGAN'">
						<xsl:choose>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 50">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE50" />
							</xsl:when>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 100">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE100" />
							</xsl:when>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 150">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE150" />
							</xsl:when>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 200">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE200" />
							</xsl:when>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 250">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE250" />
							</xsl:when>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 500 and VEHICLES/VEHICLE/COVGCOLLISIONTYPE='BROAD'">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE500" />
							</xsl:when>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 500 and VEHICLES/VEHICLE/COVGCOLLISIONTYPE='REGULAR'">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE500REG" />
							</xsl:when>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 400 and VEHICLES/VEHICLE/COVGCOLLISIONTYPE='BROAD'">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE500" />
							</xsl:when>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 400 and VEHICLES/VEHICLE/COVGCOLLISIONTYPE='REGULAR'">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE400REG" />
							</xsl:when>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 750">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE750" />
							</xsl:when>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 1000">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE1000" />
							</xsl:when>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONTYPE = 'LIMITED'">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLELTD" />
							</xsl:when>
							<xsl:otherwise>
							1.00
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>
						<xsl:choose>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 50">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE50" />
							</xsl:when>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 100">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE100" />
							</xsl:when>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 150">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE150" />
							</xsl:when>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 200">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE200" />
							</xsl:when>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 250">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE250" />
							</xsl:when>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 500">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE500" />
							</xsl:when>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 750">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE750" />
							</xsl:when>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 1000">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE1000" />
							</xsl:when>
							<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONTYPE = 'LIMITED'">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLELTD" />
							</xsl:when>
							<xsl:otherwise>
							1.00
							</xsl:otherwise>
						</xsl:choose>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- End of Vehicle Type -->
	<!-- ********************************************************************************************************************************************* -->
	<!--  *****************************************  Driver Class  ************************************************************************************* -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="CLASS">
		<xsl:param name="FACTORELEMENT" /> <!-- Depending on the factor element BI/PD..etc -->
		<xsl:variable name="PDRIVERCLASS">
			<xsl:value-of select="normalize-space(VEHICLES/VEHICLE/VEHICLECLASS)" /> <!-- need to determine driver class first-->
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLECLASS ='' or VEHICLES/VEHICLE/VEHICLECLASS='0'">
			1.00
		</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="$FACTORELEMENT = 'BI'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSRELATIVITY']/NODE[@ID='DCR']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'PD'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSRELATIVITY']/NODE[@ID='DCR']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'PPI'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSRELATIVITY']/NODE[@ID='DCR']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'PIP'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSRELATIVITY']/NODE[@ID='DCR']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'UM'">1.00</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'UIM'">1.00</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'MEDPAY'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSRELATIVITY']/NODE[@ID='DCR']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'COMP'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSCOMPREHENSIVE']/NODE[@ID='DRIVERCLASSCOMP']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'COLL'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSCOLLISION']/NODE[@ID='DRIVERCLASSCOLL']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'LTDCOLL'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSCOLLISION']/NODE[@ID='DRIVERCLASSCOLL']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY" />
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- ***************************************** Start of Limit ********************************************************************************** -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="LIMIT">
		<xsl:param name="FACTORELEMENT" />
		<xsl:variable name="PVEHICLEROWID" select="VEHICLES/VEHICLE/VEHICLEROWID"></xsl:variable> <!-- Ask Deepak -->
		<xsl:choose>
			<xsl:when test="$FACTORELEMENT = 'BI'"> <!-- BI -->
				<xsl:variable name="COVERAGELL" select="VEHICLES/VEHICLE/BILIMIT1" /> <!-- Lower Limit CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
				<xsl:variable name="COVERAGEUL" select="VEHICLES/VEHICLE/BILIMIT2" /> <!-- Upper Limit -->
				<xsl:choose>
					<xsl:when test="$COVERAGELL ='' or $COVERAGELL='0'">
					1.00
				</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIMITS']/NODE[@ID= $FACTORELEMENT ]/ATTRIBUTES[@MINCOVERAGE= $COVERAGELL and @MAXCOVERAGE = $COVERAGEUL ]/@RELATIVITY" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='UM'"> <!-- UM -->
				<xsl:variable name="COVERAGELL" select="VEHICLES/VEHICLE/UMSPLITLIMIT1" /> <!-- Lower Limit CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
				<xsl:variable name="COVERAGEUL" select="VEHICLES/VEHICLE/UMSPLITLIMIT2" /> <!-- Upper Limit -->
				<xsl:choose>
					<!-- Check if split limit or combined single limit -->
					<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL !='0'"> <!-- If combined single limit -->
						<xsl:variable name="PUMCSL">
							<xsl:choose>
								<xsl:when test="contains(VEHICLES/VEHICLE/UMCSL,',')">
									<xsl:value-of select="VEHICLES/VEHICLE/UMCSL" />
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="format-number(VEHICLES/VEHICLE/UMCSL,'###,###')" />
								</xsl:otherwise>
							</xsl:choose>
						</xsl:variable> <!--select="translate(VEHICLES/VEHICLE/UMCSL,',','')" /-->
						<xsl:choose>
							<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = $VT_CLASSIC_CAR or VEHICLES/VEHICLE/VEHICLETYPE = $VT_ANTIQUE_CAR">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID ='CSLUMBI']/ATTRIBUTES[@CSLUMBILIMITS = $PUMCSL]/@CSLUMRATES2" />
							</xsl:when>
							<xsl:when test="$PVEHICLEROWID &gt; 0 and $PVEHICLEROWID =1">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID ='CSLUMBI']/ATTRIBUTES[@CSLUMBILIMITS = $PUMCSL]/@CSLUMRATES1" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID ='CSLUMBI']/ATTRIBUTES[@CSLUMBILIMITS = $PUMCSL]/@CSLUMRATES2" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise> <!-- If  split limit -->
						<xsl:variable name="PMINLIMIT" select="VEHICLES/VEHICLE/UMSPLITLIMIT1"></xsl:variable>
						<xsl:variable name="PMAXLIMIT" select="VEHICLES/VEHICLE/UMSPLITLIMIT2"></xsl:variable>
						<xsl:choose>
							<xsl:when test="$PMINLIMIT != 0 and $PMAXLIMIT != 0">
								<xsl:choose>
									<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = $VT_CLASSIC_CAR or VEHICLES/VEHICLE/VEHICLETYPE = $VT_ANTIQUE_CAR">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID ='UMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMIT and @MAXLIMIT=$PMAXLIMIT]/@UMRATE2" />
									</xsl:when>
									<xsl:when test="$PVEHICLEROWID &gt; 0 and $PVEHICLEROWID =1">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID ='UMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMIT and @MAXLIMIT=$PMAXLIMIT]/@UMRATE1" />
									</xsl:when>
									<xsl:when test="$PVEHICLEROWID &gt; 1">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID ='UMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMIT and @MAXLIMIT=$PMAXLIMIT]/@UMRATE2" />
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
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT ='UM_MICHIGAN_SPLIT'">
				<xsl:variable name="COVERAGELL" select="VEHICLES/VEHICLE/UMSPLITLIMIT1" /> <!-- Lower Limit CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
				<xsl:variable name="COVERAGEUL" select="VEHICLES/VEHICLE/UMSPLITLIMIT2" /> <!-- Upper Limit -->
				<!-- Split limit -->
				<xsl:choose>
					<xsl:when test="$COVERAGELL !='' and $COVERAGELL!= '0' and $COVERAGEUL !='' and $COVERAGEUL != '0'">
						<xsl:variable name="PMINLIMITUM" select="VEHICLES/VEHICLE/UMSPLITLIMIT1"></xsl:variable>
						<xsl:variable name="PMAXLIMITUM" select="VEHICLES/VEHICLE/UMSPLITLIMIT2"></xsl:variable>
						<xsl:choose>
							<xsl:when test="$PMINLIMITUM != 0 and $PMAXLIMITUM != 0">
								<xsl:choose>
									<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = $VT_CLASSIC_CAR or VEHICLES/VEHICLE/VEHICLETYPE = $VT_ANTIQUE_CAR">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID ='UMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMITUM and @MAXLIMIT=$PMAXLIMITUM]/@UMRATE2" />
									</xsl:when>
									<xsl:when test="$PVEHICLEROWID &gt; 0 and $PVEHICLEROWID =1">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID ='UMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMITUM and @MAXLIMIT=$PMAXLIMITUM]/@UMRATE1" />
									</xsl:when>
									<xsl:when test="$PVEHICLEROWID &gt; 1">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID ='UMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMITUM and @MAXLIMIT=$PMAXLIMITUM]/@UMRATE2" />
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
					</xsl:when>
					<xsl:otherwise>
					1.00
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT ='UM_MICHIGAN_CSL'">
				<xsl:variable name="PCSL">
					<xsl:choose>
						<xsl:when test="contains(VEHICLES/VEHICLE/UMCSL,',')">
							<xsl:value-of select="VEHICLES/VEHICLE/UMCSL" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="format-number(VEHICLES/VEHICLE/UMCSL,'###,###')" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable> <!--select="VEHICLES/VEHICLE/UMCSL"/-->
				<!-- Combined single limit -->
				<xsl:choose>
					<xsl:when test="$PCSL !='' and $PCSL!='0' and $PCSL!='NO COVERAGE'">
						<xsl:choose>
							<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = $VT_CLASSIC_CAR or VEHICLES/VEHICLE/VEHICLETYPE = $VT_ANTIQUE_CAR">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID ='CSLUMBI']/ATTRIBUTES[@CSLUMBILIMITS = $PCSL]/@CSLUMRATES2" />
							</xsl:when>
							<xsl:when test="$PVEHICLEROWID &gt; 0 and $PVEHICLEROWID =1">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID ='CSLUMBI']/ATTRIBUTES[@CSLUMBILIMITS = $PCSL]/@CSLUMRATES1" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID ='CSLUMBI']/ATTRIBUTES[@CSLUMBILIMITS = $PCSL]/@CSLUMRATES2" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>
					1.00
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT ='UM_INDIANA_SPLIT'">
				<xsl:variable name="COVERAGELL" select="VEHICLES/VEHICLE/UMSPLITLIMIT1" /> <!-- Lower Limit CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
				<xsl:variable name="COVERAGEUL" select="VEHICLES/VEHICLE/UMSPLITLIMIT2" /> <!-- Upper Limit -->
				<!-- Split limit -->
				<xsl:choose>
					<xsl:when test="$COVERAGELL !='' and $COVERAGELL!= '0' and $COVERAGEUL !='' and $COVERAGEUL != '0'">
						<xsl:variable name="PMINLIMITUM" select="VEHICLES/VEHICLE/UMSPLITLIMIT1"></xsl:variable>
						<xsl:variable name="PMAXLIMITUM" select="VEHICLES/VEHICLE/UMSPLITLIMIT2"></xsl:variable>
						<xsl:choose>
							<xsl:when test="$PMINLIMITUM != 0 and $PMAXLIMITUM != 0">
								<xsl:choose>
									<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = $VT_CLASSIC_CAR or VEHICLES/VEHICLE/VEHICLETYPE = $VT_ANTIQUE_CAR">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID ='UMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMITUM and @MAXLIMIT=$PMAXLIMITUM]/@UMRATE2" />
									</xsl:when>
									<xsl:when test="$PVEHICLEROWID &gt; 0 and $PVEHICLEROWID =1">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID ='UMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMITUM and @MAXLIMIT=$PMAXLIMITUM]/@UMRATE1" />
									</xsl:when>
									<xsl:when test="$PVEHICLEROWID &gt; 1">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID ='UMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMITUM and @MAXLIMIT=$PMAXLIMITUM]/@UMRATE2" />
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
					</xsl:when>
					<xsl:otherwise>
					1.00
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT ='UM_INDIANA_CSL'">
				<xsl:variable name="PCSL">
					<xsl:choose>
						<xsl:when test="contains(VEHICLES/VEHICLE/UMCSL,',')">
							<xsl:value-of select="VEHICLES/VEHICLE/UMCSL" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="format-number(VEHICLES/VEHICLE/UMCSL,'###,###')" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable> <!--select="VEHICLES/VEHICLE/UMCSL"/-->
				<!-- Combined single limit -->
				<xsl:choose>
					<xsl:when test="$PCSL !='' and $PCSL!='0' and $PCSL!='NO COVERAGE'">
						<xsl:choose>
							<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = $VT_CLASSIC_CAR or VEHICLES/VEHICLE/VEHICLETYPE = $VT_ANTIQUE_CAR">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID ='CSLUMBI']/ATTRIBUTES[@CSLUMBILIMITS = $PCSL]/@CSLUMRATES2" />
							</xsl:when>
							<xsl:when test="$PVEHICLEROWID &gt; 0 and $PVEHICLEROWID =1">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID ='CSLUMBI']/ATTRIBUTES[@CSLUMBILIMITS = $PCSL]/@CSLUMRATES1" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID ='CSLUMBI']/ATTRIBUTES[@CSLUMBILIMITS = $PCSL]/@CSLUMRATES2" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>
					1.00
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT = 'UIM'"> <!-- UIM -->
				<xsl:variable name="COVERAGELL" select="VEHICLES/VEHICLE/UIMSPLITLIMIT1" /> <!-- Lower Limit CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
				<xsl:variable name="COVERAGEUL" select="VEHICLES/VEHICLE/UIMSPLITLIMIT2" /> <!-- Upper Limit -->
				<xsl:variable name="SPLITLIMIT" select="VEHICLES/VEHICLE/UIMSPLIT" /> <!-- this value is blank when CSL is selected -->
				<xsl:variable name="PUMCSL" select="translate(normalize-space(VEHICLES/VEHICLE/UIMCSL),',','')" /> <!-- this value is blank when SPLIT is selected -->
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/ISUNDERINSUREDMOTORISTS ='TRUE'">
						<xsl:variable name="PMINLIMIT" select="VEHICLES/VEHICLE/UIMSPLITLIMIT1"></xsl:variable>
						<xsl:variable name="PMAXLIMIT" select="VEHICLES/VEHICLE/UIMSPLITLIMIT2"></xsl:variable>
						<xsl:choose>
							<xsl:when test="($PMINLIMIT != 0 or $PMINLIMIT != '') and ($PMAXLIMIT != 0 or $PMAXLIMIT !='')">
								<!-- If combined single limit -->
								<xsl:choose>
									<xsl:when test="VEHICLES/VEHICLE/UIMSPLIT='' or VEHICLES/VEHICLE/UIMSPLIT ='NO COVERAGE' or VEHICLES/VEHICLE/UIMSPLIT ='0/0' or VEHICLES/VEHICLE/UIMSPLIT ='0'">
										<xsl:choose>
											<xsl:when test="VEHICLES/VEHICLE/UIMCSL != '0' and VEHICLES/VEHICLE/UIMCSL != ''">
												<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUIMBILIMITS']/NODE[@ID ='CSLUIMBI']/ATTRIBUTES[@CSLUIMBILIMITS=$PUMCSL]/@CSLUIMRATES" />
											</xsl:when>
											<xsl:otherwise>0.00</xsl:otherwise>
										</xsl:choose>
									</xsl:when>
									<!-- If  split limit >  -->
									<xsl:when test="VEHICLES/VEHICLE/UIMCSL ='' or VEHICLES/VEHICLE/UIMCSL= 'NO COVERAGE' or VEHICLES/VEHICLE/UIMCSL = '0'">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UIMBILIMITS']/NODE[@ID ='UIMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMIT and @MAXLIMIT=$PMAXLIMIT]/@UIMRATES" />
									</xsl:when>
									<xsl:otherwise>0.00</xsl:otherwise>
								</xsl:choose>
							</xsl:when>
							<xsl:otherwise>0.00</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>
						0.00
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='MEDPAY'"> <!-- MEDPAY limit :indiana -->
				<xsl:variable name="PMEDICALLIMIT">
					<xsl:value-of select="translate(VEHICLES/VEHICLE/MPLIMIT,',','')" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$PMEDICALLIMIT != ''">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MEDICALPAYMENTLIMIT']/NODE[@ID = 'MEDPAY']/ATTRIBUTES[@LIMIT=$PMEDICALLIMIT]/@FACTOR" />
					</xsl:when>
					<xsl:otherwise>
					1.00
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise> <!-- PD -->
				<xsl:variable name="COVERAGE"> <!-- CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
					<xsl:choose>
						<xsl:when test="contains(VEHICLES/VEHICLE/PD,',')">
							<xsl:value-of select="VEHICLES/VEHICLE/PD" />
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="format-number(VEHICLES/VEHICLE/PD,'###,###')" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$COVERAGE ='' or $COVERAGE='0'">
					1.00
				</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIMITS']/NODE[@ID = $FACTORELEMENT ]/ATTRIBUTES[@COVERAGE = $COVERAGE ]/@RELATIVITY" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- ************************************  Start of Extended Non-Owned Coverage for Named Individual(A-35) ********************************************************************************** -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="ENOLIABILITY_PREMIUM">
		<xsl:variable name="PENOCNI" select="normalize-space(VEHICLES/VEHICLE/ENOCNI)" />
		<xsl:variable name="PENOMIN" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENOLIABILITY']/NODE[@ID='ENOLBI']/@MIN_PREMIUM" />
		<xsl:variable name="PPENOPREMIUM">
			<xsl:choose>
				<xsl:when test="$PENOCNI='TRUE' and $PENOCNI!=''">
					<xsl:variable name="PCOVERAGEBI" select="VEHICLES/VEHICLE/BI" /> <!-- Limit Bi -->
					<xsl:variable name="PCOVERAGELL" select="VEHICLES/VEHICLE/BILIMIT1" /> <!-- Lower Limit Bi -->
					<xsl:variable name="PCOVERAGEUL" select="VEHICLES/VEHICLE/BILIMIT2" /> <!-- Upper Limit Bi -->
					<xsl:variable name="PCOVERAGEPD"> <!--Physical Damage Limit -->
						<xsl:choose>
							<xsl:when test="contains(VEHICLES/VEHICLE/PD,',')">
								<xsl:value-of select="VEHICLES/VEHICLE/PD" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="format-number(VEHICLES/VEHICLE/PD,'###,###')" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:variable>
					<xsl:choose>
						<xsl:when test="($PCOVERAGELL != '0' and $PCOVERAGELL != '') and ($PCOVERAGEUL != '0' and $PCOVERAGEUL !='')">
							<xsl:variable name="PENOBI">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENOLIABILITY']/NODE[@ID='ENOLBI']/ATTRIBUTES[@MINLIMIT=$PCOVERAGELL and @MAXLIMIT=$PCOVERAGEUL]/@BIRATES" />
							</xsl:variable>
							<xsl:variable name="PENOPD">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENOLIABILITY']/NODE[@ID='ENOLPD']/ATTRIBUTES[@PDLIMIT=$PCOVERAGEPD]/@PDRATES" />
							</xsl:variable>
							<xsl:choose>
								<xsl:when test="($PCOVERAGEBI ='' and $PCOVERAGEBI ='0')">
									0
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="round($PENOBI + $PENOPD)" />
								</xsl:otherwise>
							</xsl:choose>
						</xsl:when>
						<xsl:otherwise>
							<xsl:variable name="PCOVERAGECSL"> <!-- CSL Limit  -->
								<xsl:choose>
									<xsl:when test="contains(VEHICLES/VEHICLE/CSL,',')">
										<xsl:value-of select="VEHICLES/VEHICLE/CSL" />
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="format-number(VEHICLES/VEHICLE/CSL,'###,###')" />
									</xsl:otherwise>
								</xsl:choose>
							</xsl:variable>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ENOLIABILITY']/NODE[@ID='ENOLCSL']/ATTRIBUTES[@CSLLIMIT=$PCOVERAGECSL]/@CSLRATES" />
						</xsl:otherwise>
					</xsl:choose>
				</xsl:when>
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PENOMIN &lt; $PPENOPREMIUM">
				<xsl:choose>
					<xsl:when test="$PPENOPREMIUM &gt; 0">
						<xsl:value-of select="$PPENOPREMIUM" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="$PPENOPREMIUM &gt; 0">
						<xsl:value-of select="$PENOMIN" />
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************************************* -->
	<!-- ************************************  End of Extended Non-Owned Coverage for Named Individual(A-35) ********************************************************************************** -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- ********************************************************************************************************************************************* -->
	<!-- ************************************  Start of Multi-vehicle ********************************************************************************** -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="MULTIVEHICLE">
		<xsl:param name="FACTORELEMENT" /> <!-- Depending on the factor element BI/PD..etc -->
		<!--	<xsl:variable name="PDRIVERCLASS" select="DRIVERS/DRIVER/DRIVERCLASSCOMPONENT1"/> -->  <!-- ASK RAJAN -->
		<xsl:variable name="PDRIVERCLASS" select="normalize-space(VEHICLES/VEHICLE/VEHICLECLASSCOMPONENT1)" />
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE  =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_CAMPER_TRAVEL_TRAILER">
			 1.00
		</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/MULTICARDISCOUNT != '' and VEHICLES/VEHICLE/MULTICARDISCOUNT !='FALSE'">
				<xsl:choose>
					<xsl:when test="$FACTORELEMENT = 'BI'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEBI']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'PD'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEPD']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'PPI'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEPPI']/ATTRIBUTES[@CLASS  = $PDRIVERCLASS]/@FACTOR" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'PIP'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEPIP']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'MEDPAY'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEMEDPAY']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'COLL'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLECOLL']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'LTDCOLL'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLECOLL']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR" />
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'COMP'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLECOMP']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR" />
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MULTIVEHICLE_DISPLAY">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/MULTICARDISCOUNT = 'TRUE' and (VEHICLES/VEHICLE/VEHICLETYPE != $VT_CAMPER_TRAVEL_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_UTILITY_TRAILER)">
			Included
		</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Multicar discount CSL -->
	<!--xsl:template name="MULTICARDISCOUNTCSL">
		<xsl:variable name="PDRIVERCLASS">
			<xsl:value-of select="DRIVERS/DRIVER/DRIVERCLASS" />
			<xsl:value-of select="VEHICLES/VEHICLE/VEHICLEUSE" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PDRIVERCLASS ='P1' or $PDRIVERCLASS ='P2'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTICARCSL']/ATTRIBUTES[@DRIVERCLASS = $PDRIVERCLASS]/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTICARCSL']/ATTRIBUTES[@DRIVERCLASS = 'OTHER']/@FACTOR" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template-->
	<!-- ********************************************************************************************************************************************* -->
	<!-- ************************************** Start of Vehicle Use ********************************************************************************** -->
	<!-- ********************************************************************************************************************************************* -->
	<xsl:template name="VEHICLEUSE">
		<xsl:variable name="PVEHICLETYPE">
			<xsl:value-of select="VEHICLES/VEHICLE/VEHICLETYPE" />
		</xsl:variable>
		<xsl:variable name="PUSECODE">
			<xsl:choose>
				<xsl:when test="normalize-space(VEHICLES/VEHICLE/CARPOOL)=normalize-space('YES')">O</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="VEHICLES/VEHICLE/VEHICLEUSE" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="PDRIVERCLASS" select="VEHICLES/VEHICLE/VEHICLECLASSCOMPONENT1" />
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME = 'MICHIGAN'">
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/VEHICLECLASS !=''">
						<xsl:choose>
							<xsl:when test="($PDRIVERCLASS = '2' or $PDRIVERCLASS = '3' or $PDRIVERCLASS = '4' or $PDRIVERCLASS = '6') and $PUSECODE='P'">1.00</xsl:when>
							<xsl:when test="($PVEHICLETYPE = $VT_MOTORHOME or $PVEHICLETYPE = $VT_CLASSIC_CAR  or $PVEHICLETYPE = $VT_ANTIQUE_CAR) and $PUSECODE='P'">1.00</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEUSERELATIVITY']/NODE[@ID='VEHICLEUSE']/ATTRIBUTES[@USECODE = $PUSECODE and @CLASS = $PDRIVERCLASS]/@FACTOR" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="POLICY/STATENAME = 'INDIANA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEUSERELATIVITY']/NODE[@ID='VEHICLEUSE']/ATTRIBUTES[@USECODE = $PUSECODE and @CLASS = $PDRIVERCLASS]/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- ******************************* Primer or Safr Driver ********************************************************* -->
	<!-- ********************************************************************************************************************** -->
	<!-- Premier or Safe driver discount -->
	<xsl:template name="DRIVERDISCOUNT">
		<!-- 1. Check for Premier or Safe Discount 
		 2. If Premier then call Premier template.
		 3. If Safe then check for Surcharge.
		 4. If Surcharge > 1 then do not call Safe Driver Template ..return 1.00
		 5.			else call SafeDriver template.-->
		<xsl:variable name="PSURCHARGE">
			<xsl:call-template name="SURCHARGE"></xsl:call-template>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="normalize-space(POLICY/LOSSES_CHARGEABLE_NONCHARGEABLE) &lt; '3'">
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = $VT_SUSPENDED_COMP or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_CAMPER_TRAVEL_TRAILER ">
			1.00
			</xsl:when>
					<xsl:when test="(normalize-space(VEHICLES/VEHICLE/SAFEDRIVER)='TRUE' and $PSURCHARGE = 1) ">
						<xsl:call-template name="SAFEDISCOUNT" />
					</xsl:when>
					<xsl:when test="normalize-space(VEHICLES/VEHICLE/PREMIERDRIVER)='TRUE' and normalize-space(VEHICLES/VEHICLE/SAFEDRIVER)='FALSE' ">
						<xsl:call-template name="PREMIERDISCOUNT" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/PREMIERDRIVER='TRUE'">			
				1.00
			</xsl:when>
					<xsl:otherwise>
			1.00
			</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="PREMIERDISCOUNT">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@PREMIERDRIVERFACTOR" />
	</xsl:template>
	<xsl:template name="SAFEDISCOUNT">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@SAFEDRIVERFACTOR" />
	</xsl:template>
	<xsl:template name="DRIVERDISCOUNT_DISPLAY">
		<!-- 1. Check for Premier or Safe Discount 
		 2. If Premier then call Premier template.
		 3. If Safe then check for Surcharge.
		 4. If Surcharge > 1 then do not call Safe Driver Template ..return 1.00
		 5.			else call SafeDriver template.-->
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = $VT_SUSPENDED_COMP or   VEHICLES/VEHICLE/VEHICLETYPE  =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_CAMPER_TRAVEL_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE_SCO = 'SCO'">
				0
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="PDRIVERDISCOUNT">
					<xsl:call-template name="DRIVERDISCOUNT" />
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$PDRIVERDISCOUNT &lt; 1">
						Included
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="DRIVERDISCOUNT_CREDIT">
		<!-- 1. Check for Premier or Safe Discount 
		 2. If Premier then call Premier template.
		 3. If Safe then check for Surcharge.
		 4. If Surcharge > 1 then do not call Safe Driver Template ..return 1.00
		 5.			else call SafeDriver template.-->
		<xsl:variable name="PSURCHARGE">
			<xsl:call-template name="SURCHARGE"></xsl:call-template>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = $VT_SUSPENDED_COMP or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_CAMPER_TRAVEL_TRAILER "></xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/SAFEDRIVER='TRUE' and $PSURCHARGE =1"><xsl:call-template name="SAFEDISCOUNT_CREDIT" />%</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/PREMIERDRIVER='TRUE' and VEHICLES/VEHICLE/SAFEDRIVER='FALSE'"><xsl:call-template name="PREMIERDISCOUNT_CREDIT" />%</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/PREMIERDRIVER='FALSE'"></xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Premier Driver Discount Credit -->
	<xsl:template name="PREMIERDISCOUNT_CREDIT">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@PREMIERDRIVERCREDIT" />
	</xsl:template>
	<!--Safe Driver Discount Credit -->
	<xsl:template name="SAFEDISCOUNT_CREDIT">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@SAFEDRIVERCREDIT" />
	</xsl:template>
	<!-- End of Premier or Safe driver discount -->
	<!-- ********************************************************************************************************************** -->
	<!-- **************************      END OF PRIMER DRIVER   ********************************************************* -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- **************************      START OF GOOD STUDENT    ********************************************************* -->
	<!-- ********************************************************************************************************************** -->
	<!-- Good student discount -->
	<xsl:template name="GOODSTUDENT">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE  =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_CAMPER_TRAVEL_TRAILER ">1.00</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/GOODSTUDENT = 'TRUE'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='GOODSTUDENT']/NODE[@ID ='GOODSTUDENTDISCOUNT']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
				1.00
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="GOODSTUDENT_DISPLAY">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE  =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_CAMPER_TRAVEL_TRAILER ">0</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/GOODSTUDENT = 'TRUE'">
				Included
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Good student discount Pecentage -->
	<xsl:template name="GOODSTUDENT_DISCOUNT_PERCENT">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE  =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_CAMPER_TRAVEL_TRAILER ">1.00</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/GOODSTUDENT = 'TRUE'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='GOODSTUDENT']/NODE[@ID ='GOODSTUDENTDISCOUNT']/ATTRIBUTES/@CREDIT" />
			</xsl:when>
			<xsl:otherwise>
			1.00
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- **************************      END OF GOOD STUDENT    ********************************************************* -->
	<!-- ********************************************************************************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<!-- ************************** Combined Single Limit Relativity **************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="CSLRELATIVITY">
		<xsl:variable name="PCSLLIMIT"> <!-- ASK DEEPAK TO GIVE CSL FOR ALL NODES -->
			<xsl:value-of select="translate(VEHICLES/VEHICLE/CSL,',','')" />
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$PCSLLIMIT &gt; 0">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMBINEDSINGLELIMITS']/NODE[@ID ='CSL']/ATTRIBUTES[@CSLLIMIT = $PCSLLIMIT ]/@CSLRELATIVITY" />
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
		<!--   <xsl:value-of select="$PCSLLIMIT"/>   -->
		<!-- chk list  -->
	</xsl:template>
	<!-- ********************************************************************************************************************** -->
	<!-- *****************************************  Surcharge  ******************************************************** -->
	<!-- ********************************************************************************************************************** -->
	<xsl:template name="SURCHARGEPOINTS">
		<xsl:variable name="VAR_SUMOFACCIDENTPOINT" select="user:GetPvioAcc(-1)" />
		<xsl:variable name="ACCIDENTPOINTS">
			<xsl:for-each select="VEHICLES/VEHICLE/ASSIGNDRIVIOACCPNT[@ID &lt; 9999999]">
				<xsl:variable name="ACCIDENTPOINT">
					<xsl:value-of select="SUMOFACCIDENTPOINTS" />
				</xsl:variable>
				<xsl:variable name="VAR_TOTALACCIDENTPOINTS" select="user:GetPvioAcc($ACCIDENTPOINT)" />
			</xsl:for-each>
		</xsl:variable>
		<xsl:variable name="VIOLATIONPOINTS">
			<xsl:for-each select="VEHICLES/VEHICLE/ASSIGNDRIVIOACCPNT[@ID &lt; 9999999]">
				<xsl:variable name="VIOLATIONPOINT">
					<xsl:value-of select="SUMOFVIOLATIONPOINTS" />
				</xsl:variable>
				<xsl:variable name="VAR_TOTALVIOLATIONPOINTS" select="user:GetPvioAcc($VIOLATIONPOINT)" />
			</xsl:for-each>
		</xsl:variable>
		<xsl:variable name="VAR1">
			<xsl:value-of select="user:GetPvioAcc(0)" />
		</xsl:variable>
		<xsl:value-of select="$VAR1" />
	</xsl:template>
	<xsl:template name="SURCHARGE">
		<!-- 1. Get the violation and accident points.
		2. Against each violation/accident point, fetch the surcharge  (After 13 points, calculate the surcharge using "For each additional" value
		3. Add the surcharges
		4. Divide the sum by 100 and add 1 -->
		<!-- Accident Surcharge  -->
		<!-- Run a loop for each Assign Driver Accident Points. -->
		<xsl:variable name="VAR_SUMOFACCIDENTPOINT" select="user:GetPvioAcc(-1)" />
		<xsl:variable name="ACCIDENTSURCHARGE">
			<xsl:for-each select="VEHICLES/VEHICLE/ASSIGNDRIVIOACCPNT[@ID &lt; 9999999]">
				<xsl:variable name="ACCIDENTPOINTS">
					<xsl:value-of select="SUMOFACCIDENTPOINTS" />
				</xsl:variable>
				<xsl:variable name="VAR_ACCIDENT_PERCENTAGE">
					<xsl:choose>
						<xsl:when test="$ACCIDENTPOINTS !='' and $ACCIDENTPOINTS !='0' and $ACCIDENTPOINTS &gt; 0">
							<xsl:choose>
								<xsl:when test="$ACCIDENTPOINTS &gt; 13">
									<xsl:variable name="VAR1">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=13]/@SURCHARGEPERCENT" />
									</xsl:variable>
									<xsl:variable name="VAR2">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/@FOR_EACH_ADDITIONAL" />
									</xsl:variable>
									<xsl:value-of select="$VAR1 + round(($ACCIDENTPOINTS - 13) * $VAR2)" />
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=$ACCIDENTPOINTS]/@SURCHARGEPERCENT" />
								</xsl:otherwise>
							</xsl:choose>
						</xsl:when>
						<xsl:otherwise>0.00</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="$VAR_ACCIDENT_PERCENTAGE !=0">
					<xsl:value-of select="user:GetPvioAcc($VAR_ACCIDENT_PERCENTAGE)" />
				</xsl:if>
			</xsl:for-each>
		</xsl:variable>
		<!-- Violation Surcharge -->
		<!-- run a loop for each assign driver violation point	 -->
		<xsl:variable name="VIOLATIONSURCHARGE">
			<xsl:for-each select="VEHICLES/VEHICLE/ASSIGNDRIVIOACCPNT[@ID &lt; 9999999]">
				<xsl:variable name="VIOLATIONPOINTS">
					<xsl:value-of select="SUMOFVIOLATIONPOINTS" />
				</xsl:variable>
				<xsl:variable name="VAR_VIOLATION_PERCENTAGE">
					<xsl:choose>
						<xsl:when test="$VIOLATIONPOINTS !='' and $VIOLATIONPOINTS !='0' and $VIOLATIONPOINTS &gt; 0">
							<xsl:choose>
								<xsl:when test="$VIOLATIONPOINTS &gt; 13">
									<xsl:variable name="VAR1">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEVIOLATIONPOINTS']/ATTRIBUTES[@POINTS=13]/@SURCHARGE" />
									</xsl:variable>
									<xsl:variable name="VAR2">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEVIOLATIONPOINTS']/@FOR_EACH_ADDITIONAL" />
									</xsl:variable>
									<xsl:value-of select="$VAR1 + round(($VIOLATIONPOINTS - 13) * $VAR2)" />
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEVIOLATIONPOINTS']/ATTRIBUTES[@POINTS=$VIOLATIONPOINTS]/@SURCHARGE" />
								</xsl:otherwise>
							</xsl:choose>
						</xsl:when>
						<xsl:otherwise>0.00</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="$VAR_VIOLATION_PERCENTAGE !=0">
					<xsl:value-of select="user:GetPvioAcc($VAR_VIOLATION_PERCENTAGE)" />
				</xsl:if>
			</xsl:for-each>
		</xsl:variable>
		<xsl:variable name="var1" select="user:GetPvioAcc(0)" />
		<xsl:value-of select="(($var1) div 100.00) + 1.00" />
	</xsl:template>
	<xsl:template name="ACCIDENT_VIOLATION_SURCHRAGE_PERCENTAGE">
		<!-- Accident Surcharge  -->
		<!-- Run a loop for each Assign Driver Accident Points. -->
		<xsl:variable name="VAR_SUMOFACCIDENTPOINT" select="user:GetPvioAcc(-1)" />
		<xsl:variable name="ACCIDENTSURCHARGE">
			<xsl:for-each select="VEHICLES/VEHICLE/ASSIGNDRIVIOACCPNT[@ID &lt; 9999999]">
				<xsl:variable name="ACCIDENTPOINTS">
					<xsl:value-of select="SUMOFACCIDENTPOINTS" />
				</xsl:variable>
				<xsl:variable name="VAR_ACCIDENT_PERCENTAGE">
					<xsl:choose>
						<xsl:when test="$ACCIDENTPOINTS !='' and $ACCIDENTPOINTS !='0' and $ACCIDENTPOINTS &gt; 0">
							<xsl:choose>
								<xsl:when test="$ACCIDENTPOINTS &gt; 13">
									<xsl:variable name="VAR1">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=13]/@SURCHARGEPERCENT" />
									</xsl:variable>
									<xsl:variable name="VAR2">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/@FOR_EACH_ADDITIONAL" />
									</xsl:variable>
									<xsl:value-of select="$VAR1 + round(($ACCIDENTPOINTS - 13) * $VAR2)" />
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=$ACCIDENTPOINTS]/@SURCHARGEPERCENT" />
								</xsl:otherwise>
							</xsl:choose>
						</xsl:when>
						<xsl:otherwise>0.00</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="$VAR_ACCIDENT_PERCENTAGE !=0">
					<xsl:value-of select="user:GetPvioAcc($VAR_ACCIDENT_PERCENTAGE)" />
				</xsl:if>
			</xsl:for-each>
		</xsl:variable>
		<!-- Violation Surcharge -->
		<!-- run a loop for each assign driver violation point	 -->
		<xsl:variable name="VIOLATIONSURCHARGE">
			<xsl:for-each select="VEHICLES/VEHICLE/ASSIGNDRIVIOACCPNT[@ID &lt; 9999999]">
				<xsl:variable name="VIOLATIONPOINTS">
					<xsl:value-of select="SUMOFVIOLATIONPOINTS" />
				</xsl:variable>
				<xsl:variable name="VAR_VIOLATION_PERCENTAGE">
					<xsl:choose>
						<xsl:when test="$VIOLATIONPOINTS !='' and $VIOLATIONPOINTS !='0' and $VIOLATIONPOINTS &gt; 0">
							<xsl:choose>
								<xsl:when test="$VIOLATIONPOINTS &gt; 13">
									<xsl:variable name="VAR1">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEVIOLATIONPOINTS']/ATTRIBUTES[@POINTS=13]/@SURCHARGE" />
									</xsl:variable>
									<xsl:variable name="VAR2">
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEVIOLATIONPOINTS']/@FOR_EACH_ADDITIONAL" />
									</xsl:variable>
									<xsl:value-of select="$VAR1 + round(($VIOLATIONPOINTS - 13) * $VAR2)" />
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEVIOLATIONPOINTS']/ATTRIBUTES[@POINTS=$VIOLATIONPOINTS]/@SURCHARGE" />
								</xsl:otherwise>
							</xsl:choose>
						</xsl:when>
						<xsl:otherwise>0.00</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:if test="$VAR_VIOLATION_PERCENTAGE !=0">
					<xsl:value-of select="user:GetPvioAcc($VAR_VIOLATION_PERCENTAGE)" />
				</xsl:if>
			</xsl:for-each>
		</xsl:variable>
		<xsl:variable name="var1" select="user:GetPvioAcc(0)" />
		<xsl:value-of select="$var1" />
	</xsl:template>
	<xsl:template name="ACCIDENT_VIOLATION_SURCHARGE_DISPLAY">
		<xsl:variable name="PACCVIOSURVAL">
			<xsl:call-template name="ACCIDENT_VIOLATION_SURCHRAGE_PERCENTAGE"></xsl:call-template>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE  =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_CAMPER_TRAVEL_TRAILER ">0</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="$PACCVIOSURVAL &gt; 0">Included</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--  *************************************  Multi Policy discount  ************************************************************************************************** -->
	<xsl:template name="MULTIPOLICY">
		<xsl:choose>
			<xsl:when test=" VEHICLES/VEHICLE/VEHICLETYPE  =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_CAMPER_TRAVEL_TRAILER ">
		1.00
		</xsl:when>
			<xsl:when test="(POLICY/MULTIPOLICYAUTOHOMEDISCOUNT = 'TRUE' or POLICY/MULTIPOLICYAUTOHOMEDISCOUNT = 'Y') and  (VEHICLES/VEHICLE/VEHICLETYPE = 'PP' or VEHICLES/VEHICLE/VEHICLETYPE ='CV' or  VEHICLES/VEHICLE/VEHICLETYPE ='SCO' or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_CLASSIC_CAR or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_ANTIQUE_CAR or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_MOTORHOME)">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIPOLICY']/NODE[@ID ='MULTIPOLICYDISCOUNT']/ATTRIBUTES/@FACTOR" />
			</xsl:when>
			<xsl:otherwise>
		1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MULTIPOLICY_DISPLAY">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE=$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE=$VT_CAMPER_TRAVEL_TRAILER   ">
		0
		</xsl:when>
			<xsl:when test="(POLICY/MULTIPOLICYAUTOHOMEDISCOUNT = 'TRUE' or POLICY/MULTIPOLICYAUTOHOMEDISCOUNT = 'Y') and  (VEHICLES/VEHICLE/VEHICLETYPE = 'PP' or VEHICLES/VEHICLE/VEHICLETYPE ='CV' or  VEHICLES/VEHICLE/VEHICLETYPE ='SCO' or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_CLASSIC_CAR or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_ANTIQUE_CAR or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_MOTORHOME )">
			Included
		</xsl:when>
			<xsl:otherwise>
			0
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="MULTIPOLICY_DISCOUNT">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE =$VT_CAMPER_TRAVEL_TRAILER ">
			0	
		</xsl:when>
			<xsl:when test="(POLICY/MULTIPOLICYAUTOHOMEDISCOUNT = 'TRUE' or POLICY/MULTIPOLICYAUTOHOMEDISCOUNT = 'Y') and  (VEHICLES/VEHICLE/VEHICLETYPE = 'PP' or VEHICLES/VEHICLE/VEHICLETYPE ='CV' or  VEHICLES/VEHICLE/VEHICLETYPE ='SCO' or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_CLASSIC_CAR or normalize-space(VEHICLES/VEHICLE/VEHICLETYPE) = $VT_ANTIQUE_CAR or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_MOTORHOME)">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIPOLICY']/NODE[@ID ='MULTIPOLICYDISCOUNT']/ATTRIBUTES/@CREDIT" />
			</xsl:when>
			<xsl:otherwise>
		1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="EXTRAEQUIPMENT_DEDUC">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONTYPE = 'LIMITED'">LIMITED</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONDEDUCTIBLE" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************* ********************************************** -->
	<!-- ***********************************  TRAILBLAZER START ************************************************************************  -->
	<!-- ********************************************************************* ********************************************** -->
	<!-- Trailblazer -->
	<!-- trail blazer discount 15%,applied to BI,PD,MEDPAY ,coll comp for elligible PPA-->
	<xsl:template name="TRAILBLAZER">
		<xsl:param name="FACTORELEMENT" />
		<xsl:choose>
			<!--xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = $VT_CLASSIC_CAR or VEHICLES/VEHICLE/VEHICLETYPE = $VT_ANTIQUE_CAR">1.00</xsl:when-->
			<xsl:when test="$FACTORELEMENT = 'BI' and POLICY/STATENAME = 'INDIANA' and VEHICLES/VEHICLE/QUALIFIESTRAIBLAZERPROGRAM='Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TRAILBLAZER']/NODE[@ID='TBP']/ATTRIBUTES/@BI_TRAILBLAZERFACTOR" />
			</xsl:when>
			<xsl:when test="$FACTORELEMENT = 'PD' and POLICY/STATENAME = 'INDIANA' and VEHICLES/VEHICLE/QUALIFIESTRAIBLAZERPROGRAM='Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TRAILBLAZER']/NODE[@ID='TBP']/ATTRIBUTES/@PD_TRAILBLAZERFACTOR" />
			</xsl:when>
			<xsl:when test="$FACTORELEMENT = 'MEDPAY'  and POLICY/STATENAME = 'INDIANA'  and VEHICLES/VEHICLE/QUALIFIESTRAIBLAZERPROGRAM='Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TRAILBLAZER']/NODE[@ID='TBP']/ATTRIBUTES/@MED_TRAILBLAZERFACTOR" />
			</xsl:when>
			<xsl:when test="$FACTORELEMENT = 'COMP'  and POLICY/STATENAME = 'INDIANA' and VEHICLES/VEHICLE/QUALIFIESTRAIBLAZERPROGRAM='Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TRAILBLAZER']/NODE[@ID='TBP']/ATTRIBUTES/@COMP_TRAILBLAZERFACTOR" />
			</xsl:when>
			<xsl:when test="$FACTORELEMENT = 'COLL'  and POLICY/STATENAME = 'INDIANA' and VEHICLES/VEHICLE/QUALIFIESTRAIBLAZERPROGRAM='Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TRAILBLAZER']/NODE[@ID='TBP']/ATTRIBUTES/@COLL_TRAILBLAZERFACTOR" />
			</xsl:when>
			<xsl:otherwise>1.00</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Trailblazer Display -->
	<xsl:template name="TRAILBLAZER_DISPLAY">
		<xsl:choose>
			<!--xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = $VT_CLASSIC_CAR or VEHICLES/VEHICLE/VEHICLETYPE = $VT_ANTIQUE_CAR">
		0
		</xsl:when-->
			<xsl:when test="POLICY/STATENAME = 'INDIANA' and VEHICLES/VEHICLE/QUALIFIESTRAIBLAZERPROGRAM='Y'">
			Included
		</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- To display the discount for Trailblazer (15%) -->
	<xsl:template name="TRAILBLAZER_DISCOUNT">
		<xsl:choose>
			<!--xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = $VT_CLASSIC_CAR or VEHICLES/VEHICLE/VEHICLETYPE = $VT_ANTIQUE_CAR">0</xsl:when-->
			<xsl:when test="POLICY/STATENAME = 'INDIANA' and VEHICLES/VEHICLE/QUALIFIESTRAIBLAZERPROGRAM='Y'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TRAILBLAZER']/NODE[@ID='TBP']/ATTRIBUTES/@BI_TRAILBLAZERDISCOUNT" />
			</xsl:when>
			<xsl:otherwise>0</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- ********************************************************************* ********************************************** -->
	<!-- ***********************************  TRAILBLAZER END ************************************************************************  -->
	<!-- ********************************************************************* ********************************************** -->
	<!-- ***********************************  Coverage Type discount  ************************************************************************************************** -->
	<xsl:template name="COVERAGETYPE">
		<xsl:variable name="PPIPCOVERAGECODE" select="VEHICLES/VEHICLE/MEDPM"></xsl:variable>
		<xsl:variable name="PDRIVERINCOME" select="VEHICLES/VEHICLE/DRIVERINCOME"></xsl:variable>
		<xsl:variable name="PNODEPENDENT" select="VEHICLES/VEHICLE/DEPENDENTS"></xsl:variable>
		<xsl:variable name="PWAIVEWORKLOSS" select="VEHICLES/VEHICLE/WAIVEWORKLOSS"></xsl:variable>
		<xsl:choose>
			<xsl:when test="$PPIPCOVERAGECODE = 'FULL' or $PPIPCOVERAGECODE = 'FULLMEDICALFULLWAGELOSS' or $PPIPCOVERAGECODE = 'PRIMARY' ">
				<!-- Full PIP ,Waiver of Work Loss, No dependents -->
				<xsl:choose>
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE' and ($PNODEPENDENT= '' or $PNODEPENDENT='NDEP' or $PNODEPENDENT='0')">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C6' ]/@RELATIVITY" />
					</xsl:when>
					<!-- Full PIP , No dependents, Low Income -->
					<xsl:when test="($PNODEPENDENT = '' or  $PNODEPENDENT = 'NDEP' or $PNODEPENDENT = '0') and $PDRIVERINCOME = 'LOW'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C5' ]/@RELATIVITY" />
					</xsl:when>
					<!-- Full PIP , Waiver of work loss -->
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C4' ]/@RELATIVITY" />
					</xsl:when>
					<!-- Full PIP , Low Income -->
					<xsl:when test="$PDRIVERINCOME = 'LOW'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C3' ]/@RELATIVITY" />
					</xsl:when>
					<!-- Full PIP ,No dependents -->
					<xsl:when test="$PNODEPENDENT = '' or $PNODEPENDENT= 'NDEP' or $PNODEPENDENT='0'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C2' ]/@RELATIVITY" />
					</xsl:when>
					<!-- Full PIP -->
					<xsl:when test="$PWAIVEWORKLOSS != 'TRUE' and $PNODEPENDENT='1MORE' and $PDRIVERINCOME != 'LOW'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C1' ]/@RELATIVITY" />
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$PPIPCOVERAGECODE = 'EXCESSWAGE'">
				<!-- Excess Workloss  means - Excess Wage was selected. It has no link with the Waiver of Workloss 
										- Full PIP is included -->
				<!-- Full PIP and Excess Workloss, No dependents, Low Income -->
				<xsl:choose>
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE' and ($PNODEPENDENT='' or $PNODEPENDENT='NDEP' or $PNODEPENDENT='0')">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C6' ]/@RELATIVITY" />
					</xsl:when>
					<xsl:when test="($PNODEPENDENT='' or $PNODEPENDENT='NDEP' or $PNODEPENDENT='0') and $PDRIVERINCOME='LOW'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C10' ]/@RELATIVITY" />
					</xsl:when>
					<!-- Full PIP and Excess Workloss, No dependents -->
					<xsl:when test="$PNODEPENDENT='' or $PNODEPENDENT='NDEP' or $PNODEPENDENT='0'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C9' ]/@RELATIVITY" />
					</xsl:when>
					<!-- Full PIP , Waiver of work loss -->
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C4' ]/@RELATIVITY" />
					</xsl:when>
					<!-- Full PIP and Excess Workloss , Low Income -->
					<xsl:when test="$PDRIVERINCOME = 'LOW'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C8' ]/@RELATIVITY" />
					</xsl:when>
					<!-- Full PIP and Excess Workloss -->
					<xsl:when test="$PWAIVEWORKLOSS !='TRUE' and $PNODEPENDENT='1MORE' and $PDRIVERINCOME='HIGH'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID ='C7']/@RELATIVITY" />
					</xsl:when>
					<xsl:otherwise> 
						1.00
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$PPIPCOVERAGECODE = 'EXCESSMEDICAL'">
				<!-- Excess Medical, No dependents ,Waiver of Work Loss -->
				<xsl:choose>
					<xsl:when test="($PNODEPENDENT='' or $PNODEPENDENT='NDEP' or $PNODEPENDENT='0') and $PWAIVEWORKLOSS='TRUE'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C20' ]/@RELATIVITY" />
					</xsl:when>
					<!-- Excess Medical, No dependents, Low Income -->
					<xsl:when test="($PNODEPENDENT='' or $PNODEPENDENT='NDEP' or $PNODEPENDENT='0') and $PDRIVERINCOME='LOW'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C15' ]/@RELATIVITY" />
					</xsl:when>
					<!-- Excess Medical, Waiver of work loss -->
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C14' ]/@RELATIVITY" />
					</xsl:when>
					<!-- Excess Medical,Low Income -->
					<xsl:when test="$PDRIVERINCOME = 'LOW'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C13' ]/@RELATIVITY" />
					</xsl:when>
					<!-- Excess Medical, No dependents -->
					<xsl:when test="$PNODEPENDENT='' or $PNODEPENDENT='NDEP' or $PNODEPENDENT='0'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C12' ]/@RELATIVITY" />
					</xsl:when>
					<!-- Excess Medical -->
					<xsl:when test="$PWAIVEWORKLOSS !='TRUE' and $PNODEPENDENT='1MORE' and $PDRIVERINCOME='HIGH'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C11' ]/@RELATIVITY" />
					</xsl:when>
					<xsl:otherwise> 
						1.00	
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$PPIPCOVERAGECODE = 'EXCESSBOTH'">
				<!-- Excess Both is for Excess Wage/Medical 
			 Excess Workloss  means - Excess Wage was selected. It has no link with the Waiver of Workloss 
		 -->
				<!-- Excess Medical and Excess Workloss, No dependents, Low Income -->
				<xsl:choose>
					<!--xsl:when test="($PNODEPENDENT='' or $PNODEPENDENT='NDEP' or $PNODEPENDENT='0') and $PDRIVERINCOME='LOW'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C19' ]/@RELATIVITY" />
					</xsl:when-->
					<!-- Excess Medical and Excess WorkLoss, No Dependents, Waiver of work Loss -->
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE' and ($PNODEPENDENT='' or $PNODEPENDENT='NDEP' or $PNODEPENDENT='0')">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C20' ]/@RELATIVITY" />
					</xsl:when>
					<!-- Waiver of Workloss  -->
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C14' ]/@RELATIVITY" />
					</xsl:when>
					<!-- Excess Medical and Excess Workloss , Low Income, No dependents  -->
					<xsl:when test="$PDRIVERINCOME = 'LOW' and ($PNODEPENDENT ='' or $PNODEPENDENT='NDEP' or $PNODEPENDENT = '0')">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C19' ]/@RELATIVITY" />
					</xsl:when>
					<!-- Excess Medical and Excess Workloss , Low Income -->
					<xsl:when test="$PDRIVERINCOME = 'LOW'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C18' ]/@RELATIVITY" />
					</xsl:when>
					<!-- Excess Medical and Excess Workloss, No dependents -->
					<xsl:when test="$PNODEPENDENT='' or $PNODEPENDENT='NDEP' or $PNODEPENDENT='0'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C17' ]/@RELATIVITY" />
					</xsl:when>
					<!-- Excess Medical and Excess Workloss , High Income, 1 or more dependents -->
					<xsl:when test="$PDRIVERINCOME = 'HIGH' and $PNODEPENDENT='1MORE'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C16' ]/@RELATIVITY" />
					</xsl:when>
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="COVERAGETYPE_DISPLAY">
		<xsl:variable name="PPIPCOVERAGECODE" select="VEHICLES/VEHICLE/MEDPM"></xsl:variable>
		<xsl:variable name="PDRIVERINCOME" select="VEHICLES/VEHICLE/DRIVERINCOME"></xsl:variable>
		<xsl:variable name="PNODEPENDENT" select="VEHICLES/VEHICLE/DEPENDENTS"></xsl:variable>
		<xsl:variable name="PWAIVEWORKLOSS" select="VEHICLES/VEHICLE/WAIVEWORKLOSS"></xsl:variable>
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = $VT_ANTIQUE_CAR">0</xsl:when>
			<xsl:when test="$PPIPCOVERAGECODE = 'FULL' or $PPIPCOVERAGECODE = 'FULLMEDICALFULLWAGELOSS' or $PPIPCOVERAGECODE = 'PRIMARY' "> <!-- Full PIP -->
				<xsl:choose>
					<!-- Full PIP ,Waiver of Work Loss, No dependents -->
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE' and ($PNODEPENDENT = '' or  $PNODEPENDENT = 'NDEP' or  $PNODEPENDENT = '0')"> 
					Discount :  No dependents, Work Loss Waiver<xsl:value-of select="' '" />-<xsl:value-of select="' '" /><xsl:call-template name="COVERAGETYPE_DISCOUNT_PERCENTAGE" /><xsl:value-of select="' '" />(PIP)
				</xsl:when>
					<!-- Full PIP , No dependents, Low Income -->
					<xsl:when test="($PNODEPENDENT = '' or  $PNODEPENDENT = 'NDEP' or  $PNODEPENDENT = '0') and $PDRIVERINCOME= 'LOW'">
						   Discount : No dependents, Low Income<xsl:value-of select="' '" />-<xsl:value-of select="' '" /><xsl:call-template name="COVERAGETYPE_DISCOUNT_PERCENTAGE" /><xsl:value-of select="' '" />(PIP)
					</xsl:when>
					<!-- Full PIP , Waiver of work loss -->
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE'"> 
						Discount : Work Loss Waiver<xsl:value-of select="' '" />-<xsl:value-of select="' '" /><xsl:call-template name="COVERAGETYPE_DISCOUNT_PERCENTAGE" /><xsl:value-of select="' '" />(PIP)
					</xsl:when>
					<!-- Full PIP , Low Income -->
					<xsl:when test="$PDRIVERINCOME = 'LOW'"> 
						Discount : Low Income<xsl:value-of select="' '" />-<xsl:value-of select="' '" /><xsl:call-template name="COVERAGETYPE_DISCOUNT_PERCENTAGE" /><xsl:value-of select="' '" />(PIP)
					</xsl:when>
					<!-- Full PIP ,No dependents -->
					<xsl:when test="$PNODEPENDENT ='' or  $PNODEPENDENT = 'NDEP' or  $PNODEPENDENT = '0'">
						Discount : No dependents<xsl:value-of select="' '" />-<xsl:value-of select="' '" /><xsl:call-template name="COVERAGETYPE_DISCOUNT_PERCENTAGE" /><xsl:value-of select="' '" />(PIP)
					</xsl:when>
					<!-- Full PIP -->
					<xsl:when test="$PWAIVEWORKLOSS != 'TRUE' and  $PNODEPENDENT = '1MORE' and $PDRIVERINCOME !='LOW'">
						0
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$PPIPCOVERAGECODE = 'EXCESSWAGE'">
				<!-- Excess Workloss  means - Excess Wage was selected. It has no link with the Waiver of Workloss 
												- Full PIP is included -->
				<xsl:choose>
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE' and ($PNODEPENDENT='' or $PNODEPENDENT='NDEP' or $PNODEPENDENT='0')">
						Discount : No dependents, Work Loss Waiver<xsl:value-of select="' '" />-<xsl:value-of select="' '" /><xsl:call-template name="COVERAGETYPE_DISCOUNT_PERCENTAGE" /><xsl:value-of select="' '" />(PIP)	
					</xsl:when>
					<!-- Full PIP and Excess Workloss, No dependents, Low Income -->
					<xsl:when test="($PNODEPENDENT = '' or  $PNODEPENDENT = 'NDEP' or  $PNODEPENDENT= '0') and $PDRIVERINCOME = 'LOW'"> 
						Discount : No dependents, Low Income<xsl:value-of select="' '" />-<xsl:value-of select="' '" /><xsl:call-template name="COVERAGETYPE_DISCOUNT_PERCENTAGE" /><xsl:value-of select="' '" />(PIP) 
					</xsl:when>
					<!-- Full PIP and Excess Workloss, No dependents -->
					<xsl:when test="$PNODEPENDENT ='' or  $PNODEPENDENT = 'NDEP' or $PNODEPENDENT = '0'"> 
						Discount : No dependents<xsl:value-of select="' '" />-<xsl:value-of select="' '" /><xsl:call-template name="COVERAGETYPE_DISCOUNT_PERCENTAGE" /><xsl:value-of select="' '" />(PIP)
					</xsl:when>
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE'">
						Discount : Work Loss Waiver<xsl:value-of select="' '" />-<xsl:value-of select="' '" /><xsl:call-template name="COVERAGETYPE_DISCOUNT_PERCENTAGE" /><xsl:value-of select="' '" />(PIP)
					</xsl:when>
					<!-- Full PIP and Excess Workloss , Low Income -->
					<xsl:when test="$PDRIVERINCOME = 'LOW'"> 
						Discount : Low Income<xsl:value-of select="' '" />-<xsl:value-of select="' '" /><xsl:call-template name="COVERAGETYPE_DISCOUNT_PERCENTAGE" /><xsl:value-of select="' '" />(PIP)
					</xsl:when>
					<!-- Full PIP and Excess Workloss -->
					<xsl:when test="$PWAIVEWORKLOSS  != 'TRUE' and  $PNODEPENDENT ='1MORE'  and $PDRIVERINCOME ='HIGH'">
						0
					 </xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$PPIPCOVERAGECODE = 'EXCESSMEDICAL'">
				<xsl:choose>
					<!-- Excess Medical, No dependents ,Waiver of Work Loss -->
					<xsl:when test="($PNODEPENDENT = '' or  $PNODEPENDENT = 'NDEP' or  $PNODEPENDENT = '0') and $PWAIVEWORKLOSS = 'TRUE'"> 
							Discount : No dependents, Work Loss Waiver<xsl:value-of select="' '" />-<xsl:value-of select="' '" /><xsl:call-template name="COVERAGETYPE_DISCOUNT_PERCENTAGE" /><xsl:value-of select="' '" />(PIP)
						</xsl:when>
					<!-- Excess Medical, No dependents, Low Income -->
					<xsl:when test="($PNODEPENDENT = '' or  $PNODEPENDENT ='NDEP' or  $PNODEPENDENT = '0') and $PDRIVERINCOME = 'LOW'"> 
							Discount : No dependents, Low Income<xsl:value-of select="' '" />-<xsl:value-of select="' '" /><xsl:call-template name="COVERAGETYPE_DISCOUNT_PERCENTAGE" /><xsl:value-of select="' '" />(PIP)
						</xsl:when>
					<!-- Excess Medical, Waiver of work loss -->
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE'"> 
							Discount : Work Loss Waiver<xsl:value-of select="' '" />-<xsl:value-of select="' '" /><xsl:call-template name="COVERAGETYPE_DISCOUNT_PERCENTAGE" /><xsl:value-of select="' '" />(PIP)
						</xsl:when>
					<!-- Excess Medical,Low Income -->
					<xsl:when test="$PDRIVERINCOME = 'LOW'"> 
							Discount : Low Income<xsl:value-of select="' '" />-<xsl:value-of select="' '" /><xsl:call-template name="COVERAGETYPE_DISCOUNT_PERCENTAGE" /><xsl:value-of select="' '" />(PIP)
						</xsl:when>
					<!-- Excess Medical, No dependents -->
					<xsl:when test="$PNODEPENDENT = '' or  $PNODEPENDENT = 'NDEP' or  $PNODEPENDENT = '0'"> 
							Discount : No dependents<xsl:value-of select="' '" />-<xsl:value-of select="' '" /><xsl:call-template name="COVERAGETYPE_DISCOUNT_PERCENTAGE" /><xsl:value-of select="' '" />(PIP)
						</xsl:when>
					<!-- Excess Medical -->
					<xsl:when test="$PWAIVEWORKLOSS != 'TRUE' and  $PNODEPENDENT ='1MORE' and $PDRIVERINCOME = 'HIGH'">
							0
						</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$PPIPCOVERAGECODE = 'EXCESSBOTH'">
				<!-- Excess Both is for Excess Wage/Medical 
					Excess Workloss  means - Excess Wage was selected. It has no link with the Waiver of Workloss -->
				<xsl:choose>
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE' and ($PNODEPENDENT='' or $PNODEPENDENT='NDEP' or $PNODEPENDENT='0')">
								Discount -  No Dependents, Work Loss Waiver<xsl:value-of select="' '" />-<xsl:value-of select="' '" /><xsl:call-template name="COVERAGETYPE_DISCOUNT_PERCENTAGE" /><xsl:value-of select="' '" />(PIP)
					</xsl:when>
					<!-- Excess Medical and Excess Workloss, Waiver of work loss -->
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE'"> 
							Discount : Work Loss Waiver<xsl:value-of select="' '" />-<xsl:value-of select="' '" /><xsl:call-template name="COVERAGETYPE_DISCOUNT_PERCENTAGE" /><xsl:value-of select="' '" />(PIP)
					</xsl:when>
					<!-- Excess Medical and Excess Workloss, No dependents, Low Income -->
					<xsl:when test="($PNODEPENDENT  = '' or  $PNODEPENDENT = 'NDEP' or  $PNODEPENDENT = '0') and $PDRIVERINCOME = 'LOW'">
								Discount : No dependents, Low Income<xsl:value-of select="' '" />-<xsl:value-of select="' '" /><xsl:call-template name="COVERAGETYPE_DISCOUNT_PERCENTAGE" /><xsl:value-of select="' '" />(PIP)
							</xsl:when>
					<!-- Excess Medical and Excess Workloss, No dependents -->
					<xsl:when test="$PNODEPENDENT ='' or  $PNODEPENDENT = 'NDEP' or  $PNODEPENDENT = '0'">
								Discount : No dependents<xsl:value-of select="' '" />-<xsl:value-of select="' '" /><xsl:call-template name="COVERAGETYPE_DISCOUNT_PERCENTAGE" /><xsl:value-of select="' '" />(PIP)
							</xsl:when>
					<!-- Excess Medical and Excess Workloss , Low Income -->
					<xsl:when test="$PDRIVERINCOME = 'LOW'">
								Discount : Low Income<xsl:value-of select="' '" />-<xsl:value-of select="' '" /><xsl:call-template name="COVERAGETYPE_DISCOUNT_PERCENTAGE" /><xsl:value-of select="' '" />(PIP)
					</xsl:when>
					<!-- Excess Medical and Excess Workloss -->
					<xsl:when test="$PDRIVERINCOME !='LOW' and $PNODEPENDENT = '1MORE'">
								0
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
			0
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="COVERAGETYPE_DISCOUNT_PERCENTAGE">
		<xsl:variable name="PPIPCOVERAGECODE" select="VEHICLES/VEHICLE/MEDPM"></xsl:variable>
		<xsl:variable name="PDRIVERINCOME" select="VEHICLES/VEHICLE/DRIVERINCOME"></xsl:variable>
		<xsl:variable name="PNODEPENDENT" select="VEHICLES/VEHICLE/DEPENDENTS"></xsl:variable>
		<xsl:variable name="PWAIVEWORKLOSS" select="VEHICLES/VEHICLE/WAIVEWORKLOSS"></xsl:variable>
		<xsl:variable name="PPIPFACTOR">
			<xsl:call-template name="COVERAGETYPE" />
		</xsl:variable>
		<xsl:variable name="PPIPFACTOR_PERCENT">
			<xsl:choose>
				<xsl:when test="$PPIPFACTOR != '' and $PPIPFACTOR &lt; 1.00">
					<xsl:value-of select="round((1 - $PPIPFACTOR) * 100)" />%
				</xsl:when>
				<xsl:when test="$PPIPFACTOR != '' and $PPIPFACTOR &gt; 1.00">
					<xsl:value-of select="round(($PPIPFACTOR -1 ) * 100)" />% 
				</xsl:when>
			</xsl:choose>
		</xsl:variable>
		<xsl:value-of select="$PPIPFACTOR_PERCENT" />
	</xsl:template>
	<!-- ***************************************************************************************************************************  -->
	<!-- ****************************************  Symbol  *************************************************************************** -->
	<!-- ***************************************************************************************************************************  -->
	<xsl:template name="SYMBOL">
		<xsl:param name="FACTORELEMENT" />
		<!--<xsl:variable name="PAGE" select="VEHICLES/VEHICLE/AGE"></xsl:variable>  select="AGE" check with deepak -->
		<xsl:variable name="PYEAR" select="VEHICLES/VEHICLE/YEAR"></xsl:variable>
		<xsl:variable name="PSYMBOL" select="VEHICLES/VEHICLE/SYMBOL"></xsl:variable>
		<xsl:choose>
			<xsl:when test="$PYEAR !='' and $PSYMBOL !='' and $PSYMBOL !='0'">
				<xsl:variable name="VAR_SYMBOL_FACTOR">
					<xsl:choose>
						<xsl:when test="$FACTORELEMENT='COMP'">
							<xsl:choose>
								<xsl:when test="$PYEAR &gt;= 1990">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SYMBOLRELATIVITYOTHERTHANCOLLISON']/NODE[@ID='SYMBOLRELATIVITYOTHERTHANCOLL']/ATTRIBUTES[@NINETYPLUS = $PSYMBOL]/@RELATIVITY" />
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SYMBOLRELATIVITYOTHERTHANCOLLISON']/NODE[@ID='SYMBOLRELATIVITYOTHERTHANCOLL']/ATTRIBUTES[@EIGHTYNINEANDPRIOR = $PSYMBOL]/@RELATIVITY" />
								</xsl:otherwise>
							</xsl:choose>
						</xsl:when>
						<xsl:when test="$FACTORELEMENT = 'COLL' or $FACTORELEMENT = 'LTDCOLL' ">
							<xsl:choose>
								<xsl:when test="$PYEAR &gt;= 1990">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SYMBOLRELATIVITYCOLLISION']/NODE[@ID='SYMBOLRELATIVITYCOLL']/ATTRIBUTES[@NINETYPLUS = $PSYMBOL ]/@RELATIVITY" />
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SYMBOLRELATIVITYCOLLISION']/NODE[@ID='SYMBOLRELATIVITYCOLL']/ATTRIBUTES[@EIGHTYNINEANDPRIOR = $PSYMBOL ]/@RELATIVITY" />
								</xsl:otherwise>
							</xsl:choose>
						</xsl:when>
						<xsl:otherwise>1.00</xsl:otherwise>
					</xsl:choose>
				</xsl:variable>
				<xsl:choose>
					<xsl:when test="$VAR_SYMBOL_FACTOR =''">0.00</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$VAR_SYMBOL_FACTOR" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
			0.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Deductible -->
	<xsl:template name="DEDUCTIBLE">
		<xsl:param name="FACTORELEMENT" />
		<xsl:choose>
			<xsl:when test="$FACTORELEMENT = 'COMP'">
				<xsl:variable name="PCOMPREHENSIVEDEDUCTIBLE" select="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE" />
				<xsl:choose>
					<xsl:when test="$PCOMPREHENSIVEDEDUCTIBLE !='' and $PCOMPREHENSIVEDEDUCTIBLE &gt; 0">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLEO']/NODE[@ID='DEDTBLO']/ATTRIBUTES[@DEDUCTIBLE = $PCOMPREHENSIVEDEDUCTIBLE ]/@RELATIVITY" />
					</xsl:when>
					<xsl:otherwise>
					0.00
				</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT = 'COLL' and POLICY/STATENAME ='MICHIGAN'">
				<xsl:variable name="PCOLLISIONTYPE" select="VEHICLES/VEHICLE/COVGCOLLISIONTYPE" />
				<xsl:variable name="PCOVGCOLLISIONDEDUCTIBLE" select="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE" />
				<xsl:choose>
					<xsl:when test="$PCOLLISIONTYPE !='' and $PCOVGCOLLISIONDEDUCTIBLE !='' and $PCOVGCOLLISIONDEDUCTIBLE &gt; 0">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@COLLISIONTYPE = $PCOLLISIONTYPE and @DEDUCTIBLE = $PCOVGCOLLISIONDEDUCTIBLE ]/@RELATIVITY" />
					</xsl:when>
					<xsl:otherwise>
							0.00
						</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT = 'COLL' and POLICY/STATENAME ='INDIANA'">
				<xsl:variable name="PCOVGCOLLISIONDEDUCTIBLE" select="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE" />
				<xsl:choose>
					<xsl:when test="$PCOVGCOLLISIONDEDUCTIBLE !='' and $PCOVGCOLLISIONDEDUCTIBLE &gt; 0">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@DEDUCTIBLE = $PCOVGCOLLISIONDEDUCTIBLE ]/@RELATIVITY" />
					</xsl:when>
					<xsl:otherwise>
						0.00
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Limit Factor for Limited Collision-->
	<xsl:template name="LIMITFACTOR">
		<xsl:variable name="PCOLLISIONTYPE" select="VEHICLES/VEHICLE/COVGCOLLISIONTYPE" />
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@COLLISIONTYPE = $PCOLLISIONTYPE ]/@RELATIVITY" />
	</xsl:template>
	<!-- End of Deductibles -->
	<!-- Stated Amount -->
	<xsl:template name="STATEDAMT">
	1
</xsl:template>
	<!-- Fiberglass or Inner Shield -->
	<xsl:template name="FIBERGLASS">
	1
</xsl:template>
	<!-- ********************************* To display the PIP text ******************************************************** -->
	<xsl:template name="PIP_TEXT">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/MEDPM = 'FULL' or VEHICLES/VEHICLE/MEDPM = 'FULLMEDICALFULLWAGELOSS'">Full Premium</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/MEDPM = 'EXCESSWAGE'">Excess Wage</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/MEDPM = 'EXCESSMEDICAL'">Excess Medical</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/MEDPM = 'EXCESSBOTH'">Excess Wage/Medical </xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- PIP DEDUCTIBLE -->
	<xsl:template name="PIP_DEDUCTIBLE">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/MEDPM = 'FULL' or VEHICLES/VEHICLE/MEDPMDEDUCTIBLE='0'"></xsl:when>
			<xsl:otherwise>
				<!--logic : When PIP is selected as other than primary , in that case , for PIP in Deductible column should have value as $300 -->
				<xsl:value-of select="VEHICLES/VEHICLE/MEDPMDEDUCTIBLE" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Insurance score in percentage -->
	<xsl:template name="INSURANCESCORE_PERCENT">
		<xsl:variable name="VAR_SCORE">
			<xsl:call-template name="INSURANCESCORE" />
		</xsl:variable>
		<xsl:variable name="VAR_SCORE_PERCENT">
			<xsl:choose>
				<xsl:when test="$VAR_SCORE != '' and $VAR_SCORE &lt; 1.00">
					<xsl:value-of select="round((1 - $VAR_SCORE) * 100)" />%
				</xsl:when>
				<xsl:when test="$VAR_SCORE != '' and $VAR_SCORE &gt; 1.00">
					<xsl:value-of select="round(($VAR_SCORE -1 ) * 100)" />% 
				</xsl:when>
			</xsl:choose>
		</xsl:variable>
		<xsl:value-of select="$VAR_SCORE_PERCENT" />
	</xsl:template>
	<xsl:template name="INSURENCESCOREDISPLAY">
		<xsl:choose>
			<xsl:when test="normalize-space(POLICY/INSURANCESCORE)='NOHITNOSCORE'">No Hit No Score</xsl:when>
			<xsl:when test="normalize-space(POLICY/INSURANCESCORE)&gt;=0">
				<xsl:value-of select="POLICY/INSURANCESCORE" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Safe driver Renewal Discount and Premior driver discount -->
	<xsl:template name="SAFEDRIVERDISPLAY">
		<xsl:variable name="PSURCHARGE">
			<xsl:call-template name="SURCHARGE"></xsl:call-template>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/SAFEDRIVER='TRUE' and $PSURCHARGE = 1">Discount : Safe Driver Renewal -</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/PREMIERDRIVER='TRUE' and VEHICLES/VEHICLE/SAFEDRIVER='FALSE'">Discount : Premier Driver -</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Business Use Surcharge    -->
	<xsl:template name="BUSINESS_SURCHRAGE">
		<xsl:variable name="PVEHUSE" select="VEHICLES/VEHICLE/VEHICLEUSE" />
		<xsl:choose>
			<xsl:when test="normalize-space(VEHICLES/VEHICLE/CARPOOL)=normalize-space('YES')">0</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="($PVEHUSE='B' or $PVEHUSE='C' or $PVEHUSE='W') and (VEHICLES/VEHICLE/VEHICLETYPE !=$VT_SUSPENDED_COMP  and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_UTILITY_TRAILER and VEHICLES/VEHICLE/VEHICLETYPE !=$VT_CAMPER_TRAVEL_TRAILER)">Included</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="BUSINESS_SURCHRAGE_PERCENTAGE">
		<xsl:variable name="PVEHUSE" select="VEHICLES/VEHICLE/VEHICLEUSE" />
		<xsl:variable name="PDRIVERCLASS" select="VEHICLES/VEHICLE/VEHICLECLASSCOMPONENT1" />
		<xsl:choose>
			<xsl:when test="$PVEHUSE='B' or $PVEHUSE='C' or $PVEHUSE='W'">
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/VEHICLECLASS !=''">
						<xsl:variable name="PVEHUSEFACTOR" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEUSERELATIVITY']/NODE[@ID='VEHICLEUSE']/ATTRIBUTES[@USECODE = $PVEHUSE and @CLASS = $PDRIVERCLASS]/@FACTOR" />
						<xsl:value-of select="round(($PVEHUSEFACTOR - 1)*100)" />%
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="INSURANCE_SCORE_DISPLAY">
		<xsl:variable name="INS1" select="POLICY/INSURANCESCORE"></xsl:variable>
		<xsl:variable name="INS">
			<xsl:value-of select="translate(translate($INS1,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
		</xsl:variable>
		<!-- FOR MAXIMUM SCORE -->
		<xsl:variable name="VAR_MAX_SCORE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MAX = 'MAX_SCORE']/@MINSCORE" />
		<xsl:variable name="INSDISCOUNT">
			<xsl:choose>
				<xsl:when test="$INS = 0">
					<xsl:value-of select="$INS"></xsl:value-of>
				</xsl:when>
				<xsl:when test="$INS = ''">1.00</xsl:when>
				<xsl:when test="$INS = 'NOHITNOSCORE'"> <!-- NO HIT NO SCORE -->
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = 'N']/@FACTOR" />
				</xsl:when>
				<xsl:when test="$INS &gt;= $VAR_MAX_SCORE">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = $VAR_MAX_SCORE]/@FACTOR" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@FACTOR" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$INSDISCOUNT &lt; 1">
					Discount : Insurance Score Credit (<xsl:call-template name="INSURENCESCOREDISPLAY" />) - <xsl:call-template name="INSURANCESCORE_PERCENT"></xsl:call-template>	
					</xsl:when>
			<xsl:otherwise>
					Surcharge : Insurance Score Credit (<xsl:call-template name="INSURENCESCOREDISPLAY" />) - <xsl:call-template name="INSURANCESCORE_PERCENT"></xsl:call-template>	
					</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="INSURANCE_SCORE_COMPONENT_TYPE">
		<xsl:variable name="INS1" select="POLICY/INSURANCESCORE"></xsl:variable>
		<xsl:variable name="INS">
			<xsl:value-of select="translate(translate($INS1,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
		</xsl:variable>
		<!-- FOR MAXIMUM SCORE -->
		<xsl:variable name="VAR_MAX_SCORE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MAX = 'MAX_SCORE']/@MINSCORE" />
		<xsl:variable name="INSDISCOUNT">
			<xsl:choose>
				<xsl:when test="$INS = 0">
					<xsl:value-of select="$INS"></xsl:value-of>
				</xsl:when>
				<xsl:when test="$INS = ''">1.00</xsl:when>
				<xsl:when test="$INS = 'NOHITNOSCORE'"> <!-- NO HIT NO SCORE -->
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = 'N']/@FACTOR" />
				</xsl:when>
				<xsl:when test="$INS &gt;= $VAR_MAX_SCORE">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = $VAR_MAX_SCORE]/@FACTOR" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@FACTOR" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$INSDISCOUNT &lt; 1">D</xsl:when>
			<xsl:otherwise>S</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Now onward VEHICLEUSE is determined fully from Proc node value - Asfa Praveen (06-June-2007)-->
	<!--xsl:template name="VEHICLE_USE">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLEUSE =''"></xsl:when>
			<xsl:when test="(VEHICLES/VEHICLE/MILESEACHWAY &lt;= 10 and VEHICLES/VEHICLE/MILESEACHWAY &gt; 0 ) and (substring(VEHICLES/VEHICLE/VEHICLECLASS,1,1)!=$VEHICLE_CLASS_PRE_2 or substring(VEHICLES/VEHICLE/VEHICLECLASS,1,1)!=$VEHICLE_CLASS_PRE_3 or substring(VEHICLES/VEHICLE/VEHICLECLASS,1,1)!=$VEHICLE_CLASS_PRE_4 or substring(VEHICLES/VEHICLE/VEHICLECLASS,1,1)!=$VEHICLE_CLASS_PRE_6)">P</xsl:when>
			<xsl:when test="substring(VEHICLES/VEHICLE/VEHICLECLASS,1,1)=$VEHICLE_CLASS_PRE_2 or substring(VEHICLES/VEHICLE/VEHICLECLASS,1,1)=$VEHICLE_CLASS_PRE_3 or substring(VEHICLES/VEHICLE/VEHICLECLASS,1,1)=$VEHICLE_CLASS_PRE_4 or substring(VEHICLES/VEHICLE/VEHICLECLASS,1,1)=$VEHICLE_CLASS_PRE_6">
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/VEHICLEUSE='P'"></xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLEUSE='O' or VEHICLES/VEHICLE/VEHICLEUSE ='o'">W</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="VEHICLES/VEHICLE/VEHICLEUSE" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template-->
	<xsl:template name="PDDEDUCTIBLE">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/PDDEDUCTIBLE !='0'">
				<xsl:value-of select="VEHICLES/VEHICLE/PDDEDUCTIBLE" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- START OF TEMPLATE FOR COMP_REMARK -->
	<!-- PIP-->
	<xsl:template name="COVERAGETEXT_DISPLAY">
		<xsl:variable name="PPIPCOVERAGECODE" select="VEHICLES/VEHICLE/MEDPM"></xsl:variable>
		<xsl:variable name="PDRIVERINCOME" select="VEHICLES/VEHICLE/DRIVERINCOME"></xsl:variable>
		<xsl:variable name="PNODEPENDENT" select="VEHICLES/VEHICLE/DEPENDENTS"></xsl:variable>
		<xsl:variable name="PWAIVEWORKLOSS" select="VEHICLES/VEHICLE/WAIVEWORKLOSS"></xsl:variable>
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = $VT_ANTIQUE_CAR">0</xsl:when>
			<xsl:when test="$PPIPCOVERAGECODE = 'FULL' or $PPIPCOVERAGECODE = 'FULLMEDICALFULLWAGELOSS' or $PPIPCOVERAGECODE = 'PRIMARY' "> <!-- Full PIP -->
				<xsl:choose>
					<!-- Full PIP ,Waiver of Work Loss, No dependents -->
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE' and ($PNODEPENDENT = '' or  $PNODEPENDENT = 'NDEP' or  $PNODEPENDENT = '0')"> 
						Discount :  No dependents, Work Loss Waiver<xsl:value-of select="' '" />(PIP)
					</xsl:when>
					<!-- Full PIP , No dependents, Low Income -->
					<xsl:when test="($PNODEPENDENT = '' or  $PNODEPENDENT = 'NDEP' or  $PNODEPENDENT = '0') and $PDRIVERINCOME= 'LOW'">
						   Discount : No dependents, Low Income<xsl:value-of select="' '" />(PIP)
					</xsl:when>
					<!-- Full PIP , Waiver of work loss -->
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE'"> 
						Discount : Work Loss Waiver<xsl:value-of select="' '" />(PIP)
					</xsl:when>
					<!-- Full PIP , Low Income -->
					<xsl:when test="$PDRIVERINCOME = 'LOW'"> 
						Discount : Low Income<xsl:value-of select="' '" />(PIP)
					</xsl:when>
					<!-- Full PIP ,No dependents -->
					<xsl:when test="$PNODEPENDENT ='' or  $PNODEPENDENT = 'NDEP' or  $PNODEPENDENT = '0'">
						Discount : No dependents<xsl:value-of select="' '" />(PIP)
					</xsl:when>
					<!-- Full PIP -->
					<xsl:when test="$PWAIVEWORKLOSS != 'TRUE' and  $PNODEPENDENT = '1MORE' and $PDRIVERINCOME !='LOW'">
						0
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$PPIPCOVERAGECODE = 'EXCESSWAGE'">
				<!-- Excess Workloss  means - Excess Wage was selected. It has no link with the Waiver of Workloss 
												- Full PIP is included -->
				<xsl:choose>
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE' and ($PNODEPENDENT='' or $PNODEPENDENT='NDEP' or $PNODEPENDENT='0')">
						Discount : No dependents, Work Loss Waiver<xsl:value-of select="' '" />(PIP)	
					</xsl:when>
					<!-- Full PIP and Excess Workloss, No dependents, Low Income -->
					<xsl:when test="($PNODEPENDENT = '' or  $PNODEPENDENT = 'NDEP' or  $PNODEPENDENT= '0') and $PDRIVERINCOME = 'LOW'"> 
						Discount : No dependents, Low Income<xsl:value-of select="' '" />(PIP) 
					</xsl:when>
					<!-- Full PIP and Excess Workloss, No dependents -->
					<xsl:when test="$PNODEPENDENT ='' or  $PNODEPENDENT = 'NDEP' or $PNODEPENDENT = '0'"> 
						Discount : No dependents<xsl:value-of select="' '" />(PIP)
					</xsl:when>
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE'">
						Discount : Work Loss Waiver<xsl:value-of select="' '" />(PIP)
					</xsl:when>
					<!-- Full PIP and Excess Workloss , Low Income -->
					<xsl:when test="$PDRIVERINCOME = 'LOW'"> 
						Discount : Low Income<xsl:value-of select="' '" />(PIP)
					</xsl:when>
					<!-- Full PIP and Excess Workloss -->
					<xsl:when test="$PWAIVEWORKLOSS  != 'TRUE' and  $PNODEPENDENT ='1MORE'  and $PDRIVERINCOME ='HIGH'">
						0
					 </xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$PPIPCOVERAGECODE = 'EXCESSMEDICAL'">
				<xsl:choose>
					<!-- Excess Medical, No dependents ,Waiver of Work Loss -->
					<xsl:when test="($PNODEPENDENT = '' or  $PNODEPENDENT = 'NDEP' or  $PNODEPENDENT = '0') and $PWAIVEWORKLOSS = 'TRUE'"> 
							Discount : No dependents, Work Loss Waiver<xsl:value-of select="' '" />(PIP)
						</xsl:when>
					<!-- Excess Medical, No dependents, Low Income -->
					<xsl:when test="($PNODEPENDENT = '' or  $PNODEPENDENT ='NDEP' or  $PNODEPENDENT = '0') and $PDRIVERINCOME = 'LOW'"> 
							Discount : No dependents, Low Income<xsl:value-of select="' '" />(PIP)
						</xsl:when>
					<!-- Excess Medical, Waiver of work loss -->
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE'"> 
							Discount : Work Loss Waiver<xsl:value-of select="' '" />(PIP)
						</xsl:when>
					<!-- Excess Medical,Low Income -->
					<xsl:when test="$PDRIVERINCOME = 'LOW'"> 
							Discount : Low Income<xsl:value-of select="' '" />(PIP)
						</xsl:when>
					<!-- Excess Medical, No dependents -->
					<xsl:when test="$PNODEPENDENT = '' or  $PNODEPENDENT = 'NDEP' or  $PNODEPENDENT = '0'"> 
							Discount : No dependents<xsl:value-of select="' '" />(PIP)
						</xsl:when>
					<!-- Excess Medical -->
					<xsl:when test="$PWAIVEWORKLOSS != 'TRUE' and  $PNODEPENDENT ='1MORE' and $PDRIVERINCOME = 'HIGH'">
							0
						</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="$PPIPCOVERAGECODE = 'EXCESSBOTH'">
				<!-- Excess Both is for Excess Wage/Medical 
					Excess Workloss  means - Excess Wage was selected. It has no link with the Waiver of Workloss -->
				<xsl:choose>
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE' and ($PNODEPENDENT='' or $PNODEPENDENT='NDEP' or $PNODEPENDENT='0')">
								Discount -  No Dependents, Work Loss Waiver<xsl:value-of select="' '" />(PIP)
					</xsl:when>
					<!-- Excess Medical and Excess Workloss, Waiver of work loss -->
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE'"> 
							Discount : Work Loss Waiver<xsl:value-of select="' '" />(PIP)
					</xsl:when>
					<!-- Excess Medical and Excess Workloss, No dependents, Low Income -->
					<xsl:when test="($PNODEPENDENT  = '' or  $PNODEPENDENT = 'NDEP' or  $PNODEPENDENT = '0') and $PDRIVERINCOME = 'LOW'">
								Discount : No dependents, Low Income<xsl:value-of select="' '" />(PIP)
							</xsl:when>
					<!-- Excess Medical and Excess Workloss, No dependents -->
					<xsl:when test="$PNODEPENDENT ='' or  $PNODEPENDENT = 'NDEP' or  $PNODEPENDENT = '0'">
								Discount : No dependents<xsl:value-of select="' '" />(PIP)
							</xsl:when>
					<!-- Excess Medical and Excess Workloss , Low Income -->
					<xsl:when test="$PDRIVERINCOME = 'LOW'">
								Discount : Low Income<xsl:value-of select="' '" />(PIP)
					</xsl:when>
					<!-- Excess Medical and Excess Workloss -->
					<xsl:when test="$PDRIVERINCOME !='LOW' and $PNODEPENDENT = '1MORE'">
								0
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
			0
		</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- WEARING SEAT BEALT  -->
	<xsl:template name="REMARKSEATBELT">Discount: Wearing Seat Belts</xsl:template>
	<!--Air Bags-->
	<xsl:template name="REMARKAIRBAG">Discount: Air Bags</xsl:template>
	<!-- Anti-Lock Breaks-->
	<xsl:template name="REMARKANTILOCK">Discount: Anti-Lock Brake System</xsl:template>
	<!-- Multi-Car -->
	<xsl:template name="REMARKMULTICAR">Discount: Multi-Car</xsl:template>
	<!--Multi-Policy (Auto/Home)-->
	<xsl:template name="REMARKMULTIPOLICY">Discount: Multi-Policy (Auto/Home)</xsl:template>
	<!--Trailblazer-->
	<xsl:template name="REMARKTRAILBLAZER">Discount: Trailblazer</xsl:template>
	<!--Insurance Score Credit-->
	<xsl:template name="REMARKINSURANCESCORE">
		<xsl:variable name="INS1" select="POLICY/INSURANCESCORE"></xsl:variable>
		<xsl:variable name="INS">
			<xsl:value-of select="translate(translate($INS1,' ',''),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
		</xsl:variable>
		<!-- FOR MAXIMUM SCORE -->
		<xsl:variable name="VAR_MAX_SCORE" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MAX = 'MAX_SCORE']/@MINSCORE" />
		<xsl:variable name="INSDISCOUNT">
			<xsl:choose>
				<xsl:when test="$INS = 0">
					<xsl:value-of select="$INS"></xsl:value-of>
				</xsl:when>
				<xsl:when test="$INS = ''">1.00</xsl:when>
				<xsl:when test="$INS = 'NOHITNOSCORE'"> <!-- NO HIT NO SCORE -->
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = 'N']/@FACTOR" />
				</xsl:when>
				<xsl:when test="$INS &gt;= $VAR_MAX_SCORE">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = $VAR_MAX_SCORE]/@FACTOR" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@FACTOR" />
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$INSDISCOUNT &lt; 1">
					Discount: Insurance Score Credit (<xsl:call-template name="INSURENCESCOREDISPLAY" />)	
					</xsl:when>
			<xsl:otherwise>
					Surcharge: Insurance Score Credit (<xsl:call-template name="INSURENCESCOREDISPLAY" />)	
					</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!--Premier Driver-->
	<xsl:template name="REMARKSAFEDRIVER">
		<xsl:variable name="PSURCHARGE">
			<xsl:call-template name="SURCHARGE"></xsl:call-template>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/SAFEDRIVER='TRUE' and $PSURCHARGE = 1">Discount: Safe Driver Renewal</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/PREMIERDRIVER='TRUE' and VEHICLES/VEHICLE/SAFEDRIVER='FALSE'">Discount: Premier Driver</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- Good Student-->
	<xsl:template name="REMARKGOODSTUDENT">Discount: Good Student</xsl:template>
	<!--VEHICLE Use-->
	<xsl:template name="REMARKVEHICLEUSE">Surcharge: <xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLEUSE = 'B'">Business Use</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLEUSE = 'W'">Work - Over 25 Miles one way</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLEUSE = 'C'">Vehicle Corporate or Partnership Owned</xsl:when>
		</xsl:choose></xsl:template>
	<!-- Accident and Violation-->
	<xsl:template name="REMARKACCIDENTVIOL">Surcharge : Accident and Violation</xsl:template>
	<!-- END OF TEMPLATE FOR COMP_REMARK -->
	<!-- End of templates-->
	<!-- ****************************************************************************************************************** -->
	<!-- *********************************** Template for Lables   ******************************************************************************** -->
	<!-- ****************************************************************************************************************** -->
	<!--  Group Details  -->
	<xsl:template name="GROUPID0"><xsl:text>VEHICLE : </xsl:text><xsl:value-of select="VEHICLES/VEHICLE/VEHICLETYPEDESC" /></xsl:template>
	<xsl:template name="GROUPID1"><xsl:text>Final Premium</xsl:text></xsl:template>
	<xsl:template name="PRODUCTNAME"><xsl:text>Private Passenger Automobile</xsl:text></xsl:template>
	<xsl:template name="STEPID0"><xsl:value-of select="VEHICLES/VEHICLE/VEHICLEROWID" />.<xsl:value-of select="'   '" /><xsl:value-of select="VEHICLES/VEHICLE/YEAR" /> <xsl:value-of select="'   '" />  <xsl:value-of select="VEHICLES/VEHICLE/MAKE" /> <xsl:value-of select="'   '" />   <xsl:value-of select="VEHICLES/VEHICLE/MODEL" /> <xsl:value-of select="'   '" />     <xsl:text>VIN:</xsl:text><xsl:value-of select="VEHICLES/VEHICLE/VIN" /></xsl:template>
	<xsl:template name="STEPID1"><xsl:text>Symbol: </xsl:text><xsl:value-of select="VEHICLES/VEHICLE/SYMBOL" />,<xsl:value-of select="'   '" /> 
						<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE  =$VT_UTILITY_TRAILER or VEHICLES/VEHICLE/VEHICLETYPE  =$VT_CAMPER_TRAVEL_TRAILER">
								<xsl:text>Actual Cash Value: $</xsl:text><xsl:value-of select="VEHICLES/VEHICLE/COST" />
							</xsl:when>
			<xsl:otherwise>
									Class <xsl:value-of select="VEHICLES/VEHICLE/VEHICLECLASS" /> 
									<!--Asfa Praveen 06-June-2007 -->
									<!--xsl:call-template name="VEHICLE_USE" /-->		
									<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/VEHICLEUSE='O'"></xsl:when>
					<xsl:when test="(VEHICLES/VEHICLE/VEHICLECLASSCOMPONENT1 = '2' or VEHICLES/VEHICLE/VEHICLECLASSCOMPONENT1 = '3' or VEHICLES/VEHICLE/VEHICLECLASSCOMPONENT1 = '4' or VEHICLES/VEHICLE/VEHICLECLASSCOMPONENT1 = '6') and VEHICLES/VEHICLE/VEHICLEUSE='P'"></xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="VEHICLES/VEHICLE/VEHICLEUSE" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="STEPID2">
		<xsl:choose>
			<!--Againt iTrack issue No. 2051 - Asfa Praveen 29/June/2007  -->
			<xsl:when test="VEHICLES/VEHICLE/VEHICLEUSEDESC=normalize-space('DRIVE TO WORK/SCHOOL')">
				<xsl:text>Use: </xsl:text> <xsl:value-of select="VEHICLES/VEHICLE/VEHICLEUSEDESC" />, <xsl:value-of select="'   '" /><xsl:text>Miles Each Way : </xsl:text><xsl:value-of select="VEHICLES/VEHICLE/MILESEACHWAY" />
			</xsl:when>
			<xsl:otherwise><xsl:text>Use: </xsl:text><xsl:value-of select="VEHICLES/VEHICLE/VEHICLEUSEDESC" />,<xsl:value-of select="'   '" /><xsl:value-of select="VEHICLES/VEHICLE/SNOWPLOW" /></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="STEPID3">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/ZIPCODEGARAGEDLOCATION !='' and VEHICLES/VEHICLE/ZIPCODEGARAGEDLOCATION !='0'">
			<xsl:text>Garaged Location: </xsl:text><xsl:value-of select="VEHICLES/VEHICLE/GARAGEDLOCATION" />
		</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="UIMSPLITDISPLAY">
		<xsl:variable name="PUMSPLIT1" select="VEHICLES/VEHICLE/UIMSPLITLIMIT1" />
		<xsl:variable name="PUMSPLIT2" select="VEHICLES/VEHICLE/UIMSPLITLIMIT2" />
		<xsl:variable name="PUIMMIN" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UIMBILIMITS']/NODE[@ID ='UIMBICOMP']/ATTRIBUTES[@MINLIMIT = $PUMSPLIT1]/@MINLIMITCOMP" />
		<xsl:variable name="PUIMMAX" select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UIMBILIMITS']/NODE[@ID ='UIMBICOMP']/ATTRIBUTES[@MINLIMIT = $PUMSPLIT2]/@MAXLIMITCOMP" />
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA'">
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/UIMSPLIT !='' and VEHICLES/VEHICLE/UIMSPLIT != '0/0'  and VEHICLES/VEHICLE/UIMSPLIT != 'NO COVERAGE' and VEHICLES/VEHICLE/UIMSPLIT != '0'">
						<xsl:choose>
							<xsl:when test="$PUIMMIN!='' and $PUIMMAX!=''">
								<xsl:value-of select="$PUIMMIN" />/<xsl:value-of select="$PUIMMAX" />
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="VEHICLES/VEHICLE/UIMSPLIT" />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/UIMCSL !='' and VEHICLES/VEHICLE/UIMCSL != '0' and VEHICLES/VEHICLE/UIMCSL != 'NO COVERAGE' ">
						<xsl:value-of select="VEHICLES/VEHICLE/UIMCSL" />
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/UIMSPLIT !='' and VEHICLES/VEHICLE/UIMSPLIT != '0/0'  and VEHICLES/VEHICLE/UIMSPLIT != 'NO COVERAGE' and VEHICLES/VEHICLE/UIMSPLIT != '0'">
						<xsl:value-of select="VEHICLES/VEHICLE/UIMSPLIT" />
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/UIMCSL !='' and VEHICLES/VEHICLE/UIMCSL != '0' and VEHICLES/VEHICLE/UIMCSL != 'NO COVERAGE' ">
						<xsl:value-of select="VEHICLES/VEHICLE/UIMCSL" />
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="UMSPLITDISPLAY">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/UMSPLIT !='' and VEHICLES/VEHICLE/UMSPLIT != '0/0' and VEHICLES/VEHICLE/UMSPLIT != '0' and VEHICLES/VEHICLE/UMSPLIT != 'NO COVERAGE'">
				<xsl:value-of select="VEHICLES/VEHICLE/UMSPLIT" />
			</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL != '0' and VEHICLES/VEHICLE/UMCSL != 'NO COVERAGE'">
				<xsl:value-of select="VEHICLES/VEHICLE/UMCSL" />
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="STEPID4">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA'"><xsl:text>Bodily Injury Liability</xsl:text></xsl:when>
			<xsl:when test="POLICY/STATENAME ='MICHIGAN'"><xsl:text>Residual Bodily Injury Liability</xsl:text></xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template name="STEPID5">
		<xsl:choose>
			<xsl:when test="POLICY/STATENAME ='INDIANA'"><xsl:text>Property Damage Liability</xsl:text></xsl:when>
			<xsl:when test="POLICY/STATENAME ='MICHIGAN'"><xsl:text>Residual Property Damage Liability</xsl:text></xsl:when>
		</xsl:choose>
	</xsl:template>
	<!--xsl:template name="STEPID6">Residual Liability (BI and PD)</xsl:template-->
	<xsl:template name="STEPID6"><xsl:text>Single Limit Liability (CSL)</xsl:text></xsl:template>
	<xsl:template name="STEPID7"><xsl:text>Personal Injury Protection - </xsl:text><xsl:call-template name="PIP_TEXT" /></xsl:template>
	<xsl:template name="STEPID8"><xsl:text>Michigan Statutory Assessments</xsl:text></xsl:template>
	<xsl:template name="STEPID9"><xsl:text>Michigan Statutory Assessments</xsl:text></xsl:template>
	<xsl:template name="STEPID10"><xsl:text>Property Protection Insurance</xsl:text></xsl:template>
	<xsl:template name="STEPID11"><xsl:text>Medical Payments</xsl:text></xsl:template>
	<xsl:template name="STEPID12"><xsl:text>Uninsured Motorists</xsl:text></xsl:template>
	<xsl:template name="STEPID13"><xsl:text>Underinsured Motorists</xsl:text></xsl:template>
	<xsl:template name="STEPID14"><xsl:text>Uninsured Motorists Property Damage</xsl:text></xsl:template>
	<xsl:template name="STEPID15"><xsl:text>Damage to Your Auto - Comprehensive</xsl:text></xsl:template>
	<xsl:template name="STEPID16"><xsl:text>Damage to Your Auto - </xsl:text><xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONTYPE = 'BROAD'"><xsl:text>Broadened</xsl:text></xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONTYPE = 'LIMITED'"><xsl:text>Limited</xsl:text></xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONTYPE = 'REGULAR'"><xsl:text>Regular</xsl:text></xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose><xsl:value-of select="' '" /><xsl:text>Collision</xsl:text></xsl:template>
	<xsl:template name="STEPID17"><xsl:text>Mini-Tort PD Liability</xsl:text></xsl:template>
	<xsl:template name="STEPID18"><xsl:text>Road Service</xsl:text></xsl:template>
	<xsl:template name="STEPID19"><xsl:text>Rental Reimbursement</xsl:text></xsl:template>
	<xsl:template name="STEPID20"><xsl:text>Loan/Lease Gap Coverage</xsl:text></xsl:template>
	<xsl:template name="STEPID21"><xsl:text>Sound Reproducing Tapes</xsl:text></xsl:template>
	<xsl:template name="STEPID22"><xsl:text>Sound Receiving And Transmitting Equipment</xsl:text></xsl:template>
	<xsl:template name="STEPID23"><xsl:text>Extra Equipment - Comprehensive</xsl:text></xsl:template>
	<xsl:template name="STEPID24"><xsl:text>Extra Equipment - </xsl:text><xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONTYPE = 'BROAD'"><xsl:text>Broadened</xsl:text></xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONTYPE = 'LIMITED'"><xsl:text>Limited</xsl:text></xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONTYPE = 'REGULAR'"><xsl:text>Regular</xsl:text></xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose><xsl:value-of select="' '" /><xsl:text>Collision</xsl:text></xsl:template>
	<xsl:template name="STEPID25"><xsl:text>Extended Non-Owned Coverage for Named Individual</xsl:text></xsl:template>
	<xsl:template name="STEPID26">
		<xsl:call-template name="COVERAGETYPE_DISPLAY" />
	</xsl:template>
	<xsl:template name="STEPID27"><xsl:text>Discount : Wearing Seat Belts  - </xsl:text><xsl:call-template name="SEAT_BELTS_CREDIT_PERCENT" /><xsl:text>% (PIP) </xsl:text></xsl:template>
	<xsl:template name="STEPID28"><xsl:text>Discount : Air Bags - </xsl:text><xsl:call-template name="AIRBAG_DISCOUNT_PERCENT" /><xsl:text>% </xsl:text></xsl:template>
	<xsl:template name="STEPID29"><xsl:text>Discount : Anti-Lock Brake System - </xsl:text><xsl:call-template name="ABS_CREDIT" /> </xsl:template>
	<xsl:template name="STEPID30"><xsl:text>Discount : Multi-Car</xsl:text></xsl:template>
	<xsl:template name="STEPID31"><xsl:text>Discount : Multi-Policy (Auto/Home) - </xsl:text><xsl:call-template name="MULTIPOLICY_DISCOUNT" /><xsl:text>%</xsl:text></xsl:template>
	<xsl:template name="STEPID32"><xsl:text>Discount : Trailblazer - </xsl:text><xsl:call-template name="TRAILBLAZER_DISCOUNT" /><xsl:text>%</xsl:text></xsl:template>
	<xsl:template name="STEPID33">
		<xsl:call-template name="INSURANCE_SCORE_DISPLAY" />
	</xsl:template>
	<xsl:template name="STEPID34">
		<xsl:call-template name="SAFEDRIVERDISPLAY" />
		<xsl:value-of select="' '" />
		<xsl:call-template name="DRIVERDISCOUNT_CREDIT" />
	</xsl:template>
	<xsl:template name="STEPID35"><xsl:text>Discount : Good Student -</xsl:text><xsl:call-template name="GOODSTUDENT_DISCOUNT_PERCENT" /><xsl:text>% </xsl:text></xsl:template>
	<xsl:template name="STEPID36"><xsl:text>Surcharge : </xsl:text><xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLEUSE = 'B'"><xsl:text>Business Use - </xsl:text></xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLEUSE = 'W'"><xsl:text>Work - Over 25 Miles one way - </xsl:text></xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLEUSE = 'C'"><xsl:text>Vehicle Corporate or Partnership Owned - </xsl:text></xsl:when>
		</xsl:choose><xsl:call-template name="BUSINESS_SURCHRAGE_PERCENTAGE" /></xsl:template>
	<xsl:template name="STEPID37"><xsl:text>Surcharge : Accident and Violation - </xsl:text><xsl:call-template name="ACCIDENT_VIOLATION_SURCHRAGE_PERCENTAGE" /><xsl:text>% </xsl:text></xsl:template>
	<xsl:template name="STEPID38"><xsl:text>Total Minimum Vehicle Premium </xsl:text></xsl:template>
	<xsl:template name="STEPID39"><xsl:text>Total Vehicle Premium</xsl:text></xsl:template>
	<xsl:template name="COMPONENT_CODE">
		<xsl:param name="FACTORELEMENT" />
		<xsl:choose>
			<xsl:when test="$FACTORELEMENT='BI'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
					<xsl:text>114</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
					<xsl:text>2</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='PD'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
				<xsl:text>115</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
				<xsl:text>4</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='CSL'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
				<xsl:text>113</xsl:text>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
				<xsl:text>1</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='PIP'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
				<xsl:text>116</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='PPI'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
				<xsl:text>117</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='MED_PMT'">
				<xsl:if test="POLICY/STATENAME='INDIANA'">
				<xsl:text>6</xsl:text>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='UM'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
					<xsl:choose>
						<xsl:when test="(VEHICLES/VEHICLE/UMSPLIT !='' and VEHICLES/VEHICLE/UMSPLIT !='0/0' and VEHICLES/VEHICLE/UMSPLIT !='NO COVERAGE') ">
					<xsl:text>120</xsl:text>
				</xsl:when>
						<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL !='0' and VEHICLES/VEHICLE/UMCSL !='NO COVERAGE' ">
					<xsl:text>119</xsl:text>
				</xsl:when>
					</xsl:choose>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
					<xsl:choose>
						<xsl:when test="(VEHICLES/VEHICLE/UMSPLIT !='' and VEHICLES/VEHICLE/UMSPLIT !='0/0' and VEHICLES/VEHICLE/UMSPLIT !='NO COVERAGE') ">
							<xsl:text>12</xsl:text>
						</xsl:when>
						<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL !='0' and VEHICLES/VEHICLE/UMCSL !='NO COVERAGE' ">
							<xsl:text>9</xsl:text>
						</xsl:when>
					</xsl:choose>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='UIM'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
					<xsl:choose>
						<xsl:when test="(VEHICLES/VEHICLE/UMSPLIT !='' and VEHICLES/VEHICLE/UMSPLIT !='0/0' and VEHICLES/VEHICLE/UMSPLIT !='NO COVERAGE') ">
					<xsl:text>121</xsl:text>
				</xsl:when>
						<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL !='0' and VEHICLES/VEHICLE/UMCSL !='NO COVERAGE' ">
					<xsl:text>304</xsl:text>
				</xsl:when>
					</xsl:choose>
				</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
					<xsl:choose>
						<xsl:when test="(VEHICLES/VEHICLE/UMSPLIT !='' and VEHICLES/VEHICLE/UMSPLIT !='0/0' and VEHICLES/VEHICLE/UMSPLIT !='NO COVERAGE') ">
							<xsl:text>34</xsl:text>
						</xsl:when>
						<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL !='0' and VEHICLES/VEHICLE/UMCSL !='NO COVERAGE' ">
							<xsl:text>14</xsl:text>
						</xsl:when>
					</xsl:choose>
				</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='UMPD'">
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>36</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='COMP'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
						<xsl:text>123</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>42</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='COLL'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
						<xsl:text>122</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>38</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='M_TRT'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
						<xsl:text>118</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='RD_SRVC'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
						<xsl:text>124</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>44</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='RNT_RMBRS'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
						<xsl:text>125</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>45</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='LN_LSE'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
						<xsl:text>249</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>46</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='SND_RPR'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
						<xsl:text>252</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>50</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='SND_RCV'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
						<xsl:text>1030</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>1029</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='XTR_COMP'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
						<xsl:text>301</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>300</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='XTR_COLL'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
						<xsl:text>303</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>302</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='XTR_COLL'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
						<xsl:text>303</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>302</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='ENO'">
				<xsl:if test="POLICY/STATENAME='MICHIGAN'">
						<xsl:text>254</xsl:text>
					</xsl:if>
				<xsl:if test="POLICY/STATENAME='INDIANA'">
						<xsl:text>52</xsl:text>
					</xsl:if>
			</xsl:when>
			<xsl:when test="$FACTORELEMENT='MCCAFEE'"><xsl:text>20007</xsl:text>
			</xsl:when>
			<xsl:otherwise></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>
