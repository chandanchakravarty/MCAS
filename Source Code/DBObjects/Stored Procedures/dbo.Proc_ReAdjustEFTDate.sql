IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ReAdjustEFTDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ReAdjustEFTDate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran     
--drop proc dbo.Proc_ReAdjustEFTDate    
--go     
/*----------------------------------------------------------        
Proc Name       : dbo.Proc_ReAdjustEFTDate    
Created by      : Ravindra    
Date            : 04-14-2007    
Purpose         : Will readjust Due dates of Installment which are not released    
        
Revison History :        
Modified By  : Praveen Kasana  
Modified On  : 2 Sep 2009  
Purpose   : Itrack 6320 :  
    When due date of EFT installment is different than effective date of the policy, the 1st EFT installment  
    should be set based on the 15 day rule, the next EFT installment should be due on the same date in the following month,  
    and the next EFT installment should be due on the same date in the next month, and so forth.    
  
Modified By  : Praveen Kasana  
Modified On  : 8 Oct 2009  
Purpose   : Itrack 6320 :  
    : @EFT_TENTATIVE_DATE > 15 and Calculated Date Month is in FEB.  
  
  
Modified By  : Praveen Kasana  
Modified On  : 12 Oct 2009  
Purpose   : Itrack 6537 :  


Modified By  : Praveen Kasana  
Modified On  : 12 Oct 2009  
Purpose   : Itrack 6537 :  
  
  
           
Used In  : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
-- drop proc dbo.[Proc_ReAdjustEFTDate]   2126,439,1,22   
CREATE PROC [dbo].[Proc_ReAdjustEFTDate]  
(    
 @CUSTOMER_ID   int,        
 @POLICY_ID    int,        
 @POLICY_VERSION_ID     smallint,        
 @EFT_TENTATIVE_DATE    int     
)    
AS     
BEGIN     
DECLARE @APP_EXPIRATION_DATE DATETIME    
DECLARE @CURRENT_TERM Int,    
 @EFT Int    
    
SET @EFT   = 11973    
    
SELECT @CURRENT_TERM = CURRENT_TERM,@APP_EXPIRATION_DATE = APP_EXPIRATION_DATE FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)    
WHERE CUSTOMER_ID = @CUSTOMER_ID     
AND   POLICY_ID   = @POLICY_ID     
AND   POLICY_VERSION_ID = @POLICY_VERSION_ID     
    

-- If there are any unreleased installments for this Policy Term with payment mode eft     
-- readjust their due date     
IF EXISTS (SELECT ROW_ID FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID     
   AND POLICY_ID = @POLICY_ID    
   AND CURRENT_TERM = @CURRENT_TERM    
   AND ISNULL(RELEASED_STATUS,'N') <> 'Y'     
   AND  ISNULL(PAYMENT_MODE,0) = @EFT   )    
    
BEGIN  

   
 CREATE TABLE #TMP_OPEN_ITEMS     
 (    
 IDEN_COL Int Identity(1,1),    
 OPEN_ITEM_ID Int,    
 DUE_DATE Datetime,    
 INS_ROW_ID Int,    
 )    
  
 INSERT INTO #TMP_OPEN_ITEMS(OPEN_ITEM_ID , DUE_DATE , INS_ROW_ID )    
 SELECT OI.IDEN_ROW_ID , OI.DUE_DATE , INSD.ROW_ID     
 FROM ACT_CUSTOMER_OPEN_ITEMS OI WITH(NOLOCK)   
 INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS INSD WITH(NOLOCK)   
 ON OI.INSTALLMENT_ROW_ID = INSD.ROW_ID     
 WHERE INSD.CUSTOMER_ID = @CUSTOMER_ID     
 AND INSD.POLICY_ID = @POLICY_ID     
 AND INSD.CURRENT_TERM = @CURRENT_TERM     
 AND  ISNULL(INSD.PAYMENT_MODE,0) = @EFT       
 AND  ISNULL(INSD.RELEASED_STATUS,'N') <> 'Y'   
 AND ISNULL(OI.NOTICE_SEND ,'N' ) <> 'Y'    

 DECLARE @IDEN_COL   int ,    
   @DUE_DATE   Datetime,    
   @NEW_DUE_DATE  Datetime,    
   @OPEN_ITEM_ID  Int,    
   @INS_ROW_ID   Int,     
   @TEMP_DUE_DATE  Datetime ,  
   @DAY_LIMIT int   
  
--Added for itrack #6906 by Shikha.  
   IF NOT EXISTS (  
     SELECT OI.IDEN_ROW_ID FROM ACT_CUSTOMER_OPEN_ITEMS OI INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS INSD ON OI.INSTALLMENT_ROW_ID = INSD.ROW_ID   
     WHERE INSD.CUSTOMER_ID = @CUSTOMER_ID AND INSD.POLICY_ID = @POLICY_ID AND INSD.CURRENT_TERM = @CURRENT_TERM   
     AND (ISNULL(INSD.RELEASED_STATUS,'N') = 'Y' OR ISNULL(OI.NOTICE_SEND ,'N' ) = 'Y')  
      )  
 BEGIN  
  EXEC Proc_DoPostRenewalAdjustments @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID  
 END  
  
 SET @IDEN_COL = 1    
 WHILE(1 = 1)      
 BEGIN  
	
  IF NOT EXISTS(SELECT IDEN_COL FROM #TMP_OPEN_ITEMS WHERE IDEN_COL = @IDEN_COL )    
	BEGIN
		
		RETURN 1
	END

  SELECT @DUE_DATE = DUE_DATE ,@OPEN_ITEM_ID = OPEN_ITEM_ID ,    
    @INS_ROW_ID = INS_ROW_ID    
  FROM #TMP_OPEN_ITEMS WHERE IDEN_COL = @IDEN_COL    
  
  DECLARE @DD SmallInt,    
    @MM SmallInt,    
    @YYYY SmallInt    

  IF( @EFT_TENTATIVE_DATE < DATEPART(DD,@DUE_DATE)  )    
  BEGIN     
   SET @DD = @EFT_TENTATIVE_DATE    
   SET @MM = DATEPART(MM,@DUE_DATE)+ 1    
   SET @YYYY = DATEPART(YYYY,@DUE_DATE)    
  END    
  ELSE IF(@EFT_TENTATIVE_DATE > DATEPART(DD,@DUE_DATE)  )    
  BEGIN     
   SET @DD = @EFT_TENTATIVE_DATE    
   SET @MM = DATEPART(MM,@DUE_DATE)    
   SET @YYYY = DATEPART(YYYY,@DUE_DATE)    
  END    
  ELSE IF(@EFT_TENTATIVE_DATE = 28 AND DATEPART(DD,@DUE_DATE) = 28)  
  BEGIN   
   SET @DD = @EFT_TENTATIVE_DATE    
   SET @MM = DATEPART(MM,@DUE_DATE)+ 1    
   SET @YYYY = DATEPART(YYYY,@DUE_DATE)    
  END   
  ELSE     
  BEGIN    
   BREAK     
  END     

    
  -- If month is Feb then date will be updated to 28 if it is 29    
  IF(@MM = 2 AND @DD >=29)    
  BEGIN     
   SET @DD = 28    
  END    
  --IF @MM=0 i.e Installment due in January has to be migrated to last Dec    
  IF (@MM = 0)     
  BEGIN     
   SET @MM = 12    
   SET @YYYY = @YYYY-1    
  END    
  -- If month is of 30 days and @DD=31 then date will be updated to 30 if it is 31     
  IF((@MM = 4 or @MM = 6 or @MM = 9 or @MM = 11) and @DD =31)    
  BEGIN     
   SET @DD = 30    
  END    
  
  --If month is 13 or greater than   
  IF(@MM >=13)   
  BEGIN  
   SET @MM = 1  
   SET @YYYY = @YYYY+1   
  END   
    
  SET @TEMP_DUE_DATE = CAST(    
  ( CONVERT(VARCHAR,@YYYY) + '-' + CONVERT(VARCHAR,@MM) + '-' + CONVERT(VARCHAR, @DD) )    
  AS DATETIME)   
  
  --Intialise only once  
  IF(@IDEN_COL = 1)  
  BEGIN  
   SET @DAY_LIMIT = ABS(DATEDIFF(DD, @TEMP_DUE_DATE,@DUE_DATE))  
  END  
  
     
  --RAvindra(07-07-09): iTrack 6061, Change the 5 day limit to 15 days     
  --IF(ABS(DATEDIFF(DD, @TEMP_DUE_DATE,@DUE_DATE))  > 15) --5 )  //Commented Itrack 6320  
  IF(@DAY_LIMIT > 15)  
  BEGIN     
   SET @NEW_DUE_DATE = DATEADD(MM , -1 , @TEMP_DUE_DATE)    
   --Added on 8 Oct 2009 Itrack 6320  
   IF(MONTH(@TEMP_DUE_DATE) = 2 AND @EFT_TENTATIVE_DATE >=29 AND MONTH(@NEW_DUE_DATE) =1)  
   BEGIN  
    SET @NEW_DUE_DATE = CAST(    
    ( CONVERT(VARCHAR,YEAR(@NEW_DUE_DATE)) + '-' + CONVERT(VARCHAR,MONTH(@NEW_DUE_DATE)) + '-' + CONVERT(VARCHAR, @EFT_TENTATIVE_DATE) )    
    AS DATETIME)    
   END  
  END    
  ELSE    
  BEGIN     
   SET @NEW_DUE_DATE = @TEMP_DUE_DATE    
  END    
  
  IF (DATEDIFF(DD,@NEW_DUE_DATE,@APP_EXPIRATION_DATE) >=0)  
  BEGIN  
   UPDATE ACT_CUSTOMER_OPEN_ITEMS SET DUE_DATE = @NEW_DUE_DATE ,    
   SWEEP_DATE  = @NEW_DUE_DATE    
   WHERE IDEN_ROW_ID = @OPEN_ITEM_ID     
  
    UPDATE ACT_POLICY_INSTALLMENT_DETAILS SET  INSTALLMENT_EFFECTIVE_DATE =  @NEW_DUE_DATE    
    WHERE CUSTOMER_ID = @CUSTOMER_ID      
    AND POLICY_ID  = @POLICY_ID    
    AND ROW_ID = @INS_ROW_ID    
    SET @IDEN_COL = @IDEN_COL + 1     


      RETURN 1  
   
  END  
  ELSE  

   RETURN -5     
 END    
  
 DROP TABLE #TMP_OPEN_ITEMS   
  
--Changes for itrack #6906 by Shikha.  
   
END     
     
END    
   
--go  
--EXEC Proc_ReAdjustEFTDate 1577 ,70 , 3 ,18  
--  
----SELECT A.SOURCE_ROW_ID, A.POLICY_VERSION_ID AS VER ,A.RISK_ID , A.TRANS_DESC,A.TOTAL_DUE,A.TOTAL_PAID,    
----A.SWEEP_DATE,A.ITEM_TRAN_CODE_TYPE,A.ITEM_TRAN_CODE,A.AGENCY_COMM_PERC, A.NET_PREMIUM, A.GROSS_AMOUNT,    
----ISNULL(B.INSTALLMENT_NO,50) INS_NO,B.INSTALLMENT_NO  AS INS_NO,B.RELEASED_STATUS,A.RISK_ID,A.RISK_TYPE,    
----A.* FROM ACT_CUSTOMER_OPEN_ITEMS A WITH(NOLOCK)    
----left JOIN ACT_POLICY_INSTALLMENT_DETAILS B WITH(NOLOCK) ON A.INSTALLMENT_ROW_ID  = B.ROW_ID    
----LEFT JOIN MNT_LOOKUP_VALUES MNT ON     
----B.PAYMENT_MODE = MNT.LOOKUP_UNIQUE_ID     
----WHERE A.CUSTOMER_ID= 1577 AND A.POLICY_ID = 57 --AND ITEM_TRAN_CODE_TYPE = 'PREM' --AND A.SOURCE_ROW_ID = 79    
----ORDER BY ISNULL(B.CURRENT_TERM,50),ISNULL(B.INSTALLMENT_NO,50) ,    
----A.IDEN_ROW_ID ,A.SOURCE_EFF_DATE,A.UPDATED_FROM    
--rollback tran    
  
  
  
  
GO

