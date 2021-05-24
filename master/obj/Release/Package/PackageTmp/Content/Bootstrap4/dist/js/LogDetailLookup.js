

function doLookupMaterial(type, idText, idContainer) {
  $.ajax({
    type: "POST",
    url: "/LogMonitoring/LookupLogDetail",
    data: JSON.stringify({
      PROCESS_ID: idText,
      SelectionType: type,
      IdTextReturn: idText, 
    }),
    contentType: "application/json; charset=utf-8",
    success: function (resultMessage) {
      var maxperpage = $('.page-size-Material').val() || 10;
      $(idContainer).html(resultMessage);
      $('.page-size-Material').val(maxperpage);
      $.disable_enable_button.enable();

      $(idContainer).modal();

    },
    error: function (resultMessage) {
      $.disable_enable_button.enable();
    }
  })
}

