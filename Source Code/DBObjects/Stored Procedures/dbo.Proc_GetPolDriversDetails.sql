IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolDriversDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolDriversDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                          
Proc Name                : Dbo.Proc_GetPolDriversDetails                                
Created by               :Ashish kumar                                                        
Date                     : 16 Mar 2006                                                       
Purpose                  : To all drivers of the policy     
Revison History          :                                                          
Used In                  : Wolverine                                                          
------------------------------------------------------------                                                          
Date     Review By          Comments                                                         
drop proc dbo.Proc_GetPolDriversDetails 1081,136,1                                                       
------   ------------       -------------------------*/                                                          
CREATE proc dbo.Proc_GetPolDriversDetails                                                       
(                                                          
 @CUSTOMER_ID    int,                                                          
 @POL_ID    int,                                                          
 @POLICY_VERSION_ID   int ,  
 @DRIVER_ID int = null  
)                                                          
as                                           
  
IF  (@DRIVER_ID  IS NOT null)  
 SELECT     dbo.POL_DRIVER_DETAILS.DRIVER_ID, dbo.POL_DRIVER_DETAILS.DRIVER_FNAME, dbo.POL_DRIVER_DETAILS.DRIVER_MNAME,         
           dbo.POL_DRIVER_DETAILS.DRIVER_LNAME, dbo.POL_DRIVER_DETAILS.DRIVER_CITY, dbo.POL_DRIVER_DETAILS.DRIVER_ZIP, dbo.POL_DRIVER_DETAILS.DRIVER_CODE, dbo.POL_DRIVER_DETAILS.DRIVER_SUFFIX,         
           dbo.POL_DRIVER_DETAILS.DRIVER_ADD1,dbo.POL_DRIVER_DETAILS.DRIVER_ADD2, dbo.POL_DRIVER_DETAILS.DRIVER_STATE, dbo.POL_DRIVER_DETAILS.DRIVER_DOB,         
           dbo.POL_DRIVER_DETAILS.DRIVER_SSN, dbo.POL_DRIVER_DETAILS.DRIVER_SEX, dbo.POL_DRIVER_DETAILS.DRIVER_DRIV_LIC,         
           dbo.MNT_COUNTRY_STATE_LIST.STATE_CODE,(SELECT STATE_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID) STATE_ID,
 	dbo.POL_DRIVER_DETAILS.MVR_CLASS,dbo.POL_DRIVER_DETAILS.MVR_LIC_CLASS,dbo.POL_DRIVER_DETAILS.MVR_LIC_RESTR,dbo.POL_DRIVER_DETAILS.MVR_DRIV_LIC_APPL      
 FROM       dbo.POL_DRIVER_DETAILS LEFT JOIN        
           dbo.MNT_COUNTRY_STATE_LIST ON dbo.POL_DRIVER_DETAILS.DRIVER_LIC_STATE = dbo.MNT_COUNTRY_STATE_LIST.STATE_ID         
      AND POL_DRIVER_DETAILS.DRIVER_COUNTRY = MNT_COUNTRY_STATE_LIST.COUNTRY_ID      
 where ( CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POL_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID AND @DRIVER_ID=DRIVER_ID)     
  
ELSE    
 SELECT     dbo.POL_DRIVER_DETAILS.DRIVER_ID, dbo.POL_DRIVER_DETAILS.DRIVER_FNAME, dbo.POL_DRIVER_DETAILS.DRIVER_MNAME,         
           dbo.POL_DRIVER_DETAILS.DRIVER_LNAME, dbo.POL_DRIVER_DETAILS.DRIVER_CITY, dbo.POL_DRIVER_DETAILS.DRIVER_ZIP, dbo.POL_DRIVER_DETAILS.DRIVER_CODE, dbo.POL_DRIVER_DETAILS.DRIVER_SUFFIX,         
           dbo.POL_DRIVER_DETAILS.DRIVER_ADD1,dbo.POL_DRIVER_DETAILS.DRIVER_ADD2, dbo.POL_DRIVER_DETAILS.DRIVER_STATE, dbo.POL_DRIVER_DETAILS.DRIVER_DOB,         
           dbo.POL_DRIVER_DETAILS.DRIVER_SSN, dbo.POL_DRIVER_DETAILS.DRIVER_SEX, dbo.POL_DRIVER_DETAILS.DRIVER_DRIV_LIC,         
           dbo.MNT_COUNTRY_STATE_LIST.STATE_CODE,(SELECT STATE_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID) STATE_ID,      
	 dbo.POL_DRIVER_DETAILS.MVR_CLASS,dbo.POL_DRIVER_DETAILS.MVR_LIC_CLASS,dbo.POL_DRIVER_DETAILS.MVR_LIC_RESTR,dbo.POL_DRIVER_DETAILS.MVR_DRIV_LIC_APPL      
 FROM       dbo.POL_DRIVER_DETAILS LEFT JOIN        
           dbo.MNT_COUNTRY_STATE_LIST ON dbo.POL_DRIVER_DETAILS.DRIVER_LIC_STATE = dbo.MNT_COUNTRY_STATE_LIST.STATE_ID         
      AND POL_DRIVER_DETAILS.DRIVER_COUNTRY = MNT_COUNTRY_STATE_LIST.COUNTRY_ID      
 where ( CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POL_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID )     
  



GO

