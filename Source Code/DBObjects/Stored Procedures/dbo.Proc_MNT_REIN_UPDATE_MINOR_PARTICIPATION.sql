IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_UPDATE_MINOR_PARTICIPATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_UPDATE_MINOR_PARTICIPATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       : dbo.Proc_MNT_REIN_UPDATE_MINOR_PARTICIPATION                
Created by      : Harmanjeet Singh               
Date            : May 7, 2007              
Purpose         : To insert the data into Reinsurer TIV GROUP table.                
Revison History :                
Used In         : Wolverine         
               
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/     
--drop PROC [dbo].Proc_MNT_REIN_UPDATE_MINOR_PARTICIPATION              
CREATE PROC [dbo].Proc_MNT_REIN_UPDATE_MINOR_PARTICIPATION                
(                
               
	@MINOR_PARTICIPATION_ID	int ,
	@MAJOR_PARTICIPANTS NVARCHAR(100),
	@MINOR_LAYER int,
	@MINOR_PARTICIPANTS int,
	@MINOR_WHOLE_PERCENT decimal(18,2),
	@MODIFIED_BY INT,
	@LAST_UPDATED_DATETIME DATETIME
 )                
AS                
BEGIN      
                
             
   UPDATE MNT_REIN_MINOR_PARTICIPATION
	SET
	MINOR_PARTICIPATION_ID=@MINOR_PARTICIPATION_ID,
	MAJOR_PARTICIPANTS=@MAJOR_PARTICIPANTS,
	MINOR_LAYER=@MINOR_LAYER,      
	MINOR_PARTICIPANTS=@MINOR_PARTICIPANTS,
	MINOR_WHOLE_PERCENT=@MINOR_WHOLE_PERCENT, 
	MODIFIED_BY=@MODIFIED_BY,
	LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME
	WHERE 
	MINOR_PARTICIPATION_ID=@MINOR_PARTICIPATION_ID;       
  END




GO

