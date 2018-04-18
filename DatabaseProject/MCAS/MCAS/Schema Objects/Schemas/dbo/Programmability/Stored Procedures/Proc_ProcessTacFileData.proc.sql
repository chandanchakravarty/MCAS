CREATE PROCEDURE [dbo].[Proc_ProcessTacFileData]
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON      
   DECLARE  @Id int      
   DECLARE  @IpNumber bigint      
   DECLARE  @AccidentDateTime datetime      
   DECLARE  @FinalLiabilityDate datetime      
   DECLARE  @FinalFinding int      
   DECLARE  @PlaceOfAccident varchar(max)        
   DECLARE  @OperatingHours int      
   DECLARE  @DamageToBus varchar(max)       
   DECLARE  @FactsOfIncident varchar(max)      
   DECLARE  @ReportNo varchar(50)       
   DECLARE  @ReportDate datetime       
   DECLARE  @BusNumber varchar(50)      
   DECLARE  @ServiceNo varchar(20)      
   DECLARE  @FinalLiability varchar(50)      
   DECLARE  @BOIResults varchar(max)        
   DECLARE  @StaffNo varchar(10)      
   DECLARE  @FileId int      
   ----- IsProcessed =1 'success', IsProcessed =0 'fail'      
   ----- PROCESS FOR CUR_TAC_IP Table      
  DECLARE CUR_TAC_IP CURSOR FOR      
  SELECT Id,IpNumber,AccidentDateTime,FinalLiabilityDate,FinalFinding,PlaceOfAccident,OperatingHours,DamageToBus,FactsOfIncident,ReportNo,FileId      
        FROM STG_TAC_IP       
        WHERE  IsProcessed=0      
    OPEN CUR_TAC_IP      
       --IF @@CURSOR_ROWS > 0      
   BEGIN       
     FETCH NEXT FROM CUR_TAC_IP INTO  @Id,@IpNumber,@AccidentDateTime,@FinalLiabilityDate,@FinalFinding,@PlaceOfAccident,@OperatingHours,@DamageToBus,@FactsOfIncident,@ReportNo,@FileId      
       WHILE @@Fetch_status = 0      
         BEGIN      
                 
           IF NOT EXISTS( SELECT IpNumber FROM STG_TEMP_UploadedFileData WHERE IpNumber=@IpNumber)      
             BEGIN TRY      
                 BEGIN      
                     INSERT INTO STG_TEMP_UploadedFileData(IpNumber,AccidentDateTime,FinalLiabilityDate,FinalFinding,PlaceOfAccident,OperatingHours,DamageToBus,FactsOfIncident,ReportNo,FileId,Status,IsProcessed)      
                     VALUES(@IpNumber,@AccidentDateTime,@FinalLiabilityDate,@FinalFinding,@PlaceOfAccident,@OperatingHours,@DamageToBus,@FactsOfIncident,@ReportNo,@FileId,'Pass',1)      
                     --IF(@IpNumber IS NULL)    
                     -- BEGIN    
                     --   UPDATE STG_TAC_IP SET IsProcessed=0 WHERE ID=@Id      
                     -- END    
                     --ELSE    
                     -- BEGIN     
                        UPDATE STG_TAC_IP SET IsProcessed=1 WHERE ID=@Id      
                      --END      
                         
                 END      
             END TRY      
             BEGIN CATCH      
                     INSERT INTO STG_TEMP_UploadedFileData(IpNumber,AccidentDateTime,FinalLiabilityDate,FinalFinding,PlaceOfAccident,OperatingHours,DamageToBus,FactsOfIncident,ReportNo,FileId,Status,ExceptionDetails,IsProcessed)      
                     VALUES(@IpNumber,@AccidentDateTime,@FinalLiabilityDate,@FinalFinding,@PlaceOfAccident,@OperatingHours,@DamageToBus,@FactsOfIncident,@ReportNo,@FileId,'Fail',ERROR_MESSAGE(),0)      
                       
                     UPDATE STG_TAC_IP SET IsProcessed=0 WHERE ID=@Id      
             END CATCH      
           ELSE      
                
             BEGIN TRY      
                BEGIN      
                     UPDATE STG_TEMP_UploadedFileData SET IpNumber=@IpNumber,AccidentDateTime=@AccidentDateTime,      
                                                          FinalLiabilityDate=@FinalLiabilityDate,      
                                                          FinalFinding=@FinalFinding,PlaceOfAccident=@PlaceOfAccident,      
                                                          OperatingHours=@OperatingHours,DamageToBus=@DamageToBus,      
                                                          FactsOfIncident=@FactsOfIncident,ReportNo=@ReportNo,      
                                                          FileId=@FileId,      
                                                          Status='Pass',      
                                                          IsProcessed=1      
                                                 WHERE    IpNumber=@IpNumber      
                                                       
                     --IF(@IpNumber IS NULL)    
                     -- BEGIN    
                     --   UPDATE STG_TAC_IP SET IsProcessed=0 WHERE ID=@Id      
                     -- END    
                     --ELSE    
                     -- BEGIN     
                        UPDATE STG_TAC_IP SET IsProcessed=1 WHERE ID=@Id      
                      --END     
                END      
             END TRY      
                   
             BEGIN CATCH      
                   
                      UPDATE STG_TEMP_UploadedFileData SET IpNumber=@IpNumber,AccidentDateTime=@AccidentDateTime,      
                                                          FinalLiabilityDate=@FinalLiabilityDate,      
                                                          FinalFinding=@FinalFinding,PlaceOfAccident=@PlaceOfAccident,      
                                                          OperatingHours=@OperatingHours,DamageToBus=@DamageToBus,      
                                                          FactsOfIncident=@FactsOfIncident,ReportNo=@ReportNo,      
                                                          FileId=@FileId,      
                                                          Status='Fail',      
                                                          ExceptionDetails=ERROR_MESSAGE(),      
                                                          IsProcessed=0      
                                                 WHERE    IpNumber=@IpNumber      
                   
                     UPDATE STG_TAC_IP SET IsProcessed=0 WHERE ID=@Id      
             END CATCH      
      
     FETCH NEXT FROM CUR_TAC_IP INTO @Id,@IpNumber,@AccidentDateTime,@FinalLiabilityDate,@FinalFinding,@PlaceOfAccident,@OperatingHours,@DamageToBus,@FactsOfIncident,@ReportNo,@FileId      
         END      
   END      
    CLOSE CUR_TAC_IP      
  DEALLOCATE CUR_TAC_IP      
      
