IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_GETXML_CONSTRUCTIONTRANSLATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_GETXML_CONSTRUCTIONTRANSLATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*----------------------------------------------------------                
Proc Name       : dbo.[MNT_REIN_UPDATE_CONSTRUCTIONTRANSLATION]                
Created by      : Harmanjeet Singh               
Date            : April 27, 2007              
Purpose         : To insert the data into Reinsurer Construction Translation table.                
Revison History :                
Used In         : Wolverine         
               
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
CREATE PROC [dbo].[Proc_MNT_REIN_GETXML_CONSTRUCTIONTRANSLATION]                
(                
               
	@REIN_CONSTRUCTION_CODE_ID	int 
	
		
  )                
AS                
BEGIN      
                
             
    SELECT

	ISNULL(REIN_CONSTRUCTION_CODE_ID,0) REIN_CONSTRUCTION_CODE_ID,

	ISNULL(REIN_EXTERIOR_CONSTRUCTION,'') REIN_EXTERIOR_CONSTRUCTION,
	ISNULL(REIN_DESCRIPTION,'') REIN_DESCRIPTION,      
	ISNULL(REIN_REPORT_CODE,'') REIN_REPORT_CODE,
	ISNULL(REIN_NISS,'') REIN_NISS,      
	ISNULL(CREATED_BY,0) CREATED_BY,
	CONVERT(VARCHAR,CREATED_DATETIME,101) CREATED_DATETIME,
	ISNULL(MODIFIED_BY,0) MODIFIED_BY,
	CONVERT(VARCHAR,LAST_UPDATED_DATETIME,101) LAST_UPDATED_DATETIME,      
	ISNULL(IS_ACTIVE,'') IS_ACTIVE

	FROM MNT_REIN_CONSTRUCTION_TRANSLATION
    
	WHERE
	REIN_CONSTRUCTION_CODE_ID=@REIN_CONSTRUCTION_CODE_ID;
	
	              
  END


GO

