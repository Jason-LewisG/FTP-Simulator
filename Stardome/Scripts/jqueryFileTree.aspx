<%@ Page Language="C#" AutoEventWireup="true" %>

<%	
	string dir;
    int Role = Convert.ToInt32(Request.QueryString["Role"]);
	if(Request.Form["dir"] == null || Request.Form["dir"].Length <= 0)
		dir = "/";
	else
		dir = Server.UrlDecode(Request.Form["dir"]);
	System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(dir);
	Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">\n");
	foreach (System.IO.DirectoryInfo di_child in di.GetDirectories())
        Response.Write("\t<li class=\"directory collapsed\"><a href=\"#\" name='" + di_child.Name + "' rel=\"" + dir + di_child.Name + "/\">" + di_child.Name + "</a></li>\n");
	foreach (System.IO.FileInfo fi in di.GetFiles())
	{
		string ext = ""; 
		if(fi.Extension.Length > 1)
			ext = fi.Extension.Substring(1).ToLower();        
        string filePath = dir.Remove(0, Server.MapPath("~").Length);
        filePath = "/" + filePath.Remove(filePath.Length - 1) + "\\" + fi.Name;
        string DeleteButton = "";
        if (Role == 1)
            DeleteButton = "&nbsp;<a href='#' ><img id='" + filePath + "' src='\\Images\\Delete.png' Title='Delete file' style='width:15px;Height:15px' onclick ='deleteFile(this.id);'></a>";


        Response.Write("\t<li id=\"file\" class=\"file ext_" + ext + "\"><input class=\"FTCB\" type=\"checkbox\" onclick=\"checkB(this.id)\" id=\"" + dir + fi.Name +
                    "\" rel=\"" + dir + fi.Name + "\"><a class=\"file\" href=\"#\" rel=\"" + dir + fi.Name + "\">" + fi.Name +
                    "</a>&nbsp;&nbsp;<a href='#' ><img src='\\Images\\Play.png' Title='Play the file' id='" + filePath + "' style='width:15px;Height:15px' onclick='aud_play_pause(this.id);'/></a>" +
                    DeleteButton +
                    "&nbsp;<a href='#' ><img src='\\Images\\download.ico' Title='Download the file' style='width:15px;Height:15px' id='" + filePath + "' onclick ='downloadMP3(this.id);'/></a></li>" +
                        "\n");		
	}
	Response.Write("</ul>");
    Response.Write("<audio id='MP3Player'> <source src='' type='audio/mp3'></audio>");
 %>