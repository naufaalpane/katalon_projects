execute @RetVal = [NAMA_SP_BATCHNYA]
		@@v_err_mesg			= @ErrMesg output,
	@@ri_v_process_code		= @PROCESS_CD,
	@@ri_v_date				= @DDMMYYYY,
	@@@ri_v_company_code	= @COMPANY_CD,
	@@ri_v_line_cd			= @LINE_CD
	