var key = CryptoJS.enc.Utf8.parse('D*G-KaPdSgVkYp3s');  //需要和伺服器端一致，否則… 無法解密
var iv = CryptoJS.enc.Utf8.parse('D(G+KaPdSgVkYp3s');   //需要和伺服器端一致，否則… 無法解密
var aesEncryDecry = {
    decryptStringAES: function (strEncryptText) {
        var decrypted = CryptoJS.AES.decrypt(strEncryptText, key, {
            keySize: 128 / 8,
            iv: iv,
            mode: CryptoJS.mode.CBC,
            padding: CryptoJS.pad.Pkcs7
        });
        return decrypted.toString(CryptoJS.enc.Utf8);
    },
    encryptStringAES: function (strOrignText) {
        var encrypted = CryptoJS.AES.encrypt(strOrignText, key, {
            keySize: 128 / 8,
            iv: iv,
            mode: CryptoJS.mode.CBC,
            padding: CryptoJS.pad.Pkcs7
        });
        return encrypted.toString();
    }
};