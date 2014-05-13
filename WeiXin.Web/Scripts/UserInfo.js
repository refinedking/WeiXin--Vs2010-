/// <reference path="Shared/jquery-1.4.1.min.js" />
$(function () {
    $(".pwdChangeRow").toggle();
    var oldPwdIsRight = false;
    $("#oldPwd").focus(function () {
        oldPwdIsRight = false;
        $("#spanOldPwd").text("请输入原密码");
    }).blur(function () {
        $("#spanOldPwd").text("验证中...");
        $.ajax({
            url: "/Public/CheckOldPwd",
            data: { oldPwd: $("#oldPwd").val() },
            Type: "POST",
            dataType: "string",
            success: function (data) {
                if (data == "ok") {
                    $("#spanOldPwd").text("正确");
                    oldPwdIsRight = true;
                }
                else {
                    $("#spanOldPwd").text("原密码错");
                }
            },
            error: function () {
                $("#spanOldPwd").text("验证出错，请重试");
            }
        });
    });
    var newPwdIsRight = false;
    $("#newPwd").focus(function () {
        newPwdIsRight = false;
        $("#spanNewPwd").text("请输入6-18位新密码");
    }).blur(function () {
        if ($("#newPwd").val().length > 5 && $("#newPwd").val().length < 19) {
            newPwdIsRight = true;
            $("#spanNewPwd").text("");
            $("#cfrmPwd").focus();
        }
        else {
            $("#newPwd").focus();
        }
    });
    $("#cfrmPwd").focus(function () {
        newPwdIsRight = false;
        $("#spanCheckPwd").text("请再次输入新密码");
    }).blur(function () {
        if ($("#cfrmPwd").val().length > 0 && $("#newPwd").val() == $("#cfrmPwd").val()) {
            newPwdIsRight = true;
            $("#spanCheckPwd").text("");
        }
        else {
            $("#spanCheckPwd").text("两次密码不一致");
        }
    });
    $("#btnSavePwd").click(function () {
        if (newPwdIsRight && oldPwdIsRight) {
            document.location.href = "/Public/UpdatePwd/" + $("#newPwd").val();
        }
    });
});