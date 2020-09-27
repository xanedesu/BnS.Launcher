namespace Unlakki.Bns.Launcher.Components
{
    partial class ActivationCodePage
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.comfirmationCodePanel = new System.Windows.Forms.Panel();
            this.messageContainer = new System.Windows.Forms.Label();
            this.activationCodeTextBox = new System.Windows.Forms.TextBox();
            this.activationCodePanel = new System.Windows.Forms.Panel();
            this.comfirmationCodePanel.SuspendLayout();
            this.activationCodePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // comfirmationCodePanel
            // 
            this.comfirmationCodePanel.Controls.Add(this.messageContainer);
            this.comfirmationCodePanel.Controls.Add(this.activationCodePanel);
            this.comfirmationCodePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comfirmationCodePanel.Location = new System.Drawing.Point(0, 0);
            this.comfirmationCodePanel.Margin = new System.Windows.Forms.Padding(0);
            this.comfirmationCodePanel.Name = "comfirmationCodePanel";
            this.comfirmationCodePanel.Padding = new System.Windows.Forms.Padding(12, 6, 12, 12);
            this.comfirmationCodePanel.Size = new System.Drawing.Size(240, 99);
            this.comfirmationCodePanel.TabIndex = 7;
            // 
            // messageContainer
            // 
            this.messageContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.messageContainer.Location = new System.Drawing.Point(12, 6);
            this.messageContainer.Name = "messageContainer";
            this.messageContainer.Size = new System.Drawing.Size(216, 32);
            this.messageContainer.TabIndex = 1;
            this.messageContainer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // activationCodeTextBox
            // 
            this.activationCodeTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.activationCodeTextBox.Location = new System.Drawing.Point(0, 0);
            this.activationCodeTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.activationCodeTextBox.Name = "activationCodeTextBox";
            this.activationCodeTextBox.Size = new System.Drawing.Size(216, 20);
            this.activationCodeTextBox.TabIndex = 0;
            this.activationCodeTextBox.TextChanged += new System.EventHandler(this.activationCodeTextBox_TextChanged);
            // 
            // activationCodePanel
            // 
            this.activationCodePanel.Controls.Add(this.activationCodeTextBox);
            this.activationCodePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.activationCodePanel.Location = new System.Drawing.Point(12, 41);
            this.activationCodePanel.Margin = new System.Windows.Forms.Padding(0);
            this.activationCodePanel.Name = "activationCodePanel";
            this.activationCodePanel.Size = new System.Drawing.Size(216, 46);
            this.activationCodePanel.TabIndex = 2;
            // 
            // ActivationCodePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comfirmationCodePanel);
            this.Name = "ActivationCodePage";
            this.Size = new System.Drawing.Size(240, 99);
            this.comfirmationCodePanel.ResumeLayout(false);
            this.activationCodePanel.ResumeLayout(false);
            this.activationCodePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel comfirmationCodePanel;
        private System.Windows.Forms.Label messageContainer;
        private System.Windows.Forms.TextBox activationCodeTextBox;
        private System.Windows.Forms.Panel activationCodePanel;
    }
}
