namespace WinFormsApp_MCP;

partial class SettingsForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.groupBoxProvider = new GroupBox();
        this.radioButtonOpenRouter = new RadioButton();
        this.radioButtonOpenAI = new RadioButton();
        this.groupBoxOpenAI = new GroupBox();
        this.labelOpenAIModel = new Label();
        this.comboBoxOpenAIModel = new ComboBox();
        this.labelOpenAIKey = new Label();
        this.textBoxOpenAIKey = new TextBox();
        this.groupBoxOpenRouter = new GroupBox();
        this.labelOpenRouterModel = new Label();
        this.comboBoxOpenRouterModel = new ComboBox();
        this.labelOpenRouterKey = new Label();
        this.textBoxOpenRouterKey = new TextBox();
        this.buttonSave = new Button();
        this.buttonCancel = new Button();
        this.tabControl1 = new TabControl();
        this.tabPageLLM = new TabPage();
        this.tabPageMCP = new TabPage();
        this.groupBoxFilesystem = new GroupBox();
        this.textBoxFilesystemDirectories = new TextBox();
        this.labelFilesystemDirectories = new Label();
        this.groupBoxGitLab = new GroupBox();
        this.textBoxGitLabApiUrl = new TextBox();
        this.labelGitLabApiUrl = new Label();
        this.labelGitLabToken = new Label();
        this.textBoxGitLabToken = new TextBox();
        this.groupBoxProvider.SuspendLayout();
        this.groupBoxOpenAI.SuspendLayout();
        this.groupBoxOpenRouter.SuspendLayout();
        this.tabControl1.SuspendLayout();
        this.tabPageLLM.SuspendLayout();
        this.tabPageMCP.SuspendLayout();
        this.groupBoxFilesystem.SuspendLayout();
        this.groupBoxGitLab.SuspendLayout();
        this.SuspendLayout();
        // 
        // groupBoxProvider
        // 
        this.groupBoxProvider.Controls.Add(this.radioButtonOpenRouter);
        this.groupBoxProvider.Controls.Add(this.radioButtonOpenAI);
        this.groupBoxProvider.Location = new Point(17, 20);
        this.groupBoxProvider.Margin = new Padding(4, 5, 4, 5);
        this.groupBoxProvider.Name = "groupBoxProvider";
        this.groupBoxProvider.Padding = new Padding(4, 5, 4, 5);
        this.groupBoxProvider.Size = new Size(657, 100);
        this.groupBoxProvider.TabIndex = 0;
        this.groupBoxProvider.TabStop = false;
        this.groupBoxProvider.Text = "LLM Provider";
        // 
        // radioButtonOpenRouter
        // 
        this.radioButtonOpenRouter.AutoSize = true;
        this.radioButtonOpenRouter.Location = new Point(343, 42);
        this.radioButtonOpenRouter.Margin = new Padding(4, 5, 4, 5);
        this.radioButtonOpenRouter.Name = "radioButtonOpenRouter";
        this.radioButtonOpenRouter.Size = new Size(133, 29);
        this.radioButtonOpenRouter.TabIndex = 1;
        this.radioButtonOpenRouter.Text = "OpenRouter";
        this.radioButtonOpenRouter.UseVisualStyleBackColor = true;
        this.radioButtonOpenRouter.CheckedChanged += this.radioButtonOpenRouter_CheckedChanged;
        // 
        // radioButtonOpenAI
        // 
        this.radioButtonOpenAI.AutoSize = true;
        this.radioButtonOpenAI.Checked = true;
        this.radioButtonOpenAI.Location = new Point(29, 42);
        this.radioButtonOpenAI.Margin = new Padding(4, 5, 4, 5);
        this.radioButtonOpenAI.Name = "radioButtonOpenAI";
        this.radioButtonOpenAI.Size = new Size(180, 29);
        this.radioButtonOpenAI.TabIndex = 0;
        this.radioButtonOpenAI.TabStop = true;
        this.radioButtonOpenAI.Text = "OpenAI (ChatGPT)";
        this.radioButtonOpenAI.UseVisualStyleBackColor = true;
        this.radioButtonOpenAI.CheckedChanged += this.radioButtonOpenAI_CheckedChanged;
        // 
        // groupBoxOpenAI
        // 
        this.groupBoxOpenAI.Controls.Add(this.labelOpenAIModel);
        this.groupBoxOpenAI.Controls.Add(this.comboBoxOpenAIModel);
        this.groupBoxOpenAI.Controls.Add(this.labelOpenAIKey);
        this.groupBoxOpenAI.Controls.Add(this.textBoxOpenAIKey);
        this.groupBoxOpenAI.Location = new Point(17, 130);
        this.groupBoxOpenAI.Margin = new Padding(4, 5, 4, 5);
        this.groupBoxOpenAI.Name = "groupBoxOpenAI";
        this.groupBoxOpenAI.Padding = new Padding(4, 5, 4, 5);
        this.groupBoxOpenAI.Size = new Size(657, 183);
        this.groupBoxOpenAI.TabIndex = 1;
        this.groupBoxOpenAI.TabStop = false;
        this.groupBoxOpenAI.Text = "OpenAI Settings";
        // 
        // labelOpenAIModel
        // 
        this.labelOpenAIModel.AutoSize = true;
        this.labelOpenAIModel.Location = new Point(29, 117);
        this.labelOpenAIModel.Margin = new Padding(4, 0, 4, 0);
        this.labelOpenAIModel.Name = "labelOpenAIModel";
        this.labelOpenAIModel.Size = new Size(67, 25);
        this.labelOpenAIModel.TabIndex = 3;
        this.labelOpenAIModel.Text = "Model:";
        // 
        // comboBoxOpenAIModel
        // 
        this.comboBoxOpenAIModel.DropDownStyle = ComboBoxStyle.DropDownList;
        this.comboBoxOpenAIModel.FormattingEnabled = true;
        this.comboBoxOpenAIModel.Location = new Point(171, 112);
        this.comboBoxOpenAIModel.Margin = new Padding(4, 5, 4, 5);
        this.comboBoxOpenAIModel.Name = "comboBoxOpenAIModel";
        this.comboBoxOpenAIModel.Size = new Size(455, 33);
        this.comboBoxOpenAIModel.TabIndex = 2;
        // 
        // labelOpenAIKey
        // 
        this.labelOpenAIKey.AutoSize = true;
        this.labelOpenAIKey.Location = new Point(29, 50);
        this.labelOpenAIKey.Margin = new Padding(4, 0, 4, 0);
        this.labelOpenAIKey.Name = "labelOpenAIKey";
        this.labelOpenAIKey.Size = new Size(76, 25);
        this.labelOpenAIKey.TabIndex = 1;
        this.labelOpenAIKey.Text = "API Key:";
        // 
        // textBoxOpenAIKey
        // 
        this.textBoxOpenAIKey.Location = new Point(171, 45);
        this.textBoxOpenAIKey.Margin = new Padding(4, 5, 4, 5);
        this.textBoxOpenAIKey.Name = "textBoxOpenAIKey";
        this.textBoxOpenAIKey.PasswordChar = '•';
        this.textBoxOpenAIKey.Size = new Size(455, 31);
        this.textBoxOpenAIKey.TabIndex = 0;
        // 
        // groupBoxOpenRouter
        // 
        this.groupBoxOpenRouter.Controls.Add(this.labelOpenRouterModel);
        this.groupBoxOpenRouter.Controls.Add(this.comboBoxOpenRouterModel);
        this.groupBoxOpenRouter.Controls.Add(this.labelOpenRouterKey);
        this.groupBoxOpenRouter.Controls.Add(this.textBoxOpenRouterKey);
        this.groupBoxOpenRouter.Location = new Point(17, 330);
        this.groupBoxOpenRouter.Margin = new Padding(4, 5, 4, 5);
        this.groupBoxOpenRouter.Name = "groupBoxOpenRouter";
        this.groupBoxOpenRouter.Padding = new Padding(4, 5, 4, 5);
        this.groupBoxOpenRouter.Size = new Size(657, 183);
        this.groupBoxOpenRouter.TabIndex = 2;
        this.groupBoxOpenRouter.TabStop = false;
        this.groupBoxOpenRouter.Text = "OpenRouter Settings";
        // 
        // labelOpenRouterModel
        // 
        this.labelOpenRouterModel.AutoSize = true;
        this.labelOpenRouterModel.Location = new Point(29, 117);
        this.labelOpenRouterModel.Margin = new Padding(4, 0, 4, 0);
        this.labelOpenRouterModel.Name = "labelOpenRouterModel";
        this.labelOpenRouterModel.Size = new Size(67, 25);
        this.labelOpenRouterModel.TabIndex = 3;
        this.labelOpenRouterModel.Text = "Model:";
        // 
        // comboBoxOpenRouterModel
        // 
        this.comboBoxOpenRouterModel.DropDownStyle = ComboBoxStyle.DropDownList;
        this.comboBoxOpenRouterModel.FormattingEnabled = true;
        this.comboBoxOpenRouterModel.Location = new Point(171, 112);
        this.comboBoxOpenRouterModel.Margin = new Padding(4, 5, 4, 5);
        this.comboBoxOpenRouterModel.Name = "comboBoxOpenRouterModel";
        this.comboBoxOpenRouterModel.Size = new Size(455, 33);
        this.comboBoxOpenRouterModel.TabIndex = 2;
        // 
        // labelOpenRouterKey
        // 
        this.labelOpenRouterKey.AutoSize = true;
        this.labelOpenRouterKey.Location = new Point(29, 50);
        this.labelOpenRouterKey.Margin = new Padding(4, 0, 4, 0);
        this.labelOpenRouterKey.Name = "labelOpenRouterKey";
        this.labelOpenRouterKey.Size = new Size(76, 25);
        this.labelOpenRouterKey.TabIndex = 1;
        this.labelOpenRouterKey.Text = "API Key:";
        // 
        // textBoxOpenRouterKey
        // 
        this.textBoxOpenRouterKey.Location = new Point(171, 45);
        this.textBoxOpenRouterKey.Margin = new Padding(4, 5, 4, 5);
        this.textBoxOpenRouterKey.Name = "textBoxOpenRouterKey";
        this.textBoxOpenRouterKey.PasswordChar = '•';
        this.textBoxOpenRouterKey.Size = new Size(455, 31);
        this.textBoxOpenRouterKey.TabIndex = 0;
        // 
        // buttonSave
        // 
        this.buttonSave.Location = new Point(718, 663);
        this.buttonSave.Margin = new Padding(4, 5, 4, 5);
        this.buttonSave.Name = "buttonSave";
        this.buttonSave.Size = new Size(124, 43);
        this.buttonSave.TabIndex = 3;
        this.buttonSave.Text = "Save";
        this.buttonSave.UseVisualStyleBackColor = true;
        this.buttonSave.Click += this.buttonSave_Click;
        // 
        // buttonCancel
        // 
        this.buttonCancel.Location = new Point(571, 663);
        this.buttonCancel.Margin = new Padding(4, 5, 4, 5);
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Size = new Size(124, 43);
        this.buttonCancel.TabIndex = 4;
        this.buttonCancel.Text = "Cancel";
        this.buttonCancel.UseVisualStyleBackColor = true;
        this.buttonCancel.Click += this.buttonCancel_Click;
        // 
        // tabControl1
        // 
        this.tabControl1.Controls.Add(this.tabPageLLM);
        this.tabControl1.Controls.Add(this.tabPageMCP);
        this.tabControl1.Location = new Point(17, 20);
        this.tabControl1.Margin = new Padding(4, 5, 4, 5);
        this.tabControl1.Name = "tabControl1";
        this.tabControl1.SelectedIndex = 0;
        this.tabControl1.Size = new Size(829, 633);
        this.tabControl1.TabIndex = 5;
        // 
        // tabPageLLM
        // 
        this.tabPageLLM.Controls.Add(this.groupBoxProvider);
        this.tabPageLLM.Controls.Add(this.groupBoxOpenAI);
        this.tabPageLLM.Controls.Add(this.groupBoxOpenRouter);
        this.tabPageLLM.Location = new Point(4, 34);
        this.tabPageLLM.Margin = new Padding(4, 5, 4, 5);
        this.tabPageLLM.Name = "tabPageLLM";
        this.tabPageLLM.Padding = new Padding(4, 5, 4, 5);
        this.tabPageLLM.Size = new Size(821, 595);
        this.tabPageLLM.TabIndex = 0;
        this.tabPageLLM.Text = "LLM Settings";
        this.tabPageLLM.UseVisualStyleBackColor = true;
        // 
        // tabPageMCP
        // 
        this.tabPageMCP.Controls.Add(this.groupBoxFilesystem);
        this.tabPageMCP.Controls.Add(this.groupBoxGitLab);
        this.tabPageMCP.Location = new Point(4, 34);
        this.tabPageMCP.Margin = new Padding(4, 5, 4, 5);
        this.tabPageMCP.Name = "tabPageMCP";
        this.tabPageMCP.Padding = new Padding(4, 5, 4, 5);
        this.tabPageMCP.Size = new Size(821, 595);
        this.tabPageMCP.TabIndex = 1;
        this.tabPageMCP.Text = "MCP Settings";
        this.tabPageMCP.UseVisualStyleBackColor = true;
        // 
        // groupBoxFilesystem
        // 
        this.groupBoxFilesystem.Controls.Add(this.textBoxFilesystemDirectories);
        this.groupBoxFilesystem.Controls.Add(this.labelFilesystemDirectories);
        this.groupBoxFilesystem.Location = new Point(9, 187);
        this.groupBoxFilesystem.Margin = new Padding(4, 5, 4, 5);
        this.groupBoxFilesystem.Name = "groupBoxFilesystem";
        this.groupBoxFilesystem.Padding = new Padding(4, 5, 4, 5);
        this.groupBoxFilesystem.Size = new Size(800, 390);
        this.groupBoxFilesystem.TabIndex = 4;
        this.groupBoxFilesystem.TabStop = false;
        this.groupBoxFilesystem.Text = "Filesystem Settings";
        // 
        // textBoxFilesystemDirectories
        // 
        this.textBoxFilesystemDirectories.Location = new Point(23, 78);
        this.textBoxFilesystemDirectories.Margin = new Padding(4, 5, 4, 5);
        this.textBoxFilesystemDirectories.Multiline = true;
        this.textBoxFilesystemDirectories.Name = "textBoxFilesystemDirectories";
        this.textBoxFilesystemDirectories.Size = new Size(767, 299);
        this.textBoxFilesystemDirectories.TabIndex = 1;
        // 
        // labelFilesystemDirectories
        // 
        this.labelFilesystemDirectories.AutoSize = true;
        this.labelFilesystemDirectories.Location = new Point(23, 48);
        this.labelFilesystemDirectories.Margin = new Padding(4, 0, 4, 0);
        this.labelFilesystemDirectories.Name = "labelFilesystemDirectories";
        this.labelFilesystemDirectories.Size = new Size(294, 25);
        this.labelFilesystemDirectories.TabIndex = 0;
        this.labelFilesystemDirectories.Text = "Accessible Directories (one per line):";
        // 
        // groupBoxGitLab
        // 
        this.groupBoxGitLab.Controls.Add(this.textBoxGitLabApiUrl);
        this.groupBoxGitLab.Controls.Add(this.labelGitLabApiUrl);
        this.groupBoxGitLab.Controls.Add(this.labelGitLabToken);
        this.groupBoxGitLab.Controls.Add(this.textBoxGitLabToken);
        this.groupBoxGitLab.Location = new Point(9, 10);
        this.groupBoxGitLab.Margin = new Padding(4, 5, 4, 5);
        this.groupBoxGitLab.Name = "groupBoxGitLab";
        this.groupBoxGitLab.Padding = new Padding(4, 5, 4, 5);
        this.groupBoxGitLab.Size = new Size(800, 167);
        this.groupBoxGitLab.TabIndex = 3;
        this.groupBoxGitLab.TabStop = false;
        this.groupBoxGitLab.Text = "GitLab Settings";
        // 
        // textBoxGitLabApiUrl
        // 
        this.textBoxGitLabApiUrl.Location = new Point(171, 100);
        this.textBoxGitLabApiUrl.Margin = new Padding(4, 5, 4, 5);
        this.textBoxGitLabApiUrl.Name = "textBoxGitLabApiUrl";
        this.textBoxGitLabApiUrl.Size = new Size(618, 31);
        this.textBoxGitLabApiUrl.TabIndex = 3;
        // 
        // labelGitLabApiUrl
        // 
        this.labelGitLabApiUrl.AutoSize = true;
        this.labelGitLabApiUrl.Location = new Point(23, 105);
        this.labelGitLabApiUrl.Margin = new Padding(4, 0, 4, 0);
        this.labelGitLabApiUrl.Name = "labelGitLabApiUrl";
        this.labelGitLabApiUrl.Size = new Size(79, 25);
        this.labelGitLabApiUrl.TabIndex = 2;
        this.labelGitLabApiUrl.Text = "API URL:";
        // 
        // labelGitLabToken
        // 
        this.labelGitLabToken.AutoSize = true;
        this.labelGitLabToken.Location = new Point(23, 50);
        this.labelGitLabToken.Margin = new Padding(4, 0, 4, 0);
        this.labelGitLabToken.Name = "labelGitLabToken";
        this.labelGitLabToken.Size = new Size(133, 25);
        this.labelGitLabToken.TabIndex = 1;
        this.labelGitLabToken.Text = "Personal Token:";
        // 
        // textBoxGitLabToken
        // 
        this.textBoxGitLabToken.Location = new Point(171, 45);
        this.textBoxGitLabToken.Margin = new Padding(4, 5, 4, 5);
        this.textBoxGitLabToken.Name = "textBoxGitLabToken";
        this.textBoxGitLabToken.PasswordChar = '•';
        this.textBoxGitLabToken.Size = new Size(618, 31);
        this.textBoxGitLabToken.TabIndex = 0;
        // 
        // SettingsForm
        // 
        this.AutoScaleDimensions = new SizeF(10F, 25F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(863, 735);
        this.Controls.Add(this.buttonSave);
        this.Controls.Add(this.buttonCancel);
        this.Controls.Add(this.tabControl1);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.Margin = new Padding(4, 5, 4, 5);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "SettingsForm";
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "LLM Settings";
        this.Load += this.SettingsForm_Load;
        this.groupBoxProvider.ResumeLayout(false);
        this.groupBoxProvider.PerformLayout();
        this.groupBoxOpenAI.ResumeLayout(false);
        this.groupBoxOpenAI.PerformLayout();
        this.groupBoxOpenRouter.ResumeLayout(false);
        this.groupBoxOpenRouter.PerformLayout();
        this.tabControl1.ResumeLayout(false);
        this.tabPageLLM.ResumeLayout(false);
        this.tabPageMCP.ResumeLayout(false);
        this.groupBoxFilesystem.ResumeLayout(false);
        this.groupBoxFilesystem.PerformLayout();
        this.groupBoxGitLab.ResumeLayout(false);
        this.groupBoxGitLab.PerformLayout();
        this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.GroupBox groupBoxProvider;
    private System.Windows.Forms.RadioButton radioButtonOpenRouter;
    private System.Windows.Forms.RadioButton radioButtonOpenAI;
    private System.Windows.Forms.GroupBox groupBoxOpenAI;
    private System.Windows.Forms.Label labelOpenAIModel;
    private System.Windows.Forms.ComboBox comboBoxOpenAIModel;
    private System.Windows.Forms.Label labelOpenAIKey;
    private System.Windows.Forms.TextBox textBoxOpenAIKey;
    private System.Windows.Forms.GroupBox groupBoxOpenRouter;
    private System.Windows.Forms.Label labelOpenRouterModel;
    private System.Windows.Forms.ComboBox comboBoxOpenRouterModel;
    private System.Windows.Forms.Label labelOpenRouterKey;
    private System.Windows.Forms.TextBox textBoxOpenRouterKey;
    private System.Windows.Forms.Button buttonSave;
    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPageLLM;
    private System.Windows.Forms.TabPage tabPageMCP;
    private System.Windows.Forms.GroupBox groupBoxGitLab;
    private System.Windows.Forms.TextBox textBoxGitLabApiUrl;
    private System.Windows.Forms.Label labelGitLabApiUrl;
    private System.Windows.Forms.Label labelGitLabToken;
    private System.Windows.Forms.TextBox textBoxGitLabToken;
    private System.Windows.Forms.GroupBox groupBoxFilesystem;
    private System.Windows.Forms.TextBox textBoxFilesystemDirectories;
    private System.Windows.Forms.Label labelFilesystemDirectories;
}
