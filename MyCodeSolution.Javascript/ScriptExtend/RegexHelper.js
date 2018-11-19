var validateRegExp = {
    // 浮点数
    decmal: "^([+-]?)\\d*\\.\\d+$",
    // 正浮点数
    decmal1: "^[1-9]\\d*.\\d*|0.\\d*[1-9]\\d*$",
    // 负浮点数
    decmal2: "^-([1-9]\\d*.\\d*|0.\\d*[1-9]\\d*)$",
    // 浮点数
    decmal3: "^-?([1-9]\\d*.\\d*|0.\\d*[1-9]\\d*|0?.0+|0)$",
    // 非负浮点数（正浮点数 + 0）
    decmal4: "^[1-9]\\d*.\\d*|0.\\d*[1-9]\\d*|0?.0+|0$",
    // 非正浮点数（负浮点数 +0）
    decmal5: "^(-([1-9]\\d*.\\d*|0.\\d*[1-9]\\d*))|0?.0+|0$",
    // 整数
    intege: "^-?[1-9]\\d*$",
    // 正整数
    intege1: "^[1-9]\\d*$",
    // 负整数
    intege2: "^-[1-9]\\d*$",
    // 数字
    num: "^([+-]?)\\d*\\.?\\d+$",
    // 正数（正整数 + 0）
    num1: "^[1-9]\\d*|0$",
    // 负数（负整数 + 0）
    num2: "^-[1-9]\\d*|0$",
    // 仅ACSII字符
    ascii: "^[\\x00-\\xFF]+$",
    // 仅中文
    chinese: "^[\\u4e00-\\u9fa5]+$",
    // 颜色
    color: "^[a-fA-F0-9]{6}$",
    // 日期
    date: "^\\d{4}(\\-|\\/|\.)\\d{1,2}\\1\\d{1,2}$",
    // 邮件
    email: "^\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+$",
    // 身份证
    idcard: "^[1-9]([0-9]{14}|[0-9]{17})$",
    // ip地址
    ip4: "^(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)$",
    // 字母
    letter: "^[A-Za-z]+$",
    // 小写字母
    letter_l: "^[a-z]+$",
    // 大写字母
    letter_u: "^[A-Z]+$",
    // 手机
    mobile: "^0?(13|15|18|14|17)[0-9]{9}$",
    // 非空
    notempty: "^\\S+$",
    // 密码
    password: "^.*[A-Za-z0-9\\w_-]+.*$",
    // 数字
    fullNumber: "^[0-9]+$",
    // 图片
    picture: "(.*)\\.(jpg|bmp|gif|ico|pcx|jpeg|tif|png|raw|tga)$",
    // QQ号码
    qq: "^[1-9]*[1-9][0-9]*$",
    // 压缩文件
    rar: "(.*)\\.(rar|zip|7zip|tgz)$",
    // 电话号码的函数(包括验证国内区号,国际区号,分机号)
    tel: "^[0-9\-()（）]{7,18}$",
    // url
    url: "^http[s]?:\\/\\/([\\w-]+\\.)+[\\w-]+([\\w-./?%&=]*)?$"
};
// 验证规则
var RegexHelper = {

    // 是否浮点数
    isFloat: function (str) {
        return new RegExp(validateRegExp.decmal).test(str);
    },
    // 是否浮点数
    isFloat1: function (str) {
        return new RegExp(validateRegExp.decmal3).test(str);
    },
    // 是否正浮点数
    isPositiveFloat: function (str) {
        return new RegExp(validateRegExp.decmal1).test(str);
    },
    // 是否负浮点数
    isNegtiveFloat: function (str) {
        return new RegExp(validateRegExp.decmal2).test(str);
    },
    // 是否非负浮点数（正浮点数 + 0）
    isPositiveFloatAnd0: function (str) {
        return new RegExp(validateRegExp.decmal4).test(str);
    },
    // 是否非正浮点数（负浮点数 +0）
    isNegtiveFloatAnd0: function (str) {
        return new RegExp(validateRegExp.decmal5).test(str);
    },
    // 是否整数
    isInteger: function (str) {
        return new RegExp(validateRegExp.intege).test(str);
    },
    // 是否正整数
    isPositiveInteger: function (str) {
        return new RegExp(validateRegExp.intege1).test(str);
    },
    // 是否负整数
    isNegtiveInteger: function (str) {
        return new RegExp(validateRegExp.intege2).test(str);
    },
    // 是否数字
    isNumber: function (str) {
        return new RegExp(validateRegExp.num).test(str);
    },
    // 是否数字
    isFullNumber: function (str) {
        return new RegExp(validateRegExp.fullNumber).test(str);
    },
    // 是否正数（正整数 + 0）
    isPositiveNumber: function (str) {
        return new RegExp(validateRegExp.num1).test(str);
    },
    // 是否负数（负整数 + 0）
    isNegtiveNumber: function (str) {
        return new RegExp(validateRegExp.num2).test(str);
    },
    // 是否ASCII
    isASCII: function (str) {
        return new RegExp(validateRegExp.ascii).test(str);
    },
    // 是否中文
    isChinese: function (str) {
        return new RegExp(validateRegExp.chinese).test(str);
    },
    // 是否颜色
    isColor: function (str) {
        return new RegExp(validateRegExp.color).test(str);
    },
    // 是否日期
    isDate: function (str) {
        return new RegExp(validateRegExp.date).test(str);
    },
    // 是否Email
    isEmail: function (str) {
        return new RegExp(validateRegExp.email).test(str);
    },
    // 是否身份证号
    isTel: function (str) {
        return new RegExp(validateRegExp.idcard).test(str);
    },
    // 是否IP地址
    isIp4: function (str) {
        return new RegExp(validateRegExp.ip4).test(str);
    },
    // 是否字母
    isLetter: function (str) {
        return new RegExp(validateRegExp.letter).test(str);
    },
    // 是否小写字母
    isLetterLow: function (str) {
        return new RegExp(validateRegExp.letter_l).test(str);
    },
    // 是否大写字母
    isLetterUp: function (str) {
        return new RegExp(validateRegExp.letter_u).test(str);
    },
    // 是否手机号
    isMobile: function (str) {
        return new RegExp(validateRegExp.mobile).test(str);
    },
    // 是否非空
    isNotempty: function (str) {
        return new RegExp(validateRegExp.notempty).test(str);
    },
    // 是否密码
    isPassword: function (str) {
        return new RegExp(validateRegExp.password).test(str);
    },
    // 是否图片
    isPicture: function (str) {
        return new RegExp(validateRegExp.picture).test(str);
    },
    // 是否QQ
    isQQ: function (str) {
        return new RegExp(validateRegExp.qq).test(str);
    },
    // 是否压缩文件
    isRar: function (str) {
        return new RegExp(validateRegExp.rar).test(str);
    },
    // 是否固定电话
    isTel: function (str) {
        return new RegExp(validateRegExp.tel).test(str);
    },
    // 是否URL
    isUrl: function (str) {
        return new RegExp(validateRegExp.url).test(str);
    }
};


