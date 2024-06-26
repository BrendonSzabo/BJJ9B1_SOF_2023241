document.addEventListener('DOMContentLoaded', (event) => {
    const api = "api/Auto";
    function setMatch(user) {
        try {
            const response = await fetch(api + '/SetMatch', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(user)
            });

            if (response.ok) {
                document.getElementById('scheduled').innerHTML = 'Match scheduled';
                setTimeout(() => {
                    document.getElementById('scheduled').innerHTML = '';
                }, 5000);
            } else {
                // Handle error response
                const error = await response.text();
                console.error('Error scheduling match:', error);
            }
        } catch (error) {
            // Handle network or other errors
            console.error('Fetch error:', error);
        }
    }

    window.setMatch = setMatch;
});