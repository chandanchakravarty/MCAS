IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetExpertServiceProviderTypes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetExpertServiceProviderTypes]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_GetExpertServiceProviderTypes  
Created by      : Sumit Chhabra  
Date            : 21/04/2006    
Purpose        : Fetch Expert Service Providers Type from Master Table  
Revison History :    
Used In        : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC Dbo.Proc_GetExpertServiceProviderTypes
   
AS    
BEGIN    
--TYPE_ID=9=Service Types  
SELECT DETAIL_TYPE_ID,DETAIL_TYPE_DESCRIPTION FROM CLM_TYPE_DETAIL WHERE TYPE_ID=9    
END  
  



GO

