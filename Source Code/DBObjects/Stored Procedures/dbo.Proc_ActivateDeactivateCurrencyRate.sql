IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateCurrencyRate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateCurrencyRate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
 /*----------------------------------------------------------                
Proc Name       : dbo.Proc_ActivateDeactivateCurrencyRate              
Created by      : praveer panghal     
Date            : 23/12/2010                
Purpose       :To Activate and deactivate records in MNT_CURRENCY_RATE_MASTER table.                
Revison History :                
Used In        : Ebix Advantage                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
--DROP PROC dbo.Proc_ActivateDeactivateCurrencyRate       
      
      
CREATE  PROC [dbo].[Proc_ActivateDeactivateCurrencyRate]    
(              
 @CRR_RATE_ID Int,                     
 @IS_ACTIVE   NChar(1)              
)              
AS              
BEGIN    
    
UPDATE MNT_CURRENCY_RATE_MASTER          
 SET               
    IS_ACTIVE = @IS_ACTIVE             
 WHERE              
    CRR_RATE_ID=@CRR_RATE_ID  
    return 1            
END 
GO

