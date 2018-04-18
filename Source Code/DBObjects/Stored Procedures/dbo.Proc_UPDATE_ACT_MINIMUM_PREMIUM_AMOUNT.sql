IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_ACT_MINIMUM_PREMIUM_AMOUNT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_ACT_MINIMUM_PREMIUM_AMOUNT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                        
Proc Name       : Proc_UPDATE_ACT_MINIMUM_PREMIUM_AMOUNT     
Created by      : Pravesh Chandel    
Date            : 27-10-2006      
Purpose         : Saves records in ACT_MINIMUM_PREM_CANCEL     
Revison History :                        
Used In  : Wolverine                        
------------------------------------------------------------                        
Date     	Review By     Comments                        

12th-Jul-06 Swastika	  Conditions were not appropriate	
------   ------------       -------------------------*/               
--drop proc Proc_UPDATE_ACT_MINIMUM_PREMIUM_AMOUNT                    
CREATE  proc dbo.Proc_UPDATE_ACT_MINIMUM_PREMIUM_AMOUNT      
(                        
 @ROW_ID INT OUTPUT,  
 @LOB_ID     int,                        
 @SUB_LOB_ID     int,                        
 @STATE_ID     smallint,            
 @COUNTRY_ID   SMALLINT,  
 @IS_ACTIVE    NCHAR(1),              
 @EFFECTIVE_FROM_DATE DATETIME,                        
 @EFFECTIVE_TO_DATE DATETIME,                        
 @PREMIUM_AMT DECIMAL(12,2),  
 @CREATED_BY int=null,    
 @CREATED_DATETIME datetime=null,    
 @MODIFIED_BY int= null,    
 @LAST_UPDATED_DATETIME datetime=null                               
)                        
AS                        
                        
DECLARE @ROW_ID_MAX SMALLINT       
DECLARE @TEMP_ROW_ID INT             
DECLARE @COUNT INT
            
BEGIN                        
   
   
SET @COUNT = (SELECT COUNT(*) FROM ACT_MINIMUM_PREM_CANCEL                
WHERE LOB_ID = @LOB_ID and                         
SUB_LOB_ID=@SUB_LOB_ID and                         
STATE_ID = @STATE_ID  and  
COUNTRY_ID=@COUNTRY_ID                        
and EFFECTIVE_FROM_DATE = @EFFECTIVE_FROM_DATE       
and EFFECTIVE_TO_DATE   = @EFFECTIVE_TO_DATE
and ROW_ID <> @ROW_ID)  

 IF(@COUNT=0)  
 BEGIN  
	-- checking if to date overlaps with any record of same group  
	-- checking if from date overlaps with any record of same group  
	  IF EXISTS                
	  (                
	   SELECT LOB_ID FROM ACT_MINIMUM_PREM_CANCEL                
	   WHERE LOB_ID = @LOB_ID and                         
	   SUB_LOB_ID=@SUB_LOB_ID and                         
	   STATE_ID = @STATE_ID  and  
	   COUNTRY_ID=@COUNTRY_ID                        
	   AND (  
			(@EFFECTIVE_FROM_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)OR     
			(@EFFECTIVE_TO_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)OR    
			(@EFFECTIVE_FROM_DATE  < EFFECTIVE_FROM_DATE AND @EFFECTIVE_TO_DATE > EFFECTIVE_TO_DATE)  
	        )   
	   AND ROW_ID <> @ROW_ID  
	 )  
	BEGIN    
	  SELECT @ROW_ID = -1 --DATES OVERLAP   
	  RETURN -- EXITING PROCEDURE    
	END  

	UPDATE ACT_MINIMUM_PREM_CANCEL
	SET
	LOB_ID = @LOB_ID,
	SUB_LOB_ID = @SUB_LOB_ID,
	STATE_ID = @STATE_ID,
	COUNTRY_ID = @COUNTRY_ID,
	EFFECTIVE_FROM_DATE = @EFFECTIVE_FROM_DATE,
	EFFECTIVE_TO_DATE =  @EFFECTIVE_TO_DATE,
	PREMIUM_AMT = @PREMIUM_AMT
	WHERE ROW_ID = @ROW_ID

	 IF @@ERROR <> 0            
	 BEGIN            
	   RAISERROR ('Unable to add record.', 16, 1)            
	 END       
 ELSE  
	BEGIN  
		SELECT ROW_ID = -1  
		RETURN  
	END   
 END  

