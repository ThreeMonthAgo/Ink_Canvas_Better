using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Ink_Canvas_Better.Helpers.Converter;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.Services;
using ColorConverter = Ink_Canvas_Better.Helpers.Converter.ColorConverter;

namespace Ink_Canvas_Better.Controls.FloatingBar.SubPanel
{
    public partial class PenSubpanel : UserControl, IFloatingBarComponentSettingBase
    {
        public static string Guid { get; } = "0683F0B3-9EE7-4E0A-A645-66B157239A03";
        public string ComponentGuid => Guid;
        public object Settings { get; set; } = new PenSubpanelSettings();
        public PenSubpanelSettings PenSubpanelSettings => Settings as PenSubpanelSettings;

        public PenSubpanel()
        {
            InitializeComponent();
            DataContext = PenSubpanelSettings;
        }
    }

    public class PenSubpanelSettings
    {
        public SolidColorBrush Color0  { get; set; } = ColorConverter.HexToSolidColorBrush("#FFFFFF");
        public SolidColorBrush Color1  { get; set; } = ColorConverter.HexToSolidColorBrush("#000000");
        public SolidColorBrush Color2  { get; set; } = ColorConverter.HexToSolidColorBrush("#A72C1D");
        public SolidColorBrush Color3  { get; set; } = ColorConverter.HexToSolidColorBrush("#E03B27");
        public SolidColorBrush Color4  { get; set; } = ColorConverter.HexToSolidColorBrush("#EFC046");
        public SolidColorBrush Color5  { get; set; } = ColorConverter.HexToSolidColorBrush("#FCFC58");
        public SolidColorBrush Color6  { get; set; } = ColorConverter.HexToSolidColorBrush("#A0CB64");
        public SolidColorBrush Color7  { get; set; } = ColorConverter.HexToSolidColorBrush("#59AA5C");
        public SolidColorBrush Color8  { get; set; } = ColorConverter.HexToSolidColorBrush("#61ADE9");
        public SolidColorBrush Color9  { get; set; } = ColorConverter.HexToSolidColorBrush("#4170B8");
        public SolidColorBrush Color10 { get; set; } = ColorConverter.HexToSolidColorBrush("#19275C");
        public SolidColorBrush Color11 { get; set; } = ColorConverter.HexToSolidColorBrush("#673C98");

    }
}
