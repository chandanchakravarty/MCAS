IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MNT_REIN_INSERT_CONSTRUCTIONTRANSLATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MNT_REIN_INSERT_CONSTRUCTIONTRANSLATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 
/*----------------------------------------------------------                
Proc Name       : dbo.[MNT_REIN_INSERT_CONSTRUCTIONTRANSLATION]                
Created by      : Harmanjeet Singh               
Date            : April 27, 2007              
Purpose         : To insert the data into Reinsurer Construction Translation table.                
Revison History :                
Used In         : Wolverine         
               
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
create PROC dbo.PROC_MNT_REIN_INSERT_CONSTRUCTIONTRANSLATION
(                
               
	@REIN_CONSTRUCTION_CODE_ID	int OUTPUT,
	@REIN_EXTERIOR_CONSTRUCTION	nvarchar(50),
	@REIN_DESCRIPTION	nvarchar(50),
	@REIN_REPORT_CODE	nvarchar(50),
	@REIN_NISS	NCHAR(1),
	@CREATED_BY	INT,
	@CREATED_DATETIME	DATETIME
	
		
  )                
AS                
BEGIN      
                
SELECT @REIN_CONSTRUCTION_CODE_ID = isnull(Max(REIN_CONSTRUCTION_CODE_ID),0)+1 FROM MNT_REIN_CONSTRUCTION_TRANSLATION        
              
   INSERT INTO MNT_REIN_CONSTRUCTION_TRANSLATION                
   (               
	REIN_CONSTRUCTION_CODE_ID	,
	REIN_EXTERIOR_CONSTRUCTION	,
	REIN_DESCRIPTION	,
	REIN_REPORT_CODE	,
	REIN_NISS	,
	CREATED_BY	,
	CREATED_DATETIME,	
	IS_ACTIVE
	
	              
   )                
   VALUES                
   (    
	@REIN_CONSTRUCTION_CODE_ID,
	@REIN_EXTERIOR_CONSTRUCTION	,
	@REIN_DESCRIPTION,
	@REIN_REPORT_CODE,
	@REIN_NISS	,
	@CREATED_BY	,
	@CREATED_DATETIME,	
	'Y'
	
	  )    
   
        
     END





GO

