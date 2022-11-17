//using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDVDroidElgin.Hardware
{
    public interface IPrinter
    {

        int PrinterInternalImpStart();

        int PrinterExternalImpStart(Dictionary<string, string> parametros);

        void PrinterStop();

        int AvancaLinhas(int linesNumber);

        int CutPaper(int cut);

        int ImprimeBarCode(Dictionary<string, string> parametros);

        int ImprimeQR_CODE(Dictionary<string, string> parametros);

        //int ImprimeImagem(Bitmap bitmap);

        int ImprimeXMLNFCe(string xml, int indexCsc, string tokenCsc, int param);

        int ImprimeXMLSAT(Dictionary<string, string> parametros);

        int StatusGaveta();

        int AbrirGaveta();

        int StatusSensorPapel();

        int ImprimeCupomTEF(Dictionary<string, string> parametros);

        int ImprimeTexto(PrinterCommand options);

        int ImprimeImagemPadrao();
    }
}
