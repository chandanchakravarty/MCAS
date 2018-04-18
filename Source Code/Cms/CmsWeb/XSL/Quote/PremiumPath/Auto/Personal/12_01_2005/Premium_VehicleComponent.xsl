
<!-- ============================================================================================ -->
<!-- File Name		:	Primium.xsl																  -->
<!-- Description	:	This xsl file is for generating 
						the Final Primium (For vehicle component Auto)INDIANA and MICHIGAN		  -->
<!-- Developed By	:	Nidhi Sahay													  -->		
<!-- ============================================================================================ -->

<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  version="1.0">
<xsl:output encoding="ISO8859-1" omit-xml-declaration="no" method="xml"/>


<!-- ============================================================================================ -->
<!--								Loading ProductFactorMaster File (START)					  -->
<!-- ============================================================================================ -->

<xsl:variable name="HOCoveragesDoc" select="document('FactorPath')"></xsl:variable>

<!-- ============================================================================================ -->
<!--								Loading ProductFactorMaster File (END)						  -->
<!-- ============================================================================================ -->
<xsl:template match="/">
	<xsl:apply-templates select="INPUTXML/QUICKQUOTE"/>
</xsl:template>

<xsl:template match="INPUTXML/QUICKQUOTE">
<PREMIUM>
	<CREATIONDATE></CREATIONDATE>
	<GETPATH>
		<PRODUCT PRODUCTID="0" > 			
			
			<!--Group for Vehicles-->
			<GROUP GROUPID="0" >
				<xsl:attribute name="DESC"><xsl:call-template name="GROUPID0"/></xsl:attribute> 
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
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
									<xsl:call-template name="BI"></xsl:call-template>
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
									<xsl:call-template name="PD"></xsl:call-template>
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
									<xsl:call-template name="CSLBIPD"></xsl:call-template>
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
								<xsl:call-template name="PIP"></xsl:call-template>
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
								<xsl:call-template name="MCCAFEE"></xsl:call-template>
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				
				<!-- PPI -->
				<SUBGROUP>
					<IF>
						<STEP STEPID="9">				 
							<PATH>
								{
									<xsl:call-template name="PPI"></xsl:call-template>
								}
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				
				<!-- Medical Payments -->
				<SUBGROUP>
					<IF>
						<STEP STEPID="10">				 
							<PATH>
								{
									<xsl:call-template name="MEDPAY"></xsl:call-template>
								}
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				
				<!--Uninsured Motorists -->
				<SUBGROUP>
					<IF>
						<STEP STEPID="11">				 
							<PATH>
								{
									<xsl:call-template name="UM"></xsl:call-template>
								}
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				
				<!-- Under Insured Motorists -->
				<SUBGROUP>
					<IF>
						<STEP STEPID="12">				 
							<PATH>
								{
									<xsl:call-template name="UIM"></xsl:call-template>
								}
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				
				<!--Uninsured Motorists - Property Damagae-->
				<SUBGROUP>
					<IF>
						<STEP STEPID="13">				 
							<PATH>
								{
									<xsl:call-template name="UMPD"></xsl:call-template>
								}
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				
				<!-- Comprehensive -->
				<SUBGROUP>
					<IF>
						<STEP STEPID="14">				 
							<PATH>
								{
									<xsl:call-template name="COMP"></xsl:call-template>
								}
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				
				<!-- Collision -->
				<SUBGROUP>
					<IF>
						<STEP STEPID="15">				 
							<PATH>
								 {
									<xsl:call-template name="COLL"/>  
								 }
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				
				<!-- Mini-tort -->
				<SUBGROUP>
					<IF>
						<STEP STEPID="16">				 
							<PATH>
								<xsl:call-template name="MINITORT"></xsl:call-template>
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				
				<!-- Road Service -->
				<SUBGROUP>
					<IF>
						<STEP STEPID="17">				 
							<PATH>
								<xsl:call-template name="ROADSERVICES"></xsl:call-template>
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				
				<!-- Rental Reimbursement -->
				<SUBGROUP>
					<IF>
						<STEP STEPID="18">
							<PATH>
								<xsl:call-template name="RENTALREIMBURSEMENT"></xsl:call-template>							
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				
				<!-- Loan/Loss Lease Gap-->
				<SUBGROUP>
					<IF>
						<STEP STEPID="19">	  	 
							<PATH>
								{
									 <xsl:call-template name="LOANLEASEGAP"></xsl:call-template>
								}
							</PATH> 
						</STEP>
					</IF>
				</SUBGROUP>
				
				<!-- Sound Reproducing Tapes -->
				<SUBGROUP>
					<IF>
						<STEP STEPID="20">				 
							<PATH>
								{
								<xsl:call-template name="SOUNDREPRODUCINGTAPES"></xsl:call-template>
								}
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				
				<!-- Sound Receiving and Transmitting Equipments -->
				<SUBGROUP>
					<IF>
						<STEP STEPID="21">				 
							<PATH>
								{
								<xsl:call-template name="SOUNDRECVTRANSEQUIPMENT"></xsl:call-template>
								}
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				
				<!-- Extra Equipment - Comprehensive  -->
				<SUBGROUP>
					<IF>
						<STEP STEPID="22">				 
							<PATH>
								{
									<xsl:call-template name="EXTRAEQUIPMENTCOMP"></xsl:call-template>
								}
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				
				<!-- Extra Equipment -Broadened Collision -->
				<SUBGROUP>
					<IF>
						<STEP STEPID="23">				 
							<PATH>
								{
									<xsl:call-template name="EXTRAEQUIPMENTCOLL"></xsl:call-template>
								}
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				
				
				<!-- Discount:PIP Discount Display -->
				<SUBGROUP>
					  <IF>
						<STEP STEPID="24">				 
							<PATH>
							  {													
									<xsl:call-template name="PIP_DISPLAY"/> 							
							  }
							</PATH>
						</STEP>
					</IF>
				</SUBGROUP>
				
				
				<!-- Discount:Wearing Seat Belts -->
				<SUBGROUP>
					
						<STEP STEPID="25">				 
							<PATH>
								
								<xsl:call-template name="SEATBELT_DISPLAY"></xsl:call-template>
								
							</PATH>
						</STEP>
					
				</SUBGROUP>
				
				<!-- Discount:Air Bags -->
				<SUBGROUP>
					
						<STEP STEPID="26">				 
							<PATH>
							
								<xsl:call-template name="AIRBAG_DISPLAY"></xsl:call-template>
								
							</PATH>
						</STEP>
					
				</SUBGROUP>
				
				<!-- Discount:Anti-Lock Breaks System -->
				<SUBGROUP>
					 
						<STEP STEPID="27">				 
							<PATH>
								 
								<xsl:call-template name="ABS_DISPLAY"></xsl:call-template>
								 
							</PATH>
						</STEP>
					 
				</SUBGROUP>
				
				<!-- Discount:Multi-Car-->
				<SUBGROUP>
					
						<STEP STEPID="28">				 
							<PATH>
								
									<xsl:call-template name="MULTIVEHICLE_DISPLAY"></xsl:call-template>
								
							</PATH>
						</STEP>
					
				</SUBGROUP>
				
				<!-- Discount:Multi-Policy(Auto/Home)-->
				<SUBGROUP>
					 
						<STEP STEPID="29">				 
							<PATH>
								 
								<xsl:call-template name="MULTIPOLICY_DISPLAY"></xsl:call-template>
								 
							</PATH>
						</STEP>
				 
				</SUBGROUP>
				
				<!-- Discount:Insurance Score Credit-->
				<SUBGROUP>
					 
						<STEP STEPID="30">				 
							<PATH>
								 
									<xsl:call-template name="INSURANCESCORE_DISPLAY"></xsl:call-template>
								 
							</PATH>
						</STEP>
					 
				</SUBGROUP>
				
				<!-- Discount:Premier Driver -->
				<SUBGROUP>
					 
						<STEP STEPID="31">				 
							<PATH>
								 
									<xsl:call-template name="DRIVERDISCOUNT_DISPLAY"></xsl:call-template>
								 
							</PATH>
						</STEP>
					 
				</SUBGROUP>
				
				<!-- Discount:Good Student -->
				<SUBGROUP>
					 <IF>
						<STEP STEPID="32">				 
							<PATH>
									{		 
									<xsl:call-template name="GOODSTUDENT_DISPLAY"></xsl:call-template>
								 }
							</PATH>
						</STEP>
					 </IF>
				</SUBGROUP>
			</GROUP>
			
			<!--Group for Final Premium-->
			<GROUP GROUPID="1" >
				<xsl:attribute name="DESC"><xsl:call-template name="GROUPID1"/></xsl:attribute> 
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
				</GROUPCONDITION> 
				
				<!-- Final Premium -->
				<SUBGROUP>
					<IF>
					<STEP STEPID="33">
						<PATH>
							{
								<xsl:call-template name="FINALPREMIUM"></xsl:call-template>
							}
						</PATH>
					</STEP>			
					</IF>
				</SUBGROUP>
			</GROUP>
			 
		</PRODUCT>
	</GETPATH>
	<CALCULATION>
		<PRODUCT PRODUCTID="0" >
			
			<!--Product Name -->
			<xsl:attribute name="DESC"><xsl:call-template name="PRODUCTNAME"/></xsl:attribute> 
					
			
			<!--Group for Vehicles-->
			<GROUP GROUPID="0" CALC_ID="10000"> 
				<xsl:attribute name="DESC"><xsl:call-template name="GROUPID0"/></xsl:attribute> 
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
				</GROUPCONDITION> 
				<GROUPRULE></GROUPRULE>
				<GROUPFORMULA></GROUPFORMULA>
				<GDESC></GDESC>
				
				<!-- Vehicle year VIN -->
				<STEP STEPID="0" CALC_ID="1000" PREFIX="" SUFIX ="" ISCALCULATE="F"> 
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID0"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<!-- Symbol, Class-->
				<STEP STEPID="1" CALC_ID="1001" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID1"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<!-- Use ,Miles each way-->
				<STEP STEPID="2" CALC_ID="1002" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID2"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<!-- Garaged At, Territory-->
				<STEP STEPID="3" CALC_ID="1003" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID3"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<!-- Residual Bodily Injury -->
				<STEP STEPID="4" CALC_ID="1004" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID4"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH><xsl:value-of select="VEHICLES/VEHICLE/BI" /></L_PATH>
				</STEP> 
				
				<!-- Residual Property Damage -->
				<STEP STEPID="5" CALC_ID="1005" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID5"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH><xsl:value-of select="VEHICLES/VEHICLE/PD" /></L_PATH>
				</STEP> 
				
				<!-- Combined Single Limit (Bodily Injury, PD) -->
				<STEP STEPID="6" CALC_ID="1006" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID6"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH><xsl:value-of select="VEHICLES/VEHICLE/CSL" /></L_PATH>
				</STEP> 
				
				<!-- PIP -->
				<STEP STEPID="7" CALC_ID="1007" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID7"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH><xsl:call-template name="PIP_DEDUCTIBLE"/></D_PATH>
					<L_PATH>Unlimited</L_PATH>
				</STEP> 
				
				<!-- Michigan Statutory Assessment -->
				<STEP STEPID="8" CALC_ID="1008" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID8"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<!-- PPI -->
				<STEP STEPID="9" CALC_ID="1009" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID9"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH>1,000,000</L_PATH>
				</STEP> 
				
				<!-- Medical Payments -->
				<STEP STEPID="10" CALC_ID="1010" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID10"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<!-- Uninsured Motorist -->
				<STEP STEPID="11" CALC_ID="1011" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID11"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH><xsl:value-of select="VEHICLES/VEHICLE/UMSPLIT" /></L_PATH>
				</STEP> 
				
				<!-- Underinsured Motorist -->
				<STEP STEPID="12" CALC_ID="1012" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID12"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH><xsl:value-of select="VEHICLES/VEHICLE/UMSPLIT" /></L_PATH>
				</STEP> 
				
				<!-- Uninsured Motorist -Property Damage-->
				<STEP STEPID="13" CALC_ID="1013" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID13"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH><xsl:value-of select="VEHICLES/VEHICLE/PDDEDUCTIBLE" /></D_PATH>
					<L_PATH><xsl:value-of select="VEHICLES/VEHICLE/PDLIMIT" /></L_PATH>
				</STEP>
				
				<!-- Comprehensive -->
				<STEP STEPID="14" CALC_ID="1014" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID14"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH><xsl:value-of select="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE" /></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<!-- Collision -->
				<STEP STEPID="15" CALC_ID="1015" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID15"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH><xsl:value-of select="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE" /></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<!-- Mini-tort -->
				<STEP STEPID="16" CALC_ID="1016" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID16"/></xsl:attribute> 
					<PATH></PATH>
					<D_PATH></D_PATH>
					<L_PATH><xsl:call-template name="MINITORTLIMIT"/></L_PATH>
				</STEP> 
				
				<!-- Road Service -->
				<STEP STEPID="17" CALC_ID="1017" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID17"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH><xsl:value-of select="VEHICLES/VEHICLE/ROADSERVICE" /></L_PATH>
				</STEP> 
				
				<!-- Rental Reimbursement -->
				<STEP STEPID="18" CALC_ID="1018" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID18"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH><xsl:value-of select="VEHICLES/VEHICLE/RENTALREIMBURSEMENT" /></L_PATH>
				</STEP> 
				
				<!-- Loan/Loss Lease Gap-->
				<STEP STEPID="19" CALC_ID="1019" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID19"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<!-- Sound Reproducing Tapes -->
				<STEP STEPID="20" CALC_ID="1020" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID20"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH><xsl:call-template name="SOUNDREPRODUCINGTAPESLIMIT"/></L_PATH>
				</STEP> 
				
				<!-- Sound Receiving and Transmitting Equipments -->
				<STEP STEPID="21" CALC_ID="1021" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID21"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH><xsl:value-of select="VEHICLES/VEHICLE/SOUNDRECEIVINGTRANSMITTINGSYSTEM"/></L_PATH>
				</STEP> 
				
				<!-- Extra Equipment - Comprehensive  -->
				<STEP STEPID="22" CALC_ID="1022" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID22"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH><xsl:value-of select="VEHICLES/VEHICLE/EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE" /></D_PATH>
					<L_PATH><xsl:value-of select="VEHICLES/VEHICLE/INSURANCEAMOUNT" /></L_PATH>
				</STEP> 
				
				<!-- Extra Equipment -Broadened Collision -->
				<STEP STEPID="23" CALC_ID="1023" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID23"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH><xsl:value-of select="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONDEDUCTIBLE" /></D_PATH>
					<L_PATH><xsl:value-of select="VEHICLES/VEHICLE/INSURANCEAMOUNT" /></L_PATH>
				</STEP> 
				
				<!-- Discount:Work Loss Waiver -->
				<STEP STEPID="24" CALC_ID="1024" PREFIX="" SUFIX ="" ISCALCULATE="T">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID24"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<!-- Discount:Wearing Seat Belts -->
				<STEP STEPID="25" CALC_ID="1025" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID25"/></xsl:attribute> 
					<PATH></PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<!-- Discount:Air Bags -->
				<STEP STEPID="26" CALC_ID="1026" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID26"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<!-- Discount:Anti-Lock Breaks System -->
				<STEP STEPID="27" CALC_ID="1027" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID27"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<!-- Discount:Multi-Car-->
				<STEP STEPID="28" CALC_ID="1028" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID28"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<!-- Discount:Multi-Policy(Auto/Home)-->
				<STEP STEPID="29" CALC_ID="1029" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID29"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<!-- Discount:Insurance Score Credit-->
				<STEP STEPID="30" CALC_ID="1030" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID30"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<!-- Discount:Premier Driver -->
				<STEP STEPID="31" CALC_ID="1031" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID31"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
				
				<!-- Discount:Good Student -->
				<STEP STEPID="32" CALC_ID="1032" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID32"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
				</STEP> 
			</GROUP> 
			
			<!--Group for Final Premium-->
			<GROUP GROUPID="1" CALC_ID="10001" PREFIX="" SUFIX ="" ISCALCULATE="F">
				<xsl:attribute name="DESC"><xsl:call-template name="GROUPID1"/></xsl:attribute> 
				<GROUPCONDITION>
					<CONDITION>
					</CONDITION>
				</GROUPCONDITION> 
				<GROUPRULE></GROUPRULE>
				<GROUPFORMULA></GROUPFORMULA>
				<GDESC></GDESC>
				
				<!-- Total Premium -->
				<STEP STEPID="33" CALC_ID="1033" PREFIX="" SUFIX ="" ISCALCULATE="F">
					<xsl:attribute name="DESC"><xsl:call-template name="STEPID33"/></xsl:attribute> 
					<PATH>0</PATH>
					<D_PATH></D_PATH>
					<L_PATH></L_PATH>
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
	<xsl:choose>
	
		<!-- For Michigan -Personal Automobile -->
		<xsl:when test ="POLICY/STATENAME ='MICHIGAN'" >
			<xsl:call-template name ="FINALPREMIUM_PERSONAL_MICHIGAN"/>
		</xsl:when>	
		<!-- For Indiana -Personal Automobile -->
		<xsl:when test ="POLICY/STATENAME ='INDIANA'" >
			<xsl:call-template name ="FINALPREMIUM_PERSONAL_INDIANA"/>
		</xsl:when>	
		
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<!-- For Michigan -Personal Automobile -->

<xsl:template name="FINALPREMIUM_PERSONAL_MICHIGAN">	 
	
	<xsl:choose>
			<!-- When vehicle type is suspended comp then only comprehensive will be calculated  -->
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SCO'">
				<xsl:call-template name ="COMP"/>
			</xsl:when>
			<xsl:otherwise>
			(
				<!-- If it is combined single limit then BI and PD will not be calculated separately.-->
				<xsl:choose>	
					<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL=''">			
						(<xsl:call-template name ="BI"/>)
						+
						(<xsl:call-template name ="PD"/>)
					</xsl:when>
					<xsl:otherwise>
						(<xsl:call-template name ="CSLBIPD"/>)				
					</xsl:otherwise>	 
				</xsl:choose>
			)
			+
			(
				<xsl:call-template name ="PPI"/>
			)	  
			+
			(
				<xsl:call-template name ="PIP"/>
			)
			+
			(
				<xsl:call-template name ="UM"/>
				
			)
			+
			(
		 
				<!--Add UIM only if IsUnderInsuredMotorist is true/-->
 				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/ISUNDERINSUREDMOTORISTS ='TRUE'">
						<xsl:call-template name ="UIM"/>
					</xsl:when>
					<xsl:otherwise>
						0.00
					</xsl:otherwise>		
				</xsl:choose>		
			)
			+
			(
				<xsl:call-template name ="COMP"/>
				
			)
			+
			(
				<xsl:choose>
					<xsl:when test="COVGCOLLISIONTYPE ='LIMITED'">
						<xsl:call-template name ="LTDCOLL"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:call-template name ="COLL"/>	
					</xsl:otherwise>		
				</xsl:choose>
				
				
			) 	  
			+
			(
				<xsl:call-template name ="MINITORT"/>
				
			)
			+
			(
				<xsl:call-template name ="ROADSERVICES"/>
			)
			+
			(
				<xsl:call-template name ="RENTALREIMBURSEMENT"/>
			)
			+
			(
				<xsl:call-template name ="LOANLEASEGAP"/>
			)
			+
			(
				<xsl:call-template name ="SOUNDRECVTRANSEQUIPMENT"/>
			)
			+
			(
				<xsl:call-template name ="MCCAFEE"/>
			)
		 
 			</xsl:otherwise>
	 </xsl:choose>	
	 
	 <!-- Added By praveen Singh For Implementing Round -->
	
	<!-- <xsl:variable name="VAR1">
	 <xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL=''">			
				<xsl:variable name="VAR_BI"> <xsl:call-template name ="BI"/> </xsl:variable>
				<xsl:variable name="VAR_PD"> <xsl:call-template name ="PD"/> </xsl:variable>
				 <xsl:value-of select="round($VAR_BI+$VAR_PD)"/>
				 
			</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name ="CSLBIPD"/>				
			</xsl:otherwise>	 
		</xsl:choose>
		
		
	 
	 </xsl:variable>
	 <xsl:variable name="VAR2"><xsl:call-template name ="PPI"/></xsl:variable>
	 <xsl:variable name="VAR3"><xsl:call-template name ="PIP"/></xsl:variable>
	 <xsl:variable name="VAR4"><xsl:call-template name ="UM"/></xsl:variable>
	 
	  
	 <xsl:variable name="VAR5">
		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/ISUNDERINSUREDMOTORISTS ='TRUE'">
				<xsl:call-template name ="UIM"/>
			</xsl:when>
			<xsl:otherwise>
				0.00
			</xsl:otherwise>		
		</xsl:choose>
	</xsl:variable>
	 
	 
	 <xsl:variable name="VAR6"><xsl:call-template name ="COMP"/></xsl:variable>
	 
	 <xsl:variable name="VAR7">
		 <xsl:choose>
				<xsl:when test="COVGCOLLISIONTYPE ='LIMITED'">
					<xsl:call-template name ="LTDCOLL"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:call-template name ="COLL"/>	
				</xsl:otherwise>		
		</xsl:choose>
	 </xsl:variable>
	
	 <xsl:variable name="VAR8"><xsl:call-template name ="MINITORT"/></xsl:variable>
	 <xsl:variable name="VAR9"><xsl:call-template name ="ROADSERVICES"/></xsl:variable>
	 <xsl:variable name="VAR10"><xsl:call-template name ="RENTALREIMBURSEMENT"/></xsl:variable>
	 <xsl:variable name="VAR11"><xsl:call-template name ="SOUNDRECVTRANSEQUIPMENT"/></xsl:variable>
	 <xsl:variable name="VAR12"><xsl:call-template name ="MCCAFEE"/></xsl:variable>
	  
	  
		
	<xsl:value-of select ="round($VAR1)+round($VAR2)+round($VAR3)+round($VAR4)+round($VAR5)+round($VAR6)+round($VAR7)+round($VAR8)+round($VAR9)+round($VAR10)+round($VAR11)+round($VAR12)"/>
	
	 -->
	 
	 
</xsl:template>
<!-- End of Michigan -Personal Automobile -->

<!-- For Indiana -Personal Automobile -->
<xsl:template name="FINALPREMIUM_PERSONAL_INDIANA">
<!--	
	<xsl:choose>
			  When vehicle type is suspended comp then only comprehensive will be calculated   
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SCO'">
				<xsl:call-template name ="COMP"/>
			</xsl:when>
			<xsl:otherwise>
				  If it is combined single limit then BI and PD will not be calculated separately. 
				(
					<xsl:choose>
						<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL=''">			
							(<xsl:call-template name ="BI"/>)
							+
							(<xsl:call-template name ="PD"/>)
							
						</xsl:when>
						<xsl:otherwise>
							(<xsl:call-template name ="CSLBIPD"/>)				
						</xsl:otherwise>	 
					</xsl:choose>
				)	 	  
				+
				(
					<xsl:call-template name ="MEDPAY"/>
				)
				+
				(
					<xsl:call-template name ="UM"/>
					
				)
				+
				(
					 Add UIM only if IsUnderInsuredMotorist is true   
					<xsl:choose>
						<xsl:when test="VEHICLES/VEHICLE/ISUNDERINSUREDMOTORISTS ='FALSE'">
							0.00
						</xsl:when>
						<xsl:otherwise>
							<xsl:call-template name ="UIM"/>	
						</xsl:otherwise>		
					</xsl:choose>		
				)
				+
				(
					<xsl:call-template name ="COMP"/>
					
				)
				+
				(
					<xsl:choose>
						<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONTYPE !='' and VEHICLES/VEHICLE/COVGCOLLISIONTYPE ='LIMITED'">
							<xsl:call-template name ="LTDCOLL"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:call-template name ="COLL"/>	
						</xsl:otherwise>		
					</xsl:choose>
				)
				+
				(
					<xsl:call-template name ="ROADSERVICES"/>
				)
				+
				(
					<xsl:call-template name ="RENTALREIMBURSEMENT"/>
				)
			</xsl:otherwise>
	 </xsl:choose>
	 -->
	 
	 <!-- new calculation implemented by praveen singh --> 
	 <xsl:choose>
			<!--  When vehicle type is suspended comp then only comprehensive will be calculated    -->
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SCO'">
				<xsl:call-template name ="COMP"/>
			</xsl:when>
			<xsl:otherwise>
				<!--  If it is combined single limit then BI and PD will not be calculated separately. -->
				<xsl:variable name="VAR1">
					<xsl:choose>
						<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL=''">			
							
							<xsl:variable name="VAR_BI"><xsl:call-template name ="BI"/></xsl:variable>
							<xsl:variable name="VAR_PD"><xsl:call-template name ="PD"/></xsl:variable>
						
						<xsl:value-of select="$VAR_BI+$VAR_PD"/>
						
						
						</xsl:when>
						
						<xsl:otherwise>
							<xsl:call-template name ="CSLBIPD"/> 			
						</xsl:otherwise>	 
					</xsl:choose>
				</xsl:variable>	 	  
				
				<xsl:variable name="VAR2">
					<xsl:call-template name ="MEDPAY"/>
				</xsl:variable>	 	  
				
				<xsl:variable name="VAR3">
					<xsl:call-template name ="UM"/>
				</xsl:variable>	 	  
				
				<xsl:variable name="VAR4">

					<!-- Add UIM only if IsUnderInsuredMotorist is true   -->
					<xsl:choose>
						<xsl:when test="VEHICLES/VEHICLE/ISUNDERINSUREDMOTORISTS ='FALSE'">
							0.00
						</xsl:when>
						<xsl:otherwise>
							<xsl:call-template name ="UIM"/>	
						</xsl:otherwise>		
					</xsl:choose>		
				</xsl:variable>	 	  
				
				<xsl:variable name="VAR5">
					<xsl:call-template name ="COMP"/>
				</xsl:variable>	 	  
				
				<xsl:variable name="VAR6">
					<xsl:choose>
						<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONTYPE !='' and VEHICLES/VEHICLE/COVGCOLLISIONTYPE ='LIMITED'">
							<xsl:call-template name ="LTDCOLL"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:call-template name ="COLL"/>	
						</xsl:otherwise>		
					</xsl:choose>
				</xsl:variable>	 	  
				
				<xsl:variable name="VAR7">
					<xsl:call-template name ="ROADSERVICES"/>
				</xsl:variable>	 	  
				
				<xsl:variable name="VAR8">
					<xsl:call-template name ="RENTALREIMBURSEMENT"/>
				</xsl:variable>	
				
				<xsl:value-of select="$VAR1+$VAR2+$VAR3+$VAR4+$VAR5+$VAR6+$VAR7+$VAR8"/>
				
				
			</xsl:otherwise>
	 </xsl:choose>
	 
	 
	 
