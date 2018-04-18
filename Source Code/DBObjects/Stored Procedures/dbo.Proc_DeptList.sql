IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeptList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeptList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------        
Proc Name   : dbo.Proc_DeptList       
Created by  :Priya        
Date        :24 Mar,2005      
Purpose     :         
Revison History  :              
 ------------------------------------------------------------                    
Date     Review By          Comments                  
         
------   ------------       -------------------------*/        
CREATE PROCEDURE dbo.Proc_DeptList      
AS       
BEGIN        
   SELECT DEPT_ID,DEPT_NAME        
   FROM  MNT_DEPT_LIST      
   WHERE  IS_ACTIVE = 'y'    
   ORDER BY   DEPT_NAME          
End        
    
    
    
  




GO

