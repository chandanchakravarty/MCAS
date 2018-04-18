IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRatingInformationForWatercraft_TrailerComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRatingInformationForWatercraft_TrailerComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*----------------------------------------------------------                    
Proc Name   : dbo.Proc_GetRatingInformationForWatercraft_TrailerComponent          
Created by  : Shafi     
Date        : 16 JUNE,2006         
Purpose     : Get the WATERCRAFT TRAILER Information                    

Reviewed By	:	Anurag verma
Reviewed On	:	16-07-2007
------------------------------------------------------------ */    

--DROP PROC dbo.Proc_GetRatingInformationForWatercraft_TrailerComponent    

CREATE PROC dbo.Proc_GetRatingInformationForWatercraft_TrailerComponent    
(    
 @CUSTOMERID INT,    
 @APPID INT,    
 @APPVERSIONID INT,    
 @TRAILER_ID INT    
    
)    
AS    
BEGIN    
    
DECLARE @MANUFACTURER VARCHAR(50)    
DECLARE @MODEL NVARCHAR(150)  
DECLARE @YEAR VARCHAR(50)    
DECLARE @SERIALNUMBER VARCHAR(50)    
DECLARE @ASSOCIATEDBOAT VARCHAR(50)    
DECLARE @MARKETVALUE VARCHAR(50)    
DECLARE @BOATTYPE VARCHAR(50)    
DECLARE @BOATTYPECODE VARCHAR(50)    
DECLARE @DEDUCTIBLE VARCHAR(50)    
DECLARE @UNATTACHEDEQUIPMENT VARCHAR(50)    
DECLARE @UNATTACHEDEQUIPMENT_DEDUCTIBLE VARCHAR(50)    
DECLARE @C VARCHAR(100)    
DECLARE @BOATSTYLECODE VARCHAR(100)    
DECLARE @COVERAGETYPEBASIS VARCHAR(100)  
   
    
SET @MANUFACTURER=''    
SET @MODEL=''    
SET @YEAR =''    
SET @SERIALNUMBER = ''    
SET @ASSOCIATEDBOAT = ''    
SET @MARKETVALUE = ''    
SET @BOATTYPE = ''    
SET @BOATTYPECODE = ''    
SET @DEDUCTIBLE = ''    
SET @UNATTACHEDEQUIPMENT = ''    
SET @UNATTACHEDEQUIPMENT_DEDUCTIBLE = ''    
    
 SELECT    
 @MANUFACTURER=MANUFACTURER,    
 @MODEL=MODEL,    
 @YEAR =YEAR,    
 @SERIALNUMBER = SERIAL_NO,    
 @ASSOCIATEDBOAT = ASSOCIATED_BOAT,    
 @MARKETVALUE = INSURED_VALUE,    
 @BOATTYPE =    LOOKUP_VALUE_DESC,    
 @BOATTYPECODE = LOOKUP_VALUE_CODE,  
 @BOATSTYLECODE= TYPE    
 FROM APP_WATERCRAFT_TRAILER_INFO AWT      (NOLOCK)  INNER JOIN  MNT_LOOKUP_VALUES MNT      (NOLOCK)  ON    
 AWT.TRAILER_TYPE = MNT.LOOKUP_UNIQUE_ID    
 WHERE    
    AWT.CUSTOMER_ID = @CUSTOMERID AND     AWT.APP_ID=@APPID AND AWT.APP_VERSION_ID=@APPVERSIONID AND  AWT.TRAILER_ID=@TRAILER_ID    
    
  
  
-- SELECT COVERAGE TYPE BASIS      
 SELECT   
 @COVERAGETYPEBASIS = LOOKUP_VALUE_CODE   
 FROM APP_WATERCRAFT_INFO A WITH(NOLOCK) INNER JOIN MNT_LOOKUP_VALUES B WITH(NOLOCK)   
 ON A.COV_TYPE_BASIS = B.LOOKUP_UNIQUE_ID   
 WHERE  A.CUSTOMER_ID=@CUSTOMERID AND A.APP_ID=@APPID AND A.APP_VERSION_ID=@APPVERSIONID AND A.BOAT_ID=@ASSOCIATEDBOAT   

