IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_Policy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_Policy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                    
Proc Name       : dbo.Proc_Get_Policy                  
Created by      : Swarup                  
Date            : 04-01-2007                   
Purpose         :                   
Revison History :                    
Used In         : Wolverine                    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/             
--Drop Procedure dbo.Proc_Get_Policy                    
CREATE PROCEDURE dbo.Proc_Get_Policy                 
(                      
  @CUSTOMERID int          
)                          
AS                               
               
 BEGIN                
  SELECT  CUSTOMER_ID,POLICY_ID,ISNULL(POLICY_NUMBER,'') + '-' + ISNULL(POLICY_DISP_VERSION,'') AS POLICY_DISP_NUMBER,  
 (CAST(ISNULL(POLICY_ID,0) AS VARCHAR) + '^' + CAST(ISNULL(POLICY_VERSION_ID,0) AS VARCHAR)) AS POLICY  
  FROM POL_CUSTOMER_POLICY_LIST PCL                
  WHERE CUSTOMER_ID = @CUSTOMERID                
   AND ISNULL(IS_ACTIVE, '') = 'Y' order by POLICY_DISP_NUMBER                
 END                
        
  



GO

