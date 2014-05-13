/// <reference path="Shared/jquery-1.4.1.min.js" />
/// <reference path="PlugIn/jquery.form.js" />
$(function () {
    if ($("#divLeftPage").width() > 0) {
        $("#divRightPage").css("margin-left", "222px");
    }
    else {
        $("#divRightPage").css("margin-left", "2px");
    }
    //注册分页表单提交时显示数据加载提示事件
    $("form").bind("submit", function (e) {
        newPage = $(".txtCurPage").val();
        //newKeys = $("[name='keyWords']").val();
        if (oldPage < 1) {
            oldPage = 1;
        }
        if (newPage > 0) {
            //数据加载提示
            Zuxia.Loading.show("数据加载中,请稍后...");
            /// <summary>分页功能a标签单击事件处理函数</summary>
        }
        else {
            $(".txtCurPage").val(oldPage);
        }
        //数据加载提示
        Zuxia.Loading.show("数据加载中,请稍后...");
    });
    //获取原当前页序号
    var oldPage = $(".txtCurPage").val();
    //var oldKeys = $("[name='keyWords']").val();
    //var newKeys = "";
    var newPage = 0;
    //分页函数
    $(".divPager .right span[curPage]").click(function () {
        if ($(this).attr("curPage") > 0) {
            $(".txtCurPage").val($(this).attr("curPage"));
        }
        $(this).parents("form").submit();
    });

    //全选效果
    $("#tblList .chkAll").click(function () {
        $(this).parents("table").find(".chkItem").attr("checked", $(this).attr("checked"));
    });
    $("#tblList .chkItem").click(function () {
        if ($(this).attr("checked")) {
            $(this).parents("table").find(".chkAll").attr("checked", "checked");
        }
        else if ($("#tblList").find(".chkItem[checked='true']").length == 0) {
            $(this).parents("table").find(".chkAll").removeAttr("checked");
        }
    });
    $("#tblList a").click(function () {
        if ($(this).attr("cfmMsg") == undefined) {
            return;
        }
        else {
            return confirm($(this).attr("cfmMsg"));
        }
    });
    //行点击时选中这一行
    $(".tblItem").each(function (index, entity) {
        $(this).children().eq(1).siblings().click(function () {
            $(this).parent().find(".chkItem").attr("checked", !($(this).parent().find(".chkItem").attr("checked")));
            if ($(this).parent().find(".chkItem").attr("checked")) {
                $(this).parents("table").find(".chkAll").attr("checked", "checked");
            }
            else if ($("#tblList").find(".chkItem[checked='true']").length == 0) {
                $(this).parents("table").find(".chkAll").removeAttr("checked");
            }
        });
    });
    //增删改事件处理
    $(".divTitleCtrl a").click(function () {
        var eventType = $(this).attr("eventType"); //事件类型
        var strUrl = $(this).attr("href"); //与服务器交互地址
        var cfmMsg = $(this).attr("cfmMsg");
        if ($(this).attr("cfmMsg") == undefined) {
            cfmMsg = "你确定要操作吗？";
        }
        switch (eventType) {
            case "add":
                return true;
            case "edit":
                if (document.getElementById("tblList") == null) {
                    alert("目前没有数据可以操作！");
                    return false;
                }
                var inputList = document.getElementById("tblList").getElementsByTagName("input");
                var checkIndex = 0;
                var checkCount = 0;
                for (var i = 0; i < inputList.length; i++) {
                    if (inputList[i].type = "checkbox") {
                        if (inputList[i].checked && inputList[i].className == "chkItem") {
                            checkIndex = i;
                            checkCount++;
                        }
                    }
                }
                if (checkCount < 1) {
                    alert("请选择您要操作的一项");
                }
                else if (checkCount > 1) {
                    alert("一次只能修改一项");
                }
                else {
                    strUrl += "/" + $(inputList[checkIndex]).val(); //获取要修改的项
                    window.location.href = strUrl;
                }
                break;
            case "del":
                if (document.getElementById("tblList") == null) {
                    alert("目前没有数据可以操作！");
                    return false;
                }
                else if ($("#tblList").find(".chkItem[checked='true']").length == 0) {
                    alert("请选择要操作的项");
                    return false;
                }
                //确认操作
                if (confirm(cfmMsg)) {
                    var strIds = new Array();
                    for (var i = 0; i < $("#tblList").find(".chkItem[checked='true']").length; i++) {
                        strIds[i] = $("#tblList").find(".chkItem[checked='true']")[i].value;
                    }
                    window.location.href = strUrl + "/" + strIds.toString();
                }
                break;
            default:
                break;
        }
        return false;
    });
    //纠正每个内容框架页面大小布局
    var obj = document.getElementById("divMain");
    var bro = $.browser;
    $(window).resize(function () {
        if (bro.msie && bro.version == 7) {
            $("#divRightPage").css("min-height", eval($(window).height() - 26) + "px");
        }
        else {
            $("#divRightPage").css("min-height", eval($(window).height() - 8) + "px");
        }
        //        if (obj.scrollHeight > obj.clientHeight || obj.offsetWidth > obj.clientWidth) {
        //            $("#divRightPage").width($("#divMain").width() - $("#divLeftPage").width() - 22);
        //        }
        //        else {
        //            $("#divRightPage").width($("#divMain").width() - $("#divLeftPage").width() - 5);
        //        }
    });
    $(window).resize().resize(); //此事件必须触发两次
    //查询区域表单样式
    $("#tblQuery td:even").width(70).css("text-align", "right");
    $("#tblQuery td:odd").width(110).css("text-align", "left");
});