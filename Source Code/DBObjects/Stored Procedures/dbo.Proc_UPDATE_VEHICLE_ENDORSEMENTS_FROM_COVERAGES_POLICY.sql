IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_VEHICLE_ENDORSEMENTS_FROM_COVERAGES_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_VEHICLE_ENDORSEMENTS_FROM_COVERAGES_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc Proc_UPDATE_VEHICLE_ENDORSEMENTS_FROM_COVERAGES_POLICY    
    
/*----------------------------------------------------------                      
Proc Name   : dbo.Proc_Get_UPDATE_VEHICLE_ENDORSEMENTS_FROM_COVERAGES            
Created by  : Pradeep            
Date        : mar 16, 2006    
Purpose     : Updates relevant endorsement s based on coverages        
  
modified by  :Pravesh K Chandel  
Modified Date : 16 April 09  
Purpose   : update Condition to hv a9 endorsement  
Revison History  :                            
  
Modified Date : 7 July 09  
Purpose   :add Condition of M-17 for MotorCycle to hv a9 endorsement  
  
 ------------------------------------------------------------                                  
Date     Review By          Comments                                
                       
------   ------------       -------------------------*/                      
CREATE PROCEDURE dbo.Proc_UPDATE_VEHICLE_ENDORSEMENTS_FROM_COVERAGES_POLICY        
(            
  @CUSTOMER_ID int,            
  @POLICY_ID int,            
  @POLICY_VERSION_ID int,    
  @VEHICLE_ID smallint      
            
)                
AS                     
BEGIN                      
           
 DECLARE @STATE_ID Int    
 DECLARE @LOB_ID Int    
     
 SELECT @STATE_ID = STATE_ID,    
        @LOB_ID = POLICY_LOB    
 FROM POL_CUSTOMER_POLICY_LIST    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
 POLICY_ID = @POLICY_ID AND    
 POLICY_VERSION_ID = @POLICY_VERSION_ID    
    
/*    
12  PUMSP Uninsured Motorists (BI Split Limit)    
34  UNDSP Underinsured Motorists (BI Split Limit) 14 2    
9  PUNCS Uninsured Motorists (CSL) 14 2    
14  UNCSL Underinsured Motorists (CSL) 14    
1  SLL Single Limits Liability CSL (BI and PD) 14    
2  BISPL Bodily Injury Liability ( Split Limit) 14    
    
--Motor    
126  RLCSL Single Limits Liability (CSL) 14 3    
127  BISPL Bodily Injury Liability (Split Limit) 14 3    
131  PUNCS Uninsured Motorists (CSL) 14 3    
133  UNCSL Underinsured Motorists (CSL) (M-16) 14    
132  PUMSP Uninsured Motorists (BI Split Limit)    
214  UNDSP Underinsured Motorists (BI Split Limit) (M-16) 14 3    
*/    
 IF ( @STATE_ID = 14 )    
 BEGIN    
  DECLARE @CSL_OR_BISPL Decimal(18,0)    
  DECLARE @CSL_OR_BISPL_ID Int    
     
  DECLARE @UNINSURED_BI Decimal(18,0)    
  DECLARE @UNINSURED_BI_TEXT VarChar(10)    
      
  DECLARE @UNDERINSURED_BI Decimal(18,0)    
  DECLARE @UNDERINSURED_BI_TEXT VarChar(10)    
     
  DECLARE @UNINSURED_CSL Decimal(18,0)    
  DECLARE @UNINSURED_CSL_TEXT VarChar(10)    
     
  DECLARE @UNDERINSURED_CSL Decimal(18,0)    
  DECLARE @UNDERINSURED_CSL_TEXT VarChar(10)    
   
  DECLARE @UNINSURED_MOTORIST Decimal(18,0)    
  DECLARE @UNINSURED_MOTORIST_TEXT VarChar(10)    
    
  DECLARE @REJECT_OR_REDUCED VarChar(1)    
  DECLARE @A9 Int    
  DECLARE @IS_A9_EXISTS CHAR(1)      
  SET  @IS_A9_EXISTS='N'  
  
  SET @REJECT_OR_REDUCED = 'N'     
              
  IF ( @LOB_ID = 2 )    
  BEGIN    
   SET @A9 = 15    
  END    
      
  IF ( @LOB_ID = 3 )    
  BEGIN    
   SET @A9 = 47     
  END    
    
  --Get the amount of CSL or Bodily Injury liability    
  SELECT  @CSL_OR_BISPL = LIMIT_1,    
   @CSL_OR_BISPL_ID = COVERAGE_CODE_ID    
  FROM POL_VEHICLE_COVERAGES  with(nolock)  
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
  POLICY_ID = @POLICY_ID AND    
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
  VEHICLE_ID = @VEHICLE_ID AND    
  COVERAGE_CODE_ID IN (1,2, 126, 127)    
    
