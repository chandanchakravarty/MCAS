IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyYearsWithWolverine]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyYearsWithWolverine]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                              
Proc Name    : dbo.Proc_GetPolicyYearsWithWolverine              
Created by    : Sibin Philip                   
Date          : 19 feb 2009                    
Purpose       : Get the Years with wolverine              
Revison History  :                                    
 ------------------------------------------------------------                                          
Date     Review By          Comments                                        
                               
------   ------------       -------------------------*/    
-- drop proc dbo.Proc_GetPolicyYearsWithWolverine  
--declare @yww int;                     
--exec Proc_GetPolicyYearsWithWolverine 1692,1,1,@yww output;   
--select @yww;          
        
CREATE PROCEDURE dbo.Proc_GetPolicyYearsWithWolverine                    
(                    
 @CUSTOMER_ID int,                    
 @POLICY_ID int,                    
 @POLICY_VERSION_ID int,         
 @YEARS_WITH_WOLVERINE INT OUTPUT                   
)                        
AS            
 DECLARE @YEARS INT      
BEGIN                              
    
 SELECT @YEARS=ISNULL(YEARS_INSU_WOL,0)                    
 FROM POL_AUTO_GEN_INFO                   
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID          
        
 IF(@YEARS>2)        
  BEGIN    
   SET @YEARS_WITH_WOLVERINE=1    
  END    
 ELSE      
  BEGIN      
   SET @YEARS_WITH_WOLVERINE=0       
  END    
  
/*Commented by Charles for Itrack Issue 6012 on 2-Jul-2009  
SELECT ISNULL(YEARS_INSU_WOL,0) as "YEARSCONTINSUREDWITHWOLVERINE",ISNULL(YEARS_INSU,0) as "YEARSCONTINSURED"   
FROM POL_AUTO_GEN_INFO                   
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
*/
               
END 
GO

