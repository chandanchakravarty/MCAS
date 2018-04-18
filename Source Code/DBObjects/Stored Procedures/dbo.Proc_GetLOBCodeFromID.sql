IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLOBCodeFromID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLOBCodeFromID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*  
----------------------------------------------------------      
Proc Name       : Charles Gomes  
Created by      : Proc_GetLOBCodeFromID  
Date            : 07-11-2005  
Purpose      : To Get the LOB Code from LOB_ID  
Revison History :      
Used In   : Wolverine      

DROP PROC Dbo.Proc_GetLOBCodeFromID 
      
------------------------------------------------------------      
Date        Review By          Comments      
------   ------------       -------------------------*/      
CREATE  PROC [dbo].[Proc_GetLOBCodeFromID]      
@LOB_ID INT  
AS      
BEGIN         
SELECT LOB_CODE FROM MNT_LOB_MASTER WITH(NOLOCK) WHERE LOB_ID = @LOB_ID    
END      

GO

