IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ADJUST_DWELLING_COVERAGES_POLICY_NEW]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ADJUST_DWELLING_COVERAGES_POLICY_NEW]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                                          
Proc Name       : dbo.Proc_ADJUST_DWELLING_COVERAGES_POLICY_NEW                                          
Created by      : Pradeep                                          
Date            : 02/03/2006                                     
Purpose      :  Called when policy type is changed from           
  Policy page          
Revison History :                                          
Used In  : Wolverine                                          
------------------------------------------------------------                                          
Date     Review By          Comments                                          
------   ------------       -------------------------*/       
-- drop proc Proc_ADJUST_DWELLING_COVERAGES_POLICY_NEW                                     
CREATE  PROC Dbo.Proc_ADJUST_DWELLING_COVERAGES_POLICY_NEW                                          
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
DECLARE @DATE_APP_EFFECTIVE_DATE DATETIME  --Added by Charles on 18-Dec-09 for Itrack 6681   
    
SELECT @PRODUCT_ID = POLICY_TYPE,        
@STATE_ID = STATE_ID,        
@LOB_ID = POLICY_LOB,
@DATE_APP_EFFECTIVE_DATE= CAST(CONVERT(VARCHAR,APP_EFFECTIVE_DATE,101) AS DATETIME)  --Added by Charles on 18-Dec-09 for Itrack 6681           
FROM POL_CUSTOMER_POLICY_LIST        
WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
POLICY_ID = @POLICY_ID AND              
POLICY_VERSION_ID = @POLICY_VERSION_ID    

--Added by Charles on 18-Dec-09 for Itrack 6681
DECLARE @GF_REP_COST CHAR(2)
SET @GF_REP_COST='-1'

IF @STATE_ID = 14
BEGIN
	IF( @DATE_APP_EFFECTIVE_DATE >= CAST(CONVERT(VARCHAR,'01/01/2000',101) AS DATETIME) AND @DATE_APP_EFFECTIVE_DATE <= CAST(CONVERT(VARCHAR,'10/14/2008',101) AS DATETIME))
    BEGIN
		SET @GF_REP_COST = 'Y'
	END
	ELSE IF( @DATE_APP_EFFECTIVE_DATE >= CAST(CONVERT(VARCHAR,'10/15/2008',101) AS DATETIME) AND @DATE_APP_EFFECTIVE_DATE <= CAST(CONVERT(VARCHAR,'12/31/9999',101) AS DATETIME))
	BEGIN
		SET @GF_REP_COST = 'N'
	END 
END
ELSE IF @STATE_ID = 22
BEGIN
	IF( @DATE_APP_EFFECTIVE_DATE >= CAST(CONVERT(VARCHAR,'01/01/2000',101) AS DATETIME) AND @DATE_APP_EFFECTIVE_DATE <= CAST(CONVERT(VARCHAR,'01/31/2009',101) AS DATETIME))
    BEGIN
		SET @GF_REP_COST = 'Y'
	END
	ELSE IF( @DATE_APP_EFFECTIVE_DATE >= CAST(CONVERT(VARCHAR,'02/01/2009',101) AS DATETIME) AND @DATE_APP_EFFECTIVE_DATE <= CAST(CONVERT(VARCHAR,'12/31/9999',101) AS DATETIME))
	BEGIN
		SET @GF_REP_COST = 'N'
	END	
END 
--Added till here     
        
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
 IF ( @PRODUCT_ID = 11401 AND @GF_REP_COST='Y') --Added @GF_REP_COST, Charles (18-Dec-09), Itrack 6681       
 BEGIN        
	SET @REPL_COST = 125000            
 END 
 ELSE IF ( @PRODUCT_ID = 11401 AND @GF_REP_COST='N') --Added by Charles (18-Dec-09), Itrack 6681 
 BEGIN
	SET @REPL_COST = 200000
 END  --Added till here    
  
 --HO-5 Premier        
 IF ( @PRODUCT_ID = 11410 AND @GF_REP_COST='Y')  --Added @GF_REP_COST, Charles (18-Dec-09), Itrack 6681        
 BEGIN        
  SET @REPL_COST = 175000           
 END 
 ELSE IF ( @PRODUCT_ID = 11410 AND @GF_REP_COST='N') --Added by Charles (18-Dec-09), Itrack 6681 
 BEGIN
	SET @REPL_COST = 200000
 END  --Added till here        
     
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
 IF ( @PRODUCT_ID = 11149  AND @GF_REP_COST='Y')   --Added @GF_REP_COST, Charles (18-Dec-09), Itrack 6681      
 BEGIN        
  SET @REPL_COST = 125000       
 END  
 ELSE IF ( @PRODUCT_ID = 11149 AND @GF_REP_COST='N') --Added by Charles (18-Dec-09), Itrack 6681 
 BEGIN
	SET @REPL_COST = 200000
 END  --Added till here  
      
     
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
    
DECLARE @COUNTER Int        
SET @COUNTER = 0        
        
DECLARE DWELL_CURSOR CURSOR  STATIC            
FOR SELECT DWELLING_ID              
FROM POL_DWELLINGS_INFO              
WHERE  CUSTOMER_ID = @CUSTOMER_ID AND              
 POLICY_ID = @POLICY_ID AND              
 POLICY_VERSION_ID = @POLICY_VERSION_ID              
    
OPEN DWELL_CURSOR              
          
WHILE ( 1 = 1 )        
BEGIN        
 FETCH NEXT FROM DWELL_CURSOR INTO @DWELL_ID               
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
END -- of 1 = 1        
             
 CLOSE  DWELL_CURSOR              
 DEALLOCATE DWELL_CURSOR              
                        
END                       
                       
                         
                          

GO

