CREATE PROCEDURE [dbo].[Proc_Insert_STG_UploadedFileData]
WITH EXECUTE AS CALLER
AS
INSERT INTO STG_UploadedFileData (  
                                     IpNumber ,ReportDate ,AccidentDateTime ,PlaceOfAccident ,OperatingHours ,DamageToBus,FactsOfIncident ,FinalLiabilityDate ,  
                                  FinalFinding,ReportNo , BusNumber ,ServiceNo ,FinalLiability ,BOIResults ,StaffNo,FileRefId ,Status,ExceptionDetails ,  
                                  CreatedBy ,IsActive,IsProcessed  
                                )  
  SELECT                           IpNumber ,ReportDate ,AccidentDateTime ,PlaceOfAccident ,OperatingHours ,DamageToBus,FactsOfIncident, FinalLiabilityDate ,  
                                FinalFinding,ReportNo ,BusNumber,ServiceNo ,FinalLiability ,BOIResults ,StaffNo, FileId ,Status, ExceptionDetails ,  
                                'PackageUser' ,'Y',IsProcessed  
  FROM STG_TEMP_UploadedFileData  
  DELETE FROM STG_TEMP_UploadedFileData


