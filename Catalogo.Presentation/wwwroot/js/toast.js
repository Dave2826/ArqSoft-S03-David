document.addEventListener("DOMContentLoaded", function () {
    const container = document.querySelector(".toast-container[data-toast-message]");

    if (!container) {
        return;
    }

    const message = container.dataset.toastMessage;
    const type = container.dataset.toastType || "success";

    const toast = document.createElement("div");
    toast.className = `toast-message toast-${type}`;
    toast.textContent = message;

    container.appendChild(toast);

    window.setTimeout(function () {
        toast.classList.add("toast-show");
    }, 40);

    window.setTimeout(function () {
        toast.classList.remove("toast-show");
    }, 3200);

    window.setTimeout(function () {
        toast.remove();
        container.remove();
    }, 3800);
});
