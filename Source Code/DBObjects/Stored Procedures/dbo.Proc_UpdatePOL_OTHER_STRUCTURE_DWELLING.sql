IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePOL_OTHER_STRUCTURE_DWELLING]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePOL_OTHER_STRUCTURE_DWELLING]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

    
    
/* ----------------------------------------------------------------------------------            
Proc Name       : dbo.Proc_UpdatePOL_OTHER_STRUCTURE_DWELLING            
Created by      : Ashwani            
Date            : 29 June,2006            
Purpose     :            
Revison History :            
Used In  : Wolverine            
------------------------------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       ---------------------------------------------------*/            
-- drop PROC dbo.Proc_UpdatePOL_OTHER_STRUCTURE_DWELLING            
CREATE PROC dbo.Proc_UpdatePOL_OTHER_STRUCTURE_DWELLING            
(            
@CUSTOMER_ID     int,            
@POL_ID     int,            
@POL_VERSION_ID     smallint,            
@DWELLING_ID     smallint,            
@OTHER_STRUCTURE_ID     smallint,            
@PREMISES_LOCATION     nvarchar(10),            
@PREMISES_DESCRIPTION     nvarchar(600),            
@PREMISES_USE     nvarchar(300),            
@PREMISES_CONDITION     nvarchar(10),            
@PICTURE_ATTACHED     nvarchar(10),            
@COVERAGE_BASIS     nvarchar(10),            
@SATELLITE_EQUIPMENT     nvarchar(10),            
@LOCATION_ADDRESS     nvarchar(200),            
@LOCATION_CITY     nvarchar(100),            
@LOCATION_STATE     nvarchar(10),            
@LOCATION_ZIP     nvarchar(40),            
@ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED     int,            
@INSURING_VALUE     int,            
@INSURING_VALUE_OFF_PREMISES     decimal(18,2),            
@MODIFIED_BY     int,            
@LAST_UPDATED_DATETIME     datetime,          
@COVERAGE_AMOUNT decimal(10,2)=NULL,    
@LIABILITY_EXTENDED smallint = null ,  
@SOLID_FUEL_DEVICE NVARCHAR(10)=NULL, --Added by Charles on 27-Nov-09 for Itrack 6681      
@APPLY_ENDS NVARCHAR(10)=NULL --Added by Charles on 3-Dec-09 for Itrack 6405             
)            
AS            
BEGIN            
            
DECLARE @COVERAGEB INT            
DECLARE @IN_VAL INT            
DECLARE @DIFF_VAL INT            
DECLARE @COVBADD_VAL INT            
            
--find the sum of insuring value before adding new value (this will give old sum)            
SELECT @IN_VAL=ISNULL(SUM(ISNULL(ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED,0)),0)            
 FROM POL_OTHER_STRUCTURE_DWELLING STR4 WITH(NOLOCK)            
 WHERE             
     STR4.PREMISES_LOCATION = 11841 -- Off Primises            
 AND STR4.CUSTOMER_ID = @CUSTOMER_ID            
 AND STR4.POLICY_ID = @POL_ID            
 AND STR4.POLICY_VERSION_ID=@POL_VERSION_ID            
    and STR4.DWELLING_ID=@DWELLING_ID            
    AND ISNULL(STR4.SATELLITE_EQUIPMENT,'0')='2'            
 and STR4.IS_ACTIVE='Y'            
            
            
            
--find value in coverage b..already saved            
SELECT @COVERAGEB=ISNULL(DEDUCTIBLE_1,0) FROM POL_DWELLING_SECTION_COVERAGES            
 WHERE  CUSTOMER_ID = @CUSTOMER_ID            
 AND POLICY_ID = @POL_ID            
 AND POLICY_VERSION_ID=@POL_VERSION_ID            
    AND DWELLING_ID=@DWELLING_ID            
    AND COVERAGE_CODE_ID IN(774,794)            
    
