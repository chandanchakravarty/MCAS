IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertREIN_LOSSLAYER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertREIN_LOSSLAYER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*        
CREATED BY   : Swarup         
CREATED DATE  : August 14, 2007        
Purpose   : To implement the Insert on the MNT_REIN_LOSSLAYER TABLE        
*/      
--DROP PROCEDURE [dbo].[Proc_InsertREIN_LOSSLAYER]         
CREATE PROCEDURE [dbo].[Proc_InsertREIN_LOSSLAYER]        
(        
 @LOSS_LAYER_ID INT OUTPUT,      
 @CONTRACT_ID INT,        
 @LAYER INT,        
 @COMPANY_RETENTION INT,        
 @LAYER_AMOUNT numeric(12,0),       
 @RETENTION_AMOUNT numeric(12,2),        
 @RETENTION_PERCENTAGE NUMERIC(7,4),        
 @REIN_CEDED numeric(12,2),   
 @REIN_CEDED_PERCENTAGE NUMERIC(7,4),      
 @CREATED_BY INT,        
 @CREATED_DATETIME DATETIME        
)        
AS        
BEGIN        
SELECT @LOSS_LAYER_ID = IsNull(Max(LOSS_LAYER_ID),0) + 1 FROM MNT_REIN_LOSSLAYER        
        
INSERT INTO  MNT_REIN_LOSSLAYER        
(        
 LOSS_LAYER_ID,      
 CONTRACT_ID,        
 LAYER,      
 COMPANY_RETENTION,      
 LAYER_AMOUNT,      
 RETENTION_AMOUNT,      
 RETENTION_PERCENTAGE,      
 REIN_CEDED,  
 REIN_CEDED_PERCENTAGE,      
 CREATED_BY,        
 CREATED_DATETIME,        
 IS_ACTIVE        
 )        
VALUES        
(        
 @LOSS_LAYER_ID,      
 @CONTRACT_ID,        
 @LAYER,      
 @COMPANY_RETENTION,      
 @LAYER_AMOUNT,      
 @RETENTION_AMOUNT,      
 @RETENTION_PERCENTAGE,      
 @REIN_CEDED,  
 @REIN_CEDED_PERCENTAGE,        
 @CREATED_BY,        
 @CREATED_DATETIME,        
 'Y'        
)         
        
END        
      
    
  
  




GO

