DECLARE @@tc_From VARCHAR(100),
		@@tc_To VARCHAR(100)

SET @@tc_From = (RIGHT(@v_tcFrom,4)+'/'+SUBSTRING(@v_tcFrom,3,2)+'/'+LEFT(@v_tcFrom,2))
SET @@tc_To   = (RIGHT(@v_tcTo,4)+'/'+SUBSTRING(@v_tcTo,3,2)+'/'+LEFT(@v_tcTo,2))

SELECT DISTINCT 1
FROM(
	SELECT * FROM TB_M_DESTINATION
	WHERE @@tc_From <= TC_TO AND @@tc_To >= TC_FROM
)SRC