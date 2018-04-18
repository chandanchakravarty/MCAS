IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRuleXml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRuleXml]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create proc dbo.Proc_GetRuleXml
(
 @CUSTOMER_ID int,
 @APP_ID int,
 @APP_VERSION_ID int
)
as
begin
      
   SELECT 
	ISNULL(APP_VERIFICATION_XML,'') FROM APP_LIST with(nolock) 
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND 
	APP_ID = @APP_ID AND 
	APP_VERSION_ID = @APP_VERSION_ID              

end
   

GO

