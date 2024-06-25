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
                    $"<div class=\"playerContainer\">" +
                      $"<div class=\"playerImg\"><img src=\"{@player.Image}\"/></div>"+
                      $"<div class=\"playerStat\">"+
                        $"<div class=\"playerNationality\">{player.Nationality}, {player.Role.ToString()}</div>"+
                        $"<div class=\"playerName\">{player.Name}</div>" +
                      $"</div>"+
                      $"<div class=\"playerRating\">{player.Rating}</div>" +
                      $"<div class=\"playerCost\">{player.Price}</div>" +
                    $"</div>";
            }
            return
                $"<div class=\"playerContainer\">" +
                      $"<div class=\"playerImg\"><img src=\"{@player.Image}\"/></div>" +
                      $"<div class=\"playerStat\">" +
                        $"<div class=\"playerNationality\">{player.Nationality}, {player.Role.ToString()}</div>" +
                        $"<div class=\"playerName\"><a asp-action=\"PlayerDetails\" asp-controller=\"Home\" asp-route-id=\"{player.Id}\">{player.Name}</a></div>" +
                      $"</div>" +
                      $"<div class=\"playerRating\">{player.Rating}</div>" +
                      $"<div class=\"playerCost\"></div>" +
                    $"</div>";
        }

        public static string CreateMatchCard(Match match)
        {
            return
                $"<div class=\"matchContainer\">"+
                  $"<div class=\"teamName1\">{match.Teams.First(x => x.Id == match.WinnerId).Name}</div>"+
                  $"<div class=\"matchResult\">{Math.Max(match.Team1Score, match.Team2Score)} ---- {Math.Min(match.Team1Score, match.Team2Score)}</div>"+
                  $"<div class=\"teamName2\">{match.Teams.First(x => x.Id == match.LoserId).Name}</div>"+
                $"</div>";
        }
    }
}
