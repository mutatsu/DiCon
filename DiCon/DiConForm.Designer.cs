namespace DiCon
{
    partial class DiConForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VersionInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tb_rep = new System.Windows.Forms.TabPage();
            this.rep_text = new System.Windows.Forms.TextBox();
            this.tb_rep_szwls = new System.Windows.Forms.TabPage();
            this.szwls_text = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tb_szwls_cpu_utilization = new System.Windows.Forms.TextBox();
            this.tb_szwls_complexity = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.szwls_btn = new System.Windows.Forms.Button();
            this.tb_szwls_peak_tpm = new System.Windows.Forms.TextBox();
            this.tb_szwls_cores_per_cpu = new System.Windows.Forms.TextBox();
            this.tb_szwls_target_cint_base = new System.Windows.Forms.TextBox();
            this.tb_szwls_target_cint_peak = new System.Windows.Forms.TextBox();
            this.tb_szwls_base_cint = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_hstxt = new System.Windows.Forms.TabPage();
            this.hs_text = new System.Windows.Forms.TextBox();
            this.CopyHSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetTemplFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearTemplFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyWLSSizingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearHSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearWLSSizingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tb_rep.SuspendLayout();
            this.tb_rep_szwls.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tb_hstxt.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(537, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SetTemplFileToolStripMenuItem,
            this.ClearTemplFileToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.FileToolStripMenuItem.Text = "ファイル";
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyHSToolStripMenuItem,
            this.CopyWLSSizingToolStripMenuItem,
            this.ClearHSToolStripMenuItem,
            this.ClearWLSSizingToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.EditToolStripMenuItem.Text = "編集";
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VersionInfoToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.HelpToolStripMenuItem.Text = "ヘルプ";
            // 
            // VersionInfoToolStripMenuItem
            // 
            this.VersionInfoToolStripMenuItem.Name = "VersionInfoToolStripMenuItem";
            this.VersionInfoToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.VersionInfoToolStripMenuItem.Text = "バージョン情報";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tb_rep);
            this.tabControl1.Controls.Add(this.tb_rep_szwls);
            this.tabControl1.Controls.Add(this.tb_hstxt);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(537, 338);
            this.tabControl1.TabIndex = 1;
            // 
            // tb_rep
            // 
            this.tb_rep.Controls.Add(this.rep_text);
            this.tb_rep.Location = new System.Drawing.Point(4, 21);
            this.tb_rep.Name = "tb_rep";
            this.tb_rep.Padding = new System.Windows.Forms.Padding(3);
            this.tb_rep.Size = new System.Drawing.Size(529, 313);
            this.tb_rep.TabIndex = 0;
            this.tb_rep.Text = "レポート";
            this.tb_rep.UseVisualStyleBackColor = true;
            // 
            // rep_text
            // 
            this.rep_text.AllowDrop = true;
            this.rep_text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rep_text.Location = new System.Drawing.Point(3, 3);
            this.rep_text.Multiline = true;
            this.rep_text.Name = "rep_text";
            this.rep_text.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.rep_text.Size = new System.Drawing.Size(523, 307);
            this.rep_text.TabIndex = 0;
            // 
            // tb_rep_szwls
            // 
            this.tb_rep_szwls.Controls.Add(this.szwls_text);
            this.tb_rep_szwls.Controls.Add(this.panel1);
            this.tb_rep_szwls.Location = new System.Drawing.Point(4, 21);
            this.tb_rep_szwls.Name = "tb_rep_szwls";
            this.tb_rep_szwls.Padding = new System.Windows.Forms.Padding(3);
            this.tb_rep_szwls.Size = new System.Drawing.Size(529, 313);
            this.tb_rep_szwls.TabIndex = 1;
            this.tb_rep_szwls.Text = "レポート（SizingWLS）";
            this.tb_rep_szwls.UseVisualStyleBackColor = true;
            // 
            // szwls_text
            // 
            this.szwls_text.AllowDrop = true;
            this.szwls_text.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.szwls_text.Location = new System.Drawing.Point(3, 214);
            this.szwls_text.Multiline = true;
            this.szwls_text.Name = "szwls_text";
            this.szwls_text.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.szwls_text.Size = new System.Drawing.Size(523, 96);
            this.szwls_text.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.tb_szwls_cpu_utilization);
            this.panel1.Controls.Add(this.tb_szwls_complexity);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.szwls_btn);
            this.panel1.Controls.Add(this.tb_szwls_peak_tpm);
            this.panel1.Controls.Add(this.tb_szwls_cores_per_cpu);
            this.panel1.Controls.Add(this.tb_szwls_target_cint_base);
            this.panel1.Controls.Add(this.tb_szwls_target_cint_peak);
            this.panel1.Controls.Add(this.tb_szwls_base_cint);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(523, 203);
            this.panel1.TabIndex = 0;
            // 
            // tb_szwls_cpu_utilization
            // 
            this.tb_szwls_cpu_utilization.Location = new System.Drawing.Point(259, 156);
            this.tb_szwls_cpu_utilization.Name = "tb_szwls_cpu_utilization";
            this.tb_szwls_cpu_utilization.Size = new System.Drawing.Size(100, 19);
            this.tb_szwls_cpu_utilization.TabIndex = 15;
            this.tb_szwls_cpu_utilization.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tb_szwls_complexity
            // 
            this.tb_szwls_complexity.Location = new System.Drawing.Point(259, 124);
            this.tb_szwls_complexity.Name = "tb_szwls_complexity";
            this.tb_szwls_complexity.Size = new System.Drawing.Size(100, 19);
            this.tb_szwls_complexity.TabIndex = 14;
            this.tb_szwls_complexity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(175, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "・利用予定サーバのCINT値（Base）";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(174, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "・利用予定サーバのCINT値（Peak）";
            // 
            // szwls_btn
            // 
            this.szwls_btn.Location = new System.Drawing.Point(272, 177);
            this.szwls_btn.Name = "szwls_btn";
            this.szwls_btn.Size = new System.Drawing.Size(75, 23);
            this.szwls_btn.TabIndex = 11;
            this.szwls_btn.Text = "計算";
            this.szwls_btn.UseVisualStyleBackColor = true;
            this.szwls_btn.Click += new System.EventHandler(this.szwls_btn_Click);
            // 
            // tb_szwls_peak_tpm
            // 
            this.tb_szwls_peak_tpm.Location = new System.Drawing.Point(259, 100);
            this.tb_szwls_peak_tpm.Name = "tb_szwls_peak_tpm";
            this.tb_szwls_peak_tpm.Size = new System.Drawing.Size(100, 19);
            this.tb_szwls_peak_tpm.TabIndex = 10;
            this.tb_szwls_peak_tpm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tb_szwls_cores_per_cpu
            // 
            this.tb_szwls_cores_per_cpu.Location = new System.Drawing.Point(259, 76);
            this.tb_szwls_cores_per_cpu.Name = "tb_szwls_cores_per_cpu";
            this.tb_szwls_cores_per_cpu.Size = new System.Drawing.Size(100, 19);
            this.tb_szwls_cores_per_cpu.TabIndex = 9;
            this.tb_szwls_cores_per_cpu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tb_szwls_target_cint_base
            // 
            this.tb_szwls_target_cint_base.Location = new System.Drawing.Point(259, 52);
            this.tb_szwls_target_cint_base.Name = "tb_szwls_target_cint_base";
            this.tb_szwls_target_cint_base.Size = new System.Drawing.Size(100, 19);
            this.tb_szwls_target_cint_base.TabIndex = 8;
            this.tb_szwls_target_cint_base.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tb_szwls_target_cint_peak
            // 
            this.tb_szwls_target_cint_peak.Location = new System.Drawing.Point(259, 29);
            this.tb_szwls_target_cint_peak.Name = "tb_szwls_target_cint_peak";
            this.tb_szwls_target_cint_peak.Size = new System.Drawing.Size(100, 19);
            this.tb_szwls_target_cint_peak.TabIndex = 7;
            this.tb_szwls_target_cint_peak.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tb_szwls_base_cint
            // 
            this.tb_szwls_base_cint.BackColor = System.Drawing.SystemColors.MenuBar;
            this.tb_szwls_base_cint.Location = new System.Drawing.Point(259, 7);
            this.tb_szwls_base_cint.Name = "tb_szwls_base_cint";
            this.tb_szwls_base_cint.Size = new System.Drawing.Size(100, 19);
            this.tb_szwls_base_cint.TabIndex = 6;
            this.tb_szwls_base_cint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(173, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "・ピーク時のCPU使用率（%） 50、80";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "低：10-20 中：20-30 高：30-50";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(196, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "・アプリケーションの複雑度（10-100の間）";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(209, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "・ピーク時のトランザクション数（1分間あたり）";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "・利用予定サーバの1CPUあたりのCore数";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "・基準サーバのCINTの値（24.24 * 1.2）";
            // 
            // tb_hstxt
            // 
            this.tb_hstxt.Controls.Add(this.hs_text);
            this.tb_hstxt.Location = new System.Drawing.Point(4, 21);
            this.tb_hstxt.Name = "tb_hstxt";
            this.tb_hstxt.Padding = new System.Windows.Forms.Padding(3);
            this.tb_hstxt.Size = new System.Drawing.Size(529, 313);
            this.tb_hstxt.TabIndex = 2;
            this.tb_hstxt.Text = "ヒアリング・シート（内容）";
            this.tb_hstxt.UseVisualStyleBackColor = true;
            // 
            // hs_text
            // 
            this.hs_text.AllowDrop = true;
            this.hs_text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hs_text.Location = new System.Drawing.Point(3, 3);
            this.hs_text.Multiline = true;
            this.hs_text.Name = "hs_text";
            this.hs_text.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.hs_text.Size = new System.Drawing.Size(523, 307);
            this.hs_text.TabIndex = 0;
            // 
            // CopyHSToolStripMenuItem
            // 
            this.CopyHSToolStripMenuItem.Name = "CopyHSToolStripMenuItem";
            this.CopyHSToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.CopyHSToolStripMenuItem.Text = "Copy(ヒアリング・シート内容）";
            // 
            // SetTemplFileToolStripMenuItem
            // 
            this.SetTemplFileToolStripMenuItem.Name = "SetTemplFileToolStripMenuItem";
            this.SetTemplFileToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.SetTemplFileToolStripMenuItem.Text = "Set Templ File";
            // 
            // ClearTemplFileToolStripMenuItem
            // 
            this.ClearTemplFileToolStripMenuItem.Name = "ClearTemplFileToolStripMenuItem";
            this.ClearTemplFileToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.ClearTemplFileToolStripMenuItem.Text = "Clear Templ File";
            // 
            // CopyWLSSizingToolStripMenuItem
            // 
            this.CopyWLSSizingToolStripMenuItem.Name = "CopyWLSSizingToolStripMenuItem";
            this.CopyWLSSizingToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.CopyWLSSizingToolStripMenuItem.Text = "Copy(WLS Sizing計算内容)";
            // 
            // ClearHSToolStripMenuItem
            // 
            this.ClearHSToolStripMenuItem.Name = "ClearHSToolStripMenuItem";
            this.ClearHSToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.ClearHSToolStripMenuItem.Text = "Clear(ヒアリング・シート内容)";
            // 
            // ClearWLSSizingToolStripMenuItem
            // 
            this.ClearWLSSizingToolStripMenuItem.Name = "ClearWLSSizingToolStripMenuItem";
            this.ClearWLSSizingToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.ClearWLSSizingToolStripMenuItem.Text = "Clear(WLS Sizing計算内容)";
            // 
            // DiConForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 362);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DiConForm";
            this.Text = "DiCon";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tb_rep.ResumeLayout(false);
            this.tb_rep.PerformLayout();
            this.tb_rep_szwls.ResumeLayout(false);
            this.tb_rep_szwls.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tb_hstxt.ResumeLayout(false);
            this.tb_hstxt.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem VersionInfoToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tb_rep;
        private System.Windows.Forms.TextBox rep_text;
        private System.Windows.Forms.TabPage tb_rep_szwls;
        private System.Windows.Forms.TabPage tb_hstxt;
        private System.Windows.Forms.TextBox hs_text;
        private System.Windows.Forms.TextBox szwls_text;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tb_szwls_cpu_utilization;
        private System.Windows.Forms.TextBox tb_szwls_complexity;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button szwls_btn;
        private System.Windows.Forms.TextBox tb_szwls_peak_tpm;
        private System.Windows.Forms.TextBox tb_szwls_cores_per_cpu;
        private System.Windows.Forms.TextBox tb_szwls_target_cint_base;
        private System.Windows.Forms.TextBox tb_szwls_target_cint_peak;
        private System.Windows.Forms.TextBox tb_szwls_base_cint;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem CopyHSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetTemplFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearTemplFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyWLSSizingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearHSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearWLSSizingToolStripMenuItem;

    }
}