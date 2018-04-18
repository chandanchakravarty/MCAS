 /*----------------------------------------------------------                            
Proc Name        :            dbo.Proc_GetXMLPOL_MVR_INFORMATION                             
Created by         :           Anurag Verma                            
Date                :           11/08/2005                            
Purpose           :           Get the table informatoin in the form of xml data                            
Revison History  :                            
Used In             :           Wolverine            
------------------------------------------------------------                            
Date     Review By          Comments        
drop PROC Dbo.Proc_GetXMLPOL_MVR_INFORMATION                       
------   ------------       -------------------------*/                            
ALTER  PROC Dbo.Proc_GetXMLPOL_MVR_INFORMATION                            
(                 
 @POL_MVR_ID  INT,          
 @CUSTOMER_ID INT,          
 @POLICY_ID INT,          
 @POLICY_VERSION_ID INT,          
 @DRIVER_ID INT                           
          
)                            
AS                            
BEGIN                            
 SELECT POL_MVR_ID,                            
 POL_MVR_INFORMATION.CUSTOMER_ID,                            
 POL_MVR_INFORMATION.POLICY_ID,                            
 POL_MVR_INFORMATION.POLICY_VERSION_ID,                            
 POL_MVR_INFORMATION.DRIVER_ID,                        
 VIOLATION_ID,                            
 POL_MVR_INFORMATION.DRIVER_ID,                            
 MVR_AMOUNT,                            
 MVR_DEATH,             
 VERIFIED,    
 VIOLATION_TYPE,                        
 convert(char,mvr_date,103) MVR_DATE,  
 convert(varchar, POL_MVR_INFORMATION.OCCURENCE_DATE,103) OCCURENCE_DATE,        
 POL_MVR_INFORMATION.DETAILS,      
 case when POL_MVR_INFORMATION.POINTS_ASSIGNED is null then '' else convert(varchar,POL_MVR_INFORMATION.POINTS_ASSIGNED) end POINTS_ASSIGNED,     
 case when POL_MVR_INFORMATION.ADJUST_VIOLATION_POINTS is null then '' else convert(varchar,POL_MVR_INFORMATION.ADJUST_VIOLATION_POINTS) end ADJUST_VIOLATION_POINTS,                
 POL_MVR_INFORMATION.IS_ACTIVE  ,            
 POL_DRIVER_DETAILS.DRIVER_CODE,          
 Convert(varchar,POL_DRIVER_DETAILS.DRIVER_DOB,101) DRIVER_DOB           
 FROM  POL_MVR_INFORMATION            
 INNER JOIN POL_DRIVER_DETAILS ON            
 POL_MVR_INFORMATION.DRIVER_ID =    POL_DRIVER_DETAILS.DRIVER_ID AND                        
 POL_MVR_INFORMATION.CUSTOMER_ID =    POL_DRIVER_DETAILS.CUSTOMER_ID AND      
POL_MVR_INFORMATION.POLICY_ID =    POL_DRIVER_DETAILS.POLICY_ID AND       
POL_MVR_INFORMATION.POLICY_VERSION_ID =    POL_DRIVER_DETAILS.POLICY_VERSION_ID      
 WHERE  POL_MVR_ID=@POL_MVR_ID AND                   
 POL_MVR_INFORMATION.CUSTOMER_ID=@CUSTOMER_ID AND          
 POL_MVR_INFORMATION.POLICY_ID= @POLICY_ID AND          
 POL_MVR_INFORMATION.POLICY_VERSION_ID= @POLICY_VERSION_ID AND          
 POL_MVR_INFORMATION.DRIVER_ID= @DRIVER_ID                                        
END              
          
          
          
            
          
          
          
          
          
        
      
    
  
  