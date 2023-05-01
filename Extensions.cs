using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRM_Api_Viewer
{
    public static class Extensions
    {
        public static void AddItem(this ComboBox comboBox, object item)
        {
            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(new Action(() =>
                {
                    comboBox.Items.Add(item);
                }));
            }
            else
            {
                comboBox.Items.Add(item);
            }
        }
        public static void AddItem(this ListBox listBox, object item)
        {
            if (listBox.InvokeRequired)
            {
                listBox.Invoke(new Action(() =>
                {
                    listBox.Items.Add(item);
                }));
            }
            else
            {
                listBox.Items.Add(item);
            }
        }
        public static void AppendTexts(this TextBox textBox, string text)
        {
            try
            {
                if (textBox.InvokeRequired)
                {
                    textBox.Invoke(new Action(() =>
                    {
                        textBox.AppendText(text);
                    }));
                }
                else
                {
                    textBox.AppendText(text);
                }
            }
            catch { }
        }
        public static void SetProperty(this Control control, string propertyName, object value)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() =>
                {
                    var property = control.GetType().GetProperty(propertyName);
                    if (property.PropertyType.IsEnum)
                    {
                        property.SetValue(control, Enum.Parse(property.PropertyType, value.ToString()));
                    }
                    else
                    {
                        TypeConverter converter = TypeDescriptor.GetConverter(property.PropertyType);
                        property.SetValue(control, converter.ConvertFromString(value.ToString()));
                    }
                }));
            }
            else
            {
                var property = control.GetType().GetProperty(propertyName);
                if (property.PropertyType.IsEnum)
                {
                    property.SetValue(control, Enum.Parse(property.PropertyType, value.ToString()));
                }
                else
                {
                    TypeConverter converter = TypeDescriptor.GetConverter(property.PropertyType);
                    property.SetValue(control, converter.ConvertFromString(value.ToString()));
                }
            }
        }
    }
}

