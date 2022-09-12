// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

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

function decimalToHex(dec) {
    try {
        let bn = BigInt(dec);
        return bn.toString(16);
    } catch (SyntaxError) {
        return -1;
    }
}