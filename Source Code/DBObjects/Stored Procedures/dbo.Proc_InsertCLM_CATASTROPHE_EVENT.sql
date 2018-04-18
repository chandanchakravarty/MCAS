IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_CATASTROPHE_EVENT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_CATASTROPHE_EVENT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_InsertCLM_CATASTROPHE_EVENT  
Created by      : Vijay Arora  
Date            : 4/24/2006  
Purpose     	: To Insert the Data the in table named CLM_CATASTROPHE_EVENT  
Revison History :  
Used In  	: Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_InsertCLM_CATASTROPHE_EVENT  
(  
 @CATASTROPHE_EVENT_ID     int OUTPUT,  
 @CATASTROPHE_EVENT_TYPE     int,  
 @DATE_FROM     datetime,  
 @DATE_TO     datetime,  
 @DESCRIPTION     varchar(500),  
 @CAT_CODE     varchar(20),  
 @CREATED_BY     int,  
 @CREATED_DATETIME     datetime  
)  
AS  
BEGIN  
  
select @CATASTROPHE_EVENT_ID=isnull(Max(CATASTROPHE_EVENT_ID),0)+1 from CLM_CATASTROPHE_EVENT  
INSERT INTO CLM_CATASTROPHE_EVENT  
(  
CATASTROPHE_EVENT_ID,  
CATASTROPHE_EVENT_TYPE,  
DATE_FROM,  
DATE_TO,  
DESCRIPTION,  
CAT_CODE,  
IS_ACTIVE,  
CREATED_BY,  
CREATED_DATETIME  
)  
VALUES  
(  
@CATASTROPHE_EVENT_ID,  
@CATASTROPHE_EVENT_TYPE,  
@DATE_FROM,  
@DATE_TO,  
@DESCRIPTION,  
@CAT_CODE,  
'Y',  
@CREATED_BY,  
GETDATE()  
)  
END  
  



GO

