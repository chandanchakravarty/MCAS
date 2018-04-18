IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLMNT_REINSURANCE_CONTRACTTYPE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLMNT_REINSURANCE_CONTRACTTYPE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   
-- drop PROC Dbo.Proc_GetXMLMNT_REINSURANCE_CONTRACTTYPE 
/*----------------------------------------------------------  
Created by      : Deepak Batra  
Purpose     : Evaluation for the Contract Type screen  
Date  : 16 Jan 2006  
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
  
CREATE PROC Dbo.Proc_GetXMLMNT_REINSURANCE_CONTRACTTYPE  
(  
@CONTRACTTYPEID INT  
)  
AS  
BEGIN  
  
SELECT  
 CONTRACTTYPEID,  
 CONTRACT_TYPE_DESC,  
 ISNULL(CREATED_BY,0) CREATED_BY,  
 CONVERT(VARCHAR,CREATED_DATETIME,101) CREATED_DATETIME,  
 ISNULL(MODIFIED_BY,0) MODIFIED_BY,  
 CONVERT(VARCHAR,LAST_UPDATED_DATETIME,101) LAST_UPDATED_DATETIME,        
 ISNULL(IS_ACTIVE,'') IS_ACTIVE  
  
  
FROM  MNT_REINSURANCE_CONTRACT_TYPE  
  
WHERE  CONTRACTTYPEID = @CONTRACTTYPEID  
   
END  
  
  


GO

