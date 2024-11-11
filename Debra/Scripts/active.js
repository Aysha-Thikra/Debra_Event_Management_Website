document.querySelectorAll('.sidebar-menu a').forEach(link => {
    link.addEventListener('click', function() {
        // Remove the 'active' class from all links
        document.querySelectorAll('.sidebar-menu a').forEach(item => {
            item.classList.remove('active');
        });

        // Add the 'active' class to the clicked link
        this.classList.add('active');
    });
});
