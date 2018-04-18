IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MotorGreaterAmountRulePolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MotorGreaterAmountRulePolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                      
Proc Name       : dbo.Proc_MotorGreaterAmountRulePolicy                     
Created by      : Sumit Chhabra            
Date            : 01/20/2006            
Purpose         : To set the value in db when motor Has Amount Greater Than 30000                    
Revison History :                      
Used In         :                      
                    
                  
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------*/                      
CREATE PROC dbo.Proc_MotorGreaterAmountRulePolicy                      
(                      
@CUSTOMER_ID    int,                      
@POLICY_ID          int,                      
@POLICY_VERSION_ID  int                      
)                      
AS                      
BEGIN                      
               
             
if exists          
   (select customer_id from pol_auto_gen_info where             
                    CUSTOMER_ID  =@CUSTOMER_ID AND                      
                     POLICY_ID   =@POLICY_ID AND                      
                     POLICY_VERSION_ID =@POLICY_VERSION_ID)          
begin           
 if exists(SELECT customer_id FROM POL_VEHICLES  where          
                    CUSTOMER_ID  =@CUSTOMER_ID AND                      
                    POLICY_ID   =@POLICY_ID AND                      
                    POLICY_VERSION_ID =@POLICY_VERSION_ID AND                      
                     AMOUNT > 40000 and IS_ACTIVE ='Y')          
 begin           
     update pol_auto_gen_info set IS_COST_OVER_DEFINED_LIMIT=1 where           
                    CUSTOMER_ID  =@CUSTOMER_ID AND                      
                     POLICY_ID   =@POLICY_ID AND                      
                     POLICY_VERSION_ID =@POLICY_VERSION_ID          
 end          
 else          
 begin           
    update pol_auto_gen_info set IS_COST_OVER_DEFINED_LIMIT=0,IS_COST_OVER_DEFINED_LIMIT_DESC='' where           
                    CUSTOMER_ID  =@CUSTOMER_ID AND                      
                     POLICY_ID   =@POLICY_ID AND                      
                     POLICY_VERSION_ID =@POLICY_VERSION_ID          
 end          
               
end          
            
         
END                      
                    
                  
    
  



GO

