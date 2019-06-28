using System;
using System.Reflection;
using System.Windows.Forms;

namespace SimilarImage.Services
{
    public class HackService
    {

        public void SetPropertyGridViewLabelColumnWidth(PropertyGrid grid, int width)
        {
            if (grid == null)
                throw new ArgumentNullException("grid");

            // get the grid view
            Control view = (Control)grid.GetType().GetField("gridView", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(grid);

            // set label width
            FieldInfo fi = view.GetType().GetField("labelWidth", BindingFlags.Instance | BindingFlags.NonPublic);  
            fi.SetValue(view, width);

            // refresh
            view.Invalidate();
        }


        public void SetDoubleBuffered(object obj, bool setting)
        {
            Type dgvType = obj.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(obj, setting, null);
        }
    }
}