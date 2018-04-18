IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_SPLIT_GETCOVERAGE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_SPLIT_GETCOVERAGE]
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
--DROP PROC [dbo].[Proc_MNT_REIN_SPLIT_GETCOVERAGE]   
CREATE PROC [dbo].[Proc_MNT_REIN_SPLIT_GETCOVERAGE]  
(  
@COV_CODE nvarchar(50),
@COV_ID int  
  
)  
AS  
BEGIN  
  
SELECT COV_DES, COV_CODE,COV_ID  
FROM MNT_COVERAGE  
WHERE  COV_CODE= @COV_CODE and COV_ID = @COV_ID 
  
  
END  
  



GO

