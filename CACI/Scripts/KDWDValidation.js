// 動態正規式檢查 ==============================================================
function _RegexTest(val,pattern)
{
    var regex = new RegExp(pattern);
    return regex.test(val);
}

// 動態數字檢查 ==============================================================
function _chkNum(val,IntCount,DecCount)
{
    var patternString = "";
    if( DecCount == 0 )
        patternString = "(^[-+]?\\d{1," + IntCount + "})$";    // /^([+-])?\d+$/;
    else
        patternString = "(^[-+]?\\d{1," + IntCount + "}(\\.\\d{1," + DecCount + "})?)$";

    return _RegexTest(val,patternString);
}

// 數字檢查 ============================================================== 
function _TxtNumber(val) 
{
    var regex = /(^([+-]?[1-9])?\d*(\.\d*)?)$/; // 非 0.1234 檢查
    var regex2 = /(^([-]?[0][\.]\d*))$/;        // 0.1234 檢查
    
    var valid = regex.test(val);
    if(!valid)
        regex2.test(val);
        
    return valid;
}

// 身分證字號檢核 ==============================================================
function _TxtIDNO(IDString) {
/*
   val = val.toUpperCase();
   var regex = /^[A-Z][1-2][0-9]{8}$/;
   var valid = regex.test(val);
                             
   // 檢核規則
   var tab = "ABCDEFGHJKLMNPQRSTUVWXYZIO";
   var A1 = new Array (1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,3,3,3,3,3,3 );
   var A2 = new Array (0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5 );
   var Mx = new Array (9,8,7,6,5,4,3,2,1,1);

   var index = tab.indexOf(val.charAt(0));     
   var sum = A1[i] + A2[i] * 9;
   for (index = 1; index < 10; index++) {
       var v = parseInt(val.charAt(index), 10);
      sum += (v * Mx[index]);
   }

   alert(sum);
   
   return (valid && (sum % 10 == 0)) || val == "" ;
*/
    var ErrString = "";
    var ID1 = IDString.toUpperCase();
    if (IDString.length != 0) { IDString = IDString.toUpperCase() }
    if (IDString.length != 10) { ErrString = ErrString + "身分證字號字數不對。" + unescape('%0D') }
    if (ID1.length != 10) return false; //alert("身分證字號字數不對 !");
    var IDdigit = new Array(10);
    for (var i = 0; i < 10; i++) { IDdigit[i] = ID1.charAt(i); }
    var CharEng = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    IDdigit[0] = CharEng.indexOf(IDdigit[0]);
    if (IDdigit[0] == -1) return false; //alert("身分證字號第一位為錯誤英文字母 !");
    if (IDdigit[1] != 1 && IDdigit[1] != 2) return false; //alert("身分證字號無法辨識性別 !");

    var Array1 = new Array(26);
    Array1[0] = 1; Array1[1] = 10; Array1[2] = 19;
    Array1[3] = 28; Array1[4] = 37; Array1[5] = 46;
    Array1[6] = 55; Array1[7] = 64; Array1[8] = 39;
    Array1[9] = 73; Array1[10] = 82; Array1[11] = 2;
    Array1[12] = 11; Array1[13] = 20; Array1[14] = 48;
    Array1[15] = 29; Array1[16] = 38; Array1[17] = 47;
    Array1[18] = 56; Array1[19] = 65; Array1[20] = 74;
    Array1[21] = 83; Array1[22] = 21; Array1[23] = 3;
    Array1[24] = 12; Array1[25] = 30;
    var result = Array1[IDdigit[0]];
    for (var i = 1; i < 10; i++) {
        var Number = "0123456789";
        IDdigit[i] = Number.indexOf(IDdigit[i]);
        if (IDdigit[i] == -1) {
            //alert("身分證字號錯誤 !");
            return false;
        } else {
            result += IDdigit[i] * (9 - i);
        }
    }
    result += 1 * IDdigit[9];
    //alert("result=="+result);
    if (result % 10 != 0) {
        //alert("身分證字號錯誤 !");
        return false;
    }
    else {
        return true;
    }
}

