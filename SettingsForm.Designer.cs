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
        groupBoxProvider = new System.Windows.Forms.GroupBox();
        radioButtonOpenRouter = new System.Windows.Forms.RadioButton();
        radioButtonOpenAI = new System.Windows.Forms.RadioButton();
        groupBoxOpenAI = new System.Windows.Forms.GroupBox();
        labelOpenAIModel = new System.Windows.Forms.Label();
        comboBoxOpenAIModel = new System.Windows.Forms.ComboBox();
        labelOpenAIKey = new System.Windows.Forms.Label();
        textBoxOpenAIKey = new System.Windows.Forms.TextBox();
        groupBoxOpenRouter = new System.Windows.Forms.GroupBox();
        labelOpenRouterModel = new System.Windows.Forms.Label();
        comboBoxOpenRouterModel = new System.Windows.Forms.ComboBox();
        labelOpenRouterKey = new System.Windows.Forms.Label();
        textBoxOpenRouterKey = new System.Windows.Forms.TextBox();
        buttonSave = new System.Windows.Forms.Button();
        buttonCancel = new System.Windows.Forms.Button();
        tabControl1 = new System.Windows.Forms.TabControl();
        tabPageLLM = new System.Windows.Forms.TabPage();
        tabPageMCP = new System.Windows.Forms.TabPage();
        groupBoxGitLab = new System.Windows.Forms.GroupBox();
        textBoxGitLabApiUrl = new System.Windows.Forms.TextBox();
        labelGitLabApiUrl = new System.Windows.Forms.Label();
        labelGitLabToken = new System.Windows.Forms.Label();
        textBoxGitLabToken = new System.Windows.Forms.TextBox();
        groupBoxFilesystem = new System.Windows.Forms.GroupBox();
        labelFilesystemDirectories = new System.Windows.Forms.Label();
        textBoxFilesystemDirectories = new System.Windows.Forms.TextBox();
        groupBoxProvider.SuspendLayout();
        groupBoxOpenAI.SuspendLayout();
        groupBoxOpenRouter.SuspendLayout();
        tabControl1.SuspendLayout();
        tabPageLLM.SuspendLayout();
        tabPageMCP.SuspendLayout();
        groupBoxGitLab.SuspendLayout();
        groupBoxFilesystem.SuspendLayout();
        SuspendLayout();
        // 
        // groupBoxProvider
        // 
        groupBoxProvider.Controls.Add(radioButtonOpenRouter);
        groupBoxProvider.Controls.Add(radioButtonOpenAI);
        groupBoxProvider.Location = new System.Drawing.Point(12, 12);
        groupBoxProvider.Name = "groupBoxProvider";
        groupBoxProvider.Size = new System.Drawing.Size(460, 60);
        groupBoxProvider.TabIndex = 0;
        groupBoxProvider.TabStop = false;
        groupBoxProvider.Text = "LLM Provider";
        // 
        // radioButtonOpenRouter
        // 
        radioButtonOpenRouter.AutoSize = true;
        radioButtonOpenRouter.Location = new System.Drawing.Point(240, 25);
        radioButtonOpenRouter.Name = "radioButtonOpenRouter";
        radioButtonOpenRouter.Size = new System.Drawing.Size(91, 19);
        radioButtonOpenRouter.TabIndex = 1;
        radioButtonOpenRouter.Text = "OpenRouter";
        radioButtonOpenRouter.UseVisualStyleBackColor = true;
        radioButtonOpenRouter.CheckedChanged += radioButtonOpenRouter_CheckedChanged;
        // 
        // radioButtonOpenAI
        // 
        radioButtonOpenAI.AutoSize = true;
        radioButtonOpenAI.Checked = true;
        radioButtonOpenAI.Location = new System.Drawing.Point(20, 25);
        radioButtonOpenAI.Name = "radioButtonOpenAI";
        radioButtonOpenAI.Size = new System.Drawing.Size(126, 19);
        radioButtonOpenAI.TabIndex = 0;
        radioButtonOpenAI.TabStop = true;
        radioButtonOpenAI.Text = "OpenAI (ChatGPT)";
        radioButtonOpenAI.UseVisualStyleBackColor = true;
        radioButtonOpenAI.CheckedChanged += radioButtonOpenAI_CheckedChanged;
        // 
        // groupBoxOpenAI
        // 
        groupBoxOpenAI.Controls.Add(labelOpenAIModel);
        groupBoxOpenAI.Controls.Add(comboBoxOpenAIModel);
        groupBoxOpenAI.Controls.Add(labelOpenAIKey);
        groupBoxOpenAI.Controls.Add(textBoxOpenAIKey);
        groupBoxOpenAI.Location = new System.Drawing.Point(12, 78);
        groupBoxOpenAI.Name = "groupBoxOpenAI";
        groupBoxOpenAI.Size = new System.Drawing.Size(460, 110);
        groupBoxOpenAI.TabIndex = 1;
        groupBoxOpenAI.TabStop = false;
        groupBoxOpenAI.Text = "OpenAI Settings";
        // 
        // labelOpenAIModel
        // 
        labelOpenAIModel.AutoSize = true;
        labelOpenAIModel.Location = new System.Drawing.Point(20, 70);
        labelOpenAIModel.Name = "labelOpenAIModel";
        labelOpenAIModel.Size = new System.Drawing.Size(44, 15);
        labelOpenAIModel.TabIndex = 3;
        labelOpenAIModel.Text = "Model:";
        // 
        // comboBoxOpenAIModel
        // 
        comboBoxOpenAIModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBoxOpenAIModel.FormattingEnabled = true;
        comboBoxOpenAIModel.Location = new System.Drawing.Point(120, 67);
        comboBoxOpenAIModel.Name = "comboBoxOpenAIModel";
        comboBoxOpenAIModel.Size = new System.Drawing.Size(320, 23);
        comboBoxOpenAIModel.TabIndex = 2;
        // 
        // labelOpenAIKey
        // 
        labelOpenAIKey.AutoSize = true;
        labelOpenAIKey.Location = new System.Drawing.Point(20, 30);
        labelOpenAIKey.Name = "labelOpenAIKey";
        labelOpenAIKey.Size = new System.Drawing.Size(53, 15);
        labelOpenAIKey.TabIndex = 1;
        labelOpenAIKey.Text = "API Key:";
        // 
        // textBoxOpenAIKey
        // 
        textBoxOpenAIKey.Location = new System.Drawing.Point(120, 27);
        textBoxOpenAIKey.Name = "textBoxOpenAIKey";
        textBoxOpenAIKey.PasswordChar = '•';
        textBoxOpenAIKey.Size = new System.Drawing.Size(320, 23);
        textBoxOpenAIKey.TabIndex = 0;
        // 
        // groupBoxOpenRouter
        // 
        groupBoxOpenRouter.Controls.Add(labelOpenRouterModel);
        groupBoxOpenRouter.Controls.Add(comboBoxOpenRouterModel);
        groupBoxOpenRouter.Controls.Add(labelOpenRouterKey);
        groupBoxOpenRouter.Controls.Add(textBoxOpenRouterKey);
        groupBoxOpenRouter.Location = new System.Drawing.Point(12, 198);
        groupBoxOpenRouter.Name = "groupBoxOpenRouter";
        groupBoxOpenRouter.Size = new System.Drawing.Size(460, 110);
        groupBoxOpenRouter.TabIndex = 2;
        groupBoxOpenRouter.TabStop = false;
        groupBoxOpenRouter.Text = "OpenRouter Settings";
        // 
        // labelOpenRouterModel
        // 
        labelOpenRouterModel.AutoSize = true;
        labelOpenRouterModel.Location = new System.Drawing.Point(20, 70);
        labelOpenRouterModel.Name = "labelOpenRouterModel";
        labelOpenRouterModel.Size = new System.Drawing.Size(44, 15);
        labelOpenRouterModel.TabIndex = 3;
        labelOpenRouterModel.Text = "Model:";
        // 
        // comboBoxOpenRouterModel
        // 
        comboBoxOpenRouterModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBoxOpenRouterModel.FormattingEnabled = true;
        comboBoxOpenRouterModel.Location = new System.Drawing.Point(120, 67);
        comboBoxOpenRouterModel.Name = "comboBoxOpenRouterModel";
        comboBoxOpenRouterModel.Size = new System.Drawing.Size(320, 23);
        comboBoxOpenRouterModel.TabIndex = 2;
        // 
        // labelOpenRouterKey
        // 
        labelOpenRouterKey.AutoSize = true;
        labelOpenRouterKey.Location = new System.Drawing.Point(20, 30);
        labelOpenRouterKey.Name = "labelOpenRouterKey";
        labelOpenRouterKey.Size = new System.Drawing.Size(53, 15);
        labelOpenRouterKey.TabIndex = 1;
        labelOpenRouterKey.Text = "API Key:";
        // 
        // textBoxOpenRouterKey
        // 
        textBoxOpenRouterKey.Location = new System.Drawing.Point(120, 27);
        textBoxOpenRouterKey.Name = "textBoxOpenRouterKey";
        textBoxOpenRouterKey.PasswordChar = '•';
        textBoxOpenRouterKey.Size = new System.Drawing.Size(320, 23);
        textBoxOpenRouterKey.TabIndex = 0;
        // 
        // buttonSave
        // 
        buttonSave.Location = new System.Drawing.Point(291, 318);
        buttonSave.Name = "buttonSave";
        buttonSave.Size = new System.Drawing.Size(87, 30);
        buttonSave.TabIndex = 3;
        buttonSave.Text = "Save";
        buttonSave.UseVisualStyleBackColor = true;
        buttonSave.Click += buttonSave_Click;
        // 
        // buttonCancel
        // 
        buttonCancel.Location = new System.Drawing.Point(384, 318);
        buttonCancel.Name = "buttonCancel";
        buttonCancel.Size = new System.Drawing.Size(87, 30);
        buttonCancel.TabIndex = 4;
        buttonCancel.Text = "Cancel";
        buttonCancel.UseVisualStyleBackColor = true;
        buttonCancel.Click += buttonCancel_Click;
        // 
        // tabControl1
        // 
        tabControl1.Controls.Add(tabPageLLM);
        tabControl1.Controls.Add(tabPageMCP);
        tabControl1.Location = new System.Drawing.Point(12, 12);
        tabControl1.Name = "tabControl1";
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new System.Drawing.Size(580, 380);
        tabControl1.TabIndex = 5;
        // 
        // tabPageLLM
        // 
        tabPageLLM.Controls.Add(groupBoxProvider);
        tabPageLLM.Controls.Add(groupBoxOpenAI);
        tabPageLLM.Controls.Add(groupBoxOpenRouter);
        tabPageLLM.Location = new System.Drawing.Point(4, 24);
        tabPageLLM.Name = "tabPageLLM";
        tabPageLLM.Padding = new System.Windows.Forms.Padding(3);
        tabPageLLM.Size = new System.Drawing.Size(572, 352);
        tabPageLLM.TabIndex = 0;
        tabPageLLM.Text = "LLM Settings";
        tabPageLLM.UseVisualStyleBackColor = true;
        // 
        // tabPageMCP
        // 
        tabPageMCP.Controls.Add(groupBoxFilesystem);
        tabPageMCP.Controls.Add(groupBoxGitLab);
        tabPageMCP.Location = new System.Drawing.Point(4, 24);
        tabPageMCP.Name = "tabPageMCP";
        tabPageMCP.Padding = new System.Windows.Forms.Padding(3);
        tabPageMCP.Size = new System.Drawing.Size(572, 352);
        tabPageMCP.TabIndex = 1;
        tabPageMCP.Text = "MCP Settings";
        tabPageMCP.UseVisualStyleBackColor = true;
        // 
        // groupBoxGitLab
        // 
        groupBoxGitLab.Controls.Add(textBoxGitLabApiUrl);
        groupBoxGitLab.Controls.Add(labelGitLabApiUrl);
        groupBoxGitLab.Controls.Add(labelGitLabToken);
        groupBoxGitLab.Controls.Add(textBoxGitLabToken);
        groupBoxGitLab.Location = new System.Drawing.Point(6, 6);
        groupBoxGitLab.Name = "groupBoxGitLab";
        groupBoxGitLab.Size = new System.Drawing.Size(560, 100);
        groupBoxGitLab.TabIndex = 3;
        groupBoxGitLab.TabStop = false;
        groupBoxGitLab.Text = "GitLab Settings";
        // 
        // textBoxGitLabApiUrl
        // 
        textBoxGitLabApiUrl.Location = new System.Drawing.Point(120, 60);
        textBoxGitLabApiUrl.Name = "textBoxGitLabApiUrl";
        textBoxGitLabApiUrl.Size = new System.Drawing.Size(434, 23);
        textBoxGitLabApiUrl.TabIndex = 3;
        // 
        // labelGitLabApiUrl
        // 
        labelGitLabApiUrl.AutoSize = true;
        labelGitLabApiUrl.Location = new System.Drawing.Point(16, 63);
        labelGitLabApiUrl.Name = "labelGitLabApiUrl";
        labelGitLabApiUrl.Size = new System.Drawing.Size(54, 15);
        labelGitLabApiUrl.TabIndex = 2;
        labelGitLabApiUrl.Text = "API URL:";
        // 
        // labelGitLabToken
        // 
        labelGitLabToken.AutoSize = true;
        labelGitLabToken.Location = new System.Drawing.Point(16, 30);
        labelGitLabToken.Name = "labelGitLabToken";
        labelGitLabToken.Size = new System.Drawing.Size(93, 15);
        labelGitLabToken.TabIndex = 1;
        labelGitLabToken.Text = "Personal Token:";
        // 
        // textBoxGitLabToken
        // 
        textBoxGitLabToken.Location = new System.Drawing.Point(120, 27);
        textBoxGitLabToken.Name = "textBoxGitLabToken";
        textBoxGitLabToken.PasswordChar = '•';
        textBoxGitLabToken.Size = new System.Drawing.Size(434, 23);
        textBoxGitLabToken.TabIndex = 0;
        // 
        // groupBoxFilesystem
        // 
        groupBoxFilesystem.Controls.Add(textBoxFilesystemDirectories);
        groupBoxFilesystem.Controls.Add(labelFilesystemDirectories);
        groupBoxFilesystem.Location = new System.Drawing.Point(6, 112);
        groupBoxFilesystem.Name = "groupBoxFilesystem";
        groupBoxFilesystem.Size = new System.Drawing.Size(560, 234);
        groupBoxFilesystem.TabIndex = 4;
        groupBoxFilesystem.TabStop = false;
        groupBoxFilesystem.Text = "Filesystem Settings";
        // 
        // labelFilesystemDirectories
        // 
        labelFilesystemDirectories.AutoSize = true;
        labelFilesystemDirectories.Location = new System.Drawing.Point(16, 29);
        labelFilesystemDirectories.Name = "labelFilesystemDirectories";
        labelFilesystemDirectories.Size = new System.Drawing.Size(153, 15);
        labelFilesystemDirectories.TabIndex = 0;
        labelFilesystemDirectories.Text = "Accessible Directories (one per line):";
        // 
        // textBoxFilesystemDirectories
        // 
        textBoxFilesystemDirectories.Location = new System.Drawing.Point(16, 47);
        textBoxFilesystemDirectories.Multiline = true;
        textBoxFilesystemDirectories.Name = "textBoxFilesystemDirectories";
        textBoxFilesystemDirectories.Size = new System.Drawing.Size(538, 181);
        textBoxFilesystemDirectories.TabIndex = 1;
        // 
        // SettingsForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(604, 441);
        Controls.Add(tabControl1);
        Controls.Add(buttonCancel);
        Controls.Add(buttonSave);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "SettingsForm";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        Text = "LLM Settings";
        Load += SettingsForm_Load;
        groupBoxProvider.ResumeLayout(false);
        groupBoxProvider.PerformLayout();
        groupBoxOpenAI.ResumeLayout(false);
        groupBoxOpenAI.PerformLayout();
        groupBoxOpenRouter.ResumeLayout(false);
        groupBoxOpenRouter.PerformLayout();
        tabControl1.ResumeLayout(false);
        tabPageLLM.ResumeLayout(false);
        tabPageMCP.ResumeLayout(false);
        groupBoxGitLab.ResumeLayout(false);
        groupBoxGitLab.PerformLayout();
        groupBoxFilesystem.ResumeLayout(false);
        groupBoxFilesystem.PerformLayout();
        ResumeLayout(false);
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
