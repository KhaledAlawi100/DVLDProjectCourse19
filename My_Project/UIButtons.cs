using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public static class UIButtons
{
    public static void ApplyPrimaryStyle(KryptonButton btn)
    {
        // ===== Base =====
        btn.StateCommon.Back.Color1 = Color.FromArgb(37, 99, 235);
        btn.StateCommon.Back.Color2 = Color.FromArgb(29, 78, 216);
        btn.StateCommon.Back.ColorAngle = 45f;

        btn.StateCommon.Border.Color1 = Color.FromArgb(37, 99, 235);
        btn.StateCommon.Border.Color2 = Color.FromArgb(29, 78, 216);
        btn.StateCommon.Border.DrawBorders = PaletteDrawBorders.All;
        btn.StateCommon.Border.Rounding = 12;
        btn.StateCommon.Border.Width = 1;

        // ===== Text =====
        btn.StateCommon.Content.ShortText.Color1 = Color.White;
        btn.StateCommon.Content.ShortText.Font =
            new Font("Segoe UI", 10, FontStyle.Bold);

        btn.StateCommon.Content.Padding =
            new Padding(10, 6, 10, 6);

        // ===== Hover (Mouse Over) =====
        btn.StateTracking.Back.Color1 = Color.FromArgb(59, 130, 246);
        btn.StateTracking.Back.Color2 = Color.FromArgb(37, 99, 235);

        btn.StateTracking.Border.Color1 = Color.FromArgb(59, 130, 246);
        btn.StateTracking.Border.Color2 = Color.FromArgb(37, 99, 235);

        // ===== Pressed (Click) =====
        btn.StatePressed.Back.Color1 = Color.FromArgb(29, 78, 216);
        btn.StatePressed.Back.Color2 = Color.FromArgb(30, 64, 175);

        btn.StatePressed.Border.Color1 = Color.FromArgb(29, 78, 216);
        btn.StatePressed.Border.Color2 = Color.FromArgb(30, 64, 175);

        // ===== Cursor =====
        btn.Cursor = System.Windows.Forms.Cursors.Hand;
    }
}
