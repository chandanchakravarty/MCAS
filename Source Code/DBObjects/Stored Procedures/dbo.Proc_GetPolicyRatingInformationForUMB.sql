IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyRatingInformationForUMB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyRatingInformationForUMB]
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
--DROP PROC  dbo.Proc_GetPolicyRatingInformationForUMB                      
CREATE   PROC dbo.Proc_GetPolicyRatingInformationForUMB                                                        
(                                                        
 @CUSTOMERID     	INT,                                                        
 @APPID       		INT,                                                        
 @APPVERSIONID   	INT,                                                        
 @UMBID     		INT                                                        
)                                                        
AS                                                        
BEGIN                                                        

SET QUOTED_IDENTIFIER OFF 
 
DECLARE @TERMFACTOR       		nvarchar(100)
DECLARE @QUOTEEFFDATE       		nvarchar(100)                                                        
DECLARE @QUOTEEXPDATE      		nvarchar(100)   
DECLARE @LOB_ID       			nvarchar(100)
DECLARE @NEW_BUSINESS 			nvarchar(100)
DECLARE @EFFECTIVE_DATE       		nvarchar(100)      
DECLARE @COUNTYS       			nvarchar(100)      
DECLARE @UMBRELLA_LIMIT			nvarchar(100)      
DECLARE @MATURE_DISCOUNT		nvarchar(100) 
     
------------ UnderLyingPolicy-LiabilityLimit--------------------
DECLARE @HOMEOWNERS_POLICY_LIMIT	nvarchar(100)      
DECLARE @DWELLINGFIRE_POLICY_LIMIT	nvarchar(100)      
DECLARE @WATERCRAFT_POLICY_LIMIT	nvarchar(100)      
DECLARE @PERSONALAUTO_POLICY_LIMIT	nvarchar(100)      
DECLARE @MOTORCYCLE_POLICY_LIMIT	nvarchar(100)      
DECLARE @UNDERLYING_MOTORIST_LIMIT	nvarchar(100)      
------------- Personal_Exposer----------------------------------
DECLARE @OFFICE_PROMISES		nvarchar(100)      
DECLARE @RENTAL_DWELLING_UNITS		nvarchar(100)      
------------- AutoMobile_Motor_Vehicle_Exposer------------------
DECLARE @TOTALNUMER_OF_DRIVERS		nvarchar(100)      
DECLARE @AUTOMOBILE    			nvarchar(100)      
DECLARE @MOTORHOMES   			nvarchar(100)      
DECLARE @MOTORCYCLES   			nvarchar(100)      
DECLARE @AUTOMOBILE_INEXPERIENCED_DRIVER nvarchar(100)      
DECLARE @MOTORHOMES_INEXPERIENCED_DRIVER nvarchar(100)      
DECLARE @MOTORCYCLES_INEXPERIENCED_DRIVER nvarchar(100)      
DECLARE @EXCLUDE_UNINSURED_MOTORIST	nvarchar(100)      
DECLARE @WATERCRATFT_ID			nvarchar(100)      
DECLARE @WATERCRAFT_LENGTH		nvarchar(100)      
DECLARE @WATERCRAFT_RATEDSPEED		nvarchar(100)      
DECLARE @WATERCRAFT_WEIGHT		nvarchar(100)      
DECLARE @INCEPTIONDATE			varchar(100)      
DECLARE @AGE 				INT            


-------- START ---------
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
DECLARE @APP_NUMBER VARCHAR(20)
DECLARE @APP_VERSION VARCHAR(20)
DECLARE @STATENAME VARCHAR(20)
DECLARE @QQ_NUMBER VARCHAR(20)
SELECT                                                         
 @LOB_ID		= 	ISNULL(APP_LOB,0) ,
 @STATENAME		=	STATE,                                                       
 @TERMFACTOR 		= 	ISNULL(APP_TERMS,''),
 @QUOTEEFFDATE 		= 	CONVERT(VARCHAR(10),APP_EFFECTIVE_DATE,101),                                                            
 @QUOTEEXPDATE 		= 	CONVERT(VARCHAR(10),APP_EXPIRATION_DATE,101),
 @COUNTYS		=	COUNTY,
 @UMBRELLA_LIMIT	=	CONVERT(VARCHAR(20),POLICY_LIMITS,101),
 @QQ_NUMBER		=	CONVERT(VARCHAR(20),QQ_NUMBER,101),
 @APP_NUMBER		=	APP_NUMBER,
 @APP_VERSION		=	APP_VERSION,
 @MATURE_DISCOUNT       =       0                
FROM                                 
 POL_LIST WITH (NOLOCK) INNER JOIN CLT_QUICKQUOTE_LIST CQL 
 ON POL_LIST.CUSTOMER_ID = CQL.CUSTOMER_ID INNER JOIN POL_UMBRELLA_REAL_ESTATE_LOCATION AUREL ON POL_LIST.CUSTOMER_ID = AUREL.CUSTOMER_ID INNER JOIN POL_UMBRELLA_LIMITS AUL ON POL_LIST.CUSTOMER_ID = POL.CUSTOMER_ID                                             
