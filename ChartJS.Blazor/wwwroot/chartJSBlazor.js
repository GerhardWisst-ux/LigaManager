var map = new Map();
export function createChart(id, configs) {
    if (map === undefined || map === null) {
        map = new Map();
    }
    var chart = new Chart(
        document.getElementById(id),
        configs
    );

    map[id] = chart;
    return true;
}

export function destroyChart(id) {
    var chart = map[id];
    chart.destroy();
    map.delete(id);
}
