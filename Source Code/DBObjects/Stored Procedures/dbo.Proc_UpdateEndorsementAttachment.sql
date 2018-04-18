IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateEndorsementAttachment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateEndorsementAttachment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--begin tran 
--drop PROC dbo.Proc_UpdateEndorsementAttachment   
--go 
/*----------------------------------------------------------                
Proc Name       : dbo.Proc_UpdateEndorsementAttachment                
Created by      : Gaurav                
Date            : 10/20/2005                
Purpose       :Evaluation                
Revison History :                
Used In        : Wolverine                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/  
--drop PROC dbo.Proc_UpdateEndorsementAttachment                
CREATE PROC dbo.Proc_UpdateEndorsementAttachment                
(                
@ENDORSEMENT_ATTACH_ID     int,                
@ENDORSEMENT_ID     int,                
@ATTACH_FILE     varchar(500),                
@VALID_DATE     datetime ,      
@EFFECTIVE_TO_DATE datetime=null,            
@DISABLED_DATE datetime=null,       
@FORM_NUMBER varchar(20)=null,          
@EDITION_DATE varchar (20)              
)                
AS                
BEGIN              

	DECLARE @OLD_VALID_DATE				DateTime,
			@OLD_EFFECTIVE_TO_DATE		DateTime,
			@OLD_DISABLED_DATE			DateTime

	SELECT  @OLD_VALID_DATE				= VALID_DATE,
			@OLD_EFFECTIVE_TO_DATE		= EFFECTIVE_TO_DATE,
			@OLD_DISABLED_DATE			= DISABLED_DATE
	FROM MNT_ENDORSEMENT_ATTACHMENT WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID 
	AND ENDORSEMENT_ATTACH_ID = @ENDORSEMENT_ATTACH_ID

	IF( ISNULL(@OLD_VALID_DATE ,'12-31-3000') = ISNULL(@VALID_DATE,'12-31-3000') 
		AND ISNULL(@EFFECTIVE_TO_DATE ,'12-31-3000') = ISNULL(@OLD_EFFECTIVE_TO_DATE,'12-31-3000') 
		AND ISNULL(@DISABLED_DATE,'12-31-3000')  = ISNULL(@OLD_DISABLED_DATE,'12-31-3000') )
	BEGIN 
		UPDATE  MNT_ENDORSEMENT_ATTACHMENT                
		SET                
		ENDORSEMENT_ID   =  @ENDORSEMENT_ID,                
		ATTACH_FILE    =  @ATTACH_FILE,                
		FORM_NUMBER   =  @FORM_NUMBER,          
		EDITION_DATE   =  @EDITION_DATE              
		WHERE  ENDORSEMENT_ATTACH_ID = @ENDORSEMENT_ATTACH_ID         
	END 
	ELSE
	BEGIN 
		IF EXISTS (SELECT TOP 1 ATTACH_FILE FROM MNT_ENDORSEMENT_ATTACHMENT WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID 
				AND EFFECTIVE_TO_DATE IS NULL AND     
			  DISABLED_DATE IS NULL AND ENDORSEMENT_ATTACH_ID <> @ENDORSEMENT_ATTACH_ID)    
		BEGIN 
			RETURN -1 
		END


		IF(@EFFECTIVE_TO_DATE IS NULL)    
		BEGIN     

			/***************************************************************************************/  
			/* When we are not providing EFFECTIVE_TO_DATE and VALID_DATE is before of existing VALID_DATE */  
			/***************************************************************************************/  

			IF EXISTS ( SELECT ENDORSEMENT_ATTACH_ID FROM MNT_ENDORSEMENT_ATTACHMENT     
						WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID    
						AND VALID_DATE >= @VALID_DATE AND ENDORSEMENT_ATTACH_ID <> @ENDORSEMENT_ATTACH_ID)    

			BEGIN 
				return -1  
			END
			/**************************************************************************************************************/  
			/* When we are not providing EFFECTIVE_TO_DATE and VALID_DATE is before of existing VALID_DATE without having EFFECTIVE_TO_DATE*/   
			/***************************************************************************************************************/  

			IF EXISTS ( SELECT ENDORSEMENT_ATTACH_ID FROM MNT_ENDORSEMENT_ATTACHMENT     
						WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID    
						AND @VALID_DATE BETWEEN VALID_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-12-31') AND ENDORSEMENT_ATTACH_ID <> @ENDORSEMENT_ATTACH_ID)    
			BEGIN 
				return -1 
			END    

		END     
		ELSE    
			BEGIN   
			/*********************************************************************************/  
			/* When we are providing EFFECTIVE_TO_DATE and it is before of existing VALID_DATE */  
			/*********************************************************************************/  

			IF EXISTS ( SELECT ENDORSEMENT_ATTACH_ID FROM MNT_ENDORSEMENT_ATTACHMENT     
						WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID   AND ENDORSEMENT_ATTACH_ID <> @ENDORSEMENT_ATTACH_ID      
						AND (    
							(@VALID_DATE BETWEEN VALID_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-12-31'))    
						OR    
							(@EFFECTIVE_TO_DATE BETWEEN VALID_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-12-31'))    
							)
						    
						)     
			BEGIN
				return -1   
			END

			/***********************************************************************************************************/  
			/* When we are providing EFFECTIVE_TO_DATE and it is before of existing VALID_DATE without having EFFECTIVE_TO_DATE*/   
			/***********************************************************************************************************/  

			IF EXISTS ( SELECT ENDORSEMENT_ATTACH_ID FROM MNT_ENDORSEMENT_ATTACHMENT     
						WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID   AND ENDORSEMENT_ATTACH_ID <> @ENDORSEMENT_ATTACH_ID  
						AND (    
							(@VALID_DATE <= VALID_DATE )    
							AND    
							(@EFFECTIVE_TO_DATE >= ISNULL(EFFECTIVE_TO_DATE,'3000-12-31'))    
							)    
						)     
			BEGIN 
				return -1 
			END

			IF EXISTS ( SELECT ENDORSEMENT_ATTACH_ID FROM MNT_ENDORSEMENT_ATTACHMENT     
						WHERE ENDORSEMENT_ID=@ENDORSEMENT_ID    
						AND VALID_DATE >= @VALID_DATE and 
						(@EFFECTIVE_TO_DATE BETWEEN VALID_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-12-31')) AND ENDORSEMENT_ATTACH_ID <> @ENDORSEMENT_ATTACH_ID  )    
			BEGIN 
					return -1     
			END
		END    
	           

	        
		Update  MNT_ENDORSEMENT_ATTACHMENT                
		set                
		ENDORSEMENT_ID   =  @ENDORSEMENT_ID,                
		ATTACH_FILE    =  @ATTACH_FILE,                
		VALID_DATE    =  @VALID_DATE  ,      
		EFFECTIVE_TO_DATE       =  @EFFECTIVE_TO_DATE,            
		DISABLED_DATE           =  @DISABLED_DATE ,      
		FORM_NUMBER   =  @FORM_NUMBER,          
		EDITION_DATE   =  @EDITION_DATE              
		                
		where  ENDORSEMENT_ATTACH_ID = @ENDORSEMENT_ATTACH_ID          
	END      
	                
END                
              
     
--go 
--
--exec Proc_UpdateEndorsementAttachment   216 , 67 , 'aaaa.pdf' , '12-01-2009', '12-31-2009' , null , 'OP-900' , '12-2009' 
--
--SELECT *  FROM MNT_ENDORSEMENT_ATTACHMENT   WHERE ENDORSEMENT_ID=67    
--
--rollback tran 





GO

