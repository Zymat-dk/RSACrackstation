// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function asciiToHex(ascii) {
    let hexArray = [];
    for (let n = 0, l = ascii.length; n < l; n++) {
        let hex = Number(ascii.charCodeAt(n)).toString(16);
        hexArray.push(hex);
    }
    return hexArray.join('');
}

function hexToDecimal(hex) {
    if (hex.length % 2) {
        hex = '0' + hex;
    }
    try {
        let bn = BigInt('0x' + hex);
        return bn.toString(10);
    } catch (SyntaxError) {
        return -1;
    }
}

function asciiToDecimal(ascii) {
    return hexToDecimal(asciiToHex(ascii));
}

function decimalToHex(dec) {
    try {
        let bn = BigInt(dec);
        return bn.toString(16);
    } catch (SyntaxError) {
        return -1;
    }
}
