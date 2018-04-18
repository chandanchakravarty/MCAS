IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Ceded_COIFNOL_PAYMENTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Ceded_COIFNOL_PAYMENTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*---------------------------------------------------------------                                                          
Proc Name             : Dbo.[Proc_Ceded_COIFNOL_PAYMENTS]                                                        
Created by            : Sneha                                                    
Date                  :                                          
Purpose               :            
Revison History       :                                                          
Used In               :             
-----------------------------------------------------------------                                                          
Date     Review By          Comments                               
drop Proc [Proc_Ceded_COIFNOL_PAYMENTS]  1333,1,'CLM_LETTER_35',2                                             
----------------------------------------------------------------*/            



CREATE PROCEDURE  [dbo].[Proc_Ceded_COIFNOL_PAYMENTS]  
(  
	@CLAIM_ID          INT,  
	@ACTIVITY_ID       INT, 
	@PROCESS_TYPE      VARCHAR(50),
	@LANG_ID           INT =2,
	@INP_POLICY_NUMBER NVARCHAR(21) =NULL
)  
AS  

BEGIN  
IF @INP_POLICY_NUMBER =''
	SET @INP_POLICY_NUMBER=NULL
DECLARE   
	@OUT_COVERAGES				DECIMAL (18,2),    
	@PROFESSIONAL_SERVICES		DECIMAL (18,2),    
	@LOSS_SALVAGE_SUBROGATION	DECIMAL (18,2),    
	@OUT_SUBROGATION			DECIMAL (18,2),    
	@OUT_SALVAGES				DECIMAL (18,2),    
	@RESV_COVERAGES				DECIMAL (18,2),    
	@RESV_PROFESSIONAL_SERVICES DECIMAL (18,2),    
	@RESV_SUBROGATION			DECIMAL (18,2),    
	@RESV_SALVAGES				DECIMAL (18,2),    
	@RESV_LOSS_SALAGE_SUBROGATION DECIMAL (18,2),    
	@INFLATIONRATE				DECIMAL(18,2),    
	@TODAY_DATE					DATETIME=CONVERT(DATE,GETDATE(),101),    
	@MOEDA_ESTIMATIVE			DECIMAL (18,2),    
	@MOEDA_HONOR				DECIMAL (18,2),    
	@MOEDA_RESSARCIMENTO		DECIMAL (18,2),    
	@MOEDA_SALVADOS				DECIMAL (18,2),    
	@MOEDA_DESPESAS				DECIMAL (18,2),    
	@TOTAL_GEREAL_LIDER			DECIMAL (18,2),    
	@TOTAL_GERAL_COSSEG			DECIMAL (18,2),    
	@TOTAL_GERAL_MOEDA			DECIMAL (18,2),    
	@LIMIT						DECIMAL (18,2),    
	@VAR						INT  ,  
	@OFFCIAL_CLAIM_NUMBER		VARCHAR(50),  
	@POLICY_NUMBER				VARCHAR(50),  
	@REIN_COMAPANY_CODE			VARCHAR(50),  
	@REIN_COMAPANY_NAME			VARCHAR(50),  
	@CARRIER_NAME				VARCHAR(50),  
	@NOME_RAMO					VARCHAR(50),  
	@PART_CONGENERE				VARCHAR(50),  
	@N_ORDERM_APOLICE			VARCHAR(50),  
	@N_ENDOSSO					VARCHAR(50),  
	@EFFECTIVE_DATETIME			VARCHAR(50),  
	@EXPIRY_DATE				VARCHAR(50),  
	@N_ITEM						INT,  
	@DT_AVISO					VARCHAR(50),  
	@DT_SINISTRO				VARCHAR(50),  
	@SINISTRO_IRB				varchar(50),  
	@CLAIM_NUMBER				varchar(50),     
	@BRANCH_CODE				varchar(50),  
	@CITY						VARCHAR(50),  
	@BRANCH_CITY_DETAIL			VARCHAR(50),  
	@DETAIL_TYPE_DESCRIPTION	VARCHAR(50),  
	@CLAIMANT_NAME				VARCHAR(50),  
	@CO_APPL_DOB				VARCHAR(50),  
	@LETTER_GENERATION_DATE		VARCHAR(50),  
	@LOCAL_DE_OCORRENCIA		VARCHAR(50),  
	@CIDADE						VARCHAR(50),  
	@UF							VARCHAR(10),  
	@BENS_SINISTRADOS			VARCHAR(50),  
	@ACTIVITY_TYPE				VARCHAR(50),  
	@MARCA_VEICULO				VARCHAR(50),  
	@N_PLACA					VARCHAR(50),  
	@N_CHASSIS					VARCHAR(50),  
	@ANO						VARCHAR(50),  
	@N_AVERBACAO_CERTIF			VARCHAR(50),  
	@MEIO_TTRANSPORTE			VARCHAR(50),  
	@PREFIXO					VARCHAR(50),  
	@EMPRESA_TRANS				VARCHAR(50),  
	@MERCADORIA_SINISTRADA		VARCHAR(50),
	@ORIGEM						varchar(50),
	@DATA_SAIDA					varchar(50),
	@DESTINO					varchar(50),
	@DATA_CHEGADA				VARCHAR(50),
	@DATA_VISTORIA				VARCHAR(50),
	@NOME_DA_EMBARCACAO			VARCHAR(50),
	@INFLATION_RATE				VARCHAR(50),
	@LETTER_SEQUENCE_NO			INT,
	@LETTER_SEQUENCE_NO1		INT,
	@DIV_ID						INT,
	@SUSEP_LOB_CODE				VARCHAR(10),
	@ACTION_ON_PAYMENT			INT,
	@CLAIM_STATUS_UNDER			VARCHAR(20),
	@CLAIM_STATUS				INT,
	@RESV_PAYMENT_COVERAGES		DECIMAL(18,2),
	@MOEDA_PAYMENT_ESTIMATIVE	DECIMAL(18,2),
	@PAYMENT_COVERAGE			DECIMAL(18,2),
	@PAYMENT_PROFESSIONAL_SERVICES DECIMAL(18,2),
	@PAYMENT_SALVAGE_SUBROGATION   DECIMAL(18,2),
	@PAYMENT_SUBROGATION		DECIMAL (18,2),
	@PAYMENT_SALVAGES			DECIMAL (18,2),
	@TOTAL_PAYMENT_GERAL_COSSEG DECIMAL (18,2),    
	@TOTAL_PAYMENT_GERAL_MOEDA	DECIMAL (18,2),   
	@TOTAL_PAYMENT_GEREAL_LIDER DECIMAL (18,2),
	@LETTERSEQUENCENUMBER       VARCHAR(20) , 
	 @N_ITEM_MERATIME VARCHAR(5) ,
	 @N_ITEM_TRANS VARCHAR(5) 

SELECT  @PROFESSIONAL_SERVICES		  = ISNULL(SUM( CASE WHEN CPC.COVERAGE_CODE_ID =50018				  THEN CAR.OUTSTANDING_TRAN ELSE 0 END),0), 
		@OUT_COVERAGES				  = ISNULL(SUM( CASE WHEN CPC.IS_RISK_COVERAGE='Y'					  THEN CAR.OUTSTANDING_TRAN ELSE 0 END),0),
		@LOSS_SALVAGE_SUBROGATION	  = ISNULL(SUM( CASE WHEN CPC.COVERAGE_CODE_ID IN(50017,50020,50022)  THEN CAR.OUTSTANDING_TRAN ELSE 0 END),0),
		@OUT_SUBROGATION			  = ISNULL(SUM( CASE WHEN CPC.COVERAGE_CODE_ID =50021				  THEN CAR.OUTSTANDING_TRAN ELSE 0 END),0), 
		@OUT_SALVAGES			      = ISNULL(SUM( CASE WHEN CPC.COVERAGE_CODE_ID =50019			      THEN CAR.OUTSTANDING_TRAN ELSE 0 END),0),
		@RESV_COVERAGES				  = ISNULL(SUM( CASE WHEN CPC.IS_RISK_COVERAGE='Y'					  THEN CAR.CO_RESERVE_TRAN  ELSE 0 END),0),
		@RESV_PROFESSIONAL_SERVICES	  = ISNULL(SUM( CASE WHEN CPC.COVERAGE_CODE_ID=50018				  THEN CAR.CO_RESERVE_TRAN  ELSE 0 END),0),
		@RESV_SUBROGATION			  = ISNULL(SUM( CASE WHEN CPC.COVERAGE_CODE_ID =50021			      THEN CAR.CO_RESERVE_TRAN  ELSE 0 END),0),
		@RESV_SALVAGES				  = ISNULL(SUM( CASE WHEN CPC.COVERAGE_CODE_ID =50019				  THEN CAR.CO_RESERVE_TRAN  ELSE 0 END),0),
		@RESV_LOSS_SALAGE_SUBROGATION = ISNULL(SUM( CASE WHEN CPC.COVERAGE_CODE_ID IN(50017,50020,50022)  THEN CAR.CO_RESERVE_TRAN  ELSE 0 END),0),
		@LIMIT						  = ISNULL(SUM( CASE WHEN CPC.IS_RISK_COVERAGE='Y'		              THEN CPC.LIMIT_1		    ELSE 0 END),0),
		@PAYMENT_COVERAGE			  = ISNULL(SUM( CASE WHEN CPC.IS_RISK_COVERAGE='Y'					  THEN CAR.PAYMENT_AMOUNT   ELSE 0 END),0),
		@PAYMENT_PROFESSIONAL_SERVICES= ISNULL(SUM( CASE WHEN CPC.COVERAGE_CODE_ID =50018				  THEN CAR.PAYMENT_AMOUNT   ELSE 0 END),0),
		@PAYMENT_SALVAGE_SUBROGATION  = ISNULL(SUM( CASE WHEN CPC.COVERAGE_CODE_ID IN(50017,50020,50022)  THEN CAR.PAYMENT_AMOUNT   ELSE 0 END),0),
		@PAYMENT_SUBROGATION		  = ISNULL(SUM( CASE WHEN CPC.COVERAGE_CODE_ID =50021			      THEN CAR.PAYMENT_AMOUNT   ELSE 0 END),0),
		@PAYMENT_SALVAGES			  = ISNULL(SUM( CASE WHEN CPC.COVERAGE_CODE_ID =50019				  THEN CAR.PAYMENT_AMOUNT   ELSE 0 END),0),
		@RESV_PAYMENT_COVERAGES		  = ISNULL(SUM( CASE WHEN CPC.IS_RISK_COVERAGE='Y'					  THEN CAR.PAYMENT_AMOUNT   ELSE 0 END),0)
		
FROM CLM_ACTIVITY_RESERVE   CAR 
LEFT OUTER JOIN CLM_PRODUCT_COVERAGES  CPC ON CPC.CLAIM_COV_ID=CAR.COVERAGE_ID 
WHERE CAR.CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID 


--TO FIND CURRENT RATE  
SELECT TOP 1 @INFLATIONRATE=RATE FROM MNT_CURRENCY_RATE_MASTER where (CONVERT(DATE,RATE_EFFETIVE_FROM,101)<=@TODAY_DATE AND CONVERT(DATE,RATE_EFFETIVE_TO,101)>=@TODAY_DATE)  
--(COI Reserve of coverages x Inflation Rate For Estimative)  
SELECT @MOEDA_ESTIMATIVE=ABS(@INFLATIONRATE*@RESV_COVERAGES)  
--(COI Reserve of coverages x Inflation Rate For Honor)  
SELECT @MOEDA_HONOR=@INFLATIONRATE*@RESV_PROFESSIONAL_SERVICES  
--(COI Reserve of coverages x Inflation Rate For Ressarcimento)  
SELECT @MOEDA_RESSARCIMENTO=@INFLATIONRATE*@RESV_SUBROGATION  
--(COI Reserve of coverages x Inflation Rate For Salvados)  
SELECT @MOEDA_SALVADOS=@INFLATIONRATE*@RESV_SALVAGES  
--(COI Reserve of coverages x Inflation Rate For Despesas)  
SELECT @MOEDA_DESPESAS=@INFLATIONRATE*@RESV_LOSS_SALAGE_SUBROGATION
--FOR Indenização TO FIND Part.Cosseg(R$)
SELECT @MOEDA_PAYMENT_ESTIMATIVE=(@INFLATIONRATE*@RESV_PAYMENT_COVERAGES)  
--Total Geral For Total Lider  
SELECT @TOTAL_GEREAL_LIDER=ABS((ISNULL(@OUT_COVERAGES,0)+ISNULL(@PROFESSIONAL_SERVICES,0)+ISNULL(@LOSS_SALVAGE_SUBROGATION,0)+ISNULL(@OUT_SUBROGATION,0)+ISNULL(@OUT_SALVAGES,0)))  
-- Total Geral For Part.Cosseg(R$)  
SELECT @TOTAL_GERAL_COSSEG=ABS((ISNULL(@RESV_COVERAGES,0)+ISNULL(@RESV_PROFESSIONAL_SERVICES,0)+ISNULL(@RESV_SUBROGATION,0)+ISNULL(@RESV_SALVAGES,0)+ISNULL(@RESV_LOSS_SALAGE_SUBROGATION,0)))
-- Total Geral For Part.Qt.Moeda  
SELECT @TOTAL_GERAL_MOEDA=(ISNULL(@MOEDA_ESTIMATIVE,0)+ISNULL(@MOEDA_HONOR,0)+ISNULL(@MOEDA_RESSARCIMENTO,0)+ISNULL(@MOEDA_SALVADOS,0)+ISNULL(@MOEDA_DESPESAS,0))  
--Total Geral For Total Lider  
SELECT @TOTAL_PAYMENT_GEREAL_LIDER=ABS(ISNULL(@PAYMENT_COVERAGE,0)+(ISNULL(@PAYMENT_PROFESSIONAL_SERVICES,0)+ISNULL(@PAYMENT_SALVAGE_SUBROGATION,0)+ISNULL(@PAYMENT_SUBROGATION,0)+ISNULL(@PAYMENT_SALVAGES,0)))  
-- Total Geral For Part.Cosseg(R$)  
SELECT @TOTAL_PAYMENT_GERAL_COSSEG=ABS(ISNULL(@RESV_PAYMENT_COVERAGES,0)+(ISNULL(@RESV_PROFESSIONAL_SERVICES,0)+ISNULL(@RESV_SUBROGATION,0)+ISNULL(@RESV_SALVAGES,0)+ISNULL(@RESV_LOSS_SALAGE_SUBROGATION,0)))
-- Total Geral For Part.Qt.Moeda  
SELECT @TOTAL_PAYMENT_GERAL_MOEDA=ABS(ISNULL(@MOEDA_PAYMENT_ESTIMATIVE,0)+ISNULL(@MOEDA_HONOR,0)+ISNULL(@MOEDA_RESSARCIMENTO,0)+ISNULL(@MOEDA_SALVADOS,0)+ISNULL(@MOEDA_DESPESAS,0))  
-- To Find The Loss Type Id    

SELECT   
	@OFFCIAL_CLAIM_NUMBER=(CCI.OFFCIAL_CLAIM_NUMBER),    
	@POLICY_NUMBER=('0.'+PCPL.POLICY_NUMBER),           --Ploicy Number    
	@REIN_COMAPANY_CODE=(MRCL.REIN_COMAPANY_CODE),      --carrier susep code    
	@REIN_COMAPANY_NAME=(MRCL.REIN_COMAPANY_NAME),      --Carrier Name    
	@CARRIER_NAME=(CCI.CLAIMANT_NAME),					--CoApplicant Name    
	@NOME_RAMO=(ISNULL(MLOBM.LOB_DESC,MLOB.LOB_DESC)),  --Product Name    
	@PART_CONGENERE=(PCI.COINSURANCE_PERCENT),			--COI Share    
	@N_ORDERM_APOLICE=(PCI.TRANSACTION_ID),             --Transaction Id     
	@LIMIT=@LIMIT,										--Sum insured of all claim coverages     
	@N_ITEM=(CIP.ITEM_NUMBER),							--Item Number    
	@N_ENDOSSO=(CONVERT(VARCHAR,PPP.ENDORSEMENT_NO)+'.'+CONVERT(VARCHAR,PPP.ENDORSEMENT_TYPE)), --<endorsement digit>.<endorsement sequence #>    
	@EFFECTIVE_DATETIME=(CONVERT(VARCHAR(2),DATEPART(DD,PCPL.APP_EFFECTIVE_DATE))+'/'+CONVERT(VARCHAR(2),DATEPART(MM,PCPL.APP_EFFECTIVE_DATE))+'/'+CONVERT(VARCHAR(4),DATEPART(YYYY,PCPL.APP_EFFECTIVE_DATE))), -- Effective Date     
	@EXPIRY_DATE=(CONVERT(VARCHAR(2),DATEPART(DD,PCPL.APP_EXPIRATION_DATE))+'/'+CONVERT(VARCHAR(2),DATEPART(MM,PCPL.APP_EXPIRATION_DATE))+'/'+CONVERT(VARCHAR(4),DATEPART(YYYY,PCPL.APP_EXPIRATION_DATE))),     -- Expire Date    
	@DT_AVISO=(CONVERT(VARCHAR(2),DATEPART(DD,CCI.FIRST_NOTICE_OF_LOSS))+'/'+CONVERT(VARCHAR(2),DATEPART(MM,CCI.FIRST_NOTICE_OF_LOSS))+'/'+CONVERT(VARCHAR(4),DATEPART(YYYY,CCI.FIRST_NOTICE_OF_LOSS))),   --First Notice of Loss    
	@DT_SINISTRO=(CONVERT(VARCHAR(2),DATEPART(DD,CCI.LOSS_DATE))+'/'+CONVERT(VARCHAR(2),DATEPART(MM,CCI.LOSS_DATE))+'/'+CONVERT(VARCHAR(4),DATEPART(YYYY,CCI.LOSS_DATE))),      --Date of Loss     
	@SINISTRO_IRB=(CCI.REIN_CLAIM_NUMBER),			    --Reinsurance Claim Number    
	--Outstanding of coverages For Total Lider    
	@OUT_COVERAGES=ABS(ISNULL(@OUT_COVERAGES,@VAR)) ,							--TL_ESTIMATIVE  
	--Outstanding of Loss Expense for Professional Services For Total Lider    
	@PROFESSIONAL_SERVICES=ABS((ISNULL(@PROFESSIONAL_SERVICES,0) ))  ,			--TL_HONOR  
	--Outstanding of Loss Expense + Salvage Expense + Subrogation Expense For Total Lider    
	@LOSS_SALVAGE_SUBROGATION=ABS((ISNULL(@LOSS_SALVAGE_SUBROGATION,0)) ) ,		--LOSS_SALVAGE_SUBROGATION_DESPESAS  
	--Outstanding of Subrogation For Total Lider     
	@OUT_SUBROGATION=ABS((ISNULL(@OUT_SUBROGATION,0))) ,						--TL_RESSARCIMENTO  
	--Outstanding of Salvages For Total Lider    
	@OUT_SALVAGES=ABS((ISNULL(@OUT_SALVAGES,0))) ,								--TL_SALVADOS  
	--COI reserve of coverages For Cosseg    
	@RESV_COVERAGES=ABS((ISNULL(@RESV_COVERAGES,0)) ) ,						    --COSSEG_ESTIMATIVE  
	--COI reserve of Expenses of Professional Services    
	@RESV_PROFESSIONAL_SERVICES=ABS((ISNULL(@RESV_PROFESSIONAL_SERVICES,0))) ,  --COSSEG_HONOR  
	--COI reserve of Subrogation    
	@RESV_SUBROGATION=ABS((ISNULL(@RESV_SUBROGATION,0))) ,						--COSSEG_RESSARCIMENTO  
	--COI reserve of Salvages    
	@RESV_SALVAGES=ABS((ISNULL(@RESV_SALVAGES,0))) ,							--COSSEG_SALVADOS  
	@RESV_LOSS_SALAGE_SUBROGATION=ABS(ISNULL(@RESV_LOSS_SALAGE_SUBROGATION,0) ),--COSSEG_DESPESAS  
	--(COI Reserve of coverages x Inflation Rate For Estimative)    
	@MOEDA_ESTIMATIVE=ABS(ISNULL(@MOEDA_ESTIMATIVE,0) ) ,					    --MOEDA_ESTIMATIVE  
	--(COI Reserve of coverages x Inflation Rate For Honor)    
	@MOEDA_HONOR=ABS(ISNULL(@MOEDA_HONOR,0) ) ,									--MOEDA_HONOR  
	--(COI Reserve of coverages x Inflation Rate For Ressarcimento)    
	@MOEDA_RESSARCIMENTO=ABS(ISNULL(@MOEDA_RESSARCIMENTO,0) ) ,					--MOEDA_RESSARCIMENTO  
	--(COI Reserve of coverages x Inflation Rate For Salvados)    
	@MOEDA_SALVADOS=ABS(ISNULL(@MOEDA_SALVADOS,0) ) ,							--MOEDA_SALVADOS  
	--(COI Reserve of coverages x Inflation Rate For Despesas)    
	@MOEDA_DESPESAS=ABS(ISNULL(@MOEDA_DESPESAS,0) ) ,							--MOEDA_DESPESAS  
	--Total Geral For Total Lider    
	@TOTAL_GEREAL_LIDER=ABS(ISNULL(@TOTAL_GEREAL_LIDER,0) ) ,                   --TOTAL_GEREAL_LIDER  
	-- Total Geral For Part.Cosseg(R$)    
	@TOTAL_GERAL_COSSEG=ABS(ISNULL(@TOTAL_GERAL_COSSEG,0) ) ,                   --TOTAL_GERAL_COSSEG  
	-- Total Geral For Part.Qt.Moeda    
	@TOTAL_GERAL_MOEDA=ABS(ISNULL(@TOTAL_GERAL_MOEDA,0)) ,						--TOTAL_GERAL_MOEDA  
		--FOR LETTER NUMBER 30
	@PAYMENT_COVERAGE =ABS(ISNULL(@PAYMENT_COVERAGE,0)),						--PAYMENT FOR ESTIMATIVE
	@PAYMENT_PROFESSIONAL_SERVICES =ABS(ISNULL(@PAYMENT_PROFESSIONAL_SERVICES,0)),--PAYMENT FOR HONOR
	@PAYMENT_SALVAGE_SUBROGATION=ABS(ISNULL(@PAYMENT_SALVAGE_SUBROGATION,0)),   --PAYMENT FOR DESPESAS
	@PAYMENT_SUBROGATION =ABS(ISNULL(@PAYMENT_SUBROGATION,0)),					--PAYMENT FOR RESSARCIMENTO
	@PAYMENT_SALVAGES = ABS(ISNULL(@PAYMENT_SALVAGES,0)),						--PAYMENT FOR SALVADOS
	--Influation Rate     
	@INFLATION_RATE= @INFLATIONRATE,  --as INFLATION_RATE,    
	@CLAIM_NUMBER=(CCI.CLAIM_NUMBER), --AS CLAIM_NUMBER,    -- Claim Number     
	@BRANCH_CODE=(MDL.BRANCH_CODE),   --AS BRANCH_CODE,     -- Branch Code    
	@CITY=(MDL.DIV_CITY),             --branch city     
	@BRANCH_CITY_DETAIL=( CONVERT(VARCHAR,DAY(GETDATE()))+' de '+dbo.fun_GetMonth_BR(MONTH(GETDATE()))+' de '+CONVERT(VARCHAR,YEAR(GETDATE())) ),-- branch city of policy letter generation date    
	@DETAIL_TYPE_DESCRIPTION=(CTD.DETAIL_TYPE_DESCRIPTION ) ,   --- Claim Cause  Or Loss Type    
	@CLAIMANT_NAME=(CCI.CLAIMANT_NAME),      --Insured Name    
	@CO_APPL_DOB=(CONVERT(VARCHAR,DAY( CAL.CO_APPL_DOB))+'/'+CONVERT(VARCHAR,MONTH( CAL.CO_APPL_DOB))+'/'+CONVERT(VARCHAR,YEAR( CAL.CO_APPL_DOB))),   -- Insured Date Of Birth    
	@LETTER_GENERATION_DATE=(CONVERT(VARCHAR,DAY( @TODAY_DATE))+'/'+CONVERT(VARCHAR,MONTH( @TODAY_DATE))+'/'+CONVERT(VARCHAR,YEAR( @TODAY_DATE))),  

	---------FOR LETTER NO 21-----------  
	@LOCAL_DE_OCORRENCIA=(COD.LOSS_LOCATION),       --Location of Loss     
	@CIDADE=(COD.LOSS_LOCATION_CITY ),				--Loss Location City    
	@UF=(MCSL.STATE_CODE),							--Loss Location State     
	@BENS_SINISTRADOS=(CIP.DAMAGE_DESCRIPTION),     --DAMAGE DESCRIPTION     
	@ACTIVITY_TYPE=(ISNULL(CTDM.TYPE_DESC ,CTD1.DETAIL_TYPE_DESCRIPTION)),   --ACTIVITY  

	--------FOR LETTER NO 22------------  
	@MARCA_VEICULO=(CIP.VEHICLE_MODEL) ,        --Vehicle Model  
	@N_PLACA=(CIP.LICENCE_PLATE_NUMBER),        --LICENCE PLATE NUMBER  
	@N_CHASSIS=(CIP.VEHICLE_VIN)  ,				--#VIN  
	@ANO=(CIP.YEAR),							--Vehicle Year  

	--------FOR LETTER NO 23--------------  
	@N_AVERBACAO_CERTIF=(CIP.VOYAGE_CERT_NUMBER),        --Certificate Number  
	@MEIO_TTRANSPORTE=(CIP.VOYAGE_CONVEYENCE_TYPE),      --Conveyence Type  
	@PREFIXO=(CIP.VOYAGE_PREFIX),						 --prefix  
	@EMPRESA_TRANS=(CIP.VOYAGE_TRAN_COMPANY),            --Transportation Company  
	@MERCADORIA_SINISTRADA=(CIP.ACTUAL_INSURED_OBJECT) , --Insured Object Description  
	@ORIGEM=(CIP.CITY1) ,    
	--Origin City  
	@DATA_SAIDA=(
	CONVERT(VARCHAR,DAY(CIP.VOYAGE_DEPARTURE_DATE))  
	+'/'+CONVERT(VARCHAR,MONTH(CIP.VOYAGE_DEPARTURE_DATE))  
	+'/'+CONVERT(VARCHAR,YEAR(CIP.VOYAGE_DEPARTURE_DATE))   
	),            --DEPARTURE DATE  


	@DESTINO=(CIP.CITY2) ,										  --Destination City  
	@DATA_CHEGADA=(CONVERT(VARCHAR,DAY(CIP.VOYAGE_ARRIVAL_DATE))  
	+'/'+CONVERT(VARCHAR,MONTH(CIP.VOYAGE_ARRIVAL_DATE))  
	+'/'+CONVERT(VARCHAR,YEAR(CIP.VOYAGE_ARRIVAL_DATE))),		  --ARRIVAL DATE  

	@DATA_VISTORIA=(CONVERT(VARCHAR,DAY(CIP.VOYAGE_SURVEY_DATE ))  
	+'/'+CONVERT(VARCHAR,MONTH(CIP.VOYAGE_SURVEY_DATE ))  
	+'/'+CONVERT(VARCHAR,YEAR(CIP.VOYAGE_SURVEY_DATE ))),         --SURVEY_DATE  

	@NOME_DA_EMBARCACAO=(CIP.VESSEL_NAME ) ,                      --VESSEL_NAME  
	@DIV_ID=(MDL.DIV_ID),
	@SUSEP_LOB_CODE=MLOB.SUSEP_LOB_CODE,				-- Product Code 
	@CLAIM_STATUS_UNDER=CCI.CLAIM_STATUS_UNDER,			-- Claim Status Under 
	@CLAIM_STATUS=CCI.CLAIM_STATUS	,					-- to find claim status (open or close)@N_ITEM_MERATIME=PM.VESSEL_NUMBER -- CLAIM ITEM NUMBER FOR LETTER 29
	@N_ITEM_MERATIME=PM.VESSEL_NUMBER, -- CLAIM ITEM NUMBER FOR LETTER 29
	 ---------------------LETTER 35----------
    @N_ITEM_TRANS =PCIN.COMMODITY_NUMBER

FROM     
	CLM_CLAIM_INFO CCI WITH(NOLOCK)     
	LEFT OUTER JOIN      
	CLM_ACTIVITY CA WITH(NOLOCK) ON (CA.CLAIM_ID = CCI.CLAIM_ID AND CA.ACTIVITY_ID =CASE WHEN @ACTIVITY_ID IS NULL THEN CA.ACTIVITY_ID  ELSE @ACTIVITY_ID END )    
	LEFT OUTER JOIN      
	POL_CUSTOMER_POLICY_LIST PCPL ON (PCPL.CUSTOMER_ID = CCI.CUSTOMER_ID AND PCPL.POLICY_ID = CCI.POLICY_ID AND PCPL.POLICY_VERSION_ID = CCI.POLICY_VERSION_ID)     
	LEFT OUTER JOIN      
	POL_CO_INSURANCE PCI WITH(NOLOCK) ON (PCI.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PCI.POLICY_ID = PCPL.POLICY_ID AND PCI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID AND PCI.LEADER_FOLLOWER = '14549')    
	LEFT OUTER JOIN      
	MNT_REIN_COMAPANY_LIST MRCL WITH(NOLOCK) ON (MRCL.REIN_COMAPANY_ID = PCI.COMPANY_ID)      
	LEFT OUTER JOIN    
	POL_APPLICANT_LIST PAL WITH(NOLOCK) ON (PAL.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PAL.POLICY_ID = PCPL.POLICY_ID AND PAL.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID)    
	LEFT OUTER JOIN    
	CLT_APPLICANT_LIST CAL WITH(NOLOCK) ON (CAL.CUSTOMER_ID=PAL.CUSTOMER_ID AND CAL.APPLICANT_ID = PAL.APPLICANT_ID AND CAL.IS_PRIMARY_APPLICANT=1)    
	LEFT OUTER JOIN       
	MNT_LOB_MASTER  MLOB WITH(NOLOCK) ON (MLOB.LOB_ID=PCPL.POLICY_LOB)    
	LEFT OUTER JOIN 
	MNT_LOB_MASTER_MULTILINGUAL MLOBM WITH(NOLOCK) ON(MLOBM.LOB_ID=PCPL.POLICY_LOB AND LANG_ID=@LANG_ID) 
	LEFT OUTER JOIN       
	MNT_DIV_LIST MDL WITH(NOLOCK) ON (MDL.DIV_ID=PCPL.DIV_ID)    
	LEFT OUTER JOIN      
	POL_POLICY_PROCESS PPP WITH(NOLOCK) ON (PPP.CUSTOMER_ID=CCI.CUSTOMER_ID AND PPP.POLICY_ID=CCI.POLICY_ID AND PPP.NEW_POLICY_VERSION_ID=CCI.POLICY_VERSION_ID AND PPP.PROCESS_STATUS <> 'ROLLBACK')    
	LEFT OUTER JOIN      
	CLM_OCCURRENCE_DETAIL COD WITH(NOLOCK) ON (COD.CLAIM_ID=CCI.CLAIM_ID)      
	LEFT OUTER JOIN    
	CLM_TYPE_DETAIL CTD WITH(NOLOCK) ON CTD.DETAIL_TYPE_ID= CAST( REPLACE(ISNULL(COD.LOSS_TYPE,'0'),',','') AS INT)  
	LEFT OUTER JOIN    
	CLM_TYPE_DETAIL CTD1 WITH(NOLOCK) ON CTD1.DETAIL_TYPE_ID= CA.ACTION_ON_PAYMENT  
	LEFT OUTER JOIN 
	CLM_TYPE_DETAIL_MULTILINGUAL CTDM WITH(NOLOCK) ON CTDM.DETAIL_TYPE_ID=CA.ACTION_ON_PAYMENT AND CTDM.LANG_ID=@LANG_ID
	LEFT OUTER JOIN      
	CLM_INSURED_PRODUCT CIP WITH(NOLOCK) ON (CIP.CLAIM_ID=CCI.CLAIM_ID)    
	LEFT OUTER JOIN       
	MNT_COUNTRY_STATE_LIST MCSL WITH(NOLOCK) ON (MCSL.STATE_ID=COD.LOSS_LOCATION_STATE)     
	LEFT OUTER JOIN 
	CLM_PROCESS_LOG CPL WITH(NOLOCK) ON (CPL.CLAIM_ID=CA.CLAIM_ID AND CPL.ACTIVITY_ID=CA.ACTIVITY_ID)      
	LEFT OUTER JOIN
	POL_MARITIME PM WITH(NOLOCK) ON  PM.CUSTOMER_ID=PCPL.CUSTOMER_ID AND PM.POLICY_ID=PCPL.POLICY_ID AND PM.POLICY_VERSION_ID =PCPL.POLICY_VERSION_ID
	LEFT OUTER JOIN
    POL_COMMODITY_INFO PCIN WITH(NOLOCK) ON PCIN.CUSTOMER_ID=PCPL.CUSTOMER_ID AND PCIN.POLICY_ID=PCPL.POLICY_ID AND PCIN.POLICY_VERSION_ID=PCPL.POLICY_VERSION_ID
 
