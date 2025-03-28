using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DnD_Project
{
    public partial class CharacterSheet : Form
    {
        private Form previousForm; // Store the reference to the previous form
        private int userid;
        private int characterid;
        private bool edit = false;
        //string dbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db_connection.txt");
        private string connectionString = File.ReadAllText(@"db_connection.txt").Trim();
        public CharacterSheet()
        {
            InitializeComponent();
        }

        // Constructor accepts previous form as parameter
        public CharacterSheet(Form callerForm, int userid)
        {
            InitializeComponent();
            this.previousForm = callerForm; // Store reference
            this.userid = userid;
        }

        public CharacterSheet(Form callerForm, int userid, int characterid , bool editable)
        {
            InitializeComponent();

            this.previousForm = callerForm; // Store reference
            this.userid = userid;
            this.characterid = characterid;
            this.edit = editable;
            LoadCharacterData();
        }

        private void LoadCharacterData()
        {

            string query = @"
            SELECT  character_name,userid, class, level, race, background, 
                    strength, dexterity, constitution, intelligence, wisdom, charisma,
                    proficiency_bonus, armor_class, initiative, speed, 
                    hit_point_maximum, current_hit_points, hit_dice, 
                    character_notes, features_traits, equipment, other_proficiency
            FROM Characters 
            WHERE Character_ID = @CharacterID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CharacterID", characterid);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // General Info
                            textBox1.Text = reader["Character_Name"].ToString();
                            textBox2.Text = reader["Class"].ToString();
                            comboBox2.SelectedItem = reader["Level"].ToString();
                            textBox33.Text = reader["Race"].ToString();
                            textBox34.Text = reader["Background"].ToString();
                            textBox36.Text = reader["Armor_Class"].ToString();
                            textBox35.Text = reader["Initiative"].ToString();
                            textBox37.Text = reader["Speed"].ToString();
                            numericUpDown2.Value = Convert.ToInt32(reader["Hit_Point_Maximum"]);
                            textBox38.Text = reader["Current_Hit_Points"].ToString();
                            comboBox1.SelectedItem = reader["Hit_Dice"].ToString();
                            textBox52.Text = reader["Character_Notes"].ToString();
                            textBox51.Text = reader["Features_Traits"].ToString();
                            textBox50.Text = reader["Equipment"].ToString();
                            textBox49.Text = reader["other_proficiency"].ToString();
                            textBox9.Text = reader["proficiency_bonus"].ToString();

                            // Ability Scores
                            textBox3.Text = reader["Strength"].ToString();
                            textBox4.Text = reader["Dexterity"].ToString();
                            textBox5.Text = reader["Constitution"].ToString();
                            textBox6.Text = reader["Intelligence"].ToString();
                            textBox7.Text = reader["Wisdom"].ToString();
                            textBox8.Text = reader["Charisma"].ToString();

                        }
                    }
                }
            }
            LoadSavingThrows();
            LoadSkills();
        }

        private void LoadSavingThrows()
        {
            string query = @"
        SELECT strength, dexterity, constitution, 
               intelligence, wisdom, charisma
        FROM SavingThrows 
        WHERE character_id = @CharacterId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CharacterId", characterid);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            checkBox1.Checked = Convert.ToBoolean(reader["strength"]);
                            checkBox2.Checked = Convert.ToBoolean(reader["dexterity"]);
                            checkBox3.Checked = Convert.ToBoolean(reader["constitution"]);
                            checkBox4.Checked = Convert.ToBoolean(reader["intelligence"]);
                            checkBox5.Checked = Convert.ToBoolean(reader["wisdom"]);
                            checkBox6.Checked = Convert.ToBoolean(reader["charisma"]);
                        }
                    }
                }
            }
        }

        private void LoadSkills()
        {
            string query = @"
        SELECT acrobatics, animal_handling, arcana, deception, history, 
               insight, intimidation, investigation, medicine, nature, perception, 
               performance, persuasion, religion, sleight_of_hand, stealth, survival
        FROM Skills 
        WHERE character_id = @CharacterId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CharacterId", characterid);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            checkBox12.Checked = Convert.ToBoolean(reader["acrobatics"]);
                            checkBox11.Checked = Convert.ToBoolean(reader["animal_handling"]);
                            checkBox10.Checked = Convert.ToBoolean(reader["arcana"]);
                            checkBox9.Checked = Convert.ToBoolean(reader["deception"]);
                            checkBox8.Checked = Convert.ToBoolean(reader["history"]);
                            checkBox7.Checked = Convert.ToBoolean(reader["insight"]);
                            checkBox13.Checked = Convert.ToBoolean(reader["intimidation"]);
                            checkBox14.Checked = Convert.ToBoolean(reader["investigation"]);
                            checkBox15.Checked = Convert.ToBoolean(reader["medicine"]);
                            checkBox16.Checked = Convert.ToBoolean(reader["nature"]);
                            checkBox17.Checked = Convert.ToBoolean(reader["perception"]);
                            checkBox18.Checked = Convert.ToBoolean(reader["performance"]);
                            checkBox19.Checked = Convert.ToBoolean(reader["persuasion"]);
                            checkBox20.Checked = Convert.ToBoolean(reader["religion"]);
                            checkBox21.Checked = Convert.ToBoolean(reader["sleight_of_hand"]);
                            checkBox22.Checked = Convert.ToBoolean(reader["stealth"]);
                            checkBox23.Checked = Convert.ToBoolean(reader["survival"]);
                        }
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox11_Enter(object sender, EventArgs e)
        {

        }
        private void SavingThrows_CheckedChanged(object sender, EventArgs e)
        {
            // Get all checkboxes inside the GroupBox
            var checkboxes = groupBox11.Controls.OfType<CheckBox>();

            // Count how many checkboxes are checked
            int checkedCount = checkboxes.Count(cb => cb.Checked);

            // Disable unselected checkboxes if 2 are already checked
            if (checkedCount >= 2)
            {
                foreach (var checkbox in checkboxes)
                {
                    if (!checkbox.Checked)
                        checkbox.Enabled = false;
                }
            }
            else
            {
                // Re-enable all checkboxes if less than 2 are checked
                foreach (var checkbox in checkboxes)
                {
                    checkbox.Enabled = true;
                }
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Blocks non-numeric input
            }
        }

        private void button1_Click(object sender, EventArgs e) //Go back Button
        {
            this.Hide();
            previousForm.Show();
        }

        private void CharacterSheet_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void addCharacter_Click(object sender, EventArgs e)
        {
            
            string charName = textBox1.Text;
            string charClass = textBox2.Text;
            int level = Convert.ToInt32(comboBox2.SelectedItem);
            string race = textBox33.Text;
            string background = textBox34.Text;
            int armorClass = Convert.ToInt32(textBox36.Text);
            int initiative = Convert.ToInt32(textBox35.Text);
            int speed = Convert.ToInt32(textBox37.Text);
            int hitPointMax = Convert.ToInt32(numericUpDown2.Value);
            int currentHitPoints = Convert.ToInt32(textBox38.Text);
            string hitDice = comboBox1.SelectedItem.ToString();
            int proficiencyBonus = 2 + (level - 1) / 4; // Standard D&D calculation
            string characterNotes = textBox52.Text;
            string featuresTraits = textBox51.Text;
            string equipments = textBox50.Text;
            string otherProficiency = textBox49.Text; // Add if you have this field

            // Ability scores
            int strength = Convert.ToInt32(textBox3.Text);
            int dexterity = Convert.ToInt32(textBox4.Text);
            int constitution = Convert.ToInt32(textBox5.Text);
            int intelligence = Convert.ToInt32(textBox6.Text);
            int wisdom = Convert.ToInt32(textBox7.Text);
            int charisma = Convert.ToInt32(textBox8.Text);

            // Saving throw proficiencies (from checkboxes)
            bool strengthSave = checkBox1.Checked;
            bool dexteritySave = checkBox2.Checked;
            bool constitutionSave = checkBox3.Checked;
            bool intelligenceSave = checkBox4.Checked;
            bool wisdomSave = checkBox5.Checked;
            bool charismaSave = checkBox6.Checked;

            // Skills (from checkboxes)
            bool acrobatics = checkBox7.Checked;
            bool animalHandling = checkBox8.Checked;
            bool arcana = checkBox9.Checked;
            bool deception = checkBox10.Checked;
            bool history = checkBox11.Checked;
            bool insight = checkBox12.Checked;
            bool intimidation = checkBox13.Checked;
            bool investigation = checkBox14.Checked;
            bool medicine = checkBox15.Checked;
            bool nature = checkBox16.Checked;
            bool perception = checkBox17.Checked;
            bool performance = checkBox18.Checked;
            bool persuasion = checkBox19.Checked;
            bool religion = checkBox20.Checked;
            bool sleightOfHand = checkBox21.Checked;
            bool stealth = checkBox22.Checked;
            bool survival = checkBox23.Checked;

                // Validate required fields
            if (edit == false)
            {// Get input values
                if (string.IsNullOrWhiteSpace(charName) || string.IsNullOrWhiteSpace(charClass) || level <= 0)
                {
                    MessageBox.Show("Character Name, Class, and Level are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();

                            // Insert into Characters table
                            string insertCharacterQuery = @"
                INSERT INTO Characters (
                    character_name,userid, class, level, race, background, 
                    strength, dexterity, constitution, intelligence, wisdom, charisma,
                    proficiency_bonus, armor_class, initiative, speed, 
                    hit_point_maximum, current_hit_points, hit_dice, 
                    character_notes, features_traits, equipment, other_proficiency
                ) 
                VALUES (
                    @CharacterName,@userid, @Class, @Level, @Race, @Background,
                    @Strength, @Dexterity, @Constitution, @Intelligence, @Wisdom, @Charisma,
                    @ProficiencyBonus, @ArmorClass, @Initiative, @Speed,
                    @HitPointMax, @CurrentHitPoints, @HitDice,
                    @CharacterNotes, @FeaturesTraits, @Equipments, @OtherProficiency
                );
                SELECT SCOPE_IDENTITY();";

                            int characterId;
                            using (SqlCommand cmd = new SqlCommand(insertCharacterQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@CharacterName", charName);
                                cmd.Parameters.AddWithValue("@userid", userid);
                                cmd.Parameters.AddWithValue("@Class", charClass);
                                cmd.Parameters.AddWithValue("@Level", level);
                                cmd.Parameters.AddWithValue("@Race", race);
                                cmd.Parameters.AddWithValue("@Background", background);
                                cmd.Parameters.AddWithValue("@Strength", strength);
                                cmd.Parameters.AddWithValue("@Dexterity", dexterity);
                                cmd.Parameters.AddWithValue("@Constitution", constitution);
                                cmd.Parameters.AddWithValue("@Intelligence", intelligence);
                                cmd.Parameters.AddWithValue("@Wisdom", wisdom);
                                cmd.Parameters.AddWithValue("@Charisma", charisma);
                                cmd.Parameters.AddWithValue("@ProficiencyBonus", proficiencyBonus);
                                cmd.Parameters.AddWithValue("@ArmorClass", armorClass);
                                cmd.Parameters.AddWithValue("@Initiative", initiative);
                                cmd.Parameters.AddWithValue("@Speed", speed);
                                cmd.Parameters.AddWithValue("@HitPointMax", hitPointMax);
                                cmd.Parameters.AddWithValue("@CurrentHitPoints", currentHitPoints);
                                cmd.Parameters.AddWithValue("@HitDice", hitDice);
                                cmd.Parameters.AddWithValue("@CharacterNotes", characterNotes);
                                cmd.Parameters.AddWithValue("@FeaturesTraits", featuresTraits);
                                cmd.Parameters.AddWithValue("@Equipments", equipments);
                                cmd.Parameters.AddWithValue("@OtherProficiency", otherProficiency);

                                characterId = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            // Insert into Abilities table

                            // Link abilities to character with proficiency status

                            // Insert saving throws
                            string insertSavingThrowsQuery = @"
                INSERT INTO SavingThrows (
                    character_id, strength, dexterity, constitution, 
                    intelligence, wisdom, charisma
                ) 
                VALUES (
                    @CharacterId, @Strength, @Dexterity, @Constitution,
                    @Intelligence, @Wisdom, @Charisma
                )";

                            using (SqlCommand cmd = new SqlCommand(insertSavingThrowsQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@CharacterId", characterId);
                                cmd.Parameters.AddWithValue("@Strength", strengthSave);
                                cmd.Parameters.AddWithValue("@Dexterity", dexteritySave);
                                cmd.Parameters.AddWithValue("@Constitution", constitutionSave);
                                cmd.Parameters.AddWithValue("@Intelligence", intelligenceSave);
                                cmd.Parameters.AddWithValue("@Wisdom", wisdomSave);
                                cmd.Parameters.AddWithValue("@Charisma", charismaSave);
                                cmd.ExecuteNonQuery();
                            }

                            // Insert skills
                            string insertSkillsQuery = @"
                INSERT INTO Skills (
                    character_id, acrobatics, animal_handling, arcana, deception, history,
                    insight, intimidation, investigation, medicine, nature, perception,
                    performance, persuasion, religion, sleight_of_hand, stealth, survival
                ) 
                VALUES (
                    @CharacterId, @Acrobatics, @AnimalHandling, @Arcana, @Deception, @History,
                    @Insight, @Intimidation, @Investigation, @Medicine, @Nature, @Perception,
                    @Performance, @Persuasion, @Religion, @SleightOfHand, @Stealth, @Survival
                )";

                            using (SqlCommand cmd = new SqlCommand(insertSkillsQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@CharacterId", characterId);
                                cmd.Parameters.AddWithValue("@Acrobatics", acrobatics);
                                cmd.Parameters.AddWithValue("@AnimalHandling", animalHandling);
                                cmd.Parameters.AddWithValue("@Arcana", arcana);
                                cmd.Parameters.AddWithValue("@Deception", deception);
                                cmd.Parameters.AddWithValue("@History", history);
                                cmd.Parameters.AddWithValue("@Insight", insight);
                                cmd.Parameters.AddWithValue("@Intimidation", intimidation);
                                cmd.Parameters.AddWithValue("@Investigation", investigation);
                                cmd.Parameters.AddWithValue("@Medicine", medicine);
                                cmd.Parameters.AddWithValue("@Nature", nature);
                                cmd.Parameters.AddWithValue("@Perception", perception);
                                cmd.Parameters.AddWithValue("@Performance", performance);
                                cmd.Parameters.AddWithValue("@Persuasion", persuasion);
                                cmd.Parameters.AddWithValue("@Religion", religion);
                                cmd.Parameters.AddWithValue("@SleightOfHand", sleightOfHand);
                                cmd.Parameters.AddWithValue("@Stealth", stealth);
                                cmd.Parameters.AddWithValue("@Survival", survival);
                                cmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("Character added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: The Message{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
            }
                else if (edit == true)
                {
                if (characterid <= 0)
                {
                    MessageBox.Show("Invalid Character ID for updating!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        // Update Characters Table
                        string updateCharacterQuery = @"
                UPDATE Characters 
                SET character_name = @CharacterName, class = @Class, level = @Level, race = @Race, background = @Background,
                    strength = @Strength, dexterity = @Dexterity, constitution = @Constitution, intelligence = @Intelligence,
                    wisdom = @Wisdom, charisma = @Charisma, proficiency_bonus = @ProficiencyBonus, armor_class = @ArmorClass,
                    initiative = @Initiative, speed = @Speed, hit_point_maximum = @HitPointMax, current_hit_points = @CurrentHitPoints,
                    hit_dice = @HitDice, character_notes = @CharacterNotes, features_traits = @FeaturesTraits,
                    equipment = @Equipments, other_proficiency = @OtherProficiency
                WHERE character_id = @CharacterId";

                        using (SqlCommand cmd = new SqlCommand(updateCharacterQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@CharacterId", characterid);
                            cmd.Parameters.AddWithValue("@CharacterName", charName);
                            cmd.Parameters.AddWithValue("@Class", charClass);
                            cmd.Parameters.AddWithValue("@Level", level);
                            cmd.Parameters.AddWithValue("@Race", race);
                            cmd.Parameters.AddWithValue("@Background", background);
                            cmd.Parameters.AddWithValue("@Strength", strength);
                            cmd.Parameters.AddWithValue("@Dexterity", dexterity);
                            cmd.Parameters.AddWithValue("@Constitution", constitution);
                            cmd.Parameters.AddWithValue("@Intelligence", intelligence);
                            cmd.Parameters.AddWithValue("@Wisdom", wisdom);
                            cmd.Parameters.AddWithValue("@Charisma", charisma);
                            cmd.Parameters.AddWithValue("@ProficiencyBonus", proficiencyBonus);
                            cmd.Parameters.AddWithValue("@ArmorClass", armorClass);
                            cmd.Parameters.AddWithValue("@Initiative", initiative);
                            cmd.Parameters.AddWithValue("@Speed", speed);
                            cmd.Parameters.AddWithValue("@HitPointMax", hitPointMax);
                            cmd.Parameters.AddWithValue("@CurrentHitPoints", currentHitPoints);
                            cmd.Parameters.AddWithValue("@HitDice", hitDice);
                            cmd.Parameters.AddWithValue("@CharacterNotes", characterNotes);
                            cmd.Parameters.AddWithValue("@FeaturesTraits", featuresTraits);
                            cmd.Parameters.AddWithValue("@Equipments", equipments);
                            cmd.Parameters.AddWithValue("@OtherProficiency", otherProficiency);

                            cmd.ExecuteNonQuery();
                        }

                        // Update Saving Throws Table
                        string updateSavingThrowsQuery = @"
                UPDATE SavingThrows 
                SET strength = @Strength, dexterity = @Dexterity, constitution = @Constitution, 
                    intelligence = @Intelligence, wisdom = @Wisdom, charisma = @Charisma
                WHERE character_id = @CharacterId";

                        using (SqlCommand cmd = new SqlCommand(updateSavingThrowsQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@CharacterId", characterid);
                            cmd.Parameters.AddWithValue("@Strength", strengthSave);
                            cmd.Parameters.AddWithValue("@Dexterity", dexteritySave);
                            cmd.Parameters.AddWithValue("@Constitution", constitutionSave);
                            cmd.Parameters.AddWithValue("@Intelligence", intelligenceSave);
                            cmd.Parameters.AddWithValue("@Wisdom", wisdomSave);
                            cmd.Parameters.AddWithValue("@Charisma", charismaSave);

                            cmd.ExecuteNonQuery();
                        }

                        // Update Skills Table
                        string updateSkillsQuery = @"
                UPDATE Skills 
                SET acrobatics = @Acrobatics, animal_handling = @AnimalHandling, arcana = @Arcana, deception = @Deception, 
                    history = @History, insight = @Insight, intimidation = @Intimidation, investigation = @Investigation, 
                    medicine = @Medicine, nature = @Nature, perception = @Perception, performance = @Performance, 
                    persuasion = @Persuasion, religion = @Religion, sleight_of_hand = @SleightOfHand, 
                    stealth = @Stealth, survival = @Survival
                WHERE character_id = @CharacterId";

                        using (SqlCommand cmd = new SqlCommand(updateSkillsQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@CharacterId", characterid);
                            cmd.Parameters.AddWithValue("@Acrobatics", acrobatics);
                            cmd.Parameters.AddWithValue("@AnimalHandling", animalHandling);
                            cmd.Parameters.AddWithValue("@Arcana", arcana);
                            cmd.Parameters.AddWithValue("@Deception", deception);
                            cmd.Parameters.AddWithValue("@History", history);
                            cmd.Parameters.AddWithValue("@Insight", insight);
                            cmd.Parameters.AddWithValue("@Intimidation", intimidation);
                            cmd.Parameters.AddWithValue("@Investigation", investigation);
                            cmd.Parameters.AddWithValue("@Medicine", medicine);
                            cmd.Parameters.AddWithValue("@Nature", nature);
                            cmd.Parameters.AddWithValue("@Perception", perception);
                            cmd.Parameters.AddWithValue("@Performance", performance);
                            cmd.Parameters.AddWithValue("@Persuasion", persuasion);
                            cmd.Parameters.AddWithValue("@Religion", religion);
                            cmd.Parameters.AddWithValue("@SleightOfHand", sleightOfHand);
                            cmd.Parameters.AddWithValue("@Stealth", stealth);
                            cmd.Parameters.AddWithValue("@Survival", survival);

                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Character updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            //Change it to edit from query
        }



        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

