IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AgencyCommission_1099]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AgencyCommission_1099]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--	drop proc dbo.Proc_AgencyCommission_1099 
--go 

/* [Proc_AgencyCommission_1099] 2008,600
Calculate total of commission given to agency, this will be the sum of Regular Commission DB, 
Regular Commission AB,  Additional Commission DB and Additional Commission AB. 
Over payment type checks will not to be considered as payment.
For all checks (CHECK_TYPE agency commission check and COMM_TYPE REG and ADC)
 issued against agency sum up DB Commission and AB Commission from check reconciliation table.
 For all deposits from this agency calculate AB Commission and DB commission from deposit reconciliation table,
 add this amount to total off commissions from check.
 If this sum is greater than $600 and agency is set up as Other for
 1099 create a record for the agency in 1099 table. 
Check and deposit details have to be entered in detail table for 1099.
In 1099 table amount will go in Nonemployee Compensation (field 7).

DATE : 07 JAN 2009 
DESC: INCLUDE EFT RECORDS*/
--Proc_AgencyCommission_1099 2008,200
--drop proc dbo.Proc_AgencyCommission_1099 

CREATE PROC [dbo].[Proc_AgencyCommission_1099] 
(
	@YEAR INT,
	@SYS_1099_AMOUNT decimal(18,2)
)
as


DECLARE @EFT INT			SET @EFT = 11788
DECLARE @EFT_SWEEP INT		SET @EFT_SWEEP= 11976

DECLARE @OTHER_SSN		 Int		SET @OTHER_SSN =	114245
DECLARE @OTHER_FEDRAL_ID Int		SET @OTHER_FEDRAL_ID = 11735
DECLARE @AGENCY_TYPE_CHECK INT		SET @AGENCY_TYPE_CHECK = 2472    

------------------------------------------------AGENCY START----------------------------------------

CREATE TABLE #INCLUDED_AGENCIES
(
	[IDENT_COL] [INT]		IDENTITY(1,1) NOT NULL , 
	ENTITY_ID				Int, --payee
	REFERENCE_ID			INT, --check id
	COMMISSION_PAID			DECIMAL(18,2),
	REF_CODE				NVARCHAR(20)
	
)	


INSERT INTO #INCLUDED_AGENCIES
SELECT MNT.AGENCY_ID AS AGENCY_ID, CHK.CHECK_ID,
(ISNULL(DIST.AMT_COMMISSION_PAYABLE_AB,0) + ISNULL(DIST.AMT_COMMISSION_PAYABLE_DB,0) ) *- 1 AS COMMISSION_AMOUNT
,'CHK'
FROM ACT_CHECK_INFORMATION CHK 
INNER JOIN ACT_AGENCY_CHECK_DISTRIBUTION DIST 
	ON CHK.CHECK_ID = DIST.CHECK_ID 
INNER JOIN MNT_AGENCY_LIST MNT 
	ON MNT.AGENCY_ID = CHK.PAYEE_ENTITY_ID
WHERE CHK.COMM_TYPE IN ('REG','ADC')
AND YEAR(CHK.DATE_COMMITTED) = @YEAR 
AND CHK.CHECK_TYPE = @AGENCY_TYPE_CHECK
AND ISNULL(CHK.IS_PRINTED,'N') = 'Y' 
AND ISNULL(CHK.IS_COMMITED,'N') = 'Y' 
AND ISNULL(CHK.IS_1099_PROCESSED,'N') <> 'Y'
AND ISNULL(CHK.PAYMENT_MODE,0) NOT IN (@EFT,@EFT_SWEEP)
AND ISNULL(CHK.GL_UPDATE,0) <> 2 
AND ISNULL(MNT.PROCESS_1099,0) IN (@OTHER_SSN,@OTHER_FEDRAL_ID) 

UNION

