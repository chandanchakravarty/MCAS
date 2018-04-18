IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertClaimVictim]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertClaimVictim]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

          
 /*----------------------------------------------------------                                                                  
Proc Name             : Proc_InsertClaimVictim                                                                  
Created by            : Santosh Kumar Gautam                                                                 
Date                  : 09 Feb 2011                                                                 
Purpose               : To add new victim in claim
Revison History       :                                                                  
Used In               : claim module                        
------------------------------------------------------------                                                                  
Date     Review By          Comments                                     
                            
drop Proc Proc_InsertClaimVictim 599,0,N'TEST',90,14863,'02/24/2011',406,'PASS'                                                       
------   ------------       -------------------------*/                                                                  
--                                     
                                      
--                                   
                                
CREATE PROCEDURE [dbo].[Proc_InsertClaimVictim]                                      
                                     
                     
 @CLAIM_ID          int                        
,@VICTIM_ID         int OUT          
,@NAME              nvarchar(256)   
,@STATUS            int     
,@INJURY_TYPE       int
,@CREATED_DATETIME  datetime  
,@CREATED_BY        int 
,@PAGE_MODE         VARCHAR(20) 
 
         
                                      
AS                                      
BEGIN   
  
   DECLARE @NUM_OF_PASS INT=0
   DECLARE @VICTIM_COUNT INT=0
   DECLARE @RISK_ID INT=0

    
   ------------------------------------------------------
   -- RESERVE IS CREATED SO NO MORE COVERAGE CAN ADDED
   ------------------------------------------------------
  IF(EXISTS(SELECT CLAIM_ID FROM CLM_ACTIVITY_RESERVE WITH(NOLOCK) WHERE CLAIM_ID= @CLAIM_ID))       
  BEGIN
  
   
	  SET @VICTIM_ID=-4 
	  RETURN  
  END 
  
  
   ------------------------------------------------------
   -- IF RISK IS NOT ADDED IN CLAIM THEN RETURN ERROR
   ------------------------------------------------------
  SELECT @RISK_ID= INSURED_PRODUCT_ID     
  FROM [dbo].[CLM_INSURED_PRODUCT]  WITH(NOLOCK)        
  WHERE CLAIM_ID=@CLAIM_ID             
    
    
   IF(@RISK_ID IS NULL OR @RISK_ID='' )                    
   BEGIN       
       SET @VICTIM_ID=-6    
       RETURN  
   END      
  
  
  ----------------------------------------------------------------
  -- FOR PRODUCT PERSOANL ACCIDENT FOR PASSENGERS
  -- USER CAN ADD PASSENGER MAXIMUM GIVEN IN RISK INFORMATION PAGE
  -----------------------------------------------------------------
   IF(@PAGE_MODE='PASS') 
   BEGIN
     SELECT @NUM_OF_PASS= PA_NUM_OF_PASS FROM CLM_INSURED_PRODUCT WITH(NOLOCK) WHERE CLAIM_ID=@CLAIM_ID     
     SELECT @VICTIM_COUNT= COUNT(VICTIM_ID) FROM [CLM_VICTIM_INFO] WITH(NOLOCK) WHERE CLAIM_ID=@CLAIM_ID
     
     
     IF (@VICTIM_COUNT>=@NUM_OF_PASS)
      BEGIN
    
		  SET @VICTIM_ID=-5 
		  RETURN  
	  END
   
   END
 
   SELECT @VICTIM_ID=(ISNULL(MAX([VICTIM_ID]),0))+1  FROM CLM_VICTIM_INFO  WITH(NOLOCK)                  
     


		INSERT INTO [CLM_VICTIM_INFO]   
		(  
			CLAIM_ID,                   
			VICTIM_ID,  
			NAME,  
			[STATUS],    
			INJURY_TYPE,        		
			IS_ACTIVE,  
			CREATED_BY,  
			CREATED_DATETIME  

		)  
		VALUES  
		(  
			 @CLAIM_ID           
			,@VICTIM_ID   
			,@NAME      
			,@STATUS   
			,@INJURY_TYPE		
			,'Y' --IS_ACTIVE,  
			,@CREATED_BY  
			,@CREATED_DATETIME  


		)        

  
     
END 
GO

