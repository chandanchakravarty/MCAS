IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckApplicationExistsCapitalrater]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckApplicationExistsCapitalrater]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                      
Proc Name       : dbo.Proc_CheckApplicationExistsCapitalrater                                      
Created by      : Praveen Kasana                                      
Date            : 18 july 2007                               
Purpose         :Check if App Exists fro Capital rater Implementation                                       
Revison History :                                      
Used In        : Wolverine                                      
                                      
                             
------------------------------------------------------------                                      
Date     Review By          Comments                                      
------   ------------       -------------------------*/                         
          
CREATE PROC dbo.Proc_CheckApplicationExistsCapitalrater  
(                                      
 @INSURANCE_SVC_RQ varchar(100)  ,
 @CALLED_FROM varchar(10)                        
)
AS                   
BEGIN   
DECLARE @CUSTOMER_ID INT
DECLARE @APP_ID INT
DECLARE @APP_VERSION_ID INT
SELECT @CUSTOMER_ID=CUSTOMER_ID,@APP_ID=APP_ID,@APP_VERSION_ID=APP_VERSION_ID FROM ACORD_QUOTE_DETAILS WHERE
INSURANCE_SVC_RQ= @INSURANCE_SVC_RQ

IF(@CUSTOMER_ID IS NULL AND @APP_ID IS NULL AND @APP_VERSION_ID IS NULL)
BEGIN
  SELECT '1' AS STATUS --APP DOES NOT EXISTS
END
ELSE
BEGIN
 IF(@CALLED_FROM ='APP')
 SELECT '2' AS STATUS  --APP EXISTS
 ELSE
 SELECT  cast(CUSTOMER_ID as varchar(22)) + '/' + cast(APP_ID as varchar(22)) + '/' + 
cast(APP_VERSION_ID as varchar(22)) + '/' + 'EXISTS' as STATUS FROM ACORD_QUOTE_DETAILS WHERE
 INSURANCE_SVC_RQ= @INSURANCE_SVC_RQ
END
END











GO

