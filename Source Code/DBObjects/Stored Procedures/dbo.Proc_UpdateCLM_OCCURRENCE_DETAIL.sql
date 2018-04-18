IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_OCCURRENCE_DETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_OCCURRENCE_DETAIL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------            
Proc Name       : dbo.Proc_UpdateCLM_OCCURRENCE_DETAIL            
Created by      : Vijay Arora            
Date            : 5/3/2006            
Purpose     : To update the record in table named CLM_OCCURRENCE_DETAIL            
Revison History :            
Used In        : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/        
--drop proc Dbo.Proc_UpdateCLM_OCCURRENCE_DETAIL                
CREATE PROC [dbo].[Proc_UpdateCLM_OCCURRENCE_DETAIL]            
(            
@OCCURRENCE_DETAIL_ID     int,            
@CLAIM_ID     int,            
@LOSS_DESCRIPTION     TEXT,            
@AUTHORITY_CONTACTED     varchar(100),            
@REPORT_NUMBER     varchar(20),            
@VIOLATIONS     varchar(300),            
@MODIFIED_BY     int,            
@LOSS_TYPE     varchar(150),          
@LOSS_LOCATION     varchar(300),  
@LOSS_LOCATION_ZIP     nvarchar(11),     -- Added by Santosh Kumar Gautam on 25 Nov 2010
@LOSS_LOCATION_CITY     varchar(150),    -- Added by Santosh Kumar Gautam on 25 Nov 2010      
@LOSS_LOCATION_STATE     int,            -- Added by Santosh Kumar Gautam on 25 Nov 2010      
@ESTIMATE_AMOUNT decimal(12,2),      
@OTHER_DESCRIPTION varchar(300),  
@WATERBACKUP_SUMPPUMP_LOSS INT = NULL,  --Added by Charles on 1-Dec-09 for Itrack 6647     
@WEATHER_RELATED_LOSS INT = NULL --Added for Itrack 6640 on 9 Dec 09         
)            
AS            
BEGIN            
Update  CLM_OCCURRENCE_DETAIL            
set            
LOSS_DESCRIPTION = @LOSS_DESCRIPTION,            
AUTHORITY_CONTACTED  = @AUTHORITY_CONTACTED,            
REPORT_NUMBER = @REPORT_NUMBER,            
VIOLATIONS  = @VIOLATIONS,            
MODIFIED_BY = @MODIFIED_BY,            
LAST_UPDATED_DATETIME  = GETDATE(),              
LOSS_TYPE = @LOSS_TYPE,            
LOSS_LOCATION = @LOSS_LOCATION, 
LOSS_LOCATION_ZIP=@LOSS_LOCATION_ZIP,       
LOSS_LOCATION_CITY=@LOSS_LOCATION_CITY,
LOSS_LOCATION_STATE=@LOSS_LOCATION_STATE,
ESTIMATE_AMOUNT = @ESTIMATE_AMOUNT,      
OTHER_DESCRIPTION = @OTHER_DESCRIPTION,  
WATERBACKUP_SUMPPUMP_LOSS = @WATERBACKUP_SUMPPUMP_LOSS ,  --Added by Charles on 1-Dec-09 for Itrack 6647   
WEATHER_RELATED_LOSS = @WEATHER_RELATED_LOSS   --Added for Itrack 6640 on 9 Dec 09        
where  OCCURRENCE_DETAIL_ID = @OCCURRENCE_DETAIL_ID AND CLAIM_ID = @CLAIM_ID            
END        
  

GO

