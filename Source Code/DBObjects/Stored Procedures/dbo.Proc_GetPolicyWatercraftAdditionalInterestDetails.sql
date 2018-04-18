IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyWatercraftAdditionalInterestDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyWatercraftAdditionalInterestDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          	: Dbo.Proc_GetPolicyWatercraftAdditionalInterestDetails  
Created by           	: Vijay Arora
Date                    : 23-11-2005
Purpose               	:  Retrieve additional interest from POL_WATERCRAFT_COV_ADD_INT table  
Revison History 	:  
Used In                	:   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE  PROC Dbo.Proc_GetPolicyWatercraftAdditionalInterestDetails  
(  
 @CUSTOMER_ID int,  
 @POLICY_ID  int,  
 @POLICY_VERSION_ID int,  
 @BOAT_ID  int,  
 @ADD_INT_ID int   
)  
AS  
  
DECLARE @HOLDER_ID1 int  
  
SELECT  @HOLDER_ID1 = HOLDER_ID  
FROM  POL_WATERCRAFT_COV_ADD_INT  
WHERE   
 CUSTOMER_ID  = @CUSTOMER_ID and  
 POLICY_ID   = @POLICY_ID and  
 POLICY_VERSION_ID = @POLICY_VERSION_ID and  
 BOAT_ID  = @BOAT_ID AND  
 ADD_INT_ID = @ADD_INT_ID  
   
BEGIN  
   
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
  FROM  POL_WATERCRAFT_COV_ADD_INT  
  WHERE   
   CUSTOMER_ID  = @CUSTOMER_ID and  
   POLICY_ID   = @POLICY_ID and  
   POLICY_VERSION_ID = @POLICY_VERSION_ID and  
   ADD_INT_ID = @ADD_INT_ID  AND   
   BOAT_ID  = @BOAT_ID   
    
  ORDER BY  CUSTOMER_ID ASC  
 END  
 ELSE  
 BEGIN  
  SELECT  
   MEMO,  
   NATURE_OF_INTEREST,  
   RANK,  
   LOAN_REF_NUMBER,  
   I.IS_ACTIVE,  
   L.HOLDER_ID,  
   L.HOLDER_NAME,  
   L.HOLDER_ADD1,  
   L.HOLDER_ADD2,  
   L.HOLDER_CITY,  
   L.HOLDER_COUNTRY,  
   L.HOLDER_STATE,  
   L.HOLDER_ZIP  
  FROM  POL_WATERCRAFT_COV_ADD_INT I  
  INNER JOIN MNT_HOLDER_INTEREST_LIST L ON  
   I.HOLDER_ID = L.HOLDER_ID   
  WHERE   
   I.CUSTOMER_ID  = @CUSTOMER_ID and  
   I.POLICY_ID   = @POLICY_ID and  
   I.POLICY_VERSION_ID = @POLICY_VERSION_ID and  
   I.ADD_INT_ID = @ADD_INT_ID  AND   
   I.BOAT_ID  = @BOAT_ID    
 END  
END  
  



GO

