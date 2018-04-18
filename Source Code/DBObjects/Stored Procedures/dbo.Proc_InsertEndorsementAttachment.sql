IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertEndorsementAttachment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertEndorsementAttachment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_InsertEndorsementAttachment                  
Created by      : Gaurav                  
Date            : 10/20/2005                  
Purpose       :Evaluation                  
Revison History :                  
Used In        : Wolverine                  
  
Reviewed By : Anurag Verma  
Reviewed On : 06-07-2007  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/    
--drop PROC dbo.Proc_InsertEndorsementAttachment                  
CREATE PROC dbo.Proc_InsertEndorsementAttachment                  
(                  
@ENDORSEMENT_ATTACH_ID     int output,                  
@ENDORSEMENT_ID     int,                  
@ATTACH_FILE     varchar(500),                  
@VALID_DATE     datetime,          
@EFFECTIVE_TO_DATE datetime=null,              
@DISABLED_DATE datetime=null,           
@FORM_NUMBER varchar(20),            
@EDITION_DATE varchar(20)=null,           
@ATTACH_FILE1 varchar(500) output          
)                  
AS                  
BEGIN           
--DECLARE @ATTACH_FILE1 varchar(500)    
SET @ATTACH_FILE1 = ''           
SET @ENDORSEMENT_ATTACH_ID=0    
    
SET @ATTACH_FILE1 = (SELECT TOP 1 ATTACH_FILE FROM MNT_ENDORSEMENT_ATTACHMENT WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID 
AND EFFECTIVE_TO_DATE IS NULL AND     
      DISABLED_DATE IS NULL)    



IF(@EFFECTIVE_TO_DATE IS NULL)    
		BEGIN     

		/***************************************************************************************/  
		/* When we are not providing EFFECTIVE_TO_DATE and VALID_DATE is before of existing VALID_DATE */  
		/***************************************************************************************/  

		IF EXISTS ( SELECT ENDORSEMENT_ATTACH_ID FROM MNT_ENDORSEMENT_ATTACHMENT     
					WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID    
					AND VALID_DATE >= @VALID_DATE)    

		 return -1  

		/**************************************************************************************************************/  
		/* When we are not providing EFFECTIVE_TO_DATE and VALID_DATE is before of existing VALID_DATE without having EFFECTIVE_TO_DATE*/   
		/***************************************************************************************************************/  

		IF EXISTS ( SELECT ENDORSEMENT_ATTACH_ID FROM MNT_ENDORSEMENT_ATTACHMENT     
					WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID    
					AND @VALID_DATE BETWEEN VALID_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-12-31'))    

		return -1     

		END     
ELSE    
		BEGIN   
		/*********************************************************************************/  
		/* When we are providing EFFECTIVE_TO_DATE and it is before of existing VALID_DATE */  
		/*********************************************************************************/  

		IF EXISTS ( SELECT ENDORSEMENT_ATTACH_ID FROM MNT_ENDORSEMENT_ATTACHMENT     
					WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID        
					AND (    
						(@VALID_DATE BETWEEN VALID_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-12-31'))    
					OR    
						(@EFFECTIVE_TO_DATE BETWEEN VALID_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-12-31'))    
						)    
					)     

		return -1   

		/***********************************************************************************************************/  
		/* When we are providing EFFECTIVE_TO_DATE and it is before of existing VALID_DATE without having EFFECTIVE_TO_DATE*/   
		/***********************************************************************************************************/  

		IF EXISTS ( SELECT ENDORSEMENT_ATTACH_ID FROM MNT_ENDORSEMENT_ATTACHMENT     
					WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID     
					AND (    
						(@VALID_DATE <= VALID_DATE )    
						AND    
						(@EFFECTIVE_TO_DATE >= ISNULL(EFFECTIVE_TO_DATE,'3000-12-31'))    
						)    
					)     

		return -1 

		IF EXISTS ( SELECT ENDORSEMENT_ATTACH_ID FROM MNT_ENDORSEMENT_ATTACHMENT     
					WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID    
					AND VALID_DATE >= @VALID_DATE and 
					(@EFFECTIVE_TO_DATE BETWEEN VALID_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-12-31'))  )    

		 return -1     
		END    
           
    
print @ATTACH_FILE1    
IF @ATTACH_FILE1 != '' AND @ATTACH_FILE1 IS NOT NULL    
RETURN -1                   
    
if exists(select ENDORSEMENT_ID from MNT_ENDORSEMENT_ATTACHMENT where ENDORSEMENT_ID = @ENDORSEMENT_ID and VALID_DATE=@VALID_DATE)            
 SET @ENDORSEMENT_ATTACH_ID=-1            
ELSE            
BEGIN            
 select @ENDORSEMENT_ATTACH_ID=isnull(Max(ENDORSEMENT_ATTACH_ID),0)+1 from MNT_ENDORSEMENT_ATTACHMENT                  
 INSERT INTO MNT_ENDORSEMENT_ATTACHMENT                  
 (                  
 ENDORSEMENT_ATTACH_ID,                  
 ENDORSEMENT_ID,                  
 ATTACH_FILE,                  
 VALID_DATE,          
 EFFECTIVE_TO_DATE,          
 DISABLED_DATE,          
 FORM_NUMBER,          
 EDITION_DATE          
          
                   
 )                  
 VALUES                  
 (                  
 @ENDORSEMENT_ATTACH_ID,                  
 @ENDORSEMENT_ID,                  
 @ATTACH_FILE,                  
 @VALID_DATE,          
 @EFFECTIVE_TO_DATE,          
 @DISABLED_DATE,          
 @FORM_NUMBER,          
 @EDITION_DATE                  
 )                  
END    
    
RETURN 1                   
                  
END            
    



GO

