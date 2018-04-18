IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateCLT_APPLICANT_LIST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateCLT_APPLICANT_LIST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------        
Proc Name          : Dbo.Proc_ActivateDeactivateCLT_APPLICANT_LIST        
Created by           : Mohit Gupta        
Date                    : 19/05/2005        
Purpose               :         
Revison History :        
Used In                :   Wolverine          
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------  
--drop proc Proc_ActivateDeactivateCLT_APPLICANT_LIST   */     
CREATE    PROCEDURE  [dbo].[Proc_ActivateDeactivateCLT_APPLICANT_LIST]   
(        
 @code int ,        
 @Is_Active nchar(1)        
)        
AS        
BEGIN        
UPDATE CLT_APPLICANT_LIST        
SET Is_Active= @Is_Active        
WHERE APPLICANT_ID= @code      
    
/*Update Coverage On Occupation */    
--exec PROC_UPDATE_HOME_COVERAGE_BY_APPLICANT NULL,@code    
--exec PROC_UPDATE_POLICY_HOME_COVERAGE_BY_APPLICANT NULL,@CODE      
    
      
--update data in the app_applicant_list      
if(upper(@Is_Active)='Y')      
 update POL_APPLICANT_LIST set is_primary_applicant=1      
 where applicant_id=  @code      
else      
 update POL_APPLICANT_LIST set is_primary_applicant=0      
 where applicant_id=  @code      
END        
      
    
    
    
GO

