document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".tarjeta-ps, .detalle-card, .dashboard-card, .stat-card")
        .forEach(function (element) {
            element.classList.add("hover-lift", "smooth-transition");
        });

    document.querySelectorAll(".imagen-juego, .detalle-imagen, .dashboard-card img")
        .forEach(function (element) {
            element.classList.add("image-zoom", "smooth-transition");
        });

    document.querySelectorAll(".btn-ps, .btn-volver, .btn-global-agregar, .btn-favorito-detalle")
        .forEach(function (element) {
            element.classList.add("glow-button", "smooth-transition");
        });

    document.querySelectorAll(".btn-favorito, .btn-favorito-detalle")
        .forEach(function (element) {
            element.classList.add("pulse-favorite", "smooth-transition");
        });

    document.querySelectorAll(".dashboard-card, .stat-card, .dashboard-section")
        .forEach(function (element) {
            element.classList.add("dashboard-hover", "smooth-transition");
        });
});