</xsl:template>
<!-- End of Indiana -Personal Automobile -->



<!-- ######################################### END OF FINAL PREMIUM ################# -->

<!-- Start of BI AND PD -->
<xsl:template name ="CSLBIPD">
<!--
 	<xsl:choose>
	      Suspended comp will return 0 
		<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SCO' or VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL='' or VEHICLES/VEHICLE/CSL='NO COVERAGE' ">
			0.00
		</xsl:when>
		<xsl:otherwise>
		 
		  Multiply 
				1. Territory Base for BI and PD
				2. Insurance Score
				3. Driver Class Relativity
				4. CSL Relativity
				5. CSL Rate for single car
				6. Multi-car Discount
				7. CSL Rate for multi-car  
		 
		 		(
		 			(
					<xsl:call-template name ="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>		
					</xsl:call-template> 
					)
					+
					(
					<xsl:call-template name ="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>		
					</xsl:call-template> 
					)
				)
				*
				(
				<xsl:call-template name ="INSURANCESCORE"/>					
				)
				*
				(
				<xsl:call-template name ="CLASS">						  Driver Clss Relativity  
					<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param> 
				</xsl:call-template>
				)
				*
				(
				<xsl:call-template name ="VEHICLEUSE"/>
				)
				*
				(
				<xsl:call-template name ="DRIVERDISCOUNT"/>
				)
				*
				(
				<xsl:call-template name ="GOODSTUDENT"/>
				)
				*
				(				
				<xsl:call-template name ="SURCHARGE"/>
				)
				*			
				(
					<xsl:choose>
						<xsl:when test="MULTICARDISCOUNT='TRUE'">		  Multi car discount  
							<xsl:call-template name ="MULTICARDISCOUNTCSL"/>		
						</xsl:when>				
						<xsl:otherwise>
							1.00
						</xsl:otherwise>
					</xsl:choose>
				)
				*
				(
				<xsl:call-template name ="CSLRELATIVITY" />				 CSL Rate for single car  				
				)
		</xsl:otherwise>
	</xsl:choose>
	-->
	
	<xsl:choose>
	   <!--   Suspended comp will return 0  -->
		<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SCO' or VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL='' or VEHICLES/VEHICLE/CSL='NO COVERAGE' ">
			0.00
		</xsl:when>
		<xsl:otherwise>
		 
		 <!-- Multiply 
				1. Territory Base for BI and PD
				2. Insurance Score
				3. Driver Class Relativity
				4. CSL Relativity
				5. CSL Rate for single car
				6. Multi-car Discount
				7. CSL Rate for multi-car  
		 
		        calculation formula below
		 		(
		 			(1)+(2)
		 		)
		 		*(3)*(4)*(5)*(6)*(7)*(8)*(9)*(10)
			-->	
				
				<xsl:variable name="VAR1"> 
					<xsl:call-template name ="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>		
					</xsl:call-template> 
				</xsl:variable>
				
				<xsl:variable name="VAR2">
					<xsl:call-template name ="TERRITORYBASE">
						<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>		
					</xsl:call-template> 
				</xsl:variable>
				
				<xsl:variable name="VAR3"> 
					<xsl:call-template name ="INSURANCESCORE"/>					
				</xsl:variable>
				
				<xsl:variable name="VAR4"> 
					<xsl:call-template name ="CLASS">					<!--	  Driver Clss Relativity   -->
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param> 
					</xsl:call-template>
				</xsl:variable>
				
				<xsl:variable name="VAR5"> 
				<xsl:call-template name ="VEHICLEUSE"/>
				 </xsl:variable>
				
				<xsl:variable name="VAR6">
				<xsl:call-template name ="DRIVERDISCOUNT"/>
				  </xsl:variable>
				
				<xsl:variable name="VAR7">
					<xsl:call-template name ="GOODSTUDENT"/> 
				</xsl:variable>
				
				<xsl:variable name="VAR8"> 
					<xsl:call-template name ="SURCHARGE"/>
				</xsl:variable>
				
				<xsl:variable name="VAR9"> 
					<xsl:choose>
						<xsl:when test="MULTICARDISCOUNT='TRUE'">		<!--   Multi car discount   -->
							<xsl:call-template name ="MULTICARDISCOUNTCSL"/>		
						</xsl:when>				
						<xsl:otherwise>
							1.00
						</xsl:otherwise>
					</xsl:choose>
				 </xsl:variable>
				
				<xsl:variable name="VAR10"> 
					<xsl:call-template name ="CSLRELATIVITY" />	
				</xsl:variable>
				 
				
				<xsl:value-of select ="round(round(round(round(round(round(round(round(round($VAR1+$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)"/>  
				
				 
				
		</xsl:otherwise>
	</xsl:choose>
	
	
	
</xsl:template>



<!-- BodilyInjury -->
<xsl:template name="BI">
	<xsl:choose>	
		<!-- For Michigan -Personal Automobile -->
		<xsl:when test ="POLICY/STATENAME ='MICHIGAN'">
			<xsl:call-template name ="BI_PERSONAL_MICHIGAN"/>		
		</xsl:when>	
		<!-- For Indiana -Personal Automobile -->
		<xsl:when test ="POLICY/STATENAME ='INDIANA'">
			<xsl:call-template name ="BI_PERSONAL_INDIANA"/>
		</xsl:when>		
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- Start of BI Personal Michigan -->
<xsl:template name ="BI_PERSONAL_MICHIGAN">
 <!--
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SCO' or VEHICLES/VEHICLE/BI ='NO COVERAGE' or VEHICLES/VEHICLE/BI ='0'  or VEHICLES/VEHICLE/BI ='' or VEHICLES/VEHICLE/BI='0/0'">
			0.00
		</xsl:when>
		<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL=''">
			(
				<xsl:call-template name ="TERRITORYBASE">
					<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>		
				</xsl:call-template> 
			)
			*
			(<xsl:call-template name ="INSURANCESCORE"/>)
			*
			(<xsl:call-template name ="OPTIMAL"/>)
			*
			(<xsl:call-template name ="TYPEOFVEHICLE">
				<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
			</xsl:call-template>)
			*
			(<xsl:call-template name ="CLASS">
				<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
			</xsl:call-template>)
			*
			(<xsl:call-template name ="LIMIT">	
				<xsl:with-param name="FACTORELEMENT" select="'BI'"/>
			</xsl:call-template>)
			*
			(<xsl:call-template name ="MULTIVEHICLE">
				<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>			
			</xsl:call-template>)
			*
			(<xsl:call-template name ="VEHICLEUSE"/>)
			*
			(<xsl:call-template name ="DRIVERDISCOUNT"/>)
			*
			(<xsl:call-template name ="GOODSTUDENT"/>)
			*
			(<xsl:call-template name ="SURCHARGE"/>)
			*
			(<xsl:call-template name ="MULTIPOLICY"/>)
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
-->	 
	 
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/BI ='NO COVERAGE'">
			0.00
		</xsl:when>
		<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL=''">
		
			<xsl:variable name ="VAR1">
		 			<xsl:call-template name ="TERRITORYBASE">
					<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>		
				</xsl:call-template> 
			</xsl:variable>
			
			<xsl:variable name ="VAR2"> <xsl:call-template name ="INSURANCESCORE"/>  </xsl:variable>
			<xsl:variable name ="VAR3"> <xsl:call-template name ="OPTIMAL"/> </xsl:variable>

			<xsl:variable name ="VAR4">
				<xsl:call-template name ="TYPEOFVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
				</xsl:call-template>
			</xsl:variable>
		   
			<xsl:variable name ="VAR5"> 
				<xsl:call-template name ="CLASS">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
				</xsl:call-template>
			</xsl:variable>
			
			<xsl:variable name ="VAR6"> 
				<xsl:call-template name ="LIMIT">	
						<xsl:with-param name="FACTORELEMENT" select="'BI'"/>
				</xsl:call-template>
			</xsl:variable>
			
			<xsl:variable name ="VAR7"> 
				<xsl:call-template name ="MULTIVEHICLE">
						<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>			
				</xsl:call-template>
			</xsl:variable>
			 
			<xsl:variable name ="VAR8"> <xsl:call-template name ="VEHICLEUSE"/> </xsl:variable>
			<xsl:variable name ="VAR9"> <xsl:call-template name ="DRIVERDISCOUNT"/> </xsl:variable>
			<xsl:variable name ="VAR10"> <xsl:call-template name ="GOODSTUDENT"/> </xsl:variable>
			<xsl:variable name ="VAR11"> <xsl:call-template name ="SURCHARGE"/> </xsl:variable>
			<xsl:variable name ="VAR12"> <xsl:call-template name ="MULTIPOLICY"/> </xsl:variable>


		<xsl:value-of select ="round(round(round(round(round(round(round(round(round(round(round(round($VAR1)*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)"/>
</xsl:when>
		
		<xsl:otherwise>0.00</xsl:otherwise>
		
</xsl:choose>
 
</xsl:template>
<!-- End of BI Personal Michigan -->

<!-- Start of BI Commercial Michigan -->
<xsl:template name ="BI_PERSONAL_INDIANA">
 <!-- 
	<xsl:choose> 
		<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SCO' or VEHICLES/VEHICLE/BI ='NO COVERAGE' or VEHICLES/VEHICLE/BI ='0'  or VEHICLES/VEHICLE/BI ='' or VEHICLES/VEHICLE/BI='0/0'">
			0.00
		</xsl:when>
		<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL=''">
			(
				<xsl:call-template name ="TERRITORYBASE">
					<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>		
				</xsl:call-template> 
			)
			*
			(<xsl:call-template name ="MOTORHOME"/>)
			*
			(<xsl:call-template name ="CLASS">
				<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
			</xsl:call-template>)
			*
			(<xsl:call-template name ="LIMIT">	
				<xsl:with-param name="FACTORELEMENT" select="'BI'"/>
			</xsl:call-template>)
			*
			(<xsl:call-template name ="MULTIVEHICLE">
				<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>			
			</xsl:call-template>)
			*
			(<xsl:call-template name ="VEHICLEUSE"/>)			
			*
			(<xsl:call-template name ="DRIVERDISCOUNT"/>)	
			*
			(<xsl:call-template name ="GOODSTUDENT"/>)
			*
			(<xsl:call-template name ="INSURANCESCORE"/>)
			*
			(<xsl:call-template name ="SURCHARGE"/>)
			*
			(<xsl:call-template name ="MULTIPOLICY"/>)
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
--> 
	<!-- New Calculation implemented by praveen singh  -->
  
	<xsl:variable name ="VAR1"> 
		    	 
			 	<xsl:call-template name ="TERRITORYBASE">
					<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>		
				</xsl:call-template>   
	</xsl:variable>

	<xsl:variable name ="VAR2"> <xsl:call-template name ="MOTORHOME"/></xsl:variable>
	
	<xsl:variable name ="VAR3"> 
		<xsl:call-template name ="CLASS">
				<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>
		</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name ="VAR4"> 
		<xsl:call-template name ="LIMIT">	
				<xsl:with-param name="FACTORELEMENT" select="'BI'"/>
		</xsl:call-template> 
	</xsl:variable>
	
	<xsl:variable name ="VAR5"> 
		<xsl:call-template name ="MULTIVEHICLE">
				<xsl:with-param name="FACTORELEMENT" select="'BI'"></xsl:with-param>			
		</xsl:call-template> 
	</xsl:variable>
	
	<xsl:variable name ="VAR6"> <xsl:call-template name ="VEHICLEUSE"/></xsl:variable>
	<xsl:variable name ="VAR7"> <xsl:call-template name ="DRIVERDISCOUNT"/> </xsl:variable>
	<xsl:variable name ="VAR8"> <xsl:call-template name ="GOODSTUDENT"/> </xsl:variable>
	<xsl:variable name ="VAR9"><xsl:call-template name ="INSURANCESCORE"/> </xsl:variable>
	<xsl:variable name ="VAR10"> <xsl:call-template name ="SURCHARGE"/> </xsl:variable>
	<xsl:variable name ="VAR11"> <xsl:call-template name ="MULTIPOLICY"/> </xsl:variable>
	 
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/BI ='NO COVERAGE'">
			0.00
		</xsl:when>
		<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL=''">
			 
			<!-- <xsl:value-of select ="round(round(round($VAR1*$VAR2)*$VAR3)*$VAR4)"/> -->
			 
			   <xsl:value-of select ="round(round(round(round(round(round(round(round(round(round(round($VAR1)*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)"/>  
		
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
 
	
</xsl:template>
<!-- End of BI Commercial Michigan -->



<!-- End of Bodily Injury -->


<!-- Property Damage -->
<xsl:template name="PD">
	<xsl:choose>	
		<!-- For Michigan -Personal Automobile -->
		<xsl:when test ="POLICY/STATENAME ='MICHIGAN'">
			<xsl:call-template name ="PD_PERSONAL_MICHIGAN"/>		
		</xsl:when>		
		<!-- For Indiana -Personal Automobile -->
		<xsl:when test ="POLICY/STATENAME ='INDIANA' ">
			<xsl:call-template name ="PD_PERSONAL_INDIANA"/>
		</xsl:when>			
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- Start of PD Personal Michigan -->
<xsl:template name ="PD_PERSONAL_MICHIGAN">
<!--
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SCO' or VEHICLES/VEHICLE/PD = 'NO COVERAGE'">
			0.00
		</xsl:when>
		<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL=''">
			(
			<xsl:call-template name ="TERRITORYBASE">
				<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
			</xsl:call-template> 
			)
			*
			(
			<xsl:call-template name ="INSURANCESCORE"/>
			)
			*
			(
			<xsl:call-template name ="OPTIMAL"/>
			)
			*
			(
			<xsl:call-template name ="TYPEOFVEHICLE">
				<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
			</xsl:call-template>
			)
			*
			(
			<xsl:call-template name ="CLASS">
				<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
			</xsl:call-template>
			)
			*
			(
			<xsl:call-template name ="LIMIT">
				<xsl:with-param name="FACTORELEMENT" select="'PD'"/>
			</xsl:call-template>
			)
			*
			(
			<xsl:call-template name ="MULTIVEHICLE">
				<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>		
			</xsl:call-template>
			)
			*
			(
			<xsl:call-template name ="VEHICLEUSE"/>
			)
			*
			(
			<xsl:call-template name ="DRIVERDISCOUNT"/>
			)
			*
			(
			<xsl:call-template name ="GOODSTUDENT"/>
			)
			*
			(
			<xsl:call-template name ="SURCHARGE"/>
			)
			*
			(
			<xsl:call-template name ="MULTIPOLICY"/>
			)
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	-->
	
	<!-- new calculation added by praveen singh --> 
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SCO' or VEHICLES/VEHICLE/PD = 'NO COVERAGE'">
			0.00
		</xsl:when>
		<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL=''">
			
			<xsl:variable name="VAR1"> 
			<xsl:call-template name ="TERRITORYBASE">
				<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
			</xsl:call-template> 
			</xsl:variable>
			
			<xsl:variable name="VAR2"> 
			<xsl:call-template name ="INSURANCESCORE"/>
			</xsl:variable>
			
			<xsl:variable name="VAR3"> 
			<xsl:call-template name ="OPTIMAL"/>
			</xsl:variable>
			
			<xsl:variable name="VAR4"> 
			<xsl:call-template name ="TYPEOFVEHICLE">
				<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
			</xsl:call-template>
			</xsl:variable>
			
			<xsl:variable name="VAR5"> 
			<xsl:call-template name ="CLASS">
				<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
			</xsl:call-template>
			</xsl:variable>
			
			<xsl:variable name="VAR6"> 
			<xsl:call-template name ="LIMIT">
				<xsl:with-param name="FACTORELEMENT" select="'PD'"/>
			</xsl:call-template>
			</xsl:variable>
			
			<xsl:variable name="VAR7"> 
			<xsl:call-template name ="MULTIVEHICLE">
				<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>		
			</xsl:call-template>
			</xsl:variable>
			
			<xsl:variable name="VAR8"> 
			<xsl:call-template name ="VEHICLEUSE"/>
			</xsl:variable>
			
			<xsl:variable name="VAR9"> 
			<xsl:call-template name ="DRIVERDISCOUNT"/>
			</xsl:variable>
			
			<xsl:variable name="VAR10"> 
			<xsl:call-template name ="GOODSTUDENT"/>
			</xsl:variable>
			
			<xsl:variable name="VAR11"> 
			<xsl:call-template name ="SURCHARGE"/>
			</xsl:variable>
			
			<xsl:variable name="VAR12"> 
			<xsl:call-template name ="MULTIPOLICY"/>
			</xsl:variable> 
			
			<xsl:value-of select ="round(round(round(round(round(round(round(round(round(round(round(round($VAR1)*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)"/>   
			
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	
		
	</xsl:choose>
</xsl:template>
<!-- End of Property Damage -Personal Michigan-->

<!-- Start of PD Commercial Michigan -->
<xsl:template name ="PD_PERSONAL_INDIANA">
<!--
 <xsl:choose>
	<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SCO' or VEHICLES/VEHICLE/PD = 'NO COVERAGE'">
		0.00
	</xsl:when>
	<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL=''">
		(
		<xsl:call-template name ="TERRITORYBASE">
			<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
		</xsl:call-template> 
		)
		*
		(
		<xsl:call-template name ="MOTORHOME"/>			
		)
		*
		(
		<xsl:call-template name ="CLASS">
			<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
		</xsl:call-template>
		)
		*
		(
		<xsl:call-template name ="LIMIT">
			<xsl:with-param name="FACTORELEMENT" select="'PD'"/>
		</xsl:call-template>
		)
		*
		(
		<xsl:call-template name ="MULTIVEHICLE">
			<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>		
		</xsl:call-template>
		)
		*
		(
		<xsl:call-template name ="VEHICLEUSE"/>
		)
		*
		(
		<xsl:call-template name ="DRIVERDISCOUNT"/>
		)
		*
		(
		<xsl:call-template name ="GOODSTUDENT"/>
		)
		*
		(
		<xsl:call-template name ="INSURANCESCORE"/>
		)
		*
		(
		<xsl:call-template name ="SURCHARGE"/>
		)
		*
		(
		<xsl:call-template name ="MULTIPOLICY"/>
		)
	</xsl:when>
	<xsl:otherwise>
		0.00
	</xsl:otherwise>
</xsl:choose>
-->

<xsl:choose>
	<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SCO' or VEHICLES/VEHICLE/PD = 'NO COVERAGE'">
			0.00
	</xsl:when>
	<xsl:when test="VEHICLES/VEHICLE/CSL = 0 or VEHICLES/VEHICLE/CSL=''">
		
		<xsl:variable name="VAR1">
			 <xsl:call-template name ="TERRITORYBASE">
				<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
			</xsl:call-template> 
		 </xsl:variable>
		
		<xsl:variable name="VAR2">
			 <xsl:call-template name ="MOTORHOME"/>	
		</xsl:variable>
		
		<xsl:variable name="VAR3"> 
			<xsl:call-template name ="CLASS">
				<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>
		
		<xsl:variable name="VAR4"> 
			<xsl:call-template name ="LIMIT">
				<xsl:with-param name="FACTORELEMENT" select="'PD'"/>
			</xsl:call-template>
		</xsl:variable>
		
		<xsl:variable name="VAR5"> 
			<xsl:call-template name ="MULTIVEHICLE">
				<xsl:with-param name="FACTORELEMENT" select="'PD'"></xsl:with-param>		
			</xsl:call-template>
		 </xsl:variable>
		
		<xsl:variable name="VAR6"> <xsl:call-template name ="VEHICLEUSE"/> </xsl:variable>
		<xsl:variable name="VAR7"> <xsl:call-template name ="DRIVERDISCOUNT"/>  </xsl:variable>
		<xsl:variable name="VAR8"> <xsl:call-template name ="GOODSTUDENT"/> </xsl:variable>
		<xsl:variable name="VAR9"> <xsl:call-template name ="INSURANCESCORE"/> </xsl:variable>
		<xsl:variable name="VAR10"> <xsl:call-template name ="SURCHARGE"/> </xsl:variable>
		<xsl:variable name="VAR11"> <xsl:call-template name ="MULTIPOLICY"/> </xsl:variable>
		
		<xsl:value-of select ="round(round(round(round(round(round(round(round(round(round(round($VAR1)*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)"/>  
		
	</xsl:when>
	<xsl:otherwise>
		0.00
	</xsl:otherwise>
</xsl:choose>




</xsl:template>




<!-- End of Property Damage - Commercial Michigan-->

<!-- END OF BI AND PD -->

<!-- PPI -->
<xsl:template name ="PPI">
<!--
	<xsl:choose>
	<xsl:when test="POLICY/STATENAME ='MICHIGAN' and VEHICLES/VEHICLE/VEHICLETYPE !='SCO'">
		(
		<xsl:call-template name ="TERRITORYBASE">
			<xsl:with-param name="FACTORELEMENT" select="'PPI'"></xsl:with-param>
		</xsl:call-template> 
		)
		*
		(
		<xsl:call-template name ="INSURANCESCORE"/>
		)
		*
		(
		<xsl:call-template name ="OPTIMAL"/>
		)
		*
		(
		<xsl:call-template name ="TYPEOFVEHICLE">
			<xsl:with-param name="FACTORELEMENT" select="'PPI'"></xsl:with-param>
		</xsl:call-template>
		)
		*
		(
		<xsl:call-template name ="CLASS">
			<xsl:with-param name="FACTORELEMENT" select="'PPI'"></xsl:with-param>
		</xsl:call-template>
		)
		*
		(
		<xsl:call-template name ="MULTIVEHICLE">
			<xsl:with-param name="FACTORELEMENT" select="'PPI'"></xsl:with-param>		
		</xsl:call-template>
		)
		*
		(
		<xsl:call-template name ="VEHICLEUSE"/>
		)
		*
		(
		<xsl:call-template name ="DRIVERDISCOUNT"/>
		)
		*
		(
		<xsl:call-template name ="GOODSTUDENT"/>
		)
		*
		(
		<xsl:call-template name ="SURCHARGE"/>
		)
		*
		(
		<xsl:call-template name ="MULTIPOLICY"/>
		)
	</xsl:when>
	<xsl:otherwise>
		0.00
	</xsl:otherwise>
	</xsl:choose>
	-->
	
	<xsl:choose>
	<xsl:when test="POLICY/STATENAME ='MICHIGAN' and VEHICLES/VEHICLE/VEHICLETYPE !='SCO'">
		<xsl:variable name="VAR1"> 
		<xsl:call-template name ="TERRITORYBASE">
			<xsl:with-param name="FACTORELEMENT" select="'PPI'"></xsl:with-param>
		</xsl:call-template> 
		</xsl:variable>
		
		<xsl:variable name="VAR2"> 
		<xsl:call-template name ="INSURANCESCORE"/>
		</xsl:variable>
		
		<xsl:variable name="VAR3">
		<xsl:call-template name ="OPTIMAL"/>
		</xsl:variable>
		
		<xsl:variable name="VAR4">
		<xsl:call-template name ="TYPEOFVEHICLE">
			<xsl:with-param name="FACTORELEMENT" select="'PPI'"></xsl:with-param>
		</xsl:call-template>
		</xsl:variable>
		
		<xsl:variable name="VAR5">
		<xsl:call-template name ="CLASS">
			<xsl:with-param name="FACTORELEMENT" select="'PPI'"></xsl:with-param>
		</xsl:call-template>
		</xsl:variable>
		
		<xsl:variable name="VAR6">
		<xsl:call-template name ="MULTIVEHICLE">
			<xsl:with-param name="FACTORELEMENT" select="'PPI'"></xsl:with-param>		
		</xsl:call-template>
		</xsl:variable>
		
		<xsl:variable name="VAR7">
		<xsl:call-template name ="VEHICLEUSE"/>
		</xsl:variable>
		
		<xsl:variable name="VAR8">
		<xsl:call-template name ="DRIVERDISCOUNT"/>
		</xsl:variable>
		
		<xsl:variable name="VAR9">
			<xsl:call-template name ="GOODSTUDENT"/>
		</xsl:variable>
		
		<xsl:variable name="VAR10">
		<xsl:call-template name ="SURCHARGE"/>
		</xsl:variable>
		
		<xsl:variable name="VAR11">
		<xsl:call-template name ="MULTIPOLICY"/>
		</xsl:variable>
		
		<xsl:value-of select ="round(round(round(round(round(round(round(round(round(round($VAR1+$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)"/>  
		
	</xsl:when>
	<xsl:otherwise>
		0.00
	</xsl:otherwise>
	</xsl:choose>
	
	
	
</xsl:template>
<!-- End of PPI -->

