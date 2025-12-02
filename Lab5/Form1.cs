namespace Lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /* Name:Farouk Olatunji
         * Date: November 12 2025
         * This program rolls one dice or calculates mark stats.
         * Link to your repo in GitHub: https://github.com/farouq363/olatunjiLab5/tree/master 
         * */

        //class-level random object
        Random rand = new Random();

        private void Form1_Load(object sender, EventArgs e)
        {
            //select one roll radiobutton
            radOneRoll.Checked = true;

            //add your name to end of form title
            this.Text += "Farouk Olatunji";

        } // end form load

        private void btnClear_Click(object sender, EventArgs e)
        {
            //call the function
            ClearOneRoll();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //call the function
            ClearStats();

        }

        private void btnRollDice_Click(object sender, EventArgs e)
        {
            int dice1, dice2;
            //call ftn RollDice, placing returned number into integers
            dice1 = RollDice();
            dice2 = RollDice();

            //place integers into labels
            lblDice1.Text = dice1.ToString();
            lblDice2.Text = dice2.ToString();

            // call ftn GetName sending total and returning name
            string name = GetName(dice2, dice1);

            //display name in label
            lblRollName.Text = name;

        }

        /* Name: ClearOneRoll
        *  Sent: nothing
        *  Return: nothing
        *  Clear the labels */
        private void ClearOneRoll()
        {
            lblDice1.Text = "";
            lblDice2.Text = "";
            lblRollName.Text = "";
        }


        /* Name: ClearStats
        *  Sent: nothing
        *  Return: nothing
        *  Reset nud to minimum value, chkbox unselected, 
        *  clear labels and listbox */
        private void ClearStats()
        {
            lblPass.Text = "";
            lblFail.Text = "";
            lblAverage.Text = "";
            lstMarks.Items.Clear();
            chkSeed.Checked = false;
        }


        /* Name: RollDice
        * Sent: nothing
        * Return: integer (1-6)
        * Simulates rolling one dice */
        private int RollDice()
        {
            return rand.Next(1, 7);
        }


        /* Name: GetName
        * Sent: 1 integer (total of dice1 and dice2) 
        * Return: string (name associated with total) 
        * Finds the name of dice roll based on total.
        * Use a switch statement with one return only
        * Names: 2 = Snake Eyes
        *        3 = Litle Joe
        *        5 = Fever
        *        7 = Most Common
        *        9 = Center Field
        *        11 = Yo-leven
        *        12 = Boxcars
        * Anything else = No special name*/
        private string GetName(int firstNumber, int secondNumber)
        {
            int total = firstNumber + secondNumber;
            string result;

            switch (total)
            {
                case 2:
                    result = "Snake Eyes";
                    break;
                case 3:
                    result = "Little Joe";
                    break;
                case 5:
                    result = "Fever";
                    break;
                case 7:
                    result = "Most Common";
                    break;
                case 9:
                    result = "Center Field";
                    break;
                case 11:
                    result = "Yo-leven";
                    break;
                case 12:
                    result = "Boxcars";
                    break;
                default:
                    result = "No special";
                    break;
            }

            return result;
        }


        private void btnSwapNumbers_Click(object sender, EventArgs e)
        {
            //call ftn DataPresent twice sending string returning boolean
            bool _lblDice1 = DataPresent(lblDice1);
            bool _lblDice2 = DataPresent(lblDice2);

            //if data present in both labels, call SwapData sending both strings

            //put data back into labels

            //if data not present in either label display error msg

            if (_lblDice1 == true && _lblDice2 == true)
            {
                string lbldice1 = lblDice1.Text;
                string lbldice2 = lblDice2.Text;

                SwapData(ref lbldice1, ref lbldice2);
                //put data back into labels
                lblDice1.Text = lbldice1;
                lblDice2.Text = lbldice2;

            }
            else
            {
                MessageBox.Show("Roll the dice", "Data Missing");
            }

        }

        /* Name: DataPresent
        * Sent: string
        * Return: bool (true if data, false if not) 
        * See if string is empty or not*/
        private bool DataPresent(Label label)
        {
            return !(string.IsNullOrEmpty(label.Text));
        }


        /* Name: SwapData
        * Sent: 2 strings
        * Return: none 
        * Swaps the memory locations of two strings*/
        private void SwapData(ref string str1, ref string str2)
        {
            string temp = str1;
            str1 = str2;
            str2 = temp;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //declare variables and array
            int size = (int)nudNumber.Value;
            int[] marks = new int[size];

            //check if seed value
            if (chkSeed.Checked)
            {
                rand = new Random(1000);
            }
            lstMarks.Items.Clear();

            //fill array using random number
            int i = 0;
            while (i < size)
            {
                marks[i] = rand.Next(40, 101);
                lstMarks.Items.Add(marks[i]);
                i++;
            }

            //call CalcStats sending and returning data
            CalcStats(out double average, out int passNumber, out int failNumber, marks);

            //display data sent back in labels - average, pass and fail
            lblPass.Text = passNumber.ToString();
            lblFail.Text = failNumber.ToString();
            // Format average always showing 2 decimal places 
            lblAverage.Text =  average.ToString("f2");
        } // end Generate click

        /* Name: CalcStats
        * Sent: array and 2 integers
        * Return: average (double) 
        * Run a foreach loop through the array.
        * Passmark is 60%
        * Calculate average and count how many marks pass and fail
        * The pass and fail values must also get returned for display*/
        private void CalcStats(out double average, out int pass, out int fail, int[] marks)
        {
            int sum = 0;
            pass = 0;
            fail = 0;

            foreach (int mark in marks)
            {
                sum += mark;
                if (mark >= 50)
                {
                    pass++;
                }
                else
                {
                    fail++;
                }
            }

            average = (double)sum / marks.Length;
        }

        private void radOneRoll_CheckedChanged(object sender, EventArgs e)
        {

            //radRollStats.Checked = false;
            grpMarkStats.Hide();


            //radOneRoll.Checked = true;
            grpOneRoll.Show();
        }

        private void radRollStats_CheckedChanged(object sender, EventArgs e)
        {

            //radOneRoll.Checked = false;
            grpOneRoll.Hide();

            //radRollStats.Checked = true;
            grpMarkStats.Show();
        }
        private bool isHandlingCheckChanged = false;

        private void chkSeed_CheckedChanged(object sender, EventArgs e)
        {
            if (isHandlingCheckChanged) return;

            if (chkSeed.Checked)
            {
                isHandlingCheckChanged = true;

                DialogResult result = MessageBox.Show( "Are you sure you want to seed value?","Confirm Seed value",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    chkSeed.Checked = true;
                }
                else
                {
                    chkSeed.Checked = false;
                }

                isHandlingCheckChanged = false;
            }
        }
    }
}
