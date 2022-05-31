function messageBox(message) {
    window.alert(message);
}

function setColorInStorage(color) {
    if (typeof (Storage) !== "undefined") {
        localStorage.setItem("Color", color)
    }
}

function getColorFromStorage() {
    if (typeof (Storage) !== "undefined") {
        return localStorage.getItem("Color")
    }
}