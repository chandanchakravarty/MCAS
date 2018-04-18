IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHORule_Sch_Items_Cvgs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHORule_Sch_Items_Cvgs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------                                                                                    
Proc Name                : Dbo.Proc_GetHORule_Sch_Items_Cvgs                                                                                  
Created by               : Ashwani                                                                                    
Date                     : 11 Dec.,2005                                    
Purpose                  : To get the scheduled items coverage detail for HO rules                                    
Revison History          :                                                                                    
Used In                  : Wolverine                                                                                    
------------------------------------------------------------                                                                                    
Date     Review By          Comments                                                                                    
------   ------------       -------------------------*/                    
--DROP PROC Proc_GetHORule_Sch_Items_Cvgs 1692,73,1,''                                                                              
CREATE proc dbo.Proc_GetHORule_Sch_Items_Cvgs                                    
(                                                                                    
@CUSTOMERID    int,                                                                                    
@APPID    int,                                                                                    
@APPVERSIONID   int,                                    
@DESC varchar(10)                                                                      
)                                                                                    
AS                                                                                        
BEGIN                             
  DECLARE @SINGLEITEM char                  
  DECLARE @BREAKAGEITEM CHAR                          
  IF EXISTS(SELECT ITEM_INSURING_VALUE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WHERE CUSTOMER_ID = @CUSTOMERID                 
  AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND (ISNULL(ITEM_INSURING_VALUE,0))>10000 AND IS_ACTIVE='Y' AND                         
  ITEM_ID IN (SELECT ITEM_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS                           
  WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ISNULL(IS_ACTIVE,'Y')='Y') )                            
  BEGIN                             
  SET @SINGLEITEM='Y'                            
  END                 
  ELSE                
  BEGIN                             
  SET @SINGLEITEM='N'                            
  END              
          SET @SINGLEITEM='N' 

  SET @BREAKAGEITEM='N'                            
/*            
 /* Category - Fine Arts with Breakage or Fine Arts without Breakage                 
 - If any one item exceeds 5,000 or if any one item has a value of $5,000 or more                 
 - and there is a No in the Appraisal /Bill of Sale Field  Submit to Underwriting */                 
 IF EXISTS(SELECT ITEM_INSURING_VALUE  FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WHERE CUSTOMER_ID = @CUSTOMERID             
 AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND (ISNULL(ITEM_INSURING_VALUE,0))>=5000 AND ISNULL(ITEM_APPRAISAL_BILL,'1') ='1' AND IS_ACTIVE='Y' AND                         
 ITEM_ID IN (SELECT ITEM_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS                           
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ISNULL(IS_ACTIVE,'Y')='Y') AND ITEM_ID IN(849,850) )                             
  BEGIN                             
  SET @BREAKAGEITEM='Y'                            
  END                 
 ELSE                
  BEGIN                             
  SET @BREAKAGEITEM='N'                            
  END     

