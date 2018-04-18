IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertTEMP_TEMP_ACT_DISTRIBUTION_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertTEMP_TEMP_ACT_DISTRIBUTION_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name          : Dbo.Proc_InsertDistributionDetail
Created by           : Mohit Gupta
Date                    : 
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_InsertTEMP_TEMP_ACT_DISTRIBUTION_DETAILS
(
       @GROUP_ID int,
       @GROUP_TYPE varchar(5),
       @ACCOUNT_ID int,
       @DISTRIBUTION_PERCT decimal(5,2),
       @DISTRIBUTION_AMOUNT decimal(18,2) ,
       @NOTE nvarchar(255),
       @CREATED_BY int
)
AS
BEGIN
INSERT INTO TEMP_ACT_DISTRIBUTION_DETAILS
(
GROUP_ID,
GROUP_TYPE,
ACCOUNT_ID,
DISTRIBUTION_PERCT,
DISTRIBUTION_AMOUNT,
NOTE,
IS_ACTIVE,
CREATED_BY,
CREATED_DATETIME
)
VALUES
(
@GROUP_ID,
@GROUP_TYPE,
@ACCOUNT_ID,
@DISTRIBUTION_PERCT,
@DISTRIBUTION_AMOUNT,
@NOTE,
'Y',
@CREATED_BY,
GETDATE()
)
END




GO

