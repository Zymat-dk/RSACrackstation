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

function toggleHexButton(decimalText, hexText) {
    if (decimalText.css("font-weight") == 400) {
        decimalText.css("font-weight", "bold");
        hexText.css("font-weight", "normal");
    } else {
        hexText.css("font-weight", "bold");
        decimalText.css("font-weight", "normal");
    }
}

function toggleHexContent(text, decimalText) {
    if (text !== "") {
        if (decimalText.css("font-weight") == 700) {
            return hexToDecimal(text);
        } else {
            return decimalToHex(text);
        }
    }
    return -1;
}