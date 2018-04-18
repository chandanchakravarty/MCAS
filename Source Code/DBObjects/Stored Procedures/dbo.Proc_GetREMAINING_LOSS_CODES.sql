IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetREMAINING_LOSS_CODES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetREMAINING_LOSS_CODES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_GetREMAINING_LOSS_CODES  
Created by      : Sumit Chhabra      
Date            : 12/05/2006        
Purpose       	 : Fetch Remaining Loss Codes for the given LOB
Revison History :        
Used In        : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
CREATE PROC Dbo.Proc_GetREMAINING_LOSS_CODES 
@LOB_ID int,
@TYPE_ID int,
@Lang_id int=1   
AS        
BEGIN     
	SELECT 
		CTD.DETAIL_TYPE_ID,isnull(CTDM.TYPE_DESC,CTD.DETAIL_TYPE_DESCRIPTION)DETAIL_TYPE_DESCRIPTION 
	FROM 
		CLM_TYPE_DETAIL CTD
	left outer join CLM_TYPE_DETAIL_MULTILINGUAL CTDM on CTDM.DETAIL_TYPE_ID=CTD.DETAIL_TYPE_ID and CTDM.LANG_ID=@Lang_id 	 
	WHERE 
		TYPE_ID=@TYPE_ID AND 
		CTD.DETAIL_TYPE_ID NOT IN
		(SELECT LOSS_CODE_TYPE FROM CLM_LOSS_CODES WHERE LOB_ID=@LOB_ID) 
	ORDER BY DETAIL_TYPE_DESCRIPTION
END  


GO

