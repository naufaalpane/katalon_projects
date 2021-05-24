!function ($) {
    "use strict";

    $.messagebox = {
      show: function (title, text, type, msgboxbutton, trueevent, falseevent) {
        //alert(text);
            var confirmId = "";
            if (msgboxbutton == "CONFIRM")
                confirmId = msgboxbutton;
            if ($("body #messagebox" + confirmId).length === 0) {
              $("body").append("<div id=\"messagebox" + confirmId + "\" class=\"modal fade bs-example-modal-lg\" data-backdrop=\"static\" data-keyboard=\"false\">" +
                "<div class=\"modal-dialog\" style=\"min-width:300px\">" +
                "<div class=\"modal-content\"><div class=\"modal-header\">" +
                "<h4 class=\"modal-title\"  id=\"messagebox-title\"></h4>" +
                "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\">" +
                "<span aria-hidden=\"true\" id=\"message-img\"></span>" +
                "</button></div>" +
                "<div class=\"modal-body\"><div class=\"row\" style=\"overflow-y: auto; max-height: 515px;\" >" +
                "<p id=\"messagebox-text\"></p></div></div>" +
                "<div class=\"modal-footer justify-content-end\"><div id=\"message-button\"></div></div>" +
                "</div></div></div>");
              
            }

            $('#messagebox'+ confirmId+' .modal-backdrop').not(':eq(0)').not(':eq(1)').remove();

            var modalContent = $("body #messagebox" + confirmId + " .modal-content");
          var confirmButton = "<div class=\"btn-toolbar\" >" +
                              "<button type=\"button\" class=\"btn btn-primary mr-1\" data-dismiss=\"modal\" onclick=\"" +
                              (trueevent !== "" ? "javascript:" + trueevent : "") +
                              "\">Yes</button>" +
                              "<button class=\"btn btn-danger\" data-dismiss=\"modal\"  onclick=\"" +
                              (falseevent !== "" ? "javascript:" + falseevent : "") +
                              "\">No</button>" +
                              "</div>";

          var singleButton = "<div class=\"form-group form-group-xs\" >" + 
                              "<button type=\"button\" class=\"btn btn-primary\" data-dismiss=\"modal\" onclick=\"" +
                              (trueevent !== "" ? "javascript:" + trueevent : "") +
                              "\">OK</button>" +
                              "</div>";
            var img = "";
            modalContent.removeClass("message-info message-warning message-danger message-success");
            type = type || "INF";
            switch (type) {
                case "W":
                case "WRN":
                    img = "<img src=\"/Content/Bootstrap4/img/Warning.png\" class=\"modal-icon\" />";
                    modalContent.addClass("message-warning");
                    break;
                case "E":
                case "ERR":
                    img = "<img src=\"/Content/Bootstrap4/img/Error.png\" class=\"modal-icon\" />";
                    modalContent.addClass("message-danger");
                    break;
                case "S":
                case "SUC":
                    img = "<img src=\"/Content/Bootstrap4/img/information.png\" class=\"modal-icon\" />";
                    modalContent.addClass("message-success");
                    break;
                default:
                    img = "<img src=\"/Content/Bootstrap4/img/information.png\" class=\"modal-icon\" />";
                    modalContent.addClass("message-info");
                    break;
            }

            switch (msgboxbutton) {
                case "CONFIRM":
                    img = "<img src=\"/Content/Bootstrap4/img/question.png\" class=\"modal-icon\" />";
                    $("#messagebox" + confirmId + " #message-button").html(confirmButton);
                    break;
                case "SINGLE":
                default:
                    $("#messagebox" + confirmId + " #message-button").html(singleButton);
                    break;
            }

            switch (title) {
                case "W":
                case "WRN":
                    title = "Warning";
                    break;
                case "E":
                case "ERR":
                    title = "Error";
                    break;
                case "S":
                case "SUC":
                    title = "Success";
                    break;
                case "I":
                case "INF":
                    title = "Information";
                    break;
            }

            $("#messagebox"+ confirmId+ " #message-img").html(img);
            $("#messagebox" + confirmId + " #messagebox-title").html(title);
            $("#messagebox" + confirmId + " #messagebox-text").html(text);

            $("#messagebox"+ confirmId).modal();
        },

        hide: function () {
            $("#messagebox"+ confirmId).modal("hide");
        }
    }

}(window.jQuery);
