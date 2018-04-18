  
/*----------------------------------------------------------              
Proc Name       : dbo.[Proc_MNT_REIN_INSERT_CONTRACT]              
Created by      : HARMANJEET SINGH             
Date            : MAY 09,2007.              
Purpose       :              
Revison History :              
Modified by  : Pravesh K Chandel          
Modified Date : 13 ayg 2007          
Purpose  : to handle Error value and if No Data for @CONTRACT_LOB,@RISK_EXPOSURE and @STATE_ID          
Used In        : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments          
drop PROC [dbo].[Proc_MNT_REIN_INSERT_CONTRACT]              
------   ------------       -------------------------*/              
ALTER PROC [dbo].[Proc_MNT_REIN_INSERT_CONTRACT]              
(              
 @CONTRACT_ID  int output,              
 @CONTRACT_TYPE      int,              
 @CONTRACT_NUMBER    nvarchar(50),            
 @CONTRACT_DESC      nvarchar(500),            
 @LOSS_ADJUSTMENT_EXPENSE nvarchar(50),            
 @RISK_EXPOSURE  nvarchar(500),            
 @CONTRACT_LOB       nvarchar(500),              
 @STATE_ID           nvarchar(500),             
 @ORIGINAL_CONTACT_DATE datetime,            
 @CONTACT_YEAR nvarchar(10),            
 @EFFECTIVE_DATE     datetime,              
 @EXPIRATION_DATE     datetime,              
 @COMMISSION DECIMAL(7,4), -- Datatype changed for Itrack Issue 5397           
 @CALCULATION_BASE   int,            
 @PER_OCCURRENCE_LIMIT nvarchar(50),            
 @ANNUAL_AGGREGATE nvarchar(50),            
 @DEPOSIT_PREMIUMS nvarchar(50),            
 @DEPOSIT_PREMIUM_PAYABLE nvarchar(50),            
 @MINIMUM_PREMIUM nvarchar(50),            
 @SEQUENCE_NUMBER nvarchar(50),            
 @TERMINATION_DATE datetime,            
 @TERMINATION_REASON nvarchar(50),            
 @COMMENTS nvarchar(50),            
 @FOLLOW_UP_FIELDS nvarchar(255),            
 @COMMISSION_APPLICABLE int,            
 @REINSURANCE_PREMIUM_ACCOUNT nvarchar(50),            
 @REINSURANCE_PAYABLE_ACCOUNT nvarchar(50),            
 @REINSURANCE_COMMISSION_ACCOUNT nvarchar(50),            
 @REINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT nvarchar(50),        
 @FOLLOW_UP_FOR int,     
 @CASH_CALL_LIMIT DECIMAL(18,2),           
            
 @CREATED_BY     smallint,              
 @CREATED_DATETIME     datetime,          
 @iERROR int output,              
 @MAX_NO_INSTALLMENT INT,        --Added by Aditya for TFS BUG # 2512   
 @RI_CONTRACTUAL_DEDUCTIBLE DECIMAL(25,2)   --Added by Aditya for TFS BUG # 2916   
)              
AS              
BEGIN              
            
--declare @iERROR smallint            
set @iERROR = 0            
          
--if there is no data for insertion, return from the procedure                
if (@CONTRACT_LOB is null) or (@CONTRACT_LOB='') or  (@RISK_EXPOSURE is null) or (@RISK_EXPOSURE='')  or (@STATE_ID is null) or (@STATE_ID='')            
begin          
 set @iERROR = 1          
 set @CONTRACT_ID=-1              
  return          
end          
            
