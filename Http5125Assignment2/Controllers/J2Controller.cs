using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Http5125Assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class J2Controller : ControllerBase
    {
        /// QUESTION - 3 - Chili Peppers

        /// curl -X "GET" "https://localhost:7144/api/J2/ChiliPeppers?Ingredients=Poblano,Cayenne,Thai,Poblano"

        /// <summary>
        /// Sum up the comma separated peppers with corresponding Scolville Heat Units (SHU) value. 
        /// It recieves the information through GET method.
        /// </summary>
        /// <param name="Ingredients">Comma separated peppers which is string</param>
        /// <returns>It returns the total spiciness of the pepper which is an integer</returns>
        /// <example>
        /// GET https://localhost:7144/api/J2/ChiliPeppers?Ingredients=Poblano,Cayenne,Thai,Poblano =>Ingredients: "Poblano,Cayenne,Thai,Poblano" => 118000
        /// GET https://localhost:7144/api/J2/ChiliPeppers?Ingredients=Habanero,Habanero,Habanero,Habanero,Habanero =>Ingredients: "Habanero,Habanero,Habanero,Habanero,Habanero" => 625000
        /// GET https://localhost:7144/api/J2/ChiliPeppers?Ingredients=Poblano,Mirasol,Serrano,Cayenne,Thai,Habanero,Serrano =>Ingredients: "Poblano,Mirasol,Serrano,Cayenne,Thai,Habanero,Serrano" => 278500
        /// </example>
        [HttpGet(template: "ChiliPeppers")]
        public int ChiliPeppers(string Ingredients)
        {
            // get the pepper items and make it an array
            string[] pepperList = Ingredients.Split(",");
            int totalSpicy = 0;
            for(int i = 0; i < pepperList.Length; i++)
            {
                // add the spicy value according to the SHU table
                if (pepperList[i] == "Poblano") totalSpicy += 1500;
                else if (pepperList[i] == "Mirasol") totalSpicy += 6000;
                else if (pepperList[i] == "Serrano") totalSpicy += 15500;
                else if (pepperList[i] == "Cayenne") totalSpicy += 40000;
                else if (pepperList[i] == "Thai") totalSpicy += 75000;
                else if (pepperList[i] == "Habanero") totalSpicy += 125000;
                else totalSpicy += 0;
            }
            return totalSpicy;
        }


        /// QUESTION - 4 - Fergusonball Ratings

        /// curl -X "POST" -d "totalPlayer=3&points=12,4,10,3,9,1" "https://localhost:7144/api/J2/Rating"

        /// <summary>
        /// It calculates the number of players on a team who have a star rating greater than 40. 5 stars will be awarded for score and will lose 3 stars for a foul. If all
        /// the players have a star rating greater than 40, then the team is considered a gold team represented by "+" right after the total team player. 
        /// It recieves the information through POST method.
        /// </summary>
        /// <param name="totalPlayer">The total number of players on the team which is an integer</param>
        /// <param name="points">The number of points and fouls committed by a player through comma separated numbers in string</param>
        /// <returns>It returns the star rating which is string.</returns>
        /// <example>
        /// POST : https://localhost:7144/api/J2/Rating
        /// Headers: Content-Type: application/x-www-form-urlencoded
        /// FORM DATA: totalPlayer=3&points=12,4,10,3,9,1
        /// => 3+
        /// POST : https://localhost:7144/api/J2/Rating
        /// Headers: Content-Type: application/x-www-form-urlencoded
        /// FORM DATA: totalPlayer=2&points=8,0,12,1
        /// => 1
        /// </example>


        [HttpPost(template: "Rating")]
        [Consumes("application/x-www-form-urlencoded")]
        public string Rating([FromForm] int totalPlayer, [FromForm] string points)
        {
            // intialize the variable and split the points given to make it an array
            int teamPlayer = 0;
            string[] pointsList = points.Split(",");
            for(int i = 0; i < pointsList.Length-1; i=i+2)
            {
              // compute the rating for the player
              int player = (Convert.ToInt32(pointsList[i])) * 5 - (Convert.ToInt32(pointsList[i+1])) * 3;  
                if (player > 40)
                {
                    teamPlayer += 1;
                }
            }
            if (teamPlayer == totalPlayer)
            {
                return $"{teamPlayer}+";
            }
            return teamPlayer.ToString();
        }

    }
}