WHERE    
	PCPL.POLICY_NUMBER =CASE WHEN @INP_POLICY_NUMBER IS NULL THEN PCPL.POLICY_NUMBER ELSE @INP_POLICY_NUMBER END AND
	CCI.CLAIM_ID =CASE WHEN @CLAIM_ID IS NULL THEN CCI.CLAIM_ID ELSE @CLAIM_ID END 
	
END  

IF NOT EXISTS (SELECT * FROM CLM_PROCESS_LOG WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND PROCESS_TYPE=@PROCESS_TYPE )

BEGIN
SELECT @LETTER_SEQUENCE_NO1=( SELECT ISNULL(MAX(LETTER_SEQUENCE_NO),0)+1 FROM CLM_PROCESS_LOG WHERE PROCESS_TYPE=@PROCESS_TYPE  AND CLAIM_ID IN 
		(
			SELECT CCI.CLAIM_ID FROM CLM_CLAIM_INFO CCI 
			INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL ON CCI.CUSTOMER_ID=PCPL.CUSTOMER_ID AND CCI.POLICY_ID=PCPL.POLICY_ID AND CCI.POLICY_VERSION_ID=PCPL.POLICY_VERSION_ID 
			WHERE PCPL.DIV_ID=@DIV_ID
		))
		
END
ELSE
BEGIN
SELECT @LETTER_SEQUENCE_NO1=(SELECT LETTER_SEQUENCE_NO FROM CLM_PROCESS_LOG WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND PROCESS_TYPE=@PROCESS_TYPE)

END
SELECT @LETTERSEQUENCENUMBER=(RIGHT ('000000000'+CONVERT(VARCHAR(10),@LETTER_SEQUENCE_NO1),6))

-----------------TO FIND ACTION_ON_PAYMENT-----------------------------
SELECT @ACTION_ON_PAYMENT=CA.ACTION_ON_PAYMENT 
FROM CLM_ACTIVITY CA
LEFT OUTER JOIN CLM_TYPE_DETAIL CTD WITH(NOLOCK)ON CTD.DETAIL_TYPE_ID=CA.ACTION_ON_PAYMENT AND CTD.TYPE_ID=8 AND CTD.IS_ACTIVE='Y'
WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID  



----------------------------SELECT TABLE VALUES-------------  
SELECT 
	@OFFCIAL_CLAIM_NUMBER			 AS OFFICIAL_CLAIM_NUMBER, 
	@POLICY_NUMBER					 AS POLICY_NUMBER,
	@REIN_COMAPANY_CODE				 AS COMPANY_CODE, 
	@REIN_COMAPANY_NAME				 AS REIN_COMAPANY_NAME,
	@CARRIER_NAME					 AS CARRIER_NAME,
	@NOME_RAMO						 AS NOME_RAMO,
	@PART_CONGENERE					 AS PART_CONGENERE,
	@N_ORDERM_APOLICE				 AS N_ORDERM_APOLICE, 
	@LIMIT							 AS LIMIT,
	@N_ENDOSSO						 AS N_ENDOSSO,
	@EFFECTIVE_DATETIME				 AS EFFECTIVE_DATETIME,
	@EFFECTIVE_DATETIME				 AS EFFECTIVE_DATETIME,
	@EXPIRY_DATE					 AS EXPIRY_DATE,
	@N_ITEM							 AS N_ITEM,
	@DT_AVISO						 AS DT_AVISO,
	@DT_SINISTRO					 AS DT_SINISTRO,
	@SINISTRO_IRB					 AS SINISTRO_IRB,
	@OUT_COVERAGES					 AS TL_ESTIMATIVE,
	@PROFESSIONAL_SERVICES			 AS TL_HONOR,
	@LOSS_SALVAGE_SUBROGATION		 AS Loss_Salvage_Subrogation_Despesas,
	@OUT_SUBROGATION				 AS TL_RESSARCIMENTO,
	@OUT_SALVAGES					 AS TL_SALVADOS,
	@RESV_COVERAGES					 AS COSSEG_ESTIMATIVE,
	@RESV_PROFESSIONAL_SERVICES		 AS COSSEG_HONOR, 
	@RESV_SUBROGATION				 AS COSSEG_RESSARCIMENTO,
	@RESV_SALVAGES					 AS COSSEG_SALVADOS, 
	@RESV_LOSS_SALAGE_SUBROGATION	 AS COSSEG_DESPESAS, 
	@MOEDA_ESTIMATIVE				 AS MOEDA_ESTIMATIVE,
	@MOEDA_HONOR					 AS MOEDA_HONOR, 
	@MOEDA_RESSARCIMENTO			 AS MOEDA_RESSARCIMENTO,
	@MOEDA_SALVADOS					 AS MOEDA_SALVADOS,
	@MOEDA_DESPESAS					 AS MOEDA_DESPESAS, 
	@TOTAL_GEREAL_LIDER				 AS TOTAL_GEREAL_LIDER,
	@TOTAL_GERAL_COSSEG				 AS TOTAL_GERAL_COSSEG,
	@TOTAL_GERAL_MOEDA				 AS TOTAL_GERAL_MOEDA,
	@PAYMENT_COVERAGE				 AS PAYMENT_COVERAGE,
	@TOTAL_PAYMENT_GERAL_COSSEG		 AS TOTAL_PAYMENT_GERAL_COSSEG,
	@TOTAL_PAYMENT_GERAL_MOEDA		 AS TOTAL_PAYMENT_GERAL_MOEDA,
	@TOTAL_PAYMENT_GEREAL_LIDER		 AS TOTAL_PAYMENT_GEREAL_LIDER,
	@PAYMENT_PROFESSIONAL_SERVICES	 AS PAYMENT_PROFESSIONAL_SERVICES,
	@PAYMENT_SALVAGE_SUBROGATION	 AS PAYMENT_SALVAGE_SUBROGATION,
	@PAYMENT_SALVAGES				 AS PAYMENT_SALVAGES,
	@INFLATION_RATE					 AS INFLATION_RATE,
	@CLAIM_NUMBER					 AS CLAIM_NUMBER,
	@BRANCH_CODE					 AS BRANCH_CODE, 
	@CITY							 AS CITY,
	@BRANCH_CITY_DETAIL				 AS BRANCH_CITY_DETAIL,
	@DETAIL_TYPE_DESCRIPTION		 AS DETAIL_TYPE_DESCRIPTION, 
	@CLAIMANT_NAME					 AS CLAIMANT_NAME, 
	@CO_APPL_DOB					 AS  CO_APPL_DOB, 
	@LETTER_GENERATION_DATE			 AS LETTER_GENERATION_DATE, 
	@LOCAL_DE_OCORRENCIA			 AS LOSS_LOCATION,
	@CIDADE							 AS LOSS_CIDADELOCATION_CITY,     
	@UF							     AS UF,
	@BENS_SINISTRADOS				 AS BENS_SINISTRADOS,
	@ACTIVITY_TYPE					 AS ACTIVITY_TYPE, 
	@MARCA_VEICULO					 AS MARCA_VEICULO,
	@N_PLACA						 AS N_PLACA,
	@N_CHASSIS						 AS N_CHASSIS,
	@ANO							 AS ANO, 
	@N_AVERBACAO_CERTIF				 AS N_AVERBACAO_CERTIF,
	@MEIO_TTRANSPORTE				 AS MEIO_TTRANSPORTE, 
	@PREFIXO						 AS PREFIXO,
	@EMPRESA_TRANS					 AS EMPRESA_TRANS,  
	@MERCADORIA_SINISTRADA			 AS MERCADORIA_SINISTRADA,
	@ORIGEM							 AS ORIGEM,
	@DATA_SAIDA						 AS DATA_SAIDA,
	@DESTINO						 AS DESTINO,
	@DATA_VISTORIA					 AS DATA_VISTORIA,
	@NOME_DA_EMBARCACAO				 AS NOME_DA_EMBARCACAO,
	@LETTER_SEQUENCE_NO				 AS LETTER_SEQUENCE_NO,
	@LETTER_SEQUENCE_NO1			 AS LETTER_SEQUENCE_NO1,
	@PROCESS_TYPE					 as PROCESS_TYPE,
	@ACTION_ON_PAYMENT				 AS ACTION_ON_PAYMENT,
	@SUSEP_LOB_CODE					 as SUSEP_LOB_CODE, 
	@CLAIM_STATUS_UNDER				 AS CLAIM_STATUS_UNDER,
	@CLAIM_STATUS					 AS CLAIM_STATUS,
	@LETTERSEQUENCENUMBER            AS LETTERSEQUENCENUMBER,
	@N_ITEM_MERATIME                 AS MARIITEMNUMBER,
	@N_ITEM_TRANS					 AS NATIONALTRANSNUMBER

---------To find Product code (SUSEP_LOB_CODE)---------
--SELECT MLOB.SUSEP_LOB_CODE AS SUSEP_LOB_CODE
--FROM MNT_LOB_MASTER MLOB
--LEFT OUTER JOIN CLM_CLAIM_INFO CCI WITH(NOLOCK) ON  CCI.LOB_ID=MLOB.LOB_ID  
--LEFT OUTER JOIN CLM_ACTIVITY CA WITH (NOLOCK) ON CA.CLAIM_ID=CCI.CLAIM_ID 
--WHERE CCI.CLAIM_ID=@CLAIM_ID

-- Letter Number 19 HTML-----------
		DECLARE @strHTML varchar(8000)
