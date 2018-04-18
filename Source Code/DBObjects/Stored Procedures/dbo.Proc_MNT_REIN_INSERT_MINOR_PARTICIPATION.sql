IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_INSERT_MINOR_PARTICIPATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_INSERT_MINOR_PARTICIPATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       : dbo.Proc_MNT_REIN_INSERT_MINOR_PARTICIPATION                
Created by      : Harmanjeet Singh               
Date            : May 7, 2007              
Purpose         : To insert the data into Reinsurer TIV GROUP table.                
Revison History :                
modified	: Pravesh K Chandel
Modified Date	:17 aug 2007
Purpose		: Add new param @CONTRACT_ID
Used In         : Wolverine         
               
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------
drop proc [dbo].Proc_MNT_REIN_INSERT_MINOR_PARTICIPATION   
*/                
CREATE PROC [dbo].Proc_MNT_REIN_INSERT_MINOR_PARTICIPATION                
(                
               
	@MINOR_PARTICIPATION_ID	int OUTPUT,
	@MAJOR_PARTICIPANTS NVARCHAR(100),
	@MINOR_LAYER int,
	@MINOR_PARTICIPANTS int,
	@MINOR_WHOLE_PERCENT decimal(18,2),
	
	@CREATED_BY INT,
	@CREATED_DATETIME DATETIME,
	@CONTRACT_ID  int
 )                
AS                
BEGIN      
                
             
    SELECT @MINOR_PARTICIPATION_ID=ISNULL(MAX(MINOR_PARTICIPATION_ID),0)+ 1 FROM MNT_REIN_MINOR_PARTICIPATION

	INSERT INTO MNT_REIN_MINOR_PARTICIPATION
	(
		MINOR_PARTICIPATION_ID,
		MAJOR_PARTICIPANTS,
		MINOR_LAYER,      
		MINOR_PARTICIPANTS,
		MINOR_WHOLE_PERCENT, 
		CREATED_BY,
		CREATED_DATETIME,
		IS_ACTIVE,
		CONTRACT_ID
	)
	VALUES
	(
		@MINOR_PARTICIPATION_ID,
		@MAJOR_PARTICIPANTS,
		@MINOR_LAYER,      
		@MINOR_PARTICIPANTS,
		@MINOR_WHOLE_PERCENT, 
		  
		@CREATED_BY,
		@CREATED_DATETIME,
		'Y',
		@CONTRACT_ID
	)
   
	
	              
  END






GO

