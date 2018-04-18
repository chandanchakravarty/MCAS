IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCurrencyRate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCurrencyRate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
  
/*----------------------------------------------------------                            
Proc Name      : dbo.[Proc_GetCurrencyRate]                            
Created by       : praveer panghal                         
Date             : 22-12-2010                            
Purpose       : retrieving data from MNT_CURRENCY_RATE_MASTER                            
Revison History :                   
Modify by       :                            
Date             :                            
Purpose       :           
                         
Used In        : Ebix Advantage                        
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                            
--drop proc dbo.Proc_GetCurrencyRate  
  
CREATE PROCEDURE [dbo].[Proc_GetCurrencyRate]   
@CRR_RATE_ID INT  
AS  
BEGIN   
SELECT  
	 CRR_RATE_ID,  
	 CURRENCY_ID, 
	 RATE,
	 RATE_EFFETIVE_FROM,  
	 RATE_EFFETIVE_TO, 
	 IS_ACTIVE,  
	 CREATED_BY,  
	 CREATED_DATETIME,  
	 MODIFIED_BY,  
	 LAST_UPDATED_DATETIME  
 
 LEVEL  
 FROM MNT_CURRENCY_RATE_MASTER WITH(NOLOCK)   
 WHERE CRR_RATE_ID=@CRR_RATE_ID  
END 
GO