IF(@PROCESS_TYPE='CLM_LETTER')
    BEGIN

		set  @strHTML = 
			'<html>' +
			'<head>' +
			'<style type=''text/css''>' +
			'body{font-family:Arial; font-size:12pt; margn-left:0.5cm;margin-top:1.0cm;margin-right:0.5cm;margin-bottom:0.5cm;}' +
			'hr {border-top: 1px dashed #000; margin-left:0px; text-align: left}' +
			'.style1{width:20%;}' +
			'.style2{width:58%;}' +
			'</style>' +
			'</head>' +
			'<body>' +
			'<table width=100% style=''''>' +
			'<tr>' +
			'<td>' +
			'COMPANHIA DE SEGUROS ALIANÇA DA BAHIA<br />' +
			'À Cosseguradora<br />' +
			'<table  width=''100%''>' +
			'<tr>' +
			'<td colspan=3><table width=100%><tr><td>' +
			ISNULL(@REIN_COMAPANY_CODE,'')+
			'</td><td>'+CASE WHEN @REIN_COMAPANY_CODE!='' THEN ' - ' ELSE '' END+'</td><td>' +
			ISNULL(@REIN_COMAPANY_NAME,'')+
			'</td> <td>Sinistro Lider</td><td>' +
			CASE WHEN @OFFCIAL_CLAIM_NUMBER!='' THEN ISNULL(@OFFCIAL_CLAIM_NUMBER,'') ELSE '' END+
			'</td></tr></table></td>' +
			'</tr>' +
			'<tr>' +
			'<td>Ref&nbsp;: </td>' +
			'<td>(&nbsp;X&nbsp;)&nbsp;&nbsp;Aviso de Sinistro</td>' +
			'<td>(&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;Despesas/Honorários</td>' +
			'</tr>' +
			'<tr>' +
			'<td></td>' +
			'<td>(&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;Cobrança de Sinistro</td>' +
			'<td>(&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;Ressarc.Salvados</td>' +
			'</tr>' +
			'<tr>' +
			'<td></td>' +
			'<td>(&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp; Pagto. parcial/Adiantamento</td>' +
			'<td>(&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp; Encerramento sem Indenização</td>' +
			'</tr>' +
			'<tr>' +
			'<td></td>' +
			'<td>(&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp; Pagamento Final</td>' +
			'<td>'+CASE WHEN @ACTION_ON_PAYMENT=168 THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END +'&nbsp;&nbsp; Reabertura de Sinistro</td>' +
			'</tr>' +
			'</table>' +
			'<hr width=100% />' +
			'DADOS GERAIS<br />' +
			'<hr width=100% />' +
			'<table width=''100%''>' +
			'<tr>' +
			'<td class=''style2''>Nome do Segurado </td><td>!</td>' +
			'<td>Nome Ramo</td><td>!</td>' +
			'<td align=center>% Part. Congenere</td>' +
			'</tr><tr>' +
			'<td class=''style2''>' +
			ISNULL(@CARRIER_NAME,'')+
			'</td><td>!</td>' +
			'<td>' +
			ISNULL(@NOME_RAMO,'')+
			'</td><td>!</td>' +
			'<td align=center>' +
			ISNULL(@PART_CONGENERE,'')+
			'</td></tr>' +
			'</table>' +
			'<hr width=100% />' +
			'<table width=''100%''>' +
			'<tr><td>N.Apolice</td><td>!</td>' +
			'<td class=''style1'' align=center> N.Ordem Apolice</td><td>!</td>' +
			'<td align=center> N. Endosso</td><td>!</td>' +
			'<td align=center> N.Ordem Endosso</td><td>!</td>' +
			'<td align=center> Vigência</td></tr>' +
			'<tr><td class=''style1'' align=right>' +
			ISNULL(@POLICY_NUMBER,'')+
			'</td><td>!</td>' +
			'<td align=right>' +
			ISNULL(@N_ORDERM_APOLICE,'')+
			'</td><td>!</td>' +
			'<td align=right>' +
			ISNULL(@N_ENDOSSO,'')+
			'</td><td>!</td>' +
			'<td align=right></td><td>!</td>' +
			'<td align=center>' +
			ISNULL(@EFFECTIVE_DATETIME,'')+ CASE WHEN @EFFECTIVE_DATETIME!='' THEN  CASE WHEN @EXPIRY_DATE!='' THEN ' - ' ELSE '' END ELSE ''END +            
			ISNULL(@EXPIRY_DATE,'')+
			'</td></tr>' +
			'</table>' +
			'<hr width=100% />' +
			'<table width=100%>' +
			'<tr><td>Moeda</td><td>!</td>' +
			'<td>N.Item</td><td>!</td>' +
			'<td align=right>Imp.Segurada</td><td>!</td>' +
			'<td>Sinistro IRB</td><td>!</td>' +
			'<td>Dt.Sinistro</td><td>!</td>' +
			'<td>Dt.Aviso</td>' +
			'</tr><tr>' +
			'<td>R$</td><td>!</td>' +
			'<td align=right>' +
			ISNULL(CONVERT(VARCHAR,@N_ITEM),'')+
			'</td><td>!</td>' +
			'<td align=right>' +
			ISNULL(CONVERT(VARCHAR,@Limit),'')+
			'</td><td>!</td>' +
			'<td align=right>' +
			ISNULL(CONVERT(VARCHAR,@SINISTRO_IRB),'')+
			'</td><td>!</td>' +
			'<td>' +
			ISNULL(@DT_SINISTRO,'')+
			'</td><td>!</td>' +
			'<td>' +
			ISNULL(@DT_AVISO,'')+
			'</td></tr>' +
			'</table>' +
			'<hr width=100% /><br />' +
			'<hr width=99% />' +
			'<table width=''99%''>' +
			'<tr>' +
			'<td>Participações</td><td>!</td>' +
			'<td>VI. Total Lider</td><td>!</td>' +
			'<td>Part.Cosseg (R$)</td><td>!</td>' +
			'<td>Part .Qt .Moeda</td><td>!</td>' +
			'<td>Dt. Base</td><td>!</td>' +
			'<td>Fat. Index.</td></tr>' +
			'<tr>' +
			'<td><hr width=100% /></td><td>!</td>' +
			'<td><hr width=100% /></td><td>!</td>' +
			'<td><hr width=100% /></td><td>!</td>' +
			'<td><hr width=100% /></td><td>!</td>' +
			'<td><hr width=100% /></td><td>!</td>' +
			'<td><hr width=99% /></td></tr>' +
			'<tr><td>Estimativa</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @OUT_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@OUT_COVERAGES) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @RESV_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_COVERAGES) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @MOEDA_ESTIMATIVE!=0.00 THEN CONVERT(VARCHAR,@MOEDA_ESTIMATIVE) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right></td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>' +
			'</tr><tr>' +
			'<td>Indenização</td><td>!</td>' +
			'<td align=right></td><td>!</td>' +
			'<td></td><td>!</td>' +
			'<td></td><td>!</td>' +
			'<td></td><td>!</td>' +
			'<td></td>' +
			'</tr><tr>' +
			'<td>Honorários</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@PROFESSIONAL_SERVICES) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			 ISNULL(CASE WHEN @RESV_PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@RESV_PROFESSIONAL_SERVICES) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @MOEDA_HONOR!=0.00 THEN CONVERT(VARCHAR,@MOEDA_HONOR) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right></td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>' +
			'</tr><tr>' +
			'<td>Despesas</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @LOSS_SALVAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@LOSS_SALVAGE_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @RESV_LOSS_SALAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_LOSS_SALAGE_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @MOEDA_DESPESAS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_DESPESAS) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right></td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>' +
			'</tr><tr>' +
			'</tr>' +
			'<tr><td colspan=''11''><hr width=100% /></td></tr>' +
			'<tr><td>Ressarcimento</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @OUT_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@OUT_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @RESV_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @MOEDA_RESSARCIMENTO!=0.00 THEN CONVERT(VARCHAR,@MOEDA_RESSARCIMENTO) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right></td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>' +
			'</tr><tr>' +
			'<td>Salvados</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @OUT_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@OUT_SALVAGES) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @RESV_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_SALVAGES) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @MOEDA_SALVADOS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_SALVADOS) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right></td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>' +
			'</tr><tr>' +
			'</tr><tr>' +
			'<td><hr width=100% /></td><td>!</td>' +
			'<td align=right><hr width=100% /></td><td>!</td>' +
			'<td><hr width=100% /></td><td>!</td>' +
			'<td><hr width=100% /></td><td>!</td>' +
			'<td><hr width=100% /></td><td>!</td>' +
			'<td><hr width=100% /></td>' +
			'</tr><tr>' +
			'<tr><td>Total Geral</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @TOTAL_GEREAL_LIDER!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GEREAL_LIDER) ELSE '' END,'')+ '</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @TOTAL_GERAL_COSSEG!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GERAL_COSSEG) ELSE '' END,'')+ '</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @TOTAL_GERAL_MOEDA!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GERAL_MOEDA) ELSE '' END,'')+'</td><td>!</td>' +
			'<td>F-A-J-</td><td></td>' +
			'<td></td>' +
			'</tr>' +
			'<tr><td colspan=11><hr width=100% /></td></tr>' +
			'</table>' +
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Colocar a nossa disposição quantia referente a sua particioação até  0000000<br />' +
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Sinistro Sorteio já liquidado. A cota parte dessa congenere sera debitada<br />' +
			' &nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; através do L.C.C (Lancto.Contra Corrente).<br />' +
			'<table width=''60%''><tr><td>Observação:</td><td>C.S </td><td>' +
			ISNULL(@CLAIM_NUMBER,'')+
			'</td><td>N.Carta Aviso</td><td>'+ISNULL(@LETTERSEQUENCENUMBER,'')+'</td><td>O.E.</td><td>' +
			ISNULL(@BRANCH_CODE,'')+
			'</td></tr></table>' +
			'<table width=''99%''>' +
			'<tr>' +
			'<BR/><td>' + 
			ISNULL(@CITY,'')+
			'<td>' +
			ISNULL(@BRANCH_CITY_DETAIL,'')+
			'</td>' +
			'<td></td>' +
			'</tr>' +
			'<tr>' +
			'<td colspan=''2''><hr width=70% /></td>' +
			'<td><hr width=100% /></td>' +
			'</tr><tr>' +
			'<td>Local e Data</td>' +
			'<td></td>' +
			'<td>Assinatura</td>' +
			'</tr>' +
			'</table>' +
			'</td>' +
			'</tr>' +
			'</table>' +
			'</body>' +
			'</html>' 

END
--select @strHTML19 AS LETTER19


-- Letter Number 20--
IF(@PROCESS_TYPE='CLM_LETTER_20')
    BEGIN
    -- DECLARE @strHTML varchar(8000)
	 set  @strHTML =
			'<html>'+
			'<head>'+
			'<style type=text/css>'+
			'body{font-family:Arial; font-size:12pt; margn-left:0.5cm;margin-top:1.0cm;margin-right:0.5cm;margin-bottom:0.5cm;}'+
			'hr {border-top: 1px dashed #000; margin-left:0px; text-align: left}'+
			'.style1{width:53%;}'+
			'.style2{width: 74%;}'+
			'</style>'+
			'</head>'+
			'<body>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			'0000000000000000000000000000000<br />'+
			'À Cosseguradora<br />'+
			'<table  width=100%>'+
			'<tr>'+
			'<td colspan=3><table width=100%><tr><td>'+
			ISNULL(@REIN_COMAPANY_CODE,'')+
			'</td><td>'+CASE WHEN @REIN_COMAPANY_CODE!='' THEN ' - ' ELSE '' END+'</td><td></td><td>' +
			ISNULL(@REIN_COMAPANY_NAME,'')+             
			'</td> <td>Sinistro Lider</td><td>'+
			CASE WHEN @OFFCIAL_CLAIM_NUMBER!='' THEN ISNULL(@OFFCIAL_CLAIM_NUMBER,'') ELSE '' END+
			'</td></tr></table></td>'+
			'</tr>'+
			'<tr>'+
			'<td>Ref: </td>'+
			'<td>(&nbsp;X&nbsp;)&nbsp;&nbsp;Aviso de Sinistro</td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp;Despesas/Honorários</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp;Cobrança de Sinistro</td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp;Ressarc.Salvados</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp; Pagto. parcial/Adiantamento</td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp; Encerramento sem Indenização</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp; Pagamento Final</td>'+
			'<td>'+CASE WHEN @ACTION_ON_PAYMENT=168 THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END +'&nbsp;&nbsp; Reabertura de Sinistro</td>'+
			'</tr>'+
			'</table>'+
			'<hr width=100% />'+
			'DADOS GERAIS<br />'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr>'+
			'<td class=style1>Nome do Segurado </td><td>!</td>'+
			'<td>Nome Ramo</td><td>!</td>'+
			'<td align=center>% Part. Congenere</td>'+
			'</tr><tr>'+
			'<td class=style1>'+
			ISNULL(@CARRIER_NAME,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@NOME_RAMO,'')+
			'</td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@PART_CONGENERE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr><td>N.Apolice</td><td>!</td>'+
			'<td> N.Ordem Apolice</td><td>!</td>'+
			'<td> N. Endosso</td><td>!</td>'+
			'<td> N.Ordem Endosso</td><td>!</td>'+
			'<td align=center> Vigência</td></tr>'+
			'<tr><td align=right>'+
			ISNULL(@POLICY_NUMBER,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(@N_ORDERM_APOLICE,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(@N_ENDOSSO,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@EFFECTIVE_DATETIME,'')+ CASE WHEN @EFFECTIVE_DATETIME!='' THEN  CASE WHEN @EXPIRY_DATE!='' THEN ' - ' ELSE '' END ELSE ''END +

			ISNULL(@EXPIRY_DATE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr><td>Moeda</td><td>!</td>'+
			'<td>N.Item</td><td>!</td>'+
			'<td ALIGN=RIGHT>Imp.Segurada</td><td>!</td>'+
			'<td>Sinistro IRB</td><td>!</td>'+
			'<td>Dt.Sinistro</td><td>!</td>'+
			'<td>Dt.Aviso</td>'+
			'</tr><tr>'+
			'<td>R$</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@N_ITEM),'')+ '</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@Limit),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@SINISTRO_IRB),'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DT_SINISTRO,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DT_AVISO,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<hr width=99% />'+
			'DADOS ESPECIFICOS VG/APC'+
			'<hr width=99% />'+
			'<table width=100%>'+
			'<tr><td>Nome do Estipulante</td><td>!</td>'+
			'<td>Garantia reclamada do Segurado sinistrado</td></tr>'+
			'<tr><td>'+ 
			ISNULL(@CARRIER_NAME,'')+'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DETAIL_TYPE_DESCRIPTION,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99%/>'+
			'<table width=100%>'+
			'<tr><td class=style2>Nome do Segurado Principal</td><td>!</td>'+
			'<td>Dt.Nascimento</td></tr>'+
			'</tr><tr>'+
			'<td class=style2>'+ 
			ISNULL(@CLAIMANT_NAME,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@CO_APPL_DOB,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99% />'+
			'<table width=100%>'+
			'<tr><td class=style2>Nome do Segurado Sinistrado</td><td>!</td>'+
			'<td>Dt.Nascimento</td></tr>'+
			'<tr><td class=style2>'+
			ISNULL(@CLAIMANT_NAME,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@CO_APPL_DOB,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99% />'+
			'<table width=99%>'+
			'<tr>'+
			'<td>Participações</td><td>!</td>'+
			'<td>VI. Total Lider</td><td>!</td>'+
			'<td>Part.Cosseg (R$)</td><td>!</td>'+
			'<td>Part .Qt .Moeda</td><td>!</td>'+
			'<td>Dt. Base</td><td>!</td>'+
			'<td>Fat. Index.</td></tr>'+
			'<tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=90% /></td></tr>'+
			'<tr><td>Estimativa</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @OUT_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@OUT_COVERAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @RESV_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_COVERAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_ESTIMATIVE!=0.00 THEN CONVERT(VARCHAR,@MOEDA_ESTIMATIVE) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Indenização</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td>'+
			'</tr><tr>'+
			'<td>Honorários</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@PROFESSIONAL_SERVICES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			 ISNULL(CASE WHEN @RESV_PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@RESV_PROFESSIONAL_SERVICES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_HONOR!=0.00 THEN CONVERT(VARCHAR,@MOEDA_HONOR) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Despesas</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @LOSS_SALVAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@LOSS_SALVAGE_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @RESV_LOSS_SALAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_LOSS_SALAGE_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @MOEDA_DESPESAS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_DESPESAS) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'<tr><td>Ressarcimento</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @OUT_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@OUT_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @RESV_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @MOEDA_RESSARCIMENTO!=0.00 THEN CONVERT(VARCHAR,@MOEDA_RESSARCIMENTO) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Salvados</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @OUT_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@OUT_SALVAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @RESV_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_SALVAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @MOEDA_SALVADOS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_SALVADOS) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr><tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td align=right><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<tr><td>Total Geral</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @TOTAL_GEREAL_LIDER!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GEREAL_LIDER) ELSE '' END,'')+ 
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @TOTAL_GERAL_COSSEG!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GERAL_COSSEG) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @TOTAL_GERAL_MOEDA!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GERAL_MOEDA) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td>F-A-J-</td><td></td>'+
			'<td></td>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'</table>'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Colocar a nossa disposição quantia referente a sua particioação até  0000000<br />'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Sinistro Sorteio já liquidado. A cota parte dessa congenere sera debitada<br />'+
			' &nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; através do L.C.C (Lancto.Contra Corrente).<br />'+
			'<table width=60%><tr><td>Observação:</td><td>C.S </td><td>'+
			ISNULL(@CLAIM_NUMBER,'')+
			'</td><td>N.Carta Aviso</td><td>'+ISNULL(@LETTERSEQUENCENUMBER,'')+'</td><td>O.E.</td><td>'+
			ISNULL(@BRANCH_CODE,'')+
			'</td></tr></table>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			ISNULL(@CITY,'')+
			'</td>'+
			'<td>'+
			ISNULL(@BRANCH_CITY_DETAIL,'')+
			'</td>'+
			'<td></td>'+
			'</tr>'+
			'<tr>'+
			'<td colspan=2><hr width=70% /></td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<td>Local e Data</td>'+
			'<td></td>'+
			'<td>Assinatura</td>'+
			'</tr>'+
			'</table>'+
			'</td>'+
			'</tr>'+
			'</table>'+
			'</body>'+
			'</html>'
END
--select @strHTML20 AS LETTER20
--Letter Number 21 HTML
IF(@PROCESS_TYPE='CLM_LETTER_21')
    BEGIN
		--DECLARE @strHTML VARCHAR(8000)
		set @strHTML=
			'<html>'+
			'<head>'+
			'<style type=text/css>'+
			'body{font-family:Arial; font-size:12pt; margn-left:0.5cm;margin-top:1.0cm;margin-right:0.5cm;margin-bottom:0.5cm;}'+
			'hr {border-top: 1px dashed #000; margin-left:0px; text-align: left}'+
			'.style1{width:57%;}'+
			'.style2{width:30%;}'+
			'.style3{width:45%;}'+
			'.style4{width:15%;}'+
			'</style>'+
			'</head>'+
			'<body>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			'0000000000000000000000000000000<br />'+
			'À Cosseguradora<br />'+
			'<table  width=100%>'+
			'<tr>'+
			'<td colspan=3><table width=100%><tr><td>'+
			ISNULL(@REIN_COMAPANY_CODE,'')+
			'</td><td>'+CASE WHEN @REIN_COMAPANY_CODE!='' THEN ' - ' ELSE '' END+'</td><td></td><td>' +
			ISNULL(@REIN_COMAPANY_NAME,'')+
			'</td> <td>Sinistro Lider</td><td>'+
			CASE WHEN @OFFCIAL_CLAIM_NUMBER!='' THEN ISNULL(@OFFCIAL_CLAIM_NUMBER,'') ELSE '' END+
			'</td></tr></table></td>'+
			'</tr>'+
			'<tr>'+
			'<td>Ref&nbsp;: </td>'+
			'<td>(&nbsp;X&nbsp;)&nbsp;&nbsp;Aviso de Sinistro</td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp;Despesas/Honorários</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp;Cobrança de Sinistro</td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp;Ressarc.Salvados</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp; Pagto. parcial/Adiantamento</td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp; Encerramento sem Indenização</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp; Pagamento Final</td>'+
			'<td>'+CASE WHEN @ACTION_ON_PAYMENT=168 THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END +'&nbsp;&nbsp; Reabertura de Sinistro</td>'+
			'</tr>'+
			'</table>'+
			'<hr width=100% />'+
			'DADOS GERAIS<br />'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr>'+
			'<td class=style1>Nome do Segurado </td><td>!</td>'+
			'<td>Nome Ramo</td><td>!</td>'+
			'<td align=center>% Part. Congenere</td>'+
			'</tr><tr>'+
			'<td class=style1>'+
			ISNULL(@CARRIER_NAME,'')+'</td><td>!</td>'+
			'<td>'+
			ISNULL(@NOME_RAMO,'')+
			'</td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@PART_CONGENERE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr><td class=style4>N.Apolice</td><td>!</td>'+
			'<td> N.Ordem Apolice</td><td>!</td>'+
			'<td> N. Endosso</td><td>!</td>'+
			'<td> N.Ordem Endosso</td><td>!</td>'+
			'<td align=center> Vigência</td></tr>'+
			'<tr><td  class=style4 align=right>'+
			ISNULL(@POLICY_NUMBER,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(@N_ORDERM_APOLICE,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(@N_ENDOSSO,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@EFFECTIVE_DATETIME,'')+ CASE WHEN @EFFECTIVE_DATETIME!='' THEN  CASE WHEN @EXPIRY_DATE!='' THEN ' - ' ELSE '' END ELSE ''END +

			ISNULL(@EXPIRY_DATE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr><td>Moeda</td><td>!</td>'+
			'<td class=style2>N.Item</td><td>!</td>'+
			'<td align=right>Imp.Segurada</td><td>!</td>'+
			'<td>Sinistro IRB</td><td>!</td>'+
			'<td>Dt.Sinistro</td><td>!</td>'+
			'<td>Dt.Aviso</td>'+
			'</tr><tr>'+
			'<td>R$</td><td>!</td>'+
			'<td class=style2>'+
			ISNULL(CONVERT(VARCHAR,@N_ITEM),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@Limit),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@SINISTRO_IRB),'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@DT_SINISTRO,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@DT_AVISO,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<hr width=99% />'+
			'DADOS ESPECIFICOS INC/LC/RD/R.ENG./ROUBO'+
			'<hr width=99% />'+
			'<table width=100%>'+
			'<tr><td class=style3>Local de Ocorrencia</td><td>!</td>'+
			'<td>Cidade</td><td>!</td>'+
			'<td>UF</td></tr>'+
			'<tr><td class=style3>'+
			ISNULL(@LOCAL_DE_OCORRENCIA,'')+
			'</td><td>!</td>'+
			'<td>' +
			ISNULL(@CIDADE,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@UF,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99%/>'+
			'<table width=100%>'+
			'<tr><td>N.Planta</td><td>!</td>'+
			'<td>Bens Sinistrados</td><td>!</td>'+
			'<td>Natureza do Evento</td><td>!</td>'+
			'<td>Ramo Atividade</td>'+
			'</tr><tr>'+
			'<td></td><td>!</td>'+
			'<td>' +
			ISNULL(@BENS_SINISTRADOS,'')+
			'</td><td>!</td>'+
			'<td>' +
			ISNULL(@DETAIL_TYPE_DESCRIPTION,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@ACTIVITY_TYPE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99% />'+
			'<table width=99%>'+
			'<tr>'+
			'<td>Participações</td><td>!</td>'+
			'<td>VI. Total Lider</td><td>!</td>'+
			'<td>Part.Cosseg (R$)</td><td>!</td>'+
			'<td>Part .Qt .Moeda</td><td>!</td>'+
			'<td>Dt. Base</td><td>!</td>'+
			'<td >Fat. Index.</td></tr>'+
			'<tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=99% /></td></tr>'+
			'<tr><td>Estimativa</td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CASE WHEN @OUT_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@OUT_COVERAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CASE WHEN @RESV_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_COVERAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CASE WHEN @MOEDA_ESTIMATIVE!=0.00 THEN CONVERT(VARCHAR,@MOEDA_ESTIMATIVE) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Indenização</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td>'+
			'</tr><tr>'+
			'<td>Honorários</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@PROFESSIONAL_SERVICES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			 ISNULL(CASE WHEN @RESV_PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@RESV_PROFESSIONAL_SERVICES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_HONOR!=0.00 THEN CONVERT(VARCHAR,@MOEDA_HONOR) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Despesas</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @LOSS_SALVAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@LOSS_SALVAGE_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CASE WHEN @RESV_LOSS_SALAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_LOSS_SALAGE_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CASE WHEN @MOEDA_DESPESAS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_DESPESAS) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'<tr><td>Ressarcimento</td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CASE WHEN @OUT_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@OUT_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @RESV_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CASE WHEN @MOEDA_RESSARCIMENTO!=0.00 THEN CONVERT(VARCHAR,@MOEDA_RESSARCIMENTO) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Salvados</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @OUT_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@OUT_SALVAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @RESV_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_SALVAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CASE WHEN @MOEDA_SALVADOS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_SALVADOS) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr><tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td align=right><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<tr><td>Total Geral</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @TOTAL_GEREAL_LIDER!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GEREAL_LIDER) ELSE '' END,'')+ 
			'</td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CASE WHEN @TOTAL_GERAL_COSSEG!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GERAL_COSSEG) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @TOTAL_GERAL_MOEDA!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GERAL_MOEDA) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td>F-A-J-</td><td></td>'+
			'<td></td>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'</table>'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Colocar a nossa disposição quantia referente a sua particioação até  00000000<br />'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Sinistro Sorteio já liquidado. A cota parte dessa congenere sera debitada<br />'+
			' &nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; através do L.C.C (Lancto.Contra Corrente).<br />'+
			'<table width=60%><tr><td>Observação:</td><td>C.S </td><td>'+
			ISNULL(@CLAIM_NUMBER,'')+
			'</td><td>N.Carta Aviso</td><td>'+ISNULL(@LETTERSEQUENCENUMBER,'')
			--CASE WHEN @PROCESS_TYPE='CLM_LETTER_21' THEN ISNULL(@LETTER_SEQUENCE_NO1,'')
			-- ELSE 
			--   CASE WHEN @PROCESS_TYPE='CLM_LETTER_26' THEN ISNULL(@LETTER_SEQUENCE_NO,'') 
			--      ELSE 'vbn' 
			--    END  
			-- END 
			+'</td><td>O.E.</td><td>'+
			ISNULL(@BRANCH_CODE,'')+
			'</td></tr></table>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>' 	+	ISNULL(@CITY,'')+ '</td>'+
			'<td>'+
			ISNULL(@BRANCH_CITY_DETAIL,'')+
			'</td>'+
			'<td></td>'+
			'</tr>'+
			'<tr>'+
			'<td colspan=2><hr width=70% /></td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<td>Local e Data</td>'+
			'<td></td>'+
			'<td>Assinatura</td>'+
			'</tr>'+
			'</table>'+
			'</td>'+
			'</tr>'+
			'</table>'+
			'</body>'+
			'</html>'
END
--SELECT @strHTML21 AS LETTER21

--Letter Number 22 HTML
IF(@PROCESS_TYPE='CLM_LETTER_22')
    BEGIN
		--DECLARE @strHTML VARCHAR(8000)
		SET @strHTML =
			'<html>'+
			'<head>'+
			'<style type=text/css>'+
			'body{font-family:Arial; font-size:12px; margn-left:0.5cm;margin-top:1.0cm;margin-right:0.5cm;margin-bottom:0.5cm;}'+
			'hr {border-top: 1px dashed #000; margin-left:0px; text-align: left}'+
			'.style1{width:57%;}'+
			'.style2{width:30%;}'+
			'.style3{width:15%;}'+
			'</style>'+
			'</head>'+
			'<body>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			'0000000000000000000000000000000<br />'+
			'À Cosseguradora<br />'+
			'<table  width=100%>'+
			'<tr>'+
			'<td colspan=3><table width=100%><tr><td>'+
			ISNULL(@REIN_COMAPANY_CODE,'')+ '</td><td>'+CASE WHEN @REIN_COMAPANY_CODE!='' THEN ' - ' ELSE '' END+'</td><td></td><td>' +
			ISNULL(@REIN_COMAPANY_NAME,'')+
			'</td> <td>Sinistro Lider</td><td>'+
			CASE WHEN @OFFCIAL_CLAIM_NUMBER!='' THEN ISNULL(@OFFCIAL_CLAIM_NUMBER,'') ELSE '' END+
			'</td></tr></table></td>'+
			'</tr>'+
			'<tr>'+
			'<td>Ref: </td>'+
			'<td>(&nbsp;X&nbsp;)&nbsp;&nbsp;Aviso de Sinistro</td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp;Despesas/Honorários</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp;Cobrança de Sinistro</td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp;Ressarc.Salvados</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp; Pagto. parcial/Adiantamento</td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp; Encerramento sem Indenização</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp; Pagamento Final</td>'+
			'<td>'+CASE WHEN @ACTION_ON_PAYMENT=168 THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END +'&nbsp;&nbsp; Reabertura de Sinistro</td>'+
			'</tr>'+
			'</table>'+
			'<hr width=100% />'+
			'DADOS GERAIS<br />'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr>'+
			'<td class=style1>Nome do Segurado </td><td>!</td>'+
			'<td>Nome Ramo</td><td>!</td>'+
			'<td align=center>% Part. Congenere</td>'+
			'</tr><tr>'+
			'<td class=style1>'+
			ISNULL(@CARRIER_NAME,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@NOME_RAMO,'')+
			'</td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@PART_CONGENERE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr><td class=style3>N.Apolice</td><td>!</td>'+
			'<td>N.Ordem Apolice</td><td>!</td>'+
			'<td>N. Endosso</td><td>!</td>'+
			'<td>N.Ordem Endosso</td><td>!</td>'+
			'<td align=center>Vigência</td></tr>'+
			'<tr><td class=style3  align=right>'+
			ISNULL(@POLICY_NUMBER,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(@N_ORDERM_APOLICE,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(@N_ENDOSSO,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@EFFECTIVE_DATETIME,'')+ CASE WHEN @EFFECTIVE_DATETIME!='' THEN  CASE WHEN @EXPIRY_DATE!='' THEN ' - ' ELSE '' END ELSE ''END +

			ISNULL(@EXPIRY_DATE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100% cellspacing=5>'+
			'<tr><td>Moeda</td><td>!</td>'+
			'<td class=style2>N.Item</td><td>!</td>'+
			'<td align=right>Imp.Segurada</td><td>!</td>'+
			'<td align=right>Sinistro IRB</td><td>!</td>'+
			'<td>Dt.Sinistro</td><td>!</td>'+
			'<td>Dt.Aviso</td>'+
			'</tr><tr>'+
			'<td>R$</td><td>!</td>'+
			'<td class=style2  align=right>'+
			ISNULL(CONVERT(VARCHAR,@N_ITEM),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@Limit),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@SINISTRO_IRB),'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@DT_SINISTRO,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@DT_AVISO,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<hr width=99% />'+
			'DADOS ESPECIFICOS AUT/RCV'+
			'<hr width=99% />'+
			'<table width=100%>'+
			'<tr><td>Marca Veiculo</td><td>!</td>'+
			'<td>N.Placa</td><td>!</td>'+
			'<td>N.Chassis</td><td>!</td>'+
			'<td>Ano</td><td>!</td>'+
			'<td>Tipo de Ocorrencia</td></tr>'+
			'<tr><td>'+
			ISNULL(@MARCA_VEICULO,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@N_PLACA,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(@N_CHASSIS,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(@ANO,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DETAIL_TYPE_DESCRIPTION,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99% />'+
			'<table width=99%>'+
			'<tr>'+
			'<td>Participações</td><td>!</td>'+
			'<td>VI. Total Lider</td><td>!</td>'+
			'<td>Part.Cosseg (R$)</td><td>!</td>'+
			'<td>Part .Qt .Moeda</td><td>!</td>'+
			'<td>Dt. Base</td><td>!</td>'+
			'<td>Fat. Index.</td></tr>'+
			'<tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td></tr>'+
			'<tr><td>Estimativa</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @OUT_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@OUT_COVERAGES) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @RESV_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_COVERAGES) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @MOEDA_ESTIMATIVE!=0.00 THEN CONVERT(VARCHAR,@MOEDA_ESTIMATIVE) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right></td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+ 
			'<td></td>'+
			'</tr><tr>'+
			'<td>Indenização</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td>'+
			'</tr><tr>'+
			'<td>Honorários</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@PROFESSIONAL_SERVICES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			 ISNULL(CASE WHEN @RESV_PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@RESV_PROFESSIONAL_SERVICES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_HONOR!=0.00 THEN CONVERT(VARCHAR,@MOEDA_HONOR) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Despesas</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @LOSS_SALVAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@LOSS_SALVAGE_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @RESV_LOSS_SALAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_LOSS_SALAGE_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_DESPESAS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_DESPESAS) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'<tr><td>Ressarcimento</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @OUT_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@OUT_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @RESV_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_RESSARCIMENTO!=0.00 THEN CONVERT(VARCHAR,@MOEDA_RESSARCIMENTO) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Salvados</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @OUT_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@OUT_SALVAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @RESV_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_SALVAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_SALVADOS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_SALVADOS) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr><tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td align=right><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<tr><td>Total Geral</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @TOTAL_GEREAL_LIDER!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GEREAL_LIDER) ELSE '' END,'')+ 
			'</td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CASE WHEN @TOTAL_GERAL_COSSEG!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GERAL_COSSEG) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @TOTAL_GERAL_MOEDA!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GERAL_MOEDA) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td>F-A-J-</td><td></td>'+
			'<td></td>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'</table>'+
			'<table width=100%><tr><td>'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Colocar a nossa disposição quantia referente a sua particioação até  0000000<br />'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Sinistro Sorteio já liquidado. A cota parte dessa congenere sera debitada<br />'+
			' &nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; através do L.C.C (Lancto.Contra Corrente).<br />'+
			'</td></tr></table>'+
			'<table width=60%><tr><td>Observação:</td><td>C.S </td><td>'+
			ISNULL(@CLAIM_NUMBER,'')+
			'</td><td>N.Carta Aviso</td><td>'+ISNULL(@LETTERSEQUENCENUMBER,'')+'</td><td>O.E.</td><td>'+
			ISNULL(@BRANCH_CODE,'')+
			'</td></tr></table>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+ 
			ISNULL(@CITY,'')+
			'</td>'+
			'<td>'+ 
			ISNULL(@BRANCH_CITY_DETAIL,'')+
			'</td>'+
			'<td></td>'+
			'</tr>'+
			'<tr>'+
			'<td colspan=2><hr width=70% /></td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<td>Local e Data</td>'+
			'<td></td>'+
			'<td>Assinatura</td>'+
			'</tr>'+
			'</table>'+
			'</td>'+
			'</tr>'+
			'</table>'+
			'</body>'+
			'</html>'
END
--SELECT @strHTML22 AS LETTER22

--Letter Number 23 HTML
IF(@PROCESS_TYPE='CLM_LETTER_23')
    BEGIN
		--DECLARE @strHTML VARCHAR(8000)
		SET @strHTML=				  
			'<html>'+
			'<head>'+
			'<style type=text/css>'+
			'body{font-family:Arial; font-size:12px; margn-left:0.5cm;margin-top:1.0cm;margin-right:0.5cm;margin-bottom:0.5cm;}'+
			'hr {border-top: 1px dashed #000; margin-left:0px; text-align: left}'+
			'.style1{width:57%;}'+
			'.style2{width:30%;}'+
			'.style3{width:15%;}'+
			'</style>'+
			'</head>'+
			'<body>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			'0000000000000000000000000000000<br />'+
			'À Cosseguradora<br />'+
			'<table  width=100%>'+
			'<tr>'+
			'<td colspan=3><table width=100%><tr><td>'+
			ISNULL(@REIN_COMAPANY_CODE,'')+ '</td><td>'+CASE WHEN @REIN_COMAPANY_CODE!='' THEN ' - ' ELSE '' END+'</td><td></td><td>' +

			ISNULL(@REIN_COMAPANY_NAME,'')+
			'</td> <td>Sinistro Lider</td><td>'+
			CASE WHEN @OFFCIAL_CLAIM_NUMBER!='' THEN ISNULL(@OFFCIAL_CLAIM_NUMBER,'') ELSE '' END+
			'</td></tr></table></td>'+
			'</tr>'+
			'<tr>'+
			'<td>Ref: </td>'+
			'<td>(X)Aviso de Sinistro</td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp;Despesas/Honorários</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp;Cobrança de Sinistro</td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp;Ressarc.Salvados</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp; Pagto. parcial/Adiantamento</td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp; Encerramento sem Indenização</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp; Pagamento Final</td>'+
			'<td>'+CASE WHEN @ACTION_ON_PAYMENT=168 THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END +'&nbsp;&nbsp; Reabertura de Sinistro</td>'+
			'</tr>'+
			'</table>'+
			'<hr width=100% />'+
			'DADOS GERAIS<br />'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr>'+
			'<td class=style1>Nome do Segurado </td><td>!</td>'+
			'<td>Nome Ramo</td><td>!</td>'+
			'<td align=center>% Part. Congenere</td>'+
			'</tr><tr>'+
			'<td class=style1>'+
			ISNULL(@CARRIER_NAME,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@NOME_RAMO,'')+
			'</td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@PART_CONGENERE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr><td class=style3>N.Apolice</td><td>!</td>'+
			'<td>N.Ordem Apolice</td><td>!</td>'+
			'<td>N. Endosso</td><td>!</td>'+
			'<td>N.Ordem Endosso</td><td>!</td>'+
			'<td align=center>Vigência</td></tr>'+
			'<tr><td class=style3  align=right>'+
			ISNULL(@POLICY_NUMBER,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(@N_ORDERM_APOLICE,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(@N_ENDOSSO,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@EFFECTIVE_DATETIME,'')+ CASE WHEN @EFFECTIVE_DATETIME!='' THEN  CASE WHEN @EXPIRY_DATE!='' THEN ' - ' ELSE '' END ELSE ''END +

			ISNULL(@EXPIRY_DATE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100% cellspacing=5>'+
			'<tr><td>Moeda</td><td>!</td>'+
			'<td class=style2>N.Item</td><td>!</td>'+
			'<td align=right>Imp.Segurada</td><td>!</td>'+
			'<td align=right>Sinistro IRB</td><td>!</td>'+
			'<td>Dt.Sinistro</td><td>!</td>'+
			'<td>Dt.Aviso</td>'+
			'</tr><tr>'+
			'<td>R$</td><td>!</td>'+
			'<td class=style2>'+ 
			ISNULL(CONVERT(VARCHAR,@N_ITEM_TRANS),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@Limit),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@SINISTRO_IRB),'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DT_SINISTRO,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@DT_AVISO,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<hr width=99% />'+
			'DADOS ESPECIFICOS <br/><div>TRANSP/CASCO/AERONAUTICO/P.RURAL/CRED.EXP./R.C.GERAL/CRED.INT.</div>'+
			'<hr width=99% />'+
			'<table width=99%><tr>'+
			'<td>N.Averbacao Certif.</td><td>!</td>'+
			'<td>Meio Ttransporte</td><td>!</td>'+
			'<td>N.Placa</td><td>!</td>'+
			'<td>PrefiXo</td><td>!</td>'+
			'<td>Nome de Embrcação</td></tr>'+
			'<tr><td>'+ 
			ISNULL(@N_AVERBACAO_CERTIF,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@MEIO_TTRANSPORTE,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@N_PLACA,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@PREFIXO,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@NOME_DA_EMBARCACAO,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99%/>'+
			'<table width=99%>'+
			'<tr><td>Empresa Transportadora</td><td>!</td>'+
			'<td>Mercadoria Sinisrtrada</td><td>!</td>'+
			'<td>Natureza Danos</td><td>!</td>'+
			'<td>Tipo Cobertura</td></tr>'+
			'<tr><td>'+ 
			ISNULL(@EMPRESA_TRANS,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@MERCADORIA_SINISTRADA,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@DETAIL_TYPE_DESCRIPTION,'')+
			'</td><td>!</td>'+
			'<td>CLAUSULA A +</td></tr>'+
			'</table>'+
			'<hr WIDTH=99%/>'+
			'<table width=99%>'+
			'<tr><td>Origem</td><td>!</td>'+
			'<td>Destino</td><td>!</td>'+
			'<td colspan=3>Local de Ocorrencia</td><td>!</td>'+
			'<td>Cidade</td><td>!</td>'+
			'<td>UF</td></tr>'+
			'<tr><td>'+
			ISNULL(@ORIGEM,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DESTINO,'')+
			'</td><td>!</td>'+
			'<td colspan=3>'+ 
			ISNULL(@LOCAL_DE_OCORRENCIA,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@CIDADE,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@UF,'')+
			'</td></tr>'+
			'<tr><td>Data Saida</td><td>!</td>'+
			'<td>Data Chegada</td><td>!</td>'+
			'<td>Data Vistoria</td><td>!</td>'+
			'<td>Local de Vistoria</td></tr>'+
			'<tr><td>'+
			ISNULL(@DATA_SAIDA,'')+ 
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DATA_CHEGADA,'')+ 
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DATA_VISTORIA,'')+
			'</td><td>!</td>'+
			'<td>SANTOS</td></tr>'+
			'</table>'+
			'<hr WIDTH=99% />'+
			'<table width=99%>'+
			'<tr>'+
			'<td>Participações</td><td>!</td>'+
			'<td>VI. Total Lider</td><td>!</td>'+
			'<td>Part.Cosseg (R$)</td><td>!</td>'+
			'<td>Part .Qt .Moeda</td><td>!</td>'+
			'<td>Dt. Base</td><td>!</td>'+
			'<td>Fat. Index.</td></tr>'+
			'<tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td></tr>'+
			'<tr><td>Estimativa</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @OUT_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@OUT_COVERAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @RESV_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_COVERAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_ESTIMATIVE!=0.00 THEN CONVERT(VARCHAR,@MOEDA_ESTIMATIVE) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Indenização</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td>'+
			'</tr><tr>'+
			'<td>Honorários</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@PROFESSIONAL_SERVICES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			 ISNULL(CASE WHEN @RESV_PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@RESV_PROFESSIONAL_SERVICES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_HONOR!=0.00 THEN CONVERT(VARCHAR,@MOEDA_HONOR) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Despesas</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @LOSS_SALVAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@LOSS_SALVAGE_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @RESV_LOSS_SALAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_LOSS_SALAGE_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_DESPESAS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_DESPESAS) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'<tr><td>Ressarcimento</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @OUT_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@OUT_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @RESV_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_RESSARCIMENTO!=0.00 THEN CONVERT(VARCHAR,@MOEDA_RESSARCIMENTO) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Salvados</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @OUT_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@OUT_SALVAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @RESV_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_SALVAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_SALVADOS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_SALVADOS) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr><tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td align=right><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<tr><td>Total Geral</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @TOTAL_GEREAL_LIDER!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GEREAL_LIDER) ELSE '' END,'')+ 
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @TOTAL_GERAL_COSSEG!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GERAL_COSSEG) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @TOTAL_GERAL_MOEDA!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GERAL_MOEDA) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td>F-A-J-</td><td></td>'+
			'<td></td>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'</table>'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Colocar a nossa disposição quantia referente a sua particioação até  0000000<br />'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Sinistro Sorteio já liquidado. A cota parte dessa congenere sera debitada<br />'+
			' &nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; através do L.C.C (Lancto.Contra Corrente).<br />'+
			'<table width=60%><tr><td>Observação:</td><td>C.S </td><td>'+
			ISNULL(@CLAIM_NUMBER,'')+
			'</td><td>N.Carta Aviso</td><td>'+
			CASE WHEN @PROCESS_TYPE='CLM_LETTER_23' THEN  ISNULL(@LETTERSEQUENCENUMBER,'0') ELSE CASE WHEN @PROCESS_TYPE='CLM_LETTER_29' THEN ISNULL(@LETTERSEQUENCENUMBER,'') ELSE '1' END END  +
			'</td><td>O.E.</td><td>'+ 
			ISNULL(@BRANCH_CODE,'')+
			'</td></tr></table>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+ 
			ISNULL(@CITY,'')+
			'</td>'+
			'<td>'+
			ISNULL(@BRANCH_CITY_DETAIL,'')+
			'</td>'+
			'<td></td>'+
			'</tr>'+
			'<tr>'+
			'<td colspan=2><hr width=70% /></td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<td>Local e Data</td>'+
			'<td></td>'+
			'<td>Assinatura</td>'+
			'</tr>'+
			'</table>'+
			'</td>'+
			'</tr>'+
			'</table>'+
			'</body>'+
			'</html>'
END
--SELECT @strHTML23 AS LETTER23

--Letter Number 24 HTML
IF(@PROCESS_TYPE='CLM_LETTER_24')
    BEGIN
		--DECLARE @strHTML VARCHAR(8000)
		SET @strHTML= 		
			'<html>'+
			'<head>'+
			'<style type=text/css>'+
			'body{font-family:Arial; font-size:12px; margn-left:0.5cm;margin-top:1.0cm;margin-right:0.5cm;margin-bottom:0.5cm;}'+
			'hr {border-top: 1px dashed #000; margin-left:0px; text-align: left}'+
			'.style1{width:52%;}'+
			'.style2{width:27%;}'+
			'.style3{width:32%;}'+
			'</style>'+
			'</head>'+
			'<body>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			'8000000000000000000000000010380<br />'+
			'À Cosseguradora<br />'+
			'<table  width=100%>'+
			'<tr>'+
			'<td colspan=3><table width=100%><tr><td>'+
			ISNULL(@REIN_COMAPANY_CODE,'')+ '</td><td>'+CASE WHEN @REIN_COMAPANY_CODE!='' THEN ' - ' ELSE '' END+'</td><td></td><td>' +
			ISNULL(@REIN_COMAPANY_NAME,'')+						
			'</td> <td>Sinistro Lider</td><td>'+
			CASE WHEN @OFFCIAL_CLAIM_NUMBER!='' THEN ISNULL(@OFFCIAL_CLAIM_NUMBER,'') ELSE '' END+
			'</td></tr></table></td>'+
			'</tr>'+
			'<tr>'+
			'<td>Ref&nbsp;: </td>'+
			'<td>(X)Aviso de Sinistro</td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp;Despesas/Honorários</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp;Cobrança de Sinistro</td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp;Ressarc.Salvados</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp; Pagto. parcial/Adiantamento</td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp; Encerramento sem Indenização</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp; Pagamento Final</td>'+
			'<td>'+CASE WHEN @ACTION_ON_PAYMENT=168 THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END +'&nbsp;&nbsp; Reabertura de Sinistro</td>'+
			'</tr>'+
			'</table>'+
			'<hr width=100% />'+
			'DADOS GERAIS<br />'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr>'+
			'<td class=style1>Nome do Segurado </td><td>!</td>'+
			'<td>Nome Ramo</td><td>!</td>'+
			'<td align=center>% Part. Congenere</td>'+
			'</tr><tr>'+
			'<td class=style1>'+ 
			ISNULL(@CARRIER_NAME,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@NOME_RAMO,'')+
			'</td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@PART_CONGENERE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+    
			'<table width=100%>'+
			'<tr><td>N.Apolice</td><td>!</td>'+
			'<td>N.Ordem Apolice</td><td>!</td>'+
			'<td>N. Endosso</td><td>!</td>'+
			'<td>N.Ordem Endosso</td><td>!</td>'+
			'<td align=center>Vigência</td></tr>'+
			'<tr><td align=right>'+ 
			ISNULL(@POLICY_NUMBER,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(@N_ORDERM_APOLICE,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(@N_ENDOSSO,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@EFFECTIVE_DATETIME,'')+ CASE WHEN @EFFECTIVE_DATETIME!='' THEN  CASE WHEN @EXPIRY_DATE!='' THEN ' - ' ELSE '' END ELSE ''END +

			ISNULL(@EXPIRY_DATE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100% cellspacing=5>'+
			'<tr><td>Moeda</td><td>!</td>'+
			'<td class=style2>N.Item</td><td>!</td>'+
			'<td align=right>Imp.Segurada</td><td>!</td>'+
			'<td align=right>Sinistro IRB</td><td>!</td>'+
			'<td>Dt.Sinistro</td><td>!</td>'+
			'<td>Dt.Aviso</td>'+
			'</tr><tr>'+
			'<td>R$</td><td>!</td>'+
			'<td class=style2  align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@N_ITEM_MERATIME),'')+
			'</td><td>!</td>'+
			'<td align=right> '+          
			ISNULL(CONVERT(VARCHAR,@Limit),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@SINISTRO_IRB),'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@DT_SINISTRO,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DT_AVISO,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<hr width=99% />'+
			'DADOS ESPECIFICOS <br/><div>TRANSP/CASCO/AERONAUTICO/P.RURAL/CRED.EXP./R.C.GERAL/CRED.INT.</div>'+
			'<hr width=99% />'+
			'<table width=99%><tr>'+
			'<td>N.Averbacao Certif.</td><td>!</td>'+
			'<td>Meio Ttransporte</td><td>!</td>'+
			'<td>N.Placa</td><td>!</td>'+
			'<td>PrefiXo</td><td>!</td>'+
			'<td>Nome de Embrcação</td></tr>'+
			'<tr><td>'+ 
			ISNULL(@N_AVERBACAO_CERTIF,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@MEIO_TTRANSPORTE,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@N_PLACA,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@PREFIXO,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@NOME_DA_EMBARCACAO,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99%/>'+
			'<table width=99%>'+
			'<tr><td>Empresa Transportadora</td><td>!</td>'+
			'<td class=style3>Mercadoria Sinisrtrada</td><td>!</td>'+
			'<td>Natureza Danos</td><td>!</td>'+
			'<td>Tipo Cobertura</td></tr>'+
			'<tr><td>'+
			 ISNULL(@EMPRESA_TRANS,'')+
			  '</td><td>!</td>'+
			'<td>'+
			 ISNULL(@MERCADORIA_SINISTRADA,'')+
			 '</td><td>!</td>'+
			'<td>'+ 
			--ISNULL(@DETAIL_TYPE_DESCRIPTION,'')+
			'</td><td>!</td>'+
			'<td>RC OPERADOR P</td></tr>'+
			'</table>'+
			'<hr WIDTH=99%/>'+
			'<table width=99%>'+
			'<tr><td>Origem</td><td>!</td>'+
			'<td>Destino</td><td>!</td>'+
			'<td colspan=3>Local de Ocorrencia</td><td>!</td>'+
			'<td>Cidade</td><td>!</td>'+
			'<td>UF</td></tr>'+
			'<tr><td>'+
			 --ISNULL(@ORIGEM,'')+
			 '</td><td>!</td>'+
			'<td>'+
			 --ISNULL(@DESTINO,'')+
			  '</td><td>!</td>'+
			'<td colspan=3>'+
			 ISNULL(@LOCAL_DE_OCORRENCIA,'')+
			  '</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@CIDADE,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@UF,'')+
			 '</td></tr>'+
			'<tr><td>Data Saida</td><td>!</td>'+
			'<td>Data Chegada</td><td>!</td>'+
			'<td>Data Vistoria</td><td>!</td>'+
			'<td>Local de Vistoria</td></tr>'+
			'<tr><td>'+ 
			--ISNULL(@DATA_SAIDA,'')+ 
			'</td><td>!</td>'+
			'<td>'+ 
			--ISNULL(@DATA_CHEGADA,'')+
			'</td><td>!</td>'+
			'<td>'+
			-- ISNULL(@DATA_VISTORIA,'')+
			  '</td><td>!</td>'+
			'<td>ESTRADA DA ILHA DA MADEIRA S/N</td></tr>'+
			'</table>'+
			'<hr WIDTH=99% />'+
			'<table width=99%>'+
			'<tr>'+
			'<td>Participações</td><td>!</td>'+
			'<td>VI. Total Lider</td><td>!</td>'+
			'<td>Part.Cosseg (R$)</td><td>!</td>'+
			'<td>Part .Qt .Moeda</td><td>!</td>'+
			'<td>Dt. Base</td><td>!</td>'+
			'<td>Fat. Index.</td></tr>'+
			'<tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td></tr>'+
			'<tr><td>Estimativa</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @OUT_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@OUT_COVERAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @RESV_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_COVERAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			 ISNULL(CASE WHEN @MOEDA_ESTIMATIVE!=0.00 THEN CONVERT(VARCHAR,@MOEDA_ESTIMATIVE) ELSE '' END,'')+
			  '</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			 ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			 '</td>'+
			'</tr><tr>'+
			'<td>Indenização</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td>'+
			'</tr><tr>'+
			'<td>Honorários</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@PROFESSIONAL_SERVICES) ELSE '' END,'')+
			 '</td><td>!</td>'+
			'<td align=right>'+ 
			 ISNULL(CASE WHEN @RESV_PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@RESV_PROFESSIONAL_SERVICES) ELSE '' END,'')+
			 '</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_HONOR!=0.00 THEN CONVERT(VARCHAR,@MOEDA_HONOR) ELSE '' END,'')+
			 '</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Despesas</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @LOSS_SALVAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@LOSS_SALVAGE_SUBROGATION) ELSE '' END,'')+
			 '</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @RESV_LOSS_SALAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_LOSS_SALAGE_SUBROGATION) ELSE '' END,'')+
			 '</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_DESPESAS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_DESPESAS) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			 '</td>'+
			'</tr><tr>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'<tr><td>Ressarcimento</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @OUT_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@OUT_SUBROGATION) ELSE '' END,'')+
			 '</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @RESV_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_RESSARCIMENTO!=0.00 THEN CONVERT(VARCHAR,@MOEDA_RESSARCIMENTO) ELSE '' END,'')+
			 '</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			 ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			 '</td>'+
			'</tr><tr>'+
			'<td>Salvados</td><td>!</td>'+
			'<td align=right>'+
			 ISNULL(CASE WHEN @OUT_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@OUT_SALVAGES) ELSE '' END,'')+
			 '</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @RESV_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_SALVAGES) ELSE '' END,'')+
			 '</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_SALVADOS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_SALVADOS) ELSE '' END,'')+
			 '</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			 ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			  '</td>'+
			'</tr><tr>'+
			'</tr><tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td align=right><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<tr><td>Total Geral</td><td>!</td>'+
			'<td align=right>'+
			 ISNULL(CASE WHEN @TOTAL_GEREAL_LIDER!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GEREAL_LIDER) ELSE '' END,'')+ 
			 '</td><td>!</td>'+
			'<td align=right>'+
			 ISNULL(CASE WHEN @TOTAL_GERAL_COSSEG!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GERAL_COSSEG) ELSE '' END,'')+
			  '</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @TOTAL_GERAL_MOEDA!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GERAL_MOEDA) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td>F-A-J-</td><td></td>'+
			'<td></td>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'</table>'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Colocar a nossa disposição quantia referente a sua particioação até  0000000<br />'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Sinistro Sorteio já liquidado. A cota parte dessa congenere sera debitada<br />'+
			' &nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; através do L.C.C (Lancto.Contra Corrente).<br />'+
			'<table width=60%><tr><td>Observação:</td><td>C.S </td><td>'+ 
			ISNULL(@CLAIM_NUMBER,'')+
			'</td><td>N.Carta Aviso</td><td>'+ISNULL(@LETTERSEQUENCENUMBER,'')+'</td><td>O.E.</td><td>'+ 
			ISNULL(@BRANCH_CODE,'')+
			'</td></tr></table>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			 ISNULL(@CITY,'')+
			  '</td>'+
			'<td>'+ 
			ISNULL(@BRANCH_CITY_DETAIL,'')+
			'</td>'+
			'<td></td>'+
			'</tr>'+
			'<tr>'+
			'<td colspan=2><hr width=70% /></td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<td>Local e Data</td>'+
			'<td></td>'+
			'<td>Assinatura</td>'+
			'</tr>'+
			'</table>'+
			'</td>'+
			'</tr>'+
			'</table>'+
			'</body>'+
			'</html>'
END
--SELECT @strHTML24 AS LETTER24

--Letter Number 25 HTML
IF(@PROCESS_TYPE='CLM_LETTER_25')
    BEGIN
		--DECLARE @strHTML VARCHAR(8000)
		SET @strHTML =
		'<html>'+
		'<head>'+
		'<style type=text/css>'+
		'body{font-family:Arial; font-size:12pt; margn-left:0.5cm;margin-top:1.0cm;margin-right:0.5cm;margin-bottom:0.5cm;}'+
		'hr {border-top: 1px dashed #000; margin-left:0px; text-align: left}'+
		'.style1{width:52%;}'+
		'.style2{width:27%;}'+
		'.style3{width:32%;}'+
		'</style>'+
		'</head>'+
		'<body>'+
		'<table width=100%>'+
		'<tr>'+
		'<td>'+
		'COMPANHIA DE SEGUROS ALIANÇA DA BAHIA <BR/> À Cosseguradora'+
		'<table  width=100%>'+
		'<tr>'+
		'<td colspan=3><table width=100%><tr><td>'+
		ISNULL(@REIN_COMAPANY_CODE,'')+ '</td><td>'+CASE WHEN @REIN_COMAPANY_CODE!='' THEN ' - ' ELSE '' END+'</td><td></td><td>' +
		ISNULL(@REIN_COMAPANY_NAME,'')+
		'</td> <td>Sinistro Lider</td><td>'+
		CASE WHEN @OFFCIAL_CLAIM_NUMBER!='' THEN ISNULL(@OFFCIAL_CLAIM_NUMBER,'') ELSE '' END+
		'</td></tr></table></td>'+
		'</tr>'+
		'<tr>'+
		'<td>Ref&nbsp;: </td>'+
		'<td>(&nbsp;&nbsp;)&nbsp;&nbsp;Aviso de Sinistro</td>'+
		'<td>'+CASE WHEN @ACTION_ON_PAYMENT=180 OR @ACTION_ON_PAYMENT=181 THEN 
			  CASE WHEN @LOSS_SALVAGE_SUBROGATION !=0.00  OR @PAYMENT_PROFESSIONAL_SERVICES!=0.00 THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END ELSE '(&nbsp;&nbsp;&nbsp;)' END+
		'&nbsp;&nbsp;Despesas/Honorários</td>'+
		'</tr>'+
		'<tr>'+
		'<td></td>'+
		'<td>(X)&nbsp;&nbsp;Cobrança de Sinistro</td>'+
		'<td>'+CASE WHEN @ACTION_ON_PAYMENT=180 OR @ACTION_ON_PAYMENT=181 THEN CASE WHEN @PAYMENT_SALVAGES!=0.00 OR @PAYMENT_SUBROGATION!=0.00
			       THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END ELSE '(&nbsp;&nbsp;&nbsp;)' END+
		'&nbsp;&nbsp;&nbsp;Ressarc.Salvados</td>'+
		'</tr>'+
		'<tr>'+
		'<td></td>'+
		'<td>'+CASE WHEN @ACTION_ON_PAYMENT=180 AND @OUT_COVERAGES!=0.00 THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END+'&nbsp;&nbsp; Pagto. parcial/Adiantamento</td>'+
		'<td>(&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;Encerramento sem Indenização</td>'+
		'</tr>'+
		'<tr>'+
		'<td></td>'+
		'<td>'+CASE WHEN @ACTION_ON_PAYMENT=181 AND @OUT_COVERAGES!=0.00  THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END +'&nbsp;&nbsp; Pagamento Final</td>'+
		'<td>(&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp; Reabertura de Sinistro</td>'+
		'</tr>'+
		'</table>'+
		'<hr width=100% />'+
		'DADOS GERAIS<br />'+
		'<hr width=100% />'+
		'<table width=100%>'+
		'<tr>'+
		'<td class=style1>Nome do Segurado </td><td>!</td>'+
		'<td>Nome Ramo</td><td>!</td>'+
		'<td align=center>% Part. Congenere</td>'+
		'</tr><tr>'+
		'<td class=style1>'+ 
		ISNULL(@CARRIER_NAME,'')+
		'</td><td>!</td>'+
		'<td>'+ 
		ISNULL(@NOME_RAMO,'')+
		'</td><td>!</td>'+
		'<td align=center>'+ 
		ISNULL(@PART_CONGENERE,'')+
		'</td></tr>'+
		'</table>'+
		'<hr width=100% />'+    
		'<table width=100%>'+
		'<tr><td>N.Apolice</td><td>!</td>'+
		'<td>N.Ordem Apolice</td><td>!</td>'+
		'<td>N. Endosso</td><td>!</td>'+
		'<td>N.Ordem Endosso</td><td>!</td>'+
		'<td align=center>Vigência</td></tr>'+
		'<tr><td align=right>'+ 
		ISNULL(@POLICY_NUMBER,'')+
		'</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(@N_ORDERM_APOLICE,'')+
		'</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(@N_ENDOSSO,'')+
		'</td><td>!</td>'+
		'<td  align=right></td><td>!</td>'+
		'<td align=center>'+
		ISNULL(@EFFECTIVE_DATETIME,'')+ CASE WHEN @EFFECTIVE_DATETIME!='' THEN  CASE WHEN @EXPIRY_DATE!='' THEN '  - ' ELSE '' END ELSE ''END +            
		ISNULL(@EXPIRY_DATE,'')+
		'</td></tr>'+
		'</table>'+
		'<hr width=100% />'+
		'<table width=100% cellspacing=5>'+
		'<tr><td>Moeda</td><td>!</td>'+
		'<td class=style2>N.Item</td><td>!</td>'+
		'<td align=right>Imp.Segurada</td><td>!</td>'+
		'<td align=right>Sinistro IRB</td><td>!</td>'+
		'<td>Dt.Sinistro</td><td>!</td>'+
		'<td>Dt.Aviso</td>'+
		'</tr><tr>'+
		'<td>R$</td><td>!</td>'+
		'<td class=style2 align=right>'+ 
		ISNULL(CONVERT(VARCHAR,@N_ITEM),'')+
		'</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CONVERT(VARCHAR,@Limit),'')+
		'</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CONVERT(VARCHAR,@SINISTRO_IRB),'')+
		'</td><td>!</td>'+
		'<td>'+ 
		ISNULL(@DT_SINISTRO,'')+
		'</td><td>!</td>'+
		'<td>'+ 
		ISNULL(@DT_AVISO,'')+
		'</td></tr>'+
		'</table>'+
		'<hr width=100% />'+
		'<table width=100%>'+
		'<tr>'+
		'<td>Participações</td><td>!</td>'+
		'<td>VI. Total Lider</td><td>!</td>'+
		'<td>Part.Cosseg (R$)</td><td>!</td>'+
		'<td>Part .Qt .Moeda</td><td>!</td>'+
		'<td>Dt. Base</td><td>!</td>'+
		'<td>Fat. Index.</td></tr>'+
		'<tr>'+
		'<td><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td></tr>'+
		'<tr><td>Estimativa</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @OUT_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@OUT_COVERAGES) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @RESV_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_COVERAGES) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @MOEDA_ESTIMATIVE!=0.00 THEN CONVERT(VARCHAR,@MOEDA_ESTIMATIVE) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right></td><td>!</td>'+
		'<td align=right>'+
		ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
		'</td>'+
		'</tr><tr>'+
		'<td>Indenização</td><td>!</td>'+
		'<td align=right>'+ISNULL(CASE WHEN @PAYMENT_COVERAGE!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_COVERAGE) ELSE '' END,'')+'</td><td>!</td>'+
		'<td align=right>'+ISNULL(CASE WHEN @RESV_PAYMENT_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_PAYMENT_COVERAGES) ELSE '' END,'')+'</td><td>!</td>'+
		'<td align=right>'+ISNULL(CASE WHEN @MOEDA_PAYMENT_ESTIMATIVE!=0.00 THEN CONVERT(VARCHAR,@MOEDA_PAYMENT_ESTIMATIVE) ELSE '' END,'')+'</td><td>!</td>'+
		'<td></td><td>!</td>'+
		'<td align=right>'+ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+'</td>'+
		'</tr><tr>'+
		'<td>Honorários</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @PAYMENT_PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_PROFESSIONAL_SERVICES) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right>'+ 
		 ISNULL(CASE WHEN @RESV_PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@RESV_PROFESSIONAL_SERVICES) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @MOEDA_HONOR!=0.00 THEN CONVERT(VARCHAR,@MOEDA_HONOR) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right></td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
		'</td>'+
		'</tr><tr>'+
		'<td>Despesas</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @PAYMENT_SALVAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_SALVAGE_SUBROGATION) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @RESV_LOSS_SALAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_LOSS_SALAGE_SUBROGATION) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @MOEDA_DESPESAS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_DESPESAS) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right></td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
		'</td>'+
		'</tr><tr>'+
		'</tr>'+
		'<tr><td align=right><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td><td>!</td>'+ 
		'<td><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td>'+
		'</tr>'+
		'<tr><td>Ressarcimento</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @PAYMENT_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_SUBROGATION) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @RESV_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_SUBROGATION) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @MOEDA_RESSARCIMENTO!=0.00 THEN CONVERT(VARCHAR,@MOEDA_RESSARCIMENTO) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right></td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
		'</td>'+
		'</tr><tr>'+
		'<td>Salvados</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @PAYMENT_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_SALVAGES) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @RESV_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_SALVAGES) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @MOEDA_SALVADOS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_SALVADOS) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right></td><td>!</td>'+
		'<td  align=right>'+ 
		ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
		'</td>'+
		'</tr><tr>'+
		'</tr><tr>'+
		'<td><hr width=100% /></td><td>!</td>'+
		'<td align=right><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td>'+
		'</tr><tr>'+
		'<tr><td>Total Geral</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @TOTAL_PAYMENT_GEREAL_LIDER!=0.00 THEN CONVERT(VARCHAR,@TOTAL_PAYMENT_GEREAL_LIDER) ELSE '' END,'')+
		'</td><td>!</td>'+ 
		'<td align=right>'+ 
		ISNULL(CASE WHEN @TOTAL_PAYMENT_GERAL_COSSEG!=0.00 THEN CONVERT(VARCHAR,@TOTAL_PAYMENT_GERAL_COSSEG) ELSE '' END,'')+ 
		'</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @TOTAL_PAYMENT_GERAL_MOEDA!=0.00 THEN CONVERT(VARCHAR,@TOTAL_PAYMENT_GERAL_MOEDA) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td>F-A-J-</td><td></td>'+
		'<td></td>'+
		'</tr>'+
		'<tr><td colspan=11><hr width=100% /></td></tr>'+
		'</table>'+
		'(&nbsp;&nbsp;X&nbsp;&nbsp;)&nbsp;&nbsp;Coloca a nossa disposição quantia referente a sua particioação até  '+
		ISNULL(@LETTER_GENERATION_DATE,'')+
		'<br />'+
		'(&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;Sinistro Sorteio já liquidado. A cota parte dessa congenere será debitada<br />'+
		' &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; através do L.C.C (Lancto.Contra Corrente).<br />'+
		'<table width=60%><tr><td>Observação&nbsp;&nbsp;:&nbsp; &nbsp;C.S </td><td>'+ 
		ISNULL(@CLAIM_NUMBER,'')+
		'</td><td>N.Carta Aviso</td><td></td><td>N.Carta Cobrança</td><td>'+ISNULL(@LETTERSEQUENCENUMBER,'')+'</td><td>O.E.</td><td>'+ 
		ISNULL(@BRANCH_CODE,'')+
		'</td><td>JUDICIAL</td></tr></table><br />'+
		'<table width=100%>'+
		'<tr>'+
		'<td>'+ 
		ISNULL(@CITY,'')+
		'</td>'+
		'<td>'+ 
		ISNULL(@BRANCH_CITY_DETAIL,'')+
		'</td>'+
		'<td></td>'+
		'</tr>'+
		'<tr>'+
		'<td colspan=2><hr width=70% /></td>'+
		'<td><hr width=90% /></td>'+
		'</tr><tr>'+
		'<td>Local e Data</td>'+
		'<td></td>'+
		'<td>Assinatura</td>'+
		'</tr>'+
		'</table>'+
		'</td>'+
		'</tr>'+
		'</table>'+
		'</body>'+
		'</html>'
END
--SELECT @strHTML25 AS LETTER25    

--Letter Number 25.2 HTML
IF(@PROCESS_TYPE='CLM_LETTER25_2')
    BEGIN
--DECLARE @strHTML VARCHAR(8000)

SET @strHTML ='<html>'+
'<head>'+
'<style type=text/css>'+
'body{font-family:Arial; font-size:12pt; margn-left:0.5cm;margin-top:1.0cm;margin-right:0.5cm;margin-bottom:0.5cm;}'+
'hr {border-top: 1px dashed #000; margin-left:0px; text-align: left}'+
'.style1{width:52%;}'+
'.style2{width:27%;}'+
'.style3{width:32%;}'+
'</style>'+
'</head>'+
'<body>'+
'<table width=100%>'+
'<tr>'+
'<td>'+
'COMPANHIA DE SEGUROS ALIANÇA DA BAHIA <BR/> À Cosseguradora'+
'<table  width=100%>'+
'<tr>'+
'<td colspan=3><table width=100%><tr><td>'+
ISNULL(@REIN_COMAPANY_CODE,'')+
'</td><td>'+CASE WHEN @REIN_COMAPANY_CODE!='' THEN ' - ' ELSE '' END+'</td><td></td><td>' +
'<td></td><td>'+
ISNULL(@REIN_COMAPANY_NAME,'')+
'</td> <td>Sinistro Lider</td><td>'+
CASE WHEN @OFFCIAL_CLAIM_NUMBER!='' THEN ISNULL(@OFFCIAL_CLAIM_NUMBER,'') ELSE '' END+
          
'</td></tr></table></td>'+
'</tr>'+
'<tr>'+
'<td>Ref&nbsp;: </td>'+
'<td>(&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;Aviso de Sinistro</td>'+
'<td>(X&nbsp;&nbsp;)&nbsp;&nbsp;Despesas/Honorários</td>'+
'</tr>'+
'<tr>'+
'<td></td>'+
'<td>(X&nbsp;&nbsp;)&nbsp;&nbsp;Cobrança de Sinistro</td>'+
'<td>(&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;Ressarc.Salvados</td>'+
'</tr>'+
'<tr>'+
'<td></td>'+
'<td>(&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp; Pagto. parcial/Adiantamento</td>'+
'<td>(&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp; Encerramento sem Indenização/Cancel. (-)</td>'+
'</tr>'+
'<tr>'+
'<td></td>'+
'<td>(&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp; Pagamento Final</td>'+
'<td>(&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp; Reabertura de Sinistro</td>'+
'</tr>'+
'</table>'+
'<hr width=100% />'+
'DADOS GERAIS<br />'+
'<hr width=100% />'+
'<table width=100%>'+
'<tr>'+
'<td class=style1>Nome do Segurado </td><td>!</td>'+
'<td>Nome Ramo</td><td>!</td>'+
'<td align=center>% Part. Congenere</td>'+
'</tr><tr>'+
'<td class=style1>'+
ISNULL(@CARRIER_NAME,'')+
'</td><td>!</td>'+
'<td>'+
ISNULL(@NOME_RAMO,'')+
'</td><td>!</td>'+
'<td align=center>'+
ISNULL(@PART_CONGENERE,'')+
'</td></tr>'+
'</table>'+
'<hr width=100% />'+
'<table width=100%>'+
'<tr><td>N.Apolice</td><td>!</td>'+
'<td>N.Ordem Apolice</td><td>!</td>'+
'<td>N. Endosso</td><td>!</td>'+
'<td>N.Ordem Endosso</td><td>!</td>'+
'<td align=center>Vigência</td></tr>'+
'<tr><td align=right>'+
ISNULL(@POLICY_NUMBER,'')+
'</td><td>!</td>'+
'<td align=right>'+
ISNULL(@N_ORDERM_APOLICE,'')+
'</td><td>!</td>'+
'<td align=right>'+ 
ISNULL(@N_ENDOSSO,'')+
'</td><td>!</td>'+
'<td align=right></td><td>!</td>'+
'<td align=center>'+
ISNULL(@EFFECTIVE_DATETIME,'')+ CASE WHEN @EFFECTIVE_DATETIME!='' THEN  CASE WHEN @EXPIRY_DATE!='' THEN ' - ' ELSE '' END ELSE ''END +            
ISNULL(@EXPIRY_DATE,'')+
'</td></tr>'+
'</table>'+
'<hr width=100% />'+
'<table width=100% cellspacing=5>'+
'<tr><td>Moeda</td><td>!</td>'+
'<td class=style2>N.Item</td><td>!</td>'+
'<td align=right>Imp.Segurada</td><td>!</td>'+
'<td align=right>Sinistro IRB</td><td>!</td>'+
'<td>Dt.Sinistro</td><td>!</td>'+
'<td>Dt.Aviso</td>'+
'</tr><tr>'+
'<td>R$</td><td>!</td>'+
'<td class=style2  align=right>'+
ISNULL(CONVERT(VARCHAR,@N_ITEM),'')+
'</td><td>!</td>'+
'<td align=right>'+
ISNULL(CONVERT(VARCHAR,@Limit),'')+
'</td><td>!</td>'+
'<td align=right>'+ 
ISNULL(CONVERT(VARCHAR,@SINISTRO_IRB),'')+
'</td><td>!</td>'+
'<td>'+
ISNULL(@DT_SINISTRO,'')+
'</td><td>!</td>'+
'<td>'+
ISNULL(@DT_AVISO,'')+
'</td></tr>'+
'</table>'+
'<hr width=100% />'+
'<table width=100%>'+
'<tr>'+
'<td>Participações</td><td>!</td>'+
'<td>VI. Total Lider</td><td>!</td>'+
'<td>Part.Cosseg (R$)</td><td>!</td>'+
'<td>Part .Qt .Moeda</td><td>!</td>'+
'<td>Dt. Base</td><td>!</td>'+
'<td>Fat. Index.</td></tr>'+
'<tr>'+
'<td><hr width=100% /></td><td>!</td>'+
'<td><hr width=100% /></td><td>!</td>'+
'<td><hr width=100% /></td><td>!</td>'+
'<td><hr width=100% /></td><td>!</td>'+
'<td><hr width=100% /></td><td>!</td>'+
'<td><hr width=100% /></td></tr>'+
'<tr><td>Estimativa</td><td>!</td>'+
'<td align=right>'+ 
ISNULL(CASE WHEN @OUT_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@OUT_COVERAGES) ELSE '' END,'')+
'</td><td>!</td>'+
'<td align=right>'+ 
ISNULL(CASE WHEN @RESV_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_COVERAGES) ELSE '' END,'')+
'</td><td>!</td>'+
'<td align=right>'+
ISNULL(CASE WHEN @MOEDA_ESTIMATIVE!=0.00 THEN CONVERT(VARCHAR,@MOEDA_ESTIMATIVE) ELSE '' END,'')+
'</td><td>!</td>'+
'<td align=right></td><td>!</td>'+
'<td align=right>'+ 
ISNULL(@INFLATION_RATE,'')+
'</td>'+
'</tr><tr>'+
'<td>Indenização</td><td>!</td>'+
'<td align=right></td><td>!</td>'+
'<td></td><td>!</td>'+
'<td></td><td>!</td>'+
'<td></td><td>!</td>'+
'<td></td>'+
'</tr><tr>'+
'<td>Honorários</td><td>!</td>'+
'<td align=right>'+
ISNULL(CASE WHEN @PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@PROFESSIONAL_SERVICES) ELSE '' END,'')+
'</td><td>!</td>'+
'<td align=right>'+ 
 ISNULL(CASE WHEN @RESV_PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@RESV_PROFESSIONAL_SERVICES) ELSE '' END,'')+
'</td><td>!</td>'+
'<td align=right>'+ 
ISNULL(CASE WHEN @MOEDA_HONOR!=0.00 THEN CONVERT(VARCHAR,@MOEDA_HONOR) ELSE '' END,'')+
'</td><td>!</td>'+
'<td align=right></td><td>!</td>'+
'<td align=right>'+
ISNULL(@INFLATION_RATE,'')+
'</td>'+
'</tr><tr>'+
'<td>Despesas</td><td>!</td>'+
'<td align=right>'+ 
ISNULL(CASE WHEN @LOSS_SALVAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@LOSS_SALVAGE_SUBROGATION) ELSE '' END,'')+
'</td><td>!</td>'+
'<td align=right>'+
ISNULL(CASE WHEN @RESV_LOSS_SALAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_LOSS_SALAGE_SUBROGATION) ELSE '' END,'')+
'</td><td>!</td>'+
'<td align=right>'+
ISNULL(CASE WHEN @MOEDA_DESPESAS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_DESPESAS) ELSE '' END,'')+
'</td><td>!</td>'+
'<td align=right></td><td>!</td>'+
'<td align=right>'+ 
ISNULL(@INFLATION_RATE,'')+
'</td>'+
'</tr><tr>'+
'</tr>'+
'<tr><td>Ressarcimento</td><td>!</td>'+
'<td align=right>'+ 
ISNULL(CASE WHEN @OUT_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@OUT_SUBROGATION) ELSE '' END,'')+
'</td><td>!</td>'+
'<td align=right>'+ 
ISNULL(CASE WHEN @RESV_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_SUBROGATION) ELSE '' END,'')+
'</td><td>!</td>'+
'<td align=right>'+ 
ISNULL(CASE WHEN @MOEDA_RESSARCIMENTO!=0.00 THEN CONVERT(VARCHAR,@MOEDA_RESSARCIMENTO) ELSE '' END,'')+
'</td><td>!</td>'+
'<td align=right></td><td>!</td>'+
'<td align=right>'+
ISNULL(@INFLATION_RATE,'')+
'</td>'+
'</tr><tr>'+
'<td>Salvados</td><td>!</td>'+
'<td align=right>'+ 
ISNULL(CASE WHEN @OUT_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@OUT_SALVAGES) ELSE '' END,'')+
'</td><td>!</td>'+
'<td align=right>'+ 
ISNULL(CASE WHEN @RESV_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_SALVAGES) ELSE '' END,'')+
'</td><td>!</td>'+
'<td align=right>'+ 
ISNULL(CASE WHEN @MOEDA_SALVADOS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_SALVADOS) ELSE '' END,'')+
'</td><td>!</td>'+
'<td align=right></td><td>!</td>'+
'<td align=right>'+
ISNULL(@INFLATION_RATE,'')+
'</td>'+
'</tr><tr>'+
'</tr><tr>'+
'<td><hr width=100% /></td><td>!</td>'+
'<td align=right><hr width=100% /></td><td>!</td>'+
'<td><hr width=100% /></td><td>!</td>'+
'<td><hr width=100% /></td><td>!</td>'+
'<td><hr width=100% /></td><td>!</td>'+
'<td><hr width=100% /></td>'+
'</tr><tr>'+
'<tr><td>Total Geral</td><td>!</td>'+
'<td align=right>'+ 
ISNULL(CASE WHEN @TOTAL_GEREAL_LIDER!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GEREAL_LIDER) ELSE '' END,'')+ 
'</td><td>!</td>'+
'<td align=right>'+ 
ISNULL(CASE WHEN @TOTAL_GERAL_COSSEG!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GERAL_COSSEG) ELSE '' END,'')+
'</td><td>!</td>'+
'<td align=right>'+
ISNULL(CASE WHEN @TOTAL_GERAL_MOEDA!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GERAL_MOEDA) ELSE '' END,'')+
'</td><td>!</td>'+
'<td>F-A-J-</td><td></td>'+
'<td></td>'+
'</tr>'+
'<tr><td colspan=11><hr width=100% /></td></tr>'+
'</table>'+
'(&nbsp;&nbsp;X&nbsp;&nbsp;)&nbsp;&nbsp;Coloca a nossa disposição quantia referente a sua particioação até  '+ 

ISNULL(@LETTER_GENERATION_DATE,'')+
'<br />'+
'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Sinistro Sorteio já liquidado. A cota parte dessa congenere será debitada<br />'+
' &nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; através do L.C.C (Lancto.Contra Corrente).<br />'+
'<table width=60%><tr><td>Observação:</td><td>C.S </td><td>'+
ISNULL(@CLAIM_NUMBER,'')+
'</td><td>N.Carta Aviso</td><td></td><td>N.Carta cobrança</td><td>'+ISNULL(@LETTERSEQUENCENUMBER,'')+'</td><td>O.E.</td><td>'+
ISNULL(@BRANCH_CODE,'')+
'</td><td>JUDICIAL</td></tr></table><br />'+
'<table width=100%>'+
'<tr>'+
'<td>'+ 
ISNULL(@CITY,'')+
'</td>'+
'<td>'+ 
ISNULL(@BRANCH_CITY_DETAIL,'')+
'</td>'+
'<td></td>'+
'</tr>'+
'<tr>'+
'<td colspan=2><hr width=70% /></td>'+
'<td><hr width=90% /></td>'+
'</tr><tr>'+
'<td>Local e Data</td>'+
'<td></td>'+
'<td>Assinatura</td>'+
'</tr>'+
'</table>'+
'</td>'+
'</tr>'+
'</table>'+
'</body>'+
'</html>'
END
--SELECT @strHTML25_2 AS LETTER25_2

--Letter Number 27 HTML
IF(@PROCESS_TYPE='CLM_LETTER_27')
    BEGIN
		--DECLARE @strHTML VARCHAR(8000)
		SET @strHTML =	
		'<html>'+
		'<head>'+
		'<style type=text/css>'+
		'body{font-family:Arial; font-size:12pt; margn-left:0.5cm;margin-top:1.0cm;margin-right:0.5cm;margin-bottom:0.5cm;}'+
		'hr {border-top: 1px dashed #000; margin-left:0px; text-align: left}'+
		'.style1{width:52%;}'+
		'.style2{width:27%;}'+
		'.style3{width:32%;}'+
		'</style>'+
		'</head>'+
		'<body>'+
		'<table width=100%>'+
		'<tr>'+
		'<td>'+
		'COMPAHIA DE SEGUROS ALIANÇA DA BAHIA <BR/>À Cosseguradora<br />'+
		'<table  width=100%>'+
		'<tr>'+
		'<td colspan=3><table width=100%><tr><td>'+ 
		ISNULL(@REIN_COMAPANY_CODE,'')+
		'</td><td>'+CASE WHEN @REIN_COMAPANY_CODE!='' THEN ' - ' ELSE '' END+'</td><td></td><td>' +
		ISNULL(@REIN_COMAPANY_NAME,'')+
		'</td> <td>Sinistro Lider</td><td>' +
		CASE WHEN @OFFCIAL_CLAIM_NUMBER!='' THEN ISNULL(@OFFCIAL_CLAIM_NUMBER,'') ELSE '' END+
		'</td></tr></table></td><tr>'+
		'<tr>'+
		'<td>Ref: </td>'+
		'<td>(&nbsp;&nbsp;)&nbsp;&nbsp;Aviso de Sinistro</td>'+
		'<td>'+CASE WHEN @ACTION_ON_PAYMENT=180 OR @ACTION_ON_PAYMENT=181 THEN 
			   CASE WHEN @LOSS_SALVAGE_SUBROGATION !=0.00 OR @PAYMENT_PROFESSIONAL_SERVICES!=0.00  THEN '(X)' ELSE '(&nbsp;&nbsp;)' END ELSE '(&nbsp;&nbsp;)' END+
	    '&nbsp;&nbsp;Despesas/Honorários</td>'+
		'</tr>'+
		'<tr>'+
		'<td></td>'+
		'<td>(X)&nbsp;&nbsp;Cobrança de Sinistro</td>'+
		'<td>'+CASE WHEN @ACTION_ON_PAYMENT=180 OR @ACTION_ON_PAYMENT=181 THEN CASE WHEN @PAYMENT_SALVAGES!=0.00 OR @PAYMENT_SUBROGATION!=0.00
			   THEN '(X)' ELSE '(&nbsp;&nbsp;)' END ELSE '(&nbsp;&nbsp;)' END+
	    '&nbsp;&nbsp;Ressarc.Salvados</td>'+
		'</tr>'+
		'<tr>'+
		'<td></td>'+
		'<td>'+CASE WHEN @ACTION_ON_PAYMENT=180 AND @OUT_COVERAGES!=0.00 THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END+'&nbsp;&nbsp; Pagto. parcial/Adiantamento</td>'+
		'<td>(&nbsp;&nbsp;)&nbsp;&nbsp;Encerramento sem Indenização</td>'+
		'</tr>'+
		'<tr>'+
		'<td></td>'+
		'<td>'+CASE WHEN @ACTION_ON_PAYMENT=181 AND @OUT_COVERAGES!=0.00  THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END +'&nbsp;&nbsp; Pagamento Final</td>'+
		'<td>(&nbsp;&nbsp;)&nbsp;&nbsp; Reabertura de Sinistro</td>'+
		'</tr>'+
		'</table>'+
		'<hr width=100% />'+
		'DADOS GERAIS<br />'+
		'<hr width=100% />'+
		'<table width=100%>'+
		'<tr>'+
		'<td class=style1>Nome do Segurado </td><td>!</td>'+
		'<td>Nome Ramo</td><td>!</td>'+
		'<td align=center>% Part. Congenere</td>'+
		'</tr><tr>'+
		'<td class=style1>'+
		ISNULL(@CARRIER_NAME,'')+
		'</td><td>!</td>'+
		'<td>'+
		ISNULL(@NOME_RAMO,'')+
		'</td><td>!</td>'+
		'<td align=center>'+
		ISNULL(@PART_CONGENERE,'')+
		'</td></tr>'+
		'</table>'+
		'<hr width=100% />'+    
		'<table width=100%>'+
		'<tr><td>N.Apolice</td><td>!</td>'+
		'<td>N.Ordem Apolice</td><td>!</td>'+
		'<td>N. Endosso</td><td>!</td>'+
		'<td>N.Ordem Endosso</td><td>!</td>'+
		'<td align=center>Vigência</td></tr>'+
		'<tr><td>'+ 
		ISNULL(@POLICY_NUMBER,'')+
		'</td><td>!</td>'+
		'<td align=right>'+
		ISNULL(@N_ORDERM_APOLICE,'')+
		'</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(@N_ENDOSSO,'')+
		  '</td><td>!</td>'+
		'<td align=center></td><td>!</td>'+
		'<td align=center>'+ 
		ISNULL(@EFFECTIVE_DATETIME,'')+ CASE WHEN @EFFECTIVE_DATETIME!='' THEN  CASE WHEN @EXPIRY_DATE!='' THEN ' - ' ELSE '' END ELSE ''END +            
		ISNULL(@EXPIRY_DATE,'')+
		'</td></tr>'+
		'</table>'+
		'<hr width=100% />'+
		'<table width=100% cellspacing=5>'+
		'<tr><td>Moeda</td><td>!</td>'+
		'<td class=style2>N.Item</td><td>!</td>'+
		'<td align=right>Imp.Segurada</td><td>!</td>'+
		'<td align=right>Sinistro IRB</td><td>!</td>'+
		'<td>Dt.Sinistro</td><td>!</td>'+
		'<td>Dt.Aviso</td>'+
		'</tr><tr>'+
		'<td>R$</td><td>!</td>'+
		'<td class=style2>'+
		 ISNULL(CONVERT(VARCHAR,@N_ITEM),'')+
		 '</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CONVERT(VARCHAR,@Limit),'')+
		'</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CONVERT(VARCHAR,@SINISTRO_IRB),'')+
		 '</td><td>!</td>'+
		'<td>'+
		 ISNULL(@DT_SINISTRO,'')+
		  '</td><td>!</td>'+
		'<td>'+
		 ISNULL(@DT_AVISO,'')+
		 '</td></tr>'+
		'</table>'+
		'<hr width=100% />'+
		'DADOS ESPECIFICOS <span>AUT/RCV</span>'+
		'<hr width=100% />'+
		'<table width=99%><tr>'+
		'<td>Marca Veiculo</td><td>!</td>'+
		'<td>N.Placa</td><td>!</td>'+
		'<td>N.Chassis</td><td>!</td>'+
		'<td>Ano</td><td>!</td>'+
		'<td>Tipo de Ocorrencia</td></tr>'+
		'<tr><td>'+
		 ISNULL(@MARCA_VEICULO,'')+
		 '</td><td>!</td>'+
		'<td>'+ 
		ISNULL(@N_PLACA,'')+
		 '</td><td>!</td>'+
		'<td>'+ 
		ISNULL(@N_CHASSIS,'')+
		'</td><td>!</td>'+
		'<td>'+
		 ISNULL(@ANO,'')+
		 '</td><td>!</td>'+
		'<td>'+ 
		ISNULL(@DETAIL_TYPE_DESCRIPTION,'')+
		 '</td></tr>'+
		'</table>'+
		'<hr width=100% />'+
		'<table width=100%>'+
		'<tr>'+
		'<td>Participações</td><td>!</td>'+
		'<td>VI. Total Lider</td><td>!</td>'+
		'<td>Part.Cosseg (R$)</td><td>!</td>'+
		'<td>Part .Qt .Moeda</td><td>!</td>'+
		'<td>Dt. Base</td><td>!</td>'+
		'<td>Fat. Index.</td></tr>'+
		'<tr>'+
		'<td><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td></tr>'+
		'<tr><td>Estimativa</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @OUT_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@OUT_COVERAGES) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right>'+
		 ISNULL(CASE WHEN @RESV_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_COVERAGES) ELSE '' END,'')+
		  '</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @MOEDA_ESTIMATIVE!=0.00 THEN CONVERT(VARCHAR,@MOEDA_ESTIMATIVE) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right></td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
		 '</td>'+
		'</tr><tr>'+
		'<td>Indenização</td><td>!</td>'+
		'<td align=right>'+ISNULL(CASE WHEN @PAYMENT_COVERAGE!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_COVERAGE) ELSE '' END,'')+'</td><td>!</td>'+
		'<td align=right>'+ISNULL(CASE WHEN @RESV_PAYMENT_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_PAYMENT_COVERAGES) ELSE '' END,'')+'</td><td>!</td>'+
		'<td align=right>'+ISNULL(CASE WHEN @MOEDA_PAYMENT_ESTIMATIVE!=0.00 THEN CONVERT(VARCHAR,@MOEDA_PAYMENT_ESTIMATIVE) ELSE '' END,'')+'</td><td>!</td>'+
		'<td></td><td>!</td>'+
		'<td align=right>'+ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+'</td>'+
		'</tr><tr>'+
		'<td>Honorários</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @PAYMENT_PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_PROFESSIONAL_SERVICES) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right>'+
		 ISNULL(CASE WHEN @RESV_PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@RESV_PROFESSIONAL_SERVICES) ELSE '' END,'')+
		  '</td><td>!</td>'+
		'<td align=right>'+
		 ISNULL(CASE WHEN @MOEDA_HONOR!=0.00 THEN CONVERT(VARCHAR,@MOEDA_HONOR) ELSE '' END,'')+
		 '</td><td>!</td>'+
		'<td align=right></td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
		'</td>'+
		'</tr><tr>'+
		'<td>Despesas</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @PAYMENT_SALVAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_SALVAGE_SUBROGATION) ELSE '' END,'')+
		 '</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @RESV_LOSS_SALAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_LOSS_SALAGE_SUBROGATION) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right>'+
		 ISNULL(CASE WHEN @MOEDA_DESPESAS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_DESPESAS) ELSE '' END,'')+
		  '</td><td>!</td>'+
		'<td align=right></td><td>!</td>'+
		'<td align=right>'+
		 ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
		 '</td>'+
		'</tr><tr>'+
		'</tr>'+
		'<tr><td>Ressarcimento</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @PAYMENT_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_SUBROGATION) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @RESV_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_SUBROGATION) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @MOEDA_RESSARCIMENTO!=0.00 THEN CONVERT(VARCHAR,@MOEDA_RESSARCIMENTO) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right></td><td>!</td>'+
		'<td align=right>'+ 
		 ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
		 '</td>'+
		'</tr><tr>'+
		'<td>Salvados</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @PAYMENT_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_SALVAGES) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @RESV_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_SALVAGES) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @MOEDA_SALVADOS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_SALVADOS) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right></td><td>!</td>'+
		'<td align=right>'+
		 ISNULL(@INFLATION_RATE,'')+
		 '</td>'+
		'</tr><tr>'+
		'</tr><tr>'+
		'<td><hr width=100% /></td><td>!</td>'+
		'<td align=right><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td><td>!</td>'+
		'<td><hr width=100% /></td>'+
		'</tr><tr>'+
		'<tr><td>Total Geral</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @TOTAL_PAYMENT_GEREAL_LIDER!=0.00 THEN CONVERT(VARCHAR,@TOTAL_PAYMENT_GEREAL_LIDER) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @TOTAL_PAYMENT_GERAL_COSSEG!=0.00 THEN CONVERT(VARCHAR,@TOTAL_PAYMENT_GERAL_COSSEG) ELSE '' END,'')+ 
		 '</td><td>!</td>'+
		'<td align=right>'+ 
		ISNULL(CASE WHEN @TOTAL_PAYMENT_GERAL_MOEDA!=0.00 THEN CONVERT(VARCHAR,@TOTAL_PAYMENT_GERAL_MOEDA) ELSE '' END,'')+
		'</td><td>!</td>'+
		'<td>F-A-J-</td><td></td>'+
		'<td></td>'+
		'</tr>'+
		'<tr><td colspan=11><hr width=100% /></td></tr>'+
		'</table>'+
		'(&nbsp;&nbsp;X&nbsp;&nbsp;)&nbsp;&nbsp;Coloca a nossa disposição quantia referente a sua particioação até  '+
		 ISNULL(@LETTER_GENERATION_DATE,'')+
		 '<br />'+
		'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Sinistro Sorteio já liquidado. A cota parte dessa congenere será debitada<br />'+
		' &nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; através do L.C.C (Lancto.Contra Corrente).<br />'+
		'<table width=80%><tr><td>Observação:</td><td>C.S </td><td>'+ 
		ISNULL(@CLAIM_NUMBER,'')+
		'</td><td>N.Carta Aviso</td><td></td><td>N.Carta Cobrança</td><td>'+ISNULL(@LETTERSEQUENCENUMBER,'')+'</td><td>O.E.</td><td>'+ 
		ISNULL(@BRANCH_CODE,'')+
		'</td><td></td></tr></table><br />'+
		'<table width=100%>'+
		'<tr>'+
		'<td>'+ 
		ISNULL(@CITY,'')+
		'</td>'+
		'<td>'+ 
		ISNULL(@BRANCH_CITY_DETAIL,'')+
		 '</td>'+
		'<td></td>'+
		'</tr>'+
		'<tr>'+
		'<td colspan=2><hr width=70% /></td>'+
		'<td><hr width=90% /></td>'+
		'</tr><tr>'+
		'<td>Local e Data</td>'+
		'<td></td>'+
		'<td>Assinatura</td>'+
		'</tr>'+
		'</table>'+
		'</td>'+
		'</tr>'+
		'</table>'+
		'</body>'+
		'</html>'
END
--SELECT @strHTML27 AS LETTER27

--Letter Number 28
IF(@PROCESS_TYPE='CLM_LETTER_28')
    BEGIN
		--DECLARE @strHTML VARCHAR(8000)
		SET @strHTML =   
			'<html>'+
			'<head>'+
			'<style type=text/css>'+
			'body{font-family:Arial; font-size:12px; margn-left:0.5cm;margin-top:1.0cm;margin-right:0.5cm;margin-bottom:0.5cm;}'+
			'hr {border-top: 1px dashed #000; margin-left:0px; text-align: left}'+
			'.style1{width:59.5%;}'+
			'.style2{width:35%;}'+
			'.style3{width:32%;}'+
			'.style4{width:20%;}'+
			'</style>'+
			'</head>'+
			'<body>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			'0000000000000000000000000000000<br />'+
			'À Cosseguradora<br />'+
			'<table  width=100%>'+
			'<tr>'+
			'<td colspan=3><table width=100%><tr><td>'+ 
			ISNULL(@REIN_COMAPANY_CODE,'')+
			'</td><td>'+CASE WHEN @REIN_COMAPANY_CODE!='' THEN ' - ' ELSE '' END+'</td><td></td><td>' +
			ISNULL(@REIN_COMAPANY_NAME,'')+
			'</td> <td>Sinistro Lider</td><td>' +
			CASE WHEN @OFFCIAL_CLAIM_NUMBER!='' THEN ISNULL(@OFFCIAL_CLAIM_NUMBER,'') ELSE '' END+
			'</td></tr></table></td>'+
			'</tr>'+
			'<tr>'+
			'<td>Ref: </td>'+
			'<td>(&nbsp;&nbsp;)&nbsp;&nbsp;Aviso de Sinistro</td>'+
			'<td>'+CASE WHEN @ACTION_ON_PAYMENT=180 OR @ACTION_ON_PAYMENT=181 THEN 
				   CASE WHEN @LOSS_SALVAGE_SUBROGATION !=0.00 OR @PAYMENT_PROFESSIONAL_SERVICES!=0.00  THEN '(X)' ELSE '(&nbsp;&nbsp;)' END ELSE '(&nbsp;&nbsp;)' END+
			 '&nbsp;&nbsp;Despesas/Honorários</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(X)&nbsp;&nbsp;Cobrança de Sinistro</td>'+
			'<td>'+CASE WHEN @ACTION_ON_PAYMENT=180 OR @ACTION_ON_PAYMENT=181 THEN CASE WHEN @PAYMENT_SALVAGES!=0.00 OR @PAYMENT_SUBROGATION!=0.00
			       THEN '(X)' ELSE '(&nbsp;&nbsp;)' END ELSE '(&nbsp;&nbsp;)' END+
			'&nbsp;&nbsp;Ressarc.Salvados</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>'+CASE WHEN @ACTION_ON_PAYMENT=180 AND @OUT_COVERAGES!=0.00 THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END+'&nbsp;&nbsp; Pagto. parcial/Adiantamento</td>'+
			'<td>(&nbsp;&nbsp;)&nbsp;&nbsp; Encerramento sem Indenização</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>'+CASE WHEN @ACTION_ON_PAYMENT=181 AND @OUT_COVERAGES!=0.00  THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END +'&nbsp;&nbsp; Pagamento Final</td>'+
			'<td>(&nbsp;&nbsp;)&nbsp;&nbsp; Reabertura de Sinistro</td>'+
			'</tr>'+
			'</table>'+
			'<hr width=100% />'+
			'DADOS GERAIS<br />'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr>'+
			'<td class=style1>Nome do Segurado </td><td>!</td>'+
			'<td>Nome Ramo</td><td>!</td>'+
			'<td align=center>% Part. Congenere</td>'+
			'</tr><tr>'+
			'<td class=style1>'+
			ISNULL(@CARRIER_NAME,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@NOME_RAMO,'')+
			'</td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@PART_CONGENERE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+    
			'<table width=100%>'+
			'<tr><td  class=style4>N.Apolice</td><td>!</td>'+
			'<td align=center>N.Ordem Apolice</td><td>!</td>'+
			'<td align=center>N. Endosso</td><td>!</td>'+
			'<td align=center>N.Ordem Endosso</td><td>!</td>'+
			'<td align=center>Vigência</td></tr>'+
			'<tr><td  class=style4>'+ 
			ISNULL(@POLICY_NUMBER,'')+
			'</td><td>!</td>'+
			'<td align=center>'+ 
			ISNULL(@N_ORDERM_APOLICE,'')+
			'</td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@N_ENDOSSO,'')+
			'</td><td>!</td>'+
			'<td align=center></td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@EFFECTIVE_DATETIME,'')+ CASE WHEN @EFFECTIVE_DATETIME!='' THEN  CASE WHEN @EXPIRY_DATE!='' THEN ' - ' ELSE '' END ELSE ''END +            
			ISNULL(@EXPIRY_DATE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100% cellspacing=5>'+
			'<tr><td>Moeda</td><td>!</td>'+
			'<td class=style2>N.Item</td><td>!</td>'+
			'<td align=right>Imp.Segurada</td><td>!</td>'+
			'<td align=right>Sinistro IRB</td><td>!</td>'+
			'<td>Dt.Sinistro</td><td>!</td>'+
			'<td>Dt.Aviso</td>'+
			'</tr><tr>'+
			'<td>R$</td><td>!</td>'+
			'<td class=style2>'+ 
			 ISNULL(CONVERT(VARCHAR,@N_ITEM_TRANS),'')+
			 '</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@Limit),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@SINISTRO_IRB),'')+
			 '</td><td>!</td>'+
			'<td>'+
			 ISNULL(@DT_SINISTRO,'')+
			  '</td><td>!</td>'+
			'<td>'+
			 ISNULL(@DT_AVISO,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<hr width=99% />'+
			'DADOS ESPECIFICOS <br/><div>TRANSP/CASCO/AERONAUTICO/P.RURAL/CRED.EXP./R.C.GERAL/CRED.INT.</div>'+
			'<hr width=99% />'+
			'<table width=99%><tr>'+
			'<td>N.Averbacao Certif.</td><td>!</td>'+
			'<td>Meio Ttransporte</td><td>!</td>'+
			'<td>N.Placa</td><td>!</td>'+
			'<td>PrefiXo</td><td>!</td>'+
			'<td>Nome de Embrcação</td></tr>'+
			'<tr><td>'+ 
			ISNULL(@N_AVERBACAO_CERTIF,'')+
			 '</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@MEIO_TTRANSPORTE,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@N_PLACA,'')+
			'</td><td>!</td>'+
			'<td>'+
			 ISNULL(@PREFIXO,'')+
			 '</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@NOME_DA_EMBARCACAO,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99%/>'+
			'<table width=99%>'+
			'<tr><td>Empresa Transportadora</td><td>!</td>'+
			'<td class=style3>Mercadoria Sinisrtrada</td><td>!</td>'+
			'<td>Natureza Danos</td><td>!</td>'+
			'<td>Tipo Cobertura</td></tr>'+
			'<tr><td>'+
			ISNULL(@EMPRESA_TRANS,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@MERCADORIA_SINISTRADA,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DETAIL_TYPE_DESCRIPTION,'')+
			'</td><td>!</td>'+
			'<td>CLAUSULA A +</td></tr>'+
			'</table>'+
			'<hr WIDTH=99%/>'+
			'<table width=99%>'+
			'<tr><td>Origem</td><td>!</td>'+
			'<td>Destino</td><td>!</td>'+
			'<td colspan=3>Local de Ocorrencia</td><td>!</td>'+
			'<td>Cidade</td><td>!</td>'+
			'<td>UF</td></tr>'+
			'<tr><td>'+
			ISNULL(@ORIGEM,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@DESTINO,'')+
			'</td><td>!</td>'+
			'<td colspan=3>'+ 
			ISNULL(@LOCAL_DE_OCORRENCIA,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@CIDADE,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@UF,'')+
			'</td></tr>'+
			'<tr><td>Data Saida</td><td>!</td>'+
			'<td>Data Chegada</td><td>!</td>'+
			'<td>Data Vistoria</td><td>!</td>'+
			'<td>Local de Vistoria</td></tr>'+
			'<tr><td>'+ 
			ISNULL(@DATA_SAIDA,'')+ 
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DATA_CHEGADA,'')+
			'</td><td>!</td>'+
			'<td>'+
			 ISNULL(@DATA_VISTORIA,'')+
			'</td><td>!</td>'+
			'<td>SANTOS</td></tr>'+
			'</table>'+
			'<hr WIDTH=99% />'+
			'<table width=99%>'+
			'<tr>'+
			'<td>Participações</td><td>!</td>'+
			'<td>VI. Total Lider</td><td>!</td>'+
			'<td>Part.Cosseg (R$)</td><td>!</td>'+
			'<td>Part .Qt .Moeda</td><td>!</td>'+
			'<td>Dt. Base</td><td>!</td>'+
			'<td>Fat. Index.</td></tr>'+
			'<tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td></tr>'+
			'<tr><td>Estimativa</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @OUT_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@OUT_COVERAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td  align=right>'+
			 ISNULL(CASE WHEN @RESV_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_COVERAGES) ELSE '' END,'')+
			  '</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_ESTIMATIVE!=0.00 THEN CONVERT(VARCHAR,@MOEDA_ESTIMATIVE) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			--ds.Tables[0].Rows[0]["INFLATION_RATE"].ToString());
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Indenização</td><td>!</td>'+
			'<td align=right>'+ISNULL(CASE WHEN @PAYMENT_COVERAGE!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_COVERAGE) ELSE '' END,'')+'</td><td>!</td>'+
			'<td align=right>'+ISNULL(CASE WHEN @RESV_PAYMENT_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_PAYMENT_COVERAGES) ELSE '' END,'')+'</td><td>!</td>'+
			'<td align=right>'+ISNULL(CASE WHEN @MOEDA_PAYMENT_ESTIMATIVE!=0.00 THEN CONVERT(VARCHAR,@MOEDA_PAYMENT_ESTIMATIVE) ELSE '' END,'')+'</td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td align=right>'+ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+'</td>'+
			'</tr><tr>'+
			'<td>Honorários</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @PAYMENT_PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_PROFESSIONAL_SERVICES) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @RESV_PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@RESV_PROFESSIONAL_SERVICES) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @MOEDA_HONOR!=0.00 THEN CONVERT(VARCHAR,@MOEDA_HONOR) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right></td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Despesas</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @PAYMENT_SALVAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_SALVAGE_SUBROGATION) ELSE '' END,'')+
			 '</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @RESV_LOSS_SALAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_LOSS_SALAGE_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			 ISNULL(CASE WHEN @MOEDA_DESPESAS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_DESPESAS) ELSE '' END,'')+
			  '</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			 ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'<tr><td>Ressarcimento</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @PAYMENT_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @RESV_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_RESSARCIMENTO!=0.00 THEN CONVERT(VARCHAR,@MOEDA_RESSARCIMENTO) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(@INFLATION_RATE,'')+
			'</td>'+
			'<td></td>'+
			'</tr><tr>'+
			'<td>Salvados</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @PAYMENT_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_SALVAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @RESV_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_SALVAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_SALVADOS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_SALVADOS) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			 --ISNULL(@INFLATION_RATE,'')+
			'</td>'+
			'</tr><tr>'+
			'</tr><tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td align=right><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<tr><td>Total Geral</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @TOTAL_PAYMENT_GEREAL_LIDER!=0.00 THEN CONVERT(VARCHAR,@TOTAL_PAYMENT_GEREAL_LIDER) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @TOTAL_PAYMENT_GERAL_COSSEG!=0.00 THEN CONVERT(VARCHAR,@TOTAL_PAYMENT_GERAL_COSSEG) ELSE '' END,'')+ 
			 '</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @TOTAL_PAYMENT_GERAL_MOEDA!=0.00 THEN CONVERT(VARCHAR,@TOTAL_PAYMENT_GERAL_MOEDA) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td>F-A-J-</td><td></td>'+
			'<td></td>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'</table>'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Colocar a nossa disposição quantia referente a sua particioação até  0000000<br />'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Sinistro Sorteio já liquidado. A cota parte dessa congenere sera debitada<br />'+
			' &nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; através do L.C.C (Lancto.Contra Corrente).<br />'+
			'<table width=60%><tr><td>Observação:</td><td>C.S </td><td>'+ 
				ISNULL(@CLAIM_NUMBER,'')+
			'</td><td>N.Carta Aviso</td><td>'+ISNULL(@LETTERSEQUENCENUMBER,'')+'</td><td>O.E.</td><td>05</td></tr></table>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			ISNULL(@CITY,'')+
			'</td>'+
			'<td>'+ 
			ISNULL(@BRANCH_CITY_DETAIL,'')+
			'</td>'+
			'<td></td>'+
			'</tr>'+
			'<tr>'+
			'<td colspan=2><hr width=70% /></td>'+
			'<td><hr width=90% /></td>'+
			'</tr><tr>'+
			'<td>Local e Data</td>'+
			'<td></td>'+
			'<td>Assinatura</td>'+
			'</tr>'+
			'</table>'+
			'</td>'+
			'</tr>'+
			'</table>'+
			'</body>'+
			'</html>'
END
--SELECT @strHTML28 AS LETTER28
---------------For Letter 30--------------
IF(@PROCESS_TYPE='CLM_LETTER_30')
    BEGIN
		--DECLARE @strHTML AS VARCHAR(8000)
		SET @strHTML =
		    '<html>'+
			'<head>'+
			'<style type=text/css>'+
			'body{font-family:Arial; font-size:12px; margn-left:0.5cm;margin-top:1.0cm;margin-right:0.5cm;margin-bottom:0.5cm;}'+
			'hr {border-top: 1px dashed #000; margin-left:0px; text-align: left}'+
			'.style1{width:53%;}'+
			'.style2{width: 74%;}'+
			'</style>'+
			'</head>'+
			'<body>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			'0000000000000000000000000000000<br />'+
			'À Cosseguradora<br />'+
			'<table  width=100%>'+
			'<tr>'+
			'<td colspan=3><table width=100%><tr><td>'+
			ISNULL(@REIN_COMAPANY_CODE,'')+
			'</td><td>'+CASE WHEN @REIN_COMAPANY_CODE!='' THEN ' - ' ELSE '' END+'</td><td></td><td>' +
			ISNULL(@REIN_COMAPANY_NAME,'')+             
			'</td> <td>Sinistro Lider</td><td>'+
			CASE WHEN @OFFCIAL_CLAIM_NUMBER!='' THEN ISNULL(@OFFCIAL_CLAIM_NUMBER,'') ELSE '' END+
			'</td></tr></table></td>'+
			'</tr>'+
			'<tr>'+
			'<td>Ref: </td>'+
			'<td>(&nbsp;&nbsp;)&nbsp;&nbsp;Aviso de Sinistro</td>'+
			'<td>'
			+CASE WHEN @ACTION_ON_PAYMENT=180 OR @ACTION_ON_PAYMENT=181 THEN 
			 CASE WHEN @LOSS_SALVAGE_SUBROGATION !=0.00 OR	@PAYMENT_PROFESSIONAL_SERVICES!=0.00 THEN '(X)' ELSE '(&nbsp;&nbsp;)' END ELSE '(&nbsp;&nbsp;)' END+
			'&nbsp;&nbsp;Despesas/Honorários</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(X)&nbsp;&nbsp;Cobrança de Sinistro</td>'+
			'<td>'+CASE WHEN @ACTION_ON_PAYMENT=180 OR @ACTION_ON_PAYMENT=181 THEN CASE WHEN @PAYMENT_SALVAGES!=0.00 OR @PAYMENT_SUBROGATION!=0.00
			       THEN '(X)' ELSE '(&nbsp;&nbsp;)' END ELSE '(&nbsp;&nbsp;)' END+
			'&nbsp;&nbsp;Ressarc.Salvados</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>'+CASE WHEN @ACTION_ON_PAYMENT=180 AND @OUT_COVERAGES!=0.00 THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END+'&nbsp;&nbsp; Pagto.Parcial/Adiantamento</td>'+
			'<td>(&nbsp;&nbsp;)&nbsp;&nbsp; Encerramento sem Indenização</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>'+CASE WHEN @ACTION_ON_PAYMENT=181 AND @OUT_COVERAGES!=0.00  THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END +'&nbsp;&nbsp; Pagamento Final</td>'+
			'<td>(&nbsp;&nbsp;)&nbsp;&nbsp; Reabertura de Sinistro</td>'+
			'</tr>'+
			'</table>'+
			'<hr width=100% />'+
			'DADOS GERAIS<br />'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr>'+
			'<td class=style1>Nome do Segurado </td><td>!</td>'+
			'<td>Nome Ramo</td><td>!</td>'+
			'<td align=center>% Part. Congenere</td>'+
			'</tr><tr>'+
			'<td class=style1>'+
			ISNULL(@CARRIER_NAME,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@NOME_RAMO,'')+
			'</td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@PART_CONGENERE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr><td>N.Apolice</td><td>!</td>'+
			'<td> N.Ordem Apolice</td><td>!</td>'+
			'<td> N. Endosso</td><td>!</td>'+
			'<td> N.Ordem Endosso</td><td>!</td>'+
			'<td align=center> Vigência</td></tr>'+
			'<tr><td align=right>'+
			ISNULL(@POLICY_NUMBER,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(@N_ORDERM_APOLICE,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(@N_ENDOSSO,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@EFFECTIVE_DATETIME,'')+ CASE WHEN @EFFECTIVE_DATETIME!='' THEN  CASE WHEN @EXPIRY_DATE!='' THEN ' - ' ELSE '' END ELSE ''END +

			ISNULL(@EXPIRY_DATE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr><td>Moeda</td><td>!</td>'+
			'<td>N.Item</td><td>!</td>'+
			'<td ALIGN=RIGHT>Imp.Segurada</td><td>!</td>'+
			'<td>Sinistro IRB</td><td>!</td>'+
			'<td>Dt.Sinistro</td><td>!</td>'+
			'<td>Dt.Aviso</td>'+
			'</tr><tr>'+
			'<td>R$</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@N_ITEM),'')+ '</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@Limit),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@SINISTRO_IRB),'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DT_SINISTRO,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DT_AVISO,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<hr width=99% />'+
			'DADOS ESPECIFICOS VG/APC'+
			'<hr width=99% />'+
			'<table width=100%>'+
			'<tr><td>Nome do Estipulante</td><td>!</td>'+
			'<td>Garantia reclamada do Segurado sinistrado</td></tr>'+
			'<tr><td>'+ 
			ISNULL(@CARRIER_NAME,'')+'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DETAIL_TYPE_DESCRIPTION,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99%/>'+
			'<table width=100%>'+
			'<tr><td class=style2>Nome do Segurado Principal</td><td>!</td>'+
			'<td>Dt.Nascimento</td></tr>'+
			'</tr><tr>'+
			'<td class=style2>'+ 
			ISNULL(@CLAIMANT_NAME,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@CO_APPL_DOB,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99% />'+
			'<table width=100%>'+
			'<tr><td class=style2>Nome do Segurado Sinistrado</td><td>!</td>'+
			'<td>Dt.Nascimento</td></tr>'+
			'<tr><td class=style2>'+
			ISNULL(@CLAIMANT_NAME,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@CO_APPL_DOB,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99% />'+
			'<table width=99%>'+
			'<tr>'+
			'<td>Participações</td><td>!</td>'+
			'<td>VI. Total Lider</td><td>!</td>'+
			'<td>Part.Cosseg (R$)</td><td>!</td>'+
			'<td>Part .Qt .Moeda</td><td>!</td>'+
			'<td>Dt. Base</td><td>!</td>'+
			'<td>Fat. Index.</td></tr>'+
			'<tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=90% /></td></tr>'+
			'<tr><td>Estimativa</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @OUT_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@OUT_COVERAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @RESV_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_COVERAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_ESTIMATIVE!=0.00 THEN CONVERT(VARCHAR,@MOEDA_ESTIMATIVE) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			ISNULL(@INFLATION_RATE,'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Indenização</td><td>!</td>'+
			'<td align=right>'+ISNULL(CASE WHEN @PAYMENT_COVERAGE!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_COVERAGE) ELSE '' END,'')+'</td><td>!</td>'+
			'<td align=right>'+ISNULL(CASE WHEN @RESV_PAYMENT_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_PAYMENT_COVERAGES) ELSE '' END,'')+'</td><td>!</td>'+
			'<td align=right>'+ISNULL(CASE WHEN @MOEDA_PAYMENT_ESTIMATIVE!=0.00 THEN CONVERT(VARCHAR,@MOEDA_PAYMENT_ESTIMATIVE) ELSE '' END,'')+'</td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td align=right>'+ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+'</td>'+
			'</tr><tr>'+
			'<td>Honorários</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @PAYMENT_PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_PROFESSIONAL_SERVICES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			 ISNULL(CASE WHEN @RESV_PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@RESV_PROFESSIONAL_SERVICES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_HONOR!=0.00 THEN CONVERT(VARCHAR,@MOEDA_HONOR) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Despesas</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @PAYMENT_SALVAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_SALVAGE_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @RESV_LOSS_SALAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_LOSS_SALAGE_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @MOEDA_DESPESAS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_DESPESAS) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'<tr><td>Ressarcimento</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @PAYMENT_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @RESV_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @MOEDA_RESSARCIMENTO!=0.00 THEN CONVERT(VARCHAR,@MOEDA_RESSARCIMENTO) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(@INFLATION_RATE,'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Salvados</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @PAYMENT_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_SALVAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @RESV_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_SALVAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @MOEDA_SALVADOS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_SALVADOS) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td  align=right>'+
			--ISNULL(@INFLATION_RATE,'')+
			'</td>'+
			'</tr><tr>'+
			'</tr><tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td align=right><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<tr><td>Total Geral</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @TOTAL_PAYMENT_GEREAL_LIDER!=0.00 THEN CONVERT(VARCHAR,@TOTAL_PAYMENT_GEREAL_LIDER) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @TOTAL_PAYMENT_GERAL_COSSEG!=0.00 THEN CONVERT(VARCHAR,@TOTAL_PAYMENT_GERAL_COSSEG) ELSE '' END,'')+ 
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @TOTAL_PAYMENT_GERAL_MOEDA!=0.00 THEN CONVERT(VARCHAR,@TOTAL_PAYMENT_GERAL_MOEDA) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td>F-A-J-</td><td></td>'+
			'<td></td>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'</table>'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Colocar a nossa disposição quantia referente a sua participação até '+@LETTER_GENERATION_DATE+'<br />'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Sinistro Sorteio já liquidado.A cota parte dessa congenere sera debitada<br />'+
			' &nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; através do L.C.C.(Lancto.Conta Corrente).<br />'+
			'<table width=60%><tr><td>Observação:</td><td>C.S </td><td>'+
			ISNULL(@CLAIM_NUMBER,'')+
			'</td><td>N.Carta Aviso</td><td>'+ISNULL(@LETTERSEQUENCENUMBER,'')+'</td><td>N.Carta Cobrança</td><td></td><td>O.E.</td><td>'+
			ISNULL(@BRANCH_CODE,'')+
			'</td></tr></table>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			ISNULL(@CITY,'')+
			'</td>'+
			'<td>'+
			ISNULL(@BRANCH_CITY_DETAIL,'')+
			'</td>'+
			'<td></td>'+
			'</tr>'+
			'<tr>'+
			'<td colspan=2><hr width=70% /></td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<td>Local e Data</td>'+
			'<td></td>'+
			'<td>Assinatura</td>'+
			'</tr>'+
			'</table>'+
			'</td>'+
			'</tr>'+
			'</table>'+
			'</body>'+
			'</html>'
END
--SELECT @strHTML30 AS LETTER30
IF(@PROCESS_TYPE='CLM_LETTER_29')
    BEGIN
		--DECLARE @strHTML VARCHAR(8000)
		SET @strHTML =   
			'<html>'+
			'<head>'+
			'<style type=text/css>'+
			'body{font-family:Arial; font-size:12px; margn-left:0.5cm;margin-top:1.0cm;margin-right:0.5cm;margin-bottom:0.5cm;}'+
			'hr {border-top: 1px dashed #000; margin-left:0px; text-align: left}'+
			'.style1{width:59.5%;}'+
			'.style2{width:35%;}'+
			'.style3{width:32%;}'+
			'.style4{width:20%;}'+
			'</style>'+
			'</head>'+
			'<body>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			'0000000000000000000000000000000<br />'+
			'À Cosseguradora<br />'+
			'<table  width=100%>'+
			'<tr>'+
			'<td colspan=3><table width=100%><tr><td>'+ 
			ISNULL(@REIN_COMAPANY_CODE,'')+
			'</td><td>'+CASE WHEN @REIN_COMAPANY_CODE!='' THEN ' - ' ELSE '' END+'</td><td></td><td>' +
			ISNULL(@REIN_COMAPANY_NAME,'')+
			'</td> <td>Sinistro Lider</td><td>' +
			CASE WHEN @OFFCIAL_CLAIM_NUMBER!='' THEN ISNULL(@OFFCIAL_CLAIM_NUMBER,'') ELSE '' END+
			'</td></tr></table></td>'+
			'</tr>'+
			'<tr>'+
			'<td>Ref: </td>'+
			'<td>(&nbsp;)&nbsp;&nbsp;Aviso de Sinistro</td>'+
			'<td>'+CASE WHEN @ACTION_ON_PAYMENT=180 OR @ACTION_ON_PAYMENT=181 THEN 
				   CASE WHEN @LOSS_SALVAGE_SUBROGATION !=0.00 OR @PAYMENT_PROFESSIONAL_SERVICES!=0.00  THEN '(X)' ELSE '(&nbsp;&nbsp;)' END ELSE '(&nbsp;&nbsp;)' END+
			 '&nbsp;&nbsp;Despesas/Honorários</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;X)&nbsp;&nbsp;Cobrança de Sinistro</td>'+
			'<td>'+CASE WHEN @ACTION_ON_PAYMENT=180 OR @ACTION_ON_PAYMENT=181 THEN CASE WHEN @PAYMENT_SALVAGES!=0.00 OR @PAYMENT_SUBROGATION!=0.00
			       THEN '(X)' ELSE '(&nbsp;&nbsp;)' END ELSE '(&nbsp;&nbsp;)' END+
		    '&nbsp;&nbsp;Ressarc.Salvados</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>'+CASE WHEN @ACTION_ON_PAYMENT=180 AND @OUT_COVERAGES!=0.00 THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END+'&nbsp;&nbsp; Pagto. parcial/Adiantamento</td>'+
			'<td>(&nbsp;&nbsp;)&nbsp;&nbsp; Encerramento sem Indenização</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>'+CASE WHEN @ACTION_ON_PAYMENT=181 AND @OUT_COVERAGES!=0.00  THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END +'&nbsp;&nbsp; Pagamento Final</td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp; Reabertura de Sinistro</td>'+
			'</tr>'+
			'</table>'+
			'<hr width=100% />'+
			'DADOS GERAIS<br />'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr>'+
			'<td class=style1>Nome do Segurado </td><td>!</td>'+
			'<td>Nome Ramo</td><td>!</td>'+
			'<td align=center>% Part. Congenere</td>'+
			'</tr><tr>'+
			'<td class=style1>'+
			ISNULL(@CARRIER_NAME,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@NOME_RAMO,'')+
			'</td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@PART_CONGENERE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+    
			'<table width=100%>'+
			'<tr><td  class=style4>N.Apolice</td><td>!</td>'+
			'<td align=center>N.Ordem Apolice</td><td>!</td>'+
			'<td align=center>N. Endosso</td><td>!</td>'+
			'<td align=center>N.Ordem Endosso</td><td>!</td>'+
			'<td align=center>Vigência</td></tr>'+
			'<tr><td  class=style4>'+ 
			ISNULL(@POLICY_NUMBER,'')+
			'</td><td>!</td>'+
			'<td align=center>'+ 
			ISNULL(@N_ORDERM_APOLICE,'')+
			'</td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@N_ENDOSSO,'')+
			'</td><td>!</td>'+
			'<td align=center></td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@EFFECTIVE_DATETIME,'')+ CASE WHEN @EFFECTIVE_DATETIME!='' THEN  CASE WHEN @EXPIRY_DATE!='' THEN ' - ' ELSE '' END ELSE ''END +            
			ISNULL(@EXPIRY_DATE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100% cellspacing=5>'+
			'<tr><td>Moeda</td><td>!</td>'+
			'<td class=style2>N.Item</td><td>!</td>'+
			'<td align=right>Imp.Segurada</td><td>!</td>'+
			'<td align=right>Sinistro IRB</td><td>!</td>'+
			'<td>Dt.Sinistro</td><td>!</td>'+
			'<td>Dt.Aviso</td>'+
			'</tr><tr>'+
			'<td>R$</td><td>!</td>'+
			'<td class=style2>'+ 
			 ISNULL(CONVERT(VARCHAR,@N_ITEM_MERATIME),'')+
			 '</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@Limit),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@SINISTRO_IRB),'')+
			 '</td><td>!</td>'+
			'<td>'+
			 ISNULL(@DT_SINISTRO,'')+
			  '</td><td>!</td>'+
			'<td>'+
			 ISNULL(@DT_AVISO,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<hr width=99% />'+
			'DADOS ESPECIFICOS <br/><div>TRANSP/CASCO/AERONAUTICO/P.RURAL/CRED.EXP./R.C.GERAL/CRED.INT.</div>'+
			'<hr width=99% />'+
			'<table width=99%><tr>'+
			'<td>N.Averbacao Certif.</td><td>!</td>'+
			'<td>Meio Ttransporte</td><td>!</td>'+
			'<td>N.Placa</td><td>!</td>'+
			'<td>PrefiXo</td><td>!</td>'+
			'<td>Nome de Embrcação</td></tr>'+
			'<tr><td>'+ 
			ISNULL(@N_AVERBACAO_CERTIF,'')+
			 '</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@MEIO_TTRANSPORTE,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@N_PLACA,'')+
			'</td><td>!</td>'+
			'<td>'+
			 ISNULL(@PREFIXO,'')+
			 '</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@NOME_DA_EMBARCACAO,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99%/>'+
			'<table width=99%>'+
			'<tr><td>Empresa Transportadora</td><td>!</td>'+
			'<td class=style3>Mercadoria Sinisrtrada</td><td>!</td>'+
			'<td>Natureza Danos</td><td>!</td>'+
			'<td>Tipo Cobertura</td></tr>'+
			'<tr><td>'+
			ISNULL(@EMPRESA_TRANS,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@MERCADORIA_SINISTRADA,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DETAIL_TYPE_DESCRIPTION,'')+
			'</td><td>!</td>'+
			'<td>RC OPERADOR P</td></tr>'+
			'</table>'+
			'<hr WIDTH=99%/>'+
			'<table width=99%>'+
			'<tr><td>Origem</td><td>!</td>'+
			'<td>Destino</td><td>!</td>'+
			'<td colspan=3>Local de Ocorrencia</td><td>!</td>'+
			'<td>Cidade</td><td>!</td>'+
			'<td>UF</td></tr>'+
			'<tr><td>'+
			ISNULL(@ORIGEM,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@DESTINO,'')+
			'</td><td>!</td>'+
			'<td colspan=3>'+ 
			ISNULL(@LOCAL_DE_OCORRENCIA,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@CIDADE,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@UF,'')+
			'</td></tr>'+
			'<tr><td>Data Saida</td><td>!</td>'+
			'<td>Data Chegada</td><td>!</td>'+
			'<td>Data Vistoria</td><td>!</td>'+
			'<td>Local de Vistoria</td></tr>'+
			'<tr><td>'+ 
			ISNULL(@DATA_SAIDA,'')+ 
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DATA_CHEGADA,'')+
			'</td><td>!</td>'+
			'<td>'+
			 ISNULL(@DATA_VISTORIA,'')+
			'</td><td>!</td>'+
			'<td>ESTRADA DA ILHA DA MADEIRA S/N</td></tr>'+
			'</table>'+
			'<hr WIDTH=99% />'+
			'<table width=99%>'+
			'<tr>'+
			'<td>Participações</td><td>!</td>'+
			'<td>VI. Total Lider</td><td>!</td>'+
			'<td>Part.Cosseg (R$)</td><td>!</td>'+
			'<td>Part .Qt .Moeda</td><td>!</td>'+
			'<td>Dt. Base</td><td>!</td>'+
			'<td>Fat. Index.</td></tr>'+
			'<tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td></tr>'+
			'<tr><td>Estimativa</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @OUT_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@OUT_COVERAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td  align=right>'+
			 ISNULL(CASE WHEN @RESV_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_COVERAGES) ELSE '' END,'')+
			  '</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_ESTIMATIVE!=0.00 THEN CONVERT(VARCHAR,@MOEDA_ESTIMATIVE) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(@INFLATION_RATE,'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Indenização</td><td>!</td>'+
			'<td align=right>'+ISNULL(CASE WHEN @PAYMENT_COVERAGE!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_COVERAGE) ELSE '' END,'')+'</td><td>!</td>'+
			'<td align=right>'+ISNULL(CASE WHEN @RESV_PAYMENT_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_PAYMENT_COVERAGES) ELSE '' END,'')+'</td><td>!</td>'+
			'<td align=right>'+ISNULL(CASE WHEN @MOEDA_PAYMENT_ESTIMATIVE!=0.00 THEN CONVERT(VARCHAR,@MOEDA_PAYMENT_ESTIMATIVE) ELSE '' END,'')+'</td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td align=right>'+ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+'</td>'+
			'</tr><tr>'+
			'<td>Honorários</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @PAYMENT_PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_PROFESSIONAL_SERVICES) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			 ISNULL(CASE WHEN @RESV_PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@RESV_PROFESSIONAL_SERVICES) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CASE WHEN @MOEDA_HONOR!=0.00 THEN CONVERT(VARCHAR,@MOEDA_HONOR) ELSE '' END,'')+
			'</td><td>!</td>' +
			'<td align=right></td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(@INFLATION_RATE,'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Despesas</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @PAYMENT_SALVAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_SALVAGE_SUBROGATION) ELSE '' END,'')+
			 '</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @RESV_LOSS_SALAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_LOSS_SALAGE_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			 ISNULL(CASE WHEN @MOEDA_DESPESAS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_DESPESAS) ELSE '' END,'')+
			  '</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			 ISNULL(@INFLATION_RATE,'')+
			'</td>'+
			'</tr><tr>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'<tr><td>Ressarcimento</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @PAYMENT_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @RESV_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_RESSARCIMENTO!=0.00 THEN CONVERT(VARCHAR,@MOEDA_RESSARCIMENTO) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(@INFLATION_RATE,'')+
			'</td>'+
			'<td></td>'+
			'</tr><tr>'+
			'<td>Salvados</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @PAYMENT_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_SALVAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @RESV_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_SALVAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_SALVADOS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_SALVADOS) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			 --ISNULL(@INFLATION_RATE,'')+
			'</td>'+
			'</tr><tr>'+
			'</tr><tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td align=right><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<tr><td>Total Geral</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @TOTAL_PAYMENT_GEREAL_LIDER!=0.00 THEN CONVERT(VARCHAR,@TOTAL_PAYMENT_GEREAL_LIDER) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @TOTAL_PAYMENT_GERAL_COSSEG!=0.00 THEN CONVERT(VARCHAR,@TOTAL_PAYMENT_GERAL_COSSEG) ELSE '' END,'')+ 
			 '</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @TOTAL_PAYMENT_GERAL_MOEDA!=0.00 THEN CONVERT(VARCHAR,@TOTAL_PAYMENT_GERAL_MOEDA) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td>F-A-J-</td><td></td>'+
			'<td></td>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'</table>'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Colocar a nossa disposição quantia referente a sua particioação até  0000000<br />'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Sinistro Sorteio já liquidado. A cota parte dessa congenere sera debitada<br />'+
			' &nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; através do L.C.C (Lancto.Contra Corrente).<br />'+
			'<table width=60%><tr><td>Observação:</td><td>C.S </td><td>'+ 
				ISNULL(@CLAIM_NUMBER,'')+
			'</td><td>N.Carta Aviso</td><td>'+ISNULL(@LETTERSEQUENCENUMBER,'')+'</td><td>O.E.</td><td>05</td></tr></table>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			ISNULL(@CITY,'')+
			'</td>'+
			'<td>'+ 
			ISNULL(@BRANCH_CITY_DETAIL,'')+
			'</td>'+
			'<td></td>'+
			'</tr>'+
			'<tr>'+
			'<td colspan=2><hr width=70% /></td>'+
			'<td><hr width=90% /></td>'+
			'</tr><tr>'+
			'<td>Local e Data</td>'+
			'<td></td>'+
			'<td>Assinatura</td>'+
			'</tr>'+
			'</table>'+
			'</td>'+
			'</tr>'+
			'</table>'+
			'</body>'+
			'</html>'
END
--SELECT @strHTML29 AS LETTER29

---------Letter Number 31-----------------
IF(@PROCESS_TYPE='CLM_LETTER_31')
    BEGIN
		--DECLARE @strHTML VARCHAR(8000)
		SET @strHTML =
			'<html>'+
			'<head>'+
			'<style type=text/css>'+
			'body{font-family:Arial; font-size:12px; margn-left:0.5cm;margin-top:1.0cm;margin-right:0.5cm;margin-bottom:0.5cm;}'+
			'hr {border-top: 1px dashed #000; margin-left:0px; text-align: left}'+
			'.style1{width:52%;}'+
			'.style2{width:27%;}'+
			'.style3{width:32%;}'+
			'</style>'+
			'</head>'+
			'<body>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			'COMPANHIA DE SEGUROS ALIANÇA DA BAHIA <BR/> À Cosseguradora'+
			'<table  width=100%>'+
			'<tr>'+
			'<td colspan=3><table width=100%><tr><td>'+
			ISNULL(@REIN_COMAPANY_CODE,'')+ '</td><td>'+CASE WHEN @REIN_COMAPANY_CODE!='' THEN ' - ' ELSE '' END+'</td><td></td><td>' +
			ISNULL(@REIN_COMAPANY_NAME,'')+
			'</td> <td>Sinistro Lider</td><td>'+
			CASE WHEN @OFFCIAL_CLAIM_NUMBER!='' THEN ISNULL(@OFFCIAL_CLAIM_NUMBER,'') ELSE '' END+
			'</td></tr></table></td>'+
			'</tr>'+
			'<tr>'+
			'<td>Ref&nbsp;: </td>'+
			'<td>(X)&nbsp;&nbsp;Aviso de Sinistro</td>'+
			'<td>(&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;Despesas/Honorários</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;Cobrança de Sinistro</td>'+
			'<td>(&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;Ressarc.Salvados</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp; Pagto. parcial/Adiantamento</td>'+
			'<td>( X )&nbsp;&nbsp; Encerramento sem Indenização</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp; Pagamento Final</td>'+
			'<td>(&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp; Reabertura de Sinistro</td>'+
			'</tr>'+
			'</table>'+
			'<hr width=100% />'+
			'DADOS GERAIS<br />'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr>'+
			'<td class=style1>Nome do Segurado </td><td>!</td>'+
			'<td>Nome Ramo</td><td>!</td>'+
			'<td align=center>% Part. Congenere</td>'+
			'</tr><tr>'+
			'<td class=style1>'+ 
			ISNULL(@CARRIER_NAME,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@NOME_RAMO,'')+
			'</td><td>!</td>'+
			'<td align=center>'+ 
			ISNULL(@PART_CONGENERE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+    
			'<table width=100%>'+
			'<tr><td>N.Apolice</td><td>!</td>'+
			'<td>N.Ordem Apolice</td><td>!</td>'+
			'<td>N. Endosso</td><td>!</td>'+
			'<td>N.Ordem Endosso</td><td>!</td>'+
			'<td align=center>Vigência</td></tr>'+
			'<tr><td align=right>'+ 
			ISNULL(@POLICY_NUMBER,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(@N_ORDERM_APOLICE,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(@N_ENDOSSO,'')+
			'</td><td>!</td>'+
			'<td  align=right></td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@EFFECTIVE_DATETIME,'')+ CASE WHEN @EFFECTIVE_DATETIME!='' THEN  CASE WHEN @EXPIRY_DATE!='' THEN ' - ' ELSE '' END ELSE ''END +            
			ISNULL(@EXPIRY_DATE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100% cellspacing=5>'+
			'<tr><td>Moeda</td><td>!</td>'+
			'<td class=style2>N.Item</td><td>!</td>'+
			'<td align=right>Imp.Segurada</td><td>!</td>'+
			'<td align=right>Sinistro IRB</td><td>!</td>'+
			'<td>Dt.Sinistro</td><td>!</td>'+
			'<td>Dt.Aviso</td>'+
			'</tr><tr>'+
			'<td>R$</td><td>!</td>'+
			'<td class=style2 align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@N_ITEM),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@Limit),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@SINISTRO_IRB),'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DT_SINISTRO,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DT_AVISO,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr>'+
			'<td>Participações</td><td>!</td>'+
			'<td>VI. Total Lider</td><td>!</td>'+
			'<td>Part.Cosseg (R$)</td><td>!</td>'+
			'<td>Part .Qt .Moeda</td><td>!</td>'+
			'<td>Dt. Base</td><td>!</td>'+
			'<td>Fat. Index.</td></tr>'+
			'<tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td></tr>'+
			'<tr><td>Estimativa</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@OUT_COVERAGES),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@RESV_COVERAGES),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@MOEDA_ESTIMATIVE),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Indenização</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td>'+
			'</tr><tr>'+
			'<td>Honorários</td><td>!</td>'+
			'<td align=right>'+ 
			--HTML.Append(ds.Tables[0].Rows[0]["TL_HONOR"].ToString()); 
			--ISNULL(CONVERT(VARCHAR,@PROFESSIONAL_SERVICES),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--HTML.Append(ds.Tables[0].Rows[0]["COSSEG_HONOR"].ToString());
			--ISNULL(CONVERT(VARCHAR,@RESV_PROFESSIONAL_SERVICES),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--HTML.Append(ds.Tables[0].Rows[0]["MOEDA_HONOR"].ToString()); 
			--ISNULL(CONVERT(VARCHAR,@MOEDA_HONOR),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			--HTML.Append(ds.Tables[0].Rows[0]["INFLATION_RATE"].ToString());
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Despesas</td><td>!</td>'+
			'<td align=right>'+ 
			--HTML.Append(ds.Tables[0].Rows[0]["Loss_Salvage_Subrogation_Despesas"].ToString()); 
			--ISNULL(CONVERT(VARCHAR,@LOSS_SALVAGE_SUBROGATION),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--HTML.Append(ds.Tables[0].Rows[0]["COSSEG_DESPESAS"].ToString()); 
			--ISNULL(CONVERT(VARCHAR,@RESV_LOSS_SALAGE_SUBROGATION),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@MOEDA_DESPESAS),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr>'+
			'<tr><td align=right><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+ 
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td>'+
			'</tr>'+
			'<tr><td>Ressarcimento</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CASE WHEN @OUT_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@OUT_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CASE WHEN @RESV_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CASE WHEN @MOEDA_RESSARCIMENTO!=0.00 THEN CONVERT(VARCHAR,@MOEDA_RESSARCIMENTO) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Salvados</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CASE WHEN @OUT_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@OUT_SALVAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CASE WHEN @RESV_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_SALVAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CASE WHEN @MOEDA_SALVADOS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_SALVADOS) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td  align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr><tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td align=right><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<tr><td>Total Geral</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CASE WHEN @TOTAL_GEREAL_LIDER!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GEREAL_LIDER) ELSE '' END,'')+ 
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CASE WHEN @TOTAL_GERAL_COSSEG!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GERAL_COSSEG) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CASE WHEN @TOTAL_GERAL_MOEDA!=0.00 THEN CONVERT(VARCHAR,@TOTAL_GERAL_MOEDA) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td>F-A-J-</td><td></td>'+
			'<td></td>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'</table>'+
			'(&nbsp;X&nbsp;)&nbsp;&nbsp;Colocar a nossa disposição quantia referente a sua participação até 00000000'+
			ISNULL(@LETTER_GENERATION_DATE,'')+
			'<br />'+
			'(&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;Sinistro Sorteio já liquidado.A cota parte dessa congenere sera debitada<br />'+
			' &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; através do L.C.C.(Lancto.Conta Corrente).<br />'+
			'<table width=80%><tr><td>Observação&nbsp;&nbsp;:&nbsp; &nbsp;C.S </td><td>'+ 
			ISNULL(@CLAIM_NUMBER,'')+
			'</td><td>N.Carta Aviso</td><td></td><td>N.Carta Cobrança</td><td>'+ISNULL(@LETTERSEQUENCENUMBER,'')+'</td><td>O.E.</td><td>'+ 
			ISNULL(@BRANCH_CODE,'')+
			'</td><td>JUDICIAL</td></tr></table><br />'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+ 
			ISNULL(@CITY,'')+
			'</td>'+
			'<td>'+ 
			ISNULL(@BRANCH_CITY_DETAIL,'')+
			'</td>'+
			'<td></td>'+
			'</tr>'+
			'<tr>'+
			'<td colspan=2><hr width=70% /></td>'+
			'<td><hr width=90% /></td>'+
			'</tr><tr>'+
			'<td>Local e Data</td>'+
			'<td></td>'+
			'<td>Assinatura</td>'+
			'</tr>'+
			'</table>'+
			'</td>'+
			'</tr>'+
			'</table>'+
			'</body>'+
			'</html>'
END
--SELECT @strHTML31 AS LETTER31

----------LETTER FOR LETTER NUMBER 32----------------
IF(@PROCESS_TYPE='CLM_LETTER_32')
    BEGIN
		--DECLARE @strHTML AS VARCHAR(8000)
		SET @strHTML =
			'<html>'+
			'<head>'+
			'<style type=text/css>'+
			'body{font-family:Arial; font-size:12px; margn-left:0.5cm;margin-top:1.0cm;margin-right:0.5cm;margin-bottom:0.5cm;}'+
			'hr {border-top: 1px dashed #000; margin-left:0px; text-align: left}'+
			'.style1{width:53%;}'+
			'.style2{width: 74%;}'+
			'</style>'+
			'</head>'+
			'<body>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			'0000000000000000000000000000000<br />'+
			'À Cosseguradora<br />'+
			'<table  width=100%>'+
			'<tr>'+
			'<td colspan=3><table width=100%><tr><td>'+
			ISNULL(@REIN_COMAPANY_CODE,'')+
			'</td><td>'+CASE WHEN @REIN_COMAPANY_CODE!='' THEN ' - ' ELSE '' END+'</td><td></td><td>' +
			ISNULL(@REIN_COMAPANY_NAME,'')+             
			'</td> <td>Sinistro Lider</td><td>'+
			CASE WHEN @OFFCIAL_CLAIM_NUMBER!='' THEN ISNULL(@OFFCIAL_CLAIM_NUMBER,'') ELSE '' END+
			'</td></tr></table></td>'+
			'</tr>'+
			'<tr>'+
			'<td>Ref: </td>'+
			'<td>(X)&nbsp;&nbsp;Aviso de Sinistro</td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp;Despesas/Honorários</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp;Cobrança de Sinistro</td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp;Ressarc.Salvados</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp; Pagto. parcial/Adiantamento</td>'+
			'<td>(X)&nbsp;&nbsp; Encerramento sem Indenização</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp; Pagamento Final</td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp; Reabertura de Sinistro</td>'+
			'</tr>'+
			'</table>'+
			'<hr width=100% />'+
			'DADOS GERAIS<br />'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr>'+
			'<td class=style1>Nome do Segurado </td><td>!</td>'+
			'<td>Nome Ramo</td><td>!</td>'+
			'<td align=center>% Part. Congenere</td>'+
			'</tr><tr>'+
			'<td class=style1>'+
			ISNULL(@CARRIER_NAME,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@NOME_RAMO,'')+
			'</td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@PART_CONGENERE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr><td>N.Apolice</td><td>!</td>'+
			'<td> N.Ordem Apolice</td><td>!</td>'+
			'<td> N. Endosso</td><td>!</td>'+
			'<td> N.Ordem Endosso</td><td>!</td>'+
			'<td align=center> Vigência</td></tr>'+
			'<tr><td align=right>'+
			ISNULL(@POLICY_NUMBER,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(@N_ORDERM_APOLICE,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(@N_ENDOSSO,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@EFFECTIVE_DATETIME,'')+ CASE WHEN @EFFECTIVE_DATETIME!='' THEN  CASE WHEN @EXPIRY_DATE!='' THEN ' - ' ELSE '' END ELSE ''END +

			ISNULL(@EXPIRY_DATE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr><td>Moeda</td><td>!</td>'+
			'<td>N.Item</td><td>!</td>'+
			'<td ALIGN=RIGHT>Imp.Segurada</td><td>!</td>'+
			'<td>Sinistro IRB</td><td>!</td>'+
			'<td>Dt.Sinistro</td><td>!</td>'+
			'<td>Dt.Aviso</td>'+
			'</tr><tr>'+
			'<td>R$</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@N_ITEM),'')+ '</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@Limit),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@SINISTRO_IRB),'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DT_SINISTRO,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DT_AVISO,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<hr width=99% />'+
			'DADOS ESPECIFICOS VG/APC'+
			'<hr width=99% />'+
			'<table width=100%>'+
			'<tr><td>Nome do Estipulante</td><td>!</td>'+
			'<td>Garantia reclamada do Segurado sinistrado</td></tr>'+
			'<tr><td>'+ 
			ISNULL(@CARRIER_NAME,'')+'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DETAIL_TYPE_DESCRIPTION,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99%/>'+
			'<table width=100%>'+
			'<tr><td class=style2>Nome do Segurado Principal</td><td>!</td>'+
			'<td>Dt.Nascimento</td></tr>'+
			'</tr><tr>'+
			'<td class=style2>'+ 
			ISNULL(@CLAIMANT_NAME,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@CO_APPL_DOB,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99% />'+
			'<table width=100%>'+
			'<tr><td class=style2>Nome do Segurado Sinistrado</td><td>!</td>'+
			'<td>Dt.Nascimento</td></tr>'+
			'<tr><td class=style2>'+
			ISNULL(@CLAIMANT_NAME,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@CO_APPL_DOB,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99% />'+
			'<table width=99%>'+
			'<tr>'+
			'<td>Participações</td><td>!</td>'+
			'<td>VI. Total Lider</td><td>!</td>'+
			'<td>Part.Cosseg (R$)</td><td>!</td>'+
			'<td>Part .Qt .Moeda</td><td>!</td>'+
			'<td>Dt. Base</td><td>!</td>'+
			'<td>Fat. Index.</td></tr>'+
			'<tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=90% /></td></tr>'+
			'<tr><td>Estimativa</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@OUT_COVERAGES),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@RESV_COVERAGES),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@MOEDA_ESTIMATIVE),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Indenização</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td>'+
			'</tr><tr>'+
			'<td>Honorários</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@PROFESSIONAL_SERVICES),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@RESV_PROFESSIONAL_SERVICES),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@MOEDA_HONOR),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Despesas</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@LOSS_SALVAGE_SUBROGATION),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@RESV_LOSS_SALAGE_SUBROGATION),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@MOEDA_DESPESAS),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'<tr><td>Ressarcimento</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@OUT_SUBROGATION),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@RESV_SUBROGATION),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@MOEDA_RESSARCIMENTO),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Salvados</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@OUT_SALVAGES),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@RESV_SALVAGES),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@MOEDA_SALVADOS),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td  align=right>'+
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr><tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td align=right><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<tr><td>Total Geral</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@TOTAL_GEREAL_LIDER),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@TOTAL_GERAL_COSSEG),'')+ 
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@TOTAL_GERAL_MOEDA),'')+
			'</td><td>!</td>'+
			'<td>F-A-J-</td><td></td>'+
			'<td></td>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'</table>'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Colocar a nossa disposição quantia referente a sua participação até 00000000<br />'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Sinistro Sorteio já liquidado.A cota parte dessa congenere sera debitada<br />'+
			' &nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; através do L.C.C.(Lancto.Conta Corrente).<br />'+
			'<table width=80%><tr><td>Observação:</td><td>C.S </td><td>'+
			ISNULL(@CLAIM_NUMBER,'')+
			'</td><td>N.Carta Aviso</td><td>'+ISNULL(@LETTERSEQUENCENUMBER,'')+'</td><td>N.Carta Cobrança</td><td></td><td>O.E.</td><td>'+
			ISNULL(@BRANCH_CODE,'')+
			'</td></tr></table>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			ISNULL(@CITY,'')+
			'</td>'+
			'<td>'+
			ISNULL(@BRANCH_CITY_DETAIL,'')+
			'</td>'+
			'<td></td>'+
			'</tr>'+
			'<tr>'+
			'<td colspan=2><hr width=70% /></td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<td>Local e Data</td>'+
			'<td></td>'+
			'<td>Assinatura</td>'+
			'</tr>'+
			'</table>'+
			'</td>'+
			'</tr>'+
			'</table>'+
			'</body>'+
			'</html>'
END
--SELECT @strHTML32 AS LETTER32

-------------LETTER NUMBER 33---------------
IF(@PROCESS_TYPE='CLM_LETTER_33')
    BEGIN
		--DECLARE @strHTML VARCHAR(8000)
		set @strHTML=
			'<html>'+
			'<head>'+
			'<style type=text/css>'+
			'body{font-family:Arial; font-size:12px; margn-left:0.5cm;margin-top:1.0cm;margin-right:0.5cm;margin-bottom:0.5cm;}'+
			'hr {border-top: 1px dashed #000; margin-left:0px; text-align: left}'+
			'.style1{width:57%;}'+
			'.style2{width:30%;}'+
			'.style3{width:45%;}'+
			'.style4{width:15%;}'+
			'</style>'+
			'</head>'+
			'<body>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			'0000000000000000000000000000000<br />'+
			'À Cosseguradora<br />'+
			'<table  width=100%>'+
			'<tr>'+
			'<td colspan=3><table width=100%><tr><td>'+
			ISNULL(@REIN_COMAPANY_CODE,'')+
			'</td><td>'+CASE WHEN @REIN_COMAPANY_CODE!='' THEN ' - ' ELSE '' END+'</td><td></td><td>' +
			ISNULL(@REIN_COMAPANY_NAME,'')+
			'</td> <td>Sinistro Lider</td><td>'+
			CASE WHEN @OFFCIAL_CLAIM_NUMBER!='' THEN ISNULL(@OFFCIAL_CLAIM_NUMBER,'') ELSE '' END+
			'</td></tr></table></td>'+
			'</tr>'+
			'<tr>'+
			'<td>Ref&nbsp;: </td>'+
			'<td>( X )&nbsp;&nbsp;Aviso de Sinistro</td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp;Despesas/Honorários</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp;Cobrança de Sinistro</td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp;Ressarc.Salvados</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp; Pagto. parcial/Adiantamento</td>'+
			'<td>( X )&nbsp;&nbsp; Encerramento sem Indenização</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp; Pagamento Final</td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp; Reabertura de Sinistro</td>'+
			'</tr>'+
			'</table>'+
			'<hr width=100% />'+
			'DADOS GERAIS<br />'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr>'+
			'<td class=style1>Nome do Segurado </td><td>!</td>'+
			'<td>Nome Ramo</td><td>!</td>'+
			'<td align=center>% Part. Congenere</td>'+
			'</tr><tr>'+
			'<td class=style1>'+
			ISNULL(@CARRIER_NAME,'')+'</td><td>!</td>'+
			'<td>'+
			ISNULL(@NOME_RAMO,'')+
			'</td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@PART_CONGENERE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr><td class=style4>N.Apolice</td><td>!</td>'+
			'<td> N.Ordem Apolice</td><td>!</td>'+
			'<td> N. Endosso</td><td>!</td>'+
			'<td> N.Ordem Endosso</td><td>!</td>'+
			'<td align=center> Vigência</td></tr>'+
			'<tr><td  class=style4 align=right>'+
			ISNULL(@POLICY_NUMBER,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(@N_ORDERM_APOLICE,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(@N_ENDOSSO,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@EFFECTIVE_DATETIME,'')+ CASE WHEN @EFFECTIVE_DATETIME!='' THEN  CASE WHEN @EXPIRY_DATE!='' THEN ' - ' ELSE '' END ELSE ''END +

			ISNULL(@EXPIRY_DATE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr><td>Moeda</td><td>!</td>'+
			'<td class=style2>N.Item</td><td>!</td>'+
			'<td align=right>Imp.Segurada</td><td>!</td>'+
			'<td>Sinistro IRB</td><td>!</td>'+
			'<td>Dt.Sinistro</td><td>!</td>'+
			'<td>Dt.Aviso</td>'+
			'</tr><tr>'+
			'<td>R$</td><td>!</td>'+
			'<td class=style2>'+
			ISNULL(CONVERT(VARCHAR,@N_ITEM),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@Limit),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@SINISTRO_IRB),'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@DT_SINISTRO,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@DT_AVISO,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<hr width=99% />'+
			'DADOS ESPECIFICOS INC/LC/RD/R.ENG./ROUBO'+
			'<hr width=99% />'+
			'<table width=100%>'+
			'<tr><td class=style3>Local de Ocorrencia</td><td>!</td>'+
			'<td>Cidade</td><td>!</td>'+
			'<td>UF</td></tr>'+
			'<tr><td class=style3>'+
			ISNULL(@LOCAL_DE_OCORRENCIA,'')+
			'</td><td>!</td>'+
			'<td>' +
			ISNULL(@CIDADE,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@UF,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99%/>'+
			'<table width=100%>'+
			'<tr><td>N.Planta</td><td>!</td>'+
			'<td>Bens Sinistrados</td><td>!</td>'+
			'<td>Natureza do Evento</td><td>!</td>'+
			'<td>Ramo Atividade</td>'+
			'</tr><tr>'+
			'<td></td><td>!</td>'+
			'<td>' +
			ISNULL(@BENS_SINISTRADOS,'')+
			'</td><td>!</td>'+
			'<td>' +
			ISNULL(@DETAIL_TYPE_DESCRIPTION,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@ACTIVITY_TYPE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99% />'+
			'<table width=99%>'+
			'<tr>'+
			'<td>Participações</td><td>!</td>'+
			'<td>VI. Total Lider</td><td>!</td>'+
			'<td>Part.Cosseg (R$)</td><td>!</td>'+
			'<td>Part .Qt .Moeda</td><td>!</td>'+
			'<td>Dt. Base</td><td>!</td>'+
			'<td >Fat. Index.</td></tr>'+
			'<tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=99% /></td></tr>'+
			'<tr><td>Estimativa</td><td>!</td>'+
			'<td align=right>' +
			--ISNULL(CONVERT(VARCHAR,@OUT_COVERAGES),'')+
			'</td><td>!</td>'+
			'<td align=right>' +
			--ISNULL(CONVERT(VARCHAR,@RESV_COVERAGES),'')+
			'</td><td>!</td>'+
			'<td align=right>' +
			--ISNULL(CONVERT(VARCHAR,@MOEDA_ESTIMATIVE),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Indenização</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td>'+
			'</tr><tr>'+
			'<td>Honorários</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@PROFESSIONAL_SERVICES),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@RESV_PROFESSIONAL_SERVICES),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@MOEDA_HONOR),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Despesas</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@LOSS_SALVAGE_SUBROGATION),'')+
			'</td><td>!</td>'+
			'<td align=right>' +
			--ISNULL(CONVERT(VARCHAR,@RESV_LOSS_SALAGE_SUBROGATION),'')+
			'</td><td>!</td>'+
			'<td align=right>' +
			--ISNULL(CONVERT(VARCHAR,@MOEDA_DESPESAS),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'<tr><td>Ressarcimento</td><td>!</td>'+
			'<td align=right>' +
			--ISNULL(CONVERT(VARCHAR,@OUT_SUBROGATION),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@RESV_SUBROGATION),'')+
			'</td><td>!</td>'+
			'<td align=right>' +
			--ISNULL(CONVERT(VARCHAR,@MOEDA_RESSARCIMENTO),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Salvados</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@OUT_SALVAGES),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@RESV_SALVAGES),'')+
			'</td><td>!</td>'+
			'<td align=right>' +
			--ISNULL(CONVERT(VARCHAR,@MOEDA_SALVADOS),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr><tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td align=right><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<tr><td>Total Geral</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@TOTAL_GEREAL_LIDER),'')+
			'</td><td>!</td>'+
			'<td align=right>' +
			--ISNULL(CONVERT(VARCHAR,@TOTAL_GERAL_COSSEG),'')+ 
			'</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@TOTAL_GERAL_MOEDA),'')+
			'</td><td>!</td>'+
			'<td>F-A-J-</td><td></td>'+
			'<td></td>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'</table>'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Colocar a nossa disposição quantia referente a sua participação até 00000000<br />'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Sinistro Sorteio já liquidado. A cota parte dessa congenere sera debitada<br />'+
			' &nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; através do L.C.C (Lancto.Contra Corrente).<br />'+
			'<table width=60%><tr><td>Observação:</td><td>C.S </td><td>'+
			ISNULL(@CLAIM_NUMBER,'')+
			'</td><td>N.Carta Aviso</td><td>'+ISNULL(@LETTERSEQUENCENUMBER,'')
			--CASE WHEN @PROCESS_TYPE='CLM_LETTER_21' THEN ISNULL(@LETTER_SEQUENCE_NO1,'')
			-- ELSE 
			--   CASE WHEN @PROCESS_TYPE='CLM_LETTER_26' THEN ISNULL(@LETTER_SEQUENCE_NO,'') 
			--      ELSE 'vbn' 
			--    END  
			-- END 
			+'</td><td>O.E.</td><td>'+
			ISNULL(@BRANCH_CODE,'')+
			'</td></tr></table>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>' 	+	ISNULL(@CITY,'')+ '</td>'+
			'<td>'+
			ISNULL(@BRANCH_CITY_DETAIL,'')+
			'</td>'+
			'<td></td>'+
			'</tr>'+
			'<tr>'+
			'<td colspan=2><hr width=70% /></td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<td>Local e Data</td>'+
			'<td></td>'+
			'<td>Assinatura</td>'+
			'</tr>'+
			'</table>'+
			'</td>'+
			'</tr>'+
			'</table>'+
			'</body>'+
			'</html>'
END
--SELECT @strHTML33 AS LETTER33
IF(@PROCESS_TYPE='CLM_LETTER_34')
    BEGIN
	--	DECLARE @strHTML VARCHAR(8000)
		SET @strHTML =
			'<html>'+
			'<head>'+
			'<style type=text/css>'+
			'body{font-family:Arial; font-size:12px; margn-left:0.5cm;margin-top:1.0cm;margin-right:0.5cm;margin-bottom:0.5cm;}'+
			'hr {border-top: 1px dashed #000; margin-left:0px; text-align: left}'+
			'.style1{width:57%;}'+
			'.style2{width:30%;}'+
			'.style3{width:15%;}'+
			'</style>'+
			'</head>'+
			'<body>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			'0000000000000000000000000000000<br />'+
			'À Cosseguradora<br />'+
			'<table  width=100%>'+
			'<tr>'+
			'<td colspan=3><table width=100%><tr><td>'+
			ISNULL(@REIN_COMAPANY_CODE,'')+ '</td><td>'+CASE WHEN @REIN_COMAPANY_CODE!='' THEN ' - ' ELSE '' END+'</td><td></td><td>' +
			ISNULL(@REIN_COMAPANY_NAME,'')+
			'</td> <td>Sinistro Lider</td><td>'+
			CASE WHEN @OFFCIAL_CLAIM_NUMBER!='' THEN ISNULL(@OFFCIAL_CLAIM_NUMBER,'') ELSE '' END+
			'</td></tr></table></td>'+
			'</tr>'+
			'<tr>'+
			'<td>Ref: </td>'+
			'<td>( X )&nbsp;&nbsp;Aviso de Sinistro</td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp;Despesas/Honorários</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp;Cobrança de Sinistro</td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp;Ressarc.Salvados</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp; Pagto. parcial/Adiantamento</td>'+
			'<td>( X )&nbsp;&nbsp; Encerramento sem Indenização</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp; Pagamento Final</td>'+
			'<td>(&nbsp;&nbsp;&nbsp; )&nbsp;&nbsp; Reabertura de Sinistro</td>'+
			'</tr>'+
			'</table>'+
			'<hr width=100% />'+
			'DADOS GERAIS<br />'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr>'+
			'<td class=style1>Nome do Segurado </td><td>!</td>'+
			'<td>Nome Ramo</td><td>!</td>'+
			'<td align=center>% Part. Congenere</td>'+
			'</tr><tr>'+
			'<td class=style1>'+
			ISNULL(@CARRIER_NAME,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@NOME_RAMO,'')+
			'</td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@PART_CONGENERE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr><td class=style3>N.Apolice</td><td>!</td>'+
			'<td>N.Ordem Apolice</td><td>!</td>'+
			'<td>N. Endosso</td><td>!</td>'+
			'<td>N.Ordem Endosso</td><td>!</td>'+
			'<td align=center>Vigência</td></tr>'+
			'<tr><td class=style3  align=right>'+
			ISNULL(@POLICY_NUMBER,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(@N_ORDERM_APOLICE,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(@N_ENDOSSO,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@EFFECTIVE_DATETIME,'')+ CASE WHEN @EFFECTIVE_DATETIME!='' THEN  CASE WHEN @EXPIRY_DATE!='' THEN ' - ' ELSE '' END ELSE ''END +

			ISNULL(@EXPIRY_DATE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100% cellspacing=5>'+
			'<tr><td>Moeda</td><td>!</td>'+
			'<td class=style2>N.Item</td><td>!</td>'+
			'<td align=right>Imp.Segurada</td><td>!</td>'+
			'<td align=right>Sinistro IRB</td><td>!</td>'+
			'<td>Dt.Sinistro</td><td>!</td>'+
			'<td>Dt.Aviso</td>'+
			'</tr><tr>'+
			'<td>R$</td><td>!</td>'+
			'<td class=style2  align=right>'+
			ISNULL(CONVERT(VARCHAR,@N_ITEM),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@Limit),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CONVERT(VARCHAR,@SINISTRO_IRB),'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@DT_SINISTRO,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@DT_AVISO,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<hr width=99% />'+
			'DADOS ESPECIFICOS AUT/RCV'+
			'<hr width=99% />'+
			'<table width=100%>'+
			'<tr><td>Marca Veiculo</td><td>!</td>'+
			'<td>N.Placa</td><td>!</td>'+
			'<td>N.Chassis</td><td>!</td>'+
			'<td>Ano</td><td>!</td>'+
			'<td>Tipo de Ocorrencia</td></tr>'+
			'<tr><td>'+
			ISNULL(@MARCA_VEICULO,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@N_PLACA,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(@N_CHASSIS,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(@ANO,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DETAIL_TYPE_DESCRIPTION,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99% />'+
			'<table width=99%>'+
			'<tr>'+
			'<td>Participações</td><td>!</td>'+
			'<td>VI. Total Lider</td><td>!</td>'+
			'<td>Part.Cosseg (R$)</td><td>!</td>'+
			'<td>Part .Qt .Moeda</td><td>!</td>'+
			'<td>Dt. Base</td><td>!</td>'+
			'<td>Fat. Index.</td></tr>'+
			'<tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td></tr>'+
			'<tr><td>Estimativa</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@OUT_COVERAGES),'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			--ISNULL(CONVERT(VARCHAR,@RESV_COVERAGES),'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			--ISNULL(CONVERT(VARCHAR,@MOEDA_ESTIMATIVE),'')+
			'</td><td>!</td>' +
			'<td align=right></td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+ 
			'<td></td>'+
			'</tr><tr>'+
			'<td>Indenização</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td>'+
			'</tr><tr>'+
			'<td>Honorários</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@PROFESSIONAL_SERVICES),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@RESV_PROFESSIONAL_SERVICES),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@MOEDA_HONOR),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Despesas</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@LOSS_SALVAGE_SUBROGATION),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@RESV_LOSS_SALAGE_SUBROGATION),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@MOEDA_DESPESAS),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			--HTML.Append(ds.Tables[0].Rows[0]["INFLATION_RATE"].ToString()); 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'<tr><td>Ressarcimento</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@OUT_SUBROGATION),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@RESV_SUBROGATION),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@MOEDA_RESSARCIMENTO),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Salvados</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@OUT_SALVAGES),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@RESV_SALVAGES),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@MOEDA_SALVADOS),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr><tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td align=right><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<tr><td>Total Geral</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@TOTAL_GEREAL_LIDER),'')+
			'</td><td>!</td>'+
			'<td align=right>' +
			--ISNULL(CONVERT(VARCHAR,@TOTAL_GERAL_COSSEG),'')+ 
			'</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@TOTAL_GERAL_MOEDA),'')+
			'</td><td>!</td>'+
			'<td>F-A-J-</td><td></td>'+
			'<td></td>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'</table>'+
			'<table width=100%><tr><td>'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Colocar a nossa disposição quantia referente a sua particioação até  0000000<br />'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Sinistro Sorteio já liquidado. A cota parte dessa congenere sera debitada<br />'+
			' &nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; através do L.C.C (Lancto.Contra Corrente).<br />'+
			'</td></tr></table>'+
			'<table width=60%><tr><td>Observação:</td><td>C.S </td><td>'+
			ISNULL(@CLAIM_NUMBER,'')+
			'</td><td>N.Carta Aviso</td><td>'+ISNULL(@LETTERSEQUENCENUMBER,'')+'</td><td>O.E.</td><td>'+
			ISNULL(@BRANCH_CODE,'')+
			'</td></tr></table>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+ 
			ISNULL(@CITY,'')+
			'</td>'+
			'<td>'+ 
			ISNULL(@BRANCH_CITY_DETAIL,'')+
			'</td>'+
			'<td></td>'+
			'</tr>'+
			'<tr>'+
			'<td colspan=2><hr width=70% /></td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<td>Local e Data</td>'+
			'<td></td>'+
			'<td>Assinatura</td>'+
			'</tr>'+
			'</table>'+
			'</td>'+
			'</tr>'+
			'</table>'+
			'</body>'+
			'</html>'
END
--SELECT @strHTML34 AS LETTER34

----------Letter Number 35-----------------
IF(@PROCESS_TYPE='CLM_LETTER_35')
    BEGIN
		--DECLARE @strHTML VARCHAR(8000)
		SET @strHTML=   
			'<html>'+
			'<head>'+
			'<style type=text/css>'+
			'body{font-family:Arial; font-size:12px; margn-left:0.5cm;margin-top:1.0cm;margin-right:0.5cm;margin-bottom:0.5cm;}'+
			'hr {border-top: 1px dashed #000; margin-left:0px; text-align: left}'+
			'.style1{width:59.5%;}'+
			'.style2{width:35%;}'+
			'.style3{width:32%;}'+
			'.style4{width:20%;}'+
			'</style>'+
			'</head>'+
			'<body>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			'0000000000000000000000000000000<br />'+
			'À Cosseguradora<br />'+
			'<table  width=100%>'+
			'<tr>'+
			'<td colspan=3><table width=100%><tr><td>'+ 
			ISNULL(@REIN_COMAPANY_CODE,'')+
			'</td><td>'+CASE WHEN @REIN_COMAPANY_CODE!='' THEN ' - ' ELSE '' END+'</td><td></td><td>' +
			ISNULL(@REIN_COMAPANY_NAME,'')+
			'</td> <td>Sinistro Lider</td><td>' +
			CASE WHEN @OFFCIAL_CLAIM_NUMBER!='' THEN ISNULL(@OFFCIAL_CLAIM_NUMBER,'') ELSE '' END+
			'</td></tr></table></td>'+
			'</tr>'+
			'<tr>'+
			'<td>Ref: </td>'+
			'<td>(X)&nbsp;&nbsp;Aviso de Sinistro</td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp;Despesas/Honorários</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp;Cobrança de Sinistro</td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp;Ressarc.Salvados</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp; Pagto. parcial/Adiantamento</td>'+
			'<td>(X)&nbsp;&nbsp; Encerramento sem Indenização</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp; Pagamento Final</td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp; Reabertura de Sinistro</td>'+
			'</tr>'+
			'</table>'+
			'<hr width=100% />'+
			'DADOS GERAIS<br />'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr>'+
			'<td class=style1>Nome do Segurado </td><td>!</td>'+
			'<td>Nome Ramo</td><td>!</td>'+
			'<td align=center>% Part. Congenere</td>'+
			'</tr><tr>'+
			'<td class=style1>'+
			ISNULL(@CARRIER_NAME,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@NOME_RAMO,'')+
			'</td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@PART_CONGENERE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+    
			'<table width=100%>'+
			'<tr><td  class=style4>N.Apolice</td><td>!</td>'+
			'<td align=center>N.Ordem Apolice</td><td>!</td>'+
			'<td align=center>N. Endosso</td><td>!</td>'+
			'<td align=center>N.Ordem Endosso</td><td>!</td>'+
			'<td align=center>Vigência</td></tr>'+
			'<tr><td  class=style4>'+ 
			ISNULL(@POLICY_NUMBER,'')+
			'</td><td>!</td>'+
			'<td align=center>'+ 
			ISNULL(@N_ORDERM_APOLICE,'')+
			'</td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@N_ENDOSSO,'')+
			'</td><td>!</td>'+
			'<td align=center></td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@EFFECTIVE_DATETIME,'')+ CASE WHEN @EFFECTIVE_DATETIME!='' THEN  CASE WHEN @EXPIRY_DATE!='' THEN ' - ' ELSE '' END ELSE ''END +            
			ISNULL(@EXPIRY_DATE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100% cellspacing=5>'+
			'<tr><td>Moeda</td><td>!</td>'+
			'<td class=style2>N.Item</td><td>!</td>'+
			'<td align=right>Imp.Segurada</td><td>!</td>'+
			'<td align=right>Sinistro IRB</td><td>!</td>'+
			'<td>Dt.Sinistro</td><td>!</td>'+
			'<td>Dt.Aviso</td>'+
			'</tr><tr>'+
			'<td>R$</td><td>!</td>'+
			'<td class=style2>'+ 
			 ISNULL(CONVERT(VARCHAR,@N_ITEM_TRANS),'')+
			 '</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@Limit),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@SINISTRO_IRB),'')+
			 '</td><td>!</td>'+
			'<td>'+
			 ISNULL(@DT_SINISTRO,'')+
			  '</td><td>!</td>'+
			'<td>'+
			 ISNULL(@DT_AVISO,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<hr width=99% />'+
			'DADOS ESPECIFICOS <br/><div>TRANSP/CASCO/AERONAUTICO/P.RURAL/CRED.EXP./R.C.GERAL/CRED.INT.</div>'+
			'<hr width=99% />'+
			'<table width=99%><tr>'+
			'<td>N.Averbacao Certif.</td><td>!</td>'+
			'<td>Meio Ttransporte</td><td>!</td>'+
			'<td>N.Placa</td><td>!</td>'+
			'<td>PrefiXo</td><td>!</td>'+
			'<td>Nome de Embrcação</td></tr>'+
			'<tr><td>'+ 
			ISNULL(@N_AVERBACAO_CERTIF,'')+
			 '</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@MEIO_TTRANSPORTE,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@N_PLACA,'')+
			'</td><td>!</td>'+
			'<td>'+
			 ISNULL(@PREFIXO,'')+
			 '</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@NOME_DA_EMBARCACAO,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99%/>'+
			'<table width=99%>'+
			'<tr><td>Empresa Transportadora</td><td>!</td>'+
			'<td class=style3>Mercadoria Sinisrtrada</td><td>!</td>'+
			'<td>Natureza Danos</td><td>!</td>'+
			'<td>Tipo Cobertura</td></tr>'+
			'<tr><td>'+
			ISNULL(@EMPRESA_TRANS,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@MERCADORIA_SINISTRADA,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DETAIL_TYPE_DESCRIPTION,'')+
			'</td><td>!</td>'+
			'<td>CLAUSULA A+</td></tr>'+
			'</table>'+
			'<hr WIDTH=99%/>'+
			'<table width=99%>'+
			'<tr><td>Origem</td><td>!</td>'+
			'<td>Destino</td><td>!</td>'+
			'<td colspan=3>Local de Ocorrencia</td><td>!</td>'+
			'<td>Cidade</td><td>!</td>'+
			'<td>UF</td></tr>'+
			'<tr><td>'+
			ISNULL(@ORIGEM,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@DESTINO,'')+
			'</td><td>!</td>'+
			'<td colspan=3>'+ 
			ISNULL(@LOCAL_DE_OCORRENCIA,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@CIDADE,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@UF,'')+
			'</td></tr>'+
			'<tr><td>Data Saida</td><td>!</td>'+
			'<td>Data Chegada</td><td>!</td>'+
			'<td>Data Vistoria</td><td>!</td>'+
			'<td>Local de Vistoria</td></tr>'+
			'<tr><td>'+ 
			ISNULL(@DATA_SAIDA,'')+ 
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DATA_CHEGADA,'')+
			'</td><td>!</td>'+
			'<td>'+
			 ISNULL(@DATA_VISTORIA,'')+
			'</td><td>!</td>'+
			'<td>ESTRADA DA ILHA DA MADEIRA S/N</td></tr>'+
			'</table>'+
			'<hr WIDTH=99% />'+
			'<table width=99%>'+
			'<tr>'+
			'<td>Participações</td><td>!</td>'+
			'<td>VI. Total Lider</td><td>!</td>'+
			'<td>Part.Cosseg (R$)</td><td>!</td>'+
			'<td>Part .Qt .Moeda</td><td>!</td>'+
			'<td>Dt. Base</td><td>!</td>'+
			'<td>Fat. Index.</td></tr>'+
			'<tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td></tr>'+
			'<tr><td>Estimativa</td><td>!</td>'+
			'<td align=right>'+ 
			--HTML.Append(ds.Tables[0].Rows[0]["TL_ESTIMATIVE"].ToString());
			--ISNULL(CONVERT(VARCHAR,@OUT_COVERAGES),'')+
			'</td><td>!</td>'+
			'<td  align=right>'+
			 --HTML.Append(ds.Tables[0].Rows[0]["COSSEG_ESTIMATIVE"].ToString());
			 --ISNULL(CONVERT(VARCHAR,@RESV_COVERAGES),'')+
			  '</td><td>!</td>'+
			'<td align=right>'+ 
			--HTML.Append(ds.Tables[0].Rows[0]["MOEDA_ESTIMATIVE"].ToString()); 
			--ISNULL(CONVERT(VARCHAR,@MOEDA_ESTIMATIVE),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			--HTML.Append(ds.Tables[0].Rows[0]["INFLATION_RATE"].ToString());
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Indenização</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td>'+
			'</tr><tr>'+
			'<td>Honorários</td><td>!</td>'+
			'<td align=right>'+
			--HTML.Append(ds.Tables[0].Rows[0]["TL_HONOR"].ToString()); 
			--ISNULL(CONVERT(VARCHAR,@PROFESSIONAL_SERVICES),'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			--HTML.Append(ds.Tables[0].Rows[0]["COSSEG_HONOR"].ToString()); 
			--ISNULL(CONVERT(VARCHAR,@RESV_PROFESSIONAL_SERVICES),'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			--HTML.Append(ds.Tables[0].Rows[0]["MOEDA_HONOR"].ToString()); 
			--ISNULL(CONVERT(VARCHAR,@MOEDA_HONOR),'')+
			'</td><td>!</td>' +
			'<td align=right></td><td>!</td>' +
			'<td align=right>' + 
			--HTML.Append(ds.Tables[0].Rows[0]["INFLATION_RATE"].ToString()); 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Despesas</td><td>!</td>'+
			'<td align=right>'+
			--HTML.Append(ds.Tables[0].Rows[0]["Loss_Salvage_Subrogation_Despesas"].ToString());
			--ISNULL(CONVERT(VARCHAR,@LOSS_SALVAGE_SUBROGATION),'')+
			 '</td><td>!</td>'+
			'<td align=right>'+ 
			--HTML.Append(ds.Tables[0].Rows[0]["COSSEG_DESPESAS"].ToString()); 
			--ISNULL(CONVERT(VARCHAR,@RESV_LOSS_SALAGE_SUBROGATION),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			 --HTML.Append(ds.Tables[0].Rows[0]["MOEDA_DESPESAS"].ToString());
			 --ISNULL(CONVERT(VARCHAR,@MOEDA_DESPESAS),'')+
			  '</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			 --HTML.Append(ds.Tables[0].Rows[0]["INFLATION_RATE"].ToString()); 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'<tr><td>Ressarcimento</td><td>!</td>'+
			'<td align=right>'+
			--HTML.Append(ds.Tables[0].Rows[0]["TL_RESSARCIMENTO"].ToString()); 
			--ISNULL(CONVERT(VARCHAR,@OUT_SUBROGATION),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--HTML.Append(ds.Tables[0].Rows[0]["COSSEG_RESSARCIMENTO"].ToString()); 
			--ISNULL(CONVERT(VARCHAR,@RESV_SUBROGATION),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--HTML.Append(ds.Tables[0].Rows[0]["MOEDA_RESSARCIMENTO"].ToString()); 
			--ISNULL(CONVERT(VARCHAR,@MOEDA_RESSARCIMENTO),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			--HTML.Append(ds.Tables[0].Rows[0]["INFLATION_RATE"].ToString());
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'<td></td>'+
			'</tr><tr>'+
			'<td>Salvados</td><td>!</td>'+
			'<td align=right>'+
			--HTML.Append(ds.Tables[0].Rows[0]["TL_SALVADOS"].ToString()); 
			--ISNULL(CONVERT(VARCHAR,@OUT_SALVAGES),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--HTML.Append(ds.Tables[0].Rows[0]["COSSEG_SALVADOS"].ToString()); 
			--ISNULL(CONVERT(VARCHAR,@RESV_SALVAGES),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--HTML.Append(ds.Tables[0].Rows[0]["MOEDA_SALVADOS"].ToString()); 
			--ISNULL(CONVERT(VARCHAR,@MOEDA_SALVADOS),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			 --HTML.Append(ds.Tables[0].Rows[0]["INFLATION_RATE"].ToString()); 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr><tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td align=right><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<tr><td>Total Geral</td><td>!</td>'+
			'<td align=right>'+ 
			--HTML.Append(ds.Tables[0].Rows[0]["TOTAL_GEREAL_LIDER"].ToString()); 
			--ISNULL(CONVERT(VARCHAR,@TOTAL_GEREAL_LIDER),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--HTML.Append(ds.Tables[0].Rows[0]["TOTAL_GERAL_COSSEG"].ToString());
			--ISNULL(CONVERT(VARCHAR,@TOTAL_GERAL_COSSEG),'')+ 
			 '</td><td>!</td>'+
			'<td align=right>'+ 
			--HTML.Append(ds.Tables[0].Rows[0]["TOTAL_GERAL_MOEDA"].ToString()); 
			--ISNULL(CONVERT(VARCHAR,@TOTAL_GERAL_MOEDA),'')+
			'</td><td>!</td>'+
			'<td>F-A-J-</td><td></td>'+
			'<td></td>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'</table>'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Colocar a nossa disposição quantia referente a sua particioação até  0000000<br />'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Sinistro Sorteio já liquidado. A cota parte dessa congenere sera debitada<br />'+
			' &nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; através do L.C.C (Lancto.Contra Corrente).<br />'+
			'<table width=80%><tr><td>Observação:</td><td>C.S </td><td>'+ 
			--HTML.Append(ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString()); 
				ISNULL(@CLAIM_NUMBER,'')+
			'</td><td>N.Carta Aviso</td><td>'+ISNULL(@LETTERSEQUENCENUMBER,'')+'</td><td>O.E.</td><td>05</td></tr></table>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			--HTML.Append(ds.Tables[0].Rows[0]["CITY"].ToString()); 
			ISNULL(@CITY,'')+
			'</td>'+
			'<td>'+ 
			--HTML.Append(ds.Tables[0].Rows[0]["BRANCH_CITY_DETAIL"].ToString());
			ISNULL(@BRANCH_CITY_DETAIL,'')+
			'</td>'+
			'<td></td>'+
			'</tr>'+
			'<tr>'+
			'<td colspan=2><hr width=70% /></td>'+
			'<td><hr width=90% /></td>'+
			'</tr><tr>'+
			'<td>Local e Data</td>'+
			'<td></td>'+
			'<td>Assinatura</td>'+
			'</tr>'+
			'</table>'+
			'</td>'+
			'</tr>'+
			'</table>'+
			'</body>'+
			'</html>'
END
--SELECT @strHTML35 AS LETTER35
IF(@PROCESS_TYPE='CLM_LETTER_26')
    BEGIN
		--DECLARE @strHTML VARCHAR(8000)
		set @strHTML=
			'<html>'+
			'<head>'+
			'<style type=text/css>'+
			'body{font-family:Arial; font-size:12pt; margn-left:0.5cm;margin-top:1.0cm;margin-right:0.5cm;margin-bottom:0.5cm;}'+
			'hr {border-top: 1px dashed #000; margin-left:0px; text-align: left}'+
			'.style1{width:57%;}'+
			'.style2{width:30%;}'+
			'.style3{width:45%;}'+
			'.style4{width:15%;}'+
			'</style>'+
			'</head>'+
			'<body>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			'0000000000000000000000000000000<br />'+
			'À Cosseguradora<br />'+
			'<table  width=100%>'+
			'<tr>'+
			'<td colspan=3><table width=100%><tr><td>'+
			ISNULL(@REIN_COMAPANY_CODE,'')+
			'</td><td>'+CASE WHEN @REIN_COMAPANY_CODE!='' THEN ' - ' ELSE '' END+'</td><td></td><td>' +
			ISNULL(@REIN_COMAPANY_NAME,'')+
			'</td> <td>Sinistro Lider</td><td>'+
			CASE WHEN @OFFCIAL_CLAIM_NUMBER!='' THEN ISNULL(@OFFCIAL_CLAIM_NUMBER,'') ELSE '' END+
			'</td></tr></table></td>'+
			'</tr>'+
			'<tr>'+
			'<td>Ref&nbsp;: </td>'+
			'<td>(&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;Aviso de Sinistro</td>'+
			'<td>'+
			CASE WHEN @ACTION_ON_PAYMENT=180 OR @ACTION_ON_PAYMENT=181 THEN CASE WHEN @LOSS_SALVAGE_SUBROGATION !=0.00  OR @PAYMENT_PROFESSIONAL_SERVICES!=0.00   THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END ELSE '(&nbsp;&nbsp;&nbsp;)' END
			 +'&nbsp;&nbsp;Despesas/Honorários</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(X)&nbsp;&nbsp;Cobrança de Sinistro</td>'+
			'<td>'+CASE WHEN @ACTION_ON_PAYMENT=180 OR @ACTION_ON_PAYMENT=181 THEN CASE WHEN @PAYMENT_SALVAGES!=0.00 OR @PAYMENT_SUBROGATION!=0.00
			       THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END ELSE '(&nbsp;&nbsp;&nbsp;)' END
			 +'&nbsp;&nbsp;&nbsp;Ressarc.Salvados</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>'+CASE WHEN @ACTION_ON_PAYMENT=180 AND @OUT_COVERAGES!=0.00 THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END +'&nbsp;&nbsp;&nbsp;Pagto.Parcial/Adiantamento</td>'+
			'<td>(&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp; Encerramento sem Indenização</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>'+CASE WHEN @ACTION_ON_PAYMENT=181 AND @OUT_COVERAGES!=0.00  THEN '(X)' ELSE '(&nbsp;&nbsp;&nbsp;)' END +'&nbsp;&nbsp;&nbsp;Pagamento Final</td>'+
			'<td>(&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp; Reabertura de Sinistro</td>'+
			'</tr>'+
			'</table>'+
			'<hr width=100% />'+
			'DADOS GERAIS<br />'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr>'+
			'<td class=style1>Nome do Segurado </td><td>!</td>'+
			'<td>Nome Ramo</td><td>!</td>'+
			'<td align=center>% Part. Congenere</td>'+
			'</tr><tr>'+
			'<td class=style1>'+
			--HTML.Append(ds.Tables[0].Rows[0]["CARRIER_NAME"].ToString());
			ISNULL(@CARRIER_NAME,'')+'</td><td>!</td>'+
			'<td>'+
			--                HTML.Append(ds.Tables[0].Rows[0]["NOME_RAMO"].ToString());
			ISNULL(@NOME_RAMO,'')+
			'</td><td>!</td>'+
			'<td align=center>'+
			--HTML.Append(ds.Tables[0].Rows[0]["PART_CONGENERE"].ToString());
			ISNULL(@PART_CONGENERE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr><td class=style4>N.Apolice</td><td>!</td>'+
			'<td> N.Ordem Apolice</td><td>!</td>'+
			'<td> N. Endosso</td><td>!</td>'+
			'<td> N.Ordem Endosso</td><td>!</td>'+
			'<td align=center> Vigência</td></tr>'+
			'<tr><td  class=style4 align=right>'+
			--                HTML.Append(ds.Tables[0].Rows[0]["POLICY_NUMBER"].ToString());
			ISNULL(@POLICY_NUMBER,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			--HTML.Append(ds.Tables[0].Rows[0]["N_ORDERM_APOLICE"].ToString());
			ISNULL(@N_ORDERM_APOLICE,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			--                HTML.Append(ds.Tables[0].Rows[0]["N_ENDOSSO"].ToString());
			ISNULL(@N_ENDOSSO,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@EFFECTIVE_DATETIME,'')+ CASE WHEN @EFFECTIVE_DATETIME!='' THEN  CASE WHEN @EXPIRY_DATE!='' THEN ' - ' ELSE '' END ELSE ''END +

			ISNULL(@EXPIRY_DATE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr><td>Moeda</td><td>!</td>'+
			'<td class=style2>N.Item</td><td>!</td>'+
			'<td align=right>Imp.Segurada</td><td>!</td>'+
			'<td>Sinistro IRB</td><td>!</td>'+
			'<td>Dt.Sinistro</td><td>!</td>'+
			'<td>Dt.Aviso</td>'+
			'</tr><tr>'+
			'<td>R$</td><td>!</td>'+
			'<td class=style2>'+
			--HTML.Append(ds.Tables[0].Rows[0]["N_ITEM"].ToString());
			ISNULL(CONVERT(VARCHAR,@N_ITEM),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			--HTML.Append(ds.Tables[0].Rows[0]["Limit"].ToString());
			ISNULL(CONVERT(VARCHAR,@Limit),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			--                HTML.Append(ds.Tables[0].Rows[0]["SINISTRO_IRB"].ToString());
			ISNULL(CONVERT(VARCHAR,@SINISTRO_IRB),'')+
			'</td><td>!</td>'+
			'<td>'+
			--                HTML.Append(ds.Tables[0].Rows[0]["DT_SINISTRO"].ToString());
			ISNULL(@DT_SINISTRO,'')+
			'</td><td>!</td>'+
			'<td>'+
			--HTML.Append(ds.Tables[0].Rows[0]["DT_AVISO"].ToString());
			ISNULL(@DT_AVISO,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<hr width=99% />'+
			'DADOS ESPECIFICOS INC/LC/RD/R.ENG./ROUBO'+
			'<hr width=99% />'+
			'<table width=100%>'+
			'<tr><td class=style3>Local de Ocorrencia</td><td>!</td>'+
			'<td>Cidade</td><td>!</td>'+
			'<td>UF</td></tr>'+
			'<tr><td class=style3>'+
			--HTML.Append(ds.Tables[0].Rows[0]["LOCAL_DE_OCORRENCIA"].ToString());
			ISNULL(@LOCAL_DE_OCORRENCIA,'')+
			'</td><td>!</td>'+
			'<td>' +
			--HTML.Append(ds.Tables[0].Rows[0]["CIDADE"].ToString()); 
			ISNULL(@CIDADE,'')+
			'</td><td>!</td>'+
			'<td>'+
			--HTML.Append(ds.Tables[0].Rows[0]["UF"].ToString());
			ISNULL(@UF,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99%/>'+
			'<table width=100%>'+
			'<tr><td>N.Planta</td><td>!</td>'+
			'<td>Bens Sinistrados</td><td>!</td>'+
			'<td>Natureza do Evento</td><td>!</td>'+
			'<td>Ramo Atividade</td>'+
			'</tr><tr>'+
			'<td></td><td>!</td>'+
			'<td>' +
			--HTML.Append(ds.Tables[0].Rows[0]["BENS_SINISTRADOS"].ToString()); 
			ISNULL(@BENS_SINISTRADOS,'')+
			'</td><td>!</td>'+
			'<td>' +
			--HTML.Append(ds.Tables[0].Rows[0]["DETAIL_TYPE_DESCRIPTION"].ToString()); 
			ISNULL(@DETAIL_TYPE_DESCRIPTION,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@ACTIVITY_TYPE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99% />'+
			'<table width=99%>'+
			'<tr>'+
			'<td>Participações</td><td>!</td>'+
			'<td>VI. Total Lider</td><td>!</td>'+
			'<td>Part.Cosseg (R$)</td><td>!</td>'+
			'<td>Part .Qt .Moeda</td><td>!</td>'+
			'<td>Dt. Base</td><td>!</td>'+
			'<td >Fat. Index.</td></tr>'+
			'<tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=99% /></td></tr>'+
			'<tr><td>Estimativa</td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CASE WHEN @OUT_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@OUT_COVERAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CASE WHEN @RESV_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_COVERAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CASE WHEN @MOEDA_ESTIMATIVE!=0.00 THEN CONVERT(VARCHAR,@MOEDA_ESTIMATIVE) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>' +
			ISNULL(@INFLATION_RATE,'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Indenização</td><td>!</td>'+
			'<td align=right>'+ISNULL(CASE WHEN @PAYMENT_COVERAGE!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_COVERAGE) ELSE '' END,'')+'</td><td>!</td>'+
			'<td align=right>'+ISNULL(CASE WHEN @RESV_PAYMENT_COVERAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_PAYMENT_COVERAGES) ELSE '' END,'')+'</td><td>!</td>'+
			'<td align=right>'+ISNULL(CASE WHEN @MOEDA_PAYMENT_ESTIMATIVE!=0.00 THEN CONVERT(VARCHAR,@MOEDA_PAYMENT_ESTIMATIVE) ELSE '' END,'')+'</td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td align=right>'+ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+'</td>'+
			'</tr><tr>'+
			'<td>Honorários</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @PAYMENT_PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_PROFESSIONAL_SERVICES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			 ISNULL(CASE WHEN @RESV_PROFESSIONAL_SERVICES!=0.00 THEN CONVERT(VARCHAR,@RESV_PROFESSIONAL_SERVICES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CASE WHEN @MOEDA_HONOR!=0.00 THEN CONVERT(VARCHAR,@MOEDA_HONOR) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			ISNULL(@INFLATION_RATE,'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Despesas</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @PAYMENT_SALVAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_SALVAGE_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CASE WHEN @RESV_LOSS_SALAGE_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_LOSS_SALAGE_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CASE WHEN @MOEDA_DESPESAS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_DESPESAS) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			ISNULL(@INFLATION_RATE,'')+
			'</td>'+
			'</tr><tr>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'<tr><td>Ressarcimento</td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CASE WHEN @PAYMENT_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @RESV_SUBROGATION!=0.00 THEN CONVERT(VARCHAR,@RESV_SUBROGATION) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CASE WHEN @MOEDA_RESSARCIMENTO!=0.00 THEN CONVERT(VARCHAR,@MOEDA_RESSARCIMENTO) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>' +
			--ISNULL(@INFLATION_RATE,'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Salvados</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @PAYMENT_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@PAYMENT_SALVAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @RESV_SALVAGES!=0.00 THEN CONVERT(VARCHAR,@RESV_SALVAGES) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CASE WHEN @MOEDA_SALVADOS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_SALVADOS) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr><tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td align=right><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<tr><td>Total Geral</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @TOTAL_PAYMENT_GEREAL_LIDER!=0.00 THEN CONVERT(VARCHAR,@TOTAL_PAYMENT_GEREAL_LIDER) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td align=right>' +
			ISNULL(CASE WHEN @TOTAL_PAYMENT_GERAL_COSSEG!=0.00 THEN CONVERT(VARCHAR,@TOTAL_PAYMENT_GERAL_COSSEG) ELSE '' END,'')+ 
			'</td><td>!</td>'+
			'<td align=right>'+
			ISNULL(CASE WHEN @TOTAL_PAYMENT_GERAL_MOEDA!=0.00 THEN CONVERT(VARCHAR,@TOTAL_PAYMENT_GERAL_MOEDA) ELSE '' END,'')+
			'</td><td>!</td>'+
			'<td>F-A-J-</td><td></td>'+
			'<td></td>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'</table>'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Colocar a nossa disposição quantia referente a sua participação até  '+@LETTER_GENERATION_DATE+'<br />'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Sinistro Sorteio já liquidado.A cota parte dessa congenere sera debitada<br />'+
			' &nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; através do L.C.C.(Lancto.Conta Corrente).<br />'+
			'<table width=60%><tr><td>Observação:</td><td>C.S </td><td>'+
			ISNULL(@CLAIM_NUMBER,'')+
			'</td><td>N.Carta Aviso</td><td>'+ISNULL(@LETTERSEQUENCENUMBER,'')
			--CASE WHEN @PROCESS_TYPE='CLM_LETTER_21' THEN ISNULL(@LETTER_SEQUENCE_NO1,'')
			-- ELSE 
			--   CASE WHEN @PROCESS_TYPE='CLM_LETTER_26' THEN ISNULL(@LETTER_SEQUENCE_NO,'') 
			--      ELSE 'vbn' 
			--    END  
			-- END 
			+'</td><td>O.E.</td><td>'+
			ISNULL(@BRANCH_CODE,'')+
			'</td></tr></table>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>' 	+	ISNULL(@CITY,'')+ '</td>'+
			'<td>'+
			ISNULL(@BRANCH_CITY_DETAIL,'')+
			'</td>'+
			'<td></td>'+
			'</tr>'+
			'<tr>'+
			'<td colspan=2><hr width=70% /></td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<td>Local e Data</td>'+
			'<td></td>'+
			'<td>Assinatura</td>'+
			'</tr>'+
			'</table>'+
			'</td>'+
			'</tr>'+
			'</table>'+
			'</body>'+
			'</html>'
END
--SELECT @strHTML26 AS LETTER26
IF(@PROCESS_TYPE='CLM_LETTER_36')
    BEGIN
		--DECLARE @strHTML VARCHAR(8000)
		SET @strHTML =   
			'<html>'+
			'<head>'+
			'<style type=text/css>'+
			'body{font-family:Arial; font-size:12px; margn-left:0.5cm;margin-top:1.0cm;margin-right:0.5cm;margin-bottom:0.5cm;}'+
			'hr {border-top: 1px dashed #000; margin-left:0px; text-align: left}'+
			'.style1{width:59.5%;}'+
			'.style2{width:35%;}'+
			'.style3{width:32%;}'+
			'.style4{width:20%;}'+
			'</style>'+
			'</head>'+
			'<body>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			'0000000000000000000000000000000<br />'+
			'À Cosseguradora<br />'+
			'<table  width=100%>'+
			'<tr>'+
			'<td colspan=3><table width=100%><tr><td>'+ 
			ISNULL(@REIN_COMAPANY_CODE,'')+
			'</td><td>'+CASE WHEN @REIN_COMAPANY_CODE!='' THEN ' - ' ELSE '' END+'</td><td></td><td>' +
			ISNULL(@REIN_COMAPANY_NAME,'')+
			'</td> <td>Sinistro Lider</td><td>' +
			CASE WHEN @OFFCIAL_CLAIM_NUMBER!='' THEN ISNULL(@OFFCIAL_CLAIM_NUMBER,'') ELSE '' END+
			'</td></tr></table></td>'+
			'</tr>'+
			'<tr>'+
			'<td>Ref: </td>'+
			'<td>(X)&nbsp;&nbsp;Aviso de Sinistro</td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp;Despesas/Honorários</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp;Cobrança de Sinistro</td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp;Ressarc.Salvados</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp; Pagto. parcial/Adiantamento</td>'+
			'<td>(X)&nbsp;&nbsp;Encerramento sem Indenização</td>'+
			'</tr>'+
			'<tr>'+
			'<td></td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp; Pagamento Final</td>'+
			'<td>(&nbsp;&nbsp; )&nbsp;&nbsp; Reabertura de Sinistro</td>'+
			'</tr>'+
			'</table>'+
			'<hr width=100% />'+
			'DADOS GERAIS<br />'+
			'<hr width=100% />'+
			'<table width=100%>'+
			'<tr>'+
			'<td class=style1>Nome do Segurado </td><td>!</td>'+
			'<td>Nome Ramo</td><td>!</td>'+
			'<td align=center>% Part. Congenere</td>'+
			'</tr><tr>'+
			'<td class=style1>'+
			ISNULL(@CARRIER_NAME,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@NOME_RAMO,'')+
			'</td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@PART_CONGENERE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+    
			'<table width=100%>'+
			'<tr><td  class=style4>N.Apolice</td><td>!</td>'+
			'<td align=center>N.Ordem Apolice</td><td>!</td>'+
			'<td align=center>N. Endosso</td><td>!</td>'+
			'<td align=center>N.Ordem Endosso</td><td>!</td>'+
			'<td align=center>Vigência</td></tr>'+
			'<tr><td  class=style4>'+ 
			ISNULL(@POLICY_NUMBER,'')+
			'</td><td>!</td>'+
			'<td align=center>'+ 
			ISNULL(@N_ORDERM_APOLICE,'')+
			'</td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@N_ENDOSSO,'')+
			'</td><td>!</td>'+
			'<td align=center></td><td>!</td>'+
			'<td align=center>'+
			ISNULL(@EFFECTIVE_DATETIME,'')+ CASE WHEN @EFFECTIVE_DATETIME!='' THEN  CASE WHEN @EXPIRY_DATE!='' THEN ' - ' ELSE '' END ELSE ''END +            
			ISNULL(@EXPIRY_DATE,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<table width=100% cellspacing=5>'+
			'<tr><td>Moeda</td><td>!</td>'+
			'<td class=style2>N.Item</td><td>!</td>'+
			'<td align=right>Imp.Segurada</td><td>!</td>'+
			'<td align=right>Sinistro IRB</td><td>!</td>'+
			'<td>Dt.Sinistro</td><td>!</td>'+
			'<td>Dt.Aviso</td>'+
			'</tr><tr>'+
			'<td>R$</td><td>!</td>'+
			'<td class=style2>'+ 
			 ISNULL(CONVERT(VARCHAR,@N_ITEM_MERATIME),'')+
			 '</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@Limit),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@SINISTRO_IRB),'')+
			 '</td><td>!</td>'+
			'<td>'+
			 ISNULL(@DT_SINISTRO,'')+
			  '</td><td>!</td>'+
			'<td>'+
			ISNULL(@DT_AVISO,'')+
			'</td></tr>'+
			'</table>'+
			'<hr width=100% />'+
			'<hr width=99% />'+
			'DADOS ESPECIFICOS <br/><div>TRANSP/CASCO/AERONAUTICO/P.RURAL/CRED.EXP./R.C.GERAL/CRED.INT.</div>'+
			'<hr width=99% />'+
			'<table width=99%><tr>'+
			'<td>N.Averbacao Certif.</td><td>!</td>'+
			'<td>Meio Ttransporte</td><td>!</td>'+
			'<td>N.Placa</td><td>!</td>'+
			'<td>PrefiXo</td><td>!</td>'+
			'<td>Nome de Embrcação</td></tr>'+
			'<tr><td>'+ 
			ISNULL(@N_AVERBACAO_CERTIF,'')+
			 '</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@MEIO_TTRANSPORTE,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@N_PLACA,'')+
			'</td><td>!</td>'+
			'<td>'+
			 ISNULL(@PREFIXO,'')+
			 '</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@NOME_DA_EMBARCACAO,'')+
			'</td></tr>'+
			'</table>'+
			'<hr WIDTH=99%/>'+
			'<table width=99%>'+
			'<tr><td>Empresa Transportadora</td><td>!</td>'+
			'<td class=style3>Mercadoria Sinisrtrada</td><td>!</td>'+
			'<td>Natureza Danos</td><td>!</td>'+
			'<td>Tipo Cobertura</td></tr>'+
			'<tr><td>'+
			ISNULL(@EMPRESA_TRANS,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@MERCADORIA_SINISTRADA,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DETAIL_TYPE_DESCRIPTION,'')+
			'</td><td>!</td>'+
			'<td>RC OPERADOR P</td></tr>'+
			'</table>'+
			'<hr WIDTH=99%/>'+
			'<table width=99%>'+
			'<tr><td>Origem</td><td>!</td>'+
			'<td>Destino</td><td>!</td>'+
			'<td colspan=3>Local de Ocorrencia</td><td>!</td>'+
			'<td>Cidade</td><td>!</td>'+
			'<td>UF</td></tr>'+
			'<tr><td>'+
			ISNULL(@ORIGEM,'')+
			'</td><td>!</td>'+
			'<td>'+
			ISNULL(@DESTINO,'')+
			'</td><td>!</td>'+
			'<td colspan=3>'+ 
			ISNULL(@LOCAL_DE_OCORRENCIA,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@CIDADE,'')+
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@UF,'')+
			'</td></tr>'+
			'<tr><td>Data Saida</td><td>!</td>'+
			'<td>Data Chegada</td><td>!</td>'+
			'<td>Data Vistoria</td><td>!</td>'+
			'<td>Local de Vistoria</td></tr>'+
			'<tr><td>'+ 
			ISNULL(@DATA_SAIDA,'')+ 
			'</td><td>!</td>'+
			'<td>'+ 
			ISNULL(@DATA_CHEGADA,'')+
			'</td><td>!</td>'+
			'<td>'+
			 ISNULL(@DATA_VISTORIA,'')+
			'</td><td>!</td>'+
			'<td>ESTRADA DA ILHA DA MADEIRA S/N</td></tr>'+
			'</table>'+
			'<hr WIDTH=99% />'+
			'<table width=99%>'+
			'<tr>'+
			'<td>Participações</td><td>!</td>'+
			'<td>VI. Total Lider</td><td>!</td>'+
			'<td>Part.Cosseg (R$)</td><td>!</td>'+
			'<td>Part .Qt .Moeda</td><td>!</td>'+
			'<td>Dt. Base</td><td>!</td>'+
			'<td>Fat. Index.</td></tr>'+
			'<tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td></tr>'+
			'<tr><td>Estimativa</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@OUT_COVERAGES),'')+
			'</td><td>!</td>'+
			'<td  align=right>'+
			-- ISNULL(CONVERT(VARCHAR,@RESV_COVERAGES),'')+
			  '</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@MOEDA_ESTIMATIVE),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Indenização</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td><td>!</td>'+
			'<td></td>'+
			'</tr><tr>'+
			'<td>Honorários</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@PROFESSIONAL_SERVICES),'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			--ISNULL(CONVERT(VARCHAR,@RESV_PROFESSIONAL_SERVICES),'')+
			'</td><td>!</td>' +
			'<td align=right>' + 
			--ISNULL(CONVERT(VARCHAR,@MOEDA_HONOR),'')+
			'</td><td>!</td>' +
			'<td align=right></td><td>!</td>' +
			'<td align=right>' + 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'<td>Despesas</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@LOSS_SALVAGE_SUBROGATION),'')+
			 '</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@RESV_LOSS_SALAGE_SUBROGATION),'')+
			'</td><td>!</td>'+
			'<td align=right>'+
			 --ISNULL(CASE WHEN @MOEDA_DESPESAS!=0.00 THEN CONVERT(VARCHAR,@MOEDA_DESPESAS) ELSE '' END,'')+
			  '</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
		    ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'<tr><td>Ressarcimento</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@OUT_SUBROGATION),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@RESV_SUBROGATION),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@MOEDA_RESSARCIMENTO),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+ 
			ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'<td></td>'+
			'</tr><tr>'+
			'<td>Salvados</td><td>!</td>'+
			'<td align=right>'+
			--ISNULL(CONVERT(VARCHAR,@OUT_SALVAGES),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@RESV_SALVAGES),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@MOEDA_SALVADOS),'')+
			'</td><td>!</td>'+
			'<td align=right></td><td>!</td>'+
			'<td align=right>'+
			 ISNULL(CONVERT(VARCHAR,@INFLATION_RATE),'')+
			'</td>'+
			'</tr><tr>'+
			'</tr><tr>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td align=right><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td><td>!</td>'+
			'<td><hr width=100% /></td>'+
			'</tr><tr>'+
			'<tr><td>Total Geral</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@TOTAL_GEREAL_LIDER),'')+
			'</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@TOTAL_GERAL_COSSEG),'')+ 
			 '</td><td>!</td>'+
			'<td align=right>'+ 
			--ISNULL(CONVERT(VARCHAR,@TOTAL_GERAL_MOEDA),'')+
			'</td><td>!</td>'+
			'<td>F-A-J-</td><td></td>'+
			'<td></td>'+
			'</tr>'+
			'<tr><td colspan=11><hr width=100% /></td></tr>'+
			'</table>'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Colocar a nossa disposição quantia referente a sua particioação até  0000000<br />'+
			'(&nbsp;&nbsp&nbsp;&nbsp;)&nbsp;&nbsp;Sinistro Sorteio já liquidado. A cota parte dessa congenere sera debitada<br />'+
			' &nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; através do L.C.C (Lancto.Contra Corrente).<br />'+
			'<table width=60%><tr><td>Observação:</td><td>C.S </td><td>'+ 
			ISNULL(@CLAIM_NUMBER,'')+
			'</td><td>N.Carta Aviso</td><td>'+ISNULL(@LETTERSEQUENCENUMBER,'')+'</td><td>O.E.</td><td>'+@BRANCH_CODE+'</td></tr></table>'+
			'<table width=100%>'+
			'<tr>'+
			'<td>'+
			ISNULL(@CITY,'')+
			'</td>'+
			'<td>'+ 
			ISNULL(@BRANCH_CITY_DETAIL,'')+
			'</td>'+
			'<td></td>'+
			'</tr>'+
			'<tr>'+
			'<td colspan=2><hr width=70% /></td>'+
			'<td><hr width=90% /></td>'+
			'</tr><tr>'+
			'<td>Local e Data</td>'+
			'<td></td>'+
			'<td>Assinatura</td>'+
			'</tr>'+
			'</table>'+
			'</td>'+
			'</tr>'+
			'</table>'+
			'</body>'+
			'</html>'
END
--SELECT @strHTML36 AS LETTER36
SELECT @strHTML AS LETTER

----------------------TO FIND ACTIVITY ID-----------------------------

IF EXISTS( SELECT * FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND ACTION_ON_PAYMENT=168 OR ACTION_ON_PAYMENT=165)
BEGIN
	SELECT (ACTIVITY_ID) AS ACTIVITY_ID FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND ACTION_ON_PAYMENT IN(165,168) order by ACTIVITY_ID desc
END
ELSE
BEGIN
	SELECT ACTIVITY_ID=0
END	

 GO


EXEC [Proc_Ceded_COIFNOL_PAYMENTS] 1123,6,'CLM_LETTER_31',2