SELECT MNT.AGENCY_ID AS AGENCY_ID,   CHK.CHECK_ID,
(ISNULL(DIST.AMT_COMMISSION_PAYABLE_AB,0) + ISNULL(DIST.AMT_COMMISSION_PAYABLE_DB,0) )*- 1  AS COMMISSION_AMOUNT,
'CHK'
FROM ACT_CHECK_INFORMATION CHK 
INNER JOIN ACT_AGENCY_CHECK_DISTRIBUTION DIST 
	ON CHK.CHECK_ID = DIST.CHECK_ID 
INNER JOIN MNT_AGENCY_LIST MNT 
	ON MNT.AGENCY_ID = CHK.PAYEE_ENTITY_ID
WHERE CHK.COMM_TYPE IN ('REG','ADC')
AND YEAR(CHK.DATE_COMMITTED) = @YEAR 
AND CHK.CHECK_TYPE = @AGENCY_TYPE_CHECK
AND ISNULL(CHK.IS_COMMITED,'N') = 'Y' 
AND ISNULL(CHK.IS_1099_PROCESSED,'N') <> 'Y'
AND ISNULL(CHK.PAYMENT_MODE,0) IN (@EFT,@EFT_SWEEP)
AND ISNULL(CHK.GL_UPDATE,0) <> 2 
AND ISNULL(MNT.PROCESS_1099,0) IN (@OTHER_SSN,@OTHER_FEDRAL_ID) 


---deposit 

UNION 

SELECT MNT.AGENCY_ID AS AGENCY_ID,DEP.DEPOSIT_ID,
( 
	SELECT SUM(ISNULL(AGN.TOTAL_PAID,0))
	FROM ACT_AGENCY_STATEMENT AGN 
	INNER JOIN ACT_AGENCY_RECON_GROUP_DETAILS GD 
		ON AGN.ROW_ID = GD.ITEM_REFERENCE_ID 
	INNER JOIN ACT_RECONCILIATION_GROUPS GRP 
		ON GD.GROUP_ID = GRP.GROUP_ID 
	WHERE ISNULL(COMM_TYPE,'') IN ('REG','ADC')
	AND TRAN_TYPE = 'COM'
	AND GD.GROUP_ID = GDIN.GROUP_ID
) * -1  AS COMMISSION_PAID ,'DEP'
FROM ACT_AGENCY_STATEMENT AGNIN 
INNER JOIN ACT_AGENCY_RECON_GROUP_DETAILS GDIN 
	ON AGNIN.ROW_ID = GDIN.ITEM_REFERENCE_ID 
INNER JOIN ACT_RECONCILIATION_GROUPS GRPIN 
	ON GDIN.GROUP_ID = GRPIN.GROUP_ID 
INNER JOIN ACT_CURRENT_DEPOSIT_LINE_ITEMS DEP_DET 
	ON DEP_DET.CD_LINE_ITEM_ID = AGNIN.SOURCE_ROW_ID 
INNER JOIN ACT_CURRENT_DEPOSITS DEP 
	ON DEP.DEPOSIT_ID = DEP_DET.DEPOSIT_ID
INNER JOIN MNT_AGENCY_LIST MNT 
	ON MNT.AGENCY_ID = AGNIN.AGENCY_ID
WHERE AGNIN.UPDATED_FROM = 'D' 
AND AGNIN.TRAN_TYPE='DEP'
AND YEAR(DEP.DATE_COMMITED) = @YEAR 
AND ISNULL(DEP.IS_COMMITED,'N') = 'Y' 
AND ISNULL(DEP_DET.IS_1099_PROCESSED,'N') <> 'Y'
AND ISNULL(MNT.PROCESS_1099,0) IN (@OTHER_SSN,@OTHER_FEDRAL_ID) 
     
  

   
--DISTINCT AGENCIES
  
CREATE TABLE #DISTINCT_INCLUDED_AGENCIES
(
	[IDENT_COL] [INT]		IDENTITY(1,1) NOT NULL ,   
	ENTITY_ID				INT,
	COMMISSION_PAID			DECIMAL(18,2)
)	


