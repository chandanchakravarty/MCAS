IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Insert_UDVI_Report]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Insert_UDVI_Report]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                  
Proc Name       : dbo.Proc_Insert_UDVI_Report                                  
Created by      : Mohit Agarwal                                                          
Date            : 09/09/2008                                                                  
Purpose         : Creates a UDVI report row in APP_UDVI_REPORT/POL_UDVI_REPORT            
Revewed by      :                                                           
Revison History :                                                                  
Used In        : Wolverine                                                                  
------------------------------------------------------------                                                                  
Date     Review By          Comments                                                                  
------   ------------       -------------------------*/                                                                  
--DROP PROC dbo.Proc_Insert_UDVI_Report                                
CREATE PROC [dbo].[Proc_Insert_UDVI_Report]                                  
@CUSTOMER_ID int,                                 
@APPPOL_ID int,                                 
@APPPOL_VERSION_ID int,                                 
@REPORT_HTML TEXT,
@CREATED_BY INT,
@CALLED_FOR varchar(100)            
AS                                                                  
BEGIN  
IF @CALLED_FOR = 'APPLICATION'
BEGIN
	INSERT INTO APP_UDVI_REPORT (CUSTOMER_ID,APP_ID,APP_VERSION_ID,REPORT_HTML,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME)
	VALUES( @CUSTOMER_ID, @APPPOL_ID, @APPPOL_VERSION_ID, @REPORT_HTML,@CREATED_BY, GetDate(), NULL, NULL)
END
ELSE IF   @CALLED_FOR = 'POLICY'
BEGIN
	INSERT INTO POL_UDVI_REPORT (CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,REPORT_HTML,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME)
	VALUES( @CUSTOMER_ID, @APPPOL_ID, @APPPOL_VERSION_ID, @REPORT_HTML,@CREATED_BY, GetDate(), NULL, NULL)
END

END

GO

