// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    const sidebar = document.getElementById('sidebar');
    const content = document.getElementById('main-content');
    const btn = document.getElementById('sidebarCollapse');

    btn.addEventListener('click', function () {
        
        sidebar.classList.toggle('collapsed');
        content.classList.toggle('expanded');
    });
});