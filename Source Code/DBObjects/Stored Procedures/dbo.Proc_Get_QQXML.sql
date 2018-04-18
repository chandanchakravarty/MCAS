IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_QQXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_QQXML]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name 	: Dbo.Proc_Get_QQXML      
Created by      : Praveen singh       
Date            : 8/02/2006      
Purpose         : To get the XML FROM QQ LIST 
Revison History :      
Used In         : Wolverine      
------   ------------       -------------------------*/      
CREATE    PROC Dbo.Proc_Get_QQXML
(      
 @CUSTOMER_ID    int,      
 @APP_ID  	int,      
 @APP_VERSION_ID  int      
      
)      
AS      
BEGIN      
DECLARE @app_number VARCHAR(20)

  select @app_number= isnull(app_number,'') from app_list where customer_id=@CUSTOMER_ID and app_id=@APP_ID and app_version_id=@APP_VERSION_ID 

select isnull(QQ_XML,'') from clt_quickquote_list 
where QQ_ID = (select QQ_ID from clt_quickquote_list where qq_app_number =@app_number)
and customer_id=@CUSTOMER_ID



END    
    
    


GO