// 統一編號檢核 ==============================================================
function _TxtCompNO(val) {
    var regex = /^[0-9]{8}$/;
    
    if( regex.test(val))
    {
        // 檢核規則
        var sum = 0, tmp = 0;

        for (var index = 0; index < 8; index++) 
        {
            var banNum = parseInt(val.substring(index, index + 1), 10);
            switch (index) 
            {
                case 0: case 2: case 4: case 7: tmp = banNum;     break;
                case 1: case 3: case 5:         tmp = banNum * 2; break;           
                case 6:                         tmp = banNum * 4; break;
            }           
            sum += Math.floor(tmp / 10) + (tmp % 10); // 個位十位相加            
        }
        
        return ((sum % 10 == 0) || ( (sum % 10) == 9 && val.substring(7, 8) == '7')); // checksum可被10整除, 或BAN第7位為7且checksum除10為9                 
    }
    else
        return false;
}

// 公司統編及身分證字號檢查 ================================================================================
function _TxtCoAndPIdNo(val) {

    if (!_TxtCompNO(val)) {     // 公司統編
        if (!_TxtIDNO(val)) {   // 身分證字號
            if (val.length > 10) // 立案編號
                return true;
            else
                return false;
        }
        else
            return true;
    }
    else
        return true;
}

// 整數檢查 ==============================================================
function _TxtIntNumber(val)
{
    var regex = /^([+-])?\d+$/;
	return regex.test(val);
}

// E-Mail ===============================================================
function _TxtEMail(val)
{
    var regex = /^([0-9a-z]+[-._+&])*[0-9a-z]+@([0-9a-z]+[.])+[a-z]{2,3}$/;
	return regex.test(val);
}

// 民國日期檢察 ex:098/12/12 ==============================================================
function _checkChnDate(date) {

   var regex = /(^\d{3}\/\d{2}\/\d{2})$/
   if( !regex.test(date) )
     return false;
    
   var y = parseInt(date.substr(0, 3), 10) + 1911;
   var m = parseInt(date.substr(4, 2), 10);
   var d = parseInt(date.substr(7, 2), 10);      
   
   if(y < 1 || y > 9999) return false;
   if(m < 1 || m > 12  ) return false;
   
   var maxd = getMaxDay(y, m);           
   if(d < 1 || d > maxd) return false;
   
   return true;
}

// 西元日期檢察 ex: 2009/12/30 ==============================================================
function _checkEnDate(date) {

   var regex = /(^\d{4}\/\d{1,2}\/\d{1,2})$/
   if( !regex.test(date) )
     return false;

   var y = parseInt(date.substr(0, 4), 10);
   var m = parseInt(date.substr(5, 2), 10);
   var d = parseInt(date.substr(8, 2), 10);      
   
   if(y < 1 || y > 9999) return false;
   if(m < 1 || m > 12  ) return false;
   
   var maxd = getMaxDay(y, m);           
   if(d < 1 || d > maxd) return false;
   
   return true;
}

// 判斷是否為空值 ==============================================================
function _IsNotEmpty(val)
{
    return (val != "");
}

// 文字長度檢查 ==============================================================

function _chkStrLen(val,len)
{
    if(val.Bytelength() <= len )
        return true;
    else
        return false;
}

// 取得日選項 ================================================================

function getNewDayItem(sender)
{
    var year;
    var month;
    var ddl_day;
    var ddl_val;
    var datetype;

    datetype = $("#" + sender.id.split("__Ψ")[0]).attr("showDateType");

    
    if( sender.id.indexOf("_ΨDDLDateYear") > -1 )
    {
        year = $("#" + sender.id).val();
        month = $("#" + sender.id.replace("_ΨDDLDateYear","_ΨDDLDateMonth")).val();
        ddl_day = $("#" + sender.id.replace("_ΨDDLDateYear","_ΨDDLDateDay"));
    }
    else if( sender.id.indexOf("_ΨDDLDateMonth") > -1 )
    {
        month = $("#" + sender.id).val();
        year = $("#" + sender.id.replace("_ΨDDLDateMonth","_ΨDDLDateYear")).val();
        ddl_day = $("#" + sender.id.replace("_ΨDDLDateMonth","_ΨDDLDateDay"));
    }

    ddl_val = ddl_day.val();
    
    ddl_day.find('option').remove();

    if(datetype == "Taiwan")
    {
        year = eval(year) + 1911;
    }

    for (var index = 0; index < getMaxDay(eval(year), eval(month)); index++)
    {
        var value = padLeft((index + 1).toString(), 2);
        
        ddl_day.append(  
            $("<option></option>").  
                attr("value", value).  
                text(value));
    }
    
    ddl_day.val(ddl_val);
}