-----------------------------------------------------------------------------------------
-- SELECT DEDUCTIBLE - ASFA PRAVEEN - 18-JUNE-2007
DECLARE @TRAILER_DED NVARCHAR(100) 

SELECT @C= CAST(ISNULL(AWT.TRAILER_DED, 0) AS VARCHAR) ,
	@TRAILER_DED = TRAILER_DED_AMOUNT_TEXT 
FROM APP_WATERCRAFT_TRAILER_INFO AWT
WHERE AWT.CUSTOMER_ID = @CUSTOMERID AND AWT.APP_ID=@APPID 
AND AWT.APP_VERSION_ID=@APPVERSIONID AND  AWT.TRAILER_ID=@TRAILER_ID

 --Change the format from 100-1% to 1%-100      
      
IF (@TRAILER_DED IS NOT NULL)
 BEGIN
 SET @DEDUCTIBLE= substring (@TRAILER_DED,2,4)+ '-' + @C 
 END
 
IF (@TRAILER_DED IS NULL)
 BEGIN      
 SET @DEDUCTIBLE= @C        
 END
if(ltrim(rtrim(@TRAILER_DED)) = '')
BEGIN
 SET @DEDUCTIBLE= @C        
END

 /*SET @DEDUCTIBLE=CASE CHARINDEX('-', @C)      
 WHEN 0 THEN   @C     
 ELSE      
 SUBSTRING(@C ,CHARINDEX('-', @C) +1,2) + '-' +  SUBSTRING(@C ,0,CHARINDEX('-', @C))     
 END*/
 

-----------------------------------------------------------------------------------------


/*  
SELECT    @C =                                       
(CASE COV_CODE                                         
------------------------------------------------------------------------------------------------------
                     
WHEN 'EBPPDAV'  THEN  CASE ISNULL(AWC.COVERAGE_CODE_ID,0)        
                           WHEN 0 THEN  ''        
                           ELSE  CAST(ISNULL(AWC.DEDUCTIBLE_1, 0) AS VARCHAR)+                 
                                 CAST(ISNULL(AWC.DEDUCTIBLE1_AMOUNT_TEXT, '') AS VARCHAR) END                                         
WHEN 'EBPPDACV'  THEN   CASE ISNULL(AWC.COVERAGE_CODE_ID,0)        
                           WHEN 0 THEN  ''        
                           ELSE  CAST(ISNULL(AWC.DEDUCTIBLE_1, 0) AS VARCHAR)+                 
                                 CAST(ISNULL(AWC.DEDUCTIBLE1_AMOUNT_TEXT, '') AS VARCHAR) END                  
WHEN 'EBPPDJ'  THEN  CASE ISNULL(AWC.COVERAGE_CODE_ID,0)        
                           WHEN 0 THEN  ''        
                           ELSE  CAST(ISNULL(AWC.DEDUCTIBLE_1, 0) AS VARCHAR)+                 
                                 CAST(ISNULL(AWC.DEDUCTIBLE1_AMOUNT_TEXT, '') AS VARCHAR) END           
-------------------------------------------------------------------------------------------------------
  
WHEN 'BDEDUC'  THEN  CASE ISNULL(AWC.COVERAGE_CODE_ID,0)      
                           WHEN 0 THEN  ''      
                           ELSE  CAST(ISNULL(AWC.DEDUCTIBLE_1, 0) AS VARCHAR)+               
                                 CAST(ISNULL(AWC.DEDUCTIBLE1_AMOUNT_TEXT, '') AS VARCHAR) END                                    
                             
 END) + isnull(@C,'')          
         
                                  
 FROM  APP_WATERCRAFT_TRAILER_INFO AWT  (NOLOCK)    
  
 INNER JOIN  APP_WATERCRAFT_COVERAGE_INFO AWC        (NOLOCK)    ON   
  AWC.BOAT_ID = AWT.ASSOCIATED_BOAT AND    
  AWT.CUSTOMER_ID=AWC.CUSTOMER_ID AND    
  AWT.APP_ID=AWC.APP_ID AND    
  AWT.APP_VERSION_ID=AWC.APP_VERSION_ID                                  
  
 RIGHT OUTER JOIN MNT_COVERAGE MNT      (NOLOCK)    ON  
        AWC.COVERAGE_CODE_ID = MNT.COV_ID     
      
                 
 WHERE                                         
 AWT.CUSTOMER_ID = @CUSTOMERID  AND                                   
 AWT.APP_ID  = @APPID  AND                                  
 AWT.APP_VERSION_ID  = @APPVERSIONID AND                                    
 AWT.TRAILER_ID  = @TRAILER_ID      
    
AND COV_CODE ='BDEDUC' -- IN ('EBPPDAV','EBPPDACV','EBPPDJ')                                   
            
 --Change the format from 100-1% to 1%-100      
      
     
 SET @DEDUCTIBLE=CASE CHARINDEX('-', @C)      
 WHEN 0 THEN   @C     
 ELSE      
 SUBSTRING(@C ,CHARINDEX('-', @C) +1,2) + '-' +  SUBSTRING(@C ,0,CHARINDEX('-', @C))     
 END    
*/
    
