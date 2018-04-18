IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateMNT_COUNTRY_STATE_LIST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateMNT_COUNTRY_STATE_LIST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 
/*----------------------------------------------------------                                                        
Proc Name       : dbo.Proc_UpdateMNT_COUNTRY_STATE_LIST                                                  
Created by      : Sumit Chhabra                                                      
Date            : 30/04/2007                                                        
Purpose         : Update data at    MNT_COUNTRY_STATE_LIST                     
Created by      : Sumit Chhabra                                                       
Revison History :                                                        
Used In        : Wolverine                                                        
------------------------------------------------------------                                                        
Date     Review By          Comments                                                        
------   ------------       -------------------------*/                                                        
--DROP PROC dbo.Proc_UpdateMNT_COUNTRY_STATE_LIST                                                                           
CREATE PROC dbo.Proc_UpdateMNT_COUNTRY_STATE_LIST                                                                           
@STATE_CODE varchar(20),
@STATE_NAME varchar(50),
@STATE_DESC varchar(50),
@COUNTRY_ID int,
@STATE_ID int,
@MODIFIED_BY int,
@LAST_UPDATED_DATETIME datetime
AS                                                        
BEGIN           

IF EXISTS(SELECT STATE_ID FROM MNT_COUNTRY_STATE_LIST WHERE COUNTRY_ID=@COUNTRY_ID AND STATE_CODE=@STATE_CODE AND STATE_ID<>@STATE_ID)
BEGIN
	SET @STATE_ID = -2
	RETURN
END



UPDATE MNT_COUNTRY_STATE_LIST	
SET
	STATE_CODE=@STATE_CODE,
	STATE_NAME=@STATE_NAME,
	STATE_DESC=@STATE_DESC,		
	MODIFIED_BY=@MODIFIED_BY,
	LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,
	COUNTRY_ID=@COUNTRY_ID	
WHERE
	 STATE_ID=@STATE_ID

RETURN 1
END             

                                    
                                    
                        
        
      
      
    
  
  















GO

