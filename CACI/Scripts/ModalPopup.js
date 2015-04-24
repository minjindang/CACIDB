var maps_Dialog = {
    dismap: 0,
    other: 0,
    modalPopupDialog: "",
    tWidth: 0, /* 可變區域大小 寬*/
    tHeight: 0, /* 可變區域大小 高*/
    showMaps: function (modalPopupDialogid) {
        this.showMaps(modalPopupDialogid, 0)
    },
    showMaps: function (modalPopupDialogid, power) {

        if (document.all("screen") && power != 1) {
            var screen = document.all("screen");

            if (screen.style.display != "none")
                return;
        }

        modalPopupDialog = modalPopupDialogid;

        this.screenDark();

        tWidth = 800;
        tHeight = 100;

        //document.all(modalPopupDialog).style.width = tWidth + "px";
        //document.all(modalPopupDialog).style.height = tHeight + "px";
        //		if( tHeight > document.body.offsetHeight )
        //		{
        //		    document.all(modalPopupDialog).style.top = document.body.scrollTop;
        //		}
        //		else
        {
            if (eval(document.body.clientHeight) > eval(document.all(modalPopupDialog).style.height.replace("px", "")))
                document.all(modalPopupDialog).style.top = document.body.scrollTop + (eval(document.body.clientHeight - document.all(modalPopupDialog).style.height.replace("px", "")) / 3);
            else
                document.all(modalPopupDialog).style.top = 10;
        }

        document.all(modalPopupDialog).style.left = ((document.body.clientWidth - eval(document.all(modalPopupDialog).style.width.replace("px", ""))) / 5);

        this.showmodalPopupDialog();

    }, screenDark: function () {
        if (document.all("screen")) {
            var screen = document.all("screen");
            screen.style.display = "block";
        }
        if (!screen) {
            var screen = document.createElement("<div></div>");
            //document.body.appendChild(screen);
            document.body.appendChild(screen);
        }
        screen.id = "screen";
        screen.style.cssText = "float:left; position:absolute; left:0px; top:0px; z-index:30; background-color:Gray; ";

        screen.style.width = document.body.scrollWidth + "px";

        if (window.top.document.body.scrollHeight - 120 > eval(document.all(modalPopupDialog).style.width.replace("px", "")))
            screen.style.height = (window.top.document.body.scrollHeight - 120) + "px"; // document.body.scrollHeight + "px";
        else
            screen.style.height = (eval(document.all(modalPopupDialog).style.height.replace("px", "")) + 20) + "px";
        screen.onclick = function () {
            maps_Dialog.closeMaps();
            return false;
        }
        this.setOpacity(0.8);
    }, setOpacity: function (opacity) {
        if (window.ActiveXObject) {
            if (document.all("screen"))
                document.all("screen").style.filter = "alpha(opacity=" + opacity * 100 + ")";
        }
        if (document.all("screen"))
            document.all("screen").style.opacity = opacity;
    }, showmodalPopupDialog: function () {
        document.all(modalPopupDialog).style.display = "block";
        //document.all("loadingbar").style.display = "";		
    }, closeMaps: function () {
        //document.all(modalPopupDialog).style.width = "814px";
        //document.all(modalPopupDialog).style.height = "auto";
        document.all(modalPopupDialog).style.display = "none";
        document.all("screen").style.display = "none";
        this.closeLoadingbar();
    }, closeLoadingbar: function () {
        //document.all("loadingbar").style.display = "none";
    }
}
