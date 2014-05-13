/// <reference path="Shared/jquery-1.4.1.min.js" />
$(function () {
    var isKeyDown = false;

    $(".divList").children().click(function () {
        $(this).find("input").attr("checked", !$(this).find("input").attr("checked"));
    }).mousedown(function () {
        isKeyDown = true;
    }).mouseup(function () {
        isKeyDown = false;
    }).mousemove(function () {
        if (isKeyDown) {
        }
    });
});
function moveCheckedToRight() {
    $("#divLeft").children().each(function () {
        if ($(this).find("input").attr("checked")) {
            $(this).appendTo($("#divRight"));
        }
    });
}
function moveAllToRight() {
    $("#divLeft").children().each(function () {
        $(this).appendTo($("#divRight"));
    });
}
function moveCheckedToLeft() {
    $("#divRight").children().each(function () {
        if ($(this).find("input").attr("checked")) {
            $(this).appendTo($("#divLeft"));
        }
    });
}
function moveAllToLeft() {
    $("#divRight").children().each(function () {
        $(this).appendTo($("#divLeft"));
    });
}
function setCheckedToHidden() {
    if ($("#sltClass").val() > 0) {
        if ($("#divRight").children().length == 0) {
            alert("请选择学员");
            return false;
        }
        alert($("#divRight").children().length);
        var newStuList = "";
        $("#divRight").children().each(function () {
            newStuList += $(this).find("input").val() + ",";
        });
        newStuList = newStuList.substr(0, newStuList.length - 1);
        $("#newStuList").val(newStuList);
    }
    else {
        alert("请先选择班级");
        return false;
    }
}