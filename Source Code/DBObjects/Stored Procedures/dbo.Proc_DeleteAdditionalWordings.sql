IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAdditionalWordings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAdditionalWordings]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------      
Proc Name       : dbo.Proc_DeleteAdditionalWordings      
Created by      :  Swarup Pal     
Date                :  22-Feb-2007     
Purpose         :  To delete data from MNT_PROCESS_WORDINGS     
Revison History :      
Used In         :    Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
-- drop proc dbo.Proc_DeleteAdditionalWordings    
CREATE     PROC dbo.Proc_DeleteAdditionalWordings       
(  
 @WORDINGS_ID     int                
)                
AS   
BEGIN   
BEGIN TRAN                                                                        
   DECLARE @TEMP_ERROR_CODE INT      
  
IF EXISTS( SELECT * FROM  MNT_PROCESS_WORDINGS WHERE WORDINGS_ID = @WORDINGS_ID)  
BEGIN  
 DELETE FROM MNT_PROCESS_WORDINGS  
  WHERE WORDINGS_ID = @WORDINGS_ID  
END  
  
SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM    
  
                     
  COMMIT TRAN                                             
   RETURN 1        
                                                               
         
                                                                                                       
  PROBLEM:                                                          
   IF (@TEMP_ERROR_CODE <> 0)                                              
    BEGIN                                                                                                      
      ROLLBACK TRAN                                                                                                       
      RETURN -1        
    END                                                                                                      
                
END      


GO

