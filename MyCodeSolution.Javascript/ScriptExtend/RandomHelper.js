document.write("<script language=javascript src='/ScriptExtend/ValidateHelper.js' ></script >");


var RandomHelper = {
    // 获取随机整数
    getInt: function (begin, end, defaultValue) {

        if (!ValidateHelper.isInteger(defaultValue))
            defaultValue = 0;
        if (!ValidateHelper.isInteger(begin))
            return defaultValue;
        if (!ValidateHelper.isInteger(end))
            return defaultValue;
        if (begin >= end)
            return defaultValue;

        return Math.floor(Math.random() * (end - begin + 1)) + begin;
    },
    // 获取随机浮点数，取不到最大值
    getFloat: function (begin, end, precision = 1, defaultValue = 0) {

        if (!ValidateHelper.isInteger(begin))
            return defaultValue;
        if (!ValidateHelper.isInteger(end))
            return defaultValue;
        if (begin >= end)
            return defaultValue;
        if (!ValidateHelper.isInteger(precision))
            return defaultValue;
        if (!ValidateHelper.isInteger(defaultValue))
            return null;

        return (Math.random() * (end - begin) + begin).toFixed(precision);
    },
    /**获取随机数数组
     * @param {any} type 随机数类型
     * @param {any} number 随机数数量
     * @param {any} min 最小值
     * @param {any} max 最大值
     */
    getRandomArray: function (type, number, begin, end, precision) {

        var result = [];

        //判断参数是否合法
        if (!ValidateHelper.isInteger(number))
            return result;
        if (!ValidateHelper.isInteger(begin))
            return result;
        if (!ValidateHelper.isInteger(end))
            return result;
        if (begin >= end)
            return result;

        //判断浮点类型，保留小数位是否合法
        if (type == "float") {
            if (!ValidateHelper.isInteger(precision))
                return result;
            else if (precision < 0)
                return result;
        }

        for (var i = 0; i < number; i++) {
            if (type == "float")
                result.push(this.getFloat(begin, end, precision));
            else
                result.push(this.getInt(begin, end));
        }
        return result;
    }
}







