IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_ACTIVITY_RESERVE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_ACTIVITY_RESERVE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--DROP PROC dbo.Proc_InsertCLM_ACTIVITY_RESERVE
--go
/*----------------------------------------------------------                                                                                      
Proc Name       : dbo.Proc_InsertCLM_ACTIVITY_RESERVE                                                                          
Created by      : Sumit Chhabra                                                                                    
Date            : 30/05/2006                                                                                      
Purpose         : Insert data in CLM_ACTIVITY_RESERVE table for claim reserve screen                                                                  
Created by      : Sumit Chhabra                                                                                     
Revison History :                                                                                      
Used In        : Wolverine                                                                                      
----------------------------------------------------------------------          
Modified by  : Asfa        
Date  : 05-Sept-2007        
Purpose  : Not to Allow duplicate endorsements to be inserted        
----------------------------------------------------------------------                                                                                      
Date     Review By          Comments                                                                                      
------   ------------       -------------------------*/                                                                                      
--DROP PROC dbo.Proc_InsertCLM_ACTIVITY_RESERVE              
CREATE PROC dbo.Proc_InsertCLM_ACTIVITY_RESERVE                                                                    (        
@CLAIM_ID int,                                
@ACTIVITY_ID int,                                
@COVERAGE_ID int,                                
@PRIMARY_EXCESS varchar(20),                                
@MCCA_ATTACHMENT_POINT decimal(20,2),                                
@MCCA_APPLIES decimal(20,2),                                 
@ATTACHMENT_POINT decimal(20,2),                                
@OUTSTANDING decimal(20,2),                                
@REINSURANCE_CARRIER int,                                
@RI_RESERVE decimal(20,2),                                
@CREATED_BY int,                                
@CREATED_DATETIME datetime,                            
@VEHICLE_ID int,              
@POLICY_LIMITS decimal(20,2),              
@RETENTION_LIMITS decimal(20,2),              
@ACTION_ON_PAYMENT INT = NULL,              
@CRACCTS INT = NULL,              
@DRACCTS INT = NULL,  
@TRANSACTION_ID INT,
@ACTUAL_RISK_ID INT = NULL,
@ACTUAL_RISK_TYPE VARCHAR(10) = NULL    
)           
AS                                                                                      
BEGIN                                    
      
DECLARE @RESERVE_ID int                            
DECLARE @OLD_CLAIM_RESERVE_AMOUNT DECIMAL(20,2)  
---Added  For Itrack Issue #6274,6372 on 23-sep-2009
DECLARE @OLD_CLAIM_REINSURANCE_AMOUNT DECIMAL(20,2)    
DECLARE @NEW_RESERVE_ACTIVITY_ID INT       
DECLARE @NEW_RESERVE INT 
---Added  For Itrack Issue #6274,6372 on 23-sep-2009
DECLARE @CLOSE_RI_RESERVE INT 
DECLARE @OLD_OUTSTANDING DECIMAL(20,2)    
DECLARE @OLD_RI_RESERVE DECIMAL(20,2)    
DECLARE @TRANSACTION_CATEGORY VARCHAR(20)    
      
SET  @NEW_RESERVE = 165                        
SET  @CLOSE_RI_RESERVE = 171  ---Added  For Itrack Issue #6274,6372 on 23-sep-2009                   
                                                          
 SELECT                                                                   
  @RESERVE_ID = ISNULL(MAX(RESERVE_ID),0)+1 -- , @TRANSACTION_ID=ISNULL(MAX(TRANSACTION_ID),0)+1                   
 FROM                                                                   
  CLM_ACTIVITY_RESERVE                                         
 WHERE                   
  CLAIM_ID=@CLAIM_ID                        
                                                                
/* Added by Asfa(05-Sept-2007)            
In Reference : iTrack issue #2371            
Purpose : Not to Allow duplicate endorsements to be inserted  */           
            
IF EXISTS(SELECT RESERVE_ID FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND ACTION_ON_PAYMENT=@ACTION_ON_PAYMENT AND COVERAGE_ID=@COVERAGE_ID AND VEHICLE_ID=@VEHICLE_ID AND ACTUAL_RISK_ID = @ACTUAL_RISK_ID 
AND ACTUAL_RISK_TYPE = @ACTUAL_RISK_TYPE)            
BEGIN            
  DELETE FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND ACTION_ON_PAYMENT=@ACTION_ON_PAYMENT AND COVERAGE_ID=@COVERAGE_ID AND VEHICLE_ID=@VEHICLE_ID AND ACTUAL_RISK_ID = @ACTUAL_RISK_ID 
  AND ACTUAL_RISK_TYPE = @ACTUAL_RISK_TYPE   
      
