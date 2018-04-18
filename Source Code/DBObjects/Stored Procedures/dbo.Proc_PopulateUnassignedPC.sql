IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_PopulateUnassignedPC]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_PopulateUnassignedPC]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[Proc_PopulateUnassignedPC]  (@DeptId int)        
AS           
BEGIN            
  SELECT PC_ID,PC_NAME        
  FROM MNT_PROFIT_CENTER_LIST          
           WHERE PC_ID NOT IN         
   (SELECT PC_ID        
  FROM MNT_DEPT_PC_MAPPING        
       WHERE MNT_DEPT_PC_MAPPING.DEPT_ID = @DeptId      
  ) AND IsNull(MNT_PROFIT_CENTER_LIST.IS_ACTIVE,'')='Y'    

       ORDER BY PC_NAME     
          
End        
    
  
  
  
GO

