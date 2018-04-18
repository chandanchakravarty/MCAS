IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyDriversListForClaims]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyDriversListForClaims]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                      
Proc Name       : dbo.Proc_GetPolicyDriversListForClaims  
Created by      : Sumit Chhabra                                    
Date            : 04/05/2006                                      
Purpose         : Fetch List of policy drivers to be used at Claims
Created by      : Sumit Chhabra                                     
Revison History :                                      
Used In        : Wolverine                                      
------------------------------------------------------------                                      
Date     Review By          Comments                                      
------   ------------       -------------------------*/                                      
CREATE PROC dbo.Proc_GetPolicyDriversListForClaims
(                              
@CUSTOMER_ID int,                  
@POLICY_ID int,                  
@POLICY_VERSION_ID smallint
)  
AS                                      
BEGIN                                      
declare @LOB_ID int  
 select   
  @LOB_ID = POLICY_LOB   
 FROM   
  POL_CUSTOMER_POLICY_LIST   
 WHERE  
  CUSTOMER_ID = @CUSTOMER_ID  and  
  POLICY_ID = @POLICY_ID and  
  POLICY_VERSION_ID = @POLICY_VERSION_ID  
  
/*  
LOBS AND TABLES IN WHICH DRIVER INFO IS BEGIN SAVED  
1 HOME Homeowners , 4 BOAT Watercraft ---POL_WATERCRAFT_DRIVER_DETAILS  
2 AUTOP Automobile , 3 CYCL Motorcycle ---POL_DRIVER_DETAILS  
5 UMB Umbrella              ---POL_UMBRELLA_DRIVER_DETAILS  
*/  
  
if (@LOB_ID=1 or @LOB_ID=4)  
 SELECT                       
 ISNULL(DRIVER_FNAME,'') + ' ' + ISNULL(DRIVER_LNAME,'') AS DRIVER_NAME,DRIVER_ID
 FROM  
 POL_WATERCRAFT_DRIVER_DETAILS  
 WHERE  
 CUSTOMER_ID = @CUSTOMER_ID  and  
 POLICY_ID = @POLICY_ID and  
 POLICY_VERSION_ID = @POLICY_VERSION_ID and  
 IS_ACTIVE = 'Y'  
else if (@LOB_ID=2 OR @LOB_ID=3)  
  SELECT                       
  ISNULL(DRIVER_FNAME,'') + ' ' + ISNULL(DRIVER_LNAME,'') AS DRIVER_NAME,DRIVER_ID
 FROM  
 POL_DRIVER_DETAILS  
 WHERE  
 CUSTOMER_ID = @CUSTOMER_ID  and  
 POLICY_ID = @POLICY_ID and  
 POLICY_VERSION_ID = @POLICY_VERSION_ID and  
 IS_ACTIVE = 'Y'  
else if @LOB_ID=5  
 SELECT                       
  ISNULL(DRIVER_FNAME,'') + ' ' + ISNULL(DRIVER_LNAME,'') AS DRIVER_NAME,DRIVER_ID
 FROM  
 POL_UMBRELLA_DRIVER_DETAILS  
 WHERE  
 CUSTOMER_ID = @CUSTOMER_ID  and  
 POLICY_ID = @POLICY_ID and  
 POLICY_VERSION_ID = @POLICY_VERSION_ID and  
 IS_ACTIVE = 'Y'  
         
END                                
                            
      
    
  



GO

