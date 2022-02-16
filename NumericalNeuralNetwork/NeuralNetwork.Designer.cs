
namespace NumericalNeuralNetwork
{
    partial class NeuralNetwork
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NeuralNetwork));
            this.TestNeuralNetwork = new System.Windows.Forms.Button();
            this.NumberInImage = new System.Windows.Forms.PictureBox();
            this.Coincidence = new System.Windows.Forms.Button();
            this.CheckNeuralNetwork = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NumberInImage)).BeginInit();
            this.SuspendLayout();
            // 
            // TestNeuralNetwork
            // 
            this.TestNeuralNetwork.Location = new System.Drawing.Point(54, 172);
            this.TestNeuralNetwork.Name = "TestNeuralNetwork";
            this.TestNeuralNetwork.Size = new System.Drawing.Size(167, 29);
            this.TestNeuralNetwork.TabIndex = 0;
            this.TestNeuralNetwork.Text = "Старт обучения";
            this.TestNeuralNetwork.UseVisualStyleBackColor = true;
            this.TestNeuralNetwork.Click += new System.EventHandler(this.TestNeuralNetworkClick);
            // 
            // NumberInImage
            // 
            this.NumberInImage.Location = new System.Drawing.Point(54, 12);
            this.NumberInImage.Name = "NumberInImage";
            this.NumberInImage.Size = new System.Drawing.Size(167, 153);
            this.NumberInImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.NumberInImage.TabIndex = 1;
            this.NumberInImage.TabStop = false;
            // 
            // Coincidence
            // 
            this.Coincidence.Location = new System.Drawing.Point(54, 206);
            this.Coincidence.Name = "Coincidence";
            this.Coincidence.Size = new System.Drawing.Size(167, 29);
            this.Coincidence.TabIndex = 2;
            this.Coincidence.Text = "Процент совпадения";
            this.Coincidence.UseVisualStyleBackColor = true;
            this.Coincidence.Click += new System.EventHandler(this.Coincidence_Click);
            // 
            // CheckNeuralNetwork
            // 
            this.CheckNeuralNetwork.Location = new System.Drawing.Point(87, 239);
            this.CheckNeuralNetwork.Name = "CheckNeuralNetwork";
            this.CheckNeuralNetwork.Size = new System.Drawing.Size(94, 29);
            this.CheckNeuralNetwork.TabIndex = 3;
            this.CheckNeuralNetwork.Text = "Проверка";
            this.CheckNeuralNetwork.UseVisualStyleBackColor = true;
            this.CheckNeuralNetwork.Click += new System.EventHandler(this.CheckNeuralNetwork_Click);
            // 
            // NeuralNetwork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 445);
            this.Controls.Add(this.CheckNeuralNetwork);
            this.Controls.Add(this.Coincidence);
            this.Controls.Add(this.NumberInImage);
            this.Controls.Add(this.TestNeuralNetwork);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NeuralNetwork";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NumericalNeuralNetwork";
            ((System.ComponentModel.ISupportInitialize)(this.NumberInImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TestNeuralNetwork;
        private System.Windows.Forms.PictureBox NumberInImage;
        private System.Windows.Forms.Button Coincidence;
        private System.Windows.Forms.Button CheckNeuralNetwork;
    }
}

