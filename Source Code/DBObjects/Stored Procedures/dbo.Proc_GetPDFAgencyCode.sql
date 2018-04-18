IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFAgencyCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFAgencyCode]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------            
Proc Name       : dbo.Proc_GetPDFAgencyCode      
Created by      : Mohit Agarwal      
Date            : 11-Jan-2006 7    
Purpose       : Return Agency Code from policy or application details      
Revison History :            
Used In    : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/       
--drop proc dbo.Proc_GetPDFAgencyCode           
CREATE PROC dbo.Proc_GetPDFAgencyCode     
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
   SELECT RTRIM(MAL.AGENCY_CODE) AS AGENCY_CODE, MAL.AGENCY_DISPLAY_NAME, MAL.AGENCY_ID                                        
  FROM APP_LIST  AL   WITH (NOLOCK)  
  INNER JOIN  MNT_AGENCY_LIST MAL WITH ( NOLOCK ) ON     
 MAL.AGENCY_ID = AL.APP_AGENCY_ID                          
  WHERE CUSTOMER_ID=@CUSTOMER_ID  AND APP_ID=@APPPOL_ID AND APP_VERSION_ID= @APPPOLVERSION_ID      
      
 end      
 else      
 begin      
  SELECT RTRIM(MAL.AGENCY_CODE) AS AGENCY_CODE, MAL.AGENCY_DISPLAY_NAME, MAL.AGENCY_ID      
  FROM POL_CUSTOMER_POLICY_LIST   PCPL  WITH (NOLOCK)  
  INNER JOIN  MNT_AGENCY_LIST MAL   WITH ( NOLOCK )  
  ON MAL.AGENCY_ID = PCPL.AGENCY_ID                         
  WHERE CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@APPPOL_ID AND POLICY_VERSION_ID= @APPPOLVERSION_ID      
 end      
END        
      
      
    
    
    
    
    
  




GO

