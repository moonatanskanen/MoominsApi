using Microsoft.AspNetCore.Mvc;
using MoominsApi.Repo;

namespace MoominsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoominValleyController : ControllerBase
    {
        private static MoominsRepository MoominsRepository = new MoominsRepository();

        //palauttaa listan kaikista muumeista jos name on null
        //palauttaa tietyt muumit nimen perusteella jos name parametri ei ole null
        [HttpGet]
        public List<Moomin> GETMoomins(string? name)
        {
            if (!string.IsNullOrEmpty(name)) //jos nimi ei ole null tai tyhj‰
            {
                var MoominsByName = MoominsRepository.GetAllMoomins(name); //haetaan muumit nimen perusteella

                return MoominsByName;
            }
            else
            {
                //jos nimi on null tai tyhj‰, palautetaan kaikki muumit
                var AllMoomins = MoominsRepository.GetAllMoomins();

                return AllMoomins;
            }
        }

        [HttpPost]
        public ActionResult ADDMoomins(Moomin moomin)
        {
            var AllMoomins = MoominsRepository.GetAllMoomins(); //lista kaikista muumeista

            for (int i = 0; i < AllMoomins.Count; i++) //k‰yd‰‰n muumilista l‰pi
            {
                if (AllMoomins[i].Number == moomin.Number) //jos samalla numerolla lˆytyy jo muumi, palautetaan Conflict 409
                {
                    return Conflict(); //Palauttaa 409
                }

            }

            //jos numerolla ei lˆydy jo muumia, lis‰t‰‰n muumi listaan
            MoominsRepository.AddMoomin(moomin);
            return Created("/api/MoominValley/" + moomin.Number, moomin);
        }

        [HttpGet]
        [Route("moomin/{number}")]
        public ActionResult<Moomin> MoominNumber(int number) //palautetaan actionresult joka antaa mahdollisuuden k‰ytt‰‰ OK, NotFound yms.
        {
            var ListOfMoomins = MoominsRepository.GetAllMoomins(); //muumilista
            Moomin moomin = null; //luodaan muuttuja johon numerolla lˆytyv‰ muumi tallennetaan


            //k‰yd‰‰n kaikki muumit l‰pi ja otetaan talteen se muumi jonka numero t‰sm‰‰
            for (int i = 0; i < ListOfMoomins.Count; i++)
            {
                if (ListOfMoomins[i].Number == number)
                {
                    moomin = ListOfMoomins[i];
                    return Ok(moomin); //OK 200
                }
            }

            //jos kyseisell‰ numerolla ei ole muumia palautetaan Not Found 404
            return NotFound();
        }

        [HttpDelete]
        public ActionResult DELETEMoomin(int nro)
        {
            var ListOfMoomins = MoominsRepository.GetAllMoomins(); //muumilista
            Moomin moomin = null; //muumi muuttuja johon numerolla lˆytynyt muumi tallennetaan

            for (int i = 0; i < ListOfMoomins.Count; i++) //k‰yd‰‰n muumeja l‰pi
            {
                if (ListOfMoomins[i].Number == nro) //t‰sm‰‰kˆ numero
                {
                    moomin = ListOfMoomins[i]; //tallennetaan lˆytynyt muumi muuttuvaan
                    break; //poistutaan loopista
                }
            }

            if (moomin != null) //tarkistetaan ett‰ muumi ei ole null
            {
                MoominsRepository.DeleteMoomin(moomin.Number); //poistetaan muumi
                return NoContent();
            }

            return NotFound(); //jos muumia ei lˆytynyt palautetaan NotFound
        }
    }
}
