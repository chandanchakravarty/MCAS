IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateREIN_LOSSLAYER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateREIN_LOSSLAYER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*          
CREATED BY   : Swarup         
CREATED DATE  : August 14, 2007        
Purpose   : To implement the Update on the MNT_REIN_LOSSLAYER TABLE         
*/     
--drop PROCEDURE [dbo].[Proc_UpdateREIN_LOSSLAYER]          
CREATE PROCEDURE [dbo].[Proc_UpdateREIN_LOSSLAYER]        
(          
 @LOSS_LAYER_ID INT,        
 @LAYER INT,        
 @COMPANY_RETENTION INT,        
 @LAYER_AMOUNT numeric(12,0),       
 @RETENTION_AMOUNT numeric(12,2),        
 @RETENTION_PERCENTAGE NUMERIC(7,4),        
 @REIN_CEDED numeric(12,2),   
 @REIN_CEDED_PERCENTAGE NUMERIC(7,4),      
 @MODIFIED_BY INT,          
 @LAST_UPDATED_DATETIME DATETIME          
           
          
)          
AS          
BEGIN          
          
UPDATE  MNT_REIN_LOSSLAYER          
SET          
  LOSS_LAYER_ID=@LOSS_LAYER_ID,          
  LAYER=@LAYER,          
  COMPANY_RETENTION=@COMPANY_RETENTION,          
  LAYER_AMOUNT=@LAYER_AMOUNT,         
  RETENTION_AMOUNT=@RETENTION_AMOUNT,          
  RETENTION_PERCENTAGE=@RETENTION_PERCENTAGE,          
  REIN_CEDED=@REIN_CEDED,   
  REIN_CEDED_PERCENTAGE=@REIN_CEDED_PERCENTAGE,       
  MODIFIED_BY=@MODIFIED_BY,          
  LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME          
WHERE          
 LOSS_LAYER_ID=@LOSS_LAYER_ID          
          
END          
      
    
  

GO