//  字串左邊補零  
function padLeft(str,lenght)
{
    if(str.length >= lenght)  
        return str;  
    else 
        return padLeft("0" +str,lenght);  
}

//  字串右邊補零  
function padRight(str,lenght)
{
    if(str.length >= lenght)  
        return str;  
    else 
        return padRight(str+"0",lenght);  
} 

function _DateIsNotEmpty(sourceid,ddl_Cnt)
{
    var isEmpty = true;
    
    if( !_ddl_IsNotEmpty(sourceid + "__ΨDDLDateYear"))
        isEmpty = false;
    
    if( eval(ddl_Cnt) > 1 && !_ddl_IsNotEmpty(sourceid + "__ΨDDLDateMonth"))
    {
        isEmpty = false;
    }
    
    if( eval(ddl_Cnt) > 2 && !_ddl_IsNotEmpty(sourceid + "__ΨDDLDateDay"))
    {
        isEmpty = false;
    }
    
    return isEmpty;
}

function _ddl_IsNotEmpty(sourceid)
{
    return $("select#" + sourceid ).val() != "" ;
}
// 取得最大日 ==============================================================
function getMaxDay(enyear, enmonth)
{ 
    if(enmonth == 4 || enmonth == 6 || enmonth == 9 || enmonth == 11) return 30; 
    if(enmonth == 2) 
       if(enyear % 4 == 0 && enyear % 100 != 0 || enyear % 400 == 0) 
          return 29; 
       else 
          return 28; 
    return 31; 
}

function objectValidation(source,validatorFunction )
{
    objectValidation(source,validatorFunction,null )
}

function objectValidation(sourceid,validatorFunction,parameter )
{
    if( validatorFunction != "_DateIsNotEmpty" && validatorFunction != "_ddl_IsNotEmpty")
    {
        if($("#" + sourceid).val() == "" && validatorFunction != "_IsNotEmpty")
            return true;
        
        if( parameter == null )
        {
            if( validatorFunction != "_IsNotEmpty" )
            {
                if( !eval(validatorFunction + "($(\"#" + sourceid + "\").val());"))
                {
                    $("#lbl_ErrMk_" + sourceid ).css('visibility','visible');
                    //$("#lbl_ErrMk_" + sourceid ).css('display','inline');
                    return false;
                }
                else
                {
                    $("#lbl_ErrMk_" + sourceid ).css('visibility','hidden');
                    //$("#lbl_ErrMk_" + sourceid ).css('display','none');
                    return true;
                }
            }
            else
            {
                if( !eval(validatorFunction + "($(\"#" + sourceid + "\").val());"))
                {
                    $("#lbl_ErrMk_" + sourceid ).css('visibility','visible');
                    //$("#lbl_ErrMk_" + sourceid ).css('display','inline');
                    return false;
                }
                else
                {
                    if( $("#lbl_ErrMk_" + sourceid ).css('visibility') != "visible" )
                        $("#lbl_ErrMk_" + sourceid ).css('visibility','hidden');
                    //if( $("#lbl_ErrMk_" + sourceid ).css('display') != 'inline')    
                        //$("#lbl_ErrMk_" + sourceid ).css('display','none');
                    return true;
                }
            }
        }
        else
        {
            if( !eval(validatorFunction + "($(\"#" + sourceid + "\").val(),\"" + parameter.join("\",\"") + "\");"))
            {
                $("#lbl_ErrMk_" + sourceid ).css('visibility','visible');
                //$("#lbl_ErrMk_" + sourceid ).css('display','inline');
                return false;
            }
            else
            {
                $("#lbl_ErrMk_" + sourceid ).css('visibility','hidden');
                //$("#lbl_ErrMk_" + sourceid ).css('display','none');
                return true;
            }
         }
     }
     else if( validatorFunction == "_ddl_IsNotEmpty" )
     {
        if( !eval(validatorFunction + "(\"" +sourceid + "\");"))
        {
            $("#lbl_ErrMk_" + sourceid ).css('visibility','visible');
            //$("#lbl_ErrMk_" + sourceid ).css('display','inline');
                return false;
        }
        else
        {
            $("#lbl_ErrMk_" + sourceid ).css('visibility','hidden');
            //$("#lbl_ErrMk_" + sourceid ).css('display','none');
            return true;
        }
     }
     else
     {
        if( !eval(validatorFunction + "(\"" +sourceid + "\",\"" + parameter + "\");" ))
        {
            $("#lbl_ErrMk_" + sourceid ).css('visibility','visible');
            //$("#lbl_ErrMk_" + sourceid ).css('display','inline');
                return false;
        }
        else
        {
            $("#lbl_ErrMk_" + sourceid ).css('visibility','hidden');
            //$("#lbl_ErrMk_" + sourceid ).css('display','none');
            return true;
        }
     }
}

