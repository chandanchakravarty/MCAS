IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GetCacellationProAmount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GetCacellationProAmount]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                          
Proc Name        : dbo.Proc_Generate_MasterPolicyInstallments      
Created by       : Lalit chauhan      
Date             : 22/10/2010                                        
Purpose          :       
Used In          : Ebix Advantage                                      
------------------------------------------------------------                                          
Date     Review By          Comments            
DROP PROC PROC_GetCacellationProAmount  

PROC_GetCacellationProAmount 28070,122,5,'02/25/2012'
------   ------------       -------------------------    
*/
CREATE PROC [dbo].[PROC_GetCacellationProAmount]
(
@CUSTOMER_ID INT,
@POLICY_ID INT,
@POLICY_VERSION_ID INT,
@CANCELLATION_EFFECTIVE_DATE DATETIME
)
AS 
BEGIN


DECLARE @CURRENT_TERM INT,@POL_EFF_DATE DATETIME , @POL_EXP_DATE DATETIME ,
@POL_TOTAL_TERM INT,@EFF_DAY INT,@POL_TOTAL_PRM DECIMAL(25,2),@TRAN_PREMIUM DECIMAL(25,2),
@PAID_AMOUNT DECIMAL(25,2),@RETURN_PREMIUM DECIMAL(25,2),
@POLICY_EFF_DAYS INT,@END_EFF_DAYS INT,@RETENTION_PREMIUM  DECIMAL(25,2)
,@RETURN_FULL_PREMIUM DECIMAL(25,2),@SUM_NEGA_PRM DECIMAL(25,2)

		
	SELECT @CURRENT_TERM = CURRENT_TERM,
	@POL_EFF_DATE = ISNULL(POLICY_EFFECTIVE_DATE,APP_EXPIRATION_DATE),
	@POL_EXP_DATE = ISNULL(POLICY_EXPIRATION_DATE,APP_EXPIRATION_DATE)
	 FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID  =@POLICY_ID 
	AND POLICY_VERSION_ID = @POLICY_VERSION_ID
	
	SELECT @POL_TOTAL_TERM = DATEDIFF(DAY,@POL_EFF_DATE ,@POL_EXP_DATE)
	SELECT @EFF_DAY = DATEDIFF(DAY,@POL_EFF_DATE ,@CANCELLATION_EFFECTIVE_DATE)
	
	
	SELECT @POL_TOTAL_PRM  = SUM(INSTALLMENT_AMOUNT) FROM ACT_POLICY_INSTALLMENT_DETAILS INS WITH(NOLOCK)
	WHERE  CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID IN(
	SELECT POL.POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST POL WITH(NOLOCK) WHERE 
	POL.CUSTOMER_ID = @CUSTOMER_ID 
	AND POL.POLICY_ID = @POLICY_ID --AND POL.POLICY_VERSION_ID = @POLICY_VERSION_ID 
	AND CURRENT_TERM = @CURRENT_TERM )
	
	DECLARE @RISK_DETAILS TABLE
	(
	CUSTOMER_ID INT,
	POLICY_ID INT,
	POLICY_VERSION_ID INT,
	RISK_ID INT,
	WRITTEN_PREMIUM DECIMAL(25,2),
	EFFECTIVE_DATETIME DATETIME,
	EXPIRY_DATETIME DATETIME
	)
	
	INSERT INTO @RISK_DETAILS
	EXEC Poc_GetRiskEffectiveDate @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID
	--SELECT * FROM @RISK_DETAILS
	--get Endorsement Effective day
	CREATE TABLE #tempEndEffectiveDays 
	(RISK_ID INT ,PREMIUM DECIMAL(25,2), POL_VERSION_ID INT,POL_EFF_DAYS INT,END_PERIOD INT) 
	
	--CREATE TABLE  #tempPrmDiff(RISK_ID INT,PREMIUM DECIMAL(25,2),POLICY_VERSION_ID INT,
	--EFFECTIVE_DATE DATETIME,EXP_DATE DATETIME,WRITTEN_PREMIUM DECIMAL(25,2)
	--)
	
	--INSERT INTO #tempPrmDiff 
	--SELECT * FROM  (
	--select a.RISK_ID,
	-- --x=(select b.WRITTEN_PREMIUM from #RISK_DETAILS b where b.CUSTOMER_ID = a.CUSTOMER_ID and b.POLICY_ID = a.POLICY_ID and b.POLICY_VERSION_ID = a.POLICY_VERSION_ID +1),
	-- a.WRITTEN_PREMIUM - isnull((select b.WRITTEN_PREMIUM from @RISK_DETAILS b where b.CUSTOMER_ID = a.CUSTOMER_ID 
	-- and b.POLICY_ID = a.POLICY_ID and b.POLICY_VERSION_ID+1 = a.POLICY_VERSION_ID and a.RISK_ID = b.RISK_ID ),0
	-- ) PRM_DIFF,a.POLICY_VERSION_ID,a.EFFECTIVE_DATETIME,a.EXPIRY_DATETIME,a.WRITTEN_PREMIUM
	
	--from @RISK_DETAILS a
	--)b
	--WHERE PRM_DIFF <> WRITTEN_PREMIUM

	
	--SELECT * FROM #tempPrmDiff
	--DELETE @RISK_DETAILS FROM @RISK_DETAILS r INNER JOIN (
	--SELECT a.* FROM @RISK_DETAILS a  JOIN #tempPrmDiff b ON
	-- a.RISK_ID = b.RISK_ID and a.POLICY_VERSION_ID = b.POLICY_VERSION_ID
	--)b
	--ON 
	--r.RISK_ID = b.RISK_ID and r.POLICY_VERSION_ID = b.POLICY_VERSION_ID
	

	--INSERT INTO @RISK_DETAILS 
	--SELECT @CUSTOMER_ID CUSTOMER_ID ,@POLICY_ID POLICY_ID,POLICY_VERSION_ID,RISK_ID,PREMIUM,EFFECTIVE_DATE,
	--EXP_DATE FROM  #tempPrmDiff
	
	
	INSERT INTO #tempEndEffectiveDays SELECT RISK_ID,a.WRITTEN_PREMIUM,POLICY_VERSION_ID, DATEDIFF(day,EFFECTIVE_DATETIME,
		@CANCELLATION_EFFECTIVE_DATE),DATEDIFF(day,a.EFFECTIVE_DATETIME,a.EXPIRY_DATETIME)
	 FROM @RISK_DETAILS a  
	 
	
	 --SELECT * FROM #tempEndEffectiveDays
	SELECT @RETENTION_PREMIUM =  
	CAST(
	SUM(
		CASE WHEN POL_EFF_DAYS > 0  
				THEN ISNULL(PREMIUM*POL_EFF_DAYS/END_PERIOD,0)
			ELSE 0
		END
	)AS DECIMAL(25,2)) 
	FROM #tempEndEffectiveDays
	--SELECT @RETENTION_PREMIUM
	
	SELECT @RETURN_FULL_PREMIUM  = ISNULL(SUM(ISNULL(PREMIUM,0)),0)
	FROM #tempEndEffectiveDays WHERE POL_EFF_DAYS < 0 
	

	--Policy = 01/03/2011 – 01/01/2011 = 59 days 
	--Endorsement = 01/03/2011 – 15/02/2011 = 14 days
	--Endorsement Period = 15/02/2011 – 31/12/2011 = 319 days 
	--Retention Premium = (1000 * 59/365) + (-200 * 14/319) =   161,64 - 8,78 = 152,86
	--Refund Premium = (Paid Premium – Refund Premium)– Retention Premium = (500-200) – 
	
	SELECT @SUM_NEGA_PRM =  ISNULL(SUM(ISNULL(WRITTEN_PREMIUM,0)),0) FROM @RISK_DETAILS WHERE EFFECTIVE_DATETIME > @CANCELLATION_EFFECTIVE_DATE 

	
	SELECT @PAID_AMOUNT = ISNULL(SUM(INSTALLMENT_AMOUNT),0) FROM ACT_POLICY_INSTALLMENT_DETAILS INS WITH(NOLOCK)
	WHERE  CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND  RELEASED_STATUS = 'Y' AND POLICY_VERSION_ID  IN(
	SELECT POL.POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST POL WITH(NOLOCK) WHERE 
	POL.CUSTOMER_ID = @CUSTOMER_ID 
	AND POL.POLICY_ID = @POLICY_ID --AND POL.POLICY_VERSION_ID = @POLICY_VERSION_ID 
	AND CURRENT_TERM = @CURRENT_TERM ) 
    
	--SELECT  @TRAN_PREMIUM = (@POL_TOTAL_PRM / @POL_TOTAL_TERM )* @EFF_DAY 
		
	--Changed By Lalit Feb 14,2011 as per latest doc 
	IF(@SUM_NEGA_PRM IS NOT NULL )
		SELECT @PAID_AMOUNT = @PAID_AMOUNT + @SUM_NEGA_PRM
	
	--SELECT @PAID_AMOUNT
	SELECT @RETURN_PREMIUM = ROUND(@PAID_AMOUNT,0) - ROUND(@RETENTION_PREMIUM,0)
	--IF(ISNULL(@PAID_AMOUNT,0) > @RETURN_PREMIUM )
	--	BEGIN
	--		SELECT @RETURN_PREMIUM = @RETURN_PREMIUM  --+ ISNULL(@RETURN_FULL_PREMIUM,0)
	--	END
	--IF(@PAID_AMOUNT = @POL_TOTAL_PRM)
	--		BEGIN
	--			SELECT @RETURN_PREMIUM = @POL_TOTAL_PRM - @RETENTION_PREMIUM  --@RETURN_PREMIUM  --+ ISNULL(@RETURN_FULL_PREMIUM,0)
	--		END

	DROP TABLE #tempEndEffectiveDays
	--DROP TABLE #tempPrmDiff

	SELECT  ROUND(@RETURN_PREMIUM,0) RETURN_PREMIUM
	
		

	

END


GO