*/  --Commented by Charles on 14-Dec-09 for Itrack 6488     
      
 /*                
 "CATEGORY - JEWELLERY  - IF ANY ONE ITEM EXCEEDS 10,000  - IF ANY ONE ITEM HAS A VALUE OF $5,000 OR MORE AND THERE IS A NO IN THE APPRAISAL /BILL OF SALE FIELD             
 - IF ANY ONE ITEM AS A VALUE OF $10,000 OR MORE AND  THERE IS A NO IN THE PICTURE FIELD SUBMIT TO UNDERWRITING  "                
 */                 
 DECLARE @JEWELLERYITEM_PICTURE CHAR                
 DECLARE @JEWELLERYITEM_APPRAISAL CHAR                
 IF EXISTS(SELECT ITEM_INSURING_VALUE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WHERE CUSTOMER_ID = @CUSTOMERID AND            
 APP_ID = @APPID  AND APP_VERSION_ID = @APPVERSIONID AND (ISNULL(ITEM_INSURING_VALUE,0))>=10000 AND ISNULL(ITEM_PICTURE_ATTACHED,'1') ='2' AND IS_ACTIVE='Y' AND                         
 ITEM_ID IN (SELECT ITEM_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS             
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ISNULL(IS_ACTIVE,'Y')='Y'          
 AND ITEM_ID=857 ))                             
  BEGIN                             
  SET @JEWELLERYITEM_PICTURE='Y'                            
  END                 
 ELSE                 
  BEGIN                             
  SET @JEWELLERYITEM_PICTURE='N'                            
  END                
 IF EXISTS(SELECT ITEM_INSURING_VALUE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID                  
 AND APP_VERSION_ID = @APPVERSIONID AND (ISNULL(ITEM_INSURING_VALUE,0))>=5000 AND ISNULL(ITEM_APPRAISAL_BILL,'1') ='1' AND                 
 IS_ACTIVE='Y' AND                         
 ITEM_ID IN (SELECT ITEM_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS                           
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ISNULL(IS_ACTIVE,'Y')='Y' AND ITEM_ID=857 ))                           
  BEGIN                             
  SET @JEWELLERYITEM_APPRAISAL='Y'                            
  END                 
 ELSE                 
  BEGIN                             
  SET @JEWELLERYITEM_APPRAISAL='N'                            
  END                
--------------------------------------------------                
 /* Category - Personal Computer Desktop or Personal Computers - Laptop                
 If the total  of both categories is over $8,000 Submit to Underwriting  */                
 DECLARE @PERSONALCOMPUTER  CHAR                
 IF EXISTS(SELECT SUM(ITEM_INSURING_VALUE) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WHERE CUSTOMER_ID = @CUSTOMERID                 
 AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND IS_ACTIVE='Y' AND                         
 ITEM_ID IN (SELECT ITEM_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS                           
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND                 
       ISNULL(IS_ACTIVE,'Y')='Y' AND ITEM_ID in(860,861,888,889 ) )  --888,889 added by Charles on 9-Oct-09 for Itrack 6458                
 HAVING SUM(ITEM_INSURING_VALUE) >8000 )                         
  BEGIN                             
  SET @PERSONALCOMPUTER='Y'                            
  END                 
 ELSE                 
  BEGIN                             
  SET @PERSONALCOMPUTER='N'                            
  END                
 --------------------------                
  DECLARE @AMOUNT_OF_INSURANCE DECIMAL                            
  DECLARE @AGGREGATE CHAR                            
  SET @AGGREGATE='N'                           
  IF EXISTS(SELECT ITEM_INSURING_VALUE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS                             
  WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND IS_ACTIVE='Y'                       
  AND ITEM_ID IN (SELECT ITEM_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS                           
  WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ISNULL(IS_ACTIVE,'Y')='Y'))                        
  BEGIN                       
  SELECT @AMOUNT_OF_INSURANCE=SUM(ITEM_INSURING_VALUE) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS                             
  WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                     
  AND (ISNULL(ITEM_INSURING_VALUE,0))>10000 AND                         
  ITEM_ID IN (SELECT ITEM_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS                           
  WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ISNULL(IS_ACTIVE,'Y')='Y')                       
              
  END                         
         IF(@AMOUNT_OF_INSURANCE>50000)                            
  BEGIN                             
   SET @AGGREGATE='Y'                            
  END                             
             
 -----------ITARCK # 6139-------------------            
             
 --Cameras (Non-Professional) (above $15,000)            
 DECLARE @SCH_CAMERA_AMOUNT DECIMAL(20),@SCH_CAMERAS_NON_PROFESNL VARCHAR            
 SET @SCH_CAMERAS_NON_PROFESNL='N'            
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK)            
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (847,875) AND IS_ACTIVE='Y')                                                          
 BEGIN                  
 SELECT @SCH_CAMERA_AMOUNT  = SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)               
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (847,875) AND IS_ACTIVE='Y'             
 END            
 IF(@SCH_CAMERA_AMOUNT > 15000)            
 SET @SCH_CAMERAS_NON_PROFESNL='Y'            
            
