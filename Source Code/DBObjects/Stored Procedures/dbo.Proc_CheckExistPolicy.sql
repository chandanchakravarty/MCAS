IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckExistPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckExistPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name               : Dbo.Proc_CheckExistPolicy      
Created by              : Shafi    
Date                    : 16/03/2006    
Purpose                 :       
Revison History :      
Used In                :   Wolverine        
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
--exec Proc_CheckExistPolicy @CUSTOMERID = 837    
--DROP PROC Proc_CheckExistPolicy    
CREATE PROCEDURE Proc_CheckExistPolicy    
(      
 @CUSTOMERID INT      
)      
AS      
BEGIN      
 DECLARE @RETVALUE INT    
 SET @RETVALUE=0    
 SELECT @RETVALUE = COUNT(CUSTOMER_ID) FROM POL_CUSTOMER_POLICY_LIST WHERE  CUSTOMER_ID = @CUSTOMERID AND (POLICY_LOB=1 OR POLICY_LOB=2)      
 SELECT @RETVALUE AS RETVALUE    
     
END      
      
    
  



GO

