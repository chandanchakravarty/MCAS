IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_ACT_MINIMUM_PREMIUM_AMOUNT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_ACT_MINIMUM_PREMIUM_AMOUNT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                        
Proc Name       : Proc_SAVE_ACT_MINIMUM_PREMIUM_AMOUNT     
Created by      : Pravesh Chandel    
Date            : 27-10-2006      
Purpose         : Saves records in ACT_MINIMUM_PREM_CANCEL     
Revison History :                        
Used In  : Wolverine                        
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/               
--drop proc Proc_SAVE_ACT_MINIMUM_PREMIUM_AMOUNT                    
CREATE  proc dbo.Proc_SAVE_ACT_MINIMUM_PREMIUM_AMOUNT      
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
                        
DECLARE @ROW_ID_MAX smallint       
         
DECLARE @TEMP_ROW_ID int             
            
BEGIN                        
                         
 IF NOT EXISTS                
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
  INSERT INTO ACT_MINIMUM_PREM_CANCEL                        
 	(    
	 ROW_ID,                      
	 LOB_ID ,  
	 SUB_LOB_ID ,  
	 STATE_ID   ,   
	 COUNTRY_ID ,  
	 IS_ACTIVE  ,              
	 EFFECTIVE_FROM_DATE ,  
	 EFFECTIVE_TO_DATE ,  
	 PREMIUM_AMT  ,  
	 CREATED_BY,    
	 CREATED_DATETIME     
 	)                        
 	VALUES                        
 	(      
	 @TEMP_ROW_ID,                    
	 @LOB_ID ,  
	 @SUB_LOB_ID ,  
	 @STATE_ID   ,   
	 @COUNTRY_ID ,  
	 @IS_ACTIVE  ,              
	 @EFFECTIVE_FROM_DATE ,  
	 @EFFECTIVE_TO_DATE ,  
	 @PREMIUM_AMT  ,  
	 @CREATED_BY,    
	 GETDATE()    
	 )                      
 END                         
 ELSE      
 begin
-- set @TEMP_ROW_ID=@ROW_ID
-- RETURN isnull(@TEMP_ROW_ID,0)   
set  @ROW_ID=-1
-- @TEMP_ROW_ID
return -1
 END      
                          
 IF @@ERROR <> 0            
 BEGIN            
   RAISERROR ('Unable to add record.', 16, 1)            
            
 END                 
RETURN isnull(@TEMP_ROW_ID,-1)  
--RETURN @ROW_ID  
                    
END      
      
      
      
      
    
  





GO

