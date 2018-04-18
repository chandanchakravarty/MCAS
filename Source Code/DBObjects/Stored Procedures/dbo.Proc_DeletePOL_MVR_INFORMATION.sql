IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePOL_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePOL_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
          
Proc Name       : Proc_DeletePOL_MVR_INFORMATION          
Created by      : Anurag Verma         
Date            : 11/08/2005          
Purpose         : Delete of Driver Policy MVR Information          
Revison History :          
Used In                   : Wolverine          
------------------------------------------------------------          
Date      Review By          Comments          
    
28th Mar'06 Swastika   Update class on deletion of MVR    
------    ------------       -------------------------*/   

--Drop PROC dbo.Proc_DeletePOL_MVR_INFORMATION        
CREATE PROC dbo.Proc_DeletePOL_MVR_INFORMATION          
(          
 @POL_MVR_ID  int,      
 @CUSTOMER_ID  int,      
 @POL_ID INT,      
 @POL_VERSION_ID INT,      
 @DRIVER_ID INT,    
 @CALLED_FROM varchar(10)           
)          
AS          
BEGIN          
 DELETE FROM POL_MVR_INFORMATION WHERE POL_MVR_ID=@POL_MVR_ID AND      
 CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND DRIVER_ID=@DRIVER_ID      
-- Start : Added by Swastika  for updating class on deletion of MVR    
/* IF(UPPER(@CALLED_FROM)='PPA')            
 BEGIN      
  EXEC PROC_SETVEHICLECLASSRULEPOL @CUSTOMER_ID,@POL_ID,@POL_VERSION_ID,@DRIVER_ID          
 END      
-- End    
*/  
   IF (UPPER(@CALLED_FROM)='MOT')            
   EXEC Proc_SetPreferredRiskDiscountForPolicy @CUSTOMER_ID,@POL_ID,@POL_VERSION_ID,@DRIVER_ID  

 --update "MVR ordered" and "Date MVR ordered" fields on Driver tab   
 Update POL_DRIVER_DETAILS set DATE_ORDERED = NULL ,MVR_ORDERED=''   --yes  
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND DRIVER_ID=@DRIVER_ID      
END     
    
    
    
    
  



GO

