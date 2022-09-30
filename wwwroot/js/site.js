// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Shows the snackbar for 5 seconds
function showSnackbar(message, action) {
    if (action === undefined) {
        action = 0;
    }
    $("#failText").text(message);
    $("#snackbar").addClass("show-bar");
    $("#snackbar").children().addClass("hidden");
    $("#snackbar").children().eq(action).removeClass("hidden");
    $("#failText").removeClass("hidden");
    setTimeout(function () {
        $("#snackbar").removeClass("show-bar");
    }, 5000);
}

let isHex = false;
let isHex2 = false;

function updateHexToggle() {
    $("#hexToggle").children().toggleClass("bg-transparent bg-indigo-500 text-white");
    isHex = !isHex;
}

function updateHexToggle2() {
    $("#hexToggle2").children().toggleClass("bg-transparent bg-indigo-500 text-white");
    isHex2 = !isHex2;
}

function asciiToHex(ascii) {
    let hexArray = [];
    for (let n = 0, l = ascii.length; n < l; n++) {
        let hex = Number(ascii.charCodeAt(n)).toString(16);
        hexArray.push(hex);
    }
    return hexArray.join('');
}

function hexToDecimal(hex) {
    if (hex.substring(0, 2) === "0x") {
        hex = hex.substring(2);
    }
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

function hexToAscii(hex) {
    hex  = hex.toString();
    let str = '';
    for (let n = 0; n < hex.length; n += 2) {
        str += String.fromCharCode(parseInt(hex.substr(n, 2), 16));
    }
    return str;
}

function asciiToDecimal(ascii) {
    return hexToDecimal(asciiToHex(ascii));
}

function decimalToAscii(dec) {
    return hexToAscii(decimalToHex(dec));
}

function toggleHexContent(text, toHex) {
    if (text !== "") {
        if (toHex) {
            return decimalToHex(text);
        } else {
            return hexToDecimal(text);
        }
    }
    return -1;
}