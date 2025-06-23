window.autoResizeTextarea = function(element) {
    if (!(element instanceof HTMLElement)) return;
    
    element.addEventListener('input', function() {
        this.style.height = 'auto';
        this.style.height = (this.scrollHeight) + 'px';
    });
    
    // Set initial height
    element.style.height = 'auto';
    element.style.height = (element.scrollHeight) + 'px';
};

window.scrollToElementById = function(id) {
    const element = document.getElementById(id);
    if (element) {
        setTimeout(() => {
            element.scrollIntoView({ behavior: 'smooth', block: 'end' });
        }, 100);
    }
};