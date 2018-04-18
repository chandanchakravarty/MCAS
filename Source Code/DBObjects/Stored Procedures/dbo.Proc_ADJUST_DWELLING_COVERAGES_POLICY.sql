IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ADJUST_DWELLING_COVERAGES_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ADJUST_DWELLING_COVERAGES_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_ADJUST_DWELLING_COVERAGES_POLICY          
          
/*----------------------------------------------------------                                      
Proc Name       : dbo.Proc_ADJUST_DWELLING_COVERAGES_POLICY                                      
Created by      : Pradeep                                      
Date            : 02/03/2006                                 
Purpose      :  Called when policy type is changed from       
  Policy page      
Revison History :                                      
Used In  : Wolverine                                      
------------------------------------------------------------                                      
Date     Review By          Comments                                      
------   ------------       -------------------------*/                                      
CREATE           PROC Dbo.Proc_ADJUST_DWELLING_COVERAGES_POLICY                                      
(                                      
 @CUSTOMER_ID     int,                                      
 @POLICY_ID     int,                                      
 @POLICY_VERSION_ID     smallint                                      
               
)                                      
AS                                      
                                      
BEGIN      
            
 DECLARE @DWELL_ID Int      
 DECLARE @REPL_COST Decimal(18,0)    
 DECLARE @MARKET_VALUE Decimal(18,0)    
 DECLARE @PRODUCT_ID Int    
 DECLARE @STATE_ID Int    
 DECLARE @LOB_ID Int    
 DECLARE @EXISTING_MARKET_VALUE Decimal(18,0)    
 DECLARE @EXISTING_REPL_COST Decimal(18,0)    
      
--Get all dwellings  -----------    
 IF NOT EXISTS      
 (      
  SELECT * FROM  POL_DWELLINGS_INFO      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
    POLICY_ID = @POLICY_ID AND          
    POLICY_VERSION_ID = @POLICY_VERSION_ID          
        
        )      
 BEGIN      
  RETURN      
 END      
---------------------    
    
SELECT @PRODUCT_ID = POLICY_TYPE,    
 @STATE_ID = STATE_ID,    
       @LOB_ID = POLICY_LOB    
FROM POL_CUSTOMER_POLICY_LIST    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
    POLICY_ID = @POLICY_ID AND          
    POLICY_VERSION_ID = @POLICY_VERSION_ID     
    
