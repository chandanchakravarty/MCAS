IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTempCustAgencyPayments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTempCustAgencyPayments]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*------------------------------------------------------------------------       
Proc Name       : dbo.Proc_GetTempCustAgencyPayments       
Created by      : Swastika Gaur     
Date            :         
Purpose         :        
Revison History :        
Used In  : Wolverine        
    
-----        -------------------------------------------------------------*/        
-- DROP PROC dbo.[Proc_GetTempCustAgencyPayments]      
-- Proc_GetTempCustAgencyPayments 'w001'    
CREATE PROC [dbo].[Proc_GetTempCustAgencyPayments]    
(    
 @AGENCY_CODE NVARCHAR(8)    
)       
AS        
BEGIN        
DECLARE @ALLOW_EFT VARCHAR(5), @AGENID INT    
    
DECLARE @QUERY VARCHAR(7000)    
    
SET @QUERY ='    
    
 SELECT ISNULL(ALLOWS_CUSTOMER_SWEEP,''10964'')ALLOWS_EFT,ISNULL(C.CUSTOMER_FIRST_NAME,'' '') + '' '' 
+ ISNULL(C.CUSTOMER_MIDDLE_NAME,'' '')  + '' ''+ ISNULL(C.CUSTOMER_LAST_NAME,'' '') CUSTOMER_NAME  ,  
  convert(varchar(30),convert(money,isnull(A.min_due,0)),1)  as MIN_DUE , 
  convert(varchar(30),convert(money,isnull(A.TOTAL_DUE,0)),1) as TOTAL_DUE,
  convert(varchar(30),convert(money,isnull(A.AMOUNT,0)),1) as AMOUNT,    
 * FROM ACT_TMP_CUSTOMER_PAYMENTS_FROM_AGENCY A (NOLOCK)    
 LEFT OUTER JOIN MNT_AGENCY_LIST B ON B.AGENCY_ID = A.AGENCY_ID    
 INNER JOIN CLT_CUSTOMER_LIST C ON A.CUSTOMER_ID = C.CUSTOMER_ID
 INNER JOIN MNT_USER_LIST MUSER ON MUSER.USER_ID =  A.CREATED_BY_USER  WHERE MUSER.USER_SYSTEM_ID = '    
    + '''' + @AGENCY_CODE + ''''    

----Commented W001 Condition on 25 August 2009
--DECLARE @WHERE VARCHAR(200)    
--IF(@AGENCY_CODE != 'W001') --Wolv will have all the agency customers    
--BEGIN    
-- SET @WHERE = ' WHERE AGENCY_CODE = ' + '''' + @AGENCY_CODE + ''''    
-- SET @QUERY = @QUERY + @WHERE    
--END    
    
    
 EXEC (@QUERY)     
    
    
 ---SELECT @ALLOW_EFT = ISNULL(ALLOWS_EFT,'10964'),@AGENID = AGENCY_ID FROM MNT_AGENCY_LIST WHERE AGENCY_CODE = @AGENCY_CODE    
 SELECT @ALLOW_EFT = ISNULL(ALLOWS_CUSTOMER_SWEEP,'10964'),@AGENID = AGENCY_ID FROM MNT_AGENCY_LIST WHERE AGENCY_CODE = @AGENCY_CODE    
    
 SELECT @ALLOW_EFT AS ALLOW_EFT, @AGENID AS AGENCY_ID    
     
END    
    
    
    
    
    
    
    
    
  






GO

