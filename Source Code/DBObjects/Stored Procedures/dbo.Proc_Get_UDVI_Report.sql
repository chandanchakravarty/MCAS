IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_UDVI_Report]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_UDVI_Report]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                  
Proc Name       : dbo.Proc_Get_UDVI_Report                                  
Created by      : Mohit Agarwal                                                          
Date            : 09/09/2008                                                                  
Purpose         : Fetches a UDVI report row in APP_UDVI_REPORT/POL_UDVI_REPORT            
Revewed by      :                                                           
Revison History :                                                                  
Used In        : Wolverine                                                                  
------------------------------------------------------------                                                                  
Date     Review By          Comments                                                                  
------   ------------       -------------------------*/                                                                  
--DROP PROC dbo.Proc_Get_UDVI_Report                                
CREATE PROC [dbo].[Proc_Get_UDVI_Report]                                  
@CUSTOMER_ID int,                                 
@APPPOL_ID int,                                 
@APPPOL_VERSION_ID int,                                 
@CALLED_FOR varchar(100)            
AS                                                                  
BEGIN  
IF @CALLED_FOR = 'APPLICATION'
BEGIN
	SELECT * FROM APP_UDVI_REPORT WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID =@APPPOL_ID AND APP_VERSION_ID = @APPPOL_VERSION_ID
	ORDER BY CREATED_DATETIME DESC
END
ELSE IF   @CALLED_FOR = 'POLICY'
BEGIN
	SELECT * FROM POL_UDVI_REPORT WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID =@APPPOL_ID AND POLICY_VERSION_ID = @APPPOL_VERSION_ID
	ORDER BY CREATED_DATETIME DESC

END

END

GO

