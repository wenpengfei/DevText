//DataBind jQuery plugin
var currentBindUrl = "";
(function ($) {
	var callbackList = []; //存放callback
	var dataList = []; //存放table的Data 非ajax请求分页
	var templateList = []; //存放一般的模板列表

	///
	/// 绑定一个循环列表 
	///
	$.fn.bindList = function (args, template, options, async, callback) {
		var me = this;
		var url, async, template, dataSource;

		url = me.attr("datasource");
		async = me.attr("async") == "0";
		if (arguments.length > 0) {
			switch (typeof (args)) {
				case "string":
					url = args;
					break;
				case "object":
					dataSource = args;
					break;
			}
		}
		//args是json
		switch (arguments.length) {
			case 1:
				if (typeof (args) == "string") {
					url = args;
				} else if (typeof (args) == "object") {
					url = args.url;
					async = args.async || async;
					template = args.template || template;
					options = args.options || options;
					dataSource = args.data;
				}
				break;
			case 2: // url,async || url,tempalte || data,template
				template = template;
				if (typeof (template) == "bool") {
					async = template;
					template = undefined;
				}
				break;
			case 3: //url,template,async || url,template,options || data,template,options
				template = template;
				options = options;
				if (typeof (options) == "bool") {
					async = options;
					options = undefined;
				} break;
			case 4:
				template = template;
				options = options;
				async = async;
				break;
		}

		if ((url == undefined || url == "") && dataSource == undefined) return me;

		if (dataSource == undefined) {
			$.ajax({
				async: async,
				url: url,
				dataType: "json",
				success: function (data) {
					//if (data == "" || data == null) return;
					me.bindListData(data.list || data, template, options);
					if (callback != null)
						callback();
				}
			});
		} else {
			me.bindListData(dataSource, template, options);
		}
		return me;
	};
	$.fn.bindListData = function (data, template, options) {
		var me = this;
		template = template || me.getTemplate();

		this.empty();
		var resultTemplate = replaceTemplate(template, data);

		this.html(resultTemplate);

		var prepend, append, dvalue;

		if (options != undefined) {
			prepend = options.prepend;
			append = options.append;
			dvalue = options.dvalue;
		}

		if (prepend != undefined) {
			this.prepend(prepend);
		}

		if (append != undefined) {
			this.append(append);
		}

		if (dvalue != undefined) {
			if ($.browser.msie && ($.browser.version == "6.0")) { //IE6 模式下
				var objSelect = this;
				setTimeout(function () {
					objSelect.bindControl(dvalue);
				}, 500);
			} else {
				this.bindControl(dvalue);
			}
		} else if (this.attr("type") != undefined) {
			if (this.attr("type").indexOf("select") > -1) {
				this[0].selectedIndex = 0;
			}
		}
		me.setLink();
	};

	$.fn.setLink = function () {
		$("a", this).each(function () {
			var href = $(this).attr("href");
			if (href == undefined) return true;
			if (href.indexOf("javascript") >= 0) return true;
			if (href.indexOf("#") >= 0) return true;
			$(this).unbind("click").click(function () {
				switch (this.target) {
					case "_new": //弹出窗口
						this.width = request.QueryString("width", href);
						this.height = request.QueryString("height", href);
						var win = new ajaxWin({
							url: href,
							height: this.height,
							width: this.width,
							title: this.title
						});
						win.open();
						break;
					case "_window": //弹窗本身load
						loadWindow(href, this.title);
						break;
					case "_delete": //删除连接
						deleteData(href);
						break;
					case "_main":
						var _href = this.href;
						$("#main").loadUrl(this.href);
						break;
					case "_blank":
					case "_self":
					case "_top":
					case "":
					case null:
						return true;
					case undefined:
					case "undefined":
						href = "#";
						return false;
					default:
						if (this.target != "")
							if ($("#" + this.target).length > 0) {
								$("#" + this.target).loadUrl(href);
							} else {
								this.target = "";
								return true;
							}
						break;
				}
				return false;
			});
		});
	}

	///
	/// 绑定表格
	///
	$.fn.bindTable = function (args) {
		var me = this;
		if (me.length == 0) {
			return me;
		}
		var id = this.attr("id") || me.attr("name") || me.attr("class");
		//定义需要的参数
		var url, pagesize, pageindex, data, ispage, callback, listName, isappend, sort, desc, preBind;
		//检查传递的参数

		switch (arguments.length) {
			case 0:
				args = {};
				break;
			case 1:
				switch (typeof (args)) {
					case "string": //只传url
						url = args;
						args = {};
						args.url = url;
						break;
					case "number": //只传pagesize
						pagesize = args;
						args = {};
						args.pagesize = pagesize;
						break;
					case "function": //只传callback
						callback = args;
						args = {};
						args.callback = callback;
						callbackList.id == undefined;
						break;
					case "undefined": //啥也没传
					default:
						args = args || "";
						break;
				}
				break;
			case 2:
				if (typeof (arguments[0]) == "string") {
					url = arguments[0];
					args = {};
					args.url = url;
				} else if (typeof (arguments[0]) == "object") {
					args = {};
					data = arguments[0];
				}

				if (typeof (arguments[1]) == "function") {
					callback = arguments[1];
					args.callback = callback;
				}
				break;
			case 3:
				if (typeof (arguments[0]) == "string") {
					url = arguments[0];
					args = {};
					args.url = url;
				} else if (typeof (arguments[0]) == "object") {
					args = {};
					data = arguments[0];
				}

				if (typeof (arguments[1]) == "function") {
					callback = arguments[1];
					args.callback = callback;
				}
				if (typeof (arguments[2]) == "function") {
					preBind = arguments[2];
				}
				break;
		}

		url = args.url || me.attr("datasource"); //绑定的数据源为url请求 datasource是table的一个自定义属性
		callback = args.callback || callbackList[id]; //绑定后的Function
		if (args.data == undefined && args.url == undefined && pageindex == 1) {
			dataList[id] = [];
		}
		data = args.data || dataList[id]; //绑定的数据源为已请求好的JsonData

		pageRequest = args.pageRequest || false; //是否为分页请求
		pagesize = args.pagesize || parseInt(me.attr("pagesize")) || Math.round(Math.round((window.screen.height - 500) / 26) / 5) * 5; //分页
		isappend = args.isappend || false; //是否把数据填充到现在的列表

		sort = me.attr("sort") || "";
		desc = me.attr("desc") || "";

		if (pageRequest) {
			pageindex = args.pageindex || 1;
		} else {
			pageindex = 1;
		}

		listName = args.list || "list"; //请求的数据源的格式是Array还是Dictionary 例如{recourCount:xx,list:[]}

		ispage = args.ispage || me.attr("ispage"); //是否分页

		if (url == undefined && data == undefined)//如果url请求和data都没有则不执行绑定
			return me;

		if (args.callback != undefined)//如果请求中有callback 清掉callbackList.id
			callbackList[id] = undefined;
		if (args.url != undefined) {	//如果是url请求 清掉data和dataList.id
			data = undefined;
			dataList[id] = undefined;
		}
		//如果是url请求
		if (data == undefined) {
			if (url.indexOf("?") == -1)
				url += "?";
			else
				url = url.replace(/&?(pageindex|pagesize|sort|desc|_)=([^&]*)/gi, ""); //过滤url的翻页参数
			//alert("sort:" + sort + "\ndesc:" + desc + "\n" + url);
			url += "&sort=" + sort + "&desc=" + desc;

			if (ispage != "0") {
				url += "&pageindex=" + pageindex + "&pagesize=" + pagesize;
			}
			currentBindUrl = url;
			try { me.ajaxRequest && me.ajaxRequest.abort(); } catch (ex) { }
			me.ajaxRequest = $.ajax({
				url: url,
				dataType: "json",
				async: args.async || false, //这里用了同步请求，IE6下会卡一下，如果用异步，则需要把后面的代码做成一个单独的方法。
				global: !args.async,
				beforeSend: function () {
					$("#loading").show().css({ "top": (20 + document.documentElement.scrollTop) + "px" });
				},
				complete: function () {
					$("#loading").hide();
				},
				success: function (result) {
					if (result == "") {
						data = undefined;
						return;
					}
					data = result;
					if (preBind) {
						preBind(data);
					}
					bindTableData();
				}
			});
		} else {
			bindTableData();
		}

		function bindTableData() {
			//如果不是添加，先把tbody置空
			if (!isappend) {
				$(".pagelist", me).empty().hide();
				$("tbody:eq(0)", me).empty();
			}
			if (data == undefined) {	//ajax请求失败
				return me;
			}

			//声明一个新的数据源 不能改变请求的数据结构
			var dataSource = [];

			if (data[listName] === undefined) {
				dataSource = data;
			} else if (data[listName] != null) {
				dataSource = data[listName];
			}
			var recordCount = data.recordCount || dataSource.length;

			var isAjaxRequest = (url != undefined); //是否为ajax请求
			var currentPageData = [];

			if (!isAjaxRequest && ispage != "0") {
				var loop = pagesize * pageindex;
				if (loop >= recordCount) loop = recordCount;

				for (var i = (pageindex - 1) * pagesize; i < loop; i++) {
					currentPageData.push(dataSource[i]);
				}
			} else {
				currentPageData = dataSource;
			}

			//替换模板创建到列表
			if ($(">tbody", me).length == 0)
				me.append("<tbody></tbody>");

			//没有数据的显示
			if (isappend == false) {
				if (currentPageData.length == 0) {
					$(">tbody", me).html("<tr><td colspan='" + $("thead th", me).length + "'>暂时没有数据</td></tr>");
					$("tfoot", me).empty().hide();
					return me;
				}
			}
			$("tr:contains('暂时没有数据')", me).remove();
			$(">tbody", me).append(replaceTemplate(me.getTemplate(), currentPageData)); //这里执行数据替换

			//如果分页 创建分页到tfoot
			if (ispage != "0" || $("tfoot", me).length > 0) {
				me.attr("datasource", url);
				if (!isAjaxRequest) {
					dataList[id] = data;
				}
				me.pageHtml(recordCount, pagesize, pageindex);
			}

			me.bindSort(); //排序
			setLink(); //设置其他连接,这里以后还需要修改。
			me.setRowsClass();
			//设置th里的全选为非选中状态
			try {
				$(">thead th input:checkbox", me).attr("checked", false);
			} catch (e) { }
			//执行callback，callback可以是string或Function callback主要用于绑定后的列表处理
			if (callback != undefined) {
				if (typeof (callback) == "function") {
					callback(dataSource, data);
				} else {
					try {
						eval(callback)();
					} catch (e) { }
				}
				if (ispage != "0") {
					callbackList[id] = callback;
				}
			}
		}
		return me;
	};
	//设置各行分色
	$.fn.setRowsClass = function (css1, css2) {
		css1 = css1 || "bg";
		css2 = css2 || "";
		$("tbody tr", this).each(function (i) {
			$(this).removeClass(css1).removeClass(css2);
			$(this).addClass(i % 2 == 0 ? css2 : css1);
		});
	}

	///
	/// 设置分页连接
	///
	$.fn.setPageLink = function (pageCount) {
		var me = this;
		$("tfoot .pagelist a", this).unbind("click").click(function () {
			var thisPageIndex = $(this).attr("href");
			thisPageIndex = thisPageIndex.substring(thisPageIndex.lastIndexOf("#") + 1);
			me.bindTable({ pageindex: thisPageIndex, pageRequest: true });
		});
		$("tfoot .pagelist input:button", this).unbind("click").click(function () {
			var thisPageIndex = $(this).prev().val();
			if (thisPageIndex.length == 0) {
				alert("请输入跳转页码");
				return false;
			}
			thisPageIndex = parseInt(thisPageIndex)
			if (thisPageIndex < 1 || thisPageIndex > parseInt(pageCount)) {
				alert("超出页码范围！");
				return false;
			}
			me.bindTable({ pageindex: thisPageIndex, pageRequest: true });
		});
		$("tfoot .pagelist input:text", this).bind("keypress", function (event) {
			if (event.keyCode == 13) {
				$(this).next().click();
				return false;
			}
		}).limit_number_input("int");
		return me;
	};
	///
	/// 表格分页
	///
	$.fn.pageHtml = function (recordCount, pageSize, pageIndex) {
		pageIndex = parseInt(pageIndex);
		pageSize = parseInt(pageSize);
		recordCount = parseInt(recordCount || 0);
		if (recordCount <= 0)
			return this;
		var pageCount = parseInt(recordCount / pageSize);

		if (recordCount % pageSize > 0)
			pageCount++;
		if (pageIndex > pageCount)
			pageIndex = pageCount;


		var isShowCount = this.attr("isShowCount");
		var isShowPageList = this.attr("isShowPageList");

		var html = "<div class=\"pagelist\">";
		if (isShowCount != "0") {
			html += "<span>共<span class=\"recordcount\">" + recordCount + "</span>条 第" + pageIndex + "页/共" + pageCount + "页,每页" + pageSize + "条</span>";
		}

		if (pageIndex == 1) {
			html += "<span>首页</span>";
			html += "<span>上一页</span>";
		} else {
			html += "<a href=\"#1\">首页</a>";
			html += "<a href=\"#" + (pageIndex - 1) + "\">上一页</a>";
		}
		if (isShowPageList != "0") {

			var listSize = 5; //显示 1 2 3 4
			var loopSize = 5; //循环上限
			var listIndex = 0; //当前list的索引
			if (pageIndex % listSize > 0)
				listIndex = parseInt(pageIndex / listSize);
			else
				listIndex = parseInt(pageIndex / listSize) - 1;

			if (listSize > pageCount)//总页数不到一个页码数
				loopSize = pageCount;
			else
				loopSize = listSize;

			//
			if ((listIndex + 1) * listSize >= pageCount && listSize < pageCount) {
				loopSize = pageCount % listSize;
				if (loopSize == 0)
					loopSize = listSize;
			}

			for (var i = 1; i <= loopSize; i++) {
				var pageText = i + (listIndex * listSize);
				if (pageText == pageIndex)
					html += "<span class='currentPage'>" + pageText + "</span>";
				else
					html += "<a href='#" + pageText + "'>" + pageText + "</a>";
			}
			if (loopSize == listSize && pageCount > listSize) {
				var nextList = (listIndex + 1) * listSize + 1;
				if (nextList <= pageCount)
					html += "<a href=\"#" + ((listIndex + 1) * listSize + 1) + "\">...</a>";
			}
		}
		if (pageCount == pageIndex) {
			html += "<span>下一页</span>";
			html += "<span>末页</span>";
		} else {
			html += "<a href=\"#" + (pageIndex + 1) + "\">下一页</a>";
			html += "<a href=\"#" + pageCount + "\">末页</a>";
		}
		html += "<input type='text' class='txt number' /><input type='button' class='btn' value='跳转' /></div>";

		if ($("tfoot", this).length == 0)
			this.append("<tfoot></tfoot>");

		var hCellCount = 0;
		$("thead tr:first th", this).each(function (i, n) {
			hCellCount++;
			if ($(this).attr("colspan"))
				hCellCount = hCellCount + (parseInt($(this).attr("colspan")) - 1);
		});
		$("tfoot:eq(0)", this).show().html("<tr><td colspan=\"" + hCellCount + "\">" + html + "</td></tr>");

		//翻页需要存储数据到table的属性中
		this.attr("pagesize", pageSize);
		this.attr("ispage", "1");
		this.setPageLink(pageCount); //设置分页的链接
		$("thead th[sort], thead th[sort]", this).css("cursor", "pointer");
		return this;
	};
	///
	/// 表格排序
	///
	$.fn.bindSort = function () {
		var me = this;
		var url = me.attr("datasource");

		if (url == undefined) return me;

		if (url.indexOf('?') == -1)
			url += "?";

		$("tbody tr td", $(this)).removeClass("orderby");
		$("th", me).each(function (_i, _th) {
			var sort = $(this).attr("sort");
			if (!sort) return;
			var desc = $(this).attr("desc") || 0;

			//移除样式
			removeStyle(this, sort, desc);

			//根据当前的url设置默认排序样式
			if (url.indexOf('sort=') > -1 &&
				request.QueryString("sort", url) == sort) {
				desc = request.QueryString("desc", url);
				setStyle(this, sort, desc);
				if (sort) {
					$("tbody tr td:visible:nth-child(" + (_i + 1) + ")", $(this).parents("table:first")).addClass("orderby");
				}
			}

			//设置点击
			$(this).unbind("click").click(function () {
				desc = Math.abs(desc - 1) || 0;
				setStyle(this, sort, desc);
				me.attr("sort", sort);
				me.attr("desc", desc);

				//if($(this).attr("field") != undefined && $(this).attr("field") != "") {
				if ($(this).attr("field")) {
					url = url.replace(/&?(field|_)=([^&]*)/gi, ""); //过滤url中的焦点列参数
					url += "&field=" + $(this).attr("field");
				}
				//刷新Table数据
				me.bindTable(url);
			});
			function sortList() {
				//移除样式
				removeStyle(this, sort, desc);
			}
		});
		function setStyle(th, sort, desc) {
			if ($("span", $(th)).size() <= 0) {
				$(th).wrapInner("<span></span>");
			}
			$(th).addClass("sortable").removeClass("asc desc").addClass(desc == 1 ? "desc" : "asc");
		}
		function removeStyle(th, sort, desc) {
			if ($("span", $(th)).size() <= 0) {
				$(th).wrapInner("<span></span>");
			}
			if (sort != $(me).attr("sort")) {
				$(th).addClass("sortable").removeClass("asc desc");
			}
		}
		return this;
	};


	///
	/// bind form
	///
	$.fn.bindForm = function (model) {
		if (model == undefined || model == null) {
			return;
		}
		var formId = this.attr("id");
		$("input,textarea,select", "#" + formId).each(function () {
			var cname = $(this).attr("name");
			if (cname == "") return true;
			var cid = $(this).attr("id");

			if (cid == "") {
				cid = $(this)[0].tagName + "[name='" + cname + "']";
				$(this).attr("id", cname);
			} else {
				cid = "#" + cid;
			}
			$(cid).bindControl(model[cname.replace("c", "")], formId);
		});
		return this;
	}
	///
	/// bind control
	///
	$.fn.bindControl = function (value, formId) {

		if (value === undefined) return this;

		value = value.toString();
		formId = formId || "";
		if (formId != "")
			formId = "#" + formId + " ";

		switch (this.attr("type")) {
			case "select-one": //DropDownList
				//this[0].selectedIndex = 0;
				//$("option[value='" + value + "']", this).attr("selected");
				var isSelected = false;
				this.children().each(function () {
					if (this.value == value) {
						isSelected = true;
						this.selected = true;
					}
				});
				if (!isSelected)
					this[0].selectedIndex = 0;
				break;
			case "select-multiple": //ListBox
				this.children().each(function () {
					var arr = value.split(',');
					for (var i = 0; i < arr.length; i++) {
						if (this.value == arr[i]) {
							this.selected = true;
						}
					}
				});
				break;
			case "checkbox": //CheckboxList
				//单选
				if (value.indexOf(',') == -1) {
					$(formId + "input[name='" + this.attr("name") + "']").each(function () {
						if (this.value == value) {
							this.checked = true;
							return;
						}
					});
				} else if (this.attr("type") == 'checkbox') {//多选
					var arr = value.split(',');
					for (var i = 0; i < arr.length; i++) {
						$(formId + "input[name='" + this.attr("name") + "']").each(function () {
							if (this.value == arr[i]) {
								this.checked = true;
							}
						});
					}
				}
				break;
			case "radio": //RadioButtonList
				$(formId + " input[name='" + this.attr("name") + "']").each(function () {
					if (this.value == value) {
						this.checked = true;
						return;
					}
				});
				break;
			default: //Normal
				this.val(value);
				break;
		}

		return this;
	}

	//翻页设置复选框状态  arr是存放id的数组
	$.fn.setCheckBox = function (arr) {
		arr = arr || [];
		var me = this;
		var ids = arr.join();
		$("tbody :checkbox", this).unbind("change").change(function () {
			var has = (("," + ids + ",").indexOf("," + this.value + ",") == -1);
			if (this.checked && has) {
				arr.push(this.value);
			} else if (!has) {
				for (var i = 0; i < arr.length; i++) {
					if (arr[i] == this.value) {
						arr.splice(i, 1);
						break;
					}
				};
			}
			ids = arr.join();
		})
		.each(function () {
			$(this).attr("checked", ("," + ids + ",").indexOf("," + this.value + ",") >= 0);
		});
		$("thead input:checkbox", me).attr("checked", false).unbind("click").click(function () {
			selectAll(this);
			$("tbody input:checkbox", me).trigger("change");
		});
		return this;
	}

	$.fn.getTemplate = function () {
		var t = this.html();
		if (t == null)
			return "";
		t = t.match(/<!--([\s\S]*?)-->/);
		if (t != null)
			t = t[1];
		return t;
	}
})(jQuery);

///
/// 替换模板得到数据
///
function replaceTemplate(template, data) {
	if (data == undefined || data.length == 0) return;
	if (template == undefined) return;
	var resultHtml = "";
	for (var i = 0; i < data.length; i++) {
		if (data[i] == undefined) break;
		var rowHtml = template;
		var re = /\{([\w\.\[\]\\"]+)\}/gi;
		while ((fields = re.exec(template)) != null) {
			var re1 = new RegExp("\\{" + fields[1].replace(/[\"\]\[\.]/gi, "[\\\"\\]\\[\\.]") + "\\}", "gi");
			//rowHtml = rowHtml.replace(re1, data[i][fields[1]]);
			rowHtml = rowHtml.replace(re1, eval("data[" + i + "][\"" + fields[1] + "\"]"));
		}
		resultHtml += rowHtml;
	}
	return resultHtml;
}
///
/// 获取指定ID的模板 IE下对个别控件无效(非容器一般无效)
///
function getTemplate(id) {
	var t = $("#" + id).html();
	if (t == null) return "";
	t = t.match(/<!--([\s\S]*?)-->/);
	if (t != null)
		t = t[1];
	return t;
}
