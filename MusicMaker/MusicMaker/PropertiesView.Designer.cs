namespace MusicMaker
{
   partial class PropertiesView
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
         this.uxGridViewMotif = new System.Windows.Forms.DataGridView();
         this.uxTabView = new System.Windows.Forms.TabControl();
         this.uxTabMotif = new System.Windows.Forms.TabPage();
         this.uxButtonMotifSave = new System.Windows.Forms.Button();
         this.uxButtonMotifCancel = new System.Windows.Forms.Button();
         this.uxTabBass = new System.Windows.Forms.TabPage();
         ((System.ComponentModel.ISupportInitialize)(this.uxGridViewMotif)).BeginInit();
         this.uxTabView.SuspendLayout();
         this.uxTabMotif.SuspendLayout();
         this.SuspendLayout();
         // 
         // uxGridViewMotif
         // 
         this.uxGridViewMotif.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.uxGridViewMotif.Location = new System.Drawing.Point(6, 6);
         this.uxGridViewMotif.Name = "uxGridViewMotif";
         this.uxGridViewMotif.Size = new System.Drawing.Size(627, 287);
         this.uxGridViewMotif.TabIndex = 0;
         // 
         // uxTabView
         // 
         this.uxTabView.Controls.Add(this.uxTabMotif);
         this.uxTabView.Controls.Add(this.uxTabBass);
         this.uxTabView.Location = new System.Drawing.Point(12, 12);
         this.uxTabView.Name = "uxTabView";
         this.uxTabView.SelectedIndex = 0;
         this.uxTabView.Size = new System.Drawing.Size(647, 374);
         this.uxTabView.TabIndex = 2;
         // 
         // uxTabMotif
         // 
         this.uxTabMotif.Controls.Add(this.uxButtonMotifSave);
         this.uxTabMotif.Controls.Add(this.uxButtonMotifCancel);
         this.uxTabMotif.Controls.Add(this.uxGridViewMotif);
         this.uxTabMotif.Location = new System.Drawing.Point(4, 22);
         this.uxTabMotif.Name = "uxTabMotif";
         this.uxTabMotif.Padding = new System.Windows.Forms.Padding(3);
         this.uxTabMotif.Size = new System.Drawing.Size(639, 348);
         this.uxTabMotif.TabIndex = 0;
         this.uxTabMotif.Text = "Motif";
         this.uxTabMotif.UseVisualStyleBackColor = true;
         // 
         // uxButtonMotifSave
         // 
         this.uxButtonMotifSave.Location = new System.Drawing.Point(477, 319);
         this.uxButtonMotifSave.Name = "uxButtonMotifSave";
         this.uxButtonMotifSave.Size = new System.Drawing.Size(75, 23);
         this.uxButtonMotifSave.TabIndex = 2;
         this.uxButtonMotifSave.Text = "Save";
         this.uxButtonMotifSave.UseVisualStyleBackColor = true;
         this.uxButtonMotifSave.Click += new System.EventHandler(this.uxButtonMotifSave_Click);
         // 
         // uxButtonMotifCancel
         // 
         this.uxButtonMotifCancel.Location = new System.Drawing.Point(558, 319);
         this.uxButtonMotifCancel.Name = "uxButtonMotifCancel";
         this.uxButtonMotifCancel.Size = new System.Drawing.Size(75, 23);
         this.uxButtonMotifCancel.TabIndex = 1;
         this.uxButtonMotifCancel.Text = "Cancel";
         this.uxButtonMotifCancel.UseVisualStyleBackColor = true;
         this.uxButtonMotifCancel.Click += new System.EventHandler(this.uxButtonMotifCancel_Click);
         // 
         // uxTabBass
         // 
         this.uxTabBass.Location = new System.Drawing.Point(4, 22);
         this.uxTabBass.Name = "uxTabBass";
         this.uxTabBass.Padding = new System.Windows.Forms.Padding(3);
         this.uxTabBass.Size = new System.Drawing.Size(639, 348);
         this.uxTabBass.TabIndex = 1;
         this.uxTabBass.Text = "Bass";
         this.uxTabBass.UseVisualStyleBackColor = true;
         // 
         // PropertiesView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(671, 398);
         this.Controls.Add(this.uxTabView);
         this.Name = "PropertiesView";
         this.Text = "Properties";
         ((System.ComponentModel.ISupportInitialize)(this.uxGridViewMotif)).EndInit();
         this.uxTabView.ResumeLayout(false);
         this.uxTabMotif.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.TabControl uxTabView;
      private System.Windows.Forms.TabPage uxTabMotif;
      private System.Windows.Forms.Button uxButtonMotifSave;
      private System.Windows.Forms.Button uxButtonMotifCancel;
      private System.Windows.Forms.TabPage uxTabBass;
      private System.Windows.Forms.DataGridView uxGridViewMotif;
   }
}

