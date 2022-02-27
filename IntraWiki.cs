using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace IntraWiki
{
    public partial class IntraWiki : Form
    {
        #region Construct and Global Variable
        public IntraWiki()
        {
            InitializeComponent();
        }

        static int rowSize = 12;
        static int colSize = 4;
        static string[,] myArray = new string[rowSize, colSize];

        string defaultFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Definitions.bin");
        string currentFile;       //Setting global variable as filename, can be used for saving or renaming file while saving
        static int counter;
        #endregion

        #region Display,Application auto Load, Data Auto load
        private void DisplayArray()
        {
            listBoxWiki.Items.Clear();
            listBoxCategory.Items.Clear();

            for (int x = 0; x < rowSize; x++)
            {
                if ((string.IsNullOrEmpty(myArray[x, 0]) || string.IsNullOrEmpty(myArray[x, 1])))   //Will not add to the list box if the array element is empty or null.
                {
                    return;
                }
                listBoxWiki.Items.Add(myArray[x, 0]);
                listBoxCategory.Items.Add(myArray[x, 1]);

            }
        }

        private void sortByColumn()
        {
            int num = 0;
            for (int rowX = 0; rowX < rowSize; rowX++)
            {
                for (int j = rowX + 1; j < rowSize; j++)
                {
                    if ((myArray[rowX, num].CompareTo(myArray[j, num]) > 0))
                    {
                        string temp = myArray[rowX, num];
                        myArray[rowX, num] = myArray[j, num];
                        myArray[j, num] = temp;

                        for (int k = 1; k < colSize; k++)                       // sort the other column respective to their rows sorted
                        {
                            string temp1 = myArray[rowX, k];
                            myArray[rowX, k] = myArray[j, k];
                            myArray[j, k] = temp1;
                        }
                    }
                }
            }
        }

        private void IntraWiki_Load(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialogVG = new OpenFileDialog();
            openFileDialogVG.InitialDirectory = defaultFileName;
            openFileDialogVG.Filter = "BIN Files|*.bin";
            openFileDialogVG.Title = "Select a BIN File";
            if (openFileDialogVG.ShowDialog() == DialogResult.OK)
            {
                currentFile = openFileDialogVG.FileName;                   
                openRecord(currentFile);
                toolStripStatusLabel1.Text = currentFile + " is Opened";
            }
            else
            {
                for (int x = 0; x < rowSize; x++)
                {
                    myArray[x, 0] = "";
                    myArray[x, 1] = String.Empty;
                    myArray[x, 2] = String.Empty;
                    myArray[x, 3] = String.Empty;
                }
            }
            sortByColumn();
            DisplayArray();
        }

        private void openRecord(string openFileName)
        {
            try
            {
                using (Stream stream = File.Open(openFileName, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    for (int y = 0; y < colSize; y++)
                    {
                        for (int x = 0; x < rowSize; x++)
                        {
                            myArray[x, y] = (string)bin.Deserialize(stream);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            DisplayArray();
        }

        private void saveRecord(string saveFileName)
        {
            try
            {
                using (Stream stream = File.Open(saveFileName, FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    for (int y = 0; y < colSize; y++)
                    {
                        for (int x = 0; x < rowSize; x++)
                        {
                            bin.Serialize(stream, myArray[x, y]);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonAutoLoadData_Click(object sender, EventArgs e)
        {
            myArray[0, 0] = "Array"; myArray[0, 1] = "Array"; myArray[0, 2] = "Linear"; myArray[0, 3] = "An array is a collection of elements of the same type placed in contiguous memory locations that can be individually referenced by using an index to a unique identifier.";
            myArray[1, 0] = "Two Dimension Array"; myArray[1, 1] = "Array"; myArray[1, 2] = "Linear"; myArray[1, 3] = "Arrays can have more than one dimension.The multidimensional array can be declared by adding commas in the square brackets. For example, [,] declares two-dimensional array";
            myArray[2, 0] = "List"; myArray[2, 1] = "List"; myArray[2, 2] = "Linear"; myArray[2, 3] = "The List<T> is a collection of strongly typed objects that can be accessed by index and having methods for sorting, searching, and modifying list. It is the generic version of the ArrayList that comes under System.Collection.Generic namespace.";
            myArray[3, 0] = "Linked list"; myArray[3, 1] = "List"; myArray[3, 2] = "Linear"; myArray[3, 3] = "A LinkedList is a linear data structure which stores element in the non-contiguous location. The elements in a linked list are linked with each other using pointers. Or in other words, LinkedList consists of nodes where each node contains a data field and a reference(link) to the next node in the list.";
            myArray[4, 0] = "Self - Balance Tree"; myArray[4, 1] = "Tree"; myArray[4, 2] = "Non-Linear"; myArray[4, 3] = " A self-balancing binary search tree (BST) is any node-based binary search tree that automatically keeps its height (maximal number of levels below the root) small in the face of arbitrary item insertions and deletions.";
            myArray[5, 0] = "Heaps"; myArray[5, 1] = "Tree"; myArray[5, 2] = "Non-Linear"; myArray[5, 3] = "A Heap is a special Tree-based data structure in which the tree is a complete binary tree. Generally, Heaps can be of two types: Max-Heap: In a Max-Heap the key present at the root node must be greatest among the keys present at all of it's children.";
            myArray[6, 0] = "Binary Search Tree"; myArray[6, 1] = "Tree"; myArray[6, 2] = "Non-Linear"; myArray[6, 3] = "A binary search tree (BST) is a binary tree where each node has a Comparable key (and an associated value) and satisfies the restriction that the key in any node is larger than the keys in all nodes in that node's left subtree and smaller than the keys in all nodes in that node's right subtree.";
            myArray[7, 0] = "Graph"; myArray[7, 1] = "Graphs"; myArray[7, 2] = "Non-Linear"; myArray[7, 3] = "A Graph is a non-linear data structure consisting of nodes and edges. The nodes are sometimes also referred to as vertices and the edges are lines or arcs that connect any two nodes in the graph";
            myArray[8, 0] = "Set"; myArray[8, 1] = "Abstract"; myArray[8, 2] = "Non-Linear"; myArray[8, 3] = "A Graph is a non-linear data structure consisting of nodes and edges. The nodes are sometimes also referred to as vertices and the edges are lines or arcs that connect any two nodes in the graph";
            myArray[9, 0] = "Queue"; myArray[9, 1] = "Abstract"; myArray[9, 2] = "Linear"; myArray[9, 3] = "Queue is a special type of collection that stores the elements in FIFO style (First In First Out), exactly opposite of the Stack<T> collection. It contains the elements in the order they were added. C# includes generic Queue<T> and non-generic Queue collection. It is recommended to use the generic Queue<T> collection.";
            myArray[10, 0] = "Stack"; myArray[10, 1] = "Abstract"; myArray[10, 2] = "Linear"; myArray[10, 3] = "Stack is a special type of collection that stores elements in LIFO style (Last In First Out). C# includes the generic Stack<T> and non-generic Stack collection classes. It is recommended to use the generic Stack<T> collection.Stack is useful to store temporary data in LIFO style, and you might want to delete an element after retrieving its value.";
            myArray[11, 0] = "Hash Table"; myArray[11, 1] = "Hash"; myArray[11, 2] = "Non-Linear"; myArray[11, 3] = "Stack is a special type of collection that stores elements in LIFO style (Last In First Out). C# includes the generic Stack<T> and non-generic Stack collection classes. It is recommended to use the generic Stack<T> collection.Stack is useful to store temporary data in LIFO style, and you might want to delete an element after retrieving its value.";
          
            sortByColumn();
            DisplayArray();
            toolStripStatusLabel1.Text = "DATA IS AUTO-LOADED";
        }


        #endregion

        #region ListBox Display 

        private void listBoxWki_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxWiki.SelectedIndex == -1)
            {
                toolStripStatusLabel1.Text = "! Element in the List Not selected";
            }
            else
            {
                int currentIndex = listBoxWiki.SelectedIndex;
                textBoxName.Text = myArray[currentIndex, 0];
                toolStripStatusLabel1.Text = String.Format("\"{0}\" is Selected from Row {1} ", textBoxName.Text, currentIndex + 1);
                textBoxCategory.Text = myArray[currentIndex, 1];
                textBoxStructure.Text = myArray[currentIndex, 2];
                textBoxDefinition.Text = myArray[currentIndex, 3];
            }
        }
        #endregion

        #region File Handling
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenBinary = new OpenFileDialog();
            OpenBinary.InitialDirectory = defaultFileName;
            //defaultFileName = OpenBinary.FileName;
            OpenBinary.Filter = "BIN Files|*.bin";
            OpenBinary.Title = "Select a BIN File";

            if (OpenBinary.ShowDialog() == DialogResult.OK)
            {
                currentFile = OpenBinary.FileName;
                openRecord(currentFile);
                toolStripStatusLabel1.Text = currentFile + " is opened.";
            }

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveBinary = new SaveFileDialog();
            saveBinary.InitialDirectory = defaultFileName;
            saveBinary.Filter = "binary files (*.bin)|*.bin|All files (*.*)|*.*";            //setting so that user can save as binary files
            saveBinary.DefaultExt = "bin";                                                   //save the file as bin extension if all files is choosed.
            saveBinary.FileName = Path.GetFileName(currentFile);                             //set filename in the SaveFileDialog // getFileName extract the name only without path.
            
            DialogResult sr = saveBinary.ShowDialog();

            if (saveBinary.FileName != "")
            {
                saveRecord(currentFile);
            }
            else
            {
                saveRecord(defaultFileName);
            }
           
        }

        #endregion

        #region AddFunctions
        private void buttonAdd_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(textBoxName.Text) && !string.IsNullOrEmpty(textBoxCategory.Text) && !string.IsNullOrEmpty(textBoxStructure.Text) && !string.IsNullOrEmpty(textBoxDefinition.Text))
            {
                
                if (counter == rowSize)                                       // will show exception if added more than the size of 2d array.
                {
                    MessageBox.Show("CANNOT EXCEED MORE THAN 12 ROWS", "*User Information*", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                for (int x = 0; x < rowSize; x++)
                {
                   
                    if (myArray[x, 0] == "")
                    {
                        myArray[x, 0] = textBoxName.Text;
                        myArray[x, 1] = textBoxCategory.Text;
                        myArray[x, 2] = textBoxStructure.Text;
                        myArray[x, 3] = textBoxDefinition.Text;
                        var result = MessageBox.Show("Proceed with new Record?", "Add New Record",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.OK)
                        {
                          toolStripStatusLabel1.Text =  String.Format("\"{0}\" is Added to Row {1} ", textBoxName.Text, x + 1);
                            counter++;
                            break;
                        }
                        else
                        {
                            myArray[x, 0] = "";
                            myArray[x, 1] = "";
                            myArray[x, 2] = "";
                            myArray[x, 3] = "";
                            toolStripStatusLabel1.Text = "!User has cancel to Add";
                            break;
                        }

                    }
                }
                clearTextBox();
                DisplayArray();
            }
            else
            {
                toolStripStatusLabel1.Text = "Field cannot be left empty";
            }
           
            
        }

        private void clearTextBox()
        {
            textBoxName.Clear();
            textBoxCategory.Clear();
            textBoxStructure.Clear();
            textBoxDefinition.Clear();
            textBoxName.Focus();
        }

        private int identifyNextEligibleRow()                                      //useful for add button, if the element already present then it will return the next available array index.
        {
            for (int y = 0; y < colSize; y++)
            {
                for (int x = 0; x < rowSize; x++)
                {
                    if (string.IsNullOrEmpty(myArray[x, y]))           //when the program starts and file does not exist, it will create file at that time the value will be null.
                    {                                                  // As we saved string.Empty when saved the file before, it will help to detect the empty string and returns the available row index.
                        return x;
                    }
                }
            }
            return rowSize;                                        //if not able to find empty or null then return rowsize
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
      {
            errorProvider1.Clear();
        }

        private void textBoxCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            errorProvider1.Clear();
        }

        private void textBoxStructure_KeyPress(object sender, KeyPressEventArgs e)
        {
            errorProvider1.Clear();
        }

        private void textBoxDefinition_KeyPress(object sender, KeyPressEventArgs e)
        {
            errorProvider1.Clear();
        }


    }
    #endregion

}
