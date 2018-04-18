IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPOL_UMBRELLA_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPOL_UMBRELLA_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                          
Proc Name       : Proc_InsertPOL_UMBRELLA_MVR_INFORMATION      
Created by      : Sumit Chhabra
Date            : 22-03-2006      
Purpose         : Insert of Umbrella Driver/Operator MVR Information              
Revison History :                          
Used In         : Wolverine                          
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                          
CREATE PROC Dbo.Proc_InsertPOL_UMBRELLA_MVR_INFORMATION                          
(                          
 @CUSTOMER_ID  int,                          
 @POLICY_ID  int,                          
 @POLICY_VERSION_ID int,                          
 @DRIVER_ID  int,                          
 @POL_UMB_MVR_ID  int OUTPUT,                          
 @VIOLATION_ID  int,                          
 @MVR_AMOUNT  decimal(20,0),                
 @MVR_DEATH  nvarchar(2),                          
 @MVR_DATE  datetime,                      
 @IS_ACTIVE  nvarchar(2) ,            
 @VIOLATION_SOURCE varchar(10) = null,    
 @VERIFIED smallint,  
 @VIOLATION_TYPE int,
 @CREATED_BY int,
 @CREATED_DATETIME datetime                            
)                          
AS                          
BEGIN                          

  SELECT @POL_UMB_MVR_ID=ISNULL(MAX(POL_UMB_MVR_ID),0)+1 FROM POL_UMBRELLA_MVR_INFORMATION          
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND DRIVER_ID=@DRIVER_ID                        
                 
  INSERT INTO POL_UMBRELLA_MVR_INFORMATION      
     (CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, DRIVER_ID,POL_UMB_MVR_ID,VIOLATION_ID,                          
     MVR_AMOUNT, MVR_DEATH, MVR_DATE, IS_ACTIVE, VIOLATION_SOURCE,VERIFIED,VIOLATION_TYPE,CREATED_BY,CREATED_DATETIME)                          
  VALUES                          
     (@CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@DRIVER_ID,@POL_UMB_MVR_ID,@VIOLATION_ID,                  
     @MVR_AMOUNT,@MVR_DEATH,@MVR_DATE,@IS_ACTIVE, @VIOLATION_SOURCE,@VERIFIED, @VIOLATION_TYPE,@CREATED_BY,@CREATED_DATETIME)                
END    
    


GO

