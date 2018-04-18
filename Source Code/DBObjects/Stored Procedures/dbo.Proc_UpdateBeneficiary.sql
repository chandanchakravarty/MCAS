IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateBeneficiary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateBeneficiary]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
--Proc Name       : dbo.Proc_UpdateBeneficiary    
--Created by      : ADITYA GOEL        
--Date            : 24/02/2011                  
--Purpose   :To Update Reinsurer in POL_BENEFICIARY      
--Revison History :                  
--Used In   : Ebix Advantage                  
--------------------------------------------------------------                  
--Date     Review By          Comments                  
--------   ------------       -------------------------*/                  
--DROP PROC dbo.Proc_UpdateBeneficiary  
CREATE PROC [dbo].[Proc_UpdateBeneficiary]  
(   
@CUSTOMER_ID  INT,        
@POLICY_ID INT,        
@POLICY_VERSION_ID INT,   
@RISK_ID int,         
@BENEFICIARY_ID INT,      
@BENEFICIARY_NAME NVARCHAR(100),      
@BENEFICIARY_SHARE DECIMAL(12, 2),      
@BENEFICIARY_RELATION NVARCHAR(100),    
@MODIFIED_BY INT,      
@LAST_UPDATED_DATETIME DATETIME      
      
)      
AS 
DECLARE @SUM numeric(12,2)               
     
BEGIN  

SELECT  @SUM=sum(BENEFICIARY_SHARE) +   @BENEFICIARY_SHARE 
  FROM   [POL_BENEFICIARY]    
  WHERE   CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND RISK_ID=@RISK_ID 
  AND BENEFICIARY_ID<>@BENEFICIARY_ID    
  IF(@SUM > 100)   
   BEGIN          
     RETURN -2 
     END     
      
 UPDATE POL_BENEFICIARY      
 SET      
 BENEFICIARY_NAME = @BENEFICIARY_NAME ,      
 BENEFICIARY_SHARE = @BENEFICIARY_SHARE,      
 BENEFICIARY_RELATION = @BENEFICIARY_RELATION,      
 MODIFIED_BY = @MODIFIED_BY,      
 LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME    
  WHERE     
  CUSTOMER_ID = @CUSTOMER_ID AND  
  POLICY_ID = @POLICY_ID AND  
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND   
  RISK_ID = @RISK_ID AND  
 BENEFICIARY_ID = @BENEFICIARY_ID    
 return 1  
END   
GO

