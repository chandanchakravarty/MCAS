IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateMVRFetchDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateMVRFetchDetail]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/********************************************
CREATE BY 			: Vijay Joshi
CREATED DATETIME	: 20 Feb,2006
REASON				: Update the last mvr fetch details
*********************************************/
CREATE PROCEDURE Proc_UpdateMVRFetchDetail    
(            
	@CUSTOMER_ID 	INT,
	@FETCH_DATE 			DATETIME
)      
AS      
BEGIN

	UPDATE CLT_CUSTOMER_LIST
	SET LAST_MVR_SCORE_FETCHED = @FETCH_DATE
	WHERE CUSTOMER_ID= @CUSTOMER_ID

END    
  




GO

