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

        static int rowCounter = 0;
        static int colCounter = 0;

        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Definitions.bin");
        string currentFile;       //Setting global variable as filename, can be used for saving or renaming file while saving
        #endregion

        #region Display,Application auto Load, Data Auto load
        private void DisplayArray()
        {
            listBoxWki.Items.Clear();
            for (int x = 0; x < rowSize; x++)
            {
                if ((string.IsNullOrEmpty(myArray[x, 0])))   //Will not add to the list box if the array element is empty or null.
                {
                    return;
                }
                string oneLine = "";
                oneLine = oneLine + " " + myArray[x, 0];
                listBoxWki.Items.Add(oneLine);
            }
        }

        private void sortName()
        {
            int num = 0;
            for (int rowX = 0; rowX < rowCounter; rowX++)
            {
                for (int j = rowX + 1; j < rowCounter; j++)
                {
                    if ((myArray[rowX, num].CompareTo(myArray[j, num]) > 0))
                    {
                        string temp = myArray[rowX, num];
                        myArray[rowX, num] = myArray[j, num];
                        myArray[j, num] = temp;

                        for (int k = 1; k <= colCounter; k++)                       // sort the other column respective to their rows sorted
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
            if (File.Exists(fileName))
            {
                try
                {
                    using (Stream stream = File.Open(fileName, FileMode.Open))      //by using it open the file and  close (it will do background work for us)
                    {
                        if (stream.Length == 0)                                     // if the file is empty just do nothing, to handle the error.
                        {
                            toolStripStatusLabel1.Text = fileName + "(empty) is opened";
                            return;
                        }
                        else                                                       //Only deserialize if the file has binary content
                        {
                            BinaryFormatter bin = new BinaryFormatter();
                            {
                                for (int y = 0; y < colSize; y++)
                                {
                                    for (int x = 0; x < rowSize; x++)
                                    {
                                        myArray[x, y] = bin.Deserialize(stream).ToString();
                                    }
                                }
                            }
                        }
                    }

                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                toolStripStatusLabel1.Text = fileName + " is opened";
            }
            else
            {
                try
                {
                    using (Stream stream = File.Open(fileName, FileMode.Create))      //by using it open the file and  close (it will do background work for us)
                    {
                        toolStripStatusLabel1.Text = fileName + " is Created";
                    }

                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            DisplayArray();
            currentFile = fileName;
            // sortName();


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
            rowCounter = 11;
            colCounter = 3;
            DisplayArray();
        }


        #endregion

        #region ListBox Display 

        public int findIndex(string a)
        {
            for (int i = 0; i < rowSize; i++)
            {
                if (myArray[i, 0].Equals(a))
                {
                    return i;
                }
            }
            return -1;
        }

        private void listBoxWki_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBoxWki.SelectedIndex == -1)
            {
                toolStripStatusLabel1.Text = "! Element in the List Not selected";
            }
            else
            {
                string selectedItem = listBoxWki.SelectedItem.ToString().Trim();
                int b = findIndex(selectedItem);
                textBoxName.Text = myArray[b, 0];
                toolStripStatusLabel1.Text = String.Format("\"{0}\" is Selected from Row {1} ", selectedItem,b +1);
                textBoxCategory.Text = myArray[b, 1];
                textBoxStructure.Text = myArray[b, 2];
                textBoxDefinition.Text = myArray[b, 3];
            }
        }
        #endregion

        #region File Handling
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenBinary = new OpenFileDialog
            {
                InitialDirectory = fileName
            };                                                           //set the default filename as Rainbow.bin 
            DialogResult sr = OpenBinary.ShowDialog();
            if (sr == DialogResult.OK)
            {
                fileName = OpenBinary.FileName;
                currentFile = fileName;
                toolStripStatusLabel1.Text = fileName + " is opened.";
             
                try
                {
                    using (Stream stream = File.Open(fileName, FileMode.Open))      //by using it open the file and  close (it will do background work for us)
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        {
                            for (int y = 0; y < colCounter; y++)
                            {
                                for (int x = 0; x < rowCounter; x++)
                                {
                                    myArray[x, y] = bin.Deserialize(stream).ToString();
                                }
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
            else
            {
                return;                                                  //will exit dialog box without doing anything
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveBinary = new SaveFileDialog
            {
                InitialDirectory = fileName,
                Filter = "binary files (*.bin)|*.bin|All files (*.*)|*.*",               //setting so that user can save as binary files
                DefaultExt = "bin",                                                      //save the file as bin extension if all files is choosed.
                FileName = Path.GetFileName(currentFile)                                //set filename in the SaveFileDialog // getFileName extract the name only without path.
            };
            DialogResult sr = saveBinary.ShowDialog();
            if (sr == DialogResult.Cancel)
            {
                return;
            }
            if (sr == DialogResult.OK)
            {
                fileName = saveBinary.FileName;
                currentFile = fileName;
                toolStripStatusLabel1.Text = "File saved as: " + fileName;

                try
                {
                    using (Stream stream = File.Open(fileName, FileMode.Create))      //by using it open the file and  close (it will do background work for us)
                    {
                        BinaryFormatter bin = new BinaryFormatter();

                        {
                            for (int y = 0; y < colSize; y++)
                            {
                                for (int x = 0; x < rowSize; x++)
                                {
                                    if (myArray[x, y] == null)                      //if the array index contains null then it will be filled with empty to deal with null.
                                    {
                                        myArray[x, y] = string.Empty;              //It will fill all the element with empty and save it to binary. useful when same file is need to open.
                                    }
                                    bin.Serialize(stream, myArray[x, y]);
                                }
                            }
                        }
                    }

                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        #endregion

        #region AddFunctions
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try                                     
            {
                if (string.IsNullOrEmpty(textBoxName.Text))                                  //this code force the user to input something. As empty string is already in the array, need to mention NullnEmpty both
                {
                    errorProvider1.SetError(textBoxName, "Cannot leave blank");
                    
                }
                if (string.IsNullOrEmpty(textBoxCategory.Text))
                {
                    errorProvider1.SetError(textBoxCategory, "Cannot leave blank");
                }
                if (string.IsNullOrEmpty(textBoxStructure.Text))
                {
                    errorProvider1.SetError(textBoxStructure, "Cannot leave blank");
                }
                if (string.IsNullOrEmpty(textBoxDefinition.Text))
                {
                    errorProvider1.SetError(textBoxDefinition, "Cannot leave blank");
                }

                else
                {
                   
                        rowCounter = identifyNextEligibleRow();                             //Will get the next available index to be added.
                    if (rowCounter >= rowSize)                                       // will show exception if added more than the size of 2d array.
                    {
                        MessageBox.Show("Cannot add more than 12 element");
                        return;
                    }
                        myArray[rowCounter, 0] = textBoxName.Text;
                        myArray[rowCounter, 1] = textBoxCategory.Text;
                        myArray[rowCounter, 2] = textBoxStructure.Text;
                        myArray[rowCounter, 3] = textBoxDefinition.Text;
                        rowCounter++;
                        colCounter = 3;
                        DisplayArray();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
