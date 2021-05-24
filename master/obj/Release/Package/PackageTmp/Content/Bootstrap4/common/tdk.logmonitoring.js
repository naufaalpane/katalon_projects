!function ($) {
  "use strict";

  $.log = {
    doOpenDetail: function(processId, idContainer) {
      $.ajax({
        type: "POST",
        url: "/Common/LogMonitoring/OpenDetail",
        data: JSON.stringify({
          processId: processId,
          page: 1,
          pageSize: 10,
        }),
        contentType: "application/json; charset=utf-8",
        success: function (resultMessage) {
          var maxperpage = $('.page-sizeLogDetail').val() || 10;
          $(idContainer).html(resultMessage);
          $('.page-sizeLogDetail').val(maxperpage);
          $.disable_enable_button.enable();
          $(idContainer).on('shown.bs.modal', function () {
            $.crud.doInitSelectSingle('#cmbMessageType');
            $("#cmbMessageType").focus();
          });
          $(idContainer).modal();
          
        },
        error: function (resultMessage) {
          $.disable_enable_button.enable();
        }
      })
    },

    doSearchDetail: function (page) {
      $.disable_enable_button.disable();
      $.ajax({
        type: "POST",
        url: "/Common/LogMonitoring/SearchDetail",
        data: JSON.stringify({
          processId: $('#processId').val(),
          msgType: $('#cmbMessageType').val(),
          page: page,
          pageSize: $('.page-sizeLogDetail').val() || 10
        }),
        contentType: "application/json; charset=utf-8",
        success: function (resultMessage) {
          var maxperpage = $('.page-sizeLogDetail').val() || 10;
          $('#datagridLogDetail').html(resultMessage);
          $('.page-sizeLogDetail').val(maxperpage);
          $.disable_enable_button.enable();
        },
        error: function (resultMessage) {
          $.disable_enable_button.enable();
        }
      })
    }
  }
}(window.jQuery);