<!-- PIP -->
<xsl:template name ="PIP">
	<xsl:choose>
	<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE !='SCO' and POLICY/STATENAME ='MICHIGAN' and VEHICLES/VEHICLE/MEDPM != '' and VEHICLES/VEHICLE/MEDPM != '0' and VEHICLES/VEHICLE/MEDPM != 'NO COVERAGE'">
		<!--	(
			<xsl:call-template name ="TERRITORYBASE">
				<xsl:with-param name="FACTORELEMENT" select="'PIP'"></xsl:with-param>
			</xsl:call-template> 
			)
			*
			(
			<xsl:call-template name ="INSURANCESCORE"/>
			)
			*
			(
			<xsl:call-template name ="OPTIMAL"/>
			)
			*
			(
			<xsl:call-template name ="TYPEOFVEHICLE">
				<xsl:with-param name="FACTORELEMENT" select="'PIP'"></xsl:with-param>
			</xsl:call-template>
			)
			*
			(
			<xsl:call-template name ="CLASS">
				<xsl:with-param name="FACTORELEMENT" select="'PIP'"></xsl:with-param>
			</xsl:call-template>
			)
			*
			(
			<xsl:call-template name ="COVERAGETYPE"/>
			)
			*
			(
			<xsl:call-template name ="SEATBELT"/>
			)
			*
			(
			<xsl:call-template name ="AIRBAG"/>
			)
			*
			(
			<xsl:call-template name ="MULTIVEHICLE">
				<xsl:with-param name="FACTORELEMENT" select="'PIP'"></xsl:with-param>		
			</xsl:call-template>
			)
			*
			(
			<xsl:call-template name ="VEHICLEUSE"/>
			)
			*
			(
			<xsl:call-template name ="DRIVERDISCOUNT"/>
			)
			*
			(
			<xsl:call-template name ="GOODSTUDENT"/>
			)
			*
			(
			<xsl:call-template name ="SURCHARGE"/>
			)
			*
			(
			<xsl:call-template name ="MULTIPOLICY"/>
			)
		-->	
			<xsl:variable name="VAR1">
				<xsl:call-template name ="TERRITORYBASE">
					<xsl:with-param name="FACTORELEMENT" select="'PIP'"></xsl:with-param>
				</xsl:call-template> 
			</xsl:variable>
		
			<xsl:variable name="VAR2">
				<xsl:call-template name ="INSURANCESCORE"/>
			</xsl:variable>
			
			<xsl:variable name="VAR3">
				<xsl:call-template name ="OPTIMAL"/>
			</xsl:variable>
			
			
			<xsl:variable name="VAR4">
				<xsl:call-template name ="TYPEOFVEHICLE">
					<xsl:with-param name="FACTORELEMENT" select="'PIP'"></xsl:with-param>
				</xsl:call-template>
			</xsl:variable>
			
			<xsl:variable name="VAR5">
				<xsl:call-template name ="CLASS">
					<xsl:with-param name="FACTORELEMENT" select="'PIP'"></xsl:with-param>
				</xsl:call-template>
			</xsl:variable>
			
			<xsl:variable name="VAR6">
				<xsl:call-template name ="COVERAGETYPE"/>
			</xsl:variable>
			
			<xsl:variable name="VAR7">			
				<xsl:call-template name ="SEATBELT"/>
			</xsl:variable>
			
			<xsl:variable name="VAR8">
					<xsl:call-template name ="AIRBAG"/>
			</xsl:variable>
			
			<xsl:variable name="VAR9">			
				<xsl:call-template name ="MULTIVEHICLE">
					<xsl:with-param name="FACTORELEMENT" select="'PIP'"></xsl:with-param>		
				</xsl:call-template>
			</xsl:variable>
			
			<xsl:variable name="VAR10">
				<xsl:call-template name ="VEHICLEUSE"/>
			</xsl:variable>
			
			<xsl:variable name="VAR11">
				<xsl:call-template name ="DRIVERDISCOUNT"/>
			</xsl:variable>
			
			<xsl:variable name="VAR12">			
				<xsl:call-template name ="GOODSTUDENT"/>
			</xsl:variable>
			
			<xsl:variable name="VAR13">
				<xsl:call-template name ="SURCHARGE"/>
			</xsl:variable>
			
			<xsl:variable name="VAR14">			
				<xsl:call-template name ="MULTIPOLICY"/>
			</xsl:variable>
			
		 <xsl:value-of select="$VAR6+1"/>
			<!--	<xsl:value-of select="round(round(round(round(round(round(round(round(round(round(round(round(round(round($VAR1)*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)*$VAR13)*$VAR14)"/> -->
			 
			
			 
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- End of PIP -->

<!-- Medical  Payment -->
<xsl:template name ="MEDPAY">
<!--	<xsl:choose>
		<xsl:when test="POLICY/STATENAME = 'INDIANA' and VEHICLES/VEHICLE/MEDPM !='NO COVERAGE' and VEHICLES/VEHICLE/VEHICLETYPE !='SCO'">			
			(
				<xsl:call-template name ="TERRITORYBASE">
					<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'"></xsl:with-param>
				</xsl:call-template> 
			)
			*
			(
				<xsl:call-template name ="MOTORHOME" />
			)
			*
			(
				<xsl:call-template name ="CLASS">
					<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'"></xsl:with-param>
				</xsl:call-template>
			)
			* 
			(
				<xsl:call-template name ="LIMIT">
					<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'"/>
				</xsl:call-template>				
			)
			*
			(
			<xsl:call-template name ="AIRBAG"/>
			) 
			*
			(
			<xsl:call-template name ="MULTIVEHICLE">
				<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'"></xsl:with-param>		
			</xsl:call-template>
			) 
			*
			(
			<xsl:call-template name ="VEHICLEUSE"/>
			)
			*
			(
			<xsl:call-template name ="DRIVERDISCOUNT"/>
			)
			*
			(
			<xsl:call-template name ="GOODSTUDENT"/>
			)
			*
			(
			<xsl:call-template name ="INSURANCESCORE"/>
			)
			*
			(
			<xsl:call-template name ="SURCHARGE"/>
			)
			*
			(
			<xsl:call-template name ="MULTIPOLICY"/>
			)
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
-->	
	
		<xsl:choose> 
		<xsl:when test="POLICY/STATENAME = 'INDIANA' and VEHICLES/VEHICLE/MEDPM !='NO COVERAGE' and VEHICLES/VEHICLE/VEHICLETYPE !='SCO'">			
			<xsl:variable name="VAR1"> 
				<xsl:call-template name ="TERRITORYBASE">
					<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'"></xsl:with-param>
				</xsl:call-template> 
			 </xsl:variable>
			 
			 <xsl:variable name="VAR2">
				<xsl:call-template name ="MOTORHOME" />
			 </xsl:variable>
			 
			 <xsl:variable name="VAR3">
				<xsl:call-template name ="CLASS">
					<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'"></xsl:with-param>
				</xsl:call-template>
			</xsl:variable>
			
			<xsl:variable name="VAR4">
				<xsl:call-template name ="LIMIT">
					<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'"/>
				</xsl:call-template>				
			</xsl:variable> 
			
			
			<xsl:variable name="VAR5"> 
			<xsl:call-template name ="AIRBAG"/>
			</xsl:variable>
			
			<xsl:variable name="VAR6"> 
			<xsl:call-template name ="MULTIVEHICLE">
				<xsl:with-param name="FACTORELEMENT" select="'MEDPAY'"></xsl:with-param>		
			</xsl:call-template>
			</xsl:variable> 
			
			<xsl:variable name="VAR7"> 
				<xsl:call-template name ="VEHICLEUSE"/>
			</xsl:variable>
			
			<xsl:variable name="VAR8"> 
				<xsl:call-template name ="DRIVERDISCOUNT"/>
			</xsl:variable>
			
			<xsl:variable name="VAR9"> 
				<xsl:call-template name ="GOODSTUDENT"/>
			</xsl:variable>
			
			<xsl:variable name="VAR10"> 
				<xsl:call-template name ="INSURANCESCORE"/>
			</xsl:variable>
			
			<xsl:variable name="VAR11"> 
				<xsl:call-template name ="SURCHARGE"/>
			</xsl:variable>
			
			<xsl:variable name="VAR12"> 
				<xsl:call-template name ="MULTIPOLICY"/>
			</xsl:variable>
		
		<xsl:value-of select ="round(round(round(round(round(round(round(round(round(round(round($VAR1+$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)"/>  
		
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
	
	
</xsl:template>

<xsl:template name="MOTORHOME">
	1.00
</xsl:template>


<!-- Uninsured Motorists -->
<xsl:template name="UM">
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SCO' or VEHICLES/VEHICLE/TYPE ='NO COVERAGE' or VEHICLES/VEHICLE/TYPE ='' or VEHICLES/VEHICLE/TYPE ='0'">
			0.00
		</xsl:when>
		<xsl:otherwise>
			<xsl:choose>	
				<!-- For Michigan -Personal Automobile -->
				<xsl:when test ="POLICY/STATENAME ='MICHIGAN'">
					<xsl:call-template name ="UM_PERSONAL_MICHIGAN"/>		
				</xsl:when>		
				<!-- For Indiana -Personal Automobile -->
				<xsl:when test ="POLICY/STATENAME ='INDIANA'">
					<xsl:call-template name ="UM_PERSONAL_INDIANA"/>
				</xsl:when>
				
				<xsl:otherwise>0.00</xsl:otherwise>
			</xsl:choose>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<xsl:template name ="UM_PERSONAL_MICHIGAN">
<!--	(
	<xsl:call-template name ="TERRITORYBASE">
		<xsl:with-param name="FACTORELEMENT" select="'UM'"></xsl:with-param>
	</xsl:call-template> 
	)
	*
	(	
			 
			<xsl:choose>		 
				<xsl:when test="VEHICLES/VEHICLE/TYPE ='NO COVERAGE' or VEHICLES/VEHICLE/TYPE =''">  No Coverage 
					0.00
				</xsl:when>
				<xsl:when test="(VEHICLES/VEHICLE/UMSPLIT !='' and VEHICLES/VEHICLE/UMSPLIT !='0/0' and VEHICLES/VEHICLE/UMSPLIT !='NO COVERAGE') ">   Split Limit 			
					(	
						<xsl:call-template name ="LIMIT"> 
							<xsl:with-param name="FACTORELEMENT" select="'UM_MICHIGAN_SPLIT'"/>
						</xsl:call-template>
					)
				</xsl:when>
				<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL !='0' and VEHICLES/VEHICLE/UMCSL !='NO COVERAGE' ">   Split Limit  			
					(	
						<xsl:call-template name ="LIMIT"> 
							<xsl:with-param name="FACTORELEMENT" select="'UM_MICHIGAN_CSL'"/>
						</xsl:call-template>
					)
				</xsl:when>
				<xsl:otherwise>
					0.00
				</xsl:otherwise>
			</xsl:choose>
	)-->
	
	
	<xsl:variable name="VAR1"> 
	<xsl:call-template name ="TERRITORYBASE">
		<xsl:with-param name="FACTORELEMENT" select="'UM'"></xsl:with-param>
	</xsl:call-template> 
	</xsl:variable>
	
		<xsl:variable name="VAR2"> 	 
			<xsl:choose>		 
				<xsl:when test="VEHICLES/VEHICLE/TYPE ='NO COVERAGE' or VEHICLES/VEHICLE/TYPE =''">  
					0.00
				</xsl:when>
				<xsl:when test="(VEHICLES/VEHICLE/UMSPLIT !='' and VEHICLES/VEHICLE/UMSPLIT !='0/0' and VEHICLES/VEHICLE/UMSPLIT !='NO COVERAGE') ">    			
					 
						<xsl:call-template name ="LIMIT"> 
							<xsl:with-param name="FACTORELEMENT" select="'UM_MICHIGAN_SPLIT'"/>
						</xsl:call-template>
					 
				</xsl:when>
				<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL !='0' and VEHICLES/VEHICLE/UMCSL !='NO COVERAGE' ">     			
					 	
						<xsl:call-template name ="LIMIT"> 
							<xsl:with-param name="FACTORELEMENT" select="'UM_MICHIGAN_CSL'"/>
						</xsl:call-template>
					 
				</xsl:when>
				<xsl:otherwise>
					0.00
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
			
	 <xsl:value-of select="round($VAR1*$VAR2)"/>
	
	
	<!--*	THIS IS TAKEN CARE OF IN THE ABOVE TWO FACTORS (commented by srikant)
	(
		<xsl:call-template name ="MULTIVEHICLE"> 
			<xsl:with-param name="FACTORELEMENT" select="'UM'"></xsl:with-param>
		</xsl:call-template> 
	)-->
</xsl:template>


<xsl:template name ="UM_PERSONAL_INDIANA">
	<!-- Check if  it is 
			1. No coverage
			2. BI Only
			3. BI and PD -->
	<!--
	(
	<xsl:choose>		 
		<xsl:when test="VEHICLES/VEHICLE/TYPE ='NO COVERAGE' or VEHICLES/VEHICLE/TYPE =''">  No coverage
			0.00
		</xsl:when>
		<xsl:when test="(VEHICLES/VEHICLE/UMSPLIT !='' and VEHICLES/VEHICLE/UMSPLIT !='0/0' and VEHICLES/VEHICLE/UMSPLIT !='NO COVERAGE') ">   Split Limit  	
			(	
				<xsl:call-template name ="LIMIT"> 
					<xsl:with-param name="FACTORELEMENT" select="'UM_INDIANA_SPLIT'"/>
				</xsl:call-template>
			)
		</xsl:when>
		<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL !='0' and VEHICLES/VEHICLE/UMCSL !='NO COVERAGE' ">   Split Limit  			
			(	
				<xsl:call-template name ="LIMIT"> 
					<xsl:with-param name="FACTORELEMENT" select="'UM_INDIANA_CSL'"/>
				</xsl:call-template>
			)
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
	)
	+
	(
		<xsl:call-template name ="UMPD"/> 
	)
	-->
	
	
	<xsl:variable name="VAR1"> 
		<xsl:choose>		 
			<xsl:when test="VEHICLES/VEHICLE/TYPE ='NO COVERAGE' or VEHICLES/VEHICLE/TYPE =''">  No coverage
				0.00
			</xsl:when>
			<xsl:when test="(VEHICLES/VEHICLE/UMSPLIT !='' and VEHICLES/VEHICLE/UMSPLIT !='0/0' and VEHICLES/VEHICLE/UMSPLIT !='NO COVERAGE') ">   <!-- Split Limit  	-->
					<xsl:call-template name ="LIMIT"> 
						<xsl:with-param name="FACTORELEMENT" select="'UM_INDIANA_SPLIT'"/>
					</xsl:call-template>
				
			</xsl:when>
			<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL !='0' and VEHICLES/VEHICLE/UMCSL !='NO COVERAGE' ">   <!-- Split Limit --> 			
					
					<xsl:call-template name ="LIMIT"> 
						<xsl:with-param name="FACTORELEMENT" select="'UM_INDIANA_CSL'"/>
					</xsl:call-template>
				
			</xsl:when>
			<xsl:otherwise>
				0.00
			</xsl:otherwise>
		</xsl:choose>
	</xsl:variable>
	
	
	<xsl:variable name="VAR2"> 
		<xsl:call-template name ="UMPD"/> 
	</xsl:variable>
	
	
<xsl:value-of select="round($VAR1+$VAR2)"/> 
	
</xsl:template>


<!--Start of UM Property Damage -->
<xsl:template name="UMPD">
	<xsl:choose>
		<xsl:when test="POLICY/STATENAME ='INDIANA' and VEHICLES/VEHICLE/VEHICLETYPE !='SCO'">
			<xsl:choose>		 
				<xsl:when test="VEHICLES/VEHICLE/TYPE ='NO COVERAGE' or VEHICLES/VEHICLE/TYPE =''"> <!-- No Coverage -->
					0.00
				</xsl:when>				
				<xsl:when test="(VEHICLES/VEHICLE/TYPE != ''and VEHICLES/VEHICLE/TYPE !='BI ONLY' and VEHICLES/VEHICLE/PDLIMIT &gt; 0)"> <!-- UMPD -->			
					 
						<xsl:variable name="PPDDEDUCTIBLE" select="VEHICLES/VEHICLE/PDDEDUCTIBLE" />
						<xsl:variable name="PPDLIMIT" select="VEHICLES/VEHICLE/PDLIMIT" />
						<xsl:choose>
							<xsl:when test="$PPDDEDUCTIBLE = 0">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMPDLIMITS']/NODE[@ID='UMPD']/ATTRIBUTES[@LIMIT =$PPDLIMIT]/@DEDUCTIBLE_0" />
							</xsl:when>
							<xsl:when test="$PPDDEDUCTIBLE = 300">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMPDLIMITS']/NODE[@ID='UMPD']/ATTRIBUTES[@LIMIT =$PPDLIMIT]/@DEDUCTIBLE_300" />
							</xsl:when>
							<xsl:otherwise>0.00</xsl:otherwise>
						</xsl:choose>
					 
				</xsl:when>
				<xsl:otherwise>
					0.00
				</xsl:otherwise>				
			</xsl:choose>	
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- End of Uninsured Motorists-->

<!-- Underinsured Motorists -->
<xsl:template name ="UIM">
 <!--	 
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SCO'">
			0.00
		</xsl:when>
		<xsl:otherwise>
			(
			<xsl:call-template name ="TERRITORYBASE">
				<xsl:with-param name="FACTORELEMENT" select="'UIM'"></xsl:with-param>
			</xsl:call-template> 
			)
			*
			(	
			<xsl:call-template name ="LIMIT"> 
				<xsl:with-param name="FACTORELEMENT" select="'UIM'"/>
			</xsl:call-template>
			)
		</xsl:otherwise>
	</xsl:choose>
-->
 
<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SCO'">
			0.00
		</xsl:when>
		<xsl:otherwise>
			
			<xsl:variable name="VAR1"> 
				<xsl:call-template name ="TERRITORYBASE">
					<xsl:with-param name="FACTORELEMENT" select="'UIM'"></xsl:with-param>
				</xsl:call-template> 
			</xsl:variable>
			
			<xsl:variable name="VAR2"> 		
				<xsl:call-template name ="LIMIT"> 
					<xsl:with-param name="FACTORELEMENT" select="'UIM'"/>
				</xsl:call-template>
			</xsl:variable>
			
	 	 <xsl:value-of select="$VAR2"/>   <!--Error point -->
				  
			 
			
		</xsl:otherwise>
	</xsl:choose>
 
	

</xsl:template>
<!-- End of Underinsured Motorists  -->


<!-- Comprehensive -->
<xsl:template name="COMP">
	<xsl:choose>	
		<!-- For Michigan -Personal Automobile -->
		<xsl:when test ="POLICY/STATENAME ='MICHIGAN'">
			<xsl:call-template name ="COMP_PERSONAL_MICHIGAN"/>		
		</xsl:when>		
		<!-- For Indiana -Personal Automobile -->
		<xsl:when test ="POLICY/STATENAME ='INDIANA'">
			<xsl:call-template name ="COMP_PERSONAL_INDIANA"/>
		</xsl:when>		
		<xsl:otherwise>0.00</xsl:otherwise>	
	</xsl:choose>
</xsl:template>

<xsl:template name ="COMP_PERSONAL_MICHIGAN"> <!--  i(srikant) was here. this is not gnvg proper result-->
<!--
	(
	<xsl:call-template name ="TERRITORYBASE">
		<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
	</xsl:call-template> 
	)
	*
	(
	<xsl:call-template name ="INSURANCESCORE"/>
	)
	*
	(
	<xsl:call-template name ="TYPEOFVEHICLE">
		<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
	</xsl:call-template>
	)
	*
	(
	<xsl:call-template name ="CLASS">
		<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
	</xsl:call-template>
	)
	*
	(
	<xsl:call-template name ="SYMBOL">
		<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
	</xsl:call-template>
	)
	*
	(
	<xsl:call-template name ="DEDUCTIBLE">
		<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>	
	</xsl:call-template>
	)
	*
	(
	<xsl:call-template name ="AGE">
		<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>	
	</xsl:call-template>
	)
	*
	(
	<xsl:call-template name ="MULTIPOLICY"/>
	)
	-->
	<!-- TO DO : 'OR' PART IN THE FORMULA -->
	<xsl:variable name="VAR1"> 
		<xsl:call-template name ="TERRITORYBASE">
			<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
		</xsl:call-template> 
	</xsl:variable>
	
	<xsl:variable name="VAR2"> 
		<xsl:call-template name ="INSURANCESCORE"/>
	</xsl:variable>
	
	<xsl:variable name="VAR3">
		<xsl:call-template name ="TYPEOFVEHICLE">
			<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
		</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR4">
		<xsl:call-template name ="CLASS">
			<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
		</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR5">
		<xsl:call-template name ="SYMBOL">
			<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
		</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR6">
		<xsl:call-template name ="DEDUCTIBLE">
			<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>	
		</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR7">
		<xsl:call-template name ="AGE">
			<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>	
		</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR8">
		<xsl:call-template name ="MULTIPOLICY"/>
	</xsl:variable>
	
	<xsl:value-of select ="round(round(round(round(round(round(round(round($VAR1)*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)"/>   
	
	
	
</xsl:template>

<xsl:template name ="COMP_PERSONAL_INDIANA">
 <!--
	(
	<xsl:call-template name ="TERRITORYBASE">
		<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
	</xsl:call-template> 
	)
	*
	(
	<xsl:call-template name ="MOTORHOME"/>
	)	
	*
	(
	<xsl:call-template name ="CLASS">
		<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
	</xsl:call-template>
	)	
	*
	(
	<xsl:call-template name ="SYMBOL">
		<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
	</xsl:call-template>
	)
	*
	(
	<xsl:call-template name ="DEDUCTIBLE">
		<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>	
	</xsl:call-template>
	)
	*
	(
	<xsl:call-template name ="AGE">
		<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>	
	</xsl:call-template>
	)
	*
	(
	<xsl:call-template name ="MULTIVEHICLE">
		<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>		
	</xsl:call-template>
	)	 
	*
	(
	<xsl:call-template name ="VEHICLEUSE"/>
	)
	*
	(
	<xsl:call-template name ="DRIVERDISCOUNT"/>
	)
	*
	(
	<xsl:call-template name ="GOODSTUDENT"/>
	)
	*
	(
	<xsl:call-template name ="INSURANCESCORE"/>
	)
	*
	(
	<xsl:call-template name ="SURCHARGE"/>
	)
	*
	(
	<xsl:call-template name ="MULTIPOLICY"/>
	)
-->	
	<!-- new calculation added by praveen singh -->
 	
	<xsl:variable name="VAR1"> 
		<xsl:call-template name ="TERRITORYBASE">
			<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
		</xsl:call-template> 
	</xsl:variable>
	
	<xsl:variable name="VAR2"> 
		<xsl:call-template name ="MOTORHOME"/>
	</xsl:variable>
	
	<xsl:variable name="VAR3"> 
		<xsl:call-template name ="CLASS">
			<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
		</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR4"> 
		<xsl:call-template name ="SYMBOL">
			<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>
		</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR5"> 
		<xsl:call-template name ="DEDUCTIBLE">
			<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>	
		</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR6"> 
		<xsl:call-template name ="AGE">
			<xsl:with-param name="FACTORELEMENT" select="'COMP'"></xsl:with-param>	
		</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR7"> 
		<xsl:call-template name ="MULTIVEHICLE">
			<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>		
		</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR8"> 
		<xsl:call-template name ="VEHICLEUSE"/>
	</xsl:variable>
	
	<xsl:variable name="VAR9"> 
		<xsl:call-template name ="DRIVERDISCOUNT"/>
	</xsl:variable>
	
	<xsl:variable name="VAR10"> 
		<xsl:call-template name ="GOODSTUDENT"/>
	</xsl:variable>
	
	<xsl:variable name="VAR11"> 
		<xsl:call-template name ="INSURANCESCORE"/>
	</xsl:variable>
	
	<xsl:variable name="VAR12"> 
		<xsl:call-template name ="SURCHARGE"/>
	</xsl:variable>
	
	<xsl:variable name="VAR13"> 
		<xsl:call-template name ="MULTIPOLICY"/>
	</xsl:variable>
	
  
 <xsl:value-of select ="round(round(round(round(round(round(round(round(round(round(round(round(round($VAR1)*$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)*$VAR13)"/>   
  
 
 	 
</xsl:template>
<!-- End of Comprehensive  -->