----- PROCESS FOR STG_TAC_ACC_REP Table      
      
  DECLARE CUR_TAC_ACC_REP CURSOR FOR      
  SELECT Id,IpNumber,ReportDate,AccidentDateTime,PlaceOfAccident,OperatingHours,DamageToBus,FactsOfIncident,FileId      
         FROM STG_TAC_ACC_REP       
         WHERE  IsProcessed=0      
    OPEN CUR_TAC_ACC_REP      
      --IF @@CURSOR_ROWS > 0      
   BEGIN       
     FETCH NEXT FROM CUR_TAC_ACC_REP INTO  @Id ,@IpNumber,@ReportDate ,@AccidentDateTime ,@PlaceOfAccident ,@OperatingHours ,@DamageToBus ,@FactsOfIncident,@FileId      
       WHILE @@Fetch_status = 0      
         BEGIN      
                 
           IF NOT EXISTS( SELECT IpNumber FROM STG_TEMP_UploadedFileData WHERE IpNumber=@IpNumber)      
             BEGIN TRY      
                BEGIN      
                     INSERT INTO STG_TEMP_UploadedFileData(IpNumber,ReportDate,AccidentDateTime,PlaceOfAccident,OperatingHours,DamageToBus,FactsOfIncident,FileId,Status,IsProcessed)      
                     VALUES(@IpNumber,@ReportDate ,@AccidentDateTime ,@PlaceOfAccident ,@OperatingHours ,@DamageToBus ,@FactsOfIncident,@FileId,'Pass',1)      
                          
    --                 IF(@IpNumber IS NULL)    
    --                   BEGIN    
    --UPDATE STG_TAC_ACC_REP SET IsProcessed=0 WHERE ID=@Id      
    --                   END    
    --                  ELSE    
    --                   BEGIN     
                         UPDATE STG_TAC_ACC_REP SET IsProcessed=1 WHERE ID=@Id      
                       --END      
                 END      
             END TRY      
             BEGIN CATCH      
                   
                    INSERT INTO STG_TEMP_UploadedFileData(IpNumber,ReportDate,AccidentDateTime,PlaceOfAccident,OperatingHours,DamageToBus,FactsOfIncident,FileId,Status,ExceptionDetails,IsProcessed)      
                    VALUES(@IpNumber,@ReportDate ,@AccidentDateTime ,@PlaceOfAccident ,@OperatingHours ,@DamageToBus ,@FactsOfIncident,@FileId,'Fail',ERROR_MESSAGE(),0)      
                           
                    UPDATE STG_TAC_ACC_REP SET IsProcessed=0 WHERE ID=@Id      
      
             END CATCH      
           ELSE      
             BEGIN TRY      
                BEGIN      
                     UPDATE STG_TEMP_UploadedFileData SET IpNumber=@IpNumber,ReportDate=@ReportDate,      
                            AccidentDateTime=@AccidentDateTime,      
                                                       PlaceOfAccident=@PlaceOfAccident,OperatingHours=@OperatingHours,      
                                                       DamageToBus=@DamageToBus,FactsOfIncident=@FactsOfIncident,      
                                                       FileId=@FileId,      
                                                       Status='Pass',      
                                                       IsProcessed=1      
                     WHERE IpNumber=@IpNumber      
                          
                     --IF(@IpNumber IS NULL)    
                     --   BEGIN    
                     --     UPDATE STG_TAC_ACC_REP SET IsProcessed=0 WHERE ID=@Id      
                     --   END    
                     --ELSE    
                     --   BEGIN     
                          UPDATE STG_TAC_ACC_REP SET IsProcessed=1 WHERE ID=@Id      
                        --END     
                END      
             END TRY      
             BEGIN CATCH      
                     UPDATE STG_TEMP_UploadedFileData SET IpNumber=@IpNumber,ReportDate=@ReportDate,      
                                                       AccidentDateTime=@AccidentDateTime,      
                                                       PlaceOfAccident=@PlaceOfAccident,OperatingHours=@OperatingHours,      
                                                       DamageToBus=@DamageToBus,FactsOfIncident=@FactsOfIncident,      
                                                       FileId=@FileId,      
                                                       Status='Fail',      
                                                       ExceptionDetails=ERROR_MESSAGE(),      
                                                       IsProcessed=0      
                                                             
                     WHERE IpNumber=@IpNumber      
                           
                     UPDATE STG_TAC_ACC_REP SET IsProcessed=0 WHERE ID=@Id      
             END CATCH      
     FETCH NEXT FROM CUR_TAC_ACC_REP INTO @Id ,@IpNumber,@ReportDate ,@AccidentDateTime ,@PlaceOfAccident ,@OperatingHours ,@DamageToBus ,@FactsOfIncident,@FileId      
         END      
   END      
   CLOSE CUR_TAC_ACC_REP      
  DEALLOCATE CUR_TAC_ACC_REP      
      
