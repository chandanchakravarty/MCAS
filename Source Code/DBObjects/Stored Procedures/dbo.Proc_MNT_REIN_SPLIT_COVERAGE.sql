IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_SPLIT_COVERAGE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_SPLIT_COVERAGE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Created by  : Harmanjeet Singh  
Purpose     : Evaluation for the Contract Type screen  
Date  : April 27, 2007  
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--drop PROC [dbo].[Proc_MNT_REIN_SPLIT_COVERAGE]    
CREATE PROC [dbo].[Proc_MNT_REIN_SPLIT_COVERAGE]  
(  
@REIN_SPLIT_DEDUCTION_ID nvarchar(5)  
  
)  
AS  
BEGIN  
  
SELECT REIN_IST_SPLIT_COVERAGE,REIN_COVERAGE  
FROM MNT_REIN_SPLIT  
WHERE  REIN_SPLIT_DEDUCTION_ID=@REIN_SPLIT_DEDUCTION_ID  
  
  
END  


GO

