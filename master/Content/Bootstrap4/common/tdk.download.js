!function ($) {
  "use strict";

  $.download = {

    doDownload: function (options) {
      var $settings = $.extend({}, {
        dataName: "",
        searchFormId: "",
        gridId: "",
        downloadUrl: "",
        useStyling: true

      }, options);

      if ($settings.downloadUrl === "") {
        throw new InvalidOperationException("options must have downloadUrl value");
      }

      var searchParameter = {};
      $('._search' + $settings.dataName).each(function (i, obj) {
        searchParameter[$(obj).data('name')] = $(obj).val();
      });

      var downloadConfig = {};
      var header = {};
      var staticData = {};
      var detail = [];
      if ($settings.searchFormId) {
        $('#' + $settings.searchFormId).find('input:text, input:radio, select').each(function(i, element) {
          if ($(element).data('header')) {
            var dat = {};
            var key = $(element).data('header');
            var val = $(element).val();
            dat[key] = val;
            //staticData.push(dat);
            staticData[key] = val;
          }
        });
        $('#' + $settings.searchFormId).find('label').each(function(i, element) {
          if ($(element).data('header')) {
            var dat = {};
            var key = $(element).data('header');
            var val = $(element).text();
            dat[key] = val;
            //staticData.push(dat);
            staticData[key] = val;
          }
        });
      }

      if ($settings.gridId) {
        if ($('#' + $settings.gridId).data('field-start')) {
          header['StartRow'] = $('#' + $settings.gridId).data('field-start');
        } else {
          header['StartRow'] = 1;
        }
        var j = 1;
        $('#' + $settings.gridId).find('th').each(function (i, element) {
         
          if ($(element).data('field')) {
            var val = "";
            if ($(element).find('label').length) {
              val = $(element).children('label').text();
            } else {
              val = $(element).text();
            }
            //$(element).index();
            detail.push({ ColumnNo: j++, ColumnDbField: $(element).data('field'), ColumnHeader: val });
          }
        });
      }
      header['StaticData'] = staticData;


      downloadConfig['Header'] = header;
      downloadConfig['Detail'] = detail;
      downloadConfig['UseStyling'] = $settings.useStyling;

      searchParameter['DownloadConfig'] = downloadConfig; 

      $.fileDownload($settings.downloadUrl,
        {
          data: searchParameter,
          httpMethod: "POST",
          failCallback: function (status) {
            //$('.custom-overlay').hide();
            $("#messagePanel").html(status);
          }
        });
    }
  }
}(window.jQuery);
