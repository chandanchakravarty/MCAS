IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyDriversDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyDriversDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                      
Proc Name       : dbo.Proc_GetPolicyDriversDetails  
Created by      : Sumit Chhabra                                    
Date            : 04/05/2006                                      
Purpose         : Fetch details of policy drivers  
Created by      : Sumit Chhabra                                     
Revison History :                                      
Used In        : Wolverine                                      
------------------------------------------------------------                                      
Date     Review By          Comments                                      
------   ------------       -------------------------*/                                      
CREATE PROC dbo.Proc_GetPolicyDriversDetails      
(                              
@CUSTOMER_ID int,                  
@POLICY_ID int,                  
@POLICY_VERSION_ID smallint,  
@DRIVER_ID int  
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
 CAST(ISNULL(DRIVER_FNAME,'') + ' ' + ISNULL(DRIVER_LNAME,'') AS VARCHAR(50)) AS DRIVER_NAME,DRIVER_ADD1,DRIVER_ADD2,  
 DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,convert(char,DRIVER_DOB,101) DRIVER_DOB      
 FROM  
 POL_WATERCRAFT_DRIVER_DETAILS  
 WHERE  
 CUSTOMER_ID = @CUSTOMER_ID  and  
 POLICY_ID = @POLICY_ID and  
 POLICY_VERSION_ID = @POLICY_VERSION_ID and  
 DRIVER_ID = @DRIVER_ID and   
 IS_ACTIVE = 'Y'  
else if (@LOB_ID=2 OR @LOB_ID=3)  
  SELECT                       
 CAST(ISNULL(DRIVER_FNAME,'') + ' ' + ISNULL(DRIVER_LNAME,'') AS VARCHAR(50)) AS DRIVER_NAME,DRIVER_ADD1,DRIVER_ADD2,  
 DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,convert(char,DRIVER_DOB,101) DRIVER_DOB        
 FROM  
 POL_DRIVER_DETAILS  
 WHERE  
 CUSTOMER_ID = @CUSTOMER_ID  and  
 POLICY_ID = @POLICY_ID and  
 POLICY_VERSION_ID = @POLICY_VERSION_ID and  
 DRIVER_ID = @DRIVER_ID and   
 IS_ACTIVE = 'Y'  
else if @LOB_ID=5  
 SELECT                       
 CAST(ISNULL(DRIVER_FNAME,'') + ' ' + ISNULL(DRIVER_LNAME,'') AS VARCHAR(50)) AS DRIVER_NAME,DRIVER_ADD1,DRIVER_ADD2,  
 DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,convert(char,DRIVER_DOB,101) DRIVER_DOB        
 FROM  
 POL_UMBRELLA_DRIVER_DETAILS  
 WHERE  
 CUSTOMER_ID = @CUSTOMER_ID  and  
 POLICY_ID = @POLICY_ID and  
 POLICY_VERSION_ID = @POLICY_VERSION_ID and  
 DRIVER_ID = @DRIVER_ID   and   
 IS_ACTIVE = 'Y'  
         
END                                
                            
      
    
  



GO

