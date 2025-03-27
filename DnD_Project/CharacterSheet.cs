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

        //private void addCharacter_Click(object sender, EventArgs e)
        //{
        //    string dbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db_connection.txt");
        //    string connectionString = File.ReadAllText(dbFilePath).Trim();

        //    // Get input values
        //    int userID = this.userid; // Fetch the currently logged-in user's ID
        //    string charName = textBox1.Text;
        //    string charClass = textBox2.Text;
        //    int level = Convert.ToInt32(comboBox2.SelectedItem);
        //    string race = textBox33.Text;
        //    string background = textBox34.Text;
        //    int armorClass = Convert.ToInt32(textBox36.Text);
        //    int initiative = Convert.ToInt32(textBox35.Text);
        //    int speed = Convert.ToInt32(textBox37.Text);
        //    int hitPointMax = Convert.ToInt32(numericUpDown2.Value);
        //    int currentHitPoints = Convert.ToInt32(textBox38.Text);
        //    string hitDice = (comboBox1.SelectedItem.ToString());
        //    //int proficiencyBonus = Convert.ToInt32(textBox9.Text);
        //    int proficiencyBonus = 0;
        //    string characterNotes = textBox52.Text;
        //    string featuresTraits = textBox51.Text;
        //    string equipments = textBox50.Text;
        //    int strength = Convert.ToInt32(textBox3.Text);
        //    int dexterity = Convert.ToInt32(textBox4.Text);
        //    int Constitution = Convert.ToInt32(textBox5.Text);
        //    int Intelligence = Convert.ToInt32(textBox6.Text);
        //    int Wisdom = Convert.ToInt32(textBox7.Text);
        //    int Charisma = Convert.ToInt32(textBox8.Text);
        //    bool strengthbool = false, dexteritybool = false, constitutionbool = false, intelligencebool = false, wisdombool = false, charismabool = false;
        //    if (checkBox1.Checked) strengthbool = true;
        //    if (checkBox2.Checked) dexteritybool = true;
        //    if (checkBox3.Checked) constitutionbool = true;
        //    if (checkBox4.Checked) intelligencebool = true;
        //    if (checkBox5.Checked) wisdombool = true;
        //    if (checkBox6.Checked) charismabool = true;

        //    // Initialize all skill booleans to false
        //    bool acrobatics = false, animalHandling = false, arcana = false, deception = false;
        //    bool history = false, insight = false, intimidation = false, investigation = false;
        //    bool medicine = false, nature = false, perception = false, performance = false;
        //    bool persuasion = false, religion = false, sleightOfHand = false, stealth = false;
        //    bool survival = false;

        //    // Check which checkboxes are checked
        //    if (checkBox7.Checked) acrobatics = true;
        //    if (checkBox8.Checked) animalHandling = true;
        //    if (checkBox9.Checked) arcana = true;
        //    if (checkBox10.Checked) deception = true;
        //    if (checkBox11.Checked) history = true;
        //    if (checkBox12.Checked) insight = true;
        //    if (checkBox13.Checked) intimidation = true;
        //    if (checkBox14.Checked) investigation = true;
        //    if (checkBox15.Checked) medicine = true;
        //    if (checkBox16.Checked) nature = true;
        //    if (checkBox17.Checked) perception = true;
        //    if (checkBox18.Checked) performance = true;
        //    if (checkBox19.Checked) persuasion = true;
        //    if (checkBox20.Checked) religion = true;
        //    if (checkBox21.Checked) sleightOfHand = true;
        //    if (checkBox22.Checked) stealth = true;
        //    if (checkBox23.Checked) survival = true;



        //    // Validate required fields
        //    if (string.IsNullOrWhiteSpace(charName) || string.IsNullOrWhiteSpace(charClass) || level <= 0)
        //    {
        //        MessageBox.Show("Character Name, Class, and Level are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlTransaction transaction = conn.BeginTransaction(); // Start transaction

        //        try
        //        {
        //            // **Step 1: Insert into Characters Table**
        //            string insertCharacterQuery = @"
        //        INSERT INTO Characters (UserID, CharacterName, Class, Level, Race, Background, ArmorClass, Initiative, Speed, HitPointMax, 
        //        CurrentHitPoints, HitDice, ProficiencyBonus, CharacterNotes, FeaturesTraits, Equipments) 
        //        VALUES (@UserID, @CharacterName, @Class, @Level, @Race, @Background, @ArmorClass, @Initiative, @Speed, @HitPointMax, 
        //        @CurrentHitPoints, @HitDice, @ProficiencyBonus, @CharacterNotes, @FeaturesTraits, @Equipments);
        //        SELECT SCOPE_IDENTITY();"; // Get new CharacterID

        //            using (SqlCommand cmd = new SqlCommand(insertCharacterQuery, conn, transaction))
        //            {
        //                cmd.Parameters.AddWithValue("@UserID", userID);
        //                cmd.Parameters.AddWithValue("@CharacterName", charName);
        //                cmd.Parameters.AddWithValue("@Class", charClass);
        //                cmd.Parameters.AddWithValue("@Level", level);
        //                cmd.Parameters.AddWithValue("@Race", race);
        //                cmd.Parameters.AddWithValue("@Background", background);
        //                cmd.Parameters.AddWithValue("@ArmorClass", armorClass);
        //                cmd.Parameters.AddWithValue("@Initiative", initiative);
        //                cmd.Parameters.AddWithValue("@Speed", speed);
        //                cmd.Parameters.AddWithValue("@HitPointMax", hitPointMax);
        //                cmd.Parameters.AddWithValue("@CurrentHitPoints", currentHitPoints);
        //                cmd.Parameters.AddWithValue("@HitDice", hitDice);
        //                cmd.Parameters.AddWithValue("@ProficiencyBonus", proficiencyBonus);
        //                cmd.Parameters.AddWithValue("@CharacterNotes", characterNotes);
        //                cmd.Parameters.AddWithValue("@FeaturesTraits", featuresTraits);
        //                cmd.Parameters.AddWithValue("@Equipments", equipments);

        //                int characterID = Convert.ToInt32(cmd.ExecuteScalar()); // Get inserted CharacterID

        //                string query = @"
        //                INSERT INTO Abilities (AbilityName, AbilityScore, Selected) VALUES 
        //                ('Strength', @Strength, @StrengthBool),
        //                ('Dexterity', @Dexterity, @DexterityBool),
        //                ('Constitution', @Constitution, @ConstitutionBool),
        //                ('Intelligence', @Intelligence, @IntelligenceBool),
        //                ('Wisdom', @Wisdom, @WisdomBool),
        //                ('Charisma', @Charisma, @CharismaBool);";


        //                using (SqlCommand abilitycmd = new SqlCommand(query, conn))
        //                {
        //                    abilitycmd.Parameters.AddWithValue("@Strength", Convert.ToInt32(textBox3.Text));
        //                    abilitycmd.Parameters.AddWithValue("@Dexterity", Convert.ToInt32(textBox4.Text));
        //                    abilitycmd.Parameters.AddWithValue("@Constitution", Convert.ToInt32(textBox5.Text));
        //                    abilitycmd.Parameters.AddWithValue("@Intelligence", Convert.ToInt32(textBox6.Text));
        //                    abilitycmd.Parameters.AddWithValue("@Wisdom", Convert.ToInt32(textBox7.Text));
        //                    abilitycmd.Parameters.AddWithValue("@Charisma", Convert.ToInt32(textBox8.Text));
        //                    abilitycmd.Parameters.AddWithValue("@StrengthBool", strengthbool);
        //                    abilitycmd.Parameters.AddWithValue("@DexterityBool", dexteritybool);
        //                    abilitycmd.Parameters.AddWithValue("@ConstitutionBool", constitutionbool);
        //                    abilitycmd.Parameters.AddWithValue("@IntelligenceBool", intelligencebool);
        //                    abilitycmd.Parameters.AddWithValue("@WisdomBool", wisdombool);
        //                    abilitycmd.Parameters.AddWithValue("@CharismaBool", charismabool);

        //                    abilitycmd.ExecuteNonQuery();
        //                }

        //                string insertAbilitiesQuery = @"
        //                INSERT INTO Character_Abilities (CharacterID, AbilityID)
        //                VALUES 
        //                (@CharacterID, (SELECT AbilityID FROM Abilities WHERE AbilityName = 'Strength')),
        //                (@CharacterID, (SELECT AbilityID FROM Abilities WHERE AbilityName = 'Dexterity')),
        //                (@CharacterID, (SELECT AbilityID FROM Abilities WHERE AbilityName = 'Constitution')),
        //                (@CharacterID, (SELECT AbilityID FROM Abilities WHERE AbilityName = 'Intelligence')),
        //                (@CharacterID, (SELECT AbilityID FROM Abilities WHERE AbilityName = 'Wisdom')),
        //                (@CharacterID, (SELECT AbilityID FROM Abilities WHERE AbilityName = 'Charisma'));";

        //                using (SqlCommand charcmd = new SqlCommand(insertAbilitiesQuery, conn))
        //                {
        //                    charcmd.Parameters.AddWithValue("@CharacterID", characterID);
        //                    charcmd.ExecuteNonQuery();
        //                }


        //                string query2 = @"
        //                INSERT INTO Character_Skills (CharacterID, SkillID, HasProficiency) VALUES 
        //                (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Acrobatics'), @Acrobatics),
        //                (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Animal Handling'), @AnimalHandling),
        //                (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Arcana'), @Arcana),
        //                (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Deception'), @Deception),
        //                (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'History'), @History),
        //                (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Insight'), @Insight),
        //                (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Intimidation'), @Intimidation),
        //                (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Investigation'), @Investigation),
        //                (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Medicine'), @Medicine),
        //                (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Nature'), @Nature),
        //                (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Perception'), @Perception),
        //                (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Performance'), @Performance),
        //                (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Persuasion'), @Persuasion),
        //                (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Religion'), @Religion),
        //                (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Sleight of Hand'), @SleightOfHand),
        //                (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Stealth'), @Stealth),
        //                (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Survival'), @Survival);";

        //                using (SqlCommand skillcmd = new SqlCommand(query2, conn))
        //                {
        //                    skillcmd.Parameters.AddWithValue("@CharacterID", characterID);
        //                    skillcmd.Parameters.AddWithValue("@Acrobatics", acrobatics);
        //                    skillcmd.Parameters.AddWithValue("@AnimalHandling", animalHandling);
        //                    skillcmd.Parameters.AddWithValue("@Arcana", arcana);
        //                    skillcmd.Parameters.AddWithValue("@Deception", deception);
        //                    skillcmd.Parameters.AddWithValue("@History", history);
        //                    skillcmd.Parameters.AddWithValue("@Insight", insight);
        //                    skillcmd.Parameters.AddWithValue("@Intimidation", intimidation);
        //                    skillcmd.Parameters.AddWithValue("@Investigation", investigation);
        //                    skillcmd.Parameters.AddWithValue("@Medicine", medicine);
        //                    skillcmd.Parameters.AddWithValue("@Nature", nature);
        //                    skillcmd.Parameters.AddWithValue("@Perception", perception);
        //                    skillcmd.Parameters.AddWithValue("@Performance", performance);
        //                    skillcmd.Parameters.AddWithValue("@Persuasion", persuasion);
        //                    skillcmd.Parameters.AddWithValue("@Religion", religion);
        //                    skillcmd.Parameters.AddWithValue("@SleightOfHand", sleightOfHand);
        //                    skillcmd.Parameters.AddWithValue("@Stealth", stealth);
        //                    skillcmd.Parameters.AddWithValue("@Survival", survival);

        //                    skillcmd.ExecuteNonQuery();
        //                }

        //                //transaction.Commit(); // Commit transaction
        //                MessageBox.Show("Character added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            //transaction.Rollback(); // Rollback transaction on failure
        //            MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}

        private void addCharacter_Click(object sender, EventArgs e)
        {
            string dbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db_connection.txt");
            string connectionString = File.ReadAllText(dbFilePath).Trim();

            // Get input values
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
            string otherProficiency = ""; // Add if you have this field

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



        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Data.SqlClient;
//using System.Diagnostics;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.IO;

//namespace DnD_Project
//{
//    public partial class CharacterSheet : Form
//    {
//        private Form previousForm; // Store the reference to the previous form
//        private int userid;
//        public CharacterSheet()
//        {
//            InitializeComponent();
//        }

//        // Constructor accepts previous form as parameter
//        public CharacterSheet(Form callerForm, int userid)
//        {
//            InitializeComponent();
//            this.previousForm = callerForm; // Store reference
//            this.userid = userid;
//        }

//        private void label1_Click(object sender, EventArgs e) { }

//        private void groupBox11_Enter(object sender, EventArgs e) { }

//        private void SavingThrows_CheckedChanged(object sender, EventArgs e)
//        {
//            // Get all checkboxes inside the GroupBox
//            var checkboxes = groupBox11.Controls.OfType<CheckBox>();

//            // Count how many checkboxes are checked
//            int checkedCount = checkboxes.Count(cb => cb.Checked);

//            // Disable unselected checkboxes if 2 are already checked
//            if (checkedCount >= 2)
//            {
//                foreach (var checkbox in checkboxes)
//                {
//                    if (!checkbox.Checked)
//                        checkbox.Enabled = false;
//                }
//            }
//            else
//            {
//                // Re-enable all checkboxes if less than 2 are checked
//                foreach (var checkbox in checkboxes)
//                {
//                    checkbox.Enabled = true;
//                }
//            }
//        }

//        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
//            {
//                e.Handled = true; // Blocks non-numeric input
//            }
//        }

//        private void button1_Click(object sender, EventArgs e) //Go back Button
//        {
//            this.Hide();
//            previousForm.Show();
//        }

//        private void CharacterSheet_Load(object sender, EventArgs e) { }

//        private void textBox1_TextChanged(object sender, EventArgs e) { }

//        private void addCharacter_Click(object sender, EventArgs e)
//        {
//            string dbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db_connection.txt");
//            string connectionString = File.ReadAllText(dbFilePath).Trim();

//            // Get input values
//            int userID = this.userid; // Fetch the currently logged-in user's ID
//            string charName = textBox1.Text;
//            string charClass = textBox2.Text;
//            int level = Convert.ToInt32(comboBox2.SelectedItem);
//            string race = textBox33.Text;
//            string background = textBox34.Text;
//            int armorClass = Convert.ToInt32(textBox36.Text);
//            int initiative = Convert.ToInt32(textBox35.Text);
//            int speed = Convert.ToInt32(textBox37.Text);
//            int hitPointMax = Convert.ToInt32(numericUpDown2.Value);
//            int currentHitPoints = Convert.ToInt32(textBox38.Text);
//            string hitDice = (comboBox1.SelectedItem.ToString());
//            int proficiencyBonus = 0;
//            string characterNotes = textBox52.Text;
//            string featuresTraits = textBox51.Text;
//            string equipments = textBox50.Text;
//            int strength = Convert.ToInt32(textBox3.Text);
//            int dexterity = Convert.ToInt32(textBox4.Text);
//            int Constitution = Convert.ToInt32(textBox5.Text);
//            int Intelligence = Convert.ToInt32(textBox6.Text);
//            int Wisdom = Convert.ToInt32(textBox7.Text);
//            int Charisma = Convert.ToInt32(textBox8.Text);

//            bool strengthbool = checkBox1.Checked;
//            bool dexteritybool = checkBox2.Checked;
//            bool constitutionbool = checkBox3.Checked;
//            bool intelligencebool = checkBox4.Checked;
//            bool wisdombool = checkBox5.Checked;
//            bool charismabool = checkBox6.Checked;

//            // Initialize all skill booleans
//            bool acrobatics = checkBox7.Checked;
//            bool animalHandling = checkBox8.Checked;
//            bool arcana = checkBox9.Checked;
//            bool deception = checkBox10.Checked;
//            bool history = checkBox11.Checked;
//            bool insight = checkBox12.Checked;
//            bool intimidation = checkBox13.Checked;
//            bool investigation = checkBox14.Checked;
//            bool medicine = checkBox15.Checked;
//            bool nature = checkBox16.Checked;
//            bool perception = checkBox17.Checked;
//            bool performance = checkBox18.Checked;
//            bool persuasion = checkBox19.Checked;
//            bool religion = checkBox20.Checked;
//            bool sleightOfHand = checkBox21.Checked;
//            bool stealth = checkBox22.Checked;
//            bool survival = checkBox23.Checked;

//            // Validate required fields
//            if (string.IsNullOrWhiteSpace(charName) || string.IsNullOrWhiteSpace(charClass) || level <= 0)
//            {
//                MessageBox.Show("Character Name, Class, and Level are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                try
//                {
//                    conn.Open();

//                    // **Step 1: Insert into Characters Table**
//                    string insertCharacterQuery = @"
//                    INSERT INTO Characters (UserID, CharacterName, Class, Level, Race, Background, ArmorClass, Initiative, Speed, HitPointMax, 
//                    CurrentHitPoints, HitDice, ProficiencyBonus, CharacterNotes, FeaturesTraits, Equipments) 
//                    VALUES (@UserID, @CharacterName, @Class, @Level, @Race, @Background, @ArmorClass, @Initiative, @Speed, @HitPointMax, 
//                    @CurrentHitPoints, @HitDice, @ProficiencyBonus, @CharacterNotes, @FeaturesTraits, @Equipments);
//                    SELECT SCOPE_IDENTITY();";

//                    int characterID;
//                    using (SqlCommand cmd = new SqlCommand(insertCharacterQuery, conn))
//                    {
//                        cmd.Parameters.AddWithValue("@UserID", userID);
//                        cmd.Parameters.AddWithValue("@CharacterName", charName);
//                        cmd.Parameters.AddWithValue("@Class", charClass);
//                        cmd.Parameters.AddWithValue("@Level", level);
//                        cmd.Parameters.AddWithValue("@Race", race);
//                        cmd.Parameters.AddWithValue("@Background", background);
//                        cmd.Parameters.AddWithValue("@ArmorClass", armorClass);
//                        cmd.Parameters.AddWithValue("@Initiative", initiative);
//                        cmd.Parameters.AddWithValue("@Speed", speed);
//                        cmd.Parameters.AddWithValue("@HitPointMax", hitPointMax);
//                        cmd.Parameters.AddWithValue("@CurrentHitPoints", currentHitPoints);
//                        cmd.Parameters.AddWithValue("@HitDice", hitDice);
//                        cmd.Parameters.AddWithValue("@ProficiencyBonus", proficiencyBonus);
//                        cmd.Parameters.AddWithValue("@CharacterNotes", characterNotes);
//                        cmd.Parameters.AddWithValue("@FeaturesTraits", featuresTraits);
//                        cmd.Parameters.AddWithValue("@Equipments", equipments);

//                        characterID = Convert.ToInt32(cmd.ExecuteScalar());
//                    }

//                    // Insert abilities
//                    string insertAbilitiesQuery = @"
//                    INSERT INTO Abilities (AbilityName, AbilityScore, Selected) VALUES 
//                    ('Strength', @Strength, @StrengthBool),
//                    ('Dexterity', @Dexterity, @DexterityBool),
//                    ('Constitution', @Constitution, @ConstitutionBool),
//                    ('Intelligence', @Intelligence, @IntelligenceBool),
//                    ('Wisdom', @Wisdom, @WisdomBool),
//                    ('Charisma', @Charisma, @CharismaBool)";

//                    using (SqlCommand abilityCmd = new SqlCommand(insertAbilitiesQuery, conn))
//                    {
//                        abilityCmd.Parameters.AddWithValue("@Strength", strength);
//                        abilityCmd.Parameters.AddWithValue("@Dexterity", dexterity);
//                        abilityCmd.Parameters.AddWithValue("@Constitution", Constitution);
//                        abilityCmd.Parameters.AddWithValue("@Intelligence", Intelligence);
//                        abilityCmd.Parameters.AddWithValue("@Wisdom", Wisdom);
//                        abilityCmd.Parameters.AddWithValue("@Charisma", Charisma);
//                        abilityCmd.Parameters.AddWithValue("@StrengthBool", strengthbool);
//                        abilityCmd.Parameters.AddWithValue("@DexterityBool", dexteritybool);
//                        abilityCmd.Parameters.AddWithValue("@ConstitutionBool", constitutionbool);
//                        abilityCmd.Parameters.AddWithValue("@IntelligenceBool", intelligencebool);
//                        abilityCmd.Parameters.AddWithValue("@WisdomBool", wisdombool);
//                        abilityCmd.Parameters.AddWithValue("@CharismaBool", charismabool);

//                        abilityCmd.ExecuteNonQuery();
//                    }

//                    // Link abilities to character
//                    string linkAbilitiesQuery = @"
//                    INSERT INTO Character_Abilities (CharacterID, AbilityID)
//                    VALUES 
//                    (@CharacterID, (SELECT AbilityID FROM Abilities WHERE AbilityName = 'Strength')),
//                    (@CharacterID, (SELECT AbilityID FROM Abilities WHERE AbilityName = 'Dexterity')),
//                    (@CharacterID, (SELECT AbilityID FROM Abilities WHERE AbilityName = 'Constitution')),
//                    (@CharacterID, (SELECT AbilityID FROM Abilities WHERE AbilityName = 'Intelligence')),
//                    (@CharacterID, (SELECT AbilityID FROM Abilities WHERE AbilityName = 'Wisdom')),
//                    (@CharacterID, (SELECT AbilityID FROM Abilities WHERE AbilityName = 'Charisma'));";

//                    using (SqlCommand linkAbilitiesCmd = new SqlCommand(linkAbilitiesQuery, conn))
//                    {
//                        linkAbilitiesCmd.Parameters.AddWithValue("@CharacterID", characterID);
//                        linkAbilitiesCmd.ExecuteNonQuery();
//                    }

//                    // Insert skills
//                    string insertSkillsQuery = @"
//                    INSERT INTO Character_Skills (CharacterID, SkillID, HasProficiency) VALUES 
//                    (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Acrobatics'), @Acrobatics),
//                    (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Animal Handling'), @AnimalHandling),
//                    (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Arcana'), @Arcana),
//                    (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Deception'), @Deception),
//                    (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'History'), @History),
//                    (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Insight'), @Insight),
//                    (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Intimidation'), @Intimidation),
//                    (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Investigation'), @Investigation),
//                    (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Medicine'), @Medicine),
//                    (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Nature'), @Nature),
//                    (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Perception'), @Perception),
//                    (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Performance'), @Performance),
//                    (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Persuasion'), @Persuasion),
//                    (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Religion'), @Religion),
//                    (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Sleight of Hand'), @SleightOfHand),
//                    (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Stealth'), @Stealth),
//                    (@CharacterID, (SELECT SkillID FROM Skills WHERE SkillName = 'Survival'), @Survival);";

//                    using (SqlCommand skillsCmd = new SqlCommand(insertSkillsQuery, conn))
//                    {
//                        skillsCmd.Parameters.AddWithValue("@CharacterID", characterID);
//                        skillsCmd.Parameters.AddWithValue("@Acrobatics", acrobatics);
//                        skillsCmd.Parameters.AddWithValue("@AnimalHandling", animalHandling);
//                        skillsCmd.Parameters.AddWithValue("@Arcana", arcana);
//                        skillsCmd.Parameters.AddWithValue("@Deception", deception);
//                        skillsCmd.Parameters.AddWithValue("@History", history);
//                        skillsCmd.Parameters.AddWithValue("@Insight", insight);
//                        skillsCmd.Parameters.AddWithValue("@Intimidation", intimidation);
//                        skillsCmd.Parameters.AddWithValue("@Investigation", investigation);
//                        skillsCmd.Parameters.AddWithValue("@Medicine", medicine);
//                        skillsCmd.Parameters.AddWithValue("@Nature", nature);
//                        skillsCmd.Parameters.AddWithValue("@Perception", perception);
//                        skillsCmd.Parameters.AddWithValue("@Performance", performance);
//                        skillsCmd.Parameters.AddWithValue("@Persuasion", persuasion);
//                        skillsCmd.Parameters.AddWithValue("@Religion", religion);
//                        skillsCmd.Parameters.AddWithValue("@SleightOfHand", sleightOfHand);
//                        skillsCmd.Parameters.AddWithValue("@Stealth", stealth);
//                        skillsCmd.Parameters.AddWithValue("@Survival", survival);

//                        skillsCmd.ExecuteNonQuery();
//                    }

//                    MessageBox.Show("Character added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void textBox3_TextChanged(object sender, EventArgs e) { }
//    }
//}
