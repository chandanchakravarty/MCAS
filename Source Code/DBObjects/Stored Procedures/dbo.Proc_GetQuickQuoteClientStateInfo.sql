IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetQuickQuoteClientStateInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetQuickQuoteClientStateInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------  
Proc Name       : Proc_GetQuickQuoteClientStateInfo  
Created by      : Deepak  
Date            : 8/17/2005  
Purpose      :Evaluation  
Revison History :  
Used In        : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--DROP PROC Proc_GetQuickQuoteClientStateInfo  1576
CREATE PROC Proc_GetQuickQuoteClientStateInfo   
(  
 @CUSTOMER_ID     int  
)  
AS  
BEGIN  
 /*SELECT ISNULL(STATE_NAME,'') STATE,CUSTOMER_ZIP,ISNULL(DATE_OF_BIRTH,'') DATE_OF_BIRTH  
 FROM CLT_CUSTOMER_LIST  
 INNER JOIN MNT_COUNTRY_STATE_LIST  SL ON CUSTOMER_STATE = STATE_ID AND CUSTOMER_COUNTRY=COUNTRY_ID  
 WHERE CUSTOMER_ID = @CUSTOMER_ID*/  
  
 SELECT   
 ISNULL(STATE_NAME,'') STATE,  
 CUSTOMER_ZIP,  
 CASE   
 WHEN   
 (SELECT 1 FROM CLT_CUSTOMER_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND CUSTOMER_TYPE='11109') = 1   
 THEN  
 (SELECT ISNULL(CO_APPL_DOB,'') FROM CLT_APPLICANT_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND CUSTOMER_TYPE='11109' AND IS_PRIMARY_APPLICANT = 1)   
 ELSE   
 ISNULL(DATE_OF_BIRTH,'') END DATE_OF_BIRTH  ,
 CUSTOMER_STATE
 FROM CLT_CUSTOMER_LIST WITH(NOLOCK)  
 INNER JOIN MNT_COUNTRY_STATE_LIST  SL ON CUSTOMER_STATE = STATE_ID AND CUSTOMER_COUNTRY=COUNTRY_ID  
 WHERE CUSTOMER_ID = @CUSTOMER_ID  
END  
  
/*  
SP_HELP CLT_CUSTOMER_LIST  
*/  
GO

