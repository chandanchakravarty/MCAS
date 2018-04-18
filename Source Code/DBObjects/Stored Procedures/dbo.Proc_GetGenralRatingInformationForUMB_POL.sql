IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetGenralRatingInformationForUMB_POL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetGenralRatingInformationForUMB_POL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                          
Proc Name           : Proc_GetRatingInformationForUmbrella                                                                
Created by          : Neeraj singh                                                                         
Date                : 06-11-2006                                                                          
Purpose             : To get the information for creating the input xml                                                                           
Revison History     :                                                                          
Used In             : Wolverine                                                                          
------------------------------------------------------------                                                                          
Date     Review By          Comments                                                                          
------   ------------       -------------------------*/                    
--drop PROC dbo.Proc_GetGenralRatingInformationForUMB_POL                        
                                    
CREATE     PROC dbo.Proc_GetGenralRatingInformationForUMB_POL          
 (          
 @CUSTOMER_ID       INT,                                                                          
  @ID           INT,                                                                          
  @VERSION_ID      INT                
  )                                                                   
                                                                       
AS                                                                   
BEGIN                                                                          
                  
SET QUOTED_IDENTIFIER OFF           
          
          
DECLARE @TERITORYCODES   NVARCHAR(100)                       
DECLARE @TERMFACTOR          NVARCHAR(100)                      
DECLARE @QUOTEEFFDATE          NVARCHAR(100)                                                                              
DECLARE @QUOTEEXPDATE         NVARCHAR(100)                         
DECLARE @LOB_ID           NVARCHAR(100)                      
DECLARE @NEW_BUSINESS     NVARCHAR(100)                      
DECLARE @EFFECTIVE_DATE         NVARCHAR(100)                            
DECLARE @COUNTYS           NVARCHAR(100)                            
DECLARE @UMBRELLA_LIMIT    NVARCHAR(100)                            
DECLARE @MATURE_DISCOUNT   NVARCHAR(100)           
DECLARE @STATENAME    NVARCHAR(100)          
DECLARE @STATENAMEID   SMALLINT           
DECLARE @QQ_NUMBER   NVARCHAR(100)
DECLARE @POL_VERSION   NVARCHAR(100)
DECLARE @POL_NUMBER   NVARCHAR(100)           
DECLARE @NEWBUSINESSFACTOR   NVARCHAR(100)          
DECLARE @OFFICE_PROMISES SMALLINT              
DECLARE @RENTAL_DWELLING_UNITS SMALLINT            
DECLARE @TOTALNUMER_OF_MOTORCYCLE_DRIVERS INT    
           
 SET @NEWBUSINESSFACTOR='Y'          
                   
   SELECT                                                                               
   @LOB_ID  =  ISNULL(POLICY_LOB,0) ,                      
   @TERMFACTOR   =  ISNULL(APP_TERMS,''),                      
   @QUOTEEFFDATE   =  CONVERT(VARCHAR(10),POLICY_EFFECTIVE_DATE,101),                                                                                  
   @QUOTEEXPDATE   =  CONVERT(VARCHAR(10),POLICY_EXPIRATION_DATE,101),                      
   @STATENAMEID  = STATE_ID,
   @POL_NUMBER	=  POLICY_NUMBER,
   @POL_VERSION	=  POLICY_DISP_VERSION           
   FROM                                                       
   POL_CUSTOMER_POLICY_LIST WITH (NOLOCK)WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@ID AND POLICY_VERSION_ID=@VERSION_ID                    
               
-- umbrella policy limit                 
   SELECT @UMBRELLA_LIMIT = CONVERT(VARCHAR(20),POLICY_LIMITS,101),       -- check for umbrella limit              
               @TERITORYCODES=(CASE WHEN TERRITORY=14107 or TERRITORY=14109  THEN 1 WHEN TERRITORY=14108 or TERRITORY=14110   THEN 2 END)  
   FROM POL_UMBRELLA_LIMITS  WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@ID AND POLICY_VERSION_ID=@VERSION_ID                    

   -- state name of the umbrella policy          
   SELECT @STATENAME = upper(STATE_NAME) FROM MNT_COUNTRY_STATE_LIST WHERE STATE_ID=@STATENAMEID                    
--county name           
-- TERITORY CODES                    
  SELECT --@TERITORYCODES = TERR,           
  @COUNTYS  = COUNTY                     
  FROM MNT_TERRITORY_CODES WHERE lobID=@LOB_ID AND STATE = @STATENAMEID             
-- QQ_NUMBER          
  SELECT @QQ_NUMBER = QQ_NUMBER           
  FROM CLT_QUICKQUOTE_LIST  WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@ID AND APP_VERSION_ID=@VERSION_ID             
          
   -- number of officepromises and rentaldwellingunit                
    SELECT                  
       @OFFICE_PROMISES  = CONVERT(VARCHAR(20),isnull(OFFICE_PREMISES,'0')),    -- office promises              
      @RENTAL_DWELLING_UNITS  =    CONVERT(VARCHAR(20),FAMILIES)      -- rantal Dwelling              
     FROM                  
      POL_UMBRELLA_GEN_INFO     
 WITH (NOLOCK)                  
     WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID = @ID AND POLICY_VERSION_ID = @VERSION_ID      
    
    
-- total number of drivers in others case    
    
SELECT                           
     @TOTALNUMER_OF_MOTORCYCLE_DRIVERS = COUNT(distinct(DRIVER_ID))   -- TOTAL NUMBER OF DRIVERS IN OTHERS CASE                        
     FROM  POL_UMBRELLA_DRIVER_DETAILS  WITH (NOLOCK)         
              WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @ID AND POLICY_VERSION_ID= @VERSION_ID                
          
END          
                
                     
---------------------------RETURN VALUES--------------------------                      
BEGIN                      
SELECT            
 @LOB_ID            AS LOB_ID,                    
 @TERITORYCODES    AS TERRITORYCODES,                    
 @STATENAME      AS STATENAME,                    
 @NEWBUSINESSFACTOR AS NEWBUSINESSFACTOR,                      
 @QUOTEEFFDATE          AS QUOTEEFFDATE,                                                                             
 @QUOTEEXPDATE          AS QUOTEEXPDATE,                      
 @QQ_NUMBER        AS QQNUMBER,
 @POL_NUMBER	AS  POL_NUMBER,
 @POL_VERSION   AS  POL_VERSION,                
 @COUNTYS           AS COUNTY,                            
 @UMBRELLA_LIMIT    AS UUMBRELLA_LIMIT,          
 @OFFICE_PROMISES AS OFFICE_PROMISES,              
 @RENTAL_DWELLING_UNITS AS RENTAL_DWELLING_UNITS,    
 @TOTALNUMER_OF_MOTORCYCLE_DRIVERS AS OTHER_DRIVERS           
END                 
          
          
    
                     
        
      
    
  
  
  
  



GO

