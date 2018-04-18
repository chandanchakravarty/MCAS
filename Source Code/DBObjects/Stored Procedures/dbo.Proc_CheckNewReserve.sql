IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckNewReserve]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckNewReserve]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       :  dbo.Proc_CheckNewReserve      
Created by      :  Sumit Chhabra    
Date            :  6/16/2006      
Purpose         :  To check that new reserve is exist or not   
Revison History :      
Used In         :   Wolverine      
-------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE proc dbo.Proc_CheckNewReserve      
@CLAIM_ID int      
as      
begin   
 declare @NEW_RESERVE int   
 set @NEW_RESERVE =11836  
 if exists(select activity_id from clm_activity where claim_id=@claim_id AND ACTIVITY_REASON=@NEW_RESERVE)
	return 1
 else
  return 0
end        



GO

