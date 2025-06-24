// Helper function to scroll to a specific element by ID
window.scrollToElementById = function(id) {
    try {
        const element = document.getElementById(id);
        if (element) {
            setTimeout(() => {
                element.scrollIntoView({ behavior: 'smooth', block: 'end' });
            }, 100);
        }
    } catch (error) {
        console.error("Error scrolling to element:", error);
    }
};

// Helper function to auto-resize text areas
window.autoResizeTextarea = function(element) {
    try {
        if (!(element instanceof HTMLElement)) return;
        
        element.style.height = 'auto';
        element.style.height = (element.scrollHeight) + 'px';
        
        element.addEventListener('input', function() {
            this.style.height = 'auto';
            this.style.height = (this.scrollHeight) + 'px';
        });
    } catch (error) {
        console.error("Error setting up textarea auto-resize:", error);
    }
};