  
--Proc Name       : dbo.Proc_UpdateRuleCollection    
--Created by      : ADITYA GOEL      
--Date            : 21/12/2010                
--Purpose   :To Update Reinsurer in MNT_RULE_COLLECTION_DETAILS    
--Revison History :                
--Used In   : Ebix Advantage                
--------------------------------------------------------------                
--Date     Review By          Comments                
--------   ------------       -------------------------*/                
--DROP PROC dbo.Proc_UpdateRuleCollection    
CREATE PROC [dbo].[Proc_UpdateRuleCollection]    
(     
@RULE_COLLECTION_ID INT,    
@EFFECTIVE_FROM DATETIME,    
@EFFECTIVE_TO DATETIME,    
@COUNTRY_ID INT,
@STATE_ID INT,
@LOB_ID INT,   
@SUB_LOB_ID INT, 
@VALIDATION_ORDER DECIMAL(9,2),
@RULE_XML_PATH NVARCHAR(255)=NULL,  
@VALIDATE_NEXT_IF_FAILED char(1),
@MODIFIED_BY INT,    
@LAST_UPDATED_DATETIME DATETIME    
    
)    
AS    
BEGIN    
    
 UPDATE MNT_RULE_COLLECTION_DETAILS     
 SET    
 EFFECTIVE_FROM = @EFFECTIVE_FROM,    
 EFFECTIVE_TO = @EFFECTIVE_TO,    
 COUNTRY_ID = @COUNTRY_ID,    
 STATE_ID = @STATE_ID,
 LOB_ID = @LOB_ID,
 SUB_LOB_ID = @SUB_LOB_ID,
 VALIDATION_ORDER =@VALIDATION_ORDER,
 VALIDATE_NEXT_IF_FAILED = @VALIDATE_NEXT_IF_FAILED,
 RULE_XML_PATH  = @RULE_XML_PATH,
 MODIFIED_BY = @MODIFIED_BY,    
 LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME    
 WHERE RULE_COLLECTION_ID = @RULE_COLLECTION_ID    
END    







