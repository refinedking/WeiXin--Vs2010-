var roller = function () {
    return {
        init: function (el, ty, sx, ex, d, st) {
            e = document.getElementById(el);
            if (!e) {
                return;
            }
            a = e.getElementsByTagName("li");//标签
            for (i = 0; i < a.length; i++) {
                if (!a[i].id) {
                    a[i].id = el.id + i;
                }
                a[i].n = a[i].o = sx; //y坐标
                a[i].en = ex; //x坐标
                a[i].ty = ty; //动画方向
                if (a[i].ty == 'v') {//纵向动画
                    a[i].style.backgroundPosition = "0px " + a[i].n + "px";
                }
                else if (a[i].ty == 'h') {//横向动画
                    a[i].style.backgroundPosition = a[i].n + "px 0px";
                }
                else {
                    return;
                }
                a[i].onmouseover = roller.o;
                a[i].onmouseout = roller.o;
                a[i].st = Math.abs(Math.abs(ex - sx) / st); //
                a[i].t = d / st; //
            }
        },
        o: function (e) {
            e = e || window.event;
            c = e.target != null ? e.target : e.srcElement;
            //目标标签名要大写
            if (c.nodeName == "LI" && e.type == "mouseover") {
                c.w = c.en;
                roller.s(c);
            }
            else if (c.nodeName == "LI") {
                c.w = c.o;
                roller.s(c);
            }
        },
        s: function (e) {
            if (e.ti) {
                clearTimeout(e.ti);
            }
            if (Math.abs(e.n - e.w) < e.st) {
                e.n = e.w;
            }
            else if (e.n < e.w) {
                e.n = e.n + e.st;
            }
            else if (e.n > e.w) {
                e.n = e.n - e.st;
            }
            if (e.ty == 'v') {
                e.style.backgroundPosition = "0px " + e.n + "px";
            }
            else {
                e.style.backgroundPosition = e.n + "px 0px";
            }
            if (e.n == e.w) {
                clearTimeout(e.ti);
                return;
            }
            e.ti = setTimeout(function () { roller.s(e) }, e.t);
        }
    }
} ();