---Set Default values -----------------------------------------    
  
 IF ( @STATE_ID = 22 )    
 BEGIN    
   --Ho-2    
   IF ( @PRODUCT_ID = 11402)    
   BEGIN    
    SET @REPL_COST = 35000    
   END    
       
   --Ho-2 Repair Cost    
   IF ( @PRODUCT_ID = 11403)    
   BEGIN    
    SET @MARKET_VALUE = 15000     
   END    
       
   --Ho-3     
   IF ( @PRODUCT_ID = 11400)    
   BEGIN    
    SET @REPL_COST = 40000    
   END    
       
   --Ho-3 Premier    
   IF ( @PRODUCT_ID = 11409)    
   BEGIN    
    SET @REPL_COST = 175000    
   END    
       
   --HO-3 Repair Cost    
   IF ( @PRODUCT_ID = 11404)    
   BEGIN    
    SET @MARKET_VALUE = 40000     
   END    
       
   --HO-4    
   IF ( @PRODUCT_ID = 11405)    
   BEGIN    
    SET @REPL_COST = 10000    
   END    
       
   --HO-4 Deluxe    
   IF ( @PRODUCT_ID = 11407)    
   BEGIN    
    SET @REPL_COST = 25000    
   END    
       
   --HO-5    
   IF ( @PRODUCT_ID = 11401)    
   BEGIN    
    SET @REPL_COST = 125000    
   END    
       
   --HO-5    
   IF ( @PRODUCT_ID = 11401)    
   BEGIN    
    SET @REPL_COST = 125000    
   END    
       
   --HO-5 Premier    
   IF ( @PRODUCT_ID = 11410)    
   BEGIN    
    SET @REPL_COST = 175000    
   END    
       
   --HO-6    
   IF ( @PRODUCT_ID = 11406)    
   BEGIN    
    SET @REPL_COST = 15000    
   END    
       
   --HO-6 Deluxe    
   IF ( @PRODUCT_ID = 11408)    
   BEGIN    
    SET @REPL_COST = 25000    
   END    
     
   --Rental--------------------    
   --DP-2    
   IF ( @PRODUCT_ID = 11289)    
   BEGIN    
    SET @REPL_COST = 30000    
   END    
       
   --DP-2 Repair Cost     
   IF ( @PRODUCT_ID = 11290)    
   BEGIN    
    SET @REPL_COST = 10000    
   END    
       
   --DP-3    
   IF ( @PRODUCT_ID = 11291)    
   BEGIN    
    SET @REPL_COST = 75000    
   END    
        
   --DP-3 Premier    
   IF ( @PRODUCT_ID = 11458)    
   BEGIN    
    SET @REPL_COST = 75000    
   END    
       
   --DP-3 Repair Cost    
   IF ( @PRODUCT_ID = 11292)    
   BEGIN    
    SET @REPL_COST = 75000    
   END      
  -----------------------------    
    
 END    
       
 IF ( @STATE_ID = 14 )    
 BEGIN    
  --Ho-2    
  IF ( @PRODUCT_ID = 11192)    
  BEGIN    
   SET @REPL_COST = 50000    
  END    
      
  --Ho-2 Repair Cost    
  IF ( @PRODUCT_ID = 11193)    
  BEGIN    
   SET @MARKET_VALUE = 50000     
  END    
      
  --Ho-3     
  IF ( @PRODUCT_ID = 11148)    
  BEGIN    
   SET @REPL_COST = 50000    
  END    
       
  --HO-3 Repair Cost    
  IF ( @PRODUCT_ID = 11194)    
  BEGIN    
   SET @MARKET_VALUE = 50000     
  END    
      
  --HO-4    
  IF ( @PRODUCT_ID = 11195)    
  BEGIN    
   SET @REPL_COST = 10000    
  END    
      
  --HO-4 Deluxe    
  IF ( @PRODUCT_ID = 11245)    
  BEGIN    
   SET @REPL_COST = 25000    
  END    
      
  --HO-5    
  IF ( @PRODUCT_ID = 11149)    
  BEGIN    
   SET @REPL_COST = 125000    
  END    
      
  --HO-6    
  IF ( @PRODUCT_ID = 11196)    
  BEGIN    
   SET @REPL_COST = 15000    
  END    
      
  --HO-6 Deluxe    
  IF ( @PRODUCT_ID = 11246)    
  BEGIN    
   SET @REPL_COST = 25000    
  END    
      
  --Rental--------------------    
  --DP-2    
  IF ( @PRODUCT_ID = 11479)    
  BEGIN    
   SET @REPL_COST = 30000    
  END    
      
  --DP-2 Repair Cost     
  IF ( @PRODUCT_ID = 11480)    
  BEGIN    
   SET @REPL_COST = 10000    
  END    
      
  --DP-3    
  IF ( @PRODUCT_ID = 11481)    
  BEGIN    
   SET @REPL_COST = 75000    
  END    
       
  --DP-3 Premier    
  IF ( @PRODUCT_ID = 11458)    
  BEGIN    
   SET @REPL_COST = 75000    
  END    
      
  --DP-3 Repair Cost    
  IF ( @PRODUCT_ID = 11482)    
  BEGIN    
   SET @REPL_COST = 75000    
  END      
  -----------------------------    
 END    
    
--End of default values----------------------    
    
    
     
      
/*    
 --Delete all coverages and endorsements      
 DELETE FROM POL_DWELLING_SECTION_COVERAGES      
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
    POLICY_ID = @POLICY_ID AND          
    POLICY_VERSION_ID = @POLICY_VERSION_ID            
       
 DELETE FROM POL_DWELLING_ENDORSEMENTS      
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
    POLICY_ID = @POLICY_ID AND          
    POLICY_VERSION_ID = @POLICY_VERSION_ID            
*/       
      
