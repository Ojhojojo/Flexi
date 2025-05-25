function playSound(soundFile) {
    var audio = new Audio(soundFile);
    audio.play();
}


window.scrollToBottom = (element) => {
    element.scrollTop = element.scrollHeight;
};