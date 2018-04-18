IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_UPDATE_CONSTRUCTIONTRANSLATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_UPDATE_CONSTRUCTIONTRANSLATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


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
CREATE PROC [dbo].[Proc_MNT_REIN_UPDATE_CONSTRUCTIONTRANSLATION]                
(                
               
	@REIN_CONSTRUCTION_CODE_ID	int ,
	@REIN_EXTERIOR_CONSTRUCTION	nvarchar(50),
	@REIN_DESCRIPTION	nvarchar(50),
	@REIN_REPORT_CODE	nvarchar(50),
	@REIN_NISS	NCHAR(1),
	@MODIFIED_BY	INT,
	@LAST_UPDATED_DATETIME	DATETIME
	
		
  )                
AS                
BEGIN      
                
             
   UPDATE MNT_REIN_CONSTRUCTION_TRANSLATION                
   
	SET
    REIN_CONSTRUCTION_CODE_ID=@REIN_CONSTRUCTION_CODE_ID	,
	REIN_EXTERIOR_CONSTRUCTION=@REIN_EXTERIOR_CONSTRUCTION	,
	REIN_DESCRIPTION=@REIN_DESCRIPTION	,
	REIN_REPORT_CODE=@REIN_REPORT_CODE	,
	REIN_NISS=@REIN_NISS	,
	MODIFIED_BY=@MODIFIED_BY	,
	LAST_UPDATED_DATETIME= @LAST_UPDATED_DATETIME	
	WHERE
	REIN_CONSTRUCTION_CODE_ID=@REIN_CONSTRUCTION_CODE_ID;
	
	              
  END

GO

