IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchLocationDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchLocationDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name       : dbo.Proc_FetchLocationDetails                
Created by      : Chetna Agarwal      
Date            : 19/04/2010                
Purpose   :To FETCH applicants Details    
Revison History :                
Used In   : Ebix Advantage                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
--DROP PROC dbo.Proc_FetchLocationDetails     
    
CREATE PROC [dbo].[Proc_FetchLocationDetails]    
(    
@CUSTOMER_ID INT,   
@LOCATION_ID INT ,
@CALLEDFROM NVARCHAR(20)=NULL ,
@ADDRESS NVARCHAR(200)=NULL
)    
AS 


 if(UPPER(@CALLEDFROM)='POL_LOC')
 BEGIN 
 SELECT @LOCATION_ID= max(LOCATION_ID) FROM POL_LOCATIONS WITH(NOLOCK) WHERE LOC_NUM=@LOCATION_ID AND CUSTOMER_ID=@CUSTOMER_ID 
 END
BEGIN  

select    
LOCATION_ID,    
POLICY_ID,     
POLICY_VERSION_ID,     
CUSTOMER_ID,
CAL_NUM, 
LOC_NUM,
LOC_ZIP, 
NAME,
LOC_ADD1,
LOC_ADD2,
NUMBER,
LOC_ADD2,
DISTRICT,
LOC_CITY,
LOC_COUNTY,
LOC_STATE,
OCCUPIED,
PHONE_NUMBER,
EXT,
FAX_NUMBER,
CATEGORY,
ACTIVITY_TYPE,
CONSTRUCTION,
DESCRIPTION  
FROM     
POL_LOCATIONS with(nolock)     
WHERE     
CUSTOMER_ID=@CUSTOMER_ID and LOCATION_ID= @LOCATION_ID      
END
GO

