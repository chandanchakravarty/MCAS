  
 /*----------------------------------------------------------                    
Proc Name       : dbo.[[Proc_InsertRuleCollection]]            
Created by      : ADITYA GOEL        
Date            : 22/09/2011                    
Purpose         :INSERT RECORDS IN MNT_RULE_COLLECTION_DETAILS TABLE.                    
Revison History :                    
Used In        : Ebix Advantage                    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------    
DROP PROC dbo.[Proc_InsertRuleCollection]      
    
*/     
/****** Script for POL_REMUNERATION into DATABASE  ******/         
    
CREATE PROC [dbo].[Proc_InsertRuleCollection]    
(      
@RULE_COLLECTION_ID INT OUT,  
@EFFECTIVE_FROM DATETIME,    
@EFFECTIVE_TO DATETIME,    
@COUNTRY_ID INT,
@STATE_ID INT,
@LOB_ID INT,
@SUB_LOB_ID INT,  
@VALIDATION_ORDER DECIMAL(9,2), 
@VALIDATE_NEXT_IF_FAILED char(1),
@RULE_XML_PATH NVARCHAR(255)=NULL,  
@CREATED_BY INT,    
@CREATED_DATETIME DATETIME    
    
)    
As              
BEGIN     
  
    
 INSERT INTO MNT_RULE_COLLECTION_DETAILS      
 (      
 
  EFFECTIVE_FROM,    
  EFFECTIVE_TO,    
  COUNTRY_ID,    
  STATE_ID,
  LOB_ID,
  SUB_LOB_ID,
  VALIDATION_ORDER,
  VALIDATE_NEXT_IF_FAILED,
  RULE_XML_PATH,
  IS_ACTIVE,    
  CREATED_BY,    
  CREATED_DATETIME      
 )    
 VALUES              
 (     

   @EFFECTIVE_FROM,
   @EFFECTIVE_TO,
   @COUNTRY_ID,
   @STATE_ID,
   @LOB_ID,
   @SUB_LOB_ID, 
   @VALIDATION_ORDER,
   @VALIDATE_NEXT_IF_FAILED,
   @RULE_XML_PATH,
  '1',    
  @CREATED_BY,    
  @CREATED_DATETIME    
     
 )  
 SET @RULE_COLLECTION_ID = 1  

END    