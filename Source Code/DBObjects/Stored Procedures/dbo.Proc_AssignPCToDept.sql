IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AssignPCToDept]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AssignPCToDept]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------              
Proc Name       : dbo.Proc_AssignPCToDept        
Created by      : Priya              
Date            : 24 Mar,2005              
Purpose         : To assign the Profit Center  to department.              
Revison History :              
Used In         : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
CREATE PROC Dbo.Proc_AssignPCToDept              
(          
 @DeptId int,    
 @PCId  int    
)              
AS              
BEGIN            
 
  INSERT INTO MNT_DEPT_PC_MAPPING(DEPT_ID,PC_ID)    
  VALUES(@DeptId,@PCId)     
 
END        




GO

