/*----------------------------------------------------------          
Proc Name       : PROC_GenerateCriteriaCode          
Created by      : RUCHIKA CHAUHAN       
Date            : 1 MARCH 2012    
Purpose   : Generates Criteria Code for Accumulation Criteria   
  
Revison History :          
Used In  : EbixAdvantage - Singapore    
          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       --------------------------------*/          
-- DROP PROC PROC_GenerateCriteriaCode  
--/*    
    
    
CREATE PROC PROC_GenerateCriteriaCode  
AS    
BEGIN   
 
DECLARE @MAX_CRITERIA_COUNT INT,
@CODE NVARCHAR(10)

SELECT @MAX_CRITERIA_COUNT = COUNT(CRITERIA_ID)+1 FROM MNT_ACCUMULATION_CRITERIA_MASTER  
	IF(@MAX_CRITERIA_COUNT < 10)
	BEGIN
		SELECT 'C000' + CAST(@MAX_CRITERIA_COUNT AS NVARCHAR)		
	END
	ELSE IF(@MAX_CRITERIA_COUNT < 100)
	BEGIN
		SELECT 'C00' + CAST(@MAX_CRITERIA_COUNT AS NVARCHAR)				
	END
	ELSE IF(@MAX_CRITERIA_COUNT < 1000)
	BEGIN
		SELECT 'C0' + CAST(@MAX_CRITERIA_COUNT AS NVARCHAR)		
	END
	ELSE IF(@MAX_CRITERIA_COUNT < 10000)
	BEGIN
		SELECT CAST(('C' + @MAX_CRITERIA_COUNT) AS NVARCHAR )	
	END	
	ELSE
	BEGIN
		SELECT CAST(('C' + @MAX_CRITERIA_COUNT) AS NVARCHAR )
	END
END    
  

--exec PROC_GenerateCriteriaCode
 