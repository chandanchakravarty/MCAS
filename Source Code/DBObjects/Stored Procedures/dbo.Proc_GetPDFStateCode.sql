IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFStateCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFStateCode]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : dbo.Proc_GetPDFStateCode      
Created by      : Anurag Verma      
Date            : 28/08/2006      
Purpose       : Return State Code from policy or applicationd etails      
Revison History :            
Used In    : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/    
--drop PROC dbo.Proc_GetPDFStateCode            
CREATE PROC dbo.Proc_GetPDFStateCode      
(            
   @Customer_Id  int,        
  @AppPol_Id  int,        
  @AppPolVersion_Id  int,        
  @CalledFrom  varchar(20)       
)            
AS            
BEGIN         
 IF lower(@CalledFrom)='application'      
 begin      
   SELECT MNT_COUNTRY_STATE_LIST.STATE_CODE,MNT_COUNTRY_STATE_LIST.STATE_ID,'' AS POLICY_STATUS, 
CASE APP_LIST.APP_LOB WHEN '1' THEN 'HOME'
			WHEN '2' THEN 'PPA'
			WHEN '3' THEN 'MOT'
			WHEN '4' THEN 'WAT'
			WHEN '6' THEN 'RENT' END LOB
  FROM MNT_COUNTRY_STATE_LIST WITH (NOLOCK)    
  INNER JOIN APP_LIST WITH (NOLOCK) ON MNT_COUNTRY_STATE_LIST.STATE_ID =APP_LIST.STATE_ID
  WHERE CUSTOMER_ID=@CUSTOMER_ID  AND APP_ID=@APPPOL_ID AND APP_VERSION_ID= @APPPOLVERSION_ID      
      
 end      
 else      
 begin      
  SELECT MNT_COUNTRY_STATE_LIST.STATE_CODE,MNT_COUNTRY_STATE_LIST.STATE_ID, PL.POLICY_STATUS, 
CASE PL.POLICY_LOB WHEN '1' THEN 'HOME'
			WHEN '2' THEN 'PPA'
			WHEN '3' THEN 'MOT'
			WHEN '4' THEN 'WAT'
			WHEN '6' THEN 'RENT' END LOB     
  FROM MNT_COUNTRY_STATE_LIST WITH (NOLOCK)      
  INNER JOIN POL_CUSTOMER_POLICY_LIST PL WITH (NOLOCK)     
  ON MNT_COUNTRY_STATE_LIST.STATE_ID =PL.STATE_ID                          
  WHERE CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@APPPOL_ID AND POLICY_VERSION_ID= @APPPOLVERSION_ID      
 end      
END        
      
      
    
  



GO

