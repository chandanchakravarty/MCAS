IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AssignDeptToDiv]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AssignDeptToDiv]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------              
Proc Name       : dbo.Proc_AssignDeptToDiv        
Created by      : Ashwani              
Date            : 17 Mar,2005              
Purpose         : To assign the departments  to division.              
Revison History :              
Used In         : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
CREATE PROC Dbo.Proc_AssignDeptToDiv              
(          
 @DivisionId int,    
 @DeptId  int    
)              
AS              
BEGIN            
   INSERT INTO MNT_dIV_DEPT_MAPPING(DIV_ID,DEPT_ID)    
   VALUES(@DivisionId,@DeptId)     
END        




GO

