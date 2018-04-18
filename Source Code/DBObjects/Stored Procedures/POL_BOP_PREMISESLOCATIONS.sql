  
---------------------------------------------------------------  
--Proc Name          : dbo.POL_BOP_PREMISES_INFO]  
--Created by         : Rajeev          
--Date               :  16 NOVEMBER 2011         
--------------------------------------------------------  
/*--Date     Review By          Comments      
[GET_POL_BOP_PREMISESLOCATIONS]    8,1,1,1
------   ------------       -------------------------*/      
       
ALTER  PROCEDURE [dbo].[GET_POL_BOP_PREMISESLOCATIONS]        
(         
 --@BLDNG_ID int,  
 @CUSTOMER_ID int,  
 @POLICY_ID int ,  
 @POLICY_VERSION_ID smallint--,  
 --@LOCATION_ID smallint   
 --@PREMISES_ID int  
)         
AS         
BEGIN        
  Select * FROM POL_BOP_PREMISESLOCATIONS  
    
  where   
  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID   
  and POLICY_VERSION_ID=@POLICY_VERSION_ID  
  --and LOCATION_ID=@LOCATION_ID  
  --and PREMISES_ID=@PREMISES_ID  
    
End