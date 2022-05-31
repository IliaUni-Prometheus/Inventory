function messageBox(messsage) {
    window.alert(messsage);
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