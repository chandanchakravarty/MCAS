IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DELETE_HOME_ENDORSEMENTS_FOR_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DELETE_HOME_ENDORSEMENTS_FOR_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_DELETE_HOME_ENDORSEMENTS_FOR_POLICY    
Created by      : shafi    
Date            : 20 FEB 2006   
Purpose      : Deletes records in Dwelling_Endorsements     
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE            PROC Dbo.Proc_DELETE_HOME_ENDORSEMENTS_FOR_POLICY    
(    
  @CUSTOMER_ID     int,    
  @POLICY_ID     int,    
  @POLICY_VERSION_ID     smallint,    
  @DWELLING_ID smallint,    
  @ENDORSEMENT_ID int,    
  @DWELLING_ENDORSEMENT_ID Int  
)    
AS    
    
  
  
BEGIN    
     
 IF EXISTS  
 (  
  SELECT * FROM POL_DWELLING_ENDORSEMENTS  
  WHERE CUSTOMER_ID = @CUSTOMER_ID and     
      POLICY_ID=@POLICY_ID and     
      POLICY_VERSION_ID = @POLICY_VERSION_ID     
      and DWELLING_ID = @DWELLING_ID  AND  
    ENDORSEMENT_ID = @ENDORSEMENT_ID AND  
      DWELLING_ENDORSEMENT_ID = @DWELLING_ENDORSEMENT_ID  
 )  
 BEGIN  
  DELETE FROM POL_DWELLING_ENDORSEMENTS  
  WHERE CUSTOMER_ID = @CUSTOMER_ID and     
      POLICY_ID=@POLICY_ID and     
      POLICY_VERSION_ID = @POLICY_VERSION_ID     
      and DWELLING_ID = @DWELLING_ID  AND  
    ENDORSEMENT_ID = @ENDORSEMENT_ID AND  
      DWELLING_ENDORSEMENT_ID = @DWELLING_ENDORSEMENT_ID  
 END   
   
END   
   
    
    
  
  
  
  
  
  



GO

