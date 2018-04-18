IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDiaryEntryListID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDiaryEntryListID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name         : dbo.Proc_GetDiaryEntryListID    
Created by         : Vijay Arora    
Date                 : 23-12-2005  
Purpose           : Gets the Diary Entry List ID.  
Revison History   :            
Used In           : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
CREATE  PROC dbo.Proc_GetDiaryEntryListID
(            
	@CUSTOMER_ID INT,  
	@POLICY_ID INT,  
	@POLICY_VERSION_ID SMALLINT,  
	@ROW_ID INT,  
	@DIARY_LIST_ID INT OUTPUT  
)            
AS            
            
BEGIN            
	IF EXISTS ( SELECT DIARY_LIST_ID FROM POL_POLICY_PROCESS  PP
		LEFT JOIN TODOLIST TD ON TD.LISTID = PP.DIARY_LIST_ID
        WHERE PP.CUSTOMER_ID = @CUSTOMER_ID  
   		AND PP.POLICY_ID = @POLICY_ID  
  		AND PP.POLICY_VERSION_ID = @POLICY_VERSION_ID  
  		AND PP.ROW_ID = @ROW_ID 
		AND ISNULL(LISTOPEN,'') = 'Y')
	BEGIN  
   		SELECT @DIARY_LIST_ID = DIARY_LIST_ID FROM POL_POLICY_PROCESS  
        WHERE CUSTOMER_ID = @CUSTOMER_ID  
     	AND POLICY_ID = @POLICY_ID  
    	AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
    	AND ROW_ID = @ROW_ID  
  	END
	ELSE  
  	BEGIN  
  		SET @DIARY_LIST_ID = 0  
  	END  
  
 	RETURN @DIARY_LIST_ID  
END


GO

