IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAuthorityLimits]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAuthorityLimits]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------              
Proc Name       : dbo.Proc_GetAuthorityLimits        
Created by      : Sumit Chhabra            
Date            : 25/04/2006              
Purpose         : Fetch Authority Limits         
Revison History :              
Used In        : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
--DROP PROC dbo.Proc_GetAuthorityLimits        
CREATE PROC [dbo].[Proc_GetAuthorityLimits]        
        
AS              
BEGIN              
 SELECT       
  cast(ISNULL(LIMIT_ID,'') as varchar) + '^' +    
  case ISNULL(cast(PAYMENT_LIMIT AS DECIMAL(18,2)),0) WHEN 0 THEN '' ELSE CAST(CAST(PAYMENT_LIMIT AS DECIMAL(18,2)) AS VARCHAR) END + '^' +   
  case ISNULL(CAST(RESERVE_LIMIT AS DECIMAL(18,2)),0) WHEN 0 THEN '' ELSE CAST(CAST(RESERVE_LIMIT AS DECIMAL(18,2)) AS VARCHAR) END      
  AS LIMIT_ID,      
  AUTHORITY_LEVEL       
 FROM       
  CLM_AUTHORITY_LIMIT         
 WHERE IS_ACTIVE='Y'  
 ORDER BY      
  CLM_AUTHORITY_LIMIT.LIMIT_ID    
END     
  






GO

