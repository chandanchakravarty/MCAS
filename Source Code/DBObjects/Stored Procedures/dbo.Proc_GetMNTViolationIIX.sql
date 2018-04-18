IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMNTViolationIIX]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMNTViolationIIX]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                    
Proc Name                : Dbo.Proc_GetMNTViolationIIX                          
Created by               :Mohit Agarwal                                                  
Date                    : 03 Jul 2007                                                 
Purpose                  : To get MNT Violation Type for MVR IIX Response        
Used In                  : Wolverine                                                    
------------------------------------------------------------                                                    
Date     Review By          Comments   
drop proc Dbo.Proc_GetMNTViolationIIX                                                  
------   ------------       -------------------------*/                                                    
CREATE  proc Dbo.Proc_GetMNTViolationIIX     
(                                                    
 @LOB_ID    int,                                                    
 @STATE_ID    int,                                                    
 @VIOL_TYPE   varchar(100)     
           
)                                                    
as                                     
  
SELECT     *  
FROM        MNT_VIOLATIONS  
where State=@STATE_ID AND LOB=@LOB_ID AND VIOLATION_DES LIKE @VIOL_TYPE + '%' AND VIOLATION_ID >= 15000



GO

