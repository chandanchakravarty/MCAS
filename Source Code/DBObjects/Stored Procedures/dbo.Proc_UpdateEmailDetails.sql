IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateEmailDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateEmailDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_UpdateEmailDetails
Created by           : Gaurav
Date                    : 19/07/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_UpdateEmailDetails
(
	@CUSTOMER_ID int,
	@EMAIL_FROM_NAME nvarchar(100),
	@EMAIL_FROM nvarchar(100),
	@EMAIL_TO nvarchar(100),
	@EMAIL_RECIPIENTS nvarchar(2000),
	@EMAIL_SUBJECT nvarchar(255),
	@EMAIL_MESSAGE nvarchar(4000),
	@EMAIL_ATTACH_PATH nvarchar(255),
	@Email_Row_Id int output	

)
AS
BEGIN

UPDATE CLT_CUSTOMER_EMAIL
SET
CUSTOMER_ID =@CUSTOMER_ID,
EMAIL_FROM_NAME=@EMAIL_FROM_NAME,
EMAIL_FROM=@EMAIL_FROM,
EMAIL_TO=@EMAIL_TO,
EMAIL_RECIPIENTS=@EMAIL_RECIPIENTS,
EMAIL_SUBJECT=@EMAIL_SUBJECT,
EMAIL_MESSAGE=@EMAIL_MESSAGE,
EMAIL_ATTACH_PATH=@EMAIL_ATTACH_PATH
WHERE CUSTOMER_ID =@CUSTOMER_ID AND EMAIL_ROW_ID=@Email_Row_Id


END





GO

