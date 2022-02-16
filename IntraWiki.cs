using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntraWiki
{
    public partial class IntraWiki : Form
    {
        public IntraWiki()
        {
            InitializeComponent();
        }

        static int rowSize = 12;
        static int colSize = 5;
        static string[,] myArray = new string[rowSize, colSize];

       

        private void DisplayArray()
        {
            listBoxWki.Items.Clear();
          /*  for(int i=0; i< 1; i++)
            {
                listBoxWki.Items.Add("List of items");
                listBoxWki.Items.Add("------------------------");
            }*/
            for (int x = 0; x < rowSize; x++)
            {
                string oneLine = "";
                oneLine = oneLine + " " + myArray[x, 0];
                listBoxWki.Items.Add(oneLine);
            }
        }

        private void populateTitle()
        {
            myArray[0, 0] = "Array";
            myArray[1, 0] = "Two Dimension Array";
            myArray[2, 0] = "List";
            myArray[3, 0] = "Linked list";
            myArray[4, 0] = "Self - Balance Tree";
            myArray[5, 0] = "Heaps";
            myArray[6, 0] = "Binary Search Tree";
            myArray[7, 0] = "Graph";
            myArray[8, 0] = "Set";
            myArray[9, 0] = "Queue";
            myArray[10, 0] = "Stack";
            myArray[11, 0] = "Hash Table";
            for(int i=0; i< rowSize; i++)
            {
                myArray[0, i] = myArray[i, 0];
            }

        }

        private void IntraWiki_Load(object sender, EventArgs e)
        {
            myArray[0, 0] = "Array";
            myArray[1, 0] = "Two Dimension Array";
            myArray[2, 0] = "List";
            myArray[3, 0] = "Linked list";
            myArray[4, 0] = "Self - Balance Tree";
            myArray[5, 0] = "Heaps";
            myArray[6, 0] = "Binary Search Tree";
            myArray[7, 0] = "Graph";
            myArray[8, 0] = "Set";
            myArray[9, 0] = "Queue";
            myArray[10, 0] = "Stack";
            myArray[11, 0] = "Hash Table";
            DisplayArray();
        }
    }
}
