IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETDIARY_TYPE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETDIARY_TYPE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


  /*----------------------------------------------------------        
Proc Name       :  dbo.PROC_GETDIARY_TYPE        
Created by      :  Anurag Verma      
Date            :  3/21/2007        
Purpose         :  To retrieve type_flag of the diary entry   
Revison History :        
Used In         :   Wolverine        
-------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
CREATE PROCEDURE [dbo].[PROC_GETDIARY_TYPE]   
@DIARY_ID INT   
AS  
BEGIN  
SELECT TYPE_FLAG FROM TODOLISTTYPES (nolock) WHERE TYPEID=@DIARY_ID   
END  
  
  
GO

