IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetWATERCRAFT_TRAILERIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetWATERCRAFT_TRAILERIDs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name   : dbo.Proc_GetWATERCRAFT_TRAILERIDs      
Created by  : Shafi 
Date        : 16 JUNE,2006     
Purpose     : Get the WATERCRAFT TRAILER IDs                 
Revison History  :                      
 ------------------------------------------------------------                            
Date     Review By          Comments                          
                 
------   ------------       -------------------------*/                
CREATE PROCEDURE dbo.Proc_GetWATERCRAFT_TRAILERIDs      
(      
 @CUSTOMER_ID int,      
 @APP_ID int,      
 @APP_VERSION_ID int      
)          
AS               
BEGIN                
      
SELECT   TRAILER_ID      
FROM       APP_WATERCRAFT_TRAILER_INFO
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID   
   ORDER BY   TRAILER_ID
                  
End     
  



GO

