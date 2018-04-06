namespace Vocup
{
    partial class optionen_dialog
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(optionen_dialog));
            this.ok_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.start_screen = new System.Windows.Forms.GroupBox();
            this.nichts_anzeigen = new System.Windows.Forms.RadioButton();
            this.zuletzt_geöffnet = new System.Windows.Forms.RadioButton();
            this.trackBar_anzahl_falsch_richtig = new System.Windows.Forms.TrackBar();
            this.trackBar_anzahl_noch_nicht = new System.Windows.Forms.TrackBar();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_general = new System.Windows.Forms.TabPage();
            this.path_vhf = new System.Windows.Forms.GroupBox();
            this.button_path_vhf = new System.Windows.Forms.Button();
            this.textbox_path_vhf = new System.Windows.Forms.TextBox();
            this.statistik = new System.Windows.Forms.GroupBox();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.practise_result_list_option = new System.Windows.Forms.CheckBox();
            this.update = new System.Windows.Forms.GroupBox();
            this.auto_update_option = new System.Windows.Forms.CheckBox();
            this.save = new System.Windows.Forms.GroupBox();
            this.auto_save_option = new System.Windows.Forms.CheckBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.übersetzungen_selber_bewerten = new System.Windows.Forms.GroupBox();
            this.selber_bewerten_checkbox = new System.Windows.Forms.CheckBox();
            this.sound_groupbox = new System.Windows.Forms.GroupBox();
            this.sound = new System.Windows.Forms.CheckBox();
            this.continue_button_group_box = new System.Windows.Forms.GroupBox();
            this.continue_button = new System.Windows.Forms.CheckBox();
            this.nearly_correct = new System.Windows.Forms.GroupBox();
            this.panel = new System.Windows.Forms.Panel();
            this.checkbox_synonyme = new System.Windows.Forms.CheckBox();
            this.checkbox_artikel = new System.Windows.Forms.CheckBox();
            this.checkbox_leerschläge = new System.Windows.Forms.CheckBox();
            this.checkbox_sonderzeichen = new System.Windows.Forms.CheckBox();
            this.checkbox_satzzeichen = new System.Windows.Forms.CheckBox();
            this.input_fields = new System.Windows.Forms.GroupBox();
            this.colored_textfield = new System.Windows.Forms.CheckBox();
            this.tabPage_practise = new System.Windows.Forms.TabPage();
            this.zurücksetzen_button = new System.Windows.Forms.Button();
            this.part_of_practise = new System.Windows.Forms.GroupBox();
            this.anzahl_falsch_label = new System.Windows.Forms.Label();
            this.anzahl_richtig_label = new System.Windows.Forms.Label();
            this.anzahl_noch_nicht_label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.times_of_translation = new System.Windows.Forms.GroupBox();
            this.label = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.max_richtig = new System.Windows.Forms.TrackBar();
            this.path_vhr = new System.Windows.Forms.GroupBox();
            this.button_path_vhr = new System.Windows.Forms.Button();
            this.textbox_path_vhr = new System.Windows.Forms.TextBox();
            this.start_screen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_anzahl_falsch_richtig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_anzahl_noch_nicht)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage_general.SuspendLayout();
            this.path_vhf.SuspendLayout();
            this.statistik.SuspendLayout();
            this.update.SuspendLayout();
            this.save.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.übersetzungen_selber_bewerten.SuspendLayout();
            this.sound_groupbox.SuspendLayout();
            this.continue_button_group_box.SuspendLayout();
            this.nearly_correct.SuspendLayout();
            this.panel.SuspendLayout();
            this.input_fields.SuspendLayout();
            this.tabPage_practise.SuspendLayout();
            this.part_of_practise.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.times_of_translation.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.max_richtig)).BeginInit();
            this.path_vhr.SuspendLayout();
            this.SuspendLayout();
            // 
            // ok_button
            // 
            resources.ApplyResources(this.ok_button, "ok_button");
            this.ok_button.Name = "ok_button";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cancel_button, "cancel_button");
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.UseVisualStyleBackColor = true;
            // 
            // start_screen
            // 
            this.start_screen.BackColor = System.Drawing.Color.Transparent;
            this.start_screen.Controls.Add(this.nichts_anzeigen);
            this.start_screen.Controls.Add(this.zuletzt_geöffnet);
            resources.ApplyResources(this.start_screen, "start_screen");
            this.start_screen.Name = "start_screen";
            this.start_screen.TabStop = false;
            // 
            // nichts_anzeigen
            // 
            resources.ApplyResources(this.nichts_anzeigen, "nichts_anzeigen");
            this.nichts_anzeigen.Name = "nichts_anzeigen";
            this.nichts_anzeigen.TabStop = true;
            this.nichts_anzeigen.UseVisualStyleBackColor = true;
            // 
            // zuletzt_geöffnet
            // 
            resources.ApplyResources(this.zuletzt_geöffnet, "zuletzt_geöffnet");
            this.zuletzt_geöffnet.Checked = true;
            this.zuletzt_geöffnet.Name = "zuletzt_geöffnet";
            this.zuletzt_geöffnet.TabStop = true;
            this.zuletzt_geöffnet.UseVisualStyleBackColor = true;
            // 
            // trackBar_anzahl_falsch_richtig
            // 
            this.trackBar_anzahl_falsch_richtig.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.trackBar_anzahl_falsch_richtig, "trackBar_anzahl_falsch_richtig");
            this.trackBar_anzahl_falsch_richtig.Minimum = 1;
            this.trackBar_anzahl_falsch_richtig.Name = "trackBar_anzahl_falsch_richtig";
            this.trackBar_anzahl_falsch_richtig.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar_anzahl_falsch_richtig.Value = 5;
            this.trackBar_anzahl_falsch_richtig.ValueChanged += new System.EventHandler(this.trackBar_anzahl_falsch_richtig_ValueChanged);
            // 
            // trackBar_anzahl_noch_nicht
            // 
            this.trackBar_anzahl_noch_nicht.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.trackBar_anzahl_noch_nicht, "trackBar_anzahl_noch_nicht");
            this.trackBar_anzahl_noch_nicht.Maximum = 8;
            this.trackBar_anzahl_noch_nicht.Minimum = 1;
            this.trackBar_anzahl_noch_nicht.Name = "trackBar_anzahl_noch_nicht";
            this.trackBar_anzahl_noch_nicht.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar_anzahl_noch_nicht.Value = 5;
            this.trackBar_anzahl_noch_nicht.ValueChanged += new System.EventHandler(this.trackBar_anzahl_noch_nicht_ValueChanged);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_general);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage_practise);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPage_general
            // 
            this.tabPage_general.BackColor = System.Drawing.Color.White;
            this.tabPage_general.Controls.Add(this.path_vhr);
            this.tabPage_general.Controls.Add(this.path_vhf);
            this.tabPage_general.Controls.Add(this.statistik);
            this.tabPage_general.Controls.Add(this.update);
            this.tabPage_general.Controls.Add(this.save);
            this.tabPage_general.Controls.Add(this.start_screen);
            resources.ApplyResources(this.tabPage_general, "tabPage_general");
            this.tabPage_general.Name = "tabPage_general";
            // 
            // path_vhf
            // 
            this.path_vhf.Controls.Add(this.button_path_vhf);
            this.path_vhf.Controls.Add(this.textbox_path_vhf);
            resources.ApplyResources(this.path_vhf, "path_vhf");
            this.path_vhf.Name = "path_vhf";
            this.path_vhf.TabStop = false;
            // 
            // button_path_vhf
            // 
            resources.ApplyResources(this.button_path_vhf, "button_path_vhf");
            this.button_path_vhf.Name = "button_path_vhf";
            this.button_path_vhf.UseVisualStyleBackColor = true;
            this.button_path_vhf.Click += new System.EventHandler(this.button_path_vhf_Click);
            // 
            // textbox_path_vhf
            // 
            resources.ApplyResources(this.textbox_path_vhf, "textbox_path_vhf");
            this.textbox_path_vhf.Name = "textbox_path_vhf";
            this.textbox_path_vhf.ReadOnly = true;
            // 
            // statistik
            // 
            this.statistik.Controls.Add(this.comboBox);
            this.statistik.Controls.Add(this.label8);
            this.statistik.Controls.Add(this.practise_result_list_option);
            resources.ApplyResources(this.statistik, "statistik");
            this.statistik.Name = "statistik";
            this.statistik.TabStop = false;
            // 
            // comboBox
            // 
            this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox.Items.AddRange(new object[] {
            resources.GetString("comboBox.Items"),
            resources.GetString("comboBox.Items1")});
            resources.ApplyResources(this.comboBox, "comboBox");
            this.comboBox.Name = "comboBox";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // practise_result_list_option
            // 
            resources.ApplyResources(this.practise_result_list_option, "practise_result_list_option");
            this.practise_result_list_option.Checked = true;
            this.practise_result_list_option.CheckState = System.Windows.Forms.CheckState.Checked;
            this.practise_result_list_option.Name = "practise_result_list_option";
            this.practise_result_list_option.UseVisualStyleBackColor = true;
            // 
            // update
            // 
            this.update.BackColor = System.Drawing.Color.Transparent;
            this.update.Controls.Add(this.auto_update_option);
            resources.ApplyResources(this.update, "update");
            this.update.Name = "update";
            this.update.TabStop = false;
            // 
            // auto_update_option
            // 
            resources.ApplyResources(this.auto_update_option, "auto_update_option");
            this.auto_update_option.Name = "auto_update_option";
            this.auto_update_option.UseVisualStyleBackColor = true;
            // 
            // save
            // 
            this.save.BackColor = System.Drawing.Color.Transparent;
            this.save.Controls.Add(this.auto_save_option);
            resources.ApplyResources(this.save, "save");
            this.save.Name = "save";
            this.save.TabStop = false;
            // 
            // auto_save_option
            // 
            resources.ApplyResources(this.auto_save_option, "auto_save_option");
            this.auto_save_option.Name = "auto_save_option";
            this.auto_save_option.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.übersetzungen_selber_bewerten);
            this.tabPage1.Controls.Add(this.sound_groupbox);
            this.tabPage1.Controls.Add(this.continue_button_group_box);
            this.tabPage1.Controls.Add(this.nearly_correct);
            this.tabPage1.Controls.Add(this.input_fields);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            // 
            // übersetzungen_selber_bewerten
            // 
            this.übersetzungen_selber_bewerten.Controls.Add(this.selber_bewerten_checkbox);
            resources.ApplyResources(this.übersetzungen_selber_bewerten, "übersetzungen_selber_bewerten");
            this.übersetzungen_selber_bewerten.Name = "übersetzungen_selber_bewerten";
            this.übersetzungen_selber_bewerten.TabStop = false;
            // 
            // selber_bewerten_checkbox
            // 
            resources.ApplyResources(this.selber_bewerten_checkbox, "selber_bewerten_checkbox");
            this.selber_bewerten_checkbox.Name = "selber_bewerten_checkbox";
            this.selber_bewerten_checkbox.UseVisualStyleBackColor = true;
            // 
            // sound_groupbox
            // 
            this.sound_groupbox.Controls.Add(this.sound);
            resources.ApplyResources(this.sound_groupbox, "sound_groupbox");
            this.sound_groupbox.Name = "sound_groupbox";
            this.sound_groupbox.TabStop = false;
            // 
            // sound
            // 
            resources.ApplyResources(this.sound, "sound");
            this.sound.Checked = true;
            this.sound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sound.Name = "sound";
            this.sound.UseVisualStyleBackColor = true;
            // 
            // continue_button_group_box
            // 
            this.continue_button_group_box.Controls.Add(this.continue_button);
            resources.ApplyResources(this.continue_button_group_box, "continue_button_group_box");
            this.continue_button_group_box.Name = "continue_button_group_box";
            this.continue_button_group_box.TabStop = false;
            // 
            // continue_button
            // 
            resources.ApplyResources(this.continue_button, "continue_button");
            this.continue_button.BackColor = System.Drawing.Color.Transparent;
            this.continue_button.Name = "continue_button";
            this.continue_button.UseVisualStyleBackColor = false;
            // 
            // nearly_correct
            // 
            this.nearly_correct.Controls.Add(this.panel);
            resources.ApplyResources(this.nearly_correct, "nearly_correct");
            this.nearly_correct.Name = "nearly_correct";
            this.nearly_correct.TabStop = false;
            // 
            // panel
            // 
            resources.ApplyResources(this.panel, "panel");
            this.panel.Controls.Add(this.checkbox_synonyme);
            this.panel.Controls.Add(this.checkbox_artikel);
            this.panel.Controls.Add(this.checkbox_leerschläge);
            this.panel.Controls.Add(this.checkbox_sonderzeichen);
            this.panel.Controls.Add(this.checkbox_satzzeichen);
            this.panel.Name = "panel";
            // 
            // checkbox_synonyme
            // 
            resources.ApplyResources(this.checkbox_synonyme, "checkbox_synonyme");
            this.checkbox_synonyme.Checked = true;
            this.checkbox_synonyme.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkbox_synonyme.Name = "checkbox_synonyme";
            this.checkbox_synonyme.UseVisualStyleBackColor = true;
            // 
            // checkbox_artikel
            // 
            resources.ApplyResources(this.checkbox_artikel, "checkbox_artikel");
            this.checkbox_artikel.Checked = true;
            this.checkbox_artikel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkbox_artikel.Name = "checkbox_artikel";
            this.checkbox_artikel.UseVisualStyleBackColor = true;
            // 
            // checkbox_leerschläge
            // 
            resources.ApplyResources(this.checkbox_leerschläge, "checkbox_leerschläge");
            this.checkbox_leerschläge.Checked = true;
            this.checkbox_leerschläge.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkbox_leerschläge.Name = "checkbox_leerschläge";
            this.checkbox_leerschläge.UseVisualStyleBackColor = true;
            // 
            // checkbox_sonderzeichen
            // 
            resources.ApplyResources(this.checkbox_sonderzeichen, "checkbox_sonderzeichen");
            this.checkbox_sonderzeichen.Checked = true;
            this.checkbox_sonderzeichen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkbox_sonderzeichen.Name = "checkbox_sonderzeichen";
            this.checkbox_sonderzeichen.UseVisualStyleBackColor = true;
            // 
            // checkbox_satzzeichen
            // 
            resources.ApplyResources(this.checkbox_satzzeichen, "checkbox_satzzeichen");
            this.checkbox_satzzeichen.Checked = true;
            this.checkbox_satzzeichen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkbox_satzzeichen.Name = "checkbox_satzzeichen";
            this.checkbox_satzzeichen.UseVisualStyleBackColor = true;
            // 
            // input_fields
            // 
            this.input_fields.Controls.Add(this.colored_textfield);
            resources.ApplyResources(this.input_fields, "input_fields");
            this.input_fields.Name = "input_fields";
            this.input_fields.TabStop = false;
            // 
            // colored_textfield
            // 
            resources.ApplyResources(this.colored_textfield, "colored_textfield");
            this.colored_textfield.BackColor = System.Drawing.Color.Transparent;
            this.colored_textfield.Checked = true;
            this.colored_textfield.CheckState = System.Windows.Forms.CheckState.Checked;
            this.colored_textfield.Name = "colored_textfield";
            this.colored_textfield.UseVisualStyleBackColor = false;
            // 
            // tabPage_practise
            // 
            this.tabPage_practise.BackColor = System.Drawing.Color.White;
            this.tabPage_practise.Controls.Add(this.zurücksetzen_button);
            this.tabPage_practise.Controls.Add(this.part_of_practise);
            this.tabPage_practise.Controls.Add(this.times_of_translation);
            resources.ApplyResources(this.tabPage_practise, "tabPage_practise");
            this.tabPage_practise.Name = "tabPage_practise";
            // 
            // zurücksetzen_button
            // 
            resources.ApplyResources(this.zurücksetzen_button, "zurücksetzen_button");
            this.zurücksetzen_button.Name = "zurücksetzen_button";
            this.zurücksetzen_button.UseVisualStyleBackColor = true;
            this.zurücksetzen_button.Click += new System.EventHandler(this.zurücksetzen_button_Click);
            // 
            // part_of_practise
            // 
            this.part_of_practise.Controls.Add(this.anzahl_falsch_label);
            this.part_of_practise.Controls.Add(this.anzahl_richtig_label);
            this.part_of_practise.Controls.Add(this.anzahl_noch_nicht_label);
            this.part_of_practise.Controls.Add(this.label1);
            this.part_of_practise.Controls.Add(this.pictureBox2);
            this.part_of_practise.Controls.Add(this.pictureBox1);
            this.part_of_practise.Controls.Add(this.pictureBox3);
            this.part_of_practise.Controls.Add(this.label2);
            this.part_of_practise.Controls.Add(this.trackBar_anzahl_noch_nicht);
            this.part_of_practise.Controls.Add(this.trackBar_anzahl_falsch_richtig);
            resources.ApplyResources(this.part_of_practise, "part_of_practise");
            this.part_of_practise.Name = "part_of_practise";
            this.part_of_practise.TabStop = false;
            // 
            // anzahl_falsch_label
            // 
            resources.ApplyResources(this.anzahl_falsch_label, "anzahl_falsch_label");
            this.anzahl_falsch_label.Name = "anzahl_falsch_label";
            // 
            // anzahl_richtig_label
            // 
            resources.ApplyResources(this.anzahl_richtig_label, "anzahl_richtig_label");
            this.anzahl_richtig_label.Name = "anzahl_richtig_label";
            // 
            // anzahl_noch_nicht_label
            // 
            resources.ApplyResources(this.anzahl_noch_nicht_label, "anzahl_noch_nicht_label");
            this.anzahl_noch_nicht_label.Name = "anzahl_noch_nicht_label";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Vocup.icons.falsch_geübt;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Vocup.icons.noch_nicht_geübt;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Vocup.icons.richtig_geübt;
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // times_of_translation
            // 
            this.times_of_translation.Controls.Add(this.label);
            this.times_of_translation.Controls.Add(this.panel1);
            resources.ApplyResources(this.times_of_translation, "times_of_translation");
            this.times_of_translation.Name = "times_of_translation";
            this.times_of_translation.TabStop = false;
            // 
            // label
            // 
            this.label.AutoEllipsis = true;
            resources.ApplyResources(this.label, "label");
            this.label.Name = "label";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.max_richtig);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // max_richtig
            // 
            this.max_richtig.BackColor = System.Drawing.Color.White;
            this.max_richtig.LargeChange = 1;
            resources.ApplyResources(this.max_richtig, "max_richtig");
            this.max_richtig.Maximum = 6;
            this.max_richtig.Minimum = 2;
            this.max_richtig.Name = "max_richtig";
            this.max_richtig.Value = 3;
            // 
            // path_vhr
            // 
            this.path_vhr.Controls.Add(this.button_path_vhr);
            this.path_vhr.Controls.Add(this.textbox_path_vhr);
            resources.ApplyResources(this.path_vhr, "path_vhr");
            this.path_vhr.Name = "path_vhr";
            this.path_vhr.TabStop = false;
            // 
            // button_path_vhr
            // 
            resources.ApplyResources(this.button_path_vhr, "button_path_vhr");
            this.button_path_vhr.Name = "button_path_vhr";
            this.button_path_vhr.UseVisualStyleBackColor = true;
            this.button_path_vhr.Click += new System.EventHandler(this.button_path_vhr_Click);
            // 
            // textbox_path_vhr
            // 
            resources.ApplyResources(this.textbox_path_vhr, "textbox_path_vhr");
            this.textbox_path_vhr.Name = "textbox_path_vhr";
            this.textbox_path_vhr.ReadOnly = true;
            // 
            // optionen_dialog
            // 
            this.AcceptButton = this.ok_button;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.cancel_button;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.ok_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "optionen_dialog";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.optionen_dialog_Load);
            this.start_screen.ResumeLayout(false);
            this.start_screen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_anzahl_falsch_richtig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_anzahl_noch_nicht)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage_general.ResumeLayout(false);
            this.path_vhf.ResumeLayout(false);
            this.path_vhf.PerformLayout();
            this.statistik.ResumeLayout(false);
            this.statistik.PerformLayout();
            this.update.ResumeLayout(false);
            this.update.PerformLayout();
            this.save.ResumeLayout(false);
            this.save.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.übersetzungen_selber_bewerten.ResumeLayout(false);
            this.übersetzungen_selber_bewerten.PerformLayout();
            this.sound_groupbox.ResumeLayout(false);
            this.sound_groupbox.PerformLayout();
            this.continue_button_group_box.ResumeLayout(false);
            this.continue_button_group_box.PerformLayout();
            this.nearly_correct.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.input_fields.ResumeLayout(false);
            this.input_fields.PerformLayout();
            this.tabPage_practise.ResumeLayout(false);
            this.part_of_practise.ResumeLayout(false);
            this.part_of_practise.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.times_of_translation.ResumeLayout(false);
            this.times_of_translation.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.max_richtig)).EndInit();
            this.path_vhr.ResumeLayout(false);
            this.path_vhr.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ok_button;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.GroupBox start_screen;
        private System.Windows.Forms.RadioButton nichts_anzeigen;
        private System.Windows.Forms.RadioButton zuletzt_geöffnet;
        private System.Windows.Forms.TrackBar trackBar_anzahl_falsch_richtig;
        private System.Windows.Forms.TrackBar trackBar_anzahl_noch_nicht;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_general;
        private System.Windows.Forms.TabPage tabPage_practise;
        private System.Windows.Forms.Label anzahl_falsch_label;
        private System.Windows.Forms.Label anzahl_richtig_label;
        private System.Windows.Forms.Label anzahl_noch_nicht_label;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar max_richtig;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox times_of_translation;
        private System.Windows.Forms.GroupBox part_of_practise;
        private System.Windows.Forms.Button zurücksetzen_button;
        private System.Windows.Forms.CheckBox auto_save_option;
        private System.Windows.Forms.GroupBox save;
        private System.Windows.Forms.CheckBox auto_update_option;
        private System.Windows.Forms.GroupBox update;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox input_fields;
        private System.Windows.Forms.CheckBox colored_textfield;
        private System.Windows.Forms.GroupBox nearly_correct;
        private System.Windows.Forms.CheckBox checkbox_artikel;
        private System.Windows.Forms.CheckBox checkbox_sonderzeichen;
        private System.Windows.Forms.CheckBox checkbox_satzzeichen;
        private System.Windows.Forms.CheckBox checkbox_leerschläge;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.CheckBox checkbox_synonyme;
        private System.Windows.Forms.GroupBox continue_button_group_box;
        private System.Windows.Forms.CheckBox continue_button;
        private System.Windows.Forms.GroupBox statistik;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox practise_result_list_option;
        private System.Windows.Forms.GroupBox sound_groupbox;
        private System.Windows.Forms.CheckBox sound;
        private System.Windows.Forms.GroupBox übersetzungen_selber_bewerten;
        private System.Windows.Forms.CheckBox selber_bewerten_checkbox;
        private System.Windows.Forms.GroupBox path_vhf;
        private System.Windows.Forms.Button button_path_vhf;
        private System.Windows.Forms.TextBox textbox_path_vhf;
        private System.Windows.Forms.GroupBox path_vhr;
        private System.Windows.Forms.Button button_path_vhr;
        private System.Windows.Forms.TextBox textbox_path_vhr;
    }
}