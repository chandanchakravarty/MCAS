IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETPOLWATERCRAFT_TRAILERIDS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETPOLWATERCRAFT_TRAILERIDS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name   : dbo.Proc_GetPolWATERCRAFT_TRAILERIDs      
Created by  : Shafi 
Date        : 16 JUNE,2006     
Purpose     : Get the WATERCRAFT TRAILER IDs                 
Revison History  :                      
 ------------------------------------------------------------                            
Date     Review By          Comments                          
                 
------   ------------       -------------------------*/                
CREATE PROCEDURE dbo.PROC_GETPOLWATERCRAFT_TRAILERIDS      
(      
 @CUSTOMER_ID INT,      
 @POLICY_ID INT,      
 @POLICY_VERSION_ID INT      
)          
AS               
BEGIN                
      
SELECT   TRAILER_ID      
FROM       POL_WATERCRAFT_TRAILER_INFO
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND IS_ACTIVE='Y'
   ORDER BY   TRAILER_ID
                  
END     
  



GO