--begin Transaction             
select @CONTRACT_ID=isnull(Max(CONTRACT_ID),0)+1 from MNT_REINSURANCE_CONTRACT              
INSERT INTO MNT_REINSURANCE_CONTRACT              
(              
 CONTRACT_ID ,              
 CONTRACT_TYPE     ,              
 CONTRACT_NUMBER   ,            
 CONTRACT_DESC    ,            
 LOSS_ADJUSTMENT_EXPENSE ,            
 ORIGINAL_CONTACT_DATE ,            
 CONTACT_YEAR ,            
 EFFECTIVE_DATE     ,              
 EXPIRATION_DATE     ,              
 COMMISSION ,            
 CALCULATION_BASE   ,            
 PER_OCCURRENCE_LIMIT ,            
 ANNUAL_AGGREGATE ,            
 DEPOSIT_PREMIUMS ,            
 DEPOSIT_PREMIUM_PAYABLE ,            
 MINIMUM_PREMIUM ,            
 SEQUENCE_NUMBER ,            
 TERMINATION_DATE ,            
 TERMINATION_REASON ,            
 COMMENTS ,            
 FOLLOW_UP_FIELDS ,            
 COMMISSION_APPLICABLE ,            
 REINSURANCE_PREMIUM_ACCOUNT ,            
 REINSURANCE_PAYABLE_ACCOUNT ,            
 REINSURANCE_COMMISSION_ACCOUNT ,            
 REINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT ,        
 FOLLOW_UP_FOR,    
 CASH_CALL_LIMIT,            
            
 CREATED_BY    ,              
 CREATED_DATETIME,            
 IS_ACTIVE ,  
 MAX_NO_INSTALLMENT ,            --Added by Aditya for TFS BUG # 2512   
 RI_CONTRACTUAL_DEDUCTIBLE --Added by Aditya for TFS BUG # 2916   
)              
VALUES              
(              
 @CONTRACT_ID ,              
 @CONTRACT_TYPE     ,              
 @CONTRACT_NUMBER   ,            
 @CONTRACT_DESC    ,            
 @LOSS_ADJUSTMENT_EXPENSE ,            
 @ORIGINAL_CONTACT_DATE ,            
 @CONTACT_YEAR ,            
 @EFFECTIVE_DATE     ,              
 @EXPIRATION_DATE     ,              
 @COMMISSION ,            
 @CALCULATION_BASE   ,            
 @PER_OCCURRENCE_LIMIT ,            
 @ANNUAL_AGGREGATE ,            
 @DEPOSIT_PREMIUMS ,            
 @DEPOSIT_PREMIUM_PAYABLE ,            
 @MINIMUM_PREMIUM ,            
 @SEQUENCE_NUMBER ,            
 @TERMINATION_DATE ,            
 @TERMINATION_REASON ,            
 @COMMENTS ,            
 @FOLLOW_UP_FIELDS ,            
 @COMMISSION_APPLICABLE ,            
 @REINSURANCE_PREMIUM_ACCOUNT ,            
 @REINSURANCE_PAYABLE_ACCOUNT ,            
 @REINSURANCE_COMMISSION_ACCOUNT ,            
 @REINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT ,        
 @FOLLOW_UP_FOR,     
 @CASH_CALL_LIMIT,           
            
 @CREATED_BY    ,              
 @CREATED_DATETIME,            
 'Y',  
 @MAX_NO_INSTALLMENT,            --Added by Aditya for TFS BUG # 2512   
 @RI_CONTRACTUAL_DEDUCTIBLE  --Added by Aditya for TFS BUG # 2916   
)             
            
if(@@error >0)            
begin            
set @iERROR = 1            
end            
            
            
-- --if there is no data for insertion, return from the procedure                
-- if (@CONTRACT_LOB is null) or (@CONTRACT_LOB='')            
--  set @iERROR = 1              
                
DECLARE @CURRENT_CONTRACT_LOB VARCHAR(20)                
DECLARE @COUNT INT                
DECLARE @XOL_ID INT      
SET @COUNT=2                
              
 SET @CURRENT_CONTRACT_LOB = DBO.PIECE(@CONTRACT_LOB,',',1)                               
   SELECT @XOL_ID=(ISNULL(MAX([XOL_ID]),0)+1)  FROM [dbo].[MNT_XOL_INFORMATION]           
-------------------------------------------------------------------------                   
-- COMMENTED BY SANTOH KUMAR GAUTAM ON 21 MARCH 2011    
-------------------------------------------------------------------------          
-- --Run a loop to go through the list of comma-separated values for insertion                
--while @CURRENT_CONTRACT_LOB is not null                      
-- BEGIN                              
--  --Insert LossCodesType data              
-- --IF NOT EXISTS(SELECT LOSS_CODE_ID FROM CLM_LOSS_CODES WHERE LOB_ID=@LOB_ID AND LOSS_CODE_TYPE=@CURRENT_LOSS_CODE_TYPE)            
--    INSERT INTO MNT_REIN_CONTRACT_LOB                 
--    (   CONTRACT_ID,            
--  CONTRACT_LOB            
                     
--    )                
--    values                
--    (                
--        @CONTRACT_ID,            
--  @CURRENT_CONTRACT_LOB            
--    )              
    
    
 --Run a loop to go through the list of comma-separated values for insertion                
while @CURRENT_CONTRACT_LOB is not null                      
 BEGIN                              
  --Insert LossCodesType data              
 --IF NOT EXISTS(SELECT LOSS_CODE_ID FROM CLM_LOSS_CODES WHERE LOB_ID=@LOB_ID AND LOSS_CODE_TYPE=@CURRENT_LOSS_CODE_TYPE)            
    INSERT INTO MNT_REIN_CONTRACT_LOB                 
    (   CONTRACT_ID,            
  CONTRACT_LOB            
                     
    )                
    values                
    (                
        @CONTRACT_ID,            
  @CURRENT_CONTRACT_LOB            
    )     
        
