IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_UPDATE_COMMISSION_FLAG]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_UPDATE_COMMISSION_FLAG]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP PROC PROC_UPDATE_COMMISSION_FLAG  
CREATE PROC PROC_UPDATE_COMMISSION_FLAG   
@TABLE_NAME VARCHAR(MAX) = '',  
@ROW_ID INT =0,  
@CLAIM_ID INT =0,  
@ACTIVITY_ID INT =0,
@COV_ID INT = 0,
@FLAG_RI_CLAIM INT = 0,  
@COMP_ID INT =0,
@IS_COMMISSION_PROCESS VARCHAR(2) = 'Y'
AS  

BEGIN                
     --commented for first case of customer refund           
IF @TABLE_NAME = 'ACT_CUSTOMER_OPEN_ITEMS'                 
BEGIN                
 UPDATE ACT_CUSTOMER_OPEN_ITEMS                
 SET IS_COMMISSION_PROCESS = @IS_COMMISSION_PROCESS                
 WHERE IDEN_ROW_ID = @ROW_ID                
END               
              
 IF @TABLE_NAME = 'ACT_POLICY_INSTALLMENT_DETAILS'                 
BEGIN                
 UPDATE ACT_POLICY_INSTALLMENT_DETAILS                
 SET IS_COMMISSION_PROCESS = @IS_COMMISSION_PROCESS                
 WHERE ROW_ID = @ROW_ID                
END               
               
                
ELSE IF @TABLE_NAME = 'ACT_AGENCY_STATEMENT_DETAILED'                 
BEGIN                
 UPDATE ACT_AGENCY_STATEMENT_DETAILED                
 SET IS_COMMISSION_PROCESS = @IS_COMMISSION_PROCESS                
 WHERE ROW_ID = @ROW_ID                
END                
  --commented by naveen itrack 1750, refund change in co and ri.           
  --added by naveen, itrack 1750, sheet version 20              
ELSE IF @TABLE_NAME = 'ACT_COI_STATEMENT_DETAILED'                 
BEGIN                
 UPDATE ACT_COI_STATEMENT_DETAILED                
 SET IS_COMMISSION_PROCESS = @IS_COMMISSION_PROCESS                
 WHERE ROW_ID = @ROW_ID            
 END           
           
ELSE IF @TABLE_NAME = 'ACT_POLICY_INSTALLMENT_DETAILS'                 
BEGIN                
 UPDATE ACT_POLICY_INSTALLMENT_DETAILS                
 SET IS_COMMISSION_PROCESS = @IS_COMMISSION_PROCESS                
 WHERE ROW_ID = @ROW_ID                
END                
ELSE IF @TABLE_NAME = 'CLM_ACTIVITY_RESERVE'                 
BEGIN               
 IF  @COV_ID > 0 --CLM_EXPENSE              
  BEGIN              
  UPDATE CLM_ACTIVITY_RESERVE                
    SET IS_COMMISSION_PROCESS = @IS_COMMISSION_PROCESS                
    WHERE CLAIM_ID = @CLAIM_ID                
      AND ACTIVITY_ID = @ACTIVITY_ID              
      AND COVERAGE_ID = @COV_ID               
  END               
 ELSE -- CLM_INDEMNITY              
  BEGIN              
   UPDATE CLM_ACTIVITY_RESERVE                
    SET IS_COMMISSION_PROCESS = @IS_COMMISSION_PROCESS                
    WHERE CLAIM_ID = @CLAIM_ID                
      AND ACTIVITY_ID = @ACTIVITY_ID               
  END              
               
                
 END               
ELSE  IF @TABLE_NAME = 'CLM_ACTIVITY_CO_RI_BREAKDOWN'  --RI_CLAIM              
  BEGIN                
   UPDATE CACRB              
   SET CACRB.IS_COMMISSION_PROCESS = @IS_COMMISSION_PROCESS              
   FROM CLM_ACTIVITY_CO_RI_BREAKDOWN CACRB  WITH(NOLOCK)              
   INNER JOIN CLM_ACTIVITY_RESERVE  CAR WITH(NOLOCK)              
    ON CAR.CLAIM_ID= CACRB.CLAIM_ID               
    AND CAR.ACTIVITY_ID = CACRB.ACTIVITY_ID               
    AND CAR.RESERVE_ID = CACRB.RESERVE_ID              
   WHERE CACRB.COMP_ID = @COMP_ID              
    AND CAR.CLAIM_ID= @CLAIM_ID              
    AND CAR.ACTIVITY_ID =  @ACTIVITY_ID              
  END                 
ELSE IF @TABLE_NAME = 'POL_REINSURANCE_BREAKDOWN_DETAILS'                 
BEGIN                
 UPDATE POL_REINSURANCE_BREAKDOWN_DETAILS                
 SET IS_COMMISSION_PROCESS = @IS_COMMISSION_PROCESS                
 WHERE IDEN_ROW_ID = @ROW_ID                
END                
ELSE                
BEGIN                
PRINT ''                
END                
                
                
                   
END 
GO