--Unattached Equipment    
    
 SELECT @UNATTACHEDEQUIPMENT=ISNULL(Limit_1,0),    
        @UNATTACHEDEQUIPMENT_DEDUCTIBLE=ISNULL(Deductible_1,0) FROM APP_WATERCRAFT_COVERAGE_INFO AWC  (NOLOCK)    
 INNER JOIN APP_WATERCRAFT_TRAILER_INFO AWT      (NOLOCK)      
  ON AWT.ASSOCIATED_BOAT=AWC.BOAT_ID AND    
    AWT.CUSTOMER_ID=AWC.CUSTOMER_ID AND    
    AWT.APP_ID=AWC.APP_ID AND    
    AWT.APP_VERSION_ID=AWC.APP_VERSION_ID    
 INNER JOIN MNT_COVERAGE MNT      (NOLOCK)  ON  MNT.COV_ID =AWC.coverage_code_id    
  WHERE                                           
 AWT.CUSTOMER_ID =@CUSTOMERID AND AWT.APP_ID=@APPID AND AWT.APP_VERSION_ID=@APPVERSIONID  AND AWT.TRAILER_ID=@TRAILER_ID                           
 AND COV_CODE= 'EBIUE'  -- Increase in "Unattached Equipment" And Personal Effects Coverage "                                          
                                          
 IF @UNATTACHEDEQUIPMENT is not null                                           
 BEGIN                                                   
 SET @UNATTACHEDEQUIPMENT='$' + @UNATTACHEDEQUIPMENT                                                  
 END     
    
SELECT      
 @MANUFACTURER AS MANUFACTURER,    
 @MODEL AS MODEL,    
 @YEAR AS  YEAR,    
 @SERIALNUMBER AS SERIALNUMBER,    
 @MARKETVALUE AS MARKETVALUE ,    
 @BOATTYPE AS BOATTYPE,    
 @BOATTYPECODE AS  BOATTYPECODE,  
 @BOATSTYLECODE  AS BOATSTYLECODE,  
 ISNULL(@DEDUCTIBLE,0) AS DEDUCTIBLE,    
 @UNATTACHEDEQUIPMENT AS  UNATTACHEDEQUIPMENT,    
 @UNATTACHEDEQUIPMENT_DEDUCTIBLE AS  UNATTACHEDEQUIPMENT_DEDUCTIBLE ,   
 isnull(@COVERAGETYPEBASIS,'ANA')  AS COVERAGEBASIS --ANA           
    
    
END    
    
  
  
  
  
  
  
  








GO

