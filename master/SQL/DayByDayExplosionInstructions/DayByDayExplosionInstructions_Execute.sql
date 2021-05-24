execute @RetVal = [sp_DayByDayExplosion]
	@@v_err_mesg			= @ErrMesg output,
	@@ri_v_process_code		= @PROCESS_CD,
	@@ri_v_start_date		= @START_DATE,
	@@@ri_v_company_code	= @COMPANY_CD,
	@@ri_v_rcv_no			= @RECEIVE_NO,
	@@ri_v_user_id			= @userId