DECLARE @COUNTER Int    
SET @COUNTER = 0    
    
DECLARE DWELL_CURSOR CURSOR  STATIC        
FOR           
 SELECT DWELLING_ID          
 FROM POL_DWELLINGS_INFO          
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
  POLICY_ID = @POLICY_ID AND          
  POLICY_VERSION_ID = @POLICY_VERSION_ID          
    
 OPEN DWELL_CURSOR          
      
 WHILE ( 1 = 1 )    
 BEGIN    
        
  FETCH NEXT FROM DWELL_CURSOR          
  INTO @DWELL_ID           
       
  IF ( @@FETCH_STATUS <> 0 )    
  BEGIN    
  BREAK    
  END    
      
   --For repair cost products, update market value if required----    
 IF ( @PRODUCT_ID IN (11403, 11404, 11193, 11194 , 11480 , 11482, 11290, 11292) )    
 BEGIN    
      
  SELECT @EXISTING_MARKET_VALUE = ISNULL(MARKET_VALUE,0)    
  FROM POL_DWELLINGS_INFO    
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
      POLICY_ID = @POLICY_ID AND          
      POLICY_VERSION_ID = @POLICY_VERSION_ID       AND    
      DWELLING_ID = @DWELL_ID    
     
  IF ( @EXISTING_MARKET_VALUE < @MARKET_VALUE )    
  BEGIN    

   UPDATE POL_DWELLINGS_INFO    
   SET MARKET_VALUE = @MARKET_VALUE    
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
       POLICY_ID = @POLICY_ID AND          
       POLICY_VERSION_ID = @POLICY_VERSION_ID       AND    
       DWELLING_ID = @DWELL_ID    
       
   --Update Coverage limits    
   EXEC Proc_ADJUST_DWELLING_COVERAGES_FOR_POLICY    
    @CUSTOMER_ID,--@CUSTOMER_ID     int,                
     @POLICY_ID,--@POLICY_ID     int,                
     @POLICY_VERSION_ID,--@POLICY_VERSION_ID     smallint,                
     @DWELL_ID--@DWELLING_ID smallint  piyer         
  END    
 END    
 ELSE    
 BEGIN   
  --For other products, update repl cost if required    
  SELECT @EXISTING_REPL_COST = ISNULL(REPLACEMENT_COST,0)    
  FROM POL_DWELLINGS_INFO    
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
      POLICY_ID = @POLICY_ID AND          
      POLICY_VERSION_ID = @POLICY_VERSION_ID       AND    
      DWELLING_ID = @DWELL_ID    
     
  IF ( @EXISTING_REPL_COST < @REPL_COST )    
  BEGIN    
   UPDATE POL_DWELLINGS_INFO    
   SET REPLACEMENT_COST = @REPL_COST    
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
       POLICY_ID = @POLICY_ID AND          
       POLICY_VERSION_ID = @POLICY_VERSION_ID       AND    
       DWELLING_ID = @DWELL_ID    
       
   EXEC Proc_ADJUST_REPLACEMENT_COST_FOR_POLICY     
    @CUSTOMER_ID,--@CUSTOMER_ID     int,                
     @POLICY_ID,--@POLICY_ID     int,                
     @POLICY_VERSION_ID,--@POLICY_VERSION_ID     smallint,                
     @DWELL_ID--@DWELLING_ID smallint        
  END    
 END    
    
 EXEC Proc_Update_DWELLING_COVERAGES_FOR_Policy    
    @CUSTOMER_ID,--@CUSTOMER_ID     int,                
     @POLICY_ID,--@POLICY_ID     int,                
     @POLICY_VERSION_ID,--@POLICY_VERSION_ID     smallint,    
    @DWELL_ID    
      
 EXEC Proc_UPDATE_HOME_ENDORSEMENTS_FOR_POLICY    
    @CUSTOMER_ID,--@CUSTOMER_ID     int,                
     @POLICY_ID,--@POLICY_ID     int,                
     @POLICY_VERSION_ID,--@POLICY_VERSION_ID     smallint,    
    @DWELL_ID    
    
 --Update Rating Info if required, for DP-3 Premier, Builder's risk    
    
 IF ( @PRODUCT_ID = 11458 )    
 BEGIN    
  IF EXISTS    
  (    
   SELECT * FROM POL_HOME_RATING_INFO     
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
       POLICY_ID = @POLICY_ID AND          
       POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
       DWELLING_ID = @DWELL_ID    
  )    
  BEGIN    
   DECLARE @IS_UNDER_CONSTRUCTION Char(1)    
       
   SELECT @IS_UNDER_CONSTRUCTION = IS_UNDER_CONSTRUCTION     
   FROM POL_HOME_RATING_INFO     
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
       POLICY_ID = @POLICY_ID AND          
       POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
       DWELLING_ID = @DWELL_ID    
       
   IF ( @IS_UNDER_CONSTRUCTION = '1' )    
   BEGIN    
    UPDATE POL_HOME_RATING_INFO    
    SET IS_UNDER_CONSTRUCTION = '0'    
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
        POLICY_ID = @POLICY_ID AND          
        POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
        DWELLING_ID = @DWELL_ID    
        
    EXEC Proc_Update_DWELLING_COVERAGES_FROM_RATING_FOR_POLICY     
     @CUSTOMER_ID,--@CUSTOMER_ID     int,                
      @POLICY_ID,--@POLICY_ID     int,                
      @POLICY_VERSION_ID,--@POLICY_VERSION_ID     smallint,    
     @DWELL_ID    
        
    EXEC Proc_UPDATE_HOME_ENDORSEMENTS_RATING_FOR_POLICY    
     @CUSTOMER_ID,--@CUSTOMER_ID     int,                
      @POLICY_ID,--@POLICY_ID     int,                
      @POLICY_VERSION_ID,--@POLICY_VERSION_ID     smallint,    
     @DWELL_ID    
   END    
    
  END    
 END    
    
 --Ho-5 Indiana---------------    
