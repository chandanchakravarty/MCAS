IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDriverDOBForPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDriverDOBForPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name          : Dbo. Proc_GetDriverDOBForPolicy    
Created by         : Shafi    
Date               : 09/02/2006   
Purpose            :     
Revison History    :    
Used In                :   Wolverine      
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--drop proc Proc_GetDriverDOBForPolicy  
CREATE   PROCEDURE Proc_GetDriverDOBForPolicy    
(    
 @CUSTOMER_ID int,    
 @POL_ID              int,    
 @POL_VERSION_ID int,    
 @DRIVER_ID            int  ,  
 @CALLEDFROM  VARCHAR(30)  
     
)    
AS    
BEGIN    
IF UPPER(@CALLEDFROM)='WAT' or UPPER(@CALLEDFROM)='HOME'   
BEGIN  
SELECT convert(varchar,DRIVER_DOB,101) DRIVER_DOB     
FROM POL_WATERCRAFT_DRIVER_DETAILS     
WHERE           CUSTOMER_ID=@CUSTOMER_ID AND  
  POLICY_ID=@POL_ID  AND     
         POLICY_VERSION_ID=@POL_VERSION_ID AND    
                DRIVER_ID=@DRIVER_ID    
END   
  
ELSE IF UPPER(@CALLEDFROM)='PPA' OR UPPER(@CALLEDFROM)='MOT'      
  BEGIN      
    SELECT convert(varchar,DRIVER_DOB,101) DRIVER_DOB FROM POL_DRIVER_DETAILS        
    WHERE     CUSTOMER_ID=@CUSTOMER_ID AND  
       POLICY_ID=@POL_ID  AND     
       POLICY_VERSION_ID=@POL_VERSION_ID AND    
       DRIVER_ID=@DRIVER_ID  
  END     
ELSE IF UPPER(@CALLEDFROM)='UMB'
  BEGIN      
    SELECT convert(varchar,DRIVER_DOB,101) DRIVER_DOB FROM POL_UMBRELLA_DRIVER_DETAILS        
    WHERE     CUSTOMER_ID=@CUSTOMER_ID AND  
       POLICY_ID=@POL_ID  AND     
       POLICY_VERSION_ID=@POL_VERSION_ID AND    
       DRIVER_ID=@DRIVER_ID  
  END       
 ELSE      
  RETURN -1     
   
    
END  
  


GO

