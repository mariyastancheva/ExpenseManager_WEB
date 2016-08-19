$(function() {

    Morris.Donut({
        element: 'morris-donut-chart',
        data: dataTotal,
        resize: true
    });
   
    Morris.Donut({
        
        element: 'morris-donut-chart2',
        data: dataFromToday,
        resize: true
    });
    Morris.Donut({

        element: 'morris-donut-chart3',
        data: dataFromThisMonth,
        resize: true
    });

    Morris.Donut({

        element: 'morris-donut-chart4',
        data: dataFromThisYear,
        resize: true
    });
});
