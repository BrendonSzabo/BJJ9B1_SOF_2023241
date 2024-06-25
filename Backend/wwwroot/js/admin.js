document.addEventListener('DOMContentLoaded', (event) => {
    let results = document.getElementById('results');
    const api = "api/Api";
    function unfinishedMatches() {
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({})
        };
        callApiData(api + "/ListUnfinishedMatches", requestOptions);
    }

    function seedData() {
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({})
        };
        callApiData(api + "/SeedData", requestOptions);
    }

    function finishedMatches() {
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({})
        };
        callApiData(api + "/ListFinishedMatches", requestOptions);
    }

    function getUsers() {
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({})
        };
        callApiData(api + "/ListUsers", requestOptions);
    }
    
    function listPlayers() {
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({})
        };
        callApiData(api + "/ListPlayers", requestOptions);
    }

    function playersByRating() {
        const lower = document.getElementById('lower').value;
        const higher = document.getElementById('higher').value;

        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ lower: parseInt(lower), higher: parseInt(higher) })
        };
        callApiData(api + "/ListPlayerByRating", requestOptions);
    }

    function userByRating() {
        const lower = document.getElementById('lower').value;
        const higher = document.getElementById('higher').value;

        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ lower: parseInt(lower), higher: parseInt(higher) })
        };
        callApiData(api + "/ListUserByRating", requestOptions);
    }

    function teamByRating() {
        const lower = document.getElementById('lower').value;
        const higher = document.getElementById('higher').value;

        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ lower: parseInt(lower), higher: parseInt(higher) })
        };
        callApiData(api + "/ListTeamByRating", requestOptions);
    }

    function callApiData(apiUrl, requestOptions) {
        results.textContent = '';
        fetch(apiUrl, requestOptions)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {
                results.textContent = JSON.stringify(data, null, 2);
            })
            .catch(error => {
                console.error('Error:', error);
            });
    }

    window.unfinishedMatches = unfinishedMatches;
    window.finishedMatches = finishedMatches;
    window.playersByRating = playersByRating;
    window.userByRating = userByRating;
    window.teamByRating = teamByRating;
    window.getUsers = getUsers;
    window.seedData = seedData;
    window.listPlayers = listPlayers;
});