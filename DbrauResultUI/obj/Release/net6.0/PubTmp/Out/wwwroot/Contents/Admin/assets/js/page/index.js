
$(function() {
    "use strict";

    // block-header bar chart js
    $('.chart_1').sparkline('html', {
        type: 'bar',
        height: '80px',
        barSpacing: 7,
        barWidth: 3,
        barColor: '#467fcf',        
    });
    $('.chart_2').sparkline('html', {
        type: 'pie',
        offset: 90,
        width: '80px',
        height: '80px',
        sliceColors: ['#2bcbba', '#de5d83', '#ffc107']
    });
    $('.chart_3').sparkline('html', {
        type: 'bar',
        height: '80px',
        barSpacing: 7,
        barWidth: 3,
        barColor: '#28a745',        
    });
    $('.chart_4').sparkline('html', {
        type: 'bar',
        height: '80px',
        barSpacing: 7,
        barWidth: 3,
        barColor: '#45aaf2',        
    });

    // Sales Analytics chart
    $(document).ready(function(){
        var chart = c3.generate({
            bindto: '#sales_analytics', // id of chart wrapper
            data: {
                columns: [
                    // each columns data
                    ['data1', 13, 8, 1, 2, 21, 9, 12, 10, 7, 13, 65, 10, 12, 6, 4, 16, 7],
                    ['data2', 0, 0, 1, 2, 7, 5, 6, 8, 24, 7, 12, 5, 6, 3, 2, 2, 17],
                    ['data3', 0, 4, 1, 0, 2, 6, 1, 0, 2, 3, 0, 2, 3, 2, 12, 0, 23]
                ],
                classes: {
                    data1: 'filled',
                    data2: 'filled',
                    data3: 'filled'
                },
                type: 'line', // default type of chart
                groups: [
                    [ 'data1', 'data2', 'data3']
                ],
                colors: {
                    'data1': buzzer.colors["blue"],
                    'data2': buzzer.colors["orange"],
                    'data3': buzzer.colors["green"]
                },
                names: {
                    // name of each serie
                    'data1': 'Revenue',
                    'data2': 'Income',
                    'data3': 'Growth'
                }
            },
            axis: {
                y: {
                    padding: {
                        bottom: 0,
                    },
                    show: false,
                        tick: {
                        outer: false
                    }
                },
                x: {
                    padding: {
                        left: 0,
                        right: 0
                    },
                    show: false
                }
            },
            legend: {
                position: 'inset',
                padding: 0,
                inset: {
                    anchor: 'top-left',
                    x: 20,
                    y: 8,
                    step: 10
                }
            },
            tooltip: {
                format: {
                    title: function (x) {
                        return '';
                    }
                }
            },
            padding: {
                bottom: 0,
                left: -1,
                right: -1
            },
            point: {
                show: false
            }
        });
    });

    // Email Statistics
    $(document).ready(function() {
        var chart = c3.generate({
            bindto: '#chart-emails',
            padding: {
            bottom: 24,
            top: 0
        },
        data: {
                type: 'donut',
                names: {
                data1: 'Open',
                data2: 'Bounce',
                data3: 'Unsubscribe',
            },
                columns: [
                    ['data1', 30],
                    ['data2', 50],
                    ['data3', 25],
                ],
                colors: {
                    data1: buzzer.colors.blue,
                    data2: buzzer.colors.red,
                    data3: buzzer.colors.yellow,
                }
            },
            donut: {
                label: {
                    show: false
                }
            },
            legend: {
                show: true
            },

        });
    });

    // Visitors Overview
    if( $('#world-map-markers').length > 0 ){

        $('#world-map-markers').vectorMap(
        {
            map: 'world_mill_en',
            backgroundColor: 'transparent',
            borderColor: '#fff',
            borderOpacity: 0.25,
            borderWidth: 0,
            color: '#e6e6e6',
            regionStyle : {
                initial : {
                fill : '#e9ecef'
                }
            },

            markerStyle: {
            initial: {
                        r: 5,
                        'fill': '#fff',
                        'fill-opacity':1,
                        'stroke': '#000',
                        'stroke-width' : 1,
                        'stroke-opacity': 0.4
                    },
                },
        
            markers : [{
                latLng : [21.00, 78.00],
                name : 'INDIA : 350'
            
            },
                {
                latLng : [-33.00, 151.00],
                name : 'Australia : 250'
                
            },
                {
                latLng : [36.77, -119.41],
                name : 'USA : 250'
                
            },
                {
                latLng : [55.37, -3.41],
                name : 'UK   : 250'
                
            },
                {
                latLng : [25.20, 55.27],
                name : 'UAE : 250'
            
            }],

            series: {
                regions: [{
                    values: {
                        "US": '#17a2b8',
                        "SA": '#28a745',
                        "AU": '#de5d83',
                        "IN": '#fd9644',
                        "GB": '#a55eea',
                    },
                    attribute: 'fill'
                }]
            },
            hoverOpacity: null,
            normalizeFunction: 'linear',
            zoomOnScroll: false,
            scaleColors: ['#000000', '#000000'],
            selectedColor: '#000000',
            selectedRegions: [],
            enableZoom: false,
            hoverColor: '#fff',
        });
    }
});