if exists(SELECT  coverage_code_id  
  FROM POL_VEHICLE_COVERAGES  with(nolock)  
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
  POLICY_ID = @POLICY_ID AND    
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
  VEHICLE_ID = @VEHICLE_ID AND    
  COVERAGE_CODE_ID IN (1007,1022)    
   )  
    SET  @IS_A9_EXISTS='Y'  
  
  
 IF ( @CSL_OR_BISPL IS NULL )    
 BEGIN  
  RETURN    
 END    
     
 IF NOT EXISTS    
 (    
  SELECT  *    
  FROM POL_VEHICLE_COVERAGES    
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
  POLICY_ID = @POLICY_ID AND    
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
  VEHICLE_ID = @VEHICLE_ID AND    
  COVERAGE_CODE_ID IN (12,132,9,131,199)     
 )    
 BEGIN    
   EXEC Proc_Delete_POL_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES            
     @CUSTOMER_ID,--@CUSTOMER_ID int,      
     @POLICY_ID,--@POLICY_ID int,            
     @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,             
     @A9,--@ENDORSEMENT_ID smallint,        
        @VEHICLE_ID   
   
       
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR ('Unable to delete A-9 endorsement.', 16, 1)    
   END    
   RETURN    
 END    
    
 --Bodily Injury Liability ( Split Limit) ----------------------    
 IF ( @CSL_OR_BISPL_ID IN (2, 127) )    
 BEGIN    
   --Get the amount and text of Uninsured Motorists (BI Split Limit)    
   SELECT  @UNINSURED_BI = LIMIT_1,    
    @UNINSURED_BI_TEXT = LIMIT1_AMOUNT_TEXT    
   FROM POL_VEHICLE_COVERAGES    
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
   POLICY_ID = @POLICY_ID AND    
   POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
   VEHICLE_ID = @VEHICLE_ID AND    
   COVERAGE_CODE_ID IN (12,132)    
      
   --Get the amount and text of Underinsured Motorists (BI Split Limit)       
   SELECT  @UNDERINSURED_BI = LIMIT_1,    
    @UNDERINSURED_BI_TEXT = LIMIT1_AMOUNT_TEXT    
   FROM POL_VEHICLE_COVERAGES    
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
   POLICY_ID = @POLICY_ID AND    
   POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
   VEHICLE_ID = @VEHICLE_ID AND    
   COVERAGE_CODE_ID IN (34,214)    
      
   --Check for reduced limits    
   IF ( ISNULL(@UNINSURED_BI,0) < ISNULL(@CSL_OR_BISPL,0)  
   OR  ISNULL(@UNDERINSURED_BI,0) < ISNULL(@UNINSURED_BI,0)   -- ADED BY pRAVESH ON 16 APRIL 09  
   )   
   BEGIN    
    SET @REJECT_OR_REDUCED = 'Y'    
   END    
       
   --Check for reject in Uninsured and Underinsured    
   IF ( ISNULL(@UNINSURED_BI_TEXT,'') = 'Reject' AND     
    ISNULL(@UNDERINSURED_BI_TEXT,'') = 'Reject' )    
   BEGIN    
    SET @REJECT_OR_REDUCED = 'Y'    
   END     
    
 END    
 --End of BISPL--------------------------------------------------------    
     
    
 --CSL----------------------------------    
 IF ( @CSL_OR_BISPL_ID IN (1,126) )    
 BEGIN    
  --Get the amount and text of Uninsured Motorists (CSL)     
   SELECT  @UNINSURED_CSL = LIMIT_1,    
    @UNINSURED_CSL_TEXT = LIMIT1_AMOUNT_TEXT    
   FROM POL_VEHICLE_COVERAGES    
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
   POLICY_ID = @POLICY_ID AND    
   POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
   VEHICLE_ID = @VEHICLE_ID AND    
   COVERAGE_CODE_ID IN (9,131)    
      
   --Get the amount and text of Underinsured Motorists (CSL)       
   SELECT  @UNDERINSURED_CSL = LIMIT_1,    
    @UNDERINSURED_CSL_TEXT = LIMIT1_AMOUNT_TEXT    
   FROM POL_VEHICLE_COVERAGES    
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
   POLICY_ID = @POLICY_ID AND    
   POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
   VEHICLE_ID = @VEHICLE_ID AND    
   COVERAGE_CODE_ID IN (14,133)    
      
   --Check for reduced limits    
   IF ( ISNULL(@UNINSURED_CSL,0) < ISNULL(@CSL_OR_BISPL,0)   
  OR ISNULL(@UNDERINSURED_CSL,0) < ISNULL(@UNINSURED_CSL,0)  -- ADED BY pRAVESH ON 16 APRIL 09  
  )    
   BEGIN    
    SET @REJECT_OR_REDUCED = 'Y'    
   END    
        
   --Check for reject in Uninsured and Underinsured    
   IF ( ISNULL(@UNINSURED_CSL_TEXT,'') = 'Reject' AND     
    ISNULL(@UNDERINSURED_CSL_TEXT,'') = 'Reject' )    
   BEGIN    
    SET @REJECT_OR_REDUCED = 'Y'    
   END     
     
 END    
 --End of CSL--------------------------------------------------------    
