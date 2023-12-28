new Chart(document.getElementById("barchart"), {
	type: 'bar',
	data: {
		labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', ' November', 'December'],
		datasets: [{
			data: [55,179,218,312,194,166,188,230,732,180,172,596],
			label: '2022',
			backgroundColor: "#03c895",
			borderWidth: 1,
		}, {
			data: [30,10,70,15,30,20,70,80,100,90,20,100],
			label: '2021',
			backgroundColor: "#E7EDF6",
			borderWidth: 1,
		}]
	},
	options: {
		responsive: true,
		legend: {
			position: 'top',
		},
	}
});