-------------------------------------------------------------------------                   
-- ADDED BY SANTOH KUMAR GAUTAM ON 21 MARCH 2011    
-- ADD XOL DETAILS IF CONTARCT TYPE IS XOL Per Accident or XOL Per Risk    
-------------------------------------------------------------------------    
 IF(@CONTRACT_TYPE IN (3,4))    
 BEGIN    
   INSERT INTO MNT_XOL_INFORMATION            
  (     
   XOL_ID,    
   CONTRACT_ID,            
   LOB_ID,    
   IS_ACTIVE,    
   LOSS_DEDUCTION,    
   AGGREGATE_LIMIT,    
   MIN_DEPOSIT_PREMIUM,    
   FLAT_ADJ_RATE,    
   REINSTATE_PREMIUM_RATE,       
   PREMIUM_DISCOUNT,       
   CREATED_BY,    
   CREATED_DATETIME       
                      
  )                
  values                
  (                
   @XOL_ID,    
   @CONTRACT_ID,            
   @CURRENT_CONTRACT_LOB ,           
   'Y',    
   0,--LOSS_DEDUCTION,    
   0,--AGGREGATE_LIMIT,    
   0,--MIN_DEPOSIT_PREMIUM,    
   0,--FLAT_ADJ_RATE,    
   0,--REINSTATE_PREMIUM_RATE,          
   0,--PREMIUM_DISCOUNT,    
   @CREATED_BY,    
   @CREATED_DATETIME    
  )     
  SET @XOL_ID=@XOL_ID+1    
 END    
    
    --increment the loss_code_id                   
      SET @CURRENT_CONTRACT_LOB=DBO.PIECE(@CONTRACT_LOB,',',@COUNT)                        
      SET @COUNT=@COUNT+1               
    -- SET @LOSS_CODE_ID=@LOSS_CODE_ID+1                
 END                 
                
-- --if there is no data for insertion, return from the procedure                
-- if (@RISK_EXPOSURE is null) or (@RISK_EXPOSURE='')            
--  set @iERROR = 1               
--                 
DECLARE @CURRENT_RISK_EXPOSURE VARCHAR(20)                
DECLARE @COUNT2 INT                
SET @COUNT2=2                
              
 SET @CURRENT_RISK_EXPOSURE = DBO.PIECE(@RISK_EXPOSURE,',',1)                               
                     
 --Run a loop to go through the list of comma-separated values for insertion                
while @CURRENT_RISK_EXPOSURE is not null                      
 BEGIN                              
  --Insert LossCodesType data              
 --IF NOT EXISTS(SELECT LOSS_CODE_ID FROM CLM_LOSS_CODES WHERE LOB_ID=@LOB_ID AND LOSS_CODE_TYPE=@CURRENT_LOSS_CODE_TYPE)            
    INSERT INTO MNT_REIN_CONTRACT_RISKEXPOSURE                 
    (   CONTRACT_ID,            
  RISK_EXPOSURE            
                     
    )                
    values                
    (                
        @CONTRACT_ID,            
  @CURRENT_RISK_EXPOSURE            
    )              
    --increment the loss_code_id                   
      SET @CURRENT_RISK_EXPOSURE=DBO.PIECE(@RISK_EXPOSURE,',',@COUNT2)                        
 SET @COUNT2=@COUNT2+1               
    -- SET @LOSS_CODE_ID=@LOSS_CODE_ID+1                
 END                
                 
-- --if there is no data for insertion, return from the procedure                
-- if (@STATE_ID is null) or (@STATE_ID='')            
--  set @iERROR = 1              
                
DECLARE @CURRENT_STATE_ID VARCHAR(20)                
DECLARE @COUNT1 INT                
SET @COUNT1=2                
              
 SET @CURRENT_STATE_ID = DBO.PIECE(@STATE_ID,',',1)                               
                     
 --Run a loop to go through the list of comma-separated values for insertion                
while @CURRENT_STATE_ID is not null                      
 BEGIN                              
  --Insert LossCodesType data              
 --IF NOT EXISTS(SELECT LOSS_CODE_ID FROM CLM_LOSS_CODES WHERE LOB_ID=@LOB_ID AND LOSS_CODE_TYPE=@CURRENT_LOSS_CODE_TYPE)            
    INSERT INTO MNT_REIN_CONTRACT_STATE                 
    (          
  CONTRACT_ID,            
  STATE_ID            
                     
    )                
    values                
    (                
        @CONTRACT_ID,            
  @CURRENT_STATE_ID            
    )              
    --increment the loss_code_id                   
      SET @CURRENT_STATE_ID=DBO.PIECE(@STATE_ID,',',@COUNT1)                        
 SET @COUNT1=@COUNT1+1               
    -- SET @LOSS_CODE_ID=@LOSS_CODE_ID+1                
 END                     
          
/*            
if(@iERROR =1)      
begin            
rollback transaction            
end            
else            
commit transaction            
*/            
            
            
END     
    
    