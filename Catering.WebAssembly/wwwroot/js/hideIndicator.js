function hideIndicator() {
    const span = document.getElementById("indicator");
    if (span.textContent === 0) {
        span.style.display = "none";
    }
    else {
        span.style.display = "block";
    }
}