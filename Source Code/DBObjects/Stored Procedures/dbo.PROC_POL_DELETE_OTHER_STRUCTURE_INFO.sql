IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_POL_DELETE_OTHER_STRUCTURE_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_POL_DELETE_OTHER_STRUCTURE_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 



/*-----------------------------------------------------------------------------------------  
 Proc Name    : dbo.PROC_POL_DELETE_OTHER_STRUCTURE_INFO                          
 Created by   : Ashwani   
 Date         : 30 June,2006    
 Purpose      : Deletes a record from POL_OTHER_STRUCTURE_DWELLING            
 Revison History :            
 Used In  :   Wolverine                   
--------------------------------------------------------------------------------------------                            
Date      Review By           Comments                          
  
------   ------------       ----------------------------------------------------------*/         
-- drop proc dbo.POL_PROC_DELETE_OTHER_STRUCTURE_INFO                             
CREATE PROC DBO.PROC_POL_DELETE_OTHER_STRUCTURE_INFO                      
(            
 @CUSTOMER_ID Int,            
 @POL_ID Int,            
 @POL_VERSION_ID Int,            
 @DWELLING_ID smallint,  
 @OTHER_STRUCTURE_ID int                      
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



--change in value of coverage b with 
SET @DIFF_VAL=ISNULL(@COVERAGEB,0) - ISNULL(@IN_VAL,0)              
 --Delete from Home rating                      
  DELETE FROM POL_OTHER_STRUCTURE_DWELLING            
   WHERE  CUSTOMER_ID = @CUSTOMER_ID AND   POLICY_ID = @POL_ID AND  POLICY_VERSION_ID = @POL_VERSION_ID   
  AND DWELLING_ID = @DWELLING_ID  AND  OTHER_STRUCTURE_ID=@OTHER_STRUCTURE_ID   
             
 
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
 IF @@ERROR <> 0            
 BEGIN            
  RETURN -1            
 END                      
  RETURN 1            
END            
      
  
  
  
  
  
  
  
  
  
  
  





GO

