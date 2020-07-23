(function(divId){
    var func = (x) => Math.pow(x, 2);
    var domain = [-5, 5];

    var elements = domain[1] - domain[0];
    var xs = Array.from(Array(elements + 1), (_, i) => i + domain[0]);
    var ys = xs.map((x) => func(x));

    var options = {

    };

    var data = {
        labels: xs,
        datasets: [
            {
                type: 'line',
                label: 'y',
                data: ys,
                backgroundColor: 'rgba(41, 128, 185, 0.5)'
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