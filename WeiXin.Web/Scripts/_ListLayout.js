/// <reference path="Shared/jquery-1.4.1.min.js" />
$(function () {
    //注册分页表单提交时显示数据加载提示事件
    $("form").bind("submit", function (e) {
        newPage = $(".txtCurPage").val();
        newKeys = $("[name='keyWords']").val();
        if (oldPage < 1) {
            oldPage = 1;
        }
        if ((newPage > 0 && newPage != oldPage) || newKeys != oldKeys) {
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
    var oldKeys = $("[name='keyWords']").val();
    var newKeys = "";
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
    //数据项选择事件
    $("#tblList .chkItem").click(function () {
        if ($(this).attr("checked")) {
            $(this).parents("table").find(".chkAll").attr("checked", "checked");
        }
        else if ($("#tblList").find(".chkItem[checked='true']").length == 0) {
            $(this).parents("table").find(".chkAll").removeAttr("checked");
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
                    Zuxia.Alert("目前没有数据可以操作！");
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
                    Zuxia.Alert("请选择您要操作的一项");
                }
                else if (checkCount > 1) {
                    Zuxia.Alert("一次只能操作一项");
                }
                else {
                    strUrl += "/" + $(inputList[checkIndex]).val(); //获取要修改的项
                    window.location.href = strUrl;
                }
                break;
            case "del":
                if (document.getElementById("tblList") == null) {
                    Zuxia.Alert("目前没有数据可以操作！");
                    return false;
                }
                else if ($("#tblList").find(".chkItem[checked='true']").length == 0) {
                    Zuxia.Alert("请选择要操作的项");
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
            case "createtab":

                if (document.getElementById("tblList") == null) {
                    Zuxia.Alert("目前没有数据可以操作！");
                    return false;
                }
                var inputList = document.getElementById("tblList").getElementsByTagName("input");
                var inputListes = $("#tblList").children(".chkItem");

                var NameList = document.getElementById("tblList").getElementsByTagName("span");
                var checkIndex = 0;
                var checkCount = 0;
                for (var i = 0; i < inputList.length; i++) {

                    if (inputList[i].className == "chkItem") {
                        if (inputList[i].type = "checkbox") {
                            if (inputList[i].checked && inputList[i].className == "chkItem") {
                                checkIndex = i;
                                checkCount++;
                            }
                        }
                    }
                }
                if (checkCount < 1) {
                    Zuxia.Alert("请选择您要操作的一项");
                }
                else if (checkCount > 1) {
                    Zuxia.Alert("一次只能操作一项");
                }
                else {

                    strUrl += "/" + $(inputList[checkIndex]).val(); //获取要修改的项
                    CreateTab('StuBaseInfo' + $(inputList[checkIndex]).val(), '' + $('#' + $(inputList[checkIndex]).val() + '').val() + '', '' + strUrl + '', 2);
                }
                break;
        }
        return false;
    });
});