INSERT #DISTINCT_INCLUDED_AGENCIES

SELECT ENTITY_ID,SUM(ISNULL(EXC.COMMISSION_PAID,0) )
FROM #INCLUDED_AGENCIES  EXC
GROUP BY EXC.ENTITY_ID
HAVING SUM(ISNULL(EXC.COMMISSION_PAID,0)) >= @SYS_1099_AMOUNT
ORDER BY ENTITY_ID


--SELECT * FROM #INCLUDED_AGENCIES ORDER BY ENTITY_ID 
--SELECT * FROM #DISTINCT_INCLUDED_AGENCIES 



-----Varibales         
    
DECLARE @ENTITY_TYPE VARCHAR(10) --
SET @ENTITY_TYPE = 'AGN'   --To be AGN
DECLARE @RECIPIENT_IDENTIFICATION NVARCHAR(200)        
DECLARE @NON_EMPLOYEMENT_COMPENSATION DECIMAL(18,2)     
DECLARE @RECIPIENT_ENTITY_NAME NVARCHAR(70)        
DECLARE @RECIPIENT_ADD1 NVARCHAR(70)        
DECLARE @RECIPIENT_ADD2 NVARCHAR(70)        
DECLARE @RECIPIENT_CITY NVARCHAR(40)        
DECLARE @RECIPIENT_STATE NVARCHAR(30)        
DECLARE @RECIPIENT_ZIP NVARCHAR(120)        
DECLARE @FORM_1099_ID INT        
DECLARE @FED_SSN_1099 VARCHAR(1)
DECLARE @ENTITY_NAME VARCHAR(200) 
DECLARE @FIELD_1 NVARCHAR(70)   
DECLARE @FIELD_2 NVARCHAR(70)
DECLARE @AGENCY_CODE VARCHAR(30) 
DECLARE @PAYORS_CARRIER_CODE VARCHAR(12)
SET @PAYORS_CARRIER_CODE = 'W001'
DECLARE @CREATED_DATETIME DATETIME
SET @CREATED_DATETIME = GETDATE()
DECLARE @ASSIGN_AGENCY_CODE VARCHAR(40)

DECLARE @PROCESSED_BY INT
SET @PROCESSED_BY = 3 --TO BE CHANGED

DECLARE @IDENT_COL INT
SET @IDENT_COL = 1
DECLARE @ENTITY_ID INT

