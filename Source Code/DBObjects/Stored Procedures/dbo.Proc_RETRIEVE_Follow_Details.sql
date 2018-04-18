IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_RETRIEVE_Follow_Details]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_RETRIEVE_Follow_Details]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------      
Proc Name       :  dbo.Proc_RETRIEVE_Follow_Details      
Created by      :  Anurag Verma    
Date            :  3/21/2007      
Purpose         :  To retrieve DIARY details of the diary entry 
Revison History :      
Used In         :   Wolverine      
-------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC dbo.Proc_RETRIEVE_Follow_Details
(
	@MODULEID INT,
	@DIARYTYPEID INT,
	@LOBID INT=NULL

)
AS

BEGIN
IF EXISTS (SELECT * FROM MNT_DIARY_DETAILS WHERE MDD_MODULE_ID=@MODULEID AND MDD_DIARYTYPE_ID=@DIARYTYPEID AND MDD_LOB_ID=@LOBID)
	SELECT * FROM MNT_DIARY_DETAILS WHERE MDD_MODULE_ID=@MODULEID  AND MDD_DIARYTYPE_ID=@DIARYTYPEID AND MDD_LOB_ID=@LOBID
ELSE
	SELECT * FROM MNT_DIARY_DETAILS WHERE MDD_MODULE_ID=@MODULEID  AND MDD_DIARYTYPE_ID=@DIARYTYPEID AND MDD_LOB_ID=-1

END
	


GO

