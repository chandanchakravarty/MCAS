IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_PopulateassignedDept]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_PopulateassignedDept]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------          
Proc Name   : dbo.Proc_PopulateassignedDept         
Created by  : Ashwani          
Date        : 15 Mar,2005        
Purpose     : To display the assigned Department List          
Revison History  :                
------------------------------------------------------------                      
Date     Review By          Comments                    
           
-------------------------------------------*/      
--   drop proc Proc_PopulateassignedDept      
CREATE PROCEDURE [dbo].[Proc_PopulateassignedDept]  
(
	@DivisionId int,
	@ShowAll nchar(2) = 'Y'
)      
AS         
BEGIN          
  
	if @ShowAll = 'Y'  
	BEGIN  
		SELECT DIVDEPTLIST.DEPT_ID,MNT_DEPT_LIST.DEPT_NAME   
		FROM MNT_DIV_DEPT_MAPPING DIVDEPTLIST WITH(NOLOCK)  
		INNER JOIN  MNT_DEPT_LIST    
		ON MNT_DEPT_LIST.DEPT_ID= DIVDEPTLIST.DEPT_ID  
		WHERE DIVDEPTLIST.DIV_ID = @DivisionId  
		ORDER BY DEPT_NAME ASC  
	
	End    
	ELSE  
	BEGIN  
		SELECT DIVDEPTLIST.DIV_ID,DIVDEPTLIST.DEPT_ID,MNT_DEPT_LIST.DEPT_NAME   
		FROM MNT_DIV_DEPT_MAPPING DIVDEPTLIST WITH(NOLOCK) 
		INNER JOIN MNT_DEPT_LIST    
		ON MNT_DEPT_LIST.DEPT_ID= DIVDEPTLIST.DEPT_ID  
		WHERE MNT_DEPT_LIST.IS_ACTIVE='Y' 
		ORDER BY DEPT_NAME ASC  
	END    
	
	SELECT DISTINCT DEPT_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE DEPT_ID!='' AND DEPT_ID is not null
END 



GO

