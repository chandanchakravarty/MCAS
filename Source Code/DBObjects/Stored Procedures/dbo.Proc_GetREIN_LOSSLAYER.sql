IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetREIN_LOSSLAYER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetREIN_LOSSLAYER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : Proc_GetREIN_LOSSLAYER          
Created by      : Swarup            
Date            : 14-Aug-2007           
Purpose      : Get values from  MNT_REIN_LOSSLAYER table            
Revison History :            
Used In  : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
        
-- drop proc dbo.Proc_GetREIN_LOSSLAYER 1,52          
CREATE PROC dbo.Proc_GetREIN_LOSSLAYER             
(            
 @LOSS_LAYER_ID      int ,    
 @CONTRACT_ID int           
)            
AS            
BEGIN            
 SELECT            
  LOSS_LAYER_ID,        
  LAYER,    
  COMPANY_RETENTION,    
  LAYER_AMOUNT,    
  RETENTION_AMOUNT,    
  RETENTION_PERCENTAGE,    
  REIN_CEDED,
  REIN_CEDED_PERCENTAGE,    
  IS_ACTIVE    
         
 FROM       
 MNT_REIN_LOSSLAYER        
 WHERE             
 LOSS_LAYER_ID = @LOSS_LAYER_ID    AND     
CONTRACT_ID = @CONTRACT_ID           
            
END            
            
     
    
    
    
  
   
    


GO

