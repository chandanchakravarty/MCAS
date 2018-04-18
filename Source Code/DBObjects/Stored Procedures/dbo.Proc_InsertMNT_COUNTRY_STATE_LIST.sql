IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertMNT_COUNTRY_STATE_LIST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertMNT_COUNTRY_STATE_LIST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------                                                        
Proc Name       : dbo.Proc_InsertMNT_COUNTRY_STATE_LIST                                                  
Created by      : Sumit Chhabra                                                      
Date            : 30/04/2007                                                        
Purpose         : Insert data at    MNT_COUNTRY_STATE_LIST                     
Created by      : Sumit Chhabra                                                       
Revison History :                                                        
Used In        : Wolverine                                                        
------------------------------------------------------------                                                        
Date     Review By          Comments                                                        
------   ------------       -------------------------*/                                                        
--DROP PROC dbo.Proc_InsertMNT_COUNTRY_STATE_LIST                                                                           
CREATE PROC dbo.Proc_InsertMNT_COUNTRY_STATE_LIST                                                                           
@STATE_CODE varchar(20),
@STATE_NAME varchar(50),
@STATE_DESC varchar(50),
@COUNTRY_ID int,
@STATE_ID int OUTPUT,
@CREATED_BY int,
@CREATED_DATETIME datetime
AS                                                        
BEGIN           

IF EXISTS(SELECT STATE_ID FROM MNT_COUNTRY_STATE_LIST WHERE COUNTRY_ID=@COUNTRY_ID AND STATE_CODE=@STATE_CODE)
BEGIN
	SET @STATE_ID = -2
	RETURN
END
SELECT @STATE_ID = MAX(ISNULL(STATE_ID,0))+1 FROM MNT_COUNTRY_STATE_LIST

INSERT INTO MNT_COUNTRY_STATE_LIST
(
STATE_CODE,
STATE_NAME,
STATE_DESC,
COUNTRY_ID,
STATE_ID,
CREATED_BY,
CREATED_DATETIME,
IS_ACTIVE
)
VALUES
(
@STATE_CODE,
@STATE_NAME,
@STATE_DESC,
@COUNTRY_ID,
@STATE_ID,
@CREATED_BY,
@CREATED_DATETIME,
'Y'
)

END                                                  
                                    
                        
        
      
      
    
  
  













GO

