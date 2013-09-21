tinyMCE.init({
	language: "en",
	mode: "exact",
	elements: "txtEditorBody",
	width: "100%",
	theme: "advanced",
	plugins: "safari,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,syntaxhl",
	theme_advanced_buttons1: "formatselect,fontselect,fontsizeselect,separator,forecolor,backcolor,separator,bold,italic,underline,strikethrough,separator,bullist,numlist,separator, justifyleft, justifycenter, justifyright", theme_advanced_buttons2: "undo,redo,cut,copy,paste,separator,justifyleft,justifycenter,justifyright,separator,outdent,indent,removeformat,separator,link,unlink,image,uploadImage,media,insertMusic,separator,syntaxhl,separator,code,fullscreen",
	theme_advanced_buttons3: "",
	theme_advanced_resizing: true,
	theme_advanced_resize_horizontal: false,
	theme_advanced_toolbar_location: "top",
	theme_advanced_toolbar_align: "left",
	theme_advanced_statusbar_location: "bottom",
	theme_advanced_fonts: "宋体=宋体;黑体=黑体;仿宋=仿宋;楷体=楷体;隶书=隶书;幼圆=幼圆;Arial=arial,helvetica,sans-serif;Comic Sans MS=comic sans ms,sans-serif;Courier New=courier new,courier;Tahoma=tahoma,arial,helvetica,sans-serif;Times New Roman=times new roman,times;Verdana=verdana,geneva;Webdings=webdings;Wingdings=wingdings,zapf dingbats",
	theme_advanced_font_sizes: "12px=12px,13px=13px,14px=14px,15px=15px,16px=16px,4(14pt)=14pt,5(18pt)=18pt,6(24pt)=24pt",
	forced_root_block: "",
	convert_fonts_to_spans: true,    
	remove_trailing_nbsp: true,
	convert_newlines_to_brs: false,
	force_br_newlines: false,
	force_p_newlines: true,
	remove_linebreaks: false,
	verify_html: false,
	relative_urls: false,
	convert_urls: false,
	remove_script_host: false,
	paste_auto_cleanup_on_paste: true,
	/*paste_preprocess: function(pl, o) {
	o.content = o.content.replace(/[\n\r\t]/g, '');
	o.content = o.content.replace(/<br\s\/><br\s\/>/g, '</p><p>');
	o.content = o.content.replace(/<br><br>/g, '</p><p>');
	},*/
	extended_valid_elements: "pre[name|class],style",
	handle_event_callback: "MCECheckIndent",
	cleanup: true,
	setupcontent_callback : "CustomSetupContent"
});

function CustomSetupContent(editor_id, body, doc) {    
	if (body.innerHTML == '编辑器加载中...' || body.innerHTML == '编辑器加载中...') {
		body.innerHTML = '';
	}
}

function CustomCommandHandler(editor_id, elm, command, user_interface, value) {
	//mceCodeEditor
	//mceRepaint
//    if (command == 'mceRepaint') {
//        var content = $(elm).html().replace(/<p>&nbsp;<\/p>/ig, '');
//        alert(content);
//        $(elm).html(content);
//    }
}

function MCECheckIndent(e) {
	if (e.type == 'keydown' && e.keyCode == 9) {
		//tinyMCE.activeEditor.selection.setContent('　　');
		tinyMCE.execCommand('mceInsertContent', false, '　　');
		return false;
	}
}

function InsertToEditor(content) {
	tinyMCE.execCommand('mceInsertContent', false, content);
}
function MyCustomUrlConverter(url, node, on_save) {
	if (url.indexOf("http://") < 0 && url.indexOf('://') < 0 && url.indexOf(".") != 0) {
		url = "http://" + url;
	}
	return url;
}
