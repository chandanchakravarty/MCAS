IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyRatingInformationForWatercraft_SportEquipmentsComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyRatingInformationForWatercraft_SportEquipmentsComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------        
Proc Name                : Dbo.Proc_GetRatingInformationForWatercraft_SportEquipmentsComponent        
Created by               : shafi.        
Date                     : 01/03/2006       
Purpose                  : To get the information for creating the input xml          
Revison History          :        
Used In                  :   Creating InputXML for vehicle        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
      
create PROC Dbo.Proc_GetPolicyRatingInformationForWatercraft_SportEquipmentsComponent        
(        
@CUSTOMERID    int,        
@POLICYID    int,        
@POLICYVERSIONID   int,        
@EQUIP_ID    int        
)        
AS           
BEGIN          
set quoted_identifier off          
 DECLARE @SCH_MISC_ITEM_NAME  varchar(150)  
 DECLARE @SCH_MISC_ITEM_DESC  varchar(150)       
 DECLARE @SCH_MISC_DEDUCTIBLE varchar(100)        
 DECLARE @SCH_MISC_SERIAL_NO varchar(130)        
 DECLARE @SCH_MISC_ELECTRONIC varchar(100)        
 DECLARE @SCH_MISC_AMOUNT varchar(100)          
        

    

  
-- START           
SELECT    
 --@SCH_MISC_ITEM=ISNULL(MAKE,''),       
 @SCH_MISC_ITEM_NAME  =  (SELECT LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES WITH(NOLOCK) WHERE LOOKUP_UNIQUE_ID = ISNULL(EQUIP_TYPE,0)),
 @SCH_MISC_ITEM_DESC  = ISNULL(OTHER_DESCRIPTION,''),
 @SCH_MISC_SERIAL_NO  = ISNULL(SERIAL_NO,''),        
 @SCH_MISC_AMOUNT  = ISNULL(INSURED_VALUE,0),        
 @SCH_MISC_DEDUCTIBLE = ISNULL(EQUIP_AMOUNT,0),

 @SCH_MISC_ELECTRONIC = CASE ISNULL(EQUIPMENT_TYPE,0)           
 WHEN 11766 then 'Y'  
 ELSE 'N'      
 end     
FROM         
 POL_WATERCRAFT_EQUIP_DETAILLS   WITH (NOLOCK)         
WHERE        
 CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID AND EQUIP_ID = @EQUIP_ID   
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   
--------      
IF(@SCH_MISC_DEDUCTIBLE IS NULL)      
BEGIN       
 SET @SCH_MISC_DEDUCTIBLE=''      
END     
--------      
IF(@SCH_MISC_SERIAL_NO IS NULL)      
BEGIN       
 SET @SCH_MISC_SERIAL_NO=''      
END     
--------      
IF(@SCH_MISC_ELECTRONIC IS NULL)      
BEGIN       
 SET @SCH_MISC_ELECTRONIC='N'      
END     
--------      
IF(@SCH_MISC_AMOUNT IS NULL)      
BEGIN       
 SET @SCH_MISC_AMOUNT=''      
END     
    
------------------------------------------------------------------------------------------------------------------------------    
      
SELECT        
 @SCH_MISC_ITEM_NAME     AS SCH_MISC_ITEM_NAME,    
 @SCH_MISC_ITEM_DESC     AS SCH_MISC_ITEM_DESC,    
 @SCH_MISC_DEDUCTIBLE    AS SCH_MISC_DEDUCTIBLE,        
 @SCH_MISC_SERIAL_NO    AS SCH_MISC_SERIAL_NO,        
 @SCH_MISC_ELECTRONIC    AS SCH_MISC_ELECTRONIC,        
 @SCH_MISC_AMOUNT     AS SCH_MISC_AMOUNT            
END      
      
    
    
    
  











GO