<!-- Collision -->
<xsl:template name="COLL">
	<xsl:choose>	
		<!-- For Michigan -Personal Automobile -->
		<xsl:when test ="POLICY/STATENAME ='MICHIGAN' and VEHICLES/VEHICLE/VEHICLETYPE !='SCO'">
			<xsl:call-template name ="COLL_PERSONAL_MICHIGAN"/>		
		</xsl:when>		
		<!-- For Indiana -Personal Automobile -->
		<xsl:when test ="POLICY/STATENAME ='INDIANA' and VEHICLES/VEHICLE/VEHICLETYPE !='SCO'">
			<xsl:call-template name ="COLL_PERSONAL_INDIANA"/>
		</xsl:when>		
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- Start Michigan -Personal Automobile -->
<xsl:template name ="COLL_PERSONAL_MICHIGAN">
<!--
	(
		<xsl:call-template name ="TERRITORYBASE">
			<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
		</xsl:call-template> 
	)
	*
	(
	<xsl:call-template name ="INSURANCESCORE"/>
	)
	*
	(
	<xsl:call-template name ="OPTIMAL"/>
	)
	*
	(
	<xsl:call-template name ="TYPEOFVEHICLE">
		<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
	</xsl:call-template>
	)
	*
	(
	<xsl:call-template name ="CLASS">
		<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
	</xsl:call-template>
	)
	*
	(
	<xsl:call-template name ="SYMBOL">
		<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>	
	</xsl:call-template>
	)
	*
	(
	<xsl:call-template name ="DEDUCTIBLE">
		<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>	
	</xsl:call-template>
	)
	*
	(
	<xsl:call-template name ="AGE">
		<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>	
	</xsl:call-template>
	)
	*
	(
	<xsl:call-template name ="ABS"/>
	)
	*
	(
	<xsl:call-template name ="MULTIVEHICLE">
		<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>		
	</xsl:call-template>
	)
	*
	(
	<xsl:call-template name ="VEHICLEUSE"/>
	)
	*
	(
	<xsl:call-template name ="DRIVERDISCOUNT"/>
	)
	*
	(
	<xsl:call-template name ="GOODSTUDENT"/>
	)
	*
	(
	<xsl:call-template name ="SURCHARGE"/>
	)
	*
	(
	<xsl:call-template name ="MULTIPOLICY"/>
	)
	-->
	<!-- TO DO : OR PART IN THE FORMULA -->
	<xsl:variable name="VAR1"> 
		<xsl:call-template name ="TERRITORYBASE">
			<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
		</xsl:call-template> 
	</xsl:variable>
	
	<xsl:variable name="VAR2"> 
		<xsl:call-template name ="INSURANCESCORE"/>
	</xsl:variable>
	
	<xsl:variable name="VAR3"> 
		<xsl:call-template name ="OPTIMAL"/>
	</xsl:variable>
	
	<xsl:variable name="VAR4"> 
		<xsl:call-template name ="TYPEOFVEHICLE">
			<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
		</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR5"> 
		<xsl:call-template name ="CLASS">
			<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
		</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR6"> 
		<xsl:call-template name ="SYMBOL">
			<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>	
		</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR7"> 
		<xsl:call-template name ="DEDUCTIBLE">
			<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>	
		</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR8"> 
		<xsl:call-template name ="AGE">
			<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>	
		</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR9"> 
		<xsl:call-template name ="ABS"/>
	</xsl:variable>
	
	<xsl:variable name="VAR10"> 
		<xsl:call-template name ="MULTIVEHICLE">
			<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>		
		</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR11"> 
		<xsl:call-template name ="VEHICLEUSE"/>
	</xsl:variable>
	
	<xsl:variable name="VAR12"> 
		<xsl:call-template name ="DRIVERDISCOUNT"/>
	</xsl:variable>
	
	<xsl:variable name="VAR13"> 
		<xsl:call-template name ="GOODSTUDENT"/>
	</xsl:variable>
	
	<xsl:variable name="VAR14"> 
		<xsl:call-template name ="SURCHARGE"/>
	</xsl:variable>
	
	<xsl:variable name="VAR15"> 
		<xsl:call-template name ="MULTIPOLICY"/>
	</xsl:variable>
	
	<xsl:value-of select ="round(round(round(round(round(round(round(round(round(round(round(round(round(round($VAR1+$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)*$VAR13)*$VAR14)*$VAR15)"/>   
	<!--
	<xsl:value-of select ="4"/> 
	-->
	
	
	
	
</xsl:template>
<!-- End Michigan -Personal Automobile -->

<!-- Start Indiana -Personal Automobile -->
<xsl:template name ="COLL_PERSONAL_INDIANA">
<!--
	(
		<xsl:call-template name ="TERRITORYBASE">
			<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
		</xsl:call-template> 
	)	
	*
	(
	<xsl:call-template name ="MOTORHOME"></xsl:call-template>
	)
	*
	(
	<xsl:call-template name ="CLASS">
		<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
	</xsl:call-template>
	)
	*
	(
	<xsl:call-template name ="SYMBOL">
		<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>	
	</xsl:call-template>
	)
	*
	(
	<xsl:call-template name ="DEDUCTIBLE">
		<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>	
	</xsl:call-template>
	)
	*
	(
	<xsl:call-template name ="AGE">
		<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>	
	</xsl:call-template>
	)	
	*
	(
	<xsl:call-template name ="ABS"/>
	)
	*
	(
	<xsl:call-template name ="MULTIVEHICLE">
		<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>		
	</xsl:call-template>
	)
	*	
	(
	<xsl:call-template name ="VEHICLEUSE"/>
	)
	*
	(
	<xsl:call-template name ="DRIVERDISCOUNT"/>
	)
	*
	(
	<xsl:call-template name ="GOODSTUDENT"/>
	)
	*
	(
	<xsl:call-template name ="INSURANCESCORE"/>
	)
	*
	(
	<xsl:call-template name ="SURCHARGE"/>
	)
	*
	(
	<xsl:call-template name ="MULTIPOLICY"/>
	)
-->
   <xsl:variable name="VAR1"> 
		<xsl:call-template name ="TERRITORYBASE">
			<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
		</xsl:call-template> 
	</xsl:variable>
	
	<xsl:variable name="VAR2"> 
		<xsl:call-template name ="MOTORHOME"></xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR3"> 
		<xsl:call-template name ="CLASS">
			<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>
		</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR4"> 
	<xsl:call-template name ="SYMBOL">
		<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>	
	</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR5"> 
		<xsl:call-template name ="DEDUCTIBLE">
			<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>	
		</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR6"> 
		<xsl:call-template name ="AGE">
			<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>	
		</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR7"> 
		<xsl:call-template name ="ABS"/>
	</xsl:variable>
	
	<xsl:variable name="VAR8"> 
		<xsl:call-template name ="MULTIVEHICLE">
			<xsl:with-param name="FACTORELEMENT" select="'COLL'"></xsl:with-param>		
		</xsl:call-template>
	</xsl:variable>
	
	<xsl:variable name="VAR9"> 
		<xsl:call-template name ="VEHICLEUSE"/>
	</xsl:variable>
	
	<xsl:variable name="VAR10"> 
		<xsl:call-template name ="DRIVERDISCOUNT"/>
	</xsl:variable>
	
	<xsl:variable name="VAR11"> 
		<xsl:call-template name ="GOODSTUDENT"/>
	</xsl:variable>
	
	<xsl:variable name="VAR12"> 
		<xsl:call-template name ="INSURANCESCORE"/>
	</xsl:variable>
	
	<xsl:variable name="VAR13"> 
		<xsl:call-template name ="SURCHARGE"/>
	</xsl:variable>
	
	<xsl:variable name="VAR14"> 
		<xsl:call-template name ="MULTIPOLICY"/>
	</xsl:variable>
	
	<xsl:value-of select ="round(round(round(round(round(round(round(round(round(round(round(round(round($VAR1+$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)*$VAR13)*$VAR14)"/>   
	
</xsl:template>
<!-- End Indiana - Personal Automobile -->

<!-- End of Collision  -->

<!-- Limited Collision -->
<xsl:template name ="LTDCOLL">
<!--
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE !='SCO'">
			(
				<xsl:call-template name ="TERRITORYBASE">
					<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>
				</xsl:call-template> 
			)
			*
			(
			<xsl:call-template name ="INSURANCESCORE"/>
			)
			*
			(
			<xsl:call-template name ="OPTIMAL"/>
			)
			*
			(
			<xsl:call-template name ="TYPEOFVEHICLE">
				<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>
			</xsl:call-template>
			)
			*
			(
			<xsl:call-template name ="CLASS">
				<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>
			</xsl:call-template>
			)
			*
			(
			<xsl:call-template name ="SYMBOL">
				<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>	
			</xsl:call-template>
			)
			*
			(
			<xsl:call-template name ="LIMITFACTOR"/>
			)
			*
			(
			<xsl:call-template name ="AGE">
				<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>	
			</xsl:call-template>
			)
			*
			(
			<xsl:call-template name ="ABS"/>
			)
			*
			(
			<xsl:call-template name ="MULTIVEHICLE">
				<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>		
			</xsl:call-template>
			)
			*
			(
			<xsl:call-template name ="VEHICLEUSE"/>
			)
			*
			(
			<xsl:call-template name ="DRIVERDISCOUNT"/>
			)
			*
			(
			<xsl:call-template name ="GOODSTUDENT"/>
			)	
			*
			(
			<xsl:call-template name ="SURCHARGE"/>
			)
			*
			(
			<xsl:call-template name ="MULTIPOLICY"/>
			)
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>

	</xsl:choose>
-->

<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE !='SCO'">
			<xsl:variable name="VAR1"> 
				<xsl:call-template name ="TERRITORYBASE">
					<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>
				</xsl:call-template> 
			</xsl:variable>
			
			<xsl:variable name="VAR2">
				<xsl:call-template name ="INSURANCESCORE"/>
			</xsl:variable>
			
			<xsl:variable name="VAR3">
			<xsl:call-template name ="OPTIMAL"/>
			</xsl:variable>
			
			<xsl:variable name="VAR4">
			<xsl:call-template name ="TYPEOFVEHICLE">
				<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>
			</xsl:call-template>
			</xsl:variable>
			
			<xsl:variable name="VAR5">
			<xsl:call-template name ="CLASS">
				<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>
			</xsl:call-template>
			</xsl:variable>
			
			<xsl:variable name="VAR6">
			<xsl:call-template name ="SYMBOL">
				<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>	
			</xsl:call-template>
			</xsl:variable>
			
			<xsl:variable name="VAR7">
			<xsl:call-template name ="LIMITFACTOR"/>
			</xsl:variable>
			
			<xsl:variable name="VAR8">
			<xsl:call-template name ="AGE">
				<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>	
			</xsl:call-template>
			</xsl:variable>
			
			<xsl:variable name="VAR9">
				<xsl:call-template name ="ABS"/>
			</xsl:variable>
			
			<xsl:variable name="VAR10">
				<xsl:call-template name ="MULTIVEHICLE">
					<xsl:with-param name="FACTORELEMENT" select="'LTDCOLL'"></xsl:with-param>		
				</xsl:call-template>
			</xsl:variable>
			
			<xsl:variable name="VAR11">
				<xsl:call-template name ="VEHICLEUSE"/>
			</xsl:variable>
			
			<xsl:variable name="VAR12">
				<xsl:call-template name ="DRIVERDISCOUNT"/>
			</xsl:variable>
			
			<xsl:variable name="VAR13">
			<xsl:call-template name ="GOODSTUDENT"/>
			</xsl:variable>
			
			<xsl:variable name="VAR14">
			<xsl:call-template name ="SURCHARGE"/>
			</xsl:variable>
			
			<xsl:variable name="VAR15">
				<xsl:call-template name ="MULTIPOLICY"/>
			</xsl:variable>

			<xsl:value-of select ="round(round(round(round(round(round(round(round(round(round(round(round(round(round($VAR1+$VAR2)*$VAR3)*$VAR4)*$VAR5)*$VAR6)*$VAR7)*$VAR8)*$VAR9)*$VAR10)*$VAR11)*$VAR12)*$VAR13)*$VAR14)*$VAR15)"/>  
			
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>

	</xsl:choose>

	
</xsl:template>
<!-- End of Limited Collision -->



<!-- Mini-Tort -->
<xsl:template name ="MINITORT">
	<xsl:choose>
		<xsl:when test="POLICY/STATENAME='MICHIGAN' and VEHICLES/VEHICLE/MINITORTPDLIAB = 'TRUE' and VEHICLES/VEHICLE/VEHICLETYPE !='SCO'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINITORT']/NODE[@ID='MINITORTRATE']/ATTRIBUTES/@RATE_PER_VEHICLE"/>
		</xsl:when>
		<xsl:otherwise>
			0	
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<xsl:template name ="MINITORTLIMIT">
	<xsl:choose>
		<xsl:when test="POLICY/STATENAME='MICHIGAN' and VEHICLES/VEHICLE/MINITORTPDLIAB = 'TRUE' and VEHICLES/VEHICLE/VEHICLETYPE !='SCO'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MINITORT']/NODE[@ID='MINITORTRATE']/ATTRIBUTES/@LIMIT"/>
		</xsl:when>
		<xsl:otherwise>
			0.00		
		</xsl:otherwise>
	</xsl:choose>
</xsl:template> 
<!-- End of Mini-Tort-->

<!-- Road Services-->
<xsl:template name ="ROADSERVICES">	
	<xsl:variable name="PROADSERVICE" select="VEHICLES/VEHICLE/ROADSERVICE"/>
	<xsl:choose>
		<xsl:when test="$PROADSERVICE = '' or $PROADSERVICE = 0 or VEHICLES/VEHICLE/VEHICLETYPE ='SCO'">
			0
		</xsl:when>
		<xsl:otherwise>					
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='ROADSERVICELIMIT']/ATTRIBUTES[@COVERAGE = $PROADSERVICE]/@RATE"/>				
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- End of Road Services-->

<!-- Rental Reimbursement -->
<xsl:template name ="RENTALREIMBURSEMENT">
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SCO' or VEHICLES/VEHICLE/RENTALREIMBURSEMENT ='' or VEHICLES/VEHICLE/RENTALREIMBURSEMENT ='0'" >
			0 
		</xsl:when>
		<xsl:otherwise>
			<xsl:choose>
				<xsl:when test="VEHICLES/VEHICLE/RENTALREIMMAXCOVG ='' or VEHICLES/VEHICLE/RENTALREIMMAXCOVG ='0'">
					0 
				</xsl:when> 
				<xsl:otherwise>
					<xsl:variable name="PRENTALREIMLIMITDAY" select ="VEHICLES/VEHICLE/RENTALREIMLIMITDAY"></xsl:variable> 
					<xsl:variable name="PRENTALREIMMAXCOVG" select ="VEHICLES/VEHICLE/RENTALREIMMAXCOVG"></xsl:variable>			
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='RENTALREIMBURSEMENT']/ATTRIBUTES[@DAYS = $PRENTALREIMLIMITDAY and @MAXCOVERAGE = $PRENTALREIMMAXCOVG]/@RATE"/>
				</xsl:otherwise>
			</xsl:choose>	
		</xsl:otherwise>
	</xsl:choose>
	
</xsl:template>
<!-- End of Rental Reimbursement-->

<!-- MCCA Fee-->
<xsl:template name ="MCCAFEE">
	<xsl:choose>
		<xsl:when test="POLICY/STATENAME ='MICHIGAN' and VEHICLES/VEHICLE/VEHICLETYPE !='SCO'">
			<!--xsl:choose>
				<xsl:when test="VEHICLES/VEHICLE/CC ='' or VEHICLES/VEHICLE/CC &lt; 50">
					0.00
				</xsl:when>
				<xsl:when test="VEHICLES/VEHICLE/CC &gt; 50"-->
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MCCAFEE']/NODE[@ID='MCCAFEESEMIANNUAL']/ATTRIBUTES/@RATE_PER_VEHICLE"/>
				<!--/xsl:when>
				<xsl:otherwise>
					0.00
				</xsl:otherwise>
			</xsl:choose-->
		</xsl:when>
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose> 
</xsl:template>
<!-- End of MCCA Fee-->

<!-- Age -->
<xsl:template name ="AGE">
	<xsl:param name="FACTORELEMENT" /> <!-- Depending on the factor element COMP/COLL..etc -->
<!--
	<xsl:variable name="PAGE" select="VEHICLES/VEHICLE/AGE"></xsl:variable>
	  
		IF (<xsl:value-of select="$PAGE"/> &gt; 0)
		THEN 
			<xsl:choose>
				<xsl:when test="$FACTORELEMENT ='COMP'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEAGEGROUPOTHER']/NODE[@ID='VEHICLEAGEGROUPCOMP']/ATTRIBUTES[@AGE = $PAGE ]/@RELATIVITY"/>	
				</xsl:when>
				<xsl:when test="$FACTORELEMENT ='COLL'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEAGEGROUPCOLLISION']/NODE[@ID='VEHICLEAGEGROUPCOLL']/ATTRIBUTES[@AGE = $PAGE ]/@RELATIVITY"/>	
				</xsl:when>
				<xsl:otherwise>
					1.00
				</xsl:otherwise>
			</xsl:choose>
		ELSE
			1.00
-->			
		<xsl:variable name="PAGE" select="VEHICLES/VEHICLE/AGE"></xsl:variable>
	  
	  <xsl:choose> 
			<xsl:when test="$PAGE &gt; 0"> 
				<xsl:choose>
					<xsl:when test="$FACTORELEMENT ='COMP'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEAGEGROUPOTHER']/NODE[@ID='VEHICLEAGEGROUPCOMP']/ATTRIBUTES[@AGE = $PAGE ]/@RELATIVITY"/>	
					</xsl:when>
					<xsl:when test="$FACTORELEMENT ='COLL'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEAGEGROUPCOLLISION']/NODE[@ID='VEHICLEAGEGROUPCOLL']/ATTRIBUTES[@AGE = $PAGE ]/@RELATIVITY"/>	
					</xsl:when>
					<xsl:otherwise>
						1.00
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
		
		<xsl:otherwise>0.00</xsl:otherwise>
		</xsl:choose>	
			
			
	 
</xsl:template>

<!-- Anti Breaking System -->
<xsl:template name ="ABS">
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/ISANTILOCKBRAKESDISCOUNTS= 'TRUE' and VEHICLES/VEHICLE/VEHICLETYPE !='SCO'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ANTILOCKBREAKSYSTEM']/NODE[@ID='ABS']/ATTRIBUTES/@FACTOR"/>
		</xsl:when>
		<xsl:otherwise>
			1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<xsl:template name ="ABS_DISPLAY">
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/ISANTILOCKBRAKESDISCOUNTS= 'TRUE' and VEHICLES/VEHICLE/VEHICLETYPE !='SCO'">
				Included
		</xsl:when>
		<xsl:otherwise>
			0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<xsl:template name ="ABS_CREDIT">
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/ISANTILOCKBRAKESDISCOUNTS= 'TRUE' and VEHICLES/VEHICLE/VEHICLETYPE !='SCO'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='ANTILOCKBREAKSYSTEM']/NODE[@ID='ABS']/ATTRIBUTES/@CREDIT"/>%
		</xsl:when>
		<xsl:otherwise>
			
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<!-- Seat Belt-->
<xsl:template name ="SEATBELT">
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/WEARINGSEATBELT = 'TRUE' and VEHICLES/VEHICLE/VEHICLETYPE !='SCO'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SEATBELTCREDIT']/NODE[@ID='SEATBELT']/ATTRIBUTES/@FACTOR"/>
		</xsl:when>
		<xsl:otherwise>
			1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<xsl:template name ="SEATBELT_DISPLAY">
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/WEARINGSEATBELT = 'TRUE' and VEHICLES/VEHICLE/VEHICLETYPE !='SCO'">
			Included
		</xsl:when>
		<xsl:otherwise>
			0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<!-- Air Bag-->
<xsl:template name ="AIRBAG">
	<xsl:variable name="PAIRBAGDISCOUNT" select="VEHICLES/VEHICLE/AIRBAGDISCOUNT" />
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SCO' or $PAIRBAGDISCOUNT = '' or $PAIRBAGDISCOUNT = '0'">
				1.00
		</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='AIRBAGCREDIT']/NODE[@ID='AIRBAG']/ATTRIBUTES[@ID=$PAIRBAGDISCOUNT]/@FACTOR"/>
			 
		</xsl:otherwise>
	</xsl:choose>
	
</xsl:template>
<xsl:template name ="AIRBAG_DISPLAY">
	<xsl:variable name="PAIRBAGDISCOUNT" select="VEHICLES/VEHICLE/AIRBAGDISCOUNT" />
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SCO' or $PAIRBAGDISCOUNT = ''">
				0
		</xsl:when>
		<xsl:otherwise>
			Included
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<!-- Loan/Lease Gap -->
<xsl:template name ="LOANLEASEGAP">
	<!-- 1. ONLY ON NEW (CURRENT) VEHICLES ... 
		 2. LEASED: 10% TO EACH COMPREHENSIVE AND COLLISION AND COMPARE TO MIN
		 3. LOAN: 5% TO EACH COMPREHENSIVE AND COLLISION AND COMPARE TO MIN -->
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE ='SCO' or VEHICLES/VEHICLE/LOANLEASEGAP =0 or VEHICLES/VEHICLE/LOANLEASEGAP =''">
			0.00
		</xsl:when>
		<xsl:otherwise>
			<xsl:variable name="GAPFACTOR">
				<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/LOANLEASEGAP = 'LOAN'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='A11LOANLEASEGAP']/NODE[@ID='LOANLEASEGAP']/ATTRIBUTES/@LOANFACTOR"/>	
					</xsl:when>
					<xsl:when test="VEHICLES/VEHICLE/LOANLEASEGAP = 'LEASE'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='A11LOANLEASEGAP']/NODE[@ID='LOANLEASEGAP']/ATTRIBUTES/@LEASEFACTOR"/>	
					</xsl:when>
					<xsl:otherwise>
						0.00
					</xsl:otherwise>
				</xsl:choose>
			</xsl:variable>
			
			<xsl:variable name="MINVALUECOMP">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='A11LOANLEASEGAP']/NODE[@ID='LOANLEASEGAP']/ATTRIBUTES/@MINPREMIUMPERCARCOMP"/>	
			</xsl:variable>
			<xsl:variable name="MINVALUECOLL">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='A11LOANLEASEGAP']/NODE[@ID='LOANLEASEGAP']/ATTRIBUTES/@MINPREMIUMPERCARCOLL"/>	
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


<!-- Sound reproducing tapes -->
<xsl:template name="SOUNDREPRODUCINGTAPES">
	<xsl:choose>
		<xsl:when test ="VEHICLES/VEHICLE/IS200SOUNDREPRODUCING='TRUE' and VEHICLES/VEHICLE/VEHICLETYPE !='SCO'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='SOUNDREPRODUCINGTAPES']/ATTRIBUTES/@PREMIUM" />
		</xsl:when>
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="SOUNDREPRODUCINGTAPESLIMIT">
	<xsl:choose>
		<xsl:when test ="VEHICLES/VEHICLE/IS200SOUNDREPRODUCING = 'TRUE' and VEHICLES/VEHICLE/VEHICLETYPE !='SCO'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='SOUNDREPRODUCINGTAPES']/ATTRIBUTES/@LIMIT" />
		</xsl:when>
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
</xsl:template>



<!-- Sound receiving and transmitting tapes -->
<xsl:template name="SOUNDRECVTRANSEQUIPMENT">
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/SOUNDRECEIVINGTRANSMITTINGSYSTEM &gt; 0 and VEHICLES/VEHICLE/VEHICLETYPE !='SCO'">
			<!--
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='SOUNDRECVTRANSEQUIPMENT']/ATTRIBUTES/@PREMIUM"/>
			*
			(<xsl:value-of select="VEHICLES/VEHICLE/SOUNDRECEIVINGTRANSMITTINGSYSTEM"/>		
			  DIV
			 <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='SOUNDRECVTRANSEQUIPMENT']/ATTRIBUTES/@PER_EACH_ADDITION_OF"/>
			)
			-->
			<!-- new calculation added by praveen -->
			
			<xsl:variable name="VAR1"> 
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='SOUNDRECVTRANSEQUIPMENT']/ATTRIBUTES/@PREMIUM"/>
			</xsl:variable>	
			
			<xsl:variable name="VAR2"> 
			<xsl:value-of select="VEHICLES/VEHICLE/SOUNDRECEIVINGTRANSMITTINGSYSTEM"/>		
			</xsl:variable>	
			
			<xsl:variable name="VAR3"> 
			 <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='SOUNDRECVTRANSEQUIPMENT']/ATTRIBUTES/@PER_EACH_ADDITION_OF"/>
			</xsl:variable>	
			<xsl:value-of select="round($VAR1 * round($VAR2 div $VAR3))"/>
				
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>	
	</xsl:choose>		
</xsl:template>


<!-- Work Loss Waiver -->
<xsl:template name="WAIVEWORKLOSS">
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/WAIVEWORKLOSS = 'TRUE' and VEHICLES/VEHICLE/VEHICLETYPE !='SCO'">
			<!--xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='SOUNDRECVTRANSEQUIPMENT']/ATTRIBUTES/@PREMIUM"/>
			*
			(<xsl:value-of select="SOUNDRECEIVINGTRANSMITTINGSYSTEM"/>		
			  DIV
			 <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='SOUNDRECVTRANSEQUIPMENT']/ATTRIBUTES/@PER_EACH_ADDITION_OF"/>
			)	-->
			0.00	
		</xsl:when>
		<xsl:otherwise>
			0.00
		</xsl:otherwise>	
	</xsl:choose>		
</xsl:template>

<xsl:template name="PIP_DISPLAY">	
	<xsl:variable name="PPIPDISPLAY">
		<xsl:call-template name="COVERAGETYPE"/>
	</xsl:variable>
	
	<!--xsl:value-of select="$PPIPDISPLAY"/--> 
	<!--xsl:choose>
		<xsl:when test="'$PPIPDISPLAY' ='1' ">0</xsl:when>
		<xsl:otherwise>Included</xsl:otherwise>	
	</xsl:choose-->
	 
 	IF(('<xsl:value-of select="VEHICLES/VEHICLE/VEHICLETYPE"/>' != 'SCO') and (<xsl:value-of select="$PPIPDISPLAY"/> &gt; 0.00)) 
 	THEN
		0
	ELSE
		1
		 
</xsl:template>


