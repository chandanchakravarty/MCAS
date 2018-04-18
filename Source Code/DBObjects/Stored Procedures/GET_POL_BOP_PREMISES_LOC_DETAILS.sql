  
---------------------------------------------------------------  
--Proc Name          : dbo.POL_BOP_PREMISES_INFO]  
--Created by         : Rajeev          
--Date               :  16 NOVEMBER 2011         
--------------------------------------------------------  
--Date     Review By          Comments          
------   ------------       -------------------------*/      
       
ALTER  PROCEDURE [dbo].[GET_POL_BOP_PREMISES_LOC_DETAILS]        
(         
 --@BLDNG_ID int,  
 @CUSTOMER_ID int,  
 @POLICY_ID int ,  
 @POLICY_VERSION_ID smallint--,  
 --@LOCATION_ID smallint,   
 --@PREMISES_ID int  
)         
AS         
BEGIN        
  Select * FROM POL_BOP_PREMISES_LOC_DETAILS  
    
  --where BLDNG_ID=@BLDNG_ID  
    
  --Select * FROM POL_SUP_FORM_SHOP  
    
  where   
  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID   
  and POLICY_VERSION_ID=@POLICY_VERSION_ID  
  --and LOCATION_ID=@LOCATION_ID  
  --and PREMISES_ID=@PREMISES_ID  
    
End