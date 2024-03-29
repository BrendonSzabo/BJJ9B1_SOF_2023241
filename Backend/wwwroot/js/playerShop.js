const playerList = [
    { UserName: 'Toplaner tmp', Image: 'lib/assets/placeholder.jpg', Role: 'Top', Rating: '100' },
    { UserName: 'Jungler tmp', Image: 'lib/assets/placeholder.jpg', Role: 'Jg', Rating: '100' },
    { UserName: 'Midlaner tmp', Image: 'lib/assets/placeholder.jpg', Role: 'Mid', Rating: '100' },
    { UserName: 'Adc tmp', Image: 'lib/assets/placeholder.jpg', Role: 'ADC', Rating: '100' },
    { UserName: 'Support tmp', Image: 'lib/assets/placeholder.jpg', Role: 'Sup', Rating: '100' }
];

const ownTeam = [
    { name: 'Toplaner tmp', isSet: false },
    { name: 'Jungler tmp', isSet: false },
    { name: 'Midlaner tmp', isSet: false },
    { name: 'Adc tmp', isSet: false },
    { name: 'Support tmp', isSet: false }
];

function renderPlayerShop() {
    const container = document.getElementById('playerShopInitializer');
    container.innerHTML = '';

    playerList.forEach((player, index) => {
        container.appendChild(createPlayerDiv(player, 'ShopPlayers'));
    });
}

function renderOwnTeam() {
    const container = document.getElementById('ownTeamInitializer');
    container.innerHTML = '';

    playerList.forEach((player, index) => {
        if (player.UserName == ownTeam[0].name) {
                container.appendChild(createPlayerDiv(player, 'OwnPlayers'));
            ownTeam[0].isSet = true;
        }
        else if (player.UserName == ownTeam[1].name) {
            if (ownTeam[0].isSet != false) {
                container.appendChild(createPlayerDiv(player, 'OwnPlayers'));
                ownTeam[1].isSet = true;
            }
        }
        else if (player.UserName == ownTeam[2].name) {
            if (ownTeam[1].isSet != false) {
               
            container.appendChild(createPlayerDiv(player, 'OwnPlayers'));
            ownTeam[2].isSet = true;
            }
        }
        else if (player.UserName == ownTeam[3].name) {
            if (ownTeam[2].isSet != false) {
                
            container.appendChild(createPlayerDiv(player, 'OwnPlayers'));
            ownTeam[3].isSet = true;
            }
        }
        else if (player.UserName == ownTeam[4].name) {
            if (ownTeam[3].isSet != false) {
                
            container.appendChild(createPlayerDiv(player, 'OwnPlayers'));
            ownTeam[4].isSet = true;
            }
        }
    });
}

function createPlayerDiv(player, own) {
    const playerDiv = document.createElement('div');
    playerDiv.classList.add(own);
    playerDiv.innerHTML = `
        <div class="button-body">
            <label class="labl">
                <input type="radio" name="${own}" value="${player}" checked>
                <div class="listbutton-main">
                    <div class="listbutton-column">
                        <div class="listbutton-icon">
                            <img src="${player.Image}" class="profile-image" />
                        </div>
                    </div>
                    <div class="listbutton-column-text">
                        <div class="listbutton-text">${player.UserName}</div>
                        <div class="listbutton-text">${player.Role}</div>
                    </div>
                    <div class="listbutton-column" style="align-self: right">
                        <div class="listbutton-text">${player.Rating}</div>
                    </div>
                </div>
            </label>
        </div>
    `;
    return playerDiv;
}

document.addEventListener('DOMContentLoaded', function () {
    renderPlayerShop();
    renderOwnTeam();
});