<!-- Extra Equipment :Comprehensive-->
<xsl:template name ="EXTRAEQUIPMENTCOMP"> 
	<xsl:variable name="PDEDUCTIBLE" select="VEHICLES/VEHICLE/EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE" />
	<xsl:variable name="PINSURANCEAMOUNT" select="VEHICLES/VEHICLE/INSURANCEAMOUNT" />
	
		IF ('<xsl:value-of select="VEHICLES/VEHICLE/VEHICLETYPE"/>' != 'SCO' and '<xsl:value-of select="$PINSURANCEAMOUNT"/>' !='' and '<xsl:value-of select="$PINSURANCEAMOUNT"/>' !='NO COVERAGE' and <xsl:value-of select="$PINSURANCEAMOUNT"/> &gt; 0 and '<xsl:value-of select="$PDEDUCTIBLE"/>' !='' and '<xsl:value-of select="$PDEDUCTIBLE"/>' != '0' )
		THEN
			(
				<!-- Check for Rate per value -->
				<xsl:variable name="PRATEPERVALUE">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COMP']/@RATE_PER_VALUE"/>
				</xsl:variable>
					
				<!-- Check the Extra Equipment component -->
				<xsl:variable name="PEXTRAEQUIPMENTCOMP">			
					<xsl:choose>
						<xsl:when test="'$PDEDUCTIBLE'='' or $PDEDUCTIBLE=0">
							0.00
						</xsl:when>
						<xsl:when test="'$PDEDUCTIBLE'='250'">
							( <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COMP']/@RATE"/>)
							*
							( <xsl:value-of select ="$PINSURANCEAMOUNT" /> DIV <xsl:value-of select="$PRATEPERVALUE"/>)
						</xsl:when>
				
						
						
						
						<!-- Multiply rate with Deductible Relativity-->
						<xsl:otherwise> 
							(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COMP']/@RATE"/>)
							*
							(<xsl:value-of select ="$PINSURANCEAMOUNT" /> DIV <xsl:value-of select="$PRATEPERVALUE"/>)
							*
							(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLEO']/NODE[@ID='DEDTBLO']/ATTRIBUTES[@DEDUCTIBLE=$PDEDUCTIBLE]/@RELATIVITY"/>)
						</xsl:otherwise>
					</xsl:choose>	
				</xsl:variable>
			
				<!-- Check for Minimum Premium  -->
				<xsl:variable name="PMINIMUMVALUE">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COMP']/@MINIMUM_PREMIUM"/>
				</xsl:variable>
			 
				<!-- If the extra equipment component is less than the minimum premium component then return minimum premium -->				
				(
					<xsl:choose>
					<xsl:when test="$PEXTRAEQUIPMENTCOMP=0 or '$PEXTRAEQUIPMENTCOMP' =''">
						0.00
					</xsl:when>
					<xsl:otherwise>
							IF (<xsl:value-of select="$PEXTRAEQUIPMENTCOMP"/> &gt; <xsl:value-of select="$PMINIMUMVALUE"/>)
							THEN
								<xsl:value-of select = "$PEXTRAEQUIPMENTCOMP"/>
							ELSE IF (<xsl:value-of select="$PEXTRAEQUIPMENTCOMP"/> &lt;= <xsl:value-of select="$PMINIMUMVALUE"/>)
							THEN
								<xsl:value-of select = "$PMINIMUMVALUE"/>
							ELSE
								0.00
					</xsl:otherwise>
					
					</xsl:choose>
				
				)
			)		 
		ELSE
			0.00
</xsl:template>

<!-- Extra Equipment :Collision-->
<xsl:template name ="EXTRAEQUIPMENTCOLL">
	<xsl:variable name="PDEDUCTIBLE" select="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONDEDUCTIBLE" />
	<xsl:variable name="PCOLLISIONTYPE" select="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONTYPE"/>
	<xsl:variable name="PINSURANCEAMOUNT" select="VEHICLES/VEHICLE/INSURANCEAMOUNT" />
	
		IF ('<xsl:value-of select="VEHICLES/VEHICLE/VEHICLETYPE"/>' != 'SCO' and '<xsl:value-of select="$PINSURANCEAMOUNT"/>' !='' and '<xsl:value-of select="$PINSURANCEAMOUNT"/>' !='NO COVERAGE' and <xsl:value-of select="$PINSURANCEAMOUNT"/> &gt; 0 and '<xsl:value-of select="$PDEDUCTIBLE"/>' !='' and '<xsl:value-of select="$PDEDUCTIBLE"/>' != '0' )
		THEN
			(
			<!-- Check for Rate per value -->
			<xsl:variable name="PRATEPERVALUE">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE_PER_VALUE"/>
			</xsl:variable>
			
			
			<!-- Check the Extra Equipment component -->		
			<xsl:variable name="PEXTRAEQUIPMENTCOLL">
			(
					IF (
						('<xsl:value-of select="$PINSURANCEAMOUNT"/>' !='' and '<xsl:value-of select="$PINSURANCEAMOUNT"/>' !='NO COVERAGE' and <xsl:value-of select="$PINSURANCEAMOUNT"/> &gt; 0  and '<xsl:value-of select="$PDEDUCTIBLE"/>' !='' and '<xsl:value-of select="$PDEDUCTIBLE"/>' != '0'  )
						or
						('<xsl:value-of select="$PINSURANCEAMOUNT"/>' !='' and '<xsl:value-of select="$PINSURANCEAMOUNT"/>' !='NO COVERAGE' and <xsl:value-of select="$PINSURANCEAMOUNT"/> &gt; 0 and '<xsl:value-of select="$PDEDUCTIBLE"/>' ='0'  and '<xsl:value-of select="$PCOLLISIONTYPE"/>' = 'LIMITED')
						)
					THEN				 
						<xsl:choose>
									<xsl:when test="POLICY/STATENAME='MICHIGAN'">
										<xsl:choose>							
											<xsl:when test="'$PDEDUCTIBLE'='250'">
												(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE"/>)
												*
												(<xsl:value-of select ="$PINSURANCEAMOUNT" /> DIV <xsl:value-of select="$PRATEPERVALUE"/>)
											</xsl:when>
											<xsl:when test="'$PCOLLISIONTYPE'='LIMITED'">
												(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE"/>)
												*
												(<xsl:value-of select ="$PINSURANCEAMOUNT" /> DIV <xsl:value-of select="$PRATEPERVALUE"/>)
												*
												(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@COLLISIONTYPE=$PCOLLISIONTYPE]/@RELATIVITY"/>)
											</xsl:when>							
											<!-- Multiply rate with Deductible Relativity-->
											<xsl:otherwise>
												(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE"/>)
												*
												(<xsl:value-of select ="$PINSURANCEAMOUNT" /> DIV <xsl:value-of select="$PRATEPERVALUE"/>)
												*
												(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@COLLISIONTYPE=$PCOLLISIONTYPE and @DEDUCTIBLE=$PDEDUCTIBLE]/@RELATIVITY"/>)
											</xsl:otherwise>
										</xsl:choose>
									</xsl:when>
									<xsl:when test="POLICY/STATENAME = 'INDIANA'">
										<xsl:choose>
											<xsl:when test="'$PDEDUCTIBLE'='0'">0</xsl:when>
											<xsl:when test="$PDEDUCTIBLE=250">
												(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE"/>)
												*
												( <xsl:value-of select ="$PINSURANCEAMOUNT" /> DIV <xsl:value-of select="$PRATEPERVALUE"/>)
											</xsl:when>
											
											<xsl:when test="$PDEDUCTIBLE &gt; 0">
													(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE"/>)
												*
												(<xsl:value-of select ="$PINSURANCEAMOUNT" /> DIV <xsl:value-of select="$PRATEPERVALUE"/>)
												*
												(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@DEDUCTIBLE=$PDEDUCTIBLE]/@RELATIVITY"/>)
											
											</xsl:when>
											<!-- Multiply rate with Deductible Relativity -->
											<xsl:otherwise> 
												0.00
											</xsl:otherwise>
										</xsl:choose>
									</xsl:when>
									<xsl:otherwise>
										0.00
									</xsl:otherwise>
								</xsl:choose>	
			ELSE
				(0.00)
		)		
		</xsl:variable>
	
		<!-- Check for Minimum Premium -->
		<xsl:variable name="PMINIMUMVALUE">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@MINIMUM_PREMIUM"/>
		</xsl:variable>
				
		<!-- If the extra equipment component is less than the minimum premium component then return minimum premium -->							
		(
			<xsl:choose>
				<xsl:when test="'$PEXTRAEQUIPMENTCOLL'='' or ('$PEXTRAEQUIPMENTCOLL' ='0' and '$PCOLLISIONTYPE' !='LIMITED')">
					0.00
				</xsl:when>
				<xsl:otherwise>
					IF (<xsl:value-of select="$PEXTRAEQUIPMENTCOLL"/> &gt; <xsl:value-of select="$PMINIMUMVALUE"/>)
					THEN
						<xsl:value-of select = "$PEXTRAEQUIPMENTCOLL"/>
					ELSE IF (<xsl:value-of select="$PEXTRAEQUIPMENTCOLL"/> &lt;= <xsl:value-of select="$PMINIMUMVALUE"/>)
					THEN
						<xsl:value-of select = "$PMINIMUMVALUE"/>
					ELSE
						0.00
				</xsl:otherwise>
			</xsl:choose>	
		)
		)
		ELSE
			0.00
</xsl:template>


<xsl:template name="GETEXTRAEQUIPCOLLISIONVALUE">
	<xsl:variable name="PDEDUCTIBLE" select="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONDEDUCTIBLE" />
	<xsl:variable name="PCOLLISIONTYPE" select="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONTYPE"/>
	<xsl:variable name="PINSURANCEAMOUNT" select="VEHICLES/VEHICLE/INSURANCEAMOUNT" />
	
			<!-- Check for Rate per value -->
			<xsl:variable name="PRATEPERVALUE">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE_PER_VALUE"/>
			</xsl:variable>
			
			<!-- Check the Extra Equipment component -->		
			<xsl:variable name="PEXTRAEQUIPMENTCOLL">
				<xsl:choose>
					<xsl:when test="POLICY/STATENAME='MICHIGAN'">
						<xsl:choose>							
							<xsl:when test="$PDEDUCTIBLE=250">
								(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE"/>)
								*
								(<xsl:value-of select ="$PINSURANCEAMOUNT" /> DIV <xsl:value-of select="$PRATEPERVALUE"/>)
							</xsl:when>
							<xsl:when test="'$PCOLLISIONTYPE'='LIMITED'">
								(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE"/>)
								*
								(<xsl:value-of select ="$PINSURANCEAMOUNT" /> DIV <xsl:value-of select="$PRATEPERVALUE"/>)
								*
								(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@COLLISIONTYPE=$PCOLLISIONTYPE]/@RELATIVITY"/>)
							</xsl:when>							
							<!-- Multiply rate with Deductible Relativity-->
							<xsl:otherwise>
								(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE"/>)
								*
								(<xsl:value-of select ="$PINSURANCEAMOUNT" /> DIV <xsl:value-of select="$PRATEPERVALUE"/>)
								*
								(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@COLLISIONTYPE=$PCOLLISIONTYPE and @DEDUCTIBLE=$PDEDUCTIBLE]/@RELATIVITY"/>)
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					<xsl:when test="POLICY/STATENAME = 'INDIANA'">
						<xsl:choose>
							<xsl:when test="$PDEDUCTIBLE=250">
								(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE"/>)
								*
								( <xsl:value-of select ="$PINSURANCEAMOUNT" /> DIV <xsl:value-of select="$PRATEPERVALUE"/>)
							</xsl:when>
							
							 <xsl:when test="'$PCOLLISIONTYPE'='LIMITED'">
								(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE"/>)
								*
								(<xsl:value-of select ="$PINSURANCEAMOUNT" /> DIV <xsl:value-of select="$PRATEPERVALUE"/>)
								*
								(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@COLLISIONTYPE=$PCOLLISIONTYPE]/@RELATIVITY"/>)
							
							</xsl:when>	
							
							<!-- Multiply rate with Deductible Relativity -->
							<xsl:otherwise> 
								(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@RATE"/>)
								*
								(<xsl:value-of select ="$PINSURANCEAMOUNT" /> DIV <xsl:value-of select="$PRATEPERVALUE"/>)
								*
								(<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@DEDUCTIBLE=$PDEDUCTIBLE]/@RELATIVITY"/>)
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
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MISCELLANEOUSCOVERAGES']/NODE[@ID='EXTRAEQUIPMENT']/ATTRIBUTES[@RISK='COLL']/@MINIMUM_PREMIUM"/>
			</xsl:variable>
				 
			<!-- If the extra equipment component is less than the minimum premium component then return minimum premium -->							
			(
				<xsl:choose>
					<xsl:when test="('$PEXTRAEQUIPMENTCOLL' ='0' and '$PCOLLISIONTYPE' !='LIMITED') or '$PEXTRAEQUIPMENTCOLL' =''">
						0.00
					</xsl:when>
					<xsl:otherwise>
						IF (<xsl:value-of select="$PEXTRAEQUIPMENTCOLL"/> &gt; <xsl:value-of select="$PMINIMUMVALUE"/>)
						THEN
							<xsl:value-of select = "$PEXTRAEQUIPMENTCOLL"/>
						ELSE IF (<xsl:value-of select="$PEXTRAEQUIPMENTCOLL"/> &lt;= <xsl:value-of select="$PMINIMUMVALUE"/>)
						THEN
							<xsl:value-of select = "$PMINIMUMVALUE"/>
						ELSE
							0.00
					</xsl:otherwise>
				</xsl:choose>	
			)	 		
</xsl:template>



<!-- *******************************************************************************************************-->
<!-- ************** Other Factors **************************************************************************-->
<!-- *******************************************************************************************************-->

<!-- Territory Base -->
<xsl:template name ="TERRITORYBASE">
	<xsl:param name="FACTORELEMENT" /> <!-- Depending on the factor element BI/PD..etc -->
	<xsl:choose>	
		<!-- For Michigan Automobile -->
		<xsl:when test ="POLICY/STATENAME ='MICHIGAN'">
			<xsl:call-template name ="TERRITORYBASE_MICHIGAN">
				<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>	
			</xsl:call-template>		
		</xsl:when>		
		<!-- For Indiana Automobile -->
		<xsl:when test ="POLICY/STATENAME ='INDIANA'">
			<xsl:call-template name ="TERRITORYBASE_INDIANA">
				<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>	
			</xsl:call-template>
		</xsl:when>
		<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- Territory base michigan -->
<xsl:template name ="TERRITORYBASE_MICHIGAN">
	<xsl:param name="FACTOR" /> <!-- Depending on the factor element BI/PD..etc -->
	<xsl:variable name="PTERRITORYCODE" select="POLICY/TERRITORY"/>
 <!--	
	<xsl:choose>
		<xsl:when test="$PTERRITORYCODE =''">
			1.00
		</xsl:when>
		<xsl:otherwise>
			
			IF ('<xsl:value-of select="$FACTOR"/>' = 'BI')THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE=$PTERRITORYCODE]/@BIBASE"/> 
			ELSE IF ('<xsl:value-of select="$FACTOR"/>' = 'PD')THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE=$PTERRITORYCODE]/@PDBASE"/> 
			ELSE IF ('<xsl:value-of select="$FACTOR"/>' = 'PPI')THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE=$PTERRITORYCODE]/@PPIBASE"/> 
				 
			ELSE IF ('<xsl:value-of select="$FACTOR"/>' = 'PIP')THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE=$PTERRITORYCODE]/@PIPBASE"/> 
			ELSE IF ('<xsl:value-of select="$FACTOR"/>' = 'UM') THEN 
				1.00 
			ELSE IF ('<xsl:value-of select="$FACTOR"/>' = 'UIM') THEN 	
				1.00 
			ELSE IF ('<xsl:value-of select="$FACTOR"/>' = 'COMP') THEN 			 
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPREHENSIVETERRITORYBASE']/NODE[@ID ='COMPREHENSIVETERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE ]/@BASE"/> 
			ELSE IF ('<xsl:value-of select="$FACTOR"/>' = 'COLL') THEN  
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLISIONTERRITORYBASE']/NODE[@ID ='COLLISIONTERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE ]/@BASE"/> 
			ELSE IF ('<xsl:value-of select="$FACTOR"/>' = 'LTDCOLL') THEN  
				1.00	
			ELSE
				1.00
		</xsl:otherwise>
	</xsl:choose>
	-->
	<!-- new calculation added by praveen singh -->
	
	<xsl:choose>
		<xsl:when test="$PTERRITORYCODE =''">
			1.00
		</xsl:when>
		<xsl:otherwise>
			<xsl:choose> 
			<xsl:when test="$FACTOR = 'BI'"> 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE=$PTERRITORYCODE]/@BIBASE"/> 
			</xsl:when>
			
			<xsl:when test="$FACTOR = 'PD'"> 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE=$PTERRITORYCODE]/@PDBASE"/> 
			</xsl:when> 
			
			<xsl:when test="$FACTOR = 'PPI'"> 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE=$PTERRITORYCODE]/@PPIBASE"/> 
			</xsl:when> 
			
			<xsl:when test="$FACTOR = 'PIP'"> 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE=$PTERRITORYCODE]/@PIPBASE"/> 
			</xsl:when> 
			
			<xsl:when test="$FACTOR = 'UM'"> 
				1.00
			</xsl:when> 
			
			<xsl:when test="$FACTOR = 'UIM'"> 
				1.00
			</xsl:when> 
				
			 <xsl:when test="$FACTOR = 'COMP'"> 
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPREHENSIVETERRITORYBASE']/NODE[@ID ='COMPREHENSIVETERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE ]/@BASE"/> 
			</xsl:when>
		
			<xsl:when test="$FACTOR = 'COLL'"> 
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLISIONTERRITORYBASE']/NODE[@ID ='COLLISIONTERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE ]/@BASE"/> 
			</xsl:when> 
			
			<xsl:when test="$FACTOR = 'LTDCOLL'"> 
				1.00	
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
<xsl:template name ="TERRITORYBASE_INDIANA">
	<xsl:param name="FACTOR" /> <!-- Depending on the factor element BI/PD..etc -->
	<xsl:variable name="PTERRITORYCODE" select="POLICY/TERRITORY"/> <!--VEHICLES/VEHICLE/TERRCODEGARAGEDLOCATION"/-->
 <!--	
	<xsl:choose>
		<xsl:when test="$PTERRITORYCODE =''">
			1.00
		</xsl:when>
		<xsl:otherwise>
	  IF ('<xsl:value-of select="$FACTOR"/>' = 'BI')THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE= $PTERRITORYCODE ]/@BIBASE"/> 
			ELSE IF ('<xsl:value-of select="$FACTOR"/>' = 'PD')THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE= $PTERRITORYCODE ]/@PDBASE"/> 
			ELSE IF ('<xsl:value-of select="$FACTOR"/>' = 'MEDPAY' and '<xsl:value-of select="POLICY/STATENAME"/>' = 'INDIANA')THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE= $PTERRITORYCODE ]/@MEDPAYBASE"/> 
			ELSE IF ('<xsl:value-of select="$FACTOR"/>' = 'UM') THEN 
				1.00 
			ELSE IF ('<xsl:value-of select="$FACTOR"/>' = 'UIM') THEN 	
				1.00 
			ELSE IF ('<xsl:value-of select="$FACTOR"/>' = 'COMP') THEN 			 
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPREHENSIVETERRITORYBASE']/NODE[@ID ='COMPREHENSIVETERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE ]/@BASE"/> 
			ELSE IF ('<xsl:value-of select="$FACTOR"/>' = 'COLL') THEN  
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLISIONTERRITORYBASE']/NODE[@ID ='COLLISIONTERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE ]/@BASE"/> 
			ELSE IF ('<xsl:value-of select="$FACTOR"/>' = 'LTDCOLL') THEN  
				1.00	
			ELSE
				1.00
		</xsl:otherwise>
	</xsl:choose>
 	-->
	 
	<xsl:choose>
		<xsl:when test="$PTERRITORYCODE =''">
			1.00
		</xsl:when>
		<xsl:otherwise>
			 <xsl:choose>
				<xsl:when test="$FACTOR = 'BI'">
					 	<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE= $PTERRITORYCODE ]/@BIBASE"/> 
				</xsl:when>
			
				<xsl:when test="$FACTOR = 'PD'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE= $PTERRITORYCODE ]/@PDBASE"/> 
				</xsl:when>
				
				<xsl:when test="$FACTOR = 'MEDPAY'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='BASECOVGANDRATES']/NODE[@ID='PREMIUMTERRITORY']/ATTRIBUTES[@TERRITORYCODE= $PTERRITORYCODE ]/@MEDPAYBASE"/> 
				</xsl:when>
				
				<xsl:when test="$FACTOR = 'UM'">
						1.00
				</xsl:when>
				
				<xsl:when test="$FACTOR = 'UIM'">
						1.00
				</xsl:when>
				
				<xsl:when test="$FACTOR = 'COMP'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMPREHENSIVETERRITORYBASE']/NODE[@ID ='COMPREHENSIVETERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE ]/@BASE"/> 
				</xsl:when>
				
				<xsl:when test="$FACTOR = 'COLL'">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COLLISIONTERRITORYBASE']/NODE[@ID ='COLLISIONTERRITORY']/ATTRIBUTES[@TERRITORY = $PTERRITORYCODE ]/@BASE"/> 
				</xsl:when>
				
				<xsl:when test="$FACTOR = 'LTDCOLL'">
						1.00
				</xsl:when>
				
				<xsl:otherwise>1.00</xsl:otherwise>
			
			</xsl:choose>
	  </xsl:otherwise>
	</xsl:choose>
	
	  
	
	
</xsl:template>
<!-- End of Territory base Indiana -->
<!-- End of Territory Base -->

<!-- Insurance Score -->
<xsl:template name ="INSURANCESCORE">
	<xsl:variable name="INS" select="POLICY/INSURANCESCORE"></xsl:variable>
	<xsl:choose>
		<xsl:when test = "$INS = 0">
			1.00  <!-- <xsl:value-of select= "$INS"></xsl:value-of> -->
		</xsl:when>
		<xsl:when test = "$INS = ''">
			1.00
		</xsl:when>
		<xsl:when test = "$INS = 'No Hit No Score'">
			0.87
		</xsl:when>
		<xsl:when test = "$INS &gt;= 751">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = 751]/@FACTOR"/>
		</xsl:when>		
		<xsl:otherwise>						
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@FACTOR"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<xsl:template name ="INSURANCESCORE_DISPLAY">
	<xsl:variable name="INS" select="POLICY/INSURANCESCORE"></xsl:variable>
	
	<xsl:variable name="INSDISCOUNT">
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
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE = 751]/@FACTOR"/>
			</xsl:when>		
			<xsl:otherwise>						
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='INSURANCEDISCOUNT']/NODE[@ID ='INSURANCESCOREDISCOUNT']/ATTRIBUTES[@MINSCORE &lt;= $INS and @MAXSCORE &gt;= $INS]/@FACTOR"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:variable>
	<xsl:choose>
		<xsl:when test = "$INSDISCOUNT = 0 or $INSDISCOUNT='' or $INSDISCOUNT=1">
			0
		</xsl:when>	
		<xsl:when test = "$INSDISCOUNT &gt; 0 and $INSDISCOUNT &lt; 1">
			Included
		</xsl:when>		 
		<xsl:otherwise>	
			0		 
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>



<!-- Optimal -->
<xsl:template name ="OPTIMAL">
	1.00
</xsl:template>



<!-- antique or classic or motor home-->
<xsl:template name ="TYPEOFVEHICLE_1">
	1.00
</xsl:template>



<!--  *******************************************************************************************  -->

<xsl:template name ="TYPEOFVEHICLE">	 
	<xsl:param name="FACTORELEMENT" /> <!-- Depending on the factor element BI/PD..etc -->
	<xsl:variable name="PVEHICLETYPE" select="VEHICLES/VEHICLE/VEHICLETYPE" />
	<!-- take the basic rates -->
	<!-- multiply with deductible rates in case of collision and comprehensive -->	
<!--	IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'BI')THEN 
		(
		<xsl:choose>
			<xsl:when test="$PVEHICLETYPE='AA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@BI"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='CA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@BI"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='SA'">			
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@BI"/>	
			</xsl:when>
			<xsl:otherwise>
				1.00
			</xsl:otherwise>		
		</xsl:choose>
		)
	ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'PD')THEN 
		(
		<xsl:choose>
			<xsl:when test="$PVEHICLETYPE='AA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@PD"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='CA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@PD"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='SA'">			
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@PD"/>	
			</xsl:when>
			<xsl:otherwise>
				1.00
			</xsl:otherwise>		
		</xsl:choose>
		)
	ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'PPI')THEN 
		(
		<xsl:choose>
			<xsl:when test="$PVEHICLETYPE='AA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@PPI"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='CA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@PPI"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='SA'">			
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@PPI"/>	
			</xsl:when>
			<xsl:otherwise>
				1.00
			</xsl:otherwise>
		</xsl:choose>
		)
	ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'PIP')THEN 
		(
		<xsl:choose>
			<xsl:when test="$PVEHICLETYPE='AA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@PIP"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='CA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@PIP"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='SA'">			
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@PIP"/>	
			</xsl:when>
			<xsl:otherwise>
				1.00
			</xsl:otherwise>
		</xsl:choose>
		)
	ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'UM') THEN 
		(
		<xsl:choose>
			<xsl:when test="$PVEHICLETYPE='AA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@UM"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='CA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@UM"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='SA'">			
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@UM"/>	
			</xsl:when>
			<xsl:otherwise>
				1.00
			</xsl:otherwise>
		</xsl:choose>
		)
	ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'UIM') THEN 		 
		(
		<xsl:choose>
			<xsl:when test="$PVEHICLETYPE='AA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@UIM"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='CA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@UIM"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='SA'">			
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@UIM"/>	
			</xsl:when>
			<xsl:otherwise>
				1.00
			</xsl:otherwise>
		</xsl:choose>
		)
	ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'COMP') THEN 
		<xsl:choose>
			<xsl:when test="$PVEHICLETYPE='AA'">
				(
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@COMP50"/>	
				)
				*
				(
					<xsl:call-template name ="FETCHDEDUCTIBLEFACTOR">
						<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
					</xsl:call-template>
				)				
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='CA'">
				(
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@COMP50"/>	
				)
				*
				(
					<xsl:call-template name ="FETCHDEDUCTIBLEFACTOR">
						<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
					</xsl:call-template>
				)	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='SA'">	
				(		
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@COMP50"/>	
				)
				*
				(
					<xsl:call-template name ="FETCHDEDUCTIBLEFACTOR">
						<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
					</xsl:call-template>
				)	
			</xsl:when>
			<xsl:otherwise>
				1.00
			</xsl:otherwise>
		</xsl:choose>
	ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'COLL' or '<xsl:value-of select="$FACTORELEMENT"/>' = 'LTDCOLL') THEN 
			<xsl:choose>
				<xsl:when test="$PVEHICLETYPE='AA'">
				(
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@COLL250"/>	
				)
				*
				(
					<xsl:call-template name ="FETCHDEDUCTIBLEFACTOR">
						<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
					</xsl:call-template>
				)				
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='CA'">
				(
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@COLL250"/>	
				)
				*
				(
					<xsl:call-template name ="FETCHDEDUCTIBLEFACTOR">
						<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
					</xsl:call-template>
				)	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='SA'">	
				(		
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@COLL250"/>	
				)
				*
				(
					<xsl:call-template name ="FETCHDEDUCTIBLEFACTOR">
						<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
					</xsl:call-template>
				)	
			</xsl:when>
				<xsl:otherwise>
					1.00
				</xsl:otherwise>		
			</xsl:choose>
	ELSE
		1.00
 -->
 
 <xsl:choose>  
 <xsl:when test="$FACTORELEMENT = 'BI'">
 		<xsl:choose>
			<xsl:when test="$PVEHICLETYPE='AA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@BI"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='CA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@BI"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='SA'">			
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@BI"/>	
			</xsl:when>
			<xsl:otherwise>
				1.00
			</xsl:otherwise>		
		</xsl:choose>
