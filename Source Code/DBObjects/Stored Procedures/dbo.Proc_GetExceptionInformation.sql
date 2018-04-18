IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetExceptionInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetExceptionInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : Dbo.Proc_GetExceptionInformation          
Created by      : Swarup pal         
Date                    : 8-Mar-2007          
Purpose         : To get the Exception Information           
Revison History :          
Used In         : Wolverine          
------------------------------------------------------------          
   
------   ------------       -------------------------*/          
  -- drop Proc dbo.Proc_GetExceptionInformation          
CREATE  PROC dbo.Proc_GetExceptionInformation          
(          
 @EXCEPTIONID  int          
)          
AS          
BEGIN          
SELECT EXCEPTIONID,
isnull(EXCEPTIONDATE,'') AS EXCEPTIONDATE,
isnull(EXCEPTIONDESC,'') AS EXCEPTIONDESC,
isnull(CUSTOMER_ID,'') AS CUSTOMER_ID,
isnull(APP_ID,'') AS APP_ID,
isnull(APP_VERSION_ID,'') AS APP_VERSION_ID,
isnull(POLICY_ID,'') AS POLICY_ID,
isnull(POLICY_VERSION_ID,'') AS POLICY_VERSION_ID,
isnull(CLAIM_ID,'') AS CLAIM_ID,
isnull(QQ_ID,'') AS QQ_ID,
isnull(SOURCE,'') AS SOURCE,
isnull(MESSAGE,'') AS MESSAGE,
isnull(CLASS_NAME,'') AS CLASS_NAME,
isnull(METHOD_NAME,'') AS METHOD_NAME,
isnull(QUERY_STRING_PARAMS,'') AS QUERY_STRING_PARAMS,
isnull(SYSTEM_ID,'') AS SYSTEM_ID,
isnull(USER_ID,'') AS USER_ID,
isnull(LOB_ID,'') AS LOB_ID,
isnull(EXCEPTION_TYPE,'') AS EXCEPTION_TYPE
 FROM EXCEPTIONLOG     
WHERE    EXCEPTIONID = @EXCEPTIONID;         
          
END          



GO

