IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRDRule_AddInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRDRule_AddInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* ----------------------------------------------------------                                                                                                                                    
Proc Name                : Dbo.Proc_GetRDRule_AddInterest  1151,634,1,1                                                                                                                                
Created by               : Ashwani                                                                                                                                    
Date                     : 12 Dec.,2005                                                                                    
Purpose                  : To get the rating detail for RD rules                                                                                    
Revison History          :                                                                                                                                    
Used In                  : Wolverine                                                                                                                                    
------------------------------------------------------------                                                                                                                                    
Date     Review By          Comments                                                                                                                                    
------   ------------       -------------------------*/              
-- DROP PROC Proc_GetRDRule_AddInterest                                                                                                             
CREATE proc dbo.Proc_GetRDRule_AddInterest                                                                                   
(                                                                                                                                    
@CUSTOMERID    int,                                                                                                                                    
@APPID    int,                                                                                                                                    
@APPVERSIONID   int,                                                                                    
@DWELLINGID int                                                                                                                      
)                                                                                                                                    
AS                                                                                                                                        
BEGIN                                                                                       
 -- Mandatory                                                                                      

 DECLARE @BILLMORTAGAGEE CHAR  
 DECLARE @BILL_MORTAGAGEE_COUNT INT                                    
 IF EXISTS (SELECT CUSTOMER_ID FROM APP_HOME_OWNER_ADD_INT     WITH(NOLOCK)                                                                                                  
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID and IS_ACTIVE='Y')                                                                                    
  BEGIN                                        
	
	SELECT @BILL_MORTAGAGEE_COUNT=COUNT(BILL_MORTAGAGEE)                                         
	FROM APP_HOME_OWNER_ADD_INT  WITH(NOLOCK)                                                                                  
	WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID 
	AND ISNULL(BILL_MORTAGAGEE,'')  =10963 and IS_ACTIVE='Y'
  END          
 ELSE         
BEGIN        
	 SET @BILL_MORTAGAGEE_COUNT=0
	 
 END 
DECLARE @APP_LOB NVARCHAR(5)
SELECT @APP_LOB=APP_LOB FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID and IS_ACTIVE='Y'

IF(@APP_LOB='1' OR @APP_LOB='6')
BEGIN
  IF EXISTS(SELECT BILL_TYPE_ID FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID and IS_ACTIVE='Y'
   AND (BILL_TYPE_ID=11276)) -- OR BILL_TYPE_ID=  11278 or BILL_TYPE_ID=11277) ) CHANGED BY PRAVESH ON 3 APRIL 2008 AS MORTEGAGEE SINCE INCEPTION WILL BE CONSIDER
BEGIN
		IF(@BILL_MORTAGAGEE_COUNT>0)
		BEGIN
			SET @BILLMORTAGAGEE='N'
		END   
	 ELSE 
	       BEGIN
			SET @BILLMORTAGAGEE='Y'
		END 
END
ELSE
	BEGIN
		SET @BILLMORTAGAGEE='N'
	END 
END

SELECT                 
 @BILLMORTAGAGEE AS BILLMORTAGAGEE
END 








GO

