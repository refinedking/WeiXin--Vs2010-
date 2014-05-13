/// <reference path="Shared/jquery-1.4.1.min.js" />
/// <reference path="PlugIn/jquery.form.js" />


$(function () {
    //右边框架页面加载提示关闭
    $("#RightPage").load(function () {
        //关闭数据加载提示
        Zuxia.Loading.hide();

        var win = document.getElementById("RightPage");
        if (document.getElementById("RightPage")) {
            if (win && !window.opera) {
                if (win.contentDocument && win.contentDocument.body.offsetHeight) {
                    $("#RightPage").height(win.contentDocument.body.scrollHeight);
                }
                else if (win.Document && win.Document.body.scrollHeight) {
                    $("#RightPage").height(win.Document.body.scrollHeight);
                }
            }
        }
        $(window).resize(); //由于此处可能会影响页面布局，所以一定要触发窗体尺寸改变事件
    });






    //查询区域表单样式
    $("#tblQuery td:even").width(70).css("text-align", "right");
    $("#tblQuery td:odd").width(110).css("text-align", "left");

    //查询区域表单异步提交事件
    $("#tblQuery").find(".divBtn").click(function () {
        $(this).parents("form").ajaxSubmit(function (data) {
            if (data == "ok") {
                $("#RightPage").attr("src", $("#RightPage").attr("src") + "?");
            }
        });
    });

    //左边模块事件
    //    $("#ulModule").find("ul").hide().first().show();
    $("#ulModule").find("ul").find("ul").hide();
    $("#ulModule").find("li").click(function () {
        var theUrl = $(this).attr("url");
        if (theUrl != undefined) {
            //判断是否在本窗体打开
            if ($(this).attr("ParentOpen") != undefined) {
                window.location.href = theUrl;
            }
            else {
                $("#RightPage").attr("src", theUrl).show();
            }
        }
        else {
            $(this).siblings().children("ul").hide();
            $(this).children("ul").toggle();
            $(window).resize(); //由于此处可能会影响页面布局，所以一定要触发窗体尺寸改变事件
        }
        return false; //阻止继续触发父级事件
    });
    //默认选中第一项
    //    $("#ulModule").find("li[url]").first().parents("li").click();
    $("#ulModule").find("li[url]").click(function () {
        $("#ulModule").find("li[url]").children().removeClass("cur");
        $(this).children().addClass("cur");
    });
    //打开默认页面
    //    $("#ulModule").find("li[url]").first().click();



    //纠正每个内容框架页面大小布局
    var obj = document.getElementById("divMain");
    var ifrmRightPage = document.getElementById("divLeftPage"); //右边框架对象
    $(window).resize(function () {
        $("#divLeftPage").height($("#divMain").height() - 2);
        $("#divRightPage,#divRightPage iframe").css("min-height", ($("#divMain").height() - 5)).children();
        if ((obj.scrollHeight > obj.clientHeight || obj.offsetWidth > obj.clientWidth)) {
            $("#divRightPage").width($("#divMain").width() - $("#divLeftPage").width() - 22);
        }
        else {
            $("#divRightPage").width($("#divMain").width() - $("#divLeftPage").width() - 5);
        }
    });
    $(window).resize().resize(); //此事件必须触发两次

});