</xsl:when>
	
	<xsl:when test="$FACTORELEMENT = 'PD'"> 
	 
		<xsl:choose>
			<xsl:when test="$PVEHICLETYPE='AA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@PD"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='CA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@PD"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='SA'">			
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@PD"/>	
			</xsl:when>
			<xsl:otherwise>
				1.00
			</xsl:otherwise>		
		</xsl:choose>
	</xsl:when>	 
	
	<xsl:when test="$FACTORELEMENT = 'PPI'"> 
	 
		<xsl:choose>
			<xsl:when test="$PVEHICLETYPE='AA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@PPI"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='CA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@PPI"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='SA'">			
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@PPI"/>	
			</xsl:when>
			<xsl:otherwise>
				1.00
			</xsl:otherwise>
		</xsl:choose>
	</xsl:when>
	
	<xsl:when test="$FACTORELEMENT = 'PIP'"> 
	 
		<xsl:choose>
			<xsl:when test="$PVEHICLETYPE='AA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@PIP"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='CA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@PIP"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='SA'">			
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@PIP"/>	
			</xsl:when>
			<xsl:otherwise>
				1.00
			</xsl:otherwise>
		</xsl:choose>
	</xsl:when>
	
	<xsl:when test="$FACTORELEMENT = 'UM'"> 
	 
		<xsl:choose>
			<xsl:when test="$PVEHICLETYPE='AA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@UM"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='CA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@UM"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='SA'">			
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@UM"/>	
			</xsl:when>
			<xsl:otherwise>
				1.00
			</xsl:otherwise>
		</xsl:choose>
	</xsl:when>
	
	<xsl:when test="$FACTORELEMENT = 'UIM'"> 
	 
		<xsl:choose>
			<xsl:when test="$PVEHICLETYPE='AA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@UIM"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='CA'">
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@UIM"/>	
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='SA'">			
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@UIM"/>	
			</xsl:when>
			<xsl:otherwise>
				1.00
			</xsl:otherwise>
		</xsl:choose>
   </xsl:when> 
   
   <xsl:when test="$FACTORELEMENT = 'COMP'"> 
   
	 
		<xsl:choose>
			<xsl:when test="$PVEHICLETYPE='AA'">
				<xsl:variable name="VAR1"> 
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@COMP50"/>	
				</xsl:variable>
				
				<xsl:variable name="VAR2">
					<xsl:call-template name ="FETCHDEDUCTIBLEFACTOR">
						<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable> 
				
				<xsl:value-of select="round($VAR1*$VAR2)"/>				
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='CA'">
				<xsl:variable name="VAR1">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@COMP50"/>	
				</xsl:variable> 
				 
				<xsl:variable name="VAR2">
					<xsl:call-template name ="FETCHDEDUCTIBLEFACTOR">
						<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable> 
				<xsl:value-of select="round($VAR1*$VAR2)"/>
				
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='SA'">	
				<xsl:variable name="VAR1">	
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@COMP50"/>	
				</xsl:variable> 
				
				<xsl:variable name="VAR2">
					<xsl:call-template name ="FETCHDEDUCTIBLEFACTOR">
						<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable> 
				
				<xsl:value-of select="round($VAR1*$VAR2)"/>
				
				
			</xsl:when>
			<xsl:otherwise>
				1.00
			</xsl:otherwise>
		</xsl:choose>
	</xsl:when> 
	
	 <xsl:when test="$FACTORELEMENT = 'COLL' or $FACTORELEMENT = 'LTDCOLL'"> 
	 
	 	
	
			<xsl:choose>
				<xsl:when test="$PVEHICLETYPE='AA'">
				<xsl:variable name="VAR1">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_ANTIQUE']/ATTRIBUTES/@COLL250"/>	
				</xsl:variable> 
				
				<xsl:variable name="VAR2">
					<xsl:call-template name ="FETCHDEDUCTIBLEFACTOR">
						<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable> 
				
				<xsl:value-of select="round($VAR1*$VAR2)"/>	
						
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='CA'">
				<xsl:variable name="VAR1">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_CLASSIC']/ATTRIBUTES/@COLL250"/>	
				</xsl:variable> 
				
				<xsl:variable name="VAR2">
					<xsl:call-template name ="FETCHDEDUCTIBLEFACTOR">
						<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable> 
				
				<xsl:value-of select="round($VAR1*$VAR2)"/>	
				
			</xsl:when>
			<xsl:when test="$PVEHICLETYPE='SA'">	
				<xsl:variable name="VAR1">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='BASERATE_STATEDAMOUNT']/ATTRIBUTES/@COLL250"/>	
				</xsl:variable> 
				
				<xsl:variable name="VAR2">
					<xsl:call-template name ="FETCHDEDUCTIBLEFACTOR">
						<xsl:with-param name="FACTOR" select="$FACTORELEMENT"></xsl:with-param>
					</xsl:call-template>
				</xsl:variable> 
				
				<xsl:value-of select="round($VAR1*$VAR2)"/>	
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



<!--  *******************************************************************************************  -->




<xsl:template name="FETCHDEDUCTIBLEFACTOR">
	<xsl:param name="FACTOR" /> <!-- Depending on the factor  BI/PD..etc -->
	 
	<xsl:choose>
		<xsl:when test ="$FACTOR = 'COMP'">
			<xsl:choose>
				<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 50">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE50"/>	
				</xsl:when>
				<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 100">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE100"/>
				</xsl:when>
				<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 150">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE150"/>
				</xsl:when>
				<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 200">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE200"/>
				</xsl:when>
				<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 250">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE250"/>
				</xsl:when>
				<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 500">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE500"/>
				</xsl:when>
				<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 750">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE750"/>
				</xsl:when>
				<xsl:when test="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE = 1000">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COMP']/ATTRIBUTES/@DEDUCTIBLE1000"/>
				</xsl:when>
				<xsl:otherwise>
					1.00
				</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:when test="$FACTOR = 'COLL' or $FACTOR = 'LTDCOLL'">
			<xsl:choose>
				<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 50">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE50"/>	
				</xsl:when>
				<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 100">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE100"/>
				</xsl:when>
				<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 150">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE150"/>
				</xsl:when>
				<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 200">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE200"/>
				</xsl:when>
				<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 250">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE250"/>
				</xsl:when>
				<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 500">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE500"/>
				</xsl:when>
				<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 750">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE750"/>
				</xsl:when>
				<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE = 1000">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='TYPEOFVEHICLE']/NODE[@ID='DEDUCTIBLEFACTOR_COLL']/ATTRIBUTES/@DEDUCTIBLE1000"/>
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
<!-- End of Vehicle Type -->

<!--  ************************************************************************************* -->

<!-- Driver Class -->
<xsl:template name ="CLASS">
	<xsl:param name="FACTORELEMENT" /> <!-- Depending on the factor element BI/PD..etc -->
	<xsl:variable name="PDRIVERCLASS" >
		<xsl:value-of select="VEHICLES/VEHICLE/VEHICLECLASS" />   <!-- need to determine driver class first-->
		 
	</xsl:variable>
 <!--
	<xsl:choose>
		<xsl:when  test="VEHICLES/VEHICLE/VEHICLECLASS ='' or VEHICLES/VEHICLE/VEHICLECLASS">
			1.00
		</xsl:when>
		<xsl:otherwise>
			IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'BI')THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSRELATIVITY']/NODE[@ID='DCR']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'PD')THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSRELATIVITY']/NODE[@ID='DCR']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'PPI')THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSRELATIVITY']/NODE[@ID='DCR']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'PIP')THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSRELATIVITY']/NODE[@ID='DCR']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'UM') THEN 
				1.00
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'UIM') THEN 
				1.00
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'COMP') THEN 
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSCOMPREHENSIVE']/NODE[@ID='DRIVERCLASSCOMP']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'COLL') THEN 
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSCOLLISION']/NODE[@ID='DRIVERCLASSCOLL']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'LTDCOLL') THEN 
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSCOLLISION']/NODE[@ID='DRIVERCLASSCOLL']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY"/>
			ELSE
				1.00
		</xsl:otherwise>	
	</xsl:choose>	
 -->

<!-- new calculation added by praveen singh --> 

 
<xsl:choose>
		<xsl:when  test="VEHICLES/VEHICLE/VEHICLECLASS ='' or VEHICLES/VEHICLE/VEHICLECLASS">
			1.00
		</xsl:when>
		<xsl:otherwise>
			<xsl:choose>
				<xsl:when test="$FACTORELEMENT = 'BI'"> 
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSRELATIVITY']/NODE[@ID='DCR']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY"/>
				</xsl:when>
				<xsl:when test="$FACTORELEMENT = 'PD'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSRELATIVITY']/NODE[@ID='DCR']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY"/>
				</xsl:when>
				<xsl:when test="$FACTORELEMENT = 'PPI'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSRELATIVITY']/NODE[@ID='DCR']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY"/>
				</xsl:when>
				<xsl:when test="$FACTORELEMENT = 'PIP'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSRELATIVITY']/NODE[@ID='DCR']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY"/>
				</xsl:when>
				<xsl:when test="$FACTORELEMENT = 'UM'">1.00</xsl:when>
				<xsl:when test="$FACTORELEMENT = 'UIM'">1.00</xsl:when>
				<xsl:when test="$FACTORELEMENT = 'COMP'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSCOMPREHENSIVE']/NODE[@ID='DRIVERCLASSCOMP']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY"/>
				</xsl:when>
				<xsl:when test="$FACTORELEMENT = 'COLL'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSCOLLISION']/NODE[@ID='DRIVERCLASSCOLL']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY"/>
				</xsl:when>
				<xsl:when test="$FACTORELEMENT = 'LTDCOLL'">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERCLASSCOLLISION']/NODE[@ID='DRIVERCLASSCOLL']/ATTRIBUTES[@CLASS= $PDRIVERCLASS ]/@RELATIVITY"/>
				</xsl:when>
				<xsl:otherwise>1.00</xsl:otherwise>
			</xsl:choose>		
		</xsl:otherwise>	
	</xsl:choose>	
 

	
</xsl:template>

<!-- Limit -->
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
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIMITS']/NODE[@ID= $FACTORELEMENT ]/ATTRIBUTES[@MINCOVERAGE= $COVERAGELL and @MAXCOVERAGE = $COVERAGEUL ]/@RELATIVITY"/>	
				</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:when test = "$FACTORELEMENT='UM'"> <!-- UM -->
			<xsl:variable name="COVERAGELL" select="VEHICLES/VEHICLE/UMSPLITLIMIT1"/> <!-- Lower Limit CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
			<xsl:variable name="COVERAGEUL" select="VEHICLES/VEHICLE/UMSPLITLIMIT2"/> <!-- Upper Limit -->
			<xsl:choose>
				<!-- Check if split limit or combined single limit -->
				<xsl:when test="VEHICLES/VEHICLE/UMCSL !='' and VEHICLES/VEHICLE/UMCSL !='0'"><!-- If combined single limit -->
					<xsl:variable name="PUMCSL" select="VEHICLES/VEHICLE/UMCSL" />
					<xsl:choose>						
						<xsl:when test ="$PVEHICLEROWID &gt; 0 and $PVEHICLEROWID =1">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID ='CSLUMBI']/ATTRIBUTES[@CSLUMBILIMITS = $PUMCSL]/@CSLUMRATES1"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID ='CSLUMBI']/ATTRIBUTES[@CSLUMBILIMITS = $PUMCSL]/@CSLUMRATES2"/>
						</xsl:otherwise>
					</xsl:choose>						
				</xsl:when>
				<xsl:otherwise> <!-- If  split limit -->							
						<xsl:variable name="PMINLIMIT" select="VEHICLES/VEHICLE/UMSPLITLIMIT1"></xsl:variable>
						<xsl:variable name="PMAXLIMIT" select="VEHICLES/VEHICLE/UMSPLITLIMIT2"></xsl:variable>
						<xsl:choose>
							<xsl:when test ="$PVEHICLEROWID &gt; 0 and $PVEHICLEROWID =1">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID ='UMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMIT and @MAXLIMIT=$PMAXLIMIT]/@UMRATE1"/>
							</xsl:when>
							<xsl:when test="$PVEHICLEROWID &gt; 1">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID ='UMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMIT and @MAXLIMIT=$PMAXLIMIT]/@UMRATE2"/>
							</xsl:when>
							<xsl:otherwise>
								1.00
							</xsl:otherwise>
						</xsl:choose>	
				</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:when test = "$FACTORELEMENT ='UM_MICHIGAN_SPLIT'">
			<xsl:variable name="COVERAGELL" select="VEHICLES/VEHICLE/UMSPLITLIMIT1"/> <!-- Lower Limit CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
			<xsl:variable name="COVERAGEUL" select="VEHICLES/VEHICLE/UMSPLITLIMIT2"/> <!-- Upper Limit -->
		 
			<!-- Split limit -->
			<xsl:choose>				 
				<xsl:when test="$COVERAGELL !='' and $COVERAGELL!= '0' and $COVERAGEUL !='' and $COVERAGEUL != '0'">
					<xsl:variable name="PMINLIMITUM" select="VEHICLES/VEHICLE/UMSPLITLIMIT1"></xsl:variable>
					<xsl:variable name="PMAXLIMITUM" select="VEHICLES/VEHICLE/UMSPLITLIMIT2"></xsl:variable>
					<xsl:choose>
							<xsl:when test ="$PVEHICLEROWID &gt; 0 and $PVEHICLEROWID =1">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID ='UMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMITUM and @MAXLIMIT=$PMAXLIMITUM]/@UMRATE1"/>
							</xsl:when>
							<xsl:when test="$PVEHICLEROWID &gt; 1">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID ='UMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMITUM and @MAXLIMIT=$PMAXLIMITUM]/@UMRATE2"/>
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
		
		<xsl:when test = "$FACTORELEMENT ='UM_MICHIGAN_CSL'">
			<xsl:variable name="PCSL" select="VEHICLES/VEHICLE/UMCSL"/>
			<!-- Combined single limit -->
			<xsl:choose>
				<xsl:when test ="$PCSL !='' and $PCSL!='0' and $PCSL!='NO COVERAGE'">
					<xsl:choose>
						<xsl:when test ="$PVEHICLEROWID &gt; 0 and $PVEHICLEROWID =1">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID ='CSLUMBI']/ATTRIBUTES[@CSLUMBILIMITS = $PCSL]/@CSLUMRATES1"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID ='CSLUMBI']/ATTRIBUTES[@CSLUMBILIMITS = $PCSL]/@CSLUMRATES2"/>
						</xsl:otherwise>
					</xsl:choose>	
				</xsl:when>												
				<xsl:otherwise>
					1.00
				</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		
		<xsl:when test = "$FACTORELEMENT ='UM_INDIANA_SPLIT'">
			<xsl:variable name="COVERAGELL" select="VEHICLES/VEHICLE/UMSPLITLIMIT1"/> <!-- Lower Limit CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
			<xsl:variable name="COVERAGEUL" select="VEHICLES/VEHICLE/UMSPLITLIMIT2"/> <!-- Upper Limit -->
		 
			<!-- Split limit -->
			<xsl:choose>				 
				<xsl:when test="$COVERAGELL !='' and $COVERAGELL!= '0' and $COVERAGEUL !='' and $COVERAGEUL != '0'">
					<xsl:variable name="PMINLIMITUM" select="VEHICLES/VEHICLE/UMSPLITLIMIT1"></xsl:variable>
					<xsl:variable name="PMAXLIMITUM" select="VEHICLES/VEHICLE/UMSPLITLIMIT2"></xsl:variable>
					<xsl:choose>
							<xsl:when test ="$PVEHICLEROWID &gt; 0 and $PVEHICLEROWID =1">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID ='UMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMITUM and @MAXLIMIT=$PMAXLIMITUM]/@UMRATE1"/>
							</xsl:when>
							<xsl:when test="$PVEHICLEROWID &gt; 1">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UMBILIMITS']/NODE[@ID ='UMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMITUM and @MAXLIMIT=$PMAXLIMITUM]/@UMRATE2"/>
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
		<xsl:when test = "$FACTORELEMENT ='UM_INDIANA_CSL'">
			<xsl:variable name="PCSL" select="VEHICLES/VEHICLE/UMCSL"/>
			<!-- Combined single limit -->
			<xsl:choose>
				<xsl:when test ="$PCSL !='' and $PCSL!='0' and $PCSL!='NO COVERAGE'">
					<xsl:choose>
						<xsl:when test ="$PVEHICLEROWID &gt; 0 and $PVEHICLEROWID =1">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID ='CSLUMBI']/ATTRIBUTES[@CSLUMBILIMITS = $PCSL]/@CSLUMRATES1"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUMBILIMITS']/NODE[@ID ='CSLUMBI']/ATTRIBUTES[@CSLUMBILIMITS = $PCSL]/@CSLUMRATES2"/>
						</xsl:otherwise>
					</xsl:choose>	
				</xsl:when>												
				<xsl:otherwise>
					1.00
				</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:when test = "$FACTORELEMENT = 'UIM'"> <!-- UIM -->
			<xsl:variable name="COVERAGELL" select="VEHICLES/VEHICLE/UMSPLITLIMIT1"/> <!-- Lower Limit CHECK WITH DEEPAK FOR THE NODE NAME IN INPUTXML-->
			<xsl:variable name="COVERAGEUL" select="VEHICLES/VEHICLE/UMSPLITLIMIT2"/> <!-- Upper Limit -->
			<xsl:variable name="SPLITLIMIT" select="VEHICLES/VEHICLE/UMSPLIT"/> <!-- this value is blank when CSL is selected -->
			<xsl:variable name="PUMCSL" select="VEHICLES/VEHICLE/UMCSL" /><!-- this value is blank when SPLIT is selected -->
			<!--
			<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/ISUNDERINSUREDMOTORISTS ='TRUE'">
							
							<xsl:variable name="PMINLIMIT" select="VEHICLES/VEHICLE/UMSPLITLIMIT1"></xsl:variable>
							<xsl:variable name="PMAXLIMIT" select="VEHICLES/VEHICLE/UMSPLITLIMIT2"></xsl:variable>
						-->
							<!-- If combined single limit -->	
												
							<!--xsl:when test="'$SPLITLIMIT'='' or '$SPLITLIMIT' ='NO COVERAGE' or '$SPLITLIMIT' ='0/0'"-->
						 <!--
							IF ('<xsl:value-of select='$SPLITLIMIT'/>' ='' or '<xsl:value-of select='$SPLITLIMIT'/>' ='NO COVERAGE' or '<xsl:value-of select='$SPLITLIMIT'/>' ='0/0')
							THEN				
								(<xsl:choose>
									<xsl:when test="'$PUMCSL' != '0'">
											<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUIMBILIMITS']/NODE[@ID ='CSLUIMBI']/ATTRIBUTES[@CSLUIMBILIMITS = $PUMCSL]/@CSLUIMRATES"/>
									</xsl:when>
									<xsl:otherwise>0.00</xsl:otherwise>
								</xsl:choose>
								)
							ELSE IF ('<xsl:value-of select='$PUMCSL'/>' ='' or '<xsl:value-of select='$PUMCSL'/>' ='NO COVERAGE' or '<xsl:value-of select='$PUMCSL'/>' ='0')
							THEN
								(	<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UIMBILIMITS']/NODE[@ID ='UIMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMIT and @MAXLIMIT=$PMAXLIMIT]/@UIMRATES"/>)
							ELSE
								0.00
							-->
							<!-- If  split limit >
							<xsl:when test="'$PUMCSL'='' or '$PUMCSL' ='NO COVERAGE' or '$PUMCSL' ='0'">
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UIMBILIMITS']/NODE[@ID ='UIMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMIT and @MAXLIMIT=$PMAXLIMIT]/@UIMRATES"/>
							</xsl:when-->
					<!--	</xsl:when>
						<xsl:otherwise>0.00</xsl:otherwise>
			</xsl:choose>	
			-->
			
			<!-- new calculation added by praveen singh --> 
			<xsl:choose>
					<xsl:when test="VEHICLES/VEHICLE/ISUNDERINSUREDMOTORISTS ='TRUE'">
							
							<xsl:variable name="PMINLIMIT" select="VEHICLES/VEHICLE/UMSPLITLIMIT1"></xsl:variable>
							<xsl:variable name="PMAXLIMIT" select="VEHICLES/VEHICLE/UMSPLITLIMIT2"></xsl:variable>
							<!-- If combined single limit -->	
								<xsl:choose> 			
										<xsl:when test="'$SPLITLIMIT'='' or '$SPLITLIMIT' ='NO COVERAGE' or '$SPLITLIMIT' ='0/0'"> 
						 						<xsl:choose>
													<xsl:when test="'$PUMCSL' != '0'">
															<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='CSLUIMBILIMITS']/NODE[@ID ='CSLUIMBI']/ATTRIBUTES[@CSLUIMBILIMITS = $PUMCSL]/@CSLUIMRATES"/>
													</xsl:when>
													<xsl:otherwise>0.00</xsl:otherwise>
												</xsl:choose>
										</xsl:when>
								
											<!-- If  split limit >  -->
										<xsl:when test="'$PUMCSL'='' or '$PUMCSL' ='NO COVERAGE' or '$PUMCSL' ='0'">
												<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='UIMBILIMITS']/NODE[@ID ='UIMBI']/ATTRIBUTES[@MINLIMIT = $PMINLIMIT and @MAXLIMIT=$PMAXLIMIT]/@UIMRATES"/>
										</xsl:when> 
										<xsl:otherwise>0.00</xsl:otherwise>
								</xsl:choose>		
					</xsl:when>
					<xsl:otherwise>
						0.00
					</xsl:otherwise>
			</xsl:choose>	
			
		</xsl:when>
		 
		<xsl:when test = "$FACTORELEMENT='MEDPAY'"> <!-- MEDPAY limit :indiana -->			
				<xsl:variable name="PMEDICALLIMIT"><xsl:value-of select="VEHICLES/VEHICLE/MEDPM"/></xsl:variable>				
				 
				<xsl:choose>
					<xsl:when test="$PMEDICALLIMIT != '' and $PMEDICALLIMIT &gt; 0">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MEDICALPAYMENTLIMIT']/NODE[@ID = 'MEDPAY']/ATTRIBUTES[@LIMIT=$PMEDICALLIMIT]/@FACTOR"/>
					</xsl:when>
					<xsl:otherwise>
					1.00
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
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='LIMITS']/NODE[@ID = $FACTORELEMENT ]/ATTRIBUTES[@COVERAGE = $COVERAGE ]/@RELATIVITY"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:otherwise>
	</xsl:choose>	

</xsl:template>

 


	 

<!-- Multi-vehicle -->
<xsl:template name ="MULTIVEHICLE">
	<xsl:param name="FACTORELEMENT" /> <!-- Depending on the factor element BI/PD..etc -->
	<xsl:variable name="PDRIVERCLASS" select="DRIVERS/DRIVER[@ID='1']/DRIVERCLASSCOMPONENT1"/>  <!-- ASK RAJAN --> 
<!--	
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/MULTICARDISCOUNT != '' and VEHICLES/VEHICLE/MULTICARDISCOUNT !='FALSE'">
			IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'BI')THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEBI']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'PD')THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEPD']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'PPI')THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEPPI']/ATTRIBUTES[@CLASS  = $PDRIVERCLASS]/@FACTOR"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'PIP')THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEPIP']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'COLL') THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLECOLL']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR"/>
			ELSE IF ('<xsl:value-of select="$FACTORELEMENT"/>' = 'LTDCOLL') THEN 
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLECOLL']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR"/>
			ELSE
				1.00
		</xsl:when>
		<xsl:otherwise>
			1.00
		</xsl:otherwise>
	</xsl:choose>	
-->

