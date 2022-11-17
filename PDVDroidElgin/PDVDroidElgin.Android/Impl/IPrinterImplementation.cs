using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PDVDroidElgin.Droid.Impl;
using PDVDroidElgin.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(IPrinterImplementation))]
namespace PDVDroidElgin.Droid.Impl
{
    class IPrinterImplementation : IPrinter
    {
        public int PrinterInternalImpStart()
        {
            return MainActivity.Printer.PrinterInternalImpStart();
        }

        public int PrinterExternalImpStart(Dictionary<string, string> parametros)
        {
            return MainActivity.Printer.PrinterExternalImpStart(parametros);
        }

        public void PrinterStop()
        {
            MainActivity.Printer.PrinterStop();
        }

        public int AvancaLinhas(int linesNumber)
        {
            return MainActivity.Printer.AvancaLinhas(linesNumber);
        }

        public int CutPaper(int cut)
        {
            return MainActivity.Printer.CutPaper(cut);
        }

        public int ImprimeBarCode(Dictionary<string, string> parametros)
        {
            return MainActivity.Printer.ImprimeBarCode(parametros);
        }

        public int ImprimeQR_CODE(Dictionary<string, string> parametros)
        {
            return MainActivity.Printer.ImprimeQR_CODE(parametros);
        }

        public int ImprimeImagem(Dictionary<string, string> parametros)
        {
            return MainActivity.Printer.ImprimeImagem(parametros);
        }

        public int ImprimeXMLNFCe(string xml, int indexCsc, string tokenCsc, int param)
        {
            return MainActivity.Printer.ImprimeXMLNFCe(xml, indexCsc, tokenCsc, param);
        }

        public int ImprimeXMLSAT(Dictionary<string, string> parametros)
        {
            return MainActivity.Printer.ImprimeXMLSAT(parametros);
        }

        public int StatusGaveta()
        {
            return MainActivity.Printer.StatusGaveta();
        }

        public int AbrirGaveta()
        {
            return MainActivity.Printer.AbrirGaveta();
        }

        public int StatusSensorPapel()
        {
            return MainActivity.Printer.StatusSensorPapel();
        }

        public int ImprimeCupomTEF(Dictionary<string, string> parametros)
        {
            return MainActivity.Printer.ImprimeCupomTEF(parametros);
        }

        public int ImprimeTexto(PrinterCommand options)
        {
            return MainActivity.Printer.ImprimeTexto(options);
        }

        public int ImprimeImagem(Bitmap bitmap)
        {
            return MainActivity.Printer.ImprimeImagem(bitmap);
        }

        public int ImprimeImagemPadrao()
        {
            return MainActivity.Printer.ImprimeImagemPadrao();
        }
    }
}