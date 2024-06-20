using Backend.Models;

namespace Backend.WebHelpers
{
    public static class Helpers
    {
        public static string CreatePlayerCard(Player player, bool isShop)
        {
            if (isShop)
            {
                return
                $"<div class='playerCard' data-player-id='{player.Id}' ondblclick='submitPlayerForm(this)'>" +
                    $"<div class='playerImgCol'>{player.Image}</div>" +
                    "<div class='playerStatCol'>" +
                        $"<div>{player.Name}, {player.Nationality}</div>" +
                        $"<div>{player.Role} <p class='playerPrice'>{player.Price}</p></div>" +
                    "</div>" +
                    "<div class='playerImgCol'>100</div>" +
                "</div>";
            }
            return
                $"<div class='playerCard' data-player-id='{player.Id}' ondblclick='submitPlayerForm(this)'>" +
                    $"<div class='playerImgCol'>{player.Image}</div>" +
                    "<div class='playerStatCol'>" +
                        $"<div>{player.Name}, {player.Nationality}</div>" +
                        $"<div>{player.Role}</div>" +
                    "</div>" +
                    "<div class='playerImgCol'>100</div>" +
                "</div>";
        }

        public static string CreateMatchCard(Match match)
        {
            return
                $"< div class='matchCard' data-match-id='{match.Id}' ondblclick='submitMatchForm(this)'>" +
                    $"<div class='teamName'>{match.Teams.First(x => x.Id == match.WinnerId).Name}</div>" +
                    "<div class='matchScore'>" +
                        $"<div>{Math.Max(match.Team1Score, match.Team2Score)} ----- {Math.Min(match.Team1Score, match.Team2Score)}</div>" +
                    "</div>" +
                    $"<div class='teamName'>{match.Teams.First(x => x.Id == match.LoserId).Name}</div>" +
                "</div>";
        }
    }
}
