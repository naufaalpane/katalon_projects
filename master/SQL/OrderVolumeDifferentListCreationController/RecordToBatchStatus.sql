DECLARE @@message  VARCHAR(255)

EXECUTE sp_Common_Batch_Status
	@@company_cd = @company_cd,
	@@batch_id = @batch_id,
	@@class = @class_name,
	@@batch_name = @batch_name,
	@@status = @status,
	@@start_time = @start_time,
	@@changed_dt = @changed_dt,
	@@changed_by = @changed_by,
	@@return_message = @@message OUTPUT

SELECT @@message