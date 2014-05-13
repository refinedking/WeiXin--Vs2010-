<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="HRMS.PlugIn.FileUpLoad.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="jquery-1.3.2.js" type="text/javascript"></script>

    <script src="jquery.uploadify.js" type="text/javascript"></script>

    <script src="uploadPreview.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            $("#aa").uploadPreview({ width: 240, height: 176, imgDiv: "#imgDiv", imgType: ["bmp", "gif", "png", "jpg"] });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div style="width: 240px; height: 176px; overflow: hidden;">
        <div id="imgDiv" style="width: 240px; height: 176px;">
        </div>
    </div>
    <div>
        <input type="file" id="aa" />
    </div>
    </form>
</body>
</html>