-- Added by Asfa (15-Feb-2008) - iTrack issue 3495           
if @SATELLITE_EQUIPMENT=''    
  set @SATELLITE_EQUIPMENT=null    
  
--Added by Charles on 27-Nov-09 for Itrack 6681   
IF @SOLID_FUEL_DEVICE = ''  
 SET @SOLID_FUEL_DEVICE = NULL   

--Added by Charles on 3-Dec-09 for Itrack 6405 
IF @APPLY_ENDS = ''  
 SET @APPLY_ENDS = NULL            
         
            
--change in value of coverage b with             
SET @DIFF_VAL=ISNULL(@COVERAGEB,0) - ISNULL(@IN_VAL,0)            
Update  POL_OTHER_STRUCTURE_DWELLING            
set            
POLICY_ID  =  @POL_ID,            
POLICY_VERSION_ID  =  @POL_VERSION_ID,            
DWELLING_ID  =  @DWELLING_ID,            
OTHER_STRUCTURE_ID  =  @OTHER_STRUCTURE_ID,            
PREMISES_LOCATION  =  @PREMISES_LOCATION,            
PREMISES_DESCRIPTION  =  @PREMISES_DESCRIPTION,            
PREMISES_USE  =  @PREMISES_USE,            
PREMISES_CONDITION  =  @PREMISES_CONDITION,            
PICTURE_ATTACHED  = @PICTURE_ATTACHED,            
COVERAGE_BASIS  =  @COVERAGE_BASIS,            
SATELLITE_EQUIPMENT  =  @SATELLITE_EQUIPMENT,            
LOCATION_ADDRESS  =  @LOCATION_ADDRESS,            
LOCATION_CITY  =  @LOCATION_CITY,            
LOCATION_STATE  =  @LOCATION_STATE,            
LOCATION_ZIP  =  @LOCATION_ZIP,            
ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED  =  @ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED,            
INSURING_VALUE  =  @INSURING_VALUE,            
INSURING_VALUE_OFF_PREMISES  =  @INSURING_VALUE_OFF_PREMISES,            
MODIFIED_BY  =  @MODIFIED_BY,            
LAST_UPDATED_DATETIME  =  @LAST_UPDATED_DATETIME,          
COVERAGE_AMOUNT  = @COVERAGE_AMOUNT,    
LIABILITY_EXTENDED = @LIABILITY_EXTENDED,  
SOLID_FUEL_DEVICE = @SOLID_FUEL_DEVICE,  --Added by Charles on 27-Nov-09 for Itrack 6681          
APPLY_ENDS = @APPLY_ENDS --Added by Charles on 3-Dec-09 for Itrack 6405 
            
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND DWELLING_ID=@DWELLING_ID AND OTHER_STRUCTURE_ID=@OTHER_STRUCTURE_ID             
            
SELECT @IN_VAL=ISNULL(SUM(ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED),0)            
 FROM POL_OTHER_STRUCTURE_DWELLING STR4 WITH(NOLOCK)            
 WHERE             
     STR4.PREMISES_LOCATION = 11841 -- Off Primises            
 AND STR4.CUSTOMER_ID = @CUSTOMER_ID            
 AND STR4.POLICY_ID = @POL_ID            
 AND STR4.POLICY_VERSION_ID=@POL_VERSION_ID            
     and STR4.DWELLING_ID=@DWELLING_ID            
    AND ISNULL(STR4.SATELLITE_EQUIPMENT,'0')='2'            
    and STR4.IS_ACTIVE='Y'            
            
            
SET @COVBADD_VAL=@IN_VAL+@DIFF_VAL            
 if(@COVBADD_VAL > 0)            
  begin              
    exec Proc_Update_POL_Additional_CoverageB @CUSTOMER_ID ,@POL_ID,@POL_VERSION_ID,@DWELLING_ID,@COVBADD_VAL            
  end            
            
END            
            
            
  
GO

