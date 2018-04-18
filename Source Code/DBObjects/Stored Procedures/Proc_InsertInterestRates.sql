  
 /*----------------------------------------------------------                    
Proc Name       : dbo.[[Proc_InsertInterestRates]]            
Created by      : ADITYA GOEL        
Date            : 03/11/2011                    
Purpose         :INSERT RECORDS IN MNT_INTEREST_RATES TABLE.                    
Revison History :                    
Used In        : Ebix Advantage                    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------    
DROP PROC dbo.[Proc_InsertInterestRates]      
    
*/     
/****** Script for MNT_INTEREST_RATES into DATABASE  ******/         
    
CREATE PROC [dbo].[Proc_InsertInterestRates]    
(      
@INTEREST_RATE_ID INT OUT,  
@RATE_EFFECTIVE_DATE DATETIME,    
@NO_OF_INSTALLMENTS INT,
@INTEREST_TYPE NVARCHAR(200),
@INTEREST_RATE DECIMAL(12,4),
@CREATED_BY INT,    
@CREATED_DATETIME DATETIME    
    
)    
As              
BEGIN   
  
SELECT  @INTEREST_RATE_ID=ISNULL(MAX(INTEREST_RATE_ID),0)+1 FROM MNT_INTEREST_RATES    
    
 INSERT INTO MNT_INTEREST_RATES      
 (      
  INTEREST_RATE_ID,
  RATE_EFFECTIVE_DATE,    
  NO_OF_INSTALLMENTS,    
  INTEREST_TYPE,    
  INTEREST_RATE,
  IS_ACTIVE,    
  CREATED_BY,    
  CREATED_DATETIME      
 )    
 VALUES              
 (  
   @INTEREST_RATE_ID,   
   @RATE_EFFECTIVE_DATE,
   @NO_OF_INSTALLMENTS,
   @INTEREST_TYPE,
   @INTEREST_RATE,
   'Y',    
   @CREATED_BY,    
   @CREATED_DATETIME    
     
 )  

END    