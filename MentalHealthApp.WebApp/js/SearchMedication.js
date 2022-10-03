function Search() {
    const medication = document.getElementById("searchMed").value;
    const url = `https://www.prospecte.net/?s=${medication}/`;
	fetch(url).then(function (response) {
		return response.json();
	}).then(function (data) {
		// This is the JSON from our response
		console.log(data);
	}).catch(function (err) {
		// There was an error
		console.warn('Something went wrong.', err);
	});
}