/* Added by Asfa(05-Oct-2007)            
In Reference : iTrack issue #2569 */           
      
  SET @OLD_CLAIM_RESERVE_AMOUNT = 0           
  SET @NEW_RESERVE_ACTIVITY_ID = 0      
        
  IF(@ACTION_ON_PAYMENT=@NEW_RESERVE)      
  BEGIN      
	 SELECT @NEW_RESERVE_ACTIVITY_ID = ACTIVITY_ID FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND	
	 ACTION_ON_PAYMENT=@NEW_RESERVE      
      
	 SELECT @OLD_CLAIM_RESERVE_AMOUNT = CLAIM_RESERVE_AMOUNT FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND 
	 ACTIVITY_ID=@NEW_RESERVE_ACTIVITY_ID      
        
	 UPDATE CLM_ACTIVITY SET                           
	 CLAIM_RESERVE_AMOUNT = ISNULL(@OLD_CLAIM_RESERVE_AMOUNT,0) - ISNULL(@OUTSTANDING,0)                          
	 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID = @NEW_RESERVE_ACTIVITY_ID        
  END      
---Added  For Itrack Issue #6274,6372 on 23-sep-2009
  ELSE IF(@ACTION_ON_PAYMENT=@CLOSE_RI_RESERVE)
  BEGIN
	 SELECT TOP 1 @OLD_CLAIM_REINSURANCE_AMOUNT = CLAIM_RI_RESERVE FROM CLM_ACTIVITY 
	 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID       
	 
	 UPDATE CLM_ACTIVITY SET                           
	 CLAIM_RI_RESERVE = ISNULL(@OLD_CLAIM_REINSURANCE_AMOUNT,0) - ISNULL(RI_RESERVE,0)	
	 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID = @ACTIVITY_ID


  END
  ELSE      
  BEGIN      
	 SELECT TOP 1 @OLD_CLAIM_RESERVE_AMOUNT = CLAIM_RESERVE_AMOUNT FROM CLM_ACTIVITY 
	 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID        
	 
	 UPDATE CLM_ACTIVITY SET                           
	 CLAIM_RESERVE_AMOUNT = ISNULL(@OLD_CLAIM_RESERVE_AMOUNT,0) - ISNULL(@OUTSTANDING,0)	
	 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID = @ACTIVITY_ID    
  
  END      
END            
   
SELECT TOP 1 @OLD_OUTSTANDING = ISNULL(OUTSTANDING,0) FROM CLM_ACTIVITY_RESERVE  
WHERE CLAIM_ID=@CLAIM_ID AND COVERAGE_ID=@COVERAGE_ID  AND VEHICLE_ID=@VEHICLE_ID AND ACTUAL_RISK_ID = @ACTUAL_RISK_ID AND ACTUAL_RISK_TYPE = @ACTUAL_RISK_TYPE AND IS_ACTIVE='Y'  
ORDER BY ACTIVITY_ID DESC  
                                                                 
  
SELECT TOP 1 @OLD_RI_RESERVE = ISNULL(RI_RESERVE,0) FROM CLM_ACTIVITY_RESERVE  
WHERE CLAIM_ID=@CLAIM_ID AND COVERAGE_ID=@COVERAGE_ID  AND VEHICLE_ID=@VEHICLE_ID AND ACTUAL_RISK_ID = @ACTUAL_RISK_ID AND ACTUAL_RISK_TYPE = @ACTUAL_RISK_TYPE AND IS_ACTIVE='Y'  
ORDER BY ACTIVITY_ID DESC  
  
SELECT @TRANSACTION_CATEGORY = TRANSACTION_CATEGORY FROM CLM_TYPE_DETAIL WHERE DETAIL_TYPE_ID = @ACTION_ON_PAYMENT;  
  