<!-- New Calculation added by praveen  Singh -->
<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/MULTICARDISCOUNT != '' and VEHICLES/VEHICLE/MULTICARDISCOUNT !='FALSE'">
			
				<xsl:choose>
					<xsl:when test="$FACTORELEMENT = 'BI'">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEBI']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR"/>
					</xsl:when>
					
					<xsl:when test="$FACTORELEMENT = 'PD'">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEPD']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR"/>
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'PPI'"> 
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEPPI']/ATTRIBUTES[@CLASS  = $PDRIVERCLASS]/@FACTOR"/>
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'PIP'"> 
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLEPIP']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR"/>
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'COLL'"> 
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLECOLL']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR"/>
					</xsl:when>
					<xsl:when test="$FACTORELEMENT = 'LTDCOLL'"> 
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTIVEHICLECOLL']/ATTRIBUTES[@CLASS = $PDRIVERCLASS]/@FACTOR"/>
					</xsl:when>
					 
					
					<xsl:otherwise>1.00</xsl:otherwise>
				</xsl:choose>
			 
		</xsl:when>
		<xsl:otherwise>
			1.00
		</xsl:otherwise>
	</xsl:choose>		
	
	
</xsl:template>


<xsl:template name ="MULTIVEHICLE_DISPLAY">	
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/MULTICARDISCOUNT='TRUE'">
			Included
		</xsl:when>
		<xsl:otherwise>
			0
		</xsl:otherwise>	
	</xsl:choose>
</xsl:template>


<!-- Multicar discount CSL -->
<xsl:template name ="MULTICARDISCOUNTCSL">	 
	 
	<xsl:variable name="PDRIVERCLASS">
		<xsl:value-of select="DRIVERS/DRIVER[@ID='1']/DRIVERCLASS" /><xsl:value-of select="VEHICLES/VEHICLE/VEHICLEUSE" />		
	</xsl:variable>
	<xsl:choose>
		<xsl:when test="$PDRIVERCLASS ='P1' or $PDRIVERCLASS ='P2'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTICARCSL']/ATTRIBUTES[@DRIVERCLASS = $PDRIVERCLASS]/@FACTOR"/>	
		</xsl:when> 
		<xsl:otherwise>
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIVEHICLE']/NODE[@ID='MULTICARCSL']/ATTRIBUTES[@DRIVERCLASS = 'OTHER']/@FACTOR"/>
		</xsl:otherwise>	
	</xsl:choose>
</xsl:template>




<!-- Vehicle Use -->
<xsl:template name ="VEHICLEUSE">	 
	<xsl:variable name="PDRIVERCLASS" select="VEHICLES/VEHICLE/VEHICLECLASSCOMPONENT1"/> 
	<xsl:variable name="PUSECODE" select="VEHICLES/VEHICLE/VEHICLEUSE" /> 
	
	<xsl:choose>
	<xsl:when test="VEHICLES/VEHICLE/VEHICLECLASS !=''">
	<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLEUSERELATIVITY']/NODE[@ID='VEHICLEUSE']/ATTRIBUTES[@USECODE = $PUSECODE and @CLASS = $PDRIVERCLASS]/@FACTOR"/>		
	</xsl:when>
	<xsl:otherwise>1.00</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- Premier or Safe driver discount -->
<xsl:template name ="DRIVERDISCOUNT">
	<!-- 1. Check for Premier or Safe Discount 
		 2. If Premier then call Premier template.
		 3. If Safe then check for Surcharge.
		 4. If Surcharge > 1 then do not call Safe Driver Template ..return 1.00
		 5.			else call SafeDriver template.-->

	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = 'SCO'">
		1.00
		</xsl:when>
		<xsl:when test="DRIVERS/DRIVER[@ID='1']/NOPREMDRIVERDISC='FALSE'">
			<xsl:call-template name ="PREMIERDISCOUNT"/>
		</xsl:when>
		<xsl:when test="DRIVERS/DRIVER[@ID='1']/NOPREMDRIVERDISC='TRUE'">			
				<xsl:variable name="PSURCHARGE">
					<xsl:call-template name="SURCHARGE"/>	 
				</xsl:variable>
			
				<xsl:choose>
					<xsl:when test="$PSURCHARGE &gt; 0">
							<xsl:call-template name ="SAFEDISCOUNT"/>
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


<xsl:template name ="PREMIERDISCOUNT">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@SAFEDRIVERFACTOR"/> 		
</xsl:template>
<xsl:template name ="SAFEDISCOUNT">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@PREMIERDRIVERFACTOR"/> 		
</xsl:template>

<xsl:template name ="DRIVERDISCOUNT_DISPLAY">
	<!-- 1. Check for Premier or Safe Discount 
		 2. If Premier then call Premier template.
		 3. If Safe then check for Surcharge.
		 4. If Surcharge > 1 then do not call Safe Driver Template ..return 1.00
		 5.			else call SafeDriver template.-->

		<xsl:choose>
			<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = 'SCO'">
				0
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="PDRIVERDISCOUNT">
					<xsl:call-template name="DRIVERDISCOUNT"/>	 
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

<xsl:template name ="DRIVERDISCOUNT_CREDIT">
	<!-- 1. Check for Premier or Safe Discount 
		 2. If Premier then call Premier template.
		 3. If Safe then check for Surcharge.
		 4. If Surcharge > 1 then do not call Safe Driver Template ..return 1.00
		 5.			else call SafeDriver template.-->

	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = 'SCO'"></xsl:when>
		<xsl:when test="DRIVERS/DRIVER[@ID='1']/NOPREMDRIVERDISC='FALSE'">
			<xsl:call-template name ="PREMIERDISCOUNT_CREDIT"/>%
		</xsl:when>
		<xsl:when test="DRIVERS/DRIVER[@ID='1']/NOPREMDRIVERDISC='TRUE'">			
				<xsl:variable name="PSURCHARGE">
					<xsl:call-template name="SURCHARGE"/>	 
				</xsl:variable>
			
				<xsl:choose>
					<xsl:when test="$PSURCHARGE &gt; 0">
							<xsl:call-template name ="SAFEDISCOUNT_CREDIT"/>%
					</xsl:when>
					<xsl:otherwise></xsl:otherwise>
				</xsl:choose>
		</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	
	</xsl:choose>
</xsl:template> 
<xsl:template name ="PREMIERDISCOUNT_CREDIT">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@SAFEDRIVERCREDIT"/> 		
</xsl:template>
<xsl:template name ="SAFEDISCOUNT_CREDIT">
		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@PREMIERDRIVERCREDIT"/> 		
</xsl:template>
<!-- End of Premier or Safe driver discount -->

<!-- Good student discount -->
<xsl:template name ="GOODSTUDENT">
	<xsl:choose>
		<xsl:when test="DRIVERS/DRIVER/GOODSTUDENT = 'TRUE'">
			<!-- Check if the surcharge is greater less than 2.-->							
			<xsl:variable name="PSURCHARGE">
				<xsl:call-template name="SURCHARGE"/>	 
			</xsl:variable>
		
			<xsl:choose>	
				<!-- discount applicable only if surcharge is less than 2 -->
				<xsl:when test="$PSURCHARGE &lt; 2">			
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='GOODSTUDENT']/NODE[@ID ='GOODSTUDENTDISCOUNT']/ATTRIBUTES/@FACTOR"/> 		
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

<xsl:template name ="GOODSTUDENT_DISPLAY">
	<xsl:choose>
		<xsl:when test="DRIVERS/DRIVER/GOODSTUDENT = 'TRUE'">
			<!-- Check if the surcharge is greater less than 2.-->							
			<xsl:variable name="PSURCHARGE">
				<xsl:call-template name="SURCHARGE"/>	 
			</xsl:variable>
		
			<!--			
			IF (<xsl:value-of select="$PSURCHARGE"/> &lt; 2.00)
			THEN
				<xsl:value-of select="$PSURCHARGE"/>
			ELSE
				0
			-->
			<!-- new calculation added by praveen singh --> 
			<xsl:choose>
				<xsl:when test="$PSURCHARGE &lt; 2.00">
					<xsl:value-of select="$PSURCHARGE"/>
				</xsl:when>	
				<xsl:otherwise>0</xsl:otherwise>
			</xsl:choose>
			
		</xsl:when>
		<xsl:otherwise>
		0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<!-- Combined Single Limit Relativity -->
<xsl:template name ="CSLRELATIVITY">
		<xsl:variable name="PCSLLIMIT" select="VEHICLES/VEHICLE/CSL"/> <!-- ASK DEEPAK TO GIVE CSL FOR ALL NODES -->
		<xsl:choose>
		<xsl:when test="$PCSLLIMIT &gt; 0">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='COMBINEDSINGLELIMITS']/NODE[@ID ='CSL']/ATTRIBUTES[@CSLLIMIT=$PCSLLIMIT]/@CSLRELATIVITY"/> 		
		</xsl:when>
		<xsl:otherwise>
			1.00
		</xsl:otherwise>
		</xsl:choose>
		
</xsl:template>



<!-- Surcharge -->
<xsl:template name ="SURCHARGE">
	 
	<!-- 1. Get the violation and accident points.
		2. Against each violation/accident point, fetch the surcharge  (After 13 points, calculate the surcharge using "For each additional" value
		3. Add the surcharges
		4. Divide the sum by 100 and add 1 -->
	
	
	<!-- Accident Surcharge  -->
	<xsl:variable name="ACCIDENTPOINTS"><xsl:value-of select="VEHICLES/VEHICLE/SUMOFACCIDENTPOINTS"/></xsl:variable>
	<xsl:variable name="ACCIDENTSURCHARGE">
	<xsl:choose>
		<xsl:when test="$ACCIDENTPOINTS !='' and $ACCIDENTPOINTS !='0' and $ACCIDENTPOINTS &gt; 0">
			<xsl:choose>
				<xsl:when test="$ACCIDENTPOINTS &gt; 13" >
							 <!--
								(
									<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=13]/@SURCHARGE"/> 
								)
								+
								(
									(<xsl:value-of select="$ACCIDENTPOINTS"/> -13)*<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/@FOR_EACH_ADDITIONAL"/> 
								)
								-->
						 <!-- new calculation added by praveen singh -->	
						 
							<xsl:variable name="VAR1"><xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=13]/@SURCHARGE"/> </xsl:variable>		
						<xsl:variable name="VAR2"><xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/@FOR_EACH_ADDITIONAL"/>  </xsl:variable>	
							
							<!-- <xsl:value-of select="$VAR1"/> -->
							
						  <xsl:value-of select="$VAR1 + round(($ACCIDENTPOINTS - 13) * $VAR2)"/>   
						 
						 
						  
				 
										
				</xsl:when>
				<xsl:otherwise>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEACCIDENTPOINTS']/ATTRIBUTES[@POINTS=$ACCIDENTPOINTS]/@SURCHARGE"/> 		
				</xsl:otherwise>			
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
	</xsl:variable> 
	
	<!-- Violation Surcharge -->	
	<xsl:variable name="VIOLATIONPOINTS"><xsl:value-of select="VEHICLES/VEHICLE/SUMOFVIOLATIONPOINTS"/></xsl:variable>
	<xsl:variable name="VIOLATIONSURCHARGE">
		<xsl:choose>
			<xsl:when test="$VIOLATIONPOINTS !='' and $VIOLATIONPOINTS !='0' and $VIOLATIONPOINTS &gt; 0">
				<xsl:choose>
					<xsl:when test="$VIOLATIONPOINTS &gt; 13" > 
									<!--
									(
										<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEVIOLATIONPOINTS']/ATTRIBUTES[@POINTS=13]/@SURCHARGE"/> 
									)
									+
									(
										(<xsl:value-of select="$VIOLATIONPOINTS"/> -13)* <xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEVIOLATIONPOINTS']/@FOR_EACH_ADDITIONAL"/> 
									)
									-->
									
							<!-- new calculation added by praveen singh -->		
							<xsl:variable name="VAR1"><xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEVIOLATIONPOINTS']/ATTRIBUTES[@POINTS=13]/@SURCHARGE"/>  </xsl:variable>		
						   <xsl:variable name="VAR2"><xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEVIOLATIONPOINTS']/@FOR_EACH_ADDITIONAL"/>   </xsl:variable>	
							 
								 
					     <xsl:value-of select="$VAR1 + round(($VIOLATIONPOINTS - 13) * $VAR2)"/>  
						 
						 
											
					</xsl:when>
					<xsl:otherwise>
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SURCHARGECONVERSION']/NODE[@ID ='SURCHARGEVIOLATIONPOINTS']/ATTRIBUTES[@POINTS=$VIOLATIONPOINTS]/@SURCHARGE"/>
					</xsl:otherwise>			
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>0.00</xsl:otherwise>
	</xsl:choose>
	</xsl:variable>
	<!-- 
	(
		(
			(<xsl:value-of select="$ACCIDENTSURCHARGE"/>)
			+	
			(<xsl:value-of select="$VIOLATIONSURCHARGE"/>)
		)
		DIV 100.00
	)
	+ 1.00
   -->
   
   <!-- new calculation added by praveen singh --> 
   <xsl:value-of select="round(($ACCIDENTSURCHARGE+$VIOLATIONSURCHARGE) div 100.00) + 1.00"/>
     
</xsl:template>




<!--  Multi Policy discount  -->
<xsl:template name ="MULTIPOLICY">
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = 'SCO'">
		1.00
		</xsl:when>		
		<xsl:when test="POLICY/MULTIPOLICYAUTOHOMEDISCOUNT = 'TRUE' or POLICY/MULTIPOLICYAUTOHOMEDISCOUNT = 'Y'">
			<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='MULTIPOLICY']/NODE[@ID ='MULTIPOLICYDISCOUNT']/ATTRIBUTES/@FACTOR"/> 		
		</xsl:when>
		<xsl:otherwise>
		1.00
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<xsl:template name ="MULTIPOLICY_DISPLAY">
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/VEHICLETYPE = 'SCO'">
		0
		</xsl:when>
		<xsl:when test="POLICY/MULTIPOLICYAUTOHOMEDISCOUNT = 'TRUE' or POLICY/MULTIPOLICYAUTOHOMEDISCOUNT = 'Y'">
			Included
		</xsl:when>
		<xsl:otherwise>
			0
		</xsl:otherwise>
	</xsl:choose>

</xsl:template>

<!-- Coverage Type discount  -->
<xsl:template name ="COVERAGETYPE">
	<xsl:variable name="PPIPCOVERAGECODE" select ="VEHICLES/VEHICLE/MEDPM"></xsl:variable>
	<xsl:variable name="PDRIVERINCOME" select ="VEHICLES/VEHICLE/DRIVERINCOME"></xsl:variable>
	<xsl:variable name="PNODEPENDENT" select ="VEHICLES/VEHICLE/DEPENDENTS"></xsl:variable>
	<xsl:variable name="PWAIVEWORKLOSS" select ="VEHICLES/VEHICLE/WAIVEWORKLOSS"></xsl:variable>
	
	
<!--	(
	<xsl:choose>
		<xsl:when test="$PPIPCOVERAGECODE = 'FULL'">   Full PIP  
			  Full PIP ,Waiver of Work Loss, No dependents  
			IF ('<xsl:value-of select="$PWAIVEWORKLOSS"/>' = 'TRUE' and  ('<xsl:value-of select="$PNODEPENDENT"/>' ='' or  '<xsl:value-of select="$PNODEPENDENT"/>' ='NDEP' or  '<xsl:value-of select="$PNODEPENDENT"/>' = '0'))
			THEN
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C6' ]/@RELATIVITY"/>
			
			  Full PIP , No dependents, Low Income  
			ELSE IF (('<xsl:value-of select="$PNODEPENDENT"/>' ='' or  '<xsl:value-of select="$PNODEPENDENT"/>' ='NDEP' or '<xsl:value-of select="$PNODEPENDENT"/>' = '0') and '<xsl:value-of select="$PDRIVERINCOME"/>' = 'LOW')
			THEN
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C5' ]/@RELATIVITY"/>
			
			  Full PIP , Waiver of work loss  
			ELSE IF ('<xsl:value-of select="$PWAIVEWORKLOSS"/>' = 'TRUE')
			THEN
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C4' ]/@RELATIVITY"/>

			  Full PIP , Low Income  
			ELSE IF('<xsl:value-of select="$PDRIVERINCOME"/>'='LOW')
			THEN
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C3' ]/@RELATIVITY"/>

			  Full PIP ,No dependents  
			ELSE IF('<xsl:value-of select="$PNODEPENDENT"/>' ='' or  '<xsl:value-of select="$PNODEPENDENT"/>' ='NDEP' or  '<xsl:value-of select="$PNODEPENDENT"/>' = '0') 
			THEN
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C2' ]/@RELATIVITY"/>
			  Full PIP  		
			ELSE IF ('<xsl:value-of select="$PWAIVEWORKLOSS"/>' != 'TRUE' and  ('<xsl:value-of select="$PNODEPENDENT"/>' ='1MORE') and '<xsl:value-of select="$PDRIVERINCOME"/>' !='LOW')
			THEN
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C1' ]/@RELATIVITY"/>
			ELSE 
				1.00						
		</xsl:when>
		<xsl:when test="$PPIPCOVERAGECODE = 'EXCESSWAGE'">
			  Excess Workloss  means - Excess Wage was selected. It has no link with the Waiver of Workloss 
										- Full PIP is included  
			
			  Full PIP and Excess Workloss, No dependents, Low Income  
			IF (('<xsl:value-of select="$PNODEPENDENT"/>' ='' or  '<xsl:value-of select="$PNODEPENDENT"/>' ='NDEP' or  '<xsl:value-of select="$PNODEPENDENT"/>' = '0') and '<xsl:value-of select="$PDRIVERINCOME"/>'='LOW')
			THEN
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C10' ]/@RELATIVITY"/>
			
			  Full PIP and Excess Workloss, No dependents  
			ELSE IF ('<xsl:value-of select="$PNODEPENDENT"/>' ='' or  '<xsl:value-of select="$PNODEPENDENT"/>' ='NDEP' or  '<xsl:value-of select="$PNODEPENDENT"/>' = '0' )
			THEN
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C9' ]/@RELATIVITY"/>
			
			  Full PIP and Excess Workloss , Low Income  
			ELSE IF ('<xsl:value-of select="$PDRIVERINCOME"/>' = 'LOW')
			THEN
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C8' ]/@RELATIVITY"/>

			  Full PIP and Excess Workloss  
			ELSE IF ('<xsl:value-of select="$PWAIVEWORKLOSS"/>' != 'TRUE' and  '<xsl:value-of select="$PNODEPENDENT"/>' ='1MORE'  and '<xsl:value-of select="$PDRIVERINCOME"/>' ='HIGH')
			THEN
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID ='C7']/@RELATIVITY"/>
			ELSE 
				1.00						
		
		</xsl:when>
		<xsl:when test="$PPIPCOVERAGECODE = 'EXCESSMEDICAL'">
		      Excess Medical, No dependents ,Waiver of Work Loss  
			IF ( ('<xsl:value-of select="$PNODEPENDENT"/>' ='' or  '<xsl:value-of select="$PNODEPENDENT"/>' ='NDEP' or  '<xsl:value-of select="$PNODEPENDENT"/>' = '0') and '<xsl:value-of select="$PWAIVEWORKLOSS"/>' = 'TRUE')
			THEN
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C20' ]/@RELATIVITY"/>
			
			 Excess Medical, No dependents, Low Income 
			ELSE IF (('<xsl:value-of select="$PNODEPENDENT"/>' ='' or  '<xsl:value-of select="$PNODEPENDENT"/>' ='NDEP' or  '<xsl:value-of select="$PNODEPENDENT"/>' = '0') and '<xsl:value-of select="$PDRIVERINCOME"/>' = 'LOW')
			THEN
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C15' ]/@RELATIVITY"/>
			
			 Excess Medical, Waiver of work loss 
			ELSE IF ('<xsl:value-of select="$PWAIVEWORKLOSS"/>' = 'TRUE')
			THEN
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C14' ]/@RELATIVITY"/>

			 Excess Medical,Low Income 
			ELSE IF('<xsl:value-of select="$PDRIVERINCOME"/>'='LOW')
			THEN
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C13' ]/@RELATIVITY"/>

			 Excess Medical, No dependents 
			ELSE IF('<xsl:value-of select="$PNODEPENDENT"/>' ='' or  '<xsl:value-of select="$PNODEPENDENT"/>' ='NDEP' or '<xsl:value-of select="$PNODEPENDENT"/>' = '0') 
			THEN
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C12' ]/@RELATIVITY"/>
			 Excess Medical 		
			ELSE IF ('<xsl:value-of select="$PWAIVEWORKLOSS"/>' != 'TRUE' and  '<xsl:value-of select="$PNODEPENDENT"/>' ='1MORE' and '<xsl:value-of select="$PDRIVERINCOME"/>' ='HIGH')
			THEN
				<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C11' ]/@RELATIVITY"/>
			ELSE 
				1.00					
		
		</xsl:when>
		<xsl:when test="$PPIPCOVERAGECODE = 'EXCESSBOTH'">
		 Excess Both is for Excess Wage/Medical 
			 Excess Workloss  means - Excess Wage was selected. It has no link with the Waiver of Workloss 
					
			 Excess Medical and Excess Workloss, No dependents, Low Income 
			IF (('<xsl:value-of select="$PNODEPENDENT"/>' ='' or  '<xsl:value-of select="$PNODEPENDENT"/>' ='NDEP' or  '<xsl:value-of select="$PNODEPENDENT"/>' = '0') and '<xsl:value-of select="$PDRIVERINCOME"/>'='LOW')
			THEN
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C10' ]/@RELATIVITY"/>
			
			 Excess Medical and Excess Workloss, No dependents 
			ELSE IF (('<xsl:value-of select="$PNODEPENDENT"/>' ='' or  '<xsl:value-of select="$PNODEPENDENT"/>' ='NDEP' or  '<xsl:value-of select="$PNODEPENDENT"/>' = '0') )
			THEN
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C9' ]/@RELATIVITY"/>
			
			 Excess Medical and Excess Workloss , Low Income 
			ELSE IF ('<xsl:value-of select="$PDRIVERINCOME"/>' = 'LOW')
			THEN
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C8' ]/@RELATIVITY"/>

			 Excess Medical and Excess Workloss 
			ELSE IF('<xsl:value-of select="$PDRIVERINCOME"/>' !='LOW' and ('<xsl:value-of select="$PNODEPENDENT"/>' !='' or  '<xsl:value-of select="$PNODEPENDENT"/>' ='NDEP' or  '<xsl:value-of select="$PNODEPENDENT"/>' != '0'))
			THEN
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C7' ]/@RELATIVITY"/>
			ELSE 
				1.00						
		
		</xsl:when>
		<xsl:otherwise>
			1.00
		</xsl:otherwise>
	</xsl:choose>)
-->

<xsl:choose>
		<xsl:when test="$PPIPCOVERAGECODE = 'FULL'">    
			<!-- Full PIP ,Waiver of Work Loss, No dependents -->
			<xsl:choose> 
			<xsl:when test="$PWAIVEWORKLOSS = 'TRUE' and ($PNODEPENDENT= '' or $PNODEPENDENT='NDEP' or $PNODEPENDENT='0')"> 
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C6' ]/@RELATIVITY"/>
			</xsl:when>
			
			<!-- Full PIP , No dependents, Low Income -->
			<xsl:when test="($PNODEPENDENT = '' or  $PNODEPENDENT = 'NDEP' or $PNODEPENDENT = '0') and $PDRIVERINCOME = 'LOW'"> 
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C5' ]/@RELATIVITY"/>
			</xsl:when>
			
			<!-- Full PIP , Waiver of work loss -->
			<xsl:when test="$PWAIVEWORKLOSS = 'TRUE'"> 
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C4' ]/@RELATIVITY"/>
			</xsl:when>
			
			<!-- Full PIP , Low Income -->
			<xsl:when test="$PDRIVERINCOME = 'LOW'"> 
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C3' ]/@RELATIVITY"/>
			</xsl:when>
			<!-- Full PIP ,No dependents -->
			
			<xsl:when test="$PNODEPENDENT = '' or $PNODEPENDENT= 'NDEP' or $PNODEPENDENT='0'"> 
			 		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C2' ]/@RELATIVITY"/>
			</xsl:when>
			<!-- Full PIP -->
				<xsl:when test="$PWAIVEWORKLOSS != 'TRUE' and $PNODEPENDENT='1MORE' and $PDRIVERINCOME != 'LOW'"> 
			 		<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C1' ]/@RELATIVITY"/>
			</xsl:when> 	
				<xsl:otherwise>1.00</xsl:otherwise>
				
		
		</xsl:choose>
										
		</xsl:when>
		<xsl:when test="$PPIPCOVERAGECODE = 'EXCESSWAGE'">
			<!-- Excess Workloss  means - Excess Wage was selected. It has no link with the Waiver of Workloss 
										- Full PIP is included -->
			
			<!-- Full PIP and Excess Workloss, No dependents, Low Income -->
			<xsl:choose> 
					<xsl:when test="($PNODEPENDENT='' or $PNODEPENDENT='NDEP' or $PNODEPENDENT='0') and $PDRIVERINCOME='LOW'"> 
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C10' ]/@RELATIVITY"/>
					</xsl:when>
					
					
					<!-- Full PIP and Excess Workloss, No dependents -->
					<xsl:when test="$PNODEPENDENT='' or $PNODEPENDENT='NDEP' or $PNODEPENDENT='0'"> 
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C9' ]/@RELATIVITY"/>
					</xsl:when>
					
					<!-- Full PIP and Excess Workloss , Low Income -->
					<xsl:when test="$PDRIVERINCOME = 'LOW'"> 
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C8' ]/@RELATIVITY"/>
					</xsl:when>
					
					<!-- Full PIP and Excess Workloss -->
					
					<xsl:when test="$PWAIVEWORKLOSS !='TRUE' and $PNODEPENDENT='1MORE' and $PDRIVERINCOME='HIGH'"> 
		 					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID ='C7']/@RELATIVITY"/>
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
					 
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C20' ]/@RELATIVITY"/>
					</xsl:when>
					<!-- Excess Medical, No dependents, Low Income -->
					
					<xsl:when test="($PNODEPENDENT='' or $PNODEPENDENT='NDEP' or $PNODEPENDENT='0') and $PDRIVERINCOME='LOW'"> 
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C15' ]/@RELATIVITY"/>
					</xsl:when>
					<!-- Excess Medical, Waiver of work loss -->
					
					<xsl:when test="$PWAIVEWORKLOSS = 'TRUE'">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C14' ]/@RELATIVITY"/>
					</xsl:when>
					
					<!-- Excess Medical,Low Income -->
					<xsl:when test="$PDRIVERINCOME = 'LOW'">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C13' ]/@RELATIVITY"/>
					</xsl:when>
					
					<!-- Excess Medical, No dependents -->
					<xsl:when test="$PNODEPENDENT='' or $PNODEPENDENT='NDEP' or $PNODEPENDENT='0'"> 
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C12' ]/@RELATIVITY"/>
					</xsl:when>
					<!-- Excess Medical -->		
					<xsl:when test="$PWAIVEWORKLOSS !='TRUE' and $PNODEPENDENT='1MORE' and $PDRIVERINCOME='HIGH'"> 
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C11' ]/@RELATIVITY"/>
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
					<xsl:when test="($PNODEPENDENT='' or $PNODEPENDENT='NDEP' or $PNODEPENDENT='0') and $PDRIVERINCOME='LOW'"> 
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C10' ]/@RELATIVITY"/>
					</xsl:when>
					
					<!-- Excess Medical and Excess Workloss, No dependents -->
					<xsl:when test="$PNODEPENDENT='' or $PNODEPENDENT='NDEP' or $PNODEPENDENT='0'"> 						
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C9' ]/@RELATIVITY"/>
					</xsl:when>
					
					<!-- Excess Medical and Excess Workloss , Low Income -->
					<xsl:when test="$PDRIVERINCOME = 'LOW'">
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C8' ]/@RELATIVITY"/>
					</xsl:when>	
					<!-- Excess Medical and Excess Workloss -->
					<xsl:when test="$PDRIVERINCOME = 'LOW' and ($PNODEPENDENT !='' or $PNODEPENDENT='NDEP' or $PNODEPENDENT != '0')">
					 
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='PIPCOVERAGE']/NODE[@ID='COVERAGE']/ATTRIBUTES[@ID = 'C7' ]/@RELATIVITY"/>
					</xsl:when>	
					<xsl:otherwise>1.00 </xsl:otherwise>
										
		</xsl:choose>
		
		</xsl:when>
		<xsl:otherwise>
			1.00
		</xsl:otherwise>
	</xsl:choose> 


