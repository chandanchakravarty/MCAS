IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRemainingLOB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRemainingLOB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       : dbo.Proc_GetRemainingLOB
Created by      : Sumit Chhabra              
Date            : 25/04/2006                
Purpose         : Get Remaining LOBs for the current adjuster
Created by      : Sumit Chhabra               
Revison History :                
Used In        : Wolverine                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
CREATE PROC dbo.Proc_GetRemainingLOB
@ADJUSTER_ID int              
AS                
BEGIN                
	SELECT 
		LOB_ID,LOB_DESC 
	FROM 
		MNT_LOB_MASTER 
	WHERE 
		LOB_ID NOT IN 
			(SELECT LOB_ID FROM CLM_ADJUSTER_AUTHORITY WHERE ADJUSTER_ID=@ADJUSTER_ID)
      

END          


  
  



GO

