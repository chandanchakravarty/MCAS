IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateMNT_PROCESS_WORDINGS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateMNT_PROCESS_WORDINGS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                    
Proc Name    : dbo.Proc_ActivateDeactivateMNT_PROCESS_WORDINGS                  
Created by   : Swarup Pal              
Date         : 22 Feb 2007                  
Purpose      :                 
Revison History :                
Used In  :   Wolverine                       
 ------------------------------------------------------------                                
Date     Review By          Comments                              
                     
------   ------------       -------------------------*/                   
              
--drop PROC dbo.Proc_ActivateDeactivateMNT_PROCESS_WORDINGS              
create PROC dbo.Proc_ActivateDeactivateMNT_PROCESS_WORDINGS        
(                
 @WORDINGS_ID int,      
 @IS_ACTIVE NCHAR(2)              
                     
)                
AS                
BEGIN     
UPDATE MNT_PROCESS_WORDINGS SET IS_ACTIVE=@IS_ACTIVE           
 WHERE WORDINGS_ID=@WORDINGS_ID             
END              
            
        
    
      
    
    
    


GO