WHERE                                                         
 POL_LIST.CUSTOMER_ID=@CUSTOMERID AND POL_LIST.APP_ID=@APPID AND POL_LIST.APP_VERSION_ID=@APPVERSIONID   
                                                
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
DECLARE @INTERVAL INT
DECLARE @QUOTEEFFDATES DATETIME
SELECT 
@AGE = datediff("year",DRIVER_DOB,APP_EFFECTIVE_DATE)
FROM
 APP_LIST WITH (NOLOCK) INNER JOIN APP_UMBRELLA_DRIVER_DETAILS UDD ON
APP_LIST.CUSTOMER_ID=UDD.CUSTOMER_ID
WHERE                                                         
 APP_LIST.CUSTOMER_ID=@CUSTOMERID AND APP_LIST.APP_ID=@APPID AND APP_LIST.APP_VERSION_ID=@APPVERSIONID
   
--DECLARE @AGE		VARCHAR(100)
--set Age = '';
--select datediff("year",'DOB','QUOTEEFFDATES')
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SELECT
 @OFFICE_PROMISES		=	CONVERT(VARCHAR(20),RESIDENCES_OWNER_OCCUPIED,101),
 @RENTAL_DWELLING_UNITS		=   	CONVERT(VARCHAR(20),NUM_OF_RENTAL_UNITS,101)
FROM
 POL_UMBRELLA_LIMITS WITH (NOLOCK)
WHERE                                                         
 CUSTOMER_ID=@CUSTOMERID    
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

SELECT
 @TOTALNUMER_OF_DRIVERS				=	CONVERT(VARCHAR(20),NUM_OF_OPERATORS,101),
 @AUTOMOBILE					=	CONVERT(VARCHAR(20),NUM_OF_AUTO,101),
 @AUTOMOBILE_INEXPERIENCED_DRIVER		=	CONVERT(VARCHAR(20),OPER_UNDER_AGE,101)
FROM
 POL_UMBRELLA_LIMITS WITH (NOLOCK)	
 WHERE                                                         
 CUSTOMER_ID=@CUSTOMERID    


----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SELECT
 @WATERCRATFT_ID				=	CONVERT(VARCHAR(20),BOAT_ID,101),
 @WATERCRAFT_RATEDSPEED				=	CONVERT(VARCHAR(20),MAX_SPEED,101),
 @WATERCRAFT_WEIGHT				=	CONVERT(VARCHAR(20),WEIGHT,101),
 @WATERCRAFT_LENGTH				=	LENGTH
FROM
 POL_UMBRELLA_WATERCRAFT_INFO WITH (NOLOCK)
 WHERE                                                         
 CUSTOMER_ID=@CUSTOMERID    
END

---------------------------RETURN VALUES--------------------------
BEGIN
SELECT
 'Y'       				AS NEWBUSINESSFACTOR,
 @QUOTEEFFDATE       			AS QUOTEEFFDATE,	                                                      
 @QUOTEEXPDATE      			AS QUOTEEXPDATE,
 @QQ_NUMBER   				AS QQNUMBER,
 @LOB_ID       				AS LOB_ID,
 @STATENAME				AS STATENAME,	                                                     
 @COUNTYS       			AS COUNTY,      
 @UMBRELLA_LIMIT			AS UUMBRELLA_LIMIT,	      
 @MATURE_DISCOUNT			AS MATUREDISCOUNT,	 
     
------------ UnderLyingPolicy-LiabilityLimit--------------------
 @HOMEOWNERS_POLICY_LIMIT	   	AS HOMEOWNERPOLICY,   
 @DWELLINGFIRE_POLICY_LIMIT	   	AS DWELLINGFIREPOLICY,   
 @WATERCRAFT_POLICY_LIMIT	   	AS WATERCRAFTPOLICY,   
 @PERSONALAUTO_POLICY_LIMIT	   	AS PERSONALAUTOPOLICY,  
 @MOTORCYCLE_POLICY_LIMIT	    	AS MOTORCYCLEPOLICY,  
 @UNDERLYING_MOTORIST_LIMIT	    	AS UNINSUNDERINSMOTORISTLIMIT,  
------------- Personal_Exposer----------------------------------
 @OFFICE_PROMISES		    	AS OFFICEPREMISES, 
 @RENTAL_DWELLING_UNITS		     	AS RENTALDWELLINGUNIT,
------------- AutoMobile_Motor_Vehicle_Exposer------------------
 @TOTALNUMER_OF_DRIVERS		      	AS TOTALNUMBERDRIVERS,
 @AUTOMOBILE    		     	AS AUTOMOBILES,
 @MOTORHOMES   			     	AS MOTOTHOMES,
 @MOTORCYCLES   			AS MOTORCYCLES,      
 @AUTOMOBILE_INEXPERIENCED_DRIVER       AS INEXPDRIVERSAUTO,
 @MOTORHOMES_INEXPERIENCED_DRIVER       AS INEXPDRIVERSMOTORHOME,
 @MOTORCYCLES_INEXPERIENCED_DRIVER      AS INEXPDRIVERSMOTORCYCL,
 @EXCLUDE_UNINSURED_MOTORIST	     	AS EXCLUDEUNINSMOTORIST,
 @WATERCRATFT_ID			AS TYPE,      
 @WATERCRAFT_LENGTH		      	AS LENGTH,
 @WATERCRAFT_RATEDSPEED		      	AS RATEDSPEED,
 @WATERCRAFT_WEIGHT		      	AS HORSEPOWER
                                            
 END                                                  



GO

