  
--Proc Name       : dbo.Proc_UpdateInterestRates    
--Created by      : ADITYA GOEL      
--Date            : 03/11/2011              
--Purpose   :To Update in MNT_INTEREST_RATES    
--Revison History :                
--Used In   : Ebix Advantage                
--------------------------------------------------------------                
--Date     Review By          Comments                
--------   ------------       -------------------------*/                
--DROP PROC dbo.Proc_UpdateInterestRates    
CREATE PROC [dbo].[Proc_UpdateInterestRates]    
(     
@INTEREST_RATE_ID INT,  
@RATE_EFFECTIVE_DATE DATETIME,    
@NO_OF_INSTALLMENTS INT,
@INTEREST_TYPE NVARCHAR(200),
@INTEREST_RATE DECIMAL(12,4),
@MODIFIED_BY INT,    
@LAST_UPDATED_DATETIME DATETIME      
)    
AS    
BEGIN    
    
 UPDATE MNT_INTEREST_RATES     
 SET    
 RATE_EFFECTIVE_DATE = @RATE_EFFECTIVE_DATE,       
 NO_OF_INSTALLMENTS = @NO_OF_INSTALLMENTS,    
 INTEREST_TYPE = @INTEREST_TYPE,
 INTEREST_RATE = @INTEREST_RATE,
 MODIFIED_BY = @MODIFIED_BY,    
 LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME    
 WHERE INTEREST_RATE_ID = @INTEREST_RATE_ID    
END    







