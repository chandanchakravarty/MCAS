IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CalculateReturnPremium]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CalculateReturnPremium]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran   
--drop proc Dbo.Proc_CalculateReturnPremium  
--go  
/*----------------------------------------------------------                  
Proc Name       : Dbo.Proc_CalculateReturnPremium  
Created by      : Ravindra   
Date            : 12-30-2006  
Purpose         : Calculates  the Return Premium of the Policy.        
Revison History :  
            
MODIFIED bY  :Pravesh k Chandel  
modified Date : 31 july 2007  
Purpose  : set @MINIMUM_PREMIUM=0 n case of Rescind Process   
  
MODIFIED bY  :Pravesh k Chandel  
modified Date : 9 oct  2007  
Purpose  : set @MINIMUM_PREMIUM=0 n case of Cancellation Process / NSF - Replace/No replacement as per itrack 2687  
  
MODIFIED bY  :Pravesh k Chandel  
modified Date : 26 JUNE 2008  
Purpose  : set @MINIMUM_PREMIUM=0 n case of Cancellation Process / Rescind  type as per itrack 4398  
  
Modified By		: Ravindra
Modified On		: 08-20-2008
Purpose			: Changed calculation logic as discussed with Sally 

Used In       : Wolverine   (Cancellation Process)    
             
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
-- drop proc Dbo.Proc_CalculateReturnPremium  
CREATE PROC [dbo].[Proc_CalculateReturnPremium]  
(                  
  @CUSTOMER_ID  INT,            
  @POLICY_ID  INT,          
  @POLICY_VERSION_ID INT,        
  @CHANGE_EFFECTIVE_DATE Datetime,  
  @CANCELLATION_PREMIUM Decimal(25,2) OUT,  
  @CANCELLTION_TYPE    INT=NULL,  
  @CANCELLATION_OPTION  int =null  
)                  
AS                 
BEGIN   
  
DECLARE @TOTAL_INFORCE_PREMIUM  Decimal(25,2) ,          
		@TOTAL_WRITTEN_PREMIUM  Decimal(25,2) , 
		@WRITTEN_EFF_AFTER_CANCELLATION Decimal(18,2),     
		@CURRENT_TERM    Int,  
		@POLICY_EFFECTIVE_DATE Datetime,  
		@POLICY_EXPIRATION_DATE Datetime,  
		@INSTALL_PLAN_ID  Int,  
		@LOB_ID   Int,  
		@SUB_LOB_ID   Smallint,  
		@STATE_ID   SmallInt,  
		@EARNED_PREMIUM  Decimal(25,2),  
		@MINIMUM_PREMIUM  Decimal(25,2)  
  
-- Fetch Policy Level Details  
SELECT  @CURRENT_TERM = CURRENT_TERM ,  
		@POLICY_EFFECTIVE_DATE  = APP_EFFECTIVE_DATE,  
		@POLICY_EXPIRATION_DATE = APP_EXPIRATION_DATE,  
		@INSTALL_PLAN_ID = INSTALL_PLAN_ID,  
		@LOB_ID  = POLICY_LOB ,   
		@SUB_LOB_ID = ISNULL(POLICY_SUBLOB ,0),  
		@STATE_ID = STATE_ID  
FROM POL_CUSTOMER_POLICY_LIST   
WHERE   CUSTOMER_ID = @CUSTOMER_ID   
AND POLICY_ID = @POLICY_ID   
AND POLICY_VERSION_ID = @POLICY_VERSION_ID  

SET @MINIMUM_PREMIUM  = 0   
  
-- If cancellation after renewal Minimum premium will be zero   
DECLARE @BASE_PROCESS AS VarChar(5)  
DECLARE @CURRENT_PROCESS AS VarChar(5)  
  
SELECT  @BASE_PROCESS =   CASE PROCESS_ID WHEN 25 THEN 'NBS'    WHEN 18 THEN 'REN'   END    
FROM POL_POLICY_PROCESS  (NOLOCK)   
WHERE  CUSTOMER_ID= @CUSTOMER_ID     
AND POLICY_ID=@POLICY_ID     
AND PROCESS_ID IN (25,18)   -- New Business Commit & Renewal Commit    
AND PROCESS_STATUS ='COMPLETE'    
AND ISNULL(REVERT_BACK,'N') <> 'Y'  
AND NEW_POLICY_VERSION_ID IN     
(    
	SELECT MAX(NEW_POLICY_VERSION_ID) FROM POL_POLICY_PROCESS ( NOLOCK)   
	WHERE    
	CUSTOMER_ID= @CUSTOMER_ID    
	AND POLICY_ID=@POLICY_ID    
	AND PROCESS_ID IN (25,18)    
	AND PROCESS_STATUS ='COMPLETE'    
	AND ISNULL(REVERT_BACK,'N') <> 'Y'  
) 
   
--by pravesh geting current process on the policy  
SET @CURRENT_PROCESS='';  

SELECT @CURRENT_PROCESS=  CASE PROCESS_ID WHEN 28 THEN 'RESC' WHEN 29 THEN 'RESC' ELSE ''  END     
FROM POL_POLICY_PROCESS ( NOLOCK)   
WHERE    
CUSTOMER_ID= @CUSTOMER_ID    
AND POLICY_ID=@POLICY_ID    
AND NEW_POLICY_VERSION_ID IN  
(  
	SELECT MAX(NEW_POLICY_VERSION_ID) FROM POL_POLICY_PROCESS ( NOLOCK)   
	WHERE    
	CUSTOMER_ID= @CUSTOMER_ID    
	AND POLICY_ID=@POLICY_ID    
)  
--end here  

--IF (@BASE_PROCESS <> 'REN' AND @CURRENT_PROCESS <> 'RESC')  
IF (@CURRENT_PROCESS <> 'RESC')   -- commented by Pravesh on 29 july08 as min cancel premium will also be charged if cancel after renewal as discussed with Rajan
BEGIN   
	SELECT @MINIMUM_PREMIUM = ISNULL(PREMIUM_AMT , 0)  
	FROM ACT_MINIMUM_PREM_CANCEL  
	WHERE  LOB_ID = @LOB_ID  
	AND ISNULL(SUB_LOB_ID,0) = @SUB_LOB_ID  
	AND STATE_ID = @STATE_ID  
	AND ISNULL(IS_ACTIVE,'Y') = 'Y'  
	AND @CHANGE_EFFECTIVE_DATE BETWEEN EFFECTIVE_FROM_DATE AND EFFECTIVE_TO_DATE  
END  


-- Ravindra(10-02-2008):: As discussed with Saaly; if cancellation after Resintate Lapse Consider 
-- Resinstatement Effective date for minimum premium calculation

DECLARE @REINSTATMENT_TYPE SMALLINT , @LAST_REINSTATE_VERSION int,@REINS_LAPSE INT , @REINS_EFFECTIVE_DATE DATETIME ,
		@TERM_EFFECTIVE_DATE Datetime 

SET @REINS_LAPSE=14244
		
IF EXISTS(
			SELECT CANCELLATION_TYPE FROM POL_POLICY_PROCESS WITH(NOLOCK) 
			WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID	AND PROCESS_ID=16 
			AND POLICY_VERSION_ID IN 
					(
					SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WHERE 
					CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND CURRENT_TERM = @CURRENT_TERM 
					)
			AND CANCELLATION_TYPE = @REINS_LAPSE			
		)
BEGIN
			SELECT @LAST_REINSTATE_VERSION = ISNULL(MAX(POLICY_VERSION_ID),0) FROM POL_POLICY_PROCESS WITH(NOLOCK)
			WHERE CUSTOMER_ID=@CUSTOMER_ID	  AND POLICY_ID=@POLICY_ID AND PROCESS_ID=16  

			SELECT @REINS_EFFECTIVE_DATE=EFFECTIVE_DATETIME FROM POL_POLICY_PROCESS WITH(NOLOCK) 
			WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID
			AND PROCESS_ID=16 AND POLICY_VERSION_ID > = @LAST_REINSTATE_VERSION
			AND CANCELLATION_TYPE = @REINS_LAPSE
END


SET @TERM_EFFECTIVE_DATE = ISNULL(@REINS_EFFECTIVE_DATE,@POLICY_EFFECTIVE_DATE)

--added by pravesh as If Cancelling a policy using Insured's request and the effective date of canellation is the same as the effective date of the policy   
-- then cancellation is flat (11995) insured requests   
--and the minimum premium does not come into effect as per Itrack Issue 2217  
--11989 - Insured Request  
-- by pravesh on 9 oct 2007  
--11992 - NSF/ Replace    
--11993 - NSF/ No replacement  as per Itrack 2687  
--11970   Rescind  iTRACK 4398 Do not charge the Mininiun Premium   
-- the date of cancellation is equal to the effective date of the policy and Cancellation type is NSF ->Do not charge the Mininiun Premium   
if (  
   (isnull(@CANCELLTION_TYPE,0)=11989 and isnull(@CANCELLATION_OPTION,0) = 11995 and  DATEDIFF (DAY,@TERM_EFFECTIVE_DATE, @CHANGE_EFFECTIVE_DATE )=0)  
 OR  
   ((isnull(@CANCELLTION_TYPE,0)=11992 or isnull(@CANCELLTION_TYPE,0)=11993) and  DATEDIFF (DAY,@TERM_EFFECTIVE_DATE, @CHANGE_EFFECTIVE_DATE )=0)  
 OR  
   (isnull(@CANCELLTION_TYPE,0)=11970 and  DATEDIFF (DAY,@TERM_EFFECTIVE_DATE, @CHANGE_EFFECTIVE_DATE )=0) 
 OR 
  (DATEDIFF (DAY,@TERM_EFFECTIVE_DATE, @CHANGE_EFFECTIVE_DATE )=0) -- NO MINI PREM IF CANCELED FLAT(SAME DATE AS EFF DATE)
   )   
BEGIN  
	set @MINIMUM_PREMIUM=0    
END


SET @TOTAL_INFORCE_PREMIUM = 0  

-- Changed by Ravindra(08-03-2007) For calculating cancellation premium enforce premium need to be considered  
-- instead of written premium  
  
--Ravindra(08-20-2008) : Consider inforce of transactions efective before cancellation 
SELECT @TOTAL_INFORCE_PREMIUM = ISNULL(SUM(ISNULL(PPDD.INFORCE_PREMIUM,0)),0)  ,  
       @TOTAL_WRITTEN_PREMIUM = ISNULL(SUM(ISNULL(PPDD.GROSS_PREMIUM,0)),0)   
FROM ACT_PREMIUM_PROCESS_SUB_DETAILS PPDD  
WHERE  PPDD.CUSTOMER_ID = @CUSTOMER_ID   
AND PPDD.POLICY_ID = @POLICY_ID   
AND PPDD.POLICY_VERSION_ID IN 
						(
							SELECT CPL.POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST CPL WHERE   
							CPL.CUSTOMER_ID = @CUSTOMER_ID AND CPL.POLICY_ID = @POLICY_ID AND CPL.CURRENT_TERM = @CURRENT_TERM 
							AND DATEDIFF(DD, CPL.POL_VER_EFFECTIVE_DATE , @CHANGE_EFFECTIVE_DATE )   > 0 
						) 

-- Calculate Written for transaction effective after cancellation effective date 
SELECT @WRITTEN_EFF_AFTER_CANCELLATION = ISNULL(SUM(ISNULL(PPDD.GROSS_PREMIUM,0)),0)   
FROM ACT_PREMIUM_PROCESS_SUB_DETAILS PPDD  
WHERE  PPDD.CUSTOMER_ID = @CUSTOMER_ID   
AND PPDD.POLICY_ID = @POLICY_ID   
AND PPDD.POLICY_VERSION_ID IN 
						(
							SELECT CPL.POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST CPL WHERE   
							CPL.CUSTOMER_ID = @CUSTOMER_ID AND CPL.POLICY_ID = @POLICY_ID AND CPL.CURRENT_TERM = @CURRENT_TERM 
							AND DATEDIFF(DD, @CHANGE_EFFECTIVE_DATE ,CPL.POL_VER_EFFECTIVE_DATE )   >= 0 
						) 

---- Temporary Code to be removed due the implementation change policies creadted before this change will not   
---- have premium data in ACT_PREMIUM_PROCESS_SUB_DETAILS and hence total premium is required to be pulled   
---- from main premium process table  
--IF ( @TOTAL_INFORCE_PREMIUM IS NULL)  
--BEGIN   
--	SET @TOTAL_INFORCE_PREMIUM = 0  
--
--	CREATE TABLE #POSTED_PREMIUM  
--	(   
--		[IDENT_COL] [int] IDENTITY(1,1) NOT NULL ,  
--		[POSTED_PREMIUM_XML] VARCHAR(4000),  
--		[CURRENT_TERM] Int ,  
--		POLICY_VERSION_ID Int  
--	)  
--	  
--	INSERT INTO #POSTED_PREMIUM  
--	(  
--		POSTED_PREMIUM_XML ,  
--		CURRENT_TERM ,  
--		POLICY_VERSION_ID  
--	)  
--	SELECT  PPD.POSTED_PREMIUM_XML , PCL.CURRENT_TERM ,PPD.POLICY_VERSION_ID  
--	FROM ACT_PREMIUM_PROCESS_DETAILS PPD  
--	INNER JOIN POL_CUSTOMER_POLICY_LIST PCL  
--	ON  PPD.CUSTOMER_ID = PCL.CUSTOMER_ID  
--	AND PPD.POLICY_ID = PCL.POLICY_ID   
--	AND PPD.POLICY_VERSION_ID = PCL.POLICY_VERSION_ID  
--	AND PCL.CURRENT_TERM = @CURRENT_TERM  
--	WHERE  PCL.CUSTOMER_ID = @CUSTOMER_ID   
--	AND PCL.POLICY_ID = @POLICY_ID   
--  
--	DECLARE @POSTED_PREMIUM_XML VARCHAR(4000)  
--	DECLARE @IDENT_COL Int  
--
--	SET @IDENT_COL = 1  
--
--	WHILE 1 = 1                
--	BEGIN                
--		IF NOT EXISTS                
--		(                
--			SELECT IDENT_COL FROM #POSTED_PREMIUM               
--			WHERE IDENT_COL = @IDENT_COL                
--		)                
--		BEGIN                
--			BREAK                
--		END                
--
--		SELECT @POSTED_PREMIUM_XML = POSTED_PREMIUM_XML   
--		FROM #POSTED_PREMIUM  
--		WHERE IDENT_COL = @IDENT_COL                
--
--		DECLARE @IDOC INT 
--		--CREATE AN INTERNAL REPRESENTATION OF THE XML DOCUMENT.          
--		EXEC SP_XML_PREPAREDOCUMENT @IDOC OUTPUT, @POSTED_PREMIUM_XML  
--
--		-- EXECUTE A SELECT STATEMENT THAT USES THE OPENXML ROWSET PROVIDER.          
--
--		DECLARE @NET_PREMIUM VARCHAR(40) ,         
--				@GROSS_PREMIUM VARCHAR(40),          
--				@MCCA_FEES  VARCHAR(40)  ,      
--				@OTHER_FEES VARCHAR(40)        
--
--
--		SELECT  @NET_PREMIUM = NETPREMIUM ,          
--				@GROSS_PREMIUM = GROSSPREMIUM,           
--				@MCCA_FEES = MCCAFEES ,          
--				@OTHER_FEES = OTHERFEES          
--		FROM   OPENXML (@IDOC, '/PREMIUM',2 )          
--		WITH(          
--			NETPREMIUM   VARCHAR(40),           
--			GROSSPREMIUM VARCHAR(40),          
--			MCCAFEES     VARCHAR(40),          
--			OTHERFEES    VARCHAR(40)          
--			)          
--		EXEC SP_XML_REMOVEDOCUMENT @IDOC  
--
--		SET @TOTAL_INFORCE_PREMIUM = @TOTAL_INFORCE_PREMIUM  + ISNULL(CONVERT(Decimal , @GROSS_PREMIUM ), 0)            
--		SET @IDENT_COL = @IDENT_COL + 1  
--	END  
--
--	DROP TABLE #POSTED_PREMIUM  
--END  
----End of temporary code  
---- Changed by Ravindra(08-03-2007) ends here  
  
  
-- Calculate Cancellation Premium   
  
DECLARE @TOTAL_DAYS INT  ,  @CALCULATIVE_EARNED_PREMIUM Decimal(25,2)  , @UN_COVERED_DAYS Int, 
		@UN_COVERED_DAYS_PREMIUM  Decimal(25,2) , @WRITTEN_OFF_AT_CANCELLATION Decimal(25,2) 
  
SET	@CALCULATIVE_EARNED_PREMIUM = 0  

--Ravindra(08-25-2008): As discussed with Sally Written Off/Charged from Agency at cancellation are 
-- not part of total written for the purpose of Cancellation Credit cancellation  
SELECT @WRITTEN_OFF_AT_CANCELLATION = SUM(ISNULL(TOTAL_DUE ,0) ) 
FROM ACT_CUSTOMER_OPEN_ITEMS PPDD 
WHERE  PPDD.CUSTOMER_ID = @CUSTOMER_ID   
AND PPDD.POLICY_ID = @POLICY_ID   
AND PPDD.POLICY_VERSION_ID IN 
						(
							SELECT CPL.POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST CPL WHERE   
							CPL.CUSTOMER_ID = @CUSTOMER_ID AND CPL.POLICY_ID = @POLICY_ID AND CPL.CURRENT_TERM = @CURRENT_TERM 
							AND DATEDIFF(DD, CPL.POL_VER_EFFECTIVE_DATE , @CHANGE_EFFECTIVE_DATE )   > 0 
						) 
AND ISNULL(IS_TEMP_ENTRY,0) = 9 

  
SELECT @TOTAL_WRITTEN_PREMIUM = @TOTAL_WRITTEN_PREMIUM + ISNULL(@WRITTEN_OFF_AT_CANCELLATION,0) 


SET @TOTAL_DAYS = DATEDIFF(DAY, @POLICY_EFFECTIVE_DATE, @POLICY_EXPIRATION_DATE)    
  
--SET @UN_COVERED_DAYS = DATEDIFF(DAY, DATEADD(DD, -1 , @CHANGE_EFFECTIVE_DATE) , @POLICY_EXPIRATION_DATE)  
SET @UN_COVERED_DAYS = DATEDIFF(DAY, @CHANGE_EFFECTIVE_DATE, @POLICY_EXPIRATION_DATE)  

SET @UN_COVERED_DAYS_PREMIUM = ROUND(@TOTAL_INFORCE_PREMIUM * @UN_COVERED_DAYS / @TOTAL_DAYS , 0) 

SET @EARNED_PREMIUM  = @TOTAL_WRITTEN_PREMIUM - @UN_COVERED_DAYS_PREMIUM
 

--SELECT @EARNED_PREMIUM  = ROUND((@TOTAL_INFORCE_PREMIUM * DATEDIFF ( DAY, @POLICY_EFFECTIVE_DATE, @CHANGE_EFFECTIVE_DATE ) /@TOTAL_DAYS),0)  
  
  
IF(@EARNED_PREMIUM > @MINIMUM_PREMIUM)  
BEGIN   
	SET @CALCULATIVE_EARNED_PREMIUM = @EARNED_PREMIUM  
END   
ELSE  
BEGIN   
	SET @CALCULATIVE_EARNED_PREMIUM = @MINIMUM_PREMIUM  
END  

SET @CANCELLATION_PREMIUM = @TOTAL_WRITTEN_PREMIUM  - @CALCULATIVE_EARNED_PREMIUM + ISNULL(@WRITTEN_EFF_AFTER_CANCELLATION,0) 

--IF(@TOTAL_INFORCE_PREMIUM <= @CALCULATIVE_EARNED_PREMIUM)  
-- SET @CANCELLATION_PREMIUM = 0  
--ELSE  
--begin
-- -- To be substracted from Written Premium  
-- --SET @CANCELLATION_PREMIUM = ROUND(@TOTAL_WRITTEN_PREMIUM - @CALCULATIVE_EARNED_PREMIUM,0)  
---- Changed to be subtracted from Enforce Premium for correct figure - 7/28/2008 iTrack 4585
--   SET @CANCELLATION_PREMIUM = ROUND(@TOTAL_INFORCE_PREMIUM - @CALCULATIVE_EARNED_PREMIUM,0)  
--end
--
----condition addedd by pravesh on 28 july 2008 as discussed with Rajan iTrack 4585  
--IF( @CANCELLATION_PREMIUM > @TOTAL_WRITTEN_PREMIUM )  
--	SET @CANCELLATION_PREMIUM = ROUND(@TOTAL_WRITTEN_PREMIUM - @CALCULATIVE_EARNED_PREMIUM,0)  

END  
   
--go   
--
--DELETE from ACT_PREMIUM_PROCESS_SUB_DETAILS WHERE PPD_ROW_ID  = 478
--DECLARE @R Decimal(18,2)  
--
--
--exec Proc_CalculateReturnPremium 1137,7,4,'2008-10-20',@R out   
--
--select @R  
--
--rollback tran  








GO

