(function(divId){
    var func = (x) => Math.pow(x, 2);
    var funcPrime = (x) => 2*x;
    var domain = [-5, 5];

    var elements = domain[1] - domain[0];
    var xs = Array.from(Array(elements + 1), (_, i) => i + domain[0]);
    var ys = xs.map((x) => func(x));
    var ysPrime = xs.map((x) => funcPrime(x));

    var options = {

    };

    var data = {
        labels: xs,
        datasets: [
            {
                type: 'line',
                label: 'y',
                data: ys,
                fill: false,
                borderColor: 'rgba(41, 128, 185, 1)'
            },
            {
                type: 'line',
                label: 'y`',
                data: ysPrime,
                fill: false,
                borderColor: 'rgba(137, 207, 240, 1)'
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