----- PROCESS FOR TAC_IP_BUS Table      
      
  DECLARE TAC_IP_BUS CURSOR FOR      
  SELECT Id,IpNumber,BusNumber,ServiceNo,FinalLiability,BOIResults,StaffNo,FileId      
         FROM STG_TAC_IP_BUS       
         WHERE  IsProcessed=0      
    OPEN TAC_IP_BUS      
      --IF @@CURSOR_ROWS > 0      
   BEGIN       
     FETCH NEXT FROM TAC_IP_BUS INTO  @Id,@IpNumber,@BusNumber,@ServiceNo,@FinalLiability,@BOIResults,@StaffNo,@FileId      
       WHILE @@Fetch_status = 0      
         BEGIN      
                 
           IF NOT EXISTS( SELECT IpNumber FROM STG_TEMP_UploadedFileData WHERE IpNumber=@IpNumber)      
             BEGIN TRY      
                BEGIN      
                     INSERT INTO STG_TEMP_UploadedFileData(IpNumber,BusNumber,ServiceNo,FinalLiability,BOIResults,StaffNo,FileId,Status,IsProcessed)      
                     VALUES(@IpNumber,@BusNumber,@ServiceNo,@FinalLiability,@BOIResults,@StaffNo,@FileId,'Pass',1)    
                           
                     --IF(@IpNumber IS NULL)    
                     --  BEGIN    
                     --    UPDATE STG_TAC_IP_BUS SET IsProcessed=0 WHERE ID=@Id      
                     --  END    
                     --ELSE    
                     --  BEGIN     
                         UPDATE STG_TAC_IP_BUS SET IsProcessed=1 WHERE ID=@Id     
                       --END      
        
                END      
             END TRY      
             BEGIN CATCH      
                     INSERT INTO STG_TEMP_UploadedFileData(IpNumber,BusNumber,ServiceNo,FinalLiability,BOIResults,StaffNo,FileId,Status,ExceptionDetails,IsProcessed)      
                     VALUES(@IpNumber,@BusNumber,@ServiceNo,@FinalLiability,@BOIResults,@StaffNo,@FileId,'Fail',ERROR_MESSAGE(),0)      
                           
                     UPDATE STG_TAC_IP_BUS SET IsProcessed=0 WHERE ID=@Id      
             END CATCH      
                   
           ELSE      
             BEGIN TRY      
                BEGIN      
                     UPDATE STG_TEMP_UploadedFileData SET BusNumber=@BusNumber,      
                                                          ServiceNo=@ServiceNo,FinalLiability=@FinalLiability,      
                                                          BOIResults=@BOIResults,StaffNo=@StaffNo,FileId=@FileId,      
                                                          Status='Pass',IsProcessed=1      
                                                   WHERE  IpNumber=@IpNumber      
                     --IF(@IpNumber IS NULL)    
                     --  BEGIN    
                     --    UPDATE STG_TAC_IP_BUS SET IsProcessed=0 WHERE ID=@Id      
                     --  END    
                     --ELSE    
                     --  BEGIN     
                         UPDATE STG_TAC_IP_BUS SET IsProcessed=1 WHERE ID=@Id     
                       --END                                         
                END      
             END TRY      
             BEGIN CATCH      
                    UPDATE STG_TEMP_UploadedFileData SET BusNumber=@BusNumber,      
                                                          ServiceNo=@ServiceNo,FinalLiability=@FinalLiability,      
                                                          BOIResults=@BOIResults,StaffNo=@StaffNo,FileId=@FileId,      
                                                          Status='Fail',ExceptionDetails=ERROR_MESSAGE(),      
                                                          IsProcessed=0      
                                                   WHERE  IpNumber=@IpNumber      
                    UPDATE STG_TAC_IP_BUS SET IsProcessed=0 WHERE ID=@Id      
             END CATCH      
      
     FETCH NEXT FROM TAC_IP_BUS INTO @Id,@IpNumber,@BusNumber,@ServiceNo,@FinalLiability,@BOIResults,@StaffNo,@FileId      
         END      
   END      
    CLOSE TAC_IP_BUS      
  DEALLOCATE TAC_IP_BUS      
SET NOCOUNT OFF


