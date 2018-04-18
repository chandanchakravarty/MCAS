    
-- =============================================    
-- Author:  Amit Kr. Mishra    
-- Create date: 1st November,2011    
-- Description: -    
-- ============================================= 
-- MODIFIED BY: Ruchika Chauhan
-- MODIFIED DATE: 3rd February,2012    
-- REASON: Two columns namely Effective and End dates have been added and the proc modified accordingly as per TFS # 3322. 
-- =============================================   
-- DROP PROC INSERT_MNT_UNDERWRITING_CLAIM_LIMITS   
    
    
CREATE PROCEDURE dbo.INSERT_MNT_UNDERWRITING_CLAIM_LIMITS    
    @ASSIGN_ID INT OUTPUT,    
    @USER_ID int=929,    
    @COUNTRY_ID int=7,    
    @LOB_ID int=39,    
    @PML_LIMIT decimal(10,2)=10.00,    
    @PREMIUM_APPROVAL_LIMIT decimal(10,2)=10.00,    
    @CLAIM_RESERVE_LIMIT decimal(10,2)=10.00,    
    @CLAIM_REOPEN int,    
    @CLAIM_SETTLMENT_LIMIT decimal(10,2)=10.00,    
    @CREATED_BY int=90,    
    @CREATED_DATETIME datetime='12/12/2011'    ,
    
    --Added by Ruchika Chauhan on 3-Feb-2012 for TFS Bug # 3322    
    @EFFECTIVEDATE datetime,
    @ENDDATE datetime
AS    
    
BEGIN    
   
SELECT @ASSIGN_ID= ISNULL(MAX(ASSIGN_ID),0)+1 FROM MNT_UNDERWRITING_CLAIM_LIMITS   

----------------------------------------------------------------------------------
DECLARE @LAST_END_DATE DATETIME,
@LAST_EFFECTIVE_DATE DATETIME,
@DAY_DIFF INT,
@LAST_ASSIGN_ID INT,
@MODIFIED_LAST_END_DATE DATETIME


SELECT @LAST_ASSIGN_ID = MAX(ASSIGN_ID) FROM MNT_UNDERWRITING_CLAIM_LIMITS WHERE LOB_ID = @LOB_ID
SELECT @LAST_EFFECTIVE_DATE = EffectiveDate FROM MNT_UNDERWRITING_CLAIM_LIMITS WHERE ASSIGN_ID = @LAST_ASSIGN_ID
SELECT @LAST_END_DATE = EndDate FROM MNT_UNDERWRITING_CLAIM_LIMITS WHERE ASSIGN_ID = @LAST_ASSIGN_ID

SET @DAY_DIFF = DATEDIFF(D, @LAST_END_DATE,@EFFECTIVEDATE);
SET @MODIFIED_LAST_END_DATE = @EFFECTIVEDATE - 1;

IF(@DAY_DIFF =0)
BEGIN
	IF(@MODIFIED_LAST_END_DATE > @LAST_EFFECTIVE_DATE)
	BEGIN
		UPDATE MNT_UNDERWRITING_CLAIM_LIMITS SET EndDate = @EFFECTIVEDATE -1 WHERE ASSIGN_ID = @LAST_ASSIGN_ID
	
		INSERT INTO MNT_UNDERWRITING_CLAIM_LIMITS    
		(    
		ASSIGN_ID,    
		USER_ID,    
		COUNTRY_ID,    
		LOB_ID,    
		PML_LIMIT,    
		PREMIUM_APPROVAL_LIMIT,    
		CLAIM_RESERVE_LIMIT,    
		CLAIM_REOPEN,    
		CLAIM_SETTLMENT_LIMIT,    
		CREATED_BY,    
		CREATED_DATETIME,
	    
		--Added for TFS 3322
		EffectiveDate,
		EndDate  
	   )    
	    
		VALUES     
		(    
		@ASSIGN_ID,    
		@USER_ID,    
		@COUNTRY_ID,    
		@LOB_ID,    
		@PML_LIMIT,    
		@PREMIUM_APPROVAL_LIMIT,    
		@CLAIM_RESERVE_LIMIT,    
		@CLAIM_REOPEN,    
		@CLAIM_SETTLMENT_LIMIT,    
		@CREATED_BY,    
		@CREATED_DATETIME,

		--Added for TFS 3322
		@EFFECTIVEDATE,
		@ENDDATE   
		)    
		RETURN @Assign_id 
	END
	ELSE
		RETURN 0
