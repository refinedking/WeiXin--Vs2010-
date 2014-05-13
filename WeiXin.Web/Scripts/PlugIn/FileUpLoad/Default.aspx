<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LoadFile._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>

    <script src="File/jquery-1.3.2.min.js" type="text/javascript"></script>

    <script src="File/jquery.uploadify.js" type="text/javascript"></script>

    <link href="File/uploadify.css" rel="stylesheet" type="text/css" media="screen"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input type="file" name="fileInput" id="fileInput" />
    <br />
    <a href="javascript:$('#fileInput').fileUploadStart();">上传文件</a> | <a href="javascript:$('#fileInput').fileUploadClearQueue();">清空</a>
    <script type="text/javascript">
        $(document).ready(function() {
            $('#fileInput').fileUpload({
                'uploader': 'File/uploader.swf', //指定上传控件的主体文件，默认‘uploader.swf’
                'script': 'File/UploadHandler.ashx', //指定服务器端上传处理文件，默认‘upload.php’
                'cancelImg': 'File/cancel.png', //指定取消上传的图片，默认‘cancel.png’                
                'auto': false,              //选定文件后是否自动上传，默认false
                //'folder': '/',         //要上传到的服务器路径，默认‘/’
                'muti': true,               //是否允许同时上传多文件，默认false
                //'fileDesc': 'rar文件或zip文件',  //出现在上传对话框中的文件类型描述
                'fileExt': '*.*',      //控制可上传文件的扩展名，启用本项时需同时声明fileDesc
                'sizeLimit': 5000*1024*1024,         //控制上传文件的大小，单位byte
                'simUploadLimit': 1         //多文件上传时，同时上传文件数目限制

            });
        });
</script>
    </div>
    </form>
</body>
</html>
