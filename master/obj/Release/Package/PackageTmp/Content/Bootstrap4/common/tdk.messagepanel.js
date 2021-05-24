!function ($) {
  "use strict";

  $.messagepanel = {
    //messages= [{type: 'ERR', message="Err: message Error"},{type: 'INF', message: ""}] or message = "", or message = ["",""];
    show: function (messages) {
      $("#messageContainer").empty();
      //var content = "<div class=\"message\"><div class=\"collapse\" id=\"messageCollapse\" aria-expanded=\"false\">";
      var content = "<div class=\"collapse\" id=\"messageCollapse\">";
      var type = 'ERR';
      if (!$.isArray(messages)) {
        var obj = [{ message: messages }];
      } else {
        if (messages.every(function (i) { return typeof i === "string" })) {
          obj = [];
          for (var m of messages) {
            obj.push({ message: m });
          }
        } else {
          try {
            obj = JSON.parse(messages);
          } catch (e) {
            obj = messages;
          }
        }
      }
      $.each(obj, function (key, value) {
        if (typeof value.type !== 'undefined') {
          type = value.type;
        } else {
          if (value.message.indexOf('INF') >= 0) {
            type = 'INF';
          } else if (value.message.indexOf('WRN') >= 0){
            type = 'WRN';
          } else {
            type = 'ERR'
          }
        }
       
        if (type == 'INF') {
          content += "<div class=\"alert alert-success alert-dismissible\" style='margin: 0px;'>" +
            "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\" onclick=\"javascript: $.messagepanel.closeMessage()\">×</button>";
          content += "<i class=\"far fa-check-circle\">&nbsp;</i>";
          content += value.message;
          content += "</div>"
        } else if (type == 'WRN') {
          content += "<div class=\"alert alert-warning alert-dismissible\" style='margin: 0px;'>" +
            "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\" onclick=\"javascript: $.messagepanel.closeMessage()\">×</button>";
          content += "<i class=\"fas fa-exclamation-circle\">&nbsp;</i>";
          content += value.message;
          content += "</div>"
        } else {
          content += "<div class=\"alert alert-danger alert-dismissible\" style='margin: 0px;'>" +
            "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\" onclick=\"javascript: $.messagepanel.closeMessage()\">×</button>";
          content += "<i class=\"far fa-times-circle\">&nbsp;</i>";
          content += value.message;
          content += "</div>"
        }
      });
      content += "</div>"    
      content += "<div id=\"collapseButton\" class=\"col-12 text-right\"> <a role=\"button\" class=\"collapsed text-right\" data-toggle=\"collapse\" href=\"#messageCollapse\"" +
        "aria-expanded=\"false\" aria-controls=\"messageCollapse\" style='display: none;'></a></div>";
      $("#messageContainer").html(content);
      $("#messageCollapse").collapse('show');
      var mandatoryTimer = setTimeout(function () {
        $("#messageCollapse").collapse('hide');
        clearTimeout(mandatoryTimer);
      }, 4000);
    },

    close: function () {
      $("#messageContainer").empty();
    },
    closeMessage: function () {
      var count = $("#messageContainer").find('.alert').length;
      if (count == 1) { // last alert
        $("#messageContainer").empty();
      }
    },
    showContent: function (content) {
      $("#messageContainer").html(content);
      $("#messageCollapse").collapse('show');
      var mandatoryTimer = setTimeout(function () {
        $("#messageCollapse").collapse('hide');
        clearTimeout(mandatoryTimer);
      }, 4000);
    }
    
  }

}(window.jQuery);
