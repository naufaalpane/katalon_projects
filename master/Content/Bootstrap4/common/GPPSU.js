!function ($) {
  "use strict";

  $.GPPSU = {
    validateMandatory: function (idList, mode) {
      var mandatoryEmptyList = [];
      var messageList = [];
      for (var i = 0; i < idList.length; i++) {
        var $this = $('#' + idList[i]);
        var $thisElement = $this;
        var $thisValue = $this.valOrDefault();

        if ($this.hasClass("lookup")) {
          $thisElement = $this.find("[id^=txtlookup]");
          $thisValue = $thisElement.valOrDefault();
        }

        var isNotValid = $thisValue === "";//|| $thisValue === "0";
        if (isNotValid) {
          messageList.push($.GPPSU.getMessage(MSLSSTD005E, $("label[for=" + idList[i] + "]").html().trim()));
          if ($thisElement.hasClass('chosen-select')) {
            $thisElement.siblings().find('a').addClass("mandatory");
          } else {
            $thisElement.addClass("mandatory");
          }
          mandatoryEmptyList.push($thisElement);
        }
        if (messageList.length > 0) {
          $.messagepanel.show(messageList);
        }
      }

      return mandatoryEmptyList.length === 0;
    },

    format: function (fmt, ...args) {
      if (typeof args === 'undefined') {
        return fmt;
      }
      if (args.length == 0) {
        return fmt;
      }
      if (!fmt.match(/^(?:(?:(?:[^{}]|(?:\{\{)|(?:\}\}))+)|(?:\{[0-9]+\}))+$/)) {
        throw new Error('invalid format string.');
      }
      return fmt.replace(/((?:[^{}]|(?:\{\{)|(?:\}\}))+)|(?:\{([0-9]+)\})/g, (m, str, index) => {
        if (str) {
          return str.replace(/(?:{{)|(?:}})/g, m => m[0]);
        } else {
          if (index >= args.length) {
            throw new Error('argument index is out of range in format');
          }
          return args[index];
        }
      });
    },

    getMessage: function (msgId, ...args) {
      return $.GPPSU.format(msgId, ...args);
    },

    formatDate: function (date) {
      var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

      if (month.length < 2)
        month = '0' + month;
      if (day.length < 2)
        day = '0' + day;

      return [day, month, year].join('.');
    },

    formatDateMonth: function (date) {
      var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

      if (month.length < 2)
        month = '0' + month;
      if (day.length < 2)
        day = '0' + day;

      return [month, year].join('.');
    },

    formatDateRange: function (date) {
      var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

      if (month.length < 2)
        month = '0' + month;
      if (day.length < 2)
        day = '0' + day;



      var date1 = [day, month, year].join('.');
      var date2 = [day, month, year].join('.');
      return date1.concat(" - ", date2);
    },

    doOpenLogDetail: function (processId, idContainer) {
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
            $("#cmbMessageType").focus();
          });
          $(idContainer).modal();

        },
        error: function (resultMessage) {
          $.disable_enable_button.enable();
        }
      })
    },

    doSearchLogDetail: function(page) {
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
