IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_ACTIVITY_RESERVE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_ACTIVITY_RESERVE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                        
Proc Name       : dbo.Proc_UpdateCLM_ACTIVITY_RESERVE                                                            
Created by      : Sumit Chhabra                                                                      
Date            : 31/05/2006                                                                        
Purpose         : Update data at CLM_ACTIVITY_RESERVE table for claim reserve screen                                                    
Created by      : Sumit Chhabra                                                                       
Revison History :                                                                        
Used In        : Wolverine                                                                        
------------------------------------------------------------                                                                        
Modified by  : Asfa Praveen  
Date   : 28-Sept-2007  
Purpose  : To add Vehicle_id as update condition  
------------------------------------------------------------                                                                        
Date     Review By          Comments                                                                        
------   ------------       -------------------------*/           
--DROP PROC dbo.Proc_UpdateCLM_ACTIVITY_RESERVE                                                                   
CREATE PROC [dbo].[Proc_UpdateCLM_ACTIVITY_RESERVE]                                                              
@CLAIM_ID int,            
@RESERVE_ID int,            
@ACTIVITY_ID int,            
@COVERAGE_ID int,            
@PRIMARY_EXCESS varchar(20),            
@MCCA_ATTACHMENT_POINT decimal(10,2),            
@MCCA_APPLIES decimal(10,2),            
@ATTACHMENT_POINT decimal(10,2),            
@OUTSTANDING decimal(18,2),            
@REINSURANCE_CARRIER int,            
@RI_RESERVE decimal(18,2),            
@MODIFIED_BY int,            
@LAST_UPDATED_DATETIME datetime,              
@VEHICLE_ID int,        
@POLICY_LIMITS decimal(10),      
@RETENTION_LIMITS decimal(10,2),      
@ACTION_ON_PAYMENT INT=NULL,      
@CRACCTS INT = NULL,      
@DRACCTS INT = NULL,
@TRANSACTION_ID INT,
@ACTUAL_RISK_ID INT = NULL,
@ACTUAL_RISK_TYPE VARCHAR(10) = NULL                                            
AS                                                                        
BEGIN 

DECLARE @OLD_OUTSTANDING DECIMAL(20,2)  
DECLARE @OLD_RI_RESERVE DECIMAL(20,2)  
DECLARE @TRANSACTION_CATEGORY VARCHAR(20)

SELECT TOP 1 @OLD_OUTSTANDING = ISNULL(OUTSTANDING,0) FROM CLM_ACTIVITY_RESERVE
WHERE CLAIM_ID=@CLAIM_ID AND COVERAGE_ID=@COVERAGE_ID AND IS_ACTIVE='Y'
ORDER BY ACTIVITY_ID DESC
                                                               

SELECT TOP 1 @OLD_RI_RESERVE = ISNULL(RI_RESERVE,0) FROM CLM_ACTIVITY_RESERVE
WHERE CLAIM_ID=@CLAIM_ID AND COVERAGE_ID=@COVERAGE_ID AND IS_ACTIVE='Y'
ORDER BY ACTIVITY_ID DESC

SELECT @TRANSACTION_CATEGORY = TRANSACTION_CATEGORY FROM CLM_TYPE_DETAIL WHERE DETAIL_TYPE_ID = @ACTION_ON_PAYMENT;

IF(@TRANSACTION_CATEGORY = 'GENERAL')
BEGIN                                                    
 UPDATE CLM_ACTIVITY_RESERVE                                         
 SET            
  ACTIVITY_ID=@ACTIVITY_ID,            
  COVERAGE_ID=@COVERAGE_ID,            
  PRIMARY_EXCESS=@PRIMARY_EXCESS,            
  MCCA_ATTACHMENT_POINT=@MCCA_ATTACHMENT_POINT,            
  MCCA_APPLIES=@MCCA_APPLIES,            
  ATTACHMENT_POINT=@ATTACHMENT_POINT,            
  OUTSTANDING=@OUTSTANDING,            
  REINSURANCE_CARRIER=@REINSURANCE_CARRIER,            
--  RI_RESERVE=@RI_RESERVE,            
  RI_RESERVE=@OLD_RI_RESERVE,
  @MODIFIED_BY=@MODIFIED_BY,            
  @LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,        
  POLICY_LIMITS=@POLICY_LIMITS,        
  RETENTION_LIMITS=@RETENTION_LIMITS,       
  ACTION_ON_PAYMENT = @ACTION_ON_PAYMENT,      
  CRACCTS = @CRACCTS,      
  DRACCTS = @DRACCTS,
  ACTUAL_RISK_ID = @ACTUAL_RISK_ID,
  ACTUAL_RISK_TYPE = @ACTUAL_RISK_TYPE
                                                          
 WHERE            
  CLAIM_ID=@CLAIM_ID AND RESERVE_ID=@RESERVE_ID AND VEHICLE_ID =@VEHICLE_ID AND           
  ACTIVITY_ID=@ACTIVITY_ID AND ACTION_ON_PAYMENT=@ACTION_ON_PAYMENT AND 
  COVERAGE_ID=@COVERAGE_ID AND TRANSACTION_ID=@TRANSACTION_ID AND 
  ACTUAL_RISK_ID = @ACTUAL_RISK_ID AND ACTUAL_RISK_TYPE = @ACTUAL_RISK_TYPE
END
ELSE IF(@TRANSACTION_CATEGORY = 'REINSURANCE')
BEGIN
 UPDATE CLM_ACTIVITY_RESERVE                                         
 SET            
  ACTIVITY_ID=@ACTIVITY_ID,            
  COVERAGE_ID=@COVERAGE_ID,            
  PRIMARY_EXCESS=@PRIMARY_EXCESS,            
  MCCA_ATTACHMENT_POINT=@MCCA_ATTACHMENT_POINT,            
  MCCA_APPLIES=@MCCA_APPLIES,            
  ATTACHMENT_POINT=@ATTACHMENT_POINT,            
--  OUTSTANDING=@OUTSTANDING,            
  OUTSTANDING=@OLD_OUTSTANDING,
  REINSURANCE_CARRIER=@REINSURANCE_CARRIER,            
  RI_RESERVE=@RI_RESERVE,            
  @MODIFIED_BY=@MODIFIED_BY,            
  @LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,        
  POLICY_LIMITS=@POLICY_LIMITS,        
  RETENTION_LIMITS=@RETENTION_LIMITS,        
  ACTION_ON_PAYMENT = @ACTION_ON_PAYMENT,      
  CRACCTS = @CRACCTS,      
  DRACCTS = @DRACCTS,
  ACTUAL_RISK_ID = @ACTUAL_RISK_ID,
  ACTUAL_RISK_TYPE = @ACTUAL_RISK_TYPE      
                                                          
 WHERE            
  CLAIM_ID=@CLAIM_ID AND RESERVE_ID=@RESERVE_ID AND VEHICLE_ID =@VEHICLE_ID AND           
  ACTIVITY_ID=@ACTIVITY_ID AND ACTION_ON_PAYMENT=@ACTION_ON_PAYMENT AND 
  COVERAGE_ID=@COVERAGE_ID AND TRANSACTION_ID=@TRANSACTION_ID AND 
  ACTUAL_RISK_ID = @ACTUAL_RISK_ID AND ACTUAL_RISK_TYPE = @ACTUAL_RISK_TYPE

END
                     
END

GO

