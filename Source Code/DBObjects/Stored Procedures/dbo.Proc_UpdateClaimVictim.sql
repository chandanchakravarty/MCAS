IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateClaimVictim]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateClaimVictim]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


          
 /*----------------------------------------------------------                                                                  
Proc Name             : Proc_UpdateClaimVictim                                                                  
Created by            : Santosh Kumar Gautam                                                                 
Date                  : 09 Feb 2011                                                                 
Purpose               : To update victim info in claim  
Revison History       :                                                                  
Used In               : claim module                        
------------------------------------------------------------                                                                  
Date     Review By          Comments                                     
                            
drop Proc Proc_UpdateClaimVictim                                                        
------   ------------       -------------------------*/                                                                  
--                                     
                                      
--                                   
                                
CREATE PROCEDURE [dbo].[Proc_UpdateClaimVictim]                                      
                                     
                     
 @CLAIM_ID          int                        
,@VICTIM_ID         int OUT          
,@NAME              nvarchar(256)   
,@STATUS            int     
,@INJURY_TYPE       int
,@MODIFIED_BY       int  
,@LAST_UPDATED_DATETIME      datetime     
,@ERROR_CODE				 INT OUT 
    
  
         
                                      
AS                                      
BEGIN   
  
IF(EXISTS(SELECT CLAIM_ID FROM CLM_ACTIVITY_RESERVE  WHERE CLAIM_ID= @CLAIM_ID))       
 BEGIN
  SET @ERROR_CODE=-4
  RETURN
 END
  
 
   UPDATE CLM_VICTIM_INFO
   SET NAME					= @NAME,    
    [STATUS]				= @STATUS,    
    INJURY_TYPE				= @INJURY_TYPE,
    MODIFIED_BY				= @MODIFIED_BY  ,  
    LAST_UPDATED_DATETIME   = @LAST_UPDATED_DATETIME  
   WHERE (CLAIM_ID=@CLAIM_ID AND VICTIM_ID=@VICTIM_ID)      
 
  SET @ERROR_CODE=0      
       
END 

GO

