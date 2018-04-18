IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertDistributionDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertDistributionDetail]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name          : Dbo.Proc_InsertDistributionDetail    
Created by           : Mohit Gupta    
Date                    :     
Purpose               :     
Revison History :    
Used In                :   Wolverine      
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
-- DROP PROC dbo.Proc_InsertDistributionDetail 
CREATE   PROCEDURE Proc_InsertDistributionDetail    
(    
       @GROUP_ID int,    
       @GROUP_TYPE varchar(5),    
       @ACCOUNT_ID int,    
       @DISTRIBUTION_PERCT decimal(18,2),    
       @DISTRIBUTION_AMOUNT decimal(18,2) ,    
       @NOTE nvarchar(255),    
       @CREATED_BY int    
)    
AS    
BEGIN    
INSERT INTO ACT_DISTRIBUTION_DETAILS    
(    
GROUP_ID,    
GROUP_TYPE,    
ACCOUNT_ID,    
DISTRIBUTION_PERCT,    
DISTRIBUTION_AMOUNT,    
NOTE,    
IS_ACTIVE,    
CREATED_BY,    
CREATED_DATETIME,
MODIFIED_BY   
)    
VALUES    
(    
@GROUP_ID,    
@GROUP_TYPE,    
@ACCOUNT_ID,    
@DISTRIBUTION_PERCT,    
@DISTRIBUTION_AMOUNT,    
@NOTE,    
'Y',    
@CREATED_BY,    
GETDATE(),  
@CREATED_BY 
)    
END    




GO

