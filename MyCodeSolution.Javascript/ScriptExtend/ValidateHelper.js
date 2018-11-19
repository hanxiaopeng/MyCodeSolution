var ValidateHelper = {
    // 是否整数
    isInteger: function (obj) {
        return typeof obj === 'number' && obj % 1 === 0
    }
}