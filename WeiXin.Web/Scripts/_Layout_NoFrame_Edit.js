/// <reference path="Shared/jquery-1.4.1.min.js" />
/// <reference path="PlugIn/jquery.form.js" />
$(function () {
    $("#divRightPage").css("padding-left", "0px");
    //纠正每个内容框架页面大小布局
    var obj = document.getElementById("divMain");

    if ($("#divLeftPage").width() > 0) {
        $("#divRightPage").css("margin-left", "222px");
    }
    else {
        $("#divRightPage").css("margin-left", "2px");
    }
    var bro = $.browser;
    $(window).resize(function () {
        if (bro.msie && bro.version == 7) {
            $("#divRightPage").css("min-height", eval($(window).height() - 26) + "px");
        }
        else {
            $("#divRightPage").css("min-height", eval($(window).height() - 8) + "px");
        }
        //        if (obj.scrollHeight > obj.clientHeight) {
        //            $("#divRightPage").width($("#divMain").width() - $("#divLeftPage").width() - 21);
        //        }
        //        else {
        //            $("#divRightPage").width($("#divMain").width() - $("#divLeftPage").width() - 6);
        //        }
    });
    $(window).resize();
});