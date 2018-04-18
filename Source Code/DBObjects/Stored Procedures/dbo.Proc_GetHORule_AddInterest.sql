IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHORule_AddInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHORule_AddInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /* ----------------------------------------------------------                                                                                                                                        
Proc Name                : Dbo.Proc_GetHORule_AddInterest  1151,634,1,1                                                                                                                                    
Created by               : Manoj Rathore                                                                                                                                        
Date                     : 28 May.,2007                                                                                        
Purpose                  : To get the Additional Interest                                                                                       
Revison History          :                                                                                                                                        
Used In                  : Wolverine                                                                                                                                        
------------------------------------------------------------                                                                                                                                        
Date     Review By          Comments                                                                                                                                        
------   ------------       -------------------------*/                  
-- DROP PROC dbo.Proc_GetHORule_AddInterest 1707,42,1,1      
CREATE proc dbo.Proc_GetHORule_AddInterest                                                                                     
(                                                                                                                                        
 @CUSTOMERID    int,                                                                                                                                        
 @APPID    int,                                                                                                                                        
 @APPVERSIONID   int,                                                                                        
 @DWELLINGID int                                                                                                                          
)                                                                                                                                        
AS                                                                                                                                            
BEGIN                                                                                           
 -- Mandatory                                                                                          
    
 DECLARE @HOME_BILLMORTAGAGEE CHAR     
 DECLARE @BILL_MORTAGAGEE_COUNT INT   
--Added by Charles on 9-Sep-09 for Itrack 6370  
 DECLARE @MORE_MORTAGAGEE CHAR   
 DECLARE @MORE_MORTAGAGEE_COUNT CHAR    
 DECLARE @STATE_ID INT                                                                                   
--Added till here  
 IF EXISTS (SELECT CUSTOMER_ID FROM APP_HOME_OWNER_ADD_INT     WITH(NOLOCK)                                                                                                      
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID AND IS_ACTIVE='Y')                                                                                        
  BEGIN                                      
  
 SELECT @BILL_MORTAGAGEE_COUNT=COUNT(BILL_MORTAGAGEE)                                             
 FROM APP_HOME_OWNER_ADD_INT  WITH(NOLOCK)                                                                                      
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID     
 AND ISNULL(BILL_MORTAGAGEE,0)  =10963 AND IS_ACTIVE='Y'    
  END              
 ELSE             
 BEGIN            
  SET @BILL_MORTAGAGEE_COUNT=0    
      
 END     
DECLARE @APP_LOB NVARCHAR(5)    
SELECT @APP_LOB=APP_LOB, @STATE_ID=STATE_ID FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND IS_ACTIVE='Y' --STATE_ID Added by Charles on 9-Sep-09 for Itrack 6370   
    
IF(@APP_LOB='1')    
BEGIN    
 --Added by Charles on 9-Sep-09 for Itrack 6370  
 SELECT @MORE_MORTAGAGEE_COUNT=COUNT(ADD_INT_ID) FROM APP_HOME_OWNER_ADD_INT WITH(NOLOCK)  
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID     
 AND IS_ACTIVE='Y' AND @STATE_ID=14 --AND NATURE_OF_INTEREST='11867'  --Commented by Charles on 5-Oct-09 for Itrack 6370
 IF(@MORE_MORTAGAGEE_COUNT>2)    
  BEGIN    
   SET @MORE_MORTAGAGEE='Y'    
  END       
  ELSE     
  BEGIN    
   SET @MORE_MORTAGAGEE='N'    
  END     
 --Added till here  
  
  IF EXISTS(SELECT BILL_TYPE_ID FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND IS_ACTIVE='Y'    
   AND (BILL_TYPE_ID=11276)) --- OR BILL_TYPE_ID=  11278 or BILL_TYPE_ID=11277) ) CAHNGED BY PRAVESH ON 3RD APRIL AS mORTEGAGEE SINCE INCEPTION WILL BE CONSIDERED     
BEGIN    
  IF(@BILL_MORTAGAGEE_COUNT>0)    
  BEGIN    
   SET @HOME_BILLMORTAGAGEE='N'    
  END       
  ELSE     
        BEGIN    
   SET @HOME_BILLMORTAGAGEE='Y'    
  END     
END    
ELSE    
 BEGIN    
  SET @HOME_BILLMORTAGAGEE='N'    
 END     
END    
    
SELECT                     
 @HOME_BILLMORTAGAGEE AS HOME_BILLMORTAGAGEE,  
 @MORE_MORTAGAGEE AS MORE_MORTAGAGEE  --Added by Charles on 9-Sep-09 for Itrack 6370   
END     
    
GO