String.prototype.Bytelength = function() 
{   
     var arr = this.match(/[^\x00-\xff]/ig);   

     return  arr == null ? this.length : this.length + arr.length;   
}  

//String.prototype.padLeft = function(num,chr) 
//{
//    if (this.length >= num)
//    {
//     return this;
//    }
//    
//    return (chr + this).pedLedt(num,chr);
//}


var powerButton = false;

/* 整頁檢查機制 */

var kdWdPageValidator = new Array();

function addKdValidator(source,validatorStr,parameter,msg,ValidationGroup)
{
    kdWdPageValidator[kdWdPageValidator.length] = [source, validatorStr, parameter, msg, ValidationGroup];
}

//function page_Validation_Result()
//{
//	var errMsgArr = "";

//	for (var i = 0; i < kdWdPageValidator.length; i++)
//	{
//	    if (!objectValidation(kdWdPageValidator[i][0], kdWdPageValidator[i][1], kdWdPageValidator[i][2]))
//        {
//            errMsgArr += kdWdPageValidator[i][3] + "\r\n";
//        }
//	}
//	
//	if( errMsgArr.length > 0 )
//	{
//	    alert( errMsgArr);
//	    return false;
//	}
//	else
    //	    return true;

//    alert("XXX");

//    return page_Validation_Result("");

//}

function page_Validation_Result(val) {
    var errMsgArr = "";
    for (var i = 0; i < kdWdPageValidator.length; i++) {
        if (kdWdPageValidator[i][4] == val && !objectValidation(kdWdPageValidator[i][0], kdWdPageValidator[i][1], kdWdPageValidator[i][2])) {
            errMsgArr += kdWdPageValidator[i][3] + "\r\n";
        }
    }

    if (errMsgArr.length > 0) {
        alert(errMsgArr);
        return false;
    }
    else
        return true;
}

Date.prototype.dateDiff = function(interval, objDate) {
    var dtEnd = new Date(objDate);
    if (isNaN(dtEnd)) return undefined;
    
    var diffDate = YearsRelation((dtEnd.getFullYear() - this.getFullYear()) + "/" + (dtEnd.getMonth() - this.getMonth()) + "/" + (dtEnd.getDate() - this.getDate())).split("/") ;
    
    switch (interval) {
        case "d": return diffDate[2];
        case "m": return diffDate[1];
        case "y": return diffDate[0];
    }
}

function YearsRelation(yearStr)
{
    var dateStr = yearStr.split("/");

    while (eval(dateStr[2]) < 0) {
        dateStr[2] = eval(dateStr[2]) + 30;
        dateStr[1] = eval(dateStr[1]) - 1;
    }

    while (eval(dateStr[1]) < 0) {

        dateStr[1] = eval(dateStr[1]) + 12;
        dateStr[0] = eval(dateStr[0]) - 1;
    }
    
    return dateStr[0].toString() + "/" + dateStr[1].toString() + "/" + dateStr[2].toString();
}

