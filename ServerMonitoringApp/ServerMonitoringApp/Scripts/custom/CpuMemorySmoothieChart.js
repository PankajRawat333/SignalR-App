var chart = new SmoothieChart({ millisPerPixel: 42 }),
    canvas = document.getElementById('smoothie-chart'),
    series = new TimeSeries();

chart.addTimeSeries(series, { lineWidth: 2, strokeStyle: '#00ff00' });
chart.streamTo(canvas, 500);

// Data
var line1 = new TimeSeries();
var line2 = new TimeSeries();

// Add a random value to each line every second
setInterval(function () {
    var cpu = 0;
    var memory = 100;
    if ($('#cpu').text() !== '') {
        cpu = $('#cpu').text();
    }
    if ($('#memory').text() !== '') {
        memory = $('#memory').text();
    }

    line1.append(new Date().getTime(), memory);
    line2.append(new Date().getTime(), cpu);
}, 2000);

// Add to SmoothieChart
chart.addTimeSeries(line1);
chart.addTimeSeries(line2);