</xsl:template>




<xsl:template name ="COVERAGETYPE_DISPLAY">
	<xsl:variable name="PPIPCOVERAGECODE" select ="VEHICLES/VEHICLE/MEDPM"></xsl:variable>
	<xsl:variable name="PDRIVERINCOME" select ="VEHICLES/VEHICLE/DRIVERINCOME"></xsl:variable>
	<xsl:variable name="PNODEPENDENT" select ="VEHICLES/VEHICLE/DEPENDENTS"></xsl:variable>
	<xsl:variable name="PWAIVEWORKLOSS" select ="VEHICLES/VEHICLE/WAIVEWORKLOSS"></xsl:variable>
	
	
	
	<xsl:choose>
		<xsl:when test="$PPIPCOVERAGECODE = 'FULL'"> <!-- Full PIP -->
			
			<!-- Full PIP ,Waiver of Work Loss, No dependents -->
			IF ('<xsl:value-of select="$PWAIVEWORKLOSS"/>' = 'TRUE' and  ('<xsl:value-of select="$PNODEPENDENT"/>' ='' or  '<xsl:value-of select="$PNODEPENDENT"/>' ='NDEP' or  '<xsl:value-of select="$PNODEPENDENT"/>' = '0'))
			THEN  'Discount:Work Loss Waiver, No dependents (PIP)'
			<!-- Full PIP , No dependents, Low Income -->
			ELSE IF (('<xsl:value-of select="$PNODEPENDENT"/>' ='' or  '<xsl:value-of select="$PNODEPENDENT"/>' ='NDEP' or  '<xsl:value-of select="$PNODEPENDENT"/>' = '0') and '<xsl:value-of select="$PDRIVERINCOME"/>' = 'LOW')
			THEN  'Discount:No dependents, Low Income(PIP)'
			
			<!-- Full PIP , Waiver of work loss -->
			ELSE IF ('<xsl:value-of select="$PWAIVEWORKLOSS"/>' = 'TRUE')
			THEN
				'Discount:Work Loss Waiver(PIP)'

			<!-- Full PIP , Low Income -->
			ELSE IF('<xsl:value-of select="$PDRIVERINCOME"/>'='LOW')
			THEN
				'Discount:Low Income(PIP)'

			<!-- Full PIP ,No dependents -->
			ELSE IF('<xsl:value-of select="$PNODEPENDENT"/>' ='' or  '<xsl:value-of select="$PNODEPENDENT"/>' ='NDEP' or  '<xsl:value-of select="$PNODEPENDENT"/>' = '0') 
			THEN
				'Discount: No dependents (PIP)'
			<!-- Full PIP -->		
			ELSE IF ('<xsl:value-of select="$PWAIVEWORKLOSS"/>' != 'TRUE' and  ('<xsl:value-of select="$PNODEPENDENT"/>' = '1MORE') and '<xsl:value-of select="$PDRIVERINCOME"/>' !='LOW')
			THEN
				'0'
			ELSE 
				'0'
		</xsl:when>
		<xsl:when test="$PPIPCOVERAGECODE = 'EXCESSWAGE'">
			<!-- Excess Workloss  means - Excess Wage was selected. It has no link with the Waiver of Workloss 
										- Full PIP is included -->
			
			<!-- Full PIP and Excess Workloss, No dependents, Low Income -->
			IF (('<xsl:value-of select="$PNODEPENDENT"/>' ='' or  '<xsl:value-of select="$PNODEPENDENT"/>' ='NDEP' or  '<xsl:value-of select="$PNODEPENDENT"/>' = '0') and '<xsl:value-of select="$PDRIVERINCOME"/>'='LOW')
			THEN
				'Discount: No dependents, Low Income (PIP)'
			
			<!-- Full PIP and Excess Workloss, No dependents -->
			ELSE IF ('<xsl:value-of select="$PNODEPENDENT"/>' ='' or  '<xsl:value-of select="$PNODEPENDENT"/>' ='NDEP' or  '<xsl:value-of select="$PNODEPENDENT"/>' = '0' )
			THEN
				'Discount: No dependents(PIP)'
			
			<!-- Full PIP and Excess Workloss , Low Income -->
			ELSE IF ('<xsl:value-of select="$PDRIVERINCOME"/>' = 'LOW')
			THEN
				'Discount: Low Income (PIP)'

			<!-- Full PIP and Excess Workloss -->
			ELSE IF ('<xsl:value-of select="$PWAIVEWORKLOSS"/>' != 'TRUE' and  '<xsl:value-of select="$PNODEPENDENT"/>' ='1MORE'  and '<xsl:value-of select="$PDRIVERINCOME"/>' ='HIGH')
			THEN
				'0'
			ELSE 
				'0'						
		
		</xsl:when>
		<xsl:when test="$PPIPCOVERAGECODE = 'EXCESSMEDICAL'">
			<!-- Excess Medical, No dependents ,Waiver of Work Loss -->
			IF ( ('<xsl:value-of select="$PNODEPENDENT"/>' ='' or  '<xsl:value-of select="$PNODEPENDENT"/>' ='NDEP' or  '<xsl:value-of select="$PNODEPENDENT"/>' = '0') and '<xsl:value-of select="$PWAIVEWORKLOSS"/>' = 'TRUE')
			THEN  'Discount: No dependents ,Work Loss Waiver(PIP)'
			
			<!-- Excess Medical, No dependents, Low Income -->
			ELSE IF (('<xsl:value-of select="$PNODEPENDENT"/>' ='' or  '<xsl:value-of select="$PNODEPENDENT"/>' ='NDEP' or  '<xsl:value-of select="$PNODEPENDENT"/>' = '0') and '<xsl:value-of select="$PDRIVERINCOME"/>' = 'LOW')
			THEN  'Discount: No dependents, Low Income (PIP)'
			
			<!-- Excess Medical, Waiver of work loss -->
			ELSE IF ('<xsl:value-of select="$PWAIVEWORKLOSS"/>' = 'TRUE')
			THEN  'Discount: Work Loss Waiver (PIP)'

			<!-- Excess Medical,Low Income -->
			ELSE IF('<xsl:value-of select="$PDRIVERINCOME"/>'='LOW')
			THEN 'Discount: Low Income (PIP)'

			<!-- Excess Medical, No dependents -->
			ELSE IF('<xsl:value-of select="$PNODEPENDENT"/>' ='' or  '<xsl:value-of select="$PNODEPENDENT"/>' ='NDEP' or  '<xsl:value-of select="$PNODEPENDENT"/>' = '0') 
			THEN 'Discount: No dependents (PIP)'
			<!-- Excess Medical -->		
			ELSE IF ('<xsl:value-of select="$PWAIVEWORKLOSS"/>' != 'TRUE' and  ('<xsl:value-of select="$PNODEPENDENT"/>' ='1MORE') and '<xsl:value-of select="$PDRIVERINCOME"/>' ='HIGH')
			THEN  '0'
			ELSE '0'
		
		</xsl:when>
		<xsl:when test="$PPIPCOVERAGECODE = 'EXCESSBOTH'">
		<!-- Excess Both is for Excess Wage/Medical 
			 Excess Workloss  means - Excess Wage was selected. It has no link with the Waiver of Workloss -->
			
			<!-- Excess Medical and Excess Workloss, No dependents, Low Income -->
			IF (('<xsl:value-of select="$PNODEPENDENT"/>' ='' or  '<xsl:value-of select="$PNODEPENDENT"/>' ='NDEP' or  '<xsl:value-of select="$PNODEPENDENT"/>' = '0') and '<xsl:value-of select="$PDRIVERINCOME"/>'='LOW')
			THEN
				'Discount: No dependents, Low Income (PIP)'
			
			<!-- Excess Medical and Excess Workloss, No dependents -->
			ELSE IF (('<xsl:value-of select="$PNODEPENDENT"/>' ='' or  '<xsl:value-of select="$PNODEPENDENT"/>' ='NDEP' or  '<xsl:value-of select="$PNODEPENDENT"/>' = '0') )
			THEN
				'Discount: No dependents (PIP)'
			
			<!-- Excess Medical and Excess Workloss , Low Income -->
			ELSE IF ('<xsl:value-of select="$PDRIVERINCOME"/>' = 'LOW')
			THEN
				'Discount: Low Income (PIP)'

			<!-- Excess Medical and Excess Workloss -->
			ELSE IF('<xsl:value-of select="$PDRIVERINCOME"/>' !='LOW' and ('<xsl:value-of select="$PNODEPENDENT"/>' ='1MORE' ))
			THEN
					'0'
			ELSE 
				'0'						
		
		</xsl:when>
		<xsl:otherwise>
			0
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- Symbol-->
<xsl:template name ="SYMBOL">
	<xsl:param name="FACTORELEMENT" />
	<!--<xsl:variable name="PAGE" select="VEHICLES/VEHICLE/AGE"></xsl:variable>  select="AGE" check with deepak -->
	
	<xsl:variable name="PYEAR" select="VEHICLES/VEHICLE/YEAR"></xsl:variable>
	<xsl:variable name="PSYMBOL" select="VEHICLES/VEHICLE/SYMBOL"></xsl:variable>
	<xsl:choose>
		<xsl:when test="$PYEAR !='' and $PSYMBOL !='' ">
			<xsl:choose>
				<xsl:when test="$FACTORELEMENT='COMP'">
					<!--IF (<xsl:value-of select="$PYEAR"/> &gt;= 1990)
					THEN
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SYMBOLRELATIVITYOTHERTHANCOLLISON']/NODE[@ID='SYMBOLRELATIVITYOTHERTHANCOLL']/ATTRIBUTES[@NINETYPLUS = $PSYMBOL]/@RELATIVITY"/>
					ELSE
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SYMBOLRELATIVITYOTHERTHANCOLLISON']/NODE[@ID='SYMBOLRELATIVITYOTHERTHANCOLL']/ATTRIBUTES[@EIGHTYNINEANDPRIOR = $PSYMBOL]/@RELATIVITY"/>
				  -->
				  <!-- new calculation added by praveen singh -->
				  <xsl:choose>
					<xsl:when test="$PYEAR &gt; 1900">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SYMBOLRELATIVITYOTHERTHANCOLLISON']/NODE[@ID='SYMBOLRELATIVITYOTHERTHANCOLL']/ATTRIBUTES[@NINETYPLUS = $PSYMBOL]/@RELATIVITY"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SYMBOLRELATIVITYOTHERTHANCOLLISON']/NODE[@ID='SYMBOLRELATIVITYOTHERTHANCOLL']/ATTRIBUTES[@EIGHTYNINEANDPRIOR = $PSYMBOL]/@RELATIVITY"/>
					</xsl:otherwise>
				  </xsl:choose>
				  
				</xsl:when>
				<xsl:when test="$FACTORELEMENT = 'COLL' or $FACTORELEMENT = 'LTDCOLL' ">
				<!--	IF (<xsl:value-of select="$PYEAR"/> &gt;= 1990)
					THEN
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SYMBOLRELATIVITYCOLLISION']/NODE[@ID='SYMBOLRELATIVITYCOLL']/ATTRIBUTES[@NINETYPLUS = $PSYMBOL ]/@RELATIVITY"/>
					ELSE
							<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SYMBOLRELATIVITYCOLLISION']/NODE[@ID='SYMBOLRELATIVITYCOLL']/ATTRIBUTES[@EIGHTYNINEANDPRIOR = $PSYMBOL ]/@RELATIVITY"/>
				-->
				 <!-- new calculation added by praveen singh -->
						<xsl:choose>
							<xsl:when test="$PYEAR &gt; 1900">
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SYMBOLRELATIVITYCOLLISION']/NODE[@ID='SYMBOLRELATIVITYCOLL']/ATTRIBUTES[@NINETYPLUS = $PSYMBOL ]/@RELATIVITY"/>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='SYMBOLRELATIVITYCOLLISION']/NODE[@ID='SYMBOLRELATIVITYCOLL']/ATTRIBUTES[@EIGHTYNINEANDPRIOR = $PSYMBOL ]/@RELATIVITY"/>
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
</xsl:template>

<!-- Deductible -->
<xsl:template name ="DEDUCTIBLE">
	<xsl:param name="FACTORELEMENT" />
	
	<xsl:choose>		
		<xsl:when test="$FACTORELEMENT = 'COMP'">
			<xsl:variable name="PCOMPREHENSIVEDEDUCTIBLE" select="VEHICLES/VEHICLE/COMPREHENSIVEDEDUCTIBLE"/>
			<xsl:choose>
				<xsl:when test="$PCOMPREHENSIVEDEDUCTIBLE !=''">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLEO']/NODE[@ID='DEDTBLO']/ATTRIBUTES[@DEDUCTIBLE = $PCOMPREHENSIVEDEDUCTIBLE ]/@RELATIVITY"/>	
				</xsl:when>
				<xsl:otherwise>
					0.00
				</xsl:otherwise>			
			</xsl:choose>
		</xsl:when>		
		<xsl:when test="$FACTORELEMENT = 'COLL' and POLICY/STATENAME ='MICHIGAN'">
			<xsl:variable name="PCOLLISIONTYPE" select="VEHICLES/VEHICLE/COVGCOLLISIONTYPE"/>
			<xsl:variable name="PCOVGCOLLISIONDEDUCTIBLE" select="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE"/>
				<xsl:choose>
					<xsl:when test="$PCOLLISIONTYPE !='' and $PCOVGCOLLISIONDEDUCTIBLE !=''">
						<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@COLLISIONTYPE = $PCOLLISIONTYPE and @DEDUCTIBLE = $PCOVGCOLLISIONDEDUCTIBLE ]/@RELATIVITY"/>		
					</xsl:when>
					<xsl:otherwise>
						0.00
					</xsl:otherwise>
				</xsl:choose>				 
		</xsl:when>
		<xsl:when test="$FACTORELEMENT = 'COLL' and POLICY/STATENAME ='INDIANA'">			 
			
			<xsl:variable name="PCOVGCOLLISIONDEDUCTIBLE" select="VEHICLES/VEHICLE/COVGCOLLISIONDEDUCTIBLE"/>
			<xsl:choose>
				<xsl:when test="$PCOVGCOLLISIONDEDUCTIBLE !='' and $PCOVGCOLLISIONDEDUCTIBLE &gt; 0">
					<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@DEDUCTIBLE = $PCOVGCOLLISIONDEDUCTIBLE ]/@RELATIVITY"/>		
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
<xsl:template name ="LIMITFACTOR">	
	<xsl:variable name="PCOLLISIONTYPE" select="VEHICLES/VEHICLE/COLLISIONTYPE"/>	
	<xsl:value-of select="$HOCoveragesDoc/PRODUCTMASTER/PRODUCT/FACTOR[@ID='DEDUCTIBLECOLLISION']/NODE[@ID='DEDUCTIBLECOLL']/ATTRIBUTES[@COLLISIONTYPE = $PCOLLISIONTYPE ]/@RELATIVITY"/>		
</xsl:template>

<!-- End of Deductibles -->


<!-- Stated Amount -->
<xsl:template name ="STATEDAMT">

1
</xsl:template>


<!-- Fiberglass or Inner Shield -->
<xsl:template name ="FIBERGLASS">
1

</xsl:template>


<!-- End of templates-->



<!--  Template for Lables   -->
<!--  Group Details  -->       
<xsl:template name ="GROUPID0">VEHICLES</xsl:template>
<xsl:template name ="GROUPID1">Final Premium</xsl:template>
<xsl:template name ="PRODUCTNAME">Private Passenger Automobile</xsl:template>
<xsl:template name = "STEPID0"><xsl:value-of select="VEHICLES/VEHICLE/VEHICLEROWID"/>.<xsl:value-of select ="'   '"/><xsl:value-of select="VEHICLES/VEHICLE/YEAR"/> <xsl:value-of select ="'   '"/>  <xsl:value-of select="VEHICLES/VEHICLE/MAKE"/> <xsl:value-of select ="'   '"/>   <xsl:value-of select="VEHICLES/VEHICLE/MODEL"/> <xsl:value-of select ="'   '"/>    VIN:<xsl:value-of select="VEHICLES/VEHICLE/VIN"/></xsl:template>
<xsl:template name = "STEPID1">Symbol <xsl:value-of select="VEHICLES/VEHICLE/SYMBOL"/>,<xsl:value-of select ="'   '"/> 
								Class <xsl:value-of select="VEHICLES/VEHICLE/VEHICLECLASS" /> 
										<xsl:choose>
											<xsl:when test="VEHICLES/VEHICLE/VEHICLEUSE ='' or VEHICLES/VEHICLE/VEHICLEUSE='O' or VEHICLES/VEHICLE/VEHICLEUSE ='o'"></xsl:when>
											<xsl:otherwise><xsl:value-of select="VEHICLES/VEHICLE/VEHICLEUSE" /></xsl:otherwise>
										</xsl:choose>
										
										</xsl:template>
<xsl:template name = "STEPID2">Use: <xsl:value-of select="VEHICLES/VEHICLE/VEHICLEUSEDESC"/> , <xsl:value-of select ="'   '"/>Miles Each Way : <xsl:value-of select="VEHICLES/VEHICLE/MILESEACHWAY"/></xsl:template>
<xsl:template name = "STEPID3">
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/ZIPCODEGARAGEDLOCATION !='' and VEHICLES/VEHICLE/ZIPCODEGARAGEDLOCATION !='0'">
			Garaged Location: <xsl:value-of select="VEHICLES/VEHICLE/GARAGEDLOCATION"/>
		</xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>
<xsl:template name = "STEPID4">Residual Bodily Injury Liability</xsl:template>
<xsl:template name = "STEPID5">Residual Property Damage Liability</xsl:template>
<xsl:template name = "STEPID6">Residual Liability (BI and PD)</xsl:template>
<xsl:template name = "STEPID7">Personal Injury Protection - <xsl:call-template name="PIP_TEXT"/></xsl:template>
<xsl:template name = "STEPID8">Michigan Statutory Assessments</xsl:template>
<xsl:template name = "STEPID9">Property Protection Insurance</xsl:template>
<xsl:template name = "STEPID10">Medical Payments</xsl:template>
<xsl:template name = "STEPID11">Uninsured Motorists</xsl:template>
<xsl:template name = "STEPID12">Underinsured Motorists</xsl:template>
<xsl:template name = "STEPID13">Uninsured Motorists Property Damage</xsl:template>
<xsl:template name = "STEPID14">Damage to Your Auto - Comprehensive</xsl:template>
<xsl:template name = "STEPID15">Damage to Your Auto - <xsl:choose>
															<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONTYPE = 'BROAD'">Broadened</xsl:when>
															<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONTYPE = 'LIMIT'">Limited</xsl:when>
															<xsl:when test="VEHICLES/VEHICLE/COVGCOLLISIONTYPE = 'REGULAR'">Regular</xsl:when>
															<xsl:otherwise></xsl:otherwise>
														</xsl:choose><xsl:value-of select="' '"/> Collision</xsl:template>
<xsl:template name = "STEPID16">Mini-Tort PD Liability</xsl:template>
<xsl:template name = "STEPID17">Road Service</xsl:template>
<xsl:template name = "STEPID18">Rental Reimbursement</xsl:template>
<xsl:template name = "STEPID19">Loan/Lease Gap Coverage</xsl:template>
<xsl:template name = "STEPID20">Sound Reproducing Tapes</xsl:template>
<xsl:template name = "STEPID21">Sound Receiving And Transmitting Equipment</xsl:template>
<xsl:template name = "STEPID22">Extra Equipment - Comprehensive</xsl:template>
<xsl:template name = "STEPID23">Extra Equipment - <xsl:choose>
															<xsl:when test="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONTYPE = 'BROAD'">Broadened</xsl:when>
															<xsl:when test="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONTYPE = 'LIMITED'">Limited</xsl:when>
															<xsl:when test="VEHICLES/VEHICLE/EXTRAEQUIPCOLLISIONTYPE = 'REGULAR'">Regular</xsl:when>
															<xsl:otherwise></xsl:otherwise>
														</xsl:choose><xsl:value-of select="' '"/>Collision</xsl:template>
<xsl:template name = "STEPID24"><xsl:call-template name="COVERAGETYPE_DISPLAY"/></xsl:template>
<xsl:template name = "STEPID25">Discount:Wearing Seat Belts</xsl:template>
<xsl:template name = "STEPID26">Discount:Air Bags</xsl:template>
<xsl:template name = "STEPID27">Discount:Anti-Lock Brakes System <xsl:call-template name="ABS_CREDIT"/> </xsl:template>
<xsl:template name = "STEPID28">Discount:Multi-Car</xsl:template>
<xsl:template name = "STEPID29">Discount:Multi-Policy(Auto/Home)</xsl:template>
<xsl:template name = "STEPID30">Discount:Insurance Score Credit (<xsl:value-of select ="POLICY/INSURANCESCORE"/>) - <xsl:call-template name="INSURANCESCORE_PERCENT"></xsl:call-template></xsl:template>
<xsl:template name = "STEPID31">Discount:Premier Driver  <xsl:call-template name="DRIVERDISCOUNT_CREDIT"/> </xsl:template>
<xsl:template name = "STEPID32">Discount:Good Student</xsl:template>
<xsl:template name = "STEPID33">Total Vehicle Premium</xsl:template>


<xsl:template name ="PIP_TEXT">
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/MEDPM = 'FULL'">Full Premium</xsl:when>
		<xsl:when test="VEHICLES/VEHICLE/MEDPM = 'EXCESSWAGE'">Excess Wage</xsl:when>
		<xsl:when test="VEHICLES/VEHICLE/MEDPM = 'EXCESSMEDICAL'">Excess Medical</xsl:when>
		<xsl:when test="VEHICLES/VEHICLE/MEDPM = 'EXCESSBOTH'">Excess Wage/Medical </xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>
<xsl:template name="PIP_DEDUCTIBLE">
	<xsl:choose>
		<xsl:when test="VEHICLES/VEHICLE/MEDPM = 'FULL'"></xsl:when>
		<xsl:otherwise>300</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name ="INSURANCESCORE_PERCENT">

		<xsl:variable name ="VAR_SCORE">
		<xsl:call-template name ="INSURANCESCORE" /> </xsl:variable>

		<xsl:variable name="VAR_SCORE_PERCENT"> 
			<xsl:choose> 
				<xsl:when test ="$VAR_SCORE != '' and $VAR_SCORE &lt; 1.00">
					<xsl:value-of select="round((1 - $VAR_SCORE) * 100)"/>%
				</xsl:when>

				<xsl:when test ="$VAR_SCORE != '' and $VAR_SCORE &gt; 1.00">
					<xsl:value-of select="round(($VAR_SCORE -1 ) * 100)"/>% 
				</xsl:when>
			</xsl:choose>
		</xsl:variable>
		
		<xsl:value-of select="$VAR_SCORE_PERCENT"/>
</xsl:template>
</xsl:stylesheet>
  

