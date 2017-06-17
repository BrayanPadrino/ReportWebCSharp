using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report/Reporte
        public ActionResult Reporte()
        {

            return View();
        }

        // GET: Report/DescargaPDF
        public ActionResult DescargaPDF()
        {




            {
                #region Datos dummy

                List<Persona> datos = new List<Persona>(){
                                            new Persona() { Nombre = "Sacarias", Apellido = "Piedras del Rio", Codigo = 1234 },
                                            new Persona() { Nombre = "Alan", Apellido = "Brito", Codigo = 4323 },
                                            new Persona() { Nombre = "Marcos", Apellido = "Pinto", Codigo = 9090 }
            };

                #endregion Datos dummy

                string DirectorioReportesRelativo = "~/";
                string urlArchivo = string.Format("{0}\\{1}.{2}", "Reportes", "MiReporte", "rdlc");

                string FullPathReport = string.Format("{0}{1}",
                                        this.HttpContext.Server.MapPath(DirectorioReportesRelativo),
                                         urlArchivo);

                ReportViewer Reporte = new ReportViewer();

                Reporte.Reset();
                Reporte.LocalReport.ReportPath = FullPathReport;
                ReportDataSource DataSource = new ReportDataSource("DS_MiReporte", datos);
                Reporte.LocalReport.DataSources.Add(DataSource);
                Reporte.LocalReport.Refresh();
                byte[] file = Reporte.LocalReport.Render("PDF");

                return File(new MemoryStream(file).ToArray(),
                          System.Net.Mime.MediaTypeNames.Application.Octet,
                          /*Esto para forzar la descarga del archivo*/
                          string.Format("{0}{1}", "archivoprueba.", "PDF"));
            }

        }
    }
}
            /*#region Datos dummy

            List<Persona> datos = new List<Persona>(){
                new Persona() { Nombre = "Sacarias", Apellido = "Piedras del Rio", Codigo = 1234 },
                new Persona() { Nombre = "Alan", Apellido = "Brito", Codigo = 4323 },
                new Persona() { Nombre = "Marcos", Apellido = "Pinto", Codigo = 9090 }
            };

            #endregion Datos dummy

            string DirectorioReportesRelativo = "~/";
            //unir NOMBRE DEL ARCHIVO. EXTENSION
            string urlArchivo = string.Format("{0}\\{1}.{2}", "Reportes","MiReporte", "rdlc");
            
            string FullPathReport = string.Format("{0}{1}",
                  this.HttpContext.Server.MapPath(DirectorioReportesRelativo), urlArchivo);

            ReportViewer Reporte = new ReportViewer();
            
            Reporte.Reset();
            Reporte.LocalReport.ReportPath = FullPathReport;
                                        //NOMBRE DEL DATA SOURCE Y LLENADO CON LISTA 
            ReportDataSource DataSource = new ReportDataSource("DS_MiReporte", datos);
            Reporte.LocalReport.DataSources.Add(DataSource);
            // VERSION 2 PARAMETROS
            // ReportParameter p = new ReportParameter("titulo", "Prueba");
            //Reporte.LocalReport.SetParameters(p);
            // VERSION 2
            Reporte.LocalReport.Refresh();
            byte[] file = Reporte.LocalReport.Render("PDF");

            return File(new MemoryStream(file).ToArray(),
                System.Net.Mime.MediaTypeNames.Application.Octet,
                      //Esto para forzar la descarga del archivo
                      string.Format("{0}{1}", "archivoprueba.", "PDF"));
        }*/
    