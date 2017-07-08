using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public class URotator : IXmlSerializable, IUnrealExportable
    {
        [UEExport]
        public int Pitch { set; get; }
        [UEExport]
        public int Yaw { set; get; }
        [UEExport]
        public int Roll { set; get; }

        [UEExport]
        public float UE4_Pitch
        {
            get
            {
                return Pitch / 65536.0f * 360.0f;
            }
        }

        public static bool operator ==(URotator left, URotator right)
        {
            return ((object)left != null && (object)right != null) &&
                left.Pitch == right.Pitch &&
                left.Yaw == right.Yaw &&
                left.Roll == right.Roll;
        }
        public static bool operator !=(URotator left, URotator right)
        {
            return !(left == right);
        }
        [UEExport]
        public float UE4_Yaw
        {
            get
            {
                return Yaw / 65536.0f * 360.0f;
            }
        }

        [UEExport]
        public float UE4_Roll
        {
            get
            {
                return Roll / 65536.0f * 360.0f;
            }
        }

        public void Deserialize(XElement element)
        {
            if (element.Attribute("class").Value != "Rotator")
                throw new Exception("Wrong class.");
            Pitch = Convert.ToInt32(Utility.GetElement(element, "pitch").Value.ToString(), NumberFormatInfo.InvariantInfo);
            Yaw = Convert.ToInt32(Utility.GetElement(element, "yaw").Value.ToString(), NumberFormatInfo.InvariantInfo);
            Roll = Convert.ToInt32(Utility.GetElement(element, "roll").Value.ToString(), NumberFormatInfo.InvariantInfo);
        }

        public XElement SerializeXML(string Name)
        {
            return new XElement(Name,
                 new XElement("pitch", Pitch.ToString(NumberFormatInfo.InvariantInfo)),
                 new XElement("yaw", Yaw.ToString(NumberFormatInfo.InvariantInfo)),
                 new XElement("roll", Roll.ToString(NumberFormatInfo.InvariantInfo)),
                 new XAttribute("class", "Rotator"));
        }

        public string UnrealString
        {
            get
            {
                return Utility.GetExport(this);
            }
        }

        public void ResetTemplate()
        {
            URotator.Template = null;
        }

        private static string Template = null;


        public string UnrealStringTemplate
        {
            set
            {
                Template = value;
                Utility.SetTemplate(this.GetType().Name, Template);
            }
            get
            {
                if (!string.IsNullOrEmpty(Template)) return Template;
                Template = Utility.GetTemplate(this.GetType().Name);
                return Template;
            }
        }

        public string[] PropertiesList
        {
            get
            {
                return Utility.GetExportProperties(this.GetType());
            }
        }
    }
}
