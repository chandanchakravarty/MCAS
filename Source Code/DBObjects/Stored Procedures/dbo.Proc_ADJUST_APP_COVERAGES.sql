IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ADJUST_APP_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ADJUST_APP_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_ADJUST_APP_COVERAGES            
            
/*----------------------------------------------------------                                        
Proc Name       : dbo.Proc_ADJUST_APP_COVERAGES                                        
Created by      : Pradeep                                        
Date            : 20/03/2006                                   
Purpose      :  Updates/ deletes coverages when effective date 
		of Policy changes     
Revison History :                                        
Used In  : Wolverine                                        
------------------------------------------------------------                                        
Date     Review By          Comments                                        
------   ------------       -------------------------*/                                        
CREATE           PROC Dbo.Proc_ADJUST_APP_COVERAGES
(                                        
 @CUSTOMER_ID     int,                                        
 @APP_ID     int,                                        
 @APP_VERSION_ID     smallint                                        
                 
)                                        
AS                                        
                                        
BEGIN        
         
	DECLARE @EFFECTIVE_DATE DateTime
	DECLARE @LOB_ID Int	
	DECLARE @STATE_ID Int	

	SELECT  @EFFECTIVE_DATE = APP_EFFECTIVE_DATE,
		@LOB_ID = APP_LOB,
		@STATE_ID = STATE_ID	
	FROM APP_LIST
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID = @APP_ID AND
		APP_VERSION_ID = @APP_VERSION_ID
	
	--Get coverages not falling in range
	DECLARE @TEMP_COV TABLE  
 	(  
  		COV_ID Int
	)
	
	INSERT INTO @TEMP_COV
	SELECT COV_ID
	FROM MNT_COVERAGE
	WHERE  EFFECTIVE_FROM_DATE > @EFFECTIVE_DATE OR
		EFFECTIVE_TO_DATE < @EFFECTIVE_DATE
        AND LOB_ID = @LOB_ID AND
		STATE_ID = @STATE_ID
	
	--Auto, Motor
	IF (  @LOB_ID IN ( 2,3 ) )
	BEGIN
		IF EXISTS
		(
			SELECT * FROM APP_VEHICLE_COVERAGES
			WHERE  CUSTOMER_ID = @CUSTOMER_ID AND
				APP_ID = @APP_ID AND
				APP_VERSION_ID = @APP_VERSION_ID AND
				COVERAGE_CODE_ID IN 
				(
					SELECT COV_ID FROM @TEMP_COV
				)	
		)
		DELETE FROM APP_VEHICLE_COVERAGES
			WHERE  CUSTOMER_ID = @CUSTOMER_ID AND
				APP_ID = @APP_ID AND
				APP_VERSION_ID = @APP_VERSION_ID AND
				COVERAGE_CODE_ID IN 
				(
					SELECT COV_ID FROM @TEMP_COV
				)
	END 
	
	--Home, Rental
	 IF (  @LOB_ID IN ( 1,6 ) )
	BEGIN
		IF EXISTS
		(
			SELECT * FROM DWELLING_SECTION_COVERAGES
			WHERE  CUSTOMER_ID = @CUSTOMER_ID AND
				APP_ID = @APP_ID AND
				APP_VERSION_ID = @APP_VERSION_ID AND
				COVERAGE_CODE_ID IN 
				(
					SELECT COV_ID FROM @TEMP_COV
				)	
		)
		DELETE FROM DWELLING_SECTION_COVERAGES
			WHERE  CUSTOMER_ID = @CUSTOMER_ID AND
				APP_ID = @APP_ID AND
				APP_VERSION_ID = @APP_VERSION_ID AND
				COVERAGE_CODE_ID IN 
				(
					SELECT COV_ID FROM @TEMP_COV
				)
	END  
	
	--Watercraft
	 IF (  @LOB_ID IN ( 4 ) )
	BEGIN
		IF EXISTS
		(
			SELECT * FROM APP_WATERCRAFT_COVERAGE_INFO
			WHERE  CUSTOMER_ID = @CUSTOMER_ID AND
				APP_ID = @APP_ID AND
				APP_VERSION_ID = @APP_VERSION_ID AND
				COVERAGE_CODE_ID IN 
				(
					SELECT COV_ID FROM @TEMP_COV
				)	
		)
		DELETE FROM DWELLING_SECTION_COVERAGES
			WHERE  CUSTOMER_ID = @CUSTOMER_ID AND
				APP_ID = @APP_ID AND
				APP_VERSION_ID = @APP_VERSION_ID AND
				COVERAGE_CODE_ID IN 
				(
					SELECT COV_ID FROM @TEMP_COV
				)
	END  
	           
END                     
                     
                       
                        
                    
                                   
                                        
                                        
                                      
                                      
                                      
                                    
                                  
                                
                              
                            
                          
                        
                      
                    
                  
                
              
            
          
        
      
    
  



GO

