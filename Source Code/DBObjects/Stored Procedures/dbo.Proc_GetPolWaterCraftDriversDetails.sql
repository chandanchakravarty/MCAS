IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolWaterCraftDriversDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolWaterCraftDriversDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                      
Proc Name                : Dbo.Proc_GetPolWaterCraftDriversDetails                             
Created by               :Swarup kumar pal
Date                     : 19 Mar 2007
Purpose                  : To all watercraft drivers of the policy    
Revison History          :                                                      
Used In                  : Wolverine                                                      
------------------------------------------------------------                                                      
drop proc dbo.Proc_GetPolWaterCraftDriversDetails                                              
Date     Review By          Comments                                                      
------   ------------       -------------------------*/                                                      
CREATE  proc dbo.Proc_GetPolWaterCraftDriversDetails                                              
(                                                      
 @CUSTOMER_ID    int,                                                      
 @POLICY_ID    int,                                                      
 @POLICY_VERSION_ID   int       
             
)                                                      
AS                                       
    
	SELECT     
		dbo.POL_WATERCRAFT_DRIVER_DETAILS.DRIVER_ID, 
		dbo.POL_WATERCRAFT_DRIVER_DETAILS.DRIVER_FNAME,   
		dbo.POL_WATERCRAFT_DRIVER_DETAILS.DRIVER_MNAME, 
		dbo.POL_WATERCRAFT_DRIVER_DETAILS.DRIVER_LNAME,   
		dbo.POL_WATERCRAFT_DRIVER_DETAILS.DRIVER_CITY,
		dbo.POL_WATERCRAFT_DRIVER_DETAILS.DRIVER_ZIP,  
		dbo.POL_WATERCRAFT_DRIVER_DETAILS.DRIVER_CODE,
		dbo.POL_WATERCRAFT_DRIVER_DETAILS.DRIVER_SUFFIX,   
		dbo.POL_WATERCRAFT_DRIVER_DETAILS.DRIVER_ADD1,
		dbo.POL_WATERCRAFT_DRIVER_DETAILS.DRIVER_ADD2,   
		dbo.POL_WATERCRAFT_DRIVER_DETAILS.DRIVER_STATE,   
		dbo.POL_WATERCRAFT_DRIVER_DETAILS.DRIVER_DOB, 
		dbo.POL_WATERCRAFT_DRIVER_DETAILS.DRIVER_SSN,   
		dbo.POL_WATERCRAFT_DRIVER_DETAILS.DRIVER_DRIV_LIC, 
		dbo.MNT_COUNTRY_STATE_LIST.STATE_CODE,   
		dbo.POL_WATERCRAFT_DRIVER_DETAILS.DRIVER_SEX,  
		(SELECT STATE_ID FROM POL_CUSTOMER_POLICY_LIST 
			WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID) STATE_ID  
	FROM 
	dbo.POL_WATERCRAFT_DRIVER_DETAILS INNER JOIN dbo.MNT_COUNTRY_STATE_LIST ON   
	dbo.POL_WATERCRAFT_DRIVER_DETAILS.DRIVER_LIC_STATE = dbo.MNT_COUNTRY_STATE_LIST.STATE_ID   
	WHERE ( CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID )  

  



GO

