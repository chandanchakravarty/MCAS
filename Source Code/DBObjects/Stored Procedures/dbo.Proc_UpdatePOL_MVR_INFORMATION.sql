IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePOL_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePOL_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                      
Proc Name       : Dbo.Proc_UpdatePOL_MVR_INFORMATION                      
Created by      : Anurag Verma                 
Date            : 11/8/2005                      
Purpose         :Update of Driver Policy MVR Information                      
Revison History :                      
Used In                   : Wolverine                      
Modified by : Anurag Verma          
Modified On : 17/11/2005          
Purpose  : Changing where clause 
drop PROC dbo.Proc_UpdatePOL_MVR_INFORMATION          
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------*/                      
CREATE PROC dbo.Proc_UpdatePOL_MVR_INFORMATION                      
(                      
 @POL_MVR_ID  int,                      
 @CUSTOMER_ID    int,                      
 @POL_ID      int,                      
 @POL_VERSION_ID int,                      
 @VIOLATION_ID      int,                      
 @DRIVER_ID      int,                      
 @MVR_AMOUNT     decimal(20,0),            
 @MVR_DEATH      nvarchar(2),                      
 @MVR_DATE      datetime,        
 @VERIFIED smallint,      
 @CALLED_FROM varchar(10)=null,    
 @VIOLATION_TYPE int,
 @OCCURENCE_DATE DateTime,    
 @DETAILS varchar(500),  
 @POINTS_ASSIGNED int,  
 @ADJUST_VIOLATION_POINTS int          
                
)                      
AS                      
BEGIN                      
                      
 UPDATE  POL_MVR_INFORMATION                      
 SET                      
  VIOLATION_ID    =  @VIOLATION_ID,                      
  MVR_AMOUNT     =  @MVR_AMOUNT,                      
  MVR_DEATH     =  @MVR_DEATH,                      
  MVR_DATE       =  @MVR_DATE,        
  VERIFIED     =  @VERIFIED,    
  VIOLATION_TYPE  =  @VIOLATION_TYPE,
  OCCURENCE_DATE = @OCCURENCE_DATE,    
  DETAILS = @DETAILS,  
  POINTS_ASSIGNED = @POINTS_ASSIGNED,  
  ADJUST_VIOLATION_POINTS = @ADJUST_VIOLATION_POINTS          
    
        
 WHERE                    
  CUSTOMER_ID   =  @CUSTOMER_ID and                      
  POLICY_ID    =  @POL_ID and                      
  POLICY_VERSION_ID  =  @POL_VERSION_ID and          
  POL_MVR_ID = @POL_MVR_ID AND          
  DRIVER_ID=@DRIVER_ID              
      
  IF (UPPER(@CALLED_FROM)='MOT')              
   EXEC Proc_SetPreferredRiskDiscountForPolicy @CUSTOMER_ID,@POL_ID,@POL_VERSION_ID,@DRIVER_ID                        
END                
          
          
          
          
          
        
      
    
  



GO

