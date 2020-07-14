/* globals Chart:false, feather:false */

(function () {
  'use strict'

  feather.replace()

  // Graphs
  var ctx = document.getElementById('myChart')
  // eslint-disable-next-line no-unused-vars
  var myChart = new Chart(ctx, {
    type: 'line',
    data: {
      labels: [
        'Lunes',
        'Martes',
        'Miercoles',
        'Jueves',
        'Viernes',
        'Sabado',
        'Domingo'
      ],
      datasets: [{
        data: [
          15,
          16,
          14,
          20,
          25,
          30,
          31
        ],
        lineTension: 0,
        backgroundColor: 'transparent',
        borderColor: '#D57B22',
        borderWidth: 4,
        pointBackgroundColor: '#007bff'
      }]
    },
    options: {
      scales: {
        yAxes: [{
            ticks: {
                beginAtZero: true
          }
        }]
      },
      legend: {
        display: false
      }
    }
  })
}())
