function deleteItem(buttonId) {
    var button = document.getElementById(buttonId);
    var row = button.parentNode.parentNode;
    row.parentNode.removeChild(row);
}