IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAppBillMortagagee]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAppBillMortagagee]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                  
Proc Name          : Dbo.Proc_GetAppBillMortagagee                  
Created by         : Sumit Chhabra              
Date               : 04 Dec,2006                  
Purpose            : To get Bill Mortagagee details              
Revison History    :                  
Used In            :                     
Modified By        :   Pravesh K Chandel                 
Modified On        :    3rd April 2008                 
Purpose            :   Only consider Mortegagee since inception  bill type
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
--DROP PROC dbo.Proc_GetAppBillMortagagee 
CREATE  PROC dbo.Proc_GetAppBillMortagagee                  
(                  
 @CUSTOMER_ID int,                  
 @APP_ID  int,                  
 @APP_VERSION_ID int,        
 @DWELLING_ID smallINT = NULL,        
 @ADD_INT_ID int = null             
)                  
AS                  
BEGIN                  
                   
 DECLARE @BILL_MORTAGAGEE SMALLINT,@YES_LOOKUP_ID SMALLINT,@AGENCY_BILL_MORTAGAGEE SMALLINT,@INSURED_BILL_MORTAGAGEE SMALLINT,@MORTAGAGEE_INCEPTION SMALLINT  
 SET @AGENCY_BILL_MORTAGAGEE = 11277          
 SET @INSURED_BILL_MORTAGAGEE = 11278          
 SET @YES_LOOKUP_ID = 10963                
 SET @BILL_MORTAGAGEE = -1                
 SET @MORTAGAGEE_INCEPTION = 11276  
 
 /*Bill Mortagagee Rule--                
 SET BILL MORTAGAGEE TO YES LOOKUP ID IF THAT VALUE HAS BEEN CHOSEN YES ONCE                
 when the bill mortagagee is set to yes at additional interest page, we set the values of                
 dwelling_id and ADD_INT_ID to their respective values..else it remains null by default*/                
                 
 IF exists(SELECT CUSTOMER_ID FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID           
  AND APP_VERSION_ID=@APP_VERSION_ID           
  --AND BILL_TYPE_ID IN (@AGENCY_BILL_MORTAGAGEE,@INSURED_BILL_MORTAGAGEE,@MORTAGAGEE_INCEPTION)          
AND BILL_TYPE_ID IN (@MORTAGAGEE_INCEPTION)          
  AND ISNULL(IS_ACTIVE,'N')='Y')          
 SET @BILL_MORTAGAGEE = 1          
            
 IF exists(SELECT CUSTOMER_ID FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID           
  AND APP_VERSION_ID=@APP_VERSION_ID           
  AND (ADD_INT_ID IS NOT NULL AND ADD_INT_ID<>0)           
  AND (DWELLING_ID IS NOT NULL AND DWELLING_ID<>0)           
  AND ISNULL(IS_ACTIVE,'N')='Y'          
  --AND BILL_TYPE_ID IN (@AGENCY_BILL_MORTAGAGEE,@INSURED_BILL_MORTAGAGEE,@MORTAGAGEE_INCEPTION))                 
AND BILL_TYPE_ID IN (@MORTAGAGEE_INCEPTION))                 
 SET @BILL_MORTAGAGEE = @YES_LOOKUP_ID          
        
--if either the dwelling or the addl. int. data is inactive, we do not show the bill mortagee field        
 IF EXISTS(        
  (SELECT CUSTOMER_ID FROM APP_DWELLINGS_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID           
     AND APP_VERSION_ID=@APP_VERSION_ID           
     AND DWELLING_ID=@DWELLING_ID AND ISNULL(IS_ACTIVE,'N')='N'))         
 OR         
EXISTS(        
  (SELECT CUSTOMER_ID FROM APP_HOME_OWNER_ADD_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID           
     AND APP_VERSION_ID=@APP_VERSION_ID           
     AND DWELLING_ID=@DWELLING_ID AND ADD_INT_ID=ISNULL(@ADD_INT_ID,0) AND ISNULL(IS_ACTIVE,'N')='N')          
 )        
 SET @BILL_MORTAGAGEE = -1        
              
 return @BILL_MORTAGAGEE                   
                  
END                  
                  
                  
          
        
                  
                
              
              
            
            
          
        
      
    
  
  
  




GO

