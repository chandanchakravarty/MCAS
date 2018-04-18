IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDefaultHierarchy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDefaultHierarchy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

   /*----------------------------------------------------------  
Proc Name       : Proc_GetDefaultHierarchy  
Created by      : Gaurav  
Date            : 9/5/2005  
Purpose       :Evaluation  
Revison History :  
Used In        : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  

drop proc Proc_GetDefaultHierarchy
------   ------------       -------------------------*/  
CREATE PROC [dbo].[Proc_GetDefaultHierarchy]  
(  
@AGENCY_ID     int  
)  
AS  
BEGIN  
select REC_ID,  
AGENCY_ID,  
DIV_ID as Division,  
DEPT_ID as DeptId,  
PC_ID as ProfitCenterId,  
MODIFIED_BY,  
LAST_UPDATED_DATETIME  
from  MNT_DEFAULT_HIERARCHY with(nolock) 
  
  
  
where  AGENCY_ID = @AGENCY_ID  
  
END  
  
GO

