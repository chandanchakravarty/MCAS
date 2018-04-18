IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_CLM_DELETEDRIVER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_CLM_DELETEDRIVER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                                                                                                                                          
Proc Name       : dbo.PROC_CLM_DELETEDRIVER                                                                                                                                                                              
Created by      : Sibin Thomas Philip                                                                                                                                                                                        
Date            : 8/09/2009                                                                                                                                                                                          
Purpose         : DELETING CLAIM DRIVER Details                                                                                                                                                     
Created by      : Sibin Thomas Philip                                                                                                                                                                                         
Revison History :                                                                                                                                                                                          
Used In        : Wolverine                                                                                                                                                                                          
                 
------------------------------------------------------------                          
Date     Review By          Comments                                                                                                                                                                                          
------   ------------       -------------------------*/                                                                                                                                                                                          
--DROP PROC dbo.PROC_CLM_DELETEDRIVER                                                                                                                                                              
CREATE   PROC dbo.PROC_CLM_DELETEDRIVER                                                                                                                                                                      
@CLAIM_ID int,                                                             
@DRIVER_ID int              
AS                                                                                                                            
DECLARE @RESULT INT                                                             
BEGIN     
    
 IF EXISTS(SELECT 1 FROM CLM_DRIVER_INFORMATION WHERE CLAIM_ID=@CLAIM_ID AND DRIVER_ID=@DRIVER_ID)          
  BEGIN          
   DELETE CLM_DRIVER_INFORMATION WHERE CLAIM_ID=@CLAIM_ID AND DRIVER_ID=@DRIVER_ID       
   SET @RESULT = 1        
  END        
  ELSE
  BEGIN        
   SET @RESULT = 0        
  END 
   
  RETURN @RESULT      
             
 END
GO

