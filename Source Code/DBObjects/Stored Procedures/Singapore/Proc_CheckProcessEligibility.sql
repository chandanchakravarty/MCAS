  --DECLARE @RETVAL int
  --exec Proc_CheckProcessEligibility  28718,159,4,18  ,@RETVAL OUTPUT 
  --PRINT @RETVAL
  
 --BEGIN TRAN       
 --DROP PROC  dbo.Proc_CheckProcessEligibility                        
 --GO      
/*----------------------------------------------------------                        
Proc Name        : dbo.Proc_CheckProcessEligibility                        
Created by        : Vijay Arora                
Date                : 22-12-2005                
Purpose          : Check that the specific process can be launched or not.                
Revison History  :          
Modified BY   : Pravesh K Chandel        
Used In          :   Wolverine                        
------------------------------------------------------------                        
Date     Review By          Comments               
DROP PROC dbo.Proc_CheckProcessEligibility    28718,159,4,18  ,@RETVAL           
------   ------------       -------------------------*/                        
ALTER PROC [dbo].[Proc_CheckProcessEligibility]                        
(                        
 @CUSTOMER_ID     int,                        
 @POLICY_ID     int,                        
 @POLICY_VERSION_ID     smallint,                        
 @PROCESS_ID int,                
 @RETVAL int OUTPUT                
)                        
AS                        
                        
BEGIN                        
       DECLARE @CURRENT_TRRM INT      
        SELECT @CURRENT_TRRM = CURRENT_TERM FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE       
        CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID      
              
             
              
 IF ( SELECT MAX(POLICY_VERSION_ID)              
    FROM POL_CUSTOMER_POLICY_LIST  WITH(NOLOCK)        
    WHERE POLICY_ID = @POLICY_ID AND CUSTOMER_ID = @CUSTOMER_ID  -- AND POLICY_STATUS<>'UNWTRRENEWD'            
     --changed by Lalit process should eligible on previous term,itrack # 1209 for auto cancellation      
    AND CURRENT_TERM = @CURRENT_TRRM      
    ) <> @POLICY_VERSION_ID              
  BEGIN         
 --Ravindra(04-03-3008): Set @RETVAL with proper error code         
 SET @RETVAL =    2        
 RETURN 2 --Can not launch process on previous version              
  END        
---- BY PRAVESH- IF ANY PENDING PROCESS CAN'T LAUNCH NEW PROCESS        
IF EXISTS(SELECT PPP.POLICY_VERSION_ID              
  FROM POL_POLICY_PROCESS PPP WITH(NOLOCK)        
  JOIN POL_CUSTOMER_POLICY_LIST PCPL WITH(NOLOCK)  ON      
  PPP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND       
  PPP.POLICY_ID = PCPL.POLICY_ID AND       
  PPP.NEW_POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID        
  WHERE PPP.POLICY_ID = @POLICY_ID AND PPP.CUSTOMER_ID = @CUSTOMER_ID          
  AND PPP.PROCESS_STATUS='PENDING' AND PCPL.CURRENT_TERM = @CURRENT_TRRM          
    )            
 AND @PROCESS_ID IN (2,3,4,5,6,8,24,28,31,35)        
  BEGIN         
 SET @RETVAL =    2        
 RETURN 2 --Can not launch process         
  END        
              
                
  DECLARE @APPLICABLE_TO VARCHAR(200)                
  DECLARE @POLICY_STATUS VARCHAR(100)                
                
  SELECT @APPLICABLE_TO = APPLICABLE_TO FROM POL_PROCESS_MASTER WHERE PROCESS_ID = @PROCESS_ID                
                 
  SELECT @POLICY_STATUS = UPPER(POLICY_STATUS) FROM POL_CUSTOMER_POLICY_LIST with(nolock)                
  WHERE CUSTOMER_ID = @CUSTOMER_ID                
          AND POLICY_ID = @POLICY_ID                
          AND POLICY_VERSION_ID = @POLICY_VERSION_ID                
   --       AND IS_ACTIVE = 'Y'                 
                
              
  IF PATINDEX (  '%' + @POLICY_STATUS + '%', @APPLICABLE_TO) <> 0                 
  SET @RETVAL = 1                
  ELSE                
    SET @RETVAL = 2                
              
               
 RETURN @RETVAL                  
END              
        
--  GO      
--DECLARE @RET_VAL INT      
--exec Proc_CheckProcessEligibility 2813,43,2,2,@RET_VAL OUT      
--SELECT @RET_VAL                      
--  ROLLBACK TRAN  