END
ELSE IF(@DAY_DIFF > 0)
BEGIN
	IF(@DAY_DIFF = 1)
	BEGIN
		INSERT INTO MNT_UNDERWRITING_CLAIM_LIMITS    
		(    
		ASSIGN_ID,    
		USER_ID,    
		COUNTRY_ID,    
		LOB_ID,    
		PML_LIMIT,    
		PREMIUM_APPROVAL_LIMIT,    
		CLAIM_RESERVE_LIMIT,    
		CLAIM_REOPEN,    
		CLAIM_SETTLMENT_LIMIT,    
		CREATED_BY,    
		CREATED_DATETIME,
	    
		--Added for TFS 3322
		EffectiveDate,
		EndDate  
	   )    
	    
		VALUES     
		(    
		@ASSIGN_ID,    
		@USER_ID,    
		@COUNTRY_ID,    
		@LOB_ID,    
		@PML_LIMIT,    
		@PREMIUM_APPROVAL_LIMIT,    
		@CLAIM_RESERVE_LIMIT,    
		@CLAIM_REOPEN,    
		@CLAIM_SETTLMENT_LIMIT,    
		@CREATED_BY,    
		@CREATED_DATETIME,

		--Added for TFS 3322
		@EFFECTIVEDATE,
		@ENDDATE   
		)    
		RETURN @Assign_id 
	END
	ELSE --IF DATE_DIFF > 1
	BEGIN
		UPDATE MNT_UNDERWRITING_CLAIM_LIMITS SET EndDate = @MODIFIED_LAST_END_DATE WHERE ASSIGN_ID = @LAST_ASSIGN_ID
	
		INSERT INTO MNT_UNDERWRITING_CLAIM_LIMITS    
		(    
		ASSIGN_ID,    
		USER_ID,    
		COUNTRY_ID,    
		LOB_ID,    
		PML_LIMIT,    
		PREMIUM_APPROVAL_LIMIT,    
		CLAIM_RESERVE_LIMIT,    
		CLAIM_REOPEN,    
		CLAIM_SETTLMENT_LIMIT,    
		CREATED_BY,    
		CREATED_DATETIME,
	    
		--Added for TFS 3322
		EffectiveDate,
		EndDate  
	   )    
	    
		VALUES     
		(    
		@ASSIGN_ID,    
		@USER_ID,    
		@COUNTRY_ID,    
		@LOB_ID,    
		@PML_LIMIT,    
		@PREMIUM_APPROVAL_LIMIT,    
		@CLAIM_RESERVE_LIMIT,    
		@CLAIM_REOPEN,    
		@CLAIM_SETTLMENT_LIMIT,    
		@CREATED_BY,    
		@CREATED_DATETIME,

		--Added for TFS 3322
		@EFFECTIVEDATE,
		@ENDDATE   
		)    
		RETURN @Assign_id 
	END
END
ELSE -- IF DATE_DIFF < 0
BEGIN	
	IF(@MODIFIED_LAST_END_DATE > @LAST_EFFECTIVE_DATE)
	BEGIN
		UPDATE MNT_UNDERWRITING_CLAIM_LIMITS SET EndDate = @MODIFIED_LAST_END_DATE WHERE ASSIGN_ID = @LAST_ASSIGN_ID
	
		INSERT INTO MNT_UNDERWRITING_CLAIM_LIMITS    
		(    
		ASSIGN_ID,    
		USER_ID,    
		COUNTRY_ID,    
		LOB_ID,    
		PML_LIMIT,    
		PREMIUM_APPROVAL_LIMIT,    
		CLAIM_RESERVE_LIMIT,    
		CLAIM_REOPEN,    
		CLAIM_SETTLMENT_LIMIT,    
		CREATED_BY,    
		CREATED_DATETIME,
	    
		--Added for TFS 3322
		EffectiveDate,
		EndDate  
	   )    
	    
		VALUES     
		(    
		@ASSIGN_ID,    
		@USER_ID,    
		@COUNTRY_ID,    
		@LOB_ID,    
		@PML_LIMIT,    
		@PREMIUM_APPROVAL_LIMIT,    
		@CLAIM_RESERVE_LIMIT,    
		@CLAIM_REOPEN,    
		@CLAIM_SETTLMENT_LIMIT,    
		@CREATED_BY,    
		@CREATED_DATETIME,

		--Added for TFS 3322
		@EFFECTIVEDATE,
		@ENDDATE   
		)    
		RETURN @Assign_id 
	END
	ELSE
		RETURN 0
END
END
-----------------------------------------------------------------------------------

  