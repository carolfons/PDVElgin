using System;
using System.Collections.Generic;
using System.Text;

namespace PDVDroidElgin.Hardware
{
    public enum ElginFontType
    {
        FontA = 0,
        FontB = 1
    }

    public enum ElginFontSize
    {
        s10 = 10,
        s11 = 11,
        s12 = 12,
        s17 = 17,
        s34 = 34,
        s51 = 51,
        s68 = 68
    }

    public enum ElginFontAlign
    {
        Esquerda = 0,
        Centralizado = 1,
        Direita = 2
    }

    public class PrinterCommand
    {
        public string Text { get; private set; }
        public string Alignment { get; private set; }
        public string Font { get; private set; }
        public int FontSize { get; private set; }
        public bool IsUnderline { get; internal set; }
        public bool IsBold { get; internal set; }

        public PrinterCommand(string text,
            ElginFontType font = ElginFontType.FontA,
            ElginFontSize size = ElginFontSize.s11,
            ElginFontAlign align = ElginFontAlign.Esquerda,
            bool isBold = false,
            bool isUnderline = false)
        {
            Text = text;
            Font = (font == ElginFontType.FontA ? "FONT A" : "FONT B");
            FontSize = (int)size;
            IsUnderline = isUnderline;
            IsBold = isBold;

            if (align == ElginFontAlign.Esquerda) Alignment = "Esquerda";
            if (align == ElginFontAlign.Direita) Alignment = "Direita";
            if (align == ElginFontAlign.Centralizado) Alignment = "Centralizado";
        }
    }
}
