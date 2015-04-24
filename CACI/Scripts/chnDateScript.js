if( typeof(Sys.Extended) != "undefined" )
{
    if( typeof(Sys.Extended.UI.CalendarBehavior) != "undefined" ){
        
        var k="ajax__calendar_hover",j="ajax__calendar_active",i="ajax__calendar_other",t="tbody",h="td",g="tr",s="auto",r="table",d="div",q="years",e="months",p="dateSelectionChanged",o="hidden",n="hiding",m="shown",l="showing",f="days",b=false,a=null,c=true;

        Sys.Extended.UI.CalendarBehavior.prototype._performLayout = function () {

            var a = this, w = a.get_element();
            if (!w) return;
            if (!a.get_isInitialized()) return;
            if (!a._isOpen) return;
            var v = Sys.CultureInfo.CurrentCulture.dateTimeFormat, x = a.get_selectedDate(), c = a._getEffectiveVisibleDate(), s = a.get_todaysDate();

            switch (a._mode) {
                case f:
                    var r = a._getFirstDayOfWeek(), n = c.getDay() - r;
                    if (n <= 0) n += 7;

                    for (var t = new Date(c.getFullYear(), c.getMonth(), c.getDate() - n), h = t, g = 0; g < 7; g++) {  //, a._hourOffsetForDst
                        var d = a._daysTableHeaderRow.cells[g].firstChild;
                        d.firstChild && d.removeChild(d.firstChild);
                        d.appendChild(document.createTextNode(v.ShortestDayNames[(g + r) % 7]))
                    }

                    for (var p = 0; p < 6; p++)
                        for (var u = a._daysBody.rows[p], o = 0; o < 7; o++) {
                            var d = u.cells[o].firstChild;
                            d.firstChild && d.removeChild(d.firstChild);
                            d.appendChild(document.createTextNode(h.getDate()));

                            if (this._textbox.get_element().getAttribute("DateType") == "Taiwan") {
                                d.title = "民國 " + (h.getFullYear() - 1911) + "年 " + (h.getMonth() + 1) + "月 " + h.getDate() + "日";
                            }
                            else {
                                d.title = h.localeFormat("D");
                            }

                            d.date = h;
                            $common.removeCssClasses(d.parentNode, [i, j]);
                            Sys.UI.DomElement.addCssClass(d.parentNode, a._getCssClass(d.date, "d"));
                            h = new Date(h.getFullYear(), h.getMonth(), h.getDate() + 1); //, a._hourOffsetForDst
                        }

                        a._prevArrow.date = new Date(c.getFullYear(), c.getMonth() , 1); //, a._hourOffsetForDst
                        a._nextArrow.date = new Date(c.getFullYear(), c.getMonth() + 2, 1); // , a._hourOffsetForDst
                    a._title.firstChild && a._title.removeChild(a._title.firstChild);

                    if (this._textbox.get_element().getAttribute("DateType") == "Taiwan") {
                        a._title.appendChild(document.createTextNode("民國 " + (c.getFullYear() - 1911) + "年" + c.localeFormat(" MM") + "月"));
                    }
                    else {
                        a._title.appendChild(document.createTextNode(c.localeFormat(a.get_daysModeTitleFormat())));
                    }
                    a._title.date = c;
                    break;

                case e:
                    for (var g = 0; g < a._monthsBody.rows.length; g++)
                        for (var m = a._monthsBody.rows[g], l = 0; l < m.cells.length; l++) {
                            var b = m.cells[l].firstChild;
                            b.date = new Date(c.getFullYear(), b.month + 1, 1); //, a._hourOffsetForDst

                            if (a._textbox.get_element().getAttribute("DateType") == "Taiwan") {
                                b.title = "民國 " + (b.date.getFullYear() - 1911) + "年 " + (b.date.getMonth() + 1) + "月";
                            }
                            else {
                                b.title = b.date.localeFormat("Y");
                            }
                            $common.removeCssClasses(b.parentNode, [i, j]);
                            Sys.UI.DomElement.addCssClass(b.parentNode, a._getCssClass(b.date, "M"))
                        }
                    a._title.firstChild && a._title.removeChild(a._title.firstChild);

                    if (a._textbox.get_element().getAttribute("DateType") == "Taiwan") {
                        a._title.appendChild(document.createTextNode("民國 " + (c.getFullYear() - 1911)));
                    }
                    else {
                        a._title.appendChild(document.createTextNode(c.localeFormat("yyyy")));
                    }
                    a._title.date = c;
                    a._prevArrow.date = new Date(c.getFullYear() , 0, 1); //, a._hourOffsetForDst
                    a._nextArrow.date = new Date(c.getFullYear() + 2, 0, 1); //, a._hourOffsetForDst
                    break;
                case q:
                    for (var k = Math.floor(c.getFullYear() / 10) * 10, g = 0; g < a._yearsBody.rows.length; g++)
                        for (var m = a._yearsBody.rows[g], l = 0; l < m.cells.length; l++) {
                            var b = m.cells[l].firstChild;
                            b.date = new Date(k + b.year, 1, 1); //, a._hourOffsetForDst
                            if (b.firstChild) b.removeChild(b.lastChild);
                            else b.appendChild(document.createElement("br"));

                            if (a._textbox.get_element().getAttribute("DateType") == "Taiwan") {
                                b.appendChild(document.createTextNode(k - 1911 + b.year));
                            }
                            else {
                                b.appendChild(document.createTextNode(k + b.year));
                            }

                            $common.removeCssClasses(b.parentNode, [i, j]);
                            Sys.UI.DomElement.addCssClass(b.parentNode, a._getCssClass(b.date, "y"))
                        }

                    a._title.firstChild && a._title.removeChild(a._title.firstChild);

                    if (a._textbox.get_element().getAttribute("DateType") == "Taiwan") {
                        a._title.appendChild(document.createTextNode((k - 1912).toString() + "-" + (k - 1910 + 9).toString()));
                    }
                    else {
                        a._title.appendChild(document.createTextNode(k.toString() + "-" + (k + 9).toString()));
                    }
                    a._title.date = c;
                    a._prevArrow.date = new Date(k , 0, 1); //, a._hourOffsetForDst
                    a._nextArrow.date = new Date(k + 20, 0, 1); // , a._hourOffsetForDst
            }

            a._today.firstChild && a._today.removeChild(a._today.firstChild);
            //
            //this._today.appendChild(document.createTextNode(String.format(Sys.Extended.UI.Resources.Calendar_Today, s.localeFormat("MMMM d") + ", 民國" + ( s.getYear() - 1911 ) )));
            if (a._textbox.get_element().getAttribute("DateType") == "Taiwan") {
                a._today.appendChild(document.createTextNode(String.format(Sys.Extended.UI.Resources.Calendar_Today, "民國 " + (s.getYear() - 1911) + " 年" + s.localeFormat(" MM") + "月" + s.localeFormat(" dd") + "日")));
            }
            else {
                a._today.appendChild(document.createTextNode(String.format(Sys.Extended.UI.Resources.Calendar_Today, s.localeFormat(a.get_todaysDateFormat()))));
            }

            a._today.date = s;

        }
        
        Sys.Extended.UI.CalendarBehavior.prototype._buildMonths = function() {
        /// <summary>
        /// Builds a "months of the year" view for the calendar
        /// </summary>

            var dtf = Sys.CultureInfo.CurrentCulture.dateTimeFormat;
            var id = this.get_id();

            this._months = $common.createElementFromTemplate({
                nodeName: "div",
                properties: { id: id + "_months" },
                cssClasses: ["ajax__calendar_months"],
                visible: false
            }, this._body);
            this._modes["months"] = this._months;

            this._monthsTable = $common.createElementFromTemplate({
                nodeName: "table",
                properties: {
                    id: id + "_monthsTable",
                    cellPadding: 0,
                    cellSpacing: 0,
                    border: 0,
                    style: { margin: "auto" }
                }
            }, this._months);

            this._monthsBody = $common.createElementFromTemplate({ nodeName: "tbody", properties: { id: id + "_monthsBody"} }, this._monthsTable);
            for (var i = 0; i < 3; i++) {
                var monthsRow = $common.createElementFromTemplate({ nodeName: "tr" }, this._monthsBody);
                for (var j = 0; j < 4; j++) {
                    var monthCell = $common.createElementFromTemplate({ nodeName: "td" }, monthsRow);
                    if(this._textbox.get_element().getAttribute("DateType") == "Taiwan")
                    {
                        var monthDiv = $common.createElementFromTemplate({
                            nodeName: "div",
                            properties: {
                                id: id + "_month_" + i + "_" + j,
                                mode: "month",
                                month: (i * 4) + j,
                                innerHTML: "<br />" + [(i * 4) + j + 1 ] + "月"
                                //innerHTML: "<br />" + dtf.AbbreviatedMonthNames[(i * 4) + j]
                                
                            },
                            events: this._cell$delegates,
                            cssClasses: ["ajax__calendar_month"]
                        }, monthCell);
                    }
                    else
                    {
                        var monthDiv = $common.createElementFromTemplate({
                            nodeName: "div",
                            properties: {
                                id: id + "_month_" + i + "_" + j,
                                mode: "month",
                                month: (i * 4) + j,
                                //innerHTML: "<br />" + [(i * 4) + j + 1 ] + "月"
                                innerHTML: "<br />" + dtf.AbbreviatedMonthNames[(i * 4) + j]
                                
                            },
                            events: this._cell$delegates,
                            cssClasses: ["ajax__calendar_month"]
                        }, monthCell);
                    }
                }
            }
        }
        
        Sys.Extended.UI.CalendarBehavior.prototype.set_selectedDate=function(d)
        {
            var a=this;
      	    if(d&&String.isInstanceOfType(d)&&d.length!=0)d=new Date(d);
      	    if(d)d=d.getDateOnly();
      	    if(a._selectedDate!=d)
            {
                a._selectedDate=d;
                a._selectedDateChanging=c;
                var f="";
                if(d)
                {
                    if(a._textbox.get_element().getAttribute("DateType") == "Taiwan")
                    {
                        f = ((d.getFullYear() - 1911) < 100 ? "0" : "") + (d.getFullYear() - 1911) + "/" + ((d.getMonth() + 1) < 10 ? "0" : "") + (d.getMonth() + 1) + "/" + (d.getDate() < 10 ? "0" : "") + d.getDate() ;
                    }
                    else
                    {
                        f = d.getFullYear() + "/" + ((d.getMonth() + 1) < 10 ? "0" : "") + (d.getMonth() + 1) + "/" + (d.getDate() < 10 ? "0" : "") + d.getDate() ;
                        //f=d.localeFormat(a._format);
                    }
                    
                    if(!a._clearTime)
                    {
                        var e=a._textbox.get_Value();
                        if(e)e=a._parseTextValue(e);
                        if(e)
                            if(d!=e.getDateOnly())
                            {
                                if(a._textbox.get_element().getAttribute("DateType") == "Taiwan")
                                {
                                    f = ((d.add(e.getTimeOfDay()).getFullYear() - 1911) < 100 ? "0" : "") + (d.add(e.getTimeOfDay()).getFullYear() - 1911) + "/" + ((d.add(e.getTimeOfDay()).getMonth() + 1) < 10 ? "0" : "") + (d.add(e.getTimeOfDay()).getMonth() + 1) + "/" + (d.add(e.getTimeOfDay()).getDate() < 10 ? "0" : "") + d.add(e.getTimeOfDay()).getDate() ;
                                }
                                else
                                {
                                    f = d.add(e.getTimeOfDay()).getFullYear() + "/" + ((d.add(e.getTimeOfDay()).getMonth() + 1) < 10 ? "0" : "") + (d.add(e.getTimeOfDay()).getMonth() + 1) + "/" + (d.add(e.getTimeOfDay()).getDate() < 10 ? "0" : "") + d.add(e.getTimeOfDay()).getDate() ;
                                    //f=d.add(e.getTimeOfDay()).localeFormat(a._format);
                                }
                            }
                    }
                }
                if(f!=a._textbox.get_Value())
                {a._textbox.set_Value(f);
                a._fireChanged()}
                a._selectedDateChanging=b;
                a.invalidate();
            a.raisePropertyChanged("selectedDate")}
        }
    }
}