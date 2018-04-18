IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_DELETE_TIVGROUP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_DELETE_TIVGROUP]
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
CREATE PROC [dbo].[Proc_MNT_REIN_DELETE_TIVGROUP]                
(                
               
	@REIN_TIV_GROUP_ID	int 
	
 )                
AS                
BEGIN      
                
             
   DELETE MNT_REIN_TIV_GROUP
	WHERE 
	REIN_TIV_GROUP_ID=@REIN_TIV_GROUP_ID;     
  
  END




GO

