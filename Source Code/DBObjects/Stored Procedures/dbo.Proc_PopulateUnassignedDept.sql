IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_PopulateUnassignedDept]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_PopulateUnassignedDept]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  /*----------------------------------------------------------        
Proc Name   : dbo.Proc_PopulateUnassignedDept       
Created by  : Ashwani        
Date        : 15 Mar,2005      
Purpose     : To display the unassigned Department List        
Revison History  :              
------------------------------------------------------------                    
Date     Review By          Comments                  
         
-------------------------------------------*/    
--     drop procedure Proc_PopulateUnassignedDept    
CREATE PROCEDURE [dbo].[Proc_PopulateUnassignedDept]  (@DivisionId int)    
AS       
BEGIN        
 SELECT DEPT_ID,DEPT_NAME    
 FROM MNT_DEPT_LIST      
        WHERE DEPT_ID NOT IN     
  (SELECT DEPT_ID     
  FROM MNT_DIV_DEPT_MAPPING    
  WHERE MNT_DIV_DEPT_MAPPING.DIV_ID = @DivisionId   ) AND IsNull(MNT_DEPT_LIST.IS_ACTIVE,'')='Y' 
  ORDER BY DEPT_NAME    
      
End  
  
  
  
GO

