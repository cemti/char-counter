namespace CharCounter;

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
        menuStrip1 = new MenuStrip();
        fileToolStripMenuItem = new ToolStripMenuItem();
        openToolStripMenuItem = new ToolStripMenuItem();
        refreshToolStripMenuItem = new ToolStripMenuItem();
        listView1 = new ListView();
        charHeader = new ColumnHeader();
        hexHeader = new ColumnHeader();
        countHeader = new ColumnHeader();
        fileNameHeader = new ColumnHeader();
        menuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(800, 24);
        menuStrip1.TabIndex = 0;
        menuStrip1.Text = "menuStrip1";
        // 
        // fileToolStripMenuItem
        // 
        fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, refreshToolStripMenuItem });
        fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        fileToolStripMenuItem.Size = new Size(37, 20);
        fileToolStripMenuItem.Text = "File";
        // 
        // openToolStripMenuItem
        // 
        openToolStripMenuItem.Name = "openToolStripMenuItem";
        openToolStripMenuItem.Size = new Size(113, 22);
        openToolStripMenuItem.Text = "Open";
        openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
        // 
        // refreshToolStripMenuItem
        // 
        refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
        refreshToolStripMenuItem.Size = new Size(113, 22);
        refreshToolStripMenuItem.Text = "Refresh";
        refreshToolStripMenuItem.Click += RefreshToolStripMenuItem_Click;
        // 
        // listView1
        // 
        listView1.Columns.AddRange(new ColumnHeader[] { charHeader, hexHeader, countHeader, fileNameHeader });
        listView1.Dock = DockStyle.Fill;
        listView1.FullRowSelect = true;
        listView1.GridLines = true;
        listView1.Location = new Point(0, 24);
        listView1.MultiSelect = false;
        listView1.Name = "listView1";
        listView1.Size = new Size(800, 426);
        listView1.Sorting = SortOrder.Ascending;
        listView1.TabIndex = 1;
        listView1.UseCompatibleStateImageBehavior = false;
        listView1.View = View.Details;
        listView1.ColumnClick += ListView1_ColumnClick;
        listView1.MouseDoubleClick += ListView1_MouseDoubleClick;
        // 
        // charHeader
        // 
        charHeader.Text = "Character";
        charHeader.Width = 63;
        // 
        // hexHeader
        // 
        hexHeader.Text = "Hex value";
        hexHeader.Width = 63;
        // 
        // countHeader
        // 
        countHeader.Text = "Count";
        countHeader.Width = 45;
        // 
        // fileNameHeader
        // 
        fileNameHeader.Text = "Files";
        fileNameHeader.Width = 625;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(listView1);
        Controls.Add(menuStrip1);
        MainMenuStrip = menuStrip1;
        Name = "Form1";
        Text = "Character Counter";
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem openToolStripMenuItem;
    private ToolStripMenuItem refreshToolStripMenuItem;
    private ListView listView1;
    private ColumnHeader charHeader;
    private ColumnHeader hexHeader;
    private ColumnHeader countHeader;
    private ColumnHeader fileNameHeader;
}