IF (  @PRODUCT_ID = 11149 )    
BEGIN
	IF ( @STATE_ID = 14 )
	BEGIN
		--Set replacement cost
		UPDATE POL_DWELLING_COVERAGE
		SET DWELLING_REPLACE_COST = '1'
		 WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
	        POLICY_ID = @POLICY_ID AND          
	        POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
	        DWELLING_ID = @DWELL_ID

		--Update coverage/limits
		EXEC Proc_Update_DWELLING_COVERAGES_FROM_COV_LIMITS_FOR_POLICY
			   @CUSTOMER_ID,--@CUSTOMER_ID     int,                
			      @POLICY_ID,--@POLICY_ID     int,                
			      @POLICY_VERSION_ID,--@POLICY_VERSION_ID     smallint,    
			     @DWELL_ID    		
    		
		
		EXEC Proc_UPDATE_HOME_ENDORSEMENTS_FROM_LIMITS_FOR_POLICY
			  @CUSTOMER_ID,--@CUSTOMER_ID     int,                
			      @POLICY_ID,--@POLICY_ID     int,                
			      @POLICY_VERSION_ID,--@POLICY_VERSION_ID     smallint,    
			     @DWELL_ID    	
	END
END
 --------------------------------    
     
 --Delete non-relevant coverages-------------    
 EXEC Proc_DELETE_DWELLING_COVERAGES_BY_PRODUCT
	 @CUSTOMER_ID,--@CUSTOMER_ID     int,                
	      @POLICY_ID,--@POLICY_ID     int,                
	      @POLICY_VERSION_ID,--@POLICY_VERSION_ID     smallint,    
	     @DWELL_ID    
 -----------------    
   ---------------------------------------------------------------    
    
 END -- of 1 = 1    
         
 CLOSE  DWELL_CURSOR          
 DEALLOCATE DWELL_CURSOR          
                    
END                   
                   
                     
                      
                  
                                 
                                      
                                      
                                    
                                    
                                    
                                  
                                
                              
                            
                          
                        
                      
                    
                  
                
              
            
          
        
      
    
  



GO