--Underinsured Motorist M-17 --------------------------------    
 IF exists(SELECT   COVERAGE_CODE_ID    
      FROM POL_VEHICLE_COVERAGES    
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
  POLICY_ID = @POLICY_ID AND    
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
  VEHICLE_ID = @VEHICLE_ID AND    
  COVERAGE_CODE_ID IN (199)    
  )    
 BEGIN    
  --Get the amount and text of Underinsured Motorist M-17  
   SELECT  @UNINSURED_MOTORIST = LIMIT_1,    
    @UNINSURED_MOTORIST_TEXT = LIMIT1_AMOUNT_TEXT    
   FROM POL_VEHICLE_COVERAGES        WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
   POLICY_ID = @POLICY_ID AND    
   POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
   VEHICLE_ID = @VEHICLE_ID AND    
   COVERAGE_CODE_ID IN (199)    
  
   --Check for reject in Underinsured Motorist M-17  
   IF ( ISNULL(@UNINSURED_MOTORIST_TEXT,'') = 'Reject')    
   BEGIN    
    SET @REJECT_OR_REDUCED = 'Y'    
   END     
     
 END    
 --End of Underinsured Motorist M-17 --------------------------------------------------------    
      
     
 IF ( @REJECT_OR_REDUCED = 'Y' AND @IS_A9_EXISTS='Y' )    
 BEGIN    
   EXEC Proc_Insert_POL_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES                        
     @CUSTOMER_ID,--@CUSTOMER_ID int,              
     @POLICY_ID,--@POLICY_ID int,              
     @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,               
     @A9,--@ENDORSEMENT_ID smallint,          
  @VEHICLE_ID  
       
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR ('Unable to update A-9 endorsement.', 16, 1)    
   END    
              
 END    
 ELSE    
  BEGIN    
   EXEC Proc_Delete_POL_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES              
     @CUSTOMER_ID,--@CUSTOMER_ID int,            
     @POLICY_ID,--@POLICY_ID int,            
     @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,             
     @A9,--@ENDORSEMENT_ID smallint,        
  @VEHICLE_ID  
       
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR ('Unable to delete A-9 endorsement.', 16, 1)    
   END    
 END    
 --added by pravesh on 28 march 2007 for endorsement M-17  
IF(@LOB_ID=3)  
 BEGIN   
    set  @UNDERINSURED_CSL=null  
    set @UNDERINSURED_CSL_TEXT =null  
  
 SELECT  @UNDERINSURED_CSL = LIMIT_1,    
  @UNDERINSURED_CSL_TEXT = ISNULL(LIMIT1_AMOUNT_TEXT,'0')  
  FROM POL_VEHICLE_COVERAGES    
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
  POLICY_ID = @POLICY_ID AND    
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
  VEHICLE_ID = @VEHICLE_ID AND    
  COVERAGE_CODE_ID in (133) --(133,214) Commented by Pravesh on 7 july as now we have two diffrent endorsement for these covg  
  
  print @UNDERINSURED_CSL_TEXT  
    IF(@UNDERINSURED_CSL_TEXT !='Reject')  
         BEGIN  
      
   
   EXEC Proc_Insert_POL_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES                        
       @CUSTOMER_ID,--@CUSTOMER_ID int,              
       @POLICY_ID,--@POLICY_ID int,              
       @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,               
      366,--@ENDORSEMENT_ID smallint,     
     @VEHICLE_ID  
    IF @@ERROR <> 0     
    BEGIN    
     RAISERROR ('Unable to update Endorsement M-16.', 16, 1)    
    END  
  END    
    ELSE    
 BEGIN    
    EXEC Proc_Delete_POL_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES              
       @CUSTOMER_ID,--@CUSTOMER_ID int,            
       @POLICY_ID,--@POLICY_ID int,            
       @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,             
              366,--@ENDORSEMENT_ID smallint,        
     @VEHICLE_ID  
      
    IF @@ERROR <> 0     
    BEGIN    
     RAISERROR ('Unable to delete Endorsement M-16.', 16, 1)    
    END    
 END    