--Furs (above $30,000)            
 DECLARE @SCH_FURS_AMOUNT DECIMAL(20),@SCH_FURS_HO61 VARCHAR            
 SET @SCH_FURS_HO61='N'            
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK)             
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (851,879) AND IS_ACTIVE='Y')                                                          
 BEGIN                                        
 SELECT @SCH_FURS_AMOUNT  = SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)               
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (851,879) AND IS_ACTIVE='Y'            
 END            
        IF(@SCH_FURS_AMOUNT > 30000)            
 SET @SCH_FURS_HO61='Y'            
            
--Guns (HO-61) (above $4,000)            
 DECLARE @SCH_GUNS_AMOUNT DECIMAL(20),@SCH_GUNS_HO61 VARCHAR            
 SET @SCH_GUNS_HO61='N'            
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK)             
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (853,881) AND IS_ACTIVE='Y')                                                          
 BEGIN                                        
 SELECT @SCH_GUNS_AMOUNT  = SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)               
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (853,881) AND IS_ACTIVE='Y'            
 END            
        IF(@SCH_GUNS_AMOUNT > 4000)            
 SET @SCH_GUNS_HO61='Y'            
            
            
--Jewelry (HO-61) (above $30,000)-Changed        
--Itrack # 6455 - 1st Oct.2009 -Manoj            
/************************************************************************          
Category - Jewellery           
- If any one item exceeds 10,000           
- if any one item has a value of $2,000 or more and there is a No  or blank in the Appraisal /Bill of Sale Field           
- If any item has a value of 10,000 or more and  there is a No in the Picture Field Submit to Underwriting           
***********************************************************************/           
          
 DECLARE @SCH_JWELERY_AMOUNT DECIMAL(20),@SCH_JEWELRY_HO61 VARCHAR,@SCH_JEWELRY_HO61_EXCT_TEN_PIC VARCHAR          
 DECLARE @SCH_JEWELRY_HO61_EXCT_TEN VARCHAR,@SCH_JEWELRY_HO61_EXCT_TWO VARCHAR          
 DECLARE @ITEM_APPRAISAL_BILL NCHAR,@ITEM_PICTURE_ATTACHED NCHAR           
 SET @SCH_JEWELRY_HO61='N'      
 --Added by Charles on 8-Oct-09 for Itrack 6455     
 SET @SCH_JEWELRY_HO61_EXCT_TEN='N'     
 SET @SCH_JEWELRY_HO61_EXCT_TWO='N'    
 SET @SCH_JEWELRY_HO61_EXCT_TEN_PIC='N'      
 --Added till here        
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK)             
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (857,885) AND IS_ACTIVE='Y')                                                          
 BEGIN          
 --Commented Itrack # 6455 by Manoj Rathore        
 --IF(@SCH_JWELERY_AMOUNT > 30000)          
  --SET @SCH_JEWELRY_HO61='Y'          
                                       
 --If any one item exceeds 10,000           
 IF EXISTS          
  (          
   SELECT           
   MAX(ITEM_INSURING_VALUE) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)          
   WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND          
   ITEM_ID IN (857,885) AND IS_ACTIVE='Y' HAVING MAX(ITEM_INSURING_VALUE)>10000             
  )          
 BEGIN          
  SET @SCH_JEWELRY_HO61_EXCT_TEN='Y'              
 END           
          
 --if any one item has a value of $2,000 or more and there is a No in the Appraisal /Bill of Sale Field           
          
  IF EXISTS( SELECT           
   MAX(ITEM_INSURING_VALUE) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)          
   WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND          
   ITEM_ID IN (857,885) AND IS_ACTIVE='Y'  AND ISNULL(ITEM_APPRAISAL_BILL,'1')='1'          
   HAVING MAX(ITEM_INSURING_VALUE)>=2000          
         )          
 BEGIN          
  SET @SCH_JEWELRY_HO61_EXCT_TWO='Y'              
 END          
 -- if any one item as a value of $10,000 or more and there is a No in the Picture Field Submit to Underwriting            
 IF EXISTS          
  (          
   SELECT           
   MAX(ITEM_INSURING_VALUE) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)          
   WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND          
   ITEM_ID IN (857,885) AND IS_ACTIVE='Y'  AND ISNULL(ITEM_PICTURE_ATTACHED,'1')='1'          
   HAVING MAX(ITEM_INSURING_VALUE)>=10000             
  )           
 BEGIN          
  SET @SCH_JEWELRY_HO61_EXCT_TEN_PIC='Y'              
 END          
 END              
