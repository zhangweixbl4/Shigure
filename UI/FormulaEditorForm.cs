using System.Drawing;

namespace Shigure;

public sealed class FormulaEditorForm : Form
{
    private readonly TextBox _formulaBox = new();

    public string FormulaText { get; private set; } = string.Empty;

    public FormulaEditorForm(string? formula)
    {
        FormulaText = FormulaEvaluator.NormalizeExpression(formula);
        InitializeComponent();
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        UiTheme.ApplyDarkTitleBar(this);
    }

    private void InitializeComponent()
    {
        Text = "编辑公式";
        Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        BackColor = UiTheme.Background;
        ForeColor = UiTheme.Text;
        ClientSize = new Size(760, 260);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;

        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            BackColor = UiTheme.Background,
            Padding = new Padding(12),
            ColumnCount = 1,
            RowCount = 2
        };
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
        Controls.Add(root);

        _formulaBox.Multiline = true;
        _formulaBox.ScrollBars = ScrollBars.Vertical;
        _formulaBox.Text = FormulaText;
        _formulaBox.Dock = DockStyle.Fill;
        _formulaBox.Margin = new Padding(0, 0, 0, 8);
        UiTheme.StyleTextBox(_formulaBox);
        root.Controls.Add(_formulaBox, 0, 0);

        root.Controls.Add(BuildActionRow(), 0, 1);
    }

    private Control BuildActionRow()
    {
        var row = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft,
            WrapContents = false,
            BackColor = UiTheme.Background,
            Margin = new Padding(0)
        };

        var okButton = UiTheme.CreateButton("确定", UiTheme.Accent, Color.Black);
        okButton.Width = 72;
        okButton.Height = 30;
        okButton.Margin = new Padding(6, 6, 0, 0);
        okButton.Click += (_, _) =>
        {
            FormulaText = _formulaBox.Text.Trim();
            DialogResult = DialogResult.OK;
        };

        var cancelButton = UiTheme.CreateButton("取消", UiTheme.Field, UiTheme.Text);
        cancelButton.Width = 72;
        cancelButton.Height = 30;
        cancelButton.Margin = new Padding(6, 6, 0, 0);
        cancelButton.Click += (_, _) => DialogResult = DialogResult.Cancel;

        row.Controls.Add(okButton);
        row.Controls.Add(cancelButton);
        AcceptButton = okButton;
        CancelButton = cancelButton;
        return row;
    }
}
