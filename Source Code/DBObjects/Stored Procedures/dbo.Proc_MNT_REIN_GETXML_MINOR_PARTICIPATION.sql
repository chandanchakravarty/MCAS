IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_GETXML_MINOR_PARTICIPATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_GETXML_MINOR_PARTICIPATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       : dbo.Proc_MNT_REIN_GETXML_MINOR_PARTICIPATION                
Created by      : Harmanjeet Singh               
Date            : May 07, 2007              
Purpose         : To insert the data into Reinsurer Construction Translation table.                
Revison History :               
Modified by 	:Pravesh k Chandel
 Modified Date	:20 aug 2007
purpose		: to pick tolal Percentage
Used In         : Wolverine         
               
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------
drop proc [dbo].[Proc_MNT_REIN_GETXML_MINOR_PARTICIPATION]
*/                
CREATE proc [dbo].[Proc_MNT_REIN_GETXML_MINOR_PARTICIPATION]
(              
	@MINOR_PARTICIPATION_ID	int 	
  )                
AS                
BEGIN      
                
             
    SELECT

	ISNULL(MINOR_PARTICIPATION_ID,0) MINOR_PARTICIPATION_ID,
	ISNULL(MAJOR_PARTICIPANTS,'') MAJOR_PARTICIPANTS,
	ISNULL(MINOR_LAYER,'') MINOR_LAYER,
	ISNULL(MINOR_PARTICIPANTS,'') MINOR_PARTICIPANTS, 
	ISNULL(MINOR_WHOLE_PERCENT,0) MINOR_WHOLE_PERCENT,
	ISNULL(CREATED_BY,0) CREATED_BY,
	CONVERT(VARCHAR,CREATED_DATETIME,101) CREATED_DATETIME,
	ISNULL(MODIFIED_BY,0) MODIFIED_BY,
	CONVERT(VARCHAR,LAST_UPDATED_DATETIME,101) LAST_UPDATED_DATETIME,      
	ISNULL(IS_ACTIVE,'') IS_ACTIVE,
	(select sum(MINOR_WHOLE_PERCENT) from MNT_REIN_MINOR_PARTICIPATION where CONTRACT_ID=MRP.CONTRACT_ID) as OLDTOTALPERCENT
	FROM MNT_REIN_MINOR_PARTICIPATION MRP
    
	WHERE
	MINOR_PARTICIPATION_ID=@MINOR_PARTICIPATION_ID;
     
	
	              
  END









GO

