IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertBeneficiary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertBeneficiary]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
 /*----------------------------------------------------------                      
Proc Name       : dbo.[[Proc_InsertBeneficiary]]              
Created by      : ADITYA GOEL          
Date            : 24/02/2011                      
Purpose         :INSERT RECORDS IN POL_BENEFICIARY TABLE.                      
Revison History :                      
Used In        : Ebix Advantage                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------      
DROP PROC dbo.[Proc_InsertBeneficiary] 2,4,5,6,7,5   
      
*/       
/****** Script for POL_BENEFICIARY into DATABASE  ******/           
      
CREATE PROC [dbo].[Proc_InsertBeneficiary]  
(    
@CUSTOMER_ID     int,                                        
@POLICY_ID     int,                                        
@POLICY_VERSION_ID     int,    
@RISK_ID int,      
@BENEFICIARY_ID INT OUT,      
@BENEFICIARY_NAME NVARCHAR(100),      
@BENEFICIARY_SHARE DECIMAL(12, 2),      
@BENEFICIARY_RELATION NVARCHAR(100),    
@CREATED_BY INT,      
@CREATED_DATETIME DATETIME      
      
)      
As  
DECLARE @SUM numeric(12,2)                
BEGIN     

 SELECT  @SUM=sum(BENEFICIARY_SHARE) + @BENEFICIARY_SHARE
  FROM   [POL_BENEFICIARY]    
  WHERE   CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND RISK_ID=@RISK_ID 
  IF(@SUM > 100)   
   BEGIN          
     RETURN -2 
     END 
  
  ELSE
  BEGIN    
SELECT  @BENEFICIARY_ID=ISNULL(MAX(BENEFICIARY_ID),0)+1 FROM POL_BENEFICIARY with(nolock)    
 WHERE   CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND RISK_ID=@RISK_ID  --  -- changed by praveer for TFS # 2393   
      
 INSERT INTO POL_BENEFICIARY              
 (      
 CUSTOMER_ID,                                        
 POLICY_ID,                                        
 POLICY_VERSION_ID,    
 RISK_ID,    
  BENEFICIARY_ID ,      
  BENEFICIARY_NAME,      
  BENEFICIARY_SHARE,      
  BENEFICIARY_RELATION,      
  IS_ACTIVE,      
  CREATED_BY,      
  CREATED_DATETIME        
 )      
 VALUES                
 (     
 @CUSTOMER_ID,                                        
 @POLICY_ID,                                        
 @POLICY_VERSION_ID,    
 @RISK_ID,    
  @BENEFICIARY_ID ,      
  @BENEFICIARY_NAME,      
  @BENEFICIARY_SHARE ,      
  @BENEFICIARY_RELATION ,      
  'Y',      
  @CREATED_BY,      
  @CREATED_DATETIME      
       
 )  
 return 1 
 END         
END   
GO

