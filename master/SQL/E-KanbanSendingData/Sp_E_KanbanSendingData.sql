-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<WOT.Rizal>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_E_KanbanDataSending] 
	-- Add the parameters for the stored procedure here
	@ri_v_process_code char(1), -- M, K
	@ri_v_company_code varchar(4),
	@ri_v_user_id varchar(25),
	--
	@ro_i_process_status int OUTPUT,
	@ro_v_err_mesgs varchar(max) OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @l_i_process_id bigint,
			@l_i_seq_no int = 0,
			@l_v_loc varchar(100),
			@l_v_message varchar(max),
			@l_v_msg_id varchar(30),
			@l_v_function_id varchar(30) = 'U15-008',
			@l_v_function_desc varchar(max) = 'E-Kanban Data Sending',
			@query varchar(max)

	SELECT @l_v_function_desc = tmf.FUNCTION_NM FROM dbo.TB_M_FUNCTION tmf WHERE tmf.FUNCTION_ID = @l_v_function_id

	--initial process batch log
	INSERT INTO dbo.TB_R_LOG_H (MODULE_ID, FUNCTION_ID, START_DT,PROCESS_STS, USER_ID)
	VALUES ('BACTH', @l_v_function_id, getdate(),'K', @ri_v_user_id)
	
	set @l_i_process_id = @@identity
	set @l_v_msg_id = 'MSSTD001I'
	set @l_v_message = dbo.fn_GetMessage(@l_v_msg_id,@l_v_function_id,'','','')
	set @l_v_loc = 'Batch Start'
	
	INSERT INTO TB_R_LOG_D(PROCESS_ID,SEQ_NO, ERR_DT, LOC, USER_ID, MSG_ID, MSG_TYPE, MSG_TEXT)
	VALUES (@l_i_process_id, @l_i_seq_no, GETDATE(), @l_v_loc, @ri_v_user_id, @l_v_msg_id, RIGHT(@l_v_msg_id,1),@l_v_message)

	BEGIN TRY
		--getting parameter company code
		set @l_i_process_id = @@identity
		SET @l_v_msg_id = 'MSU1500801I'
		SET @l_v_message = dbo.fn_GetMessage(@l_v_msg_id,@l_v_function_id,'','','')
		set @l_v_loc = 'Got Parameter'
		INSERT INTO TB_R_LOG_D(PROCESS_ID,SEQ_NO, ERR_DT, LOC, USER_ID, MSG_ID, MSG_TYPE, MSG_TEXT)
		VALUES (@l_i_process_id, @l_i_seq_no, GETDATE(), @l_v_loc, @ri_v_user_id, @l_v_msg_id, RIGHT(@l_v_msg_id,1),@l_v_message)

		IF (@ri_v_company_code = null or @ri_v_company_code = '')
		BEGIN
			PRINT 'company cd null'
			
		END


	END TRY
	BEGIN CATCH
		/* 
			SELECT
				ERROR_NUMBER() AS ErrorNumber,
				ERROR_SEVERITY() AS ErrorSeverity,
				ERROR_STATE() AS ErrorState,
				ERROR_PROCEDURE() AS ErrorProcedure,
				ERROR_LINE() AS ErrorLine,
				ERROR_MESSAGE() AS ErrorMessage
		*/
	END CATCH
END
GO
