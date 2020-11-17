namespace WindowsFormsApplication1
{
    partial class form_inicial
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
            this.BotoEntrar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.NomUsuari = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RegistrarUsuari = new System.Windows.Forms.Button();
            this.Desconnectar = new System.Windows.Forms.Button();
            this.Contrasenya = new System.Windows.Forms.TextBox();
            this.DonamNomGuanyadorsPartidaButton = new System.Windows.Forms.RadioButton();
            this.ID_Partida_TextBox = new System.Windows.Forms.TextBox();
            this.DonamPartidaMaximPuntsJugadorButton = new System.Windows.Forms.RadioButton();
            this.Nom_Jugador_TextBox = new System.Windows.Forms.TextBox();
            this.DonamPersonesQueNoHanGuanyatButton = new System.Windows.Forms.RadioButton();
            this.EnviarPeticionButton = new System.Windows.Forms.Button();
            this.count_lbl = new System.Windows.Forms.Label();
            this.serveis = new System.Windows.Forms.Button();
            this.LlistaConectats = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.MostrarUsuarisConectats = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.LlistaConectats)).BeginInit();
            this.SuspendLayout();
            // 
            // BotoEntrar
            // 
            this.BotoEntrar.Location = new System.Drawing.Point(72, 114);
            this.BotoEntrar.Name = "BotoEntrar";
            this.BotoEntrar.Size = new System.Drawing.Size(119, 42);
            this.BotoEntrar.TabIndex = 0;
            this.BotoEntrar.Text = "Entrar";
            this.BotoEntrar.UseVisualStyleBackColor = true;
            this.BotoEntrar.Click += new System.EventHandler(this.BotoEntrar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "NOM USUARI";
            // 
            // NomUsuari
            // 
            this.NomUsuari.Location = new System.Drawing.Point(127, 32);
            this.NomUsuari.Name = "NomUsuari";
            this.NomUsuari.Size = new System.Drawing.Size(172, 22);
            this.NomUsuari.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "CONTRASENYA";
            // 
            // RegistrarUsuari
            // 
            this.RegistrarUsuari.Location = new System.Drawing.Point(72, 169);
            this.RegistrarUsuari.Name = "RegistrarUsuari";
            this.RegistrarUsuari.Size = new System.Drawing.Size(119, 42);
            this.RegistrarUsuari.TabIndex = 5;
            this.RegistrarUsuari.Text = "Registrar-se";
            this.RegistrarUsuari.UseVisualStyleBackColor = true;
            this.RegistrarUsuari.Click += new System.EventHandler(this.RegistrarUsuari_Click);
            // 
            // Desconnectar
            // 
            this.Desconnectar.Location = new System.Drawing.Point(525, 373);
            this.Desconnectar.Name = "Desconnectar";
            this.Desconnectar.Size = new System.Drawing.Size(194, 42);
            this.Desconnectar.TabIndex = 6;
            this.Desconnectar.Text = "Desconnectar-se";
            this.Desconnectar.UseVisualStyleBackColor = true;
            this.Desconnectar.Click += new System.EventHandler(this.Desconnectar_Click);
            // 
            // Contrasenya
            // 
            this.Contrasenya.Location = new System.Drawing.Point(127, 73);
            this.Contrasenya.Name = "Contrasenya";
            this.Contrasenya.Size = new System.Drawing.Size(172, 22);
            this.Contrasenya.TabIndex = 7;
            // 
            // DonamNomGuanyadorsPartidaButton
            // 
            this.DonamNomGuanyadorsPartidaButton.AutoSize = true;
            this.DonamNomGuanyadorsPartidaButton.Location = new System.Drawing.Point(22, 252);
            this.DonamNomGuanyadorsPartidaButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DonamNomGuanyadorsPartidaButton.Name = "DonamNomGuanyadorsPartidaButton";
            this.DonamNomGuanyadorsPartidaButton.Size = new System.Drawing.Size(337, 21);
            this.DonamNomGuanyadorsPartidaButton.TabIndex = 8;
            this.DonamNomGuanyadorsPartidaButton.TabStop = true;
            this.DonamNomGuanyadorsPartidaButton.Text = "Dona\'m els noms dels guanyadors de la partida: ";
            this.DonamNomGuanyadorsPartidaButton.UseVisualStyleBackColor = true;
            // 
            // ID_Partida_TextBox
            // 
            this.ID_Partida_TextBox.Location = new System.Drawing.Point(348, 250);
            this.ID_Partida_TextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ID_Partida_TextBox.Name = "ID_Partida_TextBox";
            this.ID_Partida_TextBox.Size = new System.Drawing.Size(89, 22);
            this.ID_Partida_TextBox.TabIndex = 9;
            // 
            // DonamPartidaMaximPuntsJugadorButton
            // 
            this.DonamPartidaMaximPuntsJugadorButton.AutoSize = true;
            this.DonamPartidaMaximPuntsJugadorButton.Location = new System.Drawing.Point(22, 276);
            this.DonamPartidaMaximPuntsJugadorButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DonamPartidaMaximPuntsJugadorButton.Name = "DonamPartidaMaximPuntsJugadorButton";
            this.DonamPartidaMaximPuntsJugadorButton.Size = new System.Drawing.Size(510, 21);
            this.DonamPartidaMaximPuntsJugadorButton.TabIndex = 10;
            this.DonamPartidaMaximPuntsJugadorButton.TabStop = true;
            this.DonamPartidaMaximPuntsJugadorButton.Text = "Dona\'m la ID de la partida on el jugador amb el maxim de punts del jugador: ";
            this.DonamPartidaMaximPuntsJugadorButton.UseVisualStyleBackColor = true;
            // 
            // Nom_Jugador_TextBox
            // 
            this.Nom_Jugador_TextBox.Location = new System.Drawing.Point(525, 274);
            this.Nom_Jugador_TextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Nom_Jugador_TextBox.Name = "Nom_Jugador_TextBox";
            this.Nom_Jugador_TextBox.Size = new System.Drawing.Size(108, 22);
            this.Nom_Jugador_TextBox.TabIndex = 11;
            // 
            // DonamPersonesQueNoHanGuanyatButton
            // 
            this.DonamPersonesQueNoHanGuanyatButton.AutoSize = true;
            this.DonamPersonesQueNoHanGuanyatButton.Location = new System.Drawing.Point(22, 300);
            this.DonamPersonesQueNoHanGuanyatButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DonamPersonesQueNoHanGuanyatButton.Name = "DonamPersonesQueNoHanGuanyatButton";
            this.DonamPersonesQueNoHanGuanyatButton.Size = new System.Drawing.Size(432, 21);
            this.DonamPersonesQueNoHanGuanyatButton.TabIndex = 12;
            this.DonamPersonesQueNoHanGuanyatButton.TabStop = true;
            this.DonamPersonesQueNoHanGuanyatButton.Text = "Dona\'m els noms dels jugadors que no han guanyat cap partida";
            this.DonamPersonesQueNoHanGuanyatButton.UseVisualStyleBackColor = true;
            // 
            // EnviarPeticionButton
            // 
            this.EnviarPeticionButton.Location = new System.Drawing.Point(72, 373);
            this.EnviarPeticionButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EnviarPeticionButton.Name = "EnviarPeticionButton";
            this.EnviarPeticionButton.Size = new System.Drawing.Size(119, 40);
            this.EnviarPeticionButton.TabIndex = 13;
            this.EnviarPeticionButton.Text = "Enviar";
            this.EnviarPeticionButton.UseVisualStyleBackColor = true;
            this.EnviarPeticionButton.Click += new System.EventHandler(this.EnviarPeticionButton_Click);
            // 
            // count_lbl
            // 
            this.count_lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.count_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.count_lbl.Location = new System.Drawing.Point(271, 430);
            this.count_lbl.Name = "count_lbl";
            this.count_lbl.Size = new System.Drawing.Size(194, 39);
            this.count_lbl.TabIndex = 17;
            // 
            // serveis
            // 
            this.serveis.Location = new System.Drawing.Point(271, 373);
            this.serveis.Name = "serveis";
            this.serveis.Size = new System.Drawing.Size(194, 42);
            this.serveis.TabIndex = 16;
            this.serveis.Text = "Nº de serveis fets";
            this.serveis.UseVisualStyleBackColor = true;
            this.serveis.Click += new System.EventHandler(this.serveis_Click);
            // 
            // LlistaConectats
            // 
            this.LlistaConectats.AllowUserToAddRows = false;
            this.LlistaConectats.AllowUserToDeleteRows = false;
            this.LlistaConectats.AllowUserToResizeColumns = false;
            this.LlistaConectats.AllowUserToResizeRows = false;
            this.LlistaConectats.BackgroundColor = System.Drawing.SystemColors.Control;
            this.LlistaConectats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.LlistaConectats.ColumnHeadersVisible = false;
            this.LlistaConectats.Location = new System.Drawing.Point(587, 52);
            this.LlistaConectats.Name = "LlistaConectats";
            this.LlistaConectats.ReadOnly = true;
            this.LlistaConectats.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.LlistaConectats.RowTemplate.Height = 24;
            this.LlistaConectats.Size = new System.Drawing.Size(152, 175);
            this.LlistaConectats.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(584, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 17);
            this.label3.TabIndex = 20;
            this.label3.Text = "Llista Usuaris Conectats";
            // 
            // MostrarUsuarisConectats
            // 
            this.MostrarUsuarisConectats.Location = new System.Drawing.Point(442, 114);
            this.MostrarUsuarisConectats.Name = "MostrarUsuarisConectats";
            this.MostrarUsuarisConectats.Size = new System.Drawing.Size(119, 45);
            this.MostrarUsuarisConectats.TabIndex = 21;
            this.MostrarUsuarisConectats.Text = "Actualitzar llista conectats";
            this.MostrarUsuarisConectats.UseVisualStyleBackColor = true;
            this.MostrarUsuarisConectats.Click += new System.EventHandler(this.MostrarUsuarisConectats_Click_1);
            // 
            // form_inicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 497);
            this.Controls.Add(this.MostrarUsuarisConectats);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LlistaConectats);
            this.Controls.Add(this.count_lbl);
            this.Controls.Add(this.serveis);
            this.Controls.Add(this.EnviarPeticionButton);
            this.Controls.Add(this.DonamPersonesQueNoHanGuanyatButton);
            this.Controls.Add(this.Nom_Jugador_TextBox);
            this.Controls.Add(this.DonamPartidaMaximPuntsJugadorButton);
            this.Controls.Add(this.ID_Partida_TextBox);
            this.Controls.Add(this.DonamNomGuanyadorsPartidaButton);
            this.Controls.Add(this.Contrasenya);
            this.Controls.Add(this.Desconnectar);
            this.Controls.Add(this.RegistrarUsuari);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NomUsuari);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BotoEntrar);
            this.Name = "form_inicial";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.form_inicial_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LlistaConectats)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BotoEntrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NomUsuari;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button RegistrarUsuari;
        private System.Windows.Forms.Button Desconnectar;
        private System.Windows.Forms.TextBox Contrasenya;
        private System.Windows.Forms.RadioButton DonamNomGuanyadorsPartidaButton;
        private System.Windows.Forms.TextBox ID_Partida_TextBox;
        private System.Windows.Forms.RadioButton DonamPartidaMaximPuntsJugadorButton;
        private System.Windows.Forms.TextBox Nom_Jugador_TextBox;
        private System.Windows.Forms.RadioButton DonamPersonesQueNoHanGuanyatButton;
        private System.Windows.Forms.Button EnviarPeticionButton;
        private System.Windows.Forms.Label count_lbl;
        private System.Windows.Forms.Button serveis;
        private System.Windows.Forms.DataGridView LlistaConectats;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button MostrarUsuarisConectats;
    }
}

