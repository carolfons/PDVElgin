﻿using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Elgin.E1.Impressora;
using Java.IO;
using PDVDroidElgin.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PDVDroidElgin.Droid
{

    public class Printer
    {
        private Activity mActivity { get; set; }
        private Context mContext { get; set; }

        public Printer(Activity mainActivity)
        {
            //mActivity = activity;
            //Termica.SetContext(mActivity);

            SetActivity(mainActivity);
        }

        public void SetActivity(Activity activity)
        {
            this.mActivity = activity;
            this.mContext = activity;
            Termica.SetContext(activity);
        }
        public int PrinterInternalImpStart()
        {
            PrinterStop();
            int result = Termica.AbreConexaoImpressora(6, "M10", "", 0);
            return result;
        }

        public int PrinterExternalImpStart(Dictionary<string, string> dictionary)
        {
            PrinterStop();
            String ip = (String)dictionary["ip"];
            int port = Int32.Parse(dictionary["port"]);
            try
            {
                int result = Termica.AbreConexaoImpressora(3, "I9", ip, port);
                return result;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("exception: " + e);
                return PrinterInternalImpStart();
            }
        }

        public void PrinterStop()
        {
           Termica.FechaConexaoImpressora();
        }

        public int AvancaLinhas(int linesNumber)
        {
            return Termica.AvancaPapel(linesNumber);
        }

        public int CutPaper(int cut)
        {
            return Termica.Corte(cut);
        }


        private int CodeOfBarCode(String barCodeName)
        {
            if (barCodeName.Equals("UPC-A"))
                return 0;
            else if (barCodeName.Equals("UPC-E"))
                return 1;
            else if (barCodeName.Equals("EAN 13") || barCodeName.Equals("JAN 13"))
                return 2;

            else if (barCodeName.Equals("EAN 8") || barCodeName.Equals("JAN 8"))
                return 3;
            else if (barCodeName.Equals("CODE 39"))
                return 4;
            else if (barCodeName.Equals("ITF"))
                return 5;
            else if (barCodeName.Equals("CODE BAR"))
                return 6;
            else if (barCodeName.Equals("CODE 93"))
                return 7;
            else if (barCodeName.Equals("CODE 128"))
                return 8;
            else return 0;
        }

        public int ImprimeBarCode(Dictionary<string, string> dictionary)
        {

            int barCodeType = CodeOfBarCode((String)dictionary["barCodeType"]);
            String text = (String)dictionary["text"];
            int height = (int)Int32.Parse(dictionary["height"]);
            int width = (int)Int32.Parse(dictionary["width"]);
            String align = (String)dictionary["align"];

            int hri = 4; // NO PRINT
            int result;
            int alignValue;

            if (align.Equals("Esquerda"))
            {
                alignValue = 0;
            }
            else if (align.Equals("Centralizado"))
            {
                alignValue = 1;
            }
            else
            {
                alignValue = 2;
            }

            Termica.DefinePosicao(alignValue);

            return  Termica.ImpressaoCodigoBarras(barCodeType, text, height, width, hri);

        }

        public int ImprimeQR_CODE(Dictionary<string, string> dictionary)
        {
            int size = (int)Int32.Parse(dictionary["qrSize"]);
            String text = (String)dictionary["text"];
            String align = (String)dictionary["align"];

            int nivelCorrecao = 2;
            int result;
            int alignValue;

            if (align.Equals("Esquerda"))
            {
                alignValue = 0;
            }
            else if (align.Equals("Centralizado"))
            {
                alignValue = 1;
            }
            else
            {
                alignValue = 2;
            }

            Termica.DefinePosicao(alignValue);

            return Termica.ImpressaoQRCode(text, size, nivelCorrecao);

        }

        public int ImprimeImagem(Dictionary<string, string> dictionary)
        {
            String pathImage = (String)dictionary["pathImage"];
            Boolean isBase64 = (Boolean)Convert.ToBoolean(dictionary["isBase64"]);

            int result;

            File mSaveBit = new File(pathImage); // Your image file

            Bitmap bitmap;

            if (pathImage.Equals("elgin"))
            {
                int id = 0;

                // id = mContext.Resources.GetIdentifier(pathImage, "drawable", mContext.PackageName);
                id = mActivity.ApplicationContext.Resources.GetIdentifier(pathImage, "drawable", mActivity.ApplicationContext.PackageName);
                System.Console.WriteLine("id: " + id);

                bitmap = BitmapFactory.DecodeResource(mActivity.ApplicationContext.Resources, id);
                bitmap = BitmapFactory.DecodeResource(mActivity.ApplicationContext.Resources, id);
            }
            else
            {
                if (isBase64)
                {
                    byte[] decodedString = Base64.Decode(pathImage, Base64Flags.Default);
                    bitmap = BitmapFactory.DecodeByteArray(decodedString, 0, decodedString.Length);

                }
                else
                {
                    String filePath = mSaveBit.Path;
                    bitmap = BitmapFactory.DecodeFile(filePath);
                }
            }

            return  Termica.ImprimeBitmap(bitmap);

        }

        public int ImprimeXMLNFCe(string xml, int indexCsc, string tokenCsc, int param)
        {
            return Termica.ImprimeXMLNFCe(xml, indexCsc, tokenCsc, 1);
        }

        public int ImprimeXMLSAT(Dictionary<string, string> dictionary)
        {
            return 0;
            //String xml = (String) dictionary["xmlSAT"];
         //   String xml = this.xmlSat;
         //   int param = (int)Int32.Parse(dictionary["param"]);
           // return Termica.ImprimeXMLSAT(xml, param);
        }

        public int StatusGaveta()
        {
            return  Termica.StatusImpressora(1);
        }

        public int AbrirGaveta()
        {
            return  Termica.AbreGavetaElgin();
        }

        public int StatusSensorPapel()
        {
            return  Termica.StatusImpressora(3);
        }

        public int ImprimeCupomTEF(Dictionary<string, string> dictionary)
        {
            String base64 = (String)dictionary["base64"];

            return Termica.ImprimeCupomTEF(base64);
        }

        public int ImprimeTexto(PrinterCommand options)
        {
            String text = options.Text;
            String align = options.Alignment;
            String font = options.Font;
            int fontSize = options.FontSize;
            Boolean isBold = options.IsBold;
            Boolean isUnderline = options.IsUnderline;


            int result;

            int alignValue = 0;
            int styleValue = 0;

            // ALINHAMENTO VALUE
            if (align.Equals("Esquerda"))
            {
                alignValue = 0;
            }
            else if (align.Equals("Centralizado"))
            {
                alignValue = 1;
            }
            else
            {
                alignValue = 2;
            }
            //STILO VALUE
            if (font.Equals("FONT B"))
            {
                styleValue += 1;
            }
            if (isUnderline)
            {
                styleValue += 2;
            }
            if (isBold)
            {
                styleValue += 8;
            }

            return Termica.ImpressaoTexto(text, alignValue, styleValue, fontSize);
        }

        public int ImprimeImagemPadrao()
        {
            Bitmap bitmap;
            int id = 0;

            id = mActivity.ApplicationContext.Resources.GetIdentifier("elgin", "drawable", mActivity.ApplicationContext.PackageName);

            bitmap = BitmapFactory.DecodeResource(mActivity.ApplicationContext.Resources, id);
            bitmap = BitmapFactory.DecodeResource(mActivity.ApplicationContext.Resources, id);

            return  Termica.ImprimeBitmap(bitmap);
        }

        public int ImprimeImagem(Bitmap bitmap)
        {
            return  Termica.ImprimeBitmap(bitmap);
        }

        protected string CarregarArquivo(string nomeArquivo)
        {
            var stream = this.mContext.Resources.OpenRawResource(this.mContext.Resources.GetIdentifier(nomeArquivo, "raw", this.mContext.PackageName));
            string conteudoArquivo = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                conteudoArquivo = reader.ReadToEnd();
            }
            return conteudoArquivo;

        }

    }

}