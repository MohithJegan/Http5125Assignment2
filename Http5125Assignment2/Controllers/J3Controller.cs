using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Http5125Assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class J3Controller : ControllerBase

    {
        /// QUESTION - 5 - Secret Instructions

        /// curl -X "POST" -d "instruction=57234,00907,34100,99999" "https://localhost:7144/api/J3/SecretInstruction"

        /// <summary>
        /// Decodes the instructions given in order to find the secret formula set by the professor.
        /// It recieves the information through POST method.
        /// </summary>
        /// <param name="instruction">It takes the comma separated numbers in string format</param>
        /// <returns>It returns the decoding of the instruction either right or left followed by the number of steps to be taken in that direction in string</returns>
        /// <example>
        /// The direction is found by below rules:
        /// -> sum of the first two digits is odd, then the direction to turn is left
        /// -> sum of the first two digits is even and not zero, then the direction to turn is right
        /// -> sum of the first two digits is zero, then the direction to turn is the same as the previous instruction
        /// POST : https://localhost:7144/api/J3/SecretInstruction
        /// Headers: Content-Type: application/x-www-form-urlencoded
        /// FORM DATA: instruction=57234,00907,34100,99999
        /// => right 234
        /// => right 907
        /// => left  100
        /// </example>


        [HttpPost(template:"SecretInstruction")]
        [Consumes("application/x-www-form-urlencoded")]
        public string SecretInstruction([FromForm] string instruction)
        {
            // initialize the variables
            string[] instructionList = instruction.Split(",");
            string direction = "";
            string decodedInstruction = "";
            // Loop the instruction list and not the last item which is "99999" 
            for (var i = 0; i < instructionList.Length - 1; i++)
            {
                int total = 0;
                // get the two characters from each instruction
                var charArray =  instructionList[i].ToCharArray(0, 2);
                for(int j = 0; j < 2; j++)
                {
                    // convert to number to add 
                    total += int.Parse(string.Join("",charArray[j]));
                   
                }
                if (total % 2 == 1)
                {
                    direction = "left";
                  
                    decodedInstruction += (direction + " " + string.Join("", instructionList[i].ToCharArray(2, instructionList[i].Length - 2)))+"\r\n";
                }
                else if(total%2==0 && total != 0)
                {
                    direction = "right";
                  
                    decodedInstruction += (direction + " " + string.Join("", instructionList[i].ToCharArray(2, instructionList[i].Length - 2))) + "\r\n";
                }
                else
                {
                    
                    decodedInstruction += (direction + " " + string.Join("", instructionList[i].ToCharArray(2, instructionList[i].Length - 2))) + "\r\n";
                }
            }
            
            return decodedInstruction;
        }
    }
}
