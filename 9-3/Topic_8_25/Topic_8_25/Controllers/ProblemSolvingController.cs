using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WepAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemSolvingController : ControllerBase
    {
        //[Route("Sum")]
        //[HttpGet]
        //public IActionResult Sumation(string num)
        //{

        //    var sum = num.Split("+");
        //    var num1 = Convert.ToInt32(sum[0]);
        //    var num2 = Convert.ToInt32(sum[1]);


        //    var result = num1 + num2;

        //    return Ok(result);
        //}

        //[Route("Minuse")]
        //[HttpGet]
        //public IActionResult Minuse(string num)
        //{

        //    var sum = num.Split("-");
        //    var num1 = Convert.ToInt32(sum[0]);
        //    var num2 = Convert.ToInt32(sum[1]);


        //    var result = num1 - num2;

        //    return Ok(result);
        //}

        //[Route("Multi")]
        //[HttpGet]
        //public IActionResult Multi(string num)
        //{

        //    var sum = num.Split("*");
        //    var num1 = Convert.ToInt32(sum[0]);
        //    var num2 = Convert.ToInt32(sum[1]);


        //    var result = num1 * num2;

        //    return Ok(result);
        //}

        //[Route("Divide")]
        //[HttpGet]
        //public IActionResult Divide(string num)
        //{

        //    var sum = num.Split("/");
        //    var num1 = Convert.ToInt32(sum[0]);
        //    var num2 = Convert.ToInt32(sum[1]);


        //    var result = num1 / num2;

        //    return Ok(result);
        //}

        [Route("Proplem/8/28")]
        [HttpGet]
        public IActionResult getbool(int num1, int num2)
        {
            if (num1 == 30 || num2 == 30)
            {
                return Ok(true);
            }
            if (num1 + num2 == 30)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }


        [Route("Proplem/2/8/28")]
        [HttpGet]
        public IActionResult getnumber(int num1)
        {
            if (num1 >= 0 )
            {
                if (num1 % 3 == 0 || num1 % 7 == 0)
                {
                    return Ok("true");
                }

            } 

                return Ok("false");

        }


        //لما جربت اعمل get ما زبط ف لازم post 
        // الفكرة انو الجيت بتجيب داتا ف من وين بدها اتجيب هاي القيم (مااااااااااا ندري )
        [HttpPost("OddNumber")]
        public IActionResult OddNumber([FromForm] List<int> numbers)
        {
            var sum = numbers
                .GroupBy(x => x) // الاشياء المتشابهه بتحطهم بعيلة لحالهم 
                .Where(p => p.Count() % 2 != 0)
                //.Select(p => p.Key) // فيها وبلاها عادي 
                .ToList();


            return Ok(sum);
        }
    }
}
