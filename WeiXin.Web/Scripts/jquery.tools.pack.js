﻿/* jQuery 1.2.6 */
(function(){var w=window.jQuery,_$=window.$;var D=window.jQuery=window.$=function(a,b){return new D.fn.init(a,b)};var u=/^[^<]*(<(.|\s)+>)[^>]*$|^#(\w+)$/,isSimple=/^.[^:#\[\.]*$/,undefined;D.fn=D.prototype={init:function(d,b){d=d||document;if(d.nodeType){this[0]=d;this.length=1;return this}if(typeof d=="string"){var c=u.exec(d);if(c&&(c[1]||!b)){if(c[1])d=D.clean([c[1]],b);else{var a=document.getElementById(c[3]);if(a){if(a.id!=c[3])return D().find(d);return D(a)}d=[]}}else return D(b).find(d)}else if(D.isFunction(d))return D(document)[D.fn.ready?"ready":"load"](d);return this.setArray(D.makeArray(d))},jquery:"1.2.6",size:function(){return this.length},length:0,get:function(a){return a==undefined?D.makeArray(this):this[a]},pushStack:function(b){var a=D(b);a.prevObject=this;return a},setArray:function(a){this.length=0;Array.prototype.push.apply(this,a);return this},each:function(a,b){return D.each(this,a,b)},index:function(b){var a=-1;return D.inArray(b&&b.jquery?b[0]:b,this)},attr:function(c,a,b){var d=c;if(c.constructor==String)if(a===undefined)return this[0]&&D[b||"attr"](this[0],c);else{d={};d[c]=a}return this.each(function(i){for(c in d)D.attr(b?this.style:this,c,D.prop(this,d[c],b,i,c))})},css:function(b,a){if((b=='width'||b=='height')&&parseFloat(a)<0)a=undefined;return this.attr(b,a,"curCSS")},text:function(b){if(typeof b!="object"&&b!=null)return this.empty().append((this[0]&&this[0].ownerDocument||document).createTextNode(b));var a="";D.each(b||this,function(){D.each(this.childNodes,function(){if(this.nodeType!=8)a+=this.nodeType!=1?this.nodeValue:D.fn.text([this])})});return a},wrapAll:function(b){if(this[0])D(b,this[0].ownerDocument).clone().insertBefore(this[0]).map(function(){var a=this;while(a.firstChild)a=a.firstChild;return a}).append(this);return this},wrapInner:function(a){return this.each(function(){D(this).contents().wrapAll(a)})},wrap:function(a){return this.each(function(){D(this).wrapAll(a)})},append:function(){return this.domManip(arguments,true,false,function(a){if(this.nodeType==1)this.appendChild(a)})},prepend:function(){return this.domManip(arguments,true,true,function(a){if(this.nodeType==1)this.insertBefore(a,this.firstChild)})},before:function(){return this.domManip(arguments,false,false,function(a){this.parentNode.insertBefore(a,this)})},after:function(){return this.domManip(arguments,false,true,function(a){this.parentNode.insertBefore(a,this.nextSibling)})},end:function(){return this.prevObject||D([])},find:function(b){var c=D.map(this,function(a){return D.find(b,a)});return this.pushStack(/[^+>] [^+>]/.test(b)||b.indexOf("..")>-1?D.unique(c):c)},clone:function(e){var f=this.map(function(){if(D.browser.msie&&!D.isXMLDoc(this)){var a=this.cloneNode(true),container=document.createElement("div");container.appendChild(a);return D.clean([container.innerHTML])[0]}else return this.cloneNode(true)});var d=f.find("*").andSelf().each(function(){if(this[E]!=undefined)this[E]=null});if(e===true)this.find("*").andSelf().each(function(i){if(this.nodeType==3)return;var c=D.data(this,"events");for(var a in c)for(var b in c[a])D.event.add(d[i],a,c[a][b],c[a][b].data)});return f},filter:function(b){return this.pushStack(D.isFunction(b)&&D.grep(this,function(a,i){return b.call(a,i)})||D.multiFilter(b,this))},not:function(b){if(b.constructor==String)if(isSimple.test(b))return this.pushStack(D.multiFilter(b,this,true));else b=D.multiFilter(b,this);var a=b.length&&b[b.length-1]!==undefined&&!b.nodeType;return this.filter(function(){return a?D.inArray(this,b)<0:this!=b})},add:function(a){return this.pushStack(D.unique(D.merge(this.get(),typeof a=='string'?D(a):D.makeArray(a))))},is:function(a){return!!a&&D.multiFilter(a,this).length>0},hasClass:function(a){return this.is("."+a)},val:function(b){if(b==undefined){if(this.length){var c=this[0];if(D.nodeName(c,"select")){var e=c.selectedIndex,values=[],options=c.options,one=c.type=="select-one";if(e<0)return null;for(var i=one?e:0,max=one?e+1:options.length;i<max;i++){var d=options[i];if(d.selected){b=D.browser.msie&&!d.attributes.value.specified?d.text:d.value;if(one)return b;values.push(b)}}return values}else return(this[0].value||"").replace(/\r/g,"")}return undefined}if(b.constructor==Number)b+='';return this.each(function(){if(this.nodeType!=1)return;if(b.constructor==Array&&/radio|checkbox/.test(this.type))this.checked=(D.inArray(this.value,b)>=0||D.inArray(this.name,b)>=0);else if(D.nodeName(this,"select")){var a=D.makeArray(b);D("option",this).each(function(){this.selected=(D.inArray(this.value,a)>=0||D.inArray(this.text,a)>=0)});if(!a.length)this.selectedIndex=-1}else this.value=b})},html:function(a){return a==undefined?(this[0]?this[0].innerHTML:null):this.empty().append(a)},replaceWith:function(a){return this.after(a).remove()},eq:function(i){return this.slice(i,i+1)},slice:function(){return this.pushStack(Array.prototype.slice.apply(this,arguments))},map:function(b){return this.pushStack(D.map(this,function(a,i){return b.call(a,i,a)}))},andSelf:function(){return this.add(this.prevObject)},data:function(d,b){var a=d.split(".");a[1]=a[1]?"."+a[1]:"";if(b===undefined){var c=this.triggerHandler("getData"+a[1]+"!",[a[0]]);if(c===undefined&&this.length)c=D.data(this[0],d);return c===undefined&&a[1]?this.data(a[0]):c}else return this.trigger("setData"+a[1]+"!",[a[0],b]).each(function(){D.data(this,d,b)})},removeData:function(a){return this.each(function(){D.removeData(this,a)})},domManip:function(g,f,h,d){var e=this.length>1,elems;return this.each(function(){if(!elems){elems=D.clean(g,this.ownerDocument);if(h)elems.reverse()}var b=this;if(f&&D.nodeName(this,"table")&&D.nodeName(elems[0],"tr"))b=this.getElementsByTagName("tbody")[0]||this.appendChild(this.ownerDocument.createElement("tbody"));var c=D([]);D.each(elems,function(){var a=e?D(this).clone(true)[0]:this;if(D.nodeName(a,"script"))c=c.add(a);else{if(a.nodeType==1)c=c.add(D("script",a).remove());d.call(b,a)}});c.each(evalScript)})}};D.fn.init.prototype=D.fn;function evalScript(i,a){if(a.src)D.ajax({url:a.src,async:false,dataType:"script"});else D.globalEval(a.text||a.textContent||a.innerHTML||"");if(a.parentNode)a.parentNode.removeChild(a)}function now(){return+new Date}D.extend=D.fn.extend=function(){var b=arguments[0]||{},i=1,length=arguments.length,deep=false,options;if(b.constructor==Boolean){deep=b;b=arguments[1]||{};i=2}if(typeof b!="object"&&typeof b!="function")b={};if(length==i){b=this;--i}for(;i<length;i++)if((options=arguments[i])!=null)for(var c in options){var a=b[c],copy=options[c];if(b===copy)continue;if(deep&&copy&&typeof copy=="object"&&!copy.nodeType)b[c]=D.extend(deep,a||(copy.length!=null?[]:{}),copy);else if(copy!==undefined)b[c]=copy}return b};var E="jQuery"+now(),uuid=0,windowData={},exclude=/z-?index|font-?weight|opacity|zoom|line-?height/i,defaultView=document.defaultView||{};D.extend({noConflict:function(a){window.$=_$;if(a)window.jQuery=w;return D},isFunction:function(a){return!!a&&typeof a!="string"&&!a.nodeName&&a.constructor!=Array&&/^[\s[]?function/.test(a+"")},isXMLDoc:function(a){return a.documentElement&&!a.body||a.tagName&&a.ownerDocument&&!a.ownerDocument.body},globalEval:function(a){a=D.trim(a);if(a){var b=document.getElementsByTagName("head")[0]||document.documentElement,script=document.createElement("script");script.type="text/javascript";if(D.browser.msie)script.text=a;else script.appendChild(document.createTextNode(a));b.insertBefore(script,b.firstChild);b.removeChild(script)}},nodeName:function(b,a){return b.nodeName&&b.nodeName.toUpperCase()==a.toUpperCase()},cache:{},data:function(c,d,b){c=c==window?windowData:c;var a=c[E];if(!a)a=c[E]=++uuid;if(d&&!D.cache[a])D.cache[a]={};if(b!==undefined)D.cache[a][d]=b;return d?D.cache[a][d]:a},removeData:function(c,b){c=c==window?windowData:c;var a=c[E];if(b){if(D.cache[a]){delete D.cache[a][b];b="";for(b in D.cache[a])break;if(!b)D.removeData(c)}}else{try{delete c[E]}catch(e){if(c.removeAttribute)c.removeAttribute(E)}delete D.cache[a]}},each:function(d,a,c){var e,i=0,length=d.length;if(c){if(length==undefined){for(e in d)if(a.apply(d[e],c)===false)break}else for(;i<length;)if(a.apply(d[i++],c)===false)break}else{if(length==undefined){for(e in d)if(a.call(d[e],e,d[e])===false)break}else for(var b=d[0];i<length&&a.call(b,i,b)!==false;b=d[++i]){}}return d},prop:function(b,a,c,i,d){if(D.isFunction(a))a=a.call(b,i);return a&&a.constructor==Number&&c=="curCSS"&&!exclude.test(d)?a+"px":a},className:{add:function(c,b){D.each((b||"").split(/\s+/),function(i,a){if(c.nodeType==1&&!D.className.has(c.className,a))c.className+=(c.className?" ":"")+a})},remove:function(c,b){if(c.nodeType==1)c.className=b!=undefined?D.grep(c.className.split(/\s+/),function(a){return!D.className.has(b,a)}).join(" "):""},has:function(b,a){return D.inArray(a,(b.className||b).toString().split(/\s+/))>-1}},swap:function(b,c,a){var e={};for(var d in c){e[d]=b.style[d];b.style[d]=c[d]}a.call(b);for(var d in c)b.style[d]=e[d]},css:function(d,e,c){if(e=="width"||e=="height"){var b,props={position:"absolute",visibility:"hidden",display:"block"},which=e=="width"?["Left","Right"]:["Top","Bottom"];function getWH(){b=e=="width"?d.offsetWidth:d.offsetHeight;var a=0,border=0;D.each(which,function(){a+=parseFloat(D.curCSS(d,"padding"+this,true))||0;border+=parseFloat(D.curCSS(d,"border"+this+"Width",true))||0});b-=Math.round(a+border)}if(D(d).is(":visible"))getWH();else D.swap(d,props,getWH);return Math.max(0,b)}return D.curCSS(d,e,c)},curCSS:function(f,l,k){var e,style=f.style;function color(b){if(!D.browser.safari)return false;var a=defaultView.getComputedStyle(b,null);return!a||a.getPropertyValue("color")==""}if(l=="opacity"&&D.browser.msie){e=D.attr(style,"opacity");return e==""?"1":e}if(D.browser.opera&&l=="display"){var d=style.outline;style.outline="0 solid black";style.outline=d}if(l.match(/float/i))l=y;if(!k&&style&&style[l])e=style[l];else if(defaultView.getComputedStyle){if(l.match(/float/i))l="float";l=l.replace(/([A-Z])/g,"-$1").toLowerCase();var c=defaultView.getComputedStyle(f,null);if(c&&!color(f))e=c.getPropertyValue(l);else{var g=[],stack=[],a=f,i=0;for(;a&&color(a);a=a.parentNode)stack.unshift(a);for(;i<stack.length;i++)if(color(stack[i])){g[i]=stack[i].style.display;stack[i].style.display="block"}e=l=="display"&&g[stack.length-1]!=null?"none":(c&&c.getPropertyValue(l))||"";for(i=0;i<g.length;i++)if(g[i]!=null)stack[i].style.display=g[i]}if(l=="opacity"&&e=="")e="1"}else if(f.currentStyle){var h=l.replace(/\-(\w)/g,function(a,b){return b.toUpperCase()});e=f.currentStyle[l]||f.currentStyle[h];if(!/^\d+(px)?$/i.test(e)&&/^\d/.test(e)){var j=style.left,rsLeft=f.runtimeStyle.left;f.runtimeStyle.left=f.currentStyle.left;style.left=e||0;e=style.pixelLeft+"px";style.left=j;f.runtimeStyle.left=rsLeft}}return e},clean:function(l,h){var k=[];h=h||document;if(typeof h.createElement=='undefined')h=h.ownerDocument||h[0]&&h[0].ownerDocument||document;D.each(l,function(i,d){if(!d)return;if(d.constructor==Number)d+='';if(typeof d=="string"){d=d.replace(/(<(\w+)[^>]*?)\/>/g,function(b,a,c){return c.match(/^(abbr|br|col|img|input|link|meta|param|hr|area|embed)$/i)?b:a+"></"+c+">"});var f=D.trim(d).toLowerCase(),div=h.createElement("div");var e=!f.indexOf("<opt")&&[1,"<select multiple='multiple'>","</select>"]||!f.indexOf("<leg")&&[1,"<fieldset>","</fieldset>"]||f.match(/^<(thead|tbody|tfoot|colg|cap)/)&&[1,"<table>","</table>"]||!f.indexOf("<tr")&&[2,"<table><tbody>","</tbody></table>"]||(!f.indexOf("<td")||!f.indexOf("<th"))&&[3,"<table><tbody><tr>","</tr></tbody></table>"]||!f.indexOf("<col")&&[2,"<table><tbody></tbody><colgroup>","</colgroup></table>"]||D.browser.msie&&[1,"div<div>","</div>"]||[0,"",""];div.innerHTML=e[1]+d+e[2];while(e[0]--)div=div.lastChild;if(D.browser.msie){var g=!f.indexOf("<table")&&f.indexOf("<tbody")<0?div.firstChild&&div.firstChild.childNodes:e[1]=="<table>"&&f.indexOf("<tbody")<0?div.childNodes:[];for(var j=g.length-1;j>=0;--j)if(D.nodeName(g[j],"tbody")&&!g[j].childNodes.length)g[j].parentNode.removeChild(g[j]);if(/^\s/.test(d))div.insertBefore(h.createTextNode(d.match(/^\s*/)[0]),div.firstChild)}d=D.makeArray(div.childNodes)}if(d.length===0&&(!D.nodeName(d,"form")&&!D.nodeName(d,"select")))return;if(d[0]==undefined||D.nodeName(d,"form")||d.options)k.push(d);else k=D.merge(k,d)});return k},attr:function(d,f,c){if(!d||d.nodeType==3||d.nodeType==8)return undefined;var e=!D.isXMLDoc(d),set=c!==undefined,msie=D.browser.msie;f=e&&D.props[f]||f;if(d.tagName){var g=/href|src|style/.test(f);if(f=="selected"&&D.browser.safari)d.parentNode.selectedIndex;if(f in d&&e&&!g){if(set){if(f=="type"&&D.nodeName(d,"input")&&d.parentNode)throw"type property can't be changed";d[f]=c}if(D.nodeName(d,"form")&&d.getAttributeNode(f))return d.getAttributeNode(f).nodeValue;return d[f]}if(msie&&e&&f=="style")return D.attr(d.style,"cssText",c);if(set)d.setAttribute(f,""+c);var h=msie&&e&&g?d.getAttribute(f,2):d.getAttribute(f);return h===null?undefined:h}if(msie&&f=="opacity"){if(set){d.zoom=1;d.filter=(d.filter||"").replace(/alpha\([^)]*\)/,"")+(parseInt(c)+''=="NaN"?"":"alpha(opacity="+c*100+")")}return d.filter&&d.filter.indexOf("opacity=")>=0?(parseFloat(d.filter.match(/opacity=([^)]*)/)[1])/100)+'':""}f=f.replace(/-([a-z])/ig,function(a,b){return b.toUpperCase()});if(set)d[f]=c;return d[f]},trim:function(a){return(a||"").replace(/^\s+|\s+$/g,"")},makeArray:function(b){var a=[];if(b!=null){var i=b.length;if(i==null||b.split||b.setInterval||b.call)a[0]=b;else while(i)a[--i]=b[i]}return a},inArray:function(b,a){for(var i=0,length=a.length;i<length;i++)if(a[i]===b)return i;return-1},merge:function(a,b){var i=0,elem,pos=a.length;if(D.browser.msie){while(elem=b[i++])if(elem.nodeType!=8)a[pos++]=elem}else while(elem=b[i++])a[pos++]=elem;return a},unique:function(a){var c=[],done={};try{for(var i=0,length=a.length;i<length;i++){var b=D.data(a[i]);if(!done[b]){done[b]=true;c.push(a[i])}}}catch(e){c=a}return c},grep:function(c,a,d){var b=[];for(var i=0,length=c.length;i<length;i++)if(!d!=!a(c[i],i))b.push(c[i]);return b},map:function(d,a){var c=[];for(var i=0,length=d.length;i<length;i++){var b=a(d[i],i);if(b!=null)c[c.length]=b}return c.concat.apply([],c)}});var v=navigator.userAgent.toLowerCase();D.browser={version:(v.match(/.+(?:rv|it|ra|ie)[\/: ]([\d.]+)/)||[])[1],safari:/webkit/.test(v),opera:/opera/.test(v),msie:/msie/.test(v)&&!/opera/.test(v),mozilla:/mozilla/.test(v)&&!/(compatible|webkit)/.test(v)};var y=D.browser.msie?"styleFloat":"cssFloat";D.extend({boxModel:!D.browser.msie||document.compatMode=="CSS1Compat",props:{"for":"htmlFor","class":"className","float":y,cssFloat:y,styleFloat:y,readonly:"readOnly",maxlength:"maxLength",cellspacing:"cellSpacing"}});D.each({parent:function(a){return a.parentNode},parents:function(a){return D.dir(a,"parentNode")},next:function(a){return D.nth(a,2,"nextSibling")},prev:function(a){return D.nth(a,2,"previousSibling")},nextAll:function(a){return D.dir(a,"nextSibling")},prevAll:function(a){return D.dir(a,"previousSibling")},siblings:function(a){return D.sibling(a.parentNode.firstChild,a)},children:function(a){return D.sibling(a.firstChild)},contents:function(a){return D.nodeName(a,"iframe")?a.contentDocument||a.contentWindow.document:D.makeArray(a.childNodes)}},function(c,d){D.fn[c]=function(b){var a=D.map(this,d);if(b&&typeof b=="string")a=D.multiFilter(b,a);return this.pushStack(D.unique(a))}});D.each({appendTo:"append",prependTo:"prepend",insertBefore:"before",insertAfter:"after",replaceAll:"replaceWith"},function(c,b){D.fn[c]=function(){var a=arguments;return this.each(function(){for(var i=0,length=a.length;i<length;i++)D(a[i])[b](this)})}});D.each({removeAttr:function(a){D.attr(this,a,"");if(this.nodeType==1)this.removeAttribute(a)},addClass:function(a){D.className.add(this,a)},removeClass:function(a){D.className.remove(this,a)},toggleClass:function(a){D.className[D.className.has(this,a)?"remove":"add"](this,a)},remove:function(a){if(!a||D.filter(a,[this]).r.length){D("*",this).add(this).each(function(){D.event.remove(this);D.removeData(this)});if(this.parentNode)this.parentNode.removeChild(this)}},empty:function(){D(">*",this).remove();while(this.firstChild)this.removeChild(this.firstChild)}},function(a,b){D.fn[a]=function(){return this.each(b,arguments)}});D.each(["Height","Width"],function(i,c){var b=c.toLowerCase();D.fn[b]=function(a){return this[0]==window?D.browser.opera&&document.body["client"+c]||D.browser.safari&&window["inner"+c]||document.compatMode=="CSS1Compat"&&document.documentElement["client"+c]||document.body["client"+c]:this[0]==document?Math.max(Math.max(document.body["scroll"+c],document.documentElement["scroll"+c]),Math.max(document.body["offset"+c],document.documentElement["offset"+c])):a==undefined?(this.length?D.css(this[0],b):null):this.css(b,a.constructor==String?a:a+"px")}});function num(a,b){return a[0]&&parseInt(D.curCSS(a[0],b,true),10)||0}var C=D.browser.safari&&parseInt(D.browser.version)<417?"(?:[\\w*_-]|\\\\.)":"(?:[\\w\u0128-\uFFFF*_-]|\\\\.)",quickChild=new RegExp("^>\\s*("+C+"+)"),quickID=new RegExp("^("+C+"+)(#)("+C+"+)"),quickClass=new RegExp("^([#.]?)("+C+"*)");D.extend({expr:{"":function(a,i,m){return m[2]=="*"||D.nodeName(a,m[2])},"#":function(a,i,m){return a.getAttribute("id")==m[2]},":":{lt:function(a,i,m){return i<m[3]-0},gt:function(a,i,m){return i>m[3]-0},nth:function(a,i,m){return m[3]-0==i},eq:function(a,i,m){return m[3]-0==i},first:function(a,i){return i==0},last:function(a,i,m,r){return i==r.length-1},even:function(a,i){return i%2==0},odd:function(a,i){return i%2},"first-child":function(a){return a.parentNode.getElementsByTagName("*")[0]==a},"last-child":function(a){return D.nth(a.parentNode.lastChild,1,"previousSibling")==a},"only-child":function(a){return!D.nth(a.parentNode.lastChild,2,"previousSibling")},parent:function(a){return a.firstChild},empty:function(a){return!a.firstChild},contains:function(a,i,m){return(a.textContent||a.innerText||D(a).text()||"").indexOf(m[3])>=0},visible:function(a){return"hidden"!=a.type&&D.css(a,"display")!="none"&&D.css(a,"visibility")!="hidden"},hidden:function(a){return"hidden"==a.type||D.css(a,"display")=="none"||D.css(a,"visibility")=="hidden"},enabled:function(a){return!a.disabled},disabled:function(a){return a.disabled},checked:function(a){return a.checked},selected:function(a){return a.selected||D.attr(a,"selected")},text:function(a){return"text"==a.type},radio:function(a){return"radio"==a.type},checkbox:function(a){return"checkbox"==a.type},file:function(a){return"file"==a.type},password:function(a){return"password"==a.type},submit:function(a){return"submit"==a.type},image:function(a){return"image"==a.type},reset:function(a){return"reset"==a.type},button:function(a){return"button"==a.type||D.nodeName(a,"button")},input:function(a){return/input|select|textarea|button/i.test(a.nodeName)},has:function(a,i,m){return D.find(m[3],a).length},header:function(a){return/h\d/i.test(a.nodeName)},animated:function(a){return D.grep(D.timers,function(b){return a==b.elem}).length}}},parse:[/^(\[) *@?([\w-]+) *([!*$^~=]*) *('?"?)(.*?)\4 *\]/,/^(:)([\w-]+)\("?'?(.*?(\(.*?\))?[^(]*?)"?'?\)/,new RegExp("^([:.#]*)("+C+"+)")],multiFilter:function(a,c,b){var d,cur=[];while(a&&a!=d){d=a;var f=D.filter(a,c,b);a=f.t.replace(/^\s*,\s*/,"");cur=b?c=f.r:D.merge(cur,f.r)}return cur},find:function(t,o){if(typeof t!="string")return[t];if(o&&o.nodeType!=1&&o.nodeType!=9)return[];o=o||document;var d=[o],done=[],last,nodeName;while(t&&last!=t){var r=[];last=t;t=D.trim(t);var l=false,re=quickChild,m=re.exec(t);if(m){nodeName=m[1].toUpperCase();for(var i=0;d[i];i++)for(var c=d[i].firstChild;c;c=c.nextSibling)if(c.nodeType==1&&(nodeName=="*"||c.nodeName.toUpperCase()==nodeName))r.push(c);d=r;t=t.replace(re,"");if(t.indexOf(" ")==0)continue;l=true}else{re=/^([>+~])\s*(\w*)/i;if((m=re.exec(t))!=null){r=[];var k={};nodeName=m[2].toUpperCase();m=m[1];for(var j=0,rl=d.length;j<rl;j++){var n=m=="~"||m=="+"?d[j].nextSibling:d[j].firstChild;for(;n;n=n.nextSibling)if(n.nodeType==1){var g=D.data(n);if(m=="~"&&k[g])break;if(!nodeName||n.nodeName.toUpperCase()==nodeName){if(m=="~")k[g]=true;r.push(n)}if(m=="+")break}}d=r;t=D.trim(t.replace(re,""));l=true}}if(t&&!l){if(!t.indexOf(",")){if(o==d[0])d.shift();done=D.merge(done,d);r=d=[o];t=" "+t.substr(1,t.length)}else{var h=quickID;var m=h.exec(t);if(m){m=[0,m[2],m[3],m[1]]}else{h=quickClass;m=h.exec(t)}m[2]=m[2].replace(/\\/g,"");var f=d[d.length-1];if(m[1]=="#"&&f&&f.getElementById&&!D.isXMLDoc(f)){var p=f.getElementById(m[2]);if((D.browser.msie||D.browser.opera)&&p&&typeof p.id=="string"&&p.id!=m[2])p=D('[@id="'+m[2]+'"]',f)[0];d=r=p&&(!m[3]||D.nodeName(p,m[3]))?[p]:[]}else{for(var i=0;d[i];i++){var a=m[1]=="#"&&m[3]?m[3]:m[1]!=""||m[0]==""?"*":m[2];if(a=="*"&&d[i].nodeName.toLowerCase()=="object")a="param";r=D.merge(r,d[i].getElementsByTagName(a))}if(m[1]==".")r=D.classFilter(r,m[2]);if(m[1]=="#"){var e=[];for(var i=0;r[i];i++)if(r[i].getAttribute("id")==m[2]){e=[r[i]];break}r=e}d=r}t=t.replace(h,"")}}if(t){var b=D.filter(t,r);d=r=b.r;t=D.trim(b.t)}}if(t)d=[];if(d&&o==d[0])d.shift();done=D.merge(done,d);return done},classFilter:function(r,m,a){m=" "+m+" ";var c=[];for(var i=0;r[i];i++){var b=(" "+r[i].className+" ").indexOf(m)>=0;if(!a&&b||a&&!b)c.push(r[i])}return c},filter:function(t,r,h){var d;while(t&&t!=d){d=t;var p=D.parse,m;for(var i=0;p[i];i++){m=p[i].exec(t);if(m){t=t.substring(m[0].length);m[2]=m[2].replace(/\\/g,"");break}}if(!m)break;if(m[1]==":"&&m[2]=="not")r=isSimple.test(m[3])?D.filter(m[3],r,true).r:D(r).not(m[3]);else if(m[1]==".")r=D.classFilter(r,m[2],h);else if(m[1]=="["){var g=[],type=m[3];for(var i=0,rl=r.length;i<rl;i++){var a=r[i],z=a[D.props[m[2]]||m[2]];if(z==null||/href|src|selected/.test(m[2]))z=D.attr(a,m[2])||'';if((type==""&&!!z||type=="="&&z==m[5]||type=="!="&&z!=m[5]||type=="^="&&z&&!z.indexOf(m[5])||type=="$="&&z.substr(z.length-m[5].length)==m[5]||(type=="*="||type=="~=")&&z.indexOf(m[5])>=0)^h)g.push(a)}r=g}else if(m[1]==":"&&m[2]=="nth-child"){var e={},g=[],test=/(-?)(\d*)n((?:\+|-)?\d*)/.exec(m[3]=="even"&&"2n"||m[3]=="odd"&&"2n+1"||!/\D/.test(m[3])&&"0n+"+m[3]||m[3]),first=(test[1]+(test[2]||1))-0,d=test[3]-0;for(var i=0,rl=r.length;i<rl;i++){var j=r[i],parentNode=j.parentNode,id=D.data(parentNode);if(!e[id]){var c=1;for(var n=parentNode.firstChild;n;n=n.nextSibling)if(n.nodeType==1)n.nodeIndex=c++;e[id]=true}var b=false;if(first==0){if(j.nodeIndex==d)b=true}else if((j.nodeIndex-d)%first==0&&(j.nodeIndex-d)/first>=0)b=true;if(b^h)g.push(j)}r=g}else{var f=D.expr[m[1]];if(typeof f=="object")f=f[m[2]];if(typeof f=="string")f=eval("false||function(a,i){return "+f+";}");r=D.grep(r,function(a,i){return f(a,i,m,r)},h)}}return{r:r,t:t}},dir:function(b,c){var a=[],cur=b[c];while(cur&&cur!=document){if(cur.nodeType==1)a.push(cur);cur=cur[c]}return a},nth:function(a,e,c,b){e=e||1;var d=0;for(;a;a=a[c])if(a.nodeType==1&&++d==e)break;return a},sibling:function(n,a){var r=[];for(;n;n=n.nextSibling){if(n.nodeType==1&&n!=a)r.push(n)}return r}});D.event={add:function(f,i,g,e){if(f.nodeType==3||f.nodeType==8)return;if(D.browser.msie&&f.setInterval)f=window;if(!g.guid)g.guid=this.guid++;if(e!=undefined){var h=g;g=this.proxy(h,function(){return h.apply(this,arguments)});g.data=e}var j=D.data(f,"events")||D.data(f,"events",{}),handle=D.data(f,"handle")||D.data(f,"handle",function(){if(typeof D!="undefined"&&!D.event.triggered)return D.event.handle.apply(arguments.callee.elem,arguments)});handle.elem=f;D.each(i.split(/\s+/),function(c,b){var a=b.split(".");b=a[0];g.type=a[1];var d=j[b];if(!d){d=j[b]={};if(!D.event.special[b]||D.event.special[b].setup.call(f)===false){if(f.addEventListener)f.addEventListener(b,handle,false);else if(f.attachEvent)f.attachEvent("on"+b,handle)}}d[g.guid]=g;D.event.global[b]=true});f=null},guid:1,global:{},remove:function(e,h,f){if(e.nodeType==3||e.nodeType==8)return;var i=D.data(e,"events"),ret,index;if(i){if(h==undefined||(typeof h=="string"&&h.charAt(0)=="."))for(var g in i)this.remove(e,g+(h||""));else{if(h.type){f=h.handler;h=h.type}D.each(h.split(/\s+/),function(b,a){var c=a.split(".");a=c[0];if(i[a]){if(f)delete i[a][f.guid];else for(f in i[a])if(!c[1]||i[a][f].type==c[1])delete i[a][f];for(ret in i[a])break;if(!ret){if(!D.event.special[a]||D.event.special[a].teardown.call(e)===false){if(e.removeEventListener)e.removeEventListener(a,D.data(e,"handle"),false);else if(e.detachEvent)e.detachEvent("on"+a,D.data(e,"handle"))}ret=null;delete i[a]}}})}for(ret in i)break;if(!ret){var d=D.data(e,"handle");if(d)d.elem=null;D.removeData(e,"events");D.removeData(e,"handle")}}},trigger:function(h,c,f,g,i){c=D.makeArray(c);if(h.indexOf("!")>=0){h=h.slice(0,-1);var a=true}if(!f){if(this.global[h])D("*").add([window,document]).trigger(h,c)}else{if(f.nodeType==3||f.nodeType==8)return undefined;var b,ret,fn=D.isFunction(f[h]||null),event=!c[0]||!c[0].preventDefault;if(event){c.unshift({type:h,target:f,preventDefault:function(){},stopPropagation:function(){},timeStamp:now()});c[0][E]=true}c[0].type=h;if(a)c[0].exclusive=true;var d=D.data(f,"handle");if(d)b=d.apply(f,c);if((!fn||(D.nodeName(f,'a')&&h=="click"))&&f["on"+h]&&f["on"+h].apply(f,c)===false)b=false;if(event)c.shift();if(i&&D.isFunction(i)){ret=i.apply(f,b==null?c:c.concat(b));if(ret!==undefined)b=ret}if(fn&&g!==false&&b!==false&&!(D.nodeName(f,'a')&&h=="click")){this.triggered=true;try{f[h]()}catch(e){}}this.triggered=false}return b},handle:function(b){var a,ret,namespace,all,handlers;b=arguments[0]=D.event.fix(b||window.event);namespace=b.type.split(".");b.type=namespace[0];namespace=namespace[1];all=!namespace&&!b.exclusive;handlers=(D.data(this,"events")||{})[b.type];for(var j in handlers){var c=handlers[j];if(all||c.type==namespace){b.handler=c;b.data=c.data;ret=c.apply(this,arguments);if(a!==false)a=ret;if(ret===false){b.preventDefault();b.stopPropagation()}}}return a},fix:function(b){if(b[E]==true)return b;var d=b;b={originalEvent:d};var c="altKey attrChange attrName bubbles button cancelable charCode clientX clientY ctrlKey currentTarget data detail eventPhase fromElement handler keyCode metaKey newValue originalTarget pageX pageY prevValue relatedNode relatedTarget screenX screenY shiftKey srcElement target timeStamp toElement type view wheelDelta which".split(" ");for(var i=c.length;i;i--)b[c[i]]=d[c[i]];b[E]=true;b.preventDefault=function(){if(d.preventDefault)d.preventDefault();d.returnValue=false};b.stopPropagation=function(){if(d.stopPropagation)d.stopPropagation();d.cancelBubble=true};b.timeStamp=b.timeStamp||now();if(!b.target)b.target=b.srcElement||document;if(b.target.nodeType==3)b.target=b.target.parentNode;if(!b.relatedTarget&&b.fromElement)b.relatedTarget=b.fromElement==b.target?b.toElement:b.fromElement;if(b.pageX==null&&b.clientX!=null){var a=document.documentElement,body=document.body;b.pageX=b.clientX+(a&&a.scrollLeft||body&&body.scrollLeft||0)-(a.clientLeft||0);b.pageY=b.clientY+(a&&a.scrollTop||body&&body.scrollTop||0)-(a.clientTop||0)}if(!b.which&&((b.charCode||b.charCode===0)?b.charCode:b.keyCode))b.which=b.charCode||b.keyCode;if(!b.metaKey&&b.ctrlKey)b.metaKey=b.ctrlKey;if(!b.which&&b.button)b.which=(b.button&1?1:(b.button&2?3:(b.button&4?2:0)));return b},proxy:function(a,b){b.guid=a.guid=a.guid||b.guid||this.guid++;return b},special:{ready:{setup:function(){bindReady();return},teardown:function(){return}},mouseenter:{setup:function(){if(D.browser.msie)return false;D(this).bind("mouseover",D.event.special.mouseenter.handler);return true},teardown:function(){if(D.browser.msie)return false;D(this).unbind("mouseover",D.event.special.mouseenter.handler);return true},handler:function(a){if(F(a,this))return true;a.type="mouseenter";return D.event.handle.apply(this,arguments)}},mouseleave:{setup:function(){if(D.browser.msie)return false;D(this).bind("mouseout",D.event.special.mouseleave.handler);return true},teardown:function(){if(D.browser.msie)return false;D(this).unbind("mouseout",D.event.special.mouseleave.handler);return true},handler:function(a){if(F(a,this))return true;a.type="mouseleave";return D.event.handle.apply(this,arguments)}}}};D.fn.extend({bind:function(c,a,b){return c=="unload"?this.one(c,a,b):this.each(function(){D.event.add(this,c,b||a,b&&a)})},one:function(d,b,c){var e=D.event.proxy(c||b,function(a){D(this).unbind(a,e);return(c||b).apply(this,arguments)});return this.each(function(){D.event.add(this,d,e,c&&b)})},unbind:function(a,b){return this.each(function(){D.event.remove(this,a,b)})},trigger:function(c,a,b){return this.each(function(){D.event.trigger(c,a,this,true,b)})},triggerHandler:function(c,a,b){return this[0]&&D.event.trigger(c,a,this[0],false,b)},toggle:function(b){var c=arguments,i=1;while(i<c.length)D.event.proxy(b,c[i++]);return this.click(D.event.proxy(b,function(a){this.lastToggle=(this.lastToggle||0)%i;a.preventDefault();return c[this.lastToggle++].apply(this,arguments)||false}))},hover:function(a,b){return this.bind('mouseenter',a).bind('mouseleave',b)},ready:function(a){bindReady();if(D.isReady)a.call(document,D);else D.readyList.push(function(){return a.call(this,D)});return this}});D.extend({isReady:false,readyList:[],ready:function(){if(!D.isReady){D.isReady=true;if(D.readyList){D.each(D.readyList,function(){this.call(document)});D.readyList=null}D(document).triggerHandler("ready")}}});var x=false;function bindReady(){if(x)return;x=true;if(document.addEventListener&&!D.browser.opera)document.addEventListener("DOMContentLoaded",D.ready,false);if(D.browser.msie&&window==top)(function(){if(D.isReady)return;try{document.documentElement.doScroll("left")}catch(error){setTimeout(arguments.callee,0);return}D.ready()})();if(D.browser.opera)document.addEventListener("DOMContentLoaded",function(){if(D.isReady)return;for(var i=0;i<document.styleSheets.length;i++)if(document.styleSheets[i].disabled){setTimeout(arguments.callee,0);return}D.ready()},false);if(D.browser.safari){var a;(function(){if(D.isReady)return;if(document.readyState!="loaded"&&document.readyState!="complete"){setTimeout(arguments.callee,0);return}if(a===undefined)a=D("style, link[rel=stylesheet]").length;if(document.styleSheets.length!=a){setTimeout(arguments.callee,0);return}D.ready()})()}D.event.add(window,"load",D.ready)}D.each(("blur,focus,load,resize,scroll,unload,click,dblclick,"+"mousedown,mouseup,mousemove,mouseover,mouseout,change,select,"+"submit,keydown,keypress,keyup,error").split(","),function(i,b){D.fn[b]=function(a){return a?this.bind(b,a):this.trigger(b)}});var F=function(a,c){var b=a.relatedTarget;while(b&&b!=c)try{b=b.parentNode}catch(error){b=c}return b==c};D(window).bind("unload",function(){D("*").add(document).unbind()});D.fn.extend({_load:D.fn.load,load:function(g,d,c){if(typeof g!='string')return this._load(g);var e=g.indexOf(" ");if(e>=0){var i=g.slice(e,g.length);g=g.slice(0,e)}c=c||function(){};var f="GET";if(d)if(D.isFunction(d)){c=d;d=null}else{d=D.param(d);f="POST"}var h=this;D.ajax({url:g,type:f,dataType:"html",data:d,complete:function(a,b){if(b=="success"||b=="notmodified")h.html(i?D("<div/>").append(a.responseText.replace(/<script(.|\s)*?\/script>/g,"")).find(i):a.responseText);h.each(c,[a.responseText,b,a])}});return this},serialize:function(){return D.param(this.serializeArray())},serializeArray:function(){return this.map(function(){return D.nodeName(this,"form")?D.makeArray(this.elements):this}).filter(function(){return this.name&&!this.disabled&&(this.checked||/select|textarea/i.test(this.nodeName)||/text|hidden|password/i.test(this.type))}).map(function(i,c){var b=D(this).val();return b==null?null:b.constructor==Array?D.map(b,function(a,i){return{name:c.name,value:a}}):{name:c.name,value:b}}).get()}});D.each("ajaxStart,ajaxStop,ajaxComplete,ajaxError,ajaxSuccess,ajaxSend".split(","),function(i,o){D.fn[o]=function(f){return this.bind(o,f)}});var B=now();D.extend({get:function(d,b,a,c){if(D.isFunction(b)){a=b;b=null}return D.ajax({type:"GET",url:d,data:b,success:a,dataType:c})},getScript:function(b,a){return D.get(b,null,a,"script")},getJSON:function(c,b,a){return D.get(c,b,a,"json")},post:function(d,b,a,c){if(D.isFunction(b)){a=b;b={}}return D.ajax({type:"POST",url:d,data:b,success:a,dataType:c})},ajaxSetup:function(a){D.extend(D.ajaxSettings,a)},ajaxSettings:{url:location.href,global:true,type:"GET",timeout:0,contentType:"application/x-www-form-urlencoded",processData:true,async:true,data:null,username:null,password:null,accepts:{xml:"application/xml, text/xml",html:"text/html",script:"text/javascript, application/javascript",json:"application/json, text/javascript",text:"text/plain",_default:"*/*"}},lastModified:{},ajax:function(s){s=D.extend(true,s,D.extend(true,{},D.ajaxSettings,s));var g,jsre=/=\?(&|$)/g,status,data,type=s.type.toUpperCase();if(s.data&&s.processData&&typeof s.data!="string")s.data=D.param(s.data);if(s.dataType=="jsonp"){if(type=="GET"){if(!s.url.match(jsre))s.url+=(s.url.match(/\?/)?"&":"?")+(s.jsonp||"callback")+"=?"}else if(!s.data||!s.data.match(jsre))s.data=(s.data?s.data+"&":"")+(s.jsonp||"callback")+"=?";s.dataType="json"}if(s.dataType=="json"&&(s.data&&s.data.match(jsre)||s.url.match(jsre))){g="jsonp"+B++;if(s.data)s.data=(s.data+"").replace(jsre,"="+g+"$1");s.url=s.url.replace(jsre,"="+g+"$1");s.dataType="script";window[g]=function(a){data=a;success();complete();window[g]=undefined;try{delete window[g]}catch(e){}if(i)i.removeChild(h)}}if(s.dataType=="script"&&s.cache==null)s.cache=false;if(s.cache===false&&type=="GET"){var j=now();var k=s.url.replace(/(\?|&)_=.*?(&|$)/,"$1_="+j+"$2");s.url=k+((k==s.url)?(s.url.match(/\?/)?"&":"?")+"_="+j:"")}if(s.data&&type=="GET"){s.url+=(s.url.match(/\?/)?"&":"?")+s.data;s.data=null}if(s.global&&!D.active++)D.event.trigger("ajaxStart");var n=/^(?:\w+:)?\/\/([^\/?#]+)/;if(s.dataType=="script"&&type=="GET"&&n.test(s.url)&&n.exec(s.url)[1]!=location.host){var i=document.getElementsByTagName("head")[0];var h=document.createElement("script");h.src=s.url;if(s.scriptCharset)h.charset=s.scriptCharset;if(!g){var l=false;h.onload=h.onreadystatechange=function(){if(!l&&(!this.readyState||this.readyState=="loaded"||this.readyState=="complete")){l=true;success();complete();i.removeChild(h)}}}i.appendChild(h);return undefined}var m=false;var c=window.ActiveXObject?new ActiveXObject("Microsoft.XMLHTTP"):new XMLHttpRequest();if(s.username)c.open(type,s.url,s.async,s.username,s.password);else c.open(type,s.url,s.async);try{if(s.data)c.setRequestHeader("Content-Type",s.contentType);if(s.ifModified)c.setRequestHeader("If-Modified-Since",D.lastModified[s.url]||"Thu, 01 Jan 1970 00:00:00 GMT");c.setRequestHeader("X-Requested-With","XMLHttpRequest");c.setRequestHeader("Accept",s.dataType&&s.accepts[s.dataType]?s.accepts[s.dataType]+", */*":s.accepts._default)}catch(e){}if(s.beforeSend&&s.beforeSend(c,s)===false){s.global&&D.active--;c.abort();return false}if(s.global)D.event.trigger("ajaxSend",[c,s]);var d=function(a){if(!m&&c&&(c.readyState==4||a=="timeout")){m=true;if(f){clearInterval(f);f=null}status=a=="timeout"&&"timeout"||!D.httpSuccess(c)&&"error"||s.ifModified&&D.httpNotModified(c,s.url)&&"notmodified"||"success";if(status=="success"){try{data=D.httpData(c,s.dataType,s.dataFilter)}catch(e){status="parsererror"}}if(status=="success"){var b;try{b=c.getResponseHeader("Last-Modified")}catch(e){}if(s.ifModified&&b)D.lastModified[s.url]=b;if(!g)success()}else D.handleError(s,c,status);complete();if(s.async)c=null}};if(s.async){var f=setInterval(d,13);if(s.timeout>0)setTimeout(function(){if(c){c.abort();if(!m)d("timeout")}},s.timeout)}try{c.send(s.data)}catch(e){D.handleError(s,c,null,e)}if(!s.async)d();function success(){if(s.success)s.success(data,status);if(s.global)D.event.trigger("ajaxSuccess",[c,s])}function complete(){if(s.complete)s.complete(c,status);if(s.global)D.event.trigger("ajaxComplete",[c,s]);if(s.global&&!--D.active)D.event.trigger("ajaxStop")}return c},handleError:function(s,a,b,e){if(s.error)s.error(a,b,e);if(s.global)D.event.trigger("ajaxError",[a,s,e])},active:0,httpSuccess:function(a){try{return!a.status&&location.protocol=="file:"||(a.status>=200&&a.status<300)||a.status==304||a.status==1223||D.browser.safari&&a.status==undefined}catch(e){}return false},httpNotModified:function(a,c){try{var b=a.getResponseHeader("Last-Modified");return a.status==304||b==D.lastModified[c]||D.browser.safari&&a.status==undefined}catch(e){}return false},httpData:function(a,c,b){var d=a.getResponseHeader("content-type"),xml=c=="xml"||!c&&d&&d.indexOf("xml")>=0,data=xml?a.responseXML:a.responseText;if(xml&&data.documentElement.tagName=="parsererror")throw"parsererror";if(b)data=b(data,c);if(c=="script")D.globalEval(data);if(c=="json")data=eval("("+data+")");return data},param:function(a){var s=[];if(a.constructor==Array||a.jquery)D.each(a,function(){s.push(encodeURIComponent(this.name)+"="+encodeURIComponent(this.value))});else for(var j in a)if(a[j]&&a[j].constructor==Array)D.each(a[j],function(){s.push(encodeURIComponent(j)+"="+encodeURIComponent(this))});else s.push(encodeURIComponent(j)+"="+encodeURIComponent(D.isFunction(a[j])?a[j]():a[j]));return s.join("&").replace(/%20/g,"+")}});D.fn.extend({show:function(c,b){return c?this.animate({height:"show",width:"show",opacity:"show"},c,b):this.filter(":hidden").each(function(){this.style.display=this.oldblock||"";if(D.css(this,"display")=="none"){var a=D("<"+this.tagName+" />").appendTo("body");this.style.display=a.css("display");if(this.style.display=="none")this.style.display="block";a.remove()}}).end()},hide:function(b,a){return b?this.animate({height:"hide",width:"hide",opacity:"hide"},b,a):this.filter(":visible").each(function(){this.oldblock=this.oldblock||D.css(this,"display");this.style.display="none"}).end()},_toggle:D.fn.toggle,toggle:function(a,b){return D.isFunction(a)&&D.isFunction(b)?this._toggle.apply(this,arguments):a?this.animate({height:"toggle",width:"toggle",opacity:"toggle"},a,b):this.each(function(){D(this)[D(this).is(":hidden")?"show":"hide"]()})},slideDown:function(b,a){return this.animate({height:"show"},b,a)},slideUp:function(b,a){return this.animate({height:"hide"},b,a)},slideToggle:function(b,a){return this.animate({height:"toggle"},b,a)},fadeIn:function(b,a){return this.animate({opacity:"show"},b,a)},fadeOut:function(b,a){return this.animate({opacity:"hide"},b,a)},fadeTo:function(c,a,b){return this.animate({opacity:a},c,b)},animate:function(k,j,i,g){var h=D.speed(j,i,g);return this[h.queue===false?"each":"queue"](function(){if(this.nodeType!=1)return false;var f=D.extend({},h),p,hidden=D(this).is(":hidden"),self=this;for(p in k){if(k[p]=="hide"&&hidden||k[p]=="show"&&!hidden)return f.complete.call(this);if(p=="height"||p=="width"){f.display=D.css(this,"display");f.overflow=this.style.overflow}}if(f.overflow!=null)this.style.overflow="hidden";f.curAnim=D.extend({},k);D.each(k,function(c,a){var e=new D.fx(self,f,c);if(/toggle|show|hide/.test(a))e[a=="toggle"?hidden?"show":"hide":a](k);else{var b=a.toString().match(/^([+-]=)?([\d+-.]+)(.*)$/),start=e.cur(true)||0;if(b){var d=parseFloat(b[2]),unit=b[3]||"px";if(unit!="px"){self.style[c]=(d||1)+unit;start=((d||1)/e.cur(true))*start;self.style[c]=start+unit}if(b[1])d=((b[1]=="-="?-1:1)*d)+start;e.custom(start,d,unit)}else e.custom(start,a,"")}});return true})},queue:function(a,b){if(D.isFunction(a)||(a&&a.constructor==Array)){b=a;a="fx"}if(!a||(typeof a=="string"&&!b))return A(this[0],a);return this.each(function(){if(b.constructor==Array)A(this,a,b);else{A(this,a).push(b);if(A(this,a).length==1)b.call(this)}})},stop:function(b,c){var a=D.timers;if(b)this.queue([]);this.each(function(){for(var i=a.length-1;i>=0;i--)if(a[i].elem==this){if(c)a[i](true);a.splice(i,1)}});if(!c)this.dequeue();return this}});var A=function(b,c,a){if(b){c=c||"fx";var q=D.data(b,c+"queue");if(!q||a)q=D.data(b,c+"queue",D.makeArray(a))}return q};D.fn.dequeue=function(a){a=a||"fx";return this.each(function(){var q=A(this,a);q.shift();if(q.length)q[0].call(this)})};D.extend({speed:function(b,a,c){var d=b&&b.constructor==Object?b:{complete:c||!c&&a||D.isFunction(b)&&b,duration:b,easing:c&&a||a&&a.constructor!=Function&&a};d.duration=(d.duration&&d.duration.constructor==Number?d.duration:D.fx.speeds[d.duration])||D.fx.speeds.def;d.old=d.complete;d.complete=function(){if(d.queue!==false)D(this).dequeue();if(D.isFunction(d.old))d.old.call(this)};return d},easing:{linear:function(p,n,b,a){return b+a*p},swing:function(p,n,b,a){return((-Math.cos(p*Math.PI)/2)+0.5)*a+b}},timers:[],timerId:null,fx:function(b,c,a){this.options=c;this.elem=b;this.prop=a;if(!c.orig)c.orig={}}});D.fx.prototype={update:function(){if(this.options.step)this.options.step.call(this.elem,this.now,this);(D.fx.step[this.prop]||D.fx.step._default)(this);if(this.prop=="height"||this.prop=="width")this.elem.style.display="block"},cur:function(a){if(this.elem[this.prop]!=null&&this.elem.style[this.prop]==null)return this.elem[this.prop];var r=parseFloat(D.css(this.elem,this.prop,a));return r&&r>-10000?r:parseFloat(D.curCSS(this.elem,this.prop))||0},custom:function(c,b,d){this.startTime=now();this.start=c;this.end=b;this.unit=d||this.unit||"px";this.now=this.start;this.pos=this.state=0;this.update();var e=this;function t(a){return e.step(a)}t.elem=this.elem;D.timers.push(t);if(D.timerId==null){D.timerId=setInterval(function(){var a=D.timers;for(var i=0;i<a.length;i++)if(!a[i]())a.splice(i--,1);if(!a.length){clearInterval(D.timerId);D.timerId=null}},13)}},show:function(){this.options.orig[this.prop]=D.attr(this.elem.style,this.prop);this.options.show=true;this.custom(0,this.cur());if(this.prop=="width"||this.prop=="height")this.elem.style[this.prop]="1px";D(this.elem).show()},hide:function(){this.options.orig[this.prop]=D.attr(this.elem.style,this.prop);this.options.hide=true;this.custom(this.cur(),0)},step:function(a){var t=now();if(a||t>this.options.duration+this.startTime){this.now=this.end;this.pos=this.state=1;this.update();this.options.curAnim[this.prop]=true;var b=true;for(var i in this.options.curAnim)if(this.options.curAnim[i]!==true)b=false;if(b){if(this.options.display!=null){this.elem.style.overflow=this.options.overflow;this.elem.style.display=this.options.display;if(D.css(this.elem,"display")=="none")this.elem.style.display="block"}if(this.options.hide)this.elem.style.display="none";if(this.options.hide||this.options.show)for(var p in this.options.curAnim)D.attr(this.elem.style,p,this.options.orig[p])}if(b)this.options.complete.call(this.elem);return false}else{var n=t-this.startTime;this.state=n/this.options.duration;this.pos=D.easing[this.options.easing||(D.easing.swing?"swing":"linear")](this.state,n,0,1,this.options.duration);this.now=this.start+((this.end-this.start)*this.pos);this.update()}return true}};D.extend(D.fx,{speeds:{slow:600,fast:200,def:400},step:{scrollLeft:function(a){a.elem.scrollLeft=a.now},scrollTop:function(a){a.elem.scrollTop=a.now},opacity:function(a){D.attr(a.elem.style,"opacity",a.now)},_default:function(a){a.elem.style[a.prop]=a.now+a.unit}}});D.fn.offset=function(){var b=0,top=0,elem=this[0],results;if(elem)with(D.browser){var d=elem.parentNode,offsetChild=elem,offsetParent=elem.offsetParent,doc=elem.ownerDocument,safari2=safari&&parseInt(version)<522&&!/adobeair/i.test(v),css=D.curCSS,fixed=css(elem,"position")=="fixed";if(elem.getBoundingClientRect){var c=elem.getBoundingClientRect();add(c.left+Math.max(doc.documentElement.scrollLeft,doc.body.scrollLeft),c.top+Math.max(doc.documentElement.scrollTop,doc.body.scrollTop));add(-doc.documentElement.clientLeft,-doc.documentElement.clientTop)}else{add(elem.offsetLeft,elem.offsetTop);while(offsetParent){add(offsetParent.offsetLeft,offsetParent.offsetTop);if(mozilla&&!/^t(able|d|h)$/i.test(offsetParent.tagName)||safari&&!safari2)border(offsetParent);if(!fixed&&css(offsetParent,"position")=="fixed")fixed=true;offsetChild=/^body$/i.test(offsetParent.tagName)?offsetChild:offsetParent;offsetParent=offsetParent.offsetParent}while(d&&d.tagName&&!/^body|html$/i.test(d.tagName)){if(!/^inline|table.*$/i.test(css(d,"display")))add(-d.scrollLeft,-d.scrollTop);if(mozilla&&css(d,"overflow")!="visible")border(d);d=d.parentNode}if((safari2&&(fixed||css(offsetChild,"position")=="absolute"))||(mozilla&&css(offsetChild,"position")!="absolute"))add(-doc.body.offsetLeft,-doc.body.offsetTop);if(fixed)add(Math.max(doc.documentElement.scrollLeft,doc.body.scrollLeft),Math.max(doc.documentElement.scrollTop,doc.body.scrollTop))}results={top:top,left:b}}function border(a){add(D.curCSS(a,"borderLeftWidth",true),D.curCSS(a,"borderTopWidth",true))}function add(l,t){b+=parseInt(l,10)||0;top+=parseInt(t,10)||0}return results};D.fn.extend({position:function(){var a=0,top=0,results;if(this[0]){var b=this.offsetParent(),offset=this.offset(),parentOffset=/^body|html$/i.test(b[0].tagName)?{top:0,left:0}:b.offset();offset.top-=num(this,'marginTop');offset.left-=num(this,'marginLeft');parentOffset.top+=num(b,'borderTopWidth');parentOffset.left+=num(b,'borderLeftWidth');results={top:offset.top-parentOffset.top,left:offset.left-parentOffset.left}}return results},offsetParent:function(){var a=this[0].offsetParent;while(a&&(!/^body|html$/i.test(a.tagName)&&D.css(a,'position')=='static'))a=a.offsetParent;return D(a)}});D.each(['Left','Top'],function(i,b){var c='scroll'+b;D.fn[c]=function(a){if(!this[0])return;return a!=undefined?this.each(function(){this==window||this==document?window.scrollTo(!i?a:D(window).scrollLeft(),i?a:D(window).scrollTop()):this[c]=a}):this[0]==window||this[0]==document?self[i?'pageYOffset':'pageXOffset']||D.boxModel&&document.documentElement[c]||document.body[c]:this[0][c]}});D.each(["Height","Width"],function(i,b){var c=i?"Left":"Top",br=i?"Right":"Bottom";D.fn["inner"+b]=function(){return this[b.toLowerCase()]()+num(this,"padding"+c)+num(this,"padding"+br)};D.fn["outer"+b]=function(a){return this["inner"+b]()+num(this,"border"+c+"Width")+num(this,"border"+br+"Width")+(a?num(this,"margin"+c)+num(this,"margin"+br):0)}})})();


/* 二级城市联动：jquery.provincecity */
(function(){var Object$Province=['安徽','澳门','北京','福建','甘肃','广东','广西','贵州','海南','河北','河南','黑龙江','湖北','湖南','吉林','江苏','江西','辽宁','内蒙古','宁夏','青海','山东','山西','陕西','上海','四川','台湾','天津','西藏','香港','新疆','云南','浙江','重庆','海外'];var Object$City=[['合肥','安庆','蚌埠','亳州','巢湖','池州','滁州','阜阳','淮北','淮南','黄山','六安','马鞍山','宿州','铜陵','芜湖','宣城'],['澳门'],['昌平','朝阳','崇文','大兴','东城','房山','丰台','海淀','怀柔','门头沟','密云','平谷','石景山','顺义','通州','西城','宣武','延庆'],['福州','龙岩','南平','宁德','莆田','泉州','三明','厦门','漳州'],['兰州','白银','定西','甘南','嘉峪关','金昌','酒泉','临夏','陇南','平凉','庆阳','天水','武威','张掖'],['广州','潮州','东莞','佛山','河源','惠州','江门','揭阳','茂名','梅州','清远','汕头','汕尾','韶关','深圳','阳江','云浮','湛江','肇庆','中山','珠海'],['桂林','百色','北海','崇左','防城港','贵港','河池','贺州','来宾','柳州','南宁','钦州','梧州','玉林'],['贵阳','安顺','毕节','六盘水','黔东南','黔南','黔西南','铜仁','遵义'],['海口','白沙','保亭','昌江','澄迈','儋州','定安','东方','乐东','临高','陵水','南沙群岛','琼海','琼中','三亚','屯昌','万宁','文昌','五指山','西沙群岛','中沙群岛'],['石家庄','保定','沧州','承德','邯郸','衡水','廊坊','秦皇岛','唐山','邢台','张家口'],['郑州','安阳','鹤壁','焦作','开封','洛阳','漯河','南阳','平顶山','濮阳','三门峡','商丘','新乡','信阳','许昌','周口','驻马店'],['哈尔滨','大庆','大兴安岭','鹤岗','黑河','鸡西','佳木斯','牡丹江','七台河','齐齐哈尔','双鸭山','绥化','伊春'],['武汉','鄂州','恩施','黄冈','黄石','荆门','荆州','潜江','神农架','十堰','随州','天门','仙桃','咸宁','襄樊','孝感','宜昌'],['长沙','常德','郴州','衡阳','怀化','娄底','邵阳','湘潭','湘西','益阳','永州','岳阳','张家界','株洲'],['长春','白城','白山','吉林','辽源','四平','松原','通化','延边'],['南京','常州','淮安','连云港','南通','苏州','宿迁','泰州','无锡','徐州','盐城','扬州','镇江'],['南昌','抚州','赣州','吉安','景德镇','九江','萍乡','上饶','新余','宜春','鹰潭'],['沈阳','鞍山','本溪','朝阳','大连','丹东','抚顺','阜新','葫芦岛','锦州','辽阳','盘锦','铁岭','营口'],['呼和浩特','阿拉善','巴彦淖尔','包头','赤峰','鄂尔多斯','呼伦贝尔','通辽','乌海','乌兰察布','锡林郭勒','兴安'],['银川','固原','石嘴山','吴忠','中卫'],['西宁','果洛','海北','海东','海南','海西','黄南','玉树'],['济南','滨州','德州','东营','菏泽','济宁','莱芜','聊城','临沂','青岛','日照','泰安','威海','潍坊','烟台','枣庄','淄博'],['太原','长治','大同','晋城','晋中','临汾','吕梁','朔州','忻州','阳泉','运城'],['西安','安康','宝鸡','汉中','商洛','铜川','渭南','咸阳','延安','榆林'],['宝山','长宁','崇明','奉贤','虹口','黄浦','嘉定','金山','静安','卢湾','闵行','南汇','浦东','普陀','青浦','松江','徐汇','杨浦','闸北'],['成都','阿坝','巴中','达州','德阳','甘孜','广安','广元','乐山','凉山','泸州','眉山','绵阳','内江','南充','攀枝花','遂宁','雅安','宜宾','资阳','自贡'],['台北','阿莲','安定','安平','八德','八里','白河','白沙','板桥','褒忠','宝山','卑南','北斗','北港','北门','北埔','北投','补子','布袋','草屯','长宾','长治','潮州','车城','成功','城中区','池上','春日','刺桐','高雄','花莲','基隆','嘉义','苗栗','南投','屏东','台东','台南','台中','桃园','新竹','宜兰','彰化'],['宝坻','北辰','大港','东丽','汉沽','和平','河北','河东','河西','红桥','蓟县','津南','静海','南开','宁河','塘沽','武清','西青'],['拉萨','阿里','昌都','林芝','那曲','日喀则','山南'],['北区','大埔区','东区','观塘区','黄大仙区','九龙','葵青区','离岛区','南区','荃湾区','沙田区','深水埗区','屯门区','湾仔区','西贡区','香港','新界','油尖旺区','元朗区','中西区'],['乌鲁木齐','阿克苏','阿拉尔','阿勒泰','巴音郭楞','博尔塔拉','昌吉','哈密','和田','喀什','克拉玛依','克孜勒苏柯尔克孜','石河子','塔城','图木舒克','吐鲁番','五家渠','伊犁'],['昆明','保山','楚雄','大理','德宏','迪庆','红河','丽江','临沧','怒江','曲靖','思茅','文山','西双版纳','玉溪','昭通'],['杭州','湖州','嘉兴','金华','丽水','宁波','衢州','绍兴','台州','温州','舟山'],['巴南','北碚','璧山','长寿','城口','大渡口','大足','垫江','丰都','奉节','涪陵','合川','江北','江津','九龙坡','开县','梁平','南岸','南川','彭水','綦江','黔江','荣昌','沙坪坝','石柱','双桥','铜梁','潼南','万盛','万州','巫山','巫溪','武隆','秀山','永川','酉阳','渝北','渝中','云阳','忠县'],['阿根廷','埃及','爱尔兰','奥地利','奥克兰','澳大利亚','巴基斯坦','巴西','保加利亚','比利时','冰岛','朝鲜','丹麦','德国','俄罗斯','法国','菲律宾','芬兰','哥伦比亚','韩国','荷兰','加拿大','柬埔寨','喀麦隆','老挝','卢森堡','罗马尼亚','马达加斯加','马来西亚','毛里求斯','美国','秘鲁','缅甸','墨西哥','南非','尼泊尔','挪威','葡萄牙','其它地区','日本','瑞典','瑞士','斯里兰卡','泰国','土耳其','委内瑞拉','文莱','乌克兰','西班牙','希腊','新加坡','新西兰','匈牙利','以色列','意大利','印度','印度尼西亚','英国','越南','智利']];jQuery.fn.provincecity=function(opts){opts=jQuery.extend({selProvince:'selProvince',defProvinceVal:'北京',selCity:'selCity',defCityVal:'西城'},opts||{});var _sel1=jQuery('#'+opts.selProvince);var _sel2=jQuery('#'+opts.selCity);_sel1.append("<option value='"+opts.defProvinceVal+"'>"+opts.defProvinceVal+"</option>");jQuery.each(Object$Province,function(index,data){_sel1.append("<option value='"+data+"'>"+data+"</option>")});_sel2.append("<option value='"+opts.defCityVal+"'>"+opts.defCityVal+"</option>");var index1="";_sel1.change(function(){_sel2.empty();index1=this.selectedIndex;jQuery.each(Object$City[index1-1],function(index,data){_sel2.append("<option value='"+data+"'>"+data+"</option>")})});return this}})(jQuery);



/* 颜色选择器：jquery.colorPicker */
(function(){function colorPicker(){this._nextId=0;this._inst=[];this._curInst=null;this._colorpickerShowing=false;this._colorPickerDiv=jQuery('<div id="colorPickerDiv"></div>')}jQuery.extend(colorPicker.prototype,{markerClassName:'hasColorPicker',_register:function(inst){var id=this._nextId++;this._inst[id]=inst;return id},_getInst:function(id){return this._inst[id]||id},_doKeyDown:function(e){var inst=jQuery.colorPicker._getInst(this._colId);if(jQuery.colorPicker._colorpickerShowing){switch(e.keyCode){case 9:case 27:jQuery.colorPicker.hideColorPicker();break}}else if(e.keyCode==40){jQuery.colorPicker.showFor(this)}},_resetSample:function(e){var inst=jQuery.colorPicker._getInst(this._colId);inst._sampleSpan.css('background-color',inst._input.value);alert(inst._input.value)},_hasClass:function(element,className){var classes=element.attr('class');return(classes&&classes.indexOf(className)>-1)},showFor:function(control){control=(control.jquery?control[0]:(typeof control=='string'?jQuery(control)[0]:control));var input=(control.nodeName&&control.nodeName.toLowerCase()=='input'?control:this);if(jQuery.colorPicker._lastInput==input){return};if(jQuery.colorPicker._colorpickerShowing){return};var inst=jQuery.colorPicker._getInst(input._colId);jQuery.colorPicker.hideColorPicker();jQuery.colorPicker._lastInput=input;if(!jQuery.colorPicker._pos){jQuery.colorPicker._pos=jQuery.colorPicker._findPos(input);jQuery.colorPicker._pos[1]+=input.offsetHeight}var isFixed=false;jQuery(input).parents().each(function(){isFixed|=jQuery(this).css('position')=='fixed'});if(isFixed&&jQuery.browser.opera){jQuery.colorPicker._pos[0]-=document.documentElement.scrollLeft;jQuery.colorPicker._pos[1]-=document.documentElement.scrollTop};inst._colorPickerDiv.css('position',(jQuery.blockUI?'static':(isFixed?'fixed':'absolute'))).css('left',jQuery.colorPicker._pos[0]+'px').css('top',jQuery.colorPicker._pos[1]+1+'px');jQuery.colorPicker._pos=null;jQuery.colorPicker._showColorPicker(inst);return this},_findPos:function(obj){while(obj&&(obj.type=='hidden'||obj.nodeType!=1)){obj=obj.nextSibling}var curleft=curtop=0;if(obj&&obj.offsetParent){curleft=obj.offsetLeft;curtop=obj.offsetTop;while(obj=obj.offsetParent){var origcurleft=curleft;curleft+=obj.offsetLeft;if(curleft<0)curleft=origcurleft;curtop+=obj.offsetTop}};return[curleft,curtop]},_checkExternalClick:function(event){if(!jQuery.colorPicker._curInst)return;var target=jQuery(event.target);if((target.parents("#colorPickerDiv").length==0)&&jQuery.colorPicker._colorpickerShowing&&!(jQuery.blockUI))if(target.text()!=jQuery.colorPicker._curInst._colorPickerDiv.text())jQuery.colorPicker.hideColorPicker()},hideColorPicker:function(s){var inst=this._curInst;if(!inst)return;if(this._colorpickerShowing){this._colorpickerShowing=false;this._lastInput=null;this._colorPickerDiv.css('position','absolute').css('left','0px').css('top','-1000px');if(jQuery.blockUI){jQuery.unblockUI();jQuery('body').append(this._colorPickerDiv)}this._curInst=null}if(inst._input[0].value!=jQuery.css(inst._sampleSpan,'background-color')){inst._sampleSpan.css('background-color',inst._input[0].value)}this._showSelectBoxes()},_connectColorPicker:function(target,inst){var input=jQuery(target);if(this._hasClass(input,this.markerClassName)){return}jQuery(input).attr('autocomplete','OFF');inst._input=jQuery(input);inst._sampleSpan=jQuery('<span class="ColorPickerDivSample" style="background-color:'+inst._input[0].value+';height:'+inst._input[0].offsetHeight+';">&nbsp;</span>');input.after(inst._sampleSpan);inst._sampleSpan.click(function(){input.focus()});input.focus(this.showFor);input.addClass(this.markerClassName).keydown(this._doKeyDown);input[0]._colId=inst._id},_showColorPicker:function(id){var inst=this._getInst(id);this._updateColorPicker(inst);inst._colorPickerDiv.css('width',inst._startTime!=null?'10em':'6em');inst._colorPickerDiv.show('fast');if(inst._input[0].type!='hidden')inst._input[0].focus();this._curInst=inst;this._colorpickerShowing=true;this._hideSelectBoxes()},_updateColorPicker:function(inst){inst._colorPickerDiv.empty().append(inst._generateColorPicker());if(inst._input&&inst._input[0].type!='hidden'){inst._input[0].focus();jQuery("td.color",inst._timePickerDiv).unbind().mouseover(function(){inst._sampleSpan.css('background-color',jQuery.css(this,'background-color'))}).click(function(){inst._setValue(this)})}},_showSelectBoxes:function(){/*jQuery('SELECT').css('visibility','visible')*/},_hideSelectBoxes:function(){/*jQuery('SELECT').css('visibility','hidden')*/}});function ColorPickerInstance(){this._id=jQuery.colorPicker._register(this);this._input=null;this._colorPickerDiv=jQuery.colorPicker._colorPickerDiv;this._sampleSpan=null};jQuery.extend(ColorPickerInstance.prototype,{_get:function(name){return(this._settings[name]!=null?this._settings[name]:jQuery.colorPicker._defaults[name])},_getValue:function(){if(this._input&&this._input[0].type!='hidden'&&this._input[0].value!=""){return this._input[0].value}return null},_setValue:function(sel){if(this._input&&this._input[0].type!='hidden'){this._input[0].value=jQuery.attr(sel,'title');jQuery(this._input[0]).change()}jQuery.colorPicker.hideColorPicker()},_generateColorPicker:function(){var colors=new Array("#000000","#000033","#000066","#000099","#0000CC","#0000FF","#330000","#330033","#330066","#330099","#3300CC","#3300FF","#660000","#660033","#660066","#660099","#6600CC","#6600FF","#990000","#990033","#990066","#990099","#9900CC","#9900FF","#CC0000","#CC0033","#CC0066","#CC0099","#CC00CC","#CC00FF","#FF0000","#FF0033","#FF0066","#FF0099","#FF00CC","#FF00FF","#003300","#003333","#003366","#003399","#0033CC","#0033FF","#333300","#333333","#333366","#333399","#3333CC","#3333FF","#663300","#663333","#663366","#663399","#6633CC","#6633FF","#993300","#993333","#993366","#993399","#9933CC","#9933FF","#CC3300","#CC3333","#CC3366","#CC3399","#CC33CC","#CC33FF","#FF3300","#FF3333","#FF3366","#FF3399","#FF33CC","#FF33FF","#006600","#006633","#006666","#006699","#0066CC","#0066FF","#336600","#336633","#336666","#336699","#3366CC","#3366FF","#666600","#666633","#666666","#666699","#6666CC","#6666FF","#996600","#996633","#996666","#996699","#9966CC","#9966FF","#CC6600","#CC6633","#CC6666","#CC6699","#CC66CC","#CC66FF","#FF6600","#FF6633","#FF6666","#FF6699","#FF66CC","#FF66FF","#009900","#009933","#009966","#009999","#0099CC","#0099FF","#339900","#339933","#339966","#339999","#3399CC","#3399FF","#669900","#669933","#669966","#669999","#6699CC","#6699FF","#999900","#999933","#999966","#999999","#9999CC","#9999FF","#CC9900","#CC9933","#CC9966","#CC9999","#CC99CC","#CC99FF","#FF9900","#FF9933","#FF9966","#FF9999","#FF99CC","#FF99FF","#00CC00","#00CC33","#00CC66","#00CC99","#00CCCC","#00CCFF","#33CC00","#33CC33","#33CC66","#33CC99","#33CCCC","#33CCFF","#66CC00","#66CC33","#66CC66","#66CC99","#66CCCC","#66CCFF","#99CC00","#99CC33","#99CC66","#99CC99","#99CCCC","#99CCFF","#CCCC00","#CCCC33","#CCCC66","#CCCC99","#CCCCCC","#CCCCFF","#FFCC00","#FFCC33","#FFCC66","#FFCC99","#FFCCCC","#FFCCFF","#00FF00","#00FF33","#00FF66","#00FF99","#00FFCC","#00FFFF","#33FF00","#33FF33","#33FF66","#33FF99","#33FFCC","#33FFFF","#66FF00","#66FF33","#66FF66","#66FF99","#66FFCC","#66FFFF","#99FF00","#99FF33","#99FF66","#99FF99","#99FFCC","#99FFFF","#CCFF00","#CCFF33","#CCFF66","#CCFF99","#CCFFCC","#CCFFFF","#FFFF00","#FFFF33","#FFFF66","#FFFF99","#FFFFCC","#EEEEEE","#111111","#222222","#333333","#444444","#555555","#666666","#777777","#888888","#999999","#A5A5A5","#AAAAAA","#BBBBBB","#C3C3C3","#CCCCCC","#D2D2D2","#DDDDDD","#E1E1E1","#FFFFFF");var total=colors.length;var width=18;var html="<table class='ColorPickerArea' cellspacing='0' cellpadding='0'>";for(var i=0;i<total;i++){if((i%width)==0){html+="<tr>"}html+='<td class="color" title="'+colors[i]+'" style="cursor: pointer;border: 1px solid #000000;background-color:'+colors[i]+'"><div style="width:9px;height:9px;"></div></td>';if(((i+1)>=total)||(((i+1)%width)==0)){html+="</tr>"}}html+="</table>";return html}});jQuery.fn.attachColorPicker=function(){return this.each(function(){var nodeName=this.nodeName.toLowerCase();if(nodeName=='input'){var inst=new ColorPickerInstance();jQuery.colorPicker._connectColorPicker(this,inst)}})};jQuery.fn.getValue=function(){var inst=(this.length>0?jQuery.colorPicker._getInst(this[0]._colId):null);return(inst?inst._getValue():null)};jQuery.fn.setValue=function(value){var inst=(this.length>0?jQuery.colorPicker._getInst(this[0]._colId):null);if(inst)inst._setValue(value)};jQuery(document).ready(function(){jQuery.colorPicker=new colorPicker();jQuery(document.body).append(jQuery.colorPicker._colorPickerDiv).mousedown(jQuery.colorPicker._checkExternalClick)})})(jQuery);


/* 右键菜单：jquery.ContextMenu */
(function(){var menu,shadow,trigger,content,hash,currentTarget;var defaults={menuStyle:{listStyle:'none',padding:'1px',margin:'0px',backgroundColor:'#fff',border:'1px solid #999',width:'100px'},itemStyle:{margin:'0px',color:'#000',display:'block',cursor:'default',padding:'3px',border:'1px solid #fff',backgroundColor:'transparent'},itemHoverStyle:{border:'1px solid #0a246a',backgroundColor:'#b6bdd2'},eventPosX:'pageX',eventPosY:'pageY',shadow:true,onContextMenu:null,onShowMenu:null};jQuery.fn.contextMenu=function(id,options){if(!menu){menu=jQuery('<div id="jqContextMenu"></div>').hide().css({position:'absolute',zIndex:'500'}).appendTo('body').bind('click',function(e){e.stopPropagation()})};if(!shadow){shadow=jQuery('<div></div>').css({backgroundColor:'#000',position:'absolute',opacity:0.2,zIndex:499}).appendTo('body').hide()};hash=hash||[];hash.push({id:id,menuStyle:jQuery.extend({},defaults.menuStyle,options.menuStyle||{}),itemStyle:jQuery.extend({},defaults.itemStyle,options.itemStyle||{}),itemHoverStyle:jQuery.extend({},defaults.itemHoverStyle,options.itemHoverStyle||{}),bindings:options.bindings||{},shadow:options.shadow||options.shadow===false?options.shadow:defaults.shadow,onContextMenu:options.onContextMenu||defaults.onContextMenu,onShowMenu:options.onShowMenu||defaults.onShowMenu,eventPosX:options.eventPosX||defaults.eventPosX,eventPosY:options.eventPosY||defaults.eventPosY});var index=hash.length-1;jQuery(this).bind('contextmenu',function(e){var bShowContext=(!!hash[index].onContextMenu)?hash[index].onContextMenu(e):true;if(bShowContext)display(index,this,e,options);return false});return this};function display(index,trigger,e,options){var cur=hash[index];content=jQuery('#'+cur.id).find('ul:first').clone(true);content.css(cur.menuStyle).find('li').css(cur.itemStyle).hover(function(){jQuery(this).css(cur.itemHoverStyle)},function(){jQuery(this).css(cur.itemStyle)}).find('img').css({verticalAlign:'middle',paddingRight:'2px'});menu.html(content);if(!!cur.onShowMenu)menu=cur.onShowMenu(e,menu);jQuery.each(cur.bindings,function(id,func){jQuery('#'+id,menu).bind('click',function(e){hide();func(trigger,currentTarget)})});menu.css({'left':e[cur.eventPosX],'top':e[cur.eventPosY]}).show();if(cur.shadow)shadow.css({width:menu.width(),height:menu.height(),left:e.pageX+2,top:e.pageY+2}).show();jQuery(document).one('click',hide)}function hide(){menu.hide();shadow.hide()}jQuery.contextMenu={defaults:function(userDefaults){jQuery.each(userDefaults,function(i,val){if(typeof val=='object'&&defaults[i]){jQuery.extend(defaults[i],val)}else defaults[i]=val})}}})(jQuery);jQuery(function(){jQuery('div.contextMenu').hide()});


/* 文件树状：jquery.fileTree */
(function(){jQuery.extend(jQuery.fn,{fileTree:function(o,h){if(!o)var o={};if(o.root==undefined)o.root='/';if(o.script==undefined)o.script='jqueryFileTree.aspx';if(o.folderEvent==undefined)o.folderEvent='click';if(o.expandSpeed==undefined)o.expandSpeed=500;if(o.collapseSpeed==undefined)o.collapseSpeed=500;if(o.expandEasing==undefined)o.expandEasing=null;if(o.collapseEasing==undefined)o.collapseEasing=null;if(o.multiFolder==undefined)o.multiFolder=true;if(o.loadMessage==undefined)o.loadMessage='Loading...';jQuery(this).each(function(){function showTree(c,t){jQuery(c).addClass('wait');jQuery(".jqueryFileTree.start").remove();jQuery.post(o.script,{dir:t},function(data){jQuery(c).find('.start').html('');jQuery(c).removeClass('wait').append(data);if(o.root==t)jQuery(c).find('UL:hidden').show();else jQuery(c).find('UL:hidden').slideDown({duration:o.expandSpeed,easing:o.expandEasing});bindTree(c)})}function bindTree(t){jQuery(t).find('LI A').bind(o.folderEvent,function(){if(jQuery(this).parent().hasClass('directory')){if(jQuery(this).parent().hasClass('collapsed')){if(!o.multiFolder){jQuery(this).parent().parent().find('UL').slideUp({duration:o.collapseSpeed,easing:o.collapseEasing});jQuery(this).parent().parent().find('LI.directory').removeClass('expanded').addClass('collapsed')}jQuery(this).parent().find('UL').remove();showTree(jQuery(this).parent(),escape(jQuery(this).attr('rel').match(/.*\//)));jQuery(this).parent().removeClass('collapsed').addClass('expanded')}else{jQuery(this).parent().find('UL').slideUp({duration:o.collapseSpeed,easing:o.collapseEasing});jQuery(this).parent().removeClass('expanded').addClass('collapsed')}}else{h(jQuery(this).attr('rel'))}return false});if(o.folderEvent.toLowerCase!='click')jQuery(t).find('LI A').bind('click',function(){return false})}jQuery(this).html('<ul class="jqueryFileTree start"><li class="wait">'+o.loadMessage+'<li></ul>');showTree(jQuery(this),escape(o.root))})}})})(jQuery);



/* jquery.cookie */
jQuery.cookie=function(name,value,options){if(typeof value!='undefined'){options=options||{};if(value===null){value='';options.expires=-1}var expires='';if(options.expires&&(typeof options.expires=='number'||options.expires.toUTCString)){var date;if(typeof options.expires=='number'){date=new Date();date.setTime(date.getTime()+(options.expires*24*60*60*1000))}else{date=options.expires}expires='; expires='+date.toUTCString()}var path=options.path?'; path='+(options.path):'';var domain=options.domain?'; domain='+(options.domain):'';var secure=options.secure?'; secure':'';document.cookie=[name,'=',encodeURIComponent(value),expires,path,domain,secure].join('')}else{var cookieValue=null;if(document.cookie&&document.cookie!=''){var cookies=document.cookie.split(';');for(var i=0;i<cookies.length;i++){var cookie=jQuery.trim(cookies[i]);if(cookie.substring(0,name.length+1)==(name+'=')){cookieValue=decodeURIComponent(cookie.substring(name.length+1));break}}}return cookieValue}};


/* jquery.form */
(function(){jQuery.fn.ajaxSubmit=function(options){if(!this.length){log('ajaxSubmit: skipping submit process - no element selected');return this}if(typeof options=='function')options={success:options};options=jQuery.extend({url:this.attr('action')||window.location.toString(),type:this.attr('method')||'GET'},options||{});var veto={};this.trigger('form-pre-serialize',[this,options,veto]);if(veto.veto){log('ajaxSubmit: submit vetoed via form-pre-serialize trigger');return this}var a=this.formToArray(options.semantic);if(options.data){options.extraData=options.data;for(var n in options.data)a.push({name:n,value:options.data[n]})}if(options.beforeSubmit&&options.beforeSubmit(a,this,options)===false){log('ajaxSubmit: submit aborted via beforeSubmit callback');return this}this.trigger('form-submit-validate',[a,this,options,veto]);if(veto.veto){log('ajaxSubmit: submit vetoed via form-submit-validate trigger');return this}var q=jQuery.param(a);if(options.type.toUpperCase()=='GET'){options.url+=(options.url.indexOf('?')>=0?'&':'?')+q;options.data=null}else options.data=q;var $form=this,callbacks=[];if(options.resetForm)callbacks.push(function(){$form.resetForm()});if(options.clearForm)callbacks.push(function(){$form.clearForm()});if(!options.dataType&&options.target){var oldSuccess=options.success||function(){};callbacks.push(function(data){jQuery(options.target).html(data).each(oldSuccess,arguments)})}else if(options.success)callbacks.push(options.success);options.success=function(data,status){for(var i=0,max=callbacks.length;i<max;i++)callbacks[i](data,status,$form)};var files=jQuery('input:file',this).fieldValue();var found=false;for(var j=0;j<files.length;j++)if(files[j])found=true;if(options.iframe||found){if(jQuery.browser.safari&&options.closeKeepAlive)jQuery.get(options.closeKeepAlive,fileUpload);else fileUpload()}else jQuery.ajax(options);this.trigger('form-submit-notify',[this,options]);return this;function fileUpload(){var form=$form[0];if(jQuery(':input[@name=submit]',form).length){alert('Error: Form elements must not be named "submit".');return}var opts=jQuery.extend({},jQuery.ajaxSettings,options);var id='jqFormIO'+(new Date().getTime());var $io=jQuery('<iframe id="'+id+'" name="'+id+'" />');var io=$io[0];if(jQuery.browser.msie||jQuery.browser.opera)io.src='javascript:false;document.write("");';$io.css({position:'absolute',top:'-1000px',left:'-1000px'});var xhr={responseText:null,responseXML:null,status:0,statusText:'n/a',getAllResponseHeaders:function(){},getResponseHeader:function(){},setRequestHeader:function(){}};var g=opts.global;if(g&&!jQuery.active++)jQuery.event.trigger("ajaxStart");if(g)jQuery.event.trigger("ajaxSend",[xhr,opts]);var cbInvoked=0;var timedOut=0;var sub=form.clk;if(sub){var n=sub.name;if(n&&!sub.disabled){options.extraData=options.extraData||{};options.extraData[n]=sub.value;if(sub.type=="image"){options.extraData[name+'.x']=form.clk_x;options.extraData[name+'.y']=form.clk_y}}}setTimeout(function(){var t=$form.attr('target'),a=$form.attr('action');$form.attr({target:id,encoding:'multipart/form-data',enctype:'multipart/form-data',method:'POST',action:opts.url});if(opts.timeout)setTimeout(function(){timedOut=true;cb()},opts.timeout);var extraInputs=[];try{if(options.extraData)for(var n in options.extraData)extraInputs.push(jQuery('<input type="hidden" name="'+n+'" value="'+options.extraData[n]+'" />').appendTo(form)[0]);$io.appendTo('body');io.attachEvent?io.attachEvent('onload',cb):io.addEventListener('load',cb,false);form.submit()}finally{$form.attr('action',a);t?$form.attr('target',t):$form.removeAttr('target');jQuery(extraInputs).remove()}},10);function cb(){if(cbInvoked++)return;io.detachEvent?io.detachEvent('onload',cb):io.removeEventListener('load',cb,false);var operaHack=0;var ok=true;try{if(timedOut)throw'timeout';var data,doc;doc=io.contentWindow?io.contentWindow.document:io.contentDocument?io.contentDocument:io.document;if(doc.body==null&&!operaHack&&jQuery.browser.opera){operaHack=1;cbInvoked--;setTimeout(cb,100);return}xhr.responseText=doc.body?doc.body.innerHTML:null;xhr.responseXML=doc.XMLDocument?doc.XMLDocument:doc;xhr.getResponseHeader=function(header){var headers={'content-type':opts.dataType};return headers[header]};if(opts.dataType=='json'||opts.dataType=='script'){var ta=doc.getElementsByTagName('textarea')[0];xhr.responseText=ta?ta.value:xhr.responseText}else if(opts.dataType=='xml'&&!xhr.responseXML&&xhr.responseText!=null){xhr.responseXML=toXml(xhr.responseText)}data=jQuery.httpData(xhr,opts.dataType)}catch(e){ok=false;jQuery.handleError(opts,xhr,'error',e)}if(ok){opts.success(data,'success');if(g)jQuery.event.trigger("ajaxSuccess",[xhr,opts])}if(g)jQuery.event.trigger("ajaxComplete",[xhr,opts]);if(g&&!--jQuery.active)jQuery.event.trigger("ajaxStop");if(opts.complete)opts.complete(xhr,ok?'success':'error');setTimeout(function(){$io.remove();xhr.responseXML=null},100)};function toXml(s,doc){if(window.ActiveXObject){doc=new ActiveXObject('Microsoft.XMLDOM');doc.async='false';doc.loadXML(s)}else doc=(new DOMParser()).parseFromString(s,'text/xml');return(doc&&doc.documentElement&&doc.documentElement.tagName!='parsererror')?doc:null}}};jQuery.fn.ajaxForm=function(options){return this.ajaxFormUnbind().bind('submit.form-plugin',function(){jQuery(this).ajaxSubmit(options);return false}).each(function(){jQuery(":submit,input:image",this).bind('click.form-plugin',function(e){var $form=this.form;$form.clk=this;if(this.type=='image'){if(e.offsetX!=undefined){$form.clk_x=e.offsetX;$form.clk_y=e.offsetY}else if(typeof jQuery.fn.offset=='function'){var offset=jQuery(this).offset();$form.clk_x=e.pageX-offset.left;$form.clk_y=e.pageY-offset.top}else{$form.clk_x=e.pageX-this.offsetLeft;$form.clk_y=e.pageY-this.offsetTop}}setTimeout(function(){$form.clk=$form.clk_x=$form.clk_y=null},10)})})};jQuery.fn.ajaxFormUnbind=function(){this.unbind('submit.form-plugin');return this.each(function(){jQuery(":submit,input:image",this).unbind('click.form-plugin')})};jQuery.fn.formToArray=function(semantic){var a=[];if(this.length==0)return a;var form=this[0];var els=semantic?form.getElementsByTagName('*'):form.elements;if(!els)return a;for(var i=0,max=els.length;i<max;i++){var el=els[i];var n=el.name;if(!n)continue;if(semantic&&form.clk&&el.type=="image"){if(!el.disabled&&form.clk==el)a.push({name:n+'.x',value:form.clk_x},{name:n+'.y',value:form.clk_y});continue}var v=jQuery.fieldValue(el,true);if(v&&v.constructor==Array){for(var j=0,jmax=v.length;j<jmax;j++)a.push({name:n,value:v[j]})}else if(v!==null&&typeof v!='undefined')a.push({name:n,value:v})}if(!semantic&&form.clk){var inputs=form.getElementsByTagName("input");for(var i=0,max=inputs.length;i<max;i++){var input=inputs[i];var n=input.name;if(n&&!input.disabled&&input.type=="image"&&form.clk==input)a.push({name:n+'.x',value:form.clk_x},{name:n+'.y',value:form.clk_y})}}return a};jQuery.fn.formSerialize=function(semantic){return jQuery.param(this.formToArray(semantic))};jQuery.fn.fieldSerialize=function(successful){var a=[];this.each(function(){var n=this.name;if(!n)return;var v=jQuery.fieldValue(this,successful);if(v&&v.constructor==Array){for(var i=0,max=v.length;i<max;i++)a.push({name:n,value:v[i]})}else if(v!==null&&typeof v!='undefined')a.push({name:this.name,value:v})});return jQuery.param(a)};jQuery.fn.fieldValue=function(successful){for(var val=[],i=0,max=this.length;i<max;i++){var el=this[i];var v=jQuery.fieldValue(el,successful);if(v===null||typeof v=='undefined'||(v.constructor==Array&&!v.length))continue;v.constructor==Array?jQuery.merge(val,v):val.push(v)}return val};jQuery.fieldValue=function(el,successful){var n=el.name,t=el.type,tag=el.tagName.toLowerCase();if(typeof successful=='undefined')successful=true;if(successful&&(!n||el.disabled||t=='reset'||t=='button'||(t=='checkbox'||t=='radio')&&!el.checked||(t=='submit'||t=='image')&&el.form&&el.form.clk!=el||tag=='select'&&el.selectedIndex==-1))return null;if(tag=='select'){var index=el.selectedIndex;if(index<0)return null;var a=[],ops=el.options;var one=(t=='select-one');var max=(one?index+1:ops.length);for(var i=(one?index:0);i<max;i++){var op=ops[i];if(op.selected){var v=jQuery.browser.msie&&!(op.attributes['value'].specified)?op.text:op.value;if(one)return v;a.push(v)}}return a}return el.value};jQuery.fn.clearForm=function(){return this.each(function(){jQuery('input,select,textarea',this).clearFields()})};jQuery.fn.clearFields=jQuery.fn.clearInputs=function(){return this.each(function(){var t=this.type,tag=this.tagName.toLowerCase();if(t=='text'||t=='password'||tag=='textarea')this.value='';else if(t=='checkbox'||t=='radio')this.checked=false;else if(tag=='select')this.selectedIndex=-1})};jQuery.fn.resetForm=function(){return this.each(function(){if(typeof this.reset=='function'||(typeof this.reset=='object'&&!this.reset.nodeType))this.reset()})};jQuery.fn.enable=function(b){if(b==undefined)b=true;return this.each(function(){this.disabled=!b})};jQuery.fn.select=function(select){if(select==undefined)select=true;return this.each(function(){var t=this.type;if(t=='checkbox'||t=='radio')this.checked=select;else if(this.tagName.toLowerCase()=='option'){var $sel=jQuery(this).parent('select');if(select&&$sel[0]&&$sel[0].type=='select-one'){$sel.find('option').select(false)}this.selected=select}})};function log(){if(jQuery.fn.ajaxSubmit.debug&&window.console&&window.console.log)window.console.log('[jquery.form] '+Array.prototype.join.call(arguments,''))}})(jQuery);




/* 提示框：jquery.jTips */
(function(){jQuery.fn.jtip=function(opts){opts=jQuery.extend({fade:false,gravity:'r'},opts||{});var tip=null,cancelHide=false;this.hover(function(){if(jQuery("#jtip").is("div")){return}if(jQuery(this).attr('tip')&&jQuery(this).attr('tip')!=""){jQuery.data(this,'cancel.jtip',true);var tip=jQuery.data(this,'active.jtip');if(!tip){tip=jQuery('<div id="jtip" class="jtip"><div id="jtip-inner" class="jtip-inner">'+jQuery(this).attr('tip')+'</div></div>');tip.css({position:'absolute',zIndex:1000});jQuery(this).attr('title','');jQuery.data(this,'active.jtip',tip)};var pos=jQuery.extend({},jQuery(this).offset(),{width:this.offsetWidth,height:this.offsetHeight});tip.remove().css({top:0,left:0,visibility:'hidden',display:'block'}).appendTo(document.body);var actualWidth=tip[0].offsetWidth,actualHeight=tip[0].offsetHeight;switch(opts.gravity.charAt(0)){case'b':tip.css({top:pos.top+pos.height,left:pos.left});break;case't':tip.css({top:pos.top-actualHeight,left:pos.left});break;case'l':tip.css({top:pos.top+pos.height,left:pos.left-actualWidth});break;case'r':tip.css({top:pos.top+pos.height,left:pos.left});break}if(opts.fade){tip.css({opacity:0,display:'block',visibility:'visible'}).animate({opacity:1})}else{tip.css({visibility:'visible'})};bgiframe=jQuery('<iframe class="bgiframe" frameborder="0" scrolling="no" src="" style="display:block;position:absolute;z-index:999;'+'top:'+(jQuery('#jtip').offset().top)+'px;'+'left:'+(jQuery('#jtip').offset().left)+'px;'+'width:'+(tip[0].offsetWidth-2)+'px;'+'height:'+(tip[0].offsetHeight-2)+'px;'+'"></iframe>');bgiframe.appendTo(document.body)}},function(){if(jQuery(this).attr('tip')&&jQuery(this).attr('tip')!=""){jQuery.data(this,'cancel.jtip',false);var self=this;setTimeout(function(){if(jQuery.data(this,'cancel.jtip'))return;var tip=jQuery.data(self,'active.jtip');if(opts.fade){tip.stop().fadeOut(function(){jQuery(this).remove()})}else{tip.remove()};jQuery('iframe.bgiframe').remove()},2)}})}})(jQuery);



/* jquery.message */
(function(jQuery){this.layer={'width':200,'height':24,'top':0};this.title='1';this.sfuc=null;this.time=4000;this.anims={'type':'slide','speed':1000,'root':'/message/'};this.inits=function(title,text){if($("#PopTips").is("div")){return};var sBody='<div id="PopTips" style="z-index:100;width:90%;margin:0px auto;text-align:center;height:'+(this.layer.height+2)+'px;position:absolute;display:none; top:'+(this.layer.top)+'px;"><div id="tips_Content" style="margin:0px auto;font-size:12px;width:'+(this.layer.width)+'px;height:'+(this.layer.height)+'px;border:#000000 1px solid;color:#000000;background:#ffffcc;text-align:left;"><span style="background:url('+this.anims.root+this.title+'.gif) 1px no-repeat; text-indent:25px;float:left;height:16px;line-height:16px;padding-top:6px;">'+text+'</span><span id="PopTips_Close" style="float:right; padding-right:2px;width:16px;line-height:auto;color:red;font-size:12px;font-weight:bold;text-align:center;cursor:pointer;overflow:hidden;padding-top:6px;">×</span></div></div>';$(document.body).prepend(sBody)};this.show=function(title,text,time){if($("#PopTips").is("div")){return};if(title=='0'||!title)title=this.title;this.inits(title,text);if(time)this.time=time;switch(this.anims.type){case'slide':$("#PopTips").slideDown(this.anims.speed);break;case'fade':$("#PopTips").fadeIn(this.anims.speed);break;case'show':$("#PopTips").show(this.anims.speed);break;default:$("#PopTips").slideDown(this.anims.speed);break}$("#PopTips_Close").click(function(){setTimeout('this.close()',1)});this.rmtips(this.time)};this.lays=function(width,height,top){if($("#PopTips").is("div")){return};if(width!=0&&width)this.layer.width=width;if(height!=0&&height)this.layer.height=height;if(top!=0&&top)this.layer.top=top};this.anim=function(type,speed,root){if($("#PopTips").is("div")){return};if(type!=0&&type)this.anims.type=type;if(speed!=0&&speed){switch(speed){case'slow':this.anims.speed=1000;break;case'fast':this.anims.speed=200;break;case'normal':this.anims.speed=400;break;default:this.anims.speed=speed}};if(root){this.anims.root=root;}};this.rmtips=function(time){setTimeout('this.close()',time)};this.close=function(){if($("#PopTips").is("div")){switch(this.anims.type){case'slide':$("#PopTips").slideUp(this.anims.speed);break;case'fade':$("#PopTips").fadeOut(this.anims.speed);break;case'show':$("#PopTips").hide(this.anims.speed);break;default:$("#PopTips").slideUp(this.anims.speed);break};setTimeout('$("#PopTips").remove();',this.anims.speed);if(this.sfuc!=null)eval(this.sfuc);this.original()}};this.original=function(){this.layer={'width':200,'height':24,'top':0};this.title='1';this.sfuc=null;this.time=4000;this.anims={'type':'slide','speed':1000,'root':'/message/'}};this.doafter=function(_sFuc){this.sfuc=_sFuc};jQuery.message=this;return jQuery})(jQuery);



/* jquery.tabs */
(function($){$.extend({tabs:{remoteCount:0}});$.fn.tabs=function(x,w){if(typeof x=='object')w=x;w=$.extend({initial:(x&&typeof x=='number'&&x>0)?--x:0,disabled:null,bookmarkable:$.ajaxHistory?true:false,remote:false,spinner:'Loading&#8230;',hashPrefix:'remote-tab-',fxFade:null,fxSlide:null,fxShow:null,fxHide:null,fxSpeed:'normal',fxShowSpeed:null,fxHideSpeed:null,fxAutoHeight:false,onClick:null,onHide:null,onShow:null,navClass:'tabs-nav',selectedClass:'tabs-selected',disabledClass:'tabs-disabled',containerClass:'tabs-container',hideClass:'tabs-hide',loadingClass:'tabs-loading',tabStruct:'div'},w||{});$.browser.msie6=$.browser.msie&&($.browser.version&&$.browser.version<7||/MSIE 6.0/.test(navigator.userAgent));function unFocus(){scrollTo(0,0)}return this.each(function(){var p=this;var r=$('ul.'+w.navClass,p);r=r.size()&&r||$('>ul:eq(0)',p);var j=$('a',r);if(w.remote){j.each(function(){var c=w.hashPrefix+(++$.tabs.remoteCount),hash='#'+c,url=this.href;this.href=hash;$('<div id="'+c+'" class="'+w.containerClass+'"></div>').appendTo(p);$(this).bind('loadRemoteTab',function(e,a){var b=$(this).addClass(w.loadingClass),span=$('span',this)[0],tabTitle=span.innerHTML;if(w.spinner){span.innerHTML='<em>'+w.spinner+'</em>'}setTimeout(function(){$(hash).load(url,function(){if(w.spinner){span.innerHTML=tabTitle}b.removeClass(w.loadingClass);a&&a()})},0)})})}var n=$('div.'+w.containerClass,p);n=n.size()&&n||$('>'+w.tabStruct,p);r.is('.'+w.navClass)||r.addClass(w.navClass);n.each(function(){var a=$(this);a.is('.'+w.containerClass)||a.addClass(w.containerClass)});var s=$('li',r).index($('li.'+w.selectedClass,r)[0]);if(s>=0){w.initial=s}if(location.hash){j.each(function(i){if(this.hash==location.hash){w.initial=i;if(($.browser.msie||$.browser.opera)&&!w.remote){var a=$(location.hash);var b=a.attr('id');a.attr('id','');setTimeout(function(){a.attr('id',b)},500)}unFocus();return false}})}if($.browser.msie){unFocus()}n.filter(':eq('+w.initial+')').show().end().not(':eq('+w.initial+')').addClass(w.hideClass);$('li',r).removeClass(w.selectedClass).eq(w.initial).addClass(w.selectedClass);j.eq(w.initial).trigger('loadRemoteTab').end();if(w.fxAutoHeight){var l=function(d){var c=$.map(n.get(),function(a){var h,jq=$(a);if(d){if($.browser.msie6){a.style.removeExpression('behaviour');a.style.height='';a.minHeight=null}h=jq.css({'min-height':''}).height()}else{h=jq.height()}return h}).sort(function(a,b){return b-a});if($.browser.msie6){n.each(function(){this.minHeight=c[0]+'px';this.style.setExpression('behaviour','this.style.height = this.minHeight ? this.minHeight : "1px"')})}else{n.css({'min-height':c[0]+'px'})}};l();var q=p.offsetWidth;var m=p.offsetHeight;var v=$('#tabs-watch-font-size').get(0)||$('<span id="tabs-watch-font-size">M</span>').css({display:'block',position:'absolute',visibility:'hidden'}).appendTo(document.body).get(0);var o=v.offsetHeight;setInterval(function(){var b=p.offsetWidth;var a=p.offsetHeight;var c=v.offsetHeight;if(a>m||b!=q||c!=o){l((b>q||c<o));q=b;m=a;o=c}},50)}var u={},hideAnim={},showSpeed=w.fxShowSpeed||w.fxSpeed,hideSpeed=w.fxHideSpeed||w.fxSpeed;if(w.fxSlide||w.fxFade){if(w.fxSlide){u['height']='show';hideAnim['height']='hide'}if(w.fxFade){u['opacity']='show';hideAnim['opacity']='hide'}}else{if(w.fxShow){u=w.fxShow}else{u['min-width']=0;showSpeed=1}if(w.fxHide){hideAnim=w.fxHide}else{hideAnim['min-width']=0;hideSpeed=1}}var t=w.onClick,onHide=w.onHide,onShow=w.onShow;j.bind('triggerTab',function(){var c=$(this).parents('li:eq(0)');if(p.locked||c.is('.'+w.selectedClass)||c.is('.'+w.disabledClass)){return false}var a=this.hash;if($.browser.msie){$(this).trigger('click');if(w.bookmarkable){$.ajaxHistory.update(a);location.hash=a.replace('#','')}}else if($.browser.safari){var b=$('<form action="'+a+'"><div><input type="submit" value="h" /></div></form>').get(0);b.submit();$(this).trigger('click');if(w.bookmarkable){$.ajaxHistory.update(a)}}else{if(w.bookmarkable){location.hash=a.replace('#','')}else{$(this).trigger('click')}}});j.bind('disableTab',function(){var a=$(this).parents('li:eq(0)');if($.browser.safari){a.animate({opacity:0},1,function(){a.css({opacity:''})})}a.addClass(w.disabledClass)});if(w.disabled&&w.disabled.length){for(var i=0,k=w.disabled.length;i<k;i++){j.eq(--w.disabled[i]).trigger('disableTab').end()}};j.bind('enableTab',function(){var a=$(this).parents('li:eq(0)');a.removeClass(w.disabledClass);if($.browser.safari){a.animate({opacity:1},1,function(){a.css({opacity:''})})}});j.bind('click',function(e){var g=e.clientX;var d=this,li=$(this).parents('li:eq(0)'),toShow=$(this.hash),toHide=n.filter(':visible');if(p['locked']||li.is('.'+w.selectedClass)||li.is('.'+w.disabledClass)||typeof t=='function'&&t(this,toShow[0],toHide[0])===false){this.blur();return false}p['locked']=true;if(toShow.size()){if($.browser.msie&&w.bookmarkable){var c=this.hash.replace('#','');toShow.attr('id','');setTimeout(function(){toShow.attr('id',c)},0)}var f={display:'',overflow:'',height:''};if(!$.browser.msie){f['opacity']=''}function switchTab(){if(w.bookmarkable&&g){$.ajaxHistory.update(d.hash)}toHide.animate(hideAnim,hideSpeed,function(){$(d).parents('li:eq(0)').addClass(w.selectedClass).siblings().removeClass(w.selectedClass);toHide.addClass(w.hideClass).css(f);if(typeof onHide=='function'){onHide(d,toShow[0],toHide[0])}if(!(w.fxSlide||w.fxFade||w.fxShow)){toShow.css('display','block')}toShow.animate(u,showSpeed,function(){toShow.removeClass(w.hideClass).css(f);if($.browser.msie){toHide[0].style.filter='';toShow[0].style.filter=''}if(typeof onShow=='function'){onShow(d,toShow[0],toHide[0])}p['locked']=null})})}if(!w.remote){switchTab()}else{$(d).trigger('loadRemoteTab',[switchTab])}}else{alert('There is no such container.')}var a=window.pageXOffset||document.documentElement&&document.documentElement.scrollLeft||document.body.scrollLeft||0;var b=window.pageYOffset||document.documentElement&&document.documentElement.scrollTop||document.body.scrollTop||0;setTimeout(function(){window.scrollTo(a,b)},0);this.blur();return w.bookmarkable&&!!g});if(w.bookmarkable){$.ajaxHistory.initialize(function(){j.eq(w.initial).trigger('click').end()})}})};var y=['triggerTab','disableTab','enableTab'];for(var i=0;i<y.length;i++){$.fn[y[i]]=(function(d){return function(c){return this.each(function(){var b=$('ul.tabs-nav',this);b=b.size()&&b||$('>ul:eq(0)',this);var a;if(!c||typeof c=='number'){a=$('li a',b).eq((c&&c>0&&c-1||0))}else if(typeof c=='string'){a=$('li a[@href$="#'+c+'"]',b)}a.trigger(d)})}})(y[i])}$.fn.activeTab=function(){var c=[];this.each(function(){var a=$('ul.tabs-nav',this);a=a.size()&&a||$('>ul:eq(0)',this);var b=$('li',a);c.push(b.index(b.filter('.tabs-selected')[0])+1)});return c[0]}})(jQuery);


(function($) { 
	$.fn.jFloat = function(o) {
		o = $.extend({
			top:60,  //广告距页面顶部距离
			left:0,//广告左侧距离
			right:0,//广告右侧距离
			width:100,  //广告容器的宽度
			height:360, //广告容器的高度
			minScreenW:800,//出现广告的最小屏幕宽度，当屏幕分辨率小于此，将不出现对联广告
			position:"left"
		}, o || {});
		var h=o.height;
		var fDiv=$(this);
		if(o.minScreenW>=$(window).width()){
			fDiv.hide();
			showAd=false;
		}
		else{
			fDiv.css("display","block");
			switch(o.position){
			case "left":
				fDiv.css({position:"absolute",left:o.left+"px",top:o.top+"px",width:o.width+"px",height:h+"px",overflow:"hidden"});
				break;
			case "right":
				fDiv.css({position:"absolute",left:"auto",right:o.right+"px",top:o.top+"px",width:o.width+"px",height:h+"px",overflow:"hidden"});
				break;
			};
		};
		function ylFloat(){
			var windowTop=$(window).scrollTop();
			if(fDiv.css("display")!="none")
				fDiv.css("top",o.top+windowTop+"px");
		};

		$(window).scroll(ylFloat);
		$(document).ready(ylFloat);     
       
	}; 
})(jQuery);


/* jquery.jTemplates */
if(window.jQuery&&!window.jQuery.createTemplate){(function(){var Template=function(s,includes,settings){this._tree=[];this._param={};this._includes=null;this._templates={};this._templates_code={};this.settings=jQuery.extend({disallow_functions:false,filter_data:true,filter_params:false,runnable_functions:false,clone_data:true,clone_params:true},settings);this.f_cloneData=(this.settings.f_cloneData!==undefined)?(this.settings.f_cloneData):(TemplateUtils.cloneData);this.f_escapeString=(this.settings.f_escapeString!==undefined)?(this.settings.f_escapeString):(TemplateUtils.escapeHTML);this.splitTemplates(s,includes);if(s){this.setTemplate(this._templates_code['MAIN'],includes,this.settings)}this._templates_code=null};Template.prototype.version='0.7.5';Template.DEBUG_MODE=true;Template.prototype.splitTemplates=function(s,includes){var reg=/\{#template *(\w*?)( .*)*\}/g;var iter,tname,se;var lastIndex=null;var _template_settings=[];while((iter=reg.exec(s))!=null){lastIndex=reg.lastIndex;tname=iter[1];se=s.indexOf('{#/template '+tname+'}',lastIndex);if(se==-1){throw new Error('jTemplates: Template "'+tname+'" is not closed.');}this._templates_code[tname]=s.substring(lastIndex,se);_template_settings[tname]=TemplateUtils.optionToObject(iter[2])}if(lastIndex===null){this._templates_code['MAIN']=s;return}for(var i in this._templates_code){if(i!='MAIN'){this._templates[i]=new Template()}}for(var i in this._templates_code){if(i!='MAIN'){this._templates[i].setTemplate(this._templates_code[i],jQuery.extend({},includes||{},this._templates||{}),jQuery.extend({},this.settings,_template_settings[i]));this._templates_code[i]=null}}};Template.prototype.setTemplate=function(s,includes,settings){if(s==undefined){this._tree.push(new TextNode('',1));return}s=s.replace(/[\n\r]/g,'');s=s.replace(/\{\*.*?\*\}/g,'');this._includes=jQuery.extend({},this._templates||{},includes||{});this.settings=new Object(settings);var node=this._tree;var op=s.match(/\{#.*?\}/g);var ss=0,se=0;var e;var literalMode=0;var elseif_level=0;for(var i=0,l=(op)?(op.length):(0);i<l;++i){if(literalMode){se=s.indexOf('{#/literal}');if(se==-1){throw new Error("jTemplates: No end of literal.");}if(se>ss){node.push(new TextNode(s.substring(ss,se),1))}ss=se+11;literalMode=0;i=jQuery.inArray('{#/literal}',op);continue}se=s.indexOf(op[i],ss);if(se>ss){node.push(new TextNode(s.substring(ss,se),literalMode))}var ppp=op[i].match(/\{#([\w\/]+).*?\}/);var op_=RegExp.$1;switch(op_){case'elseif':++elseif_level;node.switchToElse();case'if':e=new opIF(op[i],node);node.push(e);node=e;break;case'else':node.switchToElse();break;case'/if':while(elseif_level){node=node.getParent();--elseif_level}case'/for':case'/foreach':node=node.getParent();break;case'foreach':e=new opFOREACH(op[i],node,this);node.push(e);node=e;break;case'for':e=opFORFactory(op[i],node,this);node.push(e);node=e;break;case'include':node.push(new Include(op[i],this._includes));break;case'param':node.push(new UserParam(op[i]));break;case'cycle':node.push(new Cycle(op[i]));break;case'ldelim':node.push(new TextNode('{',1));break;case'rdelim':node.push(new TextNode('}',1));break;case'literal':literalMode=1;break;case'/literal':if(Template.DEBUG_MODE){throw new Error("jTemplates: No begin of literal.");}break;default:if(Template.DEBUG_MODE){throw new Error('jTemplates: unknown tag '+op_+'.');}}ss=se+op[i].length}if(s.length>ss){node.push(new TextNode(s.substr(ss),literalMode))}};Template.prototype.get=function(d,param,element,deep){++deep;var $T=d,_param1,_param2;if(this.settings.clone_data){$T=this.f_cloneData(d,{escapeData:(this.settings.filter_data&&deep==1),noFunc:this.settings.disallow_functions},this.f_escapeString)}if(!this.settings.clone_params){_param1=this._param;_param2=param}else{_param1=this.f_cloneData(this._param,{escapeData:(this.settings.filter_params),noFunc:false},this.f_escapeString);_param2=this.f_cloneData(param,{escapeData:(this.settings.filter_params&&deep==1),noFunc:false},this.f_escapeString)}var $P=jQuery.extend({},_param1,_param2);var $Q=element;$Q.version=this.version;var ret='';for(var i=0,l=this._tree.length;i<l;++i){ret+=this._tree[i].get($T,$P,$Q,deep)}--deep;return ret};Template.prototype.setParam=function(name,value){this._param[name]=value};TemplateUtils=function(){};TemplateUtils.escapeHTML=function(txt){return txt.replace(/&/g,'&amp;').replace(/>/g,'&gt;').replace(/</g,'&lt;').replace(/"/g,'&quot;').replace(/'/g,'&#39;')};TemplateUtils.cloneData=function(d,filter,f_escapeString){if(d==null){return d}switch(d.constructor){case Object:var o={};for(var i in d){o[i]=TemplateUtils.cloneData(d[i],filter,f_escapeString)}if(!filter.noFunc){if(d.hasOwnProperty("toString"))o.toString=d.toString}return o;case Array:var o=[];for(var i=0,l=d.length;i<l;++i){o[i]=TemplateUtils.cloneData(d[i],filter,f_escapeString)}return o;case String:return(filter.escapeData)?(f_escapeString(d)):(d);case Function:if(filter.noFunc){if(Template.DEBUG_MODE)throw new Error("jTemplates: Functions are not allowed.");else return undefined}default:return d}};TemplateUtils.optionToObject=function(optionText){if(optionText===null||optionText===undefined){return{}}var o=optionText.split(/[= ]/);if(o[0]===''){o.shift()}var obj={};for(var i=0,l=o.length;i<l;i+=2){obj[o[i]]=o[i+1]}return obj};var TextNode=function(val,literalMode){this._value=val;this._literalMode=literalMode};TextNode.prototype.get=function(d,param,element,deep){var t=this._value;if(!this._literalMode){var $T=d;var $P=param;var $Q=element;t=t.replace(/\{(.*?)\}/g,function(__a0,__a1){try{var tmp=eval(__a1);if(typeof tmp=='function'){var settings=jQuery.data(element,'jTemplate').settings;if(settings.disallow_functions||!settings.runnable_functions){return''}else{tmp=tmp($T,$P,$Q)}}return(tmp===undefined)?(""):(String(tmp))}catch(e){if(Template.DEBUG_MODE)throw e;return""}})}return t};var opIF=function(oper,par){this._parent=par;oper.match(/\{#(?:else)*if (.*?)\}/);this._cond=RegExp.$1;this._onTrue=[];this._onFalse=[];this._currentState=this._onTrue};opIF.prototype.push=function(e){this._currentState.push(e)};opIF.prototype.getParent=function(){return this._parent};opIF.prototype.switchToElse=function(){this._currentState=this._onFalse};opIF.prototype.get=function(d,param,element,deep){var $T=d;var $P=param;var $Q=element;var ret='';try{var tab=(eval(this._cond))?(this._onTrue):(this._onFalse);for(var i=0,l=tab.length;i<l;++i){ret+=tab[i].get(d,param,element,deep)}}catch(e){if(Template.DEBUG_MODE)throw e;}return ret};opFORFactory=function(oper,par,template){if(oper.match(/\{#for (\w+?) *= *(\S+?) +to +(\S+?) *(?:step=(\S+?))*\}/)){oper='{#foreach opFORFactory.funcIterator as '+RegExp.$1+' begin='+(RegExp.$2||0)+' end='+(RegExp.$3||-1)+' step='+(RegExp.$4||1)+' extData=$T}';return new opFOREACH(oper,par,template)}else{throw new Error('jTemplates: Operator failed "find": '+oper);}};opFORFactory.funcIterator=function(i){return i};var opFOREACH=function(oper,par,template){this._parent=par;this._template=template;oper.match(/\{#foreach (.+?) as (\w+?)( .+)*\}/);this._arg=RegExp.$1;this._name=RegExp.$2;this._option=RegExp.$3||null;this._option=TemplateUtils.optionToObject(this._option);this._onTrue=[];this._onFalse=[];this._currentState=this._onTrue};opFOREACH.prototype.push=function(e){this._currentState.push(e)};opFOREACH.prototype.getParent=function(){return this._parent};opFOREACH.prototype.switchToElse=function(){this._currentState=this._onFalse};opFOREACH.prototype.get=function(d,param,element,deep){try{var $T=d;var $P=param;var $Q=element;var fcount=eval(this._arg);var key=[];var mode=typeof fcount;if(mode=='object'){var arr=[];jQuery.each(fcount,function(k,v){key.push(k);arr.push(v)});fcount=arr}var extData=(this._option.extData!==undefined)?(eval(this._option.extData)):{};var s=Number(eval(this._option.begin)||0),e;var step=Number(eval(this._option.step)||1);if(mode!='function'){e=fcount.length}else{if(this._option.end===undefined||this._option.end===null){e=Number.MAX_VALUE}else{e=Number(eval(this._option.end))+((step>0)?(1):(-1))}}var ret='';var i,l;if(this._option.count){var tmp=s+Number(eval(this._option.count));e=(tmp>e)?(e):(tmp)}if((e>s&&step>0)||(e<s&&step<0)){var iteration=0;var _total=(mode!='function')?(Math.ceil((e-s)/step)):undefined;var ckey,cval;for(;((step>0)?(s<e):(s>e));s+=step,++iteration){ckey=key[s];if(mode!='function'){cval=fcount[s]}else{cval=fcount(s);if(cval===undefined||cval===null){break}}if((typeof cval=='function')&&(this._template.settings.disallow_functions||!this._template.settings.runnable_functions)){continue}if((mode=='object')&&(ckey in Object)){continue}$T=extData;var p=$T[this._name]=cval;$T[this._name+'$index']=s;$T[this._name+'$iteration']=iteration;$T[this._name+'$first']=(iteration==0);$T[this._name+'$last']=(s+step>=e);$T[this._name+'$total']=_total;$T[this._name+'$key']=(ckey!==undefined&&ckey.constructor==String)?(this._template.f_escapeString(ckey)):(ckey);$T[this._name+'$typeof']=typeof cval;for(i=0,l=this._onTrue.length;i<l;++i){ret+=this._onTrue[i].get($T,param,element,deep)}delete $T[this._name+'$index'];delete $T[this._name+'$iteration'];delete $T[this._name+'$first'];delete $T[this._name+'$last'];delete $T[this._name+'$total'];delete $T[this._name+'$key'];delete $T[this._name+'$typeof'];delete $T[this._name]}}else{for(i=0,l=this._onFalse.length;i<l;++i){ret+=this._onFalse[i].get($T,param,element,deep)}}return ret}catch(e){if(Template.DEBUG_MODE)throw e;return""}};var Include=function(oper,includes){oper.match(/\{#include (.*?)(?: root=(.*?))?\}/);this._template=includes[RegExp.$1];if(this._template==undefined){if(Template.DEBUG_MODE)throw new Error('jTemplates: Cannot find include: '+RegExp.$1);}this._root=RegExp.$2};Include.prototype.get=function(d,param,element,deep){var $T=d;try{return this._template.get(eval(this._root),param,element,deep)}catch(e){if(Template.DEBUG_MODE)throw e;}};var UserParam=function(oper){oper.match(/\{#param name=(\w*?) value=(.*?)\}/);this._name=RegExp.$1;this._value=RegExp.$2};UserParam.prototype.get=function(d,param,element,deep){var $T=d;var $P=param;var $Q=element;try{param[this._name]=eval(this._value)}catch(e){if(Template.DEBUG_MODE)throw e;param[this._name]=undefined}return''};var Cycle=function(oper){oper.match(/\{#cycle values=(.*?)\}/);this._values=eval(RegExp.$1);this._length=this._values.length;if(this._length<=0){throw new Error('jTemplates: cycle has no elements');}this._index=0;this._lastSessionID=-1};Cycle.prototype.get=function(d,param,element,deep){var sid=jQuery.data(element,'jTemplateSID');if(sid!=this._lastSessionID){this._lastSessionID=sid;this._index=0}var i=this._index++%this._length;return this._values[i]};jQuery.fn.setTemplate=function(s,includes,settings){if(s.constructor===Template){return jQuery(this).each(function(){jQuery.data(this,'jTemplate',s);jQuery.data(this,'jTemplateSID',0)})}else{return jQuery(this).each(function(){jQuery.data(this,'jTemplate',new Template(s,includes,settings));jQuery.data(this,'jTemplateSID',0)})}};jQuery.fn.setTemplateURL=function(url_,includes,settings){var s=jQuery.ajax({url:url_,async:false}).responseText;return jQuery(this).setTemplate(s,includes,settings)};jQuery.fn.setTemplateElement=function(elementName,includes,settings){var s=$('#'+elementName).val();if(s==null){s=$('#'+elementName).html();s=s.replace(/&lt;/g,"<").replace(/&gt;/g,">")}s=jQuery.trim(s);s=s.replace(/^<\!\[CDATA\[([\s\S]*)\]\]>$/im,'$1');s=s.replace(/^<\!--([\s\S]*)-->$/im,'$1');return jQuery(this).setTemplate(s,includes,settings)};jQuery.fn.hasTemplate=function(){var count=0;jQuery(this).each(function(){if(jQuery.data(this,'jTemplate')){++count}});return count};jQuery.fn.removeTemplate=function(){jQuery(this).processTemplateStop();return jQuery(this).each(function(){jQuery.removeData(this,'jTemplate')})};jQuery.fn.setParam=function(name,value){return jQuery(this).each(function(){var t=jQuery.data(this,'jTemplate');if(t===undefined){if(Template.DEBUG_MODE)throw new Error('jTemplates: Template is not defined.');else return}t.setParam(name,value)})};jQuery.fn.processTemplate=function(d,param){return jQuery(this).each(function(){var t=jQuery.data(this,'jTemplate');if(t===undefined){if(Template.DEBUG_MODE)throw new Error('jTemplates: Template is not defined.');else return}jQuery.data(this,'jTemplateSID',jQuery.data(this,'jTemplateSID')+1);jQuery(this).html(t.get(d,param,this,0))})};jQuery.fn.processTemplateURL=function(url_,param,options){var that=this;options=jQuery.extend({type:'GET',async:true,cache:false},options);jQuery.ajax({url:url_,type:options.type,data:options.data,dataFilter:options.dataFilter,async:options.async,cache:options.cache,timeout:options.timeout,dataType:'json',success:function(d){var r=jQuery(that).processTemplate(d,param);if(options.on_success){options.on_success(r)}},error:options.on_error,complete:options.on_complete});return this};var Updater=function(url,param,interval,args,objs,options){this._url=url;this._param=param;this._interval=interval;this._args=args;this.objs=objs;this.timer=null;this._options=options||{};var that=this;jQuery(objs).each(function(){jQuery.data(this,'jTemplateUpdater',that)});this.run()};Updater.prototype.run=function(){this.detectDeletedNodes();if(this.objs.length==0){return}var that=this;jQuery.getJSON(this._url,this._args,function(d){var r=jQuery(that.objs).processTemplate(d,that._param);if(that._options.on_success){that._options.on_success(r)}});this.timer=setTimeout(function(){that.run()},this._interval)};Updater.prototype.detectDeletedNodes=function(){this.objs=jQuery.grep(this.objs,function(o){if(jQuery.browser.msie){var n=o.parentNode;while(n&&n!=document){n=n.parentNode}return n!=null}else{return o.parentNode!=null}})};jQuery.fn.processTemplateStart=function(url,param,interval,args,options){return new Updater(url,param,interval,args,this,options)};jQuery.fn.processTemplateStop=function(){return jQuery(this).each(function(){var updater=jQuery.data(this,'jTemplateUpdater');if(updater==null){return}var that=this;updater.objs=jQuery.grep(updater.objs,function(o){return o!=that});jQuery.removeData(this,'jTemplateUpdater')})};jQuery.extend({createTemplate:function(s,includes,settings){return new Template(s,includes,settings)},createTemplateURL:function(url_,includes,settings){var s=jQuery.ajax({url:url_,async:false}).responseText;return new Template(s,includes,settings)},jTemplatesDebugMode:function(value){Template.DEBUG_MODE=value}})})(jQuery)}




/* jquery.ajaxfileupload */
(function() {jQuery.extend({createUploadIframe:function(id,uri){var frameId='jUploadFrame'+id;if(window.ActiveXObject){var io=document.createElement('<iframe id="'+frameId+'" name="'+frameId+'" />');if(typeof uri=='boolean'){io.src='javascript:false'}else if(typeof uri=='string'){io.src=uri}}else{var io=document.createElement('iframe');io.id=frameId;io.name=frameId};io.style.position='absolute';io.style.top='-1000px';io.style.left='-1000px';document.body.appendChild(io);return io},createUploadForm:function(id,fileElementId){var formId='jUploadForm'+id;var fileId='jUploadFile'+id;var form=jQuery('<form  action="" method="POST" name="'+formId+'" id="'+formId+'" enctype="multipart/form-data"></form>');var oldElement=jQuery('#'+fileElementId);var newElement=jQuery(oldElement).clone();jQuery(oldElement).attr('id',fileId);jQuery(oldElement).before(newElement);jQuery(oldElement).appendTo(form);jQuery(form).css('position','absolute');jQuery(form).css('top','-1200px');jQuery(form).css('left','-1200px');jQuery(form).appendTo('body');return form},ajaxFileUpload:function(s){s=jQuery.extend({},jQuery.ajaxSettings,s);var id=new Date().getTime();var form=jQuery.createUploadForm(id,s.fileElementId);var io=jQuery.createUploadIframe(id,s.secureuri);var frameId='jUploadFrame'+id;var formId='jUploadForm'+id;if(s.global&&!jQuery.active++){jQuery.event.trigger("ajaxStart")}var requestDone=false;var xml={};if(s.global)jQuery.event.trigger("ajaxSend",[xml,s]);var uploadCallback=function(isTimeout){var io=document.getElementById(frameId);try{if(io.contentWindow){xml.responseText=io.contentWindow.document.body?io.contentWindow.document.body.innerHTML:null;xml.responseXML=io.contentWindow.document.XMLDocument?io.contentWindow.document.XMLDocument:io.contentWindow.document}else if(io.contentDocument){xml.responseText=io.contentDocument.document.body?io.contentDocument.document.body.innerHTML:null;xml.responseXML=io.contentDocument.document.XMLDocument?io.contentDocument.document.XMLDocument:io.contentDocument.document}}catch(e){jQuery.handleError(s,xml,null,e)}if(xml||isTimeout=="timeout"){requestDone=true;var status;try{status=isTimeout!="timeout"?"success":"error";if(status!="error"){var data=jQuery.uploadHttpData(xml,s.dataType);if(s.success)s.success(data,status);if(s.global)jQuery.event.trigger("ajaxSuccess",[xml,s])}else jQuery.handleError(s,xml,status)}catch(e){status="error";jQuery.handleError(s,xml,status,e)};if(s.global)jQuery.event.trigger("ajaxComplete",[xml,s]);if(s.global&&!--jQuery.active)jQuery.event.trigger("ajaxStop");if(s.complete)s.complete(xml,status);jQuery(io).unbind();setTimeout(function(){try{jQuery(io).remove();jQuery(form).remove()}catch(e){jQuery.handleError(s,xml,null,e)}},100);xml=null}};if(s.timeout>0){setTimeout(function(){if(!requestDone)uploadCallback("timeout")},s.timeout)}try{var form=jQuery('#'+formId);jQuery(form).attr('action',s.url);jQuery(form).attr('method','POST');jQuery(form).attr('target',frameId);if(form.encoding){form.encoding='multipart/form-data'}else{form.enctype='multipart/form-data'}jQuery(form).submit()}catch(e){jQuery.handleError(s,xml,null,e)};if(window.attachEvent){document.getElementById(frameId).attachEvent('onload',uploadCallback)}else{document.getElementById(frameId).addEventListener('load',uploadCallback,false)}return{abort:function(){}}},uploadHttpData:function(r,type){var data=!type;data=type=="xml"||data?r.responseXML:r.responseText;if(type=="script")jQuery.globalEval(data);if(type=="json")eval("data = "+data);if(type=="html")jQuery("<div>").html(data).evalScripts();return data}});})(jQuery);



/* jquery.select */
(function(){jQuery.fn.getCount=function(){return jQuery(this).get(0).options.length};jQuery.fn.getSelectedIndex=function(){return jQuery(this).get(0).selectedIndex};jQuery.fn.getSelectedText=function(){if(this.getCount()==0){return"下拉框中无选项"}else{var index=this.getSelectedIndex();return jQuery(this).get(0).options[index].text}};jQuery.fn.getSelectedValue=function(){if(this.getCount()==0){return"下拉框中无选中值"}else{return jQuery(this).val()}};jQuery.fn.setSelectedValue=function(value){jQuery(this).get(0).value=value};jQuery.fn.setSelectedText=function(text){var isExist=false;var count=this.getCount();for(var i=0;i<count;i++){if(jQuery(this).get(0).options[i].text==text){jQuery(this).get(0).options[i].selected=true;isExist=true;break}}if(!isExist){alert("下拉框中不存在该项")}};jQuery.fn.setSelectedIndex=function(index){var count=this.getCount();if(index>=count||index<0){alert("选中项索引超出范围")}else{jQuery(this).get(0).selectedIndex=index}};jQuery.fn.isExistItem=function(value){var isExist=false;var count=this.getCount();for(var i=0;i<count;i++){if(jQuery(this).get(0).options[i].value==value){isExist=true;break}}return isExist};jQuery.fn.addOption=function(text,value){if(this.isExistItem(value)){alert("待添加项的值已存在")}else{jQuery(this).get(0).options.add(new Option(text,value))}};jQuery.fn.removeItem=function(value){if(this.isExistItem(value)){var count=this.getCount();for(var i=0;i<count;i++){if(jQuery(this).get(0).options[i].value==value){jQuery(this).get(0).remove(i);break}}}else{alert("待删除的项不存在!")}};jQuery.fn.removeIndex=function(index){var count=this.getCount();if(index>=count||index<0){alert("待删除项索引超出范围")}else{jQuery(this).get(0).remove(index)}};jQuery.fn.removeSelected=function(){var index=this.getSelectedIndex();this.removeIndex(index)};jQuery.fn.clearAll=function(){jQuery(this).get(0).options.length=0}})(jQuery);

/* jquery.hoverIntent */
(function($){$.fn.hoverIntent=function(f,g){var cfg={sensitivity:7,interval:100,timeout:0};cfg=$.extend(cfg,g?{over:f,out:g}:f);var cX,cY,pX,pY;var track=function(ev){cX=ev.pageX;cY=ev.pageY;};var compare=function(ev,ob){ob.hoverIntent_t=clearTimeout(ob.hoverIntent_t);if((Math.abs(pX-cX)+Math.abs(pY-cY))<cfg.sensitivity){$(ob).unbind("mousemove",track);ob.hoverIntent_s=1;return cfg.over.apply(ob,[ev]);}else{pX=cX;pY=cY;ob.hoverIntent_t=setTimeout(function(){compare(ev,ob);},cfg.interval);}};var delay=function(ev,ob){ob.hoverIntent_t=clearTimeout(ob.hoverIntent_t);ob.hoverIntent_s=0;return cfg.out.apply(ob,[ev]);};var handleHover=function(e){var p=(e.type=="mouseover"?e.fromElement:e.toElement)||e.relatedTarget;while(p&&p!=this){try{p=p.parentNode;}catch(e){p=this;}}if(p==this){return false;}var ev=jQuery.extend({},e);var ob=this;if(ob.hoverIntent_t){ob.hoverIntent_t=clearTimeout(ob.hoverIntent_t);}if(e.type=="mouseover"){pX=ev.pageX;pY=ev.pageY;$(ob).bind("mousemove",track);if(ob.hoverIntent_s!=1){ob.hoverIntent_t=setTimeout(function(){compare(ev,ob);},cfg.interval);}}else{$(ob).unbind("mousemove",track);if(ob.hoverIntent_s==1){ob.hoverIntent_t=setTimeout(function(){delay(ev,ob);},cfg.timeout);}}};return this.mouseover(handleHover).mouseout(handleHover);};})(jQuery);




/* jquery.formValidator */
var regexEnum = 
{
intege:"^-?[1-9]\\d*$",//整数
intege1:"^[1-9]\\d*$",//正整数
intege2:"^-[1-9]\\d*$",//负整数
num:"^([+-]?)\\d*\\.?\\d+$",//数字
num1:"^([+]?)\\d*\\.?\\d+$",//正数
num2:"^-\\d*\\.?\\d+$",//负数
decmal:"^([+-]?)\\d*\\.\\d+$",//浮点数
decmal1:"^([+]?)\\d*\\.\\d+$",//正浮点数
decmal2:"^-\\d*\\.\\d+$",//负浮点数
money:"^[1-9][0-9]*(\\.[0-9]{1,2})?$",//货币
email:"^\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+$", //邮件
emailgroup:"^(\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+,)*\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+$", //邮件群
color:"^[a-fA-F0-9]{6}$",//颜色
url:"^http[s]?:\\/\\/([\\w-]+\\.)+[\\w-]+([\\w-./?%&=]*)?$",//url
domain:"^http[s]?:\\/\\/([\\w-]+\\.)+[\\w-]+(:[1-9][0-9]*)?$",//domain
chinese:"^[\\u4E00-\\u9FA5\\uF900-\\uFA2D]+$",//仅中文
ascii:"^[\\x00-\\xFF]+$",//仅ACSII字符
zipcode:"^\\d{6}$",//邮编
mobile:"^(13|15|18)[0-9]{9}$",//手机
ip4:"^(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5]).(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5]).(d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5]).(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])$",//ip地址
notempty:"^\\S+$",//非空
picture:"(.*)\\.(jpg|bmp|gif|ico|pcx|jpeg|tif|png|raw|tga)$",//图片
rar:"(.*)\\.(rar|zip|7zip|tgz)$",//压缩文件
date:"^\\d{4}(\\-|\\/|\.)\\d{1,2}\\1\\d{1,2}$",//日期
datetime:"^\\d{4}(\\-)(\\d{2})\\1\\d{2} \\d{2}:\\d{2}:\\d{2}$",//长日期
qq:"^[1-9]*[1-9][0-9]*$",//QQ号码
tel:"(\\d{3}-|\\d{4}-)?(\\d{8}|\\d{7})",//国内电话
username:"^[A-Za-z\\u4E00-\\u9FA5\\uF900-\\uFA2D]+[A-Za-z0-9\\u4E00-\\u9FA5\\uF900-\\uFA2D\\-\\_]*$",//可以是中文、英文、数字、-、_
letter:"^[A-Za-z]+$",//字母
letter_u:"^[A-Z]+$",//大写字母
letter_l:"^[a-z]+$",//小写字母
idcard:"^[1-9]([0-9]{13}|[0-9]{16})[0-9a-zA-Z]$"//身份证
}

var aCity={11:"北京",12:"天津",13:"河北",14:"山西",15:"内蒙古",21:"辽宁",22:"吉林",23:"黑龙江",31:"上海",32:"江苏",33:"浙江",34:"安徽",35:"福建",36:"江西",37:"山东",41:"河南",42:"湖北",43:"湖南",44:"广东",45:"广西",46:"海南",50:"重庆",51:"四川",52:"贵州",53:"云南",54:"西藏",61:"陕西",62:"甘肃",63:"青海",64:"宁夏",65:"新疆",71:"台湾",81:"香港",82:"澳门",91:"国外"} 

function isCardID(sId){ 
var iSum=0 ;
var info="" ;
if(!/^\d{17}(\d|x)$/i.test(sId)) return "你输入的身份证长度或格式错误"; 
sId=sId.replace(/x$/i,"a"); 
if(aCity[parseInt(sId.substr(0,2))]==null) return "你的身份证地区非法"; 
sBirthday=sId.substr(6,4)+"-"+Number(sId.substr(10,2))+"-"+Number(sId.substr(12,2)); 
var d=new Date(sBirthday.replace(/-/g,"/")) ;
if(sBirthday!=(d.getFullYear()+"-"+ (d.getMonth()+1) + "-" + d.getDate()))return "身份证上的出生日期非法"; 
for(var i = 17;i>=0;i --) iSum += (Math.pow(2,i) % 11) * parseInt(sId.charAt(17 - i),11) ;
if(iSum%11!=1) return "你输入的身份证号非法"; 
return true;//aCity[parseInt(sId.substr(0,2))]+","+sBirthday+","+(sId.substr(16,1)%2?"男":"女") 
}

var jQuery_formValidator_initConfig; (function () {
    jQuery.formValidator = {
        sustainType: function (id, setting) {
            var elem = jQuery("#" + id).get(0);
            var srcTag = elem.tagName;
            var stype = elem.type;
            switch (setting.validateType) {
                case "InitValidator":
                    return true;
                case "InputValidator":
                    if (srcTag == "INPUT" || srcTag == "TEXTAREA" || srcTag == "SELECT") {
                        return true
                    } else {
                        return false
                    }
                case "CompareValidator":
                    if (srcTag == "INPUT" || srcTag == "TEXTAREA") {
                        if (stype == "checkbox" || stype == "radio") {
                            return false
                        } else {
                            return true
                        }
                    }
                    return false;
                case "AjaxValidator":
                    if (stype == "text" || stype == "textarea" || stype == "file" || stype == "select-one") {
                        return true
                    } else {
                        return false
                    }
                case "RegexValidator":
                    if (srcTag == "INPUT" || srcTag == "TEXTAREA") {
                        if (stype == "checkbox" || stype == "radio") {
                            return false
                        } else {
                            return true
                        }
                    }
                    return false;
                case "FunctionValidator":
                    return true
            }
        },
        initConfig: function (controlOptions) {
            var settings = {
                validatorGroup: "1",
                alertMessage: false,
                onSuccess: function () {
                    return true
                },
                onError: function () { },
                submitOnce: false
            };
            controlOptions = controlOptions || {};
            jQuery.extend(settings, controlOptions);
            if (jQuery_formValidator_initConfig == null) {
                jQuery_formValidator_initConfig = new Array()
            }
            jQuery_formValidator_initConfig.push(settings)
        },
        appendValid: function (id, setting) {
            if (!$.formValidator.sustainType(id, setting)) return -1;
            var srcjo = jQuery("#" + id).get(0);
            if (setting.validateType == "InitValidator" || !srcjo.settings || srcjo.settings == undefined) {
                srcjo.settings = new Array()
            }
            var len = srcjo.settings.push(setting);
            srcjo.settings[len - 1].index = len - 1;
            return len - 1
        },
        getInitConfig: function (validatorGroup) {
            if (jQuery_formValidator_initConfig != null) {
                for (i = 0; i < jQuery_formValidator_initConfig.length; i++) {
                    if (validatorGroup == jQuery_formValidator_initConfig[i].validatorGroup) {
                        return jQuery_formValidator_initConfig[i]
                    }
                }
            }
            return null
        },
        triggerValidate: function (returnObj) {
            switch (returnObj.setting.validateType) {
                case "InputValidator":
                    $.formValidator.InputValid(returnObj);
                    break;
                case "CompareValidator":
                    $.formValidator.CompareValid(returnObj);
                    break;
                case "AjaxValidator":
                    $.formValidator.AjaxValid(returnObj);
                    break;
                case "RegexValidator":
                    $.formValidator.RegexValid(returnObj);
                    break;
                case "FunctionValidator":
                    $.formValidator.FunctionValid(returnObj);
                    break
            }
        },
        SetTipState: function (tipid, showclass, showmsg) {
            var tip = jQuery("#" + tipid);
            tip.removeClass();
            tip.addClass(showclass);
            tip.html(showmsg);
            tip.attr("title", showmsg)
        },
        SetFailState: function (tipid, showmsg) {
            var tip = jQuery("#" + tipid);
            tip.removeClass();
            tip.addClass("onError");
            tip.html(showmsg);
            tip.attr("title", showmsg);
            alert(tip.width())
        },
        ShowMessage: function (returnObj) {
            var id = returnObj.id;
            var isValid = returnObj.isValid;
            var setting = returnObj.setting;
            var showmsg = "";
            var showclass = "onNormal";
            var settings = jQuery("#" + id).get(0).settings;
            if (!isValid) {
                if (setting.validateType == "AjaxValidator") {
                    if (setting.lastValid == "") {
                        showclass = "onLoad";
                        showmsg = setting.onwait
                    } else {
                        showclass = "onError";
                        showmsg = setting.onerror
                    }
                } else {
                    showmsg = (returnObj.errormsg == "" ? setting.onerror : returnObj.errormsg);
                    showclass = "onError"
                }
                if ($.formValidator.getInitConfig(settings[0].validatorGroup).alertMessage) {
                    var elem = jQuery("#" + id).get(0);
                    if (elem.validoldvalue != jQuery(elem).val()) alert(showmsg)
                } else {
                    $.formValidator.SetTipState(settings[0].tipid, showclass, showmsg)
                }
            } else {
                if (!$.formValidator.getInitConfig(setting.validatorGroup).alertMessage) {
                    var showmsg = "";
                    if ($.formValidator.IsEmpty(id)) {
                        showmsg = setting.onempty
                    } else {
                        showmsg = (setting.oncorrect ? setting.oncorrect : '')
                    }
                    $.formValidator.SetTipState(setting.tipid, "onSuccess", showmsg)
                }
            }
        },
        GetLength: function (id) {
            var srcjo = jQuery("#" + id);
            sType = srcjo.get(0).type;
            var len = 0;
            switch (sType) {
                case "text":
                case "hidden":
                case "password":
                case "textarea":
                case "file":
                    var val = srcjo.val();
                    for (var i = 0; i < val.length; i++) {
                        if (val.charCodeAt(i) >= 0x4e00 && val.charCodeAt(i) <= 0x9fa5) {
                            len += 2
                        } else {
                            len++
                        }
                    }
                    break;
                case "checkbox":
                case "radio":
                    len = jQuery("input[@type='" + sType + "'][@name='" + srcjo.attr("name") + "'][@checked]").length;
                    break;
                case "select-one":
                    len = srcjo.get(0).options ? srcjo.get(0).options.selectedIndex : -1;
                    break;
                case "select-more":
                    break
            }
            return len
        },
        IsEmpty: function (id) {
            if (jQuery("#" + id).get(0).settings[0].empty && $.formValidator.GetLength(id) == 0) {
                return true
            } else {
                return false
            }
        },
        IsOneValid: function (id) {
            return OneIsValid(id, 1).isValid
        },
        OneIsValid: function (id, index) {
            var returnObj = new Object();
            returnObj.id = id;
            returnObj.ajax = -1;
            returnObj.errormsg = "";
            var elem = jQuery("#" + id).get(0);
            var settings = elem.settings;
            var settingslen = settings.length;
            if ($.formValidator.IsEmpty(id)) {
                returnObj.setting = settings[0];
                returnObj.isValid = true
            } else {
                for (var i = index; i < settingslen; i++) {
                    returnObj.setting = settings[i];
                    if (settings[i].validateType != "AjaxValidator") {
                        $.formValidator.triggerValidate(returnObj)
                    } else {
                        returnObj.ajax = i
                    }
                    if (!settings[i].isValid) {
                        returnObj.isValid = false;
                        returnObj.setting = settings[i];
                        break
                    } else {
                        returnObj.isValid = true;
                        returnObj.setting = settings[0];
                        if (settings[i].validateType == "AjaxValidator") break
                    }
                }
            }
            if (returnObj.isValid) {
                var lb_ret = returnObj.setting.onvalid(jQuery("#" + id).get(0), jQuery("#" + id).val());
                if (lb_ret != undefined) {
                    if (typeof lb_ret == "string") {
                        returnObj.errormsg = lb_ret;
                        returnObj.isValid = false
                    } else {
                        settings[settings.length - 1].isValid = lb_ret;
                        returnObj.isValid = lb_ret
                    }
                }
            }
            return returnObj
        },
        PageIsValid: function (validatorGroup) {
            if (validatorGroup == null || validatorGroup == undefined) validatorGroup = "1";
            var isValid = true;
            var thefirstid = "";
            var returnObj, setting;
            var error_tip = "^";
            jQuery("form").each(function (i, form1) {
                for (i = 0; i < form1.elements.length; i++) {
                    elem = form1.elements[i];
                    if (elem.settings != undefined && elem.settings != null) {
                        if (elem.settings[0].validatorGroup == validatorGroup) {
                            if ($.formValidator.getInitConfig(validatorGroup).alertMessage) {
                                if (isValid) {
                                    returnObj = $.formValidator.OneIsValid(elem.id, 1);
                                    if (!returnObj.isValid) {
                                        $.formValidator.ShowMessage(returnObj);
                                        isValid = false;
                                        if (thefirstid == "") {
                                            thefirstid = returnObj.id;
                                            setting = returnObj.setting
                                        }
                                    }
                                }
                            } else {
                                returnObj = $.formValidator.OneIsValid(elem.id, 1);
                                if (!returnObj.isValid) {
                                    isValid = false;
                                    if (thefirstid == "") {
                                        thefirstid = returnObj.id;
                                        setting = returnObj.setting
                                    }
                                    if (error_tip.indexOf("^" + elem.settings[0].tipid + "^") == -1) {
                                        error_tip = error_tip + elem.settings[0].tipid + "^";
                                        $.formValidator.ShowMessage(returnObj)
                                    }
                                } else {
                                    if (error_tip.indexOf("^" + elem.settings[0].tipid + "^") == -1) {
                                        $.formValidator.ShowMessage(returnObj)
                                    }
                                }
                            }
                        }
                    }
                }
            });
            if (isValid) {
                isValid = $.formValidator.getInitConfig(validatorGroup).onSuccess();
                if ($.formValidator.getInitConfig(validatorGroup).submitOnce) {
                    jQuery("input[@type='submit']").attr("disabled", true)
                }
            } else {
                $.formValidator.getInitConfig(validatorGroup).onError(setting.onerror, jQuery("#" + thefirstid));
                if (thefirstid != "") {
                    jQuery("#" + thefirstid).focus()
                }
            }
            return isValid
        },
        AjaxValid: function (returnObj) {
            var id = returnObj.id;
            var srcjo = jQuery("#" + id);
            var setting = srcjo.get(0).settings[returnObj.ajax];
            var ls_url = setting.url;
            if (srcjo.size() == 0 && srcjo.get(0).settings[0].empty) {
                returnObj.setting = jQuery("#" + id).get(0).settings[0];
                returnObj.isValid = true;
                $.formValidator.ShowMessage(returnObj);
                setting.isValid = true;
                return
            }
            if (setting.addidvalue) {
                var parm = id + "=" + escape(srcjo.val());
                ls_url = ls_url + ((ls_url).indexOf("?") > 0 ? ("&" + parm) : ("?" + parm))
            }
            jQuery.ajax({
                mode: "abort",
                type: setting.type,
                url: ls_url,
                data: setting.data,
                async: setting.async,
                dataType: setting.datatype,
                success: function (data) {
                    setting0 = srcjo.get(0).settings[0];
                    if (setting.success(data)) {
                        $.formValidator.SetTipState(setting0.tipid, "onSuccess", (setting0.oncorrect ? setting0.oncorrect : ''));
                        setting.isValid = true
                    } else {
                        $.formValidator.SetTipState(setting0.tipid, "onError", setting.onerror);
                        setting.isValid = false
                    }
                },
                complete: function () {
                    if (setting.buttons && setting.buttons.length > 0) setting.buttons.attr({
                        "disabled": false
                    });
                    setting.complete
                },
                beforeSend: function () {
                    if (setting.buttons && setting.buttons.length > 0) setting.buttons.attr({
                        "disabled": true
                    });
                    var isvalid = setting.beforesend();
                    if (isvalid) setting.isValid = false;
                    setting.lastValid = "-1";
                    return setting.beforesend
                },
                error: function () {
                    setting0 = srcjo.get(0).settings[0];
                    $.formValidator.SetTipState(setting0.tipid, "onError", setting.onerror);
                    setting.isValid = false;
                    setting.error()
                },
                processData: setting.processdata
            })
        },
        RegexValid: function (returnObj) {
            var id = returnObj.id;
            var setting = returnObj.setting;
            var srcTag = jQuery("#" + id).get(0).tagName;
            var elem = jQuery("#" + id).get(0);
            if (elem.settings[0].empty && elem.value == "") {
                setting.isValid = true
            } else {
                var regexpress = setting.regexp;
                if (setting.datatype == "enum") {
                    regexpress = eval("regexEnum." + regexpress)
                }
                if (regexpress == undefined || regexpress == "") {
                    setting.isValid = false;
                    return
                }
                var exp = new RegExp(regexpress, setting.param);
                if (exp.test(jQuery("#" + id).val())) {
                    setting.isValid = true
                } else {
                    setting.isValid = false
                }
            }
        },
        FunctionValid: function (returnObj) {
            var id = returnObj.id;
            var setting = returnObj.setting;
            var srcjo = jQuery("#" + id);
            var lb_ret = setting.fun(srcjo.val(), srcjo.get(0));
            if (lb_ret != undefined) {
                if (typeof lb_ret == "string") {
                    setting.isValid = false;
                    returnObj.errormsg = lb_ret
                } else {
                    setting.isValid = lb_ret
                }
            }
        },
        InputValid: function (returnObj) {
            var id = returnObj.id;
            var setting = returnObj.setting;
            var srcjo = jQuery("#" + id);
            var elem = srcjo.get(0);
            var val = srcjo.val();
            var sType = elem.type;
            var len = $.formValidator.GetLength(id);
            switch (sType) {
                case "text":
                case "hidden":
                case "password":
                case "textarea":
                case "file":
                case "checkbox":
                case "select-one":
                case "radio":
                    if (sType == "select-one") {
                        setting.type = "size"
                    }
                    if (setting.type == "size") {
                        if (len < setting.min || len > setting.max) {
                            setting.isValid = false
                        } else {
                            setting.isValid = true
                        }
                    } else {
                        stype = (typeof setting.min);
                        if (stype == "number") {
                            if (!isNaN(val)) {
                                nval = parseFloat(val);
                                if (nval >= setting.min && nval <= setting.max) {
                                    setting.isValid = true
                                } else {
                                    setting.isValid = false
                                }
                            } else setting.isValid = false
                        }
                        if (stype == "string") {
                            if (val >= setting.min && val <= setting.max) {
                                setting.isValid = true
                            } else {
                                setting.isValid = false
                            }
                        }
                    }
                    break;
                case "select-more":
                    break
            }
        },
        CompareValid: function (returnObj) {
            var id = returnObj.id;
            var setting = returnObj.setting;
            var srcjo = jQuery("#" + id);
            var desjo = jQuery("#" + setting.desID);
            setting.isValid = false;
            curvalue = srcjo.val();
            ls_data = desjo.val();
            if (setting.datatype == "number") {
                if (!isNaN(curvalue) && !isNaN(ls_data)) {
                    curvalue = parseFloat(curvalue);
                    ls_data = parseFloat(ls_data)
                } else {
                    return
                }
            }
            switch (setting.operateor) {
                case "=":
                    if (curvalue == ls_data) {
                        setting.isValid = true
                    }
                    break;
                case "!=":
                    if (curvalue != ls_data) {
                        setting.isValid = true
                    }
                    break;
                case ">":
                    if (curvalue > ls_data) {
                        setting.isValid = true
                    }
                    break;
                case ">=":
                    if (curvalue >= ls_data) {
                        setting.isValid = true
                    }
                    break;
                case "<":
                    if (curvalue < ls_data) {
                        setting.isValid = true
                    }
                    break;
                case "<=":
                    if (curvalue <= ls_data) {
                        setting.isValid = true
                    }
                    break;
                case "oneok":
                    if ($.formValidator.IsEmpty(id) || $.formValidator.IsEmpty(IsEmpty.desID)) {
                        setting.isValid = false
                    } else {
                        setting.isValid = true
                    }
            }
        }
    };
    jQuery.fn.formValidator = function (msgOptions) {
   
        var setting = {
            validatorGroup: "1",
            empty: false,
            submitonce: false,
            automodify: false,
            entermovetonext: true,
            onshow: "",
            onfocus: "请输入内容",
            onempty: "输入内容为空",
            onvalid: function () {
                return true
            },
            onfocusevent: function () { },
            onblurevent: function () { },
            tipid: this.get(0).id + "Tip",
            defaultvalue: null,
            validateType: "InitValidator"
        };
        msgOptions = msgOptions || {};
        jQuery.extend(setting, msgOptions);
        return this.each(function () {
            var triggerID = this.id;
            var tip = jQuery("#" + setting.tipid);
            $.formValidator.appendValid(triggerID, setting);
            if (!$.formValidator.getInitConfig(setting.validatorGroup).alertMessage) {
                $.formValidator.SetTipState(setting.tipid, "onShow", setting.onshow)
            }
            var srcTag = this.tagName;
            var defaultValue = setting.defaultvalue;
            if (srcTag == "INPUT" || srcTag == "TEXTAREA") {
                var stype = this.type;
                var joeach = jQuery(this);
                if (stype == "checkbox" || stype == "radio") {
                    joeach = jQuery("input[@name=" + this.name + "]");
                    if (defaultValue) {
                        checkobj = jQuery("input[@name=" + this.name + "][@value='" + defaultValue + "']");
                        if (checkobj.length == 1) checkobj.attr("checked", "true")
                    }
                } else {
                    if (defaultValue) joeach.val(setting.defaultvalue)
                }
                joeach.focus(function () {
                    var settings = joeach.get(0).settings;
                    if (!$.formValidator.getInitConfig(settings[0].validatorGroup).alertMessage) {
                        $.formValidator.SetTipState(settings[0].tipid, "onFocus", settings[0].onfocus)
                    }
                    if (stype == "password" || stype == "text" || stype == "textarea" || stype == "file") {
                        this.validoldvalue = jQuery(this).val()
                    }
                    settings[0].onfocusevent(joeach.get(0))
                });
                joeach.blur(function () {
                    var elem = joeach.get(0);
                    var thefirstsettings = elem.settings;
                    var settingslen = thefirstsettings.length;
                    var returnObj = $.formValidator.OneIsValid(triggerID, 1);
                    if (returnObj.ajax >= 0 && elem.validoldvalue != jQuery(elem).val()) {
                        $.formValidator.SetTipState(thefirstsettings[0].tipid, "onLoad", thefirstsettings[returnObj.ajax].onwait);
                        $.formValidator.AjaxValid(returnObj)
                    } else {
                        $.formValidator.ShowMessage(returnObj);
                        if (!returnObj.isValid) {
                            var auto = thefirstsettings[0].automodify && (elem.type == "text" || elem.type == "textarea" || elem.type == "file");
                            if (auto && !$.formValidator.getInitConfig(thefirstsettings[0].validatorGroup).alertMessage) {
                                alert(returnObj.setting.onerror);
                                $.formValidator.SetTipState(thefirstsettings[0].tipid, "onShow", setting.onshow)
                            }
                        }
                    }
                    thefirstsettings[0].onblurevent(joeach.get(0))
                })
            } else if (srcTag == "SELECT") {
                srcjo = jQuery(this);
                var settings = this.settings;
                if (defaultValue) {
                    jQuery.each(this.options,
					function () {
					    if (this.value == defaultValue) this.selected = true
					})
                }
                srcjo.focus(function () {
                    if (!$.formValidator.getInitConfig(setting.validatorGroup).alertMessage) {
                        $.formValidator.SetTipState(settings[0].tipid, "onFocus", settings[0].onfocus)
                    }
                });
                srcjo.bind("change",
				function () {
				    var returnObj = $.formValidator.OneIsValid(triggerID, 1);
				    if (returnObj.ajax >= 0 && this.validoldvalue != jQuery(this).val()) {
				        $.formValidator.AjaxValid(triggerID, returnObj.setting)
				    } else {
				        $.formValidator.ShowMessage(returnObj)
				    }
				})
            }
        })
    };
    jQuery.fn.InputValidator = function (controlOptions) {
        var settings = {
            isValid: false,
            min: 0,
            max: 99999999999999,
            forceValid: false,
            type: "size",
            defaultValue: null,
            onerror: "输入错误",
            validateType: "InputValidator"
        };
        controlOptions = controlOptions || {};
        jQuery.extend(settings, controlOptions);
        return this.each(function () {
            $.formValidator.appendValid(this.id, settings)
        })
    };
    jQuery.fn.SelectValidator = function (controlOptions) {
        var settings = {
            isValid: false,
            onerror: "必须选择",
            defaultValue: null,
            validateType: "SelectValidator"
        };
        controlOptions = controlOptions || {};
        jQuery.extend(settings, controlOptions);
        return this.each(function () {
            $.formValidator.appendValid(this.id, settings)
        })
    };
    jQuery.fn.CompareValidator = function (controlOptions) {
        var settings = {
            isValid: false,
            desID: "",
            operateor: "=",
            onerror: "输入错误",
            validateType: "CompareValidator"
        };
        controlOptions = controlOptions || {};
        jQuery.extend(settings, controlOptions);
        return this.each(function () {
            var li_index = $.formValidator.appendValid(this.id, settings)
        })
    };
    jQuery.fn.RegexValidator = function (controlOptions) {
        var settings = {
            isValid: false,
            regexp: "",
            param: "i",
            datatype: "string",
            onerror: "输入的格式不正确",
            validateType: "RegexValidator"
        };
        controlOptions = controlOptions || {};
        jQuery.extend(settings, controlOptions);
        return this.each(function () {
            $.formValidator.appendValid(this.id, settings)
        })
    };
    jQuery.fn.FunctionValidator = function (controlOptions) {
        var settings = {
            isValid: true,
            onerror: "你输入的数据不正确，请确认",
            fun: function () {
                this.isValid = true
            },
            validateType: "FunctionValidator"
        };
        controlOptions = controlOptions || {};
        jQuery.extend(settings, controlOptions);
        return this.each(function () {
            $.formValidator.appendValid(this.id, settings)
        })
    };
    jQuery.fn.AjaxValidator = function (controlOptions) {
        var settings = {
            isValid: false,
            lastValid: "",
            type: "GET",
            url: "",
            addidvalue: true,
            datatype: "html",
            data: "",
            async: true,
            beforesend: function () {
                return true
            },
            success: function () {
                return true
            },
            complete: function () { },
            processdata: false,
            error: function () { },
            buttons: null,
            onerror: "服务器校验没有通过",
            onwait: "正在等待服务器返回数据",
            validateType: "AjaxValidator"
        };
        controlOptions = controlOptions || {};
        jQuery.extend(settings, controlOptions);
        return this.each(function () {
            $.formValidator.appendValid(this.id, settings)
        })
    };
    jQuery.fn.DefaultPassed = function () {
        return this.each(function () {
            var settings = this.settings;
            for (var i = 1; i < settings.length; i++) {
                settings[i].isValid = true;
                $.formValidator.SetTipState(settings[0].tipid, "onSuccess", (settings[0].oncorrect ? settings[0].oncorrect : ''))
            }
        })
    }
})(jQuery);