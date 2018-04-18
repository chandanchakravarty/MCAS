IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_DELETE_CONSTRUCTIONTRANSLATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_DELETE_CONSTRUCTIONTRANSLATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------                
Proc Name       : dbo.[Proc_MNT_REIN_DELETE_TIVGROUP]                
Created by      : Harmanjeet Singh               
Date            : April 30, 2007              
Purpose         : To insert the data into Reinsurer TIV GROUP table.                
Revison History :                
Used In         : Wolverine         
               
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
CREATE PROC [dbo].[Proc_MNT_REIN_DELETE_CONSTRUCTIONTRANSLATION]                
(                
               
	@REIN_CONSTRUCTION_CODE_ID	int 
	
 )                
AS                
BEGIN      
                
             
   DELETE MNT_REIN_CONSTRUCTION_TRANSLATION
	WHERE 
	REIN_CONSTRUCTION_CODE_ID=@REIN_CONSTRUCTION_CODE_ID;     
  
  END




GO

