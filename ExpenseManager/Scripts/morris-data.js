$(function() {

 

    Morris.Donut({
        element: 'morris-donut-chart',
        data: data,
        resize: true
    });
   
    Morris.Donut({
        
        element: 'morris-donut-chart2',
        data: [{
            label: "COCAINE",
            value: 12
        }, {
            label: "HASH",
            value: 30
        }, {
            label: "AMPHETAMINE",
            value: 20
        }, {
            label: "KOKA-KOLA",
            value: 13
        }
        ],
        resize: true
    });
    
});
