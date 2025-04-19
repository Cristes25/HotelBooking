using System.Drawing;
using System.Windows.Forms;

public static class ThemeManager
{
    // Color Palette
    public static Color NavyBlue { get; } = Color.FromArgb(0, 40, 85);
    public static Color Orange { get; } = Color.FromArgb(255, 140, 0);
    public static Color Grey { get; } = Color.FromArgb(240, 240, 240);
    public static Color Brown { get; } = Color.FromArgb(90, 70, 50);
    public static Color White { get; } = Color.White;

    // Fonts
    public static Font HeaderFont { get; } = new Font("Segoe UI", 14, FontStyle.Bold);
    public static Font NormalFont { get; } = new Font("Segoe UI", 10);

    public static void Apply(Form form)
    {
        form.BackColor = Grey;
        form.Font = NormalFont;

        foreach (var control in GetAllControls(form))
        {
            switch (control)
            {
                case Button btn:
                    StyleButton(btn);
                    break;

                case Panel panel:
                    panel.BackColor = White;
                    break;

                case Label lbl when lbl.Tag?.ToString() == "header":
                    lbl.Font = HeaderFont;
                    lbl.ForeColor = NavyBlue;
                    break;

                case TextBox txt:
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.BackColor = White;
                    break;
            }
        }
    }

    private static void StyleButton(Button btn)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.BackColor = Orange;
        btn.ForeColor = White;
        btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        btn.Cursor = Cursors.Hand;

        // Hover effects
        btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(255, 165, 0);
        btn.MouseLeave += (s, e) => btn.BackColor = Orange;
    }

    private static IEnumerable<Control> GetAllControls(Control control)
    {
        var controls = control.Controls.Cast<Control>();
        return controls.SelectMany(ctrl => GetAllControls(ctrl)).Concat(controls);
    }
}