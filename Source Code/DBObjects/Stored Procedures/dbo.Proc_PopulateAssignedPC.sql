IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_PopulateAssignedPC]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_PopulateAssignedPC]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


  /*----------------------------------------------------------              
Proc Name    : dbo.Proc_PopulateassignedPC            
Created by   : Priya             
Date        : 24 Mar,2005            
Purpose      : To display the assigned Profit Center List              
Revison History :    
Modified By : Gaurav, added extra parameter to show Active records in drop down on Add Accounting Entity                   
------------------------------------------------------------                          
Date     Review By          Comments                        
               
-------------------------------------------*/          
      --  drop proc Proc_PopulateAssignedPC 
CREATE PROCEDURE [dbo].[Proc_PopulateAssignedPC]  
(   
 @DeptId int,  
 @ShowAll nchar(2) = 'Y'  
)          
AS             
BEGIN        
 if @ShowAll = 'Y'    
 BEGIN          
  SELECT MNT_PROFIT_CENTER_LIST.PC_ID , MNT_PROFIT_CENTER_LIST.PC_NAME       
  FROM MNT_DEPT_PC_MAPPING          
  LEFT JOIN MNT_PROFIT_CENTER_LIST on MNT_PROFIT_CENTER_LIST.PC_ID =  MNT_DEPT_PC_MAPPING.PC_ID    
  WHERE MNT_DEPT_PC_MAPPING.DEPT_ID = @DeptId     
  ORDER BY PC_NAME       
 END
 ELSE    
 BEGIN     
  SELECT DEPTPCLIST.PC_ID,MNT_PROFIT_CENTER_LIST.PC_NAME     
  FROM MNT_DEPT_PC_MAPPING DEPTPCLIST     
  INNER JOIN MNT_PROFIT_CENTER_LIST      
  ON MNT_PROFIT_CENTER_LIST.PC_ID= DEPTPCLIST.PC_ID    
  WHERE IsNull(MNT_PROFIT_CENTER_LIST.IS_ACTIVE,'')='Y' and DEPTPCLIST.DEPT_ID = @DeptId    
  ORDER BY PC_NAME ASC    
 END  
 
  SELECT DISTINCT PC_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE PC_ID!='' AND PC_ID IS NOT NULL
   
END  
  
  
  
GO