-- added by Pravesh on 7 july 09 for BI m-16   
 set  @UNDERINSURED_CSL=null  
    set @UNDERINSURED_CSL_TEXT =null  
  
 SELECT  @UNDERINSURED_CSL = LIMIT_1,    
  @UNDERINSURED_CSL_TEXT = ISNULL(LIMIT1_AMOUNT_TEXT,'0')  
  FROM POL_VEHICLE_COVERAGES    
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
  POLICY_ID = @POLICY_ID AND    
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
  VEHICLE_ID = @VEHICLE_ID AND    
  COVERAGE_CODE_ID in (214)   
  
  print @UNDERINSURED_CSL_TEXT  
    IF(@UNDERINSURED_CSL_TEXT !='Reject')  
         BEGIN  
      
   
   EXEC Proc_Insert_POL_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES                        
       @CUSTOMER_ID,--@CUSTOMER_ID int,              
       @POLICY_ID,--@POLICY_ID int,              
       @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,               
      420,--@ENDORSEMENT_ID smallint,     
     @VEHICLE_ID  
    IF @@ERROR <> 0     
    BEGIN    
     RAISERROR ('Unable to update Endorsement M-16.', 16, 1)    
    END  
  END    
    ELSE    
 BEGIN    
    EXEC Proc_Delete_POL_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES        
       @CUSTOMER_ID,--@CUSTOMER_ID int,            
       @POLICY_ID,--@POLICY_ID int,            
       @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,             
              420,--@ENDORSEMENT_ID smallint,        
     @VEHICLE_ID  
    IF @@ERROR <> 0     
    BEGIN    
     RAISERROR ('Unable to delete Endorsement M-16.', 16, 1)    
    END    
 END    
--end here  
set @UNDERINSURED_CSL=null  
set @UNDERINSURED_CSL_TEXT=null  
SELECT  @UNDERINSURED_CSL = LIMIT_1,    
  @UNDERINSURED_CSL_TEXT = ISNULL(LIMIT1_AMOUNT_TEXT,'0')  
  FROM POL_VEHICLE_COVERAGES    
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
  POLICY_ID = @POLICY_ID AND    
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
  VEHICLE_ID = @VEHICLE_ID AND    
  COVERAGE_CODE_ID in (199,1021)  
  
  print @UNDERINSURED_CSL_TEXT  
    IF(@UNDERINSURED_CSL_TEXT !='Reject')  
         BEGIN  
      
   EXEC Proc_Insert_POL_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES                        
       @CUSTOMER_ID,--@CUSTOMER_ID int,              
       @POLICY_ID,--@POLICY_ID int,              
       @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,               
    359,--@ENDORSEMENT_ID smallint,       
    @VEHICLE_ID  
    IF @@ERROR <> 0     
    BEGIN    
     RAISERROR ('Unable to update Endorsement M-17.', 16, 1)    
    END  
  END    
    ELSE    
 BEGIN    
     EXEC Proc_Delete_POL_VEHICLE_ENDORSEMENT_BY_ID_ALL_VEHICLES              
        @CUSTOMER_ID,--@CUSTOMER_ID int,            
        @POLICY_ID,--@POLICY_ID int,            
        @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,             
             359,--@ENDORSEMENT_ID smallint,        
     @VEHICLE_ID  
      
    IF @@ERROR <> 0     
    BEGIN    
     RAISERROR ('Unable to delete Endorsement M-17.', 16, 1)    
    END    
 END    
 END  
-----end here   
 END  --end of stateid=14  
  
  
  
          
          
END            
            
            
          
  
  
  
  
  
  
  
  
  
  
GO

