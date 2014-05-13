<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.IO;
using System.Net;
using System.Web;

public class UploadHandler : IHttpHandler {

    public void ProcessRequest(HttpContext context) {
        try {
            string resId = context.Request.QueryString["resId"];
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";
            string strUploadPath = "E:\\自用\\项目学习资料\\上传\\codefans.net\\ManyFileUpload\\uploadFile\\";
            for (int i = 0; i < context.Request.Files.Count; i++) {
                HttpPostedFile postedFile = context.Request.Files[i];
                string fileName = strUploadPath + Path.GetFileName(postedFile.FileName);
                if (fileName != "") {
                    postedFile.SaveAs(fileName);
                }
            }
            context.Response.Write(" ");
        }
        catch (Exception ex) {
            context.Response.ContentType = "text/plain";
            context.Response.Write(ex.Message);
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}