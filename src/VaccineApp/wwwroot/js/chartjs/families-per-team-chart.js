function GenerateChart(labeltodisplay, datatodisplay) {
    new Chart(document.getElementById("bar-chart"), {
        type: 'bar',
        data: {
            labels: labeltodisplay,
            datasets: [{
                data: datatodisplay,
                label: "numbers",
                backgroundColor: "#3addbb",
                borderColor: [
                    "#3e95cd", "#3e9aad", "#3bb5cd", "#3dd5cd", "#3e9ccd", "#3aa5cd",
                    ],
                borderswidth: 10
            }
            ]
        },
        options: {
            title: {
                display: true,
                text: 'Households per team'
            }
        }
    });
}