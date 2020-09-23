namespace AlumniDesktopApplication
{
    partial class AddJob
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.addJobPost = new System.Windows.Forms.Button();
            this.titleTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.expTxt = new System.Windows.Forms.TextBox();
            this.skillsTxt = new System.Windows.Forms.TextBox();
            this.descTxt = new System.Windows.Forms.TextBox();
            this.organizationDropDown = new System.Windows.Forms.ComboBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title";
            // 
            // addJobPost
            // 
            this.addJobPost.Location = new System.Drawing.Point(201, 281);
            this.addJobPost.Name = "addJobPost";
            this.addJobPost.Size = new System.Drawing.Size(75, 23);
            this.addJobPost.TabIndex = 5;
            this.addJobPost.Text = "ADD";
            this.addJobPost.UseVisualStyleBackColor = true;
            this.addJobPost.Click += new System.EventHandler(this.addJobPost_Click);
            // 
            // titleTxt
            // 
            this.titleTxt.Location = new System.Drawing.Point(176, 30);
            this.titleTxt.Name = "titleTxt";
            this.titleTxt.Size = new System.Drawing.Size(121, 20);
            this.titleTxt.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Skills";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Experience";
            // 
            // expTxt
            // 
            this.expTxt.Location = new System.Drawing.Point(176, 167);
            this.expTxt.Name = "expTxt";
            this.expTxt.Size = new System.Drawing.Size(121, 20);
            this.expTxt.TabIndex = 3;
            // 
            // skillsTxt
            // 
            this.skillsTxt.Location = new System.Drawing.Point(176, 121);
            this.skillsTxt.Name = "skillsTxt";
            this.skillsTxt.Size = new System.Drawing.Size(121, 20);
            this.skillsTxt.TabIndex = 2;
            // 
            // descTxt
            // 
            this.descTxt.Location = new System.Drawing.Point(176, 70);
            this.descTxt.Name = "descTxt";
            this.descTxt.Size = new System.Drawing.Size(121, 20);
            this.descTxt.TabIndex = 1;
            // 
            // organizationDropDown
            // 
            this.organizationDropDown.FormattingEnabled = true;
            this.organizationDropDown.Location = new System.Drawing.Point(176, 218);
            this.organizationDropDown.Name = "organizationDropDown";
            this.organizationDropDown.Size = new System.Drawing.Size(121, 21);
            this.organizationDropDown.TabIndex = 4;
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(95, 281);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 10;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 226);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Organization";
            // 
            // AddJob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 330);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.organizationDropDown);
            this.Controls.Add(this.descTxt);
            this.Controls.Add(this.skillsTxt);
            this.Controls.Add(this.expTxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.titleTxt);
            this.Controls.Add(this.addJobPost);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddJob";
            this.Text = "AddJob";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addJobPost;
        private System.Windows.Forms.TextBox titleTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox expTxt;
        private System.Windows.Forms.TextBox skillsTxt;
        private System.Windows.Forms.TextBox descTxt;
        private System.Windows.Forms.ComboBox organizationDropDown;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label label5;
    }
}