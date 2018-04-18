IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetSelectedOtherPolicyInformationForUMB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetSelectedOtherPolicyInformationForUMB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                                      
Proc Name           : Proc_GetSelectedOtherPolicyInformationForUMB                                                                            
Created by          : Sumit Chhabra                                                                                
Date                : 04-05-2007                                                                                      
Purpose             : To get policies selected at Schedule of underlying                  
Revison History     :                                                                                      
Used In             : Wolverine                                                                                      
------------------------------------------------------------                                                                                      
Date     Review By          Comments                                                                                      
------   ------------       -------------------------*/                                
                                   
--drop PROC dbo.Proc_GetSelectedOtherPolicyInformationForUMB 1199,232,1,'rec_veh',''      
CREATE     PROC dbo.Proc_GetSelectedOtherPolicyInformationForUMB                
 (                      
  @CUSTOMER_ID       INT,                                                                                      
  @APP_ID           INT,                                                                                      
  @APP_VERSION_ID smallint,            
  @CALLEDFROM varchar (30) = null,            
  @LOB int = null                 
 )                      
                      
AS                      
BEGIN                      
                
            
IF (@CALLEDFROM = 'VEHICLES')--AND ((@LOB=11956) OR (@LOB=11957))            
            
BEGIN            
            
  DECLARE @TEMPLOB VARCHAR(10)            
  IF(@LOB=11956)            
   SET @TEMPLOB=2            
  IF (@LOB =11957)            
   SET @TEMPLOB=3            
  IF(@LOB =11958)            
   SET @TEMPLOB=2           
  IF(@LOB =11959)            
   SET @TEMPLOB = null            
           
  IF (@TEMPLOB is not null)           
  BEGIN          
           
    SELECT POLICY_NUMBER, POLICY_LOB, POLICY_COMPANY, CONVERT(VARCHAR(20),IS_POLICY) AS IS_POLICY,                  
      (LOB_CODE + '/' + POLICY_NUMBER) AS POLICY_NUMBER_LOB                  
      FROM APP_UMBRELLA_UNDERLYING_POLICIES POLICY WITH (NOLOCK)                  
     LEFT OUTER JOIN MNT_LOB_MASTER LOB ON                  
      POLICY.POLICY_LOB = LOB.LOB_ID                  
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                
      AND UPPER(POLICY_COMPANY)<>'WOLVERINE' AND LOB_ID  = @TEMPLOB           
     ORDER BY POLICY_LOB DESC             
  END            
  ELSE            
  BEGIN            
    SELECT POLICY_NUMBER, POLICY_LOB, POLICY_COMPANY, CONVERT(VARCHAR(20),IS_POLICY) AS IS_POLICY,                  
      (LOB_CODE + '/' + POLICY_NUMBER) AS POLICY_NUMBER_LOB                  
      FROM APP_UMBRELLA_UNDERLYING_POLICIES POLICY WITH (NOLOCK)                  
     LEFT OUTER JOIN MNT_LOB_MASTER LOB ON                  
     POLICY.POLICY_LOB = LOB.LOB_ID                  
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                
      AND UPPER(POLICY_COMPANY)<>'WOLVERINE' --AND LOB_ID IN (1,6)                
     ORDER BY POLICY_LOB DESC              
  END            
        
END      
ELSE           
IF (@CALLEDFROM = 'BOAT')          
BEGIN          
SELECT POLICY_NUMBER, POLICY_LOB, POLICY_COMPANY, CONVERT(VARCHAR(20),IS_POLICY) AS IS_POLICY,                  
    (LOB_CODE + '/' + POLICY_NUMBER) AS POLICY_NUMBER_LOB                  
    FROM APP_UMBRELLA_UNDERLYING_POLICIES POLICY WITH (NOLOCK)                  
   LEFT OUTER JOIN MNT_LOB_MASTER LOB ON                  
    POLICY.POLICY_LOB = LOB.LOB_ID                  
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID         
    AND UPPER(POLICY_COMPANY)<>'WOLVERINE' AND LOB_ID  IN (1,4)          
   ORDER BY POLICY_LOB DESC              
END   
ELSE   
IF (@CALLEDFROM = 'REC_VEH'  )  
BEGIN    
SELECT POLICY_NUMBER, POLICY_LOB, POLICY_COMPANY, CONVERT(VARCHAR(20),IS_POLICY) AS IS_POLICY,                  
      (LOB_CODE + '/' + POLICY_NUMBER) AS POLICY_NUMBER_LOB                  
      FROM APP_UMBRELLA_UNDERLYING_POLICIES POLICY WITH (NOLOCK)                  
     LEFT OUTER JOIN MNT_LOB_MASTER LOB ON                  
     POLICY.POLICY_LOB = LOB.LOB_ID                  
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                
      AND UPPER(POLICY_COMPANY)<>'WOLVERINE' --AND LOB_ID IN (1,6)                
     ORDER BY POLICY_LOB DESC    
END   
  
ELSE            
  BEGIN            
    SELECT POLICY_NUMBER, POLICY_LOB, POLICY_COMPANY, CONVERT(VARCHAR(20),IS_POLICY) AS IS_POLICY,                  
      (LOB_CODE + '/' + POLICY_NUMBER) AS POLICY_NUMBER_LOB                  
      FROM APP_UMBRELLA_UNDERLYING_POLICIES POLICY WITH (NOLOCK)                  
     LEFT OUTER JOIN MNT_LOB_MASTER LOB ON                  
     POLICY.POLICY_LOB = LOB.LOB_ID                  
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                
      AND UPPER(POLICY_COMPANY)<>'WOLVERINE' AND LOB_ID IN (1,6)                
     ORDER BY POLICY_LOB DESC              
  END    
       
END             
        
  



GO

