IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_DUMMY_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_DUMMY_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*Proc Name     : dbo.Proc_InsertCLM_DUMMY_POLICY                                        
Created by      : Sumit Chhabra                                        
Date            : 4/26/2006                                        
Purpose       :Insert                                        
Revison History :                                        
Used In        : Wolverine                                        
                    
Modified By :            
Modified On :            
Purpose     :            
                         
------------------------------------------------------------                                        
Date     Review By          Comments                                        
------   ------------       -------------------------*/                                        
--drop PROC dbo.Proc_InsertCLM_DUMMY_POLICY                                 
create PROC dbo.Proc_InsertCLM_DUMMY_POLICY                                 
(                                        
@DUMMY_POLICY_ID int output,                                         
@CLAIM_ID int,            
@INSURED_NAME varchar(100),            
@POLICY_NUMBER varchar(10) = null,            
@LOB_ID int,            
@EFFECTIVE_DATE datetime,            
@EXPIRATION_DATE datetime,            
@NOTES varchar(500),            
@CREATED_BY int,            
@CREATED_DATETIME datetime,              
@DUMMY_ADD1 nvarchar(150),  
@DUMMY_ADD2 nvarchar(150),  
@DUMMY_CITY varchar(70),  
@DUMMY_ZIP nvarchar(20),  
@DUMMY_STATE nvarchar(20),  
@DUMMY_COUNTRY nvarchar(10)  
)                                  
AS                                        
BEGIN                                        
 SELECT @DUMMY_POLICY_ID = ISNULL(MAX(DUMMY_POLICY_ID),0)+1 FROM CLM_DUMMY_POLICY            
 INSERT INTO CLM_DUMMY_POLICY            
 (            
  DUMMY_POLICY_ID,            
  CLAIM_ID,            
  INSURED_NAME,            
  POLICY_NUMBER,            
  LOB_ID,            
  EFFECTIVE_DATE,            
  EXPIRATION_DATE,            
  NOTES,            
  CREATED_BY,            
  CREATED_DATETIME,            
  IS_ACTIVE,  
  DUMMY_ADD1,  
  DUMMY_ADD2,  
  DUMMY_CITY,  
  DUMMY_ZIP,  
  DUMMY_STATE,  
  DUMMY_COUNTRY  
            
 )            
 VALUES            
 (            
  @DUMMY_POLICY_ID,            
  @CLAIM_ID,            
  @INSURED_NAME,            
  @POLICY_NUMBER,            
  @LOB_ID,            
  @EFFECTIVE_DATE,  
  @EXPIRATION_DATE,            
  @NOTES,            
  @CREATED_BY,            
  @CREATED_DATETIME,            
  'Y',  
  @DUMMY_ADD1,  
  @DUMMY_ADD2,  
  @DUMMY_CITY,  
  @DUMMY_ZIP,  
  @DUMMY_STATE,  
  @DUMMY_COUNTRY  
            
 )            
--update corresponding data in CLM_CLAIM_INFO table correspoding to claim number and claim id        
/* UPDATE           
  CLM_CLAIM_INFO           
 SET           
  DUMMY_POLICY_ID = @DUMMY_POLICY_ID           
 WHERE           
  --CLAIM_NUMBER=@POLICY_NUMBER AND          
  CLAIM_ID = @CLAIM_ID          

*/
END                                
                                        
                                        
                                      
                                      
                                      
                                      
                                 
  



GO