/**********************************************************************************/          
--Musical Instruments  (not professional) (HO-61) (above $15,000)            
 DECLARE @SCH_MUSICAL_AMOUNT DECIMAL(20),@SCH_MUSICAL_INSTRMNT_HO61 VARCHAR            
 SET @SCH_MUSICAL_INSTRMNT_HO61='N'            
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK)             
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (859,887) AND IS_ACTIVE='Y')                                                          
 BEGIN                                        
 SELECT @SCH_MUSICAL_AMOUNT  = SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)               
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (859,887) AND IS_ACTIVE='Y'            
 END            
        IF(@SCH_MUSICAL_AMOUNT > 15000)            
 SET @SCH_MUSICAL_INSTRMNT_HO61='Y'            
            
--Silver (HO-61) (above $15,000)            
 DECLARE @SCH_SILVER_AMOUNT DECIMAL(20),@SCH_SILVER_HO61 VARCHAR            
 SET @SCH_SILVER_HO61='N'            
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK)             
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (865,893) AND IS_ACTIVE='Y')                                                          
 BEGIN                                        
 SELECT @SCH_SILVER_AMOUNT  = SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)               
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (865,893) AND IS_ACTIVE='Y'            
 END            
        IF(@SCH_SILVER_AMOUNT > 15000)            
 SET @SCH_SILVER_HO61='Y'            
            
--Stamps (HO-61) (above $10,000)            
 DECLARE @SCH_STAMPS_AMOUNT DECIMAL(20),@SCH_STAMPS_HO61 VARCHAR            
 SET @SCH_STAMPS_HO61='N'            
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK)             
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (867,895) AND IS_ACTIVE='Y')                                                          
 BEGIN                                        
 SELECT @SCH_STAMPS_AMOUNT  = SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)               
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (867,895) AND IS_ACTIVE='Y'            
 END            
        IF(@SCH_STAMPS_AMOUNT > 10000)            
 SET @SCH_STAMPS_HO61='Y'            
            
--Rare Coins (HO-61) (above $10,000)            
 DECLARE @SCH_RARECOINS_AMOUNT DECIMAL(20),@SCH_RARECOINS_HO61 VARCHAR            
 SET @SCH_RARECOINS_HO61='N'            
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK)             
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (862,890) AND IS_ACTIVE='Y')                                                          
 BEGIN                                        
 SELECT @SCH_RARECOINS_AMOUNT  = SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)               
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (862,890) AND IS_ACTIVE='Y'            
 END            
        IF(@SCH_RARECOINS_AMOUNT > 10000)            
 SET @SCH_RARECOINS_HO61='Y'            
            
--Fine Arts without Breakage (above $30,000)            
 DECLARE @SCH_FINEARTS_WO_BREAK_AMOUNT DECIMAL(20),@SCH_FINEARTS_WO_BREAK_HO61 VARCHAR            
 SET @SCH_FINEARTS_WO_BREAK_HO61='N'            
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK)             
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (850,878) AND IS_ACTIVE='Y')                                                          
 BEGIN                                        
 SELECT @SCH_FINEARTS_WO_BREAK_AMOUNT  = SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)               
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (850,878) AND IS_ACTIVE='Y'            
 END            
        IF(@SCH_FINEARTS_WO_BREAK_AMOUNT > 30000)            
 SET @SCH_FINEARTS_WO_BREAK_HO61='Y'            
            
