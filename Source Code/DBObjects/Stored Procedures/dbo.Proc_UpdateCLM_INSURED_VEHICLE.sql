IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_INSURED_VEHICLE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_INSURED_VEHICLE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------            
Proc Name       : Proc_UpdateCLM_INSURED_VEHICLE            
Created by      : Ebix            
Date            : 5/1/2006            
Purpose       :Evaluation            
Revison History :            
Used In        : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
CREATE PROC dbo.Proc_UpdateCLM_INSURED_VEHICLE            
(            
@INSURED_VEHICLE_ID     int,            
@CLAIM_ID     int,            
@NON_OWNED_VEHICLE     char(1),            
@VEHICLE_YEAR     varchar(4),            
@MAKE     varchar(150),            
@MODEL     varchar(150),            
@VIN     varchar(150),            
@BODY_TYPE     varchar(150),            
@PLATE_NUMBER     varchar(10),            
@STATE     int,            
@OWNER_ID     int,            
@DRIVER_ID    int,            
@MODIFIED_BY     int,            
@LAST_UPDATED_DATETIME     datetime,      
@WHERE_VEHICLE_SEEN varchar(50),      
@WHEN_VEHICLE_SEEN  varchar(25),    
@DESCRIBE_DAMAGE varchar(100),    
@USED_WITH_PERMISSION int,
@PURPOSE_OF_USE varchar(50),    
@ESTIMATE_AMOUNT decimal(12,2),    
@OTHER_VEHICLE_INSURANCE varchar(100),  
@POLICY_VEHICLE_ID int  
)            
AS            
BEGIN            
Update  CLM_INSURED_VEHICLE            
set            
NON_OWNED_VEHICLE  =  @NON_OWNED_VEHICLE,            
VEHICLE_YEAR  =  @VEHICLE_YEAR,            
MAKE  =  @MAKE,            
MODEL  =  @MODEL,            
VIN  =  @VIN,            
BODY_TYPE  =  @BODY_TYPE,            
PLATE_NUMBER  =  @PLATE_NUMBER,            
STATE  =  @STATE,            
MODIFIED_BY  =  @MODIFIED_BY,            
LAST_UPDATED_DATETIME  =  @LAST_UPDATED_DATETIME,            
OWNER_ID = @OWNER_ID,        
DRIVER_ID = @DRIVER_ID,      
WHERE_VEHICLE_SEEN = @WHERE_VEHICLE_SEEN,      
WHEN_VEHICLE_SEEN = @WHEN_VEHICLE_SEEN,    
DESCRIBE_DAMAGE = @DESCRIBE_DAMAGE,    
USED_WITH_PERMISSION =@USED_WITH_PERMISSION,    
PURPOSE_OF_USE = @PURPOSE_OF_USE,    
ESTIMATE_AMOUNT = @ESTIMATE_AMOUNT,    
OTHER_VEHICLE_INSURANCE = @OTHER_VEHICLE_INSURANCE--,  
--POLICY_VEHICLE_ID = @POLICY_VEHICLE_ID            
where  INSURED_VEHICLE_ID = @INSURED_VEHICLE_ID AND   CLAIM_ID = @CLAIM_ID          
END            
            
            
            
          
        
      
    
  





GO

