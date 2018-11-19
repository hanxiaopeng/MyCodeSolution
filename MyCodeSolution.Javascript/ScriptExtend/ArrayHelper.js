var ArrayHelper = {
    // 获取最大值
    getMaxValue: function (arr) {
        var maxNum = -Infinity;
        for (var i = 0; i < arr.length; i++) {
            arr[i] > maxNum ? maxNum = arr[i] : null;
        };
        return maxNum;
    },
    // 获取最小值
    getMinValue: function (arr) {
        var minNum = Infinity;
        for (var i = 0; i < arr.length; i++) {
            arr[i] < minNum ? minNum = arr[i] : null;
        };
        return minNum;
    },
    // 获取平均值
    getAvgValue: function (arr) {
        var sum = 0;
        for (var i = 0; i < arr.length; i++) {
            sum += arr[i];
        };
        return (sum / arr.length * 100) / 100;
    },
    // 获取总和
    getSumValue: function (arr) {
        var sum = 0;
        for (var i = 0; i < arr.length; i++) {
            sum += arr[i];
        };
        return sum;
    },
    // 获取各位置元素占比
    getDataPercent: function (arr) {
        var result = [];
        var sum = parseFloat(getDemoDataArraySumNum(arr));

        for (var i = 0; i < arr.length; i++) {
            result.push((arr[i] / sum * 100).toFixed(1));
        };
        return result;
    },
    // 获取月份数据数组
    getDateMonthArray: function (number, startYear, startMonth) {
        var result = [];

        if (number == undefined || number == 0)
            return result;

        for (var i = 0; i < number; i++) {
            var month = (startMonth + i) % 12 == 0 ? 12 : (startMonth + i) % 12;
            var year = startYear + parseInt((startMonth + i) / 12) - ((startMonth + i) % 12 == 0 ? 1 : 0);

            result.push(year + '-' + (month < 10 ? ("0" + month) : ("" + month)));
        }

        return result;
    }

}