--Fine Arts with Breakage (above $30,000)            
 DECLARE @SCH_FINEARTS_BREAK_AMOUNT DECIMAL(20),@SCH_FINEARTS_BREAK_HO61 VARCHAR            
 SET @SCH_FINEARTS_BREAK_HO61='N'            
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK)             
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (849,877) AND IS_ACTIVE='Y')                                                          
 BEGIN                                        
 SELECT @SCH_FINEARTS_BREAK_AMOUNT  = SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)               
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (849,877) AND IS_ACTIVE='Y'            
 END            
        IF(@SCH_FINEARTS_BREAK_AMOUNT > 30000)            
 SET @SCH_FINEARTS_BREAK_HO61='Y'            
 -----------------END  ITARCK # 6139--------------------       
      
/*Revised Requirement 09/24/2009  - Fine Arts with Breakage or Fine Arts without Breakage      
 - if any one item exceeds 2,000       
 - and there is  a No or Blank  in the Appraisal /Bill of Sale Field      - Refer to Underwriting       
 - Message: Fine Arts Item(s) exceeds $2,000 with no Appraisal/Bill of Sale       
*/      
 --Added by Charles on 6-Oct-09 for Itrack 6488      
DECLARE  @SCH_FINEARTS_AMOUNT DECIMAL(20), @SCH_FINEARTS_HO61 VARCHAR            
 SET @SCH_FINEARTS_HO61='N'         
SELECT @SCH_FINEARTS_AMOUNT=MAX(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)               
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (850,878,849,877)      
 AND IS_ACTIVE='Y' AND ISNULL(ITEM_APPRAISAL_BILL,'1')='1'       
 IF @SCH_FINEARTS_AMOUNT IS NOT NULL AND @SCH_FINEARTS_AMOUNT>2000      
  SET @SCH_FINEARTS_HO61='Y'      
 --Added till here       
                        
  --=====================================================                          
  SELECT                            
  @SINGLEITEM as SINGLEITEM,                            
  @AGGREGATE as AGGREGATE ,                
  @BREAKAGEITEM AS BREAKAGEITEM ,                
  @JEWELLERYITEM_APPRAISAL as JEWELLERYITEM_APPRAISAL,                
  @JEWELLERYITEM_PICTURE AS   JEWELLERYITEM_PICTURE,                
  @PERSONALCOMPUTER AS PERSONALCOMPUTER ,            
  @SCH_CAMERAS_NON_PROFESNL AS SCH_CAMERAS_NON_PROFESNL,            
  @SCH_FURS_HO61 AS SCH_FURS_HO61,            
  @SCH_GUNS_HO61 AS SCH_GUNS_HO61,          
            
  --@SCH_JEWELRY_HO61 AS SCH_JEWELRY_HO61,            
  @SCH_JEWELRY_HO61_EXCT_TEN AS SCH_JEWELRY_HO61_EXCT_TEN,          
  @SCH_JEWELRY_HO61_EXCT_TWO AS SCH_JEWELRY_HO61_EXCT_TWO,          
  @SCH_JEWELRY_HO61_EXCT_TEN_PIC AS SCH_JEWELRY_HO61_EXCT_TEN_PIC,          
            
  @SCH_MUSICAL_INSTRMNT_HO61 AS SCH_MUSICAL_INSTRMNT_HO61,            
  @SCH_SILVER_HO61 AS SCH_SILVER_HO61,            
  @SCH_STAMPS_HO61 AS SCH_STAMPS_HO61,            
  @SCH_RARECOINS_HO61 AS SCH_RARECOINS_HO61,            
  @SCH_FINEARTS_WO_BREAK_HO61 AS SCH_FINEARTS_WO_BREAK_HO61,            
  @SCH_FINEARTS_BREAK_HO61  AS SCH_FINEARTS_BREAK_HO61,      
  @SCH_FINEARTS_HO61 AS SCH_FINEARTS_HO61 --Added by Charles on 6-Oct-09 for Itrack 6488             
END               
GO

