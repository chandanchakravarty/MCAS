IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHomeOwnerAdditionalInterestDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHomeOwnerAdditionalInterestDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name          : Dbo.Proc_GetHomeOwnerAdditionalInterestDetails          
Created by         : Vijay Joshi          
Date               : 16 May,2005          
Purpose            : To get Additiona interest information from Home Owner Additional Interest          
Revison History    :          
Used In            :   Wolverine          
Modified By        :   Mohit Gupta          
Modified On        :   5/10/2005           
Purpose            :   Commenting field Certificate Required.           
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
--DROP PROC dbo.Proc_GetHomeOwnerAdditionalInterestDetails       
CREATE  PROC dbo.Proc_GetHomeOwnerAdditionalInterestDetails          
(          
 @CUSTOMER_ID int,          
 @APP_ID  int,          
 @APP_VERSION_ID int,          
 @DWELLING_ID int,          
 @ADD_INT_ID  INT          
)          
AS          
BEGIN          
           
 DECLARE @HOLDER_ID1 int          
 DECLARE @BILL_MORTAGAGEE SMALLINT --,@YES_LOOKUP_ID SMALLINT        
 --SET @YES_LOOKUP_ID = 10963        
 --SET @BILL_MORTAGAGEE = -1        
        
 /*Bill Mortagagee Rule--        
 SET BILL MORTAGAGEE TO YES LOOKUP ID IF THAT VALUE HAS BEEN CHOSEN YES ONCE        
 when the bill mortagagee is set to yes at additional interest page, we set the values of        
 dwelling_id and ADD_INT_ID to their respective values..else it remains null by default*/        
         
 /*IF exists(SELECT CUSTOMER_ID FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND        
       APP_VERSION_ID=@APP_VERSION_ID AND         
    (ADD_INT_ID IS NULL OR ADD_INT_ID<>0) AND        
    (DWELLING_ID IS NULL OR DWELLING_ID<>0) AND ISNULL(IS_ACTIVE,'N')='Y')        
 SET @BILL_MORTAGAGEE = @YES_LOOKUP_ID   */     
        
        
          
 SELECT  @HOLDER_ID1 = HOLDER_ID          
 FROM  APP_HOME_OWNER_ADD_INT          
 WHERE           
  CUSTOMER_ID  = @CUSTOMER_ID and          
  APP_ID   = @APP_ID and          
  APP_VERSION_ID = @APP_VERSION_ID and          
  DWELLING_ID  = @DWELLING_ID and          
  ADD_INT_ID = @ADD_INT_ID          
           
 IF ( @HOLDER_ID1 IS NULL )          
 BEGIN          
 SELECT MEMO,           
  NATURE_OF_INTEREST,           
  RANK,           
  LOAN_REF_NUMBER,           
  IS_ACTIVE,           
  HOLDER_ID,          
  --CERTIFICATE_REQUIRED,          
  HOLDER_ID,          
  HOLDER_NAME,          
  HOLDER_ADD1,          
  HOLDER_ADD2,          
  HOLDER_CITY,          
  HOLDER_COUNTRY,          
  HOLDER_STATE,          
  HOLDER_ZIP,        
 -- @BILL_MORTAGAGEE as 
  BILL_MORTAGAGEE          
 FROM  APP_HOME_OWNER_ADD_INT          
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
  APP_ID = @APP_ID AND          
  APP_VERSION_ID = @APP_VERSION_ID AND          
  ADD_INT_ID = @ADD_INT_ID AND           
  DWELLING_ID = @DWELLING_ID           
 END          
 ELSE          
 BEGIN          
  SELECT          
   I.MEMO,          
   I.NATURE_OF_INTEREST,          
   I.RANK,          
   I.LOAN_REF_NUMBER,          
   I.IS_ACTIVE,          
   --I.CERTIFICATE_REQUIRED,          
   L.HOLDER_ID,          
   L.HOLDER_NAME,          
   L.HOLDER_ADD1,          
   L.HOLDER_ADD2,          
   L.HOLDER_CITY,          
   L.HOLDER_COUNTRY,          
   L.HOLDER_STATE,          
   L.HOLDER_ZIP,        
   --@BILL_MORTAGAGEE as 
   I.BILL_MORTAGAGEE           
  FROM  APP_HOME_OWNER_ADD_INT I          
  INNER JOIN MNT_HOLDER_INTEREST_LIST L ON          
   I.HOLDER_ID = L.HOLDER_ID           
  WHERE           
   I.CUSTOMER_ID  = @CUSTOMER_ID and          
   I.APP_ID   = @APP_ID and          
   I.APP_VERSION_ID = @APP_VERSION_ID and          
   I.ADD_INT_ID = @ADD_INT_ID  AND           
   I.DWELLING_ID  = @DWELLING_ID            
 END          
          
END          
          
          
          
        
      
      
    
  



GO

