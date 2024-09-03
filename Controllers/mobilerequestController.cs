using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wapimobile.Models;

namespace wapimobile.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class mobilerequestController : ControllerBase
    {
        /*private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };*/

        private readonly ILogger<mobilerequestController> _logger;

        public mobilerequestController(ILogger<mobilerequestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> getResumenDia_BlueFenyx(int mobileid, int? plataforma = 0)
        {   
            string GalonesXHora1 = null;
            string StrResultado = "";
            string strError = "";
            string strODBC = "";
            // var usuario = _Session.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario").First();
            try
            {

                strODBC = "DSN=FleetManager;UID=fleetmanageruser;PWD=Ibc09KupDC;";
                if (plataforma == 1)
                {
                    strODBC = "DSN=Bluefenyx;UID=mantenimiento;PWD=mttofleet;";
                }
                if (plataforma == 2)
                {
                    strODBC = "DSN=PuntoCiudadano;UID=Mantenimiento;PWD=mttofleet;";
                }




                Summary.Summ ResumenReco = new Summary.Summ();
                List<DatosResumenDia> lDatosResDia = new List<DatosResumenDia>();
                string strFechainicio, strFechafin/*, strError = ""*/;
                double MaxSpeed, AvgSpeed, SumDistTot, TimeProd, TimeRecoProd, TimeDetProd, TimeDetVehON, MaxTimeStopOn, Kmconsumidos;
                int NumParadas;

                DateTime dtFH = DateTime.Now.AddHours(-6);
                DateTime dtFechaHoy = new DateTime(dtFH.Year, dtFH.Month, dtFH.Day, 6, 0, 0);

                strFechainicio = dtFechaHoy.ToString("dd-MM-yyyy HH:mm:ss");
                //strFechafin = DateTime.Now.AddHours(6).ToString("dd-MM-yyyy HH:mm:ss");
                strFechafin = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                string strMobileid = Convert.ToString(mobileid);
                MaxSpeed = 0;
                AvgSpeed = 0;
                SumDistTot = 0;
                TimeDetProd = 0;
                TimeDetVehON = 0;
                TimeProd = 0;
                TimeRecoProd = 0;
                NumParadas = 0;
                MaxTimeStopOn = 0;
                Kmconsumidos = 0;



                // List<DatosResumenDia> DatosResumenDia = new List<DatosResumenDia>();
                if (ResumenReco.ObtenerResumen(strODBC, strFechainicio, strFechafin, strMobileid, ref MaxSpeed, ref AvgSpeed, ref SumDistTot, ref NumParadas, ref TimeProd, ref TimeRecoProd, ref TimeDetProd, ref TimeDetVehON, ref MaxTimeStopOn, ref Kmconsumidos, ref strError) == 1)
                {
                    /* DatosResumenDia.Add(new DatosResumenDia
                     {
                         MaxSpeed_Title = "Máxima Velocidad (km/h):",
                         MaxSpeed = Math.Round(MaxSpeed, 2),

                         SumDistTot_Title = "Distancia Total (km):",
                         SumDistTot = Math.Round(SumDistTot, 2),

                         TimeProd_Title = "Tiempo Producción (min):",
                         TimeProd = Math.Round(TimeProd, 2),

                         TimeRecoProd_Title = "Tiempo Recorrido en Producción (min):",
                         TimeRecoProd = Math.Round(TimeRecoProd, 2),

                         TimeDetProd_Title = "Tiempo Detenido en Producción (min):",
                         TimeDetProd = Math.Round(TimeDetProd, 2),

                         TimeDetVehON_Title = "Ralentí (min):",
                         TimeDetVehON = Math.Round(TimeDetVehON, 2),

                         MaxTimeStopOn_Title = "Máximo Ralentí (min):",
                         MaxTimeStopOn = Math.Round(MaxTimeStopOn, 2),

                         Kmconsumidos_Title = "Combustible Consumido por Kilometraje Recorrido (gal):",
                         Kmconsumidos = Math.Round(Kmconsumidos, 2),

                         CombConsumidos_Title = "Combustible Consumido por Tiempo Encendido (gal):",
                         CombConsumidos = Math.Round(((TimeRecoProd + TimeDetVehON) / 60) * Convert.ToDouble(GalonesXHora1))
                     });

                     */
                    StrResultado = "MaxSpeed" + "¤";
                    StrResultado += "AvgSpeed" + "¤";
                    StrResultado += "SumDistTot" + "¤";
                    StrResultado += "TimeDetProd" + "¤";
                    StrResultado += "TimeDetVehON" + "¤";
                    StrResultado += "TimeProd" + "¤";
                    StrResultado += "TimeRecoProd" + "¤";
                    StrResultado += "NumParadas" + "¤";
                    StrResultado += "MaxTimeStopOn" + "¤";
                    StrResultado += "Kmconsumidos" + "¥";

                    // Creando los datos
                    StrResultado += Math.Round(MaxSpeed, 2) + "" + "¤";
                    StrResultado += Math.Round(AvgSpeed, 2) + "" + "¤";
                    StrResultado += Math.Round(SumDistTot, 2) + "" + "¤";
                    StrResultado += Math.Round(TimeDetProd, 2) + "" + "¤";
                    StrResultado += Math.Round(TimeDetVehON, 2) + "" + "¤";
                    StrResultado += Math.Round(TimeProd, 2) + "" + "¤";
                    StrResultado += Math.Round(TimeRecoProd, 2) + "" + "¤";
                    StrResultado += NumParadas + "" + "¤";
                    StrResultado += Math.Round(MaxTimeStopOn, 2) + "" + "¤";
                    StrResultado += Math.Round(Kmconsumidos, 2) + "";


                }
                else
                {
                    StrResultado = strError;
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return await Task.Run(() =>
                {
                    return new JsonResult(new { Result = strError/*DatosResumenDia*/ });
                });
            }

            return await Task.Run(() =>
            {
                return new JsonResult(new { Result = StrResultado/*DatosResumenDia*/ });
            });

        }
        /*[HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }*/
    }
}
