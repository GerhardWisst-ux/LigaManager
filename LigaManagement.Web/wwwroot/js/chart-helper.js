window.renderChart = () => {
    const canvas = document.getElementById("myChart");
    if (!canvas) return;
    const ctx = canvas.getContext("2d");

    const xValues = ["Siege", "Unentschieden", "Niederlagen"];
    const yValues = [10, 5, 2]; // Beispielwerte

    new Chart(ctx, {
        type: "bar",
        data: {
            labels: xValues,
            datasets: [{
                label: "Ergebnisse",
                backgroundColor: ["green", "yellow", "red"],
                data: yValues
            }]
        },
        options: {
            indexAxis: 'y',
            scales: {
                x: { beginAtZero: true },
                y: {}
            },
            plugins: {
                title: {
                    display: true,
                    text: "Siege - Unentschieden - Niederlagen"
                },
                legend: {
                    display: true
                }
            }
        }
    });
};

window.renderMap = (latitude, longitude) => {
    if (!window.L) return;

    const mapContainer = document.getElementById("mapid");
    if (!mapContainer) return;

    const map = new L.Map('mapid');
    const osm = new L.TileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        minZoom: 4,
        maxZoom: 20,
        attribution: '<a href="https://openstreetmap.org">OpenStreetMap</a>'
    });
    map.setView([latitude, longitude], 15);
    map.addLayer(osm);
};

window.onscroll = function () {
    scrollFunction();
};

function scrollFunction() {
    const btn = document.getElementById("scrollTopBtn");
    if (!btn) return;

    if (document.body.scrollTop > 100 || document.documentElement.scrollTop > 100) {
        btn.style.display = "block";
    } else {
        btn.style.display = "none";
    }
}

function topFunction() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
}