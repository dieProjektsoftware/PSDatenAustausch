namespace PSDatenAustausch;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        var resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        btn_Start = new Button();
        txt_Info = new TextBox();
        txt_Pfad = new TextBox();
        label1 = new Label();
        timer1 = new System.Windows.Forms.Timer(components);
        rB_1 = new RadioButton();
        labelInfo = new Label();
        label2 = new Label();
        SuspendLayout();
        // 
        // btn_Start
        // 
        btn_Start.BackColor = Color.Black;
        btn_Start.FlatStyle = FlatStyle.Flat;
        btn_Start.Font = new Font("Segoe UI", 11F);
        btn_Start.ForeColor = Color.FromArgb(224, 224, 224);
        btn_Start.Location = new Point(18, 7);
        btn_Start.Margin = new Padding(2);
        btn_Start.Name = "btn_Start";
        btn_Start.Size = new Size(104, 31);
        btn_Start.TabIndex = 0;
        btn_Start.Text = "START";
        btn_Start.UseVisualStyleBackColor = false;
        btn_Start.Click += btn_Start_Click;
        // 
        // txt_Info
        // 
        txt_Info.Location = new Point(18, 79);
        txt_Info.Margin = new Padding(2);
        txt_Info.Multiline = true;
        txt_Info.Name = "txt_Info";
        txt_Info.Size = new Size(407, 180);
        txt_Info.TabIndex = 1;
        // 
        // txt_Pfad
        // 
        txt_Pfad.Location = new Point(63, 45);
        txt_Pfad.Margin = new Padding(2);
        txt_Pfad.Name = "txt_Pfad";
        txt_Pfad.Size = new Size(362, 23);
        txt_Pfad.TabIndex = 3;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(25, 49);
        label1.Margin = new Padding(2, 0, 2, 0);
        label1.Name = "label1";
        label1.Size = new Size(31, 15);
        label1.TabIndex = 4;
        label1.Text = "Pfad";
        // 
        // timer1
        // 
        timer1.Tick += timer1_Tick;
        // 
        // rB_1
        // 
        rB_1.AutoSize = true;
        rB_1.Location = new Point(139, 7);
        rB_1.Margin = new Padding(2);
        rB_1.Name = "rB_1";
        rB_1.Size = new Size(41, 19);
        rB_1.TabIndex = 5;
        rB_1.TabStop = true;
        rB_1.Text = "OK";
        rB_1.UseVisualStyleBackColor = true;
        // 
        // labelInfo
        // 
        labelInfo.AutoSize = true;
        labelInfo.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        labelInfo.ForeColor = Color.Red;
        labelInfo.Location = new Point(204, 4);
        labelInfo.Name = "labelInfo";
        labelInfo.Size = new Size(127, 30);
        labelInfo.TabIndex = 6;
        labelInfo.Text = "Was bin Ich";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.BackColor = Color.FromArgb(0, 192, 192);
        label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
        label2.ForeColor = Color.Yellow;
        label2.Location = new Point(409, 7);
        label2.Name = "label2";
        label2.Size = new Size(16, 20);
        label2.TabIndex = 7;
        label2.Text = "?";
        label2.Click += label2_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(438, 270);
        Controls.Add(label2);
        Controls.Add(labelInfo);
        Controls.Add(rB_1);
        Controls.Add(label1);
        Controls.Add(txt_Pfad);
        Controls.Add(txt_Info);
        Controls.Add(btn_Start);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Margin = new Padding(2);
        Name = "Form1";
        Text = "PS Daten Senden";
        Load += Form1_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button btn_Start;
    private TextBox txt_Info;
    private TextBox txt_Pfad;
    private Label label1;
    private System.Windows.Forms.Timer timer1;
    private RadioButton rB_1;
    private Label labelInfo;
    private Label label2;
}
