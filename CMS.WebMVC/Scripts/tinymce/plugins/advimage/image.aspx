<%@ Page Theme=""  Language="C#" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Configuration" %>
<script runat="server">

    private string m_strBasePath = ConfigurationManager.AppSettings["tinyMCEImageBasePath"];
    private string m_strCurrentPath = null;
    private string m_strCurrentFile = null;

    protected override void OnLoad(EventArgs e)
    {
        if (IsPostBack)
        {
            HandlePostBack();
        }
        base.OnLoad(e);
    }
    
    protected override void Render(HtmlTextWriter writer)
    {
        LoadFiles();
        base.Render(writer);
    }

    private void HandlePostBack()
    {
        string strArg = Request["__EVENTARGUMENT"];
        if (strArg!=null)
        {
            general_tab.Attributes["class"] = "";
            appearance_tab.Attributes["class"] = "";
            general_panel.Attributes["class"] = "panel";
            appearance_panel.Attributes["class"] = "panel";

            search_panel.Attributes["class"] = "panel current";
            search_tab.Attributes["class"] = "current";

            if (strArg.StartsWith("CD|"))
            {
                string strFolder = strArg.Substring(3);
                if (strFolder.StartsWith("/")) strFolder = strFolder.Substring(1);
                if (!string.IsNullOrEmpty(strFolder))
                    m_strCurrentPath = m_strBasePath + "/" + strFolder;
                m_strCurrentPath = m_strCurrentPath.Replace("//", "/");
                hdnPath.Value = m_strCurrentPath;
            }
            else if (strArg.StartsWith("UPLOAD"))
            {
                m_strCurrentPath = m_strBasePath;
                if (!string.IsNullOrEmpty(hdnPath.Value))
                    m_strCurrentPath = hdnPath.Value;
                string strExt = System.IO.Path.GetExtension(upload.FileName.ToLower());
                if (upload.FileName.Length == 0)
                    lblUpload.Text = "No File!";
                else if (upload.FileContent.Length == 0)
                    lblUpload.Text = "Nothing was done!";
                else if (strExt.Equals(".jpg") || strExt.Equals(".jpeg") || strExt.Equals(".gif") || strExt.Equals(".png") || strExt.Equals(".bmp"))
                {
                    string strFilename = System.IO.Path.GetFileName(upload.FileName);
                    int file_append = 0;
                    while (System.IO.File.Exists(Server.MapPath(m_strCurrentPath + "/" + strFilename)))
                    {
                        file_append++;
                        strFilename = System.IO.Path.GetFileNameWithoutExtension(upload.FileName)
                                         + "_" + file_append + System.IO.Path.GetExtension(upload.FileName);
                    }
                    strFilename = Server.MapPath(m_strCurrentPath + "/" + strFilename);
                    try
                    {
                        upload.SaveAs(strFilename);
                        m_strCurrentFile = ResolveURL(CurrentPath) + "/" + System.IO.Path.GetFileName(strFilename);
                        m_strCurrentFile = m_strCurrentFile.Replace("//", "/");
                    }
                    catch (Exception ex)
                    {
                        lblUpload.Text = "Error uploading : " + ex.Message;
                    }
                }
                else 
                    lblUpload.Text = "Not an image!" + strExt;
            }
        }
    }
  private static string BaseURL
    {
        get
        {
            string strURL =
System.Web.HttpContext.Current.Request.ApplicationPath;
            if (!strURL.EndsWith("/"))
            {
                strURL += "/";
            }
            return strURL;
        }
    }
   
    private static string ResolveURL(string strURL)
    {
        if (strURL.Contains("~/"))
        {
            strURL = strURL.Replace("~/", BaseURL);
        }
        return strURL;
    }

    private string CurrentPath
    {
        get
        {
            if (m_strCurrentPath == null) return m_strBasePath;
            return m_strCurrentPath;
        }
    }

    private void LoadFiles()
    {
        if (string.IsNullOrEmpty(CurrentPath))
        {
            plcFileList.Controls.Add(new LiteralControl("No images available"));
        }
        else
        {
            LinkButton lnk = new LinkButton();
            lnk.Text = "force!!!";
            plcHidden.Controls.Add(lnk);
            
            plcFileList.Controls.Add(new LiteralControl("<table cellpadding=\"2\" cellspacing=\"0\" width=\"100%\">"));
            string strBase = Server.MapPath(CurrentPath);
            int nCount = 0;
            if (CurrentPath != m_strBasePath)
            {
                string strUp = CurrentPath;
                strUp = strUp.Substring(0, strUp.LastIndexOf("/"));
                strUp = strUp.Replace(m_strBasePath, "");
                string strStyle = nCount % 2 == 0 ? "SearchRow" : "SearchAltRow";
                plcFileList.Controls.Add(new LiteralControl("<tr onmouseover=\"this.className='SearchRowOver';\" onmouseout=\"SwitchClassBack(this,'" + strStyle + "');\" onclick=\"changeDir('" + strUp + "');\" class=\"" + strStyle + "\"><td><img align=\"top\" src=\"images/folderup.gif\" />&nbsp;"));
                plcFileList.Controls.Add(new LiteralControl("[ ... ]</td></tr>"));
                nCount++;
            }
            //load list of sub directories
            foreach (DirectoryInfo dir in new DirectoryInfo(Server.MapPath(CurrentPath)).GetDirectories())
            {
                string strFile = dir.FullName;
                strFile = strFile.Replace(Server.MapPath(m_strBasePath), "").Replace("\\", "/");
                string strStyle = nCount % 2 == 0 ? "SearchRow" : "SearchAltRow";
                plcFileList.Controls.Add(new LiteralControl("<tr onmouseover=\"this.className='SearchRowOver';\" onmouseout=\"SwitchClassBack(this,'" + strStyle + "');\" onclick=\"changeDir('" + strFile + "');\" class=\"" + strStyle + "\"><td><img align=\"top\" src=\"images/folder.gif\" />&nbsp;"));
                plcFileList.Controls.Add(new LiteralControl("[ " + dir.Name + " ]</td></tr>"));
                nCount++;
            }
            //load list of files
            foreach (FileInfo file in new DirectoryInfo(Server.MapPath(CurrentPath)).GetFiles())
            {
                string strExt = file.Extension.ToLower().Replace(".", "");
                if (strExt.Equals("jpg") || strExt.Equals("jpeg") || strExt.Equals("gif") || strExt.Equals("png") || strExt.Equals("bmp"))
                {
                    string strFile = ResolveURL(CurrentPath) + "/" + file.Name;
                    strFile = strFile.Replace("//", "/");
                    string strStyle = nCount % 2 == 0 ? "SearchRow" : "SearchAltRow";
                    if (strFile == m_strCurrentFile)
                    {
                        string strScript = "showPreview('" + strFile + "',document.getElementById('tr" + nCount + "'),'" + strStyle + "');";
                        Page.ClientScript.RegisterStartupScript(this.GetType(),"SelectRow", strScript, true);
                    }
                    
                    plcFileList.Controls.Add(new LiteralControl("<tr id=\"tr"+nCount+"\" onmouseover=\"this.className='SearchRowOver';\" onmouseout=\"SwitchClassBack(this,'" + strStyle + "');\" onclick=\"showPreview('" + strFile + "',this,'" + strStyle + "');\" class=\"" + strStyle + "\"><td>" + file.Name + "</td></tr>"));
                    nCount++;
                }
            }
            plcFileList.Controls.Add(new LiteralControl("</table>"));
        }
    }

    protected void hdnButton_Click(object sender, EventArgs e)
    {
        //do nothing
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>{#advimage_dlg.dialog_title}</title>
	<script language="javascript" type="text/javascript" src="../../tiny_mce_popup.js"></script>
	<script language="javascript" type="text/javascript" src="../../utils/mctabs.js"></script>
	<script language="javascript" type="text/javascript" src="../../utils/form_utils.js"></script>
	<script language="javascript" type="text/javascript" src="jscripts/functions.js"></script>
	<script type="text/javascript" src="js/image.js"> </script>	
	<script type="text/javascript" src="../../utils/validate.js"> </script>	
	<link href="css/advimage.css" rel="stylesheet" type="text/css" />
	<base target="_self" />
	<script language="javascript">
	var lastClass;
	var lastTD;
	function SetImage(src)
	{
	    mcTabs.displayTab('general_tab','general_panel');
	    var s = document.getElementById('src');
        s.value = src;
        //s.value = convertURL(src);
        //showPreviewImage(src, 0);
        ImageDialog.showPreviewImage(src)
	}
	
    function showPreview(src, td, Class) {
        if (lastTD) {
            lastTD.className = lastClass;
        }
        lastTD = td;
        lastClass = Class;
        td.className = 'SearchRowOver';

        loadPreview(src);
        return false;
    }
    
    function loadPreview(src){
        var img = document.getElementById('prevImage');
    
		var imgPreloader = new Image();
		imgPreloader.onload = function(){
			
		imgPreloader.onload = null;
	    
		var x = 190;
		var y = 220;
		var imageWidth = imgPreloader.width;
		var imageHeight = imgPreloader.height;
		var chkResize = document.getElementById('chkResize');
		if (chkResize.checked) {
		    if (imageWidth > 190 | imageHeight > 220) {
                var wscale = x / imageWidth;
                var hscale = y / imageHeight;
                var scale = (hscale < wscale ? hscale : wscale);
                imageWidth *= scale;
                imageHeight *= scale;
            }
		}

        img.src = src;
        img.width = imageWidth;
        img.height = imageHeight;
		};
		
		if (!src)
            src = img.src;
		imgPreloader.src = src;        
    }
    
    function SwitchClassBack(td, Class)
    {
        if (td == lastTD) {
            return;
        }
        td.className = Class;
    }

    function selectPic()
    {
        var img = document.getElementById('prevImage');
        //alert(img.src);
        SetImage(img.src);
    }
    
    function changeDir(dir){
        var theForm = document.forms['form1'];
        if (!theForm) {
            theForm = document.form1;
        }
        theForm.__EVENTTARGET.value = 'plcFileList';
        theForm.__EVENTARGUMENT.value = "CD|"+dir;
        theForm.submit();
    }
    
    function uploadPic() {
        var theForm = document.forms['form1'];
        if (!theForm) {
            theForm = document.form1;
        }
        theForm.__EVENTTARGET.value = 'plcFileList';
        theForm.__EVENTARGUMENT.value = "UPLOAD";
        theForm.submit();
    }
	</script>
</head>
<body id="advimage" onload="tinyMCEPopup.executeOnLoad('init();');" style="display: none">
    <form id="form1" runat="server">
    
		<div class="tabs">
			<ul>
				<li class="current" runat="server" id="general_tab"><span><a href="javascript:mcTabs.displayTab('general_tab','general_panel');" onmousedown="return false;">{#advimage_dlg.tab_general}</a></span></li>
				<li runat="server" id="appearance_tab"><span><a href="javascript:mcTabs.displayTab('appearance_tab','appearance_panel');" onmousedown="return false;">{#advimage_dlg.tab_appearance}</a></span></li>
				<li runat="server" id="search_tab"><span><a href="javascript:mcTabs.displayTab('search_tab','search_panel');" onmousedown="return false;">Upload</a></span></li>				
				<li runat="server" id="advanced_tab"><span><a href="javascript:mcTabs.displayTab('advanced_tab','advanced_panel');" onmousedown="return false;">{#advimage_dlg.tab_advanced}</a></span></li>

			</ul>
		</div>
        <input runat="server" id="hdnPath" type="hidden" />
		<div class="panel_wrapper">
			<div runat="server" id="general_panel" class="panel current">
				<fieldset>
						<legend>{#advimage_dlg.general} </legend>

						<table class="properties">
							<tr>
								<td class="column1"><label id="srclabel" for="src">{#advimage_dlg.src}</label></td>
								<td colspan="2"><table border="0" cellspacing="0" cellpadding="0">
									<tr> 
									  <td><input name="src" type="text" id="src" value="" onchange="ImageDialog.showPreviewImage(this.value);" /></td> 
									  <td id="srcbrowsercontainer">&nbsp;</td>
									</tr>
								  </table></td>
							</tr>
							<tr> 
								<td class="column1"><label id="altlabel" for="alt">{#advimage_dlg.alt}</label></td> 
								<td colspan="2"><input id="alt" name="alt" type="text" value="" /></td> 
							</tr> 
							<tr> 
								<td class="column1"><label id="titlelabel" for="title">{#advimage_dlg.title}</label></td> 
								<td colspan="2"><input id="title" name="title" type="text" value="" /></td> 
							</tr>
						</table>
				</fieldset>

				<fieldset>
					<legend>{#advimage_dlg.preview}</legend>
					<div id="prev"></div>
				</fieldset>
			</div>

				<div id="appearance_panel" runat="server" class="panel">
				<fieldset>
					<legend>{#advimage_dlg.tab_appearance}</legend>

					<table border="0" cellpadding="4" cellspacing="0">
						<tr> 
							<td class="column1"><label id="alignlabel" for="align">{#advimage_dlg.align}</label></td> 
							<td><select id="align" name="align" onchange="ImageDialog.updateStyle();ImageDialog.changeAppearance();"> 
									<option value="">{#not_set}</option> 
									<option value="baseline">{#advimage_dlg.align_baseline}</option>
									<option value="top">{#advimage_dlg.align_top}</option>
									<option value="middle">{#advimage_dlg.align_middle}</option>
									<option value="bottom">{#advimage_dlg.align_bottom}</option>
									<option value="text-top">{#advimage_dlg.align_texttop}</option>
									<option value="text-bottom">{#advimage_dlg.align_textbottom}</option>
									<option value="left">{#advimage_dlg.align_left}</option>
									<option value="right">{#advimage_dlg.align_right}</option>
								</select> 
							</td>
							<td rowspan="6" valign="top">
								<div class="alignPreview">
									<img id="alignSampleImg" src="img/sample.gif" alt="{#advimage_dlg.example_img}" />
									Lorem ipsum, Dolor sit amet, consectetuer adipiscing loreum ipsum edipiscing elit, sed diam
									nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.Loreum ipsum
									edipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam
									erat volutpat.
								</div>
							</td>
						</tr>

						<tr>
							<td class="column1"><label id="widthlabel" for="width">{#advimage_dlg.dimensions}</label></td>
							<td nowrap="nowrap">
								<input name="width" type="text" id="width" value="" size="5" maxlength="5" class="size" onchange="ImageDialog.changeHeight();" /> x 
								<input name="height" type="text" id="height" value="" size="5" maxlength="5" class="size" onchange="ImageDialog.changeWidth();" /> px
							</td>
						</tr>

						<tr>
							<td>&nbsp;</td>
							<td><table border="0" cellpadding="0" cellspacing="0">
									<tr>
										<td><input id="constrain" type="checkbox" name="constrain" class="checkbox" /></td>
										<td><label id="constrainlabel" for="constrain">{#advimage_dlg.constrain_proportions}</label></td>
									</tr>
								</table></td>
						</tr>

						<tr>
							<td class="column1"><label id="vspacelabel" for="vspace">{#advimage_dlg.vspace}</label></td> 
							<td><input name="vspace" type="text" id="vspace" value="" size="3" maxlength="3" class="number" onchange="ImageDialog.updateStyle();ImageDialog.changeAppearance();" />
							</td>
						</tr>

						<tr> 
							<td class="column1"><label id="hspacelabel" for="hspace">{#advimage_dlg.hspace}</label></td> 
							<td><input name="hspace" type="text" id="hspace" value="" size="3" maxlength="3" class="number" onchange="ImageDialog.updateStyle();ImageDialog.changeAppearance();" /></td> 
						</tr>

						<tr>
							<td class="column1"><label id="borderlabel" for="border">{#advimage_dlg.border}</label></td> 
							<td><input id="border" name="border" type="text" value="" size="3" maxlength="3" class="number" onchange="ImageDialog.updateStyle();ImageDialog.changeAppearance();" /></td> 
						</tr>

						<tr>
							<td><label for="class_list">{#class_name}</label></td>
							<td><select id="class_list" name="class_list"></select></td>
						</tr>

						<tr>
							<td class="column1"><label id="stylelabel" for="style">{#advimage_dlg.style}</label></td> 
							<td colspan="2"><input id="style" name="style" type="text" value="" onchange="ImageDialog.changeAppearance();" /></td> 
						</tr>

						<!-- <tr>
							<td class="column1"><label id="classeslabel" for="classes">{#advimage_dlg.classes}</label></td> 
							<td colspan="2"><input id="classes" name="classes" type="text" value="" onchange="selectByValue(this.form,'classlist',this.value,true);" /></td> 
						</tr> -->
					</table>
				</fieldset>
			</div>

			<div runat="server" id="search_panel" class="panel">
			<div class="search_panel">
	            <fieldset>
		            <legend>Browse Server</legend>
		            <table cellpadding="0" cellspacing="0" width="100%">
		            <tr>
		                <td rowspan="2">
                            <div class="search">
                                <asp:PlaceHolder ID="plcFileList" runat="server">
                                </asp:PlaceHolder>
                            </div>
                        </td>
                        <td style="padding-left:5px" align="center" width="200" height="100%">
                            <div class="preview">
                                <table width="100%" height="100%"><tr><td width="100%" height="100%" align="center" class="previewcell" valign="middle">
                                <img id="prevImage" src="img/sample.gif" border="0" alt="preview" />
                                </td></tr></table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center"><input id="chkResize" onclick="loadPreview();" style="height:10pt;width:20px" checked type="checkbox" /><label for="chkResize">Resize to fit</label></td>
                    </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>Upload File</legend>
                    <asp:FileUpload CssClass="upload" ID="upload" runat="server" /><input class="uploadButton" onclick="uploadPic();" type="button" name="btnUpload" value="Upload" style="display:inline;float:left;margin-left:10px;width:100px;" />
                    <asp:Label ID="lblUpload" runat="server" ForeColor="red" Text=""></asp:Label>
                </fieldset>
                <fieldset>
                    <legend>Complete</legend>
                    <input class="uploadButton topSpace" onclick="selectPic();" type="button" name="btnSelect" value="Select" />
                </fieldset>
			</div>
			</div>

			<div id="advanced_panel" class="panel">
				<fieldset>
					<legend>{#advimage_dlg.swap_image}</legend>

					<input type="checkbox" id="onmousemovecheck" name="onmousemovecheck" class="checkbox" onclick="ImageDialog.setSwapImage(this.checked);" />
					<label id="onmousemovechecklabel" for="onmousemovecheck">{#advimage_dlg.alt_image}</label>

					<table border="0" cellpadding="4" cellspacing="0" width="100%">
							<tr>
								<td class="column1"><label id="onmouseoversrclabel" for="onmouseoversrc">{#advimage_dlg.mouseover}</label></td> 
								<td><table border="0" cellspacing="0" cellpadding="0"> 
									<tr> 
									  <td><input id="onmouseoversrc" name="onmouseoversrc" type="text" value="" /></td> 
									  <td id="onmouseoversrccontainer">&nbsp;</td>
									</tr>
								  </table></td>
							</tr>
							<tr>
								<td><label for="over_list">{#advimage_dlg.image_list}</label></td>
								<td><select id="over_list" name="over_list" onchange="document.getElementById('onmouseoversrc').value=this.options[this.selectedIndex].value;"></select></td>
							</tr>
							<tr> 
								<td class="column1"><label id="onmouseoutsrclabel" for="onmouseoutsrc">{#advimage_dlg.mouseout}</label></td> 
								<td class="column2"><table border="0" cellspacing="0" cellpadding="0"> 
									<tr> 
									  <td><input id="onmouseoutsrc" name="onmouseoutsrc" type="text" value="" /></td> 
									  <td id="onmouseoutsrccontainer">&nbsp;</td>
									</tr> 
								  </table></td> 
							</tr>
							<tr>
								<td><label for="out_list">{#advimage_dlg.image_list}</label></td>
								<td><select id="out_list" name="out_list" onchange="document.getElementById('onmouseoutsrc').value=this.options[this.selectedIndex].value;"></select></td>
							</tr>
					</table>
				</fieldset>

				<fieldset>
					<legend>{#advimage_dlg.misc}</legend>

					<table border="0" cellpadding="4" cellspacing="0">
						<tr>
							<td class="column1"><label id="idlabel" for="id">{#advimage_dlg.id}</label></td> 
							<td><input id="id" name="id" type="text" value="" /></td> 
						</tr>

						<tr>
							<td class="column1"><label id="dirlabel" for="dir">{#advimage_dlg.langdir}</label></td> 
							<td>
								<select id="dir" name="dir" onchange="ImageDialog.updateStyle();ImageDialog.changeAppearance();"> 
										<option value="">{#not_set}</option> 
										<option value="ltr">{#advimage_dlg.ltr}</option> 
										<option value="rtl">{#advimage_dlg.rtl}</option> 
								</select>
							</td> 
						</tr>

						<tr>
							<td class="column1"><label id="langlabel" for="lang">{#advimage_dlg.langcode}</label></td> 
							<td>
								<input id="lang" name="lang" type="text" value="" />
							</td> 
						</tr>

						<tr>
							<td class="column1"><label id="usemaplabel" for="usemap">{#advimage_dlg.map}</label></td> 
							<td>
								<input id="usemap" name="usemap" type="text" value="" />
							</td> 
						</tr>

						<tr>
							<td class="column1"><label id="longdesclabel" for="longdesc">{#advimage_dlg.long_desc}</label></td>
							<td><table border="0" cellspacing="0" cellpadding="0">
									<tr>
									  <td><input id="longdesc" name="longdesc" type="text" value="" /></td>
									  <td id="longdesccontainer">&nbsp;</td>
									</tr>
								</table></td> 
						</tr>
					</table>
				</fieldset>
			</div>		
		


		</div>








		<div class="mceActionPanel">
			<div style="float: left">
				<input type="button" id="insert" name="insert" value="{#insert}" onclick="ImageDialog.insert();" />
			</div>

			<div style="float: right">
				<input type="button" id="cancel" name="cancel" value="{#cancel}" onclick="tinyMCEPopup.close();" />
			</div>
		</div>
		<div style="display:none">
        <asp:PlaceHolder ID="plcHidden" runat="server"></asp:PlaceHolder></div>
    </form>
</body>
</html>