END  

                  
/*
    
 IF EXISTS                
  (                
   SELECT LOB_ID FROM ACT_MINIMUM_PREM_CANCEL                
   WHERE LOB_ID = @LOB_ID and                         
   SUB_LOB_ID=@SUB_LOB_ID and                         
   STATE_ID = @STATE_ID  and  
   COUNTRY_ID=@COUNTRY_ID                        
   AND (  
--        (EFFECTIVE_FROM_DATE>= @EFFECTIVE_FROM_DATE AND EFFECTIVE_TO_DATE<= @EFFECTIVE_TO_DATE) OR  
--        (EFFECTIVE_FROM_DATE<= @EFFECTIVE_FROM_DATE AND EFFECTIVE_TO_DATE>= @EFFECTIVE_TO_DATE) OR   
--        (EFFECTIVE_FROM_DATE>= @EFFECTIVE_FROM_DATE AND EFFECTIVE_TO_DATE>= @EFFECTIVE_TO_DATE) OR   
--        (EFFECTIVE_FROM_DATE<= @EFFECTIVE_FROM_DATE AND EFFECTIVE_TO_DATE<= @EFFECTIVE_TO_DATE)   
		(@EFFECTIVE_FROM_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)OR     
		(@EFFECTIVE_TO_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)OR    
		(@EFFECTIVE_FROM_DATE  < EFFECTIVE_FROM_DATE AND @EFFECTIVE_TO_DATE > EFFECTIVE_TO_DATE)  
        )   
  )                        
 BEGIN                        
                          
 	 SELECT @TEMP_ROW_ID=ISNULL(MAX(ROW_ID),0)+1 FROM ACT_MINIMUM_PREM_CANCEL                     
	SET @ROW_ID=@TEMP_ROW_ID  
	
  UPDATE ACT_MINIMUM_PREM_CANCEL                        
	   SET PREMIUM_AMT=@PREMIUM_AMT,  
--	 	IS_ACTIVE = @IS_ACTIVE,              
	        MODIFIED_BY = MODIFIED_BY,    
	   LAST_UPDATED_DATETIME=  GETDATE()    
    

	 WHERE 
	  LOB_ID= @LOB_ID AND                        
	   SUB_LOB_ID = @SUB_LOB_ID AND                        
	   STATE_ID= @STATE_ID   AND   
	          COUNTRY_ID= @COUNTRY_ID 

and   
	 (  
-- 	       (EFFECTIVE_FROM_DATE>= @EFFECTIVE_FROM_DATE AND EFFECTIVE_TO_DATE<= @EFFECTIVE_TO_DATE) OR  
-- 	       (EFFECTIVE_FROM_DATE<= @EFFECTIVE_FROM_DATE AND EFFECTIVE_TO_DATE>= @EFFECTIVE_TO_DATE) OR   
-- 	       (EFFECTIVE_FROM_DATE>= @EFFECTIVE_FROM_DATE AND EFFECTIVE_TO_DATE>= @EFFECTIVE_TO_DATE) OR   
-- 	       (EFFECTIVE_FROM_DATE<= @EFFECTIVE_FROM_DATE AND EFFECTIVE_TO_DATE<= @EFFECTIVE_TO_DATE)   
		(@EFFECTIVE_FROM_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)OR     
		(@EFFECTIVE_TO_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)OR    
		(@EFFECTIVE_FROM_DATE  < EFFECTIVE_FROM_DATE AND @EFFECTIVE_TO_DATE > EFFECTIVE_TO_DATE)  
	        )    


set @TEMP_ROW_ID=@ROW_ID
-- RETURN isnull(@TEMP_ROW_ID,0)   
set  @ROW_ID= @TEMP_ROW_ID
 END      
                   
 IF @@ERROR <> 0            
 BEGIN            
   RAISERROR ('Unable to add record.', 16, 1)            
            
 END                 
RETURN isnull(@TEMP_ROW_ID,0)  
--RETURN @ROW_ID  
                    
END      
      
      
  */    
      
    
  







GO

