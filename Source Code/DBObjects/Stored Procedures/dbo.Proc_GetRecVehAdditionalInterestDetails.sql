IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRecVehAdditionalInterestDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRecVehAdditionalInterestDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name    : Proc_GetRecVehAdditionalInterestDetails        
Created by   : Swastika Gaur        
Date         : 15th June'06           
Purpose      : Fetches the record from APP_HOMEOWNER_REC_VEH_ADD_INT  Table        

Modified By  : 
Modified On  : 
Purpose      : 
 ------------------------------------------------------------                        
Date     Review By          Comments                      
             
------   ------------       -------------------------*/           
-- drop proc dbo.Proc_GetRecVehAdditionalInterestDetails  
CREATE  PROC dbo.Proc_GetRecVehAdditionalInterestDetails  
(  
 @CUSTOMER_ID int,  
 @APP_ID  int,  
 @APP_VERSION_ID int,  
 @REC_VEH_ID  int,  
 @ADD_INT_ID  INT  
)  
AS  
BEGIN  
  
 DECLARE @HOLDER_ID1 int  
  
 SELECT  @HOLDER_ID1 = HOLDER_ID  
 FROM  APP_HOMEOWNER_REC_VEH_ADD_INT (NOLOCK) 
 WHERE   
  CUSTOMER_ID  = @CUSTOMER_ID and  
  APP_ID   = @APP_ID and  
  APP_VERSION_ID = @APP_VERSION_ID and  
  REC_VEH_ID  = @REC_VEH_ID and  
  ADD_INT_ID = @ADD_INT_ID  
   
 IF ( @HOLDER_ID1 IS NULL )  
 BEGIN  
  SELECT  
  MEMO,  
  NATURE_OF_INTEREST,  
  RANK,  
  LOAN_REF_NUMBER,  
  IS_ACTIVE,  
  HOLDER_ID,  
  HOLDER_NAME,  
  HOLDER_ADD1,  
  HOLDER_ADD2,  
  HOLDER_CITY,  
  HOLDER_COUNTRY,  
  HOLDER_STATE,  
  HOLDER_ZIP  
    
  FROM  APP_HOMEOWNER_REC_VEH_ADD_INT (NOLOCK) 
  WHERE   
  CUSTOMER_ID  = @CUSTOMER_ID and  
  APP_ID   = @APP_ID and  
  APP_VERSION_ID = @APP_VERSION_ID and  
  ADD_INT_ID = @ADD_INT_ID  AND   
  REC_VEH_ID  = @REC_VEH_ID   
    
 END  
 ELSE  
 BEGIN  
  SELECT  
   I.MEMO,  
   I.NATURE_OF_INTEREST,  
   I.RANK,  
   I.LOAN_REF_NUMBER,  
   I.IS_ACTIVE,  
   L.HOLDER_ID,  
   L.HOLDER_NAME,  
   L.HOLDER_ADD1,  
   L.HOLDER_ADD2,  
   L.HOLDER_CITY,  
   L.HOLDER_COUNTRY,  
   L.HOLDER_STATE,  
   L.HOLDER_ZIP  
  FROM  APP_HOMEOWNER_REC_VEH_ADD_INT I  (NOLOCK)
  INNER JOIN MNT_HOLDER_INTEREST_LIST L ON  
   I.HOLDER_ID = L.HOLDER_ID   
  WHERE   
   I.CUSTOMER_ID  = @CUSTOMER_ID and  
   I.APP_ID   = @APP_ID and  
   I.APP_VERSION_ID = @APP_VERSION_ID and  
   I.ADD_INT_ID = @ADD_INT_ID  AND   
   I.REC_VEH_ID  = @REC_VEH_ID    
 END  
  
END  
  



GO

