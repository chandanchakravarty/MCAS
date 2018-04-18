/*----------------------------------------------------------          
Proc Name       : PROC_GenerateAccumulationReferenceCode          
Created by      : RUCHIKA CHAUHAN       
Date            : 2 MARCH 2012    
Purpose   : Generates Accumulation Reference Code for Accumulation Reference on the basis of LOB  
  
Revison History :          
Used In  : EbixAdvantage - Singapore    
          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       --------------------------------*/          
-- DROP PROC PROC_GenerateAccumulationReferenceCode  
--/*    
    
    
CREATE PROC PROC_GenerateAccumulationReferenceCode  

@LOB_ID INT

AS    
BEGIN
  
	DECLARE @CODE_PREFIX NVARCHAR(10),	 
	@ROW_COUNT INT
	
	SELECT @ROW_COUNT = COUNT(LOB_ID)+1 FROM MNT_ACCUMULATION_REFERENCE 
	
	SET @CODE_PREFIX = 	
	CASE @LOB_ID	
		--FOR FIRE
		WHEN 1 THEN 'F'
		--FOR MOTOR
		WHEN 38 THEN 'V'
		--FOR MARINE CARGO
		WHEN 40 THEN 'MC'
		--FOR AVIATION
		WHEN 8 THEN 'A'
	END; 
	
	IF(@ROW_COUNT < 10)
	BEGIN
		SELECT @CODE_PREFIX + '000' +CAST(@ROW_COUNT AS NVARCHAR)		
	END 
	ELSE IF(@ROW_COUNT < 100)
	BEGIN
		SELECT @CODE_PREFIX + '00' +CAST(@ROW_COUNT AS NVARCHAR)				
	END
	ELSE IF(@ROW_COUNT < 1000)
	BEGIN
		SELECT @CODE_PREFIX + '0' +CAST(@ROW_COUNT AS NVARCHAR)		
	END
	ELSE IF(@ROW_COUNT < 10000)
	BEGIN
		SELECT CAST((@CODE_PREFIX + @ROW_COUNT) AS NVARCHAR )	
	END	
	ELSE
	BEGIN
		SELECT CAST((@CODE_PREFIX + @ROW_COUNT) AS NVARCHAR )
	END		  
END  

--exec PROC_GenerateAccumulationReferenceCode 8
 