(function(divId){
    var last = -1;
    var start = 0;
    var end = 1;
    var next = 2;

    var midpoint = start + (end - start) * 0.5;
    var xs = []; for (var x = 0; x <= 1; x+= 0.1) { xs.push(x); } 
    var step = xs.map((x) => (x > midpoint ? end : start));
    var linear = xs.map((x) => x * end + (1-x) * start);
    var cosine = xs.map((x) => {
        var t2 = (1 - Math.cos(x * Math.PI)) / 2;
        return start * (1 - t2) + end * t2;
    });
    var smoothstep = xs.map((t) => {
        var _01 = t * t * t * (t * (t * 6 - 15) + 10); 
        return _01 * end + (1 - _01) * start;
    });
    var cubic = xs.map((t) => {
        var t2 = t * t;
        var a0 = next - end - last + start;
        var a1 = last - start - a0;
        var a2 = end - last;
        var a3 = start;

        return(a0 * t * t2 + a1 * t2 + a2 * t + a3);
    });

    var options = {
        legend: {
            position: 'right'
        },
        scales: {
            xAxes: [{
                ticks:{
                    stepSize : 0.1,
                },
                display: false
            }]
        }
    };

    var data = {
        labels: xs,
        datasets: [
            {
                type: 'line',
                label: 'step',
                data: step,
                fill: false,
                borderColor: 'rgba(255, 255, 0, 1)',
                lineTension: 0
            },
            {
                type: 'line',
                label: 'linear',
                data: linear,
                fill: false,
                borderColor: 'rgba(41, 128, 185, 1)'
            },
            {
                type: 'line',
                label: 'cosine',
                data: cosine,
                fill: false,
                borderColor: 'rgba(245, 66, 66, 1)'
            },
            {
                type: 'line',
                label: 'smoothstep',
                data: smoothstep,
                fill: false,
                borderColor: 'rgba(21, 140, 6, 1)'
            },
            {
                type: 'line',
                label: 'cubic',
                data: cubic,
                fill: false,
                borderColor: 'rgba(162, 0, 255, 1)'
            }
        ]
    };

    var div = document.getElementById(divId);
    if (!div)
        return;
        
    var ctx = div.getContext("2d");
    var chart = new Chart(ctx, {
        type: 'bar',
        data: data,
        options: options
    });
})("c1");