WHILE 1 = 1                        
BEGIN                        
	IF NOT EXISTS (SELECT IDENT_COL FROM #DISTINCT_INCLUDED_AGENCIES WITH(NOLOCK) WHERE IDENT_COL = @IDENT_COL)                        
	BEGIN                        
		BREAK                        
	END    
	SELECT @ENTITY_ID = ENTITY_ID FROM #DISTINCT_INCLUDED_AGENCIES WHERE IDENT_COL = @IDENT_COL
   
	--UPDATE CHECK TABLE FLAG --checks
	UPDATE ACT_CHECK_INFORMATION SET IS_1099_PROCESSED = 'Y'         
	WHERE CHECK_ID IN ( SELECT REFERENCE_ID FROM #INCLUDED_AGENCIES WHERE ENTITY_ID = @ENTITY_ID
	AND REF_CODE = 'CHK' ) 
	AND CHECK_TYPE = @AGENCY_TYPE_CHECK AND COMM_TYPE IN ('REG', 'ADC')

	--UPDATE DEPOSIT
	UPDATE ACT_CURRENT_DEPOSIT_LINE_ITEMS SET IS_1099_PROCESSED = 'Y'         
	WHERE CD_LINE_ITEM_ID IN ( SELECT REFERENCE_ID FROM #INCLUDED_AGENCIES WHERE ENTITY_ID = @ENTITY_ID   
	AND REF_CODE='DEP')  
	AND DEPOSIT_TYPE = 'AGN'

	--MOve to Non Employee Compensation
	SELECT @NON_EMPLOYEMENT_COMPENSATION = SUM(COMMISSION_PAID) FROM #INCLUDED_AGENCIES WHERE [ENTITY_ID] = @ENTITY_ID

	--SSN
	SELECT @RECIPIENT_IDENTIFICATION = ISNULL(FEDERAL_ID,0) FROM MNT_AGENCY_LIST WHERE AGENCY_ID = @ENTITY_ID  

	--AGENCY ADDRESS INFO
	SELECT      
		@ENTITY_ID			= AGENCY_ID,         
		@FIELD_1			= ISNULL(AGENCY_DISPLAY_NAME,'') ,     	
		@RECIPIENT_ADD1		= ISNULL(AGENCY_ADD1,''),        
		@RECIPIENT_ADD2		= ISNULL(AGENCY_ADD2,''),        
		@RECIPIENT_CITY		= ISNULL(AGENCY_CITY,''),        
		@RECIPIENT_STATE	= ISNULL(AGENCY_STATE,''),
		@RECIPIENT_ZIP		= ISNULL(AGENCY_ZIP,''),
		@FED_SSN_1099		= ISNULL(FED_SSN_1099,''),
		@AGENCY_CODE		= RTRIM(LTRIM(ISNULL(AGENCY_CODE,'')))      
		FROM MNT_AGENCY_LIST WITH(NOLOCK) WHERE AGENCY_ID = @ENTITY_ID 		


		SET @ASSIGN_AGENCY_CODE = @AGENCY_CODE + 'A'
		--INSERT IN FORM 1099
		EXECUTE Proc_Insert_FORM_1099         
		@FORM_1099_ID OUT, 
		@ENTITY_ID    =  @ENTITY_ID,        
		@ENTITY_TYPE    = @ENTITY_TYPE,        
		@PAYORS_CARRIER_CODE  = @PAYORS_CARRIER_CODE,        
		@RECIPIENT_NAME   =  @FIELD_1,
		@RECIPIENT_STREET_ADDRESS1  =  @RECIPIENT_ADD1 ,        
		@RECIPIENT_STREET_ADDRESS2  =  @RECIPIENT_ADD2 ,        
		@RECIPIENT_CITY   =  @RECIPIENT_CITY,        
		@RECIPIENT_STATE   = @RECIPIENT_STATE,        
		@RECIPIENT_ZIP    =  @RECIPIENT_ZIP,        
		@RECIPIENT_IDENTIFICATION  =  @RECIPIENT_IDENTIFICATION,        
		@NON_EMPLOYEMENT_COMPENSATION = @NON_EMPLOYEMENT_COMPENSATION,
		@ACCOUNT_NO     =  @ASSIGN_AGENCY_CODE,        
		@CREATED_BY     =  @PROCESSED_BY,        
		@CREATED_DATETIME   =  @CREATED_DATETIME,        
		@MODIFIED_BY     =  NULL,        
		@LAST_UPDATED_DATETIME   =  NULL,        
		@INSERTUPDATE    =  'I', 
		@FED_SSN_1099    = @FED_SSN_1099,
		@RECIPIENT_NAME_1 = @FIELD_2,
		@YEAR_1099 = @YEAR


	--INSERT CHECK LINEITEMS       
	INSERT INTO CHECK_DETAILS_1099        
	SELECT @FORM_1099_ID ,[REFERENCE_ID] , GETDATE() ,        
	CASE WHEN REF_CODE = 'DEP' THEN 'D' ELSE 'C' END        
	FROM #INCLUDED_AGENCIES WHERE ENTITY_ID = @ENTITY_ID    

	SET @IDENT_COL = @IDENT_COL + 1    		

END

DROP TABLE #INCLUDED_AGENCIES
DROP TABLE #DISTINCT_INCLUDED_AGENCIES



--GO      
--
--EXEC [Proc_AgencyCommission_1099] 2008,600
--
--select * from FORM_1099
--SELECT * FROM CHECK_DETAILS_1099 ORDER BY REF_ID
--
--rollback





GO

