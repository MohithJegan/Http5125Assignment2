using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Http5125Assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class J1Controller : ControllerBase

    {
        /// QUESTION - 1 - Deliv-e-droid

        /// curl -H "Content-Type: application/x-www-form-urlencoded" -d "Collisions=2&Deliveries=5" https://localhost:7144/api/J1/Delivedroid

        /// <summary>
        /// A robot droid has to deliver packages, which gains 50 points for package delivered and lose 10 points for collision and
        /// 500 bonus if package delivery is more than collison. It recieves the information through POST method.
        /// </summary>
        /// <param name="Collisions">The number of collisions which is an integer</param>
        /// <param name="Deliveries">The number of package delivered which is an integer</param>
        /// <returns>It returns the final score of the robot droid which is an integer</returns>
        /// <example>
        /// POST : https://localhost:7144/api/J1/Delivedroid
        /// Headers: Content-Type: application/x-www-form-urlencoded
        /// FORM DATA: Collisions=3&Deliveries=2
        /// => 730
        /// POST : https://localhost:7144/api/J1/Delivedroid
        /// Headers: Content-Type: application/x-www-form-urlencoded
        /// FORM DATA: Collisions=10&Deliveries=0
        /// => -100
        ///  POST : https://localhost:7144/api/J1/Delivedroid
        /// Headers: Content-Type: application/x-www-form-urlencoded
        /// FORM DATA: Collisions=3&Deliveries=2
        /// => 70
        /// </example>
        [HttpPost(template: "Delivedroid")]
        [Consumes("application/x-www-form-urlencoded")]
        public int Delivedroid([FromForm] int Collisions, [FromForm] int Deliveries)
        {
            // initialize the variables
            int bonusPoints = 500;
            int packageDelivered = 50 * Deliveries;
            int losePoints = 10 * Collisions;
            int finalScore = packageDelivered - losePoints;
            // add bonus for the below condition
            if (Deliveries > Collisions) finalScore += bonusPoints;
            return finalScore;
        }


        /// QUESTION - 2 - Cupcake Party

        /// curl -X "GET" "https://localhost:7144/api/J1/CupCake/2/5"

        /// <summary>
        /// It calculates the cupcake leftover provided the regular box holds 8 cupcakes, small box holds 3 cupcakes and total student is 28 in a class.
        /// It recieves the information through GET method.
        /// </summary>
        /// <param name="regularBox">The number of regular boxes which is an integer</param>
        /// <param name="smallBox">The number of small boxes which is an integer</param>
        /// <returns>It returns the number of cupcakes left over which is an integer</returns>
        /// <example>
        /// GET https://localhost:7144/api/J1/CupCake/2/5 => regularBox:2 smallBox:5 => 3
        /// GET https://localhost:7144/api/J1/CupCake/2/4 => regularBox:2 smallBox:4 => 0
        /// </example>


        [HttpGet(template:"CupCake/{regularBox}/{smallBox}")]
        public int CupCake(int regularBox, int smallBox)
        {
            // intialize the variables
            int totalStudent = 28;
            int regularBoxQuantity = 8;
            int smallBoxQuantity = 3;
            // compute the calculation for leftover cupcake
            return ((regularBox * regularBoxQuantity) + (smallBox * smallBoxQuantity)) - totalStudent ;
        }
    }
}
