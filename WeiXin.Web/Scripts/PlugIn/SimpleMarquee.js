var SimpleMarquee = function (id,speed) {
	this.init.call(this,id,speed);
}
SimpleMarquee.prototype.stopFlag = false;
SimpleMarquee.prototype.lis='';
SimpleMarquee.prototype.init = function(id,speed){
	this.ul = document.getElementById(id);
	this.lis=this.ul.innerHTML;
	this.speed = speed || 80;
	if (!this.ul) return;
	this.createWrap();
	this.clonelist();
	this.mouseover();
	this.mouseout();
}
SimpleMarquee.prototype.createWrap=function(){
	var wrap = this.wrap = document.createElement('div'),
	ul     = this.ul,
	parent = ul.parentNode,
	width  = this.ul.offsetWidth, 
	height = this.moveDistance = this.ul.offsetHeight;
	wrap.style.cssText += 'width:' + width + 'px;height:' + height + 'px;overflow:hidden;'
	parent.replaceChild(wrap,ul);
	wrap.appendChild(ul);
}
SimpleMarquee.prototype.clonelist=function(){
	var newUl = this.newUl = this.ul.cloneNode(true);
	newUl.removeAttribute('id');
	this.wrap.appendChild(newUl);
}
SimpleMarquee.prototype.startMove=function(){
	var sm   = this,
	pos      = 0,
	moveMode = sm.ul;
	var x=1;
	this.animate = setInterval(function () {
		if (sm.stopFlag) return;
		var old;
		moveMode.style.marginTop = pos + 'px';
		pos -- ;
		if (pos == - sm.moveDistance) {
			old = moveMode;
			moveMode = moveMode.nextSibling;
			moveMode.style.marginTop = old.style.marginTop = 0;
			sm.wrap.appendChild(old);
			old.innerHTML=sm.lis;
			setTimeout(function () {
				sm.moveDistance = moveMode.offsetHeight;
			},0);
			pos = 0;
		}
	},this.speed);
}
SimpleMarquee.prototype.stop = function () {
	clearInterval(this.animate);	   
}
SimpleMarquee.prototype.mouseover=function(){
	var This = this;
	this.wrap.onmouseover = function () {
		This.stopFlag = true;
	}			
}
SimpleMarquee.prototype.mouseout=function(){
	var This = this;
	this.wrap.onmouseout = function () {
		This.stopFlag = false;
	}	
}