IF(@TRANSACTION_CATEGORY = 'GENERAL')  
BEGIN  
 INSERT INTO CLM_ACTIVITY_RESERVE                                                       
 (                                                                  
  CLAIM_ID,                                
  RESERVE_ID,                                
  ACTIVITY_ID,            
  COVERAGE_ID,                                
  PRIMARY_EXCESS,                                
  MCCA_ATTACHMENT_POINT,                                
  MCCA_APPLIES,                               
  ATTACHMENT_POINT,                                
  OUTSTANDING,              
  REINSURANCE_CARRIER,                                
  RI_RESERVE,                                
  CREATED_BY,                                
  CREATED_DATETIME,                                
  IS_ACTIVE,                            
  VEHICLE_ID,                
  MODIFIED_BY,                
  LAST_UPDATED_DATETIME,              
  POLICY_LIMITS,              
  RETENTION_LIMITS,           
  ACTION_ON_PAYMENT,              
  CRACCTS,              
  DRACCTS,    
  TRANSACTION_ID,
  ACTUAL_RISK_ID,
  ACTUAL_RISK_TYPE              
 )                                                                  
 VALUES                                                                  
 (                                                                  
  @CLAIM_ID,                                
  @RESERVE_ID,                                
  @ACTIVITY_ID,                                
  @COVERAGE_ID,                                
  @PRIMARY_EXCESS,                                
  @MCCA_ATTACHMENT_POINT,                                
  @MCCA_APPLIES,                       
  @ATTACHMENT_POINT,                                
  @OUTSTANDING,                                
  @REINSURANCE_CARRIER,                  
--  @RI_RESERVE,              
  @OLD_RI_RESERVE,  
  @CREATED_BY,                                
  @CREATED_DATETIME,                                
  'Y',                            
  @VEHICLE_ID,                
  @CREATED_BY,                                
  @CREATED_DATETIME,              
  @POLICY_LIMITS,              
  @RETENTION_LIMITS,              
  @ACTION_ON_PAYMENT,              
  @CRACCTS,              
  @DRACCTS,    
  @TRANSACTION_ID,
  @ACTUAL_RISK_ID,
  @ACTUAL_RISK_TYPE                        
 )                         
END  
ELSE IF(@TRANSACTION_CATEGORY = 'REINSURANCE')  
BEGIN  
 INSERT INTO CLM_ACTIVITY_RESERVE                                                       
 (                                                                  
  CLAIM_ID,                                
  RESERVE_ID,                                
  ACTIVITY_ID,            
  COVERAGE_ID,                                
  PRIMARY_EXCESS,                                
  MCCA_ATTACHMENT_POINT,                                
  MCCA_APPLIES,                               
  ATTACHMENT_POINT,                                
  OUTSTANDING,                                
  REINSURANCE_CARRIER,                                
  RI_RESERVE,                                
  CREATED_BY,                                
  CREATED_DATETIME,                                
  IS_ACTIVE,                            
  VEHICLE_ID,                
  MODIFIED_BY,                
  LAST_UPDATED_DATETIME,              
  POLICY_LIMITS,              
  RETENTION_LIMITS,              
  ACTION_ON_PAYMENT,              
  CRACCTS,              
  DRACCTS,    
  TRANSACTION_ID,
  ACTUAL_RISK_ID,
  ACTUAL_RISK_TYPE              
 )                                                                  
 VALUES                                                                  
 (                                                                  
  @CLAIM_ID,                                
  @RESERVE_ID,                                
  @ACTIVITY_ID,                                
  @COVERAGE_ID,                                
  @PRIMARY_EXCESS,                                
  @MCCA_ATTACHMENT_POINT,                                
  @MCCA_APPLIES,                       
  @ATTACHMENT_POINT,                                
--  @OUTSTANDING,                                
  @OLD_OUTSTANDING,  
  @REINSURANCE_CARRIER,                  
  @RI_RESERVE,                                
  @CREATED_BY,                                
  @CREATED_DATETIME,                                
  'Y',  
  @VEHICLE_ID,                
  @CREATED_BY,                                
  @CREATED_DATETIME,              
  @POLICY_LIMITS,              
  @RETENTION_LIMITS,              
  @ACTION_ON_PAYMENT,              
  @CRACCTS,              
  @DRACCTS,    
  @TRANSACTION_ID,
  @ACTUAL_RISK_ID,
  @ACTUAL_RISK_TYPE                        
 )                         
  
END                                
                    
END 

--go
--exec Proc_InsertCLM_ACTIVITY_RESERVE
--rollback tran

GO

