IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCurrencyRate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCurrencyRate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name       : dbo.Proc_UpdateCurrencyRate                
Created by      :praveer panghal      
Date            : 22/12/2010                
Purpose   :To Update records in MNT_CURRENCY_RATE_MASTER table.                
Revison History :                
Used In   : Ebix Advantage                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
--DROP PROC dbo.Proc_UpdateCurrencyRate     
    
CREATE PROC [dbo].[Proc_UpdateCurrencyRate]     
(      
@CRR_RATE_ID int OUTPUT ,
@CURRENCY_ID INT ,
@RATE DECIMAL(7,4)=NULL,
@RATE_EFFETIVE_FROM DATETIME=NULL,
@RATE_EFFETIVE_TO DATETIME=NULL,
@IS_ACTIVE NCHAR(1)=NULL, 
@MODIFIED_BY INT=NULL,  
@LAST_UPDATED_DATETIME DATETIME=NULL  
)    
AS    
BEGIN    
IF EXISTS(SELECT RATE FROM MNT_CURRENCY_RATE_MASTER with(nolock) WHERE RATE_EFFETIVE_FROM >= 
convert(datetime,@RATE_EFFETIVE_FROM) AND RATE_EFFETIVE_FROM<=convert(datetime,@RATE_EFFETIVE_TO)
 AND CURRENCY_ID=@CURRENCY_ID and CRR_RATE_ID<>@CRR_RATE_ID)                                                                                                                                                                      
    BEGIN  
      SET @CRR_RATE_ID=-2  
      RETURN  
    END    
UPDATE MNT_CURRENCY_RATE_MASTER    
SET    
CURRENCY_ID=@CURRENCY_ID,
RATE=@RATE,
RATE_EFFETIVE_FROM=@RATE_EFFETIVE_FROM,
RATE_EFFETIVE_TO=@RATE_EFFETIVE_TO,
IS_ACTIVE =@IS_ACTIVE,
MODIFIED_BY =@MODIFIED_BY, 
LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME
      
 WHERE    
 CRR_RATE_ID=@CRR_RATE_ID    
     
END
GO

