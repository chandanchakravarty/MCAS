IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetLossReportOrder]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetLossReportOrder]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                
Proc Name                : Dbo.Proc_SetLossReportOrder                                      
Created by               :Mohit Agarwal                                                              
Date                    : 31-Oct-2007                                                             
Purpose                  : To set Location Loss Ordered field              
Revison History          :                                                                
Used In                  : Wolverine                                                                
------------------------------------------------------------                                                                
Date     Review By          Comments                                                               
drop proc dbo.Proc_SetLossReportOrder                                                         
------   ------------       -------------------------*/                                                                
CREATE proc [dbo].[Proc_SetLossReportOrder]                                                             
(                                                                
 @CUSTOMER_ID    int,                                                                
 @APP_ID    int,                                                                
 @APP_VERSION_ID   int,                 
 @LOCATION_ID   int,     
 @CALLED_FROM varchar(50)=null ,
 @REPORT_STATUS char(1)=null               
)                                                                
as       
BEGIN
                                                
IF UPPER(ISNULL(@CALLED_FROM,'')) = 'HOME'              
BEGIN      
UPDATE APP_LOCATIONS SET LOSSREPORT_ORDER = 10963,     
   LOSSREPORT_DATETIME = GetDate() ,REPORT_STATUS=@REPORT_STATUS
WHERE @CUSTOMER_ID=CUSTOMER_ID AND @APP_ID=APP_ID AND @APP_VERSION_ID=APP_VERSION_ID        
AND @LOCATION_ID=LOCATION_ID        
END      
      
ELSE IF UPPER(ISNULL(@CALLED_FROM,'')) = 'WAT'  
BEGIN      
UPDATE APP_WATERCRAFT_INFO SET LOSSREPORT_ORDER = 10963,     
   LOSSREPORT_DATETIME = GetDate() 
WHERE @CUSTOMER_ID=CUSTOMER_ID AND @APP_ID=APP_ID AND @APP_VERSION_ID=APP_VERSION_ID        
AND BOAT_ID=@LOCATION_ID        
END      

ELSE IF UPPER(ISNULL(@CALLED_FROM,'')) = 'PPA'  
BEGIN      
UPDATE APP_DRIVER_DETAILS SET LOSSREPORT_ORDER = 10963,     
   LOSSREPORT_DATETIME = GetDate()  
WHERE @CUSTOMER_ID=CUSTOMER_ID AND @APP_ID=APP_ID AND @APP_VERSION_ID=APP_VERSION_ID        
AND DRIVER_ID=@LOCATION_ID        
END      
    
ELSE IF UPPER(ISNULL(@CALLED_FROM,'')) = 'HOME-POL'              
BEGIN      
UPDATE POL_LOCATIONS SET LOSSREPORT_ORDER = 10963,     
   LOSSREPORT_DATETIME = GetDate()  ,REPORT_STATUS=@REPORT_STATUS
WHERE @CUSTOMER_ID=CUSTOMER_ID AND @APP_ID=POLICY_ID AND @APP_VERSION_ID=POLICY_VERSION_ID        
AND @LOCATION_ID=LOCATION_ID        
END      
      
ELSE IF UPPER(ISNULL(@CALLED_FROM,'')) = 'WAT-POL'   
BEGIN     
UPDATE POL_WATERCRAFT_INFO SET LOSSREPORT_ORDER = 10963,     
   LOSSREPORT_DATETIME = GetDate()  
WHERE @CUSTOMER_ID=CUSTOMER_ID AND @APP_ID=POLICY_ID AND @APP_VERSION_ID=POLICY_VERSION_ID        
AND BOAT_ID=@LOCATION_ID        
END      

ELSE IF UPPER(ISNULL(@CALLED_FROM,'')) = 'PPA-POL'  
BEGIN      
UPDATE POL_DRIVER_DETAILS SET LOSSREPORT_ORDER = 10963,     
   LOSSREPORT_DATETIME = GetDate()  
WHERE @CUSTOMER_ID=CUSTOMER_ID AND @APP_ID=POLICY_ID AND @APP_VERSION_ID=POLICY_VERSION_ID        
AND DRIVER_ID=@LOCATION_ID        
END   
END      
    
  







GO

