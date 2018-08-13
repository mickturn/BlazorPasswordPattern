
document.onkeyup = function (evt) {
    evt = evt || window.event;
    DotNet.invokeMethodAsync('BlazorPasswordPatternComponent', 'KeyUpFromjs', evt.keyCode);


};