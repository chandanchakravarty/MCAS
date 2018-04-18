IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchCustomerNotes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchCustomerNotes]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_FetchCustomerNotes  
Created by           : Gaurav  
Date                    : 08/01/2005  
Purpose               : To get Customer Notes from CLT_CUSTOMER_NOTES  
Revison History :  
Used In                :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--DROP PROC Dbo.Proc_FetchCustomerNotes  
CREATE  PROC Dbo.Proc_FetchCustomerNotes  
(  
  
 @CUSTOMER_ID int,
 @NOTES_ID varchar(1000)  
)  
  
AS  
BEGIN  
DECLARE @SQLSTMT VARCHAR(4000)

SET  @SQLSTMT ='SELECT NOTES_SUBJECT +''(''+ CONVERT(VARCHAR,CREATED_DATETIME,101)+'')'' SUBJECT,NOTES_DESC ,*
 FROM CLT_CUSTOMER_NOTES  
 WHERE CUSTOMER_ID=' + CONVERT(VARCHAR,@CUSTOMER_ID) + ' AND NOTES_ID in (''' + (@NOTES_ID) 
 + ''') ORDER BY CREATED_DATETIME' 
--PRINT  @SQLSTMT
EXEC (@SQLSTMT)
END  



GO

